using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Generics = System.Collections.Generic;

namespace BB.Tools.Collections;

/// <summary>
/// 表示键和值的字典集合类，继承系统默认的Dictionary类，并实现特别的接口
/// </summary>
/// <typeparam name="TKey">字典中的键的类型</typeparam>
/// <typeparam name="TValue">字典中的值的类型</typeparam>
[Serializable()]
[XmlRoot("dictionary")]
public class CDictionary<TKey, TValue> : System.Collections.Generic.Dictionary<TKey, TValue>, 
    ICloneable, ICloneable<CDictionary<TKey, TValue>>, IXmlSerializable
{
    #region 构造函数

    /// <summary>
    /// 初始化Dictionary类的新实例，实例为空并具有默认的初始容量，使用默认的相等比较器等。
    /// </summary>     
    public CDictionary() : base() { } 

    /// <summary>
    /// 初始化Dictionary类，并从指定的参数中复制元素。
    /// </summary>
    /// <param name="dictionary">字典对象，它的元素被复制到新的字典集合中</param>
    /// <exception cref="System.ArgumentException">dictionary 包含一个或多个重复键</exception>
    /// <exception cref="System.ArgumentNullException">dictionary 为 null。</exception>
    public CDictionary(Generics.IDictionary<TKey, TValue> dictionary) : base(dictionary) { }

    /// <summary>
    /// 初始化Dictionary类的新实例，实例为空并具有默认的初始容量，使用指定的相等比较器等。
    /// </summary>
    /// <param name="comparer">比较键时使用指定比较器，或者为 null，以便为键类型使用默认的相等比较器。</param>
    public CDictionary(Generics.IEqualityComparer<TKey> comparer) : base(comparer) { }

    /// <summary>
    /// 初始化 Dictionary类的新实例，该实例为空且具有指定的初始容量，并为键类型使用默认的相等比较器。
    /// </summary>
    /// <param name="capacity">Dictionary可包含的初始元素数。</param>
    /// <exception cref="System.ArgumentOutOfRangeException">capacity 小于 0。</exception>
    public CDictionary(int capacity) : base(capacity) { } 

    /// <summary>
    /// 初始化 Dictionary类的新实例，该实例包含从指定的dictionary中复制的元素并使用指定的比较器。
    /// </summary>
    /// <param name="dictionary">它的元素被复制到新的Dictionary中。</param>
    /// <param name="comparer">比较键时要使用指定比较器，或者为 null，以便为键类型使用默认相等比较器。</param>
    /// <exception cref="System.ArgumentException">dictionary 包含一个或多个重复键。</exception>
    /// <exception cref="System.ArgumentNullException"> dictionary 为 null。</exception>
    public CDictionary(Generics.IDictionary<TKey, TValue> dictionary, Generics.IEqualityComparer<TKey> comparer) : base(dictionary, comparer) { }

    /// <summary>
    /// 初始化 Dictionary类的新实例，该实例为空且具有指定的初始容量，并使用指定的相等比较器
    /// </summary>
    /// <param name="capacity">Dictionary可包含的初始元素数。</param>
    /// <param name="comparer">比较键时要使用指定比较器，或者为 null，以便为键类型使用默认相等比较器。</param>
    /// <exception cref="System.ArgumentOutOfRangeException">capacity 小于 0。</exception>
    public CDictionary(int capacity, Generics.IEqualityComparer<TKey> comparer) : base(capacity, comparer) { }
        
    /// <summary>
    /// 用序列化数据初始化 Dictionary 类的新实例。
    /// </summary>
    /// <param name="info">一个 SerializationInfo 对象，它包含序列化Dictionary所需的信息。</param>
    /// <param name="context"> StreamingContext 结构，该结构包含与 Dictionary相关联的序列化流的源和目标。</param>
    protected CDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }

    #endregion

    #region 方法实现

    /// <summary>
    /// 确定 Dictionary是否包含指定的键。
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>
    /// 包含具有指定键的元素，则为 true；否则为false
    /// </returns>
    public bool Contains(TKey key)
    {
        return ContainsKey(key);
    }

    /// <summary>
    /// 返回Hashtable的同步（线程安全）封装对象。
    /// </summary>
    /// <param name="dictionary">字典对象</param>
    /// <returns>
    /// 一个同步（线程安全）的封装字典表
    /// </returns>2
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static CDictionary<TKey, TValue> Synchronized(CDictionary<TKey, TValue> dictionary)
    {
        if (dictionary == null)
        {
            throw new ArgumentNullException("dictionary");
        }
        return new SyncDictionary<TKey, TValue>(dictionary);
    }

    /// <summary>实现序列化接口，并返回序列化字典实例所需的数据。</summary>
    /// <param name="info">一个 SerializationInfo 对象，它包含序列化Dictionary所需的信息。</param>
    /// <param name="context"> StreamingContext 结构，该结构包含与 Dictionary相关联的序列化流的源和目标。</param>
    /// <exception cref="System.ArgumentNullException">info为空引用</exception>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }

    /// <summary>
    /// 获取一个值，该值指示集合的访问是否是同步的（线程安全）。
    /// </summary>
    /// <returns>如果词典的访问是同步的（线程安全）为True，否则为false。默认为false。</returns>
    public virtual bool IsSynchronized => false;

    /// <summary>
    /// 把ICollection的元素复制到KeyValuePair类型的数组，在指定的数组索引开始。
    /// </summary>
    /// <param name="array">一维数组，它是类型的KeyValuePair 从ICollection的复制目的地的KeyValuePair元素。
    /// 阵列必须有以零起始的索引。
    /// </param>
    /// <param name="index">集合中的从零开始的索引，该索引是复制的位置</param>
    public void CopyTo(Generics.KeyValuePair<TKey, TValue>[] array, int index)
    {
        ((Generics.IDictionary<TKey, TValue>)this).CopyTo(array, index);
    }
    #endregion

    #region 克隆接口实现

    /// <summary>
    /// 创建一个新的对象，是当前实例副本。
    /// </summary>
    object ICloneable.Clone()
    {
        return Clone();
    }

    /// <summary>
    /// 创建一个新的对象，是当前实例副本。
    /// </summary>
    public CDictionary<TKey, TValue> Clone()
    {
        return new CDictionary<TKey, TValue>(this);
    }

    #endregion

    #region XML序列化接口实现

    /// <summary>
    /// 保留属性
    /// </summary>
    public XmlSchema GetSchema()
    {
        return null;
    }

    /// <summary>
    /// 生成XML表示的对象。
    /// </summary>
    public void ReadXml(XmlReader reader)
    {
        XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));

        bool wasEmpty = reader.IsEmptyElement;
        reader.Read();
            
        if (wasEmpty)
            return;

        while (reader.NodeType != XmlNodeType.EndElement)
        {
            reader.ReadStartElement("item");
            reader.ReadStartElement("key");

            TKey key = (TKey)keySerializer.Deserialize(reader);
            reader.ReadEndElement();
            reader.ReadStartElement("value");

            TValue value = (TValue)valueSerializer.Deserialize(reader);
            reader.ReadEndElement();

            Add(key, value);

            reader.ReadEndElement();
            reader.MoveToContent();
        }

        reader.ReadEndElement();
    }

    /// <summary>
    /// 转换成XML表示的对象。
    /// </summary>
    public void WriteXml(XmlWriter writer)
    {
        XmlSerializer keySerializer = new XmlSerializer(typeof(TKey));
        XmlSerializer valueSerializer = new XmlSerializer(typeof(TValue));
            
        foreach (TKey key in Keys)
        {
            writer.WriteStartElement("item");
            writer.WriteStartElement("key");

            keySerializer.Serialize(writer, key);
            writer.WriteEndElement();
                
            writer.WriteStartElement("value");
            TValue value = this[key];
            valueSerializer.Serialize(writer, value);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
    }

    #endregion
}