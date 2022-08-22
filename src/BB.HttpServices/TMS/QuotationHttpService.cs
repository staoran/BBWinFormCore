using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 普通报价 业务逻辑类
/// </summary>
public class QuotationHttpService : BaseHttpService<Quotation>
{
    public QuotationHttpService(IQuotationHttpService httpService) : base(httpService)
    {
    }
}