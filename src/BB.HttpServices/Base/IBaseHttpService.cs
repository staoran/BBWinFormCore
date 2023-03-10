using System.Collections;
using System.Data;
using BB.Tools.Cache;
using BB.Tools.Entity;
using BB.Tools.Extension;
using Furion.DataEncryption;
using Furion.FriendlyException;
using Furion.RemoteRequest;

namespace BB.HttpServices.Base;

public interface IBaseHttpService<T>
{
    /// <summary>
    /// 插入指定对象到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    [Post("")]
    Task<RESTfulResultControl<bool>> InsertAsync([Body] T obj);

    /// <summary>
    /// 插入指定对象集合到数据库中
    /// </summary>
    /// <param name="list">指定的对象集合</param>
    /// <returns>执行操作是否成功。</returns>
    [Put("insertRange")]
    Task<RESTfulResultControl<bool>> InsertRangeAsync([Body] List<T> list);

    /// <summary>
    /// 更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [Put("")]
    Task<RESTfulResultControl<bool>> UpdateAsync([Body]T obj);

    /// <summary>
    /// 更新对象属性到数据库中
    /// </summary>
    /// <param name="recordField">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [Put("updateFields")]
    Task<RESTfulResultControl<bool>> UpdateFieldsAsync([Body]Hashtable recordField);

    /// <summary>
    /// 插入或更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [Post("insertUpdate")]
    Task<RESTfulResultControl<bool>> InsertUpdateAsync([Body] T obj);

    /// <summary>
    /// 如果不存在记录，则插入对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行插入成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [Post("insertIfNew")]
    Task<RESTfulResultControl<bool>> InsertIfNewAsync([Body] T obj);

    /// <summary>
    /// 查询数据库,检查是否存在指定ID的对象
    /// </summary>
    /// <param name="key">对象的ID值</param>
    /// <returns>存在则返回指定的对象,否则返回Null</returns>
    [Get("findById")]
    Task<RESTfulResultControl<T>> FindByIdAsync([QueryString]object key);

    /// <summary>
    /// 通过外键获取表数据
    /// </summary>
    /// <param name="foreignKeyId">外键ID</param>
    /// <param name="foreignKeyName">外键名称</param>
    /// <returns>数据列表</returns>
    [Get("findByForeignKey")]
    Task<RESTfulResultControl<List<T>>> FindByForeignKeyAsync([QueryString]object foreignKeyId, [QueryString]string foreignKeyName = "");

    /// <summary>
    /// 通过外键获取表ID
    /// </summary>
    /// <param name="foreignKeyId">外键ID</param>
    /// <param name="foreignKeyName">外键名称</param>
    /// <returns>ID列表</returns>
    [Get("findIdByForeignKey")]
    Task<RESTfulResultControl<List<string>>> FindIdByForeignKeyAsync([QueryString]object foreignKeyId, [QueryString]string foreignKeyName = "");

    /// <summary>
    /// 根据多个主键获取对象列表
    /// </summary>
    /// <param name="ids">主键数组</param>
    /// <returns>符合条件的对象列表</returns>
    [Get("findByIDs")]
    Task<RESTfulResultControl<List<T>>> FindByIDsAsync([QueryString]object[] ids);

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合
    /// </summary>
    /// <param name="searchInfos">查询的条件</param>
    /// <returns>指定对象的集合</returns>
    [Post("find")]
    Task<RESTfulResultControl<List<T>>> FindAsync([Body]Dictionary<string, string> searchInfos);

    /// <summary>
    /// 返回当前模块所有数据
    /// </summary>
    /// <param name="orderBy">自定义排序语句，如 Name Desc；如不指定，则使用默认排</param>
    /// <returns>指定对象的集合</returns>
    [Get("all")]
    Task<RESTfulResultControl<List<T>>> GetAllAsync([QueryString]string orderBy = "");

    /// <summary>
    /// 返回数据库所有的对象集合(用于分页数据显示)
    /// </summary>
    /// <param name="info">分页实体信息</param>
    /// <returns>指定对象的集合</returns>
    [Get("allByPage")]
    Task<RESTfulResultControl<PageResult<T>>> GetAllAsync([QueryString]PageInput info);

    /// <summary>
    /// 查询数据库,检查是否存在指定键值的对象
    /// </summary>
    /// <param name="fieldName">指定的属性名</param>
    /// <param name="key">指定的值</param>
    /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
    [Post("isExistKey")]
    Task<RESTfulResultControl<bool>> IsExistKeyAsync([QueryString]string fieldName, [QueryString]object key);

    /// <summary>
    /// 根据主键和字段名称，获取对应字段的内容
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    [Get("fieldValue")]
    Task<RESTfulResultControl<string>> GetFieldValueAsync([QueryString]object key, [QueryString]string fieldName);

    /// <summary>
    /// 根据主键和字段名称，获取对应字段的内容
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <param name="fieldNameList">字段名称列表</param>
    /// <returns></returns>
    [Get("fieldValueList")]
    Task<RESTfulResultControl<Dictionary<string, string>>> GetFieldValueListAsync([QueryString]string key, [QueryString]List<string> fieldNameList);

    /// <summary>
    /// 获取字段列表
    /// </summary>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    [Get("fieldList")]
    Task<RESTfulResultControl<List<string>>> GetFieldListAsync([QueryString]string fieldName);

    /// <summary>
    /// 根据指定对象的ID,从数据库中删除指定对象
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [Delete("")]
    Task<RESTfulResultControl<bool>> DeleteAsync([QueryString]object key);

