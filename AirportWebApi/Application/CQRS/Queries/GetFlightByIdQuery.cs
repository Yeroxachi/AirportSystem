using Application.Mapper;
using Application.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Queries;

public class GetFlightByIdQuery : IRequest<BaseResponse>
{
    public Guid FlightId { get; init; }
}

public class GetFlightByIdQueryHandler : BaseRequest, IRequestHandler<GetFlightByIdQuery, BaseResponse>
{
    public GetFlightByIdQueryHandler(AirportContext context) : base(context)
    {
    }

    public async Task<BaseResponse> Handle(GetFlightByIdQuery request, CancellationToken cancellationToken)
    {
        var flight = await _Context.Flights
            .Include(x => x.Origin)
            .Include(x => x.Departure)
            .FirstOrDefaultAsync(x => x.Id == request.FlightId, cancellationToken);
        if (flight == null)
        {
            return NotFound($"Flight with Id {request.FlightId} was not found");
        }

        var response = flight.MapToFlightResponse();
        return Ok(response);
    }
}
