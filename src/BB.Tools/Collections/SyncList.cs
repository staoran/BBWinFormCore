using System.Collections.ObjectModel;

namespace BB.Tools.Collections;

/// <summary>
/// 同步列表，提供一个线程安全的列表集合。
/// </summary>
/// <typeparam name="T">列表元素的类型</typeparam>
[Serializable]
public class SyncList<T> : CList<T>
{
    /// <summary>
    /// 列表对象
    /// </summary>
    protected CList<T> list = new CList<T>();

    /// <summary>
    /// 初始化一个同步列表对象实例
    /// </summary>
    public SyncList()
    {
    }

    /// <summary>
    /// 初始化一个同步列表对象实例
    /// </summary>
    /// <param name="list">The list.</param>
    public SyncList(CList<T> list)
    {
        this.list = list;
    }

    /// <summary>
    /// 获取一个值，判断<see cref="System.Collections.ICollection"></see> 是否为同步的（线程安全）.
    /// </summary>
    /// <value>如果为同步（线程安全）的，返回true,否则false. 默认为false </value>
    /// <returns>如果为同步（线程安全）的，返回true,否则false</returns>
    public override bool IsSynchronized => true;

    /// <summary>添加一个对象到 <see cref="System.Collections.Generic.List&lt;T&gt;"></see>末尾.</summary>
    /// <param name="item">待添加的对象. 值可以为空引用类型。</param>
    public new void Add(T item)
    {
        lock (list)
        {
            list.Add(item);
        }
    }

    /// <summary>添加一个集合到 <see cref="System.Collections.Generic.List&lt;T&gt;"></see>末尾</summary>
    /// <param name="collection">其集合元素待添加到 <see cref="System.Collections.Generic.List&lt;T&gt;"></see>末尾的集合. 
    /// 集合本身不能为空，但如果类型TKey是一个引用类型，则它包含的元素可以为空</param>
    /// <exception cref="System.ArgumentNullException">集合为空</exception>
    public new void AddRange(IEnumerable<T> collection)
    {
        lock (list)
        {
            list.InsertRange(Count, collection);
        }
    }
        
    /// <summary>
    /// 为当前集合返回一个只读的<see cref="System.Collections.Generic.IList&lt;T&gt;"></see> 包装类。
    /// </summary>
    public new ReadOnlyCollection<T> AsReadOnly()
    {
        lock (list)
        {
            return new ReadOnlyCollection<T>(list);
        }
    }

    /// <summary>
    /// 从<see cref="System.Collections.Generic.List&lt;T&gt;"></see>集合中移除所有对象。
    /// </summary>
    public new void Clear()
    {
        lock (list)
        {
            list.Clear();
        }
    }
        
    /// <summary>判定指定的元素是否在 <see cref="System.Collections.Generic.List&lt;T&gt;"></see> 中.</summary>
    /// <returns>如果元素在 <see cref="System.Collections.Generic.List&lt;T&gt;"></see>中存在返回true，否则返回false.</returns>
    /// <param name="item">待查找的元素，如果值为引用类型则可以为null</param>
    public new bool Contains(T item)
    {
        bool flag;

        lock (list)
        {
            flag = list.Contains(item);
        }

        return flag;
    } 

    /// <summary>
    /// 转换当前 <see cref="System.Collections.Generic.List&lt;T&gt;"></see> 的元素到另外的类型，并返回包含转换后的元素列表。
    /// </summary>
    /// <returns>一个<see cref="System.Collections.Generic.List&lt;T&gt;"></see>集合，包含当前集合转换后的元素。</returns>
    /// <param name="converter">每个元素从一种类型到另一种类型的转换委托</param>
    /// <exception cref="System.ArgumentNullException">converter 为 null.</exception>
    public new CList<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
    {
        CList<TOutput> list;
        lock (this.list)
        {
            list = new CList<TOutput>(this.list.ConvertAll(converter));
        }

        return list;
    }

