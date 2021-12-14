

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsQtyBalanceDtl : IBGenericManager<ReaBmsQtyBalanceDtl>
    {
        BaseResultDataValue AddReaBmsQtyBalanceDtl(IList<ReaBmsQtyBalanceDtl> dtAddList, long empID, string empName);

    }
}