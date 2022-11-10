using System.Diagnostics;
using System.Text;
using BB.BaseUI.BaseUI;
using BB.BaseUI.Extension;
using BB.Entity.Base;
using BB.Entity.Dictionary;
using BB.Entity.Security;
using BB.Entity.TMS;
using BB.HttpServices.Core.Dict;
using BB.HttpServices.Core.Menu;
using BB.HttpServices.Core.OU;
using BB.HttpServices.Core.Region;
using BB.HttpServices.Core.RoleData;
using BB.HttpServices.Core.User;
using BB.HttpServices.TMS;
using BB.Tools.Device;
using BB.Tools.Encrypt;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.File;
using BB.Tools.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using Furion;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Win32;

namespace BB.BaseUI.Other;

public static class GB
{
    #region 系统全局变量
        
    public static dynamic? MainDialog;

    /// <summary>
    /// 单位名称
    /// </summary>
    public static string AppUnit = string.Empty; //单位名称
        
    /// <summary>
    /// 程序名称
    /// </summary>
    public static string AppName = string.Empty;  //程序名称
        
    /// <summary>
    /// 单位名称+程序名称
    /// </summary>
    public static string AppWholeName = string.Empty;//单位名称+程序名称
        
    /// <summary>
    /// 系统类型
    /// </summary>
    public static string SystemType = "WareMis";//系统类型
    
    /// <summary>
    /// 系统登陆用户名记忆Key
    /// </summary>
    public static string LoginNameKey = "HussarLoginName";
        
    /// <summary>
    /// 授权码检查的结果
    /// </summary>
    public static LicenseCheckResult LicenseResult = new();//授权码检查的结果
        
    /// <summary>
    /// 程序配置
    /// </summary>
    public static readonly AppConfig Config = new();// config 全局变量 只一次读取config文件提高效率
    
    /// <summary>
    /// 前后台会话ID
    /// </summary>
    public static string? SessionId { get; set; }

    /// <summary>
    /// 登录用户具有的功能字典集合
    /// </summary>
    public static readonly Dictionary<string, string> FunctionDict = new();//登录用户具有的功能字典集合

    /// <summary>
    /// 登录用户具有的菜单集合
    /// </summary>
    public static List<MenuNodeInfo> UserMenuNode = new();

    /// <summary>
    /// 用户具有的角色集合
    /// </summary>
    public static List<RoleInfo> RoleList { get; set; } = new();

    /// <summary>
    /// 菜单中注册的功能按钮
    /// </summary>
    public static readonly Dictionary<string, string> AllFunctionDict = new();
        
    /// <summary>
    /// 是否注册
    /// </summary>
    public static bool Registed { get; set;}
        
    /// <summary>
    /// 设置一个开关，确定是否要求注册后才能使用软件
    /// </summary>
    public static readonly bool EnableRegister = false; //设置一个开关，确定是否要求注册后才能使用软件

    /// <summary>
    /// 登陆用户基础信息
    /// </summary>
    public static LoginUserInfo LoginUserInfo
    {
        get => _loginUserInfo ?? throw new Exception("登陆用户为空");
        set => _loginUserInfo = value;
    }

    private static LoginUserInfo? _loginUserInfo;

    /// <summary>
    /// 全部用户字典
    /// </summary>
    public static List<CListItem> AllUserDict { get; } = new();
        
    /// <summary>
    /// 有效的用户字典
    /// </summary>
    public static List<CListItem> EnabledUserDict { get; } = new();

    /// <summary>
    /// 全部公司和部门字典
    /// </summary>
    public static List<CListItem> AllOuDict { get; } = new();

    /// <summary>
    /// 有效的公司和部门字典
    /// </summary>
    public static List<CListItem> EnabledOuDict { get; } = new();

    /// <summary>
    /// 全部费用类型
    /// </summary>
    public static List<CListItem> AllCostType { get; } = new();

