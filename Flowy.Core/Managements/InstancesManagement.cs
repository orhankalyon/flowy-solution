using Flowy.Camunda.Operate.Models;
using Flowy.Camunda.Operate.Models.Search;
using Flowy.Camunda.Operate.Services;
using Flowy.Camunda.Tasklist.Models;
using Flowy.Camunda.Tasklist.Services;
using Flowy.Core.Helpers;
using Flowy.Core.Models;
using Flowy.Core.Models.Search;
using Flowy.Core.Services;

namespace Flowy.Core.Managements;

public interface IInstancesManagement {
  Instance GetInstanceById(long id);
  Result<Instance> GetInstancesByIdProcess(Request request);
  List<InstanceData>? GetInstanceDatasByIdInstance(long idInstance);
  List<InstanceTrack>? GetInstanceTracksByIdInstance(long idInstance);
  List<InstanceTask>? GetInstanceTasksByIdInstance(long idInstance);
}

public class InstancesManagement : IInstancesManagement {
  private readonly IProcessesService ProcessesService;
  private readonly IInstancesService InstancesService;

  private readonly IProcessInstancesService ProcessInstancesService;
  private readonly IVariablesService VariablesService;
  private readonly IFlowNodeInstancesService FlowNodeInstancesService;
  private readonly ITasksService TasksService;

  public InstancesManagement(
    IProcessesService pros,
    IInstancesService insServ,
    IProcessInstancesService pis,
    IVariablesService vars,
    IFlowNodeInstancesService fnServ,
    ITasksService tServ
  ) {
    ProcessesService = pros;
    InstancesService = insServ;
    ProcessInstancesService = pis;
    VariablesService = vars;
    FlowNodeInstancesService = fnServ;
    TasksService = tServ;
  }

  public Instance GetInstanceById(long id) {
    // recupero l'istanza dal database
    Instance? instance = InstancesService.GetInstanceById(id);
    if (instance == null) { throw new Exception("No Instance by id: " + id); }
    // interrogo camunda sull'istanza
    ProcessInstance? processInstance = ProcessInstancesService.GetProcessInstanceByKey(instance.Key);
    if (processInstance != null) {
      instance.State = processInstance.State;
      instance.ParentKey = processInstance.ParentKey;
      instance.ParentFlowNodeInstanceKey = processInstance.ParentFlowNodeInstanceKey;
      instance.StartDate = processInstance.StartDate;
      instance.EndDate = processInstance.EndDate;
      instance.ProcessDefinitionKey = processInstance.ProcessDefinitionKey;
      instance.TenantId = processInstance.TenantId;
      instance.ParentProcessInstanceKey = processInstance.ParentProcessInstanceKey;
    }

    return instance;
  }

  public List<InstanceData>? GetInstanceDatasByIdInstance(long idInstance) {
    // recupero l'istanza dal database
    Instance? instance = InstancesService.GetInstanceById(idInstance);
    if (instance == null) { throw new Exception("No Instance by id: " + idInstance); }

    // recupero le instance data
    List<InstanceData>? datas = InstancesService.GetInstanceDatasByIdInstance(idInstance);
    if (datas == null) { datas = new List<InstanceData>(); }

    // recupero le variabili da camunda
    Results<Variable>? variables = VariablesService.GetVariables(new Quary<Variable>(){
      Size = 1000,
      Filter = new Variable(){
        ProcessInstanceKey = instance.Key
      }
    });

    if (variables != null && variables.Items != null && variables.Items.Count > 0) {
      foreach(Variable variable in variables.Items) {
        InstanceData? found = datas.FirstOrDefault(idat => idat.Name != null && idat.Name.Equals(variable.Name));
        if (found != null){
          found.KeyVariable = variable.Key;
          found.ValueVariable = variable.Value;
        } else {
          datas.Add(new(){
            IdInsatnce = instance.Id,
            Name = variable.Name,
            KeyVariable = variable.Key,
            ValueVariable = variable.Value
          });
        }
      }
    }
    return datas;
  }

