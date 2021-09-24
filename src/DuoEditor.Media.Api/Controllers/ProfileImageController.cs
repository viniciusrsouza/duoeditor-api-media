using DuoEditor.Media.App.UseCases;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DuoEditor.Media.Api.Controllers
{
  [ApiController]
  [Route("api/profile_images")]
  public class ProfileImageController : BaseController
  {
    public ProfileImageController(IMediator mediator) : base(mediator)
    {
    }

    [HttpPost("{id}")]
    [Authorize]
    public async Task<IActionResult> Post([FromRoute] int id, [FromForm] IFormFile image)
    {
      if (id != UserId)
      {
        return Unauthorized();
      }
      var userImage = await _mediator.Send(new SetUserImage(image.FileName, image.OpenReadStream(), id));
      return Ok(userImage);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
      var userImage = await _mediator.Send(new GetUserImage(id));
      return Ok(userImage);
    }
  }
}
