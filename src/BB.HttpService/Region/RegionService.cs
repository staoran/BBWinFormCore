﻿using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpService.Base;

namespace BB.HttpService.Region;

/// <summary>
/// 行政区域
/// </summary>
public class RegionHttpService : BaseHttpService<RegionInfo>
{
    private readonly IRegionHttpService _httpService;

    public RegionHttpService(IRegionHttpService httpService) : base(httpService)
    {
        _httpService = httpService;
    }
    
    /// <summary>
    /// 根据父级ID获取下级区域
    /// </summary>
    /// <param name="parentId">父级ID</param>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetRegionsByParentIdAsync([Required] long parentId)
    {
        return (await _httpService.GetRegionsByParentIdAsync(parentId)).Data;
    }

    /// <summary>
    /// 获取所有省
    /// </summary>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetAllProvinceAsync()
    {
        return (await _httpService.GetAllProvinceAsync()).Data;
    }

    /// <summary>
    /// 获取所有市
    /// </summary>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetAllCityAsync()
    {
        return (await _httpService.GetAllCityAsync()).Data;
    }

    /// <summary>
    /// 获取所有区
    /// </summary>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetAllDistrictAsync()
    {
        return (await _httpService.GetAllDistrictAsync()).Data;
    }

    /// <summary>
    /// 获取所有行政区
    /// </summary>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetAllRegionAsync()
    {
        return (await _httpService.GetAllRegionAsync()).Data;
    }
}