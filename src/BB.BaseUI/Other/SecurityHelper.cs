using BB.BaseUI.Extension;
using BB.Entity.Base;
using BB.Entity.Security;
using BB.HttpService.Auth;
using BB.HttpService.Auth.dto;
using BB.HttpService.OU;
using BB.HttpService.Role;
using BB.HttpService.User;
using BB.Tools.Extension;
using BB.Tools.Utils;
using Furion;

namespace BB.BaseUI.Other;

/// <summary>
/// 增加一个辅助类，操作和权限系统相关的资源，以便使得权限和工作流相对独立使用。
/// </summary>
public static class SecurityHelper
{
    /// <summary>
    /// 通用登陆逻辑
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="userPassword">密码</param>
    /// <param name="systemType">系统类型</param>
    /// <param name="checkAdmin">验证管理员权限</param>
    /// <returns></returns>
    public static async Task<bool> Login(string userName, string userPassword, string systemType, bool checkAdmin = false)
    {
        try
        {
            LoginUserInfo loginUser = await App.GetService<AuthHttpService>()
                .VerifyUserAsync(new LoginInput(userName, userPassword, systemType));
            if (loginUser != null)
            {
                var userRoleHttpService = App.GetService<UserRoleHttpService>();
                if(checkAdmin && !await userRoleHttpService.UserIsAdminAsync(loginUser.ID))
                {
                    "该用户没有管理员权限".ShowUxWarning();
                    return false;
                }
                GB.LoginUserInfo = loginUser;
                // GB.SetSessionData(); // 验证成功后直接在服务端设置session数据
                GB.TimedTask(); // 保持心跳，CoreRemoting 模式不开启
                GB.RoleList = await userRoleHttpService.GetRolesByUserAsync(loginUser.ID);//用户的角色集合

                #region 保存用户登录信息

                RegistryHelper.SaveValue(GB.LoginNameKey, userName);

                #endregion
            }
            else
            {
                "用户帐号密码不正确".ShowUxTips();
                return false;
                // tbPass.Text = ""; //设置密码为空
            }
        }
        catch (Exception err)
        {
            err.Message.ShowUxError();
            return false;
        }

        return true;
    }
    
    private static bool InUserList(List<UserInfo> list, UserInfo userInfo)
    {
        bool result = false;
        foreach (UserInfo info in list)
        {
            if (info.ID == userInfo.ID)
            {
                result = true;
                break;
            }
        }
        return result;
    }

    /// <summary>
    /// 根据用户的登陆名称，获取用户的全名，并放到缓存里面
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public static async Task<string> GetFullNameByName(string name)
    {
        string result = "";
        if (!string.IsNullOrEmpty(name))
        {
            System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
            string key = $"{method.DeclaringType.FullName}-{method.Name}-{name}";

            result = await Cache.Instance.GetOrCreateAsync(key,
                async () => await App.GetService<UserHttpService>().GetFullNameByNameAsync(name),
                new TimeSpan(0, 30, 0)) ?? string.Empty;//30分钟过期
        }
        return result;
    }

    /// <summary>
    /// 根据用户的ID，获取用户的登陆名称，并放到缓存里面
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static async Task<string> GetNameById(int userId)
    {
        System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
        string key = $"{method.DeclaringType.FullName}-{method.Name}-{userId}";

        return await Cache.Instance.GetOrCreateAsync(key,
            async () => await App.GetService<UserHttpService>().GetNameByIdAsync(userId),
            new TimeSpan(0, 30, 0)) ?? string.Empty; //30分钟过期
    }

    /// <summary>
    /// 根据用户的ID，获取用户的全名，并放到缓存里面
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static async Task<string> GetFullNameById(int userId)
    {
        System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
        string key = $"{method.DeclaringType.FullName}-{method.Name}-{userId}";

        return await Cache.Instance.GetOrCreateAsync(key,
            async () => await App.GetService<UserHttpService>().GetFullNameByIdAsync(userId),
            new TimeSpan(0, 30, 0)) ?? string.Empty;//30分钟过期
    }

    /// <summary>
    /// 获取用户全部简单对象信息，并放到缓存里面
    /// </summary>
    /// <returns></returns>
    public static async Task<List<SimpleUserInfo>> GetSimpleUsers()
    {
        System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
        string key = $"{method.DeclaringType.FullName}-{method.Name}";

        return await Cache.Instance.GetOrCreateAsync(key,
            async () => await App.GetService<UserHttpService>().GetSimpleUsersAsync(),
            new TimeSpan(0, 10, 0));//10分钟过期
    }

    /// <summary>
    /// 获取用户角色集合
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public static async Task<List<RoleInfo>> GetRoleList(int userId)
    {
        System.Reflection.MethodBase method = System.Reflection.MethodBase.GetCurrentMethod();
        string key = $"{method.DeclaringType.FullName}-{method.Name}-{userId}";

        return await Cache.Instance.GetOrCreateAsync(key,
            async () => await App.GetService<UserRoleHttpService>().GetRolesByUserAsync(userId),
            new TimeSpan(0, 30, 0));//30分钟过期
    }

    /// <summary>
    /// 根据当前用户身份，获取对应的顶级机构管理节点。
    /// 如果是超级管理员，返回集团节点；如果是公司管理员，返回其公司节点
    /// </summary>
    /// <returns></returns>
    public static async Task<List<OUInfo>> GetMyTopGroup(LoginUserInfo userInfo)
    {
        List<OUInfo> list = new();
        string key = "Security_MyTopGroup" + userInfo.ID;
        return await Cache.Instance.GetOrCreateAsync(key,
            async delegate
            {
                var ouHttpService = App.GetService<OUHttpService>();
                if (await UserInRole(RoleInfo.SUPER_ADMIN_NAME, userInfo.ID))
                {
                    list.AddRange(await ouHttpService.GetTopGroupAsync());//超级管理员取集团节点
                }
                else
                {
                    OUInfo groupInfo = await ouHttpService.FindByIdAsync(userInfo.CompanyId);//公司管理员取公司节点
                    list.Add(groupInfo);
                }

                return list;
            },
            new TimeSpan(0, 30, 0));//30分钟过期
    }

    /// <summary>
    /// 判断当前用户具有某个角色
    /// </summary>
    /// <param name="roleName">角色名称</param>
    /// <param name="userId">s</param>
    /// <returns></returns>
    public static async Task<bool> UserInRole(string roleName, int userId)
    {
        List<RoleInfo> roleList = await GetRoleList(userId);
        bool result = false;
        if (roleList != null)
        {
            foreach (RoleInfo info in roleList)
            {
                if (info.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase))
                {
                    result = true;
                    break;
                }
            }
        }
        return result;
    }
}