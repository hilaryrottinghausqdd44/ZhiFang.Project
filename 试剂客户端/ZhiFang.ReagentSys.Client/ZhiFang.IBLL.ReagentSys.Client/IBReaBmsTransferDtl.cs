
using System.Collections.Generic;
using System.IO;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsTransferDtl : IBGenericManager<ReaBmsTransferDtl>
    {
        /// <summary>
        /// 移库明细保存前的验证
        /// </summary>
        /// <param name="dtlList"></param>
        /// <returns></returns>
        BaseResultDataValue EditValidTransferDtlList(IList<ReaBmsTransferDtl> dtlList);
        /// <summary>
        /// 新增移库明细信息
        /// </summary>
        /// <param name="transferDoc"></param>
        /// <param name="dtAddList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddTransferDtlList(ReaBmsTransferDoc transferDoc, IList<ReaBmsTransferDtl> dtAddList, long empID, string empName);
        /// <summary>
        /// 编辑移库明细信息
        /// </summary>
        /// <param name="transferDoc"></param>
        /// <param name="dtEditList"></param>
        /// <returns></returns>
        BaseResultBool UpdateTransferDtlList(ReaBmsTransferDoc transferDoc, IList<ReaBmsTransferDtl> dtEditList);
        /// <summary>
        /// 移库领用统计报表
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsTransferDtl> SearchReaBmsTransferDtlSummaryListByHQL(string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit);
        /// <summary>
        /// 移库领用统计报表
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsTransferDtl> SearchReaBmsTransferDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit);
        /// <summary>
        /// 移库汇总统计报表导出Excel
        /// </summary>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="groupType"></param>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="sort"></param>
        /// <param name="breportType"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchBusinessSummaryReportOfExcelByHql(long labID, string labCName, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string fileName, string startDate, string endDate);
        /// <summary>
        /// 移库汇总统计报表生成PDF
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="groupType"></param>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="sort"></param>
        /// <param name="breportType"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchBusinessSummaryReportOfPdfByHql(string reaReportClass, long labID, string labCName, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string fileName, string startDate, string endDate);
        /// <summary>
        /// 移库领用及使用返回统计
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaTransferAndOutDtlVO> SearchReaTransferAndOutDtlVOListByHQL(int groupType, string hqlStr, string order, int page, int limit);
        /// <summary>
        /// 移库领用及使用返回统计导出Excel
        /// </summary>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="groupType"></param>
        /// <param name="hqlStr"></param>
        /// <param name="sort"></param>
        /// <param name="breportType"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchReaTransferAndOutDtlVOOfExcelByHql(long labID, string labCName, int groupType, string hqlStr, string sort, string breportType, string frx, ref string fileName);
        /// <summary>
        /// 移库领用及使用返回统计生成PDF
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="groupType"></param>
        /// <param name="hqlStr"></param>
        /// <param name="sort"></param>
        /// <param name="breportType"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchReaTransferAndOutDtlVOOfPdfByHql(string reaReportClass, long labID, string labCName, int groupType, string hqlStr, string sort, string breportType, string frx, ref string fileName);
    }
}