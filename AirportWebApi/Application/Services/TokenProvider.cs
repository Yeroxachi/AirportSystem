using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Helpers;
using Application.Interfaces;
using Domain.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class TokenProvider : ITokenProvider
{
    private readonly IOptions<AuthOptions> _authOptions;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly TokenValidationParameters _validationParameters;

    public TokenProvider(IOptions<AuthOptions> authOptions)
    {
        _authOptions = authOptions;
        _tokenHandler = new JwtSecurityTokenHandler();
        var tokenKey = Encoding.UTF8.GetBytes(_authOptions.Value.Key);
        _validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
            ValidateIssuer = true,
            ValidIssuer = _authOptions.Value.Issuer,
            ValidateAudience = true,
            ValidAudience = _authOptions.Value.Audience,
            ClockSkew = TimeSpan.Zero
        };

    }

    public string CreateToken(IList<Claim> claims, TimeSpan lifetime)
    {
        var expirationDate = DateTime.UtcNow.Add(lifetime);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Value.Key));
        var securityToken =  new JwtSecurityToken(
            issuer: _authOptions.Value.Issuer,
            audience: _authOptions.Value.Audience,
            claims: claims,
            expires: expirationDate,
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}