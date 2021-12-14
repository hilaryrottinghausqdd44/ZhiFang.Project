using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZhiFang.WebLis.Class
{
    public class BasePage : System.Web.UI.Page
    {
        //初始化
        override protected void OnInit(EventArgs e)
        {
            Session.Timeout = ZhiFang.Common.Public.ConfigHelper.GetConfigInt("SessionCacheTime");
            //验证
            if (!CheckCookies("ZhiFangUser"))
            {
                Response.Redirect(HttpContext.Current.Request.ApplicationPath+"\\"+ "Login.aspx");
            }
        }
        public bool CheckCookies(string cookiesname)
        {
            if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(cookiesname) != string.Empty)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckCookies(string cookiesname, string alertstr)
        {
            if (Request.Cookies[cookiesname] != null && Request.Cookies[cookiesname].Value.ToString().Trim().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckCookies(string cookiesname, string alertstr, int flag)
        {
            if (Request.Cookies[cookiesname] != null && Request.Cookies[cookiesname].Value.ToString().Trim().Length > 0)
            {
                return true;
            }
            else
            {
                ResposneAlert(alertstr, flag);
                return false;
            }
        }
        public bool CheckCookies(string cookiesname, string alertstr, string url)
        {
            if (Request.Cookies[cookiesname] != null && Request.Cookies[cookiesname].Value.ToString().Trim().Length > 0)
            {
                return true;
            }
            else
            {
                Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndRedirect(alertstr, url));
                return false;
            }
        }
        public bool CheckQueryStringNull(string querystringname)
        {
            if (Request.QueryString[querystringname] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckQueryStringNull_INT(string querystringname)
        {
            if (Request.QueryString[querystringname] != null)
            {
                try
                {
                    Convert.ToInt32(Request.QueryString[querystringname].Trim());
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
        public bool CheckQueryStringNull(string querystringname, string alertstr, int flag)
        {
            if (Request.QueryString[querystringname] != null)
            {
                return true;
            }
            else
            {
                ResposneAlert(alertstr, flag);
                return false;
            }
        }
        public bool CheckQueryString(string querystringname)
        {
            if (Request.QueryString[querystringname] != null && Request.QueryString[querystringname].ToString().Trim().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckQueryString(string querystringname, string alertstr, int flag)
        {
            if (Request.QueryString[querystringname] != null && Request.QueryString[querystringname].ToString().Trim().Length > 0)
            {
                return true;
            }
            else
            {
                ResposneAlert(alertstr, flag);
                return false;
            }
        }
        public bool CheckQueryString(string querystringname, string alertstr, string url)
        {
            if (Request.QueryString[querystringname] != null && Request.QueryString[querystringname].ToString().Trim().Length > 0)
            {
                return true;
            }
            else
            {
                Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndRedirect(alertstr, url));
                return false;
            }
        }
        public void ResposneAlert(string alertstr, int flag)
        {
            switch (flag)
            {
                case 0: Response.Write(ZhiFang.Common.Public.ScriptStr.Alert(alertstr));
                    break;
                case 1: Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndClose(alertstr));
                    break;
                default: break;
            }
        }
        public bool CheckPageControlNull(string fromname)
        {
            if (this.Page.FindControl(fromname) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckFormNull(string fromname)
        {
            if (Request.Form[fromname] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool CheckFormNullAndValue(string fromname)
        {
            if (Request.Form[fromname] != null && Request.Form[fromname].Trim().Length > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public string ReadCookies(string cookiesname)
        {
            if (cookiesname != null)
            {
                if (CheckCookies(cookiesname))
                {
                    ZhiFang.Common.Log.Log.Info("5");
                    return HttpContext.Current.Request.Cookies[cookiesname].Value.ToString().Trim();

                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }
        public string ReadQueryString(string querystringname)
        {
            if (CheckQueryStringNull(querystringname))
            {
                return HttpContext.Current.Request.QueryString[querystringname].ToString().Trim().Replace("\'", "\'\'");
            }
            else
            {
                return "";
            }
        }  
        public string ReadForm(string fromname)
        {
            if (CheckFormNull(fromname))
            {
                return HttpContext.Current.Request.Form[fromname].ToString().Trim().Replace("\'", "\'\'");
            }
            else
            {
                return "";
            }
        } 
    }
}
