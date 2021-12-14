using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Text;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{	
	public class ReaBmsReqDtlDao : BaseDaoNHB<ReaBmsReqDtl, long>, IDReaBmsReqDtlDao
	{
        public EntityList<ReaBmsReqDtl> GetReaBmsReqDtlListByHQL(string strHqlWhere, string Order, int start, int count)
        {
            EntityList<ReaBmsReqDtl> entityList = new EntityList<ReaBmsReqDtl>();

            #region 获取list
            StringBuilder sbHql = new StringBuilder();
            sbHql.Append("select new ReaBmsReqDtl(reabmsreqdtl, reabmsreqdoc, reacenorg, reagoods) ");
            sbHql.Append("from ReaBmsReqDtl reabmsreqdtl , ReaBmsReqDoc reabmsreqdoc , ReaCenOrg reacenorg , ReaGoods reagoods ");
            sbHql.Append("where reabmsreqdtl.ReaBmsReqDoc.Id=reabmsreqdoc.Id and reabmsreqdtl.ReaCenOrg.Id=reacenorg.Id and reabmsreqdtl.GoodsID=reagoods.Id");
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                sbHql.Append(" and " + strHqlWhere);
            }

            GetDataRowRoleHQLString("reabmsreqdtl");
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
            DaoNHBSearchByHqlAction<List<ReaBmsReqDtl>, ReaBmsReqDtl> action = new DaoNHBSearchByHqlAction<List<ReaBmsReqDtl>, ReaBmsReqDtl>(strHQL, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaBmsReqDtl>>(action);
            #endregion

            #region 获取记录总数
            StringBuilder sbCount = new StringBuilder();
            sbCount.Append("select count(DISTINCT reabmsreqdtl.Id) from ReaBmsReqDtl reabmsreqdtl , ReaBmsReqDoc reabmsreqdoc , ReaCenOrg reacenorg , ReaGoods reagoods ");
            sbCount.Append("where reabmsreqdtl.ReaBmsReqDoc.Id=reabmsreqdoc.Id and reabmsreqdtl.ReaCenOrg.Id=reacenorg.Id and reabmsreqdtl.GoodsID=reagoods.Id ");
            if (strHqlWhere != null && strHqlWhere.Length > 0)
            {
                sbCount.Append(" and " + strHqlWhere);
            }
            sbCount.Append(" and " + DataRowRoleHQLString);
            string countHql = FilterMacroCommand(sbCount.ToString());//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaBmsReqDtl> actionCount = new DaoNHBGetCountByHqlAction<ReaBmsReqDtl>(countHql);
            var listCount = this.HibernateTemplate.Execute<int>(actionCount);
            #endregion

            entityList.list = list;
            entityList.count = listCount;
            return entityList;
        }
    } 
}