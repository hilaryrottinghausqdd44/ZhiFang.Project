using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaBmsQtyBalanceDtlDao : BaseDaoNHB<ReaBmsQtyBalanceDtl, long>, IDReaBmsQtyBalanceDtlDao
    {
        public IList<ReaBmsQtyBalanceDtl> SearchListByReaGoodHQL(string dtlHql, string reaGoodHql, string sort, int page, int limit)
        {
            IList<ReaBmsQtyBalanceDtl> entityList = new List<ReaBmsQtyBalanceDtl>();

            StringBuilder sqlHql = new StringBuilder();
            if (string.IsNullOrEmpty(reaGoodHql))
            {
                sqlHql.Append(" select reabmsqtybalancedtl from ReaBmsQtyBalanceDtl reabmsqtybalancedtl where 1=1 ");
            }
            else if (!string.IsNullOrEmpty(reaGoodHql))
            {
                sqlHql.Append(" select DISTINCT reabmsqtybalancedtl from ReaBmsQtyBalanceDtl reabmsqtybalancedtl,ReaGoods reagoods where reabmsqtybalancedtl.GoodsID=reagoods.Id ");
                sqlHql.Append(" and " + reaGoodHql);
            }
            if (!string.IsNullOrEmpty(dtlHql))
            {
                sqlHql.Append(" and " + dtlHql);
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
            GetDataRowRoleHQLString("reabmsqtybalancedtl");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsQtyBalanceDtl>, ReaBmsQtyBalanceDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsQtyBalanceDtl>, ReaBmsQtyBalanceDtl>(hql, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<ReaBmsQtyBalanceDtl>>(action);
            return entityList;
        }
    }
}