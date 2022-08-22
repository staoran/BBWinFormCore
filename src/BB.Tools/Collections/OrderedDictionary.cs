using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace BB.Tools.Collections;

/// <summary>
/// 表示一个有序的字典集合。
/// 一个有序的字典是一个集合类，它里面的项目可以通过其索引或他们的键进行操作。
/// 有序集合对象，特点是查找非常高效率，尽量减少插入或者删除集合对象。
/// 有序的字典集合，虽然存在一些缺点，但是提供了一个非常灵活和用户友好数据结构，允许通过键值或索引进行访问。
/// 并且提供了强类型的数据操作。
/// </summary>
/// <typeparam name="TKey">字典中键的类型。</typeparam>
/// <typeparam name="TValue">字典中值的类型。</typeparam>
[Serializable()]
public class OrderedDictionary<TKey, TValue> : IDictionary<TKey, TValue>, ICloneable,
    ICloneable<OrderedDictionary<TKey, TValue>>, IEnumerable<KeyValuePair<TKey, TValue>>, 
    IEnumerable<TValue>
{
    #region 构造函数

    /// <summary>
    /// 初始化OrderedDictionary类的新实例
    /// </summary>
    /// <summary>初始化OrderedDictionary类的新实例，实例为空并具有默认的初始容量，使用默认的相等比较器等。</summary>
    public OrderedDictionary()
    {
        _dictionary = new CDictionary<TKey, TValue>();
        _list = new CList<KeyValuePair<TKey, TValue>>();
    }

    /// <summary>
    /// 初始化OrderedDictionary类的新实例
    /// </summary>
    /// <param name="dictionary">字典对象</param>
    public OrderedDictionary(IDictionary<TKey, TValue> dictionary)
    {
        _dictionary = new CDictionary<TKey, TValue>(dictionary);
        _list = new CList<KeyValuePair<TKey, TValue>>();
    }
        
    /// <summary>
    /// 初始化OrderedDictionary类的新实例
    /// </summary>
    /// <param name="comparer">比较器对象</param>
    public OrderedDictionary(IEqualityComparer<TKey> comparer)
    {
        _dictionary = new CDictionary<TKey, TValue>(comparer);
        _list = new CList<KeyValuePair<TKey, TValue>>();
    }

    /// <summary>
    /// 初始化OrderedDictionary类的新实例
    /// </summary>
    /// <param name="capacity">字典元素容量</param>
    public OrderedDictionary(int capacity)
    {
        _dictionary = new CDictionary<TKey, TValue>(capacity);
        _list = new CList<KeyValuePair<TKey, TValue>>();
    } 

    /// <summary>
    /// 初始化OrderedDictionary类的新实例
    /// </summary>
    /// <param name="dictionary">字典表.</param>
    /// <param name="comparer">比较器对象</param>
    public OrderedDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
    {
        _dictionary = new CDictionary<TKey, TValue>(dictionary, comparer);
        _list = new CList<KeyValuePair<TKey, TValue>>();
    }

    /// <summary>
    /// 初始化OrderedDictionary类的新实例
    /// </summary>
    /// <param name="capacity">字典元素容量</param>
    /// <param name="comparer">比较器对象</param>
    public OrderedDictionary(int capacity, IEqualityComparer<TKey> comparer)
    {
        _dictionary = new CDictionary<TKey, TValue>(capacity, comparer);
        _list = new CList<KeyValuePair<TKey, TValue>>();
    }

    #endregion

    #region 字段属性

    private CDictionary<TKey, TValue> _dictionary;
    private CList<KeyValuePair<TKey, TValue>> _list;

    /// <summary>获取<see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> 对象，该对象用来比较字典表键的相等</summary>
    /// <returns>对象<see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> 泛型接口，用来判断当前对象<see cref="T:System.Collections.Generic.Dictionary`2"></see>的相等性，
    /// 并提供键的哈希值.</returns>
    public virtual IEqualityComparer<TKey> Comparer => _dictionary.Comparer;

    #endregion

    #region 方法

    /// <summary>
    /// 判断字典<see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> 是否包含指定的值。
    /// </summary>
    /// <param name="value">待判断的值,如果值为引用类型，则可以为null</param>
    /// <returns>
    /// 如果字典包含项目，则返回true，否则为false。
    /// </returns>
    public virtual bool ContainsValue(TValue value)
    {
        return _dictionary.ContainsValue(value);
    }

    #endregion

    #region IDictionary<TKey,TValue> 成员函数

    /// <summary>
    /// 在字典中添加一个元素，根据提供的键和值
    /// </summary>
    /// <param name="key">作为待添加元素的键</param>
    /// <param name="value">作为待添加元素的值.</param>
    /// <exception cref="System.NotSupportedException">字典对象为只读</exception>
    /// <exception cref="System.ArgumentException">含有相同键的元素已经存在字典中</exception>
    /// <exception cref="System.ArgumentNullException">键为空.</exception>
    public virtual void Add(TKey key, TValue value)
    {
        _dictionary.Add(key, value);
        _list.Add(new KeyValuePair<TKey, TValue>(key, value));
    }

    /// <summary>
    /// 判断字典是否包含指定键的元素
    /// </summary>
    /// <param name="key">在字典中查找的键.</param>
    /// <returns>
    /// 如果字典中包含指定键的元素，返回true，否则为false
    /// </returns>
    /// <exception cref="System.ArgumentNullException">键为空</exception>
    public virtual bool ContainsKey(TKey key)
    {
        return _dictionary.ContainsKey(key);
    }

    /// <summary>
    /// 在指定的索引插入元素
    /// </summary>
    /// <param name="index">插入位置，从零开始的索引</param>
    /// <param name="key">作为待插入元素的键</param>
    /// <param name="value">作为待插入元素的值</param>
    /// <exception cref="T:System.NotSupportedException">集合为只读</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">指定索引在<see cref="T:System.Collections.Generic.IList`1"></see>中为无效索引.</exception>
    public void Insert(int index, TKey key, TValue value)
    {
        if ((index < 0) || (index >= Count))
        {
            throw new ArgumentOutOfRangeException("index");
        }

        _dictionary.Add(key, value);
        _list.Insert(index, new KeyValuePair<TKey, TValue>(key, value));
    }

    /// <summary>
    /// 判定是否包含指定的键
    /// </summary>
    /// <param name="key">键对象</param>
    /// <returns>
    /// 如果包含返回true，否则为false
    /// </returns>
    public virtual bool Contains(TKey key)
    {
        return ContainsKey(key);
    }

    /// <summary>
    /// 获取字典对象的键集合
    /// </summary>
    public virtual ICollection<TKey> Keys
    {
        get
        {
            CList<TKey> keys = new CList<TKey>();

            for (int index = 0; index < _list.Count; index++)
            {
                KeyValuePair<TKey, TValue> keyValue = _list[index];
                keys.Add(keyValue.Key);
            }
            return keys;
        }
    }

    /// <summary>
    /// 获取指定键的元素索引
    /// </summary>
    /// <param name="key">待查找的键</param>
    /// <returns>
    /// 如果列表中发现则为项目的索引，否则为-1。
    /// </returns>
    public int IndexOf(TKey key)
    {
        for (int index = 0; index < _list.Count; index++)
        {
            KeyValuePair<TKey, TValue> keyValue = _list[index];
            if (keyValue.Key.Equals(key))
            {
                return index;
            }
        }
        return -1;
    }

    /// <summary>
    /// 获取指定键的元素索引
    /// </summary>
    /// <param name="value">待查找的键</param>
    /// <returns>
    /// 如果列表中发现则为项目的索引，否则为-1。
    /// </returns>
    public int IndexOf(TValue value)
    {
        for (int index = 0; index < _list.Count; index++)
        {
            KeyValuePair<TKey, TValue> keyValue = _list[index];
            if (keyValue.Value.Equals(value))
            {
                return index;
            }
        }
        return -1;
    }        

    /// <summary>
    /// 在集合中移除指定键的元素
    /// </summary>
    /// <param name="key">待移除元素的键</param>
    /// <returns>
    /// 如果元素被移除返回true，否则为false。如果原始集合中没有发现指定键的元素，也返回false。
    /// </returns>
    /// <exception cref="System.NotSupportedException">集合为只读</exception>
    /// <exception cref="System.ArgumentNullException">键为空</exception>
    public virtual bool Remove(TKey key)
    {
        bool flag = _dictionary.Remove(key);
        _list.RemoveAt(IndexOf(key));
        return flag;
    }
    

    /// <summary>
    /// 通过指定索引移除指定的元素
    /// </summary>
    /// <param name="index">从零开始</param>
    /// <exception cref="T:System.NotSupportedException">集合为只读</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException">索引不是集合中的有效索引</exception>
    public void RemoveAt(int index)
    {
        if ((index < 0) || (index >= Count))
        {
            throw new ArgumentOutOfRangeException("index");
        }

        KeyValuePair<TKey, TValue> keyValue = _list[index];
        _dictionary.Remove(keyValue.Key);
        _list.RemoveAt(index);
    }


    /// <summary>
    /// 获取与指定的键相关联的值。
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    public virtual bool TryGetValue(TKey key, out TValue value)
    {
        bool flag = _dictionary.TryGetValue(key, out value);
        return flag;
    }

    /// <summary>
    /// 获取字典中值的对象集合
    /// </summary>
    public virtual ICollection<TValue> Values
    {
        get
        {
            CList<TValue> values = new CList<TValue>();

            for (int index = 0; index < _list.Count; index++)
            {
                KeyValuePair<TKey, TValue> keyValue = _list[index];
                values.Add(keyValue.Value);
            }
            return values;
        }
    }

    /// <summary>
    /// 获取或设置与指定键的值。
    /// </summary>
    public virtual TValue this[TKey key]
    {
        get
        {
            if (_dictionary.ContainsKey(key))
            {
                return _dictionary[key];
            }
            return default(TValue);
        }
        set
        {
            if (_dictionary.ContainsKey(key))
            {
                _dictionary[key] = value;
                _list[IndexOf(key)] = new KeyValuePair<TKey, TValue>(key, value);
            }
            else
            {
                Add(key, value);
            }
        }
    }

    /// <summary>获取或设置指定索引相关联的值</summary>
    /// <returns>返回指定索引中关联的值, 如果指定的索引为无效，获取或设置操作将抛出 <see cref="ArgumentOutOfRangeException"></see>。
    /// </returns>
    /// <param name="index">获取或设置值的索引</param>
    /// <exception cref="ArgumentOutOfRangeException">索引大于集合数量或者小于0</exception>
    public virtual TValue this[int index]
    {
        get
        {
            if ((index < 0) || (index >= Count))
            {
                throw new ArgumentOutOfRangeException("index");
            }

            return _list[index].Value;
        }
        set
        {
            if ((index < 0) || (index >= Count))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            TKey key = _list[index].Key;

            if (_dictionary.ContainsKey(key))
            {
                _dictionary[key] = value;
                _list[index] = new KeyValuePair<TKey, TValue>(key, value);
            }
            else
            {
                Add(key, value);
            }
        }
    }

    /// <summary>实现 <see cref="T:System.Runtime.Serialization.ISerializable"></see> 接口并返回序列化字典对象所需要的数据.</summary>
    /// <param name="info">一个 SerializationInfo 对象，它包含序列化Dictionary所需的信息。</param>
    /// <param name="context"> StreamingContext 结构，该结构包含与 Dictionary相关联的序列化流的源和目标。</param>
    /// <exception cref="System.ArgumentNullException">info为空引用</exception>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        _dictionary.GetObjectData(info, context);
    }

    /// <summary>
    /// 获取一个值，该值判定访问的<see cref="System.Collections.ICollection"></see>对象是否为同步（线程安全）。
    /// </summary>
    /// <returns>如果词典的访问是同步的（线程安全）返回true，否则为false。默认为false。</returns>
    public virtual bool IsSynchronized => _dictionary.IsSynchronized;


    /// <summary>
    /// 返回Hashtable的同步（线程安全）包装类。
    /// </summary>
    /// <param name="dictionary">待同步的字典. </param>
    /// <returns>一个同步（线程安全）的字典包装类 </returns>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static OrderedDictionary<TKey, TValue> Synchronized(OrderedDictionary<TKey, TValue> dictionary)
    {
        if (dictionary == null)
        {
            throw new ArgumentNullException("dictionary");
        }
        return new SyncOrderedDictionary<TKey, TValue>(dictionary);
    }

    /// <summary>实现<see cref="T:System.Runtime.Serialization.ISerializable"></see> 接口并在反序列化完成后抛出反序列化事件。</summary>
    /// <param name="sender">反序列化事件的源对象</param>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">在当前字典中关联的该对象为无效对象.</exception>
    public virtual void OnDeserialization(object sender)
    {
        _dictionary.OnDeserialization(sender);
    }


    #endregion

    #region ICollection<KeyValuePair<TKey,TValue>> 成员函数

    /// <summary>
    /// 添加一个项目
    /// </summary>
    /// <param name="keyValuePair">键值对</param>
    void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair)
    {
        Add(keyValuePair.Key, keyValuePair.Value);
    }

    /// <summary>
    /// 移除所有项目
    /// </summary>
    /// <exception cref="System.NotSupportedException">集合为只读的时候. </exception>
    public virtual void Clear()
    {
        _dictionary.Clear();
        _list.Clear();
    }

    /// <summary>
    /// 确定集合是否包含一个指定的键值对项目
    /// </summary>
    /// <param name="item">在集合总查找的项目</param>
    /// <returns>
    /// 如果项目在集合中找到返回true，否则为false
    /// </returns>
    bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item)
    {
        if (ContainsKey(item.Key))
        {
            TValue keyValue = this[item.Key];

            if (keyValue.Equals(item.Value))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 复制集合元素到数组中, 以数组的索引开始.
    /// </summary>
    /// <param name="array">一维数组<see cref="System.Array"></see>，它是从集合中复制的目标。数组对象必须是从零开始的索引。</param>
    /// <param name="arrayIndex">Array中的从零开始的索引，位于复制开始。</param>
    /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex 小于0</exception>
    /// <exception cref="System.ArgumentNullException">array 为null</exception>
    /// <exception cref="System.ArgumentException">
    /// 数组为多维；或arrayIndex等于或大于数组的长度；
    /// 或源集合中元素的数量大于arrayIndex到目标数组结束间可用空间。
    /// 或者TKey的类型不能自动转换为目标数组的类型。</exception>
    public virtual void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        // Calling CopyTo on a Dictionary
        // Copies all KeyValuePair objects in Dictionary object to objs[]
        _dictionary.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// 获取集合中包含元素的数量
    /// </summary>
    /// <value></value>
    /// <returns>集合中所含元素的数目</returns>
    public virtual int Count => _list.Count;


    /// <summary>
    /// 从集合中移除指定的对象，该对象第一次出现在集合中。
    /// </summary>
    /// <param name="item">集合中待移除的对象</param>
    /// <returns>
    /// 如果项目成功地从集合中移除，否则为false。这种方法也返回false，如果项目没有在原来的集合中，该方法也返回false。
    /// </returns>
    /// <exception cref="System.NotSupportedException">集合为只读</exception>
    bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
    {
        if (ContainsKey(item.Key))
        {
            TValue keyValue = this[item.Key];

            if (keyValue.Equals(item.Value))
            {
                return _dictionary.Remove(item.Key);
            }
        }
        return false;

    }

    /// <summary>
    /// 获取一个值，该值指示集合是否是只读。
    /// </summary>
    /// <value></value>
    /// <returns>如果集合是只读的返回True，否则为false</returns>
    bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => false;

    #endregion

    #region IEnumerable<KeyValuePair<TKey,TValue>> 成员函数


    /// <summary>
    /// 返回一个集合迭代的枚举器
    /// </summary>
    /// <returns>
    /// IEnumerator对象接口能用来迭代集合的枚举器
    /// </returns>
    IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
    {
        return new OrderedDictionaryEnumerator<TKey, TValue>(_list);
    }

    /// <summary>
    /// 获取迭代结合的枚举器
    /// </summary>
    public virtual OrderedDictionaryEnumerator<TKey, TValue> GetEnumerator()
    {
        return new OrderedDictionaryEnumerator<TKey, TValue>(_list);
    }

    #endregion

    #region IEnumerable 成员函数

    /// <summary>
    /// 返回一个迭代对象，用来遍历集合。
    /// </summary>
    /// <returns>
    /// 一个可以用来迭代集合的 <see cref="System.Collections.IEnumerator"></see> 对象。
    /// </returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return new OrderedDictionaryEnumerator<TKey, TValue>(_list);
    }

    #endregion               

    #region ICloneable 成员函数

    /// <summary>
    /// 创建一个新的对象，它是当前实例的一个副本。
    /// </summary>
    /// <returns>
    /// 一个新的对象，此实例的一个副本。
    /// </returns>
    object ICloneable.Clone()
    {
        return Clone();
    }

    #endregion

    #region ICloneable<OrderedDictionary<TKey,TValue>> 成员函数

    /// <summary>
    /// 创建一个新的对象，它是当前实例的一个副本
    /// </summary>
    /// <returns>
    /// 一个新的对象，此实例的一个副本。
    /// </returns>
    public OrderedDictionary<TKey, TValue> Clone()
    {
        return new OrderedDictionary<TKey, TValue>(this);
    }

    #endregion

    #region IEnumerable<TValue> 成员函数

    /// <summary>
    /// 返回一个迭代对象，用来遍历集合。
    /// </summary>
    /// <returns>
    /// 用来迭代集合的<see cref="T:System.Collections.Generic.IEnumerator`1"></see>对象。
    /// </returns>
    IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator()
    {
        return new OrderedDictionaryEnumerator<TKey, TValue>(_list);
    }

    #endregion
}