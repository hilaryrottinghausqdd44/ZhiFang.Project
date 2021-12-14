using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.IDAO.ReagentSys;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.DAO.NHB.ReagentSys
{
    public class BmsCenOrderDocDao : BaseDaoNHB<BmsCenOrderDoc, long>, IDBmsCenOrderDocDao
    {
        public EntityList<BmsCenOrderDoc> SearchBmsCenOrderDocDao(string orderDocWhere, string orderDtlWhere, string sort, int page, int limit)
        {
            EntityList<BmsCenOrderDoc> entityList = new EntityList<BmsCenOrderDoc>();
            if (string.IsNullOrWhiteSpace(orderDocWhere) && string.IsNullOrWhiteSpace(orderDtlWhere))
                return entityList;
            string hql = " select bmscenorderdoc from BmsCenOrderDoc bmscenorderdoc where 1=1 ";
            if (!string.IsNullOrWhiteSpace(orderDocWhere))
            { 
                hql = hql + " and " + orderDocWhere;
            }
            if (!string.IsNullOrWhiteSpace(orderDtlWhere))
            {
                hql = hql + " and  bmscenorderdoc.Id in (select bmscenorderdtl.BmsCenOrderDoc.Id from BmsCenOrderDtl bmscenorderdtl " +
                    " where bmscenorderdtl.BmsCenOrderDoc.Id =bmscenorderdoc.Id and " + orderDtlWhere + ")";

            }
            if (!string.IsNullOrWhiteSpace(sort))
                hql = hql + " order by " + sort;
            DaoNHBSearchByHqlAction<IList<BmsCenOrderDoc>, BmsCenOrderDoc> action = new DaoNHBSearchByHqlAction<IList<BmsCenOrderDoc>, BmsCenOrderDoc>(hql, page, limit);
            var list = this.HibernateTemplate.Execute<IList<BmsCenOrderDoc>>(action);
            if (action.Count != null)
            {
                entityList.list = list;
                entityList.count = (int)action.Count;
            }
            return entityList;
        }
    } 
}