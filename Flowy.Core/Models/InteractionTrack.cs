using System.ComponentModel.DataAnnotations.Schema;
namespace Flowy.Core.Models;

public class InteractionTrack {
  public long Id { get; set; }

  [ForeignKey("Interaction")]
  public long IdInteraction { get; set; }
  public Interaction? Interaction { get; set; }
  public DateTime? EventAt { get; set; }
  public string? UserIdentifier { get; set; }
  public string? Operation { get; set; }
  public string? Description { get; set; }
  public string? DataBackup { get; set; }
}