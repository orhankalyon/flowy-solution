using Flowy.Core.Managements;
using Flowy.Core.Models;
using Flowy.Core.Models.Search;
using Microsoft.AspNetCore.Mvc;

namespace Flowy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InstancesController : ControllerBase {

  private IInstancesManagement InstancesManagement;

  public InstancesController(IInstancesManagement instancesMan) {
    InstancesManagement = instancesMan;
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(Result<Instance>), 200)]
  public IActionResult GetInstancesByIdProcess(Request request) {
    var r = InstancesManagement.GetInstancesByIdProcess(request);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(Instance), 200)]
  public IActionResult GetInstanceById(long id) {
    var r = InstancesManagement.GetInstanceById(id);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<InstanceData>), 200)]
  public IActionResult GetInstanceDatasByIdInstance(long idInstance) {
    var r = InstancesManagement.GetInstanceDatasByIdInstance(idInstance);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<InstanceTrack>), 200)]
  public IActionResult GetInstanceTracksByIdInstance(long idInstance) {
    var r = InstancesManagement.GetInstanceTracksByIdInstance(idInstance);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<InstanceTask>), 200)]
  public IActionResult GetInstanceTasksByIdInstance(long idInstance) {
    var r = InstancesManagement.GetInstanceTasksByIdInstance(idInstance);
    return Ok(r);
  }
  
}