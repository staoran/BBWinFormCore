using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BB.Core.DbContext;
using BB.Entity.Base;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using BB.Tools.Validation;

namespace BB.Core.Services.Base;

public class BaseService<T> where T : BaseEntity, new()
{
    #region 构造函数

    /// <summary>
    /// 数据仓储
    /// </summary>
    protected readonly BaseRepository<T> Repository;

    /// <summary>
    /// 登陆用户基础信息（泛型类继承的父类中的静态对象是共享的）
    /// </summary>
    protected readonly LoginUserInfo LoginUserInfo;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">数据仓储</param>
    public BaseService(BaseRepository<T> repository)
    {
        Repository = repository;
        
        LoginUserInfo = App.User.Adapt<LoginUserInfo>();
    }

    #endregion

    #region 对象添加、修改、查询接口

    /// <summary>
    /// 插入指定对象到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    public virtual async Task<bool> InsertAsync(T obj)
    {
        CheckEntity(OperationType.Add, obj);

        return await Repository.InsertAsync(obj);
    }

    /// <summary>
    /// 插入指定对象集合到数据库中
    /// </summary>
    /// <param name="list">指定的对象集合</param>
    /// <returns>执行操作是否成功。</returns>
    public virtual async Task<bool> InsertRangeAsync(List<T> list)
    {
        list.ForEach(x => CheckEntity(OperationType.Add, x));
        return await Repository.InsertRangeAsync(list);
    }

    /// <summary>
    /// 更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public virtual async Task<bool> UpdateAsync(T obj)
    {
        CheckEntity(OperationType.Edit, obj);
        return await Repository.UpdateAsync(obj);
    }

    /// <summary>
    /// 更新对象属性到数据库中
    /// </summary>
    /// <param name="recordField">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public virtual async Task<bool> UpdateFieldsAsync(Hashtable recordField)
    {
        CheckEntity(OperationType.Edit, recordField);
        return await Repository.UpdateFieldsAsync(recordField);
    }

    /// <summary>
    /// 更新某个表的记录(根据条件更新)
    /// </summary>
    /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
    /// <param name="condition">查询的条件</param>
    [NonAction]
    public virtual async Task<bool> UpdateFieldsByConditionAsync(Hashtable recordField, string condition)
    {
        return await Repository.UpdateFieldsByConditionAsync(recordField, condition);
    }

    /// <summary>
    /// 更新某个表的记录(根据条件更新)
    /// </summary>
    /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
    /// <param name="expression">查询的条件</param>
    [NonAction]
    public virtual async Task<bool> UpdateFieldsByConditionAsync(Hashtable recordField, Expression<Func<T,bool>> expression)
    {
        return await Repository.UpdateFieldsByConditionAsync(recordField, expression);
    }

    /// <summary>
    /// 更新某个表一条记录(只适用于用单键,用string类型作键值的表)
    /// </summary>
    /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
    [NonAction]
    public virtual async Task<bool> UpdateAsync(Hashtable recordField)
    {
        CheckEntity(OperationType.Edit, recordField);
        return await Repository.UpdateAsync(recordField);
    }

    /// <summary>
    /// 插入或更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public virtual async Task<bool> InsertUpdateAsync(T obj)
    {
        return await Repository.InsertUpdateAsync(obj);
    }

    /// <summary>
    /// 如果不存在记录，则插入对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行插入成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public virtual async Task<bool> InsertIfNewAsync(T obj)
    {
        return await Repository.InsertIfNewAsync(obj);
    }

    /// <summary>
    /// 执行SQL查询语句，返回查询结果的所有记录的第一个字段,用逗号分隔。
    /// </summary>
    /// <param name="sql">SQL语句</param>
    /// <returns>
    /// 返回查询结果的所有记录的第一个字段,用逗号分隔。
    /// </returns>
    [NonAction]
    public virtual async Task<string> SqlValueListAsync(string sql)
    {
        return await Repository.SqlValueListAsync(sql);
    }

    /// <summary>
    /// 执行SQL查询语句，返回所有记录的DataTable集合。
    /// </summary>
    /// <param name="sql">SQL查询语句</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<DataTable> SqlTableAsync(string sql)
    {
        return await Repository.SqlTableAsync(sql);
    }

    /// <summary>
    /// 执行SQL查询语句，返回所有记录的DataTable集合。
    /// </summary>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">参数集合</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<DataTable> SqlTableAsync(string sql, DbParameter[] parameters)
    {
        return await Repository.SqlTableAsync(sql, parameters);
    }

