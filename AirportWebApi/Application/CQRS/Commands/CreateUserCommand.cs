using Application.DTOs.User;
using Application.Mapper;
using Application.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Commands;

public class CreateUserCommand : IRequest<BaseResponse>
{
    public CreateUserDto Dto;
}

public class CreateUserCommandHandler :  BaseRequest, IRequestHandler<CreateUserCommand, BaseResponse>
{
    public CreateUserCommandHandler(AirportContext context) : base(context)
    {
    }
    
    public async Task<BaseResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var isUserExist = await _Context.Users
            .AnyAsync(x => x.Username == request.Dto.Username, cancellationToken);

        if (isUserExist)
        {
            return BadRequest("User with similar username already exist.");
        }

        var user = request.Dto.MapToUser();
        await _Context.Users.AddAsync(user, cancellationToken);
        await _Context.SaveChangesAsync(cancellationToken);
        var response = user.MapToUserResponse();
        return Created(response);
    }
}