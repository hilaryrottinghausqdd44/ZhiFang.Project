using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.NHB.ReagentSys.Client
{
	public interface IDReaBmsCenSaleDtlConfirmDao : IDBaseDao<ReaBmsCenSaleDtlConfirm, long>
	{
        IList<ReaBmsCenSaleDtlConfirm> SearchReaBmsCenSaleDtlConfirmSummaryByHQL(string docHql, string dtlHql, string order, int page, int limit);

        /// <summary>
        /// 关联货品表查询供货验收明细
        /// </summary>
        EntityList<ReaBmsCenSaleDtlConfirm> GetReaBmsCenSaleDtlConfirmListByHql(string strHqlWhere, string sort, int page, int limit);

    } 
}