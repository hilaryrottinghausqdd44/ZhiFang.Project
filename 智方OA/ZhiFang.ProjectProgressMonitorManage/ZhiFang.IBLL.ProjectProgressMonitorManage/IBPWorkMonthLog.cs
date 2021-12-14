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
    public interface IBPWorkMonthLog : IBGenericManager<PWorkMonthLog>
    {
        bool AddPWorkMonthLogByWeiXin(List<string> attachmentUrlList);
       bool UpdatePWorkMonthLogByField(PWorkMonthLog entity, string[] tempArray);
    }
}