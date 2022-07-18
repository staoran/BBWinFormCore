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
    /// 提交事务
    /// </summary>
    void CommitTran();

    /// <summary>
    /// 回滚事务
    /// </summary>
    void RollbackTran();
}