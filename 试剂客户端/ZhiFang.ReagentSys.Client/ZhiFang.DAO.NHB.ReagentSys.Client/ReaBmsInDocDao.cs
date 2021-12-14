using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaBmsInDocDao : BaseDaoNHB<ReaBmsInDoc, long>, IDReaBmsInDocDao
    {
        public EntityList<ReaBmsInDoc> SearchListByDocAndDtlHQL(string docHql, string dtlHql, string sort, int page, int limit)
        {
            EntityList<ReaBmsInDoc> entityList = new EntityList<ReaBmsInDoc>();

            string listHql = "";
            string countHql = "";
            if (!string.IsNullOrEmpty(dtlHql))
            {
                listHql = " select DISTINCT reabmsindoc from ReaBmsInDoc reabmsindoc,ReaBmsInDtl reabmsindtl where 1=1 and reabmsindtl.InDocID=reabmsindoc.Id ";
                countHql = " select count(DISTINCT reabmsindoc.Id) from ReaBmsInDoc reabmsindoc,ReaBmsInDtl reabmsindtl where 1=1 and reabmsindtl.InDocID=reabmsindoc.Id  ";

                listHql = listHql + " and " + dtlHql;
                countHql = countHql + " and " + dtlHql;
            }
            else
            {
                listHql = " select reabmsindoc from ReaBmsInDoc reabmsindoc where 1=1 ";
                countHql = " select count(reabmsindoc.Id) from ReaBmsInDoc reabmsindoc where 1=1  ";
            }

            if (!string.IsNullOrEmpty(docHql))
            {
                listHql = listHql + " and " + docHql;
                countHql = countHql + " and " + docHql;
            }

            GetDataRowRoleHQLString("reabmsindoc");
            listHql += " and " + DataRowRoleHQLString;
            listHql = FilterMacroCommand(listHql);//宏命令过滤

            countHql += " and " + DataRowRoleHQLString;
            countHql = FilterMacroCommand(countHql);//宏命令过滤

            if (!string.IsNullOrEmpty(sort))
                listHql = listHql + " order by " + sort;
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

            DaoNHBSearchByHqlAction<List<ReaBmsInDoc>, ReaBmsInDoc> action = new DaoNHBSearchByHqlAction<List<ReaBmsInDoc>, ReaBmsInDoc>(listHql, start1, count1);
            entityList.list = this.HibernateTemplate.Execute<List<ReaBmsInDoc>>(action);

            DaoNHBGetCountByHqlAction<ReaBmsInDoc> action2 = new DaoNHBGetCountByHqlAction<ReaBmsInDoc>(countHql);
            entityList.count = this.HibernateTemplate.Execute<int>(action2);
            return entityList;
        }
        public EntityList<ReaBmsInDoc> SearchReaBmsInDocOfQtyGEZeroByJoinHql(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaBmsInDoc> entityList = new EntityList<ReaBmsInDoc>();

            string listHqlStr = "";
            string countHqlStr = "";
            StringBuilder listHql = new StringBuilder();
            StringBuilder countHql = new StringBuilder();
            //联查默认过滤条件and reabmsqtydtl.PlaceID=reabmsindtl.PlaceID
            string onHql = " reabmsqtydtl.GoodsQty>0 and reabmsqtydtl.InDtlID=reabmsindtl.Id  and reabmsqtydtl.InDocNo=reabmsindoc.InDocNo and reabmsqtydtl.StorageID=reabmsindtl.StorageID and reabmsindtl.InDocID=reabmsindoc.Id ";

            listHql.Append(" select DISTINCT reabmsindoc from ReaBmsInDoc reabmsindoc,ReaBmsInDtl reabmsindtl,ReaBmsQtyDtl reabmsqtydtl ");
            listHql.Append(" where ");
            listHql.Append(onHql);

            countHql.Append(" select count(DISTINCT reabmsindoc.Id) from ReaBmsInDoc reabmsindoc,ReaBmsInDtl reabmsindtl,ReaBmsQtyDtl reabmsqtydtl ");
            countHql.Append(" where ");
            countHql.Append(onHql);

            if (!string.IsNullOrEmpty(docHql))
            {
                listHql.Append(" and " + docHql);
                countHql.Append(" and " + docHql);
            }
            if (!string.IsNullOrEmpty(dtlHql))
            {
                listHql.Append(" and " + dtlHql);
                countHql.Append(" and " + dtlHql);
            }
            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                listHql.Append(" and " + reaGoodsHql);
                countHql.Append(" and " + reaGoodsHql);
            }
            listHqlStr = listHql.ToString();
            countHqlStr = countHql.ToString();

            GetDataRowRoleHQLString("reabmsindoc");
            listHqlStr += " and " + DataRowRoleHQLString;
            listHqlStr = FilterMacroCommand(listHqlStr);//宏命令过滤
            countHqlStr += " and " + DataRowRoleHQLString;
            countHqlStr = FilterMacroCommand(countHqlStr);//宏命令过滤

            if (!string.IsNullOrEmpty(sort))
                listHqlStr = listHqlStr + " order by " + sort;
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

            DaoNHBSearchByHqlAction<List<ReaBmsInDoc>, ReaBmsInDoc> action = new DaoNHBSearchByHqlAction<List<ReaBmsInDoc>, ReaBmsInDoc>(listHqlStr, start1, count1);
            entityList.list = this.HibernateTemplate.Execute<List<ReaBmsInDoc>>(action).Distinct().ToList();

            DaoNHBGetCountByHqlAction<ReaBmsInDoc> action2 = new DaoNHBGetCountByHqlAction<ReaBmsInDoc>(countHqlStr);
            entityList.count = this.HibernateTemplate.Execute<int>(action2);
            return entityList;
        }
    }
}