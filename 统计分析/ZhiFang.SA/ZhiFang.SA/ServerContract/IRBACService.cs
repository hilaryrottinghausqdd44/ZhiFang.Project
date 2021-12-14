using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.SA.ServerContract
{
    [ServiceContract]
    public interface IRBACService
    {

        #region 单表操作

        #region RBACUser

        [ServiceContractDescription(Name = "新增帐户", Desc = "新增帐户", Url = "RBACService.svc/RBAC_UDTO_AddRBACUser", Get = "", Post = "RBACUser", Return = "BaseResultDataValue", ReturnType = "RBACUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddRBACUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddRBACUser(RBACUser entity);

        [ServiceContractDescription(Name = "修改帐户", Desc = "修改帐户", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACUser", Get = "", Post = "RBACUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACUser(RBACUser entity);

        [ServiceContractDescription(Name = "修改帐户指定的属性", Desc = "修改帐户指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACUserByField", Get = "", Post = "RBACUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACUserByField(RBACUser entity, string fields);

        [ServiceContractDescription(Name = "删除帐户", Desc = "删除帐户", Url = "RBACService.svc/RBAC_UDTO_DelRBACUser?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelRBACUser?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelRBACUser(long id);

        [ServiceContractDescription(Name = "查询帐户", Desc = "查询帐户", Url = "RBACService.svc/RBAC_UDTO_SearchRBACUser", Get = "", Post = "RBACUser", Return = "BaseResultList<RBACUser>", ReturnType = "ListRBACUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACUser(RBACUser entity);

        [ServiceContractDescription(Name = "查询帐户(HQL)", Desc = "查询帐户(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchRBACUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACUser>", ReturnType = "ListRBACUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询帐户", Desc = "通过主键ID查询帐户", Url = "RBACService.svc/RBAC_UDTO_SearchRBACUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACUser>", ReturnType = "RBACUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACUserById(long id, string fields, bool isPlanish);
        #endregion


        #region RBACRoleRight

        [ServiceContractDescription(Name = "新增角色权限", Desc = "新增角色权限", Url = "RBACService.svc/RBAC_UDTO_AddRBACRoleRight", Get = "", Post = "RBACRoleRight", Return = "BaseResultDataValue", ReturnType = "RBACRoleRight")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddRBACRoleRight", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddRBACRoleRight(RBACRoleRight entity);

        [ServiceContractDescription(Name = "修改角色权限", Desc = "修改角色权限", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACRoleRight", Get = "", Post = "RBACRoleRight", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACRoleRight", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACRoleRight(RBACRoleRight entity);

        [ServiceContractDescription(Name = "修改角色权限指定的属性", Desc = "修改角色权限指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACRoleRightByField", Get = "", Post = "RBACRoleRight", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACRoleRightByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACRoleRightByField(RBACRoleRight entity, string fields);

        [ServiceContractDescription(Name = "删除角色权限", Desc = "删除角色权限", Url = "RBACService.svc/RBAC_UDTO_DelRBACRoleRight?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelRBACRoleRight?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelRBACRoleRight(long id);

        [ServiceContractDescription(Name = "查询角色权限", Desc = "查询角色权限", Url = "RBACService.svc/RBAC_UDTO_SearchRBACRoleRight", Get = "", Post = "RBACRoleRight", Return = "BaseResultList<RBACRoleRight>", ReturnType = "ListRBACRoleRight")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACRoleRight", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACRoleRight(RBACRoleRight entity);

        [ServiceContractDescription(Name = "查询角色权限(HQL)", Desc = "查询角色权限(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchRBACRoleRightByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACRoleRight>", ReturnType = "ListRBACRoleRight")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACRoleRightByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACRoleRightByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询角色权限", Desc = "通过主键ID查询角色权限", Url = "RBACService.svc/RBAC_UDTO_SearchRBACRoleRightById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACRoleRight>", ReturnType = "RBACRoleRight")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACRoleRightById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACRoleRightById(long id, string fields, bool isPlanish);
        #endregion


        #region RBACRoleModule

        [ServiceContractDescription(Name = "新增角色模块访问权限", Desc = "新增角色模块访问权限", Url = "RBACService.svc/RBAC_UDTO_AddRBACRoleModule", Get = "", Post = "RBACRoleModule", Return = "BaseResultDataValue", ReturnType = "RBACRoleModule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddRBACRoleModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddRBACRoleModule(RBACRoleModule entity);

        [ServiceContractDescription(Name = "修改角色模块访问权限", Desc = "修改角色模块访问权限", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACRoleModule", Get = "", Post = "RBACRoleModule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACRoleModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACRoleModule(RBACRoleModule entity);

        [ServiceContractDescription(Name = "修改角色模块访问权限指定的属性", Desc = "修改角色模块访问权限指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACRoleModuleByField", Get = "", Post = "RBACRoleModule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACRoleModuleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACRoleModuleByField(RBACRoleModule entity, string fields);

        [ServiceContractDescription(Name = "删除角色模块访问权限", Desc = "删除角色模块访问权限", Url = "RBACService.svc/RBAC_UDTO_DelRBACRoleModule?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelRBACRoleModule?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelRBACRoleModule(long id);

        [ServiceContractDescription(Name = "查询角色模块访问权限", Desc = "查询角色模块访问权限", Url = "RBACService.svc/RBAC_UDTO_SearchRBACRoleModule", Get = "", Post = "RBACRoleModule", Return = "BaseResultList<RBACRoleModule>", ReturnType = "ListRBACRoleModule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACRoleModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACRoleModule(RBACRoleModule entity);

        [ServiceContractDescription(Name = "查询角色模块访问权限(HQL)", Desc = "查询角色模块访问权限(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACRoleModule>", ReturnType = "ListRBACRoleModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACRoleModuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACRoleModuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询角色模块访问权限", Desc = "通过主键ID查询角色模块访问权限", Url = "RBACService.svc/RBAC_UDTO_SearchRBACRoleModuleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACRoleModule>", ReturnType = "RBACRoleModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACRoleModuleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACRoleModuleById(long id, string fields, bool isPlanish);
        #endregion


        #region RBACRole

        [ServiceContractDescription(Name = "新增角色", Desc = "新增角色", Url = "RBACService.svc/RBAC_UDTO_AddRBACRole", Get = "", Post = "RBACRole", Return = "BaseResultDataValue", ReturnType = "RBACRole")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddRBACRole", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddRBACRole(RBACRole entity);

        [ServiceContractDescription(Name = "修改角色", Desc = "修改角色", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACRole", Get = "", Post = "RBACRole", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACRole", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACRole(RBACRole entity);

        [ServiceContractDescription(Name = "修改角色指定的属性", Desc = "修改角色指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACRoleByField", Get = "", Post = "RBACRole", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACRoleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACRoleByField(RBACRole entity, string fields);

        [ServiceContractDescription(Name = "删除角色", Desc = "删除角色", Url = "RBACService.svc/RBAC_UDTO_DelRBACRole?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelRBACRole?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelRBACRole(long id);

        [ServiceContractDescription(Name = "查询角色", Desc = "查询角色", Url = "RBACService.svc/RBAC_UDTO_SearchRBACRole", Get = "", Post = "RBACRole", Return = "BaseResultList<RBACRole>", ReturnType = "ListRBACRole")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACRole", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACRole(RBACRole entity);

        [ServiceContractDescription(Name = "查询角色(HQL)", Desc = "查询角色(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchRBACRoleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACRole>", ReturnType = "ListRBACRole")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACRoleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACRoleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询角色", Desc = "通过主键ID查询角色", Url = "RBACService.svc/RBAC_UDTO_SearchRBACRoleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACRole>", ReturnType = "RBACRole")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACRoleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACRoleById(long id, string fields, bool isPlanish);
        #endregion


        #region RBACModule

        [ServiceContractDescription(Name = "新增模块", Desc = "新增模块", Url = "RBACService.svc/RBAC_UDTO_AddRBACModule", Get = "", Post = "RBACModule", Return = "BaseResultDataValue", ReturnType = "RBACModule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddRBACModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddRBACModule(RBACModule entity);

        [ServiceContractDescription(Name = "修改模块", Desc = "修改模块", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACModule", Get = "", Post = "RBACModule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACModule(RBACModule entity);

        [ServiceContractDescription(Name = "修改模块指定的属性", Desc = "修改模块指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACModuleByField", Get = "", Post = "RBACModule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACModuleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACModuleByField(RBACModule entity, string fields);

        [ServiceContractDescription(Name = "删除模块", Desc = "删除模块", Url = "RBACService.svc/RBAC_UDTO_DelRBACModule?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelRBACModule?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelRBACModule(long id);

        [ServiceContractDescription(Name = "查询模块", Desc = "查询模块", Url = "RBACService.svc/RBAC_UDTO_SearchRBACModule", Get = "", Post = "RBACModule", Return = "BaseResultList<RBACModule>", ReturnType = "ListRBACModule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACModule(RBACModule entity);

        [ServiceContractDescription(Name = "查询模块(HQL)", Desc = "查询模块(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchRBACModuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACModule>", ReturnType = "ListRBACModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACModuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACModuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询模块", Desc = "通过主键ID查询模块", Url = "RBACService.svc/RBAC_UDTO_SearchRBACModuleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACModule>", ReturnType = "RBACModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACModuleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACModuleById(long id, string fields, bool isPlanish);
        #endregion


        #region RBACModuleOper

        [ServiceContractDescription(Name = "新增模块操作", Desc = "新增模块操作", Url = "RBACService.svc/RBAC_UDTO_AddRBACModuleOper", Get = "", Post = "RBACModuleOper", Return = "BaseResultDataValue", ReturnType = "RBACModuleOper")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddRBACModuleOper", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddRBACModuleOper(RBACModuleOper entity);

        [ServiceContractDescription(Name = "修改模块操作", Desc = "修改模块操作", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACModuleOper", Get = "", Post = "RBACModuleOper", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACModuleOper", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACModuleOper(RBACModuleOper entity);

        [ServiceContractDescription(Name = "修改模块操作指定的属性", Desc = "修改模块操作指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACModuleOperByField", Get = "", Post = "RBACModuleOper", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACModuleOperByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACModuleOperByField(RBACModuleOper entity, string fields);

        [ServiceContractDescription(Name = "删除模块操作", Desc = "删除模块操作", Url = "RBACService.svc/RBAC_UDTO_DelRBACModuleOper?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelRBACModuleOper?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelRBACModuleOper(long id);

        [ServiceContractDescription(Name = "查询模块操作", Desc = "查询模块操作", Url = "RBACService.svc/RBAC_UDTO_SearchRBACModuleOper", Get = "", Post = "RBACModuleOper", Return = "BaseResultList<RBACModuleOper>", ReturnType = "ListRBACModuleOper")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACModuleOper", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACModuleOper(RBACModuleOper entity);

        [ServiceContractDescription(Name = "查询模块操作(HQL)", Desc = "查询模块操作(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchRBACModuleOperByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACModuleOper>", ReturnType = "ListRBACModuleOper")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACModuleOperByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACModuleOperByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询模块操作", Desc = "通过主键ID查询模块操作", Url = "RBACService.svc/RBAC_UDTO_SearchRBACModuleOperById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACModuleOper>", ReturnType = "RBACModuleOper")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACModuleOperById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACModuleOperById(long id, string fields, bool isPlanish);
        #endregion


        #region RBACEmpRoles

        [ServiceContractDescription(Name = "新增员工角色", Desc = "新增员工角色", Url = "RBACService.svc/RBAC_UDTO_AddRBACEmpRoles", Get = "", Post = "RBACEmpRoles", Return = "BaseResultDataValue", ReturnType = "RBACEmpRoles")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddRBACEmpRoles", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddRBACEmpRoles(RBACEmpRoles entity);

        [ServiceContractDescription(Name = "修改员工角色", Desc = "修改员工角色", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACEmpRoles", Get = "", Post = "RBACEmpRoles", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACEmpRoles", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACEmpRoles(RBACEmpRoles entity);

        [ServiceContractDescription(Name = "修改员工角色指定的属性", Desc = "修改员工角色指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACEmpRolesByField", Get = "", Post = "RBACEmpRoles", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACEmpRolesByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACEmpRolesByField(RBACEmpRoles entity, string fields);

        [ServiceContractDescription(Name = "删除员工角色", Desc = "删除员工角色", Url = "RBACService.svc/RBAC_UDTO_DelRBACEmpRoles?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelRBACEmpRoles?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelRBACEmpRoles(long id);

        [ServiceContractDescription(Name = "查询员工角色", Desc = "查询员工角色", Url = "RBACService.svc/RBAC_UDTO_SearchRBACEmpRoles", Get = "", Post = "RBACEmpRoles", Return = "BaseResultList<RBACEmpRoles>", ReturnType = "ListRBACEmpRoles")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACEmpRoles", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACEmpRoles(RBACEmpRoles entity);

        [ServiceContractDescription(Name = "查询员工角色(HQL)", Desc = "查询员工角色(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACEmpRoles>", ReturnType = "ListRBACEmpRoles")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACEmpRolesByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACEmpRolesByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询员工角色", Desc = "通过主键ID查询员工角色", Url = "RBACService.svc/RBAC_UDTO_SearchRBACEmpRolesById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACEmpRoles>", ReturnType = "RBACEmpRoles")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACEmpRolesById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACEmpRolesById(long id, string fields, bool isPlanish);
        #endregion


        #region RBACEmpOptions

        [ServiceContractDescription(Name = "新增员工设置", Desc = "新增员工设置", Url = "RBACService.svc/RBAC_UDTO_AddRBACEmpOptions", Get = "", Post = "RBACEmpOptions", Return = "BaseResultDataValue", ReturnType = "RBACEmpOptions")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddRBACEmpOptions", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddRBACEmpOptions(RBACEmpOptions entity);

        [ServiceContractDescription(Name = "修改员工设置", Desc = "修改员工设置", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACEmpOptions", Get = "", Post = "RBACEmpOptions", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACEmpOptions", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACEmpOptions(RBACEmpOptions entity);

        [ServiceContractDescription(Name = "修改员工设置指定的属性", Desc = "修改员工设置指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACEmpOptionsByField", Get = "", Post = "RBACEmpOptions", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACEmpOptionsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACEmpOptionsByField(RBACEmpOptions entity, string fields);

        [ServiceContractDescription(Name = "删除员工设置", Desc = "删除员工设置", Url = "RBACService.svc/RBAC_UDTO_DelRBACEmpOptions?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelRBACEmpOptions?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelRBACEmpOptions(long id);

        [ServiceContractDescription(Name = "查询员工设置", Desc = "查询员工设置", Url = "RBACService.svc/RBAC_UDTO_SearchRBACEmpOptions", Get = "", Post = "RBACEmpOptions", Return = "BaseResultList<RBACEmpOptions>", ReturnType = "ListRBACEmpOptions")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACEmpOptions", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACEmpOptions(RBACEmpOptions entity);

        [ServiceContractDescription(Name = "查询员工设置(HQL)", Desc = "查询员工设置(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchRBACEmpOptionsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACEmpOptions>", ReturnType = "ListRBACEmpOptions")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACEmpOptionsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACEmpOptionsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询员工设置", Desc = "通过主键ID查询员工设置", Url = "RBACService.svc/RBAC_UDTO_SearchRBACEmpOptionsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACEmpOptions>", ReturnType = "RBACEmpOptions")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACEmpOptionsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACEmpOptionsById(long id, string fields, bool isPlanish);
        #endregion


        #region RBACRowFilter

        [ServiceContractDescription(Name = "新增行过滤", Desc = "新增行过滤", Url = "RBACService.svc/RBAC_UDTO_AddRBACRowFilter", Get = "", Post = "RBACRowFilter", Return = "BaseResultDataValue", ReturnType = "RBACRowFilter")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddRBACRowFilter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddRBACRowFilter(RBACRowFilter entity);

        [ServiceContractDescription(Name = "修改行过滤", Desc = "修改行过滤", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACRowFilter", Get = "", Post = "RBACRowFilter", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACRowFilter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACRowFilter(RBACRowFilter entity);

        [ServiceContractDescription(Name = "修改行过滤指定的属性", Desc = "修改行过滤指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateRBACRowFilterByField", Get = "", Post = "RBACRowFilter", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateRBACRowFilterByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateRBACRowFilterByField(RBACRowFilter entity, string fields);

        [ServiceContractDescription(Name = "删除行过滤", Desc = "删除行过滤", Url = "RBACService.svc/RBAC_UDTO_DelRBACRowFilter?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelRBACRowFilter?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelRBACRowFilter(long id);

        [ServiceContractDescription(Name = "查询行过滤", Desc = "查询行过滤", Url = "RBACService.svc/RBAC_UDTO_SearchRBACRowFilter", Get = "", Post = "RBACRowFilter", Return = "BaseResultList<RBACRowFilter>", ReturnType = "ListRBACRowFilter")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACRowFilter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACRowFilter(RBACRowFilter entity);

        [ServiceContractDescription(Name = "查询行过滤(HQL)", Desc = "查询行过滤(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchRBACRowFilterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACRowFilter>", ReturnType = "ListRBACRowFilter")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACRowFilterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACRowFilterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询行过滤", Desc = "通过主键ID查询行过滤", Url = "RBACService.svc/RBAC_UDTO_SearchRBACRowFilterById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACRowFilter>", ReturnType = "RBACRowFilter")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchRBACRowFilterById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACRowFilterById(long id, string fields, bool isPlanish);
        #endregion


        #region HRPosition

        [ServiceContractDescription(Name = "新增职位", Desc = "新增职位", Url = "RBACService.svc/RBAC_UDTO_AddHRPosition", Get = "", Post = "HRPosition", Return = "BaseResultDataValue", ReturnType = "HRPosition")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddHRPosition", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddHRPosition(HRPosition entity);

        [ServiceContractDescription(Name = "修改职位", Desc = "修改职位", Url = "RBACService.svc/RBAC_UDTO_UpdateHRPosition", Get = "", Post = "HRPosition", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateHRPosition", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateHRPosition(HRPosition entity);

        [ServiceContractDescription(Name = "修改职位指定的属性", Desc = "修改职位指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateHRPositionByField", Get = "", Post = "HRPosition", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateHRPositionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateHRPositionByField(HRPosition entity, string fields);

        [ServiceContractDescription(Name = "删除职位", Desc = "删除职位", Url = "RBACService.svc/RBAC_UDTO_DelHRPosition?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelHRPosition?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelHRPosition(long id);

        [ServiceContractDescription(Name = "查询职位", Desc = "查询职位", Url = "RBACService.svc/RBAC_UDTO_SearchHRPosition", Get = "", Post = "HRPosition", Return = "BaseResultList<HRPosition>", ReturnType = "ListHRPosition")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRPosition", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRPosition(HRPosition entity);

        [ServiceContractDescription(Name = "查询职位(HQL)", Desc = "查询职位(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchHRPositionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRPosition>", ReturnType = "ListHRPosition")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRPositionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRPositionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询职位", Desc = "通过主键ID查询职位", Url = "RBACService.svc/RBAC_UDTO_SearchHRPositionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRPosition>", ReturnType = "HRPosition")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRPositionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRPositionById(long id, string fields, bool isPlanish);
        #endregion


        #region HREmployee

        [ServiceContractDescription(Name = "新增员工", Desc = "新增员工", Url = "RBACService.svc/RBAC_UDTO_AddHREmployee", Get = "", Post = "HREmployee", Return = "BaseResultDataValue", ReturnType = "HREmployee")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddHREmployee", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddHREmployee(HREmployee entity);

        [ServiceContractDescription(Name = "修改员工", Desc = "修改员工", Url = "RBACService.svc/RBAC_UDTO_UpdateHREmployee", Get = "", Post = "HREmployee", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateHREmployee", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateHREmployee(HREmployee entity);

        [ServiceContractDescription(Name = "修改员工指定的属性", Desc = "修改员工指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateHREmployeeByField", Get = "", Post = "HREmployee", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateHREmployeeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateHREmployeeByField(HREmployee entity, string fields);

        [ServiceContractDescription(Name = "删除员工", Desc = "删除员工", Url = "RBACService.svc/RBAC_UDTO_DelHREmployee?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelHREmployee?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelHREmployee(long id);

        [ServiceContractDescription(Name = "查询员工", Desc = "查询员工", Url = "RBACService.svc/RBAC_UDTO_SearchHREmployee", Get = "", Post = "HREmployee", Return = "BaseResultList<HREmployee>", ReturnType = "ListHREmployee")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHREmployee", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHREmployee(HREmployee entity);

        [ServiceContractDescription(Name = "查询员工(HQL)", Desc = "查询员工(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HREmployee>", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHREmployeeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHREmployeeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询员工", Desc = "通过主键ID查询员工", Url = "RBACService.svc/RBAC_UDTO_SearchHREmployeeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "HREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHREmployeeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHREmployeeById(long id, string fields, bool isPlanish);
        #endregion


        #region HREmpIdentity

        [ServiceContractDescription(Name = "新增员工身份", Desc = "新增员工身份", Url = "RBACService.svc/RBAC_UDTO_AddHREmpIdentity", Get = "", Post = "HREmpIdentity", Return = "BaseResultDataValue", ReturnType = "HREmpIdentity")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddHREmpIdentity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddHREmpIdentity(HREmpIdentity entity);

        [ServiceContractDescription(Name = "修改员工身份", Desc = "修改员工身份", Url = "RBACService.svc/RBAC_UDTO_UpdateHREmpIdentity", Get = "", Post = "HREmpIdentity", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateHREmpIdentity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateHREmpIdentity(HREmpIdentity entity);

        [ServiceContractDescription(Name = "修改员工身份指定的属性", Desc = "修改员工身份指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateHREmpIdentityByField", Get = "", Post = "HREmpIdentity", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateHREmpIdentityByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateHREmpIdentityByField(HREmpIdentity entity, string fields);

        [ServiceContractDescription(Name = "删除员工身份", Desc = "删除员工身份", Url = "RBACService.svc/RBAC_UDTO_DelHREmpIdentity?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelHREmpIdentity?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelHREmpIdentity(long id);

        [ServiceContractDescription(Name = "查询员工身份", Desc = "查询员工身份", Url = "RBACService.svc/RBAC_UDTO_SearchHREmpIdentity", Get = "", Post = "HREmpIdentity", Return = "BaseResultList<HREmpIdentity>", ReturnType = "ListHREmpIdentity")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHREmpIdentity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHREmpIdentity(HREmpIdentity entity);

        [ServiceContractDescription(Name = "查询员工身份(HQL)", Desc = "查询员工身份(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HREmpIdentity>", ReturnType = "ListHREmpIdentity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHREmpIdentityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHREmpIdentityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询员工身份", Desc = "通过主键ID查询员工身份", Url = "RBACService.svc/RBAC_UDTO_SearchHREmpIdentityById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HREmpIdentity>", ReturnType = "HREmpIdentity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHREmpIdentityById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHREmpIdentityById(long id, string fields, bool isPlanish);
        #endregion


        #region HRDept

        [ServiceContractDescription(Name = "新增部门", Desc = "新增部门", Url = "RBACService.svc/RBAC_UDTO_AddHRDept", Get = "", Post = "HRDept", Return = "BaseResultDataValue", ReturnType = "HRDept")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddHRDept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddHRDept(HRDept entity);

        [ServiceContractDescription(Name = "修改部门", Desc = "修改部门", Url = "RBACService.svc/RBAC_UDTO_UpdateHRDept", Get = "", Post = "HRDept", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateHRDept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateHRDept(HRDept entity);

        [ServiceContractDescription(Name = "修改部门指定的属性", Desc = "修改部门指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateHRDeptByField", Get = "", Post = "HRDept", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateHRDeptByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateHRDeptByField(HRDept entity, string fields);

        [ServiceContractDescription(Name = "删除部门", Desc = "删除部门", Url = "RBACService.svc/RBAC_UDTO_DelHRDept?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelHRDept?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelHRDept(long id);

        [ServiceContractDescription(Name = "查询部门", Desc = "查询部门", Url = "RBACService.svc/RBAC_UDTO_SearchHRDept", Get = "", Post = "HRDept", Return = "BaseResultList<HRDept>", ReturnType = "ListHRDept")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDept(HRDept entity);

        [ServiceContractDescription(Name = "查询部门(HQL)", Desc = "查询部门(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchHRDeptByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRDept>", ReturnType = "ListHRDept")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询部门", Desc = "通过主键ID查询部门", Url = "RBACService.svc/RBAC_UDTO_SearchHRDeptById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRDept>", ReturnType = "HRDept")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptById(long id, string fields, bool isPlanish);
        #endregion


        #region HRDeptIdentity

        [ServiceContractDescription(Name = "新增部分身份", Desc = "新增部分身份", Url = "RBACService.svc/RBAC_UDTO_AddHRDeptIdentity", Get = "", Post = "HRDeptIdentity", Return = "BaseResultDataValue", ReturnType = "HRDeptIdentity")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddHRDeptIdentity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddHRDeptIdentity(HRDeptIdentity entity);

        [ServiceContractDescription(Name = "修改部分身份", Desc = "修改部分身份", Url = "RBACService.svc/RBAC_UDTO_UpdateHRDeptIdentity", Get = "", Post = "HRDeptIdentity", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateHRDeptIdentity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateHRDeptIdentity(HRDeptIdentity entity);

        [ServiceContractDescription(Name = "修改部分身份指定的属性", Desc = "修改部分身份指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateHRDeptIdentityByField", Get = "", Post = "HRDeptIdentity", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateHRDeptIdentityByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateHRDeptIdentityByField(HRDeptIdentity entity, string fields);

        [ServiceContractDescription(Name = "删除部分身份", Desc = "删除部分身份", Url = "RBACService.svc/RBAC_UDTO_DelHRDeptIdentity?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelHRDeptIdentity?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelHRDeptIdentity(long id);

        [ServiceContractDescription(Name = "查询部分身份", Desc = "查询部分身份", Url = "RBACService.svc/RBAC_UDTO_SearchHRDeptIdentity", Get = "", Post = "HRDeptIdentity", Return = "BaseResultList<HRDeptIdentity>", ReturnType = "ListHRDeptIdentity")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptIdentity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptIdentity(HRDeptIdentity entity);

        [ServiceContractDescription(Name = "查询部分身份(HQL)", Desc = "查询部分身份(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRDeptIdentity>", ReturnType = "ListHRDeptIdentity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptIdentityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptIdentityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询部分身份", Desc = "通过主键ID查询部分身份", Url = "RBACService.svc/RBAC_UDTO_SearchHRDeptIdentityById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRDeptIdentity>", ReturnType = "HRDeptIdentity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptIdentityById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptIdentityById(long id, string fields, bool isPlanish);
        #endregion


        #region HRDeptEmp

        [ServiceContractDescription(Name = "新增部门员工关系", Desc = "新增部门员工关系", Url = "RBACService.svc/RBAC_UDTO_AddHRDeptEmp", Get = "", Post = "HRDeptEmp", Return = "BaseResultDataValue", ReturnType = "HRDeptEmp")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddHRDeptEmp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddHRDeptEmp(HRDeptEmp entity);

        [ServiceContractDescription(Name = "修改部门员工关系", Desc = "修改部门员工关系", Url = "RBACService.svc/RBAC_UDTO_UpdateHRDeptEmp", Get = "", Post = "HRDeptEmp", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateHRDeptEmp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateHRDeptEmp(HRDeptEmp entity);

        [ServiceContractDescription(Name = "修改部门员工关系指定的属性", Desc = "修改部门员工关系指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateHRDeptEmpByField", Get = "", Post = "HRDeptEmp", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateHRDeptEmpByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateHRDeptEmpByField(HRDeptEmp entity, string fields);

        [ServiceContractDescription(Name = "删除部门员工关系", Desc = "删除部门员工关系", Url = "RBACService.svc/RBAC_UDTO_DelHRDeptEmp?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelHRDeptEmp?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelHRDeptEmp(long id);

        [ServiceContractDescription(Name = "查询部门员工关系", Desc = "查询部门员工关系", Url = "RBACService.svc/RBAC_UDTO_SearchHRDeptEmp", Get = "", Post = "HRDeptEmp", Return = "BaseResultList<HRDeptEmp>", ReturnType = "ListHRDeptEmp")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptEmp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptEmp(HRDeptEmp entity);

        [ServiceContractDescription(Name = "查询部门员工关系(HQL)", Desc = "查询部门员工关系(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchHRDeptEmpByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRDeptEmp>", ReturnType = "ListHRDeptEmp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptEmpByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptEmpByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询部门员工关系", Desc = "通过主键ID查询部门员工关系", Url = "RBACService.svc/RBAC_UDTO_SearchHRDeptEmpById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRDeptEmp>", ReturnType = "HRDeptEmp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptEmpById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptEmpById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询员工(HQL)", Desc = "查询员工(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchHREmployeeByHQLEx?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HREmployee>", ReturnType = "ListHRDeptEmp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHREmployeeByHQLEx?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHREmployeeByHQLEx(int page, int limit, string fields, string where, string sort, bool isPlanish);
        #endregion


        #region SLog

        [ServiceContractDescription(Name = "新增系统日志", Desc = "新增系统日志", Url = "RBACService.svc/RBAC_UDTO_AddSLog", Get = "", Post = "SLog", Return = "BaseResultDataValue", ReturnType = "SLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_AddSLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_AddSLog(SLog entity);

        [ServiceContractDescription(Name = "修改系统日志", Desc = "修改系统日志", Url = "RBACService.svc/RBAC_UDTO_UpdateSLog", Get = "", Post = "SLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateSLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateSLog(SLog entity);

        [ServiceContractDescription(Name = "修改系统日志指定的属性", Desc = "修改系统日志指定的属性", Url = "RBACService.svc/RBAC_UDTO_UpdateSLogByField", Get = "", Post = "SLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_UpdateSLogByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_UpdateSLogByField(SLog entity, string fields);

        [ServiceContractDescription(Name = "删除系统日志", Desc = "删除系统日志", Url = "RBACService.svc/RBAC_UDTO_DelSLog?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_DelSLog?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_UDTO_DelSLog(long id);

        [ServiceContractDescription(Name = "查询系统日志", Desc = "查询系统日志", Url = "RBACService.svc/RBAC_UDTO_SearchSLog", Get = "", Post = "SLog", Return = "BaseResultList<SLog>", ReturnType = "ListSLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchSLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchSLog(SLog entity);

        [ServiceContractDescription(Name = "查询系统日志(HQL)", Desc = "查询系统日志(HQL)", Url = "RBACService.svc/RBAC_UDTO_SearchSLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SLog>", ReturnType = "ListSLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchSLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchSLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询系统日志", Desc = "通过主键ID查询系统日志", Url = "RBACService.svc/RBAC_UDTO_SearchSLogById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SLog>", ReturnType = "SLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchSLogById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchSLogById(long id, string fields, bool isPlanish);
        #endregion

        #endregion

        #region 查询类操作

        #region 查询员工列表
        [ServiceContractDescription(Name = "根据HQL查询条件查询用户列表", Desc = "根据HQL查询条件查询用户列表", Url = "RBACService.svc/RBAC_UDTO_SearchRBACUserListByHQL?where={where}&start={start}&count={count}", Get = "", Post = "", Return = "EntityList<RBACUser> ", ReturnType = "ListRBACUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_SearchRBACUserListByHQL?where={where}&start={start}&limit={limit}&page={page}&isPlanish={isPlanish}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACUserListByHQL(string where, int start, int limit, int page, bool isPlanish, string fields);

        [ServiceContractDescription(Name = "查询部门直属员工列表(包含子部门)", Desc = "查询部门直属员工列表(包含子部门)", Url = "RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Get = "where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_GetHREmployeeByHRDeptID?where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_GetHREmployeeByHRDeptID(string where, int limit, int page, bool isPlanish, string fields, string sort);

        [ServiceContractDescription(Name = "查询部门直属的未分配角色的员工列表(包含子部门)", Desc = "查询部门直属的未分配角色的员工列表(包含子部门)", Url = "RBACService.svc/RBAC_UDTO_GetHREmployeeNoRoleByHRDeptID?where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Get = "where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_GetHREmployeeNoRoleByHRDeptID?where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_GetHREmployeeNoRoleByHRDeptID(string where, int limit, int page, bool isPlanish, string fields, string sort);

        [ServiceContractDescription(Name = "查询部门直属的已分配角色的员工列表(包含子部门)", Desc = "查询部门直属的已分配角色的员工列表(包含子部门)", Url = "RBACService.svc/RBAC_UDTO_GetHREmployeeRoleByHRDeptID?where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Get = "where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_GetHREmployeeRoleByHRDeptID?where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_GetHREmployeeRoleByHRDeptID(string where, int limit, int page, bool isPlanish, string fields, string sort);

        [ServiceContractDescription(Name = "查询部门直属的已分配特定角色的员工列表(包含子部门)", Desc = "查询部门直属的已分配特定角色的员工列表(包含子部门)", Url = "RBACService.svc/RBAC_UDTO_GetHREmployeeByHRDeptIDAndRBACRoleID?longHRDeptID={longHRDeptID}&longRBACRoleID={longRBACRoleID}&fields={fields}&isPlanish={isPlanish}", Get = "longHRDeptID={longHRDeptID}&longRBACRoleID={longRBACRoleID}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_GetHREmployeeByHRDeptIDAndRBACRoleID?longHRDeptID={longHRDeptID}&longRBACRoleID={longRBACRoleID}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_GetHREmployeeByHRDeptIDAndRBACRoleID(long longHRDeptID, long longRBACRoleID, bool isPlanish, string fields);

        [ServiceContractDescription(Name = "查询部门直属员工列表(包含子部门),并过滤已分配特定角色的员工", Desc = "查询部门直属员工列表(包含子部门),并过滤已分配特定角色的员工", Url = "RBACService.svc/RBAC_UDTO_GetHREmployeeNoRBACRoleIDByHRDeptID?longHRDeptID={longHRDeptID}&longRBACRoleID={longRBACRoleID}&fields={fields}&isPlanish={isPlanish}", Get = "longHRDeptID={longHRDeptID}&longRBACRoleID={longRBACRoleID}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_GetHREmployeeNoRBACRoleIDByHRDeptID?longHRDeptID={longHRDeptID}&longRBACRoleID={longRBACRoleID}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_GetHREmployeeNoRBACRoleIDByHRDeptID(long longHRDeptID, long longRBACRoleID, bool isPlanish, string fields);

        [ServiceContractDescription(Name = "根据Session中人员ID查询该员工的信息", Desc = "根据Session中人员ID查询该员工的信息", Url = "RBACService.svc/RBAC_UDTO_GetHREmployeeBySessionHREmpID?fields={fields}&isPlanish={isPlanish}", Get = "fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "HREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_GetHREmployeeBySessionHREmpID?fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_GetHREmployeeBySessionHREmpID(string fields, bool isPlanish);
        #endregion

        #region 查询部门列表

        [ServiceContractDescription(Name = "根据部门ID获取部门列表树", Desc = "根据部门ID获取部门列表树", Url = "RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree?id={id}&fields={fields}", Get = "id={id}&fields={fields}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeHRDept")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_GetHRDeptFrameListTree?id={id}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_GetHRDeptFrameListTree(string id, string fields);

        [ServiceContractDescription(Name = "根据部门ID获取部门单列树", Desc = "根据部门ID获取部门单列树", Url = "RBACService.svc/RBAC_RJ_GetHRDeptFrameTree?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "Tree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_GetHRDeptFrameTree?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_GetHRDeptFrameTree(string id);

        [ServiceContractDescription(Name = "根据部门ID获取部门员工单列树", Desc = "根据部门ID获取部门员工单列树", Url = "RBACService.svc/RBAC_RJ_GetHRDeptEmployeeFrameTree?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "Tree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_GetHRDeptEmployeeFrameTree?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_GetHRDeptEmployeeFrameTree(string id);

        #endregion

        #region 查询角色列表
        [ServiceContractDescription(Name = "根据用户ID查询该用户拥有的角色列表", Desc = "根据用户ID查询该用户拥有的角色列表", Url = "RBACService.svc/RBAC_UDTO_SearchRoleByHREmpID?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACRole>", ReturnType = "ListRBACRole")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_SearchRoleByHREmpID?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRoleByHREmpID(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "根据模块ID查询拥有该模块权限的角色列表", Desc = "根据模块ID查询拥有该模块权限的角色列表", Url = "RBACService.svc/RBAC_UDTO_SearchRoleByModuleID?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACRole>", ReturnType = "ListRBACRole")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_SearchRoleByModuleID?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRoleByModuleID(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "根据模块ID查询拥有该模块权限的角色操作列表", Desc = "根据模块ID查询拥有该模块权限的角色操作列表", Url = "RBACService.svc/RBAC_UDTO_SearchRoleModuleOperByModuleID?id={id}", Get = "id={id}", Post = "", Return = "BaseResultList<RBACRole>", ReturnType = "ListRBACRole")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_SearchRoleModuleOperByModuleID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRoleModuleOperByModuleID(long id);

        [ServiceContractDescription(Name = "根据角色ID获取角色列表树", Desc = "根据角色ID获取角色列表树", Url = "RBACService.svc/RBAC_RJ_GetRBACRoleFrameListTree?id={id}&fields={fields}", Get = "id={id}&fields={fields}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeRBACRole")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_GetRBACRoleFrameListTree?id={id}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_GetRBACRoleFrameListTree(string id, string fields);

        [ServiceContractDescription(Name = "根据角色ID获取角色单列树", Desc = "根据角色ID获取角色单列树", Url = "RBACService.svc/RBAC_RJ_GetRBACRoleFrameTree?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "Tree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_GetRBACRoleFrameTree?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_GetRBACRoleFrameTree(string id);

        #endregion

        #region 查询模块列表
        [ServiceContractDescription(Name = "根据人员ID查询该人员所属角色所具有权限的模块列表", Desc = "根据人员ID查询该人员所具有权限的模块列表", Url = "RBACService.svc/RBAC_UDTO_SearchModuleByHREmpIDRole?id={id}&page={page}&limit={limit}&fields={fields}&isPlanis={isPlanis}", Get = "id={id}&page={page}&limit={limit}&fields={fields}&isPlanis={isPlanis}", Post = "", Return = "BaseResultList<RBACModule>", ReturnType = "ListRBACModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_SearchModuleByHREmpIDRole?id={id}&page={page}&limit={limit}&fields={fields}&isPlanis={isPlanis}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchModuleByHREmpIDRole(long id, int page, int limit, string fields, bool isPlanis);

        [ServiceContractDescription(Name = "获取常用模块列表加权限判断", Desc = "获取常用模块列表加权限判断", Url = "RBACService.svc/RBAC_RJ_CheckEmpModuleRight?id={id}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "ListRBACEmpOptions")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_RJ_CheckEmpModuleRight?id={id}&fields={fields}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_CheckEmpModuleRight(long id, bool isPlanish, string fields, string sort);

        [ServiceContractDescription(Name = "根据Session人员ID、Cookie中模块ID查询是否具有操作此模块的权限", Desc = "根据Session人员ID、Cookie中模块ID查询是否具有操作此模块的权限", Url = "RBAC_UDTO_SearchModuleBySessionHREmpIDAndCookieModuleID?CurModuleID={CurModuleID}", Get = "CurModuleID={CurModuleID}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_SearchModuleBySessionHREmpIDAndCookieModuleID?CurModuleID={CurModuleID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchModuleBySessionHREmpIDAndCookieModuleID(long CurModuleID);

        [ServiceContractDescription(Name = "根据员工ID、模块ID查询是否具有操作此模块的权限", Desc = "根据员工ID、模块ID查询是否具有操作此模块的权限", Url = "RBAC_UDTO_SearchModuleRoleByHREmpIDAndModuleID?strHREmpID={strHREmpID}&strModuleID={strModuleID}", Get = "strHREmpID={strHREmpID}&strModuleID={strModuleID}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_SearchModuleRoleByHREmpIDAndModuleID?strHREmpID={strHREmpID}&strModuleID={strModuleID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchModuleRoleByHREmpIDAndModuleID(string strHREmpID, string strModuleID);

        [ServiceContractDescription(Name = "根据模块ID获取此模块的应用组件", Desc = "根据模块ID获取此模块的应用组件", Url = "RBACService.svc/RBAC_UDTO_SearchBTDAppComponentsByModuleID?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "RBACModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchBTDAppComponentsByModuleID?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchBTDAppComponentsByModuleID(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过ID查询模块", Desc = "通过ID查询模块", Url = "RBACService.svc/RBAC_UDTO_GetRBACModuleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Return = "BaseResultDataValue", ReturnType = "RBACModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_GetRBACModuleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_GetRBACModuleById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询模块表，根据父模块ID返回模块的Tree", Desc = "查询模块表，根据父模块ID返回模块的Tree", Url = "RBACService.svc/RBAC_UDTO_SearchRBACModuleToTree?id={id}", Get = "?id={id}", Post = "", Return = "BaseResultTree", ReturnType = "TreeRBACModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_SearchRBACModuleToTree?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACModuleToTree(string id);

        [ServiceContractDescription(Name = "查询模块表，根据父模块ID返回模块的ListTree", Desc = "查询模块表，根据父模块ID返回模块的ListTree", Url = "RBACService.svc/RBAC_UDTO_SearchRBACModuleToListTree?id={id}&fields={fields}", Get = "?id={id}&fields={fields}", Post = "", Return = "BaseResultTree", ReturnType = "TreeRBACModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_SearchRBACModuleToListTree?id={id}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchRBACModuleToListTree(string id, string fields);

        [ServiceContractDescription(Name = "根据Session中员工ID查询该人员所具有权限的模块树", Desc = "根据Session中员工ID查询该人员所具有权限的模块树", Url = "RBACService.svc/RBAC_UDTO_SearchModuleTreeBySessionHREmpID", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeRBACModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchModuleTreeBySessionHREmpID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchModuleTreeBySessionHREmpID();

        [ServiceContractDescription(Name = "根据员工ID查询该人员所具有权限的模块树", Desc = "根据员工ID查询该人员所具有权限的模块树", Url = "RBACService.svc/RBAC_UDTO_SearchModuleTreeByHREmpID?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeRBACModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchModuleTreeByHREmpID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchModuleTreeByHREmpID(long id);
        #endregion

        #region 查询操作列表
        [ServiceContractDescription(Name = "根据模块ID查询其包含的模块操作列表", Desc = "根据模块ID查询其包含的模块操作列表", Url = "RBACService.svc/RBAC_UDTO_SearchModuleOperByModuleID?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACModule>", ReturnType = "ListRBACModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_UDTO_SearchModuleOperByModuleID?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchModuleOperByModuleID(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "根据模块操作ID获取此操作的行过滤树", Desc = "根据模块操作ID获取此操作的行过滤树", Url = "RBACService.svc/RBAC_RJ_SearchRBACRowFilterTreeByModuleOperID?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "Tree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_SearchRBACRowFilterTreeByModuleOperID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_SearchRBACRowFilterTreeByModuleOperID(string id);
        #endregion

        #endregion

        #region 业务逻辑相关

        [ServiceContractDescription(Name = "用户登陆服务", Desc = "用户登陆服务", Url = "RBACService.svc/RBAC_BA_Login?strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}", Get = "strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_BA_Login?strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool RBAC_BA_Login(string strUserAccount, string strPassWord, bool isValidate);

        [ServiceContractDescription(Name = "用户注销服务", Desc = "用户注销服务", Url = "RBACService.svc/RBAC_BA_Logout?strUserAccount={strUserAccount}", Get = "strUserAccount={strUserAccount}", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_BA_Logout?strUserAccount={strUserAccount}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool RBAC_BA_Logout(string strUserAccount);

        [ServiceContractDescription(Name = "身份验证令牌服务", Desc = "身份验证令牌服务", Url = "RBACService.svc/RBAC_RJ_Authentication", Get = "", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_Authentication", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool RBAC_RJ_Authentication();

        [ServiceContractDescription(Name = "模块权限判定服务", Desc = "模块权限判定服务", Url = "RBACService.svc/RBAC_RJ_JudgeModuleByRBACUserCode?strUserCode={strUserCode}&longModuleID={longModuleID}", Get = "strUserCode={strUserCode}", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_JudgeModuleByRBACUserCode?strUserCode={strUserCode}&longModuleID={longModuleID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool RBAC_RJ_JudgeModuleByRBACUserCode(string strUserCode, long longModuleID);

        [ServiceContractDescription(Name = "获取数据实体对象信息服务", Desc = "获取数据实体对象信息服务", Url = "RBACService.svc/SYS_BA_GetEntityInfo", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SYS_BA_GetEntityInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SYS_BA_GetEntityInfo();

        [ServiceContractDescription(Name = "获取数据实体服务列表服务", Desc = "获取数据实体服务列表服务", Url = "RBACService.svc/SYS_BA_GetEntityListInfo", Get = "", Post = "", Return = "", ReturnType = "List")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SYS_BA_GetEntityListInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SYS_BA_GetEntityListInfo();

        [ServiceContractDescription(Name = "验证码服务", Desc = "验证码服务", Url = "RBACService.svc/RBAC_BA_GetVerificationcode", Get = "", Post = "", Return = "string", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_BA_GetVerificationcode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_BA_GetVerificationcode();

        [ServiceContractDescription(Name = "组织机构，模块列表，模块操作列表（权限），角色列表，身份列表", Desc = "组织机构，模块列表，模块操作列表（权限），角色列表，身份列表", Url = "RBACService.svc/RBAC_BA_GetRBACInfoByRBACUserCode?strUserCode={strUserCode}", Get = "strUserCode={strUserCode}", Post = "", Return = "BaseResultBool", ReturnType = "List")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_BA_GetRBACInfoByRBACUserCode?strUserCode={strUserCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RBAC_BA_GetRBACInfoByRBACUserCode(string strUserCode);

        [ServiceContractDescription(Name = "自动生成和修改员工账号名", Desc = "自动生成和修改员工账号名", Url = "RBACService.svc/RBAC_RJ_AutoCreateUserAccount?id={id}&strUserAccount={strUserAccount}", Get = "id={id}&strUserAccount={strUserAccount}", Post = "", Return = "BaseResultDataValue", ReturnType = "HREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_AutoCreateUserAccount?id={id}&strUserAccount={strUserAccount}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_AutoCreateUserAccount(long id, string strUserAccount);

        [ServiceContractDescription(Name = "验证用户账户是否存在", Desc = "验证用户账户是否存在", Url = "RBACService.svc/RBAC_RJ_ValidateUserAccountIsExist?strUserAccount={strUserAccount}", Get = "strUserAccount={strUserAccount}", Post = "", Return = "BaseResultDataValue", ReturnType = "HREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_ValidateUserAccountIsExist?strUserAccount={strUserAccount}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_ValidateUserAccountIsExist(string strUserAccount);

        [ServiceContractDescription(Name = "账户密码重置", Desc = "账户密码重置", Url = "RBACService.svc/RBAC_RJ_ResetAccountPassword?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "HREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_ResetAccountPassword?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_ResetAccountPassword(long id);

        [ServiceContractDescription(Name = "批量增加或减少员工与角色的关系", Desc = "批量增加或减少员工与角色的关系", Url = "RBACService.svc/RBAC_RJ_SetEmpRolesByEmpIdList?empIdList={empIdList}&roleIdList={roleIdList}&flag={flag}", Get = "empIdList={empIdList}&roleIdList={roleIdList}&flag={flag}", Post = "", Return = "BaseResultDataValue", ReturnType = "HREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_SetEmpRolesByEmpIdList?empIdList={empIdList}&roleIdList={roleIdList}&flag={flag}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_SetEmpRolesByEmpIdList(string empIdList, string roleIdList, int flag);

        [ServiceContractDescription(Name = "批量导入员工(支持同步生成账户)", Desc = "批量导入员工(支持同步生成账户)", Url = "RBACService.svc/RBAC_RJ_AddInEmpList", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "HREmployee")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_RJ_AddInEmpList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_AddInEmpList();

        [ServiceContractDescription(Name = "批量导入部门", Desc = "批量导入部门", Url = "RBACService.svc/RBAC_RJ_AddInDeptList", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "HRDept")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_RJ_AddInDeptList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_AddInDeptList();

        [ServiceContractDescription(Name = "复制角色权限", Desc = "复制角色权限", Url = "RBACService.svc/RBAC_RJ_CopyRoleRightByModuleOperID?sourceModuleOperID={sourceModuleOperID}&targetModuleOperID={targetModuleOperID}", Get = "sourceModuleOperID={sourceModuleOperID}&targetModuleOperID={targetModuleOperID}", Post = "", Return = "BaseResultDataValue", ReturnType = "bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_CopyRoleRightByModuleOperID?sourceModuleOperID={sourceModuleOperID}&targetModuleOperID={targetModuleOperID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_CopyRoleRightByModuleOperID(long sourceModuleOperID, long targetModuleOperID);


        [ServiceContractDescription(Name = "导入人员Excel文件", Desc = "导入人员Excel文件", Url = "RBACService.svc/RBAC_RJ_UploadHRDeptEmpByExcel", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_RJ_UploadHRDeptEmpByExcel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message RBAC_RJ_UploadHRDeptEmpByExcel();


        [ServiceContractDescription(Name = "下载格式有问题的Excel文件", Desc = "下载格式有问题的Excel文件", Url = "RBACService.svc/RBAC_RJ_DownLoadExcel?fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}", Get = "fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/RBAC_RJ_DownLoadExcel?fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}")]
        [OperationContract]
        Stream RBAC_RJ_DownLoadExcel(string fileName, string downFileName, int isUpLoadFile, int operateType);

        #endregion

        [ServiceContractDescription(Name = "获取用户信息后,手工调用数据库升级服务", Desc = "获取用户信息后,手工调用数据库升级服务", Url = "RBACService.svc/RBAC_SYS_DBUpdate", Get = "", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_SYS_DBUpdate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool RBAC_SYS_DBUpdate();
    }
}

