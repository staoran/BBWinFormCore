using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 运单货物明细 接口请求类
/// </summary>
public class WaybillsHttpService : BaseHttpService<Waybills>
{
    public WaybillsHttpService(IWaybillsHttpService httpService) : base(httpService)
    {
    }
}