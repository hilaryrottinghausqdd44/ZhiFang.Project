using System.Collections.Generic;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaBmsInDtlDao : IDBaseDao<ReaBmsInDtl, long>
    {
        IList<ReaBmsInDtl> SearchReaBmsInDtlSummaryByHQL(string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit);
        IList<ReaBmsInDtl> SearchListByReaGoodHQL(string dtlHql, string reaGoodHql, string sort, int page, int limit);
        IList<ReaBmsInDtl> SearchReaBmsInDtlListByJoinHql(string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort);
        EntityList<ReaBmsInDtl> SearchReaBmsInDtlEntityListByJoinHql(string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort);

        /// <summary>
        /// 关联货品表查询入库明细
        /// </summary>
        EntityList<ReaBmsInDtl> GetReaBmsInDtlListByHql(string strHqlWhere, string sort, int page, int limit);
    }
}