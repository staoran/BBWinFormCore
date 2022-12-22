using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 运单管理 接口请求类
/// </summary>
public class WaybillHttpService : BaseHttpService<Waybill>
{
    public WaybillHttpService(IWaybillHttpService httpService) : base(httpService)
    {
    }
}