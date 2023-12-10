using Flowy.Camunda.Operate.Models;
using Flowy.Camunda.Operate.Services;
using Flowy.Core.Models;
using Flowy.Core.Services;
using log4net;

namespace Flowy.Core.Managements;

public interface IProcessesManagement {
  ICollection<Process>? GetProcessesByIdScope(long idScope);
  ICollection<FlowNodeStatistics>? GetStatisticsByIdProcess(long idProcess);
  string? GetSchemaByIdProcess(long idProcess);
}

public class ProcessesManagement : IProcessesManagement {
  private static readonly ILog Log = LogManager.GetLogger(typeof(ProcessesManagement));
  private readonly IProcessesService ProcessesService;
  private readonly IProcessDefinitionsService ProcessDefinitionsService;
  private readonly IProcessInstancesService ProcessInstancesService;

  public ProcessesManagement(
    IProcessesService prs,
    IProcessDefinitionsService pds,
    IProcessInstancesService pis
  ){
    ProcessesService = prs;
    ProcessDefinitionsService = pds;
    ProcessInstancesService = pis;
  }

  public ICollection<Process>? GetProcessesByIdScope(long idScope){
    // recupero tutti i deployment archiviati per lo scope indicato
    ICollection<Process>? deployments = ProcessesService.GetProcessesByIdScope(idScope);
    if (deployments == null) { return null; }
    
    // per ogni deployments inderrogo camunda per recuperarmi il dettaglio
    /*foreach(Process deployment in deployments) {
      deployment.ProcessDefinition = ProcessDefinitionsService.GetProcessDefinitionByKey(deployment.Key);   
    }*/
    return deployments;
  }

  public ICollection<FlowNodeStatistics>? GetStatisticsByIdProcess(long idProcess){
    // recupero la deployment specifica per sapre la keyprocessdefinition di camunda
    Process? deployment = ProcessesService.GetProcessById(idProcess);
    if (deployment == null) { return null; }
    // inderrogo camunda per le statistiche sulle istanze del deployment
    return ProcessInstancesService.GetProcessInstancesStatisticsByProcessDefinition(deployment.Key);
  }
  
  public string? GetSchemaByIdProcess(long idProcess) {
    // recupero la deployment specifica per sapre la keyprocessdefinition di camunda
    Process? deployment = ProcessesService.GetProcessById(idProcess);
    if (deployment == null) { throw new Exception("No Process with id: " + idProcess); }
    // recupero lo schema del processo
    return ProcessDefinitionsService.GetProcessDefinitionSchemaByKey(deployment.Key);
  }
  
}