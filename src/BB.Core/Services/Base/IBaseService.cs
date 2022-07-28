using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BB.Entity.Base;
using BB.Tools.Entity;
using BB.Tools.Validation;

namespace BB.Core.Services.Base;

public interface IBaseService<T> where T : BaseEntity, new()
{
    /// <summary>
    /// 插入指定对象到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    Task<bool> InsertAsync(T obj);

    /// <summary>
    /// 插入指定对象集合到数据库中
    /// </summary>
    /// <param name="list">指定的对象集合</param>
    /// <returns>执行操作是否成功。</returns>
    Task<bool> InsertRangeAsync(List<T> list);

    /// <summary>
    /// 更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    Task<bool> UpdateAsync(T obj);

    /// <summary>
    /// 更新某个表一条记录(只适用于用单键,用string类型作键值的表)
    /// </summary>
    /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
    Task<bool> UpdateAsync(Hashtable recordField);

    /// <summary>
    /// 更新对象属性到数据库中
    /// </summary>
    /// <param name="recordField">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    Task<bool> UpdateFieldsAsync(Hashtable recordField);

    /// <summary>
    /// 更新某个表的记录(根据条件更新)
    /// </summary>
    /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
    /// <param name="condition">查询的条件</param>
    Task<bool> UpdateFieldsByConditionAsync(Hashtable recordField, string condition);

    /// <summary>
    /// 更新某个表的记录(根据条件更新)
    /// </summary>
    /// <param name="recordField">Hashtable:键[key]为字段名;值[value]为字段对应的值</param>
    /// <param name="expression">查询的条件</param>
    Task<bool> UpdateFieldsByConditionAsync(Hashtable recordField, Expression<Func<T,bool>> expression);

    /// <summary>
    /// 插入或更新对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    Task<bool> InsertUpdateAsync(T obj);

    /// <summary>
    /// 如果不存在记录，则插入对象属性到数据库中
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行插入成功返回<c>true</c>，否则为<c>false</c>。</returns>
    Task<bool> InsertIfNewAsync(T obj);

    /// <summary>
    /// 执行SQL查询语句，返回查询结果的所有记录的第一个字段,用逗号分隔。
    /// </summary>
    /// <param name="sql">SQL语句</param>
    /// <returns>
    /// 返回查询结果的所有记录的第一个字段,用逗号分隔。
    /// </returns>
    Task<string> SqlValueListAsync(string sql);

    /// <summary>
    /// 执行SQL查询语句，返回所有记录的DataTable集合。
    /// </summary>
    /// <param name="sql">SQL查询语句</param>
    /// <returns></returns>
    Task<DataTable> SqlTableAsync(string sql);

    /// <summary>
    /// 执行SQL查询语句，返回所有记录的DataTable集合。
    /// </summary>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="parameters">参数集合</param>
    /// <returns></returns>
    Task<DataTable> SqlTableAsync(string sql, DbParameter[] parameters);

    /// <summary>
    /// 执行SQL查询语句,并返回DataTable集合(用于分页数据显示)
    /// </summary>
    /// <param name="sql">SQL查询语句</param>
    /// <param name="info">分页实体</param>
    /// <returns>指定DataTable的集合</returns>
    Task<DataTable> SqlTableWithPagerAsync(string sql, PageInput info);

    /// <summary>
    /// 查询数据库,检查是否存在指定ID的对象
    /// </summary>
    /// <param name="key">对象的ID值</param>
    /// <returns>存在则返回指定的对象,否则返回Null</returns>
    Task<T> FindByIdAsync(object key);

    /// <summary>
    /// 根据条件查询数据库,返回仅有的一个对象，如有多个则抛出异常
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="orderBy">自定义排序语句，如Name desc；如不指定，则使用默认排序</param>
    /// <param name="paramList">排序类型</param>
    /// <returns>指定的对象</returns>
    Task<T> FindSingleAsync(string condition, string orderBy = "", IDbDataParameter[] paramList = null);

    /// <summary>
    /// 根据条件查询数据库,返回仅有的一个对象，如有多个则抛出异常
    /// </summary>
    /// <param name="expression">查询的条件</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序类型</param>
    /// <returns>指定的对象</returns>
    Task<T> FindSingleAsync(Expression<Func<T,bool>> expression, Expression<Func<T, object>> orderByExp = null,
        OrderByType orderByType = OrderByType.Desc);