    /// <summary>
    /// 执行SQL查询语句,并返回DataTable集合(用于分页数据显示)
    /// </summary>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="info">分页实体</param>
    /// <returns>指定DataTable的集合</returns>
    [NonAction]
    public virtual async Task<DataTable> SqlTableWithPagerAsync(string sql, PageInput info)
    {
        return await Repository.SqlTableWithPagerAsync(sql, info);
    }

    /// <summary>
    /// 查询数据库,检查是否存在指定ID的对象
    /// </summary>
    /// <param name="key">对象的ID值</param>
    /// <returns>存在则返回指定的对象,否则返回Null</returns>
    public virtual async Task<T> FindByIdAsync(object key)
    {
        return await Repository.FindByIdAsync(key);
    }

    /// <summary>
    /// 根据条件查询数据库,返回仅有的一个对象，如有多个则抛出异常
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="orderBy">自定义排序语句，如Name desc；如不指定，则使用默认排序</param>
    /// <param name="paramList">排序类型</param>
    /// <returns>指定的对象</returns>
    [NonAction]
    public virtual async Task<T> FindSingleAsync(string condition, string orderBy = "", IDbDataParameter[] paramList = null)
    {
        return await Repository.FindSingleAsync(condition, orderBy, paramList);
    }

    /// <summary>
    /// 根据条件查询数据库,返回仅有的一个对象，如有多个则抛出异常
    /// </summary>
    /// <param name="expression">查询的条件</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序类型</param>
    /// <returns>指定的对象</returns>
    [NonAction]
    public virtual async Task<T> FindSingleAsync(Expression<Func<T,bool>> expression, Expression<Func<T, object>> orderByExp = null,
        OrderByType orderByType = OrderByType.Desc)
    {
        return await Repository.FindSingleAsync(expression, orderByExp, orderByType);
    }

    /// <summary>
    /// 查找记录表中最旧的一条记录
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="orderByField">自定义排序字段</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<T> FindFirstAsync(string condition = "", string orderByField = "")
    {
        return await Repository.FindFirstAsync(condition, orderByField);
    }

    /// <summary>
    /// 查找记录表中最新的一条记录
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="orderByField">自定义排序字段</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<T> FindLastAsync(string condition = "", string orderByField = "")
    {
        return await Repository.FindLastAsync(condition, orderByField);
    }

    /// <summary>
    /// 根据条件获取指定的记录(子类根据不同数据库实现)
    /// </summary>
    /// <param name="condition">查询语句</param>
    /// <param name="orderBy">自定义排序语句，如 Name Desc；如不指定，则使用默认排</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<T> FindTopAsync(string condition, string orderBy)
    {
        return await Repository.FindTopAsync(condition, orderBy);
    }

