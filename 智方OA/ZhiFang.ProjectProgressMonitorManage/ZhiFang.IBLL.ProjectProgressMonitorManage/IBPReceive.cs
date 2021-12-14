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
    public  interface IBPReceive : IBGenericManager<PReceive>
	{
        BaseResultDataValue AddPReceive();
        BaseResultDataValue AddBackPReceive();
        BaseResultDataValue SearchListTotalByHQL(string where, string fields);
    }
}