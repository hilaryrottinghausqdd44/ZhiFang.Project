

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaMonthUsageStatisticsDtl : IBGenericManager<ReaMonthUsageStatisticsDtl>
    {
        IList<ReaMonthUsageStatisticsDtl> SearchReaMonthUsageStatisticsDtlListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit);
        EntityList<ReaMonthUsageStatisticsDtl> SearchReaMonthUsageStatisticsEntityListDtlByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit);
    }
}