namespace BB.Tools.Entity;

/// <summary>
/// 统一的分页格式
/// </summary>
public class PageResult<T> : PagerInfo
{
    public PageResult()
    {
    }

    public PageResult(IEnumerable<T> rows)
    {
        IEnumerable<T> enumerable = rows.ToList();
        Rows = enumerable;
        PageNo = 1;
        PageSize = enumerable.Count();
        TotalPage = PageSize;
    }

    public PageResult(int pageNo, int pageSize, IEnumerable<T> rows, int totalRows, int totalPage)
    {
        PageNo = pageNo;
        PageSize = pageSize;
        Rows = rows;
        TotalRows = totalRows;
        TotalPage = totalPage;
    }

    /// <summary>
    /// 当前页集合
    /// </summary>
    public IEnumerable<T> Rows { get; set; }
}