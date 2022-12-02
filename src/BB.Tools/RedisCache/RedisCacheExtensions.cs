using Furion;
using Microsoft.Extensions.DependencyInjection;
using NewLife.Caching;

namespace BB.Tools.RedisCache;

public static class RedisCacheExtensions
{
    /// <summary>
    /// Redis 缓存注册
    /// </summary>
    /// <param name="services"></param>
    public static void AddRedisCache(this IServiceCollection services)
    {
        services.AddSingleton(options =>
        {
            var cacheOptions = App.GetOptions<RedisCacheOptions>();
            if (cacheOptions.CacheType == "R")
            {
                var redis = new FullRedis();
                redis.Init(cacheOptions.RedisConnectionString);
                return redis;
            }

            return NewLife.Caching.Cache.Default;
        });
    }
}