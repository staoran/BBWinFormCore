using System.Runtime.Serialization;
using System.Security.Permissions;

namespace BB.Tools.Collections;

/// <summary>
/// 同步字典对象，提供一个线程安全的字典集合。
/// 该类可废弃，使用系统内置对象ConcurrentDictionary进行处理即可。
/// </summary>
[Serializable]
public class SyncDictionary<TKey, TValue> : CDictionary<TKey, TValue>
{
    /// <summary>
    /// 获取指定键的关联值
    /// </summary>
    /// <param name="key">字典键</param>
    /// <param name="value">当此方法返回值时，如果找到该键，便会返回与指定的键相关联的值；否则，则会返回 value 参数的类型默认值。该参数未经初始化即被传递。</param>
    /// <returns>如果字典对象包含具有指定键的元素，则为 true；否则为false. </returns>
    public new bool TryGetValue(TKey key, out TValue value)
    {
        bool flag;
        lock (_dictionary)
        {
            flag = _dictionary.TryGetValue(key, out value);
        }
        return flag;
    }

    /// <summary>获取或设置指定键的值</summary>
    /// <returns>与指定键关联的值。如果没有找到指定键，KeyNotFoundException将抛出，操作创建一个有指定键的新元素。</returns>
    /// <param name="key">设定值的键</param>
    /// <exception cref="System.ArgumentNullException">键位空</exception>
    /// <exception cref="System.Collections.Generic.KeyNotFoundException">属性检索和键在集合不存在。</exception>
    public new TValue this[TKey key]
    {
        get => _dictionary[key];
        set
        {
            lock (_dictionary)
            {
                _dictionary[key] = value;
            } 
        } 
    }

    /// <summary>获取字典对象的键集合</summary>
    public new KeyCollection Keys
    {
        get
        {
            KeyCollection collection;

            lock (_dictionary)
            {
                collection = new KeyCollection(_dictionary);
            }
            return collection;
        }
    }

    /// <summary>获取字典中值的对象集合</summary>
    public new ValueCollection Values
    {
        get
        {

            ValueCollection collection;

            lock (_dictionary)
            {
                collection = new ValueCollection(_dictionary);
            }
            return collection;

        }
    }

    /// <summary>获取<see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> 对象，该对象用来比较字典表键的相等</summary>
    /// <returns>对象<see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> 通用接口，用来判断当前对象<see cref="T:System.Collections.Generic.Dictionary`2"></see>的相等性，
    /// 并提供键的哈希值.</returns>
    public new IEqualityComparer<TKey> Comparer
    {
        get
        {
            IEqualityComparer<TKey> tempComparer;
            lock (_dictionary)
            {
                tempComparer = _dictionary.Comparer;
            }
            return tempComparer;
        }
    }

    /// <summary>
    /// 获取字典集合中包含键值对的数量
    /// </summary>
    /// <value></value>
    /// <returns>字典集合中所含元素的数目</returns>
    public new virtual int Count => _dictionary.Count;

    /// <summary>在集合中移除指定键的元素</summary>
    /// <returns>如果元素被移除返回true，否则为false。如果原始集合中没有发现指定键的元素，也返回false。</returns>
    /// <param name="key">待移除元素的键</param>
    /// <exception cref="System.ArgumentNullException">键为空</exception>
    public new bool Remove(TKey key)
    {
        lock (_dictionary)
        {
            return _dictionary.Remove(key);
        }
    }

    /// <summary>获取迭代结合的枚举器.</summary>
    public new Enumerator GetEnumerator()
    {
        Enumerator temp;
        lock (_dictionary)
        {
            temp = _dictionary.GetEnumerator();
        }

        return temp;
    }

    private CDictionary<TKey, TValue> _dictionary = new CDictionary<TKey,TValue>();

    /// <summary>
    /// 初始化SyncDictionary对象实例
    /// </summary>
    /// <param name="dictionary">字典对象</param>
    public SyncDictionary(CDictionary<TKey, TValue> dictionary)
    {
        _dictionary = dictionary;
    }

