using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 车辆档案 业务逻辑类
/// </summary>
public class CarHttpService : BaseHttpService<Car>
{
    public CarHttpService(ICarHttpService httpService) : base(httpService)
    {
    }
}