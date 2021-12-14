using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using System.Data;
using System.Collections;
using ZhiFang.Common.Public;
using ZhiFang.Common.Log;
using AjaxPro;

namespace ZhiFang.WebLis.Ashx
{
    /// <summary>
    /// $codebehindclassname$ 的摘要说明
    /// </summary>
    [WebService(Namespace = "ZhiFang.WebLis.Ashx")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ReportPrint : IHttpHandler
    {        
        private readonly IBShowFrom showform = BLLFactory<IBShowFrom>.GetBLL("ShowFrom");
        private readonly IBPrintFrom printform = BLLFactory<IBPrintFrom>.GetBLL("PrintFrom");
        private readonly IBReportForm reportform = BLLFactory<IBReportForm>.GetBLL("ReportForm");
        private readonly IBReportFormFull reportformfull = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
        private readonly IBStation_PrinterList station_printerlist = BLLFactory<IBStation_PrinterList>.GetBLL("Station_PrinterList");
        public readonly IBPrintFrom_Weblis Printform_Weblis = BLLFactory<IBPrintFrom_Weblis>.GetBLL("PrintFrom_Weblis");
        public readonly IBPrintFrom_Weblis Printform_WeblisFr3 = BLLFactory<IBPrintFrom_Weblis>.GetBLL("PrintUseDeliph");
        protected readonly ZhiFang.IBLL.Report.IBUserReportFormDataListShowConfig iburfdlsc = ZhiFang.BLLFactory.BLLFactory<IBUserReportFormDataListShowConfig>.GetBLL("UserReportFormDataListShowConfig");
        //public readonly IBPrintFrom Printform = BLLFactory<IBPrintFrom>.GetBLL("PrintFrom");
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }


        [AjaxPro.AjaxMethod]
        public string[] ShowForm(string FormNo, string SectionNo, string PageName, string ShowType)
        {
            try
            {//<?xml version="1.0" encoding="utf-8"?>
                string[] aaa = new string[2];
                int st = ZhiFang.Common.Public.Valid.ToInt(ShowType);
                int sn = ZhiFang.Common.Public.Valid.ToInt(SectionNo);
                ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + "aaa[0]");

                if (int.Parse(ShowType) >= 0)
                {
                    aaa[0] = showform.ShowReportForm(FormNo, sn, PageName, st);
                }
                else
                {
                    aaa[0] = showform.ShowReportFormFrx(FormNo, sn, PageName, st);
                }
                ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + aaa[0]);
                aaa[0] = aaa[0].Substring(aaa[0].IndexOf("<html"), aaa[0].Length - aaa[0].IndexOf("<html"));
                aaa[0] = aaa[0].Replace("\r\n ", " ");
                aaa[1] = ZhiFang.WebLis.Class.ShowTools.ShowFormTypeList(FormNo, sn, PageName);
                return aaa;
            }
            catch
            {
                return new string[] { "", "" };
            }
        }
        [AjaxPro.AjaxMethod]
        public string[] ShowForm_Weblis(string FormNo, string SectionNo, string PageName, string ShowType, string SectionType)
        {
            try
            {//<?xml version="1.0" encoding="utf-8"?>
                string[] aaa = new string[2];
                int st = ZhiFang.Common.Public.Valid.ToInt(ShowType);
                int sn = ZhiFang.Common.Public.Valid.ToInt(SectionNo);
                //ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss")+"aaa[0]");
                if (int.Parse(ShowType) >= 0)
                {
                    if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                    {
                        //用Fr3显示 找模版去数据库
                        aaa[0] = "暂不支持Fr3模版！";//showformFr3.ShowReportForm_Weblis(FormNo, sn, PageName, st, Convert.ToInt32(SectionType));

                    }
                    else
                    {   //用xslt显示 找模版去xml配置文件
                        aaa[0] = showform.ShowReportForm_Weblis(FormNo, sn, PageName, st, Convert.ToInt32(SectionType));
                    }
                }
                else
                {
                    if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                    {
                        aaa[0] = "暂不支持Fr3模版！";//showformFr3.ShowReportFormFrx(FormNo, sn, PageName, st);
                    }
                    else
                    {
                        aaa[0] = showform.ShowReportFormFrx(FormNo, sn, PageName, st);
                    }
                }
                if (aaa[0].IndexOf("<html") >= 0)
                {
                    aaa[0] = aaa[0].Substring(aaa[0].IndexOf("<html"), aaa[0].Length - aaa[0].IndexOf("<html"));
                }
                aaa[0] = aaa[0].Replace("\r\n ", " ");
                //aaa[1] = ShowTools.ShowFormTypeList(FormNo, sn, PageName);
                return aaa;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + e.ToString());
                return new string[] { "", "" };
            }
        }


        [AjaxPro.AjaxMethod]
        public string[] ShowReportPrint_ALL(string FromNo, string SectionNo, string PageName, string ShowType, string SectionType)
        {
            Log.Info(DateTime.Now.ToLongDateString() + "ShowReportPrint_ALL-FromNo:" + FromNo + "SectionNo:" + SectionNo + "PageName:" + PageName + "ShowType:" + ShowType + "SectionType:" + SectionType);

            string[] aaa = new string[2];
            int emplateType = 0; //模版类型方法
            string ShowModel = "";
            string ShowName = "";
            DataTable dtrf = new DataTable();
            DataTable dtTable = new DataTable();
            DataTable dtGraph = new DataTable();
            try
            {
                //取数据
                //判断是否调用打印模版0不调用 1调用  改为是否从数据库读取配置0从XML1从数据库
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template") == "0")
                {
                    Log.Info(DateTime.Now.ToLongDateString() + " 从数据库读取选择模版（Template）:" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template"));
                    //读取xmlConfig
                    //返回路径ShowModel,返回执行方法emplateType
                    ShowModel = showform.ShowReportXML(FromNo, PageName, ShowType, SectionType, out  emplateType);
                    Log.Info(DateTime.Now.ToLongDateString() + " ShowModel:" + ShowModel);

                    ShowName = showform.CheckSupergroup(FromNo, Convert.ToInt32(SectionType), out dtrf, out dtTable, out dtGraph);
                    Log.Info(DateTime.Now.ToLongDateString() + " ShowName:" + ShowName);
                    if (emplateType == 0)
                    {
                        aaa[0] = showform.ShowReportXSLT(dtrf, dtTable, ShowName, ShowModel);
                    }
                    else if (emplateType == 1)
                    {
                        aaa[0] = "暂不支持Fr3模版！";// showform.ShowReportFR3(FromNo, dtrf, dtTable, ShowName, ShowModel);
                    }
                    else if (emplateType == 2)
                    {
                        aaa[0] = showform.ShowReportFRX(FromNo, dtrf, dtTable, ShowName, ShowModel);
                    }
                    else
                    {
                        Log.Info("配置有错误！");
                    }
                }
                else if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template") == "1")
                { //读取数据库
                    Log.Info(DateTime.Now.ToLongDateString() + " 从Xml文件读取选择模版Template:" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("Template"));
                    ShowName = showform.CheckSupergroup(FromNo, Convert.ToInt32(SectionType), out dtrf, out dtTable, out dtGraph);

                    Log.Info(DateTime.Now.ToLongDateString() + " ShowName:" + ShowName);
                    ShowModel = showform.ShowReportSqlDB(FromNo, dtrf, dtTable, SectionType, out  emplateType);
                    Log.Info(DateTime.Now.ToLongDateString() + " ShowModel:" + ShowModel);
                   
                    //ShowName = showform.CheckSupergroup(FromNo, Convert.ToInt32(SectionType), out dtrf, out dtTable, out dtGraph);
                    //Log.Info(DateTime.Now.ToLongDateString() + " ShowName:" + ShowName);

                    aaa[0] = "暂不支持FR3模版!";//showform.ShowReportFR3(FromNo, dtrf, dtTable, ShowName, ShowModel);

                    switch (emplateType)
                    {
                        case 0:
                            aaa[0] = showform.ShowReportXSLT(dtrf, dtTable, ShowName, ShowModel);
                            break;
                        case 1:
                            aaa[0] = "暂不支持FR3模版!";//showform.ShowReportFR3(FromNo, dtrf, dtTable, ShowName, ShowModel);
                            break;
                        case 2:
                            aaa[0] = showform.ShowReportFRX(FromNo, dtrf, dtTable, ShowName, ShowModel);
                            break;
                        default:
                            Log.Info("配置有错误！");
                            break;
                    }
                }
                else
                {
                    Log.Info(DateTime.Now + ":请配置Template！");
                }
                return aaa;
            }
            catch (Exception ex)
            {
                Log.Info(ex.TargetSite + ex.Message);
                return aaa;
            }
            #region 备份
            //    string[] aaa = new string[2];
            //    int st = ZhiFang.Common.Public.Valid.ToInt(ShowType);
            //    int sn = ZhiFang.Common.Public.Valid.ToInt(SectionNo);
            //    string showmodel = "Normal.XSLT";
            //    try
            //    {
            //        SortedList al = this.ShowFormTypeList(Convert.ToInt32(SectionType), PageName);
            //        if (al.Count <= 0)
            //        {
            //            aaa[0] = "显示模板配置文件错误！";
            //            return aaa;
            //        }
            //        else
            //        {
            //            try
            //            {
            //                showmodel = al.GetKey(st).ToString();
            //            }
            //            catch
            //            {
            //                showmodel = al.GetKey(0).ToString();
            //            }
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        ZhiFang.Common.Log.Log.Debug("异常信息:" + ex.ToString());
            //        aaa[0] = "显示模板配置错误！";
            //        return aaa;
            //    }
            //    ZhiFang.Common.Log.Log.Info("显示模板:" + showmodel);

            //    if (int.Parse(ShowType) >= 0)
            //    {
            //        //其他页面（ShowType）
            //        //查询ReportFromShowXslConfig.xml文件

            //        switch (showmodel.ToUpper().Trim().Split('.')[1])//变更 取最后的
            //        {
            //            case "FR3":
            //                aaa[0] = showformFr3.ShowReportForm_Weblis(FormNo, sn, PageName, st, Convert.ToInt32(SectionType));
            //                break;
            //            case "FRX":
            //                aaa[0] = showform.ShowReportForm_Weblis(FormNo, sn, PageName, st, Convert.ToInt32(SectionType));

            //                break;
            //            case "XSLT":
            //                aaa[0] = showform.ShowReportForm(FormNo, sn, PageName, st);
            //                break;
            //            default:
            //                ZhiFang.Common.Log.Log.Info("ReportFromShowXslConfig配置问题");
            //                break;
            //        }
            //    }
            //    else
            //    {
            //        //FrxTest.aspx 这个页面（ShowType）
            //        switch (showmodel.ToUpper().Trim().Split('.')[1])
            //        {
            //            case "FR3":
            //                aaa[0] = showformFr3.ShowReportFormFrx(FormNo, sn, PageName, st);
            //                break;
            //            case "FRX":
            //                aaa[0] = showform.ShowReportFormFrx(FormNo, sn, PageName, st);
            //                break;
            //            case "XSLT":
            //                break;
            //            default:
            //                ZhiFang.Common.Log.Log.Info("ReportFromShowXslConfig配置问题");
            //                break;
            //        }
            //    }
            //    if (aaa[0].IndexOf("<html") >= 0)
            //    {
            //        aaa[0] = aaa[0].Substring(aaa[0].IndexOf("<html"), aaa[0].Length - aaa[0].IndexOf("<html"));
            //    }
            //    aaa[0] = aaa[0].Replace("\r\n ", " ");
            //    return aaa;
            //}
            //catch (Exception e)
            //{
            //    ZhiFang.Common.Log.Log.Debug(DateTime.Now.ToString("YYYYMMDD HH:mm:ss") + e.ToString());
            //    return new string[] { "", "" };
            //}
            #endregion
        }


        public SortedList ShowFormTypeList(int SectionType, string PageName)
        {
            Log.Info("这是ShowFormTypeList()方法:ReportPrint.cs");
            SortedList al = new SortedList();
            DataSet ds = iburfdlsc.ShowFormTypeList("");
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    if (((ZhiFang.Common.Dictionary.SectionType)SectionType).ToString().Trim() == ds.Tables[0].Rows[i]["ReportType"].ToString().Trim() && PageName.Trim() == ds.Tables[0].Rows[i]["PageName"].ToString().Trim())
                    {
                        al.Add(ds.Tables[0].Rows[i]["XSLName"].ToString().Trim(), ds.Tables[0].Rows[i]["Name"].ToString().Trim());
                    }
                }
            }
            return al;
        }
        [AjaxPro.AjaxMethod]
        public string[] GetPageSizeByReportFormList(string FormNo)
        {
            string[] resulta = new string[2];
            try
            {//<?xml version="1.0" encoding="utf-8"?>
                string[] FormNoList = FormNo.Split(',');
                string PageList = "";
                string OnePageName = "";
                string result1 = "";
                string result2 = "";
                for (int i = 0; i < FormNoList.Length; i++)
                {
                    ZhiFang.Model.PrintFormat pf = printform.GetPrintModelInfo(FormNoList[i]);

                    if (pf != null && pf.PaperSize != string.Empty && pf.PaperSize != "")
                    {
                        OnePageName = pf.PaperSize;
                        result2 += FormNoList[i] + "," + OnePageName + "&";
                        if (PageList.IndexOf(OnePageName) < 0)
                        {
                            PageList += OnePageName + ";";
                            ZhiFang.Model.Station_PrinterList spl = new ZhiFang.Model.Station_PrinterList();
                            spl.StationName = System.Net.Dns.GetHostName();
                            spl.PageSize = OnePageName;
                            DataSet ds = station_printerlist.GetList(spl);
                            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                            {
                                result1 += OnePageName + "," + ds.Tables[0].Rows[0]["PrinterName"].ToString() + ";";
                            }
                            else
                            {
                                result1 += OnePageName + ",-1;";
                            }
                        }
                    }
                }
                resulta[0] = result1.Substring(0, result1.LastIndexOf(';'));
                resulta[1] = result2.Substring(0, result2.LastIndexOf('&'));
                return resulta;
            }
            catch
            {
                return resulta;
            }
        }
        [AjaxPro.AjaxMethod]
        public string UpLoadPrinterPageSize(string PrinterName, string PageSize)
        {
            try
            {
                ZhiFang.Model.Station_PrinterList spl = new ZhiFang.Model.Station_PrinterList();
                spl.StationName = System.Net.Dns.GetHostName();
                spl.PageSize = PageSize;
                station_printerlist.Delete(spl);
                spl.PrinterName = PrinterName;
                if (station_printerlist.Add(spl) > 0)
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch
            {
                return "-1";
            }
        }
        [AjaxPro.AjaxMethod]
        public string SetReportFromPrintFlag(string FormNo, string PrintNumber)
        {
            try
            {//<?xml version="1.0" encoding="utf-8"?>
                string aaa = "";
                int pn = ZhiFang.Common.Public.Valid.ToInt(PrintNumber);
                string[] formnoa = FormNo.Split(',');
                for (int i = 0; i < formnoa.Length; i++)
                {
                    ZhiFang.Model.ReportForm rf = reportform.GetModel(formnoa[i].Split(';')[0]);
                    if (rf.PrintTimes != null)
                    {
                        rf.PrintTimes = rf.PrintTimes.Value + pn;
                    }
                    else
                    {
                        rf.PrintTimes = pn;
                    }
                    aaa = reportform.Update(rf).ToString();
                    if (aaa.Trim() != "1")
                    {
                        return "-1";
                    }
                }
                return "1";
            }
            catch
            {
                return "-1";
            }
        }
        [AjaxPro.AjaxMethod]
        public string SetReportFromFullPrintFlag(string FormNo, string PrintNumber)
        {
            try
            {//<?xml version="1.0" encoding="utf-8"?>
                string aaa = "";
                int pn = ZhiFang.Common.Public.Valid.ToInt(PrintNumber);
                string[] formnoa = FormNo.Split(',');
                for (int i = 0; i < formnoa.Length; i++)
                {
                    ZhiFang.Model.ReportFormFull rf = reportformfull.GetModel(formnoa[i].Split(';')[0]);
                    if (rf.PRINTTIMES != null)
                    {
                        rf.PRINTTIMES = rf.PRINTTIMES.Value + pn;
                    }
                    else
                    {
                        rf.PRINTTIMES = pn;
                    }
                    aaa = reportformfull.Update(rf).ToString();
                    if (aaa.Trim() != "1")
                    {
                        return "-1";
                    }
                }
                return "1";
            }
            catch
            {
                return "-1";
            }
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        [AjaxPro.AjaxMethod]
        public string[] ReportPrint_Image(string ReportFormID, string ReportFormTitle, string PrintType)
        {
            ZhiFang.Common.Log.Log.Info("这是ReportPrint_Image(方法)：ReportPrint.ashx");
            try
            {
                string[] ImageList = null;
                string title = "0";
                if (ReportFormTitle != null)
                {
                    if (ReportFormTitle == "Center")
                    {
                        title = "0";
                    }
                    else
                    {
                        if (ReportFormTitle == "Client")
                        {
                            title = "1";
                        }
                        else
                        {
                            if (ReportFormTitle == "Batch")
                            {
                                title = "2";
                            }
                            else
                            {
                                if (ReportFormTitle == "MenZhen")
                                {
                                    title = "3";
                                }
                                else
                                {
                                    if (ReportFormTitle == "ZhuYuan")
                                    {
                                        title = "4";
                                    }
                                    else
                                    {
                                        if (ReportFormTitle == "TiJian")
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

                    if (PrintType != null && PrintType == "A5")
                    {
                        if (ReportFormTitle != null)
                        {
                            ImageList = this.PrintHtml(ReportFormID.Split(','), title);
                        }
                    }
                    else
                    {
                        string[] tmp = ReportFormID.Split(',');
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
                        if (PrintType != null && PrintType == "A4")
                        {
                            if (ReportFormTitle != null)
                            {
                                ImageList = this.PrintMergeHtml(sl, title);
                            }
                        }
                        if (PrintType != null && PrintType == "EA4")
                        {
                            if (ReportFormTitle != null)
                            {
                                ImageList = this.PrintMergeEnHtml(sl, title);
                            }
                        }
                    }
                }
                return ImageList;
            }
            catch
            {
                return null;
            }
        }
        private string[] PrintMergeHtml(SortedList ReportFormID, string title)
        {
            ZhiFang.Common.Log.Log.Info("这是PrintMergeHtml()方法：ReportPrint.ashx");
            ZhiFang.Common.Log.Log.Info(title.ToString() + "---------" + ReportFormID.GetByIndex(0).ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            List<string> tmp;
            List<string> tmplist = new List<string>();
            for (int i = 0; i < ReportFormID.Count; i++)
            {
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    tmp = Printform_WeblisFr3.PrintMergeHtml(ReportFormID.GetByIndex(i).ToString(), (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
                }
                else
                {
                    tmp = Printform_Weblis.PrintMergeHtml(ReportFormID.GetByIndex(i).ToString(), (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));

                }
                if (tmp != null)
                {
                    for (int j = 0; j < tmp.Count; j++)
                    {
                        tmplist.Add(tmp[j]);
                    }
                }
            }
            string[] tmphtml = new string[tmplist.Count];
            for (int i = 0; i < tmplist.Count; i++)
            {
                tmphtml[i] = "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")));
            }
            return tmphtml;
        }
        private string[] PrintMergeEnHtml(SortedList ReportFormID, string title)
        {
            ZhiFang.Common.Log.Log.Info("这是PrintMergeEnHtml()方法：ReportPrint.ashx");
            ZhiFang.Common.Log.Log.Info(title.ToString() + "E---------" + ReportFormID.GetByIndex(0).ToString() + "E---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            List<string> tmp;
            List<string> tmplist = new List<string>();
            for (int i = 0; i < ReportFormID.Count; i++)
            {
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    ZhiFang.Common.Log.Log.Info("这是PrintMergeEnHtml()方法-fr3：ReportPrint.ashx");
                    tmp = Printform_WeblisFr3.PrintMergeHtml(ReportFormID.GetByIndex(i).ToString(), (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("这是PrintMergeEnHtml()方法-frx：ReportPrint.ashx");
                    tmp = Printform_Weblis.PrintMergeHtml(ReportFormID.GetByIndex(i).ToString(), (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
                }
                if (tmp != null)
                {
                    for (int j = 0; j < tmp.Count; j++)
                    {
                        tmplist.Add(tmp[j]);
                    }
                }
            }
            string[] tmphtml = new string[tmplist.Count];
            for (int i = 0; i < tmplist.Count; i++)
            {
                tmphtml[i] = "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")));
            }
            return tmphtml;
        }
        public string[] PrintHtml(string[] ReportFormID, string title)
        {
            ZhiFang.Common.Log.Log.Info("这是PrintHtml()方法：ReportPrint.ashx");
            List<string> tmp;
            List<string> tmplist = new List<string>();
            for (int i = 0; i < ReportFormID.Length; i++)
            {
                if (ConfigHelper.GetConfigString("modelType").ToString() == "fr3")
                {
                    ZhiFang.Common.Log.Log.Info("这是PrintHtml()方法-fr3：ReportPrint.ashx");
                    tmp = Printform_WeblisFr3.PrintHtml(ReportFormID[i].Split(';')[0], (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("这是PrintHtml()方法-frx：ReportPrint.ashx");
                    tmp = Printform_Weblis.PrintHtml(ReportFormID[i].Split(';')[0], (ZhiFang.Common.Dictionary.ReportFormTitle)Convert.ToInt32(title));
                }
                if (tmp != null)
                {
                    for (int j = 0; j < tmp.Count; j++)
                    {
                        tmplist.Add(tmp[j]);
                    }
                }
            }
            string[] tmphtml = new string[tmplist.Count];
            for (int i = 0; i < tmplist.Count; i++)
            {
                tmphtml[i] = "../" + tmplist[i].Substring(tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")), tmplist[i].Length - tmplist[i].LastIndexOf(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")));
            }
            return tmphtml;
        }
        [AjaxPro.AjaxMethod]
        public string DeleteReportFormByReportFormID(string ListReportFormID)
        {
            ZhiFang.Common.Log.Log.Info("管理员删除报告单！报告单数组："+ListReportFormID);
            try
            {
                string[] listrfid = ListReportFormID.Split(',');
                string delinfo="";
                for(int i=0;i<listrfid.Length;i++)
                {
                    if (reportformfull.Delete(listrfid[i].Split(';')[0]) > 0)
                    {
                        delinfo += "报告单ID：" + listrfid[i].Split(';')[0] + "删除成功！\r\n";
                    }
                    else
                    {
                        delinfo += "报告单ID：" + listrfid[i].Split(';')[0] + "删除失败！\r\n";
                    }
                }
                return delinfo;
            }
            catch(Exception e)
            {
                ZhiFang.Common.Log.Log.Info("删除报告单异常：" + e.ToString());
                return e.ToString();
            }
        }
        [AjaxPro.AjaxMethod(HttpSessionStateRequirement.ReadWrite)]
        public string CheckSuperAccount()
        {
            ZhiFang.Common.Log.Log.Info("验证管理员！");
            try
            {
                if (HttpContext.Current.Request.Cookies["ZhiFangUser"].Value == ZhiFang.Common.Public.ConfigHelper.GetConfigString("SuperAccount"))
                {
                    return "true";
                }
                else
                {
                    return "false";
                }                
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Info("验证管理员异常：" + e.ToString());
                return e.ToString();
            }
        }
    }
}
