using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Security;
using FluentValidation;

namespace BB.Core.Services.User;

[ApiDescriptionSettings("用户与机构")]
public class UserRoleService : BaseService<UserRoleEntity>, IDynamicApiController, ITransient
{
    public UserRoleService(BaseRepository<UserRoleEntity> repository, IValidator<UserRoleEntity> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 通过用户角色ID方式获取对应的用户基本信息列表
    /// </summary>
    /// <param name="roleId">用户角色ID</param>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> GetSimpleUsersByRoleAsync(int roleId)
    {
        return await Repository.Db.Queryable<UserInfo, UserRoleEntity>((u, ur) => u.ID == ur.UserId)
            .Where((u, ur) => ur.RoleId == roleId && !u.Deleted)
            .Select<SimpleUserInfo>()
            .ToListAsync();
    }

    /// <summary>
    /// 通过角色ID获取对应的用户列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    public async Task<List<UserInfo>> GetUsersByRoleAsync(int roleId)
    {
        return await Repository.Db.Queryable<UserInfo, UserRoleEntity>((u, ur) => u.ID == ur.UserId)
            .Where((u, ur) => ur.RoleId == roleId && !u.Deleted)
            .Select<UserInfo>()
            .ToListAsync();
    }

    /// <summary>
    /// 更新用户的角色列表
    /// </summary>
    /// <param name="userid">用户ID</param>
    /// <param name="roleList">角色列表</param>
    public async Task UpdateRolesAsync(int userid, List<int> roleList)
    {
        List<UserRoleEntity> roleFunctions =
            roleList.Select(x => new UserRoleEntity() { RoleId = x, UserId = userid }).ToList();
        
        await UseTransactionAsync(async ()=>
        {
            await Repository.DeleteAsync(x => x.UserId == userid);
            await Repository.InsertRangeAsync(roleFunctions);
        });
    }

    /// <summary>
    /// 给指定角色添加用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleId">角色ID</param>
    public async Task AddUserAsync(int userId, int roleId)
    {
        await InsertAsync(new UserRoleEntity(){RoleId = roleId, UserId = userId});
    }

    /// <summary>
    /// 从角色的用户列表中移除指定的用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleId">角色ID</param>
    public async Task RemoveUserAsync(int userId, int roleId)
    {
        await Repository.DeleteAsync(x => x.UserId == userId && x.RoleId == roleId);
    }

    /// <summary>
    /// 根据用户的ID获取对应的角色列表
    /// </summary>
    /// <param name="userId">用户的ID</param>
    /// <returns></returns>
    public async Task<List<RoleInfo>> GetRolesByUserAsync(int userId)
    {
        return await Repository.Db
            .Queryable<RoleInfo, UserRoleEntity>((r, ur) => r.ID == ur.RoleId)
            .Where((r, ur) => ur.UserId == userId).ToListAsync();
    }

    /// <summary>
    /// 判断用户ID是否在指定的角色中
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    public async Task<bool> UserInRoleAsync(int userId, int roleId)
    {
        return await Repository.IsAnyAsync(x => x.UserId == userId && x.RoleId == roleId);
    }

    /// <summary>
    /// 判断用户是否为公司管理员
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<bool> UserIsCompanyAdminAsync(int userId)
    {
        return await UserInRoleAsync(userId, RoleInfo.COMPANY_ADMIN_ID);
    }

    /// <summary>
    /// 判断用户是否为超级管理员
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<bool> UserIsSuperAdminAsync(int userId)
    {
        return await UserInRoleAsync(userId, RoleInfo.SUPER_ADMIN_ID);
    }

    /// <summary>
    /// 判断用户是否为管理员，超级管理员、公司级别的系统管理员均通过。
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<bool> UserIsAdminAsync(int userId)
    {
        bool result = await UserInRoleAsync(userId, RoleInfo.SUPER_ADMIN_ID);
        if (!result)
        {
            result = await UserInRoleAsync(userId, RoleInfo.COMPANY_ADMIN_ID);
        }
        return result;
    }

    /// <summary>
    /// 获取管理员包含的用户基础信息列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<SimpleUserInfo>> GetAdminSimpleUsersAsync()
    {
        return await GetSimpleUsersByRoleAsync(RoleInfo.SUPER_ADMIN_ID);
    }

    /// <summary>
    /// 为角色指定新的人员列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newUserList">人员列表</param>
    /// <returns></returns>
    public async Task<bool> EditRoleUsersAsync(int roleId, List<int> newUserList)
    {
        List<UserRoleEntity> roleFunctions =
            newUserList.Select(x => new UserRoleEntity() { RoleId = roleId, UserId = x }).ToList();
        
        return await UseTransactionAsync(async ()=>
        {
            await Repository.DeleteAsync(x => x.RoleId == roleId);
            await Repository.InsertRangeAsync(roleFunctions);
        });
    }
}