using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Encrypt;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 客户管理 业务逻辑类
/// </summary>
public class CustomerService : BaseMultiService<Customer, Customers>, IDynamicApiController, ITransient
{
    public CustomerService(BaseRepository<Customer> repository, BaseRepository<Customers> childRepository, IValidator<Customer> validator, IValidator<Customers> childValidator)
        : base(repository, childRepository, validator, childValidator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<Customer> NewEntityAsync()
    {
        Customer entity = await base.NewEntityAsync();
        entity.CustomerCode = Snowflake.Instance().GetId().ToString();
        entity.TranNode = LoginUserInfo.CompanyId;
        entity.InUse = true;
        entity.FlagInvoice = false;
        entity.CreationDate = DateTime.Now;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
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
            // 主键不为空则只读
            { Customer.FieldCustomerCode, 1 },
            { Customer.FieldTranNode, 1 },
            { Customer.FieldProvinceNo, 3 },
            { Customer.FieldCityNo, 3 },
            { Customer.FieldCreationDate, 1 },
            { Customer.FieldCreatedBy, 1 },
            { Customer.FieldLastUpdateDate, 1 },
            { Customer.FieldLastUpdatedBy, 1 },
            { Customer.FieldFlagApp, 1 },
            { Customer.FieldAppUser, 1 },
            { Customer.FieldAppDate, 1 }
        };
        // 主键不为空则只读
        permitDict.TryAdd(Customer.PrimaryKey, 4);
        // 后端权限控制
        // var permitDict = BllFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(Test1Car).FullName, LoginUserInfo.ID.ToInt32());
        return await Task.FromResult(permitDict);
    }

    /// <summary>
    /// 新增一条数据
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行操作是否成功。</returns>
    public override async Task<bool> InsertAsync(Customer obj)
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
    public override async Task<bool> UpdateAsync(Customer obj)
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
        condition.AddCondition(Customer.FieldCustomerCode, searchInfos[Customer.FieldCustomerCode], SqlOperator.Like);
        condition.AddCondition(Customer.FieldMnemonicCode, searchInfos[Customer.FieldMnemonicCode], SqlOperator.Like);
        condition.AddCondition(Customer.FieldTranNode, searchInfos[Customer.FieldTranNode], SqlOperator.Equal);
        condition.AddCondition(Customer.FieldContactPerson, searchInfos[Customer.FieldContactPerson], SqlOperator.Like);
        condition.AddCondition(Customer.FieldNativeName, searchInfos[Customer.FieldNativeName], SqlOperator.Like);
        condition.AddCondition(Customer.FieldAddress, searchInfos[Customer.FieldAddress], SqlOperator.Like);
        bool areaNoLength = !searchInfos[Customer.FieldAreaNo].IsNullOrEmpty() && searchInfos[Customer.FieldAreaNo].Length < 6;
        condition.AddCondition(Customer.FieldAreaNo, searchInfos[Customer.FieldAreaNo],
            areaNoLength ? SqlOperator.LikeStartAt : SqlOperator.Equal);
        condition.AddCondition(Customer.FieldTel, searchInfos[Customer.FieldTel], SqlOperator.Like);
        condition.AddCondition(Customer.FieldMobile, searchInfos[Customer.FieldMobile], SqlOperator.Like);
        condition.AddCondition(Customer.FieldBank, searchInfos[Customer.FieldBank], SqlOperator.Like);
        condition.AddCondition(Customer.FieldBankAccount, searchInfos[Customer.FieldBankAccount], SqlOperator.Like);
        condition.AddCondition(Customer.FieldRemark, searchInfos[Customer.FieldRemark], SqlOperator.Like);
        condition.AddCondition(Customer.FieldInUse, searchInfos[Customer.FieldInUse], SqlOperator.Equal);
        condition.AddCondition(Customer.FieldPaymentType, searchInfos[Customer.FieldPaymentType], SqlOperator.Equal);
        condition.AddCondition(Customer.FieldInvoiceFax, searchInfos[Customer.FieldInvoiceFax], SqlOperator.Between);
        condition.AddCondition(Customer.FieldCommissionType, searchInfos[Customer.FieldCommissionType], SqlOperator.Equal);
        condition.AddCondition(Customer.FieldCommissionRate, searchInfos[Customer.FieldCommissionRate], SqlOperator.Between);
        condition.AddCondition(Customer.FieldSalesDeputy, searchInfos[Customer.FieldSalesDeputy], SqlOperator.Equal);
        condition.AddCondition(Customer.FieldProjectManager, searchInfos[Customer.FieldProjectManager], SqlOperator.Equal);
        condition.AddCondition(Customer.FieldFlagInvoice, searchInfos[Customer.FieldFlagInvoice], SqlOperator.Equal);
        condition.AddCondition(Customer.FieldSalesPerson, searchInfos[Customer.FieldSalesPerson], SqlOperator.Equal);
        condition.AddCondition(Customer.FieldCreationDate, searchInfos[Customer.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(Customer.FieldCreatedBy, searchInfos[Customer.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(Customer.FieldLastUpdateDate, searchInfos[Customer.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(Customer.FieldLastUpdatedBy, searchInfos[Customer.FieldLastUpdatedBy], SqlOperator.Equal);
        condition.AddCondition(Customer.FieldFlagApp, searchInfos[Customer.FieldFlagApp], SqlOperator.Equal);
        condition.AddCondition(Customer.FieldAppUser, searchInfos[Customer.FieldAppUser], SqlOperator.Equal);
        condition.AddCondition(Customer.FieldAppDate, searchInfos[Customer.FieldAppDate], SqlOperator.Between);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}