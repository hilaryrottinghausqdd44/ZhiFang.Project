using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;
using System.Collections.Generic;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
	public interface IDReaEquipReagentLinkDao : IDBaseDao<ReaEquipReagentLink, long>
	{
        IList<ReaEquipReagentLink> SearchNewListByHQL(string where, string sort, int page, int limit);
        EntityList<ReaEquipReagentLink> SearchNewEntityListByHQL(string where, string sort, int page, int limit);
    } 
}