    /// <summary>复制整个 <see cref="System.Collections.Generic.List&lt;T&gt;"></see> 到一个一维数组中</summary>
    /// <param name="array">一维数组，从列表中复制元素的目标对象。该数组必须具有从零开始的索引。</param>
    /// <exception cref="System.ArgumentException">源列表中元素的数量大于目标数组能容纳的数量</exception>
    /// <exception cref="System.ArgumentNullException">array 为 null.</exception>
    public new void CopyTo(T[] array)
    {
        lock (list)
        {
            list.CopyTo(array, 0);
        }
    }

    /// <summary>复制整个 <see cref="System.Collections.Generic.List&lt;T&gt;"></see> 到一个一维数组中</summary>
    /// <param name="array">一维数组，从列表中复制元素的目标对象。该数组必须具有从零开始的索引。</param>
    /// <param name="arrayIndex">array中的从零开始的索引，位于复制开始。</param>
    /// <exception cref="System.ArgumentException">arrayIndex 大于或等于array数组长度；或源列表的元素数量大于目标array数组从arrayIndex到结束的可用空间</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">arrayIndex 小于等于 0.</exception>
    /// <exception cref="System.ArgumentNullException">array 为 null.</exception>
    public new void CopyTo(T[] array, int arrayIndex)
    {
        lock (list)
        {
            list.CopyTo(array, arrayIndex);
        }
    }
        
    /// <summary>
    /// 从列表中复制一个范围内的元素到一个相容的一维数组中。以指定的目标索引开始。
    /// </summary>
    /// <param name="index">在从零开始的索引，位于复制开始的源。</param>
    /// <param name="array">一维数组，从列表中复制元素的目标对象。该数组必须具有从零开始的索引。</param>
    /// <param name="arrayIndex">array中的从零开始的索引，位于复制开始。</param>
    /// <param name="count">待复制的元素数量.</param>
    /// <exception cref="System.ArgumentNullException">array 为 null. </exception>
    /// <exception cref="System.ArgumentOutOfRangeException">index 小于 0；或者arrayIndex 小于0；或者count 小于 0. </exception>
    /// <exception cref="System.ArgumentException">index 大于或等于源列表的数量。或者arrayIndex 大于或等于数组array的长度。或者从index到源列表结束的元素数量大于从 arrayIndex 到数组array结束的可用空间。 </exception>
    public new void CopyTo(int index, T[] array, int arrayIndex, int count)
    {
        lock (list)
        {
            list.CopyTo(index, array, arrayIndex, count);
        }
    }

    /// <summary>
    /// 判定<see cref="System.Collections.Generic.List&lt;T&gt;"></see>是否包含符合定义在指定predicate的条件中包含有元素。</summary>
    /// <returns>如果列表中包含指定条件的元素，返回true，否则为false。</returns>
    /// <param name="match">定义元素的条件来搜索对象的委托</param>
    /// <exception cref="System.ArgumentNullException">match 为 null.</exception>
    public new bool Exists(Predicate<T> match)
    {
        return (FindIndex(match) != -1);
    }
        
    /// <summary>根据定义条件查询一个元素，并返回第一个在整个列表中出现的元素.</summary>
    /// <returns>根据查询条件，第一个在整个列表中出现的元素，如果没有找到，返回Tkey类型的默认值。</returns>
    /// <param name="match">定义元素的条件来搜索的委托</param>
    /// <exception cref="System.ArgumentNullException">match 为 null.</exception>
    public new T Find(Predicate<T> match)
    {
        T temp;

        lock (list)
        {
            temp = list.Find(match);
        }

        return temp;
    }

    /// <summary>获取指定Predicate定义的条件相匹配的所有元素。</summary>
    /// <returns>如果遭到元素，返回指定Predicate定义的条件相匹配的所有元素列表，否则返回一个空列表。</see>.</returns>
    /// <param name="match">定义元素的条件来搜索的委托</param>
    /// <exception cref="System.ArgumentNullException">match 为 null.</exception>
    public new CList<T> FindAll(Predicate<T> match)
    {
        CList<T> temp;
        lock (list)
        {
            temp = new CList<T>(list.FindAll(match));
        }

        return temp;
    }

