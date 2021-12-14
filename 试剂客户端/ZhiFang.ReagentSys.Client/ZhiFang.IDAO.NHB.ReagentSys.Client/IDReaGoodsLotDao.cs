using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaGoodsLotDao : IDBaseDao<ReaGoodsLot, long>
    {
        IList<ReaGoodsLot> SearchReaGoodsLotListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit);
        EntityList<ReaGoodsLot> SearchReaGoodsLotEntityListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit);
    }
}