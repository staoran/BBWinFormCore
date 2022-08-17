using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Dictionary;
using FluentValidation;

namespace BB.Core.Services.Region;

/// <summary>
/// 地区业务类
/// </summary>
public class DistrictService : BaseService<DistrictInfo>, IDynamicApiController, ITransient
{
    public DistrictService(BaseRepository<DistrictInfo> repository, IValidator<DistrictInfo> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 根据城市ID获取对应的地区列表
    /// </summary>
    /// <param name="cityId">城市ID</param>
    /// <returns></returns>
     public async Task<List<DistrictInfo>> GetDistrictByCityAsync([Required] string cityId)
    {
        var condition = $"CityID={cityId}";
        return await FindAsync(condition);
    }

    /// <summary>
    /// 根据城市名获取对应的行政区划
    /// </summary>
    /// <param name="cityName">城市名</param>
    /// <returns></returns>
     public async Task<List<DistrictInfo>> GetDistrictByCityNameAsync([Required] string cityName)
    {
        var sql = $"Select c.* from TB_District as c inner join TB_City as p on c.CityID=p.ID where CityName='{cityName}' ";
        return await Repository.GetListAsync(sql);
    }

    /// <summary>
    /// 根据行政区ID获取名称
    /// </summary>
    /// <param name="id">行政区ID</param>
    /// <returns></returns>
    public async Task<string> GetNameByIdAsync([Required] int id)
    {
        return await GetFieldValueAsync(id, "DistrictName");
    }

    /// <summary>
    /// 根据名称获取对应的记录ID
    /// </summary>
    /// <param name="name">行政区名称</param>
    /// <returns></returns>
    public async Task<string> GetIdByNameAsync([Required] string name)
    {
        var result = "";
        var condition = $"Name ='{name}'";
        List<string> list = await Repository.GetFieldListByConditionAsync("ID", condition);
        if (list is { Count: > 0 })
        {
            result = list[0];
        }
        return result;
    }
}