namespace BB.Tools.Collections;

/// <summary>
/// 线程安全的先进先出队列
/// </summary>
/// <typeparam name="T">任意类型的参数</typeparam>
public sealed class Fifo<T>
{
    private Queue<T> _queue = null;
    private int _maxCount = int.MaxValue - 1;
    private AutoResetEvent _writeLock = new AutoResetEvent(true);
    private AutoResetEvent _readLock = new AutoResetEvent(true);
    private object _thisObject = new object();
    private object _objWrite = new object();
    private object _objRead = new object();

    /// <summary>
    /// 默认最大值是int.MaxValue的实例
    /// </summary>
    public Fifo()
    {
        _queue = new Queue<T>();
    }

    /// <summary>
    /// 指定队列大小的构造函数
    /// </summary>
    /// <param name="capacity"></param>
    public Fifo(int capacity)
    {
        _queue = new Queue<T>(capacity);
    }

    /// <summary>
    /// 自定义最大值的实例
    /// </summary>
    /// <param name="maxCount">大于一小于int.MaxValue</param>
    /// <param name="capacity">队列容量</param>
    public Fifo(int maxCount, int capacity)
        : this(capacity)
    {
        if (maxCount > 1 || maxCount < int.MaxValue)
        {
            _maxCount = maxCount;
        }
    }

    /// <summary>
    /// 队列中目前存在个数
    /// </summary>
    public int Count => _queue.Count;

    /// <summary>
    /// 队列的最大容量
    /// </summary>
    public int MaxCount => _maxCount;

    /// <summary>
    /// 重新设置队列的最大容量
    /// </summary>
    /// <param name="maxCount">大于1的整数</param>
    public void ResetMaxCount(int maxCount)
    {
        if (maxCount > 1 || maxCount < int.MaxValue)
        {
            _maxCount = maxCount;
        }
    }

    /// <summary>
    /// 进队,将指定的对象值添加到队列的尾部
    /// </summary>
    /// <param name="obj">T 型的参数</param>
    public void Append(T obj)
    {
        lock (_objWrite)
        {
            while (_queue.Count >= _maxCount)
            {
                _writeLock.WaitOne(Timeout.Infinite, false);
            }
            lock (_thisObject)
            {
                _queue.Enqueue(obj);
                _readLock.Set();
            }
        }
    }

    /// <summary>
    /// 元素出队，即移除队列中开始的元素，
    /// 按先进先出（FIFO）的规则，从前向后移除元素。
    /// </summary>
    /// <returns></returns>
    public T Pop()
    {
        lock (_objRead)
        {
            while (_queue.Count <= 0)
            {
                _readLock.WaitOne(Timeout.Infinite, false);
            }
            lock (_thisObject)
            {
                T obj = _queue.Dequeue();
                _writeLock.Set();
                return obj;
            }
        }
    }
}