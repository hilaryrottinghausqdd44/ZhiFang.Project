using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaEquipReagentLinkDao : BaseDaoNHB<ReaEquipReagentLink, long>, IDReaEquipReagentLinkDao
    {
        public IList<ReaEquipReagentLink> SearchNewListByHQL(string where, string sort, int page, int limit)
        {
            IList<ReaEquipReagentLink> entityList = new List<ReaEquipReagentLink>();
            string hql = " select new ReaEquipReagentLink(reaequipreagentlink,reatestequiplab,reagoods) from ReaEquipReagentLink reaequipreagentlink,ReaTestEquipLab reatestequiplab,ReaGoods reagoods where reaequipreagentlink.TestEquipID=reatestequiplab.Id and reaequipreagentlink.GoodsID=reagoods.Id and reaequipreagentlink.LabID=reatestequiplab.LabID and reaequipreagentlink.LabID=reagoods.LabID ";
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
            GetDataRowRoleHQLString("reaequipreagentlink");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaEquipReagentLink>, ReaEquipReagentLink> action = new DaoNHBSearchByHqlAction<List<ReaEquipReagentLink>, ReaEquipReagentLink>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaEquipReagentLink>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }
        public EntityList<ReaEquipReagentLink> SearchNewEntityListByHQL(string where, string sort, int page, int limit)
        {
            EntityList<ReaEquipReagentLink> entityList = new EntityList<ReaEquipReagentLink>();
            entityList.list = new List<ReaEquipReagentLink>();

            var list = SearchNewListByHQL(where, sort, page, limit);
            if (list != null)
            {
                entityList.list = list;
            }
            else
            {
                return entityList;
            }
            string strHQL = " select count(reaequipreagentlink.Id) from ReaEquipReagentLink reaequipreagentlink,ReaTestEquipLab reatestequiplab,ReaGoods reagoods where reaequipreagentlink.TestEquipID=reatestequiplab.Id and reaequipreagentlink.GoodsID=reagoods.Id  and reaequipreagentlink.LabID=reatestequiplab.LabID and reaequipreagentlink.LabID=reagoods.LabID";
            if (!string.IsNullOrEmpty(where))
            {
                strHQL = strHQL + " and " + where;
            }
            GetDataRowRoleHQLString("reaequipreagentlink");
            strHQL += " and " + DataRowRoleHQLString;
            strHQL = FilterMacroCommand(strHQL);//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaEquipReagentLink> actionCount = new DaoNHBGetCountByHqlAction<ReaEquipReagentLink>(strHQL);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);
            return entityList;
        }
    }
}