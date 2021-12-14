using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AopAlliance.Intercept;
using Spring.Aop;
using System.Reflection;
using Spring.Context;
using ZhiFang.Digitlab.IBLL.RBAC;
using System.Web;
using Spring.Context.Support;
using ZhiFang.Common.Public;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Common.Log;

namespace ZhiFang.Digitlab.Interceptor.ReagentSys
{
    public class InterceptorLog : IMethodInterceptor  
    {
        ZhiFang.Digitlab.IBLL.RBAC.IBRBACModule IBRBACModule { get; set; }
        ZhiFang.Digitlab.IBLL.RBAC.IBRBACRoleRight IBRBACRoleRight { get; set; }
        public bool JudgeRABCModuleAccessRight(IMethodInvocation invocation)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            IBRBACModule = (ZhiFang.Digitlab.IBLL.RBAC.IBRBACModule)context.GetObject("BRBACModule");
            IBRBACRoleRight = (ZhiFang.Digitlab.IBLL.RBAC.IBRBACRoleRight)context.GetObject("BRBACRoleRight");

            bool tempResult = false;
            string methodName = invocation.Method.Name;
            System.Collections.Specialized.NameValueCollection Judgeheaders = HttpContext.Current.Request.Headers;
            /*为了方便调试暂时注释，正式使用时开启
            if (Judgeheaders["User-Agent-Zip-Fa"] == null)//检测是否通过前台UI框架登陆6D0F3E94-B672-4BFD-B614-00398C73447D
            {
                return false;
            }
            */

            if (methodName.IndexOf("RBAC_BA_Login") >= 0)//登陆不判断权限
            {
                return true;
            }

