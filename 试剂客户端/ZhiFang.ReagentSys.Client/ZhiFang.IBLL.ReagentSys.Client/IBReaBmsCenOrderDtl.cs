

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsCenOrderDtl : IBGenericManager<ReaBmsCenOrderDtl>
    {
        IList<ReaBmsCenOrderDtl> SearchReaBmsCenOrderDtlListByHQL(string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit);

        BaseResultBool AddDtList(IList<ReaBmsCenOrderDtl> dtAddList, ReaBmsCenOrderDoc reaBmsOrderDoc, long empID, string empName);
        BaseResultBool EditDtList(IList<ReaBmsCenOrderDtl> dtEditList, ReaBmsCenOrderDoc reaBmsOrderDoc);
        /// <summary>
        /// 定制客户端订单验收获取某一订单的订单明细集合信息
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="Order"></param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<ReaOrderDtlOfConfirmVO> SearchReaOrderDtlOfConfirmVOListByHQL(string strHqlWhere, string Order, int page, int count);
        /// <summary>
        /// 订单汇总统计报表
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsCenOrderDtl> SearchReaBmsCenOrderDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit);
        /// <summary>
        /// 订单汇总统计报表导出Excel
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
        /// 订单汇总统计报表生成PDF
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
        /// 根据订货单ID查询订单明细
        /// </summary>
        /// <param name="orderDocId">订货总单ID</param>
        /// <returns></returns>
        IList<ReaBmsCenOrderDtl> GetReaBmsCenOrderDtlListByDocId(long orderDocId);

        EntityList<ReaBmsCenOrderDtl> GetReaBmsCenOrderDtlListByHQL(string strHqlWhere, string Order, int start, int count, long orderDocId);
    }
}