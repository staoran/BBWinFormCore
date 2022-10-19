using System.ComponentModel.DataAnnotations;
using BB.Entity.Dictionary;
using BB.HttpServices.Base;
using BB.Tools.Entity;

namespace BB.HttpServices.Core.Dict;

/// <summary>
/// 字典类型对象
/// </summary>
public class DictTypeHttpService : BaseHttpService<DictTypeInfo>
{
    private readonly IDictTypeHttpService _httpService;

    public DictTypeHttpService(IDictTypeHttpService httpService) : base(httpService)
    {
        _httpService = httpService;
    }

    /// <summary>
    /// 获取所有字典类型的列表集合(Key为名称，Value为ID值）
    /// </summary>
    /// <param name="dictTypeId">字典类型ID</param>
    /// <returns></returns>
    public async Task<Dictionary<string, string>> GetAllTypeAsync(int dictTypeId = -1)
    {
        return (await _httpService.GetAllTypeAsync(dictTypeId)).Handling();
    }

    /// <summary>
    /// 判断是否重复，如果重复返回True，否则为False
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="id">编号</param>
    /// <returns></returns>
    public async Task<bool> CheckDuplicatedAsync(string name, int id)
    {
        return (await _httpService.CheckDuplicatedAsync(name, id)).Handling();
    }

    /// <summary>
    /// 获取字典类型的树形结构列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<DictTypeNodeInfo>> GetTreeAsync()
    {
        return (await _httpService.GetTreeAsync()).Handling();
    }

    /// <summary>
    /// 获取所有字典类型的终结点数据
    /// </summary>
    /// <returns></returns>
    public async Task<List<CListItem>> GetEndpointItemsAsync(List<DictTypeNodeInfo>? nodeInfos = null, List<CListItem>? items = null)
    {
        return (await _httpService.GetEndpointItemsAsync(nodeInfos, items)).Handling();
    }

    /// <summary>
    /// 获取字典类型顶级的列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<DictTypeInfo>> GetTopItemsAsync()
    {
        return (await _httpService.GetTopItemsAsync()).Handling();
    }

    /// <summary>
    /// 获取指定ID下的树形结构列表
    /// </summary>
    /// <param name="mainId">字典类型ID</param>
    /// <returns></returns>
    public async Task<List<DictTypeNodeInfo>> GetTreeByIdAsync([Required] int mainId)
    {
        return (await _httpService.GetTreeByIdAsync(mainId)).Handling();
    }
}