using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace ZhiFang.WebLis.ServiceWCF
{
    /// <summary>
    /// ReportFormWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ReportFormWebService : System.Web.Services.WebService
    {
        PrintService pss = new PrintService();
        [WebMethod]
        public bool GenerationReportPrint(string reportformId, string reportformtitle, string reportformfiletype, string printtype)
        {

            return pss.GenerationReportPrint(reportformId, reportformtitle, reportformfiletype, printtype);
        }
    }
}
