
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
	public interface IDReaBmsReqDtlDao : IDBaseDao<ReaBmsReqDtl, long>
	{
        EntityList<ReaBmsReqDtl> GetReaBmsReqDtlListByHQL(string strHqlWhere, string Order, int start, int count);

    } 
}