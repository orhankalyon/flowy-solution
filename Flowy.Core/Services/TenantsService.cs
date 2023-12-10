using Flowy.Core.Contexts;
using Flowy.Core.Models;
using Flowy.Core.Models.Search;

namespace Flowy.Core.Services;

public interface ITenantsService {
  Result<Tenant> Search(Request request);
}

public class TenantsService : ITenantsService {

  private readonly FlowyContext Context;

  public TenantsService(FlowyContext context) {
    Context = context;
  }

  public Result<Tenant> Search(Request request) {
    Result<Tenant> result = new () { Request = request };
    IQueryable<Tenant>? queryable = Context.Tenants?
      .OrderBySort(request.Sort)
      .FiltersBy(request.Queries);
    result.Total = queryable != null ? queryable.Count() : 0;
    result.Items = queryable?.Skip(request.Offset).Take(request.Size).ToList();
    return result;
  }

}