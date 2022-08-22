using System.Collections;
using System.Data;
using BB.Tools.Entity;
using Furion.FriendlyException;
using Furion.RemoteRequest;
using Furion.UnifyResult;

namespace BB.HttpServices.Base;

public interface IBaseHttpService<T> : IHttpDispatchProxy
{
    /// <summary>
    /// 插入指定对象到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    [Post(""), Client("local")]
    Task<RESTfulResult<bool>> InsertAsync([Body] T obj);

    /// <summary>
    /// 插入指定对象集合到数据库中
    /// </summary>
    /// <param name="list">指定的对象集合</param>
    /// <returns>执行操作是否成功。</returns>
    [Put("insertRange"), Client("local")]
    Task<RESTfulResult<bool>> InsertRangeAsync([Body] List<T> list);

    /// <summary>
    /// 更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [Put(""), Client("local")]
    Task<RESTfulResult<bool>> UpdateAsync([Body]T obj);

    /// <summary>
    /// 更新对象属性到数据库中
    /// </summary>
    /// <param name="recordField">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [Put("updateFields"), Client("local")]
    Task<RESTfulResult<bool>> UpdateFieldsAsync(Hashtable recordField);

    /// <summary>
    /// 插入或更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [Post("insertUpdate"), Client("local")]
    Task<RESTfulResult<bool>> InsertUpdateAsync([Body] T obj);

    /// <summary>
    /// 如果不存在记录，则插入对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行插入成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [Post("insertIfNew"), Client("local")]
    Task<RESTfulResult<bool>> InsertIfNewAsync([Body] T obj);

    /// <summary>
    /// 查询数据库,检查是否存在指定ID的对象
    /// </summary>
    /// <param name="key">对象的ID值</param>
    /// <returns>存在则返回指定的对象,否则返回Null</returns>
    [Get("findById"), Client("local")]
    Task<RESTfulResult<T>> FindByIdAsync(object key);

    /// <summary>
    /// 通过外键获取表数据
    /// </summary>
    /// <param name="foreignKeyId">外键ID</param>
    /// <param name="foreignKeyName">外键名称</param>
    /// <returns>数据列表</returns>
    [Get("findByForeignKey"), Client("local")]
    Task<RESTfulResult<List<T>>> FindByForeignKeyAsync(object foreignKeyId, string foreignKeyName = null);

    /// <summary>
    /// 通过外键获取表ID
    /// </summary>
    /// <param name="foreignKeyId">外键ID</param>
    /// <param name="foreignKeyName">外键名称</param>
    /// <returns>ID列表</returns>
    [Get("findIdByForeignKey"), Client("local")]
    Task<RESTfulResult<List<string>>> FindIdByForeignKeyAsync(object foreignKeyId, string foreignKeyName = null);

    /// <summary>
    /// 根据多个主键获取对象列表
    /// </summary>
    /// <param name="ids">主键数组</param>
    /// <returns>符合条件的对象列表</returns>
    [Get("findByIDs"), Client("local")]
    Task<RESTfulResult<List<T>>> FindByIDsAsync(object[] ids);

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合
    /// </summary>
    /// <param name="searchInfos">查询的条件</param>
    /// <returns>指定对象的集合</returns>
    [Post("find"), Client("local")]
    Task<RESTfulResult<List<T>>> FindAsync(CListItem[] searchInfos);

    /// <summary>
    /// 返回当前模块所有数据
    /// </summary>
    /// <param name="orderBy">自定义排序语句，如 Name Desc；如不指定，则使用默认排</param>
    /// <returns>指定对象的集合</returns>
    [Get("all"), Client("local")]
    Task<RESTfulResult<List<T>>> GetAllAsync(string orderBy = "");

    /// <summary>
    /// 返回数据库所有的对象集合(用于分页数据显示)
    /// </summary>
    /// <param name="info">分页实体信息</param>
    /// <returns>指定对象的集合</returns>
    [Get("allByPage"), Client("local")]
    Task<RESTfulResult<PageResult<T>>> GetAllAsync(PageInput info);

