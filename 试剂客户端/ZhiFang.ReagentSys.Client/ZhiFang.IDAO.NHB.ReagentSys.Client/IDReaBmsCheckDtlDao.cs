using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;
using System.Collections.Generic;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
	public interface IDReaBmsCheckDtlDao : IDBaseDao<ReaBmsCheckDtl, long>
	{
        IList<ReaBmsCheckDtl> SearchReaBmsCheckDtlListByJoinHQL(string checkHql, string checkDtlHql, string reaGoodHql, string sort, int page, int limit);
        EntityList<ReaBmsCheckDtl> SearchReaBmsCheckDtlEntityListByJoinHQL(string checkHql, string checkDtlHql, string reaGoodHql, string sort, int page, int limit);
    } 
}