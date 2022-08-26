using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Utils;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 费用类型 业务逻辑类
/// </summary>
public class BasicCostTypeService : BaseService<BasicCostType>, IDynamicApiController, ITransient
{
    public BasicCostTypeService(BaseRepository<BasicCostType> repository, IValidator<BasicCostType> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<BasicCostType> NewEntityAsync()
    {
        BasicCostType entity = await base.NewEntityAsync();
        entity.UseYN = false;
        entity.CostYN = false;
        entity.FlagApp = false;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.CreationDate = DateTime.Now;
        entity.LastUpdatedBy = LoginUserInfo.ID.ToString();
        entity.LastUpdateDate = DateTime.Now;
        return entity;
    }

    /// <summary>
    /// 获取字段显示权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    /// <returns></returns>
    public override async Task<Dictionary<string, int>> GetPermitDictAsync()
    {
        var permitDict = new Dictionary<string, int>
        {
            // 主键不为空则只读
            { BasicCostType.FieldCostType, 4 },
            { BasicCostType.FieldCostTypeDesc, 4 },
            { BasicCostType.FieldFlagApp, 1 },
            { BasicCostType.FieldAppUser, 1 },
            { BasicCostType.FieldAppDate, 1 },
            { BasicCostType.FieldCreatedBy, 1 },
            { BasicCostType.FieldCreationDate, 1 },
            { BasicCostType.FieldLastUpdatedBy, 1 },
            { BasicCostType.FieldLastUpdateDate, 1 }
        };
        // 主键不为空则只读
        permitDict.TryAdd(BasicCostType.PrimaryKey, 4);
        // 后端权限控制
        // var permitDict = BllFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(Test1Car).FullName, LoginUserInfo.ID.ToInt32());
        return await Task.FromResult(permitDict);
    }

    /// <summary>
    /// 新增一条数据
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    public override async Task<bool> InsertAsync(BasicCostType obj)
    {
        #region 新增前

        #endregion

        bool succeed = await base.InsertAsync(obj);

        #region 新增后

        if (!succeed) return false;

        //可添加其他关联操作

        return true;

        #endregion
    }

    /// <summary>
    /// 更新一条数据
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public override async Task<bool> UpdateAsync(BasicCostType obj)
    {
        #region 修改前

        #endregion

        bool succeed = await base.UpdateAsync(obj);

        #region 修改后

        if (!succeed) return false;

        //可添加其他关联操作

        return true;

        #endregion
    }

    /// <summary>
    /// 删除一条数据
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public override async Task<bool> DeleteAsync(object key)
    {
        #region 删除前

        #endregion

        bool succeed = await base.DeleteAsync(key);
        if (!succeed) return false;

        #region 删除后

        //可添加其他关联操作

        #endregion

        return true;
    }

    /// <summary>
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.Instance.GetOrCreate($"{nameof(BasicCostType)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(BasicCostType.FieldCostType, SqlOperator.Equal),
                new(BasicCostType.FieldCostTypeDesc, SqlOperator.Equal),
                new(BasicCostType.FieldCtrl, SqlOperator.Between),
                new(BasicCostType.FieldUseYN, SqlOperator.Equal),
                new(BasicCostType.FieldUseType, SqlOperator.Equal),
                new(BasicCostType.FieldPayNodeType, SqlOperator.Equal),
                new(BasicCostType.FieldRecvNodeType, SqlOperator.Equal),
                new(BasicCostType.FieldPayPostType, SqlOperator.Equal),
                new(BasicCostType.FieldRecvPostType, SqlOperator.Equal),
                new(BasicCostType.FieldCostYN, SqlOperator.Equal),
                new(BasicCostType.FieldRemark, SqlOperator.Like),
                new(BasicCostType.FieldFlagApp, SqlOperator.Equal),
                new(BasicCostType.FieldAppUser, SqlOperator.Equal),
                new(BasicCostType.FieldAppDate, SqlOperator.Between),
                new(BasicCostType.FieldCreatedBy, SqlOperator.Equal),
                new(BasicCostType.FieldCreationDate, SqlOperator.Between),
                new(BasicCostType.FieldLastUpdatedBy, SqlOperator.Equal),
                new(BasicCostType.FieldLastUpdateDate, SqlOperator.Between)
            });
    }
}