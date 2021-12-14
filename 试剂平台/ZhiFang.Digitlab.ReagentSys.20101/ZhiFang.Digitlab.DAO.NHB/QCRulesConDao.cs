using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{	
	public class QCRulesConDao : BaseDaoNHB<QCRulesCon, long>, IDQCRulesConDao
	{
        #region IDQCRulesConDao 成员

        public IList<QCRulesCon> SearchQCRulesConByQCRuleUseID(long longQCRuleUseID)
        {
            string strHql = "from QCRulesCon qcrulescon where qcrulescon.QCRuleUse.Id=" + longQCRuleUseID;
            return this.HibernateTemplate.Find<QCRulesCon>(strHql);
        }

        #endregion
    } 
}