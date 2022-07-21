using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Resources;
using System.Text;
using BB.Tools.Extension;
using BB.Tools.Format;

namespace BB.Tools.Others;

#region 反射操作辅助类，如获取或设置字段、属性的值等反射信息。

/// <summary>
/// 反射操作辅助类，如获取或设置字段、属性的值等反射信息。
/// </summary>
public static class ReflectionExtension
{
    #region 属性字段设置
    /// <summary>
    /// 绑定标识
    /// </summary>
    public static BindingFlags bf = BindingFlags.DeclaredOnly | BindingFlags.Public | 
                                    BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;

    /// <summary>
    /// 执行方法
    /// </summary>
    /// <param name="obj">对象实例</param>
    /// <param name="methodName">方法名称</param>
    /// <param name="args">参数</param>
    /// <returns></returns>
    public static object InvokeMethod(this object obj, string methodName, object[] args)
    {
        Type type = obj.GetType();
        MethodInfo method = type.GetMethod(methodName);
        return method.Invoke(obj, args);
    }

    /// <summary>
    /// 设置对象实例的字段值
    /// </summary>
    /// <param name="obj">对象实例</param>
    /// <param name="name">字段名称</param>
    /// <param name="value">字段值</param>
    public static void SetField(this object obj, string name, object value)
    {
        FieldInfo fi = obj.GetType().GetField(name, bf);
        if (fi != null)
        {
            fi.SetValue(obj, value);
        }
    }

    /// <summary>
    /// 获取对象实例的字段值
    /// </summary>
    /// <param name="obj">对象实例</param>
    /// <param name="name">字段名称</param>
    /// <returns></returns>
    public static object GetField(this object obj, string name)
    {
        object result = null;
        FieldInfo fi = obj.GetType().GetField(name, bf);
        if (fi != null)
        {
            result = fi.GetValue(obj);
        }
        return result;
    }

    /// <summary>
    /// 获取对象的常量或静态字段值
    /// </summary>
    /// <param name="type">对象类型</param>
    /// <param name="name">字段名称</param>
    /// <returns></returns>
    public static object GetField(Type type, string name)
    {
        object result = null;
        FieldInfo fi = type.GetField(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        if (fi != null)
        {
            result = fi.GetValue(null);
        }
        return result;
    }

    /// <summary>
    /// 获取对象实例的字段集合
    /// </summary>
    /// <param name="obj">对象实例</param>
    /// <returns></returns>
    public static FieldInfo[] GetFields(this object obj)
    {
        FieldInfo[] fieldInfos = obj.GetType().GetFields(bf);
        return fieldInfos;
    }

    /// <summary>
    /// 设置对象实例的属性值
    /// </summary>
    /// <param name="obj">对象实例</param>
    /// <param name="name">属性名称</param>
    /// <param name="value">属性值</param>
    public static void SetProperty(this object obj, string name, object value)
    {
        //PropertyInfo fieldInfo = obj.GetType().GetProperty(name, bf);
        //value = Convert.ChangeType(value, fieldInfo.PropertyType);
        //fieldInfo.SetValue(obj, value, null);

        //下面方法可以获取基类属性
        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
        {
            if (prop.Name == name)
            {
                value = Convert.ChangeType(value, prop.PropertyType);
                prop.SetValue(obj, value);
            }
        }
    }

    /// <summary>
    /// 获取对象实例的属性值
    /// </summary>
    /// <param name="obj">对象实例</param>
    /// <param name="name">属性名称</param>
    /// <returns></returns>
    public static object GetProperty(this object obj, string name)
    {
        //这个无法获取基类
        //PropertyInfo fieldInfo = obj.GetType().GetProperty(name, bf);
        //return fieldInfo.GetValue(obj, null);

        //下面方法可以获取基类属性
        object result = null;
        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
        {
            if(prop.Name == name)
            {
                result = prop.GetValue(obj);
            }
        }
        return result;
    }

    /// <summary>
    /// 根据对象实例的常量字符串获取指定属性的值
    /// </summary>
    /// <param name="obj">对象实例</param>
    /// <param name="constName">常量属性名称</param>
    /// <returns></returns>
    public static object GetValue(this object obj, string constName)
    {
        string constValue = GetField(obj, constName).ObjToStr();
        if (constValue.IsNullOrEmpty())
            return null;

