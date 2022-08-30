using BB.HttpServices.Base;

namespace BB.HttpServices.Core.FieldControlConfig;

public class FieldControlConfigHttpService : BaseHttpService<Entity.Security.FieldControlConfig>
{
    private readonly IFieldControlConfigHttpService _fieldControlConfigHttpService;

    public FieldControlConfigHttpService(IFieldControlConfigHttpService fieldControlConfigHttpService) : base(fieldControlConfigHttpService)
    {
        _fieldControlConfigHttpService = fieldControlConfigHttpService;
    }
    
    /// <summary>
    /// 获取数据库的所有表名称
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> GetTableNames()
    {
        return (await _fieldControlConfigHttpService.GetTableNames()).Handling();
    }
                       
    /// <summary>
    /// 获取表的主键
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    public async Task<IEnumerable<string>> GetTableKeyList(string name)
    {
        return (await _fieldControlConfigHttpService.GetTableKeyList(name)).Handling();
    }
                       
    /// <summary>
    /// 获取表的自增字段
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    public async Task<List<string>> GetTableIdentityList(string name)
    {
        return (await _fieldControlConfigHttpService.GetTableIdentityList(name)).Handling();
    }
                       
    /// <summary>
    /// 获取表的注释
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    public async Task<string> GetTableComment(string name)
    {
        return (await _fieldControlConfigHttpService.GetTableComment(name)).Handling();
    }
                       
    /// <summary>
    /// 获取表的控件配置模版
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    public async Task<IEnumerable<Entity.Security.FieldControlConfig>> GetFieldControlConfigs(string name)
    {
        return (await _fieldControlConfigHttpService.GetFieldControlConfigs(name)).Handling();
    }

    /// <summary>
    /// 获取数据库的全部表名称和注释
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<string>> GetTableNamesAndComments()
    {
        return (await _fieldControlConfigHttpService.GetTableNamesAndComments()).Handling();
    }
}