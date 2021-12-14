using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using ZhiFang.Common.Dictionary;
using FastReport;
using ZhiFang.Tools;
using ZhiFang.Common.Public;
using System.IO;
using FastReport.Export.Html;
using System.Runtime.InteropServices;
using System.Configuration;
using ZhiFang.BLL.Common;
//using ICSharpCode.SharpZipLib.Zip;

namespace ZhiFang.BLL.Report.Print
{
    public class PrintReportFormCommon
    {
        #region CreatHtml报告内容
        /// <summary>
        /// 生成HTML内容(XSLT模版)
        /// </summary>
        /// <param name="reportform">报告单(dt)</param>
        /// <param name="reportitem">报告项目(dt)</param>
        /// <param name="modulepath">模版路径</param>
        /// <returns></returns>
        public static string CreatHtmlContextXslt(DataTable reportform, DataTable reportitem, string modulepath)
        {
            if (reportform.Rows.Count > 0)
            {
                //reportform = SetUserImage(reportform);
                string tmphtml = TransXMLToHtml.TransformXMLIntoHtml(ZhiFang.Common.Public.TransDataToXML.MergeXML(ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(reportform, "WebReportFile", "ReportForm"), ZhiFang.Common.Public.TransDataToXML.TransformDTIntoXML(reportitem, "WebReportFile", "ReportItem"), "WebReportFile"), modulepath);
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
        public static string CreatHtmlContextFRX(DataTable reportform, DataTable reportitem, string modulepath)
        {
            DataRow drreportform = reportform.Rows[0];
            ReportFormTitle rft = ReportFormTitle.center;

            string htmlcontext = "";
            List<string> reportpath = new List<string>();
            FastReport.Report report = new FastReport.Report();
            report.Load(modulepath);
            PrintReportFormCommon.RegeditDataFRX(drreportform, reportitem, ref report, modulepath, rft);
            PrintReportFormCommon.RegeditImageFRX(drreportform, ref report);
            report.RegisterData(reportform.DataSet);
            report.Prepare();
            string reportformfiletype = "JPG";
            //if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").Trim() != "")
            //{
            //    reportformfiletype = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").Trim().ToUpper();
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
                    string Mhtinnerhtml = ZhiFang.Tools.FilesHelper.ReadContext(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".mht");
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
                    string htmleinnerhtml = ZhiFang.Tools.FilesHelper.ReadContext(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".html");
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
                    ZhiFang.Tools.FilesHelper.WriteContext(htmleinnerhtml, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".html");
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
        /// 生成HTML内容(FR3模版)
        /// </summary>
        /// <param name="reportform">报告单(dt)</param>
        /// <param name="reportitem">报告项目(dt)</param>
        /// <param name="modulepath">模版路径</param>
        /// <returns></returns>
        public static string CreatHtmlContextFR3(DataTable reportform, DataTable reportitem, string modulepath)
        {
            return "";
        }
        #endregion

        #region Creat报告文件
        /// <summary>
        /// 生成报告文件(XSLT模版)
        /// </summary>
        /// <param name="reportform">报告单(dr)</param>
        /// <param name="reportitem">报告项目(dt)</param>
        /// <param name="modulepath">模版路径</param>
        /// <param name="rft">抬头类型</param>
        /// <returns></returns>
        public static List<string> CreatReportFormFilesXslt(DataRow reportform, DataTable reportitem, string modulepath, ReportFormTitle rft)
        {
            return null;
        }
        /// <summary>
        /// 生成报告文件(FRX模版)
        /// </summary>
        /// <param name="reportform">报告单(dr)</param>
        /// <param name="reportitem">报告项目(dt)</param>
        /// <param name="modulepath">模版路径</param>
        /// <param name="rft">抬头类型</param>
        /// <returns></returns>
        public static List<string> CreatReportFormFilesFRX(DataRow reportform, DataTable reportitem, string modulepath, ReportFormTitle rft)
        {
            List<string> reportpath = new List<string>();
            FastReport.Report report = new FastReport.Report();
            report.Load(modulepath);
            PrintReportFormCommon.RegeditDataFRX(reportform, reportitem, ref report, modulepath, rft);
            PrintReportFormCommon.RegeditImageFRX(reportform, ref report);
            report.RegisterData(reportform.Table.DataSet);
            report.Prepare();
            string reportformfiletype = "PDF";
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").Trim() != "")
            {
                reportformfiletype = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").Trim().ToUpper();
            }
            DateTime receivedate = DateTime.Parse(reportform["Receivedate"].ToString().Trim());
            string savepath = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + receivedate.Year + "\\" + receivedate.Month + "\\" + receivedate.Day + "\\" + rft.ToString() + "\\";
            if (ZhiFang.Tools.FilesHelper.CheckAndCreatDir(savepath))
            {
                switch (reportformfiletype)
                {
                    case "MHT":
                        #region MHT
                        FastReport.Export.Mht.MHTExport mhte = new FastReport.Export.Mht.MHTExport();
                        mhte.ImageFormat = FastReport.Export.Html.ImageFormat.Jpeg;
                        mhte.CurPage = 10;
                        //report.c
                        report.Export(mhte, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".mht");
                        string Mhtinnerhtml = ZhiFang.Tools.FilesHelper.ReadContext(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".mht");
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
                        ZhiFang.Tools.FilesHelper.WriteContext(Mhtinnerhtml, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".mht");

                        reportpath.Add(ConfigHelper.GetConfigString("LocalhostURL") + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".mht");
                        #endregion
                        break;
                    case "HTML":
                        #region HTML
                        FastReport.Export.Html.HTMLExport htmle = new HTMLExport();
                        htmle.SinglePage = true;
                        htmle.Navigator = false;
                        htmle.SubFolder = false;
                        report.Export(htmle, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".html");
                        string htmleinnerhtml = ZhiFang.Tools.FilesHelper.ReadContext(System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".html");
                        int stylei = 0;
                        while (htmleinnerhtml.IndexOf(".s" + stylei + " {") >= 0)
                        {
                            htmleinnerhtml = htmleinnerhtml.Replace(".s" + stylei + " {", ".s" + stylei + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "") + " {");
                            htmleinnerhtml = htmleinnerhtml.Replace("class=\"s" + stylei + "\"", "class=\"s" + stylei + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "") + "\"");
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
                        ZhiFang.Tools.FilesHelper.WriteContext(htmleinnerhtml, System.AppDomain.CurrentDomain.BaseDirectory + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".html");
                        reportpath.Add(htmleinnerhtml);
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
                        if (tmpjpg.GeneratedFiles.Count > 0)
                        {
                            foreach (var filefullpath in tmpjpg.GeneratedFiles)
                            {
                                reportpath.Add(filefullpath);

                                //htmlcontext += "<img src='../" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "/" + drreportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".JPG' border='0'/>";
                            }
                        }


                        #endregion
                        break;
                    case "PDF":
                        #region PDF

                        FastReport.Export.Pdf.PDFExport tmppdf = new FastReport.Export.Pdf.PDFExport();
                        report.Export(tmppdf, savepath + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "").Replace(",", "&") + "@" + rft.ToString() + ".PDF");

                        if (tmppdf.GeneratedFiles.Count > 0)
                        {
                            foreach (var filefullpath in tmppdf.GeneratedFiles)
                            {
                                reportpath.Add(filefullpath);
                            }
                        }

                        #endregion
                        break;
                    default: break;
                }
            }
            report.Dispose();
            return reportpath;
        }
        /// <summary>
        /// 生成报告文件(FR3模版)
        /// </summary>
        /// <param name="reportform">报告单(dr)</param>
        /// <param name="reportitem">报告项目(dt)</param>
        /// <param name="modulepath">模版路径</param>
        /// <param name="rft">抬头类型</param>
        /// <returns></returns>
        public static List<string> CreatReportFormFilesFR3(DataRow reportform, DataTable reportitem, string modulepath, ReportFormTitle rft)
        {
            string SaveType = "PDF";
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").Trim() != "")
            {
                SaveType = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").Trim().ToUpper();
            }
            string _clientname = "";
            PrintReportFormCommon.RegeditDataFR3(reportform, reportitem, ref _clientname, rft);
            string modelType = "CHAM";
            bool result = false;
            string sectiontype = "0";
            if (reportform["SECTIONTYPE"] != null && reportform["SECTIONTYPE"].ToString().Trim() != "")
            {
                sectiontype = reportform["SECTIONTYPE"].ToString();
            }
            switch ((SectionType)Convert.ToInt32(sectiontype))
            {
                case SectionType.all:
                    #region Normal
                    modelType = "CHAM";
                    break;
                    #endregion
                case SectionType.Normal:
                    #region Normal
                    modelType = "CHAM";
                    break;
                    #endregion
                case SectionType.Micro:
                    #region Micro
                    modelType = "MICROBE";
                    break;
                    #endregion
                case SectionType.NormalIncImage:
                    #region NormalIncImage
                    modelType = "CHAM";
                    break;
                    #endregion
                case SectionType.MicroIncImage:
                    #region MicroIncImage
                    modelType = "MICROBE";
                    break;
                    #endregion
                case SectionType.CellMorphology:
                    #region CellMorphology
                    modelType = "MARROW";
                    break;
                    #endregion
                case SectionType.FishCheck:
                    #region FishCheck
                    modelType = "MARROW";
                    break;
                    #endregion
                case SectionType.SensorCheck:
                    #region SensorCheck
                    modelType = "CHAM";
                    break;
                    #endregion
                case SectionType.ChromosomeCheck:
                    #region ChromosomeCheck
                    modelType = "MARROW";
                    break;
                    #endregion
                case SectionType.PathologyCheck:
                    #region PathologyCheck
                    modelType = "MARROW";
                    break;
                    #endregion
            }
            DateTime receivedate = DateTime.Parse(reportform["Receivedate"].ToString().Trim());
            string savepath = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + receivedate.Year + "\\" + receivedate.Month + "\\" + receivedate.Day + "\\" + rft.ToString() + "\\" + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "000000").Replace(":", "") + "\\" + Guid.NewGuid().ToString() + "\\";
            Tools.FilesHelper.CheckAndCreatDir(savepath);
            IntPtr ConnectionString = Marshal.StringToHGlobalAnsi(ConfigurationManager.ConnectionStrings["WebLisDB"].ToString());
            IntPtr modelNameDeliph = Marshal.StringToHGlobalAnsi(modulepath);
            IntPtr modelTypeDeliph = Marshal.StringToHGlobalAnsi(modelType);
            //已此来判断版本(weblis;) 
            IntPtr PrintID = Marshal.StringToHGlobalAnsi(" weblis;" + reportform["ReportFormID"].ToString() + " ");
            IntPtr saveType = Marshal.StringToHGlobalAnsi(SaveType);
            IntPtr SavePath = Marshal.StringToHGlobalAnsi(savepath + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "000000").Replace(":", ""));
            IntPtr Path = Marshal.StringToHGlobalAnsi(GetImagePath(reportform));
            IntPtr LogPath = Marshal.StringToHGlobalAnsi(System.AppDomain.CurrentDomain.BaseDirectory + "\\Log\\");
            IntPtr where = Marshal.StringToHGlobalAnsi(" 1=1 ");
            IntPtr clientname = Marshal.StringToHGlobalAnsi(_clientname);
            ZhiFang.Common.Log.Log.Info("开始调用ModelPrint.dll");
            result = PrintReport(ConnectionString, modelNameDeliph, modelTypeDeliph, PrintID, saveType, SavePath, Path, LogPath, where, clientname);
            if (result)
            {
                string aaa = "";
                ZhiFang.Common.Log.Log.Info("生成报告成功");
                List<string> filepathlist = new List<string>();
                if (SaveType == "JPG")
                {
                    string[] reportfilearray = Directory.GetFiles(savepath + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "000000").Replace(":", "") + "\\", "*.jpg");
                    if (reportfilearray.Length > 0)
                    {
                        for (int i = 0; i < reportfilearray.Length; i++)
                        {
                            filepathlist.Add(reportfilearray[i]);
                        }
                    }

                }
                if (SaveType == "PDF")
                {
                    filepathlist.Add(savepath + reportform["ReportFormID"].ToString().Replace(" 00:00:00", "000000").Replace(":", "") + "." + SaveType + "");
                }
                return filepathlist;
            }
            return null;
        }
        /// <summary>
        /// 找到预先生成的报告图片
        /// </summary>
        /// <param name="reportform"></param>
        /// <param name="rft"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static List<string> FindReportFormFiles(DataRow reportform, ReportFormTitle rft, string title)
        {
            try
            {
                string SaveType = "PDF";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").Trim() != "")
                {
                    SaveType = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").Trim().ToUpper();
                }

                ZhiFang.Common.Log.Log.Info("FindReportFormFiles");
                string reportSavePath = System.AppDomain.CurrentDomain.BaseDirectory +ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" +
                 DateTime.Parse(reportform["UploadDate"].ToString()).Year + "\\" + DateTime.Parse(reportform["UploadDate"].ToString()).Month + "\\" + DateTime.Parse(reportform["UploadDate"].ToString()).Day + "\\";

                //是否在ReportFormFilesDir底下添加report文件夹
                if (ConfigHelper.GetConfigString("isUpLoadReport") != null && ConfigHelper.GetConfigString("isUpLoadReport").ToString() == "1") 
                {
                    reportSavePath = System.AppDomain.CurrentDomain.BaseDirectory + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\report\\" +
                   DateTime.Parse(reportform["UploadDate"].ToString()).Year + "\\" + DateTime.Parse(reportform["UploadDate"].ToString()).Month + "\\" + DateTime.Parse(reportform["UploadDate"].ToString()).Day + "\\";
                }
                //生成统一的文件名称
                string fileNameUnique = "";
                if (reportform["REPORTFORMID"] != null)
                {
                    fileNameUnique = reportform["REPORTFORMID"].ToString();
                }
                if (fileNameUnique == "")
                    fileNameUnique = System.Guid.NewGuid().ToString();
                fileNameUnique = fileNameUnique.Replace(":", "：");

                ZhiFang.Common.Log.Log.Info("报告单名称:" + fileNameUnique +" 抬头："+title);
                //fileNameUnique = fileNameUnique.Replace(":", "");
                List<string> filepathlist = new List<string>();
                if (SaveType == "JPG")
                {
                    string[] reportfilearray = Directory.GetFiles(reportSavePath + fileNameUnique + "\\", "*.jpg");
                    if (reportfilearray.Length > 0)
                    {
                        for (int i = 0; i < reportfilearray.Length; i++)
                        {
                            filepathlist.Add(reportfilearray[i]);
                        }
                    }
                }


                if (SaveType == "PDF")
                {
                    ZhiFang.Common.Log.Log.Info("预生成的PDF文件路径：" + reportSavePath);
                    //string filePath = "";
                    string[] reportfilearray;
                    //新的weblis上传服务
                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsNewWeblisService").Trim() == "1")
                    {
                        ZhiFang.Common.Log.Log.Info("新服务上传预生成的PDF文件路径：" + reportSavePath + rft.ToString());
                        reportfilearray = Directory.GetFiles(reportSavePath + rft.ToString() + "\\", "*.pdf");

                        string t1 = reportfilearray == null ? "null" : string.Join(";", reportfilearray);
                        ZhiFang.Common.Log.Log.Info("报告路径(新服务):" + t1);
                        if (reportfilearray.Length <= 0)
                            return filepathlist;

                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("旧服务上传预生成的PDF文件路径：" + reportSavePath);
                        reportfilearray = Directory.GetFiles(reportSavePath, "*.pdf");

                        string t1 = reportfilearray == null ? "null" : string.Join(";", reportfilearray);
                        //ZhiFang.Common.Log.Log.Info("报告路径(旧服务):" + t1);
                        if (reportfilearray.Length <= 0)
                            return filepathlist;

                    }


                    for (int i = 0; i < reportfilearray.Length; i++)
                    {
                        filepathlist = new List<string>();
                        //ZhiFang.Common.Log.Log.Info("预先生成的报告文件:" + reportfilearray[i]);
                        //ZhiFang.Common.Log.Log.Info("抬头:" + title);
                        ////0：中心  1：医院（医院报告名称带T或者T_）
                        if (title == "2" && reportfilearray[i].Contains(fileNameUnique) && reportfilearray[i].Contains("T_"))
                        {
                            filepathlist.Add(reportfilearray[i]);
                            break;
                        }

                        else if (title == "0" && reportfilearray[i].Contains(fileNameUnique) && (reportfilearray[i].Contains("T_" + fileNameUnique)==false && reportfilearray[i].Contains("T" + fileNameUnique)==false))//中心
                        {
                            filepathlist.Add(reportfilearray[i]);
                            ZhiFang.Common.Log.Log.Info("报告抬头为中心的报告路径：" + reportfilearray[i]);
                            break;
                        }
                        else if (title == "1" && reportfilearray[i].Contains(fileNameUnique) && (reportfilearray[i].Contains("T_" + fileNameUnique) || reportfilearray[i].Contains("T" + fileNameUnique)))//医院
                            //else if (title == "1" && reportfilearray[i].Contains(fileNameUnique))//医院
                        {
                            filepathlist.Add(reportfilearray[i]);
                            ZhiFang.Common.Log.Log.Info("报告抬头为医院的报告路径：" + reportfilearray[i]);
                            break;
                        }
                        else
                        {
                            if (title != "1" && title != "0" && reportfilearray[i].Contains(fileNameUnique) && reportfilearray[i].Contains("_QT"))//其他类型
                            {
                                filepathlist.Add(reportfilearray[i]);
                                break;
                            }
                        }
                    }

                }

                string applocalroot = System.AppDomain.CurrentDomain.BaseDirectory;//获取程序根目录           
                List<string> tmphtml = new List<string>();
                for (int i = 0; i < filepathlist.Count; i++)
                {
                    tmphtml.Add(filepathlist[i].Replace(applocalroot, "").Replace(@"\", @"/"));
                }
                ZhiFang.Common.Log.Log.Info("返回报告路径:" + string.Join(",", tmphtml.ToArray()));
                return tmphtml;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("获取报告出错:" + ex.Message);
            }
            return null;
        }
        [DllImport("ModelPrint.dll", EntryPoint = "PrintReport")]
        public static extern Boolean PrintReport(IntPtr ConnectionString, IntPtr ModelName, IntPtr ModelType, IntPtr PrintID, IntPtr SaveType, IntPtr SavePath, IntPtr PicturePath, IntPtr LogPath, IntPtr SubWhere, IntPtr clientName);
        #endregion

        #region 查找模版
        /// <summary>
        /// 查找模版
        /// </summary>
        /// <param name="drreportform">报告单(dr)</param>
        /// <param name="dtreportitem">报告项目(dt)</param>
        /// <param name="rft">抬头类型</param>
        /// <returns></returns>
        public static string FindModel(DataRow drreportform, DataTable dtreportitem, ReportFormTitle rft)
        {
            return FindModel(drreportform, dtreportitem, null, rft);
        }
        /// <summary>
        /// 查找模版
        /// </summary>
        /// <param name="drreportform">报告单(dr)</param>
        /// <param name="dtreportitem">报告项目(dt)</param>
        /// <param name="dtreportitem1">报告项目(dt)</param>
        /// <param name="rft">抬头类型</param>
        /// <returns></returns>
        public static string FindModel(DataRow drreportform, DataTable dtreportitem, DataTable dtreportitem1, ReportFormTitle rft)
        {
            try
            {
                ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint PGroupPrint = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPGroupPrint>.GetBLL();
                ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat pfb = ZhiFang.BLLFactory.BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBPrintFormat>.GetBLL();
                DataTable dtpgroupprint;
                string formatPrint = "";
                Model.PGroupPrint t = new Model.PGroupPrint();
                try
                {
                    t.SectionNo = int.Parse(drreportform["SectionNo"].ToString());
                }
                catch
                {
                    t.SectionNo = -1;
                }
                t.UseFlag = 1;
                if (rft == ReportFormTitle.BatchPrint)
                {
                    t.BatchPrint = 1;
                }
                if (rft == ReportFormTitle.center)
                {
                    t.ModelTitleType = 0;
                }
                if (rft == ReportFormTitle.client)
                {
                    t.ModelTitleType = 1;
                }
                if (Convert.ToInt32(rft) > 2)
                {
                    t.SickTypeNo = Convert.ToInt32(rft) - 2;
                }
                dtpgroupprint = PGroupPrint.GetList(t).Tables[0];
                if (dtpgroupprint.Rows.Count <= 0)
                {
                    return null;
                }
                else
                {
                    bool fitem = false;
                    bool fclient = false;
                    bool fitemcount = false;
                    string str = "";
                    int itemcount = 0;
                    string where = "";
                    ZhiFang.Common.Log.Log.Info("报告模板查询_特殊项目配置个数：" + dtpgroupprint.Select(" SpecialtyItemNo is null ").Count().ToString());
                    if (dtpgroupprint.Select(" SpecialtyItemNo is null ").Count() != dtpgroupprint.Rows.Count)
                    {

                        if (dtreportitem != null && dtreportitem.Rows.Count > 0)
                        {
                            for (int num = 0; num < dtreportitem.Rows.Count; num++)
                            {
                                str = str + " SpecialtyItemNo = " + dtreportitem.Rows[num]["itemno"].ToString() + " or ";
                            }
                        }
                        if (dtreportitem1 != null && dtreportitem1.Rows.Count > 0)
                        {
                            for (int num = 0; num < dtreportitem1.Rows.Count; num++)
                            {
                                str = str + " SpecialtyItemNo = " + dtreportitem1.Rows[num]["itemno"].ToString() + " or ";
                            }
                        }
                        str = str.Substring(0, str.LastIndexOf(" or "));
                        ZhiFang.Common.Log.Log.Info("报告模板查询_匹配特殊项目：" + str);
                        if (dtpgroupprint.Select(" " + str).Count() > 0)
                        {
                            fitem = true;
                        }
                    }
                    if (dtpgroupprint.Select(" clientno is null ").Count() != dtpgroupprint.Rows.Count && drreportform["clientno"] != DBNull.Value)
                    {
                        ZhiFang.Common.Log.Log.Info("报告模板查询_匹配客户@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@：" + dtpgroupprint.Select(" clientno is null ").Count() + "####################" + drreportform["clientno"]);
                        if (dtpgroupprint.Select(" clientno=" + drreportform["clientno"].ToString()).Count() > 0)
                        {
                            ZhiFang.Common.Log.Log.Info("报告模板查询_匹配客户@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@：" + dtpgroupprint.Select(" clientno is null ").Count() + " @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@：" + drreportform["clientno"] + "@@@@@@@@@@@" + dtpgroupprint.Select(" clientno=" + drreportform["clientno"].ToString()));
                            fclient = true;
                        }
                    }
                    ZhiFang.Common.Log.Log.Info("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@：" + dtpgroupprint.Select(" clientno is null ").Count() + "####################" + drreportform["clientno"]);
                    if (dtpgroupprint.Select(" ItemMinNumber is null and ItemMaxNumber is null ").Count() != dtpgroupprint.Rows.Count)
                    {
                        if (dtreportitem != null)
                        {
                            itemcount += dtreportitem.Rows.Count;
                        }
                        if (dtreportitem1 != null)
                        {
                            itemcount += dtreportitem1.Rows.Count;
                        }
                        if (dtpgroupprint.Select(" ItemMinNumber<=" + itemcount + " and ItemMaxNumber>=" + itemcount).Count() > 0)
                        {
                            fitemcount = true;
                        }
                    }

                    if (fitem == false && fclient && fitemcount)
                    {
                        where = " clientno=" + drreportform["clientno"].ToString() + " and ( ItemMinNumber<=" + itemcount + " and ItemMaxNumber>=" + itemcount + " ) ";
                    }
                    if (fitem == false && fclient == false && fitemcount)
                    {
                        where = " ( ItemMinNumber<=" + itemcount + " and ItemMaxNumber>=" + itemcount + " ) ";
                    }
                    if (fitem == false && fclient == false && fitemcount == false)
                    {
                        where = " 1=1 ";
                    }
                    if (fitem && fclient == false && fitemcount)
                    {
                        where = " " + str + " and ( ItemMinNumber<=" + itemcount + " and ItemMaxNumber>=" + itemcount + " ) ";
                    }
                    if (fitem && fclient == false && fitemcount == false)
                    {
                        where = " " + str;
                    }
                    if (fitem && fclient && fitemcount == false)
                    {
                        where = " " + str + " and clientno=" + drreportform["clientno"].ToString();
                    }
                    if (fitem && fclient && fitemcount)
                    {
                        where = " " + str + " and clientno=" + drreportform["clientno"].ToString() + " and  ( ItemMinNumber<=" + itemcount + " and ItemMaxNumber>=" + itemcount + " ) ";
                    }

                    ZhiFang.Common.Log.Log.Info("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@：" + where);
                    DataRow[] dra = dtpgroupprint.Select(where, "Sort asc,Id desc");
                    ZhiFang.Common.Log.Log.Info("报告模板查询Sub：" + where + ",排序：Sort asc,Id desc");
                    if (dra.Count() > 0)
                    {
                        formatPrint = dra[0]["PrintFormatNo"].ToString();
                        ZhiFang.Common.Log.Log.Info("报告模板ID：" + formatPrint);
                    }
                    else
                    {

                    }
                }
                try
                {
                    ZhiFang.Common.Log.Log.Info("aaa");
                    if (formatPrint != "")
                    {
                        ZhiFang.Common.Log.Log.Info("bbb");
                        Model.PrintFormat pf_m = pfb.GetModel(formatPrint);
                        ZhiFang.Common.Log.Log.Info("ccc");
                        string showmodel = pf_m.PintFormatAddress.ToString().Trim() + "\\" + pf_m.Id + "\\" + pf_m.Id + "." + pf_m.PintFormatFileName.Split('.')[1];
                        ZhiFang.Common.Log.Log.Info("ddd");
                        ZhiFang.Common.Log.Log.Info("模板名称:" + showmodel);
                        formatPrint = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("XSLModelURL") + "\\" + showmodel);
                        formatPrint = formatPrint.Replace("//", "");
                        ZhiFang.Common.Log.Log.Info("模板路径:" + formatPrint);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("模板不存在");
                    }
                }
                catch (Exception e)
                {
                    ZhiFang.Common.Log.Log.Debug(e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                    return "";
                }
                return formatPrint;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return "";
            }
        }
        #endregion

        #region 向模版注入数据
        public static void RegeditDataFRX(DataRow drreportform, DataTable dtreportitem, ref FastReport.Report report, string formatPrint, ZhiFang.Common.Dictionary.ReportFormTitle rft)
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
                        DataSet set3;
                        DataTable table2;
                        DataSet set4;
                        DataTable table3;
                        set3 = new DataSet();
                        table2 = new DataTable("fritem1");
                        set4 = new DataSet();
                        table3 = new DataTable("fritem2");
                        dtreportitem.Columns.Add("RowId", typeof(string));
                        int num2 = 0;
                        while (num2 < dtreportitem.Rows.Count)
                        {
                            dtreportitem.Rows[num2]["RowId"] = (num2 + 1).ToString();
                            num2++;
                        }

                        if (formatPrint.IndexOf("自动单双") >= 0)
                        {
                            SubreportObject obj3;
                            SubreportObject obj4;
                            SubreportObject obj5;
                            LineObject obj6;
                            TextObject obj2 = (TextObject)report.FindObject("Row");
                            if (((obj2 != null) && Validate.IsInt(obj2.Text.Trim())) && (Convert.ToInt32(obj2.Text.Trim()) < dtreportitem.Rows.Count))
                            {
                                int PageSize = int.Parse(obj2.Text.Trim());
                                int PageCount = (dtreportitem.Rows.Count / PageSize) + 1;
                                if (PageCount > 0)
                                {
                                    table2 = dtreportitem.Clone();
                                    table3 = dtreportitem.Clone();
                                    for (int Pageindex = 0; Pageindex < PageCount; Pageindex++)
                                    {
                                        for (int i = Pageindex * PageSize; i < ((Pageindex + 1) * PageSize); i++)
                                        {
                                            if (i >= dtreportitem.Rows.Count)
                                            {
                                                break;
                                            }
                                            if ((Pageindex % 2) == 0)
                                            {
                                                table2.Rows.Add(dtreportitem.Rows[i].ItemArray);
                                            }
                                            else
                                            {
                                                table3.Rows.Add(dtreportitem.Rows[i].ItemArray);
                                            }
                                        }
                                    }
                                }
                                table2.TableName = "fritem1";
                                set3.Tables.Add(table2);
                                table3.TableName = "fritem2";
                                set4.Tables.Add(table3);
                                obj3 = (SubreportObject)report.FindObject("Subreport1");
                                obj3.Visible = false;
                                obj4 = (SubreportObject)report.FindObject("Subreport2");
                                obj4.Visible = true;
                                obj5 = (SubreportObject)report.FindObject("Subreport3");
                                obj5.Visible = true;
                                obj6 = (LineObject)report.FindObject("Line7");
                                if (obj6 != null)
                                {
                                    obj6.Visible = true;
                                }
                            }
                            else
                            {
                                table2 = dtreportitem.Copy();
                                table3 = dtreportitem.Copy();
                                table2.TableName = "fritem1";
                                set3.Tables.Add(table2);
                                table3.TableName = "fritem2";
                                set4.Tables.Add(table3);
                                obj3 = (SubreportObject)report.FindObject("Subreport1");
                                obj3.Visible = true;
                                obj4 = (SubreportObject)report.FindObject("Subreport2");
                                obj4.Visible = false;
                                obj5 = (SubreportObject)report.FindObject("Subreport3");
                                obj5.Visible = false;
                                obj6 = (LineObject)report.FindObject("Line7");
                                if (obj6 != null)
                                {
                                    obj6.Visible = false;
                                }
                            }
                            report.RegisterData(dtreportitem.DataSet);
                            report.RegisterData(set3);
                            report.RegisterData(set4);
                        }

                        if (formatPrint.IndexOf("唐氏筛查") >= 0)
                        {
                            for (int i = 0; i < dtreportitem.Rows.Count; i++)
                            {
                                #region 出生日期
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100771")
                                {
                                    TextObject text_V100771 = (TextObject)report.FindObject("V100771");
                                    if (text_V100771 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100771.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                }
                                #endregion
                                #region 种族
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100772")
                                {
                                    TextObject text_V100772 = (TextObject)report.FindObject("V100772");
                                    if (text_V100772 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100772.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                }
                                #endregion
                                #region 母亲体重
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100774")
                                {
                                    TextObject text_V100774 = (TextObject)report.FindObject("V100774");
                                    if (text_V100774 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100774.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                }
                                #endregion
                                #region 孕周
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100775")
                                {
                                    TextObject text_V100775 = (TextObject)report.FindObject("V100775");
                                    if (text_V100775 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100775.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                }
                                #endregion
                                #region 预产年龄
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100776")
                                {
                                    TextObject text_V100776 = (TextObject)report.FindObject("V100776");
                                    if (text_V100776 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100776.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                }
                                #endregion
                                #region 计算方法
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100777")
                                {
                                    TextObject text_V100777 = (TextObject)report.FindObject("V100777");
                                    if (text_V100777 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100777.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                }
                                #endregion
                                #region 既往病史
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100773")
                                {
                                    TextObject text_V100773 = (TextObject)report.FindObject("V100773");
                                    if (text_V100773 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100773.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                }
                                #endregion
                                #region 临床建议
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100788")
                                {
                                    TextObject text_V100788 = (TextObject)report.FindObject("V100788");
                                    if (text_V100788 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100788.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                }
                                #endregion
                                #region 末次月经
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100789")
                                {
                                    TextObject text_V100789 = (TextObject)report.FindObject("V100789");
                                    if (text_V100789 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100789.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                }
                                #endregion
                                #region AFP
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "2010")
                                {
                                    TextObject text_v2010 = (TextObject)report.FindObject("V2010");
                                    if (text_v2010 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_v2010.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                    TextObject text_U2010 = (TextObject)report.FindObject("U2010");
                                    if (text_U2010 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("UNIT"))
                                        {
                                            text_U2010.Text += dtreportitem.Rows[i]["UNIT"].ToString();
                                        }

                                    }
                                    //TextObject text_R2010 = (TextObject)report.FindObject("R2010");
                                    //if (text_R2010 != null)
                                    //{
                                    //    if (dtreportitem.Columns.Contains("REFRANGE"))
                                    //    {
                                    //        text_R2010.Text += dtreportitem.Rows[i]["REFRANGE"].ToString();
                                    //    }

                                    //}
                                }
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100782")
                                {
                                    TextObject text_V100782 = (TextObject)report.FindObject("V100782");
                                    if (text_V100782 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100782.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }

                                    TextObject text_R100782 = (TextObject)report.FindObject("R100782");
                                    if (text_R100782 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REFRANGE"))
                                        {
                                            text_R100782.Text += dtreportitem.Rows[i]["REFRANGE"].ToString();
                                        }

                                    }
                                }

                                #endregion

                                #region Free-β-HCG
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "16000")
                                {
                                    TextObject text_V16000 = (TextObject)report.FindObject("V16000");
                                    if (text_V16000 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V16000.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                    TextObject text_U16000 = (TextObject)report.FindObject("U16000");
                                    if (text_U16000 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("UNIT"))
                                        {
                                            text_U16000.Text += dtreportitem.Rows[i]["UNIT"].ToString();
                                        }

                                    }
                                    //TextObject text_R16000 = (TextObject)report.FindObject("R16000");
                                    //if (text_R16000 != null)
                                    //{
                                    //    if (dtreportitem.Columns.Contains("REFRANGE"))
                                    //    {
                                    //        text_R16000.Text += dtreportitem.Rows[i]["REFRANGE"].ToString();
                                    //    }

                                    //}
                                }
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100783")
                                {
                                    TextObject text_V100783 = (TextObject)report.FindObject("V100783");
                                    if (text_V100783 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100783.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }

                                    TextObject text_R100783 = (TextObject)report.FindObject("R100783");
                                    if (text_R100783 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REFRANGE"))
                                        {
                                            text_R100783.Text += dtreportitem.Rows[i]["REFRANGE"].ToString();
                                        }

                                    }
                                }

                                #endregion

                                #region 唐氏综合症风险率
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100778")
                                {
                                    TextObject text_V100778 = (TextObject)report.FindObject("V100778");
                                    if (text_V100778 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100778.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }

                                    TextObject text_R100778 = (TextObject)report.FindObject("R100778");
                                    if (text_R100778 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REFRANGE"))
                                        {
                                            text_R100778.Text += dtreportitem.Rows[i]["REFRANGE"].ToString();
                                        }

                                    }
                                }
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100784")
                                {
                                    TextObject text_V100784 = (TextObject)report.FindObject("V100784");
                                    if (text_V100784 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100784.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                }

                                #endregion

                                #region 神经管缺陷风险
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100779")
                                {
                                    TextObject text_V100779 = (TextObject)report.FindObject("V100779");
                                    if (text_V100779 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100779.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }

                                    TextObject text_R100779 = (TextObject)report.FindObject("R100779");
                                    if (text_R100779 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REFRANGE"))
                                        {
                                            text_R100779.Text += dtreportitem.Rows[i]["REFRANGE"].ToString();
                                        }

                                    }
                                }
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100785")
                                {
                                    TextObject text_V100785 = (TextObject)report.FindObject("V100785");
                                    if (text_V100785 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100785.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                }

                                #endregion

                                #region 18三体综合症风险率
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100780")
                                {
                                    TextObject text_V100780 = (TextObject)report.FindObject("V100780");
                                    if (text_V100780 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100780.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }

                                    TextObject text_R100780 = (TextObject)report.FindObject("R100780");
                                    if (text_R100780 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REFRANGE"))
                                        {
                                            text_R100780.Text += dtreportitem.Rows[i]["REFRANGE"].ToString();
                                        }

                                    }
                                }
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100786")
                                {
                                    TextObject text_V100786 = (TextObject)report.FindObject("V100786");
                                    if (text_V100786 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100786.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                }

                                #endregion

                                #region 母龄风险
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100781")
                                {
                                    TextObject text_V100781 = (TextObject)report.FindObject("V100781");
                                    if (text_V100781 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100781.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                    TextObject text_U100781 = (TextObject)report.FindObject("U100781");
                                    if (text_U100781 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("UNIT"))
                                        {
                                            text_U100781.Text += dtreportitem.Rows[i]["UNIT"].ToString();
                                        }

                                    }
                                    TextObject text_R100781 = (TextObject)report.FindObject("R100781");
                                    if (text_R100781 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REFRANGE"))
                                        {
                                            text_R100781.Text += dtreportitem.Rows[i]["REFRANGE"].ToString();
                                        }

                                    }
                                }
                                if (dtreportitem.Rows[i]["ItemNo"].ToString() == "100787")
                                {
                                    TextObject text_V100787 = (TextObject)report.FindObject("V100787");
                                    if (text_V100787 != null)
                                    {
                                        if (dtreportitem.Columns.Contains("REPORTVALUEALL"))
                                        {
                                            text_V100787.Text += dtreportitem.Rows[i]["REPORTVALUEALL"].ToString();
                                        }

                                    }
                                }

                                #endregion

                            }
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
                FastReport.TextObject txtAdd = (FastReport.TextObject)report.FindObject("txtAdd");
                string txtAddTitle = "";
                if (txtAdd != null)
                {
                    txtAddTitle = txtAdd.Text;
                }
                if (rft == ReportFormTitle.center)
                {
                    FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                    if (texttitle != null)
                    {
                        texttitle.Text = ZhiFang.Common.Public.ConfigHelper.GetConfigString("CenterName") + txtAddTitle + "";
                    }
                    FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                    if (p != null)
                    {
                        //byte[] poperatorimage;
                        //System.IO.MemoryStream aaa = new System.IO.MemoryStream(pcheckerimage);
                        //System.Drawing.Image a ;
                        //a = p.Image;
                        //a.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"ReportPrint\PrintImage\logo.jpg");
                        //p.ImageLocation = @"D:\project\ljjPJ\vs2008\Report\Report\TmpHtmlPath\aaa.files\aaa.png";
                        p.Visible = true;
                    }
                    FastReport.PictureObject p1 = (FastReport.PictureObject)report.FindObject("logo");
                    if (p1 != null)
                    {
                        //byte[] poperatorimage;
                        //System.IO.MemoryStream aaa = new System.IO.MemoryStream(pcheckerimage);
                        //System.Drawing.Image a ;
                        //a = p.Image 
                        //a.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"ReportPrint\PrintImage\logo.jpg");
                        //p.ImageLocation = @"D:\project\ljjPJ\vs2008\Report\Report\TmpHtmlPath\aaa.files\aaa.png";
                        p1.Visible = true;
                    }
                }
                if (rft == ReportFormTitle.client)
                {
                    FastReport.TextObject texttitle = (FastReport.TextObject)report.FindObject("TextTitle");
                    if (texttitle != null)
                    {
                        texttitle.Text = drreportform["CLIENTNAME"].ToString() + txtAddTitle + "";
                    }
                    FastReport.PictureObject p = (FastReport.PictureObject)report.FindObject("Picture1");
                    if (p != null)
                    {
                        //System.Drawing.Image a;
                        //a = p.Image;
                        //a.Save(System.AppDomain.CurrentDomain.BaseDirectory + @"ReportPrint\PrintImage\logo.jpg");
                        p.Visible = false;
                    }
                    FastReport.PictureObject p1 = (FastReport.PictureObject)report.FindObject("logo");
                    if (p1 != null)
                    {
                        p1.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            }
        }

        public static void RegeditDataFR3(DataRow drreportform, DataTable dtreportitem, ref string reporttitle, ZhiFang.Common.Dictionary.ReportFormTitle rft)
        {
            try
            {
                if (rft == ReportFormTitle.center)
                {
                    reporttitle = ZhiFang.Common.Public.ConfigHelper.GetConfigString("CenterName");

                }
                if (rft == ReportFormTitle.client)
                {
                    reporttitle = drreportform["CLIENTNAME"].ToString();
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug(ex.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            }
        }

        #region 向模版图片信息
        public static void RegeditImageFRX(DataRow drreportform, ref FastReport.Report report)
        {
            ZhiFang.Common.Log.Log.Info("这是RegeditImage（）方法：ShowFrom.cs");
            try
            {
                string path = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() != "")
                {
                    if (drreportform != null && drreportform.Table.Columns.Contains("ReceiveDate") && drreportform["ReceiveDate"] != null && drreportform["ReceiveDate"].ToString().Trim() != "")
                    {
                        DateTime datetime = Convert.ToDateTime(drreportform["ReceiveDate"].ToString().Trim());
                        string date = "";
                        date = datetime.ToString("yyyy-MM-dd");
                        string[] ArrayDate = date.Split('-');
                        string linshiFormNo = "";
                        try
                        {
                            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo").Trim() != "" && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo").Trim() == "1")
                            {//TestTypeNo   检测类型编号
                                //SampleNo	样本号
                                linshiFormNo = datetime.Year + "-" + ArrayDate[1] + "-" + ArrayDate[2] + ";" + drreportform["SectionNo"] + ";" +
                                    drreportform["TestTypeNo"] + ";" + drreportform["SampleNo"];
                            }
                            else
                            {
                                linshiFormNo = drreportform["FormNo"].ToString();
                            }
                        }
                        catch (Exception)
                        {
                            linshiFormNo = drreportform["FormNo"].ToString();
                        }
                        path = ConfigHelper.GetConfigString("ReportIncludeImage").Trim() + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Year + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Month + "\\" + Convert.ToDateTime(datetime.ToString("yyyy-MM-dd")).Day + "\\" + linshiFormNo + "\\";
                        ZhiFang.Common.Log.Log.Info(path);
                        if (ZhiFang.Common.Public.FilesHelper.CheckDirectory(path))
                        {
                            string[] files = Directory.GetFiles(path);
                            for (int i = 0; i < files.Count(); i++)
                            {
                                string[] a = files[i].Split('@');
                                if (a.Count() > 1)
                                {
                                    if (a[3] == "NameImage")
                                    {
                                        //= (PictureObject)report.FindObject("PV" + (i + 1).ToString());
                                        PictureObject p = (PictureObject)report.FindObject(a[3] + a[5]);
                                        ZhiFang.Common.Log.Log.Info("NameImage图片" + a[3] + a[5]);
                                        //if (p != null)
                                        //{
                                        //    byte[] tmpa = (byte[])File.ReadAllBytes(path+"\\"+files[i]);
                                        //    if (tmpa != null && tmpa.Length > 0)
                                        //    {
                                        //        System.IO.MemoryStream aaa = new System.IO.MemoryStream(tmpa);
                                        //        System.Drawing.Image ia = Image.FromStream(aaa);
                                        //        p.Image = ia;
                                        //    }
                                        //}
                                        if (p != null)
                                        {
                                            p.ImageLocation = files[i];
                                            p.Visible = true;
                                            ZhiFang.Common.Log.Log.Info("NameImage图片名称" + files[i]);
                                        }
                                    }
                                    if (a[3] == "RFGraphData")
                                    {
                                        PictureObject p = (PictureObject)report.FindObject(a[3] + (int.Parse(a[2]) + 1).ToString());
                                        ZhiFang.Common.Log.Log.Info("RFGraphData图片" + a[3] + (int.Parse(a[2]) + 1).ToString());
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
                                            p.ImageLocation = files[i];
                                            p.Visible = true;
                                            ZhiFang.Common.Log.Log.Info("RFGraphData图片名称" + files[i]);
                                        }
                                    }
                                    if (a[3] == "S_RequestVItem")
                                    {
                                        PictureObject p = (PictureObject)report.FindObject("PV" + (int.Parse(a[2]) + 1).ToString());
                                        ZhiFang.Common.Log.Log.Info("S_RequestVItem图片PV" + (int.Parse(a[2]) + 1).ToString());
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
                                            p.ImageLocation = files[i];
                                            p.Visible = true;
                                            ZhiFang.Common.Log.Log.Info("S_RequestVItem图片名称" + files[i]);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString() + "---------" + e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
            }
        }
        public static string GetImagePath(DataRow drreportform)
        {
            try
            {
                string path = "";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() != "")
                {
                    if (drreportform != null && drreportform.Table.Columns.Contains("ReceiveDate") && drreportform["ReceiveDate"] != null && drreportform["ReceiveDate"].ToString().Trim() != "")
                    {
                        DateTime datetime = Convert.ToDateTime(drreportform["ReceiveDate"].ToString().Trim());
                        path = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportIncludeImage").Trim() + "\\" + datetime.Year + "\\" + datetime.Month + "\\" + datetime.Day + "\\" + drreportform["FormNo"].ToString() + "\\";
                    }
                }
                return path;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString() + "---------" + e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                return null;
            }
        }
        public static DataTable SetUserImage(DataTable reportform)
        {
            ALLReportForm arfb = new ALLReportForm();
            ZhiFang.Common.Log.Log.Info("这是SetUserImage(方法)：ShowFrom.cs");
            reportform.Columns.Add("TechnicianUrl", typeof(string));
            reportform.Columns.Add("OperatorUrl", typeof(string));
            reportform.Columns.Add("CheckerUrl", typeof(string));
            reportform.Columns.Add("PrintOperUrl", typeof(string));
            if (reportform.Rows[0]["Technician"].ToString().Trim() != "")
            {
                string tmpdir = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL"));
                string imagetype = ".jpg";
                string tmpfilename = reportform.Rows[0]["Technician"].ToString().Trim() + imagetype;
                if (!ZhiFang.Tools.FilesHelper.CheckDirFile(tmpdir, tmpfilename))
                {
                    DataTable dtimage = arfb.GetFromUserImage(reportform.Rows[0]["Technician"].ToString().Trim());
                    if (dtimage.Rows.Count > 0 && dtimage.Rows[0]["userimage"] != DBNull.Value)
                    {
                        ZhiFang.Tools.FilesHelper.CreatDirFile(tmpdir, tmpfilename, (Byte[])(dtimage.Rows[0]["userimage"]));
                        reportform.Rows[0]["TechnicianUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                    }
                }
                else
                {
                    reportform.Rows[0]["TechnicianUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                }
            }
            if (reportform.Rows[0]["Operator"].ToString().Trim() != "")
            {
                string tmpdir = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL"));
                string imagetype = ".jpg";
                string tmpfilename = reportform.Rows[0]["Operator"].ToString().Trim() + imagetype;
                if (!ZhiFang.Tools.FilesHelper.CheckDirFile(tmpdir, tmpfilename))
                {
                    DataTable dtimage = arfb.GetFromUserImage(reportform.Rows[0]["Operator"].ToString().Trim());
                    if (dtimage.Rows.Count > 0 && dtimage.Rows[0]["userimage"] != DBNull.Value)
                    {
                        ZhiFang.Tools.FilesHelper.CreatDirFile(tmpdir, tmpfilename, (Byte[])(dtimage.Rows[0]["userimage"]));
                        reportform.Rows[0]["OperatorUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                    }
                }
                else
                {
                    reportform.Rows[0]["OperatorUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                }
            }
            if (reportform.Rows[0]["Checker"].ToString().Trim() != "")
            {
                string tmpdir = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL"));
                string imagetype = ".jpg";
                string tmpfilename = reportform.Rows[0]["Checker"].ToString().Trim() + imagetype;
                if (!ZhiFang.Tools.FilesHelper.CheckDirFile(tmpdir, tmpfilename))
                {
                    DataTable dtimage = arfb.GetFromUserImage(reportform.Rows[0]["Checker"].ToString().Trim());
                    if (dtimage.Rows.Count > 0 && dtimage.Rows[0]["userimage"] != DBNull.Value)
                    {
                        ZhiFang.Tools.FilesHelper.CreatDirFile(tmpdir, tmpfilename, (Byte[])(dtimage.Rows[0]["userimage"]));
                        reportform.Rows[0]["CheckerUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                    }
                }
                else
                {
                    reportform.Rows[0]["CheckerUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                }
            }
            if (reportform.Rows[0]["PrintOper"].ToString().Trim() != "")
            {
                string tmpdir = ZhiFang.Common.Public.GetFilePath.GetPhysicsFilePath(ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL"));
                string imagetype = ".jpg";
                string tmpfilename = reportform.Rows[0]["PrintOper"].ToString().Trim() + imagetype;
                if (!ZhiFang.Tools.FilesHelper.CheckDirFile(tmpdir, tmpfilename))
                {
                    DataTable dtimage = arfb.GetFromUserImage(reportform.Rows[0]["PrintOper"].ToString().Trim());
                    if (dtimage.Rows.Count > 0 && dtimage.Rows[0]["userimage"] != DBNull.Value)
                    {
                        ZhiFang.Tools.FilesHelper.CreatDirFile(tmpdir, tmpfilename, (Byte[])(dtimage.Rows[0]["userimage"]));
                        reportform.Rows[0]["PrintOperUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                    }
                }
                else
                {
                    reportform.Rows[0]["PrintOperUrl"] = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LocalhostURL") + ZhiFang.Common.Public.ConfigHelper.GetConfigString("UserImagesURL") + "\\" + tmpfilename;
                }
            }
            return reportform;
        }
        #endregion

        public string GetOrderPrintHtmlContext(DataSet ds, string OrderNo)
        {
            //string htmlcontext = "";
            string imageUrl = "";
            string modulepath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + "OrderPrint" + "\\" + "OrderPrint.frx";
            string imagePath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" + "OrderPrint" + "\\" + OrderNo + "_OrderPrint.png";
            FastReport.Report report = new FastReport.Report();
            //给frx模板赋值
            SetOrderPrintFRX(ds, ref report, modulepath);
            FastReport.Export.Image.ImageExport tmpjpg = new FastReport.Export.Image.ImageExport();
           
            //FastReport.Export.Pdf.PDFExport tmppdf = new FastReport.Export.Pdf.PDFExport();
            //string imagetype = "jpeg";
            if (ConfigHelper.GetConfigInt("JpegQuality") != 0)
            {
                tmpjpg.JpegQuality = Convert.ToInt32(ConfigHelper.GetConfigInt("JpegQuality")) + 400;
            }
            tmpjpg.Resolution = 500;
            tmpjpg.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
            //tmpjpg.
            //生成PDF
            report.Export(tmpjpg, imagePath);
            if (tmpjpg.GeneratedFiles.Count > 0)
            {
                foreach (var filefullpath in tmpjpg.GeneratedFiles)
                {
                    //htmlcontext += "<img src='../" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "/" + "OrderPrint/" + "OrderPrint.png' border='0'/>";
                    string[] temp = filefullpath.Split('\\');
                    if (imageUrl == "")
                        //imageUrl += "../" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "/" + "OrderPrint/" + filefullpath + ".png";
                        imageUrl += "\"" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "/" + "OrderPrint/" + temp[temp.Length - 1] + "\"";
                    else
                        //imageUrl += "," + "../" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "/" + "OrderPrint/" + filefullpath + ".png";
                        imageUrl += ",\"" + ConfigHelper.GetConfigString("ReportFormFilesDir") + "/" + "OrderPrint/" + temp[temp.Length - 1] + "\"";

                }
            }
            //htmlcontext = "<html><head>    <title>清单打印</title>    <meta http-equiv='Content-Type' content='text/html; charset=utf-8' /></head><body > " + htmlcontext + "</body></html>";

            return "[" + imageUrl + "]";
        }

        private static void SetOrderPrintFRX(DataSet ds, ref FastReport.Report report, string modulepath)
        {

            try
            {
                report.Load(modulepath);
                report.RegisterData(ds);

                report.Prepare();
            }
            catch (Exception e)
            {

                throw;
            }
        }

    }
}
        #endregion