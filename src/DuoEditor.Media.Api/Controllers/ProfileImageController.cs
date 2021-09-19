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
    public async Task<IActionResult> PostAsync([FromRoute] int id, [FromForm] IFormFile image)
    {
      var userImage = await _mediator.Send(new SetUserImage(image.FileName, image.OpenReadStream(), id));
      return Ok(userImage);
    }
  }
}
