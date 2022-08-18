using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 预付金管理 业务逻辑类
/// </summary>
public class CostBillHttpService : BaseHttpService<CostBill>
{
    public CostBillHttpService(ICostBillHttpService httpService) : base(httpService)
    {
    }
}