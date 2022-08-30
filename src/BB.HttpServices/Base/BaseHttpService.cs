using System.Collections;
using System.Data;
using BB.Tools.Entity;
using Furion.DependencyInjection;

namespace BB.HttpServices.Base;

public class BaseHttpService<T> : ITransient
{
    private readonly IBaseHttpService<T> _baseHttpService;

    public BaseHttpService(IBaseHttpService<T> baseHttpService)
    {
        _baseHttpService = baseHttpService;
    }

    /// <summary>
    /// 插入指定对象到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    public async Task<bool> InsertAsync(T obj)
    {
        return (await _baseHttpService.InsertAsync(obj)).Handling();
    }

    /// <summary>
    /// 插入指定对象集合到数据库中
    /// </summary>
    /// <param name="list">指定的对象集合</param>
    /// <returns>执行操作是否成功。</returns>
    public async Task<bool> InsertRangeAsync(List<T> list)
    {
        return (await _baseHttpService.InsertRangeAsync(list)).Handling();
    }

    /// <summary>
    /// 更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public async Task<bool> UpdateAsync(T obj)
    {
        return (await _baseHttpService.UpdateAsync(obj)).Handling();
    }

    /// <summary>
    /// 更新对象属性到数据库中
    /// </summary>
    /// <param name="recordField">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public async Task<bool> UpdateFieldsAsync(Hashtable recordField)
    {
        return (await _baseHttpService.UpdateFieldsAsync(recordField)).Handling();
    }

    /// <summary>
    /// 插入或更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public async Task<bool> InsertUpdateAsync(T obj)
    {
        return (await _baseHttpService.InsertUpdateAsync(obj)).Handling();
    }

    /// <summary>
    /// 如果不存在记录，则插入对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行插入成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public async Task<bool> InsertIfNewAsync(T obj)
    {
        return (await _baseHttpService.InsertIfNewAsync(obj)).Handling();
    }

    /// <summary>
    /// 查询数据库,检查是否存在指定ID的对象
    /// </summary>
    /// <param name="key">对象的ID值</param>
    /// <returns>存在则返回指定的对象,否则返回Null</returns>
    public async Task<T> FindByIdAsync(object key)
    {
        return (await _baseHttpService.FindByIdAsync(key)).Handling();
    }

    /// <summary>
    /// 通过外键获取表数据
    /// </summary>
    /// <param name="foreignKeyId">外键ID</param>
    /// <param name="foreignKeyName">外键名称</param>
    /// <returns>数据列表</returns>
    public async Task<List<T>> FindByForeignKeyAsync(object foreignKeyId, string foreignKeyName = null)
    {
        return (await _baseHttpService.FindByForeignKeyAsync(foreignKeyId, foreignKeyName)).Handling();
    }

    /// <summary>
    /// 通过外键获取表ID
    /// </summary>
    /// <param name="foreignKeyId">外键ID</param>
    /// <param name="foreignKeyName">外键名称</param>
    /// <returns>ID列表</returns>
    public async Task<List<string>> FindIdByForeignKeyAsync(object foreignKeyId, string foreignKeyName = null)
    {
        return (await _baseHttpService.FindIdByForeignKeyAsync(foreignKeyId, foreignKeyName)).Handling();
    }

    /// <summary>
    /// 根据多个主键获取对象列表
    /// </summary>
    /// <param name="ids">主键数组</param>
    /// <returns>符合条件的对象列表</returns>
    public async Task<List<T>> FindByIDsAsync(object[] ids)
    {
        return (await _baseHttpService.FindByIDsAsync(ids)).Handling();
    }

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合
    /// </summary>
    /// <param name="searchInfos">查询的条件</param>
    /// <returns>指定对象的集合</returns>
    public async Task<List<T>> FindAsync(Dictionary<string, string> searchInfos)
    {
        return (await _baseHttpService.FindAsync(searchInfos)).Handling();
    }

    /// <summary>
    /// 返回当前模块所有数据
    /// </summary>
    /// <param name="orderBy">自定义排序语句，如 Name Desc；如不指定，则使用默认排</param>
    /// <returns>指定对象的集合</returns>
    public async Task<List<T>> GetAllAsync(string orderBy = "")
    {
        return (await _baseHttpService.GetAllAsync(orderBy)).Handling();
    }

