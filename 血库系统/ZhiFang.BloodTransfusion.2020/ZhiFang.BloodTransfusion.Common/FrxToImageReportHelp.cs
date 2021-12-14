using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using FastReport.Export.Image;
using FastReport.Export.Pdf;

namespace ZhiFang.BloodTransfusion.Common
{
    public class FrxToImageReportHelp
    {
        /// <summary>
        /// 公共模板目录名称
        /// </summary>
        public static string PublicTemplateDir = "Frx模板";

        public static string SaveImage(DataSet dataSet, long labId, string fileName, string publicTemplateDir, string reportSubDir, string frx, bool isClose)
        {
            FastReport.Report report = new FastReport.Report();
            report.Clear();
            string fullPath = "";
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
                        ZhiFang.Common.Log.Log.Error("模板为:" + frx + ",生成报告失败!");
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("生成报表错误:" + ex.Message);
                    throw ex;
                }
                string savePath = ImageReportHelp.GetSaveImageFullDir(labId, reportSubDir);
                fullPath = savePath + "\\" + fileName;
                ImageExport imageExport = new ImageExport();
                //{
                //    JpegQuality = 100,
                //    Resolution = 300
                //};
                imageExport.ImageFormat = ImageExportFormat.Png;
                report.Export(imageExport, fullPath);
                report.Dispose();
                //ZhiFang.Common.Log.Log.Error("生成报表.fullPath:" + fullPath);
            }
            return fullPath;
        }

        /// <summary>
        /// 生成PDF报表文件
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="labId"></param>
        /// <param name="fileName"></param>
        /// <param name="publicTemplateDir"></param>
        /// <param name="reportSubDir"></param>
        /// <param name="frx"></param>
        /// <param name="isClose"></param>
        /// <returns></returns>
        public static Stream GetImageReport(DataSet dataSet, long labId, string fileName, string publicTemplateDir, string reportSubDir, string frx, bool isClose)
        {
            Stream stream = null;

            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(frx)) return stream;

            string fullPath = SaveImage(dataSet, labId, fileName, publicTemplateDir, reportSubDir, frx, isClose);
            if (string.IsNullOrEmpty(fullPath)) return stream;

            stream = new FileStream(fullPath, FileMode.Open);
            StreamReader sr = new StreamReader(stream);
            //stream = sr.BaseStream;
            if (isClose == true)
                sr.Close();

            return stream;
        }
        public static string GetImageReportToBase64String(DataSet dataSet, long labId, string fileName, string publicTemplateDir, string reportSubDir, string frx, bool isClose)
        {
            string fullPath = SaveImage(dataSet, labId, fileName, publicTemplateDir, reportSubDir, frx, isClose);
            if (string.IsNullOrEmpty(fullPath)) return "";

            try
            {
                Bitmap bmp = new Bitmap(fullPath);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                ms.Dispose();
                string strBASE64Code = Convert.ToBase64String(arr);
                bmp.Dispose();
                return strBASE64Code;
            }
            catch (Exception ex)
            {
                return "";
            }
        }
        public static MemoryStream GetImageReportToMemoryStream(DataSet dataSet, long labId, string fileName, string publicTemplateDir, string reportSubDir, string frx, bool isClose)
        {
            string fullPath = "";
            var reportDir = ReportBTemplateHelp.GetBTemplateFullDir(labId, publicTemplateDir, reportSubDir);
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
            string savePath = ImageReportHelp.GetSaveImageFullDir(labId, reportSubDir);
            fullPath = savePath + "\\" + fileName;
            if (string.IsNullOrEmpty(fullPath)) return null;
            System.IO.MemoryStream reportStream = new System.IO.MemoryStream();
            using (FastReport.Report report = new FastReport.Report())
            {
                report.Load(reportFile);
                report.RegisterData(dataSet);
                bool IsPrepared = report.Prepare();
                ImageExport imgExport = new ImageExport()
                {
                    JpegQuality = 100,
                    Resolution = 300
                };
                report.Export(imgExport, reportStream);
                reportStream.Position = 0;
                StreamReader sr = new StreamReader(reportStream);
                if (isClose == true)
                    sr.Close();
                return reportStream;
            }
        }
    }
}
