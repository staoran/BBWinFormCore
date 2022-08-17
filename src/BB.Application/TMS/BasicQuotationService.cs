using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 公式报价 业务逻辑类
/// </summary>
public class BasicQuotationService : BaseMultiService<BasicQuotation, BasicQuotations>, IDynamicApiController, ITransient
{
    public BasicQuotationService(BaseRepository<BasicQuotation> repository, BaseRepository<BasicQuotations> childRepository, IValidator<BasicQuotation> validator, IValidator<BasicQuotations> childValidator)
        : base(repository, childRepository, validator, childValidator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<BasicQuotation> NewEntityAsync()
    {
        BasicQuotation entity = await base.NewEntityAsync();
        entity.TranNodeNO = LoginUserInfo.CompanyId;
        entity.CreationDate = DateTime.Now;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdatedBy = LoginUserInfo.ID.ToString();
        entity.FlagApp = false;
        entity.RakeMarkYN = false;
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
            { BasicQuotation.FieldISID, 3 },
            // 主键不为空则只读
            { BasicQuotation.FieldQuotationNo, 4 },
            { BasicQuotation.FieldTranNodeNO, 1 },
            { BasicQuotation.FieldCreationDate, 1 },
            { BasicQuotation.FieldCreatedBy, 1 },
            { BasicQuotation.FieldLastUpdateDate, 1 },
            { BasicQuotation.FieldLastUpdatedBy, 1 },
            { BasicQuotation.FieldFlagApp, 1 },
            { BasicQuotation.FieldAppUser, 1 },
            { BasicQuotation.FieldAppDate, 1 },
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
    public override async Task<bool> InsertAsync(BasicQuotation obj)
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
    public override async Task<bool> UpdateAsync(BasicQuotation obj)
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
    /// 构造查询语句
    /// </summary>
    /// <param name="searchInfos">查询参数</param>
    /// <returns></returns>
    public override string GetConditionSql(NameValueCollection searchInfos)
    {
        var condition = new SearchCondition();
        condition.AddCondition(BasicQuotation.FieldQuotationNo, searchInfos[BasicQuotation.FieldQuotationNo], SqlOperator.Like);
        condition.AddCondition(BasicQuotation.FieldQuotationDesc, searchInfos[BasicQuotation.FieldQuotationDesc], SqlOperator.Like);
        condition.AddCondition(BasicQuotation.FieldTranNodeNO, searchInfos[BasicQuotation.FieldTranNodeNO], SqlOperator.Equal);
        condition.AddCondition(BasicQuotation.FieldCostType, searchInfos[BasicQuotation.FieldCostType], SqlOperator.Equal);
        condition.AddCondition(BasicQuotation.FieldCargoType, searchInfos[BasicQuotation.FieldCargoType], SqlOperator.Equal);
        condition.AddCondition(BasicQuotation.FieldPickUpType, searchInfos[BasicQuotation.FieldPickUpType], SqlOperator.Equal);
        condition.AddCondition(BasicQuotation.FieldDeliveryType, searchInfos[BasicQuotation.FieldDeliveryType], SqlOperator.Equal);
        condition.AddCondition(BasicQuotation.FieldTransportType, searchInfos[BasicQuotation.FieldTransportType], SqlOperator.Equal);
        condition.AddCondition(BasicQuotation.FieldFroms, searchInfos[BasicQuotation.FieldFroms], SqlOperator.Like);
        condition.AddCondition(BasicQuotation.FieldTos, searchInfos[BasicQuotation.FieldTos], SqlOperator.Like);
        condition.AddCondition(BasicQuotation.FieldBeginTime, searchInfos[BasicQuotation.FieldBeginTime], SqlOperator.Between);
        condition.AddCondition(BasicQuotation.FieldEndTime, searchInfos[BasicQuotation.FieldEndTime], SqlOperator.Between);
        condition.AddCondition(BasicQuotation.FieldRemark, searchInfos[BasicQuotation.FieldRemark], SqlOperator.Like);
        condition.AddCondition(BasicQuotation.FieldCreationDate, searchInfos[BasicQuotation.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(BasicQuotation.FieldCreatedBy, searchInfos[BasicQuotation.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(BasicQuotation.FieldLastUpdateDate, searchInfos[BasicQuotation.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(BasicQuotation.FieldLastUpdatedBy, searchInfos[BasicQuotation.FieldLastUpdatedBy], SqlOperator.Equal);
        condition.AddCondition(BasicQuotation.FieldFlagApp, searchInfos[BasicQuotation.FieldFlagApp], SqlOperator.Equal);
        condition.AddCondition(BasicQuotation.FieldAppUser, searchInfos[BasicQuotation.FieldAppUser], SqlOperator.Equal);
        condition.AddCondition(BasicQuotation.FieldAppDate, searchInfos[BasicQuotation.FieldAppDate], SqlOperator.Between);
        condition.AddCondition(BasicQuotation.FieldRakeMarkYN, searchInfos[BasicQuotation.FieldRakeMarkYN], SqlOperator.Equal);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}