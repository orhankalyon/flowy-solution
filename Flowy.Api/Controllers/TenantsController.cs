using Flowy.Core.Models;
using Flowy.Core.Models.Search;
using Flowy.Core.Managements;
using Microsoft.AspNetCore.Mvc;

namespace Flowy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TenantsController : ControllerBase {

  private ITenantsManagement TenantsManagement;

  public TenantsController(ITenantsManagement tenantsMang) {
    TenantsManagement = tenantsMang;
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(Result<Tenant>), 200)]
  public IActionResult Search(Request request) {
    //var r = OperateService.GetProcessInstances(new () { Size = 10});
    var r = TenantsManagement.Search(request);
    return Ok(r);
  }


  /*private IProcessDefinitionService OperateService;

  public TestController(
    IProcessDefinitionService ios
  ){
    OperateService = ios;
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(object), 200)]
  public IActionResult PrimoTest(long key) {
    //var r = OperateService.GetProcessInstances(new () { Size = 10});
    var r = OperateService.GetProcessDefinitionSchemaByKey(key);
    return Ok(r);
  }*/
  
}
