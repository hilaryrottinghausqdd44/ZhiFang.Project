

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBmsCenSaleDtl : IBGenericManager<BmsCenSaleDtl>
    {
        /// <summary>
        /// 获取某一供货单待验收的明细
        /// </summary>
        /// <param name="bmsCenSaleDocId"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<BmsCenSaleDtlOV> SearchListForCheckByBmsCenSaleDocId(long bmsCenSaleDocId, string Order, int page, int count);
        /// <summary>
        /// 获取某一供货单的(同一种试剂)合并处理后的明细
        /// </summary>
        /// <param name="bmsCenSaleDocId"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<BmsCenSaleDtl> SearchMergerDtListForCheckByBmsCenSaleDocId(long bmsCenSaleDocId, string sort, int page, int count);
    }
}