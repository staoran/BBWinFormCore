using BB.Entity.Dictionary;
using BB.HttpServices.Base;
using BB.Tools.Entity;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.Dict;

public interface IDictDataHttpService : IHttpDispatchProxy, IBaseHttpService<DictDataInfo>
{
    /// <summary>
    /// 根据字典类型ID获取所有该类型的字典列表集合
    /// </summary>
    /// <param name="dictTypeId">字典类型ID</param>
    /// <returns></returns>
    [Get("findByTypeId")]
    Task<RESTfulResultControl<List<DictDataInfo>>> FindByTypeIdAsync([QueryString]string dictTypeId);

    /// <summary>
    /// 根据字典类型名称获取所有该类型的字典列表集合
    /// </summary>
    /// <param name="dictTypeName">字典类型名称</param>
    /// <returns></returns>
    [Get("findByDictType")]
    Task<RESTfulResultControl<List<DictDataInfo>>> FindByDictTypeAsync([QueryString]string dictTypeName);

    /// <summary>
    /// 根据字典类型代码获取所有该类型的字典列表集合
    /// </summary>
    /// <param name="dictCode">字典类型代码</param>
    /// <returns></returns>
    [Get("findByDictCode")]
    Task<RESTfulResultControl<List<DictDataInfo>>> FindByDictCodeAsync([QueryString]string dictCode);

    /// <summary>
    /// 获取所有的字典列表集合(Key为名称，Value为值）
    /// </summary>
    /// <returns></returns>
    [Get("allDict")]
    Task<RESTfulResultControl<Dictionary<string, string>>> GetAllDictAsync();

    /// <summary>
    /// 根据字典类型ID获取所有该类型的字典列表集合(Key为名称，Value为值）
    /// </summary>
    /// <param name="dictTypeId">字典类型ID</param>
    /// <returns></returns>
    [Get("dictByTypeId")]
    Task<RESTfulResultControl<Dictionary<string, string>>> GetDictByTypeIdAsync([QueryString]string dictTypeId);

    /// <summary>
    /// 根据字典类型名称获取所有该类型的字典列表集合(Key为名称，Value为值）
    /// </summary>
    /// <param name="dictTypeName">字典类型名称</param>
    /// <returns></returns>
    [Get("dictByDictType")]
    Task<RESTfulResultControl<Dictionary<string, string>>> GetDictByDictTypeAsync([QueryString]string dictTypeName);

    /// <summary>
    /// 根据字典类型获取对应的CListItem集合
    /// </summary>
    /// <param name="dictTypeName"></param>
    /// <returns></returns>
    [Get("dictListItemByDictType")]
    Task<RESTfulResultControl<List<CListItem>>> GetDictListItemByDictTypeAsync([QueryString]string dictTypeName);

    /// <summary>
    /// 根据字典类型名称和字典Value值（即字典编码），解析成字典对应的名称
    /// </summary>
    /// <param name="dictTypeName">字典类型名称</param>
    /// <param name="dictValue">字典Value值，即字典编码</param>
    /// <returns>字典对应的名称</returns>
    [Get("dictName")]
    Task<RESTfulResultControl<string>> GetDictNameAsync([QueryString]string dictTypeName, [QueryString]string dictValue);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}dictData/";
        // req.BaseAddress = builder.Uri;
    }
}