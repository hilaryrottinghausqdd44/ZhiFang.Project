using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ZhiFang.WebAssist.Common
{
    public class ImageReportHelp
    {
        /// <summary>
        /// 生成Image文件的存放根目录路径
        /// </summary>
        public static string BaseSaveImageDir { get; set; }
        /// <summary>
        /// 获取生成Image文件的存放路径
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="reportSubDir">报表二级存放路径</param>
        /// <returns></returns>
        public static string GetSaveImageSubDir(long labId, string reportSubDir)
        {
            if (string.IsNullOrEmpty(ImageReportHelp.BaseSaveImageDir))
                ImageReportHelp.BaseSaveImageDir = "ImageReport";
            string reportDir = "";
            if (labId >= 0)
                reportDir = labId.ToString() + "\\";
            if (!string.IsNullOrEmpty(reportSubDir))
                reportDir = reportDir + reportSubDir;
            return ImageReportHelp.BaseSaveImageDir + "\\" + reportDir;
        }
        /// <summary>
        /// 获取生成Image文件的存放全路径
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="reportSubDir">报表二级存放路径</param>
        /// <returns></returns>
        public static string GetSaveImageFullDir(long labId, string reportSubDir)
        {
            string tmpdate = ImageReportHelp.GetSaveImageSubDir(labId, reportSubDir);
            string pdfFilePath = System.AppDomain.CurrentDomain.BaseDirectory + tmpdate;
            if (!Directory.Exists(pdfFilePath))
                Directory.CreateDirectory(pdfFilePath);
            return pdfFilePath;
        }
        /// <summary>
        /// 获取已经生成好的Image文件
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="imageName">pdf文件名称</param>
        /// <param name="reportSubDir">报表二级存放路径</param>
        /// <returns></returns>
        public static Stream GetReportImage(long labId, string imageName, string reportSubDir)
        {
            Stream stream = null;
            if (string.IsNullOrEmpty(imageName)) return stream;
            string pdfFilePath = ImageReportHelp.GetSaveImageFullDir(labId, reportSubDir);
            pdfFilePath = pdfFilePath + "\\" + imageName;
            if (!File.Exists(pdfFilePath))
            {
                ZhiFang.Common.Log.Log.Error("Image文件路径为:" + pdfFilePath + ",不存在!");
            }
            FileStream fs = new FileStream(pdfFilePath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            stream = sr.BaseStream;
            //sr.Close();
            return stream;
        }
        /// <summary>
        /// 获取已经生成好的Image文件
        /// </summary>
        /// <param name="imageFullDir">Image文件存放路径</param>
        /// <returns></returns>
        public static Stream GetReportPDF(string imageFullDir)
        {
            Stream stream = null;
            if (!File.Exists(imageFullDir))
            {
                ZhiFang.Common.Log.Log.Error("Image文件路径为:" + imageFullDir + ",不存在!");
            }
            FileStream fs = new FileStream(imageFullDir, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            stream = sr.BaseStream;
            //sr.Close();
            return stream;
        }
    }
}
