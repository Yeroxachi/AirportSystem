using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.DTOs.Flight;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;

namespace Presentation.Controllers;

public class FlightController : BaseController
{
    public FlightController(IMediator mediator) : base(mediator)
    {
    }
    
    [AuthorizeRoles("Admin")]
    [HttpPost]
    public async Task<ActionResult<BaseResponse<FlightResponse>>> Create([FromBody] CreateFlightDto dto)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var response = await _mediator.Send(new CreateFlightCommand {Dto = dto}, cancellationToken);
        return HandleRequest(response);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<BaseResponse<IEnumerable<FlightResponse>>>> GetAll()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var response = await _mediator.Send(new GetAllFlightsQuery(), cancellationToken);
        return HandleRequest(response);
    }
}