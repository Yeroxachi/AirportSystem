using Application.Response;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Commands;

public class RegistrationToFlightCommand : IRequest<BaseResponse>
{
    public Guid FlightId { get; init; }
    public Guid UserId { get; init; }
}

public class RegistrationToFlightCommandHandler : BaseRequest, IRequestHandler<RegistrationToFlightCommand, BaseResponse>
{
    public RegistrationToFlightCommandHandler(AirportContext context) : base(context)
    {
    }

    public async Task<BaseResponse> Handle(RegistrationToFlightCommand request, CancellationToken cancellationToken)
    {
        var user = await _Context.Users.FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if (user is null)
        {
            return UnAuthorized();
        }
        
        var flight = await _Context.Flights
            .Include(x => x.Clients)
            .FirstOrDefaultAsync(x => x.Id == request.FlightId, cancellationToken);
        if (flight is null)
        {
            return NotFound($"Flight with Id {request.FlightId} was not found");
        }

        if (flight.Clients.Any(x => x.Id == request.UserId))
        {
            return BadRequest($"User {request.UserId} is already registered to flight {request.FlightId}");
        }

        if (flight.Status != FlightStatus.RegistrationOpen)
        {
            return BadRequest($"Flight with Id {request.FlightId} is not registration open");
        }
        
        flight.Clients.Add(user);
        await _Context.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}