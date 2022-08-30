using BB.Entity.Dictionary;
using BB.HttpServices.Base;
using BB.Tools.Entity;

namespace BB.HttpServices.Core.Dict;

/// <summary>
/// 字典数据对象
/// </summary>
public class DictDataHttpService : BaseHttpService<DictDataInfo>
{
    private readonly IDictDataHttpService _httpService;

    public DictDataHttpService(IDictDataHttpService httpService) : base(httpService)
    {
        _httpService = httpService;
    }

    /// <summary>
    /// 根据字典类型ID获取所有该类型的字典列表集合
    /// </summary>
    /// <param name="dictTypeId">字典类型ID</param>
    /// <returns></returns>
    public async Task<List<DictDataInfo>> FindByTypeIdAsync(string dictTypeId)
    {
        return (await _httpService.FindByTypeIdAsync(dictTypeId)).Handling();
    }

    /// <summary>
    /// 根据字典类型名称获取所有该类型的字典列表集合
    /// </summary>
    /// <param name="dictTypeName">字典类型名称</param>
    /// <returns></returns>
    public async Task<List<DictDataInfo>> FindByDictTypeAsync(string dictTypeName)
    {
        return (await _httpService.FindByDictTypeAsync(dictTypeName)).Handling();
    }

    /// <summary>
    /// 根据字典类型代码获取所有该类型的字典列表集合
    /// </summary>
    /// <param name="dictCode">字典类型代码</param>
    /// <returns></returns>
    public async Task<List<DictDataInfo>> FindByDictCodeAsync(string dictCode)
    {
        return (await _httpService.FindByDictCodeAsync(dictCode)).Handling();
    }
        
    /// <summary>
    /// 获取所有的字典列表集合(Key为名称，Value为值）
    /// </summary>
    /// <returns></returns>
    public async Task<Dictionary<string, string>> GetAllDictAsync()
    {
        return (await _httpService.GetAllDictAsync()).Handling();
    }

    /// <summary>
    /// 根据字典类型ID获取所有该类型的字典列表集合(Key为名称，Value为值）
    /// </summary>
    /// <param name="dictTypeId">字典类型ID</param>
    /// <returns></returns>
    public async Task<Dictionary<string, string>> GetDictByTypeIdAsync(string dictTypeId)
    {
        return (await _httpService.GetDictByTypeIdAsync(dictTypeId)).Handling();
    }

    /// <summary>
    /// 根据字典类型名称获取所有该类型的字典列表集合(Key为名称，Value为值）
    /// </summary>
    /// <param name="dictTypeName">字典类型名称</param>
    /// <returns></returns>
    public async Task<Dictionary<string, string>> GetDictByDictTypeAsync(string dictTypeName)
    {
        return (await _httpService.GetDictByDictTypeAsync(dictTypeName)).Handling();
    }

    /// <summary>
    /// 根据字典类型获取对应的CListItem集合
    /// </summary>
    /// <param name="dictTypeName"></param>
    /// <returns></returns>
    public async Task<List<CListItem>> GetDictListItemByDictTypeAsync(string dictTypeName)
    {
        return (await _httpService.GetDictListItemByDictTypeAsync(dictTypeName)).Handling();
    }
                
    /// <summary>
    /// 根据字典类型名称和字典Value值（即字典编码），解析成字典对应的名称
    /// </summary>
    /// <param name="dictTypeName">字典类型名称</param>
    /// <param name="dictValue">字典Value值，即字典编码</param>
    /// <returns>字典对应的名称</returns>
    public async Task<string> GetDictNameAsync(string dictTypeName, string dictValue)
    {
        return (await _httpService.GetDictNameAsync(dictTypeName, dictValue)).Handling();
    }
}