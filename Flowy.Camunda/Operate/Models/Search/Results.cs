using Newtonsoft.Json;

namespace Flowy.Camunda.Operate.Models.Search;

public class Results<T> {
  [JsonProperty("items")]
  public List<T>? Items { get; set; }

  [JsonProperty("sortValues")]
  public List<object>? SortValues { get; set; }

  [JsonProperty("total")]
  public int Total { get; set; }
}