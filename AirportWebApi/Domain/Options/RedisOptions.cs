namespace Domain.Options;

public record RedisOptions
{
    public string ConnectionString { get; init; }
    public string InstanceName { get; init; }
}