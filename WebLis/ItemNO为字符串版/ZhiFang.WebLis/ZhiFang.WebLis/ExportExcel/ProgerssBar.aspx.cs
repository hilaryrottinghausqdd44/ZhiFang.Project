using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.IO;
using ZhiFang.WebLis.Class;

namespace ZhiFang.WebLis.ExportExcel
{
    public partial class ProgerssBar :BasePage
    {
        private void beginProgress()
        {
            //根据ProgressBar.htm显示进度条界面
            string templateFileName = Path.Combine(Server.MapPath("."), "ProgressBar.htm");
            StreamReader reader = new StreamReader(@templateFileName, System.Text.Encoding.GetEncoding("GB2312"));
            string html = reader.ReadToEnd();
            reader.Close();
            Response.Write(html);
            Response.Flush();
        }

        private void setProgress(int percent)
        {
            string jsBlock = "<script>SetPorgressBar('" + percent.ToString() + "'); </script>";
            Response.Write(jsBlock);
            Response.Flush();

        }
        private void finishProgress()
        {
            string jsBlock = "<script>SetCompleted();</script>";
            Response.Write(jsBlock);
            Response.Flush();
            Response.Close();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            beginProgress();
            if (Request.QueryString["Count"] != null)
            {
                int count = Convert.ToInt32(Request.QueryString["Count"]);
                for (int i = 0; i < count; i++) 
                {
                    setProgress(i);
                    System.Threading.Thread.Sleep(1000);
                }
            }
            //finishProgress();
        }

    }
}