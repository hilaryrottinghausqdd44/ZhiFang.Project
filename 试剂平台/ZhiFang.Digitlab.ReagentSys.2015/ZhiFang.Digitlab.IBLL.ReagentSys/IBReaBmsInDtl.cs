using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.Response;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaGoodsScanCode;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaStoreIn;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsInDtl : IBGenericManager<ReaBmsInDtl>
    {
        /// <summary>
        /// 入库明细的保存验证判断
        /// </summary>
        /// <param name="dtAddList"></param>
        /// <param name="codeScanningMode">扫码模式:严格模式-strict;混合模式-mixing</param>
        /// <returns></returns>
        BaseResultBool AddReaBmsInDtlValid(IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode);
        BaseResultBool AddReaBmsInDtl(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> dtAddList, long empID, string empName);

    }
}