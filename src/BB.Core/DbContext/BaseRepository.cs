using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using BB.Entity.Base;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using BB.Tools.Validation;
using Furion.ClayObject.Extensions;
using Microsoft.Extensions.Caching.Distributed;

namespace BB.Core.DbContext;

/// <summary>
/// 仓储基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class BaseRepository<T> : SimpleClient<T> where T : BaseEntity, new()
{
    #region 构造函数

    /// <summary>
    /// 分布式缓存
    /// </summary>
    private readonly IDistributedCache _cache;

    /// <summary>
    /// 需要初始化的对象表名
    /// </summary>
    private string _tableName;

    /// <summary>
    /// 数据库访问对象的主键约束
    /// </summary>
    private readonly string _primaryKey;

    /// <summary>
    /// 数据库访问对象的外键约束
    /// </summary>
    private readonly string _foreignKey;
    
    /// <summary>
    /// 工作单元
    /// </summary>
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// 构造函数中初始化的 DbContext
    /// </summary>
    private readonly SqlSugarScope _dbBase;

    /// <summary>
    /// 本地使用的数据库访问对象
    /// </summary>
    private ISqlSugarClient _db
    {
        get
        {
            // 可以处理多库等问题
            return _dbBase;
        }
    }

    #region 属性字段

    /// <summary>
    /// SqlSugar 连接实例，用来处理事务多表查询和复杂的操作
    /// </summary>
    public ISqlSugarClient Db => _db;

    /// <summary>
    /// 排序字段
    /// </summary>
    public string SortField { get; set; }

    /// <summary>
    /// 是否为降序
    /// </summary>
    public bool IsDescending { get; set; }

    /// <summary>
    /// 定义乐观锁的字段名称
    /// </summary>
    public string OptimisticLockKey { get; set; }

    /// <summary>
    /// 数据库访问对象的表名
    /// </summary>
    public string TableName => _tableName;

    /// <summary>
    /// 数据库访问对象的主键约束
    /// </summary>
    public string PrimaryKey => _primaryKey;

    /// <summary>
    /// 数据库访问对象的外键约束
    /// </summary>
    public string ForeignKey => _foreignKey;

    /// <summary>
    /// 登陆用户基础信息（泛型类继承的父类中的静态对象是共享的）
    /// </summary>
    private LoginUserInfo _loginUserInfo;

    #endregion

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="unitOfWork">工作单元</param>
    /// <param name="cache">分布式缓存</param>
    public BaseRepository(IUnitOfWork unitOfWork, IDistributedCache cache) : base(unitOfWork.GetDbClient())
    {
        _unitOfWork = unitOfWork;
        _dbBase = unitOfWork.GetDbClient();
        _cache = cache;
        Context = _dbBase;
        _loginUserInfo = App.User.Adapt<LoginUserInfo>();
        Type type = typeof(T);
        _primaryKey = ReflectionExtension.GetField(type, nameof(BaseEntity.PrimaryKey)).ObjToStr();
        _foreignKey = ReflectionExtension.GetField(type, nameof(BaseEntity.ForeignKey)).ObjToStr();
        _tableName = ReflectionExtension.GetField(type, nameof(BaseEntity.DBTableName)).ObjToStr();
        SortField = ReflectionExtension.GetField(type, nameof(BaseEntity.SortKey)).ObjToStr();
        IsDescending = ReflectionExtension.GetField(type, nameof(BaseEntity.IsDesc)).ObjToBool();
        OptimisticLockKey = ReflectionExtension.GetField(type, nameof(BaseEntity.OptimisticLockKey)).ObjToStr();
    }

    #endregion

    #region 通用操作方法

    /// <summary>
    /// 执行SQL查询语句，返回查询结果的所有记录的第一个字段,用逗号分隔。
    /// </summary>
    /// <param name="sql">SQL语句</param>
    /// <returns>
    /// 返回查询结果的所有记录的第一个字段,用逗号分隔。
    /// </returns>
    public virtual async Task<string> SqlValueListAsync(string sql)
    {
        return await _db.Ado.GetStringAsync(sql);
    }

    /// <summary>
    /// 执行一些特殊的语句
    /// </summary>
    /// <param name="sql">SQL语句</param>
    public virtual async Task<int> SqlExecuteAsync(string sql)
    {
        return await _db.Ado.ExecuteCommandAsync(sql);
    }

    /// <summary>
    /// 执行存储过程函数。
    /// </summary>
    /// <param name="storeProcName">存储过程函数</param>
    /// <param name="parameters">参数集合</param>
    /// <returns></returns>
    public virtual async Task<int> StoreProcExecuteAsync(string storeProcName, DbParameter[] parameters)
    {
        return await _db.Ado.UseStoredProcedure()
            .GetIntAsync(storeProcName, parameters as SugarParameter[]);
    }

    /// <summary>
    /// 执行SQL查询语句，返回所有记录的DataTable集合。
    /// </summary>
    /// <param name="sql">SQL查询语句</param>
    /// <returns></returns>
    public virtual async Task<DataTable> SqlTableAsync(string sql)
    {
        return await SqlTableAsync(sql, null);
    }

    /// <summary>
    /// 执行SQL查询语句，返回所有记录的DataTable集合。
    /// </summary>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">参数集合</param>
    /// <returns></returns>
    public virtual async Task<DataTable> SqlTableAsync(string sql, DbParameter[] parameters)
    {
        DataTable dt = await _db.Ado.GetDataTableAsync(sql, parameters);
        if (dt != null && dt.TableName.IsNullOrEmpty())
        {
            dt.TableName = "tableName"; //增加一个表名称，防止WCF方式因为TableName为空出错
        }

        return dt;
    }

    /// <summary>
    /// 执行SQL查询语句,并返回DataTable集合(用于分页数据显示)
    /// </summary>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="info">分页参数</param>
    /// <returns>指定DataTable的集合</returns>
    public virtual async Task<DataTable> SqlTableWithPagerAsync(string sql, PageInput info)
    {
        //如果不指定排序字段，用默认的
        string fieldToSort = !string.IsNullOrEmpty(info.SortField) ? info.SortField : SortField;

        var query = _db.SqlQueryable<object>(sql);

        return await query.OrderBy($"{fieldToSort} {info.SortOrder}")
            .ToDataTablePageAsync(info.PageNo, info.PageSize);
    }

    /// <summary>
    /// 打开数据库连接，并创建事务对象
    /// </summary>
    public virtual DbTransaction CreateTransaction()
    {
        _unitOfWork.BeginTran();
        return (DbTransaction)_db.Ado.Transaction;
    }

    /// <summary>
    /// 打开数据库连接，并创建事务对象
    /// </summary>
    /// <param name="level">事务级别</param>
    public virtual DbTransaction CreateTransaction(IsolationLevel level)
    {
        _unitOfWork.BeginTran(level);
        return (DbTransaction)_db.Ado.Transaction;
    }

    /// <summary>
    /// 提交事务对象
    /// </summary>
    public virtual void CommitTransaction()
    {
        _unitOfWork.CommitTran();
    }

    /// <summary>
    /// 回滚事务对象
    /// </summary>
    public virtual void RollbackTransaction()
    {
        _unitOfWork.RollbackTran();
    }

    /// <summary>
    /// 执行事务，自动提交和回滚
    /// </summary>
    /// <param name="action">事务逻辑</param>
    /// <param name="errorCallBack">错误回调</param>
    public virtual async Task<bool> UseTransactionAsync(Func<Task> action, Action<Exception> errorCallBack = null)
    {
        return (await _unitOfWork.UseTranAsync(() => action?.Invoke(), e => errorCallBack?.Invoke(e)))
            .IsSuccess;
    }

    /// <summary>
    /// 测试数据库是否正常连接
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    public virtual bool TestConnection(string connectionString)
    {
        try
        {
            _db.Open();
        }
        catch (Exception e)
        {
            return false;
        }
        finally
        {
            _db.Close();
        }

        return true;
    }

    /// <summary>
    /// 通用的插入检查处理
    /// </summary>
    /// <param name="checkSql">检查是否存在语句</param>
    /// <param name="insertSql">插入数据的语句</param>
    public async Task InsertCheckDuplicatedAsync(string checkSql, string insertSql)
    {
        if (await _db.Ado.GetIntAsync(checkSql) == 0)
        {
            await _db.Ado.ExecuteCommandAsync(insertSql);
        }
    }

    #endregion

    #region 对象添加、修改

    /// <summary>
    /// 插入指定对象到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回true或false</returns>
    public override async Task<bool> InsertAsync(T obj)
    {
        ArgumentValidation.CheckForNullReference(obj, "传入的对象obj为空");

        return await base.InsertAsync(obj);
    }

    /// <summary>
    /// 插入指定对象集合到数据库中
    /// </summary>
    /// <param name="list">指定的对象集合</param>
    /// <returns>执行操作是否成功。</returns>
    public override async Task<bool> InsertRangeAsync(List<T> list)
    {
        return await base.InsertRangeAsync(list);
    }

    /// <summary>
    /// 添加记录
    /// </summary>
    /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
    public virtual async Task<int> Insert2Async(Hashtable recordField)
    {
        return await Insert2Async(recordField, _tableName);
    }

    /// <summary>
    /// 添加记录
    /// </summary>
    /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
    /// <param name="targetTable">需要操作的目标表名称</param>
    public virtual async Task<int> Insert2Async(Hashtable recordField, string targetTable)
    {
        return recordField != null
            ? await _db.Insertable(recordField.ToDictionary() as Dictionary<string, object>)
                .AS(targetTable)
                .ExecuteCommandAsync()
            : 0;
    }

    /// <summary>
    /// 插入指定对象到数据库中,并返回自增长的键值
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回True</returns>
    public virtual async Task<int> Insert2Async(T obj)
    {
        ArgumentValidation.CheckForNullReference(obj, "传入的对象obj为空");

        return await base.InsertReturnIdentityAsync(obj);
    }

    /// <summary>
    /// 乐观锁验证，正常返回 true，不正常抛异常
    /// </summary>
    /// <param name="primaryKeyValue">主键值</param>
    /// <param name="optimisticLock">乐观锁值，等于 null 时返回 true</param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public virtual async Task<bool> UpdateVersionValidationAsync(object primaryKeyValue, object optimisticLock)
    {
        if (optimisticLock == null) return true;

        // 构造动态查询条件
        List<IConditionalModel> conModels = new()
        {
            new ConditionalModel
            {
                FieldName = _primaryKey,
                ConditionalType = ConditionalType.Equal,
                FieldValue = primaryKeyValue.ObjToStr()
            }
        };

        // Select<object> 和 Select<dynamic> 返回的是匿名对象 ExpandoObject，它本质是一个字典 IDictionary<string,object>
        object obj = await AsQueryable().Select<object>(OptimisticLockKey).Where(conModels).FirstAsync();
        if (obj == null) throw new Exception("修改失败，数据已被删除，请刷新后核实！");

        var dic = obj as IDictionary<string, object>;
        if (dic != null && !dic.ContainsKey(OptimisticLockKey))
            throw new Exception("修改失败，没有找到乐观锁，请向开发者反馈异常！");

        var version = dic?[OptimisticLockKey];
        if (!optimisticLock.Equals(version))
            throw new Exception("数据已经被其他用户修改，请退出后重新编辑！");

        return true;
    }

    /// <summary>
    /// 更新某个表一条记录(只适用于用单键)
    /// </summary>
    /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
    public virtual async Task<bool> UpdateAsync(Hashtable recordField)
    {
        return await UpdateFieldsAsync(recordField);
    }

    /// <summary>
    /// 更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public override async Task<bool> UpdateAsync(T obj)
    {
        ArgumentValidation.CheckForNullReference(obj, "传入的对象obj为空");

        // 拿主键的值
        object primaryKeyValue = obj.GetProperty(_primaryKey);

        // 拿乐观锁的值，如果实体中没设置乐观锁则返回null，不会验证
        object optimisticLockValue = obj.GetProperty(OptimisticLockKey);

        // 乐观锁验证，正常返回 true，不正常抛异常，optimisticLockValue = null 返回 true
        await UpdateVersionValidationAsync(primaryKeyValue, optimisticLockValue);

        return await _db.Updateable(obj).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 更新某个表一条记录(只适用于用单键)
    /// </summary>
    /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
    public virtual async Task<bool> UpdateFieldsAsync(Hashtable recordField)
    {
        ArgumentValidation.CheckForNullReference(recordField, "传入的对象recordField为空");

        // 拿主键的值
        object primaryKeyValue = recordField.ContainsKey(_primaryKey)
            ? recordField[_primaryKey]
            : throw new Exception("待更新的数据中找不到主键");

        // 拿乐观锁的值，如果实体中没设置乐观锁则返回null，不会验证
        object optimisticLockValue = recordField.ContainsKey(OptimisticLockKey) ? recordField[OptimisticLockKey] : null;

        await UpdateVersionValidationAsync(primaryKeyValue, optimisticLockValue);

        Dictionary<string, object> dic = recordField.ToDictionary() as Dictionary<string, object>;
        return await _db.Updateable<T>(dic).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 更新某个表的记录(根据条件更新)
    /// </summary>
    /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
    /// <param name="condition">查询的条件</param>
    public virtual async Task<bool> UpdateFieldsByConditionAsync(Hashtable recordField, string condition)
    {
        return await _db.Updateable<T>(recordField.ToDictionary() as Dictionary<string, object>)
            .Where(condition)
            .ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 更新某个表的记录(根据条件更新)
    /// </summary>
    /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
    /// <param name="expression">查询的条件</param>
    public virtual async Task<bool> UpdateFieldsByConditionAsync(Hashtable recordField, Expression<Func<T,bool>> expression)
    {
        return await _db.Updateable<T>(recordField.ToDictionary() as Dictionary<string, object>)
            .Where(expression)
            .ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 插入或更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public virtual async Task<bool> InsertUpdateAsync(T obj)
    {
        return await _db.Storageable(obj).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 如果不存在记录，则插入对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行插入成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public virtual async Task<bool> InsertIfNewAsync(T obj)
    {
        var x = await _db.Storageable(obj)
            .SplitInsert(it => !it.Any())
            .ToStorageAsync();
        return await x.AsInsertable.ExecuteCommandAsync() > 0;
    }

    #endregion

    #region 返回实体类操作

    /// <summary>
    /// 查询数据库,检查是否存在指定ID的对象
    /// </summary>
    /// <param name="key">对象的ID值</param>
    /// <returns>存在则返回指定的对象,否则返回Null</returns>
    public virtual async Task<T> FindByIdAsync(object key)
    {
        return await base.GetByIdAsync(key);
    }

    /// <summary>
    /// 根据条件查询数据库,返回仅有的一个对象，如有多个则抛出异常
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="orderBy">自定义排序语句，如Name desc；如不指定，则使用默认排序</param>
    /// <param name="paramList">排序类型</param>
    /// <returns>指定的对象</returns>
    public virtual async Task<T> FindSingleAsync(string condition, string orderBy = "", IDbDataParameter[] paramList = null)
    {
        if (orderBy.IsNullOrEmpty())
            orderBy = SortField.IsNullOrEmpty() ? string.Empty : $"{SortField} {(IsDescending ? "desc" : "asc")}";

        #region 获取单条记录

        return await _db.Queryable<T>()
            .WhereIF(!string.IsNullOrEmpty(condition), condition)
            .OrderByIF(!string.IsNullOrEmpty(orderBy), orderBy)
            .AddParameters(paramList as SugarParameter[])
            .SingleAsync();

        #endregion
    }

    /// <summary>
    /// 根据条件查询数据库,返回仅有的一个对象，如有多个则抛出异常
    /// </summary>
    /// <param name="expression">查询的条件</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序类型</param>
    /// <returns>指定的对象</returns>
    public virtual async Task<T> FindSingleAsync(Expression<Func<T,bool>> expression, Expression<Func<T, object>> orderByExp = null,
        OrderByType orderByType = OrderByType.Desc)
    {
        var query = _db.Queryable<T>().Where(expression);

        var orderBy = string.Empty;
        if (orderByExp == null && !SortField.IsNullOrEmpty())
            orderBy = $"{SortField} {(IsDescending ? "desc" : "asc")}";
        
        query.OrderByIF(orderByExp != null, orderByExp, orderByType);

        return await query
            .OrderByIF(orderByExp != null, orderByExp, orderByType)
            .OrderByIF(orderByExp == null && !string.IsNullOrEmpty(orderBy), orderBy)
            .SingleAsync();
    }

    /// <summary>
    /// 查找记录表中最旧的一条记录
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="orderByField">自定义排序字段</param>
    /// <returns></returns>
    public virtual async Task<T> FindFirstAsync(string condition = "", string orderByField = "")
    {
        if (orderByField.IsNullOrEmpty())
            orderByField = SortField.IsNullOrEmpty() ? string.Empty : SortField;
        if (orderByField.IsNullOrEmpty())
            orderByField = $"{orderByField} asc";
        
        return await FindTopAsync(condition, orderByField);
    }

    /// <summary>
    /// 查找记录表中最新的一条记录
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="orderByField">自定义排序字段</param>
    /// <returns></returns>
    public virtual async Task<T> FindLastAsync(string condition = "", string orderByField = "")
    {
        if (orderByField.IsNullOrEmpty())
            orderByField = SortField.IsNullOrEmpty() ? string.Empty : SortField;
        if (orderByField.IsNullOrEmpty())
            orderByField = $"{orderByField} desc";
        
        return await FindTopAsync(condition, orderByField);
    }

    /// <summary>
    /// 初始化一个实体
    /// </summary>
    /// <returns></returns>
    public virtual async Task<T> NewEntityAsync()
    {
        return await Task.FromResult(new T());
    }

    /// <summary>
    /// 根据条件获取第一条记录
    /// </summary>
    /// <param name="condition">查询语句</param>
    /// <param name="orderBy">自定义排序语句，如 Name Desc；如不指定，则使用默认排序</param>
    /// <returns></returns>
    public virtual async Task<T> FindTopAsync(string condition, string orderBy)
    {
        if (orderBy.IsNullOrEmpty())
            orderBy = SortField.IsNullOrEmpty() ? string.Empty : $"{SortField} {(IsDescending ? "desc" : "asc")}";

        return await base.AsQueryable()
            .Where(condition)
            .OrderByIF(!orderBy.IsNullOrEmpty(), orderBy)
            .FirstAsync();
    }

    /// <summary>
    /// 根据条件获取第一条记录
    /// </summary>
    /// <param name="expression">查询语句</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序方式</param>
    /// <returns></returns>
    public virtual async Task<T> FindTopAsync(Expression<Func<T,bool>> expression, Expression<Func<T, object>> orderByExp = null,
        OrderByType orderByType = OrderByType.Desc)
    {
        var query = _db.Queryable<T>().Where(expression);

        var orderBy = string.Empty;
        if (orderByExp == null && !SortField.IsNullOrEmpty())
            orderBy = $"{SortField} {(IsDescending ? "desc" : "asc")}";
        
        query.OrderByIF(orderByExp != null, orderByExp, orderByType);

        return await query
            .OrderByIF(orderByExp != null, orderByExp, orderByType)
            .OrderByIF(orderByExp == null && !string.IsNullOrEmpty(orderBy), orderBy)
            .FirstAsync();
    }

    /// <summary>
    /// 根据条件获取指定的记录
    /// </summary>
    /// <param name="count">指定数量</param>
    /// <param name="condition">查询语句</param>
    /// <param name="orderBy">自定义排序语句，如 Name Desc；如不指定，则使用默认排序</param>
    /// <returns></returns>
    public virtual async Task<List<T>> FindTopListAsync(int count, string condition, string orderBy)
    {
        if (orderBy.IsNullOrEmpty())
            orderBy = SortField.IsNullOrEmpty() ? string.Empty : $"{SortField} {(IsDescending ? "desc" : "asc")}";

        return await base.AsQueryable()
            .Take(count)
            .Where(condition)
            .OrderByIF(!orderBy.IsNullOrEmpty(), orderBy)
            .ToListAsync();
    }

    /// <summary>
    /// 根据条件获取指定的记录
    /// </summary>
    /// <param name="count">指定数量</param>
    /// <param name="expression">查询语句</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序方式</param>
    /// <returns></returns>
    public virtual async Task<List<T>> FindTopListAsync(int count, Expression<Func<T,bool>> expression,
        Expression<Func<T, object>> orderByExp = null, OrderByType orderByType = OrderByType.Desc)
    {
        var query = _db.Queryable<T>().Take(count).Where(expression);

        var orderBy = string.Empty;
        if (orderByExp == null && !SortField.IsNullOrEmpty())
            orderBy = $"{SortField} {(IsDescending ? "desc" : "asc")}";
        
        query.OrderByIF(orderByExp != null, orderByExp, orderByType);

        return await query
            .OrderByIF(orderByExp != null, orderByExp, orderByType)
            .OrderByIF(orderByExp == null && !string.IsNullOrEmpty(orderBy), orderBy)
            .ToListAsync();
    }

    #endregion

    #region 返回集合的接口

    /// <summary>
    /// 根据多个主键获取对象列表
    /// </summary>
    /// <param name="ids">主键数组</param>
    /// <returns>符合条件的对象列表</returns>
    public virtual async Task<List<T>> FindByIDsAsync(object[] ids)
    {
        return await FindAsync(x=>ids.Contains(x.GetType().GetProperty(_primaryKey).GetValue(x)));
    }

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="orderBy">自定义排序语句，如Name desc；如不指定，则使用默认排序</param>
    /// <param name="paramList">参数列表</param>
    /// <returns>指定对象的集合</returns>
    public virtual async Task<List<T>> FindAsync(string condition, string orderBy = "", IDbDataParameter[] paramList = null)
    {
        if (orderBy.IsNullOrEmpty())
            orderBy = SortField.IsNullOrEmpty() ? string.Empty : $"{SortField} {(IsDescending ? "desc" : "asc")}";

        return await base.AsQueryable()
            .Where(condition)
            .OrderByIF(!orderBy.IsNullOrEmpty(), orderBy)
            .ToListAsync();
    }

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合
    /// </summary>
    /// <param name="expression">查询的条件</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序方式</param>
    /// <returns>指定对象的集合</returns>
    public virtual async Task<List<T>> FindAsync(Expression<Func<T,bool>> expression, Expression<Func<T, object>> orderByExp = null,
        OrderByType orderByType = OrderByType.Desc )
    {
        var query = _db.Queryable<T>().Where(expression);

        var orderBy = string.Empty;
        if (orderByExp == null && !SortField.IsNullOrEmpty())
            orderBy = $"{SortField} {(IsDescending ? "desc" : "asc")}";
        
        query.OrderByIF(orderByExp != null, orderByExp, orderByType);

        return await query
            .OrderByIF(orderByExp != null, orderByExp, orderByType)
            .OrderByIF(orderByExp == null && !string.IsNullOrEmpty(orderBy), orderBy)
            .ToListAsync();
    }

    /// <summary>
    /// 通用获取集合对象方法
    /// </summary>
    /// <param name="sql">查询的Sql语句</param>
    /// <param name="paramList">参数列表，如果没有则为null</param>
    /// <returns></returns>
    public virtual async Task<List<T>> GetListAsync(string sql, IDbDataParameter[] paramList = null)
    {
        return await _db.Ado.SqlQueryAsync<T>(sql, paramList as SugarParameter[]);
    }

    /// <summary>
    /// 以分页方式通用获取集合对象方法
    /// </summary>
    /// <param name="sql">查询的Sql语句</param>
    /// <param name="info">分页参数</param>
    /// <param name="paramList">参数列表，如果没有则为null</param>
    /// <returns></returns>
    public virtual async Task<PageResult<T>> GetListWithPagerAsync(string sql, PageInput info,
        IDbDataParameter[] paramList = null)
    {
        return await _db.SqlQueryable<T>(sql)
            .AddParameters(paramList as SugarParameter[])
            .OrderByIF(!SortField.IsNullOrEmpty(), $"{SortField} {(IsDescending ? "desc" : "asc")}")
            .ToPagedListAsync(info.PageNo, info.PageSize);
    }

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="info">分页参数</param>
    /// <returns>指定对象的集合</returns>
    public virtual async Task<PageResult<T>> FindWithPagerAsync(string condition, PageInput info)
    {
        //如果不指定排序字段，用默认的
        string fieldToSort = !string.IsNullOrEmpty(info.SortField) ? info.SortField : SortField;

        return await _db.Queryable<T>()
            .Where(condition)
            .OrderByIF(!fieldToSort.IsNullOrEmpty(), $"{fieldToSort} {info.SortOrder}")
            .ToPagedListAsync(info.PageNo, info.PageSize);
    }

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
    /// </summary>
    /// <param name="whereExpression">查询的条件</param>
    /// <param name="info">分页参数</param>
    /// <returns>指定对象的集合</returns>
    public virtual async Task<PageResult<T>> FindWithPagerAsync(Expression<Func<T, bool>> whereExpression, PageInput info)
    {
        //如果不指定排序字段，用默认的
        string fieldToSort = !string.IsNullOrEmpty(info.SortField) ? info.SortField : SortField;

        return await _db.Queryable<T>()
            .Where(whereExpression)
            .OrderByIF(!fieldToSort.IsNullOrEmpty(), $"{fieldToSort} {info.SortOrder}")
            .ToPagedListAsync(info.PageNo, info.PageSize);
    }

    /// <summary>
    /// 通过外键获取表数据
    /// </summary>
    /// <param name="foreignKeyId">外键ID</param>
    /// <param name="foreignKeyName">外键名称</param>
    /// <returns>数据列表</returns>
    public virtual async Task<List<T>> FindByForeignKeyAsync(object foreignKeyId, string foreignKeyName)
    {
        foreignKeyName ??= ForeignKey;
        if (foreignKeyName.IsNullOrEmpty())
            throw new ArgumentException("没有指定有效的外键名称");
        var condition = $"{foreignKeyName} = '{foreignKeyId}'";
        return await FindAsync(condition);
    }

    /// <summary>
    /// 通过外键获取表ID
    /// </summary>
    /// <param name="foreignKeyId">外键ID</param>
    /// <param name="foreignKeyName">外键名称</param>
    /// <returns>ID列表</returns>
    public virtual async Task<List<string>> FindIdByForeignKeyAsync(object foreignKeyId, string foreignKeyName)
    {
        foreignKeyName ??= ForeignKey;
        if (foreignKeyName.IsNullOrEmpty())
            throw new ArgumentException("没有指定有效的外键名称");
        var condition = $"{foreignKeyName} = '{foreignKeyId}'";
        return await GetFieldListByConditionAsync(_primaryKey, condition);
    }

    /// <summary>
    /// 返回数据库所有的对象集合
    /// </summary>
    /// <param name="orderBy">不可用，返回结果后自行排序</param>
    /// <returns>指定对象的集合</returns>
    public virtual async Task<List<T>> GetAllAsync(string orderBy = "")
    {
        if (orderBy.IsNullOrEmpty())
            orderBy = SortField.IsNullOrEmpty() ? string.Empty : $"{SortField} {(IsDescending ? "desc" : "asc")}";
        return await _db.Queryable<T>().OrderByIF(!orderBy.IsNullOrEmpty(), orderBy).ToListAsync();
    }

    /// <summary>
    /// 返回数据库所有的对象集合(用于分页数据显示)
    /// </summary>
    /// <param name="info">分页参数信息</param>
    /// <returns>指定对象的集合</returns>
    public virtual async Task<PageResult<T>> GetAllAsync(PageInput info)
    {
        return await FindWithPagerAsync("", info);
    }

    /// <summary>
    /// 通用获取dynamic集合对象方法
    /// </summary>
    /// <param name="sql">查询的Sql语句</param>
    /// <param name="paramList">参数列表，如果没有则为null</param>
    /// <returns></returns>
    public virtual async Task<List<dynamic>> GetDynamicListAsync(string sql, IDbDataParameter[] paramList = null)
    {
        return await _db.SqlQueryable<dynamic>(sql).AddParameters(paramList as SugarParameter[]).ToListAsync();
    }

    /// <summary>
    /// 返回所有记录到DataTable集合中
    /// </summary>
    /// <param name="orderBy">自定义排序语句，如Name desc；如不指定，则使用默认排序</param>
    /// <returns></returns>
    public virtual async Task<DataTable> GetAllToDataTableAsync(string orderBy = "")
    {
        ISugarQueryable<T> query = base.AsQueryable();
        query = !string.IsNullOrEmpty(orderBy)
            ? query.OrderBy(orderBy)
            : query.OrderByIF(!SortField.IsNullOrEmpty(), $"{SortField} {(IsDescending ? "desc" : "asc")}");
        return await query.ToDataTableAsync();
    }

    /// <summary>
    /// 根据条件查询数据库,并返回DataTable集合(用于分页数据显示)
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="info">分页参数</param>
    /// <returns>指定DataTable的集合</returns>
    public virtual async Task<DataTable> FindToDataTableAsync(string condition = "", PageInput info = null)
    {
        ISugarQueryable<T> query = AsQueryable();

        if (!string.IsNullOrEmpty(condition))
        {
            query = query.Where(condition);
        }

        //如果不指定排序字段，用默认的
        string fieldToSort = info != null && !string.IsNullOrEmpty(info.SortField)
            ? $"{info.SortField} {info.SortOrder}"
            : SortField.IsNullOrEmpty() ? string.Empty : $"{SortField} {(IsDescending ? "desc" : "asc")}";

        query = query.OrderByIF(!fieldToSort.IsNullOrEmpty(), fieldToSort);
        
        if (info == null)
        {
            return await query.ToDataTableAsync();
        }
    
        return await query.ToDataTablePageAsync(info.PageNo, info.PageSize);
    }

    /// <summary>
    /// 根据条件查询数据库,并返回DataTable集合
    /// </summary>
    /// <param name="expression">查询的条件</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序方式</param>
    /// <returns>指定DataTable的集合</returns>
    public virtual async Task<DataTable> FindToDataTableAsync(Expression<Func<T,bool>> expression, Expression<Func<T, object>> orderByExp = null,
        OrderByType orderByType = OrderByType.Desc)
    {
        var query = _db.Queryable<T>().Where(expression);

        var orderBy = string.Empty;
        if (orderByExp == null && !SortField.IsNullOrEmpty())
            orderBy = $"{SortField} {(IsDescending ? "desc" : "asc")}";
        
        query.OrderByIF(orderByExp != null, orderByExp, orderByType);

        return await query
            .OrderByIF(orderByExp != null, orderByExp, orderByType)
            .OrderByIF(orderByExp == null && !string.IsNullOrEmpty(orderBy), orderBy)
            .ToDataTableAsync();
    }

    /// <summary>
    /// 根据条件，获取某字段数据字典列表
    /// </summary>
    /// <param name="fieldName">字段名称</param>
    /// <param name="condition">查询的条件</param>
    /// <returns></returns>
    public virtual async Task<List<string>> GetFieldListByConditionAsync(string fieldName, string condition = null)
    {
        ISugarQueryable<T> query = AsQueryable();
        if (!string.IsNullOrEmpty(condition))
        {
            query = query.Where(condition);
        }

        return await query.Select<string>(fieldName)
            .OrderBy($"{fieldName} {(IsDescending ? "desc" : "asc")}")
            .ToListAsync();
    }

    /// <summary>
    /// 根据条件，获取某字段数据字典列表
    /// </summary>
    /// <param name="fieldName">字段名称</param>
    /// <param name="expression">查询的条件</param>
    /// <returns></returns>
    public virtual async Task<List<string>> GetFieldListByConditionAsync(string fieldName, Expression<Func<T,bool>> expression)
    {
        var query = AsQueryable().Where(expression);
        return await query.Select<string>(fieldName)
            .OrderBy($"{fieldName} {(IsDescending ? "desc" : "asc")}")
            .ToListAsync();
    }

    /// <summary>
    /// 根据条件，获取某字段数据字典列表
    /// </summary>
    /// <param name="filedExpression">字段名称</param>
    /// <param name="expression">查询的条件</param>
    /// <returns></returns>
    public virtual async Task<List<string>> GetFieldListByConditionAsync(Expression<Func<T,string>> filedExpression, Expression<Func<T,bool>> expression)
    {
        var query = AsQueryable().Where(expression);
        return await query.Select(filedExpression)
            .ToListAsync();
    }

    /// <summary>
    /// 根据条件，从视图里面获取记录
    /// </summary>
    /// <param name="viewName">视图名称</param>
    /// <param name="condition">查询条件</param>
    /// <param name="sortField">排序字段</param>
    /// <param name="isDescending">是否为降序</param>
    /// <returns></returns>
    public virtual async Task<DataTable> FindByViewAsync(string viewName, string condition, string sortField = "",
        bool isDescending = default)
    {
        //串连条件语句为一个完整的Sql语句
        string sql = $"Select * From {viewName} Where {condition}";
        if (!sortField.IsNullOrEmpty())
        {
            sql += $" Order by {sortField} {(isDescending ? "DESC" : "ASC")}";
        }

        return await _db.Ado.GetDataTableAsync(sql);
    }

    /// <summary>
    /// 获取前面记录指定数量的记录
    /// </summary>
    /// <param name="count">指定数量</param>
    /// <param name="condition">查询语句</param>
    /// <param name="orderBy">排序条件，例如order by id</param>
    /// <returns></returns>
    public virtual async Task<DataTable> GetTopResultAsync(int count, string condition, string orderBy)
    {
        if (orderBy.IsNullOrEmpty())
            orderBy = SortField.IsNullOrEmpty() ? string.Empty : $"{SortField} {(IsDescending ? "desc" : "asc")}";

        return await base.AsQueryable()
            .Take(count)
            .Where(condition)
            .OrderByIF(!orderBy.IsNullOrEmpty(), orderBy)
            .ToDataTableAsync();
    }

    /// <summary>
    /// 获取前面记录指定数量的记录
    /// </summary>
    /// <param name="count">指定数量</param>
    /// <param name="expression">查询的条件</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序方式</param>
    /// <returns></returns>
    public virtual async Task<DataTable> GetTopResultAsync(int count, Expression<Func<T,bool>> expression,
        Expression<Func<T, object>> orderByExp = null, OrderByType orderByType = OrderByType.Desc)
    {
        var query = _db.Queryable<T>().Take(count).Where(expression);

        var orderBy = string.Empty;
        if (orderByExp == null && !SortField.IsNullOrEmpty())
            orderBy = $"{SortField} {(IsDescending ? "desc" : "asc")}";
        
        query.OrderByIF(orderByExp != null, orderByExp, orderByType);

        return await query
            .OrderByIF(orderByExp != null, orderByExp, orderByType)
            .OrderByIF(orderByExp == null && !string.IsNullOrEmpty(orderBy), orderBy)
            .ToDataTableAsync();
    }

    /// <summary>
    /// 根据条件，从视图里面获取记录
    /// </summary>
    /// <param name="viewName">视图名称</param>
    /// <param name="condition">查询条件</param>
    /// <param name="info">分页条件</param>
    /// <returns></returns>
    public virtual async Task<DataTable> FindByViewWithPagerAsync(string viewName, string condition, PageInput info)
    {
        //如果不指定排序字段，用默认的
        string sortField = !string.IsNullOrEmpty(info.SortField) ? info.SortField : SortField;

        //从视图中获取数据
        return await _db.Queryable(viewName, viewName)
            .Where(condition)
            .OrderByIF(!sortField.IsNullOrEmpty(), $"{sortField} {info.SortOrder}")
            .ToDataTablePageAsync(info.PageNo, info.PageSize);
    }

    #endregion

    #region 子类必须实现的函数(用于更新或者插入)

    /// <summary>
    /// 将DataReader的属性值转化为实体类的属性值，返回实体类
    /// (提供了默认的反射机制获取信息，为了提高性能，建议重写该函数)
    /// </summary>
    /// <param name="dr">有效的DataReader对象</param>
    /// <returns>实体类对象</returns>
    protected virtual T DataReaderToEntity(IDataReader dr)
    {
        T obj = new T();
        PropertyInfo[] pis = obj.GetType().GetProperties();

        foreach (PropertyInfo pi in pis)
        {
            try
            {
                if (dr[pi.Name].ToString() != "")
                {
                    pi.SetValue(obj, dr[pi.Name] ?? "", null);
                }
            }
            catch
            {
                // ignored
            }
        }

        return obj;
    }

    /// <summary>
    /// 将实体对象的属性值转化为Hashtable对应的键值(用于插入或者更新操作)
    /// (提供了默认的反射机制获取信息，为了提高性能，建议重写该函数)
    /// </summary>
    /// <param name="obj">有效的实体对象</param>
    /// <returns>包含键值映射的Hashtable</returns>
    protected virtual Hashtable GetHashByEntity(T obj)
    {
        Hashtable ht = new Hashtable();
        PropertyInfo[] pis = obj.GetType().GetProperties();
        for (int i = 0; i < pis.Length; i++)
        {
            //if (pis[i].Name != PrimaryKey)
            {
                object objValue = pis[i].GetValue(obj, null);
                objValue = (objValue == null) ? DBNull.Value : objValue;

                if (!ht.ContainsKey(pis[i].Name))
                {
                    ht.Add(pis[i].Name, objValue);
                }
            }
        }

        return ht;
    }

    #endregion

    #region IBaseDAL接口

    /// <summary>
    /// 获取表的所有记录数量
    /// </summary>
    /// <param name="condition">查询条件</param>
    /// <returns></returns>
    public virtual async Task<int> GetRecordCountAsync(string condition = "")
    {
        return await base.AsQueryable().WhereIF(!condition.IsNullOrEmpty(), condition).CountAsync();
    }

    /// <summary>
    /// 获取表的所有记录数量
    /// </summary>
    /// <param name="expression">查询条件</param>
    /// <returns></returns>
    public virtual async Task<int> GetRecordCountAsync(Expression<Func<T,bool>> expression)
    {
        return await base.AsQueryable().Where(expression).CountAsync();
    }

    /// <summary>
    /// 根据condition条件，判断是否存在记录
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <returns>如果存在返回True，否则False</returns>
    public virtual async Task<bool> IsExistRecordAsync(string condition)
    {
        return await base.AsQueryable().Where(condition).AnyAsync();
    }

    /// <summary>
    /// 根据 expression 条件，判断是否存在记录
    /// </summary>
    /// <param name="expression">查询的条件</param>
    /// <returns>如果存在返回True，否则False</returns>
    public virtual async Task<bool> IsExistRecordAsync(Expression<Func<T,bool>> expression)
    {
        return await base.AsQueryable().Where(expression).AnyAsync();
    }

    /// <summary>
    /// 查询数据库,检查是否存在指定键值的对象
    /// </summary>
    /// <param name="recordTable">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
    /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
    public virtual async Task<bool> IsExistKeyAsync(Hashtable recordTable)
    {
        var fields = ""; // where
        foreach (string field in recordTable.Keys)
        {
            fields += $" {field} = @{field} AND";
        }

        fields = fields[..^3]; // 除去最后的AND，等于 fields.Substring(0, fields.Length - 3);
        return await base.AsQueryable().Where(fields)
            .AddParameters(recordTable.ToDictionary() as Dictionary<string, object>).AnyAsync();
    }

    /// <summary>
    /// 查询数据库,检查是否存在指定键值的对象
    /// </summary>
    /// <param name="fieldName">指定的属性名</param>
    /// <param name="key">指定的值</param>
    /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
    public virtual async Task<bool> IsExistKeyAsync(string fieldName, object key)
    {
        return await base.AsQueryable()
            .Where($" {fieldName} = @{fieldName}")
            .AddParameters(new[] { new SugarParameter(fieldName, key) }).AnyAsync();
    }

    /// <summary>
    /// 获取数据库中该对象的最大ID值
    /// </summary>
    /// <returns>最大ID值</returns>
    public virtual async Task<object> GetMaxIdAsync()
    {
        return await base.AsQueryable().MaxAsync<object>("");
    }

    /// <summary>
    /// 根据主键和字段名称，获取对应字段的内容
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public virtual async Task<string> GetFieldValueAsync(object key, string fieldName)
    {
        return await base.AsQueryable()
            .Select<string>(fieldName)
            .Where($"{_primaryKey} = @id", new { id = key })
            .FirstAsync();
    }

    /// <summary>
    /// 根据主键和字段名称，获取对应字段的内容
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <param name="fieldNameList">字段名称列表</param>
    /// <returns></returns>
    public virtual async Task<Dictionary<string, string>> GetFieldValueListAsync(string key, List<string> fieldNameList)
    {
        Dictionary<string, string> dict = new();
        IDictionary<string, object> row = (await base.AsQueryable()
            .Select(fieldNameList.Splice())
            .Where($"{_primaryKey} = @id", new { id = key }).FirstAsync()).ToDictionary();
        foreach (KeyValuePair<string, object> keyValuePair in row)
        {
            if (fieldNameList.Contains(keyValuePair.Key))
            {
                dict.Add(keyValuePair.Key, keyValuePair.Value.ObjToStr());
            }
        }

        return dict;
    }

    /// <summary>
    /// 根据指定对象的ID,从数据库中删除指定对象
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public virtual async Task<bool> DeleteAsync(object key)
    {
        if (key.IsNull() || key.ObjToStr().IsNullOrEmpty())
            throw new ArgumentException("删除时指定的主键为空");

        return await base.DeleteByIdAsync(key);
    }

    /// <summary>
    /// 从数据库中删除指定对象
    /// </summary>
    /// <param name="entity">指定对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public override async Task<bool> DeleteAsync(T entity)
    {
        if (entity.IsNull())
            throw new ArgumentException("删除时指定的对象为空");

        return await base.DeleteAsync(entity);
    }

    /// <summary>
    /// 根据指定条件,从数据库中删除指定对象
    /// </summary>
    /// <param name="condition">删除记录的条件语句</param>
    /// <param name="paramList">Sql参数列表</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public virtual async Task<bool> DeleteByConditionAsync(string condition, IDbDataParameter[] paramList = null)
    {
        return paramList == null
            ? await _db.Deleteable<T>().Where(condition).ExecuteCommandHasChangeAsync()
            : await _db.Deleteable<T>().Where(condition, paramList as SugarParameter[]).ExecuteCommandHasChangeAsync();
    }

    #endregion

    #region 辅助类方法

    /// <summary>
    /// 获取数据库的全部表名称
    /// </summary>
    /// <returns></returns>
    public virtual List<string> GetTableNames()
    {
        return _db.DbMaintenance.GetTableInfoList().Select(x => x.Name).ToList();
    }

    /// <summary>
    /// 获取表的主键
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    public virtual List<string> GetTableKeyList(string name)
    {
        return _db.DbMaintenance.GetPrimaries(name);
    }

    /// <summary>
    /// 获取表的自增字段
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    public virtual List<string> GetTableIdentityList(string name)
    {
        return _db.DbMaintenance.GetIsIdentities(name);
    }

    /// <summary>
    /// 获取表的注释
    /// </summary>
    /// <param name="name">表名</param>
    /// <returns></returns>
    public virtual string GetTableComment(string name)
    {
        return _db.DbMaintenance.GetTableInfoList()
            .Where(x => x.Name == name)
            .Select(x => x.Description)
            .FirstOrDefault();
    }

    /// <summary>
    /// 获取表的字段名称和数据类型列表。
    /// </summary>
    /// <returns></returns>
    public virtual DataTable GetFieldTypeList()
    {
        DataTable dt = DataTableHelper.CreateTable("ColumnName,DataType");
        List<DbColumnInfo> cols = _db.DbMaintenance.GetColumnInfosByTableName(_tableName);
        if (cols.Any())
        {
            cols.ForEach(x =>
            {
                DataRow row = dt.NewRow();
                row["ColumnName"] = x.DbColumnName;
                row["DataType"] = x.DataType;

                dt.Rows.Add(row);
            });
        }

        if (dt.TableName.IsNullOrEmpty())
        {
            dt.TableName = "tableName"; //增加一个表名称，防止WCF方式因为TableName为空出错
        }

        return dt;
    }

    /// <summary>
    /// 获取字段显示权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public virtual Dictionary<string, int> GetPermitDict()
    {
        return _cache.GetOrCreate($"{typeof(T).Name}PermitDict", () =>
        {
            Dictionary<string, int> dic = new();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                object attribute = prop.GetCustomAttribute(typeof(ColumnAttribute), false);
                if (attribute != null)
                {
                    var attr = (ColumnAttribute)attribute;
                    if (attr.Permit != 0)
                    {
                        dic.Add(attr.Name, attr.Permit);
                    }
                }
            }

            return dic;
        });
    }

    /// <summary>
    /// 获取字段中文别名（用于界面显示）的字典集合
    /// </summary>
    /// <returns></returns>
    public virtual Dictionary<string, string> GetColumnNameAlias()
    {
        return _cache.GetOrCreate($"{typeof(T).Name}ColumnNameAlias", () =>
        {
            Dictionary<string, string> dic = new();
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                object attribute = prop.GetCustomAttribute(typeof(ColumnAttribute), false);
                if (attribute != null)
                {
                    var attr = (ColumnAttribute)attribute;
                    bool isHide = prop.GetCustomAttribute(typeof(HideAttribute), false)!.IsNotNull();
                    // Column 和 Hide 特性都可以设置隐藏
                    if (!attr.Hide && !isHide)
                    {
                        dic.Add(attr.Name, attr.Display);
                    }
                }
            }

            if (dic.Count != 0) return dic;

            List<DbColumnInfo> dbCols = _db.DbMaintenance.GetColumnInfosByTableName(_tableName);
            if (dbCols.Any())
            {
                dbCols.ForEach(x => { dic.Add(x.DbColumnName, x.ColumnDescription); });
            }

            return dic;
        }, new TimeSpan(6, 0, 0));
    }

    /// <summary>
    /// 获取列表显示的字段（用于界面显示）
    /// </summary>
    /// <returns></returns>
    public virtual string GetDisplayColumns()
    {
        List<DbColumnInfo> cols = _db.DbMaintenance.GetColumnInfosByTableName(_tableName);
        return cols.Any() ? cols.Select(x => x.ColumnDescription).Splice() : "";
    }

    /// <summary>
    /// 获取指定字段的报表数据
    /// </summary>
    /// <param name="fieldName">表字段</param>
    /// <param name="condition">查询条件</param>
    /// <returns></returns>
    public virtual async Task<DataTable> GetReportDataAsync(string fieldName, string condition)
    {
        string where = "";
        if (!string.IsNullOrEmpty(condition))
        {
            where = $"Where {condition}";
        }

        string sql =
            $"select {fieldName} as argument, count(*) as datavalue from {_tableName} {where} group by {fieldName} order by count(*) desc";

        return await SqlTableAsync(sql);
    }

    #endregion

    #region 存储过程执行通用方法

    /// <summary>
    /// 执行存储过程，如果影响记录数，返回True，否则为False，修改并输出外部参数outParameters（如果有）。
    /// </summary>
    /// <param name="storeProcName">存储过程名称</param>
    /// <param name="inParameters">输入参数，可为空</param>
    /// <param name="outParameters">输出参数，可为空</param>
    /// <returns>如果影响记录数，返回True，否则为False</returns>
    public virtual async Task<bool> StorePorcExecuteAsync(string storeProcName, Hashtable inParameters, RefAsync<Hashtable> outParameters)
    {
        //参数传入
        List<SugarParameter> par = new();
        if (inParameters != null)
        {
            par.AddRange(SetStoreParameters(inParameters));
        }

        if (outParameters != null)
        {
            par.AddRange(SetStoreParameters(outParameters, true));
        }

        //获取执行结果
        bool result = await _db.Ado.UseStoredProcedure().ExecuteCommandAsync(storeProcName, par) > 0;

        //获取输出参数的值
        if (outParameters != null)
        {
            outParameters = EditOutParameters(par);
        }

        return result;
    }

    /// <summary>
    /// 执行存储过程，返回实体列表集合，修改并输出外部参数outParameters（如果有）。
    /// </summary>
    /// <param name="storeProcName">存储过程名称</param>
    /// <param name="inParameters">输入参数，可为空</param>
    /// <param name="outParameters">输出参数，可为空</param>
    /// <returns>返回实体列表集合</returns>
    public async Task<List<T>> StorePorcToListAsync(string storeProcName, Hashtable inParameters, RefAsync<Hashtable> outParameters)
    {
        //参数传入
        List<SugarParameter> par = new();
        if (inParameters != null)
        {
            par.AddRange(SetStoreParameters(inParameters));
        }

        if (outParameters != null)
        {
            par.AddRange(SetStoreParameters(outParameters, true));
        }

        //获取执行结果
        List<T> result = await _db.Ado.UseStoredProcedure().SqlQueryAsync<T>(storeProcName, par);

        //获取输出参数的值
        if (outParameters != null)
        {
            outParameters = EditOutParameters(par);
        }

        return result;
    }

    /// <summary>
    /// 执行存储过程，返回DataTable集合，修改并输出外部参数outParameters（如果有）。
    /// </summary>
    /// <param name="storeProcName">存储过程名称</param>
    /// <param name="inParameters">输入参数，可为空</param>
    /// <param name="outParameters">输出参数，可为空</param>
    /// <returns>返回DataTable集合</returns>
    public virtual async Task<DataTable> StorePorcToDataTableAsync(string storeProcName, Hashtable inParameters, RefAsync<Hashtable> outParameters)
    {
        //参数传入
        List<SugarParameter> par = new();
        if (inParameters != null)
        {
            par.AddRange(SetStoreParameters(inParameters));
        }

        if (outParameters != null)
        {
            par.AddRange(SetStoreParameters(outParameters, true));
        }

        //获取执行结果
        DataTable result = await _db.Ado.UseStoredProcedure().GetDataTableAsync(storeProcName, par);

        //获取输出参数的值
        if (outParameters != null)
        {
            outParameters = EditOutParameters(par);
        }

        return result;
    }

    /// <summary>
    /// 执行存储过程，返回实体对象，修改并输出外部参数outParameters（如果有）。
    /// </summary>
    /// <param name="storeProcName">存储过程名称</param>
    /// <param name="inParameters">输入参数，可为空</param>
    /// <param name="outParameters">输出参数，可为空</param>
    /// <returns>返回实体对象</returns>
    public virtual async Task<T> StorePorcToEntityAsync(string storeProcName, Hashtable inParameters, RefAsync<Hashtable> outParameters)
    {
        //参数传入
        List<SugarParameter> par = new();
        if (inParameters != null)
        {
            par.AddRange(SetStoreParameters(inParameters));
        }

        if (outParameters != null)
        {
            par.AddRange(SetStoreParameters(outParameters, true));
        }

        //获取执行结果
        T result = await _db.Ado.UseStoredProcedure().SqlQuerySingleAsync<T>(storeProcName, par);

        //获取输出参数的值
        if (outParameters != null)
        {
            outParameters = EditOutParameters(par);
        }

        return result;
    }

    /// <summary>
    /// 输入参数和输出参数转换。
    /// </summary>
    /// <param name="parameters">参数的哈希表</param>
    /// <param name="isOutput">是否是输出参数</param>
    private static IEnumerable<SugarParameter> SetStoreParameters(Hashtable parameters, bool isOutput = false)
    {
        if (parameters == null)
        {
            return null;
        }

        List<SugarParameter> result = new();
        foreach (DictionaryEntry item in parameters)
        {
            var parameter = new SugarParameter(item.Key.ToString(), item.Value);
            if (isOutput)
            {
                parameter.Direction = ParameterDirection.Output;
            }

            result.Add(parameter);
        }

        return result;
    }

    /// <summary>
    /// 执行存储过程后，获取需要输出的参数值，修改存储在哈希表里
    /// </summary>
    /// <param name="parameters">输出参数的哈希表</param>
    private static Hashtable EditOutParameters(List<SugarParameter> parameters)
    {
        if (parameters == null)
        {
            return null;
        }

        Hashtable result = new();
        foreach (SugarParameter item in parameters)
        {
            if (item.Direction == ParameterDirection.Output)
            {
                result.Add(item.ParameterName, item.Value);
            }
        }

        return result;
    }

    #endregion

    #region 多表查询

    /// <summary> 
    ///查询-多表查询
    /// </summary> 
    /// <typeparam name="T1">实体1</typeparam> 
    /// <typeparam name="T2">实体2</typeparam> 
    /// <typeparam name="T3">实体3</typeparam>
    /// <typeparam name="TResult">返回对象</typeparam>
    /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param> 
    /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
    /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param> 
    /// <returns>值</returns>
    public async Task<List<TResult>> QueryMuch<T1, T2, T3, TResult>(
        Expression<Func<T1, T2, T3, object[]>> joinExpression,
        Expression<Func<T1, T2, T3, TResult>> selectExpression,
        Expression<Func<T1, T2, T3, bool>> whereLambda = null) where T1 : class, new()
    {
        if (whereLambda == null)
        {
            return await _db.Queryable(joinExpression).Select(selectExpression).ToListAsync();
        }

        return await _db.Queryable(joinExpression).Where(whereLambda).Select(selectExpression).ToListAsync();
    }

    /// <summary>
    /// 两表联合查询-分页
    /// </summary>
    /// <typeparam name="T1">实体1</typeparam>
    /// <typeparam name="T2">实体1</typeparam>
    /// <typeparam name="TResult">返回对象</typeparam>
    /// <param name="joinExpression">关联表达式</param>
    /// <param name="selectExpression">返回表达式</param>
    /// <param name="whereExpression">查询表达式</param>
    /// <param name="pageInput">分页参数</param>
    /// <returns></returns>
    public async Task<PageResult<TResult>> QueryTabsPage<T1, T2, TResult>(
        Expression<Func<T1, T2, object[]>> joinExpression,
        Expression<Func<T1, T2, TResult>> selectExpression,
        Expression<Func<TResult, bool>> whereExpression,
        PageInput pageInput) where TResult : new()
    {
        //如果不指定排序字段，用默认的
        string fieldToSort = !string.IsNullOrEmpty(pageInput.SortField) ? pageInput.SortField : SortField;

        return await _db.Queryable(joinExpression)
            .Select(selectExpression)
            .OrderByIF(!fieldToSort.IsNullOrEmpty(), $"{fieldToSort} {pageInput.SortOrder}")
            .WhereIF(whereExpression != null, whereExpression)
            .ToPagedListAsync(pageInput.PageNo, pageInput.PageSize);
    }

    /// <summary>
    /// 两表联合查询-分页-分组
    /// </summary>
    /// <typeparam name="T1">实体1</typeparam>
    /// <typeparam name="T2">实体1</typeparam>
    /// <typeparam name="TResult">返回对象</typeparam>
    /// <param name="joinExpression">关联表达式</param>
    /// <param name="selectExpression">返回表达式</param>
    /// <param name="whereExpression">查询表达式</param>
    /// <param name="groupExpression">group表达式</param>
    /// <param name="pageInput">分页参数</param>
    /// <returns></returns>
    public async Task<PageResult<TResult>> QueryTabsPage<T1, T2, TResult>(
        Expression<Func<T1, T2, object[]>> joinExpression,
        Expression<Func<T1, T2, TResult>> selectExpression,
        Expression<Func<TResult, bool>> whereExpression,
        Expression<Func<T1, object>> groupExpression,
        PageInput pageInput) where TResult : new()
    {
        //如果不指定排序字段，用默认的
        string fieldToSort = !string.IsNullOrEmpty(pageInput.SortField) ? pageInput.SortField : SortField;

        return await _db.Queryable(joinExpression).GroupBy(groupExpression)
            .Select(selectExpression)
            .OrderByIF(!fieldToSort.IsNullOrEmpty(), $"{fieldToSort} {pageInput.SortOrder}")
            .WhereIF(whereExpression != null, whereExpression)
            .ToPagedListAsync(pageInput.PageNo, pageInput.PageSize);
    }

    #endregion
}