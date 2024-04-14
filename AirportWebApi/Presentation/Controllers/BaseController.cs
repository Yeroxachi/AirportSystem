using System.Net;
using Application.Response;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController : ControllerBase
{
    protected readonly IMediator _mediator;

    public BaseController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    protected ActionResult HandleRequest(BaseResponse response)
    {
        ActionResult status = response.Code switch
        {
            ResponseCode.Created => new ObjectResult(response) { StatusCode = (int)HttpStatusCode.Created },
            ResponseCode.NotFound => NotFound(response),
            ResponseCode.Forbidden => Forbid(),
            ResponseCode.BadRequest => BadRequest(response),
            ResponseCode.UnAuthorize => Unauthorized(response),
            ResponseCode.Ok => Ok(response),
            ResponseCode.NoContent => NoContent(),
            _ => throw new ArgumentOutOfRangeException(nameof(response.Code))
        };
        return status;
    }
}