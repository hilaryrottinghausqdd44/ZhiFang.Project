using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhiFang.ReportFormQueryPrint.ReportPrint
{
    public partial class PrintPDF : System.Web.UI.Page
    {
        public StringBuilder sb = new StringBuilder();
        protected string tmpfilepath = "";
        public string reportid = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            if (!Page.IsPostBack)
            {
                if (Request.QueryString["reportfile"] != null)
                {
                    //if (Request.QueryString["flag"] != null)
                    //{
                    //    btnprintpreview.Visible = true;
                    //    btnprint.Visible = true;
                    //}
                    //if (Request.QueryString["reportformid"] != null)
                    //{
                    //    reportid = Request.QueryString["reportformid"].ToString();
                    //}

                    tmpfilepath = Request.QueryString["reportfile"].ToString();
                    //string filep = Request.QueryString["reportfile"].ToString();
                    //string filename = System.AppDomain.CurrentDomain.BaseDirectory + filep.Replace("../", "");
                    //filename = filename.Replace("/", "\\");
                    //filename = filename.Replace("\\\\", "\\");
                    //if (File.Exists(filename))
                    //{
                    //    tmpfilepath = filep.Replace("/", "\\");
                    //}
                    //else
                    //{
                    //    TipMessage();
                    //    tmpfilepath = "";
                    //}

                }
            }
        }
        /// <summary>
        /// 显示pdf控件
        /// </summary>
        /// <param name="tmpfilepath"></param>
        private void ShowPdf(string tmpfilepath)
        {
            sb.Append("<DIV id=\"showdiv\" style=\"Z-INDEX: 0; LEFT:0px; WIDTH: 100%; POSITION: absolute; TOP: -35px; HEIGHT: 600px\">");
            sb.Append("	<object classid=\"clsid:CA8A9780-280D-11CF-A24D-444553540000\" width=\"100%\" height=\"100%\" border=\"0\"");
            sb.Append("		id=\"pdf\" name=\"pdf\" VIEWASTEXT>");
            sb.Append("		<param name=\"toolbar\" value=\"false\">");
            sb.Append("		<param name=\"_Version\" value=\"65539\">");
            sb.Append("		<param name=\"_ExtentX\" value=\"20108\">");
            sb.Append("		<param name=\"_ExtentY\" value=\"10866\">");
            sb.Append("		<param name=\"_StockProps\" value=\"0\">");
            sb.Append("		<param name=\"SRC\" value=\"PDFReader.aspx?reportfile=" + tmpfilepath + "\">");
            sb.Append("	</object>");
            sb.Append("</DIV>");
        }
        /// <summary>
        /// 提示文件不存在
        /// </summary>
        private void TipMessage()
        {
            Response.Write("文件不存在");
        }
    }
}