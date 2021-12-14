using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaBmsQtyDtlOperationDao : IDBaseDao<ReaBmsQtyDtlOperation, long>
    {
        IList<ReaBmsQtyDtlOperation> SearchReaBmsQtyDtlOperationListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit);
        EntityList<ReaBmsQtyDtlOperation> SearchReaBmsQtyDtlOperationEntityListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit);
    }
}