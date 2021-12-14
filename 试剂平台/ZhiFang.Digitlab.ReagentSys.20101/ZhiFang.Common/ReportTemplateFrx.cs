using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastReport;
using FastReport.Export.Pdf;
using System.Data;
using ZhiFang.Common.Public;
using System.Web;

namespace ZhiFang.Common.Public
{
    public class ReportTemplateFrx
    {
        /// <summary>
        /// 生成报告文件
        /// </summary>
        /// <param name="ds">数据源</param>
        /// <param name="templatecode">模版代码</param>
        /// <param name="rft">文件类型</param>
        /// <returns>生成文件后的文件存放地址</returns>
        public static string CreatReport(DataSet ds, string templatecode, ReportFileType rft)
        {
            Report report = new Report();
            string url = "";
            string strModelPath = Common.Public.ConfigHelper.GetConfigString("ReportTemplateDir");
            string strBasePath=GetFilePath.GetPhysicsFilePath(strModelPath + "/");
            report.Clear();
            report.Load(strBasePath + templatecode + Common.Public.ConfigHelper.GetConfigString("ReportTemplateFileExtension"));
            report.RegisterData(ds);
            report.Prepare();
            string guid = GUIDHelp.GetGUIDString();
            switch (rft)
            {
                case ReportFileType.PDF:
                    #region PDF
                    PDFExport exportpdf = new PDFExport();
                    exportpdf.EmbeddingFonts = true;
                    exportpdf.ShowProgress = true;
                    exportpdf.Compressed = true;
                    exportpdf.Background = true;
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\PDF\\"))
                    {
                        report.Export(exportpdf, strBasePath + "\\PDF\\" + guid + "." + rft.ToString());
                    }
                    url = strBasePath + "\\PDF\\" + guid + "." + rft.ToString();
                    break;
                    #endregion
                case ReportFileType.JPG:
                    #region JPG
                    FastReport.Export.Image.ImageExport ie = new FastReport.Export.Image.ImageExport();
                        ie.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
                        ie.JpegQuality = 100;
                        ie.Resolution = 100;
                        if (FilesHelper.CheckAndCreatDir(strBasePath + "\\JPG\\"))
                        {
                            report.Export(ie, strBasePath + "\\JPG\\" + guid + "." + rft.ToString());
                        }
                        url = strBasePath + "\\JPG\\" + guid + "." + rft.ToString();
                    break;
                    #endregion
                case ReportFileType.EXCEL:
                    #region EXCEL
                    FastReport.Export.OoXML.Excel2007Export excele= new FastReport.Export.OoXML.Excel2007Export();
                    if (FilesHelper.CheckAndCreatDir(strBasePath +"\\EXCEL\\"))
                        {
                            report.Export(excele, strBasePath + "\\EXCEL\\" + guid + ".xlsx");
                        }
                    url = strBasePath + "\\EXCEL\\" + guid + ".xlsx";
                    break;
                    #endregion
                case ReportFileType.WORD:
                    #region WORD
                    FastReport.Export.RichText.RTFExport worde = new FastReport.Export.RichText.RTFExport();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\WORD\\"))
                        {
                            report.Export(worde, strBasePath + "\\WORD\\" + guid + ".rtf");
                        }
                        url = strBasePath + "\\WORD\\" + guid + ".rtf";
                    break;
                    #endregion
                case ReportFileType.HTML:
                    #region HTML
                    FastReport.Export.Mht.MHTExport mhte = new FastReport.Export.Mht.MHTExport();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\HTML\\"))
                        {
                            report.Export(mhte, strBasePath + "\\HTML\\" + guid + ".mht");
                        }
                        url = strBasePath + "\\HTML\\" + guid + ".mht";
                    break;
                    #endregion
                case ReportFileType.XML:
                    #region XML
                    FastReport.Export.Xml.XMLExport xmle = new FastReport.Export.Xml.XMLExport();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\XML\\"))
                        {
                            report.Export(xmle, strBasePath + "\\XML\\" + guid + ".xml");
                        }
                        url = strBasePath + "\\XML\\" + guid + ".xml";
                    break;
                    #endregion
                default:
                    #region PDF
                    PDFExport export = new PDFExport();
                    export.EmbeddingFonts = false;
                    export.ShowProgress = false;
                    export.Compressed = true;
                    export.Background = true;
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\PDF\\"))
                    {
                        report.Export(export, strBasePath + "\\PDF\\" + guid + "." + rft.ToString());
                    }
                    break;
                    #endregion
            }

            report.Dispose();
            return url;
        }

