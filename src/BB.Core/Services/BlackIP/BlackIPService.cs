using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Core.Services.User;
using BB.Entity.Security;
using BB.Tools.Cache;
using BB.Tools.Device;
using BB.Tools.Entity;
using BB.Tools.Extension;
using FluentValidation;

namespace BB.Core.Services.BlackIP;

[ApiDescriptionSettings("权限")]
public class BlackIPService : BaseService<BlackIpInfo>, IDynamicApiController, ITransient
{
    private readonly UserService _userService;

    public BlackIPService(BaseRepository<BlackIpInfo> repository, IValidator<BlackIpInfo> validator, UserService userService) : base(repository, validator)
    {
        _userService = userService;
    }

    /// <summary>
    /// 根据黑名单ID获取对应的用户ID列表
    /// </summary>
    /// <param name="id">黑名单ID</param>
    /// <returns></returns>
    public async Task<string> GetUserIdListAsync(string id)
    {
        string sql = $@"SELECT USER_ID FROM T_ACL_BLACKIP_USER m INNER JOIN T_ACL_BLACKIP t
            ON m.BLACKIP_ID=t.ID WHERE t.ID = '{id}' ";
        return await SqlValueListAsync(sql);
    }

    /// <summary>
    /// 根据名单ID获取对应的用户列表
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> GetSimpleUserByBlackIpAsync(string id)
    {
        string userIdList = "-1," + await GetUserIdListAsync(id);

        return await _userService.GetSimpleUsersAsync(userIdList.Trim(','));
    }

    /// <summary>
    /// 新增黑用户
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="blackId"></param>
    public async Task AddUserAsync(int userId, string blackId)
    {
        Dictionary<string, object> dic = new() { { "User_ID", userId }, { "BLACKIP_ID", blackId } };
        await Repository.Db.Insertable(dic).AS("T_ACL_BLACKIP_USER").ExecuteCommandAsync();
    }

    /// <summary>
    /// 移除黑用户
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="blackId"></param>
    public async Task RemoveUserAsync(int userId, string blackId)
    {
        Dictionary<string, object> dic = new()
        {
            { "User_ID", userId },
            { "BLACKIP_ID", blackId }
        };

        await Repository.Db.Deleteable<object>().AS("T_ACL_BLACKIP_USER")
            .WhereColumns(new List<Dictionary<string, object>>() { { dic } }).ExecuteCommandAsync();
    }
        
    /// <summary>
    /// 根据用户ID和授权类型获取列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="type">授权类型</param>
    /// <returns></returns>
    public async Task<List<BlackIpInfo>> FindByUserAsync(int userId, AuthrizeType type)
    {
        string sql = $@"SELECT t.* FROM T_ACL_BLACKIP t INNER JOIN T_ACL_BLACKIP_USER m
            ON t.ID=m.BLACKIP_ID WHERE m.User_ID = {userId} and t.AuthorizeType={(int)type} and t.Forbid=0 ";
        return await Repository.GetListAsync(sql);
    }

    /// <summary>
    /// 检验IP的可访问性(白名单优先于黑名单），如果同时白名单、黑名名单都有同一IP，则也允许访问。
    /// </summary>
    /// <param name="ipAddress"></param>
    /// <returns></returns>
    public async Task<bool> ValidateIpAccessAsync(string ipAddress, int userId)
    {
        bool result = false;
        List<BlackIpInfo> whiteList = await FindByUserAsync(userId, AuthrizeType.白名单);

        if (whiteList.Count > 0)
        {
            result = IsInList(whiteList, ipAddress);
            return result; //白名单优先于黑名单，在白名单则通过
        }

        List<BlackIpInfo> blackList = await FindByUserAsync(userId, AuthrizeType.黑名单);
        if (blackList.Count > 0)
        {
            bool flag = IsInList(blackList, ipAddress);
            return !flag;//不在则通过，在就禁止
        }

        //当黑白名单都为空的时候，那么返回true，则默认不禁止
        return true;
    }

    private bool IsInList(List<BlackIpInfo> list, string ip)
    {
        foreach (BlackIpInfo info in list)
        {
            if (NetworkUtil.IsInIp(ip, info.IPStart, info.IPEnd))
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.GetOrAdd($"{nameof(BlackIpInfo)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(BlackIpInfo.FieldName, SqlOperator.Like),
                new(BlackIpInfo.FieldAuthorizeType, SqlOperator.Equal ),
                new(BlackIpInfo.FieldForbid, SqlOperator.Equal )
            });
    }
}