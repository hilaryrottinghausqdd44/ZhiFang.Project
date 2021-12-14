using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaGoodsLotDao : BaseDaoNHB<ReaGoodsLot, long>, IDReaGoodsLotDao
    {
        public IList<ReaGoodsLot> SearchReaGoodsLotListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaGoodsLot> entityList = new List<ReaGoodsLot>();
            StringBuilder sqlHql = new StringBuilder();

            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reagoodslot from ReaGoodsLot reagoodslot,ReaGoods reagoods where reagoodslot.ReaGoodsNo=reagoods.ReaGoodsNo and reagoodslot.LabID=reagoods.LabID ");
                sqlHql.Append(" and " + reaGoodsHql);
            }
            else
            {
                sqlHql.Append(" select reagoodslot from ReaGoodsLot reagoodslot where 1=1 ");
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
            GetDataRowRoleHQLString("reagoodslot");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaGoodsLot>, ReaGoodsLot> action = new DaoNHBSearchByHqlAction<List<ReaGoodsLot>, ReaGoodsLot>(hql, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<ReaGoodsLot>>(action);
            if (entityList != null && entityList.Count > 0) entityList = entityList.Distinct().ToList();
            return entityList;
        }
        public EntityList<ReaGoodsLot> SearchReaGoodsLotEntityListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaGoodsLot> entityList = new EntityList<ReaGoodsLot>();
            entityList.list = SearchReaGoodsLotListByAllJoinHql(where, reaGoodsHql, sort, page, limit);
            if (entityList.list == null || entityList.list.Count <= 0)
                return entityList;

            StringBuilder countHql = new StringBuilder();
            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reagoodslot.Id) from ReaGoodsLot reagoodslot,ReaGoods reagoods where reagoodslot.ReaGoodsNo=reagoods.ReaGoodsNo and reagoodslot.LabID=reagoods.LabID ");
                countHql.Append(" and " + reaGoodsHql);
            }
            else
            {
                countHql.Append(" select count(DISTINCT reagoodslot.Id) from ReaGoodsLot reagoodslot where 1=1 ");
            }

            if (!string.IsNullOrEmpty(where))
            {
                countHql.Append(" and " + where);
            }
            GetDataRowRoleHQLString("reagoodslot");
            countHql.Append(" and " + DataRowRoleHQLString);
            string countHql2 = FilterMacroCommand(countHql.ToString());//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaGoodsLot> actionCount = new DaoNHBGetCountByHqlAction<ReaGoodsLot>(countHql2);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);
            return entityList;
        }
    }
}