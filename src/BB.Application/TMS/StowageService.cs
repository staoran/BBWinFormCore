using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Cache;
using BB.Tools.Entity;
using BB.Tools.Extension;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 送货配载 业务逻辑类
/// </summary>
[ApiDescriptionSettings("运输与配载")]
public class StowageService : BaseMultiService<Stowage, Stowages>, IDynamicApiController, ITransient
{
    public StowageService(BaseRepository<Stowage> repository, BaseRepository<Stowages> childRepository, IValidator<Stowage> validator, IValidator<Stowages> childValidator)
        : base(repository, childRepository, validator, childValidator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<Stowage> SetDynamicDefaults(Stowage entity)
    {
        return Task.FromResult(entity);
    }

    /// <summary>
    /// 获取字段显示权限，0可读写 1只读 2隐藏值 3不显示 4 可新增不可编辑
    /// </summary>
    /// <returns></returns>
    public override async Task<Dictionary<string, int>> GetPermitDictAsync()
    {
        var permitDict = new Dictionary<string, int>
        {
            { Stowage.FieldStowageNo, 1 },
            { Stowage.FieldTranNodeNO, 1 },
            { Stowage.FieldTotalQty, 1 },
            { Stowage.FieldTotalWeight, 1 },
            { Stowage.FieldTotalCubage, 1 },
            { Stowage.FieldCheckInYN, 1 },
            { Stowage.FieldCheckInAccount, 1 },
            { Stowage.FieldCheckInDate, 1 },
            { Stowage.FieldCreationDate, 1 },
            { Stowage.FieldCreatedBy, 1 },
            { Stowage.FieldLastUpdateDate, 1 },
            { Stowage.FieldLastUpdatedBy, 1 },
            { Stowage.FieldFlagApp, 1 },
            { Stowage.FieldAppUser, 1 },
            { Stowage.FieldAppDate, 1 },
        };
        // 后端权限控制
        // var permitDict = BllFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(Test1Car).FullName, LoginUserInfo.ID.ToInt32());
        return await Task.FromResult(permitDict);
    }

    /// <summary>
    /// 新增一条数据
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    public override async Task<bool> InsertAsync(Stowage obj)
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
    public override async Task<bool> UpdateAsync(Stowage obj)
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

        #region 删除后

        if (!succeed) return false;

        //可添加其他关联操作

        return true;

        #endregion
    }

    /// <summary>
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.Instance.GetOrCreate($"{nameof(Stowage)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(Stowage.FieldStowageNo, SqlOperator.Like),
                new(Stowage.FieldTranNodeNO, SqlOperator.Equal),
                new(Stowage.FieldStowageType, SqlOperator.Equal),
                new(Stowage.FieldTransType, SqlOperator.Equal),
                new(Stowage.FieldTransMarkNo, SqlOperator.Like),
                new(Stowage.FieldTransNo, SqlOperator.Like),
                new(Stowage.FieldTransDriver, SqlOperator.Like),
                new(Stowage.FieldTransDriverPhone, SqlOperator.Like),
                new(Stowage.FieldTransDate, SqlOperator.Between),
                new(Stowage.FieldTotalQty, SqlOperator.Between),
                new(Stowage.FieldTotalWeight, SqlOperator.Between),
                new(Stowage.FieldTotalCubage, SqlOperator.Between),
                new(Stowage.FieldTotalCarriage, SqlOperator.Between),
                new(Stowage.FieldTransCarriage, SqlOperator.Between),
                new(Stowage.FieldCheckInYN, SqlOperator.Equal),
                new(Stowage.FieldCheckInAccount, SqlOperator.Equal),
                new(Stowage.FieldCheckInDate, SqlOperator.Between),
                new(Stowage.FieldRemark, SqlOperator.Like),
                new(Stowage.FieldCreationDate, SqlOperator.Between),
                new(Stowage.FieldCreatedBy, SqlOperator.Equal),
                new(Stowage.FieldLastUpdateDate, SqlOperator.Between),
                new(Stowage.FieldLastUpdatedBy, SqlOperator.Equal),
                new(Stowage.FieldFlagApp, SqlOperator.Equal),
                new(Stowage.FieldAppUser, SqlOperator.Equal),
                new(Stowage.FieldAppDate, SqlOperator.Between),
                new(Stowage.FieldIncome, SqlOperator.Between),
                new(Stowage.FieldSharType, SqlOperator.Equal),
            });
    }
}