using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DuoEditor.Media.Api.Controllers
{
  [ApiController]
  public class BaseController : ControllerBase
  {
    protected int? UserId
    {
      get
      {
        var idClaim = User.FindFirst(ClaimTypes.Name);

        if (idClaim == null)
        {
          return null;
        }

        return int.Parse(idClaim.Value);
      }
    }

    protected readonly IMediator _mediator;
    public BaseController(IMediator mediator)
    {
      _mediator = mediator;
    }
  }
}