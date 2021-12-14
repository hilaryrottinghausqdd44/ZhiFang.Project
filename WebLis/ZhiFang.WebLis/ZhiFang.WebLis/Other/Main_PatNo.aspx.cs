using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.WebLis.Class;

namespace ZhiFang.WebLis.Other
{
    public partial class Main_PatNo : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("PatNo_Value") == string.Empty)
            {
                Response.Write("<span style=\"font-size:12px\">未登录！请重新</span><a href='Login_PatNo.aspx' style=\"font-size:12px\" >登录</a>！");
                Response.End();
            }
        }
    }
}
