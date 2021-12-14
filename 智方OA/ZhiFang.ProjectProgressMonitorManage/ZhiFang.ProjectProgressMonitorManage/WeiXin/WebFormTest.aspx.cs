using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.Entity.Base;
using ZhiFang.IBLL.ProjectProgressMonitorManage;

namespace ZhiFang.ProjectProgressMonitorManage.WeiXin
{
    public partial class WebFormTest : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            var pmlist = GetPermanentMediaList(this.Application, PermanentMediaType.news, 0, 20);
            ZhiFang.Common.Log.Log.Debug("1");
            if (pmlist.item != null && pmlist.item.Length > 0)
            {
                ZhiFang.Common.Log.Log.Debug("2");
                for (int i = 0; i < pmlist.item.Length; i++)
                {
                    ZhiFang.Common.Log.Log.Debug("3@" + i);
                    if (pmlist.item[i] != null && pmlist.item[i].content != null && pmlist.item[i].content.news_item != null && pmlist.item[i].content.news_item.Length > 0)
                    {
                        ZhiFang.Common.Log.Log.Debug("4@" + i);
                        for (int j = 0; j < pmlist.item[i].content.news_item.Length; j++)
                        {
                            ZhiFang.Common.Log.Log.Debug("5@" + i + "@" + j);
                            //pmlist.item[i].content.news_item[j].thumb_media_Url = GetPermanentMediaFile(this.Application, pmlist.item[i].content.news_item[j].thumb_media_id).Replace(System.AppDomain.CurrentDomain.BaseDirectory, "");
                            ZhiFang.Common.Log.Log.Debug("6@" + i + "@" + j);
                        }
                    }
                }
            }
            ZhiFang.Common.Log.Log.Debug("7");
            Response.Write(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(pmlist));
            ZhiFang.Common.Log.Log.Debug("8");
        }
    

        protected void Button2_Click(object sender, EventArgs e)
        {
        Spring.Context.IApplicationContext context = Spring.Context.Support.ContextRegistry.GetContext();

        var bffile = context.GetObject("BFFile") as IBFFile;
        var ffilelist = bffile.SearchFFileReadingUserListByHQLAndEmployeeID("5006210335888865502", " ffile.Title like '%1%' ", true, 1, 7, "5250965737715795590", "");
        }
    }
}