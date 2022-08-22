using System.Security.Permissions;
using System.Xml.Serialization;

namespace BB.Tools.Collections;

/// <summary>
/// 表示可通过索引访问的对象的强类型列表。提供方法来进行搜索，排序和操作列表。
/// </summary>
/// <typeparam name="T">列表中的元素的类型。</typeparam>
[Serializable()]
[XmlRoot("list")]
public class CList<T> : List<T>, ICloneable, ICloneable<CList<T>>
{
    #region 构造函数
    /// <summary>
    /// 初始化列表，默认为空列表。
    /// </summary>
    public CList() : base() { }

    /// <summary>
    /// 初始化列表，并从集合对象collection中复制元素到集合中。
    /// </summary>
    /// <param name="collection">待复制到新列表中的集合.</param>
    /// <exception cref="System.ArgumentNullException">collection 为空应用.</exception>
    public CList(IEnumerable<T> collection) : base(collection) { } 

    /// <summary>
    /// 初始化列表，并指定容量大小
    /// </summary>
    /// <param name="capacity">新的列表可以初步存储的元素的数量。</param>
    /// <exception cref="System.ArgumentOutOfRangeException">capacity 小于0. </exception>
    public CList(int capacity) : base(capacity) { }

    #endregion

    #region 属性方法

    /// <summary>
    /// 获取一个值，该值指示列表的访问是否是同步的（线程安全）。
    /// </summary>
    /// <value>如果列表的访问是同步的（线程安全）为True，否则为false。默认为false。</value>
    public virtual bool IsSynchronized => false;

    /// <summary>返回是同步的（线程安全）的列表包装。</summary>
    /// <param name="list">待同步的列表 </param>
    /// <exception cref="System.ArgumentNullException">list为空引用. </exception>
    /// <typeparam name="V">列表中的元素的类型</typeparam>
    [HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
    public static CList<TV> Synchronized<TV>(CList<TV> list)
    {
        if (list == null)
        {
            throw new ArgumentNullException("list");
        }
        return new SyncList<TV>(list);
    }

    /// <summary>返回一个<see cref="System.Collections.ArrayList"></see>对象，它的元素被复制为指定的值.</summary>
    /// <returns>一个有指定元素数量的 <see cref="System.Collections.ArrayList"></see>对象，它所有值被复制为指定的值。</returns>
    /// <param name="value">要复制的对象在新的ArrayList重复多次。该值可以为null。 </param>
    /// <param name="count">值被复制的次数</param>
    /// <exception cref="System.ArgumentOutOfRangeException">count 小于0. </exception>
    /// <typeparam name="V">列表中的元素的类型</typeparam>
    public static CList<TV> Repeat<TV>(TV value, int count)
    {
        if (count < 0)
        {
            throw new ArgumentException("count", "Non-negative number required.");
        }

        CList<TV> list = new CList<TV>(count);
        for (int index = 0; index < count; index++)
        {
            list.Add(value);
        }

        return list;
    }
    #endregion


    #region 克隆接口实现

    /// <summary>
    /// 创建一个新的对象，是当前实例副本。
    /// </summary>
    public CList<T> Clone()
    {
        return new CList<T>(this);
    }

    /// <summary>
    /// 创建一个新的对象，是当前实例副本。
    /// </summary>
    object ICloneable.Clone()
    {
        return Clone();
    }

    #endregion
}