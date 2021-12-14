using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Report;
using System.Data;

namespace ZhiFang.WebLis.Other
{
    public partial class ChangPassWords_PatNo : System.Web.UI.Page
    {
        private readonly IBPatNo_Passwords pnpwb = BLLFactory<IBPatNo_Passwords>.GetBLL("PatNo_Passwords");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("PatNo_Value") == string.Empty)
            {
                Response.Write("<span style=\"font-size:12px\">未登录！请重新</span><a href='Login_PatNo.aspx' style=\"font-size:12px\" >登录</a>！");
                Response.End();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (this.TextBox1.Text.Trim() != "")
            {
                ZhiFang.Model.PatNo_Passwords pnpw_m = new ZhiFang.Model.PatNo_Passwords();
                pnpw_m.PatNo = ZhiFang.Common.Public.Cookie.CookieHelper.Read("PatNo_Value");
                pnpw_m.AddTime = null;
                DataSet ds = pnpwb.GetList(pnpw_m);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (this.TextBox1.Text.Trim().ToUpper() == ds.Tables[0].Rows[0]["Passwords"].ToString().Trim().ToUpper())
                    {
                        if (this.TextBox2.Text.Trim() != "" && this.TextBox3.Text.Trim() != "" && this.TextBox2.Text.Trim() == this.TextBox3.Text.Trim())
                        {                            
                            pnpw_m.UpdateTime = DateTime.Now;
                            pnpw_m.Passwords = this.TextBox2.Text.Trim().Replace("\'", "\'\'");
                            if (pnpwb.Update(pnpw_m) > 0)
                            {
                                Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("密码修改成功！", " location.href=location.href;"));
                            }
                            else
                            {
                                Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("密码修改失败！", " location.href=location.href;"));
                            }
                        }
                        else
                        {
                            Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("密码不一致请重新输入！", " location.href=location.href;"));
                        }
                    }
                    else
                    {
                        Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("原密码错误请重新输入！", " location.href=location.href;"));
                    }
                }
                else
                {
                    Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("病例号不存在请重新输入！", " location.href=location.href;"));
                }
            }
            else
            {
                Response.Write(ZhiFang.Common.Public.ScriptStr.AlertAndFunction("原密码错误请重新输入！", " location.href=location.href;"));
            }
        }
    }
}
