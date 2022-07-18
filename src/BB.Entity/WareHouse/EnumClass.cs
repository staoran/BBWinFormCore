namespace BB.Entity.WareHouse;

/// <summary>
/// 采购进货退货方式
/// </summary>
//[DataEntity]
//[Flags]
public enum PuchaseStatus
{        
    进货,        
    退货
}

/// <summary>
/// 收入支出类型
/// </summary>
public enum IncomeType
{
    收入,
    支出
}

public enum MonthlyReportType
{
    库房部门结存 = 1,
    库房结存 = 2,
    所有库房结存 = 3,
    车间成本月报表 = 4,
    全年费用汇总表 = 100
}

public enum StatisticValueType
{
    /// <summary>
    /// 本月结存数量
    /// </summary>
    CurrentCount,

    /// <summary>
    /// 本月入库数量
    /// </summary>
    CurrentInCount,

    /// <summary>
    /// 本月出库数量
    /// </summary>
    CurrentOutCount,

    /// <summary>
    /// 上月结存数量
    /// </summary>
    LastCount,

    /// <summary>
    /// 本月入库金额
    /// </summary>
    CurrentInMoney,

    /// <summary>
    /// 本月结存金额
    /// </summary>
    CurrentMoney,

    /// <summary>
    /// 本月出库金额
    /// </summary>
    CurrentOutMoney,

    /// <summary>
    /// 上月结存金额
    /// </summary>
    LastMoney
}