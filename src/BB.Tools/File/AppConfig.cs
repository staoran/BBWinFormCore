using System.Xml;

namespace BB.Tools.File;

/// <summary>
/// 用于获取或设置Web.config/*.exe.config中节点数据的辅助类
/// </summary>
public sealed class AppConfig
{
    private string _filePath;

    /// <summary>
    /// 从当前目录中按顺序检索Web.Config和*.App.Config文件。
    /// 如果找到一个，则使用它作为配置文件；否则会抛出一个ArgumentNullException异常。
    /// </summary>
    public AppConfig()
    {
        string webconfig = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Web.Config");
        string appConfig = AppDomain.CurrentDomain.SetupInformation.TargetFrameworkName.Replace(".vshost", "");

        if (System.IO.File.Exists(webconfig))
        {
            _filePath = webconfig;
        }
        else if (System.IO.File.Exists(appConfig))
        {
            _filePath = appConfig;
        }
        else
        {
            throw new ArgumentNullException("没有找到Web.Config文件或者应用程序配置文件, 请指定配置文件");
        }
    }

    /// <summary>
    /// 用户指定具体的配置文件路径
    /// </summary>
    /// <param name="configFilePath">配置文件路径（绝对路径）</param>
    public AppConfig(string configFilePath)
    {
        _filePath = configFilePath;
    }

    /// <summary>
    /// 设置程序的config文件
    /// </summary>
    /// <param name="keyName">键名</param>
    /// <param name="keyValue">键值</param>
    public void AppConfigSet(string keyName, string keyValue)
    {
        //由于存在多个Add键值，使得访问appSetting的操作不成功，故注释下面语句，改用新的方式
        /* 
        string xpath = "//add[@key='" + keyName + "']";
        XmlDocument document = new XmlDocument();
        document.Load(filePath);

        XmlNode node = document.SelectSingleNode(xpath);
        node.Attributes["value"].Value = keyValue;
        document.Save(filePath); 
         */

        XmlDocument document = new XmlDocument();
        document.Load(_filePath);

        XmlNodeList nodes = document.GetElementsByTagName("add");
        for (int i = 0; i < nodes.Count; i++)
        {
            //获得将当前元素的key属性
            XmlAttribute attribute = nodes[i].Attributes["key"];
            //根据元素的第一个属性来判断当前的元素是不是目标元素
            if (attribute != null && (attribute.Value == keyName))
            {
                attribute = nodes[i].Attributes["value"];
                //对目标元素中的第二个属性赋值
                if (attribute != null)
                {
                    attribute.Value = keyValue;
                    break;
                }
            }
        }
        document.Save(_filePath);
    }

    /// <summary>
    /// 读取程序的config文件的键值。
    /// 如果键名不存在，返回空
    /// </summary>
    /// <param name="keyName">键名</param>
    /// <returns></returns>
    public string AppConfigGet(string keyName)
    {
        string strReturn = string.Empty;
        try
        {
            XmlDocument document = new XmlDocument();
            document.Load(_filePath);

            XmlNodeList nodes = document.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute attribute = nodes[i].Attributes["key"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (attribute != null && (attribute.Value == keyName))
                {
                    attribute = nodes[i].Attributes["value"];
                    if (attribute != null)
                    {
                        strReturn = attribute.Value;
                        break;
                    }
                }
            }
        }
        catch
        {
            ;
        }

        return strReturn;
    }

    /// <summary>
    /// 获取指定键名中的子项的值
    /// </summary>
    /// <param name="keyName">键名</param>
    /// <param name="subKeyName">以分号(;)为分隔符的子项名称</param>
    /// <returns>对应子项名称的值（即是=号后面的值）</returns>
    public string GetSubValue(string keyName, string subKeyName)
    {
        string connectionString = AppConfigGet(keyName).ToLower();
        string[] item = connectionString.Split(new[] { ';' });

        for (int i = 0; i < item.Length; i++)
        {
            string itemValue = item[i].ToLower();
            if (itemValue.IndexOf(subKeyName.ToLower()) >= 0) //如果含有指定的关键字
            {
                int startIndex = item[i].IndexOf("="); //等号开始的位置
                return item[i].Substring(startIndex + 1); //获取等号后面的值即为Value
            }
        }
        return string.Empty;
    }

    #region 一些常用的配置项属性

    /// <summary>
    /// 从配置文件获取权限系统链接（配置项HWSecurity的值）
    /// </summary>
    public string HwSecurity => AppConfigGet("HWSecurity");

    /// <summary>
    /// 系统的标识ID（配置项System_ID的值）
    /// </summary>
    public string SystemId => AppConfigGet("System_ID");

    /// <summary>
    /// 应用程序名称（配置项ApplicationName的值）
    /// </summary>
    public string AppName => AppConfigGet("ApplicationName");

    /// <summary>
    /// 软件厂商名称（配置项Manufacturer的值）
    /// </summary>
    public string Manufacturer => AppConfigGet("Manufacturer");

    /// <summary>
    /// 设置程序的config文件的Enterprise Library的数据库链接地址
    /// </summary>
    /// <param name="keyName">键名</param>
    /// <param name="keyValue">键值</param>
    public void SetConnectionString(string keyName, string keyValue)
    {
        XmlDocument document = new XmlDocument();
        document.Load(_filePath);

        XmlNodeList nodes = document.GetElementsByTagName("add");
        for (int i = 0; i < nodes.Count; i++)
        {
            //获得将当前元素的name属性
            XmlAttribute att = nodes[i].Attributes["name"];
            //根据元素的第一个属性来判断当前的元素是不是目标元素
            if (att != null && (att.Value == keyName))
            {
                att = nodes[i].Attributes["connectionString"];
                if (att != null)
                {
                    att.Value = keyValue;
                    break;
                }
            }
        }
        document.Save(_filePath);
    }

    /// <summary>
    /// 读取程序的config文件Enterprise Library的数据库链接地址
    /// </summary>
    /// <param name="keyName">键名</param>
    /// <returns></returns>
    public string GetConnectionString(string keyName)
    {
        string strReturn = string.Empty;
        try
        {
            XmlDocument document = new XmlDocument();
            document.Load(_filePath);

            XmlNodeList nodes = document.GetElementsByTagName("add");
            for (int i = 0; i < nodes.Count; i++)
            {
                //获得将当前元素的key属性
                XmlAttribute att = nodes[i].Attributes["name"];
                //根据元素的第一个属性来判断当前的元素是不是目标元素
                if (att != null && (att.Value == keyName))
                {
                    att = nodes[i].Attributes["connectionString"];
                    if (att != null)
                    {
                        strReturn = att.Value;
                        break;
                    }
                }
            }
        }
        catch
        { ; }

        return strReturn;
    }

    /// <summary>
    /// 获取数据库配置信息
    /// </summary>
    /// <param name="keyName">节点名称</param>
    /// <returns></returns>
    public DatabaseInfo GetDatabaseInfo(string keyName)
    {
        string connectionString = GetConnectionString(keyName);
        return new DatabaseInfo(connectionString);
    }

    /// <summary>
    /// 设置数据库配置信息
    /// </summary>
    /// <param name="keyName"></param>
    /// <param name="databaseInfo"></param>
    public void SetDatabaseInfo(string keyName, DatabaseInfo databaseInfo)
    {
        SetConnectionString(keyName, databaseInfo.ConnectionString);
    }

    #endregion
}