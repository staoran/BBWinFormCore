using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 公式报价明细 业务逻辑类
/// </summary>
public class BasicQuotationsHttpService : BaseHttpService<BasicQuotations>
{
    public BasicQuotationsHttpService(IBasicQuotationsHttpService httpService) : base(httpService)
    {
    }
}