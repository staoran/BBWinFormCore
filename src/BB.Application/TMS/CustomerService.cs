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
/// 客户管理 业务逻辑类
/// </summary>
[ApiDescriptionSettings("客户基础资料")]
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
            { nameof(Customer.CustomerCode), 1 },
            { nameof(Customer.TranNode), 1 },
            { nameof(Customer.ProvinceNo), 3 },
            { nameof(Customer.CityNo), 3 },
            { nameof(Customer.CreationDate), 1 },
            { nameof(Customer.CreatedBy), 1 },
            { nameof(Customer.LastUpdateDate), 1 },
            { nameof(Customer.LastUpdatedBy), 1 },
            { nameof(Customer.FlagApp), 1 },
            { nameof(Customer.AppUser), 1 },
            { nameof(Customer.AppDate), 1 }
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
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.Instance.GetOrCreate($"{nameof(Customer)}ConditionTypes",
            () => new List<FieldConditionType>
            {
                new(Customer.FieldCustomerCode, SqlOperator.Like),
                new(Customer.FieldMnemonicCode, SqlOperator.Like),
                new(Customer.FieldTranNode, SqlOperator.Equal),
                new(Customer.FieldContactPerson, SqlOperator.Like),
                new(Customer.FieldNativeName, SqlOperator.Like),
                new(Customer.FieldAddress, SqlOperator.Like),
                new(Customer.FieldAreaNo, SqlOperator.Equal),
                new(Customer.FieldTel, SqlOperator.Like),
                new(Customer.FieldMobile, SqlOperator.Like),
                new(Customer.FieldBank, SqlOperator.Like),
                new(Customer.FieldBankAccount, SqlOperator.Like),
                new(Customer.FieldRemark, SqlOperator.Like),
                new(Customer.FieldInUse, SqlOperator.Equal),
                new(Customer.FieldPaymentType, SqlOperator.Equal),
                new(Customer.FieldInvoiceFax, SqlOperator.Between),
                new(Customer.FieldCommissionType, SqlOperator.Equal),
                new(Customer.FieldCommissionRate, SqlOperator.Between),
                new(Customer.FieldSalesDeputy, SqlOperator.Equal),
                new(Customer.FieldProjectManager, SqlOperator.Equal),
                new(Customer.FieldFlagInvoice, SqlOperator.Equal),
                new(Customer.FieldSalesPerson, SqlOperator.Equal),
                new(Customer.FieldCreationDate, SqlOperator.Between),
                new(Customer.FieldCreatedBy, SqlOperator.Equal),
                new(Customer.FieldLastUpdateDate, SqlOperator.Between),
                new(Customer.FieldLastUpdatedBy, SqlOperator.Equal),
                new(Customer.FieldFlagApp, SqlOperator.Equal),
                new(Customer.FieldAppUser, SqlOperator.Equal),
                new(Customer.FieldAppDate, SqlOperator.Between)
            });
    }

    /// <summary>
    /// 构造查询条件
    /// </summary>
    /// <param name="searchInfos">查询参数</param>
    /// <returns></returns>
    public override async Task<List<IConditionalModel>> GetConditionExc(Dictionary<string, string> searchInfos)
    {
        var condition = await base.GetConditionExc(searchInfos);
        bool areaNoLength =
            !searchInfos[Customer.FieldAreaNo].IsNullOrEmpty() && searchInfos[Customer.FieldAreaNo].Length < 6;
        if (areaNoLength)
        {
            condition.ForEach(x =>
            {
                if (x is ConditionalModel { FieldName: Customer.FieldAreaNo } c)
                {
                    c.ConditionalType = ConditionalType.LikeLeft;
                }
            });
        }

        return condition;
    }
}