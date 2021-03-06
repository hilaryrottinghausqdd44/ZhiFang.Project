using System.Collections.Generic;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaBmsCenOrderDtlDao : BaseDaoNHBService<ReaBmsCenOrderDtl, long>, IDReaBmsCenOrderDtlDao
    {
        public IList<ReaBmsCenOrderDtl> SearchReaBmsCenOrderDtlSummaryByHQL(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaBmsCenOrderDtl> entityList = new List<ReaBmsCenOrderDtl>();
            StringBuilder sqlHql = new StringBuilder();
<<<<<<< .mine
            sqlHql.Append(" select new ReaBmsCenOrderDtl(reabmscenorderdoc.LabID,reabmscenorderdoc.DeptID,reabmscenorderdoc.DeptName, reabmscenorderdoc.ReaCompID,reabmscenorderdoc.CompanyName,reabmscenorderdoc.LabcID,reabmscenorderdoc.LabcName,reabmscenorderdoc.ReaServerLabcCode,reabmscenorderdtl.ReaGoodsID,reabmscenorderdtl.ReaGoodsName,reabmscenorderdtl.ReaGoodsNo,reabmscenorderdtl.GoodsUnit,reabmscenorderdtl.UnitMemo,reabmscenorderdtl.ExpectedStock,reabmscenorderdtl.MonthlyUsage,reabmscenorderdtl.LastMonthlyUsage,reabmscenorderdtl.ReqGoodsQty,reabmscenorderdtl.GoodsQty,reabmscenorderdtl.Price,reabmscenorderdtl.SumTotal,reabmscenorderdtl.CurrentQty,reabmscenorderdtl.ProdID,reabmscenorderdtl.ProdOrgName,reabmscenorderdtl.BarCodeType,reabmscenorderdtl.DataAddTime,reabmscenorderdtl.ArrivalTime,reabmscenorderdtl.Memo,reabmscenorderdtl.Id,reabmscenorderdtl.CompGoodsLinkID,reabmscenorderdtl.CenOrgGoodsNo)");
||||||| .r2673
            sqlHql.Append(" select new ReaBmsCenOrderDtl(reabmscenorderdoc.LabID,reabmscenorderdoc.DeptID,reabmscenorderdoc.DeptName, reabmscenorderdoc.ReaCompID,reabmscenorderdoc.CompanyName,reabmscenorderdoc.LabcID,reabmscenorderdoc.LabcName,reabmscenorderdoc.ReaServerLabcCode,reabmscenorderdtl.ReaGoodsID,reabmscenorderdtl.ReaGoodsName,reabmscenorderdtl.ReaGoodsNo,reabmscenorderdtl.GoodsUnit,reabmscenorderdtl.UnitMemo,reabmscenorderdtl.ExpectedStock,reabmscenorderdtl.MonthlyUsage,reabmscenorderdtl.LastMonthlyUsage,reabmscenorderdtl.ReqGoodsQty, reabmscenorderdtl.GoodsQty, reabmscenorderdtl.Price,reabmscenorderdtl.SumTotal,reabmscenorderdtl.CurrentQty,reabmscenorderdtl.ProdID, reabmscenorderdtl.ProdOrgName,reabmscenorderdtl.BarCodeType,reabmscenorderdtl.DataAddTime,reabmscenorderdtl.ArrivalTime,reabmscenorderdtl.Memo,reabmscenorderdtl.Id)");
=======
            sqlHql.Append(" select new ReaBmsCenOrderDtl(reabmscenorderdoc.LabID,reabmscenorderdoc.DeptID,reabmscenorderdoc.DeptName, reabmscenorderdoc.ReaCompID,reabmscenorderdoc.CompanyName,reabmscenorderdoc.LabcID,reabmscenorderdoc.LabcName,reabmscenorderdoc.ReaServerLabcCode,reabmscenorderdtl.ReaGoodsID,reabmscenorderdtl.ReaGoodsName,reabmscenorderdtl.ReaGoodsNo,reabmscenorderdtl.GoodsUnit,reabmscenorderdtl.UnitMemo,reabmscenorderdtl.ExpectedStock,reabmscenorderdtl.MonthlyUsage,reabmscenorderdtl.LastMonthlyUsage,reabmscenorderdtl.ReqGoodsQty, reabmscenorderdtl.GoodsQty, reabmscenorderdtl.Price,reabmscenorderdtl.SumTotal,reabmscenorderdtl.CurrentQty,reabmscenorderdtl.ProdID, reabmscenorderdtl.ProdOrgName,reabmscenorderdtl.BarCodeType,reabmscenorderdtl.DataAddTime,reabmscenorderdtl.ArrivalTime,reabmscenorderdtl.Memo,reabmscenorderdtl.Id,reabmscenorderdtl.OrderDocNo,reagoods.RegistNo,reagoods.NetGoodsNo,reagoods.SName as goodSName)");
>>>>>>> .r2783
            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                sqlHql.Append(" from ReaBmsCenOrderDtl reabmscenorderdtl,ReaBmsCenOrderDoc reabmscenorderdoc,ReaGoods reagoods where reabmscenorderdtl.OrderDocID=reabmscenorderdoc.Id and reabmscenorderdtl.ReaGoodsID=reagoods.Id ");
            }
            else
            {
                sqlHql.Append(" from ReaBmsCenOrderDtl reabmscenorderdtl,ReaBmsCenOrderDoc reabmscenorderdoc where reabmscenorderdtl.OrderDocID=reabmscenorderdoc.Id ");
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
            GetDataRowRoleHQLString();
            sqlHql.Append(" and " + DataRowRoleHQLString);
            if (!string.IsNullOrEmpty(sort))
                sqlHql.Append(" order by " + sort);
            string hql = sqlHql.ToString();
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaBmsCenOrderDtl>, ReaBmsCenOrderDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsCenOrderDtl>, ReaBmsCenOrderDtl>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaBmsCenOrderDtl>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }

        /// <summary>
        /// 根据订货单ID查询订单明细
        /// </summary>
        /// <param name="orderDocId">订货总单ID</param>
        /// <returns></returns>
        public IList<ReaBmsCenOrderDtl> GetReaBmsCenOrderDtlListByDocId(long orderDocId)
        {
            StringBuilder sqlHql = new StringBuilder();
            sqlHql.Append("select new ReaBmsCenOrderDtl(reabmscenorderdtl,reagoods) from ReaBmsCenOrderDtl reabmscenorderdtl,ReaGoods reagoods where reabmscenorderdtl.ReaGoodsID=reagoods.Id ");
            sqlHql.Append(string.Format("and reabmscenorderdtl.OrderDocID={0} ", orderDocId));
            return Session.CreateQuery(sqlHql.ToString()).List<ReaBmsCenOrderDtl>();
        }

        public EntityList<ReaBmsCenOrderDtl> GetReaBmsCenOrderDtlListByHQL(string strHqlWhere, string Order, int start, int count)
        {
            EntityList<ReaBmsCenOrderDtl> entityList = new EntityList<ReaBmsCenOrderDtl>();

            #region 获取list
            StringBuilder sbHql = new StringBuilder();
            sbHql.Append("select new ReaBmsCenOrderDtl(reabmscenorderdtl, reagoods) ");
            sbHql.Append("from ReaBmsCenOrderDtl reabmscenorderdtl, ReaGoods reagoods ");
            sbHql.Append("where reabmscenorderdtl.ReaGoodsID=reagoods.Id");
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                sbHql.Append(" and " + strHqlWhere);
            }

            GetDataRowRoleHQLString();
            sbHql.Append(" and " + DataRowRoleHQLString);
            string strHQL = FilterMacroCommand(sbHql.ToString());//宏命令过滤
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
            DaoNHBSearchByHqlAction<List<ReaBmsCenOrderDtl>, ReaBmsCenOrderDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsCenOrderDtl>, ReaBmsCenOrderDtl>(strHQL, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaBmsCenOrderDtl>>(action);
            #endregion

            #region 获取记录总数
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("select count(DISTINCT reabmscenorderdtl.Id) from ReaBmsCenOrderDtl reabmscenorderdtl, ReaGoods reagoods ");
            sbCount.Append("where reabmscenorderdtl.ReaGoodsID=reagoods.Id");
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                sbCount.Append(" and " + strHqlWhere);
            }
            sbCount.Append(" and " + DataRowRoleHQLString);
            string countHql = FilterMacroCommand(sbCount.ToString());//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaBmsCenOrderDtl> actionCount = new DaoNHBGetCountByHqlAction<ReaBmsCenOrderDtl>(countHql);
            var listCount = this.HibernateTemplate.Execute<int>(actionCount);
            #endregion

            entityList.list = list;
            entityList.count = listCount;
            return entityList;
        }

    }
}