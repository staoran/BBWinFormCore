using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.Role;

public interface IRoleHttpService : IHttpDispatchProxy, IBaseHttpService<RoleInfo>
{
	/// <summary>
	/// 根据公司ID（机构ID）获取对应的角色列表
	/// </summary>
	/// <param name="companyId">公司ID（机构ID）</param>
	/// <returns></returns>
	[Get("rolesByCompany")]
	Task<RESTfulResultControl<List<RoleInfo>>> GetRolesByCompanyAsync([QueryString]string companyId);

	/// <summary>
	/// 根据角色名称查找角色对象
	/// </summary>
	/// <param name="roleName">角色名称</param>
	/// <param name="companyId">公司ID</param>
	/// <returns></returns>
	[Get("roleByName")]
	Task<RESTfulResultControl<RoleInfo>> GetRoleByNameAsync([QueryString]string roleName, [QueryString]string companyId = null);

	/// <summary>
	/// 设置删除标志
	/// </summary>
	/// <param name="id">记录ID</param>
	/// <param name="deleted">是否删除</param>
	/// <returns></returns>
	[Post("setDeletedFlag")]
	Task<RESTfulResultControl<bool>> SetDeletedFlagAsync([QueryString]object id, [QueryString]bool deleted = true);


	/// <summary>
	/// 给指定角色添加菜单
	/// </summary>
	/// <param name="menuId">菜单ID</param>
	/// <param name="roleId">角色ID</param>
	[Post("menu")]
	Task AddMenuAsync([QueryString]string menuId, [QueryString]int roleId);

	/// <summary>
	/// 为指定角色移除对应菜单
	/// </summary>
	/// <param name="menuId">菜单ID</param>
	/// <param name="roleId">角色ID</param>
	[Delete("menu")]
	Task RemoveMenuAsync([QueryString]string menuId, [QueryString]int roleId);

	/// <summary>
	/// 为角色指定新的菜单列表
	/// </summary>
	/// <param name="roleId">角色ID</param>
	/// <param name="newList">菜单列表</param>
	/// <param name="systemType">系统类型</param>
	/// <returns></returns>
	[Post("editRoleMenus")]
	Task<RESTfulResultControl<bool>> EditRoleMenusAsync([QueryString]int roleId, [Body]List<string> newList, [QueryString]string systemType);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}role/";
        // req.BaseAddress = builder.Uri;
    }
}