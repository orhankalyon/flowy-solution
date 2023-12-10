namespace Flowy.Core.Models.Search;

public class Query {
  public string? Column { get; set; }
  public string? Method { get; set; }
  public object? Value { get; set; }
}