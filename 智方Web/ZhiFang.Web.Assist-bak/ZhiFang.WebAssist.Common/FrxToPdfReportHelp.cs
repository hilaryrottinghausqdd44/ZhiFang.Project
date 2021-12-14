using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using FastReport.Export.Pdf;

namespace ZhiFang.WebAssist.Common
{
    public class FrxToPdfReportHelp
    {
        /// <summary>
        /// 公共模板目录名称
        /// </summary>
        public static string PublicTemplateDir = "Frx模板";
        /// <summary>
        /// 生成PDF报表文件
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="labId"></param>
        /// <param name="pdfName"></param>
        /// <param name="publicTemplateDir"></param>
        /// <param name="reportSubDir"></param>
        /// <param name="frx"></param>
        /// <param name="isClose"></param>
        /// <returns></returns>
        public static Stream SavePdfReport(DataSet dataSet, long labId, string pdfName, string publicTemplateDir, string reportSubDir, string frx, bool isClose)
        {
            Stream stream = null;

            if (string.IsNullOrEmpty(pdfName) || string.IsNullOrEmpty(frx)) return stream;

            FastReport.Report report = new FastReport.Report();
            report.Clear();

            var reportDir = ReportBTemplateHelp.GetBTemplateFullDir(labId, publicTemplateDir, reportSubDir);
            if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
            {
                var reportFile = Path.Combine(reportDir, frx);
                //如果实验室的模板不存在,需要记录并提示或获取公共模板
                if (!File.Exists(reportFile))
                {
                    ZhiFang.Common.Log.Log.Error("实验室的模板路径为:" + reportFile + ",不存在!");
                    //获取公共模板
                    reportDir = ReportBTemplateHelp.GetBTemplateFullDir(-1, publicTemplateDir, reportSubDir);
                    reportFile = Path.Combine(reportDir, frx);
                    if (!File.Exists(reportFile))
                    {
                        ZhiFang.Common.Log.Log.Error("公共模板路径为:" + reportFile + ",不存在!");
                    }
                }

                try
                {
                    report.Load(reportFile);
                    report.RegisterData(dataSet);
                    //运行报表
                    bool result = report.Prepare();
                    if (!result)
                    {
                        stream=ResponseResultStream.GetErrMemoryStreamInfo("模板为:" + frx + ",生成报告失败!");
                        ZhiFang.Common.Log.Log.Error("模板为:" + frx + ",生成报告失败!");
                        return stream;
                    }
                }
                catch (Exception ex)
                {
                    stream = ResponseResultStream.GetErrMemoryStreamInfo("生成报表错误:" + ex.Message);
                    ZhiFang.Common.Log.Log.Error("生成报表错误:" + ex.Message);
                    //throw ex;
                }
                string savePath = PdfReportHelp.GetSavePdfFullDir(labId, reportSubDir);
                string pdfFullPath = savePath + "\\" + pdfName;
                //ZhiFang.Common.Log.Log.Error("生成报表.PdfFullPath:" + pdfFullPath);
                //生成PDF保存
                //FastReport.Export.Image.ImageExport
                PDFExport pdfexport = new PDFExport();
                report.Export(pdfexport, pdfFullPath);
                //导出excel
                //using (var reportExcel = new FastReport.Export.OoXML.Excel2007Export())
                //{
                //    reportExcel.Export(report, savePath + "\\result.xlsx");
                //}
                //using (var reportExcel = new FastReport.Export.OoXML.XPSExport())
                //{
                //    reportExcel.Export(report, savePath + "\\result.xps");
                //}
                //using (var reportExcel = new FastReport.Export.OoXML.Word2007Export())
                //{
                //    reportExcel.Export(report, savePath + "\\result.docx");
                //}

                //using (var htmlExport = new FastReport.Export.Html.HTMLExport())
                //{
                //    htmlExport.Export(report, savePath + "\\result.html");
                //}
                report.Dispose();
                stream = new FileStream(pdfFullPath, FileMode.Open);
                StreamReader sr = new StreamReader(stream);
                //stream = sr.BaseStream;
                if (isClose == true)
                    sr.Close();
            }
            return stream;
        }

    }
}
