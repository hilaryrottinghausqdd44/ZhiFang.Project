using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaGoodsScanCode;

namespace ZhiFang.Digitlab.IBLL.ReagentSys
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaCenBarCodeFormat : IBGenericManager<ReaCenBarCodeFormat>
    {

        ReaGoodsScanCodeVO DecodingReaGoodsScanCodeVOOfCompIDAndSerialNo(long reaCompID, string serialNo);
    }
}