    /// <summary>
    /// 已审核的费用类型
    /// </summary>
    public static List<CListItem> EnabledCostType { get; } = new();

    /// <summary>
    /// 全部预付金操作类型
    /// </summary>
    public static List<CListItem> AllCostBillType { get; } = new();

    /// <summary>
    /// 已审核的预付金操作类型
    /// </summary>
    public static List<CListItem> EnabledCostBillType { get; } = new();

    /// <summary>
    /// 全部省信息
    /// </summary>
    public static List<CListItem> AllProvince { get; } = new();

    /// <summary>
    /// 全部市信息
    /// </summary>
    public static List<CListItem> AllCity { get; } = new();

    /// <summary>
    /// 全部区信息
    /// </summary>
    public static List<CListItem> AllDistrict { get; } = new();

    /// <summary>
    /// 全部省市区信息
    /// </summary>
    public static List<CListItem> AllRegions { get; } = new();

    /// <summary>
    /// 全部字典类型
    /// </summary>
    public static List<DictTypeInfo> AllDictType { get; private set; } = new();

    /// <summary>
    /// 全部字典数据
    /// </summary>
    public static List<DictDataInfo> AllDictData { get; private set; } = new();

    /// <summary>
    /// 是否
    /// </summary>
    public static readonly List<CListItem> YesOrNo = new()
    {
        new CListItem("是", "1"),
        new CListItem("否", "0")
    };

    /// <summary>
    /// 正负
    /// </summary>
    public static readonly List<CListItem> Ctrl = new()
    {
        new CListItem("正", "1"),
        new CListItem("负", "-1")
    };

    #endregion
    
    #region 时间服务，心跳维持

    /// <summary>
    /// 上次设置的服务器时间
    /// </summary>
    private static DateTime _lastServerTime;

    /// <summary>
    /// 本地时间
    /// </summary>
    private static DateTime _lastLocalTime;

    /// <summary>
    /// 获取或设置当前时间(与服务器时间同步)，精确到毫秒。
    /// </summary>
    public static DateTime Now
    {
        get
        {
            if (_lastServerTime == DateTime.MinValue)
            {
                return DateTime.Now;
            }
            else
            {
                //服务器当前时间 = 上一次服务器返回的时间 + 返回之后的时间
                return _lastServerTime.AddMilliseconds(DateTime.Now.Subtract(_lastLocalTime).TotalMilliseconds);
            }
        }
        set
        {
            _lastLocalTime = DateTime.Now;
            _lastServerTime = value;
        }
    }

    /// <summary>
    /// 获得与服务器同步的当前时间，精确到分钟（秒和毫秒为0）。
    /// </summary>
    public static DateTime Now2
    {
        get
        {
            DateTime now = Now;
            return new DateTime(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, 0);
        }
    }

    #endregion

    #region 基本操作函数

    /// <summary>
    /// 转换框架通用的用户基础信息，方便框架使用
    /// </summary>
    /// <param name="info">权限系统定义的用户信息</param>
    /// <returns></returns>
    public static LoginUserInfo ConvertToLoginUser(UserInfo info)
    {
        var loginInfo = new LoginUserInfo
        {
            ID = info.ID,
            Name = info.Name,
            FullName = info.FullName,
            MobilePhone = info.MobilePhone,
            Email = info.Email,
            Gender = info.Gender,
            DeptId = info.DeptId,
            CompanyId = info.CompanyId
        };

        return loginInfo;
    }

    /// <summary>
    /// 看用户是否具有某个功能（管理员和功能ID为空都视为有权）
    /// </summary>
    /// <param name="controlId"></param>
    /// <returns></returns>
    public static bool HasFunction(string controlId)
    {
        return DataCanManage(LoginUserInfo.CompanyId) || (!string.IsNullOrEmpty(controlId) &&
            (!AllFunctionDict.ContainsKey(controlId) || FunctionDict.ContainsKey(controlId)));
    }

