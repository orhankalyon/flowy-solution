using Flowy.Core.Managements;
using Flowy.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Flowy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProcessingController : ControllerBase {

  private IProcessingManagement ProcessingManagement;

  public ProcessingController(IProcessingManagement processingManagement) {
    ProcessingManagement = processingManagement;
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(bool), 200)]
  public IActionResult Start(long idProcess) {
    var res = ProcessingManagement.Start(idProcess);
    return Ok(res);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(InstanceTask), 200)]
  public IActionResult GetInstanceTaskById(long idTask) {
    var res = ProcessingManagement.GetInstanceTaskById(idTask);
    return Ok(res);
  }
  

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(Interaction), 200)]
  public IActionResult GetInteractionByIdTask(long idTask) {
    var res = ProcessingManagement.GetInteractionByIdTask(idTask);
    return Ok(res);
  }
}
