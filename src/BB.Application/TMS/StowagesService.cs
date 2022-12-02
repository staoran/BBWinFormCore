using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Cache;
using BB.Tools.Entity;
using BB.Tools.Extension;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 配载明细表 业务逻辑类
/// </summary>
[ApiDescriptionSettings("运输与配载")]
public class StowagesService : BaseService<Stowages>, IDynamicApiController, ITransient
{
    public StowagesService(BaseRepository<Stowages> repository, IValidator<Stowages> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<Stowages> SetDynamicDefaults(Stowages entity)
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
            { Stowages.FieldISID, 3 },
            { Stowages.FieldStowageNo, 1 },
            { Stowages.FieldInputType, 1 },
            { Stowages.FieldWaybillNo, 1 },
            { Stowages.FieldFromNode, 1 },
            { Stowages.FieldFromNodes, 1 },
            { Stowages.FieldToNode, 1 },
            { Stowages.FieldToNodes, 1 },
            { Stowages.FieldConsigneeCompanyName, 1 },
            { Stowages.FieldConsigneeAddress, 1 },
            { Stowages.FieldConsigneeTel, 1 },
            { Stowages.FieldConsignee, 1 },
            { Stowages.FieldDeliveryType, 1 },
            { Stowages.FieldPaymentType, 1 },
            { Stowages.FieldAckRecQty, 1 },
            { Stowages.FieldAckRecType, 1 },
            { Stowages.FieldAckRecNo, 1 },
            { Stowages.FieldCreationDate, 1 },
            { Stowages.FieldCreatedBy, 1 },
            { Stowages.FieldLastUpdateDate, 1 },
            { Stowages.FieldLastUpdatedBy, 1 },
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
    public override async Task<bool> InsertAsync(Stowages obj)
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
    public override async Task<bool> UpdateAsync(Stowages obj)
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
        return Cache.GetOrAdd($"{nameof(Stowages)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(Stowages.FieldISID, SqlOperator.Like),
                new(Stowages.FieldStowageNo, SqlOperator.Like),
                new(Stowages.FieldInputType, SqlOperator.Equal),
                new(Stowages.FieldWaybillNo, SqlOperator.Like),
                new(Stowages.FieldFromNode, SqlOperator.Like),
                new(Stowages.FieldFromNodes, SqlOperator.Like),
                new(Stowages.FieldToNode, SqlOperator.Like),
                new(Stowages.FieldToNodes, SqlOperator.Like),
                new(Stowages.FieldConsigneeCompanyName, SqlOperator.Like),
                new(Stowages.FieldConsigneeAddress, SqlOperator.Like),
                new(Stowages.FieldConsigneeTel, SqlOperator.Like),
                new(Stowages.FieldConsignee, SqlOperator.Like),
                new(Stowages.FieldDeliveryType, SqlOperator.Equal),
                new(Stowages.FieldPaymentType, SqlOperator.Equal),
                new(Stowages.FieldAckRecQty, SqlOperator.Like),
                new(Stowages.FieldAckRecType, SqlOperator.Like),
                new(Stowages.FieldAckRecNo, SqlOperator.Like),
                new(Stowages.FieldQty, SqlOperator.Like),
                new(Stowages.FieldWeight, SqlOperator.Between),
                new(Stowages.FieldCubage, SqlOperator.Between),
                new(Stowages.FieldUnloadYN, SqlOperator.Equal),
                new(Stowages.FieldUpstairYN, SqlOperator.Equal),
                new(Stowages.FieldUpstairNum, SqlOperator.Like),
                new(Stowages.FieldSmsYN, SqlOperator.Equal),
                new(Stowages.FieldStowageCarriage, SqlOperator.Between),
                new(Stowages.FieldStatusID, SqlOperator.Like),
                new(Stowages.FieldRemark, SqlOperator.Like),
                new(Stowages.FieldCreationDate, SqlOperator.Between),
                new(Stowages.FieldCreatedBy, SqlOperator.Equal),
                new(Stowages.FieldLastUpdateDate, SqlOperator.Between),
                new(Stowages.FieldLastUpdatedBy, SqlOperator.Equal),
            });
    }
}