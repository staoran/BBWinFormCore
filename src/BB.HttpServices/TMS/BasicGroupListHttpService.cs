using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 区域分组 业务逻辑类
/// </summary>
public class BasicGroupListHttpService : BaseHttpService<BasicGroupList>
{
    public BasicGroupListHttpService(IBasicGroupListHttpService httpService) : base(httpService)
    {
    }
}