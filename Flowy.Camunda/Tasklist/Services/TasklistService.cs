using Flowy.Camunda.Auth.Services;
using Flowy.Camunda.Common.Services;

namespace Flowy.Camunda.Tasklist.Services;

public class TasklistService : CommonService {

  public TasklistService(IAuthService ias) : base(ias) {
    BaseApiUrl = "http://localhost:8082/v1";
  }

}