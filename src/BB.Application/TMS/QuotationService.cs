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
/// 普通报价 业务逻辑类
/// </summary>
public class QuotationService : BaseMultiService<Quotation, Quotations>, IDynamicApiController, ITransient
{
    public QuotationService(BaseRepository<Quotation> repository, BaseRepository<Quotations> childRepository, IValidator<Quotation> validator, IValidator<Quotations> childValidator)
        : base(repository, childRepository, validator, childValidator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<Quotation> NewEntityAsync()
    {
        Quotation entity = await base.NewEntityAsync();
        entity.QuotationNo = Snowflake.Instance().GetId().ToString();
        entity.CargoTypePerYN = false;
        entity.CreationDate = DateTime.Now;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdatedBy = LoginUserInfo.ID.ToString();
        entity.FlagApp = false;
        entity.TranNodeNO = LoginUserInfo.CompanyId;
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
            { Quotation.FieldISID, 3 },
            { Quotation.FieldQuotationNo, 1 },
            { Quotation.FieldCreationDate, 1 },
            { Quotation.FieldCreatedBy, 1 },
            { Quotation.FieldLastUpdateDate, 1 },
            { Quotation.FieldLastUpdatedBy, 1 },
            { Quotation.FieldFlagApp, 1 },
            { Quotation.FieldAppUser, 1 },
            { Quotation.FieldAppDate, 1 },
            { Quotation.FieldTranNodeNO, 1 },
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
    public override async Task<bool> InsertAsync(Quotation obj)
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
    public override async Task<bool> UpdateAsync(Quotation obj)
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
    /// 构造查询语句
    /// </summary>
    /// <param name="searchInfos">查询参数</param>
    /// <returns></returns>
    public override string GetConditionSql(NameValueCollection searchInfos)
    {
        var condition = new SearchCondition();
        condition.AddCondition(Quotation.FieldQuotationNo, searchInfos[Quotation.FieldQuotationNo], SqlOperator.Like);
        condition.AddCondition(Quotation.FieldQuotationDesc, searchInfos[Quotation.FieldQuotationDesc], SqlOperator.Like);
        condition.AddCondition(Quotation.FieldCostType, searchInfos[Quotation.FieldCostType], SqlOperator.Equal);
        condition.AddCondition(Quotation.FieldCargoTypePerYN, searchInfos[Quotation.FieldCargoTypePerYN], SqlOperator.Equal);
        condition.AddCondition(Quotation.FieldRemark, searchInfos[Quotation.FieldRemark], SqlOperator.Like);
        condition.AddCondition(Quotation.FieldCreationDate, searchInfos[Quotation.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(Quotation.FieldCreatedBy, searchInfos[Quotation.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(Quotation.FieldLastUpdateDate, searchInfos[Quotation.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(Quotation.FieldLastUpdatedBy, searchInfos[Quotation.FieldLastUpdatedBy], SqlOperator.Equal);
        condition.AddCondition(Quotation.FieldFlagApp, searchInfos[Quotation.FieldFlagApp], SqlOperator.Equal);
        condition.AddCondition(Quotation.FieldAppUser, searchInfos[Quotation.FieldAppUser], SqlOperator.Equal);
        condition.AddCondition(Quotation.FieldAppDate, searchInfos[Quotation.FieldAppDate], SqlOperator.Between);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}