        return GetProperty(obj, constValue);
    }

    /// <summary>
    /// 获取对象实例的主键值
    /// </summary>
    /// <param name="obj">对象实例</param>
    /// <returns></returns>
    public static object GetPrimaryKeyValue(this object obj)
    {
        return obj.GetValue("PrimaryKey");
    }

    /// <summary>
    /// 获取对象实例的外键值
    /// </summary>
    /// <param name="obj">对象实例</param>
    /// <returns></returns>
    public static object GetForeignKeyValue(this object obj)
    {
        return obj.GetValue("ForeignKey");
    }

    /// <summary>
    /// 获取对象实例的属性列表
    /// </summary>
    /// <param name="obj">对象实例</param>
    /// <returns></returns>
    public static PropertyInfo[] GetProperties(this object obj)
    {
        PropertyInfo[] propertyInfos = obj.GetType().GetProperties(bf);
        return propertyInfos;
    }

    /// <summary>
    /// 获取对象类型中拥有指定特性的属性列表
    /// </summary>
    /// <returns></returns>
    public static List<PropertyInfo> GetProperties<T, AT>() where T : class where AT : Attribute
    {
        PropertyInfo[] propertyInfos = typeof(T).GetProperties(bf);
        List<PropertyInfo> result = new List<PropertyInfo>();
        foreach (PropertyInfo propertyInfo in propertyInfos)
        {
            if (propertyInfo.GetCustomAttributes(typeof(AT), true).Length > 0)
            {
                result.Add(propertyInfo);
            }
        }

        return result;
    }

    /// <summary>
    /// 获取指定对象的属性名称列表
    /// </summary>
    /// <param name="obj">object对象</param>
    /// <returns></returns>
    public static List<string> GetPropertyNames(this object obj)
    {
        List<string> list = new();

        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
        {
            list.Add(prop.Name);
        }

        //Type type = obj.GetType();
        //string[] propertyNames = type.GetProperties().Select(p => p.Name).ToArray();
        //if (propertyNames != null)
        //{
        //    list.AddRange(propertyNames.ToArray());
        //}

        return list;
    }

    /// <summary>
    /// 获取指定类型的属性名称列表
    /// </summary>
    /// <param name="obj">object对象</param>
    /// <returns></returns>
    public static List<string> GetPropertyNames<T>() where T : class
    {
        List<string> list = new();

        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(typeof(T)))
        {
            list.Add(prop.Name);
        }

        return list;
    }

    /// <summary>
    /// 把object对象的属性反射获取到字典列表中
    /// </summary>
    /// <param name="obj">object对象</param>
    /// <returns></returns>
    public static Dictionary<string, object> GetPropertyDict(this object obj)
    {
        Dictionary<string, object> dict = new Dictionary<string, object>();
        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
        {
            object propValue = prop.GetValue(obj);
            if (!dict.ContainsKey(prop.Name))
            {
                dict.Add(prop.Name, propValue);
            }
        }
        return dict;
    }

