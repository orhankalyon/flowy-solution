using Flowy.Camunda.Auth.Services;
using Flowy.Camunda.Operate.Models;
using Flowy.Camunda.Operate.Models.Search;

namespace Flowy.Camunda.Operate.Services;

public interface IFlowNodeInstancesService {
  Results<FlowNodeInstance>? GetFlowNodeInstances(Quary<FlowNodeInstance>? quary = null);
  FlowNodeInstance? GetFlowNodeInstanceByKey(long key);
}

public class FlowNodeInstancesService : OperateService, IFlowNodeInstancesService {

  public FlowNodeInstancesService(IAuthService ias) : base(ias) {}
  
  public Results<FlowNodeInstance>? GetFlowNodeInstances(Quary<FlowNodeInstance>? quary = null){
    return Post<Results<FlowNodeInstance>>(
      GetCompleteUrl("/flownode-instances/search"),
      quary != null ? quary : new {}
    );
  }

  public FlowNodeInstance? GetFlowNodeInstanceByKey(long key) {
    return Get<FlowNodeInstance>(GetCompleteUrl("/flownode-instances/" + key));
  }

}
