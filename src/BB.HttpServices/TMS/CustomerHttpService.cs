using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 客户管理 业务逻辑类
/// </summary>
public class CustomerService : BaseHttpService<Customer>
{
    public CustomerService(ICustomerHttpService httpService) : base(httpService)
    {
    }
}