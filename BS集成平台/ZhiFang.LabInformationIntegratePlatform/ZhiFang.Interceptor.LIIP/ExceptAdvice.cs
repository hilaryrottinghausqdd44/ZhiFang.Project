using Spring.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Common.Log;

namespace ZhiFang.Interceptor.LIIP
{
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
}
