using System.Collections;
using System.Globalization;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using BB.Tools.Format;
using Microsoft.VisualBasic;

namespace BB.Tools.Extension;

/// <summary>
/// 用于字符串转换其他类型的扩展函数
/// </summary>
public static class StringExtension
{
    #region 字符串转换其他格式

    /// <summary>
    /// 通用的转换数据类型，支持可空类型
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="convertibleValue"></param>
    /// <example>
    /// <![CDATA[例如："123".ConvertTo<int?>();]]>
    /// </example>
    /// <returns></returns>
    public static T ConvertTo<T>(this IConvertible convertibleValue)
    {
        if (null == convertibleValue)
        {
            return default(T);
        }

        if (!typeof(T).IsGenericType)
        {
            return (T)Convert.ChangeType(convertibleValue, typeof(T));
        }
        else
        {
            Type genericTypeDefinition = typeof(T).GetGenericTypeDefinition();
            if (genericTypeDefinition == typeof(Nullable<>))
            {
                return (T)Convert.ChangeType(convertibleValue, Nullable.GetUnderlyingType(typeof(T)));
            }
        }
        throw new InvalidCastException(
            $"Invalid cast from type \"{convertibleValue.GetType().FullName}\" to type \"{typeof(T).FullName}\".");
    }

    /// <summary>
    /// 转换字符串为日期类型
    /// </summary>
    /// <param name="str">字符串内容</param>
    /// <param name="defaultValue">默认值</param>
    /// <returns></returns>
    public static DateTime ToDateTime(this string str)
    {
        str = ConvertHelper.ConvertToDbc(str);//先转换为半角字符串
        DateTime defaultValue = Convert.ToDateTime("1900-1-1");
        bool converted = DateTime.TryParse(str, out defaultValue);
        return defaultValue;
    }

    /// <summary>
    /// 字符串转换为指定格式的列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">字符串内容</param>
    /// <param name="delimiter">分隔符号</param>
    /// <returns></returns>
    public static List<T> ToDelimitedList<T>(this string value, string delimiter)
    {
        if (value == null)
        {
            return new List<T>();
        }

        var output = value.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
        return output.Select(x => (T)Convert.ChangeType(x.Trim(), typeof(T))).ToList();
    }

    /// <summary>
    /// 字符串转换为指定格式的列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="value">字符串内容</param>
    /// <param name="delimiter">分隔符号</param>
    /// <param name="converter">提供的转换操作</param>
    /// <returns></returns>
    public static List<T> ToDelimitedList<T>(this string value, string delimiter, Func<string, T> converter)
    {
        if (value == null)
        {
            return new List<T>();
        }

        var output = value.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
        return output.Select(converter).ToList();
    }

    /// <summary>
    /// 根据长度分割不同的字符串到列表里面
    /// </summary>
    /// <param name="value">字符串内容</param>
    /// <param name="length">分割的长度</param>
    /// <returns></returns>
    public static IEnumerable<string> SplitEvery(this string value, int length)
    {
        int index = 0;
        while (index + length < value.Length)
        {
            yield return value.Substring(index, length);
            index += length;
        }

        if (index < value.Length)
        {
            yield return value.Substring(index, value.Length - index);
        }
    }

    /// <summary>
    /// 把两个字符串组合为URL的路径形式
    /// </summary>
    /// <param name="val">URL1</param>
    /// <param name="append">增加的URL2</param>
    /// <returns>返回两个URL路径的组合</returns>
    public static string UriCombine(this string val, string append)
    {
        if (string.IsNullOrEmpty(val)) return append;
        if (string.IsNullOrEmpty(append)) return val;

        return val.TrimEnd('/') + "/" + append.TrimStart('/');
    }

    /// <summary>
    /// 将两个路径组合一个合适的路径
    /// </summary>
    /// <param name="val">路径</param>
    /// <param name="path2">追加的路径</param>
    /// <returns></returns>
    public static string PathCombine(this string val, string path2)
    {
        if (Path.IsPathRooted(path2))
        {
            path2 = path2.TrimStart(Path.DirectorySeparatorChar);
            path2 = path2.TrimStart(Path.AltDirectorySeparatorChar);
        }

        return Path.Combine(val, path2);
    }