    /// <summary>
    /// 看用户是否具有某个功能（管理员和功能ID为空都视为有权）
    /// </summary>
    /// <param name="control"></param>
    /// <returns></returns>
    public static bool HasFunction(object control)
    {
        bool hsaFunction;
        switch (control)
        {
            case BarItem barItem:
            {
                Type form = barItem.Manager.Form.GetType();
                var fullName = $"{form.FullName ?? form.Name}/{barItem.Name}";
                hsaFunction = HasFunction(fullName);
                barItem.Visibility = hsaFunction ? BarItemVisibility.Always : BarItemVisibility.Never;
                break;
            }
            case BaseButton baseButton:
            {
                Type form = baseButton.Parent!.GetType();
                var fullName = $"{form.FullName ?? form.Name}/{baseButton.Name}";
                hsaFunction = HasFunction(fullName);
                baseButton.Visible = hsaFunction;
                baseButton.Enabled = hsaFunction;
                break;
            }
            case string text:
                hsaFunction = HasFunction(text);
                break;
            default:
                hsaFunction = false;
                break;
        }

        return  hsaFunction;
    }

    /// <summary>
    /// 检查某功能是否可显示
    /// </summary>
    /// <param name="controlId"></param>
    /// <returns></returns>
    public static BarItemVisibility IsVisibility(string controlId)
    {
        return HasFunction(controlId) ? BarItemVisibility.Always : BarItemVisibility.Never;
    }

    /// <summary>
    /// 替换用户关键字
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public static string ReplaceUserInfo(string str)
    {
        return str.Replace("{UserName}", LoginUserInfo.Name)
            .Replace("{UserID}", LoginUserInfo.ID.ToString())
            .Replace("{CompanyName}", LoginUserInfo.CompanyName)
            .Replace("{CompanyID}", LoginUserInfo.CompanyId);
    }

    /// <summary>
    /// 退出系统
    /// </summary>
    public static void Quit()
    {
        MainDialog?.Close();

        Cache.Instance.FlushAll();

        Application.ExitThread();
        Application.Exit();
    }

    /// <summary>
    /// 重新登陆(此方法目前有异常)
    /// </summary>
    public static void Refresh()
    {
        MainDialog?.Hide();
        
        Cache.Instance.FlushAll();

        Logon dlg = new ();
        dlg.StartPosition = FormStartPosition.CenterScreen;
        if (DialogResult.OK == dlg.ShowDialog())
        {
            if (dlg.BLogin)
            {
                MainDialog?.CloseAllDocuments();
                MainDialog?.InitUserRelated();
            }
        }
        dlg.Dispose();
        MainDialog?.Show();
    }

    /// <summary>
    /// 打开帮助文档
    /// </summary>
    public static void Help()
    {
        try
        {
            const string helpfile = "Help.chm";
            Process.Start(helpfile);
        }
        catch (Exception)
        {
            "文件打开失败".ShowUxWarning();
        }
    }

    /// <summary>
    /// 关于对话框信息
    /// </summary>
    public static void About()
    {
        AboutBox dlg = new AboutBox();
        dlg.StartPosition = FormStartPosition.CenterScreen;
        dlg.ShowDialog();
    }

    #endregion

    #region 注册相关的函数

    /// <summary>
    /// 如果用户没有注册，提示用户注册
    /// </summary>
    public static void ShowRegDlg()
    {
        RegDlg myRegdlg = RegDlg.Instance();
        myRegdlg.StartPosition = FormStartPosition.CenterScreen;
        myRegdlg.TopMost = true;
        myRegdlg.Hide();
        myRegdlg.Show();
        myRegdlg.BringToFront();
    }

