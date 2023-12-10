using Flowy.Camunda.Auth.Services;
using Flowy.Camunda.Common.Services;
using Flowy.Camunda.Operate.Models.Search;

namespace Flowy.Camunda.Operate.Services;

public class OperateService : CommonService {
  public OperateService(IAuthService ias) : base(ias) {
    BaseApiUrl = "http://localhost:8081/v1";
  }
}