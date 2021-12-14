using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.IDAO;
using NHibernate;
using NHibernate.Criterion;

namespace ZhiFang.Digitlab.DAO.NHB
{	
	public class QCMatTimeDao : BaseDaoNHB<QCMatTime, long>, IDQCMatTimeDao
	{
        #region IDQCMatTimeDao 成员

        public IList<QCMatTime> SearchQCMatTimeByMatIDAndDate(long longMatID, string strStartDate, string strEndDate)
        {
            Dictionary<string, List<ICriterion>> dic = new Dictionary<string, List<ICriterion>>();
            dic.Add("QCMatTime", new List<ICriterion>() { Restrictions.Le("BeginDate", DateTime.Parse(strStartDate)), Restrictions.Ge("EndDate", DateTime.Parse(strEndDate)) });
            dic.Add("QCMat", new List<ICriterion>() { Restrictions.Eq("Id", longMatID) });

            DaoNHBCriteriaAction<List<QCMatTime>, QCMatTime> action = new DaoNHBCriteriaAction<List<QCMatTime>, QCMatTime>(dic);

            List<QCMatTime> l = base.HibernateTemplate.Execute<List<QCMatTime>>(action);
            return l;
        }

        #endregion
    } 
}