            string strUserAccount = SessionHelper.GetSessionValue(DicCookieSession.UserAccount);
            if (!string.IsNullOrEmpty(strUserAccount) && strUserAccount == DicCookieSession.SuperUser)
            {
                return true;
            }
            string strEmployeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string strOldModuleID = SessionHelper.GetSessionValue(DicCookieSession.OldModuleID);//缓存的模块ID
            string strCurModuleID = Cookie.CookieHelper.Read(DicCookieSession.CurModuleID);     //新请求的模块ID
            if (strEmployeeID == "")//判断是否登陆
            {
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(strCurModuleID))//判断是否有模块ID
                {
                    return false;
                }
                if (strCurModuleID == DicCookieSession.DefaultModule)//判断是否是控制台模块
                {
                    return true;
                }
                if (strOldModuleID != strCurModuleID)
                {
                    SessionHelper.SetSessionValue(DicCookieSession.OldModuleID, strCurModuleID);
                    IList<RBACModule> tempRBACModuleList = IBRBACModule.SearchModuleByHREmpIDAndModuleID(Int64.Parse(strEmployeeID), Int64.Parse(strCurModuleID));
                    return (tempRBACModuleList == null || tempRBACModuleList.Count == 0) ? false : true;
                }
                else
                {
                    return true;
                }
            }
            #region
            /*
            //if (methodName == "RBAC_BA_Login" ||
            //    methodName == "RBAC_RJ_CheckEmpModuleRight" ||
            //    methodName == "RBAC_UDTO_SearchRBACModuleToTree")
            //{
            //    tempResult = true;
            //}
            //else
            //{
                string strEmployeeID = SessionHelper.GetSessionValue("000200");
                string strOldModuleID = SessionHelper.GetSessionValue("000560");//缓存的模块ID
                string strCurModuleID = Cookie.CookieHelper.Read("000500");     //当前访问的模块ID
                if (string.IsNullOrEmpty(strCurModuleID))
                {
                    tempResult = false;
                }
                else if (strCurModuleID == "608EE9C7CA151681C73")//不用判断权限的服务
                {
                    tempResult = true;
                }
                else
                {
                    if (strOldModuleID != strCurModuleID)
                    {
                        strOldModuleID = strCurModuleID;
                        string strModuleName = Cookie.CookieHelper.Read("000501");
                        IList<RBACModule> tempRBACModuleList = IBRBACModule.SearchModuleByHREmpIDAndModuleID(Int64.Parse(strEmployeeID), Int64.Parse(strCurModuleID));
                        tempResult = (tempRBACModuleList == null || tempRBACModuleList.Count == 0) ? false : true;
                        //Dictionary<string, string> tempRBACModuleDic = (Dictionary<string, string>)SessionHelper.GetSessionObjectValue("000560");
                        //tempResult = (tempRBACModuleDic == null || tempRBACModuleDic.Count == 0 || !tempRBACModuleDic.ContainsKey(strCurModuleID)) ? false : true;
                    }
                }

                string strOldModuleOperID = SessionHelper.GetSessionValue("000660");//缓存的模块操作ID
                string strCurModuleOperID = Cookie.CookieHelper.Read("000600");     //当前访问的模块操作ID
                if (!string.IsNullOrEmpty(strCurModuleOperID))
                {
                    if (strOldModuleOperID != strCurModuleOperID)
                    {
                        strOldModuleOperID = strCurModuleOperID;
                        string strModuleOperName = Cookie.CookieHelper.Read("000601");
                        tempResult = IBRBACRoleRight.JudgeRBACRoleRightByHREmpIDAndModuleOperID(Int64.Parse(strEmployeeID), Int64.Parse(strCurModuleOperID));
                        //Dictionary<string, string> tempRBACModuleOperDic = (Dictionary<string, string>)SessionHelper.GetSessionObjectValue("000560");
                        //tempResult = (tempRBACModuleOperDic == null || tempRBACModuleOperDic.Count == 0 || !tempRBACModuleOperDic.ContainsKey(strCurModuleOperID)) ? false : true;
                    }
                }
            //}           
            return tempResult;
            */
            #endregion
        }
        public bool JudgeRABCModuleOperaterAccessRight(IMethodInvocation invocation)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            object drrh = context.GetObject("DataRowRoleHQL");
            ((DataRowRoleHQL)drrh).Hql = "";
            string strUserAccount = SessionHelper.GetSessionValue(DicCookieSession.UserAccount);
            if (!string.IsNullOrEmpty(strUserAccount) && strUserAccount == DicCookieSession.SuperUser)
            {
                return true;
            }
            string strEmployeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string strOldModuleOperID = SessionHelper.GetSessionValue(DicCookieSession.OldModuleOperID);//缓存的模块操作ID
            string strCurModuleOperID = Cookie.CookieHelper.Read(DicCookieSession.CurModuleOperID);     //新请求的模块操作ID
            if (string.IsNullOrEmpty(strCurModuleOperID))//判断前台是否控制操作权限如为空则不判断操作权限
            {
                return true;
            }
            if (strOldModuleOperID != strCurModuleOperID)
            {
                SessionHelper.SetSessionValue(DicCookieSession.OldModuleOperID, strCurModuleOperID);
            }
            Cookie.CookieHelper.Write(DicCookieSession.CurModuleOperID, "");
            return IBRBACRoleRight.JudgeRBACRoleRightByHREmpIDAndModuleOperID(Int64.Parse(strEmployeeID), Int64.Parse(strCurModuleOperID));

            //else
            //{
            //    Cookie.CookieHelper.Write(DicCookieSession.CurModuleOperID, "");
            //    return IBRBACRoleRight.JudgeRBACRoleRightByHREmpIDAndModuleOperID(Int64.Parse(strEmployeeID), Int64.Parse(strCurModuleOperID));
            //    //return true;
            //}
        }
        public object Invoke(IMethodInvocation invocation)
        {
            bool CheckModuleAccess = JudgeRABCModuleAccessRight(invocation);
            bool CheckModuleOperaterAccess = JudgeRABCModuleOperaterAccessRight(invocation);
            if (CheckModuleAccess && CheckModuleOperaterAccess)
            {
                return invocation.Proceed();
            }
            else
            {
                HttpContext.Current.Response.Write("{ErrorInfo:\"无此功能权限！\",success:\"false\",ResultDataFormatType:\"JSON\",ResultDataValue:\"\"}");
                return null;
            }
        }
    }

    public class InterceptorAuthorityFilter : IMethodBeforeAdvice
    {
        private string advice = "AuthorityFilter";

        public string Advice
        {
            get { return advice; }
            set { advice = value; }
        }

        public void Before(System.Reflection.MethodInfo method, object[] args, object target)
        {
            #region 记录用户操作日志
            IApplicationContext context = ContextRegistry.GetContext();
            object bslog = context.GetObject("BSLog");
            //object bhremployee = context.GetObject("BHREmployee");
            IBSLog ibslog = (IBSLog)bslog;
            //IBHREmployee ibhremployee = (IBHREmployee)bhremployee;
            Entity.SLog esl = new Entity.SLog();
            esl.Id = Common.Public.GUIDHelp.GetGUIDLong();
            //esl.IP = HttpContext.Current.Request.UserHostAddress;
            #region 请求IP地址
            string ipcount = "";
            var iplist = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).Where(p => p.ToString().IndexOf('.') > 0);
            if (iplist.Count() > 0)
            {
                foreach (var p in iplist)
                {
                    ipcount += p.ToString() + ";";
                }
            }
            esl.IP = ipcount;
            #endregion
            #region 实验室ID
            esl.LabID = -1;
            if (HttpContext.Current.Session["LabID"] != null && HttpContext.Current.Session["LabID"].ToString().Trim() != "")
            {
                esl.LabID = long.Parse(HttpContext.Current.Session["LabID"].ToString());
            }
            #endregion
            #region 员工ID
            esl.EmpID = 1;
            if (HttpContext.Current.Session["HREmployeeID"] != null && HttpContext.Current.Session["HREmployeeID"].ToString().Trim() != "")
            {
                esl.LabID = long.Parse(HttpContext.Current.Session["HREmployeeID"].ToString());
            }
            #endregion
            #region 员工姓名
            esl.EmpName = "无名";
            if (HttpContext.Current.Session["HREmployeeName"] != null && HttpContext.Current.Session["HREmployeeName"].ToString().Trim() != "")
            {
                esl.LabID = long.Parse(HttpContext.Current.Session["HREmployeeName"].ToString());
            }
            #endregion
            esl.DataAddTime = DateTime.Now;
            esl.OperateName = "调用前： 方法名 : " + method.Name;
            esl.OperateType = "调用前： 完整路径 : " + HttpContext.Current.Request.Path;
            esl.InfoLevel = (int)InfoLevel.Normal;
            esl.Comment += "调用前：客户端计算机名：" + System.Net.Dns.GetHostName();
            esl.Comment += "调用前：客户端IP地址：" + esl.IP;
            string para = "";
            if (args != null)
            {
                foreach (object arg in args)
                {
                    if (arg != null)
                    {
                        para += arg.ToString() + "@";
                    }
                }
            }
            esl.Comment += "调用前：方法名 :" + method.Name + ";完整路径 :" + HttpContext.Current.Request.Path + ";目标 :" + target + ";参数:" + para;
            //esl.HREmployee = ibhremployee.Get(1);
            // esl.HREmployee = (ibhremployee.Get(long.Parse(HttpContext.Current.Session["HREmployeeID"].ToString())));
            //ibslog.Entity = esl;
            //ibslog.Add();

            //Log.Info("调用前： 调用的方法名 : " + method.Name);
            //Log.Info("调用前： 目标  : " + target);
            //Log.Info("调用前： 参数为   : ");
            //if (args != null)
            //{
            //    foreach (object arg in args)
            //    {
            //        if (arg != null)
            //        {
            //            Log.Info(arg.ToString());
            //        }
            //    }
            //}
            #endregion
        }
    }

    public class AfterReturningAdvice : IAfterReturningAdvice
    {
        public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
        {
            #region 记录用户操作日志
            IApplicationContext context = ContextRegistry.GetContext();
            object bslog = context.GetObject("BSLog");
            //object bhremployee = context.GetObject("BHREmployee");
            IBSLog ibslog = (IBSLog)bslog;
            //IBHREmployee ibhremployee = (IBHREmployee)bhremployee;
            Entity.SLog esl = new Entity.SLog();
            esl.Id = Common.Public.GUIDHelp.GetGUIDLong();
            //esl.IP = HttpContext.Current.Request.UserHostAddress;
            #region 请求IP地址
            string ipcount = "";
            var iplist = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).Where(p => p.ToString().IndexOf('.') > 0);
            if (iplist.Count() > 0)
            {
                foreach (var p in iplist)
                {
                    ipcount += p.ToString() + ";";
                }
            }
            esl.IP = ipcount;
            #endregion
            #region 实验室ID
            esl.LabID = -1;
            if (HttpContext.Current.Session["LabID"] != null && HttpContext.Current.Session["LabID"].ToString().Trim() != "")
            {
                esl.LabID = long.Parse(HttpContext.Current.Session["LabID"].ToString());
            }
            #endregion
            #region 员工ID
            esl.EmpID = 1;
            if (HttpContext.Current.Session["HREmployeeID"] != null && HttpContext.Current.Session["HREmployeeID"].ToString().Trim() != "")
            {
                esl.LabID = long.Parse(HttpContext.Current.Session["HREmployeeID"].ToString());
            }
            #endregion
            #region 员工姓名
            esl.EmpName = "无名";
            if (HttpContext.Current.Session["HREmployeeName"] != null && HttpContext.Current.Session["HREmployeeName"].ToString().Trim() != "")
            {
                esl.LabID = long.Parse(HttpContext.Current.Session["HREmployeeName"].ToString());
            }
            #endregion
            esl.DataAddTime = DateTime.Now;
            esl.OperateName = "调用后： 方法调用成功，方法名 : " + method.Name;
            esl.OperateType = "调用后： 完整路径 : " + HttpContext.Current.Request.Path;
            esl.InfoLevel = (int)InfoLevel.Normal;
            esl.Comment += "调用后：客户端计算机名：" + System.Net.Dns.GetHostName();
            esl.Comment += "调用后：客户端IP地址：" + esl.IP;
            string para = "";
            if (args != null)
            {
                foreach (object arg in args)
                {
                    if (arg != null)
                    {
                        para += arg.ToString() + "@";
                    }
                }
            }
            esl.Comment += "调用后：方法名 :" + method.Name + ";完整路径 :" + HttpContext.Current.Request.Path + ";目标 :" + target + ";参数:" + para + ";返回值是 : " + returnValue;
            //esl.HREmployee = ibhremployee.Get(1);
            // esl.HREmployee = (ibhremployee.Get(long.Parse(HttpContext.Current.Session["HREmployeeID"].ToString())));
            //ibslog.Entity = esl;
            //ibslog.Add();

            //Log.Info("后置通知： 方法调用成功，方法名 : " + method.Name);
            //Log.Info("后置通知： 目标为 : " + target);
            //Log.Info("后置通知： 参数 : ");
            //if (args != null)
            //{
            //    foreach (object arg in args)
            //    {
            //        if (arg != null)
            //        {
            //            Log.Info(arg.ToString());
            //        }
            //    }
            //}
            //Log.Info("后置通知：  返回值是 : " + returnValue);
            #endregion
        }
    }

    public class ExceptAdvice : IThrowsAdvice
    {
        public void AfterThrowing(MethodInfo method, Object[] args, Object target, Exception ex)
        {

            string errorMsg = string.Format("异常通知： 方法抛出的异常 : {0}", ex.Message);
            Log.Info("异常通知： 方法调用异常，方法名 : " + method.Name);
            Log.Info("异常通知： 目标为 : " + target);
            Log.Info("异常通知： 参数 : ");
            if (args != null)
            {
                foreach (object arg in args)
                {
                    if (arg != null)
                    {
                        Log.Info(arg.ToString());
                    }
                }
            }
            Log.Info(errorMsg);
        }
    }

    public class RABCMethodBeforeAdvice : IMethodBeforeAdvice
    {
        public void Before(System.Reflection.MethodInfo method, object[] args, object target)
        {
            bool tempFlag = true;
            string methodName = method.Name;
            //从Session中获取
            object tempRBACModuleOperList = SessionHelper.GetSessionObjectValue("RBACModuleOperList");

            if (tempRBACModuleOperList != null)
            {
                //BaseResultList<RBACModuleOper> tempList = (BaseResultList<RBACModuleOper>)tempRBACModuleOperList;
                //foreach (RBACModuleOper tempRBACModuleOper in tempList.list.list)
                //{
                //    //if (tempRBACModuleOper.OperateURLtempType.IndexOf(methodName)>=0)
                //    if (tempRBACModuleOper.OperateURL == methodName)
                //    {
                //        tempFlag = true;
                //        break;
                //    }
                //}
                List<string> tempList = (List<string>)tempRBACModuleOperList;
                foreach (string tempOperateURL in tempList)
                {
                    //if (tempOperateURL.IndexOf(methodName)>=0) //注意tempOperateURL=null
                    if (tempOperateURL == methodName)
                    {
                        tempFlag = true;
                        break;
                    }
                }
                if (!tempFlag)
                {
                    throw new Exception("该用户没有执行服务" + method.Name + "的权限！");
                }
            }
            //else
            //{
            //    throw new Exception("无法获取用户执行服务" + method.Name + "的权限！");
            //}
        }
    }

    public class LoginValidationAdvice : IMethodInterceptor  
    {
        public object Invoke(IMethodInvocation invocation)
        {  
            string methodName = invocation.Method.Name;
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string employeeName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            if (string.IsNullOrEmpty(employeeID) && string.IsNullOrEmpty(employeeName))
            {
                Log.Info("方法：" + methodName + "从Session中获取用户ID和名称失败！");
                HttpContext.Current.Response.Write("{Code:\"" + ((int)CodeValue.无法从Session中获取用户ID和名称).ToString()+ "\",ErrorInfo:\"登录过期，请重新登录系统！\",success:\"false\",ResultDataFormatType:\"JSON\",ResultDataValue:\"\"}");
                return null;
            }
            else
            {
                return invocation.Proceed();
            }
        }
            
        
    }

    public class UpdateDataBaseAdvice : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            if (invocation.Method.Name.IndexOf("RBAC_BA_Login") >= 0)
                ZhiFang.DBUpdate.PM.DBUpdate.DataBaseUpdate("");
            return invocation.Proceed();
        }
    }
}
