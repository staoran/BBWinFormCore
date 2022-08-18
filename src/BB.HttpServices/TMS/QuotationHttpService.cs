using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 普通报价 业务逻辑类
/// </summary>
public class QuotationService : BaseHttpService<Quotation>
{
    public QuotationService(IQuotationHttpService httpService) : base(httpService)
    {
    }
}