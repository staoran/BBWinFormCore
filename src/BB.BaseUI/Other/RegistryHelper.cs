using Microsoft.Win32;

namespace BB.BaseUI.Other;

/// <summary>
/// 注册表操作辅助类，通过默认指定注册表的前缀路径，减少调用复杂性。
/// </summary>
public sealed class RegistryHelper
{
    #region 基础操作（读取和保存）
    private static string _softwareKey = @"Software\DeepLand\Hussar";

    /// <summary>
    /// 获取注册表项的值。如果该键不存在，则返回空字符串。
    /// </summary>
    /// <param name="key">注册表键</param>
    /// <returns>指定键返回的值</returns>
    public static string GetValue(string key)
    {
        return GetValue(_softwareKey, key);
    }

    /// <summary>
    /// 获取注册表项的值。如果该键不存在，则返回空字符串。
    /// </summary>
    /// <param name="softwareKey">注册表键的前缀路径</param>
    /// <param name="key">注册表键</param>
    /// <returns>指定键返回的值</returns>
    public static string GetValue(string softwareKey, string key)
    {
        return GetValue(softwareKey, key, Registry.CurrentUser);
    }

    /// <summary>
    /// 获取注册表项的值。如果该键不存在，则返回空字符串。
    /// </summary>
    /// <param name="softwareKey">注册表键的前缀路径</param>
    /// <param name="key">注册表键</param>
    /// <param name="rootRegistry">开始的根节点（Registry.CurrentUser或者Registry.LocalMachine等）</param>
    /// <returns>指定键返回的值</returns>
    public static string GetValue(string softwareKey, string key, RegistryKey rootRegistry)
    {
        const string parameter = "key";
        if (null == key)
        {
            throw new ArgumentNullException(parameter);
        }

        string strRet;
        try
        {
            RegistryKey regKey = rootRegistry.OpenSubKey(softwareKey);
            strRet = regKey?.GetValue(key)?.ToString();
        }
        catch
        {
            strRet = "";
        }
        return strRet;
    }

    /// <summary>
    /// 保存键值到注册表
    /// </summary>
    /// <param name="key">注册表键</param>
    /// <param name="value">键的值内容</param>
    /// <returns>如果保存成功返回true，否则为false</returns>
    public static bool SaveValue(string key, string value)
    {
        return SaveValue(_softwareKey, key, value);
    }

    /// <summary>
    /// 保存键值到注册表
    /// </summary>
    /// <param name="softwareKey">注册表键的前缀路径</param>
    /// <param name="key">注册表键</param>
    /// <param name="value">键的值内容</param>
    /// <returns>如果保存成功返回true，否则为false</returns>
    public static bool SaveValue(string softwareKey, string key, string value)
    {
        return SaveValue(softwareKey, key, value, Registry.CurrentUser);
    }

    /// <summary>
    /// 保存键值到注册表
    /// </summary>
    /// <param name="softwareKey">注册表键的前缀路径</param>
    /// <param name="key">注册表键</param>
    /// <param name="value">键的值内容</param>
    /// <param name="rootRegistry">开始的根节点（Registry.CurrentUser或者Registry.LocalMachine等）</param>
    /// <returns>如果保存成功返回true，否则为false</returns>
    public static bool SaveValue(string softwareKey, string key, string value, RegistryKey rootRegistry)
    {
        const string parameter1 = "key";
        const string parameter2 = "value";
        if (null == key)
        {
            throw new ArgumentNullException(parameter1);
        }

        if (null == value)
        {
            throw new ArgumentNullException(parameter2);
        }

        RegistryKey reg;
        reg = rootRegistry.OpenSubKey(softwareKey, true);

        if (null == reg)
        {
            reg = rootRegistry.CreateSubKey(softwareKey);
        }
        reg.SetValue(key, value);

        return true;
    } 
    #endregion

    #region 自动启动程序设置

    /// <summary>
    /// 检查是否随系统启动
    /// </summary>
    /// <returns></returns>
    public static bool CheckStartWithWindows()
    {
        RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
        if (regkey != null && (string)regkey.GetValue(Application.ProductName, "null", RegistryValueOptions.None) != "null")
        {
            Registry.CurrentUser.Flush();
            return true;
        }

        Registry.CurrentUser.Flush();
        return false;
    }

    /// <summary>
    /// 设置随系统启动
    /// </summary>
    /// <param name="startWin"></param>
    public static void SetStartWithWindows(bool startWin)
    {
        RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
        if (regkey != null)
        {
            if (startWin)
            {
                regkey.SetValue(Application.ProductName, Application.ExecutablePath, RegistryValueKind.String);
            }
            else
            {
                regkey.DeleteValue(Application.ProductName, false);
            }

            Registry.CurrentUser.Flush();
        }
    }

    #endregion

    /// <summary>
    /// 获取某个指定子项名称的注册表键对象
    /// </summary>
    /// <param name="regDomain">注册表分类节点</param>
    /// <param name="subKey">要创建或打开的子项的名称或路径</param>
    /// <param name="writable">是否可写</param>
    /// <returns></returns>
    public RegistryKey GetRegistrySubKey(RegDomain regDomain, string subKey, bool writable)
    {
        RegistryKey regDomainKey = GetRegDomainKey(regDomain);
        return regDomainKey.OpenSubKey(subKey, writable);
    }

