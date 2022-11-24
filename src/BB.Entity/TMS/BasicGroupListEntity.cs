using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 区域分组 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class BasicGroupList : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过关键字替换或重写 SetDynamicDefaults 方法实现）
    /// </summary>
    public BasicGroupList()
    {
        CreationDate = DateTime.Now;
        CreatedBy = "*当前用户*";
        LastUpdateDate = DateTime.Now;
        LastUpdatedBy = "*当前用户*";
        FlagApp = false;
    }

    #region Property Members

    /// <summary>
    /// 自增ID
    /// </summary>
    [DataMember]
    [Key]
    [Identity]
    [Sort(IsDesc)]
    [Column(FieldISID, DisISID)]
    public int ISID { get; set; }

    /// <summary>
    /// 分组名称
    /// </summary>
    [DataMember]
    [Column(FieldGroupName, DisGroupName)]
    public string GroupName { get; set; }

    /// <summary>
    /// 分组类型
    /// </summary>
    [DataMember]
    [Column(FieldGroupType, DisGroupType)]
    public string GroupType { get; set; }

    /// <summary>
    /// 费用类型
    /// </summary>
    [DataMember]
    [Column(FieldCostType, DisCostType)]
    public string CostType { get; set; }

    /// <summary>
    /// 分组区域
    /// </summary>
    [DataMember]
    [Column(FieldGroupContent, DisGroupContent)]
    public string GroupContent { get; set; }

    /// <summary>
    /// 排除区域
    /// </summary>
    [DataMember]
    [Column(FieldGroupExceptContent, DisGroupExceptContent)]
    public string GroupExceptContent { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [DataMember]
    [Column(FieldRemark, DisRemark)]
    public string Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
    [Column(FieldCreationDate, DisCreationDate)]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [DataMember]
    [Column(FieldCreatedBy, DisCreatedBy)]
    public string CreatedBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [DataMember]
    [OptimisticLock]
    [Column(FieldLastUpdateDate, DisLastUpdateDate)]
    public DateTime LastUpdateDate { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    [DataMember]
    [Column(FieldLastUpdatedBy, DisLastUpdatedBy)]
    public string LastUpdatedBy { get; set; }

    /// <summary>
    /// 审批
    /// </summary>
    [DataMember]
    [Column(FieldFlagApp, DisFlagApp)]
    public bool FlagApp { get; set; }

    /// <summary>
    /// 审批人
    /// </summary>
    [DataMember]
    [Column(FieldAppUser, DisAppUser)]
    public string AppUser { get; set; }

    /// <summary>
    /// 审批时间
    /// </summary>
    [DataMember]
    [Column(FieldAppDate, DisAppDate)]
    public DateTime? AppDate { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_BasicGroupList";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldISID;

    /// <summary>
    /// 排序字段
    /// </summary>
    [NonSerialized]
    public new const string SortKey = FieldISID;

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

    #region 列名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string FieldISID = "ISID";

    /// <summary>
    /// 分组名称
    /// </summary>
    [NonSerialized]
    public const string FieldGroupName = "GroupName";

    /// <summary>
    /// 分组类型
    /// </summary>
    [NonSerialized]
    public const string FieldGroupType = "GroupType";

    /// <summary>
    /// 费用类型
    /// </summary>
    [NonSerialized]
    public const string FieldCostType = "CostType";

    /// <summary>
    /// 分组区域
    /// </summary>
    [NonSerialized]
    public const string FieldGroupContent = "GroupContent";

    /// <summary>
    /// 排除区域
    /// </summary>
    [NonSerialized]
    public const string FieldGroupExceptContent = "GroupExceptContent";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string FieldRemark = "Remark";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string FieldCreationDate = "CreationDate";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string FieldCreatedBy = "CreatedBy";

    /// <summary>
    /// 修改时间
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdateDate = "LastUpdateDate";

    /// <summary>
    /// 修改人
    /// </summary>
    [NonSerialized]
    public const string FieldLastUpdatedBy = "LastUpdatedBy";

    /// <summary>
    /// 审批
    /// </summary>
    [NonSerialized]
    public const string FieldFlagApp = "FlagApp";

    /// <summary>
    /// 审批人
    /// </summary>
    [NonSerialized]
    public const string FieldAppUser = "AppUser";

    /// <summary>
    /// 审批时间
    /// </summary>
    [NonSerialized]
    public const string FieldAppDate = "AppDate";

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 分组名称
    /// </summary>
    [NonSerialized]
    public const string DisGroupName = "分组名称";

    /// <summary>
    /// 分组类型
    /// </summary>
    [NonSerialized]
    public const string DisGroupType = "分组类型";

    /// <summary>
    /// 费用类型
    /// </summary>
    [NonSerialized]
    public const string DisCostType = "费用类型";

    /// <summary>
    /// 分组区域
    /// </summary>
    [NonSerialized]
    public const string DisGroupContent = "分组区域";

    /// <summary>
    /// 排除区域
    /// </summary>
    [NonSerialized]
    public const string DisGroupExceptContent = "排除区域";

    /// <summary>
    /// 备注
    /// </summary>
    [NonSerialized]
    public const string DisRemark = "备注";

    /// <summary>
    /// 创建时间
    /// </summary>
    [NonSerialized]
    public const string DisCreationDate = "创建时间";

    /// <summary>
    /// 创建人
    /// </summary>
    [NonSerialized]
    public const string DisCreatedBy = "创建人";

    /// <summary>
    /// 修改时间
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdateDate = "修改时间";

    /// <summary>
    /// 修改人
    /// </summary>
    [NonSerialized]
    public const string DisLastUpdatedBy = "修改人";

    /// <summary>
    /// 审批
    /// </summary>
    [NonSerialized]
    public const string DisFlagApp = "审批";

    /// <summary>
    /// 审批人
    /// </summary>
    [NonSerialized]
    public const string DisAppUser = "审批人";

    /// <summary>
    /// 审批时间
    /// </summary>
    [NonSerialized]
    public const string DisAppDate = "审批时间";

    #endregion

    #endregion
}