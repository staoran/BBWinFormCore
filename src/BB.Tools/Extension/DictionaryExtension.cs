using System.Xml.Linq;
using Furion.ClayObject.Extensions;

namespace BB.Tools.Extension;

/// <summary>
/// 字典扩展类
/// </summary>
public static class DictionaryExtension
{

    /// <summary>
    /// 获取字典值
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <typeparam name="TReturn">获取字典值的返回类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="key">获取的键值</param>
    /// <param name="defaultValue">获取失败的默认返回值</param>
    /// <returns>返回的值</returns>
    public static TReturn Get<TKey, TValue, TReturn>(this IDictionary<TKey, TValue> dict, TKey key,
        TReturn defaultValue)
    {
        if (dict == null || key == null) return defaultValue;
        TValue value;
        if (dict.TryGetValue(key, out value))
        {
            return value.To(defaultValue);
        }

        return defaultValue;
    }

    /// <summary>
    /// 获取字典值
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <typeparam name="TReturn">获取字典值的返回类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="key">获取的键值</param>
    /// <returns>返回的值</returns>
    public static TReturn Get<TKey, TValue, TReturn>(this IDictionary<TKey, TValue> dict, TKey key)
    {
        return Get(dict, key, default(TReturn));
    }

    /// <summary>
    /// 获取字典值
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="key">获取的键值</param>
    /// <returns>返回的值</returns>
    public static TValue Get<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
    {
        return Get(dict, key, default(TValue));
    }

    /// <summary>
    /// 当键存在时更新键值，当键不存在则添加键值
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
    {
        if (dict == null) throw new ArgumentNullException();
        if (dict.ContainsKey(key))
        {
            dict[key] = value;
        }
        else
        {
            dict.Add(key, value);
        }
    }

