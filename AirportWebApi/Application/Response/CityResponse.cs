namespace Application.Response;

public record CityResponse
{
    public Guid Id { get; init; }
    public string CityName { get; init; }
}