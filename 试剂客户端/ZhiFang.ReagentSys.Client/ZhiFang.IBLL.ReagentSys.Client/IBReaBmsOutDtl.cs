using System;
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
    public interface IBReaBmsOutDtl : IBGenericManager<ReaBmsOutDtl>
    {
        /// <summary>
        /// 新增出库申请明细
        /// </summary>
        /// <param name="outDoc"></param>
        /// <param name="dtAddList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddOutDtlList(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> dtAddList, long empID, string empName);
        /// <summary>
        /// 编辑保存出库申请明细
        /// </summary>
        /// <param name="outDoc"></param>
        /// <param name="dtEditList"></param>
        /// <returns></returns>
        BaseResultBool UpdateOutDtlList(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> dtEditList);
        /// <summary>
        /// 出库汇总统计
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsOutDtl> SearchReaBmsOutDtlSummaryListByHQL(string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit);
        /// <summary>
        /// 出库汇总统计
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsOutDtl> SearchReaBmsOutDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit);
        /// <summary>
        /// 出库汇总统计
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="deptId"></param>
        /// <param name="testEquipId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsOutDtl> SearchReaBmsOutDtlSummaryByHQL(int groupType, string companyId, string deptId, string testEquipId, string startDate, string endDate, string sort, int page, int limit);
        /// <summary>
        /// 出库明细汇总统计报表导出Excel
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
        /// 出库明细汇总统计报表生成Pdf
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
        /// 消耗比对分析,获取仪器试剂使用量
        /// </summary>
        /// <param name="dtlHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<ReaBmsOutDtl> SearchReaBmsOutDtEntityListByJoinHql(int groupType, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort);
        /// <summary>
        /// EChart仪器试剂使用量
        /// </summary>
        /// <param name="statisticType"></param>
        /// <param name="showZero"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<EChartsVO> SearchEquipReagUsageEChartsVOListByHql(int statisticType, bool showZero, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, string sort);
        /// <summary>
        /// ECharts(按库房/按供货商/按品牌/按货品分类)出库统计
        /// </summary>
        /// <param name="statisticType"></param>
        /// <param name="showZero"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<EChartsVO> SearchOutEChartsVOListByHql(int statisticType, bool showZero, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, string sort);
        /// <summary>
        /// ECharts(堆叠):出库统计--按货品一级分类及二级分类
        /// </summary>
        /// <param name="statisticType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <returns></returns>
        BaseResultDataValue SearchStackOutEChartsVOListByHql(int statisticType, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql);
        /// <summary>
        /// 按出库主单+出库明细+机构货品+(部门货品)连接查询
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        IList<ReaBmsOutDtl> SearchOutDocAndDtlListByAllJoinHql(string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, int page, int limit, string sort);

        /// <summary>
        /// 出库变更台账导出Excel或PDF预览
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="labId"></param>
        /// <param name="labCName"></param>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <param name="breportType"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        Stream SearchReaBmsOutDtlLotNoAndTransportNoChangeOfExcelPdfByHQL(string reaReportClass, long labId, string labCName, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string fileName, string startDate, string endDate);

        /// <summary>
        /// 智方试剂平台查询使用，hasLabId传false，不增加LabID的默认条件
        /// </summary>
        EntityList<ReaBmsOutDtl> GetListByHQL(string strHqlWhere, string Order, int start, int count, bool hasLabId);

    }
}