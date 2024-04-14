using Application.DTOs.City;
using Application.Response;
using Domain.Models;

namespace Application.Mapper;

public static class CityMapper
{
    public static City MapToCity(this CreateCityDto dto)
    {
        return new City
        {
            Id = Guid.NewGuid(),
            Name = dto.CityName
        };
    }

    public static CityResponse MapToCityResponse(this City city)
    {
        return new CityResponse
        {
            Id = city.Id,
            CityName = city.Name
        };
    }

    public static IEnumerable<CityResponse> MapToCityResponse(this IEnumerable<City> cities)
    {
        return cities.Select(x => x.MapToCityResponse());
    }
}