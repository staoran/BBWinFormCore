using System.Text;

namespace BB.Tools.Format;

/// <summary>
/// 处理数据类型转换，数制转换、编码转换相关的类
/// </summary>
public static class ConvertHelper
{
    #region 各进制数间转换

    /// <summary>
    /// 实现各进制数间的转换。ConvertBase("15",10,16)表示将十进制数15转换为16进制的数。
    /// </summary>
    /// <param name="value">要转换的值,即原值</param>
    /// <param name="from">原值的进制,只能是2,8,10,16四个值。</param>
    /// <param name="to">要转换到的目标进制，只能是2,8,10,16四个值。</param>
    public static string ConvertBase(string value, int from, int to)
    {
        if (!IsBaseNumber(from))
            throw new ArgumentException("参数from只能是2,8,10,16四个值。");

        if (!IsBaseNumber(to))
            throw new ArgumentException("参数to只能是2,8,10,16四个值。");

        int intValue = Convert.ToInt32(value, from);  //先转成10进制
        string result = Convert.ToString(intValue, to);  //再转成目标进制
        if (to == 2)
        {
            int resultLength = result.Length;  //获取二进制的长度
            switch (resultLength)
            {
                case 7:
                    result = "0" + result;
                    break;
                case 6:
                    result = "00" + result;
                    break;
                case 5:
                    result = "000" + result;
                    break;
                case 4:
                    result = "0000" + result;
                    break;
                case 3:
                    result = "00000" + result;
                    break;
            }
        }
        return result;
    }

    /// <summary>
    /// 判断是否是 2 8 10 16
    /// </summary>
    /// <param name="baseNumber"></param>
    /// <returns></returns>
    private static bool IsBaseNumber(this int baseNumber)
    {
        if (baseNumber == 2 || baseNumber == 8 || baseNumber == 10 || baseNumber == 16)
            return true;
        return false;
    }

    #endregion

    #region 使用指定字符集将string转换成byte[]

    /// <summary>
    /// 将string转换成byte[]
    /// </summary>
    /// <param name="text">要转换的字符串</param>
    public static byte[] StringToBytes(this string text)
    {
        return Encoding.Default.GetBytes(text);
    }

    /// <summary>
    /// 使用指定字符集将string转换成byte[]
    /// </summary>
    /// <param name="text">要转换的字符串</param>
    /// <param name="encoding">字符编码</param>
    public static byte[] StringToBytes(this string text, Encoding encoding)
    {
        return encoding.GetBytes(text);
    }

    #endregion

    #region 使用指定字符集将byte[]转换成string
        
    /// <summary>
    /// 将byte[]转换成string
    /// </summary>
    /// <param name="bytes">要转换的字节数组</param>
    public static string BytesToString(this byte[] bytes)
    {
        return Encoding.Default.GetString(bytes);
    }

    /// <summary>
    /// 使用指定字符集将byte[]转换成string
    /// </summary>
    /// <param name="bytes">要转换的字节数组</param>
    /// <param name="encoding">字符编码</param>
    public static string BytesToString(this byte[] bytes, Encoding encoding)
    {
        return encoding.GetString(bytes);
    }
    #endregion

    #region 将byte[]转换成int

    /// <summary>
    /// 将byte[]转换成int
    /// </summary>
    /// <param name="data">需要转换成整数的byte数组</param>
    public static int BytesToInt32(this byte[] data)
    {
        //如果传入的字节数组长度小于4,则返回0
        if (data.Length < 4)
        {
            return 0;
        }

        //定义要返回的整数
        int num = 0;

        //如果传入的字节数组长度大于4,需要进行处理
        if (data.Length >= 4)
        {
            //创建一个临时缓冲区
            byte[] tempBuffer = new byte[4];

            //将传入的字节数组的前4个字节复制到临时缓冲区
            Buffer.BlockCopy(data, 0, tempBuffer, 0, 4);

            //将临时缓冲区的值转换成整数，并赋给num
            num = BitConverter.ToInt32(tempBuffer, 0);
        }

        //返回整数
        return num;
    }

