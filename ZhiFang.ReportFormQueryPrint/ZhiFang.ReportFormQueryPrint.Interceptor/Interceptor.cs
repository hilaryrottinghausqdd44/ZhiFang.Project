using AopAlliance.Intercept;
using Spring.Aop;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.SessionState;
using ZhiFang.Common.Log;

namespace ZhiFang.ReportFormQueryPrint.Interceptor
{
    public class BeforeAdvice : IMethodInterceptor
    {
        public object Invoke(IMethodInvocation invocation)
        {
            bool CheckModuleAccess = JudgeParam(invocation);
            try
            {
                if (CheckModuleAccess)
                {
                    Log.Info("调用前拦截：方法名 : " + invocation.Method.Name + ",IP=" + GetClientIP());
                    //List<string> NoIpList = new List<string>()
                    //{
                    //    "10.3.12.128"
                    //};
                    //if (NoIpList.Contains(GetClientIP()))
                    //{
                    //    Log.Info("调用前拦截：方法名 : " + invocation.Method.Name + ",拦截黑名单,IP=" + GetClientIP() + "！");
                    //    return null;
                    //}
                    if (invocation.Method.Name == "SelectReport")
                    {
                        List<string> aaa = new List<string>();
                        if (HttpContext.Current.Request.QueryString != null && HttpContext.Current.Request.QueryString.AllKeys != null && HttpContext.Current.Request.QueryString.AllKeys.Length > 0)
                        {
                            HttpContext.Current.Request.QueryString.AllKeys.ToList().ForEach(a =>
                            {
                                aaa.Add(HttpContext.Current.Request.QueryString[a].ToString());
                            });
                        }
                        else
                        {
                            Log.Info("调用前拦截：方法名 : " + invocation.Method.Name + ",IP=" + GetClientIP() + ",无法获取查询参数！");

                        }
                        Log.Info("调用前拦截：方法名 : " + invocation.Method.Name + ",IP=" + GetClientIP() + ",QueryPara=" + string.Join("@", aaa));
                    }
                    #region 拦截验证
                    //    HttpCookie hc =  HttpContext.Current.Request.Cookies.Get("user");
                    //    string cookieValue = "";
                    //    if (hc !=null)
                    //    {
                    //        Log.Debug("调用前拦截：存在cookie");
                    //        cookieValue = hc.Value;
                    //    }
                    //    if (cookieValue != null && cookieValue != "")
                    //    {
                    //        DataTable dt = (DataTable)HttpContext.Current.Session["user"];
                    //        if (dt ==null || dt.Rows.Count <=0)
                    //        {
                    //            Log.Debug("调用前拦截：session没有值");
                    //            HttpContext.Current.Response.Write("{ErrorInfo:\"无此功能权限！\",success:false ,ResultDataFormatType:\"JSON\",ResultDataValue:\"\"}");
                    //            return null;
                    //        }
                    //        if (!cookieValue.Equals(dt.Rows[0]["UserNo"].ToString()))
                    //        {
                    //            Log.Debug("调用前拦截：cookie不匹配");
                    //            HttpContext.Current.Response.Write("{ErrorInfo:\"无此功能权限！\",success:false ,ResultDataFormatType:\"JSON\",ResultDataValue:\"\"}");
                    //            return null;
                    //        }
                    //        Log.Debug("调用前拦截：cookie匹配");
                    //    }
                    //    else {
                    //        Log.Debug("调用前拦截：cookie没有值");
                    //        HttpContext.Current.Response.Write("{ErrorInfo:\"无此功能权限！\",success:false ,ResultDataFormatType:\"JSON\",ResultDataValue:\"\"}");
                    //        return null;
                    //    }
                    #endregion
                    return invocation.Proceed();
                }
                else
                {
                    HttpContext.Current.Response.Write("{ErrorInfo:\"无此功能权限！\",success:false ,ResultDataFormatType:\"JSON\",ResultDataValue:\"\"}");
                    return null;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("调用前拦截：" + e.ToString());
                HttpContext.Current.Response.Write("{ErrorInfo:\"出现异常！\",success:false ,ResultDataFormatType:\"JSON\",ResultDataValue:\"\"}");
                return null;
            }

        }

