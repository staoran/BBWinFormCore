using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpService.Base;
using Furion.UnifyResult;

namespace BB.HttpService.Region;

public interface IDistrictHttpService : IBaseHttpService<DistrictInfo>
{
    /// <summary>
    /// 根据城市ID获取对应的地区列表
    /// </summary>
    /// <param name="cityId">城市ID</param>
    /// <returns></returns>
    Task<RESTfulResult<List<DistrictInfo>>> GetDistrictByCityAsync([Required] string cityId);

    /// <summary>
    /// 根据城市名获取对应的行政区划
    /// </summary>
    /// <param name="cityName">城市名</param>
    /// <returns></returns>
    Task<RESTfulResult<List<DistrictInfo>>> GetDistrictByCityNameAsync([Required] string cityName);

    /// <summary>
    /// 根据行政区ID获取名称
    /// </summary>
    /// <param name="id">行政区ID</param>
    /// <returns></returns>
    Task<RESTfulResult<string>> GetNameByIdAsync([Required] int id);

    /// <summary>
    /// 根据名称获取对应的记录ID
    /// </summary>
    /// <param name="name">行政区名称</param>
    /// <returns></returns>
    Task<RESTfulResult<string>> GetIdByNameAsync([Required] string name);
}