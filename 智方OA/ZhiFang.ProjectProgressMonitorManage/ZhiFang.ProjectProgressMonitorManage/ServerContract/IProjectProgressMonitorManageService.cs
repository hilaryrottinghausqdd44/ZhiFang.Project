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
using ZhiFang.Entity.OA;

namespace ZhiFang.ProjectProgressMonitorManage.ServerContract
{

    [ServiceContract]
    public interface IProjectProgressMonitorManageService
    {
        #region PDict

        [ServiceContractDescription(Name = "新增字典表", Desc = "新增字典表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPDict", Get = "", Post = "PDict", Return = "BaseResultDataValue", ReturnType = "PDict")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPDict(PDict entity);

        [ServiceContractDescription(Name = "修改字典表", Desc = "修改字典表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePDict", Get = "", Post = "PDict", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePDict(PDict entity);

        [ServiceContractDescription(Name = "修改字典表指定的属性", Desc = "修改字典表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePDictByField", Get = "", Post = "PDict", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePDictByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePDictByField(PDict entity, string fields);

        [ServiceContractDescription(Name = "删除字典表", Desc = "删除字典表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPDict?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPDict?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPDict(long id);

        [ServiceContractDescription(Name = "查询字典表", Desc = "查询字典表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDict", Get = "", Post = "PDict", Return = "BaseResultList<PDict>", ReturnType = "ListPDict")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPDict(PDict entity);

        [ServiceContractDescription(Name = "查询字典表(HQL)", Desc = "查询字典表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PDict>", ReturnType = "ListPDict")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPDictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPDictByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询字典表", Desc = "通过主键ID查询字典表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PDict>", ReturnType = "PDict")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPDictById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPDictById(long id, string fields, bool isPlanish);
        #endregion

        #region PDictType

        [ServiceContractDescription(Name = "新增字典类型表", Desc = "新增字典类型表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPDictType", Get = "", Post = "PDictType", Return = "BaseResultDataValue", ReturnType = "PDictType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPDictType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPDictType(PDictType entity);

        [ServiceContractDescription(Name = "修改字典类型表", Desc = "修改字典类型表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePDictType", Get = "", Post = "PDictType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePDictType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePDictType(PDictType entity);

        [ServiceContractDescription(Name = "修改字典类型表指定的属性", Desc = "修改字典类型表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePDictTypeByField", Get = "", Post = "PDictType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePDictTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePDictTypeByField(PDictType entity, string fields);

        [ServiceContractDescription(Name = "删除字典类型表", Desc = "删除字典类型表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPDictType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPDictType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPDictType(long id);

        [ServiceContractDescription(Name = "查询字典类型表", Desc = "查询字典类型表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictType", Get = "", Post = "PDictType", Return = "BaseResultList<PDictType>", ReturnType = "ListPDictType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPDictType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPDictType(PDictType entity);

        [ServiceContractDescription(Name = "查询字典类型表(HQL)", Desc = "查询字典类型表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PDictType>", ReturnType = "ListPDictType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPDictTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPDictTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询字典类型表", Desc = "通过主键ID查询字典类型表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPDictTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PDictType>", ReturnType = "PDictType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPDictTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPDictTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region PInteraction

        [ServiceContractDescription(Name = "新增互动记录表", Desc = "新增互动记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPInteraction", Get = "", Post = "PInteraction", Return = "BaseResultDataValue", ReturnType = "PInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPInteraction(PInteraction entity);

        [ServiceContractDescription(Name = "修改互动记录表", Desc = "修改互动记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePInteraction", Get = "", Post = "PInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePInteraction(PInteraction entity);

        [ServiceContractDescription(Name = "修改互动记录表指定的属性", Desc = "修改互动记录表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePInteractionByField", Get = "", Post = "PInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePInteractionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePInteractionByField(PInteraction entity, string fields);

        [ServiceContractDescription(Name = "删除互动记录表", Desc = "删除互动记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPInteraction?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPInteraction?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPInteraction(long id);

        [ServiceContractDescription(Name = "查询互动记录表", Desc = "查询互动记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInteraction", Get = "", Post = "PInteraction", Return = "BaseResultList<PInteraction>", ReturnType = "ListPInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPInteraction(PInteraction entity);

        [ServiceContractDescription(Name = "查询互动记录表(HQL)", Desc = "查询互动记录表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PInteraction>", ReturnType = "ListPInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询互动记录表", Desc = "通过主键ID查询互动记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PInteraction>", ReturnType = "PInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPInteractionById(long id, string fields, bool isPlanish);
        #endregion        

        #region PProjectAttachment

        [ServiceContractDescription(Name = "新增项目附件表", Desc = "新增项目附件表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPProjectAttachment", Get = "", Post = "PProjectAttachment", Return = "BaseResultDataValue", ReturnType = "PProjectAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPProjectAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPProjectAttachment(PProjectAttachment entity);

        [ServiceContractDescription(Name = "修改项目附件表", Desc = "修改项目附件表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePProjectAttachment", Get = "", Post = "PProjectAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePProjectAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePProjectAttachment(PProjectAttachment entity);

        [ServiceContractDescription(Name = "修改项目附件表指定的属性", Desc = "修改项目附件表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePProjectAttachmentByField", Get = "", Post = "PProjectAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePProjectAttachmentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePProjectAttachmentByField(PProjectAttachment entity, string fields);

        [ServiceContractDescription(Name = "删除项目附件表", Desc = "删除项目附件表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPProjectAttachment?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPProjectAttachment?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPProjectAttachment(long id);

        [ServiceContractDescription(Name = "查询项目附件表", Desc = "查询项目附件表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectAttachment", Get = "", Post = "PProjectAttachment", Return = "BaseResultList<PProjectAttachment>", ReturnType = "ListPProjectAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectAttachment(PProjectAttachment entity);

        [ServiceContractDescription(Name = "查询项目附件表(HQL)", Desc = "查询项目附件表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PProjectAttachment>", ReturnType = "ListPProjectAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询项目附件表", Desc = "通过主键ID查询项目附件表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PProjectAttachment>", ReturnType = "PProjectAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectAttachmentById(long id, string fields, bool isPlanish);
        #endregion

        #region PTask

        [ServiceContractDescription(Name = "新增任务表", Desc = "新增任务表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTask", Get = "", Post = "PTask", Return = "BaseResultDataValue", ReturnType = "PTask")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPTask", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPTask(PTask entity);

        [ServiceContractDescription(Name = "新增任务表", Desc = "新增任务表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_PTaskAdd", Get = "", Post = "PTask", Return = "BaseResultDataValue", ReturnType = "PTask")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_PTaskAdd", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_PTaskAdd(PTask entity);

        [ServiceContractDescription(Name = "修改任务表", Desc = "修改任务表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTask", Get = "", Post = "PTask", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePTask", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePTask(PTask entity);

        [ServiceContractDescription(Name = "修改任务表指定的属性", Desc = "修改任务表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskByField", Get = "", Post = "PTask", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePTaskByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePTaskByField(PTask entity, string fields);

        [ServiceContractDescription(Name = "任务流程操作", Desc = "任务流程操作", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskStatusByField", Get = "", Post = "PTask", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePTaskStatusByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePTaskStatusByField(PTask entity, string fields);

        [ServiceContractDescription(Name = "删除任务表", Desc = "删除任务表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPTask?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPTask?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPTask(long id);

        [ServiceContractDescription(Name = "查询任务表", Desc = "查询任务表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTask", Get = "", Post = "PTask", Return = "BaseResultList<PTask>", ReturnType = "ListPTask")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPTask", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPTask(PTask entity);

        [ServiceContractDescription(Name = "查询任务表(HQL)", Desc = "查询任务表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PTask>", ReturnType = "ListPTask")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPTaskByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPTaskByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询任务表", Desc = "通过主键ID查询任务表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PTask>", ReturnType = "PTask")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPTaskById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPTaskById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询（高级）任务列表", Desc = "查询（高级）任务列表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AdvSearchPTask", Get = "", Post = "Task_Search", Return = "BaseResultDataValue", ReturnType = "Task_Search")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AdvSearchPTask", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AdvSearchPTask(Task_Search entity);
        #endregion

        #region PTaskCopyFor

        [ServiceContractDescription(Name = "新增抄送关系表", Desc = "新增抄送关系表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskCopyFor", Get = "", Post = "PTaskCopyFor", Return = "BaseResultDataValue", ReturnType = "PTaskCopyFor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPTaskCopyFor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPTaskCopyFor(PTaskCopyFor entity);

        [ServiceContractDescription(Name = "修改抄送关系表", Desc = "修改抄送关系表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskCopyFor", Get = "", Post = "PTaskCopyFor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePTaskCopyFor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePTaskCopyFor(PTaskCopyFor entity);

        [ServiceContractDescription(Name = "修改抄送关系表指定的属性", Desc = "修改抄送关系表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskCopyForByField", Get = "", Post = "PTaskCopyFor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePTaskCopyForByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePTaskCopyForByField(PTaskCopyFor entity, string fields);

        [ServiceContractDescription(Name = "删除抄送关系表", Desc = "删除抄送关系表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPTaskCopyFor?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPTaskCopyFor?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPTaskCopyFor(long id);

        [ServiceContractDescription(Name = "查询抄送关系表", Desc = "查询抄送关系表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskCopyFor", Get = "", Post = "PTaskCopyFor", Return = "BaseResultList<PTaskCopyFor>", ReturnType = "ListPTaskCopyFor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPTaskCopyFor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPTaskCopyFor(PTaskCopyFor entity);

        [ServiceContractDescription(Name = "查询抄送关系表(HQL)", Desc = "查询抄送关系表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskCopyForByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PTaskCopyFor>", ReturnType = "ListPTaskCopyFor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPTaskCopyForByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPTaskCopyForByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询抄送关系表", Desc = "通过主键ID查询抄送关系表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskCopyForById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PTaskCopyFor>", ReturnType = "PTaskCopyFor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPTaskCopyForById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPTaskCopyForById(long id, string fields, bool isPlanish);
        #endregion

        #region PTaskOperLog

        [ServiceContractDescription(Name = "新增任务操作记录表", Desc = "新增任务操作记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskOperLog", Get = "", Post = "PTaskOperLog", Return = "BaseResultDataValue", ReturnType = "PTaskOperLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPTaskOperLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPTaskOperLog(PTaskOperLog entity);

        [ServiceContractDescription(Name = "修改任务操作记录表", Desc = "修改任务操作记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskOperLog", Get = "", Post = "PTaskOperLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePTaskOperLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePTaskOperLog(PTaskOperLog entity);

        [ServiceContractDescription(Name = "修改任务操作记录表指定的属性", Desc = "修改任务操作记录表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskOperLogByField", Get = "", Post = "PTaskOperLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePTaskOperLogByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePTaskOperLogByField(PTaskOperLog entity, string fields);

        [ServiceContractDescription(Name = "删除任务操作记录表", Desc = "删除任务操作记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPTaskOperLog?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPTaskOperLog?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPTaskOperLog(long id);

        [ServiceContractDescription(Name = "查询任务操作记录表", Desc = "查询任务操作记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskOperLog", Get = "", Post = "PTaskOperLog", Return = "BaseResultList<PTaskOperLog>", ReturnType = "ListPTaskOperLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPTaskOperLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPTaskOperLog(PTaskOperLog entity);

        [ServiceContractDescription(Name = "查询任务操作记录表(HQL)", Desc = "查询任务操作记录表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskOperLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PTaskOperLog>", ReturnType = "ListPTaskOperLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPTaskOperLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPTaskOperLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询任务操作记录表", Desc = "通过主键ID查询任务操作记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskOperLogById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PTaskOperLog>", ReturnType = "PTaskOperLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPTaskOperLogById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPTaskOperLogById(long id, string fields, bool isPlanish);
        #endregion

        #region PWorkDayLog

        [ServiceContractDescription(Name = "新增工作日志表", Desc = "新增工作日志表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkDayLog", Get = "", Post = "PWorkDayLog", Return = "BaseResultDataValue", ReturnType = "PWorkDayLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPWorkDayLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPWorkDayLog(PWorkDayLog entity);

        [ServiceContractDescription(Name = "新增工作日志表通过微信客户端", Desc = "新增工作日志表通过微信客户端", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkDayLogByWeiXin", Get = "", Post = "PWorkDayLog", Return = "BaseResultDataValue", ReturnType = "PWorkDayLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPWorkDayLogByWeiXin", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPWorkDayLogByWeiXin(PWorkDayLog entity, List<string> AttachmentUrlList);

        [ServiceContractDescription(Name = "修改工作日志表", Desc = "修改工作日志表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkDayLog", Get = "", Post = "PWorkDayLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkDayLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePWorkDayLog(PWorkDayLog entity);

        [ServiceContractDescription(Name = "修改工作日志表指定的属性", Desc = "修改工作日志表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkDayLogByField", Get = "", Post = "PWorkDayLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkDayLogByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePWorkDayLogByField(PWorkDayLog entity, string fields);

        [ServiceContractDescription(Name = "删除工作日志表", Desc = "删除工作日志表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPWorkDayLog?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPWorkDayLog?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPWorkDayLog(long id);

        [ServiceContractDescription(Name = "查询工作日志表", Desc = "查询工作日志表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkDayLog", Get = "", Post = "PWorkDayLog", Return = "BaseResultList<PWorkDayLog>", ReturnType = "ListPWorkDayLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkDayLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkDayLog(PWorkDayLog entity);

        [ServiceContractDescription(Name = "查询工作日志表(HQL)", Desc = "查询工作日志表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkDayLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkDayLog>", ReturnType = "ListPWorkDayLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkDayLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkDayLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询工作日志表", Desc = "通过主键ID查询工作日志表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkDayLogById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkDayLog>", ReturnType = "PWorkDayLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkDayLogById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkDayLogById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询工作日志表", Desc = "查询工作日志表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType?sd={sd}&ed={ed}&page={page}&limit={limit}&sendtype={sendtype}&worklogtype={worklogtype}&sort={sort}&empid={empid}", Get = "sd={sd}&ed={ed}&page={page}&limit={limit}&sendtype={sendtype}&worklogtype={worklogtype}&sort={sort}&empid={empid}", Post = "", Return = "BaseResultList<WorkLogVO>", ReturnType = "ListWorkLogVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType?sd={sd}&ed={ed}&page={page}&limit={limit}&sendtype={sendtype}&worklogtype={worklogtype}&sort={sort}&empid={empid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType(string sd, string ed, int page, int limit, string sendtype, string worklogtype, string sort, string empid);

        [ServiceContractDescription(Name = "查询任务工作日志表", Desc = "查询任务工作日志表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchTaskWorkDayLogTaskId?sd={sd}&ed={ed}&page={page}&limit={limit}&taskid={taskid}&sort={sort}&empid={empid}", Get = "sd={sd}&ed={ed}&page={page}&limit={limit}&taskid={taskid}&sort={sort}&empid={empid}", Post = "", Return = "BaseResultList<WorkLogVO>", ReturnType = "ListWorkLogVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchTaskWorkDayLogTaskId?sd={sd}&ed={ed}&page={page}&limit={limit}&taskid={taskid}&sort={sort}&empid={empid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchTaskWorkDayLogTaskId(string sd, string ed, int page, int limit, string taskid, string sort, string empid);

        [ServiceContractDescription(Name = "查询任务工作日志信息", Desc = "查询任务工作日志信息", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchWorkDayLogByIdAndWorkLogType?Id={Id}&worklogtype={worklogtype}", Get = "Id={Id}&worklogtype={worklogtype}", Post = "", Return = "BaseResultList<WorkLogVO>", ReturnType = "ListWorkLogVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchWorkDayLogByIdAndWorkLogType?Id={Id}&worklogtype={worklogtype}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchWorkDayLogByIdAndWorkLogType(long Id, string worklogtype);

        [ServiceContractDescription(Name = "工作日志点赞", Desc = "工作日志点赞", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_WorkDayAddLikeCountLogByIdAndWorkLogType?Id={Id}&worklogtype={worklogtype}", Get = "Id={Id}&worklogtype={worklogtype}", Post = "", Return = "BaseResultList<bool>", ReturnType = "bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_WorkDayAddLikeCountLogByIdAndWorkLogType?Id={Id}&worklogtype={worklogtype}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_WorkDayAddLikeCountLogByIdAndWorkLogType(long Id, string worklogtype);

        [ServiceContractDescription(Name = "查询统计工作日志表", Desc = "查询统计工作日志表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType?sd={sd}&ed={ed}&page={page}&limit={limit}&deptid={deptid}&worklogtype={worklogtype}&sort={sort}&empid={empid}&isincludesubdept={isincludesubdept}", Get = "sd={sd}&ed={ed}&page={page}&limit={limit}&deptid={deptid}&worklogtype={worklogtype}&sort={sort}&empid={empid}&isincludesubdept={isincludesubdept}", Post = "", Return = "BaseResultList<WorkLogVO>", ReturnType = "ListWorkLogVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType?sd={sd}&ed={ed}&page={page}&limit={limit}&deptid={deptid}&worklogtype={worklogtype}&sort={sort}&empid={empid}&isincludesubdept={isincludesubdept}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType(string sd, string ed, int page, int limit, string deptid, string worklogtype, string sort, string empid, bool isincludesubdept);

        [ServiceContractDescription(Name = "查询工作日志表通过EmpId", Desc = "查询工作日志表通过EmpId", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkDayLogByEmpId?monthday={monthday}&empid={empid}&isincludesubdept={isincludesubdept}", Get = "monthday={monthday}&empid={empid}&isincludesubdept={isincludesubdept}", Post = "", Return = "BaseResultList<WorkLogVO>", ReturnType = "ListWorkLogVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkDayLogByEmpId?monthday={monthday}&empid={empid}&isincludesubdept={isincludesubdept}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkDayLogByEmpId(string monthday, string empid, bool isincludesubdept);

        #endregion

        #region PWorkLogCopyFor

        [ServiceContractDescription(Name = "新增工作日志抄送关系表（日、周、月）", Desc = "新增工作日志抄送关系表（日、周、月）", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkLogCopyFor", Get = "", Post = "PWorkLogCopyFor", Return = "BaseResultDataValue", ReturnType = "PWorkLogCopyFor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPWorkLogCopyFor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPWorkLogCopyFor(PWorkLogCopyFor entity);

        [ServiceContractDescription(Name = "修改工作日志抄送关系表（日、周、月）", Desc = "修改工作日志抄送关系表（日、周、月）", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkLogCopyFor", Get = "", Post = "PWorkLogCopyFor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkLogCopyFor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePWorkLogCopyFor(PWorkLogCopyFor entity);

        [ServiceContractDescription(Name = "修改工作日志抄送关系表（日、周、月）指定的属性", Desc = "修改工作日志抄送关系表（日、周、月）指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkLogCopyForByField", Get = "", Post = "PWorkLogCopyFor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkLogCopyForByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePWorkLogCopyForByField(PWorkLogCopyFor entity, string fields);

        [ServiceContractDescription(Name = "删除工作日志抄送关系表（日、周、月）", Desc = "删除工作日志抄送关系表（日、周、月）", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPWorkLogCopyFor?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPWorkLogCopyFor?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPWorkLogCopyFor(long id);

        [ServiceContractDescription(Name = "查询工作日志抄送关系表（日、周、月）", Desc = "查询工作日志抄送关系表（日、周、月）", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkLogCopyFor", Get = "", Post = "PWorkLogCopyFor", Return = "BaseResultList<PWorkLogCopyFor>", ReturnType = "ListPWorkLogCopyFor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkLogCopyFor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkLogCopyFor(PWorkLogCopyFor entity);

        [ServiceContractDescription(Name = "查询工作日志抄送关系表（日、周、月）(HQL)", Desc = "查询工作日志抄送关系表（日、周、月）(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkLogCopyForByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkLogCopyFor>", ReturnType = "ListPWorkLogCopyFor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkLogCopyForByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkLogCopyForByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询工作日志抄送关系表（日、周、月）", Desc = "通过主键ID查询工作日志抄送关系表（日、周、月）", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkLogCopyForById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkLogCopyFor>", ReturnType = "PWorkLogCopyFor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkLogCopyForById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkLogCopyForById(long id, string fields, bool isPlanish);
        #endregion

        #region PWorkLogSendFor

        [ServiceContractDescription(Name = "新增工作日志发送关系表（日、周、月）", Desc = "新增工作日志发送关系表（日、周、月）", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkLogSendFor", Get = "", Post = "PWorkLogSendFor", Return = "BaseResultDataValue", ReturnType = "PWorkLogSendFor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPWorkLogSendFor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPWorkLogSendFor(PWorkLogSendFor entity);

        [ServiceContractDescription(Name = "修改工作日志发送关系表（日、周、月）", Desc = "修改工作日志发送关系表（日、周、月）", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkLogSendFor", Get = "", Post = "PWorkLogSendFor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkLogSendFor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePWorkLogSendFor(PWorkLogSendFor entity);

        [ServiceContractDescription(Name = "修改工作日志发送关系表（日、周、月）指定的属性", Desc = "修改工作日志发送关系表（日、周、月）指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkLogSendForByField", Get = "", Post = "PWorkLogSendFor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkLogSendForByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePWorkLogSendForByField(PWorkLogSendFor entity, string fields);

        [ServiceContractDescription(Name = "删除工作日志发送关系表（日、周、月）", Desc = "删除工作日志发送关系表（日、周、月）", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPWorkLogSendFor?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPWorkLogSendFor?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPWorkLogSendFor(long id);

        [ServiceContractDescription(Name = "查询工作日志发送关系表（日、周、月）", Desc = "查询工作日志发送关系表（日、周、月）", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkLogSendFor", Get = "", Post = "PWorkLogSendFor", Return = "BaseResultList<PWorkLogSendFor>", ReturnType = "ListPWorkLogSendFor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkLogSendFor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkLogSendFor(PWorkLogSendFor entity);

        [ServiceContractDescription(Name = "查询工作日志发送关系表（日、周、月）(HQL)", Desc = "查询工作日志发送关系表（日、周、月）(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkLogSendForByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkLogSendFor>", ReturnType = "ListPWorkLogSendFor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkLogSendForByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkLogSendForByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询工作日志发送关系表（日、周、月）", Desc = "通过主键ID查询工作日志发送关系表（日、周、月）", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkLogSendForById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkLogSendFor>", ReturnType = "PWorkLogSendFor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkLogSendForById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkLogSendForById(long id, string fields, bool isPlanish);
        #endregion

        #region PWorkMonthLog

        [ServiceContractDescription(Name = "新增工作月总结表", Desc = "新增工作月总结表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkMonthLog", Get = "", Post = "PWorkMonthLog", Return = "BaseResultDataValue", ReturnType = "PWorkMonthLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPWorkMonthLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPWorkMonthLog(PWorkMonthLog entity);

        [ServiceContractDescription(Name = "新增工作月总结表通过微信客户端", Desc = "新增工作月总结表通过微信客户端", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkMonthLogByWeiXin", Get = "", Post = "PWorkMonthLog", Return = "BaseResultDataValue", ReturnType = "PWorkMonthLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPWorkMonthLogByWeiXin", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPWorkMonthLogByWeiXin(PWorkMonthLog entity, List<string> AttachmentUrlList);

        [ServiceContractDescription(Name = "修改工作月总结表", Desc = "修改工作月总结表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkMonthLog", Get = "", Post = "PWorkMonthLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkMonthLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePWorkMonthLog(PWorkMonthLog entity);

        [ServiceContractDescription(Name = "修改工作月总结表指定的属性", Desc = "修改工作月总结表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkMonthLogByField", Get = "", Post = "PWorkMonthLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkMonthLogByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePWorkMonthLogByField(PWorkMonthLog entity, string fields);

        [ServiceContractDescription(Name = "删除工作月总结表", Desc = "删除工作月总结表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPWorkMonthLog?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPWorkMonthLog?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPWorkMonthLog(long id);

        [ServiceContractDescription(Name = "查询工作月总结表", Desc = "查询工作月总结表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkMonthLog", Get = "", Post = "PWorkMonthLog", Return = "BaseResultList<PWorkMonthLog>", ReturnType = "ListPWorkMonthLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkMonthLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkMonthLog(PWorkMonthLog entity);

        [ServiceContractDescription(Name = "查询工作月总结表(HQL)", Desc = "查询工作月总结表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkMonthLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkMonthLog>", ReturnType = "ListPWorkMonthLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkMonthLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkMonthLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询工作月总结表", Desc = "通过主键ID查询工作月总结表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkMonthLogById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkMonthLog>", ReturnType = "PWorkMonthLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkMonthLogById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkMonthLogById(long id, string fields, bool isPlanish);
        #endregion

        //#region PWorkTaskLog

        //[ServiceContractDescription(Name = "新增工作任务日志表", Desc = "新增工作任务日志表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkTaskLog", Get = "", Post = "PWorkTaskLog", Return = "BaseResultDataValue", ReturnType = "PWorkTaskLog")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPWorkTaskLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_AddPWorkTaskLog(PWorkTaskLog entity);

        //[ServiceContractDescription(Name = "修改工作任务日志表", Desc = "修改工作任务日志表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkTaskLog", Get = "", Post = "PWorkTaskLog", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkTaskLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdatePWorkTaskLog(PWorkTaskLog entity);

        //[ServiceContractDescription(Name = "修改工作任务日志表指定的属性", Desc = "修改工作任务日志表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkTaskLogByField", Get = "", Post = "PWorkTaskLog", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkTaskLogByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdatePWorkTaskLogByField(PWorkTaskLog entity, string fields);

        //[ServiceContractDescription(Name = "删除工作任务日志表", Desc = "删除工作任务日志表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPWorkTaskLog?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPWorkTaskLog?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelPWorkTaskLog(long id);

        //[ServiceContractDescription(Name = "查询工作任务日志表", Desc = "查询工作任务日志表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkTaskLog", Get = "", Post = "PWorkTaskLog", Return = "BaseResultList<PWorkTaskLog>", ReturnType = "ListPWorkTaskLog")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkTaskLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_SearchPWorkTaskLog(PWorkTaskLog entity);

        //[ServiceContractDescription(Name = "查询工作任务日志表(HQL)", Desc = "查询工作任务日志表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkTaskLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkTaskLog>", ReturnType = "ListPWorkTaskLog")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkTaskLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_SearchPWorkTaskLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询工作任务日志表", Desc = "通过主键ID查询工作任务日志表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkTaskLogById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkTaskLog>", ReturnType = "PWorkTaskLog")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkTaskLogById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_SearchPWorkTaskLogById(long id, string fields, bool isPlanish);
        //#endregion

        #region PWorkWeekLog

        [ServiceContractDescription(Name = "新增工作周计划表", Desc = "新增工作周计划表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkWeekLog", Get = "", Post = "PWorkWeekLog", Return = "BaseResultDataValue", ReturnType = "PWorkWeekLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPWorkWeekLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPWorkWeekLog(PWorkWeekLog entity);

        [ServiceContractDescription(Name = "新增工作周计划表通过微信客户端", Desc = "新增工作周计划表通过微信客户端", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkWeekLogByWeiXin", Get = "", Post = "PWorkWeekLog", Return = "BaseResultDataValue", ReturnType = "PWorkWeekLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPWorkWeekLogByWeiXin", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPWorkWeekLogByWeiXin(PWorkWeekLog entity, List<string> AttachmentUrlList);

        [ServiceContractDescription(Name = "修改工作周计划表", Desc = "修改工作周计划表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkWeekLog", Get = "", Post = "PWorkWeekLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkWeekLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePWorkWeekLog(PWorkWeekLog entity);

        [ServiceContractDescription(Name = "修改工作周计划表指定的属性", Desc = "修改工作周计划表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkWeekLogByField", Get = "", Post = "PWorkWeekLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkWeekLogByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePWorkWeekLogByField(PWorkWeekLog entity, string fields);

        [ServiceContractDescription(Name = "删除工作周计划表", Desc = "删除工作周计划表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPWorkWeekLog?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPWorkWeekLog?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPWorkWeekLog(long id);

        [ServiceContractDescription(Name = "查询工作周计划表", Desc = "查询工作周计划表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkWeekLog", Get = "", Post = "PWorkWeekLog", Return = "BaseResultList<PWorkWeekLog>", ReturnType = "ListPWorkWeekLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkWeekLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkWeekLog(PWorkWeekLog entity);

        [ServiceContractDescription(Name = "查询工作周计划表(HQL)", Desc = "查询工作周计划表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkWeekLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkWeekLog>", ReturnType = "ListPWorkWeekLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkWeekLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkWeekLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询工作周计划表", Desc = "通过主键ID查询工作周计划表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkWeekLogById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkWeekLog>", ReturnType = "PWorkWeekLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkWeekLogById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkWeekLogById(long id, string fields, bool isPlanish);
        #endregion

        #region PWorkLogInteraction

        [ServiceContractDescription(Name = "新增总结计划互动记录表", Desc = "新增总结计划互动记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPWorkLogInteraction", Get = "", Post = "PWorkLogInteraction", Return = "BaseResultDataValue", ReturnType = "PWorkLogInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPWorkLogInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPWorkLogInteraction(PWorkLogInteraction entity);

        [ServiceContractDescription(Name = "修改总结计划互动记录表", Desc = "修改总结计划互动记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkLogInteraction", Get = "", Post = "PWorkLogInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkLogInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePWorkLogInteraction(PWorkLogInteraction entity);

        [ServiceContractDescription(Name = "修改总结计划互动记录表指定的属性", Desc = "修改总结计划互动记录表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePWorkLogInteractionByField", Get = "", Post = "PWorkLogInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePWorkLogInteractionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePWorkLogInteractionByField(PWorkLogInteraction entity, string fields);

        [ServiceContractDescription(Name = "删除总结计划互动记录表", Desc = "删除总结计划互动记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPWorkLogInteraction?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPWorkLogInteraction?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPWorkLogInteraction(long id);

        [ServiceContractDescription(Name = "查询总结计划互动记录表", Desc = "查询总结计划互动记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkLogInteraction", Get = "", Post = "PWorkLogInteraction", Return = "BaseResultList<PWorkLogInteraction>", ReturnType = "ListPWorkLogInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkLogInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkLogInteraction(PWorkLogInteraction entity);

        [ServiceContractDescription(Name = "查询总结计划互动记录表(HQL)", Desc = "查询总结计划互动记录表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkLogInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkLogInteraction>", ReturnType = "ListPWorkLogInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkLogInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkLogInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询总结计划互动记录表", Desc = "通过主键ID查询总结计划互动记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPWorkLogInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PWorkLogInteraction>", ReturnType = "PWorkLogInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPWorkLogInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPWorkLogInteractionById(long id, string fields, bool isPlanish);
        #endregion

        #region PClient

        [ServiceContractDescription(Name = "新增客户", Desc = "新增客户", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPClient", Get = "", Post = "PClient", Return = "BaseResultDataValue", ReturnType = "PClient")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPClient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPClient(PClient entity);

        [ServiceContractDescription(Name = "修改客户", Desc = "修改客户", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePClient", Get = "", Post = "PClient", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePClient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePClient(PClient entity);

        [ServiceContractDescription(Name = "修改客户指定的属性", Desc = "修改客户指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePClientByField", Get = "", Post = "PClient", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePClientByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePClientByField(PClient entity, string fields);

        [ServiceContractDescription(Name = "删除客户", Desc = "删除客户", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPClient?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPClient?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPClient(long id);

        [ServiceContractDescription(Name = "查询客户", Desc = "查询客户", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClient", Get = "", Post = "PClient", Return = "BaseResultList<PClient>", ReturnType = "ListPClient")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPClient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPClient(PClient entity);

        [ServiceContractDescription(Name = "查询客户(HQL)", Desc = "查询客户(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PClient>", ReturnType = "ListPClient")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPClientByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPClientByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询所属客户(HQL)", Desc = "查询所属客户(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientByHQLAndSalesManId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&SalesManId={SalesManId}&IsOwn={IsOwn}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&SalesManId={SalesManId}&IsOwn={IsOwn}", Post = "", Return = "BaseResultList<PClient>", ReturnType = "ListPClient")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPClientByHQLAndSalesManId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&SalesManId={SalesManId}&IsOwn={IsOwn}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPClientByHQLAndSalesManId(int page, int limit, string fields, string where, string sort, bool isPlanish, long SalesManId, bool IsOwn);

        [ServiceContractDescription(Name = "通过主键ID查询客户", Desc = "通过主键ID查询客户", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PClient>", ReturnType = "PClient")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPClientById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPClientById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "客户信息Excel导出", Desc = "客户信息Excel导出", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_ExportExcelPClient?operateType={operateType}&where={where}", Get = "operateType={operateType}&where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}&SalesManId={SalesManId}&IsOwn={IsOwn}&filename={filename}&type={type}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ExportExcelPClient?operateType={operateType}&where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}&SalesManId={SalesManId}&IsOwn={IsOwn}&filename={filename}&type={type}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_ExportExcelPClient(long operateType, string where, int page, int limit, string fields, string sort, bool isPlanish, long SalesManId, bool IsOwn, string filename, string type);


        #endregion

        #region PClientLinker

        [ServiceContractDescription(Name = "新增联系人", Desc = "新增联系人", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPClientLinker", Get = "", Post = "PClientLinker", Return = "BaseResultDataValue", ReturnType = "PClientLinker")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPClientLinker", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPClientLinker(PClientLinker entity);

        [ServiceContractDescription(Name = "修改联系人", Desc = "修改联系人", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePClientLinker", Get = "", Post = "PClientLinker", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePClientLinker", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePClientLinker(PClientLinker entity);

        [ServiceContractDescription(Name = "修改联系人指定的属性", Desc = "修改联系人指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePClientLinkerByField", Get = "", Post = "PClientLinker", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePClientLinkerByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePClientLinkerByField(PClientLinker entity, string fields);

        [ServiceContractDescription(Name = "删除联系人", Desc = "删除联系人", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPClientLinker?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPClientLinker?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPClientLinker(long id);

        [ServiceContractDescription(Name = "查询联系人", Desc = "查询联系人", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientLinker", Get = "", Post = "PClientLinker", Return = "BaseResultList<PClientLinker>", ReturnType = "ListPClientLinker")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPClientLinker", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPClientLinker(PClientLinker entity);

        [ServiceContractDescription(Name = "查询联系人(HQL)", Desc = "查询联系人(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientLinkerByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PClientLinker>", ReturnType = "ListPClientLinker")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPClientLinkerByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPClientLinkerByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询联系人", Desc = "通过主键ID查询联系人", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPClientLinkerById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PClientLinker>", ReturnType = "PClientLinker")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPClientLinkerById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPClientLinkerById(long id, string fields, bool isPlanish);
        #endregion

        #region PContract

        [ServiceContractDescription(Name = "新增合同", Desc = "新增合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPContract", Get = "", Post = "PContract", Return = "BaseResultDataValue", ReturnType = "PContract")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPContract", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPContract(PContract entity);

        [ServiceContractDescription(Name = "修改合同", Desc = "修改合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContract", Get = "", Post = "PContract", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePContract", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePContract(PContract entity);

        [ServiceContractDescription(Name = "修改合同指定的属性", Desc = "修改合同指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractByField", Get = "", Post = "PContract", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePContractByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePContractByField(PContract entity, string fields);

        [ServiceContractDescription(Name = "删除合同", Desc = "删除合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPContract?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPContract?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPContract(long id);

        [ServiceContractDescription(Name = "查询合同", Desc = "查询合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContract", Get = "", Post = "PContract", Return = "BaseResultList<PContract>", ReturnType = "ListPContract")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPContract", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPContract(PContract entity);

        [ServiceContractDescription(Name = "查询合同(HQL)", Desc = "查询合同(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PContract>", ReturnType = "ListPContract")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPContractByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPContractByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询合同统计(HQL)", Desc = "查询合同统计(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractTotalByHQL?fields={fields}&where={where}", Get = "fields={fields}&where={where}", Post = "", Return = "BaseResultList<PContract>", ReturnType = "ListPContract")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPContractTotalByHQL?fields={fields}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPContractTotalByHQL(string fields, string where);

        [ServiceContractDescription(Name = "通过主键ID查询合同", Desc = "通过主键ID查询合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PContract>", ReturnType = "PContract")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPContractById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPContractById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "修改合同的状态", Desc = "修改合同的状态", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractStatus", Get = "", Post = "PContract", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePContractStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePContractStatus(PContract entity, string fields);
        #endregion

        #region PContractFollow

        [ServiceContractDescription(Name = "新增合同", Desc = "新增合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPContractFollow", Get = "", Post = "PContractFollow", Return = "BaseResultDataValue", ReturnType = "PContractFollow")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPContractFollow", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPContractFollow(PContractFollow entity);

        [ServiceContractDescription(Name = "修改合同", Desc = "修改合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractFollow", Get = "", Post = "PContractFollow", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePContractFollow", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePContractFollow(PContractFollow entity);

        [ServiceContractDescription(Name = "修改合同指定的属性", Desc = "修改合同指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractFollowByField", Get = "", Post = "PContractFollow", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePContractFollowByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePContractFollowByField(PContractFollow entity, string fields);

        [ServiceContractDescription(Name = "删除合同", Desc = "删除合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPContractFollow?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPContractFollow?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPContractFollow(long id);

        [ServiceContractDescription(Name = "查询合同", Desc = "查询合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractFollow", Get = "", Post = "PContractFollow", Return = "BaseResultList<PContractFollow>", ReturnType = "ListPContractFollow")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPContractFollow", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPContractFollow(PContractFollow entity);

        [ServiceContractDescription(Name = "查询合同(HQL)", Desc = "查询合同(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractFollowByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PContractFollow>", ReturnType = "ListPContractFollow")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPContractFollowByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPContractFollowByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询合同统计(HQL)", Desc = "查询合同统计(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractFollowTotalByHQL?fields={fields}&where={where}", Get = "fields={fields}&where={where}", Post = "", Return = "BaseResultList<PContractFollow>", ReturnType = "ListPContractFollow")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPContractFollowTotalByHQL?fields={fields}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPContractFollowTotalByHQL(string fields, string where);

        [ServiceContractDescription(Name = "通过主键ID查询合同", Desc = "通过主键ID查询合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractFollowById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PContractFollow>", ReturnType = "PContractFollow")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPContractFollowById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPContractFollowById(long id, string fields, bool isPlanish);


        #endregion

        #region PContractFollowInteraction

        [ServiceContractDescription(Name = "新增合同", Desc = "新增合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPContractFollowInteraction", Get = "", Post = "PContractFollowInteraction", Return = "BaseResultDataValue", ReturnType = "PContractFollowInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPContractFollowInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPContractFollowInteraction(PContractFollowInteraction entity);

        [ServiceContractDescription(Name = "修改合同", Desc = "修改合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractFollowInteraction", Get = "", Post = "PContractFollowInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePContractFollowInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePContractFollowInteraction(PContractFollowInteraction entity);

        [ServiceContractDescription(Name = "修改合同指定的属性", Desc = "修改合同指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePContractFollowInteractionByField", Get = "", Post = "PContractFollowInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePContractFollowInteractionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePContractFollowInteractionByField(PContractFollowInteraction entity, string fields);

        [ServiceContractDescription(Name = "删除合同", Desc = "删除合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPContractFollowInteraction?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPContractFollowInteraction?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPContractFollowInteraction(long id);

        [ServiceContractDescription(Name = "查询合同", Desc = "查询合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractFollowInteraction", Get = "", Post = "PContractFollowInteraction", Return = "BaseResultList<PContractFollowInteraction>", ReturnType = "ListPContractFollowInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPContractFollowInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPContractFollowInteraction(PContractFollowInteraction entity);

        [ServiceContractDescription(Name = "查询合同(HQL)", Desc = "查询合同(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractFollowInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PContractFollowInteraction>", ReturnType = "ListPContractFollowInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPContractFollowInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPContractFollowInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询合同统计(HQL)", Desc = "查询合同统计(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractFollowInteractionTotalByHQL?fields={fields}&where={where}", Get = "fields={fields}&where={where}", Post = "", Return = "BaseResultList<PContractFollowInteraction>", ReturnType = "ListPContractFollowInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPContractFollowInteractionTotalByHQL?fields={fields}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPContractFollowInteractionTotalByHQL(string fields, string where);

        [ServiceContractDescription(Name = "通过主键ID查询合同", Desc = "通过主键ID查询合同", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPContractFollowInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PContractFollowInteraction>", ReturnType = "PContractFollowInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPContractFollowInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPContractFollowInteractionById(long id, string fields, bool isPlanish);


        #endregion

        #region PTaskTypeEmpLink

        [ServiceContractDescription(Name = "新增P_TaskTypeEmpLink", Desc = "新增P_TaskTypeEmpLink", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPTaskTypeEmpLink", Get = "", Post = "PTaskTypeEmpLink", Return = "BaseResultDataValue", ReturnType = "PTaskTypeEmpLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPTaskTypeEmpLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPTaskTypeEmpLink(PTaskTypeEmpLink entity);

        [ServiceContractDescription(Name = "修改P_TaskTypeEmpLink", Desc = "修改P_TaskTypeEmpLink", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskTypeEmpLink", Get = "", Post = "PTaskTypeEmpLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePTaskTypeEmpLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePTaskTypeEmpLink(PTaskTypeEmpLink entity);

        [ServiceContractDescription(Name = "修改P_TaskTypeEmpLink指定的属性", Desc = "修改P_TaskTypeEmpLink指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePTaskTypeEmpLinkByField", Get = "", Post = "PTaskTypeEmpLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePTaskTypeEmpLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePTaskTypeEmpLinkByField(PTaskTypeEmpLink entity, string fields);

        [ServiceContractDescription(Name = "删除P_TaskTypeEmpLink", Desc = "删除P_TaskTypeEmpLink", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPTaskTypeEmpLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPTaskTypeEmpLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPTaskTypeEmpLink(long id);

        [ServiceContractDescription(Name = "查询P_TaskTypeEmpLink", Desc = "查询P_TaskTypeEmpLink", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskTypeEmpLink", Get = "", Post = "PTaskTypeEmpLink", Return = "BaseResultList<PTaskTypeEmpLink>", ReturnType = "ListPTaskTypeEmpLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPTaskTypeEmpLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPTaskTypeEmpLink(PTaskTypeEmpLink entity);

        [ServiceContractDescription(Name = "查询P_TaskTypeEmpLink(HQL)", Desc = "查询P_TaskTypeEmpLink(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskTypeEmpLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PTaskTypeEmpLink>", ReturnType = "ListPTaskTypeEmpLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPTaskTypeEmpLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPTaskTypeEmpLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询P_TaskTypeEmpLink", Desc = "通过主键ID查询P_TaskTypeEmpLink", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskTypeEmpLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PTaskTypeEmpLink>", ReturnType = "PTaskTypeEmpLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPTaskTypeEmpLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPTaskTypeEmpLinkById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "根据Session中员工ID查询该人员所具有权限的字典树", Desc = "根据Session中员工ID查询该人员所具有权限的字典树", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPTaskTypeEmpLinkToTreeBySessionHREmpID?id={id}&where={where}", Get = "id={id}&where={where}", Post = "", Return = "BaseResultList<PTaskTypeEmpLink>", ReturnType = "PTaskTypeEmpLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPTaskTypeEmpLinkToTreeBySessionHREmpID?id={id}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPTaskTypeEmpLinkToTreeBySessionHREmpID(string id,string where);
        #endregion

        #region PInvoice

        [ServiceContractDescription(Name = "新增P_Invoice", Desc = "新增P_Invoice", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPInvoice", Get = "", Post = "PInvoice", Return = "BaseResultDataValue", ReturnType = "PInvoice")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPInvoice", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPInvoice(PInvoice entity);

        [ServiceContractDescription(Name = "修改P_Invoice", Desc = "修改P_Invoice", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePInvoice", Get = "", Post = "PInvoice", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePInvoice", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePInvoice(PInvoice entity);

        [ServiceContractDescription(Name = "修改P_Invoice指定的属性", Desc = "修改P_Invoice指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePInvoiceByField", Get = "", Post = "PInvoice", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePInvoiceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePInvoiceByField(PInvoice entity, string fields);

        [ServiceContractDescription(Name = "修改P_Invoice指定的属性", Desc = "修改P_Invoice指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePInvoiceByFieldManager", Get = "", Post = "PInvoice", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePInvoiceByFieldManager", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePInvoiceByFieldManager(PInvoice entity, string fields);

        [ServiceContractDescription(Name = "删除P_Invoice", Desc = "删除P_Invoice", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPInvoice?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPInvoice?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPInvoice(long id);

        [ServiceContractDescription(Name = "查询P_Invoice", Desc = "查询P_Invoice", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInvoice", Get = "", Post = "PInvoice", Return = "BaseResultList<PInvoice>", ReturnType = "ListPInvoice")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPInvoice", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPInvoice(PInvoice entity);

        [ServiceContractDescription(Name = "查询P_Invoice(HQL)", Desc = "查询P_Invoice(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInvoiceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PInvoice>", ReturnType = "ListPInvoice")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPInvoiceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPInvoiceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询P_Invoice", Desc = "通过主键ID查询P_Invoice", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInvoiceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PInvoice>", ReturnType = "PInvoice")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPInvoiceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPInvoiceById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询P_Invoice(HQL)带浏览类型", Desc = "查询P_Invoice(HQL)带浏览类型", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPInvoiceByExportType?ExportType={ExportType}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "ExportType={ExportType}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PInvoice>", ReturnType = "ListPInvoice")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPInvoiceByExportType?ExportType={ExportType}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPInvoiceByExportType(long ExportType, int page, int limit, string fields, string where, string sort, bool isPlanish);
        #endregion

        #region PFinanceReceive

        [ServiceContractDescription(Name = "新增P_Finance_Receive", Desc = "新增P_Finance_Receive", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPFinanceReceive", Get = "", Post = "PFinanceReceive", Return = "BaseResultDataValue", ReturnType = "PFinanceReceive")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPFinanceReceive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPFinanceReceive(PFinanceReceive entity);

        [ServiceContractDescription(Name = "修改P_Finance_Receive", Desc = "修改P_Finance_Receive", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePFinanceReceive", Get = "", Post = "PFinanceReceive", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePFinanceReceive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePFinanceReceive(PFinanceReceive entity);

        [ServiceContractDescription(Name = "修改P_Finance_Receive指定的属性", Desc = "修改P_Finance_Receive指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePFinanceReceiveByField", Get = "", Post = "PFinanceReceive", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePFinanceReceiveByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePFinanceReceiveByField(PFinanceReceive entity, string fields);

        [ServiceContractDescription(Name = "商务收款对比关联客户及付款单位修改指定的属性", Desc = "商务收款对比关联客户及付款单位修改指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePFinanceReceiveOfAssociateByField", Get = "", Post = "PFinanceReceive", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePFinanceReceiveOfAssociateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePFinanceReceiveOfAssociateByField(PFinanceReceive entity, string fields);

        [ServiceContractDescription(Name = "删除P_Finance_Receive", Desc = "删除P_Finance_Receive", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPFinanceReceive?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPFinanceReceive?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPFinanceReceive(long id);

        [ServiceContractDescription(Name = "查询P_Finance_Receive", Desc = "查询P_Finance_Receive", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPFinanceReceive", Get = "", Post = "PFinanceReceive", Return = "BaseResultList<PFinanceReceive>", ReturnType = "ListPFinanceReceive")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPFinanceReceive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPFinanceReceive(PFinanceReceive entity);

        [ServiceContractDescription(Name = "查询P_Finance_Receive(HQL)", Desc = "查询P_Finance_Receive(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPFinanceReceiveByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PFinanceReceive>", ReturnType = "ListPFinanceReceive")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPFinanceReceiveByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPFinanceReceiveByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询财务收款统计(HQL)", Desc = "查询财务收款统计(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPFinanceReceiveTotalByHQL?fields={fields}&where={where}", Get = "fields={fields}&where={where}", Post = "", Return = "BaseResultList<PFinanceReceive>", ReturnType = "ListPFinanceReceive")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPFinanceReceiveTotalByHQL?fields={fields}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPFinanceReceiveTotalByHQL(string fields, string where);

        [ServiceContractDescription(Name = "通过主键ID查询P_Finance_Receive", Desc = "通过主键ID查询P_Finance_Receive", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPFinanceReceiveById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PFinanceReceive>", ReturnType = "PFinanceReceive")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPFinanceReceiveById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPFinanceReceiveById(long id, string fields, bool isPlanish);
        #endregion

        #region PReceive

        [ServiceContractDescription(Name = "新增商务收款", Desc = "新增商务收款", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPReceive", Get = "", Post = "PReceive", Return = "BaseResultDataValue", ReturnType = "PReceive")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPReceive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPReceive(PReceive entity);

        [ServiceContractDescription(Name = "修改商务收款", Desc = "修改商务收款", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePReceive", Get = "", Post = "PReceive", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePReceive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePReceive(PReceive entity);

        [ServiceContractDescription(Name = "修改商务收款指定的属性", Desc = "修改商务收款指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePReceiveByField", Get = "", Post = "PReceive", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePReceiveByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePReceiveByField(PReceive entity, string fields);


        [ServiceContractDescription(Name = "撤回新增商务收款", Desc = "撤回新增商务收款", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddBackPReceive", Get = "", Post = "PReceive", Return = "BaseResultDataValue", ReturnType = "PReceive")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBackPReceive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBackPReceive(PReceive entity);

        [ServiceContractDescription(Name = "删除商务收款", Desc = "删除商务收款", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPReceive?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPReceive?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPReceive(long id);

        [ServiceContractDescription(Name = "查询商务收款", Desc = "查询商务收款", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceive", Get = "", Post = "PReceive", Return = "BaseResultList<PReceive>", ReturnType = "ListPReceive")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPReceive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPReceive(PReceive entity);

        [ServiceContractDescription(Name = "查询商务收款(HQL)", Desc = "查询商务收款(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceiveByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PReceive>", ReturnType = "ListPReceive")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPReceiveByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPReceiveByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询商务收款(HQL)", Desc = "查询商务收款(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AdvSearchPReceiveByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PReceive>", ReturnType = "ListPReceive")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AdvSearchPReceiveByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AdvSearchPReceiveByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询统计商务收款(HQL)", Desc = "查询统计商务收款(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceiveTotalByHQL?fields={fields}&where={where}", Get = "fields={fields}&where={where}", Post = "", Return = "BaseResultList<PReceive>", ReturnType = "ListPReceive")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPReceiveTotalByHQL?fields={fields}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPReceiveTotalByHQL(string fields, string where);

        [ServiceContractDescription(Name = "通过主键ID查询商务收款", Desc = "通过主键ID查询商务收款", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceiveById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PReceive>", ReturnType = "PReceive")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPReceiveById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPReceiveById(long id, string fields, bool isPlanish);
        #endregion

        #region PReceivePlan

        [ServiceContractDescription(Name = "新增收款计划", Desc = "新增收款计划", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPReceivePlan", Get = "", Post = "PReceivePlan", Return = "BaseResultDataValue", ReturnType = "PReceivePlan")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPReceivePlan", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPReceivePlan(PReceivePlan entity);

        [ServiceContractDescription(Name = "修改收款计划", Desc = "修改收款计划", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePReceivePlan", Get = "", Post = "PReceivePlan", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePReceivePlan", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePReceivePlan(PReceivePlan entity);

        [ServiceContractDescription(Name = "修改收款计划指定的属性", Desc = "修改收款计划指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePReceivePlanByField", Get = "", Post = "PReceivePlan", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePReceivePlanByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePReceivePlanByField(PReceivePlan entity, string fields);

        [ServiceContractDescription(Name = "收款计划变更", Desc = "收款计划变更", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_ChangeApplyPReceivePlan", Get = "", Post = "PReceivePlan", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ChangeApplyPReceivePlan", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_ChangeApplyPReceivePlan(List<PReceivePlan> entity, long PPReceivePlanID);

        [ServiceContractDescription(Name = "收款计划变更审核通过", Desc = "收款计划变更审核通过", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_ChangeSubmitPReceivePlan?PPReceivePlanID={PPReceivePlanID}", Get = "PPReceivePlanID={PPReceivePlanID}", Post = "", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ChangeSubmitPReceivePlan?PPReceivePlanID={PPReceivePlanID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_ChangeSubmitPReceivePlan(long PPReceivePlanID);

        [ServiceContractDescription(Name = "收款计划变更审核退回", Desc = "收款计划变更审核退回", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UnChangeSubmitPReceivePlan?PPReceivePlanID={PPReceivePlanID}", Get = "PPReceivePlanID={PPReceivePlanID}", Post = "", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UnChangeSubmitPReceivePlan?PPReceivePlanID={PPReceivePlanID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_UnChangeSubmitPReceivePlan(long PPReceivePlanID);

        [ServiceContractDescription(Name = "删除收款计划", Desc = "删除收款计划", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPReceivePlan?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPReceivePlan?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPReceivePlan(long id);

        [ServiceContractDescription(Name = "查询收款计划", Desc = "查询收款计划", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceivePlan", Get = "", Post = "PReceivePlan", Return = "BaseResultList<PReceivePlan>", ReturnType = "ListPReceivePlan")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPReceivePlan", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPReceivePlan(PReceivePlan entity);

        [ServiceContractDescription(Name = "查询收款计划(HQL)", Desc = "查询收款计划(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceivePlanByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PReceivePlan>", ReturnType = "ListPReceivePlan")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPReceivePlanByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPReceivePlanByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询收款计划树(HQL)", Desc = "查询收款计划树(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceivePlanTreeByHQL?fields={fields}&where={where}", Get = "fields={fields}&where={where}", Post = "", Return = "BaseResultTree<PReceivePlan>", ReturnType = "ListPReceivePlan")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPReceivePlanTreeByHQL?fields={fields}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPReceivePlanTreeByHQL(string fields, string where);

        [ServiceContractDescription(Name = "查询收款计划树(HQL)", Desc = "查询收款计划树(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AdvSearchPReceivePlanTreeByHQL?fields={fields}&where={where}", Get = "fields={fields}&where={where}", Post = "", Return = "BaseResultTree<PReceivePlan>", ReturnType = "ListPReceivePlan")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AdvSearchPReceivePlanTreeByHQL?fields={fields}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AdvSearchPReceivePlanTreeByHQL(string fields, string where);

        [ServiceContractDescription(Name = "查询统计收款计划(HQL)", Desc = "查询统计收款计划(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AdvSearchTotalPReceivePlanByHQL?fields={fields}&where={where}", Get = "fields={fields}&where={where}", Post = "", Return = "BaseResultTree<PReceivePlan>", ReturnType = "ListPReceivePlan")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AdvSearchTotalPReceivePlanByHQL?fields={fields}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AdvSearchTotalPReceivePlanByHQL(string fields, string where);

        [ServiceContractDescription(Name = "通过主键ID查询收款计划", Desc = "通过主键ID查询收款计划", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPReceivePlanById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PReceivePlan>", ReturnType = "PReceivePlan")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPReceivePlanById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPReceivePlanById(long id, string fields, bool isPlanish);
        #endregion

        #region PRepayment

        [ServiceContractDescription(Name = "新增还款记录", Desc = "新增还款记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPRepayment", Get = "", Post = "PRepayment", Return = "BaseResultDataValue", ReturnType = "PRepayment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPRepayment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPRepayment(PRepayment entity);

        [ServiceContractDescription(Name = "修改还款记录", Desc = "修改还款记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePRepayment", Get = "", Post = "PRepayment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePRepayment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePRepayment(PRepayment entity);

        [ServiceContractDescription(Name = "修改还款记录指定的属性", Desc = "修改还款记录指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePRepaymentByField", Get = "", Post = "PRepayment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePRepaymentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePRepaymentByField(PRepayment entity, string fields);

        [ServiceContractDescription(Name = "删除还款记录", Desc = "删除还款记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPRepayment?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPRepayment?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPRepayment(long id);

        [ServiceContractDescription(Name = "查询还款记录", Desc = "查询还款记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPRepayment", Get = "", Post = "PRepayment", Return = "BaseResultList<PRepayment>", ReturnType = "ListPRepayment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPRepayment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPRepayment(PRepayment entity);

        [ServiceContractDescription(Name = "查询还款记录(HQL)", Desc = "查询还款记录(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPRepaymentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PRepayment>", ReturnType = "ListPRepayment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPRepaymentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPRepaymentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询还款记录", Desc = "通过主键ID查询还款记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPRepaymentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PRepayment>", ReturnType = "PRepayment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPRepaymentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPRepaymentById(long id, string fields, bool isPlanish);
        #endregion

        #region PLoanBill

        [ServiceContractDescription(Name = "新增借款单管理", Desc = "新增借款单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPLoanBill", Get = "", Post = "PLoanBill", Return = "BaseResultDataValue", ReturnType = "PLoanBill")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPLoanBill", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPLoanBill(PLoanBill entity);

        [ServiceContractDescription(Name = "修改借款单管理", Desc = "修改借款单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePLoanBill", Get = "", Post = "PLoanBill", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePLoanBill", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePLoanBill(PLoanBill entity);

        [ServiceContractDescription(Name = "修改借款单管理指定的属性", Desc = "修改借款单管理指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePLoanBillByField", Get = "", Post = "PLoanBill", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePLoanBillByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePLoanBillByField(PLoanBill entity, string fields);

        [ServiceContractDescription(Name = "删除借款单管理", Desc = "删除借款单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPLoanBill?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPLoanBill?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPLoanBill(long id);

        [ServiceContractDescription(Name = "查询借款单管理", Desc = "查询借款单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPLoanBill", Get = "", Post = "PLoanBill", Return = "BaseResultList<PLoanBill>", ReturnType = "ListPLoanBill")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPLoanBill", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPLoanBill(PLoanBill entity);

        [ServiceContractDescription(Name = "查询借款单管理NoPlanish(HQL)", Desc = "查询借款单管理NoPlanish(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPLoanBillNoPlanishByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}", Post = "", Return = "BaseResultList<PLoanBill>", ReturnType = "ListPLoanBill")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPLoanBillNoPlanishByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPLoanBillNoPlanishByHQL(int page, int limit, string fields, string where, string sort);

        [ServiceContractDescription(Name = "查询借款单管理(HQL)", Desc = "查询借款单管理(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPLoanBillByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PLoanBill>", ReturnType = "ListPLoanBill")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPLoanBillByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPLoanBillByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询借款单管理", Desc = "通过主键ID查询借款单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPLoanBillById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PLoanBill>", ReturnType = "PLoanBill")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPLoanBillById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPLoanBillById(long id, string fields, bool isPlanish);
        #endregion

        #region PEmpFinanceAccount

        [ServiceContractDescription(Name = "新增员工财务账户", Desc = "新增员工财务账户", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPEmpFinanceAccount", Get = "", Post = "PEmpFinanceAccount", Return = "BaseResultDataValue", ReturnType = "PEmpFinanceAccount")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPEmpFinanceAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPEmpFinanceAccount(PEmpFinanceAccount entity);

        [ServiceContractDescription(Name = "修改员工财务账户", Desc = "修改员工财务账户", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePEmpFinanceAccount", Get = "", Post = "PEmpFinanceAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePEmpFinanceAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePEmpFinanceAccount(PEmpFinanceAccount entity);

        [ServiceContractDescription(Name = "修改员工财务账户指定的属性", Desc = "修改员工财务账户指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePEmpFinanceAccountByField", Get = "", Post = "PEmpFinanceAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePEmpFinanceAccountByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePEmpFinanceAccountByField(PEmpFinanceAccount entity, string fields);

        [ServiceContractDescription(Name = "删除员工财务账户", Desc = "删除员工财务账户", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPEmpFinanceAccount?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPEmpFinanceAccount?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPEmpFinanceAccount(long id);

        [ServiceContractDescription(Name = "查询员工财务账户", Desc = "查询员工财务账户", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPEmpFinanceAccount", Get = "", Post = "PEmpFinanceAccount", Return = "BaseResultList<PEmpFinanceAccount>", ReturnType = "ListPEmpFinanceAccount")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPEmpFinanceAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPEmpFinanceAccount(PEmpFinanceAccount entity);

        [ServiceContractDescription(Name = "查询员工财务账户(HQL)", Desc = "查询员工财务账户(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPEmpFinanceAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PEmpFinanceAccount>", ReturnType = "ListPEmpFinanceAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPEmpFinanceAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPEmpFinanceAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询员工财务账户", Desc = "通过主键ID查询员工财务账户", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPEmpFinanceAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PEmpFinanceAccount>", ReturnType = "PEmpFinanceAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPEmpFinanceAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPEmpFinanceAccountById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过部门ID查询员工财务账户", Desc = "通过部门ID查询员工财务账户", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPEmpFinanceAccountByDeptId?deptid={deptid}&isincludesubdept={isincludesubdept}", Get = "deptid={deptid}&isincludesubdept={isincludesubdept}", Post = "", Return = "BaseResultList<PEmpFinanceAccount>", ReturnType = "ATEmpAttendanceEventParaSettings")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPEmpFinanceAccountByDeptId?deptid={deptid}&isincludesubdept={isincludesubdept}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPEmpFinanceAccountByDeptId(string deptid, bool isincludesubdept);
        #endregion

        #region PExpenseAccount

        [ServiceContractDescription(Name = "新增报销单管理", Desc = "新增报销单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPExpenseAccount", Get = "", Post = "PExpenseAccount", Return = "BaseResultDataValue", ReturnType = "PExpenseAccount")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPExpenseAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPExpenseAccount(PExpenseAccount entity);

        [ServiceContractDescription(Name = "修改报销单管理", Desc = "修改报销单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePExpenseAccount", Get = "", Post = "PExpenseAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePExpenseAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePExpenseAccount(PExpenseAccount entity);

        [ServiceContractDescription(Name = "修改报销单管理指定的属性", Desc = "修改报销单管理指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePExpenseAccountByField", Get = "", Post = "PExpenseAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePExpenseAccountByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePExpenseAccountByField(PExpenseAccount entity, string fields);

        [ServiceContractDescription(Name = "删除报销单管理", Desc = "删除报销单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPExpenseAccount?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPExpenseAccount?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPExpenseAccount(long id);

        [ServiceContractDescription(Name = "查询报销单管理", Desc = "查询报销单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPExpenseAccount", Get = "", Post = "PExpenseAccount", Return = "BaseResultList<PExpenseAccount>", ReturnType = "ListPExpenseAccount")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPExpenseAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPExpenseAccount(PExpenseAccount entity);

        [ServiceContractDescription(Name = "查询报销单管理(HQL)", Desc = "查询报销单管理(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPExpenseAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PExpenseAccount>", ReturnType = "ListPExpenseAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPExpenseAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPExpenseAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询报销单管理", Desc = "通过主键ID查询报销单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPExpenseAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PExpenseAccount>", ReturnType = "PExpenseAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPExpenseAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPExpenseAccountById(long id, string fields, bool isPlanish);
        #endregion

        #region PCustomerServiceOperation

        [ServiceContractDescription(Name = "新增服务单处理记录", Desc = "新增服务单处理记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPCustomerServiceOperation", Get = "", Post = "PCustomerServiceOperation", Return = "BaseResultDataValue", ReturnType = "PCustomerServiceOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPCustomerServiceOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPCustomerServiceOperation(PCustomerServiceOperation entity);

        [ServiceContractDescription(Name = "修改服务单处理记录", Desc = "修改服务单处理记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePCustomerServiceOperation", Get = "", Post = "PCustomerServiceOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePCustomerServiceOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePCustomerServiceOperation(PCustomerServiceOperation entity);

        [ServiceContractDescription(Name = "修改服务单处理记录指定的属性", Desc = "修改服务单处理记录指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePCustomerServiceOperationByField", Get = "", Post = "PCustomerServiceOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePCustomerServiceOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePCustomerServiceOperationByField(PCustomerServiceOperation entity, string fields);

        [ServiceContractDescription(Name = "删除服务单处理记录", Desc = "删除服务单处理记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPCustomerServiceOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPCustomerServiceOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPCustomerServiceOperation(long id);

        [ServiceContractDescription(Name = "查询服务单处理记录", Desc = "查询服务单处理记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceOperation", Get = "", Post = "PCustomerServiceOperation", Return = "BaseResultList<PCustomerServiceOperation>", ReturnType = "ListPCustomerServiceOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPCustomerServiceOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPCustomerServiceOperation(PCustomerServiceOperation entity);

        [ServiceContractDescription(Name = "查询服务单处理记录(HQL)", Desc = "查询服务单处理记录(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PCustomerServiceOperation>", ReturnType = "ListPCustomerServiceOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPCustomerServiceOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPCustomerServiceOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询服务单处理记录", Desc = "通过主键ID查询服务单处理记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PCustomerServiceOperation>", ReturnType = "PCustomerServiceOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPCustomerServiceOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPCustomerServiceOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region PCustomerServiceAttachment
        [ServiceContractDescription(Name = "上传服务单附件服务", Desc = "上传服务单附件服务", Url = "ProjectProgressMonitorManageService.svc/SC_UploadAddPCustomerServiceAttachment", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UploadAddPCustomerServiceAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message SC_UploadAddPCustomerServiceAttachment();

        [ServiceContractDescription(Name = "下载服务单附件服务", Desc = "下载服务单附件服务", Url = "ProjectProgressMonitorManageService.svc/SC_UDTO_DownLoadPCustomerServiceAttachment?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_DownLoadPCustomerServiceAttachment?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream SC_UDTO_DownLoadPCustomerServiceAttachment(long id, long operateType);

        [ServiceContractDescription(Name = "新增服务单附件", Desc = "新增服务单附件", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPCustomerServiceAttachment", Get = "", Post = "PCustomerServiceAttachment", Return = "BaseResultDataValue", ReturnType = "PCustomerServiceAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPCustomerServiceAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPCustomerServiceAttachment(PCustomerServiceAttachment entity);

        [ServiceContractDescription(Name = "修改服务单附件", Desc = "修改服务单附件", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePCustomerServiceAttachment", Get = "", Post = "PCustomerServiceAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePCustomerServiceAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePCustomerServiceAttachment(PCustomerServiceAttachment entity);

        [ServiceContractDescription(Name = "修改服务单附件指定的属性", Desc = "修改服务单附件指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePCustomerServiceAttachmentByField", Get = "", Post = "PCustomerServiceAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePCustomerServiceAttachmentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePCustomerServiceAttachmentByField(PCustomerServiceAttachment entity, string fields);

        [ServiceContractDescription(Name = "删除服务单附件", Desc = "删除服务单附件", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPCustomerServiceAttachment?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPCustomerServiceAttachment?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPCustomerServiceAttachment(long id);

        [ServiceContractDescription(Name = "查询服务单附件", Desc = "查询服务单附件", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceAttachment", Get = "", Post = "PCustomerServiceAttachment", Return = "BaseResultList<PCustomerServiceAttachment>", ReturnType = "ListPCustomerServiceAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPCustomerServiceAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPCustomerServiceAttachment(PCustomerServiceAttachment entity);

        [ServiceContractDescription(Name = "查询服务单附件(HQL)", Desc = "查询服务单附件(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PCustomerServiceAttachment>", ReturnType = "ListPCustomerServiceAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPCustomerServiceAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPCustomerServiceAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询服务单附件", Desc = "通过主键ID查询服务单附件", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PCustomerServiceAttachment>", ReturnType = "PCustomerServiceAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPCustomerServiceAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPCustomerServiceAttachmentById(long id, string fields, bool isPlanish);
        #endregion

        #region PCustomerService

        [ServiceContractDescription(Name = "新增服务单管理", Desc = "新增服务单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPCustomerService", Get = "", Post = "PCustomerService", Return = "BaseResultDataValue", ReturnType = "PCustomerService")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPCustomerService", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPCustomerService(PCustomerService entity);

        [ServiceContractDescription(Name = "修改服务单管理", Desc = "修改服务单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePCustomerService", Get = "", Post = "PCustomerService", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePCustomerService", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePCustomerService(PCustomerService entity);

        [ServiceContractDescription(Name = "修改服务单管理指定的属性", Desc = "修改服务单管理指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePCustomerServiceByField", Get = "", Post = "PCustomerService", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePCustomerServiceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePCustomerServiceByField(PCustomerService entity, string fields);

        [ServiceContractDescription(Name = "删除服务单管理", Desc = "删除服务单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPCustomerService?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPCustomerService?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPCustomerService(long id);

        [ServiceContractDescription(Name = "查询服务单管理", Desc = "查询服务单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerService", Get = "", Post = "PCustomerService", Return = "BaseResultList<PCustomerService>", ReturnType = "ListPCustomerService")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPCustomerService", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPCustomerService(PCustomerService entity);

        [ServiceContractDescription(Name = "查询服务单管理(HQL)", Desc = "查询服务单管理(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PCustomerService>", ReturnType = "ListPCustomerService")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPCustomerServiceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPCustomerServiceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询服务单管理", Desc = "通过主键ID查询服务单管理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PCustomerService>", ReturnType = "PCustomerService")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPCustomerServiceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPCustomerServiceById(long id, string fields, bool isPlanish);
        #endregion

        #region PSalesManClientLink

        [ServiceContractDescription(Name = "新增销售人员同客户关系表", Desc = "新增销售人员同客户关系表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPSalesManClientLink", Get = "", Post = "PSalesManClientLink", Return = "BaseResultDataValue", ReturnType = "PSalesManClientLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPSalesManClientLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPSalesManClientLink(PSalesManClientLink entity);

        [ServiceContractDescription(Name = "修改销售人员同客户关系表", Desc = "修改销售人员同客户关系表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePSalesManClientLink", Get = "", Post = "PSalesManClientLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePSalesManClientLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePSalesManClientLink(PSalesManClientLink entity);

        [ServiceContractDescription(Name = "修改销售人员同客户关系表指定的属性", Desc = "修改销售人员同客户关系表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePSalesManClientLinkByField", Get = "", Post = "PSalesManClientLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePSalesManClientLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePSalesManClientLinkByField(PSalesManClientLink entity, string fields);

        [ServiceContractDescription(Name = "删除销售人员同客户关系表", Desc = "删除销售人员同客户关系表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPSalesManClientLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPSalesManClientLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPSalesManClientLink(long id);

        [ServiceContractDescription(Name = "查询销售人员同客户关系表", Desc = "查询销售人员同客户关系表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPSalesManClientLink", Get = "", Post = "PSalesManClientLink", Return = "BaseResultList<PSalesManClientLink>", ReturnType = "ListPSalesManClientLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPSalesManClientLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPSalesManClientLink(PSalesManClientLink entity);

        [ServiceContractDescription(Name = "查询销售人员同客户关系表(HQL)", Desc = "查询销售人员同客户关系表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPSalesManClientLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PSalesManClientLink>", ReturnType = "ListPSalesManClientLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPSalesManClientLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPSalesManClientLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询销售人员同客户关系表", Desc = "通过主键ID查询销售人员同客户关系表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPSalesManClientLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PSalesManClientLink>", ReturnType = "PSalesManClientLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPSalesManClientLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPSalesManClientLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region PCustomerServiceOperationLog

        [ServiceContractDescription(Name = "新增服务单操作记录表", Desc = "新增服务单操作记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPCustomerServiceOperationLog", Get = "", Post = "PCustomerServiceOperationLog", Return = "BaseResultDataValue", ReturnType = "PCustomerServiceOperationLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPCustomerServiceOperationLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPCustomerServiceOperationLog(PCustomerServiceOperationLog entity);

        [ServiceContractDescription(Name = "修改服务单操作记录表", Desc = "修改服务单操作记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePCustomerServiceOperationLog", Get = "", Post = "PCustomerServiceOperationLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePCustomerServiceOperationLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePCustomerServiceOperationLog(PCustomerServiceOperationLog entity);

        [ServiceContractDescription(Name = "修改服务单操作记录表指定的属性", Desc = "修改服务单操作记录表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePCustomerServiceOperationLogByField", Get = "", Post = "PCustomerServiceOperationLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePCustomerServiceOperationLogByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePCustomerServiceOperationLogByField(PCustomerServiceOperationLog entity, string fields);

        [ServiceContractDescription(Name = "删除服务单操作记录表", Desc = "删除服务单操作记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPCustomerServiceOperationLog?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPCustomerServiceOperationLog?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPCustomerServiceOperationLog(long id);

        [ServiceContractDescription(Name = "查询服务单操作记录表", Desc = "查询服务单操作记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceOperationLog", Get = "", Post = "PCustomerServiceOperationLog", Return = "BaseResultList<PCustomerServiceOperationLog>", ReturnType = "ListPCustomerServiceOperationLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPCustomerServiceOperationLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPCustomerServiceOperationLog(PCustomerServiceOperationLog entity);

        [ServiceContractDescription(Name = "查询服务单操作记录表(HQL)", Desc = "查询服务单操作记录表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceOperationLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PCustomerServiceOperationLog>", ReturnType = "ListPCustomerServiceOperationLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPCustomerServiceOperationLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPCustomerServiceOperationLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询服务单操作记录表", Desc = "通过主键ID查询服务单操作记录表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPCustomerServiceOperationLogById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PCustomerServiceOperationLog>", ReturnType = "PCustomerServiceOperationLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPCustomerServiceOperationLogById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPCustomerServiceOperationLogById(long id, string fields, bool isPlanish);
        #endregion


        #region 上传文件

        [ServiceContractDescription(Name = "上传文件DEMO", Desc = "上传文件DEMO", Url = "ProjectProgressMonitorManageService.svc/WM_UploadDemo", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WM_UploadDemo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message WM_UploadDemo();

        [ServiceContractDescription(Name = "上传附件保存服务", Desc = "上传附件保存服务", Url = "ProjectProgressMonitorManageService.svc/WM_UploadNewFiles", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WM_UploadNewFiles", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message WM_UploadNewFiles();

        [ServiceContractDescription(Name = "下载项目监控的附件表的文件", Desc = "下载项目监控的附件表的文件", Url = "ProjectProgressMonitorManageService.svc/WM_UDTO_PProjectAttachmentDownLoadFiles?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WM_UDTO_PProjectAttachmentDownLoadFiles?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream WM_UDTO_PProjectAttachmentDownLoadFiles(long id, long operateType);

        #endregion

        #region BDictTree

        [ServiceContractDescription(Name = "新增类型树", Desc = "新增类型树", Url = "ProjectProgressMonitorManageService.svc/UDTO_AddBDictTree", Get = "", Post = "BDictTree", Return = "BaseResultDataValue", ReturnType = "BDictTree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UDTO_AddBDictTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UDTO_AddBDictTree(BDictTree entity);

        [ServiceContractDescription(Name = "修改类型树", Desc = "修改类型树", Url = "ProjectProgressMonitorManageService.svc/UDTO_UpdateBDictTree", Get = "", Post = "BDictTree", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UDTO_UpdateBDictTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool UDTO_UpdateBDictTree(BDictTree entity);

        [ServiceContractDescription(Name = "修改类型树指定的属性", Desc = "修改类型树指定的属性", Url = "ProjectProgressMonitorManageService.svc/UDTO_UpdateBDictTreeByField", Get = "", Post = "BDictTree", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UDTO_UpdateBDictTreeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool UDTO_UpdateBDictTreeByField(BDictTree entity, string fields);

        [ServiceContractDescription(Name = "删除类型树", Desc = "删除类型树", Url = "ProjectProgressMonitorManageService.svc/UDTO_DelBDictTree?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/UDTO_DelBDictTree?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool UDTO_DelBDictTree(long id);

        [ServiceContractDescription(Name = "查询类型树", Desc = "查询类型树", Url = "ProjectProgressMonitorManageService.svc/UDTO_SearchBDictTree", Get = "", Post = "BDictTree", Return = "BaseResultList<BDictTree>", ReturnType = "ListBDictTree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchBDictTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UDTO_SearchBDictTree(BDictTree entity);

        [ServiceContractDescription(Name = "查询类型树(HQL)", Desc = "查询类型树(HQL)", Url = "ProjectProgressMonitorManageService.svc/UDTO_SearchBDictTreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDictTree>", ReturnType = "ListBDictTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UDTO_SearchBDictTreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UDTO_SearchBDictTreeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询类型树", Desc = "通过主键ID查询类型树", Url = "ProjectProgressMonitorManageService.svc/UDTO_SearchBDictTreeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDictTree>", ReturnType = "BDictTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UDTO_SearchBDictTreeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UDTO_SearchBDictTreeById(long id, string fields, bool isPlanish);
        #endregion

        #region 文件类型树
        [ServiceContractDescription(Name = "根据类型树id(支持传入多个值)及快捷码获取类型树", Desc = "根据类型树id(支持传入多个值)及快捷码获取类型树", Url = "ProjectProgressMonitorManageService.svc/UDTO_SearchBDictTreeListTreeByIdListStr?id={id}&idListStr={idListStr}&fields={fields}&maxLevelStr={maxLevelStr}", Get = "id={id}&idListStr={idListStr}&fields={fields}&maxLevelStr={maxLevelStr}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeBBDictTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/UDTO_SearchBDictTreeListTreeByIdListStr?id={id}&idListStr={idListStr}&fields={fields}&maxLevelStr={maxLevelStr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UDTO_SearchBDictTreeListTreeByIdListStr(string id, string idListStr, string fields, string maxLevelStr);
        #endregion

        [ServiceContractDescription(Name = "查询任务表(HQL)", Desc = "查询任务表(HQL)", Url = "ProjectProgressMonitorManageService.svc/PushWeiXinMessageTest?openid={openid}&first={first}&remark={remark}", Get = "openid={openid}&first={first}&remark={remark}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PushWeiXinMessageTest?openid={openid}&first={first}&remark={remark}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue PushWeiXinMessageTest(string openid, string first, string remark);

        #region 借款单打印
        [ServiceContractDescription(Name = "预览借款单PDF文件", Desc = "预览借款单PDF文件", Url = "ProjectProgressMonitorManageService.svc/PLoanBill_UDTO_PreviewPdf?id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}", Get = "id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/PLoanBill_UDTO_PreviewPdf?id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}")]
        [OperationContract]
        Stream PLoanBill_UDTO_PreviewPdf(long id, int operateType, bool isPreview, string templetName);
        #endregion

        #region 发票申请打印
        [ServiceContractDescription(Name = "预览发票申请PDF文件", Desc = "预览发票申请PDF文件", Url = "ProjectProgressMonitorManageService.svc/PInvoice_UDTO_PreviewPdf?id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}", Get = "id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/PInvoice_UDTO_PreviewPdf?id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}")]
        [OperationContract]
        Stream PInvoice_UDTO_PreviewPdf(long id, int operateType, bool isPreview, string templetName);
        #endregion

        #region 合同评审打印
        [ServiceContractDescription(Name = "预览合同评审PDF文件", Desc = "预览合同评审PDF文件", Url = "ProjectProgressMonitorManageService.svc/PContract_UDTO_PreviewPdf?id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}", Get = "id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/PContract_UDTO_PreviewPdf?id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}")]
        [OperationContract]
        Stream PContract_UDTO_PreviewPdf(long id, int operateType, bool isPreview, string templetName);
        #endregion

        #region 授权

        #region AHOperation

        [ServiceContractDescription(Name = "新增授权操作记录", Desc = "新增授权操作记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddAHOperation", Get = "", Post = "AHOperation", Return = "BaseResultDataValue", ReturnType = "AHOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddAHOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddAHOperation(AHOperation entity);

        [ServiceContractDescription(Name = "修改授权操作记录", Desc = "修改授权操作记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHOperation", Get = "", Post = "AHOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateAHOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateAHOperation(AHOperation entity);

        [ServiceContractDescription(Name = "修改授权操作记录指定的属性", Desc = "修改授权操作记录指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHOperationByField", Get = "", Post = "AHOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateAHOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateAHOperationByField(AHOperation entity, string fields);

        [ServiceContractDescription(Name = "删除授权操作记录", Desc = "删除授权操作记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelAHOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelAHOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelAHOperation(long id);

        [ServiceContractDescription(Name = "查询授权操作记录", Desc = "查询授权操作记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHOperation", Get = "", Post = "AHOperation", Return = "BaseResultList<AHOperation>", ReturnType = "ListAHOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHOperation(AHOperation entity);

        [ServiceContractDescription(Name = "查询授权操作记录(HQL)", Desc = "查询授权操作记录(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AHOperation>", ReturnType = "ListAHOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询授权操作记录", Desc = "通过主键ID查询授权操作记录", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AHOperation>", ReturnType = "AHOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region AHServerEquipLicence

        [ServiceContractDescription(Name = "新增服务器仪器授权", Desc = "新增服务器仪器授权", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddAHServerEquipLicence", Get = "", Post = "AHServerEquipLicence", Return = "BaseResultDataValue", ReturnType = "AHServerEquipLicence")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddAHServerEquipLicence", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddAHServerEquipLicence(AHServerEquipLicence entity);

        [ServiceContractDescription(Name = "修改服务器仪器授权", Desc = "修改服务器仪器授权", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHServerEquipLicence", Get = "", Post = "AHServerEquipLicence", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateAHServerEquipLicence", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateAHServerEquipLicence(AHServerEquipLicence entity);

        [ServiceContractDescription(Name = "修改服务器仪器授权指定的属性", Desc = "修改服务器仪器授权指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHServerEquipLicenceByField", Get = "", Post = "AHServerEquipLicence", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateAHServerEquipLicenceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateAHServerEquipLicenceByField(AHServerEquipLicence entity, string fields);

        [ServiceContractDescription(Name = "删除服务器仪器授权", Desc = "删除服务器仪器授权", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelAHServerEquipLicence?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelAHServerEquipLicence?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelAHServerEquipLicence(long id);

        [ServiceContractDescription(Name = "查询服务器仪器授权", Desc = "查询服务器仪器授权", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerEquipLicence", Get = "", Post = "AHServerEquipLicence", Return = "BaseResultList<AHServerEquipLicence>", ReturnType = "ListAHServerEquipLicence")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHServerEquipLicence", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHServerEquipLicence(AHServerEquipLicence entity);

        [ServiceContractDescription(Name = "查询服务器仪器授权(HQL)", Desc = "查询服务器仪器授权(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerEquipLicenceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AHServerEquipLicence>", ReturnType = "ListAHServerEquipLicence")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHServerEquipLicenceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHServerEquipLicenceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询服务器仪器授权", Desc = "通过主键ID查询服务器仪器授权", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerEquipLicenceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AHServerEquipLicence>", ReturnType = "AHServerEquipLicence")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHServerEquipLicenceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHServerEquipLicenceById(long id, string fields, bool isPlanish);
        #endregion

        #region AHServerLicence

        [ServiceContractDescription(Name = "通过FormData方式上传服务器授权文件", Desc = "通过FormData方式上传服务器授权文件", Url = "ProjectProgressMonitorManageService.svc/ST_UploadAHServerLicenceFile", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UploadAHServerLicenceFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UploadAHServerLicenceFile();

        [ServiceContractDescription(Name = "下载服务器授权文件", Desc = "下载服务器授权文件", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DownLoadAHServerLicenceFile?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_DownLoadAHServerLicenceFile?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_DownLoadAHServerLicenceFile(long id, long operateType);

        [ServiceContractDescription(Name = "新增服务器授权申请及申请程序明细和申请仪器明细", Desc = "新增服务器授权申请及申请程序明细和申请仪器明细", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddAHAHServerLicenceAndDetails", Get = "", Post = "ApplyAHServerLicence", Return = "BaseResultDataValue", ReturnType = "ApplyAHServerLicence")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddAHAHServerLicenceAndDetails", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddAHAHServerLicenceAndDetails(Entity.OA.ViewObject.Response.ApplyAHServerLicence entity);

        [ServiceContractDescription(Name = "新增服务器授权", Desc = "新增服务器授权", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddAHServerLicence", Get = "", Post = "AHServerLicence", Return = "BaseResultDataValue", ReturnType = "AHServerLicence")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddAHServerLicence", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddAHServerLicence(AHServerLicence entity);

        [ServiceContractDescription(Name = "修改服务器授权信息及明细信息(包括手工追加的程序明细)", Desc = "修改服务器授权信息及明细信息(包括手工追加的程序明细)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHServerLicenceAndDetailsByField", Get = "", Post = "ApplyAHServerLicence", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateAHServerLicenceAndDetailsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateAHServerLicenceAndDetailsByField(Entity.OA.ViewObject.Response.ApplyAHServerLicence entity, string fields);

        [ServiceContractDescription(Name = "修改服务器授权", Desc = "修改服务器授权", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHServerLicence", Get = "", Post = "AHServerLicence", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateAHServerLicence", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateAHServerLicence(AHServerLicence entity);

        [ServiceContractDescription(Name = "修改服务器授权指定的属性", Desc = "修改服务器授权指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHServerLicenceByField", Get = "", Post = "AHServerLicence", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateAHServerLicenceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateAHServerLicenceByField(AHServerLicence entity, string fields);

        [ServiceContractDescription(Name = "删除服务器授权", Desc = "删除服务器授权", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelAHServerLicence?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelAHServerLicence?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelAHServerLicence(long id);

        [ServiceContractDescription(Name = "查询服务器授权", Desc = "查询服务器授权", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerLicence", Get = "", Post = "AHServerLicence", Return = "BaseResultList<AHServerLicence>", ReturnType = "ListAHServerLicence")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHServerLicence", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHServerLicence(AHServerLicence entity);

        [ServiceContractDescription(Name = "查询服务器授权(HQL)", Desc = "查询服务器授权(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerLicenceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AHServerLicence>", ReturnType = "ListAHServerLicence")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHServerLicenceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHServerLicenceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询服务器授权(支持主单查询条件及仪器明细查询条件)", Desc = "查询服务器授权(支持主单查询条件及仪器明细查询条件)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerLicenceByDocAndDtlHQL?page={page}&limit={limit}&fields={fields}&where={where}&dtlWhere={dtlWhere}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&dtlWhere={dtlWhere}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AHServerLicence>", ReturnType = "ListAHServerLicence")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHServerLicenceByDocAndDtlHQL?page={page}&limit={limit}&fields={fields}&where={where}&dtlWhere={dtlWhere}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHServerLicenceByDocAndDtlHQL(int page, int limit, string fields, string where, string dtlWhere, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取服务器授权需要特批的数据", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchSpecialApprovalAHServerLicenceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AHServerLicence>", ReturnType = "ListAHServerLicence")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSpecialApprovalAHServerLicenceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSpecialApprovalAHServerLicenceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID获取服务器授权信息及明细信息数据", Desc = "通过主键ID获取服务器授权信息及明细信息数据", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerLicenceAndAndDetailsById?id={id}", Get = "id={id}", Post = "", Return = "BaseResultList<AHServerLicence>", ReturnType = "ApplyAHServerLicence")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHServerLicenceAndAndDetailsById?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHServerLicenceAndAndDetailsById(long id);

        [ServiceContractDescription(Name = "通过主键ID查询服务器授权", Desc = "通过主键ID查询服务器授权", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerLicenceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AHServerLicence>", ReturnType = "AHServerLicence")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHServerLicenceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHServerLicenceById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "重新生成服务器授权返回文件", Desc = "重新生成服务器授权返回文件", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_RegenerateAHServerLicenceById?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_RegenerateAHServerLicenceById?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_RegenerateAHServerLicenceById(long id);
        #endregion

        #region AHServerProgramLicence

        [ServiceContractDescription(Name = "新增服务器程序授权明细", Desc = "新增服务器程序授权明细", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddAHServerProgramLicence", Get = "", Post = "AHServerProgramLicence", Return = "BaseResultDataValue", ReturnType = "AHServerProgramLicence")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddAHServerProgramLicence", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddAHServerProgramLicence(AHServerProgramLicence entity);

        [ServiceContractDescription(Name = "修改服务器程序授权明细", Desc = "修改服务器程序授权明细", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHServerProgramLicence", Get = "", Post = "AHServerProgramLicence", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateAHServerProgramLicence", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateAHServerProgramLicence(AHServerProgramLicence entity);

        [ServiceContractDescription(Name = "修改服务器程序授权明细指定的属性", Desc = "修改服务器程序授权明细指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHServerProgramLicenceByField", Get = "", Post = "AHServerProgramLicence", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateAHServerProgramLicenceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateAHServerProgramLicenceByField(AHServerProgramLicence entity, string fields);

        [ServiceContractDescription(Name = "删除服务器程序授权明细", Desc = "删除服务器程序授权明细", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelAHServerProgramLicence?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelAHServerProgramLicence?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelAHServerProgramLicence(long id);

        [ServiceContractDescription(Name = "查询服务器程序授权明细", Desc = "查询服务器程序授权明细", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerProgramLicence", Get = "", Post = "AHServerProgramLicence", Return = "BaseResultList<AHServerProgramLicence>", ReturnType = "ListAHServerProgramLicence")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHServerProgramLicence", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHServerProgramLicence(AHServerProgramLicence entity);

        [ServiceContractDescription(Name = "查询服务器程序授权明细(HQL)", Desc = "查询服务器程序授权明细(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerProgramLicenceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AHServerProgramLicence>", ReturnType = "ListAHServerProgramLicence")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHServerProgramLicenceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHServerProgramLicenceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询服务器程序授权明细", Desc = "通过主键ID查询服务器程序授权明细", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHServerProgramLicenceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AHServerProgramLicence>", ReturnType = "AHServerProgramLicence")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHServerProgramLicenceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHServerProgramLicenceById(long id, string fields, bool isPlanish);
        #endregion

        #region AHSingleLicence

        //[ServiceContractDescription(Name = "新增单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成", Desc = "新增单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddAHSingleLicence", Get = "", Post = "AHSingleLicence", Return = "BaseResultDataValue", ReturnType = "AHSingleLicence")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddAHSingleLicence", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddAHSingleLicence(AHSingleLicence entity);

        //[ServiceContractDescription(Name = "修改单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成", Desc = "修改单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHSingleLicence", Get = "", Post = "AHSingleLicence", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateAHSingleLicence", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateAHSingleLicence(AHSingleLicence entity);

        //[ServiceContractDescription(Name = "修改单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成指定的属性", Desc = "修改单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateAHSingleLicenceByField", Get = "", Post = "AHSingleLicence", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateAHSingleLicenceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateAHSingleLicenceByField(AHSingleLicence entity, string fields);

        //[ServiceContractDescription(Name = "删除单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成", Desc = "删除单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelAHSingleLicence?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelAHSingleLicence?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelAHSingleLicence(long id);

        //[ServiceContractDescription(Name = "查询单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成", Desc = "查询单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHSingleLicence", Get = "", Post = "AHSingleLicence", Return = "BaseResultList<AHSingleLicence>", ReturnType = "ListAHSingleLicence")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHSingleLicence", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHSingleLicence(AHSingleLicence entity);

        //[ServiceContractDescription(Name = "查询单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成(HQL)", Desc = "查询单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHSingleLicenceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AHSingleLicence>", ReturnType = "ListAHSingleLicence")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHSingleLicenceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHSingleLicenceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "查询单站点需要特批的数据", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchSpecialApprovalAHSingleLicenceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AHSingleLicence>", ReturnType = "ListAHSingleLicence")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSpecialApprovalAHSingleLicenceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSpecialApprovalAHSingleLicenceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成", Desc = "通过主键ID查询单站点授权授权流程Status：1暂存、2申请、3授权中、4商务授权通过、5商务授权退回、6特批授权中、7特批授权通过、8特批授权退回、9授权完成", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchAHSingleLicenceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AHSingleLicence>", ReturnType = "AHSingleLicence")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchAHSingleLicenceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchAHSingleLicenceById(long id, string fields, bool isPlanish);
        #endregion

        [ServiceContractDescription(Name = "授权截止日期节假日顺延处理", Desc = "授权截止日期节假日顺延处理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_GetLicenceEndDate?endDate={endDate}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetLicenceEndDate?endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetLicenceEndDate(string endDate);

        [ServiceContractDescription(Name = "授权类型为临时时获取开始日期值处理", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_GetStartDateValueOfApply?mac={mac}&sqh={sqh}", Get = "mac={mac}&sqh={sqh}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_GetStartDateValueOfApply?mac={mac}&sqh={sqh}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetStartDateValueOfApply(string mac, string sqh);
        #endregion

        #region PProject

        [ServiceContractDescription(Name = "新增项目表", Desc = "新增项目表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPProject", Get = "", Post = "PProject", Return = "BaseResultDataValue", ReturnType = "PProject")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPProject", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPProject(PProject entity);

        [ServiceContractDescription(Name = "修改项目表", Desc = "修改项目表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePProject", Get = "", Post = "PProject", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePProject", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePProject(PProject entity);

        [ServiceContractDescription(Name = "修改项目表指定的属性", Desc = "修改项目表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePProjectByField", Get = "", Post = "PProject", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePProjectByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePProjectByField(PProject entity, string fields);

        [ServiceContractDescription(Name = "删除项目表", Desc = "删除项目表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPProject?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPProject?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPProject(long id);

        [ServiceContractDescription(Name = "查询项目表", Desc = "查询项目表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProject", Get = "", Post = "PProject", Return = "BaseResultList<PProject>", ReturnType = "ListPProject")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProject", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProject(PProject entity);

        [ServiceContractDescription(Name = "查询项目表(HQL)", Desc = "查询项目表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PProject>", ReturnType = "ListPProject")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询项目表", Desc = "通过主键ID查询项目表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PProject>", ReturnType = "PProject")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectById(long id, string fields, bool isPlanish);
        #endregion

        #region PProjectTask

        [ServiceContractDescription(Name = "新增 项目任务表", Desc = "新增 项目任务表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPProjectTask", Get = "", Post = "PProjectTask", Return = "BaseResultDataValue", ReturnType = "PProjectTask")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPProjectTask", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPProjectTask(PProjectTask entity);

        [ServiceContractDescription(Name = "修改 项目任务表", Desc = "修改 项目任务表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePProjectTask", Get = "", Post = "PProjectTask", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePProjectTask", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePProjectTask(PProjectTask entity);

        [ServiceContractDescription(Name = "修改 项目任务表指定的属性", Desc = "修改 项目任务表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePProjectTaskByField", Get = "", Post = "PProjectTask", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePProjectTaskByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePProjectTaskByField(PProjectTask entity, string fields);

        [ServiceContractDescription(Name = "删除 项目任务表", Desc = "删除 项目任务表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPProjectTask?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPProjectTask?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPProjectTask(long id);

        [ServiceContractDescription(Name = "查询 项目任务表", Desc = "查询 项目任务表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectTask", Get = "", Post = "PProjectTask", Return = "BaseResultList<PProjectTask>", ReturnType = "ListPProjectTask")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectTask", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectTask(PProjectTask entity);

        [ServiceContractDescription(Name = "查询 项目任务表(HQL)", Desc = "查询 项目任务表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectTaskByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PProjectTask>", ReturnType = "ListPProjectTask")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectTaskByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectTaskByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询 项目任务表", Desc = "通过主键ID查询 项目任务表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectTaskById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PProjectTask>", ReturnType = "PProjectTask")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectTaskById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectTaskById(long id, string fields, bool isPlanish);
        #endregion

        #region PProjectTaskProgress

        [ServiceContractDescription(Name = "新增 项目任务进度表", Desc = "新增 项目任务进度表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddPProjectTaskProgress", Get = "", Post = "PProjectTaskProgress", Return = "BaseResultDataValue", ReturnType = "PProjectTaskProgress")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPProjectTaskProgress", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPProjectTaskProgress(PProjectTaskProgress entity);

        [ServiceContractDescription(Name = "修改 项目任务进度表", Desc = "修改 项目任务进度表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePProjectTaskProgress", Get = "", Post = "PProjectTaskProgress", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePProjectTaskProgress", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePProjectTaskProgress(PProjectTaskProgress entity);

        [ServiceContractDescription(Name = "修改 项目任务进度表指定的属性", Desc = "修改 项目任务进度表指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdatePProjectTaskProgressByField", Get = "", Post = "PProjectTaskProgress", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePProjectTaskProgressByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePProjectTaskProgressByField(PProjectTaskProgress entity, string fields);

        [ServiceContractDescription(Name = "删除 项目任务进度表", Desc = "删除 项目任务进度表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelPProjectTaskProgress?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPProjectTaskProgress?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPProjectTaskProgress(long id);

        [ServiceContractDescription(Name = "查询 项目任务进度表", Desc = "查询 项目任务进度表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectTaskProgress", Get = "", Post = "PProjectTaskProgress", Return = "BaseResultList<PProjectTaskProgress>", ReturnType = "ListPProjectTaskProgress")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectTaskProgress", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectTaskProgress(PProjectTaskProgress entity);

        [ServiceContractDescription(Name = "查询 项目任务进度表(HQL)", Desc = "查询 项目任务进度表(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectTaskProgressByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PProjectTaskProgress>", ReturnType = "ListPProjectTaskProgress")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectTaskProgressByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectTaskProgressByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询 项目任务进度表", Desc = "通过主键ID查询 项目任务进度表", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchPProjectTaskProgressById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PProjectTaskProgress>", ReturnType = "PProjectTaskProgress")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectTaskProgressById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectTaskProgressById(long id, string fields, bool isPlanish);
        #endregion

        #region PProjectDocument

        [ServiceContractDescription(Name = "新增项目文档表", Desc = "新增项目文档表", Url = "SingleTableService.svc/ST_UDTO_AddPProjectDocument", Get = "", Post = "PProjectDocument", Return = "BaseResultDataValue", ReturnType = "PProjectDocument")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPProjectDocument", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPProjectDocument(PProjectDocument entity);

        [ServiceContractDescription(Name = "修改项目文档表", Desc = "修改项目文档表", Url = "SingleTableService.svc/ST_UDTO_UpdatePProjectDocument", Get = "", Post = "PProjectDocument", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePProjectDocument", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePProjectDocument(PProjectDocument entity);

        [ServiceContractDescription(Name = "修改项目文档表指定的属性", Desc = "修改项目文档表指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdatePProjectDocumentByField", Get = "", Post = "PProjectDocument", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePProjectDocumentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePProjectDocumentByField(PProjectDocument entity, string fields);

        [ServiceContractDescription(Name = "删除项目文档表", Desc = "删除项目文档表", Url = "SingleTableService.svc/ST_UDTO_DelPProjectDocument?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPProjectDocument?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPProjectDocument(long id);

        [ServiceContractDescription(Name = "查询项目文档表", Desc = "查询项目文档表", Url = "SingleTableService.svc/ST_UDTO_SearchPProjectDocument", Get = "", Post = "PProjectDocument", Return = "BaseResultList<PProjectDocument>", ReturnType = "ListPProjectDocument")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectDocument", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectDocument(PProjectDocument entity);

        [ServiceContractDescription(Name = "查询项目文档表(HQL)", Desc = "查询项目文档表(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchPProjectDocumentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PProjectDocument>", ReturnType = "ListPProjectDocument")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectDocumentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectDocumentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询项目文档表", Desc = "通过主键ID查询项目文档表", Url = "SingleTableService.svc/ST_UDTO_SearchPProjectDocumentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PProjectDocument>", ReturnType = "PProjectDocument")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPProjectDocumentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPProjectDocumentById(long id, string fields, bool isPlanish);
        #endregion

        #region CUser

        [ServiceContractDescription(Name = "新增CUser", Desc = "新增CUser", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_AddCUser", Get = "", Post = "CUser", Return = "BaseResultDataValue", ReturnType = "CUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddCUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddCUser(CUser entity);

        [ServiceContractDescription(Name = "修改CUser", Desc = "修改CUser", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateCUser", Get = "", Post = "CUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCUser(CUser entity);

        [ServiceContractDescription(Name = "修改CUser指定的属性", Desc = "修改CUser指定的属性", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_UpdateCUserByField", Get = "", Post = "CUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCUserByField(CUser entity, string fields);

        [ServiceContractDescription(Name = "删除CUser", Desc = "删除CUser", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_DelCUser?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelCUser?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelCUser(long id);

        [ServiceContractDescription(Name = "查询CUser", Desc = "查询CUser", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchCUser", Get = "", Post = "CUser", Return = "BaseResultList<CUser>", ReturnType = "ListCUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCUser(CUser entity);

        [ServiceContractDescription(Name = "查询CUser(HQL)", Desc = "查询CUser(HQL)", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchCUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CUser>", ReturnType = "ListCUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询CUser", Desc = "通过主键ID查询CUser", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_SearchCUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CUser>", ReturnType = "CUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCUserById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "将CUser某一记录行复制到PClient中", Desc = "将CUser某一记录行复制到PClient中", Url = "ProjectProgressMonitorManageService.svc/ST_UDTO_CopyCUserToPClientByCUserId?id={id}&type={type}", Get = "id={id}&type={type}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_CopyCUserToPClientByCUserId?id={id}&type={type}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_CopyCUserToPClientByCUserId(long id, int type);
        #endregion

        [ServiceContractDescription(Name = "项目复制", Desc = "项目复制", Url = "ProjectProgressMonitorManageService.svc/PM_UDTO_CopyProject?projectID={projectID}&typeID={typeID}&isStandard={isStandard}", Get = "projectID={projectID}&typeID={typeID}&isStandard={isStandard}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PM_UDTO_CopyProject?projectID={projectID}&typeID={typeID}&isStandard={isStandard}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue PM_UDTO_CopyProject(long projectID, long typeID, bool isStandard);

        [ServiceContractDescription(Name = "项目复制", Desc = "项目复制", Url = "ProjectProgressMonitorManageService.svc/PM_UDTO_CopyStandardTask?projectID={projectID}", Get = "projectID={projectID}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PM_UDTO_CopyStandardTask?projectID={projectID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue PM_UDTO_CopyStandardTask(long projectID);

        [ServiceContractDescription(Name = "项目任务复制", Desc = "项目任务复制", Url = "ProjectProgressMonitorManageService.svc/PM_UDTO_CopyProjectTask?fromProjectID={fromProjectID}&toProjectID={toProjectID}&isStandard={isStandard}", Get = "fromProjectID={fromProjectID}&toProjectID={toProjectID}&isStandard={isStandard}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PM_UDTO_CopyProjectTask?fromProjectID={fromProjectID}&toProjectID={toProjectID}&isStandard={isStandard}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue PM_UDTO_CopyProjectTask(long fromProjectID, long toProjectID, bool isStandard);
    }
}
