using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 网点区域 业务逻辑类
/// </summary>
public class NodesHttpService : BaseHttpService<Nodes>
{
    public NodesHttpService(INodesHttpService httpService) : base(httpService)
    {
    }
}