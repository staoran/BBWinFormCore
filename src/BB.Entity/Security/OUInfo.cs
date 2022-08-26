using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;
using SqlSugar;

namespace BB.Entity.Security;

/// <summary>
/// 部门机构信息
/// </summary>
[Serializable]
[DataContract]
[Table(DBTableName)]
public class OUInfo : BaseEntity
{
	/// <summary>
	/// 默认构造函数（需要初始化属性的在此处理）
	/// </summary>
	public OUInfo()
	{
		ID = 0;
		PID = "-1";
		CreationDate = DateTime.Now;
		LastUpdateDate = DateTime.Now;
		Deleted = false; //是否已删除
		Enabled = true; //有效标志

	}

    #region Property Members

    /// <summary>
    /// ID
    /// </summary>
    [DataMember]
    [Identity]
    [Sort(IsDesc)]
    [Column(FieldID, DisID)]
    public virtual int ID { get; set; }

    /// <summary>
    /// 父ID
    /// </summary>
    [DataMember]
    [Column(FieldPID, DisPID)]
    public virtual string PID { get; set; }

    /// <summary>
    /// 机构编码
    /// </summary>
    [DataMember]
    [Key]
    [Column(FieldHandNo, DisHandNo)]
    public virtual string HandNo { get; set; }

    /// <summary>
    /// 机构名称
    /// </summary>
    [DataMember]
    [Column(FieldName, DisName)]
    public virtual string Name { get; set; }

    /// <summary>
    /// 排序码
    /// </summary>
    [DataMember]
    [Column(FieldSortCode, DisSortCode)]
    public virtual string SortCode { get; set; }

    /// <summary>
    /// 机构分类
    /// </summary>
    [DataMember]
    [Column(FieldCategory, DisCategory)]
    public virtual string Category { get; set; }

    /// <summary>
    /// 机构地址
    /// </summary>
    [DataMember]
    [Column(FieldAddress, DisAddress)]
    public virtual string Address { get; set; }

    /// <summary>
    /// 外线电话
    /// </summary>
    [DataMember]
    [Column(FieldOuterPhone, DisOuterPhone)]
    public virtual string OuterPhone { get; set; }

