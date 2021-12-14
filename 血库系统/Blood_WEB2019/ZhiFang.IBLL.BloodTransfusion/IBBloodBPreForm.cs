

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBloodBPreForm : IBGenericManager<BloodBPreForm, string>
    {
        /// <summary>
        /// (输血申请综合查询)按申请单号获取血袋入库VO信息
        /// </summary>
        /// <param name="reqFormId"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<BReqComplexOfInInfoVO> SearchBReqComplexOfInInfoVOByBReqFormID(string reqFormId, string sort, int page, int limit);
    }
}