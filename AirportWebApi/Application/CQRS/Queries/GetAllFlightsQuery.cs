using Application.Mapper;
using Application.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetAllFlightsQuery : IRequest<BaseResponse>
{
}

public class GetAllFlightsQueryHandler : BaseRequest, IRequestHandler<GetAllFlightsQuery, BaseResponse>
{
    public GetAllFlightsQueryHandler(AirportContext context) : base(context)
    {
    }

    public async Task<BaseResponse> Handle(GetAllFlightsQuery request, CancellationToken cancellationToken)
    {
        var flights = await _Context.Flights
            .Include(x => x.Origin)
            .Include(x => x.Destination)
            .ToListAsync(cancellationToken);
        var response = flights.MapToFlightResponse();
        return Ok(response);
    }
}