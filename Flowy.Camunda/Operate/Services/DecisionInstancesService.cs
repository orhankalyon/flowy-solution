using Flowy.Camunda.Auth.Services;
using Flowy.Camunda.Operate.Models;
using Flowy.Camunda.Operate.Models.Search;

namespace Flowy.Camunda.Operate.Services;

public interface IDecisionInstancesService {
  Results<DecisionInstance>? GetDecisionInstances(Quary<DecisionInstance>? quary = null);
  DecisionInstance? GetDecisionInstanceById(string id);
}

public class DecisionInstancesService : OperateService, IDecisionInstancesService {

  public DecisionInstancesService(IAuthService ias) : base(ias) {}
  
  public Results<DecisionInstance>? GetDecisionInstances(Quary<DecisionInstance>? quary = null){
    return Post<Results<DecisionInstance>>(
      GetCompleteUrl("/decision-instances/search"),
      quary != null ? quary : new {}
    );
  }

  public DecisionInstance? GetDecisionInstanceById(string id) {
    return Get<DecisionInstance>(GetCompleteUrl("/decision-instances/" + id));
  }

}
