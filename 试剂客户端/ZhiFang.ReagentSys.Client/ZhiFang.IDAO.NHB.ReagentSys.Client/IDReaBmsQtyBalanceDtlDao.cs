using ZhiFang.IDAO.Base;
using ZhiFang.Entity.ReagentSys.Client;
using System.Collections.Generic;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaBmsQtyBalanceDtlDao : IDBaseDao<ReaBmsQtyBalanceDtl, long>
    {
        /// <summary>
        /// 按库存结转明细条件及机构货品条件获取库存结转明细信息
        /// </summary>
        /// <param name="balanceDtlHql"></param>
        /// <param name="reaGoodHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsQtyBalanceDtl> SearchListByReaGoodHQL(string balanceDtlHql, string reaGoodHql, string sort, int page, int limit);
    }
}