using Furion.ConfigurableOptions;

namespace BB.Tools.RedisCache;

/// <summary>
/// Redis 缓存配置选项
/// </summary>
public class RedisCacheOptions : IConfigurableOptions
{
    /// <summary>
    /// 缓存类型，M 内存缓存，R Redis缓存
    /// </summary>
    public string CacheType { get; set; }

    /// <summary>
    /// Redis连接字符串
    /// </summary>
    public string RedisConnectionString { get; set; }
}