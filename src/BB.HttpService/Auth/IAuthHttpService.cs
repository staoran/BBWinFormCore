using BB.Entity.Base;
using BB.Entity.Security;
using BB.HttpService.Auth.dto;
using Furion.RemoteRequest;
using Furion.UnifyResult;
using BB.HttpService.Base;

namespace BB.HttpService.Auth;

/// <summary>
/// 用户认证服务
/// </summary>
public interface IAuthHttpService : IBaseHttpService<UserInfo>
{

    /// <summary>
    /// 根据用户名、密码验证用户身份有效性
    /// </summary>
    /// <param name="input">用户登录输入参数</param>
    /// <returns></returns>
    [Post("verifyUser")]
    Task<RESTfulResult<LoginUserInfo>> VerifyUserAsync([Body] LoginInput input);
}