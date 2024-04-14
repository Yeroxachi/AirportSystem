using Application.DTOs.City;
using Application.Mapper;
using Application.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Commands;

public class CreateCityCommand : IRequest<BaseResponse>
{
    public CreateCityDto Dto { get; init; }
}

public class CreateCityCommandHandler : BaseRequest, IRequestHandler<CreateCityCommand, BaseResponse>
{
    public CreateCityCommandHandler(AirportContext context) : base(context)
    {
    }
    
    public async Task<BaseResponse> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var isCityExist = await _Context.Cities.AnyAsync(x => x.Name == request.Dto.CityName, cancellationToken);
        if (isCityExist)
        {
            return BadRequest("City already exists");
        }

        var city = request.Dto.MapToCity();
        await _Context.Cities.AddAsync(city, cancellationToken);
        await _Context.SaveChangesAsync(cancellationToken);
        var response = city.MapToCityResponse();
        return Created(response);
    }
}
