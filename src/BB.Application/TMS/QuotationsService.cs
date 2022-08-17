using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 普通报价明细 业务逻辑类
/// </summary>
public class QuotationsService : BaseService<Quotations>, IDynamicApiController, ITransient
{
    public QuotationsService(BaseRepository<Quotations> repository, IValidator<Quotations> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<Quotations> NewEntityAsync()
    {
        Quotations entity = await base.NewEntityAsync();
        entity.MinCost = 0;
        entity.MaxCost = 999999;
        entity.FirstCost = 0;
        entity.FirstValue = 0;
        entity.MinValue = 0;
        entity.MaxValue = 99999;
        entity.UnitPrice = 0;
        entity.UnitPricePer = 1;
        entity.CreationDate = DateTime.Now;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdatedBy = LoginUserInfo.ID.ToString();
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
            { Quotations.FieldISID, 3 },
            { Quotations.FieldQuotationNo, 3 },
            { Quotations.FieldMathConditional, 3 },
            { Quotations.FieldMathContent, 3 },
            { Quotations.FieldCreationDate, 3 },
            { Quotations.FieldCreatedBy, 3 },
            { Quotations.FieldLastUpdateDate, 3 },
            { Quotations.FieldLastUpdatedBy, 3 },
            { Quotations.FieldFromGroupsID, 3 },
            { Quotations.FieldToGroupsID, 3 },
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
    public override async Task<bool> InsertAsync(Quotations obj)
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
    public override async Task<bool> UpdateAsync(Quotations obj)
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
        condition.AddCondition(Quotations.FieldQuotationType, searchInfos[Quotations.FieldQuotationType], SqlOperator.Equal);
        condition.AddCondition(Quotations.FieldFromGroups, searchInfos[Quotations.FieldFromGroups], SqlOperator.Like);
        condition.AddCondition(Quotations.FieldToGroups, searchInfos[Quotations.FieldToGroups], SqlOperator.Like);
        condition.AddCondition(Quotations.FieldMinCost, searchInfos[Quotations.FieldMinCost], SqlOperator.Between);
        condition.AddCondition(Quotations.FieldMaxCost, searchInfos[Quotations.FieldMaxCost], SqlOperator.Between);
        condition.AddCondition(Quotations.FieldFirstCost, searchInfos[Quotations.FieldFirstCost], SqlOperator.Between);
        condition.AddCondition(Quotations.FieldFirstValue, searchInfos[Quotations.FieldFirstValue], SqlOperator.Between);
        condition.AddCondition(Quotations.FieldMinValue, searchInfos[Quotations.FieldMinValue], SqlOperator.Between);
        condition.AddCondition(Quotations.FieldMaxValue, searchInfos[Quotations.FieldMaxValue], SqlOperator.Between);
        condition.AddCondition(Quotations.FieldUnitPrice, searchInfos[Quotations.FieldUnitPrice], SqlOperator.Between);
        condition.AddCondition(Quotations.FieldUnitPricePer, searchInfos[Quotations.FieldUnitPricePer], SqlOperator.Between);
        condition.AddCondition(Quotations.FieldMathConditional, searchInfos[Quotations.FieldMathConditional], SqlOperator.Like);
        condition.AddCondition(Quotations.FieldMathContent, searchInfos[Quotations.FieldMathContent], SqlOperator.Like);
        condition.AddCondition(Quotations.FieldRemark, searchInfos[Quotations.FieldRemark], SqlOperator.Like);
        condition.AddCondition(Quotations.FieldCreationDate, searchInfos[Quotations.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(Quotations.FieldCreatedBy, searchInfos[Quotations.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(Quotations.FieldLastUpdateDate, searchInfos[Quotations.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(Quotations.FieldLastUpdatedBy, searchInfos[Quotations.FieldLastUpdatedBy], SqlOperator.Equal);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}