using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ProjectProgressMonitorManage
{
    /// <summary>
    ///
    /// </summary>
    public interface IBPWorkWeekLog : IBGenericManager<PWorkWeekLog>
    {
        bool AddPWorkWeekLogByWeiXin(List<string> attachmentUrlList);
        bool UpdatePWorkWeekLogByField(PWorkWeekLog entity, string[] tempArray);
    }
}