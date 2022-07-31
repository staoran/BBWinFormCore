namespace BB.Tools.Entity;

/// <summary>
/// 通用分页入参
/// </summary>
public class PageInput
{
    /// <summary>
    /// 当前页码
    /// </summary>
    /// <example>1</example>
    public virtual int PageNo { get; set; } = 1;

    /// <summary>
    /// 页码容量
    /// </summary>
    /// <example>20</example>
    public virtual int PageSize { get; set; } = 20;

    /// <summary>
    /// 排序字段
    /// </summary>
    public virtual string SortField { get; set; }

    /// <summary>
    /// 排序方法,默认升序,否则降序
    /// </summary>
    /// <example>Desc</example>
    public virtual string SortOrder { get; set; } = "Desc";
}