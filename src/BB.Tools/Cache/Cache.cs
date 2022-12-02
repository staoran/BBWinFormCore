using System.Collections.Concurrent;
using BB.Tools.Extension;
using Furion;
using Microsoft.Extensions.Caching.Distributed;

namespace BB.Tools.Cache;

/// <summary>
/// 全局统一的缓存类
/// </summary>
public class Cache
{
    #region 属性

    /// <summary>
    /// MemoryCacheCache
    /// </summary>
    private static IDistributedCache MC => Instance;

    private static volatile IDistributedCache? _instance;
    private static readonly object Locker = new();

    #endregion

    #region 单键值

    #region Set

    /// <summary>
    /// 向缓存中写入一个对象。
    /// </summary>
    /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
    /// <param name="value">需要缓存的对象。</param>
    /// <param name="expireTimeSpan">滑动过期时长</param>
    /// <param name="absoluteTimeSpan">绝对过期时长</param>
    /// <param name="absoluteExpiry">绝对过期时间，absoluteTimeSpan 和 absoluteExpiry 都不为空时以 absoluteExpiry 为主</param>
    public async Task SetAsync(string key, object value, TimeSpan? expireTimeSpan = null, TimeSpan? absoluteTimeSpan = null,
        DateTimeOffset? absoluteExpiry = null)
    {
        await SetAsync<object>(key, value, expireTimeSpan, absoluteTimeSpan, absoluteExpiry);
    }

    /// <summary>
    /// 向缓存中写入字符串。
    /// </summary>
    /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
    /// <param name="value">需要缓存的对象。</param>
    /// <param name="expireTimeSpan">滑动过期时长</param>
    /// <param name="absoluteTimeSpan">绝对过期时长</param>
    /// <param name="absoluteExpiry">绝对过期时间，absoluteTimeSpan 和 absoluteExpiry 都不为空时以 absoluteExpiry 为主</param>
    public async Task SetStringAsync(string key, string value, TimeSpan? expireTimeSpan = null, TimeSpan? absoluteTimeSpan = null,
        DateTimeOffset? absoluteExpiry = null)
    {
        await MC.SetAsync(key, value, expireTimeSpan, absoluteTimeSpan, absoluteExpiry);
    }

    /// <summary>
    /// 向缓存中写入一个对象。
    /// </summary>
    /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
    /// <param name="value">需要缓存的对象。</param>
    /// <param name="expireTimeSpan">滑动过期时长</param>
    /// <param name="absoluteTimeSpan">绝对过期时长</param>
    /// <param name="absoluteExpiry">绝对过期时间，absoluteTimeSpan 和 absoluteExpiry 都不为空时以 absoluteExpiry 为主</param>
    public async Task SetAsync<T>(string key, T value, TimeSpan? expireTimeSpan = null, TimeSpan? absoluteTimeSpan = null,
        DateTimeOffset? absoluteExpiry = null)
    {
        await MC.SetAsync(key, value, expireTimeSpan, absoluteTimeSpan, absoluteExpiry);
    }

    #endregion

    #region Get

    /// <summary>
    /// 从缓存中读取对象。
    /// </summary>
    /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
    /// <returns>被缓存的对象。</returns>
    public async Task<T?> GetAsync<T>(string key)
    {
        return await MC.GetAsync<T>(key);
    }

    /// <summary>
    /// 从缓存中读取对象。
    /// </summary>
    /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
    /// <returns>被缓存的对象。</returns>
    public async Task<string> GetStringAsync(string key)
    {
        return await MC.GetStringAsync(key);
    }

    #endregion

    #region Other

    /// <summary>
    /// 从缓存中移除对象。
    /// </summary>
    /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
    public async Task RemoveAsync(string key)
    {
        await MC.RemoveAsync(key);
    }

    /// <summary>
    /// 获取一个值，该值表示拥有指定键值的缓存是否存在。
    /// </summary>
    /// <param name="key">指定的键值。</param>
    /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
    public async Task<bool>ExistsAsync(string key)
    {
        return await MC.ExistsAsync(key);
    }

    /// <summary>
    /// 清空所有缓存
    /// </summary>
    public async Task FlushAll()
    {
        await MC.FlushAllAsync();
    }

