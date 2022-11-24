using System.Runtime.Serialization;
using BB.Entity.Base;
using BB.Tools.Entity;

namespace BB.Entity.TMS;

/// <summary>
/// 线路报价 实体类
/// </summary>
[DataContract]
[Serializable]
[Table(DBTableName)]
public sealed class Segments : BaseEntity
{
    /// <summary>
    /// 默认构造函数（需要初始化属性的在此处理）
    /// </summary>
    public Segments()
    {
        PayNodeType = "1";
        RecvNodeType = "4";
        OpenTime = DateTime.Now;
        Closetime = DateTime.Now;
        CreationDate = DateTime.Now;
        CreatedBy = "*当前用户*";
        LastUpdateDate = DateTime.Now;
        LastUpdatedBy = "*当前用户*";
        FinancialCenterType = "4";
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
    /// 线路编号
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldSegmentNo, DisSegmentNo)]
    public string SegmentNo { get; set; }

    /// <summary>
    /// 费用类型
    /// </summary>
    [DataMember]
    [Column(FieldCostType, DisCostType)]
    public string CostType { get; set; }

    /// <summary>
    /// 报价编号
    /// </summary>
    [DataMember]
    [Column(FieldQuotationNo, DisQuotationNo)]
    public string QuotationNo { get; set; }

    /// <summary>
    /// 付款网点类型
    /// </summary>
    [DataMember]
    [Column(FieldPayNodeType, DisPayNodeType)]
    public string PayNodeType { get; set; }

    /// <summary>
    /// 付款网点
    /// </summary>
    [DataMember]
    [Column(FieldPayNodeNo, DisPayNodeNo)]
    public string PayNodeNo { get; set; }

    /// <summary>
    /// 收款网点类型
    /// </summary>
    [DataMember]
    [Column(FieldRecvNodeType, DisRecvNodeType)]
    public string RecvNodeType { get; set; }

    /// <summary>
    /// 收款网点
    /// </summary>
    [DataMember]
    [Column(FieldRecvNodeNo, DisRecvNodeNo)]
    public string RecvNodeNo { get; set; }

    /// <summary>
    /// 启用时间
    /// </summary>
    [DataMember]
    [Column(FieldOpenTime, DisOpenTime)]
    public DateTime OpenTime { get; set; }

    /// <summary>
    /// 终止时间
    /// </summary>
    [DataMember]
    [Column(FieldClosetime, DisClosetime)]
    public DateTime Closetime { get; set; }

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
    [Hide]
    [OptimisticLock]
    [Column(FieldLastUpdateDate, DisLastUpdateDate)]
    public DateTime LastUpdateDate { get; set; }

    /// <summary>
    /// 修改人
    /// </summary>
    [DataMember]
    [Hide]
    [Column(FieldLastUpdatedBy, DisLastUpdatedBy)]
    public string LastUpdatedBy { get; set; }

    /// <summary>
    /// 财务中心类型
    /// </summary>
    [DataMember]
    [Column(FieldFinancialCenterType, DisFinancialCenterType)]
    public string FinancialCenterType { get; set; }

    /// <summary>
    /// 财务中心
    /// </summary>
    [DataMember]
    [Column(FieldFinancialCenter, DisFinancialCenter)]
    public string FinancialCenter { get; set; }

    #endregion

    #region 定义字段名称相关常量

    /// <summary>
    /// 表名
    /// </summary>
    [NonSerialized]
    public new const string DBTableName = "tb_TranSegments";

    /// <summary>
    /// 主键名
    /// </summary>
    [NonSerialized]
    public new const string PrimaryKey = FieldISID;

    /// <summary>
    /// 外键名
    /// </summary>
    [NonSerialized]
    public new const string ForeignKey = FieldSegmentNo;

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
    /// 线路编号
    /// </summary>
    [NonSerialized]
    public const string FieldSegmentNo = "SegmentNo";

    /// <summary>
    /// 费用类型
    /// </summary>
    [NonSerialized]
    public const string FieldCostType = "CostType";

    /// <summary>
    /// 报价编号
    /// </summary>
    [NonSerialized]
    public const string FieldQuotationNo = "QuotationNo";

    /// <summary>
    /// 付款网点类型
    /// </summary>
    [NonSerialized]
    public const string FieldPayNodeType = "PayNodeType";

    /// <summary>
    /// 付款网点
    /// </summary>
    [NonSerialized]
    public const string FieldPayNodeNo = "PayNodeNo";

    /// <summary>
    /// 收款网点类型
    /// </summary>
    [NonSerialized]
    public const string FieldRecvNodeType = "RecvNodeType";

    /// <summary>
    /// 收款网点
    /// </summary>
    [NonSerialized]
    public const string FieldRecvNodeNo = "RecvNodeNo";

    /// <summary>
    /// 启用时间
    /// </summary>
    [NonSerialized]
    public const string FieldOpenTime = "OpenTime";

    /// <summary>
    /// 终止时间
    /// </summary>
    [NonSerialized]
    public const string FieldClosetime = "Closetime";

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
    /// 财务中心类型
    /// </summary>
    [NonSerialized]
    public const string FieldFinancialCenterType = "FinancialCenterType";

    /// <summary>
    /// 财务中心
    /// </summary>
    [NonSerialized]
    public const string FieldFinancialCenter = "FinancialCenter";

    #endregion

    #region 列显示名
    /// <summary>
    /// 自增ID
    /// </summary>
    [NonSerialized]
    public const string DisISID = "自增ID";

    /// <summary>
    /// 线路编号
    /// </summary>
    [NonSerialized]
    public const string DisSegmentNo = "线路编号";

    /// <summary>
    /// 费用类型
    /// </summary>
    [NonSerialized]
    public const string DisCostType = "费用类型";

    /// <summary>
    /// 报价编号
    /// </summary>
    [NonSerialized]
    public const string DisQuotationNo = "报价编号";

    /// <summary>
    /// 付款网点类型
    /// </summary>
    [NonSerialized]
    public const string DisPayNodeType = "付款网点类型";

    /// <summary>
    /// 付款网点
    /// </summary>
    [NonSerialized]
    public const string DisPayNodeNo = "付款网点";

    /// <summary>
    /// 收款网点类型
    /// </summary>
    [NonSerialized]
    public const string DisRecvNodeType = "收款网点类型";

    /// <summary>
    /// 收款网点
    /// </summary>
    [NonSerialized]
    public const string DisRecvNodeNo = "收款网点";

    /// <summary>
    /// 启用时间
    /// </summary>
    [NonSerialized]
    public const string DisOpenTime = "启用时间";

    /// <summary>
    /// 终止时间
    /// </summary>
    [NonSerialized]
    public const string DisClosetime = "终止时间";

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
    /// 财务中心类型
    /// </summary>
    [NonSerialized]
    public const string DisFinancialCenterType = "财务中心类型";

    /// <summary>
    /// 财务中心
    /// </summary>
    [NonSerialized]
    public const string DisFinancialCenter = "财务中心";

    #endregion

    #endregion
}