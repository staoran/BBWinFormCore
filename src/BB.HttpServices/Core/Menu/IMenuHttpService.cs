using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.Menu;

public interface IMenuHttpService : IHttpDispatchProxy, IBaseHttpService<MenuInfo>
{
    /// <summary>
    /// 获取所有的菜单列表
    /// </summary>
    [Get("allMenu")]
    Task<RESTfulResultControl<List<MenuInfo>>> GetAllMenuAsync([QueryString]string systemType);

    /// <summary>
    /// 获取树形结构的菜单列表
    /// </summary>
    [Get("tree")]
    Task<RESTfulResultControl<List<MenuNodeInfo>>> GetTreeAsync([QueryString]string systemType);

    /// <summary>
    /// 获取所有的菜单列表
    /// </summary>
    [Get("allTree")]
    Task<RESTfulResultControl<List<MenuInfo>>> GetAllTreeAsync([QueryString]string systemType);

    /// <summary>
    /// 获取第一级的菜单列表
    /// </summary>
    [Get("topMenu")]
    Task<RESTfulResultControl<List<MenuInfo>>> GetTopMenuAsync([QueryString]string systemType);

    /// <summary>
    /// 获取指定菜单下面的树形列表
    /// </summary>
    /// <param name="mainMenuId">指定菜单ID</param>
    [Get("treeById")]
    Task<RESTfulResultControl<List<MenuNodeInfo>>> GetTreeByIdAsync([QueryString]string mainMenuId);

    /// <summary>
    /// 根据指定的父ID获取其下面一级（仅限一级）的菜单列表
    /// </summary>
    /// <param name="pid">菜单父ID</param>
    [Get("menuById")]
    Task<RESTfulResultControl<List<MenuInfo>>> GetMenuByIdAsync([QueryString]string pid);

    /// <summary>
    /// 快速新增自动生成模块的菜单
    /// </summary>
    /// <param name="name">菜单名</param>
    /// <param name="winFormType">模块地址</param>
    [Post("transferMenu")]
    Task<RESTfulResultControl<bool>> AddTransferMenuAsync([QueryString]string name, [QueryString]string winFormType);


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
    [Get("menuNodes")]
    Task<RESTfulResultControl<List<MenuNodeInfo>>> GetMenuNodesAsync([QueryString]int[] roleIds, [QueryString]string typeId);

    /// <summary>
    /// 根据角色ID获取功能集合
    /// </summary>
    /// <param name="roleIds">角色ID数组</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    [Get("menusByRole")]
    Task<RESTfulResultControl<List<MenuInfo>>> GetMenusByRole([QueryString]int[] roleIds, [QueryString]string typeId);

    /// <summary>
    /// 根据用户ID，获取对应的菜单列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="typeId">系统类别ID</param>
    /// <returns></returns>
    [Get("menuNodesByUser")]
    Task<RESTfulResultControl<List<MenuNodeInfo>>> GetMenuNodesByUser([QueryString]int userId, [QueryString]string typeId);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}menu/";
        // req.BaseAddress = builder.Uri;
    }
}