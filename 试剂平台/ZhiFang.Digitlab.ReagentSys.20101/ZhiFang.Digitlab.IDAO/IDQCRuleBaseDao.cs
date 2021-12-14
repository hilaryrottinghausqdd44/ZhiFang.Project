using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
    public  interface IDQCRuleBaseDao : IDBaseDao<QCRuleBase, long>
    {
        /// <summary>
        /// 根据质控规则ID获取基本质控规则列表
        /// </summary>
        /// <param name="longQCRuleUseID">质控规则ID</param>
        /// <returns>IList&lt;QCRuleBase&gt;</returns>
        IList<QCRuleBase> SearchQCRuleBaseByQCRuleUseID(long longQCRuleUseID);
        
    }
}
