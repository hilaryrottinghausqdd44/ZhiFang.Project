using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.WeiXin.Common;

namespace ZhiFang.WeiXin
{
    public partial class SJBhttp_SmsOperatorTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SJBhttp_SmsOperatorHelp.SendMessageTest();
        }
    }
}