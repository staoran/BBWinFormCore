using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 签收表 接口请求类
/// </summary>
public class WaybillSignHttpService : BaseHttpService<WaybillSign>
{
    public WaybillSignHttpService(IWaybillSignHttpService httpService) : base(httpService)
    {
    }
}