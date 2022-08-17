using System.Collections.Specialized;
using BB.Core.DbContext;
using BB.Core.Services.Base;
using BB.Entity.TMS;
using BB.Tools.Entity;
using BB.Tools.Extension;
using BB.Tools.Format;
using FluentValidation;

namespace BB.Application.TMS;

/// <summary>
/// 单号规则 业务逻辑类
/// </summary>
public class DocNoRuleService : BaseService<DocNoRule>, IDynamicApiController, ITransient
{
    public DocNoRuleService(BaseRepository<DocNoRule> repository, IValidator<DocNoRule> validator) : base(repository, validator)
    {
    }

    /// <summary>
    /// 初始化实体并附加默认值
    /// </summary>
    /// <returns></returns>
    public override async Task<DocNoRule> NewEntityAsync()
    {
        DocNoRule entity = await base.NewEntityAsync();
        entity.NoLength = 5;
        entity.ResetZero = false;
        entity.FlagSpilitNo = true;
        entity.FlagIncludeDocCode = false;
        entity.FlagLastMillisecond = false;
        entity.CurrentValue = 0;
        entity.CreationDate = DateTime.Now;
        entity.CreatedBy = LoginUserInfo.ID.ToString();
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
            { DocNoRule.FieldISID, 4 },
            { DocNoRule.FieldRuleFormat, 1 },
            { DocNoRule.FieldCurrentValue, 1 },
            { DocNoRule.FieldCurrentYMD, 1 },
            { DocNoRule.FieldCreationDate, 1 },
            { DocNoRule.FieldCreatedBy, 1 },
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
    public override async Task<bool> InsertAsync(DocNoRule obj)
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
    public override async Task<bool> UpdateAsync(DocNoRule obj)
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
        condition.AddCondition(DocNoRule.FieldDocCode, searchInfos[DocNoRule.FieldDocCode], SqlOperator.Like);
        condition.AddCondition(DocNoRule.FieldRuleFormat, searchInfos[DocNoRule.FieldRuleFormat], SqlOperator.Like);
        condition.AddCondition(DocNoRule.FieldNoLength, searchInfos[DocNoRule.FieldNoLength], SqlOperator.Like);
        condition.AddCondition(DocNoRule.FieldResetZero, searchInfos[DocNoRule.FieldResetZero], SqlOperator.Equal);
        condition.AddCondition(DocNoRule.FieldFlagSpilitNo, searchInfos[DocNoRule.FieldFlagSpilitNo], SqlOperator.Equal);
        condition.AddCondition(DocNoRule.FieldFlagIncludeDocCode, searchInfos[DocNoRule.FieldFlagIncludeDocCode], SqlOperator.Equal);
        condition.AddCondition(DocNoRule.FieldFlagLastMillisecond, searchInfos[DocNoRule.FieldFlagLastMillisecond], SqlOperator.Equal);
        condition.AddCondition(DocNoRule.FieldCurrentValue, searchInfos[DocNoRule.FieldCurrentValue], SqlOperator.Like);
        condition.AddCondition(DocNoRule.FieldCurrentYMD, searchInfos[DocNoRule.FieldCurrentYMD], SqlOperator.Like);
        condition.AddCondition(DocNoRule.FieldCreationDate, searchInfos[DocNoRule.FieldCreationDate], SqlOperator.Between);
        condition.AddCondition(DocNoRule.FieldCreatedBy, searchInfos[DocNoRule.FieldCreatedBy], SqlOperator.Equal);
        return condition.BuildConditionSql().Replace("Where", "");
    }

    /// <summary>
    /// 获取单据流水号
    /// </summary>
    /// <param name="docCode">单据字头</param>
    /// <returns></returns>
    public async Task<string> GetSNNoAsync(string docCode)
    {
        string no = await Repository.Db.Ado.UseStoredProcedure().GetStringAsync("sp_sys_GetDocNo", new { DocCode = docCode });
        if (no.IsNullOrEmpty())
        {
            throw Oops.Bah("没有获取到合法的单号");
        }
        
        return no;
    }
}