    /// <summary>
    /// 把object对象的属性反射获取到字典列表中
    /// </summary>
    /// <param name="obj">object对象</param>
    /// <returns></returns>
    public static Dictionary<string, string> GetPropertyDict2(this object obj)
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(obj))
        {
            object propValue = prop.GetValue(obj);
            string value = (propValue != null) ? propValue.ToString() : "";
            if (!dict.ContainsKey(prop.Name))
            {
                dict.Add(prop.Name, value);
            }
        }
        return dict;
    }

    /// <summary>
    /// 获取属性名称，优先获取DisplayName
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <returns>属性名称</returns>
    public static Dictionary<string, string> GetPropertyDisplayNames<T>() where T : class
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        PropertyInfo[] properties = typeof(T).GetProperties();

        foreach (PropertyInfo prop in properties)
        {
            object[] attribute = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false);
            dict.Add(prop.Name, attribute.Length == 0 ? prop.Name : ((DisplayNameAttribute)attribute[0]).DisplayName);
        }

        return dict;
    }

    /// <summary>
    /// 将实体类属性转换为字典形式
    /// </summary>
    /// <typeparam name="T">泛型</typeparam>
    /// <param name="model">实体类</param>
    /// <returns>字典</returns>
    public static Dictionary<string, object> DictionaryFromType<T>(this T model) where T : class
    {
        PropertyInfo[] properties = typeof(T).GetProperties();
        Dictionary<string, object> dict = new Dictionary<string, object>();

        foreach (PropertyInfo prop in properties)
        {
            object[] attribute = prop.GetCustomAttributes(typeof(DisplayNameAttribute), false);
            string proName = attribute.Length == 0 ? prop.Name : ((DisplayNameAttribute)attribute[0]).DisplayName;
            object proValue = prop.GetValue(model, new object[] { });
            dict.Add(proName, proValue);
        }

        return dict;
    }

    /// <summary>
    /// 把对象的属性和值，输出一个键值的字符串，如A=1&B=test
    /// </summary>
    /// <param name="obj">实体对象</param>
    /// <param name="includeEmptyProperties">是否包含空白属性的键值</param>
    /// <returns></returns>
    public static string ToNameValuePairs(this object obj, bool includeEmptyProperties = true)
    {
        string result = "";

        foreach (PropertyDescriptor p in TypeDescriptor.GetProperties(obj))
        {
            var objVal = p.GetValue(obj);
            var value = objVal != null ? objVal.ToString() : null;

            if (string.IsNullOrEmpty(value))
            {
                if (includeEmptyProperties)
                {
                    if (!string.IsNullOrEmpty(result))
                    {
                        result += "&";
                    }

                    result += $"{p.Name}={value}";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(result))
                {
                    result += "&";
                }

                result += $"{p.Name}={value}";
            }
        }

        return result;
    }

    #endregion

    #region 获取枚举字段的Description

    /// <summary>
    /// 获取枚举字段的Description属性值
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>return description or value.ToString()</returns>
    public static string GetDescription(Enum value)
    {
        return GetDescription(value, null);
    }

    /// <summary>
    /// Get The Enum Field Description using Description Attribute and 
    /// objects to format the Description.
    /// </summary>
    /// <param name="value">Enum For Which description is required.</param>
    /// <param name="args">An Object array containing zero or more objects to format.</param>
    /// <returns>return null if DescriptionAttribute is not found or return type description</returns>
    public static string GetDescription(Enum value, params object[] args)
    {
        if (value == null)
        {
            throw new ArgumentNullException("value");
        }

        string text1;

        FieldInfo fi = value.GetType().GetField(value.ToString());

        DescriptionAttribute[] attributes =
            (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

        text1 = (attributes.Length > 0) ? attributes[0].Description : value.ToString();

        if ((args != null) && (args.Length > 0))
        {
            return string.Format(null, text1, args);
        }
        return text1;
    }

    /// <summary>
    ///	获取字段的Description属性值
    /// </summary>
    /// <param name="member">Specified Member for which Info is Required</param>
    /// <returns>return null if DescriptionAttribute is not found or return type description</returns>
    public static string GetDescription(MemberInfo member)
    {
        return GetDescription(member, null);
    }

    /// <summary>
    /// Get The Type Description using Description Attribute and 
    /// objects to format the Description.
    /// </summary>
    /// <param name="member"> Specified Member for which Info is Required</param>
    /// <param name="args">An Object array containing zero or more objects to format.</param>
    /// <returns>return <see cref="String.Empty"/> if DescriptionAttribute is 
    /// not found or return type description</returns>
    public static string GetDescription(MemberInfo member, params object[] args)
    {
        string text1;

        if (member == null)
        {
            throw new ArgumentNullException("member");
        }

        if (member.IsDefined(typeof(DescriptionAttribute), false))
        {
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])member.GetCustomAttributes(typeof(DescriptionAttribute), false);
            text1 = attributes[0].Description;
        }
        else
        {
            return String.Empty;
        }

        if ((args != null) && (args.Length > 0))
        {
            return String.Format(null, text1, args);
        }
        return text1;
    }

    #endregion

    #region 获取Attribute信息

    /// <summary>
    /// 获取指定成员的attributes内容
    /// </summary>
    /// <param name="member">指定成员信息</param>
    /// <param name="func">获取具体attributes内容的委托</param>
    /// <typeparam name="T">attributes类型</typeparam>
    /// <typeparam name="TResult">返回值类型</typeparam>
    /// <returns></returns>
    public static TResult GetAttribute<T, TResult>(this MemberInfo member, Func<T, TResult> func) where T : Attribute
    {
        var attribute = member.GetCustomAttribute<T>();
        return attribute != null ? func(attribute) : default;
    }
    
    /// <summary>
    /// 获取指定对象实例的attributes内容
    /// </summary>
    /// <param name="attributeType">The attribute Type for which the custom attributes are to be returned.</param>
    /// <param name="assembly">定义指定属性的程序集</param>
    /// <returns>Attribute as Object or null if not found.</returns>
    public static object GetAttribute(Type attributeType, Assembly assembly)
    {
        if (attributeType == null)
        {
            throw new ArgumentNullException("attributeType");
        }

        if (assembly == null)
        {
            throw new ArgumentNullException("assembly");
        }


        if (assembly.IsDefined(attributeType, false))
        {
            object[] attributes = assembly.GetCustomAttributes(attributeType, false);

            return attributes[0];
        }

        return null;
    }


    /// <summary>
    /// 获取指定对象实例的attributes内容
    /// </summary>
    /// <param name="attributeType">The attribute Type for which the custom attributes are to be returned.</param>
    /// <param name="type">定义指定属性的类型</param>
    /// <returns>Attribute as Object or null if not found.</returns>
    public static object GetAttribute(Type attributeType, MemberInfo type)
    {
        return GetAttribute(attributeType, type, false);
    }


    /// <summary>
    /// 获取类型指定的类型的指定对象属性，并带有搜索父项的选项
    /// </summary>
    /// <param name="attributeType">The attribute Type for which the custom attributes are to be returned.</param>
    /// <param name="type">the type on which the specified attribute is defined</param>
    /// <param name="searchParent">if set to <see langword="true"/> [search parent].</param>
    /// <returns>
    /// Attribute as Object or null if not found.
    /// </returns>
    public static object GetAttribute(Type attributeType, MemberInfo type, bool searchParent)
    {
        if (attributeType == null)
        {
            return null;
        }

        if (type == null)
        {
            return null;
        }

        if (!(attributeType.IsSubclassOf(typeof(Attribute))))
        {
            return null;
        }


        if (type.IsDefined(attributeType, searchParent))
        {
            object[] attributes = type.GetCustomAttributes(attributeType, searchParent);

            if (attributes.Length > 0)
            {
                return attributes[0];
            }
        }

        return null;
    }

    /// <summary>
    /// 获取类型指定的所有指定对象属性的集合
    /// </summary>
    /// <param name="attributeType">The attribute Type for which the custom attributes are to be returned.</param>
    /// <param name="type">the type on which the specified attribute is defined</param>
    /// <returns>Attribute as Object or null if not found.</returns>
    public static object[] GetAttributes(Type attributeType, MemberInfo type)
    {
        return GetAttributes(attributeType, type, false);
    }

    /// <summary>
    /// 获取类型指定的所有指定对象属性的集合，该类型由类型指定，带有 serach parent 选项
    /// </summary>
    /// <param name="attributeType">要为其返回自定义属性的属性类型。</param>
    /// <param name="type">定义指定属性的类型</param>
    /// <param name="searchParent">要为其返回自定义属性的属性类型。</param>
    /// <returns>
    /// Attribute as Object or null if not found.
    /// </returns>
    public static object[] GetAttributes(Type attributeType, MemberInfo type, bool searchParent)
    {
        if (type == null)
        {
            return null;
        }

        if (attributeType == null)
        {
            return null;
        }

        if (!(attributeType.IsSubclassOf(typeof(Attribute))))
        {
            return null;
        }


        if (type.IsDefined(attributeType, false))
        {
            return type.GetCustomAttributes(attributeType, searchParent);
        }

        return null;
    }

    #endregion

    #region 资源获取

    /// <summary>
    /// 根据资源名称获取图片资源流
    /// </summary>
    /// <param name="resourceName"></param>
    /// <returns></returns>
    public static Stream GetImageResource(string resourceName)
    {
        Assembly asm = Assembly.GetExecutingAssembly();
        return asm.GetManifestResourceStream(resourceName);
    }

    /// <summary>
    /// 获取程序集资源的位图资源
    /// </summary>
    /// <param name="assemblyType">程序集中的某一对象类型</param>
    /// <param name="resourceHolder">资源的根名称。例如，名为“MyResource.en-US.resources”的资源文件的根名称为“MyResource”。</param>
    /// <param name="imageName">资源项名称</param>
    public static Bitmap LoadBitmap(Type assemblyType, string resourceHolder, string imageName)
    {
        Assembly thisAssembly = Assembly.GetAssembly(assemblyType);
        ResourceManager rm = new ResourceManager(resourceHolder, thisAssembly);
        return (Bitmap)rm.GetObject(imageName);
    }

    /// <summary>
    ///  获取程序集资源的文本资源
    /// </summary>
    /// <param name="assemblyType">程序集中的某一对象类型</param>
    /// <param name="resName">资源项名称</param>
    /// <param name="resourceHolder">资源的根名称。例如，名为“MyResource.en-US.resources”的资源文件的根名称为“MyResource”。</param>
    public static string GetStringRes(Type assemblyType, string resName, string resourceHolder)
    {
        Assembly thisAssembly = Assembly.GetAssembly(assemblyType);
        ResourceManager rm = new ResourceManager(resourceHolder, thisAssembly);
        return rm.GetString(resName);
    }

    /// <summary>
    /// 获取程序集嵌入资源的文本形式
    /// </summary>
    /// <param name="assemblyType">程序集中的某一对象类型</param>
    /// <param name="charset">字符集编码</param>
    /// <param name="resName">嵌入资源相对路径</param>
    /// <returns>如没找到该资源则返回空字符</returns>
    public static string GetManifestString(Type assemblyType, string charset, string resName)
    {
        Assembly asm = Assembly.GetAssembly(assemblyType);
        Stream st = asm.GetManifestResourceStream(string.Concat(assemblyType.Namespace,
            ".", resName.Replace("/", ".")));
        if (st == null) { return ""; }
        int iLen = (int)st.Length;
        byte[] bytes = new byte[iLen];
        st.Read(bytes, 0, iLen);
        return (bytes != null) ? Encoding.GetEncoding(charset).GetString(bytes) : "";
    }

    #endregion

    #region 创建对应实例
    /// <summary>
    /// 创建对应实例
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns>对应实例</returns>
    public static object CreateInstance(string type)
    {
        Type tmp = null;
        Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
        for (int i = 0; i < assemblies.Length; i++)
        {
            tmp = assemblies[i].GetType(type);
            if (tmp != null)
            {
                return assemblies[i].CreateInstance(type);

            }
        }
        return null;
        //return Assembly.GetExecutingAssembly().CreateInstance(type);
    }

    /// <summary>
    /// 创建对应实例
    /// </summary>
    /// <param name="type">类型</param>
    /// <returns>对应实例</returns>
    public static object CreateInstance(Type type)
    {
        return CreateInstance(type.FullName);
    } 
    #endregion

    #region 对象属性复制

    /// <summary>
    /// 利用反射实现两个类的对象之间相同属性的值的复制
    /// </summary>
    /// <typeparam name="D">最终生成的新对象类型</typeparam>
    /// <typeparam name="S">传入的源数据对象类型</typeparam>
    /// <param name="s">待复制属性的源对象</param>
    /// <returns></returns>
    public static TD Mapper<TD, TS>(TS s)
    {
        TD d = Activator.CreateInstance<TD>();

        var types = s.GetType();//获得类型  
        var typed = typeof(TD);
        foreach (PropertyInfo sp in types.GetProperties())//获得类型的属性字段  
        {
            foreach (PropertyInfo dp in typed.GetProperties())
            {
                if (dp.Name == sp.Name)//判断属性名是否相同  
                {
                    dp.SetValue(d, sp.GetValue(s, null), null);//获得s对象属性的值复制给d对象的属性  
                }
            }
        }

        return d;
    }  
    #endregion
        
    /// <summary>
    /// 获取对象属性信息（组装成字符串输出）
    /// </summary>
    public static Dictionary<string,string> GetPropertyNameTypes(this object obj)
    {
        Dictionary<string, string> nameList = new Dictionary<string, string>();
        PropertyInfo[] propertyInfos = obj.GetType().GetProperties(bf);

        foreach (PropertyInfo property in propertyInfos)
        {
            nameList.Add(property.Name, property.PropertyType.FullName);
        }

        return nameList;
    }

    public static DataTable CreateTable(this object objSource)
    {
        DataTable table = null;
        IEnumerable objList = objSource as IEnumerable;
        if (objList == null)
            return null;

        foreach (object obj in objList)
        {
            if (table == null)
            {
                List<string> nameList = GetPropertyNames(obj);
                table = new DataTable("");
                DataColumn column;

                foreach (string name in nameList)
                {
                    column = new DataColumn();
                    column.DataType = Type.GetType("System.String");
                    column.ColumnName = name;
                    column.Caption = name;
                    table.Columns.Add(column);
                }
            }

            DataRow row = table.NewRow();
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties(bf);
            foreach (PropertyInfo property in propertyInfos)
            {
                row[property.Name] = property.GetValue(obj, null);                    
            }
            table.Rows.Add(row);
        }

        return table;
    }
}

