using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaEquipTestItemReaGoodLinkDao : BaseDaoNHB<ReaEquipTestItemReaGoodLink, long>, IDReaEquipTestItemReaGoodLinkDao
    {
        public IList<ReaEquipTestItemReaGoodLink> SearchNewListByHQL(string where, string sort, int page, int limit)
        {
            IList<ReaEquipTestItemReaGoodLink> entityList = new List<ReaEquipTestItemReaGoodLink>();
            //reabmsindtl.UnitMemo,
            string hql = " select new ReaEquipTestItemReaGoodLink(reaequiptestitemreagoodlink,reatestequiplab.LisCode as equipCode,reatestequiplab.CName as equipCName,reatestitem.LisCode as testItemCode,reatestitem.SName as testItemSName,reatestitem.CName as testItemCName,reagoods.ReaGoodsNo as reaGoodsNo,reagoods.CName as goodsCName,reagoods.SName as goodsSName,reagoods.UnitName as unitName,reagoods.UnitMemo as unitMemo) from ReaEquipTestItemReaGoodLink reaequiptestitemreagoodlink,ReaTestEquipLab reatestequiplab,ReaTestItem reatestitem,ReaGoods reagoods where reaequiptestitemreagoodlink.TestEquipID=reatestequiplab.Id and reaequiptestitemreagoodlink.TestItemID=reatestitem.Id and reaequiptestitemreagoodlink.GoodsID=reagoods.Id and reaequiptestitemreagoodlink.LabID=reatestequiplab.LabID and reaequiptestitemreagoodlink.LabID=reagoods.LabID and reaequiptestitemreagoodlink.LabID=reatestitem.LabID ";
            if (!string.IsNullOrEmpty(where))
            {
                hql = hql + " and " + where;
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
            GetDataRowRoleHQLString("reaequiptestitemreagoodlink");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaEquipTestItemReaGoodLink>, ReaEquipTestItemReaGoodLink> action = new DaoNHBSearchByHqlAction<List<ReaEquipTestItemReaGoodLink>, ReaEquipTestItemReaGoodLink>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaEquipTestItemReaGoodLink>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }
        public EntityList<ReaEquipTestItemReaGoodLink> SearchNewEntityListByHQL(string where, string sort, int page, int limit)
        {
            EntityList<ReaEquipTestItemReaGoodLink> entityList = new EntityList<ReaEquipTestItemReaGoodLink>();
            entityList.list = new List<ReaEquipTestItemReaGoodLink>();

            var list = SearchNewListByHQL(where, sort, page, limit);
            if (list != null)
            {
                entityList.list = list;
            }
            else
            {
                return entityList;
            }
            string strHQL = "select count(*) from ReaEquipTestItemReaGoodLink reaequiptestitemreagoodlink where 1=1 ";
            if (where != null && where.Length > 0)
                strHQL += "and " + where;
            GetDataRowRoleHQLString("reaequiptestitemreagoodlink");
            strHQL += " and " + DataRowRoleHQLString;
            strHQL = FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaEquipTestItemReaGoodLink> actionCount = new DaoNHBGetCountByHqlAction<ReaEquipTestItemReaGoodLink>(strHQL);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);
            return entityList;
        }
    }
}