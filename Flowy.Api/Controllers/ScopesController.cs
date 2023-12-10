using Flowy.Core.Models;
using Flowy.Core.Models.Search;
using Flowy.Core.Managements;
using Microsoft.AspNetCore.Mvc;

namespace Flowy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ScopesController : ControllerBase {

  private readonly IScopesManagement ScopesManagement;

  public ScopesController(IScopesManagement scopesMang) {
    ScopesManagement = scopesMang;
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(Result<Scope>), 200)]
  public IActionResult Search(Request request) {
    Result<Scope> result = ScopesManagement.Search(request);
    return Ok(result);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(Scope), 200)]
  public IActionResult GetScopeById(long id) {
    return Ok(ScopesManagement.GetScopeById(id));
  }
  
}
