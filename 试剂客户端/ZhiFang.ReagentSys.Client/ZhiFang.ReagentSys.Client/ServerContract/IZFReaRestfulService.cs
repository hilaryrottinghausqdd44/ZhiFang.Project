using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.ReagentSys.Client.ServerContract
{
    /// <summary>
    /// 供试剂平台使用:按不同的应用场景作数据上传,下载处理
    /// </summary>
    [ServiceContract]
    public interface IZFReaRestfulService
    {
        #region 客户端与平台在同一数据库

        [ServiceContractDescription(Name = "智方客户端上传订单,只更新订单状态及数据标志,客户端与平台都在同一数据库", Desc = "智方客户端上传订单,只更新订单状态及数据标志,客户端与平台都在同一数据库", Url = "ZFReaRestfulService.svc/RS_UDTO_UpdateReaBmsCenOrderDocOfUploadByIdStr", Get = "", Post = "string", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsCenOrderDocOfUploadByIdStr", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsCenOrderDocOfUploadByIdStr(string idStr, bool isVerifyProdGoodsNo);

        [ServiceContractDescription(Name = "智方客户端订单取消上传,客户端与平台都在同一数据库", Desc = "智方客户端订单取消上传,客户端与平台都在同一数据库", Url = "ZFReaRestfulService.svc/RS_UDTO_UpdateReaBmsCenOrderDocOfCancelUpload", Get = "", Post = "ReaBmsCenOrderDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsCenOrderDocOfCancelUpload", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsCenOrderDocOfCancelUpload(ReaBmsCenOrderDoc entity, bool isVerifyProdGoodsNo, string fields);

        [ServiceContractDescription(Name = "供应商(供应商确认/取消确认)订单操作服务", Desc = "供应商(供应商确认/取消确认)订单操作服务", Url = "ZFReaRestfulService.svc/RS_UDTO_UpdateReaBmsCenOrderDocOfComp", Get = "", Post = "ReaBmsCenOrderDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsCenOrderDocOfComp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsCenOrderDocOfComp(ReaBmsCenOrderDoc entity, string fields);

        [ServiceContractDescription(Name = "客户端订单转为供应商的供货单(客户端与平台同在一数据库)", Desc = "客户端订单转为供应商的供货单(客户端与平台同在一数据库)", Url = "ZFReaRestfulService.svc/RS_UDTO_AddReaBmsCenSaleDocOfOrderToSupply", Get = "", Post = "orderId", Return = "BaseResultDataValue", ReturnType = "ReaBmsCenSaleDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddReaBmsCenSaleDocOfOrderToSupply", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddReaBmsCenSaleDocOfOrderToSupply(long orderId);

        [ServiceContractDescription(Name = "实验室按选择的供货单或选择的供应商+供货单号提取供货单,实验室与供应商同一数据库", Desc = "实验室按选择的供货单或选择的供应商+供货单号提取供货单,实验室与供应商同一数据库", Url = "ZFReaRestfulService.svc/RS_UDTO_UpdateReaBmsCenSaleDocOfExtract", Get = "", Post = "saleDocId,reaServerCompCode,saleDocNo,reaServerLabcCode", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsCenSaleDocOfExtract", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsCenSaleDocOfExtract(long saleDocId, string reaServerCompCode, string saleDocNo, string reaServerLabcCode);

        #endregion

        #region 客户端与平台不在同一数据库--客户端部分
        //,orderDoc,orderDtlList
        [ServiceContractDescription(Name = "智方客户端获取上传到平台供货商的订单服务", Desc = "智方客户端获取上传到平台供货商的订单服务", Url = "ZFReaRestfulService.svc/RS_Client_EditUploadPlatformReaOrderDocAndDtl", Get = "", Post = "appkey,timestamp,token,version,isVerifyProdGoodsNo,platformUrl", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_Client_EditUploadPlatformReaOrderDocAndDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_Client_EditUploadPlatformReaOrderDocAndDtl(long orderId, string appkey, string timestamp, string token, string version, string platformUrl, bool isVerifyProdGoodsNo, long empID, string empName);//ReaBmsCenOrderDoc orderDoc, IList<ReaBmsCenOrderDtl> orderDtlList, 

        [ServiceContractDescription(Name = "智方客户端获取智方平台供货商的供货总单(HQL)", Desc = "智方客户端获取智方平台供货商的供货总单(HQL)", Url = "ZFReaRestfulService.svc/RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocByHQL?labcCode={labcCode}&compCode={compCode}&platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "labcCode={labcCode}&compCode={compCode}&platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDoc>", ReturnType = "ListReaBmsCenSaleDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocByHQL?labcCode={labcCode}&compCode={compCode}&platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocByHQL(string platformUrl, string labcCode, string compCode, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "智方客户端从平台供货商提取供货信息并保存", Desc = "智方客户端从平台供货商提取供货信息并保存", Url = "ZFReaRestfulService.svc/RS_UDTO_AddSaleDocAndDtlOfPlatformExtract", Get = "", Post = "platformUrl,labcCode,compCode,saleDocId,saleDocNo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddSaleDocAndDtlOfPlatformExtract", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddSaleDocAndDtlOfPlatformExtract(string platformUrl, string labcCode, string compCode, long saleDocId, string saleDocNo);

        [ServiceContractDescription(Name = "客户端出库单上传至试剂平台", Desc = "", Url = "ZFReaRestfulService.svc/RS_Client_UploadOutDocAndDtlToPlatform", Get = "", Post = "outDocId,appkey,timestamp,token,version,platformUrl,empID,empName,deptID", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_Client_UploadOutDocAndDtlToPlatform", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_Client_UploadOutDocAndDtlToPlatform(long outDocId, string appkey, string timestamp, string token, string version, string platformUrl, long empID, string empName, long deptID);

        [ServiceContractDescription(Name = "智方客户端订单，取消上传", Desc = "", Url = "ZFReaRestfulService.svc/RS_Client_EditCancelUploadPlatformReaOrderDocAndDtl", Get = "", Post = "appkey,timestamp,token,version,isVerifyProdGoodsNo,platformUrl", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_Client_EditCancelUploadPlatformReaOrderDocAndDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_Client_EditCancelUploadPlatformReaOrderDocAndDtl(long orderId, string appkey, string timestamp, string token, string version, string platformUrl, bool isVerifyProdGoodsNo, long empID, string empName);
        #endregion

        #region 客户端与平台不在同一数据库--平台部分

        [ServiceContractDescription(Name = "智方平台供货商保存客户端上传的订单-字符串提交方式", Desc = "智方平台供货商保存客户端上传的订单)-字符串提交方式", Url = "ZFReaRestfulService.svc/RS_Client_AddReaOrderDocAndDtlOfUploadPlatform", Get = "", Post = "String", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_Client_AddReaOrderDocAndDtlOfUploadPlatform", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_Client_AddReaOrderDocAndDtlOfUploadPlatform(String postData);

        [ServiceContractDescription(Name = "智方平台供货商保存客户端上传的订单-实体提交方式", Desc = "智方平台供货商保存客户端上传的订单-实体提交方式", Url = "ZFReaRestfulService.svc/RS_Client_AddReaOrderDocAndDtlOfUploadPlatform2", Get = "", Post = "orderDoc,orderDtlList", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_Client_AddReaOrderDocAndDtlOfUploadPlatform2", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_Client_AddReaOrderDocAndDtlOfUploadPlatform2(ReaBmsCenOrderDoc orderDoc, IList<ReaBmsCenOrderDtl> orderDtlList);

        [ServiceContractDescription(Name = "智方平台供货商向智方客户端提供获取供货商供货总单(HQL)", Desc = "智方平台供货商向智方客户端提供获取供货商供货总单(HQL)", Url = "ZFReaRestfulService.svc/RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocToClientByHQL?labcCode={labcCode}&compCode={compCode}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "labcCode={labcCode}&compCode={compCode}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDoc>", ReturnType = "ListReaBmsCenSaleDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocToClientByHQL?labcCode={labcCode}&compCode={compCode}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchUploadPlatformReaBmsCenSaleDocToClientByHQL(string labcCode, string compCode, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "智方平台供货商向智方客户端提供获取供货商供货信息(总单+明细)", Desc = "智方平台供货商向智方客户端提供获取供货商供货信息(总单+明细)", Url = "ZFReaRestfulService.svc/RS_UDTO_SearchPlatformSaleDocAndDtlToClient?labcCode={labcCode}&compCode={compCode}&saleDocId={saleDocId}&saleDocNo={saleDocNo}", Get = "labcCode={labcCode}&compCode={compCode}&saleDocId={saleDocId}&saleDocNo={saleDocNo}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDoc>", ReturnType = "ListReaBmsCenSaleDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPlatformSaleDocAndDtlToClient?labcCode={labcCode}&compCode={compCode}&saleDocId={saleDocId}&saleDocNo={saleDocNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPlatformSaleDocAndDtlToClient(string labcCode, string compCode, long saleDocId, string saleDocNo);

        [ServiceContractDescription(Name = "客户端提取平台供货数据成功后,更新平台供货单的提取标志及状态(总单+明细)", Desc = "客户端提取平台供货数据成功后,更新平台供货单的提取标志及状态(总单+明细)", Url = "ZFReaRestfulService.svc/RS_UDTO_UpdatePlatformSaleDocAndDtlToClient?labcCode={labcCode}&compCode={compCode}&saleDocId={saleDocId}&saleDocNo={saleDocNo}", Get = "labcCode={labcCode}&compCode={compCode}&saleDocId={saleDocId}&saleDocNo={saleDocNo}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDoc>", ReturnType = "ListReaBmsCenSaleDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdatePlatformSaleDocAndDtlToClient?labcCode={labcCode}&compCode={compCode}&saleDocId={saleDocId}&saleDocNo={saleDocNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_UpdatePlatformSaleDocAndDtlToClient(string labcCode, string compCode, long saleDocId, string saleDocNo);

        [ServiceContractDescription(Name = "智方试剂平台端，接收出库单-字符串提交方式", Desc = "", Url = "ZFReaRestfulService.svc/RS_Platform_ReceiveOutDocAndDtlFormClient", Get = "", Post = "String", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_Platform_ReceiveOutDocAndDtlFormClient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_Platform_ReceiveOutDocAndDtlFormClient(String postData);

        [ServiceContractDescription(Name = "智方试剂平台端，接收客户端订单取消上传", Desc = "", Url = "ZFReaRestfulService.svc/RS_Platform_ReceiveCancelUploadOrder", Get = "", Post = "String", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_Platform_ReceiveCancelUploadOrder", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_Platform_ReceiveCancelUploadOrder(String postData);
        #endregion

    }
}
