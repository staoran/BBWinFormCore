using System.Collections;
using System.Reflection;
using BB.Tools.Format;

namespace BB.Tools.Extension;

public static class HashtableExtension
{
    /// <summary>
    /// Hashtable转object实体对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static T ToObject<T>(this Hashtable source)
    {
        var obj = Activator.CreateInstance<T>();

        PropertyInfo[] ps = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        foreach (PropertyInfo p in ps)
        {
            if (!source.ContainsKey(p.Name)) continue;
            object tv = source[p.Name];

            Type propertyType = p.PropertyType;
            string propertyFullName = propertyType.FullName ?? string.Empty;
            bool isNullable = propertyFullName.Contains("System.Nullable");
            if (p.PropertyType.IsArray)//数组类型,单独处理
            {
                p.SetValue(obj, tv, null);
            }
            else
            {
                if (string.IsNullOrEmpty(tv.ToString()))//空值
                    tv = p.PropertyType.IsValueType ? Activator.CreateInstance(p.PropertyType) : null;//值类型
                else
                {
                    if (propertyFullName.Contains("Int32"))
                    {
                        tv = isNullable ? tv.ObjToIntNull() : tv.ObjToInt();
                    }
                    else if (propertyFullName.Contains("Int64"))
                    {
                        tv = isNullable ? tv.ObjToLongNull() : tv.ObjToLong();
                    }
                    else if (propertyFullName.Contains("Int16"))
                    {
                        tv = isNullable ? tv.ObjToShortNull() : tv.ObjToShort();
                    }
                    else if (propertyFullName.Contains("Decimal"))
                    {
                        tv = isNullable ? tv.ObjToDecimalNull() : tv.ObjToDecimal();
                    }
                    else if (propertyFullName.Contains("Double"))
                    {
                        tv = tv.ObjToDouble();
                    }
                    else if (propertyFullName.Contains("DateTime"))
                    {
                        tv = tv.ObjToDateNull();
                    }
                    else if (propertyFullName.Contains("Boolean"))
                    {
                        tv = tv.ObjToBool();
                    }
                    else if (propertyFullName.Contains("Guid"))
                    {
                        tv = isNullable ? tv.ObjToGuidNull() : tv.ObjToGuid();
                    }
                    else if (propertyFullName.Contains("Long"))
                    {
                        tv = isNullable ? tv.ObjToLongNull() : tv.ObjToLong();
                    }
                    else
                    {
                        tv = tv.ObjToStr();
                    }
                }

                p.SetValue(obj, tv, null);
            }
        }
   
        return obj;
    }

    /// <summary>
    /// 实体对象Object转HashTable
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Hashtable ToHashtable(object obj)
    {
        var ht = new Hashtable();
        PropertyInfo[] pis = obj.GetType().GetProperties();
        foreach (PropertyInfo t in pis)
        {
            //if (pis[i].Name != PrimaryKey)
            {
                object objValue = t.GetValue(obj, null);
                objValue ??= DBNull.Value;

                if (!ht.ContainsKey(t.Name))
                {
                    ht.Add(t.Name, objValue);
                }                    
            }
        }
        return ht;
        var hash = new Hashtable();
   
        PropertyInfo[] ps = obj.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
        foreach (PropertyInfo p in ps)
        {
            hash.Add(p.Name, p.GetValue(obj));
        }
   
        return hash;
    }

    /// <summary>
    /// 将 hashtable 转换为一个字典
    /// </summary>
    /// <param name="value">对象</param>
    /// <returns>对象属性字典</returns>
    public static Dictionary<K,V> ToDictionary<K,V>(this Hashtable value)
    {
        return value.Cast<DictionaryEntry>()
            .ToDictionary(d => (K)d.Key, d => (V)d.Value);
    }
}