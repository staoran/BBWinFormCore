using BB.Entity.TMS;
using BB.HttpServices.Base;
using Furion.UnifyResult;

namespace BB.HttpServices.TMS;

public interface IDocNoRuleHttpService : IBaseHttpService<DocNoRule>
{
    /// <summary>
    /// 获取单据流水号
    /// </summary>
    /// <param name="docCode">单据字头</param>
    /// <returns></returns>
    Task<RESTfulResult<string>> GetSNNoAsync(string docCode);
}