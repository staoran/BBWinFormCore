using System.Text;

namespace BB.Tools.Extension;

/// <summary>
/// 可枚举对象扩展
/// </summary>
public static class EnumerableExtension
{

    /// <summary>
    /// 将可枚举对象值转换为以指定符号分隔字符串连接的字符串
    /// </summary>
    /// <param name="val">可枚举对象</param>
    /// <param name="separator">连接符号</param>
    /// <param name="format">转换格式</param>
    /// <returns>字符串</returns>
    public static string Splice<T>(this IEnumerable<T> val, string separator, Func<T, string> format)
    {
        StringBuilder s = new StringBuilder();
        if (val == null) return string.Empty;
        foreach (T v in val)
        {
            s.AppendFormat(format(v), v.ToString()).Append(separator);
        }

        if (s.Length > 0)
            s.Remove(s.Length - separator.Length, separator.Length);
        return s.ToString();
    }


    /// <summary>
    /// 将可枚举对象值转换为以指定符号分隔字符串连接的字符串
    /// </summary>
    /// <param name="val">可枚举对象</param>
    /// <param name="separator">连接符号</param>
    /// <param name="format">转换格式</param>
    /// <returns>字符串</returns>
    public static string Splice<T>(this IEnumerable<T> val, string separator, string format)
    {
        return Splice(val, separator, o => string.Format(format, o));
    }


    /// <summary>
    /// 将可枚举对象值转换为以指定符号分隔字符串连接的字符串
    /// </summary>
    /// <param name="val">可枚举对象</param>
    /// <param name="separator">连接符号</param>
    /// <returns>字符串</returns>
    public static string Splice<T>(this IEnumerable<T> val, string separator)
    {
        return Splice(val, separator, "{0}");
    }


    /// <summary>
    /// 将可枚举对象值转换为以逗号分隔字符串连接的字符串
    /// </summary>
    /// <param name="val">可枚举对象</param>
    /// <returns>字符串</returns>
    public static string Splice<T>(this IEnumerable<T> val)
    {
        return Splice(val, ",", "{0}");
    }

    /// <summary>
    /// 可枚举类型迭代函数
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="val">枚举</param>
    /// <param name="action">迭代函数主体，第一个参数为迭代项，第二个参数为索引号</param>
    public static void ForEach<T>(this IEnumerable<T> val, Action<T, int> action)
    {
        if (val == null) throw new ArgumentNullException();
        if (action == null) throw new ArgumentNullException();
        var index = 0;
        foreach (var o in val)
        {
            action(o, index++);
        }
    }

    /// <summary>
    /// 可枚举类型迭代函数
    /// </summary>
    /// <typeparam name="T">枚举类型</typeparam>
    /// <param name="val">枚举</param>
    /// <param name="action">迭代函数主体,第一个参数为迭代项</param>
    public static void ForEach<T>(this IEnumerable<T> val, Action<T> action)
    {
        ForEach(val, (o, i) => action(o));
    }

    /// <summary>
    /// Alternates the specified first.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <param name="first">The first.</param>
    /// <param name="second">The second.</param>
    /// <returns></returns>
    public static IEnumerable<TSource> Alternate<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
    {
        using (IEnumerator<TSource> e1 = first.GetEnumerator())
        using (IEnumerator<TSource> e2 = second.GetEnumerator())
        {
            while (e1.MoveNext() && e2.MoveNext())
            {
                yield return e1.Current;
                yield return e2.Current;
            }
        }
    }

    /// <summary>
    /// 附加指定的源
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="element">The element.</param>
    /// <returns>IEnumerable<TSource></returns>
    public static IEnumerable<TSource> Append<TSource>(this IEnumerable<TSource> source, TSource element)
    {
        using (IEnumerator<TSource> e1 = source.GetEnumerator())
        {
            while (e1.MoveNext())
            {
                yield return e1.Current;
            }
        }

        yield return element;
    }

    /// <summary>
    /// 确定是否[包含] [指定的源]。
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="value">The value.</param>
    /// <param name="selector">The selector.</param>
    /// <returns>
    ///     <c>true</c> if [contains] [the specified source]; otherwise, <c>false</c>.
    /// </returns>
    public static bool Contains<TSource, TResult>(this IEnumerable<TSource> source, TResult value,
        Func<TSource, TResult> selector)
    {
        foreach (TSource sourceItem in source)
        {
            TResult sourceValue = selector(sourceItem);
            if (sourceValue.Equals(value))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Prepends the specified source.
    /// </summary>
    /// <typeparam name="TSource">The type of the source.</typeparam>
    /// <param name="source">The source.</param>
    /// <param name="element">The element.</param>
    /// <returns>IEnumerable<TSource></returns>
    public static IEnumerable<TSource> Prepend<TSource>(this IEnumerable<TSource> source, TSource element)
    {
        yield return element;

        using (IEnumerator<TSource> e1 = source.GetEnumerator())
        {
            while (e1.MoveNext())
            {
                yield return e1.Current;
            }
        }
    }

    /// <summary>根据条件成立再构建 Where 查询</summary>
    /// <typeparam name="TSource">泛型类型</typeparam>
    /// <param name="sources">集合对象</param>
    /// <param name="condition">布尔条件</param>
    /// <param name="expression">表达式</param>
    /// <returns>新的集合对象</returns>
    public static IEnumerable<TSource> Where<TSource>(
        this IEnumerable<TSource> sources,
        bool condition,
        Func<TSource, bool> expression)
    {
        return !condition ? sources : sources.Where(expression);
    }
}