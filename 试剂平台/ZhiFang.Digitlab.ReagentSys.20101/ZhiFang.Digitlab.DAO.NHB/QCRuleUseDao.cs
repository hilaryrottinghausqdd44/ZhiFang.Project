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
	public class QCRuleUseDao : BaseDaoNHB<QCRuleUse, long>, IDQCRuleUseDao
	{
        #region IDQCRuleUseDao 成员

        public IList<QCRuleUse> SearchNamedQCRuleUseByQCItemID(long longQCItemID)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("QCRuleUse", null);
            dic.Add("QCItemRuleList", null);
            dic.Add("QCItem", new List<ICriterion>() { Restrictions.Eq("Id", longQCItemID) });

            DaoNHBCriteriaAction<List<QCRuleUse>, QCRuleUse> action = new DaoNHBCriteriaAction<List<QCRuleUse>, QCRuleUse>(dic);

            List<QCRuleUse> l = base.HibernateTemplate.Execute<List<QCRuleUse>>(action);
            return l;
        }

        /// <summary>
        /// 根据质控项目ID获取指定的质控规则ID
        /// </summary>
        /// <param name="longQCItemID">质控项目ID</param>
        /// <returns>long</returns>
        public long SearchNamedQCRuleUseIDByQCItemID(long longQCItemID)
        {
            string strHQL = "select qcitem.QCRuleUse.Id from QCItem qcitem where qcitem.Id=" + longQCItemID;
            var l = this.HibernateTemplate.Find<object>(strHQL).ToArray();
            if (l[0] != null)
            {
                return long.Parse(l[0].ToString());
            }
            else
            {
                return 0;
            }            
        }

        public IList<QCRuleUse> SearchDefaultRuleUse()
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("QCRuleUse", new List<ICriterion>() { Restrictions.Eq("IsDefault", true) });

            DaoNHBCriteriaAction<List<QCRuleUse>, QCRuleUse> action = new DaoNHBCriteriaAction<List<QCRuleUse>, QCRuleUse>(dic);

            List<QCRuleUse> l = base.HibernateTemplate.Execute<List<QCRuleUse>>(action);
            return l;
        }

        #endregion
    } 
}