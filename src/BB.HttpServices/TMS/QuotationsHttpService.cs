using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 普通报价明细 业务逻辑类
/// </summary>
public class QuotationsHttpService : BaseHttpService<Quotations>
{
    public QuotationsHttpService(IQuotationsHttpService httpService) : base(httpService)
    {
    }
}