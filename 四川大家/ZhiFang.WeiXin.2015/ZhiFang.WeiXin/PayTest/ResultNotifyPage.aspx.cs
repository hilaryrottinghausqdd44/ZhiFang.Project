using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.WeiXin.BusinessObject;

namespace ZhiFang.WeiXin.PayTest
{
    public partial class ResultNotifyPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            PayBackJSAPI resultNotify = new PayBackJSAPI(this);
            resultNotify.ProcessNotify();
        }
    }
}