using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“ILabStarQCService”。
    [ServiceContract]
    public interface ILabStarQCService
    {
        //#region LBQCItem

        //[ServiceContractDescription(Name = "新增LB_QCItem", Desc = "新增LB_QCItem", Url = "LabStarQCService.svc/QC_UDTO_AddLBQCItem", Get = "", Post = "LBQCItem", Return = "BaseResultDataValue", ReturnType = "LBQCItem")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddLBQCItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddLBQCItem(LBQCItem entity);

        //[ServiceContractDescription(Name = "修改LB_QCItem", Desc = "修改LB_QCItem", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCItem", Get = "", Post = "LBQCItem", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCItem(LBQCItem entity);

        //[ServiceContractDescription(Name = "修改LB_QCItem指定的属性", Desc = "修改LB_QCItem指定的属性", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCItemByField", Get = "", Post = "LBQCItem", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCItemByField(LBQCItem entity, string fields);

        //[ServiceContractDescription(Name = "删除LB_QCItem", Desc = "删除LB_QCItem", Url = "LabStarQCService.svc/QC_UDTO_DelLBQCItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QC_UDTO_DelLBQCItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_DelLBQCItem(long id);

        //[ServiceContractDescription(Name = "查询LB_QCItem", Desc = "查询LB_QCItem", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCItem", Get = "", Post = "LBQCItem", Return = "BaseResultList<LBQCItem>", ReturnType = "ListLBQCItem")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCItem(LBQCItem entity);

        //[ServiceContractDescription(Name = "查询LB_QCItem(HQL)", Desc = "查询LB_QCItem(HQL)", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCItem>", ReturnType = "ListLBQCItem")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询LB_QCItem", Desc = "通过主键ID查询LB_QCItem", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCItem>", ReturnType = "LBQCItem")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCItemById(long id, string fields, bool isPlanish);
        //#endregion

        //#region LBQCItemTime

        //[ServiceContractDescription(Name = "新增LB_QCItemTime", Desc = "新增LB_QCItemTime", Url = "LabStarQCService.svc/QC_UDTO_AddLBQCItemTime", Get = "", Post = "LBQCItemTime", Return = "BaseResultDataValue", ReturnType = "LBQCItemTime")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddLBQCItemTime", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddLBQCItemTime(LBQCItemTime entity);

        //[ServiceContractDescription(Name = "修改LB_QCItemTime", Desc = "修改LB_QCItemTime", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCItemTime", Get = "", Post = "LBQCItemTime", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCItemTime", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCItemTime(LBQCItemTime entity);

        //[ServiceContractDescription(Name = "修改LB_QCItemTime指定的属性", Desc = "修改LB_QCItemTime指定的属性", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCItemTimeByField", Get = "", Post = "LBQCItemTime", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCItemTimeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCItemTimeByField(LBQCItemTime entity, string fields);

        //[ServiceContractDescription(Name = "删除LB_QCItemTime", Desc = "删除LB_QCItemTime", Url = "LabStarQCService.svc/QC_UDTO_DelLBQCItemTime?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QC_UDTO_DelLBQCItemTime?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_DelLBQCItemTime(long id);

        //[ServiceContractDescription(Name = "查询LB_QCItemTime", Desc = "查询LB_QCItemTime", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCItemTime", Get = "", Post = "LBQCItemTime", Return = "BaseResultList<LBQCItemTime>", ReturnType = "ListLBQCItemTime")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCItemTime", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCItemTime(LBQCItemTime entity);

        //[ServiceContractDescription(Name = "查询LB_QCItemTime(HQL)", Desc = "查询LB_QCItemTime(HQL)", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCItemTimeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCItemTime>", ReturnType = "ListLBQCItemTime")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCItemTimeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCItemTimeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询LB_QCItemTime", Desc = "通过主键ID查询LB_QCItemTime", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCItemTimeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCItemTime>", ReturnType = "LBQCItemTime")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCItemTimeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCItemTimeById(long id, string fields, bool isPlanish);
        //#endregion

        //#region LBQCMaterial

        //[ServiceContractDescription(Name = "新增LB_QCMaterial", Desc = "新增LB_QCMaterial", Url = "LabStarQCService.svc/QC_UDTO_AddLBQCMaterial", Get = "", Post = "LBQCMaterial", Return = "BaseResultDataValue", ReturnType = "LBQCMaterial")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddLBQCMaterial", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddLBQCMaterial(LBQCMaterial entity);

        //[ServiceContractDescription(Name = "修改LB_QCMaterial", Desc = "修改LB_QCMaterial", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCMaterial", Get = "", Post = "LBQCMaterial", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCMaterial", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCMaterial(LBQCMaterial entity);

        //[ServiceContractDescription(Name = "修改LB_QCMaterial指定的属性", Desc = "修改LB_QCMaterial指定的属性", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCMaterialByField", Get = "", Post = "LBQCMaterial", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCMaterialByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCMaterialByField(LBQCMaterial entity, string fields);

        //[ServiceContractDescription(Name = "删除LB_QCMaterial", Desc = "删除LB_QCMaterial", Url = "LabStarQCService.svc/QC_UDTO_DelLBQCMaterial?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QC_UDTO_DelLBQCMaterial?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_DelLBQCMaterial(long id);

        //[ServiceContractDescription(Name = "查询LB_QCMaterial", Desc = "查询LB_QCMaterial", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCMaterial", Get = "", Post = "LBQCMaterial", Return = "BaseResultList<LBQCMaterial>", ReturnType = "ListLBQCMaterial")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCMaterial", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCMaterial(LBQCMaterial entity);

        //[ServiceContractDescription(Name = "查询LB_QCMaterial(HQL)", Desc = "查询LB_QCMaterial(HQL)", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCMaterialByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCMaterial>", ReturnType = "ListLBQCMaterial")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCMaterialByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCMaterialByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询LB_QCMaterial", Desc = "通过主键ID查询LB_QCMaterial", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCMaterialById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCMaterial>", ReturnType = "LBQCMaterial")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCMaterialById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCMaterialById(long id, string fields, bool isPlanish);
        //#endregion

        //#region LBQCMatTime

        //[ServiceContractDescription(Name = "新增LB_QCMatTime", Desc = "新增LB_QCMatTime", Url = "LabStarQCService.svc/QC_UDTO_AddLBQCMatTime", Get = "", Post = "LBQCMatTime", Return = "BaseResultDataValue", ReturnType = "LBQCMatTime")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddLBQCMatTime", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddLBQCMatTime(LBQCMatTime entity);

        //[ServiceContractDescription(Name = "修改LB_QCMatTime", Desc = "修改LB_QCMatTime", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCMatTime", Get = "", Post = "LBQCMatTime", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCMatTime", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCMatTime(LBQCMatTime entity);

        //[ServiceContractDescription(Name = "修改LB_QCMatTime指定的属性", Desc = "修改LB_QCMatTime指定的属性", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCMatTimeByField", Get = "", Post = "LBQCMatTime", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCMatTimeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCMatTimeByField(LBQCMatTime entity, string fields);

        //[ServiceContractDescription(Name = "删除LB_QCMatTime", Desc = "删除LB_QCMatTime", Url = "LabStarQCService.svc/QC_UDTO_DelLBQCMatTime?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QC_UDTO_DelLBQCMatTime?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_DelLBQCMatTime(long id);

        //[ServiceContractDescription(Name = "查询LB_QCMatTime", Desc = "查询LB_QCMatTime", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCMatTime", Get = "", Post = "LBQCMatTime", Return = "BaseResultList<LBQCMatTime>", ReturnType = "ListLBQCMatTime")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCMatTime", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCMatTime(LBQCMatTime entity);

        //[ServiceContractDescription(Name = "查询LB_QCMatTime(HQL)", Desc = "查询LB_QCMatTime(HQL)", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCMatTimeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCMatTime>", ReturnType = "ListLBQCMatTime")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCMatTimeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCMatTimeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询LB_QCMatTime", Desc = "通过主键ID查询LB_QCMatTime", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCMatTimeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCMatTime>", ReturnType = "LBQCMatTime")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCMatTimeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCMatTimeById(long id, string fields, bool isPlanish);
        //#endregion

        //#region LBQCItemRule

        //[ServiceContractDescription(Name = "新增LB_QCItemRule", Desc = "新增LB_QCItemRule", Url = "LabStarQCService.svc/QC_UDTO_AddLBQCItemRule", Get = "", Post = "LBQCItemRule", Return = "BaseResultDataValue", ReturnType = "LBQCItemRule")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddLBQCItemRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddLBQCItemRule(LBQCItemRule entity);

        //[ServiceContractDescription(Name = "修改LB_QCItemRule", Desc = "修改LB_QCItemRule", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCItemRule", Get = "", Post = "LBQCItemRule", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCItemRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCItemRule(LBQCItemRule entity);

        //[ServiceContractDescription(Name = "修改LB_QCItemRule指定的属性", Desc = "修改LB_QCItemRule指定的属性", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCItemRuleByField", Get = "", Post = "LBQCItemRule", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCItemRuleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCItemRuleByField(LBQCItemRule entity, string fields);

        //[ServiceContractDescription(Name = "删除LB_QCItemRule", Desc = "删除LB_QCItemRule", Url = "LabStarQCService.svc/QC_UDTO_DelLBQCItemRule?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QC_UDTO_DelLBQCItemRule?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_DelLBQCItemRule(long id);

        //[ServiceContractDescription(Name = "查询LB_QCItemRule", Desc = "查询LB_QCItemRule", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCItemRule", Get = "", Post = "LBQCItemRule", Return = "BaseResultList<LBQCItemRule>", ReturnType = "ListLBQCItemRule")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCItemRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCItemRule(LBQCItemRule entity);

        //[ServiceContractDescription(Name = "查询LB_QCItemRule(HQL)", Desc = "查询LB_QCItemRule(HQL)", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCItemRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCItemRule>", ReturnType = "ListLBQCItemRule")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCItemRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCItemRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询LB_QCItemRule", Desc = "通过主键ID查询LB_QCItemRule", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCItemRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCItemRule>", ReturnType = "LBQCItemRule")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCItemRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCItemRuleById(long id, string fields, bool isPlanish);
        //#endregion

        //#region LBQCRule

        //[ServiceContractDescription(Name = "新增LB_QCRule", Desc = "新增LB_QCRule", Url = "LabStarQCService.svc/QC_UDTO_AddLBQCRule", Get = "", Post = "LBQCRule", Return = "BaseResultDataValue", ReturnType = "LBQCRule")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddLBQCRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddLBQCRule(LBQCRule entity);

        //[ServiceContractDescription(Name = "修改LB_QCRule", Desc = "修改LB_QCRule", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCRule", Get = "", Post = "LBQCRule", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCRule(LBQCRule entity);

        //[ServiceContractDescription(Name = "修改LB_QCRule指定的属性", Desc = "修改LB_QCRule指定的属性", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCRuleByField", Get = "", Post = "LBQCRule", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCRuleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCRuleByField(LBQCRule entity, string fields);

        //[ServiceContractDescription(Name = "删除LB_QCRule", Desc = "删除LB_QCRule", Url = "LabStarQCService.svc/QC_UDTO_DelLBQCRule?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QC_UDTO_DelLBQCRule?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_DelLBQCRule(long id);

        //[ServiceContractDescription(Name = "查询LB_QCRule", Desc = "查询LB_QCRule", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCRule", Get = "", Post = "LBQCRule", Return = "BaseResultList<LBQCRule>", ReturnType = "ListLBQCRule")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCRule(LBQCRule entity);

        //[ServiceContractDescription(Name = "查询LB_QCRule(HQL)", Desc = "查询LB_QCRule(HQL)", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCRule>", ReturnType = "ListLBQCRule")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询LB_QCRule", Desc = "通过主键ID查询LB_QCRule", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCRule>", ReturnType = "LBQCRule")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCRuleById(long id, string fields, bool isPlanish);
        //#endregion

        //#region LBQCRuleBase

        //[ServiceContractDescription(Name = "新增LB_QCRuleBase", Desc = "新增LB_QCRuleBase", Url = "LabStarQCService.svc/QC_UDTO_AddLBQCRuleBase", Get = "", Post = "LBQCRuleBase", Return = "BaseResultDataValue", ReturnType = "LBQCRuleBase")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddLBQCRuleBase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddLBQCRuleBase(LBQCRuleBase entity);

        //[ServiceContractDescription(Name = "修改LB_QCRuleBase", Desc = "修改LB_QCRuleBase", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCRuleBase", Get = "", Post = "LBQCRuleBase", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCRuleBase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCRuleBase(LBQCRuleBase entity);

        //[ServiceContractDescription(Name = "修改LB_QCRuleBase指定的属性", Desc = "修改LB_QCRuleBase指定的属性", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCRuleBaseByField", Get = "", Post = "LBQCRuleBase", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCRuleBaseByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCRuleBaseByField(LBQCRuleBase entity, string fields);

        //[ServiceContractDescription(Name = "删除LB_QCRuleBase", Desc = "删除LB_QCRuleBase", Url = "LabStarQCService.svc/QC_UDTO_DelLBQCRuleBase?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QC_UDTO_DelLBQCRuleBase?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_DelLBQCRuleBase(long id);

        //[ServiceContractDescription(Name = "查询LB_QCRuleBase", Desc = "查询LB_QCRuleBase", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCRuleBase", Get = "", Post = "LBQCRuleBase", Return = "BaseResultList<LBQCRuleBase>", ReturnType = "ListLBQCRuleBase")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCRuleBase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCRuleBase(LBQCRuleBase entity);

        //[ServiceContractDescription(Name = "查询LB_QCRuleBase(HQL)", Desc = "查询LB_QCRuleBase(HQL)", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCRuleBaseByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCRuleBase>", ReturnType = "ListLBQCRuleBase")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCRuleBaseByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCRuleBaseByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询LB_QCRuleBase", Desc = "通过主键ID查询LB_QCRuleBase", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCRuleBaseById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCRuleBase>", ReturnType = "LBQCRuleBase")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCRuleBaseById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCRuleBaseById(long id, string fields, bool isPlanish);
        //#endregion

        //#region LBQCRulesCon

        //[ServiceContractDescription(Name = "新增LB_QCRulesCon", Desc = "新增LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_AddLBQCRulesCon", Get = "", Post = "LBQCRulesCon", Return = "BaseResultDataValue", ReturnType = "LBQCRulesCon")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddLBQCRulesCon", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddLBQCRulesCon(LBQCRulesCon entity);

        //[ServiceContractDescription(Name = "修改LB_QCRulesCon", Desc = "修改LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCRulesCon", Get = "", Post = "LBQCRulesCon", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCRulesCon", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCRulesCon(LBQCRulesCon entity);

        //[ServiceContractDescription(Name = "修改LB_QCRulesCon指定的属性", Desc = "修改LB_QCRulesCon指定的属性", Url = "LabStarQCService.svc/QC_UDTO_UpdateLBQCRulesConByField", Get = "", Post = "LBQCRulesCon", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLBQCRulesConByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLBQCRulesConByField(LBQCRulesCon entity, string fields);

        //[ServiceContractDescription(Name = "删除LB_QCRulesCon", Desc = "删除LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_DelLBQCRulesCon?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QC_UDTO_DelLBQCRulesCon?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_DelLBQCRulesCon(long id);

        //[ServiceContractDescription(Name = "查询LB_QCRulesCon", Desc = "查询LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCRulesCon", Get = "", Post = "LBQCRulesCon", Return = "BaseResultList<LBQCRulesCon>", ReturnType = "ListLBQCRulesCon")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCRulesCon", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCRulesCon(LBQCRulesCon entity);

        //[ServiceContractDescription(Name = "查询LB_QCRulesCon(HQL)", Desc = "查询LB_QCRulesCon(HQL)", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCRulesConByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCRulesCon>", ReturnType = "ListLBQCRulesCon")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCRulesConByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCRulesConByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询LB_QCRulesCon", Desc = "通过主键ID查询LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_SearchLBQCRulesConById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCRulesCon>", ReturnType = "LBQCRulesCon")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLBQCRulesConById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLBQCRulesConById(long id, string fields, bool isPlanish);
        //#endregion

        //#region LisQCData

        //[ServiceContractDescription(Name = "新增LB_QCRulesCon", Desc = "新增LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_AddLisQCData", Get = "", Post = "LisQCData", Return = "BaseResultDataValue", ReturnType = "LisQCData")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddLisQCData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddLisQCData(LisQCData entity);

        //[ServiceContractDescription(Name = "修改LB_QCRulesCon", Desc = "修改LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_UpdateLisQCData", Get = "", Post = "LisQCData", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLisQCData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLisQCData(LisQCData entity);

        //[ServiceContractDescription(Name = "修改LB_QCRulesCon指定的属性", Desc = "修改LB_QCRulesCon指定的属性", Url = "LabStarQCService.svc/QC_UDTO_UpdateLisQCDataByField", Get = "", Post = "LisQCData", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLisQCDataByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLisQCDataByField(LisQCData entity, string fields);

        //[ServiceContractDescription(Name = "删除LB_QCRulesCon", Desc = "删除LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_DelLisQCData?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QC_UDTO_DelLisQCData?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_DelLisQCData(long id);

        //[ServiceContractDescription(Name = "查询LB_QCRulesCon", Desc = "查询LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_SearchLisQCData", Get = "", Post = "LisQCData", Return = "BaseResultList<LisQCData>", ReturnType = "ListLisQCData")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLisQCData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLisQCData(LisQCData entity);

        //[ServiceContractDescription(Name = "查询LB_QCRulesCon(HQL)", Desc = "查询LB_QCRulesCon(HQL)", Url = "LabStarQCService.svc/QC_UDTO_SearchLisQCDataByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisQCData>", ReturnType = "ListLisQCData")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLisQCDataByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLisQCDataByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询LB_QCRulesCon", Desc = "通过主键ID查询LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_SearchLisQCDataById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisQCData>", ReturnType = "LisQCData")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLisQCDataById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLisQCDataById(long id, string fields, bool isPlanish);
        //#endregion

        //#region LisQCComments

        //[ServiceContractDescription(Name = "新增LB_QCRulesCon", Desc = "新增LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_AddLisQCComments", Get = "", Post = "LisQCComments", Return = "BaseResultDataValue", ReturnType = "LisQCComments")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddLisQCComments", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddLisQCComments(LisQCComments entity);

        //[ServiceContractDescription(Name = "修改LB_QCRulesCon", Desc = "修改LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_UpdateLisQCComments", Get = "", Post = "LisQCComments", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLisQCComments", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLisQCComments(LisQCComments entity);

        //[ServiceContractDescription(Name = "修改LB_QCRulesCon指定的属性", Desc = "修改LB_QCRulesCon指定的属性", Url = "LabStarQCService.svc/QC_UDTO_UpdateLisQCCommentsByField", Get = "", Post = "LisQCComments", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_UpdateLisQCCommentsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_UpdateLisQCCommentsByField(LisQCComments entity, string fields);

        //[ServiceContractDescription(Name = "删除LB_QCRulesCon", Desc = "删除LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_DelLisQCComments?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QC_UDTO_DelLisQCComments?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool QC_UDTO_DelLisQCComments(long id);

        //[ServiceContractDescription(Name = "查询LB_QCRulesCon", Desc = "查询LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_SearchLisQCComments", Get = "", Post = "LisQCComments", Return = "BaseResultList<LisQCComments>", ReturnType = "ListLisQCComments")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLisQCComments", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLisQCComments(LisQCComments entity);

        //[ServiceContractDescription(Name = "查询LB_QCRulesCon(HQL)", Desc = "查询LB_QCRulesCon(HQL)", Url = "LabStarQCService.svc/QC_UDTO_SearchLisQCCommentsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisQCComments>", ReturnType = "ListLisQCComments")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLisQCCommentsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLisQCCommentsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询LB_QCRulesCon", Desc = "通过主键ID查询LB_QCRulesCon", Url = "LabStarQCService.svc/QC_UDTO_SearchLisQCCommentsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisQCComments>", ReturnType = "LisQCComments")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_SearchLisQCCommentsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_SearchLisQCCommentsById(long id, string fields, bool isPlanish);
        //#endregion

        //#region 质控定制服务

        //[ServiceContractDescription(Name = "获取仪器质控物树形列表", Desc = "获取仪器质控物树形列表", Url = "LabStarQCService.svc/QC_UDTO_GetEquipMaterialTree?equipID={equipID}&matID={matID}", Get = "equipID={equipID}&matID={matID}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_GetEquipMaterialTree?equipID={equipID}&matID={matID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_GetEquipMaterialTree(long equipID, long matID);

        //[ServiceContractDescription(Name = "新增和删除质控项目", Desc = "新增和删除质控项目", Url = "LabStarPreService.svc/QC_UDTO_AddDelLBQCItem", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddDelLBQCItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddDelLBQCItem(IList<LBQCItem> addEntityList, bool isCheckEntityExist, string delIDList);

        //[ServiceContractDescription(Name = "新增和删除质控规则关系", Desc = "新增和删除质控规则关系", Url = "LabStarPreService.svc/QC_UDTO_AddDelLBQCRulesCon", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddDelLBQCRulesCon", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddDelLBQCRulesCon(IList<LBQCRulesCon> addEntityList, bool isCheckEntityExist, string delIDList);

        //[ServiceContractDescription(Name = "新增和删除质控项目特殊规则", Desc = "新增和删除质控项目特殊规则", Url = "LabStarPreService.svc/QC_UDTO_AddDelLBQCItemRule", Get = "", Post = "listLBQCItem,listLBQCRule", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddDelLBQCItemRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddDelLBQCItemRule(IList<LBQCItemRule> addEntityList, bool isCheckEntityExist, string delIDList);

        //[ServiceContractDescription(Name = "新增质控项目特殊规则", Desc = "新增除质控项目特殊规则", Url = "LabStarPreService.svc/QC_UDTO_AddLBQCItemRuleByItemList", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_AddLBQCItemRuleByItemList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_AddLBQCItemRuleByItemList(IList<LBQCItem> listLBQCItem, IList<LBQCRule> listLBQCRule);

        //[ServiceContractDescription(Name = "清理无效的质控规则", Desc = "清理无效的质控规则", Url = "LabStarQCService.svc/QC_UDTO_DeleteInvalidLBQCRule", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_DeleteInvalidLBQCRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_DeleteInvalidLBQCRule();

        //[ServiceContractDescription(Name = "复制质控物项目", Desc = "复制质控物项目", Url = "LabStarQCService.svc/QC_UDTO_CopyLBQCItemByMatID?fromMatID={fromMatID}&toMatID={toMatID}", Get = "fromMatID={fromMatID}&toMatID={toMatID}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_CopyLBQCItemByMatID?fromMatID={fromMatID}&toMatID={toMatID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_CopyLBQCItemByMatID(long fromMatID, long toMatID);

        //[ServiceContractDescription(Name = "计算靶值", Desc = "计算靶值", Url = "LabStarQCService.svc/QC_UDTO_GetCalcTargetByQCData?qcItemID={qcItemID}&beginDate={beginDate}&endDate={endDate}", Get = "qcItemID={qcItemID}&beginDate={beginDate}&endDate={endDate}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_GetCalcTargetByQCData?qcItemID={qcItemID}&beginDate={beginDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_GetCalcTargetByQCData(long qcItemID, string beginDate, string endDate);

        //[ServiceContractDescription(Name = "计算标准差", Desc = "计算标准差", Url = "LabStarQCService.svc/QC_UDTO_GetCalcSDByQCData?qcItemID={qcItemID}&beginDate={beginDate}&endDate={endDate}", Get = "qcItemID={qcItemID}&beginDate={beginDate}&endDate={endDate}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_GetCalcSDByQCData?qcItemID={qcItemID}&beginDate={beginDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_GetCalcSDByQCData(long qcItemID, string beginDate, string endDate);

        ////[ServiceContractDescription(Name = "计算靶值标准差", Desc = "计算靶值标准差", Url = "LabStarQCService.svc/QC_UDTO_GetCalcTargetSDByQCData", Get = "", Post = "listQCItemID,beginDate,endDate", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        ////[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_GetCalcTargetSDByQCData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        ////[OperationContract]
        //[ServiceContractDescription(Name = "计算靶值标准差", Desc = "计算靶值标准差", Url = "LabStarQCService.svc/QC_UDTO_GetCalcTargetSDByQCData?listQCItemID={listQCItemID}&beginDate={beginDate}&endDate={endDate}", Get = "listQCItemID={listQCItemID}&beginDate={beginDate}&endDate={endDate}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_GetCalcTargetSDByQCData?listQCItemID={listQCItemID}&beginDate={beginDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_GetCalcTargetSDByQCData(string listQCItemID, string beginDate, string endDate);

        //[ServiceContractDescription(Name = "查询日指控", Desc = "查询日指控", Url = "LabStarQCService.svc/GetQCDays?EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetQCDays?EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue GetQCDays(long EquipID, long QCMatID, DateTime dateTime, int page, int limit);

        //[ServiceContractDescription(Name = "查询月指控", Desc = "查询月指控", Url = "LabStarQCService.svc/getQCMoths?QCItemID={QCItemID}&Buse={Buse}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/getQCMoths?QCItemID={QCItemID}&Buse={Buse}&fields={fields}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue getQCMoths(long QCItemID, bool Buse, string fields, DateTime startDate, DateTime endDate);
        //[ServiceContractDescription(Name = "月质控项目查询", Desc = "月质控项目查询", Url = "LabStarQCService.svc/GetQCMothItem?QCItemID={QCItemID}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetQCMothItem?QCItemID={QCItemID}&fields={fields}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue GetQCMothItem(long QCItemID, string fields, DateTime startDate, DateTime endDate);

        //[ServiceContractDescription(Name = "月质控树列表", Desc = "月质控树列表", Url = "LabStarQCService.svc/GetQCMothTree", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetQCMothTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue GetQCMothTree();
        //[ServiceContractDescription(Name = "查询多浓度质控", Desc = "查询多浓度质控", Url = "LabStarQCService.svc/GetMultipleConcentrationQCM?QCItemID={QCItemID}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetMultipleConcentrationQCM?EquipId={EquipId}&QCMMoudle={QCMMoudle}&QCMGroup={QCMGroup}&ItemId={ItemId}&fields={fields}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue GetMultipleConcentrationQCM(long EquipId, long ItemId, DateTime startDate, DateTime endDate, string QCMMoudle, string QCMGroup, string fields);

        //[ServiceContractDescription(Name = "多浓度质控树列表", Desc = "多浓度质控树列表", Url = "LabStarQCService.svc/GetMultipleConcentrationQCMTree", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetMultipleConcentrationQCMTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue GetMultipleConcentrationQCMTree();
        //[ServiceContractDescription(Name = "多浓度质控树筛选列表", Desc = "多浓度质控树筛选列表", Url = "LabStarQCService.svc/SearchLBQCItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCItem>", ReturnType = "ListLBQCItem")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchLBQCItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue SearchLBQCItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "查询LB_QCItem(HQL)", Desc = "查询LB_QCItem(HQL)", Url = "LabStarQCService.svc/QC_UDTO_QueryLBQCItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCItem>", ReturnType = "ListLBQCItem")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_UDTO_QueryLBQCItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QC_UDTO_QueryLBQCItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "多浓度质控对比信息", Desc = "多浓度质控对比信息", Url = "LabStarQCService.svc/GetMultipleConcentrationQCMCompareInfo?QCItemIds={QCItemIds}&startDate={startDate}&endDate={endDate}", Post = "", Return = "BaseResultList<LBQCItem>", ReturnType = "ListLBQCItem")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetMultipleConcentrationQCMCompareInfo?QCItemIds={QCItemIds}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue GetMultipleConcentrationQCMCompareInfo(string QCItemIds, DateTime startDate, DateTime endDate);
        //[ServiceContractDescription(Name = "多浓度质控详细信息", Desc = "多浓度质控详细信息", Url = "LabStarQCService.svc/GetMultipleConcentrationQCMInfoFull?QCItemIds={QCItemIds}&fields={fields}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetMultipleConcentrationQCMInfoFull?QCItemIds={QCItemIds}&fields={fields}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue GetMultipleConcentrationQCMInfoFull(string QCItemIds, string fields, DateTime startDate, DateTime endDate);

        //[ServiceContractDescription(Name = "新增质控项目数据", Desc = "新增质控项目数据", Url = "LabStarPreService.svc/SaveLisQCData", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SaveLisQCData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool SaveLisQCData(LisQCData entity);

        //[ServiceContractDescription(Name = "新增质控项目数据", Desc = "新增质控项目数据", Url = "LabStarPreService.svc/SaveLisQCDataBatch", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SaveLisQCDataBatch", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool SaveLisQCDataBatch(List<LisQCData> entity);

        //[ServiceContractDescription(Name = "修改质控项目数据", Desc = "修改质控项目数据", Url = "LabStarPreService.svc/UpdateLisQCData", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLisQCData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool UpdateLisQCData(LisQCData entity, string fields);

        //[ServiceContractDescription(Name = "修改质控项目数据", Desc = "修改质控项目数据", Url = "LabStarPreService.svc/UpdateLisQCDataBatch", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLisQCDataBatch", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool UpdateLisQCDataBatch(List<LisQCData> entity, string fields);

        //[ServiceContractDescription(Name = "仪器日指控 仪器列表查询", Desc = "仪器日指控 仪器列表查询", Url = "LabStarQCService.svc/SearchEquipDayQCM", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchEquipDayQCM", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue SearchEquipDayQCM();

        //[ServiceContractDescription(Name = "仪器日指控 对比信息", Desc = "仪器日指控 对比信息", Url = "LabStarQCService.svc/SearchEquipDayQCMList?EquipId={EquipId}&EquipModel={EquipModel}&EquipGroup={EquipGroup}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchEquipDayQCMList?EquipId={EquipId}&EquipModel={EquipModel}&EquipGroup={EquipGroup}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue SearchEquipDayQCMList(long EquipId, string EquipModel, string EquipGroup, DateTime startDate, DateTime endDate);

        //[ServiceContractDescription(Name = "仪器日指控 单个质控物信息", Desc = "仪器日指控 单个质控物信息", Url = "LabStarQCService.svc/SearchEquipDayQCData?QCMId={QCMId}&fields={fields}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchEquipDayQCData?QCMId={QCMId}&fields={fields}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue SearchEquipDayQCData(long QCMId, string fields, DateTime startDate, DateTime endDate);

        //[ServiceContractDescription(Name = "仪器日指控 详细信息", Desc = "仪器日指控 详细信息", Url = "LabStarQCService.svc/SearchEquipDayQCMFull?QCMIDs={QCMIDs}&fields={fields}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchEquipDayQCMFull?QCMIDs={QCMIDs}&fields={fields}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue SearchEquipDayQCMFull(string QCMIDs, string fields, DateTime startDate, DateTime endDate);

        //[ServiceContractDescription(Name = "仪器日指控 质控物查询", Desc = "仪器日指控 质控物查询", Url = "LabStarQCService.svc/SearchEquipDayQCMalt?EquipId={EquipId}&fields={fields}&EquipModel={EquipModel}&EquipGroup={EquipGroup}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchEquipDayQCMalt?EquipId={EquipId}&fields={fields}&EquipModel={EquipModel}&EquipGroup={EquipGroup}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue SearchEquipDayQCMalt(long EquipId, string EquipModel, string EquipGroup, string fields);

        //[ServiceContractDescription(Name = "失控处理树", Desc = "失控处理树", Url = "LabStarQCService.svc/GetOutControlTree?type={type}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetOutControlTree?type={type}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue GetOutControlTree(string type);

        //[ServiceContractDescription(Name = "获得质控模板列表", Desc = "获得质控模板列表", Url = "LabStarQCService.svc/GetQCTempleModuleList?Name={Name}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetQCTempleModuleList?Name={Name}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue GetQCTempleModuleList(string Name);

        //[ServiceContractDescription(Name = "lj质控图", Desc = "lj质控图", Url = "LabStarQCService.svc/QCMFigureLJ?QCItemId={QCItemId}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QCMFigureLJ?QCItemId={QCItemId}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QCMFigureLJ(long QCItemId, DateTime startDate, DateTime endDate);

        //[ServiceContractDescription(Name = "Z质控图", Desc = "Z质控图", Url = "LabStarQCService.svc/QCMFigureZ?EquipId={EquipId}&QCMId={QCMId}&ItemId={ItemId}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QCMFigureZ?EquipId={EquipId}&QCMId={QCMId}&ItemId={ItemId}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QCMFigureZ(long EquipId, long QCMId, long ItemId, DateTime startDate, DateTime endDate);

        //[ServiceContractDescription(Name = "值范围质控图", Desc = "值范围质控图", Url = "LabStarQCService.svc/QCMFigureValueRange?QCItemId={QCItemId}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QCMFigureValueRange?QCItemId={QCItemId}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QCMFigureValueRange(long QCItemId, DateTime startDate, DateTime endDate);

        //[ServiceContractDescription(Name = "Monica图", Desc = "Monica图", Url = "LabStarQCService.svc/QCMFigureValueMonica?QCItemId={QCItemId}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QCMFigureValueMonica?QCItemId={QCItemId}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QCMFigureValueMonica(long QCItemId, DateTime startDate, DateTime endDate);

        //[ServiceContractDescription(Name = "定性图", Desc = "定性图", Url = "LabStarQCService.svc/QCMFigureQualitative?QCItemId={QCItemId}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QCMFigureQualitative?QCItemId={QCItemId}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QCMFigureQualitative(long QCItemId, DateTime startDate, DateTime endDate);

        //[ServiceContractDescription(Name = "Youden图", Desc = "Youden图", Url = "LabStarQCService.svc/QCMFigureYouden?QCItemIds={QCItemIds}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QCMFigureYouden?QCItemIds={QCItemIds}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QCMFigureYouden(string QCItemIds, DateTime startDate, DateTime endDate);

        //[ServiceContractDescription(Name = "正太分布图", Desc = "正太分布图", Url = "LabStarQCService.svc/QCMFigureNormalDistribution?QCItemIds={QCItemIds}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QCMFigureNormalDistribution?QCItemIds={QCItemIds}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QCMFigureNormalDistribution(string QCItemIds, DateTime startDate, DateTime endDate);

        //[ServiceContractDescription(Name = "累积和图", Desc = "累积和图", Url = "LabStarQCService.svc/QCMFigureCumulativeSumGraph?QCItemIds={QCItemIds}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QCMFigureCumulativeSumGraph?QCItemId={QCItemId}&Target={Target}&SD={SD}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QCMFigureCumulativeSumGraph(long QCItemId, double Target, double SD, DateTime startDate, DateTime endDate);
        //[ServiceContractDescription(Name = "频数分布图", Desc = "频数分布图", Url = "LabStarQCService.svc/QCMFigureFrequencyDistribution?QCItemIds={QCItemIds}&startDate={startDate}&endDate={endDate}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QCMFigureFrequencyDistribution?QCItemId={QCItemId}&Target={Target}&SD={SD}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QCMFigureFrequencyDistribution(long QCItemId, double Target, double SD, DateTime startDate, DateTime endDate);
        //#endregion

        //#region 检验业务需要的质控服务
        //[ServiceContractDescription(Name = "根据小组找仪器质控物", Desc = "根据小组找仪器质控物", Url = "LabStarQCService.svc/SearchQCMaterialbySectionEquip?SectionId={SectionId}&fields={fields}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchQCMaterialbySectionEquip?SectionId={SectionId}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue SearchQCMaterialbySectionEquip(long SectionId, string fields);

        //[ServiceContractDescription(Name = "检验结果转为质控结果", Desc = "检验结果转为质控结果", Url = "LabStarQCService.svc/TestFormConvatQCItem?QCMatId={QCMatId}&TestFormId={TestFormId}", Get = "EquipID={EquipID}&QCMatID={QCMatID}&dateTime={dateTime}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/TestFormConvatQCItem?QCMatId={QCMatId}&TestFormId={TestFormId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool TestFormConvatQCItem(long QCMatId, long TestFormId);

        //#endregion

        //#region 质控打印
        //[ServiceContractDescription(Name = "月质控打印", Desc = "月质控打印", Url = "LabStarQCService.svc/MathQCMReport", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/MathQCMReport", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue MathQCMReport();

        //[ServiceContractDescription(Name = "月质控打印", Desc = "月质控打印", Url = "LabStarQCService.svc/ReportTempleUpload", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ReportTempleUpload", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ReportTempleUpload();
        //#endregion

        //#region LBQCPrintTemplate

        //[ServiceContractDescription(Name = "新增LB_QCPrintTemplate", Desc = "新增LB_QCPrintTemplate", Url = "SingleTableService.svc/ST_UDTO_AddLBQCPrintTemplate", Get = "", Post = "LBQCPrintTemplate", Return = "BaseResultDataValue", ReturnType = "LBQCPrintTemplate")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddLBQCPrintTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_AddLBQCPrintTemplate(LBQCPrintTemplate entity);

        //[ServiceContractDescription(Name = "修改LB_QCPrintTemplate", Desc = "修改LB_QCPrintTemplate", Url = "SingleTableService.svc/ST_UDTO_UpdateLBQCPrintTemplate", Get = "", Post = "LBQCPrintTemplate", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateLBQCPrintTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateLBQCPrintTemplate(LBQCPrintTemplate entity);

        //[ServiceContractDescription(Name = "修改LB_QCPrintTemplate指定的属性", Desc = "修改LB_QCPrintTemplate指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateLBQCPrintTemplateByField", Get = "", Post = "LBQCPrintTemplate", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateLBQCPrintTemplateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateLBQCPrintTemplateByField(LBQCPrintTemplate entity, string fields);

        //[ServiceContractDescription(Name = "删除LB_QCPrintTemplate", Desc = "删除LB_QCPrintTemplate", Url = "SingleTableService.svc/ST_UDTO_DelLBQCPrintTemplate?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelLBQCPrintTemplate?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelLBQCPrintTemplate(long id);

        //[ServiceContractDescription(Name = "查询LB_QCPrintTemplate", Desc = "查询LB_QCPrintTemplate", Url = "SingleTableService.svc/ST_UDTO_SearchLBQCPrintTemplate", Get = "", Post = "LBQCPrintTemplate", Return = "BaseResultList<LBQCPrintTemplate>", ReturnType = "ListLBQCPrintTemplate")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchLBQCPrintTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_SearchLBQCPrintTemplate(LBQCPrintTemplate entity);

        //[ServiceContractDescription(Name = "查询LB_QCPrintTemplate(HQL)", Desc = "查询LB_QCPrintTemplate(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchLBQCPrintTemplateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCPrintTemplate>", ReturnType = "ListLBQCPrintTemplate")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchLBQCPrintTemplateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_SearchLBQCPrintTemplateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询LB_QCPrintTemplate", Desc = "通过主键ID查询LB_QCPrintTemplate", Url = "SingleTableService.svc/ST_UDTO_SearchLBQCPrintTemplateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCPrintTemplate>", ReturnType = "LBQCPrintTemplate")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchLBQCPrintTemplateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_SearchLBQCPrintTemplateById(long id, string fields, bool isPlanish);
        //#endregion
    }
}
