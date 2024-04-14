using Application.DTOs.Flight;
using Application.Mapper;
using Application.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Commands;

public class CreateFlightCommand : IRequest<BaseResponse>
{
    public CreateFlightDto Dto;
}

public class CreateFlightCommandHandler : BaseRequest, IRequestHandler<CreateFlightCommand, BaseResponse>
{
    public CreateFlightCommandHandler(AirportContext context) : base(context)
    {
    }

    public async Task<BaseResponse> Handle(CreateFlightCommand request, CancellationToken cancellationToken)
    {
        var origin = await _Context.Cities.FirstOrDefaultAsync(x => x.Id == request.Dto.OriginId, cancellationToken);
        if (origin == null)
        {
            return NotFound($"Origin with ID {request.Dto.OriginId} does not exist");
        }
        
        var destination = await _Context.Cities.FirstOrDefaultAsync(x => x.Id == request.Dto.DestinationId, cancellationToken);
        if (destination == null)
        {
            return NotFound($"Destination with ID {request.Dto.DestinationId} does not exist");
        }
        
        var flight = request.Dto.MapToFlight();
        await _Context.Flights.AddAsync(flight, cancellationToken);
        await _Context.SaveChangesAsync(cancellationToken);
        flight.Origin = origin;
        flight.Destination = destination;
        var response = flight.MapToFlightResponse();
        return Created(response);
    }
}