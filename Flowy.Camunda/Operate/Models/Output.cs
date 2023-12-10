using Newtonsoft.Json;

namespace Flowy.Camunda.Operate.Models;

public class Output {
  
  [JsonProperty("id")]
  public string? Id	{ get; set; }

  [JsonProperty("name")]
  public string? Name	{ get; set; }

  [JsonProperty("value")]
  public string? Value { get; set; }

  [JsonProperty("ruleId")]
  public string? RuleId	{ get; set; }

  [JsonProperty("ruleIndex")]
  public int RuleIndex	{ get; set; }
}