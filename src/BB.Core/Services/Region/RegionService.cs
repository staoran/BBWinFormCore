using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Dictionary;
using BB.Tools.Extension;
using BB.Tools.Utils;
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
        return await Cache.Instance.GetOrCreateAsync($"GetRegionsByParentIdAsync_{parentId}", async () =>
            await Repository.AsQueryable()
                .Where(x => !x.IsDeleted && x.ParentId == parentId)
                .Select<RegionInfo>(
                    $"{RegionInfo.FieldId},{RegionInfo.FieldName},{RegionInfo.FieldParentId},{RegionInfo.FieldFullName}")
                .ToListAsync());
    }

    /// <summary>
    /// 获取所有省
    /// </summary>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetAllProvinceAsync()
    {
        return await Cache.Instance.GetOrCreateAsync("GetAllProvinceAsync", async () =>
            await Repository.AsQueryable()
                .Where(x => !x.IsDeleted && x.Type == 1)
                .Select<RegionInfo>(
                    $"{RegionInfo.FieldId},{RegionInfo.FieldName},{RegionInfo.FieldParentId},{RegionInfo.FieldFullName}")
                .ToListAsync());
    }

    /// <summary>
    /// 获取所有市
    /// </summary>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetAllCityAsync()
    {
        return await Cache.Instance.GetOrCreateAsync("GetAllCityAsync", async () =>
            await Repository.AsQueryable()
                .Where(x => !x.IsDeleted && x.Type == 2)
                .Select<RegionInfo>(
                    $"{RegionInfo.FieldId},{RegionInfo.FieldName},{RegionInfo.FieldParentId},{RegionInfo.FieldFullName}")
                .ToListAsync());
    }

    /// <summary>
    /// 获取所有区
    /// </summary>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetAllDistrictAsync()
    {
        return await Cache.Instance.GetOrCreateAsync("GetAllDistrictAsync", async () =>
            await Repository.AsQueryable()
                .Where(x => !x.IsDeleted && x.Type == 3)
                .Select<RegionInfo>(
                    $"{RegionInfo.FieldId},{RegionInfo.FieldName},{RegionInfo.FieldParentId},{RegionInfo.FieldFullName}")
                .ToListAsync());
    }

    /// <summary>
    /// 获取所有行政区
    /// </summary>
    /// <returns></returns>
    public async Task<List<RegionInfo>> GetAllRegionAsync()
    {
        return await Cache.Instance.GetOrCreateAsync("GetAllRegionAsync", async () =>
            await Repository.AsQueryable()
                .Where(x => !x.IsDeleted)
                .Select<RegionInfo>(
                    $"{RegionInfo.FieldId},{RegionInfo.FieldName},{RegionInfo.FieldParentId},{RegionInfo.FieldFullName},{RegionInfo.FieldType}")
                .ToListAsync());
    }
}