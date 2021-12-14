using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;
using System.Collections.Generic;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
	public interface IDReaStorageGoodsLinkDao : IDBaseDao<ReaStorageGoodsLink, long>
	{
        IList<ReaStorageGoodsLink> SearchReaStorageGoodsLinkListByAllJoinHql(string where, string storageHql, string placeHql, string reaGoodsHql, int page, int limit, string sort);
        EntityList<ReaStorageGoodsLink> SearchReaStorageGoodsLinkEntityListByAllJoinHql(string where, string storageHql, string placeHql, string reaGoodsHql, int page, int limit, string sort);
    } 
}