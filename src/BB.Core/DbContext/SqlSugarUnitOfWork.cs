using System;
using System.Data;
using System.Threading.Tasks;

namespace BB.Core.DbContext;

/// <summary>
/// 单例的 SqlSugar 工作单元
/// </summary>
public class SqlSugarUnitOfWork : ISqlSugarUnitOfWork
{
    /// <summary>
    /// 单例的 SqlSugar 实例
    /// </summary>
    private readonly ISqlSugarClient _sqlSugarClient;

    /// <summary>
    /// 事务计数
    /// </summary>
    private int TranCount { get; set; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sqlSugarClient">SqlSugar 实例</param>
    public SqlSugarUnitOfWork(ISqlSugarClient sqlSugarClient)
    {
        _sqlSugarClient = sqlSugarClient;
        TranCount = 0;
    }

    /// <summary>
    /// 获取单例 SqlSugarScope 实例
    /// </summary>
    /// <returns></returns>
    public SqlSugarScope GetDbClient() => (SqlSugarScope)_sqlSugarClient;

    /// <summary>
    /// 开启事务
    /// </summary>
    public void BeginTran()
    {
        lock (this)
        {
            TranCount++;
            GetDbClient().BeginTran();
        }
    }

    /// <summary>
    /// 开启事务
    /// </summary>
    /// <param name="level">事务隔离级别</param>
    public void BeginTran(IsolationLevel level)
    {
        lock (this)
        {
            TranCount++;
            GetDbClient().Ado.BeginTran(level);
        }
    }

    /// <summary>
    /// 提交事务
    /// </summary>
    public void CommitTran()
    {
        lock (this)
        {
            TranCount--;
            if (TranCount == 0)
            {
                try
                {
                    // 注意 多租户（多库） 不能用 db.Ado.CommitTran，单库可以用 db.Ado.CommitTran
                    GetDbClient().CommitTran();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    GetDbClient().RollbackTran();
                }
            }
        }
    }

    /// <summary>
    /// 回滚事务
    /// </summary>
    public void RollbackTran()
    {
        lock (this)
        {
            TranCount--;
            GetDbClient().RollbackTran();
        }
    }

    /// <summary>
    /// 执行事务，自动提交和回滚
    /// </summary>
    /// <param name="action">事务逻辑</param>
    /// <param name="errorCallBack">错误回调</param>
    /// <returns></returns>
    public DbResult<bool> UseTran(Action action, Action<Exception> errorCallBack = null)
    {
        DbResult<bool> dbResult = new();
        try
        {
            BeginTran();
            action?.Invoke();
            CommitTran();
            dbResult.Data = dbResult.IsSuccess = true;
        }
        catch (Exception ex)
        {
            dbResult.ErrorException = ex;
            dbResult.ErrorMessage = ex.Message;
            dbResult.IsSuccess = false;
            RollbackTran();
            errorCallBack?.Invoke(ex);
        }

        return dbResult;
    }

    /// <summary>
    /// 执行事务，自动提交和回滚
    /// </summary>
    /// <param name="action">事务逻辑</param>
    /// <param name="errorCallBack">错误回调</param>
    /// <returns></returns>
    public async Task<DbResult<bool>> UseTranAsync(Func<Task> action, Action<Exception> errorCallBack = null)
    {
        DbResult<bool> result = new();
        try
        {
            BeginTran();

            if (action != null) await action();

            CommitTran();
            result.Data = result.IsSuccess = true;
        }
        catch (Exception ex)
        {
            result.ErrorException = ex;
            result.ErrorMessage = ex.Message;
            result.IsSuccess = false;
            RollbackTran();
            errorCallBack?.Invoke(ex);
        }

        DbResult<bool> dbResult = result;
        result = null;
        return dbResult;
    }

    /// <summary>
    /// 执行事务，自动提交和回滚
    /// </summary>
    /// <param name="action">事务逻辑</param>
    /// <param name="errorCallBack">错误回调</param>
    /// <returns></returns>
    public DbResult<T> UseTran<T>(Func<T> action, Action<Exception> errorCallBack = null)
    {
        DbResult<T> dbResult = new DbResult<T>();
        try
        {
            BeginTran();
            if (action != null) dbResult.Data = action();
            CommitTran();
            dbResult.IsSuccess = true;
        }
        catch (Exception ex)
        {
            dbResult.ErrorException = ex;
            dbResult.ErrorMessage = ex.Message;
            dbResult.IsSuccess = false;
            RollbackTran();
            errorCallBack?.Invoke(ex);
        }

        return dbResult;
    }

    /// <summary>
    /// 执行事务，自动提交和回滚
    /// </summary>
    /// <param name="action">事务逻辑</param>
    /// <param name="errorCallBack">错误回调</param>
    /// <returns></returns>
    public async Task<DbResult<T>> UseTranAsync<T>(Func<Task<T>> action, Action<Exception> errorCallBack = null)
    {
        // return await _sqlSugarClient.Ado.UseTranAsync(action, errorCallBack);
        // UseTran / UseTranAsync 抄 SqlSugar 的逻辑，使用自己的 BeginTran / CommitTran，因为自己代码做了事务嵌套处理
        var result = new DbResult<T>();
        try
        {
            BeginTran();
            var data = default(T);
            if (action != null)
                data = await action();
            CommitTran();
            result.IsSuccess = true;
            result.Data = data;
        }
        catch (Exception ex)
        {
            result.ErrorException = ex;
            result.ErrorMessage = ex.Message;
            result.IsSuccess = false;
            RollbackTran();
            errorCallBack?.Invoke(ex);
        }

        return result;
    }
}