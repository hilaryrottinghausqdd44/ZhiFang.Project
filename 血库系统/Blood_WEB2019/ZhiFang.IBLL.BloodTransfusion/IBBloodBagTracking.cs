

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    /// 血袋跟踪统计
    /// </summary>
    public interface IBBloodBagTracking : IBGenericManager<BloodBagTrackingVO>
    {
        /// <summary>
        /// 按申请单号进行血袋跟踪统计
        /// </summary>
        /// <param name="reqId"></param>
        /// <returns></returns>
        BaseResultDataValue GetBReqComplexOfMergeVO(string reqId);
    }
}