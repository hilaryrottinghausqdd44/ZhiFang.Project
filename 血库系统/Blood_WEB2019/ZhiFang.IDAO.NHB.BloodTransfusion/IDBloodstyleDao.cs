using ZhiFang.IDAO.Base;
using ZhiFang.Entity.BloodTransfusion;
using System.Collections.Generic;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.BloodTransfusion
{
    public interface IDBloodstyleDao : IDBaseDao<Bloodstyle, int>
    {
        IList<Bloodstyle> SearchBloodstyleListByJoinHql(string where, string bloodclassHql, string sort, int page, int limit);
        EntityList<Bloodstyle> SearchBloodstyleEntityListByJoinHql(string where, string bloodclassHql, string sort, int page, int limit);

    }
}