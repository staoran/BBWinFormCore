using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 费用调整确认 业务逻辑类
/// </summary>
public class CostMsgsHttpService : BaseHttpService<CostMsgs>
{
    public CostMsgsHttpService(ICostMsgsHttpService httpService) : base(httpService)
    {
    }
}