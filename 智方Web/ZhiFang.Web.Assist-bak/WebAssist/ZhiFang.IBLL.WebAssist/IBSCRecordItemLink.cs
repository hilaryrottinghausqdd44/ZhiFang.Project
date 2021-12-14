

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WebAssist
{
	/// <summary>
	///
	/// </summary>
	public  interface IBSCRecordItemLink : IBGenericManager<SCRecordItemLink>
	{
        /// <summary>
        /// 记录项类型选择记录项字典时,获取待选择的记录项字典信息(HQL)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="where"></param>
        /// <param name="linkWhere"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<SCRecordTypeItem> SearchSCRecordTypeItemByLinkHQL(int page, int limit, string where, string linkWhere, string sort);
    }
}