    /// <summary>
    /// 查找符合指定Predicate定义的条件相匹配的一个元素，并返回第一次出现在整个集合中的从零开始的索引。
    /// </summary>
    /// <returns>如果找到符合定义查询条件的元素，返回第一个出现在集合中从零开始的索引，否则返回–1.</returns>
    /// <param name="match">定义元素的条件来搜索的委托</param>
    /// <exception cref="System.ArgumentNullException">match 为 null.</exception>
    public new int FindIndex(Predicate<T> match)
    {
        int temp;
        lock (list)
        {
            temp = list.FindIndex(match);
        }

        return temp;
    }

    /// <summary>
    /// 查找符合指定Predicate定义的条件相匹配的一个元素，并返回第一次出现在整个集合中的从零开始的索引。
    /// </summary>
    /// <returns>如果找到符合定义查询条件的元素，返回第一个出现在集合中从零开始的索引，否则返回–1.</returns>
    /// <param name="startIndex">从零开始的起始索引的搜索。</param>
    /// <param name="match">定义元素的条件来搜索的委托</param>
    /// <exception cref="System.ArgumentOutOfRangeException">startIndex 在列表集合有效索引之外。</exception>
    /// <exception cref="System.ArgumentNullException">match 为 null.</exception>
    public new int FindIndex(int startIndex, Predicate<T> match)
    {
        int temp;
        lock (list)
        {
            temp = list.FindIndex(startIndex, match);
        }

        return temp;
    }
        
    /// <summary>
    /// 查找符合指定Predicate定义的条件相匹配的一个元素，并返回第一次出现在整个集合中的从零开始的索引。
    /// 以指定索引开始。
    /// </summary>
    /// <returns>如果找到符合定义查询条件的元素，返回第一个出现在集合中从零开始的索引，否则返回–1.</returns>
    /// <param name="count">搜索部分中的元素数量。</param>
    /// <param name="startIndex">从零开始的起始索引的搜索。</param>
    /// <param name="match">定义元素的条件来搜索的委托</param>
    /// <exception cref="System.ArgumentOutOfRangeException">startIndex 在列表集合有效索引之外。</exception>
    /// <exception cref="System.ArgumentNullException">match 为 null.</exception>
    public new int FindIndex(int startIndex, int count, Predicate<T> match)
    {
        int temp;
        lock (list)
        {
            temp = list.FindIndex(startIndex, count, match);
        }

        return temp;
    }
        
    /// <summary>
    /// 查找符合指定Predicate定义的条件相匹配的一个元素，并返回在整个集合中出现的最后一个元素。
    /// </summary>
    /// <returns>如果遭到符合定义条件的元素，则返回最后出现的最后一个元素，否则返回Tkey类型的默认值。</returns>
    /// <param name="match">定义元素的条件来搜索的委托</param>
    /// <exception cref="System.ArgumentNullException">match 为 null.</exception>
    public new T FindLast(Predicate<T> match)
    {
        T temp;

        lock (list)
        {
            temp = list.FindLast(match);
        }

        return temp;

    }

    /// <summary>
    /// 查找符合指定Predicate定义的条件相匹配的一个元素，并返回最后出现在整个集合中的从零开始的索引。
    /// </summary>
    /// <returns>如果找到符合定义查询条件的元素，返回最后一个出现在集合中从零开始的索引，否则返回–1。</returns>
    /// <param name="match">定义元素的条件来搜索的委托</param>
    /// <exception cref="System.ArgumentNullException">match 为 null.</exception>
    public new int FindLastIndex(Predicate<T> match)
    {
        int temp;

        lock (list)
        {
            temp = list.FindLastIndex(match);
        }

        return temp;
    }

