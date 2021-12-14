using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaGoodsBarcodeOperationDao : IDBaseDao<ReaGoodsBarcodeOperation, long>
    {
        BaseResultBool UpdatePrintCount(IList<long> boxList);

    }
}