using System.Data;
using BB.Entity.Security;
using BB.HttpServices.Base;

namespace BB.HttpServices.Core.Function;

public class FunctionHttpService : BaseHttpService<FunctionInfo>
{
    private readonly IFunctionHttpService _functionHttpService;

    public FunctionHttpService(IFunctionHttpService functionHttpService) : base(functionHttpService)
    {
        _functionHttpService = functionHttpService;
    }

    /// <summary>
    /// 根据角色ID列表和系统类型ID，获取对应的操作功能列表
    /// </summary>
    /// <param name="roleList">角色ID</param>
    /// <param name="typeId">系统类型ID</param>
    /// <returns></returns>
    public async Task<List<FunctionInfo>> GetFunctionsAsync(IEnumerable<int> roleList, string typeId)
    {
        return (await _functionHttpService.GetFunctionsAsync(roleList, typeId)).Data;
    }

    /// <summary>
    /// 根据角色ID字符串（逗号分开)和系统类型ID，获取对应的操作功能列表
    /// </summary>
    /// <param name="roleIDs">角色ID</param>
    /// <param name="typeId">系统类型ID</param>
    /// <returns></returns>
    public async Task<List<FunctionNodeInfo>> GetFunctionNodesAsync(string roleIDs, string typeId)
    {
        return (await _functionHttpService.GetFunctionNodesAsync(roleIDs, typeId)).Data;
    }

    /// <summary>
    /// 根据角色ID获取对应的操作功能列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    public async Task<List<FunctionInfo>> GetFunctionsByRoleAsync(int roleId)
    {
        return (await _functionHttpService.GetFunctionsByRoleAsync(roleId)).Data;
    }

    /// <summary>
    /// 根据用户ID，获取对应的功能列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    public async Task<List<FunctionInfo>> GetFunctionsByUserAsync(int userId, string typeId)
    {
        return (await _functionHttpService.GetFunctionsByUserAsync(userId, typeId)).Data;
    }

    /// <summary>
    /// 根据用户ID，获取对应的功能列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    public async Task<List<FunctionNodeInfo>> GetFunctionNodesByUserAsync(int userId, string typeId)
    {
        return (await _functionHttpService.GetFunctionNodesByUserAsync(userId, typeId)).Data;
    }

    /// <summary>
    /// 获取当前用户在指定系统类型下的功能集合
    /// </summary>
    /// <param name="typeId"></param>
    /// <returns></returns>
    public async Task<List<FunctionInfo>> GetUserFunctionsAsync(string typeId)
    {
        return (await _functionHttpService.GetUserFunctionsAsync(typeId)).Data;
    }

    /// <summary>
    /// 获取树形结构的功能列表
    /// </summary>
    /// <param name="systemType">系统类型的OID</param>
    public async Task<List<FunctionNodeInfo>> GetTreeAsync(string systemType)
    {
        return (await _functionHttpService.GetTreeAsync(systemType)).Data;
    }

    /// <summary>
    /// 获取指定功能下面的树形列表
    /// </summary>
    /// <param name="mainId">指定功能ID</param>
    public async Task<List<FunctionNodeInfo>> GetTreeByIdAsync(string mainId)
    {
        return (await _functionHttpService.GetTreeByIdAsync(mainId)).Data;
    }
                       
    /// <summary>
    /// 根据角色获取树形结构的功能列表
    /// </summary>
    public async Task<List<FunctionNodeInfo>> GetTreeWithRoleAsync(string systemType, List<int> roleList)
    {
        return (await _functionHttpService.GetTreeWithRoleAsync(systemType, roleList)).Data;
    }

    /// <summary>
    /// 根据角色获取树形结构的功能列表
    /// </summary>
    public async Task<List<FunctionNodeInfo>> GetTreeWithUserAsync(string systemType, int userId)
    {
        return (await _functionHttpService.GetTreeWithUserAsync(systemType, userId)).Data;
    }
    
    /// <summary>
    /// 批量新增功能权限
    /// </summary>
    /// <param name="mainInfo">模块权限</param>
    /// <param name="isAdd">新增权限</param>
    /// <param name="isUpdate">修改权限</param>
    /// <param name="isDelete">删除权限</param>
    /// <param name="isExport">导出权限</param>
    /// <param name="isImport">导入权限</param>
    /// <param name="isView">查看权限</param>
    /// <returns></returns>
    /// <exception cref="DataException"></exception>
    /// <exception cref="Exception"></exception>
    public async Task<bool> AddMore(FunctionInfo mainInfo, bool isAdd, bool isUpdate, bool isDelete, bool isExport, bool isImport, bool isView)
    {
        return (await _functionHttpService.AddMore(mainInfo, isAdd, isUpdate, isDelete, isExport, isImport, isView)).Data;
    }

    /// <summary>
    /// 删除指定节点及其子节点。如果该节点含有子节点，子节点也会一并删除
    /// </summary>
    /// <param name="mainId">节点ID</param>
    /// <returns></returns>
    public async Task<bool> DeleteWithSubNodeAsync(string mainId)
    {
        return (await _functionHttpService.DeleteWithSubNodeAsync(mainId)).Data;
    }
}