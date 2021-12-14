using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public  interface IBPFinanceReceive : IBGenericManager<PFinanceReceive>
	{
        bool UpdateByFinance(string[] strParas, PFinanceReceive entity, string fields);
        bool RemoveByFinance(long PFinanceReceiveID);
        BaseResultDataValue SearchListTotalByHQL(string where, string fields);
    }
}