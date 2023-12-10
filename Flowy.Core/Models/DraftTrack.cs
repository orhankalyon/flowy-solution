using System.ComponentModel.DataAnnotations.Schema;

namespace Flowy.Core.Models;

public class DraftTrack {
  public long Id { get; set; }
  
  [ForeignKey("Draft")]
  public long IdDraft { get; set; }
  public Draft? Draft { get; set; }
  public DateTime? EventAt { get; set; }
  public string? UserIdentifier { get; set; }
  public string? Operation { get; set; }
  public string? Description { get; set; }
  public string? SchemaBackup { get; set; }
}