using Flowy.Core.Managements;
using Flowy.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Flowy.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class InteractionsController : ControllerBase {

  private IInteractionsManagement InteractionsManagement;

  public InteractionsController(IInteractionsManagement interactionsManagement) {
    InteractionsManagement = interactionsManagement;
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<Interaction>), 200)]
  public IActionResult GetInteractionsByIdScope(long idScope) {
    var r = InteractionsManagement.GetInteractionsByIdScope(idScope);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(Interaction), 200)]
  public IActionResult GetInteractionById(long id) {
    var r = InteractionsManagement.GetInteractionById(id);
    return Ok(r);
  }

  [HttpGet]
  [Route("[action]")]
  [ProducesResponseType(typeof(List<Interaction>), 200)]
  public IActionResult GetInteractionTracksByIdDraft(long idInteraction) {
    var r = InteractionsManagement.GetInteractionTracksByIdInteraction(idInteraction);
    return Ok(r);
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(bool), 200)]
  public IActionResult UpdateInteractionData(Interaction interaction) {
    InteractionsManagement.UpdateInteractionData(interaction);
    return Ok();
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(bool), 200)]
  public IActionResult UpdateInteractionInfo(Interaction interaction) {
    InteractionsManagement.UpdateInteractionInfo(interaction);
    return Ok();
  }

  [HttpPut]
  [Route("[action]")]
  [ProducesResponseType(typeof(Interaction), 200)]
  public IActionResult CloneInteraction([FromQuery] long idInteraction) {
    var newi = InteractionsManagement.CloneInteraction(idInteraction);
    return Ok(newi);
  }

  [HttpPost]
  [Route("[action]")]
  [ProducesResponseType(typeof(Interaction), 200)]
  public IActionResult NewInteraction(Interaction interaction) {
    InteractionsManagement.NewInteraction(interaction);
    return Ok(interaction);
  }
}