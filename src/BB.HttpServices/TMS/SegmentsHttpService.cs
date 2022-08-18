using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 线路报价明细 业务逻辑类
/// </summary>
public class SegmentsHttpService : BaseHttpService<Segments>
{
    public SegmentsHttpService(ISegmentsHttpService httpService) : base(httpService)
    {
    }
}