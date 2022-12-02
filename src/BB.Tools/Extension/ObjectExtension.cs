using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using BB.Tools.Format;
using Furion.ClayObject.Extensions;

namespace BB.Tools.Extension;

/// <summary>
/// 对象扩展类
/// </summary>
public static class ObjectExtension
{    
    public static bool IsNull(this object? obj)
    {
        return obj == null;
    }

    public static bool IsNotNull(this object? obj)
    {
        return obj != null;
    }
    
    /// <summary>
    /// 将整形转换为指定枚举类型的显示输出
    /// </summary>
    /// <typeparam name="TEnum">枚举类型</typeparam>
    /// <param name="value">枚举值</param>
    /// <param name="defaultText">未在枚举中找到此枚举值时显示的文本</param>
    /// <returns>枚举显示</returns>
    public static string ToDisplay<TEnum>(this object? value, string defaultText = "")
    {
        if (value == null) return defaultText;
        return EnumHelper.GetDisplayName<TEnum>(value.ToString(), defaultText);
    }

    /// <summary>
    /// 检查输入对象值是否为空，为空返回默认值，否则将对象转换为输入类型
    /// </summary>
    /// <typeparam name="TFrom">源类型</typeparam>
    /// <typeparam name="TTo">目标类型</typeparam>
    /// <param name="from">待转换值</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>返回值</returns>
    public static TTo To<TFrom, TTo>(this TFrom from, TTo defaultValue)
    {
        if (from.Equals(null)) return defaultValue;
        try
        {

            var type = typeof(TTo);

            //如目标类型为可空类型，则获取其基础类型作为转换目标类型
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                type = Nullable.GetUnderlyingType(type);
            }

            //如果目标类型为枚举，则使用枚举格式化方法
            if (type.IsEnum) return (TTo)Enum.Parse(type, from.ToString());
            return (TTo)Convert.ChangeType(from, type);
        }
        catch
        {
            return defaultValue;
        }
    }

    /// <summary>
    /// 检查输入对象值是否为空，为空返回default(TTo)，否则将对象转换为输入类型
    /// </summary>
    /// <typeparam name="TFrom">源类型</typeparam>
    /// <typeparam name="TTo">目标类型</typeparam>
    /// <param name="from">待转换值</param>
    /// <returns>返回值</returns>
    public static TTo To<TFrom, TTo>(this TFrom from)
    {
        return To(from, default(TTo));
    }

    /// <summary>
    /// 检查输入对象值是否为空，为空返回默认值，否则将对象转换为输入类型
    /// </summary>
    /// <typeparam name="TTo">类型</typeparam>
    /// <param name="from">待转换值</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>返回值</returns>
    public static TTo To<TTo>(this object from, TTo defaultValue)
    {
        return To<object, TTo>(from, defaultValue);
    }

    /// <summary>
    /// 检查输入对象值是否为空，为空返回默认值，否则将对象转换为输入类型
    /// </summary>
    /// <typeparam name="TTo">类型</typeparam>
    /// <param name="from">待转换值</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns>返回值</returns>
    public static TTo To<TTo>(this string from, TTo defaultValue)
    {
        return To<string, TTo>(from, defaultValue);
    }

    /// <summary>
    /// 检查输入对象值是否为空，为空返回default(TTo)，否则将对象转换为输入类型
    /// </summary>
    /// <typeparam name="TTo">类型</typeparam>
    /// <param name="from">待转换值</param>
    /// <returns>返回值</returns>
    public static TTo To<TTo>(this object from)
    {
        return To(from, default(TTo));
    }

    /// <summary>
    /// 检查输入字符串是否为空，为空返回default(TTo)，否则将字符串转换为输入类型
    /// </summary>
    /// <typeparam name="TTo">类型</typeparam>
    /// <param name="from">待转换字符串</param>
    /// <returns>返回值</returns>
    public static TTo To<TTo>(this string from)
    {
        return To(from, default(TTo));
    }

    /// <summary>
    /// 获取对象的属性值，并转换成目标类型，获取或者转换失败，返回默认值
    /// </summary>
    /// <typeparam name="TFrom">对象的类型</typeparam>
    /// <typeparam name="TProperty">对象属性类型</typeparam>
    /// <typeparam name="TTo">转换类型</typeparam>
    /// <param name="from">对象</param>
    /// <param name="getProperty">属性获取委托</param>
    /// <param name="defaultValue"></param>
    /// <returns>转换结果</returns>
    public static TTo To<TFrom, TProperty, TTo>(this TFrom from, Func<TFrom, TProperty> getProperty, TTo defaultValue)
    {
        if (from.Equals(null)) return defaultValue;
        TProperty v = getProperty(from);
        return To(v, defaultValue);
    }

    /// <summary>
    /// 获取对象的属性值，并转换成目标类型，获取或者转换失败，返回类型默认值
    /// </summary>
    /// <typeparam name="TFrom">对象的类型</typeparam>
    /// <typeparam name="TProperty">对象属性类型</typeparam>
    /// <typeparam name="TTo">转换类型</typeparam>
    /// <param name="from">对象</param>
    /// <param name="getProperty">属性获取委托</param>
    /// <returns>转换结果</returns>
    public static TTo To<TFrom, TProperty, TTo>(this TFrom from, Func<TFrom, TProperty> getProperty)
    {
        if (from.Equals(null)) return default(TTo);
        TProperty v = getProperty(from);
        return To(v, default(TTo));
    }

    /// <summary>
    /// 获取对象的属性值，并转换成目标类型，获取或者转换失败，返回类型默认值
    /// </summary>
    /// <typeparam name="TFrom">对象的类型</typeparam>
    /// <typeparam name="TTo">转换类型</typeparam>
    /// <param name="from">对象</param>
    /// <param name="getProperty">属性获取委托</param>
    /// <returns>转换结果</returns>
    public static TTo To<TFrom, TTo>(this TFrom from, Func<TFrom, object> getProperty)
    {
        return To<TFrom, object, TTo>(from, getProperty);
    }

    #region Furion

    /// <summary>
        /// 将 DateTimeOffset 转换成本地 DateTime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this DateTimeOffset dateTime)
        {
            if (dateTime.Offset.Equals(TimeSpan.Zero))
                return dateTime.UtcDateTime;
            if (dateTime.Offset.Equals(TimeZoneInfo.Local.GetUtcOffset(dateTime.DateTime)))
                return dateTime.ToLocalTime().DateTime;
            else
                return dateTime.DateTime;
        }

        /// <summary>
        /// 将 DateTimeOffset? 转换成本地 DateTime?
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime? ConvertToDateTime(this DateTimeOffset? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ConvertToDateTime() : null;
        }

        /// <summary>
        /// 将 DateTime 转换成 DateTimeOffset
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTimeOffset ConvertToDateTimeOffset(this DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
        }

        /// <summary>
        /// 将 DateTime? 转换成 DateTimeOffset?
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTimeOffset? ConvertToDateTimeOffset(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ConvertToDateTimeOffset() : null;
        }

        /// <summary>
        /// 判断是否是元组类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        internal static bool IsValueTuple(this Type type)
        {
            return type.ToString().StartsWith(typeof(ValueTuple).FullName);
        }

        /// <summary>
        /// 判断类型是否实现某个泛型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="generic">泛型类型</param>
        /// <returns>bool</returns>
        internal static bool HasImplementedRawGeneric(this Type type, Type generic)
        {
            // 检查接口类型
            var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
            if (isTheRawGenericType) return true;

            // 检查类型
            while (type != null && type != typeof(object))
            {
                isTheRawGenericType = IsTheRawGenericType(type);
                if (isTheRawGenericType) return true;
                type = type.BaseType;
            }

            return false;

            // 判断逻辑
            bool IsTheRawGenericType(Type type) => generic == (type.IsGenericType ? type.GetGenericTypeDefinition() : type);
        }

        /// <summary>
        /// 判断是否是匿名类型
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        internal static bool IsAnonymous(this object obj)
        {
            var type = obj.GetType();

            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                   && type.IsGenericType && type.Name.Contains("AnonymousType")
                   && (type.Name.StartsWith("<>") || type.Name.StartsWith("VB$"))
                   && type.Attributes.HasFlag(TypeAttributes.NotPublic);
        }

        /// <summary>
        /// 获取所有祖先类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static IEnumerable<Type> GetAncestorTypes(this Type type)
        {
            var ancestorTypes = new List<Type>();
            while (type != null && type != typeof(object))
            {
                if (IsNoObjectBaseType(type))
                {
                    var baseType = type.BaseType;
                    ancestorTypes.Add(baseType);
                    type = baseType;
                }
                else break;
            }

            return ancestorTypes;

            static bool IsNoObjectBaseType(Type type) => type.BaseType != typeof(object);
        }

        /// <summary>
        /// 将一个对象转换为指定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static T ChangeType<T>(this object obj)
        {
            return (T)ChangeType(obj, typeof(T));
        }

        /// <summary>
        /// 将一个对象转换为指定类型
        /// </summary>
        /// <param name="obj">待转换的对象</param>
        /// <param name="type">目标类型</param>
        /// <returns>转换后的对象</returns>
        internal static object ChangeType(this object obj, Type type)
        {
            if (type == null) return obj;
            if (type == typeof(string)) return obj?.ToString();
            if (type == typeof(Guid) && obj != null) return Guid.Parse(obj.ToString());
            if (obj == null) return type.IsValueType ? Activator.CreateInstance(type) : null;

            var underlyingType = Nullable.GetUnderlyingType(type);
            if (type.IsAssignableFrom(obj.GetType())) return obj;
            else if ((underlyingType ?? type).IsEnum)
            {
                if (underlyingType != null && string.IsNullOrWhiteSpace(obj.ToString())) return null;
                else return Enum.Parse(underlyingType ?? type, obj.ToString());
            }
            // 处理DateTime -> DateTimeOffset 类型
            else if (obj.GetType().Equals(typeof(DateTime)) && (underlyingType ?? type).Equals(typeof(DateTimeOffset)))
            {
                return ((DateTime)obj).ConvertToDateTimeOffset();
            }
            // 处理 DateTimeOffset -> DateTime 类型
            else if (obj.GetType().Equals(typeof(DateTimeOffset)) && (underlyingType ?? type).Equals(typeof(DateTime)))
            {
                return ((DateTimeOffset)obj).ConvertToDateTime();
            }
            else if (typeof(IConvertible).IsAssignableFrom(underlyingType ?? type))
            {
                try
                {
                    return Convert.ChangeType(obj, underlyingType ?? type, null);
                }
                catch
                {
                    return underlyingType == null ? Activator.CreateInstance(type) : null;
                }
            }
            else
            {
                var converter = TypeDescriptor.GetConverter(type);
                if (converter.CanConvertFrom(obj.GetType())) return converter.ConvertFrom(obj);

                var constructor = type.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    var o = constructor.Invoke(null);
                    var propertys = type.GetProperties();
                    var oldType = obj.GetType();

                    foreach (var property in propertys)
                    {
                        var p = oldType.GetProperty(property.Name);
                        if (property.CanWrite && p != null && p.CanRead)
                        {
                            property.SetValue(o, ChangeType(p.GetValue(obj, null), property.PropertyType), null);
                        }
                    }
                    return o;
                }
            }
            return obj;
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        internal static string Format(this string str, params object[] args)
        {
            return args == null || args.Length == 0 ? str : string.Format(str, args);
        }

        /// <summary>
        /// 切割骆驼命名式字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        internal static string[] SplitCamelCase(this string str)
        {
            if (str == null) return new string[0];

            if (string.IsNullOrWhiteSpace(str)) return new string[] { str };
            if (str.Length == 1) return new string[] { str };

            return Regex.Split(str, @"(?=\p{Lu}\p{Ll})|(?<=\p{Ll})(?=\p{Lu})")
                .Where(u => u.Length > 0)
                .ToArray();
        }

    #endregion
}