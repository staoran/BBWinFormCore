using System;
using Furion.DatabaseAccessor;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BB.Core.DbContext;

/// <summary>
/// 工作单元特性实现类
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// SqlSugar 对象
    /// </summary>
    private readonly SqlSugarScope _sqlSugarScope;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="sqlSugarClient"></param>
    public UnitOfWork(ISqlSugarClient sqlSugarClient)
    {
        _sqlSugarScope = sqlSugarClient as SqlSugarScope;
    }

    /// <summary>
    /// 开启工作单元处理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="unitOfWork"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void BeginTransaction(FilterContext context, UnitOfWorkAttribute unitOfWork)
    {
        _sqlSugarScope.BeginTran();
    }

    /// <summary>
    /// 提交工作单元处理
    /// </summary>
    /// <param name="resultContext"></param>
    /// <param name="unitOfWork"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void CommitTransaction(FilterContext resultContext, UnitOfWorkAttribute unitOfWork)
    {
        _sqlSugarScope.CommitTran();
    }

    /// <summary>
    /// 回滚工作单元处理
    /// </summary>
    /// <param name="resultContext"></param>
    /// <param name="unitOfWork"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void RollbackTransaction(FilterContext resultContext, UnitOfWorkAttribute unitOfWork)
    {
        _sqlSugarScope.RollbackTran();
    }

    /// <summary>
    /// 执行完毕（无论成功失败）
    /// </summary>
    /// <param name="context"></param>
    /// <param name="resultContext"></param>
    /// <exception cref="NotImplementedException"></exception>
    public void OnCompleted(FilterContext context, FilterContext resultContext)
    {
        _sqlSugarScope.Dispose();
    }
}