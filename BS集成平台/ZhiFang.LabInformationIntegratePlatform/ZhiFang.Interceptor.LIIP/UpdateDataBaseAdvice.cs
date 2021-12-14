using AopAlliance.Intercept;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.Interceptor.LIIP
{
    public class UpdateDataBaseAdvice : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            if (invocation.Method.Name.IndexOf("RBAC_BA_Login") >= 0)
                ZhiFang.DBUpdate.DBUpdate.DataBaseUpdate("");
            return invocation.Proceed();
        }
    }
}
