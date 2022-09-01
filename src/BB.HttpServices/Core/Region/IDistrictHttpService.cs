using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.Core.Region;

public interface IDistrictHttpService : IHttpDispatchProxy, IBaseHttpService<DistrictInfo>
{
    /// <summary>
    /// 根据城市ID获取对应的地区列表
    /// </summary>
    /// <param name="cityId">城市ID</param>
    /// <returns></returns>
    [Get("districtByCity")]
    Task<RESTfulResultControl<List<DistrictInfo>>> GetDistrictByCityAsync([QueryString][Required] string cityId);

    /// <summary>
    /// 根据城市名获取对应的行政区划
    /// </summary>
    /// <param name="cityName">城市名</param>
    /// <returns></returns>
    [Get("districtByCityName")]
    Task<RESTfulResultControl<List<DistrictInfo>>> GetDistrictByCityNameAsync([QueryString][Required] string cityName);

    /// <summary>
    /// 根据行政区ID获取名称
    /// </summary>
    /// <param name="id">行政区ID</param>
    /// <returns></returns>
    [Get("nameById")]
    Task<RESTfulResultControl<string>> GetNameByIdAsync([QueryString][Required] int id);

    /// <summary>
    /// 根据名称获取对应的记录ID
    /// </summary>
    /// <param name="name">行政区名称</param>
    /// <returns></returns>
    [Get("idByName")]
    Task<RESTfulResultControl<string>> GetIdByNameAsync([QueryString][Required] string name);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}district/";
        // req.BaseAddress = builder.Uri;
    }
}