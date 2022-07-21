using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using BB.Tools.Extension;

namespace BB.Tools.Format;

/// <summary>
/// 枚举操作辅助类
/// </summary>
public static class EnumHelper
{
    /// <summary>
    /// 通过字符串获取枚举成员实例
    /// </summary>
    /// <typeparam name="T">枚举名,比如Enum1</typeparam>
    /// <param name="member">枚举成员的常量名或常量值,
    /// 范例:Enum1枚举有两个成员A=0,B=1,则传入"A"或"0"获取 Enum1.A 枚举类型</param>
    public static T GetInstance<T>(string member)
    {
        return ConvertHelper.ConvertTo<T>(Enum.Parse(typeof(T), member, true));
    }

    /// <summary>
    /// 获取枚举成员名称和成员值的键值对集合
    /// </summary>
    /// <typeparam name="T">枚举名,比如Enum1</typeparam>
    public static Dictionary<string, object> GetMemberKeyValue<T>()
    {
        Dictionary<string, object> dic = new Dictionary<string, object>();

        //获取枚举所有成员名称
        string[] memberNames = GetMemberNames<T>();

        //遍历枚举成员
        foreach (string memberName in memberNames)
        {
            dic.Add(memberName, GetMemberValue<T>(memberName));
        }


        //返回哈希表
        return dic;
    }

    /// <summary>
    /// 获取枚举所有成员名称
    /// </summary>
    /// <typeparam name="T">枚举名,比如Enum1</typeparam>
    public static string[] GetMemberNames<T>()
    {
        return Enum.GetNames(typeof(T));
    }

    /// <summary>
    /// 获取枚举成员的名称
    /// </summary>
    /// <typeparam name="T">枚举名,比如Enum1</typeparam>
    /// <param name="member">枚举成员实例或成员值,
    /// 范例:Enum1枚举有两个成员A=0,B=1,则传入Enum1.A或0,获取成员名称"A"</param>
    public static string GetMemberName<T>(object member)
    {
        //转成基础类型的成员值
        Type underlyingType = GetUnderlyingType(typeof(T));
        object memberValue = ConvertHelper.ConvertTo(member, underlyingType);

        //获取枚举成员的名称
        return Enum.GetName(typeof(T), memberValue);
    }

    /// <summary>
    /// 获取枚举所有成员值
    /// </summary>
    /// <typeparam name="T">枚举名,比如Enum1</typeparam>
    public static Array GetMemberValues<T>()
    {
        return Enum.GetValues(typeof(T));
    }

    /// <summary>
    /// 获取枚举成员的值
    /// </summary>
    /// <typeparam name="T">枚举名,比如Enum1</typeparam>
    /// <param name="memberName">枚举成员的常量名,
    /// 范例:Enum1枚举有两个成员A=0,B=1,则传入"A"获取0</param>
    public static object GetMemberValue<T>(string memberName)
    {
        //获取基础类型
        Type underlyingType = GetUnderlyingType(typeof(T));

        //获取枚举实例
        T instance = GetInstance<T>(memberName);

        //获取枚举成员的值
        return ConvertHelper.ConvertTo(instance, underlyingType);
    }

    /// <summary>
    /// 获取枚举的基础类型
    /// </summary>
    /// <param name="enumType">枚举类型</param>
    public static Type GetUnderlyingType(Type enumType)
    {
        //获取基础类型
        return Enum.GetUnderlyingType(enumType);
    }

    /// <summary>
    /// 检测枚举是否包含指定成员
    /// </summary>
    /// <typeparam name="T">枚举名,比如Enum1</typeparam>
    /// <param name="member">枚举成员名或成员值</param>
    public static bool IsDefined<T>(string member)
    {
        return Enum.IsDefined(typeof(T), member);
    }

    /// <summary>
    /// 返回指定枚举类型的指定值的描述
    /// </summary>
    /// <param name="t">枚举类型</param>
    /// <param name="v">枚举值</param>
    /// <returns></returns>
    public static string GetDescription(Type t, object v)
    {
        try
        {
            FieldInfo fi = t.GetField(GetName(t, v));
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (attributes.Length > 0) ? attributes[0].Description : GetName(t, v);
        }
        catch
        {
            return "UNKNOWN";
        }
    }

    /// <summary>
    /// 返回指定枚举类型的指定值的名称
    /// </summary>
    /// <param name="t">指定枚举类型</param>
    /// <param name="v">指定值</param>
    /// <returns></returns>
    private static string GetName(Type t, object v)
    {
        try
        {
            return Enum.GetName(t, v);
        }
        catch
        {
            return "UNKNOWN";
        }
    }

