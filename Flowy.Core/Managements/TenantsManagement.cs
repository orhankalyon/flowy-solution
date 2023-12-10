using Flowy.Core.Models;
using Flowy.Core.Models.Search;
using Flowy.Core.Services;
using log4net;

namespace Flowy.Core.Managements;

public interface ITenantsManagement {
  Result<Tenant> Search(Request request);
}

public class TenantsManagement : ITenantsManagement {
  private static readonly ILog Log = LogManager.GetLogger(typeof(TenantsManagement));

  private readonly ITenantsService TenantsService ;

  public TenantsManagement(
    ITenantsService ts
  ){
    TenantsService = ts;
  }
  
  public Result<Tenant> Search(Request request) {
    return TenantsService.Search(request);
  }
}