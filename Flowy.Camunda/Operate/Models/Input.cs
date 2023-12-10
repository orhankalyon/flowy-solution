using Newtonsoft.Json;

namespace Flowy.Camunda.Operate.Models;

public class Input {

  [JsonProperty("id")]
  public string? Id	{ get; set;}

  [JsonProperty("name")]
  public string? Name	{ get; set; }

  [JsonProperty("value")]
  public string? Value	{ get; set; }
}