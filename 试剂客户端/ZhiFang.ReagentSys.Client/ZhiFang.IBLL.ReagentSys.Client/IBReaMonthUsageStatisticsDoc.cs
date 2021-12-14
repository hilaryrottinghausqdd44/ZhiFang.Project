

using System.Collections.Generic;
using System.IO;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaMonthUsageStatisticsDoc : IBGenericManager<ReaMonthUsageStatisticsDoc>
    {
        /// <summary>
        /// 新增出库使用量单
        /// </summary>
        /// <param name="round"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaMonthUsageStatisticsDoc(ReaMonthUsageStatisticsDoc entity, long empID, string employeeName);
        /// <summary>
        /// 依主单ID,删除使用出库使用量统计主单及明细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        BaseResultBool RemoveDocAndDtlByDocId(long id);
        /// <summary>
        /// 获取出库使用量单PDF文件
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType">BTemplateType的Name</param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream GetPdfReportOfTypeById(string reaReportClass, long id, long labID, string labCName, string breportType, string frx, ref string fileName);
        /// <summary>
        /// 获取出库使用量单Excel文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType">BTemplateType的Name</param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream GetExcelReportOfExcelById(long id, long labID, string labCName, string breportType, string frx, ref string fileName);
    }
}