    /// <summary>
    /// 获取枚举类型的对应序号及描述名称
    /// </summary>
    /// <param name="t">枚举类型</param>
    /// <returns></returns>
    public static SortedList GetStatus(Type t)
    {
        SortedList list = new SortedList();

        Array a = Enum.GetValues(t);
        for (int i = 0; i < a.Length; i++)
        {
            string enumName = a.GetValue(i).ToString();
            int enumKey = (int)Enum.Parse(t, enumName);
            string enumDescription = GetDescription(t, enumKey);
            list.Add(enumKey, enumDescription);
        }

        return list;
    }

    //系统枚举字典
    static ConcurrentDictionary<Type, Dictionary<object, DisplayAttribute>> values =
        new ConcurrentDictionary<Type, Dictionary<object, DisplayAttribute>>();

    static ConcurrentDictionary<Assembly, List<Type>> cacheAssemblyTypes =
        new ConcurrentDictionary<Assembly, List<Type>>();

    /// <summary>
    /// 根据枚举值获取枚举显示信息
    /// </summary>
    /// <param name="enumType">枚举类型</param>
    /// <param name="enumField">枚举成员或枚举值或枚举名称</param>
    /// <param name="defaultText">默认值</param>
    /// <returns>枚举显示信息</returns>
    public static string GetDisplayName(Type enumType, object enumField, string defaultText = "")
    {
        var displays = GetDisplays(enumType);
        try
        {
            var enumItem = Enum.Parse(enumType, enumField.ToString());
            var display = displays[enumItem];
            return display.Name ?? display.ShortName;
        }
        catch (Exception)
        {
            return defaultText;
        }
    }

    /// <summary>
    /// 根据枚举值获取枚举描述信息
    /// </summary>
    /// <param name="enumType"></param>
    /// <param name="enumField"></param>
    /// <returns></returns>
    public static string GetDescription(Type enumType, object enumField, string defaultText = "")
    {
        string Description = string.Empty;
        try
        {
            var enumObj = Enum.Parse(enumType, enumField.ToString());
            FieldInfo fieldInfo = enumObj.GetType().GetField(enumObj.ToString());
            object[] attribArray = fieldInfo.GetCustomAttributes(false);
            if (attribArray.Length == 0)
                return Description;
            else
            {
                return (attribArray[0] as DescriptionAttribute).Description;
            }
        }
        catch (Exception)
        {
            return defaultText;
        }
    }

    /// <summary>
    /// 根据枚举值获取枚举显示信息
    /// </summary>
    /// <param name="enumField">枚举成员</param>
    /// <returns>枚举显示信息</returns>
    public static string GetDisplayName(object enumField)
    {
        return GetDisplayName(enumField.GetType(), enumField);
    }

    /// <summary>
    /// 根据枚举值获取枚举显示信息
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="value">枚举成员值</param>
    /// <param name="defaultText">未找到枚举值时的默认显示</param>
    /// <returns>枚举显示信息</returns>
    public static string GetDisplayName<TEnum>(object value, string defaultText = "")
    {
        return GetDisplayName(typeof(TEnum), value, defaultText);
    }

    /// <summary>
    /// 获取枚举类型的显示属性
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <returns>枚举和显示属性字典</returns>
    public static Dictionary<object, string> GetDisplayNames<TEnum>()
    {
        return GetDisplayNames(typeof(TEnum));
    }

    /// <summary>
    /// 获取枚举类型的显示属性
    /// </summary>
    /// <param name="enumType">枚举类型</param>
    /// <returns>枚举和显示属性字典</returns>
    public static Dictionary<object, string> GetDisplayNames(Type enumType)
    {
        return GetDisplays(enumType)
            .OrderBy(o => o.Value.GetOrder() ?? 0)
            .ToDictionary(o => o.Key, o => o.Value.Name ?? o.Value.ShortName);
    }

    /// <summary>
    /// 在程序集中搜索枚举类型，并转换成字典返回
    /// </summary>
    /// <param name="assemblies">程序集</param>
    /// <returns>返回信息</returns>
    public static Dictionary<Type, Dictionary<object, string>> GetDisplayNames(params Assembly[] assemblies)
    {
        var dirs = GetDisplays(assemblies);
        return dirs.ToDictionary(
            o => o.Key,
            o => o.Value.ToDictionary(
                x => x.Key,
                x => x.Value.Name ?? x.Value.ShortName
            )
        );
    }

    /// <summary>
    /// 获取枚举类型的显示属性
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <returns>枚举和显示属性字典</returns>
    public static Dictionary<object, DisplayAttribute> GetDisplays<TEnum>()
    {
        return GetDisplays(typeof(TEnum));
    }

