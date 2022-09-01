using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Furion.UnifyResult;

namespace BB.HttpServices.Base;

/// <summary>
/// 通用返回值泛型转换工厂
/// </summary>
public class RESTfulResultConverter : JsonConverterFactory
{
    /// <summary>
    /// 是否符合转换条件
    /// </summary>
    /// <param name="typeToConvert"></param>
    /// <returns></returns>
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
            return false;

        Type generic = typeToConvert.GetGenericTypeDefinition();
        if (generic != typeof(RESTfulResult<>))
            return false;

        // 判断泛型参数，符合条件的泛型将执行转换
        Type arg = typeToConvert.GetGenericArguments()[0];
        return arg == typeof(int) ||
               arg == typeof(long) ||
               arg == typeof(bool);
    }

    /// <summary>
    /// 创建转换器
    /// </summary>
    /// <param name="type"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
    {
        // 获取泛型类型
        Type elementType = type.GetGenericArguments()[0];

        // 根据泛型类型创建对应的转换器
        var converter = (JsonConverter)Activator.CreateInstance(
            typeof(RESTfulResultTConverter<>).MakeGenericType(elementType),
            BindingFlags.Instance | BindingFlags.Public)!;

        return converter;
    }

    /// <summary>
    /// 通用返回值泛型转换器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    private class RESTfulResultTConverter<T> : JsonConverter<RESTfulResult<T>>
    {
        /// <summary>
        /// 读 Json 反序列化为 对象
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="typeToConvert"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        /// <exception cref="JsonException"></exception>
        public override RESTfulResult<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // 必须是一个 Json 对象
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            var value = new RESTfulResult<T>();

            // 逐个处理 Json 元素
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return value;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string? propertyName = reader.GetString();
                    // 往前读一步
                    reader.Read();
                    // switch (propertyName)
                    // {
                    //     case "CreditLimit":
                    //         decimal creditLimit = reader.GetDecimal();
                    //         value.Data = creditLimit;
                    //         break;
                    //     case "OfficeNumber":
                    //         string? officeNumber = reader.GetString();
                    //         ((Employee)person).OfficeNumber = officeNumber;
                    //         break;
                    //     case "Name":
                    //         string? name = reader.GetString();
                    //         person.Name = name;
                    //         break;
                    // }
                }
                // if (typeof(T) == typeof(int))
                // {
                //     int element = reader.GetInt32();
                //     IList list = value;
                //     list.Add(element + _offset);
                // }
                // else if (typeof(T) == typeof(long))
                // {
                //     long element = reader.GetInt64();
                //     IList list = value;
                //     list.Add(element + _offset);
                // }
            }

            throw new JsonException();
        }

        /// <summary>
        /// 将对象写入Json
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="options"></param>
        public override void Write(Utf8JsonWriter writer, RESTfulResult<T> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            // foreach (T item in value)
            // {
            //     if (item is int)
            //     {
            //         writer.WriteNumberValue((int)(object)item - _offset);
            //     }
            //     else if (item is long)
            //     {
            //         writer.WriteNumberValue((long)(object)item - _offset);
            //     }
            //     else
            //     {
            //         Assert.True(false);
            //     }
            // }

            writer.WriteEndArray();
        }
    }
}