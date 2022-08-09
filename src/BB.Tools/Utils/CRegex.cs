using System.Data;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace BB.Tools.Utils
{
    /// <summary>
    /// 正则表达式辅助类
    /// </summary>
    public partial class CRegex
    {
        #region BaseMethod

        /// <summary>
        /// 内容是否匹配指定的表达式
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        /// <returns></returns>
        public static bool IsMatch(string sInput, string sRegex)
        {
            if (sRegex == "")
                return true;
            Regex re = new Regex(sRegex,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            return re.Match(sInput).Success;
        }


        /// <summary>
        /// 多个匹配内容
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        /// <param name="iGroupIndex">第几个分组, 从1开始, 0代表不分组</param>
        public static List<string> GetList(string sInput, string sRegex, int iGroupIndex)
        {
            List<string> list = new List<string>();
            Regex re = new Regex(sRegex,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            MatchCollection mcs = re.Matches(sInput);
            foreach (Match mc in mcs)
            {
                if (iGroupIndex > 0)
                {
                    list.Add(mc.Groups[iGroupIndex].Value);
                }
                else
                {
                    list.Add(mc.Value);
                }
            }

            return list;
        }

        /// <summary>
        /// 多个匹配内容
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        /// <param name="sGroupName">分组名, ""代表不分组</param>
        public static List<string> GetList(string sInput, string sRegex, string sGroupName)
        {
            List<string> list = new List<string>();
            Regex re = new Regex(sRegex,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            MatchCollection mcs = re.Matches(sInput);
            foreach (Match mc in mcs)
            {
                if (sGroupName != "")
                {
                    list.Add(mc.Groups[sGroupName].Value);
                }
                else
                {
                    list.Add(mc.Value);
                }
            }

            return list;
        }

        /// <summary>
        /// 单个匹配内容
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        /// <param name="iGroupIndex">分组序号, 从1开始, 0不分组</param>
        public static string GetText(string sInput, string sRegex, int iGroupIndex)
        {
            Regex re = new Regex(sRegex,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            Match mc = re.Match(sInput);
            string result = "";
            if (mc.Success)
            {
                if (iGroupIndex > 0)
                {
                    result = mc.Groups[iGroupIndex].Value;
                }
                else
                {
                    result = mc.Value;
                }
            }

            return result;
        }

        /// <summary>
        /// 单个匹配内容
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        /// <param name="sGroupName">分组名, ""代表不分组</param>
        public static string GetText(string sInput, string sRegex, string sGroupName)
        {
            Regex re = new Regex(sRegex,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            Match mc = re.Match(sInput);
            string result = "";
            if (mc.Success)
            {
                if (sGroupName != "")
                {
                    result = mc.Groups[sGroupName].Value;
                }
                else
                {
                    result = mc.Value;
                }
            }

            return result;
        }

        /// <summary>
        /// 替换指定内容
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        /// <param name="sReplace">替换值</param>
        /// <param name="iGroupIndex">分组序号, 0代表不分组</param>
        public static string Replace(string sInput, string sRegex, string sReplace, int iGroupIndex)
        {
            Regex re = new Regex(sRegex,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            MatchCollection mcs = re.Matches(sInput);
            foreach (Match mc in mcs)
            {
                if (iGroupIndex > 0)
                {
                    sInput = sInput.Replace(mc.Groups[iGroupIndex].Value, sReplace);
                }
                else
                {
                    sInput = sInput.Replace(mc.Value, sReplace);
                }
            }

            return sInput;
        }

        /// <summary>
        /// 替换指定内容
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        /// <param name="sReplace">替换值</param>
        /// <param name="sGroupName">分组名, "" 代表不分组</param>
        public static string Replace(string sInput, string sRegex, string sReplace, string sGroupName)
        {
            Regex re = new Regex(sRegex,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            MatchCollection mcs = re.Matches(sInput);
            foreach (Match mc in mcs)
            {
                if (sGroupName != "")
                {
                    sInput = sInput.Replace(mc.Groups[sGroupName].Value, sReplace);
                }
                else
                {
                    sInput = sInput.Replace(mc.Value, sReplace);
                }
            }

            return sInput;
        }

        /// <summary>
        /// 使用列表集合，依次替换指定匹配内容
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        /// <param name="sReplaceList">替换值的列表</param>
        public static string Replace(string sInput, string sRegex, List<string> sReplaceList)
        {
            Regex re = new Regex(sRegex,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            MatchCollection mcs = re.Matches(sInput);
            for (int i = 0; i < mcs.Count; i++)
            {
                //如果替换表有对应位置的值，那么使用这个值，否则如果集合数量不够，则取其最后一个
                var matchIndexValue = (sReplaceList.Count > i) ? sReplaceList[i] : sReplaceList[sReplaceList.Count - 1];
                sInput = re.Replace(sInput, delegate(Match match) { return matchIndexValue; }, 1);
            }

            return sInput;
        }

        /// <summary>
        /// 分割指定内容
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        /// <param name="iStrLen">最小保留字符串长度</param>
        public static List<string> Split(string sInput, string sRegex, int iStrLen)
        {
            Regex re = new Regex(sRegex,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            string[] sArray = re.Split(sInput);
            List<string> list = new List<string>();
            list.Clear();
            foreach (string s in sArray)
            {
                if (s.Trim().Length < iStrLen)
                    continue;

                list.Add(s.Trim());
            }

            return list;
        }

        #endregion BaseMethod

        #region 获得特定内容

        /// <summary>
        /// 多个链接
        /// </summary>
        /// <param name="sInput">输入内容</param>
        public static List<string> GetLinks(string sInput)
        {
            return GetList(sInput, @"<a[^>]+href=\s*(?:'(?<href>[^']+)'|""(?<href>[^""]+)""|(?<href>[^>\s]+))\s*[^>]*>",
                "href");
        }

        /// <summary>
        /// 单个链接
        /// </summary>
        /// <param name="sInput">输入内容</param>
        public static string GetLink(string sInput)
        {
            return GetText(sInput, @"<a[^>]+href=\s*(?:'(?<href>[^']+)'|""(?<href>[^""]+)""|(?<href>[^>\s]+))\s*[^>]*>",
                "href");
        }

        /// <summary>
        /// 图片标签
        /// </summary>
        /// <param name="sInput">输入内容</param>
        public static List<string> GetImgTag(string sInput)
        {
            return GetList(sInput, "<img[^>]+src=\\s*(?:'(?<src>[^']+)'|\"(?<src>[^\"]+)\"|(?<src>[^>\\s]+))\\s*[^>]*>",
                "");
        }

        /// <summary>
        /// 图片地址
        /// </summary>
        /// <param name="sInput">输入内容</param>
        public static string GetImgSrc(string sInput)
        {
            return GetText(sInput, "<img[^>]+src=\\s*(?:'(?<src>[^']+)'|\"(?<src>[^\"]+)\"|(?<src>[^>\\s]+))\\s*[^>]*>",
                "src");
        }

        /// <summary>
        /// 根据URL获得域名
        /// </summary>
        /// <param name="sInput">输入内容</param>
        public static string GetDomain(string sInput)
        {
            return GetText(sInput, @"http(s)?://([\w-]+\.)+(\w){2,}", 0);
        }

        #endregion 获得特定内容

        #region 根据表达式,获得文章内容

        /// <summary>
        /// 文章标题
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        public static string GetTitle(string sInput, string sRegex)
        {
            string sTitle = GetText(sInput, sRegex, "Title");
            sTitle = ClearTag(sTitle);
            if (sTitle.Length > 99)
            {
                sTitle = sTitle.Substring(0, 99);
            }

            return sTitle;
        }

        /// <summary>
        /// 网页标题
        /// </summary>
        public static string GetTitle(string sInput)
        {
            return GetText(sInput, @"<Title[^>]*>(?<Title>[\s\S]{10,})</Title>", "Title");
        }

        /// <summary>
        /// 网页内容
        /// </summary>
        /// <param name="sInput">输入内容</param>
        public static string GetHtml(string sInput)
        {
            return Replace(sInput, @"(?<Head>[^<]+)<", "", "Head");
        }

        /// <summary>
        /// 网页Body内容
        /// </summary>
        public static string GetBody(string sInput)
        {
            return GetText(sInput, @"<Body[^>]*>(?<Body>[\s\S]{10,})</body>", "Body");
        }

        /// <summary>
        /// 网页Body内容
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        public static string GetBody(string sInput, string sRegex)
        {
            return GetText(sInput, sRegex, "Body");
        }

        /// <summary>
        /// 文章来源
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        public static string GetSource(string sInput, string sRegex)
        {
            string sSource = GetText(sInput, sRegex, "Source");
            sSource = ClearTag(sSource);
            if (sSource.Length > 99)
                sSource = sSource.Substring(0, 99);
            return sSource;
        }

        /// <summary>
        /// 作者名
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        public static string GetAuthor(string sInput, string sRegex)
        {
            string sAuthor = GetText(sInput, sRegex, "Author");
            sAuthor = ClearTag(sAuthor);
            if (sAuthor.Length > 99)
                sAuthor = sAuthor.Substring(0, 99);
            return sAuthor;
        }

        /// <summary>
        /// 分页链接地址
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        public static List<string> GetPageLinks(string sInput, string sRegex)
        {
            return GetList(sInput, sRegex, "href");
        }

        /// <summary>
        /// 根据相对路径得到绝对路径
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRelativeUrl">相对链接地址</param>
        public static string GetUrl(string sInput, string sRelativeUrl)
        {
            string sReturnUrl = "";
            string sUrl = _GetStandardUrlDepth(sInput); //返回了http://www.163.com/news/这种形式

            if (sRelativeUrl.ToLower().StartsWith("http") || sRelativeUrl.ToLower().StartsWith("https"))
            {
                sReturnUrl = sRelativeUrl.Trim();
            }
            else if (sRelativeUrl.StartsWith("/"))
            {
                sReturnUrl = GetDomain(sInput) + sRelativeUrl;
            }
            else if (sRelativeUrl.StartsWith("../"))
            {
                sUrl = sUrl.Substring(0, sUrl.Length - 1);
                while (sRelativeUrl.IndexOf("../") >= 0)
                {
                    string temp = GetPreStrByLast(sUrl, "/");
                    if (temp.Length > 6)
                    {
                        //temp != "http:/"，否则的话，说明已经回溯到尽头了，"../"与网址的层次对应不上。存在这种情况，网页上面的链接是错误的，但浏览器还能正常显示
                        sUrl = temp;
                    }

                    sRelativeUrl = sRelativeUrl.Substring(3);
                }

                sReturnUrl = sUrl + "/" + sRelativeUrl.Trim();
            }
            else if (sRelativeUrl.StartsWith("./"))
            {
                sReturnUrl = sUrl + sRelativeUrl.Trim().Substring(2);
            }
            else if (sRelativeUrl.Trim() != "")
            {
                //2007images/modecss.css
                sReturnUrl = sUrl + sRelativeUrl.Trim();
            }
            else
            {
                sRelativeUrl = sUrl;
            }

            return sReturnUrl;
        }

        /// <summary>
        /// 获得标准的URL路径深度
        /// </summary>
        /// <param name="url"></param>
        /// <returns>返回标准的形式：http://www.163.com/或http://www.163.com/news/。</returns>
        private static string _GetStandardUrlDepth(string url)
        {
            string sheep = url.Trim().ToLower();
            string header = "http://";
            if (sheep.IndexOf("https://") != -1)
            {
                header = "https://";
                sheep = sheep.Replace("https://", "");
            }
            else
            {
                sheep = sheep.Replace("http://", "");
            }

            int p = sheep.LastIndexOf("/");
            if (p == -1)
            {
                //www.163.com
                sheep += "/";
            }
            else if (p == sheep.Length - 1)
            {
                //传来的是：http://www.163.com/news/
            }
            else if (sheep.Substring(p).IndexOf(".") != -1)
            {
                //传来的是：http://www.163.com/news/hello.htm 这种形式
                sheep = sheep.Substring(0, p + 1);
            }
            else
            {
                sheep += "/";
            }

            return header + sheep;
        }

        /// <summary>
        /// 关键字
        /// </summary>
        /// <param name="sInput">输入内容</param>
        public static string GetKeyWord(string sInput)
        {
            List<string> list = Split(sInput, "(,|，|\\+|＋|。|;|；|：|:|“)|”|、|_|\\(|（|\\)|）", 2);
            List<string> listReturn = new List<string>();
            Regex re;
            foreach (string str in list)
            {
                re = new Regex(@"[a-zA-z]+", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                MatchCollection mcs = re.Matches(str);
                string sTemp = str;
                foreach (Match mc in mcs)
                {
                    if (mc.Value.Length > 2)
                        listReturn.Add(mc.Value);
                    sTemp = sTemp.Replace(mc.Value, ",");
                }

                re = new Regex(@",{1}", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
                mcs = re.Matches(sTemp);
                foreach (string s in re.Split(sTemp))
                {
                    if (s.Trim().Length <= 2)
                        continue;
                    listReturn.Add(s);
                }
            }

            string sReturn = "";
            for (int i = 0; i < listReturn.Count - 1; i++)
            {
                for (int j = i + 1; j < listReturn.Count; j++)
                {
                    if (listReturn[i] == listReturn[j])
                    {
                        listReturn[j] = "";
                    }
                }
            }

            foreach (string str in listReturn)
            {
                if (str.Length > 2)
                    sReturn += str + ",";
            }

            if (sReturn.Length > 0)
                sReturn = sReturn.Substring(0, sReturn.Length - 1);
            else
                sReturn = sInput;
            if (sReturn.Length > 99)
                sReturn = sReturn.Substring(0, 99);
            return sReturn;
        }

        /// <summary>
        /// 发布日期
        /// </summary>
        /// <param name="sInput">输入内容</param>
        /// <param name="sRegex">表达式字符串</param>
        public static DateTime GetCreateDate(string sInput, string sRegex)
        {
            DateTime dt = DateTime.Now;
            Regex re = new Regex(sRegex,
                RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
            Match mc = re.Match(sInput);
            if (mc.Success)
            {
                try
                {
                    int iYear = int.Parse(mc.Groups["Year"].Value);
                    int iMonth = int.Parse(mc.Groups["Month"].Value);
                    int iDay = int.Parse(mc.Groups["Day"].Value);
                    int iHour = dt.Hour;
                    int iMinute = dt.Minute;

                    string sHour = mc.Groups["Hour"].Value;
                    string sMintue = mc.Groups["Mintue"].Value;

                    if (sHour != "")
                        iHour = int.Parse(sHour);
                    if (sMintue != "")
                        iMinute = int.Parse(sMintue);

                    dt = new DateTime(iYear, iMonth, iDay, iHour, iMinute, 0);
                }
                catch
                {
                    // ignored
                }
            }

            return dt;
        }

        public static string GetContent(string sOriContent, string sOtherRemoveReg, string sPageUrl,
            DataTable dtAntiLink)
        {
            string sFormartted = sOriContent;

            //去掉有危险的标记
            sFormartted = Regex.Replace(sFormartted, @"<script[\s\S]*?</script>", "",
                RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
            sFormartted = Regex.Replace(sFormartted, @"<iframe[^>]*>[\s\S]*?</iframe>", "",
                RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
            Regex r = new Regex(
                @"<input[\s\S]+?>|<form[\s\S]+?>|</form[\s\S]*?>|<select[\s\S]+?>?</select>|<textarea[\s\S]*?>?</textarea>|<file[\s\S]*?>|<noscript>|</noscript>",
                RegexOptions.IgnoreCase);
            sFormartted = r.Replace(sFormartted, "");
            string[] sOtherReg = sOtherRemoveReg.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string sRemoveReg in sOtherReg)
            {
                sFormartted = Replace(sFormartted, sRemoveReg, "", 0);
            }

            //图片路径
            //sFormartted = _ReplaceUrl("<img[^>]+src\\s*=\\s*(?:'(?<src>[^']+)'|\"(?<src>[^\"]+)\"|(?<src>[^>\\s]+))\\s*[^>]*>", "src", sFormartted,sPageUrl);
            sFormartted =
                _ReplaceUrl(
                    "<img[\\s\\S]+?src\\s*=\\s*(?:'(?<src>[^']+)'|\"(?<src>[^\"]+)\"|(?<src>[^>\\s]+))\\s*[^>]*>",
                    "src", sFormartted, sPageUrl);
            //反防盗链
            string domain = GetDomain(sPageUrl);
            DataRow[] drs = dtAntiLink.Select("Domain='" + domain + "'");
            if (drs.Length > 0)
            {
                foreach (DataRow dr in drs)
                {
                    switch (Convert.ToInt32(dr["Type"]))
                    {
                        case 1: //置换
                            sFormartted = sFormartted.Replace(dr["imgUrl"].ToString(),
                                "http://stat.580k.com/t.asp?url=");
                            break;
                        default: //附加
                            sFormartted = sFormartted.Replace(dr["imgUrl"].ToString(),
                                "http://stat.580k.com/t.asp?url=" + dr["imgUrl"]);
                            break;
                    }
                }
            }

            //A链接
            sFormartted =
                _ReplaceUrl(@"<a[^>]+href\s*=\s*(?:'(?<href>[^']+)'|""(?<href>[^""]+)""|(?<href>[^>\s]+))\s*[^>]*>",
                    "href", sFormartted, sPageUrl);

            //CSS
            sFormartted =
                _ReplaceUrl(@"<link[^>]+href\s*=\s*(?:'(?<href>[^']+)'|""(?<href>[^""]+)""|(?<href>[^>\s]+))\s*[^>]*>",
                    "href", sFormartted, sPageUrl);

            //BACKGROUND
            sFormartted = _ReplaceUrl(@"background\s*=\s*(?:'(?<img>[^']+)'|""(?<img>[^""]+)""|(?<img>[^>\s]+))", "img",
                sFormartted, sPageUrl);
            //style方式的背景：background-image:url(...)
            sFormartted = _ReplaceUrl(@"background-image\s*:\s*url\s*\x28(?<img>[^\x29]+)\x29", "img", sFormartted,
                sPageUrl);

            //FLASH
            sFormartted = _ReplaceUrl(@"<param\s[^>]+""movie""[^>]+value\s*=\s*""(?<flash>[^"">]+\x2eswf)""[^>]*>",
                "flash", sFormartted, sPageUrl);

            //XSL
            if (IsXml(sFormartted))
            {
                sFormartted =
                    _ReplaceUrl(
                        @"<\x3fxml-stylesheet\s+[^\x3f>]+href=\s*(?:'(?<href>[^']+)'|""(?<href>[^""]+)"")\s*[^\x3f>]*\x3f>",
                        "href", sFormartted, sPageUrl);
            }

            //script
            //sFormartted = _ReplaceUrl(@"<script[^>]+src\s*=\s*(?:'(?<src>[^']+)'|""(?<src>[^""]+)""|(?<src>[^>\s]+))\s*[^>]*>", "src", sFormartted,sPageUrl);

            return sFormartted;
        }

        /// <summary>
        /// 置换连接
        /// </summary>
        private static string _ReplaceUrl(string strRe, string subMatch, string sFormartted, string sPageUrl)
        {
            Regex re = new Regex(strRe,
                RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace | RegexOptions.IgnoreCase);
            MatchCollection mcs = re.Matches(sFormartted);
            string sOriStr = "";
            string sSubMatch = "";
            string sReplaceStr = "";
            foreach (Match mc in mcs)
            {
                sOriStr = mc.Value;
                sSubMatch = mc.Groups[subMatch].Value;
                sReplaceStr = sOriStr.Replace(sSubMatch, GetUrl(sPageUrl, sSubMatch));
                sFormartted = sFormartted.Replace(sOriStr, sReplaceStr);
            }

            return sFormartted;
        }

        /// <summary>
        /// 判断是否为XML
        /// </summary>
        /// <param name="sFormartted"></param>
        /// <returns></returns>
        public static bool IsXml(string sFormartted)
        {
            Regex re = new Regex(@"<\x3fxml\s+", RegexOptions.IgnoreCase);
            MatchCollection mcs = re.Matches(sFormartted);
            return (mcs.Count > 0);
        }

        #endregion 根据表达式，获得文章内容

        #region 其他正则处理

        /// <summary>
        /// 检测字符串中是否包含符合正则的子集
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="reg">正则, e.g. \d+</param>
        /// <returns>true:包含，反之不包含</returns>
        public bool CheckContains(string source, string reg)
        {
            return Regex.Match(source, reg).Success;
        }

        /// <summary>
        /// 检测整个字符串是否能匹配正则,而不是包含
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="reg">正则, e.g. ^\d+$</param>
        /// <returns>true:匹配，反之不匹配</returns>
        public bool CheckString(string source, string reg)
        {
            Regex rg = new Regex(reg, RegexOptions.IgnoreCase);
            return rg.IsMatch(source);
        }

        /// <summary>
        /// 从指定字符串中过滤出第一个符合正则匹配的子集
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="reg">正则, e.g. \d+</param>
        /// <returns>源字符串的第一个匹配的子集</returns>
        public string GetFirstString(string source, string reg)
        {
            return Regex.Match(source, reg).Groups[0].Value;
        }

        /// <summary>
        /// 从指定字符串中过滤出所有符合正则匹配的子集
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="reg">正则, e.g. \d+</param>
        /// <returns>true:匹配，反之不匹配</returns>
        public List<string> GetString(string source, string reg)
        {
            var regex = Regex.Matches(source, reg);
            List<string> list = new List<string>();
            foreach (Match item in regex)
            {
                list.Add(item.Value);
            }

            return list;
        }

        /// <summary>
        /// 从指定字符串中过滤出第一个数字
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>源字符串的第一个数字</returns>
        public string GetFirstNumber(string source)
        {
            return Regex.Match(source, @"\d+").Groups[0].Value;
        }

        /// <summary>
        /// 从指定字符串中过滤出最后一个数字
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>源字符串的最后一个数字</returns>
        public string GetLastNumber(string source)
        {
            var reg = Regex.Matches(source, @"\d+");
            return reg[reg.Count - 1].Value;
        }

        /// <summary>
        /// 从指定字符串中过滤出所有数字
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>源字符串的所有数字</returns>
        public List<string> GetAllNumber(string source)
        {
            var reg = Regex.Matches(source, @"\d+");
            List<string> list = new List<string>();
            foreach (Match item in reg)
            {
                list.Add(item.Value);
            }

            return list;
        }

        /// <summary>
        /// 检测源字符串中是否包含数字
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>true:源字符串包含数字;false:源字符串不包含数字</returns>
        public bool CheckNumber(string source)
        {
            return Regex.Match(source, @"\d").Success;
        }

        /// <summary>
        /// 判断字符串是否全部是数字且长度等于指定长度
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="length">指定长度</param>
        /// <returns>返回值</returns>
        public bool CheckLength(string source, int length)
        {
            Regex rg = new Regex(@"^\d{" + length + "}$");
            return rg.IsMatch(source);
        }

        /// <summary>
        /// 截取字符串中开始和结束字符串中间的字符串
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="startStr">开始字符串</param>
        /// <param name="endStr">结束字符串</param>
        /// <returns>中间字符串</returns>
        public string Substring(string source, string startStr, string endStr)
        {
            Regex rg = new Regex("(?<=(" + startStr + "))[.\\s\\S]*?(?=(" + endStr + "))",
                RegexOptions.Multiline | RegexOptions.Singleline);
            return rg.Match(source).Value;
        }

        /// <summary>
        /// 匹配邮箱是否合法
        /// </summary>
        /// <param name="source">待匹配字符串</param>
        /// <returns>匹配结果true是邮箱反之不是邮箱</returns>
        public bool IsValidEmail(string source)
        {
            Regex rg = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$", RegexOptions.IgnoreCase);
            return rg.IsMatch(source);
        }

        /// <summary>
        /// 匹配URL是否合法
        /// </summary>
        /// <param name="source">待匹配字符串</param>
        /// <returns>匹配结果true是URL反之不是URL</returns>
        public bool IsValidUrl(string source)
        {
            Regex rg = new Regex(
                @"^(https?|s?ftp):\/\/(((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:)*@)?(((\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5])\.(\d|[1-9]\d|1\d\d|2[0-4]\d|25[0-5]))|((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?)(:\d*)?)(\/((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)+(\/(([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)*)*)?)?(\?((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|[\uE000-\uF8FF]|\/|\?)*)?(#((([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(%[\da-f]{2})|[!\$&'\(\)\*\+,;=]|:|@)|\/|\?)*)?$",
                RegexOptions.IgnoreCase);
            return rg.IsMatch(source);
        }

        /// <summary>
        /// 匹配日期是否合法
        /// </summary>
        /// <param name="source">待匹配字符串</param>
        /// <returns>匹配结果true是日期反之不是日期</returns>
        public bool IsValidDate(string source)
        {
            Regex rg = new Regex(
                @"^(\d{4}[\/\-](0?[1-9]|1[0-2])[\/\-]((0?[1-9])|((1|2)[0-9])|30|31))|((0?[1-9]|1[0-2])[\/\-]((0?[1-9])|((1|2)[0-9])|30|31)[\/\-]\d{4})$");
            return rg.IsMatch(source);
        }

        /// <summary>
        /// 从字符串中获取第一个日期
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>源字符串中的第一个日期</returns>
        public string GetFirstDate(string source)
        {
            return Regex.Match(source,
                    @"(\d{4}[\/\-](0?[1-9]|1[0-2])[\/\-]((0?[1-9])|((1|2)[0-9])|30|31))|((0?[1-9]|1[0-2])[\/\-]((0?[1-9])|((1|2)[0-9])|30|31)[\/\-]\d{4})")
                .Groups[0].Value;
        }

        /// <summary>
        /// 从字符串中获取所有的日期
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>源字符串中的所有日期</returns>
        public List<string> GetAllDate(string source)
        {
            var all = Regex.Matches(source,
                @"(\d{4}[\/\-](0?[1-9]|1[0-2])[\/\-]((0?[1-9])|((1|2)[0-9])|30|31))|((0?[1-9]|1[0-2])[\/\-]((0?[1-9])|((1|2)[0-9])|30|31)[\/\-]\d{4})");
            List<string> list = new List<string>();
            foreach (Match item in all)
            {
                list.Add(item.Value);
            }

            return list;
        }

        /// <summary>
        /// 检测密码复杂度是否达标：密码中必须包含字母、数字、特称字符，至少6个字符，最多16个字符。
        /// </summary>
        /// <param name="source">待匹配字符串</param>
        /// <returns>密码复杂度是否达标true是达标反之不达标</returns>
        public bool IsValidPassword(string source)
        {
            Regex rg = new Regex(@"^(?=.*\d)(?=.*[a-zA-Z])(?=.*[^a-zA-Z0-9]).{6,16}$");
            return rg.IsMatch(source);
        }

        /// <summary>
        /// 检测密码复杂度是否达标：密码中必须包含字母、数字、特称字符，至少8个字符，最多16个字符。
        /// </summary>
        /// <param name="source">待匹配字符串</param>
        /// <returns>密码复杂度是否达标true是达标反之不达标</returns>
        public bool IsValidPassword8(string source)
        {
            Regex rg = new Regex(@"^(?=.*\d)(?=.*[a-zA-Z])(?=.*[^a-zA-Z0-9]).{8,16}$");
            return rg.IsMatch(source);
        }

        /// <summary>
        /// 匹配邮编是否合法
        /// </summary>
        /// <param name="source">待匹配字符串</param>
        /// <returns>邮编合法返回true,反之不合法</returns>
        public bool IsValidPostCode(string source)
        {
            Regex rg = new Regex(@"^\d{6}$");
            return rg.IsMatch(source);
        }

        /// <summary>
        /// 匹配电话号码是否合法
        /// </summary>
        /// <param name="source">待匹配字符串</param>
        /// <returns>电话号码合法返回true,反之不合法</returns>
        public bool IsValidTelephone(string source)
        {
            Regex rg = new Regex(@"^(\(\d{3,4}-)|\d{3.4}-\)?\d{7,8}$");
            return rg.IsMatch(source);
        }

        /// <summary>
        /// 从字符串中获取电话号码
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>源字符串中电话号码</returns>
        public string GetTelephone(string source)
        {
            return Regex.Match(source, @"(\(\d{3,4}-)|\d{3.4}-\)?\d{7,8}").Groups[0].Value;
        }

        /// <summary>
        /// 匹配手机号码是否合法
        /// </summary>
        /// <param name="source">待匹配字符串</param>
        /// <returns>手机号码合法返回true,反之不合法</returns>
        public bool IsValidMobile(string source)
        {
            Regex rg = new Regex(@"^[1]+[3,5,7]+\d{9}$");
            return rg.IsMatch(source);
        }

        /// <summary>
        /// 从字符串中获取手机号码
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>源字符串中手机号码</returns>
        public string GetMobile(string source)
        {
            return Regex.Match(source, @"[1]+[3,5,7]+\d{9}").Groups[0].Value;
        }

        /// <summary>
        /// 匹配身份证号码是否合法
        /// </summary>
        /// <param name="source">待匹配字符串</param>
        /// <returns>身份证号码合法返回true,反之不合法</returns>
        public bool IsValidIdCard(string source)
        {
            Regex rg = new Regex(@"^(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))$");
            return rg.IsMatch(source);
        }

        /// <summary>
        /// 从字符串中获取身份证号码
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>源字符串中身份证号码</returns>
        public string GetIdCard(string source)
        {
            return Regex.Match(source, @"(^\d{15}$|^\d{18}$|^\d{17}(\d|X|x))").Groups[0].Value;
        }

        #endregion

        #region 常规字符串操作

        /// <summary>
        /// 检查字符串是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsEmpty(string str)
        {
            if (str == null || str == "")
                return true;
            else
                return false;
        }

        /// <summary>
        /// 检查字符串中是否包含非法字符
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool CheckValidity(string s)
        {
            string str = s;
            if (str.IndexOf("'") > 0 || str.IndexOf("&") > 0 || str.IndexOf("%") > 0 || str.IndexOf("+") > 0 ||
                str.IndexOf("\"") > 0 || str.IndexOf("=") > 0 || str.IndexOf("!") > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 把价格精确至小数点两位
        /// </summary>
        /// <param name="dPrice">价格</param>
        /// <returns></returns>
        public static string TransformPrice(double dPrice)
        {
            double d = dPrice;
            NumberFormatInfo myNfi = new NumberFormatInfo
            {
                NumberNegativePattern = 2
            };
            string s = d.ToString("N", myNfi);
            return s;
        }

        /// <summary>
        /// 转换为字符串格式
        /// </summary>
        /// <param name="f"></param>
        /// <param name="iNum"></param>
        /// <returns></returns>
        public static string TransToStr(float f, int iNum)
        {
            float fl = f;
            NumberFormatInfo myNfi = new NumberFormatInfo
            {
                NumberNegativePattern = iNum
            };
            string s = f.ToString("N", myNfi);
            return s;
        }

        /// <summary> 
        /// 检测含有中文字符串的实际长度 
        /// </summary> 
        /// <param name="str">字符串</param> 
        public static int GetLength(string str)
        {
            ASCIIEncoding n = new ASCIIEncoding();
            byte[] b = n.GetBytes(str);
            int l = 0; // l 为字符串之实际长度 
            for (int i = 0; i <= b.Length - 1; i++)
            {
                if (b[i] == 63) //判断是否为汉字或全脚符号 
                {
                    l++;
                }

                l++;
            }

            return l;

        }

        /// <summary>
        /// 截取长度,num是英文字母的总数，一个中文算两个英文
        /// </summary>
        /// <param name="str"></param>
        /// <param name="iNum"></param>
        /// <param name="bAddDot"></param>
        /// <returns></returns>
        public static string GetLetter(string str, int iNum, bool bAddDot)
        {
            if (str == null || iNum <= 0) return "";

            if (str.Length < iNum && str.Length * 2 < iNum)
            {
                return str;
            }

            string sContent = str;
            int iTmp = iNum;

            char[] arrC;
            if (sContent.Length >= iTmp) //防止因为中文的原因使ToCharArray溢出
            {
                arrC = str.ToCharArray(0, iTmp);
            }
            else
            {
                arrC = str.ToCharArray(0, sContent.Length);
            }

            int i = 0;
            int iLength = 0;
            foreach (char ch in arrC)
            {
                iLength++;

                int k = (int)ch;
                if (k > 127 || k < 0)
                {
                    i += 2;
                }
                else
                {
                    i++;
                }

                if (i > iTmp)
                {
                    iLength--;
                    break;
                }
                else if (i == iTmp)
                {
                    break;
                }
            }

            if (iLength < str.Length && bAddDot)
                sContent = sContent.Substring(0, iLength - 3) + "...";
            else
                sContent = sContent.Substring(0, iLength);

            return sContent;
        }

        /// <summary>
        /// 获取日期字符串
        /// </summary>
        /// <param name="dt">时间对象</param>
        /// <returns></returns>
        public static string GetDateString(DateTime dt)
        {
            return dt.Year + dt.Month.ToString().PadLeft(2, '0') + dt.Day.ToString().PadLeft(2, '0');
        }

        /// <summary>
        /// 根据指定字符，截取相应字符串
        /// </summary>
        /// <param name="sOrg"></param>
        /// <param name="sLast"></param>
        /// <returns></returns>
        public static string GetStrByLast(string sOrg, string sLast)
        {
            int iLast = sOrg.LastIndexOf(sLast);
            if (iLast > 0)
                return sOrg.Substring(iLast + 1);
            else
                return sOrg;
        }

        /// <summary>
        /// 获取以sLast结束的字符的前面部分内容。
        /// </summary>
        /// <param name="sOrg">字符内容</param>
        /// <param name="sLast">结束字符</param>
        /// <returns></returns>
        public static string GetPreStrByLast(string sOrg, string sLast)
        {
            int iLast = sOrg.LastIndexOf(sLast);
            if (iLast > 0)
                return sOrg.Substring(0, iLast);
            else
                return sOrg;
        }

        /// <summary>
        /// 移除以sEnd结束的字符
        /// </summary>
        /// <param name="sOrg"></param>
        /// <param name="sEnd"></param>
        /// <returns></returns>
        public static string RemoveEndWith(string sOrg, string sEnd)
        {
            if (sOrg.EndsWith(sEnd))
                sOrg = sOrg.Remove(sOrg.IndexOf(sEnd), sEnd.Length);
            return sOrg;
        }

        #endregion 常规字符串操作

        #region HTML相关操作

        /// <summary>
        /// 移除HTML标记
        /// </summary>
        /// <param name="sHtml">html内容</param>
        /// <returns></returns>
        public static string ClearTag(string sHtml)
        {
            if (sHtml == "")
                return "";
            string sTemp = sHtml;
            Regex re = new Regex(@"(<[^>\s]*\b(\w)+\b[^>]*>)|(<>)|(&nbsp;)|(&gt;)|(&lt;)|(&amp;)|\r|\n|\t",
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            return re.Replace(sHtml, "");
        }

        /// <summary>
        /// 根据正则表达式，移除指定的标识
        /// </summary>
        /// <param name="sHtml">html内容</param>
        /// <param name="sRegex">正则表达式</param>
        /// <returns></returns>
        public static string ClearTag(string sHtml, string sRegex)
        {
            string sTemp = sHtml;
            Regex re = new Regex(sRegex,
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            return re.Replace(sHtml, "");
        }

        /// <summary>
        /// 转换为JS脚本
        /// </summary>
        /// <param name="sHtml"></param>
        /// <returns></returns>
        public static string ConvertToJs(string sHtml)
        {
            StringBuilder sText = new StringBuilder();
            Regex re;
            re = new Regex(@"\r\n", RegexOptions.IgnoreCase);
            string[] strArray = re.Split(sHtml);
            foreach (string strLine in strArray)
            {
                sText.Append("document.writeln(\"" + strLine.Replace("\"", "\\\"") + "\");\r\n");
            }

            return sText.ToString();
        }

        /// <summary>
        /// 替换HTML内容的nbsp空格
        /// </summary>
        /// <param name="str">HTML内容</param>
        /// <returns></returns>
        public static string ReplaceNbsp(string str)
        {
            string sContent = str;
            if (sContent.Length > 0)
            {
                sContent = sContent.Replace(" ", "");
                sContent = sContent.Replace("&nbsp;", "");
                sContent = "&nbsp;&nbsp;&nbsp;&nbsp;" + sContent;
            }

            return sContent;
        }

        /// <summary>
        /// 字符串转换为HTML
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StringToHtml(string str)
        {
            string sContent = str;
            if (sContent.Length > 0)
            {
                char csCr = (char)13;
                sContent = sContent.Replace(csCr.ToString(), "<br>");
                sContent = sContent.Replace(" ", "&nbsp;");
                sContent = sContent.Replace("　", "&nbsp;&nbsp;");
            }

            return sContent;
        }

        /// <summary>
        /// 截取长度并转换为HTML
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string AcquireAssignString(string str, int num)
        {
            string sContent = str;
            sContent = GetLetter(sContent, num, false);
            sContent = StringToHtml(sContent);
            return sContent;
        }

        /// <summary>
        /// 此方法与AcquireAssignString的功能已经一样，为了不报错，故保留此方法
        /// </summary>
        /// <param name="str"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public static string TranslateToHtmlString(string str, int num)
        {
            string sContent = str;
            sContent = GetLetter(sContent, num, false);
            sContent = StringToHtml(sContent);
            return sContent;
        }

        /// <summary>
        /// 在字符前面添加空白
        /// </summary>
        /// <param name="str">字符串</param>
        /// <returns></returns>
        public static string AddBlankAtForefront(string str)
        {
            string sContent = str;
            return sContent;
        }

        /// <summary>
        /// 删除所有的html标记 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DelHtmlString(string str)
        {
            string[] regexs =
            {
                @"<script[^>]*?>.*?</script>",
                @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
                @"([\r\n])[\s]+",
                @"&(quot|#34);",
                @"&(amp|#38);",
                @"&(lt|#60);",
                @"&(gt|#62);",
                @"&(nbsp|#160);",
                @"&(iexcl|#161);",
                @"&(cent|#162);",
                @"&(pound|#163);",
                @"&(copy|#169);",
                @"&#(\d+);",
                @"-->",
                @"<!--.*\n"
            };

            string[] replaces =
            {
                "",
                "",
                "",
                "\"",
                "&",
                "<",
                ">",
                " ",
                "\xa1", //chr(161),
                "\xa2", //chr(162),
                "\xa3", //chr(163),
                "\xa9", //chr(169),
                "",
                "\r\n",
                ""
            };

            string s = str;
            for (int i = 0; i < regexs.Length; i++)
            {
                s = new Regex(regexs[i], RegexOptions.Multiline | RegexOptions.IgnoreCase).Replace(s, replaces[i]);
            }

            s.Replace("<", "");
            s.Replace(">", "");
            s.Replace("\r\n", "");
            return s;
        }

        /// <summary>
        /// 删除字符串中的特定标记 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tag"></param>
        /// <param name="isContent">是否清除内容 </param>
        /// <returns></returns>
        public static string DelTag(string str, string tag, bool isContent)
        {
            if (tag == null || tag == " ")
            {
                return str;
            }

            if (isContent) //要求清除内容 
            {
                return Regex.Replace(str, $"<({tag})[^>]*>([\\s\\S]*?)<\\/\\1>", "", RegexOptions.IgnoreCase);
            }

            return Regex.Replace(str, string.Format(@"(<{0}[^>]*(>)?)|(</{0}[^>] *>)|", tag), "",
                RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 删除字符串中的一组标记 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="tagA"></param>
        /// <param name="isContent">是否清除内容 </param>
        /// <returns></returns>
        public static string DelTagArray(string str, string tagA, bool isContent)
        {

            string[] tagAa = tagA.Split(',');

            foreach (string sr1 in tagAa) //遍历所有标记，删除 
            {
                str = DelTag(str, sr1, isContent);
            }

            return str;

        }

        #endregion HTML相关操作

        #region 其他字符串操作

        /// <summary>
        /// 格式化为版本号字符串
        /// </summary>
        /// <param name="sVersion"></param>
        /// <returns></returns>
        public static string SetVersionFormat(string sVersion)
        {
            if (sVersion == null || sVersion == "") return "";
            int n = 0, k = 0;

            string stmVersion = "";
            while (n < 4 && k > -1)
            {
                k = sVersion.IndexOf(".", k + 1);
                n++;
            }

            if (k > 0)
            {
                stmVersion = sVersion.Substring(0, k);
            }
            else
            {
                stmVersion = sVersion;
            }

            return stmVersion;
        }

        /// <summary>
        /// 格式化字符串为 SQL 语句字段
        /// </summary>
        /// <param name="fldList"></param>
        /// <returns></returns>
        public static string GetSqlFildList(string fldList)
        {
            if (fldList == null)
                return "*";
            if (fldList.Trim() == "")
                return "*";
            if (fldList.Trim() == "*")
                return "*";
            //先去掉空格，[]符号
            string strTemp = fldList;
            strTemp = strTemp.Replace(" ", "");
            strTemp = strTemp.Replace("[", "").Replace("]", "");
            //为防止使用保留字，给所有字段加上[]
            strTemp = "[" + strTemp + "]";
            strTemp = strTemp.Replace('，', ',');
            strTemp = strTemp.Replace(",", "],[");
            return strTemp;
        }

        /// <summary>
        /// 获取延迟时间
        /// </summary>
        /// <param name="dtStar">开始时间</param>
        /// <param name="dtEnd">结束时间</param>
        /// <returns></returns>
        public static string GetTimeDelay(DateTime dtStar, DateTime dtEnd)
        {
            long lTicks = (dtEnd.Ticks - dtStar.Ticks) / 10000000;
            string sTemp = (lTicks / 3600).ToString().PadLeft(2, '0') + ":";
            sTemp += ((lTicks % 3600) / 60).ToString().PadLeft(2, '0') + ":";
            sTemp += ((lTicks % 3600) % 60).ToString().PadLeft(2, '0');
            return sTemp;
        }

        /// <summary>
        /// 在前面补0
        /// </summary>
        /// <returns></returns>
        public static string AddZero(int sheep, int length)
        {
            return AddZero(sheep.ToString(), length);
        }

        /// <summary>
        /// 在前面补0
        /// </summary>
        /// <returns></returns>
        public static string AddZero(string sheep, int length)
        {
            StringBuilder goat = new StringBuilder(sheep);
            for (int i = goat.Length; i < length; i++)
            {
                goat.Insert(0, "0");
            }

            return goat.ToString();
        }

        /// <summary>
        /// 简介：获得唯一的字符串
        /// </summary>
        /// <returns></returns>
        public static string GetUniqueString()
        {
            Random rand = new Random();
            return ((int)(rand.NextDouble() * 10000)) + DateTime.Now.Ticks.ToString();
        }

        /// <summary>
        /// 获得干净,无非法字符的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetCleanJsString(string str)
        {
            str = str.Replace("\"", "“");
            str = str.Replace("'", "”");
            str = str.Replace("\\", "\\\\");
            Regex re = new Regex(@"\r|\n|\t",
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            str = re.Replace(str, " ");

            return str;
        }

        /// <summary>
        /// 获得干净,无非法字符的字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetCleanJsString2(string str)
        {
            str = str.Replace("\"", "\\\"");
            //str = str.Replace("'", "\\\'");
            //str = str.Replace("\\", "\\\\");
            Regex re = new Regex(@"\r|\n|\t",
                RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.IgnorePatternWhitespace);
            str = re.Replace(str, " ");

            return str;
        }

        #endregion 其他字符串操作

        /// <summary>
        /// 取得所有链接URL
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetAllUrl(string html)
        {
            StringBuilder sb = new StringBuilder();
            Match m = Regex.Match(html.ToLower(), "<a href=(.*?)>.*?</a>");

            while (m.Success)
            {
                sb.AppendLine(m.Result("$1"));
                m.NextMatch();
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取所有连接文本
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string GetAllLinkText(string html)
        {
            StringBuilder sb = new StringBuilder();
            Match m = Regex.Match(html.ToLower(), "<a href=.*?>(1,100})</a>");

            while (m.Success)
            {
                sb.AppendLine(m.Result("$1"));
                m.NextMatch();
            }

            return sb.ToString();
        }
    }
}