    /// <summary>
    /// 转换为Camel字符串格式，去掉字符之间的空格以及起始"_"符号
    /// </summary>
    /// <param name="name">待转换字符串</param>
    /// <returns></returns>
    public static string ToCamel(string name)
    {
        string clone = name.TrimStart('_');
        clone = RemoveSpaces(ToProperCase(clone));
        return $"{Char.ToLower(clone[0])}{clone.Substring(1, clone.Length - 1)}";
    }

    /// <summary>
    /// 转换为Capital格式显示，去掉字符之间的空格以及起始"_"符号
    /// </summary>
    /// <param name="name">待转换字符串</param>
    /// <returns></returns>
    public static string ToCapit(String name)
    {
        string clone = name.TrimStart('_');
        return RemoveSpaces(ToProperCase(clone));
    }

    /// <summary>
    /// 移除字符串最后的一个字符
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string RemoveFinalChar(string s)
    {
        if (s.Length > 1)
        {
            s = s.Substring(0, s.Length - 1);
        }
        return s;
    }

    /// <summary>
    /// 移除字符串中最后的一个逗号
    /// </summary>
    /// <param name="s">操作的字符串</param>
    /// <returns></returns>
    public static string RemoveFinalComma(string s)
    {
        if (s.Trim().Length > 0)
        {
            int c = s.LastIndexOf(",");
            if (c > 0)
            {
                s = s.Substring(0, s.Length - (s.Length - c));
            }
        }
        return s;
    }

    /// <summary>
    /// 清除字符间的空格，并转换为合适的大小写
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static string ToTrimmedProperCase(this string s)
    {
        return RemoveSpaces(ToProperCase(s));
    }

    /// <summary>
    /// 在字符串中，指定开始字符和结束字符，提取中间的内容
    /// </summary>
    /// <param name="content">待操作字符串</param>
    /// <param name="start">开始字符</param>
    /// <param name="end">结束字符</param>
    /// <returns></returns>
    public static ArrayList ExtractInnerContent(this string content, string start, string end)
    {
        int sindex = -1, eindex = -1;
        int msindex = -1, meindex = -1;
        int span = 0;

        ArrayList al = new ArrayList();

        sindex = content.IndexOf(start);
        msindex = sindex + start.Length;
        eindex = content.IndexOf(end, msindex);
        span = eindex - msindex;

        if (sindex >= 0 && eindex > sindex)
        {
            al.Add(content.Substring(msindex, span));
        }

        while (sindex >= 0 && eindex > 0)
        {
            sindex = content.IndexOf(start, eindex);
            if (sindex > 0)
            {
                eindex = content.IndexOf(end, sindex);
                msindex = sindex + start.Length;
                span = eindex - msindex;

                if (msindex > 0 && eindex > 0)
                {
                    al.Add(content.Substring(msindex, span));
                }
            }
        }

        return al;
    }

    /// <summary>
    /// 在字符串中，指定开始字符和结束字符，提取非中间的数据
    /// </summary>
    /// <param name="content">待操作的字符</param>
    /// <param name="start">开始字符</param>
    /// <param name="end">结束字符</param>
    /// <returns></returns>
    public static ArrayList ExtractOuterContent(this string content, string start, string end)
    {
        int sindex = -1, eindex = -1;

        ArrayList al = new ArrayList();

        sindex = content.IndexOf(start);
        eindex = content.IndexOf(end);
        if (sindex >= 0 && eindex > sindex)
        {
            al.Add(content.Substring(sindex, eindex + end.Length - sindex));
        }

        while (sindex >= 0 && eindex > 0)
        {
            sindex = content.IndexOf(start, eindex);
            if (sindex > 0)
            {
                eindex = content.IndexOf(end, sindex);
                if (sindex > 0 && eindex > 0)
                {
                    al.Add(content.Substring(sindex, eindex + end.Length - sindex));
                }
            }
        }

        return al;
    }

