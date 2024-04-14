using Application.DTOs.City;
using Application.Mapper;
using Application.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Application.CQRS.Commands;

public class UpdateCityCommand : IRequest<BaseResponse>
{
    public UpdateCityDto Dto { get; init; }
}

public class UpdateCityCommandHandler : BaseRequest, IRequestHandler<UpdateCityCommand, BaseResponse>
{
    public UpdateCityCommandHandler(AirportContext context) : base(context)
    {
    }

    public async Task<BaseResponse> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
    {
        var city = await _Context.Cities.FirstOrDefaultAsync(x => x.Id == request.Dto.CityId, cancellationToken);
        if (city is null)
        {
            return NotFound($"City with Id {request.Dto.CityId} was not found");
        }
        
        city.Name = request.Dto.Name;
        _Context.Update(city);
        await _Context.SaveChangesAsync(cancellationToken);
        var response = city.MapToCityResponse();
        return Ok(response);
    }
}