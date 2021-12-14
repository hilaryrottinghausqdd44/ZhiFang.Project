using System.Collections.Generic;
using System.IO;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaStoreIn;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsInDoc : IBGenericManager<ReaBmsInDoc>
    {
        /// <summary>
        /// 新增入库及入库明细集合
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtlAddList"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddInDocAndInDtlList(ReaBmsInDoc entity, IList<ReaBmsInDtl> dtlAddList, long empID, string empName);
        /// <summary>
        /// 新增库存初始化(手工入库)
        /// 入库单状态为暂存时不新增库存信息
        /// </summary>
        /// <param name="dtAddList"></param>
        /// <param name="codeScanningMode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsInDocAndDtlOfVO(IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode, long empID, string empName);
        /// <summary>
        /// 修改库存初始化信息(手工入库)
        /// </summary>
        /// <param name="tempArray"></param>
        /// <param name="dtAddList"></param>
        /// <param name="dtEditList"></param>
        /// <param name="codeScanningMode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsInDocAndDtlOfVO(ReaBmsInDoc entity, string[] tempArray, IList<ReaBmsInDtlVO> dtAddList, IList<ReaBmsInDtlVO> dtEditList, string fieldsDtl, string codeScanningMode, long empID, string empName);
        /// <summary>
        /// 接口提取数据转新增入库
        /// </summary>
        /// <param name="dtAddList"></param>
        /// <param name="codeScanningMode"></param>
        /// <param name="iSNeedCreateBarCode">接口数据是否需要重新生成条码 1:是;2:否;</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsInDocAndDtlByInterface(IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode, string iSNeedCreateBarCode, long empID, string empName);
        /// <summary>
        /// 接口供货转入库,编辑更新入库主单信息及编辑入库明细集合信息
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="dtAddList"></param>
        /// <param name="dtEditList"></param>
        /// <param name="iSNeedCreateBarCode">接口数据是否需要重新生成条码 1:是;2:否;</param>
        /// <param name="fieldsDtl"></param>
        /// <param name="codeScanningMode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsInDocAndDtlOfVOByInterface(ReaBmsInDoc entity, string[] tempArray, IList<ReaBmsInDtlVO> dtEditList, string iSNeedCreateBarCode, string fieldsDtl, string codeScanningMode, long empID, string empName);
        /// <summary>
        /// 新增验收入库
        /// 入库单状态为暂存时不新增库存信息
        /// </summary>
        /// <param name="dtAddList"></param>
        /// <param name="docConfirmID"></param>
        /// <param name="dtlDocConfirmIDStr"></param>
        /// <param name="codeScanningMode"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsInDocAndDtlOfVO(IList<ReaBmsInDtlVO> dtAddList, long docConfirmID, string dtlDocConfirmIDStr, string codeScanningMode, long empID, string empName);
        /// <summary>
        /// 手工入库,确认入库
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsInDocOfConfirmStock(long id, string codeScanningMode, long empID, string empName);
        /// <summary>
        /// 接口供货转入库,确认入库
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditConfirmInDocByInterface(long id, string codeScanningMode, string iSNeedCreateBarCode, long empID, string empName);
        BaseResultDataValue StatisticalAnalysisBmsInDocByGoodsNo(string startDate, string endDate, string cenOrgIdList, string goodsNoList, string comOrgIdList, string bmsInDocTypeList, string dateType, string cenOrgLevel);
        /// <summary>
        /// 退库入库
        /// </summary>
        /// <param name="inputType">入库类型：0整单入库，1部分入库</param>
        /// <param name="inDoc">退库的出库单</param>
        /// <param name="listReaBmsInDtl">退库的出库单明细</param>
        /// <param name="listOutDtlOfIn">库确认后调用退库接口的出库明细信息</param> 
        /// <returns></returns>
        BaseResultDataValue AddReaBmsInDocByReturnReaGoods(int inputType, ref ReaBmsInDoc inDoc, IList<ReaBmsInDtl> listReaBmsInDtl, ref IList<ReaBmsOutDtl> listOutDtlOfIn, ref IList<ReaBmsInDtl> addInDtlList,ref IList<ReaBmsQtyDtl> addQtyDtlList);
        /// <summary>
        /// 删除退库入库信息
        /// </summary>
        /// <param name="inDoc"></param>
        /// <returns></returns>
        BaseResultBool DelReaBmsInDocByReturn(ReaBmsInDoc inDoc);

        /// <summary>
        /// 入库撤消
        /// </summary>
        /// <param name="id"></param>
        /// <param name="empID"></param>
        /// <param name="empNam"></param>
        /// <returns></returns>
        BaseResultBool EditCancelReaBmsInDocById(long id, long empID, string empNam);
        /// <summary>
        /// 获取入库单PDF文件
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType">BTemplateType的Name</param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchPdfReportOfTypeById(string reaReportClass, long id, long labID, string labCName, string breportType, string frx, ref string fileName);
        /// <summary>
        /// 获取入库单Excel文件
        /// </summary>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType">BTemplateType的Name</param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchExcelReportOfExcelById(long id, long labID, string labCName, string breportType, string frx, ref string fileName);
        /// <summary>
        /// 根据入库主单条件及入库明细条件获取入库主单实体列表
        /// </summary>
        /// <param name="docHql">入库主单条件</param>
        /// <param name="dtlHql">入库明细条件</param>
        /// <param name="order">入库主单排序</param>
        /// <param name="page"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        EntityList<ReaBmsInDoc> SearchListByDocAndDtlHQL(string docHql, string dtlHql, string order, int page, int count);

        bool GetInDocInfoByOutDtl(long outDtlID, ref ReaBmsInDoc reaBmsInDoc, ref ReaBmsInDtl reaBmsInDtl);

        bool GetInDocInfoByOutDtl(ReaBmsOutDtl outDtl, ref ReaBmsInDoc reaBmsInDoc, ref ReaBmsInDtl reaBmsInDtl);

        /// <summary>
        /// 入库移库,获取库存货品库存数大于0的入库主单信息(HQL)
        /// </summary>
        /// <param name="docHql"></param>
        /// <param name="dtlHql"></param>
        /// <param name="reaGoodsHql"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        EntityList<ReaBmsInDoc> SearchReaBmsInDocOfQtyGEZeroByJoinHql(string docHql, string dtlHql, string reaGoodsHql, string sort, int page, int limit);

        BaseResultDataValue GetInterfaceNo(string goodsBarCode, string goodsNo, string goodsBatNo, ref string otherDocNo, ref string otherBatNo, ref string otherDtlNo);
    }
}