    #endregion

    #endregion

    #region 双键值

    #region Set

    /// <summary>
    /// 向缓存中添加一个对象。
    /// </summary>
    /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
    /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
    /// <param name="value">需要缓存的对象。</param>
    /// <param name="expireTimeSpan">滑动过期时长</param>
    /// <param name="absoluteTimeSpan">绝对过期时长</param>
    /// <param name="absoluteExpiry">绝对过期时间，absoluteTimeSpan 和 absoluteExpiry 都不为空时以 absoluteExpiry 为主</param>
    public async Task SetAsync(string key, string valKey, object value, TimeSpan? expireTimeSpan = null, TimeSpan? absoluteTimeSpan = null,
        DateTimeOffset? absoluteExpiry = null)
    {
        await SetAsync<object>(key, valKey, value, expireTimeSpan, absoluteTimeSpan, absoluteExpiry);
    }

    /// <summary>
    /// 向缓存中添加一个对象。
    /// </summary>
    /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
    /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
    /// <param name="value">需要缓存的对象。</param>
    /// <param name="expireTimeSpan">滑动过期时长</param>
    /// <param name="absoluteTimeSpan">绝对过期时长</param>
    /// <param name="absoluteExpiry">绝对过期时间，absoluteTimeSpan 和 absoluteExpiry 都不为空时以 absoluteExpiry 为主</param>
    public async Task SetAsync<T>(string key, string valKey, T value, TimeSpan? expireTimeSpan = null, TimeSpan? absoluteTimeSpan = null,
        DateTimeOffset? absoluteExpiry = null)
    {
        ConcurrentDictionary<string, T>? dict = await GetAsync<ConcurrentDictionary<string, T>>(key);
        
        if (dict != null)
        {
            dict[valKey] = value;
        }
        else
        {
            dict = new ConcurrentDictionary<string, T>();
            dict.AddOrUpdate(valKey, value);
        }

        await SetAsync(key, dict, expireTimeSpan, absoluteTimeSpan, absoluteExpiry);
    }

    #endregion

    #region Get

    /// <summary>
    /// 从缓存中读取对象。
    /// </summary>
    /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
    /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
    /// <returns>被缓存的对象。</returns>
    public async Task<T?> GetAsync<T>(string key, string valKey)
    {
        ConcurrentDictionary<string, T>? dict = await GetAsync<ConcurrentDictionary<string, T>>(key);

        T? t = default(T);
        if (dict != null)
        {
            if (dict.ContainsKey(valKey))
                t = dict[valKey];
        }
        return t;
    }

    /// <summary>
    /// 从缓存中读取对象。
    /// </summary>
    /// <param name="key">缓存的键值，该值通常是使用缓存机制的方法的名称。</param>
    /// <param name="valKey">缓存值的键值，该值通常是由使用缓存机制的方法的参数值所产生。</param>
    /// <returns>被缓存的对象。</returns>
    public async Task<object?> GetAsync(string key, string valKey)
    {
        ConcurrentDictionary<string, object>? dict = await GetAsync<ConcurrentDictionary<string, object>>(key);

        object? obj = null;
        if (dict != null)
        {
            if (dict.ContainsKey(valKey))
                obj = dict[valKey];
        }
        return obj;
    }

    #endregion

    #region Other

    /// <summary>
    /// 获取一个值，该值表示拥有指定键值和缓存值键的缓存是否存在。
    /// </summary>
    /// <param name="key">指定的键值。</param>
    /// <param name="valKey">缓存值键。</param>
    /// <returns>如果缓存存在，则返回true，否则返回false。</returns>
    public async Task<bool> ExistsAsync(string key, string valKey)
    {
        ConcurrentDictionary<string, object>? dict = await GetAsync<ConcurrentDictionary<string, object>>(key);

        return dict != null && dict.ContainsKey(valKey);
    }

    #endregion

    #endregion

    /// <summary>
    /// 单件实例
    /// </summary>
    public static IDistributedCache Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (Locker)
                {
                    if (_instance == null)
                    {
                        _instance = App.GetService<IDistributedCache>();
                    }
                }
            }
            return _instance;
        }
    }
}