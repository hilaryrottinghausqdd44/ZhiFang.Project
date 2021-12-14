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
                    if (string.IsNullOrEmpty(ConfigHelper.GetConfigString("WebLisInCluderRMS")) || ConfigHelper.GetConfigString("WebLisInCluderRMS").Trim() == "0")
                    {
                        string ZhiFangUser = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                        User user = new User(ZhiFangUser);
                        Company = user.CompanyName;
                        if (Company == null)
                            Company = "";
                        EmployeeName = user.NameL + user.NameF;
                        this.Label1.Text = "当前用户：" + EmployeeName + "";
                        Cookie.CookieHelper.Write("EmployeeName", EmployeeName);
                    }
                    else
                    {
                        string ZhiFangUser = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                        string ZhiFangUserID = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUserID");
                        if (ZhiFangUser == "zhifangkj8001" || (!string.IsNullOrEmpty(ConfigHelper.GetConfigString("adminaccount")) && ZhiFangUser == ConfigHelper.GetConfigString("adminaccount")))
                        {
                            EmployeeName = "WebLis管理员";
                            this.Label1.Text = "当前用户：" + EmployeeName + "";
                            Cookie.CookieHelper.Write("EmployeeName", EmployeeName);
                        }
                        else
                        {
                            BLL.RBAC.HR_Employees embbll = new BLL.RBAC.HR_Employees();
                            var emp = embbll.GetModel(int.Parse(ZhiFangUserID));
                            EmployeeName = emp.NameL + emp.NameF;
                            this.Label1.Text = "当前用户：" + EmployeeName + "";
                            Cookie.CookieHelper.Write("EmployeeName", EmployeeName);
                        }
                    }
                }
            }
        }
    }
}