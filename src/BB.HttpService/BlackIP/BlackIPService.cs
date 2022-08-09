using BB.Entity.Security;
using BB.HttpService.Base;

namespace BB.HttpService.BlackIP;

public class BlackIPHttpService : BaseHttpService<BlackIpInfo>
{
    private readonly IBlackIPHttpService _blackIPHttpService;

    public BlackIPHttpService(IBlackIPHttpService blackIPHttpService) : base(blackIPHttpService)
    {
        _blackIPHttpService = blackIPHttpService;
    }

    /// <summary>
    /// 根据黑名单ID获取对应的用户ID列表
    /// </summary>
    /// <param name="id">黑名单ID</param>
    /// <returns></returns>
    public async Task<string> GetUserIdListAsync(string id)
    {
        return (await _blackIPHttpService.GetUserIdListAsync(id)).Data;
    }

    /// <summary>
    /// 根据名单ID获取对应的用户列表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> GetSimpleUserByBlackIpAsync(string id)
    {
        return (await _blackIPHttpService.GetSimpleUserByBlackIpAsync(id)).Data;
    }

    /// <summary>
    /// 新增黑用户
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="blackId"></param>
    public async Task AddUserAsync(int userId, string blackId)
    {
        await _blackIPHttpService.AddUserAsync(userId, blackId);
    }

    /// <summary>
    /// 移除黑用户
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="blackId"></param>
    public async Task RemoveUserAsync(int userId, string blackId)
    {
        await _blackIPHttpService.RemoveUserAsync(userId, blackId);
    }
        
    /// <summary>
    /// 根据用户ID和授权类型获取列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="type">授权类型</param>
    /// <returns></returns>
    public async Task<List<BlackIpInfo>> FindByUserAsync(int userId, AuthrizeType type)
    {
        return (await _blackIPHttpService.FindByUserAsync(userId, type)).Data;
    }

    /// <summary>
    /// 检验IP的可访问性(白名单优先于黑名单），如果同时白名单、黑名名单都有同一IP，则也允许访问。
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    public async Task<bool> ValidateIpAccessAsync(string ipAddress, int userId)
    {
        return (await _blackIPHttpService.ValidateIpAccessAsync(ipAddress, userId)).Data;
    }
}