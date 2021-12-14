using System;
using System.Collections.Generic;
using System.IO;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaStoreIn;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsInDtl : IBGenericManager<ReaBmsInDtl>
    {
        /// <summary>
        /// 入库明细保存时的基本验证
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        BaseResultBool EditsInDtlBasicValid(ReaBmsInDtl model);
        /// <summary>
        /// 入库明细的保存验证判断
        /// </summary>
        /// <param name="dtAddList"></param>
        /// <param name="codeScanningMode">扫码模式:严格模式-strict;混合模式-mixing</param>
        /// <returns></returns>
        BaseResultBool EditReaBmsInDtlListVOOfValid(IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode);
        /// <summary>
        /// 入库明细的保存验证判断
        /// </summary>
        /// <param name="dtEditList"></param>
        /// <param name="codeScanningMode">扫码模式:严格模式-strict;混合模式-mixing</param>
        /// <returns></returns>
        BaseResultBool EditReaBmsInDtlListOfValid(IList<ReaBmsInDtl> dtEditList, string codeScanningMode);
        /// <summary>
        /// 新增入库明细集合
        /// </summary>
        /// <param name="inDoc"></param>
        /// <param name="dtAddList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsInDtlList(ReaBmsInDoc inDoc, IList<ReaBmsInDtl> dtAddList, long empID, string empName);
        /// <summary>
        /// 新增入库明细(入库单状态为暂存时不新增库存信息)
        /// </summary>
        /// <param name="inDoc"></param>
        /// <param name="dtAddList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool AddReaBmsInDtlOfVO(ReaBmsInDoc inDoc, IList<ReaBmsInDtlVO> dtAddList, long empID, string empName);
        /// <summary>
        /// 接口提取数据转新增入库
        /// </summary>
        /// <param name="inDoc"></param>
        /// <param name="dtAddList"></param>
        /// <param name="iSNeedCreateBarCode">接口数据是否需要重新生成条码 1:是;2:否;</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool AddReaBmsInDtlOfVOByInterface(ReaBmsInDoc inDoc, IList<ReaBmsInDtlVO> dtAddList, string iSNeedCreateBarCode, long empID, string empName);
        /// <summary>
        /// 编辑入库信息
        /// 入库总单由暂存转为确认入库操作时处理入库明细信息(入库单状态为暂存时不新增库存信息)
        /// </summary>
        /// <param name="inDoc"></param>
        /// <param name="dtAddList"></param>
        /// <param name="dtEditList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsInDtlOfVO(ReaBmsInDoc inDoc, IList<ReaBmsInDtlVO> dtAddList, IList<ReaBmsInDtlVO> dtEditList, string fieldsDtl, long empID, string empName);
        /// <summary>
        /// 接口供货转入库,编辑更新入库主单信息及编辑入库明细集合信息
        /// </summary>
        /// <param name="inDoc"></param>
        /// <param name="dtEditList"></param>
        /// <param name="iSNeedCreateBarCode">接口数据是否需要重新生成条码 1:是;2:否;</param>
        /// <param name="fieldsDtl"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsInDtlOfVOByInterface(ReaBmsInDoc inDoc, IList<ReaBmsInDtlVO> dtEditList, string iSNeedCreateBarCode, string fieldsDtl, long empID, string empName);
        BaseResultBool EditReaBmsInDtl(ReaBmsInDoc inDoc, IList<ReaBmsInDtl> dtEditList, bool isEditDtl, long empID, string empName);
        /// <summary>
        /// 接口供货转入库,确认入库
        /// </summary>
        /// <param name="inDoc"></param>
        /// <param name="dtEditList"></param>
        /// <param name="isEditDtl"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsInDtlByInterface(ReaBmsInDoc inDoc, IList<ReaBmsInDtl> dtEditList, bool isEditDtl,string iSNeedCreateBarCode, long empID, string empName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strHqlWhere"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsInDtlVO> SearchReaBmsInDtlVOByHQL(string strHqlWhere, string order, int page, int limit);

        IList<string> SearchGoodsIDListByGoodsSerialNo(string serialNo);

        ReaBmsInDtl SearchReaBmsInDtlByReaBmsOutDtl(ReaBmsInDoc inDoc, ReaBmsOutDtl outDtl, double goodsQty, long empID, string empName);

        /// <summary>
        /// 入库汇总统计报表
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="order"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="isPage">是否返回分页的数据</param>
        /// <returns></returns>
        EntityList<ReaBmsInDtl> SearchReaBmsInDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string order, int page, int limit, bool isOfColdInfoMerge, bool isPage);
        /// <summary>
        /// 入库汇总统计报表导出Excel
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
        Stream SearchBusinessSummaryReportOfExcelByHql(long labID, string labCName, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string fileName, string startDate, string endDate, bool isOfColdInfoMerge);
        /// <summary>
        /// 入库汇总统计报表生成PDF
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
        Stream SearchBusinessSummaryReportOfPdfByHql(string reaReportClass, long labID, string labCName, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string breportType, string frx, ref string fileName, string startDate, string endDate, bool isOfColdInfoMerge);
        void AddReaGoodsLot(ref ReaGoodsLot reaGoodsLot, long empID, string empName);
        /// <summary>
        /// ECharts(堆叠):入库统计--按货品一级分类及二级分类
        /// </summary>
        /// <param name="statisticType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <returns></returns>
        BaseResultDataValue SearchStackInEChartsVOListByHql(int statisticType, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql);
        /// <summary>
        /// ECharts(按库房/按供货商/按品牌/按货品分类)入库统计
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
        EntityList<EChartsVO> SearchInEChartsVOListByHql(int statisticType, bool showZero, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, string sort);

        /// <summary>
        /// 关联货品表查询入库明细
        /// </summary>
        EntityList<ReaBmsInDtl> GetReaBmsInDtlListByHql(string strHqlWhere, string sort, int page, int limit);
    }
}