using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.WebLis.Class;
using ZhiFang.Common.Public;

namespace ZhiFang.WebLis.Main
{
    public partial class Main_foshan : BasePage
    {
        protected string EmployeeName;
        protected string Company = "";
        protected string text;
        protected void Page_Load(object sender, EventArgs e)
        {
            //IDAL.IDModules dal = DALFactory.DalFactory<IDAL.IDModules>.GetDal("S_Modules", ZhiFang.Common.Dictionary.DBSource.LisDB());
            
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

                    string ZhiFangUserPosition = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUserPosition");
                    List<string> postion = new List<string>() { "WebLisReportFormQueryPrint", "WebLisApplyInput" };
                    for (int i = 0; i < postion.Count; i++)
                    {
                        if (ZhiFangUserPosition.IndexOf(postion[i]) >= 0)
                        {
                            text += postion[i]+',';
                            ZhiFang.Common.Log.Log.Info("aaaaaaaaaaaaaaaaaaaa"+i+":" + postion[i]);
                        }
                    }
                    try
                    {
                        inputPosition.Value = text.TrimEnd(',');
                    }
                    catch (Exception)
                    {
                        inputPosition.Value = text;
                    }
                   
                }
            }
        }
    }
}