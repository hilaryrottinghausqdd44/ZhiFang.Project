
using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaBmsCenOrderDtlDao : IDBaseDao<ReaBmsCenOrderDtl, long>
    {
        IList<ReaBmsCenOrderDtl> SearchReaBmsCenOrderDtlSummaryByHQL(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit);

        /// <summary>
        /// 根据订货单ID查询订单明细
        /// </summary>
        /// <param name="orderDocId">订货总单ID</param>
        /// <returns></returns>
        IList<ReaBmsCenOrderDtl> GetReaBmsCenOrderDtlListByDocId(long orderDocId);

        EntityList<ReaBmsCenOrderDtl> GetReaBmsCenOrderDtlListByHQL(string strHqlWhere, string Order, int start, int count);
    }
}