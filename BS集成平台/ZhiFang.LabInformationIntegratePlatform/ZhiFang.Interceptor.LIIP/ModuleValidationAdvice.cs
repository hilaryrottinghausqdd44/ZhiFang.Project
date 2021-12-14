using AopAlliance.Intercept;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZhiFang.Common.Public;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Interceptor.LIIP
{
    public class ModuleValidationAdvice : IMethodInterceptor
    {
        ZhiFang.IBLL.RBAC.IBRBACModule IBRBACModule { get; set; }
        ZhiFang.IBLL.RBAC.IBRBACRoleRight IBRBACRoleRight { get; set; }
        public bool JudgeRBACModuleAccessRight(IMethodInvocation invocation)
        {
            IApplicationContext context = ContextRegistry.GetContext();
            IBRBACModule = (ZhiFang.IBLL.RBAC.IBRBACModule)context.GetObject("BRBACModule");
            IBRBACRoleRight = (ZhiFang.IBLL.RBAC.IBRBACRoleRight)context.GetObject("BRBACRoleRight");

            bool tempResult = false;
            string methodName = invocation.Method.Name;
            NameValueCollection Judgeheaders = HttpContext.Current.Request.Headers;
            /*为了方便调试暂时注释，正式使用时开启*/
            //if (Judgeheaders["User-Agent-Zip-Fa"] == null || Judgeheaders["User-Agent-Zip-Fa"].ToString().Trim() != "6D0F3E94-B672-4BFD-B614-00398C73447D")//检测是否通过前台UI框架登陆
            //{
            //    return false;
            //}


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
                //if (string.IsNullOrEmpty(strCurModuleID))//判断是否有模块ID
                //{
                //    return false;
                //}
                //if (strCurModuleID == DicCookieSession.DefaultModule)//判断是否是控制台模块
                //{
                //    return true;
                //}
                //if (strOldModuleID != strCurModuleID)
                //{
                //    SessionHelper.SetSessionValue(ZhiFang.Entity.RBAC.DicCookieSession.OldModuleID, strCurModuleID);
                //    IList<RBACModule> tempRBACModuleList = IBRBACModule.SearchModuleByHREmpIDAndModuleID(Int64.Parse(strEmployeeID), Int64.Parse(strCurModuleID));
                //    return (tempRBACModuleList == null || tempRBACModuleList.Count == 0) ? false : true;
                //}
                //else
                //{
                return true;
                //}
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
        public bool JudgeRBACModuleOperaterAccessRight(IMethodInvocation invocation)
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
            return true;
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
            //if (ZhiFang.Digitlab.Service.SubscriptionService.ClientCallbackList.Count>0)
            //{
            //    //ZhiFang.Common.Log.Log.Debug(ZhiFang.Digitlab.Service.SubscriptionService.ClientCallbackList[0].SessionId);
            //}
            bool CheckModuleAccess = JudgeRBACModuleAccessRight(invocation);
            bool CheckModuleOperaterAccess = JudgeRBACModuleOperaterAccessRight(invocation);
            if (CheckModuleAccess && CheckModuleOperaterAccess)
            {

                return invocation.Proceed();
            }
            else
            {
                HttpContext.Current.Response.Write("{ErrorInfo:\"无此功能权限！\",success:false ,ResultDataFormatType:\"JSON\",ResultDataValue:\"\"}");
                return null;
            }
        }
    }
}
