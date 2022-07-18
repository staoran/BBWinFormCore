using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Security;

/// <summary>
/// 角色信息
/// </summary>
[Serializable]
[DataContract]
[Table(DBTableName)]
public class RoleInfo : BaseEntity
{
    /// <summary>
    /// 超级管理员名称
    /// </summary>
    public const string SUPER_ADMIN_NAME = "超级管理员";

    /// <summary>
    /// 公司级别的系统管理员
    /// </summary>
    public const string COMPANY_ADMIN_NAME = "系统管理员";

      
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public RoleInfo()
    {
        ID = 0;
        PID = -1;
        CreateTime = DateTime.Now;
        EditTime = DateTime.Now;
        Deleted = false; //是否已删除
        Enabled = true; //有效标志

    }

    #region Property Members

    /// <summary>
    /// ID
    /// </summary>
    [DataMember]
    [Key]
    [Identity]
    [Sort(IsDesc)]
    [Column(FieldID)]
    public virtual int ID { get; set; }

    /// <summary>
    /// 父ID
    /// </summary>
    [DataMember]
    [Column(FieldPID)]
    public virtual int PID { get; set; }

    /// <summary>
    /// 角色编码
    /// </summary>
    [DataMember]
    [Column(FieldHandNo)]
    public virtual string HandNo { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    [DataMember]
    [Column(FieldName)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldNote)]
    public virtual string Note { get; set; }

    /// <summary>
    /// 排序码
    /// </summary>
    [DataMember]
    [Column(FieldSortCode)]
    public virtual string SortCode { get; set; }

    /// <summary>
    /// 角色分类
    /// </summary>
    [DataMember]
    [Column(FieldCategory)]
    public virtual string Category { get; set; }

    /// <summary>
    /// 所属机构ID
    /// </summary>
    [DataMember]
    [Column(FieldCompanyId)]
    public virtual string CompanyId { get; set; }

    /// <summary>
    /// 所属机构名称
    /// </summary>
    [DataMember]
    [Column(FieldCompanyName)]
    public virtual string CompanyName { get; set; }

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

    /// <summary>
    /// 是否已删除
    /// </summary>
    [DataMember]
    [Column(FieldDeleted)]
    public virtual bool Deleted { get; set; }

    /// <summary>
    /// 有效标志
    /// </summary>
    [DataMember]
    [Column(FieldEnabled)]
    public virtual bool Enabled { get; set; }


    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_Role";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldID;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldID;

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
    /// 父ID
    /// </summary>
    [NonSerialized]
    public const string FieldPID = "PID";

    /// <summary>
    /// 角色编码
    /// </summary>
    [NonSerialized]
    public const string FieldHandNo = "HandNo";

    /// <summary>
    /// 角色名称
    /// </summary>
    [NonSerialized]
    public const string FieldName = "Name";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldNote = "Note";

    /// <summary>
    /// 排序码
    /// </summary>
    [NonSerialized]
    public const string FieldSortCode = "SortCode";

    /// <summary>
    /// 角色分类
    /// </summary>
    [NonSerialized]
    public const string FieldCategory = "Category";

    /// <summary>
    /// 所属公司ID
    /// </summary>
    [NonSerialized]
    public const string FieldCompanyId = "Company_ID";

    /// <summary>
    /// 所属公司名称
    /// </summary>
    [NonSerialized]
    public const string FieldCompanyName = "CompanyName";

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

    /// <summary>
    /// 是否已删除
    /// </summary>
    [NonSerialized]
    public const string FieldDeleted = "Deleted";

    /// <summary>
    /// 有效标志
    /// </summary>
    [NonSerialized]
    public const string FieldEnabled = "Enabled";


    #endregion
}