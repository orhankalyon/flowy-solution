using Flowy.Camunda.Auth.Services;
using Flowy.Camunda.Tasklist.Models;

namespace Flowy.Camunda.Tasklist.Services;

public interface ITaskVariablesService {
  TaskVariable? GetVariableById(string variableId);
}

public class TaskVariablesService : TasklistService, ITaskVariablesService {
  public TaskVariablesService(IAuthService ias) : base(ias) { }

  public TaskVariable? GetVariableById(string variableId){
    return Get<TaskVariable>(GetCompleteUrl("/variables/" + variableId));
  }
}