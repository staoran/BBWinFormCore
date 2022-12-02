using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Cache;
using BB.Tools.Entity;
using BB.Tools.Extension;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 预付金管理 业务逻辑类
/// </summary>
[ApiDescriptionSettings("费用与账单")]
public class CostBillService : BaseService<CostBill>, IDynamicApiController, ITransient
{
    public CostBillService(BaseRepository<CostBill> repository, IValidator<CostBill> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override Task<CostBill> SetDynamicDefaults(CostBill entity)
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
            { nameof(CostBill.ISID), 3 },
            { nameof(CostBill.CostBillType), 1 },
            { nameof(CostBill.CostBillNo), 1 },
            { nameof(CostBill.Currency), 3 },
            { nameof(CostBill.PostYN), 1 },
            { nameof(CostBill.PostDate), 1 },
            { nameof(CostBill.PostBy), 1 },
            { nameof(CostBill.StatusID), 1 },
            { nameof(CostBill.CreationDate), 1 },
            { nameof(CostBill.CreatedBy), 1 },
            { nameof(CostBill.CreatedByNode), 1 },
            { nameof(CostBill.LastUpdateDate), 1 },
            { nameof(CostBill.LastUpdatedBy), 1 },
            { nameof(CostBill.FlagApp), 1 },
            { nameof(CostBill.AppUser), 1 },
            { nameof(CostBill.AppDate), 1 },
            { nameof(CostBill.AttaPath), 3 },
            { nameof(CostBill.FinancialCenter), 1 },
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
    public override async Task<bool> InsertAsync(CostBill obj)
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
    public override async Task<bool> UpdateAsync(CostBill obj)
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
        return Cache.GetOrAdd($"{nameof(CostBill)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(CostBill.FieldCostBillType, SqlOperator.Equal),
                new(CostBill.FieldCostBillNo, SqlOperator.Like),
                new(CostBill.FieldWaybillNo, SqlOperator.Like),
                new(CostBill.FieldTranNodeNO, SqlOperator.Equal),
                new(CostBill.FieldTranNodeNOPay, SqlOperator.Equal),
                new(CostBill.FieldSourceNo, SqlOperator.Like),
                new(CostBill.FieldCostType, SqlOperator.Equal),
                new(CostBill.FieldCtrl, SqlOperator.Like),
                new(CostBill.FieldCost, SqlOperator.Between),
                new(CostBill.FieldPostYN, SqlOperator.Equal),
                new(CostBill.FieldPostDate, SqlOperator.Between),
                new(CostBill.FieldPostBy, SqlOperator.Equal),
                new(CostBill.FieldStatusID, SqlOperator.Equal),
                new(CostBill.FieldRemark, SqlOperator.Like),
                new(CostBill.FieldCreationDate, SqlOperator.Between),
                new(CostBill.FieldCreatedBy, SqlOperator.Equal),
                new(CostBill.FieldCreatedByNode, SqlOperator.Equal),
                new(CostBill.FieldLastUpdateDate, SqlOperator.Between),
                new(CostBill.FieldLastUpdatedBy, SqlOperator.Equal),
                new(CostBill.FieldFlagApp, SqlOperator.Equal),
                new(CostBill.FieldAppUser, SqlOperator.Equal),
                new(CostBill.FieldAppDate, SqlOperator.Between),
                new(CostBill.FieldFinancialCenter, SqlOperator.Equal)
            });
    }
}