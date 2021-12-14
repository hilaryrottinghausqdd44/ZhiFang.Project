

using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.BloodTransfusion
{
    /// <summary>
    ///
    /// </summary>
    public interface IBBloodChargeItemLink : IBGenericManager<BloodChargeItemLink>
    {
        void AddSCOperation(BloodChargeItemLink serverEntity, string[] arrFields, long empID, string empName);

        /// <summary>
        ///收费类型费用项目关系选择费用项目时,获取待选择的费用项目信息(HQL)
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="where"></param>
        /// <param name="linkWhere"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<BloodChargeItem> SearchBloodChargeItemByChargeItemLinkHQL(int page, int limit, string where, string linkWhere, string sort);

    }
}