using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaBmsTransferDtlDao : BaseDaoNHB<ReaBmsTransferDtl, long>, IDReaBmsTransferDtlDao
    {
        public IList<ReaBmsTransferDtl> SearchReaBmsTransferDtlSummaryByHQL(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaBmsTransferDtl> entityList = new List<ReaBmsTransferDtl>();
            StringBuilder sqlHql = new StringBuilder();

            sqlHql.Append(" select new ReaBmsTransferDtl(reabmstransferdoc,reabmstransferdtl) ");
            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                sqlHql.Append(" from ReaBmsTransferDtl reabmstransferdtl,ReaBmsTransferDoc reabmstransferdoc,ReaGoods reagoods where reabmstransferdtl.TransferDocID=reabmstransferdoc.Id and reabmstransferdtl.GoodsID=reagoods.Id ");
            }
            else
            {
                sqlHql.Append(" from ReaBmsTransferDtl reabmstransferdtl,ReaBmsTransferDoc reabmstransferdoc where reabmstransferdtl.TransferDocID=reabmstransferdoc.Id ");
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
            GetDataRowRoleHQLString("reabmstransferdtl");
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsTransferDtl>, ReaBmsTransferDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsTransferDtl>, ReaBmsTransferDtl>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaBmsTransferDtl>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }

    }
}