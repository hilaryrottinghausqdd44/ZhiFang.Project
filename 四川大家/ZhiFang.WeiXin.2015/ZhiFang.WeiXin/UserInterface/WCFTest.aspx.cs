using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.Common.Log;
using ZhiFang.WeiXin.IBLL;
using ZhiFang.WeiXin.BusinessObject;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.UserInterface
{
    public partial class WCFTest : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
            var bwxa = context.GetObject("BBWeiXinAccount") as IBBWeiXinAccount;
            ZhiFang.WeiXin.Entity.BWeiXinAccount BWeiXinAccount = new Entity.BWeiXinAccount();
            BWeiXinAccount.UserName = "nickname";
            BWeiXinAccount.WeiXinAccount = "openid";
            BWeiXinAccount.SexID = 1;
            BWeiXinAccount.CountryName = "country";
            BWeiXinAccount.ProvinceName = "province";
            BWeiXinAccount.CityName = "city";
            BWeiXinAccount.Language = "language";
            BWeiXinAccount.WeiXinAccount = ZhiFang.WeiXin.Common.GUIDHelp.GetGUIDLong().ToString();
            bwxa.Entity = BWeiXinAccount;
            //bwxa.Entity.Id = ZhiFang.WeiXin.Common.GUIDHelp.GetGUIDLong();
            bwxa.Add();

            //Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();
            //var bwxa = context.GetObject("BBAntiType") as IBBAntiType;
            //ZhiFang.WeiXin.Entity.BAntiType BAntiType = new Entity.BAntiType();
            //BAntiType.CName = "aaa";

            //bwxa.Entity = BAntiType;
            ////bwxa.Entity.Id = ZhiFang.WeiXin.Common.GUIDHelp.GetGUIDLong();
            //bwxa.Add();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BasePage.PushTextToWeiXinOpenId(this.Application, this.TextBox1.Text, this.TextBox2.Text);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            BasePage.PushMessageTemplate3Context(this.Application, this.TextBox3.Text, ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("TemplateId1"), "http://" + ZhiFang.WeiXin.Common.ConfigHelper.GetConfigString("Domain") + "/zhifang.weixin/webapp/ui/index.html#barcode", "#cc9966", new TemplateIdObject3() { first = new TemplateDataObject() { value = "您的检查报告结果已经出来了", color = "#cccccc" }, keyword1 = new TemplateDataObject() { value = "1", color = "#000000" }, keyword2 = new TemplateDataObject() { value = "2", color = "#000000" }, keyword3 = new TemplateDataObject() { value = "3", color = "#000000" }, remark = new TemplateDataObject() { value = "123123123", color = "#000000" } });
        }
    }
}