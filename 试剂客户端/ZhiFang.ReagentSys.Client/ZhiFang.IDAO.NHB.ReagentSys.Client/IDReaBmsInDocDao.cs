using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaBmsInDocDao : IDBaseDao<ReaBmsInDoc, long>
    {
        /// <summary>
        /// 根据入库主单条件及入库明细条件获取入库主单实体列表
        /// </summary>
        /// <param name="docHql">入库主单条件</param>
        /// <param name="dtlHql">入库明细条件</param>
        /// <param name="order">入库主单排序</param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<ReaBmsInDoc> SearchListByDocAndDtlHQL(string docHql, string dtlHql, string order, int page, int count);
        /// <summary>
        /// 入库移库,获取库存货品库存数大于0的入库主单信息(HQL)
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsInDoc> SearchReaBmsInDocOfQtyGEZeroByJoinHql(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit);
    }
}