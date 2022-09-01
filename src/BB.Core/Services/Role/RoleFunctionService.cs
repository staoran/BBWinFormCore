using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Security;
using FluentValidation;

namespace BB.Core.Services.Role;

[ApiDescriptionSettings("权限")]
public class RoleFunctionService : BaseService<RoleFunction>, IDynamicApiController, ITransient
{
    public RoleFunctionService(BaseRepository<RoleFunction> repository, IValidator<RoleFunction> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 获取对应功能的相关角色列表
    /// </summary>
    /// <param name="functionId">对应功能ID</param>
    /// <returns></returns>
    public async Task<List<RoleInfo>> GetRolesByFunctionAsync(string functionId)
    {
        return await Repository.Db.Queryable<RoleInfo, RoleFunction>((r, rf) => r.ID == rf.RoleId)
            .Where((r, rf) => rf.FunctionId == functionId).ToListAsync();
    }

    /// <summary>
    /// 给指定角色添加功能点
    /// </summary>
    /// <param name="functionId">功能ID</param>
    /// <param name="roleId">角色ID</param>
    public async Task AddFunctionAsync(string functionId, int roleId)
    {
        await InsertAsync(new RoleFunction() { FunctionId = functionId, RoleId = roleId });
    }

    /// <summary>
    /// 从角色操作功能列表中，移除对应的功能
    /// </summary>
    /// <param name="functionId">功能ID</param>
    /// <param name="roleId">角色ID</param>
    public async Task RemoveFunctionAsync(string functionId, int roleId)
    {
        await DeleteAsync(x => x.FunctionId == functionId && x.RoleId == roleId);
    }

    /// <summary>
    /// 为角色指定新的操作功能列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="newFunctionList">功能列表</param>
    /// <returns></returns>
    public async Task<bool> EditRoleFunctionsAsync(int roleId, List<string> newFunctionList)
    {
        List<string> fList =
            await Repository.GetFieldListByConditionAsync(RoleFunction.FieldFunctionId, x => x.RoleId == roleId);

        var newList = newFunctionList.Where(x => !fList.Contains(x))
            .Select(x => new RoleFunction() { RoleId = roleId, FunctionId = x }).ToList();

        return await Repository.InsertRangeAsync(newList);
    }
}