using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ZhiFang.WebLis.Class
{
    public class BaseAshx
    { 
        public static bool CheckQueryStringNull(HttpContext context,string querystringname)
        {
            if (context.Request.QueryString[querystringname] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckQueryStringNull_INT(HttpContext context, string querystringname)
        {
            if (context.Request.QueryString[querystringname] != null)
            {
                try
                {
                    Convert.ToInt64(context.Request.QueryString[querystringname].Trim());
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static bool CheckQueryStringNull(HttpContext context, string querystringname, string alertstr, int flag)
        {
            if (context.Request.QueryString[querystringname] != null)
            {
                return true;
            }
            else
            {
                ResposneAlert(context,alertstr, flag);
                return false;
            }
        }
        public static bool CheckQueryString(HttpContext context, string querystringname)
        {
            if (context.Request.QueryString[querystringname] != null && context.Request.QueryString[querystringname].ToString().Trim().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool CheckQueryString(HttpContext context, string querystringname, string alertstr, int flag)
        {
            if (context.Request.QueryString[querystringname] != null && context.Request.QueryString[querystringname].ToString().Trim().Length > 0)
            {
                return true;
            }
            else
            {
                ResposneAlert(context,alertstr, flag);
                return false;
            }
        }
        public static bool CheckQueryString(HttpContext context, string querystringname, string alertstr, string url)
        {
            if (context.Request.QueryString[querystringname] != null && context.Request.QueryString[querystringname].ToString().Trim().Length > 0)
            {
                return true;
            }
            else
            {
                context.Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndRedirect(alertstr, url));
                return false;
            }
        }
        public static void ResposneAlert(HttpContext context, string alertstr, int flag)
        {
            switch (flag)
            {
                case 0: context.Response.Write(ZhiFang.Common.Public.ScriptStr.Alert(alertstr));
                    break;
                case 1: context.Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndClose(alertstr));
                    break;
                default: break;
            }
        }
        public static string ReadQueryString(HttpContext context, string querystringname)
        {
            if (CheckQueryStringNull(context,querystringname))
            {
                return HttpContext.Current.Request.QueryString[querystringname].ToString().Trim().Replace("\'", "\'\'");
            }
            else
            {
                return "";
            }
        }
    }
}
