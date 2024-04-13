using Application.CQRS.Commands;
using Application.DTOs;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class UserController : BaseController
{
    public UserController(IMediator mediator) : base(mediator)
    {
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<BaseResponse<UserResponse>>> CreateUserAsync([FromBody] CreateUserDto dto)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var response = await _mediator.Send(new CreateUserCommand{Dto = dto}, cancellationToken);
        return HandleRequest(response);
    }
}