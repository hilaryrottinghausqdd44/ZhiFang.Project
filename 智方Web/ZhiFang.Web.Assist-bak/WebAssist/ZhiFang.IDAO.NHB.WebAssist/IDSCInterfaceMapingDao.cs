using ZhiFang.IDAO.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.WebAssist
{
    public interface IDSCInterfaceMapingDao : IDBaseDao<SCInterfaceMaping, long>
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