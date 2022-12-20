using System.Collections.Concurrent;
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace BB.Tools.Extension;

/// <summary>
/// 分布式缓存扩展
/// </summary>
public static class DistributedCacheExtensions
{
    private static readonly ConcurrentBag<string> AllKeys = new();
    
    #region 异步方法

    /// <summary>
    /// 写缓存
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="key">缓存名称</param>
    /// <param name="value">缓存对象</param>
    /// <param name="expireTimeSpan">滑动过期时长</param>
    /// <param name="absoluteTimeSpan">绝对过期时长</param>
    /// <param name="absoluteExpiry">绝对过期时间，absoluteTimeSpan 和 absoluteExpiry 都不为空时以 absoluteExpiry 为主</param>
    /// <typeparam name="T"></typeparam>
    public static Task SetAsync<T>(this IDistributedCache cache, string key, T value, TimeSpan? expireTimeSpan = null,
        TimeSpan? absoluteTimeSpan = null, DateTimeOffset? absoluteExpiry = null)
    {
        return SetAsync(cache, key, value, CreateExpirationConfig(expireTimeSpan, absoluteTimeSpan, absoluteExpiry));
    }

    /// <summary>
    /// 写缓存
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="key">缓存名称</param>
    /// <param name="value">缓存对象</param>
    /// <param name="options">缓存配置</param>
    /// <typeparam name="T"></typeparam>
    public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value,
        DistributedCacheEntryOptions options)
    {
        using var memoryStream = new MemoryStream();
        await JsonSerializer.SerializeAsync(memoryStream, value);
        await cache.SetAsync(key, memoryStream.ToArray(), options);
        if (!AllKeys.Contains(key))
        {
            AllKeys.Add(key);
        }
    }

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="key">缓存名称</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<T?> GetAsync<T>(this IDistributedCache cache, string key)
    {
        var value = await cache.GetAsync(key);

        if (value is null)
            return default;

        try
        {
            using var memoryStream = new MemoryStream(value);
            return await JsonSerializer.DeserializeAsync<T>(memoryStream);
        }
        catch
        {
            return default;
        }
    }

    /// <summary>
    /// 获取或新增缓存
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="key">缓存名称</param>
    /// <param name="factory">缓存对象委托</param>
    /// <param name="expireTimeSpan">滑动过期时长</param>
    /// <param name="absoluteTimeSpan">绝对过期时长</param>
    /// <param name="absoluteExpiry">绝对过期时间，absoluteTimeSpan 和 absoluteExpiry 都不为空时以 absoluteExpiry 为主</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static Task<T?> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> factory,
        TimeSpan? expireTimeSpan = null, TimeSpan? absoluteTimeSpan = null, DateTimeOffset? absoluteExpiry = null)
    {
        return cache.GetOrCreateAsync(key, factory,
            CreateExpirationConfig(expireTimeSpan, absoluteTimeSpan, absoluteExpiry));
    }

    /// <summary>
    /// 获取或新增缓存
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="key">缓存名称</param>
    /// <param name="factory">缓存对象委托</param>
    /// <param name="options">缓存配置</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<T?> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> factory,
        DistributedCacheEntryOptions options)
    {
        var value = await cache.GetAsync<T>(key);
        if (value is not null)
            return value;

        value = await factory();

        if (value is null)
            return value;

        await cache.SetAsync(key, value, options);

        return value;
    }

    /// <summary>
    /// 获取一个值，该值表示拥有指定键值的缓存是否存在。
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="key">指定的键值。</param>
    /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
    public static async Task<bool>ExistsAsync(this IDistributedCache cache, string key)
    {
        return await cache.GetAsync(key) != null;
    }

    /// <summary>
    /// 清空所有缓存
    /// </summary>
    public static async Task FlushAllAsync(this IDistributedCache cache)
    {
        foreach (var item in AllKeys)
            await cache.RemoveAsync(item);
        AllKeys.Clear();
    }

    #endregion

    #region 同步方法

    /// <summary>
    /// 写缓存
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="key">缓存名称</param>
    /// <param name="value">缓存对象</param>
    /// <param name="expireTimeSpan">滑动过期时长</param>
    /// <param name="absoluteTimeSpan">绝对过期时长</param>
    /// <param name="absoluteExpiry">绝对过期时间，absoluteTimeSpan 和 absoluteExpiry 都不为空时以 absoluteExpiry 为主</param>
    /// <typeparam name="T"></typeparam>
    public static void Set<T>(this IDistributedCache cache, string key, T value, TimeSpan? expireTimeSpan = null,
        TimeSpan? absoluteTimeSpan = null, DateTimeOffset? absoluteExpiry = null)
    {
        Set(cache, key, value, CreateExpirationConfig(expireTimeSpan, absoluteTimeSpan, absoluteExpiry));
    }

    /// <summary>
    /// 写缓存
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="key">缓存名称</param>
    /// <param name="value">缓存对象</param>
    /// <param name="options">缓存配置</param>
    /// <typeparam name="T"></typeparam>
    public static void Set<T>(this IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options)
    {
        using var memoryStream = new MemoryStream();
        JsonSerializer.Serialize(memoryStream, value);
        cache.Set(key, memoryStream.ToArray(), options);
        if (!AllKeys.Contains(key))
        {
            AllKeys.Add(key);
        }
    }

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="key">缓存名称</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? Get<T>(this IDistributedCache cache, string key)
    {
        var value = cache.Get(key);

        if (value is null)
            return default;

        try
        {
            using var memoryStream = new MemoryStream(value);
            return JsonSerializer.Deserialize<T>(memoryStream);
        }
        catch
        {
            return default;
        }
    }

    /// <summary>
    /// 获取或新增缓存
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="key">缓存名称</param>
    /// <param name="factory">缓存对象委托</param>
    /// <param name="expireTimeSpan">滑动过期时长</param>
    /// <param name="absoluteTimeSpan">绝对过期时长</param>
    /// <param name="absoluteExpiry">绝对过期时间，absoluteTimeSpan 和 absoluteExpiry 都不为空时以 absoluteExpiry 为主</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? GetOrCreate<T>(this IDistributedCache cache, string key, Func<T> factory,
        TimeSpan? expireTimeSpan = null, TimeSpan? absoluteTimeSpan = null, DateTimeOffset? absoluteExpiry = null)
    {
        return cache.GetOrCreate(key, factory, CreateExpirationConfig(expireTimeSpan, absoluteTimeSpan, absoluteExpiry));
    }

    /// <summary>
    /// 获取或新增缓存
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="key">缓存名称</param>
    /// <param name="factory">缓存对象委托</param>
    /// <param name="options">缓存配置</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static T? GetOrCreate<T>(this IDistributedCache cache, string key, Func<T> factory,
        DistributedCacheEntryOptions options)
    {
        var value = cache.Get<T>(key);
        if (value is not null)
            return value;

        value = factory();

        if (value is null)
            return value;

        cache.Set(key, value, options);

        return value;
    }

    /// <summary>
    /// 获取一个值，该值表示拥有指定键值的缓存是否存在。
    /// </summary>
    /// <param name="cache">分布式缓存</param>
    /// <param name="key">指定的键值。</param>
    /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
    public static bool Exists(this IDistributedCache cache, string key)
    {
        return cache.Get(key) != null;
    }

    /// <summary>
    /// 清空所有缓存
    /// </summary>
    public static void FlushAll(this IDistributedCache cache)
    {
        foreach (var item in AllKeys)
            cache.Remove(item);
        AllKeys.Clear();
    }

    #endregion

    /// <summary>
    /// 创建缓存过期配置
    /// </summary>
    /// <param name="slidingExpiration">滑动过期时长</param>
    /// <param name="absoluteTimeSpan">绝对过期时长</param>
    /// <param name="absoluteExpiration">绝对过期时间，absoluteTimeSpan 和 absoluteExpiration 都不为空时以 absoluteExpiration 为主</param>
    /// <returns></returns>
    private static DistributedCacheEntryOptions CreateExpirationConfig(TimeSpan? slidingExpiration = null,
        TimeSpan? absoluteTimeSpan = null, DateTimeOffset? absoluteExpiration = null)
    {
        var options = new DistributedCacheEntryOptions();
	 
        if(absoluteExpiration.HasValue)
        {
            options.AbsoluteExpiration = absoluteExpiration.Value;
        }
        else
        {
            if (absoluteTimeSpan.HasValue)
            {
                options.AbsoluteExpirationRelativeToNow = absoluteTimeSpan.Value;
            }
        }
        
        if(slidingExpiration.HasValue)
        {
            options.SlidingExpiration = slidingExpiration.Value;
        }
	 
        return options;
    }
}