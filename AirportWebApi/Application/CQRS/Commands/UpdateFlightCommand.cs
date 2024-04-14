using Application.DTOs.Flight;
using Application.Mapper;
using Application.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Commands;

public class UpdateFlightCommand : IRequest<BaseResponse>
{
    public UpdateFlightDto Dto { get; init; }
}

public class UpdateFlightCommandHandler : BaseRequest, IRequestHandler<UpdateFlightCommand, BaseResponse>
{
    public UpdateFlightCommandHandler(AirportContext context) : base(context)
    {
    }

    public async Task<BaseResponse> Handle(UpdateFlightCommand request, CancellationToken cancellationToken)
    {
        var flight = await _Context.Flights.FirstOrDefaultAsync(x => x.Id == request.Dto.FlightId, cancellationToken);
        if (flight is null)
        {
            return NotFound($"Flight whit Id: \"{request.Dto.FlightId}\" was not found.");
        }

        var origin = await _Context.Cities.FirstOrDefaultAsync(x => x.Id == request.Dto.OriginId, cancellationToken);
        if (origin is null)
        {
            return NotFound($"Origin with id {request.Dto.OriginId} was not found");
        }
        
        var destination = await _Context.Cities.FirstOrDefaultAsync(x => x.Id == request.Dto.DestinationId, cancellationToken);
        if (destination is null)
        {
            return NotFound($"Destination with id {request.Dto.DestinationId} was not found");
        }
        
        flight.Update(request.Dto);
        _Context.Flights.Update(flight);
        await _Context.SaveChangesAsync(cancellationToken);
        flight.Origin = origin;
        flight.Destination = destination;
        var response = flight.MapToFlightResponse();
        return Ok(response);
    }
}