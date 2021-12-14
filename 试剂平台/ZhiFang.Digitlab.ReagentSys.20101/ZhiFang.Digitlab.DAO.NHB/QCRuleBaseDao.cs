using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;
using NHibernate;
using NHibernate.Criterion;

namespace ZhiFang.Digitlab.DAO.NHB
{	
	public class QCRuleBaseDao : BaseDaoNHB<QCRuleBase, long>, IDQCRuleBaseDao
	{
        #region IDQCRuleBaseDao 成员

        public IList<QCRuleBase> SearchQCRuleBaseByQCRuleUseID(long longQCRuleUseID)
        {
            string strHQL = "select qcrulebase from QCRuleBase qcrulebase join qcrulebase.QCRulesConList qcrulesconlist where qcrulesconlist.QCRuleUse.Id=" + longQCRuleUseID + " order by qcrulesconlist.JudgeIndex asc ";
            var l = this.HibernateTemplate.Find<QCRuleBase>(strHQL);
            return l;
            //Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            //dic.Add("QCRuleBase", null);
            //dic.Add("QCRulesConList", null);
            //dic.Add("QCRuleUse", new List<ICriterion>() { Restrictions.Eq("Id", longQCRuleUseID) });

            //DaoNHBCriteriaAction<List<QCRuleBase>, QCRuleBase> action = new DaoNHBCriteriaAction<List<QCRuleBase>, QCRuleBase>(dic);

            //List<QCRuleBase> l = base.HibernateTemplate.Execute<List<QCRuleBase>>(action);
            //return l;
        }

        #endregion
    } 
}