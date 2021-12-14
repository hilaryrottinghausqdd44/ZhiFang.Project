using AopAlliance.Intercept;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ZhiFang.Interceptor.LIIP
{
    public class WeiXinAppDoctAdvice : IMethodInterceptor
    {
        public bool JudgeWeiXinAppDoct(IMethodInvocation invocation)
        {
            return true;
        }
        public object Invoke(IMethodInvocation invocation)
        {
            bool CheckModuleAccess = JudgeWeiXinAppDoct(invocation);
            if (CheckModuleAccess)
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
