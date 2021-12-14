using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.DAO.NHB.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.IDAO.ProjectProgressMonitorManage;

namespace ZhiFang.DAO.NHB.ProjectProgressMonitorManage
{	
	public class FFileInteractionDao : BaseDaoNHB<FFileInteraction, long>, IDFFileInteractionDao
	{
        public bool DeleteByFFileId(long fFileId)
        {
            try
            {
               this.HibernateTemplate.Delete("From FFileInteraction ffileinteraction where ffileinteraction.FFile.Id = " + fFileId);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    } 
}