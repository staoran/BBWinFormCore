using System.Runtime.Serialization;
using BB.Entity.Base;

namespace BB.Entity.Security;

/// <summary>
/// 用户关键操作记录
/// </summary>
[DataContract]
[Table(DBTableName)]
public class OperationLogInfo : BaseEntity
{    
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public OperationLogInfo()
    {
        ID = Guid.NewGuid().ToString();
        CreateTime = DateTime.Now;

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
    /// 登录用户ID
    /// </summary>
    [DataMember]
    [Column(FieldUserId)]
    public virtual string UserId { get; set; }

    /// <summary>
    /// 登录名
    /// </summary>
    [DataMember]
    [Column(FieldLoginName)]
    public virtual string LoginName { get; set; }

    /// <summary>
    /// 真实名称
    /// </summary>
    [DataMember]
    [Column(FieldFullName)]
    public virtual string FullName { get; set; }

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
    /// 操作表名称
    /// </summary>
    [DataMember]
    [Column(FieldTableName)]
    public virtual string TableName { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    [DataMember]
    [Column(FieldOperationType)]
    public virtual string OperationType { get; set; }

    /// <summary>
    /// 日志描述
    /// </summary>
    [DataMember]
    [Column(FieldNote)]
    public virtual string Note { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    [DataMember]
    [Column(FieldIPAddress)]
    public virtual string IPAddress { get; set; }

    /// <summary>
    /// Mac地址
    /// </summary>
    [DataMember]
    [Column(FieldMacAddress)]
    public virtual string MacAddress { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
    [Sort(IsDesc)]
    [Column(FieldCreateTime)]
    public virtual DateTime CreateTime { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_OperationLog";

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

    [NonSerialized]
    public const string FieldID = "ID";

    /// <summary>
    /// 登录用户ID
    /// </summary>
    [NonSerialized]
    public const string FieldUserId = "User_ID";

    /// <summary>
    /// 登录名
    /// </summary>
    [NonSerialized]
    public const string FieldLoginName = "LoginName";

    /// <summary>
    /// 真实名称
    /// </summary>
    [NonSerialized]
    public const string FieldFullName = "FullName";

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
    /// 操作表名称
    /// </summary>
    [NonSerialized]
    public const string FieldTableName = "TableName";

    /// <summary>
    /// 操作类型
    /// </summary>
    [NonSerialized]
    public const string FieldOperationType = "OperationType";

    /// <summary>
    /// 日志描述
    /// </summary>
    [NonSerialized]
    public const string FieldNote = "Note";

    /// <summary>
    /// IP地址
    /// </summary>
    [NonSerialized]
    public const string FieldIPAddress = "IPAddress";

    /// <summary>
    /// Mac地址
    /// </summary>
    [NonSerialized]
    public const string FieldMacAddress = "MacAddress";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string FieldCreateTime = "CreateTime";

    #endregion
}