    /// <summary>
    /// 根据条件获取第一条记录
    /// </summary>
    /// <param name="expression">查询语句</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序方式</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<T> FindTopAsync(Expression<Func<T,bool>> expression, Expression<Func<T, object>> orderByExp = null,
        OrderByType orderByType = OrderByType.Desc)
    {
        return await Repository.FindTopAsync(expression, orderByExp, orderByType);
    }

    /// <summary>
    /// 根据条件获取指定的记录
    /// </summary>
    /// <param name="count">指定数量</param>
    /// <param name="condition">查询语句</param>
    /// <param name="orderBy">自定义排序语句，如 Name Desc；如不指定，则使用默认排序</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<List<T>> FindTopListAsync(int count, string condition, string orderBy)
    {
        return await Repository.FindTopListAsync(count, condition, orderBy);
    }

    /// <summary>
    /// 根据条件获取指定的记录
    /// </summary>
    /// <param name="count">指定数量</param>
    /// <param name="expression">查询语句</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序方式</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<List<T>> FindTopListAsync(int count, Expression<Func<T,bool>> expression,
        Expression<Func<T, object>> orderByExp = null, OrderByType orderByType = OrderByType.Desc)
    {
        return await Repository.FindTopListAsync(count, expression, orderByExp, orderByType);
    }

    /// <summary>
    /// 通过外键获取表数据
    /// </summary>
    /// <param name="foreignKeyId">外键ID</param>
    /// <param name="foreignKeyName">外键名称</param>
    /// <returns>数据列表</returns>
    public virtual async Task<List<T>> FindByForeignKeyAsync(object foreignKeyId, string foreignKeyName = null)
    {
        return await Repository.FindByForeignKeyAsync(foreignKeyId, foreignKeyName);
    }

    /// <summary>
    /// 通过外键获取主键ID列表
    /// </summary>
    /// <param name="foreignKeyId">外键ID</param>
    /// <param name="foreignKeyName">外键名称</param>
    /// <returns>ID列表</returns>
    public virtual async Task<List<string>> FindIdByForeignKeyAsync(object foreignKeyId, string foreignKeyName = null)
    {
        return await Repository.FindIdByForeignKeyAsync(foreignKeyId, foreignKeyName);
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
        return await Repository.FindByIDsAsync(ids);
    }

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="orderBy">排序条件</param>
    /// <param name="paramList">参数列表</param>
    /// <returns>指定对象的集合</returns>
    [NonAction]
    public virtual async Task<List<T>> FindAsync(string condition, string orderBy = "", IDbDataParameter[] paramList = null)
    {
        return await Repository.FindAsync(condition, orderBy, paramList);
    }

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合
    /// </summary>
    /// <param name="expression">查询的条件</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序方式</param>
    /// <returns>指定对象的集合</returns>
    [NonAction]
    public virtual async Task<List<T>> FindAsync(Expression<Func<T,bool>> expression, Expression<Func<T, object>> orderByExp = null,
        OrderByType orderByType = OrderByType.Desc )
    {
        return await Repository.FindAsync(expression, orderByExp, orderByType);
    }

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="info">分页实体</param>
    /// <returns>指定对象的集合</returns>
    [NonAction]
    public virtual async Task<PageResult<T>> FindWithPagerAsync(string condition, PageInput info)
    {
        return await Repository.FindWithPagerAsync(condition, info);
    }

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
    /// </summary>
    /// <param name="whereExpression">查询的条件</param>
    /// <param name="info">分页参数</param>
    /// <returns>指定对象的集合</returns>
    [NonAction]
    public virtual async Task<PageResult<T>> FindWithPagerAsync(Expression<Func<T, bool>> whereExpression, PageInput info)
    {
        return await Repository.FindWithPagerAsync(whereExpression, info);
    }

    /// <summary>
    /// 返回当前模块所有数据
    /// </summary>
    /// <param name="orderBy">自定义排序语句，如 Name Desc；如不指定，则使用默认排</param>
    /// <returns>指定对象的集合</returns>
    [NonAction]
    public virtual async Task<List<T>> GetAllAsync(string orderBy = "")
    {
        return await Repository.GetAllAsync(orderBy);
    }

    /// <summary>
    /// 返回当前模块所有数据(用于分页数据显示)
    /// </summary>
    /// <param name="info">分页实体信息</param>
    /// <returns>指定对象的集合</returns>
    public virtual async Task<PageResult<T>> GetAllAsync(PageInput info)
    {
        return await Repository.GetAllAsync(info);
    }

    /// <summary>
    /// 返回所有记录到DataTable集合中
    /// </summary>
    /// <param name="orderBy">自定义排序语句，如 Name Desc；如不指定，则使用默认排</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<DataTable> GetAllToDataTableAsync(string orderBy = "")
    {
        return await Repository.GetAllToDataTableAsync(orderBy);
    }

    /// <summary>
    /// 根据查询条件，返回记录到DataTable集合中
    /// </summary>
    /// <param name="condition">查询条件</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<DataTable> FindToDataTableAsync(NameValueCollection condition)
    {
        string where = GetConditionSql(condition);
        return await FindToDataTableAsync(where);
    }

    /// <summary>
    /// 根据查询条件，返回记录到DataTable集合中
    /// </summary>
    /// <param name="condition">查询条件</param>
    /// <param name="pagerInfo">分页条件</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<DataTable> FindToDataTableAsync(string condition = "", PageInput pagerInfo = null)
    {
        return await Repository.FindToDataTableAsync(condition, pagerInfo);
    }

    /// <summary>
    /// 根据条件查询数据库,并返回DataTable集合
    /// </summary>
    /// <param name="expression">查询的条件</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序方式</param>
    /// <returns>指定DataTable的集合</returns>
    [NonAction]
    public virtual async Task<DataTable> FindToDataTableAsync(Expression<Func<T,bool>> expression, Expression<Func<T, object>> orderByExp = null,
        OrderByType orderByType = OrderByType.Desc)
    {
        return await Repository.FindToDataTableAsync(expression, orderByExp, orderByType);
    }

    /// <summary>
    /// 获取字段列表
    /// </summary>
    /// <param name="fieldName">字段名称</param>
    /// <param name="condition">查询的条件</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<List<string>> GetFieldListAsync(string fieldName, string condition = "")
    {
        return await Repository.GetFieldListByConditionAsync(fieldName, condition);
    }

    /// <summary>
    /// 根据条件，从视图里面获取记录
    /// </summary>
    /// <param name="viewName">视图名称</param>
    /// <param name="condition">查询条件</param>
    /// <param name="sortField">排序字段</param>
    /// <param name="isDescending">是否为降序</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<DataTable> FindByViewAsync(string viewName, string condition, string sortField = "",
        bool isDescending = default)
    {
        return await Repository.FindByViewAsync(viewName, condition, sortField, isDescending);
    }

    /// <summary>
    /// 根据条件，从视图里面获取记录
    /// </summary>
    /// <param name="viewName">视图名称</param>
    /// <param name="condition">查询条件</param>
    /// <param name="info">分页条件</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<DataTable> FindByViewWithPagerAsync(string viewName, string condition, PageInput info)
    {
        return await Repository.FindByViewWithPagerAsync(viewName, condition, info);
    }

    #endregion

    #region 基础接口

    /// <summary>
    /// 获取表的指定条件记录数量
    /// </summary>
    /// <param name="condition">查询条件</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<int> GetRecordCountAsync(string condition = "")
    {
        return await Repository.GetRecordCountAsync(condition);
    }

    /// <summary>
    /// 获取表的所有记录数量
    /// </summary>
    /// <param name="expression">查询条件</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<int> GetRecordCountAsync(Expression<Func<T,bool>> expression)
    {
        return await Repository.GetRecordCountAsync(expression);
    }

    /// <summary>
    /// 根据condition条件，判断是否存在记录
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <returns>如果存在返回True，否则False</returns>
    [NonAction]
    public virtual async Task<bool> IsExistRecordAsync(string condition)
    {
        return await Repository.IsExistRecordAsync(condition);
    }

    /// <summary>
    /// 根据 expression 条件，判断是否存在记录
    /// </summary>
    /// <param name="expression">查询的条件</param>
    /// <returns>如果存在返回True，否则False</returns>
    [NonAction]
    public virtual async Task<bool> IsExistRecordAsync(Expression<Func<T,bool>> expression)
    {
        return await Repository.IsExistRecordAsync(expression);
    }

    /// <summary>
    /// 查询数据库,检查是否存在指定键值的对象
    /// </summary>
    /// <param name="fieldName">指定的属性名</param>
    /// <param name="key">指定的值</param>
    /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
    public virtual async Task<bool> IsExistKeyAsync(string fieldName, object key)
    {
        return await Repository.IsExistKeyAsync(fieldName, key);
    }

    /// <summary>
    /// 根据主键和字段名称，获取对应字段的内容
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    public virtual async Task<string> GetFieldValueAsync(object key, string fieldName)
    {
        return await Repository.GetFieldValueAsync(key, fieldName);
    }

    /// <summary>
    /// 根据主键和字段名称，获取对应字段的内容
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <param name="fieldNameList">字段名称列表</param>
    /// <returns></returns>
    public virtual async Task<Dictionary<string, string>> GetFieldValueListAsync(string key, List<string> fieldNameList)
    {
        return await Repository.GetFieldValueListAsync(key, fieldNameList);
    }

    /// <summary>
    /// 从数据库中删除指定对象
    /// </summary>
    /// <param name="entity">指定对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [NonAction]
    public virtual async Task<bool> DeleteAsync(T entity)
    {
        return await Repository.DeleteAsync(entity);
    }

    /// <summary>
    /// 根据指定对象的ID,从数据库中删除指定对象
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public virtual async Task<bool> DeleteAsync(object key)
    {
        return await Repository.DeleteAsync(key);
    }

    /// <summary>
    /// 根据指定条件,从数据库中删除指定对象
    /// </summary>
    /// <param name="expression">条件表达式</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [NonAction]
    public virtual async Task<bool> DeleteAsync(Expression<Func<T,bool>> expression)
    {
        return await Repository.DeleteAsync(expression);
    }

    /// <summary>
    /// 根据指定条件,从数据库中删除指定对象
    /// </summary>
    /// <param name="condition">删除记录的条件语句</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [NonAction]
    public virtual async Task<bool> DeleteByConditionAsync(string condition)
    {
        return await Repository.DeleteByConditionAsync(condition);
    }

    /// <summary>
    /// 打开数据库连接，并创建事务对象
    /// </summary>
    [NonAction]
    public virtual DbTransaction CreateTransaction()
    {
        return Repository.CreateTransaction();
    }

    /// <summary>
    /// 打开数据库连接，并创建事务对象
    /// </summary>
    /// <param name="level">事务级别</param>
    [NonAction]
    public virtual DbTransaction CreateTransaction(IsolationLevel level)
    {
        return Repository.CreateTransaction(level);
    }
    
    /// <summary>
    /// 执行事务，自动提交和回滚
    /// </summary>
    /// <param name="action">事务逻辑</param>
    /// <param name="errorCallBack">错误回调</param>
    [NonAction]
    public virtual async Task<bool> UseTransactionAsync(Func<Task> action, Action<Exception> errorCallBack = null)
    {
        return await Repository.UseTransactionAsync(action, errorCallBack ?? (ex => throw Oops.Oh($"执行事务时发生错误:{ex}")));
    }

    #endregion

    #region 业务逻辑接口

    /// <summary>
    /// 构造查询语句
    /// </summary>
    /// <param name="searchInfos">查询参数</param>
    /// <returns></returns>
    [NonAction]
    protected virtual string GetConditionSql(NameValueCollection searchInfos)
    {
        var condition = new SearchCondition();
        foreach (string key in searchInfos.AllKeys)
        {
            condition.AddCondition(key, searchInfos[key], SqlOperator.Equal);
        }

        return condition.BuildConditionSql().Replace("Where", "");
    }

    /// <summary>
    /// 根据条件获取实体数据
    /// </summary>
    /// <param name="searchInfos">参数名和参数值</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<List<T>> GetEntitiesAsync(NameValueCollection searchInfos)
    {
        // todo 使用 json to sql
        string where = GetConditionSql(searchInfos);
        return await FindAsync(where);
    }

    /// <summary>
    /// 根据条件获取实体数据
    /// </summary>
    /// <param name="searchInfos">参数名和参数值</param>
    /// <param name="pagerInfo">分页条件</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<PageResult<T>> GetEntitiesAsync(NameValueCollection searchInfos, PageInput pagerInfo)
    {
        string where = GetConditionSql(searchInfos);
        return await FindWithPagerAsync(where, pagerInfo);
    }

    /// <summary>
    /// 通用审批
    /// </summary>
    /// <param name="key">主键</param>
    /// <returns></returns>
    public virtual async Task<bool> ApproveAsync(object key)
    {
        T info = await FindByIdAsync(key);

        if (info == null)
            throw Oops.Bah("数据异常，此单不存在，可能已被删除！");

        string creationDate = info.GetProperty("CreationDate").ObjToStr();
        if (creationDate.IsNullOrEmpty())
            throw new Exception("数据异常，创建时间为空！");

        var hashTable = new Hashtable()
        {
            { info.GetValue("PrimaryKey").ObjToStr(), key },
            { info.GetValue("FieldFlagApp").ObjToStr(), info.GetProperty("FlagApp").ObjToBool() ? 0 : 1 },
            { info.GetValue("FieldAppUser").ObjToStr(), LoginUserInfo.ID },
            { info.GetValue("FieldAppDate").ObjToStr(), DateTime.Now},
            { info.GetValue("FieldLastUpdatedBy").ObjToStr(), LoginUserInfo.ID },
            { info.GetValue("LastUpdateDate").ObjToStr(), info.GetProperty("LastUpdateDate")}
        };
        return await UpdateFieldsAsync(hashTable);
    }

    /// <summary>
    /// 初始化一个实体
    /// </summary>
    /// <returns></returns>
    public virtual async Task<T> NewEntityAsync()
    {
        return await Repository.NewEntityAsync();
    }

    /// <summary>
    /// 验证一个实体或键值对
    /// </summary>
    /// <param name="operationType">操作类型</param>
    /// <param name="obj">实体或键值对</param>
    /// <returns></returns>
    [NonAction]
    public virtual void CheckEntity(OperationType operationType, object obj)
    {
        ArgumentValidation.CheckForNullReference(obj, "待验证的对象为空");
    }

    /// <summary>
    /// 实体或Hashtable参数值验证
    /// </summary>
    /// <param name="validateType">验证类型</param>
    /// <param name="value">实体或Hashtable</param>
    /// <param name="argumentName">参数名称</param>
    /// <param name="checkNull">空是否验证</param>
    /// <param name="max">最大长度</param>
    /// <param name="min">最小长度</param>
    /// <param name="errorText">错误提示</param>
    /// <returns></returns>
    [NonAction]
    public virtual void CheckEntityValue(ValidateType validateType, object value, string argumentName,
        bool checkNull = false, int max = 0, int min = 0, string errorText = null)
    {
        switch (value)
        {
            case T:
            {
                #region 验证实体

                ArgumentCheck.CheckEntityValue(validateType, value.GetProperty(argumentName)!, argumentName, checkNull,
                    max, min, errorText);

                #endregion

                break;
            }
            case Hashtable ht:
            {
                #region 验证键值对

                if (ht.ContainsKey(argumentName))
                {
                    ArgumentCheck.CheckEntityValue(validateType, ht[argumentName]!, argumentName, checkNull, max, min,
                        errorText);
                }

                #endregion

                break;
            }
        }
    }

    /// <summary>
    /// 验证一个参数值是否唯一
    /// </summary>
    /// <param name="parameterName">参数名</param>
    /// <param name="parameterDis">参数显示名</param>
    /// <param name="obj">参数值或实体或Hashtable</param>
    /// <param name="primaryKeyName">主键名</param>
    /// <param name="primaryKeyValue">主键值</param>
    /// <param name="errorMessage">自定义错误提示</param>
    /// <exception cref="Exception"></exception>
    [NonAction]
    public virtual async Task CheckUniqueAsync(string parameterName, string parameterDis, object obj, string primaryKeyName = null,
        object primaryKeyValue = null, string errorMessage = null)
    {
        ArgumentValidation.CheckForNullReference(obj, "待验证的参数值为空");

        string value = obj switch
        {
            T => obj.GetProperty(parameterName).ObjToStr(),
            Hashtable ht => ht[parameterName].ObjToStr(),
            _ => obj.ObjToStr()
        };
        if (value.IsNullOrEmpty()) throw Oops.Bah("没有找到有效的待验证参数值");

        var condition = $"{parameterName} ='{value}' ";
        if (primaryKeyValue != null && primaryKeyName != null)
        {
            condition += $" and {primaryKeyName} <> '{primaryKeyValue}'";
        }

        bool exist = await IsExistRecordAsync(condition);
        if (exist)
        {
            throw Oops.Bah(errorMessage ?? $"指定的【{parameterDis}】已经存在，不能重复添加，请修改！");
        }
    }

    /// <summary>
    /// 获取字段显示权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    public virtual async Task<Dictionary<string, int>> GetPermitDictAsync()
    {
        return await Task.FromResult(Repository.GetPermitDict());

        // 字段权限表控制
        // var permitDict = BllFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(Test1Car).FullName, LoginUserInfo.ID.ToInt32());
        // return permitDict;
    }

    #endregion

    #region 其他接口

    /// <summary>
    /// 获取表的字段名称和数据类型列表
    /// </summary>
    /// <returns></returns>
    public virtual async Task<DataTable> GetFieldTypeListAsync()
    {
        return await Task.FromResult(Repository.GetFieldTypeList());
    }

    /// <summary>
    /// 获取字段中文别名（用于界面显示）的字典集合
    /// </summary>
    /// <returns></returns>
    public virtual async Task<Dictionary<string, string>> GetColumnNameAliasAsync()
    {
        return await Task.FromResult(Repository.GetColumnNameAlias());
    }

    /// <summary>
    /// 获取列表显示的字段（用于界面显示）
    /// </summary>
    /// <returns></returns>
    public virtual async Task<string> GetDisplayColumnsAsync()
    {
        return await Task.FromResult(Repository.GetDisplayColumns());
    }

    /// <summary>
    /// 获取指定字段的报表数据
    /// </summary>
    /// <param name="fieldName">表字段</param>
    /// <param name="condition">查询条件</param>
    /// <returns></returns>
    [NonAction]
    public virtual async Task<DataTable> GetReportDataAsync(string fieldName, string condition)
    {
        return await Repository.GetReportDataAsync(fieldName, condition);
    }

    #endregion
}