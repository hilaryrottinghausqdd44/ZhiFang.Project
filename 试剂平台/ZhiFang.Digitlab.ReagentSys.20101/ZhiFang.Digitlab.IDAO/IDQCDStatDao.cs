using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
	public interface IDQCDStatDao : IDBaseDao<QCDStat, long>
	{
        /// <summary>
        /// 质控统计
        /// </summary>
        /// <param name="qcItemList">质控项目ID列表</param>
        /// <param name="yearID">年份</param>
        /// <param name="monthID">月份</param>
        /// <returns>IList&lt;QCDataStatistics&gt;</returns>
        IList<QCDataStatistics> SearchQCDValueToStatistics(IList<QCItem> qcItemList, int yearID, int monthID);

        /// <summary>
        /// 根据质控项目和年份、月份查询质控统计数据列表
        /// </summary>
        /// <param name="qcItemList">质控项目ID列表</param>
        /// <param name="yearID">年份</param>
        /// <param name="monthID">月份</param>
        /// <returns>IList&lt;QCDStat&gt;</returns>
        IList<QCDStat> SearchQCDStatByQCItemAndDate(IList<QCItem> qcItemList, int yearID, int monthID);
	} 
}