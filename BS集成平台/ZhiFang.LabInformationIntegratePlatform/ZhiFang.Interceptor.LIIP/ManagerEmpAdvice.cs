using AopAlliance.Intercept;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZhiFang.Interceptor.LIIP
{
    public class ManagerEmpAdvice : IMethodInterceptor
    {
        ZhiFang.IBLL.RBAC.IBRBACModule IBRBACModule { get; set; }
        ZhiFang.IBLL.RBAC.IBRBACRoleRight IBRBACRoleRight { get; set; }
        public bool JudgeRBACModuleAccessRight(IMethodInvocation invocation)
        {
            return true;
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
            return true;
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
