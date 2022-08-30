using BB.Entity.Base;
using BB.Entity.Security;
using BB.HttpServices.Base;
using BB.HttpServices.Core.Auth.dto;

namespace BB.HttpServices.Core.Auth;

public class AuthHttpService : BaseHttpService<UserInfo>
{
    private readonly IAuthHttpService _authHttpService;

    public AuthHttpService(IAuthHttpService authHttpService) : base(authHttpService)
    {
        _authHttpService = authHttpService;
    }

    /// <summary>
    /// 根据用户名、密码验证用户身份有效性
    /// </summary>
    /// <param name="input">用户登录输入参数</param>
    /// <returns></returns>
    public async Task<LoginUserInfo> VerifyUserAsync(LoginInput input)
    {
        return (await _authHttpService.VerifyUserAsync(input)).Handling();
    }
}