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
    public interface IBPCustomerService : IBGenericManager<PCustomerService>
    {
        bool PCustomerServiceAdd(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction);
        bool PCustomerServiceStatusUpdate(SysWeiXinTemplate.PushWeiXinMessage pushWeiXinMessageAction,  string[] tempArray, long empID, string empName);
    }
}