using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 回单操作记录 接口请求类
/// </summary>
public class WaybillAckRecsHttpService : BaseHttpService<WaybillAckRecs>
{
    public WaybillAckRecsHttpService(IWaybillAckRecsHttpService httpService) : base(httpService)
    {
    }
}