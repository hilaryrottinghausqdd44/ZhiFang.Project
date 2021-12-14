

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.WebAssist;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.WebAssist
{
    /// <summary>
    ///
    /// </summary>
    public interface IBGKDeptAutoCheckLink : IBGenericManager<GKDeptAutoCheckLink>
    {
        /// <summary>
        /// 科室自动核收关系维护时,获取待选择的科室信息(HQL)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="where"></param>
        /// <param name="linkWhere"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<Department> SearchDepartmentByLinkHQL(int page, int limit, string where, string linkWhere, string sort);

    }
}