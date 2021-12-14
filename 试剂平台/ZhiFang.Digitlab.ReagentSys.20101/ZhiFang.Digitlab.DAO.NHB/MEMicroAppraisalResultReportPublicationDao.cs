using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;

namespace ZhiFang.Digitlab.DAO.NHB
{
    public class MEMicroAppraisalResultReportPublicationDao : BaseDaoNHB<MEMicroAppraisalResultReportPublication, long>, IDMEMicroAppraisalResultReportPublicationDao
	{
        IDMEMicroAppraisalValueDao IDMEMicroAppraisalValueDao { get; set; }
        public override bool Save(MEMicroAppraisalResultReportPublication entity)
        {
            this.HibernateTemplate.Save(entity);
            string hql = "update MEMicroAppraisalValue memicroappraisalvalue set memicroappraisalvalue.IsReportPublication = true where memicroappraisalvalue.Id=" + entity.MEMicroAppraisalValue.Id;
            //IDMEMicroAppraisalValueDao.UpdateByHql(hql);
            var result = this.HibernateTemplate.Execute<int>(new DaoNHBHqlAction(hql));//FilterMacroCommand(hql)
            return true;
        }
	} 
}
