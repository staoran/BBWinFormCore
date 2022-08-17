using BB.Entity.Security;
using BB.HttpServices.Base;
using BB.Tools.Entity;

namespace BB.HttpServices.Core.User;

public class UserHttpService : BaseHttpService<UserInfo>
{
    private readonly IUserHttpService _userHttpService;

    public UserHttpService(IUserHttpService userHttpService) : base(userHttpService)
    {
        _userHttpService = userHttpService;
    }

    /// <summary>
    /// 批量设置过期
    /// </summary>
    /// <param name="idList">ID集合</param>
    /// <param name="expired">是否过期</param>
    /// <returns></returns>
    public async Task<bool> BatchExpireAsync(List<int> idList, bool expired)
    {
        return (await _userHttpService.BatchExpireAsync(idList, expired)).Data;
    }

    /// <summary>
    /// 设置用户的过期与否
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="expired">是否禁用，true为禁用，否则为启用</param>
    public async Task SetExpireAsync(int userId, bool expired)
    {
        await _userHttpService.SetExpireAsync(userId, expired);
    }

    /// <summary>
    /// 取消用户的过期设置，变为正常状态
    /// </summary>
    /// <param name="userId">用户ID</param>
    public async Task CancelExpireAsync(int userId)
    {
        await _userHttpService.CancelExpireAsync(userId);
    }

    /// <summary>
    /// 获取所有用户的基本信息
    /// </summary>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> GetSimpleUsersAsync()
    {
        return (await _userHttpService.GetSimpleUsersAsync()).Data;
    }

    /// <summary>
    /// 获取指定ID字符串的用户基本信息
    /// </summary>
    /// <param name="userIds">ID字符串,逗号分开</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> GetSimpleUsersAsync(string userIds)
    {
        return (await _userHttpService.GetSimpleUsersAsync(userIds)).Data;
    }

    /// <summary>
    /// 根据部门ID获取默认机构为该部门的相关人员
    /// </summary>
    /// <param name="ouId">部门ID</param>
    /// <returns></returns>
    public async Task<List<UserInfo>> FindByDeptAsync(string ouId)
    {
        return (await _userHttpService.FindByDeptAsync(ouId)).Data;
    }

    /// <summary>
    /// 根据公司ID获取公司的相关人员
    /// </summary>
    /// <param name="companyId">公司门ID</param>
    /// <returns></returns>
    public async Task<List<UserInfo>> FindByCompanyAsync(string companyId)
    {
        return (await _userHttpService.FindByCompanyAsync(companyId)).Data;
    }

    /// <summary>
    /// 根据公司ID获取公司的相关人员
    /// </summary>
    /// <param name="companyId">公司门ID</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> FindSimpleUsersByCompanyAsync(string companyId)
    {
        return (await _userHttpService.FindSimpleUsersByCompanyAsync(companyId)).Data;
    }

    /// <summary>
    /// 根据部门ID获取默认机构为该部门的相关人员
    /// </summary>
    /// <param name="ouId">部门ID</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> FindSimpleUsersByDeptAsync(string ouId)
    {
        return (await _userHttpService.FindSimpleUsersByDeptAsync(ouId)).Data;
    }

    /// <summary>
    /// 通过用户登录名称获取对应的用户信息
    /// </summary>
    /// <param name="userName">用户登录名称</param>
    /// <returns></returns>
    public async Task<UserInfo> GetUserByNameAsync(string userName)
    {
        return (await _userHttpService.GetUserByNameAsync(userName)).Data;
    }

    /// <summary>
    /// 根据用户ID获取用户登录名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<string> GetNameByIdAsync(int userId)
    {
        return (await _userHttpService.GetNameByIdAsync(userId)).Data;
    }


    /// <summary>
    /// 根据用户ID获取用户登录名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<string> GetFullNameByOpenIdAsync(string userId)
    {
        return (await _userHttpService.GetFullNameByOpenIdAsync(userId)).Data;
    }

    /// <summary>
    /// 根据用户ID获取用户全名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<string> GetFullNameByIdAsync(int userId)
    {
        return (await _userHttpService.GetFullNameByIdAsync(userId)).Data;
    }

    /// <summary>
    /// 根据用户登录名称，获取用户全名
    /// </summary>
    /// <param name="userName">用户登录名称</param>
    /// <returns></returns>
    public async Task<string> GetFullNameByNameAsync(string userName)
    {
        return (await _userHttpService.GetFullNameByNameAsync(userName)).Data;
    }

