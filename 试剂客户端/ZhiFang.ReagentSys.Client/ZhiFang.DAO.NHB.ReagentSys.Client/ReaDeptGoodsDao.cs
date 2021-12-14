using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaDeptGoodsDao : BaseDaoNHB<ReaDeptGoods, long>, IDReaDeptGoodsDao
    {
        public EntityList<ReaGoods> SearchReaGoodsListByHQL(int page, int limit, string where, string sort)
        {
            EntityList<ReaGoods> entityList = new EntityList<ReaGoods>();
            StringBuilder sqlHql = new StringBuilder();
            sqlHql.Append(" select reagoods from ReaDeptGoods readeptgoods left outer join readeptgoods.ReaGoods as reagoods where 1=1 ");
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
            GetDataRowRoleHQLString("readeptgoods");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaGoods>, ReaGoods> action = new DaoNHBSearchByHqlAction<List<ReaGoods>, ReaGoods>(hql, start1, count1);
            entityList.list = this.HibernateTemplate.Execute<List<ReaGoods>>(action);

            StringBuilder countHql = new StringBuilder();
            countHql.Append(" select count(DISTINCT reagoods.Id) from ReaDeptGoods readeptgoods left outer join readeptgoods.ReaGoods as reagoods where 1=1 ");
            if (!string.IsNullOrEmpty(where))
            {
                countHql.Append(" and " + where);
            }
            GetDataRowRoleHQLString("readeptgoods");
            countHql.Append(" and " + DataRowRoleHQLString);
            string countHql2 = FilterMacroCommand(countHql.ToString());//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaBmsOutDtl> actionCount = new DaoNHBGetCountByHqlAction<ReaBmsOutDtl>(countHql2);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);

            return entityList;
        }
    }
}