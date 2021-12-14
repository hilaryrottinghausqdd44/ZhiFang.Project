using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaStoreIn;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using System.ServiceModel.Channels;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaSale;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.ReagentSys.Client.ServerContract
{
    /// <summary>
    /// 试剂定制接口
    /// </summary>
    [ServiceContract]
    public interface IReaCustomInterface
    {
        #region 四川大家NC接口

        [ServiceContractDescription(Name = "调用NC接口获取货品", Desc = "", Url = "ReaCustomInterface.svc/RS_GetReaGoodsByInterface?ts={ts}&supplierId={supplierId}", Get = "ts={ts}&supplierId={supplierId}", Post = "", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_GetReaGoodsByInterface?ts={ts}&supplierId={supplierId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_GetReaGoodsByInterface(string ts, long supplierId);

        [ServiceContractDescription(Name = "调用NC接口上传订单", Desc = "", Url = "ReaCustomInterface.svc/RS_SendBmsCenOrderByInterface?orderDocId={orderDocId}", Get = "orderDocId={orderDocId}", Post = "", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_SendBmsCenOrderByInterface?orderDocId={orderDocId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_SendBmsCenOrderByInterface(long orderDocId);
        
        [ServiceContractDescription(Name = "调用NC接口获取出库单", Desc = "", Url = "ReaCustomInterface.svc/RS_GetOutOrderInfoByInterface?ncBillNo={ncBillNo}", Get = "ncBillNo={ncBillNo}", Post = "", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_GetOutOrderInfoByInterface?ncBillNo={ncBillNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_GetOutOrderInfoByInterface(string ncBillNo);

        [ServiceContractDescription(Name = "调用NC接口上传出库单", Desc = "", Url = "ReaCustomInterface.svc/RS_SendOutInfoByInterface?outDocId={outDocId}", Get = "outDocId={outDocId}", Post = "", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_SendOutInfoByInterface?outDocId={outDocId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_SendOutInfoByInterface(long outDocId);

        #endregion
    }
}
