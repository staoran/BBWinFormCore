using System.Data;
using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.RemoteRequest;
using Furion.UnifyResult;

namespace BB.HttpServices.Core.Function;

public interface IFunctionHttpService : IHttpDispatchProxy, IBaseHttpService<FunctionInfo>
{
    /// <summary>
    /// 根据角色ID列表和系统类型ID，获取对应的操作功能列表
    /// </summary>
    /// <param name="roleList">角色ID</param>
    /// <param name="typeId">系统类型ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<FunctionInfo>>> GetFunctionsAsync(IEnumerable<int> roleList, string typeId);

    /// <summary>
    /// 根据角色ID字符串（逗号分开)和系统类型ID，获取对应的操作功能列表
    /// </summary>
    /// <param name="roleIDs">角色ID</param>
    /// <param name="typeId">系统类型ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<FunctionNodeInfo>>> GetFunctionNodesAsync(string roleIDs, string typeId);

    /// <summary>
    /// 根据角色ID获取对应的操作功能列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<FunctionInfo>>> GetFunctionsByRoleAsync(int roleId);

    /// <summary>
    /// 根据用户ID，获取对应的功能列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<FunctionInfo>>> GetFunctionsByUserAsync(int userId, string typeId);

    /// <summary>
    /// 根据用户ID，获取对应的功能列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<FunctionNodeInfo>>> GetFunctionNodesByUserAsync(int userId, string typeId);

    /// <summary>
    /// 获取当前用户在指定系统类型下的功能集合
    /// </summary>
    /// <param name="typeId"></param>
    /// <returns></returns>
    Task<RESTfulResult<List<FunctionInfo>>> GetUserFunctionsAsync(string typeId);

    /// <summary>
    /// 获取树形结构的功能列表
    /// </summary>
    /// <param name="systemType">系统类型的OID</param>
    Task<RESTfulResult<List<FunctionNodeInfo>>> GetTreeAsync(string systemType);

    /// <summary>
    /// 获取指定功能下面的树形列表
    /// </summary>
    /// <param name="mainId">指定功能ID</param>
    Task<RESTfulResult<List<FunctionNodeInfo>>> GetTreeByIdAsync(string mainId);
                       
    /// <summary>
    /// 根据角色获取树形结构的功能列表
    /// </summary>
    Task<RESTfulResult<List<FunctionNodeInfo>>> GetTreeWithRoleAsync(string systemType, List<int> roleList);

    /// <summary>
    /// 根据角色获取树形结构的功能列表
    /// </summary>
    Task<RESTfulResult<List<FunctionNodeInfo>>> GetTreeWithUserAsync(string systemType, int userId);
    
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
    Task<RESTfulResult<bool>> AddMore(FunctionInfo mainInfo, bool isAdd, bool isUpdate, bool isDelete, bool isExport, bool isImport, bool isView);

    /// <summary>
    /// 删除指定节点及其子节点。如果该节点含有子节点，子节点也会一并删除
    /// </summary>
    /// <param name="mainId">节点ID</param>
    /// <returns></returns>
    Task<RESTfulResult<bool>> DeleteWithSubNodeAsync(string mainId);
}