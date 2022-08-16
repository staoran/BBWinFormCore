namespace BB.Tools.Entity;

/// <summary>
/// 主键
/// </summary>
public class KeyAttribute : Attribute
{
    
}

/// <summary>
/// 自增
/// </summary>
public class IdentityAttribute : Attribute
{
    
}

/// <summary>
/// 乐观锁
/// </summary>
public class OptimisticLockAttribute : Attribute
{
    
}

/// <summary>
/// 忽略
/// </summary>
public class IgnoreAttribute : Attribute
{
    
}

/// <summary>
/// 隐藏
/// </summary>
public class HideAttribute : Attribute
{
    
}

/// <summary>
/// 子表可以为空
/// </summary>
public class IsChildListNullAttribute : Attribute
{
    
}

/// <summary>
/// 排序特性
/// </summary>
public class SortAttribute : Attribute
{
    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="isDesc">是否倒序</param>
    public SortAttribute(bool isDesc)
    {
        IsDesc = isDesc;
    }

    public bool IsDesc { get; }
}

/// <summary>
/// 表定义
/// </summary>
public class TableAttribute : Attribute
{
    /// <summary>
    /// 表定义
    /// </summary>
    /// <param name="name">表名</param>
    public TableAttribute(string name)
    {
        Name = name;
    }

    /// <summary>
    /// 表名
    /// </summary>
    public string Name { get; }
}

/// <summary>
/// 列定义
/// </summary>
public class ColumnAttribute : Attribute
{
    /// <summary>
    /// 列定义
    /// </summary>
    /// <param name="name">列名</param>
    /// <param name="display">列显示名</param>
    /// <param name="hide">是否隐藏</param>
    /// <param name="permit">表单操作权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑</param>
    public ColumnAttribute(string name, string display = "", bool hide = false, int permit = 0)
    {
        Name = name;
        Display = display;
        Hide = hide;
        Permit = permit;
    }

    /// <summary>
    /// 列名
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// 列显示名
    /// </summary>
    public string Display { get; }
    
    /// <summary>
    /// 是否隐藏
    /// </summary>
    public bool Hide { get; }
    
    /// <summary>
    /// 表单操作权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    public int Permit { get; }
}