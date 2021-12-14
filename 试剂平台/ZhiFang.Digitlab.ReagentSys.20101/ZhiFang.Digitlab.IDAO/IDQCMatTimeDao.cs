using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
    public interface IDQCMatTimeDao : IDBaseDao<QCMatTime, long>
    {
        /// <summary>
        /// 根据质控物ID和日期获取质控物时效性列表
        /// </summary>
        /// <param name="longMatID">质控物ID</param>
        /// <param name="strStartDate">开始日期</param>
        /// <param name="strEndDate">结束日期</param>
        /// <returns>IList&lt;QCMatTime&gt;</returns>
        IList<QCMatTime> SearchQCMatTimeByMatIDAndDate(long longMatID, string strStartDate, string strEndDate);
    }
}
