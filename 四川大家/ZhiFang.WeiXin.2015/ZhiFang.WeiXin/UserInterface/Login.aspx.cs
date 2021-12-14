using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.WeiXin.UserInterface
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_Click(object sender, EventArgs e)
        {
            if (this.TextBox1.Text != null && this.TextBox1.Text.Trim() != "")
            {
                if (this.TextBox2.Text != null && this.TextBox2.Text.Trim() != "")
                {
                    if (this.TextBox1.Text.Trim() == "zhifangweixin" && this.TextBox2.Text.Trim() == "weixinadmin82272494")
                    {
                        ZhiFang.WeiXin.Common.Cookie.CookieHelper.Write(DicCookieSession.WeiXinAdminFlag, DicCookieSession.WeiXinAdminFlagvalue);
                        this.Response.Redirect("BackStageManagementSystem/Main.aspx", false);
                    }
                }
                else
                {
                    this.Response.Redirect(Request.Url.AbsoluteUri, false);
                }
            }
            else
            {
                this.Response.Redirect(Request.Url.AbsoluteUri, false);
            }
        }
    }
}