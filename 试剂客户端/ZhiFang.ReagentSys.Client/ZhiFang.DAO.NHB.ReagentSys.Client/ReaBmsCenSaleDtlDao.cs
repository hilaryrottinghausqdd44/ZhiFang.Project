using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaBmsCenSaleDtlDao : BaseDaoNHBService<ReaBmsCenSaleDtl, long>, IDReaBmsCenSaleDtlDao
    {
        public BaseResultBool UpdatePrintCount(IList<long> batchList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (batchList == null || batchList.Count <= 0)
                return tempBaseResultBool;

            foreach (var id in batchList)
            {
                string hql = "update ReaBmsCenSaleDtl reabmscensaledtl set reabmscensaledtl.PrintCount=(reabmscensaledtl.PrintCount+1) where reabmscensaledtl.Id=" + id;
                int counts = this.UpdateByHql(hql);
                if (counts > 0)
                    tempBaseResultBool.success = true;
                else
                    tempBaseResultBool.success = false;
            }
            return tempBaseResultBool;
        }
        public IList<ReaBmsCenSaleDtl> SearchDtlAdnDocListByHQL(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaBmsCenSaleDtl> entityList = new List<ReaBmsCenSaleDtl>();

            string hql = " select new ReaBmsCenSaleDtl(reabmscensaledoc,reabmscensaledtl) from ReaBmsCenSaleDtl reabmscensaledtl,ReaBmsCenSaleDoc reabmscensaledoc where reabmscensaledtl.SaleDocID=reabmscensaledoc.Id";
            if (!string.IsNullOrEmpty(docHql))
            {
                hql = hql + " and " + docHql;
            }
            if (!string.IsNullOrEmpty(dtlHql))
            {
                hql = hql + " and " + dtlHql;
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
            GetDataRowRoleHQLString();
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsCenSaleDtl>, ReaBmsCenSaleDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsCenSaleDtl>, ReaBmsCenSaleDtl>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaBmsCenSaleDtl>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }
        public IList<ReaBmsCenSaleDtl> SearchReaBmsCenSaleDtlSummaryByHQL(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit)
        {
            return SearchDtlAdnDocListByHQL(docHql, dtlHql, reaGoodsHql, sort, page, limit);
        }
        public EntityList<ReaBmsCenSaleDtl> SearchNewEntityListByHQL(string where, string sort, int page, int limit)
        {
            EntityList<ReaBmsCenSaleDtl> entityList = new EntityList<ReaBmsCenSaleDtl>();
            entityList.list = new List<ReaBmsCenSaleDtl>();

            var list = SearchDtlAdnDocListByHQL("", where,"", sort, page, limit);
            if (list != null)
            {
                entityList.list = list;
            }
            else
            {
                return entityList;
            }
            string strHQL = "select count(*) from ReaBmsCenSaleDtl reabmscensaledtl where 1=1 ";
            if (where != null && where.Length > 0)
                strHQL += "and " + where;
            DaoNHBGetCountByHqlAction<ReaBmsCenSaleDtl> actionCount = new DaoNHBGetCountByHqlAction<ReaBmsCenSaleDtl>(strHQL);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);
            return entityList;
        }
    }
}