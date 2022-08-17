using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Encrypt;
using BB.Tools.Entity;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 预付金管理 业务逻辑类
/// </summary>
public class CostBillService : BaseService<CostBill>, IDynamicApiController, ITransient
{
    public CostBillService(BaseRepository<CostBill> repository, IValidator<CostBill> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<CostBill> NewEntityAsync()
    {
        CostBill entity = await base.NewEntityAsync();
        entity.CostBillType = "1";
        entity.CostBillNo = Snowflake.Instance().GetId().ToString();
        entity.PostYN = false;
        entity.StatusID = "0";
        entity.CreationDate = DateTime.Now;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.CreatedByNode = LoginUserInfo.CompanyId;
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdatedBy = LoginUserInfo.ID.ToString();
        entity.FlagApp = false;
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
            { CostBill.FieldISID, 3 },
            { CostBill.FieldCostBillType, 1 },
            { CostBill.FieldCostBillNo, 1 },
            { CostBill.FieldCurrency, 3 },
            { CostBill.FieldPostYN, 1 },
            { CostBill.FieldPostDate, 1 },
            { CostBill.FieldPostBy, 1 },
            { CostBill.FieldStatusID, 1 },
            { CostBill.FieldCreationDate, 1 },
            { CostBill.FieldCreatedBy, 1 },
            { CostBill.FieldCreatedByNode, 1 },
            { CostBill.FieldLastUpdateDate, 1 },
            { CostBill.FieldLastUpdatedBy, 1 },
            { CostBill.FieldFlagApp, 1 },
            { CostBill.FieldAppUser, 1 },
            { CostBill.FieldAppDate, 1 },
            { CostBill.FieldAttaPath, 3 },
            { CostBill.FieldFinancialCenter, 1 },
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
    /// 构造查询语句
    /// </summary>
    /// <param name="searchInfos">查询参数</param>
    /// <returns></returns>
    public override string GetConditionSql(NameValueCollection searchInfos)
    {
        var condition = new SearchCondition();
        condition.AddCondition(CostBill.FieldCostBillType, searchInfos[CostBill.FieldCostBillType], SqlOperator.Equal);
        condition.AddCondition(CostBill.FieldCostBillNo, searchInfos[CostBill.FieldCostBillNo], SqlOperator.Like);
        condition.AddCondition(CostBill.FieldWaybillNo, searchInfos[CostBill.FieldWaybillNo], SqlOperator.Like);
        condition.AddCondition(CostBill.FieldTranNodeNO, searchInfos[CostBill.FieldTranNodeNO], SqlOperator.Equal);
        condition.AddCondition(CostBill.FieldTranNodeNOPay, searchInfos[CostBill.FieldTranNodeNOPay], SqlOperator.Equal);
        condition.AddCondition(CostBill.FieldSourceNo, searchInfos[CostBill.FieldSourceNo], SqlOperator.Like);
        condition.AddCondition(CostBill.FieldCostType, searchInfos[CostBill.FieldCostType], SqlOperator.Equal);
        condition.AddCondition(CostBill.FieldCtrl, searchInfos[CostBill.FieldCtrl], SqlOperator.Like);
        condition.AddCondition(CostBill.FieldCost, searchInfos[CostBill.FieldCost], SqlOperator.Between);
        condition.AddCondition(CostBill.FieldPostYN, searchInfos[CostBill.FieldPostYN], SqlOperator.Equal);
        condition.AddCondition(CostBill.FieldPostDate, searchInfos[CostBill.FieldPostDate], SqlOperator.Between);
        condition.AddCondition(CostBill.FieldPostBy, searchInfos[CostBill.FieldPostBy], SqlOperator.Equal);
        condition.AddCondition(CostBill.FieldStatusID, searchInfos[CostBill.FieldStatusID], SqlOperator.Equal);
        condition.AddCondition(CostBill.FieldRemark, searchInfos[CostBill.FieldRemark], SqlOperator.Like);
        condition.AddCondition(CostBill.FieldCreationDate, searchInfos[CostBill.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(CostBill.FieldCreatedBy, searchInfos[CostBill.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(CostBill.FieldCreatedByNode, searchInfos[CostBill.FieldCreatedByNode], SqlOperator.Equal);
        condition.AddCondition(CostBill.FieldLastUpdateDate, searchInfos[CostBill.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(CostBill.FieldLastUpdatedBy, searchInfos[CostBill.FieldLastUpdatedBy], SqlOperator.Equal);
        condition.AddCondition(CostBill.FieldFlagApp, searchInfos[CostBill.FieldFlagApp], SqlOperator.Equal);
        condition.AddCondition(CostBill.FieldAppUser, searchInfos[CostBill.FieldAppUser], SqlOperator.Equal);
        condition.AddCondition(CostBill.FieldAppDate, searchInfos[CostBill.FieldAppDate], SqlOperator.Between);
        condition.AddCondition(CostBill.FieldFinancialCenter, searchInfos[CostBill.FieldFinancialCenter], SqlOperator.Equal);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}