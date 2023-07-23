using System.Text;
using Market.Application.Common.Cache;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Market.Infrastructure.Configurations.Cache;
public class ReposeCache : IReposeCache
{
    private readonly IDistributedCache distributedCache;
    private readonly IConnectionMultiplexer connectionMultiplexer;

    public ReposeCache(IDistributedCache distributedCache, IConnectionMultiplexer connectionMultiplexer)
    {
        this.distributedCache = distributedCache;
        this.connectionMultiplexer = connectionMultiplexer;
    }

    public async Task<string> GetCacheReponseAsync(string cacheKey)
    {
        // Lấy ra cái Catche có Key = cacheKey
        string cacheRespone = await distributedCache.GetStringAsync(cacheKey);
        return string.IsNullOrEmpty(cacheRespone) ? null : cacheRespone;
    }

    public async Task<List<string>> GetCacheReponseByPatternAsync(string pattern)
    {
        List<string> readByCatcheRedis = new();
        if (string.IsNullOrWhiteSpace(pattern) || pattern.Equals("_")) {
            throw new AggregateException("Dữ liệu không thể null hoặc khoảng trắng");
        }
        // Lấy ra cái Catche có Key = cacheKey

        foreach (var key in this.GetKeyAsync(pattern + "*")) {
            var cacheResponeByte = await distributedCache.GetAsync(key);

            string cacheRespone = Encoding.UTF8.GetString(cacheResponeByte);
            if (!string.IsNullOrEmpty(cacheRespone)) {
                readByCatcheRedis.Add(cacheRespone);
            }
        }
        return readByCatcheRedis;
    }

    public async Task RemoveCacheAsync(string pattern, Guid Id)
    {
        if (string.IsNullOrWhiteSpace(pattern))
            throw new AggregateException("Dữ liệu không thể null hoặc khoảng trắng");
        await distributedCache.RemoveAsync(pattern + Id);
    }

    public async Task RemoveCacheByPatternAsync(string pattern)
    {
        if (string.IsNullOrWhiteSpace(pattern)) 
            throw new AggregateException("Dữ liệu không thể null hoặc khoảng trắng");
        foreach (var key in this.GetKeyAsync(pattern + "*")) {
            await distributedCache.RemoveAsync(key); // Xóa key bắt đầu bằng pattern    
        }
    }

    public async Task SetCacheReponseAsync(string cacheKey, object response, TimeSpan timeOut)
    {
        if (response == null) return;
        var converReponseToString = JsonConvert.SerializeObject(response);
        var redisUTF8 = Encoding.UTF8.GetBytes(converReponseToString);

        await distributedCache.SetAsync(cacheKey, redisUTF8,
            new DistributedCacheEntryOptions {
                AbsoluteExpirationRelativeToNow = timeOut
            }
        );
    }

    public async Task UpdateDataCacheAsync(string cacheKey, object response)
    {
        if (response == null) return;
        var converReponseToString = JsonConvert.SerializeObject(response);
        var redisUTF8 = Encoding.UTF8.GetBytes(converReponseToString);

        await distributedCache.SetAsync(cacheKey, redisUTF8);
    }

    private IEnumerable<string> GetKeyAsync(string pattern)
    {
        if (string.IsNullOrWhiteSpace(pattern)) {
            throw new AggregateException("Dữ liệu không thể null hoặc khoảng trắng");
        }

        foreach (var endPoint in connectionMultiplexer.GetEndPoints()) {
            var server = connectionMultiplexer.GetServer(endPoint);

            foreach (var key in server.Keys(pattern: pattern)) {
                yield return key.ToString();
            }
        }
    }
}
