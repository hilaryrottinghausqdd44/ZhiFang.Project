

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsCheckDtl : IBGenericManager<ReaBmsCheckDtl>
    {
        BaseResultBool AddCheckDtlOfList(ReaBmsCheckDoc doc, IList<ReaBmsCheckDtl> dtAddList, long empID, string empName, bool isTakenFromQty);

        BaseResultBool EditCheckDtlOfList(ReaBmsCheckDoc doc, IList<ReaBmsCheckDtl> dtEditList, string fieldsDtl, long empID, string empName);
        IList<ReaBmsCheckDtl> SearchReaBmsCheckDtlListByJoinHQL(string checkHql, string checkDtlHql, string reaGoodHql, string sort, int page, int limit);
        EntityList<ReaBmsCheckDtl> SearchReaBmsCheckDtlEntityListByJoinHQL(string checkHql, string checkDtlHql, string reaGoodHql, string sort, int page, int limit);
    }
}