  public List<InstanceTask>? GetInstanceTasksByIdInstance(long idInstance) {
    // recupero l'istanza dal database
    Instance? instance = InstancesService.GetInstanceById(idInstance);
    if (instance == null) { throw new Exception("No Instance by id: " + idInstance); }

    // costruisco la lista dei task
    List<InstanceTask> tasks = new List<InstanceTask>();

    // recupero i tasks da camunda
    List<Camunda.Tasklist.Models.Task>? camundaTasks = TasksService.GetTasks(new TaskQuary(){
      PageSize = 1000,
      ProcessInstanceKey = instance.Key.ToString()
    });

    if(camundaTasks != null && camundaTasks.Count > 0) {
      foreach(Camunda.Tasklist.Models.Task camundaTask in camundaTasks) {        
        tasks.Add(MappingHelper.MappTask(camundaTask));
      }
    }
    return tasks;
  }

  public List<InstanceTrack>? GetInstanceTracksByIdInstance(long idInstance) {
    // recupero l'istanza dal database
    Instance? instance = InstancesService.GetInstanceById(idInstance);
    if (instance == null) { throw new Exception("No Instance by id: " + idInstance); }

    // recupero la tracciatura
    List<InstanceTrack>? tracks = InstancesService.GetInstanceTracksByIdInstance(idInstance);
    if (tracks == null) { tracks = new List<InstanceTrack>(); }

    // recupero la tracciatura da camunda
    Results<FlowNodeInstance>? results = FlowNodeInstancesService.GetFlowNodeInstances(new Quary<FlowNodeInstance>() {
      Size = 1000,
      Filter = new FlowNodeInstance() {
        ProcessInstanceKey = instance.Key
      }
    });
    if (results != null && results.Items != null && results.Items.Count > 0) {
      foreach(FlowNodeInstance flowNodeInstance in results.Items) {
        tracks.Add(MappingHelper.MappTrack(flowNodeInstance));
      }
    }
    // riordino la lista
    tracks.Sort((a, b) => {
      if (!a.EventAt.HasValue) { return 0; }
      if (!b.EventAt.HasValue) { return 0; }
      return a.EventAt.Value.CompareTo(b.EventAt.Value);
    });
    return tracks;
  }

  public Result<Instance> GetInstancesByIdProcess(Request request) {
    if (request.Queries == null) { throw new Exception("No filter queries"); }
    
    // recupero l'idProcess per usarlo nel filtro
    Query? idProcessQuery = request.Queries.FirstOrDefault(q => q.Column != null && q.Column.Equals("IdProcess"));
    string? idProcessString = idProcessQuery?.Value?.ToString();
    if (idProcessString == null) { throw new Exception("IdDeployment not in queries");}
    long idProcess = long.Parse(idProcessString);
    Process? process = ProcessesService.GetProcessById(idProcess);
    if (process == null) { throw new Exception("Deployment not found with id : " + idProcess);}
    // recupero lo stato per usarlo nel filtro
    Query? stateQuary = request.Queries.FirstOrDefault(q => q.Column != null && q.Column.Equals("State"));
    string? state = stateQuary?.Value?.ToString();

    // costruisco la risposta
    Result<Instance> result = new (){
      Request = request,
      Items = new List<Instance>()
    };

    // interrogo camunda
    Results<ProcessInstance>? resultCamunda = ProcessInstancesService.GetProcessInstances(new Quary<ProcessInstance>(){
      Size = request.Size,
      SearchAfter = request.SearchAfter,
      Filter = new ProcessInstance(){
        ProcessDefinitionKey = process.Key,
        State = state
      }
    });
    if (resultCamunda == null) { throw new Exception("Camunda request failed."); }
    result.Total = resultCamunda.Total;
    result.SortValues = resultCamunda.SortValues;

    // ciclo sui risultati di camunda per completarli con quelli di flowy
    if (resultCamunda.Items != null){
      foreach(ProcessInstance procInst in resultCamunda.Items){
        // se presente l'oggetto nel database lo recupero
        Instance? instance = null;
        if (procInst.Key.HasValue) { instance = InstancesService.GetInstanceByKey(procInst.Key.Value); }
        if (instance == null) { instance = new(); }
        instance.State = procInst.State;
        instance.ParentKey = procInst.ParentKey;
        instance.ParentFlowNodeInstanceKey = procInst.ParentFlowNodeInstanceKey;
        instance.StartDate = procInst.StartDate;
        instance.EndDate = procInst.EndDate;
        instance.ProcessDefinitionKey = procInst.ProcessDefinitionKey;
        instance.TenantId = procInst.TenantId;
        instance.ParentProcessInstanceKey = procInst.ParentProcessInstanceKey;
        result.Items.Add(instance);
      }
    }

    return result;
  }

}