using System.ComponentModel.DataAnnotations.Schema;

namespace Flowy.Core.Models;

public class Interaction {
  public long Id { get; set; }

  [ForeignKey("Scope")]
  public long IdScope { get; set; }
  public Scope? Scope { get; set; }
  public string? Type { get; set; }
  public string? Name { get; set; }
  public string? Description { get; set; }
  public string? Data { get; set; }
}