using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhiFang.WebLis
{
    public partial class ChangePwd : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string pwd = txtNewPwd.Text.Trim();
            string pwd2 = txtConfirmPwd.Text.Trim();
            WSRBAC.WSRbac wsrbac = null;
            if (pwd == "") {
                lblMessage.Text = "密码不能为空!";
                lblMessage.ForeColor =System.Drawing.Color.Red;
                return;
            }
            if (pwd != pwd2)
            {
                lblMessage.Text = "两次输入的密码不一样!";
                lblMessage.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else {
                string userID = null;
                if (HttpContext.Current.Request.Cookies["ZhiFangUser"] != null) {
                     userID = HttpContext.Current.Request.Cookies["ZhiFangUser"].Value;
                }
                wsrbac = new WSRBAC.WSRbac();
                bool a = wsrbac.ChangePassWord(userID,pwd2);
                if (a) {
                    lblMessage.Text = "密码修改成功!";
                }
            }
        }
    }
}