    /// <summary>
    /// 返回数据库所有的对象集合(用于分页数据显示)
    /// </summary>
    /// <param name="info">分页实体信息</param>
    /// <returns>指定对象的集合</returns>
    public async Task<PageResult<T>> GetAllAsync(PageInput info)
    {
        return (await _baseHttpService.GetAllAsync(info)).Handling();
    }

    /// <summary>
    /// 查询数据库,检查是否存在指定键值的对象
    /// </summary>
    /// <param name="fieldName">指定的属性名</param>
    /// <param name="key">指定的值</param>
    /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
    public async Task<bool> IsExistKeyAsync(string fieldName, object key)
    {
        return (await _baseHttpService.IsExistKeyAsync(fieldName, key)).Handling();
    }

    /// <summary>
    /// 根据主键和字段名称，获取对应字段的内容
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public async Task<string> GetFieldValueAsync(object key, string fieldName)
    {
        return (await _baseHttpService.GetFieldValueAsync(key, fieldName)).Handling();
    }

    /// <summary>
    /// 根据主键和字段名称，获取对应字段的内容
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <param name="fieldNameList">字段名称列表</param>
    /// <returns></returns>
    public async Task<Dictionary<string, string>> GetFieldValueListAsync(string key, List<string> fieldNameList)
    {
        return (await _baseHttpService.GetFieldValueListAsync(key, fieldNameList)).Handling();
    }
    
    /// <summary>
    /// 获取字段列表
    /// </summary>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public async Task<List<string>> GetFieldListAsync(string fieldName)
    {
        return (await _baseHttpService.GetFieldListAsync(fieldName)).Handling();
    }

    /// <summary>
    /// 根据指定对象的ID,从数据库中删除指定对象
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public async Task<bool> DeleteAsync(object key)
    {
        return (await _baseHttpService.DeleteAsync(key)).Handling();
    }

    /// <summary>
    /// 根据指定对象的ID,从数据库中删除指定对象
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public async Task<bool> DeleteByIdsAsync(object[] key)
    {
        return (await _baseHttpService.DeleteByIdsAsync(key)).Handling();
    }

    /// <summary>
    /// 根据条件获取分页实体数据
    /// </summary>
    /// <param name="searchInfos">分页搜索条件</param>
    /// <returns></returns>
    public async Task<PageResult<T>> GetEntitiesByPageAsync(PaginatedSearchInfos searchInfos)
    {
        return (await _baseHttpService.GetEntitiesByPageAsync(searchInfos)).Handling();
    }

    /// <summary>
    /// 通用审批
    /// </summary>
    /// <param name="key">主键</param>
    /// <returns></returns>
    public async Task<bool> ApproveAsync(object key)
    {
        return (await _baseHttpService.ApproveAsync(key)).Handling();
    }

    /// <summary>
    /// 初始化一个实体
    /// </summary>
    /// <returns></returns>
    public async Task<T> NewEntityAsync()
    {
        return (await _baseHttpService.NewEntityAsync()).Handling();
    }

    /// <summary>
    /// 获取字段显示权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public async Task<Dictionary<string, int>> GetPermitDictAsync()
    {
        return (await _baseHttpService.GetPermitDictAsync()).Handling();
    }

    /// <summary>
    /// 获取表的字段名称和数据类型列表
    /// </summary>
    /// <returns></returns>
    public async Task<DataTable> GetFieldTypeListAsync()
    {
        return (await _baseHttpService.GetFieldTypeListAsync()).Handling();
    }

    /// <summary>
    /// 获取字段中文别名（用于界面显示）的字典集合
    /// </summary>
    /// <returns></returns>
    public async Task<Dictionary<string, string>> GetColumnNameAliasAsync()
    {
        return (await _baseHttpService.GetColumnNameAliasAsync()).Handling();
    }

    /// <summary>
    /// 获取列表显示的字段（用于界面显示）
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetDisplayColumnsAsync()
    {
        return (await _baseHttpService.GetDisplayColumnsAsync()).Handling();
    }
}