    /// <summary>
    /// 修改用户密码
    /// </summary>
    /// <param name="userName">修改用户名</param>
    /// <param name="userPassword">用户密码（未加密）</param>
    /// <returns></returns>
    public async Task<bool> ModifyPasswordAsync(string userName, string userPassword)
    {
        return (await _userHttpService.ModifyPasswordAsync(userName, userPassword)).Data;
    }

    /// <summary>
    /// 管理员重置密码
    /// </summary>
    /// <param name="changeUserId">修改账号ID</param>
    /// <returns></returns>
    public async Task<bool> ResetPasswordAsync(int changeUserId)
    {
        return (await _userHttpService.ResetPasswordAsync(changeUserId)).Data;
    }

    /// <summary>
    /// 更新用户登录的时间和IP地址
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="ip">IP地址</param>
    /// <param name="macAddr">MAC地址</param>
    /// <returns></returns>
    public async Task<bool> UpdateUserLoginDataAsync(int id, string ip, string macAddr)
    {
        return (await _userHttpService.UpdateUserLoginDataAsync(id, ip, macAddr)).Data;
    }

    /// <summary>
    /// 根据个人图片枚举类型获取图片数据
    /// </summary>
    /// <param name="imagetype">图片枚举类型</param>
    /// <returns></returns>
    public async Task<byte[]> GetPersonImageBytesAsync(UserImageType imagetype, int userId)
    {
        return (await _userHttpService.GetPersonImageBytesAsync(imagetype, userId)).Data;
    }

    /// <summary>
    /// 更新个人相关图片数据
    /// </summary>
    /// <param name="imagetype">图片类型</param>
    /// <param name="userId">用户ID</param>
    /// <param name="imageBytes">图片字节数组</param>
    /// <returns></returns>
    public async Task<bool> UpdatePersonImageBytesAsync(UserImageType imagetype, int userId, byte[] imageBytes)
    {
        return (await _userHttpService.UpdatePersonImageBytesAsync(imagetype, userId, imageBytes)).Data;
    }

    /// <summary>
    /// 设置删除标志
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="deleted">是否删除</param>
    /// <returns></returns>
    public async Task<bool> SetDeletedFlagAsync(object id, bool deleted = true)
    {
        return (await _userHttpService.SetDeletedFlagAsync(id, deleted)).Data;
    }

    /// <summary>
    /// 绑定用户，第一次或重复绑定同一个，提示成功，否则提示失败
    /// </summary>
    /// <param name="openid">用户的OpenID</param>
    /// <param name="unionId"></param>
    /// <param name="id">用户ID</param>
    /// <returns></returns>
    public async Task BindUserAsync(string openid, string unionId, int id)
    {
        await _userHttpService.BindUserAsync(openid, unionId, id);
    }

    /// <summary>
    /// 根据OpenID获取对应的用户信息
    /// </summary>
    /// <param name="openid">微信OpenID</param>
    /// <returns></returns>
    public async Task<UserInfo> FindByOpenIdAsync(string openid)
    {
        return (await _userHttpService.FindByOpenIdAsync(openid)).Data;
    }

    /// <summary>
    /// 使用唯一的UnionID来获取用户
    /// </summary>
    /// <param name="unionId">开放平台下唯一的UnionID</param>
    /// <returns></returns>
    public async Task<UserInfo> FindByUnionIdAsync(string unionId)
    {
        return (await _userHttpService.FindByUnionIdAsync(unionId)).Data;
    }

    /// <summary>
    /// 根据微信企业微信的UserID获取对应的用户信息
    /// </summary>
    /// <param name="openid">微信企业微信的UserID</param>
    /// <returns></returns>
    public async Task<UserInfo> FindByCorpUserIdAsync(string openid)
    {
        return (await _userHttpService.FindByCorpUserIdAsync(openid)).Data;
    }

    /// <summary>
    /// 判断用户是否绑定了OpenID
    /// </summary>
    /// <param name="openid">微信OpenID</param>
    /// <returns></returns>
    public async Task<bool> IsExistOpenIdAsync(string openid)
    {
        return (await _userHttpService.IsExistOpenIdAsync(openid)).Data;
    }

    /// <summary>
    /// 清空绑定的用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns></returns>
    public async Task<bool> CancelBindWechatAsync(int id)
    {
        return (await _userHttpService.CancelBindWechatAsync(id)).Data;
    }
}