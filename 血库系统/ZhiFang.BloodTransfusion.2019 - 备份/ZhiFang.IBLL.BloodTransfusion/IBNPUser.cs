

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBNPUser : IBGenericManager<NPUser, int>
    {
        /// <summary>
        /// 获取封装处理后的NPUser信息
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<RBACUser> SearchRBACUserOfNPUserByHQL(string where, string sort, int page, int limit);

    }
}