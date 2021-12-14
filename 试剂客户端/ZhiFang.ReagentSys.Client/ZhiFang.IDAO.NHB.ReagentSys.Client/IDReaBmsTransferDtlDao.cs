using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
	public interface IDReaBmsTransferDtlDao : IDBaseDao<ReaBmsTransferDtl, long>
	{
        /// <summary>
        /// 移库领用统计报表
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsTransferDtl> SearchReaBmsTransferDtlSummaryByHQL(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit);
    } 
}