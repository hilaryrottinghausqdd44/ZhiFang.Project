using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IMsgManageService”。
    [ServiceContract]
    public interface IMsgManageService
    {
        [ServiceContractDescription(Name = "危急值消息发送测试服务", Desc = "危急值消息发送测试服务", Url = "MsgManageService.svc/TestMsgSend", Get = "", Post = "msg", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/TestMsgSend", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue TestMsgSend(string msg);

        [ServiceContractDescription(Name = "危急值消息接收服务", Desc = "危急值消息接收服务", Url = "MsgManageService.svc/MsgAccept", Get = "", Post = "msg", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/MsgAccept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue MsgAccept(string msg);

        [ServiceContractDescription(Name = "获取Lis字典表信息", Desc = "获取Lis字典表信息", Url = "MsgManageService.svc/Msg_GetLisDictInfo?dictName={dictName}&strWhere={strWhere}", Get = "dictName={dictName}&strWhere={strWhere}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Msg_GetLisDictInfo?dictName={dictName}&strWhere={strWhere}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Msg_GetLisDictInfo(string dictName, string strWhere);

        [ServiceContractDescription(Name = "根据用户名和密码获取检验之星6.6用户信息", Desc = "根据用户名和密码获取检验之星6.6用户信息", Url = "MsgManageService.svc/Msg_GetUserInfoByPWD?userName={userName}&userPWD={userPWD}}", Get = "userName={userName}&userPWD={userPWD}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Msg_GetUserInfoByPWD?userName={userName}&userPWD={userPWD}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Msg_GetUserInfoByPWD(string userName, string userPWD);

        [ServiceContractDescription(Name = "根据用户名和密码获取检验之星6.6医生护士用户信息", Desc = "根据用户名和密码获取检验之星6.6医生护士用户信息", Url = "MsgManageService.svc/Msg_GetNPUserInfoByPWD?userName={userName}&userPWD={userPWD}}", Get = "userName={userName}&userPWD={userPWD}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Msg_GetNPUserInfoByPWD?userName={userName}&userPWD={userPWD}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Msg_GetNPUserInfoByPWD(string userName, string userPWD);

        [ServiceContractDescription(Name = "密码加密", Desc = "密码加密", Url = "MsgManageService.svc/Msg_CovertPassword?userPWD={userPWD}}", Get = "userPWD={userPWD}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Msg_CovertPassword?userPWD={userPWD}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Msg_CovertPassword(string userPWD);

        [ServiceContractDescription(Name = "密码解密", Desc = "密码解密", Url = "MsgManageService.svc/Msg_UnCovertPassword?userPWD={userPWD}}", Get = "userPWD={userPWD}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Msg_UnCovertPassword?userPWD={userPWD}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Msg_UnCovertPassword(string userPWD);

        [ServiceContractDescription(Name = "密码解密(如有不可见字符，解密失败)", Desc = "密码解密(如有不可见字符，解密失败)", Url = "MsgManageService.svc/Msg_UnCovertPasswordCheck?userPWD={userPWD}}", Get = "userPWD={userPWD}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Msg_UnCovertPasswordCheck?userPWD={userPWD}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Msg_UnCovertPasswordCheck(string userPWD);
    }
}
