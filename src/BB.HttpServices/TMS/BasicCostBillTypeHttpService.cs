using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 预付金操作类型 业务逻辑类
/// </summary>
public class BasicCostBillTypeHttpService : BaseHttpService<BasicCostBillType>
{
    public BasicCostBillTypeHttpService(IBasicCostBillTypeHttpService httpService) : base(httpService)
    {
    }
}