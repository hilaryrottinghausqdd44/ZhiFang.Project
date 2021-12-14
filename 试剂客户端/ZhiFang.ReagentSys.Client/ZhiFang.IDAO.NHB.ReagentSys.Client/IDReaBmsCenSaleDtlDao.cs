using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
    public interface IDReaBmsCenSaleDtlDao : IDBaseDao<ReaBmsCenSaleDtl, long>
    {
        /// <summary>
        /// 批条码打印次数更新
        /// </summary>
        /// <param name="batchList"></param>
        /// <returns></returns>
        BaseResultBool UpdatePrintCount(IList<long> batchList);
        /// <summary>
        /// 获取供货明细及供货主单信息
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsCenSaleDtl> SearchDtlAdnDocListByHQL(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// 获取供货明细及供货主单信息
        /// </summary>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsCenSaleDtl> SearchNewEntityListByHQL(string where, string sort, int page, int limit);
        /// <summary>
        /// 供货明细汇总统计
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsCenSaleDtl> SearchReaBmsCenSaleDtlSummaryByHQL(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit);
    }
}