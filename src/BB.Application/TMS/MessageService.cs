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
/// 消息记录 业务逻辑类
/// </summary>
public class MessageService : BaseMultiService<Message, Messages>, IDynamicApiController, ITransient
{
    public MessageService(BaseRepository<Message> repository, BaseRepository<Messages> childRepository, IValidator<Message> validator, IValidator<Messages> childValidator)
        : base(repository, childRepository, validator, childValidator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<Message> NewEntityAsync()
    {
        Message entity = await base.NewEntityAsync();
        entity.MsgNo = Snowflake.Instance().GetId().ToString();
        entity.MsgType = "99";
        entity.DealStatus = "0";
        entity.SendMsgNode = LoginUserInfo.CompanyId;
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
            { Message.FieldISID, 3 },
            // 主键不为空则只读
            { Message.FieldMsgNo, 4 },
            { Message.DisRecvMsgNode, 4 },
            { Message.FieldSendMsgNode, 1 },
            { Message.FieldDealStatus, 1 },
            { Message.FieldCreationDate, 1 },
            { Message.FieldCreatedBy, 1 },
            { Message.FieldLastUpdateDate, 1 },
            { Message.FieldLastUpdatedBy, 1 },
            { Message.FieldFlagApp, 1 },
            { Message.FieldAppUser, 1 },
            { Message.FieldAppDate, 1 },
        };
        // 后端权限控制
        // var permitDict = BllFactory<FieldPermit>.Instance.GetColumnsPermit(typeof(Test1Car).FullName, LoginUserInfo.ID.ToInt32());
        return await Task.FromResult(permitDict);
    }

    /// <summary>
    /// 构造查询语句
    /// </summary>
    /// <param name="searchInfos">查询参数</param>
    /// <returns></returns>
    public override string GetConditionSql(NameValueCollection searchInfos)
    {
        var condition = new SearchCondition();
        condition.AddCondition(Message.FieldMsgNo, searchInfos[Message.FieldMsgNo], SqlOperator.Like);
        condition.AddCondition(Message.FieldMsgType, searchInfos[Message.FieldMsgType], SqlOperator.Equal);
        condition.AddCondition(Message.FieldWaybillNo, searchInfos[Message.FieldWaybillNo], SqlOperator.Like);
        condition.AddCondition(Message.FieldSendMsgNode, searchInfos[Message.FieldSendMsgNode], SqlOperator.Equal);
        condition.AddCondition(Message.FieldSendMsgContent, searchInfos[Message.FieldSendMsgContent], SqlOperator.Like);
        condition.AddCondition(Message.FieldRecvMsgNode, searchInfos[Message.FieldRecvMsgNode], SqlOperator.Equal);
        condition.AddCondition(Message.FieldDealStatus, searchInfos[Message.FieldDealStatus], SqlOperator.Equal);
        condition.AddCondition(Message.FieldAttaPath, searchInfos[Message.FieldAttaPath], SqlOperator.Like);
        condition.AddCondition(Message.FieldCreationDate, searchInfos[Message.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(Message.FieldCreatedBy, searchInfos[Message.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(Message.FieldLastUpdateDate, searchInfos[Message.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(Message.FieldLastUpdatedBy, searchInfos[Message.FieldLastUpdatedBy], SqlOperator.Equal);
        condition.AddCondition(Message.FieldFlagApp, searchInfos[Message.FieldFlagApp], SqlOperator.Equal);
        condition.AddCondition(Message.FieldAppUser, searchInfos[Message.FieldAppUser], SqlOperator.Equal);
        condition.AddCondition(Message.FieldAppDate, searchInfos[Message.FieldAppDate], SqlOperator.Between);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}