    /// <summary>
    /// 在程序集中搜索枚举类型，并转换成字典返回
    /// </summary>
    /// <param name="assemblies">程序集</param>
    /// <returns>返回信息</returns>
    public static Dictionary<Type, Dictionary<object, DisplayAttribute>> GetDisplays(params Assembly[] assemblies)
    {
        var enumDicts = new Dictionary<Type, Dictionary<object, DisplayAttribute>>();
        if (assemblies == null) return enumDicts;
        foreach (var arr in assemblies)
        {
            if (cacheAssemblyTypes.ContainsKey(arr))
            {
                var types = cacheAssemblyTypes[arr];
                foreach (var o in types)
                {
                    enumDicts.Add(o, GetDisplays(o));
                }
            }
            else
            {
                var types = arr.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsEnum)
                    {
                        enumDicts.Add(type, GetDisplays(type));
                    }
                }
            }
        }

        return enumDicts;
    }

    /// <summary>
    /// 获取枚举类型的显示属性
    /// </summary>
    /// <param name="enumType">枚举类型</param>
    /// <returns>枚举和显示属性字典</returns>
    public static Dictionary<object, DisplayAttribute> GetDisplays(Type enumType)
    {
        if (!enumType.IsEnum) throw new Exception("非枚举类型");

        Dictionary<object, DisplayAttribute> result;
        if (values.TryGetValue(enumType, out result)) return result;
        result = new Dictionary<object, DisplayAttribute>();
        var names = Enum.GetNames(enumType);
        foreach (var name in names)
        {
            var enumItem = Enum.Parse(enumType, name);
            var displays = enumType.GetField(name).GetCustomAttributes(typeof(DisplayAttribute), true);
            if (displays == null || displays.FirstOrDefault() == null)
            {
                result.AddOrUpdate(enumItem, new DisplayAttribute()
                {
                    Name = name
                });
            }
            else
            {
                var display = (DisplayAttribute)displays.First();
                result.AddOrUpdate(enumItem, display);
            }
        }

        values.AddOrUpdate(enumType, result);
        return result;
    }

    /// <summary>
    /// 从枚举类型和它的特性读出并返回一个键值对
    /// </summary>
    /// <returns>键值对</returns>
    public static Dictionary<string, string> GetEnumDescValue<T>()
    {
        return GetEnumDescValue(typeof(T));
    }

    /// <summary>
    /// /// <summary>
    /// 从枚举类型和它的特性读出并返回一个键值对
    /// </summary>
    /// <param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>
    /// <returns>键值对</returns>
    public static Dictionary<string, string> GetEnumDescValue(Type enumType)
    {
        Dictionary<string, string> keyValues = new Dictionary<string, string>();
        Type typeDescription = typeof(DescriptionAttribute);
        FieldInfo[] fields = enumType.GetFields();
        string strText = string.Empty;
        string strValue = string.Empty;
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType.IsEnum)
            {
                strValue = ((int)enumType.InvokeMember(field.Name, BindingFlags.GetField, null, null, null)).ToString();
                object[] arr = field.GetCustomAttributes(typeDescription, true);
                if (arr.Length > 0)
                {
                    DescriptionAttribute aa = (DescriptionAttribute)arr[0];
                    strText = aa.Description;
                }
                else
                {
                    strText = field.Name;
                }

                keyValues.Add(strText, strValue);
            }
        }

        return keyValues;
    }

    /// <summary>
    /// /// <summary>
    /// 从枚举类型和它的特性读出并返回一个键值对
    /// </summary>
    /// <param name="enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>
    /// <returns>键值对</returns>
    public static Dictionary<string, string> GetEnumDescName(Type enumType)
    {
        Dictionary<string, string> keyValues = new Dictionary<string, string>();
        Type typeDescription = typeof(DescriptionAttribute);
        FieldInfo[] fields = enumType.GetFields();
        string strText = string.Empty;
        string strName = string.Empty;
        foreach (FieldInfo field in fields)
        {
            if (field.FieldType.IsEnum)
            {
                strName = field.Name;
                object[] arr = field.GetCustomAttributes(typeDescription, true);
                if (arr.Length > 0)
                {
                    DescriptionAttribute aa = (DescriptionAttribute)arr[0];
                    strText = aa.Description;
                }
                else
                {
                    strText = field.Name;
                }

                keyValues.Add(strText, strName);
            }
        }

        return keyValues;
    }

    /// <summary>
    /// 获取Enum的描述信息
    /// </summary>
    /// <param name="enumValue"></param>
    /// <returns></returns>
    public static string GetDescription(this Enum enumValue)
    {
        Type enumType = enumValue.GetType();
        while (!enumType.IsGenericType || enumType.GetGenericTypeDefinition() != typeof(Nullable<>) || enumType.GetGenericArguments().Length != 1 || !enumType.GetGenericArguments()[0].IsEnum)
        {
            enumType = enumType.GetGenericArguments()[0];
        }

        if (!enumType.IsEnum)
        {
            return string.Empty;
        }

        var name = enumValue.ToString();
        FieldInfo field = enumType.GetField(name);
        if (field == null)
        {
            return name;
        }

        object[] customAttributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
        if (customAttributes.Length > 0)
        {
            if (customAttributes[0] is DescriptionAttribute attribute) name = attribute.Description;
        }

        return (name ?? field.Name);
    }
}