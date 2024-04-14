namespace Application.Response;

public record FlightResponse
{
    public Guid Id { get; init; }
    public CityResponse Origin { get; init; }
    public CityResponse Destination { get; init; }
    public DateTime DepartureTime { get; init; }
    public DateTime ArrivalTime { get; init; }
    public string Status { get; init; }
}