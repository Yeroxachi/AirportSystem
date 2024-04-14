using Domain.Enums;

namespace Domain.Models;

public class User : BaseModel
{
    public string Username { get; init; }
    public string PasswordHash { get; init; }
    public UserRole UserRole { get; set; }
    
    public ICollection<Flight> Flights { get; set; } = new HashSet<Flight>();
}