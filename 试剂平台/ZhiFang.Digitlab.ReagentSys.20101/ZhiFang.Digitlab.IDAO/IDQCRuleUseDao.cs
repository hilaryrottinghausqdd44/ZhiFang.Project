using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
    public  interface IDQCRuleUseDao : IDBaseDao<QCRuleUse, long>
    {
        /// <summary>
        /// 根据质控项目ID获取项目指定的质控规则列表
        /// </summary>
        /// <param name="longQCItemID">质控项目ID</param>
        /// <returns>IList&lt;QCRuleUse&gt;</returns>
        IList<QCRuleUse> SearchNamedQCRuleUseByQCItemID(long longQCItemID);

        /// <summary>
        /// 根据质控项目ID获取指定的质控规则ID
        /// </summary>
        /// <param name="longQCItemID">质控项目ID</param>
        /// <returns>long</returns>
        long SearchNamedQCRuleUseIDByQCItemID(long longQCItemID);

        /// <summary>
        /// 获取默认的质控规则列表
        /// </summary>
        /// <returns>IList&lt;QCRuleUse&gt;</returns>
        IList<QCRuleUse> SearchDefaultRuleUse();
    }
}
