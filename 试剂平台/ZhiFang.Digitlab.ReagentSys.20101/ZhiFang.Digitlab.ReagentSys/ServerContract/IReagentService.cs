using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.ServiceModel;
using System.Web;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using ZhiFang.Digitlab.ServiceCommon;
using ZhiFang.Digitlab.Entity;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
using ZhiFang.Digitlab.Entity.ReagentSys;

namespace ZhiFang.Digitlab.ReagentSys.ServerContract
{
    [ServiceContract]
    public interface IReagentService
    {
        [ServiceContractDescription(Name = "新增供货单明细条码", Desc = "新增供货单明细条码", Url = "ReagentService.svc/ST_UDTO_AddBmsCenSaleDtlBarCodeList", Get = "", Post = "SaleDtlID, SaleDtlBarCodeIDList, BmsCenSaleDtlBarCodeList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsCenSaleDtlBarCodeList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsCenSaleDtlBarCodeList(long SaleDtlID, string SaleDtlBarCodeIDList, IList<BmsCenSaleDtlBarCode> BmsCenSaleDtlBarCodeList);

        [ServiceContractDescription(Name = "库存表操作", Desc = "库存表操作", Url = "ReagentService.svc/ST_UDTO_MigrationCenQtyDtlTemp?QtyDtlIDList={QtyDtlIDList}", Get = "QtyDtlIDList={QtyDtlIDList}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_MigrationCenQtyDtlTemp?QtyDtlIDList={QtyDtlIDList}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_MigrationCenQtyDtlTemp(string QtyDtlIDList);

        [ServiceContractDescription(Name = "获取最大的机构编号", Desc = "获取最大的机构编号", Url = "ReagentService.svc/ST_UDTO_GetMaxOrgNoByOrgType?orgTypeID={orgTypeID}&minOrgNo={minOrgNo}", Get = "orgTypeID={orgTypeID}&minOrgNo={minOrgNo}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetMaxOrgNoByOrgType?orgTypeID={orgTypeID}&minOrgNo={minOrgNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetMaxOrgNoByOrgType(long orgTypeID, int minOrgNo);

        [ServiceContractDescription(Name = "获取最大的机构编号", Desc = "获取最大的机构编号", Url = "ReagentService.svc/ST_UDTO_GetMaxOrgNo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetMaxOrgNo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetMaxOrgNo();

        [ServiceContractDescription(Name = "试剂消耗统计", Desc = "试剂消耗统计", Url = "ReagentService.svc/ST_UDTO_StatReagentConsume", Get = "", Post = "strPara, groupByType, fields, isPlanish", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_StatReagentConsume", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_StatReagentConsume(string strPara, int groupByType, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "导入供货单Excel文件", Desc = "导入供货单Excel文件", Url = "ReagentService.svc/ST_UDTO_UploadBmsCenSaleDocDataByExcel", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UploadBmsCenSaleDocDataByExcel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message ST_UDTO_UploadBmsCenSaleDocDataByExcel();

        [ServiceContractDescription(Name = "导入产品Excel文件", Desc = "导入产品Excel文件", Url = "ReagentService.svc/ST_UDTO_UploadGoodsDataByExcel", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UploadGoodsDataByExcel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message ST_UDTO_UploadGoodsDataByExcel();

        [ServiceContractDescription(Name = "查询平台产品(HQL)", Desc = "查询平台产品(HQL)", Url = "ReagentService.svc/RS_UDTO_SearchGoodsByHQL?compID={compID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "compID={compID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Goods>", ReturnType = "ListGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchGoodsByHQL?compID={compID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]                                           
        BaseResultDataValue RS_UDTO_SearchGoodsByHQL(long compID, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "新增平台订货总单（定制）", Desc = "新增平台订货总单（定制）", Url = "ReagentService.svc/RS_UDTO_AddBmsCenOrderDoc", Get = "", Post = "entity, IsWriteExternalSystem", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddBmsCenOrderDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddBmsCenOrderDoc(BmsCenOrderDoc entity, int IsWriteExternalSystem);

        [ServiceContractDescription(Name = "更新平台订货总单和明细", Desc = "更新平台订货总单和明细", Url = "ReagentService.svc/RS_UDTO_UpdateBmsCenOrderDoc", Get = "", Post = "bmsCenOrderDoc, mainFields, listAddBmsCenOrderDtl, listUpdateBmsCenOrderDtl, childFields, delBmsCenOrderDtlID, IsWriteExternalSystem, IsAutoCreateBmsCenSaleDoc", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateBmsCenOrderDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_UpdateBmsCenOrderDoc(BmsCenOrderDoc bmsCenOrderDoc, string mainFields, IList<BmsCenOrderDtl> listAddBmsCenOrderDtl, IList<BmsCenOrderDtl> listUpdateBmsCenOrderDtl, string childFields, string delBmsCenOrderDtlID, int IsWriteExternalSystem, int IsAutoCreateBmsCenSaleDoc);

        [ServiceContractDescription(Name = "自动生成供货单", Desc = "自动生成供货单", Url = "ReagentService.svc/RS_UDTO_BmsCenSaleDocByOrderDoc", Get = "", Post = "bmsCenOrderDoc, fields", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_BmsCenSaleDocByOrderDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_BmsCenSaleDocByOrderDoc(BmsCenOrderDoc entity, string fields);

        [ServiceContractDescription(Name = "复制产品Good服务", Desc = "复制产品Good服务", Url = "ReagentService.svc/RS_UDTO_CopyGoodsByID", Get = "", Post = "listID, compId, cenOrgId", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_CopyGoodsByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_CopyGoodsByID(string listID, long compId, long cenOrgId);

        [ServiceContractDescription(Name = "批量审核和拆分供货单服务", Desc = "批量审核和拆分供货单服务", Url = "ReagentService.svc/RS_UDTO_CheckSaleByDocIDList", Get = "", Post = "listID,isSplit", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_CheckSaleByDocIDList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_CheckSaleByDocIDList(string listID, int isSplit);

        [ServiceContractDescription(Name = "供货单验收服务（包含子单异常信息）", Desc = "供货单验收服务（包含子单异常信息）", Url = "ReagentService.svc/RS_UDTO_ConfirmSaleDocByIDAndDtlIDList", Get = "", Post = "saleDocID,saleDtlIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_ConfirmSaleDocByIDAndDtlIDList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_ConfirmSaleDocByIDAndDtlIDList(string saleDocID, string saleDtlIDList);

        [ServiceContractDescription(Name = "验收确认供货单服务", Desc = "验收确认供货单服务", Url = "ReagentService.svc/RS_UDTO_ConfirmSaleByDocID", Get = "", Post = "docID,invoiceNo,accepterMemo,secAccepterAccount,secAccepterPwd,secAccepterType,listBmsCenSaleDtl,isSplit", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_ConfirmSaleByDocID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_ConfirmSaleByDocID(string docID, string invoiceNo, string accepterMemo, string secAccepterAccount, string secAccepterPwd, int secAccepterType, IList<BmsCenSaleDtl> listBmsCenSaleDtl, int isSplit);

        [ServiceContractDescription(Name = "供货单验收服务(盒条码和非盒条码)", Desc = "供货单验收服务(盒条码和非盒条码)", Url = "ReagentService.svc/RS_UDTO_ConfirmSaleDocByID", Get = "", Post = "docID,invoiceNo,accepterMemo,secAccepterAccount,secAccepterPwd,secAccepterType,listSaleDtlError,listSaleDtlNoBarCode,isSplit", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_ConfirmSaleDocByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_ConfirmSaleDocByID(string docID, string invoiceNo, string accepterMemo, string secAccepterAccount, string secAccepterPwd, int secAccepterType, IList<BmsCenSaleDtl> listSaleDtlError, IList<BmsCenSaleDtl> listSaleDtlNoBarCode, int isSplit);

        [ServiceContractDescription(Name = "供货单取消验收服务", Desc = "供货单取消验收服务", Url = "ReagentService.svc/RS_UDTO_ConfirmSaleCancel", Get = "", Post = "docID,reason,accepterAccount,accepterPwd", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_ConfirmSaleCancel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_ConfirmSaleCancel(string docID, string reason, string accepterAccount, string accepterPwd);

        [ServiceContractDescription(Name = "供货单取消拆分服务", Desc = "供货单取消拆分服务", Url = "ReagentService.svc/RS_UDTO_SplitSaleDocCancel", Get = "", Post = "docID,reason", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SplitSaleDocCancel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SplitSaleDocCancel(string docID, string reason);

        [ServiceContractDescription(Name = "供货单拆分服务", Desc = "供货单拆分服务", Url = "ReagentService.svc/RS_UDTO_SplitSaleDocByID", Get = "", Post = "docID,splitType,compatibleType", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SplitSaleDocByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SplitSaleDocByID(string docID, int splitType, int compatibleType);

        [ServiceContractDescription(Name = "供货单审核服务", Desc = "供货单审核服务（只审核不拆分）", Url = "ReagentService.svc/RS_UDTO_CheckSaleDocByID", Get = "", Post = "docID", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_CheckSaleDocByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_CheckSaleDocByID(string docID);

        [ServiceContractDescription(Name = "供货单审核服务", Desc = "供货单审核服务", Url = "ReagentService.svc/RS_UDTO_CheckSaleDocByIDList", Get = "", Post = "idList, validateType", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_CheckSaleDocByIDList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_CheckSaleDocByIDList(string idList, int validateType);

        [ServiceContractDescription(Name = "订货单删除标志服务", Desc = "订货单删除标志服务", Url = "ReagentService.svc/RS_UDTO_SetOrderDocDeleteFlagByID", Get = "", Post = "idList,deleteFlag", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SetOrderDocDeleteFlagByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SetOrderDocDeleteFlagByID(string idList, int deleteFlag);

        [ServiceContractDescription(Name = "供货单删除标志服务", Desc = "供货单删除标志服务", Url = "ReagentService.svc/RS_UDTO_SetSaleDocDeleteFlagByID", Get = "", Post = "idList,deleteFlag", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SetSaleDocDeleteFlagByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SetSaleDocDeleteFlagByID(string idList, int deleteFlag);

        [ServiceContractDescription(Name = "试剂信息列表导出Excel", Desc = "试剂信息列表导出Excel", Url = "ReagentService.svc/RS_UDTO_ReportDetailToExcel?reportType={reportType}&idList={idList}&operateType={operateType}&isHeader={isHeader}", Get = "reportType={reportType}&idList={idList}&operateType={operateType}&isHeader={isHeader}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/RS_UDTO_ReportDetailToExcel?reportType={reportType}&idList={idList}&operateType={operateType}&isHeader={isHeader}")]
        [OperationContract]
        Stream RS_UDTO_ReportDetailToExcel(int reportType, string idList, int operateType, int isHeader);

        //[ServiceContractDescription(Name = "试剂信息列表导出Excel", Desc = "试剂信息列表导出Excel", Url = "ReagentService.svc/RS_UDTO_ReportDetailToExcel", Get = "", Post = "reportType,idList,operateType", Return = "Stream", ReturnType = "Stream")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "/RS_UDTO_ReportDetailToExcel")]
        //[OperationContract]
        //Stream RS_UDTO_ReportDetailToExcel(int reportType, string idList, int operateType);

        [ServiceContractDescription(Name = "试剂信息列表导出Excel文件路径", Desc = "试剂信息列表导出Excel文件路径", Url = "ReagentService.svc/RS_UDTO_GetReportDetailExcelPath", Get = "", Post = "reportType,idList,where,isHeader", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_GetReportDetailExcelPath", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message RS_UDTO_GetReportDetailExcelPath();

        [ServiceContractDescription(Name = "下载格式有问题的Excel文件", Desc = "下载格式有问题的Excel文件", Url = "ReagentService.svc/RS_UDTO_DownLoadExcel?fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}", Get = "fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/RS_UDTO_DownLoadExcel?fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}")]
        [OperationContract]
        Stream RS_UDTO_DownLoadExcel(string fileName, string downFileName, int isUpLoadFile, int operateType);

        [ServiceContractDescription(Name = "查询供应商试剂", Desc = "查询供应商试剂", Url = "ReagentService.svc/RS_UDTO_SearchGoodsByCompID?labCenOrgID={labCenOrgID}&compCenOrgID={compCenOrgID}&where={where}&fields={fields}&page={page}&limit={limit}&sort={sort}&isPlanish={isPlanish}", Get = "labCenOrgID={labCenOrgID}&compCenOrgID={compCenOrgID}&where={where}&fields={fields}&page={page}&limit={limit}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchGoodsByCompID?labCenOrgID={labCenOrgID}&compCenOrgID={compCenOrgID}&where={where}&fields={fields}&page={page}&limit={limit}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchGoodsByCompID(string labCenOrgID, string compCenOrgID, string where, string fields, int page, int limit, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "供货单明细金额统计", Desc = "供货单明细金额统计", Url = "ReagentService.svc/RS_UDTO_StatBmsCenSaleDocTotalPrice", Get = "", Post = "listID,fields", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_StatBmsCenSaleDocTotalPrice", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message RS_UDTO_StatBmsCenSaleDocTotalPrice();

        //[ServiceContractDescription(Name = "迈克生物供货单接口", Desc = "迈克生物供货单接口", Url = "ReagentService.svc/RS_UDTO_MaiKeInterfaceSaleDoc", Get = "", Post = "orgNo,orgName,saleDocNo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_MaiKeInterfaceSaleDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        [ServiceContractDescription(Name = "迈克生物供货单接口", Desc = "迈克生物供货单接口", Url = "ReagentService.svc/RS_UDTO_InputSaleDocInterface?saleDocNo={saleDocNo}&compOrgId={compOrgId}&labOrgId={labOrgId}", Get = "saleDocNo={saleDocNo}&compOrgId={compOrgId}&labOrgId={labOrgId}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_InputSaleDocInterface?saleDocNo={saleDocNo}&compOrgId={compOrgId}&labOrgId={labOrgId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_InputSaleDocInterface(string saleDocNo, long compOrgId, long labOrgId);

        [ServiceContractDescription(Name = "订单审核推送服务", Desc = "订单审核推送服务", Url = "ReagentService.svc/RS_UDTO_CheckBmsCenOrderDocByID", Get = "", Post = "id", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_CheckBmsCenOrderDocByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        //[ServiceContractDescription(Name = "订单审核推送服务", Desc = "订单审核推送服务", Url = "ReagentService.svc/RS_UDTO_CheckBmsCenOrderDocByID?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_CheckBmsCenOrderDocByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        BaseResultDataValue RS_UDTO_CheckBmsCenOrderDocByID(long id);

        [ServiceContractDescription(Name = "订单同步到其他系统平台", Desc = "订单同步到其他系统平台", Url = "ReagentService.svc/RS_UDTO_OrderSaveToOtherSystem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_OrderSaveToOtherSystem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_OrderSaveToOtherSystem(long id);

        [ServiceContractDescription(Name = "更新订货单总价服务", Desc = "更新订货单总价服务", Url = "ReagentService.svc/RS_UDTO_EditBmsCenOrderDocTotalPrice?orderDocID={orderDocID}", Get = "orderDocID={orderDocID}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_EditBmsCenOrderDocTotalPrice?orderDocID={orderDocID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_EditBmsCenOrderDocTotalPrice(long orderDocID);

        [ServiceContractDescription(Name = "更新供货单总价服务", Desc = "更新供货单总价服务", Url = "ReagentService.svc/RS_UDTO_EditBmsCenSaleDocTotalPrice?saleDocID={saleDocID}", Get = "saleDocID={saleDocID}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_EditBmsCenSaleDocTotalPrice?saleDocID={saleDocID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_EditBmsCenSaleDocTotalPrice(long saleDocID);

        [ServiceContractDescription(Name = "是否调用第三方供货单接口", Desc = "是否调用第三方供货单接口", Url = "ReagentService.svc/RS_UDTO_IsUseSaleDocInterface", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_IsUseSaleDocInterface", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_IsUseSaleDocInterface();

        [ServiceContractDescription(Name = "获取实验室配置的供货单接口机构列表", Desc = "获取实验室配置的供货单接口机构列表", Url = "ReagentService.svc/RS_UDTO_GetLabInterfaceOrgList", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_GetLabInterfaceOrgList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_GetLabInterfaceOrgList();

        [ServiceContractDescription(Name = "迈克生物供货单接口", Desc = "迈克生物供货单接口", Url = "ReagentService.svc/WebRequestHttpPost", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WebRequestHttpPost", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string WebRequestHttpPost();

        [ServiceContractDescription(Name = "自动审核订单", Desc = "自动审核订单", Url = "ReagentService.svc/RS_UDTO_AutoCheckOrderDoc?orderDocId={orderDocId}", Get = "orderDocId={orderDocId}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AutoCheckOrderDoc?orderDocId={orderDocId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AutoCheckOrderDoc(long orderDocId);

        [ServiceContractDescription(Name = "", Desc = "", Url = "ReagentService.svc/WebRequestHttpPostWMS", Get = "", Post = "saleDocNo", Return = "string", ReturnType = "string")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WebRequestHttpPostWMS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string WebRequestHttpPostWMS(string saleDocNo);

        [ServiceContractDescription(Name = "供货单统计服务", Desc = "供货单统计服务", Url = "ReagentService.svc/RS_UDTO_BmsCenSaleDtlStat?beginDate={beginDate}&endDate={endDate}&listStatus={listStatus}&compID={compID}&labID={labID}&goodID={goodID}&goodLotNo={goodLotNo}&groupbyType={groupbyType}&fields={fields}&isPlanish={isPlanish}&page={page}&limit={limit}&sort={sort}", Get = "beginDate={beginDate}&endDate={endDate}&listStatus={listStatus}&compID={compID}&labID={labID}&goodID={goodID}&goodLotNo={goodLotNo}&groupbyType={groupbyType}&fields={fields}&isPlanish={isPlanish}&page={page}&limit={limit}&sort={sort}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_BmsCenSaleDtlStat?beginDate={beginDate}&endDate={endDate}&listStatus={listStatus}&compID={compID}&labID={labID}&goodID={goodID}&goodLotNo={goodLotNo}&groupbyType={groupbyType}&fields={fields}&isPlanish={isPlanish}&page={page}&limit={limit}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_BmsCenSaleDtlStat(string beginDate, string endDate, string listStatus, long compID, long labID, long goodID, string goodLotNo, int groupbyType, string fields, bool isPlanish, int page, int limit, string sort);  

    }
}
