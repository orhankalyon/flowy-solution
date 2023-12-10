using System.ComponentModel.DataAnnotations.Schema;
using Flowy.Camunda.Operate.Models;

namespace Flowy.Core.Models;

public class Process {
  public long Id { get; set; }

  [ForeignKey("Scope")]
  public long IdScope { get; set; }
  public Scope? Scope { get; set; }

  // camunda variables
  public long Key { get; set; }
  public string? Name { get; set; }
  public int Version { get; set; }
  public string? BpmnProcessId { get; set; }
  public string? TenantId { get; set; }
}