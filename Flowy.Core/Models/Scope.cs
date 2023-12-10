using System.ComponentModel.DataAnnotations.Schema;

namespace Flowy.Core.Models;

public class Scope {
  public long Id { get; set; }
  [ForeignKey("Tenant")]
  public long IdTenant { get; set; }
  public Tenant? Tenant { get; set;}
  public string? Name { get; set; }
  public string? Description { get; set; }
}