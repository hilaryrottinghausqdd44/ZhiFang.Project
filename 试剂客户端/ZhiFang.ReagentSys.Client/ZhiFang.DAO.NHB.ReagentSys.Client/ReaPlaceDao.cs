using System.Collections.Generic;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaPlaceDao : BaseDaoNHB<ReaPlace, long>, IDReaPlaceDao
    {
        public IList<ReaPlace> SearchReaPlaceListByPlaceAndLinHQL(string placeHql, string linkHql, string sort, int page, int limit)
        {
            IList<ReaPlace> entityList = new List<ReaPlace>();
            if (string.IsNullOrEmpty(placeHql) && string.IsNullOrEmpty(linkHql))
            {
                return entityList;
            }
            string hql = " select DISTINCT reaplace from ReaUserStorageLink reauserstoragelink,ReaPlace reaplace where reauserstoragelink.PlaceID is not null and  reauserstoragelink.PlaceID=reaplace.Id and reauserstoragelink.LabID=reaplace.LabID ";
            if (!string.IsNullOrEmpty(placeHql))
            {
                hql = hql + " and " + placeHql;
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
            GetDataRowRoleHQLString("reaplace");
            hql += " and " + DataRowRoleHQLString;
            if (!string.IsNullOrEmpty(sort))
                hql = hql + " order by " + sort;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBSearchByHqlAction<List<ReaPlace>, ReaPlace> action = new DaoNHBSearchByHqlAction<List<ReaPlace>, ReaPlace>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaPlace>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }
        /// <summary>
        /// 根据员工权限获取货架信息
        /// </summary>
        /// <param name="placeHql"></param>
        /// <param name="linkHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<ReaPlace> SearchEntityListByPlaceeAndLinHQL(string placeHql, string linkHql, string sort, int page, int count)
        {
            EntityList<ReaPlace> entityList = new EntityList<ReaPlace>();
            if (string.IsNullOrEmpty(placeHql) && string.IsNullOrEmpty(linkHql))
            {
                return entityList;
            }
            entityList.list = SearchReaPlaceListByPlaceAndLinHQL(placeHql, linkHql, sort, page, count);
            string hql = " select count(DISTINCT reaplace.Id) from ReaUserStorageLink reauserstoragelink,ReaPlace reaplace where reauserstoragelink.PlaceID is not null and reauserstoragelink.PlaceID=reaplace.Id and reauserstoragelink.LabID=reaplace.LabID ";
            if (!string.IsNullOrEmpty(placeHql))
            {
                hql = hql + " and " + placeHql;
            }
            if (!string.IsNullOrEmpty(linkHql))
            {
                hql = hql + " and " + linkHql;
            }
            GetDataRowRoleHQLString("reaplace");
            hql += " and " + DataRowRoleHQLString;
            hql = FilterMacroCommand(hql);//宏命令过滤
            DaoNHBGetCountByHqlAction<ReaPlace> actionCount = new DaoNHBGetCountByHqlAction<ReaPlace>(hql);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);

            return entityList;
        }
    }
}