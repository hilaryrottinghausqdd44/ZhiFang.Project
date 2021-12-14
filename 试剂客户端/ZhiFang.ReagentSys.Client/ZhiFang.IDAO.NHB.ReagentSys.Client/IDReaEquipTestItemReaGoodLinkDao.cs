using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;
using System.Collections.Generic;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaEquipTestItemReaGoodLinkDao : IDBaseDao<ReaEquipTestItemReaGoodLink, long>
    {
        IList<ReaEquipTestItemReaGoodLink> SearchNewListByHQL(string where, string sort, int page, int limit);
        EntityList<ReaEquipTestItemReaGoodLink> SearchNewEntityListByHQL(string where, string sort, int page, int limit);
    }
}