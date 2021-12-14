

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaStorageGoodsLink : IBGenericManager<ReaStorageGoodsLink>
    {
        IList<ReaStorageGoodsLink> SearchReaStorageGoodsLinkListByAllJoinHql(string where, string storageHql, string placeHql, string reaGoodsHql, int page, int limit, string sort);
        EntityList<ReaStorageGoodsLink> SearchReaStorageGoodsLinkEntityListByAllJoinHql(string where, string storageHql, string placeHql, string reaGoodsHql, int page, int limit, string sort);
    }
}