#endregion
    
#region 属性取值设置值反射优化
    
/// <summary>
/// 静态成员访问类，优化反射
/// </summary>
public static class StaticDynamicMethodMemberAccessor
{
    /// <summary>
    /// 获取属性值
    /// </summary>
    /// <param name="p">属性对象</param>
    /// <param name="instance">实体对象</param>
    /// <param name="index">索引置空</param>
    /// <returns></returns>
    public static object GetValue2(this PropertyInfo p, object instance, object[] index = null)
    {
        var ma = new DynamicMethodMemberAccessor();
        return ma.GetValue(instance, p.Name);
    }

    /// <summary>
    /// 设置属性值
    /// </summary>
    /// <param name="p">属性对象</param>
    /// <param name="instance">实体对象</param>
    /// <param name="newValue">新值</param>
    /// <param name="index">索引置空</param>
    public static void SetValue2(this PropertyInfo p, object instance, object newValue, object[] index = null)
    {
        var ma = new DynamicMethodMemberAccessor();
        ma.SetValue(instance, p.Name, newValue);
    }
}

/// <summary>
/// Abstraction of the function of accessing member of a object at runtime.
/// </summary>
interface IMemberAccessor
{
    /// <summary>
    /// Get the member value of an object.
    /// </summary>
    /// <param name="instance">The object to get the member value from.</param>
    /// <param name="memberName">The member name, could be the name of a property of field. Must be public member.</param>
    /// <returns>The member value</returns>
    object GetValue(object instance, string memberName);

