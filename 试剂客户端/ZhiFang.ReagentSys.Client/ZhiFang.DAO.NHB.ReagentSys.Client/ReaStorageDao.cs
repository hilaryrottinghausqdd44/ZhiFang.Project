
using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaStorageDao : BaseDaoNHB<ReaStorage, long>, IDReaStorageDao
    {
        public IList<ReaStorage> SearchReaStorageListByStorageAndLinHQL(string storageHql, string linkHql, string operType, string sort, int page, int limit)
        {
            IList<ReaStorage> entityList = new List<ReaStorage>();
            if (string.IsNullOrEmpty(storageHql) && string.IsNullOrEmpty(linkHql))
            {
                return entityList;
            }
            string hql = " select DISTINCT reastorage from ReaUserStorageLink reauserstoragelink,ReaStorage reastorage where reauserstoragelink.StorageID is not null and reauserstoragelink.StorageID=reastorage.Id and reauserstoragelink.LabID=reastorage.LabID ";
            if (!string.IsNullOrEmpty(storageHql))
            {
                hql = hql + " and " + storageHql;
            }
            if (!string.IsNullOrEmpty(linkHql))
            {
                hql = hql + " and " + linkHql;
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
            GetDataRowRoleHQLString("reastorage");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaStorage>, ReaStorage> action = new DaoNHBSearchByHqlAction<List<ReaStorage>, ReaStorage>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaStorage>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }
        /// <summary>
        /// 根据员工权限获取库房信息
        /// </summary>
        /// <param name="storageHql"></param>
        /// <param name="linkHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<ReaStorage> SearchEntityListByStorageAndLinHQL(string storageHql, string linkHql, string operType, string sort, int page, int count)
        {
            EntityList<ReaStorage> entityList = new EntityList<ReaStorage>();
            if (string.IsNullOrEmpty(storageHql) && string.IsNullOrEmpty(linkHql))
            {
                return entityList;
            }
            entityList.list = SearchReaStorageListByStorageAndLinHQL(storageHql, linkHql, operType, sort, page, count);
            string hql = " select count(DISTINCT reastorage.Id) from ReaUserStorageLink reauserstoragelink,ReaStorage reastorage where reauserstoragelink.StorageID is not null and  reauserstoragelink.StorageID=reastorage.Id and reauserstoragelink.LabID=reastorage.LabID ";
            if (!string.IsNullOrEmpty(storageHql))
            {
                hql = hql + " and " + storageHql;
            }
            if (!string.IsNullOrEmpty(linkHql))
            {
                hql = hql + " and " + linkHql;
            }
            GetDataRowRoleHQLString("reastorage");
            hql += " and " + DataRowRoleHQLString;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaStorage> actionCount = new DaoNHBGetCountByHqlAction<ReaStorage>(hql);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);

            return entityList;
        }
    }
}