    /// <summary>
    /// 每次程序运行时候,检查用户是否注册(如果第一次, 那么写入第一次运行时间)
    /// </summary>
    /// <returns>如果用户已经注册, 那么返回True, 否则为False</returns>
    public static bool CheckRegister()
    {
        // 先获取用户的注册码进行比较
        RegistryKey? reg = Registry.CurrentUser.OpenSubKey(UiConstants.SoftwareRegistryKey, true);
        if (null != reg)
        {
            var serialNumber = (string?)reg.GetValue("SerialNumber"); //注册码
            if (string.IsNullOrEmpty(serialNumber))
            {
                return false;
            }
            Registed = Register(serialNumber);
            return Registed;
        }

        return false;
    }

    /// <summary>
    /// 调用非对称加密方式对序列号进行验证
    /// </summary>
    /// <param name="serialNumber">正确的序列号</param>
    /// <returns>如果成功返回True，否则为False</returns>
    public static bool Register(String serialNumber)
    {
        string hardNumber = HardwareInfoUtil.GetCpuId();
        return RsaSecurityHelper.Validate(hardNumber, serialNumber);
    }

    public static string GetHardNumber()
    {
        return HardwareInfoUtil.GetCpuId();
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
        
    #endregion

    #region 机构和权限相关

    /// <summary>
    /// 根据机构分类获取对应的图形序号
    /// </summary>
    /// <param name="category">机构分类</param>
    /// <returns></returns>
    public static int GetImageIndex(string category)
    {
        int index = 0;
        if (category == OuCategoryEnum.公司.ToString())
        {
            index = 1;
        }
        else if (category == OuCategoryEnum.部门.ToString())
        {
            index = 2;
        }
        else if (category == OuCategoryEnum.工作组.ToString())
        {
            index = 3;
        }
        return index;
    }

    /// <summary>
    /// 判断当前用户具有某个角色
    /// </summary>
    /// <param name="roleName">角色名称</param>
    /// <returns></returns>
    public static bool UserInRole(string roleName)
    {
        bool result = false;
        foreach (RoleInfo info in RoleList)
        {
            if (info.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase))
            {
                result = true;
                break;
            }
        }
        return result;
    }

    /// <summary>
    /// 判断当前管理员是否有权限管理相关机构节点
    /// </summary>
    /// <param name="companyId">待管理的机构公司ID</param>
    /// <returns></returns>
    public static bool DataCanManage(object companyId)
    {
        bool result = false;
        if (UserInRole(RoleInfo.SUPER_ADMIN_NAME))
        {
            result = true;
        }
        else if (UserInRole(RoleInfo.COMPANY_ADMIN_NAME))
        {
            result = (LoginUserInfo.CompanyId == companyId.ToString());
        }
        return result;
    }

    /// <summary>
    /// 根据当前用户身份，获取对应的顶级机构管理节点。
    /// 如果是超级管理员，返回集团节点；如果是公司管理员，返回其公司节点
    /// </summary>
    /// <returns></returns>
    public static async Task<List<OUInfo>> GetMyTopGroup()
    {
        List<OUInfo> list = new List<OUInfo>();
        if (UserInRole(RoleInfo.SUPER_ADMIN_NAME))
        {
            //超级管理员取集团节点
            list.AddRange(await App.GetService<OUHttpService>().GetTopGroupAsync());
        }
        else
        {
            OUInfo groupInfo = await App.GetService<OUHttpService>().FindByIdAsync(LoginUserInfo.CompanyId);
            list.Add(groupInfo);
        }
        return list;
    }

    #endregion

    #region 取字典数据的方法

    /// <summary>
    /// 根据字典类型ID获取对应的CListItem集合
    /// </summary>
    /// <param name="dictTypeId"></param>
    /// <returns></returns>
    public static List<CListItem> GetDictByType(int? dictTypeId)
    {
        return dictTypeId == null
            ? new List<CListItem>()
            : AllDictData.Where(x => x.DictTypeId == dictTypeId)
                .Select(x => new CListItem(x.Name, x.Value))
                .ToList();
    }

