using Domain.Enums;

namespace Domain.Models;

public class Flight : BaseModel
{
    public Guid OriginId { get; set; }
    public City Origin { get; set; }
    public Guid DestinationId { get; set; }
    public City Destination { get; set; }
    public DateTime Arrival { get; set; }
    public DateTime Departure { get; set; }
    public FlightStatus Status { get; set; }
    
    public ICollection<User> Clients { get; set; } = new HashSet<User>();
}