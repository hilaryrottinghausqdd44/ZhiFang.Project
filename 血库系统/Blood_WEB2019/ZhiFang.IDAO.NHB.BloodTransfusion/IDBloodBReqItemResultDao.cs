using ZhiFang.IDAO.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.Base;
using System.Collections.Generic;

namespace ZhiFang.IDAO.NHB.BloodTransfusion
{
    public interface IDBloodBReqItemResultDao : IDBaseDao<BloodBReqItemResult, long>
    {
        IList<BloodBReqItemResult> SearchBloodBReqItemResultListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit);
        EntityList<BloodBReqItemResult> SearchBloodBReqItemResultEntityListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit);

    }
}