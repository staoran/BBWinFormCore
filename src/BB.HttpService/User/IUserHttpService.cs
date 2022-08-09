using BB.Entity.Security;
using BB.HttpService.Base;
using BB.Tools.Entity;
using Furion.UnifyResult;

namespace BB.HttpService.User;

public interface IUserHttpService : IBaseHttpService<UserInfo>
{
    /// <summary>
    /// 批量设置过期
    /// </summary>
    /// <param name="idList">ID集合</param>
    /// <param name="expired">是否过期</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> BatchExpireAsync(List<int> idList, bool expired);

    /// <summary>
    /// 设置用户的过期与否
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="expired">是否禁用，true为禁用，否则为启用</param>
    Task SetExpireAsync(int userId, bool expired);

    /// <summary>
    /// 取消用户的过期设置，变为正常状态
    /// </summary>
    /// <param name="userId">用户ID</param>
    Task CancelExpireAsync(int userId);

    /// <summary>
    /// 获取所有用户的基本信息
    /// </summary>
    /// <returns></returns>
    Task<RESTfulResult<List<SimpleUserInfo>>> GetSimpleUsersAsync();

    /// <summary>
    /// 获取指定ID的用户基本信息
    /// </summary>
    /// <param name="userIds">ID字符串,逗号分开</param>
    /// <returns></returns>
    Task<RESTfulResult<List<SimpleUserInfo>>> GetSimpleUsersAsync(string userIds);
               
    /// <summary>
    /// 根据部门ID获取默认机构为该部门的相关人员
    /// </summary>
    /// <param name="ouId">部门ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<UserInfo>>> FindByDeptAsync(string ouId);

    /// <summary>
    /// 根据公司ID获取公司的相关人员
    /// </summary>
    /// <param name="companyId">公司门ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<UserInfo>>> FindByCompanyAsync(string companyId);

    /// <summary>
    /// 根据公司ID获取公司的相关人员
    /// </summary>
    /// <param name="companyId">公司门ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<SimpleUserInfo>>> FindSimpleUsersByCompanyAsync(string companyId);

    /// <summary>
    /// 根据部门ID获取默认机构为该部门的相关人员
    /// </summary>
    /// <param name="ouId">部门ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<SimpleUserInfo>>> FindSimpleUsersByDeptAsync(string ouId);

    /// <summary>
    /// 通过用户登录名称获取对应的用户信息
    /// </summary>
    /// <param name="userName">用户登录名称</param>
    /// <returns></returns>
    Task<RESTfulResult<UserInfo>> GetUserByNameAsync(string userName);

    /// <summary>
    /// 根据用户ID获取用户登录名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<RESTfulResult<string>> GetNameByIdAsync(int userId);

    /// <summary>
    /// 根据用户ID获取用户登录名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<RESTfulResult<string>> GetFullNameByOpenIdAsync(string userId);

    /// <summary>
    /// 根据用户ID获取用户全名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    Task<RESTfulResult<string>> GetFullNameByIdAsync(int userId);

    /// <summary>
    /// 根据用户登录名称，获取用户全名
    /// </summary>
    /// <param name="userName">用户登录名称</param>
    /// <returns></returns>
    Task<RESTfulResult<string>> GetFullNameByNameAsync(string userName);

    /// <summary>
    /// 修改用户密码
    /// </summary>
    /// <param name="userName">修改用户名</param>
    /// <param name="userPassword">用户密码（未加密）</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> ModifyPasswordAsync(string userName, string userPassword);

    /// <summary>
    /// 管理员重置密码
    /// </summary>
    /// <param name="changeUserId">修改账号ID</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> ResetPasswordAsync(int changeUserId);

    /// <summary>
    /// 更新用户登录的时间和IP地址
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="ip">IP地址</param>
    /// <param name="macAddr">MAC地址</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> UpdateUserLoginDataAsync(int id, string ip, string macAddr);

    /// <summary>
    /// 根据个人图片枚举类型获取图片数据
    /// </summary>
    /// <param name="imagetype">图片枚举类型</param>
    /// <returns></returns>
    Task<RESTfulResult<byte[]>> GetPersonImageBytesAsync(UserImageType imagetype, int userId);

    /// <summary>
    /// 更新个人相关图片数据
    /// </summary>
    /// <param name="imagetype">图片类型</param>
    /// <param name="userId">用户ID</param>
    /// <param name="imageBytes">图片字节数组</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> UpdatePersonImageBytesAsync(UserImageType imagetype, int userId, byte[] imageBytes);

    /// <summary>
    /// 设置删除标志
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="deleted">是否删除</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> SetDeletedFlagAsync(object id, bool deleted = true);

    /// <summary>
    /// 绑定用户，第一次或重复绑定同一个，提示成功，否则提示失败
    /// </summary>
    /// <param name="openid">用户的OpenID</param>
    /// <param name="unionId"></param>
    /// <param name="id">用户ID</param>
    /// <returns></returns>
    Task BindUserAsync(string openid, string unionId, int id);

    /// <summary>
    /// 根据OpenID获取对应的用户信息
    /// </summary>
    /// <param name="openid">微信OpenID</param>
    /// <returns></returns>
    Task<RESTfulResult<UserInfo>> FindByOpenIdAsync(string openid);

    /// <summary>
    /// 使用唯一的UnionID来获取用户
    /// </summary>
    /// <param name="unionId">开放平台下唯一的UnionID</param>
    /// <returns></returns>
    Task<RESTfulResult<UserInfo>> FindByUnionIdAsync(string unionId);

    /// <summary>
    /// 根据微信企业微信的UserID获取对应的用户信息
    /// </summary>
    /// <param name="openid">微信企业微信的UserID</param>
    /// <returns></returns>
    Task<RESTfulResult<UserInfo>> FindByCorpUserIdAsync(string openid);

    /// <summary>
    /// 判断用户是否绑定了OpenID
    /// </summary>
    /// <param name="openid">微信OpenID</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> IsExistOpenIdAsync(string openid);

    /// <summary>
    /// 清空绑定的用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> CancelBindWechatAsync(int id);
}