    /// <summary>
    /// 查询数据库,检查是否存在指定键值的对象
    /// </summary>
    /// <param name="fieldName">指定的属性名</param>
    /// <param name="key">指定的值</param>
    /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
    [Post("isExistKey"), Client("local")]
    Task<RESTfulResult<bool>> IsExistKeyAsync(string fieldName, object key);

    /// <summary>
    /// 根据主键和字段名称，获取对应字段的内容
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    [Get("fieldValue"), Client("local")]
    Task<RESTfulResult<string>> GetFieldValueAsync(object key, string fieldName);

    /// <summary>
    /// 根据主键和字段名称，获取对应字段的内容
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <param name="fieldNameList">字段名称列表</param>
    /// <returns></returns>
    [Get("fieldValueList"), Client("local")]
    Task<RESTfulResult<Dictionary<string, string>>> GetFieldValueListAsync(string key, List<string> fieldNameList);

    /// <summary>
    /// 获取字段列表
    /// </summary>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    [Get("fieldList"), Client("local")]
    Task<RESTfulResult<List<string>>> GetFieldListAsync(string fieldName);

    /// <summary>
    /// 根据指定对象的ID,从数据库中删除指定对象
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [Delete(""), Client("local")]
    Task<RESTfulResult<bool>> DeleteAsync(object key);

    /// <summary>
    /// 根据指定对象的ID,从数据库中删除指定对象
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [Delete("deleteByIds"), Client("local")]
    Task<RESTfulResult<bool>> DeleteByIdsAsync(object[] key);

    /// <summary>
    /// 根据条件获取分页实体数据
    /// </summary>
    /// <param name="searchInfos">分页搜索条件</param>
    /// <returns></returns>
    [Post("getEntitiesByPage"), Client("local")]
    Task<RESTfulResult<PageResult<T>>> GetEntitiesByPageAsync(PaginatedSearchInfos searchInfos);

    /// <summary>
    /// 通用审批
    /// </summary>
    /// <param name="key">主键</param>
    /// <returns></returns>
    [Post("approve"), Client("local")]
    Task<RESTfulResult<bool>> ApproveAsync(object key);

    /// <summary>
    /// 初始化一个实体
    /// </summary>
    /// <returns></returns>
    [Get("newEntity"), Client("local")]
    Task<RESTfulResult<T>> NewEntityAsync();

    /// <summary>
    /// 获取字段显示权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    [Get("permitDict"), Client("local")]
    Task<RESTfulResult<Dictionary<string, int>>> GetPermitDictAsync();

    /// <summary>
    /// 获取表的字段名称和数据类型列表
    /// </summary>
    /// <returns></returns>
    [Get("fieldTypeList"), Client("local")]
    Task<RESTfulResult<DataTable>> GetFieldTypeListAsync();

    /// <summary>
    /// 获取字段中文别名（用于界面显示）的字典集合
    /// </summary>
    /// <returns></returns>
    [Get("columnNameAlias"), Client("local")]
    Task<RESTfulResult<Dictionary<string, string>>> GetColumnNameAliasAsync();

    /// <summary>
    /// 获取列表显示的字段（用于界面显示）
    /// </summary>
    /// <returns></returns>
    [Post("displayColumns"), Client("local")]
    Task<RESTfulResult<string>> GetDisplayColumnsAsync();

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
    }

    /// <summary>
    /// 请求之前拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Request)]
    static void OnRequest(HttpRequestMessage req)
    {
        if (req.RequestUri == null)
        {
            throw Oops.Oh("请求 Uri 为空");
        }

        var builder = new UriBuilder(req.RequestUri);
        var path = req.RequestUri.AbsolutePath;
        builder.Path = $"/{nameof(T).Replace("Info", "").Replace("Entity", "")}{path}";
        req.RequestUri = builder.Uri;
    }

    /// <summary>
    /// 请求成功拦截
    /// </summary>
    /// <param name="res"></param>
    [Interceptor(InterceptorTypes.Response)]
    static void OnResponsing(HttpResponseMessage res)
    {
    }

    /// <summary>
    /// 请求异常拦截
    /// </summary>
    /// <param name="res"></param>
    /// <param name="errors"></param>
    [Interceptor(InterceptorTypes.Exception)]
    static void OnException(HttpResponseMessage res, string errors)
    {
    }
}