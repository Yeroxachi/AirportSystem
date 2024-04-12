using System.Security.Claims;

namespace Application.Interfaces;

public interface ITokenProvider
{
    string CreateToken(IList<Claim> claims, TimeSpan lifetime);
}