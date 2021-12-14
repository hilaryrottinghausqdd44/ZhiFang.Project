using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaMonthUsageStatisticsDtlDao : BaseDaoNHB<ReaMonthUsageStatisticsDtl, long>, IDReaMonthUsageStatisticsDtlDao
    {
        public IList<ReaMonthUsageStatisticsDtl> SearchReaMonthUsageStatisticsDtlListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaMonthUsageStatisticsDtl> entityList = new List<ReaMonthUsageStatisticsDtl>();
            StringBuilder sqlHql = new StringBuilder();

            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reamonthusagestatisticsdtl from ReaMonthUsageStatisticsDtl reamonthusagestatisticsdtl,ReaGoods reagoods where reamonthusagestatisticsdtl.ReaGoodsNo=reagoods.ReaGoodsNo and reamonthusagestatisticsdtl.LabID=reagoods.LabID ");
                sqlHql.Append(" and " + reaGoodsHql);
            }
            else
            {
                sqlHql.Append(" select reamonthusagestatisticsdtl from ReaMonthUsageStatisticsDtl reamonthusagestatisticsdtl where 1=1 ");
            }
            if (!string.IsNullOrEmpty(where))
            {
                sqlHql.Append(" and " + where);
            }

            string hql = sqlHql.ToString();

            int? start1 = null;
            int? count1 = null;
            if (page > 0)
            {
                start1 = page;
            }
            if (limit > 0)
            {
                count1 = limit;
            }
            GetDataRowRoleHQLString("reamonthusagestatisticsdtl");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaMonthUsageStatisticsDtl>, ReaMonthUsageStatisticsDtl> action = new DaoNHBSearchByHqlAction<List<ReaMonthUsageStatisticsDtl>, ReaMonthUsageStatisticsDtl>(hql, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<ReaMonthUsageStatisticsDtl>>(action);
            if (entityList != null && entityList.Count > 0) entityList = entityList.Distinct().ToList();
            return entityList;
        }
        public EntityList<ReaMonthUsageStatisticsDtl> SearchReaMonthUsageStatisticsEntityListDtlByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaMonthUsageStatisticsDtl> entityList = new EntityList<ReaMonthUsageStatisticsDtl>();
            entityList.list = SearchReaMonthUsageStatisticsDtlListByAllJoinHql(where, reaGoodsHql, sort, page, limit);
            if (entityList.list == null || entityList.list.Count <= 0)
                return entityList;

            StringBuilder countHql = new StringBuilder();
            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reamonthusagestatisticsdtl.Id) from ReaMonthUsageStatisticsDtl reamonthusagestatisticsdtl,ReaGoods reagoods where reamonthusagestatisticsdtl.ReaGoodsNo=reagoods.ReaGoodsNo and reamonthusagestatisticsdtl.LabID=reagoods.LabID ");
                countHql.Append(" and " + reaGoodsHql);
            }
            else
            {
                countHql.Append(" select count(DISTINCT reamonthusagestatisticsdtl.Id) from ReaMonthUsageStatisticsDtl reamonthusagestatisticsdtl where 1=1 ");
            }

            if (!string.IsNullOrEmpty(where))
            {
                countHql.Append(" and " + where);
            }
            GetDataRowRoleHQLString("reamonthusagestatisticsdtl");
            countHql.Append(" and " + DataRowRoleHQLString);
            string countHql2 = FilterMacroCommand(countHql.ToString());//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaMonthUsageStatisticsDtl> actionCount = new DaoNHBGetCountByHqlAction<ReaMonthUsageStatisticsDtl>(countHql2);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);
            return entityList;
        }
    }
}