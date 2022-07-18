namespace BB.Entity.Security;

/// <summary>
/// 机构分类
/// </summary>
public enum OuCategoryEnum
{
    集团, 公司, 中心, 部门, 大区办事处, 工作组
}

/// <summary>
/// 角色分类
/// </summary>
public enum RoleCategoryEnum
{
    系统角色 = 0, 业务角色=1, 应用角色=3
}

/// <summary>
/// 黑白名单的授权方式
/// </summary>
public enum AuthrizeType
{
    黑名单 = 0, 白名单 = 1
}

/// <summary>
/// 用户登录的状态
/// </summary>
public enum LoginStatus { NotExist, NotMatch, Forbidden, Pass };