    /// <summary>
    /// 去除指定字符串前缀的算法
    /// </summary>
    /// <param name="content">待除去特定字符串的内容</param>
    /// <param name="prefixString">特定字符串列表(以逗号,分号,空格等标识)</param>
    /// <returns></returns>
    public static string RemovePrefix(this string content, string prefixString)
    {
        if (string.IsNullOrEmpty(prefixString) || prefixString.Trim() == string.Empty)
        {
            return content;
        }

        char[] splitChars = new[] {',', ';', ' '};            
        string strReturn = content;
        prefixString = prefixString.Trim(splitChars); //过滤前后多余的分隔符号,否则容易出错

        string[] suffixArray = prefixString.Split(splitChars);
        foreach (string suffix in suffixArray)
        {
            int sindex = strReturn.IndexOf(suffix, StringComparison.OrdinalIgnoreCase);// 非大小写敏感
            if (sindex == 0)
            {
                strReturn = strReturn.Substring(suffix.Length);
                break; //匹配一次就应该出来
            }
        }

        return strReturn;
    }

    #endregion

    #region 其他辅助方法

    public static bool IsNullOrEmpty(this string? str)
    {
        return string.IsNullOrEmpty(str);
    }

    public static bool IsNullOrWhiteSpace(this string? str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    public static string Format(this string inputStr, params object[] obj)
    {
        return string.Format(inputStr, obj);
    }

    public static string Sub(this string inputStr, int length)
    {
        return inputStr.IsNullOrEmpty()
            ? (string)null
            : (inputStr.Length >= length ? inputStr.Substring(0, length) : inputStr);
    }

    public static string Fmt(this string text, params object[] args)
    {
        if (string.IsNullOrEmpty(text))
        {
            return string.Empty;
        }

        return args == null ? text : string.Format(CultureInfo.CurrentCulture, text, args);
    }

    /// <summary>
    /// 如果是有效的Email地址，则返回True
    /// </summary>
    /// <param name="email">邮件地址</param>
    /// <returns>如果是有效的Email地址，则返回True</returns>
    public static bool IsValidEmailAddress(this string email)
    {
        if (string.IsNullOrEmpty(email)) return false;

        return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$").IsMatch(email);

        //const string expresion = @"^(?:[a-zA-Z0-9_'^&/+-])+(?:\.(?:[a-zA-Z0-9_'^&/+-])+)*@(?:(?:\[?(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?))\.){3}(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\]?)|(?:[a-zA-Z0-9-]+\.)+(?:[a-zA-Z]){2,}\.?)$";
        //Regex regex = new Regex(expresion, RegexOptions.IgnoreCase);
        //return regex.IsMatch(email);      
    }

    /// <summary>
    /// 检查URL是否有效
    /// </summary>
    /// <param name="url">URL地址</param>
    /// <returns></returns>
    public static bool IsValidUrl(this string url)
    {
        string strRegex = "^(https?://)"
                          + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@
                          + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP- 199.194.52.184
                          + "|" // allows either IP or domain
                          + @"([0-9a-z_!~*'()-]+\.)*" // tertiary domain(s)- www.
                          + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]" // second level domain
                          + @"(\.[a-z]{2,6})?)" // first level domain- .com or .museum is optional
                          + "(:[0-9]{1,5})?" // port number- :80
                          + "((/?)|" // a slash isn't required if there is no file name
                          + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
        return new Regex(strRegex).IsMatch(url);
    }

    /// <summary>
    /// 检查字符串是否是有效的URI
    /// </summary>
    /// <param name="val">输入字符串</param>
    /// <returns>如果它是有效的URI，则返回true</returns>
    public static bool IsValidUri(this string val)
    {
        if (string.IsNullOrEmpty(val)) return false;

        return Uri.IsWellFormedUriString(val, UriKind.Absolute);
    }

