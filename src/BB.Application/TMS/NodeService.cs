using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.Security;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 网点资料 业务逻辑类
/// </summary>
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
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<Node> NewEntityAsync()
    {
        Node entity = await base.NewEntityAsync();
        // entity.TranNodeNO = Snowflake.Instance().GetId().ToString();
        entity.TranNodeNO = await _docNoRuleService.GetSNNoAsync("NO");
        entity.TranNodeType = "9";
        entity.LockLimit = false;
        entity.LockLimitAmt = 3000;
        entity.WarningLimitAmt = 5000;
        entity.SendSMS = false;
        entity.ISLocked = false;
        entity.TranNodeBeginDate = DateTime.Now;
        entity.TranNodeEndDate = DateTime.Now.AddYears(1);
        entity.AckRec = false;
        entity.AgencyRecLimitAmt = 0;
        // entity.AgencyRecLimitAmtBKP = 0;
        entity.CarriageForwardLimitAmt = 0;
        // entity.CarriageForwardLimitAmtBKP = 0;
        entity.CreationDate = DateTime.Now;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdatedBy = LoginUserInfo.ID.ToString();
        entity.TranNodeStatus = "1";
        entity.PublicYN = false;
        entity.FlagApp = false;
        entity.SignDays = 1;
        entity.AckRecDays = 2;
        entity.CostMasterYN = false;
        entity.ManagementFee = 0;
        entity.UsageFee = 0;
        entity.Deposit = 0;
        entity.DispatchOnly = false;
        entity.PickupWeightLimit = 9999;
        entity.PickupVolumeLimit = 999;
        entity.IsLockLimitKPI = false;
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
            { Node.FieldISID, 3 },
            { Node.FieldTranNodeNO, 1 },
            { Node.FieldCompanyNo, 3 },
            { Node.FieldProvinceNo, 3 },
            { Node.FieldCityNo, 3 },
            { Node.FieldCreationDate, 1 },
            { Node.FieldCreatedBy, 1 },
            { Node.FieldLastUpdateDate, 1 },
            { Node.FieldLastUpdatedBy, 1 },
            { Node.FieldFlagApp, 1 },
            { Node.FieldAppUser, 1 },
            { Node.FieldAppDate, 1 },
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
    public override async Task<bool> InsertAsync(Node obj)
    {
        #region 新增前

        #endregion

        bool succeed = await base.InsertAsync(obj);

        #region 新增后

        if (!succeed) return false;

        // 同步新增部门机构
        var ouInfo = new OUInfo
        {
            PID = obj.ParentNo,
            HandNo = obj.TranNodeNO,
            Name = obj.TranNodeName,
            Category = obj.TranNodeType,
            Address = obj.TranNodeAddress,
            OuterPhone = obj.TranNodeMobile,
            Note = obj.Remark,
            Creator = obj.CreatedBy,
            CreatedBy = obj.CreatedBy,
            Enabled = true,
            CompanyId = obj.CompanyNo
        };
        await _ouBaseService.InsertAsync(ouInfo);

        return true;

        #endregion
    }

    /// <summary>
    /// 更新一条数据
    /// </summary>
    /// <param name="obj">指定的对象</param>
    /// <returns>执行成功返回<c>true</c>，否则为<c>false</c>。</returns>
    public override async Task<bool> UpdateAsync(Node obj)
    {
        #region 修改前

        #endregion

        bool succeed = await base.UpdateAsync(obj);

        #region 修改后

        if (!succeed) return false;

        // 同步修改部门机构
        var ouInfo = new OUInfo
        {
            PID = obj.ParentNo,
            HandNo = obj.TranNodeNO,
            Name = obj.TranNodeName,
            Category = obj.TranNodeType,
            Address = obj.TranNodeAddress,
            OuterPhone = obj.TranNodeMobile,
            Note = obj.Remark,
            Creator = obj.CreatedBy,
            CreatedBy = obj.CreatedBy,
            Enabled = true,
            CompanyId = obj.CompanyNo
        };
        await _ouBaseService.UpdateAsync(ouInfo);

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

        // 同步删除部门机构
        await _ouBaseService.DeleteAsync(key);

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
        condition.AddCondition(Node.FieldTranNodeNO, searchInfos[Node.FieldTranNodeNO], SqlOperator.Like);
        condition.AddCondition(Node.FieldTranNodeCostNo, searchInfos[Node.FieldTranNodeCostNo], SqlOperator.Equal);
        condition.AddCondition(Node.FieldTranNodeName, searchInfos[Node.FieldTranNodeName], SqlOperator.Like);
        condition.AddCondition(Node.FieldTranNodeType, searchInfos[Node.FieldTranNodeType], SqlOperator.Equal);
        condition.AddCondition(Node.FieldTranNodeBeginDate, searchInfos[Node.FieldTranNodeBeginDate], SqlOperator.Between);
        condition.AddCondition(Node.FieldTranNodeEndDate, searchInfos[Node.FieldTranNodeEndDate], SqlOperator.Between);
        condition.AddCondition(Node.FieldParentNo, searchInfos[Node.FieldParentNo], SqlOperator.Equal);
        condition.AddCondition(Node.FieldTranNodePerson, searchInfos[Node.FieldTranNodePerson], SqlOperator.Like);
        condition.AddCondition(Node.FieldTranNodePersonID, searchInfos[Node.FieldTranNodePersonID], SqlOperator.Like);
        condition.AddCondition(Node.FieldTranNodeMobile, searchInfos[Node.FieldTranNodeMobile], SqlOperator.Like);
        condition.AddCondition(Node.FieldTranNodeAddress, searchInfos[Node.FieldTranNodeAddress], SqlOperator.Like);
        condition.AddCondition(Node.FieldLockLimit, searchInfos[Node.FieldLockLimit], SqlOperator.Equal);
        condition.AddCondition(Node.FieldLockLimitAmt, searchInfos[Node.FieldLockLimitAmt], SqlOperator.Between);
        condition.AddCondition(Node.FieldWarningLimitAmt, searchInfos[Node.FieldWarningLimitAmt], SqlOperator.Between);
        condition.AddCondition(Node.FieldSendSMS, searchInfos[Node.FieldSendSMS], SqlOperator.Equal);
        condition.AddCondition(Node.FieldISLocked, searchInfos[Node.FieldISLocked], SqlOperator.Equal);
        condition.AddCondition(Node.FieldAckRec, searchInfos[Node.FieldAckRec], SqlOperator.Equal);
        condition.AddCondition(Node.FieldAgencyRecLimitAmt, searchInfos[Node.FieldAgencyRecLimitAmt], SqlOperator.Between);
        // condition.AddCondition(Node.FieldAgencyRecLimitAmtBKP, searchInfos[Node.FieldAgencyRecLimitAmtBKP], SqlOperator.Between);
        condition.AddCondition(Node.FieldCarriageForwardLimitAmt, searchInfos[Node.FieldCarriageForwardLimitAmt], SqlOperator.Between);
        // condition.AddCondition(Node.FieldCarriageForwardLimitAmtBKP, searchInfos[Node.FieldCarriageForwardLimitAmtBKP], SqlOperator.Between);
        bool areaNoLength = !searchInfos[Node.FieldAreaNo].IsNullOrEmpty() && searchInfos[Node.FieldAreaNo].Length < 6;
        condition.AddCondition(Node.FieldAreaNo, searchInfos[Node.FieldAreaNo],
            areaNoLength ? SqlOperator.LikeStartAt : SqlOperator.Equal);
        condition.AddCondition(Node.FieldInTime, searchInfos[Node.FieldInTime], SqlOperator.Between);
        condition.AddCondition(Node.FieldOutTime, searchInfos[Node.FieldOutTime], SqlOperator.Between);
        condition.AddCondition(Node.FieldRemark, searchInfos[Node.FieldRemark], SqlOperator.Like);
        condition.AddCondition(Node.FieldCreationDate, searchInfos[Node.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(Node.FieldCreatedBy, searchInfos[Node.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(Node.FieldLastUpdateDate, searchInfos[Node.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(Node.FieldLastUpdatedBy, searchInfos[Node.FieldLastUpdatedBy], SqlOperator.Equal);
        condition.AddCondition(Node.FieldTranNodeStatus, searchInfos[Node.FieldTranNodeStatus], SqlOperator.Equal);
        condition.AddCondition(Node.FieldPublicYN, searchInfos[Node.FieldPublicYN], SqlOperator.Equal);
        condition.AddCondition(Node.FieldFlagApp, searchInfos[Node.FieldFlagApp], SqlOperator.Equal);
        condition.AddCondition(Node.FieldAppUser, searchInfos[Node.FieldAppUser], SqlOperator.Equal);
        condition.AddCondition(Node.FieldAppDate, searchInfos[Node.FieldAppDate], SqlOperator.Between);
        condition.AddCondition(Node.FieldSignLoopEndTime, searchInfos[Node.FieldSignLoopEndTime], SqlOperator.Between);
        condition.AddCondition(Node.FieldSignLimitTime, searchInfos[Node.FieldSignLimitTime], SqlOperator.Between);
        condition.AddCondition(Node.FieldSignDays, searchInfos[Node.FieldSignDays], SqlOperator.Like);
        condition.AddCondition(Node.FieldAckRecDays, searchInfos[Node.FieldAckRecDays], SqlOperator.Like);
        condition.AddCondition(Node.FieldCostMasterYN, searchInfos[Node.FieldCostMasterYN], SqlOperator.Equal);
        condition.AddCondition(Node.FieldManagementFee, searchInfos[Node.FieldManagementFee], SqlOperator.Between);
        condition.AddCondition(Node.FieldUsageFee, searchInfos[Node.FieldUsageFee], SqlOperator.Between);
        condition.AddCondition(Node.FieldDeposit, searchInfos[Node.FieldDeposit], SqlOperator.Between);
        condition.AddCondition(Node.FieldContractNote, searchInfos[Node.FieldContractNote], SqlOperator.Like);
        condition.AddCondition(Node.FieldDispatchOnly, searchInfos[Node.FieldDispatchOnly], SqlOperator.Equal);
        condition.AddCondition(Node.FieldPickupWeightLimit, searchInfos[Node.FieldPickupWeightLimit], SqlOperator.Between);
        condition.AddCondition(Node.FieldPickupVolumeLimit, searchInfos[Node.FieldPickupVolumeLimit], SqlOperator.Between);
        condition.AddCondition(Node.FieldTranNodeAxes, searchInfos[Node.FieldTranNodeAxes], SqlOperator.Like);
        condition.AddCondition(Node.FieldIsLockLimitKPI, searchInfos[Node.FieldIsLockLimitKPI], SqlOperator.Equal);
        condition.AddCondition(Node.FieldFinancialCenter, searchInfos[Node.FieldFinancialCenter], SqlOperator.Equal);
        condition.AddCondition(Node.FieldWhiteList, searchInfos[Node.FieldWhiteList], SqlOperator.Like);
        condition.AddCondition(Node.FieldBlackList, searchInfos[Node.FieldBlackList], SqlOperator.Like);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}