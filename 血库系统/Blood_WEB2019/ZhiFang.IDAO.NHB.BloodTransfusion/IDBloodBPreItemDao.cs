using ZhiFang.IDAO.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.BloodTransfusion
{
	public interface IDBloodBPreItemDao : IDBaseDao<BloodBPreItem, string>
	{
		EntityList<BloodBReqForm> SearchBloodBReqFormListByBBagCodeAndHql(string strHqlWhere, string scanCodeField, string bbagCode, string sort, int page, int limit);
	} 
}