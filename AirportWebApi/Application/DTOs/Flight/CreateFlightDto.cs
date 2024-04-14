namespace Application.DTOs.Flight;

public record CreateFlightDto
{
    public Guid OriginId { get; init; }
    public Guid DestinationId { get; init; }
    public DateTime DepartureTime { get; init; }
    public DateTime ArrivalTime { get; init; }
}