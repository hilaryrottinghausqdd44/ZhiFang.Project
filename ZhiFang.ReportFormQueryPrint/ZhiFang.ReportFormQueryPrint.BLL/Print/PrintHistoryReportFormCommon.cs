using FastReport;
using FastReport.Export.Html;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

using ZhiFang.Common.Log;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.Factory;
using ZhiFang.ReportFormQueryPrint.IDAL;
using ZhiFang.ReportFormQueryPrint.Model;
using ZhiFang.ReportFormQueryPrint.Model.VO;

namespace ZhiFang.ReportFormQueryPrint.BLL.Print
{
    public class PrintHistoryReportFormCommon
    {
        public static FastReport.EnvironmentSettings eSet = new EnvironmentSettings();
        #region CreatHtml报告内容
        /// <summary>
        /// 生成HTML内容(XSLT模版)
        /// </summary>
        /// <param name="reportform">报告单(dt)</param>
        /// <param name="reportitem">报告项目(dt)</param>
        /// <param name="modulepath">模版路径</param>
        /// <returns></returns>
        public string CreatHtmlContextXslt(DataTable reportform, DataTable reportitem, string modulepath)
        {
            ZhiFang.Common.Log.Log.Debug("CreatHtmlContextXslt");
            if (reportform.Rows.Count > 0)
            {
                //for (int i = 0; i < reportform.Rows.Count; i++)
                //{
                //    foreach (DataColumn c in reportform.Columns)
                //    {
                //        Log.Debug(c.ColumnName+":"+reportform.Rows[i][c.ColumnName].ToString());
                //    }
                //}
                ZhiFang.Common.Log.Log.Debug("CreatHtmlContextXslt.reportform.Rows.Count:" + reportform.Rows.Count);
                //reportform = SetUserImage(reportform);
                string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.ReportFormQueryPrint.Common.TransDataToXML.MergeXML(ZhiFang.ReportFormQueryPrint.Common.TransDataToXML.TransformDTIntoXML(reportform, "WebReportFile", "ReportForm"), ZhiFang.ReportFormQueryPrint.Common.TransDataToXML.TransformDTIntoXML(reportitem, "WebReportFile", "ReportItem"), "WebReportFile"), modulepath);
                //ZhiFang.Common.Log.Log.Debug("CreatHtmlContextXslt3.reportformcount:"+ reportform.Rows.Count + ",reportitem:" + reportitem.Rows.Count + ",tmphtml:=" + tmphtml+ ",reportform:" + reportform.DataSet.GetXml()+ ",reportitem:"+ reportitem.DataSet.GetXml());

                //MemoryStream stream = new MemoryStream();
                //XmlTextWriter writer = new XmlTextWriter(stream, null);
                //writer.Formatting = Formatting.Indented;
                //ZhiFang.ReportFormQueryPrint.Common.TransDataToXML.MergeXML(ZhiFang.ReportFormQueryPrint.Common.TransDataToXML.TransformDTIntoXML(reportform, "WebReportFile", "ReportForm"), ZhiFang.ReportFormQueryPrint.Common.TransDataToXML.TransformDTIntoXML(reportitem, "WebReportFile", "ReportItem"), "WebReportFile").Save(writer);
                //StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
                //stream.Position = 0;
                //string xmlString = sr.ReadToEnd();
                //sr.Close();
                //stream.Close();

                //ZhiFang.Common.Log.Log.Debug("CreatHtmlContextXslt.XML:" + xmlString);
                return tmphtml;
            }
            return null;
        }
        /// <summary>
        /// 生成HTML内容(XSLT模版)
        /// </summary>
        /// <param name="reportform">报告单(dt)</param>
        /// <param name="reportitem">报告项目(dt)</param>
        /// <param name="modulepath">模版路径</param>
        /// <returns></returns>
        public string CreatHtmlContextXslt(DataTable reportform, DataTable reportitem, DataTable reportgraph, string modulepath)
        {
            if (reportform.Rows.Count > 0)
            {
                //reportform = SetUserImage(reportform);
                System.Xml.XmlDocument xmlreportform = ZhiFang.ReportFormQueryPrint.Common.TransDataToXML.TransformDTIntoXML(reportform, "WebReportFile", "ReportForm");
                System.Xml.XmlDocument xmlreportitem = ZhiFang.ReportFormQueryPrint.Common.TransDataToXML.TransformDTIntoXML(reportitem, "WebReportFile", "ReportItem");
                System.Xml.XmlDocument xmlreportgraph = ZhiFang.ReportFormQueryPrint.Common.TransDataToXML.TransformDTIntoXML(reportgraph, "WebReportFile", "ReportGraph");
                System.Xml.XmlDocument tmpxml = ZhiFang.ReportFormQueryPrint.Common.TransDataToXML.MergeXML(xmlreportform, xmlreportitem, "WebReportFile");
                System.Xml.XmlDocument tmpxml1 = ZhiFang.ReportFormQueryPrint.Common.TransDataToXML.MergeXML(tmpxml, xmlreportgraph, "WebReportFile");

                #region 记录日志
                //MemoryStream stream = new MemoryStream();
                //XmlTextWriter writer = new XmlTextWriter(stream, null);
                //writer.Formatting = Formatting.Indented;
                //tmpxml1.Save(writer);
                //StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
                //stream.Position = 0;
                //string xmlString = sr.ReadToEnd();
                //sr.Close();
                //stream.Close();

                //ZhiFang.Common.Log.Log.Debug("CreatHtmlContextXslt.XML:" + xmlString);
                #endregion

                string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(tmpxml1, modulepath);
                return tmphtml;
            }
            return null;
        }

        /// <summary>
        /// 生成HTML内容(FRX模版)
        /// </summary>
        /// <param name="reportform">报告单(dt)</param>
        /// <param name="reportitem">报告项目(dt)</param>
        /// <param name="modulepath">模版路径</param>
        /// <returns></returns>
        public string CreatShowContextByFRX(DataTable reportform, DataTable reportitem, string modulepath)
        {
            return CreatShowContextByFRX(reportform, reportitem, null, modulepath);
        }

