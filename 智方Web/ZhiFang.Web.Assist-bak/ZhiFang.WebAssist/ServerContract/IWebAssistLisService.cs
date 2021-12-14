using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.Entity.RBAC;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.WebAssist.ServerContract
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IWebAssistLisService”。
    [ServiceContract]
    public interface IWebAssistLisService
    {
        [OperationContract]
        void DoWork();

        #region Doctor

        [ServiceContractDescription(Name = "新增Doctor", Desc = "新增Doctor", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddDoctor", Get = "", Post = "Doctor", Return = "BaseResultDataValue", ReturnType = "Doctor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddDoctor(Doctor entity);

        [ServiceContractDescription(Name = "修改Doctor", Desc = "修改Doctor", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateDoctor", Get = "", Post = "Doctor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateDoctor(Doctor entity);

        [ServiceContractDescription(Name = "修改Doctor指定的属性", Desc = "修改Doctor指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateDoctorByField", Get = "", Post = "Doctor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateDoctorByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateDoctorByField(Doctor entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除Doctor", Desc = "删除Doctor", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelDoctor?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelDoctor?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelDoctor(int id);

        [ServiceContractDescription(Name = "查询Doctor", Desc = "查询Doctor", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchDoctor", Get = "", Post = "Doctor", Return = "BaseResultList<Doctor>", ReturnType = "ListDoctor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchDoctor(Doctor entity);

        [ServiceContractDescription(Name = "查询Doctor(HQL)", Desc = "查询Doctor(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchDoctorByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Doctor>", ReturnType = "ListDoctor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchDoctorByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchDoctorByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Doctor", Desc = "通过主键ID查询Doctor", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchDoctorById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Doctor>", ReturnType = "Doctor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchDoctorById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchDoctorById(int id, string fields, bool isPlanish);
        #endregion

        #region NPUser

        [ServiceContractDescription(Name = "新增NPUser", Desc = "新增NPUser", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddNPUser", Get = "", Post = "NPUser", Return = "BaseResultDataValue", ReturnType = "NPUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddNPUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddNPUser(NPUser entity);

        [ServiceContractDescription(Name = "修改NPUser", Desc = "修改NPUser", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateNPUser", Get = "", Post = "NPUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateNPUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateNPUser(NPUser entity);

        [ServiceContractDescription(Name = "修改NPUser指定的属性", Desc = "修改NPUser指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateNPUserByField", Get = "", Post = "NPUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateNPUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateNPUserByField(NPUser entity, string fields);

        [ServiceContractDescription(Name = "删除NPUser", Desc = "删除NPUser", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelNPUser?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelNPUser?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelNPUser(int id);

        [ServiceContractDescription(Name = "查询NPUser", Desc = "查询NPUser", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchNPUser", Get = "", Post = "NPUser", Return = "BaseResultList<NPUser>", ReturnType = "ListNPUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchNPUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchNPUser(NPUser entity);

        [ServiceContractDescription(Name = "查询NPUser(HQL)", Desc = "查询NPUser(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchNPUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<NPUser>", ReturnType = "ListNPUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchNPUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchNPUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询NPUser", Desc = "通过主键ID查询NPUser", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchNPUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<NPUser>", ReturnType = "NPUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchNPUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchNPUserById(int id, string fields, bool isPlanish);
        #endregion

        #region Department

        [ServiceContractDescription(Name = "新增Department", Desc = "新增Department", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddDepartment", Get = "", Post = "Department", Return = "BaseResultDataValue", ReturnType = "Department")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddDepartment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddDepartment(Department entity);

        [ServiceContractDescription(Name = "修改Department", Desc = "修改Department", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateDepartment", Get = "", Post = "Department", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateDepartment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateDepartment(Department entity);

        [ServiceContractDescription(Name = "修改Department指定的属性", Desc = "修改Department指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateDepartmentByField", Get = "", Post = "Department", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateDepartmentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateDepartmentByField(Department entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除Department", Desc = "删除Department", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelDepartment?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelDepartment?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelDepartment(int id);

        [ServiceContractDescription(Name = "查询Department", Desc = "查询Department", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchDepartment", Get = "", Post = "Department", Return = "BaseResultList<Department>", ReturnType = "ListDepartment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchDepartment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchDepartment(Department entity);

        [ServiceContractDescription(Name = "查询Department(HQL)", Desc = "查询Department(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchDepartmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Department>", ReturnType = "ListDepartment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchDepartmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchDepartmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Department", Desc = "通过主键ID查询Department", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchDepartmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Department>", ReturnType = "Department")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchDepartmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchDepartmentById(int id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "按申请信息建立病区与科室关系", Desc = "按申请信息建立病区与科室关系", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddWarpAndDept", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_AddWarpAndDept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_AddWarpAndDept();

        #endregion

        #region DepartmentUser

        [ServiceContractDescription(Name = "新增DepartmentUser", Desc = "新增DepartmentUser", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddDepartmentUser", Get = "", Post = "DepartmentUser", Return = "BaseResultDataValue", ReturnType = "DepartmentUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddDepartmentUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddDepartmentUser(DepartmentUser entity);

        [ServiceContractDescription(Name = "修改DepartmentUser", Desc = "修改DepartmentUser", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateDepartmentUser", Get = "", Post = "DepartmentUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateDepartmentUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateDepartmentUser(DepartmentUser entity);

        [ServiceContractDescription(Name = "修改DepartmentUser指定的属性", Desc = "修改DepartmentUser指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateDepartmentUserByField", Get = "", Post = "DepartmentUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateDepartmentUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateDepartmentUserByField(DepartmentUser entity, string fields);

        [ServiceContractDescription(Name = "删除DepartmentUser", Desc = "删除DepartmentUser", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelDepartmentUser?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelDepartmentUser?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelDepartmentUser(long id);

        [ServiceContractDescription(Name = "查询DepartmentUser", Desc = "查询DepartmentUser", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchDepartmentUser", Get = "", Post = "DepartmentUser", Return = "BaseResultList<DepartmentUser>", ReturnType = "ListDepartmentUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchDepartmentUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchDepartmentUser(DepartmentUser entity);

        [ServiceContractDescription(Name = "查询DepartmentUser(HQL)", Desc = "查询DepartmentUser(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchDepartmentUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<DepartmentUser>", ReturnType = "ListDepartmentUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchDepartmentUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchDepartmentUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询DepartmentUser", Desc = "通过主键ID查询DepartmentUser", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchDepartmentUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<DepartmentUser>", ReturnType = "DepartmentUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchDepartmentUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchDepartmentUserById(long id, string fields, bool isPlanish);
        #endregion

        #region WardType

        [ServiceContractDescription(Name = "查询WardType", Desc = "查询WardType", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchWardType", Get = "", Post = "WardType", Return = "BaseResultList<WardType>", ReturnType = "ListWardType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchWardType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchWardType(WardType entity);

        [ServiceContractDescription(Name = "查询WardType(HQL)", Desc = "查询WardType(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchWardTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WardType>", ReturnType = "ListWardType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchWardTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchWardTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询WardType", Desc = "通过主键ID查询WardType", Url = "WebAssistLisService.svc/WA_UDTO_SearchWardTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WardType>", ReturnType = "WardType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WebAssistLisService?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchWardTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region AntiGroup

        [ServiceContractDescription(Name = "新增AntiGroup", Desc = "新增AntiGroup", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddAntiGroup", Get = "", Post = "AntiGroup", Return = "BaseResultDataValue", ReturnType = "AntiGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddAntiGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddAntiGroup(AntiGroup entity);

        [ServiceContractDescription(Name = "修改AntiGroup", Desc = "修改AntiGroup", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateAntiGroup", Get = "", Post = "AntiGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateAntiGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateAntiGroup(AntiGroup entity);

        [ServiceContractDescription(Name = "修改AntiGroup指定的属性", Desc = "修改AntiGroup指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateAntiGroupByField", Get = "", Post = "AntiGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateAntiGroupByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateAntiGroupByField(AntiGroup entity, string fields);

        [ServiceContractDescription(Name = "删除AntiGroup", Desc = "删除AntiGroup", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelAntiGroup?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelAntiGroup?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelAntiGroup(long id);

        [ServiceContractDescription(Name = "查询AntiGroup", Desc = "查询AntiGroup", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchAntiGroup", Get = "", Post = "AntiGroup", Return = "BaseResultList<AntiGroup>", ReturnType = "ListAntiGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchAntiGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchAntiGroup(AntiGroup entity);

        [ServiceContractDescription(Name = "查询AntiGroup(HQL)", Desc = "查询AntiGroup(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchAntiGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AntiGroup>", ReturnType = "ListAntiGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchAntiGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchAntiGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询AntiGroup", Desc = "通过主键ID查询AntiGroup", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchAntiGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AntiGroup>", ReturnType = "AntiGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchAntiGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchAntiGroupById(long id, string fields, bool isPlanish);
        #endregion

        #region ChargeType

        [ServiceContractDescription(Name = "新增ChargeType", Desc = "新增ChargeType", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddChargeType", Get = "", Post = "ChargeType", Return = "BaseResultDataValue", ReturnType = "ChargeType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddChargeType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddChargeType(ChargeType entity);

        [ServiceContractDescription(Name = "修改ChargeType", Desc = "修改ChargeType", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateChargeType", Get = "", Post = "ChargeType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateChargeType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateChargeType(ChargeType entity);

        [ServiceContractDescription(Name = "修改ChargeType指定的属性", Desc = "修改ChargeType指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateChargeTypeByField", Get = "", Post = "ChargeType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateChargeTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateChargeTypeByField(ChargeType entity, string fields);

        [ServiceContractDescription(Name = "删除ChargeType", Desc = "删除ChargeType", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelChargeType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelChargeType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelChargeType(long id);

        [ServiceContractDescription(Name = "查询ChargeType", Desc = "查询ChargeType", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchChargeType", Get = "", Post = "ChargeType", Return = "BaseResultList<ChargeType>", ReturnType = "ListChargeType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchChargeType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchChargeType(ChargeType entity);

        [ServiceContractDescription(Name = "查询ChargeType(HQL)", Desc = "查询ChargeType(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchChargeTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ChargeType>", ReturnType = "ListChargeType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchChargeTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchChargeTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询ChargeType", Desc = "通过主键ID查询ChargeType", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchChargeTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ChargeType>", ReturnType = "ChargeType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchChargeTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchChargeTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region SamplingGroup

        [ServiceContractDescription(Name = "新增SamplingGroup", Desc = "新增SamplingGroup", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddSamplingGroup", Get = "", Post = "SamplingGroup", Return = "BaseResultDataValue", ReturnType = "SamplingGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddSamplingGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddSamplingGroup(SamplingGroup entity);

        [ServiceContractDescription(Name = "修改SamplingGroup", Desc = "修改SamplingGroup", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateSamplingGroup", Get = "", Post = "SamplingGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSamplingGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSamplingGroup(SamplingGroup entity);

        [ServiceContractDescription(Name = "修改SamplingGroup指定的属性", Desc = "修改SamplingGroup指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateSamplingGroupByField", Get = "", Post = "SamplingGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSamplingGroupByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSamplingGroupByField(SamplingGroup entity, string fields);

        [ServiceContractDescription(Name = "删除SamplingGroup", Desc = "删除SamplingGroup", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelSamplingGroup?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelSamplingGroup?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelSamplingGroup(long id);

        [ServiceContractDescription(Name = "查询SamplingGroup", Desc = "查询SamplingGroup", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchSamplingGroup", Get = "", Post = "SamplingGroup", Return = "BaseResultList<SamplingGroup>", ReturnType = "ListSamplingGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSamplingGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSamplingGroup(SamplingGroup entity);

        [ServiceContractDescription(Name = "查询SamplingGroup(HQL)", Desc = "查询SamplingGroup(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchSamplingGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SamplingGroup>", ReturnType = "ListSamplingGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSamplingGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSamplingGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询SamplingGroup", Desc = "通过主键ID查询SamplingGroup", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchSamplingGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SamplingGroup>", ReturnType = "SamplingGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSamplingGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSamplingGroupById(long id, string fields, bool isPlanish);
        #endregion

        #region Samplingitem

        [ServiceContractDescription(Name = "新增Samplingitem", Desc = "新增Samplingitem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddSamplingitem", Get = "", Post = "Samplingitem", Return = "BaseResultDataValue", ReturnType = "Samplingitem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddSamplingitem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddSamplingitem(Samplingitem entity);

        [ServiceContractDescription(Name = "修改Samplingitem", Desc = "修改Samplingitem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateSamplingitem", Get = "", Post = "Samplingitem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSamplingitem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSamplingitem(Samplingitem entity);

        [ServiceContractDescription(Name = "修改Samplingitem指定的属性", Desc = "修改Samplingitem指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateSamplingitemByField", Get = "", Post = "Samplingitem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSamplingitemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSamplingitemByField(Samplingitem entity, string fields);

        [ServiceContractDescription(Name = "删除Samplingitem", Desc = "删除Samplingitem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelSamplingitem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelSamplingitem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelSamplingitem(long id);

        [ServiceContractDescription(Name = "查询Samplingitem", Desc = "查询Samplingitem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchSamplingitem", Get = "", Post = "Samplingitem", Return = "BaseResultList<Samplingitem>", ReturnType = "ListSamplingitem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSamplingitem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSamplingitem(Samplingitem entity);

        [ServiceContractDescription(Name = "查询Samplingitem(HQL)", Desc = "查询Samplingitem(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchSamplingitemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Samplingitem>", ReturnType = "ListSamplingitem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSamplingitemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSamplingitemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Samplingitem", Desc = "通过主键ID查询Samplingitem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchSamplingitemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Samplingitem>", ReturnType = "Samplingitem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSamplingitemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSamplingitemById(long id, string fields, bool isPlanish);
        #endregion

        #region SectionItem

        [ServiceContractDescription(Name = "新增SectionItem", Desc = "新增SectionItem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddSectionItem", Get = "", Post = "SectionItem", Return = "BaseResultDataValue", ReturnType = "SectionItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddSectionItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddSectionItem(SectionItem entity);

        [ServiceContractDescription(Name = "修改SectionItem", Desc = "修改SectionItem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateSectionItem", Get = "", Post = "SectionItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSectionItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSectionItem(SectionItem entity);

        [ServiceContractDescription(Name = "修改SectionItem指定的属性", Desc = "修改SectionItem指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateSectionItemByField", Get = "", Post = "SectionItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSectionItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSectionItemByField(SectionItem entity, string fields);

        [ServiceContractDescription(Name = "删除SectionItem", Desc = "删除SectionItem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelSectionItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelSectionItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelSectionItem(long id);

        [ServiceContractDescription(Name = "查询SectionItem", Desc = "查询SectionItem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchSectionItem", Get = "", Post = "SectionItem", Return = "BaseResultList<SectionItem>", ReturnType = "ListSectionItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSectionItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSectionItem(SectionItem entity);

        [ServiceContractDescription(Name = "查询SectionItem(HQL)", Desc = "查询SectionItem(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchSectionItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SectionItem>", ReturnType = "ListSectionItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSectionItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSectionItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询SectionItem", Desc = "通过主键ID查询SectionItem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchSectionItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SectionItem>", ReturnType = "SectionItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSectionItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSectionItemById(long id, string fields, bool isPlanish);
        #endregion

        #region StatusType

        [ServiceContractDescription(Name = "新增StatusType", Desc = "新增StatusType", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddStatusType", Get = "", Post = "StatusType", Return = "BaseResultDataValue", ReturnType = "StatusType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddStatusType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddStatusType(StatusType entity);

        [ServiceContractDescription(Name = "修改StatusType", Desc = "修改StatusType", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateStatusType", Get = "", Post = "StatusType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateStatusType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateStatusType(StatusType entity);

        [ServiceContractDescription(Name = "修改StatusType指定的属性", Desc = "修改StatusType指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateStatusTypeByField", Get = "", Post = "StatusType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateStatusTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateStatusTypeByField(StatusType entity, string fields);

        [ServiceContractDescription(Name = "删除StatusType", Desc = "删除StatusType", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelStatusType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelStatusType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelStatusType(long id);

        [ServiceContractDescription(Name = "查询StatusType", Desc = "查询StatusType", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchStatusType", Get = "", Post = "StatusType", Return = "BaseResultList<StatusType>", ReturnType = "ListStatusType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchStatusType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchStatusType(StatusType entity);

        [ServiceContractDescription(Name = "查询StatusType(HQL)", Desc = "查询StatusType(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchStatusTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<StatusType>", ReturnType = "ListStatusType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchStatusTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchStatusTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询StatusType", Desc = "通过主键ID查询StatusType", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchStatusTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<StatusType>", ReturnType = "StatusType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchStatusTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchStatusTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region MDMicroTestDevelop

        [ServiceContractDescription(Name = "新增MD_MicroTestDevelop", Desc = "新增MD_MicroTestDevelop", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddMDMicroTestDevelop", Get = "", Post = "MDMicroTestDevelop", Return = "BaseResultDataValue", ReturnType = "MDMicroTestDevelop")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddMDMicroTestDevelop", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddMDMicroTestDevelop(MDMicroTestDevelop entity);

        [ServiceContractDescription(Name = "修改MD_MicroTestDevelop", Desc = "修改MD_MicroTestDevelop", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateMDMicroTestDevelop", Get = "", Post = "MDMicroTestDevelop", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateMDMicroTestDevelop", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateMDMicroTestDevelop(MDMicroTestDevelop entity);

        [ServiceContractDescription(Name = "修改MD_MicroTestDevelop指定的属性", Desc = "修改MD_MicroTestDevelop指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateMDMicroTestDevelopByField", Get = "", Post = "MDMicroTestDevelop", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateMDMicroTestDevelopByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateMDMicroTestDevelopByField(MDMicroTestDevelop entity, string fields);

        [ServiceContractDescription(Name = "删除MD_MicroTestDevelop", Desc = "删除MD_MicroTestDevelop", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelMDMicroTestDevelop?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelMDMicroTestDevelop?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelMDMicroTestDevelop(long id);

        [ServiceContractDescription(Name = "查询MD_MicroTestDevelop", Desc = "查询MD_MicroTestDevelop", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMDMicroTestDevelop", Get = "", Post = "MDMicroTestDevelop", Return = "BaseResultList<MDMicroTestDevelop>", ReturnType = "ListMDMicroTestDevelop")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMDMicroTestDevelop", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMDMicroTestDevelop(MDMicroTestDevelop entity);

        [ServiceContractDescription(Name = "查询MD_MicroTestDevelop(HQL)", Desc = "查询MD_MicroTestDevelop(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMDMicroTestDevelopByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<MDMicroTestDevelop>", ReturnType = "ListMDMicroTestDevelop")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMDMicroTestDevelopByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMDMicroTestDevelopByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询MD_MicroTestDevelop", Desc = "通过主键ID查询MD_MicroTestDevelop", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMDMicroTestDevelopById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<MDMicroTestDevelop>", ReturnType = "MDMicroTestDevelop")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMDMicroTestDevelopById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMDMicroTestDevelopById(long id, string fields, bool isPlanish);
        #endregion

        #region MDMicroTestValue

        [ServiceContractDescription(Name = "新增MD_MicroTestValue", Desc = "新增MD_MicroTestValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddMDMicroTestValue", Get = "", Post = "MDMicroTestValue", Return = "BaseResultDataValue", ReturnType = "MDMicroTestValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddMDMicroTestValue", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddMDMicroTestValue(MDMicroTestValue entity);

        [ServiceContractDescription(Name = "修改MD_MicroTestValue", Desc = "修改MD_MicroTestValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateMDMicroTestValue", Get = "", Post = "MDMicroTestValue", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateMDMicroTestValue", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateMDMicroTestValue(MDMicroTestValue entity);

        [ServiceContractDescription(Name = "修改MD_MicroTestValue指定的属性", Desc = "修改MD_MicroTestValue指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateMDMicroTestValueByField", Get = "", Post = "MDMicroTestValue", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateMDMicroTestValueByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateMDMicroTestValueByField(MDMicroTestValue entity, string fields);

        [ServiceContractDescription(Name = "删除MD_MicroTestValue", Desc = "删除MD_MicroTestValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelMDMicroTestValue?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelMDMicroTestValue?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelMDMicroTestValue(long id);

        [ServiceContractDescription(Name = "查询MD_MicroTestValue", Desc = "查询MD_MicroTestValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMDMicroTestValue", Get = "", Post = "MDMicroTestValue", Return = "BaseResultList<MDMicroTestValue>", ReturnType = "ListMDMicroTestValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMDMicroTestValue", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMDMicroTestValue(MDMicroTestValue entity);

        [ServiceContractDescription(Name = "查询MD_MicroTestValue(HQL)", Desc = "查询MD_MicroTestValue(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMDMicroTestValueByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<MDMicroTestValue>", ReturnType = "ListMDMicroTestValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMDMicroTestValueByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMDMicroTestValueByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询MD_MicroTestValue", Desc = "通过主键ID查询MD_MicroTestValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMDMicroTestValueById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<MDMicroTestValue>", ReturnType = "MDMicroTestValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMDMicroTestValueById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMDMicroTestValueById(long id, string fields, bool isPlanish);
        #endregion

        #region MEMicroAppraisalValue

        [ServiceContractDescription(Name = "新增ME_MicroAppraisalValue", Desc = "新增ME_MicroAppraisalValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddMEMicroAppraisalValue", Get = "", Post = "MEMicroAppraisalValue", Return = "BaseResultDataValue", ReturnType = "MEMicroAppraisalValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddMEMicroAppraisalValue", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddMEMicroAppraisalValue(MEMicroAppraisalValue entity);

        [ServiceContractDescription(Name = "修改ME_MicroAppraisalValue", Desc = "修改ME_MicroAppraisalValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateMEMicroAppraisalValue", Get = "", Post = "MEMicroAppraisalValue", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateMEMicroAppraisalValue", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateMEMicroAppraisalValue(MEMicroAppraisalValue entity);

        [ServiceContractDescription(Name = "修改ME_MicroAppraisalValue指定的属性", Desc = "修改ME_MicroAppraisalValue指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateMEMicroAppraisalValueByField", Get = "", Post = "MEMicroAppraisalValue", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateMEMicroAppraisalValueByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateMEMicroAppraisalValueByField(MEMicroAppraisalValue entity, string fields);

        [ServiceContractDescription(Name = "删除ME_MicroAppraisalValue", Desc = "删除ME_MicroAppraisalValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelMEMicroAppraisalValue?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelMEMicroAppraisalValue?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelMEMicroAppraisalValue(long id);

        [ServiceContractDescription(Name = "查询ME_MicroAppraisalValue", Desc = "查询ME_MicroAppraisalValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMEMicroAppraisalValue", Get = "", Post = "MEMicroAppraisalValue", Return = "BaseResultList<MEMicroAppraisalValue>", ReturnType = "ListMEMicroAppraisalValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMEMicroAppraisalValue", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMEMicroAppraisalValue(MEMicroAppraisalValue entity);

        [ServiceContractDescription(Name = "查询ME_MicroAppraisalValue(HQL)", Desc = "查询ME_MicroAppraisalValue(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMEMicroAppraisalValueByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<MEMicroAppraisalValue>", ReturnType = "ListMEMicroAppraisalValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMEMicroAppraisalValueByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMEMicroAppraisalValueByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询ME_MicroAppraisalValue", Desc = "通过主键ID查询ME_MicroAppraisalValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMEMicroAppraisalValueById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<MEMicroAppraisalValue>", ReturnType = "MEMicroAppraisalValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMEMicroAppraisalValueById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMEMicroAppraisalValueById(long id, string fields, bool isPlanish);
        #endregion

        #region MEMicroDSTValue

        [ServiceContractDescription(Name = "新增ME_MicroDSTValue", Desc = "新增ME_MicroDSTValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddMEMicroDSTValue", Get = "", Post = "MEMicroDSTValue", Return = "BaseResultDataValue", ReturnType = "MEMicroDSTValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddMEMicroDSTValue", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddMEMicroDSTValue(MEMicroDSTValue entity);

        [ServiceContractDescription(Name = "修改ME_MicroDSTValue", Desc = "修改ME_MicroDSTValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateMEMicroDSTValue", Get = "", Post = "MEMicroDSTValue", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateMEMicroDSTValue", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateMEMicroDSTValue(MEMicroDSTValue entity);

        [ServiceContractDescription(Name = "修改ME_MicroDSTValue指定的属性", Desc = "修改ME_MicroDSTValue指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateMEMicroDSTValueByField", Get = "", Post = "MEMicroDSTValue", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateMEMicroDSTValueByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateMEMicroDSTValueByField(MEMicroDSTValue entity, string fields);

        [ServiceContractDescription(Name = "删除ME_MicroDSTValue", Desc = "删除ME_MicroDSTValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelMEMicroDSTValue?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelMEMicroDSTValue?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelMEMicroDSTValue(long id);

        [ServiceContractDescription(Name = "查询ME_MicroDSTValue", Desc = "查询ME_MicroDSTValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMEMicroDSTValue", Get = "", Post = "MEMicroDSTValue", Return = "BaseResultList<MEMicroDSTValue>", ReturnType = "ListMEMicroDSTValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMEMicroDSTValue", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMEMicroDSTValue(MEMicroDSTValue entity);

        [ServiceContractDescription(Name = "查询ME_MicroDSTValue(HQL)", Desc = "查询ME_MicroDSTValue(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMEMicroDSTValueByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<MEMicroDSTValue>", ReturnType = "ListMEMicroDSTValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMEMicroDSTValueByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMEMicroDSTValueByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询ME_MicroDSTValue", Desc = "通过主键ID查询ME_MicroDSTValue", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMEMicroDSTValueById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<MEMicroDSTValue>", ReturnType = "MEMicroDSTValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMEMicroDSTValueById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMEMicroDSTValueById(long id, string fields, bool isPlanish);
        #endregion

        #region MEMicroInoculant

        [ServiceContractDescription(Name = "新增ME_MicroInoculant", Desc = "新增ME_MicroInoculant", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddMEMicroInoculant", Get = "", Post = "MEMicroInoculant", Return = "BaseResultDataValue", ReturnType = "MEMicroInoculant")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddMEMicroInoculant", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddMEMicroInoculant(MEMicroInoculant entity);

        [ServiceContractDescription(Name = "修改ME_MicroInoculant", Desc = "修改ME_MicroInoculant", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateMEMicroInoculant", Get = "", Post = "MEMicroInoculant", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateMEMicroInoculant", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateMEMicroInoculant(MEMicroInoculant entity);

        [ServiceContractDescription(Name = "修改ME_MicroInoculant指定的属性", Desc = "修改ME_MicroInoculant指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateMEMicroInoculantByField", Get = "", Post = "MEMicroInoculant", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateMEMicroInoculantByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateMEMicroInoculantByField(MEMicroInoculant entity, string fields);

        [ServiceContractDescription(Name = "删除ME_MicroInoculant", Desc = "删除ME_MicroInoculant", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelMEMicroInoculant?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelMEMicroInoculant?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelMEMicroInoculant(long id);

        [ServiceContractDescription(Name = "查询ME_MicroInoculant", Desc = "查询ME_MicroInoculant", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMEMicroInoculant", Get = "", Post = "MEMicroInoculant", Return = "BaseResultList<MEMicroInoculant>", ReturnType = "ListMEMicroInoculant")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMEMicroInoculant", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMEMicroInoculant(MEMicroInoculant entity);

        [ServiceContractDescription(Name = "查询ME_MicroInoculant(HQL)", Desc = "查询ME_MicroInoculant(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMEMicroInoculantByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<MEMicroInoculant>", ReturnType = "ListMEMicroInoculant")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMEMicroInoculantByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMEMicroInoculantByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询ME_MicroInoculant", Desc = "通过主键ID查询ME_MicroInoculant", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchMEMicroInoculantById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<MEMicroInoculant>", ReturnType = "MEMicroInoculant")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchMEMicroInoculantById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchMEMicroInoculantById(long id, string fields, bool isPlanish);
        #endregion

        #region NRequestItem

        [ServiceContractDescription(Name = "新增NRequestItem", Desc = "新增NRequestItem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_AddNRequestItem", Get = "", Post = "NRequestItem", Return = "BaseResultDataValue", ReturnType = "NRequestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddNRequestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddNRequestItem(NRequestItem entity);

        [ServiceContractDescription(Name = "修改NRequestItem", Desc = "修改NRequestItem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateNRequestItem", Get = "", Post = "NRequestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateNRequestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateNRequestItem(NRequestItem entity);

        [ServiceContractDescription(Name = "修改NRequestItem指定的属性", Desc = "修改NRequestItem指定的属性", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_UpdateNRequestItemByField", Get = "", Post = "NRequestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateNRequestItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateNRequestItemByField(NRequestItem entity, string fields);

        [ServiceContractDescription(Name = "删除NRequestItem", Desc = "删除NRequestItem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_DelNRequestItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelNRequestItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelNRequestItem(string id);

        [ServiceContractDescription(Name = "查询NRequestItem", Desc = "查询NRequestItem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchNRequestItem", Get = "", Post = "NRequestItem", Return = "BaseResultList<NRequestItem>", ReturnType = "ListNRequestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchNRequestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchNRequestItem(NRequestItem entity);

        [ServiceContractDescription(Name = "查询NRequestItem(HQL)", Desc = "查询NRequestItem(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchNRequestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<NRequestItem>", ReturnType = "ListNRequestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchNRequestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchNRequestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询NRequestItem", Desc = "通过主键ID查询NRequestItem", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchNRequestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<NRequestItem>", ReturnType = "NRequestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchNRequestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchNRequestItemById(string id, string fields, bool isPlanish);
        #endregion

        #region PGroup

        [ServiceContractDescription(Name = "查询PGroup", Desc = "查询PGroup", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchPGroup", Get = "", Post = "PGroup", Return = "BaseResultList<PGroup>", ReturnType = "ListPGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchPGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchPGroup(PGroup entity);

        [ServiceContractDescription(Name = "查询PGroup(HQL)", Desc = "查询PGroup(HQL)", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchPGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PGroup>", ReturnType = "ListPGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchPGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchPGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询PGroup", Desc = "通过主键ID查询PGroup", Url = "ServerWCF/WebAssistLisService.svc/WA_UDTO_SearchPGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PGroup>", ReturnType = "PGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchPGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchPGroupById(int id, string fields, bool isPlanish);
        #endregion

    }
}
