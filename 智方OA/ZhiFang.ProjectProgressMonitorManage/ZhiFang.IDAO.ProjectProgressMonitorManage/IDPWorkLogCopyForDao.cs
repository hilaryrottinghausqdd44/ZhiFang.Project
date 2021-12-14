using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.Base;

namespace ZhiFang.IDAO.ProjectProgressMonitorManage
{
    public interface IDPWorkLogCopyForDao : IDBaseDao<PWorkLogCopyFor, long>
    {
        bool DeleteByLogId(long logId);

    }
}