        /// <summary>
        /// 生成HTML内容(FRX模版)
        /// </summary>
        /// <param name="reportform">报告单(dt)</param>
        /// <param name="reportitem">报告项目(dt)</param>
        /// <param name="modulepath">模版路径</param>
        /// <returns></returns>
        public string CreatShowContextByFRX(DataTable reportform, DataTable reportitem, DataTable reportgraph, string modulepath)
        {
            DataRow drreportform = reportform.Rows[0];
            ReportFormTitle rft = ReportFormTitle.center;

            string htmlcontext = "";
            List<string> reportpath = new List<string>();
            FastReport.Report report = new FastReport.Report();
            report.Load(modulepath);
            RegeditDataFRX(drreportform, reportitem, ref report, modulepath, rft);
            RegeditImageFRX(drreportform, reportgraph, ref report);
            report.RegisterData(reportform.DataSet);
            report.Prepare();
            string reportformfiletype = "JPG";
            //if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ReportFormFileType").Trim() != "")
            //{
            //    reportformfiletype = ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ReportFormFileType").Trim().ToUpper();
            //}
            switch (reportformfiletype)
            {
                case "MHT":
                    #region MHT
                    FastReport.Export.Mht.MHTExport mhte = new FastReport.Export.Mht.MHTExport();
                    mhte.ImageFormat = FastReport.Export.Html.ImageFormat.Jpeg;
                    mhte.CurPage = 10;
                    //report.c
                    report.Export(mhte, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".mht");
                    string Mhtinnerhtml = ZhiFang.ReportFormQueryPrint.Common.FilesHelper.ReadFileContent(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".mht");
                    for (int i = 2; i < 100; i++)
                    {
                        if (Mhtinnerhtml.IndexOf("<a name=3D\"PageN" + i + "\"></a>") > 0)
                        {
                            Mhtinnerhtml = Mhtinnerhtml.Replace("<a name=3D\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>");
                        }
                        else
                        {
                            break;
                        }
                        if (Mhtinnerhtml.IndexOf("<a name=\"PageN" + i + "\"></a>") > 0)
                        {
                            Mhtinnerhtml = Mhtinnerhtml.Replace("<a name=\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>");
                        }
                        else
                        {
                            break;
                        }
                    }
                    htmlcontext = Mhtinnerhtml;
                    #endregion
                    break;
                case "HTML":
                    #region HTML
                    FastReport.Export.Html.HTMLExport htmle = new HTMLExport();
                    htmle.SinglePage = true;
                    htmle.Navigator = false;
                    htmle.SubFolder = false;
                    report.Export(htmle, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".html");
                    string htmleinnerhtml = ZhiFang.ReportFormQueryPrint.Common.FilesHelper.ReadFileContent(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".html");
                    int stylei = 0;
                    while (htmleinnerhtml.IndexOf(".s" + stylei + " {") >= 0)
                    {
                        htmleinnerhtml = htmleinnerhtml.Replace(".s" + stylei + " {", ".s" + stylei + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "") + " {");
                        htmleinnerhtml = htmleinnerhtml.Replace("class=\"s" + stylei + "\"", "class=\"s" + stylei + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "") + "\"");
                        stylei++;
                    }
                    for (int i = 2; i < 100; i++)
                    {
                        if (htmleinnerhtml.IndexOf("<a name=\"PageN" + i + "\"></a>") > 0)
                        {
                            htmleinnerhtml = htmleinnerhtml.Replace("<a name=\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>").Replace("class=\"page_break\"", "");
                        }
                        else
                        {
                            break;
                        }
                    }
                    ZhiFang.ReportFormQueryPrint.Common.FilesHelper.WriteContext(htmleinnerhtml, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".html");
                    htmlcontext = htmleinnerhtml;
                    #endregion
                    break;
                case "JPG":
                    #region JPG
                    FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                    string imagetype = "jpeg";
                    if (ConfigHelper.GetConfigInt("JpegQuality") != 0)
                    {
                        tmpjpg.JpegQuality = Convert.ToInt32(ConfigHelper.GetConfigInt("JpegQuality"));
                    }
                    tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
                    report.Export(tmpjpg, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + "." + imagetype + "");
                    if (tmpjpg.GeneratedFiles.Count > 0)
                    {
                        foreach (var filefullpath in tmpjpg.GeneratedFiles)
                        {
                            htmlcontext += "<img src='../" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "/" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".JPG' border='0'/>";
                        }
                    }
                    htmlcontext = "<html><head>    <title>报告</title>    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /></head><body > " + htmlcontext + "</body></html>";
                    #endregion
                    break;
                case "PDF":
                    #region PDF
                    FastReport.Export.Pdf.PDFExport tmppdf = new FastReport.Export.Pdf.PDFExport();
                    report.Export(tmppdf, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".PDF");
                    reportpath.Add(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".PDF");
                    htmlcontext = "";
                    #endregion
                    break;
                default: break;
            }
            report.Dispose();
            return htmlcontext;
        }

        /// <summary>
        /// 生成PDF内容(FRX模版)
        /// </summary>
        /// <param name="reportform">报告单(dt)</param>
        /// <param name="reportitem">报告项目(dt)</param>
        /// <param name="modulepath">模版路径</param>
        /// <returns></returns>
        public string CreatPdfContextFRX(DataTable reportform, DataTable reportitem, DataTable reportmicro, string modulepath)
        {
            DataRow drreportform = reportform.Rows[0];

            FastReport.Report report = new FastReport.Report();
            report.Load(modulepath);

            report.RegisterData(reportform, "ReportFormQueryDataSource");
            report.RegisterData(reportitem, "ReportItemQueryDataSource");
            report.RegisterData(reportmicro, "ReportMicroQueryDataSource");
            report.Prepare();
            string reportformfiletype = "PDF";

            switch (reportformfiletype)
            {

                case "PDF":
                    #region PDF
                    FastReport.Export.Pdf.PDFExport tmppdf = new FastReport.Export.Pdf.PDFExport();
                    string path = System.AppDomain.CurrentDomain.BaseDirectory + "MergeAll\\" + drreportform["FormNo"].ToString().Split(';')[0];
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path = path + "\\" + drreportform["FormNo"].ToString() + ".PDF";
                    report.Export(tmppdf, path);

                    #endregion
                    break;
                default: break;
            }
            report.Dispose();
            return "";
        }

        #endregion

        #region Creat报告文件       
        /// <summary>
        /// 生成报告文件(FRX模版)
        /// </summary>
        /// <param name="reportform">报告单(dr)</param>
        /// <param name="reportitem">报告项目(dt)</param>
        /// <param name="templatefullpath">模版路径</param>
        /// <param name="rft">抬头类型</param>
        /// <returns></returns>
        /// 
        public List<ReportFormFilesVO> CreatReportFormFilesByFRX(DataRow reportform, DataTable reportitem, DataTable reportgraph, string templatefullpath, ReportFormTitle rft)
        {
            List<ReportFormFilesVO> reportformfileslist = new List<ReportFormFilesVO>();
            FastReport.Report report = new FastReport.Report();
            ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.读取模版开始");
            report.Load(templatefullpath);
            ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.注册数据开始");
            TextObject TxtPageSize = (TextObject)report.FindObject("Is_Automatic_single_pair");
            if (((TxtPageSize != null) && Validate.IsInt(TxtPageSize.Text.Trim())) && (Convert.ToInt32(TxtPageSize.Text.Trim()) == 1))
            {
                ZhiFang.Common.Log.Log.Debug("CreatReportFormFilesByFRX.自动单双模板注入数据");
                AutoRegeditDataFRX(reportform, reportitem, ref report, templatefullpath, rft);
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("CreatReportFormFilesByFRX.普通模板注入数据");
                RegeditDataFRX(reportform, reportitem, ref report, templatefullpath, rft);
            }
            ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.注册图片数据开始");
            RegeditImageFRX(reportform, reportgraph, ref report);
            ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.注册报告单数据开始");
            report.RegisterData(reportform.Table.DataSet);

            eSet.ReportSettings.ShowProgress = false;
            ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.生成报告文件开始");
            report.Prepare();
            string reportformfiletype = "PDF";
            //if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ReportFormFileType").Trim() != "")
            //{
            //    reportformfiletype = ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ReportFormFileType").Trim().ToUpper();
            //}

            string savepath = GetReportFormPath(reportform);
            string tmppath = GetReportFormRelativePath(reportform);
            ReportFormFilesVO tmpvo = new ReportFormFilesVO();
            if (ZhiFang.ReportFormQueryPrint.Common.FilesHelper.CheckAndCreatDir(savepath))
            {
                switch (reportformfiletype)
                {
                    case "MHT":
                        #region MHT
                        //FastReport.Export.Mht.MHTExport mhte = new FastReport.Export.Mht.MHTExport();
                        //mhte.ImageFormat = FastReport.Export.Html.ImageFormat.Jpeg;
                        //mhte.CurPage = 10;
                        ////report.c
                        //report.Export(mhte, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".mht");
                        //string Mhtinnerhtml = ZhiFang.ReportFormQueryPrint.Common.FilesHelper.ReadContext(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".mht");
                        //for (int i = 2; i < 100; i++)
                        //{
                        //    if (Mhtinnerhtml.IndexOf("<a name=3D\"PageN" + i + "\"></a>") > 0)
                        //    {
                        //        Mhtinnerhtml = Mhtinnerhtml.Replace("<a name=3D\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>");
                        //    }
                        //    else
                        //    {
                        //        break;
                        //    }
                        //    if (Mhtinnerhtml.IndexOf("<a name=\"PageN" + i + "\"></a>") > 0)
                        //    {
                        //        Mhtinnerhtml = Mhtinnerhtml.Replace("<a name=\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>");
                        //    }
                        //    else
                        //    {
                        //        break;
                        //    }
                        //}
                        //ZhiFang.ReportFormQueryPrint.Common.FilesHelper.WriteContext(Mhtinnerhtml, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".mht");

                        //reportformfileslist.Add(ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".mht");
                        #endregion
                        break;
                    case "HTML":
                        #region HTML
                        //FastReport.Export.Html.HTMLExport htmle = new HTMLExport();
                        //htmle.SinglePage = true;
                        //htmle.Navigator = false;
                        //htmle.SubFolder = false;
                        //report.Export(htmle, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".html");
                        //string htmleinnerhtml = ZhiFang.ReportFormQueryPrint.Common.FilesHelper.ReadContext(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".html");
                        //int stylei = 0;
                        //while (htmleinnerhtml.IndexOf(".s" + stylei + " {") >= 0)
                        //{
                        //    htmleinnerhtml = htmleinnerhtml.Replace(".s" + stylei + " {", ".s" + stylei + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "") + " {");
                        //    htmleinnerhtml = htmleinnerhtml.Replace("class=\"s" + stylei + "\"", "class=\"s" + stylei + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "") + "\"");
                        //    stylei++;
                        //}
                        //for (int i = 2; i < 100; i++)
                        //{
                        //    if (htmleinnerhtml.IndexOf("<a name=\"PageN" + i + "\"></a>") > 0)
                        //    {
                        //        htmleinnerhtml = htmleinnerhtml.Replace("<a name=\"PageN" + i + "\"></a>", "<p style=\"page-break-after:always\">&nbsp;</p>").Replace("class=\"page_break\"", "");
                        //    }
                        //    else
                        //    {
                        //        break;
                        //    }
                        //}
                        //ZhiFang.ReportFormQueryPrint.Common.FilesHelper.WriteContext(htmleinnerhtml, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".html");
                        //reportformfileslist.Add(htmleinnerhtml);
                        #endregion
                        break;
                    case "JPG":
                        #region JPG
                        FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
                        string imagetype = "jpeg";
                        if (ConfigHelper.GetConfigInt("JpegQuality") != 0)
                        {
                            tmpjpg.JpegQuality = Convert.ToInt32(ConfigHelper.GetConfigInt("JpegQuality"));
                        }
                        tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
                        report.Export(tmpjpg, savepath + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "." + imagetype + "");

                        tmpvo.JpgPath = new List<string>();
                        tmpvo.PageCount = tmpjpg.GeneratedFiles.Count.ToString();
                        if (tmpjpg.GeneratedFiles.Count > 0)
                        {
                            foreach (var filefullpath in tmpjpg.GeneratedFiles)
                            {
                                tmpvo.JpgPath.Add(filefullpath);

                                //htmlcontext += "<img src='../" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "/" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".JPG' border='0'/>";
                            }
                        }

                        #endregion
                        break;
                    case "PDF":
                        #region PDF
                        HistoryReportFormPDFExport tmppdf = new HistoryReportFormPDFExport();
                        string reportformfileallpath = GetReportFormFullPath(reportform);
                        report.Export(tmppdf, reportformfileallpath);                 
                        tmpvo.PDFPath = tmppath+reportformfileallpath.Replace(savepath, ""); //tmppath + GetReportFormFileName_PDF(reportform);
                        tmpvo.PageCount = tmppdf.PageCount.ToString();

                        float h = ((FastReport.ReportPage)report.FindObject("Page1")).PaperHeight;
                        float w = ((FastReport.ReportPage)report.FindObject("Page1")).PaperWidth;
                        tmpvo.PageName = PageTypeCheck(h, w);
                        tmpvo.ReportFormID = reportform["ReportFormID"].ToString();
                        reportformfileslist.Add(tmpvo);
                        report.Dispose();
                        tmppdf.Dispose();
                        #endregion
                        break;
                    default: break;
                }
            }
            ZhiFang.Common.Log.Log.Debug(this.GetType().Name + ".CreatReportFormFilesByFRX.释放报告模版开始");
            report.Dispose();
            return reportformfileslist;
        }
        #endregion

        #region 查找模版
        /// <summary>
        /// 查找模版
        /// </summary>
        /// <param name="drreportform">报告单(dr)</param>
        /// <param name="dtreportitem">报告项目(dt)</param>
        /// <param name="rft">抬头类型</param>
        /// <returns></returns>
        public string FindTemplate(DataRow drreportform, DataTable dtreportitem, ZhiFang.ReportFormQueryPrint.Common.ReportFormTitle rft,out string log)
        {
            return FindTemplate(drreportform, dtreportitem, null, rft,out log);
        }
        /// <summary>
        /// 查找模版
        /// </summary>
        /// <param name="drreportform">报告单(dr)</param>
        /// <param name="dtreportitem">报告项目(dt)</param>
        /// <param name="dtreportitem1">报告项目(dt)</param>
        /// <param name="rft">抬头类型</param>
        /// <returns></returns>
        public string FindTemplate(DataRow drreportform, DataTable dtreportitem, DataTable dtreportitem1, ZhiFang.ReportFormQueryPrint.Common.ReportFormTitle rft,out string whereLog)
        {
            whereLog = "模板查询条件：";
            try
            {
                IDSectionPrint dalsectionprint = DalFactory<IDSectionPrint>.GetDal("SectionPrint");
                // DataTable dtsectionprint;
                string TemplateFullPath;
                Model.SectionPrint sectionprint = new Model.SectionPrint();
                try
                {
                    sectionprint.SectionNo = int.Parse(drreportform["SectionNo"].ToString());
                }
                catch
                {
                    sectionprint.SectionNo = -1;
                }
                sectionprint.UseDefPrint = 1;
                //dtsectionprint = dalsectionprint.GetList(sectionprint).Tables[0];
                List<SectionPrint> listsectionprint = new List<SectionPrint>();
                listsectionprint = dalsectionprint.GetModelList(sectionprint);

                if (listsectionprint == null || listsectionprint.Count <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug("FindModel.SectionPrint未找到小组号：" + drreportform["SectionNo"].ToString() + "的模版.");
                    return null;
                }
                else
                {
                    bool fitem = false;
                    bool fitemcount = false;
                    bool fMicro = false;
                    string microattributestr = "阳性报告";

                    //bool fclient = false;
                    //bool fsicktype = false;

                    //string str = "";
                    int itemcount = 0;
                    //string where = "";

                    List<long> listitemno = new List<long>();
                    if (listsectionprint.Where(a => a!=null && a.TestItemNo != null && a.TestItemNo.ToString().Trim() != "").Count() > 0)
                    {

                        if (dtreportitem != null && dtreportitem.Rows.Count > 0)
                        {
                            for (int num = 0; num < dtreportitem.Rows.Count; num++)
                            {
                                if (listsectionprint.Exists(a => a.TestItemNo.ToString() == dtreportitem.Rows[num]["ItemNo"].ToString()))
                                {
                                    fitem = true;
                                }
                                listitemno.Add(long.Parse(dtreportitem.Rows[num]["ItemNo"].ToString().Trim()));
                                //str = str + " TestItemNo = " + dtreportitem.Rows[num]["ParItemNo"].ToString() + " or ";
                            }
                        }
                        if (dtreportitem1 != null && dtreportitem1.Rows.Count > 0)
                        {

                            for (int num = 0; num < dtreportitem1.Rows.Count; num++)
                            {
                                if (listsectionprint.Exists(a => a.TestItemNo.ToString() == dtreportitem1.Rows[num]["ItemNo"].ToString()))
                                {
                                    fitem = true;
                                }
                                listitemno.Add(long.Parse(dtreportitem1.Rows[num]["ItemNo"].ToString().Trim()));
                                //str = str + " TestItemNo = " + dtreportitem1.Rows[num]["ParItemNo"].ToString() + " or ";
                            }
                        }
                        //str = str.Substring(0, str.LastIndexOf(" or "));


                        //RecordLog.writeFile(LogTypeEnum.Info, "报告模板查询_匹配特殊项目：" + str);
                        //if (dtsectionprint.Select(" " + str).Count() > 0)
                        //{
                        //    fitem = true;
                        //}
                    }
                    //if (dtsectionprint.Select(" clientno is null ").Count() != dtsectionprint.Rows.Count && drreportform["clientno"] != DBNull.Value)
                    //{
                    //    if (dtsectionprint.Select(" clientno=" + drreportform["clientno"].ToString()).Count() > 0)
                    //    {
                    //        fclient = true;
                    //    }
                    //}
                    if (listsectionprint.Where(a =>a!=null && a.ItemCountMin != null && a.ItemCountMin.ToString().Trim() != "").Count() > 0 || listsectionprint.Where(a => a != null && a.ItemCountMax != null && a.ItemCountMax.ToString().Trim() != "").Count() > 0)
                    {
                        if (dtreportitem != null)
                        {
                            itemcount += dtreportitem.Rows.Count;
                        }
                        if (dtreportitem1 != null)
                        {
                            itemcount += dtreportitem1.Rows.Count;
                        }
                        if (listsectionprint.Where(a => a.ItemCountMin <= itemcount || a.ItemCountMax >= itemcount).Count() > 0)
                        {
                            fitemcount = true;
                        }
                    }

                    //microno为null 是阴性
                    if (dtreportitem.Columns.Contains("MicroNo"))
                    {
                        fMicro = true;
                        for (int i = 0; i < dtreportitem.Rows.Count; i++)
                        {
                            Log.Debug("PrintReportFormCommon.FindTemplate: MicroNo=" + dtreportitem.Rows[i]["MicroNo"]);
                        }

                        if (dtreportitem.Select(" MicroNo is not null").Count() >= 1)
                        {
                            ZhiFang.Common.Log.Log.Debug("FindModel.微生物模版类型：阳性");
                            microattributestr = "阳性报告";
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug("FindModel.微生物模版类型：阴性");
                            microattributestr = "阴性报告";
                        }
                    }else {
                        fMicro = false;
                        Log.Debug("PrintReportFormCommon.FindTemplate: MicroNo没有此列");
                    }
                    Log.Debug("PrintReportFormCommon.FindTemplate: fitem="+fitem+" fMicro="+fMicro+" fitemcount="+fitemcount);
                    IEnumerable<SectionPrint> tmpsectionprint = null;

                    if (fitem == false && fMicro && fitemcount)
                    {
                        whereLog += "不按照特殊项目匹配，微生物模板，有最大值最小值;阴阳性判断。";
                        tmpsectionprint = listsectionprint.Where(a => (a.ItemCountMin <= itemcount || a.ItemCountMax >= itemcount) && a.microattribute == microattributestr);
                        if (!(tmpsectionprint != null && tmpsectionprint.Count() > 0))
                        {
                            ZhiFang.Common.Log.Log.Debug("不判断阴阳性");
                            whereLog += "不按照特殊项目匹配，微生物模板，有最大值最小值;阴阳性判断不存在。";
                            tmpsectionprint = listsectionprint.Where(a => (a.ItemCountMin <= itemcount || a.ItemCountMax >= itemcount));
                        }
                         
						//where = " clientno=" + drreportform["clientno"].ToString() + " and ( ItemCountMin<=" + itemcount + " or ItemCountMax>=" + itemcount + " ) ";
                    }
                    if (fitem == false && fMicro == false && fitemcount)
                    {
                        tmpsectionprint = listsectionprint.Where(a => (a.ItemCountMin <= itemcount || a.ItemCountMax >= itemcount));
                         whereLog += "不按照特殊项目匹配，阴性模板，有最大值最小值。";
						//where = " ( ItemCountMin<=" + itemcount + " or ItemCountMax>=" + itemcount + " ) ";
                    }
                    if (fitem == false && fMicro && fitemcount == false)
                    {
                        whereLog += "不按照特殊项目匹配，阳性模板，没有最大值最小值;阴阳性判断。";
                        tmpsectionprint = listsectionprint.Where(a => a.microattribute == microattributestr);
                        if (!(tmpsectionprint != null && tmpsectionprint.Count() > 0))
                        {
                            ZhiFang.Common.Log.Log.Debug("不判断阴阳性");
                            whereLog += "不按照特殊项目匹配，微生物模板，没有最大值最小值;阴阳性判断不存在。";
                            tmpsectionprint = listsectionprint.Where(a => 1==1);
                        }
						//where = " clientno=" + drreportform["clientno"].ToString() + " and ( ItemCountMin<=" + itemcount + " or ItemCountMax>=" + itemcount + " ) ";
                    }
                    if (fitem && fMicro == false && fitemcount)
                    {
                        tmpsectionprint = listsectionprint.Where(a => (a.ItemCountMin <= itemcount || a.ItemCountMax >= itemcount) && listitemno.Contains(a.TestItemNo.HasValue ? a.TestItemNo.Value : 0));
                        whereLog += "按照特殊项目匹配，阴性模板，有最大值最小值。";
						//where = " " + str + " and ( ItemCountMin<=" + itemcount + " or ItemCountMax>=" + itemcount + " ) ";
                    }
                    if (fitem && fMicro == false && fitemcount == false)
                    {
                        tmpsectionprint = listsectionprint.Where(a => listitemno.Contains(a.TestItemNo.HasValue ? a.TestItemNo.Value : 0));
                         whereLog += "按照特殊项目匹配，阴性模板，没有最大值最小值。";
						//where = " " + str;
                    }
                    if (fitem && fMicro && fitemcount == false)
                    {
                        whereLog += "按照特殊项目匹配，阳性模板，没有最大值最小值;阴阳性判断。";
                        tmpsectionprint = listsectionprint.Where(a => listitemno.Contains(a.TestItemNo.HasValue ? a.TestItemNo.Value : 0) && a.microattribute == microattributestr);
                        if (!(tmpsectionprint != null && tmpsectionprint.Count() > 0)) {
                            ZhiFang.Common.Log.Log.Debug("不判断阴阳性");
                            tmpsectionprint = listsectionprint.Where(a => listitemno.Contains(a.TestItemNo.HasValue ? a.TestItemNo.Value : 0));
                            whereLog += "按照特殊项目匹配，阳性模板，没有最大值最小值;阴阳性判断不存在。";
                        }
						//where = " " + str + " and clientno=" + drreportform["clientno"].ToString();
                    }
                    if (fitem == false && fMicro == false && fitemcount == false)
                    {
                        tmpsectionprint = listsectionprint.Where(a => 1==1);
                        whereLog += "不按照特殊项目匹配，阴性模板，没有最大值最小值。";                       
					   //where = " 1=1 ";
                    }
                    if (fitem && fMicro && fitemcount)
                    {
                        whereLog += "按照特殊项目匹配，阳性模板，有最大值最小值;阴阳性判断。";
                        tmpsectionprint = listsectionprint.Where(a => (a.ItemCountMin <= itemcount || a.ItemCountMax >= itemcount) && a.microattribute == microattributestr && listitemno.Contains(a.TestItemNo.HasValue ? a.TestItemNo.Value : 0));
                        if (!(tmpsectionprint != null && tmpsectionprint.Count() > 0))
                        {
                            ZhiFang.Common.Log.Log.Debug("不判断阴阳性");
                            whereLog += "按照特殊项目匹配，阳性模板，有最大值最小值;阴阳性判断不存在。";
                            tmpsectionprint = listsectionprint.Where(a => (a.ItemCountMin <= itemcount || a.ItemCountMax >= itemcount) && listitemno.Contains(a.TestItemNo.HasValue ? a.TestItemNo.Value : 0));
                        }
                        //where = " " + str + " and clientno=" + drreportform["clientno"].ToString() + " and  ( ItemCountMin<=" + itemcount + " or ItemCountMax>=" + itemcount + " ) ";
                    }
                    if (tmpsectionprint != null && tmpsectionprint.Count() > 0)
                    {
                        foreach (var q in tmpsectionprint)
                        {
                            Log.Debug("PrintReportFormCommon.FindTemplate: sectionPrintList: "+"PrintOrder= "+q.PrintOrder+" SectionNo= "+q.SectionNo+" PrintProgram= "+q.PrintProgram);
                        }
                        tmpsectionprint = tmpsectionprint.OrderBy(a => a.PrintOrder).ThenByDescending(a => a.SPID);
                        TemplateFullPath = tmpsectionprint.ElementAt(0).PrintProgram.ToString();
                        TemplateFullPath = TemplateFullPath.Substring(TemplateFullPath.IndexOf(".exe") + 4);
                        TemplateFullPath = TemplateFullPath.Replace("modelprint.exe ", "").Replace("printrmf.exe ", "").Replace(".frf", "").Replace(".fr3", "").Replace(".rmf", "");
                        TemplateFullPath = System.AppDomain.CurrentDomain.BaseDirectory + SysContractPara.TemplatePath + TemplateFullPath.Trim() + SysContractPara.PrintTemplatextension;
						whereLog += "匹配到模板的字段为：SPID=" + tmpsectionprint.ElementAt(0).SPID + " PrintFormat=" + tmpsectionprint.ElementAt(0).PrintFormat + " PrintProgram=" + tmpsectionprint.ElementAt(0).PrintProgram + " TestItemNo=" + tmpsectionprint.ElementAt(0).TestItemNo + " PrintOrder=" + tmpsectionprint.ElementAt(0).PrintOrder + "。";
						
					}
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("FindModel.未找到匹配的模版.");
                        return null;
                    }
                    ZhiFang.Common.Log.Log.Debug("FindModel.找到匹配的模版，路径：" + TemplateFullPath);
                     return TemplateFullPath;
                }

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("FindModel.查找报告模版异常：" + e.ToString());
                return null;
            }
            #region
            //try
            //{
            //    IDSectionPrint dalsectionprint = DalFactory<IDSectionPrint>.GetDal("SectionPrint");
            //    DataTable dtsectionprint;
            //    string TemplateFullPath;
            //    Model.SectionPrint t = new Model.SectionPrint();
            //    try
            //    {
            //        t.SectionNo = int.Parse(drreportform["SectionNo"].ToString());
            //    }
            //    catch
            //    {
            //        t.SectionNo = -1;
            //    }
            //    t.UseDefPrint = 1;
            //    dtsectionprint = dalsectionprint.GetList(t).Tables[0];
            //    if (dtsectionprint.Rows.Count <= 0)
            //    {
            //        return null;
            //    }
            //    else
            //    {
            //        bool fitem = false;
            //        bool fclient = false;
            //        bool fitemcount = false;
            //        string fMicro = "false";
            //        string str = "";
            //        int itemcount = 0;
            //        string where = "";
            //        if (dtsectionprint.Select(" TestItemNo is null ").Count() != dtsectionprint.Rows.Count)
            //        {

            //            if (dtreportitem != null && dtreportitem.Rows.Count > 0)
            //            {
            //                for (int num = 0; num < dtreportitem.Rows.Count; num++)
            //                {
            //                    str = str + " TestItemNo = " + dtreportitem.Rows[num]["ParItemNo"].ToString() + " or ";
            //                }
            //            }
            //            if (dtreportitem1 != null && dtreportitem1.Rows.Count > 0)
            //            {
            //                for (int num = 0; num < dtreportitem1.Rows.Count; num++)
            //                {
            //                    str = str + " TestItemNo = " + dtreportitem1.Rows[num]["ParItemNo"].ToString() + " or ";
            //                }
            //            }
            //            str = str.Substring(0, str.LastIndexOf(" or "));
            //            if (dtsectionprint.Select(" " + str).Count() > 0)
            //            {
            //                fitem = true;
            //            }
            //        }
            //        if (dtsectionprint.Select(" clientno is null ").Count() != dtsectionprint.Rows.Count && drreportform["clientno"] != DBNull.Value)
            //        {
            //            if (dtsectionprint.Select(" clientno=" + drreportform["clientno"].ToString()).Count() > 0)
            //            {
            //                fclient = true;
            //            }
            //        }
            //        if (dtsectionprint.Select(" ItemCountMin is null and ItemCountMax is null ").Count() != dtsectionprint.Rows.Count)
            //        {
            //            if (dtreportitem != null)
            //            {
            //                itemcount += dtreportitem.Rows.Count;
            //            }
            //            if (dtreportitem1 != null)
            //            {
            //                itemcount += dtreportitem1.Rows.Count;
            //            }
            //            if (dtsectionprint.Select(" ItemCountMin<=" + itemcount + " or ItemCountMax>=" + itemcount).Count() > 0)
            //            {
            //                fitemcount = true;
            //            }
            //        }
            //        //microno为null 是阴性
            //        //if (table.Columns.Contains("MicroNo"))
            //        //{
            //        //    if (table.Select(" MicroNo is not null").Count() >= 1)
            //        //    {
            //        //        fMicro = "true";
            //        //    }
            //        //    else
            //        //    {
            //        //        fMicro = "middle";
            //        //    }
            //        //}
            //        if (fitem == false && fclient && fitemcount)
            //        {
            //            where = " clientno=" + drreportform["clientno"].ToString() + " and ( ItemCountMin<=" + itemcount + " or ItemCountMax>=" + itemcount + " ) ";
            //        }
            //        if (fitem == false && fclient == false && fitemcount)
            //        {
            //            where = " ( ItemCountMin<=" + itemcount + " or ItemCountMax>=" + itemcount + " ) ";
            //        }
            //        if (fitem == false && fclient == false && fitemcount == false)
            //        {
            //            where = " 1=1 ";
            //        }
            //        if (fitem && fclient == false && fitemcount)
            //        {
            //            where = " " + str + " and ( ItemCountMin<=" + itemcount + " or ItemCountMax>=" + itemcount + " ) ";
            //        }
            //        if (fitem && fclient == false && fitemcount == false)
            //        {
            //            where = " " + str;
            //        }
            //        if (fitem && fclient && fitemcount == false)
            //        {
            //            where = " " + str + " and clientno=" + drreportform["clientno"].ToString();
            //        }
            //        if (fitem && fclient && fitemcount)
            //        {
            //            where = " " + str + " and clientno=" + drreportform["clientno"].ToString() + " and  ( ItemCountMin<=" + itemcount + " or ItemCountMax>=" + itemcount + " ) ";
            //        }
            //        //if (fMicro == "true")
            //        //{
            //        //    where += " and Microattribute ='阳性报告' ";
            //        //}
            //        //else if (fMicro == "middle")
            //        //{
            //        //    where += " and Microattribute ='阴性报告' ";
            //        //}
            //        DataRow[] dra = dtsectionprint.Select(where, "PrintOrder asc,SPID desc");
            //        //RecordLog.writeFile(LogTypeEnum.Info, "报告模板查询Sub：" + where + ",排序：Sort asc,Id desc");
            //        if (dra.Count() > 0)
            //        {
            //            TemplateFullPath = dra[0]["PrintProgram"].ToString();
            //            //RecordLog.writeFile(LogTypeEnum.Info, "报告模板ID：" + TemplateFullPath);
            //        }
            //        else
            //        {
            //            return null;
            //        }
            //        return TemplateFullPath;
            //    }

            //}
            //catch (Exception ex)
            //{
            //    //RecordLog.writeFile(LogTypeEnum.Error, ex.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            //    return "";
            //}
            #endregion
        }
        
        #endregion

        #region 向模版注入数据
        public void RegeditDataFRX(DataRow drreportform, DataTable dtreportitem, ref FastReport.Report report, string TemplateFullPath, ReportFormTitle rft)
        {
            try
            {
                string sectiontype = "0";
                if (drreportform["SECTIONTYPE"] != null && drreportform["SECTIONTYPE"].ToString().Trim() != "")
                {
                    sectiontype = drreportform["SECTIONTYPE"].ToString();
                }
                switch ((SectionType)Convert.ToInt32(sectiontype))
                {
                    case SectionType.CellMorphology:
                        #region 细胞形态学
                        for (int i = 0; i < dtreportitem.Rows.Count; i++)
                        {
                            TextObject text = (TextObject)report.FindObject("TextM" + dtreportitem.Rows[i]["ItemNo"].ToString());
                            if (text != null)
                            {
                                if (dtreportitem.Columns.Contains("ReportValue"))
                                {
                                    text.Text += dtreportitem.Rows[i]["ReportValue"].ToString();
                                }
                                if (dtreportitem.Columns.Contains("ReportDesc"))
                                {
                                    text.Text += dtreportitem.Rows[i]["ReportDesc"].ToString();
                                }
                                if (dtreportitem.Columns.Contains("ReportText"))
                                {
                                    text.Text += dtreportitem.Rows[i]["ReportText"].ToString();
                                }
                            }
                            TextObject textBN = (TextObject)report.FindObject("TextBN" + dtreportitem.Rows[i]["ItemNo"].ToString());
                            if (textBN != null)
                            {
                                if (dtreportitem.Columns.Contains("BloodNum"))
                                {
                                    textBN.Text += dtreportitem.Rows[i]["BloodNum"].ToString();
                                }
                            }
                            TextObject textBP = (TextObject)report.FindObject("TextBP" + dtreportitem.Rows[i]["ItemNo"].ToString());
                            if (textBP != null)
                            {
                                if (dtreportitem.Columns.Contains("BloodPercent"))
                                {
                                    textBP.Text += dtreportitem.Rows[i]["BloodPercent"].ToString();
                                }
                            }
                            TextObject textMN = (TextObject)report.FindObject("TextMN" + dtreportitem.Rows[i]["ItemNo"].ToString());
                            if (textMN != null)
                            {
                                if (dtreportitem.Columns.Contains("MarrowNum"))
                                {
                                    textMN.Text += dtreportitem.Rows[i]["MarrowNum"].ToString();
                                }
                            }
                            TextObject textMP = (TextObject)report.FindObject("TextMP" + dtreportitem.Rows[i]["ItemNo"].ToString());
                            if (textMP != null)
                            {
                                if (dtreportitem.Columns.Contains("MarrowPercent"))
                                {
                                    textMP.Text += dtreportitem.Rows[i]["MarrowPercent"].ToString();
                                }
                            }
                            TextObject textBD = (TextObject)report.FindObject("TextBD" + dtreportitem.Rows[i]["ItemNo"].ToString());
                            if (textBD != null)
                            {
                                if (dtreportitem.Columns.Contains("BloodDesc"))
                                {
                                    textBD.Text += dtreportitem.Rows[i]["BloodDesc"].ToString();
                                }
                            }
                            TextObject textMD = (TextObject)report.FindObject("TextMD" + dtreportitem.Rows[i]["ItemNo"].ToString());
                            if (textMD != null)
                            {
                                if (dtreportitem.Columns.Contains("MarrowDesc"))
                                {
                                    textMD.Text += dtreportitem.Rows[i]["MarrowDesc"].ToString();
                                }
                            }
                            TextObject textRR = (TextObject)report.FindObject("TextRR" + dtreportitem.Rows[i]["ItemNo"].ToString());
                            if (textRR != null)
                            {
                                if (dtreportitem.Columns.Contains("RefRange"))
                                {
                                    textRR.Text += dtreportitem.Rows[i]["RefRange"].ToString();
                                }
                            }
                        }
                        #endregion
                        break;
                    case SectionType.FishCheck:
                        #region Fish检测（图）
                        for (int i = 0; i < dtreportitem.Rows.Count; i++)
                        {
                            if (dtreportitem.Columns.Contains("ItemNo"))
                            {
                                if (dtreportitem.Columns.Contains("CItemNo"))
                                {
                                    CheckBoxObject CheckBox = (CheckBoxObject)report.FindObject("CheckBoxM" + dtreportitem.Rows[i]["ItemNo"].ToString() + "C" + dtreportitem.Rows[i]["CItemNo"].ToString());

                                    if (CheckBox != null)
                                    {
                                        CheckBox.Checked = true;
                                    }
                                }
                                TextObject text = (TextObject)report.FindObject("TextM" + dtreportitem.Rows[i]["ItemNo"].ToString());

                                if (text != null)
                                {
                                    if (dtreportitem.Columns.Contains("ReportValue"))
                                    {
                                        text.Text += dtreportitem.Rows[i]["ReportValue"].ToString();
                                    }
                                    if (dtreportitem.Columns.Contains("ReportDesc"))
                                    {
                                        text.Text += dtreportitem.Rows[i]["ReportDesc"].ToString();
                                    }
                                    if (dtreportitem.Columns.Contains("ReportText"))
                                    {
                                        text.Text += dtreportitem.Rows[i]["ReportText"].ToString();
                                    }
                                }
                            }
                        }
                        #endregion
                        break;
                    case SectionType.PathologyCheck:
                        #region 病理检测（图）
                        for (int i = 0; i < dtreportitem.Rows.Count; i++)
                        {
                            if (dtreportitem.Columns.Contains("ItemNo"))
                            {
                                if (dtreportitem.Columns.Contains("CItemNo"))
                                {
                                    CheckBoxObject CheckBox = (CheckBoxObject)report.FindObject("CheckBoxM" + dtreportitem.Rows[i]["ItemNo"].ToString() + "C" + dtreportitem.Rows[i]["CItemNo"].ToString());

                                    if (CheckBox != null)
                                    {
                                        CheckBox.Checked = true;
                                    }
                                }
                                TextObject text = (TextObject)report.FindObject("TextM" + dtreportitem.Rows[i]["ItemNo"].ToString());

                                if (text != null)
                                {
                                    if (dtreportitem.Columns.Contains("ReportValue"))
                                    {
                                        text.Text += dtreportitem.Rows[i]["ReportValue"].ToString();
                                    }
                                    if (dtreportitem.Columns.Contains("ReportDesc"))
                                    {
                                        text.Text += dtreportitem.Rows[i]["ReportDesc"].ToString();
                                    }
                                    if (dtreportitem.Columns.Contains("ReportText"))
                                    {
                                        text.Text += dtreportitem.Rows[i]["ReportText"].ToString();
                                    }
                                }
                            }
                        }
                        #endregion
                        break;
                    default:
                        #region 普通
                        DataSet dsitem1;
                        DataTable dtitem1;
                        DataSet dsitem2;
                        DataTable dtitem2;
                        dsitem1 = new DataSet();
                        dtitem1 = new DataTable("fritem1");
                        dsitem2 = new DataSet();
                        dtitem2 = new DataTable("fritem2");
                        dtreportitem.Columns.Add("RowId", typeof(string));
                        int rowid = 0;
                        while (rowid < dtreportitem.Rows.Count)
                        {
                            dtreportitem.Rows[rowid]["RowId"] = (rowid + 1).ToString();
                            rowid++;
                        }
                        TextObject TxtPageSize = (TextObject)report.FindObject("TxtPageSize");
                        if (((TxtPageSize != null) && Validate.IsInt(TxtPageSize.Text.Trim())) && (Convert.ToInt32(TxtPageSize.Text.Trim()) < dtreportitem.Rows.Count))
                        {
                            int PageSize = int.Parse(TxtPageSize.Text.Trim());

                            SubreportObject Subreport1;
                            SubreportObject Subreport2;
                            LineObject Line7;
                            int PageCount = (dtreportitem.Rows.Count / PageSize) + 1;

                            if (PageCount % 2 == 0)//执行所有页面双列
                            {
                                dtitem1 = dtreportitem.Clone();
                                dtitem2 = dtreportitem.Clone();
                                for (int i = 0; i < dtreportitem.Rows.Count; i++)
                                {
                                    dtitem1.Rows.Add(dtreportitem.Rows[i].ItemArray);
                                }
                                dtitem1.TableName = "fritem1";
                                dsitem1.Tables.Add(dtitem1);
                                dtitem2.TableName = "fritem2";
                                dsitem2.Tables.Add(dtitem2);
                                Subreport1 = (SubreportObject)report.FindObject("Subreport1");
                                if (Subreport1 != null)
                                    Subreport1.Visible = true;
                                Subreport2 = (SubreportObject)report.FindObject("Subreport2");
                                if (Subreport2 != null)
                                    Subreport2.Visible = true;
                                Line7 = (LineObject)report.FindObject("Line7");
                                if (Line7 != null)
                                {
                                    Line7.Visible = true;
                                }
                            }
                            else//执行前面是双列最后一些是单列
                            {
                                dtitem1 = dtreportitem.Clone();
                                dtitem2 = dtreportitem.Clone();
                                for (int i = 0; i < dtreportitem.Rows.Count; i++)
                                {
                                    dtitem1.Rows.Add(dtreportitem.Rows[i].ItemArray);
                                    if (i > (PageCount - 1) * PageSize)
                                    {
                                        dtitem2.Rows.Add(dtreportitem.Rows[i].ItemArray);
                                    }
                                }
                                dtitem1.TableName = "fritem1";
                                dsitem1.Tables.Add(dtitem1);
                                dtitem2.TableName = "fritem2";
                                dsitem2.Tables.Add(dtitem2);
                                Subreport1 = (SubreportObject)report.FindObject("Subreport1");
                                if (Subreport1 != null)
                                    Subreport1.Visible = true;
                                Subreport2 = (SubreportObject)report.FindObject("Subreport2");
                                if (Subreport2 != null)
                                    Subreport2.Visible = true;
                                Line7 = (LineObject)report.FindObject("Line7");
                                if (Line7 != null)
                                {
                                    Line7.Visible = true;
                                }
                            }
                            report.RegisterData(dtreportitem.DataSet);
                            report.RegisterData(dsitem1);
                            report.RegisterData(dsitem2);
                        }
                        else
                        {
                            if (dtreportitem.DataSet == null)
                                break;
                            report.RegisterData(dtreportitem.DataSet);
                        }

                        break;
                        #endregion
                }
                #region 注释
                //FastReport.TextObject txtAdd = (FastReport.TextObject)report.FindObject("txtAdd");
                //string txtAddTitle = "";
                //if (txtAdd != null)
                //{
                //    txtAddTitle = txtAdd.Text;
                //}
                //if (rft == ReportFormTitle.center)
                //{
                //    FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                //    if (texttitle != null)
                //    {
                //        texttitle.Text = ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("CenterName") + txtAddTitle + "";
                //    }
                //    FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                //    if (p != null)
                //    {
                //        //byte[] poperatorimage;
                //        //System.IO.MemoryStream aaa = new System.IO.MemoryStream(pcheckerimage);
                //        //System.Drawing.Image a ;
                //        //a = p.Image;
                //        //a.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"ReportPrint\PrintImage\logo.jpg");
                //        //p.ImageLocation = @"D:\project\ljjPJ\vs2008\Report\Report\TmpHtmlPath\aaa.files\aaa.png";
                //        p.Visible = true;
                //    }
                //    FastReport.PictureObject p1 = (FastReport.PictureObject)report.FindObject("logo");
                //    if (p1 != null)
                //    {
                //        //byte[] poperatorimage;
                //        //System.IO.MemoryStream aaa = new System.IO.MemoryStream(pcheckerimage);
                //        //System.Drawing.Image a ;
                //        //a = p.Image 
                //        //a.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"ReportPrint\PrintImage\logo.jpg");
                //        //p.ImageLocation = @"D:\project\ljjPJ\vs2008\Report\Report\TmpHtmlPath\aaa.files\aaa.png";
                //        p1.Visible = true;
                //    }
                //}
                //if (rft == ReportFormTitle.client)
                //{
                //    FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                //    if (texttitle != null)
                //    {
                //        texttitle.Text = drreportform["CLIENTNAME"].ToString() + txtAddTitle + "";
                //    }
                //    FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                //    if (p != null)
                //    {
                //        //System.Drawing.Image a;
                //        //a = p.Image;
                //        //a.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"ReportPrint\PrintImage\logo.jpg");
                //        p.Visible = false;
                //    }
                //    FastReport.PictureObject p1 = (FastReport.PictureObject)report.FindObject("logo");
                //    if (p1 != null)
                //    {
                //        p1.Visible = false;
                //    }
                //}
                #endregion
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("RegeditDataFRX.异常:"+ex.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            }
        }
        #endregion

        #region 向模版注入数据 自动单双
        public void AutoRegeditDataFRX(DataRow drreportform, DataTable dtreportitem, ref FastReport.Report report, string TemplateFullPath, ReportFormTitle rft)
        {
            try
            {
                Log.Debug("AutoRegeditDataFRX.开始注入");
                string sectiontype = "0";
                if (drreportform["SECTIONTYPE"] != null && drreportform["SECTIONTYPE"].ToString().Trim() != "")
                {
                    sectiontype = drreportform["SECTIONTYPE"].ToString();
                }
                switch ((SectionType)Convert.ToInt32(sectiontype))
                {
                    #region 注释
                    //case SectionType.CellMorphology:
                    //    #region 细胞形态学
                    //    for (int i = 0; i < dtreportitem.Rows.Count; i++)
                    //    {
                    //        TextObject text = (TextObject)report.FindObject("TextM" + dtreportitem.Rows[i]["ItemNo"].ToString());
                    //        if (text != null)
                    //        {
                    //            if (dtreportitem.Columns.Contains("ReportValue"))
                    //            {
                    //                text.Text += dtreportitem.Rows[i]["ReportValue"].ToString();
                    //            }
                    //            if (dtreportitem.Columns.Contains("ReportDesc"))
                    //            {
                    //                text.Text += dtreportitem.Rows[i]["ReportDesc"].ToString();
                    //            }
                    //            if (dtreportitem.Columns.Contains("ReportText"))
                    //            {
                    //                text.Text += dtreportitem.Rows[i]["ReportText"].ToString();
                    //            }
                    //        }
                    //        TextObject textBN = (TextObject)report.FindObject("TextBN" + dtreportitem.Rows[i]["ItemNo"].ToString());
                    //        if (textBN != null)
                    //        {
                    //            if (dtreportitem.Columns.Contains("BloodNum"))
                    //            {
                    //                textBN.Text += dtreportitem.Rows[i]["BloodNum"].ToString();
                    //            }
                    //        }
                    //        TextObject textBP = (TextObject)report.FindObject("TextBP" + dtreportitem.Rows[i]["ItemNo"].ToString());
                    //        if (textBP != null)
                    //        {
                    //            if (dtreportitem.Columns.Contains("BloodPercent"))
                    //            {
                    //                textBP.Text += dtreportitem.Rows[i]["BloodPercent"].ToString();
                    //            }
                    //        }
                    //        TextObject textMN = (TextObject)report.FindObject("TextMN" + dtreportitem.Rows[i]["ItemNo"].ToString());
                    //        if (textMN != null)
                    //        {
                    //            if (dtreportitem.Columns.Contains("MarrowNum"))
                    //            {
                    //                textMN.Text += dtreportitem.Rows[i]["MarrowNum"].ToString();
                    //            }
                    //        }
                    //        TextObject textMP = (TextObject)report.FindObject("TextMP" + dtreportitem.Rows[i]["ItemNo"].ToString());
                    //        if (textMP != null)
                    //        {
                    //            if (dtreportitem.Columns.Contains("MarrowPercent"))
                    //            {
                    //                textMP.Text += dtreportitem.Rows[i]["MarrowPercent"].ToString();
                    //            }
                    //        }
                    //        TextObject textBD = (TextObject)report.FindObject("TextBD" + dtreportitem.Rows[i]["ItemNo"].ToString());
                    //        if (textBD != null)
                    //        {
                    //            if (dtreportitem.Columns.Contains("BloodDesc"))
                    //            {
                    //                textBD.Text += dtreportitem.Rows[i]["BloodDesc"].ToString();
                    //            }
                    //        }
                    //        TextObject textMD = (TextObject)report.FindObject("TextMD" + dtreportitem.Rows[i]["ItemNo"].ToString());
                    //        if (textMD != null)
                    //        {
                    //            if (dtreportitem.Columns.Contains("MarrowDesc"))
                    //            {
                    //                textMD.Text += dtreportitem.Rows[i]["MarrowDesc"].ToString();
                    //            }
                    //        }
                    //        TextObject textRR = (TextObject)report.FindObject("TextRR" + dtreportitem.Rows[i]["ItemNo"].ToString());
                    //        if (textRR != null)
                    //        {
                    //            if (dtreportitem.Columns.Contains("RefRange"))
                    //            {
                    //                textRR.Text += dtreportitem.Rows[i]["RefRange"].ToString();
                    //            }
                    //        }
                    //    }
                    //    #endregion
                    //    break;
                    //case SectionType.FishCheck:
                    //    #region Fish检测（图）
                    //    for (int i = 0; i < dtreportitem.Rows.Count; i++)
                    //    {
                    //        if (dtreportitem.Columns.Contains("ItemNo"))
                    //        {
                    //            if (dtreportitem.Columns.Contains("CItemNo"))
                    //            {
                    //                CheckBoxObject CheckBox = (CheckBoxObject)report.FindObject("CheckBoxM" + dtreportitem.Rows[i]["ItemNo"].ToString() + "C" + dtreportitem.Rows[i]["CItemNo"].ToString());

                    //                if (CheckBox != null)
                    //                {
                    //                    CheckBox.Checked = true;
                    //                }
                    //            }
                    //            TextObject text = (TextObject)report.FindObject("TextM" + dtreportitem.Rows[i]["ItemNo"].ToString());

                    //            if (text != null)
                    //            {
                    //                if (dtreportitem.Columns.Contains("ReportValue"))
                    //                {
                    //                    text.Text += dtreportitem.Rows[i]["ReportValue"].ToString();
                    //                }
                    //                if (dtreportitem.Columns.Contains("ReportDesc"))
                    //                {
                    //                    text.Text += dtreportitem.Rows[i]["ReportDesc"].ToString();
                    //                }
                    //                if (dtreportitem.Columns.Contains("ReportText"))
                    //                {
                    //                    text.Text += dtreportitem.Rows[i]["ReportText"].ToString();
                    //                }
                    //            }
                    //        }
                    //    }
                    //    #endregion
                    //    break;
                    //case SectionType.PathologyCheck:
                    //    #region 病理检测（图）
                    //    for (int i = 0; i < dtreportitem.Rows.Count; i++)
                    //    {
                    //        if (dtreportitem.Columns.Contains("ItemNo"))
                    //        {
                    //            if (dtreportitem.Columns.Contains("CItemNo"))
                    //            {
                    //                CheckBoxObject CheckBox = (CheckBoxObject)report.FindObject("CheckBoxM" + dtreportitem.Rows[i]["ItemNo"].ToString() + "C" + dtreportitem.Rows[i]["CItemNo"].ToString());

                    //                if (CheckBox != null)
                    //                {
                    //                    CheckBox.Checked = true;
                    //                }
                    //            }
                    //            TextObject text = (TextObject)report.FindObject("TextM" + dtreportitem.Rows[i]["ItemNo"].ToString());

                    //            if (text != null)
                    //            {
                    //                if (dtreportitem.Columns.Contains("ReportValue"))
                    //                {
                    //                    text.Text += dtreportitem.Rows[i]["ReportValue"].ToString();
                    //                }
                    //                if (dtreportitem.Columns.Contains("ReportDesc"))
                    //                {
                    //                    text.Text += dtreportitem.Rows[i]["ReportDesc"].ToString();
                    //                }
                    //                if (dtreportitem.Columns.Contains("ReportText"))
                    //                {
                    //                    text.Text += dtreportitem.Rows[i]["ReportText"].ToString();
                    //                }
                    //            }
                    //        }
                    //    }
                    //    #endregion
                    //    break;
                    #endregion
                    default:
                    #region 普通
                        DataSet dsitem1;
                        DataTable dtitem1;
                        DataSet dsitem2;
                        DataTable dtitem2;
                        dsitem1 = new DataSet();
                        dtitem1 = new DataTable("fritem1");
                        dsitem2 = new DataSet();
                        dtitem2 = new DataTable("fritem2");
                        dtreportitem.Columns.Add("RowId", typeof(string));
                        int rowid = 0;
                        while (rowid < dtreportitem.Rows.Count)
                        {
                            dtreportitem.Rows[rowid]["RowId"] = (rowid + 1).ToString();
                            rowid++;
                        }
                        TextObject TxtPageSize = (TextObject)report.FindObject("PageRows");
                        int PageRow = int.Parse(TxtPageSize.Text.Trim());
                        int drCount = dtreportitem.Rows.Count;
                        if (drCount <= PageRow)
                        {
                            Log.Debug("AutoRegeditDataFRX.总数量小于等于一张单列");
                            dtitem1 = dtreportitem.Clone();
                            for (int i =0 ; i < dtreportitem.Rows.Count; i++)
                            {
                                dtitem1.Rows.Add(dtreportitem.Rows[i].ItemArray);
                            }
                            dtitem1.TableName = "fritem1";
                            dsitem1.Tables.Add(dtitem1);


                            dtitem2 = dtreportitem.Clone();
                            for (int i = 0; i < dtreportitem.Rows.Count; i++)
                            {
                                dtitem2.Rows.Add(dtreportitem.Rows[i].ItemArray);
                            }
                            dtitem2.TableName = "fritem2";
                            dsitem2.Tables.Add(dtitem2);

                            SubreportObject Subreport2 = (SubreportObject)report.FindObject("Subreport2");
                            if (Subreport2 != null)
                            {
                                Subreport2.Visible = true;
                            }
                            FastReport.PageBase p = (PageBase)report.FindObject("Page1");
                            FastReport.PageBase p2 = (PageBase)report.FindObject("Page2");
                            p.Visible = false;
                            p2.Visible = true;
                            report.RegisterData(dsitem1);
                            report.RegisterData(dsitem2);
                            break;
                        }
                        Log.Debug("AutoRegeditDataFRX.flag:" + (drCount > PageRow && drCount <= (PageRow * 2)));
                        if (drCount > PageRow && drCount <= (PageRow * 2))
                        {
                            Log.Debug("AutoRegeditDataFRX.总数量在一张双列之间");
                            dtitem1 = dtreportitem.Clone();
                            for (int i = 0; i < dtreportitem.Rows.Count; i++)
                            {
                                dtitem1.Rows.Add(dtreportitem.Rows[i].ItemArray);
                            }
                            dtitem1.TableName = "fritem1";
                            dsitem1.Tables.Add(dtitem1);


                            dtitem2 = dtreportitem.Clone();
                            for (int i = 0; i < dtreportitem.Rows.Count; i++)
                            {
                                dtitem2.Rows.Add(dtreportitem.Rows[i].ItemArray);
                            }
                            dtitem2.TableName = "fritem2";
                            dsitem2.Tables.Add(dtitem2);

                            SubreportObject Subreport1 = (SubreportObject)report.FindObject("Subreport1");
                            if (Subreport1 != null)
                            {
                                Subreport1.Visible = true;
                            }
                            FastReport.PageBase p = (PageBase)report.FindObject("Page1");
                            FastReport.PageBase p2 = (PageBase)report.FindObject("Page2");
                            p.Visible = true;
                            p2.Visible = false;
                            report.RegisterData(dsitem1);
                            report.RegisterData(dsitem2);
                            break;
                        }

                        int if_one = 0;
                        Log.Debug(" pageRow:" + PageRow.ToString() + " drCount:" + drCount.ToString());
                        while (true)
                        {
                            if_one = drCount % (PageRow * 2);
                            if (if_one != 0 && if_one <= PageRow)
                            {
                                Log.Debug("AutoRegeditDataFRX.单列结尾");
                                break;
                            }
                            else
                            {
                                if (if_one == 0 || if_one <= (PageRow * 2))
                                {
                                    Log.Debug("AutoRegeditDataFRX.双列结尾");
                                    break;
                                }
                                drCount = if_one;
                            }
                        }
                        drCount = dtreportitem.Rows.Count;
                        if (if_one != 0 && if_one <= PageRow)
                        {
                            Log.Debug("AutoRegeditDataFRX.单列结尾注入数据");
                            dtitem2 = dtreportitem.Clone();
                            for (int i = if_one; i > 0; i--)
                            {
                                dtitem2.Rows.Add(dtreportitem.Rows[drCount - i].ItemArray);
                            }
                            dtitem2.TableName = "fritem2";
                            dsitem2.Tables.Add(dtitem2);

                            dtitem1 = dtreportitem.Clone();
                            for (int i = 0; i < (drCount - if_one); i++)
                            {
                                dtitem1.Rows.Add(dtreportitem.Rows[i].ItemArray);
                            }
                            dtitem1.TableName = "fritem1";
                            dsitem1.Tables.Add(dtitem1);
                            SubreportObject Subreport1 = (SubreportObject)report.FindObject("Subreport1");
                            if (Subreport1 != null)
                            {
                                Subreport1.Visible = true;
                            }
                            SubreportObject Subreport2 = (SubreportObject)report.FindObject("Subreport2");
                            if (Subreport2 != null)
                            {
                                Subreport2.Visible = true;
                            }

                            FastReport.PageBase p = (PageBase)report.FindObject("Page1");
                            FastReport.PageBase p2 = (PageBase)report.FindObject("Page2");
                            p.Visible = true;
                            p2.Visible = true;
                        }
                        else
                        {
                            Log.Debug("AutoRegeditDataFRX.双列结尾注入数据");
                            dtitem1 = dtreportitem.Clone();
                            for (int i = 0; i < dtreportitem.Rows.Count; i++)
                            {
                                dtitem1.Rows.Add(dtreportitem.Rows[i].ItemArray);
                            }
                            dtitem1.TableName = "fritem1";
                            dsitem1.Tables.Add(dtitem1);


                            dtitem2 = dtreportitem.Clone();
                            for (int i = 0; i < dtreportitem.Rows.Count; i++)
                            {
                                dtitem2.Rows.Add(dtreportitem.Rows[i].ItemArray);
                            }
                            dtitem2.TableName = "fritem2";
                            dsitem2.Tables.Add(dtitem2);

                            //遍历一个表多行多列
                            //foreach (DataRow mDr in dsitem2.Tables[0].Rows)
                            //{
                            //    foreach (DataColumn mDc in dsitem2.Tables[0].Columns)
                            //    {
                                    
                            //        Log.Debug(mDc.ColumnName + ": "+mDr[mDc].ToString());
                            //    }
                            //    break;
                            //}

                            //foreach (DataRow mDr in dsitem1.Tables[0].Rows)
                            //{
                            //    foreach (DataColumn mDc in dsitem1.Tables[0].Columns)
                            //    {

                            //        Log.Debug(mDc.ColumnName + ": " + mDr[mDc].ToString());
                            //    }
                            //    break;
                            //}




                            SubreportObject Subreport1 = (SubreportObject)report.FindObject("Subreport1");
                            if (Subreport1 != null)
                            {
                                Subreport1.Visible = true;
                            }
                            FastReport.PageBase p = (PageBase)report.FindObject("Page1");
                            FastReport.PageBase p2 = (PageBase)report.FindObject("Page2");
                            p.Visible = true;
                            p2.Visible = false;
                        }
                        report.RegisterData(dtreportitem.DataSet);
                        report.RegisterData(dsitem1);
                        report.RegisterData(dsitem2);
                        Log.Debug("AutoRegeditDataFRX.注入结束");
                        break;
                        #endregion
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("AutoRegeditDataFRX.异常："+ex.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            }
        }
        #endregion

        #region 向模版图片信息
        public void RegeditImageFRX(DataRow drreportform, DataTable reportgraph, ref FastReport.Report report)
        {
            ZhiFang.Common.Log.Log.Info("RegeditImageFRX.开始");
            try
            {
                #region 电子签名
                List<string> usernamefieldslist = new List<string>() { "Checker", "Incepter", "Operator", "Technician", "Collecter", "Sender2" };
                string path = "";
                List<string> pusernamelist = new List<string>();
                if (drreportform != null)
                {
                    foreach (var field in usernamefieldslist)
                    {
                        if (drreportform.Table.Columns.Contains(field) && drreportform[field] != null && drreportform[field].ToString().Trim() != "")
                        {
                            pusernamelist.Add(drreportform[field].ToString().Trim());
                        }
                    }
                }
                else
                {
                    return;
                }
                DAL.MSSQL.History.PUser puser = new DAL.MSSQL.History.PUser();
                DataSet dspuser = puser.GetListByPUserNameList(pusernamelist);
                if (dspuser != null && dspuser.Tables.Count > 0 && dspuser.Tables[0].Rows.Count > 0)
                {
                    foreach (var field in usernamefieldslist)
                    {
                        if (drreportform.Table.Columns.Contains(field) && drreportform[field] != null && drreportform[field].ToString().Trim() != "")
                        {
                            ZhiFang.Common.Log.Log.Info("RegeditImageFRX."+field + ":" + drreportform[field].ToString().Trim());
                            var tmpdr = dspuser.Tables[0].Select(" CName='" + drreportform[field].ToString().Trim() + "' ");
                            if (tmpdr.Count() > 0)
                            {
                                PictureObject p = (PictureObject)report.FindObject("NameImage" + field);
                                ZhiFang.Common.Log.Log.Info("RegeditImageFRX.NameImage图片NameImage" + field);
                                if (p != null)
                                {
                                    ////6.6库中没有存图片路径，而是图片的二进制流
                                    //if (tmpdr[0].Table.Columns.Contains("FilePath"))
                                    //{
                                    p.ImageLocation = tmpdr[0]["FilePath"].ToString();
                                    //    ZhiFang.Common.Log.Log.Info(field + ":" + drreportform[field].ToString().Trim() + ",图片路径：" + tmpdr[0]["FilePath"].ToString());
                                    //}
                                    //else
                                    //{
                                    //    byte[] by = (byte[])tmpdr[0]["userimage"];
                                    //    MemoryStream ms = new MemoryStream(by);
                                    //    //FastReport.Export.Image img = System.Net.Mime.MediaTypeNames.Image.FromStream(ms);

                                    //    //p.DataColumn = ;
                                    //    ZhiFang.Common.Log.Log.Info(field + ":" + drreportform[field].ToString().Trim() + ",图片路径：" + tmpdr[0]["userimage"].ToString());
                                    //}
                                    p.Visible = true;
                                    ZhiFang.Common.Log.Log.Info("RegeditImageFRX."+field + ":" + drreportform[field].ToString().Trim() + ",图片路径：" + tmpdr[0]["FilePath"].ToString());
                                }
                            }
                        }
                    }
                }
                #endregion
                #region 报告图片
                if (reportgraph != null)
                {
                    for (int i = 0; i < reportgraph.Rows.Count; i++)
                    {
                        PictureObject p = (PictureObject)report.FindObject("RFGraphData" + i);
                        ZhiFang.Common.Log.Log.Info("RegeditImageFRX.RFGraphData图片" + "RFGraphData" + i);
                        //if (p != null)
                        //{
                        //    byte[] tmpa = (byte[])File.ReadAllBytes(path + "\\" + files[i]);
                        //    if (tmpa != null && tmpa.Length > 0)
                        //    {
                        //        System.IO.MemoryStream aaa = new System.IO.MemoryStream(tmpa);
                        //        System.Drawing.Image ia = Image.FromStream(aaa);
                        //        p.Image = ia;
                        //    }
                        //}
                        if (p != null)
                        {
                            p.ImageLocation = reportgraph.Rows[i]["FilePath"].ToString();
                            p.Visible = true;
                            ZhiFang.Common.Log.Log.Info("RegeditImageFRX.RFGraphData图片:GraphNo:" + reportgraph.Rows[i]["GraphNo"].ToString() + ",GraphName:" + reportgraph.Rows[i]["GraphName"].ToString() + ",图片路径：" + reportgraph.Rows[i]["FilePath"].ToString());
                        }
                    }
                }
                else
                {
                    return;
                }
                #endregion
                #region
                //if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ReportIncludeImage") != null && ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() != "")
                //{
                //    if (drreportform != null && drreportform.Table.Columns.Contains("ReceiveDate") && drreportform["ReceiveDate"] != null && drreportform["ReceiveDate"].ToString().Trim() != "")
                //    {
                //        DateTime datetime = Convert.ToDateTime(drreportform["ReceiveDate"].ToString().Trim());
                //        string date = "";
                //        date = datetime.ToString("yyyy-MM-dd");
                //        string[] ArrayDate = date.Split('-');
                //        string linshiFormNo = "";
                //        try
                //        {
                //            if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LinSFormNo") != null && ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LinSFormNo").Trim() != "" && ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LinSFormNo").Trim() == "1")
                //            {//TestTypeNo   检测类型编号
                //                //SampleNo	样本号
                //                linshiFormNo = datetime.Year + "-" + ArrayDate[1] + "-" + ArrayDate[2] + ";" + drreportform["SectionNo"] + ";" +
                //                    drreportform["TestTypeNo"] + ";" + drreportform["SampleNo"];
                //            }
                //            else
                //            {
                //                linshiFormNo = drreportform["FormNo"].ToString();
                //            }
                //        }
                //        catch (Exception)
                //        {
                //            linshiFormNo = drreportform["FormNo"].ToString();
                //        }
                //        path = ConfigHelper.GetConfigString("ReportIncludeImage").Trim() + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Year + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Month + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Day + "\\" + linshiFormNo + "\\";
                //        ZhiFang.Common.Log.Log.Info(path);
                //        if (ZhiFang.ReportFormQueryPrint.Common.FilesHelper.CheckDirectory(path))
                //        {
                //            string[] files = Directory.GetFiles(path);
                //            for (int i = 0; i < files.Count(); i++)
                //            {
                //                string[] a = files[i].Split('@');
                //                if (a.Count() > 1)
                //                {
                //                    if (a[3] == "NameImage")
                //                    {
                //                        //= (PictureObject)report.FindObject("PV" + (i + 1).ToString());
                //                        PictureObject p = (PictureObject)report.FindObject(a[3] + a[5]);
                //                        ZhiFang.Common.Log.Log.Info("NameImage图片" + a[3] + a[5]);
                //                        //if (p != null)
                //                        //{
                //                        //    byte[] tmpa = (byte[])File.ReadAllBytes(path+"\\"+files[i]);
                //                        //    if (tmpa != null && tmpa.Length > 0)
                //                        //    {
                //                        //        System.IO.MemoryStream aaa = new System.IO.MemoryStream(tmpa);
                //                        //        System.Drawing.Image ia = Image.FromStream(aaa);
                //                        //        p.Image = ia;
                //                        //    }
                //                        //}
                //                        if (p != null)
                //                        {
                //                            p.ImageLocation = files[i];
                //                            p.Visible = true;
                //                            ZhiFang.Common.Log.Log.Info("NameImage图片名称" + files[i]);
                //                        }
                //                    }
                //                    if (a[3] == "RFGraphData")
                //                    {
                //                        PictureObject p = (PictureObject)report.FindObject(a[3] + (int.Parse(a[2]) + 1).ToString());
                //                        ZhiFang.Common.Log.Log.Info("RFGraphData图片" + a[3] + (int.Parse(a[2]) + 1).ToString());
                //                        //if (p != null)
                //                        //{
                //                        //    byte[] tmpa = (byte[])File.ReadAllBytes(path + "\\" + files[i]);
                //                        //    if (tmpa != null && tmpa.Length > 0)
                //                        //    {
                //                        //        System.IO.MemoryStream aaa = new System.IO.MemoryStream(tmpa);
                //                        //        System.Drawing.Image ia = Image.FromStream(aaa);
                //                        //        p.Image = ia;
                //                        //    }
                //                        //}
                //                        if (p != null)
                //                        {
                //                            p.ImageLocation = files[i];
                //                            p.Visible = true;
                //                            ZhiFang.Common.Log.Log.Info("RFGraphData图片名称" + files[i]);
                //                        }
                //                    }
                //                    if (a[3] == "S_RequestVItem")
                //                    {
                //                        PictureObject p = (PictureObject)report.FindObject("PV" + (int.Parse(a[2]) + 1).ToString());
                //                        ZhiFang.Common.Log.Log.Info("S_RequestVItem图片PV" + (int.Parse(a[2]) + 1).ToString());
                //                        //if (p != null)
                //                        //{
                //                        //    byte[] tmpa = (byte[])File.ReadAllBytes(path + "\\" + files[i]);
                //                        //    if (tmpa != null && tmpa.Length > 0)
                //                        //    {
                //                        //        System.IO.MemoryStream aaa = new System.IO.MemoryStream(tmpa);
                //                        //        System.Drawing.Image ia = Image.FromStream(aaa);
                //                        //        p.Image = ia;
                //                        //    }
                //                        //}
                //                        if (p != null)
                //                        {
                //                            p.ImageLocation = files[i];
                //                            p.Visible = true;
                //                            ZhiFang.Common.Log.Log.Info("S_RequestVItem图片名称" + files[i]);
                //                        }
                //                    }
                //                }
                //            }
                //        }
                //    }
                //}
                #endregion
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug("RegeditImageFRX.异常" + e.ToString() + "---------" + e.StackTrace.ToString());
            }
        }
        #endregion

        public string PageTypeCheck(float height, float width)
        {
            string pagetype = "";
            if ((Convert.ToInt32(height) >= 205 || Convert.ToInt32(height) <= 215) && (Convert.ToInt32(width) >= 292|| Convert.ToInt32(width) <= 312))
                pagetype = "A4";
            if (Convert.ToInt32(height) == 297 && Convert.ToInt32(width) == 210)
                pagetype = "A4";
            if (Convert.ToInt32(height) == 210 && Convert.ToInt32(width) == 148)
                pagetype = "A5";
            if (Convert.ToInt32(height) == 148 && Convert.ToInt32(width) == 210)
                pagetype = "A5";
            return pagetype;
        }

        public bool ExistsReportForm_PDF(DataTable brf, ref ReportFormFilesVO reportformfilesvo)
        {
            if (brf != null && brf.Rows.Count > 0)
            {
                string reportformfileallpath = GetReportFormRelativePath(brf.Rows[0]) + GetReportFormFileName_PDF(brf.Rows[0]);
                if (File.Exists(reportformfileallpath))
                {
                    reportformfilesvo.PageCount = brf.Rows[0]["PageCount"].ToString();
                    reportformfilesvo.PageName = brf.Rows[0]["PageName"].ToString();
                    reportformfilesvo.PDFPath = reportformfileallpath;
                    reportformfilesvo.ReportFormID = brf.Rows[0]["ReportFormID"].ToString();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public string GetReportFormFullPath(DataTable dtrf)
        {
            if (dtrf != null && dtrf.Rows.Count > 0)
                return GetReportFormFullPath(dtrf.Rows[0]);
            return "";
        }
        public string GetReportFormFullPath(DataRow drrf)
        {
            string ReportFormPath, reportformfilesname;
            ReportFormPath = GetReportFormPath(drrf);
            reportformfilesname = GetReportFormFileName_PDF(drrf);
            string reportformfullpath = ReportFormPath + reportformfilesname;
            return reportformfullpath;
        }

        public string GetReportFormPath(DataRow drrf)
        {
            string basepath, ReportFormPath;
            basepath = System.AppDomain.CurrentDomain.BaseDirectory;
            ReportFormPath = GetReportFormRelativePath(drrf);
            ReportFormPath = basepath + ReportFormPath;
            return ReportFormPath;
        }

        /**public string GetReportFormRelativePath(DataRow drrf)
        {
            string ReportFormRelativePath = "";
            DateTime receivedate = DateTime.Parse(drrf["Receivedate"].ToString().Trim());
            ReportFormRelativePath = SysContractPara.ReportFormFilePath + receivedate.ToString("yyyy-MM-dd") + @"\";
            return ReportFormRelativePath;
        }

        public string GetReportFormFileName_PDF(DataRow drrf)
        {
            string reportformfilesname = drrf["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + ".PDF";
            return reportformfilesname;
        }*/

        public string GetReportFormRelativePath(DataRow drrf)
        {
            string ReportFormRelativePath = "";
            DateTime receivedate = DateTime.Parse(drrf["Receivedate"].ToString().Trim());
            ReportFormRelativePath = SysContractPara.ReportFormFilePath + receivedate.ToString("yyyy-MM-dd") + @"\";
            return ReportFormRelativePath;
        }

        public string GetReportFormFileName_PDF(DataRow drrf)
        {
            string guid = Guid.NewGuid().ToString();

            
            string reportformfilesname = guid + ".PDF";
            return reportformfilesname;
        }

        //保密等级
        public void secrecyGrade(int pow, ref DataTable tb)
        {
            DataTable dtri = tb;
            if (dtri.Columns.Contains("Secretgrade"))
            {
                foreach (DataRow item in dtri.Rows)
                {
                    int secretGrade = 0;
                    if (item["Secretgrade"] !=null)
                    {
                       secretGrade = int.Parse(Convert.ToString(item["Secretgrade"]));
                    }
                    int powr = int.Parse(Math.Pow(2, pow).ToString());
                    switch (pow)
                    {
                        case 2:
                            if (((secretGrade & powr) == powr) && secretGrade > 0)
                            {
                                item.Delete();
                            }
                            powr = int.Parse(Math.Pow(2, pow + 1).ToString());
                            if (((secretGrade & powr) == powr) && secretGrade > 0)
                            {
                                item.Delete();
                            }
                            break;
                        case 3:
                            if (((secretGrade & powr) == powr) && secretGrade > 0)
                            {
                                item.Delete();
                            }
                            powr = int.Parse(Math.Pow(2, pow - 1).ToString());
                            if (((secretGrade & powr) == powr) && secretGrade > 0)
                            {
                                item.Delete();
                            }
                            break;
                        default:
                            if (((secretGrade & powr) == powr) && secretGrade > 0)
                            {
                                item.Delete();
                            }
                            break;
                    }

                }
                dtri.AcceptChanges();
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("CreatReportFormFiles:Secretgrade字段不存在,保密等级不进行处理");
            }
        }
    }
    public class HistoryReportFormPDFExport : FastReport.Export.Pdf.PDFExport
    {
        public int PageCount
        {
            get { return base.Pages.Count(); }
        }
    }
}
