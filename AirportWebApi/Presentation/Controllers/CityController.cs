using Application.CQRS.Commands;
using Application.CQRS.Queries;
using Application.DTOs.City;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Filters;

namespace Presentation.Controllers;

public class CityController : BaseController
{
    public CityController(IMediator mediator) : base(mediator)
    {
    }

    [AuthorizeRoles("Admin")]
    [HttpPost]
    public async Task<ActionResult<BaseResponse<CityResponse>>> CreateCityAsync([FromBody] CreateCityDto dto)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var response = await _mediator.Send(new CreateCityCommand {Dto = dto}, cancellationToken);
        return HandleRequest(response);
    }

    [AllowAnonymous]
    [HttpGet("all")]
    public async Task<ActionResult<BaseResponse<IEnumerable<CityResponse>>>> GetAllCityAsycn()
    {
        var cancellationToken = HttpContext.RequestAborted;
        var response = await _mediator.Send(new GetAllCitiesQuery(), cancellationToken);
        return HandleRequest(response);
    }

    [AllowAnonymous]
    [HttpGet("{cityId:guid}")]
    public async Task<ActionResult<BaseResponse<CityResponse>>> GetCityByIdAsync([FromQuery] Guid cityId)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var response = await _mediator.Send(new GetCityByIdQuery {CityId = cityId}, cancellationToken);
        return HandleRequest(response);
    }

    [AuthorizeRoles("Admin")]
    [HttpPut]
    public async Task<ActionResult<BaseResponse<CityResponse>>> UpdateCityAsync([FromBody] UpdateCityDto dto)
    {
        var cancellationToken = HttpContext.RequestAborted;
        var response = await _mediator.Send(new UpdateCityCommand {Dto = dto}, cancellationToken);
        return HandleRequest(response);
    }
}