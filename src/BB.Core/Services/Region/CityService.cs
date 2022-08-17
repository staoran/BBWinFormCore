using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Dictionary;
using FluentValidation;

namespace BB.Core.Services.Region;

/// <summary>
/// 城市业务对象类
/// </summary>
public class CityService : BaseService<CityInfo>, IDynamicApiController, ITransient
{
    public CityService(BaseRepository<CityInfo> repository, IValidator<CityInfo> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 根据省份ID获取对应的城市列表
    /// </summary>
    /// <param name="provinceId">省份ID</param>
    /// <returns></returns>
    public async Task<List<CityInfo>> GetCitysByProvinceId([Required] string provinceId)
    {
        var condition = $"ProvinceID ={provinceId} ";
        return await FindAsync(condition);
    }

    /// <summary>
    /// 根据省份名称获取对应的城市列表
    /// </summary>
    /// <param name="provinceName">省份名称</param>
    /// <returns></returns>
    public async Task<List<CityInfo>> GetCitysByProvinceNameAsync([Required] string provinceName)
    {
        var sql = $"Select c.* from TB_City as c inner join TB_Province as p on c.ProvinceId=p.ID where ProvinceName='{provinceName}' ";
        return await Repository.GetListAsync(sql);
    }

    /// <summary>
    /// 根据城市ID获取名称
    /// </summary>
    /// <param name="id">城市ID</param>
    /// <returns></returns>
    public async Task<string> GetNameByIdAsync([Required] int id)
    {
        return await GetFieldValueAsync(id, "CityName");
    }


    /// <summary>
    /// 根据名称获取对应的记录ID
    /// </summary>
    /// <param name="name">城市名称</param>
    /// <returns></returns>
    public async Task<string> GetIdByNameAsync([Required] string name)
    {
        var result = "";
        var condition = $"Name ='{name}'";
        List<string> list = await Repository.GetFieldListByConditionAsync("ID", condition);
        if (list != null && list.Count > 0)
        {
            result = list[0];
        }
        return result;
    }
}