    /// <summary>
    /// 查找记录表中最旧的一条记录
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="orderByField">自定义排序字段</param>
    /// <returns></returns>
    Task<T> FindFirstAsync(string condition = "", string orderByField = "");

    /// <summary>
    /// 查找记录表中最新的一条记录
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="orderByField">自定义排序字段</param>
    /// <returns></returns>
    Task<T> FindLastAsync(string condition = "", string orderByField = "");

    /// <summary>
    /// 根据条件获取指定的记录(子类根据不同数据库实现)
    /// </summary>
    /// <param name="condition">查询语句</param>
    /// <param name="orderBy">自定义排序语句，如 Name Desc；如不指定，则使用默认排</param>
    /// <returns></returns>
    Task<T> FindTopAsync(string condition, string orderBy);

    /// <summary>
    /// 根据条件获取第一条记录
    /// </summary>
    /// <param name="expression">查询语句</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序方式</param>
    /// <returns></returns>
    Task<T> FindTopAsync(Expression<Func<T,bool>> expression, Expression<Func<T, object>> orderByExp = null,
        OrderByType orderByType = OrderByType.Desc);

    /// <summary>
    /// 根据条件获取指定的记录
    /// </summary>
    /// <param name="count">指定数量</param>
    /// <param name="condition">查询语句</param>
    /// <param name="orderBy">自定义排序语句，如 Name Desc；如不指定，则使用默认排序</param>
    /// <returns></returns>
    Task<List<T>> FindTopListAsync(int count, string condition, string orderBy);

    /// <summary>
    /// 根据条件获取指定的记录
    /// </summary>
    /// <param name="count">指定数量</param>
    /// <param name="expression">查询语句</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序方式</param>
    /// <returns></returns>
    Task<List<T>> FindTopListAsync(int count, Expression<Func<T,bool>> expression,
        Expression<Func<T, object>> orderByExp = null, OrderByType orderByType = OrderByType.Desc);

    /// <summary>
    /// 通过外键获取表数据
    /// </summary>
    /// <param name="foreignKeyId">外键ID</param>
    /// <param name="foreignKeyName">外键名称</param>
    /// <returns>数据列表</returns>
    Task<List<T>> FindByForeignKeyAsync(object foreignKeyId, string foreignKeyName = null);

    /// <summary>
    /// 通过外键获取表ID
    /// </summary>
    /// <param name="foreignKeyId">外键ID</param>
    /// <param name="foreignKeyName">外键名称</param>
    /// <returns>ID列表</returns>
    Task<List<string>> FindIdByForeignKeyAsync(object foreignKeyId, string foreignKeyName = null);

    /// <summary>
    /// 根据多个主键获取对象列表
    /// </summary>
    /// <param name="ids">主键数组</param>
    /// <returns>符合条件的对象列表</returns>
    Task<List<T>> FindByIDsAsync(object[] ids);

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="orderBy">排序条件</param>
    /// <param name="paramList">参数列表</param>
    /// <returns>指定对象的集合</returns>
    Task<List<T>> FindAsync(string condition, string orderBy = "", IDbDataParameter[] paramList = null);

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合
    /// </summary>
    /// <param name="expression">查询的条件</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序方式</param>
    /// <returns>指定对象的集合</returns>
    Task<List<T>> FindAsync(Expression<Func<T,bool>> expression, Expression<Func<T, object>> orderByExp = null,
        OrderByType orderByType = OrderByType.Desc );

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <param name="info">分页实体</param>
    /// <returns>指定对象的集合</returns>
    Task<PageResult<T>> FindWithPagerAsync(string condition, PageInput info);

    /// <summary>
    /// 根据条件查询数据库,并返回对象集合(用于分页数据显示)
    /// </summary>
    /// <param name="whereExpression">查询的条件</param>
    /// <param name="info">分页参数</param>
    /// <returns>指定对象的集合</returns>
    Task<PageResult<T>> FindWithPagerAsync(Expression<Func<T, bool>> whereExpression, PageInput info);

