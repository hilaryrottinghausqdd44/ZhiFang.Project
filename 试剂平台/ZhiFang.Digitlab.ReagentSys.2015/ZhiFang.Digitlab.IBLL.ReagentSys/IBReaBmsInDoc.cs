using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaStoreIn;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsInDoc : IBGenericManager<ReaBmsInDoc>
    {
        BaseResultDataValue AddReaBmsInDocAndDtl(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> dtAddList, long docConfirmID, string dtlDocConfirmIDStr, string codeScanningMode, long empID, string empName);
    }
}