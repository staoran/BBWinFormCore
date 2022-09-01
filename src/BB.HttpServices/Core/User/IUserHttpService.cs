using BB.Entity.Security;
using BB.HttpServices.Base;
using BB.Tools.Entity;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.User;

public interface IUserHttpService : IHttpDispatchProxy, IBaseHttpService<UserInfo>
{
    /// <summary>
    /// 批量设置过期
    /// </summary>
    /// <param name="idList">ID集合</param>
    /// <param name="expired">是否过期</param>
    /// <returns></returns>
    [Post("batchExpire")]
    Task<RESTfulResultControl<bool>> BatchExpireAsync([Body]List<int> idList, [QueryString]bool expired);

    /// <summary>
    /// 设置用户的过期与否
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="expired">是否禁用，true为禁用，否则为启用</param>
    [Post("setExpire")]
    Task SetExpireAsync([QueryString]int userId, [QueryString]bool expired);

    /// <summary>
    /// 取消用户的过期设置，变为正常状态
    /// </summary>
    /// <param name="userId">用户ID</param>
    [Post("cancelExpire")]
    Task CancelExpireAsync([QueryString]int userId);

    /// <summary>
    /// 获取所有用户的基本信息
    /// </summary>
    /// <returns></returns>
    [Get("allSimpleUsers")]
    Task<RESTfulResultControl<List<SimpleUserInfo>>> GetSimpleUsersAsync();

    /// <summary>
    /// 获取指定ID的用户基本信息
    /// </summary>
    /// <param name="userIds">ID字符串,逗号分开</param>
    /// <returns></returns>
    [Get("simpleUsers")]
    Task<RESTfulResultControl<List<SimpleUserInfo>>> GetSimpleUsersAsync([QueryString]string userIds);
               
    /// <summary>
    /// 根据部门ID获取默认机构为该部门的相关人员
    /// </summary>
    /// <param name="ouId">部门ID</param>
    /// <returns></returns>
    [Get("byDept")]
    Task<RESTfulResultControl<List<UserInfo>>> FindByDeptAsync([QueryString]string ouId);

    /// <summary>
    /// 根据公司ID获取公司的相关人员
    /// </summary>
    /// <param name="companyId">公司门ID</param>
    /// <returns></returns>
    [Get("byCompany")]
    Task<RESTfulResultControl<List<UserInfo>>> FindByCompanyAsync([QueryString]string companyId);

    /// <summary>
    /// 根据公司ID获取公司的相关人员
    /// </summary>
    /// <param name="companyId">公司门ID</param>
    /// <returns></returns>
    [Get("simpleUsersByCompany")]
    Task<RESTfulResultControl<List<SimpleUserInfo>>> FindSimpleUsersByCompanyAsync([QueryString]string companyId);

    /// <summary>
    /// 根据部门ID获取默认机构为该部门的相关人员
    /// </summary>
    /// <param name="ouId">部门ID</param>
    /// <returns></returns>
    [Get("simpleUsersByDept")]
    Task<RESTfulResultControl<List<SimpleUserInfo>>> FindSimpleUsersByDeptAsync([QueryString]string ouId);

    /// <summary>
    /// 通过用户登录名称获取对应的用户信息
    /// </summary>
    /// <param name="userName">用户登录名称</param>
    /// <returns></returns>
    [Get("userByName")]
    Task<RESTfulResultControl<UserInfo>> GetUserByNameAsync([QueryString]string userName);

    /// <summary>
    /// 根据用户ID获取用户登录名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    [Get("nameById")]
    Task<RESTfulResultControl<string>> GetNameByIdAsync([QueryString]int userId);

    /// <summary>
    /// 根据用户ID获取用户登录名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    [Get("fullNameByOpenId")]
    Task<RESTfulResultControl<string>> GetFullNameByOpenIdAsync([QueryString]string userId);

    /// <summary>
    /// 根据用户ID获取用户全名称
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    [Get("fullNameById")]
    Task<RESTfulResultControl<string>> GetFullNameByIdAsync([QueryString]int userId);

    /// <summary>
    /// 根据用户登录名称，获取用户全名
    /// </summary>
    /// <param name="userName">用户登录名称</param>
    /// <returns></returns>
    [Get("fullNameByName")]
    Task<RESTfulResultControl<string>> GetFullNameByNameAsync([QueryString]string userName);

