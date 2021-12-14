
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
    public class BReaMonthUsageStatisticsDtl : BaseBLL<ReaMonthUsageStatisticsDtl>, ZhiFang.IBLL.ReagentSys.Client.IBReaMonthUsageStatisticsDtl
    {
        public IList<ReaMonthUsageStatisticsDtl> SearchReaMonthUsageStatisticsDtlListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit)
        {
            IList<ReaMonthUsageStatisticsDtl> entityList = new List<ReaMonthUsageStatisticsDtl>();
            entityList = ((IDReaMonthUsageStatisticsDtlDao)base.DBDao).SearchReaMonthUsageStatisticsDtlListByAllJoinHql(where, reaGoodsHql, sort, page, limit);
            return entityList;
        }
        public EntityList<ReaMonthUsageStatisticsDtl> SearchReaMonthUsageStatisticsEntityListDtlByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit)
        {
            EntityList<ReaMonthUsageStatisticsDtl> entityList = new EntityList<ReaMonthUsageStatisticsDtl>();
            entityList = ((IDReaMonthUsageStatisticsDtlDao)base.DBDao).SearchReaMonthUsageStatisticsEntityListDtlByAllJoinHql(where, reaGoodsHql, sort, page, limit);
            return entityList;
        }

    }
}