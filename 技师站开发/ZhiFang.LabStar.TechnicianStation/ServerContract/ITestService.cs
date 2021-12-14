using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        void DoWork();

        //[ServiceContractDescription(Name = "", Desc = "", Url = "TestService.svc/SendCommMessages", Get = "", Post = "", Return = "string", ReturnType = "string")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SendCommMessages", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //string SendCommMessages(string MessageInfo, string MessageType, string FormUserEmpId, string FormUserEmpName);


        [ServiceContractDescription(Name = "", Desc = "", Url = "TestService.svc/SendCommMessages?MessageInfo={MessageInfo}&MessageType={MessageType}&FormUserEmpId={FormUserEmpId}&FormUserEmpName={FormUserEmpName}", Get = "MessageInfo={MessageInfo}&MessageType={MessageType}&FormUserEmpId={FormUserEmpId}&FormUserEmpName={FormUserEmpName}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SendCommMessages?MessageInfo={MessageInfo}&MessageType={MessageType}&FormUserEmpId={FormUserEmpId}&FormUserEmpName={FormUserEmpName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string SendCommMessages(string MessageInfo, string MessageType, string FormUserEmpId, string FormUserEmpName);
    }
}