    /// <summary>
    /// 查找符合指定Predicate定义的条件相匹配的一个元素，并返回最后出现在整个集合中的从零开始的索引。
    /// </summary>
    /// <returns>如果找到符合定义查询条件的元素，返回最后一个出现在集合中从零开始的索引，否则返回–1。</returns>
    /// <param name="startIndex">开始查找的索引位置</param>
    /// <param name="match">定义元素的条件来搜索的委托</param>
    /// <exception cref="System.ArgumentOutOfRangeException">startIndex 在列表集合有效索引之外。</exception>
    /// <exception cref="System.ArgumentNullException">match 为 null.</exception>
    public new int FindLastIndex(int startIndex, Predicate<T> match)
    {
        int temp;

        lock (list)
        {
            temp = list.FindLastIndex(startIndex, match);
        }

        return temp;
    }

    /// <summary>
    /// 查找符合指定Predicate定义的条件相匹配的一个元素，并返回最后出现在整个集合中的从零开始的索引。
    /// </summary>
    /// <returns>如果找到符合定义查询条件的元素，返回最后一个出现在集合中从零开始的索引，否则返回–1。</returns>
    /// <param name="startIndex">从零开始的起始索引的搜索。</param>
    /// <param name="count">搜索部分中的元素数量。</param>
    /// <param name="match">定义元素的条件来搜索的委托</param>
    /// <exception cref="System.ArgumentOutOfRangeException">startIndex 在列表集合有效索引之外。</exception>
    /// <exception cref="System.ArgumentNullException">match 为 null.</exception>
    public new int FindLastIndex(int startIndex, int count, Predicate<T> match)
    {
        int temp;

        lock (list)
        {
            temp = list.FindLastIndex(startIndex, count, match);
        }

        return temp;
    }
                
    /// <summary>
    /// 为集合中每个元素，执行指定的Action方法
    /// </summary>
    /// <param name="action">为列表每个元素执行的<see cref="System.Action&lt;T&gt;"></see> 代理.</param>
    /// <exception cref="System.ArgumentNullException">action 为 null.</exception>
    public new void ForEach(Action<T> action)
    {
        lock (list)
        {
            list.ForEach(action);
        }
    }
        
    /// <summary>返回一个用来迭代集合的 enumerator 对象</summary>
    public new Enumerator GetEnumerator()
    {
        Enumerator temp;
        lock (list)
        {
            temp = list.GetEnumerator();
        }

        return temp;
    }

    /// <summary>源中创建的一个元素范围的浅表副本</summary>
    /// <returns>一个元素范围的浅表副本</returns>
    /// <param name="count">元素范围数量</param>
    /// <param name="index">从零开始ide多赢，指定范围的开始</param>
    /// <exception cref="System.ArgumentOutOfRangeException">index 小于 0. 或者count 小于 0.</exception>
    /// <exception cref="System.ArgumentException">index和count不代表列表中的元素的有效范围</exception>
    public new CList<T> GetRange(int index, int count)
    {
        CList<T> list;
        lock (this.list)
        {
            list = new CList<T>(this.list.GetRange(index, count));
        }

        return list;
    }

    /// <summary>
    /// 查询指定的对象并返回从零开始的，第一个出现在整个集合中的元素索引。
    /// </summary>
    /// <returns>如果查询到对象，返回从零开始的，第一个出现在整个集合中的元素索引；否则为 –1.</returns>
    /// <param name="item">待查询的对象元素，如果值为引用类型则可以为null.</param>
    public new int IndexOf(T item)
    {
        int temp;

        lock (list)
        {
            temp = list.IndexOf(item);
        }

        return temp;
    }

    /// <summary>
    /// 从开始索引index 到结束查询指定的对象并返回从零开始的，第一个出现在整个集合中的元素索引。
    /// </summary>
    /// <returns>如果查询到对象，返回从零开始的，第一个出现在整个集合中的元素索引；否则为 –1.</returns>
    /// <param name="item">待查询的对象元素，如果值为引用类型则可以为null.</param>
    /// <param name="index">从零开始的索引</param>
    /// <exception cref="System.ArgumentOutOfRangeException">index 在列表集合有效索引之外.</exception>
    public new int IndexOf(T item, int index)
    {
        int temp;

        lock (list)
        {
            temp = list.IndexOf(item, index);
        }

        return temp;
    }
        
