using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 公式报价 业务逻辑类
/// </summary>
public class BasicQuotationService : BaseHttpService<BasicQuotation>
{
    public BasicQuotationService(IBasicQuotationHttpService httpService) : base(httpService)
    {
    }
}