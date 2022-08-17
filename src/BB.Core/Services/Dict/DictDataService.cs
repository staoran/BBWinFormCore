using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Dictionary;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.Core.Services.Dict;

/// <summary>
/// 字典数据对象
/// </summary>
public class DictDataService : BaseService<DictDataInfo>, IDynamicApiController, ITransient
{
    public DictDataService(BaseRepository<DictDataInfo> repository, IValidator<DictDataInfo> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 根据字典类型ID获取所有该类型的字典列表集合
    /// </summary>
    /// <param name="dictTypeId">字典类型ID</param>
    /// <returns></returns>
    [ApiDescriptionSettings(KeepVerb = true)]
    public async Task<List<DictDataInfo>> FindByTypeIdAsync(string dictTypeId)
    {
        return await FindAsync(x => x.DictTypeId == dictTypeId);
    }

    /// <summary>
    /// 根据字典类型名称获取所有该类型的字典列表集合
    /// </summary>
    /// <param name="dictTypeName">字典类型名称</param>
    /// <returns></returns>
    [ApiDescriptionSettings(KeepVerb = true)]
    public async Task<List<DictDataInfo>> FindByDictTypeAsync(string dictTypeName)
    {
        string sql =
            $"select d.* from tb_DictData d inner join tb_DictType t on d.DictType_ID = t.ID where t.Name ='{dictTypeName}'";
        sql += $" Order by d.{Repository.SortField} {(Repository.IsDescending ? "DESC" : "ASC")}";

        return await FindAsync(sql);
    }

    /// <summary>
    /// 根据字典类型代码获取所有该类型的字典列表集合
    /// </summary>
    /// <param name="dictCode">字典类型代码</param>
    /// <returns></returns>
    [ApiDescriptionSettings(KeepVerb = true)]
    public async Task<List<DictDataInfo>> FindByDictCodeAsync(string dictCode)
    {
        return await Repository.Db.Queryable<DictDataInfo, DictTypeInfo>((dd, dt) => dd.ID == dt.ID)
            .Where((_, dt) => dt.Code == dictCode)
            .Select<DictDataInfo>()
            .ToListAsync();
    }
        
    /// <summary>
    /// 获取所有的字典列表集合(Key为名称，Value为值）
    /// </summary>
    /// <returns></returns>
    public async Task<Dictionary<string, string>> GetAllDictAsync()
    {
        string sql =
            $"select d.Name,d.Value from tb_DictData d inner join tb_DictType t on d.DictType_ID = t.ID order by d.{Repository.SortField} {(Repository.IsDescending ? "DESC" : "ASC")}";

        return await GetDictBySqlAsync(sql);
    }

    /// <summary>
    /// 根据字典类型ID获取所有该类型的字典列表集合(Key为名称，Value为值）
    /// </summary>
    /// <param name="dictTypeId">字典类型ID</param>
    /// <returns></returns>
    public async Task<Dictionary<string, string>> GetDictByTypeIdAsync(string dictTypeId)
    {
        string sql =
            $"select d.Name,d.Value from tb_DictData d where d.DictType_ID ='{dictTypeId}' order by d.{Repository.SortField} {(Repository.IsDescending ? "DESC" : "ASC")}";

        return await GetDictBySqlAsync(sql);
    }

    /// <summary>
    /// 根据字典类型名称获取所有该类型的字典列表集合(Key为名称，Value为值）
    /// </summary>
    /// <param name="dictTypeName">字典类型名称</param>
    /// <returns></returns>
    public async Task<Dictionary<string, string>> GetDictByDictTypeAsync(string dictTypeName)
    {
        string sql =
            $"select d.Name,d.Value from tb_DictData d inner join tb_DictType t on d.DictType_ID = t.ID where t.Name ='{dictTypeName}' order by d.{Repository.SortField} {(Repository.IsDescending ? "DESC" : "ASC")}";

        return await GetDictBySqlAsync(sql);
    }

    private async Task<Dictionary<string, string>> GetDictBySqlAsync(string sql)
    {
        List<DictDataInfo> dics = await FindAsync(sql);
        Dictionary<string, string> dict = new();
        dics.ForEach(x => { dict.Add(x.Value, x.Name); });
        return dict;
    }

    /// <summary>
    /// 根据字典类型获取对应的CListItem集合
    /// </summary>
    /// <param name="dictTypeName"></param>
    /// <returns></returns>
    public async Task<List<CListItem>> GetDictListItemByDictTypeAsync(string dictTypeName)
    {
        List<CListItem> itemList = new();
        Dictionary<string, string> dict = await GetDictByDictTypeAsync(dictTypeName);
        foreach (string key in dict.Keys)
        {
            itemList.Add(new CListItem(dict[key], key));
        }
        return itemList;
    }
                
    /// <summary>
    /// 根据字典类型名称和字典Value值（即字典编码），解析成字典对应的名称
    /// </summary>
    /// <param name="dictTypeName">字典类型名称</param>
    /// <param name="dictValue">字典Value值，即字典编码</param>
    /// <returns>字典对应的名称</returns>
    public async Task<string> GetDictNameAsync(string dictTypeName, string dictValue)
    {
        string sql =
            $"select d.Name from tb_DictData d inner join tb_DictType t on d.DictType_ID = t.ID where t.Name ='{dictTypeName}' and d.Value='{dictValue}'";

        string value = await SqlValueListAsync(sql);
        return value.IsNullOrEmpty() ? "" : value.Split(',')[0];
    }
}