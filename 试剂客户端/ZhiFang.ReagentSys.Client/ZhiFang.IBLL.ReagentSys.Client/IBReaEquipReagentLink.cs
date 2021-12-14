

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaEquipReagentLink : IBGenericManager<ReaEquipReagentLink>
    {
        IList<ReaEquipReagentLink> SearchNewListByHQL(string where, string sort, int page, int limit);
        EntityList<ReaEquipReagentLink> SearchNewEntityListByHQL(string where, string sort, int page, int limit);
        /// <summary>
        /// 获取试剂与仪器的基本信息(按试剂进行分组)
        /// </summary>
        /// <returns></returns>
        IList<ReaEquipReagentLinkVO> SearchReaEquipReagentLinkVOList(IList<ReaTestEquipLab> testEquipLabList, string deptName);
    }
}