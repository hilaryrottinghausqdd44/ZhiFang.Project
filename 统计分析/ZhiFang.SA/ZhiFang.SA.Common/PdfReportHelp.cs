using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZhiFang.SA.Common
{
    public class PdfReportHelp
    {
        /// <summary>
        /// 生成PDF文件的存放根目录路径
        /// </summary>
        public static string BaseSavePDFDir { get; set; }
        /// <summary>
        /// 获取生成PDF文件的存放路径
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="reportSubDir">报表二级存放路径</param>
        /// <returns></returns>
        public static string GetSavePDFSubDir(long labId, string reportSubDir)
        {
            if (string.IsNullOrEmpty(PdfReportHelp.BaseSavePDFDir))
                PdfReportHelp.BaseSavePDFDir = "PDFReport";
            string reportDir = "";
            if (labId > 0)
                reportDir = labId.ToString() + "\\";
            if (!string.IsNullOrEmpty(reportSubDir))
                reportDir = reportDir + reportSubDir;
            return PdfReportHelp.BaseSavePDFDir + "\\" + reportDir;
        }
        /// <summary>
        /// 获取生成PDF文件的存放全路径
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="reportSubDir">报表二级存放路径</param>
        /// <returns></returns>
        public static string GetSavePdfFullDir(long labId, string reportSubDir)
        {
            string tmpdate = PdfReportHelp.GetSavePDFSubDir(labId, reportSubDir);
            string pdfFilePath = System.AppDomain.CurrentDomain.BaseDirectory + tmpdate;
            if (!Directory.Exists(pdfFilePath))
                Directory.CreateDirectory(pdfFilePath);
            return pdfFilePath;
        }
        /// <summary>
        /// 获取已经生成好的PDF文件
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="pdfName">pdf文件名称</param>
        /// <param name="reportSubDir">报表二级存放路径</param>
        /// <returns></returns>
        public static Stream GetReportPDF(long labId, string pdfName, string reportSubDir)
        {
            Stream stream = null;
            if (string.IsNullOrEmpty(pdfName)) return stream;
            string pdfFilePath = PdfReportHelp.GetSavePdfFullDir(labId, reportSubDir);
            pdfFilePath = pdfFilePath + "\\" + pdfName;
            if (!File.Exists(pdfFilePath))
            {
                ZhiFang.Common.Log.Log.Error("PDF文件路径为:" + pdfFilePath + ",不存在!");
            }
            FileStream fs = new FileStream(pdfFilePath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            stream = sr.BaseStream;
            //sr.Close();
            return stream;
        }
        /// <summary>
        /// 获取已经生成好的PDF文件
        /// </summary>
        /// <param name="pdfFullDir">PDF文件存放路径</param>
        /// <returns></returns>
        public static Stream GetReportPDF(string pdfFullDir)
        {
            Stream stream = null;
            if (!File.Exists(pdfFullDir))
            {
                ZhiFang.Common.Log.Log.Error("PDF文件路径为:" + pdfFullDir + ",不存在!");
            }
            FileStream fs = new FileStream(pdfFullDir, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            stream = sr.BaseStream;
            //sr.Close();
            return stream;
        }
    }
}
