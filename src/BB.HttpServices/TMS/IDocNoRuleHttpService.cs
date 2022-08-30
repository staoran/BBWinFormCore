using BB.Entity.TMS;
using BB.HttpServices.Base;
using Furion.RemoteRequest;

namespace BB.HttpServices.TMS;

public interface IDocNoRuleHttpService : IHttpDispatchProxy, IBaseHttpService<DocNoRule>
{
    /// <summary>
    /// 获取单据流水号
    /// </summary>
    /// <param name="docCode">单据字头</param>
    /// <returns></returns>
    [Get("sNNo")]
    Task<RESTfulResultControl<string>> GetSNNoAsync(string docCode);

    /// <summary>
    /// HttpClient 拦截
    /// </summary>
    /// <param name="req"></param>
    [Interceptor(InterceptorTypes.Client)]
    static void OnClientCreating(HttpClient req)
    {
        // var builder = new UriBuilder(req.BaseAddress!);
        // var path = req.BaseAddress!.AbsolutePath;
        // builder.Path = $"{path}docNoRule/";
        // req.BaseAddress = builder.Uri;
    }
}