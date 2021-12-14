using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
	public interface IDMEPTGetReportTimeDao : IDBaseDao<MEPTGetReportTime, long>
	{
        IList<MEPTGetReportTime> SearchMEPTGetReportTimeByItemID(long longItemID, long longSpecialTimeTypeID, long longSickTypeID);
	} 
}