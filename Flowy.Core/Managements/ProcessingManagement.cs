using Flowy.Camunda.Tasklist.Models;
using Flowy.Camunda.Tasklist.Services;
using Flowy.Camunda.Zeebe.Services;
using Flowy.Core.Helpers;
using Flowy.Core.Models;
using Flowy.Core.Services;
using Zeebe.Client.Api.Responses;

namespace Flowy.Core.Managements;

public interface IProcessingManagement {
  Instance? Start(long idProcess);
  InstanceTask GetInstanceTaskById(long idTask);
  Interaction GetInteractionByIdTask(long idTask);
}

public class ProcessingManagement : IProcessingManagement {

  private readonly IInstancesService InstancesService;
  private readonly IProcessesService ProcessesService;
  private readonly IZeebeService ZeebeService;
  private readonly ITasksService TasksService;
  private readonly IFormsService FormsService;
  private readonly IInteractionsService InteractionsService;

  public ProcessingManagement(
    IZeebeService zeebeSrv,
    IProcessesService deploymentsSrv,
    IInstancesService instancesSrv,
    ITasksService tasksService,
    IFormsService formsService,
    IInteractionsService interactionsService
  ) {
    ZeebeService = zeebeSrv;
    ProcessesService = deploymentsSrv;
    InstancesService = instancesSrv;
    TasksService = tasksService;
    FormsService = formsService;
    InteractionsService = interactionsService;
  }

  public Instance? Start(long idProcess) {
    string reference = Guid.NewGuid().ToString();
    // recupero prima il deployment
    Process? process = ProcessesService.GetProcessById(idProcess);
    if (process == null) { throw new Exception("Deployment not found by id: " + idProcess);}
    
    //creo una nuova istanza in camunda
    IProcessInstanceResponse response = ZeebeService.CreateProcessInstance(process.Key);

    Instance instance = new() {
      CreatedAt = DateTime.Now,
      IdProcess = process.Id,
      Key = response.ProcessInstanceKey,
      Reference = reference
    };

    InstancesService.Insert(instance);
    return instance;
    /*
     public long IdProcess { get; set; }
  public Process? Process { get; set; }
  public long Key { get; set; }
  public DateTime CreatedAt { get; set; }
  public string? Reference { get; set; }
    */

  }

  public InstanceTask GetInstanceTaskById(long idTask) {
    // recupero il task da camunda
    Camunda.Tasklist.Models.Task? task = TasksService.GetTaskById(idTask.ToString());
    if (task == null) { throw new Exception("Task not found with id: " + idTask);}
    return MappingHelper.MappTask(task);
  }

  public Interaction GetInteractionByIdTask(long idTask) {
    Camunda.Tasklist.Models.Task? task = TasksService.GetTaskById(idTask.ToString());
    if (task == null) { throw new Exception("Task not found with id: " + idTask);}
    if (task.FormKey == null) { throw new Exception("Task Without formkey");}
    if (task.ProcessDefinitionKey == null) { throw new Exception("Task Without processdefinition");}
    Form? form = FormsService.GetFormByIdAndProcessDefinition(task.FormKey, task.ProcessDefinitionKey);
    // se è stata trovata una form allora la prendo e la convero in Interaction per restituirla
    if (form != null) { return MappingHelper.MappInteraction(form);}
    // altrimenti vedo di recuperare l'interaction
    Interaction? interaction = InteractionsService.GetInteractionByName(task.FormKey);
    if (interaction == null) { throw new Exception("No interaction with name: " + task.FormKey);}
    return interaction;
  }

}