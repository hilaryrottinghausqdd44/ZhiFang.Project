using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhiFang.WeiXin.WebForm
{
    public partial class PayToUserTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TextOpenId.Text) && string.IsNullOrEmpty(TextOpenId.Text))
            {
                Response.Write("<script LANGUAGE='javascript'>alert('微信OpenId必填！');</script>");
                return;
            }
            if (string.IsNullOrEmpty(TextName.Text))
            {
                Response.Write("<script LANGUAGE='javascript'>alert('姓名必填！');</script>");
                return;
            }
            if (string.IsNullOrEmpty(TextAmount.Text))
            {
                Response.Write("<script LANGUAGE='javascript'>alert('金额必填！');</script>");
                return;
            }

            if (string.IsNullOrEmpty(TextDesc.Text))
            {
                Response.Write("<script LANGUAGE='javascript'>alert('描述必填！');</script>");
                return;
            }

            //调用订单退款接口,如果内部出现异常则在页面上显示异常原因
            try
            {
                var result = BusinessObject.PayToUser.PayToUserWeiXin(null,TextOpenId.Text.Trim(), TextName.Text.Trim(),float.Parse(TextAmount.Text.Trim()), TextDesc.Text.Trim());
                string a ="";
                foreach (var i in result)
                {
                    a += i.Key + "=" + i.Value;
                }
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + a + "</span>");
            }
            catch (Exception ex)
            {
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + ex.ToString() + "</span>");
            }
            
        }
    }
}