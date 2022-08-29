using BB.Entity.TMS;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.TMS;

public interface ICustomerHttpService : IHttpDispatchProxy, IBaseHttpService<Customer>
{
}