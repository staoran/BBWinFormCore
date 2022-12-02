using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Core.Services.Auth.dto;
using BB.Core.Services.BlackIP;
using BB.Core.Services.User;
using BB.Entity.Base;
using BB.Entity.Security;
using BB.Tools.Encrypt;
using Furion.ClayObject.Extensions;
using Furion.EventBus;
using Furion.UnifyResult;

namespace BB.Core.Services.Auth;

/// <summary>
/// 用户认证服务
/// </summary>
[ApiDescriptionSettings(Order = 0)]
public class AuthService : IDynamicApiController, ITransient
{
    private readonly UserService _userService;
    private readonly BlackIPService _blackIPService;
    private readonly IEventPublisher _eventPublisher;

    /// <summary>
    /// 当前请求上下文
    /// </summary>
    private readonly HttpContext _httpContext;

    public AuthService(UserService userService, BlackIPService blackIPService, IEventPublisher eventPublisher, 
        IHttpContextAccessor httpContextAccessor)
    {
        _userService = userService;
        _blackIPService = blackIPService;
        _eventPublisher = eventPublisher;
        _httpContext = httpContextAccessor.HttpContext;
    }

    /// <summary>
    /// 根据用户名、密码验证用户身份有效性
    /// </summary>
    /// <param name="input">用户登录输入参数</param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<LoginUserInfo> VerifyUserAsync(LoginInput input)
    {
        if (string.IsNullOrEmpty(input.SystemType))
        {
            return null;
        }
        LoginUserInfo loginUserInfo = null;
        UserInfo userInfo = await _userService.GetUserByNameAsync(input.UserName);
        if (userInfo is { IsExpire: false, Deleted: false })
        {
            //还需要判断是否在有效期内
            DateTime? expireDate = userInfo.ExpireDate;
            if (expireDate.HasValue && expireDate.Value < DateTime.Now)
            {
                //处理非管理员外的失效设置
                if (userInfo.Name != "admin")
                {
                    var ht = new Hashtable 
                    {
                        { UserInfo.PrimaryKey, userInfo.ID },
                        { UserInfo.FieldIsExpire, 1 } //设置失效
                    };
                    await _userService.UpdateFieldsAsync(ht);//更新过期设置
                    throw Oops.Bah("用户已经过期");
                }
            }
            else
            {
                loginUserInfo = userInfo.Adapt<LoginUserInfo>();
                string ip = _httpContext.GetRemoteIpAddressToIPv4();
                bool ipAccess = await _blackIPService.ValidateIpAccessAsync(ip, userInfo.ID);
                if (ipAccess)
                {
                    //如果为null，那么密码为空字符串
                    string userPassword = EncryptHelper.ComputeHash(input.UserPassword ?? "", input.UserName.ToLower());
                    if (userPassword == userInfo.Password)
                    {
                        // identity = EncryptHelper.EncryptStr(userName + Convert.ToString(Convert.ToChar(1)) + userPassword, systemType);
                        
                        IDictionary<string,object> payload = DictionaryExtensions.ToDictionary(loginUserInfo);
                        
                        // 生成 Token
                        var token = JWTEncryption.Encrypt(payload);

                        // 设置 Swagger 的 Token
                        _httpContext.SigninToSwagger(token);
        
                        // 获取 refreshToken, 默认过期时间43200（30天）
                        string refreshToken = JWTEncryption.GenerateRefreshToken(token);
        
                        // 设置响应报文头
                        _httpContext.Response.Headers["access-token"] = token;
                        _httpContext.Response.Headers["x-access-token"] = refreshToken;
        
                        // 设置附加信息
                        UnifyContext.Fill("登陆成功");

                        //记录用户登录日志
                        await _eventPublisher.PublishAsync("Add:LoginLog", new 
                        {
                            Info = loginUserInfo,
                            SystemType = "Security",
                            IP = ip,
                            Note = "用户登录"
                        });
                    }
                }
                else
                {
                    await _eventPublisher.PublishAsync("Add:LoginLog", new 
                    {
                        Info = loginUserInfo,
                        SystemType = "Security",
                        IP = ip,
                        Note = "用户登录操作被黑白名单禁止登录！"
                    });
                    throw Oops.Bah("用户登录操作被黑名单禁止登录！");
                }
            }
        }
        return loginUserInfo;
    }
}