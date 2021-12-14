using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.LabStar
{
    /// <summary>
    ///
    /// </summary>
    public interface IBLBOrderModel : IBGenericManager<LBOrderModel>
    {
        List<tree> GetOrderModelTree(string OrderModelTypeID, string ItemWhere);
    }
}