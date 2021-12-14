using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AopAlliance.Intercept;
using ZhiFang.Common.Log;
using System.Reflection;

namespace ZhiFang.ProjectProgressMonitorManage.MessagePush
{
    public class MessagePushInterceptor : Spring.Aop.IAfterReturningAdvice
    {
        public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
        {
            try
            {
                Log.Info("消息推送服务： 方法调用成功，方法名 : " + method.Name);
                Log.Info("消息推送服务： 目标为 : " + target);
                Log.Info("消息推送服务： 参数 : ");
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
                Log.Info("消息推送服务：  返回值是 : " + returnValue);

                string message = method.Name;
                message = "{\"ModuleCode\":\"GroupID_100\",\"Message\":\"" + message + "\"}";
                if (message != null && message.Trim().Length > 0)
                {
                    var list = ZhiFang.ProjectProgressMonitorManage.SubscriptionService.ClientCallbackList;
                    if (list == null || list.Count == 0)
                        return ;
                    lock (list)
                    {
                        foreach (var client in list)
                        {
                            // Broadcast
                            client.SendMessage(message);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.Info("消息推送服务异常 : " + e.ToString());
            }
        }
        public object Invoke(IMethodInvocation invocation)
        {
            //string message = invocation.Method.Name;
            //if (message != null && message.Trim().Length > 0)
            //{
            //    var list = ZhiFang.ProjectProgressMonitorManage.SubscriptionService.ClientCallbackList;
            //    if (list == null || list.Count == 0)
            //        return null;
            //    lock (list)
            //    {
            //        foreach (var client in list)
            //        {
            //            // Broadcast
            //            client.SendMessage(message);
            //        }
            //    }

            //}
            return invocation.Proceed();
        }
    }
}