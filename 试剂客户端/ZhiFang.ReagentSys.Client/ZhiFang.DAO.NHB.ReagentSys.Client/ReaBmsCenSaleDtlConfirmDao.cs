using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{	
	public class ReaBmsCenSaleDtlConfirmDao : BaseDaoNHB<ReaBmsCenSaleDtlConfirm, long>, IDReaBmsCenSaleDtlConfirmDao
    {
        public IList<ReaBmsCenSaleDtlConfirm> SearchReaBmsCenSaleDtlConfirmSummaryByHQL(string docHql, string dtlHql, string sort, int page, int limit)
        {
            IList<ReaBmsCenSaleDtlConfirm> entityList = new List<ReaBmsCenSaleDtlConfirm>();
            //reabmscensaledtlconfirm.UnitMemo,
            string hql = " select new ReaBmsCenSaleDtlConfirm(reabmscensaledtlconfirm.LabID,reabmscensaledtlconfirm.ReaCompID,reabmscensaledtlconfirm.ReaCompanyName,reabmscensaledtlconfirm.ReaGoodsNo,reabmscensaledtlconfirm.ReaGoodsID,reabmscensaledtlconfirm.ReaGoodsName,reabmscensaledtlconfirm.GoodsUnit,reabmscensaledtlconfirm.UnitMemo,reabmscensaledtlconfirm.GoodsQty,reabmscensaledtlconfirm.Price,reabmscensaledtlconfirm.SumTotal,reabmscensaledtlconfirm.TaxRate,reabmscensaledtlconfirm.AcceptCount,reabmscensaledtlconfirm.RefuseCount,reabmscensaledtlconfirm.LotNo,reabmscensaledtlconfirm.ProdDate,reabmscensaledtlconfirm.InvalidDate,reabmscensaledtlconfirm.BiddingNo,reabmscensaledtlconfirm.GoodsNo,reabmscensaledtlconfirm.ReaServerCompCode,reabmscensaledtlconfirm.RegisterInvalidDate,reabmscensaledtlconfirm.BarCodeType,reabmscensaledtlconfirm.ProdGoodsNo,reabmscensaledtlconfirm.CenOrgGoodsNo,reabmscensaledtlconfirm.RegisterNo,reabmscensaledtlconfirm.GoodsSort,reabmscensaledtlconfirm.AcceptMemo,reabmscensaledtlconfirm.DataAddTime) from ReaBmsCenSaleDtlConfirm reabmscensaledtlconfirm,ReaBmsCenSaleDocConfirm reabmscensaledocconfirm where reabmscensaledtlconfirm.SaleDocConfirmID=reabmscensaledocconfirm.Id";
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
            GetDataRowRoleHQLString("reabmscensaledtlconfirm");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsCenSaleDtlConfirm>, ReaBmsCenSaleDtlConfirm> action = new DaoNHBSearchByHqlAction<List<ReaBmsCenSaleDtlConfirm>, ReaBmsCenSaleDtlConfirm>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaBmsCenSaleDtlConfirm>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }

        /// <summary>
        /// 关联货品表查询供货验收明细
        /// </summary>
        public EntityList<ReaBmsCenSaleDtlConfirm> GetReaBmsCenSaleDtlConfirmListByHql(string strHqlWhere, string sort, int page, int limit)
        {
            EntityList<ReaBmsCenSaleDtlConfirm> entityList = new EntityList<ReaBmsCenSaleDtlConfirm>();

            StringBuilder sqlHql = new StringBuilder();
            sqlHql.Append(" select new ReaBmsCenSaleDtlConfirm(reabmscensaledtlconfirm,reagoods )");
            sqlHql.Append(" from ReaBmsCenSaleDtlConfirm reabmscensaledtlconfirm,ReaGoods reagoods where reabmscensaledtlconfirm.ReaGoodsID=reagoods.Id ");
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
            GetDataRowRoleHQLString("reabmscensaledtlconfirm");
            string hql = sqlHql.ToString();
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsCenSaleDtlConfirm>, ReaBmsCenSaleDtlConfirm> action = new DaoNHBSearchByHqlAction<List<ReaBmsCenSaleDtlConfirm>, ReaBmsCenSaleDtlConfirm>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaBmsCenSaleDtlConfirm>>(action);

            entityList.list = list;
            entityList.count = this.GetListCountByHQL(strHqlWhere);

            return entityList;
        }

    } 
}