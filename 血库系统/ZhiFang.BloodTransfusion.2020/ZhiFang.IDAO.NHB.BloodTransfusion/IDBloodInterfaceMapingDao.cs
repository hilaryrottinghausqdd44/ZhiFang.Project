using ZhiFang.IDAO.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.BloodTransfusion
{
	public interface IDBloodInterfaceMapingDao : IDBaseDao<BloodInterfaceMaping, long>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="where"></param>
		/// <param name="sort"></param>
		/// <param name="page"></param>
		/// <param name="limit"></param>
		/// <returns></returns>
		EntityList<BDictMapingVO> SearchInterfaceMapingJoinBDictByHql(string where, string sort, int page, int limit);
	} 
}