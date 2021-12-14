using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.Text;
using System.IO;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;

namespace ZhiFang.ProjectProgressMonitorManage.ServerContract
{

    [ServiceContract]
    public interface ISystemCommonService
    {
        #region ISystemCommonService

        #region SCAttachment
        [ServiceContractDescription(Name = "上传公共附件服务", Desc = "上传公共附件服务", Url = "SystemCommonService.svc/SC_UploadAddSCAttachment", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UploadAddSCAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message SC_UploadAddSCAttachment();

        [ServiceContractDescription(Name = "下载公共附件服务", Desc = "下载公共附件服务", Url = "SystemCommonService.svc/SC_UDTO_DownLoadSCAttachment?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_DownLoadSCAttachment?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream SC_UDTO_DownLoadSCAttachment(long id, long operateType);
        [ServiceContractDescription(Name = "新增公共附件表", Desc = "新增公共附件表", Url = "SystemCommonService.svc/SC_UDTO_AddSCAttachment", Get = "", Post = "SCAttachment", Return = "BaseResultDataValue", ReturnType = "SCAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_AddSCAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_AddSCAttachment(SCAttachment entity);

        [ServiceContractDescription(Name = "修改公共附件表", Desc = "修改公共附件表", Url = "SystemCommonService.svc/SC_UDTO_UpdateSCAttachment", Get = "", Post = "SCAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCAttachment(SCAttachment entity);

        [ServiceContractDescription(Name = "修改公共附件表指定的属性", Desc = "修改公共附件表指定的属性", Url = "SystemCommonService.svc/SC_UDTO_UpdateSCAttachmentByField", Get = "", Post = "SCAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCAttachmentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCAttachmentByField(SCAttachment entity, string fields);

        [ServiceContractDescription(Name = "删除公共附件表", Desc = "删除公共附件表", Url = "SystemCommonService.svc/SC_UDTO_DelSCAttachment?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SC_UDTO_DelSCAttachment?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_DelSCAttachment(long id);

        [ServiceContractDescription(Name = "查询公共附件表", Desc = "查询公共附件表", Url = "SystemCommonService.svc/SC_UDTO_SearchSCAttachment", Get = "", Post = "SCAttachment", Return = "BaseResultList<SCAttachment>", ReturnType = "ListSCAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCAttachment(SCAttachment entity);

        [ServiceContractDescription(Name = "查询公共附件表(HQL)", Desc = "查询公共附件表(HQL)", Url = "SystemCommonService.svc/SC_UDTO_SearchSCAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCAttachment>", ReturnType = "ListSCAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共附件表", Desc = "通过主键ID查询公共附件表", Url = "SystemCommonService.svc/SC_UDTO_SearchSCAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCAttachment>", ReturnType = "SCAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCAttachmentById(long id, string fields, bool isPlanish);
        #endregion

        #region SCInteraction
        [ServiceContractDescription(Name = "新增交流内容服务扩展(支持新增话题或新增交流内容)", Desc = "新增交流内容服务扩展(支持新增话题或新增交流内容)", Url = "SystemCommonService.svc/SC_UDTO_AddSCInteractionExtend", Get = "", Post = "SCInteraction", Return = "BaseResultDataValue", ReturnType = "SCInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_AddSCInteractionExtend", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_AddSCInteractionExtend(SCInteraction entity);

        [ServiceContractDescription(Name = "依某一业务对象ID获取该业务对象ID下的所有交流内容信息", Desc = "依某一业务对象ID获取该业务对象ID下的所有交流内容信息", Url = "SystemCommonService.svc/SC_UDTO_SearchAllSCInteractionByBobjectID?page={page}&limit={limit}&fields={fields}&bobjectID={bobjectID}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&bobjectID={bobjectID}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCInteraction>", ReturnType = "ListSCInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchAllSCInteractionByBobjectID?page={page}&limit={limit}&fields={fields}&bobjectID={bobjectID}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchAllSCInteractionByBobjectID(int page, int limit, string fields, string bobjectID, string sort, bool isPlanish);


        [ServiceContractDescription(Name = "新增程序交流表", Desc = "新增程序交流表", Url = "SystemCommonService.svc/SC_UDTO_AddSCInteraction", Get = "", Post = "SCInteraction", Return = "BaseResultDataValue", ReturnType = "SCInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_AddSCInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]    
        BaseResultDataValue SC_UDTO_AddSCInteraction(SCInteraction entity);

        [ServiceContractDescription(Name = "修改程序交流表", Desc = "修改程序交流表", Url = "SystemCommonService.svc/SC_UDTO_UpdateSCInteraction", Get = "", Post = "SCInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCInteraction(SCInteraction entity);

        [ServiceContractDescription(Name = "修改程序交流表指定的属性", Desc = "修改程序交流表指定的属性", Url = "SystemCommonService.svc/SC_UDTO_UpdateSCInteractionByField", Get = "", Post = "SCInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCInteractionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCInteractionByField(SCInteraction entity, string fields);

        [ServiceContractDescription(Name = "删除程序交流表", Desc = "删除程序交流表", Url = "SystemCommonService.svc/SC_UDTO_DelSCInteraction?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SC_UDTO_DelSCInteraction?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_DelSCInteraction(long id);

        [ServiceContractDescription(Name = "查询程序交流表", Desc = "查询程序交流表", Url = "SystemCommonService.svc/SC_UDTO_SearchSCInteraction", Get = "", Post = "SCInteraction", Return = "BaseResultList<SCInteraction>", ReturnType = "ListSCInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCInteraction(SCInteraction entity);

        [ServiceContractDescription(Name = "查询程序交流表(HQL)", Desc = "查询程序交流表(HQL)", Url = "SystemCommonService.svc/SC_UDTO_SearchSCInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCInteraction>", ReturnType = "ListSCInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询程序交流表", Desc = "通过主键ID查询程序交流表", Url = "SystemCommonService.svc/SC_UDTO_SearchSCInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCInteraction>", ReturnType = "SCInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCInteractionById(long id, string fields, bool isPlanish);
        #endregion

        #region SCOperation

        [ServiceContractDescription(Name = "新增公共操作记录表", Desc = "新增公共操作记录表", Url = "SystemCommonService.svc/SC_UDTO_AddSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultDataValue", ReturnType = "SCOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_AddSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_AddSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "修改公共操作记录表", Desc = "修改公共操作记录表", Url = "SystemCommonService.svc/SC_UDTO_UpdateSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "修改公共操作记录表指定的属性", Desc = "修改公共操作记录表指定的属性", Url = "SystemCommonService.svc/SC_UDTO_UpdateSCOperationByField", Get = "", Post = "SCOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCOperationByField(SCOperation entity, string fields);

        [ServiceContractDescription(Name = "删除公共操作记录表", Desc = "删除公共操作记录表", Url = "SystemCommonService.svc/SC_UDTO_DelSCOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SC_UDTO_DelSCOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_DelSCOperation(long id);

        [ServiceContractDescription(Name = "查询公共操作记录表", Desc = "查询公共操作记录表", Url = "SystemCommonService.svc/SC_UDTO_SearchSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultList<SCOperation>", ReturnType = "ListSCOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "查询公共操作记录表(HQL)", Desc = "查询公共操作记录表(HQL)", Url = "SystemCommonService.svc/SC_UDTO_SearchSCOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCOperation>", ReturnType = "ListSCOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共操作记录表", Desc = "通过主键ID查询公共操作记录表", Url = "SystemCommonService.svc/SC_UDTO_SearchSCOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCOperation>", ReturnType = "SCOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCOperationById(long id, string fields, bool isPlanish);
        #endregion        

        #region 获取程序内部字典
        [ServiceContractDescription(Name = "获取枚举字典", Desc = "获取枚举字典", Url = "SystemCommonService.svc/GetEnumDic?enumname={enumname}", Get = "enumname={enumname}", Post = "", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetEnumDic?enumname={enumname}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetEnumDic(string enumname);

        [ServiceContractDescription(Name = "获取类字典", Desc = "获取类字典", Url = "SystemCommonService.svc/GetClassDic?classname={classname}&classnamespace={classnamespace}", Get = "classname={classname}&classnamespace={classnamespace}", Post = "", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetClassDic?classname={classname}&classnamespace={classnamespace}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetClassDic(string classname, string classnamespace);

        [ServiceContractDescription(Name = "获取类字典列表", Desc = "获取类字典列表", Url = "SystemCommonService.svc/GetClassDicList", Get = "", Post = "ClassDicSearchPara", Return = "BaseResultDataValue", ReturnType = "ClassDicList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetClassDicList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetClassDicList(ClassDicSearchPara[] jsonpara);

        #endregion
        #endregion

        #region 电子签名图片处理
        [ServiceContractDescription(Name = "上传电子签名图片", Desc = "上传电子签名图片", Url = "SystemCommonService.svc/SC_UDTO_UploadEmpSignByEmpId", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UploadEmpSignByEmpId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message SC_UDTO_UploadEmpSignByEmpId();

        [ServiceContractDescription(Name = "下载电子签名图片", Desc = "下载电子签名图片", Url = "SystemCommonService.svc/PGM_UDTO_DownLoadEmpSignByEmpId?empId={empId}&operateType={operateType}", Get = "empId={empId}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UDTO_DownLoadEmpSignByEmpId?empId={empId}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream PGM_UDTO_DownLoadEmpSignByEmpId(long empId, long operateType);
        #endregion


        [ServiceContractDescription(Name = "测试模块服务缓存信息", Desc = "测试模块服务缓存信息", Url = "SystemCommonService.svc/SC_UDTO_GetCacheModuleOperByModuleIdAndUrl?moduleId={moduleId}&url={url}", Get = "id={id}&url={url}", Post = "", Return = "TestModuleOper", ReturnType = "TestModuleOper")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_GetCacheModuleOperByModuleIdAndUrl?moduleId={moduleId}&url={url}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_GetCacheModuleOperByModuleIdAndUrl(long moduleId, string url);
    }
}
