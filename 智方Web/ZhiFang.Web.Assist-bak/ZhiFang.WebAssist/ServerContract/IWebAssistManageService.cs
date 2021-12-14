using System;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.Entity.RBAC;
using ZhiFang.ServiceCommon.RBAC;
using System.Collections.Generic;

namespace ZhiFang.WebAssist.ServerContract
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IWebAssistManageService”。
    [ServiceContract]
    public interface IWebAssistManageService
    {

        #region LIS同步HIS

        [ServiceContractDescription(Name = "LIS仅同步HIS科室信息", Desc = "LIS仅同步HIS科室信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SaveLisSyncDpetHisData", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_SaveLisSyncDpetHisData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_SaveLisSyncDpetHisData();

        [ServiceContractDescription(Name = "LIS同步HIS科室人员信息", Desc = "LIS同步HIS科室人员信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SaveLisSyncHisDataInfo", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_SaveLisSyncHisDataInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_SaveLisSyncHisDataInfo();

        #endregion

        #region 6.6登录处理
        [ServiceContractDescription(Name = "账户密码重置", Desc = "账户密码重置", Url = "ServerWCF/WebAssistManageService.svc/WA_RJ_ResetAccountPassword?id={id}&empID={empID}&empName={empName}", Get = "id={id}&empID={empID}&empName={empName}", Post = "", Return = "BaseResultDataValue", ReturnType = "HREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_RJ_ResetAccountPassword?id={id}&empID={empID}&empName={empName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_RJ_ResetAccountPassword(long id, long empID, string empName);

        [ServiceContractDescription(Name = "注册帐号,", Desc = "注册帐号", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddPUserOfReg", Get = "", Post = "PUser", Return = "BaseResultDataValue", ReturnType = "PUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddPUserOfReg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddPUserOfReg(PUser entity);

        [ServiceContractDescription(Name = "修改PUser指定的属性(绑定科室)", Desc = "修改PUser指定的属性(绑定科室)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdatePUserByBindDept", Get = "", Post = "PUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdatePUserByBindDept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdatePUserByBindDept(PUser entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "用户登陆服务", Desc = "用户登陆服务", Url = "ServerWCF/WebAssistManageService.svc/WA_BA_LoginValidate?strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}", Get = "strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_BA_LoginValidate?strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool WA_BA_LoginValidate(string strUserAccount, string strPassWord, bool isValidate);

        [ServiceContractDescription(Name = "LIS同步原院感科室人员信息", Desc = "LIS同步原院感科室人员信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SaveSyncGKBarcodeInfo", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_SaveSyncGKBarcodeInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_SaveSyncGKBarcodeInfo();

        [ServiceContractDescription(Name = "LIS同步原院感的科室记录项结果短语信息", Desc = "LIS同步原院感的科室记录项结果短语信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SaveSyncTestTypeInfo", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_SaveSyncTestTypeInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_SaveSyncTestTypeInfo();

        [ServiceContractDescription(Name = "从导入的院感记录信息同步科室记录项结果短语", Desc = "从导入的院感记录信息同步科室记录项结果短语", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SaveSyncDeptPhraseInfo", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_SaveSyncDeptPhraseInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_SaveSyncDeptPhraseInfo();

        [ServiceContractDescription(Name = "LIS同步原院感申请记录信息", Desc = "LIS同步原院感申请记录信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SaveSyncGKBarRedInfo", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_SaveSyncGKBarRedInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_SaveSyncGKBarRedInfo();

        [ServiceContractDescription(Name = "LIS按院感申请信息同步科室人员信息", Desc = "LIS按院感申请信息同步科室人员信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SaveSyncDeptUserOfGKForm", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_SaveSyncDeptUserOfGKForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_SaveSyncDeptUserOfGKForm();

        [ServiceContractDescription(Name = "按机构Id初始化条码信息", Desc = "按机构Id初始化条码信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddInitysOfLabId", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_AddInitysOfLabId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_AddInitysOfLabId();

        [ServiceContractDescription(Name = "HIS调用时,依传入HIS医生ID,获取到的医生信息(包含医生审核等级信息)", Desc = "HIS调用时,依传入HIS医生ID,获取到的医生信息(包含医生审核等级信息)", Url = "ServerWCF/WebAssistManageService.svc/BT_SYS_GetSysCurDoctorInfoByHisCode?hisDoctorCode={hisDoctorCode}&hisDeptCode={hisDeptCode}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_SYS_GetSysCurDoctorInfoByHisCode?hisDoctorCode={hisDoctorCode}&hisDeptCode={hisDeptCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_SYS_GetSysCurDoctorInfoByHisCode(string hisDeptCode, string hisDoctorCode);

        [ServiceContractDescription(Name = "BS依PUser登录帐号及密码,获取医生信息(包含医生审核等级信息)", Desc = "BS依PUser登录帐号及密码,获取医生信息(包含医生审核等级信息)", Url = "ServerWCF/WebAssistManageService.svc/BT_SYS_GetSysCurDoctorInfoByAccount?account={account}&pwd={pwd}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_SYS_GetSysCurDoctorInfoByAccount?account={account}&pwd={pwd}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_SYS_GetSysCurDoctorInfoByAccount(string account, string pwd);

        [ServiceContractDescription(Name = "按PUser的帐号及密码登录", Desc = "BS按PUser的帐号及密码登录", Url = "ServerWCF/WebAssistManageService.svc/BT_SYS_LoginOfPUser?strUserAccount={strUserAccount}&strPassWord={strPassWord}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_SYS_LoginOfPUser?strUserAccount={strUserAccount}&strPassWord={strPassWord}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_SYS_LoginOfPUser(string strUserAccount, string strPassWord);

        [ServiceContractDescription(Name = "his调用时验证及获取人员信息入口", Desc = "his调用时验证及获取人员信息入口", Url = "ServerWCF/WebAssistManageService.svc/BT_SYS_LoginOfPUserByHisCode?hisWardCode={hisWardCode}&hisDeptCode={hisDeptCode}&hisDoctorCode={hisDoctorCode}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BT_SYS_LoginOfPUserByHisCode?hisWardCode={hisWardCode}&hisDeptCode={hisDeptCode}&hisDoctorCode={hisDoctorCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BT_SYS_LoginOfPUserByHisCode(string hisWardCode, string hisDeptCode, string hisDoctorCode);

        [ServiceContractDescription(Name = "PUser用户注销服务", Desc = "PUser用户注销服务", Url = "ServerWCF/WebAssistManageService.svc/BT_SYS_LogoutOfPUser?strUserAccount={strUserAccount}", Get = "strUserAccount={strUserAccount}", Post = "", Return = "bool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BT_SYS_LogoutOfPUser?strUserAccount={strUserAccount}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool BT_SYS_LogoutOfPUser(string strUserAccount);

        [ServiceContractDescription(Name = "获取用户信息后,手工调用数据库升级服务", Desc = "获取用户信息后,手工调用数据库升级服务", Url = "ServerWCF/WebAssistManageService.svc/BT_SYS_DBUpdate", Get = "", Post = "", Return = "bool", ReturnType = "Bool")]
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
        [ServiceContractDescription(Name = "从集成平台或PUser查询RBACUser(HQL)", Desc = "从集成平台或PUser查询RBACUser(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchRBACUserOfPUserByHQL?isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACUser>", ReturnType = "ListRBACUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchRBACUserOfPUserByHQL?isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchRBACUserOfPUserByHQL(bool isliip, int page, int limit, string fields, string where, string sort, bool isPlanish);

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
        [ServiceContractDescription(Name = "从集成平台或NPUser查询RBACUser(HQL)", Desc = "从集成平台或PUser查询RBACUser(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchRBACUserOfNPUserByHQL?isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACUser>", ReturnType = "ListRBACUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchRBACUserOfNPUserByHQL?isliip={isliip}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchRBACUserOfNPUserByHQL(bool isliip, int page, int limit, string fields, string where, string sort, bool isPlanish);

        /// <summary>
        /// (1)where和sort按PUser封装;
        /// (2)fields统一按RBACUser封装;
        /// </summary>
        /// <param name="fieldKey"></param>
        /// <param name="fieldVal"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "通过指定字段(如工号等)获取RBACUser(PUser转换)", Desc = "通过指定字段(如工号等)获取RBACUser(PUser转换)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchRBACUserByFieldKey?fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", Get = "fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACUser>", ReturnType = "RBACUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchRBACUserByFieldKey?fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchRBACUserByFieldKey(string fieldKey, string fieldVal, string fields, bool isPlanish);

        /// <summary>
        /// (1)where和sort按PUser封装;
        /// (2)fields统一按PUser封装;
        /// </summary>
        /// <param name="fieldKey"></param>
        /// <param name="fieldVal"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "通过指定字段(如工号等)获取PUser", Desc = "通过(如工号等)指定字段获取PUser", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchPUserByFieldKey?fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", Get = "fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchPUserByFieldKey?fieldKey={fieldKey}&fieldVal={fieldVal}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchPUserByFieldKey(string fieldKey, string fieldVal, string fields, bool isPlanish);

        #endregion

        #region 封装集成平台服务

        [ServiceContractDescription(Name = "获取智方集成平台的帐户列表信息(HQL)", Desc = "获取智方集成平台的帐户列表信息(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchRBACUserOFLIMPByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<RBACUser>", ReturnType = "ListRBACUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchRBACUserOFLIMPByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchRBACUserOFLIMPByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取智方集成平台的员工身份列表信息(HQL)", Desc = "获取智方集成平台的员工身份列表信息(HQL)", Url = "ServerWCF/WebAssistManageService.svc/RBAC_UDTO_SearchHREmpIdentityByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HREmpIdentity>", ReturnType = "ListHREmpIdentity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHREmpIdentityByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHREmpIdentityByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取智方集成平台的部门员工关系列表信息(HQL)", Desc = "获取智方集成平台的部门员工关系列表信息(HQL)", Url = "ServerWCF/WebAssistManageService.svc/RBAC_UDTO_SearchHRDeptEmpByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRDeptEmp>", ReturnType = "ListHRDeptEmp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptEmpByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptEmpByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        #region 员工信息

        [ServiceContractDescription(Name = "获取智方集成平台的员工列表信息(HQL)", Desc = "获取智方集成平台的员工列表信息(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchHREmployeeOFLIMPByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HREmployee>", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchHREmployeeOFLIMPByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchHREmployeeOFLIMPByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询部门直属员工列表(包含子部门)", Desc = "查询部门直属员工列表(包含子部门)", Url = "ServerWCF/WebAssistManageService.svc/RBAC_UDTO_GetHREmployeeByHRDeptID?platformUrl={platformUrl}&where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_GetHREmployeeByHRDeptID?platformUrl={platformUrl}&where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_GetHREmployeeByHRDeptID(string platformUrl, string where, int limit, int page, bool isPlanish, string fields, string sort);

        [ServiceContractDescription(Name = "查询员工(HQL)", Desc = "查询员工(HQL)", Url = "ServerWCF/WebAssistManageService.svc/RBAC_UDTO_SearchHREmployeeByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HREmployee>", ReturnType = "ListHREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHREmployeeByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHREmployeeByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "将6.6数据库的人员同步到集成平台中", Desc = "将6.6数据库的人员同步到集成平台中", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SyncPuserListOfHREmployeeToLIMP", Get = "", Post = "string", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SyncPuserListOfHREmployeeToLIMP", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SyncPuserListOfHREmployeeToLIMP(string platformUrl, string syncType);

        [ServiceContractDescription(Name = "将6.6数据库的人员帐号同步到集成平台中", Desc = "将6.6数据库的人员帐号同步到集成平台中", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SyncPuserListOfRBACUseToLIMP", Get = "", Post = "string", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SyncPuserListOfRBACUseToLIMP", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SyncPuserListOfRBACUseToLIMP(string platformUrl, string syncType);

        #endregion

        #region 部门列表

        [ServiceContractDescription(Name = "获取智方集成平台的部门列表信息(HQL)", Desc = "获取智方集成平台的部门列表信息(HQL)", Url = "ServerWCF/WebAssistManageService.svc/RBAC_UDTO_SearchHRDeptByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<HRDept>", ReturnType = "ListHRDept")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RBAC_UDTO_SearchHRDeptByHQL?platformUrl={platformUrl}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_UDTO_SearchHRDeptByHQL(string platformUrl, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "根据部门ID获取部门列表树", Desc = "根据部门ID获取部门列表树", Url = "RBACService.svc/RBAC_RJ_GetHRDeptFrameListTree?platformUrl={platformUrl}&id={id}&fields={fields}", Get = "platformUrl={platformUrl}&id={id}&fields={fields}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeHRDept")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RBAC_RJ_GetHRDeptFrameListTree?platformUrl={platformUrl}&id={id}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RBAC_RJ_GetHRDeptFrameListTree(string platformUrl, string id, string fields);

        [ServiceContractDescription(Name = "将6.6数据库的科室同步到集成平台中", Desc = "将6.6数据库的科室同步到集成平台中", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SyncDeptListToLIMP", Get = "", Post = "string", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SyncDeptListToLIMP", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SyncDeptListToLIMP(string platformUrl, string syncType);

        #endregion
        #endregion

        #region BUserUIConfig

        [ServiceContractDescription(Name = "新增B_UserUIConfig", Desc = "新增B_UserUIConfig", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddBUserUIConfig", Get = "", Post = "BUserUIConfig", Return = "BaseResultDataValue", ReturnType = "BUserUIConfig")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddBUserUIConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddBUserUIConfig(BUserUIConfig entity);

        [ServiceContractDescription(Name = "修改B_UserUIConfig", Desc = "修改B_UserUIConfig", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateBUserUIConfig", Get = "", Post = "BUserUIConfig", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateBUserUIConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateBUserUIConfig(BUserUIConfig entity);

        [ServiceContractDescription(Name = "修改B_UserUIConfig指定的属性", Desc = "修改B_UserUIConfig指定的属性", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateBUserUIConfigByField", Get = "", Post = "BUserUIConfig", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateBUserUIConfigByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateBUserUIConfigByField(BUserUIConfig entity, string fields);

        [ServiceContractDescription(Name = "删除B_UserUIConfig", Desc = "删除B_UserUIConfig", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_DelBUserUIConfig?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelBUserUIConfig?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelBUserUIConfig(long id);

        [ServiceContractDescription(Name = "查询B_UserUIConfig", Desc = "查询B_UserUIConfig", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchBUserUIConfig", Get = "", Post = "BUserUIConfig", Return = "BaseResultList<BUserUIConfig>", ReturnType = "ListBUserUIConfig")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchBUserUIConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchBUserUIConfig(BUserUIConfig entity);

        [ServiceContractDescription(Name = "查询B_UserUIConfig(HQL)", Desc = "查询B_UserUIConfig(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchBUserUIConfigByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BUserUIConfig>", ReturnType = "ListBUserUIConfig")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchBUserUIConfigByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchBUserUIConfigByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_UserUIConfig", Desc = "通过主键ID查询B_UserUIConfig", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchBUserUIConfigById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BUserUIConfig>", ReturnType = "BUserUIConfig")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchBUserUIConfigById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchBUserUIConfigById(long id, string fields, bool isPlanish);
        #endregion

        #region SCOperation

        [ServiceContractDescription(Name = "新增公共操作记录表", Desc = "新增公共操作记录表", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultDataValue", ReturnType = "SCOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "修改公共操作记录表", Desc = "修改公共操作记录表", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "修改公共操作记录表指定的属性", Desc = "修改公共操作记录表指定的属性", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCOperationByField", Get = "", Post = "SCOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCOperationByField(SCOperation entity, string fields);

        [ServiceContractDescription(Name = "删除公共操作记录表", Desc = "删除公共操作记录表", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_DelSCOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelSCOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelSCOperation(long id);

        [ServiceContractDescription(Name = "查询公共操作记录表", Desc = "查询公共操作记录表", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultList<SCOperation>", ReturnType = "ListSCOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "查询公共操作记录表(HQL)", Desc = "查询公共操作记录表(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCOperation>", ReturnType = "ListSCOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共操作记录表", Desc = "通过主键ID查询公共操作记录表", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCOperation>", ReturnType = "SCOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region PUser

        [ServiceContractDescription(Name = "新增PUser", Desc = "新增PUser", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddPUser", Get = "", Post = "PUser", Return = "BaseResultDataValue", ReturnType = "PUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddPUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddPUser(PUser entity);

        [ServiceContractDescription(Name = "修改PUser", Desc = "修改PUser", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdatePUser", Get = "", Post = "PUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdatePUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdatePUser(PUser entity);

        [ServiceContractDescription(Name = "修改PUser指定的属性", Desc = "修改PUser指定的属性", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdatePUserByField", Get = "", Post = "PUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdatePUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdatePUserByField(PUser entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除PUser", Desc = "删除PUser", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_DelPUser?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelPUser?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelPUser(long id);

        [ServiceContractDescription(Name = "查询PUser", Desc = "查询PUser", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchPUser", Get = "", Post = "PUser", Return = "BaseResultList<PUser>", ReturnType = "ListPUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchPUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchPUser(PUser entity);

        [ServiceContractDescription(Name = "查询PUser(HQL)", Desc = "查询PUser(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchPUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "ListPUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchPUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchPUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询PUser", Desc = "通过主键ID查询PUser", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchPUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchPUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchPUserById(long id, string fields, bool isPlanish);

        #endregion

        #region SCRecordType

        [ServiceContractDescription(Name = "新增SC_RecordType", Desc = "新增SC_RecordType", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddSCRecordType", Get = "", Post = "SCRecordType", Return = "BaseResultDataValue", ReturnType = "SCRecordType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddSCRecordType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddSCRecordType(SCRecordType entity);

        [ServiceContractDescription(Name = "修改SC_RecordType", Desc = "修改SC_RecordType", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordType", Get = "", Post = "SCRecordType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCRecordType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCRecordType(SCRecordType entity);

        [ServiceContractDescription(Name = "修改SC_RecordType指定的属性", Desc = "修改SC_RecordType指定的属性", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordTypeByField", Get = "", Post = "SCRecordType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCRecordTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCRecordTypeByField(SCRecordType entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除SC_RecordType", Desc = "删除SC_RecordType", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_DelSCRecordType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelSCRecordType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelSCRecordType(long id);

        [ServiceContractDescription(Name = "查询SC_RecordType", Desc = "查询SC_RecordType", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordType", Get = "", Post = "SCRecordType", Return = "BaseResultList<SCRecordType>", ReturnType = "ListSCRecordType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordType(SCRecordType entity);

        [ServiceContractDescription(Name = "查询SC_RecordType(HQL)", Desc = "查询SC_RecordType(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isTestItem={isTestItem}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isTestItem={isTestItem}", Post = "", Return = "BaseResultList<SCRecordType>", ReturnType = "ListSCRecordType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isTestItem={isTestItem}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isTestItem);

        [ServiceContractDescription(Name = "通过主键ID查询SC_RecordType", Desc = "通过主键ID查询SC_RecordType", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCRecordType>", ReturnType = "SCRecordType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region SCRecordTypeItem

        [ServiceContractDescription(Name = "新增SC_RecordTypeItem", Desc = "新增SC_RecordTypeItem", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddSCRecordTypeItem", Get = "", Post = "SCRecordTypeItem", Return = "BaseResultDataValue", ReturnType = "SCRecordTypeItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddSCRecordTypeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddSCRecordTypeItem(SCRecordTypeItem entity);

        [ServiceContractDescription(Name = "修改SC_RecordTypeItem", Desc = "修改SC_RecordTypeItem", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordTypeItem", Get = "", Post = "SCRecordTypeItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCRecordTypeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCRecordTypeItem(SCRecordTypeItem entity);

        [ServiceContractDescription(Name = "修改SC_RecordTypeItem指定的属性", Desc = "修改SC_RecordTypeItem指定的属性", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordTypeItemByField", Get = "", Post = "SCRecordTypeItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCRecordTypeItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCRecordTypeItemByField(SCRecordTypeItem entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除SC_RecordTypeItem", Desc = "删除SC_RecordTypeItem", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_DelSCRecordTypeItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelSCRecordTypeItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelSCRecordTypeItem(long id);

        [ServiceContractDescription(Name = "查询SC_RecordTypeItem", Desc = "查询SC_RecordTypeItem", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordTypeItem", Get = "", Post = "SCRecordTypeItem", Return = "BaseResultList<SCRecordTypeItem>", ReturnType = "ListSCRecordTypeItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordTypeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordTypeItem(SCRecordTypeItem entity);

        [ServiceContractDescription(Name = "查询SC_RecordTypeItem(HQL)", Desc = "查询SC_RecordTypeItem(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordTypeItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isTestItem={isTestItem}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isTestItem={isTestItem}", Post = "", Return = "BaseResultList<SCRecordTypeItem>", ReturnType = "ListSCRecordTypeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordTypeItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isTestItem={isTestItem}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordTypeItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isTestItem);

        [ServiceContractDescription(Name = "通过主键ID查询SC_RecordTypeItem", Desc = "通过主键ID查询SC_RecordTypeItem", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordTypeItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCRecordTypeItem>", ReturnType = "SCRecordTypeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordTypeItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordTypeItemById(long id, string fields, bool isPlanish);
        #endregion

        #region SCRecordPhrase

        [ServiceContractDescription(Name = "新增SC_RecordPhrase", Desc = "新增SC_RecordPhrase", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddSCRecordPhrase", Get = "", Post = "SCRecordPhrase", Return = "BaseResultDataValue", ReturnType = "SCRecordPhrase")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddSCRecordPhrase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddSCRecordPhrase(SCRecordPhrase entity);

        [ServiceContractDescription(Name = "修改SC_RecordPhrase", Desc = "修改SC_RecordPhrase", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordPhrase", Get = "", Post = "SCRecordPhrase", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCRecordPhrase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCRecordPhrase(SCRecordPhrase entity);

        [ServiceContractDescription(Name = "修改SC_RecordPhrase指定的属性", Desc = "修改SC_RecordPhrase指定的属性", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordPhraseByField", Get = "", Post = "SCRecordPhrase", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCRecordPhraseByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCRecordPhraseByField(SCRecordPhrase entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除SC_RecordPhrase", Desc = "删除SC_RecordPhrase", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_DelSCRecordPhrase?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelSCRecordPhrase?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelSCRecordPhrase(long id);

        [ServiceContractDescription(Name = "查询SC_RecordPhrase", Desc = "查询SC_RecordPhrase", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordPhrase", Get = "", Post = "SCRecordPhrase", Return = "BaseResultList<SCRecordPhrase>", ReturnType = "ListSCRecordPhrase")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordPhrase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordPhrase(SCRecordPhrase entity);

        [ServiceContractDescription(Name = "查询SC_RecordPhrase(HQL)", Desc = "查询SC_RecordPhrase(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordPhraseByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCRecordPhrase>", ReturnType = "ListSCRecordPhrase")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordPhraseByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordPhraseByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询SC_RecordPhrase", Desc = "通过主键ID查询SC_RecordPhrase", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordPhraseById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCRecordPhrase>", ReturnType = "SCRecordPhrase")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordPhraseById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordPhraseById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询院感申请的SC_RecordPhrase(HQL)", Desc = "查询院感申请的SC_RecordPhrase(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordPhraseOfGKByHQL?page={page}&limit={limit}&where={where}&sort={sort}", Get = "page={page}&limit={limit}&where={where}&sort={sort}", Post = "", Return = "BaseResultList<SCRecordPhrase>", ReturnType = "ListSCRecordPhrase")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordPhraseOfGKByHQL?page={page}&limit={limit}&where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordPhraseOfGKByHQL(int page, int limit,string where, string sort);

        #endregion

        #region SCRecordDtl

        [ServiceContractDescription(Name = "新增SC_RecordDtl", Desc = "新增SC_RecordDtl", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddSCRecordDtl", Get = "", Post = "SCRecordDtl", Return = "BaseResultDataValue", ReturnType = "SCRecordDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddSCRecordDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddSCRecordDtl(SCRecordDtl entity);

        [ServiceContractDescription(Name = "修改SC_RecordDtl", Desc = "修改SC_RecordDtl", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordDtl", Get = "", Post = "SCRecordDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCRecordDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCRecordDtl(SCRecordDtl entity);

        [ServiceContractDescription(Name = "修改SC_RecordDtl指定的属性", Desc = "修改SC_RecordDtl指定的属性", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordDtlByField", Get = "", Post = "SCRecordDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCRecordDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCRecordDtlByField(SCRecordDtl entity, string fields);

        [ServiceContractDescription(Name = "删除SC_RecordDtl", Desc = "删除SC_RecordDtl", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_DelSCRecordDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelSCRecordDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelSCRecordDtl(long id);

        [ServiceContractDescription(Name = "查询SC_RecordDtl", Desc = "查询SC_RecordDtl", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordDtl", Get = "", Post = "SCRecordDtl", Return = "BaseResultList<SCRecordDtl>", ReturnType = "ListSCRecordDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordDtl(SCRecordDtl entity);

        [ServiceContractDescription(Name = "查询SC_RecordDtl(HQL)", Desc = "查询SC_RecordDtl(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCRecordDtl>", ReturnType = "ListSCRecordDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询SC_RecordDtl", Desc = "通过主键ID查询SC_RecordDtl", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCRecordDtl>", ReturnType = "SCRecordDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region SCRecordItemLink

        [ServiceContractDescription(Name = "新增记录项类型与记录项字典关系", Desc = "新增记录项类型与记录项字典关系", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddSCRecordItemLink", Get = "", Post = "SCRecordItemLink", Return = "BaseResultDataValue", ReturnType = "SCRecordItemLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddSCRecordItemLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddSCRecordItemLink(SCRecordItemLink entity);

        [ServiceContractDescription(Name = "修改记录项类型与记录项字典关系", Desc = "修改记录项类型与记录项字典关系", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordItemLink", Get = "", Post = "SCRecordItemLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCRecordItemLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCRecordItemLink(SCRecordItemLink entity);

        [ServiceContractDescription(Name = "修改记录项类型与记录项字典关系指定的属性", Desc = "修改记录项类型与记录项字典关系指定的属性", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCRecordItemLinkByField", Get = "", Post = "SCRecordItemLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCRecordItemLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCRecordItemLinkByField(SCRecordItemLink entity, string fields);

        [ServiceContractDescription(Name = "删除记录项类型与记录项字典关系", Desc = "删除记录项类型与记录项字典关系", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_DelSCRecordItemLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelSCRecordItemLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelSCRecordItemLink(long id);

        [ServiceContractDescription(Name = "查询记录项类型与记录项字典关系", Desc = "查询记录项类型与记录项字典关系", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordItemLink", Get = "", Post = "SCRecordItemLink", Return = "BaseResultList<SCRecordItemLink>", ReturnType = "ListSCRecordItemLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordItemLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordItemLink(SCRecordItemLink entity);

        [ServiceContractDescription(Name = "查询记录项类型与记录项字典关系(HQL)", Desc = "查询记录项类型与记录项字典关系(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordItemLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isTestItem={isTestItem}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isTestItem={isTestItem}", Post = "", Return = "BaseResultList<SCRecordItemLink>", ReturnType = "ListSCRecordItemLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordItemLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isTestItem={isTestItem}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordItemLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isTestItem);

        [ServiceContractDescription(Name = "通过主键ID查询记录项类型与记录项字典关系", Desc = "通过主键ID查询记录项类型与记录项字典关系", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordItemLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCRecordItemLink>", ReturnType = "SCRecordItemLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordItemLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordItemLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region BLodopTemplet

        [ServiceContractDescription(Name = "新增Lodop打印模板维护信息", Desc = "新增Lodop打印模板维护信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddBLodopTemplet", Get = "", Post = "BLodopTemplet", Return = "BaseResultDataValue", ReturnType = "BLodopTemplet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddBLodopTemplet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddBLodopTemplet(BLodopTemplet entity);

        [ServiceContractDescription(Name = "修改Lodop打印模板维护信息", Desc = "修改Lodop打印模板维护信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateBLodopTemplet", Get = "", Post = "BLodopTemplet", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateBLodopTemplet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateBLodopTemplet(BLodopTemplet entity);

        [ServiceContractDescription(Name = "修改Lodop打印模板维护信息指定的属性", Desc = "修改Lodop打印模板维护信息指定的属性", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateBLodopTempletByField", Get = "", Post = "BLodopTemplet", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateBLodopTempletByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateBLodopTempletByField(BLodopTemplet entity, string fields);

        [ServiceContractDescription(Name = "删除Lodop打印模板维护信息", Desc = "删除Lodop打印模板维护信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_DelBLodopTemplet?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelBLodopTemplet?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelBLodopTemplet(long id);

        [ServiceContractDescription(Name = "查询Lodop打印模板维护信息", Desc = "查询Lodop打印模板维护信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchBLodopTemplet", Get = "", Post = "BLodopTemplet", Return = "BaseResultList<BLodopTemplet>", ReturnType = "ListBLodopTemplet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchBLodopTemplet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchBLodopTemplet(BLodopTemplet entity);

        [ServiceContractDescription(Name = "查询Lodop打印模板维护信息(HQL)", Desc = "查询Lodop打印模板维护信息(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchBLodopTempletByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLodopTemplet>", ReturnType = "ListBLodopTemplet")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchBLodopTempletByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchBLodopTempletByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lodop打印模板维护信息", Desc = "通过主键ID查询Lodop打印模板维护信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchBLodopTempletById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLodopTemplet>", ReturnType = "BLodopTemplet")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchBLodopTempletById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchBLodopTempletById(long id, string fields, bool isPlanish);
        #endregion

        #region SCBarCodeRules

        [ServiceContractDescription(Name = "新增一维条码序号信息", Desc = "新增一维条码序号信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddSCBarCodeRules", Get = "", Post = "SCBarCodeRules", Return = "BaseResultDataValue", ReturnType = "SCBarCodeRules")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddSCBarCodeRules", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddSCBarCodeRules(SCBarCodeRules entity);

        [ServiceContractDescription(Name = "修改一维条码序号信息", Desc = "修改一维条码序号信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCBarCodeRules", Get = "", Post = "SCBarCodeRules", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCBarCodeRules", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCBarCodeRules(SCBarCodeRules entity);

        [ServiceContractDescription(Name = "修改一维条码序号信息指定的属性", Desc = "修改一维条码序号信息指定的属性", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateSCBarCodeRulesByField", Get = "", Post = "SCBarCodeRules", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateSCBarCodeRulesByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateSCBarCodeRulesByField(SCBarCodeRules entity, string fields);

        [ServiceContractDescription(Name = "删除一维条码序号信息", Desc = "删除一维条码序号信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_DelSCBarCodeRules?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelSCBarCodeRules?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelSCBarCodeRules(long id);

        [ServiceContractDescription(Name = "查询一维条码序号信息", Desc = "查询一维条码序号信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCBarCodeRules", Get = "", Post = "SCBarCodeRules", Return = "BaseResultList<SCBarCodeRules>", ReturnType = "ListSCBarCodeRules")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCBarCodeRules", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCBarCodeRules(SCBarCodeRules entity);

        [ServiceContractDescription(Name = "查询一维条码序号信息(HQL)", Desc = "查询一维条码序号信息(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCBarCodeRulesByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCBarCodeRules>", ReturnType = "ListSCBarCodeRules")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCBarCodeRulesByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCBarCodeRulesByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询一维条码序号信息", Desc = "通过主键ID查询一维条码序号信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCBarCodeRulesById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCBarCodeRules>", ReturnType = "SCBarCodeRules")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCBarCodeRulesById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCBarCodeRulesById(long id, string fields, bool isPlanish);
        #endregion

        #region GKSampleRequestForm

        [ServiceContractDescription(Name = "新增GK_SampleRequestForm", Desc = "新增GK_SampleRequestForm", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddGKSampleRequestFormAndDtl", Get = "", Post = "GKSampleRequestForm", Return = "BaseResultDataValue", ReturnType = "GKSampleRequestForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddGKSampleRequestFormAndDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddGKSampleRequestFormAndDtl(GKSampleRequestForm entity, IList<SCRecordDtl> dtlEntityList, long empID, string empName);

        [ServiceContractDescription(Name = "修改GK_SampleRequestFormAndDtl指定的属性", Desc = "修改GK_SampleRequestFormAndDtl指定的属性", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateGKSampleRequestFormAndDtlByField", Get = "", Post = "GKSampleRequestForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateGKSampleRequestFormAndDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateGKSampleRequestFormAndDtlByField(GKSampleRequestForm entity, string fields, IList<SCRecordDtl> dtlEntityList, long empID, string empName);

        [ServiceContractDescription(Name = "新增GK_SampleRequestForm", Desc = "新增GK_SampleRequestForm", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddGKSampleRequestForm", Get = "", Post = "GKSampleRequestForm", Return = "BaseResultDataValue", ReturnType = "GKSampleRequestForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddGKSampleRequestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddGKSampleRequestForm(GKSampleRequestForm entity);

        [ServiceContractDescription(Name = "修改GK_SampleRequestForm", Desc = "修改GK_SampleRequestForm", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateGKSampleRequestForm", Get = "", Post = "GKSampleRequestForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateGKSampleRequestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateGKSampleRequestForm(GKSampleRequestForm entity);

        [ServiceContractDescription(Name = "修改GK_SampleRequestForm指定的属性", Desc = "修改GK_SampleRequestForm指定的属性", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateGKSampleRequestFormByField", Get = "", Post = "GKSampleRequestForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateGKSampleRequestFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateGKSampleRequestFormByField(GKSampleRequestForm entity, string fields, long empID, string empName);

        [ServiceContractDescription(Name = "删除GK_SampleRequestForm", Desc = "删除GK_SampleRequestForm", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_DelGKSampleRequestForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelGKSampleRequestForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelGKSampleRequestForm(long id);

        [ServiceContractDescription(Name = "查询GK_SampleRequestForm", Desc = "查询GK_SampleRequestForm", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestForm", Get = "", Post = "GKSampleRequestForm", Return = "BaseResultList<GKSampleRequestForm>", ReturnType = "ListGKSampleRequestForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchGKSampleRequestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchGKSampleRequestForm(GKSampleRequestForm entity);

        [ServiceContractDescription(Name = "查询GK_SampleRequestForm(HQL)", Desc = "查询GK_SampleRequestForm(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GKSampleRequestForm>", ReturnType = "ListGKSampleRequestForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchGKSampleRequestFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchGKSampleRequestFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询GK_SampleRequestForm", Desc = "通过主键ID查询GK_SampleRequestForm", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GKSampleRequestForm>", ReturnType = "GKSampleRequestForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchGKSampleRequestFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchGKSampleRequestFormById(long id, string fields, bool isPlanish);


        [ServiceContractDescription(Name = "查询GK_SampleRequestForm及DtlList(HQL)", Desc = "查询GK_SampleRequestForm及DtlList(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormAndDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GKSampleRequestForm>", ReturnType = "ListGKSampleRequestForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchGKSampleRequestFormAndDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchGKSampleRequestFormAndDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询GK_SampleRequestForm及DtlList", Desc = "通过主键ID查询GK_SampleRequestForm及DtlList", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormAndDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GKSampleRequestForm>", ReturnType = "GKSampleRequestForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchGKSampleRequestFormAndDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchGKSampleRequestFormAndDtlById(long id, string fields, bool isPlanish);

        #endregion

        #region GKDeptAutoCheckLink

        [ServiceContractDescription(Name = "新增记录项类型与记录项字典关系", Desc = "新增记录项类型与记录项字典关系", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddGKDeptAutoCheckLink", Get = "", Post = "GKDeptAutoCheckLink", Return = "BaseResultDataValue", ReturnType = "GKDeptAutoCheckLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddGKDeptAutoCheckLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_AddGKDeptAutoCheckLink(GKDeptAutoCheckLink entity);

        [ServiceContractDescription(Name = "修改记录项类型与记录项字典关系", Desc = "修改记录项类型与记录项字典关系", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateGKDeptAutoCheckLink", Get = "", Post = "GKDeptAutoCheckLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateGKDeptAutoCheckLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateGKDeptAutoCheckLink(GKDeptAutoCheckLink entity);

        [ServiceContractDescription(Name = "修改记录项类型与记录项字典关系指定的属性", Desc = "修改记录项类型与记录项字典关系指定的属性", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_UpdateGKDeptAutoCheckLinkByField", Get = "", Post = "GKDeptAutoCheckLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_UpdateGKDeptAutoCheckLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_UpdateGKDeptAutoCheckLinkByField(GKDeptAutoCheckLink entity, string fields);

        [ServiceContractDescription(Name = "删除记录项类型与记录项字典关系", Desc = "删除记录项类型与记录项字典关系", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_DelGKDeptAutoCheckLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WA_UDTO_DelGKDeptAutoCheckLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_DelGKDeptAutoCheckLink(long id);

        [ServiceContractDescription(Name = "查询记录项类型与记录项字典关系", Desc = "查询记录项类型与记录项字典关系", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKDeptAutoCheckLink", Get = "", Post = "GKDeptAutoCheckLink", Return = "BaseResultList<GKDeptAutoCheckLink>", ReturnType = "ListGKDeptAutoCheckLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchGKDeptAutoCheckLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchGKDeptAutoCheckLink(GKDeptAutoCheckLink entity);

        [ServiceContractDescription(Name = "查询记录项类型与记录项字典关系(HQL)", Desc = "查询记录项类型与记录项字典关系(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKDeptAutoCheckLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GKDeptAutoCheckLink>", ReturnType = "ListGKDeptAutoCheckLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchGKDeptAutoCheckLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchGKDeptAutoCheckLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询记录项类型与记录项字典关系", Desc = "通过主键ID查询记录项类型与记录项字典关系", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKDeptAutoCheckLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GKDeptAutoCheckLink>", ReturnType = "GKDeptAutoCheckLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchGKDeptAutoCheckLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchGKDeptAutoCheckLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region 业务接口对照
        [ServiceContractDescription(Name = "查询业务接口对照关系信息(HQL)", Desc = "查询业务接口对照关系信息(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchBDictMapingVOByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&deveCode={deveCode}&useCode={useCode}&mapingWhere={mapingWhere}&objectTypeId={objectTypeId}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&deveCode={deveCode}&useCode={useCode}&mapingWhere={mapingWhere}&objectTypeId={objectTypeId}", Post = "", Return = "BaseResultList<BDictMapingVO>", ReturnType = "ListBDictMapingVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WebAssistManageService?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&deveCode={deveCode}&useCode={useCode}&mapingWhere={mapingWhere}&objectTypeId={objectTypeId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchBDictMapingVOByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, string deveCode, string useCode, string mapingWhere, long objectTypeId);

        #endregion

        #region PDF清单报表

        [ServiceContractDescription(Name = "获取公共模板目录的子文件夹中的所有报表模板文件", Desc = "获取公共模板目录的子文件夹中的所有报表模板文件", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchPublicTemplateFileInfoByType?publicTemplateDir={publicTemplateDir}", Get = "publicTemplateDir={publicTemplateDir}", Post = "", Return = "BaseResultList<JObject>", ReturnType = "ListJObject")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchPublicTemplateFileInfoByType?publicTemplateDir={publicTemplateDir}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchPublicTemplateFileInfoByType(string publicTemplateDir);

        [ServiceContractDescription(Name = "将选择的公共报表模板新增保存到当前实验室的报表模板表", Desc = "将选择的公共报表模板新增保存到当前实验室的报表模板表", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_AddBTemplateOfPublicTemplate", Get = "", Post = "JObject", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_AddBTemplateOfPublicTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool WA_UDTO_AddBTemplateOfPublicTemplate(string entityList, long labId, string labCName);

        [ServiceContractDescription(Name = "获取当前机构的某一模板类型的全部报表模板信息,如果当前机构未维护,取该模板类型的公共报表模板信息", Desc = "获取当前机构的某一模板类型的全部报表模板信息,如果当前机构未维护,取该模板类型的公共报表模板信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchBTemplateByLabIdAndType?labId={labId}&breportType={breportType}&publicTemplateDir={publicTemplateDir}", Get = "labId={labId}&breportType={breportType}&publicTemplateDir={publicTemplateDir}", Post = "", Return = "BaseResultList<BTemplate>", ReturnType = "ListBTemplate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchBTemplateByLabIdAndType?labId={labId}&breportType={breportType}&publicTemplateDir={publicTemplateDir}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchBTemplateByLabIdAndType(long labId, long breportType, string publicTemplateDir);

        [ServiceContractDescription(Name = "获取各业务报表(如采购申请,订货清单等)PDF文件", Desc = "获取各业务报表(如采购申请,订货清单等)PDF文件", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchBusinessReportOfPdfById?reaReportClass={reaReportClass}&breportType={breportType}&id={id}&operateType={operateType}&frx={frx}", Get = "reaReportClass={reaReportClass}&breportType={breportType}&id={id}&operateType={operateType}&frx={frx}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchBusinessReportOfPdfById?reaReportClass={reaReportClass}&breportType={breportType}&id={id}&operateType={operateType}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream WA_UDTO_SearchBusinessReportOfPdfById(string reaReportClass, string breportType, string id, long operateType, string frx);

        [ServiceContractDescription(Name = "获院感登记报表PDF文件", Desc = "获院感登记报表PDF文件", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormOfPdfByHQL?operateType={operateType}&breportType={breportType}&groupType={groupType}&where={where}&sort={sort}&frx={frx}&docVO={docVO}", Get = "operateType={operateType}&breportType={breportType}&groupType={groupType}&where={where}&sort={sort}&frx={frx}&docVO={docVO}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchGKSampleRequestFormOfPdfByHQL?operateType={operateType}&breportType={breportType}&groupType={groupType}&where={where}&sort={sort}&frx={frx}&docVO={docVO}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream WA_UDTO_SearchGKSampleRequestFormOfPdfByHQL(long operateType, string breportType, string groupType, string docVO, string where, string sort, string frx);

        #endregion

        #region Excel导出
        [ServiceContractDescription(Name = "获取各业务报表(如采购申请,订货清单等)Excel导出文件", Desc = "获取各业务报表(如采购申请,订货清单等)Excel导出文件", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchBusinessReportOfExcelById?operateType={operateType}&breportType={breportType}&id={id}&frx={frx}", Get = "operateType={operateType}&breportType={breportType}&id={id}&frx={frx}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchBusinessReportOfExcelById?operateType={operateType}&breportType={breportType}&id={id}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream WA_UDTO_SearchBusinessReportOfExcelById(long operateType, string breportType, long id, string frx);

        [ServiceContractDescription(Name = "获院感登记报表,导出Excel文件", Desc = "获院感登记报表,导出Excel文件", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKSampleRequestFormOfExcelByHql?operateType={operateType}&breportType={breportType}&groupType={groupType}&where={where}&sort={sort}&frx={frx}&docVO={docVO}", Get = "operateType={operateType}&breportType={breportType}&groupType={groupType}&where={where}&sort={sort}&frx={frx}&docVO={docVO}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchGKSampleRequestFormOfExcelByHql?operateType={operateType}&breportType={breportType}&groupType={groupType}&where={where}&sort={sort}&frx={frx}&docVO={docVO}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream WA_UDTO_SearchGKSampleRequestFormOfExcelByHql(long operateType, string breportType, string groupType, string docVO, string where, string sort, string frx);

        #endregion

        #region 统计报表
        [ServiceContractDescription(Name = "院感统计--按科室统计表(HQL)", Desc = "院感统计--按科室统计表(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKListByHQLInfectionOfDept?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&docVO={docVO}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&docVO={docVO}", Post = "", Return = "BaseResultList<GKOfDeptEvaluation>", ReturnType = "ListGKOfDeptEvaluation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchGKListByHQLInfectionOfDept?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&docVO={docVO}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchGKListByHQLInfectionOfDept(int page, int limit, string fields, string docVO, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "院感统计--按季度统计表(HQL)", Desc = "院感统计--按季度统计表(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKListByHQLInfectionOfQuarterly?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&docVO={docVO}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&docVO={docVO}", Post = "", Return = "BaseResultList<GKOfDeptEvaluation>", ReturnType = "ListGKOfDeptEvaluation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchGKListByHQLInfectionOfQuarterly?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&docVO={docVO}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchGKListByHQLInfectionOfQuarterly(int page, int limit, string fields, string docVO, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "院感统计--评价报告表(HQL)", Desc = "院感统计--评价报告表(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchGKListByHQLInfectionOfEvaluation?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&docVO={docVO}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&docVO={docVO}", Post = "", Return = "BaseResultList<GKOfDeptEvaluation>", ReturnType = "ListGKOfDeptEvaluation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchGKListByHQLInfectionOfEvaluation?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&docVO={docVO}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchGKListByHQLInfectionOfEvaluation(int page, int limit, string fields, string docVO, string where, string sort, bool isPlanish);

        #endregion

        #region 双列表选择左查询定制服务

        [ServiceContractDescription(Name = "科室自动核收关系维护时,获取待选择的科室信息(HQL)", Desc = "科室自动核收关系维护时,获取待选择的科室信息(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchDepartmentByAutoCheckLinkHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Department>", ReturnType = "ListDepartment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchDepartmentByAutoCheckLinkHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchDepartmentByAutoCheckLinkHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish);


        [ServiceContractDescription(Name = "记录项类型选择记录项字典时,获取待选择的记录项字典信息(HQL)", Desc = "记录项类型选择记录项字典时,获取待选择的记录项字典信息(HQL)", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_SearchSCRecordTypeItemByLinkHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BloodChargeItem>", ReturnType = "ListBloodChargeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_SearchSCRecordTypeItemByLinkHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_SearchSCRecordTypeItemByLinkHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish);

        #endregion

        #region 定制服务

        [ServiceContractDescription(Name = "登录成功后,获取院感待处理的预警提示信息", Desc = "登录成功后,获取院感待处理的预警提示信息", Url = "ServerWCF/WebAssistManageService.svc/WA_UDTO_GetGKWarningAlertInfo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WA_UDTO_GetGKWarningAlertInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WA_UDTO_GetGKWarningAlertInfo();

        #endregion

    }
}
