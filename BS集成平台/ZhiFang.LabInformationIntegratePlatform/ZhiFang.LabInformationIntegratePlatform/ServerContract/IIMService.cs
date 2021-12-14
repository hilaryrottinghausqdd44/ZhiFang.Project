using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.ServerContract
{
    [ServiceContract]
    public interface IIMService
    {

        #region SCIMInfomationContent

        [ServiceContractDescription(Name = "新增公共即时通讯信息内容表", Desc = "新增公共即时通讯信息内容表", Url = "IMService.svc/ST_UDTO_AddSCIMInfomationContent", Get = "", Post = "SCIMInfomationContent", Return = "BaseResultDataValue", ReturnType = "SCIMInfomationContent")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSCIMInfomationContent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSCIMInfomationContent(SCIMInfomationContent entity);

        //[ServiceContractDescription(Name = "修改公共即时通讯信息内容表", Desc = "修改公共即时通讯信息内容表", Url = "IMService.svc/ST_UDTO_UpdateSCIMInfomationContent", Get = "", Post = "SCIMInfomationContent", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSCIMInfomationContent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateSCIMInfomationContent(SCIMInfomationContent entity);

        //[ServiceContractDescription(Name = "修改公共即时通讯信息内容表指定的属性", Desc = "修改公共即时通讯信息内容表指定的属性", Url = "IMService.svc/ST_UDTO_UpdateSCIMInfomationContentByField", Get = "", Post = "SCIMInfomationContent", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSCIMInfomationContentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateSCIMInfomationContentByField(SCIMInfomationContent entity, string fields);

        //[ServiceContractDescription(Name = "删除公共即时通讯信息内容表", Desc = "删除公共即时通讯信息内容表", Url = "IMService.svc/ST_UDTO_DelSCIMInfomationContent?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSCIMInfomationContent?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelSCIMInfomationContent(long id);

        [ServiceContractDescription(Name = "阅读公共即时通讯信息内容表", Desc = "阅读公共即时通讯信息内容表", Url = "IMService.svc/ST_UDTO_SCIMInfomationContentReRead?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_SCIMInfomationContentReRead?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_SCIMInfomationContentReRead(long id);

        [ServiceContractDescription(Name = "撤回公共即时通讯信息内容表", Desc = "撤回公共即时通讯信息内容表", Url = "IMService.svc/ST_UDTO_SCIMInfomationContentReBack?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_SCIMInfomationContentReBack?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_SCIMInfomationContentReBack(long id);

        [ServiceContractDescription(Name = "查询公共即时通讯信息内容表", Desc = "查询公共即时通讯信息内容表", Url = "IMService.svc/ST_UDTO_SearchSCIMInfomationContent", Get = "", Post = "SCIMInfomationContent", Return = "BaseResultList<SCIMInfomationContent>", ReturnType = "ListSCIMInfomationContent")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCIMInfomationContent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCIMInfomationContent(SCIMInfomationContent entity);

        [ServiceContractDescription(Name = "查询公共即时通讯信息内容表(HQL)", Desc = "查询公共即时通讯信息内容表(HQL)", Url = "IMService.svc/ST_UDTO_SearchSCIMInfomationContentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCIMInfomationContent>", ReturnType = "ListSCIMInfomationContent")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCIMInfomationContentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCIMInfomationContentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共即时通讯信息内容表", Desc = "通过主键ID查询公共即时通讯信息内容表", Url = "IMService.svc/ST_UDTO_SearchSCIMInfomationContentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCIMInfomationContent>", ReturnType = "SCIMInfomationContent")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCIMInfomationContentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCIMInfomationContentById(long id, string fields, bool isPlanish);
        #endregion

        #region SCMsg

        [ServiceContractDescription(Name = "新增公共消息表", Desc = "新增公共消息表", Url = "IMService.svc/ST_UDTO_AddSCMsg", Get = "", Post = "SCMsg", Return = "BaseResultDataValue", ReturnType = "SCMsg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSCMsg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSCMsg(SCMsg entity);

        [ServiceContractDescription(Name = "修改公共消息表", Desc = "修改公共消息表", Url = "IMService.svc/ST_UDTO_UpdateSCMsg", Get = "", Post = "SCMsg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSCMsg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSCMsg(SCMsg entity);

        [ServiceContractDescription(Name = "修改公共消息表指定的属性", Desc = "修改公共消息表指定的属性", Url = "IMService.svc/ST_UDTO_UpdateSCMsgByField", Get = "", Post = "SCMsg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSCMsgByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSCMsgByField(SCMsg entity, string fields);

        [ServiceContractDescription(Name = "公共消息表_上报", Desc = "公共消息表_上报", Url = "IMService.svc/ST_UDTO_SCMsgByWarningUpload", Get = "", Post = "SCMsg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SCMsgByWarningUpload", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_SCMsgByWarningUpload(SCMsg entity, string fields);

        [ServiceContractDescription(Name = "公共消息表_确认", Desc = "公共消息表_确认", Url = "IMService.svc/ST_UDTO_SCMsgByConfirm", Get = "", Post = "SCMsg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SCMsgByConfirm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SCMsgByConfirm(SCMsg entity);


        [ServiceContractDescription(Name = "公共消息表_确认提醒处理", Desc = "公共消息表_确认提醒处理", Url = "IMService.svc/ST_UDTO_SCMsgByConfirmNotify", Get = "", Post = "SCMsg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SCMsgByConfirmNotify", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SCMsgByConfirmNotify(SCMsg entity);

        [ServiceContractDescription(Name = "公共消息表_超时重发", Desc = "公共消息表_上报", Url = "IMService.svc/ST_UDTO_SCMsgByTimeOutReSend", Get = "", Post = "SCMsg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SCMsgByTimeOutReSend", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SCMsgByTimeOutReSend(SCMsg entity);

        [ServiceContractDescription(Name = "删除公共消息表", Desc = "删除公共消息表", Url = "IMService.svc/ST_UDTO_DelSCMsg?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSCMsg?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSCMsg(long id);

        [ServiceContractDescription(Name = "查询公共消息表", Desc = "查询公共消息表", Url = "IMService.svc/ST_UDTO_SearchSCMsg", Get = "", Post = "SCMsg", Return = "BaseResultList<SCMsg>", ReturnType = "ListSCMsg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsg(SCMsg entity);

        [ServiceContractDescription(Name = "查询公共消息表(HQL)", Desc = "查询公共消息表(HQL)", Url = "IMService.svc/ST_UDTO_SearchSCMsgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCMsg>", ReturnType = "ListSCMsg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询公共消息表(HQL)", Desc = "查询公共消息表(HQL)", Url = "IMService.svc/ST_UDTO_SearchSCMsgAndSCMsgHandleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCMsg>", ReturnType = "ListSCMsg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsgAndSCMsgHandleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsgAndSCMsgHandleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询公共消息表通过登录者关联医院", Desc = "查询公共消息表通过登录者关联医院", Url = "IMService.svc/ST_UDTO_SearchSCMsgByHQLAndLabCode?page={page}&limit={limit}&fields={fields}&where={where}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCMsg>", ReturnType = "ListSCMsg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsgByHQLAndLabCode?page={page}&limit={limit}&fields={fields}&where={where}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsgByHQLAndLabCode(int page, int limit, string fields, string where, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共消息表", Desc = "通过主键ID查询公共消息表", Url = "IMService.svc/ST_UDTO_SearchSCMsgById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCMsg>", ReturnType = "SCMsg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsgById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsgById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "批量确认消息", Desc = "批量确认消息", Url = "IMService.svc/ST_UDTO_BatchConfirmMsg", Post = "", Return = "BaseResultList<SCMsg>", ReturnType = "SCMsg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_BatchConfirmMsg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_BatchConfirmMsg(List<string> idlist);
        #endregion

        #region SCMsgHandle

        [ServiceContractDescription(Name = "新增公共消息处理表", Desc = "新增公共消息处理表", Url = "IMService.svc/ST_UDTO_AddSCMsgHandle", Get = "", Post = "SCMsgHandle", Return = "BaseResultDataValue", ReturnType = "SCMsgHandle")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSCMsgHandle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSCMsgHandle(SCMsgHandle entity);

        [ServiceContractDescription(Name = "修改公共消息处理表", Desc = "修改公共消息处理表", Url = "IMService.svc/ST_UDTO_UpdateSCMsgHandle", Get = "", Post = "SCMsgHandle", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSCMsgHandle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSCMsgHandle(SCMsgHandle entity);

        [ServiceContractDescription(Name = "修改公共消息处理表指定的属性", Desc = "修改公共消息处理表指定的属性", Url = "IMService.svc/ST_UDTO_UpdateSCMsgHandleByField", Get = "", Post = "SCMsgHandle", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSCMsgHandleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSCMsgHandleByField(SCMsgHandle entity, string fields);

        [ServiceContractDescription(Name = "删除公共消息处理表", Desc = "删除公共消息处理表", Url = "IMService.svc/ST_UDTO_DelSCMsgHandle?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSCMsgHandle?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSCMsgHandle(long id);

        [ServiceContractDescription(Name = "查询公共消息处理表", Desc = "查询公共消息处理表", Url = "IMService.svc/ST_UDTO_SearchSCMsgHandle", Get = "", Post = "SCMsgHandle", Return = "BaseResultList<SCMsgHandle>", ReturnType = "ListSCMsgHandle")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsgHandle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsgHandle(SCMsgHandle entity);

        [ServiceContractDescription(Name = "查询公共消息处理表(HQL)", Desc = "查询公共消息处理表(HQL)", Url = "IMService.svc/ST_UDTO_SearchSCMsgHandleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCMsgHandle>", ReturnType = "ListSCMsgHandle")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsgHandleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsgHandleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共消息处理表", Desc = "通过主键ID查询公共消息处理表", Url = "IMService.svc/ST_UDTO_SearchSCMsgHandleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCMsgHandle>", ReturnType = "SCMsgHandle")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsgHandleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsgHandleById(long id, string fields, bool isPlanish);
        #endregion

        #region SCMsgPhraseDic

        [ServiceContractDescription(Name = "新增公共消息短语字典", Desc = "新增公共消息短语字典", Url = "IMService.svc/ST_UDTO_AddSCMsgPhraseDic", Get = "", Post = "SCMsgPhraseDic", Return = "BaseResultDataValue", ReturnType = "SCMsgPhraseDic")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSCMsgPhraseDic", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSCMsgPhraseDic(SCMsgPhraseDic entity);

        [ServiceContractDescription(Name = "修改公共消息短语字典", Desc = "修改公共消息短语字典", Url = "IMService.svc/ST_UDTO_UpdateSCMsgPhraseDic", Get = "", Post = "SCMsgPhraseDic", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSCMsgPhraseDic", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSCMsgPhraseDic(SCMsgPhraseDic entity);

        [ServiceContractDescription(Name = "修改公共消息短语字典指定的属性", Desc = "修改公共消息短语字典指定的属性", Url = "IMService.svc/ST_UDTO_UpdateSCMsgPhraseDicByField", Get = "", Post = "SCMsgPhraseDic", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSCMsgPhraseDicByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSCMsgPhraseDicByField(SCMsgPhraseDic entity, string fields);

        [ServiceContractDescription(Name = "删除公共消息短语字典", Desc = "删除公共消息短语字典", Url = "IMService.svc/ST_UDTO_DelSCMsgPhraseDic?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSCMsgPhraseDic?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSCMsgPhraseDic(long id);

        [ServiceContractDescription(Name = "查询公共消息短语字典", Desc = "查询公共消息短语字典", Url = "IMService.svc/ST_UDTO_SearchSCMsgPhraseDic", Get = "", Post = "SCMsgPhraseDic", Return = "BaseResultList<SCMsgPhraseDic>", ReturnType = "ListSCMsgPhraseDic")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsgPhraseDic", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsgPhraseDic(SCMsgPhraseDic entity);

        [ServiceContractDescription(Name = "查询公共消息短语字典(HQL)", Desc = "查询公共消息短语字典(HQL)", Url = "IMService.svc/ST_UDTO_SearchSCMsgPhraseDicByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCMsgPhraseDic>", ReturnType = "ListSCMsgPhraseDic")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsgPhraseDicByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsgPhraseDicByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共消息短语字典", Desc = "通过主键ID查询公共消息短语字典", Url = "IMService.svc/ST_UDTO_SearchSCMsgPhraseDicById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCMsgPhraseDic>", ReturnType = "SCMsgPhraseDic")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsgPhraseDicById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsgPhraseDicById(long id, string fields, bool isPlanish);
        #endregion

        #region SCMsgType

        [ServiceContractDescription(Name = "新增公共消息类型表", Desc = "新增公共消息类型表", Url = "IMService.svc/ST_UDTO_AddSCMsgType", Get = "", Post = "SCMsgType", Return = "BaseResultDataValue", ReturnType = "SCMsgType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSCMsgType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSCMsgType(SCMsgType entity);

        [ServiceContractDescription(Name = "修改公共消息类型表", Desc = "修改公共消息类型表", Url = "IMService.svc/ST_UDTO_UpdateSCMsgType", Get = "", Post = "SCMsgType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSCMsgType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSCMsgType(SCMsgType entity);

        [ServiceContractDescription(Name = "修改公共消息类型表指定的属性", Desc = "修改公共消息类型表指定的属性", Url = "IMService.svc/ST_UDTO_UpdateSCMsgTypeByField", Get = "", Post = "SCMsgType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSCMsgTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSCMsgTypeByField(SCMsgType entity, string fields);

        [ServiceContractDescription(Name = "删除公共消息类型表", Desc = "删除公共消息类型表", Url = "IMService.svc/ST_UDTO_DelSCMsgType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSCMsgType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSCMsgType(long id);

        [ServiceContractDescription(Name = "查询公共消息类型表", Desc = "查询公共消息类型表", Url = "IMService.svc/ST_UDTO_SearchSCMsgType", Get = "", Post = "SCMsgType", Return = "BaseResultList<SCMsgType>", ReturnType = "ListSCMsgType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsgType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsgType(SCMsgType entity);

        [ServiceContractDescription(Name = "查询公共消息类型表(HQL)", Desc = "查询公共消息类型表(HQL)", Url = "IMService.svc/ST_UDTO_SearchSCMsgTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCMsgType>", ReturnType = "ListSCMsgType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsgTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsgTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共消息类型表", Desc = "通过主键ID查询公共消息类型表", Url = "IMService.svc/ST_UDTO_SearchSCMsgTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCMsgType>", ReturnType = "SCMsgType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSCMsgTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSCMsgTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region CVCriticalValueEmpIdDeptLink

        [ServiceContractDescription(Name = "新增危急值员工和部门关系", Desc = "新增危急值员工和部门关系", Url = "IMService.svc/ST_UDTO_AddCVCriticalValueEmpIdDeptLink", Get = "", Post = "CVCriticalValueEmpIdDeptLink", Return = "BaseResultDataValue", ReturnType = "CVCriticalValueEmpIdDeptLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddCVCriticalValueEmpIdDeptLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddCVCriticalValueEmpIdDeptLink(CVCriticalValueEmpIdDeptLink entity);

        [ServiceContractDescription(Name = "修改危急值员工和部门关系", Desc = "修改危急值员工和部门关系", Url = "IMService.svc/ST_UDTO_UpdateCVCriticalValueEmpIdDeptLink", Get = "", Post = "CVCriticalValueEmpIdDeptLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCVCriticalValueEmpIdDeptLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCVCriticalValueEmpIdDeptLink(CVCriticalValueEmpIdDeptLink entity);

        [ServiceContractDescription(Name = "修改危急值员工和部门关系指定的属性", Desc = "修改危急值员工和部门关系指定的属性", Url = "IMService.svc/ST_UDTO_UpdateCVCriticalValueEmpIdDeptLinkByField", Get = "", Post = "CVCriticalValueEmpIdDeptLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCVCriticalValueEmpIdDeptLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCVCriticalValueEmpIdDeptLinkByField(CVCriticalValueEmpIdDeptLink entity, string fields);

        [ServiceContractDescription(Name = "删除危急值员工和部门关系", Desc = "删除危急值员工和部门关系", Url = "IMService.svc/ST_UDTO_DelCVCriticalValueEmpIdDeptLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelCVCriticalValueEmpIdDeptLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelCVCriticalValueEmpIdDeptLink(long id);

        [ServiceContractDescription(Name = "查询危急值员工和部门关系", Desc = "查询危急值员工和部门关系", Url = "IMService.svc/ST_UDTO_SearchCVCriticalValueEmpIdDeptLink", Get = "", Post = "CVCriticalValueEmpIdDeptLink", Return = "BaseResultList<CVCriticalValueEmpIdDeptLink>", ReturnType = "ListCVCriticalValueEmpIdDeptLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCVCriticalValueEmpIdDeptLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCVCriticalValueEmpIdDeptLink(CVCriticalValueEmpIdDeptLink entity);

        [ServiceContractDescription(Name = "查询危急值员工和部门关系(HQL)", Desc = "查询危急值员工和部门关系(HQL)", Url = "IMService.svc/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CVCriticalValueEmpIdDeptLink>", ReturnType = "ListCVCriticalValueEmpIdDeptLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询危急值员工和部门关系", Desc = "通过主键ID查询危急值员工和部门关系", Url = "IMService.svc/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CVCriticalValueEmpIdDeptLink>", ReturnType = "CVCriticalValueEmpIdDeptLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCVCriticalValueEmpIdDeptLinkById(long id, string fields, bool isPlanish);
        #endregion

        [ServiceContractDescription(Name = "", Desc = "", Url = "IMService.svc/SendMessagesByDeptId_CV?ToDeptId={ToDeptId}&Message={Message}&FormUserEmpId={FormUserEmpId}&SCMsgId={SCMsgId}&SCMsgTypeCode={SCMsgTypeCode}&SCMsgTypeCodeStatus={SCMsgTypeCodeStatus}", Get = "ToDeptId={ToDeptId}&Message={Message}&FormUserEmpId={FormUserEmpId}&SCMsgId={SCMsgId}&SCMsgTypeCode={SCMsgTypeCode}", Post = "", Return = "BaseResult", ReturnType = "string")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SendMessagesByDeptId_CV?ToDeptId={ToDeptId}&Message={Message}&FormUserEmpId={FormUserEmpId}&SCMsgId={SCMsgId}&SCMsgTypeCode={SCMsgTypeCode}&SCMsgTypeCodeStatus={SCMsgTypeCodeStatus}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string SendMessagesByDeptId_CV(string ToDeptId, string Message, long FormUserEmpId, long SCMsgId, string SCMsgTypeCode, string SCMsgTypeCodeStatus);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetOnlineUserList?flag={flag}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string GetOnlineUserList(int flag);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetUserMsgByPWD?Account={Account}&PWD={PWD}&flag={flag}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetUserMsgByPWD(string Account, string PWD, bool flag);

        [ServiceContractDescription(Name = "重发处理意见到HIS接口", Desc = "重发处理意见到HIS接口", Url = "IMService.svc/ST_UDTO_ReSendSCMsgHandleToHISInterface?SCMsgID={SCMsgID}&SCMsgHandleID={SCMsgHandleID}&PWD={PWD}", Get = "SCMsgID={SCMsgID}&SCMsgHandleID={SCMsgHandleID}&PWD={PWD}", Post = "", Return = "BaseResult", ReturnType = "string")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ReSendSCMsgHandleToHISInterface?SCMsgID={SCMsgID}&SCMsgHandleID={SCMsgHandleID}&PWD={PWD}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_ReSendSCMsgHandleToHISInterface(long SCMsgID, long SCMsgHandleID, string PWD);

       
    }
}
