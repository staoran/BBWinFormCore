using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 线路报价明细 业务逻辑类
/// </summary>
public class SegmentsService : BaseService<Segments>, IDynamicApiController, ITransient
{
    public SegmentsService(BaseRepository<Segments> repository, IValidator<Segments> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<Segments> NewEntityAsync()
    {
        Segments entity = await base.NewEntityAsync();
        entity.PayNodeType = "1";
        entity.RecvNodeType = "4";
        entity.OpenTime = DateTime.Now;
        entity.Closetime = DateTime.Now;
        entity.CreationDate = DateTime.Now;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
        entity.LastUpdateDate = DateTime.Now;
        entity.LastUpdatedBy = LoginUserInfo.ID.ToString();
        entity.FinancialCenterType = "4";
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
            { Segments.FieldISID, 3 },
            { Segments.FieldSegmentNo, 3 },
            { Segments.FieldCreationDate, 3 },
            { Segments.FieldCreatedBy, 3 },
            { Segments.FieldLastUpdateDate, 3 },
            { Segments.FieldLastUpdatedBy, 3 },
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
    public override async Task<bool> InsertAsync(Segments obj)
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
    public override async Task<bool> UpdateAsync(Segments obj)
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
        condition.AddCondition(Segments.FieldCostType, searchInfos[Segments.FieldCostType], SqlOperator.Equal);
        condition.AddCondition(Segments.FieldQuotationNo, searchInfos[Segments.FieldQuotationNo], SqlOperator.Equal);
        condition.AddCondition(Segments.FieldPayNodeType, searchInfos[Segments.FieldPayNodeType], SqlOperator.Equal);
        condition.AddCondition(Segments.FieldPayNodeNo, searchInfos[Segments.FieldPayNodeNo], SqlOperator.Like);
        condition.AddCondition(Segments.FieldRecvNodeType, searchInfos[Segments.FieldRecvNodeType], SqlOperator.Equal);
        condition.AddCondition(Segments.FieldRecvNodeNo, searchInfos[Segments.FieldRecvNodeNo], SqlOperator.Like);
        condition.AddCondition(Segments.FieldOpenTime, searchInfos[Segments.FieldOpenTime], SqlOperator.Between);
        condition.AddCondition(Segments.FieldClosetime, searchInfos[Segments.FieldClosetime], SqlOperator.Between);
        condition.AddCondition(Segments.FieldRemark, searchInfos[Segments.FieldRemark], SqlOperator.Like);
        condition.AddCondition(Segments.FieldFinancialCenterType, searchInfos[Segments.FieldFinancialCenterType], SqlOperator.Equal);
        condition.AddCondition(Segments.FieldFinancialCenter, searchInfos[Segments.FieldFinancialCenter], SqlOperator.Like);
        return condition.BuildConditionSql().Replace("Where", "");
    }
}