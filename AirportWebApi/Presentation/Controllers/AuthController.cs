using Application.CQRS.Commands;
using Application.DTOs;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class AuthController : BaseController
{
    public AuthController(IMediator mediator) : base(mediator)
    {
    }
    
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<BaseResponse<TokenResponse>>> Login([FromBody] LoginDto dto)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var response = await _mediator.Send(new LoginCommand {Dto = dto}, cancellationToken);
        return Ok(response);
    }
}