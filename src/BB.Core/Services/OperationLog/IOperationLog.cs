using System.Threading.Tasks;
using BB.Core.Services.Base;
using BB.Entity.Security;

namespace BB.Core.Services.OperationLog;

public interface IOperationLog : IBaseService<OperationLogInfo>
{
    /// <summary>
    /// 根据相关信息，写入用户的操作日志记录
    /// </summary>
    /// <param name="userId">操作用户</param>
    /// <param name="tableName">操作表名称</param>
    /// <param name="operationType">操作类型</param>
    /// <param name="note">操作详细表述</param>
    /// <returns></returns>
    Task<bool> OnOperationLog(string userId, string tableName, string operationType, string note);
}