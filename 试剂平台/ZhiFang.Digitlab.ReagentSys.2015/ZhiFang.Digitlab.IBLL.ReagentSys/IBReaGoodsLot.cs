using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaGoodsLot : IBGenericManager<ReaGoodsLot>
    {
        BaseResultBool AddAndValid();
        BaseResultBool EditValid();
    }
}