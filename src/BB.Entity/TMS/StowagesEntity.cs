using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 配载明细表 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Stowages : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public Stowages()
    {
        InputType = "1";
        AckRecQty = 0;
        Qty = 0;
        Weight = 0;
        Cubage = 0;
        UnloadYN = false;
        UpstairYN = false;
        UpstairNum = 0;
        SmsYN = false;
        StowageCarriage = 0;
        CreationDate = DateTime.Now;
        CreatedBy = "*当前用户*";
        LastUpdateDate = DateTime.Now;
        LastUpdatedBy = "*当前用户*";
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
    /// 配载编号
    /// </summary>
    [DataMember]
    [Column(FieldStowageNo, DisStowageNo)]
    public string StowageNo { get; set; }

    /// <summary>
    /// 导入类型
    /// </summary>
    [DataMember]
    [Column(FieldInputType, DisInputType)]
    public string InputType { get; set; }

    /// <summary>
    /// 运单编号
    /// </summary>
    [DataMember]
    [Column(FieldWaybillNo, DisWaybillNo)]
    public string WaybillNo { get; set; }

    /// <summary>
    /// 发货网点
    /// </summary>
    [DataMember]
    [Column(FieldFromNode, DisFromNode)]
    public string FromNode { get; set; }

    /// <summary>
    /// 发货区域
    /// </summary>
    [DataMember]
    [Column(FieldFromNodes, DisFromNodes)]
    public int FromNodes { get; set; }

    /// <summary>
    /// 目的网点
    /// </summary>
    [DataMember]
    [Column(FieldToNode, DisToNode)]
    public string ToNode { get; set; }

    /// <summary>
    /// 目的区域
    /// </summary>
    [DataMember]
    [Column(FieldToNodes, DisToNodes)]
    public int ToNodes { get; set; }

    /// <summary>
    /// 收货公司
    /// </summary>
    [DataMember]
    [Column(FieldConsigneeCompanyName, DisConsigneeCompanyName)]
    public string ConsigneeCompanyName { get; set; }

    /// <summary>
    /// 收货地址
    /// </summary>
    [DataMember]
    [Column(FieldConsigneeAddress, DisConsigneeAddress)]
    public string ConsigneeAddress { get; set; }

    /// <summary>
    /// 收货电话
    /// </summary>
    [DataMember]
    [Column(FieldConsigneeTel, DisConsigneeTel)]
    public string ConsigneeTel { get; set; }

    /// <summary>
    /// 收货人
    /// </summary>
    [DataMember]
    [Column(FieldConsignee, DisConsignee)]
    public string Consignee { get; set; }

    /// <summary>
    /// 交货方式
    /// </summary>
    [DataMember]
    [Column(FieldDeliveryType, DisDeliveryType)]
    public string DeliveryType { get; set; }

    /// <summary>
    /// 付款方式
    /// </summary>
    [DataMember]
    [Column(FieldPaymentType, DisPaymentType)]
    public string PaymentType { get; set; }

    /// <summary>
    /// 回单数量
    /// </summary>
    [DataMember]
    [Column(FieldAckRecQty, DisAckRecQty)]
    public int AckRecQty { get; set; }

    /// <summary>
    /// 回单类型
    /// </summary>
    [DataMember]
    [Column(FieldAckRecType, DisAckRecType)]
    public string AckRecType { get; set; }

    /// <summary>
    /// 回单号
    /// </summary>
    [DataMember]
    [Column(FieldAckRecNo, DisAckRecNo)]
    public string AckRecNo { get; set; }

    /// <summary>
    /// 数量
    /// </summary>
    [DataMember]
    [Column(FieldQty, DisQty)]
    public int Qty { get; set; }

    /// <summary>
    /// 重量
    /// </summary>
    [DataMember]
    [Column(FieldWeight, DisWeight)]
    public decimal Weight { get; set; }

    /// <summary>
    /// 体积
    /// </summary>
    [DataMember]
    [Column(FieldCubage, DisCubage)]
    public decimal Cubage { get; set; }

    /// <summary>
    /// 是否卸货
    /// </summary>
    [DataMember]
    [Column(FieldUnloadYN, DisUnloadYN)]
    public bool UnloadYN { get; set; }

    /// <summary>
    /// 是否上楼
    /// </summary>
    [DataMember]
    [Column(FieldUpstairYN, DisUpstairYN)]
    public bool UpstairYN { get; set; }

    /// <summary>
    /// 楼层
    /// </summary>
    [DataMember]
    [Column(FieldUpstairNum, DisUpstairNum)]
    public int UpstairNum { get; set; }

    /// <summary>
    /// 是否短信
    /// </summary>
    [DataMember]
    [Column(FieldSmsYN, DisSmsYN)]
    public bool SmsYN { get; set; }

    /// <summary>
    /// 分摊费用
    /// </summary>
    [DataMember]
    [Column(FieldStowageCarriage, DisStowageCarriage)]
    public decimal StowageCarriage { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [DataMember]
    [Column(FieldStatusID, DisStatusID)]
    public string StatusID { get; set; }

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
    [Sort(IsDesc)]
    [Hide]
    [Column(FieldCreationDate, DisCreationDate)]
    public DateTime CreationDate { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldCreatedBy, DisCreatedBy)]
    public string CreatedBy { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [DataMember]
    [OptimisticLock]
    [Hide]
    [Column(FieldLastUpdateDate, DisLastUpdateDate)]
    public DateTime LastUpdateDate { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldLastUpdatedBy, DisLastUpdatedBy)]
    public string LastUpdatedBy { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranStowages";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldISID;

    /// <summary>
    /// 外键名
    /// </summary>
    [NonSerialized]
    public new const string ForeignKey = FieldStowageNo;

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
    /// 配载编号
    /// </summary>
    [NonSerialized]
    public const string FieldStowageNo = "StowageNo";

    /// <summary>
    /// 导入类型
    /// </summary>
    [NonSerialized]
    public const string FieldInputType = "InputType";

    /// <summary>
    /// 运单编号
    /// </summary>
    [NonSerialized]
    public const string FieldWaybillNo = "WaybillNo";

    /// <summary>
    /// 发货网点
    /// </summary>
    [NonSerialized]
    public const string FieldFromNode = "FromNode";

    /// <summary>
    /// 发货区域
    /// </summary>
    [NonSerialized]
    public const string FieldFromNodes = "FromNodes";

    /// <summary>
    /// 目的网点
    /// </summary>
    [NonSerialized]
    public const string FieldToNode = "ToNode";

    /// <summary>
    /// 目的区域
    /// </summary>
    [NonSerialized]
    public const string FieldToNodes = "ToNodes";

    /// <summary>
    /// 收货公司
    /// </summary>
    [NonSerialized]
    public const string FieldConsigneeCompanyName = "ConsigneeCompanyName";

    /// <summary>
    /// 收货地址
    /// </summary>
    [NonSerialized]
    public const string FieldConsigneeAddress = "ConsigneeAddress";

    /// <summary>
    /// 收货电话
    /// </summary>
    [NonSerialized]
    public const string FieldConsigneeTel = "ConsigneeTel";

    /// <summary>
    /// 收货人
    /// </summary>
    [NonSerialized]
    public const string FieldConsignee = "Consignee";

    /// <summary>
    /// 交货方式
    /// </summary>
    [NonSerialized]
    public const string FieldDeliveryType = "DeliveryType";

    /// <summary>
    /// 付款方式
    /// </summary>
    [NonSerialized]
    public const string FieldPaymentType = "PaymentType";

    /// <summary>
    /// 回单数量
    /// </summary>
    [NonSerialized]
    public const string FieldAckRecQty = "AckRecQty";

    /// <summary>
    /// 回单类型
    /// </summary>
    [NonSerialized]
    public const string FieldAckRecType = "AckRecType";

    /// <summary>
    /// 回单号
    /// </summary>
    [NonSerialized]
    public const string FieldAckRecNo = "AckRecNo";

    /// <summary>
    /// 数量
    /// </summary>
    [NonSerialized]
    public const string FieldQty = "Qty";

    /// <summary>
    /// 重量
    /// </summary>
    [NonSerialized]
    public const string FieldWeight = "Weight";

    /// <summary>
    /// 体积
    /// </summary>
    [NonSerialized]
    public const string FieldCubage = "Cubage";

    /// <summary>
    /// 是否卸货
    /// </summary>
    [NonSerialized]
    public const string FieldUnloadYN = "UnloadYN";

    /// <summary>
    /// 是否上楼
    /// </summary>
    [NonSerialized]
    public const string FieldUpstairYN = "UpstairYN";

    /// <summary>
    /// 楼层
    /// </summary>
    [NonSerialized]
    public const string FieldUpstairNum = "UpstairNum";

    /// <summary>
    /// 是否短信
    /// </summary>
    [NonSerialized]
    public const string FieldSmsYN = "SmsYN";

    /// <summary>
    /// 分摊费用
    /// </summary>
    [NonSerialized]
    public const string FieldStowageCarriage = "StowageCarriage";

    /// <summary>
    /// 状态
    /// </summary>
    [NonSerialized]
    public const string FieldStatusID = "StatusID";

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

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 配载编号
    /// </summary>
    [NonSerialized]
    public const string DisStowageNo = "配载编号";

    /// <summary>
    /// 导入类型
    /// </summary>
    [NonSerialized]
    public const string DisInputType = "导入类型";

    /// <summary>
    /// 运单编号
    /// </summary>
    [NonSerialized]
    public const string DisWaybillNo = "运单编号";

    /// <summary>
    /// 发货网点
    /// </summary>
    [NonSerialized]
    public const string DisFromNode = "发货网点";

    /// <summary>
    /// 发货区域
    /// </summary>
    [NonSerialized]
    public const string DisFromNodes = "发货区域";

    /// <summary>
    /// 目的网点
    /// </summary>
    [NonSerialized]
    public const string DisToNode = "目的网点";

    /// <summary>
    /// 目的区域
    /// </summary>
    [NonSerialized]
    public const string DisToNodes = "目的区域";

    /// <summary>
    /// 收货公司
    /// </summary>
    [NonSerialized]
    public const string DisConsigneeCompanyName = "收货公司";

    /// <summary>
    /// 收货地址
    /// </summary>
    [NonSerialized]
    public const string DisConsigneeAddress = "收货地址";

    /// <summary>
    /// 收货电话
    /// </summary>
    [NonSerialized]
    public const string DisConsigneeTel = "收货电话";

    /// <summary>
    /// 收货人
    /// </summary>
    [NonSerialized]
    public const string DisConsignee = "收货人";

    /// <summary>
    /// 交货方式
    /// </summary>
    [NonSerialized]
    public const string DisDeliveryType = "交货方式";

    /// <summary>
    /// 付款方式
    /// </summary>
    [NonSerialized]
    public const string DisPaymentType = "付款方式";

    /// <summary>
    /// 回单数量
    /// </summary>
    [NonSerialized]
    public const string DisAckRecQty = "回单数量";

    /// <summary>
    /// 回单类型
    /// </summary>
    [NonSerialized]
    public const string DisAckRecType = "回单类型";

    /// <summary>
    /// 回单号
    /// </summary>
    [NonSerialized]
    public const string DisAckRecNo = "回单号";

    /// <summary>
    /// 数量
    /// </summary>
    [NonSerialized]
    public const string DisQty = "数量";

    /// <summary>
    /// 重量
    /// </summary>
    [NonSerialized]
    public const string DisWeight = "重量";

    /// <summary>
    /// 体积
    /// </summary>
    [NonSerialized]
    public const string DisCubage = "体积";

    /// <summary>
    /// 是否卸货
    /// </summary>
    [NonSerialized]
    public const string DisUnloadYN = "是否卸货";

    /// <summary>
    /// 是否上楼
    /// </summary>
    [NonSerialized]
    public const string DisUpstairYN = "是否上楼";

    /// <summary>
    /// 楼层
    /// </summary>
    [NonSerialized]
    public const string DisUpstairNum = "楼层";

    /// <summary>
    /// 是否短信
    /// </summary>
    [NonSerialized]
    public const string DisSmsYN = "是否短信";

    /// <summary>
    /// 分摊费用
    /// </summary>
    [NonSerialized]
    public const string DisStowageCarriage = "分摊费用";

    /// <summary>
    /// 状态
    /// </summary>
    [NonSerialized]
    public const string DisStatusID = "状态";

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

    #endregion

    #endregion
}