    /// <summary>
    /// Set the member value of an object.
    /// </summary>
    /// <param name="instance">The object to get the member value from.</param>
    /// <param name="memberName">The member name, could be the name of a property of field. Must be public member.</param>
    /// <param name="newValue">The new value of the property for the object instance.</param>
    void SetValue(object instance, string memberName, object newValue);
}

/// <summary>
/// 动态函数调用类，优化反射
/// </summary>
class DynamicMethodMemberAccessor : IMemberAccessor
{
    private static Dictionary<Type, IMemberAccessor> classAccessors = new Dictionary<Type, IMemberAccessor>();

    /// <summary>
    /// 获取属性值
    /// </summary>
    /// <param name="instance">实体对象</param>
    /// <param name="memberName">成员名</param>
    /// <returns></returns>
    public object GetValue(object instance, string memberName)
    {
        return FindClassAccessor(instance).GetValue(instance, memberName);
    }

    /// <summary>
    /// 设置属性值
    /// </summary>
    /// <param name="instance">实体对象</param>
    /// <param name="memberName">成员名</param>
    /// <param name="newValue">新值</param>
    /// <returns></returns>
    public void SetValue(object instance, string memberName, object newValue)
    {
        FindClassAccessor(instance).SetValue(instance, memberName, newValue);
    }

