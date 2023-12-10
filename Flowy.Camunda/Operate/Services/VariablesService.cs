using Flowy.Camunda.Auth.Services;
using Flowy.Camunda.Operate.Models;
using Flowy.Camunda.Operate.Models.Search;

namespace Flowy.Camunda.Operate.Services;

public interface IVariablesService {
  Results<Variable>? GetVariables(Quary<Variable>? quary = null);
  Variable? GetVariableByKey(long key);
}

public class VariablesService : OperateService, IVariablesService {

  public VariablesService(IAuthService ias) : base(ias) {}
  
  public Results<Variable>? GetVariables(Quary<Variable>? quary = null){
    return Post<Results<Variable>>(
      GetCompleteUrl("/variables/search"),
      quary != null ? quary : new {}
    );
  }

  public Variable? GetVariableByKey(long key) {
    return Get<Variable>(GetCompleteUrl("/variables/" + key));
  }

}
