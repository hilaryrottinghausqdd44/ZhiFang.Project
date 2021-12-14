using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.ProjectProgressMonitorManage;

namespace ZhiFang.DAO.NHB.ProjectProgressMonitorManage
{
    public class FFileCopyUserDao : BaseDaoNHB<FFileCopyUser, long>, IDFFileCopyUserDao
    {
        public bool DeleteByFFileId(long fFileId)
        {
            try
            {
                this.HibernateTemplate.Delete("From FFileCopyUser ffilecopyuser where ffilecopyuser.FFile.Id = " + fFileId);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}