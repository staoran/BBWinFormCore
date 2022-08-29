using System.Collections.Generic;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Security;
using FluentValidation;

namespace BB.Core.Services.OU;

public class OUUserService : BaseService<OUUserEntity>, IDynamicApiController, ITransient
{
    public OUUserService(BaseRepository<OUUserEntity> repository, IValidator<OUUserEntity> validator) : base(repository, validator)
    {
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
    /// 在机构中移除指定的用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="ouId">机构ID</param>
    public async Task RemoveUserAsync(int userId, string ouId)
    {
        await DeleteAsync(x => x.OUId == ouId && x.UserId == userId);
    }
}