using BB.Entity.Security;
using BB.HttpService.Base;
using Furion.UnifyResult;

namespace BB.HttpService.Menu;

public interface IMenuHttpService : IBaseHttpService<MenuInfo>
{
    /// <summary>
    /// 获取所有的菜单列表
    /// </summary>
    Task<RESTfulResult<List<MenuInfo>>> GetAllMenuAsync(string systemType);

    /// <summary>
    /// 获取树形结构的菜单列表
    /// </summary>
    Task<RESTfulResult<List<MenuNodeInfo>>> GetTreeAsync(string systemType);

    /// <summary>
    /// 获取所有的菜单列表
    /// </summary>
    Task<RESTfulResult<List<MenuInfo>>> GetAllTreeAsync(string systemType);

    /// <summary>
    /// 获取第一级的菜单列表
    /// </summary>
    Task<RESTfulResult<List<MenuInfo>>> GetTopMenuAsync(string systemType);

    /// <summary>
    /// 获取指定菜单下面的树形列表
    /// </summary>
    /// <param name="mainMenuId">指定菜单ID</param>
    Task<RESTfulResult<List<MenuNodeInfo>>> GetTreeByIdAsync(string mainMenuId);

    /// <summary>
    /// 根据指定的父ID获取其下面一级（仅限一级）的菜单列表
    /// </summary>
    /// <param name="pid">菜单父ID</param>
    Task<RESTfulResult<List<MenuInfo>>> GetMenuByIdAsync(string pid);

    /// <summary>
    /// 快速新增自动生成模块的菜单
    /// </summary>
    /// <param name="name">菜单名</param>
    /// <param name="winFormType">模块地址</param>
    Task<RESTfulResult<bool>> AddTransferMenuAsync(string name, string winFormType);


    /*
     * 在引入和角色多对多的关系后，菜单作为角色的资源之一，和功能模块并立。
     * 因此在处理上和Function表的处理类似，作为角色的资源之一。
     */ 
      
    /// <summary>
    /// 根据角色集合和系统标识获取对应的菜单集合
    /// </summary>
    /// <param name="roleIDs">角色ID字符串</param>
    /// <param name="typeId">系统类型</param>
    /// <returns></returns>
    Task<RESTfulResult<List<MenuNodeInfo>>> GetMenuNodesAsync(string roleIDs, string typeId);

    /// <summary>
    /// 根据角色ID获取功能集合
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<MenuInfo>>> GetMenusByRole(int roleId, string typeId);

    /// <summary>
    /// 根据用户ID，获取对应的菜单列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<MenuNodeInfo>>> GetMenuNodesByUser(int userId, string typeId);
}