    /// <summary>
    /// 返回数据库所有的对象集合
    /// </summary>
    /// <param name="orderBy">自定义排序语句，如 Name Desc；如不指定，则使用默认排</param>
    /// <returns>指定对象的集合</returns>
    Task<List<T>> GetAllAsync(string orderBy = "");

    /// <summary>
    /// 返回数据库所有的对象集合(用于分页数据显示)
    /// </summary>
    /// <param name="info">分页实体信息</param>
    /// <returns>指定对象的集合</returns>
    Task<PageResult<T>> GetAllAsync(PageInput info);

    /// <summary>
    /// 返回所有记录到DataTable集合中
    /// </summary>
    /// <param name="orderBy">自定义排序语句，如 Name Desc；如不指定，则使用默认排</param>
    /// <returns></returns>
    Task<DataTable> GetAllToDataTableAsync(string orderBy = "");

    /// <summary>
    /// 根据查询条件，返回记录到DataTable集合中
    /// </summary>
    /// <param name="condition">查询条件</param>
    /// <returns></returns>
    Task<DataTable> FindToDataTableAsync(NameValueCollection condition);

    /// <summary>
    /// 根据查询条件，返回记录到DataTable集合中
    /// </summary>
    /// <param name="condition">查询条件</param>
    /// <param name="pagerInfo">分页条件</param>
    /// <returns></returns>
    Task<DataTable> FindToDataTableAsync(string condition = "", PageInput pagerInfo = null);

    /// <summary>
    /// 根据条件查询数据库,并返回DataTable集合
    /// </summary>
    /// <param name="expression">查询的条件</param>
    /// <param name="orderByExp">自定义排序语句</param>
    /// <param name="orderByType">排序方式</param>
    /// <returns>指定DataTable的集合</returns>
    Task<DataTable> FindToDataTableAsync(Expression<Func<T,bool>> expression, Expression<Func<T, object>> orderByExp = null,
        OrderByType orderByType = OrderByType.Desc);

    /// <summary>
    /// 获取字段列表
    /// </summary>
    /// <param name="fieldName">字段名称</param>
    /// <param name="condition">查询的条件</param>
    /// <returns></returns>
    Task<List<string>> GetFieldListAsync(string fieldName, string condition = "");

    /// <summary>
    /// 根据条件，从视图里面获取记录
    /// </summary>
    /// <param name="viewName">视图名称</param>
    /// <param name="condition">查询条件</param>
    /// <param name="sortField">排序字段</param>
    /// <param name="isDescending">是否为降序</param>
    /// <returns></returns>
    Task<DataTable> FindByViewAsync(string viewName, string condition, string sortField = "",
        bool isDescending = default);

    /// <summary>
    /// 根据条件，从视图里面获取记录
    /// </summary>
    /// <param name="viewName">视图名称</param>
    /// <param name="condition">查询条件</param>
    /// <param name="info">分页条件</param>
    /// <returns></returns>
    Task<DataTable> FindByViewWithPagerAsync(string viewName, string condition, PageInput info);

    /// <summary>
    /// 获取表的指定条件记录数量
    /// </summary>
    /// <param name="condition">查询条件</param>
    /// <returns></returns>
    Task<int> GetRecordCountAsync(string condition = "");

    /// <summary>
    /// 获取表的所有记录数量
    /// </summary>
    /// <param name="expression">查询条件</param>
    /// <returns></returns>
    Task<int> GetRecordCountAsync(Expression<Func<T,bool>> expression);

    /// <summary>
    /// 根据condition条件，判断是否存在记录
    /// </summary>
    /// <param name="condition">查询的条件</param>
    /// <returns>如果存在返回True，否则False</returns>
    Task<bool> IsExistRecordAsync(string condition);

    /// <summary>
    /// 根据 expression 条件，判断是否存在记录
    /// </summary>
    /// <param name="expression">查询的条件</param>
    /// <returns>如果存在返回True，否则False</returns>
    Task<bool> IsExistRecordAsync(Expression<Func<T,bool>> expression);

    /// <summary>
    /// 查询数据库,检查是否存在指定键值的对象
    /// </summary>
    /// <param name="fieldName">指定的属性名</param>
    /// <param name="key">指定的值</param>
    /// <returns>存在则返回<c>true</c>，否则为<c>false</c>。</returns>
    Task<bool> IsExistKeyAsync(string fieldName, object key);

