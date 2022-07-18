using BB.Entity.Base;

namespace BB.Core.DbContext;

/// <summary>
/// 仓储基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseRepository<T> : SimpleClient<T> where T : BaseEntity, new()
{
    public BaseRepository(IUnitOfWork unitOfWork) : base(unitOfWork.GetDbClient())
    {
        Context = unitOfWork.GetDbClient();
    }
}