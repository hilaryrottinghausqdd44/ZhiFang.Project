

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaTestEquipItem : IBGenericManager<ReaTestEquipItem>
    {
        /// <summary>
        /// 客户端同步LIS的仪器项目关系信息
        /// </summary>
        /// <returns></returns>
        BaseResultBool SaveSyncLisReaTestEquipItemInfo(string equipId);
        IList<ReaTestEquipItem> SearchNewListByHQL(string where, string reatestitemHql, string sort, int page, int limit);
        EntityList<ReaTestEquipItem> SearchReaTestEquipItemEntityListByJoinHql(string where, string reatestitemHql, string sort, int page, int limit);
    }
}