    /// <summary>
    /// 内线电话
    /// </summary>
    [DataMember]
    [Column(FieldInnerPhone, DisInnerPhone)]
    public virtual string InnerPhone { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldNote, DisNote)]
    public virtual string Note { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [DataMember]
    [Column(FieldCreator, DisCreator)]
    public virtual string Creator { get; set; }

    /// <summary>
    /// 创建人ID
    /// </summary>
    [DataMember]
    [Column(FieldCreatedBy, DisCreatedBy)]
    public virtual string CreatedBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
    [Column(FieldCreationDate, DisCreationDate)]
    public virtual DateTime CreationDate { get; set; }

    /// <summary>
    /// 编辑人
    /// </summary>
    [DataMember]
    [Column(FieldEditor, DisEditor)]
    public virtual string Editor { get; set; }

    /// <summary>
    /// 编辑人ID
    /// </summary>
    [DataMember]
    [Column(FieldLastUpdatedBy, DisLastUpdatedBy)]
    public virtual string LastUpdatedBy { get; set; }

    /// <summary>
    /// 编辑时间
    /// </summary>
    [DataMember]
    [OptimisticLock]
    [Column(FieldLastUpdateDate, DisLastUpdateDate)]
    public virtual DateTime LastUpdateDate { get; set; }

    /// <summary>
    /// 是否已删除
    /// </summary>
    [DataMember]
    [Column(FieldDeleted, DisDeleted)]
    public virtual bool Deleted { get; set; }

    /// <summary>
    /// 有效标志
    /// </summary>
    [DataMember]
    [Column(FieldEnabled, DisEnabled)]
    public virtual bool Enabled { get; set; }

    /// <summary>
    /// 所属机构ID
    /// </summary>
    [DataMember]
    [Column(FieldCompanyId, DisCompanyId)]
    public virtual string CompanyId { get; set; }

    /// <summary>
    /// 所属机构名称
    /// </summary>
    [DataMember]
    [Column(FieldCompanyName, DisCompanyName)]
    public virtual string CompanyName { get; set; }
    
    /// <summary>
    /// 机构关联的用户列表
    /// </summary>
    [Navigate(typeof(OUUserEntity), nameof(OUUserEntity.OUId), nameof(OUUserEntity.UserId))]
    public List<UserInfo> UserList { get; set; }
    
    /// <summary>
    /// 机构关联的角色列表
    /// </summary>
    [Navigate(typeof(OURoleEntity), nameof(OURoleEntity.OUId), nameof(OURoleEntity.RoleId))]
    public List<RoleInfo> RoleList { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "T_ACL_OU";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldHandNo;

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

    #region 列名

    [NonSerialized]
    public const string FieldID = "ID";

    /// <summary>
    /// 父ID
    /// </summary>
    [NonSerialized]
    public const string FieldPID = "PID";

    /// <summary>
    /// 机构编码
    /// </summary>
    [NonSerialized]
    public const string FieldHandNo = "HandNo";

    /// <summary>
    /// 机构名称
    /// </summary>
    [NonSerialized]
    public const string FieldName = "Name";

    /// <summary>
    /// 排序码
    /// </summary>
    [NonSerialized]
    public const string FieldSortCode = "SortCode";

    /// <summary>
    /// 机构分类
    /// </summary>
    [NonSerialized]
    public const string FieldCategory = "Category";

    /// <summary>
    /// 机构地址
    /// </summary>
    [NonSerialized]
    public const string FieldAddress = "Address";

    /// <summary>
    /// 外线电话
    /// </summary>
    [NonSerialized]
    public const string FieldOuterPhone = "OuterPhone";

    /// <summary>
    /// 内线电话
    /// </summary>
    [NonSerialized]
    public const string FieldInnerPhone = "InnerPhone";

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

    #endregion

    #region 列显示名

    [NonSerialized]
    public const string DisID = "ID";

    /// <summary>
    /// 父ID
    /// </summary>
    [NonSerialized]
    public const string DisPID = "父ID";

    /// <summary>
    /// 机构编码
    /// </summary>
    [NonSerialized]
    public const string DisHandNo = "机构编码";

    /// <summary>
    /// 机构名称
    /// </summary>
    [NonSerialized]
    public const string DisName = "机构名称";

    /// <summary>
    /// 排序码
    /// </summary>
    [NonSerialized]
    public const string DisSortCode = "排序码";

    /// <summary>
    /// 机构分类
    /// </summary>
    [NonSerialized]
    public const string DisCategory = "机构分类";

    /// <summary>
    /// 机构地址
    /// </summary>
    [NonSerialized]
    public const string DisAddress = "机构地址";

    /// <summary>
    /// 外线电话
    /// </summary>
    [NonSerialized]
    public const string DisOuterPhone = "外线电话";

    /// <summary>
    /// 内线电话
    /// </summary>
    [NonSerialized]
    public const string DisInnerPhone = "内线电话";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string DisNote = "备注";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string DisCreator = "创建人";

    /// <summary>
    /// 创建人ID
    /// </summary>
    [NonSerialized]
    public const string DisCreatedBy = "创建人ID";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string DisCreationDate = "创建时间";

    /// <summary>
    /// 编辑人
    /// </summary>
    [NonSerialized]
    public const string DisEditor = "编辑人";

    /// <summary>
    /// 编辑人ID
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdatedBy = "编辑人ID";

    /// <summary>
    /// 编辑时间
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdateDate = "编辑时间";

    /// <summary>
    /// 是否已删除
    /// </summary>
    [NonSerialized]
    public const string DisDeleted = "是否已删除";

    /// <summary>
    /// 有效标志
    /// </summary>
    [NonSerialized]
    public const string DisEnabled = "有效标志";

    /// <summary>
    /// 所属公司ID
    /// </summary>
    [NonSerialized]
    public const string DisCompanyId = "所属公司ID";

    /// <summary>
    /// 所属公司名称
    /// </summary>
    [NonSerialized]
    public const string DisCompanyName = "所属公司名称";

    #endregion

    #endregion
}

/// <summary>
/// 部门机构节点对象
/// </summary>
[Serializable]
[DataContract]
public class OUNodeInfo : OUInfo
{
	private List<OUNodeInfo> _mChildren = new List<OUNodeInfo>();

	/// <summary>
	/// 子机构实体类对象集合
	/// </summary>
	[DataMember]
	public List<OUNodeInfo> Children
	{
		get => _mChildren;
		set => _mChildren = value;
	}

	/// <summary>
	/// 默认构造函数
	/// </summary>
	public OUNodeInfo()
	{
		_mChildren = new List<OUNodeInfo>();
	}

	/// <summary>
	/// 参数构造函数
	/// </summary>
	/// <param name="info">OUInfo对象</param>
	public OUNodeInfo(OUInfo info)
	{
		base.ID = info.ID;
		base.PID = info.PID;
		base.HandNo = info.HandNo;
		base.Name = info.Name;
		base.SortCode = info.SortCode;
		base.Category = info.Category;
		base.Address = info.Address;
		base.OuterPhone = info.OuterPhone;
		base.InnerPhone = info.InnerPhone;
		base.Note = info.Note;
		base.Creator = info.Creator;
		base.CreatedBy = info.CreatedBy;
		base.CreationDate = info.CreationDate;
		base.Editor = info.Editor;
		base.LastUpdatedBy = info.LastUpdatedBy;
		base.LastUpdateDate = info.LastUpdateDate;
		base.Deleted = info.Deleted;
		base.Enabled = info.Enabled;
		base.CompanyId = info.CompanyId;
		base.CompanyName = info.CompanyName;              
	}
}