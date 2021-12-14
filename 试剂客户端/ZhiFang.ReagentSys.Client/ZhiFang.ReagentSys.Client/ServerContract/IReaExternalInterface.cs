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
    public interface IReaExternalInterface
    {
        [ServiceContractDescription(Name = "", Desc = "", Url = "ReaExternalInterface.svc/Test", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Test", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Test();


        [ServiceContractDescription(Name = "统一登录定制接口", Desc = "", Url = "ReaExternalInterface.svc/RBAC_BA_UnifiedLogin", Get = "", Post = "orgCode,staffCode,deptCode,timestamp,sign", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_BA_UnifiedLogin", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_BA_UnifiedLogin(string orgCode, string staffCode, string deptCode, string timestamp, string sign);

        [ServiceContractDescription(Name = "安阳市第六医院一体化平台统一登录定制接口", Desc = "安阳市第六医院一体化平台统一登录定制接口", Url = "ReaExternalInterface.svc/RBAC_BA_AnYangLiuYuanUnifiedLogin", Get = "", Post = "orgCode,staffCode,deptCode,timestamp,sign", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_BA_AnYangLiuYuanUnifiedLogin", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_BA_AnYangLiuYuanUnifiedLogin(string orgCode, string staffCode, string deptCode, string timestamp, string sign);

    }
}
