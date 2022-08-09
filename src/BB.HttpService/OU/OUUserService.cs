using BB.Entity.Security;
using BB.HttpService.Base;

namespace BB.HttpService.OU;

public class OUUserHttpService : BaseHttpService<OUUserEntity>
{
    private readonly IOUUserHttpService _ouUserHttpService;

    public OUUserHttpService(IOUUserHttpService ouUserHttpService) : base(ouUserHttpService)
    {
        _ouUserHttpService = ouUserHttpService;
    }

    /// <summary>
    /// 通过用户机构ID方式获取对应的用户基本信息列表
    /// </summary>
    /// <param name="ouId">用户机构ID方式</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> GetSimpleUsersByOuAsync(string ouId)
    {
        return (await _ouUserHttpService.GetSimpleUsersByOuAsync(ouId)).Data;
    }

    /// <summary>
    /// 通过机构ID获取对应的用户列表
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <returns></returns>
    public async Task<List<UserInfo>> GetUsersByOuAsync(string ouId)
    {
        return (await _ouUserHttpService.GetUsersByOuAsync(ouId)).Data;
    }

    /// <summary>
    /// 在机构中移除指定的用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="ouId">机构ID</param>
    public async Task RemoveUserAsync(int userId, string ouId)
    {
        await _ouUserHttpService.RemoveUserAsync(userId, ouId);
    }
}