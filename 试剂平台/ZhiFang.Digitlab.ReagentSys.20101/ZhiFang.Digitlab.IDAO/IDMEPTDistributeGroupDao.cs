using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
	public interface IDMEPTDistributeGroupDao : IDBaseDao<MEPTDistributeGroup, long>
	{
        /// <summary>
        /// 根据样本单项目ID和分发规则ID获取分发小组
        /// </summary>
        /// <param name="longItemID">样本单项目ID</param>
        /// <param name="longDistributeRuleID">分发规则ID</param>
        /// <returns>分发小组实体列表</returns>
        IList<MEPTDistributeGroup> SearchMEPTDistributeGroupByItemAndDistributeRule(long longItemID, long longDistributeRuleID);

        /// <summary>
        /// 根据分发规则ID获取分发小组
        /// </summary>
        /// <param name="longDistributeRuleID">分发规则ID</param>
        /// <returns>分发小组实体列表</returns>
        IList<MEPTDistributeGroup> SearchMEPTDistributeGroupByDistributeRule(long longDistributeRuleID);
	} 
}