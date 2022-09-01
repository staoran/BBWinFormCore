using System.ComponentModel.DataAnnotations;

namespace BB.Core.Services.Auth.dto;

/// <summary>
/// 用户登录输入参数
/// </summary>
public class LoginInput
{
    /// <summary>用户名称</summary>
    /// <example>admin</example>
    [Required]
    public string UserName { get; set; }

    /// <summary>用户密码</summary>
    /// <example></example>
    public string UserPassword { get; set; }

    /// <summary>系统类型ID</summary>
    /// <example>WareMis</example>
    public string SystemType { get; set; }
}