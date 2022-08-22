using System.Runtime.Serialization;
using System.Security.Permissions;

namespace BB.Tools.Collections;

/// <summary>
/// 同步有序集合字典。
/// 一个同步的字典提供一个线程安全的字典集合。
/// 一个有序的字典是一个集合类，它里面的项目可以通过其索引或他们的键进行操作。
/// 有序集合对象，特点是查找非常高效率，尽量减少插入或者删除集合对象。
/// </summary>
/// <typeparam name="TKey">字典键的类型</typeparam>
/// <typeparam name="TValue">字典值的类型</typeparam>
[Serializable]
public class SyncOrderedDictionary<TKey, TValue> : OrderedDictionary<TKey, TValue>
{
    /// <summary>
    /// 获取与指定的键相关联的值。
    /// </summary>
    /// <param name="key">要获取的值的键。.</param>
    /// <param name="value">当此方法返回值时，如果找到该键，便会返回与指定的键相关联的值；否则，则会返回 value 参数的类型默认值。该参数未经初始化即被传递。</param>
    /// <returns> 如果字典对象包含具有指定键的元素，则为 true；否则为 false. </returns>
    public override bool TryGetValue(TKey key, out TValue value)
    {
        bool flag;
        lock (_dictionarytable)
        {
            flag = _dictionarytable.TryGetValue(key, out value);
        } 

        return flag;
    }
    /// <summary>获取或设置指定键相关联的值</summary>
    /// <returns>返回指定键中关联的值, 如果指定的键为无效，获取或设置操作将抛出 <see cref="ArgumentOutOfRangeException"></see>。
    /// </returns>
    /// <param name="key">获取或设置值的键</param>
    /// <exception cref="System.ArgumentNullException">key为null.</exception>
    /// <exception cref="System.Collections.Generic.KeyNotFoundException">在集合中属性检索和键不存在.</exception>
    public override TValue this[TKey key]
    {
        get => _dictionarytable[key];
        set
        {
            lock (_dictionarytable)
            {
                _dictionarytable[key] = value;
            } 
        } 
    }

    /// <summary>
    /// 获取或设置与指定索引的值
    /// </summary>
    /// <value></value>
    public override TValue this[int index]
    {
        get => _dictionarytable[index];
        set
        {
            lock (_dictionarytable)
            {
                _dictionarytable[index] = value;
            } 
        }
    }

    /// <summary>获取字典对象的键集合</summary>
    public override ICollection<TKey> Keys
    {
        get
        {
            ICollection<TKey> keys;

            lock (_dictionarytable)
            {
                keys = _dictionarytable.Keys;
            }
            return keys;

        }
    }

    /// <summary>
    /// 获取字典中值的对象集合
    /// </summary>
    public override ICollection<TValue> Values
    {
        get
        {
            ICollection<TValue> collection;


            lock (_dictionarytable)
            {
                collection = _dictionarytable.Values;
            }
            return collection;

        }
    }

    /// <summary>获取<see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> 对象，该对象用来比较字典表键的相等</summary>
    /// <returns>对象<see cref="System.Collections.Generic.IEqualityComparer{TKey}"></see> 泛型接口，用来判断当前对象<see cref="T:System.Collections.Generic.Dictionary`2"></see>的相等性，
    /// 并提供键的哈希值.</returns>
    public override IEqualityComparer<TKey> Comparer
    {
        get
        {
            IEqualityComparer<TKey> tempComparer;
            lock (_dictionarytable)
            {
                tempComparer = _dictionarytable.Comparer;
            }
            return tempComparer;
        }
    }

    /// <summary>
    /// 获取集合中包含元素的数量
    /// </summary>
    /// <value></value>
    /// <returns>集合中所含元素的数目</returns>
    public override int Count => _dictionarytable.Count;

    /// <summary>
    /// 在集合中移除指定键的元素
    /// </summary>
    /// <param name="key">待移除元素的键</param>
    /// <returns>
    /// 如果元素被移除返回true，否则为false。如果原始集合中没有发现指定键的元素，也返回false。
    /// </returns>
    /// <exception cref="System.NotSupportedException">集合为只读</exception>
    /// <exception cref="System.ArgumentNullException">键为空</exception>
    public override bool Remove(TKey key)
    {
        lock (_dictionarytable)
        {
            return _dictionarytable.Remove(key);
        }

    }

