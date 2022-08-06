namespace BB.Tools.Entity;

/// <summary>
/// 分页搜索条件
/// </summary>
/// <param name="SearchInfos">搜索条件</param>
/// <param name="PagerInfo">分页条件</param>
public record PaginatedSearchInfos(SearchInfo[] SearchInfos, PageInput PagerInfo);