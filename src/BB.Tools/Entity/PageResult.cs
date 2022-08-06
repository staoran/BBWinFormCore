namespace BB.Tools.Entity;

/// <summary>
/// 统一的分页格式
/// </summary>
public class PageResult<T> : PagerInfo
{
    /// <summary>
    /// 当前页集合
    /// </summary>
    public IEnumerable<T> Rows { get; set; }
}