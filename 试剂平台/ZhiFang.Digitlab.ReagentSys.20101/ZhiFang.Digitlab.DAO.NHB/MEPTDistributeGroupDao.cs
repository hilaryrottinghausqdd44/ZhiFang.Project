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
	public class MEPTDistributeGroupDao : BaseDaoNHB<MEPTDistributeGroup, long>, IDMEPTDistributeGroupDao
	{
        /// <summary>
        /// 根据样本单项目ID和分发规则ID获取分发小组
        /// </summary>
        /// <param name="longItemID">样本单项目ID</param>
        /// <param name="longDistributeRuleID">分发规则ID</param>
        /// <returns>分发小组实体列表</returns>
        public IList<MEPTDistributeGroup> SearchMEPTDistributeGroupByItemAndDistributeRule(long longItemID, long longDistributeRuleID)
        {
            string strHQL = " select mdg from MEPTDistributeGroup mdg " +
                            " inner join mdg.MEPTDistributeGroupItemList mdgi " +
                            " inner join mdg.MEPTDistributeRuleDetailList mdrd " +
                            " where mdgi.ItemAllItem.Id=" + longItemID.ToString() +
                            " and mdrd.MEPTDistributeRule.Id=" + longDistributeRuleID.ToString();
            return this.HibernateTemplate.Find<MEPTDistributeGroup>(strHQL);
        }

        /// <summary>
        /// 根据分发规则ID获取分发小组
        /// </summary>
        /// <param name="longDistributeRuleID">分发规则ID</param>
        /// <returns>分发小组实体列表</returns>
        public IList<MEPTDistributeGroup> SearchMEPTDistributeGroupByDistributeRule(long longDistributeRuleID)
        {
            //string strHQL = " select mdg from MEPTDistributeGroup mdg " +
            //    " inner join mdg.MEPTDistributeRuleDetailList mdrd " +
            //    " where mdrd.MEPTDistributeRule.Id=" + longDistributeRuleID.ToString();
            //return this.HibernateTemplate.Find<MEPTDistributeGroup>(strHQL);
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("MEPTDistributeGroup", null);
            dic.Add("MEPTDistributeRuleDetailList", null);
            dic.Add("MEPTDistributeRule", new List<ICriterion>() { Restrictions.Eq("Id", longDistributeRuleID) });
            DaoNHBCriteriaAction<List<MEPTDistributeGroup>, MEPTDistributeGroup> action = new DaoNHBCriteriaAction<List<MEPTDistributeGroup>, MEPTDistributeGroup>(dic);
            List<MEPTDistributeGroup> list = base.HibernateTemplate.Execute<List<MEPTDistributeGroup>>(action);
            return list;
        }

	} 
}