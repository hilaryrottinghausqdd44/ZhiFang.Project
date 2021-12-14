using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using System.Text;
using ZhiFang.WebLis.Class;
using ZhiFang.IBLL.Report;

namespace ZhiFang.WebLis.ReportPrint
{
    public partial class ReportPrint : BasePage
    {
        public string strTable="";
        public readonly IBPrintFrom Printform = BLLFactory<IBPrintFrom>.GetBLL("PrintFrom");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (base.CheckQueryStringNull("FormNo"))
            {             
                strTable = this.PrintHtml(base.ReadQueryString("FormNo").Split(','));
            }
        }
        public string PrintHtml(string[] FormNo)
        {
            string tmphtml = "";
            for (int i = 0; i < FormNo.Length; i++)
            {
                if (i == 0)
                {
                    tmphtml += "<br>"+Printform.PrintHtml(FormNo[i]);
                }
                else
                {
                    tmphtml += "<p style=\"page-break-after:always\">&nbsp;</p>" + Printform.PrintHtml(FormNo[i]);
                }
                //tmphtml += Printform.PrintHtml(FormNo[i]) ;
            }

            return tmphtml;
        }
        
    }
}
