using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhiFang.WebLis
{
    public partial class NewPersonSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BarcodeTxt.Text = "";
                this.NameTxt.Text = "";
                this.CheckTxt.Text = "";
                this.ErrorInfo.InnerText = "";
                //this.CheckTxtInput.Value = "";
            }
            else
            {
                if (ErrorInfo.InnerText != "")
                {

                }
            }
            
        }

        protected void Searchbtn_Click(object sender, EventArgs e)
        {
            if (Session["ValidateCode"] != null && Session["ValidateCode"].ToString().Trim() != "")
            {
                if (this.CheckTxt.Text.Trim() != "")
                {
                    if (this.CheckTxt.Text.Trim().ToLower() == Session["ValidateCode"].ToString().Trim().ToLower())
                    {
                        Response.Redirect("ui/report/pki/Person.html");
                    }
                    else
                    {
                        ErrorInfo.InnerText = "验证码错误！";
                        this.CheckTxt.Text = "";
                        this.CheckTxt.Focus();
                    }
                }
                else
                {
                    ErrorInfo.InnerText = "请输入验证码！";
                    this.CheckTxt.Text = "";
                    this.CheckTxt.Focus();
                }
            }
            else
            {
                ErrorInfo.InnerText = "请刷新验证码！";
                this.CheckTxt.Text = "";
                this.CheckTxt.Focus();
            }
        }
    }
}