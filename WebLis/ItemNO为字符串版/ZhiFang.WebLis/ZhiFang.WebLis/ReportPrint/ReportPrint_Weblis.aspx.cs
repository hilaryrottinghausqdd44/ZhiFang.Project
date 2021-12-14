using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using System.Text;
using System.Collections.Generic;
using ZhiFang.WebLis.Class;

namespace ZhiFang.WebLis.ReportPrint
{
    public partial class ReportPrint_Weblis : BasePage
    {
        public string strTable = "";
        public readonly IBPrintFrom_Weblis Printform_Weblis = BLLFactory<IBPrintFrom_Weblis>.GetBLL("PrintFrom_Weblis");
        public readonly IBPrintFrom Printform = BLLFactory<IBPrintFrom>.GetBLL("PrintFrom");

       // PrintFormWeblis a = new PrintFormWeblis();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string title = "0";
                if (base.CheckQueryStringNull("ReportFormTitle"))
                {
                    if (base.ReadQueryString("ReportFormTitle") == "Center")
                    {
                        title = "0";
                    }
                    else
                    {
                        if (base.ReadQueryString("ReportFormTitle") == "Client")
                        {
                            title = "1";
                        }
                        else
                        {
                            if (base.ReadQueryString("ReportFormTitle") == "Batch")
                            {
                                title = "2";
                            }
                            else
                            {
                                if (base.ReadQueryString("ReportFormTitle") == "MenZhen")
                                {
                                    title = "3";
                                }
                                else
                                {
                                    if (base.ReadQueryString("ReportFormTitle") == "ZhuYuan")
                                    {
                                        title = "4";
                                    }
                                    else
                                    {
                                        if (base.ReadQueryString("ReportFormTitle") == "TiJian")
                                        {
                                            title = "5";
                                        }
                                        else
                                        {

                                        }
                                    }
                                }
                            }
                        }
                    }
                    string[] tmp = base.ReadQueryString("ReportFormID").Split(',');

                    if (base.CheckQueryStringNull("PrintType") && base.ReadQueryString("PrintType") == "A5")
                    {
                        if (base.CheckQueryStringNull("ReportFormID"))
                        {
                            strTable = this.PrintHtml(base.ReadQueryString("ReportFormID").Split(','), title);
                        }
                    }
                    else
                    {
                        SortedList sl = new SortedList();
                        string tmpserial = "";
                        for (int i = 0; i < tmp.Length; i++)
                        {
                            if (tmp[i].Split(';').Length > 1)
                            {
                                if (tmpserial != tmp[i].Split(';')[1])
                                {
                                    sl.Add(tmp[i].Split(';')[1], tmp[i].Split(';')[0]);
                                    tmpserial = tmp[i].Split(';')[1];
                                }
                                else
                                {
                                    sl[tmp[i].Split(';')[1]] += ',' + tmp[i].Split(';')[0];
                                }
                            }
                        }
                        if (base.CheckQueryStringNull("PrintType") && base.ReadQueryString("PrintType") == "A4")
                        {
                            if (base.CheckQueryStringNull("ReportFormID"))
                            {
                                strTable = this.PrintMergeHtml(sl, title);
                            }
                        }
                        if (base.CheckQueryStringNull("PrintType") && base.ReadQueryString("PrintType") == "EA4")
                        {
                            if (base.CheckQueryStringNull("ReportFormID"))
                            {
                                strTable = this.PrintMergeEnHtml(sl, title);
                            }
                        }
                    }
                }
            }
            catch
            {
                Response.End();
            }
        }

        private string PrintMergeHtml(SortedList ReportFormID, string title)
        {
            string tmphtml = "";
            List<string> tmp;
            List<string> tmplist = new List<string>();
            for (int i = 0; i < ReportFormID.Count; i++)
            {
                tmp = Printform_Weblis.PrintMergeHtml(ReportFormID.GetByIndex(i).ToString(), (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
                if (tmp != null)
                {
                    for (int j = 0; j < tmp.Count; j++)
                    {
                        tmplist.Add(tmp[j]);
                    }
                }
            }
            for (int i = 0; i < tmplist.Count; i++)
            {
                if (i == 0)
                {
                    tmphtml += "<br><img src=\"" + "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  />";
                    //tmphtml += "<br>" + Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (Common.ReportFormTitle)Convert.ToInt32(title));
                }
                else
                {
                    //string aaa = "<p style=\"page-break-after:always\">&nbsp;</p>" + Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (Common.ReportFormTitle)Convert.ToInt32(title));
                    string aaa = "<p style=\"page-break-after:always\">&nbsp;</p><img src=\"" + "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  />";
                    tmphtml += aaa;
                }
                //tmphtml += Printform.PrintHtml(FormNo[i]) ;
            }

            return tmphtml;
        }
        private string PrintMergeEnHtml(SortedList ReportFormID, string title)
        {
            string tmphtml = "";
            List<string> tmp;
            List<string> tmplist = new List<string>();
            for (int i = 0; i < ReportFormID.Count; i++)
            {
                tmp = Printform_Weblis.PrintMergeHtml(ReportFormID.GetByIndex(i).ToString(), (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
                if (tmp != null)
                {
                    for (int j = 0; j < tmp.Count; j++)
                    {
                        tmplist.Add(tmp[j]);
                    }
                }
            }
            for (int i = 0; i < tmplist.Count; i++)
            {
                if (i == 0)
                {
                    tmphtml += "<br><img src=\"" + "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  />";
                    //tmphtml += "<br>" + Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (Common.ReportFormTitle)Convert.ToInt32(title));
                }
                else
                {
                    //string aaa = "<p style=\"page-break-after:always\">&nbsp;</p>" + Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (Common.ReportFormTitle)Convert.ToInt32(title));
                    string aaa = "<p style=\"page-break-after:always\">&nbsp;</p><img src=\"" + "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  />";
                    tmphtml += aaa;
                }
                //tmphtml += Printform.PrintHtml(FormNo[i]) ;
            }

            return tmphtml;
        }
        public string PrintHtml(string[] ReportFormID, string title)
        {
            string tmphtml = "";
            List<string> tmp;
            List<string> tmplist = new List<string>();
            for (int i = 0; i < ReportFormID.Length; i++)
            {
                tmp = Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
                if (tmp != null)
                {
                    for (int j = 0; j < tmp.Count; j++)
                    {
                        tmplist.Add(tmp[j]);
                    }
                }
            }
            for (int i = 0; i < tmplist.Count; i++)
            {
                if (i == 0)
                {
                    tmphtml += "<br><img src=\"" + "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  />";
                    //tmphtml += "<br>" + Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (Common.ReportFormTitle)Convert.ToInt32(title));
                }
                else
                {
                    //string aaa = "<p style=\"page-break-after:always\">&nbsp;</p>" + Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (Common.ReportFormTitle)Convert.ToInt32(title));
                    string aaa = "<p style=\"page-break-after:always\">&nbsp;</p><img src=\"" + "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  />";
                    tmphtml += aaa;
                }
                //tmphtml += Printform.PrintHtml(FormNo[i]) ;
            }
            return tmphtml;
        }
    }
}

