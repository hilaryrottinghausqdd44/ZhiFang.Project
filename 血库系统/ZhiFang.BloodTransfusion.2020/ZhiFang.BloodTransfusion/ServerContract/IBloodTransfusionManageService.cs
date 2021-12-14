using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.BloodTransfusion.hisinterface;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.RBAC;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.BloodTransfusion.ServerContract
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IBloodTransfusionManageService”。
    [ServiceContract]
    public interface IBloodTransfusionManageService
    {
        #region 6.6登录处理

        [ServiceContractDescription(Name = "按PUser的帐号及密码登录", Desc = "BS按PUser的帐号及密码登录", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_SYS_LoginOfPUser?strUserAccount={strUserAccount}&strPassWord={strPassWord}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_SYS_LoginOfPUser?strUserAccount={strUserAccount}&strPassWord={strPassWord}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_SYS_LoginOfPUser(string strUserAccount, string strPassWord);

        [ServiceContractDescription(Name = "his调用时验证及获取人员信息入口", Desc = "his调用时验证及获取人员信息入口", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_SYS_LoginOfPUserByHisCode?hisWardCode={hisWardCode}&hisDeptCode={hisDeptCode}&hisDoctorCode={hisDoctorCode}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_SYS_LoginOfPUserByHisCode?hisWardCode={hisWardCode}&hisDeptCode={hisDeptCode}&hisDoctorCode={hisDoctorCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_SYS_LoginOfPUserByHisCode(string hisWardCode, string hisDeptCode, string hisDoctorCode);

        [ServiceContractDescription(Name = "PUser用户注销服务", Desc = "PUser用户注销服务", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_SYS_LogoutOfPUser?strUserAccount={strUserAccount}", Get = "strUserAccount={strUserAccount}", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_SYS_LogoutOfPUser?strUserAccount={strUserAccount}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool BT_SYS_LogoutOfPUser(string strUserAccount);

        [ServiceContractDescription(Name = "获取用户信息后,手工调用数据库升级服务", Desc = "获取用户信息后,手工调用数据库升级服务", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_SYS_DBUpdate", Get = "", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_SYS_DBUpdate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool BT_SYS_DBUpdate();

        #endregion

        #region 公共封装处理

        /// <summary>
        /// 从集成平台或PUser查询RBACUser(HQL)
        /// (1)如果是从集成平台获取,where和sort按集成平台的RBACUser封装;
        /// (2)如果是从PUser获取,where和sort按PUser封装;
        /// (3)fields统一按RBACUser封装;
        /// </summary>
        /// <param name="isliip">是否从集成平台获取:true/false</param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "从集成平台或PUser查询RBACUser(HQL)", Desc = "从集成平台或PUser查询RBACUser(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchRBACUserOfPUserByHQL?isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACUser>", ReturnType = "ListRBACUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchRBACUserOfPUserByHQL?isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchRBACUserOfPUserByHQL(bool isliip, int page, int limit, string fields, string where, string sort, bool isPlanish);

        /// <summary>
        /// (1)where和sort按PUser封装;
        /// (2)fields统一按RBACUser封装;
        /// </summary>
        /// <param name="fieldKey"></param>
        /// <param name="fieldVal"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "通过指定字段(如工号等)获取RBACUser(PUser转换)", Desc = "通过指定字段(如工号等)获取RBACUser(PUser转换)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchRBACUserByFieldKey?fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", Get = "fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACUser>", ReturnType = "RBACUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchRBACUserByFieldKey?fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchRBACUserByFieldKey(string fieldKey, string fieldVal, string fields, bool isPlanish);

        /// <summary>
        /// (1)where和sort按PUser封装;
        /// (2)fields统一按PUser封装;
        /// </summary>
        /// <param name="fieldKey"></param>
        /// <param name="fieldVal"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "通过指定字段(如工号等)获取PUser", Desc = "通过(如工号等)指定字段获取PUser", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchPUserByFieldKey?fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", Get = "fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchPUserByFieldKey?fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchPUserByFieldKey(string fieldKey, string fieldVal, string fields, bool isPlanish);

        #endregion

        #region 封装集成平台服务

        [ServiceContractDescription(Name = "获取智方集成平台的帐户列表信息(HQL)", Desc = "获取智方集成平台的帐户列表信息(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/RS_UDTO_SearchRBACUserOFLIMPByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACUser>", ReturnType = "ListRBACUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchRBACUserOFLIMPByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchRBACUserOFLIMPByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取智方集成平台的员工身份列表信息(HQL)", Desc = "获取智方集成平台的员工身份列表信息(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HREmpIdentity>", ReturnType = "ListHREmpIdentity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHREmpIdentityByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHREmpIdentityByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取智方集成平台的部门员工关系列表信息(HQL)", Desc = "获取智方集成平台的部门员工关系列表信息(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/RBAC_UDTO_SearchHRDeptEmpByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRDeptEmp>", ReturnType = "ListHRDeptEmp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptEmpByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptEmpByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        #region 员工信息

        [ServiceContractDescription(Name = "获取智方集成平台的员工列表信息(HQL)", Desc = "获取智方集成平台的员工列表信息(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/RS_UDTO_SearchHREmployeeOFLIMPByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HREmployee>", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchHREmployeeOFLIMPByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchHREmployeeOFLIMPByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询部门直属员工列表(包含子部门)", Desc = "查询部门直属员工列表(包含子部门)", Url = "ServerWCF/BloodTransfusionManageService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?platformUrl={platformUrl}&where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_GetHREmployeeByHRDeptID?platformUrl={platformUrl}&where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_GetHREmployeeByHRDeptID(string platformUrl, string where, int limit, int page, bool isPlanish, string fields, string sort);

        [ServiceContractDescription(Name = "查询员工(HQL)", Desc = "查询员工(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/RBAC_UDTO_SearchHREmployeeByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HREmployee>", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHREmployeeByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHREmployeeByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "将6.6数据库的人员同步到集成平台中", Desc = "将6.6数据库的人员同步到集成平台中", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SyncPuserListOfHREmployeeToLIMP", Get = "", Post = "string", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SyncPuserListOfHREmployeeToLIMP", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SyncPuserListOfHREmployeeToLIMP(string platformUrl, string syncType);

        [ServiceContractDescription(Name = "将6.6数据库的人员帐号同步到集成平台中", Desc = "将6.6数据库的人员帐号同步到集成平台中", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SyncPuserListOfRBACUseToLIMP", Get = "", Post = "string", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SyncPuserListOfRBACUseToLIMP", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SyncPuserListOfRBACUseToLIMP(string platformUrl, string syncType);

        #endregion

        #region 部门列表

        [ServiceContractDescription(Name = "获取智方集成平台的部门列表信息(HQL)", Desc = "获取智方集成平台的部门列表信息(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/RBAC_UDTO_SearchHRDeptByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRDept>", ReturnType = "ListHRDept")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "根据部门ID获取部门列表树", Desc = "根据部门ID获取部门列表树", Url = "RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree?platformUrl={platformUrl}&id={id}&fields={fields}", Get = "platformUrl={platformUrl}&id={id}&fields={fields}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeHRDept")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_GetHRDeptFrameListTree?platformUrl={platformUrl}&id={id}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_GetHRDeptFrameListTree(string platformUrl, string id, string fields);

        [ServiceContractDescription(Name = "将6.6数据库的科室同步到集成平台中", Desc = "将6.6数据库的科室同步到集成平台中", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SyncDeptListToLIMP", Get = "", Post = "string", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SyncDeptListToLIMP", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SyncDeptListToLIMP(string platformUrl, string syncType);

        #endregion

        #endregion
        
        #region Department

        [ServiceContractDescription(Name = "新增Department", Desc = "新增Department", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddDepartment", Get = "", Post = "Department", Return = "BaseResultDataValue", ReturnType = "Department")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddDepartment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddDepartment(Department entity);

        [ServiceContractDescription(Name = "修改Department", Desc = "修改Department", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateDepartment", Get = "", Post = "Department", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateDepartment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateDepartment(Department entity);

        [ServiceContractDescription(Name = "修改Department指定的属性", Desc = "修改Department指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateDepartmentByField", Get = "", Post = "Department", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateDepartmentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateDepartmentByField(Department entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除Department", Desc = "删除Department", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelDepartment?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelDepartment?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelDepartment(int id);

        [ServiceContractDescription(Name = "查询Department", Desc = "查询Department", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchDepartment", Get = "", Post = "Department", Return = "BaseResultList<Department>", ReturnType = "ListDepartment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDepartment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDepartment(Department entity);

        [ServiceContractDescription(Name = "查询Department(HQL)", Desc = "查询Department(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Department>", ReturnType = "ListDepartment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDepartmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDepartmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Department", Desc = "通过主键ID查询Department", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Department>", ReturnType = "Department")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDepartmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDepartmentById(int id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "按申请信息建立病区与科室关系", Desc = "按申请信息建立病区与科室关系", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddWarpAndDept", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_AddWarpAndDept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_AddWarpAndDept();

        #endregion

        #region PUser

        [ServiceContractDescription(Name = "新增PUser", Desc = "新增PUser", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddPUser", Get = "", Post = "PUser", Return = "BaseResultDataValue", ReturnType = "PUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddPUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddPUser(PUser entity);

        [ServiceContractDescription(Name = "修改PUser", Desc = "修改PUser", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdatePUser", Get = "", Post = "PUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdatePUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdatePUser(PUser entity);

        [ServiceContractDescription(Name = "修改PUser指定的属性", Desc = "修改PUser指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdatePUserByField", Get = "", Post = "PUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdatePUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdatePUserByField(PUser entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除PUser", Desc = "删除PUser", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelPUser?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelPUser?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelPUser(long id);

        [ServiceContractDescription(Name = "查询PUser", Desc = "查询PUser", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchPUser", Get = "", Post = "PUser", Return = "BaseResultList<PUser>", ReturnType = "ListPUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchPUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchPUser(PUser entity);

        [ServiceContractDescription(Name = "查询PUser(HQL)", Desc = "查询PUser(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchPUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "ListPUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchPUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchPUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询PUser", Desc = "通过主键ID查询PUser", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchPUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchPUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchPUserById(long id, string fields, bool isPlanish);

        #endregion

        #region DepartmentUser

        [ServiceContractDescription(Name = "新增DepartmentUser", Desc = "新增DepartmentUser", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddDepartmentUser", Get = "", Post = "DepartmentUser", Return = "BaseResultDataValue", ReturnType = "DepartmentUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddDepartmentUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddDepartmentUser(DepartmentUser entity);

        [ServiceContractDescription(Name = "修改DepartmentUser", Desc = "修改DepartmentUser", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateDepartmentUser", Get = "", Post = "DepartmentUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateDepartmentUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateDepartmentUser(DepartmentUser entity);

        [ServiceContractDescription(Name = "修改DepartmentUser指定的属性", Desc = "修改DepartmentUser指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateDepartmentUserByField", Get = "", Post = "DepartmentUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateDepartmentUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateDepartmentUserByField(DepartmentUser entity, string fields);

        [ServiceContractDescription(Name = "删除DepartmentUser", Desc = "删除DepartmentUser", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelDepartmentUser?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelDepartmentUser?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelDepartmentUser(long id);

        [ServiceContractDescription(Name = "查询DepartmentUser", Desc = "查询DepartmentUser", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentUser", Get = "", Post = "DepartmentUser", Return = "BaseResultList<DepartmentUser>", ReturnType = "ListDepartmentUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDepartmentUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDepartmentUser(DepartmentUser entity);

        [ServiceContractDescription(Name = "查询DepartmentUser(HQL)", Desc = "查询DepartmentUser(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<DepartmentUser>", ReturnType = "ListDepartmentUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDepartmentUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDepartmentUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询DepartmentUser", Desc = "通过主键ID查询DepartmentUser", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<DepartmentUser>", ReturnType = "DepartmentUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDepartmentUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDepartmentUserById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodDocGrade

        [ServiceContractDescription(Name = "新增Blood_docGrade", Desc = "新增Blood_docGrade", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodDocGrade", Get = "", Post = "BloodDocGrade", Return = "BaseResultDataValue", ReturnType = "BloodDocGrade")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodDocGrade", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodDocGrade(BloodDocGrade entity);

        [ServiceContractDescription(Name = "修改Blood_docGrade", Desc = "修改Blood_docGrade", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodDocGrade", Get = "", Post = "BloodDocGrade", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodDocGrade", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodDocGrade(BloodDocGrade entity);

        [ServiceContractDescription(Name = "修改Blood_docGrade指定的属性", Desc = "修改Blood_docGrade指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodDocGradeByField", Get = "", Post = "BloodDocGrade", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodDocGradeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodDocGradeByField(BloodDocGrade entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_docGrade", Desc = "删除Blood_docGrade", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodDocGrade?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodDocGrade?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodDocGrade(long id);

        [ServiceContractDescription(Name = "查询Blood_docGrade", Desc = "查询Blood_docGrade", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodDocGrade", Get = "", Post = "BloodDocGrade", Return = "BaseResultList<BloodDocGrade>", ReturnType = "ListBloodDocGrade")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodDocGrade", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodDocGrade(BloodDocGrade entity);

        [ServiceContractDescription(Name = "查询Blood_docGrade(HQL)", Desc = "查询Blood_docGrade(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodDocGradeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodDocGrade>", ReturnType = "ListBloodDocGrade")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodDocGradeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodDocGradeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_docGrade", Desc = "通过主键ID查询Blood_docGrade", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodDocGradeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodDocGrade>", ReturnType = "BloodDocGrade")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodDocGradeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodDocGradeById(long id, string fields, bool isPlanish);
        #endregion

        #region BUserUIConfig

        [ServiceContractDescription(Name = "新增B_UserUIConfig", Desc = "新增B_UserUIConfig", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBUserUIConfig", Get = "", Post = "BUserUIConfig", Return = "BaseResultDataValue", ReturnType = "BUserUIConfig")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBUserUIConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBUserUIConfig(BUserUIConfig entity);

        [ServiceContractDescription(Name = "修改B_UserUIConfig", Desc = "修改B_UserUIConfig", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBUserUIConfig", Get = "", Post = "BUserUIConfig", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBUserUIConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBUserUIConfig(BUserUIConfig entity);

        [ServiceContractDescription(Name = "修改B_UserUIConfig指定的属性", Desc = "修改B_UserUIConfig指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBUserUIConfigByField", Get = "", Post = "BUserUIConfig", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBUserUIConfigByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBUserUIConfigByField(BUserUIConfig entity, string fields);

        [ServiceContractDescription(Name = "删除B_UserUIConfig", Desc = "删除B_UserUIConfig", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBUserUIConfig?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBUserUIConfig?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBUserUIConfig(long id);

        [ServiceContractDescription(Name = "查询B_UserUIConfig", Desc = "查询B_UserUIConfig", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBUserUIConfig", Get = "", Post = "BUserUIConfig", Return = "BaseResultList<BUserUIConfig>", ReturnType = "ListBUserUIConfig")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBUserUIConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBUserUIConfig(BUserUIConfig entity);

        [ServiceContractDescription(Name = "查询B_UserUIConfig(HQL)", Desc = "查询B_UserUIConfig(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBUserUIConfigByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BUserUIConfig>", ReturnType = "ListBUserUIConfig")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBUserUIConfigByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBUserUIConfigByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_UserUIConfig", Desc = "通过主键ID查询B_UserUIConfig", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBUserUIConfigById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BUserUIConfig>", ReturnType = "BUserUIConfig")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBUserUIConfigById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBUserUIConfigById(long id, string fields, bool isPlanish);
        #endregion

        #region SCOperation

        [ServiceContractDescription(Name = "新增公共操作记录表", Desc = "新增公共操作记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultDataValue", ReturnType = "SCOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "修改公共操作记录表", Desc = "修改公共操作记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "修改公共操作记录表指定的属性", Desc = "修改公共操作记录表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateSCOperationByField", Get = "", Post = "SCOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateSCOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateSCOperationByField(SCOperation entity, string fields);

        [ServiceContractDescription(Name = "删除公共操作记录表", Desc = "删除公共操作记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelSCOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelSCOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelSCOperation(long id);

        [ServiceContractDescription(Name = "查询公共操作记录表", Desc = "查询公共操作记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultList<SCOperation>", ReturnType = "ListSCOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "查询公共操作记录表(HQL)", Desc = "查询公共操作记录表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchSCOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCOperation>", ReturnType = "ListSCOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchSCOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchSCOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共操作记录表", Desc = "通过主键ID查询公共操作记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchSCOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCOperation>", ReturnType = "SCOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchSCOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchSCOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodABO

        [ServiceContractDescription(Name = "新增血型表", Desc = "新增血型表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodABO", Get = "", Post = "BloodABO", Return = "BaseResultDataValue", ReturnType = "BloodABO")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodABO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodABO(BloodABO entity);

        [ServiceContractDescription(Name = "修改血型表", Desc = "修改血型表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodABO", Get = "", Post = "BloodABO", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodABO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodABO(BloodABO entity);

        [ServiceContractDescription(Name = "修改血型表指定的属性", Desc = "修改血型表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodABOByField", Get = "", Post = "BloodABO", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodABOByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodABOByField(BloodABO entity, string fields,long empID, string empName);

        [ServiceContractDescription(Name = "删除血型表", Desc = "删除血型表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodABO?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodABO?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodABO(long id);

        [ServiceContractDescription(Name = "查询血型表", Desc = "查询血型表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodABO", Get = "", Post = "BloodABO", Return = "BaseResultList<BloodABO>", ReturnType = "ListBloodABO")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodABO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodABO(BloodABO entity);

        [ServiceContractDescription(Name = "查询血型表(HQL)", Desc = "查询血型表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodABOByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodABO>", ReturnType = "ListBloodABO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodABOByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodABOByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询血型表", Desc = "通过主键ID查询血型表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodABOById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodABO>", ReturnType = "BloodABO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodABOById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodABOById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodAggluItem

        [ServiceContractDescription(Name = "新增凝集规则明细字典", Desc = "新增凝集规则明细字典", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodAggluItem", Get = "", Post = "BloodAggluItem", Return = "BaseResultDataValue", ReturnType = "BloodAggluItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodAggluItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodAggluItem(BloodAggluItem entity);

        [ServiceContractDescription(Name = "修改凝集规则明细字典", Desc = "修改凝集规则明细字典", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodAggluItem", Get = "", Post = "BloodAggluItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodAggluItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodAggluItem(BloodAggluItem entity);

        [ServiceContractDescription(Name = "修改凝集规则明细字典指定的属性", Desc = "修改凝集规则明细字典指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodAggluItemByField", Get = "", Post = "BloodAggluItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodAggluItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodAggluItemByField(BloodAggluItem entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除凝集规则明细字典", Desc = "删除凝集规则明细字典", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodAggluItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodAggluItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodAggluItem(long id);

        [ServiceContractDescription(Name = "查询凝集规则明细字典", Desc = "查询凝集规则明细字典", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodAggluItem", Get = "", Post = "BloodAggluItem", Return = "BaseResultList<BloodAggluItem>", ReturnType = "ListBloodAggluItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodAggluItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodAggluItem(BloodAggluItem entity);

        [ServiceContractDescription(Name = "查询凝集规则明细字典(HQL)", Desc = "查询凝集规则明细字典(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodAggluItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodAggluItem>", ReturnType = "ListBloodAggluItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodAggluItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodAggluItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询凝集规则明细字典", Desc = "通过主键ID查询凝集规则明细字典", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodAggluItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodAggluItem>", ReturnType = "BloodAggluItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodAggluItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodAggluItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBagHandoverOper

        [ServiceContractDescription(Name = "新增交接记录主单表", Desc = "新增交接记录主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagHandoverOper", Get = "", Post = "BloodBagHandoverOper", Return = "BaseResultDataValue", ReturnType = "BloodBagHandoverOper")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagHandoverOper", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagHandoverOper(BloodBagHandoverOper entity);

        [ServiceContractDescription(Name = "修改交接记录主单表", Desc = "修改交接记录主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagHandoverOper", Get = "", Post = "BloodBagHandoverOper", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagHandoverOper", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagHandoverOper(BloodBagHandoverOper entity);

        [ServiceContractDescription(Name = "修改交接记录主单表指定的属性", Desc = "修改交接记录主单表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagHandoverOperByField", Get = "", Post = "BloodBagHandoverOper", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagHandoverOperByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagHandoverOperByField(BloodBagHandoverOper entity, string fields);

        [ServiceContractDescription(Name = "删除交接记录主单表", Desc = "删除交接记录主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagHandoverOper?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagHandoverOper?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagHandoverOper(long id);

        [ServiceContractDescription(Name = "查询交接记录主单表", Desc = "查询交接记录主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagHandoverOper", Get = "", Post = "BloodBagHandoverOper", Return = "BaseResultList<BloodBagHandoverOper>", ReturnType = "ListBloodBagHandoverOper")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagHandoverOper", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagHandoverOper(BloodBagHandoverOper entity);

        [ServiceContractDescription(Name = "查询交接记录主单表(HQL)", Desc = "查询交接记录主单表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagHandoverOperByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagHandoverOper>", ReturnType = "ListBloodBagHandoverOper")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagHandoverOperByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagHandoverOperByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询交接记录主单表", Desc = "通过主键ID查询交接记录主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagHandoverOperById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagHandoverOper>", ReturnType = "BloodBagHandoverOper")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagHandoverOperById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagHandoverOperById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBagProcess

        [ServiceContractDescription(Name = "新增血袋加工记录表", Desc = "新增血袋加工记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagProcess", Get = "", Post = "BloodBagProcess", Return = "BaseResultDataValue", ReturnType = "BloodBagProcess")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagProcess", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagProcess(BloodBagProcess entity);

        [ServiceContractDescription(Name = "修改血袋加工记录表", Desc = "修改血袋加工记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagProcess", Get = "", Post = "BloodBagProcess", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagProcess", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagProcess(BloodBagProcess entity);

        [ServiceContractDescription(Name = "修改血袋加工记录表指定的属性", Desc = "修改血袋加工记录表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagProcessByField", Get = "", Post = "BloodBagProcess", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagProcessByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagProcessByField(BloodBagProcess entity, string fields);

        [ServiceContractDescription(Name = "删除血袋加工记录表", Desc = "删除血袋加工记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagProcess?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagProcess?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagProcess(long id);

        [ServiceContractDescription(Name = "查询血袋加工记录表", Desc = "查询血袋加工记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcess", Get = "", Post = "BloodBagProcess", Return = "BaseResultList<BloodBagProcess>", ReturnType = "ListBloodBagProcess")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcess", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcess(BloodBagProcess entity);

        [ServiceContractDescription(Name = "查询血袋加工记录表(HQL)", Desc = "查询血袋加工记录表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagProcess>", ReturnType = "ListBloodBagProcess")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcessByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcessByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询血袋加工记录表", Desc = "通过主键ID查询血袋加工记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagProcess>", ReturnType = "BloodBagProcess")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcessById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcessById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBagProcessType

        [ServiceContractDescription(Name = "新增加工类型表", Desc = "新增加工类型表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagProcessType", Get = "", Post = "BloodBagProcessType", Return = "BaseResultDataValue", ReturnType = "BloodBagProcessType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagProcessType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagProcessType(BloodBagProcessType entity);

        [ServiceContractDescription(Name = "修改加工类型表", Desc = "修改加工类型表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagProcessType", Get = "", Post = "BloodBagProcessType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagProcessType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagProcessType(BloodBagProcessType entity);

        [ServiceContractDescription(Name = "修改加工类型表指定的属性", Desc = "修改加工类型表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagProcessTypeByField", Get = "", Post = "BloodBagProcessType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagProcessTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagProcessTypeByField(BloodBagProcessType entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除加工类型表", Desc = "删除加工类型表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagProcessType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagProcessType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagProcessType(long id);

        [ServiceContractDescription(Name = "查询加工类型表", Desc = "查询加工类型表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessType", Get = "", Post = "BloodBagProcessType", Return = "BaseResultList<BloodBagProcessType>", ReturnType = "ListBloodBagProcessType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcessType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcessType(BloodBagProcessType entity);

        [ServiceContractDescription(Name = "查询加工类型表(HQL)", Desc = "查询加工类型表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagProcessType>", ReturnType = "ListBloodBagProcessType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcessTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcessTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询加工类型表", Desc = "通过主键ID查询加工类型表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagProcessType>", ReturnType = "BloodBagProcessType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcessTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcessTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBagProcessTypeQry

        [ServiceContractDescription(Name = "新增血制品的加工类型", Desc = "新增血制品的加工类型", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagProcessTypeQry", Get = "", Post = "BloodBagProcessTypeQry", Return = "BaseResultDataValue", ReturnType = "BloodBagProcessTypeQry")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagProcessTypeQry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagProcessTypeQry(BloodBagProcessTypeQry entity);

        [ServiceContractDescription(Name = "修改血制品的加工类型", Desc = "修改血制品的加工类型", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagProcessTypeQry", Get = "", Post = "BloodBagProcessTypeQry", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagProcessTypeQry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagProcessTypeQry(BloodBagProcessTypeQry entity);

        [ServiceContractDescription(Name = "修改血制品的加工类型指定的属性", Desc = "修改血制品的加工类型指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagProcessTypeQryByField", Get = "", Post = "BloodBagProcessTypeQry", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagProcessTypeQryByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagProcessTypeQryByField(BloodBagProcessTypeQry entity, string fields);

        [ServiceContractDescription(Name = "删除血制品的加工类型", Desc = "删除血制品的加工类型", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagProcessTypeQry?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagProcessTypeQry?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagProcessTypeQry(long id);

        [ServiceContractDescription(Name = "查询血制品的加工类型", Desc = "查询血制品的加工类型", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessTypeQry", Get = "", Post = "BloodBagProcessTypeQry", Return = "BaseResultList<BloodBagProcessTypeQry>", ReturnType = "ListBloodBagProcessTypeQry")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcessTypeQry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcessTypeQry(BloodBagProcessTypeQry entity);

        [ServiceContractDescription(Name = "查询血制品的加工类型(HQL)", Desc = "查询血制品的加工类型(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessTypeQryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagProcessTypeQry>", ReturnType = "ListBloodBagProcessTypeQry")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcessTypeQryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcessTypeQryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询血制品的加工类型", Desc = "通过主键ID查询血制品的加工类型", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessTypeQryById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagProcessTypeQry>", ReturnType = "BloodBagProcessTypeQry")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcessTypeQryById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcessTypeQryById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBagRecordDtl

        [ServiceContractDescription(Name = "新增血袋核对记录明细表", Desc = "新增血袋核对记录明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagRecordDtl", Get = "", Post = "BloodBagRecordDtl", Return = "BaseResultDataValue", ReturnType = "BloodBagRecordDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagRecordDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagRecordDtl(BloodBagRecordDtl entity);

        [ServiceContractDescription(Name = "修改血袋核对记录明细表", Desc = "修改血袋核对记录明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagRecordDtl", Get = "", Post = "BloodBagRecordDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagRecordDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagRecordDtl(BloodBagRecordDtl entity);

        [ServiceContractDescription(Name = "修改血袋核对记录明细表指定的属性", Desc = "修改血袋核对记录明细表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagRecordDtlByField", Get = "", Post = "BloodBagRecordDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagRecordDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagRecordDtlByField(BloodBagRecordDtl entity, string fields);

        [ServiceContractDescription(Name = "删除血袋核对记录明细表", Desc = "删除血袋核对记录明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagRecordDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagRecordDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagRecordDtl(long id);

        [ServiceContractDescription(Name = "查询血袋核对记录明细表", Desc = "查询血袋核对记录明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagRecordDtl", Get = "", Post = "BloodBagRecordDtl", Return = "BaseResultList<BloodBagRecordDtl>", ReturnType = "ListBloodBagRecordDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagRecordDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagRecordDtl(BloodBagRecordDtl entity);

        [ServiceContractDescription(Name = "查询血袋核对记录明细表(HQL)", Desc = "查询血袋核对记录明细表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagRecordDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagRecordDtl>", ReturnType = "ListBloodBagRecordDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagRecordDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagRecordDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询血袋核对记录明细表", Desc = "通过主键ID查询血袋核对记录明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagRecordDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagRecordDtl>", ReturnType = "BloodBagRecordDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagRecordDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagRecordDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBagRecordItem

        [ServiceContractDescription(Name = "新增血袋记录明细字典表", Desc = "新增血袋记录明细字典表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagRecordItem", Get = "", Post = "BloodBagRecordItem", Return = "BaseResultDataValue", ReturnType = "BloodBagRecordItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagRecordItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagRecordItem(BloodBagRecordItem entity);

        [ServiceContractDescription(Name = "修改血袋记录明细字典表", Desc = "修改血袋记录明细字典表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagRecordItem", Get = "", Post = "BloodBagRecordItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagRecordItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagRecordItem(BloodBagRecordItem entity);

        [ServiceContractDescription(Name = "修改血袋记录明细字典表指定的属性", Desc = "修改血袋记录明细字典表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagRecordItemByField", Get = "", Post = "BloodBagRecordItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagRecordItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagRecordItemByField(BloodBagRecordItem entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除血袋记录明细字典表", Desc = "删除血袋记录明细字典表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagRecordItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagRecordItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagRecordItem(long id);

        [ServiceContractDescription(Name = "查询血袋记录明细字典表", Desc = "查询血袋记录明细字典表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagRecordItem", Get = "", Post = "BloodBagRecordItem", Return = "BaseResultList<BloodBagRecordItem>", ReturnType = "ListBloodBagRecordItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagRecordItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagRecordItem(BloodBagRecordItem entity);

        [ServiceContractDescription(Name = "查询血袋记录明细字典表(HQL)", Desc = "查询血袋记录明细字典表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagRecordItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagRecordItem>", ReturnType = "ListBloodBagRecordItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagRecordItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagRecordItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询血袋记录明细字典表", Desc = "通过主键ID查询血袋记录明细字典表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagRecordItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagRecordItem>", ReturnType = "BloodBagRecordItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagRecordItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagRecordItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBagRecordType

        [ServiceContractDescription(Name = "新增血袋记录类型字典表", Desc = "新增血袋记录类型字典表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagRecordType", Get = "", Post = "BloodBagRecordType", Return = "BaseResultDataValue", ReturnType = "BloodBagRecordType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagRecordType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagRecordType(BloodBagRecordType entity);

        [ServiceContractDescription(Name = "修改血袋记录类型字典表", Desc = "修改血袋记录类型字典表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagRecordType", Get = "", Post = "BloodBagRecordType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagRecordType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagRecordType(BloodBagRecordType entity);

        [ServiceContractDescription(Name = "修改血袋记录类型字典表指定的属性", Desc = "修改血袋记录类型字典表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagRecordTypeByField", Get = "", Post = "BloodBagRecordType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagRecordTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagRecordTypeByField(BloodBagRecordType entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除血袋记录类型字典表", Desc = "删除血袋记录类型字典表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagRecordType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagRecordType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagRecordType(long id);

        [ServiceContractDescription(Name = "查询血袋记录类型字典表", Desc = "查询血袋记录类型字典表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagRecordType", Get = "", Post = "BloodBagRecordType", Return = "BaseResultList<BloodBagRecordType>", ReturnType = "ListBloodBagRecordType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagRecordType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagRecordType(BloodBagRecordType entity);

        [ServiceContractDescription(Name = "查询血袋记录类型字典表(HQL)", Desc = "查询血袋记录类型字典表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagRecordTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagRecordType>", ReturnType = "ListBloodBagRecordType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagRecordTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagRecordTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询血袋记录类型字典表", Desc = "通过主键ID查询血袋记录类型字典表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagRecordTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagRecordType>", ReturnType = "BloodBagRecordType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagRecordTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagRecordTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBInForm

        [ServiceContractDescription(Name = "新增入库主单", Desc = "新增入库主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBInForm", Get = "", Post = "BloodBInForm", Return = "BaseResultDataValue", ReturnType = "BloodBInForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBInForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBInForm(BloodBInForm entity);

        [ServiceContractDescription(Name = "修改入库主单", Desc = "修改入库主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBInForm", Get = "", Post = "BloodBInForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBInForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBInForm(BloodBInForm entity);

        [ServiceContractDescription(Name = "修改入库主单指定的属性", Desc = "修改入库主单指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBInFormByField", Get = "", Post = "BloodBInForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBInFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBInFormByField(BloodBInForm entity, string fields);

        [ServiceContractDescription(Name = "删除入库主单", Desc = "删除入库主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBInForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBInForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBInForm(long id);

        [ServiceContractDescription(Name = "查询入库主单", Desc = "查询入库主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInForm", Get = "", Post = "BloodBInForm", Return = "BaseResultList<BloodBInForm>", ReturnType = "ListBloodBInForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInForm(BloodBInForm entity);

        [ServiceContractDescription(Name = "查询入库主单(HQL)", Desc = "查询入库主单(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBInForm>", ReturnType = "ListBloodBInForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询入库主单", Desc = "通过主键ID查询入库主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBInForm>", ReturnType = "BloodBInForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInFormById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBInItem

        [ServiceContractDescription(Name = "新增入库明细", Desc = "新增入库明细", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBInItem", Get = "", Post = "BloodBInItem", Return = "BaseResultDataValue", ReturnType = "BloodBInItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBInItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBInItem(BloodBInItem entity);

        [ServiceContractDescription(Name = "修改入库明细", Desc = "修改入库明细", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBInItem", Get = "", Post = "BloodBInItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBInItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBInItem(BloodBInItem entity);

        [ServiceContractDescription(Name = "修改入库明细指定的属性", Desc = "修改入库明细指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBInItemByField", Get = "", Post = "BloodBInItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBInItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBInItemByField(BloodBInItem entity, string fields);

        [ServiceContractDescription(Name = "删除入库明细", Desc = "删除入库明细", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBInItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBInItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBInItem(long id);

        [ServiceContractDescription(Name = "查询入库明细", Desc = "查询入库明细", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInItem", Get = "", Post = "BloodBInItem", Return = "BaseResultList<BloodBInItem>", ReturnType = "ListBloodBInItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInItem(BloodBInItem entity);

        [ServiceContractDescription(Name = "查询入库明细(HQL)", Desc = "查询入库明细(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBInItem>", ReturnType = "ListBloodBInItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询入库明细", Desc = "通过主键ID查询入库明细", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBInItem>", ReturnType = "BloodBInItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBOutForm

        [ServiceContractDescription(Name = "新增发血主单表", Desc = "新增发血主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBOutForm", Get = "", Post = "BloodBOutForm", Return = "BaseResultDataValue", ReturnType = "BloodBOutForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBOutForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBOutForm(BloodBOutForm entity);

        [ServiceContractDescription(Name = "修改发血主单表", Desc = "修改发血主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBOutForm", Get = "", Post = "BloodBOutForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBOutForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBOutForm(BloodBOutForm entity);

        [ServiceContractDescription(Name = "修改发血主单表指定的属性", Desc = "修改发血主单表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBOutFormByField", Get = "", Post = "BloodBOutForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBOutFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBOutFormByField(BloodBOutForm entity, string fields);

        [ServiceContractDescription(Name = "删除发血主单表", Desc = "删除发血主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBOutForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBOutForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBOutForm(long id);

        [ServiceContractDescription(Name = "查询发血主单表", Desc = "查询发血主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutForm", Get = "", Post = "BloodBOutForm", Return = "BaseResultList<BloodBOutForm>", ReturnType = "ListBloodBOutForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutForm(BloodBOutForm entity);

        [ServiceContractDescription(Name = "查询发血主单表(HQL)", Desc = "查询发血主单表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutForm>", ReturnType = "ListBloodBOutForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询发血主单表", Desc = "通过主键ID查询发血主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutForm>", ReturnType = "BloodBOutForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutFormById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBOutItem

        [ServiceContractDescription(Name = "新增发血明细表", Desc = "新增发血明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBOutItem", Get = "", Post = "BloodBOutItem", Return = "BaseResultDataValue", ReturnType = "BloodBOutItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBOutItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBOutItem(BloodBOutItem entity);

        [ServiceContractDescription(Name = "修改发血明细表", Desc = "修改发血明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBOutItem", Get = "", Post = "BloodBOutItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBOutItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBOutItem(BloodBOutItem entity);

        [ServiceContractDescription(Name = "修改发血明细表指定的属性", Desc = "修改发血明细表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBOutItemByField", Get = "", Post = "BloodBOutItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBOutItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBOutItemByField(BloodBOutItem entity, string fields);

        [ServiceContractDescription(Name = "删除发血明细表", Desc = "删除发血明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBOutItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBOutItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBOutItem(long id);

        [ServiceContractDescription(Name = "查询发血明细表", Desc = "查询发血明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItem", Get = "", Post = "BloodBOutItem", Return = "BaseResultList<BloodBOutItem>", ReturnType = "ListBloodBOutItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutItem(BloodBOutItem entity);

        [ServiceContractDescription(Name = "查询发血明细表(HQL)", Desc = "查询发血明细表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutItem>", ReturnType = "ListBloodBOutItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询发血明细表", Desc = "通过主键ID查询发血明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutItem>", ReturnType = "BloodBOutItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBPreForm

        [ServiceContractDescription(Name = "新增配血主单", Desc = "新增配血主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBPreForm", Get = "", Post = "BloodBPreForm", Return = "BaseResultDataValue", ReturnType = "BloodBPreForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBPreForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBPreForm(BloodBPreForm entity);

        [ServiceContractDescription(Name = "修改配血主单", Desc = "修改配血主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBPreForm", Get = "", Post = "BloodBPreForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBPreForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBPreForm(BloodBPreForm entity);

        [ServiceContractDescription(Name = "修改配血主单指定的属性", Desc = "修改配血主单指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBPreFormByField", Get = "", Post = "BloodBPreForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBPreFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBPreFormByField(BloodBPreForm entity, string fields);

        [ServiceContractDescription(Name = "删除配血主单", Desc = "删除配血主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBPreForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBPreForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBPreForm(long id);

        [ServiceContractDescription(Name = "查询配血主单", Desc = "查询配血主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBPreForm", Get = "", Post = "BloodBPreForm", Return = "BaseResultList<BloodBPreForm>", ReturnType = "ListBloodBPreForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBPreForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBPreForm(BloodBPreForm entity);

        [ServiceContractDescription(Name = "查询配血主单(HQL)", Desc = "查询配血主单(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBPreFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBPreForm>", ReturnType = "ListBloodBPreForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBPreFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBPreFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询配血主单", Desc = "通过主键ID查询配血主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBPreFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBPreForm>", ReturnType = "BloodBPreForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBPreFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBPreFormById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBPreItem

        [ServiceContractDescription(Name = "新增配血明细表", Desc = "新增配血明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBPreItem", Get = "", Post = "BloodBPreItem", Return = "BaseResultDataValue", ReturnType = "BloodBPreItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBPreItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBPreItem(BloodBPreItem entity);

        [ServiceContractDescription(Name = "修改配血明细表", Desc = "修改配血明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBPreItem", Get = "", Post = "BloodBPreItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBPreItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBPreItem(BloodBPreItem entity);

        [ServiceContractDescription(Name = "修改配血明细表指定的属性", Desc = "修改配血明细表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBPreItemByField", Get = "", Post = "BloodBPreItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBPreItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBPreItemByField(BloodBPreItem entity, string fields);

        [ServiceContractDescription(Name = "删除配血明细表", Desc = "删除配血明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBPreItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBPreItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBPreItem(long id);

        [ServiceContractDescription(Name = "查询配血明细表", Desc = "查询配血明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBPreItem", Get = "", Post = "BloodBPreItem", Return = "BaseResultList<BloodBPreItem>", ReturnType = "ListBloodBPreItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBPreItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBPreItem(BloodBPreItem entity);

        [ServiceContractDescription(Name = "查询配血明细表(HQL)", Desc = "查询配血明细表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBPreItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBPreItem>", ReturnType = "ListBloodBPreItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBPreItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBPreItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询配血明细表", Desc = "通过主键ID查询配血明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBPreItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBPreItem>", ReturnType = "BloodBPreItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBPreItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBPreItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBReqForm

        [ServiceContractDescription(Name = "新增用血申请主单表", Desc = "新增用血申请主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqForm", Get = "", Post = "BloodBReqForm", Return = "BaseResultDataValue", ReturnType = "BloodBReqForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBReqForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBReqForm(BloodBReqForm entity);

        [ServiceContractDescription(Name = "修改用血申请主单表", Desc = "修改用血申请主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqForm", Get = "", Post = "BloodBReqForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqForm(BloodBReqForm entity);

        [ServiceContractDescription(Name = "修改用血申请主单表指定的属性", Desc = "修改用血申请主单表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormByField", Get = "", Post = "BloodBReqForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqFormByField(BloodBReqForm entity, string fields);

        [ServiceContractDescription(Name = "删除用血申请主单表", Desc = "删除用血申请主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBReqForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBReqForm(long id);

        [ServiceContractDescription(Name = "查询用血申请主单表", Desc = "查询用血申请主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqForm", Get = "", Post = "BloodBReqForm", Return = "BaseResultList<BloodBReqForm>", ReturnType = "ListBloodBReqForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqForm(BloodBReqForm entity);

        [ServiceContractDescription(Name = "查询用血申请主单表(HQL)", Desc = "查询用血申请主单表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqForm>", ReturnType = "ListBloodBReqForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询用血申请主单表", Desc = "通过主键ID查询用血申请主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqForm>", ReturnType = "BloodBReqForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBReqFormResult

        [ServiceContractDescription(Name = "新增申请相关检验结果表", Desc = "新增申请相关检验结果表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqFormResult", Get = "", Post = "BloodBReqFormResult", Return = "BaseResultDataValue", ReturnType = "BloodBReqFormResult")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBReqFormResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBReqFormResult(BloodBReqFormResult entity);

        [ServiceContractDescription(Name = "修改申请相关检验结果表", Desc = "修改申请相关检验结果表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormResult", Get = "", Post = "BloodBReqFormResult", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqFormResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqFormResult(BloodBReqFormResult entity);

        [ServiceContractDescription(Name = "修改申请相关检验结果表指定的属性", Desc = "修改申请相关检验结果表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormResultByField", Get = "", Post = "BloodBReqFormResult", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqFormResultByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqFormResultByField(BloodBReqFormResult entity, string fields);

        [ServiceContractDescription(Name = "删除申请相关检验结果表", Desc = "删除申请相关检验结果表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqFormResult?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBReqFormResult?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBReqFormResult(long id);

        [ServiceContractDescription(Name = "查询申请相关检验结果表", Desc = "查询申请相关检验结果表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormResult", Get = "", Post = "BloodBReqFormResult", Return = "BaseResultList<BloodBReqFormResult>", ReturnType = "ListBloodBReqFormResult")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormResult(BloodBReqFormResult entity);

        [ServiceContractDescription(Name = "查询申请相关检验结果表(HQL)", Desc = "查询申请相关检验结果表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormResultByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqFormResult>", ReturnType = "ListBloodBReqFormResult")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormResultByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormResultByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询申请相关检验结果表", Desc = "通过主键ID查询申请相关检验结果表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormResultById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqFormResult>", ReturnType = "BloodBReqFormResult")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormResultById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormResultById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBReqItem

        [ServiceContractDescription(Name = "新增用血申请明细信息表", Desc = "新增用血申请明细信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqItem", Get = "", Post = "BloodBReqItem", Return = "BaseResultDataValue", ReturnType = "BloodBReqItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBReqItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBReqItem(BloodBReqItem entity);

        [ServiceContractDescription(Name = "修改用血申请明细信息表", Desc = "修改用血申请明细信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqItem", Get = "", Post = "BloodBReqItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqItem(BloodBReqItem entity);

        [ServiceContractDescription(Name = "修改用血申请明细信息表指定的属性", Desc = "修改用血申请明细信息表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqItemByField", Get = "", Post = "BloodBReqItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqItemByField(BloodBReqItem entity, string fields);

        [ServiceContractDescription(Name = "删除用血申请明细信息表", Desc = "删除用血申请明细信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBReqItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBReqItem(long id);

        [ServiceContractDescription(Name = "查询用血申请明细信息表", Desc = "查询用血申请明细信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItem", Get = "", Post = "BloodBReqItem", Return = "BaseResultList<BloodBReqItem>", ReturnType = "ListBloodBReqItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqItem(BloodBReqItem entity);

        [ServiceContractDescription(Name = "查询用血申请明细信息表(HQL)", Desc = "查询用血申请明细信息表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqItem>", ReturnType = "ListBloodBReqItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询用血申请明细信息表", Desc = "通过主键ID查询用血申请明细信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqItem>", ReturnType = "BloodBReqItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBTestItem

        [ServiceContractDescription(Name = "新增检验项目表 ", Desc = "新增检验项目表 ", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBTestItem", Get = "", Post = "BloodBTestItem", Return = "BaseResultDataValue", ReturnType = "BloodBTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBTestItem(BloodBTestItem entity);

        [ServiceContractDescription(Name = "修改检验项目表 ", Desc = "修改检验项目表 ", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBTestItem", Get = "", Post = "BloodBTestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBTestItem(BloodBTestItem entity);

        [ServiceContractDescription(Name = "修改检验项目表 指定的属性", Desc = "修改检验项目表 指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBTestItemByField", Get = "", Post = "BloodBTestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBTestItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBTestItemByField(BloodBTestItem entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除检验项目表 ", Desc = "删除检验项目表 ", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBTestItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBTestItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBTestItem(long id);

        [ServiceContractDescription(Name = "查询检验项目表 ", Desc = "查询检验项目表 ", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBTestItem", Get = "", Post = "BloodBTestItem", Return = "BaseResultList<BloodBTestItem>", ReturnType = "ListBloodBTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBTestItem(BloodBTestItem entity);

        [ServiceContractDescription(Name = "查询检验项目表 (HQL)", Desc = "查询检验项目表 (HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBTestItem>", ReturnType = "ListBloodBTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBTestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询检验项目表 ", Desc = "通过主键ID查询检验项目表 ", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBTestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBTestItem>", ReturnType = "BloodBTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBTestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBTestItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBUnit

        [ServiceContractDescription(Name = "新增检验项目单位表", Desc = "新增检验项目单位表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBUnit", Get = "", Post = "BloodBUnit", Return = "BaseResultDataValue", ReturnType = "BloodBUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBUnit(BloodBUnit entity);

        [ServiceContractDescription(Name = "修改检验项目单位表", Desc = "修改检验项目单位表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBUnit", Get = "", Post = "BloodBUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBUnit(BloodBUnit entity);

        [ServiceContractDescription(Name = "修改检验项目单位表指定的属性", Desc = "修改检验项目单位表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBUnitByField", Get = "", Post = "BloodBUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBUnitByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBUnitByField(BloodBUnit entity, string fields);

        [ServiceContractDescription(Name = "删除检验项目单位表", Desc = "删除检验项目单位表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBUnit?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBUnit?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBUnit(long id);

        [ServiceContractDescription(Name = "查询检验项目单位表", Desc = "查询检验项目单位表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBUnit", Get = "", Post = "BloodBUnit", Return = "BaseResultList<BloodBUnit>", ReturnType = "ListBloodBUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBUnit(BloodBUnit entity);

        [ServiceContractDescription(Name = "查询检验项目单位表(HQL)", Desc = "查询检验项目单位表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBUnit>", ReturnType = "ListBloodBUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBUnitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询检验项目单位表", Desc = "通过主键ID查询检验项目单位表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBUnit>", ReturnType = "BloodBUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBUnitById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodChargeGroupItem

        [ServiceContractDescription(Name = "新增收费组合项目关系表", Desc = "新增收费组合项目关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodChargeGroupItem", Get = "", Post = "BloodChargeGroupItem", Return = "BaseResultDataValue", ReturnType = "BloodChargeGroupItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodChargeGroupItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodChargeGroupItem(BloodChargeGroupItem entity);

        [ServiceContractDescription(Name = "修改收费组合项目关系表", Desc = "修改收费组合项目关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeGroupItem", Get = "", Post = "BloodChargeGroupItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodChargeGroupItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodChargeGroupItem(BloodChargeGroupItem entity);

        [ServiceContractDescription(Name = "修改收费组合项目关系表指定的属性", Desc = "修改收费组合项目关系表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeGroupItemByField", Get = "", Post = "BloodChargeGroupItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodChargeGroupItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodChargeGroupItemByField(BloodChargeGroupItem entity, string fields);

        [ServiceContractDescription(Name = "删除收费组合项目关系表", Desc = "删除收费组合项目关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodChargeGroupItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodChargeGroupItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodChargeGroupItem(long id);

        [ServiceContractDescription(Name = "查询收费组合项目关系表", Desc = "查询收费组合项目关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeGroupItem", Get = "", Post = "BloodChargeGroupItem", Return = "BaseResultList<BloodChargeGroupItem>", ReturnType = "ListBloodChargeGroupItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeGroupItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeGroupItem(BloodChargeGroupItem entity);

        [ServiceContractDescription(Name = "查询收费组合项目关系表(HQL)", Desc = "查询收费组合项目关系表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeGroupItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodChargeGroupItem>", ReturnType = "ListBloodChargeGroupItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeGroupItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeGroupItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询收费组合项目关系表", Desc = "通过主键ID查询收费组合项目关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeGroupItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodChargeGroupItem>", ReturnType = "BloodChargeGroupItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeGroupItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeGroupItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodChargeItem

        [ServiceContractDescription(Name = "新增费用项目表", Desc = "新增费用项目表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodChargeItem", Get = "", Post = "BloodChargeItem", Return = "BaseResultDataValue", ReturnType = "BloodChargeItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodChargeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodChargeItem(BloodChargeItem entity);

        [ServiceContractDescription(Name = "修改费用项目表", Desc = "修改费用项目表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeItem", Get = "", Post = "BloodChargeItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodChargeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodChargeItem(BloodChargeItem entity);

        [ServiceContractDescription(Name = "修改费用项目表指定的属性", Desc = "修改费用项目表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeItemByField", Get = "", Post = "BloodChargeItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodChargeItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodChargeItemByField(BloodChargeItem entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除费用项目表", Desc = "删除费用项目表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodChargeItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodChargeItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodChargeItem(long id);

        [ServiceContractDescription(Name = "查询费用项目表", Desc = "查询费用项目表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItem", Get = "", Post = "BloodChargeItem", Return = "BaseResultList<BloodChargeItem>", ReturnType = "ListBloodChargeItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeItem(BloodChargeItem entity);

        [ServiceContractDescription(Name = "查询费用项目表(HQL)", Desc = "查询费用项目表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodChargeItem>", ReturnType = "ListBloodChargeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询费用项目表", Desc = "通过主键ID查询费用项目表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodChargeItem>", ReturnType = "BloodChargeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeItemById(long id, string fields, bool isPlanish);

        #endregion

        #region BloodChargeItemLink

        [ServiceContractDescription(Name = "新增组合项目费用关系表", Desc = "新增组合项目费用关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodChargeItemLink", Get = "", Post = "BloodChargeItemLink", Return = "BaseResultDataValue", ReturnType = "BloodChargeItemLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodChargeItemLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodChargeItemLink(BloodChargeItemLink entity);

        [ServiceContractDescription(Name = "修改组合项目费用关系表", Desc = "修改组合项目费用关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeItemLink", Get = "", Post = "BloodChargeItemLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodChargeItemLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodChargeItemLink(BloodChargeItemLink entity);

        [ServiceContractDescription(Name = "修改组合项目费用关系表指定的属性", Desc = "修改组合项目费用关系表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeItemLinkByField", Get = "", Post = "BloodChargeItemLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodChargeItemLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodChargeItemLinkByField(BloodChargeItemLink entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除组合项目费用关系表", Desc = "删除组合项目费用关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodChargeItemLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodChargeItemLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodChargeItemLink(long id);

        [ServiceContractDescription(Name = "查询组合项目费用关系表", Desc = "查询组合项目费用关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemLink", Get = "", Post = "BloodChargeItemLink", Return = "BaseResultList<BloodChargeItemLink>", ReturnType = "ListBloodChargeItemLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeItemLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeItemLink(BloodChargeItemLink entity);

        [ServiceContractDescription(Name = "查询组合项目费用关系表(HQL)", Desc = "查询组合项目费用关系表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodChargeItemLink>", ReturnType = "ListBloodChargeItemLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeItemLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeItemLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询组合项目费用关系表", Desc = "通过主键ID查询组合项目费用关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodChargeItemLink>", ReturnType = "BloodChargeItemLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeItemLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeItemLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodChargeItemType

        [ServiceContractDescription(Name = "新增费用项目类型表 对收费项目进行分类描述", Desc = "新增费用项目类型表 对收费项目进行分类描述", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodChargeItemType", Get = "", Post = "BloodChargeItemType", Return = "BaseResultDataValue", ReturnType = "BloodChargeItemType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodChargeItemType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodChargeItemType(BloodChargeItemType entity);

        [ServiceContractDescription(Name = "修改费用项目类型表 对收费项目进行分类描述", Desc = "修改费用项目类型表 对收费项目进行分类描述", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeItemType", Get = "", Post = "BloodChargeItemType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodChargeItemType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodChargeItemType(BloodChargeItemType entity);

        [ServiceContractDescription(Name = "修改费用项目类型表 对收费项目进行分类描述指定的属性", Desc = "修改费用项目类型表 对收费项目进行分类描述指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeItemTypeByField", Get = "", Post = "BloodChargeItemType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodChargeItemTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodChargeItemTypeByField(BloodChargeItemType entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除费用项目类型表 对收费项目进行分类描述", Desc = "删除费用项目类型表 对收费项目进行分类描述", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodChargeItemType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodChargeItemType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodChargeItemType(long id);

        [ServiceContractDescription(Name = "查询费用项目类型表 对收费项目进行分类描述", Desc = "查询费用项目类型表 对收费项目进行分类描述", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemType", Get = "", Post = "BloodChargeItemType", Return = "BaseResultList<BloodChargeItemType>", ReturnType = "ListBloodChargeItemType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeItemType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeItemType(BloodChargeItemType entity);

        [ServiceContractDescription(Name = "查询费用项目类型表 对收费项目进行分类描述(HQL)", Desc = "查询费用项目类型表 对收费项目进行分类描述(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodChargeItemType>", ReturnType = "ListBloodChargeItemType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeItemTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeItemTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询费用项目类型表 对收费项目进行分类描述", Desc = "通过主键ID查询费用项目类型表 对收费项目进行分类描述", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeItemTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodChargeItemType>", ReturnType = "BloodChargeItemType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeItemTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeItemTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodChargeMoney

        [ServiceContractDescription(Name = "新增收费管理", Desc = "新增收费管理", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodChargeMoney", Get = "", Post = "BloodChargeMoney", Return = "BaseResultDataValue", ReturnType = "BloodChargeMoney")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodChargeMoney", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodChargeMoney(BloodChargeMoney entity);

        [ServiceContractDescription(Name = "修改收费管理", Desc = "修改收费管理", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeMoney", Get = "", Post = "BloodChargeMoney", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodChargeMoney", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodChargeMoney(BloodChargeMoney entity);

        [ServiceContractDescription(Name = "修改收费管理指定的属性", Desc = "修改收费管理指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodChargeMoneyByField", Get = "", Post = "BloodChargeMoney", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodChargeMoneyByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodChargeMoneyByField(BloodChargeMoney entity, string fields);

        [ServiceContractDescription(Name = "删除收费管理", Desc = "删除收费管理", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodChargeMoney?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodChargeMoney?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodChargeMoney(long id);

        [ServiceContractDescription(Name = "查询收费管理", Desc = "查询收费管理", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeMoney", Get = "", Post = "BloodChargeMoney", Return = "BaseResultList<BloodChargeMoney>", ReturnType = "ListBloodChargeMoney")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeMoney", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeMoney(BloodChargeMoney entity);

        [ServiceContractDescription(Name = "查询收费管理(HQL)", Desc = "查询收费管理(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeMoneyByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodChargeMoney>", ReturnType = "ListBloodChargeMoney")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeMoneyByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeMoneyByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询收费管理", Desc = "通过主键ID查询收费管理", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodChargeMoneyById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodChargeMoney>", ReturnType = "BloodChargeMoney")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeMoneyById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeMoneyById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodClass

        [ServiceContractDescription(Name = "新增血袋分类", Desc = "新增血袋分类", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodClass", Get = "", Post = "BloodClass", Return = "BaseResultDataValue", ReturnType = "BloodClass")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodClass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodClass(BloodClass entity);

        [ServiceContractDescription(Name = "修改血袋分类", Desc = "修改血袋分类", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodClass", Get = "", Post = "BloodClass", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodClass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodClass(BloodClass entity);

        [ServiceContractDescription(Name = "修改血袋分类指定的属性", Desc = "修改血袋分类指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodClassByField", Get = "", Post = "BloodClass", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodClassByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodClassByField(BloodClass entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除血袋分类", Desc = "删除血袋分类", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodClass?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodClass?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodClass(long id);

        [ServiceContractDescription(Name = "查询血袋分类", Desc = "查询血袋分类", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClass", Get = "", Post = "BloodClass", Return = "BaseResultList<BloodClass>", ReturnType = "ListBloodClass")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodClass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodClass(BloodClass entity);

        [ServiceContractDescription(Name = "查询血袋分类(HQL)", Desc = "查询血袋分类(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClassByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodClass>", ReturnType = "ListBloodClass")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodClassByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodClassByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询血袋分类", Desc = "通过主键ID查询血袋分类", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClassById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodClass>", ReturnType = "BloodClass")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodClassById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodClassById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodInChecked

        [ServiceContractDescription(Name = "新增盘点主单表", Desc = "新增盘点主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodInChecked", Get = "", Post = "BloodInChecked", Return = "BaseResultDataValue", ReturnType = "BloodInChecked")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodInChecked", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodInChecked(BloodInChecked entity);

        [ServiceContractDescription(Name = "修改盘点主单表", Desc = "修改盘点主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodInChecked", Get = "", Post = "BloodInChecked", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodInChecked", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodInChecked(BloodInChecked entity);

        [ServiceContractDescription(Name = "修改盘点主单表指定的属性", Desc = "修改盘点主单表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodInCheckedByField", Get = "", Post = "BloodInChecked", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodInCheckedByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodInCheckedByField(BloodInChecked entity, string fields);

        [ServiceContractDescription(Name = "删除盘点主单表", Desc = "删除盘点主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodInChecked?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodInChecked?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodInChecked(long id);

        [ServiceContractDescription(Name = "查询盘点主单表", Desc = "查询盘点主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInChecked", Get = "", Post = "BloodInChecked", Return = "BaseResultList<BloodInChecked>", ReturnType = "ListBloodInChecked")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInChecked", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInChecked(BloodInChecked entity);

        [ServiceContractDescription(Name = "查询盘点主单表(HQL)", Desc = "查询盘点主单表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInCheckedByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodInChecked>", ReturnType = "ListBloodInChecked")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInCheckedByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInCheckedByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询盘点主单表", Desc = "通过主键ID查询盘点主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInCheckedById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodInChecked>", ReturnType = "BloodInChecked")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInCheckedById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInCheckedById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodInCheckedCurrent

        [ServiceContractDescription(Name = "新增当前库存表", Desc = "新增当前库存表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodInCheckedCurrent", Get = "", Post = "BloodInCheckedCurrent", Return = "BaseResultDataValue", ReturnType = "BloodInCheckedCurrent")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodInCheckedCurrent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodInCheckedCurrent(BloodInCheckedCurrent entity);

        [ServiceContractDescription(Name = "修改当前库存表", Desc = "修改当前库存表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodInCheckedCurrent", Get = "", Post = "BloodInCheckedCurrent", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodInCheckedCurrent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodInCheckedCurrent(BloodInCheckedCurrent entity);

        [ServiceContractDescription(Name = "修改当前库存表指定的属性", Desc = "修改当前库存表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodInCheckedCurrentByField", Get = "", Post = "BloodInCheckedCurrent", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodInCheckedCurrentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodInCheckedCurrentByField(BloodInCheckedCurrent entity, string fields);

        [ServiceContractDescription(Name = "删除当前库存表", Desc = "删除当前库存表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodInCheckedCurrent?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodInCheckedCurrent?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodInCheckedCurrent(long id);

        [ServiceContractDescription(Name = "查询当前库存表", Desc = "查询当前库存表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInCheckedCurrent", Get = "", Post = "BloodInCheckedCurrent", Return = "BaseResultList<BloodInCheckedCurrent>", ReturnType = "ListBloodInCheckedCurrent")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInCheckedCurrent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInCheckedCurrent(BloodInCheckedCurrent entity);

        [ServiceContractDescription(Name = "查询当前库存表(HQL)", Desc = "查询当前库存表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInCheckedCurrentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodInCheckedCurrent>", ReturnType = "ListBloodInCheckedCurrent")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInCheckedCurrentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInCheckedCurrentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询当前库存表", Desc = "通过主键ID查询当前库存表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInCheckedCurrentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodInCheckedCurrent>", ReturnType = "BloodInCheckedCurrent")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInCheckedCurrentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInCheckedCurrentById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodInCheckedItem

        [ServiceContractDescription(Name = "新增盘点明细表", Desc = "新增盘点明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodInCheckedItem", Get = "", Post = "BloodInCheckedItem", Return = "BaseResultDataValue", ReturnType = "BloodInCheckedItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodInCheckedItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodInCheckedItem(BloodInCheckedItem entity);

        [ServiceContractDescription(Name = "修改盘点明细表", Desc = "修改盘点明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodInCheckedItem", Get = "", Post = "BloodInCheckedItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodInCheckedItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodInCheckedItem(BloodInCheckedItem entity);

        [ServiceContractDescription(Name = "修改盘点明细表指定的属性", Desc = "修改盘点明细表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodInCheckedItemByField", Get = "", Post = "BloodInCheckedItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodInCheckedItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodInCheckedItemByField(BloodInCheckedItem entity, string fields);

        [ServiceContractDescription(Name = "删除盘点明细表", Desc = "删除盘点明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodInCheckedItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodInCheckedItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodInCheckedItem(long id);

        [ServiceContractDescription(Name = "查询盘点明细表", Desc = "查询盘点明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInCheckedItem", Get = "", Post = "BloodInCheckedItem", Return = "BaseResultList<BloodInCheckedItem>", ReturnType = "ListBloodInCheckedItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInCheckedItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInCheckedItem(BloodInCheckedItem entity);

        [ServiceContractDescription(Name = "查询盘点明细表(HQL)", Desc = "查询盘点明细表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInCheckedItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodInCheckedItem>", ReturnType = "ListBloodInCheckedItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInCheckedItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInCheckedItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询盘点明细表", Desc = "通过主键ID查询盘点明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInCheckedItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodInCheckedItem>", ReturnType = "BloodInCheckedItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInCheckedItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInCheckedItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodInterfaceTransport

        [ServiceContractDescription(Name = "新增接口传输记录表", Desc = "新增接口传输记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodInterfaceTransport", Get = "", Post = "BloodInterfaceTransport", Return = "BaseResultDataValue", ReturnType = "BloodInterfaceTransport")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodInterfaceTransport", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodInterfaceTransport(BloodInterfaceTransport entity);

        [ServiceContractDescription(Name = "修改接口传输记录表", Desc = "修改接口传输记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodInterfaceTransport", Get = "", Post = "BloodInterfaceTransport", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodInterfaceTransport", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodInterfaceTransport(BloodInterfaceTransport entity);

        [ServiceContractDescription(Name = "修改接口传输记录表指定的属性", Desc = "修改接口传输记录表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodInterfaceTransportByField", Get = "", Post = "BloodInterfaceTransport", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodInterfaceTransportByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodInterfaceTransportByField(BloodInterfaceTransport entity, string fields);

        [ServiceContractDescription(Name = "删除接口传输记录表", Desc = "删除接口传输记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodInterfaceTransport?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodInterfaceTransport?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodInterfaceTransport(long id);

        [ServiceContractDescription(Name = "查询接口传输记录表", Desc = "查询接口传输记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInterfaceTransport", Get = "", Post = "BloodInterfaceTransport", Return = "BaseResultList<BloodInterfaceTransport>", ReturnType = "ListBloodInterfaceTransport")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInterfaceTransport", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInterfaceTransport(BloodInterfaceTransport entity);

        [ServiceContractDescription(Name = "查询接口传输记录表(HQL)", Desc = "查询接口传输记录表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInterfaceTransportByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodInterfaceTransport>", ReturnType = "ListBloodInterfaceTransport")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInterfaceTransportByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInterfaceTransportByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询接口传输记录表", Desc = "通过主键ID查询接口传输记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInterfaceTransportById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodInterfaceTransport>", ReturnType = "BloodInterfaceTransport")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInterfaceTransportById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInterfaceTransportById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodLargeUseItem

        [ServiceContractDescription(Name = "新增大量用血申请记录表", Desc = "新增大量用血申请记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodLargeUseItem", Get = "", Post = "BloodLargeUseItem", Return = "BaseResultDataValue", ReturnType = "BloodLargeUseItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodLargeUseItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodLargeUseItem(BloodLargeUseItem entity);

        [ServiceContractDescription(Name = "修改大量用血申请记录表", Desc = "修改大量用血申请记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodLargeUseItem", Get = "", Post = "BloodLargeUseItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodLargeUseItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodLargeUseItem(BloodLargeUseItem entity);

        [ServiceContractDescription(Name = "修改大量用血申请记录表指定的属性", Desc = "修改大量用血申请记录表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodLargeUseItemByField", Get = "", Post = "BloodLargeUseItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodLargeUseItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodLargeUseItemByField(BloodLargeUseItem entity, string fields);

        [ServiceContractDescription(Name = "删除大量用血申请记录表", Desc = "删除大量用血申请记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodLargeUseItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodLargeUseItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodLargeUseItem(long id);

        [ServiceContractDescription(Name = "查询大量用血申请记录表", Desc = "查询大量用血申请记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodLargeUseItem", Get = "", Post = "BloodLargeUseItem", Return = "BaseResultList<BloodLargeUseItem>", ReturnType = "ListBloodLargeUseItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodLargeUseItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodLargeUseItem(BloodLargeUseItem entity);

        [ServiceContractDescription(Name = "查询大量用血申请记录表(HQL)", Desc = "查询大量用血申请记录表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodLargeUseItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodLargeUseItem>", ReturnType = "ListBloodLargeUseItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodLargeUseItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodLargeUseItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询大量用血申请记录表", Desc = "通过主键ID查询大量用血申请记录表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodLargeUseItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodLargeUseItem>", ReturnType = "BloodLargeUseItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodLargeUseItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodLargeUseItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodPatinfo

        [ServiceContractDescription(Name = "新增病人就诊记录信息表", Desc = "新增病人就诊记录信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodPatinfo", Get = "", Post = "BloodPatinfo", Return = "BaseResultDataValue", ReturnType = "BloodPatinfo")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodPatinfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodPatinfo(BloodPatinfo entity);

        [ServiceContractDescription(Name = "修改病人就诊记录信息表", Desc = "修改病人就诊记录信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodPatinfo", Get = "", Post = "BloodPatinfo", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodPatinfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodPatinfo(BloodPatinfo entity);

        [ServiceContractDescription(Name = "修改病人就诊记录信息表指定的属性", Desc = "修改病人就诊记录信息表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodPatinfoByField", Get = "", Post = "BloodPatinfo", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodPatinfoByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodPatinfoByField(BloodPatinfo entity, string fields);

        [ServiceContractDescription(Name = "删除病人就诊记录信息表", Desc = "删除病人就诊记录信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodPatinfo?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodPatinfo?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodPatinfo(long id);

        [ServiceContractDescription(Name = "查询病人就诊记录信息表", Desc = "查询病人就诊记录信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodPatinfo", Get = "", Post = "BloodPatinfo", Return = "BaseResultList<BloodPatinfo>", ReturnType = "ListBloodPatinfo")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodPatinfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodPatinfo(BloodPatinfo entity);

        [ServiceContractDescription(Name = "查询病人就诊记录信息表(HQL)", Desc = "查询病人就诊记录信息表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodPatinfoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodPatinfo>", ReturnType = "ListBloodPatinfo")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodPatinfoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodPatinfoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询病人就诊记录信息表", Desc = "通过主键ID查询病人就诊记录信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodPatinfoById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodPatinfo>", ReturnType = "BloodPatinfo")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodPatinfoById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodPatinfoById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodQtyDtl

        [ServiceContractDescription(Name = "新增库存表", Desc = "新增库存表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodQtyDtl", Get = "", Post = "BloodQtyDtl", Return = "BaseResultDataValue", ReturnType = "BloodQtyDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodQtyDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodQtyDtl(BloodQtyDtl entity);

        [ServiceContractDescription(Name = "修改库存表", Desc = "修改库存表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodQtyDtl", Get = "", Post = "BloodQtyDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodQtyDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodQtyDtl(BloodQtyDtl entity);

        [ServiceContractDescription(Name = "修改库存表指定的属性", Desc = "修改库存表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodQtyDtlByField", Get = "", Post = "BloodQtyDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodQtyDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodQtyDtlByField(BloodQtyDtl entity, string fields);

        [ServiceContractDescription(Name = "删除库存表", Desc = "删除库存表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodQtyDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodQtyDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodQtyDtl(long id);

        [ServiceContractDescription(Name = "查询库存表", Desc = "查询库存表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodQtyDtl", Get = "", Post = "BloodQtyDtl", Return = "BaseResultList<BloodQtyDtl>", ReturnType = "ListBloodQtyDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodQtyDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodQtyDtl(BloodQtyDtl entity);

        [ServiceContractDescription(Name = "查询库存表(HQL)", Desc = "查询库存表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodQtyDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodQtyDtl>", ReturnType = "ListBloodQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodQtyDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodQtyDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询库存表", Desc = "通过主键ID查询库存表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodQtyDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodQtyDtl>", ReturnType = "BloodQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodQtyDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodQtyDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodRecei

        [ServiceContractDescription(Name = "新增送达主单", Desc = "新增送达主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodRecei", Get = "", Post = "BloodRecei", Return = "BaseResultDataValue", ReturnType = "BloodRecei")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodRecei", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodRecei(BloodRecei entity);

        [ServiceContractDescription(Name = "修改送达主单", Desc = "修改送达主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodRecei", Get = "", Post = "BloodRecei", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodRecei", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodRecei(BloodRecei entity);

        [ServiceContractDescription(Name = "修改送达主单指定的属性", Desc = "修改送达主单指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodReceiByField", Get = "", Post = "BloodRecei", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodReceiByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodReceiByField(BloodRecei entity, string fields);

        [ServiceContractDescription(Name = "删除送达主单", Desc = "删除送达主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodRecei?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodRecei?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodRecei(long id);

        [ServiceContractDescription(Name = "查询送达主单", Desc = "查询送达主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodRecei", Get = "", Post = "BloodRecei", Return = "BaseResultList<BloodRecei>", ReturnType = "ListBloodRecei")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodRecei", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodRecei(BloodRecei entity);

        [ServiceContractDescription(Name = "查询送达主单(HQL)", Desc = "查询送达主单(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReceiByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodRecei>", ReturnType = "ListBloodRecei")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReceiByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReceiByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询送达主单", Desc = "通过主键ID查询送达主单", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReceiById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodRecei>", ReturnType = "BloodRecei")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReceiById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReceiById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodReceiItem

        [ServiceContractDescription(Name = "新增送达明细", Desc = "新增送达明细", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodReceiItem", Get = "", Post = "BloodReceiItem", Return = "BaseResultDataValue", ReturnType = "BloodReceiItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodReceiItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodReceiItem(BloodReceiItem entity);

        [ServiceContractDescription(Name = "修改送达明细", Desc = "修改送达明细", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodReceiItem", Get = "", Post = "BloodReceiItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodReceiItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodReceiItem(BloodReceiItem entity);

        [ServiceContractDescription(Name = "修改送达明细指定的属性", Desc = "修改送达明细指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodReceiItemByField", Get = "", Post = "BloodReceiItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodReceiItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodReceiItemByField(BloodReceiItem entity, string fields);

        [ServiceContractDescription(Name = "删除送达明细", Desc = "删除送达明细", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodReceiItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodReceiItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodReceiItem(long id);

        [ServiceContractDescription(Name = "查询送达明细", Desc = "查询送达明细", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReceiItem", Get = "", Post = "BloodReceiItem", Return = "BaseResultList<BloodReceiItem>", ReturnType = "ListBloodReceiItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReceiItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReceiItem(BloodReceiItem entity);

        [ServiceContractDescription(Name = "查询送达明细(HQL)", Desc = "查询送达明细(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReceiItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodReceiItem>", ReturnType = "ListBloodReceiItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReceiItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReceiItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询送达明细", Desc = "通过主键ID查询送达明细", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReceiItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodReceiItem>", ReturnType = "BloodReceiItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReceiItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReceiItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodSelfBlood

        [ServiceContractDescription(Name = "新增自体输血血袋登记信息表", Desc = "新增自体输血血袋登记信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodSelfBlood", Get = "", Post = "BloodSelfBlood", Return = "BaseResultDataValue", ReturnType = "BloodSelfBlood")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodSelfBlood", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodSelfBlood(BloodSelfBlood entity);

        [ServiceContractDescription(Name = "修改自体输血血袋登记信息表", Desc = "修改自体输血血袋登记信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodSelfBlood", Get = "", Post = "BloodSelfBlood", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodSelfBlood", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodSelfBlood(BloodSelfBlood entity);

        [ServiceContractDescription(Name = "修改自体输血血袋登记信息表指定的属性", Desc = "修改自体输血血袋登记信息表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodSelfBloodByField", Get = "", Post = "BloodSelfBlood", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodSelfBloodByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodSelfBloodByField(BloodSelfBlood entity, string fields);

        [ServiceContractDescription(Name = "删除自体输血血袋登记信息表", Desc = "删除自体输血血袋登记信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodSelfBlood?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodSelfBlood?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodSelfBlood(long id);

        [ServiceContractDescription(Name = "查询自体输血血袋登记信息表", Desc = "查询自体输血血袋登记信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodSelfBlood", Get = "", Post = "BloodSelfBlood", Return = "BaseResultList<BloodSelfBlood>", ReturnType = "ListBloodSelfBlood")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodSelfBlood", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodSelfBlood(BloodSelfBlood entity);

        [ServiceContractDescription(Name = "查询自体输血血袋登记信息表(HQL)", Desc = "查询自体输血血袋登记信息表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodSelfBloodByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodSelfBlood>", ReturnType = "ListBloodSelfBlood")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodSelfBloodByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodSelfBloodByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询自体输血血袋登记信息表", Desc = "通过主键ID查询自体输血血袋登记信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodSelfBloodById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodSelfBlood>", ReturnType = "BloodSelfBlood")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodSelfBloodById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodSelfBloodById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodSetQtyAlertColor

        [ServiceContractDescription(Name = "新增库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分", Desc = "新增库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodSetQtyAlertColor", Get = "", Post = "BloodSetQtyAlertColor", Return = "BaseResultDataValue", ReturnType = "BloodSetQtyAlertColor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodSetQtyAlertColor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodSetQtyAlertColor(BloodSetQtyAlertColor entity);

        [ServiceContractDescription(Name = "修改库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分", Desc = "修改库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodSetQtyAlertColor", Get = "", Post = "BloodSetQtyAlertColor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodSetQtyAlertColor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodSetQtyAlertColor(BloodSetQtyAlertColor entity);

        [ServiceContractDescription(Name = "修改库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分指定的属性", Desc = "修改库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodSetQtyAlertColorByField", Get = "", Post = "BloodSetQtyAlertColor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodSetQtyAlertColorByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodSetQtyAlertColorByField(BloodSetQtyAlertColor entity, string fields);

        [ServiceContractDescription(Name = "删除库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分", Desc = "删除库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodSetQtyAlertColor?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodSetQtyAlertColor?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodSetQtyAlertColor(long id);

        [ServiceContractDescription(Name = "查询库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分", Desc = "查询库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodSetQtyAlertColor", Get = "", Post = "BloodSetQtyAlertColor", Return = "BaseResultList<BloodSetQtyAlertColor>", ReturnType = "ListBloodSetQtyAlertColor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodSetQtyAlertColor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodSetQtyAlertColor(BloodSetQtyAlertColor entity);

        [ServiceContractDescription(Name = "查询库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分(HQL)", Desc = "查询库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodSetQtyAlertColorByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodSetQtyAlertColor>", ReturnType = "ListBloodSetQtyAlertColor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodSetQtyAlertColorByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodSetQtyAlertColorByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分", Desc = "通过主键ID查询库存预警信息主表 预警信息按预警范围段预设颜色UI显示区分", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodSetQtyAlertColorById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodSetQtyAlertColor>", ReturnType = "BloodSetQtyAlertColor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodSetQtyAlertColorById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodSetQtyAlertColorById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodSetQtyAlertInfo

        [ServiceContractDescription(Name = "新增库存预警明细信息表", Desc = "新增库存预警明细信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodSetQtyAlertInfo", Get = "", Post = "BloodSetQtyAlertInfo", Return = "BaseResultDataValue", ReturnType = "BloodSetQtyAlertInfo")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodSetQtyAlertInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodSetQtyAlertInfo(BloodSetQtyAlertInfo entity);

        [ServiceContractDescription(Name = "修改库存预警明细信息表", Desc = "修改库存预警明细信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodSetQtyAlertInfo", Get = "", Post = "BloodSetQtyAlertInfo", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodSetQtyAlertInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodSetQtyAlertInfo(BloodSetQtyAlertInfo entity);

        [ServiceContractDescription(Name = "修改库存预警明细信息表指定的属性", Desc = "修改库存预警明细信息表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodSetQtyAlertInfoByField", Get = "", Post = "BloodSetQtyAlertInfo", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodSetQtyAlertInfoByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodSetQtyAlertInfoByField(BloodSetQtyAlertInfo entity, string fields);

        [ServiceContractDescription(Name = "删除库存预警明细信息表", Desc = "删除库存预警明细信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodSetQtyAlertInfo?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodSetQtyAlertInfo?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodSetQtyAlertInfo(long id);

        [ServiceContractDescription(Name = "查询库存预警明细信息表", Desc = "查询库存预警明细信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodSetQtyAlertInfo", Get = "", Post = "BloodSetQtyAlertInfo", Return = "BaseResultList<BloodSetQtyAlertInfo>", ReturnType = "ListBloodSetQtyAlertInfo")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodSetQtyAlertInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodSetQtyAlertInfo(BloodSetQtyAlertInfo entity);

        [ServiceContractDescription(Name = "查询库存预警明细信息表(HQL)", Desc = "查询库存预警明细信息表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodSetQtyAlertInfoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodSetQtyAlertInfo>", ReturnType = "ListBloodSetQtyAlertInfo")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodSetQtyAlertInfoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodSetQtyAlertInfoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询库存预警明细信息表", Desc = "通过主键ID查询库存预警明细信息表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodSetQtyAlertInfoById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodSetQtyAlertInfo>", ReturnType = "BloodSetQtyAlertInfo")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodSetQtyAlertInfoById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodSetQtyAlertInfoById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodStyle

        [ServiceContractDescription(Name = "新增血制品字典", Desc = "新增血制品字典", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodStyle", Get = "", Post = "BloodStyle", Return = "BaseResultDataValue", ReturnType = "BloodStyle")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodStyle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodStyle(BloodStyle entity);

        [ServiceContractDescription(Name = "修改血制品字典", Desc = "修改血制品字典", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodStyle", Get = "", Post = "BloodStyle", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodStyle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodStyle(BloodStyle entity);

        [ServiceContractDescription(Name = "修改血制品字典指定的属性", Desc = "修改血制品字典指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodStyleByField", Get = "", Post = "BloodStyle", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodStyleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodStyleByField(BloodStyle entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除血制品字典", Desc = "删除血制品字典", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodStyle?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodStyle?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodStyle(long id);

        [ServiceContractDescription(Name = "查询血制品字典", Desc = "查询血制品字典", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodStyle", Get = "", Post = "BloodStyle", Return = "BaseResultList<BloodStyle>", ReturnType = "ListBloodStyle")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodStyle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodStyle(BloodStyle entity);

        [ServiceContractDescription(Name = "查询血制品字典(HQL)", Desc = "查询血制品字典(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodStyleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodStyle>", ReturnType = "ListBloodStyle")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodStyleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodStyleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询血制品字典", Desc = "通过主键ID查询血制品字典", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodStyleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodStyle>", ReturnType = "BloodStyle")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodStyleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodStyleById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodTransForm

        [ServiceContractDescription(Name = "新增输血记录主单表", Desc = "新增输血记录主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodTransForm", Get = "", Post = "BloodTransForm", Return = "BaseResultDataValue", ReturnType = "BloodTransForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodTransForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodTransForm(BloodTransForm entity);

        [ServiceContractDescription(Name = "修改输血记录主单表", Desc = "修改输血记录主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransForm", Get = "", Post = "BloodTransForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransForm(BloodTransForm entity);

        [ServiceContractDescription(Name = "修改输血记录主单表指定的属性", Desc = "修改输血记录主单表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransFormByField", Get = "", Post = "BloodTransForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransFormByField(BloodTransForm entity, string fields);

        [ServiceContractDescription(Name = "删除输血记录主单表", Desc = "删除输血记录主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodTransForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodTransForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodTransForm(long id);

        [ServiceContractDescription(Name = "查询输血记录主单表", Desc = "查询输血记录主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransForm", Get = "", Post = "BloodTransForm", Return = "BaseResultList<BloodTransForm>", ReturnType = "ListBloodTransForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransForm(BloodTransForm entity);

        [ServiceContractDescription(Name = "查询输血记录主单表(HQL)", Desc = "查询输血记录主单表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransForm>", ReturnType = "ListBloodTransForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询输血记录主单表", Desc = "通过主键ID查询输血记录主单表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransForm>", ReturnType = "BloodTransForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransFormById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodTransItem

        [ServiceContractDescription(Name = "新增输血过程记录明细表", Desc = "新增输血过程记录明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodTransItem", Get = "", Post = "BloodTransItem", Return = "BaseResultDataValue", ReturnType = "BloodTransItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodTransItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodTransItem(BloodTransItem entity);

        [ServiceContractDescription(Name = "修改输血过程记录明细表", Desc = "修改输血过程记录明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransItem", Get = "", Post = "BloodTransItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransItem(BloodTransItem entity);

        [ServiceContractDescription(Name = "修改输血过程记录明细表指定的属性", Desc = "修改输血过程记录明细表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransItemByField", Get = "", Post = "BloodTransItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransItemByField(BloodTransItem entity, string fields);

        [ServiceContractDescription(Name = "删除输血过程记录明细表", Desc = "删除输血过程记录明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodTransItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodTransItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodTransItem(long id);

        [ServiceContractDescription(Name = "查询输血过程记录明细表", Desc = "查询输血过程记录明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransItem", Get = "", Post = "BloodTransItem", Return = "BaseResultList<BloodTransItem>", ReturnType = "ListBloodTransItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransItem(BloodTransItem entity);

        [ServiceContractDescription(Name = "查询输血过程记录明细表(HQL)", Desc = "查询输血过程记录明细表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransItem>", ReturnType = "ListBloodTransItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询输血过程记录明细表", Desc = "通过主键ID查询输血过程记录明细表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransItem>", ReturnType = "BloodTransItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodUnit

        [ServiceContractDescription(Name = "新增血制品单位", Desc = "新增血制品单位", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodUnit", Get = "", Post = "BloodUnit", Return = "BaseResultDataValue", ReturnType = "BloodUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodUnit(BloodUnit entity);

        [ServiceContractDescription(Name = "修改血制品单位", Desc = "修改血制品单位", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUnit", Get = "", Post = "BloodUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodUnit(BloodUnit entity);

        [ServiceContractDescription(Name = "修改血制品单位指定的属性", Desc = "修改血制品单位指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUnitByField", Get = "", Post = "BloodUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodUnitByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodUnitByField(BloodUnit entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除血制品单位", Desc = "删除血制品单位", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodUnit?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodUnit?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodUnit(long id);

        [ServiceContractDescription(Name = "查询血制品单位", Desc = "查询血制品单位", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUnit", Get = "", Post = "BloodUnit", Return = "BaseResultList<BloodUnit>", ReturnType = "ListBloodUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUnit(BloodUnit entity);

        [ServiceContractDescription(Name = "查询血制品单位(HQL)", Desc = "查询血制品单位(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodUnit>", ReturnType = "ListBloodUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUnitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询血制品单位", Desc = "通过主键ID查询血制品单位", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodUnit>", ReturnType = "BloodUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUnitById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodUseDesc

        [ServiceContractDescription(Name = "新增Blood_UseDesc", Desc = "新增Blood_UseDesc", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodUseDesc", Get = "", Post = "BloodUseDesc", Return = "BaseResultDataValue", ReturnType = "BloodUseDesc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodUseDesc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodUseDesc(BloodUseDesc entity);

        [ServiceContractDescription(Name = "修改Blood_UseDesc", Desc = "修改Blood_UseDesc", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUseDesc", Get = "", Post = "BloodUseDesc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodUseDesc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodUseDesc(BloodUseDesc entity);

        [ServiceContractDescription(Name = "修改Blood_UseDesc指定的属性", Desc = "修改Blood_UseDesc指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUseDescByField", Get = "", Post = "BloodUseDesc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodUseDescByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodUseDescByField(BloodUseDesc entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_UseDesc", Desc = "删除Blood_UseDesc", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodUseDesc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodUseDesc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodUseDesc(long id);

        [ServiceContractDescription(Name = "查询Blood_UseDesc", Desc = "查询Blood_UseDesc", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseDesc", Get = "", Post = "BloodUseDesc", Return = "BaseResultList<BloodUseDesc>", ReturnType = "ListBloodUseDesc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUseDesc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUseDesc(BloodUseDesc entity);

        [ServiceContractDescription(Name = "查询Blood_UseDesc(HQL)", Desc = "查询Blood_UseDesc(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseDescByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodUseDesc>", ReturnType = "ListBloodUseDesc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUseDescByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUseDescByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_UseDesc", Desc = "通过主键ID查询Blood_UseDesc", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseDescById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodUseDesc>", ReturnType = "BloodUseDesc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUseDescById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUseDescById(long id, string fields, bool isPlanish);
        #endregion

        #region SCBagOperation

        [ServiceContractDescription(Name = "新增血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站", Desc = "新增血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddSCBagOperation", Get = "", Post = "SCBagOperation", Return = "BaseResultDataValue", ReturnType = "SCBagOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddSCBagOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddSCBagOperation(SCBagOperation entity);

        [ServiceContractDescription(Name = "修改血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站", Desc = "修改血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateSCBagOperation", Get = "", Post = "SCBagOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateSCBagOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateSCBagOperation(SCBagOperation entity);

        [ServiceContractDescription(Name = "修改血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站指定的属性", Desc = "修改血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateSCBagOperationByField", Get = "", Post = "SCBagOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateSCBagOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateSCBagOperationByField(SCBagOperation entity, string fields);

        [ServiceContractDescription(Name = "删除血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站", Desc = "删除血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelSCBagOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelSCBagOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelSCBagOperation(long id);

        [ServiceContractDescription(Name = "查询血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站", Desc = "查询血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchSCBagOperation", Get = "", Post = "SCBagOperation", Return = "BaseResultList<SCBagOperation>", ReturnType = "ListSCBagOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchSCBagOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchSCBagOperation(SCBagOperation entity);

        [ServiceContractDescription(Name = "查询血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站(HQL)", Desc = "查询血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchSCBagOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCBagOperation>", ReturnType = "ListSCBagOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchSCBagOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchSCBagOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站", Desc = "通过主键ID查询血袋操作记录表 入库、复检、配血、发血、领用、接收、输注、回收、销毁、退库、回退血站", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchSCBagOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCBagOperation>", ReturnType = "SCBagOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchSCBagOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchSCBagOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBagChargeLink

        [ServiceContractDescription(Name = "新增血袋费用项目关系表(原Blood_Kind)", Desc = "新增血袋费用项目关系表(原Blood_Kind)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagChargeLink", Get = "", Post = "BloodBagChargeLink", Return = "BaseResultDataValue", ReturnType = "BloodBagChargeLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagChargeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagChargeLink(BloodBagChargeLink entity);

        [ServiceContractDescription(Name = "修改血袋费用项目关系表(原Blood_Kind)", Desc = "修改血袋费用项目关系表(原Blood_Kind)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagChargeLink", Get = "", Post = "BloodBagChargeLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagChargeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagChargeLink(BloodBagChargeLink entity);

        [ServiceContractDescription(Name = "修改血袋费用项目关系表(原Blood_Kind)指定的属性", Desc = "修改血袋费用项目关系表(原Blood_Kind)指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagChargeLinkByField", Get = "", Post = "BloodBagChargeLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagChargeLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagChargeLinkByField(BloodBagChargeLink entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除血袋费用项目关系表(原Blood_Kind)", Desc = "删除血袋费用项目关系表(原Blood_Kind)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagChargeLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagChargeLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagChargeLink(long id);

        [ServiceContractDescription(Name = "查询血袋费用项目关系表(原Blood_Kind)", Desc = "查询血袋费用项目关系表(原Blood_Kind)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagChargeLink", Get = "", Post = "BloodBagChargeLink", Return = "BaseResultList<BloodBagChargeLink>", ReturnType = "ListBloodBagChargeLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagChargeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagChargeLink(BloodBagChargeLink entity);

        [ServiceContractDescription(Name = "查询血袋费用项目关系表(原Blood_Kind)(HQL)", Desc = "查询血袋费用项目关系表(原Blood_Kind)(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagChargeLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagChargeLink>", ReturnType = "ListBloodBagChargeLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagChargeLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagChargeLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询血袋费用项目关系表(原Blood_Kind)", Desc = "通过主键ID查询血袋费用项目关系表(原Blood_Kind)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagChargeLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagChargeLink>", ReturnType = "BloodBagChargeLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagChargeLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagChargeLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodClassUnitLink

        [ServiceContractDescription(Name = "新增血制品分类的单位换算关系表", Desc = "新增血制品分类的单位换算关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodClassUnitLink", Get = "", Post = "BloodClassUnitLink", Return = "BaseResultDataValue", ReturnType = "BloodClassUnitLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodClassUnitLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodClassUnitLink(BloodClassUnitLink entity);

        [ServiceContractDescription(Name = "修改血制品分类的单位换算关系表", Desc = "修改血制品分类的单位换算关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodClassUnitLink", Get = "", Post = "BloodClassUnitLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodClassUnitLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodClassUnitLink(BloodClassUnitLink entity);

        [ServiceContractDescription(Name = "修改血制品分类的单位换算关系表指定的属性", Desc = "修改血制品分类的单位换算关系表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodClassUnitLinkByField", Get = "", Post = "BloodClassUnitLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodClassUnitLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodClassUnitLinkByField(BloodClassUnitLink entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除血制品分类的单位换算关系表", Desc = "删除血制品分类的单位换算关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodClassUnitLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodClassUnitLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodClassUnitLink(long id);

        [ServiceContractDescription(Name = "查询血制品分类的单位换算关系表", Desc = "查询血制品分类的单位换算关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClassUnitLink", Get = "", Post = "BloodClassUnitLink", Return = "BaseResultList<BloodClassUnitLink>", ReturnType = "ListBloodClassUnitLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodClassUnitLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodClassUnitLink(BloodClassUnitLink entity);

        [ServiceContractDescription(Name = "查询血制品分类的单位换算关系表(HQL)", Desc = "查询血制品分类的单位换算关系表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClassUnitLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodClassUnitLink>", ReturnType = "ListBloodClassUnitLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodClassUnitLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodClassUnitLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询血制品分类的单位换算关系表", Desc = "通过主键ID查询血制品分类的单位换算关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClassUnitLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodClassUnitLink>", ReturnType = "BloodClassUnitLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodClassUnitLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodClassUnitLinkById(long id, string fields, bool isPlanish);
        #endregion
       
        #region BloodLisResult

        [ServiceContractDescription(Name = "新增Lis相关检验结果表", Desc = "新增Lis相关检验结果表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodLisResult", Get = "", Post = "BloodLisResult", Return = "BaseResultDataValue", ReturnType = "BloodLisResult")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodLisResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodLisResult(BloodLisResult entity);

        [ServiceContractDescription(Name = "修改Lis相关检验结果表", Desc = "修改Lis相关检验结果表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodLisResult", Get = "", Post = "BloodLisResult", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodLisResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodLisResult(BloodLisResult entity);

        [ServiceContractDescription(Name = "修改Lis相关检验结果表指定的属性", Desc = "修改Lis相关检验结果表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodLisResultByField", Get = "", Post = "BloodLisResult", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodLisResultByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodLisResultByField(BloodLisResult entity, string fields);

        [ServiceContractDescription(Name = "删除Lis相关检验结果表", Desc = "删除Lis相关检验结果表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodLisResult?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodLisResult?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodLisResult(long id);

        [ServiceContractDescription(Name = "查询Lis相关检验结果表", Desc = "查询Lis相关检验结果表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodLisResult", Get = "", Post = "BloodLisResult", Return = "BaseResultList<BloodLisResult>", ReturnType = "ListBloodLisResult")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodLisResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodLisResult(BloodLisResult entity);

        [ServiceContractDescription(Name = "查询Lis相关检验结果表(HQL)", Desc = "查询Lis相关检验结果表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodLisResultByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodLisResult>", ReturnType = "ListBloodLisResult")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodLisResultByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodLisResultByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis相关检验结果表", Desc = "通过主键ID查询Lis相关检验结果表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodLisResultById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodLisResult>", ReturnType = "BloodLisResult")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodLisResultById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodLisResultById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodInterfaceMaping

        [ServiceContractDescription(Name = "新增接口映射(对照)关系表", Desc = "新增接口映射(对照)关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_AddBloodInterfaceMaping", Get = "", Post = "BloodInterfaceMaping", Return = "BaseResultDataValue", ReturnType = "BloodInterfaceMaping")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodInterfaceMaping", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodInterfaceMaping(BloodInterfaceMaping entity);

        [ServiceContractDescription(Name = "修改接口映射(对照)关系表", Desc = "修改接口映射(对照)关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodInterfaceMaping", Get = "", Post = "BloodInterfaceMaping", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodInterfaceMaping", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodInterfaceMaping(BloodInterfaceMaping entity);

        [ServiceContractDescription(Name = "修改接口映射(对照)关系表指定的属性", Desc = "修改接口映射(对照)关系表指定的属性", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodInterfaceMapingByField", Get = "", Post = "BloodInterfaceMaping", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodInterfaceMapingByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodInterfaceMapingByField(BloodInterfaceMaping entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除接口映射(对照)关系表", Desc = "删除接口映射(对照)关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_DelBloodInterfaceMaping?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodInterfaceMaping?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodInterfaceMaping(long id);

        [ServiceContractDescription(Name = "查询接口映射(对照)关系表", Desc = "查询接口映射(对照)关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInterfaceMaping", Get = "", Post = "BloodInterfaceMaping", Return = "BaseResultList<BloodInterfaceMaping>", ReturnType = "ListBloodInterfaceMaping")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInterfaceMaping", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInterfaceMaping(BloodInterfaceMaping entity);

        [ServiceContractDescription(Name = "查询接口映射(对照)关系表(HQL)", Desc = "查询接口映射(对照)关系表(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInterfaceMapingByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodInterfaceMaping>", ReturnType = "ListBloodInterfaceMaping")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInterfaceMapingByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInterfaceMapingByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询接口映射(对照)关系表", Desc = "通过主键ID查询接口映射(对照)关系表", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBloodInterfaceMapingById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodInterfaceMaping>", ReturnType = "BloodInterfaceMaping")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodInterfaceMapingById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodInterfaceMapingById(long id, string fields, bool isPlanish);

        #endregion

        #region 业务接口对照
        [ServiceContractDescription(Name = "查询业务接口对照关系信息(HQL)", Desc = "查询业务接口对照关系信息(HQL)", Url = "ServerWCF/BloodTransfusionManageService.svc/BT_UDTO_SearchBDictMapingVOByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&deveCode={deveCode}&useCode={useCode}&mapingWhere={mapingWhere}&objectTypeId={objectTypeId}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&deveCode={deveCode}&useCode={useCode}&mapingWhere={mapingWhere}&objectTypeId={objectTypeId}", Post = "", Return = "BaseResultList<BDictMapingVO>", ReturnType = "ListBDictMapingVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBDictMapingVOByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&deveCode={deveCode}&useCode={useCode}&mapingWhere={mapingWhere}&objectTypeId={objectTypeId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBDictMapingVOByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, string deveCode, string useCode, string mapingWhere, long objectTypeId);

        #endregion

        #region 双列表选择左查询定制服务
        [ServiceContractDescription(Name = "组合项目选择费用项目时,获取待选择的费用项目信息(HQL)", Desc = "组合项目选择费用项目时,获取待选择的费用项目信息(HQL)", Url = "ReaManageService.svc/BT_UDTO_SearchBloodChargeItemByChargeGItemHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodChargeItem>", ReturnType = "ListBloodChargeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeItemByChargeGItemHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeItemByChargeGItemHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "收费类型费用项目关系选择费用项目时,获取待选择的费用项目信息(HQL)", Desc = "收费类型费用项目关系选择费用项目时,获取待选择的费用项目信息(HQL)", Url = "ReaManageService.svc/BT_UDTO_SearchBloodChargeItemByChargeItemLinkHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodChargeItem>", ReturnType = "ListBloodChargeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodChargeItemByChargeItemLinkHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodChargeItemByChargeItemLinkHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "库存预警设置选择血制品时,获取待选择的血制品信息(HQL)", Desc = "库存预警设置选择血制品时,获取待选择的血制品信息(HQL)", Url = "ReaManageService.svc/BT_UDTO_SearchBloodStyleBySetQtyAlertInfoHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodStyle>", ReturnType = "ListBloodStyle")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodStyleBySetQtyAlertInfoHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodStyleBySetQtyAlertInfoHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "血制品分类与单位换算关系选择血制品单位时,获取待选择的血制品单位信息(HQL)", Desc = "血制品分类与单位换算关系选择血制品单位时,获取待选择的血制品单位信息(HQL)", Url = "ReaManageService.svc/BT_UDTO_SearchBloodUnitByClassUnitLinkHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodUnit>", ReturnType = "ListBloodUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUnitByClassUnitLinkHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUnitByClassUnitLinkHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish);

        #endregion

    }
}
