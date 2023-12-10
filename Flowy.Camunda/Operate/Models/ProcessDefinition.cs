using Newtonsoft.Json;

namespace Flowy.Camunda.Operate.Models;

public class ProcessDefinition {

  [JsonProperty("key")]
  public long Key { get; set; }

  [JsonProperty("name")]
  public string? Name { get; set; }

  [JsonProperty("version")]
  public int Version { get; set; }

  [JsonProperty("bpmnProcessId")]
  public string? BpmnProcessId { get; set; }

  [JsonProperty("tenantId")]
  public string? TenantId { get; set; }
}