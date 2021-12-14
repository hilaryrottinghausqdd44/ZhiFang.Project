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
    public interface IBPExpenseAccount : IBGenericManager<PExpenseAccount>
    {
        BaseResultDataValue PExpenseAccountAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction);

        BaseResultBool PExpenseAccountStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction, string[] fieldArray, long empID, string empName);
    }
}