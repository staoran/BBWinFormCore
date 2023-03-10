using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Security;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using FluentValidation;
using Furion.DatabaseAccessor;

namespace BB.Application.TMS;

/// <summary>
/// 网点资料 业务逻辑类
/// </summary>
[ApiDescriptionSettings("运输基础资料")]
public class NodeService : BaseMultiService<Node, Nodes>, IDynamicApiController, ITransient
{
    private readonly DocNoRuleService _docNoRuleService;
    private readonly BaseService<OUInfo> _ouBaseService;

    public NodeService(BaseRepository<Node> repository, BaseRepository<Nodes> childRepository, IValidator<Node> validator,
        IValidator<Nodes> childValidator, DocNoRuleService docNoRuleService, BaseService<OUInfo> ouBaseService)
        : base(repository, childRepository, validator, childValidator)
    {
        _docNoRuleService = docNoRuleService;
        _ouBaseService = ouBaseService;
    }

    /// <summary>
    /// 实体动态默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<Node> SetDynamicDefaults(Node entity)
    {
        // entity.TranNodeNO = Snowflake.Instance().GetId().ToString();
        entity.TranNodeNO = await _docNoRuleService.GetSNNoAsync("NO");
        // entity.CreatedBy = LoginUserInfo.ID.ToString();
        // entity.LastUpdatedBy = LoginUserInfo.ID.ToString();
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
            { nameof(Node.ISID), 3 },
            { nameof(Node.TranNodeNO), 1 },
            { nameof(Node.CompanyNo), 3 },
            { nameof(Node.ProvinceNo), 3 },
            { nameof(Node.CityNo), 3 },
            { nameof(Node.CreationDate), 1 },
            { nameof(Node.CreatedBy), 1 },
            { nameof(Node.LastUpdateDate), 1 },
            { nameof(Node.LastUpdatedBy), 1 },
            { nameof(Node.FlagApp), 1 },
            { nameof(Node.AppUser), 1 },
            { nameof(Node.AppDate), 1 },
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
    [UnitOfWork]
    public override async Task<bool> InsertAsync(Node obj)
    {
        #region 新增前

        #endregion

        bool succeed = await base.InsertAsync(obj);

        #region 新增后

        if (!succeed) return false;

        // 同步新增部门机构
        await _ouBaseService.InsertAsync(obj.Adapt<OUInfo>());

        return true;

        #endregion
    }

    /// <summary>
    /// 更新一条数据
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [UnitOfWork]
    public override async Task<bool> UpdateAsync(Node obj)
    {
        #region 修改前

        #endregion

        bool succeed = await base.UpdateAsync(obj);

        #region 修改后

        if (!succeed) return false;

        var ouInfo = obj.Adapt<OUInfo>();
        ouInfo.LastUpdateDate = await _ouBaseService.GetFieldValueAsync(obj.TranNodeNO, x => x.LastUpdateDate);
        // 同步修改部门机构
        await _ouBaseService.UpdateAsync(ouInfo);

        return true;

        #endregion
    }

    /// <summary>
    /// 删除一条数据
    /// </summary>
    /// <param name="key">指定对象的ID</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    [UnitOfWork]
    public override async Task<bool> DeleteAsync(object key)
    {
        #region 删除前

        #endregion

        bool succeed = await base.DeleteAsync(key);

        #region 删除后

        if (!succeed) return false;

        // 同步删除部门机构
        await _ouBaseService.DeleteAsync(key);

        return true;

        #endregion
    }

    /// <summary>
    /// 获取查询参数配置
    /// </summary>
    /// <returns></returns>
    public override List<FieldConditionType> GetConditionTypes()
    {
        return Cache.GetOrAdd($"{nameof(Node)}ConditionTypes",
            _ => new List<FieldConditionType>
            {
                new(Node.FieldTranNodeNO, SqlOperator.Like),
                new(Node.FieldTranNodeCostNo, SqlOperator.Equal),
                new(Node.FieldTranNodeName, SqlOperator.Like),
                new(Node.FieldTranNodeType, SqlOperator.Equal),
                new(Node.FieldTranNodeBeginDate, SqlOperator.Between),
                new(Node.FieldTranNodeEndDate, SqlOperator.Between),
                new(Node.FieldParentNo, SqlOperator.Equal),
                new(Node.FieldTranNodePerson, SqlOperator.Like),
                new(Node.FieldTranNodePersonID, SqlOperator.Like),
                new(Node.FieldTranNodeMobile, SqlOperator.Like),
                new(Node.FieldTranNodeAddress, SqlOperator.Like),
                new(Node.FieldLockLimit, SqlOperator.Equal),
                new(Node.FieldLockLimitAmt, SqlOperator.Between),
                new(Node.FieldWarningLimitAmt, SqlOperator.Between),
                new(Node.FieldSendSMS, SqlOperator.Equal),
                new(Node.FieldISLocked, SqlOperator.Equal),
                new(Node.FieldAckRec, SqlOperator.Equal),
                new(Node.FieldAgencyRecLimitAmt, SqlOperator.Between),
                // new(Node.FieldAgencyRecLimitAmtBKP, SqlOperator.Between),
                new(Node.FieldCarriageForwardLimitAmt, SqlOperator.Between),
                // new(Node.FieldCarriageForwardLimitAmtBKP, SqlOperator.Between),
                new(Node.FieldAreaNo, SqlOperator.Equal),
                new(Node.FieldInTime, SqlOperator.Between),
                new(Node.FieldOutTime, SqlOperator.Between),
                new(Node.FieldRemark, SqlOperator.Like),
                new(Node.FieldCreationDate, SqlOperator.Between),
                new(Node.FieldCreatedBy, SqlOperator.Equal),
                new(Node.FieldLastUpdateDate, SqlOperator.Between),
                new(Node.FieldLastUpdatedBy, SqlOperator.Equal),
                new(Node.FieldTranNodeStatus, SqlOperator.Equal),
                new(Node.FieldPublicYN, SqlOperator.Equal),
                new(Node.FieldFlagApp, SqlOperator.Equal),
                new(Node.FieldAppUser, SqlOperator.Equal),
                new(Node.FieldAppDate, SqlOperator.Between),
                new(Node.FieldSignLoopEndTime, SqlOperator.Between),
                new(Node.FieldSignLimitTime, SqlOperator.Between),
                new(Node.FieldSignDays, SqlOperator.Like),
                new(Node.FieldAckRecDays, SqlOperator.Like),
                new(Node.FieldCostMasterYN, SqlOperator.Equal),
                new(Node.FieldManagementFee, SqlOperator.Between),
                new(Node.FieldUsageFee, SqlOperator.Between),
                new(Node.FieldDeposit, SqlOperator.Between),
                new(Node.FieldContractNote, SqlOperator.Like),
                new(Node.FieldDispatchOnly, SqlOperator.Equal),
                new(Node.FieldPickupWeightLimit, SqlOperator.Between),
                new(Node.FieldPickupVolumeLimit, SqlOperator.Between),
                new(Node.FieldTranNodeAxes, SqlOperator.Like),
                new(Node.FieldIsLockLimitKPI, SqlOperator.Equal),
                new(Node.FieldFinancialCenter, SqlOperator.Equal),
                new(Node.FieldWhiteList, SqlOperator.Like),
                new(Node.FieldBlackList, SqlOperator.Like)
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
            !searchInfos[Node.FieldAreaNo].IsNullOrEmpty() && searchInfos[Node.FieldAreaNo].Length < 6;
        if (areaNoLength)
        {
            condition.ForEach(x =>
            {
                if (x is ConditionalModel { FieldName: Node.FieldAreaNo } c)
                {
                    c.ConditionalType = ConditionalType.LikeLeft;
                }
            });
        }

        return condition;
    }
}