using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Security;

/// <summary>
/// 登录系统的黑白名单列表
/// </summary>
[DataContract]
[Table(DBTableName)]
public class BlackIpInfo : BaseEntity
{    
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public BlackIpInfo()
    {
        ID = Guid.NewGuid().ToString();
        AuthorizeType = 0;
        Forbid = false;//是否禁用   
        CreateTime = DateTime.Now;
        EditTime = DateTime.Now;
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
    /// 显示名称
    /// </summary>
    [DataMember]
    [Column(FieldName)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 授权类型
    /// </summary>
    [DataMember]
    [Column(FieldAuthorizeType)]
    public virtual int AuthorizeType { get; set; }

    /// <summary>
    /// 是否禁用
    /// </summary>
    [DataMember]
    [Column(FieldForbid)]
    public virtual bool Forbid { get; set; }

    /// <summary>
    /// IP起始地址
    /// </summary>
    [DataMember]
    [Column(FieldIPStart)]
    public virtual string IPStart { get; set; }

    /// <summary>
    /// IP结束地址
    /// </summary>
    [DataMember]
    [Column(FieldIPEnd)]
    public virtual string IPEnd { get; set; }

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
    [Column(FieldCreatorId)]
    public virtual string CreatorId { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
    [Sort(IsDesc)]
    [Column(FieldCreateTime)]
    public virtual DateTime CreateTime { get; set; }

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
    [Column(FieldEditorId)]
    public virtual string EditorId { get; set; }

    /// <summary>
    /// 编辑时间
    /// </summary>
    [DataMember]
    [OptimisticLock]
    [Column(FieldEditTime)]
    public virtual DateTime EditTime { get; set; }


    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_BlackIP";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldID;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldCreateTime;

    /// <summary>
    /// 排序方式
    /// </summary>
    [NonSerialized]
    public new const bool IsDesc = true;

    /// <summary>
    /// 乐观锁字段
    /// </summary>
    [NonSerialized]
    public new const string OptimisticLockKey = FieldEditTime;

    [NonSerialized]
    public const string FieldID = "ID";

    /// <summary>
    /// 显示名称
    /// </summary>
    [NonSerialized]
    public const string FieldName = "Name";

    /// <summary>
    /// 授权类型
    /// </summary>
    [NonSerialized]
    public const string FieldAuthorizeType = "AuthorizeType";

    /// <summary>
    /// 是否禁用
    /// </summary>
    [NonSerialized]
    public const string FieldForbid = "Forbid";

    /// <summary>
    /// IP起始地址
    /// </summary>
    [NonSerialized]
    public const string FieldIPStart = "IPStart";

    /// <summary>
    /// IP结束地址
    /// </summary>
    [NonSerialized]
    public const string FieldIPEnd = "IPEnd";

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
    public const string FieldCreatorId = "Creator_ID";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string FieldCreateTime = "CreateTime";

    /// <summary>
    /// 编辑人
    /// </summary>
    [NonSerialized]
    public const string FieldEditor = "Editor";

    /// <summary>
    /// 编辑人ID
    /// </summary>
    [NonSerialized]
    public const string FieldEditorId = "Editor_ID";

    /// <summary>
    /// 编辑时间
    /// </summary>
    [NonSerialized]
    public const string FieldEditTime = "EditTime";


    #endregion
}