using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.WeiXin.UserInterface.BackStageManagementSystem.CommonObject 
{
    public class BasePage: ZhiFang.WeiXin.BusinessObject.BasePage
    {
        //初始化
        override protected void OnInit(EventArgs e)
        {
            //验证
            if (!CheckCookies(DicCookieSession.WeiXinAdminFlag))
            {
                Response.Redirect(HttpContext.Current.Request.ApplicationPath + "\\UserInterface\\" + "Login.aspx");
            }
        }

        private bool CheckCookies(string cookiesname)
        {
            if (ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(cookiesname) != string.Empty && ZhiFang.WeiXin.Common.Cookie.CookieHelper.Read(cookiesname).Trim() != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}