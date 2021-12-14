

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.IBLL;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsReqDtl : IBGenericManager<ReaBmsReqDtl>
    {
        BaseResultBool AddDtList(IList<ReaBmsReqDtl> dtAddList, ReaBmsReqDoc reaBmsReqDoc, long empID, string empName);
        BaseResultBool EditDtList(IList<ReaBmsReqDtl> dtEditList, ReaBmsReqDoc reaBmsReqDoc);

    }
}