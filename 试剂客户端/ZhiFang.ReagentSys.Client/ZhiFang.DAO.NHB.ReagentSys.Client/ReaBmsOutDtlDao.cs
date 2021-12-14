using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaBmsOutDtlDao : BaseDaoNHB<ReaBmsOutDtl, long>, IDReaBmsOutDtlDao
    {
        public IList<ReaBmsOutDtl> SearchReaBmsOutDtlSummaryByHQL(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaBmsOutDtl> entityList = new List<ReaBmsOutDtl>();
            StringBuilder sqlHql = new StringBuilder();

            sqlHql.Append(" select ");
            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                sqlHql.Append(" new ReaBmsOutDtl(reabmsoutdoc,reabmsoutdtl,reagoods) from ReaBmsOutDtl reabmsoutdtl,ReaBmsOutDoc reabmsoutdoc,ReaGoods reagoods where reabmsoutdtl.OutDocID=reabmsoutdoc.Id and reabmsoutdtl.GoodsID=reagoods.Id ");
            }
            else
            {
                sqlHql.Append(" new ReaBmsOutDtl(reabmsoutdoc,reabmsoutdtl) from ReaBmsOutDtl reabmsoutdtl,ReaBmsOutDoc reabmsoutdoc where reabmsoutdtl.OutDocID=reabmsoutdoc.Id ");
            }
            if (!string.IsNullOrEmpty(docHql))
            {
                sqlHql.Append(" and " + docHql);
            }
            if (!string.IsNullOrEmpty(dtlHql))
            {
                sqlHql.Append(" and " + dtlHql);
            }
            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                sqlHql.Append(" and " + reaGoodsHql);
            }
            if (!string.IsNullOrEmpty(sort))
                sqlHql.Append(" order by " + sort);
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
            GetDataRowRoleHQLString("reabmsoutdtl");
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsOutDtl>, ReaBmsOutDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsOutDtl>, ReaBmsOutDtl>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaBmsOutDtl>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }
        public IList<ReaBmsOutDtl> SearchOutDocAndDtlListByAllJoinHql(string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort)
        {
            IList<ReaBmsOutDtl> entityList = new List<ReaBmsOutDtl>();
            StringBuilder sqlHql = new StringBuilder();

            if (!string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select new ReaBmsOutDtl(reabmsoutdoc,reabmsoutdtl,reagoods) from ReaBmsOutDtl reabmsoutdtl,ReaGoods reagoods,ReaBmsOutDoc reabmsoutdoc,,ReaDeptGoods readeptgoods where reabmsoutdtl.GoodsID=reagoods.Id and reabmsoutdtl.OutDocID=reabmsoutdoc.Id and reabmsoutdtl.GoodsID=readeptgoods.ReaGoods.Id");
                sqlHql.Append(" and " + deptGoodsHql);
            }
            else
            {
                sqlHql.Append(" select new ReaBmsOutDtl(reabmsoutdoc,reabmsoutdtl,reagoods) from ReaBmsOutDtl reabmsoutdtl,ReaGoods reagoods,ReaBmsOutDoc reabmsoutdoc where reabmsoutdtl.GoodsID=reagoods.Id and reabmsoutdtl.OutDocID=reabmsoutdoc.Id ");
            }

            if (!string.IsNullOrEmpty(docHql))
            {
                sqlHql.Append(" and " + docHql);
            }
            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                sqlHql.Append(" and " + reaGoodsHql);
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
            GetDataRowRoleHQLString("reabmsoutdtl");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsOutDtl>, ReaBmsOutDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsOutDtl>, ReaBmsOutDtl>(hql, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<ReaBmsOutDtl>>(action);
            if (entityList != null && entityList.Count > 0) entityList = entityList.Distinct().ToList();
            return entityList;
        }
        public IList<ReaBmsOutDtl> SearchReaBmsOutDtlListByJoinHql(string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort)
        {
            IList<ReaBmsOutDtl> entityList = new List<ReaBmsOutDtl>();
            StringBuilder sqlHql = new StringBuilder();
            if (string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select reabmsoutdtl from ReaBmsOutDtl reabmsoutdtl where 1=1 ");
            }
            else if (!string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsoutdtl from ReaBmsOutDtl reabmsoutdtl,ReaBmsOutDoc reabmsoutdoc where reabmsoutdtl.OutDocID=reabmsoutdoc.Id ");
                sqlHql.Append(" and " + docHql);
            }
            else if (!string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsoutdtl from ReaBmsOutDtl reabmsoutdtl,ReaBmsOutDoc reabmsoutdoc,ReaGoods reagoods where reabmsoutdtl.OutDocID=reabmsoutdoc.Id and reabmsoutdtl.GoodsID=reagoods.Id ");
                sqlHql.Append(" and " + docHql);
                sqlHql.Append(" and " + reaGoodsHql);
            }
            else if (!string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsoutdtl from ReaBmsOutDtl reabmsoutdtl,ReaDeptGoods readeptgoods,ReaBmsOutDoc reabmsoutdoc where reabmsoutdtl.GoodsID=readeptgoods.ReaGoods.Id and reabmsoutdtl.OutDocID=reabmsoutdoc.Id ");
                sqlHql.Append(" and " + docHql);
                sqlHql.Append(" and " + deptGoodsHql);
            }
            else if (!string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsoutdtl from ReaBmsOutDtl reabmsoutdtl,ReaGoods reagoods,ReaDeptGoods readeptgoods,ReaBmsOutDoc reabmsoutdoc where reabmsoutdtl.GoodsID=reagoods.Id and reabmsoutdtl.GoodsID=readeptgoods.ReaGoods.Id and reabmsoutdtl.OutDocID=reabmsoutdoc.Id ");
                sqlHql.Append(" and " + docHql);
                sqlHql.Append(" and " + reaGoodsHql);
                sqlHql.Append(" and " + deptGoodsHql);
            }
            else if (string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsoutdtl from ReaBmsOutDtl reabmsoutdtl,ReaGoods reagoods where reabmsoutdtl.GoodsID=reagoods.Id ");
                sqlHql.Append(" and " + reaGoodsHql);
            }
            else if (string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsoutdtl from ReaBmsOutDtl reabmsoutdtl,ReaDeptGoods readeptgoods where reabmsoutdtl.GoodsID=reagoods.Id and  reabmsoutdtl.GoodsID=readeptgoods.ReaGoods.Id ");
                sqlHql.Append(" and " + reaGoodsHql);
                sqlHql.Append(" and " + deptGoodsHql);
            }
            else if (string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsoutdtl from ReaBmsOutDtl reabmsoutdtl,ReaDeptGoods readeptgoods where reabmsoutdtl.GoodsID=readeptgoods.ReaGoods.Id ");
                sqlHql.Append(" and " + deptGoodsHql);
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
            GetDataRowRoleHQLString("reabmsoutdtl");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsOutDtl>, ReaBmsOutDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsOutDtl>, ReaBmsOutDtl>(hql, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<ReaBmsOutDtl>>(action);

            return entityList;
        }
        public EntityList<ReaBmsOutDtl> SearchReaBmsOutDtlEntityListByJoinHql(string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort)
        {
            EntityList<ReaBmsOutDtl> entityList = new EntityList<ReaBmsOutDtl>();
            entityList.list = SearchReaBmsOutDtlListByJoinHql(docHql, dtlHql, deptGoodsHql, reaGoodsHql, page, limit, sort);
            if (entityList.list == null || entityList.list.Count <= 0)
                return entityList;

            StringBuilder countHql = new StringBuilder();
            if (string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsoutdtl.Id) from ReaBmsOutDtl reabmsoutdtl where 1=1 ");
            }
            else if (!string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsoutdtl.Id) from ReaBmsOutDtl reabmsoutdtl,ReaBmsOutDoc reabmsoutdoc where reabmsoutdtl.OutDocID=reabmsoutdoc.Id ");
                countHql.Append(" and " + docHql);
            }
            else if (!string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsoutdtl.Id) from ReaBmsOutDtl reabmsoutdtl,ReaBmsOutDoc reabmsoutdoc,ReaGoods reagoods where reabmsoutdtl.OutDocID=reabmsoutdoc.Id and reabmsoutdtl.GoodsID=reagoods.Id ");
                countHql.Append(" and " + docHql);
                countHql.Append(" and " + reaGoodsHql);
            }
            else if (!string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsoutdtl.Id) from ReaBmsOutDtl reabmsoutdtl,ReaDeptGoods readeptgoods,ReaBmsOutDoc reabmsoutdoc where  reabmsoutdtl.GoodsID=readeptgoods.ReaGoods.Id and reabmsoutdtl.OutDocID=reabmsoutdoc.Id ");
                countHql.Append(" and " + docHql);
                countHql.Append(" and " + deptGoodsHql);
            }
            else if (!string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsoutdtl.Id) from ReaBmsOutDtl reabmsoutdtl,ReaGoods reagoods,ReaDeptGoods readeptgoods,ReaBmsOutDoc reabmsoutdoc where reabmsoutdtl.GoodsID=reagoods.Id and reabmsoutdtl.GoodsID=readeptgoods.ReaGoods.Id and reabmsoutdtl.OutDocID=reabmsoutdoc.Id ");
                countHql.Append(" and " + docHql);
                countHql.Append(" and " + reaGoodsHql);
                countHql.Append(" and " + deptGoodsHql);
            }
            else if (string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsoutdtl.Id) from ReaBmsOutDtl reabmsoutdtl,ReaGoods reagoods where reabmsoutdtl.GoodsID=reagoods.Id ");
                countHql.Append(" and " + reaGoodsHql);
            }
            else if (string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsoutdtl.Id) from ReaBmsOutDtl reabmsoutdtl,ReaDeptGoods readeptgoods where reabmsoutdtl.GoodsID=reagoods.Id and  reabmsoutdtl.GoodsID=readeptgoods.ReaGoods.Id ");
                countHql.Append(" and " + reaGoodsHql);
                countHql.Append(" and " + deptGoodsHql);
            }
            else if (string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsoutdtl.Id) from ReaBmsOutDtl reabmsoutdtl,ReaDeptGoods readeptgoods where reabmsoutdtl.GoodsID=readeptgoods.ReaGoods.Id ");
                countHql.Append(" and " + deptGoodsHql);
            }
            if (!string.IsNullOrEmpty(dtlHql))
            {
                countHql.Append(" and " + dtlHql);
            }
            GetDataRowRoleHQLString("reabmsoutdtl");
            countHql.Append(" and " + DataRowRoleHQLString);
            string countHql2 = FilterMacroCommand(countHql.ToString());//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaBmsOutDtl> actionCount = new DaoNHBGetCountByHqlAction<ReaBmsOutDtl>(countHql2);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);

            return entityList;
        }
        public IList<ReaBmsOutDtl> SearchListByJoinHql(string dtlHql, string reaGoodHql, string sort, int page, int limit)
        {
            IList<ReaBmsOutDtl> entityList = new List<ReaBmsOutDtl>();

            StringBuilder sqlHql = new StringBuilder();
            if (string.IsNullOrEmpty(reaGoodHql))
            {
                sqlHql.Append(" select reabmsoutdtl from ReaBmsOutDtl reabmsoutdtl where 1=1 ");
            }
            else if (!string.IsNullOrEmpty(reaGoodHql))
            {
                sqlHql.Append(" select DISTINCT reabmsoutdtl from ReaBmsOutDtl reabmsoutdtl,ReaGoods reagoods where reabmsoutdtl.GoodsID=reagoods.Id ");
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
            GetDataRowRoleHQLString("reabmsoutdtl");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsOutDtl>, ReaBmsOutDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsOutDtl>, ReaBmsOutDtl>(hql, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<ReaBmsOutDtl>>(action);
            return entityList;
        }

        public new EntityList<ReaBmsOutDtl> GetListByHQL(string strHqlWhere, string Order, int start, int count, bool hasLabId)
        {
            GetDataRowRoleHQLString(hasLabId);

            EntityList<ReaBmsOutDtl> entityList = new EntityList<ReaBmsOutDtl>();

            string strHQL = "select new ReaBmsOutDtl(reabmsoutdoc,reabmsoutdtl,reagoods) from ReaBmsOutDtl reabmsoutdtl,ReaBmsOutDoc reabmsoutdoc,ReaGoods reagoods where reabmsoutdtl.OutDocID=reabmsoutdoc.Id and reabmsoutdtl.GoodsID=reagoods.Id ";
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                strHQL += "and " + strHqlWhere;
            }
            strHQL += " and " + DataRowRoleHQLString.Replace(" and LabID=", " and reabmsoutdtl.LabID=");
            strHQL = FilterMacroCommand(strHQL);//宏命令过滤
            if (Order != null && Order.Trim().Length > 0)
            {
                strHQL += " order by " + Order;
            }
            int? start1 = null;
            int? count1 = null;
            if (start > 0)
            {
                start1 = start;
            }
            if (count > 0)
            {
                count1 = count;
            }
            DaoNHBSearchByHqlAction<List<ReaBmsOutDtl>, ReaBmsOutDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsOutDtl>, ReaBmsOutDtl>(strHQL, start1, count1);
            entityList.list = this.HibernateTemplate.Execute<List<ReaBmsOutDtl>>(action);

            //获取count
            string strHQL_Count = "select count(DISTINCT reabmsoutdtl.Id) from ReaBmsOutDtl reabmsoutdtl,ReaBmsOutDoc reabmsoutdoc,ReaGoods reagoods where reabmsoutdtl.OutDocID=reabmsoutdoc.Id and reabmsoutdtl.GoodsID=reagoods.Id ";
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                strHQL_Count += "and " + strHqlWhere;
            }
            strHQL_Count += " and " + DataRowRoleHQLString.Replace(" and LabID=", " and reabmsoutdtl.LabID=");
            strHQL_Count = FilterMacroCommand(strHQL_Count);//宏命令过滤            
            DaoNHBGetCountByHqlAction<ReaBmsQtyDtl> actionCount = new DaoNHBGetCountByHqlAction<ReaBmsQtyDtl>(strHQL_Count);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);

            return entityList;
        }

    }
}