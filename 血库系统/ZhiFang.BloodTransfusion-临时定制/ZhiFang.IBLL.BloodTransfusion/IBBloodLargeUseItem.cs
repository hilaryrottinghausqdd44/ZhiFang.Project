

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBloodLargeUseItem : IBGenericManager<BloodLargeUseItem>
    {
        BaseResultDataValue AddBloodLargeUseItemList(BloodBReqForm reqForm, IList<BloodLargeUseItem> addLargeUseItemList);
    }
}