namespace DebtQuerySystem.Infrastructure.Services;

public interface IDistributedCacheService
{
    Task SetAsync(string key, string value, TimeSpan? expiry = null);
    Task<string?> GetAsync(string key);
}
