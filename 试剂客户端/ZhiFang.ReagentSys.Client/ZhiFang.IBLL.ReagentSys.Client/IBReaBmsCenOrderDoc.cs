
using System;
using System.Collections.Generic;

using System.Data;
using System.IO;
using System.Web;
using Newtonsoft.Json.Linq;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using ZhiFang.IBLL.Base;

namespace ZhiFang.IBLL.ReagentSys.Client
{
    /// <summary>
    ///
    /// </summary>
    public interface IBReaBmsCenOrderDoc : IBGenericManager<ReaBmsCenOrderDoc>
    {
        #region 客户端订单处理
        /// <summary>
        /// 客户端申请生成订单
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtl"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool AddReaBmsCenOrderDocAndDtl(ReaBmsCenOrderDoc entity, Dictionary<string, ReaBmsCenOrderDtl> dtl, long empID, string empName);
        /// <summary>
        /// 订单新增
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="dtAddList"></param>
        /// <param name="otype">1:PC端;2:移动端</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaBmsCenOrderDocAndDt(ReaBmsCenOrderDoc entity, IList<ReaBmsCenOrderDtl> dtAddList, int otype, long empID, string empName);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="dtAddList"></param>
        /// <param name="dtEditList"></param>
        /// <param name="checkIsUploaded">订单审核通过同时直接订单上传</param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsCenOrderDocAndDt(ReaBmsCenOrderDoc entity, string[] tempArray, IList<ReaBmsCenOrderDtl> dtAddList, IList<ReaBmsCenOrderDtl> dtEditList, string checkIsUploaded, long empID, string empName);
        /// <summary>
        /// 订单付款管理更新
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsCenOrderDocByPay(ReaBmsCenOrderDoc entity, string[] tempArray, long empID, string empName);
        /// <summary>
        /// 计算订单总额
        /// </summary>
        /// <param name="docID">订单ID</param>
        /// <returns></returns>
        BaseResultDataValue EditReaBmsCenOrderDocTotalPrice(long docID);
        /// <summary>
        /// 订单上传,客户端与平台都在同一数据库
        /// </summary>
        /// <param name="idStr"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsCenOrderDocOfUploadOfIdStr(string idStr, bool isVerifyProdGoodsNo, long empID, string empName);
        /// <summary>
        /// 客户端订单取消上传,客户端与平台都在同一数据库
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tempArray"></param>
        /// <param name="empID"></param>
        /// <param name="empName"></param>
        /// <returns></returns>
        BaseResultBool EditReaBmsCenOrderDocOfCancelUpload(ReaBmsCenOrderDoc entity, string[] tempArray, bool isVerifyProdGoodsNo, long empID, string empName);
        #endregion

        /// <summary>
        /// 将选择的多个订单按供货商+货品编码+包装单位合并后,生成PDF报表文件
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchReaBmsCenOrderDocOfPdfByIdStr(string reaReportClass, string idStr, long labID, string labCName, string breportType, string frx, ref string fileName);

        /// <summary>
        /// 将选择的多个订单按供货商+货品编码+包装单位合并后,生成Excel报表文件
        /// </summary>
        /// <param name="reaReportClass"></param>
        /// <param name="id"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <param name="breportType"></param>
        /// <param name="frx"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        Stream SearchReaBmsCenOrderDocOfExcelByIdStr(string reaReportClass, string idStr, long labID, string labCName, string breportType, string frx, ref string fileName);

        /// <summary>
        /// 智方客户端上传订单到平台
        /// </summary>
        /// <param name="orderDoc"></param>
        /// <param name="orderDtlList"></param>
        /// <returns></returns>
        BaseResultDataValue AddReaOrderDocAndDtlOfUpload(ReaBmsCenOrderDoc orderDoc, bool isVerifyProdGoodsNo, IList<ReaBmsCenOrderDtl> orderDtlList, long empID, string empName);
        void AddReaReqOperation(ReaBmsCenOrderDoc entity, long empID, string empName);

        /// <summary>
        /// 获取订货单PDF文件
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
        /// 获取订货单Excel文件
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
        /// 试剂耗材月订货清单
        /// </summary>
        /// <param name="where"></param>
        /// <param name="frx"></param>
        /// <param name="labID"></param>
        /// <param name="labCName"></param>
        /// <returns></returns>
        Stream SearchReaBmsCenOrderDocListReportOfPdf(string where, string frx, long labID, string labCName, ref string fileNam);
        /// <summary>
        /// 订单验收,将供货订单Excel文件导入并解析订单的当次供货信息
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        BaseResultDataValue UploadSupplyReaOrderDataByExcel(HttpPostedFile file, long labID, string labName, ref EntityList<ReaOrderDtlOfConfirmVO> entityList);

        BaseResultDataValue EditReaBmsCenOrderDocThirdFlag(long id, int isThirdFlag);

        BaseResultDataValue EditReaBmsCenOrderDocThirdFlag(long id, int isThirdFlag, string otherOrderDocNo, string jsonResult);

        BaseResultDataValue GetUploadPlatformReaOrderDocAndDtl(long id, ref JObject jPostData, ref ReaBmsCenOrderDoc orderDoc);

        /// <summary>
        /// 更新订单同步到第三方系统标志，1为已经同步到第三方
        /// </summary>
        /// <param name="id">订单ID</param>
        /// <returns></returns>
        BaseResultDataValue EditReaBmsCenOrderDocThirdFlagAndStatus(long id, int isThirdFlag, int status);

        /// <summary>
        /// 客户端供货验收后，改写订单的单据状态（部分验收、全部验收）
        /// </summary>
        /// <param name="orderDocID">订单ID</param>
        /// <param name="tempDtlConfirmList">供货验收明细列表</param>
        /// <param name="empID">员工ID</param>
        /// <param name="empName">员工姓名</param>
        /// <returns></returns>
        BaseResultBool EditReaBmsCenOrderDocStatus(long orderDocID, IList<ReaBmsCenSaleDtlConfirm> tempDtlConfirmList, long empID, string empName);


    }
}