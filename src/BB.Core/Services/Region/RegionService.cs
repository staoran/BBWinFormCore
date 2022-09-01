using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Dictionary;
using FluentValidation;

namespace BB.Core.Services.Region;

/// <summary>
/// 行政区域
/// </summary>
[ApiDescriptionSettings("基础资料")]
public class RegionService : BaseService<RegionInfo>, IDynamicApiController, ITransient
{
    public RegionService(BaseRepository<RegionInfo> repository, IValidator<RegionInfo> validator) : base(repository, validator)
    {
    }
    
    /// <summary>
    /// 根据父级ID获取下级区域
    /// </summary>
    /// <param name="parentId">父级ID</param>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetRegionsByParentIdAsync([Required] long parentId)
    {
        var sql = $"select Id, Name, ParentId, FullName from TB_Region where IsDeleted = 0 and ParentId = '{parentId}'";
        return await Repository.GetListAsync(sql);
    }

    /// <summary>
    /// 获取所有省
    /// </summary>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetAllProvinceAsync()
    {
        var sql = $"select Id, Name, ParentId, FullName from TB_Region where IsDeleted = 0 and Type = 1";
        return await Repository.GetListAsync(sql);
    }

    /// <summary>
    /// 获取所有市
    /// </summary>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetAllCityAsync()
    {
        var sql = $"select Id, Name, ParentId, FullName from TB_Region where IsDeleted = 0 and Type = 2";
        return await Repository.GetListAsync(sql);
    }

    /// <summary>
    /// 获取所有区
    /// </summary>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetAllDistrictAsync()
    {
        var sql = $"select Id, Name, ParentId, FullName  from TB_Region where IsDeleted = 0 and Type = 3";
        return await Repository.GetListAsync(sql);
    }

    /// <summary>
    /// 获取所有行政区
    /// </summary>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetAllRegionAsync()
    {
        var sql = $"select Id, Name, ParentId, FullName, Type from TB_Region where IsDeleted = 0";
        return await Repository.GetListAsync(sql);
    }
}