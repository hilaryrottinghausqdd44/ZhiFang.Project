using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{	
	public class MEMicroDSTResultReportPublicationDao : BaseDaoNHB<MEMicroDSTResultReportPublication, long>, IDMEMicroDSTResultReportPublicationDao
	{
        //IDMEMicroDSTValueDao IDMEMicroDSTValueDao { get; set; }
        public override bool Save(MEMicroDSTResultReportPublication entity)
        {
            this.HibernateTemplate.Save(entity);
            string hql = "update MEMicroDSTValue memicrodstvalue set memicrodstvalue.IsReportPublication = true where memicrodstvalue.Id=" + entity.MEMicroDSTValue.Id;
            //IDMEMicroDSTValueDao.UpdateByHql(hql);
            var result = this.HibernateTemplate.Execute<int>(new DaoNHBHqlAction(hql));
            return true ;
        }
	} 
}