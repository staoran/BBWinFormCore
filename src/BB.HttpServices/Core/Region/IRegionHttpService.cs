using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpServices.Base;
using Furion.RemoteRequest;
using Furion.UnifyResult;

namespace BB.HttpServices.Core.Region;

public interface IRegionHttpService : IHttpDispatchProxy, IBaseHttpService<RegionInfo>
{
    /// <summary>
    /// 根据父级ID获取下级区域
    /// </summary>
    /// <param name="parentId">父级ID</param>
    /// <returns></returns>
    [Get("regionsByParentId")]
    Task<RESTfulResult<List<RegionInfo>>> GetRegionsByParentIdAsync([Required] long parentId);

    /// <summary>
    /// 获取所有省
    /// </summary>
    /// <returns></returns>
    [Get("allProvince")]
    Task<RESTfulResult<List<RegionInfo>>> GetAllProvinceAsync();

    /// <summary>
    /// 获取所有市
    /// </summary>
    /// <returns></returns>
    [Get("allCity")]
    Task<RESTfulResult<List<RegionInfo>>> GetAllCityAsync();

    /// <summary>
    /// 获取所有区
    /// </summary>
    /// <returns></returns>
    [Get("allDistrict")]
    Task<RESTfulResult<List<RegionInfo>>> GetAllDistrictAsync();

    /// <summary>
    /// 获取所有行政区
    /// </summary>
    /// <returns></returns>
    [Get("allRegion")]
    Task<RESTfulResult<List<RegionInfo>>> GetAllRegionAsync();

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        var builder = new UriBuilder(req.BaseAddress!);
        var path = req.BaseAddress!.AbsolutePath;
        builder.Path = $"{path}region/";
        req.BaseAddress = builder.Uri;
    }
}