using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.BusinessObject;

namespace ZhiFang.WeiXin.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.WeiXinAppService")]
    public interface IZhiFangWeiXinAppService
    {
        [ServiceContractDescription(Name = "用户订单退费申请", Desc = "用户订单退费申请", Url = "ZhiFangWeiXinAppService.svc/ST_UDTO_OSUserOrderFormRefundU?OrderFormCode={OrderFormCode}&MessageStr={MessageStr}", Get = "OrderFormCode={OrderFormCode}&MessageStr={MessageStr}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_OSUserOrderFormRefundU?OrderFormCode={OrderFormCode}&MessageStr={MessageStr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_OSUserOrderFormRefundU(string OrderFormCode, string MessageStr);

        [ServiceContractDescription(Name = "患者强制登陆服务", Desc = "患者强制登陆服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_Login?password={password}", Get = "password={password}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_BA_Login?password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_BA_Login(string password);

        //[ServiceContractDescription(Name = "修改患者密码服务", Desc = "修改患者密码服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_ChangePassword?oldPassword={oldPassword}&newPassword={newPassword}", Get = "oldPassword={oldPassword}&newPassword={newPassword}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_BA_ChangePassword?oldPassword={oldPassword}&newPassword={newPassword}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue WXAS_BA_ChangePassword(string oldPassword, string newPassword);

        [ServiceContractDescription(Name = "修改患者密码服务", Desc = "修改患者密码服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_ChangePasswordByVerificationCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WXAS_BA_ChangePasswordByVerificationCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_BA_ChangePasswordByVerificationCode(string VerificationCode, string newPassword);

        [ServiceContractDescription(Name = "获取验证码", Desc = "获取验证码", Url = "ZhiFangWeiXinAppService.svc/GetVerificationCode?MobileCode={MobileCode}", Get = "MobileCode={MobileCode}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetVerificationCode?MobileCode={MobileCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetVerificationCode(string MobileCode);

        [ServiceContractDescription(Name = "获取患者信息服务", Desc = "获取患者信息服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_GetPatientInformation", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_BA_GetPatientInformation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_BA_GetPatientInformation();

        [ServiceContractDescription(Name = "是否开启密码登录服务", Desc = "是否开启密码登录服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_IsPasswordLogin?isPassword={isPassword}", Get = "isPassword={isPassword}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_BA_IsPasswordLogin?isPassword={isPassword}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_BA_IsPasswordLogin(bool isPassword);

        [ServiceContractDescription(Name = "根据微信账号得到账号信息服务", Desc = "根据微信账号得到账号信息服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_GetWinXinAccountInfo?account={account}", Get = "account={account}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_BA_GetWinXinAccountInfo?account={account}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_BA_GetWinXinAccountInfo(string account);

        [ServiceContractDescription(Name = "根据微信账号得到账号信息服务", Desc = "根据微信账号得到账号信息服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_GetWinXinAccountInfoByFields?account={account}&fields={fields}", Get = "account={account}&fields={fields}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_BA_GetWinXinAccountInfoByFields?account={account}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_BA_GetWinXinAccountInfoByFields(string account, string fields);

        [ServiceContractDescription(Name = "通过订单ID获取患者订单信息服务", Desc = "通过订单ID获取患者订单信息服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_GetOSUserOrderFormByID?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_BA_GetOSUserOrderFormByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_BA_GetOSUserOrderFormByID(long id);

        [ServiceContractDescription(Name = "查询患者订单信息服务", Desc = "查询患者订单信息服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_SearchOSUserOrderForm", Get = "", Post = "page,limit", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WXAS_BA_SearchOSUserOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        //[ServiceContractDescription(Name = "查询患者订单信息服务", Desc = "查询患者订单信息服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_SearchOSUserOrderForm?page={page}&limit={limit}", Get = "page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_BA_SearchOSUserOrderForm?page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        BaseResultDataValue WXAS_BA_SearchOSUserOrderForm(int page, int limit);

        [ServiceContractDescription(Name = "依用户订单状态查询患者订单信息服务", Desc = "依用户订单状态查询患者订单信息服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_SearchOSUserOrderFormByStatusStr", Get = "", Post = "page,limit,statusStr", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WXAS_BA_SearchOSUserOrderFormByStatusStr", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_BA_SearchOSUserOrderFormByStatusStr(int page, int limit,string statusStr);

        [ServiceContractDescription(Name = "查询患者医嘱单信息服务", Desc = "查询患者医嘱单信息服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_SearchDoctorOrderForm", Get = "", Post = "page,limit", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WXAS_BA_SearchDoctorOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        //[ServiceContractDescription(Name = "查询患者医嘱单信息服务", Desc = "查询患者医嘱单信息服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_SearchDoctorOrderForm?page={page}&limit={limit}", Get = "page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_BA_SearchDoctorOrderForm?page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        BaseResultDataValue WXAS_BA_SearchDoctorOrderForm(int page, int limit);

        [ServiceContractDescription(Name = "用户确定医嘱单", Desc = "用户确定医嘱单", Url = "ZhiFangWeiXinAppService.svc/UserOrderFormConfirm?Id={Id}", Get = "Id={Id}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/UserOrderFormConfirm?Id={Id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UserOrderFormConfirm(long Id);

        [ServiceContractDescription(Name = "根据消费码查报告", Desc = "根据消费码查报告", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_SearchReportFormByPC?PayCode={PayCode}", Get = "PayCode={PayCode}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_BA_SearchReportFormByPC?PayCode={PayCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_BA_SearchReportFormByPC(string PayCode);

        [ServiceContractDescription(Name = "查PDF报告", Desc = "查PDF报告", Url = "ZhiFangWeiXinAppService.svc/Get_ReportFormPDFURLById?ReportFormId={ReportFormId}", Get = "ReportFormId={ReportFormId}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Get_ReportFormPDFURLById?ReportFormId={ReportFormId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Get_ReportFormPDFURLById(string ReportFormId);

        [ServiceContractDescription(Name = "查PDF报告", Desc = "查PDF报告", Url = "ZhiFangWeiXinAppService.svc/Get_ReportFormPDFURLByIndexId?ReportFormIndexId={ReportFormIndexId}", Get = "ReportFormIndexId={ReportFormIndexId}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/Get_ReportFormPDFURLByIndexId?ReportFormIndexId={ReportFormIndexId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Get_ReportFormPDFURLByIndexId(string ReportFormIndexId);

        [ServiceContractDescription(Name = "报告查询", Desc = "报告查询", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_SearchReportFormByRbac?UserSearchReportDateRoundType={UserSearchReportDateRoundType}&page={page}&limit={limit}", Get = "UserSearchReportDateRoundType={UserSearchReportDateRoundType}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_BA_SearchReportFormByRbac?UserSearchReportDateRoundType={UserSearchReportDateRoundType}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_BA_SearchReportFormByRbac(int UserSearchReportDateRoundType, int page, int limit);

        [ServiceContractDescription(Name = "退费单查看", Desc = "退费单查看", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_SearchRefundFormInfoByUOFCode?UOFCode={UOFCode}", Get = "UOFCode={UOFCode}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_BA_SearchRefundFormInfoByUOFCode?UOFCode={UOFCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_BA_SearchRefundFormInfoByUOFCode(string UOFCode);

        [ServiceContractDescription(Name = "用户协议同意服务", Desc = "用户协议同意服务", Url = "ZhiFangWeiXinAppService.svc/WXAS_BA_UserReadAgreement", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_BA_UserReadAgreement", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_BA_UserReadAgreement();


        [ServiceContractDescription(Name = "用户自助下单项目列表", Desc = "用户自助下单项目列表", Url = "ZhiFangWeiXinAppService.svc/WXAS_UDTO_SearchOSTestItemByAreaID?page={page}&limit={limit}&where={where}&sort={sort}", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXAS_UDTO_SearchOSTestItemByAreaID?page={page}&limit={limit}&where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_UDTO_SearchOSTestItemByAreaID(int page, int limit, string where, string sort);

        [ServiceContractDescription(Name = "用户自助下单新增医嘱单", Desc = "用户自助下单新增医嘱单", Url = "ZhiFangWeiXinAppService.svc/WXAS_UDTO_SaveOSDoctorOrderForm", Get = "", Post = "PDict", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WXAS_UDTO_SaveOSDoctorOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXAS_UDTO_SaveOSDoctorOrderForm(Entity.ViewObject.Request.OSDoctorOrderFormVO entity);
    }
}
