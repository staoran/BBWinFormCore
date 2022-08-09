using System.ComponentModel.DataAnnotations;

namespace BB.HttpService.Auth.dto;

/// <summary>
/// 用户登录输入参数
/// </summary>
/// <param name="UserName">用户名称</param>
/// <param name="UserPassword">用户密码</param>
/// <param name="SystemType">系统类型ID</param>
public record LoginInput(
    [Required]string UserName, 
    string UserPassword, 
    string SystemType);