namespace Application.DTOs.City;

public record UpdateCityDto
{
    public Guid CityId { get; init; }
    public string Name { get; init; }
}