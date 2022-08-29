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
    Task<RESTfulResult<List<CityInfo>>> GetCitysByProvinceId([Required] string provinceId);

    /// <summary>
    /// 根据省份名称获取对应的城市列表
    /// </summary>
    /// <param name="provinceName">省份名称</param>
    /// <returns></returns>
    Task<RESTfulResult<List<CityInfo>>> GetCitysByProvinceNameAsync([Required] string provinceName);

    /// <summary>
    /// 根据城市ID获取名称
    /// </summary>
    /// <param name="id">城市ID</param>
    /// <returns></returns>
    Task<RESTfulResult<string>> GetNameByIdAsync([Required] int id);

    /// <summary>
    /// 根据名称获取对应的记录ID
    /// </summary>
    /// <param name="name">城市名称</param>
    /// <returns></returns>
    Task<RESTfulResult<string>> GetIdByNameAsync([Required] string name);
}