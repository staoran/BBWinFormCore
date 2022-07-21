﻿using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace BB.Tools.Extension;

/// <summary>
/// 分布式缓存扩展
/// </summary>
public static class DistributedCacheExtensions
{
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
    public static async Task SetAsync<T>(this IDistributedCache cache, string key, T value, TimeSpan? expireTimeSpan = null,
        TimeSpan? absoluteTimeSpan = null, DateTimeOffset? absoluteExpiry = null)
    {
        await SetAsync(cache, key, value, CreateExpirationConfig(expireTimeSpan, absoluteTimeSpan, absoluteExpiry));
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
        var json = JsonSerializer.Serialize(value);
        await cache.SetStringAsync(key, json, options);
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
        var value = await cache.GetStringAsync(key);

        if (value is null)
            return default;

        try
        {
            var deserializedValue = JsonSerializer.Deserialize<T>(value);
            return deserializedValue;
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
    public static async Task<T?> GetOrCreateAsync<T>(this IDistributedCache cache, string key, Func<Task<T>> factory,
        TimeSpan? expireTimeSpan = null, TimeSpan? absoluteTimeSpan = null, DateTimeOffset? absoluteExpiry = null)
    {
        return await cache.GetOrCreateAsync(key, factory,
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