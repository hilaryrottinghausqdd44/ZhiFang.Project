
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaBmsQtyDtlOperationDao : BaseDaoNHB<ReaBmsQtyDtlOperation, long>, IDReaBmsQtyDtlOperationDao
    {
        public IList<ReaBmsQtyDtlOperation> SearchReaBmsQtyDtlOperationListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaBmsQtyDtlOperation> entityList = new List<ReaBmsQtyDtlOperation>();
            StringBuilder sqlHql = new StringBuilder();

            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsqtydtloperation from ReaBmsQtyDtlOperation reabmsqtydtloperation,ReaGoods reagoods where reabmsqtydtloperation.GoodsID=reagoods.Id and reabmsqtydtloperation.LabID=reagoods.LabID ");
                sqlHql.Append(" and " + reaGoodsHql);
            }
            else
            {
                sqlHql.Append(" select reabmsqtydtloperation from ReaBmsQtyDtlOperation reabmsqtydtloperation where 1=1 ");
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
            GetDataRowRoleHQLString("reabmsqtydtloperation");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsQtyDtlOperation>, ReaBmsQtyDtlOperation> action = new DaoNHBSearchByHqlAction<List<ReaBmsQtyDtlOperation>, ReaBmsQtyDtlOperation>(hql, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<ReaBmsQtyDtlOperation>>(action);
            if (entityList != null && entityList.Count > 0) entityList = entityList.Distinct().ToList();
            return entityList;
        }
        public EntityList<ReaBmsQtyDtlOperation> SearchReaBmsQtyDtlOperationEntityListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaBmsQtyDtlOperation> entityList = new EntityList<ReaBmsQtyDtlOperation>();
            entityList.list = SearchReaBmsQtyDtlOperationListByAllJoinHql(where, reaGoodsHql, sort, page, limit);
            if (entityList.list == null || entityList.list.Count <= 0)
                return entityList;

            StringBuilder countHql = new StringBuilder();
            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsqtydtloperation.Id) from ReaBmsQtyDtlOperation reabmsqtydtloperation,ReaGoods reagoods where reabmsqtydtloperation.GoodsID=reagoods.Id and reabmsqtydtloperation.LabID=reagoods.LabID ");
                countHql.Append(" and " + reaGoodsHql);
            }
            else
            {
                countHql.Append(" select count(DISTINCT reabmsqtydtloperation.Id) from ReaBmsQtyDtlOperation reabmsqtydtloperation where 1=1 ");
            }

            if (!string.IsNullOrEmpty(where))
            {
                countHql.Append(" and " + where);
            }
            GetDataRowRoleHQLString("reabmsqtydtloperation");
            countHql.Append(" and " + DataRowRoleHQLString);
            string countHql2 = FilterMacroCommand(countHql.ToString());//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaBmsQtyDtlOperation> actionCount = new DaoNHBGetCountByHqlAction<ReaBmsQtyDtlOperation>(countHql2);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);
            return entityList;
        }
    }
}