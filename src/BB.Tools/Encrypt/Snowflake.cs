namespace BB.Tools.Encrypt;

/// <summary>
/// 雪花ID，用于分布式系统产生唯一ID
/// </summary>
public class Snowflake
{
    /// <summary>
    /// 机器ID
    /// </summary>
    private static long _machineId;//机器ID
    private static long _datacenterId = 0L;//数据ID
    private static long _sequence = 0L;//计数从零开始

    private static long _twepoch = 687888001020L; //唯一时间随机量

    private static long machineIdBits = 5L; //机器码字节数
    private static long datacenterIdBits = 5L;//数据字节数
    /// <summary>
    /// 最大机器ID
    /// </summary>
    public static long MaxMachineId = -1L ^ -1L << (int)machineIdBits; //最大机器ID
    private static long _maxDatacenterId = -1L ^ (-1L << (int)datacenterIdBits);//最大数据ID

    private static long sequenceBits = 12L; //计数器字节数，12个字节用来保存计数码        
    private static long _machineIdShift = sequenceBits; //机器码数据左移位数，就是后面计数器占用的位数
    private static long _datacenterIdShift = sequenceBits + machineIdBits;
    private static long _timestampLeftShift = sequenceBits + machineIdBits + datacenterIdBits; //时间戳左移动位数就是机器码+计数器总字节数+数据字节数
    /// <summary>
    /// 一微秒内可以产生计数，如果达到该值则等到下一微妙在进行生成
    /// </summary>
    public static long SequenceMask = -1L ^ -1L << (int)sequenceBits; //一微秒内可以产生计数，如果达到该值则等到下一微妙在进行生成
    private static long _lastTimestamp = -1L;//最后时间戳

    private static readonly object SyncRoot = new();//加锁对象
    private static Snowflake _snowflake;

    public static Snowflake Instance()
    {
        return _snowflake ??= new Snowflake();
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    public Snowflake()
    {
        Snowflakes(0L, -1);
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="machineId">机器ID</param>
    public Snowflake(long machineId)
    {
        Snowflakes(machineId, -1);
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="machineId">机器ID</param>
    /// <param name="datacenterId">数据ID</param>
    public Snowflake(long machineId, long datacenterId)
    {
        Snowflakes(machineId, datacenterId);
    }

    /// <summary>
    /// 配置初始信息
    /// </summary>
    /// <param name="machineId">机器ID</param>
    /// <param name="datacenterId">数据ID</param>
    /// <exception cref="Exception"></exception>
    private void Snowflakes(long machineId, long datacenterId)
    {
        if (machineId >= 0)
        {
            if (machineId > MaxMachineId)
            {
                throw new Exception("机器码ID非法");
            }
            _machineId = machineId;
        }
        if (datacenterId >= 0)
        {
            if (datacenterId > _maxDatacenterId)
            {
                throw new Exception("数据中心ID非法");
            }
            _datacenterId = datacenterId;
        }
    }

    /// <summary>
    /// 生成当前时间戳
    /// </summary>
    /// <returns>毫秒</returns>
    private static long GetTimestamp()
    {
        // 2000年开始
        return (long)(DateTime.UtcNow - new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
    }

    /// <summary>
    /// 获取下一微秒时间戳
    /// </summary>
    /// <param name="lastTimestamp"></param>
    /// <returns></returns>
    private static long GetNextTimestamp(long lastTimestamp)
    {
        long timestamp = GetTimestamp();
        int count = 0;
        while (timestamp <= lastTimestamp)//这里获取新的时间,可能会有错,这算法与comb一样对机器时间的要求很严格
        {
            count++;
            if (count > 10)
                throw new Exception("机器的时间可能不对");
            Thread.Sleep(1);
            timestamp = GetTimestamp();
        }
        return timestamp;
    }

    /// <summary>
    /// 获取ID（Int64）
    /// </summary>
    /// <returns></returns>
    public long GetId()
    {
        lock (SyncRoot)
        {
            long timestamp = GetTimestamp();
            if (_lastTimestamp == timestamp)
            { //同一微妙中生成ID
                _sequence = (_sequence + 1) & SequenceMask; //用&运算计算该微秒内产生的计数是否已经到达上限
                if (_sequence == 0)
                {
                    //一微妙内产生的ID计数已达上限，等待下一微妙
                    timestamp = GetNextTimestamp(_lastTimestamp);
                }
            }
            else
            {
                //不同微秒生成ID
                _sequence = 0L;
            }
            if (timestamp < _lastTimestamp)
            {
                throw new Exception("时间戳比上一次生成ID时时间戳还小，故异常");
            }
            _lastTimestamp = timestamp; //把当前时间戳保存为最后生成ID的时间戳
            long id = ((timestamp - _twepoch) << (int)_timestampLeftShift)
                      | (_datacenterId << (int)_datacenterIdShift)
                      | (_machineId << (int)_machineIdShift)
                      | _sequence;
            return id;
        }
    }
}