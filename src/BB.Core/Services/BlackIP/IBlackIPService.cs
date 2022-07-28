using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Core.Services.Base;
using BB.Entity.Security;

namespace BB.Core.Services.BlackIP;

public interface IBlackIPService : IBaseService<BlackIpInfo>
{
    /// <summary>
    /// 根据黑名单ID获取对应的用户ID列表
    /// </summary>
    /// <param name="id">黑名单ID</param>
    /// <returns></returns>
    Task<string> GetUserIdListAsync(string id);

    /// <summary>
    /// 根据名单ID获取对应的用户列表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<List<SimpleUserInfo>> GetSimpleUserByBlackIpAsync(string id);

    Task AddUserAsync(int userId, string blackId);
    Task RemoveUserAsync(int userId, string blackId);

    /// <summary>
    /// 根据用户ID和授权类型获取列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="type">授权类型</param>
    /// <returns></returns>
    Task<List<BlackIpInfo>> FindByUserAsync(int userId, AuthrizeType type);

    /// <summary>
    /// 检验IP的可访问性(白名单优先于黑名单），如果同时白名单、黑名名单都有同一IP，则也允许访问。
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    Task<bool> ValidateIpAccessAsync(string ipAddress, int userId);

    bool IsInList(List<BlackIpInfo> list, string ip);
}