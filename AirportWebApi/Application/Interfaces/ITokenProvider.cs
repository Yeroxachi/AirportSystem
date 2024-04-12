using System.Security.Claims;

namespace Application.Interfaces;

public interface ITokenProvider
{
    string CreateToken(IEnumerable<Claim> claims, TimeSpan lifetime);
}