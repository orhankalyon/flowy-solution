using Newtonsoft.Json;

namespace Flowy.Camunda.Operate.Models;

public class ChangeStatus {

  [JsonProperty("message")]
  public string? Message { get; set; }

  [JsonProperty("deleted")]
  public long Deleted { get; set; }
}