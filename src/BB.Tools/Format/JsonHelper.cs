using System.Collections;
using System.Data;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Furion.JsonSerialization;

namespace BB.Tools.Format;

/// <summary>
/// Json字符串处理辅助类
/// </summary>
public static class JsonHelper
{
    #region 私有方法

    /// <summary>
    /// 过滤特殊字符
    /// </summary>
    private static string String2Json(String s)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            char c = s.ToCharArray()[i];
            switch (c)
            {
                case '\"':
                    sb.Append("\\\"");
                    break;
                case '\\':
                    sb.Append("\\\\");
                    break;
                case '/':
                    sb.Append("\\/");
                    break;
                case '\b':
                    sb.Append("\\b");
                    break;
                case '\f':
                    sb.Append("\\f");
                    break;
                case '\n':
                    sb.Append("\\n");
                    break;
                case '\r':
                    sb.Append("\\r");
                    break;
                case '\t':
                    sb.Append("\\t");
                    break;
                default:
                    sb.Append(c);
                    break;
            }
        }

        return sb.ToString();
    }

    /// <summary>
    /// 格式化字符型、日期型、布尔型
    /// </summary>
    private static string StringFormat(string str, Type type)
    {
        if (type == typeof(string))
        {
            str = String2Json(str);
            str = "\"" + str + "\"";
        }
        else if (type == typeof(DateTime))
        {
            str = "\"" + str + "\"";
        }
        else if (type == typeof(bool))
        {
            str = str.ToLower();
        }
        else if (type != typeof(string) && string.IsNullOrEmpty(str))
        {
            str = "\"" + str + "\"";
        }
        else if (type == typeof(object))
        {
            str = "\"" + str + "\"";
        }

        return str;
    }

    #endregion

    #region 从Json反序列化

    /// <summary>
    /// 反序列化为对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public static T Deserialize<T>(string json) where T : class
    {
        try
        {
            return JSON.Deserialize<T>(json);
        }
        catch
        {
            // ignored
        }

        return default(T);
    }

    /// <summary>
    /// 反序列化为对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <param name="errMsg">异常信息</param>
    /// <returns></returns>
    public static T Deserialize<T>(string json, out string errMsg) where T : class
    {
        errMsg = string.Empty;
        try
        {
            return JSON.Deserialize<T>(json);
        }
        catch (Exception ex)
        {
            errMsg = ex.Message;
        }

        return default(T);
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="json">JSON数据</param>
    /// <param name="type">类型</param>
    /// <returns></returns>
    public static object? Deserialize(string json, Type type)
    {
        return JsonSerializer.Deserialize(json, type);
    }

    /// <summary>
    /// 反序列化
    /// </summary>
    /// <param name="json">JSON数据</param>
    /// <param name="type">类型</param>
    /// <param name="errMsg">异常信息</param>
    /// <returns></returns>
    public static object? Deserialize(string json, Type type, out string errMsg)
    {
        try
        {
            errMsg = string.Empty;
            return JsonSerializer.Deserialize(json, type);
        }
        catch (Exception ex)
        {
            errMsg = ex.Message;
        }

        return null;
    }

    /// <summary>
    /// 反序列化到dynamic
    /// </summary>
    /// <param name="json">JSON数据</param>
    /// <returns></returns>
    public static dynamic? DeserializeToDynamic(string json)
    {
        return JsonSerializer.Deserialize<dynamic>(json);
    }

    #endregion

    #region 序列化到Json字符串

    /// <summary>
    /// 序列化到json
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    [Obsolete("Obsolete")]
    public static string Serialize(object entity)
    {
        var jsonSetting = new JsonSerializerOptions()
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };
        return JSON.Serialize(entity, jsonSetting);
    }

    #endregion

    #region 纯手工无依赖序列化到Json

    #region List转换成Json

    /// <summary>
    /// List转换成Json
    /// </summary>
    public static string ListToJson<T>(this IList<T> list)
    {
        object obj = list[0];
        return ListToJson<T>(list, obj.GetType().Name);
    }

    /// <summary>
    /// List转换成Json 
    /// </summary>
    public static string ListToJson<T>(this IList<T> list, string jsonName)
    {
        StringBuilder json = new StringBuilder();
        if (string.IsNullOrEmpty(jsonName)) jsonName = list[0].GetType().Name;
        json.Append("{\"" + jsonName + "\":[");
        if (list.Count > 0)
        {
            for (int i = 0; i < list.Count; i++)
            {
                T obj = Activator.CreateInstance<T>();
                PropertyInfo[] pi = obj.GetType().GetProperties();
                json.Append("{");
                for (int j = 0; j < pi.Length; j++)
                {
                    Type type = pi[j].GetValue(list[i], null).GetType();
                    json.Append(
                        "\"" + pi[j].Name + "\":" + StringFormat(pi[j].GetValue(list[i], null).ToString(), type));

                    if (j < pi.Length - 1)
                    {
                        json.Append(",");
                    }
                }

                json.Append("}");
                if (i < list.Count - 1)
                {
                    json.Append(",");
                }
            }
        }

        json.Append("]}");
        return json.ToString();
    }

    #endregion

    #region 对象转换为Json

    /// <summary> 
    /// 对象转换为Json 
    /// </summary> 
    /// <param name="jsonObject">对象</param> 
    /// <returns>Json字符串</returns> 
    public static string ToJson(this object jsonObject)
    {
        string jsonString = "{";
        PropertyInfo[] propertyInfo = jsonObject.GetType().GetProperties();
        for (int i = 0; i < propertyInfo.Length; i++)
        {
            object objectValue = propertyInfo[i].GetGetMethod().Invoke(jsonObject, null);
            string value;
            if (objectValue is DateTime || objectValue is Guid || objectValue is TimeSpan)
            {
                value = "'" + objectValue + "'";
            }
            else if (objectValue is string)
            {
                value = "'" + ToJson(objectValue.ToString()) + "'";
            }
            else if (objectValue is IEnumerable)
            {
                value = ToJson((IEnumerable)objectValue);
            }
            else
            {
                value = ToJson(objectValue.ToString());
            }

            jsonString += "\"" + ToJson(propertyInfo[i].Name) + "\":" + value + ",";
        }

        return jsonString.Remove(jsonString.Length - 1, jsonString.Length) + "}";
    }

    /// <summary>
    /// 格式化成Json字符串
    /// </summary>
    /// <param name="obj">需要格式化的对象</param>
    /// <returns>Json字符串</returns>
    public static string ToJsonWithSerializer(this object obj)
    {
        // 首先，当然是JSON序列化
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());

        // 定义一个stream用来存发序列化之后的内容
        Stream stream = new MemoryStream();
        serializer.WriteObject(stream, obj);

        // 从头到尾将stream读取成一个字符串形式的数据，并且返回
        stream.Position = 0;
        StreamReader streamReader = new StreamReader(stream);
        return streamReader.ReadToEnd();
    }

    #endregion

    #region 对象集合转换Json

    /// <summary> 
    /// 对象集合转换Json 
    /// </summary> 
    /// <param name="array">集合对象</param> 
    /// <returns>Json字符串</returns> 
    public static string ToJson(this IEnumerable array)
    {
        string jsonString = "[";
        foreach (object item in array)
        {
            jsonString += ToJson(item) + ",";
        }

        return jsonString.Remove(jsonString.Length - 1, jsonString.Length) + "]";
    }

    #endregion

    #region 普通集合转换Json

    /// <summary> 
    /// 普通集合转换Json 
    /// </summary> 
    /// <param name="array">集合对象</param> 
    /// <returns>Json字符串</returns> 
    public static string ToArrayString(this IEnumerable array)
    {
        string jsonString = "[";
        foreach (object item in array)
        {
            jsonString = ToJson(item.ToString()) + ",";
        }

        return jsonString.Remove(jsonString.Length - 1, jsonString.Length) + "]";
    }

    #endregion

    #region DataSet转换为Json

    /// <summary> 
    /// DataSet转换为Json 
    /// </summary> 
    /// <param name="dataSet">DataSet对象</param> 
    /// <returns>Json字符串</returns> 
    public static string ToJson(this DataSet dataSet)
    {
        string jsonString = "{";
        foreach (DataTable table in dataSet.Tables)
        {
            jsonString += "\"" + table.TableName + "\":" + ToJson(table) + ",";
        }

        return jsonString.TrimEnd(',') + "}";
    }

    /// <summary>
    /// 将DataSet对象转换为Json字符串（DataSet只有一个Table情况）
    /// </summary>
    /// <param name="dataSet">DataSet对象</param>
    /// <param name="details">里面的IDictionary包含对字段名的转义字典</param>
    /// <returns></returns>
    public static string ToJson(this DataSet dataSet, IDictionary<string, IDictionary<string, string>> details)
    {
        string json = string.Empty;
        if (dataSet != null && dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
        {
            int i = 0, j = 0;
            json += "[";
            foreach (DataRow row in dataSet.Tables[0].Rows)
            {
                if (i == 0)
                {
                }
                else
                {
                    json += ",";
                }

                j = 0;
                json += "{";
                foreach (DataColumn column in dataSet.Tables[0].Columns)
                {
                    if (j == 0)
                    {
                    }
                    else
                    {
                        json += ",";
                    }

                    if (details != null && details.ContainsKey(column.ColumnName))
                    {
                        IDictionary<string, string> dict = details[column.ColumnName];

                        if (dict != null && dict.ContainsKey(row[column].ToString()))
                            json += $"'{column.ColumnName.ToLower()}':'{dict[row[column].ToString()]}'";
                        else
                            json += $"'{column.ColumnName.ToLower()}':'{row[column]}'";
                    }
                    else
                        json += $"'{column.ColumnName.ToLower()}':'{row[column]}'";

                    j++;
                }

                json += "}";
                i++;
            }

            json += "]";
        }

        //json = "{\"result\":\"" + json + "\"}";
        return json;
    }

    #endregion

    #region Datatable转换为Json

    /// <summary> 
    /// Datatable转换为Json 
    /// </summary> 
    /// <param name="table">Datatable对象</param> 
    /// <returns>Json字符串</returns> 
    public static string ToJson(this DataTable dt)
    {
        StringBuilder jsonString = new StringBuilder();
        jsonString.Append("[");
        DataRowCollection drc = dt.Rows;
        for (int i = 0; i < drc.Count; i++)
        {
            jsonString.Append("{");
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                string strKey = dt.Columns[j].ColumnName;
                string strValue = drc[i][j].ToString();
                Type type = dt.Columns[j].DataType;
                jsonString.Append("\"" + strKey + "\":");
                strValue = StringFormat(strValue, type);
                if (j < dt.Columns.Count - 1)
                {
                    jsonString.Append(strValue + ",");
                }
                else
                {
                    jsonString.Append(strValue);
                }
            }

            jsonString.Append("},");
        }

        jsonString.Remove(jsonString.Length - 1, 1);
        jsonString.Append("]");
        return jsonString.ToString();
    }

    /// <summary>
    /// DataTable转换为Json 
    /// </summary>
    /// <param name="dt">DataTable对象</param>
    /// <param name="jsonName">Json名称</param>
    public static string ToJson(this DataTable dt, string jsonName)
    {
        StringBuilder json = new StringBuilder();
        if (string.IsNullOrEmpty(jsonName)) jsonName = dt.TableName;
        json.Append("{\"" + jsonName + "\":[");
        if (dt.Rows.Count > 0)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                json.Append("{");
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    Type type = dt.Rows[i][j].GetType();
                    json.Append("\"" + dt.Columns[j].ColumnName + "\":" + StringFormat(dt.Rows[i][j].ToString(), type));
                    if (j < dt.Columns.Count - 1)
                    {
                        json.Append(",");
                    }
                }

                json.Append("}");
                if (i < dt.Rows.Count - 1)
                {
                    json.Append(",");
                }
            }
        }

        json.Append("]}");
        return json.ToString();
    }

    #endregion

    #region DataReader转换为Json

    /// <summary> 
    /// DataReader转换为Json 
    /// </summary> 
    /// <param name="dataReader">DataReader对象</param> 
    /// <returns>Json字符串</returns> 
    public static string ToJsonArray(this IDataReader dataReader)
    {
        StringBuilder jsonString = new StringBuilder();
        jsonString.Append("[");
        while (dataReader.Read())
        {
            jsonString.Append(dataReader.ToJsonObject());
            jsonString.Append(",");
        }

        dataReader.Close();
        jsonString.Remove(jsonString.Length - 1, 1);
        jsonString.Append("]");
        return jsonString.ToString();
    }

    /// <summary> 
    /// 一条DataReader转换为Json 
    /// </summary> 
    /// <param name="dataReader">DataReader对象</param> 
    /// <returns>Json字符串</returns> 
    public static string ToJsonObject(this IDataReader dataReader)
    {
        StringBuilder jsonString = new StringBuilder();

        jsonString.Append("{");
        for (int i = 0; i < dataReader.FieldCount; i++)
        {
            Type type = dataReader.GetFieldType(i);
            string strKey = dataReader.GetName(i);
            string strValue = dataReader[i].ToString();
            jsonString.Append("\"" + strKey + "\":");
            strValue = StringFormat(strValue, type);
            if (i < dataReader.FieldCount - 1)
            {
                jsonString.Append(strValue + ",");
            }
            else
            {
                jsonString.Append(strValue);
            }
        }
        jsonString.Append("}");

        return jsonString.ToString();
    }

    #endregion

    #endregion
}