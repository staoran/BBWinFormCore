namespace BB.Tools.Collections;

/// <summary>
/// 有序字典迭代器对象。
/// 一个有序的字典是一个集合类，它里面的项目可以通过其索引或他们的键进行操作。
/// 有序集合对象，特点是查找非常高效率，尽量减少插入或者删除集合对象。
/// </summary>
/// <typeparam name="TKey">字典键类型</typeparam>
/// <typeparam name="TValue">字典值类型</typeparam>
public sealed class OrderedDictionaryEnumerator<TKey, TValue> : 
    IEnumerator<KeyValuePair<TKey, TValue>>, IEnumerator<TValue>
{
    #region 构造、析构函数

    /// <summary>
    /// 初始化一个OrderedDictionaryEnumerator对象实例
    /// </summary>
    /// <param name="list">列表</param>
    internal OrderedDictionaryEnumerator(CList<KeyValuePair<TKey, TValue>> list)
    {
        _index = -1;
        _list = list;
    }

    /// <summary>
    /// 释放非托管资源和执行其他清理操作，在OrderedDictionaryEnumerator对象被回收前。
    /// </summary>
    ~OrderedDictionaryEnumerator()
    {
        Dispose(false);
    }

    #endregion

    #region 字段

    private int _index;

    private CList<KeyValuePair<TKey, TValue>> _list;

    // 跟踪Dispose是否被调用
    private bool _disposed = false;

    #endregion

    #region Methods

    /// <summary>
    /// Dispose(bool disposing) executes in two distinct scenarios.
    /// If disposing equals true, the method has been called directly
    /// or indirectly by a user's code. Managed and unmanaged resources
    /// can be disposed.
    /// If disposing equals false, the method has been called by the 
    /// runtime from inside the finalizer and you should not reference 
    /// other objects. Only unmanaged resources can be disposed.
    /// </summary>
    /// <param name="disposing">if set to <see langword="true"/> [disposing].</param>
    private void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                // 释放托管资源
                if (_list != null)
                {
                    _list = null;
                }
                GC.SuppressFinalize(this);
            }
            _disposed = true;
        }
    }

    #endregion

    #region IEnumerator<KeyValuePair<TKey,TValue>> Members

    /// <summary>
    /// 获取集合中的元素在枚举当前位置。
    /// </summary>
    /// <value></value>
    /// <returns>在当前位置枚举集合中的元素。</returns>
    KeyValuePair<TKey, TValue> IEnumerator<KeyValuePair<TKey, TValue>>.Current => InternalCurrent;

    /// <summary>
    ///获取内部的当前对象
    /// </summary>
    private KeyValuePair<TKey, TValue> InternalCurrent
    {
        get
        {
            if ((_index < 0) || (_index >= _list.Count))
            {
                throw new InvalidOperationException();
            }
            return _list[_index];

        }
    }

    /// <summary>
    /// 获取键
    /// </summary>
    public TKey Key => InternalCurrent.Key;

    /// <summary>
    /// 获取值
    /// </summary>
    public TValue Value => InternalCurrent.Value;

    #endregion

    #region IDisposable Members

    /// <summary>
    /// 释放或重置非托管资源相关的应用程序定义的任务。
    /// </summary>
    public void Dispose() { Dispose(true); }

    #endregion

    #region IEnumerator 成员函数

    /// <summary>
    /// 获取集合中在枚举当前位置的元素。
    /// </summary>
    /// <value></value>
    /// <returns>在当前位置枚举集合中的元素。</returns>
    object System.Collections.IEnumerator.Current => InternalCurrent.Value;

    /// <summary>
    /// 移动到枚举的下一个元素的集合
    /// </summary>
    /// <returns>
    /// 如果枚举数成功地推进到下一个元素，那么返回True，如果列举值已超过集合的结尾则返回false。
    /// </returns>
    /// <exception cref="T:System.InvalidOperationException">枚举创建后集合被修改则抛出异常。</exception>
    public bool MoveNext()
    {
        _index++;
        if (_index >= _list.Count)
        {
            return false;
        }
        return true;

    }

    /// <summary>
    /// 枚举设置为初始位置，这是集合中的第一个元素之前。
    /// </summary>
    /// <exception cref="T:System.InvalidOperationException">枚举创建后集合被修改则抛出异常。 </exception>
    public void Reset()
    {
        _index = -1;
    }

    /// <summary>
    /// 获取集合中在枚举当前位置的元素。
    /// </summary>
    /// <value></value>
    /// <returns>在当前位置枚举集合中的元素。</returns>
    public TValue Current => InternalCurrent.Value;

    #endregion
 
}