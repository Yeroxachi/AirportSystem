using System.Security.Cryptography;
using System.Text;
using Domain.Enums;

namespace Application.Helpers;

public static class UserHelper
{
    public static string ComputeSha256Hash(this string rawData)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString(ClaimsConstants.HashFormat));
        }

        return builder.ToString();
    }
    
    public static FlightStatus GetFlightStatus (this string? flightStatus)
    {
        return Enum.TryParse<FlightStatus>(flightStatus, out var result) ? result : default;
    }
}