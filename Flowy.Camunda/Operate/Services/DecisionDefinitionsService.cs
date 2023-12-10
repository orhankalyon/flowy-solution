using Flowy.Camunda.Auth.Services;
using Flowy.Camunda.Operate.Models;
using Flowy.Camunda.Operate.Models.Search;

namespace Flowy.Camunda.Operate.Services;

public interface IDecisionDefinitionsService {
  Results<DecisionDefinition>? GetDecisionDefinitions(Quary<DecisionDefinition>? quary = null);
  DecisionDefinition? GetDecisionDefinitionByKey(long key);
}

public class DecisionDefinitionsService : OperateService, IDecisionDefinitionsService {

  public DecisionDefinitionsService(IAuthService ias) : base(ias) {}
  
  public Results<DecisionDefinition>? GetDecisionDefinitions(Quary<DecisionDefinition>? quary = null){
    return Post<Results<DecisionDefinition>>(
      GetCompleteUrl("/decision-definitions/search"),
      quary != null ? quary : new {}
    );
  }

  public DecisionDefinition? GetDecisionDefinitionByKey(long key) {
    return Get<DecisionDefinition>(GetCompleteUrl("/decision-definitions/" + key));
  }

}
