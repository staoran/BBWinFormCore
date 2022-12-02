using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Core.Services.RoleData;
using BB.Core.Services.User;
using BB.Entity.Security;
using BB.Tools.Cache;
using BB.Tools.Extension;
using FluentValidation;

namespace BB.Core.Services.OU;

[ApiDescriptionSettings("用户与机构")]
public class OUUserService : BaseService<OUUserEntity>, IDynamicApiController, ITransient
{
    private readonly RoleDataService _roleDataService;
    private readonly UserService _userService;

    public OUUserService(BaseRepository<OUUserEntity> repository, IValidator<OUUserEntity> validator,
        RoleDataService roleDataService, UserService userService) : base(repository, validator)
    {
        _roleDataService = roleDataService;
        _userService = userService;
    }

    /// <summary>
    /// 通过用户机构ID方式获取对应的用户基本信息列表
    /// </summary>
    /// <param name="ouId">用户机构ID方式</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> GetSimpleUsersByOuAsync(string ouId)
    {
        return await Repository.Db.Queryable<UserInfo, OUUserEntity>((u, ou) => u.ID == ou.UserId)
            .Where((u, ou) => !u.Deleted && ou.OUId == ouId).Select<SimpleUserInfo>().ToListAsync();
    }

    /// <summary>
    /// 通过机构ID获取对应的用户列表
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <returns></returns>
    public async Task<List<UserInfo>> GetUsersByOuAsync(string ouId)
    {
        return await Repository.Db.Queryable<UserInfo, OUUserEntity>((u, ou) => u.ID == ou.UserId)
            .Where((u, ou) => !u.Deleted && ou.OUId == ouId).ToListAsync();
    }

    /// <summary>
    /// 通过用户ID获取对应的机构列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetOusByUserIdAsync(int userId)
    {
        return await Repository.Db.Queryable<OUInfo, OUUserEntity>((u, ou) => u.HandNo == ou.OUId)
            .Where((u, ou) => !u.Deleted && ou.UserId == userId).ToListAsync();
    }

    /// <summary>
    /// 通过用户ID获取对应的机构数据权限列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<List<string>> GetOuIdsByUserIdAsync(int userId)
    {
        return await Cache.Instance.GetOrCreateAsync($"GetOuIdsByUserIdAsync_{userId}", async () =>
        {
            // 用户关联的机构
            var userOuList = await GetFieldListAsync(x => x.OUId, x => x.UserId == userId);
            // 用户关联的角色关联的机构
            var userRoleOuList = await _roleDataService.GetBelongCompanysByUserAsync(userId);
            if (userRoleOuList.Contains("-1"))
            {
                var user = await _userService.FindByIdAsync(userId);
                userRoleOuList.Remove("-1");
                userRoleOuList.Add(user.CompanyId);
            }

            // 合并去重
            return userOuList.Union(userRoleOuList).ToList();
        }, TimeSpan.FromMinutes(1), TimeSpan.FromHours(10));
    }

    /// <summary>
    /// 在机构中移除指定的用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="ouId">机构ID</param>
    public async Task RemoveUserAsync(int userId, string ouId)
    {
        await DeleteAsync(x => x.OUId == ouId && x.UserId == userId);
    }
}