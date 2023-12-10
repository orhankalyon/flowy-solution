using Flowy.Core.Contexts;
using Flowy.Core.Models;
using Flowy.Core.Models.Search;

namespace Flowy.Core.Services;

public interface IScopesService {
  Result<Scope> Search(Request request);
  Scope? GetScopeById(long id);
}

public class ScopesService : IScopesService {

  private readonly FlowyContext Context;

  public ScopesService(FlowyContext context) {
    Context = context;
  }

  public Result<Scope> Search(Request request) {
    Result<Scope> result = new () { Request = request };
    IQueryable<Scope>? queryable = Context.Scopes?
      .OrderBySort(request.Sort)
      .FiltersBy(request.Queries);
    result.Total = queryable != null ? queryable.Count() : 0;
    result.Items = queryable?.Skip(request.Offset).Take(request.Size).ToList();
    return result;
  }

  public Scope? GetScopeById(long id) {
    return Context.Scopes?.FirstOrDefault(s => s.Id.Equals(id));
  }
}