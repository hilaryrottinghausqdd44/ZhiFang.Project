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
    public class ReaBmsCheckDtlDao : BaseDaoNHB<ReaBmsCheckDtl, long>, IDReaBmsCheckDtlDao
    {
        public IList<ReaBmsCheckDtl> SearchReaBmsCheckDtlListByJoinHQL(string checkHql, string checkDtlHql, string reaGoodHql, string sort, int page, int limit)
        {
            IList<ReaBmsCheckDtl> entityList = new List<ReaBmsCheckDtl>();
            StringBuilder sqlHql = new StringBuilder();
            if (!string.IsNullOrEmpty(checkHql))
            {
                sqlHql.Append(" select new ReaBmsCheckDtl(reabmscheckdoc,reabmscheckdtl,reagoods) from ReaBmsCheckDtl reabmscheckdtl,ReaBmsCheckDoc reabmscheckdoc,ReaGoods reagoods where reabmscheckdtl.GoodsID=reagoods.Id and reabmscheckdtl.CheckDocID=reabmscheckdoc.Id ");
                sqlHql.Append(" and " + checkHql);
            }
            else
            {
                sqlHql.Append(" select new ReaBmsCheckDtl(reabmscheckdtl,reagoods) from ReaBmsCheckDtl reabmscheckdtl,ReaGoods reagoods where reabmscheckdtl.GoodsID=reagoods.Id ");
            }
            if (!string.IsNullOrEmpty(checkDtlHql))
            {
                sqlHql.Append(" and " + checkDtlHql);
            }
            if (!string.IsNullOrEmpty(reaGoodHql))
            {
                sqlHql.Append(" and " + reaGoodHql);
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
            GetDataRowRoleHQLString("reabmscheckdtl");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsCheckDtl>, ReaBmsCheckDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsCheckDtl>, ReaBmsCheckDtl>(hql, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<ReaBmsCheckDtl>>(action);

            return entityList;
        }
        public EntityList<ReaBmsCheckDtl> SearchReaBmsCheckDtlEntityListByJoinHQL(string checkHql, string checkDtlHql, string reaGoodHql, string sort, int page, int limit)
        {
            EntityList<ReaBmsCheckDtl> entityList = new EntityList<ReaBmsCheckDtl>();
            entityList.list = SearchReaBmsCheckDtlListByJoinHQL(checkHql, checkDtlHql, reaGoodHql, sort, page, limit);
            if (entityList.list == null || entityList.list.Count <= 0)
                return entityList;

            StringBuilder countHql = new StringBuilder();
            if (!string.IsNullOrEmpty(checkHql))
            {
                countHql.Append(" select count(DISTINCT reabmscheckdtl.Id) from ReaBmsCheckDtl reabmscheckdtl,ReaBmsCheckDoc reabmscheckdoc,ReaGoods reagoods where reabmscheckdtl.GoodsID=reagoods.Id and reabmscheckdtl.CheckDocID=reabmscheckdoc.Id ");
                countHql.Append(" and " + checkHql);
            }
            else
            {
                countHql.Append(" select count(DISTINCT reabmscheckdtl.Id) from ReaBmsCheckDtl reabmscheckdtl,ReaGoods reagoods where reabmscheckdtl.GoodsID=reagoods.Id ");
            }
            if (!string.IsNullOrEmpty(checkDtlHql))
            {
                countHql.Append(" and " + checkDtlHql);
            }
            if (!string.IsNullOrEmpty(reaGoodHql))
            {
                countHql.Append(" and " + reaGoodHql);
            }

            GetDataRowRoleHQLString("reabmscheckdtl");
            countHql.Append(" and " + DataRowRoleHQLString);
            string countHql2 = FilterMacroCommand(countHql.ToString());//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaBmsQtyDtl> actionCount = new DaoNHBGetCountByHqlAction<ReaBmsQtyDtl>(countHql2);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);

            return entityList;
        }
    }
}