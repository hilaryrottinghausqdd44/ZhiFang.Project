using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IBLL;
using System.Collections;
using System.Text;
using ZhiFang.WebLis.Class;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using ZhiFang.Common.Dictionary;
using ZhiFang.Common.Public;

namespace ZhiFang.WebLis.ReportPrint
{
    public partial class ReportPrint_Weblis_QinDaoLanXin : BasePage
    {
        public string strTable = "";
        public readonly IBPrintFrom_Weblis Printform_Weblis = BLLFactory<IBPrintFrom_Weblis>.GetBLL("PrintFrom_Weblis");
        public readonly IBPrintFrom Printform = BLLFactory<IBPrintFrom>.GetBLL("PrintFrom");

        // PrintFormWeblis a = new PrintFormWeblis();
        protected void Page_Load(object sender, EventArgs e)
        {
            AjaxPro.Utility.RegisterTypeForAjax(typeof(ZhiFang.WebLis.Ashx.ReportPrint));
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
                                            //Response.Write("title---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
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
                        string tmpreportformid = "";
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
                //this.ReportFormListContent.InnerHtml = strTable;
            }
                
            catch (Exception eee)
            {
                ZhiFang.Common.Log.Log.Info(eee.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                Response.End();
            }
        }

        private string PrintMergeHtml(SortedList ReportFormID, string title)
        {
            ZhiFang.Common.Log.Log.Info(title.ToString() + "---------" + ReportFormID.GetByIndex(0).ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            string tmphtml = "";
            List<string> tmp;
            List<string> tmplist = new List<string>();
            for (int i = 0; i < ReportFormID.Count; i++)
            {
                tmp = Printform_Weblis.PrintMergeHtml(ReportFormID.GetByIndex(i).ToString(), (ReportFormTitle)Convert.ToInt32(title), "CONTEXT");
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
                    tmphtml += "<br><img src=\"" + "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  />";
                    //tmphtml += "<br>" + Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (Common.ReportFormTitle)Convert.ToInt32(title));
                }
                else
                {
                    //string aaa = "<p style=\"page-break-after:always\">&nbsp;</p>" + Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (Common.ReportFormTitle)Convert.ToInt32(title));
                    string aaa = "<p style=\"page-break-after:always\">&nbsp;</p><img src=\"" + "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  />";
                    tmphtml += aaa;
                }
                //tmphtml += Printform.PrintHtml(FormNo[i]) ;
            }

            return tmphtml;
        }
        private string PrintMergeEnHtml(SortedList ReportFormID, string title)
        {
            ZhiFang.Common.Log.Log.Info(title.ToString() + "E---------" + ReportFormID.GetByIndex(0).ToString() + "E---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            string tmphtml = "";
            List<string> tmp;
            List<string> tmplist = new List<string>();
            for (int i = 0; i < ReportFormID.Count; i++)
            {
                tmp = Printform_Weblis.PrintMergeHtml(ReportFormID.GetByIndex(i).ToString(), (ReportFormTitle)Convert.ToInt32(title), "CONTEXT");
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
                    tmphtml += "<br><img src=\"" + "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  />";
                    //tmphtml += "<br>" + Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (Common.ReportFormTitle)Convert.ToInt32(title));
                }
                else
                {
                    //string aaa = "<p style=\"page-break-after:always\">&nbsp;</p>" + Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (Common.ReportFormTitle)Convert.ToInt32(title));
                    string aaa = "<p style=\"page-break-after:always\">&nbsp;</p><img src=\"" + "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ConfigHelper.GetConfigString("ReportFormFilesDir"))) + "\"  />";
                    tmphtml += aaa;
                }
                //tmphtml += Printform.PrintHtml(FormNo[i]) ;
            }

            return tmphtml;
        }
        public string PrintHtml(string[] ReportFormID, string title)
        {
            try
            {
                Response.Write("<link href=\"../Css/Default.css\" type=\"text/css\" rel=\"stylesheet\"><div id='statstartdiv'>计算开始......</div>");
                Response.Write("<div id='statingdiv'>计算中请稍后......</div><div id='tmpdiv1'></div><table id=\"bart\" width=\"100%\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\" bordercolor=\"#000000\"><tr><td><div id='progressbar' style=\"background-color:#006699 \" width=\"0\"></div></td></tr></table>");
                Response.Flush();

                System.Text.StringBuilder tmphtml = new System.Text.StringBuilder();
                List<string> tmp;
                List<string> tmplist = new List<string>();
                for (int i = 0; i < ReportFormID.Length; i++)
                {
                    Response.Write("<script>document.getElementById('tmpdiv1').innerHTML='正在计算报告：" + ReportFormID[i].ToString().Trim() + "(" + Math.Round(Convert.ToDouble((i + 1) * 100 / ReportFormID.Length), 0) + "%)';document.getElementById('progressbar').style.width='" + Math.Round(Convert.ToDouble((i + 1) * 100 / ReportFormID.Length), 0) + "%';</script>");
                    Response.Flush();
                    tmp = Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (ReportFormTitle)Convert.ToInt32(title), "CONTEXT");
                    if (tmp != null)
                    {
                        for (int j = 0; j < tmp.Count; j++)
                        {
                            tmplist.Add(tmp[j]);
                        }
                    }
                }

                Response.Write("<script>document.getElementById('tmpdiv1').innerHTML = '正在绘制表格......';document.getElementById('statstartdiv').style.display='none';document.getElementById('statingdiv').style.display='none';document.getElementById('bart').style.display='none';</script>");
                Response.Flush();
                SortedList<string, string> csslist = new SortedList<string, string>();
                for (int i = 0; i < tmplist.Count; i++)
                {
                    string tmphtmlsub = tmplist[i].Replace("src=\"", "src=\"..\\TmpReportImagePath\\");
                    tmphtmlsub = SetCssList(tmphtmlsub, ref csslist);
                    tmphtmlsub = tmphtmlsub.Substring(tmphtmlsub.IndexOf("<table"), tmphtmlsub.LastIndexOf("</table>") - tmphtmlsub.IndexOf("<table")+8);
                    if (i != 0)
                    {
                        tmphtmlsub = "<p style=\"page-break-after:always\">&nbsp;</p>" + tmphtmlsub.Replace("src=\"", "src=\"..\\TmpReportImagePath\\");
                    }
                    tmphtml.Append(tmphtmlsub);
                }
                Response.Write("<script>document.getElementById('tmpdiv1').innerHTML = '正在生成样式表......';</script>");
                Response.Flush();
                string csshtml = GetCssHtml(csslist);
                Response.Write("<script>document.getElementById('tmpdiv1').innerHTML = '生成样式表完成';</script>");
                Response.Flush();
                //Common.FilesHelper.WriteContext(tmphtml.ToString(), System.AppDomain.CurrentDomain.BaseDirectory + "\\" + Common.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\aaa.html");
                Response.Write("<script>document.getElementById('tmpdiv1').style.display='none';</script>");
                Response.Flush();
                tmphtml.Insert(0, "<html><head><meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\"><title>检验报告单</title></head><body bgcolor=\"#FFFFFF\" text=\"#000000\">" + csshtml);
                tmphtml.Append("</body></html>");
                return tmphtml.ToString();
            }
            catch (Exception e)
            {
                return "";
            }
        }

