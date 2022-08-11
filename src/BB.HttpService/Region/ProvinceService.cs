using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpService.Base;

namespace BB.HttpService.Region;

/// <summary>
/// 中国省份业务对象类
/// </summary>
public class ProvinceHttpService : BaseHttpService<ProvinceInfo>
{
    private readonly IProvinceHttpService _httpService;

    public ProvinceHttpService(IProvinceHttpService httpService) : base(httpService)
    {
        _httpService = httpService;
    }

    /// <summary>
    /// 根据省份ID获取名称
    /// </summary>
    /// <param name="id">省份ID</param>
    /// <returns></returns>
    public async Task<string> GetNameByIdAsync([Required] int id)
    {
        return (await _httpService.GetNameByIdAsync(id)).Data;
    }

    /// <summary>
    /// 根据名称获取对应的记录ID
    /// </summary>
    /// <param name="name">省份名称</param>
    /// <returns></returns>
    public async Task<string> GetIdByNameAsync([Required] string name)
    {
        return (await _httpService.GetIdByNameAsync(name)).Data;
    }
}