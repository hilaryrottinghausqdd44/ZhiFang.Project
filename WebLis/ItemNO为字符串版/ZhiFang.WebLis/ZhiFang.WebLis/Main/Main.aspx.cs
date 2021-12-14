using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.Common.Public;
using ZhiFang.WebLis.Class;

namespace ZhiFang.WebLis.Main
{
    public partial class Main : BasePage
    {
        protected string EmployeeName;
        protected string Company = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 在此处放置用户代码以初始化页面
                if (base.CheckCookies("ZhiFangUser"))
                {
                    string ZhiFangUser = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                    User user = new User(ZhiFangUser);
                    Company = user.CompanyName;
                    if (Company == null)
                        Company = "";
                    EmployeeName = user.NameL + user.NameF;
                    this.Label1.Text = EmployeeName;
                    Cookie.CookieHelper.Write("EmployeeName", EmployeeName);
                }
            }
        }
    }
}