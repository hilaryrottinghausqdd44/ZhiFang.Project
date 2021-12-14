using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
    public interface IDQCRulesConDao : IDBaseDao<QCRulesCon, long>
    {
        /// <summary>
        /// 根据质控规则ID获取规则关系
        /// </summary>
        /// <param name="longQCRuleUseID">质控规则ID</param>
        /// <returns>IList&lt;QCRulesCon&gt;</returns>
        IList<QCRulesCon> SearchQCRulesConByQCRuleUseID(long longQCRuleUseID);
    }
}
