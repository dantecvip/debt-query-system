using Microsoft.Extensions.Caching.Distributed;
using DebtQuerySystem.Infrastructure.Services;
using Microsoft.Extensions.Options;
using DebtQuerySystem.Infrastructure.Settings;

namespace DebtQuerySystem.Infrastructure.Services;

public class DistributedCacheService(IDistributedCache cache, IOptions<CacheSettings> settings) : IDistributedCacheService
{
    private readonly TimeSpan DefaultExpiry = 
        TimeSpan.FromMinutes(settings.Value.DefaultExpirationMinutes);

    public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
    {
        if (string.IsNullOrWhiteSpace(key)) return;

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry ?? DefaultExpiry
        };

        await cache.SetStringAsync(key, value, options);
    }

    public async Task<string?> GetAsync(string key)
    {
        if (string.IsNullOrWhiteSpace(key)) return null;

        return await cache.GetStringAsync(key);
    }
}