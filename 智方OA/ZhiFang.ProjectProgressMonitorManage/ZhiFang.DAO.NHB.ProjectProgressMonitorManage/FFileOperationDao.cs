using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.ProjectProgressMonitorManage;

namespace ZhiFang.DAO.NHB.ProjectProgressMonitorManage
{	
	public class FFileOperationDao : BaseDaoNHB<FFileOperation, long>, IDFFileOperationDao
	{
        public bool DeleteByFFileId(long fFileId)
        {
            try
            {
                this.HibernateTemplate.Delete("From FFileOperation ffileoperation where ffileoperation.FFile.Id = " + fFileId);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;                
            }
        }
    } 
}