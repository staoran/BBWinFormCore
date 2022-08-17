using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpServices.Base;
using Furion.UnifyResult;

namespace BB.HttpServices.Core.Region;

public interface IRegionHttpService : IBaseHttpService<RegionInfo>
{
    /// <summary>
    /// 根据父级ID获取下级区域
    /// </summary>
    /// <param name="parentId">父级ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<RegionInfo>>> GetRegionsByParentIdAsync([Required] long parentId);

    /// <summary>
    /// 获取所有省
    /// </summary>
    /// <returns></returns>
    Task<RESTfulResult<List<RegionInfo>>> GetAllProvinceAsync();

    /// <summary>
    /// 获取所有市
    /// </summary>
    /// <returns></returns>
    Task<RESTfulResult<List<RegionInfo>>> GetAllCityAsync();

    /// <summary>
    /// 获取所有区
    /// </summary>
    /// <returns></returns>
    Task<RESTfulResult<List<RegionInfo>>> GetAllDistrictAsync();

    /// <summary>
    /// 获取所有行政区
    /// </summary>
    /// <returns></returns>
    Task<RESTfulResult<List<RegionInfo>>> GetAllRegionAsync();
}