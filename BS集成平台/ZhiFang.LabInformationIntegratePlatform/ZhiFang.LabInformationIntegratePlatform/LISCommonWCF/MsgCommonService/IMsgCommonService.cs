using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.LISCommonWCF.MsgCommonService
{
    [ServiceContract(Namespace = "ZhiFang.LabInformationIntegratePlatform.LISCommonWCF")]
    public interface IMsgCommonService
    {

        [ServiceContractDescription(Name = "新增第三方消息", Desc = "新增第三方消息", Url = "MsgCommonService.svc/MsgSend", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/MsgSend", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue MsgSend(SCMsg_OTTH entity);

        [ServiceContractDescription(Name = "查询第三方消息处理", Desc = "查询第三方消息处理", Url = "MsgCommonService.svc/MsgHandleSearch", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/MsgHandleSearch", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue MsgHandleSearch(SCMsg_OTTH_Search entity);
    }
}
