using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Dictionary;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Core.Services.Region;

/// <summary>
/// 中国省份业务对象类
/// </summary>
[ApiDescriptionSettings("基础资料")]
public class ProvinceService : BaseService<ProvinceInfo>, IDynamicApiController, ITransient
{
    public ProvinceService(BaseRepository<ProvinceInfo> repository, IValidator<ProvinceInfo> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 根据省份ID获取名称
    /// </summary>
    /// <param name="id">省份ID</param>
    /// <returns></returns>
    public async Task<string> GetNameByIdAsync([Required] int id)
    {
        return await GetFieldValueAsync(id, "ProvinceName");
    }

    /// <summary>
    /// 根据名称获取对应的记录ID
    /// </summary>
    /// <param name="name">省份名称</param>
    /// <returns></returns>
    public async Task<string> GetIdByNameAsync([Required] string name)
    {
        string result = "";
        var list = await Repository.GetFieldListByExpressionAsync(x => x.ID, x => x.ProvinceName == name);
        if (list is { Count: > 0 })
        {
            result = list[0].ObjToStr();
        }
        return result;
    }
}