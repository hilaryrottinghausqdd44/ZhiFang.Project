using System;
using System.IO;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZhiFang.ReportFormQueryPrint.ReportPrint
{
    public partial class PDFReader : System.Web.UI.Page
    {
        //private string ppath = ConfigurationSettings.AppSettings["PdfDirectory"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["reportfile"] != null)
                {
                    string filep = System.AppDomain.CurrentDomain.BaseDirectory + Request.QueryString["reportfile"].Replace("../", "").ToString();
                    EditPDF(filep);
                }
            }
        }
        /// <summary>
        /// 生成pdf流
        /// </summary>
        /// <param name="fpath"></param>
        private void EditPDF(string fpath)
        {
            string path = fpath.Replace("\\", "/");
            FileStream MyFileStream = new FileStream(path, FileMode.Open);
            ViewPdf(MyFileStream);
        }
        /// <summary>
        /// 显示pdf
        /// </summary>
        /// <param name="fs"></param>
        private void ViewPdf(Stream fs)
        {
            byte[] buffer = new byte[fs.Length];
            fs.Position = 0;
            fs.Read(buffer, 0, (int)fs.Length);
            Response.Clear();
            Response.AddHeader("Content-Length", fs.Length.ToString());
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "inline;FileName=out.pdf");
            fs.Close();
            Response.BinaryWrite(buffer);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
        }
    }
}