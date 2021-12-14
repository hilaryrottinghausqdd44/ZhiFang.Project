

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
	/// <summary>
	///
	/// </summary>
	public  interface IBReaUserStorageLink : IBGenericManager<ReaUserStorageLink>
	{
        /// <summary>
        /// 依传入的条件获取库房人员权限关系的所属库房ID集合信息
        /// </summary>
        /// <param name="hqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<long> SearchStorageIDListByHQL(string hqlWhere, string sort, int page, int limit);
    }
}