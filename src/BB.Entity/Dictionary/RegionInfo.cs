using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.Dictionary;

/// <summary>
/// 行政区域
/// </summary>
[Serializable]
[DataContract]
[Table(DBTableName)]
public class RegionInfo : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public RegionInfo()
    {
    }

    #region Property Members

    /// <summary>
    /// 区域编码
    /// </summary>
    [DataMember]
    [Key]
    [Sort(IsDesc)]
    [Column(FieldId)]
    public virtual long Id { get; set; }

    /// <summary>
    /// 区域名称
    /// </summary>
    [DataMember]
    [Column(FieldName)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 上级区域编码
    /// </summary>
    [DataMember]
    [Column(FieldParentId)]
    public virtual long ParentId { get; set; }

    /// <summary>
    /// 坐标
    /// </summary>
    [DataMember]
    [Column(FieldLng)]
    public virtual string Lng { get; set; }

    /// <summary>
    /// 坐标
    /// </summary>
    [DataMember]
    [Column(FieldLat)]
    public virtual string Lat { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
    [Column(FieldCreationDate)]
    public virtual DateTime CreationDate { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [DataMember]
    [OptimisticLock]
    [Column(FieldLastUpdateDate)]
    public virtual DateTime LastUpdateDate { get; set; }

    /// <summary>
    /// 区域类型
    /// </summary>
    [DataMember]
    [Column(FieldType)]
    public virtual short Type { get; set; }

    /// <summary>
    /// 是否停用
    /// </summary>
    [DataMember]
    [Column(FieldIsDeleted)]
    public virtual bool IsDeleted { get; set; }

    /// <summary>
    /// 区域全称
    /// </summary>
    [DataMember]
    [Column(FieldFullName)]
    public virtual string FullName { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "TB_Region";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldId;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldId;

    /// <summary>
    /// 排序方式
    /// </summary>
    [NonSerialized]
    public new const bool IsDesc = false;

    /// <summary>
    /// 乐观锁字段
    /// </summary>
    [NonSerialized]
    public new const string OptimisticLockKey = FieldLastUpdateDate;

    /// <summary>
    /// 区域编码
    /// </summary>
    [NonSerialized]
    public const string FieldId = "Id";

    /// <summary>
    /// 区域名称
    /// </summary>
    [NonSerialized]
    public const string FieldName = "Name";

    /// <summary>
    /// 上级区域编码
    /// </summary>
    [NonSerialized]
    public const string FieldParentId = "ParentId";

    /// <summary>
    /// 坐标
    /// </summary>
    [NonSerialized]
    public const string FieldLng = "Lng";

    /// <summary>
    /// 坐标
    /// </summary>
    [NonSerialized]
    public const string FieldLat = "Lat";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string FieldCreationDate = "CreationDate";

    /// <summary>
    /// 修改时间
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdateDate = "LastUpdateDate";

    /// <summary>
    /// 区域类型
    /// </summary>
    [NonSerialized]
    public const string FieldType = "Type";

    /// <summary>
    /// 是否停用
    /// </summary>
    [NonSerialized]
    public const string FieldIsDeleted = "IsDeleted";

    /// <summary>
    /// 区域全称
    /// </summary>
    [NonSerialized]
    public const string FieldFullName = "FullName";

    #endregion
}