using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Encrypt;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Utils;
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
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.Instance.GetOrCreate($"{nameof(LogisticCompany)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(LogisticCompany.FieldOrgCode, SqlOperator.Equal),
                new(LogisticCompany.FieldLogisticCode, SqlOperator.Like),
                new(LogisticCompany.FieldLogisticName, SqlOperator.Like),
                new(LogisticCompany.FieldZJM, SqlOperator.Like),
                new(LogisticCompany.FieldContacts, SqlOperator.Like),
                new(LogisticCompany.FieldTel, SqlOperator.Like),
                new(LogisticCompany.FieldAddress, SqlOperator.Like),
                new(LogisticCompany.FieldMainLine, SqlOperator.Like),
                new(LogisticCompany.FieldTrustLevel, SqlOperator.Equal),
                new(LogisticCompany.FieldLegal, SqlOperator.Like),
                new(LogisticCompany.FieldTax, SqlOperator.Like),
                new(LogisticCompany.FieldBank, SqlOperator.Like),
                new(LogisticCompany.FieldBankAccount, SqlOperator.Like),
                new(LogisticCompany.FieldPaymentTerm, SqlOperator.Like),
                new(LogisticCompany.FieldContractDate1, SqlOperator.Between),
                new(LogisticCompany.FieldContractDate2, SqlOperator.Between),
                new(LogisticCompany.FieldInUse, SqlOperator.Equal),
                new(LogisticCompany.FieldFlagInvoice, SqlOperator.Equal),
                new(LogisticCompany.FieldRemark, SqlOperator.Like),
                new(LogisticCompany.FieldCreationDate, SqlOperator.Between),
                new(LogisticCompany.FieldCreatedBy, SqlOperator.Equal),
                new(LogisticCompany.FieldLastUpdateDate, SqlOperator.Between),
                new(LogisticCompany.FieldLastUpdatedBy, SqlOperator.Equal),
                new(LogisticCompany.FieldFlagApp, SqlOperator.Equal),
                new(LogisticCompany.FieldAppUser, SqlOperator.Equal),
                new(LogisticCompany.FieldAppDate, SqlOperator.Between),
                new(LogisticCompany.FieldCancelYN, SqlOperator.Equal),
                new(LogisticCompany.FieldCancelDate, SqlOperator.Between),
                new(LogisticCompany.FieldCancelUser, SqlOperator.Like)
            });
    }
}