using System;
using System.Collections.Generic;
using System.Linq;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaUserStorageLinkDao : BaseDaoNHB<ReaUserStorageLink, long>, IDReaUserStorageLinkDao
    {
        public IList<long> SearchStorageIDListByHQL(string hqlWhere, string sort, int page, int limit)
        {
            IList<long> storageIDList = new List<long>();

            string hql = " select DISTINCT reauserstoragelink.StorageID from ReaUserStorageLink reauserstoragelink where1=1";

            if (!string.IsNullOrEmpty(hqlWhere))
            {
                hql = hql + " and " + hqlWhere;
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
            GetDataRowRoleHQLString("reauserstoragelink");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<long>, long> action = new DaoNHBSearchByHqlAction<List<long>, long>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<long>>(action);

            if (list != null)
            {
                storageIDList = list;
            }

            return storageIDList;
        }
    }
}