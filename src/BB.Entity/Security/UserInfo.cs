using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.Security;

/// <summary>
/// 用户信息
/// </summary>
[Serializable]
[DataContract]
[Table(DBTableName)]
public class UserInfo : BaseEntity
{
    public const int IDENTITY_LEN = 50;
    /// <summary>
    /// 默认密码
    /// </summary>
    public const string DEFAULT_PASSWORD = "12345678";

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public UserInfo()
    {
        PID = -1; //父ID 
        IsExpire = false; //是否过期
        CreationDate = DateTime.Now; //创建时间
        LastUpdateDate = DateTime.Now; //编辑时间
        Deleted = false; //是否已删除
        Status = "未关联";
        SubscribeWechat = "未关注";
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
    /// 用户编码
    /// </summary>
    [DataMember]
    [Column(FieldHandNo)]
    public virtual string HandNo { get; set; }

    /// <summary>
    /// 用户名/登录名
    /// </summary>
    [DataMember]
    [Column(FieldName)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 用户密码
    /// </summary>
    [DataMember]
    [Column(FieldPassword)]
    public virtual string Password { get; set; }

    /// <summary>
    /// 用户全名
    /// </summary>
    [DataMember]
    [Column(FieldFullName)]
    public virtual string FullName { get; set; }

    /// <summary>
    /// 用户呢称
    /// </summary>
    [DataMember]
    [Column(FieldNickname)]
    public virtual string Nickname { get; set; }

    /// <summary>
    /// 是否过期
    /// </summary>
    [DataMember]
    [Column(FieldIsExpire)]
    public virtual bool IsExpire { get; set; }

    /// <summary>
    /// 职务头衔
    /// </summary>
    [DataMember]
    [Column(FieldTitle)]
    public virtual string Title { get; set; }

    /// <summary>
    /// 身份证号码
    /// </summary>
    [DataMember]
    [Column(FieldIdentityCard)]
    public virtual string IdentityCard { get; set; }

    /// <summary>
    /// 办公电话
    /// </summary>
    [DataMember]
    [Column(FieldOfficePhone)]
    public virtual string OfficePhone { get; set; }

    /// <summary>
    /// 家庭电话
    /// </summary>
    [DataMember]
    [Column(FieldHomePhone)]
    public virtual string HomePhone { get; set; }

    /// <summary>
    /// 移动电话
    /// </summary>
    [DataMember]
    [Column(FieldMobilePhone)]
    public virtual string MobilePhone { get; set; }

    /// <summary>
    /// 邮件地址
    /// </summary>
    [DataMember]
    [Column(FieldEmail)]
    public virtual string Email { get; set; }

    /// <summary>
    /// 住址
    /// </summary>
    [DataMember]
    [Column(FieldAddress)]
    public virtual string Address { get; set; }

    /// <summary>
    /// 办公地址
    /// </summary>
    [DataMember]
    [Column(FieldWorkAddr)]
    public virtual string WorkAddr { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    [DataMember]
    [Column(FieldGender)]
    public virtual string Gender { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    [DataMember]
    [Column(FieldBirthday)]
    public virtual DateTime Birthday { get; set; }

    /// <summary>
    /// QQ号码
    /// </summary>
    [DataMember]
    [Column(FieldQQ)]
    public virtual string QQ { get; set; }

    /// <summary>
    /// 个性签名
    /// </summary>
    [DataMember]
    [Column(FieldSignature)]
    public virtual string Signature { get; set; }

    /// <summary>
    /// 审核状态
    /// </summary>
    [DataMember]
    [Column(FieldAuditStatus)]
    public virtual string AuditStatus { get; set; }

    /// <summary>
    /// 个人图片
    /// </summary>
    [DataMember]
    [Column(FieldPortrait)]
    public virtual byte[] Portrait { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldNote)]
    public virtual string Note { get; set; }

    /// <summary>
    /// 自定义字段
    /// </summary>
    [DataMember]
    [Column(FieldCustomField)]
    public virtual string CustomField { get; set; }

    /// <summary>
    /// 默认部门ID
    /// </summary>
    [DataMember]
    [Column(FieldDeptId)]
    public virtual string DeptId { get; set; }

    /// <summary>
    /// 默认部门名称
    /// </summary>
    [DataMember]
    [Column(FieldDeptName)]
    public virtual string DeptName { get; set; }

    /// <summary>
    /// 所属机构ID
    /// </summary>
    [DataMember]
    [Column(FieldCompanyId)]
    public virtual string CompanyId { get; set; }

    /// <summary>
    /// 所属公司名称
    /// </summary>
    [DataMember]
    [Column(FieldCompanyName)]
    public virtual string CompanyName { get; set; }

    /// <summary>
    /// 排序码
    /// </summary>
    [DataMember]
    [Column(FieldSortCode)]
    public virtual string SortCode { get; set; }

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

    /// <summary>
    /// 是否已删除
    /// </summary>
    [DataMember]
    [Column(FieldDeleted)]
    public virtual bool Deleted { get; set; }

    /// <summary>
    /// 密保：提示问题
    /// </summary>
    [DataMember]
    [Column(FieldQuestion)]
    public virtual string Question { get; set; }

    /// <summary>
    /// 密保:问题答案
    /// </summary>
    [DataMember]
    [Column(FieldAnswer)]
    public virtual string Answer { get; set; }

    /// <summary>
    /// 当前登录IP
    /// </summary>
    [DataMember]
    [Column(FieldCurrentLoginIP)]
    public virtual string CurrentLoginIP { get; set; }

    /// <summary>
    /// 当前登录时间
    /// </summary>
    [DataMember]
    [Column(FieldCurrentLoginTime)]
    public virtual DateTime CurrentLoginTime { get; set; }

    /// <summary>
    /// 当前Mac地址
    /// </summary>
    [DataMember]
    [Column(FieldCurrentMacAddress)]
    public virtual string CurrentMacAddress { get; set; }

    /// <summary>
    /// 上次登录IP
    /// </summary>
    [DataMember]
    [Column(FieldLastLoginIP)]
    public virtual string LastLoginIP { get; set; }

    /// <summary>
    /// 上次登录时间
    /// </summary>
    [DataMember]
    [Column(FieldLastLoginTime)]
    public virtual DateTime LastLoginTime { get; set; }

    /// <summary>
    /// 上次Mac地址
    /// </summary>
    [DataMember]
    [Column(FieldLastMacAddress)]
    public virtual string LastMacAddress { get; set; }

    /// <summary>
    /// 最后修改密码日期
    /// </summary>
    [DataMember]
    [Column(FieldLastPasswordTime)]
    public virtual DateTime LastPasswordTime { get; set; }

    /// <summary>
    /// 过期时间
    /// </summary>
    [DataMember]
    [Column(FieldExpireDate)]
    public virtual DateTime? ExpireDate { get; set; }

    /// <summary>
    /// 微信绑定的OpenId
    /// </summary>
    [DataMember]
    [Column(FieldOpenId)]
    public virtual string OpenId { get; set; }

    /// <summary>
    /// 微信多平台应用下的统一ID
    /// </summary>
    [DataMember]
    [Column(FieldUnionId)]
    public virtual string UnionId { get; set; }

    /// <summary>
    /// 公众号状态
    /// </summary>
    [DataMember]
    [Column(FieldStatus)]
    public virtual string Status { get; set; }

    /// <summary>
    /// 公众号
    /// </summary>
    [DataMember]
    [Column(FieldSubscribeWechat)]
    public virtual string SubscribeWechat { get; set; }

    /// <summary>
    /// 科室权限
    /// </summary>
    [DataMember]
    [Column(FieldDeptPermission)]
    public virtual string DeptPermission { get; set; }

    /// <summary>
    /// 企业微信UserID
    /// </summary>
    [DataMember]
    [Column(FieldCorpUserId)]
    public virtual string CorpUserId { get; set; }

    /// <summary>
    /// 企业微信状态
    /// </summary>
    [DataMember]
    [Column(FieldCorpStatus)]
    public virtual string CorpStatus { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_User";

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
    public new const string OptimisticLockKey = FieldLastUpdateDate;

    /// <summary>
    /// 数据权限字段
    /// </summary>
    [NonSerialized]
    public new const string DataPermissionKey = FieldCompanyId;

    [NonSerialized]
    public const string FieldID = "ID";

    /// <summary>
    /// 用户编码
    /// </summary>
    [NonSerialized]
    public const string FieldHandNo = "HandNo";

    /// <summary>
    /// 用户名/登录名
    /// </summary>
    [NonSerialized]
    public const string FieldName = "Name";

    /// <summary>
    /// 用户密码
    /// </summary>
    [NonSerialized]
    public const string FieldPassword = "Password";

    /// <summary>
    /// 真实姓名
    /// </summary>
    [NonSerialized]
    public const string FieldFullName = "FullName";

    /// <summary>
    /// 移动电话
    /// </summary>
    [NonSerialized]
    public const string FieldMobilePhone = "MobilePhone";

    /// <summary>
    /// 邮件地址
    /// </summary>
    [NonSerialized]
    public const string FieldEmail = "Email";

    /// <summary>
    /// 默认部门ID
    /// </summary>
    [NonSerialized]
    public const string FieldDeptId = "Dept_ID";

    /// <summary>
    /// 所属公司ID
    /// </summary>
    [NonSerialized]
    public const string FieldCompanyId = "Company_ID";

    /// <summary>
    /// 当前登录IP
    /// </summary>
    [NonSerialized]
    public const string FieldCurrentLoginIP = "CurrentLoginIP";

    /// <summary>
    /// 当前登录时间
    /// </summary>
    [NonSerialized]
    public const string FieldCurrentLoginTime = "CurrentLoginTime";

    /// <summary>
    /// 当前Mac地址
    /// </summary>
    [NonSerialized]
    public const string FieldCurrentMacAddress = "CurrentMacAddress";

    /// <summary>
    /// 父ID
    /// </summary>
    [NonSerialized]
    public const string FieldPID = "PID";

    /// <summary>
    /// 用户呢称
    /// </summary>
    [NonSerialized]
    public const string FieldNickname = "Nickname";

    /// <summary>
    /// 是否过期
    /// </summary>
    [NonSerialized]
    public const string FieldIsExpire = "IsExpire";

    /// <summary>
    /// 职务头衔
    /// </summary>
    [NonSerialized]
    public const string FieldTitle = "Title";

    /// <summary>
    /// 身份证号码
    /// </summary>
    [NonSerialized]
    public const string FieldIdentityCard = "IdentityCard";

    /// <summary>
    /// 办公电话
    /// </summary>
    [NonSerialized]
    public const string FieldOfficePhone = "OfficePhone";

    /// <summary>
    /// 家庭电话
    /// </summary>
    [NonSerialized]
    public const string FieldHomePhone = "HomePhone";

    /// <summary>
    /// 住址
    /// </summary>
    [NonSerialized]
    public const string FieldAddress = "Address";

    /// <summary>
    /// 办公地址
    /// </summary>
    [NonSerialized]
    public const string FieldWorkAddr = "WorkAddr";

    /// <summary>
    /// 性别
    /// </summary>
    [NonSerialized]
    public const string FieldGender = "Gender";

    /// <summary>
    /// 出生日期
    /// </summary>
    [NonSerialized]
    public const string FieldBirthday = "Birthday";

    /// <summary>
    /// QQ号码
    /// </summary>
    [NonSerialized]
    public const string FieldQQ = "QQ";

    /// <summary>
    /// 个性签名
    /// </summary>
    [NonSerialized]
    public const string FieldSignature = "Signature";

    /// <summary>
    /// 审核状态
    /// </summary>
    [NonSerialized]
    public const string FieldAuditStatus = "AuditStatus";

    /// <summary>
    /// 个人图片
    /// </summary>
    [NonSerialized]
    public const string FieldPortrait = "Portrait";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldNote = "Note";

    /// <summary>
    /// 自定义字段
    /// </summary>
    [NonSerialized]
    public const string FieldCustomField = "CustomField";

    /// <summary>
    /// 默认部门名称
    /// </summary>
    [NonSerialized]
    public const string FieldDeptName = "DeptName";

    /// <summary>
    /// 所属公司名称
    /// </summary>
    [NonSerialized]
    public const string FieldCompanyName = "CompanyName";

    /// <summary>
    /// 排序码
    /// </summary>
    [NonSerialized]
    public const string FieldSortCode = "SortCode";

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

    /// <summary>
    /// 是否已删除
    /// </summary>
    [NonSerialized]
    public const string FieldDeleted = "Deleted";

    /// <summary>
    /// 密保：提示问题
    /// </summary>
    [NonSerialized]
    public const string FieldQuestion = "Question";

    /// <summary>
    /// 密保:问题答案
    /// </summary>
    [NonSerialized]
    public const string FieldAnswer = "Answer";

    /// <summary>
    /// 上次登录IP
    /// </summary>
    [NonSerialized]
    public const string FieldLastLoginIP = "LastLoginIP";

    /// <summary>
    /// 上次登录时间
    /// </summary>
    [NonSerialized]
    public const string FieldLastLoginTime = "LastLoginTime";

    /// <summary>
    /// 上次Mac地址
    /// </summary>
    [NonSerialized]
    public const string FieldLastMacAddress = "LastMacAddress";
    /// <summary>
    /// 最后修改密码日期
    /// </summary>
    [NonSerialized]
    public const string FieldLastPasswordTime = "LastPasswordTime";

    /// <summary>
    /// 过期时间
    /// </summary>
    [NonSerialized]
    public const string FieldExpireDate = "ExpireDate";

    /// <summary>
    /// 微信绑定的OpenId
    /// </summary>
    [NonSerialized]
    public const string FieldOpenId = "OpenId";

    /// <summary>
    /// 微信多平台应用下的统一ID
    /// </summary>
    [NonSerialized]
    public const string FieldUnionId = "UnionId";

    /// <summary>
    /// 公众号状态
    /// </summary>
    [NonSerialized]
    public const string FieldStatus = "Status";

    /// <summary>
    /// 公众号
    /// </summary>
    [NonSerialized]
    public const string FieldSubscribeWechat = "SubscribeWechat";

    /// <summary>
    /// 科室权限
    /// </summary>
    [NonSerialized]
    public const string FieldDeptPermission = "DeptPermission";

    /// <summary>
    /// 企业微信UserID
    /// </summary>
    [NonSerialized]
    public const string FieldCorpUserId = "CorpUserId";

    /// <summary>
    /// 企业微信状态
    /// </summary>
    [NonSerialized]
    public const string FieldCorpStatus = "CorpStatus";

    #endregion
}