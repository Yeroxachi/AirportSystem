using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.DTOs.Flight;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extensions;
using Presentation.Filters;

namespace Presentation.Controllers;

public class FlightController : BaseController
{
    public FlightController(IMediator mediator) : base(mediator)
    {
    }
    
    [AuthorizeRoles("Admin")]
    [HttpPost]
    public async Task<ActionResult<BaseResponse<FlightResponse>>> CreateFlightAsync([FromBody] CreateFlightDto dto)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var response = await _mediator.Send(new CreateFlightCommand {Dto = dto}, cancellationToken);
        return HandleRequest(response);
    }

    [AllowAnonymous]
    [HttpGet("all")]
    public async Task<ActionResult<BaseResponse<IEnumerable<FlightResponse>>>> GetAllFlightAsync()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var response = await _mediator.Send(new GetAllFlightsQuery(), cancellationToken);
        return HandleRequest(response);
    }

    [AllowAnonymous]
    [HttpGet("{flightId:guid}")]
    public async Task<ActionResult<BaseResponse<FlightResponse>>> GetFlightByIdAsync([FromQuery] Guid flightId)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var response = await _mediator.Send(new GetFlightByIdQuery {FlightId = flightId}, cancellationToken);
        return HandleRequest(response);
    }

    [HttpPost("{flightId:guid}")]
    public async Task<ActionResult> RegistrationToFlightAsync([FromQuery] Guid flightId)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var response = await _mediator.Send(new RegistrationToFlightCommand {UserId = HttpContext.UserId(),FlightId = flightId}, cancellationToken);
        return HandleRequest(response);
    }

    [AuthorizeRoles("Admin")]
    [HttpPut]
    public async Task<ActionResult<BaseResponse<FlightResponse>>> UpdateFlightAsync([FromBody] UpdateFlightDto dto)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var response = await _mediator.Send(new UpdateFlightCommand {Dto = dto}, cancellationToken);
        return HandleRequest(response);
    }
}