    /// <summary>
    /// 从开始索引index 和指定数量中查询指定的对象并返回从零开始的，第一个出现在整个集合中的元素索引。
    /// </summary>
    /// <returns>如果查询到对象，返回从零开始的，第一个出现在整个集合中的元素索引；否则为 –1.</returns>
    /// <param name="item">待查询的对象元素，如果值为引用类型则可以为null.</param>
    /// <param name="index">从零开始的索引</param>
    /// <param name="count">查询部分中的元素数量</param>
    /// <exception cref="System.ArgumentOutOfRangeException">index 在列表集合有效索引之外.</exception>
    public new int IndexOf(T item, int index, int count)
    {
        int temp;

        lock (list)
        {
            temp = list.IndexOf(item, index, count);
        }

        return temp;
    }
        
    /// <summary>
    /// 插入一个元素到列表中，并指定索引位置。</summary>
    /// <param name="index">从0开始的索引，确定插入的位置</param>
    /// <param name="item">待插入的元素，如果为引用类型，可以为空</param>
    /// <exception cref="System.ArgumentOutOfRangeException">index 小于 0.或者 index 列表的数量.</exception>
    public new void Insert(int index, T item)
    {
        lock (list)
        {
            list.Insert(index, item);
        }
    }
        
    /// <summary>插入一个集合的元素到列表中，并指定插入的索引位置。</summary>
    /// <param name="collection">待插入列表中元素集合，集合collection本身不能为空</param>
    /// <param name="index">从0开始的索引，确定插入的位置</param>
    /// <exception cref="System.ArgumentOutOfRangeException">index 小于 0.或者 index 列表的数量.</exception>
    /// <exception cref="System.ArgumentNullException">collection 为 null.</exception>
    public new void InsertRange(int index, IEnumerable<T> collection)
    {
        lock (list)
        {
            list.InsertRange(index, collection);
        }
    }
                
    /// <summary>
    /// 查询指定的对象，返回对象最后出现在整个集合中的索引。
    /// </summary>
    /// <returns>如果查询到元素，返回从零开始，最后再整个集合总出现的索引位置，否则返回 –1.</returns>
    /// <param name="item">待查询的对象</param>
    public new int LastIndexOf(T item)
    {
        int flag;
        lock (list)
        {
            flag = list.LastIndexOf(item);
        }

        return flag;
    }
        
    /// <summary>
    /// 查询指定的对象，返回对象最后出现在整个集合中的索引，查询范围从集合开始到指定的索引index。
    /// </summary>
    /// <returns>如果查询到元素，返回从零开始，最后再整个集合总出现的索引位置，否则返回 –1.</returns>
    /// <param name="item">待查询的对象</param>
    /// <param name="index">从零开始的索引，向后查询.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">index 在列表集合有效索引之外</exception>
    public new int LastIndexOf(T item, int index)
    {
        int flag;
        lock (list)
        {
            flag = list.LastIndexOf(item, index);
        }

        return flag;
    }

    /// <summary>
    /// 查询指定的对象，返回对象最后出现在整个集合中的索引，查询范围指定具体的查询数量，并以index索引位置结束。
    /// </summary>
    /// <returns>如果查询到元素，返回从零开始，最后再整个集合总出现的索引位置，否则返回 –1</returns>
    /// <param name="item">待查询的对象元素，如果值为引用类型则可以为null.</param>
    /// <param name="index">从零开始的索引</param>
    /// <param name="count">查询部分中的元素数量</param>
    /// <exception cref="System.ArgumentOutOfRangeException">index 在列表集合有效索引之外</exception>
    public new int LastIndexOf(T item, int index, int count)
    {
        int flag;
        lock (list)
        {
            flag = list.LastIndexOf(item, index, count);
        }

        return flag;
    }

    /// <summary>移除第一个在列表中出现的元素.</summary>
    /// <returns>如果顺利移除，返回true，否则返回false，如果集合中没有找到指定的元素也返回false。</returns>
    /// <param name="item">待移除的元素</param>
    public new bool Remove(T item)
    {
        bool flag;
        lock (list)
        {
            flag = list.Remove(item);
        }

        return flag;
    }

