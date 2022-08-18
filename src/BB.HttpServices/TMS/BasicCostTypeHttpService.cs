using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 费用类型 业务逻辑类
/// </summary>
public class BasicCostTypeHttpService : BaseHttpService<BasicCostType>
{
    public BasicCostTypeHttpService(IBasicCostTypeHttpService httpService) : base(httpService)
    {
    }
}