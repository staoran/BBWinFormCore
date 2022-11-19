using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 配载明细表 接口请求类
/// </summary>
public class StowagesHttpService : BaseHttpService<Stowages>
{
    public StowagesHttpService(IStowagesHttpService httpService) : base(httpService)
    {
    }
}