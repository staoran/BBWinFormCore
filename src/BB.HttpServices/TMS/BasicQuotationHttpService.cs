using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 公式报价 业务逻辑类
/// </summary>
public class BasicQuotationHttpService : BaseHttpService<BasicQuotation>
{
    public BasicQuotationHttpService(IBasicQuotationHttpService httpService) : base(httpService)
    {
    }
}