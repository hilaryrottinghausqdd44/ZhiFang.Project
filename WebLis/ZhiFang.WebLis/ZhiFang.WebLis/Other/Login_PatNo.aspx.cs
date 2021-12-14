using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using System.Data;
using ZhiFang.WebLis.Class;

namespace ZhiFang.WebLis.Other
{
    public partial class Login_PatNo : BasePage
    {
        private readonly IBReportFormFull rfb = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
        private readonly IBPatNo_Passwords pnpwb = BLLFactory<IBPatNo_Passwords>.GetBLL("PatNo_Passwords");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.TextBox1.Text.Trim() != "" && ZhiFang.Tools.Validate.CheckSqlStr(this.TextBox1.Text.Trim()) && this.TextBox2.Text.Trim() != "")
            {
                ZhiFang.Model.PatNo_Passwords pnpw_m = new ZhiFang.Model.PatNo_Passwords();
                pnpw_m.PatNo = this.TextBox1.Text.Trim();
                pnpw_m.AddTime = null;
                DataSet ds = pnpwb.GetList(pnpw_m);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (this.TextBox2.Text.Trim().ToUpper() == ds.Tables[0].Rows[0]["Passwords"].ToString().Trim().ToUpper())
                    {
                        ZhiFang.Common.Public.Cookie.CookieHelper.Write("PatNo_Value", this.TextBox1.Text.Trim());
                        Response.Redirect("Main_PatNo.aspx");
                    }
                    else
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndCloseAndFunction("密码错误请重新登录！", " location.href=location.href;"));
                    }
                }
                else
                {
                    ZhiFang.Model.ReportFormFull rff_m = new ZhiFang.Model.ReportFormFull();
                    rff_m.PATNO = this.TextBox1.Text.Trim();
                    DataSet dsrff = rfb.GetList(rff_m);
                    if (dsrff != null && dsrff.Tables.Count > 0 && dsrff.Tables[0].Rows.Count > 0)
                    {
                        if (this.TextBox2.Text.Trim() == "123456")
                        {
                            pnpw_m.Passwords = "123456";
                            pnpw_m.AddTime = DateTime.Now;
                            pnpwb.Add(pnpw_m);
                            ZhiFang.Common.Public.Cookie.CookieHelper.Write("PatNo_Value", this.TextBox1.Text.Trim());
                            Response.Redirect("Main_PatNo.aspx");
                        }
                        else
                        {
                            Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("密码错误请重新登录！", " location.href=location.href;"));
                        }
                    }
                    else
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("病例号不存在请重新输入！", " location.href=location.href;"));
                    }
                }
            }
            else
            {
                Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("病例号不存在请重新输入！", " location.href=location.href;"));
            }
        }
    }
}