    /// <summary>
    /// 获取IMemberAccessor对象
    /// </summary>
    /// <param name="instance"></param>
    /// <returns></returns>
    private IMemberAccessor FindClassAccessor(object instance)
    {
        var typekey = instance.GetType();
        IMemberAccessor classAccessor;
        classAccessors.TryGetValue(typekey, out classAccessor);
        if (classAccessor == null && !classAccessors.ContainsKey(typekey))
        {
            classAccessor = Activator.CreateInstance(typeof(DynamicMethod<>).MakeGenericType(instance.GetType())) as IMemberAccessor;
            classAccessors.Add(typekey, classAccessor);
        }

        return classAccessor;
    }
}

/// <summary>
/// Dynamic方法类
/// </summary>
/// <typeparam name="T"></typeparam>
class DynamicMethod<T> : IMemberAccessor
{
    internal static Func<object, string, object> GetValueDelegate;
    internal static Action<object, string, object> SetValueDelegate;

    public object GetValue(T instance, string memberName)
    {
        return GetValueDelegate(instance, memberName);
    }

    public void SetValue(T instance, string memberName, object newValue)
    {
        SetValueDelegate(instance, memberName, newValue);
    }

    public object GetValue(object instance, string memberName)
    {
        return GetValueDelegate(instance, memberName);
    }

