using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Security;

/// <summary>
/// 记录操作日志的数据表配置
/// </summary>
[DataContract]
[Table("T_ACL_OperationLogSetting")]
public class OperationLogSettingInfo : BaseEntity
{    
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public OperationLogSettingInfo()
    {
        ID = Guid.NewGuid().ToString();
        Forbid = false; //是否禁用  
        InsertLog = false;
        DeleteLog = false;
        UpdateLog = false;
        CreationDate = DateTime.Now;
        LastUpdateDate = DateTime.Now;
    }

    #region Property Members

    /// <summary>
    /// ID
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldID)]
    public virtual string ID { get; set; }

    /// <summary>
    /// 是否禁用
    /// </summary>
    [DataMember]
    [Column(FieldForbid)]
    public virtual bool Forbid { get; set; }

    /// <summary>
    /// 数据库表
    /// </summary>
    [DataMember]
    [Column(FieldTableName)]
    public virtual string TableName { get; set; }

    /// <summary>
    /// 记录插入日志
    /// </summary>
    [DataMember]
    [Column(FieldInsertLog)]
    public virtual bool InsertLog { get; set; }

    /// <summary>
    /// 记录删除日志
    /// </summary>
    [DataMember]
    [Column(FieldDeleteLog)]
    public virtual bool DeleteLog { get; set; }

    /// <summary>
    /// 记录更新日志
    /// </summary>
    [DataMember]
    [Column(FieldUpdateLog)]
    public virtual bool UpdateLog { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldNote)]
    public virtual string Note { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [DataMember]
    [Column(FieldCreator)]
    public virtual string Creator { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [DataMember]
    [Column(FieldCreatedBy)]
    public virtual string CreatedBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
    [Sort(IsDesc)]
    [Column(FieldCreationDate)]
    public virtual DateTime CreationDate { get; set; }

    /// <summary>
    /// 编辑人
    /// </summary>
    [DataMember]
    [Column(FieldEditor)]
    public virtual string Editor { get; set; }

    /// <summary>
    /// 编辑人ID
    /// </summary>
    [DataMember]
    [Column(FieldLastUpdatedBy)]
    public virtual string LastUpdatedBy { get; set; }

    /// <summary>
    /// 编辑时间
    /// </summary>
    [DataMember]
    [OptimisticLock]
    [Column(FieldLastUpdateDate)]
    public virtual DateTime LastUpdateDate { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_OperationLogSetting";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldID;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldCreationDate;

    /// <summary>
    /// 排序方式
    /// </summary>
    [NonSerialized]
    public new const bool IsDesc = true;

    /// <summary>
    /// 乐观锁字段
    /// </summary>
    [NonSerialized]
    public new const string OptimisticLockKey = FieldLastUpdateDate;

    [NonSerialized]
    public const string FieldID = "ID";

    /// <summary>
    /// 是否禁用
    /// </summary>
    [NonSerialized]
    public const string FieldForbid = "Forbid";

    /// <summary>
    /// 数据库表
    /// </summary>
    [NonSerialized]
    public const string FieldTableName = "TableName";

    /// <summary>
    /// 记录插入日志
    /// </summary>
    [NonSerialized]
    public const string FieldInsertLog = "InsertLog";

    /// <summary>
    /// 记录删除日志
    /// </summary>
    [NonSerialized]
    public const string FieldDeleteLog = "DeleteLog";

    /// <summary>
    /// 记录更新日志
    /// </summary>
    [NonSerialized]
    public const string FieldUpdateLog = "UpdateLog";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldNote = "Note";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string FieldCreator = "Creator";

    /// <summary>
    /// 创建人ID
    /// </summary>
    [NonSerialized]
    public const string FieldCreatedBy = "CreatedBy";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string FieldCreationDate = "CreationDate";

    /// <summary>
    /// 编辑人
    /// </summary>
    [NonSerialized]
    public const string FieldEditor = "Editor";

    /// <summary>
    /// 编辑人ID
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdatedBy = "LastUpdatedBy";

    /// <summary>
    /// 编辑时间
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdateDate = "LastUpdateDate";

    #endregion
}