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
    [ServiceContract]
    public interface IReaInternalInterface
    {

        [ServiceContractDescription(Name = "人员数据同步接口", Desc = "人员数据同步接口", Url = "ReaInternalInterface.svc/RS_GetHREmployeeInfo?empCode={empCode}", Get = "empCode={empCode}", Post = "", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_GetHREmployeeInfo?empCode={empCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_GetHREmployeeInfo(string empCode);

        [ServiceContractDescription(Name = "科室数据同步接口", Desc = "科室数据同步接口", Url = "ReaInternalInterface.svc/RS_GetHRDeptInfo?deptCode={deptCode}", Get = "deptCode={deptCode}", Post = "", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_GetHRDeptInfo?deptCode={deptCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_GetHRDeptInfo(string deptCode);

        [ServiceContractDescription(Name = "库房数据同步接口", Desc = "库房数据同步接口", Url = "ReaInternalInterface.svc/RS_GetReaStorageInfo?storeCode={storeCode}", Get = "storeCode={storeCode}", Post = "", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_GetReaStorageInfo?storeCode={storeCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_GetReaStorageInfo(string storeCode);

        [ServiceContractDescription(Name = "供应商数据同步接口", Desc = "供应商数据同步接口", Url = "ReaInternalInterface.svc/RS_GetReaCenOrgInfo?compCode={compCode}", Get = "compCode={compCode}", Post = "", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_GetReaCenOrgInfo?compCode={compCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_GetReaCenOrgInfo(string compCode);

        [ServiceContractDescription(Name = "货品数据同步接口", Desc = "货品数据同步接口", Url = "ReaInternalInterface.svc/RS_GetReaGoodsInfo?goodsCode={goodsCode}&lastModifyDate={lastModifyDate}", Get = "goodsCode={goodsCode}&lastModifyDate={lastModifyDate}", Post = "", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_GetReaGoodsInfo?goodsCode={goodsCode}&lastModifyDate={lastModifyDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_GetReaGoodsInfo(string goodsCode, string lastModifyDate);

        [ServiceContractDescription(Name = "货品类别数据同步接口", Desc = "货品类别数据同步接口", Url = "ReaInternalInterface.svc/RS_GetReaGoodsTypeInfo?goodsTypeCode={goodsTypeCode}", Get = "goodsTypeCode={goodsTypeCode}", Post = "", Return = "BaseResultData", ReturnType = "BaseResultData")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_GetReaGoodsTypeInfo?goodsTypeCode={goodsTypeCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_GetReaGoodsTypeInfo(string goodsTypeCode);

        

    }
}
