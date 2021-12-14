using Spring.Aop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZhiFang.Interceptor.LIIP
{
    public class AuthenticationFilter : IMethodBeforeAdvice
    {
        private string advice = "AuthorityFilter";

        public string Advice
        {
            get { return advice; }
            set { advice = value; }
        }

        public void Before(System.Reflection.MethodInfo method, object[] args, object target)
        {
            //#region 记录用户操作日志
            //IApplicationContext context = ContextRegistry.GetContext();
            //object bslog = context.GetObject("BSLog");
            ////object bhremployee = context.GetObject("BHREmployee");
            //IBSLog ibslog = (IBSLog)bslog;
            ////IBHREmployee ibhremployee = (IBHREmployee)bhremployee;
            //SLog esl = new SLog();
            //esl.Id = ZhiFang.WeiXin.Common.GUIDHelp.GetGUIDLong();
            ////esl.IP = HttpContext.Current.Request.UserHostAddress;
            //#region 请求IP地址
            //string ipcount = "";
            //var iplist = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).Where(p => p.ToString().IndexOf('.') > 0);
            //if (iplist.Count() > 0)
            //{
            //    foreach (var p in iplist)
            //    {
            //        ipcount += p.ToString() + ";";
            //    }
            //}
            //esl.IP = ipcount;
            //#endregion
            //#region 实验室ID
            //esl.LabID = -1;
            //if (HttpContext.Current.Session["LabID"] != null && HttpContext.Current.Session["LabID"].ToString().Trim() != "")
            //{
            //    esl.LabID = long.Parse(HttpContext.Current.Session["LabID"].ToString());
            //}
            //#endregion
            //#region 员工ID
            //esl.EmpID = 1;
            //if (HttpContext.Current.Session["HREmployeeID"] != null && HttpContext.Current.Session["HREmployeeID"].ToString().Trim() != "")
            //{
            //    esl.LabID = long.Parse(HttpContext.Current.Session["HREmployeeID"].ToString());
            //}
            //#endregion
            //#region 员工姓名
            //esl.EmpName = "无名";
            //if (HttpContext.Current.Session["HREmployeeName"] != null && HttpContext.Current.Session["HREmployeeName"].ToString().Trim() != "")
            //{
            //    esl.LabID = long.Parse(HttpContext.Current.Session["HREmployeeName"].ToString());
            //}
            //#endregion
            //esl.DataAddTime = DateTime.Now;
            //esl.OperateName = "调用前： 方法名 : " + method.Name;
            //esl.OperateType = "调用前： 完整路径 : " + HttpContext.Current.Request.Path;
            //esl.InfoLevel = (int)InfoLevel.Normal;
            //esl.Comment += "调用前：客户端计算机名：" + System.Net.Dns.GetHostName();
            //esl.Comment += "调用前：客户端IP地址：" + esl.IP;
            //string para = "";
            //if (args != null)
            //{
            //    foreach (object arg in args)
            //    {
            //        if (arg != null)
            //        {
            //            para += arg.ToString() + "@";
            //        }
            //    }
            //}
            //esl.Comment += "调用前：方法名 :" + method.Name + ";完整路径 :" + HttpContext.Current.Request.Path + ";目标 :" + target + ";参数:" + para;
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
            //#endregion
        }
    }
}
