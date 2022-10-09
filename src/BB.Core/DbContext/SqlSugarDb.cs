﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Security.Claims;
using BB.Entity.Base;
using BB.Entity.Security;
using BB.Tools.Const;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using BB.Tools.Validation;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar.Extensions;

namespace BB.Core.DbContext;

/// <summary>
/// SqlSugar 初始化
/// </summary>
public static class SqlSugarDb
{
    /// <summary>
    /// 注入 SqlSugar 服务
    /// </summary>
    /// <param name="services"></param>
    public static void AddSqlSugarDb(this IServiceCollection services)
    {
        // 注册数据库基础配置项强类型配置
        services.Configure<List<ConnectionConfig>>(App.Configuration.GetSection("ConnectionConfigs"));
        
        // 获取数据库基础配置
        var connectionConfig = App.GetOptions<List<ConnectionConfig>>();

        // 配置外部处理服务
        var configureExternalServices = new ConfigureExternalServices
        {
            // 1 SerializeService 扩展你想要的序列化方式
            // 2 ReflectionInoCacheService缓存方式
            // 3 AppendDataReaderTypeMappings 自定义数据类型
            // 4 SqlFuncServices自定义拉姆达
            // 5 EntityService 如果不想用SqlSugar里面的 实体特性可以用这个自定义实现
            DataInfoCacheService = new ReflectionInoCacheService(), // 定义二级缓存的实现
            
            // 自定义实体配置
            EntityService = (property, column) =>
            {
                FieldInfo fieldInfo = property.DeclaringType?.GetField($"Field{property.Name}");
                if (fieldInfo != null)
                {
                    column.DbColumnName = fieldInfo.GetValue(null)!.ObjToStr();
                }

                // 根据自定义特性判断
                object[] attributes = property.GetCustomAttributes(true); //get all attributes
                attributes.ForEach(x =>
                {
                    switch (x)
                    {
                        case ColumnAttribute columnAttribute:
                            column.DbColumnName = columnAttribute.Name;
                            break;
                        case KeyAttribute:
                            column.IsPrimarykey = true;
                            break;
                        case IdentityAttribute:
                            column.IsIdentity = true;
                            break;
                        case IgnoreAttribute or NonSerializedAttribute:
                            column.IsIgnore = true;
                            break;
                        case OptimisticLockAttribute:
                            column.IsEnableUpdateVersionValidation = true;
                            // OptimisticLockKey = column.DbColumnName;
                            break;
                        // case SortAttribute sortAttribute:
                        //     SortField = column.DbColumnName;
                        //     IsDescending = sortAttribute.IsDesc;
                        //     break;
                    }
                });

                // //根据列名判断
                // if (string.Equals(property.Name, _primaryKey, StringComparison.CurrentCultureIgnoreCase))
                // {
                //     column.IsPrimarykey = true;
                // }
                //
                // if (string.Equals(property.Name, OptimisticLockKey, StringComparison.CurrentCultureIgnoreCase))
                // {
                //     column.IsEnableUpdateVersionValidation = true;
                // }
            },
            
            // 自定义表名配置
            EntityNameService = (type, entity) =>
            {
                // // 指定默认表名
                // entity.DbTableName = _tableName;

                // 如果特性有配置，则使用特性的值
                object[] attributes = type.GetCustomAttributes(true);
                if (attributes.Any(it => it is TableAttribute))
                {
                    entity.DbTableName = (attributes.First(it => it is TableAttribute) as TableAttribute)?.Name;
                }
            }
        };
        
        // 额外的配置
        var moreSettings = new ConnMoreSettings()
        {
            // 所有 增、删 、改 后自动删除 WithCache() 缓存（子查询或者 Mergetable 无法自动清除）
            IsAutoRemoveDataCache = true
        };

        // 批量配置 批量 AOP 
        connectionConfig.ForEach(c =>
        {
            c.ConfigureExternalServices = configureExternalServices;
            c.MoreSettings = moreSettings;
            // // 在循环中通过 ConfigId 获取作用域的 SqlSugar 实例，配置实例的 AOP 动作
            // ConfigAction(s.GetConnectionScope(c.ConfigId));
        });

        // 单例注入 SqlSugar 和 工作单元
        services.AddSingleton<ISqlSugarClient>(new SqlSugarScope(connectionConfig, ConfigAction));
        services.AddSingleton<IUnitOfWork, UnitOfWork>();
        // 作用域注入仓储
        services.AddScoped(typeof(BaseRepository<>));
    }

