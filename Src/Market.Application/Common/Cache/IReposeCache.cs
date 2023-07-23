namespace Market.Application.Common.Cache;

public interface IReposeCache
{
    Task SetCacheReponseAsync(string cacheKey, object response, TimeSpan timeOut);
    Task UpdateDataCacheAsync(string cacheKey, object response);
    Task<string> GetCacheReponseAsync(string cacheKey);
    Task<List<string>> GetCacheReponseByPatternAsync(string pattern);
    Task RemoveCacheByPatternAsync(string pattern);
    Task RemoveCacheAsync(string pattern, Guid Id);
}