        private bool JudgeParam(IMethodInvocation invocation)
        {
            return true;
        }

        public static string GetClientIP()
        {
            string IP4Address = String.Empty;

            foreach (IPAddress IPA in Dns.GetHostAddresses(HttpContext.Current.Request.UserHostAddress))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }

            if (IP4Address != String.Empty)
            {
                return IP4Address;
            }

            foreach (IPAddress IPA in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (IPA.AddressFamily.ToString() == "InterNetwork")
                {
                    IP4Address = IPA.ToString();
                    break;
                }
            }
            if (IP4Address.Trim() == "::1")
                IP4Address = "127.0.0.1";
            return IP4Address;
        }
    }
    public class AfterReturningAdvice : IAfterReturningAdvice
    {
        public void AfterReturning(object returnValue, MethodInfo method, object[] args, object target)
        {
            #region 记录用户操作日志

            #region 请求IP地址
            string ipcount = "";
            var iplist = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()).Where(p => p.ToString().IndexOf('.') > 0);
            if (iplist.Count() > 0)
            {
                foreach (var p in iplist)
                {
                    ipcount += p.ToString() + ";";
                }
            }
            SLog esl = new SLog();
            esl.IP = ipcount;
            #endregion
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
            esl.DataAddTime = DateTime.Now;
            esl.OperateName = "调用后： 方法调用成功，方法名 : " + method.Name;
            esl.OperateType = "调用后： 完整路径 : " + HttpContext.Current.Request.Path;
            //esl.InfoLevel = (int)InfoLevel.Normal;
            esl.Comment += "调用后：客户端计算机名：" + System.Net.Dns.GetHostName();
            esl.Comment += "调用后：客户端IP地址：" + esl.IP;
            string para = "";
            if (args != null)
            {
                foreach (object arg in args)
                {
                    if (arg != null)
                    {
                        para += arg.ToString() + "@";
                    }
                }
            }
            esl.Comment += "调用后：方法名 :" + method.Name + ";完整路径 :" + HttpContext.Current.Request.Path + ";目标 :" + target + ";参数:" + para + ";返回值是 : " + returnValue;
            //ZhiFang.Digitlab.Service.
            //esl.HREmployee = ibhremployee.Get(1);
            // esl.HREmployee = (ibhremployee.Get(long.Parse(HttpContext.Current.Session["HREmployeeID"].ToString())));
            //ibslog.Entity = esl;
            //ibslog.Add();

            //Log.Info("后置通知： 方法调用成功，方法名 : " + method.Name);
            //Log.Info("后置通知： 目标为 : " + target);
            //Log.Info("后置通知： 参数 : ");
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
            //Log.Info("后置通知：  返回值是 : " + returnValue);
            #endregion
        }
    }
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
    #region SLog  

    public class SLog
    {
        #region Member Variables

        protected string _operateName;
        protected string _operateType;
        protected string _iP;
        protected int _infoLevel;
        protected string _comment;
        protected long _empID = 0;
        protected string _empName;

        #endregion

        #region Constructors

        public SLog() { }

        #endregion

        #region Public Properties        

        public virtual string OperateName
        {
            get { return _operateName; }
            set
            {
                _operateName = value;
            }
        }



        public virtual string OperateType
        {
            get { return _operateType; }
            set
            {
                _operateType = value;
            }
        }



        public virtual string IP
        {
            get { return _iP; }
            set
            {
                _iP = value;
            }
        }



        public virtual int InfoLevel
        {
            get { return _infoLevel; }
            set { _infoLevel = value; }
        }



        public virtual string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
            }
        }



        public virtual long EmpID
        {
            get { return _empID; }
            set { _empID = value; }
        }



        public virtual string EmpName
        {
            get { return _empName; }
            set { _empName = value; }
        }

        public DateTime DataAddTime { get; internal set; }
        #endregion
    }
    #endregion
}