    /// <summary>
    /// 检查URL(http)是否可用.
    /// </summary>
    /// <param name="httpUri">URL地址</param>
    /// <example>
    /// string url = "www.codeproject.com;
    /// if( !url.UrlAvailable())
    ///     ...codeproject is not available
    /// </example>
    /// <returns>true if available</returns>
    public static bool UrlAvailable(this string httpUrl)
    {
        if (!httpUrl.StartsWith("http://") || !httpUrl.StartsWith("https://"))
            httpUrl = "http://" + httpUrl;
        try
        {
            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create(httpUrl);
            myRequest.Method = "GET";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            HttpWebResponse myHttpWebResponse = (HttpWebResponse)myRequest.GetResponse();
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 反转字符串内容的顺序
    /// </summary>
    /// <param name="input">输入的字符串</param>
    /// <returns></returns>
    public static string Reverse(this string input)
    {
        char[] charArray = null;
        string ret = string.Empty;
        if (input != null)
        {
            charArray = input.ToCharArray();
            Array.Reverse(charArray);
            ret = new string(charArray);
        }
        return ret;
    }
        
    /// <summary>
    /// 减少字符串到更短的预览，可选地由某些字符串（...）结束。
    /// </summary>
    /// <param name="s">待减少的字符串</param>
    /// <param name="count">返回字符串的长度，包括结尾。</param>
    /// <param name="endings">缩减文本的可选结尾</param>
    /// <example>
    /// string description = "This is very long description of something";
    /// string preview = description.Reduce(20,"...");
    /// produce -> "This is very long..."
    /// </example>
    /// <returns></returns>
    public static string Reduce(this string s, int count, string endings)
    {
        if (count < endings.Length)
            throw new Exception("Failed to reduce to less then endings length.");
        int sLength = s.Length;
        int len = sLength;
        if (endings != null)
            len += endings.Length;
        if (count > sLength)
            return s; //it's too short to reduce
        s = s.Substring(0, sLength - len + count);
        if (endings != null)
            s += endings;
        return s;
    }

    /// <summary>
    /// 从字符串中替换指定的字符，可以指定是否大小写敏感。
    /// </summary>
    /// <param name="str">输入的字符串</param>
    /// <param name="find">查找字符串</param>
    /// <param name="replacement">替换字符串</param>
    /// <param name="caseSensitive">是否大小写敏感</param>
    /// <returns></returns>
    public static string Replace(this string str, string find, string replacement, bool caseSensitive)
    {
        if (caseSensitive)
            return Strings.Replace(str, find, replacement, 1, -1, CompareMethod.Binary);
        else
            return Strings.Replace(str, find, replacement, 1, -1, CompareMethod.Text);
    }

    /// <summary>
    /// 在解析用户输入如电话，价格的时候，删除不是行末端的空白
    /// int.Parse("1 000 000".RemoveSpaces(),.....
    /// </summary>
    /// <param name="s"></param>
    public static string RemoveSpaces(this string s)
    {
        return s.Trim().Replace(" ", "");
    }

    /// <summary>
    /// 如果指定为浮点数，数值能够转换为Double类型，返回True；否则Int32也认为是数值（中间空格会被忽略）
    /// </summary>
    /// <param name="s">输入字符串</param>
    /// <param name="floatpoint">如果指定为浮点数，则为True，否则为Int32类型</param>
    /// <returns>如果字符串只包含数字或浮点数，则返回True</returns>
    public static bool IsNumber(this string s, bool floatpoint)
    {
        int i;
        double d;
        string withoutWhiteSpace = s.RemoveSpaces();
        if (floatpoint)
        {
            return double.TryParse(withoutWhiteSpace, NumberStyles.Any,
                Thread.CurrentThread.CurrentUICulture, out d);
        }
        else
        {
            return int.TryParse(withoutWhiteSpace, out i);
        }
    }

    /// <summary>
    /// 如果字符串只包含数字或浮点数则为True，不考虑空格
    /// </summary>
    /// <param name="s">输入字符串</param>
    /// <param name="floatpoint">如果指定为浮点数，则为True</param>
    /// <returns>如果字符串只包含数字或浮点数则为True</returns>
    public static bool IsNumberOnly(this string s, bool floatpoint)
    {
        s = s.Trim();
        if (s.Length == 0)
            return false;
        foreach (char c in s)
        {
            if (!char.IsDigit(c))
            {
                if (floatpoint && (c == '.' || c == ','))
                    continue;
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 从字符串中删除重音
    /// </summary>
    /// <example>
    ///  input:  "Příliš žluťoučký kůň úpěl ďábelské ódy."
    ///  result: "Prilis zlutoucky kun upel dabelske ody."
    /// </example>
    /// <param name="s"></param>
    /// <remarks>founded at http://stackoverflow.com/questions/249087/
    /// how-do-i-remove-diacritics-accents-from-a-string-in-net</remarks>
    /// <returns>string without accents</returns>
    public static string RemoveDiacritics(this string s)
    {
        string stFormD = s.Normalize(NormalizationForm.FormD);
        StringBuilder sb = new StringBuilder();

        for (int ich = 0; ich < stFormD.Length; ich++)
        {
            UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
            if (uc != UnicodeCategory.NonSpacingMark)
            {
                sb.Append(stFormD[ich]);
            }
        }
        return (sb.ToString().Normalize(NormalizationForm.FormC));
    }

    /// <summary>
    /// 使用<br/>替换 \r\n 或者 \n
    /// </summary>
    /// <param name="s">输入字符串</param>
    /// <returns></returns>
    public static string Nl2Br(this string s)
    {
        return s.Replace("\r\n", "<br />").Replace("\n", "<br />");
    }


    static MD5CryptoServiceProvider _sMd5 = null;
    /// <summary>
    /// 使用MD5加密字符串
    /// </summary>
    /// <param name="s">输入字符串</param>
    /// <returns></returns>
    public static string Md5(this string s)
    {
        if (_sMd5 == null) //creating only when needed
            _sMd5 = new MD5CryptoServiceProvider();
        byte[] newdata = Encoding.Default.GetBytes(s);
        byte[] encrypted = _sMd5.ComputeHash(newdata);
        return BitConverter.ToString(encrypted).Replace("-", "").ToLower();
    }


    /// <summary>
    /// 用字符串中的另一个字符代替给定的字符。 
    /// 替换不区分大小写
    /// </summary>
    /// <param name="val"></param>
    /// <param name="charToReplace">The character to replace</param>
    /// <param name="replacement">The character by which to be replaced</param>
    /// <returns>Copy of string with the characters replaced</returns>
    public static string CaseInsenstiveReplace(this string val, char charToReplace, char replacement)
    {
        Regex regEx = new Regex(charToReplace.ToString(), RegexOptions.IgnoreCase | RegexOptions.Multiline);
        return regEx.Replace(val, replacement.ToString());
    }
    /// <summary>
    /// 用字符串中的另一个字符串代替给定的字符串。 
    /// 替换不区分大小写
    /// </summary>
    /// <param name="val"></param>
    /// <param name="stringToReplace">The string to replace</param>
    /// <param name="replacement">The string by which to be replaced</param>
    /// <returns>Copy of string with the string replaced</returns>
    public static string CaseInsenstiveReplace(this string val, string stringToReplace, string replacement)
    {
        Regex regEx = new Regex(stringToReplace, RegexOptions.IgnoreCase | RegexOptions.Multiline);
        return regEx.Replace(val, replacement);
    }


    /// <summary>
    /// 删除字符串中出现的单词，匹配区分大小写。
    /// </summary>
    /// <param name="val"></param>
    /// <param name="filterWords">Array of words to be removed from the string</param>
    /// <returns>Copy of the string with the words removed</returns>
    public static string RemoveWords(this string val, params string[] filterWords)
    {
        return MaskWords(val, char.MinValue, filterWords);
    }

    /// <summary>
    /// 用给定字符掩码字符串中的单词的出现
    /// </summary>
    /// <param name="val"></param>
    /// <param name="mask">The character mask to apply</param>
    /// <param name="filterWords">The words to be replaced</param>
    /// <returns>The copy of string with the mask applied</returns>
    public static string MaskWords(this string val, char mask, params string[] filterWords)
    {
        string stringMask = mask == char.MinValue ?
            string.Empty : mask.ToString();
        string totalMask = stringMask;

        foreach (string s in filterWords)
        {
            Regex regEx = new Regex(s, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (stringMask.Length > 0)
            {
                for (int i = 1; i < s.Length; i++)
                    totalMask += stringMask;
            }

            val = regEx.Replace(val, totalMask);
            totalMask = stringMask;
        }
        return val;
    }

    /// <summary>
    /// 在传递的字符总数中,将传递的字符串换行
    /// </summary>
    /// <param name="val"></param>
    /// <param name="charCount">The number of characters after which it should wrap the text</param>
    /// <returns>The copy of the string after applying the Wrap</returns>
    public static string WordWrap(this string val, int charCount)
    {
        return WordWrap(val, charCount, false, Environment.NewLine);
    }

    /// <summary>
    /// Wraps the passed string at the passed total number of characters (if cuttOff is true)
    /// or at the next whitespace (if cutOff is false). 
    /// Uses the environment new line symbol for the break text
    /// </summary>
    /// <param name="val"></param>
    /// <param name="charCount">The number of characters after which to break</param>
    /// <param name="cutOff">true to break at specific</param>
    /// <returns></returns>
    public static string WordWrap(this string val, int charCount, bool cutOff)
    {
        return WordWrap(val, charCount, cutOff, Environment.NewLine);
    }

    private static string WordWrap(this string val, int charCount, bool cutOff, string breakText)
    {
        StringBuilder sb = new StringBuilder(val.Length + 100);
        int counter = 0;

        if (cutOff)
        {
            while (counter < val.Length)
            {
                if (val.Length > counter + charCount)
                {
                    sb.Append(val.Substring(counter, charCount));
                    sb.Append(breakText);
                }
                else
                {
                    sb.Append(val.Substring(counter));
                }
                counter += charCount;
            }
        }
        else
        {
            string[] strings = val.Split(' ');
            for (int i = 0; i < strings.Length; i++)
            {
                // added one to represent the space.
                counter += strings[i].Length + 1;
                if (i != 0 && counter > charCount)
                {
                    sb.Append(breakText);
                    counter = 0;
                }

                sb.Append(strings[i] + ' ');
            }
        }
        // to get rid of the extra space at the end.
        return sb.ToString().TrimEnd();
    }


    /// <summary>
    /// Converts an list of string to CSV string representation.
    /// 
    /// </summary>
    /// <param name="val"></param>
    /// <param name="insertSpaces">True to add spaces after each comma</param>
    /// <returns>CSV representation of the data</returns>
    public static string ToCsv(this IEnumerable<string> val, bool insertSpaces)
    {
        if (insertSpaces)
            return string.Join(", ", val.ToArray());
        else
            return string.Join(",", val.ToArray());
    }
    /// <summary>
    /// Converts an list of characters to CSV string representation.
    /// 
    /// </summary>
    /// <param name="val"></param>
    /// <param name="insertSpaces">True to add spaces after each comma</param>
    /// <returns>CSV representation of the data</returns>
    public static string ToCsv(this IEnumerable<char> val, bool insertSpaces)
    {
        List<string> casted = new List<string>();
        foreach (var item in val)
            casted.Add(item.ToString());

        if (insertSpaces)
            return string.Join(", ", casted.ToArray());
        else
            return string.Join(",", casted.ToArray());
    }
    /// <summary>
    /// Converts CSV to list of string.
    /// 
    /// </summary>
    /// <param name="val"></param>
    /// <returns>IEnumerable collection of string</returns>
    public static IEnumerable<string> ListFromCsv(this string val)
    {
        string[] split = val.Split(',');
        foreach (string item in split)
        {
            item.Trim();
        }
        return new List<string>(split);
    }
    
    /// <summary>
    /// 将字符串转换为合适的大小写
    /// </summary>
    /// <param name="s">操作的字符串</param>
    /// <returns></returns>
    public static string ToProperCase(this string s)
    {
        var revised = "";
        if (s.Length > 0)
        {
            if (s.IndexOf(" ", StringComparison.Ordinal) > 0)
            {
                TextInfo ti = new CultureInfo("en-US",false).TextInfo;
                revised = ti.ToTitleCase(s);
            }
            else
            {
                // s[..1] 等于 s.Substring(0, 1)
                string firstLetter = s[..1].ToUpper(new CultureInfo("en-US"));
                revised = firstLetter + s.Substring(1, s.Length - 1);
            }
        }
        return revised;
    }

    #endregion
}