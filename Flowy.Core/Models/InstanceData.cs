using System.ComponentModel.DataAnnotations.Schema;

namespace Flowy.Core.Models;

public class InstanceData {
  public long Id { get; set; }

  [ForeignKey("Instance")]
  public long IdInsatnce { get; set; }
  public Instance? Instance { get; set; }
  public string? Name { get; set; }
  public string? Value { get; set; }

  #region CamundaRuntime
  [NotMapped]
  public long? KeyVariable { get; set; }
  [NotMapped]
  public string? ValueVariable { get; set; }
  #endregion
}