using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.Common.Log;
using ZhiFang.WeiXin.BusinessObject;

namespace ZhiFang.WeiXin.WebForm
{
    public partial class RefundTest : PayBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Log.Info(this.GetType().ToString()+" page load");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

        }

        protected void submit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(transaction_id.Text) && string.IsNullOrEmpty(out_trade_no.Text))
            {
                Response.Write("<script LANGUAGE='javascript'>alert('微信订单号和商户订单号至少填一个！');</script>");
                return;
            }
            if (string.IsNullOrEmpty(total_fee.Text))
            {
                Response.Write("<script LANGUAGE='javascript'>alert('订单总金额必填！');</script>");
                return;
            }
            if (string.IsNullOrEmpty(refund_fee.Text))
            {
                Response.Write("<script LANGUAGE='javascript'>alert('退款金额必填！');</script>");
                return;
            }

            //调用订单退款接口,如果内部出现异常则在页面上显示异常原因
            try
            {
                string result = RefundTest.RunRefundApply(transaction_id.Text, out_trade_no.Text, total_fee.Text, refund_fee.Text, out_refund_no.Text);
                Response.Write("<span style='color:#00CD00;font-size:20px'>" + result + "</span>");
            }
            catch (Exception ex)
            {
                Response.Write("<span style='color:#FF0000;font-size:20px'>" + ex.ToString() + "</span>");
            }
        }
    }
}