using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Request;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation
{
    [ServiceContract]
    public interface IModuleConfigService
    {
        #region BModuleFormControlList

        [ServiceContractDescription(Name = "新增B_Module_FormControlList", Desc = "新增B_Module_FormControlList", Url = "ModuleConfigService.svc/ST_UDTO_AddBModuleFormControlList", Get = "", Post = "BModuleFormControlList", Return = "BaseResultDataValue", ReturnType = "BModuleFormControlList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBModuleFormControlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBModuleFormControlList(BModuleFormControlList entity);

        [ServiceContractDescription(Name = "修改B_Module_FormControlList", Desc = "修改B_Module_FormControlList", Url = "ModuleConfigService.svc/ST_UDTO_UpdateBModuleFormControlList", Get = "", Post = "BModuleFormControlList", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBModuleFormControlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBModuleFormControlList(BModuleFormControlList entity);

        [ServiceContractDescription(Name = "修改B_Module_FormControlList指定的属性", Desc = "修改B_Module_FormControlList指定的属性", Url = "ModuleConfigService.svc/ST_UDTO_UpdateBModuleFormControlListByField", Get = "", Post = "BModuleFormControlList", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBModuleFormControlListByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBModuleFormControlListByField(BModuleFormControlList entity, string fields);

        [ServiceContractDescription(Name = "删除B_Module_FormControlList", Desc = "删除B_Module_FormControlList", Url = "ModuleConfigService.svc/ST_UDTO_DelBModuleFormControlList?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBModuleFormControlList?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBModuleFormControlList(long id);

        [ServiceContractDescription(Name = "查询B_Module_FormControlList", Desc = "查询B_Module_FormControlList", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleFormControlList", Get = "", Post = "BModuleFormControlList", Return = "BaseResultList<BModuleFormControlList>", ReturnType = "ListBModuleFormControlList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleFormControlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleFormControlList(BModuleFormControlList entity);

        [ServiceContractDescription(Name = "查询B_Module_FormControlList(HQL)", Desc = "查询B_Module_FormControlList(HQL)", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleFormControlListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BModuleFormControlList>", ReturnType = "ListBModuleFormControlList")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleFormControlListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleFormControlListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_Module_FormControlList", Desc = "通过主键ID查询B_Module_FormControlList", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleFormControlListById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BModuleFormControlList>", ReturnType = "BModuleFormControlList")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleFormControlListById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleFormControlListById(long id, string fields, bool isPlanish);
        #endregion

        #region BModuleGridList

        [ServiceContractDescription(Name = "新增B_Module_GridList", Desc = "新增B_Module_GridList", Url = "ModuleConfigService.svc/ST_UDTO_AddBModuleGridList", Get = "", Post = "BModuleGridList", Return = "BaseResultDataValue", ReturnType = "BModuleGridList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBModuleGridList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBModuleGridList(BModuleGridList entity);

        [ServiceContractDescription(Name = "修改B_Module_GridList", Desc = "修改B_Module_GridList", Url = "ModuleConfigService.svc/ST_UDTO_UpdateBModuleGridList", Get = "", Post = "BModuleGridList", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBModuleGridList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBModuleGridList(BModuleGridList entity);

        [ServiceContractDescription(Name = "修改B_Module_GridList指定的属性", Desc = "修改B_Module_GridList指定的属性", Url = "ModuleConfigService.svc/ST_UDTO_UpdateBModuleGridListByField", Get = "", Post = "BModuleGridList", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBModuleGridListByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBModuleGridListByField(BModuleGridList entity, string fields);

        [ServiceContractDescription(Name = "删除B_Module_GridList", Desc = "删除B_Module_GridList", Url = "ModuleConfigService.svc/ST_UDTO_DelBModuleGridList?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBModuleGridList?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBModuleGridList(long id);

        [ServiceContractDescription(Name = "查询B_Module_GridList", Desc = "查询B_Module_GridList", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleGridList", Get = "", Post = "BModuleGridList", Return = "BaseResultList<BModuleGridList>", ReturnType = "ListBModuleGridList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleGridList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleGridList(BModuleGridList entity);

        [ServiceContractDescription(Name = "查询B_Module_GridList(HQL)", Desc = "查询B_Module_GridList(HQL)", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleGridListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BModuleGridList>", ReturnType = "ListBModuleGridList")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleGridListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleGridListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_Module_GridList", Desc = "通过主键ID查询B_Module_GridList", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleGridListById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BModuleGridList>", ReturnType = "BModuleGridList")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleGridListById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleGridListById(long id, string fields, bool isPlanish);
        #endregion
        #region BModuleGridControlSet

        [ServiceContractDescription(Name = "新增B_Module_GridControlSet", Desc = "新增B_Module_GridControlSet", Url = "ModuleConfigService.svc/ST_UDTO_AddBModuleGridControlSet", Get = "", Post = "BModuleGridControlSet", Return = "BaseResultDataValue", ReturnType = "BModuleGridControlSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBModuleGridControlSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBModuleGridControlSet(BModuleGridControlSet entity);

        [ServiceContractDescription(Name = "修改B_Module_GridControlSet", Desc = "修改B_Module_GridControlSet", Url = "ModuleConfigService.svc/ST_UDTO_UpdateBModuleGridControlSet", Get = "", Post = "BModuleGridControlSet", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBModuleGridControlSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBModuleGridControlSet(BModuleGridControlSet entity);

        [ServiceContractDescription(Name = "修改B_Module_GridControlSet指定的属性", Desc = "修改B_Module_GridControlSet指定的属性", Url = "ModuleConfigService.svc/ST_UDTO_UpdateBModuleGridControlSetByField", Get = "", Post = "BModuleGridControlSet", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBModuleGridControlSetByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBModuleGridControlSetByField(BModuleGridControlSet entity, string fields);

        [ServiceContractDescription(Name = "删除B_Module_GridControlSet", Desc = "删除B_Module_GridControlSet", Url = "ModuleConfigService.svc/ST_UDTO_DelBModuleGridControlSet?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBModuleGridControlSet?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBModuleGridControlSet(long id);

        [ServiceContractDescription(Name = "查询B_Module_GridControlSet", Desc = "查询B_Module_GridControlSet", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleGridControlSet", Get = "", Post = "BModuleGridControlSet", Return = "BaseResultList<BModuleGridControlSet>", ReturnType = "ListBModuleGridControlSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleGridControlSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleGridControlSet(BModuleGridControlSet entity);

        [ServiceContractDescription(Name = "查询B_Module_GridControlSet(HQL)", Desc = "查询B_Module_GridControlSet(HQL)", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleGridControlSetByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BModuleGridControlSet>", ReturnType = "ListBModuleGridControlSet")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleGridControlSetByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleGridControlSetByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_Module_GridControlSet", Desc = "通过主键ID查询B_Module_GridControlSet", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleGridControlSetById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BModuleGridControlSet>", ReturnType = "BModuleGridControlSet")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleGridControlSetById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleGridControlSetById(long id, string fields, bool isPlanish);
        #endregion
        #region BModuleGridControlList

        [ServiceContractDescription(Name = "新增B_Module_GridControlList", Desc = "新增B_Module_GridControlList", Url = "ModuleConfigService.svc/ST_UDTO_AddBModuleGridControlList", Get = "", Post = "BModuleGridControlList", Return = "BaseResultDataValue", ReturnType = "BModuleGridControlList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBModuleGridControlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBModuleGridControlList(BModuleGridControlList entity);

        [ServiceContractDescription(Name = "修改B_Module_GridControlList", Desc = "修改B_Module_GridControlList", Url = "ModuleConfigService.svc/ST_UDTO_UpdateBModuleGridControlList", Get = "", Post = "BModuleGridControlList", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBModuleGridControlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBModuleGridControlList(BModuleGridControlList entity);

        [ServiceContractDescription(Name = "修改B_Module_GridControlList指定的属性", Desc = "修改B_Module_GridControlList指定的属性", Url = "ModuleConfigService.svc/ST_UDTO_UpdateBModuleGridControlListByField", Get = "", Post = "BModuleGridControlList", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBModuleGridControlListByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBModuleGridControlListByField(BModuleGridControlList entity, string fields);

        [ServiceContractDescription(Name = "删除B_Module_GridControlList", Desc = "删除B_Module_GridControlList", Url = "ModuleConfigService.svc/ST_UDTO_DelBModuleGridControlList?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBModuleGridControlList?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBModuleGridControlList(long id);

        [ServiceContractDescription(Name = "查询B_Module_GridControlList", Desc = "查询B_Module_GridControlList", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleGridControlList", Get = "", Post = "BModuleGridControlList", Return = "BaseResultList<BModuleGridControlList>", ReturnType = "ListBModuleGridControlList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleGridControlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleGridControlList(BModuleGridControlList entity);

        [ServiceContractDescription(Name = "查询B_Module_GridControlList(HQL)", Desc = "查询B_Module_GridControlList(HQL)", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleGridControlListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BModuleGridControlList>", ReturnType = "ListBModuleGridControlList")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleGridControlListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleGridControlListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_Module_GridControlList", Desc = "通过主键ID查询B_Module_GridControlList", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleGridControlListById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BModuleGridControlList>", ReturnType = "BModuleGridControlList")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleGridControlListById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleGridControlListById(long id, string fields, bool isPlanish);
        #endregion
        #region BModuleFormList

        [ServiceContractDescription(Name = "新增B_Module_FormList", Desc = "新增B_Module_FormList", Url = "ModuleConfigService.svc/ST_UDTO_AddBModuleFormList", Get = "", Post = "BModuleFormList", Return = "BaseResultDataValue", ReturnType = "BModuleFormList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBModuleFormList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBModuleFormList(BModuleFormList entity);

        [ServiceContractDescription(Name = "修改B_Module_FormList", Desc = "修改B_Module_FormList", Url = "ModuleConfigService.svc/ST_UDTO_UpdateBModuleFormList", Get = "", Post = "BModuleFormList", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBModuleFormList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBModuleFormList(BModuleFormList entity);

        [ServiceContractDescription(Name = "修改B_Module_FormList指定的属性", Desc = "修改B_Module_FormList指定的属性", Url = "ModuleConfigService.svc/ST_UDTO_UpdateBModuleFormListByField", Get = "", Post = "BModuleFormList", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBModuleFormListByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBModuleFormListByField(BModuleFormList entity, string fields);

        [ServiceContractDescription(Name = "删除B_Module_FormList", Desc = "删除B_Module_FormList", Url = "ModuleConfigService.svc/ST_UDTO_DelBModuleFormList?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBModuleFormList?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBModuleFormList(long id);

        [ServiceContractDescription(Name = "查询B_Module_FormList", Desc = "查询B_Module_FormList", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleFormList", Get = "", Post = "BModuleFormList", Return = "BaseResultList<BModuleFormList>", ReturnType = "ListBModuleFormList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleFormList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleFormList(BModuleFormList entity);

        [ServiceContractDescription(Name = "查询B_Module_FormList(HQL)", Desc = "查询B_Module_FormList(HQL)", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleFormListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BModuleFormList>", ReturnType = "ListBModuleFormList")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleFormListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleFormListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_Module_FormList", Desc = "通过主键ID查询B_Module_FormList", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleFormListById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BModuleFormList>", ReturnType = "BModuleFormList")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleFormListById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleFormListById(long id, string fields, bool isPlanish);
        #endregion
        #region BModuleFormControlSet

        [ServiceContractDescription(Name = "新增B_Module_FormControlSet", Desc = "新增B_Module_FormControlSet", Url = "ModuleConfigService.svc/ST_UDTO_AddBModuleFormControlSet", Get = "", Post = "BModuleFormControlSet", Return = "BaseResultDataValue", ReturnType = "BModuleFormControlSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBModuleFormControlSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBModuleFormControlSet(BModuleFormControlSet entity);

        [ServiceContractDescription(Name = "修改B_Module_FormControlSet", Desc = "修改B_Module_FormControlSet", Url = "ModuleConfigService.svc/ST_UDTO_UpdateBModuleFormControlSet", Get = "", Post = "BModuleFormControlSet", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBModuleFormControlSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBModuleFormControlSet(BModuleFormControlSet entity);

        [ServiceContractDescription(Name = "修改B_Module_FormControlSet指定的属性", Desc = "修改B_Module_FormControlSet指定的属性", Url = "ModuleConfigService.svc/ST_UDTO_UpdateBModuleFormControlSetByField", Get = "", Post = "BModuleFormControlSet", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBModuleFormControlSetByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBModuleFormControlSetByField(BModuleFormControlSet entity, string fields);

        [ServiceContractDescription(Name = "删除B_Module_FormControlSet", Desc = "删除B_Module_FormControlSet", Url = "ModuleConfigService.svc/ST_UDTO_DelBModuleFormControlSet?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBModuleFormControlSet?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBModuleFormControlSet(long id);

        [ServiceContractDescription(Name = "查询B_Module_FormControlSet", Desc = "查询B_Module_FormControlSet", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleFormControlSet", Get = "", Post = "BModuleFormControlSet", Return = "BaseResultList<BModuleFormControlSet>", ReturnType = "ListBModuleFormControlSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleFormControlSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleFormControlSet(BModuleFormControlSet entity);

        [ServiceContractDescription(Name = "查询B_Module_FormControlSet(HQL)", Desc = "查询B_Module_FormControlSet(HQL)", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleFormControlSetByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BModuleFormControlSet>", ReturnType = "ListBModuleFormControlSet")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleFormControlSetByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleFormControlSetByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_Module_FormControlSet", Desc = "通过主键ID查询B_Module_FormControlSet", Url = "ModuleConfigService.svc/ST_UDTO_SearchBModuleFormControlSetById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BModuleFormControlSet>", ReturnType = "BModuleFormControlSet")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBModuleFormControlSetById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBModuleFormControlSetById(long id, string fields, bool isPlanish);
        #endregion


        #region 动态配置
        [ServiceContractDescription(Name = "获取模块表单配置并组合表单控件集合中的属性", Desc = "获取模块表单配置并组合表单控件集合中的属性", Url = "ModuleConfigService.svc/SearchBModuleFormControlSetListByFormCode?Where={Where}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Get = "Where={Where}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BModuleFormControlList>", ReturnType = "ListBModuleFormControlList")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchBModuleFormControlSetListByFormCode?FormCode={FormCode}&fields={fields}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SearchBModuleFormControlSetListByFormCode(string FormCode, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取模块列表配置并组合列表控件集合中的属性", Desc = "获取模块列表配置并组合列表控件集合中的属性", Url = "ModuleConfigService.svc/SearchBModuleGridControlListByGridCode?Where={Where}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Get = "Where={Where}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchBModuleGridControlListByGridCode?GridCode={GridCode}&fields={fields}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SearchBModuleGridControlListByGridCode(string GridCode, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "根据模块编码查询模块（如模块不存在则返回模块编码）", Desc = "根据模块编码查询模块（如模块不存在则返回模块编码）", Url = "ModuleConfigService.svc/SearchModuleAggregateList?GridCodes={GridCodes}&FormCodes={FormCodes}&CheartCodes={CheartCodes}", Get = "GridCodes={GridCodes}&FormCodes={FormCodes}&CheartCodes={CheartCodes}", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchModuleAggregateList?GridCodes={GridCodes}&FormCodes={FormCodes}&CheartCodes={CheartCodes}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SearchModuleAggregateList(string GridCodes, string FormCodes, string CheartCodes);

        [ServiceContractDescription(Name = "批量新增 表格配置", Desc = "批量新增 表格配置", Url = "ModuleConfigService.svc/AddBModuleGridControlSets", Get = "", Post = "BModuleFormControlSet", Return = "BaseResultDataValue", ReturnType = "BModuleFormControlSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddBModuleGridControlSets", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool AddBModuleGridControlSets(List<BModuleGridControlSet> BModuleGridControlSets);

        [ServiceContractDescription(Name = "批量新增 表单配置", Desc = "批量新增 表单配置", Url = "ModuleConfigService.svc/AddBModuleFormControlSets", Get = "", Post = "BModuleFormControlSet", Return = "BaseResultDataValue", ReturnType = "BModuleFormControlSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddBModuleFormControlSets", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool AddBModuleFormControlSets(List<BModuleFormControlSet> BModuleFormControlSets);

        [ServiceContractDescription(Name = "批量修改 表单配置", Desc = "批量修改 表单配置", Url = "ModuleConfigService.svc/EditBModuleFormControlSets", Get = "", Post = "BModuleFormControlSet", Return = "BaseResultDataValue", ReturnType = "BModuleFormControlSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/EditBModuleFormControlSets", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool EditBModuleFormControlSets(List<BModuleFormControlSetVO> BModuleFormControlSetVOs);

        [ServiceContractDescription(Name = "批量修改 表格配置", Desc = "批量修改 表格配置", Url = "ModuleConfigService.svc/EditBModuleGridControlSets", Get = "", Post = "BModuleFormControlSet", Return = "BaseResultDataValue", ReturnType = "BModuleFormControlSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/EditBModuleGridControlSets", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool EditBModuleGridControlSets(List<BModuleGridControlSetVO> BModuleGridControlSetVOs);


        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetModuleConfigDefault", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool GetModuleConfigDefault(string key, string type);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateModuleConfigDefault", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool UpdateModuleConfigDefault();


        //测试用
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SetModuleConfigDefault", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool SetModuleConfigDefault();

        #endregion
    }
}
