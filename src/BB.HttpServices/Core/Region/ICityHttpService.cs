using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpServices.Base;
using Furion.RemoteRequest;
using Furion.UnifyResult;

namespace BB.HttpServices.Core.Region;

public interface ICityHttpService : IHttpDispatchProxy, IBaseHttpService<CityInfo>
{
    /// <summary>
    /// 根据省份ID获取对应的城市列表
    /// </summary>
    /// <param name="provinceId">省份ID</param>
    /// <returns></returns>
    [Get("citysByProvinceId")]
    Task<RESTfulResult<List<CityInfo>>> GetCitysByProvinceId([Required] string provinceId);

    /// <summary>
    /// 根据省份名称获取对应的城市列表
    /// </summary>
    /// <param name="provinceName">省份名称</param>
    /// <returns></returns>
    [Get("citysByProvinceName")]
    Task<RESTfulResult<List<CityInfo>>> GetCitysByProvinceNameAsync([Required] string provinceName);

    /// <summary>
    /// 根据城市ID获取名称
    /// </summary>
    /// <param name="id">城市ID</param>
    /// <returns></returns>
    [Get("nameById")]
    Task<RESTfulResult<string>> GetNameByIdAsync([Required] int id);

    /// <summary>
    /// 根据名称获取对应的记录ID
    /// </summary>
    /// <param name="name">城市名称</param>
    /// <returns></returns>
    [Get("idByName")]
    Task<RESTfulResult<string>> GetIdByNameAsync([Required] string name);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        var builder = new UriBuilder(req.BaseAddress!);
        var path = req.BaseAddress!.AbsolutePath;
        builder.Path = $"{path}city/";
        req.BaseAddress = builder.Uri;
    }
}