    /// <summary>
    /// 移除所有符合指定条件的元素。
    /// </summary>
    /// <returns>移除列表集合中的元素数量.</returns>
    /// <param name="match">定义元素的条件用来移除的委托</param>
    /// <exception cref="System.ArgumentNullException">match 为 null.</exception>
    public new int RemoveAll(Predicate<T> match)
    {
        int flag;
        lock (list)
        {
            flag = list.RemoveAll(match);
        }

        return flag;

    }
        
    /// <summary>
    /// 移除指定位置的元素对象。
    /// </summary>
    /// <param name="index">从零开始，待移除的元素索引.</param>
    /// <exception cref="System.ArgumentOutOfRangeException">index 小于 0. 或者index 等于或大于列表的数量。</exception>
    public new void RemoveAt(int index)
    {
        lock (list)
        {
            list.RemoveAt(index);
        }
    }
        
    /// <summary>移除列表中指定范围的元素</summary>
    /// <param name="index">从零开始，待移除的元素索引的开始位置.</param>
    /// <param name="count">待移除的元素数量1</param>
    /// <exception cref="System.ArgumentOutOfRangeException">index 小于 0. 或者index 等于或大于列表的数量。</exception>
    /// <exception cref="System.ArgumentException">index和count不代表列表中的元素的有效范围</exception>
    public new void RemoveRange(int index, int count)
    {
        lock (list)
        {
            list.RemoveRange(index, count);
        }

    }

    /// <summary>反转在整个列表元素的顺序</summary>
    public new void Reverse()
    {
        lock (list)
        {
            list.Reverse();
        }
    }

    /// <summary>在指定的范围反转元素的顺序。</summary>
    /// <param name="index">从零开始，待反转的元素索引的开始位置.</param>
    /// <param name="count">待反转的元素数量</param>
    /// <exception cref="System.ArgumentOutOfRangeException">index 小于 0. 或者index 等于或大于列表的数量。</exception>
    /// <exception cref="System.ArgumentException">index和count不代表列表中的元素的有效范围</exception>
    public new void Reverse(int index, int count)
    {
        lock (list)
        {
            list.Reverse(index, count);
        }
    }

    /// <summary>
    /// 使用默认的comparer来排序整个列表
    /// </summary>
    /// <exception cref="System.InvalidOperationException">Tkey类型，它默认的 comparer <see cref="P:System.Collections.Generic.Comparer&lt;T&gt;.Default"></see> 没有找到实现 <see cref="System.IComparable&lt;T&gt;"></see> 泛型接口或者 <see cref="System.IComparable"></see> 接口.</exception>
    public new void Sort()
    {
        lock (list)
        {
            list.Sort();
        }
    }

    /// <summary>
    /// 使用指定的comparer来排序整个列表
    /// </summary>
    /// <param name="comparer">元素的比较器，或者null的时候，使用默认比较器<see cref="P:System.Collections.Generic.Comparer&lt;T&gt;.Default"></see>.</param>
    /// <exception cref="System.ArgumentException"> comparer在排序的时候，它的实现引发错误。例如，comparer 可能在比较一个元素和它本身的时候，没有返回0.</exception>
    /// <exception cref="System.InvalidOperationException">comparer为null，或者Tkey类型，它默认的 comparer <see cref="P:System.Collections.Generic.Comparer&lt;T&gt;.Default"></see> 没有找到实现 <see cref="System.IComparable&lt;T&gt;"></see> 泛型接口或者 <see cref="System.IComparable"></see> 接口.</exception>
    public new void Sort(IComparer<T> comparer)
    {
        lock (list)
        {
            list.Sort(comparer);
        }
    }
        