    /// <summary>
    /// 根据指定对象的ID,从数据库中删除指定对象
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [Delete("deleteByIds")]
    Task<RESTfulResultControl<bool>> DeleteByIdsAsync([QueryString]object[] key);

    /// <summary>
    /// 根据条件获取分页实体数据
    /// </summary>
    /// <param name="searchInfos">分页搜索条件</param>
    /// <returns></returns>
    [Post("getEntitiesByPage")]
    Task<RESTfulResultControl<PageResult<T>>> GetEntitiesByPageAsync([Body]PaginatedSearchInfos searchInfos);

    /// <summary>
    /// 通用审批
    /// </summary>
    /// <param name="key">主键</param>
    /// <returns></returns>
    [Post("approve")]
    Task<RESTfulResultControl<bool>> ApproveAsync([QueryString]object key);

    /// <summary>
    /// 获取字段显示权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    [Get("permitDict")]
    Task<RESTfulResultControl<Dictionary<string, int>>> GetPermitDictAsync();

    /// <summary>
    /// 获取表的字段名称和数据类型列表
    /// </summary>
    /// <returns></returns>
    [Get("fieldTypeList")]
    Task<RESTfulResultControl<DataTable>> GetFieldTypeListAsync();

    /// <summary>
    /// 获取字段中文别名（用于界面显示）的字典集合
    /// </summary>
    /// <returns></returns>
    [Get("columnNameAlias")]
    Task<RESTfulResultControl<Dictionary<string, string>>> GetColumnNameAliasAsync();

    /// <summary>
    /// 获取列表显示的字段（用于界面显示）
    /// </summary>
    /// <returns></returns>
    [Post("displayColumns")]
    Task<RESTfulResultControl<string>> GetDisplayColumnsAsync();

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnBaseClientCreating(HttpClient req)
    {
        var builder = new UriBuilder(req.BaseAddress!);
        var path = req.BaseAddress!.AbsolutePath;
        var name = typeof(T).Name.Replace("Info", string.Empty).Replace("Entity", string.Empty);
        builder.Path = $"{path}{string.Concat(name.First().ToString().ToLower(), name.AsSpan(1))}/";
        req.BaseAddress = builder.Uri;
    }

    /// <summary>
    /// 请求之前拦截
    /// </summary>
    /// <param name="client"></param>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Request)]
    static async void OnBaseRequest(HttpClient client, HttpRequestMessage req)
    {
        // var builder = new UriBuilder(, UriKind.RelativeOrAbsolute);
        // var path = req.BaseAddress!.AbsolutePath;
        // var name = typeof(T).Name.Replace("Info", string.Empty).Replace("Entity", string.Empty);
        // builder.Path = $"{path}{string.Concat(name.First().ToString().ToLower(), name.AsSpan(1))}/";
        // req.BaseAddress = builder.Uri;

        var token = await Cache.Instance.GetAsync<string>("access-token");
        if (!token.IsNullOrEmpty())
        {
            req.Headers.Add("Authorization", $"Bearer {token}");
            var xtoken = await Cache.Instance.GetAsync<string>("x-access-token");
            req.Headers.Add("X-Authorization", $"Bearer {xtoken}");
        }
    }

    /// <summary>
    /// 请求成功拦截
    /// </summary>
    /// <param name="client"></param>
    /// <param name="res"></param>
    [Interceptor(InterceptorTypes.Response)]
    static async void OnBaseResponsing(HttpClient client, HttpResponseMessage res)
    {
        if ((int)res.StatusCode >= 400)
        {
            throw Oops.Bah("请求不成功");
        }
        if (res.Headers.TryGetValues("access-token", out var token))
        {
            var enumerable = token.ToList();
            if (enumerable.Any())
            {
                // token 的默认过期时间是 20 分钟，缓存不设过期，因为续期需要原始 token
                await Cache.Instance.SetAsync("access-token", enumerable.First());
            }
        }
        if (res.Headers.TryGetValues("x-access-token", out var xtoken))
        {
            var enumerable = xtoken.ToList();
            if (enumerable.Any())
            {
                var st = JWTEncryption.SecurityReadJwtToken(enumerable.First());
                // token 的默认过期时间是 30 天
                await Cache.Instance.SetAsync("x-access-token", enumerable.First(), null, null, st.ValidTo);
            }
        }
        // // 读取流内容
        // var stream = await res.Content.ReadAsStreamAsync();
        //
        // var text = await new StreamReader(stream).ReadToEndAsync();
        // // 释放流
        // await stream.DisposeAsync();
        //
        // // 如果字符串为空，则返回默认值
        // if (string.IsNullOrWhiteSpace(text)) return;
        //
        // // 解析 Json 序列化提供器
        // var jsonSerializer = App.GetService(typeof(SystemTextJsonSerializerProvider), App.RootServices) as IJsonSerializerProvider;
        //
        // // 反序列化流
        // var result = jsonSerializer.Deserialize<RESTfulResult<object>>(text);
        //
        // using var ms = new MemoryStream();
        // DataContractSerializer dataContractSerializer = new(typeof(object));
        // dataContractSerializer.WriteObject(ms, result.Data);
        // ms.Position = 0;
        // res.Content = new StreamContent(ms, (int)ms.Length);
    }

    /// <summary>
    /// 请求异常拦截
    /// </summary>
    /// <param name="client"></param>
    /// <param name="res"></param>
    /// <param name="errors"></param>
    [Interceptor(InterceptorTypes.Exception)]
    static void OnBaseException(HttpClient client, HttpResponseMessage res, string errors)
    {
        throw Oops.Bah(errors);
    }
}