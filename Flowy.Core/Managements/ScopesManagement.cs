using Flowy.Core.Models;
using Flowy.Core.Models.Search;
using Flowy.Core.Services;
using log4net;

namespace Flowy.Core.Managements;

public interface IScopesManagement {
  Result<Scope> Search(Request request);
  Scope? GetScopeById(long id);
}

public class ScopesManagement : IScopesManagement {
  private static readonly ILog Log = LogManager.GetLogger(typeof(ScopesManagement));
  private readonly IScopesService ScopesService ;

  public ScopesManagement(
    IScopesService ss
  ){
    ScopesService = ss;
  }

  public Result<Scope> Search(Request request) {
    try {
      Log.Debug("Start Search");
      // verifico che sia stato specificato nelle queries il campo IdTenant
      if (request.Queries == null) { throw new Exception("Queries not found");}
      if (request.Queries.FirstOrDefault(q => q.Column != null && q.Column.Equals("IdTenant")) == null) { 
        throw new Exception("IdTenant not found in Queries");
      }
      return ScopesService.Search(request);
    } catch(Exception ex) {
      Log.Error(ex);
      throw;
    }
  }

  public Scope? GetScopeById(long id) {
    return ScopesService.GetScopeById(id);
  }
}