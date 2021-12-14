using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    /// 综合统计报表
    /// </summary>
    public interface IBComprehensiveStatistics : IBGenericManager<ReaGoodsOfMaxGonvertQtyVO>
    {
        /// <summary>
        /// 按机构货品的最大包装单位试剂耗材信息进行综合统计报表
        /// </summary>
        /// <param name="groupType"></param>
        /// <param name="companyId"></param>
        /// <param name="deptId"></param>
        /// <param name="testEquipId"></param>
        /// <param name="reaGoodsNo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaGoodsOfMaxGonvertQtyVO> SearchReaGoodsStatisticsOfMaxGonvertQtyEntityListHQL(int groupType, string companyId, string deptId, string testEquipId, string reaGoodsNo, string startDate, string endDate, string sort, int page, int limit);
        /// <summary>
        /// 按机构货品的最大包装单位试剂耗材信息进行综合统计报表,导出Excel
        /// </summary>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="groupType"></param>
        /// <param name="companyId"></param>
        /// <param name="deptId"></param>
        /// <param name="testEquipId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sort"></param>
        /// <param name="breportType"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream GetReaGoodsStatisticsOfMaxGonvertQtyReportOfExcelByHql(long labID, string labCName, int groupType, string companyId, string deptId, string testEquipId, string reaGoodsNo, string startDate, string endDate, string sort, string breportType, string frx, ref string fileName);
        /// <summary>
        /// 按机构货品的最大包装单位试剂耗材信息进行综合统计报表,导出Pdf
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="groupType"></param>
        /// <param name="companyId"></param>
        /// <param name="deptId"></param>
        /// <param name="testEquipId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sort"></param>
        /// <param name="breportType"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream GetReaGoodsStatisticsOfMaxGonvertQtyReportOfPdfByHql(string reaReportClass,long labID, string labCName, int groupType, string companyId, string deptId, string testEquipId, string reaGoodsNo, string startDate, string endDate, string sort, string breportType, string frx, ref string fileName);
    }
}
