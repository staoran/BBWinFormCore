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

    /// <summary>
    /// 根据相关信息，写入用户的操作日志记录
    /// </summary>
    /// <param name="userId">操作用户</param>
    /// <param name="tableName">操作表名称</param>
    /// <param name="operationType">操作类型</param>
    /// <param name="note">操作详细表述</param>
    /// <returns></returns>
    public async Task<bool> OnOperationLog(string userId, string tableName, string operationType, string note)
    {
        return (await _operationLogHttpService.OnOperationLog(userId, tableName, operationType, note)).Handling();
    }
}