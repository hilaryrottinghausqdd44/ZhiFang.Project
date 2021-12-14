using ZhiFang.IDAO.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.Base;
using System.Collections.Generic;

namespace ZhiFang.IDAO.NHB.BloodTransfusion
{
	public interface IDBloodBReqItemDao : IDBaseDao<BloodBReqItem, long>
	{
        IList<BloodBReqItem> SearchBloodBReqItemListByJoinHql(string where, string docHql, string bloodstyleHql, string sort, int page, int limit);
        EntityList<BloodBReqItem> SearchBloodBReqItemEntityListByJoinHql(string where, string docHql, string bloodstyleHql, string sort, int page, int limit);
    } 
}