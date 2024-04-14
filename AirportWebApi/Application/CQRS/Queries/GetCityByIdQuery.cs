using Application.Mapper;
using Application.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetCityByIdQuery : IRequest<BaseResponse>
{
    public Guid CityId { get; init; }
}

public class GetCityByIdQueryHandler : BaseRequest, IRequestHandler<GetCityByIdQuery, BaseResponse>
{
    public GetCityByIdQueryHandler(AirportContext context) : base(context)
    {
    }

    public async Task<BaseResponse> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _Context.Cities.FirstOrDefaultAsync(x => x.Id == request.CityId, cancellationToken);
        if (city == null)
        {
            return NotFound($"City with Id {request.CityId} was not found");
        }

        var response = city.MapToCityResponse();
        return Ok(response);
    }
}