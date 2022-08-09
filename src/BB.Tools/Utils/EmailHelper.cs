using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using Furion.Logging.Extensions;

namespace BB.Tools.Utils;

/// <summary>
/// 发送邮件的辅助类，可以发送附件、嵌入图片、HTML等内容邮件。使用底层SMTP协议指令进行发送。
/// </summary>
public class EmailHelper
{
    #region public属性字段

    /// <summary>
    /// 设定语言代码，默认设定为GB2312，如不需要可设置为""
    /// </summary>
    public string Charset = "GB2312";

    /// <summary>
    /// 邮箱服务器
    /// </summary>
    public string MailServer
    {
        get => _mailserver;
        set => _mailserver = value;
    }

    /// <summary>
    /// 邮件服务器端口号,默认端口为25
    /// </summary>	
    public int MailServerPort
    {
        set => _mailserverport = value;
        get => _mailserverport;
    }

    /// <summary>
    /// SMTP认证时使用的用户名
    /// </summary>
    public string MailServerUsername
    {
        set
        {
            if (value.Trim() != "")
            {
                _username = value.Trim();
                _useSmtpAuth = true;
            }
            else
            {
                _username = "";
                _useSmtpAuth = false;
            }
        }
        get => _username;
    }

    /// <summary>
    /// SMTP认证时使用的密码
    /// </summary>
    public string MailServerPassword
    {
        set => _password = value;
        get => _password;
    }

    /// <summary>
    /// 发件人地址
    /// </summary>
    public string From
    {
        get => _from;
        set
        {
            _from = value;
            //如果未设置fromName，则fromName使用发件人邮箱
            if (string.IsNullOrEmpty(_fromName)) 
            { 
                _fromName = _from; 
            }
        }
    }

    /// <summary>
    /// 发件人姓名
    /// </summary>
    public string FromName
    {
        get => _fromName;
        set => _fromName = value;
    }

    /// <summary>
    /// 回复邮件地址
    /// </summary>
    public string ReplyTo = "";

    /// <summary>
    /// 邮件主题
    /// </summary>		
    public string Subject = "";

    /// <summary>
    /// 是否Html邮件
    /// </summary>		
    public bool IsHtml = false;

    /// <summary>
    /// 收件人是否发送收条
    /// </summary>
    public bool ReturnReceipt = false;

    /// <summary>
    /// 邮件正文
    /// </summary>		
    public string Body = "";

    /// <summary>
    /// 邮件发送优先级，可设置为"High","Normal","Low"或"1","3","5"
    /// </summary>
    public string Priority
    {
        set
        {
            switch (value.ToLower())
            {
                case "high":
                    _priority = "High";
                    break;

                case "1":
                    _priority = "High";
                    break;

                case "normal":
                    _priority = "Normal";
                    break;

                case "3":
                    _priority = "Normal";
                    break;

                case "low":
                    _priority = "Low";
                    break;

                case "5":
                    _priority = "Low";
                    break;

                default:
                    _priority = "Normal";
                    break;
            }
        }
    }

    /// <summary>
    /// 错误消息反馈
    /// </summary>		
    public string ErrorMessage => _errmsg;

    /// <summary>
    /// 收件人姓名
    /// </summary>	
    public string RecipientName = "";

    #endregion

    #region private属性字段

    /// <summary>
    /// 邮件服务器域名
    /// </summary>	
    private string _mailserver;

    /// <summary>
    /// 邮件服务器端口号
    /// </summary>	
    private int _mailserverport = 25;
    /// <summary>
    /// 发件人地址
    /// </summary>
    private string _from = "";
    /// <summary>
    /// 发件人姓名
    /// </summary>
    private string _fromName = "";

    /// <summary>
    /// 是否需要SMTP验证
    /// </summary>		
    private bool _useSmtpAuth = false;

    /// <summary>
    /// SMTP认证时使用的用户名
    /// </summary>
    private string _username = "";

    /// <summary>
    /// SMTP认证时使用的密码
    /// </summary>
    private string _password = "";

    /// <summary>
    /// 收件人最大数量：现在很多SMTP都限制收件人的最大数量，以防止广告邮件泛滥，最大数量一般都限制在10个以下。
    /// </summary>
    private int _recipientMaxNum = 10;

    /// <summary>
    /// 收件人列表
    /// </summary>
    private ArrayList _recipient = new ArrayList();

