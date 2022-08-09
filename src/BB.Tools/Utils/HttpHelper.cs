using System.Collections.Specialized;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace BB.Tools.Utils;

/// <summary>
/// 获取网页数据辅助类库。
/// </summary>
public class HttpHelper
{
    #region 私有变量
    private CookieContainer _cc;
    private string _contentType = "application/x-www-form-urlencoded";
    private string _accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*";
    private string _userAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)";
        
    private Encoding _encoding = Encoding.GetEncoding("utf-8");
    private int _delay = 1000;
    private int _maxTry = 3;
    private int _currentTry = 0;
    #endregion

    #region 属性

    /// <summary>
    /// 内容类型，默认为"application/x-www-form-urlencoded"
    /// </summary>
    public string ContentType
    {
        get => _contentType;
        set => _contentType = value;
    }

    /// <summary>
    /// Accept值，默认支持各种类型
    /// </summary>
    public string Accept
    {
        get => _accept;
        set => _accept = value;
    }

    /// <summary>
    /// UserAgent，默认支持Mozilla/MSIE等
    /// </summary>
    public string UserAgent
    {
        get => _userAgent;
        set => _userAgent = value;
    }  

    /// <summary>
    /// Cookie容器
    /// </summary>
    public CookieContainer CookieContainer => _cc;

    /// <summary>
    /// 获取网页源码时使用的编码
    /// </summary>
    /// <value></value>
    public Encoding Encoding
    {
        get => _encoding;
        set => _encoding = value;
    }

    /// <summary>
    /// 网络延时
    /// </summary>
    public int NetworkDelay
    {
        get
        {
            Random r = new Random();
            return (r.Next(_delay / 1000, _delay / 1000 * 2))*1000;
        }
        set => _delay = value;
    }

    /// <summary>
    /// 最大尝试次数
    /// </summary>
    public int MaxTry
    {
        get => _maxTry;
        set => _maxTry = value;
    }

    /// <summary>
    /// X509Certificate2证书集合
    /// </summary>
    public X509CertificateCollection ClientCertificates { get; set; }

    /// <summary>
    /// 特殊的Header内容集合
    /// </summary>
    public NameValueCollection Header { get; set; }

    /// <summary>
    /// HTTP代理设置
    /// </summary>
    public WebProxy Proxy { get; set; }

    #endregion

    #region 构造函数

    /// <summary>
    /// 构造函数
    /// </summary>
    public HttpHelper()
    {
        _cc = new CookieContainer();
        Header = new NameValueCollection();

        //基础连接已经关闭: 未能为SSL/TLS 安全通道建立信任关系。的处理方法
        ServicePointManager.ServerCertificateValidationCallback = (s, cer, ch, ssl) => true;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="cc">指定CookieContainer的值</param>
    public HttpHelper(CookieContainer cc) : this()
    {
        _cc = cc;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="contentType">内容类型</param>
    /// <param name="accept">Accept类型</param>
    /// <param name="userAgent">UserAgent内容</param>
    public HttpHelper(string contentType, string accept, string userAgent) : this()
    {
        _contentType = contentType;
        _accept = accept;
        _userAgent = userAgent;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="cc">指定CookieContainer的值</param>
    /// <param name="contentType">内容类型</param>
    /// <param name="accept">Accept类型</param>
    /// <param name="userAgent">UserAgent内容</param>
    public HttpHelper(CookieContainer cc, string contentType, string accept, string userAgent) : this()
    {
        _cc = cc;
        _contentType = contentType;
        _accept = accept;
        _userAgent = userAgent;
    }

    #endregion

    #region 公共方法                     

    /// <summary>
    /// 获取指定页面的HTML代码
    /// </summary>
    /// <param name="url">指定页面的路径</param>
    /// <param name="cookieContainer">Cookie集合对象</param>
    /// <param name="postData">回发的数据</param>
    /// <param name="isPost">是否以post方式发送请求</param>
    /// <param name="referer">页面引用</param>
    /// <returns></returns>
    public string GetHtml(string url, CookieContainer cookieContainer, string postData = null, bool isPost= false, string referer = null)
    {
        _currentTry++;
        try
        {
            string newUrl = url;

            //如果不是POST方式，那么使用POSTData构建连接字符串
            if(!isPost && !string.IsNullOrEmpty(postData))
            {
                string concatChar = newUrl.Contains("?") ? "&" : "?";
                newUrl += $"{concatChar}{postData}";
            }

            byte[] byteRequest = null;
            if (isPost)
            {
                byteRequest = Encoding.GetBytes(postData);
            }

            HttpWebRequest httpWebRequest;
            httpWebRequest = (HttpWebRequest)WebRequest.Create(newUrl);
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.ContentType = _contentType;
            httpWebRequest.Referer = string.IsNullOrEmpty(referer) ? newUrl : referer;
            httpWebRequest.Accept = _accept;
            httpWebRequest.UserAgent = _userAgent;
            httpWebRequest.Method = isPost ? "POST" : "GET";
            httpWebRequest.ContentLength = isPost ? byteRequest.Length : 0;
            httpWebRequest.AllowAutoRedirect = true;
            httpWebRequest.Proxy = Proxy; //设置HTTP代理

            //添加特殊的Header参数
            if(Header != null)
            {
                httpWebRequest.Headers.Add(Header);
            }

            //如果证书集合不为空，则添加到集合
            if(ClientCertificates != null)
            {
                foreach(X509Certificate cert in ClientCertificates)
                {
                    httpWebRequest.ClientCertificates.Add(cert);
                }
            }

            //如果是POST方式，写入请求流
            if (isPost)
            {
                Stream stream = httpWebRequest.GetRequestStream();
                stream.Write(byteRequest, 0, byteRequest.Length);
                stream.Close();
            }

            HttpWebResponse httpWebResponse;
            try
            {
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //redirectURL = httpWebResponse.Headers["Location"];// Get redirected uri
            }
            catch (WebException ex)
            {
                httpWebResponse = (HttpWebResponse)ex.Response;
            }
            Stream responseStream = httpWebResponse.GetResponseStream();

            StreamReader streamReader = new StreamReader(responseStream, _encoding);
            string html = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();

            _currentTry = 0;
            return html;
        }
        catch (Exception e)
        {
            if (_currentTry <= _maxTry)
            {
                GetHtml(url, cookieContainer, postData, isPost);
            }

            _currentTry = 0;
            return string.Empty;
        }
    }


    /// <summary>
    /// 获取指定页面的HTML代码
    /// </summary>
    /// <param name="url">指定页面的路径</param>
    /// <param name="postData">回发的数据</param>
    /// <param name="isPost">是否以post方式发送请求</param>
    /// <returns></returns>
    public string GetHtml(string url, string postData = null, bool isPost = false, string referer = null)
    {
        return GetHtml(url, _cc, postData, isPost, referer);
    }
                        
    /// <summary>
    /// 获取指定页面的Stream
    /// </summary>
    /// <param name="url">指定页面的路径</param>
    /// <param name="fileName">文件名称</param>
    /// <param name="cookieContainer">Cookie集合对象</param>
    /// <returns></returns>
    public Stream GetStream(string url, ref string fileName, CookieContainer cookieContainer)
    {
        return GetStream(url, ref fileName, cookieContainer, url);
    }

    /// <summary>
    /// 获取指定页面的Stream
    /// </summary>
    /// <param name="url">指定页面的路径</param>
    /// <param name="fileName">文件名称</param>
    /// <param name="cookieContainer">Cookie对象</param>
    /// <param name="reference">页面引用</param>
    public Stream GetStream(string url, ref string fileName, CookieContainer cookieContainer, string reference)
    {
        _currentTry++;
        try
        {
            HttpWebRequest httpWebRequest;
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.ContentType = _contentType;
            httpWebRequest.Referer = reference;
            httpWebRequest.Accept = _accept;
            httpWebRequest.UserAgent = _userAgent;
            httpWebRequest.Method = "GET";
            httpWebRequest.Proxy = Proxy; //设置HTTP代理

            //添加特殊的Header参数
            if (Header != null)
            {
                httpWebRequest.Headers.Add(Header);
            }

            //如果证书集合不为空，则添加到集合
            if (ClientCertificates != null)
            {
                foreach (X509Certificate cert in ClientCertificates)
                {
                    httpWebRequest.ClientCertificates.Add(cert);
                }
            }

            HttpWebResponse httpWebResponse;
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();

            fileName = httpWebResponse.Headers["Content-Disposition"] != null ?
                httpWebResponse.Headers["Content-Disposition"].Replace("attachment; filename=", "").Replace("\"", "") :
                httpWebResponse.Headers["Location"] != null ? Path.GetFileName(httpWebResponse.Headers["Location"]) :
                    Path.GetFileName(url).Contains("=") ? url.Substring(url.LastIndexOf("=") + 1) :
                        Path.GetFileName(url).Contains("?") ? url.Substring(url.LastIndexOf("?") + 1) :
                            Path.GetFileName(httpWebResponse.ResponseUri.ToString());

            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = Path.GetFileName(fileName);
            }
            else
            {
                fileName = "UnKnowFileName";//默认文件名称
            }

            _currentTry = 0;
            return responseStream;
        }
        catch (Exception e)
        {
            if (_currentTry <= _maxTry)
            {
                CookieCollection cookie = new CookieCollection();
                GetStream(url, ref fileName, cookieContainer, reference);
            }

            _currentTry = 0;
            return null;
        }
    }

    /// <summary>
    /// 获取指定页面的Stream
    /// </summary>
    /// <param name="url">指定页面的路径</param>
    /// <param name="fileName">文件名称</param>
    /// <param name="cookieContainer">Cookie对象</param>
    /// <param name="postData">POST数据</param>
    /// <param name="isPost">是否使用POST方式</param>
    /// <param name="reference">页面引用</param>
    public Stream GetStream(string url, ref string fileName, CookieContainer cookieContainer, string postData, bool isPost, string reference)
    {
        _currentTry++;
        try
        {
            byte[] byteRequest = null;
            if (isPost)
            {
                byteRequest = Encoding.GetBytes(postData);
            }

            HttpWebRequest httpWebRequest;
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.CookieContainer = cookieContainer;
            httpWebRequest.ContentType = _contentType;
            httpWebRequest.Referer = reference;
            httpWebRequest.Accept = _accept;
            httpWebRequest.UserAgent = _userAgent;
            httpWebRequest.Method = isPost ? "POST" : "GET";
            httpWebRequest.Proxy = Proxy; //设置HTTP代理

            //添加特殊的Header参数
            if (Header != null)
            {
                httpWebRequest.Headers.Add(Header);
            }

            //如果证书集合不为空，则添加到集合
            if (ClientCertificates != null)
            {
                foreach (X509Certificate cert in ClientCertificates)
                {
                    httpWebRequest.ClientCertificates.Add(cert);
                }
            }

            Stream stream = httpWebRequest.GetRequestStream();
            stream.Write(byteRequest, 0, byteRequest.Length);
            stream.Close();

            HttpWebResponse httpWebResponse;
            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Stream responseStream = httpWebResponse.GetResponseStream();

            fileName = httpWebResponse.Headers["Content-Disposition"] != null ?
                httpWebResponse.Headers["Content-Disposition"].Replace("attachment; filename=", "").Replace("\"", "") :
                httpWebResponse.Headers["Location"] != null ? Path.GetFileName(httpWebResponse.Headers["Location"]) :
                    Path.GetFileName(url).Contains("=") ? url.Substring(url.LastIndexOf("=") + 1) :
                        Path.GetFileName(url).Contains("?") ? url.Substring(url.LastIndexOf("?") + 1) :
                            Path.GetFileName(httpWebResponse.ResponseUri.ToString());

            if (!string.IsNullOrEmpty(fileName))
            {
                fileName = Path.GetFileName(fileName);
            }
            else
            {
                fileName = "UnKnowFileName";//默认文件名称
            }

            _currentTry = 0;
            return responseStream;
        }
        catch (Exception e)
        {
            if (_currentTry <= _maxTry)
            {
                CookieCollection cookie = new CookieCollection();
                GetStream(url, ref fileName, cookieContainer, postData, isPost, reference);
            }

            _currentTry = 0;
            return null;
        }
    }

    /// <summary>
    /// 提交文件流到服务地址
    /// </summary>
    /// <param name="url">指定页面的路径</param>
    /// <param name="files">提交的文件列表</param>
    /// <param name="nvc">其他内容（名称-值键值）</param>
    /// <param name="cookieContainer">Cookie对象</param>
    /// <param name="reference">页面引用</param>
    /// <returns></returns>
    public string PostStream(string url, string[] files, NameValueCollection nvc = null, CookieContainer cookieContainer = null, string reference = null)
    {
        string boundary = "----------------------------" +
                          DateTime.Now.Ticks.ToString("x");

        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
        httpWebRequest.ContentType = "multipart/form-data; boundary=" + boundary;
        httpWebRequest.Method = "POST";
        httpWebRequest.KeepAlive = true;
        httpWebRequest.Credentials = CredentialCache.DefaultCredentials;
        httpWebRequest.CookieContainer = cookieContainer;
        httpWebRequest.Referer = reference;
        httpWebRequest.Proxy = Proxy; //设置HTTP代理
            
        //添加特殊的Header参数
        if (Header != null)
        {
            httpWebRequest.Headers.Add(Header);
        }

        //如果证书集合不为空，则添加到集合
        if (ClientCertificates != null)
        {
            foreach (X509Certificate cert in ClientCertificates)
            {
                httpWebRequest.ClientCertificates.Add(cert);
            }
        }

        Stream memStream = new MemoryStream();
        byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" +  boundary + "\r\n");            
        string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";

        if (nvc != null)
        {
            foreach (string key in nvc.Keys)
            {
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = Encoding.GetBytes(formitem);
                memStream.Write(formitembytes, 0, formitembytes.Length);
            }
        }
        memStream.Write(boundarybytes, 0, boundarybytes.Length);

        string fileHeaderName = "media";//form-data中媒体文件标识，此处默认为media
        string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
        if (files != null)
        {
            for (int i = 0; i < files.Length; i++)
            {
                //string header = string.Format(headerTemplate, "file" + i, files[i]);
                var fileName = new FileInfo(files[i]).Name;
                string header = string.Format(headerTemplate, fileHeaderName, fileName);
                byte[] headerbytes = Encoding.GetBytes(header);
                memStream.Write(headerbytes, 0, headerbytes.Length);

                FileStream fileStream = new FileStream(files[i], FileMode.Open,
                    FileAccess.Read);
                byte[] buffer = new byte[1024];
                int bytesRead = 0;

                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }

                memStream.Write(boundarybytes, 0, boundarybytes.Length);
                fileStream.Close();
            }
        }

        httpWebRequest.ContentLength = memStream.Length;
        using (Stream requestStream = httpWebRequest.GetRequestStream())
        {
            memStream.Position = 0;
            byte[] tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();

            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();
        }

        string result = null;
        using (WebResponse webResponse2 = httpWebRequest.GetResponse())
        {
            using (Stream stream2 = webResponse2.GetResponseStream())
            {
                using (StreamReader reader2 = new StreamReader(stream2))
                {
                    result = reader2.ReadToEnd();
                }
            }
        }
        httpWebRequest = null;

        return result;
    }

    /// <summary>
    /// 根据Cookie字符串获取Cookie的集合
    /// </summary>
    /// <param name="cookieString">Cookie字符串</param>
    /// <returns></returns>
    public CookieCollection GetCookieCollection(string cookieString)
    {
        CookieCollection cc = new CookieCollection();
        //string cookieString = "SID=ARRGy4M1QVBtTU-ymi8bL6X8mVkctYbSbyDgdH8inu48rh_7FFxHE6MKYwqBFAJqlplUxq7hnBK5eqoh3E54jqk=;Domain=.google.com;Path=/,LSID=AaMBTixN1MqutGovVSOejyb8mVkctYbSbyDgdH8inu48rh_7FFxHE6MKYwqBFAJqlhCe_QqxLg00W5OZejb_UeQ=;Domain=www.google.com;Path=/accounts";
        Regex re = new Regex("([^;,]+)=([^;,]+);Domain=([^;,]+);Path=([^;,]+)", RegexOptions.IgnoreCase);
        foreach (Match m in re.Matches(cookieString))
        {
            //name,   value,   path,   domain   
            Cookie c = new Cookie(m.Groups[1].Value, m.Groups[2].Value, m.Groups[3].Value, m.Groups[3].Value);
            cc.Add(c);
        }
        return cc;
    }

    /// <summary>
    /// 获取HTML页面内容指定隐藏域Key的Value内容
    /// </summary>
    /// <param name="html">待操作的HTML页面内容</param>
    /// <param name="key">隐藏域的名称</param>
    /// <returns></returns>
    public string GetHiddenKeyValue(string html, string key)
    {
        string result = "";
        string sRegex = $"<input\\s*type=\"hidden\".*?name=\"{key}\".*?\\s*value=[\"|'](?<value>.*?)[\"|'^/]";
        Regex re = new Regex(sRegex, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline);
        Match mc = re.Match(html);
        if (mc.Success)
        {
            result = mc.Groups[1].Value;
        }
        return result;
    }

    /// <summary>
    /// 获取网页的编码格式
    /// </summary>
    /// <param name="url">网页地址</param>
    /// <returns></returns>
    public string GetEncoding(string url)
    {
        HttpWebRequest httpWebRequest = null;
        HttpWebResponse response = null;
        StreamReader reader = null;
        try
        {
            httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Timeout = 5000;
            httpWebRequest.AllowAutoRedirect = false;
            httpWebRequest.Proxy = Proxy; //设置HTTP代理

            response = (HttpWebResponse)httpWebRequest.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK && response.ContentLength < 1024 * 1024)
            {
                if (response.ContentEncoding != null && response.ContentEncoding.Equals("gzip", StringComparison.InvariantCultureIgnoreCase))
                {
                    reader = new StreamReader(new GZipStream(response.GetResponseStream(), CompressionMode.Decompress));
                }
                else
                {
                    reader = new StreamReader(response.GetResponseStream(), Encoding.ASCII);
                }
                    
                string html = reader.ReadToEnd();
                Regex regCharset = new Regex(@"charset\b\s*=\s*(?<charset>[^""]*)");
                if (regCharset.IsMatch(html))
                {
                    return regCharset.Match(html).Groups["charset"].Value;
                }
                else if (response.CharacterSet != string.Empty)
                {
                    return response.CharacterSet;
                }
                else
                {
                    return Encoding.Default.BodyName;
                }
            }
        }
        catch
        {
        }
        finally
        {
            if (response != null)
            {
                response.Close();
                response = null;
            }
            if (reader != null)
                reader.Close();
            if (httpWebRequest != null)
                httpWebRequest = null;
        }
        return Encoding.Default.BodyName;
    }

    /// <summary>
    /// 检查页面是否存在(正常访问）
    /// </summary>
    public bool UrlExist(string url, WebProxy proxy = null)
    {
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Proxy = proxy;

            HttpWebResponse resp = (HttpWebResponse)request.GetResponse();
            if (resp.StatusCode == HttpStatusCode.OK)
            {
                resp.Close();
                return true;
            }
        }
        catch (WebException webex)
        {
            return false;
        }
        return false;
    }

    /// <summary>
    /// 判断URL是否有效
    /// </summary>
    /// <param name="url">待判断的URL，可以是网页以及图片链接等</param>
    /// <returns>200为正确，其余为大致网页错误代码</returns>
    public int GetUrlError(string url)
    {
        int num = 200;
        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(new Uri(url));
            request.Proxy = Proxy; //设置HTTP代理

            ServicePointManager.Expect100Continue = false;
            ((HttpWebResponse)request.GetResponse()).Close();
        }
        catch (WebException exception)
        {
            if (exception.Status != WebExceptionStatus.ProtocolError)
            {
                return num;
            }
            if (exception.Message.IndexOf("500 ") > 0)
            {
                return 500;
            }
            if (exception.Message.IndexOf("401 ") > 0)
            {
                return 401;
            }
            if (exception.Message.IndexOf("404") > 0)
            {
                num = 404;
            }
        }
        catch
        {
            num = 401;
        }
        return num;
    }

    /// <summary>
    /// 移除Html标记
    /// </summary>
    public string RemoveHtml(string content)
    {
        string regexstr = @"<[^>]*>";
        return Regex.Replace(content, regexstr, string.Empty, RegexOptions.IgnoreCase);
    }


    /// <summary>
    /// 返回 HTML 字符串的编码结果
    /// </summary>
    /// <param name="inputData">字符串</param>
    /// <returns>编码结果</returns>
    public static string HtmlEncode(string inputData)
    {
        return HttpUtility.HtmlEncode(inputData);
    }

    /// <summary>
    /// 返回 HTML 字符串的解码结果
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>解码结果</returns>
    public static string HtmlDecode(string str)
    {
        return HttpUtility.HtmlDecode(str);
    }

    /// <summary>
    /// 分析 url 字符串中的参数信息
    /// </summary>
    /// <param name="url">输入的 URL</param>
    /// <param name="baseUrl">输出 URL 的基础部分</param>
    /// <param name="nvc">输出分析后得到的 (参数名,参数值) 的集合</param>
    public static NameValueCollection ParseUrl(string url, out string baseUrl)
    {
        if (url == null)
            throw new ArgumentNullException("url");

        NameValueCollection result = new NameValueCollection();

        baseUrl = "";
        if (url == "")
            return result;

        int questionMarkIndex = url.IndexOf('?');
        if (questionMarkIndex == -1)
        {
            baseUrl = url;
            return result;
        }

        baseUrl = url.Substring(0, questionMarkIndex);
        if (questionMarkIndex == url.Length - 1)
            return result;

        string ps = url.Substring(questionMarkIndex + 1);
        // 开始分析参数对  
        Regex re = new Regex(@"(^|&)?(\w+)=([^&]+)(&|$)?", RegexOptions.Compiled);
        MatchCollection mc = re.Matches(ps);
        foreach (Match m in mc)
        {
            result.Add(m.Result("$2").ToLower(), m.Result("$3"));
        }

        return result;
    }

    #endregion
}