    public void SetValue(object instance, string memberName, object newValue)
    {
        SetValueDelegate(instance, memberName, newValue);
    }

    static DynamicMethod()
    {
        GetValueDelegate = GenerateGetValue();
        SetValueDelegate = GenerateSetValue();
    }

    private static Func<object, string, object> GenerateGetValue()
    {
        var type = typeof(T);
        var instance = Expression.Parameter(typeof(object), "instance");
        var memberName = Expression.Parameter(typeof(string), "memberName");
        var nameHash = Expression.Variable(typeof(int), "nameHash");
        var calHash = Expression.Assign(nameHash, Expression.Call(memberName, typeof(object).GetMethod("GetHashCode")));
        var cases = new List<SwitchCase>();
        foreach (var propertyInfo in type.GetProperties())
        {
            try
            {
                var property = Expression.Property(Expression.Convert(instance, typeof(T)), propertyInfo.Name);
                var propertyHash = Expression.Constant(propertyInfo.Name.GetHashCode(), typeof(int));

                cases.Add(Expression.SwitchCase(Expression.Convert(property, typeof(object)), propertyHash));
            }
            catch { }
        }
        var switchEx = Expression.Switch(nameHash, Expression.Constant(null), cases.ToArray());
        var methodBody = Expression.Block(typeof(object), new[] { nameHash }, calHash, switchEx);

        return Expression.Lambda<Func<object, string, object>>(methodBody, instance, memberName).Compile();
    }

    private static Action<object, string, object> GenerateSetValue()
    {
        var type = typeof(T);
        var instance = Expression.Parameter(typeof(object), "instance");
        var memberName = Expression.Parameter(typeof(string), "memberName");
        var newValue = Expression.Parameter(typeof(object), "newValue");
        var nameHash = Expression.Variable(typeof(int), "nameHash");
        var calHash = Expression.Assign(nameHash, Expression.Call(memberName, typeof(object).GetMethod("GetHashCode")));
        var cases = new List<SwitchCase>();
        foreach (var propertyInfo in type.GetProperties())
        {
            try
            {
                var property = Expression.Property(Expression.Convert(instance, typeof(T)), propertyInfo.Name);
                if(!((PropertyInfo) property.Member).CanWrite) continue;
                var setValue = Expression.Assign(property, Expression.Convert(newValue, propertyInfo.PropertyType));
                var propertyHash = Expression.Constant(propertyInfo.Name.GetHashCode(), typeof(int));

                cases.Add(Expression.SwitchCase(Expression.Convert(setValue, typeof(object)), propertyHash));
            }
            catch { }
        }
        var switchEx = Expression.Switch(nameHash, Expression.Constant(null), cases.ToArray());
        var methodBody = Expression.Block(typeof(object), new[] { nameHash }, calHash, switchEx);

        return Expression.Lambda<Action<object, string, object>>(methodBody, instance, memberName, newValue).Compile();
    }

    public ILGenerator GetILGenerator()
    {
        throw new NotImplementedException();
    }
}
    
#endregion
    
#region 方法反射优化

/// <summary>
/// 快速调用反射
/// </summary>
public static class FastInvoke
{
    public delegate object FastInvokeHandler(object target, object[] paramters);

    static object InvokeMethod(FastInvokeHandler invoke, object target, params object[] paramters)
    {
        return invoke(target, paramters);
    }

