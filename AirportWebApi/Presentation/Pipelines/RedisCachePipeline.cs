using System.Text.Json;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Presentation.Pipelines;

public class RedisCachePipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IDistributedCache _cache;
    private readonly TimeSpan _cacheDuration;

    public RedisCachePipelineBehavior(IDistributedCache cache, TimeSpan cacheDuration)
    {
        _cache = cache;
        _cacheDuration = cacheDuration;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var cacheKey = $"{typeof(TRequest).FullName}:{JsonSerializer.Serialize(request)}";
        
        var cachedResponse = await _cache.GetStringAsync(cacheKey, cancellationToken);
        if (!string.IsNullOrEmpty(cachedResponse))
        {
            return JsonSerializer.Deserialize<TResponse>(cachedResponse);
        }
        
        var response = await next();
        await _cache.SetStringAsync(cacheKey, JsonSerializer.Serialize(response), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _cacheDuration
        }, cancellationToken);

        return response;
    }
}