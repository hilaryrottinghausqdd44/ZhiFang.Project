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
    public  interface IBPRepayment : IBGenericManager<PRepayment>
	{
        BaseResultDataValue PRepaymentAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction);
        BaseResultBool PRepaymentStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] tempArray, long empID, string empName);
    }
}