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
	public class MEPTGetReportTimeDao : BaseDaoNHB<MEPTGetReportTime, long>, IDMEPTGetReportTimeDao
	{
        public IList<MEPTGetReportTime> SearchMEPTGetReportTimeByItemID(long longItemID, long longSpecialTimeTypeID, long longSickTypeID)
        {
            string strHQL = " select grt from MEPTGetReportTime grt " +
                            " inner join grt.MEPTGetReportTimeOfItemList grti " +
                            " inner join grt.MEPTGetReportTimeOfSickTypeList grts " +
                            //" inner join MEPTGetReportTimeOfItem grti "+
                            //" inner join MEPTGetReportTimeOfSickType grts "+
                            " where grt.MEPTBSpecialTimeType.Id=" + longSpecialTimeTypeID.ToString()+
                            " and grti.ItemAllItem.Id=" + longItemID.ToString() +
                            " and grts.BSickType.Id=" + longSickTypeID.ToString();
            return this.HibernateTemplate.Find<MEPTGetReportTime>(strHQL);
        }
	} 
}