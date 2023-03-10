using BB.Entity.Security;
using BB.HttpServices.Base;

namespace BB.HttpServices.Core.Menu;

public class MenuHttpService : BaseHttpService<MenuInfo>
{
    private readonly IMenuHttpService _menuHttpService;

    public MenuHttpService(IMenuHttpService menuHttpService) : base(menuHttpService)
    {
        _menuHttpService = menuHttpService;
    }

    /// <summary>
    /// 获取所有的菜单列表
    /// </summary>
    public async Task<List<MenuInfo>> GetAllMenuAsync(string systemType)
    {
        return (await _menuHttpService.GetAllMenuAsync(systemType)).Handling();
    }
                
    /// <summary>
    /// 获取树形结构的菜单列表
    /// </summary>
    public async Task<List<MenuNodeInfo>> GetTreeAsync(string systemType)
    {
        return (await _menuHttpService.GetTreeAsync(systemType)).Handling();
    }

    /// <summary>
    /// 获取所有的按钮列表
    /// </summary>
    public async Task<List<MenuInfo>> GetAllButtonsAsync(string systemType)
    {
        return (await _menuHttpService.GetAllButtonsAsync(systemType)).Handling();
    }

    /// <summary>
    /// 获取第一级的菜单列表
    /// </summary>
    public async Task<List<MenuInfo>> GetTopMenuAsync(string systemType)
    {
        return (await _menuHttpService.GetTopMenuAsync(systemType)).Handling();
    }

    /// <summary>
    /// 获取指定菜单下面的树形列表
    /// </summary>
    /// <param name="mainMenuId">指定菜单ID</param>
    public async Task<List<MenuNodeInfo>> GetTreeByIdAsync(string mainMenuId)
    {
        return (await _menuHttpService.GetTreeByIdAsync(mainMenuId)).Handling();
    }

    /// <summary>
    /// 根据指定的父ID获取其下面一级（仅限一级）的菜单列表
    /// </summary>
    /// <param name="pid">菜单父ID</param>
    public async Task<List<MenuInfo>> GetMenuByIdAsync(string pid)
    {
        return (await _menuHttpService.GetMenuByIdAsync(pid)).Handling();
    }

    /// <summary>
    /// 快速新增自动生成模块的菜单
    /// </summary>
    /// <param name="name">菜单名</param>
    /// <param name="winFormType">模块地址</param>
    public async Task<bool> AddTransferMenuAsync(string name, string winFormType)
    {
        return (await _menuHttpService.AddTransferMenuAsync(name, winFormType)).Handling();
    }


    /*
     * 在引入和角色多对多的关系后，菜单作为角色的资源之一，和功能模块并立。
     * 因此在处理上和Function表的处理类似，作为角色的资源之一。
     */ 
      
    /// <summary>
    /// 根据角色集合和系统标识获取对应的菜单集合
    /// </summary>
    /// <param name="roleIds">角色ID数组</param>
    /// <param name="typeId">系统类型</param>
    /// <returns></returns>
    public async Task<List<MenuNodeInfo>> GetMenuNodesAsync(int[] roleIds, string typeId)
    {
        return (await _menuHttpService.GetMenuNodesAsync(roleIds, typeId)).Handling();
    }

    /// <summary>
    /// 根据角色ID获取功能集合
    /// </summary>
    /// <param name="roleIds">角色ID数组</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    public async Task<List<MenuInfo>> GetMenusByRole(int[] roleIds, string typeId)
    {
        return (await _menuHttpService.GetMenusByRole(roleIds, typeId)).Handling();
    }

    /// <summary>
    /// 根据用户ID，获取对应的菜单列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    public async Task<List<MenuNodeInfo>> GetMenuNodesByUser(int userId, string typeId)
    {
        return (await _menuHttpService.GetMenuNodesByUser(userId, typeId)).Handling();
    }
}