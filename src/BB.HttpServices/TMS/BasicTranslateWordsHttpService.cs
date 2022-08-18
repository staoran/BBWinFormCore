using BB.Entity.TMS;
using BB.HttpServices.Base;

namespace BB.HttpServices.TMS;

/// <summary>
/// 公式定义 业务逻辑类
/// </summary>
public class BasicTranslateWordsHttpService : BaseHttpService<BasicTranslateWords>
{
    public BasicTranslateWordsHttpService(IBasicTranslateWordsHttpService httpService) : base(httpService)
    {
    }
}