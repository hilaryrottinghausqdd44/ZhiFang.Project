using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ECDS.Common;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Configuration;
using System.IO;

namespace ZhiFang.WebLisService.WebService
{
    /// <summary>
    /// DownLoadReportForm 的摘要说明
    /// </summary>
    [WebService(Namespace = "WebLisReportFromService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class DownLoadReportForm : System.Web.Services.WebService
    {

        [WebMethod]
        public int QueryStatus(string CName, string PassWord, string SerialNo, string orgCode, string ReportPara, out byte[] ReportValue, out string ReturnDesc)
        {
            if (CheckUser(CName, PassWord))
            {
                Log.Info(String.Format("下载报告列表开始用户={0},SerialNo={1},orgCode={2}", CName, SerialNo, orgCode));
                ReturnDesc = "";
                ReportValue = null;
                try
                {
                    if (SerialNo == null && SerialNo.Trim() == "")
                    {
                        ReturnDesc = "参数申请单号为空!";
                        ReportValue = null;
                        return -2;
                    }
                    if (orgCode == null && orgCode.Trim() == "")
                    {
                        ReturnDesc = "参数医院编码为空!";
                        ReportValue = null;
                        return -2;
                    }
                    string whereSQL = " SerialNo='" + SerialNo.Replace("\'", "\'\'").Replace("'", "''").Replace("<", "&lt;").Replace(">", "&gt;") + "' and CLIENTNO='" + orgCode.Replace("\'", "\'\'").Replace("'", "''").Replace("<", "&lt;").Replace(">", "&gt;") + "' ";
                    ReportValue = this.DownloadReportFormBy_HisOrder_SerialNo(whereSQL);
                    if (ReportValue == null)
                    {
                        ReturnDesc = "无报告或报告文件!";
                        return 0;
                    }
                }
                catch (System.Exception ex)
                {
                    ReturnDesc = "根据检验报告单编号获取检验报告单ReportForm列表出错:" + ex.Message;
                    ReportValue = null;
                    return -1;
                }
                return 0;
            }
            else
            {
                ReturnDesc = "用户名或密码出错！";
                ReportValue = null;
                return 0;
            }

        }

        private bool CheckUser(string CName, string PassWord)
        {
            try
            {
                string pattern = @"*|and|exec|insert|select|delete|update|count|master|truncate|declare|char(|mid(|chr(|'";
                if (Regex.Match(CName, Regex.Escape(pattern), RegexOptions.Compiled | RegexOptions.IgnoreCase).Success)
                {
                    Log.Info(String.Format("用户={0}字符串中含有非法字符!" , CName));
                    return false;
                }
                else
                {
                    string sql = " select top 1 * from RBAC_Users where Account='" + CName.Replace("\'", "\'\'").Replace("'", "''").Replace("<", "&lt;").Replace(">", "&gt;") + "'";
                    DataSet ds = new DataSet();
                    ds = WL.BLL.DataConn.CreateDB("ConnectionStringOA").ExecDS(sql);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Password"].ToString().Trim() == PassWord.Trim())
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch(Exception e)
            {
                Log.Info(String.Format("用户={0}身份验证出错！"+e.ToString(), CName));
                return false;
            }
        }

        public byte[] DownloadReportFormBy_HisOrder_SerialNo(string whereSQL)
        {
            string strFileName = "";
            if (whereSQL == "")
                //whereSQL = "2>1";
                return null;
            //string sql = "select receivedate,cname,serialno,sectionno,sampleno,resultfile,printtimes,PDFFILE from reportformfull where " + whereSQL + " order by printtimes asc";
            string sql = "select PDFFILE from reportformfull where " + whereSQL + " order by printtimes asc";
            Log.Info(sql);
            DataSet ds = WL.BLL.DataConn.CreateDB().ExecDS(sql);
            if (ECDS.Common.Security.FormatTools.CheckDataSet(ds))
            {
                strFileName = ds.Tables[0].Rows[0][0].ToString();
                return downLoadPDFFileContent(strFileName);
            }
            else
            {
                return null;
            }
        }
        public static byte[] downLoadPDFFileContent(string fileName)
        {
            string msg = "调用方法“downLoadPDFFileContent”下载某个文件(比如PDF文件),返回流";
            Log.Info(msg);

            byte[] pdfContent = null;
            string path = fileName;
            if (System.IO.File.Exists(path) == false)
            {
                //默认取上报的pdf等报表存放目录
                path = ConfigurationSettings.AppSettings["ReportConfigPath"] +"\\"+ fileName;
                //path = System.AppDomain.CurrentDomain.BaseDirectory + fileName;
            }
            if (System.IO.File.Exists(path))
            {
                System.IO.FileStream fileStream = new System.IO.FileStream(path, FileMode.Open);
                long fileSize = fileStream.Length;
                pdfContent = new byte[fileSize];
                fileStream.Read(pdfContent, 0, (int)fileSize);
                fileStream.Close();
            }
            else
            {
                msg = "文件“" + path + "”不存在！";
                Log.Info(msg);
            }
            return pdfContent;
        }
    }
}