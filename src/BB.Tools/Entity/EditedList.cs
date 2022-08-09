namespace BB.Tools.Entity;

/// <summary>
/// 编辑后的列表数据
/// </summary>
[Serializable]
public class EditedList<T>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="added">是否新增过</param>
    /// <param name="edited">是否编辑过</param>
    /// <param name="deleted">是否删除过</param>
    public EditedList(List<T> added, List<T> edited, List<T> deleted)
    {
        Added = added;
        Edited = edited;
        Deleted = deleted;
    }
    
    /// <summary>
    /// 新增的数据
    /// </summary>
    public List<T> Added { get; set; }
    
    /// <summary>
    /// 修改的数据
    /// </summary>
    public List<T> Edited { get; set; }
    
    /// <summary>
    /// 删除的数据
    /// </summary>
    public List<T> Deleted { get; set; }
}