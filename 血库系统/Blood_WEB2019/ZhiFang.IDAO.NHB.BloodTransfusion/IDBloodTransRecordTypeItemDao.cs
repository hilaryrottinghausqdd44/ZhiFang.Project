using ZhiFang.IDAO.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.BloodTransfusion
{
	public interface IDBloodTransRecordTypeItemDao : IDBaseDao<BloodTransRecordTypeItem, long>
	{
		EntityList<BloodTransRecordTypeItem> SearchBloodTransRecordTypeItemOfLeftJoinByHQL(string strHqlWhere, string sort, int page, int limit);
	} 
}