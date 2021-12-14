using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.NHB.ReagentSys.Client;

namespace ZhiFang.DAO.NHB.ReagentSys.Client
{
    public class ReaStorageGoodsLinkDao : BaseDaoNHB<ReaStorageGoodsLink, long>, IDReaStorageGoodsLinkDao
    {
        public IList<ReaStorageGoodsLink> SearchReaStorageGoodsLinkListByAllJoinHql(string where, string storageHql, string placeHql, string reaGoodsHql, int page, int limit, string sort)
        {
            IList<ReaStorageGoodsLink> entityList = new List<ReaStorageGoodsLink>();
            StringBuilder sqlHql = new StringBuilder();
            if (!string.IsNullOrEmpty(placeHql))
            {
                sqlHql.Append(" select new ReaStorageGoodsLink(reastoragegoodslink,reastorage,reaplace,reagoods) from ReaStorageGoodsLink reastoragegoodslink,ReaStorage reastorage,ReaPlace reaplace,ReaGoods reagoods  where reastoragegoodslink.GoodsID=reagoods.Id and  reastoragegoodslink.StorageID=reastorage.Id and  reastoragegoodslink.PlaceID=reaplace.Id ");
            }
            else
            {
                sqlHql.Append(" select new ReaStorageGoodsLink(reastoragegoodslink,reastorage,reagoods) from ReaStorageGoodsLink reastoragegoodslink,ReaStorage reastorage,ReaGoods reagoods  where reastoragegoodslink.GoodsID=reagoods.Id and  reastoragegoodslink.StorageID=reastorage.Id ");
            }

            if (!string.IsNullOrEmpty(storageHql))
            {
                sqlHql.Append(" and " + storageHql);
            }
            if (!string.IsNullOrEmpty(placeHql))
            {
                sqlHql.Append(" and " + placeHql);
            }
            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                sqlHql.Append(" and " + reaGoodsHql);
            }
            if (!string.IsNullOrEmpty(where))
            {
                sqlHql.Append(" and " + where);
            }
            if (!string.IsNullOrEmpty(sort))
                sqlHql.Append(" order by " + sort);
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
            GetDataRowRoleHQLString("ReaStorageGoodsLink");//"ReaStorageGoodsLink"
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            hql = FilterMacroCommand(hql);//宏命令过滤

            DaoNHBSearchByHqlAction<List<ReaStorageGoodsLink>, ReaStorageGoodsLink> action = new DaoNHBSearchByHqlAction<List<ReaStorageGoodsLink>, ReaStorageGoodsLink>(hql, start1, count1);
            var list = this.HibernateTemplate.Execute<List<ReaStorageGoodsLink>>(action);

            if (list != null)
            {
                entityList = list;
            }

            return entityList;
        }
        public EntityList<ReaStorageGoodsLink> SearchReaStorageGoodsLinkEntityListByAllJoinHql(string where, string storageHql, string placeHql, string reaGoodsHql, int page, int limit, string sort)
        {
            EntityList<ReaStorageGoodsLink> entityList = new EntityList<ReaStorageGoodsLink>();
            entityList.list = new List<ReaStorageGoodsLink>();

            var list = SearchReaStorageGoodsLinkListByAllJoinHql(where, storageHql, placeHql, reaGoodsHql, page, limit, sort);
            if (list != null)
            {
                entityList.list = list;
            }
            else
            {
                return entityList;
            }

            StringBuilder sqlHql = new StringBuilder();
            if (!string.IsNullOrEmpty(placeHql))
            {
                sqlHql.Append(" select count(DISTINCT reastoragegoodslink.Id) from ReaStorageGoodsLink reastoragegoodslink,ReaStorage reastorage,ReaPlace reaplace,ReaGoods reagoods  where reastoragegoodslink.GoodsID=reagoods.Id and  reastoragegoodslink.StorageID=reastorage.Id and  reastoragegoodslink.PlaceID=reaplace.Id ");
            }
            else
            {
                sqlHql.Append(" select count(DISTINCT reastoragegoodslink.Id) from ReaStorageGoodsLink reastoragegoodslink,ReaStorage reastorage,ReaGoods reagoods  where reastoragegoodslink.GoodsID=reagoods.Id and  reastoragegoodslink.StorageID=reastorage.Id ");
            }

            if (!string.IsNullOrEmpty(storageHql))
            {
                sqlHql.Append(" and " + storageHql);
            }
            if (!string.IsNullOrEmpty(placeHql))
            {
                sqlHql.Append(" and " + placeHql);
            }
            if (!string.IsNullOrEmpty(reaGoodsHql))
            {
                sqlHql.Append(" and " + reaGoodsHql);
            }
            if (!string.IsNullOrEmpty(where))
            {
                sqlHql.Append(" and " + where);
            }
            GetDataRowRoleHQLString("ReaStorageGoodsLink");//
            string hql = sqlHql.Append(" and " + DataRowRoleHQLString).ToString();
            hql = FilterMacroCommand(hql);//宏命令过滤

            DaoNHBGetCountByHqlAction<ReaStorageGoodsLink> actionCount = new DaoNHBGetCountByHqlAction<ReaStorageGoodsLink>(hql);
            entityList.count = this.HibernateTemplate.Execute<int>(actionCount);
            return entityList;
        }
    }
}