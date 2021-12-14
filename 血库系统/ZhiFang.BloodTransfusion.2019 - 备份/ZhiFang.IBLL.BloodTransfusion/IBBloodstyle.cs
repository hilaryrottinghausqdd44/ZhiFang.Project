

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBloodstyle : IBGenericManager<Bloodstyle, int>
    {
        IList<Bloodstyle> SearchBloodstyleListByJoinHql(string where, string bloodclassHql, string sort, int page, int limit);
        EntityList<Bloodstyle> SearchBloodstyleEntityListByJoinHql(string where, string bloodclassHql, string sort, int page, int limit);
    }
}