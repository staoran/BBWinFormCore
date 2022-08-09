using BB.HttpService.Base;
using Furion.UnifyResult;

namespace BB.HttpService.FieldControlConfig;

public interface IFieldControlConfigHttpService : IBaseHttpService<Entity.Security.FieldControlConfig>
{
    /// <summary>
    /// 获取数据库的所有表名称
    /// </summary>
    /// <returns></returns>
    Task<RESTfulResult<List<string>>> GetTableNames();
                       
    /// <summary>
    /// 获取表的主键
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    Task<RESTfulResult<IEnumerable<string>>> GetTableKeyList(string name);
                       
    /// <summary>
    /// 获取表的自增字段
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    Task<RESTfulResult<List<string>>> GetTableIdentityList(string name);
                       
    /// <summary>
    /// 获取表的注释
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    Task<RESTfulResult<string>> GetTableComment(string name);
                       
    /// <summary>
    /// 获取表的控件配置模版
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    Task<RESTfulResult<IEnumerable<Entity.Security.FieldControlConfig>>> GetFieldControlConfigs(string name);

    /// <summary>
    /// 获取数据库的全部表名称和注释
    /// </summary>
    /// <returns></returns>
    Task<RESTfulResult<IEnumerable<string>>> GetTableNamesAndComments();
}