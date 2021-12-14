using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.Base;
using System.Collections.Generic;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaMonthUsageStatisticsDtlDao : IDBaseDao<ReaMonthUsageStatisticsDtl, long>
    {
        IList<ReaMonthUsageStatisticsDtl> SearchReaMonthUsageStatisticsDtlListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit);
        EntityList<ReaMonthUsageStatisticsDtl> SearchReaMonthUsageStatisticsEntityListDtlByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit);
    }
}