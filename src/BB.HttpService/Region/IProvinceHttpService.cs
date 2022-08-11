using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpService.Base;
using Furion.UnifyResult;

namespace BB.HttpService.Region;

public interface IProvinceHttpService : IBaseHttpService<ProvinceInfo>
{
    /// <summary>
    /// 根据省份ID获取名称
    /// </summary>
    /// <param name="id">省份ID</param>
    /// <returns></returns>
    Task<RESTfulResult<string>> GetNameByIdAsync([Required] int id);

    /// <summary>
    /// 根据名称获取对应的记录ID
    /// </summary>
    /// <param name="name">省份名称</param>
    /// <returns></returns>
    Task<RESTfulResult<string>> GetIdByNameAsync([Required] string name);
}