    /// <summary>
    /// 根据字典名称获取对应的CListItem集合
    /// </summary>
    /// <param name="dictTypeName"></param>
    /// <returns></returns>
    public static List<CListItem> GetDictByName(string dictTypeName)
    {
        int? dictTypeId = AllDictType.FirstOrDefault(x => x.Name == dictTypeName)?.Code;
        return GetDictByType(dictTypeId);
    }
    
    /// <summary>
    /// 根据字典类型获取对应的CListItem集合
    /// </summary>
    /// <param name="dictTypeName"></param>
    /// <returns></returns>
    public static CListItem[] GetDictByDictType(string dictTypeName)
    {
        return GetDictByName(dictTypeName).ToArray();
    }

    /// <summary>
    /// 加载按钮功能权限
    /// </summary>
    /// <param name="dic"></param>
    /// <param name="menuNodeInfos"></param>
    public static void LoadFunction(this Dictionary<string, string> dic, List<MenuNodeInfo> menuNodeInfos)
    {
        menuNodeInfos.ForEach(x =>
        {
            if (x.MenuType == "3" && !x.FunctionId.IsNullOrEmpty())
            {
                dic.Add(x.FunctionId, x.Name);
            }

            if (x.Children.Count > 0)
            {
                dic.LoadFunction(x.Children);
            }
        });
    }

    #endregion

    #region 缓存

