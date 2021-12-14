
using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Text;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaStorageGoodsLink : BaseBLL<ReaStorageGoodsLink>, ZhiFang.IBLL.ReagentSys.Client.IBReaStorageGoodsLink
    {
        public override bool Add()
        {
            string hql = " reastoragegoodslink.StorageID=" + this.Entity.StorageID + " and reastoragegoodslink.GoodsID = " + this.Entity.GoodsID;
            if (this.Entity.PlaceID.HasValue)
                hql = hql + " and reastoragegoodslink.PlaceID = " + this.Entity.PlaceID;
            int count = ((IDReaStorageGoodsLinkDao)base.DBDao).GetListCountByHQL(hql);
            if (count > 0)
            {
                ZhiFang.Common.Log.Log.Debug("新增库房货架试剂关系提示:" + "库房ID为:" + this.Entity.StorageID + "" + "货架ID为:" + this.Entity.PlaceID + "机构货品ID为:" + this.Entity.GoodsID + ",关系已存在,请不要重复添加!");
                return true;
            }
            bool a = DBDao.Save(this.Entity);
            return a;
        }
        public IList<ReaStorageGoodsLink> SearchReaStorageGoodsLinkListByAllJoinHql(string where, string storageHql, string placeHql, string reaGoodsHql, int page, int limit, string sort)
        {
            IList<ReaStorageGoodsLink> entityList = new List<ReaStorageGoodsLink>();
            entityList = ((IDReaStorageGoodsLinkDao)base.DBDao).SearchReaStorageGoodsLinkListByAllJoinHql(where, storageHql, placeHql, reaGoodsHql, page, limit, sort);
            return entityList;
        }
        public EntityList<ReaStorageGoodsLink> SearchReaStorageGoodsLinkEntityListByAllJoinHql(string where, string storageHql, string placeHql, string reaGoodsHql, int page, int limit, string sort)
        {
            EntityList<ReaStorageGoodsLink> entityList = new EntityList<ReaStorageGoodsLink>();
            entityList = ((IDReaStorageGoodsLinkDao)base.DBDao).SearchReaStorageGoodsLinkEntityListByAllJoinHql(where, storageHql, placeHql, reaGoodsHql, page, limit, sort);
            return entityList;
        }
    }
}