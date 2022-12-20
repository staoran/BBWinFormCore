using BB.Tools.Encrypt;
using BB.Tools.File;
using System.IO;

namespace BB.Starter.UI.Other;

public class MyConstants
{
    private static string _mConfigFile = "";

    /// <summary>
    /// 默认的配置文件路径
    /// </summary>
    public static string ConfigFile
    {
        get
        {
            if (string.IsNullOrEmpty(_mConfigFile))
            {
                string searchPattern = "*.exe.config";
                string[] files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, searchPattern, SearchOption.TopDirectoryOnly);
                foreach (string filePath in files)
                {
                    if (!filePath.Contains(".vshost"))
                    {
                        _mConfigFile = filePath;
                        break;
                    }
                }
            }
            return _mConfigFile;
        }
        set => _mConfigFile = value;
    }

    private static string _mLicense = "";

    /// <summary>
    /// 授权用户的授权码
    /// </summary>
    public static string License
    {
        get
        {
            if (string.IsNullOrEmpty(_mLicense))
            {
                AppConfig config = new AppConfig(ConfigFile);
                string securityLicense = config.AppConfigGet("SecurityLicense");
                return securityLicense;
            }
            else
            {
                return _mLicense;
            }
        }
        set => _mLicense = value;
    }
}

public class LicenseCheckResult
{
    /// <summary>
    /// 是否验证通过
    /// </summary>
    public bool IsValided = false;

    /// <summary>
    /// 注册的用户名称
    /// </summary>
    public string Username = "";

    /// <summary>
    /// 注册的公司名称
    /// </summary>
    public string CompanyName = "";

    /// <summary>
    /// 是否显示授权信息
    /// </summary>
    public bool DisplayCopyright = true;
}

public class LicenseTool
{
    /// <summary>
    /// 检查用户的授权码
    /// </summary>
    /// <returns></returns>
    public static LicenseCheckResult CheckLicense()
    {
        LicenseCheckResult result = new LicenseCheckResult();
        string license = MyConstants.License;
        if (!string.IsNullOrEmpty(license))
        {
            try
            {
                string decodeLicense = Base64Util.Decrypt(Md5Util.RemoveMd5Profix(license));
                string[] strArray = decodeLicense.Split('|');
                if (strArray.Length >= 4)
                {
                    string componentType = strArray[0];
                    if (componentType.ToLower() == "BB.Framework")
                    {
                        result.IsValided = true;
                    }
                    result.Username = strArray[1];
                    result.CompanyName = strArray[2];
                    try
                    {
                        result.DisplayCopyright = Convert.ToBoolean(strArray[3]);
                    }
                    catch
                    {
                        result.DisplayCopyright = true;
                    }

                    return result;

                    #region 设置显示内容
                    //string displayText = string.Format("该组件已授权给：");
                    //if (!string.IsNullOrEmpty(LicenseResult.CompanyName))
                    //{
                    //    displayText += string.Format("{0}", LicenseResult.CompanyName);
                    //}
                    //if (!string.IsNullOrEmpty(LicenseResult.Username))
                    //{
                    //    displayText += string.Format(" ({0})", LicenseResult.Username);
                    //}
                    //this.tssLink.Text = displayText;
                    //this.tssLink.Visible = LicenseResult.DisplayCopyright; 
                    #endregion
                }
            }
            catch
            {
            }
        }

        return result;
    }
}