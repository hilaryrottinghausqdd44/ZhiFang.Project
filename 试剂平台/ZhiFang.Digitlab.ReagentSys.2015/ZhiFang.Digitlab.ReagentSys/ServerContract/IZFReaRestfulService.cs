using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.ServiceModel;
using System.Web;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.ServiceCommon;

namespace ZhiFang.Digitlab.ReagentSys.ServerContract
{
    [ServiceContract]
    public interface IZFReaRestfulService
    {
        [ServiceContractDescription(Name = "同步订货单信息", Desc = "同步订货单信息", Url = "ZFReaRestfulService.svc/RS_OrderDocCreate", Get = "", Post = "appkey,timestamp,data,token,version", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_OrderDocCreate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_OrderDocCreate(string appkey, string timestamp, string data, string token, string version);

        [ServiceContractDescription(Name = "同步供货单信息", Desc = "同步供货单信息", Url = "ZFReaRestfulService.svc/RS_SaleDocCreate", Get = "", Post = "appkey,timestamp,data,token,version", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_SaleDocCreate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_SaleDocCreate(string appkey, string timestamp, string data, string token, string version);


        #region 试剂平台与试剂客户端的服务接口
        [ServiceContractDescription(Name = "客户端物理删除平台订单信息(订单状态都为暂存或已提交才能删除)", Desc = "客户端物理删除平台订单信息(订单状态都为暂存或已提交才能删除)", Url = "ZFReaRestfulService.svc/RS_UDTO_DelBmsCenOrderDocByLabOrgNoAndOrderDocNo?labOrgNo={labOrgNo}&orderDocNo={orderDocNo}", Get = "labOrgNo={labOrgNo}&orderDocNo={orderDocNo}", Post = "", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RS_UDTO_DelBmsCenOrderDocByLabOrgNoAndOrderDocNo?labOrgNo={labOrgNo}&orderDocNo={orderDocNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_DelBmsCenOrderDocByLabOrgNoAndOrderDocNo(string labOrgNo, string orderDocNo);

        [ServiceContractDescription(Name = "获取供货验收主单列表信息(HQL)", Desc = "获取供货验收主单列表信息(HQL)", Url = "ZFReaRestfulService.svc/RS_UDTO_SearchBmsCenSaleDocConfirmByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Goods>", ReturnType = "ListGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchBmsCenSaleDocConfirmByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchBmsCenSaleDocConfirmByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "依某一验收主单ID获取供货验收明细信息", Desc = "依某一验收主单ID获取供货验收明细信息", Url = "ZFReaRestfulService.svc/RS_UDTO_SearchBmsCenSaleDtlConfirmByDocId?docId={docId}&fields={fields}&isPlanish={isPlanish}", Get = "docId={docId}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Goods>", ReturnType = "ListGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchBmsCenSaleDtlConfirmByDocId?docId={docId}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchBmsCenSaleDtlConfirmByDocId(long docId, string fields, bool isPlanish);

        #endregion

        [ServiceContractDescription(Name = "智方客户端获取平台供货单服务", Desc = "智方客户端获取平台供货单服务", Url = "ZFReaRestfulService.svc/RS_Client_GetBmsCenSaleDoc", Get = "", Post = "appkey,timestamp,data,token,version", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_Client_GetBmsCenSaleDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_Client_GetBmsCenSaleDoc(string appkey, string timestamp, string data, string token, string version);

        [ServiceContractDescription(Name = "更新平台供货单提取标志服务", Desc = "更新平台供货单提取标志服务", Url = "ZFReaRestfulService.svc/RS_Client_UpdateBmsCenSaleDocExtractFlag", Get = "", Post = "appkey,timestamp,data,token,version", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_Client_UpdateBmsCenSaleDocExtractFlag", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_Client_UpdateBmsCenSaleDocExtractFlag(string appkey, string timestamp, string data, string token, string version);

        [ServiceContractDescription(Name = "智方客户端上传订单到平台服务", Desc = "智方客户端上传订单到平台服务", Url = "ZFReaRestfulService.svc/RS_Client_OrderDocCreate", Get = "", Post = "appkey,timestamp,data,token,version", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_Client_OrderDocCreate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_Client_OrderDocCreate(string appkey, string timestamp, string data, string token, string version);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_Client_UpdateDownloadFlagByLab?validatePara={validatePara}&labID={labID}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_Client_UpdateDownloadFlagByLab(string validatePara, string labID, string startDate, string endDate);

        [ServiceContractDescription(Name = "智方客户端获取平台供货单服务", Desc = "智方客户端获取平台供货单服务", Url = "ZFReaRestfulService.svc/RS_Test", Get = "appkey={appkey}&timestamp={timestamp}&data={data}&token={token}&version={version}", Post = "", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_Test?appkey={appkey}&timestamp={timestamp}&data={data}&token={token}&version={version}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_Test(string appkey, string timestamp, string data, string token, string version);

    }
}