    /// <summary>
    /// 加载缓存
    /// </summary>
    /// <returns></returns>
    public static async Task LoadCache()
    {
        #region 用户权限

        var menuHttpService = App.GetService<MenuHttpService>();
        AllFunctionDict.Clear();
        AllFunctionDict.LoadFunction(await menuHttpService.GetTreeAsync(SystemType));
        UserMenuNode = await menuHttpService.GetMenuNodesByUser(LoginUserInfo.ID, SystemType);
        if (UserMenuNode is { Count: > 0 })
        {
            FunctionDict.Clear();
            FunctionDict.LoadFunction(UserMenuNode);
        }

        #endregion

        #region 获取角色对应的用户操作部门及公司范围
        List<string> companyLst = await App.GetService<RoleDataHttpService>().GetBelongCompanysByUserAsync(LoginUserInfo.ID);
        List<string> deptLst = await App.GetService<RoleDataHttpService>().GetBelongDeptsByUserAsync(LoginUserInfo.ID);
        var companysb = new StringBuilder();
        var deptsb = new StringBuilder();
        companysb.Append(" in (");
        foreach (string t in companyLst)
        {
            companysb.Append(" '" + t + "', ");
        }
        companysb.Append(" '')");

        if (companyLst.Contains("-1"))
        {
            companysb.Append(" or (1 = 1)");
        }

        deptsb.Append(" in (");
        for (var i = 0; i < deptLst.Count; i++)
        {
            deptsb.Append(" '" + deptsb[i] + "', ");
        }
        deptsb.Append(" '')");

        if (deptLst.Contains("-11"))
        {
            deptsb.Append(" or (1 = 1)");
        }
        #endregion

        #region 获取用户信息

        AllUserDict.Clear();
        EnabledUserDict.Clear();

        List<UserInfo> lst = await App.GetService<UserHttpService>().GetAllAsync();

        if (lst.Any())
        {
            lst.ForEach(x =>
            {
                var user = new CListItem(x.FullName, x.ID.ToString());
                if (!AllUserDict.Contains(user))
                    AllUserDict.Add(user);
                if (!x.Deleted && !x.IsExpire && !EnabledUserDict.Contains(user))
                    EnabledUserDict.Add(user);
            });
        }
            
        #endregion

        #region 获取部门信息

        AllOuDict.Clear();
        EnabledOuDict.Clear();

        List<OUInfo> ouList = await App.GetService<OUHttpService>().GetAllAsync();

        if (ouList.Any())
        {
            ouList.ForEach(x =>
            {
                var ou = new CListItem(x.Name, x.HandNo);
                if (!AllOuDict.Contains(ou))
                    AllOuDict.Add(ou);
                if (!x.Deleted && x.Enabled && !EnabledOuDict.Contains(ou))
                    EnabledOuDict.Add(ou);
            });
        }
            
        #endregion
        
        #region 获取费用类型

        AllCostType.Clear();
        EnabledCostType.Clear();

        List<BasicCostType> ctl = await App.GetService<BasicCostTypeHttpService>().GetAllAsync();

        if (ctl.Any())
        {
            ctl.ForEach(x =>
            {
                var costType = new CListItem(x.CostTypeDesc, x.CostType); 
                if (!AllCostType.Contains(costType))
                    AllCostType.Add(costType);
                if (x.FlagApp && x.UseYN && !EnabledCostType.Contains(costType))
                    EnabledCostType.Add(costType);
            });
        }

        AllCostBillType.Clear();
        EnabledCostBillType.Clear();

        List<BasicCostBillType> cbtl = await App.GetService<BasicCostBillTypeHttpService>().GetAllAsync();

        if (cbtl.Any())
        {
            cbtl.ForEach(x =>
            {
                var costBillType = new CListItem(x.CostDesc, x.CostType); 
                if (!AllCostBillType.Contains(costBillType))
                    AllCostBillType.Add(costBillType);
                if (x.FlagApp && !EnabledCostBillType.Contains(costBillType))
                    EnabledCostBillType.Add(costBillType);
            });
        }
        
        #endregion

        #region 加载字典数据

        AllDictType = await App.GetService<DictTypeHttpService>().GetAllAsync();
        AllDictData = await App.GetService<DictDataHttpService>().GetAllAsync();

        #endregion

        #region 处理和缓存行政区

        var allRegion = await App.GetService<RegionHttpService>().GetAllRegionAsync();

        if (allRegion.Any())
        {
            AllProvince.Clear();
            AllCity.Clear();
            AllDistrict.Clear();
            AllRegions.Clear();
            allRegion.ForEach(x =>
            {
                switch (x.Type)
                {
                    case 1:
                        AllProvince.Add(new CListItem(x.Name, x.Id.ToString()));
                        break;
                    case 2:
                        AllCity.Add(new CListItem(x.Name, x.Id.ToString()));
                        break;
                    case 3:
                        AllDistrict.Add(new CListItem(x.Name, x.Id.ToString()));
                        AllRegions.Add(new CListItem(x.FullName, x.Id.ToString()));
                        break;
                }
            });
        }
            
        #endregion

        #region 写缓存数据

        // // 并保持到缓存中
        // await Cache.Instance.SetAsync("LoginUserInfo", LoginUserInfo);//缓存用户信息，方便后续处理
        // // 权限
        // await Cache.Instance.SetAsync("FunctionDict", FunctionDict);//缓存权限信息，方便后续使用
        // 角色
        await Cache.Instance.SetAsync("RoleList", await App.GetService<UserRoleHttpService>().GetRolesByUserAsync(LoginUserInfo.ID));
        await Cache.Instance.SetStringAsync("canOptCompanyId", companysb.ToString());
        await Cache.Instance.SetStringAsync("canOptDeptId", deptsb.ToString());
        await Cache.Instance.SetAsync("AppConfig", Config);
        // 所有行政区
        await Cache.Instance.SetAsync("AllRegion", allRegion);
        // 所有用户
        await Cache.Instance.SetAsync("AllUserInfo", AllUserDict);
        // 所有公司和部门
        await Cache.Instance.SetAsync("AllOuInfo", AllOuDict);
        // 所有字典类型
        await Cache.Instance.SetAsync("AllDictType", AllDictType);
        // 所有字典数据
        await Cache.Instance.SetAsync("AllDictData", AllDictData);

        #endregion
    }

    #endregion
}