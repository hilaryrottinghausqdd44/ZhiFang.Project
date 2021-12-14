
using System.Collections.Generic;
using System.IO;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsQtyDtl : IBGenericManager<ReaBmsQtyDtl>
    {
        /// <summary>
        /// 获取采购申请试剂的当前库存数量
        /// </summary>
        /// <param name="idStr">格式为"货品Id:供应商Id,货品Id2:供应商Id2"</param>
        /// <param name="goodIdStr">格式为"货品Id,货品Id2"</param>
        /// <returns></returns>
        IList<ReaGoodsCurrentQtyVO> SearchReaGoodsCurrentQtyByGoodIdStr(string idStr, string goodIdStr);
        BaseResultBool AddReaBmsQtyDtl(ReaBmsInDtl entity, CenOrg cenOrg, long operTypeID, long empID, string empName, ref ReaBmsQtyDtl addQtyDtl);
        /// <summary>
        /// 接口供货转入库,确认入库,产生库存及货品条码信息
        /// </summary>
        /// <param name="inDtl"></param>
        /// <param name="iSNeedCreateBarCode">接口数据是否需要重新生成条码 1:是;2:否;</param>
        /// <param name="operTypeID"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <param name="addQtyDtl"></param>
        /// <returns></returns>
        BaseResultBool AddReaBmsQtyDtlByInterface(ReaBmsInDoc inDoc, ReaBmsInDtl inDtl, CenOrg cenOrg, string iSNeedCreateBarCode, long operTypeID, long empID, string empName, ref ReaBmsQtyDtl addQtyDtl);
        /// <summary>
        ///  出库/移库/退库入库选择的库存货品信息
        /// </summary>
        /// <param name="deptGoodsHql">部门货品查询条件</param>
        /// <param name="reaGoodsHql">机构货品查询条件</param>
        /// <param name="qtyHql"></param>
        /// <param name="groupType"></param>
        /// <param name="isMergeInDocNo">是否按入库批次合并</param>
        /// <returns></returns>
        IList<ReaBmsQtyDtl> SearchReaBmsQtyDtl(string deptGoodsHql, string reaGoodsHql, string qtyHql, int groupType, bool isMergeInDocNo);
        /// <summary>
        /// 库存预警
        /// </summary>
        /// <param name="warningType">预警类型(1:低库存：2：高库存)</param>
        /// <param name="groupType">库存货品合并方式</param>
        /// <param name="comparisonType">库存比较值类型</param>
        /// <param name="storePercent"></param>
        /// <param name="strWhere"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlListByStockWarning(int warningType, int groupType, float storePercent, string comparisonType, int monthValue, string strWhere, string reaGoodsHql, int page, int limit, string sort);
        /// <summary>
        /// 获取某一试剂的库存数描述处理
        /// </summary>
        /// <param name="qtyList"></param>
        /// <param name="reaGoodsNo"></param>
        /// <returns>"1箱;10盒;3支"</returns>
        string SearchCurrentQtyInfo(IList<ReaBmsQtyDtl> qtyList, string reaGoodsNo);
        /// <summary>
        /// 获取库存查询结果,返回按合并项合并后List
        /// </summary>
        /// <param name="groupType">ReaBmsStatisticalType的枚举</param>
        /// <param name="hqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlListByGroupType(int groupType, string hqlWhere, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// 获取库存查询结果,返回按合并项合并后EntityList
        /// </summary>
        /// <param name="groupType">ReaBmsStatisticalType的枚举</param>
        /// <param name="hqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlEntityListByGroupType(int groupType, string hqlWhere, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// 库存标志维护获取库存查询结果,返回按合并项合并后EntityList
        /// </summary>
        /// <param name="groupType">ReaBmsStatisticalType的枚举</param>
        /// <param name="hqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlEntityListOfQtyMarkByGroupType(string storageId, int groupType, string hqlWhere, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// 登录成功后,获取库存预警,效期预警,注册证预警提示信息
        /// </summary>
        /// <returns></returns>
        BaseResultDataValue SearchReaGoodsWarningAlertInfo(long empID, string empName);
        /// <summary>
        /// 获取库存查询结果并导出Excel
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="groupType">ReaBmsStatisticalType的枚举</param>
        /// <param name="qtyType">1:库存查询;2:效期已过期报警;3:效期将过期报警</param>
        /// <param name="hqlWhere"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        FileStream SearchExportExcelReaBmsQtyDtlByGroupType(long labId, int qtyType, int groupType, string hqlWhere, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit, ref string fileName);
        /// <summary>
        /// 库存预警
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="warningType">预警类型(1:低库存：2：高库存)</param>
        /// <param name="groupType">库存货品合并方式</param>
        /// <param name="storePercent"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        FileStream SearchExportExcelByStockWarning(long labId, int warningType, int groupType, float storePercent, string comparisonType, int monthValue, string where, string reaGoodsHql, string sort, ref string fileName);
        /// <summary>
        /// 出库/移库/退库入库扫码,通过扫码定位出库存货品信息
        /// </summary>
        /// <param name="storageId">库房ID</param>
        /// <param name="placeId">货架ID</param>
        /// <param name="barcode">条码值</param> 
        /// <param name="barcodeOperType">扫码类型</param>
        /// <param name="isMergeInDocNo">(相同供货商+相同库房+相同货架+相同货品ID+相同货品批号+效期+入库批次)是否按入库批次合并显示库存记录</param>
        /// <param name="isAllowOfALLStorage">移库或出库扫码是否允许从所有库房获取库存货品</param>
        /// <param name="baseResultDataValue"></param>
        /// <returns></returns>
        EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlByBarCode(string storageId, string placeId, string barcode, string barcodeOperType, bool isMergeInDocNo, bool isAllowOfALLStorage, ref BaseResultDataValue baseResultDataValue);
        /// <summary>
        /// 库存货品导出Excel
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType"></param>
        /// <param name="groupType"></param>
        /// <param name="qtyHql"></param>
        /// <param name="sort"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchReaBmsQtyDtlOfExcelByQtyHql(long labId, string labCName, string breportType, int groupType, string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, string frx, ref string fileName);
        BaseResultBool AddReaBmsQtyDtlOperation(ReaBmsInDtl inDtl, ReaBmsQtyDtl qtyDtl, long operTypeID, long empID, string empName);
        string QueryReaGoodsStockXML(string goodsNo, string lastModifyTime, string resultFieldList, string resultType);
        /// <summary>
        /// 货品批号信息性能验证处理后,更新相应库存批次的库存货品性能验证结果
        /// </summary>
        /// <param name="reaGoodsLot"></param>
        /// <returns></returns>
        BaseResultBool UpdateVerificationByReaGoodsLot(ReaGoodsLot reaGoodsLot);
        /// <summary>
        /// 获取库存查询结果,支持按部门货品及机构货品的数据项进行过滤
        /// </summary>
        /// <param name="qtyHql">库存查询条件</param>
        /// <param name="deptGoodsHql">部门货品查询条件</param>
        /// <param name="reaGoodsHql">机构货品查询条件</param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlEntityListByHql(string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// ECharts(按库房/按供货商/按品牌/按货品分类)库存统计
        /// </summary>
        /// <param name="statisticType"></param>
        /// <param name="showZero"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtlHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        EntityList<EChartsVO> SearchQtyEChartsVOListByHql(int statisticType, bool showZero, string startDate, string endDate, string dtlHql, string deptGoodsHql, string reaGoodsHql, string sort);
        /// <summary>
        /// ECharts(堆叠):库存统计--按货品一级分类及二级分类
        /// </summary>
        /// <param name="statisticType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="dtlHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <returns></returns>
        BaseResultDataValue SearchStackQtyEChartsVOListByHql(int statisticType, string startDate, string endDate, string dtlHql, string deptGoodsHql, string reaGoodsHql);
        /// <summary>
        /// 联合查询库存信息及机构货品信息
        /// </summary>
        /// <param name="qtyHql"></param>
        /// <param name="deptGoodsHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlAndReaGoodsListByAllJoinHql(string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// 入库移库,获取库存货品库存数大于0的入库库存记录信息(HQL)
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="qtyHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsQtyDtl> SearchReaBmsQtyDtlEntityListOfQtyGEZeroByJoinHql(string where, string inDtlHql, string qtyHql, string reaGoodsHql, string sort, int page, int limit);
        /// <summary>
        /// 入库移库,获取库存货品库存数大于0的入库库存记录信息(HQL)
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="qtyHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        IList<ReaBmsQtyDtl> SearchReaBmsQtyDtlListOfQtyGEZeroByJoinHql(string where, string inDtlHql, string qtyHql, string reaGoodsHql, string sort, int page, int limit);
        BaseResultBool UpdateReaBmsQtyDtlByQtyDtlMark(QtyMarkVO entity);

        #region 四川大家试剂投屏需求
        EntityList<ReaGoodsStockWarning> SearchReaGoodsStockWarningList(int page, int limit, string where, string sort, ref string warningPromptInfo);
        #endregion
    }
}