using Flowy.Camunda.Auth.Services;
using Flowy.Camunda.Tasklist.Models;

namespace Flowy.Camunda.Tasklist.Services;

public interface IFormsService {
  Form? GetFormByIdAndProcessDefinition(string formId, string processDefinitionKey);
}

public class FormsService : TasklistService, IFormsService {
  public FormsService(IAuthService ias) : base(ias) { }

  public Form? GetFormByIdAndProcessDefinition(string formId, string processDefinitionKey){
    return Get<Form>(GetCompleteUrl("/forms/" + formId + "?processDefinitionKey="+processDefinitionKey));
  }
}