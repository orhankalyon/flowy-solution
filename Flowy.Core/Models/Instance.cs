using System.ComponentModel.DataAnnotations.Schema;

namespace Flowy.Core.Models;

public class Instance {
  public long Id { get; set; }

  [ForeignKey("Process")]
  public long IdProcess { get; set; }
  public Process? Process { get; set; }
  public long Key { get; set; }
  public DateTime CreatedAt { get; set; }
  public string? Reference { get; set; }

  #region CamundaRuntime
  [NotMapped]
  public long? ParentKey {get; set;}
  [NotMapped]
  public long? ParentFlowNodeInstanceKey {get; set;}
  [NotMapped]
  public string? StartDate {get; set;}
  [NotMapped]
  public string? EndDate {get; set;}
  [NotMapped]
  public string? State {get; set;}
  [NotMapped]
  public long? ProcessDefinitionKey {get; set;}
  [NotMapped]
  public string? TenantId  {get; set;}
  [NotMapped]
  public object? ParentProcessInstanceKey {get; set;}
  #endregion
}