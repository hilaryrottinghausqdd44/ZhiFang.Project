using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.IDAO
{
    public  interface IDQCDValueDao : IDBaseDao<ZhiFang.Digitlab.Entity.QCDValue, long>
    {
        /// <summary>
        /// 根据质控项目ID和指定日期返回质控数据列表
        /// </summary>
        /// <param name="longQCItemID">质控项目ID</param>
        /// <param name="strStartDate">质控时间开始日期</param>
        /// <param name="strEndDate">质控时间结束日期</param>
        /// <returns>IList&lt;QCDValue&gt;</returns>
        IList<QCDValue> SearchQCDValueByQCItemIDAndDate(long longQCItemID, string strStartDate, string strEndDate);
        /// <summary>
        /// 根据质控项目ID和指定日期返回质控数据列表(自定义列)
        /// </summary>
        /// <param name="listQCItem">质控项目列表</param>
        /// <param name="strStartDate">质控时间开始日期</param>
        /// <param name="strEndDate">质控时间结束日期</param>
        /// <returns>IList&lt;QCDValueCustom&gt;</returns>
        IList<QCDValueCustom> SearchQCDValueCustomByQCItemIDAndDate(IList<QCItem> listQCItem, string strStartDate, string strEndDate);
        /// <summary>
        /// 根据项目ID和指定日期返回质控数据列表
        /// </summary>
        /// <param name="longQCItemID">项目ID</param>
        /// <param name="strStartDate">质控时间开始日期</param>
        /// <param name="strEndDate">质控时间结束日期</param>
        /// <returns>IList&lt;QCDValue&gt;</returns>
        IList<QCDValue> SearchQCDValueByItemIDAndDate(long longItemID, string strStartDate, string strEndDate);
        /// <summary>
        /// 根据项目ID和指定日期返回质控数据列表
        /// </summary>
        /// <param name="longQCItemID">项目ID</param>
        /// <param name="listQCMat">质控物列表</param>
        /// <param name="strStartDate">质控时间开始日期</param>
        /// <param name="strEndDate">质控时间结束日期</param>
        /// <returns>IList&lt;QCDValue&gt;</returns>
        IList<QCDValueCustom> SearchQCDValueByItemIDAndDate(long longItemID, IList<QCMat> listQCMat, string strStartDate, string strEndDate);
        /// <summary>
        /// 根据HQL语句返回质控数据列表
        /// </summary>
        /// <param name="strQueryFilter">查询条件(HQL语句)</param>
        /// <returns>IList&lt;QCDValue&gt;</returns>
        IList<QCDValue> SearchQCDValueByQueryFilter(string strQueryFilter);
        /// <summary>
        /// 根据质控项目ID和指定日期返回所有浓度的质控数据列表
        /// </summary>
        /// <param name="longQCItemID">质控项目ID</param>
        /// <param name="strStartDate">质控时间开始日期</param>
        /// <param name="strEndDate">质控时间结束日期</param>
        /// <returns>IList&lt;QCDValue&gt;</returns>
        IList<QCDValue> SearchAllConcentrationQCDValueByQCItemIDAndDate(long longQCItemID, string strStartDate, string strEndDate);   
 
        /// <summary>
        /// 根据项目ID和指定日期返回质控数据列表(自定义查询字段)
        /// </summary>
        /// <param name="longQCItemID">质控项目ID</param>
        /// <param name="strStartDate">质控时间开始日期</param>
        /// <param name="strEndDate">质控时间结束日期</param>
        /// <returns>IList&lt;QCDValue&gt;</returns>
        IList<QCDValue> SearchQCDValueCustomByItemIDAndDate(long longQCItemID, string strStartDate, string strEndDate);

        /// <summary>
        /// 根据项目ID和指定日期返回质控数据列表(自定义查询字段，用于计算靶值、计算标准差)
        /// </summary>
        /// <param name="longQCItemID">质控项目ID</param>
        /// <param name="strStartDate">质控时间开始日期</param>
        /// <param name="strEndDate">质控时间结束日期</param>
        /// <returns>IList&lt;QCDValue&gt;</returns>
        IList<QCDValue> SearchQCDValueCustomByItemIDAndDateToCalcTargetSD(long longQCItemID, string strStartDate, string strEndDate);

        /// <summary>
        /// 质控统计
        /// </summary>
        /// <param name="strStartDate">开始日期</param>
        /// <param name="strEndDate">结束日期</param>
        /// <param name="longSpecialtyID">专业ID</param>
        /// <param name="longEquipID">仪器ID</param>
        /// <param name="longQCMatID">质控物ID</param>
        /// <param name="longItemID">检验项目ID</param>
        /// <returns>IList&lt;QCDataStatistics&gt;</returns>
        IList<QCDataStatistics> SearchQCDValueToStatistics(string strStartDate, string strEndDate, long longSpecialtyID, long longEquipID, long longQCMatID, long longItemID);

    }
}
