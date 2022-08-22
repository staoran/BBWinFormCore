using BB.Entity.Security;
using BB.HttpServices.Base;

namespace BB.HttpServices.Core.OperationLog;

public class OperationLogHttpService : BaseHttpService<OperationLogInfo>
{
    private readonly IOperationLogHttpService _operationLogHttpService;

    public OperationLogHttpService(IOperationLogHttpService operationLogHttpService) : base(operationLogHttpService)
    {
        _operationLogHttpService = operationLogHttpService;
    }
}