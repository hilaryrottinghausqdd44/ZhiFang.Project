using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaTestEquipItemDao : BaseDaoNHB<ReaTestEquipItem, long>, IDReaTestEquipItemDao
    {
        public IList<ReaTestEquipItem> SearchReaTestEquipItemListByJoinHql(string where, string reatestitemHql, string sort, int page, int limit)
        {
            IList<ReaTestEquipItem> entityList = new List<ReaTestEquipItem>();
            string hql = " select new ReaTestEquipItem(reatestequipitem,reatestequiplab,reatestitem) from ReaTestEquipItem reatestequipitem,ReaTestEquipLab reatestequiplab,ReaTestItem reatestitem where reatestequipitem.TestEquipID=reatestequiplab.Id and reatestequipitem.TestItemID=reatestitem.Id  and reatestequipitem.LabID=reatestequiplab.LabID  and reatestequipitem.LabID=reatestitem.LabID ";
            if (!string.IsNullOrEmpty(where))
            {
                hql = hql + " and " + where;
            }
            if (reatestitemHql != null && reatestitemHql.Length > 0)
                hql += " and " + reatestitemHql;
            
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
            GetDataRowRoleHQLString("reatestequipitem");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaTestEquipItem>, ReaTestEquipItem> action = new DaoNHBSearchByHqlAction<List<ReaTestEquipItem>, ReaTestEquipItem>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaTestEquipItem>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }
        public EntityList<ReaTestEquipItem> SearchReaTestEquipItemEntityListByJoinHql(string where, string reatestitemHql, string sort, int page, int limit)
        {
            EntityList<ReaTestEquipItem> entityList = new EntityList<ReaTestEquipItem>();
            entityList.list = new List<ReaTestEquipItem>();

            var list = SearchReaTestEquipItemListByJoinHql(where, reatestitemHql, sort, page, limit);
            if (list != null)
            {
                entityList.list = list;
            }
            else
            {
                return entityList;
            }
            string strHQL = "select count(*) from ReaTestEquipItem reatestequipitem,ReaTestEquipLab reatestequiplab,ReaTestItem reatestitem where reatestequipitem.TestEquipID=reatestequiplab.Id and reatestequipitem.TestItemID=reatestitem.Id and reatestequipitem.LabID=reatestequiplab.LabID  and reatestequipitem.LabID=reatestitem.LabID ";
            if (where != null && where.Length > 0)
                strHQL += " and " + where;
            if (reatestitemHql != null && reatestitemHql.Length > 0)
                strHQL += " and " + reatestitemHql;
            GetDataRowRoleHQLString("reatestequipitem");
            strHQL += " and " + DataRowRoleHQLString;
            strHQL = FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaTestEquipItem> actionCount = new DaoNHBGetCountByHqlAction<ReaTestEquipItem>(strHQL);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);
            return entityList;
        }
    }
}