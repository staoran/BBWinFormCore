using System.ComponentModel;
using System.Runtime.Serialization;
using SqlSugar;

namespace BB.Entity.Base;

/// <summary>
/// 有子表的实体基类
/// </summary>
/// <typeparam name="T"></typeparam>
[DataContract]
[Serializable]
public class BaseEntity<T> : BaseEntity where T : BaseEntity
{
    /// <summary>
    /// 获取子表外键
    /// </summary>
    [DataMember]
    [Ignore]
    public string GetChildForeignKey => ChildForeignKey;

    /// <summary>
    /// 子表数据
    /// </summary>
    [DataMember]
    [Ignore]
    [Navigate(NavigateType.OneToMany, ChildForeignKey)]
    public List<T>? ChildTableList { get; set; }

    /// <summary>
    /// 子表外键
    /// </summary>
    [NonSerialized]
    public const string ChildForeignKey = "";
}

/// <summary>
/// 框架实体类的基类
/// </summary>
[DataContract]
[Serializable]
public class BaseEntity : INotifyPropertyChanged
{
    /// <summary>
    /// 当前登录用户ID。该字段不保存到数据表中，只用于记录用户的操作日志。
    /// </summary>
    [DataMember]
    [Ignore]
    public string CurrentLoginUserId { get; set; }

    /// <summary>
    /// 获取表名
    /// </summary>
    [DataMember]
    [Ignore]
    public string GetDBTableName => DBTableName;

    /// <summary>
    /// 获取主键
    /// </summary>
    [DataMember]
    [Ignore]
    public string GetPrimaryKey => PrimaryKey;

    /// <summary>
    /// 获取外键
    /// </summary>
    [DataMember]
    [Ignore]
    public string GetForeignKey => ForeignKey;

    /// <summary>
    /// 获取排序字段
    /// </summary>
    [DataMember]
    [Ignore]
    public string GetSortKey => SortKey;

    /// <summary>
    /// 获取排序方式
    /// </summary>
    [DataMember]
    [Ignore]
    public bool GetIsDesc => IsDesc;

    /// <summary>
    /// 获取乐观锁字段
    /// </summary>
    [DataMember]
    [Ignore]
    public string GetOptimisticLockKey => OptimisticLockKey;

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public const string DBTableName = "";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public const string PrimaryKey = "";

    /// <summary>
    /// 外键名
    /// </summary>
    [NonSerialized]
    public const string ForeignKey = "";

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public const string SortKey = "";

    /// <summary>
    /// 排序方式
    /// </summary>
    [NonSerialized]
    public const bool IsDesc = true;

    /// <summary>
    /// 乐观锁字段
    /// </summary>
    [NonSerialized]
    public const string OptimisticLockKey = "";
    
    #endregion

    #region 在实体类存储一些特殊的数据
    /// <summary>
    /// 用来给实体类传递一些额外的数据，如外键的转义等，该字段不保存到数据表中
    /// </summary>
    [DataMember]
    [Ignore]
    public string Data1 { get; set; }

    /// <summary>
    /// 用来给实体类传递一些额外的数据，如外键的转义等，该字段不保存到数据表中
    /// </summary>
    [DataMember]
    [Ignore]
    public string Data2 { get; set; }

    /// <summary>
    /// 用来给实体类传递一些额外的数据，如外键的转义等，该字段不保存到数据表中
    /// </summary>
    [DataMember]
    [Ignore]
    public string Data3 { get; set; } 
    #endregion

    [field:NonSerialized]
    public event PropertyChangedEventHandler PropertyChanged;

    // protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    // {
    //     PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    // }
    //
    // protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
    // {
    //     if (EqualityComparer<T>.Default.Equals(field, value)) return false;
    //     field = value;
    //     OnPropertyChanged(propertyName);
    //     return true;
    // }
    // private void SendChangeInfo(string propertyName)
    // {
    //     if (PropertyChanged != null)
    //     {
    //         PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
    //     }
    // }
}