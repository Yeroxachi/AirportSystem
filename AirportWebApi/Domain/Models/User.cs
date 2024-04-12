using Domain.Enums;

namespace Domain.Models;

public class User : BaseModel
{
    public string Username { get; init; }
    public string PasswordHash { get; init; }
    public UserRole UserRole { get; set; }
    
    public IReadOnlyCollection<Flight> Flights { get; init; } = new HashSet<Flight>();
}