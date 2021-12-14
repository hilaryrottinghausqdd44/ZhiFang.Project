using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.Services;
using ZhiFang.Common.Log;
using ZhiFang.ReportFormQueryPrint.Common;

using System.IO;
using System.Runtime.InteropServices;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF.Customization
{
    /// <summary>
    /// 宁波市第一医院定制服务：根据传入条件查询报告单信息，从自定义路径获取pdf文件，转成base64并返回
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class NingBoDiYiYiYuan : System.Web.Services.WebService
    {

        private readonly ZhiFang.ReportFormQueryPrint.BLL.BALLReportForm barf = new BLL.BALLReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BLL.BReportForm();
        [WebMethod]
        public bool GetReportFormPDFByFieldsAndFilePath_Base64String(List<string> fields, List<string> values, List<string> order, int RangeDay, int IsPrintTimes, int flag, out string[] ReportFormPdfPathBtis, out string ErrorInfo)
        {
            ReportFormPdfPathBtis = null;
            ErrorInfo = "";
            string UserName = ConfigHelper.GetConfigString("ShareUserName");//访问共享盘的账号
            string PassWord = ConfigHelper.GetConfigString("SharePassWord");//访问共享盘的密码
            string ShareIP = ConfigHelper.GetConfigString("ShareIP");//共享盘的ip
            string ShareDirectory = ConfigHelper.GetConfigString("ShareDirectory");//共享盘的共享文件夹
            if (string.IsNullOrEmpty(UserName))
            {
                ZhiFang.Common.Log.Log.Debug("getPdfStreamByPath:未配置共享盘登录名");
                return false;
            }
            if (string.IsNullOrEmpty(PassWord))
            {
                ZhiFang.Common.Log.Log.Debug("getPdfStreamByPath:未配置共享盘登录密码");
                return false;
            }
            if (string.IsNullOrEmpty(ShareIP))
            {
                ZhiFang.Common.Log.Log.Debug("getPdfStreamByPath:未配置共享盘IP");
                return false;
            }
            if (string.IsNullOrEmpty(ShareDirectory))
            {
                ZhiFang.Common.Log.Log.Debug("getPdfStreamByPath:未配置共享盘共享文件夹");
                return false;
            }
            try
            {
                if (fields == null || fields.Count <= 0 || fields[0].Length <= 2)
                {
                    ErrorInfo = "fields为空!";
                    return false;
                }
                if (values == null || values.Count <= 0 || values[0].Length <= 2)
                {
                    ErrorInfo = "values为空!";
                    return false;
                }
                string urlOrder = " order by ";

                if (order == null || order.Count <= 0 || order[0].Length <= 2)
                {
                    urlOrder = " order by ReceiveDate desc";
                }
                else
                {
                    foreach (var item in order)
                    {
                        urlOrder += item + ", ";
                    }
                    urlOrder = urlOrder.Substring(0, urlOrder.Length - 2);
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFieldsAndFilePath_Base64String.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFieldsAndFilePath_Base64String.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFieldsAndFilePath_Base64String.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFieldsAndFilePath_Base64String.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " ";
                for (int i = 0; i < fields.Count; i++)
                {
                    urlWhere += fields[i] + "='" + values[i] + "' and ";
                }
                urlWhere = urlWhere.Substring(0, urlWhere.Length - 4);

                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);

                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "')";

                DataSet ds = barf.GetList_FormFull("ReportFormID,CheckDate,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere + urlOrder);
                //根据配置文件配置的文件夹路径和报告命名规则获取pdf，命名规则：f：labstar服务端/pdf/2021-06-04/条码号.pdf 即 配置的文件夹路径/审核日期/条码号.pdf

                string NingBoPdfFilePath = ConfigHelper.GetConfigString("NingBoPdfFilePath");//获取配置文件配置的路径
                if (string.IsNullOrEmpty(NingBoPdfFilePath))
                {
                    ZhiFang.Common.Log.Log.Debug("getPdfStreamByPath:未配置共享盘共享文件夹");
                    ErrorInfo = "出现异常请查看日志";
                    return false;
                }
                DateTime checkDate = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDate"].ToString().Trim());//获取审核日期

                string pdfPath = NingBoPdfFilePath + checkDate.ToString("yyyy-MM-dd") + "\\" + values[0] + ".pdf";
                bool isPdf = false;
                //连接共享盘
                using (SharedTool tool = new SharedTool(UserName, PassWord, ShareIP))
                { 
                    if (File.Exists(pdfPath))
                    {
                        ZhiFang.Common.Log.Log.Debug("找到PDF文件，文件位置：" + pdfPath);
                        byte[] byteArray = ByteHelper.File2Bytes(pdfPath);//文件转成byte二进制数组
                        string JarContent = Convert.ToBase64String(byteArray);//将二进制转成string类型
                        ReportFormPdfPathBtis = new string[1];
                        ReportFormPdfPathBtis[0] = JarContent;
                        isPdf = true;
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("未找到PDF文件，文件位置：" + pdfPath);
                        ErrorInfo = "未找到PDF文件，文件位置：" + pdfPath;
                    }
                }
                if (isPdf)
                {
                    List<string> reportformidlist = new List<string>();

                    reportformidlist.Add(ds.Tables[0].Rows[0]["ReportFormID"].ToString().Trim());

                    #region 打印次数累计
                    ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields_Base64String:IsPrintTimes=" + IsPrintTimes);
                    if (IsPrintTimes == 1) //等于1记录打印次数
                    {
                        ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields_Base64String:开始记录打印次数");
                        if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                        {
                            DataTable dsreportform = new DataTable();
                            dsreportform = brf.GetReportFormList(reportformidlist.ToArray());
                            if (dsreportform == null || dsreportform.Rows.Count <= 0)
                            {
                                ErrorInfo = "LIS中没有找到报告单";
                                return false;
                            }
                            List<string> reportformidlist66 = new List<string>();
                            for (int i = 0; i < dsreportform.Rows.Count; i++)
                            {
                                reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                            }
                            IDAL.IDReportForm dal = Factory.DalFactory<IDAL.IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                            bool Ms66Flag = false;
                            Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray(), "");
                            ZhiFang.Common.Log.Log.Debug("向Lis添加打印次数:" + Ms66Flag);
                            Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                            ZhiFang.Common.Log.Log.Debug("向Lis添加外围打印次数:" + Ms66Flag);
                        }
                        //获得的客户端ip
                        string ip = HttpContext.Current.Request.UserHostAddress;

                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFieldsAndFilePath_Base64String.IP:" + ip + "添加打印次数:reportformID:" + reportformidlist.ToArray().ToString());
                        brf.UpdatePrintTimes(reportformidlist.ToArray(), "");//报告库打标记
                        brf.UpdateClientPrintTimes(reportformidlist.ToArray());//外围打印标记
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFieldsAndFilePath_Base64String:不记录打印次数");
                    }
                    #endregion

                }
                return isPdf;
                //return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFieldsAndFilePath_Base64String.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFieldsAndFilePath_Base64String:" + e.ToString());
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }

        public class SharedTool : IDisposable
        {
            // obtains user token       
            [DllImport("advapi32.dll", SetLastError = true)]
            static extern bool LogonUser(string pszUsername, string pszDomain, string pszPassword,
                int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

            // closes open handes returned by LogonUser       
            [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
            extern static bool CloseHandle(IntPtr handle);

            [DllImport("Advapi32.DLL")]
            static extern bool ImpersonateLoggedOnUser(IntPtr hToken);

            [DllImport("Advapi32.DLL")]
            static extern bool RevertToSelf();
            const int LOGON32_PROVIDER_DEFAULT = 0;
            const int LOGON32_LOGON_NEWCREDENTIALS = 9;//域控中的需要用:Interactive = 2       
            private bool disposed;

            public SharedTool(string username, string password, string ip)
            {
                // initialize tokens       
                IntPtr pExistingTokenHandle = new IntPtr(0);
                IntPtr pDuplicateTokenHandle = new IntPtr(0);

                try
                {
                    // get handle to token       
                    bool bImpersonated = LogonUser(username, ip, password,
                        LOGON32_LOGON_NEWCREDENTIALS, LOGON32_PROVIDER_DEFAULT, ref pExistingTokenHandle);

                    if (bImpersonated)
                    {
                        if (!ImpersonateLoggedOnUser(pExistingTokenHandle))
                        {
                            int nErrorCode = Marshal.GetLastWin32Error();
                            throw new Exception("ImpersonateLoggedOnUser error;Code=" + nErrorCode);
                        }
                    }
                    else
                    {
                        int nErrorCode = Marshal.GetLastWin32Error();
                        throw new Exception("LogonUser error;Code=" + nErrorCode);
                    }
                }
                finally
                {
                    // close handle(s)       
                    if (pExistingTokenHandle != IntPtr.Zero)
                        CloseHandle(pExistingTokenHandle);
                    if (pDuplicateTokenHandle != IntPtr.Zero)
                        CloseHandle(pDuplicateTokenHandle);
                }
            }

            protected virtual void Dispose(bool disposing)
            {
                if (!disposed)
                {
                    RevertToSelf();
                    disposed = true;
                }
            }

            public void Dispose()
            {
                Dispose(true);
            }
        }
    }
}
