using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.UnifyResult;

namespace BB.HttpServices.Core.OU;

public interface IOUUserHttpService : IBaseHttpService<OUUserEntity>
{
    /// <summary>
    /// 通过用户机构ID方式获取对应的用户基本信息列表
    /// </summary>
    /// <param name="ouId">用户机构ID方式</param>
    /// <returns></returns>
    Task<RESTfulResult<List<SimpleUserInfo>>> GetSimpleUsersByOuAsync(string ouId);

    /// <summary>
    /// 通过机构ID获取对应的用户列表
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<UserInfo>>> GetUsersByOuAsync(string ouId);

    /// <summary>
    /// 在机构中移除指定的用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="ouId">机构ID</param>
    Task RemoveUserAsync(int userId, string ouId);
}