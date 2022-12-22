using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 运单操作记录 接口请求类
/// </summary>
public class WaybillRecordsHttpService : BaseHttpService<WaybillRecords>
{
    public WaybillRecordsHttpService(IWaybillRecordsHttpService httpService) : base(httpService)
    {
    }
}