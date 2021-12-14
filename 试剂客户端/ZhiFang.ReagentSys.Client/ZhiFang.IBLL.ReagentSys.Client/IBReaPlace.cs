using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaPlace : IBGenericManager<ReaPlace>
    {
        IList<ReaPlace> SearchReaPlaceListByPlaceAndLinHQL(string placeHql, string linkHql, string sort, int page, int limit);
        /// <summary>
        /// 根据员工权限获取货架信息
        /// </summary>
        /// <param name="placeHql"></param>
        /// <param name="linkHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<ReaPlace> SearchEntityListByPlaceeAndLinHQL(string placeHql, string linkHql, string sort, int page, int count);
    }
}