using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 费用调整 业务逻辑类
/// </summary>
public class CostMsgHttpService : BaseHttpService<CostMsg>
{
    public CostMsgHttpService(ICostMsgHttpService httpService) : base(httpService)
    {
    }
}