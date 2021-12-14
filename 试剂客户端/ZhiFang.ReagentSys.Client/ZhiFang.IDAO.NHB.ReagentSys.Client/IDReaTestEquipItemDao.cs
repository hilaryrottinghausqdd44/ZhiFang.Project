using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;
using System.Collections.Generic;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaTestEquipItemDao : IDBaseDao<ReaTestEquipItem, long>
    {
        IList<ReaTestEquipItem> SearchReaTestEquipItemListByJoinHql(string where, string reatestitemHql, string sort, int page, int limit);
        EntityList<ReaTestEquipItem> SearchReaTestEquipItemEntityListByJoinHql(string where, string reatestitemHql, string sort, int page, int limit);
    }
}