    /// <summary>
    ///抄送收件人列表
    /// </summary>
    private ArrayList _recipientCc = new ArrayList();

    /// <summary>
    /// 密送收件人列表
    /// </summary>
    private ArrayList _recipientBcc = new ArrayList();

    /// <summary>
    /// 邮件发送优先级，可设置为"High","Normal","Low"或"1","3","5"
    /// </summary>
    private string _priority = "Normal";

    /// <summary>
    /// 错误消息反馈
    /// </summary>
    private string _errmsg;

    /// <summary>
    /// 回车换行
    /// </summary>
    private string _enter = "\r\n";

    /// <summary>
    /// TcpClient对象，用于连接服务器
    /// </summary>	
    private TcpClient _tc;

    /// <summary>
    /// NetworkStream对象
    /// </summary>	
    private NetworkStream _ns;

    /// <summary>
    /// SMTP错误代码哈希表
    /// </summary>
    private Hashtable _errCodeHt = new Hashtable();

    /// <summary>
    /// SMTP正确代码哈希表
    /// </summary>
    private Hashtable _rightCodeHt = new Hashtable();

    #endregion

    /// <summary>
    /// SMTP回应代码哈希表
    /// </summary>
    private void SmtpCodeAdd()
    {
        _errCodeHt.Add("500", "邮箱地址错误");
        _errCodeHt.Add("501", "参数格式错误");
        _errCodeHt.Add("502", "命令不可实现");
        _errCodeHt.Add("503", "服务器需要SMTP验证");
        _errCodeHt.Add("504", "命令参数不可实现");
        _errCodeHt.Add("421", "服务未就绪，关闭传输信道");
        _errCodeHt.Add("450", "要求的邮件操作未完成，邮箱不可用（例如，邮箱忙）");
        _errCodeHt.Add("550", "要求的邮件操作未完成，邮箱不可用（例如，邮箱未找到，或不可访问）");
        _errCodeHt.Add("451", "放弃要求的操作；处理过程中出错");
        _errCodeHt.Add("551", "用户非本地，请尝试<forward-path>");
        _errCodeHt.Add("452", "系统存储不足，要求的操作未执行");
        _errCodeHt.Add("552", "过量的存储分配，要求的操作未执行");
        _errCodeHt.Add("553", "邮箱名不可用，要求的操作未执行（例如邮箱格式错误）");
        _errCodeHt.Add("432", "需要一个密码转换");
        _errCodeHt.Add("534", "认证机制过于简单");
        _errCodeHt.Add("538", "当前请求的认证机制需要加密");
        _errCodeHt.Add("454", "临时认证失败");
        _errCodeHt.Add("530", "需要认证");

        _rightCodeHt.Add("220", "服务就绪");
        _rightCodeHt.Add("250", "要求的邮件操作完成");
        _rightCodeHt.Add("251", "用户非本地，将转发向<forward-path>");
        _rightCodeHt.Add("354", "开始邮件输入，以<CRLF>.<CRLF>结束");
        _rightCodeHt.Add("221", "服务关闭传输信道");
        _rightCodeHt.Add("334", "服务器响应验证Base64字符串");
        _rightCodeHt.Add("235", "验证成功");
    }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public EmailHelper()
    {
        SmtpCodeAdd();
    }

    /// <summary>
    /// 待邮箱发送配置参数的构造函数
    /// </summary>
    /// <param name="mailServer">邮件服务器</param>
    /// <param name="username">用户名</param>
    /// <param name="password">用户密码</param>
    public EmailHelper(string mailServer, string username, string password) : 
        this(mailServer, username, password, 25)
    {

    }

    /// <summary>
    /// 待邮箱发送配置参数的构造函数
    /// </summary>
    /// <param name="mailServer">邮件服务器</param>
    /// <param name="username">用户名</param>
    /// <param name="password">用户密码</param>
    /// <param name="port">邮箱服务器端口</param>
    public EmailHelper(string mailServer, string username, string password, int port)
    {
        MailServer = mailServer;
        MailServerUsername = username;
        MailServerPassword = password;
        MailServerPort = port;

        SmtpCodeAdd();
    }

    /// <summary>
    /// 析构函数
    /// </summary>
    ~EmailHelper()
    {
        if (_ns != null)
        {
            _ns.Close();
        }
        if (_tc != null)
        {
            _tc.Close();
        }
    }

    #region 附件

