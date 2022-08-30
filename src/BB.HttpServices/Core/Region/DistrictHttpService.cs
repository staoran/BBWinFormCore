using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpServices.Base;

namespace BB.HttpServices.Core.Region;

/// <summary>
/// 地区业务类
/// </summary>
public class DistrictHttpService : BaseHttpService<DistrictInfo>
{
    private readonly IDistrictHttpService _httpService;

    public DistrictHttpService(IDistrictHttpService httpService) : base(httpService)
    {
        _httpService = httpService;
    }

    /// <summary>
    /// 根据城市ID获取对应的地区列表
    /// </summary>
    /// <param name="cityId">城市ID</param>
    /// <returns></returns>
     public async Task<List<DistrictInfo>> GetDistrictByCityAsync([Required] string cityId)
    {
        return (await _httpService.GetDistrictByCityAsync(cityId)).Handling();
    }

    /// <summary>
    /// 根据城市名获取对应的行政区划
    /// </summary>
    /// <param name="cityName">城市名</param>
    /// <returns></returns>
     public async Task<List<DistrictInfo>> GetDistrictByCityNameAsync([Required] string cityName)
    {
        return (await _httpService.GetDistrictByCityNameAsync(cityName)).Handling();
    }

    /// <summary>
    /// 根据行政区ID获取名称
    /// </summary>
    /// <param name="id">行政区ID</param>
    /// <returns></returns>
    public async Task<string> GetNameByIdAsync([Required] int id)
    {
        return (await _httpService.GetNameByIdAsync(id)).Handling();
    }

    /// <summary>
    /// 根据名称获取对应的记录ID
    /// </summary>
    /// <param name="name">行政区名称</param>
    /// <returns></returns>
    public async Task<string> GetIdByNameAsync([Required] string name)
    {
        return (await _httpService.GetIdByNameAsync(name)).Handling();
    }
}