    public static FastInvokeHandler GetMethodInvoker(MethodInfo methodInfo)
    {
        DynamicMethod dynamicMethod = new DynamicMethod(string.Empty, typeof(object), new Type[] { typeof(object), typeof(object[]) }, methodInfo.DeclaringType.Module);
        ILGenerator il = dynamicMethod.GetILGenerator();
        ParameterInfo[] ps = methodInfo.GetParameters();
        Type[] paramTypes = new Type[ps.Length];
        for (int i = 0; i < paramTypes.Length; i++)
        {
            if (ps[i].ParameterType.IsByRef)
                paramTypes[i] = ps[i].ParameterType.GetElementType();
            else
                paramTypes[i] = ps[i].ParameterType;
        }
        LocalBuilder[] locals = new LocalBuilder[paramTypes.Length];

        for (int i = 0; i < paramTypes.Length; i++)
        {
            locals[i] = il.DeclareLocal(paramTypes[i], true);
        }
        for (int i = 0; i < paramTypes.Length; i++)
        {
            il.Emit(OpCodes.Ldarg_1);
            EmitFastInt(il, i);
            il.Emit(OpCodes.Ldelem_Ref);
            EmitCastToReference(il, paramTypes[i]);
            il.Emit(OpCodes.Stloc, locals[i]);
        }
        if (!methodInfo.IsStatic)
        {
            il.Emit(OpCodes.Ldarg_0);
        }
        for (int i = 0; i < paramTypes.Length; i++)
        {
            if (ps[i].ParameterType.IsByRef)
                il.Emit(OpCodes.Ldloca_S, locals[i]);
            else
                il.Emit(OpCodes.Ldloc, locals[i]);
        }
        if (methodInfo.IsStatic)
            il.EmitCall(OpCodes.Call, methodInfo, null);
        else
            il.EmitCall(OpCodes.Callvirt, methodInfo, null);
        if (methodInfo.ReturnType == typeof(void))
            il.Emit(OpCodes.Ldnull);
        else
            EmitBoxIfNeeded(il, methodInfo.ReturnType);

        for (int i = 0; i < paramTypes.Length; i++)
        {
            if (ps[i].ParameterType.IsByRef)
            {
                il.Emit(OpCodes.Ldarg_1);
                EmitFastInt(il, i);
                il.Emit(OpCodes.Ldloc, locals[i]);
                if (locals[i].LocalType.IsValueType)
                    il.Emit(OpCodes.Box, locals[i].LocalType);
                il.Emit(OpCodes.Stelem_Ref);
            }
        }

        il.Emit(OpCodes.Ret);
        FastInvokeHandler invoder = (FastInvokeHandler)dynamicMethod.CreateDelegate(typeof(FastInvokeHandler));
        return invoder;
    }

    private static void EmitCastToReference(ILGenerator il, Type type)
    {
        if (type.IsValueType)
        {
            il.Emit(OpCodes.Unbox_Any, type);
        }
        else
        {
            il.Emit(OpCodes.Castclass, type);
        }
    }

    private static void EmitBoxIfNeeded(ILGenerator il, Type type)
    {
        if (type.IsValueType)
        {
            il.Emit(OpCodes.Box, type);
        }
    }

    private static void EmitFastInt(ILGenerator il, int value)
    {
        switch (value)
        {
            case -1:
                il.Emit(OpCodes.Ldc_I4_M1);
                return;
            case 0:
                il.Emit(OpCodes.Ldc_I4_0);
                return;
            case 1:
                il.Emit(OpCodes.Ldc_I4_1);
                return;
            case 2:
                il.Emit(OpCodes.Ldc_I4_2);
                return;
            case 3:
                il.Emit(OpCodes.Ldc_I4_3);
                return;
            case 4:
                il.Emit(OpCodes.Ldc_I4_4);
                return;
            case 5:
                il.Emit(OpCodes.Ldc_I4_5);
                return;
            case 6:
                il.Emit(OpCodes.Ldc_I4_6);
                return;
            case 7:
                il.Emit(OpCodes.Ldc_I4_7);
                return;
            case 8:
                il.Emit(OpCodes.Ldc_I4_8);
                return;
        }

        if (value > -129 && value < 128)
        {
            il.Emit(OpCodes.Ldc_I4_S, (SByte)value);
        }
        else
        {
            il.Emit(OpCodes.Ldc_I4, value);
        }
    }
}
#endregion