    /// <summary>
    /// 使用指定的<see cref="System.Comparison&lt;T&gt;"></see>来排序整个列表
    /// </summary>
    /// <param name="comparison">当比较元素的时候，使用的 <see cref="System.Comparison&lt;T&gt;"></see> 对象</param>
    /// <exception cref="System.ArgumentException"> comparison在排序的时候，它的实现引发错误。例如，comparison 可能在比较一个元素和它本身的时候，没有返回0.</exception>
    /// <exception cref="System.InvalidOperationException">comparison为null，或者Tkey类型，它默认的 comparison  <see cref="System.Comparison&lt;T&gt;"></see> 没有找到实现 <see cref="System.IComparable&lt;T&gt;"></see> 泛型接口或者 <see cref="System.IComparable"></see> 接口.</exception>
    public new void Sort(Comparison<T> comparison)
    {
        lock (list)
        {
            list.Sort(comparison);
        }
    }

    /// <summary>
    /// 使用指定的comparer来排序整个列表
    /// </summary>
    /// <param name="index">排序的范围从零开始的起始索引。</param>
    /// <param name="count">排序范围的数量</param>
    /// <param name="comparer">元素的比较器，或者null的时候，使用默认比较器<see cref="P:System.Collections.Generic.Comparer&lt;T&gt;.Default"></see>.</param>
    /// <exception cref="System.ArgumentException"> comparer在排序的时候，它的实现引发错误。例如，comparer 可能在比较一个元素和它本身的时候，没有返回0.</exception>
    /// <exception cref="System.InvalidOperationException">comparer为null，或者Tkey类型，它默认的 comparer <see cref="P:System.Collections.Generic.Comparer&lt;T&gt;.Default"></see> 没有找到实现 <see cref="System.IComparable&lt;T&gt;"></see> 泛型接口或者 <see cref="System.IComparable"></see> 接口.</exception>
    /// <exception cref="System.ArgumentOutOfRangeException">index 小于 0. 或count 小于 0.</exception>
    public new void Sort(int index, int count, IComparer<T> comparer)
    {
        lock (list)
        {
            list.Sort(index, count, comparer);
        }
    }

    /// <summary>复制列表元素到一个新的数组中</summary>
    public new T[] ToArray()
    {
        T[] localArray1 = new T[Count];
        lock (list)
        {
            localArray1 = list.ToArray();
        }

        return localArray1;
    }
        
    /// <summary>
    /// 如果这个数字是小于一个阈值，设置列表的容量为实际元素数量。
    /// </summary>
    public new void TrimExcess()
    {
        lock (list)
        {
            list.TrimExcess();
        }
    }
        
    /// <summary>
    /// 判断在列表中所有的元素是否符合定义条件。
    /// </summary>
    /// <returns>
    /// 如果列表所有元素符合条件，则返回true，否则返回false，如果列表没有元素，返回true。
    /// </returns>
    /// <param name="match">对元素的检查条件的委托定义</param>
    /// <exception cref="System.ArgumentNullException">match 为null</exception>
    public new bool TrueForAll(Predicate<T> match)
    {
        bool flag;
        lock (list)
        {
            flag = list.TrueForAll(match);
        }
        return flag;
    }

    /// <summary>获取或设置没有调整的内部数据结构可容纳的元素的总数。</summary>
    /// <returns>对象 <see cref="System.Collections.Generic.List&lt;T&gt;"></see>包含的元素数量</returns>
    public new int Capacity
    {
        get
        {
            int temp;
            lock (list)
            {
                temp = list.Capacity;
            }
            return temp;
        }
        set
        {
            lock (list)
            {
                list.Capacity = value;
            }

        }
    }

    /// <summary>
    /// 获取集合中包含元素的数量
    /// </summary>
    /// <value></value>
    /// <returns>集合中所含元素的数目</returns>
    public new int Count
    {
        get
        {
            lock (list)
            {
                return list.Count;
            }
        }
    }

    /// <summary>获取或设置指定索引处的元素。</summary>
    /// <returns>在指定索引的元素。</returns>
    /// <param name="index">获取或设置的元素从零开始的索引</param>
    /// <exception cref="System.ArgumentOutOfRangeException">index 小于 0. 或者index 等于或大于列表的数量。</exception>
    public new T this[int index]
    {
        get
        {
            lock (list)
            {
                return list[index];
            }
        }
        set
        {
            lock (list)
            {
                list[index] = value;
            }
        }
    }

}