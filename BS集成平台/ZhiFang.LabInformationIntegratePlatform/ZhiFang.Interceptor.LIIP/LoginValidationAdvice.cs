using AopAlliance.Intercept;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ZhiFang.Common.Log;
using ZhiFang.Common.Public;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.Interceptor.LIIP
{
    public class LoginValidationAdvice : IMethodInterceptor
    {
        /// <summary>
        /// 拦截器所拦截的方法不能是虚方法，必须是实方法或者override方法
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        public object Invoke(IMethodInvocation invocation)
        {
            string methodName = invocation.Method.Name;
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string employeeName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            if (string.IsNullOrEmpty(employeeID) && string.IsNullOrEmpty(employeeName))
            {
                Log.Info("方法：" + methodName + "从Session中获取用户ID和名称失败！");
                HttpContext.Current.Response.Write("{Code:\"1001\",ErrorInfo:\"登录过期，请重新登录系统！\",success:\"false\",ResultDataFormatType:\"JSON\",ResultDataValue:\"\"}");
                return null;
            }
            else
            {
                return invocation.Proceed();
            }
        }
    }
}
