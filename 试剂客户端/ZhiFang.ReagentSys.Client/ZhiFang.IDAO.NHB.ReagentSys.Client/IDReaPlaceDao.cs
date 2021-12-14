using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaPlaceDao : IDBaseDao<ReaPlace, long>
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