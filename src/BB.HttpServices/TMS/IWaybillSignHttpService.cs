using BB.Entity.TMS;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.TMS;

public interface IWaybillSignHttpService : IHttpDispatchProxy, IBaseHttpService<WaybillSign>
{
    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}segment/";
        // req.BaseAddress = builder.Uri;
    }
}