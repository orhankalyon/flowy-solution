using Flowy.Camunda.Auth.Services;
using Flowy.Camunda.Operate.Models;
using Flowy.Camunda.Operate.Models.Search;

namespace Flowy.Camunda.Operate.Services;

public interface IDecisionRequirementsService {
  Results<DecisionRequirement>? GetDecisionRequirements(Quary<DecisionRequirement>? quary = null);
  DecisionRequirement? GetDecisionRequirementByKey(long key);
  string? GetDecisionRequirementSchemaByKey(long key);
}

public class DecisionRequirementsService : OperateService, IDecisionRequirementsService {

  public DecisionRequirementsService(IAuthService ias) : base(ias) {}
  
  public Results<DecisionRequirement>? GetDecisionRequirements(Quary<DecisionRequirement>? quary = null){
    return Post<Results<DecisionRequirement>>(
      GetCompleteUrl("/drd/search"),
      quary != null ? quary : new {}
    );
  }

  public DecisionRequirement? GetDecisionRequirementByKey(long key) {
    return Get<DecisionRequirement>(GetCompleteUrl("/drd/" + key));
  }

  public string? GetDecisionRequirementSchemaByKey(long key){
    return Get<string>(GetCompleteUrl("/drd/" + key + "/xml"));
  }
}
