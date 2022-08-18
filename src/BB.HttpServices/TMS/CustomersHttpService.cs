using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 客户收货人 业务逻辑类
/// </summary>
public class CustomersHttpService : BaseHttpService<Customers>
{
    public CustomersHttpService(ICustomersHttpService httpService) : base(httpService)
    {
    }
}