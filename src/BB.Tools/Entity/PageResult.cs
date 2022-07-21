namespace BB.Tools.Entity;

/// <summary>
/// 统一的分页格式
/// </summary>
public class PageResult<T> where T : new()
{
    /// <summary>
    /// 页码
    /// </summary>
    public int PageNo { get; set; }
    
    /// <summary>
    /// 页条数
    /// </summary>
    public int PageSize { get; set; }
    
    /// <summary>
    /// 总页数
    /// </summary>
    public int TotalPage { get; set; }
    
    /// <summary>
    /// 总条数
    /// </summary>
    public int TotalRows { get; set; }
    
    /// <summary>
    /// 当前页集合
    /// </summary>
    public IEnumerable<T> Rows { get; set; }

    /// <summary>
    /// 是否有上一页
    /// </summary>
    public bool HasPrevPages => PageNo > 1;

    /// <summary>
    /// 是否有下一页
    /// </summary>
    public bool HasNextPages => PageNo < TotalPage;
}