using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 运单线路表 接口请求类
/// </summary>
public class WaybillSegmentHttpService : BaseHttpService<WaybillSegment>
{
    public WaybillSegmentHttpService(IWaybillSegmentHttpService httpService) : base(httpService)
    {
    }
}