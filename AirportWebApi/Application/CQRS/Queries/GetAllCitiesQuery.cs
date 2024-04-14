using Application.Mapper;
using Application.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetAllCitiesQuery : IRequest<BaseResponse>
{
}

public class GetAllCitiesQueryHandler : BaseRequest, IRequestHandler<GetAllCitiesQuery, BaseResponse>
{
    public GetAllCitiesQueryHandler(AirportContext context) : base(context)
    {
    }

    public async Task<BaseResponse> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _Context.Cities.ToListAsync(cancellationToken);
        var response = cities.MapToCityResponse();
        return Ok(response);
    }
}