    /// <summary>
    /// 初始化SyncDictionary对象实例
    /// </summary>
    public SyncDictionary()
    {
    }

    /// <summary>
    /// 为字典表添加指定的键值
    /// </summary>
    /// <param name="key">待添加元素的键</param>
    /// <param name="value">待添加元素的值. 该值可以是空引用类型.</param>
    /// <exception cref="System.ArgumentException">一个具有相同键的元素已经存在于字典</exception>
    /// <exception cref="System.ArgumentNullException">键为空</exception>
    public new void Add(TKey key, TValue value)
    {
        lock (_dictionary)
        {
            _dictionary.Add(key, value);
        }
    }

    /// <summary>
    /// 获取一个值，指出对集合的存取是否同步（线程安全）
    /// </summary>
    /// <value>如果词典的访问是同步的（线程安全）则为True，否则为false。默认为false。 </value>
    public override bool IsSynchronized => true;

    /// <summary>
    /// 从字典中移除所有的键值
    /// </summary>
    public new void Clear()
    {
        lock (_dictionary)
        {
            _dictionary.Clear();
        }
    }

    /// <summary>
    /// 实现 <see cref="System.Runtime.Serialization"></see> 接口并返回需要序列化字典<see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> 实例的数据。
    /// </summary>
    /// <param name="info">一个 SerializationInfo 对象，它包含序列化Dictionary所需的信息。</param>
    /// <param name="context"> StreamingContext 结构，该结构包含与 Dictionary相关联的序列化流的源和目标。</param>
    /// <exception cref="System.ArgumentNullException">info为空引用</exception>
    [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
        {
            throw new ArgumentNullException("info");
        }
        info.AddValue("ParentTable", _dictionary, typeof(CDictionary<TKey, TValue>));
    }

    /// <summary>
    /// 初始化SyncDictionary对象实例
    /// </summary>
    /// <param name="info"><see cref="System.Runtime.Serialization.SerializationInfo"></see>对象包含序列化<see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>的必要信息。</param>
    /// <param name="context">A <see cref="System.Runtime.Serialization.StreamingContext"></see> 含有与序列化流<see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>的源和目标的结构。</param>
    internal SyncDictionary(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
        _dictionary = (CDictionary<TKey, TValue>)info.GetValue("ParentTable", typeof(CDictionary<TKey, TValue>));
        if (_dictionary == null)
        {
            throw new SerializationException("Insufficient state to return the real object.");
        }
    }

    /// <summary>
    /// 把ICollection的元素复制到一个KeyValuePair类型的数组，在指定的阵列索引开始。
    /// </summary>
    /// <param name="array">一维数组类型KeyValuePair是从ICollection复制KeyValuePair元素的目标。该数组必须具有从零开始的索引。</param>
    /// <param name="index">array中的从零开始的索引，位于复制开始</param>
    public new void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
    {
        lock (_dictionary)
        {
            _dictionary.CopyTo(array, index);
        }
    } 

    /// <summary>
    /// 实现 <see cref="System.Runtime.Serialization.ISerializable"></see> 接口并抛出反序列化事件，当反序列化完成的时候。
    /// </summary>
    /// <param name="sender">反序列化事件的源对象</param>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">在当前字典中关联的该对象为无效对象.</exception>
    public override void OnDeserialization(object sender)
    {
        //同步字典不能抛出事件
    }

    /// <summary>
    /// 确定指定的键是否包含关键。
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>
    /// 	如果包含指定的键返回True，否则false。
    /// </returns>
    public new bool ContainsKey(TKey key)
    {
        return _dictionary.ContainsKey(key);
    }
        
    /// <summary>
    /// 判断<see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see>是否包含指定的值
    /// </summary>
    /// <param name="value">在字典中待查找的值，如果值是引用类型则可以为null。</param>
    /// <returns>
    /// 如果 <see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> 包含指定值的元素返回true，否则返回false.
    /// </returns>
    public new bool ContainsValue(TValue value)
    {
        bool flag;
        lock (_dictionary)
        {
            flag = _dictionary.ContainsValue(value);
        }
        return flag;
    }

}