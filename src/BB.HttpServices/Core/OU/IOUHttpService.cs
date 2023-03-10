using BB.Entity.Security;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.OU;

public interface IOUHttpService : IHttpDispatchProxy, IBaseHttpService<OUInfo>
{

    /// <summary>
    /// 获取顶部的集团信息
    /// </summary>
    /// <returns></returns>
    [Get("topGroup")]
    Task<RESTfulResultControl<List<OUInfo>>> GetTopGroupAsync();
               
    /// <summary>
    /// 根据当前用户身份，获取对应的顶级机构管理节点。
    /// 如果是超级管理员，返回集团节点；如果是公司管理员，返回其公司节点
    /// </summary>
    /// <returns></returns>
    [Get("myTopGroup")]
    Task<RESTfulResultControl<List<OUInfo>>> GetMyTopGroupAsync();
        
    /// <summary>
    /// 获取部门分类为公司的列表【Category='公司'】
    /// </summary>
    /// <returns></returns>
    [Get("allCompany")]
    Task<RESTfulResultControl<List<OUInfo>>> GetAllCompanyAsync([QueryString]string groupId);

    /// <summary>
    /// 获取集团和公司的列表
    /// </summary>
    /// <returns></returns>
    [Get("groupCompany")]
    Task<RESTfulResultControl<List<OUInfo>>> GetGroupCompanyAsync();

    /// <summary>
    /// 获取集团和公司的树形结构列表
    /// </summary>
    /// <returns></returns>
    [Get("groupCompanyTree")]
    Task<RESTfulResultControl<List<OUNodeInfo>>> GetGroupCompanyTreeAsync();
        
    /// <summary>
    /// 为机构制定新的人员列表
    /// </summary>
    /// <param name="ouId">机构ID</param>
    /// <param name="newUserList">人员列表</param>
    /// <returns></returns>
    [Post("editOuUsers")]
    Task<RESTfulResultControl<bool>> EditOuUsersAsync([QueryString]string ouId, [Body]List<int> newUserList);

    /// <summary>
    /// 为机构添加相关用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="ouId">机构ID</param>
    [Post("user")]
    Task AddUserAsync([QueryString]int userId, [QueryString]string ouId);

    /// <summary>
    /// 根据角色ID获取对应的机构列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns></returns>
    [Get("oUsByRole")]
    Task<RESTfulResultControl<List<OUInfo>>> GetOUsByRoleAsync([QueryString]int roleId);

    /// <summary>
    /// 获取指定用户的机构列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    [Get("oUsByUser")]
    Task<RESTfulResultControl<List<OUInfo>>> GetOUsByUserAsync([QueryString]int userId);

    /// <summary>
    /// 获取指定公司下的所有部门
    /// </summary>
    /// <param name="companyId">用户所在公司</param>
    /// <returns></returns>
    [Get("ousByCompany")]
    Task<RESTfulResultControl<List<OUInfo>>> GetOusByCompanyAsync([QueryString]string companyId);
                        
    /// <summary>
    /// 根据指定机构节点ID，获取其下面所有机构列表
    /// </summary>
    /// <param name="parentId">指定机构节点ID</param>
    /// <returns></returns>
    [Get("allOUsByParent")]
    Task<RESTfulResultControl<List<OUInfo>>> GetAllOUsByParentAsync([QueryString]string parentId);

    /// <summary>
    /// 获取树形结构的机构列表
    /// </summary>
    [Get("tree")]
    Task<RESTfulResultControl<List<OUNodeInfo>>> GetTreeAsync();

    /// <summary>
    /// 获取指定机构下面的树形列表
    /// </summary>
    /// <param name="mainOuid">指定机构ID</param>
    [Get("treeById")]
    Task<RESTfulResultControl<List<OUNodeInfo>>> GetTreeByIdAsync([QueryString]string mainOuid);

    /// <summary>
    /// 获取机构的名称
    /// </summary>
    /// <param name="id">机构ID</param>
    /// <returns></returns>
    [Get("name")]
    Task<RESTfulResultControl<string>> GetNameAsync([QueryString]string id);

    /// <summary>
    /// 根据机构名称获取对应的对象
    /// </summary>
    /// <param name="name">机构名称</param>
    /// <returns></returns>
    [Get("byName")]
    Task<RESTfulResultControl<OUInfo>> FindByNameAsync([QueryString]string name);
                        
    /// <summary>
    /// 设置删除标志
    /// </summary>
    /// <param name="id">记录ID</param>
    /// <param name="deleted">是否删除</param>
    /// <returns></returns>
    [Post("setDeletedFlag")]
    Task<RESTfulResultControl<bool>> SetDeletedFlagAsync([QueryString]object id, [QueryString]bool deleted = true);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}oU/";
        // req.BaseAddress = builder.Uri;
    }
}