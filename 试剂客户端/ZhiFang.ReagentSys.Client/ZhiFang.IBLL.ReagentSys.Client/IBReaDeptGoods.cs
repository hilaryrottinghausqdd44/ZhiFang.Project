using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaDeptGoods : IBGenericManager<ReaDeptGoods>
    {
        EntityList<ReaDeptGoods> SearchReaGoodsByHRDeptID(long deptID, string where, string sort, int page, int limit);
        /// <summary>
        /// 依部门Id获取该部门(包含子部门)下的所有部门的货品Id信息
        /// </summary>
        /// <param name="deptId"></param>
        /// <returns></returns>
        string SearchReaDeptGoodsListByHRDeptId(long deptId);

        /// <summary>
        /// 采购申请定制获取部门货品信息
        /// </summary>
        /// <param name="deptId"></param>
        /// <param name="strHqlWhere"></param>
        /// <param name="goodsQty"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<ReaDeptGoods> SearchListByDeptIdAndHQL(long deptId, string strHqlWhere, string goodsQty, string order, int page, int count);
        /// <summary>
        /// 部门货品选择机构货品时,获取待选择的机构货品信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="where"></param>
        /// <param name="linkWhere"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<ReaGoods> SearchReaGoodsListByHQL(int page, int limit, string where, string linkWhere, string sort);
    }
}