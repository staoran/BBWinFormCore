using System.ComponentModel;
using System.Runtime.Serialization;

namespace BB.Entity.Base;

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

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public const string DBTableName = null;

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public const string PrimaryKey = null;

    /// <summary>
    /// 外键名
    /// </summary>
    [NonSerialized]
    public const string ForeignKey = null;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public const string SortKey = null;

    /// <summary>
    /// 排序方式
    /// </summary>
    [NonSerialized]
    public const bool IsDesc = true;

    /// <summary>
    /// 乐观锁字段
    /// </summary>
    [NonSerialized]
    public const string OptimisticLockKey = null;
    
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