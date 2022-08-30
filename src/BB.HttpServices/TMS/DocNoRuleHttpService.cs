using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 单号规则 业务逻辑类
/// </summary>
public class DocNoRuleHttpService : BaseHttpService<DocNoRule>
{
    private readonly IDocNoRuleHttpService _httpService;

    public DocNoRuleHttpService(IDocNoRuleHttpService httpService) : base(httpService)
    {
        _httpService = httpService;
    }

    /// <summary>
    /// 获取单据流水号
    /// </summary>
    /// <param name="docCode">单据字头</param>
    /// <returns></returns>
    public async Task<string> GetSNNoAsync(string docCode)
    {
        return (await _httpService.GetSNNoAsync(docCode)).Handling();
    }
}