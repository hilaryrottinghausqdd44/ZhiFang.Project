using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaBmsInDtlDao : BaseDaoNHB<ReaBmsInDtl, long>, IDReaBmsInDtlDao
    {
        public IList<ReaBmsInDtl> SearchReaBmsInDtlSummaryByHQL(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaBmsInDtl> entityList = new List<ReaBmsInDtl>();
            StringBuilder sqlHql = new StringBuilder();
            sqlHql.Append(" select new ReaBmsInDtl(reabmsindtl,reagoods,");
            sqlHql.Append(" (select t1.TransportNo from ReaBmsCenSaleDocConfirm t1 where t1.Id =(select SaleDocConfirmID from ReaBmsCenSaleDtlConfirm t2 where t2.Id=reabmsindtl.SaleDtlConfirmID)) as TransportNo,");
            sqlHql.Append(" (select t1.OrderDocNo  from ReaBmsCenSaleDocConfirm t1 where t1.Id =(select SaleDocConfirmID from ReaBmsCenSaleDtlConfirm t2 where t2.Id=reabmsindtl.SaleDtlConfirmID)) as OrderDocNo,");
            sqlHql.Append(" reabmsindoc.InvoiceNo as InvoiceNo");
            sqlHql.Append(" )");
            sqlHql.Append(" from ReaBmsInDtl reabmsindtl,ReaBmsInDoc reabmsindoc,ReaGoods reagoods where reabmsindtl.InDocID=reabmsindoc.Id and reabmsindtl.ReaGoods.Id=reagoods.Id ");
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
            GetDataRowRoleHQLString("reabmsindtl");
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsInDtl>, ReaBmsInDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsInDtl>, ReaBmsInDtl>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaBmsInDtl>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }
        public IList<ReaBmsInDtl> SearchListByReaGoodHQL(string dtlHql, string reaGoodHql, string sort, int page, int limit)
        {
            IList<ReaBmsInDtl> entityList = new List<ReaBmsInDtl>();

            StringBuilder sqlHql = new StringBuilder();
            if (string.IsNullOrEmpty(reaGoodHql))
            {
                sqlHql.Append(" select reabmsindtl from ReaBmsInDtl reabmsindtl where 1=1 ");
            }
            else if (!string.IsNullOrEmpty(reaGoodHql))
            {
                sqlHql.Append(" select DISTINCT reabmsindtl from ReaBmsInDtl reabmsindtl,ReaGoods reagoods where reabmsindtl.ReaGoods.Id=reagoods.Id ");
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
            GetDataRowRoleHQLString("reabmsindtl");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsInDtl>, ReaBmsInDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsInDtl>, ReaBmsInDtl>(hql, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<ReaBmsInDtl>>(action);
            return entityList;
        }
        public IList<ReaBmsInDtl> SearchReaBmsInDtlListByJoinHql(string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort)
        {
            IList<ReaBmsInDtl> entityList = new List<ReaBmsInDtl>();
            StringBuilder sqlHql = new StringBuilder();
            if (string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select reabmsindtl from ReaBmsInDtl reabmsindtl where 1=1 ");
            }
            else if (!string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsindtl from ReaBmsInDtl reabmsindtl,ReaBmsInDoc reabmsindoc where reabmsindtl.InDocID=reabmsindoc.Id ");
                sqlHql.Append(" and " + docHql);
            }
            else if (!string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsindtl from ReaBmsInDtl reabmsindtl,ReaBmsInDoc reabmsindoc,ReaGoods reagoods where reabmsindtl.InDocID=reabmsindoc.Id and reabmsindtl.ReaGoods.Id=reagoods.Id ");
                sqlHql.Append(" and " + docHql);
                sqlHql.Append(" and " + reaGoodsHql);
            }
            else if (!string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsindtl from ReaBmsInDtl reabmsindtl,ReaDeptGoods readeptgoods,ReaBmsInDoc reabmsindoc where reabmsindtl.ReaGoods.Id=readeptgoods.ReaGoods.Id and reabmsindtl.InDocID=reabmsindoc.Id ");
                sqlHql.Append(" and " + docHql);
                sqlHql.Append(" and " + deptGoodsHql);
            }
            else if (!string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsindtl from ReaBmsInDtl reabmsindtl,ReaGoods reagoods,ReaDeptGoods readeptgoods,ReaBmsInDoc reabmsindoc where reabmsindtl.ReaGoods.Id=reagoods.Id and reabmsindtl.ReaGoods.Id=readeptgoods.ReaGoods.Id and reabmsindtl.InDocID=reabmsindoc.Id ");
                sqlHql.Append(" and " + docHql);
                sqlHql.Append(" and " + reaGoodsHql);
                sqlHql.Append(" and " + deptGoodsHql);
            }
            else if (string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsindtl from ReaBmsInDtl reabmsindtl,ReaGoods reagoods where reabmsindtl.ReaGoods.Id=reagoods.Id ");
                sqlHql.Append(" and " + reaGoodsHql);
            }
            else if (string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsindtl from ReaBmsInDtl reabmsindtl,ReaDeptGoods readeptgoods where reabmsindtl.ReaGoods.Id=reagoods.Id and  reabmsindtl.ReaGoods.Id=readeptgoods.ReaGoods.Id ");
                sqlHql.Append(" and " + reaGoodsHql);
                sqlHql.Append(" and " + deptGoodsHql);
            }
            else if (string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                sqlHql.Append(" select DISTINCT reabmsindtl from ReaBmsInDtl reabmsindtl,ReaDeptGoods readeptgoods where reabmsindtl.ReaGoods.Id=readeptgoods.ReaGoods.Id ");
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
            GetDataRowRoleHQLString("reabmsindtl");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsInDtl>, ReaBmsInDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsInDtl>, ReaBmsInDtl>(hql, start1, count1);
            entityList = this.HibernateTemplate.Execute<List<ReaBmsInDtl>>(action);

            return entityList;
        }
        public EntityList<ReaBmsInDtl> SearchReaBmsInDtlEntityListByJoinHql(string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort)
        {
            EntityList<ReaBmsInDtl> entityList = new EntityList<ReaBmsInDtl>();
            entityList.list = SearchReaBmsInDtlListByJoinHql(docHql, dtlHql, deptGoodsHql, reaGoodsHql, page, limit, sort);
            if (entityList.list == null || entityList.list.Count <= 0)
                return entityList;

            StringBuilder countHql = new StringBuilder();
            if (string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsindtl.Id) from ReaBmsInDtl reabmsindtl where 1=1 ");
            }
            else if (!string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsindtl.Id) from ReaBmsInDtl reabmsindtl,ReaBmsInDoc reabmsindoc where reabmsindtl.InDocID=reabmsindoc.Id ");
                countHql.Append(" and " + docHql);
            }
            else if (!string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsindtl.Id) from ReaBmsInDtl reabmsindtl,ReaBmsInDoc reabmsindoc,ReaGoods reagoods where reabmsindtl.InDocID=reabmsindoc.Id and reabmsindtl.ReaGoods.Id=reagoods.Id ");
                countHql.Append(" and " + docHql);
                countHql.Append(" and " + reaGoodsHql);
            }
            else if (!string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsindtl.Id) from ReaBmsInDtl reabmsindtl,ReaDeptGoods readeptgoods,ReaBmsInDoc reabmsindoc where  reabmsindtl.ReaGoods.Id=readeptgoods.ReaGoods.Id and reabmsindtl.InDocID=reabmsindoc.Id ");
                countHql.Append(" and " + docHql);
                countHql.Append(" and " + deptGoodsHql);
            }
            else if (!string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsindtl.Id) from ReaBmsInDtl reabmsindtl,ReaGoods reagoods,ReaDeptGoods readeptgoods,ReaBmsInDoc reabmsindoc where reabmsindtl.ReaGoods.Id=reagoods.Id and reabmsindtl.ReaGoods.Id=readeptgoods.ReaGoods.Id and reabmsindtl.InDocID=reabmsindoc.Id ");
                countHql.Append(" and " + docHql);
                countHql.Append(" and " + reaGoodsHql);
                countHql.Append(" and " + deptGoodsHql);
            }
            else if (string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsindtl.Id) from ReaBmsInDtl reabmsindtl,ReaGoods reagoods where reabmsindtl.ReaGoods.Id=reagoods.Id ");
                countHql.Append(" and " + reaGoodsHql);
            }
            else if (string.IsNullOrEmpty(docHql) && !string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsindtl.Id) from ReaBmsInDtl reabmsindtl,ReaDeptGoods readeptgoods where reabmsindtl.ReaGoods.Id=reagoods.Id and  reabmsindtl.ReaGoods.Id=readeptgoods.ReaGoods.Id ");
                countHql.Append(" and " + reaGoodsHql);
                countHql.Append(" and " + deptGoodsHql);
            }
            else if (string.IsNullOrEmpty(docHql) && string.IsNullOrEmpty(reaGoodsHql) && !string.IsNullOrEmpty(deptGoodsHql))
            {
                countHql.Append(" select count(DISTINCT reabmsindtl.Id) from ReaBmsInDtl reabmsindtl,ReaDeptGoods readeptgoods where reabmsindtl.ReaGoods.Id=readeptgoods.ReaGoods.Id ");
                countHql.Append(" and " + deptGoodsHql);
            }
            if (!string.IsNullOrEmpty(dtlHql))
            {
                countHql.Append(" and " + dtlHql);
            }
            GetDataRowRoleHQLString("reabmsindtl");
            countHql.Append(" and " + DataRowRoleHQLString);
            string countHql2 = FilterMacroCommand(countHql.ToString());//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaBmsQtyDtl> actionCount = new DaoNHBGetCountByHqlAction<ReaBmsQtyDtl>(countHql2);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);

            return entityList;
        }

        /// <summary>
        /// 关联货品表查询入库明细
        /// </summary>
        public EntityList<ReaBmsInDtl> GetReaBmsInDtlListByHql(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<ReaBmsInDtl> entityList = new EntityList<ReaBmsInDtl>();

            StringBuilder sqlHql = new StringBuilder();
            sqlHql.Append(" select new ReaBmsInDtl(reabmsindtl,reagoods )");
            sqlHql.Append(" from ReaBmsInDtl reabmsindtl,ReaGoods reagoods where reabmsindtl.ReaGoods.Id=reagoods.Id ");
            if (!string.IsNullOrEmpty(strHqlWhere))
            {
                sqlHql.Append(" and " + strHqlWhere);
            }

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
            GetDataRowRoleHQLString("reabmsindtl");
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsInDtl>, ReaBmsInDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsInDtl>, ReaBmsInDtl>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaBmsInDtl>>(action);

            entityList.list = list;
            entityList.count= this.GetListCountByHQL(strHqlWhere);

            return entityList;
        }

    }
}