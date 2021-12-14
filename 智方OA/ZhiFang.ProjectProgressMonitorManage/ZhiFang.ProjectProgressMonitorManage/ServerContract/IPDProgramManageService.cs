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
    public interface IPDProgramManageService
    {
        #region IPDProgramManageService

        #region PGMProgram

        [ServiceContractDescription(Name = "新增程序列表，Status 状态（1、暂存；2，待审核；3、发布）。", Desc = "新增程序列表，Status 状态（1、暂存；2，待审核；3、发布）。", Url = "PDProgramManageService.svc/PGM_UDTO_AddPGMProgram", Get = "", Post = "PGMProgram", Return = "BaseResultDataValue", ReturnType = "PGMProgram")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UDTO_AddPGMProgram", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue PGM_UDTO_AddPGMProgram(PGMProgram entity);

        [ServiceContractDescription(Name = "修改程序列表，Status 状态（1、暂存；2，待审核；3、发布）。", Desc = "修改程序列表，Status 状态（1、暂存；2，待审核；3、发布）。", Url = "PDProgramManageService.svc/PGM_UDTO_UpdatePGMProgram", Get = "", Post = "PGMProgram", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UDTO_UpdatePGMProgram", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool PGM_UDTO_UpdatePGMProgram(PGMProgram entity);

        [ServiceContractDescription(Name = "修改程序列表，Status 状态（1、暂存；2，待审核；3、发布）。指定的属性", Desc = "修改程序列表，Status 状态（1、暂存；2，待审核；3、发布）。指定的属性", Url = "PDProgramManageService.svc/PGM_UDTO_UpdatePGMProgramByField", Get = "", Post = "PGMProgram", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UDTO_UpdatePGMProgramByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool PGM_UDTO_UpdatePGMProgramByField(PGMProgram entity, string fields);

        [ServiceContractDescription(Name = "修改程序状态，Status 状态（1、暂存；2，待审核；3、发布）。指定的属性", Desc = "修改程序状态，Status 状态（1、暂存；2，待审核；3、发布）。指定的属性", Url = "PDProgramManageService.svc/PGM_UpdateStatusByStrIds", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UpdateStatusByStrIds", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool PGM_UpdateStatusByStrIds(string strIds, string status);

        [ServiceContractDescription(Name = "程序的禁用或启用", Desc = "程序的禁用或启用", Url = "PDProgramManageService.svc/PGM_UpdateIsUseByStrIds", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UpdateIsUseByStrIds", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool PGM_UpdateIsUseByStrIds(string strIds, bool isUse);

        [ServiceContractDescription(Name = "通过FormData方式新增程序信息", Desc = "通过FormData方式新增程序信息", Url = "PDProgramManageService.svc/PGM_UDTO_AddPGMProgramByFormData", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UDTO_AddPGMProgramByFormData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream PGM_UDTO_AddPGMProgramByFormData();

        [ServiceContractDescription(Name = "FormData方式更新程序信息", Desc = "FormData方式更新程序信息", Url = "PDProgramManageService.svc/PGM_UDTO_UpdatePGMProgramByFieldAndFormData", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UDTO_UpdatePGMProgramByFieldAndFormData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream PGM_UDTO_UpdatePGMProgramByFieldAndFormData();

        [ServiceContractDescription(Name = "查询某一类型树的程序数据信息", Desc = "查询某一类型树的程序数据信息", Url = "PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramByBDictTreeId?page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFile>", ReturnType = "ListFFile")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UDTO_SearchPGMProgramByBDictTreeId?page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue PGM_UDTO_SearchPGMProgramByBDictTreeId(string where, bool isSearchChildNode, int limit, int page, bool isPlanish, string fields, string sort);

        [ServiceContractDescription(Name = "删除程序列表，Status 状态（1、暂存；2，待审核；3、发布）。", Desc = "删除程序列表，Status 状态（1、暂存；2，待审核；3、发布）。", Url = "PDProgramManageService.svc/PGM_UDTO_DelPGMProgram?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/PGM_UDTO_DelPGMProgram?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool PGM_UDTO_DelPGMProgram(long id);

        [ServiceContractDescription(Name = "查询程序列表，Status 状态（1、暂存；2，待审核；3、发布）。", Desc = "查询程序列表，Status 状态（1、暂存；2，待审核；3、发布）。", Url = "PDProgramManageService.svc/PGM_UDTO_SearchPGMProgram", Get = "", Post = "PGMProgram", Return = "BaseResultList<PGMProgram>", ReturnType = "ListPGMProgram")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UDTO_SearchPGMProgram", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue PGM_UDTO_SearchPGMProgram(PGMProgram entity);

        [ServiceContractDescription(Name = "查询程序列表，Status 状态（1、暂存；2，待审核；3、发布）。(HQL)", Desc = "查询程序列表，Status 状态（1、暂存；2，待审核；3、发布）。(HQL)", Url = "PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PGMProgram>", ReturnType = "ListPGMProgram")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UDTO_SearchPGMProgramByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue PGM_UDTO_SearchPGMProgramByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);


        [ServiceContractDescription(Name = "通过主键ID查询程序列表，Status 状态（1、暂存；2，待审核；3、发布）。", Desc = "通过主键ID查询程序列表，Status 状态（1、暂存；2，待审核；3、发布）。", Url = "PDProgramManageService.svc/PGM_UDTO_SearchPGMProgramById?id={id}&fields={fields}&isPlanish={isPlanish}&isUpdateCounts={isUpdateCounts}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}&isUpdateCounts={isUpdateCounts}", Post = "", Return = "BaseResultList<PGMProgram>", ReturnType = "PGMProgram")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UDTO_SearchPGMProgramById?id={id}&fields={fields}&isPlanish={isPlanish}&isUpdateCounts={isUpdateCounts}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue PGM_UDTO_SearchPGMProgramById(long id, string fields, bool isPlanish, bool isUpdateCounts);

        [ServiceContractDescription(Name = "下载程序附件服务", Desc = "下载程序附件服务", Url = "PDProgramManageService.svc/PGM_UDTO_DownLoadPGMProgramAttachment?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UDTO_DownLoadPGMProgramAttachment?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream PGM_UDTO_DownLoadPGMProgramAttachment(long id, long operateType);
        #endregion

        #endregion

        #region 行数据权限的测试
        [ServiceContractDescription(Name = "行数据权限的测试", Desc = "行数据权限的测试", Url = "PDProgramManageService.svc/PGM_UDTO_TestDataRowRoleSearchPGMProgramByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PGMProgram>", ReturnType = "ListPGMProgram")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PGM_UDTO_TestDataRowRoleSearchPGMProgramByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue PGM_UDTO_TestDataRowRoleSearchPGMProgramByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);
        #endregion

    }
}
