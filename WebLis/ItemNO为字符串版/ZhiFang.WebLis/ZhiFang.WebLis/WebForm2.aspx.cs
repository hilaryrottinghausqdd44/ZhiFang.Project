using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhiFang.WebLis
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Response.Write("<script>alert('@@@" + this.textUserid.Text + "');</script>");
            }
            else
            {
                Response.Write("<script>alert('" + this.textUserid.Text + "');</script>");
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Write("<script>alert('" + this.textUserid.Text + "');</script>");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("<script>alert('" + this.textUserid.Text + "');</script>");
        }
    }
}