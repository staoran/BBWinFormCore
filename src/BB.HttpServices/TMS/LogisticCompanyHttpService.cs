using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 承运商资料 业务逻辑类
/// </summary>
public class LogisticCompanyHttpService : BaseHttpService<LogisticCompany>
{
    public LogisticCompanyHttpService(ILogisticCompanyHttpService httpService) : base(httpService)
    {
    }
}