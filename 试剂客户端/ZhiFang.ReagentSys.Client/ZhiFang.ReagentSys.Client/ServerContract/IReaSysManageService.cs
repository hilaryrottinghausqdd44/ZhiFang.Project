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

namespace ZhiFang.ReagentSys.Client.ServerContract
{
    [ServiceContract]
    public interface IReaSysManageService
    {

        #region 获取程序内部字典
        [ServiceContractDescription(Name = "获取枚举字典", Desc = "获取枚举字典", Url = "ReaSysManageService.svc/GetEnumDic?enumname={enumname}", Get = "enumname={enumname}", Post = "", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetEnumDic?enumname={enumname}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetEnumDic(string enumname);

        [ServiceContractDescription(Name = "获取类字典", Desc = "获取类字典", Url = "ReaSysManageService.svc/GetClassDic?classname={classname}&classnamespace={classnamespace}", Get = "classname={classname}&classnamespace={classnamespace}", Post = "", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetClassDic?classname={classname}&classnamespace={classnamespace}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetClassDic(string classname, string classnamespace);

        [ServiceContractDescription(Name = "获取类字典列表", Desc = "获取类字典列表", Url = "ReaSysManageService.svc/GetClassDicList", Get = "", Post = "ClassDicSearchPara", Return = "BaseResultDataValue", ReturnType = "ClassDicList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetClassDicList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetClassDicList(ClassDicSearchPara[] jsonpara);

        #endregion

        #region CenOrg
        [ServiceContractDescription(Name = "查询平台机构信息(HQL)", Desc = "查询平台机构信息(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchCenOrgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenOrg>", ReturnType = "ListCenOrg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "修改平台机构指定的属性", Desc = "修改平台机构表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateCenOrgByField", Get = "", Post = "CenOrg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenOrgByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenOrgByField(CenOrg entity, string fields);

        [ServiceContractDescription(Name = "通过主键ID查询平台机构表", Desc = "通过主键ID查询平台机构表", Url = "ReaSysManageService.svc/ST_UDTO_SearchCenOrgById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenOrg>", ReturnType = "CenOrg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrgById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrgById(long id, string fields, bool isPlanish);
        #endregion

        #region SCAttachment
        [ServiceContractDescription(Name = "上传公共附件服务", Desc = "上传公共附件服务", Url = "ReaSysManageService.svc/SC_UploadAddSCAttachment", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UploadAddSCAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message SC_UploadAddSCAttachment();

        [ServiceContractDescription(Name = "下载公共附件服务", Desc = "下载公共附件服务", Url = "ReaSysManageService.svc/SC_UDTO_DownLoadSCAttachment?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_DownLoadSCAttachment?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream SC_UDTO_DownLoadSCAttachment(long id, long operateType);

        [ServiceContractDescription(Name = "新增公共附件表", Desc = "新增公共附件表", Url = "ReaSysManageService.svc/SC_UDTO_AddSCAttachment", Get = "", Post = "SCAttachment", Return = "BaseResultDataValue", ReturnType = "SCAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_AddSCAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_AddSCAttachment(SCAttachment entity);

        [ServiceContractDescription(Name = "修改公共附件表", Desc = "修改公共附件表", Url = "ReaSysManageService.svc/SC_UDTO_UpdateSCAttachment", Get = "", Post = "SCAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCAttachment(SCAttachment entity);

        [ServiceContractDescription(Name = "修改公共附件表指定的属性", Desc = "修改公共附件表指定的属性", Url = "ReaSysManageService.svc/SC_UDTO_UpdateSCAttachmentByField", Get = "", Post = "SCAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCAttachmentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCAttachmentByField(SCAttachment entity, string fields);

        [ServiceContractDescription(Name = "删除公共附件表", Desc = "删除公共附件表", Url = "ReaSysManageService.svc/SC_UDTO_DelSCAttachment?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SC_UDTO_DelSCAttachment?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_DelSCAttachment(long id);

        [ServiceContractDescription(Name = "查询公共附件表", Desc = "查询公共附件表", Url = "ReaSysManageService.svc/SC_UDTO_SearchSCAttachment", Get = "", Post = "SCAttachment", Return = "BaseResultList<SCAttachment>", ReturnType = "ListSCAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCAttachment(SCAttachment entity);

        [ServiceContractDescription(Name = "查询公共附件表(HQL)", Desc = "查询公共附件表(HQL)", Url = "ReaSysManageService.svc/SC_UDTO_SearchSCAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCAttachment>", ReturnType = "ListSCAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共附件表", Desc = "通过主键ID查询公共附件表", Url = "ReaSysManageService.svc/SC_UDTO_SearchSCAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCAttachment>", ReturnType = "SCAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCAttachmentById(long id, string fields, bool isPlanish);
        #endregion

        #region SCInteraction

        [ServiceContractDescription(Name = "新增程序交流表", Desc = "新增程序交流表", Url = "ReaSysManageService.svc/SC_UDTO_AddSCInteraction", Get = "", Post = "SCInteraction", Return = "BaseResultDataValue", ReturnType = "SCInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_AddSCInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_AddSCInteraction(SCInteraction entity);

        [ServiceContractDescription(Name = "修改程序交流表", Desc = "修改程序交流表", Url = "ReaSysManageService.svc/SC_UDTO_UpdateSCInteraction", Get = "", Post = "SCInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCInteraction(SCInteraction entity);

        [ServiceContractDescription(Name = "修改程序交流表指定的属性", Desc = "修改程序交流表指定的属性", Url = "ReaSysManageService.svc/SC_UDTO_UpdateSCInteractionByField", Get = "", Post = "SCInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCInteractionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCInteractionByField(SCInteraction entity, string fields);

        [ServiceContractDescription(Name = "删除程序交流表", Desc = "删除程序交流表", Url = "ReaSysManageService.svc/SC_UDTO_DelSCInteraction?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SC_UDTO_DelSCInteraction?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_DelSCInteraction(long id);

        [ServiceContractDescription(Name = "查询程序交流表", Desc = "查询程序交流表", Url = "ReaSysManageService.svc/SC_UDTO_SearchSCInteraction", Get = "", Post = "SCInteraction", Return = "BaseResultList<SCInteraction>", ReturnType = "ListSCInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCInteraction(SCInteraction entity);

        [ServiceContractDescription(Name = "查询程序交流表(HQL)", Desc = "查询程序交流表(HQL)", Url = "ReaSysManageService.svc/SC_UDTO_SearchSCInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCInteraction>", ReturnType = "ListSCInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询程序交流表", Desc = "通过主键ID查询程序交流表", Url = "ReaSysManageService.svc/SC_UDTO_SearchSCInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCInteraction>", ReturnType = "SCInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCInteractionById(long id, string fields, bool isPlanish);
        #endregion

        #region SCOperation

        [ServiceContractDescription(Name = "新增公共操作记录表", Desc = "新增公共操作记录表", Url = "ReaSysManageService.svc/SC_UDTO_AddSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultDataValue", ReturnType = "SCOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_AddSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_AddSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "修改公共操作记录表", Desc = "修改公共操作记录表", Url = "ReaSysManageService.svc/SC_UDTO_UpdateSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "修改公共操作记录表指定的属性", Desc = "修改公共操作记录表指定的属性", Url = "ReaSysManageService.svc/SC_UDTO_UpdateSCOperationByField", Get = "", Post = "SCOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCOperationByField(SCOperation entity, string fields);

        [ServiceContractDescription(Name = "删除公共操作记录表", Desc = "删除公共操作记录表", Url = "ReaSysManageService.svc/SC_UDTO_DelSCOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SC_UDTO_DelSCOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_DelSCOperation(long id);

        [ServiceContractDescription(Name = "查询公共操作记录表", Desc = "查询公共操作记录表", Url = "ReaSysManageService.svc/SC_UDTO_SearchSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultList<SCOperation>", ReturnType = "ListSCOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "查询公共操作记录表(HQL)", Desc = "查询公共操作记录表(HQL)", Url = "ReaSysManageService.svc/SC_UDTO_SearchSCOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCOperation>", ReturnType = "ListSCOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共操作记录表", Desc = "通过主键ID查询公共操作记录表", Url = "ReaSysManageService.svc/SC_UDTO_SearchSCOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCOperation>", ReturnType = "SCOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCOperationById(long id, string fields, bool isPlanish);
        #endregion        

        #region ReaCenOrg

        [ServiceContractDescription(Name = "新增机构表", Desc = "新增机构表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaCenOrg", Get = "", Post = "ReaCenOrg", Return = "BaseResultDataValue", ReturnType = "ReaCenOrg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaCenOrg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaCenOrg(ReaCenOrg entity);

        [ServiceContractDescription(Name = "修改机构表", Desc = "修改机构表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaCenOrg", Get = "", Post = "ReaCenOrg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaCenOrg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaCenOrg(ReaCenOrg entity);

        [ServiceContractDescription(Name = "修改机构表指定的属性", Desc = "修改机构表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaCenOrgByField", Get = "", Post = "ReaCenOrg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaCenOrgByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaCenOrgByField(ReaCenOrg entity, string fields);

        [ServiceContractDescription(Name = "删除机构表", Desc = "删除机构表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaCenOrg?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaCenOrg?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaCenOrg(long id);

        [ServiceContractDescription(Name = "查询机构表", Desc = "查询机构表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenOrg", Get = "", Post = "ReaCenOrg", Return = "BaseResultList<ReaCenOrg>", ReturnType = "ListReaCenOrg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCenOrg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenOrg(ReaCenOrg entity);

        [ServiceContractDescription(Name = "查询机构表(HQL)", Desc = "查询机构表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaCenOrg>", ReturnType = "ListReaCenOrg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCenOrgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenOrgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询机构表", Desc = "通过主键ID查询机构表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaCenOrg>", ReturnType = "ReaCenOrg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCenOrgById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenOrgById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "根据机构ID查询机构信息单列树", Desc = "根据机构ID查询机构信息单列树", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgTreeByOrgID?id={id}&orgType={orgType}", Get = "id={id}&orgType={orgType}", Post = "", Return = "BaseResultDataValue", ReturnType = "Tree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_SearchReaCenOrgTreeByOrgID?id={id}&orgType={orgType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenOrgTreeByOrgID(string id, int orgType);

        [ServiceContractDescription(Name = "根据机构ID查询机构信息列表树", Desc = "根据机构ID查询机构信息列表树", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgListTreeByOrgID?id={id}&fields={fields}&orgType={orgType}", Get = "id={id}&fields={fields}&orgType={orgType}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeReaCenOrg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_SearchReaCenOrgListTreeByOrgID?id={id}&fields={fields}&orgType={orgType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenOrgListTreeByOrgID(string id, string fields, int orgType);

        [ServiceContractDescription(Name = "查询机构信息列表数据(HQL)(可获取机构子孙节点)", Desc = "查询机构信息列表数据(HQL)(可获取机构子孙节点)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgAndChildListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isSearchChild={isSearchChild}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isSearchChild={isSearchChild}", Post = "", Return = "BaseResultList<ReaCenOrg>", ReturnType = "ListReaCenOrg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCenOrgAndChildListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isSearchChild={isSearchChild}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenOrgAndChildListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isSearchChild);

        #endregion

        #region ReaGoods

        [ServiceContractDescription(Name = "新增平台货品表", Desc = "新增平台货品表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaGoods", Get = "", Post = "ReaGoods", Return = "BaseResultDataValue", ReturnType = "ReaGoods")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaGoods(ReaGoods entity);

        [ServiceContractDescription(Name = "修改平台货品表", Desc = "修改平台货品表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoods", Get = "", Post = "ReaGoods", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoods(ReaGoods entity);

        [ServiceContractDescription(Name = "修改平台货品表指定的属性", Desc = "修改平台货品表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsByField", Get = "", Post = "ReaGoods", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsByField(ReaGoods entity, string fields);

        [ServiceContractDescription(Name = "删除平台货品表", Desc = "删除平台货品表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaGoods?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaGoods?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaGoods(long id);

        [ServiceContractDescription(Name = "查询平台货品表", Desc = "查询平台货品表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoods", Get = "", Post = "ReaGoods", Return = "BaseResultList<ReaGoods>", ReturnType = "ListReaGoods")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoods(ReaGoods entity);

        [ServiceContractDescription(Name = "查询平台货品表(HQL)", Desc = "查询平台货品表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoods>", ReturnType = "ListReaGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台货品表", Desc = "通过主键ID查询平台货品表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoods>", ReturnType = "ReaGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "根据选中行货品id修改平台货品表的相同码", Desc = "根据选中行货品id修改平台货品表的相同码", Url = "ReaSysManageService.svc/ST_UDTO_UpdateGonvertGroupCode?idList={idList}&Code={Code}", Get = "idList={idList}&Code={Code}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_UpdateGonvertGroupCode?idList={idList}&Code={Code}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGonvertGroupCode(string idList, string Code);

        #endregion

        #region ReaGoodsOrgLink

        [ServiceContractDescription(Name = "查询货品信息(HQL),需要过滤传入供应商已维护并启用的货品信息", Desc = "查询货品机构关系(HQL),需要过滤传入供应商已维护并启用的货品信息", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByCenOrgId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&cenOrgId={cenOrgId}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&cenOrgId={cenOrgId}", Post = "", Return = "BaseResultList<ReaGoods>", ReturnType = "ListReaGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsByCenOrgId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&cenOrgId={cenOrgId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsByCenOrgId(int page, int limit, string fields, string where, string sort, bool isPlanish, long cenOrgId);


        [ServiceContractDescription(Name = "新增货品机构关系", Desc = "新增货品机构关系", Url = "ReaSysManageService.svc/ST_UDTO_AddReaGoodsOrgLink", Get = "", Post = "ReaGoodsOrgLink", Return = "BaseResultDataValue", ReturnType = "ReaGoodsOrgLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaGoodsOrgLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaGoodsOrgLink(ReaGoodsOrgLink entity);

        [ServiceContractDescription(Name = "修改货品机构关系", Desc = "修改货品机构关系", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsOrgLink", Get = "", Post = "ReaGoodsOrgLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsOrgLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsOrgLink(ReaGoodsOrgLink entity);

        [ServiceContractDescription(Name = "修改货品机构关系指定的属性", Desc = "修改货品机构关系指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsOrgLinkByField", Get = "", Post = "ReaGoodsOrgLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsOrgLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsOrgLinkByField(ReaGoodsOrgLink entity, string fields);

        [ServiceContractDescription(Name = "删除货品机构关系", Desc = "删除货品机构关系", Url = "ReaSysManageService.svc/ST_UDTO_DelReaGoodsOrgLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaGoodsOrgLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaGoodsOrgLink(long id);

        [ServiceContractDescription(Name = "查询货品机构关系", Desc = "查询货品机构关系", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLink", Get = "", Post = "ReaGoodsOrgLink", Return = "BaseResultList<ReaGoodsOrgLink>", ReturnType = "ListReaGoodsOrgLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsOrgLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLink(ReaGoodsOrgLink entity);

        [ServiceContractDescription(Name = "查询货品机构关系(HQL)", Desc = "查询货品机构关系(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsOrgLink>", ReturnType = "ListReaGoodsOrgLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsOrgLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询货品机构关系", Desc = "通过主键ID查询货品机构关系", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsOrgLink>", ReturnType = "ReaGoodsOrgLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsOrgLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "根据机构Id,查询货品机构关系列表数据(可获取机构Id子孙节点)", Desc = "根据机构Id,查询货品机构关系列表数据(可获取机构Id子孙节点)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkAndChildListByHQL?orgId={orgId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isSearchChild={isSearchChild}&orgType={orgType}", Get = "orgId={orgId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isSearchChild={isSearchChild}&orgType={orgType}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsOrgLinkAndChildListByHQL?orgId={orgId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isSearchChild={isSearchChild}&orgType={orgType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkAndChildListByHQL(long orgId, string where, int page, int limit, string fields, string sort, bool isPlanish, bool isSearchChild, int orgType);
        #endregion

        #region ReaGoodsBarcodeOperation
        [ServiceContractDescription(Name = "查询货品条码操作记录信息(HQL)", Desc = "查询货品条码操作记录信息(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsBarcodeOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsBarcodeOperation>", ReturnType = "ListReaGoodsBarcodeOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsBarcodeOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsBarcodeOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        #endregion

        #region ReaBmsInDoc

        [ServiceContractDescription(Name = "新增入库总单表", Desc = "新增入库总单表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsInDoc", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsInDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsInDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsInDoc(ReaBmsInDoc entity);

        [ServiceContractDescription(Name = "修改入库总单表", Desc = "修改入库总单表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDoc", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsInDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsInDoc(ReaBmsInDoc entity);

        [ServiceContractDescription(Name = "修改入库总单表指定的属性", Desc = "修改入库总单表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDocByField", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsInDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsInDocByField(ReaBmsInDoc entity, string fields);

        [ServiceContractDescription(Name = "删除入库总单表", Desc = "删除入库总单表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsInDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsInDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsInDoc(long id);

        [ServiceContractDescription(Name = "查询入库总单表", Desc = "查询入库总单表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDoc", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultList<ReaBmsInDoc>", ReturnType = "ListReaBmsInDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDoc(ReaBmsInDoc entity);

        [ServiceContractDescription(Name = "查询入库总单表(HQL)", Desc = "查询入库总单表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsInDoc>", ReturnType = "ListReaBmsInDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询入库总单表", Desc = "通过主键ID查询入库总单表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsInDoc>", ReturnType = "ReaBmsInDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDocById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsInDtl

        [ServiceContractDescription(Name = "新增入库明细表", Desc = "新增入库明细表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsInDtl", Get = "", Post = "ReaBmsInDtl", Return = "BaseResultDataValue", ReturnType = "ReaBmsInDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsInDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsInDtl(ReaBmsInDtl entity);

        [ServiceContractDescription(Name = "修改入库明细表", Desc = "修改入库明细表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDtl", Get = "", Post = "ReaBmsInDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsInDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsInDtl(ReaBmsInDtl entity);

        [ServiceContractDescription(Name = "修改入库明细表指定的属性", Desc = "修改入库明细表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDtlByField", Get = "", Post = "ReaBmsInDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsInDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsInDtlByField(ReaBmsInDtl entity, string fields);

        [ServiceContractDescription(Name = "删除入库明细表", Desc = "删除入库明细表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsInDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsInDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsInDtl(long id);

        [ServiceContractDescription(Name = "查询入库明细表", Desc = "查询入库明细表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDtl", Get = "", Post = "ReaBmsInDtl", Return = "BaseResultList<ReaBmsInDtl>", ReturnType = "ListReaBmsInDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDtl(ReaBmsInDtl entity);

        [ServiceContractDescription(Name = "查询入库明细表(HQL)", Desc = "查询入库明细表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsInDtl>", ReturnType = "ListReaBmsInDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询入库明细表", Desc = "通过主键ID查询入库明细表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsInDtl>", ReturnType = "ReaBmsInDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsOutDoc

        [ServiceContractDescription(Name = "新增出库总单表", Desc = "新增出库总单表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsOutDoc", Get = "", Post = "ReaBmsOutDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsOutDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsOutDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsOutDoc(ReaBmsOutDoc entity);

        [ServiceContractDescription(Name = "修改出库总单表", Desc = "修改出库总单表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsOutDoc", Get = "", Post = "ReaBmsOutDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsOutDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsOutDoc(ReaBmsOutDoc entity);

        [ServiceContractDescription(Name = "修改出库总单表指定的属性", Desc = "修改出库总单表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsOutDocByField", Get = "", Post = "ReaBmsOutDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsOutDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsOutDocByField(ReaBmsOutDoc entity, string fields);

        [ServiceContractDescription(Name = "删除出库总单表", Desc = "删除出库总单表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsOutDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsOutDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsOutDoc(long id);

        [ServiceContractDescription(Name = "查询出库总单表", Desc = "查询出库总单表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsOutDoc", Get = "", Post = "ReaBmsOutDoc", Return = "BaseResultList<ReaBmsOutDoc>", ReturnType = "ListReaBmsOutDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsOutDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsOutDoc(ReaBmsOutDoc entity);

        [ServiceContractDescription(Name = "查询出库总单表(HQL)", Desc = "查询出库总单表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsOutDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsOutDoc>", ReturnType = "ListReaBmsOutDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsOutDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsOutDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询出库总单表", Desc = "通过主键ID查询出库总单表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsOutDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsOutDoc>", ReturnType = "ReaBmsOutDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsOutDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsOutDocById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsOutDtl

        [ServiceContractDescription(Name = "新增出库明细表", Desc = "新增出库明细表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsOutDtl", Get = "", Post = "ReaBmsOutDtl", Return = "BaseResultDataValue", ReturnType = "ReaBmsOutDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsOutDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsOutDtl(ReaBmsOutDtl entity);

        [ServiceContractDescription(Name = "修改出库明细表", Desc = "修改出库明细表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsOutDtl", Get = "", Post = "ReaBmsOutDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsOutDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsOutDtl(ReaBmsOutDtl entity);

        [ServiceContractDescription(Name = "修改出库明细表指定的属性", Desc = "修改出库明细表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsOutDtlByField", Get = "", Post = "ReaBmsOutDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsOutDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsOutDtlByField(ReaBmsOutDtl entity, string fields);

        [ServiceContractDescription(Name = "删除出库明细表", Desc = "删除出库明细表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsOutDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsOutDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsOutDtl(long id);

        [ServiceContractDescription(Name = "查询出库明细表", Desc = "查询出库明细表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsOutDtl", Get = "", Post = "ReaBmsOutDtl", Return = "BaseResultList<ReaBmsOutDtl>", ReturnType = "ListReaBmsOutDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsOutDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsOutDtl(ReaBmsOutDtl entity);

        [ServiceContractDescription(Name = "查询出库明细表(HQL)", Desc = "查询出库明细表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsOutDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsOutDtl>", ReturnType = "ListReaBmsOutDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsOutDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsOutDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询出库明细表", Desc = "通过主键ID查询出库明细表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsOutDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsOutDtl>", ReturnType = "ReaBmsOutDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsOutDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsOutDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsReqDoc

        [ServiceContractDescription(Name = "新增申请总单表", Desc = "新增申请总单表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsReqDoc", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsReqDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsReqDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsReqDoc(ReaBmsReqDoc entity);

        [ServiceContractDescription(Name = "修改申请总单表", Desc = "修改申请总单表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDoc", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDoc(ReaBmsReqDoc entity);

        [ServiceContractDescription(Name = "修改申请总单表指定的属性", Desc = "修改申请总单表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDocByField", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDocByField(ReaBmsReqDoc entity, string fields);

        [ServiceContractDescription(Name = "删除申请总单表", Desc = "删除申请总单表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsReqDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsReqDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsReqDoc(long id);

        [ServiceContractDescription(Name = "查询申请总单表", Desc = "查询申请总单表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDoc", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultList<ReaBmsReqDoc>", ReturnType = "ListReaBmsReqDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDoc(ReaBmsReqDoc entity);

        [ServiceContractDescription(Name = "查询申请总单表(HQL)", Desc = "查询申请总单表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsReqDoc>", ReturnType = "ListReaBmsReqDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询申请总单表", Desc = "通过主键ID查询申请总单表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsReqDoc>", ReturnType = "ReaBmsReqDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDocById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsReqDtl

        [ServiceContractDescription(Name = "新增申请明细表", Desc = "新增申请明细表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsReqDtl", Get = "", Post = "ReaBmsReqDtl", Return = "BaseResultDataValue", ReturnType = "ReaBmsReqDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsReqDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsReqDtl(ReaBmsReqDtl entity);

        [ServiceContractDescription(Name = "修改申请明细表", Desc = "修改申请明细表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDtl", Get = "", Post = "ReaBmsReqDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDtl(ReaBmsReqDtl entity);

        [ServiceContractDescription(Name = "修改申请明细表指定的属性", Desc = "修改申请明细表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDtlByField", Get = "", Post = "ReaBmsReqDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDtlByField(ReaBmsReqDtl entity, string fields);

        [ServiceContractDescription(Name = "删除申请明细表", Desc = "删除申请明细表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsReqDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsReqDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsReqDtl(long id);

        [ServiceContractDescription(Name = "查询申请明细表", Desc = "查询申请明细表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDtl", Get = "", Post = "ReaBmsReqDtl", Return = "BaseResultList<ReaBmsReqDtl>", ReturnType = "ListReaBmsReqDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDtl(ReaBmsReqDtl entity);

        [ServiceContractDescription(Name = "查询申请明细表(HQL)", Desc = "查询申请明细表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsReqDtl>", ReturnType = "ListReaBmsReqDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询申请明细表", Desc = "通过主键ID查询申请明细表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsReqDtl>", ReturnType = "ReaBmsReqDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsTransferDoc

        [ServiceContractDescription(Name = "新增Rea_BmsTransferDoc", Desc = "新增Rea_BmsTransferDoc", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsTransferDoc", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsTransferDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsTransferDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsTransferDoc(ReaBmsTransferDoc entity);

        [ServiceContractDescription(Name = "修改Rea_BmsTransferDoc", Desc = "修改Rea_BmsTransferDoc", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDoc", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsTransferDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsTransferDoc(ReaBmsTransferDoc entity);

        [ServiceContractDescription(Name = "修改Rea_BmsTransferDoc指定的属性", Desc = "修改Rea_BmsTransferDoc指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDocByField", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsTransferDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsTransferDocByField(ReaBmsTransferDoc entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_BmsTransferDoc", Desc = "删除Rea_BmsTransferDoc", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsTransferDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsTransferDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsTransferDoc(long id);

        [ServiceContractDescription(Name = "查询Rea_BmsTransferDoc", Desc = "查询Rea_BmsTransferDoc", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDoc", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultList<ReaBmsTransferDoc>", ReturnType = "ListReaBmsTransferDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsTransferDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsTransferDoc(ReaBmsTransferDoc entity);

        [ServiceContractDescription(Name = "查询Rea_BmsTransferDoc(HQL)", Desc = "查询Rea_BmsTransferDoc(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsTransferDoc>", ReturnType = "ListReaBmsTransferDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsTransferDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsTransferDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_BmsTransferDoc", Desc = "通过主键ID查询Rea_BmsTransferDoc", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsTransferDoc>", ReturnType = "ReaBmsTransferDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsTransferDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsTransferDocById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsTransferDtl

        [ServiceContractDescription(Name = "新增Rea_BmsTransferDtl", Desc = "新增Rea_BmsTransferDtl", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsTransferDtl", Get = "", Post = "ReaBmsTransferDtl", Return = "BaseResultDataValue", ReturnType = "ReaBmsTransferDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsTransferDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsTransferDtl(ReaBmsTransferDtl entity);

        [ServiceContractDescription(Name = "修改Rea_BmsTransferDtl", Desc = "修改Rea_BmsTransferDtl", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDtl", Get = "", Post = "ReaBmsTransferDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsTransferDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsTransferDtl(ReaBmsTransferDtl entity);

        [ServiceContractDescription(Name = "修改Rea_BmsTransferDtl指定的属性", Desc = "修改Rea_BmsTransferDtl指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDtlByField", Get = "", Post = "ReaBmsTransferDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsTransferDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsTransferDtlByField(ReaBmsTransferDtl entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_BmsTransferDtl", Desc = "删除Rea_BmsTransferDtl", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsTransferDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsTransferDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsTransferDtl(long id);

        [ServiceContractDescription(Name = "查询Rea_BmsTransferDtl", Desc = "查询Rea_BmsTransferDtl", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDtl", Get = "", Post = "ReaBmsTransferDtl", Return = "BaseResultList<ReaBmsTransferDtl>", ReturnType = "ListReaBmsTransferDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsTransferDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsTransferDtl(ReaBmsTransferDtl entity);

        [ServiceContractDescription(Name = "查询Rea_BmsTransferDtl(HQL)", Desc = "查询Rea_BmsTransferDtl(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsTransferDtl>", ReturnType = "ListReaBmsTransferDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsTransferDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsTransferDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_BmsTransferDtl", Desc = "通过主键ID查询Rea_BmsTransferDtl", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsTransferDtl>", ReturnType = "ReaBmsTransferDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsTransferDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsTransferDtlById(long id, string fields, bool isPlanish);
        #endregion        

        #region ReaDeptGoods

        [ServiceContractDescription(Name = "新增部门货品表", Desc = "新增部门货品表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaDeptGoods", Get = "", Post = "ReaDeptGoods", Return = "BaseResultDataValue", ReturnType = "ReaDeptGoods")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaDeptGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaDeptGoods(ReaDeptGoods entity);

        [ServiceContractDescription(Name = "修改部门货品表", Desc = "修改部门货品表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaDeptGoods", Get = "", Post = "ReaDeptGoods", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaDeptGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaDeptGoods(ReaDeptGoods entity);

        [ServiceContractDescription(Name = "修改部门货品表指定的属性", Desc = "修改部门货品表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaDeptGoodsByField", Get = "", Post = "ReaDeptGoods", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaDeptGoodsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaDeptGoodsByField(ReaDeptGoods entity, string fields);

        [ServiceContractDescription(Name = "删除部门货品表", Desc = "删除部门货品表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaDeptGoods?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaDeptGoods?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaDeptGoods(long id);

        [ServiceContractDescription(Name = "查询部门货品表", Desc = "查询部门货品表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaDeptGoods", Get = "", Post = "ReaDeptGoods", Return = "BaseResultList<ReaDeptGoods>", ReturnType = "ListReaDeptGoods")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaDeptGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaDeptGoods(ReaDeptGoods entity);

        [ServiceContractDescription(Name = "查询部门货品表(HQL)", Desc = "查询部门货品表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaDeptGoodsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaDeptGoods>", ReturnType = "ListReaDeptGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaDeptGoodsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaDeptGoodsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询部门货品表", Desc = "通过主键ID查询部门货品表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaDeptGoodsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaDeptGoods>", ReturnType = "ReaDeptGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaDeptGoodsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaDeptGoodsById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "根据HRDeptID查询货品列表", Desc = "根据HRDeptID查询货品列表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHRDeptID?deptID={deptID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "deptID={deptID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsByHRDeptID?deptID={deptID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsByHRDeptID(long deptID, string where, int page, int limit, string fields, string sort, bool isPlanish);
        #endregion

        #region ReaGoodsLot

        [ServiceContractDescription(Name = "新增货品批号表", Desc = "新增货品批号表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaGoodsLot", Get = "", Post = "ReaGoodsLot", Return = "BaseResultDataValue", ReturnType = "ReaGoodsLot")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaGoodsLot", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaGoodsLot(ReaGoodsLot entity);

        [ServiceContractDescription(Name = "修改货品批号表", Desc = "修改货品批号表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsLot", Get = "", Post = "ReaGoodsLot", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsLot", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsLot(ReaGoodsLot entity);

        [ServiceContractDescription(Name = "修改货品批号表指定的属性", Desc = "修改货品批号表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsLotByField", Get = "", Post = "ReaGoodsLot", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsLotByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsLotByField(ReaGoodsLot entity, string fields);

        [ServiceContractDescription(Name = "货品批号信息性能验证处理,同时更新相应库存批次的性能验证结果", Desc = "货品批号信息性能验证处理,同时更新相应库存批次的性能验证结果", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsLotByVerificationField", Get = "", Post = "ReaGoodsLot", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsLotByVerificationField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsLotByVerificationField(ReaGoodsLot entity, string fields);

        [ServiceContractDescription(Name = "删除货品批号表", Desc = "删除货品批号表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaGoodsLot?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaGoodsLot?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaGoodsLot(long id);

        [ServiceContractDescription(Name = "查询货品批号表", Desc = "查询货品批号表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsLot", Get = "", Post = "ReaGoodsLot", Return = "BaseResultList<ReaGoodsLot>", ReturnType = "ListReaGoodsLot")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsLot", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsLot(ReaGoodsLot entity);

        [ServiceContractDescription(Name = "查询货品批号表(HQL)", Desc = "查询货品批号表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsLotByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsLot>", ReturnType = "ListReaGoodsLot")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsLotByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsLotByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询货品批号表", Desc = "通过主键ID查询货品批号表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsLotById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsLot>", ReturnType = "ReaGoodsLot")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsLotById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsLotById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaGoodsRegister
        [ServiceContractDescription(Name = "通过FormData方式新增产品注册证信息", Desc = "通过FormData方式新增产品注册证信息", Url = "ReaSysManageService.svc/ST_UDTO_AddReaGoodsRegisterAndUploadRegisterFile", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaGoodsRegisterAndUploadRegisterFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_AddReaGoodsRegisterAndUploadRegisterFile();

        [ServiceContractDescription(Name = "通过FormData方式更新产品注册证信息", Desc = "通过FormData方式更新产品注册证信息", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsRegisterAndUploadRegisterFileByField", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsRegisterAndUploadRegisterFileByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_UpdateReaGoodsRegisterAndUploadRegisterFileByField();

        [ServiceContractDescription(Name = "预览产品注册证文件", Desc = "预览产品注册证文件", Url = "ReaSysManageService.svc/ST_UDTO_ReaGoodsRegisterPreviewPdf?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ReaGoodsRegisterPreviewPdf?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_ReaGoodsRegisterPreviewPdf(long id, long operateType);

        [ServiceContractDescription(Name = "查询过滤掉重复的注册证编号的产品注册证表(HQL)", Desc = "查询过滤掉重复的注册证编号的产品注册证表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsRegister>", ReturnType = "ListReaGoodsRegister")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);


        [ServiceContractDescription(Name = "新增货品注册证表", Desc = "新增货品注册证表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaGoodsRegister", Get = "", Post = "ReaGoodsRegister", Return = "BaseResultDataValue", ReturnType = "ReaGoodsRegister")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaGoodsRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaGoodsRegister(ReaGoodsRegister entity);

        [ServiceContractDescription(Name = "修改货品注册证表", Desc = "修改货品注册证表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsRegister", Get = "", Post = "ReaGoodsRegister", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsRegister(ReaGoodsRegister entity);

        [ServiceContractDescription(Name = "修改货品注册证表指定的属性", Desc = "修改货品注册证表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsRegisterByField", Get = "", Post = "ReaGoodsRegister", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsRegisterByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsRegisterByField(ReaGoodsRegister entity, string fields);

        [ServiceContractDescription(Name = "删除货品注册证表", Desc = "删除货品注册证表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaGoodsRegister?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaGoodsRegister?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaGoodsRegister(long id);

        [ServiceContractDescription(Name = "查询货品注册证表", Desc = "查询货品注册证表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsRegister", Get = "", Post = "ReaGoodsRegister", Return = "BaseResultList<ReaGoodsRegister>", ReturnType = "ListReaGoodsRegister")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsRegister(ReaGoodsRegister entity);

        [ServiceContractDescription(Name = "查询货品注册证表(HQL)", Desc = "查询货品注册证表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsRegisterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsRegister>", ReturnType = "ListReaGoodsRegister")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsRegisterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsRegisterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询货品注册证表", Desc = "通过主键ID查询货品注册证表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsRegisterById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsRegister>", ReturnType = "ReaGoodsRegister")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsRegisterById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsRegisterById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaGoodsUnit

        [ServiceContractDescription(Name = "新增货品包装单位表", Desc = "新增货品包装单位表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaGoodsUnit", Get = "", Post = "ReaGoodsUnit", Return = "BaseResultDataValue", ReturnType = "ReaGoodsUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaGoodsUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaGoodsUnit(ReaGoodsUnit entity);

        [ServiceContractDescription(Name = "修改货品包装单位表", Desc = "修改货品包装单位表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsUnit", Get = "", Post = "ReaGoodsUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsUnit(ReaGoodsUnit entity);

        [ServiceContractDescription(Name = "修改货品包装单位表指定的属性", Desc = "修改货品包装单位表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsUnitByField", Get = "", Post = "ReaGoodsUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsUnitByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsUnitByField(ReaGoodsUnit entity, string fields);

        [ServiceContractDescription(Name = "删除货品包装单位表", Desc = "删除货品包装单位表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaGoodsUnit?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaGoodsUnit?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaGoodsUnit(long id);

        [ServiceContractDescription(Name = "查询货品包装单位表", Desc = "查询货品包装单位表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsUnit", Get = "", Post = "ReaGoodsUnit", Return = "BaseResultList<ReaGoodsUnit>", ReturnType = "ListReaGoodsUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsUnit(ReaGoodsUnit entity);

        [ServiceContractDescription(Name = "查询货品包装单位表(HQL)", Desc = "查询货品包装单位表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsUnit>", ReturnType = "ListReaGoodsUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsUnitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询货品包装单位表", Desc = "通过主键ID查询货品包装单位表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsUnit>", ReturnType = "ReaGoodsUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsUnitById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaPlace

        [ServiceContractDescription(Name = "新增货位表", Desc = "新增货位表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaPlace", Get = "", Post = "ReaPlace", Return = "BaseResultDataValue", ReturnType = "ReaPlace")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaPlace", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaPlace(ReaPlace entity);

        [ServiceContractDescription(Name = "修改货位表", Desc = "修改货位表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaPlace", Get = "", Post = "ReaPlace", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaPlace", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaPlace(ReaPlace entity);

        [ServiceContractDescription(Name = "修改货位表指定的属性", Desc = "修改货位表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaPlaceByField", Get = "", Post = "ReaPlace", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaPlaceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaPlaceByField(ReaPlace entity, string fields);

        [ServiceContractDescription(Name = "删除货位表", Desc = "删除货位表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaPlace?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaPlace?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaPlace(long id);

        [ServiceContractDescription(Name = "查询货位表", Desc = "查询货位表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaPlace", Get = "", Post = "ReaPlace", Return = "BaseResultList<ReaPlace>", ReturnType = "ListReaPlace")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaPlace", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaPlace(ReaPlace entity);

        [ServiceContractDescription(Name = "查询货位表(HQL)", Desc = "查询货位表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaPlaceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaPlace>", ReturnType = "ListReaPlace")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaPlaceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaPlaceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询货位表", Desc = "通过主键ID查询货位表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaPlaceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaPlace>", ReturnType = "ReaPlace")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaPlaceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaPlaceById(long id, string fields, bool isPlanish);


        #endregion

        #region ReaStorage

        [ServiceContractDescription(Name = "新增存储库房表", Desc = "新增存储库房表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaStorage", Get = "", Post = "ReaStorage", Return = "BaseResultDataValue", ReturnType = "ReaStorage")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaStorage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaStorage(ReaStorage entity);

        [ServiceContractDescription(Name = "修改存储库房表", Desc = "修改存储库房表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaStorage", Get = "", Post = "ReaStorage", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaStorage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaStorage(ReaStorage entity);

        [ServiceContractDescription(Name = "修改存储库房表指定的属性", Desc = "修改存储库房表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaStorageByField", Get = "", Post = "ReaStorage", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaStorageByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaStorageByField(ReaStorage entity, string fields);

        [ServiceContractDescription(Name = "删除存储库房表", Desc = "删除存储库房表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaStorage?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaStorage?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaStorage(long id);

        [ServiceContractDescription(Name = "查询存储库房表", Desc = "查询存储库房表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaStorage", Get = "", Post = "ReaStorage", Return = "BaseResultList<ReaStorage>", ReturnType = "ListReaStorage")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaStorage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaStorage(ReaStorage entity);

        [ServiceContractDescription(Name = "查询存储库房表(HQL)", Desc = "查询存储库房表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaStorageByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaStorage>", ReturnType = "ListReaStorage")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaStorageByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaStorageByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询存储库房表", Desc = "通过主键ID查询存储库房表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaStorageById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaStorage>", ReturnType = "ReaStorage")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaStorageById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaStorageById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaReqOperation

        [ServiceContractDescription(Name = "新增采购申请单、订单操作记录表", Desc = "新增采购申请单、订单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaReqOperation", Get = "", Post = "ReaReqOperation", Return = "BaseResultDataValue", ReturnType = "ReaReqOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaReqOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaReqOperation(ReaReqOperation entity);

        [ServiceContractDescription(Name = "修改采购申请单、订单操作记录表", Desc = "修改采购申请单、订单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaReqOperation", Get = "", Post = "ReaReqOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaReqOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaReqOperation(ReaReqOperation entity);

        [ServiceContractDescription(Name = "修改采购申请单、订单操作记录表指定的属性", Desc = "修改采购申请单、订单操作记录表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaReqOperationByField", Get = "", Post = "ReaReqOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaReqOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaReqOperationByField(ReaReqOperation entity, string fields);

        [ServiceContractDescription(Name = "删除采购申请单、订单操作记录表", Desc = "删除采购申请单、订单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaReqOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaReqOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaReqOperation(long id);

        [ServiceContractDescription(Name = "查询采购申请单、订单操作记录表", Desc = "查询采购申请单、订单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaReqOperation", Get = "", Post = "ReaReqOperation", Return = "BaseResultList<ReaReqOperation>", ReturnType = "ListReaReqOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaReqOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaReqOperation(ReaReqOperation entity);

        [ServiceContractDescription(Name = "查询采购申请单、订单操作记录表(HQL)", Desc = "查询采购申请单、订单操作记录表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaReqOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaReqOperation>", ReturnType = "ListReaReqOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaReqOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaReqOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询采购申请单、订单操作记录表", Desc = "通过主键ID查询采购申请单、订单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaReqOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaReqOperation>", ReturnType = "ReaReqOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaReqOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaReqOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsQtyDtl

        [ServiceContractDescription(Name = "新增货品库存表", Desc = "新增货品库存表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsQtyDtl", Get = "", Post = "ReaBmsQtyDtl", Return = "BaseResultDataValue", ReturnType = "ReaBmsQtyDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsQtyDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsQtyDtl(ReaBmsQtyDtl entity);

        [ServiceContractDescription(Name = "修改货品库存表", Desc = "修改货品库存表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsQtyDtl", Get = "", Post = "ReaBmsQtyDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsQtyDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsQtyDtl(ReaBmsQtyDtl entity);

        [ServiceContractDescription(Name = "修改货品库存表指定的属性", Desc = "修改货品库存表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsQtyDtlByField", Get = "", Post = "ReaBmsQtyDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsQtyDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsQtyDtlByField(ReaBmsQtyDtl entity, string fields);

        [ServiceContractDescription(Name = "删除货品库存表", Desc = "删除货品库存表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsQtyDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsQtyDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsQtyDtl(long id);

        [ServiceContractDescription(Name = "查询货品库存表", Desc = "查询货品库存表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyDtl", Get = "", Post = "ReaBmsQtyDtl", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ListReaBmsQtyDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtl(ReaBmsQtyDtl entity);

        [ServiceContractDescription(Name = "查询货品库存表(HQL)", Desc = "查询货品库存表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ListReaBmsQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询货品库存表", Desc = "通过主键ID查询货品库存表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ReaBmsQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlById(long id, string fields, bool isPlanish);

        #endregion

        #region ReaCheckInOperation

        [ServiceContractDescription(Name = "新增验收单、入库单操作记录表", Desc = "新增验收单、入库单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaCheckInOperation", Get = "", Post = "ReaCheckInOperation", Return = "BaseResultDataValue", ReturnType = "ReaCheckInOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaCheckInOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaCheckInOperation(ReaCheckInOperation entity);

        [ServiceContractDescription(Name = "修改验收单、入库单操作记录表", Desc = "修改验收单、入库单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaCheckInOperation", Get = "", Post = "ReaCheckInOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaCheckInOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaCheckInOperation(ReaCheckInOperation entity);

        [ServiceContractDescription(Name = "修改验收单、入库单操作记录表指定的属性", Desc = "修改验收单、入库单操作记录表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaCheckInOperationByField", Get = "", Post = "ReaCheckInOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaCheckInOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaCheckInOperationByField(ReaCheckInOperation entity, string fields);

        [ServiceContractDescription(Name = "删除验收单、入库单操作记录表", Desc = "删除验收单、入库单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaCheckInOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaCheckInOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaCheckInOperation(long id);

        [ServiceContractDescription(Name = "查询验收单、入库单操作记录表", Desc = "查询验收单、入库单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCheckInOperation", Get = "", Post = "ReaCheckInOperation", Return = "BaseResultList<ReaCheckInOperation>", ReturnType = "ListReaCheckInOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCheckInOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCheckInOperation(ReaCheckInOperation entity);

        [ServiceContractDescription(Name = "查询验收单、入库单操作记录表(HQL)", Desc = "查询验收单、入库单操作记录表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCheckInOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaCheckInOperation>", ReturnType = "ListReaCheckInOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCheckInOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCheckInOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询验收单、入库单操作记录表", Desc = "通过主键ID查询验收单、入库单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCheckInOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaCheckInOperation>", ReturnType = "ReaCheckInOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCheckInOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCheckInOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaCenBarCodeFormat

        [ServiceContractDescription(Name = "新增供应商条码格式表", Desc = "新增供应商条码格式表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaCenBarCodeFormat", Get = "", Post = "ReaCenBarCodeFormat", Return = "BaseResultDataValue", ReturnType = "ReaCenBarCodeFormat")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaCenBarCodeFormat", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaCenBarCodeFormat(ReaCenBarCodeFormat entity);

        [ServiceContractDescription(Name = "修改供应商条码格式表", Desc = "修改供应商条码格式表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaCenBarCodeFormat", Get = "", Post = "ReaCenBarCodeFormat", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaCenBarCodeFormat", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaCenBarCodeFormat(ReaCenBarCodeFormat entity);

        [ServiceContractDescription(Name = "修改供应商条码格式表指定的属性", Desc = "修改供应商条码格式表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaCenBarCodeFormatByField", Get = "", Post = "ReaCenBarCodeFormat", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaCenBarCodeFormatByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaCenBarCodeFormatByField(ReaCenBarCodeFormat entity, string fields);

        [ServiceContractDescription(Name = "删除供应商条码格式表", Desc = "删除供应商条码格式表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaCenBarCodeFormat?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaCenBarCodeFormat?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaCenBarCodeFormat(long id);

        [ServiceContractDescription(Name = "查询供应商条码格式表", Desc = "查询供应商条码格式表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenBarCodeFormat", Get = "", Post = "ReaCenBarCodeFormat", Return = "BaseResultList<ReaCenBarCodeFormat>", ReturnType = "ListReaCenBarCodeFormat")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCenBarCodeFormat", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenBarCodeFormat(ReaCenBarCodeFormat entity);

        [ServiceContractDescription(Name = "查询供应商条码格式表(HQL)", Desc = "查询供应商条码格式表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenBarCodeFormatByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaCenBarCodeFormat>", ReturnType = "ListReaCenBarCodeFormat")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCenBarCodeFormatByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenBarCodeFormatByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询供应商条码格式表", Desc = "通过主键ID查询供应商条码格式表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenBarCodeFormatById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaCenBarCodeFormat>", ReturnType = "ReaCenBarCodeFormat")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCenBarCodeFormatById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenBarCodeFormatById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "导出条码规则信息文件给离线客户端使用", Desc = "导出条码规则信息文件给离线客户端使用", Url = "ReaSysManageService.svc/ST_UDTO_DownLoadReaCenBarCodeFormatOfPlatformOrgNo?platformOrgNo={platformOrgNo}&operateType={operateType}", Get = "platformOrgNo={platformOrgNo}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_DownLoadReaCenBarCodeFormatOfPlatformOrgNo?platformOrgNo={platformOrgNo}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_DownLoadReaCenBarCodeFormatOfPlatformOrgNo(string platformOrgNo, long operateType);

        [ServiceContractDescription(Name = "离线客户端导入下载的条码规则附件文件", Desc = "离线客户端导入下载的条码规则附件文件", Url = "ReaSysManageService.svc/ST_UDTO_UploadReaCenBarCodeFormatOfAttachment", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UploadReaCenBarCodeFormatOfAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message ST_UDTO_UploadReaCenBarCodeFormatOfAttachment();
        #endregion

        #region ReaChooseGoodsTemplate

        [ServiceContractDescription(Name = "新增申请明细及订单明细模板表", Desc = "新增申请明细及订单明细模板表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaChooseGoodsTemplate", Get = "", Post = "ReaChooseGoodsTemplate", Return = "BaseResultDataValue", ReturnType = "ReaChooseGoodsTemplate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaChooseGoodsTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaChooseGoodsTemplate(ReaChooseGoodsTemplate entity);

        [ServiceContractDescription(Name = "修改申请明细及订单明细模板表", Desc = "修改申请明细及订单明细模板表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaChooseGoodsTemplate", Get = "", Post = "ReaChooseGoodsTemplate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaChooseGoodsTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaChooseGoodsTemplate(ReaChooseGoodsTemplate entity);

        [ServiceContractDescription(Name = "修改申请明细及订单明细模板表指定的属性", Desc = "修改申请明细及订单明细模板表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaChooseGoodsTemplateByField", Get = "", Post = "ReaChooseGoodsTemplate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaChooseGoodsTemplateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaChooseGoodsTemplateByField(ReaChooseGoodsTemplate entity, string fields);

        [ServiceContractDescription(Name = "删除申请明细及订单明细模板表", Desc = "删除申请明细及订单明细模板表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaChooseGoodsTemplate?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaChooseGoodsTemplate?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaChooseGoodsTemplate(long id);

        [ServiceContractDescription(Name = "查询申请明细及订单明细模板表", Desc = "查询申请明细及订单明细模板表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaChooseGoodsTemplate", Get = "", Post = "ReaChooseGoodsTemplate", Return = "BaseResultList<ReaChooseGoodsTemplate>", ReturnType = "ListReaChooseGoodsTemplate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaChooseGoodsTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaChooseGoodsTemplate(ReaChooseGoodsTemplate entity);

        [ServiceContractDescription(Name = "查询申请明细及订单明细模板表(HQL)", Desc = "查询申请明细及订单明细模板表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaChooseGoodsTemplateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaChooseGoodsTemplate>", ReturnType = "ListReaChooseGoodsTemplate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaChooseGoodsTemplateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaChooseGoodsTemplateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询申请明细及订单明细模板表", Desc = "通过主键ID查询申请明细及订单明细模板表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaChooseGoodsTemplateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaChooseGoodsTemplate>", ReturnType = "ReaChooseGoodsTemplate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaChooseGoodsTemplateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaChooseGoodsTemplateById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsCenOrderDoc

        [ServiceContractDescription(Name = "查询订货总单(HQL)", Desc = "查询订货总单(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenOrderDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCenOrderDoc>", ReturnType = "ListBmsCenOrderDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenOrderDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenOrderDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询订单总单表", Desc = "通过主键ID查询订单总单表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenOrderDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ReaBmsQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenOrderDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenOrderDocById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "修改订货总单指定的属性", Desc = "修改订货总单指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenOrderDocByField", Get = "", Post = "ReaBmsCenOrderDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCenOrderDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCenOrderDocByField(ReaBmsCenOrderDoc entity, string fields);

        [ServiceContractDescription(Name = "删除订货总单", Desc = "删除订货总单", Url = "ReaSysManageService.svc/ST_UDTO_DelBmsCenOrderDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsCenOrderDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsCenOrderDoc(long id);
        #endregion

        #region ReaBmsCenOrderDtl

        [ServiceContractDescription(Name = "通过主键ID查询订单明细表", Desc = "通过主键ID查询订单明细表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenOrderDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ReaBmsQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenOrderDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenOrderDtlById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询订单明细(HQL)", Desc = "查询订单明细单(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenOrderDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCenOrderDoc>", ReturnType = "ListBmsCenOrderDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenOrderDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenOrderDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);
        #endregion

        #region ReaBmsCenSaleDoc

        [ServiceContractDescription(Name = "新增供货总单", Desc = "新增供货总单", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsCenSaleDoc", Get = "", Post = "ReaBmsCenSaleDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsCenSaleDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsCenSaleDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsCenSaleDoc(ReaBmsCenSaleDoc entity);

        [ServiceContractDescription(Name = "修改供货总单", Desc = "修改供货总单", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenSaleDoc", Get = "", Post = "ReaBmsCenSaleDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCenSaleDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDoc(ReaBmsCenSaleDoc entity);

        [ServiceContractDescription(Name = "修改供货总单指定的属性", Desc = "修改供货总单指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenSaleDocByField", Get = "", Post = "ReaBmsCenSaleDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCenSaleDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDocByField(ReaBmsCenSaleDoc entity, string fields);

        [ServiceContractDescription(Name = "删除供货总单", Desc = "删除供货总单", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsCenSaleDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsCenSaleDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsCenSaleDoc(long id);

        [ServiceContractDescription(Name = "查询供货总单", Desc = "查询供货总单", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDoc", Get = "", Post = "ReaBmsCenSaleDoc", Return = "BaseResultList<ReaBmsCenSaleDoc>", ReturnType = "ListReaBmsCenSaleDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenSaleDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDoc(ReaBmsCenSaleDoc entity);

        [ServiceContractDescription(Name = "查询供货总单(HQL)", Desc = "查询供货总单(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDoc>", ReturnType = "ListReaBmsCenSaleDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenSaleDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询供货总单", Desc = "通过主键ID查询供货总单", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDoc>", ReturnType = "ReaBmsCenSaleDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenSaleDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDocById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsCenSaleDtl

        [ServiceContractDescription(Name = "新增供货明细", Desc = "新增供货明细", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsCenSaleDtl", Get = "", Post = "ReaBmsCenSaleDtl", Return = "BaseResultDataValue", ReturnType = "ReaBmsCenSaleDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsCenSaleDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsCenSaleDtl(ReaBmsCenSaleDtl entity);

        [ServiceContractDescription(Name = "修改供货明细", Desc = "修改供货明细", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenSaleDtl", Get = "", Post = "ReaBmsCenSaleDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCenSaleDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDtl(ReaBmsCenSaleDtl entity);

        [ServiceContractDescription(Name = "修改供货明细指定的属性", Desc = "修改供货明细指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenSaleDtlByField", Get = "", Post = "ReaBmsCenSaleDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCenSaleDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDtlByField(ReaBmsCenSaleDtl entity, string fields);

        [ServiceContractDescription(Name = "删除供货明细", Desc = "删除供货明细", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsCenSaleDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsCenSaleDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsCenSaleDtl(long id);

        [ServiceContractDescription(Name = "查询供货明细", Desc = "查询供货明细", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtl", Get = "", Post = "ReaBmsCenSaleDtl", Return = "BaseResultList<ReaBmsCenSaleDtl>", ReturnType = "ListReaBmsCenSaleDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenSaleDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtl(ReaBmsCenSaleDtl entity);

        [ServiceContractDescription(Name = "查询供货明细(HQL)", Desc = "查询供货明细(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDtl>", ReturnType = "ListReaBmsCenSaleDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenSaleDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询供货明细", Desc = "通过主键ID查询供货明细", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDtl>", ReturnType = "ReaBmsCenSaleDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenSaleDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsCenSaleDocConfirm
        [ServiceContractDescription(Name = "新增供货验收单表", Desc = "新增供货验收单表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsCenSaleDocConfirm", Get = "", Post = "ReaBmsCenSaleDocConfirm", Return = "BaseResultDataValue", ReturnType = "ReaBmsCenSaleDocConfirm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsCenSaleDocConfirm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsCenSaleDocConfirm(ReaBmsCenSaleDocConfirm entity);

        [ServiceContractDescription(Name = "删除供货验收单表", Desc = "删除供货验收单表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsCenSaleDocConfirm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsCenSaleDocConfirm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsCenSaleDocConfirm(long id);

        [ServiceContractDescription(Name = "修改供货验收单表指定的属性", Desc = "修改供货验收单表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenSaleDocConfirmByField", Get = "", Post = "ReaBmsCenSaleDocConfirm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCenSaleDocConfirmByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDocConfirmByField(ReaBmsCenSaleDocConfirm entity, string fields);

        [ServiceContractDescription(Name = "查询供货验收单表(HQL)", Desc = "查询供货验收单表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocConfirmByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDocConfirm>", ReturnType = "ListBmsCenSaleDocConfirm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenSaleDocConfirmByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDocConfirmByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询供货验收单表", Desc = "通过主键ID查询供货验收单表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDocConfirmById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDocConfirm>", ReturnType = "BmsCenSaleDocConfirm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenSaleDocConfirmById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDocConfirmById(long id, string fields, bool isPlanish);

        #endregion

        #region ReaBmsCenSaleDtlConfirm
        [ServiceContractDescription(Name = "修改供货验收明细单表指定的属性", Desc = "修改供货验收明细单表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenSaleDtlConfirmByField", Get = "", Post = "ReaBmsCenSaleDtlConfirm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCenSaleDtlConfirmByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDtlConfirmByField(ReaBmsCenSaleDtlConfirm entity, string fields);

        [ServiceContractDescription(Name = "查询供货验收明细单表(HQL)", Desc = "查询供货验收明细单表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlConfirmByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDtlConfirm>", ReturnType = "ListBmsCenSaleDtlConfirm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenSaleDtlConfirmByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlConfirmByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询供货验收明细单表", Desc = "通过主键ID查询供货验收明细单表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlConfirmById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDtlConfirm>", ReturnType = "BmsCenSaleDtlConfirm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenSaleDtlConfirmById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlConfirmById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaEquipReagentLink

        [ServiceContractDescription(Name = "新增Rea_EquipReagentLink", Desc = "新增Rea_EquipReagentLink", Url = "ReaSysManageService.svc/ST_UDTO_AddReaEquipReagentLink", Get = "", Post = "ReaEquipReagentLink", Return = "BaseResultDataValue", ReturnType = "ReaEquipReagentLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaEquipReagentLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaEquipReagentLink(ReaEquipReagentLink entity);

        [ServiceContractDescription(Name = "修改Rea_EquipReagentLink", Desc = "修改Rea_EquipReagentLink", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaEquipReagentLink", Get = "", Post = "ReaEquipReagentLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaEquipReagentLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaEquipReagentLink(ReaEquipReagentLink entity);

        [ServiceContractDescription(Name = "修改Rea_EquipReagentLink指定的属性", Desc = "修改Rea_EquipReagentLink指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaEquipReagentLinkByField", Get = "", Post = "ReaEquipReagentLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaEquipReagentLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaEquipReagentLinkByField(ReaEquipReagentLink entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_EquipReagentLink", Desc = "删除Rea_EquipReagentLink", Url = "ReaSysManageService.svc/ST_UDTO_DelReaEquipReagentLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaEquipReagentLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaEquipReagentLink(long id);

        [ServiceContractDescription(Name = "查询Rea_EquipReagentLink", Desc = "查询Rea_EquipReagentLink", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaEquipReagentLink", Get = "", Post = "ReaEquipReagentLink", Return = "BaseResultList<ReaEquipReagentLink>", ReturnType = "ListReaEquipReagentLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaEquipReagentLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaEquipReagentLink(ReaEquipReagentLink entity);

        [ServiceContractDescription(Name = "查询Rea_EquipReagentLink(HQL)", Desc = "查询Rea_EquipReagentLink(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaEquipReagentLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaEquipReagentLink>", ReturnType = "ListReaEquipReagentLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaEquipReagentLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaEquipReagentLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_EquipReagentLink", Desc = "通过主键ID查询Rea_EquipReagentLink", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaEquipReagentLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaEquipReagentLink>", ReturnType = "ReaEquipReagentLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaEquipReagentLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaEquipReagentLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaTestEquipLab

        [ServiceContractDescription(Name = "新增ReaTestEquipLab", Desc = "新增ReaTestEquipLab", Url = "ReaSysManageService.svc/ST_UDTO_AddReaTestEquipLab", Get = "", Post = "ReaTestEquipLab", Return = "BaseResultDataValue", ReturnType = "ReaTestEquipLab")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaTestEquipLab", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaTestEquipLab(ReaTestEquipLab entity);

        [ServiceContractDescription(Name = "修改ReaTestEquipLab", Desc = "修改ReaTestEquipLab", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaTestEquipLab", Get = "", Post = "ReaTestEquipLab", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaTestEquipLab", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaTestEquipLab(ReaTestEquipLab entity);

        [ServiceContractDescription(Name = "修改ReaTestEquipLab指定的属性", Desc = "修改ReaTestEquipLab指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaTestEquipLabByField", Get = "", Post = "ReaTestEquipLab", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaTestEquipLabByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaTestEquipLabByField(ReaTestEquipLab entity, string fields);

        [ServiceContractDescription(Name = "删除ReaTestEquipLab", Desc = "删除ReaTestEquipLab", Url = "ReaSysManageService.svc/ST_UDTO_DelReaTestEquipLab?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaTestEquipLab?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaTestEquipLab(long id);

        [ServiceContractDescription(Name = "查询ReaTestEquipLab", Desc = "查询ReaTestEquipLab", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipLab", Get = "", Post = "ReaTestEquipLab", Return = "BaseResultList<ReaTestEquipLab>", ReturnType = "ListReaTestEquipLab")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestEquipLab", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestEquipLab(ReaTestEquipLab entity);

        [ServiceContractDescription(Name = "查询ReaTestEquipLab(HQL)", Desc = "查询ReaTestEquipLab(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipLabByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaTestEquipLab>", ReturnType = "ListReaTestEquipLab")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestEquipLabByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestEquipLabByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询ReaTestEquipLab", Desc = "通过主键ID查询ReaTestEquipLab", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipLabById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaTestEquipLab>", ReturnType = "ReaTestEquipLab")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestEquipLabById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestEquipLabById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaTestEquipProd

        [ServiceContractDescription(Name = "新增ReaTestEquipProd", Desc = "新增ReaTestEquipProd", Url = "ReaSysManageService.svc/ST_UDTO_AddReaTestEquipProd", Get = "", Post = "ReaTestEquipProd", Return = "BaseResultDataValue", ReturnType = "ReaTestEquipProd")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaTestEquipProd", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaTestEquipProd(ReaTestEquipProd entity);

        [ServiceContractDescription(Name = "修改ReaTestEquipProd", Desc = "修改ReaTestEquipProd", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaTestEquipProd", Get = "", Post = "ReaTestEquipProd", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaTestEquipProd", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaTestEquipProd(ReaTestEquipProd entity);

        [ServiceContractDescription(Name = "修改ReaTestEquipProd指定的属性", Desc = "修改ReaTestEquipProd指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaTestEquipProdByField", Get = "", Post = "ReaTestEquipProd", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaTestEquipProdByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaTestEquipProdByField(ReaTestEquipProd entity, string fields);

        [ServiceContractDescription(Name = "删除ReaTestEquipProd", Desc = "删除ReaTestEquipProd", Url = "ReaSysManageService.svc/ST_UDTO_DelReaTestEquipProd?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaTestEquipProd?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaTestEquipProd(long id);

        [ServiceContractDescription(Name = "查询ReaTestEquipProd", Desc = "查询ReaTestEquipProd", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipProd", Get = "", Post = "ReaTestEquipProd", Return = "BaseResultList<ReaTestEquipProd>", ReturnType = "ListReaTestEquipProd")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestEquipProd", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestEquipProd(ReaTestEquipProd entity);

        [ServiceContractDescription(Name = "查询ReaTestEquipProd(HQL)", Desc = "查询ReaTestEquipProd(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipProdByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaTestEquipProd>", ReturnType = "ListReaTestEquipProd")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestEquipProdByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestEquipProdByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询ReaTestEquipProd", Desc = "通过主键ID查询ReaTestEquipProd", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipProdById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaTestEquipProd>", ReturnType = "ReaTestEquipProd")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestEquipProdById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestEquipProdById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaTestEquipType

        [ServiceContractDescription(Name = "新增ReaTestEquipType", Desc = "新增ReaTestEquipType", Url = "ReaSysManageService.svc/ST_UDTO_AddReaTestEquipType", Get = "", Post = "ReaTestEquipType", Return = "BaseResultDataValue", ReturnType = "ReaTestEquipType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaTestEquipType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaTestEquipType(ReaTestEquipType entity);

        [ServiceContractDescription(Name = "修改ReaTestEquipType", Desc = "修改ReaTestEquipType", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaTestEquipType", Get = "", Post = "ReaTestEquipType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaTestEquipType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaTestEquipType(ReaTestEquipType entity);

        [ServiceContractDescription(Name = "修改ReaTestEquipType指定的属性", Desc = "修改ReaTestEquipType指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaTestEquipTypeByField", Get = "", Post = "ReaTestEquipType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaTestEquipTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaTestEquipTypeByField(ReaTestEquipType entity, string fields);

        [ServiceContractDescription(Name = "删除ReaTestEquipType", Desc = "删除ReaTestEquipType", Url = "ReaSysManageService.svc/ST_UDTO_DelReaTestEquipType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaTestEquipType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaTestEquipType(long id);

        [ServiceContractDescription(Name = "查询ReaTestEquipType", Desc = "查询ReaTestEquipType", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipType", Get = "", Post = "ReaTestEquipType", Return = "BaseResultList<ReaTestEquipType>", ReturnType = "ListReaTestEquipType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestEquipType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestEquipType(ReaTestEquipType entity);

        [ServiceContractDescription(Name = "查询ReaTestEquipType(HQL)", Desc = "查询ReaTestEquipType(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaTestEquipType>", ReturnType = "ListReaTestEquipType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestEquipTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestEquipTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询ReaTestEquipType", Desc = "通过主键ID查询ReaTestEquipType", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaTestEquipType>", ReturnType = "ReaTestEquipType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestEquipTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestEquipTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsQtyDtlOperation

        [ServiceContractDescription(Name = "新增库存变化操作记录操作记录表", Desc = "新增库存变化操作记录操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsQtyDtlOperation", Get = "", Post = "ReaBmsQtyDtlOperation", Return = "BaseResultDataValue", ReturnType = "ReaBmsQtyDtlOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsQtyDtlOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsQtyDtlOperation(ReaBmsQtyDtlOperation entity);

        [ServiceContractDescription(Name = "修改库存变化操作记录操作记录表", Desc = "修改库存变化操作记录操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsQtyDtlOperation", Get = "", Post = "ReaBmsQtyDtlOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsQtyDtlOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsQtyDtlOperation(ReaBmsQtyDtlOperation entity);

        [ServiceContractDescription(Name = "修改库存变化操作记录操作记录表指定的属性", Desc = "修改库存变化操作记录操作记录表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsQtyDtlOperationByField", Get = "", Post = "ReaBmsQtyDtlOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsQtyDtlOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsQtyDtlOperationByField(ReaBmsQtyDtlOperation entity, string fields);

        [ServiceContractDescription(Name = "删除库存变化操作记录操作记录表", Desc = "删除库存变化操作记录操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsQtyDtlOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsQtyDtlOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsQtyDtlOperation(long id);

        [ServiceContractDescription(Name = "查询库存变化操作记录操作记录表", Desc = "查询库存变化操作记录操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyDtlOperation", Get = "", Post = "ReaBmsQtyDtlOperation", Return = "BaseResultList<ReaBmsQtyDtlOperation>", ReturnType = "ListReaBmsQtyDtlOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyDtlOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlOperation(ReaBmsQtyDtlOperation entity);

        [ServiceContractDescription(Name = "查询库存变化操作记录操作记录表(HQL)", Desc = "查询库存变化操作记录操作记录表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyDtlOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyDtlOperation>", ReturnType = "ListReaBmsQtyDtlOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyDtlOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询库存变化操作记录操作记录表", Desc = "通过主键ID查询库存变化操作记录操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyDtlOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyDtlOperation>", ReturnType = "ReaBmsQtyDtlOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyDtlOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region 客户端入库

        [ServiceContractDescription(Name = "(验收入库)获取客户端验收明细及验收条码明细信息(HQL)", Desc = "(验收入库)获取客户端验收明细及验收条码明细信息(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaDtlConfirmVOOfStoreInByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isNoPrefEntity={isNoPrefEntity}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isNoPrefEntity={isNoPrefEntity}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDtlConfirmVO>", ReturnType = "ListReaBmsCenSaleDtlConfirmVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaDtlConfirmVOOfStoreInByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isNoPrefEntity={isNoPrefEntity}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaDtlConfirmVOOfStoreInByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isNoPrefEntity);

        [ServiceContractDescription(Name = "客户端入库货品扫码", Desc = "客户端入库货品扫码", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsScanCodeVOOfReaBmsInByCompIDAndSerialNo?reaCompID={reaCompID}&serialNo={serialNo}&docConfirmID={docConfirmID}", Get = "?reaCompID={reaCompID}&serialNo={serialNo}&docConfirmID={docConfirmID}", Post = "", Return = "BaseResultList<ReaGoodsScanCodeVO>", ReturnType = "ReaGoodsScanCodeVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsScanCodeVOOfReaBmsInByCompIDAndSerialNo?reaCompID={reaCompID}&serialNo={serialNo}&docConfirmID={docConfirmID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsScanCodeVOOfReaBmsInByCompIDAndSerialNo(long reaCompID, string serialNo, long docConfirmID);

        [ServiceContractDescription(Name = "验货入库,新增客户端入库及入库明细", Desc = "验货入库,新增客户端入库及入库明细", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsInDocAndDtl", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsInDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsInDocAndDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsInDocAndDtl(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> dtAddList, long docConfirmID, string dtlDocConfirmIDStr, string codeScanningMode);

        [ServiceContractDescription(Name = "手工入库,新增客户端入库及入库明细", Desc = "手工入库,新增客户端入库及入库明细", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsInDocAndDtlOfManualInput", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsInDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsInDocAndDtlOfManualInput", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsInDocAndDtlOfManualInput(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode);

        [ServiceContractDescription(Name = "手工入库,编辑客户端入库及入库明细", Desc = "手工入库,编辑客户端入库及入库明细", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDocAndDtlOfManualInput", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultBool", ReturnType = "ReaBmsInDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsInDocAndDtlOfManualInput", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsInDocAndDtlOfManualInput(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> dtAddList, IList<ReaBmsInDtlVO> dtEditList, string fieldsDtl, string codeScanningMode, string fields);

        [ServiceContractDescription(Name = "手工入库,确认入库", Desc = "手工入库,确认入库", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDocOfConfirmStock?id={id}&codeScanningMode={codeScanningMode}", Get = "id={id}&codeScanningMode={codeScanningMode}", Post = "", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsInDocOfConfirmStock?id={id}&codeScanningMode={codeScanningMode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsInDocOfConfirmStock(long id, string codeScanningMode);

        [ServiceContractDescription(Name = "手工入库,获取客户端入库明细及入库条码操作明细信息", Desc = "手工入库,获取客户端入库明细及入库条码操作明细信息", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDtlVOByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ListReaBmsQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDtlVOByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDtlVOByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        #endregion

        //[ServiceContractDescription(Name = "", Desc = "", Url = "ReaSysManageService.svc/RS_UDTO_GetReaGoodsInfoByBarCode?reaCompID={reaCompID}&barcode={barcode}&fields={fields}&isPlanish={isPlanish}", Get = "?reaCompID={reaCompID}&barcode={barcode}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsScanCodeVO>", ReturnType = "ReaGoodsScanCodeVO")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_GetReaGoodsInfoByBarCode?reaCompID={reaCompID}&barcode={barcode}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue RS_UDTO_GetReaGoodsInfoByBarCode(long reaCompID, string barcode, string fields, bool isPlanish);

        #region ReaBmsCheckDoc

        [ServiceContractDescription(Name = "修改Rea_BmsCheckDoc", Desc = "修改Rea_BmsCheckDoc", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCheckDoc", Get = "", Post = "ReaBmsCheckDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCheckDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCheckDoc(ReaBmsCheckDoc entity);

        [ServiceContractDescription(Name = "修改Rea_BmsCheckDoc指定的属性", Desc = "修改Rea_BmsCheckDoc指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCheckDocByField", Get = "", Post = "ReaBmsCheckDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCheckDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCheckDocByField(ReaBmsCheckDoc entity, string fields);

        [ServiceContractDescription(Name = "查询Rea_BmsCheckDoc", Desc = "查询Rea_BmsCheckDoc", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCheckDoc", Get = "", Post = "ReaBmsCheckDoc", Return = "BaseResultList<ReaBmsCheckDoc>", ReturnType = "ListReaBmsCheckDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCheckDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCheckDoc(ReaBmsCheckDoc entity);

        [ServiceContractDescription(Name = "查询Rea_BmsCheckDoc(HQL)", Desc = "查询Rea_BmsCheckDoc(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCheckDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCheckDoc>", ReturnType = "ListReaBmsCheckDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCheckDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCheckDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_BmsCheckDoc", Desc = "通过主键ID查询Rea_BmsCheckDoc", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCheckDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCheckDoc>", ReturnType = "ReaBmsCheckDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCheckDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCheckDocById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "取消盘库,物理删除盘库总单及盘库明细信息", Desc = "取消盘库,物理删除盘库总单及盘库明细信息", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsCheckDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsCheckDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsCheckDoc(long id);

        #endregion

        #region ReaBmsCheckDtl

        [ServiceContractDescription(Name = "新增Rea_BmsCheckDtl", Desc = "新增Rea_BmsCheckDtl", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsCheckDtl", Get = "", Post = "ReaBmsCheckDtl", Return = "BaseResultDataValue", ReturnType = "ReaBmsCheckDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsCheckDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsCheckDtl(ReaBmsCheckDtl entity);

        [ServiceContractDescription(Name = "修改Rea_BmsCheckDtl", Desc = "修改Rea_BmsCheckDtl", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCheckDtl", Get = "", Post = "ReaBmsCheckDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCheckDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCheckDtl(ReaBmsCheckDtl entity);

        [ServiceContractDescription(Name = "修改Rea_BmsCheckDtl指定的属性", Desc = "修改Rea_BmsCheckDtl指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCheckDtlByField", Get = "", Post = "ReaBmsCheckDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCheckDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCheckDtlByField(ReaBmsCheckDtl entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_BmsCheckDtl", Desc = "删除Rea_BmsCheckDtl", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsCheckDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsCheckDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsCheckDtl(long id);

        [ServiceContractDescription(Name = "查询Rea_BmsCheckDtl", Desc = "查询Rea_BmsCheckDtl", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCheckDtl", Get = "", Post = "ReaBmsCheckDtl", Return = "BaseResultList<ReaBmsCheckDtl>", ReturnType = "ListReaBmsCheckDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCheckDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCheckDtl(ReaBmsCheckDtl entity);

        [ServiceContractDescription(Name = "查询Rea_BmsCheckDtl(HQL)", Desc = "查询Rea_BmsCheckDtl(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCheckDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCheckDtl>", ReturnType = "ListReaBmsCheckDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCheckDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCheckDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_BmsCheckDtl", Desc = "通过主键ID查询Rea_BmsCheckDtl", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsCheckDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCheckDtl>", ReturnType = "ReaBmsCheckDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCheckDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCheckDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsQtyBalanceDoc

        [ServiceContractDescription(Name = "新增Rea_BmsQtyBalanceDoc", Desc = "新增Rea_BmsQtyBalanceDoc", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsQtyBalanceDoc", Get = "", Post = "ReaBmsQtyBalanceDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsQtyBalanceDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsQtyBalanceDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsQtyBalanceDoc(ReaBmsQtyBalanceDoc entity);

        [ServiceContractDescription(Name = "修改Rea_BmsQtyBalanceDoc", Desc = "修改Rea_BmsQtyBalanceDoc", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsQtyBalanceDoc", Get = "", Post = "ReaBmsQtyBalanceDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsQtyBalanceDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsQtyBalanceDoc(ReaBmsQtyBalanceDoc entity);

        [ServiceContractDescription(Name = "修改Rea_BmsQtyBalanceDoc指定的属性", Desc = "修改Rea_BmsQtyBalanceDoc指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsQtyBalanceDocByField", Get = "", Post = "ReaBmsQtyBalanceDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsQtyBalanceDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsQtyBalanceDocByField(ReaBmsQtyBalanceDoc entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_BmsQtyBalanceDoc", Desc = "删除Rea_BmsQtyBalanceDoc", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsQtyBalanceDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsQtyBalanceDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsQtyBalanceDoc(long id);

        [ServiceContractDescription(Name = "查询Rea_BmsQtyBalanceDoc", Desc = "查询Rea_BmsQtyBalanceDoc", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyBalanceDoc", Get = "", Post = "ReaBmsQtyBalanceDoc", Return = "BaseResultList<ReaBmsQtyBalanceDoc>", ReturnType = "ListReaBmsQtyBalanceDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyBalanceDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyBalanceDoc(ReaBmsQtyBalanceDoc entity);

        [ServiceContractDescription(Name = "查询Rea_BmsQtyBalanceDoc(HQL)", Desc = "查询Rea_BmsQtyBalanceDoc(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyBalanceDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyBalanceDoc>", ReturnType = "ListReaBmsQtyBalanceDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyBalanceDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyBalanceDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_BmsQtyBalanceDoc", Desc = "通过主键ID查询Rea_BmsQtyBalanceDoc", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyBalanceDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyBalanceDoc>", ReturnType = "ReaBmsQtyBalanceDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyBalanceDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyBalanceDocById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsQtyBalanceDtl

        [ServiceContractDescription(Name = "新增Rea_BmsQtyBalanceDtl", Desc = "新增Rea_BmsQtyBalanceDtl", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsQtyBalanceDtl", Get = "", Post = "ReaBmsQtyBalanceDtl", Return = "BaseResultDataValue", ReturnType = "ReaBmsQtyBalanceDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsQtyBalanceDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsQtyBalanceDtl(ReaBmsQtyBalanceDtl entity);

        [ServiceContractDescription(Name = "修改Rea_BmsQtyBalanceDtl", Desc = "修改Rea_BmsQtyBalanceDtl", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsQtyBalanceDtl", Get = "", Post = "ReaBmsQtyBalanceDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsQtyBalanceDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsQtyBalanceDtl(ReaBmsQtyBalanceDtl entity);

        [ServiceContractDescription(Name = "修改Rea_BmsQtyBalanceDtl指定的属性", Desc = "修改Rea_BmsQtyBalanceDtl指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsQtyBalanceDtlByField", Get = "", Post = "ReaBmsQtyBalanceDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsQtyBalanceDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsQtyBalanceDtlByField(ReaBmsQtyBalanceDtl entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_BmsQtyBalanceDtl", Desc = "删除Rea_BmsQtyBalanceDtl", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsQtyBalanceDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsQtyBalanceDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsQtyBalanceDtl(long id);

        [ServiceContractDescription(Name = "查询Rea_BmsQtyBalanceDtl", Desc = "查询Rea_BmsQtyBalanceDtl", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyBalanceDtl", Get = "", Post = "ReaBmsQtyBalanceDtl", Return = "BaseResultList<ReaBmsQtyBalanceDtl>", ReturnType = "ListReaBmsQtyBalanceDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyBalanceDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyBalanceDtl(ReaBmsQtyBalanceDtl entity);

        [ServiceContractDescription(Name = "查询Rea_BmsQtyBalanceDtl(HQL)", Desc = "查询Rea_BmsQtyBalanceDtl(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyBalanceDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyBalanceDtl>", ReturnType = "ListReaBmsQtyBalanceDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyBalanceDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyBalanceDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_BmsQtyBalanceDtl", Desc = "通过主键ID查询Rea_BmsQtyBalanceDtl", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyBalanceDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyBalanceDtl>", ReturnType = "ReaBmsQtyBalanceDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyBalanceDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyBalanceDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsQtyMonthBalanceDoc

        [ServiceContractDescription(Name = "新增Rea_BmsQtyMonthBalanceDoc", Desc = "新增Rea_BmsQtyMonthBalanceDoc", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsQtyMonthBalanceDoc", Get = "", Post = "ReaBmsQtyMonthBalanceDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsQtyMonthBalanceDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsQtyMonthBalanceDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsQtyMonthBalanceDoc(ReaBmsQtyMonthBalanceDoc entity);

        [ServiceContractDescription(Name = "修改Rea_BmsQtyMonthBalanceDoc", Desc = "修改Rea_BmsQtyMonthBalanceDoc", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsQtyMonthBalanceDoc", Get = "", Post = "ReaBmsQtyMonthBalanceDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsQtyMonthBalanceDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsQtyMonthBalanceDoc(ReaBmsQtyMonthBalanceDoc entity);

        [ServiceContractDescription(Name = "修改Rea_BmsQtyMonthBalanceDoc指定的属性", Desc = "修改Rea_BmsQtyMonthBalanceDoc指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsQtyMonthBalanceDocByField", Get = "", Post = "ReaBmsQtyMonthBalanceDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsQtyMonthBalanceDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsQtyMonthBalanceDocByField(ReaBmsQtyMonthBalanceDoc entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_BmsQtyMonthBalanceDoc", Desc = "删除Rea_BmsQtyMonthBalanceDoc", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsQtyMonthBalanceDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsQtyMonthBalanceDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsQtyMonthBalanceDoc(long id);

        [ServiceContractDescription(Name = "查询Rea_BmsQtyMonthBalanceDoc", Desc = "查询Rea_BmsQtyMonthBalanceDoc", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyMonthBalanceDoc", Get = "", Post = "ReaBmsQtyMonthBalanceDoc", Return = "BaseResultList<ReaBmsQtyMonthBalanceDoc>", ReturnType = "ListReaBmsQtyMonthBalanceDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyMonthBalanceDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyMonthBalanceDoc(ReaBmsQtyMonthBalanceDoc entity);

        [ServiceContractDescription(Name = "查询Rea_BmsQtyMonthBalanceDoc(HQL)", Desc = "查询Rea_BmsQtyMonthBalanceDoc(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyMonthBalanceDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyMonthBalanceDoc>", ReturnType = "ListReaBmsQtyMonthBalanceDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyMonthBalanceDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyMonthBalanceDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_BmsQtyMonthBalanceDoc", Desc = "通过主键ID查询Rea_BmsQtyMonthBalanceDoc", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyMonthBalanceDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyMonthBalanceDoc>", ReturnType = "ReaBmsQtyMonthBalanceDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyMonthBalanceDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyMonthBalanceDocById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaUserStorageLink

        [ServiceContractDescription(Name = "新增Rea_User_Storage_Link", Desc = "新增Rea_User_Storage_Link", Url = "ReaSysManageService.svc/ST_UDTO_AddReaUserStorageLink", Get = "", Post = "ReaUserStorageLink", Return = "BaseResultDataValue", ReturnType = "ReaUserStorageLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaUserStorageLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaUserStorageLink(ReaUserStorageLink entity);

        [ServiceContractDescription(Name = "修改Rea_User_Storage_Link", Desc = "修改Rea_User_Storage_Link", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaUserStorageLink", Get = "", Post = "ReaUserStorageLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaUserStorageLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaUserStorageLink(ReaUserStorageLink entity);

        [ServiceContractDescription(Name = "修改Rea_User_Storage_Link指定的属性", Desc = "修改Rea_User_Storage_Link指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaUserStorageLinkByField", Get = "", Post = "ReaUserStorageLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaUserStorageLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaUserStorageLinkByField(ReaUserStorageLink entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_User_Storage_Link", Desc = "删除Rea_User_Storage_Link", Url = "ReaSysManageService.svc/ST_UDTO_DelReaUserStorageLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaUserStorageLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaUserStorageLink(long id);

        [ServiceContractDescription(Name = "查询Rea_User_Storage_Link", Desc = "查询Rea_User_Storage_Link", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaUserStorageLink", Get = "", Post = "ReaUserStorageLink", Return = "BaseResultList<ReaUserStorageLink>", ReturnType = "ListReaUserStorageLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaUserStorageLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaUserStorageLink(ReaUserStorageLink entity);

        [ServiceContractDescription(Name = "查询Rea_User_Storage_Link(HQL)", Desc = "查询Rea_User_Storage_Link(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaUserStorageLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaUserStorageLink>", ReturnType = "ListReaUserStorageLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaUserStorageLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaUserStorageLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_User_Storage_Link", Desc = "通过主键ID查询Rea_User_Storage_Link", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaUserStorageLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaUserStorageLink>", ReturnType = "ReaUserStorageLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaUserStorageLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaUserStorageLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region BReport

        [ServiceContractDescription(Name = "新增B_Report", Desc = "新增B_Report", Url = "ReaSysManageService.svc/ST_UDTO_AddBReport", Get = "", Post = "BReport", Return = "BaseResultDataValue", ReturnType = "BReport")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBReport", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBReport(BReport entity);

        [ServiceContractDescription(Name = "修改B_Report", Desc = "修改B_Report", Url = "ReaSysManageService.svc/ST_UDTO_UpdateBReport", Get = "", Post = "BReport", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBReport", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBReport(BReport entity);

        [ServiceContractDescription(Name = "修改B_Report指定的属性", Desc = "修改B_Report指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateBReportByField", Get = "", Post = "BReport", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBReportByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBReportByField(BReport entity, string fields);

        [ServiceContractDescription(Name = "删除B_Report", Desc = "删除B_Report", Url = "ReaSysManageService.svc/ST_UDTO_DelBReport?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBReport?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBReport(long id);

        [ServiceContractDescription(Name = "查询B_Report", Desc = "查询B_Report", Url = "ReaSysManageService.svc/ST_UDTO_SearchBReport", Get = "", Post = "BReport", Return = "BaseResultList<BReport>", ReturnType = "ListBReport")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReport", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReport(BReport entity);

        [ServiceContractDescription(Name = "查询B_Report(HQL)", Desc = "查询B_Report(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchBReportByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReport>", ReturnType = "ListBReport")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReportByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReportByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_Report", Desc = "通过主键ID查询B_Report", Url = "ReaSysManageService.svc/ST_UDTO_SearchBReportById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReport>", ReturnType = "BReport")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReportById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReportById(long id, string fields, bool isPlanish);
        #endregion

        #region BTemplate

        [ServiceContractDescription(Name = "新增B_Template", Desc = "新增B_Template", Url = "ReaSysManageService.svc/ST_UDTO_AddBTemplate", Get = "", Post = "BTemplate", Return = "BaseResultDataValue", ReturnType = "BTemplate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBTemplate(BTemplate entity);

        [ServiceContractDescription(Name = "修改B_Template", Desc = "修改B_Template", Url = "ReaSysManageService.svc/ST_UDTO_UpdateBTemplate", Get = "", Post = "BTemplate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBTemplate(BTemplate entity);

        [ServiceContractDescription(Name = "修改B_Template指定的属性", Desc = "修改B_Template指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateBTemplateByField", Get = "", Post = "BTemplate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBTemplateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBTemplateByField(BTemplate entity, string fields);

        [ServiceContractDescription(Name = "删除B_Template", Desc = "删除B_Template", Url = "ReaSysManageService.svc/ST_UDTO_DelBTemplate?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBTemplate?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBTemplate(long id);

        [ServiceContractDescription(Name = "查询B_Template", Desc = "查询B_Template", Url = "ReaSysManageService.svc/ST_UDTO_SearchBTemplate", Get = "", Post = "BTemplate", Return = "BaseResultList<BTemplate>", ReturnType = "ListBTemplate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTemplate(BTemplate entity);

        [ServiceContractDescription(Name = "查询B_Template(HQL)", Desc = "查询B_Template(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchBTemplateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTemplate>", ReturnType = "ListBTemplate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTemplateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTemplateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_Template", Desc = "通过主键ID查询B_Template", Url = "ReaSysManageService.svc/ST_UDTO_SearchBTemplateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTemplate>", ReturnType = "BTemplate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTemplateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTemplateById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过FormData方式新增模板信息", Desc = "通过FormData方式新增模板信息", Url = "ReaSysManageService.svc/ST_UDTO_AddTemplateUploadFile", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddTemplateUploadFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_AddTemplateUploadFile();


        [ServiceContractDescription(Name = "通过FormData方式更新产品模板信息", Desc = "通过FormData方式更新模板信息", Url = "ReaSysManageService.svc/ST_UDTO_UpdateTemplateAndUploadFileByField", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateTemplateAndUploadFileByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_UpdateTemplateAndUploadFileByField();


        [ServiceContractDescription(Name = "下载模板文件", Desc = "下载模板文件", Url = "ReaSysManageService.svc/ST_UDTO_DownLoadTemplateFrx?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_DownLoadTemplateFrx?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_DownLoadTemplateFrx(long id, long operateType);



        #endregion

        #region CenOrgType

        [ServiceContractDescription(Name = "新增机构类型表", Desc = "新增机构类型表", Url = "ReaSysManageService.svc/ST_UDTO_AddCenOrgType", Get = "", Post = "CenOrgType", Return = "BaseResultDataValue", ReturnType = "CenOrgType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddCenOrgType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddCenOrgType(CenOrgType entity);

        [ServiceContractDescription(Name = "修改机构类型表", Desc = "修改机构类型表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateCenOrgType", Get = "", Post = "CenOrgType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenOrgType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenOrgType(CenOrgType entity);

        [ServiceContractDescription(Name = "修改机构类型表指定的属性", Desc = "修改机构类型表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateCenOrgTypeByField", Get = "", Post = "CenOrgType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenOrgTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenOrgTypeByField(CenOrgType entity, string fields);

        [ServiceContractDescription(Name = "删除机构类型表", Desc = "删除机构类型表", Url = "ReaSysManageService.svc/ST_UDTO_DelCenOrgType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelCenOrgType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelCenOrgType(long id);

        [ServiceContractDescription(Name = "查询机构类型表", Desc = "查询机构类型表", Url = "ReaSysManageService.svc/ST_UDTO_SearchCenOrgType", Get = "", Post = "CenOrgType", Return = "BaseResultList<CenOrgType>", ReturnType = "ListCenOrgType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrgType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrgType(CenOrgType entity);

        [ServiceContractDescription(Name = "查询机构类型表(HQL)", Desc = "查询机构类型表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchCenOrgTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenOrgType>", ReturnType = "ListCenOrgType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrgTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrgTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询机构类型表", Desc = "通过主键ID查询机构类型表", Url = "ReaSysManageService.svc/ST_UDTO_SearchCenOrgTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenOrgType>", ReturnType = "CenOrgType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrgTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrgTypeById(long id, string fields, bool isPlanish);

        #endregion

        #region ReaBusinessInterface

        [ServiceContractDescription(Name = "新增Rea_BusinessInterface", Desc = "新增Rea_BusinessInterface", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBusinessInterface", Get = "", Post = "ReaBusinessInterface", Return = "BaseResultDataValue", ReturnType = "ReaBusinessInterface")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBusinessInterface", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBusinessInterface(ReaBusinessInterface entity);

        [ServiceContractDescription(Name = "修改Rea_BusinessInterface", Desc = "修改Rea_BusinessInterface", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBusinessInterface", Get = "", Post = "ReaBusinessInterface", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBusinessInterface", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBusinessInterface(ReaBusinessInterface entity);

        [ServiceContractDescription(Name = "修改Rea_BusinessInterface指定的属性", Desc = "修改Rea_BusinessInterface指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBusinessInterfaceByField", Get = "", Post = "ReaBusinessInterface", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBusinessInterfaceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBusinessInterfaceByField(ReaBusinessInterface entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_BusinessInterface", Desc = "删除Rea_BusinessInterface", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBusinessInterface?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBusinessInterface?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBusinessInterface(long id);

        [ServiceContractDescription(Name = "查询Rea_BusinessInterface", Desc = "查询Rea_BusinessInterface", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBusinessInterface", Get = "", Post = "ReaBusinessInterface", Return = "BaseResultList<ReaBusinessInterface>", ReturnType = "ListReaBusinessInterface")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBusinessInterface", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBusinessInterface(ReaBusinessInterface entity);

        [ServiceContractDescription(Name = "查询Rea_BusinessInterface(HQL)", Desc = "查询Rea_BusinessInterface(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBusinessInterfaceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBusinessInterface>", ReturnType = "ListReaBusinessInterface")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBusinessInterfaceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBusinessInterfaceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_BusinessInterface", Desc = "通过主键ID查询Rea_BusinessInterface", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBusinessInterfaceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBusinessInterface>", ReturnType = "ReaBusinessInterface")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBusinessInterfaceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBusinessInterfaceById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBusinessInterfaceLink

        [ServiceContractDescription(Name = "新增Rea_BusinessInterfaceLink", Desc = "新增Rea_BusinessInterfaceLink", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBusinessInterfaceLink", Get = "", Post = "ReaBusinessInterfaceLink", Return = "BaseResultDataValue", ReturnType = "ReaBusinessInterfaceLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBusinessInterfaceLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBusinessInterfaceLink(ReaBusinessInterfaceLink entity);

        [ServiceContractDescription(Name = "修改Rea_BusinessInterfaceLink", Desc = "修改Rea_BusinessInterfaceLink", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBusinessInterfaceLink", Get = "", Post = "ReaBusinessInterfaceLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBusinessInterfaceLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBusinessInterfaceLink(ReaBusinessInterfaceLink entity);

        [ServiceContractDescription(Name = "修改Rea_BusinessInterfaceLink指定的属性", Desc = "修改Rea_BusinessInterfaceLink指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBusinessInterfaceLinkByField", Get = "", Post = "ReaBusinessInterfaceLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBusinessInterfaceLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBusinessInterfaceLinkByField(ReaBusinessInterfaceLink entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_BusinessInterfaceLink", Desc = "删除Rea_BusinessInterfaceLink", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBusinessInterfaceLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBusinessInterfaceLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBusinessInterfaceLink(long id);

        [ServiceContractDescription(Name = "查询Rea_BusinessInterfaceLink", Desc = "查询Rea_BusinessInterfaceLink", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBusinessInterfaceLink", Get = "", Post = "ReaBusinessInterfaceLink", Return = "BaseResultList<ReaBusinessInterfaceLink>", ReturnType = "ListReaBusinessInterfaceLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBusinessInterfaceLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBusinessInterfaceLink(ReaBusinessInterfaceLink entity);

        [ServiceContractDescription(Name = "查询Rea_BusinessInterfaceLink(HQL)", Desc = "查询Rea_BusinessInterfaceLink(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBusinessInterfaceLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBusinessInterfaceLink>", ReturnType = "ListReaBusinessInterfaceLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBusinessInterfaceLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBusinessInterfaceLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_BusinessInterfaceLink", Desc = "通过主键ID查询Rea_BusinessInterfaceLink", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBusinessInterfaceLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBusinessInterfaceLink>", ReturnType = "ReaBusinessInterfaceLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBusinessInterfaceLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBusinessInterfaceLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaEquipTestItemReaGoodLink

        [ServiceContractDescription(Name = "新增Rea_EquipTestItemReaGoodLink", Desc = "新增Rea_EquipTestItemReaGoodLink", Url = "ReaSysManageService.svc/ST_UDTO_AddReaEquipTestItemReaGoodLink", Get = "", Post = "ReaEquipTestItemReaGoodLink", Return = "BaseResultDataValue", ReturnType = "ReaEquipTestItemReaGoodLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaEquipTestItemReaGoodLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaEquipTestItemReaGoodLink(ReaEquipTestItemReaGoodLink entity);

        [ServiceContractDescription(Name = "修改Rea_EquipTestItemReaGoodLink", Desc = "修改Rea_EquipTestItemReaGoodLink", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaEquipTestItemReaGoodLink", Get = "", Post = "ReaEquipTestItemReaGoodLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaEquipTestItemReaGoodLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaEquipTestItemReaGoodLink(ReaEquipTestItemReaGoodLink entity);

        [ServiceContractDescription(Name = "修改Rea_EquipTestItemReaGoodLink指定的属性", Desc = "修改Rea_EquipTestItemReaGoodLink指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaEquipTestItemReaGoodLinkByField", Get = "", Post = "ReaEquipTestItemReaGoodLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaEquipTestItemReaGoodLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaEquipTestItemReaGoodLinkByField(ReaEquipTestItemReaGoodLink entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_EquipTestItemReaGoodLink", Desc = "删除Rea_EquipTestItemReaGoodLink", Url = "ReaSysManageService.svc/ST_UDTO_DelReaEquipTestItemReaGoodLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaEquipTestItemReaGoodLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaEquipTestItemReaGoodLink(long id);

        [ServiceContractDescription(Name = "查询Rea_EquipTestItemReaGoodLink", Desc = "查询Rea_EquipTestItemReaGoodLink", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaEquipTestItemReaGoodLink", Get = "", Post = "ReaEquipTestItemReaGoodLink", Return = "BaseResultList<ReaEquipTestItemReaGoodLink>", ReturnType = "ListReaEquipTestItemReaGoodLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaEquipTestItemReaGoodLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaEquipTestItemReaGoodLink(ReaEquipTestItemReaGoodLink entity);

        [ServiceContractDescription(Name = "查询Rea_EquipTestItemReaGoodLink(HQL)", Desc = "查询Rea_EquipTestItemReaGoodLink(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaEquipTestItemReaGoodLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaEquipTestItemReaGoodLink>", ReturnType = "ListReaEquipTestItemReaGoodLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaEquipTestItemReaGoodLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaEquipTestItemReaGoodLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_EquipTestItemReaGoodLink", Desc = "通过主键ID查询Rea_EquipTestItemReaGoodLink", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaEquipTestItemReaGoodLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaEquipTestItemReaGoodLink>", ReturnType = "ReaEquipTestItemReaGoodLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaEquipTestItemReaGoodLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaEquipTestItemReaGoodLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaTestEquipItem

        [ServiceContractDescription(Name = "新增Rea_TestEquipItem", Desc = "新增Rea_TestEquipItem", Url = "ReaSysManageService.svc/ST_UDTO_AddReaTestEquipItem", Get = "", Post = "ReaTestEquipItem", Return = "BaseResultDataValue", ReturnType = "ReaTestEquipItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaTestEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaTestEquipItem(ReaTestEquipItem entity);

        [ServiceContractDescription(Name = "修改Rea_TestEquipItem", Desc = "修改Rea_TestEquipItem", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaTestEquipItem", Get = "", Post = "ReaTestEquipItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaTestEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaTestEquipItem(ReaTestEquipItem entity);

        [ServiceContractDescription(Name = "修改Rea_TestEquipItem指定的属性", Desc = "修改Rea_TestEquipItem指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaTestEquipItemByField", Get = "", Post = "ReaTestEquipItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaTestEquipItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaTestEquipItemByField(ReaTestEquipItem entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_TestEquipItem", Desc = "删除Rea_TestEquipItem", Url = "ReaSysManageService.svc/ST_UDTO_DelReaTestEquipItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaTestEquipItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaTestEquipItem(long id);

        [ServiceContractDescription(Name = "查询Rea_TestEquipItem", Desc = "查询Rea_TestEquipItem", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipItem", Get = "", Post = "ReaTestEquipItem", Return = "BaseResultList<ReaTestEquipItem>", ReturnType = "ListReaTestEquipItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestEquipItem(ReaTestEquipItem entity);

        [ServiceContractDescription(Name = "查询Rea_TestEquipItem(HQL)", Desc = "查询Rea_TestEquipItem(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaTestEquipItem>", ReturnType = "ListReaTestEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestEquipItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_TestEquipItem", Desc = "通过主键ID查询Rea_TestEquipItem", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestEquipItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaTestEquipItem>", ReturnType = "ReaTestEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestEquipItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestEquipItemById(long id, string fields, bool isPlanish);
        #endregion


        #region ReaTestItem

        [ServiceContractDescription(Name = "新增Rea_TestItem", Desc = "新增Rea_TestItem", Url = "ReaSysManageService.svc/ST_UDTO_AddReaTestItem", Get = "", Post = "ReaTestItem", Return = "BaseResultDataValue", ReturnType = "ReaTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaTestItem(ReaTestItem entity);

        [ServiceContractDescription(Name = "修改Rea_TestItem", Desc = "修改Rea_TestItem", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaTestItem", Get = "", Post = "ReaTestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaTestItem(ReaTestItem entity);

        [ServiceContractDescription(Name = "修改Rea_TestItem指定的属性", Desc = "修改Rea_TestItem指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaTestItemByField", Get = "", Post = "ReaTestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaTestItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaTestItemByField(ReaTestItem entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_TestItem", Desc = "删除Rea_TestItem", Url = "ReaSysManageService.svc/ST_UDTO_DelReaTestItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaTestItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaTestItem(long id);

        [ServiceContractDescription(Name = "查询Rea_TestItem", Desc = "查询Rea_TestItem", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestItem", Get = "", Post = "ReaTestItem", Return = "BaseResultList<ReaTestItem>", ReturnType = "ListReaTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestItem(ReaTestItem entity);

        [ServiceContractDescription(Name = "查询Rea_TestItem(HQL)", Desc = "查询Rea_TestItem(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaTestItem>", ReturnType = "ListReaTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_TestItem", Desc = "通过主键ID查询Rea_TestItem", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaTestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaTestItem>", ReturnType = "ReaTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaTestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaTestItemById(long id, string fields, bool isPlanish);


        [ServiceContractDescription(Name = "导入检验项目Excel文件", Desc = "导入检验项目Excel文件", Url = "ReaSysManageService.svc/ST_UDTO_UploadTestItemByExcel", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UploadTestItemByExcel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message ST_UDTO_UploadTestItemByExcel();


        [ServiceContractDescription(Name = "检验项目信息列表导出Excel文件路径", Desc = "检验项目信息列表导出Excel文件路径", Url = "ReaSysManageService.svc/ST_UDTO_GetTestItemReportExcelPath", Get = "", Post = "reportType,idList,where,isHeader", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetTestItemReportExcelPath", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message ST_UDTO_GetTestItemReportExcelPath();

        #endregion


        #region ReaAlertInfoSettings

        [ServiceContractDescription(Name = "新增Rea_AlertInfoSettings", Desc = "新增Rea_AlertInfoSettings", Url = "ReaSysManageService.svc/ST_UDTO_AddReaAlertInfoSettings", Get = "", Post = "ReaAlertInfoSettings", Return = "BaseResultDataValue", ReturnType = "ReaAlertInfoSettings")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaAlertInfoSettings", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaAlertInfoSettings(ReaAlertInfoSettings entity);

        [ServiceContractDescription(Name = "修改Rea_AlertInfoSettings", Desc = "修改Rea_AlertInfoSettings", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaAlertInfoSettings", Get = "", Post = "ReaAlertInfoSettings", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaAlertInfoSettings", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaAlertInfoSettings(ReaAlertInfoSettings entity);

        [ServiceContractDescription(Name = "修改Rea_AlertInfoSettings指定的属性", Desc = "修改Rea_AlertInfoSettings指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaAlertInfoSettingsByField", Get = "", Post = "ReaAlertInfoSettings", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaAlertInfoSettingsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaAlertInfoSettingsByField(ReaAlertInfoSettings entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_AlertInfoSettings", Desc = "删除Rea_AlertInfoSettings", Url = "ReaSysManageService.svc/ST_UDTO_DelReaAlertInfoSettings?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaAlertInfoSettings?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaAlertInfoSettings(long id);

        [ServiceContractDescription(Name = "查询Rea_AlertInfoSettings", Desc = "查询Rea_AlertInfoSettings", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaAlertInfoSettings", Get = "", Post = "ReaAlertInfoSettings", Return = "BaseResultList<ReaAlertInfoSettings>", ReturnType = "ListReaAlertInfoSettings")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaAlertInfoSettings", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaAlertInfoSettings(ReaAlertInfoSettings entity);

        [ServiceContractDescription(Name = "查询Rea_AlertInfoSettings(HQL)", Desc = "查询Rea_AlertInfoSettings(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaAlertInfoSettingsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaAlertInfoSettings>", ReturnType = "ListReaAlertInfoSettings")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaAlertInfoSettingsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaAlertInfoSettingsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_AlertInfoSettings", Desc = "通过主键ID查询Rea_AlertInfoSettings", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaAlertInfoSettingsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaAlertInfoSettings>", ReturnType = "ReaAlertInfoSettings")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaAlertInfoSettingsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaAlertInfoSettingsById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaMonthUsageStatisticsDoc

        [ServiceContractDescription(Name = "新增Rea_MonthUsageStatisticsDoc", Desc = "新增Rea_MonthUsageStatisticsDoc", Url = "ReaSysManageService.svc/ST_UDTO_AddReaMonthUsageStatisticsDoc", Get = "", Post = "ReaMonthUsageStatisticsDoc", Return = "BaseResultDataValue", ReturnType = "ReaMonthUsageStatisticsDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaMonthUsageStatisticsDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaMonthUsageStatisticsDoc(ReaMonthUsageStatisticsDoc entity);

        [ServiceContractDescription(Name = "修改Rea_MonthUsageStatisticsDoc", Desc = "修改Rea_MonthUsageStatisticsDoc", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaMonthUsageStatisticsDoc", Get = "", Post = "ReaMonthUsageStatisticsDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaMonthUsageStatisticsDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaMonthUsageStatisticsDoc(ReaMonthUsageStatisticsDoc entity);

        [ServiceContractDescription(Name = "修改Rea_MonthUsageStatisticsDoc指定的属性", Desc = "修改Rea_MonthUsageStatisticsDoc指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaMonthUsageStatisticsDocByField", Get = "", Post = "ReaMonthUsageStatisticsDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaMonthUsageStatisticsDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaMonthUsageStatisticsDocByField(ReaMonthUsageStatisticsDoc entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_MonthUsageStatisticsDoc", Desc = "删除Rea_MonthUsageStatisticsDoc", Url = "ReaSysManageService.svc/ST_UDTO_DelReaMonthUsageStatisticsDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaMonthUsageStatisticsDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaMonthUsageStatisticsDoc(long id);

        [ServiceContractDescription(Name = "查询Rea_MonthUsageStatisticsDoc", Desc = "查询Rea_MonthUsageStatisticsDoc", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaMonthUsageStatisticsDoc", Get = "", Post = "ReaMonthUsageStatisticsDoc", Return = "BaseResultList<ReaMonthUsageStatisticsDoc>", ReturnType = "ListReaMonthUsageStatisticsDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaMonthUsageStatisticsDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaMonthUsageStatisticsDoc(ReaMonthUsageStatisticsDoc entity);

        [ServiceContractDescription(Name = "查询Rea_MonthUsageStatisticsDoc(HQL)", Desc = "查询Rea_MonthUsageStatisticsDoc(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaMonthUsageStatisticsDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaMonthUsageStatisticsDoc>", ReturnType = "ListReaMonthUsageStatisticsDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaMonthUsageStatisticsDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaMonthUsageStatisticsDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_MonthUsageStatisticsDoc", Desc = "通过主键ID查询Rea_MonthUsageStatisticsDoc", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaMonthUsageStatisticsDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaMonthUsageStatisticsDoc>", ReturnType = "ReaMonthUsageStatisticsDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaMonthUsageStatisticsDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaMonthUsageStatisticsDocById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaMonthUsageStatisticsDtl

        [ServiceContractDescription(Name = "新增Rea_MonthUsageStatisticsDtl", Desc = "新增Rea_MonthUsageStatisticsDtl", Url = "ReaSysManageService.svc/ST_UDTO_AddReaMonthUsageStatisticsDtl", Get = "", Post = "ReaMonthUsageStatisticsDtl", Return = "BaseResultDataValue", ReturnType = "ReaMonthUsageStatisticsDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaMonthUsageStatisticsDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaMonthUsageStatisticsDtl(ReaMonthUsageStatisticsDtl entity);

        [ServiceContractDescription(Name = "修改Rea_MonthUsageStatisticsDtl", Desc = "修改Rea_MonthUsageStatisticsDtl", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaMonthUsageStatisticsDtl", Get = "", Post = "ReaMonthUsageStatisticsDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaMonthUsageStatisticsDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaMonthUsageStatisticsDtl(ReaMonthUsageStatisticsDtl entity);

        [ServiceContractDescription(Name = "修改Rea_MonthUsageStatisticsDtl指定的属性", Desc = "修改Rea_MonthUsageStatisticsDtl指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaMonthUsageStatisticsDtlByField", Get = "", Post = "ReaMonthUsageStatisticsDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaMonthUsageStatisticsDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaMonthUsageStatisticsDtlByField(ReaMonthUsageStatisticsDtl entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_MonthUsageStatisticsDtl", Desc = "删除Rea_MonthUsageStatisticsDtl", Url = "ReaSysManageService.svc/ST_UDTO_DelReaMonthUsageStatisticsDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaMonthUsageStatisticsDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaMonthUsageStatisticsDtl(long id);

        [ServiceContractDescription(Name = "查询Rea_MonthUsageStatisticsDtl", Desc = "查询Rea_MonthUsageStatisticsDtl", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaMonthUsageStatisticsDtl", Get = "", Post = "ReaMonthUsageStatisticsDtl", Return = "BaseResultList<ReaMonthUsageStatisticsDtl>", ReturnType = "ListReaMonthUsageStatisticsDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaMonthUsageStatisticsDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaMonthUsageStatisticsDtl(ReaMonthUsageStatisticsDtl entity);

        [ServiceContractDescription(Name = "查询Rea_MonthUsageStatisticsDtl(HQL)", Desc = "查询Rea_MonthUsageStatisticsDtl(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaMonthUsageStatisticsDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaMonthUsageStatisticsDtl>", ReturnType = "ListReaMonthUsageStatisticsDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaMonthUsageStatisticsDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaMonthUsageStatisticsDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_MonthUsageStatisticsDtl", Desc = "通过主键ID查询Rea_MonthUsageStatisticsDtl", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaMonthUsageStatisticsDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaMonthUsageStatisticsDtl>", ReturnType = "ReaMonthUsageStatisticsDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaMonthUsageStatisticsDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaMonthUsageStatisticsDtlById(long id, string fields, bool isPlanish);
        #endregion


        #region ReaLisTestStatisticalResults

        [ServiceContractDescription(Name = "新增Rea_LisTestStatisticalResults", Desc = "新增Rea_LisTestStatisticalResults", Url = "ReaSysManageService.svc/ST_UDTO_AddReaLisTestStatisticalResults", Get = "", Post = "ReaLisTestStatisticalResults", Return = "BaseResultDataValue", ReturnType = "ReaLisTestStatisticalResults")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaLisTestStatisticalResults", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaLisTestStatisticalResults(ReaLisTestStatisticalResults entity);

        [ServiceContractDescription(Name = "修改Rea_LisTestStatisticalResults", Desc = "修改Rea_LisTestStatisticalResults", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaLisTestStatisticalResults", Get = "", Post = "ReaLisTestStatisticalResults", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaLisTestStatisticalResults", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaLisTestStatisticalResults(ReaLisTestStatisticalResults entity);

        [ServiceContractDescription(Name = "修改Rea_LisTestStatisticalResults指定的属性", Desc = "修改Rea_LisTestStatisticalResults指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaLisTestStatisticalResultsByField", Get = "", Post = "ReaLisTestStatisticalResults", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaLisTestStatisticalResultsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaLisTestStatisticalResultsByField(ReaLisTestStatisticalResults entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_LisTestStatisticalResults", Desc = "删除Rea_LisTestStatisticalResults", Url = "ReaSysManageService.svc/ST_UDTO_DelReaLisTestStatisticalResults?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaLisTestStatisticalResults?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaLisTestStatisticalResults(long id);

        [ServiceContractDescription(Name = "查询Rea_LisTestStatisticalResults", Desc = "查询Rea_LisTestStatisticalResults", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaLisTestStatisticalResults", Get = "", Post = "ReaLisTestStatisticalResults", Return = "BaseResultList<ReaLisTestStatisticalResults>", ReturnType = "ListReaLisTestStatisticalResults")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaLisTestStatisticalResults", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaLisTestStatisticalResults(ReaLisTestStatisticalResults entity);

        [ServiceContractDescription(Name = "查询Rea_LisTestStatisticalResults(HQL)", Desc = "查询Rea_LisTestStatisticalResults(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaLisTestStatisticalResultsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaLisTestStatisticalResults>", ReturnType = "ListReaLisTestStatisticalResults")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaLisTestStatisticalResultsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaLisTestStatisticalResultsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_LisTestStatisticalResults", Desc = "通过主键ID查询Rea_LisTestStatisticalResults", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaLisTestStatisticalResultsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaLisTestStatisticalResults>", ReturnType = "ReaLisTestStatisticalResults")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaLisTestStatisticalResultsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaLisTestStatisticalResultsById(long id, string fields, bool isPlanish);
        #endregion

        #region BUserUIConfig

        [ServiceContractDescription(Name = "新增B_UserUIConfig", Desc = "新增B_UserUIConfig", Url = "ReaSysManageService.svc/ST_UDTO_AddBUserUIConfig", Get = "", Post = "BUserUIConfig", Return = "BaseResultDataValue", ReturnType = "BUserUIConfig")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBUserUIConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBUserUIConfig(BUserUIConfig entity);

        [ServiceContractDescription(Name = "修改B_UserUIConfig", Desc = "修改B_UserUIConfig", Url = "ReaSysManageService.svc/ST_UDTO_UpdateBUserUIConfig", Get = "", Post = "BUserUIConfig", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBUserUIConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBUserUIConfig(BUserUIConfig entity);

        [ServiceContractDescription(Name = "修改B_UserUIConfig指定的属性", Desc = "修改B_UserUIConfig指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateBUserUIConfigByField", Get = "", Post = "BUserUIConfig", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBUserUIConfigByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBUserUIConfigByField(BUserUIConfig entity, string fields);

        [ServiceContractDescription(Name = "删除B_UserUIConfig", Desc = "删除B_UserUIConfig", Url = "ReaSysManageService.svc/ST_UDTO_DelBUserUIConfig?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBUserUIConfig?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBUserUIConfig(long id);

        [ServiceContractDescription(Name = "查询B_UserUIConfig", Desc = "查询B_UserUIConfig", Url = "ReaSysManageService.svc/ST_UDTO_SearchBUserUIConfig", Get = "", Post = "BUserUIConfig", Return = "BaseResultList<BUserUIConfig>", ReturnType = "ListBUserUIConfig")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBUserUIConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBUserUIConfig(BUserUIConfig entity);

        [ServiceContractDescription(Name = "查询B_UserUIConfig(HQL)", Desc = "查询B_UserUIConfig(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchBUserUIConfigByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BUserUIConfig>", ReturnType = "ListBUserUIConfig")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBUserUIConfigByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBUserUIConfigByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_UserUIConfig", Desc = "通过主键ID查询B_UserUIConfig", Url = "ReaSysManageService.svc/ST_UDTO_SearchBUserUIConfigById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BUserUIConfig>", ReturnType = "BUserUIConfig")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBUserUIConfigById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBUserUIConfigById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaStorageGoodsLink

        [ServiceContractDescription(Name = "新增Rea_Storage_Goods_Link", Desc = "新增Rea_Storage_Goods_Link", Url = "ReaSysManageService.svc/ST_UDTO_AddReaStorageGoodsLink", Get = "", Post = "ReaStorageGoodsLink", Return = "BaseResultDataValue", ReturnType = "ReaStorageGoodsLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaStorageGoodsLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaStorageGoodsLink(ReaStorageGoodsLink entity);

        [ServiceContractDescription(Name = "修改Rea_Storage_Goods_Link", Desc = "修改Rea_Storage_Goods_Link", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaStorageGoodsLink", Get = "", Post = "ReaStorageGoodsLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaStorageGoodsLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaStorageGoodsLink(ReaStorageGoodsLink entity);

        [ServiceContractDescription(Name = "修改Rea_Storage_Goods_Link指定的属性", Desc = "修改Rea_Storage_Goods_Link指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaStorageGoodsLinkByField", Get = "", Post = "ReaStorageGoodsLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaStorageGoodsLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaStorageGoodsLinkByField(ReaStorageGoodsLink entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_Storage_Goods_Link", Desc = "删除Rea_Storage_Goods_Link", Url = "ReaSysManageService.svc/ST_UDTO_DelReaStorageGoodsLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaStorageGoodsLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaStorageGoodsLink(long id);

        [ServiceContractDescription(Name = "查询Rea_Storage_Goods_Link", Desc = "查询Rea_Storage_Goods_Link", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaStorageGoodsLink", Get = "", Post = "ReaStorageGoodsLink", Return = "BaseResultList<ReaStorageGoodsLink>", ReturnType = "ListReaStorageGoodsLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaStorageGoodsLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaStorageGoodsLink(ReaStorageGoodsLink entity);

        [ServiceContractDescription(Name = "查询Rea_Storage_Goods_Link(HQL)", Desc = "查询Rea_Storage_Goods_Link(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaStorageGoodsLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaStorageGoodsLink>", ReturnType = "ListReaStorageGoodsLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaStorageGoodsLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaStorageGoodsLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_Storage_Goods_Link", Desc = "通过主键ID查询Rea_Storage_Goods_Link", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaStorageGoodsLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaStorageGoodsLink>", ReturnType = "ReaStorageGoodsLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaStorageGoodsLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaStorageGoodsLinkById(long id, string fields, bool isPlanish);
        #endregion
    }
}
