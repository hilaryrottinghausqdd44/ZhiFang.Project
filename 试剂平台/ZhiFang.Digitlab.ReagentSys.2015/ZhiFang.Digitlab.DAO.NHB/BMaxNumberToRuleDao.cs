using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;
using NHibernate.Criterion;

namespace ZhiFang.Digitlab.DAO.NHB
{	
	public class BMaxNumberToRuleDao : BaseDaoNHB<BMaxNumberToRule, long>, IDBMaxNumberToRuleDao
	{
        #region IDBMaxNumberToRuleDao 成员

        public BMaxNumberToRule SearchBMaxNumberToRuleByNumberRuleID(long longNumberRuleID)
        {
            string strQuery = "from BMaxNumberToRule bmaxnumbertorule where bmaxnumbertorule.BNumberBuildRule.Id=:Id";
            IList<BMaxNumberToRule> tempList = base.HibernateTemplate.FindByNamedParam<BMaxNumberToRule>(strQuery, "Id", longNumberRuleID).ToList();
            if (tempList != null && tempList.Count > 0)
                return tempList[0];
            else
                return null;
        }

        #endregion
    } 
}