    /// <summary>
    /// 当键存在时跳过，当键不存在则添加键值
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    public static void TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key, TValue value)
    {
        if (dict == null) throw new ArgumentNullException();
        if (!dict.ContainsKey(key))
        {
            dict.Add(key, value);
        }
    }

    /// <summary>
    /// 将字典序列化成Xml字符串
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="rootName">Xml根节点名称</param>
    /// <param name="itemName">Xml项节点名称</param>
    /// <returns>Xml字符串</returns>
    public static string Serialize<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dict, string rootName,
        string itemName)
    {
        if (dict == null) return string.Empty;
        XDocument doc = new XDocument();
        XElement root = new XElement(rootName);
        foreach (var item in dict)
        {
            var itemNode = new XElement(itemName,
                new XAttribute("name", item.Key),
                new XCData(item.Value == null ? string.Empty : item.Value.ToString())
            );
            root.Add(itemNode);
        }

        doc.Add(root);
        return doc.ToString();
    }

    /// <summary>
    /// 将字典序列化成Xml字符串，根节点名称为项目名称+s
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="itemName">Xml项节点名称</param>
    /// <returns>Xml字符串</returns>
    public static string Serialize<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dict, string itemName)
    {
        return Serialize(dict, string.Concat(itemName, "s"), itemName);
    }

    /// <summary>
    /// 将字典序列化成Xml字符串,根节点名称为params,项目节点为param
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <returns>Xml字符串</returns>
    public static string Serialize<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> dict)
    {
        return Serialize(dict, "param");
    }



    /// <summary>
    /// 从Xml字符串中读取字典填充
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="xml">xml字符串</param>
    /// <param name="rootName">Xml根节点名称</param>
    /// <param name="itemName">Xml项节点名称</param>
    /// <param name="overwrite">如果字典中已存在此键值，是否覆盖</param>
    /// <param name="predicate">字典项目检测条件，返回true则添加到字典中，返回false则忽略</param>
    private static void FillInternal<TKey, TValue>(this IDictionary<TKey, TValue?> dict, string xml, string rootName,
        string itemName, bool overwrite, Func<IDictionary<TKey, TValue>, TKey, TValue, bool>? predicate = null)
    {
        XDocument doc = XDocument.Parse(xml);
        XElement? root = doc.Element(rootName);
        if (root == null) return;
        foreach (var item in root.Elements(itemName))
        {

            var key = item.To(t => t.Attribute("name")?.Value, default(TKey));
            var value = item.To(t => t.Value, default(TValue));
            if (predicate != null && !predicate(dict, key, value)) continue;
            if (overwrite)
            {
                dict.AddOrUpdate(key, value);
            }
            else
            {
                dict.Add(key, value);
            }
        }
    }

    /// <summary>
    /// 从Xml字符串中读取字典填充
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="xml">xml字符串</param>
    /// <param name="rootName">Xml根节点名称</param>
    /// <param name="itemName">Xml项节点名称</param>
    /// <param name="overwrite">如果字典中已存在此键值，是否覆盖</param>
    /// <param name="predicate">字典项目检测条件，返回true则添加到字典中，返回false则忽略</param>
    /// <returns>填充完毕后的字典</returns>
    public static IDictionary<TKey, TValue?> Fill<TKey, TValue>(this IDictionary<TKey, TValue?> dict, string xml,
        string rootName, string itemName, bool overwrite,
        Func<IDictionary<TKey, TValue>, TKey, TValue, bool>? predicate = null)
    {
        if (dict == null) return dict;
        if (string.IsNullOrWhiteSpace(xml)) return dict;
        try
        {
            FillInternal(dict, xml, rootName, itemName, overwrite, predicate);
        }
        catch
        {
            //Do Nothing
        }

        return dict;
    }


    /// <summary>
    /// 从Xml字符串中读取字典填充
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="xml">xml字符串</param>
    /// <param name="rootName">Xml根节点名称</param>
    /// <param name="itemName">Xml项节点名称</param>
    /// <param name="overwrite">如果字典中已存在此键值，是否覆盖</param>
    /// <param name="predicate">字典项目检测条件，返回true则添加到字典中，返回false则忽略</param>
    /// <returns>填充完毕后的字典</returns>
    public static Dictionary<TKey, TValue>? Fill<TKey, TValue>(this Dictionary<TKey, TValue> dict, string xml,
        string rootName, string itemName, bool overwrite,
        Func<IDictionary<TKey, TValue>, TKey, TValue, bool>? predicate = null) where TKey : notnull
    {
        return Fill((IDictionary<TKey, TValue?>)dict, xml, rootName, itemName, overwrite, predicate) as
            Dictionary<TKey, TValue>;
    }


    /// <summary>
    /// 从Xml字符串中读取字典填充
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="xml">xml字符串</param>
    /// <param name="itemName">Xml项节点名称</param>
    /// <param name="overwrite">如果字典中已存在此键值，是否覆盖</param>
    /// <param name="predicate">字典项目检测条件，返回true则添加到字典中，返回false则忽略</param>
    /// <returns>填充完毕后的字典</returns>
    public static IDictionary<TKey, TValue?> Fill<TKey, TValue>(this IDictionary<TKey, TValue?> dict, string xml,
        string itemName, bool overwrite, Func<IDictionary<TKey, TValue>, TKey, TValue, bool>? predicate = null)
    {
        return Fill(dict, xml, string.Concat(itemName, "s"), itemName, overwrite, predicate);
    }

    /// <summary>
    /// 从Xml字符串中读取字典填充
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="xml">xml字符串</param>
    /// <param name="itemName">Xml项节点名称</param>
    /// <param name="overwrite">如果字典中已存在此键值，是否覆盖</param>
    /// <param name="predicate">字典项目检测条件，返回true则添加到字典中，返回false则忽略</param>
    /// <returns>填充完毕后的字典</returns>
    public static Dictionary<TKey, TValue>? Fill<TKey, TValue>(this Dictionary<TKey, TValue> dict, string xml,
        string itemName, bool overwrite, Func<IDictionary<TKey, TValue>, TKey, TValue, bool>? predicate = null) where TKey : notnull
    {
        return Fill((IDictionary<TKey, TValue?>)dict, xml, itemName, overwrite, predicate) as Dictionary<TKey, TValue>;
    }

    /// <summary>
    /// 从Xml字符串中读取字典填充
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="xml">xml字符串</param>
    /// <param name="itemName">Xml项节点名称</param>
    /// <param name="predicate">字典项目检测条件，返回true则添加到字典中，返回false则忽略</param>
    /// <returns>填充完毕后的字典</returns>
    public static IDictionary<TKey, TValue?> Fill<TKey, TValue>(this IDictionary<TKey, TValue?> dict, string xml,
        string itemName, Func<IDictionary<TKey, TValue>, TKey, TValue, bool>? predicate = null)
    {
        return Fill(dict, xml, string.Concat(itemName, "s"), itemName, true, predicate);
    }

    /// <summary>
    /// 从Xml字符串中读取字典填充
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="xml">xml字符串</param>
    /// <param name="itemName">Xml项节点名称</param>
    /// <param name="predicate">字典项目检测条件，返回true则添加到字典中，返回false则忽略</param>
    /// <returns>填充完毕后的字典</returns>
    public static Dictionary<TKey, TValue>? Fill<TKey, TValue>(this Dictionary<TKey, TValue> dict, string xml,
        string itemName, Func<IDictionary<TKey, TValue>, TKey, TValue, bool>? predicate = null) where TKey : notnull
    {
        return Fill((IDictionary<TKey, TValue?>)dict, xml, itemName, predicate) as Dictionary<TKey, TValue>;
    }

    /// <summary>
    /// 从Xml字符串中读取字典填充
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="xml">xml字符串</param>
    /// <param name="overwrite">如果字典中已存在此键值，是否覆盖</param>
    /// <param name="predicate">字典项目检测条件，返回true则添加到字典中，返回false则忽略</param>
    /// <returns>填充完毕后的字典</returns>
    public static IDictionary<TKey, TValue?> Fill<TKey, TValue>(this IDictionary<TKey, TValue?> dict, string xml,
        bool overwrite, Func<IDictionary<TKey, TValue>, TKey, TValue, bool>? predicate = null)
    {
        return Fill(dict, xml, "param", overwrite, predicate);
    }

    /// <summary>
    /// 从Xml字符串中读取字典填充
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="xml">xml字符串</param>
    /// <param name="overwrite">如果字典中已存在此键值，是否覆盖</param>
    /// <param name="predicate">字典项目检测条件，返回true则添加到字典中，返回false则忽略</param>
    /// <returns>填充完毕后的字典</returns>
    public static Dictionary<TKey, TValue>? Fill<TKey, TValue>(this Dictionary<TKey, TValue> dict, string xml,
        bool overwrite, Func<IDictionary<TKey, TValue>, TKey, TValue, bool>? predicate = null) where TKey : notnull
    {
        return Fill((IDictionary<TKey, TValue?>)dict, xml, overwrite, predicate) as Dictionary<TKey, TValue>;
    }

    /// <summary>
    /// 从Xml字符串中读取字典填充
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="xml">xml字符串</param>
    /// <param name="predicate">字典项目检测条件，返回true则添加到字典中，返回false则忽略</param>
    /// <returns>填充完毕后的字典</returns>
    public static IDictionary<TKey, TValue?> Fill<TKey, TValue>(this IDictionary<TKey, TValue?> dict, string xml,
        Func<IDictionary<TKey, TValue>, TKey, TValue, bool>? predicate = null)
    {
        return Fill(dict, xml, true, predicate);
    }


    /// <summary>
    /// 从Xml字符串中读取字典填充
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="xml">xml字符串</param>
    /// <param name="predicate">字典项目检测条件，返回true则添加到字典中，返回false则忽略</param>
    /// <returns>填充完毕后的字典</returns>
    public static Dictionary<TKey, TValue>? Fill<TKey, TValue>(this Dictionary<TKey, TValue> dict, string xml,
        Func<IDictionary<TKey, TValue>, TKey, TValue, bool>? predicate = null) where TKey : notnull
    {
        return Fill((IDictionary<TKey, TValue?>)dict, xml, predicate) as Dictionary<TKey, TValue>;
    }


    /// <summary>
    /// 将字典对象转换为指定符号分隔字符串连接的字符串
    /// </summary>
    /// <typeparam name="TKey">字典键类型</typeparam>
    /// <typeparam name="TValue">字典值类型</typeparam>
    /// <param name="dict">字典</param>
    /// <param name="itemSplit">字典项分隔符</param>
    /// <param name="keyValueSplit">字典名值对分隔符</param>
    /// <param name="reverse">是否反转字典名值对连接顺序（默认键连接值）</param>
    /// <returns>拼接后的字符串</returns>
    public static string Splice<TKey, TValue>(this IDictionary<TKey, TValue> dict, string itemSplit = "&",
        string keyValueSplit = "=", bool reverse = false)
    {
        if (dict == null) return string.Empty;
        var items = new string[dict.Count()];
        dict.ForEach((o, i) =>
        {
            items[i] = !reverse
                ? string.Concat(o.Key, keyValueSplit, o.Value)
                : string.Concat(o.Value, keyValueSplit, o.Key);
        });
        return items.Splice(itemSplit);
    }

    public static void Concat(this IDictionary<string, object> dict, bool overrideProperty, params object[] objects)
    {
        var dicts = new List<Dictionary<string, object>>(objects.Length);
        foreach (var o in objects)
        {
            var applyDict = DictionaryExtensions.ToDictionary(o) as Dictionary<string, object>;
            dicts.Add(applyDict);
        }

        dict.Concat(overrideProperty, dicts.ToArray());
    }

    public static void Concat<TKey, TValue>(this IDictionary<TKey, TValue> dict, bool overrideProperty,
        params IDictionary<TKey, TValue>[] contactDicts)
    {
        foreach (var o in contactDicts)
        {
            foreach (var item in o)
            {
                if (overrideProperty || !dict.ContainsKey(item.Key))
                {
                    dict.AddOrUpdate(item.Key, item.Value);
                }
            }
        }
    }

}