using Flowy.Core.Managements;
using Flowy.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Flowy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class DraftsController : ControllerBase {

  private IDraftsManagement DraftsManagement;

  public DraftsController(IDraftsManagement draftsManagement) {
    DraftsManagement = draftsManagement;
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<Draft>), 200)]
  public IActionResult GetDraftsByIdScope(long idScope) {
    var r = DraftsManagement.GetDraftsByIdScope(idScope);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(Draft), 200)]
  public IActionResult GetDraftById(long idDraft) {
    var r = DraftsManagement.GetDraftById(idDraft);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<Draft>), 200)]
  public IActionResult GetDraftTracksByIdDraft(long idDraft) {
    var r = DraftsManagement.GetDraftTracksByIdDraft(idDraft);
    return Ok(r);
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(bool), 200)]
  public IActionResult UpdateDraftSchema(Draft draft) {
    DraftsManagement.UpdateDraftSchema(draft);
    return Ok();
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(bool), 200)]
  public IActionResult UpdateDraftInfo(Draft draft) {
    DraftsManagement.UpdateDraftInfo(draft);
    return Ok();
  }

  [HttpPut]
  [Route("[action]")]
  [ProducesResponseType(typeof(Draft), 200)]
  public IActionResult CloneDraft([FromQuery] long idDraft) {
    var newDraft = DraftsManagement.CloneDraft(idDraft);
    return Ok(newDraft);
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(Draft), 200)]
  public IActionResult NewDraft(Draft draft) {
    DraftsManagement.NewDraft(draft);
    return Ok(draft);
  }

  [HttpPut]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<Process>), 200)]
  public IActionResult DeployDraft([FromQuery] long idDraft) {
    var r = DraftsManagement.DeployDraft(idDraft);
    return Ok(r);
  }
}