    /// <summary>
    /// 根据主键和字段名称，获取对应字段的内容
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <param name="fieldName">字段名称</param>
    /// <returns></returns>
    Task<string> GetFieldValueAsync(object key, string fieldName);

    /// <summary>
    /// 根据主键和字段名称，获取对应字段的内容
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <param name="fieldNameList">字段名称列表</param>
    /// <returns></returns>
    Task<Dictionary<string, string>> GetFieldValueListAsync(string key, List<string> fieldNameList);

    /// <summary>
    /// 从数据库中删除指定对象
    /// </summary>
    /// <param name="entity">指定对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    Task<bool> DeleteAsync(T entity);

    /// <summary>
    /// 根据指定对象的ID,从数据库中删除指定对象
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    Task<bool> DeleteAsync(object key);

    /// <summary>
    /// 根据指定条件,从数据库中删除指定对象
    /// </summary>
    /// <param name="expression">条件表达式</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    Task<bool> DeleteAsync(Expression<Func<T,bool>> expression);

    /// <summary>
    /// 根据指定条件,从数据库中删除指定对象
    /// </summary>
    /// <param name="condition">删除记录的条件语句</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    Task<bool> DeleteByConditionAsync(string condition);

    /// <summary>
    /// 打开数据库连接，并创建事务对象
    /// </summary>
    DbTransaction CreateTransaction();

    /// <summary>
    /// 打开数据库连接，并创建事务对象
    /// </summary>
    /// <param name="level">事务级别</param>
    DbTransaction CreateTransaction(IsolationLevel level);

    /// <summary>
    /// 根据条件获取实体数据
    /// </summary>
    /// <param name="searchInfos">参数名和参数值</param>
    /// <returns></returns>
    Task<List<T>> GetEntitiesAsync(NameValueCollection searchInfos);

    /// <summary>
    /// 根据条件获取实体数据
    /// </summary>
    /// <param name="searchInfos">参数名和参数值</param>
    /// <param name="pagerInfo">分页条件</param>
    /// <returns></returns>
    Task<PageResult<T>> GetEntitiesAsync(NameValueCollection searchInfos, PageInput pagerInfo);

    /// <summary>
    /// 通用审批
    /// </summary>
    /// <param name="key">主键</param>
    /// <returns></returns>
    Task<bool> ApproveAsync(object key);

    /// <summary>
    /// 初始化一个实体
    /// </summary>
    /// <returns></returns>
    Task<T> NewEntityAsync();

    /// <summary>
    /// 验证一个实体或键值对
    /// </summary>
    /// <param name="operationType">操作类型</param>
    /// <param name="obj">实体或键值对</param>
    /// <returns></returns>
    void CheckEntity(OperationType operationType, object obj);

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
    void CheckEntityValue(ValidateType validateType, object value, string argumentName,
        bool checkNull = false, int max = 0, int min = 0, string errorText = null);

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
    Task CheckUniqueAsync(string parameterName, string parameterDis, object obj, string primaryKeyName = null,
        object primaryKeyValue = null, string errorMessage = null);

    /// <summary>
    /// 获取字段显示权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotSupportedException"></exception>
    Task<Dictionary<string, int>> GetPermitDictAsync();

    /// <summary>
    /// 获取表的字段名称和数据类型列表
    /// </summary>
    /// <returns></returns>
    Task<DataTable> GetFieldTypeListAsync();

    /// <summary>
    /// 获取字段中文别名（用于界面显示）的字典集合
    /// </summary>
    /// <returns></returns>
    Task<Dictionary<string, string>> GetColumnNameAliasAsync();

    /// <summary>
    /// 获取列表显示的字段（用于界面显示）
    /// </summary>
    /// <returns></returns>
    Task<string> GetDisplayColumnsAsync();

    /// <summary>
    /// 获取指定字段的报表数据
    /// </summary>
    /// <param name="fieldName">表字段</param>
    /// <param name="condition">查询条件</param>
    /// <returns></returns>
    Task<DataTable> GetReportDataAsync(string fieldName, string condition);
}