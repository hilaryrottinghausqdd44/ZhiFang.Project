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
        [ServiceContractDescription(Name = "HIS调用时,依传入HIS医生ID,获取到的医生信息(包含医生审核等级信息)", Desc = "HIS调用时,依传入HIS医生ID,获取到的医生信息(包含医生审核等级信息)", Url = "BloodTransfusionManageService.svc/BT_SYS_GetSysCurDoctorInfoByHisCode?hisDoctorCode={hisDoctorCode}&hisDeptCode={hisDeptCode}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_SYS_GetSysCurDoctorInfoByHisCode?hisDoctorCode={hisDoctorCode}&hisDeptCode={hisDeptCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_SYS_GetSysCurDoctorInfoByHisCode(string hisDeptCode, string hisDoctorCode);

        [ServiceContractDescription(Name = "BS依PUser登录帐号及密码,获取医生信息(包含医生审核等级信息)", Desc = "BS依PUser登录帐号及密码,获取医生信息(包含医生审核等级信息)", Url = "BloodTransfusionManageService.svc/BT_SYS_GetSysCurDoctorInfoByAccount?account={account}&pwd={pwd}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_SYS_GetSysCurDoctorInfoByAccount?account={account}&pwd={pwd}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_SYS_GetSysCurDoctorInfoByAccount(string account, string pwd);

        [ServiceContractDescription(Name = "按PUser的帐号及密码登录", Desc = "BS按PUser的帐号及密码登录", Url = "BloodTransfusionManageService.svc/BT_SYS_LoginOfPUser?strUserAccount={strUserAccount}&strPassWord={strPassWord}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_SYS_LoginOfPUser?strUserAccount={strUserAccount}&strPassWord={strPassWord}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_SYS_LoginOfPUser(string strUserAccount, string strPassWord);

        [ServiceContractDescription(Name = "his调用时验证及获取人员信息入口", Desc = "his调用时验证及获取人员信息入口", Url = "BloodTransfusionManageService.svc/BT_SYS_LoginOfPUserByHisCode?hisWardCode={hisWardCode}&hisDeptCode={hisDeptCode}&hisDoctorCode={hisDoctorCode}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_SYS_LoginOfPUserByHisCode?hisWardCode={hisWardCode}&hisDeptCode={hisDeptCode}&hisDoctorCode={hisDoctorCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_SYS_LoginOfPUserByHisCode(string hisWardCode, string hisDeptCode, string hisDoctorCode);

        [ServiceContractDescription(Name = "PUser用户注销服务", Desc = "PUser用户注销服务", Url = "BloodTransfusionManageService.svc/BT_SYS_LogoutOfPUser?strUserAccount={strUserAccount}", Get = "strUserAccount={strUserAccount}", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_SYS_LogoutOfPUser?strUserAccount={strUserAccount}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool BT_SYS_LogoutOfPUser(string strUserAccount);

        [ServiceContractDescription(Name = "获取用户信息后,手工调用数据库升级服务", Desc = "获取用户信息后,手工调用数据库升级服务", Url = "BloodTransfusionManageService.svc/BT_SYS_DBUpdate", Get = "", Post = "", Return = "bool", ReturnType = "Bool")]
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
        [ServiceContractDescription(Name = "从集成平台或PUser查询RBACUser(HQL)", Desc = "从集成平台或PUser查询RBACUser(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchRBACUserOfPUserByHQL?isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACUser>", ReturnType = "ListRBACUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchRBACUserOfPUserByHQL?isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchRBACUserOfPUserByHQL(bool isliip, int page, int limit, string fields, string where, string sort, bool isPlanish);

        /// <summary>
        /// 从集成平台或NPUser查询RBACUser(HQL)
        /// (1)如果是从集成平台获取,where和sort按集成平台的RBACUser封装;
        /// (2)如果是从NPUser获取,where和sort按NPUser封装;
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
        [ServiceContractDescription(Name = "从集成平台或NPUser查询RBACUser(HQL)", Desc = "从集成平台或PUser查询RBACUser(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchRBACUserOfNPUserByHQL?isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACUser>", ReturnType = "ListRBACUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchRBACUserOfNPUserByHQL?isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchRBACUserOfNPUserByHQL(bool isliip, int page, int limit, string fields, string where, string sort, bool isPlanish);

        /// <summary>
        /// (1)where和sort按PUser封装;
        /// (2)fields统一按RBACUser封装;
        /// </summary>
        /// <param name="fieldKey"></param>
        /// <param name="fieldVal"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "通过指定字段(如工号等)获取RBACUser(PUser转换)", Desc = "通过指定字段(如工号等)获取RBACUser(PUser转换)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchRBACUserByFieldKey?fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", Get = "fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACUser>", ReturnType = "RBACUser")]
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
        [ServiceContractDescription(Name = "通过指定字段(如工号等)获取PUser", Desc = "通过(如工号等)指定字段获取PUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchPUserByFieldKey?fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", Get = "fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchPUserByFieldKey?fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchPUserByFieldKey(string fieldKey, string fieldVal, string fields, bool isPlanish);

        #endregion

        #region 封装集成平台服务

        [ServiceContractDescription(Name = "获取智方集成平台的帐户列表信息(HQL)", Desc = "获取智方集成平台的帐户列表信息(HQL)", Url = "BloodTransfusionManageService.svc/RS_UDTO_SearchRBACUserOFLIMPByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACUser>", ReturnType = "ListRBACUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchRBACUserOFLIMPByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchRBACUserOFLIMPByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取智方集成平台的员工身份列表信息(HQL)", Desc = "获取智方集成平台的员工身份列表信息(HQL)", Url = "BloodTransfusionManageService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HREmpIdentity>", ReturnType = "ListHREmpIdentity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHREmpIdentityByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHREmpIdentityByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取智方集成平台的部门员工关系列表信息(HQL)", Desc = "获取智方集成平台的部门员工关系列表信息(HQL)", Url = "BloodTransfusionManageService.svc/RBAC_UDTO_SearchHRDeptEmpByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRDeptEmp>", ReturnType = "ListHRDeptEmp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptEmpByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptEmpByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        #region 员工信息

        [ServiceContractDescription(Name = "获取智方集成平台的员工列表信息(HQL)", Desc = "获取智方集成平台的员工列表信息(HQL)", Url = "BloodTransfusionManageService.svc/RS_UDTO_SearchHREmployeeOFLIMPByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HREmployee>", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchHREmployeeOFLIMPByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchHREmployeeOFLIMPByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询部门直属员工列表(包含子部门)", Desc = "查询部门直属员工列表(包含子部门)", Url = "BloodTransfusionManageService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?platformUrl={platformUrl}&where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_GetHREmployeeByHRDeptID?platformUrl={platformUrl}&where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_GetHREmployeeByHRDeptID(string platformUrl, string where, int limit, int page, bool isPlanish, string fields, string sort);

        [ServiceContractDescription(Name = "查询员工(HQL)", Desc = "查询员工(HQL)", Url = "BloodTransfusionManageService.svc/RBAC_UDTO_SearchHREmployeeByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HREmployee>", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHREmployeeByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHREmployeeByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "将6.6数据库的人员同步到集成平台中", Desc = "将6.6数据库的人员同步到集成平台中", Url = "BloodTransfusionManageService.svc/BT_UDTO_SyncPuserListOfHREmployeeToLIMP", Get = "", Post = "string", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SyncPuserListOfHREmployeeToLIMP", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SyncPuserListOfHREmployeeToLIMP(string platformUrl, string syncType);

        [ServiceContractDescription(Name = "将6.6数据库的人员帐号同步到集成平台中", Desc = "将6.6数据库的人员帐号同步到集成平台中", Url = "BloodTransfusionManageService.svc/BT_UDTO_SyncPuserListOfRBACUseToLIMP", Get = "", Post = "string", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SyncPuserListOfRBACUseToLIMP", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SyncPuserListOfRBACUseToLIMP(string platformUrl, string syncType);

        #endregion

        #region 部门列表

        [ServiceContractDescription(Name = "获取智方集成平台的部门列表信息(HQL)", Desc = "获取智方集成平台的部门列表信息(HQL)", Url = "BloodTransfusionManageService.svc/RBAC_UDTO_SearchHRDeptByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRDept>", ReturnType = "ListHRDept")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "根据部门ID获取部门列表树", Desc = "根据部门ID获取部门列表树", Url = "RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree?platformUrl={platformUrl}&id={id}&fields={fields}", Get = "platformUrl={platformUrl}&id={id}&fields={fields}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeHRDept")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_GetHRDeptFrameListTree?platformUrl={platformUrl}&id={id}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_GetHRDeptFrameListTree(string platformUrl, string id, string fields);

        [ServiceContractDescription(Name = "将6.6数据库的科室同步到集成平台中", Desc = "将6.6数据库的科室同步到集成平台中", Url = "BloodTransfusionManageService.svc/BT_UDTO_SyncDeptListToLIMP", Get = "", Post = "string", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SyncDeptListToLIMP", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SyncDeptListToLIMP(string platformUrl, string syncType);

        #endregion
        #endregion

        #region BUserUIConfig

        [ServiceContractDescription(Name = "新增B_UserUIConfig", Desc = "新增B_UserUIConfig", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBUserUIConfig", Get = "", Post = "BUserUIConfig", Return = "BaseResultDataValue", ReturnType = "BUserUIConfig")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBUserUIConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBUserUIConfig(BUserUIConfig entity);

        [ServiceContractDescription(Name = "修改B_UserUIConfig", Desc = "修改B_UserUIConfig", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBUserUIConfig", Get = "", Post = "BUserUIConfig", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBUserUIConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBUserUIConfig(BUserUIConfig entity);

        [ServiceContractDescription(Name = "修改B_UserUIConfig指定的属性", Desc = "修改B_UserUIConfig指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBUserUIConfigByField", Get = "", Post = "BUserUIConfig", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBUserUIConfigByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBUserUIConfigByField(BUserUIConfig entity, string fields);

        [ServiceContractDescription(Name = "删除B_UserUIConfig", Desc = "删除B_UserUIConfig", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBUserUIConfig?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBUserUIConfig?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBUserUIConfig(long id);

        [ServiceContractDescription(Name = "查询B_UserUIConfig", Desc = "查询B_UserUIConfig", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBUserUIConfig", Get = "", Post = "BUserUIConfig", Return = "BaseResultList<BUserUIConfig>", ReturnType = "ListBUserUIConfig")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBUserUIConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBUserUIConfig(BUserUIConfig entity);

        [ServiceContractDescription(Name = "查询B_UserUIConfig(HQL)", Desc = "查询B_UserUIConfig(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBUserUIConfigByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BUserUIConfig>", ReturnType = "ListBUserUIConfig")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBUserUIConfigByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBUserUIConfigByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_UserUIConfig", Desc = "通过主键ID查询B_UserUIConfig", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBUserUIConfigById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BUserUIConfig>", ReturnType = "BUserUIConfig")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBUserUIConfigById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBUserUIConfigById(long id, string fields, bool isPlanish);
        #endregion

        #region SCOperation

        [ServiceContractDescription(Name = "新增公共操作记录表", Desc = "新增公共操作记录表", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultDataValue", ReturnType = "SCOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "修改公共操作记录表", Desc = "修改公共操作记录表", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "修改公共操作记录表指定的属性", Desc = "修改公共操作记录表指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateSCOperationByField", Get = "", Post = "SCOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateSCOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateSCOperationByField(SCOperation entity, string fields);

        [ServiceContractDescription(Name = "删除公共操作记录表", Desc = "删除公共操作记录表", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelSCOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelSCOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelSCOperation(long id);

        [ServiceContractDescription(Name = "查询公共操作记录表", Desc = "查询公共操作记录表", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultList<SCOperation>", ReturnType = "ListSCOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "查询公共操作记录表(HQL)", Desc = "查询公共操作记录表(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchSCOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCOperation>", ReturnType = "ListSCOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchSCOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchSCOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共操作记录表", Desc = "通过主键ID查询公共操作记录表", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchSCOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCOperation>", ReturnType = "SCOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchSCOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchSCOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region WardType

        [ServiceContractDescription(Name = "查询WardType", Desc = "查询WardType", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchWardType", Get = "", Post = "WardType", Return = "BaseResultList<WardType>", ReturnType = "ListWardType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchWardType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchWardType(WardType entity);

        [ServiceContractDescription(Name = "查询WardType(HQL)", Desc = "查询WardType(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchWardTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WardType>", ReturnType = "ListWardType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchWardTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchWardTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询WardType", Desc = "通过主键ID查询WardType", Url = "ReaStatisticalAnalysisService.svc/BT_UDTO_SearchWardTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WardType>", ReturnType = "WardType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BloodTransfusionManageService?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchWardTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBReqEditItem

        [ServiceContractDescription(Name = "新增Blood_BReqEditItem", Desc = "新增Blood_BReqEditItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqEditItem", Get = "", Post = "BloodBReqEditItem", Return = "BaseResultDataValue", ReturnType = "BloodBReqEditItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBReqEditItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBReqEditItem(BloodBReqEditItem entity);

        [ServiceContractDescription(Name = "修改Blood_BReqEditItem", Desc = "修改Blood_BReqEditItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqEditItem", Get = "", Post = "BloodBReqEditItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqEditItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqEditItem(BloodBReqEditItem entity);

        [ServiceContractDescription(Name = "修改Blood_BReqEditItem指定的属性", Desc = "修改Blood_BReqEditItem指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqEditItemByField", Get = "", Post = "BloodBReqEditItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqEditItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqEditItemByField(BloodBReqEditItem entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BReqEditItem", Desc = "删除Blood_BReqEditItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqEditItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBReqEditItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBReqEditItem(string id);

        [ServiceContractDescription(Name = "查询Blood_BReqEditItem", Desc = "查询Blood_BReqEditItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqEditItem", Get = "", Post = "BloodBReqEditItem", Return = "BaseResultList<BloodBReqEditItem>", ReturnType = "ListBloodBReqEditItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqEditItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqEditItem(BloodBReqEditItem entity);

        [ServiceContractDescription(Name = "查询Blood_BReqEditItem(HQL)", Desc = "查询Blood_BReqEditItem(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqEditItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqEditItem>", ReturnType = "ListBloodBReqEditItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqEditItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqEditItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BReqEditItem", Desc = "通过主键ID查询Blood_BReqEditItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqEditItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqEditItem>", ReturnType = "BloodBReqEditItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqEditItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqEditItemById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodBReqForm

        [ServiceContractDescription(Name = "新增Blood_BReqForm", Desc = "新增Blood_BReqForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqForm", Get = "", Post = "BloodBReqForm", Return = "BaseResultDataValue", ReturnType = "BloodBReqForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBReqForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBReqForm(BloodBReqForm entity);

        [ServiceContractDescription(Name = "修改Blood_BReqForm", Desc = "修改Blood_BReqForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqForm", Get = "", Post = "BloodBReqForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqForm(BloodBReqForm entity);

        [ServiceContractDescription(Name = "修改Blood_BReqForm指定的属性", Desc = "修改Blood_BReqForm指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormByField", Get = "", Post = "BloodBReqForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqFormByField(BloodBReqForm entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BReqForm", Desc = "删除Blood_BReqForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBReqForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBReqForm(string id);

        [ServiceContractDescription(Name = "更新用血申请单的打印总数", Desc = "更新用血申请单的打印总数", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormPrintTotalById?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_UpdateBloodBReqFormPrintTotalById?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqFormPrintTotalById(string id);

        [ServiceContractDescription(Name = "查询Blood_BReqForm", Desc = "查询Blood_BReqForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqForm", Get = "", Post = "BloodBReqForm", Return = "BaseResultList<BloodBReqForm>", ReturnType = "ListBloodBReqForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqForm(BloodBReqForm entity);

        [ServiceContractDescription(Name = "查询Blood_BReqForm(HQL)", Desc = "查询Blood_BReqForm(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqForm>", ReturnType = "ListBloodBReqForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BReqForm", Desc = "通过主键ID查询Blood_BReqForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqForm>", ReturnType = "BloodBReqForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodBReqItem

        [ServiceContractDescription(Name = "新增Blood_BReqItem", Desc = "新增Blood_BReqItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqItem", Get = "", Post = "BloodBReqItem", Return = "BaseResultDataValue", ReturnType = "BloodBReqItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBReqItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBReqItem(BloodBReqItem entity);

        [ServiceContractDescription(Name = "修改Blood_BReqItem", Desc = "修改Blood_BReqItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqItem", Get = "", Post = "BloodBReqItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqItem(BloodBReqItem entity);

        [ServiceContractDescription(Name = "修改Blood_BReqItem指定的属性", Desc = "修改Blood_BReqItem指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqItemByField", Get = "", Post = "BloodBReqItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqItemByField(BloodBReqItem entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BReqItem", Desc = "删除Blood_BReqItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBReqItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBReqItem(long id);

        [ServiceContractDescription(Name = "查询Blood_BReqItem", Desc = "查询Blood_BReqItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItem", Get = "", Post = "BloodBReqItem", Return = "BaseResultList<BloodBReqItem>", ReturnType = "ListBloodBReqItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqItem(BloodBReqItem entity);

        [ServiceContractDescription(Name = "查询Blood_BReqItem(HQL)", Desc = "查询Blood_BReqItem(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqItem>", ReturnType = "ListBloodBReqItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BReqItem", Desc = "通过主键ID查询Blood_BReqItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqItem>", ReturnType = "BloodBReqItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBReqItemResult

        [ServiceContractDescription(Name = "新增Blood_BReqItemResult", Desc = "新增Blood_BReqItemResult", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqItemResult", Get = "", Post = "BloodBReqItemResult", Return = "BaseResultDataValue", ReturnType = "BloodBReqItemResult")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBReqItemResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBReqItemResult(BloodBReqItemResult entity);

        [ServiceContractDescription(Name = "修改Blood_BReqItemResult", Desc = "修改Blood_BReqItemResult", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqItemResult", Get = "", Post = "BloodBReqItemResult", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqItemResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqItemResult(BloodBReqItemResult entity);

        [ServiceContractDescription(Name = "修改Blood_BReqItemResult指定的属性", Desc = "修改Blood_BReqItemResult指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqItemResultByField", Get = "", Post = "BloodBReqItemResult", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqItemResultByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqItemResultByField(BloodBReqItemResult entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BReqItemResult", Desc = "删除Blood_BReqItemResult", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqItemResult?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBReqItemResult?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBReqItemResult(long id);

        [ServiceContractDescription(Name = "查询Blood_BReqItemResult", Desc = "查询Blood_BReqItemResult", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItemResult", Get = "", Post = "BloodBReqItemResult", Return = "BaseResultList<BloodBReqItemResult>", ReturnType = "ListBloodBReqItemResult")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqItemResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqItemResult(BloodBReqItemResult entity);

        [ServiceContractDescription(Name = "查询Blood_BReqItemResult(HQL)", Desc = "查询Blood_BReqItemResult(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItemResultByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqItemResult>", ReturnType = "ListBloodBReqItemResult")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqItemResultByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqItemResultByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BReqItemResult", Desc = "通过主键ID查询Blood_BReqItemResult", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItemResultById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqItemResult>", ReturnType = "BloodBReqItemResult")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqItemResultById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqItemResultById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBReqFormResult

        [ServiceContractDescription(Name = "新增Blood_BReqFormResult", Desc = "新增Blood_BReqFormResult", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqFormResult", Get = "", Post = "BloodBReqFormResult", Return = "BaseResultDataValue", ReturnType = "BloodBReqFormResult")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBReqFormResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBReqFormResult(BloodBReqFormResult entity);

        [ServiceContractDescription(Name = "修改Blood_BReqFormResult", Desc = "修改Blood_BReqFormResult", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormResult", Get = "", Post = "BloodBReqFormResult", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqFormResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqFormResult(BloodBReqFormResult entity);

        [ServiceContractDescription(Name = "修改Blood_BReqFormResult指定的属性", Desc = "修改Blood_BReqFormResult指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormResultByField", Get = "", Post = "BloodBReqFormResult", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqFormResultByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqFormResultByField(BloodBReqFormResult entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BReqFormResult", Desc = "删除Blood_BReqFormResult", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqFormResult?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBReqFormResult?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBReqFormResult(long id);

        [ServiceContractDescription(Name = "查询Blood_BReqFormResult", Desc = "查询Blood_BReqFormResult", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormResult", Get = "", Post = "BloodBReqFormResult", Return = "BaseResultList<BloodBReqFormResult>", ReturnType = "ListBloodBReqFormResult")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormResult(BloodBReqFormResult entity);

        [ServiceContractDescription(Name = "查询Blood_BReqFormResult(HQL)", Desc = "查询Blood_BReqFormResult(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormResultByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqFormResult>", ReturnType = "ListBloodBReqFormResult")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormResultByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormResultByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BReqFormResult", Desc = "通过主键ID查询Blood_BReqFormResult", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormResultById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqFormResult>", ReturnType = "BloodBReqFormResult")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormResultById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormResultById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBReqType

        [ServiceContractDescription(Name = "新增Blood_BReqType", Desc = "新增Blood_BReqType", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqType", Get = "", Post = "BloodBReqType", Return = "BaseResultDataValue", ReturnType = "BloodBReqType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBReqType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBReqType(BloodBReqType entity);

        [ServiceContractDescription(Name = "修改Blood_BReqType", Desc = "修改Blood_BReqType", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqType", Get = "", Post = "BloodBReqType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqType(BloodBReqType entity);

        [ServiceContractDescription(Name = "修改Blood_BReqType指定的属性", Desc = "修改Blood_BReqType指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqTypeByField", Get = "", Post = "BloodBReqType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqTypeByField(BloodBReqType entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BReqType", Desc = "删除Blood_BReqType", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBReqType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBReqType(int id);

        [ServiceContractDescription(Name = "查询Blood_BReqType", Desc = "查询Blood_BReqType", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqType", Get = "", Post = "BloodBReqType", Return = "BaseResultList<BloodBReqType>", ReturnType = "ListBloodBReqType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqType(BloodBReqType entity);

        [ServiceContractDescription(Name = "查询Blood_BReqType(HQL)", Desc = "查询Blood_BReqType(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqType>", ReturnType = "ListBloodBReqType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BReqType", Desc = "通过主键ID查询Blood_BReqType", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqType>", ReturnType = "BloodBReqType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqTypeById(int id, string fields, bool isPlanish);
        #endregion

        #region BloodBReqTypeItem

        [ServiceContractDescription(Name = "新增Blood_BReqTypeItem", Desc = "新增Blood_BReqTypeItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqTypeItem", Get = "", Post = "BloodBReqTypeItem", Return = "BaseResultDataValue", ReturnType = "BloodBReqTypeItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBReqTypeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBReqTypeItem(BloodBReqTypeItem entity);

        [ServiceContractDescription(Name = "修改Blood_BReqTypeItem", Desc = "修改Blood_BReqTypeItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqTypeItem", Get = "", Post = "BloodBReqTypeItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqTypeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqTypeItem(BloodBReqTypeItem entity);

        [ServiceContractDescription(Name = "修改Blood_BReqTypeItem指定的属性", Desc = "修改Blood_BReqTypeItem指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqTypeItemByField", Get = "", Post = "BloodBReqTypeItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqTypeItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqTypeItemByField(BloodBReqTypeItem entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BReqTypeItem", Desc = "删除Blood_BReqTypeItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBReqTypeItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBReqTypeItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBReqTypeItem(string id);

        [ServiceContractDescription(Name = "查询Blood_BReqTypeItem", Desc = "查询Blood_BReqTypeItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqTypeItem", Get = "", Post = "BloodBReqTypeItem", Return = "BaseResultList<BloodBReqTypeItem>", ReturnType = "ListBloodBReqTypeItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqTypeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqTypeItem(BloodBReqTypeItem entity);

        [ServiceContractDescription(Name = "查询Blood_BReqTypeItem(HQL)", Desc = "查询Blood_BReqTypeItem(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqTypeItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqTypeItem>", ReturnType = "ListBloodBReqTypeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqTypeItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqTypeItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BReqTypeItem", Desc = "通过主键ID查询Blood_BReqTypeItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqTypeItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqTypeItem>", ReturnType = "BloodBReqTypeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqTypeItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqTypeItemById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodBTestItem

        [ServiceContractDescription(Name = "新增Blood_BTestItem", Desc = "新增Blood_BTestItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBTestItem", Get = "", Post = "BloodBTestItem", Return = "BaseResultDataValue", ReturnType = "BloodBTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBTestItem(BloodBTestItem entity);

        [ServiceContractDescription(Name = "修改Blood_BTestItem", Desc = "修改Blood_BTestItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBTestItem", Get = "", Post = "BloodBTestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBTestItem(BloodBTestItem entity);

        [ServiceContractDescription(Name = "修改Blood_BTestItem指定的属性", Desc = "修改Blood_BTestItem指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBTestItemByField", Get = "", Post = "BloodBTestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBTestItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBTestItemByField(BloodBTestItem entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BTestItem", Desc = "删除Blood_BTestItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBTestItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBTestItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBTestItem(int id);

        [ServiceContractDescription(Name = "查询Blood_BTestItem", Desc = "查询Blood_BTestItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBTestItem", Get = "", Post = "BloodBTestItem", Return = "BaseResultList<BloodBTestItem>", ReturnType = "ListBloodBTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBTestItem(BloodBTestItem entity);

        [ServiceContractDescription(Name = "查询Blood_BTestItem(HQL)", Desc = "查询Blood_BTestItem(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBTestItem>", ReturnType = "ListBloodBTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBTestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BTestItem", Desc = "通过主键ID查询Blood_BTestItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBTestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBTestItem>", ReturnType = "BloodBTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBTestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBTestItemById(int id, string fields, bool isPlanish);
        #endregion

        #region BloodClass

        [ServiceContractDescription(Name = "新增Blood_Class", Desc = "新增Blood_Class", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodClass", Get = "", Post = "BloodClass", Return = "BaseResultDataValue", ReturnType = "BloodClass")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodClass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodClass(BloodClass entity);

        [ServiceContractDescription(Name = "修改Blood_Class", Desc = "修改Blood_Class", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodClass", Get = "", Post = "BloodClass", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodClass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodClass(BloodClass entity);

        [ServiceContractDescription(Name = "修改Blood_Class指定的属性", Desc = "修改Blood_Class指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodClassByField", Get = "", Post = "BloodClass", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodClassByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodClassByField(BloodClass entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_Class", Desc = "删除Blood_Class", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodClass?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodClass?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodClass(string id);

        [ServiceContractDescription(Name = "查询Blood_Class", Desc = "查询Blood_Class", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClass", Get = "", Post = "BloodClass", Return = "BaseResultList<BloodClass>", ReturnType = "ListBloodClass")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodClass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodClass(BloodClass entity);

        [ServiceContractDescription(Name = "查询Blood_Class(HQL)", Desc = "查询Blood_Class(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClassByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodClass>", ReturnType = "ListBloodClass")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodClassByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodClassByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_Class", Desc = "通过主键ID查询Blood_Class", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodClassById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodClass>", ReturnType = "BloodClass")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodClassById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodClassById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodLargeUseForm

        [ServiceContractDescription(Name = "新增Blood_LargeUseForm", Desc = "新增Blood_LargeUseForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodLargeUseForm", Get = "", Post = "BloodLargeUseForm", Return = "BaseResultDataValue", ReturnType = "BloodLargeUseForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodLargeUseForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodLargeUseForm(BloodLargeUseForm entity);

        [ServiceContractDescription(Name = "修改Blood_LargeUseForm", Desc = "修改Blood_LargeUseForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodLargeUseForm", Get = "", Post = "BloodLargeUseForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodLargeUseForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodLargeUseForm(BloodLargeUseForm entity);

        [ServiceContractDescription(Name = "修改Blood_LargeUseForm指定的属性", Desc = "修改Blood_LargeUseForm指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodLargeUseFormByField", Get = "", Post = "BloodLargeUseForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodLargeUseFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodLargeUseFormByField(BloodLargeUseForm entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_LargeUseForm", Desc = "删除Blood_LargeUseForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodLargeUseForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodLargeUseForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodLargeUseForm(string id);

        [ServiceContractDescription(Name = "查询Blood_LargeUseForm", Desc = "查询Blood_LargeUseForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodLargeUseForm", Get = "", Post = "BloodLargeUseForm", Return = "BaseResultList<BloodLargeUseForm>", ReturnType = "ListBloodLargeUseForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodLargeUseForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodLargeUseForm(BloodLargeUseForm entity);

        [ServiceContractDescription(Name = "查询Blood_LargeUseForm(HQL)", Desc = "查询Blood_LargeUseForm(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodLargeUseFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodLargeUseForm>", ReturnType = "ListBloodLargeUseForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodLargeUseFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodLargeUseFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_LargeUseForm", Desc = "通过主键ID查询Blood_LargeUseForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodLargeUseFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodLargeUseForm>", ReturnType = "BloodLargeUseForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodLargeUseFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodLargeUseFormById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodLargeUseItem

        [ServiceContractDescription(Name = "新增Blood_LargeUseItem", Desc = "新增Blood_LargeUseItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodLargeUseItem", Get = "", Post = "BloodLargeUseItem", Return = "BaseResultDataValue", ReturnType = "BloodLargeUseItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodLargeUseItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodLargeUseItem(BloodLargeUseItem entity);

        [ServiceContractDescription(Name = "修改Blood_LargeUseItem", Desc = "修改Blood_LargeUseItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodLargeUseItem", Get = "", Post = "BloodLargeUseItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodLargeUseItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodLargeUseItem(BloodLargeUseItem entity);

        [ServiceContractDescription(Name = "修改Blood_LargeUseItem指定的属性", Desc = "修改Blood_LargeUseItem指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodLargeUseItemByField", Get = "", Post = "BloodLargeUseItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodLargeUseItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodLargeUseItemByField(BloodLargeUseItem entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_LargeUseItem", Desc = "删除Blood_LargeUseItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodLargeUseItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodLargeUseItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodLargeUseItem(long id);

        [ServiceContractDescription(Name = "查询Blood_LargeUseItem", Desc = "查询Blood_LargeUseItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodLargeUseItem", Get = "", Post = "BloodLargeUseItem", Return = "BaseResultList<BloodLargeUseItem>", ReturnType = "ListBloodLargeUseItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodLargeUseItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodLargeUseItem(BloodLargeUseItem entity);

        [ServiceContractDescription(Name = "查询Blood_LargeUseItem(HQL)", Desc = "查询Blood_LargeUseItem(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodLargeUseItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodLargeUseItem>", ReturnType = "ListBloodLargeUseItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodLargeUseItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodLargeUseItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_LargeUseItem", Desc = "通过主键ID查询Blood_LargeUseItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodLargeUseItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodLargeUseItem>", ReturnType = "BloodLargeUseItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodLargeUseItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodLargeUseItemById(long id, string fields, bool isPlanish);
        #endregion

        #region Bloodstyle

        [ServiceContractDescription(Name = "新增blood_style", Desc = "新增blood_style", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodstyle", Get = "", Post = "Bloodstyle", Return = "BaseResultDataValue", ReturnType = "Bloodstyle")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodstyle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodstyle(Bloodstyle entity);

        [ServiceContractDescription(Name = "修改blood_style", Desc = "修改blood_style", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodstyle", Get = "", Post = "Bloodstyle", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodstyle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodstyle(Bloodstyle entity);

        [ServiceContractDescription(Name = "修改blood_style指定的属性", Desc = "修改blood_style指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodstyleByField", Get = "", Post = "Bloodstyle", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodstyleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodstyleByField(Bloodstyle entity, string fields);

        [ServiceContractDescription(Name = "删除blood_style", Desc = "删除blood_style", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodstyle?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodstyle?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodstyle(int id);

        [ServiceContractDescription(Name = "查询blood_style", Desc = "查询blood_style", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodstyle", Get = "", Post = "Bloodstyle", Return = "BaseResultList<Bloodstyle>", ReturnType = "ListBloodstyle")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodstyle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodstyle(Bloodstyle entity);

        [ServiceContractDescription(Name = "查询blood_style(HQL)", Desc = "查询blood_style(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodstyleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Bloodstyle>", ReturnType = "ListBloodstyle")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodstyleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodstyleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询blood_style", Desc = "通过主键ID查询blood_style", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodstyleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Bloodstyle>", ReturnType = "Bloodstyle")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodstyleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodstyleById(int id, string fields, bool isPlanish);
        #endregion

        #region BloodUnit

        [ServiceContractDescription(Name = "新增Blood_Unit", Desc = "新增Blood_Unit", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodUnit", Get = "", Post = "BloodUnit", Return = "BaseResultDataValue", ReturnType = "BloodUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodUnit(BloodUnit entity);

        [ServiceContractDescription(Name = "修改Blood_Unit", Desc = "修改Blood_Unit", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUnit", Get = "", Post = "BloodUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodUnit(BloodUnit entity);

        [ServiceContractDescription(Name = "修改Blood_Unit指定的属性", Desc = "修改Blood_Unit指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUnitByField", Get = "", Post = "BloodUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodUnitByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodUnitByField(BloodUnit entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_Unit", Desc = "删除Blood_Unit", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodUnit?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodUnit?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodUnit(int id);

        [ServiceContractDescription(Name = "查询Blood_Unit", Desc = "查询Blood_Unit", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUnit", Get = "", Post = "BloodUnit", Return = "BaseResultList<BloodUnit>", ReturnType = "ListBloodUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUnit(BloodUnit entity);

        [ServiceContractDescription(Name = "查询Blood_Unit(HQL)", Desc = "查询Blood_Unit(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodUnit>", ReturnType = "ListBloodUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUnitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_Unit", Desc = "通过主键ID查询Blood_Unit", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodUnit>", ReturnType = "BloodUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUnitById(int id, string fields, bool isPlanish);
        #endregion

        #region BloodUseType

        [ServiceContractDescription(Name = "新增Blood_UseType", Desc = "新增Blood_UseType", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodUseType", Get = "", Post = "BloodUseType", Return = "BaseResultDataValue", ReturnType = "BloodUseType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodUseType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodUseType(BloodUseType entity);

        [ServiceContractDescription(Name = "修改Blood_UseType", Desc = "修改Blood_UseType", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUseType", Get = "", Post = "BloodUseType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodUseType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodUseType(BloodUseType entity);

        [ServiceContractDescription(Name = "修改Blood_UseType指定的属性", Desc = "修改Blood_UseType指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUseTypeByField", Get = "", Post = "BloodUseType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodUseTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodUseTypeByField(BloodUseType entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_UseType", Desc = "删除Blood_UseType", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodUseType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodUseType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodUseType(string id);

        [ServiceContractDescription(Name = "查询Blood_UseType", Desc = "查询Blood_UseType", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseType", Get = "", Post = "BloodUseType", Return = "BaseResultList<BloodUseType>", ReturnType = "ListBloodUseType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUseType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUseType(BloodUseType entity);

        [ServiceContractDescription(Name = "查询Blood_UseType(HQL)", Desc = "查询Blood_UseType(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodUseType>", ReturnType = "ListBloodUseType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUseTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUseTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_UseType", Desc = "通过主键ID查询Blood_UseType", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodUseType>", ReturnType = "BloodUseType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUseTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUseTypeById(string id, string fields, bool isPlanish);
        #endregion

        #region Department

        [ServiceContractDescription(Name = "新增Department", Desc = "新增Department", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddDepartment", Get = "", Post = "Department", Return = "BaseResultDataValue", ReturnType = "Department")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddDepartment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddDepartment(Department entity);

        [ServiceContractDescription(Name = "修改Department", Desc = "修改Department", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateDepartment", Get = "", Post = "Department", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateDepartment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateDepartment(Department entity);

        [ServiceContractDescription(Name = "修改Department指定的属性", Desc = "修改Department指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateDepartmentByField", Get = "", Post = "Department", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateDepartmentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateDepartmentByField(Department entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除Department", Desc = "删除Department", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelDepartment?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelDepartment?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelDepartment(int id);

        [ServiceContractDescription(Name = "查询Department", Desc = "查询Department", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchDepartment", Get = "", Post = "Department", Return = "BaseResultList<Department>", ReturnType = "ListDepartment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDepartment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDepartment(Department entity);

        [ServiceContractDescription(Name = "查询Department(HQL)", Desc = "查询Department(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Department>", ReturnType = "ListDepartment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDepartmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDepartmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Department", Desc = "通过主键ID查询Department", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Department>", ReturnType = "Department")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDepartmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDepartmentById(int id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "按申请信息建立病区与科室关系", Desc = "按申请信息建立病区与科室关系", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddWarpAndDept", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_AddWarpAndDept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_AddWarpAndDept();

        #endregion

        #region Doctor

        [ServiceContractDescription(Name = "新增Doctor", Desc = "新增Doctor", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddDoctor", Get = "", Post = "Doctor", Return = "BaseResultDataValue", ReturnType = "Doctor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddDoctor(Doctor entity);

        [ServiceContractDescription(Name = "修改Doctor", Desc = "修改Doctor", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateDoctor", Get = "", Post = "Doctor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateDoctor(Doctor entity);

        [ServiceContractDescription(Name = "修改Doctor指定的属性", Desc = "修改Doctor指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateDoctorByField", Get = "", Post = "Doctor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateDoctorByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateDoctorByField(Doctor entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除Doctor", Desc = "删除Doctor", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelDoctor?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelDoctor?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelDoctor(int id);

        [ServiceContractDescription(Name = "查询Doctor", Desc = "查询Doctor", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchDoctor", Get = "", Post = "Doctor", Return = "BaseResultList<Doctor>", ReturnType = "ListDoctor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDoctor(Doctor entity);

        [ServiceContractDescription(Name = "查询Doctor(HQL)", Desc = "查询Doctor(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchDoctorByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Doctor>", ReturnType = "ListDoctor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDoctorByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDoctorByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Doctor", Desc = "通过主键ID查询Doctor", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchDoctorById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Doctor>", ReturnType = "Doctor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDoctorById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDoctorById(int id, string fields, bool isPlanish);
        #endregion

        #region PUser

        [ServiceContractDescription(Name = "新增PUser", Desc = "新增PUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddPUser", Get = "", Post = "PUser", Return = "BaseResultDataValue", ReturnType = "PUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddPUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddPUser(PUser entity);

        [ServiceContractDescription(Name = "修改PUser", Desc = "修改PUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdatePUser", Get = "", Post = "PUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdatePUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdatePUser(PUser entity);

        [ServiceContractDescription(Name = "修改PUser指定的属性", Desc = "修改PUser指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdatePUserByField", Get = "", Post = "PUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdatePUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdatePUserByField(PUser entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除PUser", Desc = "删除PUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelPUser?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelPUser?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelPUser(long id);

        [ServiceContractDescription(Name = "查询PUser", Desc = "查询PUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchPUser", Get = "", Post = "PUser", Return = "BaseResultList<PUser>", ReturnType = "ListPUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchPUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchPUser(PUser entity);

        [ServiceContractDescription(Name = "查询PUser(HQL)", Desc = "查询PUser(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchPUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "ListPUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchPUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchPUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询PUser", Desc = "通过主键ID查询PUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchPUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchPUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchPUserById(long id, string fields, bool isPlanish);

        #endregion

        #region NPUser

        [ServiceContractDescription(Name = "新增NPUser", Desc = "新增NPUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddNPUser", Get = "", Post = "NPUser", Return = "BaseResultDataValue", ReturnType = "NPUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddNPUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddNPUser(NPUser entity);

        [ServiceContractDescription(Name = "修改NPUser", Desc = "修改NPUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateNPUser", Get = "", Post = "NPUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateNPUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateNPUser(NPUser entity);

        [ServiceContractDescription(Name = "修改NPUser指定的属性", Desc = "修改NPUser指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateNPUserByField", Get = "", Post = "NPUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateNPUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateNPUserByField(NPUser entity, string fields);

        [ServiceContractDescription(Name = "删除NPUser", Desc = "删除NPUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelNPUser?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelNPUser?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelNPUser(int id);

        [ServiceContractDescription(Name = "查询NPUser", Desc = "查询NPUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchNPUser", Get = "", Post = "NPUser", Return = "BaseResultList<NPUser>", ReturnType = "ListNPUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchNPUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchNPUser(NPUser entity);

        [ServiceContractDescription(Name = "查询NPUser(HQL)", Desc = "查询NPUser(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchNPUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<NPUser>", ReturnType = "ListNPUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchNPUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchNPUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询NPUser", Desc = "通过主键ID查询NPUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchNPUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<NPUser>", ReturnType = "NPUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchNPUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchNPUserById(int id, string fields, bool isPlanish);
        #endregion

        #region DepartmentUser

        [ServiceContractDescription(Name = "新增DepartmentUser", Desc = "新增DepartmentUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddDepartmentUser", Get = "", Post = "DepartmentUser", Return = "BaseResultDataValue", ReturnType = "DepartmentUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddDepartmentUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddDepartmentUser(DepartmentUser entity);

        [ServiceContractDescription(Name = "修改DepartmentUser", Desc = "修改DepartmentUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateDepartmentUser", Get = "", Post = "DepartmentUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateDepartmentUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateDepartmentUser(DepartmentUser entity);

        [ServiceContractDescription(Name = "修改DepartmentUser指定的属性", Desc = "修改DepartmentUser指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateDepartmentUserByField", Get = "", Post = "DepartmentUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateDepartmentUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateDepartmentUserByField(DepartmentUser entity, string fields);

        [ServiceContractDescription(Name = "删除DepartmentUser", Desc = "删除DepartmentUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelDepartmentUser?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelDepartmentUser?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelDepartmentUser(long id);

        [ServiceContractDescription(Name = "查询DepartmentUser", Desc = "查询DepartmentUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentUser", Get = "", Post = "DepartmentUser", Return = "BaseResultList<DepartmentUser>", ReturnType = "ListDepartmentUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDepartmentUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDepartmentUser(DepartmentUser entity);

        [ServiceContractDescription(Name = "查询DepartmentUser(HQL)", Desc = "查询DepartmentUser(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<DepartmentUser>", ReturnType = "ListDepartmentUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDepartmentUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDepartmentUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询DepartmentUser", Desc = "通过主键ID查询DepartmentUser", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchDepartmentUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<DepartmentUser>", ReturnType = "DepartmentUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchDepartmentUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchDepartmentUserById(long id, string fields, bool isPlanish);
        #endregion

        #region BlooddocGrade

        [ServiceContractDescription(Name = "新增Blood_docGrade", Desc = "新增Blood_docGrade", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBlooddocGrade", Get = "", Post = "BlooddocGrade", Return = "BaseResultDataValue", ReturnType = "BlooddocGrade")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBlooddocGrade", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBlooddocGrade(BlooddocGrade entity);

        [ServiceContractDescription(Name = "修改Blood_docGrade", Desc = "修改Blood_docGrade", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBlooddocGrade", Get = "", Post = "BlooddocGrade", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBlooddocGrade", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBlooddocGrade(BlooddocGrade entity);

        [ServiceContractDescription(Name = "修改Blood_docGrade指定的属性", Desc = "修改Blood_docGrade指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBlooddocGradeByField", Get = "", Post = "BlooddocGrade", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBlooddocGradeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBlooddocGradeByField(BlooddocGrade entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_docGrade", Desc = "删除Blood_docGrade", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBlooddocGrade?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBlooddocGrade?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBlooddocGrade(string id);

        [ServiceContractDescription(Name = "查询Blood_docGrade", Desc = "查询Blood_docGrade", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBlooddocGrade", Get = "", Post = "BlooddocGrade", Return = "BaseResultList<BlooddocGrade>", ReturnType = "ListBlooddocGrade")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBlooddocGrade", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBlooddocGrade(BlooddocGrade entity);

        [ServiceContractDescription(Name = "查询Blood_docGrade(HQL)", Desc = "查询Blood_docGrade(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBlooddocGradeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BlooddocGrade>", ReturnType = "ListBlooddocGrade")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBlooddocGradeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBlooddocGradeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_docGrade", Desc = "通过主键ID查询Blood_docGrade", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBlooddocGradeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BlooddocGrade>", ReturnType = "BlooddocGrade")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBlooddocGradeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBlooddocGradeById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodUseDesc

        [ServiceContractDescription(Name = "新增Blood_UseDesc", Desc = "新增Blood_UseDesc", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodUseDesc", Get = "", Post = "BloodUseDesc", Return = "BaseResultDataValue", ReturnType = "BloodUseDesc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodUseDesc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodUseDesc(BloodUseDesc entity);

        [ServiceContractDescription(Name = "修改Blood_UseDesc", Desc = "修改Blood_UseDesc", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUseDesc", Get = "", Post = "BloodUseDesc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodUseDesc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodUseDesc(BloodUseDesc entity);

        [ServiceContractDescription(Name = "修改Blood_UseDesc指定的属性", Desc = "修改Blood_UseDesc指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUseDescByField", Get = "", Post = "BloodUseDesc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodUseDescByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodUseDescByField(BloodUseDesc entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_UseDesc", Desc = "删除Blood_UseDesc", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodUseDesc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodUseDesc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodUseDesc(int id);

        [ServiceContractDescription(Name = "查询Blood_UseDesc", Desc = "查询Blood_UseDesc", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseDesc", Get = "", Post = "BloodUseDesc", Return = "BaseResultList<BloodUseDesc>", ReturnType = "ListBloodUseDesc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUseDesc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUseDesc(BloodUseDesc entity);

        [ServiceContractDescription(Name = "查询Blood_UseDesc(HQL)", Desc = "查询Blood_UseDesc(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseDescByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodUseDesc>", ReturnType = "ListBloodUseDesc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUseDescByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUseDescByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_UseDesc", Desc = "通过主键ID查询Blood_UseDesc", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUseDescById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodUseDesc>", ReturnType = "BloodUseDesc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUseDescById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUseDescById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodABO

        [ServiceContractDescription(Name = "新增Blood_ABO", Desc = "新增Blood_ABO", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodABO", Get = "", Post = "BloodABO", Return = "BaseResultDataValue", ReturnType = "BloodABO")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodABO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodABO(BloodABO entity);

        [ServiceContractDescription(Name = "修改Blood_ABO", Desc = "修改Blood_ABO", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodABO", Get = "", Post = "BloodABO", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodABO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodABO(BloodABO entity);

        [ServiceContractDescription(Name = "修改Blood_ABO指定的属性", Desc = "修改Blood_ABO指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodABOByField", Get = "", Post = "BloodABO", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodABOByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodABOByField(BloodABO entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_ABO", Desc = "删除Blood_ABO", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodABO?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodABO?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodABO(string id);

        [ServiceContractDescription(Name = "查询Blood_ABO", Desc = "查询Blood_ABO", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodABO", Get = "", Post = "BloodABO", Return = "BaseResultList<BloodABO>", ReturnType = "ListBloodABO")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodABO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodABO(BloodABO entity);

        [ServiceContractDescription(Name = "查询Blood_ABO(HQL)", Desc = "查询Blood_ABO(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodABOByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodABO>", ReturnType = "ListBloodABO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodABOByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodABOByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_ABO", Desc = "通过主键ID查询Blood_ABO", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodABOById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodABO>", ReturnType = "BloodABO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodABOById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodABOById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodBagOperation

        [ServiceContractDescription(Name = "新增Blood_BagOperation", Desc = "新增Blood_BagOperation", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagOperation", Get = "", Post = "BloodBagOperation", Return = "BaseResultDataValue", ReturnType = "BloodBagOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagOperation(BloodBagOperation entity);

        [ServiceContractDescription(Name = "修改Blood_BagOperation", Desc = "修改Blood_BagOperation", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagOperation", Get = "", Post = "BloodBagOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagOperation(BloodBagOperation entity);

        [ServiceContractDescription(Name = "修改Blood_BagOperation指定的属性", Desc = "修改Blood_BagOperation指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagOperationByField", Get = "", Post = "BloodBagOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagOperationByField(BloodBagOperation entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BagOperation", Desc = "删除Blood_BagOperation", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagOperation(long id);

        [ServiceContractDescription(Name = "查询Blood_BagOperation", Desc = "查询Blood_BagOperation", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagOperation", Get = "", Post = "BloodBagOperation", Return = "BaseResultList<BloodBagOperation>", ReturnType = "ListBloodBagOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagOperation(BloodBagOperation entity);

        [ServiceContractDescription(Name = "查询Blood_BagOperation(HQL)", Desc = "查询Blood_BagOperation(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagOperation>", ReturnType = "ListBloodBagOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BagOperation", Desc = "通过主键ID查询Blood_BagOperation", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagOperation>", ReturnType = "BloodBagOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBagOperationDtl

        [ServiceContractDescription(Name = "新增Blood_BagOperationDtl", Desc = "新增Blood_BagOperationDtl", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagOperationDtl", Get = "", Post = "BloodBagOperationDtl", Return = "BaseResultDataValue", ReturnType = "BloodBagOperationDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagOperationDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagOperationDtl(BloodBagOperationDtl entity);

        [ServiceContractDescription(Name = "修改Blood_BagOperationDtl", Desc = "修改Blood_BagOperationDtl", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagOperationDtl", Get = "", Post = "BloodBagOperationDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagOperationDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagOperationDtl(BloodBagOperationDtl entity);

        [ServiceContractDescription(Name = "修改Blood_BagOperationDtl指定的属性", Desc = "修改Blood_BagOperationDtl指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagOperationDtlByField", Get = "", Post = "BloodBagOperationDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagOperationDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagOperationDtlByField(BloodBagOperationDtl entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BagOperationDtl", Desc = "删除Blood_BagOperationDtl", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagOperationDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagOperationDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagOperationDtl(long id);

        [ServiceContractDescription(Name = "查询Blood_BagOperationDtl", Desc = "查询Blood_BagOperationDtl", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagOperationDtl", Get = "", Post = "BloodBagOperationDtl", Return = "BaseResultList<BloodBagOperationDtl>", ReturnType = "ListBloodBagOperationDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagOperationDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagOperationDtl(BloodBagOperationDtl entity);

        [ServiceContractDescription(Name = "查询Blood_BagOperationDtl(HQL)", Desc = "查询Blood_BagOperationDtl(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagOperationDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagOperationDtl>", ReturnType = "ListBloodBagOperationDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagOperationDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagOperationDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BagOperationDtl", Desc = "通过主键ID查询Blood_BagOperationDtl", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagOperationDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagOperationDtl>", ReturnType = "BloodBagOperationDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagOperationDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagOperationDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBOutForm

        [ServiceContractDescription(Name = "新增Blood_BOutForm", Desc = "新增Blood_BOutForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBOutForm", Get = "", Post = "BloodBOutForm", Return = "BaseResultDataValue", ReturnType = "BloodBOutForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBOutForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBOutForm(BloodBOutForm entity);

        [ServiceContractDescription(Name = "修改Blood_BOutForm", Desc = "修改Blood_BOutForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBOutForm", Get = "", Post = "BloodBOutForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBOutForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBOutForm(BloodBOutForm entity);

        [ServiceContractDescription(Name = "修改Blood_BOutForm指定的属性", Desc = "修改Blood_BOutForm指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBOutFormByField", Get = "", Post = "BloodBOutForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBOutFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBOutFormByField(BloodBOutForm entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BOutForm", Desc = "删除Blood_BOutForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBOutForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBOutForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBOutForm(string id);

        [ServiceContractDescription(Name = "查询Blood_BOutForm", Desc = "查询Blood_BOutForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutForm", Get = "", Post = "BloodBOutForm", Return = "BaseResultList<BloodBOutForm>", ReturnType = "ListBloodBOutForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutForm(BloodBOutForm entity);

        [ServiceContractDescription(Name = "查询Blood_BOutForm(HQL)", Desc = "查询Blood_BOutForm(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutForm>", ReturnType = "ListBloodBOutForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BOutForm", Desc = "通过主键ID查询Blood_BOutForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutForm>", ReturnType = "BloodBOutForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutFormById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodBOutItem

        [ServiceContractDescription(Name = "新增Blood_BOutItem", Desc = "新增Blood_BOutItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBOutItem", Get = "", Post = "BloodBOutItem", Return = "BaseResultDataValue", ReturnType = "BloodBOutItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBOutItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBOutItem(BloodBOutItem entity);

        [ServiceContractDescription(Name = "修改Blood_BOutItem", Desc = "修改Blood_BOutItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBOutItem", Get = "", Post = "BloodBOutItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBOutItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBOutItem(BloodBOutItem entity);

        [ServiceContractDescription(Name = "修改Blood_BOutItem指定的属性", Desc = "修改Blood_BOutItem指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBOutItemByField", Get = "", Post = "BloodBOutItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBOutItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBOutItemByField(BloodBOutItem entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BOutItem", Desc = "删除Blood_BOutItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBOutItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBOutItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBOutItem(string id);

        [ServiceContractDescription(Name = "查询Blood_BOutItem", Desc = "查询Blood_BOutItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItem", Get = "", Post = "BloodBOutItem", Return = "BaseResultList<BloodBOutItem>", ReturnType = "ListBloodBOutItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutItem(BloodBOutItem entity);

        [ServiceContractDescription(Name = "查询Blood_BOutItem(HQL)", Desc = "查询Blood_BOutItem(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutItem>", ReturnType = "ListBloodBOutItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BOutItem", Desc = "通过主键ID查询Blood_BOutItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutItem>", ReturnType = "BloodBOutItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutItemById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodBPreForm

        [ServiceContractDescription(Name = "新增Blood_BPreForm", Desc = "新增Blood_BPreForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBPreForm", Get = "", Post = "BloodBPreForm", Return = "BaseResultDataValue", ReturnType = "BloodBPreForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBPreForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBPreForm(BloodBPreForm entity);

        [ServiceContractDescription(Name = "修改Blood_BPreForm", Desc = "修改Blood_BPreForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBPreForm", Get = "", Post = "BloodBPreForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBPreForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBPreForm(BloodBPreForm entity);

        [ServiceContractDescription(Name = "修改Blood_BPreForm指定的属性", Desc = "修改Blood_BPreForm指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBPreFormByField", Get = "", Post = "BloodBPreForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBPreFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBPreFormByField(BloodBPreForm entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BPreForm", Desc = "删除Blood_BPreForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBPreForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBPreForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBPreForm(string id);

        [ServiceContractDescription(Name = "查询Blood_BPreForm", Desc = "查询Blood_BPreForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBPreForm", Get = "", Post = "BloodBPreForm", Return = "BaseResultList<BloodBPreForm>", ReturnType = "ListBloodBPreForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBPreForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBPreForm(BloodBPreForm entity);

        [ServiceContractDescription(Name = "查询Blood_BPreForm(HQL)", Desc = "查询Blood_BPreForm(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBPreFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBPreForm>", ReturnType = "ListBloodBPreForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBPreFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBPreFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BPreForm", Desc = "通过主键ID查询Blood_BPreForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBPreFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBPreForm>", ReturnType = "BloodBPreForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBPreFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBPreFormById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodBPreItem

        [ServiceContractDescription(Name = "新增Blood_BPreItem", Desc = "新增Blood_BPreItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBPreItem", Get = "", Post = "BloodBPreItem", Return = "BaseResultDataValue", ReturnType = "BloodBPreItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBPreItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBPreItem(BloodBPreItem entity);

        [ServiceContractDescription(Name = "修改Blood_BPreItem", Desc = "修改Blood_BPreItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBPreItem", Get = "", Post = "BloodBPreItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBPreItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBPreItem(BloodBPreItem entity);

        [ServiceContractDescription(Name = "修改Blood_BPreItem指定的属性", Desc = "修改Blood_BPreItem指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBPreItemByField", Get = "", Post = "BloodBPreItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBPreItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBPreItemByField(BloodBPreItem entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BPreItem", Desc = "删除Blood_BPreItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBPreItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBPreItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBPreItem(string id);

        [ServiceContractDescription(Name = "查询Blood_BPreItem", Desc = "查询Blood_BPreItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBPreItem", Get = "", Post = "BloodBPreItem", Return = "BaseResultList<BloodBPreItem>", ReturnType = "ListBloodBPreItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBPreItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBPreItem(BloodBPreItem entity);

        [ServiceContractDescription(Name = "查询Blood_BPreItem(HQL)", Desc = "查询Blood_BPreItem(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBPreItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBPreItem>", ReturnType = "ListBloodBPreItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBPreItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBPreItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BPreItem", Desc = "通过主键ID查询Blood_BPreItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBPreItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBPreItem>", ReturnType = "BloodBPreItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBPreItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBPreItemById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodTransForm

        [ServiceContractDescription(Name = "新增Blood_TransForm", Desc = "新增Blood_TransForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodTransForm", Get = "", Post = "BloodTransForm", Return = "BaseResultDataValue", ReturnType = "BloodTransForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodTransForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodTransForm(BloodTransForm entity);

        [ServiceContractDescription(Name = "修改Blood_TransForm", Desc = "修改Blood_TransForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransForm", Get = "", Post = "BloodTransForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransForm(BloodTransForm entity);

        [ServiceContractDescription(Name = "修改Blood_TransForm指定的属性", Desc = "修改Blood_TransForm指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransFormByField", Get = "", Post = "BloodTransForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransFormByField(BloodTransForm entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_TransForm", Desc = "删除Blood_TransForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodTransForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodTransForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodTransForm(long id);

        [ServiceContractDescription(Name = "查询Blood_TransForm", Desc = "查询Blood_TransForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransForm", Get = "", Post = "BloodTransForm", Return = "BaseResultList<BloodTransForm>", ReturnType = "ListBloodTransForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransForm(BloodTransForm entity);

        [ServiceContractDescription(Name = "查询Blood_TransForm(HQL)", Desc = "查询Blood_TransForm(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransForm>", ReturnType = "ListBloodTransForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_TransForm", Desc = "通过主键ID查询Blood_TransForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransForm>", ReturnType = "BloodTransForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransFormById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodTransItem

        [ServiceContractDescription(Name = "新增Blood_TransItem", Desc = "新增Blood_TransItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodTransItem", Get = "", Post = "BloodTransItem", Return = "BaseResultDataValue", ReturnType = "BloodTransItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodTransItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodTransItem(BloodTransItem entity);

        [ServiceContractDescription(Name = "修改Blood_TransItem", Desc = "修改Blood_TransItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransItem", Get = "", Post = "BloodTransItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransItem(BloodTransItem entity);

        [ServiceContractDescription(Name = "修改Blood_TransItem指定的属性", Desc = "修改Blood_TransItem指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransItemByField", Get = "", Post = "BloodTransItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransItemByField(BloodTransItem entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_TransItem", Desc = "删除Blood_TransItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodTransItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodTransItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodTransItem(long id);

        [ServiceContractDescription(Name = "查询Blood_TransItem", Desc = "查询Blood_TransItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransItem", Get = "", Post = "BloodTransItem", Return = "BaseResultList<BloodTransItem>", ReturnType = "ListBloodTransItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransItem(BloodTransItem entity);

        [ServiceContractDescription(Name = "查询Blood_TransItem(HQL)", Desc = "查询Blood_TransItem(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransItem>", ReturnType = "ListBloodTransItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_TransItem", Desc = "通过主键ID查询Blood_TransItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransItem>", ReturnType = "BloodTransItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodTransOperation

        [ServiceContractDescription(Name = "新增Blood_TransOperation", Desc = "新增Blood_TransOperation", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodTransOperation", Get = "", Post = "BloodTransOperation", Return = "BaseResultDataValue", ReturnType = "BloodTransOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodTransOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodTransOperation(BloodTransOperation entity);

        [ServiceContractDescription(Name = "修改Blood_TransOperation", Desc = "修改Blood_TransOperation", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransOperation", Get = "", Post = "BloodTransOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransOperation(BloodTransOperation entity);

        [ServiceContractDescription(Name = "修改Blood_TransOperation指定的属性", Desc = "修改Blood_TransOperation指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransOperationByField", Get = "", Post = "BloodTransOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransOperationByField(BloodTransOperation entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_TransOperation", Desc = "删除Blood_TransOperation", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodTransOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodTransOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodTransOperation(long id);

        [ServiceContractDescription(Name = "查询Blood_TransOperation", Desc = "查询Blood_TransOperation", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransOperation", Get = "", Post = "BloodTransOperation", Return = "BaseResultList<BloodTransOperation>", ReturnType = "ListBloodTransOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransOperation(BloodTransOperation entity);

        [ServiceContractDescription(Name = "查询Blood_TransOperation(HQL)", Desc = "查询Blood_TransOperation(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransOperation>", ReturnType = "ListBloodTransOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_TransOperation", Desc = "通过主键ID查询Blood_TransOperation", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransOperation>", ReturnType = "BloodTransOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodTransRecordType

        [ServiceContractDescription(Name = "新增Blood_TransRecordType", Desc = "新增Blood_TransRecordType", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodTransRecordType", Get = "", Post = "BloodTransRecordType", Return = "BaseResultDataValue", ReturnType = "BloodTransRecordType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodTransRecordType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodTransRecordType(BloodTransRecordType entity);

        [ServiceContractDescription(Name = "修改Blood_TransRecordType", Desc = "修改Blood_TransRecordType", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransRecordType", Get = "", Post = "BloodTransRecordType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransRecordType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransRecordType(BloodTransRecordType entity);

        [ServiceContractDescription(Name = "修改Blood_TransRecordType指定的属性", Desc = "修改Blood_TransRecordType指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransRecordTypeByField", Get = "", Post = "BloodTransRecordType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransRecordTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransRecordTypeByField(BloodTransRecordType entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_TransRecordType", Desc = "删除Blood_TransRecordType", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodTransRecordType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodTransRecordType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodTransRecordType(long id);

        [ServiceContractDescription(Name = "查询Blood_TransRecordType", Desc = "查询Blood_TransRecordType", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransRecordType", Get = "", Post = "BloodTransRecordType", Return = "BaseResultList<BloodTransRecordType>", ReturnType = "ListBloodTransRecordType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransRecordType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransRecordType(BloodTransRecordType entity);

        [ServiceContractDescription(Name = "查询Blood_TransRecordType(HQL)", Desc = "查询Blood_TransRecordType(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransRecordTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransRecordType>", ReturnType = "ListBloodTransRecordType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransRecordTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransRecordTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_TransRecordType", Desc = "通过主键ID查询Blood_TransRecordType", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransRecordTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransRecordType>", ReturnType = "BloodTransRecordType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransRecordTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransRecordTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodTransRecordTypeItem

        [ServiceContractDescription(Name = "新增Blood_TransRecordTypeItem", Desc = "新增Blood_TransRecordTypeItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodTransRecordTypeItem", Get = "", Post = "BloodTransRecordTypeItem", Return = "BaseResultDataValue", ReturnType = "BloodTransRecordTypeItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodTransRecordTypeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodTransRecordTypeItem(BloodTransRecordTypeItem entity);

        [ServiceContractDescription(Name = "修改Blood_TransRecordTypeItem", Desc = "修改Blood_TransRecordTypeItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransRecordTypeItem", Get = "", Post = "BloodTransRecordTypeItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransRecordTypeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransRecordTypeItem(BloodTransRecordTypeItem entity);

        [ServiceContractDescription(Name = "修改Blood_TransRecordTypeItem指定的属性", Desc = "修改Blood_TransRecordTypeItem指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransRecordTypeItemByField", Get = "", Post = "BloodTransRecordTypeItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransRecordTypeItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodTransRecordTypeItemByField(BloodTransRecordTypeItem entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_TransRecordTypeItem", Desc = "删除Blood_TransRecordTypeItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodTransRecordTypeItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodTransRecordTypeItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodTransRecordTypeItem(long id);

        [ServiceContractDescription(Name = "查询Blood_TransRecordTypeItem", Desc = "查询Blood_TransRecordTypeItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransRecordTypeItem", Get = "", Post = "BloodTransRecordTypeItem", Return = "BaseResultList<BloodTransRecordTypeItem>", ReturnType = "ListBloodTransRecordTypeItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransRecordTypeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransRecordTypeItem(BloodTransRecordTypeItem entity);

        [ServiceContractDescription(Name = "查询Blood_TransRecordTypeItem(HQL)", Desc = "查询Blood_TransRecordTypeItem(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransRecordTypeItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransRecordTypeItem>", ReturnType = "ListBloodTransRecordTypeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransRecordTypeItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransRecordTypeItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_TransRecordTypeItem", Desc = "通过主键ID查询Blood_TransRecordTypeItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransRecordTypeItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransRecordTypeItem>", ReturnType = "BloodTransRecordTypeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransRecordTypeItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransRecordTypeItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodUsePlace

        [ServiceContractDescription(Name = "新增Blood_UsePlace", Desc = "新增Blood_UsePlace", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodUsePlace", Get = "", Post = "BloodUsePlace", Return = "BaseResultDataValue", ReturnType = "BloodUsePlace")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodUsePlace", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodUsePlace(BloodUsePlace entity);

        [ServiceContractDescription(Name = "修改Blood_UsePlace", Desc = "修改Blood_UsePlace", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUsePlace", Get = "", Post = "BloodUsePlace", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodUsePlace", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodUsePlace(BloodUsePlace entity);

        [ServiceContractDescription(Name = "修改Blood_UsePlace指定的属性", Desc = "修改Blood_UsePlace指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodUsePlaceByField", Get = "", Post = "BloodUsePlace", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodUsePlaceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodUsePlaceByField(BloodUsePlace entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_UsePlace", Desc = "删除Blood_UsePlace", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodUsePlace?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodUsePlace?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodUsePlace(string id);

        [ServiceContractDescription(Name = "查询Blood_UsePlace", Desc = "查询Blood_UsePlace", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUsePlace", Get = "", Post = "BloodUsePlace", Return = "BaseResultList<BloodUsePlace>", ReturnType = "ListBloodUsePlace")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUsePlace", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUsePlace(BloodUsePlace entity);

        [ServiceContractDescription(Name = "查询Blood_UsePlace(HQL)", Desc = "查询Blood_UsePlace(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUsePlaceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodUsePlace>", ReturnType = "ListBloodUsePlace")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUsePlaceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUsePlaceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_UsePlace", Desc = "通过主键ID查询Blood_UsePlace", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodUsePlaceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodUsePlace>", ReturnType = "BloodUsePlace")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodUsePlaceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodUsePlaceById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodBagABOCheck

        [ServiceContractDescription(Name = "新增Blood_BagABOCheck", Desc = "新增Blood_BagABOCheck", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagABOCheck", Get = "", Post = "BloodBagABOCheck", Return = "BaseResultDataValue", ReturnType = "BloodBagABOCheck")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagABOCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagABOCheck(BloodBagABOCheck entity);

        [ServiceContractDescription(Name = "修改Blood_BagABOCheck", Desc = "修改Blood_BagABOCheck", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagABOCheck", Get = "", Post = "BloodBagABOCheck", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagABOCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagABOCheck(BloodBagABOCheck entity);

        [ServiceContractDescription(Name = "修改Blood_BagABOCheck指定的属性", Desc = "修改Blood_BagABOCheck指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagABOCheckByField", Get = "", Post = "BloodBagABOCheck", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagABOCheckByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagABOCheckByField(BloodBagABOCheck entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BagABOCheck", Desc = "删除Blood_BagABOCheck", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagABOCheck?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagABOCheck?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagABOCheck(string id);

        [ServiceContractDescription(Name = "查询Blood_BagABOCheck", Desc = "查询Blood_BagABOCheck", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagABOCheck", Get = "", Post = "BloodBagABOCheck", Return = "BaseResultList<BloodBagABOCheck>", ReturnType = "ListBloodBagABOCheck")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagABOCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagABOCheck(BloodBagABOCheck entity);

        [ServiceContractDescription(Name = "查询Blood_BagABOCheck(HQL)", Desc = "查询Blood_BagABOCheck(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagABOCheckByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagABOCheck>", ReturnType = "ListBloodBagABOCheck")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagABOCheckByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagABOCheckByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BagABOCheck", Desc = "通过主键ID查询Blood_BagABOCheck", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagABOCheckById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagABOCheck>", ReturnType = "BloodBagABOCheck")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagABOCheckById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagABOCheckById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodBagABOCheckLisItem

        [ServiceContractDescription(Name = "新增Blood_BagABOCheck_LisItem", Desc = "新增Blood_BagABOCheck_LisItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagABOCheckLisItem", Get = "", Post = "BloodBagABOCheckLisItem", Return = "BaseResultDataValue", ReturnType = "BloodBagABOCheckLisItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagABOCheckLisItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagABOCheckLisItem(BloodBagABOCheckLisItem entity);

        [ServiceContractDescription(Name = "修改Blood_BagABOCheck_LisItem", Desc = "修改Blood_BagABOCheck_LisItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagABOCheckLisItem", Get = "", Post = "BloodBagABOCheckLisItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagABOCheckLisItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagABOCheckLisItem(BloodBagABOCheckLisItem entity);

        [ServiceContractDescription(Name = "修改Blood_BagABOCheck_LisItem指定的属性", Desc = "修改Blood_BagABOCheck_LisItem指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagABOCheckLisItemByField", Get = "", Post = "BloodBagABOCheckLisItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagABOCheckLisItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagABOCheckLisItemByField(BloodBagABOCheckLisItem entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BagABOCheck_LisItem", Desc = "删除Blood_BagABOCheck_LisItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagABOCheckLisItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagABOCheckLisItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagABOCheckLisItem(long id);

        [ServiceContractDescription(Name = "查询Blood_BagABOCheck_LisItem", Desc = "查询Blood_BagABOCheck_LisItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagABOCheckLisItem", Get = "", Post = "BloodBagABOCheckLisItem", Return = "BaseResultList<BloodBagABOCheckLisItem>", ReturnType = "ListBloodBagABOCheckLisItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagABOCheckLisItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagABOCheckLisItem(BloodBagABOCheckLisItem entity);

        [ServiceContractDescription(Name = "查询Blood_BagABOCheck_LisItem(HQL)", Desc = "查询Blood_BagABOCheck_LisItem(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagABOCheckLisItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagABOCheckLisItem>", ReturnType = "ListBloodBagABOCheckLisItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagABOCheckLisItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagABOCheckLisItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BagABOCheck_LisItem", Desc = "通过主键ID查询Blood_BagABOCheck_LisItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagABOCheckLisItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagABOCheckLisItem>", ReturnType = "BloodBagABOCheckLisItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagABOCheckLisItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagABOCheckLisItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBagProcess

        [ServiceContractDescription(Name = "新增Blood_BagProcess", Desc = "新增Blood_BagProcess", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagProcess", Get = "", Post = "BloodBagProcess", Return = "BaseResultDataValue", ReturnType = "BloodBagProcess")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagProcess", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagProcess(BloodBagProcess entity);

        [ServiceContractDescription(Name = "修改Blood_BagProcess", Desc = "修改Blood_BagProcess", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagProcess", Get = "", Post = "BloodBagProcess", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagProcess", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagProcess(BloodBagProcess entity);

        [ServiceContractDescription(Name = "修改Blood_BagProcess指定的属性", Desc = "修改Blood_BagProcess指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagProcessByField", Get = "", Post = "BloodBagProcess", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagProcessByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagProcessByField(BloodBagProcess entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BagProcess", Desc = "删除Blood_BagProcess", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagProcess?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagProcess?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagProcess(long id);

        [ServiceContractDescription(Name = "查询Blood_BagProcess", Desc = "查询Blood_BagProcess", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcess", Get = "", Post = "BloodBagProcess", Return = "BaseResultList<BloodBagProcess>", ReturnType = "ListBloodBagProcess")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcess", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcess(BloodBagProcess entity);

        [ServiceContractDescription(Name = "查询Blood_BagProcess(HQL)", Desc = "查询Blood_BagProcess(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagProcess>", ReturnType = "ListBloodBagProcess")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcessByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcessByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BagProcess", Desc = "通过主键ID查询Blood_BagProcess", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagProcess>", ReturnType = "BloodBagProcess")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcessById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcessById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBagProcessType

        [ServiceContractDescription(Name = "新增Blood_BagProcessType", Desc = "新增Blood_BagProcessType", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagProcessType", Get = "", Post = "BloodBagProcessType", Return = "BaseResultDataValue", ReturnType = "BloodBagProcessType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagProcessType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagProcessType(BloodBagProcessType entity);

        [ServiceContractDescription(Name = "修改Blood_BagProcessType", Desc = "修改Blood_BagProcessType", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagProcessType", Get = "", Post = "BloodBagProcessType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagProcessType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagProcessType(BloodBagProcessType entity);

        [ServiceContractDescription(Name = "修改Blood_BagProcessType指定的属性", Desc = "修改Blood_BagProcessType指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagProcessTypeByField", Get = "", Post = "BloodBagProcessType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagProcessTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagProcessTypeByField(BloodBagProcessType entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BagProcessType", Desc = "删除Blood_BagProcessType", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBagProcessType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBagProcessType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBagProcessType(string id);

        [ServiceContractDescription(Name = "查询Blood_BagProcessType", Desc = "查询Blood_BagProcessType", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessType", Get = "", Post = "BloodBagProcessType", Return = "BaseResultList<BloodBagProcessType>", ReturnType = "ListBloodBagProcessType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcessType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcessType(BloodBagProcessType entity);

        [ServiceContractDescription(Name = "查询Blood_BagProcessType(HQL)", Desc = "查询Blood_BagProcessType(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagProcessType>", ReturnType = "ListBloodBagProcessType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcessTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcessTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BagProcessType", Desc = "通过主键ID查询Blood_BagProcessType", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagProcessTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagProcessType>", ReturnType = "BloodBagProcessType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagProcessTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagProcessTypeById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodbagProcessTypeQry

        [ServiceContractDescription(Name = "新增Blood_bagProcessTypeQry", Desc = "新增Blood_bagProcessTypeQry", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodbagProcessTypeQry", Get = "", Post = "BloodbagProcessTypeQry", Return = "BaseResultDataValue", ReturnType = "BloodbagProcessTypeQry")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodbagProcessTypeQry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodbagProcessTypeQry(BloodbagProcessTypeQry entity);

        [ServiceContractDescription(Name = "修改Blood_bagProcessTypeQry", Desc = "修改Blood_bagProcessTypeQry", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodbagProcessTypeQry", Get = "", Post = "BloodbagProcessTypeQry", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodbagProcessTypeQry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodbagProcessTypeQry(BloodbagProcessTypeQry entity);

        [ServiceContractDescription(Name = "修改Blood_bagProcessTypeQry指定的属性", Desc = "修改Blood_bagProcessTypeQry指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodbagProcessTypeQryByField", Get = "", Post = "BloodbagProcessTypeQry", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodbagProcessTypeQryByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodbagProcessTypeQryByField(BloodbagProcessTypeQry entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_bagProcessTypeQry", Desc = "删除Blood_bagProcessTypeQry", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodbagProcessTypeQry?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodbagProcessTypeQry?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodbagProcessTypeQry(long id);

        [ServiceContractDescription(Name = "查询Blood_bagProcessTypeQry", Desc = "查询Blood_bagProcessTypeQry", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodbagProcessTypeQry", Get = "", Post = "BloodbagProcessTypeQry", Return = "BaseResultList<BloodbagProcessTypeQry>", ReturnType = "ListBloodbagProcessTypeQry")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodbagProcessTypeQry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodbagProcessTypeQry(BloodbagProcessTypeQry entity);

        [ServiceContractDescription(Name = "查询Blood_bagProcessTypeQry(HQL)", Desc = "查询Blood_bagProcessTypeQry(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodbagProcessTypeQryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodbagProcessTypeQry>", ReturnType = "ListBloodbagProcessTypeQry")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodbagProcessTypeQryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodbagProcessTypeQryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_bagProcessTypeQry", Desc = "通过主键ID查询Blood_bagProcessTypeQry", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodbagProcessTypeQryById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodbagProcessTypeQry>", ReturnType = "BloodbagProcessTypeQry")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodbagProcessTypeQryById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodbagProcessTypeQryById(long id, string fields, bool isPlanish);
        #endregion

        #region BloodBInForm

        [ServiceContractDescription(Name = "新增Blood_BInForm", Desc = "新增Blood_BInForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBInForm", Get = "", Post = "BloodBInForm", Return = "BaseResultDataValue", ReturnType = "BloodBInForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBInForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBInForm(BloodBInForm entity);

        [ServiceContractDescription(Name = "修改Blood_BInForm", Desc = "修改Blood_BInForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBInForm", Get = "", Post = "BloodBInForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBInForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBInForm(BloodBInForm entity);

        [ServiceContractDescription(Name = "修改Blood_BInForm指定的属性", Desc = "修改Blood_BInForm指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBInFormByField", Get = "", Post = "BloodBInForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBInFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBInFormByField(BloodBInForm entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BInForm", Desc = "删除Blood_BInForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBInForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBInForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBInForm(string id);

        [ServiceContractDescription(Name = "查询Blood_BInForm", Desc = "查询Blood_BInForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInForm", Get = "", Post = "BloodBInForm", Return = "BaseResultList<BloodBInForm>", ReturnType = "ListBloodBInForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInForm(BloodBInForm entity);

        [ServiceContractDescription(Name = "查询Blood_BInForm(HQL)", Desc = "查询Blood_BInForm(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBInForm>", ReturnType = "ListBloodBInForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BInForm", Desc = "通过主键ID查询Blood_BInForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBInForm>", ReturnType = "BloodBInForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInFormById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodBInItem

        [ServiceContractDescription(Name = "新增Blood_BInItem", Desc = "新增Blood_BInItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBInItem", Get = "", Post = "BloodBInItem", Return = "BaseResultDataValue", ReturnType = "BloodBInItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBInItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBInItem(BloodBInItem entity);

        [ServiceContractDescription(Name = "修改Blood_BInItem", Desc = "修改Blood_BInItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBInItem", Get = "", Post = "BloodBInItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBInItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBInItem(BloodBInItem entity);

        [ServiceContractDescription(Name = "修改Blood_BInItem指定的属性", Desc = "修改Blood_BInItem指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBInItemByField", Get = "", Post = "BloodBInItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBInItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBInItemByField(BloodBInItem entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BInItem", Desc = "删除Blood_BInItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBInItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBInItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBInItem(string id);

        [ServiceContractDescription(Name = "查询Blood_BInItem", Desc = "查询Blood_BInItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInItem", Get = "", Post = "BloodBInItem", Return = "BaseResultList<BloodBInItem>", ReturnType = "ListBloodBInItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInItem(BloodBInItem entity);

        [ServiceContractDescription(Name = "查询Blood_BInItem(HQL)", Desc = "查询Blood_BInItem(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBInItem>", ReturnType = "ListBloodBInItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BInItem", Desc = "通过主键ID查询Blood_BInItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBInItem>", ReturnType = "BloodBInItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInItemById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodBInItemState

        [ServiceContractDescription(Name = "新增Blood_BInItemState", Desc = "新增Blood_BInItemState", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBInItemState", Get = "", Post = "BloodBInItemState", Return = "BaseResultDataValue", ReturnType = "BloodBInItemState")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBInItemState", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBInItemState(BloodBInItemState entity);

        [ServiceContractDescription(Name = "修改Blood_BInItemState", Desc = "修改Blood_BInItemState", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBInItemState", Get = "", Post = "BloodBInItemState", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBInItemState", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBInItemState(BloodBInItemState entity);

        [ServiceContractDescription(Name = "修改Blood_BInItemState指定的属性", Desc = "修改Blood_BInItemState指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBInItemStateByField", Get = "", Post = "BloodBInItemState", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBInItemStateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBInItemStateByField(BloodBInItemState entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_BInItemState", Desc = "删除Blood_BInItemState", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodBInItemState?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodBInItemState?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodBInItemState(string id);

        [ServiceContractDescription(Name = "查询Blood_BInItemState", Desc = "查询Blood_BInItemState", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInItemState", Get = "", Post = "BloodBInItemState", Return = "BaseResultList<BloodBInItemState>", ReturnType = "ListBloodBInItemState")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInItemState", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInItemState(BloodBInItemState entity);

        [ServiceContractDescription(Name = "查询Blood_BInItemState(HQL)", Desc = "查询Blood_BInItemState(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInItemStateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBInItemState>", ReturnType = "ListBloodBInItemState")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInItemStateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInItemStateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_BInItemState", Desc = "通过主键ID查询Blood_BInItemState", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBInItemStateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBInItemState>", ReturnType = "BloodBInItemState")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBInItemStateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBInItemStateById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodReason

        [ServiceContractDescription(Name = "新增Blood_Reason", Desc = "新增Blood_Reason", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodReason", Get = "", Post = "BloodReason", Return = "BaseResultDataValue", ReturnType = "BloodReason")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodReason", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodReason(BloodReason entity);

        [ServiceContractDescription(Name = "修改Blood_Reason", Desc = "修改Blood_Reason", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodReason", Get = "", Post = "BloodReason", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodReason", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodReason(BloodReason entity);

        [ServiceContractDescription(Name = "修改Blood_Reason指定的属性", Desc = "修改Blood_Reason指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodReasonByField", Get = "", Post = "BloodReason", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodReasonByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodReasonByField(BloodReason entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_Reason", Desc = "删除Blood_Reason", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodReason?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodReason?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodReason(string id);

        [ServiceContractDescription(Name = "查询Blood_Reason", Desc = "查询Blood_Reason", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReason", Get = "", Post = "BloodReason", Return = "BaseResultList<BloodReason>", ReturnType = "ListBloodReason")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReason", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReason(BloodReason entity);

        [ServiceContractDescription(Name = "查询Blood_Reason(HQL)", Desc = "查询Blood_Reason(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReasonByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodReason>", ReturnType = "ListBloodReason")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReasonByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReasonByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_Reason", Desc = "通过主键ID查询Blood_Reason", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReasonById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodReason>", ReturnType = "BloodReason")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReasonById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReasonById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodRecei

        [ServiceContractDescription(Name = "新增Blood_Recei", Desc = "新增Blood_Recei", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodRecei", Get = "", Post = "BloodRecei", Return = "BaseResultDataValue", ReturnType = "BloodRecei")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodRecei", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodRecei(BloodRecei entity);

        [ServiceContractDescription(Name = "修改Blood_Recei", Desc = "修改Blood_Recei", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodRecei", Get = "", Post = "BloodRecei", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodRecei", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodRecei(BloodRecei entity);

        [ServiceContractDescription(Name = "修改Blood_Recei指定的属性", Desc = "修改Blood_Recei指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodReceiByField", Get = "", Post = "BloodRecei", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodReceiByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodReceiByField(BloodRecei entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_Recei", Desc = "删除Blood_Recei", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodRecei?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodRecei?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodRecei(string id);

        [ServiceContractDescription(Name = "查询Blood_Recei", Desc = "查询Blood_Recei", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodRecei", Get = "", Post = "BloodRecei", Return = "BaseResultList<BloodRecei>", ReturnType = "ListBloodRecei")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodRecei", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodRecei(BloodRecei entity);

        [ServiceContractDescription(Name = "查询Blood_Recei(HQL)", Desc = "查询Blood_Recei(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReceiByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodRecei>", ReturnType = "ListBloodRecei")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReceiByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReceiByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_Recei", Desc = "通过主键ID查询Blood_Recei", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReceiById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodRecei>", ReturnType = "BloodRecei")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReceiById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReceiById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodReceiItem

        [ServiceContractDescription(Name = "新增Blood_ReceiItem", Desc = "新增Blood_ReceiItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodReceiItem", Get = "", Post = "BloodReceiItem", Return = "BaseResultDataValue", ReturnType = "BloodReceiItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodReceiItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodReceiItem(BloodReceiItem entity);

        [ServiceContractDescription(Name = "修改Blood_ReceiItem", Desc = "修改Blood_ReceiItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodReceiItem", Get = "", Post = "BloodReceiItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodReceiItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodReceiItem(BloodReceiItem entity);

        [ServiceContractDescription(Name = "修改Blood_ReceiItem指定的属性", Desc = "修改Blood_ReceiItem指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodReceiItemByField", Get = "", Post = "BloodReceiItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodReceiItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodReceiItemByField(BloodReceiItem entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_ReceiItem", Desc = "删除Blood_ReceiItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodReceiItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodReceiItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodReceiItem(string id);

        [ServiceContractDescription(Name = "查询Blood_ReceiItem", Desc = "查询Blood_ReceiItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReceiItem", Get = "", Post = "BloodReceiItem", Return = "BaseResultList<BloodReceiItem>", ReturnType = "ListBloodReceiItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReceiItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReceiItem(BloodReceiItem entity);

        [ServiceContractDescription(Name = "查询Blood_ReceiItem(HQL)", Desc = "查询Blood_ReceiItem(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReceiItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodReceiItem>", ReturnType = "ListBloodReceiItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReceiItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReceiItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_ReceiItem", Desc = "通过主键ID查询Blood_ReceiItem", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReceiItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodReceiItem>", ReturnType = "BloodReceiItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReceiItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReceiItemById(string id, string fields, bool isPlanish);
        #endregion

        #region Bloodrefuse

        [ServiceContractDescription(Name = "新增Blood_refuse", Desc = "新增Blood_refuse", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodrefuse", Get = "", Post = "Bloodrefuse", Return = "BaseResultDataValue", ReturnType = "Bloodrefuse")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodrefuse", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodrefuse(Bloodrefuse entity);

        [ServiceContractDescription(Name = "修改Blood_refuse", Desc = "修改Blood_refuse", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodrefuse", Get = "", Post = "Bloodrefuse", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodrefuse", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodrefuse(Bloodrefuse entity);

        [ServiceContractDescription(Name = "修改Blood_refuse指定的属性", Desc = "修改Blood_refuse指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodrefuseByField", Get = "", Post = "Bloodrefuse", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodrefuseByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodrefuseByField(Bloodrefuse entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_refuse", Desc = "删除Blood_refuse", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodrefuse?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodrefuse?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodrefuse(string id);

        [ServiceContractDescription(Name = "查询Blood_refuse", Desc = "查询Blood_refuse", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodrefuse", Get = "", Post = "Bloodrefuse", Return = "BaseResultList<Bloodrefuse>", ReturnType = "ListBloodrefuse")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodrefuse", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodrefuse(Bloodrefuse entity);

        [ServiceContractDescription(Name = "查询Blood_refuse(HQL)", Desc = "查询Blood_refuse(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodrefuseByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Bloodrefuse>", ReturnType = "ListBloodrefuse")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodrefuseByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodrefuseByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_refuse", Desc = "通过主键ID查询Blood_refuse", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodrefuseById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Bloodrefuse>", ReturnType = "Bloodrefuse")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodrefuseById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodrefuseById(string id, string fields, bool isPlanish);
        #endregion

        #region BloodrefuseDispose

        [ServiceContractDescription(Name = "新增Blood_refuseDispose", Desc = "新增Blood_refuseDispose", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodrefuseDispose", Get = "", Post = "BloodrefuseDispose", Return = "BaseResultDataValue", ReturnType = "BloodrefuseDispose")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodrefuseDispose", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodrefuseDispose(BloodrefuseDispose entity);

        [ServiceContractDescription(Name = "修改Blood_refuseDispose", Desc = "修改Blood_refuseDispose", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodrefuseDispose", Get = "", Post = "BloodrefuseDispose", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodrefuseDispose", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodrefuseDispose(BloodrefuseDispose entity);

        [ServiceContractDescription(Name = "修改Blood_refuseDispose指定的属性", Desc = "修改Blood_refuseDispose指定的属性", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodrefuseDisposeByField", Get = "", Post = "BloodrefuseDispose", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodrefuseDisposeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodrefuseDisposeByField(BloodrefuseDispose entity, string fields);

        [ServiceContractDescription(Name = "删除Blood_refuseDispose", Desc = "删除Blood_refuseDispose", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBloodrefuseDispose?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBloodrefuseDispose?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBloodrefuseDispose(string id);

        [ServiceContractDescription(Name = "查询Blood_refuseDispose", Desc = "查询Blood_refuseDispose", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodrefuseDispose", Get = "", Post = "BloodrefuseDispose", Return = "BaseResultList<BloodrefuseDispose>", ReturnType = "ListBloodrefuseDispose")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodrefuseDispose", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodrefuseDispose(BloodrefuseDispose entity);

        [ServiceContractDescription(Name = "查询Blood_refuseDispose(HQL)", Desc = "查询Blood_refuseDispose(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodrefuseDisposeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodrefuseDispose>", ReturnType = "ListBloodrefuseDispose")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodrefuseDisposeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodrefuseDisposeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Blood_refuseDispose", Desc = "通过主键ID查询Blood_refuseDispose", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodrefuseDisposeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodrefuseDispose>", ReturnType = "BloodrefuseDispose")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodrefuseDisposeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodrefuseDisposeById(string id, string fields, bool isPlanish);
        #endregion

        #region PDF清单报表

        [ServiceContractDescription(Name = "获取公共模板目录的子文件夹中的所有报表模板文件", Desc = "获取公共模板目录的子文件夹中的所有报表模板文件", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchPublicTemplateFileInfoByType?publicTemplateDir={publicTemplateDir}", Get = "publicTemplateDir={publicTemplateDir}", Post = "", Return = "BaseResultList<JObject>", ReturnType = "ListJObject")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchPublicTemplateFileInfoByType?publicTemplateDir={publicTemplateDir}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchPublicTemplateFileInfoByType(string publicTemplateDir);

        [ServiceContractDescription(Name = "将选择的公共报表模板新增保存到当前实验室的报表模板表", Desc = "将选择的公共报表模板新增保存到当前实验室的报表模板表", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBTemplateOfPublicTemplate", Get = "", Post = "JObject", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBTemplateOfPublicTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_AddBTemplateOfPublicTemplate(string entityList, long labId, string labCName);

        [ServiceContractDescription(Name = "获取当前机构的某一模板类型的全部报表模板信息,如果当前机构未维护,取该模板类型的公共报表模板信息", Desc = "获取当前机构的某一模板类型的全部报表模板信息,如果当前机构未维护,取该模板类型的公共报表模板信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBTemplateByLabIdAndType?labId={labId}&breportType={breportType}&publicTemplateDir={publicTemplateDir}", Get = "labId={labId}&breportType={breportType}&publicTemplateDir={publicTemplateDir}", Post = "", Return = "BaseResultList<BTemplate>", ReturnType = "ListBTemplate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBTemplateByLabIdAndType?labId={labId}&breportType={breportType}&publicTemplateDir={publicTemplateDir}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBTemplateByLabIdAndType(long labId, long breportType, string publicTemplateDir);

        [ServiceContractDescription(Name = "获取各业务报表(如采购申请,订货清单等)PDF文件", Desc = "获取各业务报表(如采购申请,订货清单等)PDF文件", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBusinessReportOfPdfById?reaReportClass={reaReportClass}&breportType={breportType}&id={id}&operateType={operateType}&frx={frx}", Get = "reaReportClass={reaReportClass}&breportType={breportType}&id={id}&operateType={operateType}&frx={frx}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBusinessReportOfPdfById?reaReportClass={reaReportClass}&breportType={breportType}&id={id}&operateType={operateType}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream BT_UDTO_SearchBusinessReportOfPdfById(string reaReportClass, string breportType, string id, long operateType, string frx);

        #endregion

        #region Excel导出
        [ServiceContractDescription(Name = "获取各业务报表(如采购申请,订货清单等)Excel导出文件", Desc = "获取各业务报表(如采购申请,订货清单等)Excel导出文件", Url = "ReaManageService.svc/RS_UDTO_SearchBusinessReportOfExcelById?operateType={operateType}&breportType={breportType}&id={id}&frx={frx}", Get = "operateType={operateType}&breportType={breportType}&id={id}&frx={frx}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchBusinessReportOfExcelById?operateType={operateType}&breportType={breportType}&id={id}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream RS_UDTO_SearchBusinessReportOfExcelById(long operateType, string breportType, long id, string frx);

        #endregion

        #region 医生站定制服务

        [ServiceContractDescription(Name = "定制医嘱申请查询BloodBReqForm(HQL)", Desc = "定制医嘱申请查询BloodBReqForm(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormEntityListByHql?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<hBloodBReqForm>", ReturnType = "ListhBloodBReqForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormEntityListByHql?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormEntityListByHql(string where, int page, int limit, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "定制联合查询BloodBReqForm(HQL)", Desc = "定制联合查询BloodBReqForm(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormEntityListByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<hBloodBReqForm>", ReturnType = "ListhBloodBReqForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormEntityListByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormEntityListByJoinHql(string where, int page, int limit, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "定制联合查询Bloodstyle(HQL)", Desc = "定制联合查询Bloodstyle(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodstyleEntityListByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&bloodclassHql={bloodclassHql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&bloodclassHql={bloodclassHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Bloodstyle>", ReturnType = "ListBloodstyle")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodstyleEntityListByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&bloodclassHql={bloodclassHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodstyleEntityListByJoinHql(string where, string bloodclassHql, int page, int limit, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "定制联合查询BloodBReqItem(HQL)", Desc = "定制联合查询BloodBReqItem(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItemEntityListByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&docHql={docHql}&bloodstyleHql={bloodstyleHql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&docHql={docHql}&bloodstyleHql={bloodstyleHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqItem>", ReturnType = "ListBloodBReqItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqItemEntityListByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&docHql={docHql}&bloodstyleHql={bloodstyleHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqItemEntityListByJoinHql(string where, string docHql, string bloodstyleHql, int page, int limit, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "定制联合查询BloodBReqFormResult(HQL)", Desc = "定制联合查询BloodBReqFormResult(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormResultEntityListByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&docHql={docHql}&bloodstyleHql={bloodstyleHql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&docHql={docHql}&bloodstyleHql={bloodstyleHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqFormResult>", ReturnType = "ListBloodBReqFormResult")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormResultEntityListByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&docHql={docHql}&bloodstyleHql={bloodstyleHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormResultEntityListByJoinHql(string where, string docHql, string bloodstyleHql, int page, int limit, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "定制联合查询BloodBReqItemResult(HQL)", Desc = "定制联合查询BloodBReqItemResult(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqItemResultEntityListByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&docHql={docHql}&bloodbtestitemHql={bloodbtestitemHql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&docHql={docHql}&bloodbtestitemHql={bloodbtestitemHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqItemResult>", ReturnType = "ListBloodBReqItemResult")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqItemResultEntityListByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&docHql={docHql}&bloodbtestitemHql={bloodbtestitemHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqItemResultEntityListByJoinHql(string where, string docHql, string bloodbtestitemHql, int page, int limit, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "按传入的病人信息(病历号+姓名=日期范围)获取病人的检验结果(BloodBReqItemResult)", Desc = "按传入的病人信息(病历号+姓名=日期范围)获取病人的检验结果(BloodBReqItemResult)", Url = "BloodTransfusionManageService.svc/BT_UDTO_GetBloodBReqItemResultListByVLisResultHql?page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&vlisresultHql={vlisresultHql}&reqresulthql={reqresulthql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&vlisresultHql={vlisresultHql}&reqresulthql={reqresulthql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqItemResult>", ReturnType = "ListhBloodBReqItemResult")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_GetBloodBReqItemResultListByVLisResultHql?page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&vlisresultHql={vlisresultHql}&reqresulthql={reqresulthql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_GetBloodBReqItemResultListByVLisResultHql(string reqFormId, string vlisresultHql, string reqresulthql, int page, int limit, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "新增或编辑医嘱申请时,按传入的病人信息(病历号+姓名=日期范围)获取病人的检验结果(BloodBReqFormResult)", Desc = "新增或编辑医嘱申请时,按传入的病人信息(病历号+姓名=日期范围)获取病人的检验结果(BloodBReqFormResult)", Url = "BloodTransfusionManageService.svc/BT_UDTO_GetBloodBReqFormResultListByVLisResultHql?page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&vlisresultHql={vlisresultHql}&reqresulthql={reqresulthql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&vlisresultHql={vlisresultHql}&reqresulthql={reqresulthql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqFormResult>", ReturnType = "ListhBloodBReqFormResult")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_GetBloodBReqFormResultListByVLisResultHql?page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&vlisresultHql={vlisresultHql}&reqresulthql={reqresulthql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_GetBloodBReqFormResultListByVLisResultHql(string reqFormId, string vlisresultHql, string reqresulthql, int page, int limit, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "新增或编辑医嘱申请时,按传入的病人信息(病历号+姓名=日期范围)获取病人的检验结果(BloodBReqFormResult)", Desc = "新增或编辑医嘱申请时,按传入的病人信息(病历号+姓名=日期范围)获取病人的检验结果(BloodBReqFormResult)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SelectBloodBReqFormResultListByVLisResultHql?page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&vlisresultHql={vlisresultHql}&reqresulthql={reqresulthql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&vlisresultHql={vlisresultHql}&reqresulthql={reqresulthql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBReqFormResult>", ReturnType = "ListhBloodBReqFormResult")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SelectBloodBReqFormResultListByVLisResultHql?page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&vlisresultHql={vlisresultHql}&reqresulthql={reqresulthql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SelectBloodBReqFormResultListByVLisResultHql(string reqFormId, string vlisresultHql, string reqresulthql, int page, int limit, string fields, string sort, bool isPlanish);

        //entity,addBreqItemList,addResultList,empID,empName
        [ServiceContractDescription(Name = "定制新增医嘱申请主单信息及申请明细信息Blood_BReqForm", Desc = "定制新增医嘱申请主单信息及申请明细信息Blood_BReqForm", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBReqFormAndDtl", Get = "", Post = "entity,bloodsConfigVO,curDoctor,addBreqItemList,addResultList,empID,empName", Return = "BaseResultDataValue", ReturnType = "BloodBReqForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBReqFormAndDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBReqFormAndDtl(BloodBReqForm entity, BloodsConfig bloodsConfigVO, SysCurUserInfo curDoctor, IList<BloodBReqItem> addBreqItemList, IList<BloodBReqFormResult> addResultList, string empID, string empName);

        [ServiceContractDescription(Name = "定制修改医嘱申请主单信息及申请明细信息,按Blood_BReqForm指定的属性更新", Desc = "定制修改医嘱申请主单信息及申请明细信息,按Blood_BReqForm指定的属性更新", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormAndDtlByField", Get = "", Post = "entity,bloodsConfigVO,curDoctor,addBreqItemList,editBreqItemList,addResultList,editResultList,fields,empID,empName", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqFormAndDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqFormAndDtlByField(BloodBReqForm entity, BloodsConfig bloodsConfigVO, SysCurUserInfo curDoctor, IList<BloodBReqItem> addBreqItemList, IList<BloodBReqItem> editBreqItemList, IList<BloodBReqFormResult> addResultList, IList<BloodBReqFormResult> editResultList, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "对医嘱申请单进行医嘱确认提交操作", Desc = "对医嘱申请单进行医嘱确认提交操作", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormOfConfirmApplyByReqFormId", Get = "", Post = "entity,fields,empID,empName", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqFormOfConfirmApplyByReqFormId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqFormOfConfirmApplyByReqFormId(BloodBReqForm entity, BloodsConfig bloodsConfigVO, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "对医嘱申请单进行医嘱审核操作", Desc = "对医嘱申请单进行医嘱审核操作", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBReqFormOfReviewByReqForm", Get = "", Post = "entity,curDoctor,fields,empID,empName", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBReqFormOfReviewByReqForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBReqFormOfReviewByReqForm(BloodBReqForm entity, BloodsConfig bloodsConfigVO, SysCurUserInfo curDoctor, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "手工将用血申请单上传HIS", Desc = "手工将用血申请单上传HIS", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateReqDataUploadToHis", Get = "", Post = "id,curDoctor,fields,empID,empName", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateReqDataUploadToHis", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateReqDataUploadToHis(string id, BloodsConfig bloodsConfigVO, SysCurUserInfo curDoctor, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "用血申请作废(BS调用CS服务,CS服务调用HIS作废接口作废)", Desc = "用血申请作废(BS调用CS服务,CS服务调用HIS作废接口作废)", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateReqDataObsoleteToHis", Get = "", Post = "id,curDoctor,fields,empID,empName", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateReqDataObsoleteToHis", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateReqDataObsoleteToHis(string id, BloodsConfig bloodsConfigVO, SysCurUserInfo curDoctor, string fields, long empID, string empName);

        #endregion

        #region 医生站PDF预览及打印
        [ServiceContractDescription(Name = "按业务主单Id获取PDF文件(PDFJS)", Desc = "按业务主单Id获取PDF文件(PDFJS)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBusinessReportOfPDFJSById?reaReportClass={reaReportClass}&breportType={breportType}&operateType={operateType}&id={id}&frx={frx}", Get = "reaReportClass={reaReportClass}&breportType={breportType}&operateType={operateType}&id={id}&frx={frx}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBusinessReportOfPDFJSById?reaReportClass={reaReportClass}&breportType={breportType}&operateType={operateType}&id={id}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_SearchBusinessReportOfPDFJSById(string id, string reaReportClass, string breportType, long operateType, string frx);

        [ServiceContractDescription(Name = "按业务主单Id获取Base64字符串信息", Desc = "按业务主单Id获取Base64字符串信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchImageReportToBase64String?reaReportClass={reaReportClass}&breportType={breportType}&operateType={operateType}&id={id}&frx={frx}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchImageReportToBase64String?reaReportClass={reaReportClass}&breportType={breportType}&operateType={operateType}&id={id}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchImageReportToBase64String(string id, string reaReportClass, string breportType, long operateType, string frx);

        #endregion

        #region 护士站定制服务

        #region 血制品交接登记
        [ServiceContractDescription(Name = "(血制品交接登记)获取待进行血制品交接登记的病人清单信息(HQL)", Desc = "(血制品交接登记)获取待进行血制品交接登记的病人清单信息(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormVOOfHandoverByHQL?&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReqFormVO>", ReturnType = "ListBReqFormVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormVOOfHandoverByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormVOOfHandoverByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "(血制品交接登记)根据申请信息获取未交接登记完成的出库血袋信息(HQL)", Desc = "(血制品交接登记)根据申请信息获取未交接登记完成的出库血袋信息(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemOfHandoverByBReqVOHQL?&page={page}&limit={limit}&fields={fields}&where={where}&bReqVO={bReqVO}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&bReqVO={bReqVO}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutItem>", ReturnType = "ListBloodBOutItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutItemOfHandoverByBReqVOHQL?page={page}&limit={limit}&fields={fields}&where={where}&bReqVO={bReqVO}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutItemOfHandoverByBReqVOHQL(int page, int limit, string fields, string bReqVO, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "(血制品交接登记)根据申请信息获取未交接登记完成的出库主单信息(HQL)", Desc = "(血制品交接登记)根据申请信息获取未交接登记完成的出库主单信息(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutFormOfHandoverByBReqVOHQL?&page={page}&limit={limit}&fields={fields}&bReqVO={bReqVO}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&bReqVO={bReqVO}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutForm>", ReturnType = "ListBloodBOutForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutFormOfHandoverByBReqVOHQL?page={page}&limit={limit}&fields={fields}&bReqVO={bReqVO}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutFormOfHandoverByBReqVOHQL(int page, int limit, string fields, string bReqVO, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "(血制品交接登记)根据血袋号获取未交接登记完成的出库血袋信息(HQL)", Desc = "(血制品交接登记)根据血袋号获取未交接登记完成的出库血袋信息(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemOfHandoverByBBagCodeHQL?&page={page}&limit={limit}&fields={fields}&where={where}&bagCode={bagCode}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&bagCode={bagCode}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutItem>", ReturnType = "ListBloodBOutItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutItemOfHandoverByBBagCodeHQL?page={page}&limit={limit}&fields={fields}&where={where}&bagCode={bagCode}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutItemOfHandoverByBBagCodeHQL(int page, int limit, string fields, string where, string bagCode, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "新增血制品交接登记服务", Desc = "新增血制品交接登记服务", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagOperationAndDtlOfHandover", Get = "", Post = "BloodReason,IList<BloodBagOperationDtl>", Return = "BaseResultDataValue", ReturnType = "BloodBagOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagOperationAndDtlOfHandover", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagOperationAndDtlOfHandover(BloodBagOperation entity, IList<BloodBagOperationDtl> bloodBagOperationDtlList, string empID, string empName);

        [ServiceContractDescription(Name = "获取血袋接收登记信息(包含血袋的外观,完整性)", Desc = "获取血袋接收登记信息(包含血袋的外观,完整性)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagOperationAndDtlOfHandoverByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagOperation>", ReturnType = "ListBloodBagOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagOperationAndDtlOfHandoverByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagOperationAndDtlOfHandoverByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "(血制品交接登记)根据发血单号获取发血血袋信息(包含血袋外观及完整性信息)", Desc = "(血制品交接登记)根据发血单号获取发血血袋信息(包含血袋外观及完整性信息)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemAndBagOperDtlOfHandoverByHQL?&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutItem>", ReturnType = "ListBloodBOutItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutItemAndBagOperDtlOfHandoverByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutItemAndBagOperDtlOfHandoverByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过血袋接收主键ID获取血袋接收登记信息(包括接收登记信息,血袋外观及完整性)", Desc = "通过血袋接收主键ID获取血袋接收登记信息(包括接收登记信息,血袋外观及完整性)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagHandoverVOById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagHandoverVO>", ReturnType = "BloodBagHandoverVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagHandoverVOById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagHandoverVOById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "修改血袋接收登记信息(包括接收登记信息,血袋外观及完整性)", Desc = "修改血袋接收登记信息(包括接收登记信息,血袋外观及完整性)", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodBagHandoverVO", Get = "", Post = "BloodBagHandoverVO", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodBagHandoverVO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBloodBagHandoverVO(BloodBagHandoverVO entity);

        #endregion

        #region 血袋回收登记
        [ServiceContractDescription(Name = "(血袋回收登记)获取待进行血袋回收登记的出库血袋信息(HQL)", Desc = "(血制品交接登记)获取待进行血袋回收登记的出库血袋信息(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemOfRecycleByHQL?&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReqFormVO>", ReturnType = "ListBReqFormVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutItemOfRecycleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutItemOfRecycleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "(血袋回收登记)按血袋号获取待进行血袋回收登记的出库血袋信息(HQL)", Desc = "(血袋回收登记)按血袋号获取待进行血袋回收登记的出库血袋信息(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemOfRecycleByBBagCodeHQL?&page={page}&limit={limit}&fields={fields}&where={where}&bagCode={bagCode}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&bagCode={bagCode}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutItem>", ReturnType = "ListBloodBOutItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutItemOfRecycleByBBagCodeHQL?page={page}&limit={limit}&fields={fields}&where={where}&bagCode={bagCode}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutItemOfRecycleByBBagCodeHQL(int page, int limit, string fields, string where, string bagCode, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "新增血袋回收登记服务", Desc = "新增血袋回收登记服务", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodBagOperationListOfRecycle", Get = "", Post = "BloodReason,IList<BloodBagOperationDtl>", Return = "BaseResultDataValue", ReturnType = "BloodBagOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodBagOperationListOfRecycle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodBagOperationListOfRecycle(IList<BloodBagOperation> bloodBagOperationList, bool isHasTrans, string empID, string empName);

        #endregion

        #region 输血过程记录

        [ServiceContractDescription(Name = "(输血过程记录登记)获取待进行输血过程记录登记的出库主单信息(HQL)", Desc = "(输血过程记录登记)获取待进行输血过程记录登记的出库主单信息(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutFormOfBloodTransByHQL?&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReqFormVO>", ReturnType = "ListBReqFormVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutFormOfBloodTransByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutFormOfBloodTransByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "(输血过程记录登记)获取待进行输血过程记录登记的出库血袋信息(HQL)", Desc = "(输血过程记录登记)获取待进行输血过程记录登记的出库血袋信息(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemOfBloodTransByHQL?&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReqFormVO>", ReturnType = "ListBReqFormVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutItemOfBloodTransByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutItemOfBloodTransByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "(输血过程记录登记)获取不良反应表单记录项(病人体征信息)", Desc = "(输血过程记录登记)获取不良反应表单记录项(病人体征信息)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchTransfusionAntriesOfBloodTransByHQL?fields={fields}&where={where}&sort={sort}", Get = "?fields={fields}&where={where}&sort={sort}", Post = "", Return = "BaseResultList<BloodTransRecordTypeItem>", ReturnType = "ListBloodTransRecordTypeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchTransfusionAntriesOfBloodTransByHQL?fields={fields}&where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchTransfusionAntriesOfBloodTransByHQL(string fields, string where, string sort);

        [ServiceContractDescription(Name = "(输血过程记录登记)通过输血记录主单Id获取输血记录的病人体征信息", Desc = "(输血过程记录登记)通过输血记录主单Id获取输血记录的病人体征信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransItemListByTransFormId?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransItem>", ReturnType = "ListBloodTransItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransItemListByTransFormId?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransItemListByTransFormId(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过输血过程内容分类ID或输血过程记录主单ID获取临床处理结果/临床处理结果描述信息", Desc = "(输血过程记录登记)通过输血过程内容分类ID或输血过程记录主单ID获取临床处理结果/临床处理结果描述信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransItemByContentTypeID?contentTypeId={contentTypeId}&id={id}&fields={fields}&isPlanish={isPlanish}", Get = "contentTypeId={contentTypeId}&id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransItem>", ReturnType = "BloodTransItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransItemByContentTypeID?contentTypeId={contentTypeId}&id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransItemByContentTypeID(long contentTypeId, string id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "新增输血记录过程登记(支持批量登记)", Desc = "新增输血记录过程登记(支持批量登记)", Url = "BloodTransfusionManageService.svc/BT_UDTO_AddBloodTransFormAndDtlList", Get = "", Post = "IList<BloodBOutItem> outDtlList, BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, string empID, string empName", Return = "BaseResultDataValue", ReturnType = "BloodTransForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_AddBloodTransFormAndDtlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_AddBloodTransFormAndDtlList(IList<BloodBOutItem> outDtlList, BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, string empID, string empName);

        [ServiceContractDescription(Name = "(单个血袋)更新发血血袋的输血记录过程信息及输血过程记录项信息", Desc = "更新发血血袋的输血记录过程信息及输血过程记录项信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBloodTransFormAndDtlList", Get = "", Post = "transForm,transfusionAntriesList,adverseReactionList,clinicalMeasuresList,clinicalResults,clinicalResultsDesc", Return = "BaseResultDataValue", ReturnType = "BloodTransForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBloodTransFormAndDtlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_UpdateBloodTransFormAndDtlList(BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, string empID, string empName);

        #region 批量修改录入

        [ServiceContractDescription(Name = "(批量修改录入)通过选择的多个发血明细Id(多血袋)获取输血过程记录基本登记信息", Desc = "通过选择的多个发血明细Id(多血袋)获取输血过程记录基本登记信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodTransFormByOutDtlIdStr?outDtlIdStr={outDtlIdStr}&fields={fields}&isPlanish={isPlanish}", Get = "outDtlIdStr={outDtlIdStr}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransForm>", ReturnType = "BloodTransForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodTransFormByOutDtlIdStr?outDtlIdStr={outDtlIdStr}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodTransFormByOutDtlIdStr(string outDtlIdStr, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "(批量修改录入)通过选择的多个发血明细Id(多血袋)获取输血过程记录病人体征登记信息", Desc = "通过选择的多个发血明细Id(多血袋)获取输血过程记录病人体征登记信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchPatientSignsByOutDtlIdStr?outDtlIdStr={outDtlIdStr}&fields={fields}&isPlanish={isPlanish}", Get = "outDtlIdStr={outDtlIdStr}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransItem>", ReturnType = "ListBloodTransItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchPatientSignsByOutDtlIdStr?outDtlIdStr={outDtlIdStr}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchPatientSignsByOutDtlIdStr(string outDtlIdStr, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "(批量修改录入)通过选择的多个发血明细Id(多血袋)获取输血过程记录的某一不良反应分类的不良反应选择项信息", Desc = "通过选择的多个发血明细Id(多血袋)获取输血过程记录的某一不良反应分类的不良反应选择项信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchAdverseReactionOptionsByOutDtlIdStr?outDtlIdStr={outDtlIdStr}&recordTypeId={recordTypeId}&where={where}&fields={fields}&isPlanish={isPlanish}", Get = "outDtlIdStr={outDtlIdStr}&recordTypeId={recordTypeId}&where={where}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransItem>", ReturnType = "ListBloodTransItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchAdverseReactionOptionsByOutDtlIdStr?outDtlIdStr={outDtlIdStr}&recordTypeId={recordTypeId}&where={where}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchAdverseReactionOptionsByOutDtlIdStr(string outDtlIdStr, long recordTypeId, string where, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "(批量修改录入)通过选择的多个发血明细Id(多血袋)获取输血过程记录的临床处理措施信息", Desc = "通过选择的多个发血明细Id(多血袋)获取输血过程记录的临床处理措施信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchClinicalMeasuresByOutDtlIdStr?outDtlIdStr={outDtlIdStr}&where={where}&fields={fields}&isPlanish={isPlanish}", Get = "outDtlIdStr={outDtlIdStr}&where={where}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransItem>", ReturnType = "ListBloodTransItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchClinicalMeasuresByOutDtlIdStr?outDtlIdStr={outDtlIdStr}&where={where}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchClinicalMeasuresByOutDtlIdStr(string outDtlIdStr, string where, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "(批量修改录入)通过选择的多个发血明细Id(多血袋)获取输血过程记录临床处理结果登记信息", Desc = "通过选择的多个发血明细Id(多血袋)获取输血过程记录临床处理结果登记信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchClinicalResultsByOutDtlIdStr?outDtlIdStr={outDtlIdStr}&where={where}&fields={fields}&isPlanish={isPlanish}", Get = "outDtlIdStr={outDtlIdStr}&where={where}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransItem>", ReturnType = "BloodTransItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchClinicalResultsByOutDtlIdStr?outDtlIdStr={outDtlIdStr}&where={where}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchClinicalResultsByOutDtlIdStr(string outDtlIdStr, string where, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "(批量修改录入)通过选择的多个发血明细Id(多血袋)获取输血过程记录临床处理结果描述登记信息", Desc = "通过选择的多个发血明细Id(多血袋)获取输血过程记录临床处理结果描述登记信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchClinicalResultsDescByOutDtlIdStr?outDtlIdStr={outDtlIdStr}&where={where}&fields={fields}&isPlanish={isPlanish}", Get = "outDtlIdStr={outDtlIdStr}&where={where}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodTransItem>", ReturnType = "BloodTransItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchClinicalResultsDescByOutDtlIdStr?outDtlIdStr={outDtlIdStr}&where={where}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchClinicalResultsDescByOutDtlIdStr(string outDtlIdStr, string where, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "(批量修改录入)按选择的发血血袋明细ID,批量删除某一不良反应分类的所有不良反应症状记录信息", Desc = "(批量修改录入)按选择的发血血袋明细ID,批量删除某一不良反应分类的所有不良反应症状记录信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBatchTransItemByAdverseReactions?outDtlIdStr={outDtlIdStr}&recordTypeId={recordTypeId}&empID={empID}&empName={empName}", Get = "outDtlIdStr={outDtlIdStr}&recordTypeId={recordTypeId}&empID={empID}&empName={empName}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBatchTransItemByAdverseReactions?outDtlIdStr={outDtlIdStr}&recordTypeId={recordTypeId}&empID={empID}&empName={empName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBatchTransItemByAdverseReactions(string outDtlIdStr, long recordTypeId, string empID, string empName);

        [ServiceContractDescription(Name = "(批量修改录入)按选择的发血血袋明细ID,批量删除其所有的临床处理措施记录信息", Desc = "(批量修改录入)按选择的发血血袋明细ID,批量删除其所有的临床处理措施记录信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_DelBatchTransItemByClinicalMeasures?outDtlIdStr={outDtlIdStr}&empID={empID}&empName={empName}", Get = "outDtlIdStr={outDtlIdStr}&empID={empID}&empName={empName}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_DelBatchTransItemByClinicalMeasures?outDtlIdStr={outDtlIdStr}&empID={empID}&empName={empName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_DelBatchTransItemByClinicalMeasures(string outDtlIdStr, string empID, string empName);

        [ServiceContractDescription(Name = "(批量修改录入)更新保存输血过程记录的登记信息(可能包含部分新增)", Desc = "(批量修改录入)更新保存输血过程记录的登记信息(可能包含部分新增)", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBatchTransFormAndDtlList", Get = "", Post = "IList<BloodBOutItem> outDtlList, BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, string empID, string empName", Return = "BaseResultDataValue", ReturnType = "BloodTransForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBatchTransFormAndDtlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_UpdateBatchTransFormAndDtlList(IList<BloodBOutItem> outDtlList, BloodTransForm transForm, IList<BloodTransItem> transfusionAntriesList, IList<BloodTransItem> adverseReactionList, IList<BloodTransItem> clinicalMeasuresList, BloodTransItem clinicalResults, BloodTransItem clinicalResultsDesc, string empID, string empName);

        [ServiceContractDescription(Name = "按发血单ID手工标记发血主单及明细的输血登记完成度", Desc = "按发血单ID手工标记发血主单及明细的输血登记完成度", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBOutCourseCompletionByOutId?id={id}&updateValue={updateValue}&empID={empID}&empName={empName}", Get = "id={id}&updateValue={updateValue}&empID={empID}&empName={empName}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_UDTO_UpdateBOutCourseCompletionByOutId?id={id}&updateValue={updateValue}&empID={empID}&empName={empName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBOutCourseCompletionByOutId(string id, string updateValue, string empID, string empName);

        [ServiceContractDescription(Name = "对发血主单进行终止输血操作更新", Desc = "对发血主单进行终止输血操作更新", Url = "BloodTransfusionManageService.svc/BT_UDTO_UpdateBOutCourseCompletionByEndBloodOper", Get = "", Post = "BloodBOutForm entity, string updateValue, string empID, string empName", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_UpdateBOutCourseCompletionByEndBloodOper", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BT_UDTO_UpdateBOutCourseCompletionByEndBloodOper(BloodBOutForm entity, string updateValue, string empID, string empName);
        #endregion

        #endregion

        #region 输血申请综合查询

        [ServiceContractDescription(Name = "(通过血袋号)获取输血申请综合查询的BloodBReqForm(HQL)", Desc = "(通过血袋号)获取输血申请综合查询的BloodBReqForm(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBReqFormListByBBagCodeAndHql?wardId={wardId}&page={page}&limit={limit}&fields={fields}&where={where}&bbagCode={bbagCode}&sort={sort}&isPlanish={isPlanish}", Get = "wardId={wardId}&page={page}&limit={limit}&fields={fields}&where={where}&bbagCode={bbagCode}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<hBloodBReqForm>", ReturnType = "ListhBloodBReqForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBReqFormListByBBagCodeAndHql?wardId={wardId}&page={page}&limit={limit}&fields={fields}&where={where}&bbagCode={bbagCode}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBReqFormListByBBagCodeAndHql(string wardId, string where, string bbagCode, int page, int limit, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "(输血申请综合查询)按申请信息获取到相应的样本信息", Desc = "(输血申请综合查询)按申请信息获取到相应的样本信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodReceiListByBReqVO?reqFormId={reqFormId}&page={page}&limit={limit}&fields={fields}&bReqVO={bReqVO}&sort={sort}&isPlanish={isPlanish}", Get = "reqFormId={reqFormId}&page={page}&limit={limit}&fields={fields}&bReqVO={bReqVO}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodRecei>", ReturnType = "ListBloodRecei")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodReceiListByBReqVO?reqFormId={reqFormId}&page={page}&limit={limit}&fields={fields}&bReqVO={bReqVO}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodReceiListByBReqVO(string reqFormId, int page, int limit, string fields, string bReqVO, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "(输血申请综合查询)按申请单号获取发血血袋信息(HQL)", Desc = "(输血申请综合查询)按申请单号获取发血血袋信息(HQL)", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBOutItemByBReqFormIDAndHQL?&page={page}&limit={limit}&fields={fields}&where={where}&reqFormId={reqFormId}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&reqFormId={reqFormId}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBOutItem>", ReturnType = "ListBloodBOutItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBOutItemByBReqFormIDAndHQL?page={page}&limit={limit}&fields={fields}&where={where}&reqFormId={reqFormId}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBOutItemByBReqFormIDAndHQL(int page, int limit, string fields, string where, string reqFormId, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "(输血申请综合查询)按申请单号获取血袋入库VO信息", Desc = "(输血申请综合查询)按申请单号获取血袋入库VO信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBReqComplexOfInInfoVOByBReqFormID?&page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReqComplexOfInInfoVO>", ReturnType = "ListBReqComplexOfInInfoVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBReqComplexOfInInfoVOByBReqFormID?page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBReqComplexOfInInfoVOByBReqFormID(int page, int limit, string fields, string reqFormId, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "(输血申请综合查询)按申请单号获取包含血袋接收及血袋回收信息", Desc = "(输血申请综合查询)按申请单号获取包含血袋接收及血袋回收信息", Url = "BloodTransfusionManageService.svc/BT_UDTO_SearchBloodBagOperationVOOfByBReqFormID?&page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodBagOperationVO>", ReturnType = "ListBloodBagOperationVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_UDTO_SearchBloodBagOperationVOOfByBReqFormID?page={page}&limit={limit}&fields={fields}&reqFormId={reqFormId}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_UDTO_SearchBloodBagOperationVOOfByBReqFormID(int page, int limit, string fields, string reqFormId, string sort, bool isPlanish);

        #endregion

        #endregion

    }
}
