using System.Security.Claims;
using Application.DTOs;
using Application.Helpers;
using Application.Interfaces;
using Application.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Commands;

public class LoginCommand : IRequest<BaseResponse>
{
    public LoginDto Dto;
}

public class LoginCommandHandler : BaseRequest, IRequestHandler<LoginCommand, BaseResponse>
{
    private readonly ITokenProvider _tokenProvider;

    public LoginCommandHandler(AirportContext context, ITokenProvider tokenProvider) : base(context)
    {
        _tokenProvider = tokenProvider;
    }

    public async Task<BaseResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _Context.Users.FirstOrDefaultAsync(x => x.Username == request.Dto.Username, cancellationToken);
        if (user == null)
        {
            return NotFound("Invalid username or password.");
        }
        
        if (user.PasswordHash != request.Dto.Password.ComputeSha256Hash())
        {
            return BadRequest("Invalid username or password.");
        }
        
        var claims = new List<Claim>
        {
            new(ClaimsConstants.UserIdClaimName, user.Id.ToString()),
            new (ClaimsConstants.UserRoleClaimName, user.UserRole.ToString())
        };
        var accessToken = _tokenProvider.CreateToken(claims, TimeSpan.FromMinutes(AuthConstants.AccessTokenLifeTime));
        var response = new TokenResponse { Token = accessToken};
        return Ok(response);
    }
}