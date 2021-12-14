using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
    public interface IDQCItemTimeDao : IDBaseDao<ZhiFang.Digitlab.Entity.QCItemTime, long>
    {
        /// <summary>
        /// 根据质控项目ID、启用日期、终止日期返回质控项目时效列表
        /// </summary>
        /// <param name="longQCItemID">质控项目ID</param>
        /// <param name="strStartDate">启用日期</param>
        /// <param name="strEndDate">终止日期</param>
        /// <returns>IList&lt;QCItemTime&gt;</returns>
        IList<QCItemTime> SearchQCItemTimeByQCItemIDAndDate(long longQCItemID, string strStartDate, string strEndDate);

        /// <summary>
        /// 根据质控项目ID、日期返回质控项目时效列表
        /// </summary>
        /// <param name="longQCItemID">质控项目ID</param>
        /// <param name="strDate">日期</param>
        /// <returns>IList&lt;QCItemTime&gt;</returns>
        IList<QCItemTime> SearchQCItemTimeByQCItemIDAndDate(long longQCItemID, string strDate);

        /// <summary>
        /// 根据质控项目ID、日期返回质控项目时效Target、SD、CV列表
        /// </summary>
        /// <param name="longQCItemID">质控项目ID</param>
        /// <param name="strDate">日期</param>
        /// <returns>IList&lt;QCItemTime&gt;</returns>
        IList<QCItemTime> SearchQCItemTimeCustomColByQCItemIDAndDate(long longQCItemID, string strDate);
    }
}
