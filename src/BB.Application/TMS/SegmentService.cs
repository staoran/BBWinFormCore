using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 线路表 业务逻辑类
/// </summary>
public class SegmentService : BaseMultiService<Segment, Segments>, IDynamicApiController, ITransient
{
    public SegmentService(BaseRepository<Segment> repository, BaseRepository<Segments> childRepository, IValidator<Segment> validator, IValidator<Segments> childValidator)
        : base(repository, childRepository, validator, childValidator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<Segment> NewEntityAsync()
    {
        Segment entity = await base.NewEntityAsync();
        entity.SegmentType = "1";
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
            { Segment.FieldISID, 3 },
            // 主键不为空则只读
            { Segment.FieldSegmentNo, 4 },
            { Segment.FieldCreationDate, 1 },
            { Segment.FieldCreatedBy, 1 },
            { Segment.FieldLastUpdateDate, 1 },
            { Segment.FieldLastUpdatedBy, 1 },
            { Segment.FieldFlagApp, 1 },
            { Segment.FieldAppUser, 1 },
            { Segment.FieldAppDate, 1 },
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
    public override async Task<bool> InsertAsync(Segment obj)
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
    public override async Task<bool> UpdateAsync(Segment obj)
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
        condition.AddCondition(Segment.FieldSegmentNo, searchInfos[Segment.FieldSegmentNo], SqlOperator.Like);
        condition.AddCondition(Segment.FieldSegmentType, searchInfos[Segment.FieldSegmentType], SqlOperator.Equal);
        condition.AddCondition(Segment.FieldSegmentName, searchInfos[Segment.FieldSegmentName], SqlOperator.Like);
        condition.AddCondition(Segment.FieldSegmentBeginNode, searchInfos[Segment.FieldSegmentBeginNode], SqlOperator.Equal);
        condition.AddCondition(Segment.FieldSegmentEndNode, searchInfos[Segment.FieldSegmentEndNode], SqlOperator.Equal);
        condition.AddCondition(Segment.FieldPlanBeginTime, searchInfos[Segment.FieldPlanBeginTime], SqlOperator.Between);
        condition.AddCondition(Segment.FieldExpectedHour, searchInfos[Segment.FieldExpectedHour], SqlOperator.Between);
        condition.AddCondition(Segment.FieldExpectedDistance, searchInfos[Segment.FieldExpectedDistance], SqlOperator.Between);
        condition.AddCondition(Segment.FieldExpectedOilWear, searchInfos[Segment.FieldExpectedOilWear], SqlOperator.Between);
        condition.AddCondition(Segment.FieldExpectedCharge, searchInfos[Segment.FieldExpectedCharge], SqlOperator.Between);
        condition.AddCondition(Segment.FieldExpectedPontage, searchInfos[Segment.FieldExpectedPontage], SqlOperator.Between);
        condition.AddCondition(Segment.FieldRemark, searchInfos[Segment.FieldRemark], SqlOperator.Like);
        condition.AddCondition(Segment.FieldCreationDate, searchInfos[Segment.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(Segment.FieldCreatedBy, searchInfos[Segment.FieldCreatedBy], SqlOperator.Equal);
        condition.AddCondition(Segment.FieldLastUpdateDate, searchInfos[Segment.FieldLastUpdateDate], SqlOperator.Between);
        condition.AddCondition(Segment.FieldLastUpdatedBy, searchInfos[Segment.FieldLastUpdatedBy], SqlOperator.Equal);
        condition.AddCondition(Segment.FieldFlagApp, searchInfos[Segment.FieldFlagApp], SqlOperator.Equal);
        condition.AddCondition(Segment.FieldAppUser, searchInfos[Segment.FieldAppUser], SqlOperator.Equal);
        condition.AddCondition(Segment.FieldAppDate, searchInfos[Segment.FieldAppDate], SqlOperator.Between);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}