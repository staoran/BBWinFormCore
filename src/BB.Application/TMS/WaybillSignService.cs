using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 签收表 业务逻辑类
/// </summary>
[ApiDescriptionSettings("初始区域")]
public class WaybillSignService : BaseService<WaybillSign>, IDynamicApiController, ITransient
{
    public WaybillSignService(BaseRepository<WaybillSign> repository, IValidator<WaybillSign> validator)
        : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<WaybillSign> SetDynamicDefaults(WaybillSign entity)
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
            { nameof(WaybillSign.ISID), 3 },
            { nameof(WaybillSign.TranNode), 1 },
            { nameof(WaybillSign.RelatedUser), 3 },
            { nameof(WaybillSign.SignStatus), 3 },
            { nameof(WaybillSign.SignStatusRemark), 3 },
            { nameof(WaybillSign.CreationDate), 1 },
            { nameof(WaybillSign.CreatedBy), 1 },
            { nameof(WaybillSign.LastUpdateDate), 1 },
            { nameof(WaybillSign.LastUpdatedBy), 1 },
            { nameof(WaybillSign.FlagApp), 1 },
            { nameof(WaybillSign.AppUser), 1 },
            { nameof(WaybillSign.AppDate), 1 },
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
    public override async Task<bool> InsertAsync(WaybillSign obj)
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
    public override async Task<bool> UpdateAsync(WaybillSign obj)
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
        return Cache.GetOrAdd($"{nameof(WaybillSign)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(WaybillSign.FieldWaybillNo, SqlOperator.Like),
                new(WaybillSign.FieldTranNode, SqlOperator.Equal),
                new(WaybillSign.FieldTranNodes, SqlOperator.Like),
                new(WaybillSign.FieldDeliveryType, SqlOperator.Equal),
                new(WaybillSign.FieldConsignee, SqlOperator.Like),
                new(WaybillSign.FieldConsigneeid, SqlOperator.Like),
                new(WaybillSign.FieldConsigneeidPicAdds, SqlOperator.Like),
                new(WaybillSign.FieldConsigneeRemark, SqlOperator.Like),
                new(WaybillSign.FieldQty, SqlOperator.Like),
                new(WaybillSign.FieldSignQty, SqlOperator.Like),
                new(WaybillSign.FieldAckRecYN, SqlOperator.Equal),
                new(WaybillSign.FieldAckRecNo, SqlOperator.Like),
                new(WaybillSign.FieldAckRecQty, SqlOperator.Like),
                new(WaybillSign.FieldAckRecRemark, SqlOperator.Like),
                new(WaybillSign.FieldCreationDate, SqlOperator.Between),
                new(WaybillSign.FieldCreatedBy, SqlOperator.Equal),
                new(WaybillSign.FieldLastUpdateDate, SqlOperator.Between),
                new(WaybillSign.FieldLastUpdatedBy, SqlOperator.Equal),
                new(WaybillSign.FieldFlagApp, SqlOperator.Equal),
                new(WaybillSign.FieldAppUser, SqlOperator.Equal),
                new(WaybillSign.FieldAppDate, SqlOperator.Between),
            });
    }
}