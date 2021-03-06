using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Policy;
using System.Collections;

namespace ZhiFang.Entity.RBAC
{
    public static class DicCookieSession
    {

        public static string EmployeeID = "000200";//员工ID
        public static string EmployeeName = "000201";//员工姓名
        public static string EmployeeUseCode = "000202";//员工代码

        public static string UserID = "000300";//员工账户ID
        public static string UserAccount = "000301";//员工账户名
        public static string UseCode = "000302";//员工代码
        public static string HRDeptID = "000400";//部门ID
        public static string HRDeptName = "000401";//部门名称
        public static string HRDeptCode = "000402";//部门编码
        public static string OldModuleID = "000560";//正在使用中的模块ID
        public static string CurModuleID = "000500";//新请求的模块ID
        public static string ModuleName = "000501";//模块名称
        public static string OldModuleOperID = "000600";//正在使用中的模块操作ID
        public static string ModuleOperName = "000601";//模块操作名称  
        public static string CurModuleOperID = "000660";//新请求的模块操作ID
        public static string DefaultModule = "608EE9C7CA151681C73";//默认模块ID
        public static string IsLocked = "100002";//锁定标记
        public static string OldUserAccount = "100003";//历史账户名
        public static string OpenedModuleIds = "100004";//历史打开记录
        public static string RememberUser = "100005";//是否记住用户名
        public static string RememberPwd = "100006";//是否记住密码

        public static string DoctorId = "200001";//6.6人员帐号绑定的所属医生Id
        public static string DoctorCName = "200002";//6.6人员帐号绑定的所属医生姓名
        public static string GradeId = "200003";//6.6人员帐号绑定的所属医生的等级

        public static string SuperUser = "admin";//超级用户账户名
        public static string SuperUserPwd = "adminzhifangbeijingkeji8001";//超级用户账户密码
        public static string SuperUserName = "SuperUser";//超级用户账户名

        /// <summary>
        /// OpenID用于通知（微信生成）
        /// </summary>
        public static string WeiXinOpenID = "100001";//WeiXinOpenID
        /// <summary>
        /// 微信关注用户列表中的ID（GUID）
        /// </summary>
        public static string WeiXinUserID = "100002";//WeiXinAccountID
        public static string WeiXinAdminFlag = "101010";//WeiXinAccountID

        public static string WeiXinAdminFlagvalue = ZhiFang.Common.Public.SecurityHelp.MD5Encrypt(WeiXinAdminFlag, "ZFWEIXIN");//WeiXinAdminFlagvalue
    }
}