using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BB.Tools.File;

/// <summary>
/// 这个类提供了一些实用的方法来转换XML和对象。
/// </summary>
public sealed class XmlConvertor
{
    private XmlConvertor()
    {
    }

    /// <summary>
    /// 转换XML字符串到指定类型的对象
    /// </summary>
    /// <typeparam name="T">指定的对象类型</typeparam>
    /// <param name="xml">XML字符串</param>
    /// <returns>从XML字符串转换过来的对象</returns>
    public static T XmlToObject<T>(string xml) where T : class
    {
        return XmlToObject(xml, typeof(T)) as T;
    }

    /// <summary>
    /// 转换XML字符串到指定类型的对象
    /// </summary>
    /// <param name="xml">XML字符串</param>
    /// <param name="type">指定的对象类型</param>
    /// <returns>从XML字符串转换过来的对象</returns>
    public static object XmlToObject(string xml, Type type)
    {
        if (null == xml)
        {
            throw new ArgumentNullException("xml");
        }
        if (null == type)
        {
            throw new ArgumentNullException("type");
        }

        object obj = null;
        XmlSerializer serializer = new XmlSerializer(type);
        StringReader strReader = new StringReader(xml);
        XmlReader reader = new XmlTextReader(strReader);

        try
        {
            obj = serializer.Deserialize(reader);
        }
        catch (InvalidOperationException ie)
        {
            throw new InvalidOperationException("Can not convert xml to object", ie);
        }
        finally
        {
            reader.Close();
        }
        return obj;
    }

    /// <summary>
    /// 转换object对象到具体的XML字符串
    /// </summary>
    /// <param name="obj">待序列化的对象</param>
    /// <param name="toBeIndented"><c>true</c>缩进, 否则<c>false</c>.</param>
    /// <returns>XML字符串</returns>
    public static string ObjectToXml(object obj, bool toBeIndented = false)
    {
        if (null == obj)
        {
            throw new ArgumentNullException("obj");
        }
        XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
        ns.Add("", "");

        UTF8Encoding encoding = new UTF8Encoding(false);
        XmlSerializer serializer = new XmlSerializer(obj.GetType());
        MemoryStream stream = new MemoryStream();
        XmlTextWriter writer = new XmlTextWriter(stream, encoding);
        writer.Formatting = (toBeIndented ? Formatting.Indented : Formatting.None);

        try
        {
            serializer.Serialize(writer, obj, ns);
        }
        catch (InvalidOperationException)
        {
            throw new InvalidOperationException("Can not convert object to xml.");
        }
        finally
        {
            writer.Close();
        }

        string xml = encoding.GetString(stream.ToArray());
        return xml;
    }

    /// <summary>
    /// 格式化XML
    /// </summary>
    /// <param name="xml">待格式化的XML</param>
    /// <returns></returns>
    public static string FormatXml(string xml)
    {
        XmlDocument xd = new XmlDocument();
        xd.LoadXml(xml);
        StringBuilder sb = new StringBuilder();
        StringWriter sw = new StringWriter(sb);
        XmlTextWriter xtw = null;
        try
        {
            xtw = new XmlTextWriter(sw);
            xtw.Formatting = Formatting.Indented;
            xtw.Indentation = 1;
            xtw.IndentChar = '\t';
            xd.WriteTo(xtw);
        }
        finally
        {
            if (xtw != null)
                xtw.Close();
        }
        return sb.ToString();
    } 
}