    /// <summary>
    /// 创建一个新子项或打开一个现有子项以进行写访问。
    /// </summary>
    /// <param name="regDomain">注册表分类节点</param>
    /// <param name="subKey">要创建或打开的子项的名称或路径</param>
    /// <returns></returns>
    public RegistryKey CreateRegistrySubKey(RegDomain regDomain, string subKey)
    {
        RegistryKey regDomainKey = GetRegDomainKey(regDomain);
        return regDomainKey.CreateSubKey(subKey);
    }

    /// <summary>
    /// 删除注册表的指定子项
    /// </summary>
    /// <param name="regDomain">注册表分类节点</param>
    /// <param name="subKey">要创建或打开的子项的名称或路径</param>
    public void DeleteRegistrySubKey(RegDomain regDomain, string subKey)
    {
        RegistryKey regDomainKey = GetRegDomainKey(regDomain);
        regDomainKey.DeleteSubKey(subKey);
        regDomainKey.Close();
    }

    /// <summary>
    /// 判断注册表子项是否存在
    /// </summary>
    /// <param name="regDomain">注册表分类节点</param>
    /// <param name="subKey">要创建或打开的子项的名称或路径</param>
    /// <returns></returns>
    public bool IsRegistrySubKeyExist(RegDomain regDomain, string subKey)
    {
        return GetRegistrySubKey(regDomain, subKey, false) != null;
    }

    /// <summary>
    /// 判断注册表子项的值是否包含某项目
    /// </summary>
    /// <param name="regDomain">注册表分类节点</param>
    /// <param name="subKey">要创建或打开的子项的名称或路径</param>
    /// <param name="strItemName">项目名称</param>
    /// <returns></returns>
    public bool IsRigistryItemExist(RegDomain regDomain, string subKey, string strItemName)
    {
        RegistryKey registrySubKey = GetRegistrySubKey(regDomain, subKey, false);
        string[] valueNames = registrySubKey.GetValueNames();
        bool result = false;
        for (int i = 0; i < valueNames.Length; i++)
        {
            if (valueNames[i] == strItemName)
            {
                result = true;
                break;
            }
        }
        return result;
    }

    /// <summary>
    /// 获取注册表子项中某个项目的值
    /// </summary>
    /// <param name="regDomain">注册表分类节点</param>
    /// <param name="subKey">要创建或打开的子项的名称或路径</param>
    /// <param name="strItemName">项目名称</param>
    /// <returns></returns>
    public object GetRigistryItemValue(RegDomain regDomain, string subKey, string strItemName)
    {
        RegistryKey registrySubKey = GetRegistrySubKey(regDomain, subKey, false);
        object result;
        if (null == registrySubKey)
        {
            result = null;
        }
        else
        {
            result = registrySubKey.GetValue(strItemName);
        }
        return result;
    }

    /// <summary>
    /// 设置注册表子项中某项目的值
    /// </summary>
    /// <param name="regDomain">注册表分类节点</param>
    /// <param name="subKey">要创建或打开的子项的名称或路径</param>
    /// <param name="strItemName">项目名称</param>
    /// <param name="value">项目值内容</param>
    public void SetRigistryItemValue(RegDomain regDomain, string subKey, string strItemName, object value)
    {
        RegistryKey registryKey = GetRegistrySubKey(regDomain, subKey, true);
        if (null == registryKey)
        {
            registryKey = CreateRegistrySubKey(regDomain, subKey);
        }
        registryKey.SetValue(strItemName, value);
        registryKey.Close();
    }

    /// <summary>
    /// 删除注册表子项中某项目的值
    /// </summary>
    /// <param name="regDomain">注册表分类节点</param>
    /// <param name="subKey">要创建或打开的子项的名称或路径</param>
    /// <param name="strItemName">项目名称</param>
    public void DeleteRigistryItemValue(RegDomain regDomain, string subKey, string strItemName)
    {
        RegistryKey registrySubKey = GetRegistrySubKey(regDomain, subKey, true);
        registrySubKey.DeleteValue(strItemName);
        registrySubKey.Close();
    }

    /// <summary>
    /// 根据枚举类型转换为对应的注册表键对象
    /// </summary>
    /// <param name="regDomain"></param>
    /// <returns></returns>
    public RegistryKey GetRegDomainKey(RegDomain regDomain)
    {
        RegistryKey result;
        switch (regDomain)
        {
            case RegDomain.RegDomainRoot:
                result = Registry.ClassesRoot;
                break;
            case RegDomain.RegDomainCurrentUser:
                result = Registry.CurrentUser;
                break;
            case RegDomain.RegDomainLocalMachine:
                result = Registry.LocalMachine;
                break;
            case RegDomain.RegDomainUsers:
                result = Registry.Users;
                break;
            case RegDomain.RegDomainConfig:
                result = Registry.CurrentConfig;
                break;
            case RegDomain.RegDomainPerformanceData:
                result = Registry.PerformanceData;
                break;
            default:
                result = null;
                break;
        }
        return result;
    }

}

/// <summary>
/// 定义注册表类型的几种类型
/// </summary>
public enum RegDomain
{
    RegDomainRoot,
    RegDomainCurrentUser,
    RegDomainLocalMachine,
    RegDomainUsers,
    RegDomainConfig,
    RegDomainPerformanceData
}