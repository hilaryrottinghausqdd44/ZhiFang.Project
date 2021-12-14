using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.Common;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.RBAC;
using ZhiFang.LabInformationIntegratePlatform;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.ServerContract
{
    [ServiceContract]
    public interface IWXService
    {
        #region WXAccountType

        [ServiceContractDescription(Name = "新增WX_AccountType", Desc = "新增WX_AccountType", Url = "WXService.svc/ST_UDTO_AddWXAccountType", Get = "", Post = "WXAccountType", Return = "BaseResultDataValue", ReturnType = "WXAccountType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddWXAccountType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddWXAccountType(WXAccountType entity);

        [ServiceContractDescription(Name = "修改WX_AccountType", Desc = "修改WX_AccountType", Url = "WXService.svc/ST_UDTO_UpdateWXAccountType", Get = "", Post = "WXAccountType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXAccountType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXAccountType(WXAccountType entity);

        [ServiceContractDescription(Name = "修改WX_AccountType指定的属性", Desc = "修改WX_AccountType指定的属性", Url = "WXService.svc/ST_UDTO_UpdateWXAccountTypeByField", Get = "", Post = "WXAccountType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXAccountTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXAccountTypeByField(WXAccountType entity, string fields);

        [ServiceContractDescription(Name = "删除WX_AccountType", Desc = "删除WX_AccountType", Url = "WXService.svc/ST_UDTO_DelWXAccountType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelWXAccountType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelWXAccountType(long id);

        [ServiceContractDescription(Name = "查询WX_AccountType", Desc = "查询WX_AccountType", Url = "WXService.svc/ST_UDTO_SearchWXAccountType", Get = "", Post = "WXAccountType", Return = "BaseResultList<WXAccountType>", ReturnType = "ListWXAccountType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXAccountType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXAccountType(WXAccountType entity);

        [ServiceContractDescription(Name = "查询WX_AccountType(HQL)", Desc = "查询WX_AccountType(HQL)", Url = "WXService.svc/ST_UDTO_SearchWXAccountTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXAccountType>", ReturnType = "ListWXAccountType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXAccountTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXAccountTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询WX_AccountType", Desc = "通过主键ID查询WX_AccountType", Url = "WXService.svc/ST_UDTO_SearchWXAccountTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXAccountType>", ReturnType = "WXAccountType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXAccountTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXAccountTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region WXIcons

        [ServiceContractDescription(Name = "新增WX_Icons", Desc = "新增WX_Icons", Url = "WXService.svc/ST_UDTO_AddWXIcons", Get = "", Post = "WXIcons", Return = "BaseResultDataValue", ReturnType = "WXIcons")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddWXIcons", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddWXIcons(WXIcons entity);

        [ServiceContractDescription(Name = "修改WX_Icons", Desc = "修改WX_Icons", Url = "WXService.svc/ST_UDTO_UpdateWXIcons", Get = "", Post = "WXIcons", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXIcons", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXIcons(WXIcons entity);

        [ServiceContractDescription(Name = "修改WX_Icons指定的属性", Desc = "修改WX_Icons指定的属性", Url = "WXService.svc/ST_UDTO_UpdateWXIconsByField", Get = "", Post = "WXIcons", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXIconsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXIconsByField(WXIcons entity, string fields);

        [ServiceContractDescription(Name = "删除WX_Icons", Desc = "删除WX_Icons", Url = "WXService.svc/ST_UDTO_DelWXIcons?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelWXIcons?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelWXIcons(long id);

        [ServiceContractDescription(Name = "查询WX_Icons", Desc = "查询WX_Icons", Url = "WXService.svc/ST_UDTO_SearchWXIcons", Get = "", Post = "WXIcons", Return = "BaseResultList<WXIcons>", ReturnType = "ListWXIcons")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXIcons", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXIcons(WXIcons entity);

        [ServiceContractDescription(Name = "查询WX_Icons(HQL)", Desc = "查询WX_Icons(HQL)", Url = "WXService.svc/ST_UDTO_SearchWXIconsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXIcons>", ReturnType = "ListWXIcons")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXIconsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXIconsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询WX_Icons", Desc = "通过主键ID查询WX_Icons", Url = "WXService.svc/ST_UDTO_SearchWXIconsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXIcons>", ReturnType = "WXIcons")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXIconsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXIconsById(long id, string fields, bool isPlanish);
        #endregion

        #region WXMessageSendTask

        [ServiceContractDescription(Name = "新增WX_MessageSendTask", Desc = "新增WX_MessageSendTask", Url = "WXService.svc/ST_UDTO_AddWXMessageSendTask", Get = "", Post = "WXMessageSendTask", Return = "BaseResultDataValue", ReturnType = "WXMessageSendTask")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddWXMessageSendTask", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddWXMessageSendTask(WXMessageSendTask entity);

        [ServiceContractDescription(Name = "修改WX_MessageSendTask", Desc = "修改WX_MessageSendTask", Url = "WXService.svc/ST_UDTO_UpdateWXMessageSendTask", Get = "", Post = "WXMessageSendTask", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXMessageSendTask", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXMessageSendTask(WXMessageSendTask entity);

        [ServiceContractDescription(Name = "修改WX_MessageSendTask指定的属性", Desc = "修改WX_MessageSendTask指定的属性", Url = "WXService.svc/ST_UDTO_UpdateWXMessageSendTaskByField", Get = "", Post = "WXMessageSendTask", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXMessageSendTaskByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXMessageSendTaskByField(WXMessageSendTask entity, string fields);

        [ServiceContractDescription(Name = "删除WX_MessageSendTask", Desc = "删除WX_MessageSendTask", Url = "WXService.svc/ST_UDTO_DelWXMessageSendTask?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelWXMessageSendTask?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelWXMessageSendTask(long id);

        [ServiceContractDescription(Name = "查询WX_MessageSendTask", Desc = "查询WX_MessageSendTask", Url = "WXService.svc/ST_UDTO_SearchWXMessageSendTask", Get = "", Post = "WXMessageSendTask", Return = "BaseResultList<WXMessageSendTask>", ReturnType = "ListWXMessageSendTask")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXMessageSendTask", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXMessageSendTask(WXMessageSendTask entity);

        [ServiceContractDescription(Name = "查询WX_MessageSendTask(HQL)", Desc = "查询WX_MessageSendTask(HQL)", Url = "WXService.svc/ST_UDTO_SearchWXMessageSendTaskByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXMessageSendTask>", ReturnType = "ListWXMessageSendTask")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXMessageSendTaskByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXMessageSendTaskByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询WX_MessageSendTask", Desc = "通过主键ID查询WX_MessageSendTask", Url = "WXService.svc/ST_UDTO_SearchWXMessageSendTaskById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXMessageSendTask>", ReturnType = "WXMessageSendTask")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXMessageSendTaskById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXMessageSendTaskById(long id, string fields, bool isPlanish);
        #endregion

        #region WXMsgSendLog

        [ServiceContractDescription(Name = "新增WX_MsgSend_Log", Desc = "新增WX_MsgSend_Log", Url = "WXService.svc/ST_UDTO_AddWXMsgSendLog", Get = "", Post = "WXMsgSendLog", Return = "BaseResultDataValue", ReturnType = "WXMsgSendLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddWXMsgSendLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddWXMsgSendLog(WXMsgSendLog entity);

        [ServiceContractDescription(Name = "修改WX_MsgSend_Log", Desc = "修改WX_MsgSend_Log", Url = "WXService.svc/ST_UDTO_UpdateWXMsgSendLog", Get = "", Post = "WXMsgSendLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXMsgSendLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXMsgSendLog(WXMsgSendLog entity);

        [ServiceContractDescription(Name = "修改WX_MsgSend_Log指定的属性", Desc = "修改WX_MsgSend_Log指定的属性", Url = "WXService.svc/ST_UDTO_UpdateWXMsgSendLogByField", Get = "", Post = "WXMsgSendLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXMsgSendLogByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXMsgSendLogByField(WXMsgSendLog entity, string fields);

        [ServiceContractDescription(Name = "删除WX_MsgSend_Log", Desc = "删除WX_MsgSend_Log", Url = "WXService.svc/ST_UDTO_DelWXMsgSendLog?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelWXMsgSendLog?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelWXMsgSendLog(long id);

        [ServiceContractDescription(Name = "查询WX_MsgSend_Log", Desc = "查询WX_MsgSend_Log", Url = "WXService.svc/ST_UDTO_SearchWXMsgSendLog", Get = "", Post = "WXMsgSendLog", Return = "BaseResultList<WXMsgSendLog>", ReturnType = "ListWXMsgSendLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXMsgSendLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXMsgSendLog(WXMsgSendLog entity);

        [ServiceContractDescription(Name = "查询WX_MsgSend_Log(HQL)", Desc = "查询WX_MsgSend_Log(HQL)", Url = "WXService.svc/ST_UDTO_SearchWXMsgSendLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXMsgSendLog>", ReturnType = "ListWXMsgSendLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXMsgSendLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXMsgSendLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询WX_MsgSend_Log", Desc = "通过主键ID查询WX_MsgSend_Log", Url = "WXService.svc/ST_UDTO_SearchWXMsgSendLogById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXMsgSendLog>", ReturnType = "WXMsgSendLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXMsgSendLogById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXMsgSendLogById(long id, string fields, bool isPlanish);
        #endregion

        #region WXWeiXinEmpLink

        [ServiceContractDescription(Name = "新增WX_WeiXin_Emp_Link", Desc = "新增WX_WeiXin_Emp_Link", Url = "WXService.svc/ST_UDTO_AddWXWeiXinEmpLink", Get = "", Post = "WXWeiXinEmpLink", Return = "BaseResultDataValue", ReturnType = "WXWeiXinEmpLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddWXWeiXinEmpLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddWXWeiXinEmpLink(WXWeiXinEmpLink entity);

        [ServiceContractDescription(Name = "修改WX_WeiXin_Emp_Link", Desc = "修改WX_WeiXin_Emp_Link", Url = "WXService.svc/ST_UDTO_UpdateWXWeiXinEmpLink", Get = "", Post = "WXWeiXinEmpLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXWeiXinEmpLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXWeiXinEmpLink(WXWeiXinEmpLink entity);

        [ServiceContractDescription(Name = "修改WX_WeiXin_Emp_Link指定的属性", Desc = "修改WX_WeiXin_Emp_Link指定的属性", Url = "WXService.svc/ST_UDTO_UpdateWXWeiXinEmpLinkByField", Get = "", Post = "WXWeiXinEmpLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXWeiXinEmpLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXWeiXinEmpLinkByField(WXWeiXinEmpLink entity, string fields);

        [ServiceContractDescription(Name = "删除WX_WeiXin_Emp_Link", Desc = "删除WX_WeiXin_Emp_Link", Url = "WXService.svc/ST_UDTO_DelWXWeiXinEmpLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelWXWeiXinEmpLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelWXWeiXinEmpLink(long id);

        [ServiceContractDescription(Name = "查询WX_WeiXin_Emp_Link", Desc = "查询WX_WeiXin_Emp_Link", Url = "WXService.svc/ST_UDTO_SearchWXWeiXinEmpLink", Get = "", Post = "WXWeiXinEmpLink", Return = "BaseResultList<WXWeiXinEmpLink>", ReturnType = "ListWXWeiXinEmpLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXWeiXinEmpLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXWeiXinEmpLink(WXWeiXinEmpLink entity);

        [ServiceContractDescription(Name = "查询WX_WeiXin_Emp_Link(HQL)", Desc = "查询WX_WeiXin_Emp_Link(HQL)", Url = "WXService.svc/ST_UDTO_SearchWXWeiXinEmpLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXWeiXinEmpLink>", ReturnType = "ListWXWeiXinEmpLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXWeiXinEmpLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXWeiXinEmpLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询WX_WeiXin_Emp_Link", Desc = "通过主键ID查询WX_WeiXin_Emp_Link", Url = "WXService.svc/ST_UDTO_SearchWXWeiXinEmpLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXWeiXinEmpLink>", ReturnType = "WXWeiXinEmpLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXWeiXinEmpLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXWeiXinEmpLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region WXWeiXinPushMessageTemplate

        [ServiceContractDescription(Name = "新增WX_WeiXin_PushMessageTemplate", Desc = "新增WX_WeiXin_PushMessageTemplate", Url = "WXService.svc/ST_UDTO_AddWXWeiXinPushMessageTemplate", Get = "", Post = "WXWeiXinPushMessageTemplate", Return = "BaseResultDataValue", ReturnType = "WXWeiXinPushMessageTemplate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddWXWeiXinPushMessageTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddWXWeiXinPushMessageTemplate(WXWeiXinPushMessageTemplate entity);

        [ServiceContractDescription(Name = "修改WX_WeiXin_PushMessageTemplate", Desc = "修改WX_WeiXin_PushMessageTemplate", Url = "WXService.svc/ST_UDTO_UpdateWXWeiXinPushMessageTemplate", Get = "", Post = "WXWeiXinPushMessageTemplate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXWeiXinPushMessageTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXWeiXinPushMessageTemplate(WXWeiXinPushMessageTemplate entity);

        [ServiceContractDescription(Name = "修改WX_WeiXin_PushMessageTemplate指定的属性", Desc = "修改WX_WeiXin_PushMessageTemplate指定的属性", Url = "WXService.svc/ST_UDTO_UpdateWXWeiXinPushMessageTemplateByField", Get = "", Post = "WXWeiXinPushMessageTemplate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXWeiXinPushMessageTemplateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXWeiXinPushMessageTemplateByField(WXWeiXinPushMessageTemplate entity, string fields);

        [ServiceContractDescription(Name = "删除WX_WeiXin_PushMessageTemplate", Desc = "删除WX_WeiXin_PushMessageTemplate", Url = "WXService.svc/ST_UDTO_DelWXWeiXinPushMessageTemplate?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelWXWeiXinPushMessageTemplate?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelWXWeiXinPushMessageTemplate(long id);

        [ServiceContractDescription(Name = "查询WX_WeiXin_PushMessageTemplate", Desc = "查询WX_WeiXin_PushMessageTemplate", Url = "WXService.svc/ST_UDTO_SearchWXWeiXinPushMessageTemplate", Get = "", Post = "WXWeiXinPushMessageTemplate", Return = "BaseResultList<WXWeiXinPushMessageTemplate>", ReturnType = "ListWXWeiXinPushMessageTemplate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXWeiXinPushMessageTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXWeiXinPushMessageTemplate(WXWeiXinPushMessageTemplate entity);

        [ServiceContractDescription(Name = "查询WX_WeiXin_PushMessageTemplate(HQL)", Desc = "查询WX_WeiXin_PushMessageTemplate(HQL)", Url = "WXService.svc/ST_UDTO_SearchWXWeiXinPushMessageTemplateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXWeiXinPushMessageTemplate>", ReturnType = "ListWXWeiXinPushMessageTemplate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXWeiXinPushMessageTemplateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXWeiXinPushMessageTemplateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询WX_WeiXin_PushMessageTemplate", Desc = "通过主键ID查询WX_WeiXin_PushMessageTemplate", Url = "WXService.svc/ST_UDTO_SearchWXWeiXinPushMessageTemplateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXWeiXinPushMessageTemplate>", ReturnType = "WXWeiXinPushMessageTemplate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXWeiXinPushMessageTemplateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXWeiXinPushMessageTemplateById(long id, string fields, bool isPlanish);
        #endregion

        #region WXWeiXinAccount

        [ServiceContractDescription(Name = "新增WX_WeiXinAccount", Desc = "新增WX_WeiXinAccount", Url = "WXService.svc/ST_UDTO_AddWXWeiXinAccount", Get = "", Post = "WXWeiXinAccount", Return = "BaseResultDataValue", ReturnType = "WXWeiXinAccount")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddWXWeiXinAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddWXWeiXinAccount(WXWeiXinAccount entity);

        [ServiceContractDescription(Name = "修改WX_WeiXinAccount", Desc = "修改WX_WeiXinAccount", Url = "WXService.svc/ST_UDTO_UpdateWXWeiXinAccount", Get = "", Post = "WXWeiXinAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXWeiXinAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXWeiXinAccount(WXWeiXinAccount entity);

        [ServiceContractDescription(Name = "修改WX_WeiXinAccount指定的属性", Desc = "修改WX_WeiXinAccount指定的属性", Url = "WXService.svc/ST_UDTO_UpdateWXWeiXinAccountByField", Get = "", Post = "WXWeiXinAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXWeiXinAccountByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXWeiXinAccountByField(WXWeiXinAccount entity, string fields);

        [ServiceContractDescription(Name = "删除WX_WeiXinAccount", Desc = "删除WX_WeiXinAccount", Url = "WXService.svc/ST_UDTO_DelWXWeiXinAccount?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelWXWeiXinAccount?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelWXWeiXinAccount(long id);

        [ServiceContractDescription(Name = "查询WX_WeiXinAccount", Desc = "查询WX_WeiXinAccount", Url = "WXService.svc/ST_UDTO_SearchWXWeiXinAccount", Get = "", Post = "WXWeiXinAccount", Return = "BaseResultList<WXWeiXinAccount>", ReturnType = "ListWXWeiXinAccount")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXWeiXinAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXWeiXinAccount(WXWeiXinAccount entity);

        [ServiceContractDescription(Name = "查询WX_WeiXinAccount(HQL)", Desc = "查询WX_WeiXinAccount(HQL)", Url = "WXService.svc/ST_UDTO_SearchWXWeiXinAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXWeiXinAccount>", ReturnType = "ListWXWeiXinAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXWeiXinAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXWeiXinAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询WX_WeiXinAccount", Desc = "通过主键ID查询WX_WeiXinAccount", Url = "WXService.svc/ST_UDTO_SearchWXWeiXinAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXWeiXinAccount>", ReturnType = "WXWeiXinAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXWeiXinAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXWeiXinAccountById(long id, string fields, bool isPlanish);
        #endregion

        #region WXWeiXinUserGroup

        [ServiceContractDescription(Name = "新增WX_WeiXinUserGroup", Desc = "新增WX_WeiXinUserGroup", Url = "WXService.svc/ST_UDTO_AddWXWeiXinUserGroup", Get = "", Post = "WXWeiXinUserGroup", Return = "BaseResultDataValue", ReturnType = "WXWeiXinUserGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddWXWeiXinUserGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddWXWeiXinUserGroup(WXWeiXinUserGroup entity);

        [ServiceContractDescription(Name = "修改WX_WeiXinUserGroup", Desc = "修改WX_WeiXinUserGroup", Url = "WXService.svc/ST_UDTO_UpdateWXWeiXinUserGroup", Get = "", Post = "WXWeiXinUserGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXWeiXinUserGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXWeiXinUserGroup(WXWeiXinUserGroup entity);

        [ServiceContractDescription(Name = "修改WX_WeiXinUserGroup指定的属性", Desc = "修改WX_WeiXinUserGroup指定的属性", Url = "WXService.svc/ST_UDTO_UpdateWXWeiXinUserGroupByField", Get = "", Post = "WXWeiXinUserGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateWXWeiXinUserGroupByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateWXWeiXinUserGroupByField(WXWeiXinUserGroup entity, string fields);

        [ServiceContractDescription(Name = "删除WX_WeiXinUserGroup", Desc = "删除WX_WeiXinUserGroup", Url = "WXService.svc/ST_UDTO_DelWXWeiXinUserGroup?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelWXWeiXinUserGroup?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelWXWeiXinUserGroup(long id);

        [ServiceContractDescription(Name = "查询WX_WeiXinUserGroup", Desc = "查询WX_WeiXinUserGroup", Url = "WXService.svc/ST_UDTO_SearchWXWeiXinUserGroup", Get = "", Post = "WXWeiXinUserGroup", Return = "BaseResultList<WXWeiXinUserGroup>", ReturnType = "ListWXWeiXinUserGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXWeiXinUserGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXWeiXinUserGroup(WXWeiXinUserGroup entity);

        [ServiceContractDescription(Name = "查询WX_WeiXinUserGroup(HQL)", Desc = "查询WX_WeiXinUserGroup(HQL)", Url = "WXService.svc/ST_UDTO_SearchWXWeiXinUserGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXWeiXinUserGroup>", ReturnType = "ListWXWeiXinUserGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXWeiXinUserGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXWeiXinUserGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询WX_WeiXinUserGroup", Desc = "通过主键ID查询WX_WeiXinUserGroup", Url = "WXService.svc/ST_UDTO_SearchWXWeiXinUserGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXWeiXinUserGroup>", ReturnType = "WXWeiXinUserGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWXWeiXinUserGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWXWeiXinUserGroupById(long id, string fields, bool isPlanish);
        #endregion

        [ServiceContractDescription(Name = "", Desc = "", Url = "WXService.svc/WXMessageSendTaskUpLoadFile", Get = "", Post = "WXMessageSendTask", Return = "BaseResultList<WXMessageSendTask>", ReturnType = "ListWXMessageSendTask")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WXMessageSendTaskUpLoadFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WXMessageSendTaskUpLoadFile();

        [ServiceContractDescription(Name = "发送", Desc = "发送", Url = "WXService.svc/WXMessageSendOut?id={id}&peopleId={peopleId}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WXWeiXinUserGroup>", ReturnType = "WXWeiXinUserGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WXMessageSendOut?id={id}&peopleId={peopleId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WXMessageSendOut(long id, long peopleId);
    }
}
