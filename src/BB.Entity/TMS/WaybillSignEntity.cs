using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 签收表 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class WaybillSign : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理，复杂的属性值初始化通过重写 _bll.NewEntity 方法实现）
    /// </summary>
    public WaybillSign()
    {
        TranNode = "*当前机构*";
        AckRecYN = false;
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
    /// 运单编号
    /// </summary>
    [DataMember]
    [Column(FieldWaybillNo, DisWaybillNo)]
    public string WaybillNo { get; set; }

    /// <summary>
    /// 目的网点编号
    /// </summary>
    [DataMember]
    [Column(FieldTranNode, DisTranNode)]
    public string TranNode { get; set; }

    /// <summary>
    /// 目的区域编号
    /// </summary>
    [DataMember]
    [Column(FieldTranNodes, DisTranNodes)]
    public int TranNodes { get; set; }

    /// <summary>
    /// 交货类型
    /// </summary>
    [DataMember]
    [Column(FieldDeliveryType, DisDeliveryType)]
    public string DeliveryType { get; set; }

    /// <summary>
    /// 签收人
    /// </summary>
    [DataMember]
    [Column(FieldConsignee, DisConsignee)]
    public string Consignee { get; set; }

    /// <summary>
    /// 签收人证件编号
    /// </summary>
    [DataMember]
    [Column(FieldConsigneeid, DisConsigneeid)]
    public string Consigneeid { get; set; }

    /// <summary>
    /// 签收图片地址
    /// </summary>
    [DataMember]
    [Column(FieldConsigneeidPicAdds, DisConsigneeidPicAdds)]
    public string ConsigneeidPicAdds { get; set; }

    /// <summary>
    /// 签收备注
    /// </summary>
    [DataMember]
    [Column(FieldConsigneeRemark, DisConsigneeRemark)]
    public string ConsigneeRemark { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    [DataMember]
    [Column(FieldQty, DisQty)]
    public int Qty { get; set; }

    /// <summary>
    /// 签收数量
    /// </summary>
    [DataMember]
    [Column(FieldSignQty, DisSignQty)]
    public int SignQty { get; set; }

    /// <summary>
    /// 回单签收
    /// </summary>
    [DataMember]
    [Column(FieldAckRecYN, DisAckRecYN)]
    public bool AckRecYN { get; set; }

    /// <summary>
    /// 回单编号
    /// </summary>
    [DataMember]
    [Column(FieldAckRecNo, DisAckRecNo)]
    public string AckRecNo { get; set; }

    /// <summary>
    /// 回单数量
    /// </summary>
    [DataMember]
    [Column(FieldAckRecQty, DisAckRecQty)]
    public int? AckRecQty { get; set; }

    /// <summary>
    /// 回单备注
    /// </summary>
    [DataMember]
    [Column(FieldAckRecRemark, DisAckRecRemark)]
    public string AckRecRemark { get; set; }

    /// <summary>
    /// 相关人员
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldRelatedUser, DisRelatedUser)]
    public string RelatedUser { get; set; }

    /// <summary>
    /// 签收状态
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldSignStatus, DisSignStatus)]
    public string SignStatus { get; set; }

    /// <summary>
    /// 签收状态备注
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldSignStatusRemark, DisSignStatusRemark)]
    public string SignStatusRemark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [DataMember]
    [Sort(IsDesc)]
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
    /// 审核
    /// </summary>
    [DataMember]
    [Column(FieldFlagApp, DisFlagApp)]
    public bool FlagApp { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    [DataMember]
    [Column(FieldAppUser, DisAppUser)]
    public string AppUser { get; set; }

    /// <summary>
    /// 审核时间
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
    public new const string DBTableName = "tb_TranWaybillSign";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldISID;

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

    #region 列名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string FieldISID = "ISID";

    /// <summary>
    /// 运单编号
    /// </summary>
    [NonSerialized]
    public const string FieldWaybillNo = "WaybillNo";

    /// <summary>
    /// 目的网点编号
    /// </summary>
    [NonSerialized]
    public const string FieldTranNode = "TranNode";

    /// <summary>
    /// 目的区域编号
    /// </summary>
    [NonSerialized]
    public const string FieldTranNodes = "TranNodes";

    /// <summary>
    /// 交货类型
    /// </summary>
    [NonSerialized]
    public const string FieldDeliveryType = "DeliveryType";

    /// <summary>
    /// 签收人
    /// </summary>
    [NonSerialized]
    public const string FieldConsignee = "Consignee";

    /// <summary>
    /// 签收人证件编号
    /// </summary>
    [NonSerialized]
    public const string FieldConsigneeid = "Consigneeid";

    /// <summary>
    /// 签收图片地址
    /// </summary>
    [NonSerialized]
    public const string FieldConsigneeidPicAdds = "ConsigneeidPicAdds";

    /// <summary>
    /// 签收备注
    /// </summary>
    [NonSerialized]
    public const string FieldConsigneeRemark = "ConsigneeRemark";

    /// <summary>
    /// 数量
    /// </summary>
    [NonSerialized]
    public const string FieldQty = "Qty";

    /// <summary>
    /// 签收数量
    /// </summary>
    [NonSerialized]
    public const string FieldSignQty = "SignQty";

    /// <summary>
    /// 回单签收
    /// </summary>
    [NonSerialized]
    public const string FieldAckRecYN = "AckRecYN";

    /// <summary>
    /// 回单编号
    /// </summary>
    [NonSerialized]
    public const string FieldAckRecNo = "AckRecNo";

    /// <summary>
    /// 回单数量
    /// </summary>
    [NonSerialized]
    public const string FieldAckRecQty = "AckRecQty";

    /// <summary>
    /// 回单备注
    /// </summary>
    [NonSerialized]
    public const string FieldAckRecRemark = "AckRecRemark";

    /// <summary>
    /// 相关人员
    /// </summary>
    [NonSerialized]
    public const string FieldRelatedUser = "RelatedUser";

    /// <summary>
    /// 签收状态
    /// </summary>
    [NonSerialized]
    public const string FieldSignStatus = "SignStatus";

    /// <summary>
    /// 签收状态备注
    /// </summary>
    [NonSerialized]
    public const string FieldSignStatusRemark = "SignStatusRemark";

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
    /// 审核
    /// </summary>
    [NonSerialized]
    public const string FieldFlagApp = "FlagApp";

    /// <summary>
    /// 审核人
    /// </summary>
    [NonSerialized]
    public const string FieldAppUser = "AppUser";

    /// <summary>
    /// 审核时间
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
    /// 运单编号
    /// </summary>
    [NonSerialized]
    public const string DisWaybillNo = "运单编号";

    /// <summary>
    /// 目的网点编号
    /// </summary>
    [NonSerialized]
    public const string DisTranNode = "目的网点编号";

    /// <summary>
    /// 目的区域编号
    /// </summary>
    [NonSerialized]
    public const string DisTranNodes = "目的区域编号";

    /// <summary>
    /// 交货类型
    /// </summary>
    [NonSerialized]
    public const string DisDeliveryType = "交货类型";

    /// <summary>
    /// 签收人
    /// </summary>
    [NonSerialized]
    public const string DisConsignee = "签收人";

    /// <summary>
    /// 签收人证件编号
    /// </summary>
    [NonSerialized]
    public const string DisConsigneeid = "签收人证件编号";

    /// <summary>
    /// 签收图片地址
    /// </summary>
    [NonSerialized]
    public const string DisConsigneeidPicAdds = "签收图片地址";

    /// <summary>
    /// 签收备注
    /// </summary>
    [NonSerialized]
    public const string DisConsigneeRemark = "签收备注";

    /// <summary>
    /// 数量
    /// </summary>
    [NonSerialized]
    public const string DisQty = "数量";

    /// <summary>
    /// 签收数量
    /// </summary>
    [NonSerialized]
    public const string DisSignQty = "签收数量";

    /// <summary>
    /// 回单签收
    /// </summary>
    [NonSerialized]
    public const string DisAckRecYN = "回单签收";

    /// <summary>
    /// 回单编号
    /// </summary>
    [NonSerialized]
    public const string DisAckRecNo = "回单编号";

    /// <summary>
    /// 回单数量
    /// </summary>
    [NonSerialized]
    public const string DisAckRecQty = "回单数量";

    /// <summary>
    /// 回单备注
    /// </summary>
    [NonSerialized]
    public const string DisAckRecRemark = "回单备注";

    /// <summary>
    /// 相关人员
    /// </summary>
    [NonSerialized]
    public const string DisRelatedUser = "相关人员";

    /// <summary>
    /// 签收状态
    /// </summary>
    [NonSerialized]
    public const string DisSignStatus = "签收状态";

    /// <summary>
    /// 签收状态备注
    /// </summary>
    [NonSerialized]
    public const string DisSignStatusRemark = "签收状态备注";

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
    /// 审核
    /// </summary>
    [NonSerialized]
    public const string DisFlagApp = "审核";

    /// <summary>
    /// 审核人
    /// </summary>
    [NonSerialized]
    public const string DisAppUser = "审核人";

    /// <summary>
    /// 审核时间
    /// </summary>
    [NonSerialized]
    public const string DisAppDate = "审核时间";

    #endregion

    #endregion
}