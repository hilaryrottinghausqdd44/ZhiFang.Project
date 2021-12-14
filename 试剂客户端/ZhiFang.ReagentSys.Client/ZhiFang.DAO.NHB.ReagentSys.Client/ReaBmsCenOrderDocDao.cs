using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaBmsCenOrderDocDao :BaseDaoNHBService<ReaBmsCenOrderDoc, long>, IDReaBmsCenOrderDocDao
    {
        public EntityList<ReaBmsCenOrderDoc> SearchReaBmsCenOrderDocDao(string orderDocWhere, string sort, int page, int limit)
        {
            EntityList<ReaBmsCenOrderDoc> entityList = new EntityList<ReaBmsCenOrderDoc>();
            if (string.IsNullOrEmpty(orderDocWhere))
                return entityList;
            string hql = " select reabmscenorderdoc from ReaBmsCenOrderDoc reabmscenorderdoc where 1=1 ";
            if (!string.IsNullOrEmpty(orderDocWhere))
            {
                hql = hql + " and " + orderDocWhere;
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

            DaoNHBSearchByHqlAction<IList<ReaBmsCenOrderDoc>, ReaBmsCenOrderDoc> action = new DaoNHBSearchByHqlAction<IList<ReaBmsCenOrderDoc>, ReaBmsCenOrderDoc>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<IList<ReaBmsCenOrderDoc>>(action);

            if (action.Count != null)
            {
                entityList.list = list;
            }

            string strHQL = "select count(*) from ReaBmsCenOrderDoc reabmscenorderdoc where 1=1 ";
            if (orderDocWhere != null && orderDocWhere.Length > 0)
                strHQL += "and " + orderDocWhere;
            DaoNHBGetCountByHqlAction<ReaBmsCenOrderDoc> actionCount = new DaoNHBGetCountByHqlAction<ReaBmsCenOrderDoc>(strHQL);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);

            return entityList;
        }
    }
}