        private string GetCssHtml(SortedList<string, string> csslist)
        {
            StringBuilder csshtml = new StringBuilder();
            csshtml.Append("<style type=\"text/css\">");
            for (int i = 0; i < csslist.Count; i++)
            {
                csshtml.Append("." + csslist.ElementAt(i).Key + "{" + csslist.ElementAt(i).Value + "}");
            }
            csshtml.Append("</style>");
            return csshtml.ToString();
        }

        private string SetCssList(string tmphtmlsub, ref SortedList<string, string> csslist)
        {
            try
            {
                SortedList<string, string> tmpcsslist = GetCssList(tmphtmlsub);
                for (int i = 0; i < tmpcsslist.Count; i++)
                {
                    var a = from aaa in csslist
                            where "{" + aaa.Value + "}" == "{" + tmpcsslist.ElementAt(i).Value + "}"
                            select aaa;
                    if (a.Count() > 0)
                    {
                        //tmphtmlsub = tmphtmlsub.Replace("." + tmpcsslist.ElementAt(i).Key + "{" + tmpcsslist.ElementAt(i).Value + "}", "").Replace("class=\"" + tmpcsslist.ElementAt(i).Key.Trim() + "\"", "class=\"" + a.ElementAt(0).Key.Trim() + "\"");
                        tmphtmlsub = tmphtmlsub.Replace("class=\"" + tmpcsslist.ElementAt(i).Key.Trim() + "\"", "class=\"" + a.ElementAt(0).Key.Trim() + "\"");
                    }
                    else
                    {
                        csslist.Add(tmpcsslist.ElementAt(i).Key, tmpcsslist.ElementAt(i).Value);
                    }
                }

                return tmphtmlsub;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        private SortedList<string, string> GetCssList(string tmphtmlsub)
        {
            try
            {
                SortedList<string, string> sl = new SortedList<string, string>();
                string csshtml = "";
                csshtml = tmphtmlsub.Substring(tmphtmlsub.IndexOf("<style type=\"text/css\"><!-- ") + 28, tmphtmlsub.IndexOf("--></style>") - tmphtmlsub.IndexOf("<style type=\"text/css\"><!-- ") - 28);
                string[] csshtmla = csshtml.Split('.');
                for (int i = 0; i < csshtmla.Length; i++)
                {
                    if (i > 0)
                    {
                        sl.Add(csshtmla[i].Substring(0, csshtmla[i].IndexOf('{')), csshtmla[i].Substring(csshtmla[i].IndexOf('{') + 1, csshtmla[i].IndexOf('}') - csshtmla[i].IndexOf('{') - 1));
                    }
                }
                return sl;
            }
            catch (Exception e)
            {
                return null;
            }
        }


    }
}