    /// <summary>
    /// 修改用户密码
    /// </summary>
    /// <param name="userName">修改用户名</param>
    /// <param name="userPassword">用户密码（未加密）</param>
    /// <returns></returns>
    [Post("modifyPassword")]
    Task<RESTfulResultControl<bool>> ModifyPasswordAsync([QueryString]string userName, [QueryString]string userPassword);

    /// <summary>
    /// 管理员重置密码
    /// </summary>
    /// <param name="changeUserId">修改账号ID</param>
    /// <returns></returns>
    [Post("resetPassword")]
    Task<RESTfulResultControl<bool>> ResetPasswordAsync([QueryString]int changeUserId);

    /// <summary>
    /// 更新用户登录的时间和IP地址
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="ip">IP地址</param>
    /// <param name="macAddr">MAC地址</param>
    /// <returns></returns>
    [Put("userLoginData")]
    Task<RESTfulResultControl<bool>> UpdateUserLoginDataAsync([QueryString]int id, [QueryString]string ip, [QueryString]string macAddr);

    /// <summary>
    /// 根据个人图片枚举类型获取图片数据
    /// </summary>
    /// <param name="imagetype">图片枚举类型</param>
    /// <returns></returns>
    [Get("personImageBytes")]
    Task<RESTfulResultControl<byte[]>> GetPersonImageBytesAsync([QueryString]UserImageType imagetype, [QueryString]int userId);

    /// <summary>
    /// 更新个人相关图片数据
    /// </summary>
    /// <param name="imagetype">图片类型</param>
    /// <param name="userId">用户ID</param>
    /// <param name="imageBytes">图片字节数组</param>
    /// <returns></returns>
    [Put("personImageBytes")]
    Task<RESTfulResultControl<bool>> UpdatePersonImageBytesAsync([QueryString]UserImageType imagetype, [QueryString]int userId, [QueryString]byte[] imageBytes);

    /// <summary>
    /// 设置删除标志
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="deleted">是否删除</param>
    /// <returns></returns>
    [Post("setDeletedFlag")]
    Task<RESTfulResultControl<bool>> SetDeletedFlagAsync([QueryString]object id, [QueryString]bool deleted = true);

    /// <summary>
    /// 绑定用户，第一次或重复绑定同一个，提示成功，否则提示失败
    /// </summary>
    /// <param name="openid">用户的OpenID</param>
    /// <param name="unionId"></param>
    /// <param name="id">用户ID</param>
    /// <returns></returns>
    [Post("bindUser")]
    Task BindUserAsync([QueryString]string openid, [QueryString]string unionId, [QueryString]int id);

    /// <summary>
    /// 根据OpenID获取对应的用户信息
    /// </summary>
    /// <param name="openid">微信OpenID</param>
    /// <returns></returns>
    [Get("byOpenId")]
    Task<RESTfulResultControl<UserInfo>> FindByOpenIdAsync([QueryString]string openid);

    /// <summary>
    /// 使用唯一的UnionID来获取用户
    /// </summary>
    /// <param name="unionId">开放平台下唯一的UnionID</param>
    /// <returns></returns>
    [Get("byUnionId")]
    Task<RESTfulResultControl<UserInfo>> FindByUnionIdAsync([QueryString]string unionId);

    /// <summary>
    /// 根据微信企业微信的UserID获取对应的用户信息
    /// </summary>
    /// <param name="openid">微信企业微信的UserID</param>
    /// <returns></returns>
    [Get("byCorpUserId")]
    Task<RESTfulResultControl<UserInfo>> FindByCorpUserIdAsync([QueryString]string openid);

    /// <summary>
    /// 判断用户是否绑定了OpenID
    /// </summary>
    /// <param name="openid">微信OpenID</param>
    /// <returns></returns>
    [Post("isExistOpenId")]
    Task<RESTfulResultControl<bool>> IsExistOpenIdAsync([QueryString]string openid);

    /// <summary>
    /// 清空绑定的用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns></returns>
    [Post("cancelBindWechat")]
    Task<RESTfulResultControl<bool>> CancelBindWechatAsync([QueryString]int id);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}user/";
        // req.BaseAddress = builder.Uri;
    }
}