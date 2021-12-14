using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FastReport;
using FastReport.Export.Pdf;
using ZhiFang.Common.Public;

namespace ZhiFang.SA.Common
{
    public class ReportBTemplateHelp
    {
        /// <summary>
        /// Excel/Frx报告模板存放根目录路径
        /// </summary>
        public static string ReportBaseDir { get; set; }
        /// <summary>
        /// 获取报告模板存放目录子路径
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="reportSubDir"></param>
        /// <returns></returns>
        public static string GetBTemplateSubDir(long labId, string publicTemplateDir, string reportSubDir)
        {
            if (string.IsNullOrEmpty(ReportBTemplateHelp.ReportBaseDir))
                ReportBTemplateHelp.ReportBaseDir = "Report";
            string reportDir = "";
            if (labId > 0)
                reportDir = labId.ToString() + "\\";
            if (!string.IsNullOrEmpty(publicTemplateDir))
                reportDir = reportDir + publicTemplateDir + "\\";
            if (!string.IsNullOrEmpty(reportSubDir))
                reportDir = reportDir + reportSubDir;
            return ReportBTemplateHelp.ReportBaseDir + "\\" + reportDir;
        }
        /// <summary>
        /// 获取报告模板存放目录全路径
        /// </summary>
        /// <param name="reportSubDir"></param>
        /// <returns></returns>
        public static string GetBTemplateFullDir(long labId, string publicTemplateDir, string reportSubDir)
        {
            string tmpdate = GetBTemplateSubDir(labId, publicTemplateDir, reportSubDir);
            //判断并创建报表目录
            string pdfPath = System.AppDomain.CurrentDomain.BaseDirectory + tmpdate;
            if (!Directory.Exists(pdfPath))
                Directory.CreateDirectory(pdfPath);
            return pdfPath;
        }
        /// <summary>
        /// 获取某一报告模板全路径
        /// </summary>
        /// <param name="labId"></param>
        /// <param name="publicTemplateDir"></param>
        /// <param name="reportSubDir"></param>
        /// <param name="frx"></param>
        /// <returns></returns>
        public static string GetBTemplateFullDir(long labId, string publicTemplateDir, string reportSubDir, string frx)
        {
            var reportDir = GetBTemplateFullDir(labId, publicTemplateDir, reportSubDir);
            string fileFullPath = Path.Combine(reportDir, frx);
            if (!File.Exists(fileFullPath))
            {
                ZhiFang.Common.Log.Log.Error("实验室的模板路径为:" + fileFullPath + ",不存在!");
                //获取公共模板
                reportDir = GetBTemplateFullDir(-1, publicTemplateDir, reportSubDir);
                fileFullPath = Path.Combine(reportDir, frx);
                if (!File.Exists(fileFullPath))
                {
                    fileFullPath = "";
                    ZhiFang.Common.Log.Log.Error("公共模板路径为:" + fileFullPath + ",不存在!");
                }
            }
            return fileFullPath;
        }
        public static DataTable ToDataTable<T>(IEnumerable<T> varlist, CreateRowDelegate<T> fn)
        {
            DataTable dtReturn = new DataTable();
            PropertyInfo[] oProps = null;
            foreach (T rec in varlist)
            {
                if (oProps == null)
                {
                    oProps = ((Type)rec.GetType()).GetProperties();
                    foreach (PropertyInfo pi in oProps)
                    {
                        Type colType = pi.PropertyType; if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                        {
                            if (pi.PropertyType.ToString() == "System.Nullable`1[System.DateTime]")
                            {
                                colType = typeof(DateTime);
                            }
                            else if (pi.PropertyType.ToString() == "System.Nullable`1[System.Int32]")
                            {
                                colType = typeof(Int32);
                            }
                            else if (pi.PropertyType.ToString() == "System.Nullable`1[System.Int64]")
                            {
                                colType = typeof(Int64);
                            }
                            else
                            {
                                colType = colType.GetGenericArguments()[0];
                            }
                        }
                        dtReturn.Columns.Add(new DataColumn(pi.Name, colType));
                    }
                }
                DataRow dr = dtReturn.NewRow(); foreach (PropertyInfo pi in oProps)
                {
                    dr[pi.Name] = pi.GetValue(rec, null) == null ? DBNull.Value : pi.GetValue(rec, null);
                }
                dtReturn.Rows.Add(dr);
            }
            return (dtReturn);
        }
        public delegate object[] CreateRowDelegate<T>(T t);
    }
}
