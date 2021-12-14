using ZhiFang.IDAO.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.Base;
using System.Collections.Generic;

namespace ZhiFang.IDAO.NHB.BloodTransfusion
{
    public interface IDBloodBReqFormResultDao : IDBaseDao<BloodBReqFormResult, long>
    {
        IList<BloodBReqFormResult> SearchBloodBReqFormResultListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit);
        EntityList<BloodBReqFormResult> SearchBloodBReqFormResultEntityListByJoinHql(string where, string bloodbtestitemHql, string sort, int page, int limit);

    }
}