using Newtonsoft.Json;

namespace Flowy.Camunda.Operate.Models.Search;

public class Quary<T> {

  [JsonProperty("filter", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public T? Filter {get; set;}

  [JsonProperty("size", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public int Size { get; set; }

  [JsonProperty("searchAfter", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public List<object>? SearchAfter { get; set; }

  [JsonProperty("sort", DefaultValueHandling = DefaultValueHandling.Ignore)]
  public Sort? Sort { get; set; }
}