    /// <summary>
    /// 添加一个附件,需使用绝对路径
    /// </summary>	
    public bool AddAttachment(string path)
    {
        if (System.IO.File.Exists(path))
        {
            _attachments.Add(path);
            return true;
        }
        else
        {
            _errmsg += "要附加的文件不存在" + _enter;
            return false;
        }
    }

    /// <summary>
    /// 用于分割附件的分割符.
    /// </summary>
    private string _boundary  = "=====000_HuolxPubClass113273537350_=====";
    /// <summary>
    /// 分隔符
    /// </summary>
    private string _boundary1 = "=====001_HuolxPubClass113273537350_=====";

    /// <summary>
    /// 用于存放附件路径的信息
    /// </summary>		
    private List<string> _attachments = new List<string>();

    /// <summary>
    /// 附件的BASE64编码字符串
    /// </summary>
    /// <param name="path">附件路径</param>
    private string AttachmentB64Str(string path)
    {
        FileStream fs;
        try
        {
            fs = new FileStream(path, FileMode.Open,  FileAccess.Read, FileShare.Read);
        }
        catch(Exception ex)
        {
            _errmsg += "要附加的文件不存在" + _enter;
            $"{_errmsg},{ex}".LogError();

            return Base64Encode("要附加的文件:" + path + "不存在");
        }
        int fl = (int)fs.Length;
        byte[] barray = new byte[fl];
        fs.Read(barray, 0, fl);
        fs.Close();
        return B64StrLine(Convert.ToBase64String(barray));
    }

    /// <summary>
    /// 如果文件名中含有非英文字母，则将其编码
    /// </summary>
    private string AttachmentNameStr(string fn)
    {
        if (Encoding.Default.GetByteCount(fn) > fn.Length)
        {
            return "=?" + Charset.ToUpper() + "?B?" + Base64Encode(fn) + "?=";
        }
        else
        {
            return fn;
        }
    }

    private string B64StrLine(string str)
    {
        StringBuilder b64Sb = new StringBuilder(str);
        for (int i = 76; i < b64Sb.Length; i += 78)
        {
            b64Sb.Insert(i, _enter);
        }
        return b64Sb.ToString();
    }

    /// <summary>
    /// 将字符串编码为Base64字符串
    /// </summary>
    /// <param name="str">要编码的字符串</param>
    private string Base64Encode(string str)
    {
        byte[] barray;
        barray = Encoding.Default.GetBytes(str);
        return Convert.ToBase64String(barray);
    }

    /// <summary>
    /// 将Base64字符串解码为普通字符串
    /// </summary>
    /// <param name="dstr">要解码的字符串</param>
    private string Base64Decode(string dstr)
    {
        byte[] barray;
        barray = Convert.FromBase64String(dstr);
        return Encoding.Default.GetString(barray);
    }

    #endregion

    #region 嵌入图片处理

    private Hashtable _embedList = new Hashtable(); //widened scope for MatchEvaluator

