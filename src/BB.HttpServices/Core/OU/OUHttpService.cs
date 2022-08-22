using BB.Entity.Security;
using BB.HttpServices.Base;

namespace BB.HttpServices.Core.OU;

public class OUHttpService : BaseHttpService<OUInfo>
{
    private readonly IOUHttpService _ouHttpService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public OUHttpService(IOUHttpService ouHttpService) : base(ouHttpService)
    {
        _ouHttpService = ouHttpService;
    }

    /// <summary>
    /// 获取顶部的集团信息
    /// </summary>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetTopGroupAsync()
    {
        return (await _ouHttpService.GetTopGroupAsync()).Data;
    }
               
    /// <summary>
    /// 根据当前用户身份，获取对应的顶级机构管理节点。
    /// 如果是超级管理员，返回集团节点；如果是公司管理员，返回其公司节点
    /// </summary>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetMyTopGroupAsync()
    {
        return (await _ouHttpService.GetMyTopGroupAsync()).Data;
    }
        
    /// <summary>
    /// 获取部门分类为公司的列表【Category='公司'】
    /// </summary>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetAllCompanyAsync(string groupId)
    {
        return (await _ouHttpService.GetAllCompanyAsync(groupId)).Data;
    }

    /// <summary>
    /// 获取集团和公司的列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetGroupCompanyAsync()
    {
        return (await _ouHttpService.GetGroupCompanyAsync()).Data;
    }

    /// <summary>
    /// 获取集团和公司的树形结构列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<OUNodeInfo>> GetGroupCompanyTreeAsync()
    {
        return (await _ouHttpService.GetGroupCompanyTreeAsync()).Data;
    }
        
    /// <summary>
    /// 为机构制定新的人员列表
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <param name="newUserList">人员列表</param>
    /// <returns></returns>
    public async Task<bool> EditOuUsersAsync(string ouId, List<int> newUserList)
    {
        return (await _ouHttpService.EditOuUsersAsync(ouId, newUserList)).Data;
    }

    /// <summary>
    /// 为机构添加相关用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="ouId">机构ID</param>
    public async Task AddUserAsync(int userId, string ouId)
    {
        await _ouHttpService.AddUserAsync(userId, ouId);
    }

    /// <summary>
    /// 根据角色ID获取对应的机构列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetOUsByRoleAsync(int roleId)
    {
        return (await _ouHttpService.GetOUsByRoleAsync(roleId)).Data;
    }

    /// <summary>
    /// 获取指定用户的机构列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetOUsByUserAsync(int userId)
    {
        return (await _ouHttpService.GetOUsByUserAsync(userId)).Data;
    }

    /// <summary>
    /// 获取指定公司下的所有部门
    /// </summary>
    /// <param name="companyId">用户所在公司</param>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetOusByCompanyAsync(string companyId)
    {
        return (await _ouHttpService.GetOusByCompanyAsync(companyId)).Data;
    }
                        
    /// <summary>
    /// 根据指定机构节点ID，获取其下面所有机构列表
    /// </summary>
    /// <param name="parentId">指定机构节点ID</param>
    /// <returns></returns>
    public async Task<List<OUInfo>> GetAllOUsByParentAsync(string parentId)
    {
        return (await _ouHttpService.GetAllOUsByParentAsync(parentId)).Data;
    }

    /// <summary>
    /// 获取树形结构的机构列表
    /// </summary>
    public async Task<List<OUNodeInfo>> GetTreeAsync()
    {
        return (await _ouHttpService.GetTreeAsync()).Data;
    }

    /// <summary>
    /// 获取指定机构下面的树形列表
    /// </summary>
    /// <param name="mainOuid">指定机构ID</param>
    public async Task<List<OUNodeInfo>> GetTreeByIdAsync(string mainOuid)
    {
        return (await _ouHttpService.GetTreeByIdAsync(mainOuid)).Data;
    }

    /// <summary>
    /// 获取机构的名称
    /// </summary>
    /// <param name="id">机构ID</param>
    /// <returns></returns>
    public async Task<string> GetNameAsync(string id)
    {
        return (await _ouHttpService.GetNameAsync(id)).Data;
    }

    /// <summary>
    /// 根据机构名称获取对应的对象
    /// </summary>
    /// <param name="name">机构名称</param>
    /// <returns></returns>
    public async Task<OUInfo> FindByNameAsync(string name)
    {
        return (await _ouHttpService.FindByNameAsync(name)).Data;
    }
                        
    /// <summary>
    /// 设置删除标志
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="deleted">是否删除</param>
    /// <returns></returns>
    public async Task<bool> SetDeletedFlagAsync(object id, bool deleted = true)
    {
        return (await _ouHttpService.SetDeletedFlagAsync(id, deleted)).Data;
    }
}