    #endregion

    #region 将数据转换为整型

    /// <summary>
    /// 将数据转换为整型   转换失败返回默认值
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static int ToInt32<T>(T data, int defValue)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Convert.ToInt32(data);
        }
        catch
        {
            return defValue;
        }

    }

    /// <summary>
    /// 将数据转换为整型   转换失败返回默认值
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static int ToInt32(string data, int defValue = 0)
    {
        //如果为空则返回默认值
        if (string.IsNullOrEmpty(data))
        {
            return defValue;
        }

        if (Int32.TryParse(data, out int temp))
        {
            return temp;
        }
        else
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将数据转换为整型  转换失败返回默认值
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static int ToInt32(this object data, int defValue = 0)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Convert.ToInt32(data);
        }
        catch
        {
            return defValue;
        }
    }

    #endregion

    #region 将数据转换为布尔型

    /// <summary>
    /// 将数据转换为布尔类型  转换失败返回默认值
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static bool ToBoolean<T>(T data, bool defValue)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Convert.ToBoolean(data);
        }
        catch
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将数据转换为布尔类型  转换失败返回 默认值
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static bool ToBoolean(this string data, bool defValue)
    {
        //如果为空则返回默认值
        if (string.IsNullOrEmpty(data))
        {
            return defValue;
        }

        if (bool.TryParse(data, out bool temp))
        {
            return temp;
        }
        else
        {
            return defValue;
        }
    }


    /// <summary>
    /// 将数据转换为布尔类型  转换失败返回 默认值
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static bool ToBoolean(this object data, bool defValue)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Convert.ToBoolean(data);
        }
        catch
        {
            return defValue;
        }
    }


    #endregion

    #region 将数据转换为单精度浮点型


    /// <summary>
    /// 将数据转换为单精度浮点型  转换失败 返回默认值
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static float ToFloat<T>(T data, float defValue)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Convert.ToSingle(data);
        }
        catch
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将数据转换为单精度浮点型   转换失败返回默认值
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static float ToFloat(this object data, float defValue = 0)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Convert.ToSingle(data);
        }
        catch
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将数据转换为单精度浮点型   转换失败返回默认值
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static float ToFloat(this string data, float defValue = 0)
    {
        //如果为空则返回默认值
        if (string.IsNullOrEmpty(data))
        {
            return defValue;
        }

        if (float.TryParse(data, out float temp))
        {
            return temp;
        }
        else
        {
            return defValue;
        }
    }


    #endregion

    #region 将数据转换为双精度浮点型


    /// <summary>
    /// 将数据转换为双精度浮点型   转换失败返回默认值
    /// </summary>
    /// <typeparam name="T">数据的类型</typeparam>
    /// <param name="data">要转换的数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static double ToDouble<T>(T data, double defValue = 0)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Convert.ToDouble(data);
        }
        catch
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将数据转换为双精度浮点型,并设置小数位   转换失败返回默认值
    /// </summary>
    /// <typeparam name="T">数据的类型</typeparam>
    /// <param name="data">要转换的数据</param>
    /// <param name="decimals">小数的位数</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static double ToDouble<T>(this T data, int decimals, double defValue = 0)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Math.Round(Convert.ToDouble(data), decimals);
        }
        catch
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将数据转换为双精度浮点型  转换失败返回默认值
    /// </summary>
    /// <param name="data">要转换的数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static double ToDouble(this object data, double defValue = 0)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Convert.ToDouble(data);
        }
        catch
        {
            return defValue;
        }

    }

    /// <summary>
    /// 将数据转换为双精度浮点型  转换失败返回默认值
    /// </summary>
    /// <param name="data">要转换的数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static double ToDouble(this string data, double defValue = 0)
    {
        //如果为空则返回默认值
        if (string.IsNullOrEmpty(data))
        {
            return defValue;
        }

        if (double.TryParse(data, out double temp))
        {
            return temp;
        }
        else
        {
            return defValue;
        }

    }

    /// <summary>
    /// 将数据转换为双精度浮点型,并设置小数位  转换失败返回默认值
    /// </summary>
    /// <param name="data">要转换的数据</param>
    /// <param name="decimals">小数的位数</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static double ToDouble(this object data, int decimals, double defValue = 0)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Math.Round(Convert.ToDouble(data), decimals);
        }
        catch
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将数据转换为双精度浮点型,并设置小数位  转换失败返回默认值
    /// </summary>
    /// <param name="data">要转换的数据</param>
    /// <param name="decimals">小数的位数</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static double ToDouble(this string data, int decimals, double defValue = 0)
    {
        //如果为空则返回默认值
        if (string.IsNullOrEmpty(data))
        {
            return defValue;
        }

        if (double.TryParse(data, out double temp))
        {
            return Math.Round(temp, decimals);
        }
        else
        {
            return defValue;
        }
    }

    #endregion

    #region 将数据转换为指定类型

    /// <summary>
    /// 将数据转换为指定类型
    /// </summary>
    /// <param name="data">转换的数据</param>
    /// <param name="targetType">转换的目标类型</param>
    public static object ConvertTo(this object data, Type targetType)
    {
        if (data == null || Convert.IsDBNull(data))
        {
            return null;
        }

        Type type2 = data.GetType();
        if (targetType == type2)
        {
            return data;
        }
        if (((targetType == typeof(Guid)) || (targetType == typeof(Guid?))) && (type2 == typeof(string)))
        {
            if (string.IsNullOrEmpty(data.ToString()))
            {
                return null;
            }
            return new Guid(data.ToString());
        }

        if (targetType.IsEnum)
        {
            try
            {
                return Enum.Parse(targetType, data.ToString(), true);
            }
            catch
            {
                return Enum.ToObject(targetType, data);
            }
        }

        if (targetType.IsGenericType)
        {
            targetType = targetType.GetGenericArguments()[0];
        }

        return Convert.ChangeType(data, targetType);
    }

    /// <summary>
    /// 将数据转换为指定类型
    /// </summary>
    /// <typeparam name="T">转换的目标类型</typeparam>
    /// <param name="data">转换的数据</param>
    public static T ConvertTo<T>(object data)
    {
        if (data == null || Convert.IsDBNull(data))
            return default(T);

        object obj = ConvertTo(data, typeof(T));
        if (obj == null)
        {
            return default(T);
        }
        return (T)obj;
    }

    #endregion

    #region 将数据转换Decimal

    /// <summary>
    /// 将数据转换为Decimal  转换失败返回默认值
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static Decimal ToDecimal<T>(T data, Decimal defValue = Decimal.Zero)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Convert.ToDecimal(data);
        }
        catch
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将数据转换为Decimal  转换失败返回 默认值
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static Decimal ToDecimal(this object data, Decimal defValue = Decimal.Zero)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Convert.ToDecimal(data);
        }
        catch
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将数据转换为Decimal  转换失败返回 默认值
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static Decimal ToDecimal(this string data, Decimal defValue = Decimal.Zero)
    {
        //如果为空则返回默认值
        if (string.IsNullOrEmpty(data))
        {
            return defValue;
        }

        if (decimal.TryParse(data, out decimal temp))
        {
            return temp;
        }
        else
        {
            return defValue;
        }
    }

    #endregion

    #region 将数据转换为DateTime

    /// <summary>
    /// 将数据转换为DateTime  转换失败返回默认值
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static DateTime ToDateTime<T>(T data, DateTime defValue)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Convert.ToDateTime(data);
        }
        catch
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将数据转换为DateTime  转换失败返回 默认值
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static DateTime ToDateTime(this object data, DateTime defValue)
    {
        //如果为空则返回默认值
        if (data == null || Convert.IsDBNull(data))
        {
            return defValue;
        }

        try
        {
            return Convert.ToDateTime(data);
        }
        catch
        {
            return defValue;
        }
    }

    /// <summary>
    /// 将数据转换为DateTime  转换失败返回 默认值
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="defValue">默认值</param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string data, DateTime defValue)
    {
        //如果为空则返回默认值
        if (string.IsNullOrEmpty(data))
        {
            return defValue;
        }

        if (DateTime.TryParse(data, out DateTime temp))
        {
            return temp;
        }
        else
        {
            return defValue;
        }
    }

    #endregion

    #region 半角全角转换

    /// <summary>
    /// 转全角的函数(SBC case)
    /// </summary>
    /// <param name="input">任意字符串</param>
    /// <returns>全角字符串</returns>
    ///<remarks>
    ///全角空格为12288，半角空格为32
    ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
    ///</remarks>
    public static string ConvertToSbc(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return "";
        }

        //半角转全角：
        char[] c = input.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] == 32)
            {
                c[i] = (char)12288;
                continue;
            }
            if (c[i] < 127)
            {
                c[i] = (char)(c[i] + 65248);
            }
        }
        return new string(c);
    }

    /// <summary>
    /// 转半角的函数(DBC case) 
    /// </summary>
    /// <param name="input">任意字符串</param>
    /// <returns>半角字符串</returns>
    ///<remarks>
    ///全角空格为12288，半角空格为32
    ///其他字符半角(33-126)与全角(65281-65374)的对应关系是：均相差65248
    ///</remarks>
    public static string ConvertToDbc(string input)
    {
        if(string.IsNullOrWhiteSpace(input))
        {
            return "";
        }

        char[] c = input.ToCharArray();
        for (int i = 0; i < c.Length; i++)
        {
            if (c[i] == 12288)
            {
                c[i] = (char)32;
                continue;
            }
            if (c[i] > 65280 && c[i] < 65375)
                c[i] = (char)(c[i] - 65248);
        }
        return new string(c);
    }

    #endregion

    #region 强制转化

    /// <summary>
    /// object转化为Bool类型
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static bool ObjToBool(this object obj)
    {
        if (obj == null)
        {
            return false;
        }
            
        if (obj.Equals(DBNull.Value))
        {
            return false;
        }

        bool ret;
        var str = obj.ToString();
        switch (str.ToLower())
        {
            case "true":
            case "t":
            case "yes":
            case "y":
            case "1":
            case "-1":
                ret = true;
                break;

            case "false":
            case "f":
            case "no":
            case "n":
            case "0":
                ret = false;
                break;
            default:
                ret = bool.TryParse(str, out bool flag) && flag;
                break;
        }

        return ret;
    }

    /// <summary>
    /// object强制转化为DateTime类型(吃掉异常)
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static DateTime? ObjToDateNull(this object obj)
    {
        if (obj == null)
        {
            return null;
        }

        try
        {
            return Convert.ToDateTime(obj);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// byte强制转化
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static byte ObjToByte(this object obj)
    {
        if (obj != null)
        {
            if (obj.Equals(DBNull.Value))
            {
                return 0;
            }

            if (Byte.TryParse(obj.ToString(), out byte num))
            {
                return num;
            }
        }

        return 0;
    }

    /// <summary>
    /// int强制转化
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static int ObjToInt(this object obj)
    {
        if (obj != null)
        {
            if (obj.Equals(DBNull.Value))
            {
                return 0;
            }

            if (int.TryParse(obj.ToString(), out int num))
            {
                return num;
            }
        }

        return 0;
    }

    /// <summary>
    /// 强制转化为long
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static long ObjToLong(this object obj)
    {
        if (obj != null)
        {
            if (obj.Equals(DBNull.Value))
            {
                return 0;
            }

            if (long.TryParse(obj.ToString(), out long num))
            {
                return num;
            }
        }

        return 0;
    }

    /// <summary>
    /// 强制转化可空int类型
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static int? ObjToIntNull(this object obj)
    {
        if (obj == null)
        {
            return null;
        }

        if (obj.Equals(DBNull.Value))
        {
            return null;
        }

        return ObjToInt(obj);
    }

    /// <summary>
    /// 强制转化可空long类型
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static long? ObjToLongNull(this object obj)
    {
        if (obj == null)
        {
            return null;
        }

        if (obj.Equals(DBNull.Value))
        {
            return null;
        }

        return ObjToLong(obj);
    }

    /// <summary>
    /// 强制转化为string
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string ObjToStr(this object obj)
    {
        if (obj == null || obj.Equals(DBNull.Value))
        {
            return string.Empty;
        }

        if (obj is string s)
        {
            return s;
        }

        return Convert.ToString(obj);
    }

    /// <summary>
    /// double强制转化
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static double ObjToDouble(this object obj)
    {
        if (obj != null)
        {
            if (obj.Equals(DBNull.Value))
            {
                return 0;
            }

            if (double.TryParse(obj.ToString(), out double num))
            {
                return num;
            }
        }

        return 0;
    }

    /// <summary>
    /// Decimal转化
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static decimal ObjToDecimal(this object obj)
    {
        if (obj == null)
        {
            return 0M;
        }

        if (obj.Equals(DBNull.Value))
        {
            return 0M;
        }

        try
        {
            return Convert.ToDecimal(obj);
        }
        catch
        {
            return 0M;
        }
    }

    /// <summary>
    /// Decimal可空类型转化
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static decimal? ObjToDecimalNull(this object obj)
    {
        if (obj == null)
        {
            return null;
        }

        if (obj.Equals(DBNull.Value))
        {
            return null;
        }

        return ObjToDecimal(obj);
    }

    /// <summary>
    /// Short转化
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static short ObjToShort(this object obj)
    {
        if (obj == null)
        {
            return 0;
        }

        if (obj.Equals(DBNull.Value))
        {
            return 0;
        }

        try
        {
            return Convert.ToInt16(obj);
        }
        catch
        {
            return 0;
        }
    }

    /// <summary>
    /// Short可空类型转化
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static short? ObjToShortNull(this object obj)
    {
        if (obj == null)
        {
            return null;
        }

        if (obj.Equals(DBNull.Value))
        {
            return null;
        }

        return ObjToShort(obj);
    }

    /// <summary>
    /// 强制转化为Guid
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Guid ObjToGuid(this object obj)
    {
        if (obj == null)
        {
            return Guid.Empty;
        }

        if (obj.Equals(DBNull.Value))
        {
            return Guid.Empty;
        }

        if (string.IsNullOrWhiteSpace(obj.ToString()))
        {
            return Guid.Empty;
        }

        Guid.TryParse(obj.ToString(), out Guid g);
        return g;
    }

    /// <summary>
    /// 强制转化为Guid?
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static Guid? ObjToGuidNull(this object obj)
    {
        if (obj == null)
        {
            return null;
        }

        if (obj.Equals(DBNull.Value))
        {
            return null;
        }

        return ObjToGuid(obj);
    }

    #endregion

    #region 获取时间差string GetTime(BsonString getTime)

    /// <summary>
    /// 获取时间差
    /// </summary>
    /// <param name="startTime">起始时间</param>
    /// <returns>时间差</returns>

    public static string DateStringFromNow(this DateTime startTime)
    {
        TimeSpan span = DateTime.Now - startTime;
        if (span.TotalDays > 60)
        {
            return startTime.ToShortDateString();
        }

        if (span.TotalDays > 30)
        {
            return "1个月前";
        }

        if (span.TotalDays > 14)
        {
            return "2周前";
        }

        if (span.TotalDays > 7)
        {
            return "1周前";
        }

        if (span.TotalDays > 1)
        {
            return $"{(int) Math.Floor(span.TotalDays)}天前";
        }

        if (span.TotalHours > 1)
        {
            return $"{(int) Math.Floor(span.TotalHours)}小时前";
        }

        if (span.TotalMinutes > 1)
        {
            return $"{(int) Math.Floor(span.TotalMinutes)}分钟前";
        }

        if (span.TotalSeconds >= 1)
        {
            return $"{(int) Math.Floor(span.TotalSeconds)}秒前";
        }

        return "1秒前";
    }

    #endregion
}