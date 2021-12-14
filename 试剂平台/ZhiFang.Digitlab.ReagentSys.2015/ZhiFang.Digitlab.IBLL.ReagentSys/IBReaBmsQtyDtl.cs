

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
    public interface IBReaBmsQtyDtl : IBGenericManager<ReaBmsQtyDtl>
    {
        /// <summary>
        /// 获取采购申请货品库存数量
        /// </summary>
        /// <param name="idStr">格式为"货品Id:供应商Id,货品Id2:供应商Id2"</param>
        /// <param name="goodIdStr">格式为"货品Id,货品Id2"</param>
        /// <returns></returns>
        IList<ReaGoodsCurrentQtyVO> SearchReaGoodsCurrentQtyByGoodIdStr(string idStr, string goodIdStr);
    }
}