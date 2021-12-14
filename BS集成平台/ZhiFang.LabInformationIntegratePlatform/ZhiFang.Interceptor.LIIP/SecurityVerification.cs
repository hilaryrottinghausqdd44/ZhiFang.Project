using AopAlliance.Intercept;
using Spring.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using ZhiFang.LIIP.Common;

namespace ZhiFang.Interceptor.LIIP
{
    public class SecurityVerificationBefore : IMethodInterceptor
    {
        //private const string StrRegex = @"<[^>]+?style=[\w]+?:expression\(|\b(alert|confirm|prompt)\b|^\+/v(8|9)|<[^>]*?=[^>]*?&#[^>]*?>|\b(and|or)\b.{1,6}?()|/\*.+?\*/|<\s*script\b|<\s*img\b|\bEXEC\b|UNION.+?SELECT|UPDATE.+?SET|INSERT\s+INTO.+?VALUES|(SELECT|DELETE).+?FROM|(CREATE|ALTER|DROP|TRUNCATE)\s+(TABLE|DATABASE)";
       
        public object Invoke(IMethodInvocation invocation)
        {
            bool VerifyResult = CheckXSS(invocation) && true;
            if (VerifyResult)
            {
                return invocation.Proceed();
            }
            else
            {
                ZhiFang.Common.Log.Log.Error($"SecurityVerification.发现调用服务:{invocation.Method.Name}.参数存在非法字符!IP:{ZhiFang.LIIP.Common.IPHelper.GetClientIP()}");
                return new Entity.Base.BaseResultDataValue() { ErrorInfo = "参数存在非法字符！", success = false};
            }
        }

        private bool CheckXSS(IMethodInvocation invocation)
        {
            List<string> IPList = this.IPWriteList();
            List<string> ServiceList = this.ServiceWriteList();
            string ip = ZhiFang.LIIP.Common.IPHelper.GetClientIP();
            if (IPList.Contains(ip))
                return true;
            if (ServiceList.Contains(invocation.Method.Name))
                return true;

            if (invocation.Arguments != null && invocation.Arguments.Length > 0)
            {
               string inputData= ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(invocation.Arguments);
                //if (Regex.IsMatch(inputData, StrRegex))
                //{
                //    return true;
                //}
                //else
                //{
                //    return false;
                //}
                if (XSSHelper.CheckXSS(inputData))
                    return false;
                else
                    return true;
            }
            return true;
        }

        private List<string> IPWriteList()
        {
            return new List<string>() { };
        }
        private List<string> ServiceWriteList()
        {
            return new List<string>() { };
        }

        
    }

    public class SecurityVerificationAfter : IAfterReturningAdvice
    {
        public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
        {
            bool VerifyResult = FilterXSS(returnValue,method,args,target) && true;
            if (VerifyResult)
            {
               // return invocation.Proceed();
            }
            else
            {
                ZhiFang.Common.Log.Log.Error($"SecurityVerification.发现调用服务:{method.Name}.参数存在非法字符!IP:{ZhiFang.LIIP.Common.IPHelper.GetClientIP()}");
            }

        }
        private bool FilterXSS(object returnValue, MethodInfo method, object[] args, object target)
        {
            List<string> IPList = this.IPWriteList();
            List<string> ServiceList = this.ServiceWriteList();
            string ip = ZhiFang.LIIP.Common.IPHelper.GetClientIP();
            if (IPList.Contains(ip))
                return true;
            if (ServiceList.Contains(method.Name))
                return true;

            if (returnValue != null )
            {
                string inputData = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(returnValue);
                if (XSSHelper.CheckXSS(inputData))
                    return false;
                else
                    return true;
            }
            return true;
        }

        private List<string> IPWriteList()
        {
            return new List<string>() { };
        }
        private List<string> ServiceWriteList()
        {
            return new List<string>() { };
        }


    }
}

