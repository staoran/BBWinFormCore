using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpService.Base;

namespace BB.HttpService.Region;

/// <summary>
/// 城市业务对象类
/// </summary>
public class CityHttpService : BaseHttpService<CityInfo>
{
    private readonly ICityHttpService _httpService;

    public CityHttpService(ICityHttpService httpService) : base(httpService)
    {
        _httpService = httpService;
    }

    /// <summary>
    /// 根据省份ID获取对应的城市列表
    /// </summary>
    /// <param name="provinceId">省份ID</param>
    /// <returns></returns>
    public async Task<List<CityInfo>> GetCitysByProvinceId([Required] string provinceId)
    {
        return (await _httpService.GetCitysByProvinceId(provinceId)).Data;
    }

    /// <summary>
    /// 根据省份名称获取对应的城市列表
    /// </summary>
    /// <param name="provinceName">省份名称</param>
    /// <returns></returns>
    public async Task<List<CityInfo>> GetCitysByProvinceNameAsync([Required] string provinceName)
    {
        return (await _httpService.GetCitysByProvinceNameAsync(provinceName)).Data;
    }

    /// <summary>
    /// 根据城市ID获取名称
    /// </summary>
    /// <param name="id">城市ID</param>
    /// <returns></returns>
    public async Task<string> GetNameByIdAsync([Required] int id)
    {
        return (await _httpService.GetNameByIdAsync(id)).Data;
    }


    /// <summary>
    /// 根据名称获取对应的记录ID
    /// </summary>
    /// <param name="name">城市名称</param>
    /// <returns></returns>
    public async Task<string> GetIdByNameAsync([Required] string name)
    {
        return (await _httpService.GetIdByNameAsync(name)).Data;
    }
}