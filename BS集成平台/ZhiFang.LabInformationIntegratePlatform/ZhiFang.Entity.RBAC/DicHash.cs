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
        public static string IsLocked = "000702";//锁定标记
        public static string OldUserAccount = "000703";//历史账户名
        public static string OpenedModuleIds = "000704";//历史打开记录
        public static string RememberUser = "000705";//是否记住用户名
        public static string RememberPwd = "000706";//是否记住密码
        public static string UserPwd = "000309";//用户密码只能记录在Session里

        public static string SuperUser = "admin";//超级用户账户名
        public static string SuperUserPwd = "adminzfliip8001";//超级用户账户密码
        public static string SuperUserName = "SuperUser";//超级用户账户名
    }
}