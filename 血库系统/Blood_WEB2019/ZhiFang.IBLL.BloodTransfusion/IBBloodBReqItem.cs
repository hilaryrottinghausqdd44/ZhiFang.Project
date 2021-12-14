

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBloodBReqItem : IBGenericManager<BloodBReqItem>
    {
        IList<BloodBReqItem> SearchBloodBReqItemListByJoinHql(string where, string docHql, string bloodstyleHql, string sort, int page, int limit);
        EntityList<BloodBReqItem> SearchBloodBReqItemEntityListByJoinHql(string where, string docHql, string bloodstyleHql, string sort, int page, int limit);

        BaseResultDataValue AddBReqItemList(BloodBReqForm reqForm, IList<BloodBReqItem> addBreqItemList);
        BaseResultDataValue EditBReqItemList(BloodBReqForm reqForm, IList<BloodBReqItem> editBreqItemList);
    }
}