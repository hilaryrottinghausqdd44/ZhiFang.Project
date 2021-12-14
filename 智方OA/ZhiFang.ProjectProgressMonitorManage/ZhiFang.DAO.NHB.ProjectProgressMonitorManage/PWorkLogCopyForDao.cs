using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.ProjectProgressMonitorManage;

namespace ZhiFang.DAO.NHB.ProjectProgressMonitorManage
{
    public class PWorkLogCopyForDao : BaseDaoNHB<PWorkLogCopyFor, long>, IDPWorkLogCopyForDao
    {
        public bool DeleteByLogId(long logId)
        {
            int result = 0;
            result = this.HibernateTemplate.Delete("From PWorkLogCopyFor pworklogcopyfor where pworklogcopyfor.LogID=" + logId);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return true;
            }
        }
    }
}