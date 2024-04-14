namespace Domain.Models;

public class City : BaseModel
{
    public string Name{ get; set; }
    
    public IReadOnlyCollection<Flight> OriginFlights { get; init; } = new HashSet<Flight>();
    public IReadOnlyCollection<Flight> DestinationFlights { get; init; } = new HashSet<Flight>();
}