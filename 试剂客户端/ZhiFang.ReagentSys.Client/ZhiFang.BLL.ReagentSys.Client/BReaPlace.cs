using ZhiFang.BLL.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.IDAO.NHB.ReagentSys.Client;
using System.Collections.Generic;

namespace ZhiFang.BLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public class BReaPlace : BaseBLL<ReaPlace>, ZhiFang.IBLL.ReagentSys.Client.IBReaPlace
    {
        public IList<ReaPlace> SearchReaPlaceListByPlaceAndLinHQL(string placeHql, string linkHql, string sort, int page, int count)
        {
            if (string.IsNullOrEmpty(placeHql) && string.IsNullOrEmpty(linkHql))
            {
                return new List<ReaPlace>();
            }
            return ((IDReaPlaceDao)base.DBDao).SearchReaPlaceListByPlaceAndLinHQL(placeHql, linkHql, sort, page, count);
        }
        /// <summary>
        /// 根据员工权限获取货架信息
        /// </summary>
        /// <param name="placeHql"></param>
        /// <param name="linkHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public EntityList<ReaPlace> SearchEntityListByPlaceeAndLinHQL(string placeHql, string linkHql, string sort, int page, int count)
        {
            if (string.IsNullOrEmpty(placeHql) && string.IsNullOrEmpty(linkHql))
            {
                return new EntityList<ReaPlace>();
            }
            return ((IDReaPlaceDao)base.DBDao).SearchEntityListByPlaceeAndLinHQL(placeHql, linkHql, sort, page, count);
        }
    }
}