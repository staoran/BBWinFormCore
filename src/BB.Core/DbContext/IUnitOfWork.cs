using System;
using System.Data;
using System.Threading.Tasks;

namespace BB.Core.DbContext;

/// <summary>
/// 单例的 SqlSugar 工作单元
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// 获取单例 SqlSugarScope 实例
    /// </summary>
    /// <returns></returns>
    SqlSugarScope GetDbClient();

    /// <summary>
    /// 开启事务
    /// </summary>
    void BeginTran();

    /// <summary>
    /// 开启事务
    /// </summary>
    /// <param name="level">事务隔离级别</param>
    void BeginTran(IsolationLevel level);

    /// <summary>
    /// 提交事务
    /// </summary>
    void CommitTran();

    /// <summary>
    /// 回滚事务
    /// </summary>
    void RollbackTran();

    /// <summary>
    /// 执行事务，自动提交和回滚
    /// </summary>
    /// <param name="action">事务逻辑</param>
    /// <param name="errorCallBack">错误回调</param>
    /// <returns></returns>
    DbResult<bool> UseTran(Action action, Action<Exception> errorCallBack = null);

    /// <summary>
    /// 执行事务，自动提交和回滚
    /// </summary>
    /// <param name="action">事务逻辑</param>
    /// <param name="errorCallBack">错误回调</param>
    /// <returns></returns>
    DbResult<T> UseTran<T>(Func<T> action, Action<Exception> errorCallBack = null);

    /// <summary>
    /// 执行事务，自动提交和回滚
    /// </summary>
    /// <param name="action">事务逻辑</param>
    /// <param name="errorCallBack">错误回调</param>
    /// <returns></returns>
    Task<DbResult<bool>> UseTranAsync(Func<Task> action, Action<Exception> errorCallBack = null);

    /// <summary>
    /// 执行事务，自动提交和回滚
    /// </summary>
    /// <param name="action">事务逻辑</param>
    /// <param name="errorCallBack">错误回调</param>
    /// <returns></returns>
    Task<DbResult<T>> UseTranAsync<T>(Func<Task<T>> action, Action<Exception> errorCallBack = null);
}