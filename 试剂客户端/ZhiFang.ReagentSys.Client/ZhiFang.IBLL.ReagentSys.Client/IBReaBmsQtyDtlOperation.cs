using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsQtyDtlOperation : IBGenericManager<ReaBmsQtyDtlOperation>
    {
        BaseResultDataValue AddReaBmsQtyDtlOutOperation(ReaBmsOutDoc outDoc, ReaBmsOutDtl outDtl, ReaBmsQtyDtl qtyDtl, double curoutGoodsQty);
        BaseResultDataValue AddReaBmsQtyDtlTransferOperation(ReaBmsTransferDoc outDoc, ReaBmsTransferDtl outDtl, ReaBmsQtyDtl qtyDtl, double goodsQtyOut, long operTypeID);
        IList<ReaBmsQtyDtlOperation> SearchReaBmsQtyDtlOperationListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit);
        EntityList<ReaBmsQtyDtlOperation> SearchReaBmsQtyDtlOperationEntityListByAllJoinHql(string where, string reaGoodsHql, string sort, int page, int limit);
    }
}