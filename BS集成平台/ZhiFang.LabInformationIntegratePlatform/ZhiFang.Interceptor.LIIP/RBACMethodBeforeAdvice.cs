using Spring.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Common.Public;

namespace ZhiFang.Interceptor.LIIP
{
    public class RBACMethodBeforeAdvice : IMethodBeforeAdvice
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
}
