using Flowy.Camunda.Auth.Services;
using Flowy.Camunda.Operate.Models;
using Flowy.Camunda.Operate.Models.Search;

namespace Flowy.Camunda.Operate.Services;

public interface IProcessDefinitionsService {
  Results<ProcessDefinition>? GetProcessDefinitions(Quary<ProcessDefinition>? quary = null);
  ProcessDefinition? GetProcessDefinitionByKey(long key);
  string? GetProcessDefinitionSchemaByKey(long key);
}

public class ProcessDefinitionsService : OperateService, IProcessDefinitionsService {
  public ProcessDefinitionsService(IAuthService ias) : base(ias){ }

  public Results<ProcessDefinition>? GetProcessDefinitions(Quary<ProcessDefinition>? quary = null) {
    return Post<Results<ProcessDefinition>>(
      GetCompleteUrl("/process-definitions/search"),
      quary != null ? quary : new {}
    );
  }

  public ProcessDefinition? GetProcessDefinitionByKey(long key) {
    return Get<ProcessDefinition>(GetCompleteUrl("/process-definitions/" + key));
  }

  public string? GetProcessDefinitionSchemaByKey(long key){
    return Get<string>(GetCompleteUrl("/process-definitions/" + key + "/xml"));
  }
}