    /// <summary>
    /// 获取迭代结合的枚举器
    /// </summary>
    public override OrderedDictionaryEnumerator<TKey, TValue> GetEnumerator()
    {
        OrderedDictionaryEnumerator<TKey, TValue> temp;
        lock (_dictionarytable)
        {
            temp = _dictionarytable.GetEnumerator();
        } 

        return temp;
    }

    private OrderedDictionary<TKey, TValue> _dictionarytable = new OrderedDictionary<TKey,TValue>();

    /// <summary>
    /// 初始化一个 <see cref="SyncOrderedDictionary{TKey, TValue}"/> 实例.
    /// </summary>
    public SyncOrderedDictionary()
    { 
    }

    /// <summary>
    /// 初始化一个 <see cref="SyncOrderedDictionary{TKey, TValue}"/> 实例.
    /// </summary>
    /// <param name="dictionary">字典对象</param>
    public SyncOrderedDictionary(OrderedDictionary<TKey, TValue> dictionary)
    {
        _dictionarytable = dictionary;
    }

    /// <summary>
    /// 在字典中添加一个元素，根据提供的键和值
    /// </summary>
    /// <param name="key">作为待添加元素的键</param>
    /// <param name="value">作为待添加元素的值.</param>
    /// <exception cref="System.NotSupportedException">字典对象为只读</exception>
    /// <exception cref="System.ArgumentException">含有相同键的元素已经存在字典中</exception>
    /// <exception cref="System.ArgumentNullException">键为空.</exception>
    public override void Add(TKey key, TValue value)
    {
        lock (_dictionarytable)
        {
            _dictionarytable.Add(key, value);
        }
    }

    /// <summary>
    /// 获取一个值，该值判定访问的<see cref="System.Collections.ICollection"></see>对象是否为同步（线程安全）。
    /// </summary>
    /// <returns>如果词典的访问是同步的（线程安全）返回true，否则为false。默认为false。</returns>
    public override bool IsSynchronized => true;

    /// <summary>
    /// 移除字典所有项目
    /// </summary>
    public override void Clear()
    {
        lock (_dictionarytable)
        {
            _dictionarytable.Clear();
        }
    }

    /// <summary>实现 <see cref="T:System.Runtime.Serialization.ISerializable"></see> 接口并返回序列化字典对象所需要的数据.</summary>
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
        info.AddValue("ParentTable", _dictionarytable, typeof(CDictionary<TKey, TValue>));
    }

    /// <summary>
    /// 复制集合元素到数组中, 以数组的索引开始.
    /// </summary>
    /// <param name="array">一维数组<see cref="System.Array"></see>，它是从集合中复制的目标。数组对象必须是从零开始的索引。</param>
    /// <param name="index">Array中的从零开始的索引，位于复制开始。</param>
    public override void CopyTo(KeyValuePair<TKey, TValue>[] array, int index)
    {
        lock (_dictionarytable)
        {
            _dictionarytable.CopyTo(array, index);
        }

    } 
        
    /// <summary>实现<see cref="T:System.Runtime.Serialization.ISerializable"></see> 接口并在反序列化完成后抛出反序列化事件。</summary>
    /// <param name="sender">反序列化事件的源对象</param>
    /// <exception cref="T:System.Runtime.Serialization.SerializationException">在当前字典中关联的该对象为无效对象.</exception>
    public override void OnDeserialization(object sender)
    {
        //Syncronized Dictionory Cannot throw event
    }

    /// <summary>
    /// 判断字典是否包含指定键的元素
    /// </summary>
    /// <param name="key">在字典中查找的键.</param>
    /// <returns>
    /// 如果字典中包含指定键的元素，返回true，否则为false
    /// </returns>
    public override bool ContainsKey(TKey key)
    {
        return _dictionarytable.ContainsKey(key);
    }

    /// <summary>
    /// 判断字典<see cref="System.Collections.Generic.Dictionary{TKey, TValue}"></see> 是否包含指定的值。
    /// </summary>
    /// <param name="value">待判断的值,如果值为引用类型，则可以为null</param>
    /// <returns>
    /// 如果字典包含项目，则返回true，否则为false。
    /// </returns>
    public override bool ContainsValue(TValue value)
    {
        bool flag;
        lock (_dictionarytable)
        {
            flag = _dictionarytable.ContainsValue(value);
        }
        return flag;
    } 
}