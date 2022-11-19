using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 送货配载 接口请求类
/// </summary>
public class StowageHttpService : BaseHttpService<Stowage>
{
    public StowageHttpService(IStowageHttpService httpService) : base(httpService)
    {
    }
}