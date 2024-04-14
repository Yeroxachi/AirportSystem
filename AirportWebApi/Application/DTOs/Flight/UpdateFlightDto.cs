namespace Application.DTOs.Flight;

public record UpdateFlightDto
{
    public Guid FlightId { get; init; }
    public Guid OriginId { get; init; }
    public Guid DestinationId { get; init; }
    public DateTime DepartureTime { get; init; }
    public DateTime ArrivalTime { get; init; }
    public string Status { get; init; }
}