    /// <summary>
    /// 多库实例 AOP 委托配置
    /// </summary>
    /// <param name="db">线程安全的 SqlSugar 实例</param>
    private static void ConfigAction(ISqlSugarClient db)
    {
        // 超时时间
        db.Ado.CommandTimeOut = 30;

        #region 数据过滤器

        // 数据过滤器
        db.Aop.DataExecuting = (oldValue, entityInfo) =>
        {
            // 新增
            if (entityInfo.OperationType is DataFilterType.InsertByObject)
            {
                if (entityInfo.PropertyName is "CreatedTime" or "CreationDate" or "EditTime" or "LastUpdateDate")
                {
                    entityInfo.SetValue(DateTime.Now);
                }

                if (entityInfo.PropertyName is "CreatorId" or "CreatedBy" or "EditorId" or "LastUpdatedBy")
                {
                    var userId = App.User.FindFirstValue(nameof(LoginUserInfo.ID));
                    if (userId.IsNullOrEmpty())
                    {
                        throw Oops.Bah("登陆用户信息为空，请登陆后再操作！");
                    }
                    entityInfo.SetValue(userId);
                }
            }

            // 更新
            if (entityInfo.OperationType is DataFilterType.UpdateByObject)
            {
                if (entityInfo.PropertyName is "EditTime" or "LastUpdateDate")
                {
                    entityInfo.SetValue(DateTime.Now);
                }

                if (entityInfo.PropertyName is "EditorId" or "LastUpdatedBy")
                {
                    var userId = App.User.FindFirstValue(nameof(LoginUserInfo.ID));
                    if (userId.IsNullOrEmpty())
                    {
                        throw Oops.Bah("登陆用户信息为空，请登陆后再操作！");
                    }
                    entityInfo.SetValue(userId);
                }
            }

            // 处理日期最小值问题
            if (oldValue is DateTime { Year: < 1900 })
            {
                entityInfo.SetValue(Const.DEFAULT_MINIMUM_TIME);
            }

            //根据当前列修改另一列 可以么写
            //if(当前列逻辑==XXX)
            //var properyDate = entityInfo.EntityValue.GetType().GetProperty("Date");
            //if(properyDate!=null)
            //properyDate.SetValue(entityInfo.EntityValue,1);
        };

        #endregion

        #region 查询过滤器

        var userId = App.User?.FindFirstValue(nameof(LoginUserInfo.ID));
        if (!userId.IsNullOrEmpty())
        {
            // 获取所有实体类型
            var entityTypes = App.EffectiveTypes.Where(t => t.IsClass && t.IsPublic && !t.IsInterface
                                                            && !t.IsAbstract && (t.BaseType == typeof(BaseEntity) ||
                                                                t.BaseType == typeof(BaseEntity<>))).ToList();

            var userCompanyId = App.User.FindFirstValue(nameof(LoginUserInfo.CompanyId));
            // todo 用缓存，程序启动就缓存，精简 App.User 信息，尽量不在其中写角色和数据权限，就用 userId 配合缓存的权限，用滑动的缓存
            // todo 每次查询都会循环，需优化
            var roles = App.User.FindFirstValue(nameof(LoginUserInfo.RoleIdList)).Split(",");
            if (!roles.Contains(RoleInfo.SUPER_ADMIN_NAME))
            {
                // 循环配置查询过滤器
                foreach (Type entityType in entityTypes)
                {
                    // 获取 数据权限字段
                    var dataPermissionKey = entityType.GetFieldValue(nameof(BaseEntity.DataPermissionKey)).ObjToString();
                    if (dataPermissionKey.IsNullOrEmpty()) continue;

                    // 构建动态过滤语句
                    var lambda = DynamicExpressionParser.ParseLambda(entityType, typeof(bool), $"{dataPermissionKey} == @0",
                        userCompanyId);
                    // 网点机构过滤
                    db.QueryFilter.Add(new TableFilterItem<object>(entityType, lambda));
                }
            }
        }

        #endregion

        #region SQL 执行前修改

        // // SQL 执行前 修改SQL
        // db.Aop.OnExecutingChangeSql = (sql, pars) =>
        // {
        // if (sql.StartsWith("UPDATE", StringComparison.CurrentCultureIgnoreCase))
        // {
        //     sql = sql.Replace("0001-01-01 00:00:00", "1900-01-01 00:00:00");
        //     pars.Where(x => x.TypeName is "@EditTime" or "@LastUpdateDate")
        //         .ForEach(x =>
        //         {
        //             if (x.Value is DateTime)
        //             {
        //                 x.Value = DateTime.Now;
        //             }
        //         });
        // }
            
        // return new KeyValuePair<string, SugarParameter[]>(sql, pars);
        // };

        #endregion

        #region SQL 执行后日志

        // SQL 执行完事件
        db.Aop.OnLogExecuted = (sql, pars) =>
        {
            //执行时间超过1秒
            if (db.Ado.SqlExecutionTime.TotalSeconds > 1)
            {
                //代码CS文件名
                var fileName = db.Ado.SqlStackTrace.FirstFileName;
                //代码行数
                var fileLine = db.Ado.SqlStackTrace.FirstLine;
                //方法名
                var firstMethodName = db.Ado.SqlStackTrace.FirstMethodName;
                //db.Ado.SqlStackTrace.MyStackTraceList[1].xxx 获取上层方法的信息
                $"执行时间：{db.Ado.SqlExecutionTime.TotalSeconds}秒，执行SQL：{sql}，参数：{JSON.Serialize(pars)}，执行位置：{fileName}:{fileLine}，方法：{firstMethodName}".LogError();
            }
            //相当于EF的 PrintToMiniProfiler
#if DEBUG
            // 打印SQL
            Console.WriteLine("执行时间：" + db.Ado.SqlExecutionTime.TotalSeconds + "\r\n" + sql + "\r\n" + JSON.Serialize(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
            Console.WriteLine();
#endif
        };

        #endregion

        #region SQL 执行时日志

        // // SQL 执行前事件
        // db.Aop.OnLogExecuting = (sql, pars) =>
        // {
        //     if (sql.StartsWith("SELECT", StringComparison.CurrentCultureIgnoreCase))
        //     {
        //         if (ValidateUtil.HasInjectionData(sql))
        //         {
        //             throw Oops.Oh("SQL注入攻击：" + sql);
        //         }
        //     }
        // };

        #endregion

        #region SQL 错误日志

        // 执行 SQL 错误事件
        db.Aop.OnError = (exp) =>
        {
            string json = JSON.Serialize(exp);
            json.LogError();
#if DEBUG
            // 打印SQL
            Console.WriteLine(json);
            Console.WriteLine();
#endif
            //exp.sql exp.parameters 可以拿到参数和错误Sql             
        };

        #endregion

        #region SQL 差异日志

        // SQL 差异日志
        // db.Aop.OnDiffLogEvent = it =>
        // {
        //     //操作前记录  包含： 字段描述 列名 值 表名 表描述
        //     var editBeforeData = it.BeforeData;
        //     //操作后记录   包含： 字段描述 列名 值  表名 表描述
        //     var editAfterData = it.AfterData;
        //     var sql = it.Sql;
        //     var parameter = it.Parameters;
        //     var data = it.BusinessData;//这边会显示你传进来的对象
        //     var time = it.Time;
        //     var  diffType=it.DiffType;//enum insert 、update and delete  
        //           
        //     //Write logic
        // };
        // #if DEBUG
        //         // 开启线程安全检测
        //         db.CurrentConnectionConfig.Debugger = new SugarDebugger { EnableThreadSecurityValidation = true };
        // #endif

        #endregion
    }
}