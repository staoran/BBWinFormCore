using System;
using System.Data;

namespace BB.Core.DbContext;

/// <summary>
/// 单例的 SqlSugar 工作单元
/// </summary>
public class UnitOfWork : IUnitOfWork
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
    public UnitOfWork(ISqlSugarClient sqlSugarClient)
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
}