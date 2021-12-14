using System;
using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBExpertRule : IBGenericManager<LBExpertRule>
    {

        EntityList<LBExpertRule> QueryLBExpertRuleByHQL(string where, string sort, int page, int limit);

        /// <summary>
        /// 复制专家规则并新增
        /// </summary>
        /// <param name="expertRuleID">专家规则ID</param>
        /// <returns></returns>
        BaseResultDataValue AddNewLBExpertRuleByLBExpertRule(long expertRuleID);
    }
}