        public static string CreatReportReturnUrl(DataSet ds, string templatecode, ReportFileType rft)
        {
            Report report = new Report();
            string url = "";
            string strModelPath = Common.Public.ConfigHelper.GetConfigString("ReportTemplateDir");
            string strBasePath = GetFilePath.GetPhysicsFilePath(strModelPath + "/");
            report.Clear();
            report.Load(strBasePath + templatecode + Common.Public.ConfigHelper.GetConfigString("ReportTemplateFileExtension"));
            report.RegisterData(ds);
            report.Prepare();
            string guid = GUIDHelp.GetGUIDString();
            switch (rft)
            {
                case ReportFileType.PDF:
                    #region PDF
                    PDFExport exportpdf = new PDFExport();
                    exportpdf.EmbeddingFonts = true;
                    exportpdf.ShowProgress = true;
                    exportpdf.Compressed = true;
                    exportpdf.Background = true;
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "/PDF/"))
                    {
                        report.Export(exportpdf, strBasePath + "/PDF/" + guid + "." + rft.ToString());
                    }
                    url = strModelPath + "/PDF/" + guid + "." + rft.ToString();
                    break;
                    #endregion
                case ReportFileType.JPG:
                    #region JPG
                    FastReport.Export.Image.ImageExport ie = new FastReport.Export.Image.ImageExport();
                    ie.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
                    ie.JpegQuality = 100;
                    ie.Resolution = 100;
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "/JPG/"))
                    {
                        report.Export(ie, strBasePath + "/JPG/" + guid + "." + rft.ToString());
                    }
                    url = strModelPath + "/JPG/" + guid + "." + rft.ToString();
                    break;
                    #endregion
                case ReportFileType.EXCEL:
                    #region EXCEL
                    FastReport.Export.OoXML.Excel2007Export excele = new FastReport.Export.OoXML.Excel2007Export();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "/EXCEL/"))
                    {
                        report.Export(excele, strBasePath + "/EXCEL/" + guid + ".xlsx");
                    }
                    url = strModelPath + "/EXCEL/" + guid + ".xlsx";
                    break;
                    #endregion
                case ReportFileType.WORD:
                    #region WORD
                    FastReport.Export.RichText.RTFExport worde = new FastReport.Export.RichText.RTFExport();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "/WORD/"))
                    {
                        report.Export(worde, strBasePath + "/WORD/" + guid + ".rtf");
                    }
                    url = strModelPath + "/WORD/" + guid + ".rtf";
                    break;
                    #endregion
                case ReportFileType.HTML:
                    #region HTML
                    FastReport.Export.Html.HTMLExport html = new FastReport.Export.Html.HTMLExport();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "/HTML/"))
                    {
                        report.Export(html, strBasePath + "/HTML/" + guid + ".html");
                    }
                    //FastReport.Export.Mht.MHTExport mhte = new FastReport.Export.Mht.MHTExport();
                    //if (FilesHelper.CheckAndCreatDir(strBasePath + "/HTML/"))
                    //{
                    //    report.Export(mhte, strBasePath + "/HTML/" + guid + ".mht");
                    //}
                    url = strModelPath + "/HTML/" + guid + ".html";
                    break;
                    #endregion
                case ReportFileType.XML:
                    #region XML
                    FastReport.Export.Xml.XMLExport xmle = new FastReport.Export.Xml.XMLExport();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "/XML/"))
                    {
                        report.Export(xmle, strBasePath + "/XML/" + guid + ".xml");
                    }
                    url = strModelPath + "/XML/" + guid + ".xml";
                    break;
                    #endregion
                default:
                    #region PDF
                    PDFExport export = new PDFExport();
                    export.EmbeddingFonts = false;
                    export.ShowProgress = false;
                    export.Compressed = true;
                    export.Background = true;
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "/PDF/"))
                    {
                        report.Export(export, strBasePath + "/PDF/" + guid + "." + rft.ToString());
                    }
                    url = strModelPath + "/PDF/" + guid + "." + rft.ToString();
                    break;
                    #endregion
            }

            report.Dispose();
            return url;
        }

        /// <summary>
        /// 自定义生成报告文件
        /// </summary>
        /// <param name="ds">数据源</param>
        /// <param name="templatecode">模版代码</param>
        /// <param name="rft">文件类型</param>
        /// <returns>生成文件后的文件存放地址</returns>
        public static string CreatReportCustom(DataSet ds, string templatecode, ReportFileType rft)
        {
            Report report = new Report();
            string url = "";
            string strModelPath = Common.Public.ConfigHelper.GetConfigString("ReportTemplateDir");
            string strBasePath = GetFilePath.GetPhysicsFilePath(strModelPath + "/");
            report.Clear();
            report.Load(strBasePath + templatecode + Common.Public.ConfigHelper.GetConfigString("ReportTemplateFileextension"));
            report.RegisterData(ds);


            TextObject t1 = new TextObject();
            t1.Text = "aaaa";
            t1.Name="txtTest";

            report.AllObjects.Add(t1);

            report.Prepare();
            string guid = GUIDHelp.GetGUIDString();
            switch (rft)
            {
                case ReportFileType.PDF:
                    #region PDF
                    PDFExport exportpdf = new PDFExport();
                    exportpdf.EmbeddingFonts = false;
                    exportpdf.ShowProgress = false;
                    exportpdf.Compressed = true;
                    exportpdf.Background = true;
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\PDF\\"))
                    {
                        report.Export(exportpdf, strBasePath + "\\PDF\\" + guid + "." + rft.ToString());
                    }
                    url = strBasePath + "\\PDF\\" + guid + "." + rft.ToString();
                    break;
                    #endregion
                case ReportFileType.JPG:
                    #region JPG
                    FastReport.Export.Image.ImageExport ie = new FastReport.Export.Image.ImageExport();
                    ie.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
                    ie.JpegQuality = 100;
                    ie.Resolution = 100;
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\JPG\\"))
                    {
                        report.Export(ie, strBasePath + "\\JPG\\" + guid + "." + rft.ToString());
                    }
                    //ie.GeneratedFiles
                    url = strBasePath + "\\JPG\\" + guid + "." + rft.ToString();
                    break;
                    #endregion
                case ReportFileType.EXCEL:
                    #region EXCEL
                    FastReport.Export.OoXML.Excel2007Export excele = new FastReport.Export.OoXML.Excel2007Export();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\EXCEL\\"))
                    {
                        report.Export(excele, strBasePath + "\\EXCEL\\" + guid + ".xlsx");
                    }
                    url = strBasePath + "\\EXCEL\\" + guid + ".xlsx";
                    break;
                    #endregion
                case ReportFileType.WORD:
                    #region WORD
                    FastReport.Export.RichText.RTFExport worde = new FastReport.Export.RichText.RTFExport();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\WORD\\"))
                    {
                        report.Export(worde, strBasePath + "\\WORD\\" + guid + ".rtf");
                    }
                    url = strBasePath + "\\WORD\\" + guid + ".rtf";
                    break;
                    #endregion
                case ReportFileType.HTML:
                    #region HTML
                    FastReport.Export.Mht.MHTExport mhte = new FastReport.Export.Mht.MHTExport();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\HTML\\"))
                    {
                        report.Export(mhte, strBasePath + "\\HTML\\" + guid + ".mht");
                    }
                    url = strBasePath + "\\HTML\\" + guid + ".mht";
                    break;
                    #endregion
                case ReportFileType.XML:
                    #region XML
                    FastReport.Export.Xml.XMLExport xmle = new FastReport.Export.Xml.XMLExport();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\XML\\"))
                    {
                        report.Export(xmle, strBasePath + "\\XML\\" + guid + ".xml");
                    }
                    url = strBasePath + "\\XML\\" + guid + ".xml";
                    break;
                    #endregion
                default:
                    #region PDF
                    PDFExport export = new PDFExport();
                    export.EmbeddingFonts = false;
                    export.ShowProgress = false;
                    export.Compressed = true;
                    export.Background = true;
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\PDF\\"))
                    {
                        report.Export(export, strBasePath + "\\PDF\\" + guid + "." + rft.ToString());
                    }
                    break;
                    #endregion
            }

            report.Dispose();
            return url;
        }

        /// <summary>
        /// 生成报告文件
        /// </summary>
        /// <param name="ds">数据源</param>
        /// <param name="templatecode">模版代码</param>
        /// <param name="rft">文件类型</param>
        /// <returns>生成文件后的文件存放地址</returns>
        public static List<string> CreatReportReturnFiles(DataSet ds, string templatecode, ReportFileType rft)
        {
            Report report = new Report();
            List<string> url = new List<string>();
            string strModelPath = Common.Public.ConfigHelper.GetConfigString("ReportTemplateDir");
            string strBasePath = GetFilePath.GetPhysicsFilePath(strModelPath + "/");
            report.Clear();
            report.Load(strBasePath + templatecode + Common.Public.ConfigHelper.GetConfigString("ReportTemplateFileExtension"));
            report.RegisterData(ds);
            report.Prepare();
            string guid = GUIDHelp.GetGUIDString();
            switch (rft)
            {
                case ReportFileType.PDF:
                    #region PDF
                    PDFExport exportpdf = new PDFExport();
                    exportpdf.EmbeddingFonts = true;
                    exportpdf.ShowProgress = true;
                    exportpdf.Compressed = true;
                    exportpdf.Background = true;
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\PDF\\"))
                    {
                        report.Export(exportpdf, strBasePath + "\\PDF\\" + guid + "." + rft.ToString());
                    }
                    url = exportpdf.GeneratedFiles;
                    break;
                    #endregion
                case ReportFileType.JPG:
                    #region JPG
                    FastReport.Export.Image.ImageExport ie = new FastReport.Export.Image.ImageExport();
                    ie.ImageFormat = FastReport.Export.Image.ImageExportFormat.Jpeg;
                    ie.JpegQuality = 100;
                    ie.Resolution = 100;
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\JPG\\"))
                    {
                        report.Export(ie, strBasePath + "\\JPG\\" + guid + "." + rft.ToString());
                    }
                    url = ie.GeneratedFiles;
                    break;
                    #endregion
                case ReportFileType.EXCEL:
                    #region EXCEL
                    FastReport.Export.OoXML.Excel2007Export excele = new FastReport.Export.OoXML.Excel2007Export();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\EXCEL\\"))
                    {
                        report.Export(excele, strBasePath + "\\EXCEL\\" + guid + ".xlsx");
                    }
                    url = excele.GeneratedFiles;
                    break;
                    #endregion
                case ReportFileType.WORD:
                    #region WORD
                    FastReport.Export.RichText.RTFExport worde = new FastReport.Export.RichText.RTFExport();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\WORD\\"))
                    {
                        report.Export(worde, strBasePath + "\\WORD\\" + guid + ".rtf");
                    }
                    url = worde.GeneratedFiles;
                    break;
                    #endregion
                case ReportFileType.HTML:
                    #region HTML
                    FastReport.Export.Mht.MHTExport mhte = new FastReport.Export.Mht.MHTExport();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\HTML\\"))
                    {
                        report.Export(mhte, strBasePath + "\\HTML\\" + guid + ".mht");
                    }
                    url = mhte.GeneratedFiles;
                    break;
                    #endregion
                case ReportFileType.XML:
                    #region XML
                    FastReport.Export.Xml.XMLExport xmle = new FastReport.Export.Xml.XMLExport();
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\XML\\"))
                    {
                        report.Export(xmle, strBasePath + "\\XML\\" + guid + ".xml");
                    }
                    url = xmle.GeneratedFiles;
                    break;
                    #endregion
                default:
                    #region PDF
                    PDFExport export = new PDFExport();
                    export.EmbeddingFonts = false;
                    export.ShowProgress = false;
                    export.Compressed = true;
                    export.Background = true;
                    if (FilesHelper.CheckAndCreatDir(strBasePath + "\\PDF\\"))
                    {
                        report.Export(export, strBasePath + "\\PDF\\" + guid + "." + rft.ToString());
                    }
                    url = export.GeneratedFiles;
                    break;
                    #endregion
            }

            report.Dispose();
            return url;
        }
    }
}
