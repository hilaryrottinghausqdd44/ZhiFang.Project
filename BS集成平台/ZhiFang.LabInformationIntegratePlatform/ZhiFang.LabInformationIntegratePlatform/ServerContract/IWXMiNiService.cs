using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.Entity.RBAC;
using ZhiFang.LIIP.WeiXin.Mini;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.LIIP")]
    public interface IWXMiniService
    {
        //[ServiceContractDescription(Name = "用户登陆服务", Desc = "用户登陆服务", Url = "WXMiniService.svc/RBAC_BA_Login?strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}", Get = "strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}", Post = "", Return = "bool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_BA_Login?strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //bool RBAC_BA_Login(string strUserAccount, string strPassWord, bool isValidate);

        [ServiceContractDescription(Name = "用户登陆服务并且绑定微信用户", Desc = "用户登陆服务并且绑定微信用户", Url = "WXMiniService.svc/RBAC_BA_LoginAndBindingByCode?strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}&Code={Code}", Get = "strUserAccount={strUserAccount}&strPassWord={strPassWord}&Code={Code}", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_BA_LoginAndBindingByCode?strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}&Code={Code}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_BA_LoginAndBindingByCode(string strUserAccount, string strPassWord, bool isValidate, string Code);

        [ServiceContractDescription(Name = "获取微信小程序用户认证OpenID", Desc = "获取微信小程序用户认证OpenID", Url = "WXMiniService.svc/WX_Mini_GetOpenIdByCode?Code={Code}", Get = "WX_Mini_GetOpenIdByCode?Code={Code}", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WX_Mini_GetOpenIdByCode?Code={Code}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WX_Mini_GetOpenIdByCode(string Code);

        [ServiceContractDescription(Name = "查询B_County", Desc = "查询B_County", Url = "WXMiniService.svc/WX_Mini_RegeditWeiXinAccount", Get = "", Post = "WeiXinMiniClientUserInfo", Return = "bool", ReturnType = "bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WX_Mini_RegeditWeiXinAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WX_Mini_RegeditWeiXinAccount(WeiXinMiniClientUserInfo entity);


        [ServiceContractDescription(Name = "获取微信小程序用户账户状态", Desc = "获取微信小程序用户账户状态", Url = "WXMiniService.svc/WX_Mini_CheckWeiXinAccountByWeiXinMiniOpenID", Get = "", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WX_Mini_CheckWeiXinAccountByWeiXinMiniOpenID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WX_Mini_CheckWeiXinAccountByWeiXinMiniOpenID();

        [ServiceContractDescription(Name = "获取微信小程序用户申请状态", Desc = "获取微信小程序用户申请状态", Url = "WXMiniService.svc/WX_Mini_GetBSAccountRegisterStatusByAuth", Get = "WX_Mini_GetBSAccountRegisterStatusByAuth", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WX_Mini_GetBSAccountRegisterStatusByAuth", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WX_Mini_GetBSAccountRegisterStatusByAuth();

        [ServiceContractDescription(Name = "新增S_AccountRegister", Desc = "新增S_AccountRegister", Url = "WXMiniService.svc/ST_UDTO_AddSAccountRegister", Get = "", Post = "SAccountRegister", Return = "BaseResultDataValue", ReturnType = "SAccountRegister")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSAccountRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSAccountRegister(SAccountRegister entity);

        [ServiceContractDescription(Name = "根据Cookie中的人员获取权限树仅展示两级", Desc = "根据Cookie中的人员获取权限树仅展示两级", Url = "WXMiniService.svc/RBAC_UDTO_SearchModuleTreeTwoStageByModuleID?ModuleID={ModuleID}", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeRBACModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchModuleTreeTwoStageByModuleID?ModuleID={ModuleID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchModuleTreeTwoStageByModuleID(long ModuleID);

        [ServiceContractDescription(Name = "根据Cookie中的人员获取权限树仅展示两级", Desc = "根据Cookie中的人员获取权限树仅展示两级", Url = "WXMiniService.svc/RBAC_UDTO_SearchModuleTreeTwoStageByModuleCode?ModuleCode={ModuleCode}", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeRBACModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchModuleTreeTwoStageByModuleCode?ModuleCode={ModuleCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchModuleTreeTwoStageByModuleCode(string ModuleCode);

        [ServiceContractDescription(Name = "根据OpenId登录", Desc = "根据OpenId登录", Url = "WXMiniService.svc/WX_Mini_LoginByOpenid", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WX_Mini_LoginByOpenid", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WX_Mini_LoginByOpenid();

        [ServiceContractDescription(Name = "获取员工信息", Desc = "获取员工信息", Url = "WXMiniService.svc/WX_Mini_GetEmpInfoById?fields={fields}&isPlanish={isPlanish}", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WX_Mini_GetEmpInfoById?fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WX_Mini_GetEmpInfoById(string fields, bool isPlanish);

        [ServiceContractDescription(Name = "获取医院信息", Desc = "获取医院信息", Url = "WXMiniService.svc/WX_Mini_GetHosptialInfoById?fields={fields}&isPlanish={isPlanish}", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WX_Mini_GetHosptialInfoById?fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WX_Mini_GetHosptialInfoById(string fields, bool isPlanish);
    }
}

