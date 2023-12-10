using Newtonsoft.Json;

namespace Flowy.Camunda.Operate.Models.Search;

public class Sort {

  [JsonProperty("field", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Field { get; set; }

  /// <summary>
  /// ASC, DESC 
  /// </summary>
  [JsonProperty("order", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public string? Order { get; set; }
}