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
/// 承运商资料 业务逻辑类
/// </summary>
public class LogisticCompanyService : BaseService<LogisticCompany>, IDynamicApiController, ITransient
{
    public LogisticCompanyService(BaseRepository<LogisticCompany> repository, IValidator<LogisticCompany> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<LogisticCompany> NewEntityAsync()
    {
        LogisticCompany entity = await base.NewEntityAsync();
        entity.OrgCode = LoginUserInfo.CompanyId;
        entity.LogisticCode = Snowflake.Instance().GetId().ToString();
        entity.InUse = false;
        entity.FlagInvoice = false;
        entity.CreationDate = DateTime.Now;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdatedBy = LoginUserInfo.ID.ToString();
        entity.FlagApp = false;
        entity.CancelYN = false;
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
            { LogisticCompany.FieldOrgCode, 1 },
            // 主键不为空则只读
            { LogisticCompany.FieldLogisticCode, 1 },
            { LogisticCompany.FieldStation, 3 },
            { LogisticCompany.FieldMobile, 3 },
            { LogisticCompany.FieldFax, 3 },
            { LogisticCompany.FieldCreationDate, 1 },
            { LogisticCompany.FieldCreatedBy, 1 },
            { LogisticCompany.FieldLastUpdateDate, 1 },
            { LogisticCompany.FieldLastUpdatedBy, 1 },
            { LogisticCompany.FieldDataType, 3 },
            { LogisticCompany.FieldFlagApp, 1 },
            { LogisticCompany.FieldAppUser, 1 },
            { LogisticCompany.FieldAppDate, 1 },
            { LogisticCompany.FieldAttaPath, 3 },
            { LogisticCompany.FieldCancelYN, 1 },
            { LogisticCompany.FieldCancelDate, 1 },
            { LogisticCompany.FieldCancelUser, 1 }
        };
        // 主键不为空则只读
        permitDict.TryAdd(LogisticCompany.PrimaryKey, 4);
        // 后端权限控制
        // var permitDict = BllFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(Test1Car).FullName, LoginUserInfo.ID.ToInt32());
        return await Task.FromResult(permitDict);
    }

    /// <summary>
    /// 新增一条数据
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    public override async Task<bool> InsertAsync(LogisticCompany obj)
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
    public override async Task<bool> UpdateAsync(LogisticCompany obj)
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
        condition.AddCondition(LogisticCompany.FieldOrgCode, searchInfos[LogisticCompany.FieldOrgCode], SqlOperator.Equal);
        condition.AddCondition(LogisticCompany.FieldLogisticCode, searchInfos[LogisticCompany.FieldLogisticCode], SqlOperator.Like);
        condition.AddCondition(LogisticCompany.FieldLogisticName, searchInfos[LogisticCompany.FieldLogisticName], SqlOperator.Like);
        condition.AddCondition(LogisticCompany.FieldZJM, searchInfos[LogisticCompany.FieldZJM], SqlOperator.Like);
        condition.AddCondition(LogisticCompany.FieldContacts, searchInfos[LogisticCompany.FieldContacts], SqlOperator.Like);
        condition.AddCondition(LogisticCompany.FieldTel, searchInfos[LogisticCompany.FieldTel], SqlOperator.Like);
        condition.AddCondition(LogisticCompany.FieldAddress, searchInfos[LogisticCompany.FieldAddress], SqlOperator.Like);
        condition.AddCondition(LogisticCompany.FieldMainLine, searchInfos[LogisticCompany.FieldMainLine], SqlOperator.Like);
        condition.AddCondition(LogisticCompany.FieldTrustLevel, searchInfos[LogisticCompany.FieldTrustLevel], SqlOperator.Equal);
        condition.AddCondition(LogisticCompany.FieldLegal, searchInfos[LogisticCompany.FieldLegal], SqlOperator.Like);
        condition.AddCondition(LogisticCompany.FieldTax, searchInfos[LogisticCompany.FieldTax], SqlOperator.Like);
        condition.AddCondition(LogisticCompany.FieldBank, searchInfos[LogisticCompany.FieldBank], SqlOperator.Like);
        condition.AddCondition(LogisticCompany.FieldBankAccount, searchInfos[LogisticCompany.FieldBankAccount], SqlOperator.Like);
        condition.AddCondition(LogisticCompany.FieldPaymentTerm, searchInfos[LogisticCompany.FieldPaymentTerm], SqlOperator.Like);
        condition.AddCondition(LogisticCompany.FieldContractDate1, searchInfos[LogisticCompany.FieldContractDate1], SqlOperator.Between);
        condition.AddCondition(LogisticCompany.FieldContractDate2, searchInfos[LogisticCompany.FieldContractDate2], SqlOperator.Between);
        condition.AddCondition(LogisticCompany.FieldInUse, searchInfos[LogisticCompany.FieldInUse], SqlOperator.Equal);
        condition.AddCondition(LogisticCompany.FieldFlagInvoice, searchInfos[LogisticCompany.FieldFlagInvoice], SqlOperator.Equal);
        condition.AddCondition(LogisticCompany.FieldRemark, searchInfos[LogisticCompany.FieldRemark], SqlOperator.Like);
        condition.AddCondition(LogisticCompany.FieldCreationDate, searchInfos[LogisticCompany.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(LogisticCompany.FieldCreatedBy, searchInfos[LogisticCompany.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(LogisticCompany.FieldLastUpdateDate, searchInfos[LogisticCompany.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(LogisticCompany.FieldLastUpdatedBy, searchInfos[LogisticCompany.FieldLastUpdatedBy], SqlOperator.Equal);
        condition.AddCondition(LogisticCompany.FieldFlagApp, searchInfos[LogisticCompany.FieldFlagApp], SqlOperator.Equal);
        condition.AddCondition(LogisticCompany.FieldAppUser, searchInfos[LogisticCompany.FieldAppUser], SqlOperator.Equal);
        condition.AddCondition(LogisticCompany.FieldAppDate, searchInfos[LogisticCompany.FieldAppDate], SqlOperator.Between);
        condition.AddCondition(LogisticCompany.FieldCancelYN, searchInfos[LogisticCompany.FieldCancelYN], SqlOperator.Equal);
        condition.AddCondition(LogisticCompany.FieldCancelDate, searchInfos[LogisticCompany.FieldCancelDate], SqlOperator.Between);
        condition.AddCondition(LogisticCompany.FieldCancelUser, searchInfos[LogisticCompany.FieldCancelUser], SqlOperator.Like);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}