using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 线路表 业务逻辑类
/// </summary>
public class SegmentHttpService : BaseHttpService<Segment>
{
    public SegmentHttpService(ISegmentHttpService httpService) : base(httpService)
    {
    }
}