using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Dictionary;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.Core.Services.Dict;

/// <summary>
/// 字典类型对象
/// </summary>
[ApiDescriptionSettings("基础资料")]
public class DictTypeService : BaseService<DictTypeInfo>, IDynamicApiController, ITransient
{
    public DictTypeService(BaseRepository<DictTypeInfo> repository, IValidator<DictTypeInfo> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 获取所有字典类型的列表集合(Key为名称，Value为ID值）
    /// </summary>
    /// <param name="dictTypeId">字典类型ID</param>
    /// <returns></returns>
    public async Task<Dictionary<string, string>> GetAllTypeAsync(string dictTypeId = "")
    {
        return (await Repository.AsQueryable()
            .WhereIF(ExpressionExtensions.IsNullOrEmpty(dictTypeId), x => x.PID == dictTypeId)
            .Select(x => new { x.Name, x.ID })
            .OrderBy($"{Repository.SortField} {(Repository.IsDescending ? "desc" : "asc")}")
            .ToListAsync())
            .ToDictionary(x=>x.Name,x=>x.ID);;
    }

    /// <summary>
    /// 判断是否重复，如果重复返回True，否则为False
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="id">编号</param>
    /// <returns></returns>
    [HttpGet]
    public async Task<bool> CheckDuplicatedAsync(string name, string id)
    {
        return await Repository.IsAnyAsync(x => x.Name == name && x.ID != id);
    }

    /// <summary>
    /// 获取字典类型的树形结构列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<DictTypeNodeInfo>> GetTreeAsync()
    {
        List<DictTypeNodeInfo> typeNodeList = new();

        List<DictTypeInfo> dictTypeInfos = await Repository.AsQueryable()
            .OrderBy(x => x.PID)
            .OrderBy(x => x.SEQ)
            // .WithCache(1800) // 缓存30分钟
            .ToListAsync();

        if (dictTypeInfos.Count > 0)
        {
            List<DictTypeInfo> roots = dictTypeInfos.Where(x=>x.PID == "-1").ToList();
            roots.ForEach(x =>
            {
                DictTypeNodeInfo dictTypeNodeInfo = GetNode(x.ID, dictTypeInfos);
                typeNodeList.Add(dictTypeNodeInfo);
            });
        }
        
        return typeNodeList;
    }

    /// <summary>
    /// 获取所有字典类型的终结点数据
    /// </summary>
    /// <returns></returns>
    public async Task<List<CListItem>> GetEndpointItemsAsync(List<DictTypeNodeInfo> nodeInfos = null, List<CListItem> items = null)
    {
        nodeInfos ??= await GetTreeAsync();
        items ??= new List<CListItem>();

        foreach (DictTypeNodeInfo x in nodeInfos)
        {
            if (x.Children.Count == 0)
            {
                items.Add(new CListItem(x.ID, x.Name));
            }
            else
            {
                await GetEndpointItemsAsync(x.Children, items);
            }
        }
        
        return items;
    }

    /// <summary>
    /// 获取字典类型顶级的列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<DictTypeInfo>> GetTopItemsAsync()
    {
        return await Repository.AsQueryable()
            .Where(x => x.PID == "-1")
            .OrderBy(x => x.SEQ)
            .ToListAsync();
    }

    /// <summary>
    /// 获取指定ID下的树形结构列表
    /// </summary>
    /// <param name="mainId">字典类型ID</param>
    /// <returns></returns>
    public async Task<List<DictTypeNodeInfo>> GetTreeByIdAsync([Required] string mainId)
    {
        List<DictTypeNodeInfo> typeNodeList = new();

        List<DictTypeInfo> dt = await Repository.AsQueryable()
            .OrderBy(x => x.PID)
            .OrderBy(x => x.SEQ)
            // .WithCache(1800) // 缓存30分钟
            .ToListAsync();

        if (dt.Count > 0)
        {
            var roots = dt.Where(x=>x.PID == mainId).ToList();
            roots.ForEach(x =>
            {
                DictTypeNodeInfo dictTypeNodeInfo = GetNode(x.ID, dt);
                typeNodeList.Add(dictTypeNodeInfo);
            });
        }

        return typeNodeList;
    }

    /// <summary>
    /// 插入指定对象到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    public override async Task<bool> InsertAsync(DictTypeInfo obj)
    {
        if (!ExpressionExtensions.IsNullOrEmpty(obj.Code))
            await CheckUniqueAsync(DictTypeInfo.FieldCode, "字典代码", obj.Code);
        return await base.InsertAsync(obj);
    }

    private DictTypeNodeInfo GetNode(string id, List<DictTypeInfo> dt)
    {
        DictTypeInfo dictTypeInfo = dt.Find(x => x.ID == id);
        DictTypeNodeInfo dictTypeNodeInfo = new(dictTypeInfo);

        List<DictTypeInfo> roots = dt.Where(x => x.PID == id).ToList();
        roots.ForEach(x =>
        {
            DictTypeNodeInfo childNodeInfo = GetNode(x.ID, dt);
            dictTypeNodeInfo.Children.Add(childNodeInfo);
        });

        return dictTypeNodeInfo;
    }
}