    /// <summary>
    /// 修改HTML页面中的图片引用为嵌入式图片邮件内容
    /// </summary>
    /// <param name="rawHtml">原始HTML内容</param>
    /// <param name="extras"></param>
    /// <param name="boundaryString"></param>
    /// <returns></returns>
    private string FixupReferences(string rawHtml, ref StringBuilder extras, string boundaryString)
    {
        //Build a symbol table to avoid redundant embedding.
        Regex imgRe, linkRe, hrefRe;
        MatchCollection imgMatches;

        //图片查找正则表达式
        string imgMatchExpression = @"(?<=img+.+src\=[\x27\x22])(?<Url>[^\x27\x22]*)(?=[\x27\x22])";
        imgRe = new Regex(imgMatchExpression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        //Link内容查找正则表达式
        string linkMatchExpression = "<\\s*link[^>]+href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))[^>]*>";
        linkRe = new Regex(linkMatchExpression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        //修正页面内相对URL地址表达式
        string refMatchExpression = "href\\s*=\\s*(?:['\"](?<1>[^\"]*)['\"]|(?<1>\\S+))";
        hrefRe = new Regex(refMatchExpression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        imgMatches = imgRe.Matches(rawHtml);
        foreach (Match m in imgMatches)
        {
            if (!_embedList.ContainsKey(m.Groups[1].Value))
            {
                _embedList.Add(m.Groups[1].Value, Guid.NewGuid());
            }
        }

        //准备嵌入数据
        extras.Length = 0;
        string contentType;
        ArrayList embeddees = new ArrayList(_embedList.Keys);
        foreach (string embeddee in embeddees)
        {
            contentType = embeddee.Substring(embeddee.LastIndexOf(".") + 1).ToLower();
            extras.AppendFormat(boundaryString);
            if (contentType.Equals("jpg")) contentType = "jpeg";
            switch (contentType)
            {
                case "jpeg":
                case "gif":
                case "png":
                case "bmp":
                    extras.AppendFormat("Content-Type: image/{0}; charset=\"iso-8859-1\"\r\n", contentType);
                    extras.Append("Content-Transfer-Encoding: base64\r\n");
                    extras.Append("Content-Disposition: inline\r\n");
                    extras.AppendFormat("Content-ID: <{0}>\r\n\r\n", _embedList[embeddee]);
                    extras.Append(GetDataAsBase64(embeddee));
                    extras.Append("\r\n");
                    break;
            }
        }
        //Fixups for references to items now embedded
        rawHtml = imgRe.Replace(rawHtml, FixupEmbedPath);
        return rawHtml;
    }

    /// <summary>
    /// 修正嵌入图片的地址应用为cid:*** 
    /// </summary>
    private string FixupEmbedPath(Match m)
    {
        string replaceThis = m.Groups[1].Value;
        string withThis = $"cid:{_embedList[replaceThis]}";
        return m.Value.Replace(replaceThis, withThis);
    }

    private string GetDataAsBase64(string sUrl)
    {
        WebClient webClient = new WebClient();
        webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible;MSIE 6.0)");
        MemoryStream memoryStream = new MemoryStream();
        Stream stream = webClient.OpenRead(sUrl);
        byte[] chunk = new byte[4096];
        int cbChunk;
        while ((cbChunk = stream.Read(chunk, 0, 4096)) > 0)
            memoryStream.Write(chunk, 0, cbChunk);
        stream.Close();
        byte[] buf = new byte[memoryStream.Length];
        memoryStream.Position = 0;
        memoryStream.Read(buf, 0, (int)memoryStream.Length);
        memoryStream.Close();
        string b64 = Convert.ToBase64String(buf);
        StringBuilder base64 = new StringBuilder();
        int i;
        for (i = 0; i + 60 < b64.Length; i += 60)
            base64.AppendFormat("{0}\r\n", b64.Substring(i, 60));
        base64.Append(b64.Substring(i));
        for (i = 0; i < (60 - (b64.Length % 60)); i++) base64.Append('=');
        base64.Append("\r\n");
        return base64.ToString();
    }

    private string GetDataAsString(string sUrl)
    {
        WebClient webClient = new WebClient();
        webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible;MSIE 6.0)");
        return (new StreamReader(webClient.OpenRead(sUrl))).ReadToEnd();
    }
    #endregion

    #region 收件人
    /// <summary>
    /// 添加一个收件人
    /// </summary>	
    /// <param name="str">收件人地址</param>
    /// <param name="ra"></param>
    private bool AddRs(string str, ArrayList ra)
    {
        str = str.Trim();

        if (str == null || str == "" || str.IndexOf("@") == -1)
        {
            return true;
            //				上面的语句自动滤除无效的收件人，为了不影响正常运作，未返回错误，如果您需要严格的检查收件人，请替换为下面的语句。
            //				errmsg+="存在无效收件人：" +str;
            //				return false;
        }

        if (ra.Count < _recipientMaxNum)
        {
            ra.Add(str);
            return true;
        }
        else
        {
            _errmsg += "收件人过多";
            return false;
        }
    }
    /// <summary>
    /// 添加一组收件人（不超过10个），参数为字符串数组
    /// </summary>
    /// <param name="str">保存有收件人地址的字符串数组（不超过10个）</param>	
    /// <param name="ra">添加的数组列表</param>
    private bool AddRs(string[] str, ArrayList ra)
    {
        for (int i = 0; i < str.Length; i++)
        {
            if (!AddRs(str[i], ra))
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// 添加一个收件人
    /// </summary>	
    /// <param name="str">收件人地址</param>
    public bool AddRecipient(string str)
    {
        return AddRs(str, _recipient);
    }

    /// <summary>
    /// 指定一个收件人
    /// </summary>	
    /// <param name="str">收件人地址</param>
    public void SetRecipient(string str)
    {
        //return AddRs(str, Recipient);
        _recipient.Clear();
        _recipient.Add(str);
    }

    /// <summary>
    /// 添加一组收件人（不超过10个），参数为字符串数组
    /// </summary>
    /// <param name="str">保存有收件人地址的字符串数组（不超过RecipientMaxNum个）</param>	
    public bool AddRecipient(string[] str)
    {
        return AddRs(str, _recipient);
    }

    /// <summary>
    /// 添加一个抄送收件人
    /// </summary>
    /// <param name="str">收件人地址</param>
    public bool AddRecipientCc(string str)
    {
        return AddRs(str, _recipientCc);
    }

    /// <summary>
    /// 添加一组抄送收件人（不超过10个），参数为字符串数组
    /// </summary>	
    /// <param name="str">保存有收件人地址的字符串数组（不超过RecipientMaxNum个）</param>
    public bool AddRecipientCc(string[] str)
    {
        return AddRs(str, _recipientCc);
    }

    /// <summary>
    /// 添加一个密件收件人
    /// </summary>
    /// <param name="str">收件人地址</param>
    public bool AddRecipientBcc(string str)
    {
        return AddRs(str, _recipientBcc);
    }

    /// <summary>
    /// 添加一组密件收件人（不超过10个），参数为字符串数组
    /// </summary>	
    /// <param name="str">保存有收件人地址的字符串数组（不超过RecipientMaxNum个）</param>
    public bool AddRecipientBcc(string[] str)
    {
        return AddRs(str, _recipientBcc);
    }

    /// <summary>
    /// 清空收件人列表
    /// </summary>
    public void ClearRecipient()
    {
        _recipient.Clear();
    }

    #endregion

    #region 连接邮件服务器

    /// <summary>
    /// 发送SMTP命令
    /// </summary>	
    private bool SendCommand(string command)
    {
        byte[] writeBuffer;
        if (command == null || command.Trim() == "")
        {
            return true;
        }
        //logs+=Command;
        writeBuffer = Encoding.Default.GetBytes(command);
        try
        {
            _ns.Write(writeBuffer, 0, writeBuffer.Length);
        }
        catch
        {
            _errmsg = "网络连接错误";
            return false;
        }
        return true;
    }

    /// <summary>
    /// 接收SMTP服务器回应
    /// </summary>
    private string RecvResponse()
    {
        int streamSize;
        string returnValue = "false";
        byte[] readBuffer = new byte[4096];

        try
        {
            streamSize = _ns.Read(readBuffer, 0, readBuffer.Length);
        }
        catch
        {
            _errmsg = "网络连接错误";
            return returnValue;
        }

        if (streamSize == 0)
        {
            return returnValue;
        }
        else
        {
            returnValue = Encoding.Default.GetString(readBuffer).Substring(0, streamSize).Trim(); ;
            //logs+=ReturnValue;
            return returnValue;
        }
    }

    /// <summary>
    /// 与服务器交互，发送一条命令并接收回应。
    /// </summary>
    /// <param name="command">一个要发送的命令</param>
    /// <param name="errstr">如果错误，要反馈的信息</param>
    private bool Dialog(string command, string errstr)
    {
        if (command == null || command.Trim() == "")
        {
            return true;
        }
        if (SendCommand(command))
        {
            string rr = RecvResponse();
            if (rr == "false")
            {
                return false;
            }
            string rrCode = "";

            if (rr.Length >= 3)
                rrCode = rr.Substring(0, 3);
            else
                rrCode = rr;

            if (_errCodeHt[rrCode] != null)
            {
                _errmsg += (rrCode + _errCodeHt[rrCode]);
                _errmsg += _enter;
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// 与服务器交互，发送一组命令并接收回应。
    /// </summary>
    private bool Dialog(ArrayList command, string errstr)
    {
        foreach (String item in command)
        {
            if (!Dialog(item, ""))
            {
                _errmsg += _enter;
                _errmsg += errstr;
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// SMTP验证过程.
    /// </summary>
    private bool SmtpAuth()
    {
        ArrayList sendBuffer = new ArrayList();
        string sendBufferstr;
        sendBufferstr = "EHLO " + _mailserver + _enter;
        //			SendBufferstr="HELO " + mailserver + enter;
        //这个地方经常出现命令错位，不得以加入特殊控制代码，才能正常执行。
        //以后最好能有更好的解决办法。
        if (SendCommand(sendBufferstr))
        {
            while (true)
            {
                int i = 0;
                if (_ns.DataAvailable)
                {
                    string rr = RecvResponse();
                    if (rr == "false")
                    {
                        return false;
                    }
                    string rrCode = rr.Substring(0, 3);
                    if (_rightCodeHt[rrCode] != null)
                    {
                        if (rr.IndexOf("AUTH") != -1)
                        {
                            break;
                        }
                    }
                    else
                    {
                        if (_errCodeHt[rrCode] != null)
                        {
                            _errmsg += (rrCode + _errCodeHt[rrCode]);
                            _errmsg += _enter;
                            _errmsg += "发送EHLO命令出错，服务器可能不需要验证" + _enter;
                        }
                        else
                        {
                            _errmsg += rr;
                            _errmsg += "发送EHLO命令出错，不明错误,请与作者联系" + _enter;
                        }
                        return false;
                    }
                }
                else
                {
                    System.Threading.Thread.Sleep(50);
                    i++;
                    if (i > 6)
                    {
                        _errmsg += "收不到AUTH指令，可能是连接超时，或者服务器根本不需要验证" + _enter;
                        return false;

                    }
                }

            }
        }
        else
        {
            _errmsg += "发送ehlo命令失败";
            return false;

        }

        sendBuffer.Add("AUTH LOGIN" + _enter);
        sendBuffer.Add(Base64Encode(_username) + _enter);
        sendBuffer.Add(Base64Encode(_password) + _enter);
        if (!Dialog(sendBuffer, "SMTP服务器验证失败，请核对用户名和密码。"))
            return false;
        return true;
    }

    #endregion

    #region 发送

    /// <summary>
    /// 发送邮件
    /// </summary>
    public bool SendEmail()
    {
        bool checkFlag = Check();
        if (!checkFlag)
            return false;

        #region 连接网络
        try
        {
            _tc = new TcpClient(_mailserver, _mailserverport);
            _ns = _tc.GetStream();
        }
        catch (Exception e)
        {
            _errmsg = e.ToString();
            return false;
        }

        //验证网络连接是否正确
        if (_rightCodeHt[RecvResponse().Substring(0, 3)] == null)
        {
            _errmsg = "网络连接失败";
            return false;
        } 
        #endregion

        #region 验证发件收件人

        ArrayList sendBuffer = new ArrayList();
        string sendBufferstr;

        //进行SMTP验证
        if (_useSmtpAuth)
        {
            if (!SmtpAuth())
                return false;
        }
        else
        {
            sendBufferstr = "HELO " + _mailserver + _enter;
            if (!Dialog(sendBufferstr, ""))
                return false;
        }

        //发件人信息
        sendBufferstr = "MAIL FROM:<" + From + ">" + _enter;
        if (!Dialog(sendBufferstr, "发件人地址错误，或不能为空"))
            return false;

        //收件人列表
        sendBuffer.Clear();
        foreach (String item in _recipient)
        {
            sendBuffer.Add("RCPT TO:<" + item + ">" + _enter);
            RecipientName = item;//这里其实只能支持一个收件人
        }
        if (!Dialog(sendBuffer, "收件人地址有误"))
            return false; 

        #endregion

        #region 邮件头部

        //开始发送信件内容
        sendBufferstr = "DATA" + _enter;
        if (!Dialog(sendBufferstr, ""))
            return false;
		           
        //发件人
        sendBufferstr = "From:\"" + FromName + "\" <" + From + ">" + _enter;
        //收件人
        sendBufferstr += "To:\"" + RecipientName + "\" <" + RecipientName + ">" + _enter;

        //回复地址
        if (ReplyTo.Trim() != "")
        {
            sendBufferstr += "Reply-To: " + ReplyTo + _enter;
        }

        //抄送收件人列表
        if (_recipientCc.Count > 0)
        {
            sendBufferstr += "CC:";
            foreach (String item in _recipientCc)
            {
                sendBufferstr += item + "<" + item + ">," + _enter;
            }
            sendBufferstr = sendBufferstr.Substring(0, sendBufferstr.Length - 3) + _enter;
        }

        //密件收件人列表
        if (_recipientBcc.Count > 0)
        {
            sendBufferstr += "BCC:";
            foreach (String item in _recipientBcc)
            {
                sendBufferstr += item + "<" + item + ">," + _enter;
            }
            sendBufferstr = sendBufferstr.Substring(0, sendBufferstr.Length - 3) + _enter;
        }

        //邮件主题
        if (Charset == "")
        {
            sendBufferstr += "Subject:" + Subject + _enter;
        }
        else
        {
            sendBufferstr += "Subject:" + "=?" + Charset.ToUpper() + "?B?" + Base64Encode(Subject) + "?=" + _enter;
        }

        //是否需要收件人发送收条
        if (true == ReturnReceipt)
        {
            sendBufferstr += "Disposition-Notification-To: \"" + FromName + "\" <" + ReplyTo + ">" + _enter;
        }

        #endregion         

        #region 邮件内容

        sendBufferstr += "X-Priority:" + _priority + _enter;
        sendBufferstr += "X-MSMail-Priority:" + _priority + _enter;
        sendBufferstr += "Importance:" + _priority + _enter;
        sendBufferstr += "X-Mailer: Huolx.Pubclass" + _enter;
        sendBufferstr += "MIME-Version: 1.0" + _enter;

        sendBufferstr += "Content-Type: multipart/mixed;" + _enter;
        sendBufferstr += "	boundary=\"" + _boundary + "\"" + _enter + _enter;
        sendBufferstr += "This is a multi-part message in MIME format." + _enter + _enter;
        sendBufferstr += "--" + _boundary + _enter;
        sendBufferstr += "Content-Type: multipart/alternative;" + _enter;
        sendBufferstr += "	boundary=\"" + _boundary1 + "\"" + _enter + _enter + _enter;
        sendBufferstr += "--" + _boundary1 + _enter;

        //判断信件格式是否html
        if (IsHtml)
        {
            sendBufferstr += "Content-Type: text/html;" + _enter;
        }
        else
        {
            sendBufferstr += "Content-Type: text/plain;" + _enter;
        }
        //编码信息
        if (Charset == "")
        {
            sendBufferstr += "	charset=\"iso-8859-1\"" + _enter;
        }
        else
        {
            sendBufferstr += "	charset=\"" + Charset.ToLower() + "\"" + _enter;
        }
        sendBufferstr += "Content-Transfer-Encoding: base64" + _enter;

        StringBuilder extras = new StringBuilder();
        string extrasBoundary = "--" + _boundary + _enter;
        string newBodyHtml = FixupReferences(Body, ref extras, extrasBoundary);
        sendBufferstr += _enter + _enter;
        sendBufferstr += B64StrLine(Base64Encode(newBodyHtml)) + _enter;

        sendBufferstr += _enter + "--" + _boundary1 + "--" + _enter + _enter;
        sendBufferstr += extras.ToString();

        //如果有附件,开始发送附件.
        if (_attachments.Count > 0)
        {
            sendBufferstr += _enter + "--" + _boundary1 + "--" + _enter + _enter;
            foreach (String item in _attachments)
            {
                sendBufferstr += "--" + _boundary + _enter;
                sendBufferstr += "Content-Type: application/octet-stream;" + _enter;
                sendBufferstr += "	name=\"" + AttachmentNameStr(item.Substring(item.LastIndexOf("\\") + 1)) + "\"" + _enter;
                sendBufferstr += "Content-Transfer-Encoding: base64" + _enter;
                sendBufferstr += "Content-Disposition: attachment;" + _enter;
                sendBufferstr += "	filename=\"" + AttachmentNameStr(item.Substring(item.LastIndexOf("\\") + 1)) + "\"" + _enter + _enter;
                sendBufferstr += AttachmentB64Str(item) + _enter + _enter;
            }
            sendBufferstr += "--" + _boundary + "--" + _enter + _enter;
        }
        sendBufferstr += _enter + "." + _enter; 

        if (!Dialog(sendBufferstr, "错误信件信息"))
            return false;

        #endregion

        sendBufferstr = "QUIT" + _enter;
        if (!Dialog(sendBufferstr, "断开连接时错误"))
            return false;
            
        _ns.Close();
        _tc.Close();
        return true;
    }

    /// <summary>
    /// 发送邮件前对参数进行检查，通过检查返回True，否则为False
    /// </summary>
    /// <returns>如果通过检查返回true，否则为false</returns>
    private bool Check()
    {
        if (_recipient.Count == 0)
        {
            _errmsg = "收件人列表不能为空";
            return false;
        }

        if (RecipientName == "")
        {
            RecipientName = _recipient[0].ToString();
        }

        if (_mailserver.Trim() == "")
        {
            _errmsg = "必须指定SMTP服务器";
            return false;
        }

        return true;
    }

    #endregion
}