using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;
using System.Configuration;
using System.IO;
using System.Text;

namespace ZhiFang.WebLis.ReportPrint
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["reportfile"] != null)
                {
                    string reportfile = Request.QueryString["reportfile"];
                    ZhiFang.Common.Log.Log.Debug("ZhiFang.WebLis.ReportPrint.WebForm1.Page_Load.reportfile:" + reportfile);
                    string filep = System.AppDomain.CurrentDomain.BaseDirectory + reportfile.Replace("..\\","");
                    ZhiFang.Common.Log.Log.Debug("ZhiFang.WebLis.ReportPrint.WebForm1.Page_Load.filep:" + filep);
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
            string path = fpath.Replace("\\", "/").Replace("��", "：");
            ZhiFang.Common.Log.Log.Debug("ZhiFang.WebLis.ReportPrint.WebForm1.EditPDF.fpath:" + fpath);
            ZhiFang.Common.Log.Log.Debug("ZhiFang.WebLis.ReportPrint.WebForm1.EditPDF.path:" + path);
            try
            {
                FileStream MyFileStream = new FileStream(path, FileMode.Open);
                ViewPdf(MyFileStream);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("ZhiFang.WebLis.ReportPrint.WebForm1.EditPDF.异常:" + e.ToString());
            }
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