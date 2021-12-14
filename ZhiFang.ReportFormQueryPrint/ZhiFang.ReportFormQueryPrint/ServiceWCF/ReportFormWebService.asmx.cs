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
using ZhiFang.ReportFormQueryPrint.IDAL;
using System.Text;
using System.IO;
using ZhiFang.ReportFormQueryPrint.Model.VO;
using ZhiFang.ReportFormQueryPrint.BLL;
using System.Xml;
using ZhiFang.ReportFormQueryPrint.BLL.Print;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.ServiceWCF
{
    /// <summary>
    /// ReportFormService1 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    [System.Web.Script.Services.ScriptService]
    public class ReportFormWebService : System.Web.Services.WebService
    {
        private readonly ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BALLReportForm barf = new BLL.BALLReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BReportForm brf = new BLL.BReportForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BRequestForm bqf = new BLL.BRequestForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BNRequestForm bnrf = new BLL.BNRequestForm();
        private readonly ZhiFang.ReportFormQueryPrint.BLL.BNRequestItem bnri = new BLL.BNRequestItem();

        [WebMethod(Description = "根据ReportFormID（报告库）生成PDF报告并返回报告URL")]
        public bool GetReportFormPDFByReportFormID(string ReportFormID, int RangeDay, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ReportFormPdfPath = null;
            ErrorInfo = "";
            try
            {
                if (ReportFormID == null || ReportFormID.Trim().Length <= 0)
                {
                    ErrorInfo = "ReportFormID为空!";
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByReportFormID.ReportFormID:" + ReportFormID + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByReportFormID.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByReportFormID.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByReportFormID.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " ReportFormID=" + ReportFormID;
                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);


                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "')";
                DataSet ds = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere);
                return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByReportFormID.异常:" + e.ToString() + ".ReportFormID:" + ReportFormID + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }

        [WebMethod(Description = "根据FormNo(四个关键字Receivedate;Section;TestTypeno;Sampleno)生成PDF报告并返回报告URL")]
        public bool GetReportFormPDFByFormNo(string FormNo, string FormNoFormat, string SplitChar, int RangeDay, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ReportFormPdfPath = null;
            ErrorInfo = "";
            try
            {
                if (FormNo == null || FormNo.Trim().Length <= 0)
                {
                    ErrorInfo = "报告单号为空!";
                    return false;
                }
                if (SplitChar == null || SplitChar.Trim().Length <= 0)
                {
                    SplitChar = ";";
                }
                if (FormNoFormat == null || FormNoFormat.Trim().Length <= 0)
                {
                    FormNoFormat = "ReceiveDate" + SplitChar + "SectionNo" + SplitChar + "TestTypeNo" + SplitChar + "SampleNo";
                }

                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.FormNo:" + FormNo + ", FormNoFormat:" + FormNoFormat + ", SplitChar:" + SplitChar + ", RangeDay:" + RangeDay + ", flag:" + flag);
                string[] formnop = FormNo.Split(SplitChar.Trim().ToCharArray()[0]);
                if (formnop.Length != 4)
                {
                    ErrorInfo = "报告单号或分割符出错!";
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.报告单号或分割符出错。FormNo:" + FormNo + ", SplitChar:" + SplitChar);
                    return false;
                }
                string[] formnoformatp = FormNoFormat.Split(SplitChar.Trim().ToCharArray()[0]);
                if (formnoformatp.Length != 4)
                {
                    ErrorInfo = "报告单号格式或分割符出错!";
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.报告单号或分割符出错。FormNoFormat:" + FormNoFormat);
                    return false;
                }
                string urlWhere = " " + formnoformatp[0] + "='" + formnop[0] + "' and " + formnoformatp[1] + "='" + formnop[1] + "' and " + formnoformatp[2] + "='" + formnop[2] + "' and " + formnoformatp[3] + "='" + formnop[3] + "' ";

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }

                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);


                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "')";
                DataSet ds = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere);
                string BloodGlucoseTestingConfiguration = ConfigHelper.GetConfigString("BloodGlucoseTestingConfiguration");
                if (!string.IsNullOrWhiteSpace(BloodGlucoseTestingConfiguration) && BloodGlucoseTestingConfiguration == "1")
                {
                    ReportFormPdfPath = new string[ds.Tables[0].Rows.Count];
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        List<ReportFormFilesVO> listreportformfile = new List<ReportFormFilesVO>();
                        //判断是否糖耐量等特殊报告，如果是，多份没全部检验完成则返回提示信息，都检验完成则合并到一份报告
                        if (!ZhongRiBloodGlucoseTesting(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim(), ds.Tables[0].Rows[i]["SecretType"].ToString().Trim(), 0, ref ErrorInfo, ref listreportformfile))
                        {
                            listreportformfile = bprf.CreatReportFormFiles(new List<string>() { ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim() }, ReportFormTitle.center, ReportFormFileType.PDF, ds.Tables[0].Rows[i]["SecretType"].ToString().Trim(), 0);
                        }
                        if (listreportformfile.Count > 0)
                        {
                            ReportFormPdfPath[i] = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + listreportformfile[0].PDFPath.Replace(@"\", "/");
                            
                        }
                    }
                    return true;
                }
                else
                {
                    return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
                }
                    
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo.异常:" + e.ToString() + ".FormNo:" + FormNo + ", FormNoFormat:" + FormNoFormat + ", SplitChar:" + SplitChar + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }

        [WebMethod(Description = "根据条码号生成PDF报告并返回报告URL")]
        public bool GetReportFormPDFByBarCode(string BarCode, int RangeDay, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ReportFormPdfPath = null;
            ErrorInfo = "";
            try
            {
                if (BarCode == null || BarCode.Trim().Length <= 0)
                {
                    ErrorInfo = "BarCode为空!";
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByBarCode.SerialNo:" + BarCode + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByBarCode.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByBarCode.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByBarCode.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " SerialNo='" + BarCode + "' ";
                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);


                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "')";
                DataSet ds = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere);
                return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByBarCode.异常:" + e.ToString() + ".SerialNo:" + BarCode + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }
        [WebMethod(Description = "根据病历号生成PDF报告并返回报告URL")]
        public bool GetReportFormPDFByPatNo(string PatNo, int RangeDay, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ReportFormPdfPath = null;
            ErrorInfo = "";
            try
            {
                if (PatNo == null || PatNo.Trim().Length <= 0)
                {
                    ErrorInfo = "PatNo为空!";
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByPatNo.PatNo:" + PatNo + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByPatNo.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByPatNo.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByPatNo.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " PatNo='" + PatNo + "' ";
                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);

                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "' and ISNULL(ClientPrint,0) <1)";
                DataSet ds = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere);
                bool isPdf = GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);

                if (isPdf)
                {
                    List<string> reportformidlist = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
                        reportformidlist.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                    }
                    #region 打印次数累计
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
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加外围打印次数:reportformID:" + PatNo);
                    brf.UpdatePrintTimes(reportformidlist.ToArray(), "");//报告库打标记
                    brf.UpdateClientPrintTimes(reportformidlist.ToArray());//外围打印标记
                    #endregion
                }
                return isPdf;
                //return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByPatNo.异常:" + e.ToString() + ".PatNo:" + PatNo + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }
        [WebMethod(Description = "根据病历号生成PDF报告并返回报告URL，不限制打印次数")]
        public bool GetReportFormPDFByPatNo_NoClientPrint(string PatNo, int RangeDay, int flag, int IsPrintTimes, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ReportFormPdfPath = null;
            ErrorInfo = "";
            try
            {
                if (PatNo == null || PatNo.Trim().Length <= 0)
                {
                    ErrorInfo = "PatNo为空!";
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByPatNo_NoClientPrint.PatNo:" + PatNo + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByPatNo_NoClientPrint.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByPatNo_NoClientPrint.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByPatNo_NoClientPrint.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " PatNo='" + PatNo + "' ";
                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);

                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "')";
                DataSet ds = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere);
                bool isPdf = GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);

                if (isPdf)
                {
                    List<string> reportformidlist = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        reportformidlist.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                    }
                    #region 打印次数累计
                    if (IsPrintTimes == 1) //等于1记录打印次数
                    {
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
                        ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加外围打印次数:reportformID:" + PatNo);
                        brf.UpdatePrintTimes(reportformidlist.ToArray(), "");//报告库打标记
                        brf.UpdateClientPrintTimes(reportformidlist.ToArray());//外围打印标记
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByPatNo_NoClientPrint:不记录打印次数");
                    }
                    #endregion
                }
                return isPdf;
                //return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByPatNo_NoClientPrint.异常:" + e.ToString() + ".PatNo:" + PatNo + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }
        [WebMethod(Description = "根据就诊号生成PDF报告并返回报告URL")]
        public bool GetReportFormPDFByZDY17(string ZDY17, int RangeDay, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ReportFormPdfPath = null;
            ErrorInfo = "";
            try
            {
                if (ZDY17 == null || ZDY17.Trim().Length <= 0)
                {
                    ErrorInfo = "ZDY17为空!";
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByZDY17.ZDY17:" + ZDY17 + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByZDY17.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByZDY17.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByZDY17.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " ZDY17='" + ZDY17 + "' ";
                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);

                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "' and ClientPrint <1)";
                DataSet ds = barf.GetList_FormFull("ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere);
                bool isPdf = GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);

                if (isPdf)
                {
                    List<string> reportformidlist = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        reportformidlist.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                    }
                    #region 打印次数累计
                    brf.UpdatePrintTimes(reportformidlist.ToArray(), "");//报告库打标记
                    brf.UpdateClientPrintTimes(reportformidlist.ToArray());//外围打印标记
                    #endregion
                }
                return isPdf;
                //return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByZDY17.异常:" + e.ToString() + ".ZDY17:" + ZDY17 + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }

        [WebMethod(Description = "根据传入字段和值生成PDF报告并返回报告URL")]
        public bool GetReportFormPDFByFields(List<string> fields, List<string> values, List<string> order, int RangeDay, int IsPrintTimes, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ReportFormPdfPath = null;
             ErrorInfo = "";
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
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
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

                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "' and ISNULL(ClientPrint,0) <1)";
                DataSet ds = barf.GetList_FormFull("ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere + urlOrder);
                bool isPdf = GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);

                if (isPdf)
                {
                    List<string> reportformidlist = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        reportformidlist.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                    }
                    #region 打印次数累计
                    ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields:IsPrintTimes=" + IsPrintTimes);
                    if (IsPrintTimes == 1) //等于1记录打印次数
                    {
                        ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields:开始记录打印次数");
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

                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields.IP:" + ip + "添加打印次数:reportformID:" + reportformidlist.ToArray().ToString());
                        brf.UpdatePrintTimes(reportformidlist.ToArray(), "");//报告库打标记
                        brf.UpdateClientPrintTimes(reportformidlist.ToArray());//外围打印标记
                    }
                    else {
                        ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields:不记录打印次数");
                    }
                    #endregion


                }
                return isPdf;
                //return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields:" + e.ToString());
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }
        [WebMethod(Description = "根据传入字段和值生成PDF报告并返回报告URL，不限制打印次数")]
        public bool GetReportFormPDFByFields_NoClientPrint(List<string> fields, List<string> values, List<string> order, int RangeDay, int IsPrintTimes, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ReportFormPdfPath = null;
            ErrorInfo = "";
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
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_NoClientPrint.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_NoClientPrint.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_NoClientPrint.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_NoClientPrint.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
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
                DataSet ds = barf.GetList_FormFull("ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere + urlOrder);
                bool isPdf = GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);

                if (isPdf)
                {
                    List<string> reportformidlist = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        reportformidlist.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                    }
                    #region 打印次数累计
                    ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields_NoClientPrint:IsPrintTimes=" + IsPrintTimes);
                    if (IsPrintTimes == 1) //等于1记录打印次数
                    {
                        ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields_NoClientPrint:开始记录打印次数");
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

                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_NoClientPrint.IP:" + ip + "添加打印次数:reportformID:" + reportformidlist.ToArray().ToString());
                        brf.UpdatePrintTimes(reportformidlist.ToArray(), "");//报告库打标记
                        brf.UpdateClientPrintTimes(reportformidlist.ToArray());//外围打印标记
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields_NoClientPrint:不记录打印次数");
                    }
                    #endregion
                }
                return isPdf;
                //return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields:" + e.ToString());
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }

        [WebMethod(Description = "根据传入reportFormId增加打印次数")]
        public bool UpdateReportPrintTimes(string reportformidstr, out string ErrorInfo) {

            string ip = HttpContext.Current.Request.UserHostAddress;

            bool success = false;
            ErrorInfo = "";
            try
            {
                string[] reportformlist = reportformidstr.Split(',');
                bool Ms66Flag = true;
                bool flag = true;
                #region 向lis加入打印次数
                if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                {
                    DataTable dsreportform = new DataTable();
                    dsreportform = brf.GetReportFormList(reportformlist);
                    if (dsreportform == null || dsreportform.Rows.Count <= 0)
                    {
                        success = false;
                        ErrorInfo = "LIS中没有找到报告单";
                    }
                    List<string> reportformidlist66 = new List<string>();
                    for (int i = 0; i < dsreportform.Rows.Count; i++)
                    {
                        reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                    }
                    IDReportForm dal = Factory.DalFactory<IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                    Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray(), "");
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加打印次数:" + Ms66Flag);
                    Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "向Lis添加外围打印次数:" + Ms66Flag);
                }
                #endregion

                flag = brf.UpdatePrintTimes(reportformlist, "");
                ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimes.IP:" + ip + "添加打印次数:reportformID:" + reportformidstr);
                flag = brf.UpdateClientPrintTimes(reportformlist);
                ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimes.IP:" + ip + "添加外围打印次数:reportformID:" + reportformidstr);
                if (Ms66Flag && flag)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception e)
            {
                success = false;
                ErrorInfo = e.ToString();
            }
            return success;
        }
        
        
        public static bool GetReportFormPDFByDS(DataSet ds, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
            List<string> rfids = new List<string>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    rfids.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString());
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByDS.没找到报告,请检查报告日期及查询条件。");
                ErrorInfo = "没找到报告,请检查报告日期及查询条件。";
                ReportFormPdfPath = null;
                return false;
            }
            ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByDS.rfids:" + string.Join(",", rfids));
            var listreportformfile = bprf.CreatReportFormFiles(rfids, ReportFormTitle.center, ReportFormFileType.PDF, "1", flag);
            if (listreportformfile.Count > 0)
            {

                ReportFormPdfPath = new string[listreportformfile.Count];
                for (int i = 0; i < listreportformfile.Count; i++)
                {
                    ReportFormPdfPath[i] = "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + listreportformfile[i].PDFPath.Replace(@"\", "/");
                }
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByDS.返回pdf路径:" + string.Join(",", ReportFormPdfPath));
                ErrorInfo = "";
                return true;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByDS.未查找到报告单");
                ErrorInfo = "未查找到报告单";
                ReportFormPdfPath = null;
                return false;
            }
        }

        [WebMethod(Description = "根据ReportFormID（报告库）生成PDF报告并返回报告FTP路径")]
        public bool GetReportFormPDFByReportFormID_FTP(string ReportFormID, int RangeDay, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ReportFormPdfPath = null;
            ErrorInfo = "";
            try
            {
                if (ReportFormID == null || ReportFormID.Trim().Length <= 0)
                {
                    ErrorInfo = "ReportFormID为空!";
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByReportFormID_FTP.ReportFormID:" + ReportFormID + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByReportFormID_FTP.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByReportFormID_FTP.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByReportFormID_FTP.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " ReportFormID=" + ReportFormID;
                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);


                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "')";
                DataSet ds = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere);
                bool IsDs = GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
                if (ReportFormPdfPath != null)
                {

                    for (int i = 0; i < ReportFormPdfPath.Length; i++)
                    {
                        ReportFormPdfPath[i] = ReportFormPdfPath[i].Replace("http://", "ftp://");
                    }
                }
                return IsDs;

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByReportFormID_FTP.异常:" + e.ToString() + ".ReportFormID:" + ReportFormID + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }

        [WebMethod(Description = "根据FormNo(四个关键字Receivedate;Section;TestTypeno;Sampleno)生成PDF报告并返回报告FTP路径")]
        public bool GetReportFormPDFByFormNo_FTP(string FormNo, string FormNoFormat, string SplitChar, int RangeDay, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            double d = DateTime.Now.Ticks / 10000;
            ReportFormPdfPath = null;
            ErrorInfo = "";
            try
            {
                if (FormNo == null || FormNo.Trim().Length <= 0)
                {
                    ErrorInfo = "报告单号为空!";
                    return false;
                }
                if (SplitChar == null || SplitChar.Trim().Length <= 0)
                {
                    SplitChar = ";";
                }
                if (FormNoFormat == null || FormNoFormat.Trim().Length <= 0)
                {
                    FormNoFormat = "ReceiveDate" + SplitChar + "SectionNo" + SplitChar + "TestTypeNo" + SplitChar + "SampleNo";
                }

                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.FormNo:" + FormNo + ", FormNoFormat:" + FormNoFormat + ", SplitChar:" + SplitChar + ", RangeDay:" + RangeDay + ", flag:" + flag);
                string[] formnop = FormNo.Split(SplitChar.Trim().ToCharArray()[0]);
                if (formnop.Length != 4)
                {
                    ErrorInfo = "报告单号或分割符出错!";
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.报告单号或分割符出错。FormNo:" + FormNo + ", SplitChar:" + SplitChar);
                    return false;
                }
                string[] formnoformatp = FormNoFormat.Split(SplitChar.Trim().ToCharArray()[0]);
                if (formnoformatp.Length != 4)
                {
                    ErrorInfo = "报告单号格式或分割符出错!";
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.报告单号或分割符出错。FormNoFormat:" + FormNoFormat);
                    return false;
                }
                string urlWhere = " " + formnoformatp[0] + "='" + formnop[0] + "' and " + formnoformatp[1] + "='" + formnop[1] + "' and " + formnoformatp[2] + "='" + formnop[2] + "' and " + formnoformatp[3] + "='" + formnop[3] + "' ";

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }

                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);


                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "')";

                double k = DateTime.Now.Ticks / 10000;
                DataSet ds = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere);
                double j = DateTime.Now.Ticks / 10000;
                double t = (j - k) / 1000;
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByFormNo_FTP查询报告数据时间：" + t.ToString());
                double a = DateTime.Now.Ticks / 10000;
                bool IsDs = GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
                double b = DateTime.Now.Ticks / 10000;
                double c = (j - k) / 1000;
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByFormNo_FTP生成报告并返回报告路径时间：" + c.ToString());
                if (ReportFormPdfPath != null)
                {

                    for (int i = 0; i < ReportFormPdfPath.Length; i++)
                    {
                        ReportFormPdfPath[i] = ReportFormPdfPath[i].Replace("http://", "ftp://");
                    }
                }
                double e = DateTime.Now.Ticks / 10000;
                double f = (j - k) / 1000;
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFByFormNo_FTP服务总体执行时间：" + f.ToString());
                return IsDs;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFormNo_FTP.异常:" + e.ToString() + ".FormNo:" + FormNo + ", FormNoFormat:" + FormNoFormat + ", SplitChar:" + SplitChar + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }


        [WebMethod(Description = "根据传入FormId(四个关键字Receivedate;SectionNo;TestTypeno;Sampleno)增加打印次数，多个报告单使用','分割")]
        public bool UpdateReportPrintTimesByFormId(string reportformidstr, out string ErrorInfo)
        {

            string ip = HttpContext.Current.Request.UserHostAddress;
            ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimesByFormId.调用增加打印次数开始.IP:" + ip);
            bool success = false;
            ErrorInfo = "";
            try
            {
                bool Ms66Flag = true;
                bool flag = true;

                //分割传入的FormID(四个关键字)开始
                string[] reportformlist = reportformidstr.Split(',');
                StringBuilder formIdurl = new StringBuilder();
                for (var i = 0; i < reportformlist.Length; i++) {
                    var rfls = reportformlist[i].Split(';');
                    if (rfls.Length != 4) {
                        success = false;
                        ErrorInfo = "传入参数错误";
                        return success;
                    }
                    if (i == 0)
                    {
                        formIdurl.Append(" (Receivedate = '" + rfls[0] + "' and SectionNo = '" + rfls[1] + "' and TestTypeNo = '" + rfls[2] + "' and SampleNo = '" + rfls[3] + "') ");
                    }
                    else {
                        formIdurl.Append(" OR (Receivedate = '" + rfls[0] + "' and SectionNo = '" + rfls[1] + "' and TestTypeNo = '" + rfls[2] + "' and SampleNo = '" + rfls[3] + "') ");
                    }

                }//分割传入的FormID(四个关键字)结束
                ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimesByFormId.查询报告单条件.formIdurl:" + formIdurl.ToString());


                DataTable dsreportform = new DataTable();
                dsreportform = brf.GetReportFormListByFormId(formIdurl.ToString());//根据传入参数查询报告单
                if (dsreportform == null || dsreportform.Rows.Count <= 0)
                {
                    success = false;
                    ErrorInfo = "没有找到报告单";
                }

                #region 向lis加入打印次数
                if (ConfigHelper.GetConfigString("IsLisAddPrintTime").Equals("1")) //向Lis添加打印次数
                {
                    List<string> reportformidlist66 = new List<string>();
                    for (int i = 0; i < dsreportform.Rows.Count; i++)
                    {
                        reportformidlist66.Add(dsreportform.Rows[i]["Receivedate"].ToString().Trim() + ";" + dsreportform.Rows[i]["SectionNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["TestTypeNo"].ToString().Trim() + ";" + dsreportform.Rows[i]["SampleNo"].ToString().Trim());
                    }
                    IDReportForm dal = Factory.DalFactory<IDReportForm>.GetDal("ReportForm", "ZhiFang.ReportFormQueryPrint.DAL.MSSQL.MSSQL66", ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigString("LISConnectionString"));//LIS库打标记
                    Ms66Flag = dal.UpdatePrintTimes(reportformidlist66.ToArray(), "");
                    ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimesByFormId.IP:" + ip + "向Lis添加打印次数:" + Ms66Flag);
                    Ms66Flag = dal.UpdateClientPrintTimes(reportformidlist66.ToArray());//外围打印标记
                    ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimesByFormId.IP:" + ip + "向Lis添加外围打印次数:" + Ms66Flag);
                }
                #endregion


                string reportformarr = "";
                for (var i = 0; i < dsreportform.Rows.Count; i++)
                {
                    reportformarr += dsreportform.Rows[i]["ReportFormID"].ToString().Trim() + ",";
                }
                string[] reportformidarr = reportformarr.Split(',');

                flag = brf.UpdatePrintTimes(reportformidarr, "");
                ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimesByFormId.IP:" + ip + "添加打印次数:reportformID:" + reportformidarr);
                flag = brf.UpdateClientPrintTimes(reportformidarr);
                ZhiFang.Common.Log.Log.Debug("UpdateReportPrintTimesByFormId.IP:" + ip + "添加外围打印次数:reportformID:" + reportformidarr);
                if (Ms66Flag && flag)
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
                ZhiFang.Common.Log.Log.Debug("调用增加打印次数结束UpdateReportPrintTimesByFormId.IP:" + ip);
            }
            catch (Exception e)
            {
                success = false;
                ErrorInfo = e.ToString();
            }
            return success;
        }

        [WebMethod(Description = "根据病历号生成PDF报告并返回报告二进制流")]
        public bool GetReportFormBitsPDFByPatNo(string PatNo, int RangeDay, int flag, out string[] ReportFormPdfBits, out string ErrorInfo)
        {
            ZhiFang.Common.Log.Log.Debug("根据病历号生成PDF二进制流开始.GetReportFormBitsPDFByPatNo.PatNo");
            ReportFormPdfBits = null;
            ErrorInfo = "";
            try
            {
                if (PatNo == null || PatNo.Trim().Length <= 0)
                {
                    ErrorInfo = "PatNo为空!";
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormBitsPDFByPatNo.PatNo:" + PatNo + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormBitsPDFByPatNo.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormBitsPDFByPatNo.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormBitsPDFByPatNo.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " PatNo='" + PatNo + "' ";
                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);

                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "')";
                DataSet ds = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere);
                bool isPdf = GetReportFormPDFBitsByDS(ds, flag, out ReportFormPdfBits, out ErrorInfo);

                ZhiFang.Common.Log.Log.Debug("根据病历号生成PDF二进制流结束.GetReportFormBitsPDFByPatNo.PatNo");
                return isPdf;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormBitsPDFByPatNo.异常:" + e.ToString() + ".PatNo:" + PatNo + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }

        [WebMethod(Description = "生成PDF报告并返回报告二进制流")]
        public static bool GetReportFormPDFBitsByDS(DataSet ds, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ZhiFang.ReportFormQueryPrint.BLL.Print.PrintReportForm bprf = new BLL.Print.PrintReportForm();
            List<string> rfids = new List<string>();
            var itemDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//获取项目物理路径
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    rfids.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString());
                }
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDS.没找到报告,请检查报告日期及查询条件。");
                ErrorInfo = "没找到报告,请检查报告日期及查询条件。";
                ReportFormPdfPath = null;
                return false;
            }
            ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDS.rfids:" + string.Join(",", rfids));
            var listreportformfile = bprf.CreatReportFormFiles(rfids, ReportFormTitle.center, ReportFormFileType.PDF, "1", flag);
            if (listreportformfile.Count > 0)
            {

                ReportFormPdfPath = new string[listreportformfile.Count];
                for (int i = 0; i < listreportformfile.Count; i++)
                {
                    try
                    {
                        byte[] byteArray = ByteHelper.File2Bytes(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/"));//文件转成byte二进制数组
                        string JarContent = Convert.ToBase64String(byteArray);//将二进制转成string类型

                        ReportFormPdfPath[i] = JarContent;
                    }
                    catch (Exception ex)
                    {
                        ErrorInfo = ex.Message;
                    }

                    if (File.Exists(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/")))
                    {
                        File.Delete(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/"));
                    }
                }
                //ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDS.pdf物理路径:" + string.Join(",", ReportFormPdfPath));
                ErrorInfo = "";
                return true;
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDS.未查找到报告单");
                ErrorInfo = "未查找到报告单";
                ReportFormPdfPath = null;
                return false;
            }
        }

        [WebMethod(Description = "根据传入字段和值生成PDF报告并返回报告Base64字符串")]
        public bool GetReportFormPDFByFields_Base64String(List<string> fields, List<string> values, List<string> order, int RangeDay, int IsPrintTimes, int flag, out string[] ReportFormPdfPathBtis, out string ErrorInfo)
        {
            ReportFormPdfPathBtis = null;
            ErrorInfo = "";
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
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
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

                //urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "' and ClientPrint <1)";
                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "' )";
                DataSet ds = barf.GetList_FormFull("ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere + urlOrder);
                bool isPdf = GetReportFormPDFBitsByDS(ds, flag, out ReportFormPdfPathBtis, out ErrorInfo);
                ZhiFang.Common.Log.Log.Debug(ReportFormPdfPathBtis[0].ToString());
                if (isPdf)
                {
                    List<string> reportformidlist = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        reportformidlist.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                    }
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

                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.IP:" + ip + "添加打印次数:reportformID:" + reportformidlist.ToArray().ToString());
                        brf.UpdatePrintTimes(reportformidlist.ToArray(), "");//报告库打标记
                        brf.UpdateClientPrintTimes(reportformidlist.ToArray());//外围打印标记
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("WebService调用.GetReportFormPDFByFields_Base64String:不记录打印次数");
                    }
                    #endregion

                }
                return isPdf;
                //return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormPDFByFields_Base64String:" + e.ToString());
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }
        [WebMethod(Description = "根据传入字段和值获取检验中和检验后的报告单数量")]
        public bool GetReportFormAndRequestFormCountByFields(List<string> fields, List<string> values,  int RangeDay,  out int ReportFormCount, out int RequestFormCount, out string ErrorInfo)
        {
            ReportFormCount = 0;
            RequestFormCount = 0;
            ErrorInfo = "";
            
            try
            {
                if (fields == null || fields.Count <= 0)
                {
                    ErrorInfo = "fields为空!";
                    return false;
                }
                if (values == null || values.Count <= 0)
                {
                    ErrorInfo = "values为空!";
                    return false;
                }
                
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormAndRequestFormCountByFields.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() +  ", RangeDay:" + RangeDay );

                if (RangeDay <= 0)
                {
                    RangeDay = 31;
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormAndRequestFormCountByFields.未传入RangeDay，得到默认值：" + RangeDay);
                }
                string urlWhere = " ";
                for (int i = 0; i < fields.Count; i++)
                {
                    urlWhere += fields[i] + "='" + values[i] + "' and ";
                }
                urlWhere = urlWhere.Substring(0, urlWhere.Length - 4);
                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);
                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "' )";
                ReportFormCount=barf.GetCountFormFull(urlWhere);
                RequestFormCount=bqf.GetCountFormFull(urlWhere);
                ZhiFang.Common.Log.Log.Debug("ReportFormCount:+"+ ReportFormCount + ";RequestFormCount:"+ RequestFormCount);
                return true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormAndRequestFormCountByFields.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString()  + ", RangeDay:" + RangeDay );
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportFormAndRequestFormCountByFields:" + e.ToString());
                ErrorInfo = " 出现异常请查看日志";
                return false;
            }
        }


        [WebMethod(Description = "解析传入的xml信息生成PDF服务:根据传入的xml生成PDF报告，并存储到程序ReportFormFiles文件夹，返回报告存储路径")]
        public string GenerateReportPDFAndSaveToReportFormFiles(string batchXMLReportForm)
        {

            ZhiFang.Common.Log.Log.Debug("解析xml存储报告单PDF开始.GenerateReportPDFAndSaveToReportFormFiles");
            Dictionary<string, string> dictionarys = new Dictionary<string, string>();
            try
            {
                if (batchXMLReportForm == null || batchXMLReportForm == "")
                {
                    ZhiFang.Common.Log.Log.Debug("batchXMLReportForm!");
                    return "batchXMLReportForm为空!";
                }
                //解析XML字符串开始
                var doc = new XmlDocument();
                doc.LoadXml(batchXMLReportForm);
                var rowNoteList = doc.SelectNodes("ReportForm");
                if (rowNoteList != null)
                {
                    foreach (XmlNode rowNode in rowNoteList)
                    {
                        var fieldNodeList = rowNode.ChildNodes;
                        foreach (XmlNode courseNode in fieldNodeList)
                        {
                            dictionarys.Add(courseNode.Name.ToString(), courseNode.InnerText.Trim());
                        }
                    }
                }
                //解析XML字符串结束
                string receivedate = dictionarys["receivedate"];//核收时间
                string sectionno = dictionarys["sectionno"];//小组编号
                string testtypeno = dictionarys["testtypeno"];//检验类型编号
                string sampleno = dictionarys["sampleno"];//样本编号
                string yqCode = dictionarys["yqCode"]; //院区code
                string reportcontent = dictionarys["reportcontent"];//pdf64位字符串
                
                if (receivedate == null || receivedate == "")
                {
                    ZhiFang.Common.Log.Log.Debug("receivedate为空!");
                    return "receivedate为空!";
                }
                if (sectionno == null || sectionno == "")
                {
                    ZhiFang.Common.Log.Log.Debug("sectionno为空!");
                    return "sectionno为空!";
                }

                if (testtypeno == null || testtypeno == "")
                {
                    ZhiFang.Common.Log.Log.Debug("testtypeno为空!");
                    return "testtypeno为空!";
                }
                if (yqCode == null || yqCode == "")
                {
                    ZhiFang.Common.Log.Log.Debug("yqCodereceivedate为空!");
                    return "yqCode为空!";
                }
                if (sampleno == null || sampleno == "")
                {
                    ZhiFang.Common.Log.Log.Debug("sampleno!");
                    return "sampleno为空!";
                }
                if (reportcontent == null || reportcontent == "")
                {
                    ZhiFang.Common.Log.Log.Debug("reportcontent为空!");
                    return "reportcontent为空!";
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GenerateReportPDFAndSaveToReportFormFiles.receivedate:" + receivedate + ", sampleno:" + sampleno + " ,sectionno: " + sectionno + ", testtypeno:" + testtypeno + ", yqCode:" + yqCode);

                string urlWhere = " ReceiveDate = '" + receivedate + "' and SectionNo = '" + sectionno + "' and TesttypeNo = '" + testtypeno + "' and LabCode ='" + yqCode + "' and SampleNo ='" + sampleno + "'";
                DataSet ds = barf.GetList_FormFull("ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo,SampleTypeNo,CheckDate,CheckTime", urlWhere);
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GenerateReportPDFAndSaveToReportFormFiles.receivedate:" + receivedate + ", sampleno:" + sampleno + " ,sectionno: " + sectionno + ", testtypeno:" + testtypeno + ", yqCode:" + yqCode);
                #region 根据报告单信息存储PDF
                string finalPath = "";
                string info = "";
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    DataRow reportform = ds.Tables[0].Rows[0];
                    //ds.Tables[0].Rows[0]["ReportFormID"].ToString();
                    PrintReportFormCommon prfc = new PrintReportFormCommon();
                    string savepath = prfc.GetReportFormPath(reportform);//完整的存放PDF的文件夹路径
                    string pdfName = prfc.GetReportFormFileName_PDF(ds.Tables[0].Rows[0]);//pdf名字
                    finalPath = savepath + pdfName;
                    //检查对应位置是否已生成文件
                    if (File.Exists(finalPath))
                    {
                        info = finalPath + "：文件已存在，不需重新生成";
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ":" + info);
                        return info;
                    }
                    byte[] imagebytes = Convert.FromBase64String(reportcontent);//传入的参数转byte数组
                    //将base64String生成pdf并存储到相应位置
                    bool isSuccess = ZhiFang.ReportFormQueryPrint.Common.FilesHelper.CreatDirFile(savepath, pdfName, imagebytes);
                    if (isSuccess)
                    {
                        info = "存储PDF成功，存储位置：" + finalPath;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "." + info);
                    }
                    else
                    {
                        info = "生成存储PDF过程失败，存储位置：" + finalPath;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "." + info);
                    }

                }
                else
                {
                    info = "未查询到报告，请查看传入条件是否正确";
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + "." + info);
                }
                #endregion
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".解析xml存储报告单PDF结束.GenerateReportPDFAndSaveToReportFormFiles");
                return info;

            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GenerateReportPDFAndSaveToReportFormFiles:" + e.ToString());
                return "异常信息：" + this.GetType().FullName + ".GenerateReportPDFAndSaveToReportFormFiles:" + e.ToString();
            }
        }

        [WebMethod(Description = "邢台人民医院自助机服务：根据病历号生成PDF报告并返回报告URL,不要有危急值项目的报告,限制打印次数")]
        public bool GetNoCriticalReportFormPDFByPatNo(string PatNo, int RangeDay, int flag, out string[] ReportFormPdfPath, out string ErrorInfo, out string info)
        {
            ReportFormPdfPath = null;
            ErrorInfo = "";
            info = "";
            try
            {
                if (PatNo == null || PatNo.Trim().Length <= 0)
                {
                    ErrorInfo = "PatNo为空!";
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetNoCriticalReportFormPDFByPatNo.PatNo:" + PatNo + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetNoCriticalReportFormPDFByPatNo.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetNoCriticalReportFormPDFByPatNo.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetNoCriticalReportFormPDFByPatNo.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " PatNo='" + PatNo + "' ";
                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);
                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "' and ISNULL(ClientPrint,0) <1)";
                DataSet ds = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo,SectionType,PrintTimes", urlWhere);
                DataSet ds1 = new DataSet();
                //如果有危急值报告则过滤掉，返回新的dataset
                bool isHaveNoCrisisreport = getReportFormNotHaveCrisisItem(ds, out info, out ds1);
                //
                if (ds1.Tables[0].Rows.Count <= 0 && (ds != null && ds.Tables[0].Rows.Count > 0))
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetNoCriticalReportFormPDFByPatNo.报告单均含有危急值项目");
                    return true;
                }
                else
                {
                    ds = ds1;
                }
                bool isPdf = GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);

                if (isPdf)
                {
                    List<string> reportformidlist = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        reportformidlist.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                    }
                    #region 打印次数累计
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
                    if (ConfigHelper.GetConfigString("IsMEGroupSampleFormAddPrintTime").Equals("2"))
                    {//打印微生物时向框架微生物添加打印次数----IsMEGroupSampleFormAddPrintTime=2针对邢台市人民医院
                        ZhiFang.Common.Log.Log.Debug("增加框架微生物打印次数.开始");
                        DataTable dsreportform = new DataTable();
                        dsreportform = brf.GetReportFormList(reportformidlist.ToArray());
                        if (dsreportform == null || dsreportform.Rows.Count <= 0)
                        {
                            ErrorInfo = "LIS中没有找到报告单";
                            return false;
                        }
                        for (int i = 0; i < dsreportform.Rows.Count; i++)
                        {
                            if (dsreportform.Rows[i]["SectionType"].ToString() == "2" || dsreportform.Rows[i]["SectionType"].ToString() == "4")
                            {
                                if (dsreportform.Rows[i]["GroupSampleFormID"].ToString() != null && dsreportform.Rows[i]["GroupSampleFormID"].ToString() != "")
                                {
                                    DAL.MSSQL.MSSQL66.ME_GroupSampleForm ME_GroupSampleForm = new DAL.MSSQL.MSSQL66.ME_GroupSampleForm();
                                    int isok = ME_GroupSampleForm.UpDateMEPrintCount(dsreportform.Rows[i]["GroupSampleFormID"].ToString(), dsreportform.Rows[i]["ReceiveDate"].ToString(), null);
                                    if (isok > 0)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("成功增加框架微生物打印次数GroupSampleFormID:" + dsreportform.Rows[i]["GroupSampleFormID"].ToString());
                                    }
                                }
                            }

                        }
                        ZhiFang.Common.Log.Log.Debug("增加框架微生物打印次数.结束");
                    }
                    //获得的客户端ip
                    string ip = HttpContext.Current.Request.UserHostAddress;
                    ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加外围打印次数:reportformID:" + PatNo);
                    brf.UpdatePrintTimes(reportformidlist.ToArray(), "");//报告库打标记
                    brf.UpdateClientPrintTimes(reportformidlist.ToArray());//外围打印标记
                    #endregion
                }
                return isPdf;
                //return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetNoCriticalReportFormPDFByPatNo.异常:" + e.ToString() + ".PatNo:" + PatNo + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }
        [WebMethod(Description = "邢台人民医院服务：根据病历号生成PDF报告并返回报告URL,不要有危急值项目的报告,不限制打印次数")]
        public bool GetNoCriticalReportFormPDFByPatNo_NoClientPrint(string PatNo, int RangeDay, int flag, int IsPrintTimes,out string[] ReportFormPdfPath, out string ErrorInfo, out string info)
        {
            ReportFormPdfPath = null;
            ErrorInfo = "";
            info = "";
            try
            {
                if (PatNo == null || PatNo.Trim().Length <= 0)
                {
                    ErrorInfo = "PatNo为空!";
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetNoCriticalReportFormPDFByPatNo_NoClientPrint.PatNo:" + PatNo + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetNoCriticalReportFormPDFByPatNo_NoClientPrint.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetNoCriticalReportFormPDFByPatNo_NoClientPrint.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetNoCriticalReportFormPDFByPatNo_NoClientPrint.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " PatNo='" + PatNo + "' ";
                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);
                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "')";
                DataSet ds = barf.GetList_FormFull("ReportFormID,SecretType,Receivedate,SectionNo,TestTypeNo,SampleNo,SectionType,PrintTimes", urlWhere);
                DataSet ds1 = new DataSet();
                //如果有危急值报告则过滤掉，返回新的dataset
                bool isHaveNoCrisisreport = getReportFormNotHaveCrisisItem(ds, out info, out ds1);
                //
                if (ds1.Tables[0].Rows.Count <= 0 && (ds != null && ds.Tables[0].Rows.Count > 0))
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetNoCriticalReportFormPDFByPatNo_NoClientPrint.报告单均含有危急值项目");
                    return true;
                }
                else
                {
                    ds = ds1;
                }
                bool isPdf = GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);

                if (isPdf)
                {
                    List<string> reportformidlist = new List<string>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        reportformidlist.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                    }
                    #region 打印次数累计
                    if(IsPrintTimes==1){//等于1记录打印次数
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
                        if (ConfigHelper.GetConfigString("IsMEGroupSampleFormAddPrintTime").Equals("2"))
                        {//打印微生物时向框架微生物添加打印次数----IsMEGroupSampleFormAddPrintTime=2针对邢台市人民医院
                            ZhiFang.Common.Log.Log.Debug("增加框架微生物打印次数.开始");
                            DataTable dsreportform = new DataTable();
                            dsreportform = brf.GetReportFormList(reportformidlist.ToArray());
                            if (dsreportform == null || dsreportform.Rows.Count <= 0)
                            {
                                ErrorInfo = "LIS中没有找到报告单";
                                return false;
                            }
                            for (int i = 0; i < dsreportform.Rows.Count; i++)
                            {
                                if (dsreportform.Rows[i]["SectionType"].ToString() == "2" || dsreportform.Rows[i]["SectionType"].ToString() == "4")
                                {
                                    if (dsreportform.Rows[i]["GroupSampleFormID"].ToString() != null && dsreportform.Rows[i]["GroupSampleFormID"].ToString() != "")
                                    {
                                        DAL.MSSQL.MSSQL66.ME_GroupSampleForm ME_GroupSampleForm = new DAL.MSSQL.MSSQL66.ME_GroupSampleForm();
                                        int isok = ME_GroupSampleForm.UpDateMEPrintCount(dsreportform.Rows[i]["GroupSampleFormID"].ToString(), dsreportform.Rows[i]["ReceiveDate"].ToString(), null);
                                        if (isok > 0)
                                        {
                                            ZhiFang.Common.Log.Log.Debug("成功增加框架微生物打印次数GroupSampleFormID:" + dsreportform.Rows[i]["GroupSampleFormID"].ToString());
                                        }
                                    }
                                }

                            }
                            ZhiFang.Common.Log.Log.Debug("增加框架微生物打印次数.结束");
                        }
                        //获得的客户端ip
                        string ip = HttpContext.Current.Request.UserHostAddress;
                        ZhiFang.Common.Log.Log.Debug("IP:" + ip + "添加外围打印次数:reportformID:" + PatNo);
                        brf.UpdatePrintTimes(reportformidlist.ToArray(), "");//报告库打标记
                        brf.UpdateClientPrintTimes(reportformidlist.ToArray());//外围打印标记
                    }
                    
                    #endregion
                }
                return isPdf;
                //return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetNoCriticalReportFormPDFByPatNo_NoClientPrint.异常:" + e.ToString() + ".PatNo:" + PatNo + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }
        [WebMethod(Description = "邢台人民医院自助机服务：根据病例号获取检验完成和检验中报告的状态信息")]
        public bool GetAllReportStatusByPatNo(string PatNo, int RangeDay, out string ErrorInfo, out List<string> ReprotFormStatusInfo, out List<string> RequestFormStatusInfo)
        {
            bool isSuccess = false;
            ErrorInfo = "";
            ReprotFormStatusInfo = new List<string>();
            RequestFormStatusInfo = new List<string>();
            try
            {
                if (PatNo == null || PatNo.Trim().Length <= 0)
                {
                    ErrorInfo = "PatNo为空!";
                    return false;
                }
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetAllReportStatusByPatNo.PatNo:" + PatNo + ", RangeDay:" + RangeDay);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetAllReportStatusByPatNo.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetAllReportStatusByPatNo.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetAllReportStatusByPatNo.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);
                string urlWhere = " PatNo='" + PatNo + "' and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "')";
                DataSet ReportForm = barf.GetList_FormFull("", urlWhere);
                DataSet RequestForm = bqf.GetList_FormFull("", urlWhere);
                //拼接检验完成报告xml
                if (ReportForm != null && ReportForm.Tables[0].Rows.Count > 0)
                {
                    DataRowCollection reportDataRows = ReportForm.Tables[0].Rows;
                    for (int i = 0; i < reportDataRows.Count; i++)
                    {
                        string reportFormXml = "";
                        string CName = "";//病人姓名
                        string ItemName = "";//项目名称
                        string PrintDateTime = "";//最后一次打印时间
                        string PrintTimes = "";//打印次数，框架微生物表页面和自助用的一个字段IQSPrintCount as PrintTimes
                        string PrintStatus = "";//打印状态，已打印，未打印
                        string clientprint = "";//reportform表自助打印次数
                        string TestDateTime = "";//检测时间
                        if (reportDataRows[i]["CName"] != null)
                        {
                            CName = reportDataRows[i]["CName"].ToString();
                        }
                        if (reportDataRows[i]["ItemName"] != null)
                        {
                            ItemName = reportDataRows[i]["ItemName"].ToString();
                        }

                        //视图里将微生物框架表ME_GroupSampleForm的IQSPrintCount as PrintTimes
                        if (reportDataRows[i]["GroupSampleFormID"] != null && reportDataRows[i]["GroupSampleFormID"].ToString() != "")
                        {
                            ZhiFang.Common.Log.Log.Debug("GetAllReportStatusByPatNo.GroupSampleFormID:" + reportDataRows[i]["GroupSampleFormID"].ToString());
                            if (reportDataRows[i]["PrintTimes"] != null && reportDataRows[i]["PrintTimes"].ToString() != "")
                            {
                                PrintTimes = reportDataRows[i]["PrintTimes"].ToString();
                                if (PrintTimes == "0")
                                {
                                    PrintStatus = "未打印";
                                    //未打印则需要检测时间
                                    TestDateTime = reportDataRows[i]["TestDate"].ToString();
                                }
                                else
                                {
                                    PrintStatus = "已打印";
                                    //已打印需要打印时间
                                    PrintDateTime = reportDataRows[i]["PrintDateTime"].ToString();

                                }
                            }
                            else
                            {
                                PrintStatus = "未打印";
                                //未打印则需要检测时间
                                TestDateTime = reportDataRows[i]["TestDate"].ToString();
                            }
                        }
                        else//reportform表的自助打印次数
                        {
                            ZhiFang.Common.Log.Log.Debug("GetAllReportStatusByPatNo.FormNo:" + reportDataRows[i]["FormNo"].ToString());
                            if (reportDataRows[i]["clientprint"] != null && reportDataRows[i]["clientprint"].ToString() != "")
                            {
                                clientprint = reportDataRows[i]["clientprint"].ToString();
                                if (clientprint == "0")
                                {
                                    PrintStatus = "未打印";
                                    //未打印则需要检测时间
                                    TestDateTime = reportDataRows[i]["TestTime"].ToString();
                                }
                                else
                                {
                                    PrintStatus = "已打印";
                                    //已打印需要打印时间
                                    PrintDateTime = reportDataRows[i]["PrintDateTime"].ToString();
                                }
                            }
                            else
                            {
                                PrintStatus = "未打印";
                                //未打印则需要检测时间
                                TestDateTime = reportDataRows[i]["TestTime"].ToString();
                            }
                        }

                        //拼接xml
                        reportFormXml = "<ReportForm><CName>" + CName + "</CName><ItemName>" + ItemName + "</ItemName><PrintDateTime>" + PrintDateTime + "</PrintDateTime><PrintStatus>" + PrintStatus + "</PrintStatus><TestDateTime>" + TestDateTime + "</TestDateTime></ReportForm>";
                        ReprotFormStatusInfo.Add(reportFormXml);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetAllReportStatusByPatNo.没找到检验完成报告。");
                }
                //拼接检验中报告xml
                if (RequestForm != null && RequestForm.Tables[0].Rows.Count > 0)
                {
                    DataRowCollection requestDataRows = RequestForm.Tables[0].Rows;

                    for (int i = 0; i < requestDataRows.Count; i++)
                    {
                        string requestFormXml = "";
                        string CName = "";//病人姓名
                        string ItemName = "";//项目名称
                        string PrintStatus = "检验中";//打印状态,检验中
                        string TestDateTime = "";//检测时间
                        if (requestDataRows[i]["CName"] != null)
                        {
                            CName = requestDataRows[i]["CName"].ToString();
                        }
                        if (requestDataRows[i]["ItemName"] != null)
                        {
                            ItemName = requestDataRows[i]["ItemName"].ToString();
                        }
                        if (requestDataRows[i]["TestTime"] != null)
                        {
                            TestDateTime = requestDataRows[i]["TestTime"].ToString();
                        }

                        //拼接xml
                        requestFormXml = "<RequestForm><CName>" + CName + "</CName><ItemName>" + ItemName + "</ItemName><PrintStatus>" + PrintStatus + "</PrintStatus><TestDateTime>" + TestDateTime + "</TestDateTime></RequestForm>";
                        RequestFormStatusInfo.Add(requestFormXml);
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetAllReportStatusByPatNo.没找到检验中报告。");
                }
                isSuccess = true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".getAllReportStatusByPatNo.异常:" + e.ToString() + ".PatNo:" + PatNo + ", RangeDay:" + RangeDay);
                ErrorInfo = "出现异常请查看日志";
            }
            return isSuccess;
        }
        //获取没有危急值项目的报告单
        public static bool getReportFormNotHaveCrisisItem(DataSet ds, out string message, out DataSet newDataSet)
        {
            int sectiontype = 1;
            bool isHaveReport = false;
            newDataSet = new DataSet();
            int criticalValueReportCount = 0;//含有危急值项目的报告单数量
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                DataTable dataTable = ds.Tables[0].Clone();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string reportformid = ds.Tables[0].Rows[i]["ReportFormID"].ToString();
                    #region 预处理
                    if (!String.IsNullOrWhiteSpace(ds.Tables[0].Rows[i]["SECTIONTYPE"].ToString()))
                    {
                        sectiontype = Convert.ToInt32(ds.Tables[0].Rows[i]["SECTIONTYPE"].ToString());
                    }
                    #endregion
                    #region 数据准备
                    DataTable dtri = new DataTable();
                    string where = "(resultstatus ='LL' or resultstatus ='HH')";
                    int count = 0;
                    BReportItem rifb = new BReportItem();
                    //目前只有sectiontype是1和3的
                    switch ((SectionType)Convert.ToInt32(sectiontype))
                    {
                        case SectionType.all:
                            #region Normal
                            count = rifb.GetReportItemListWhereCount(reportformid, where);
                            break;
                        #endregion
                        case SectionType.Normal:
                            #region Normal
                            count = rifb.GetReportItemListWhereCount(reportformid, where);
                            break;
                        #endregion
                        case SectionType.NormalIncImage:
                            #region NormalIncImage
                            count = rifb.GetReportItemListWhereCount(reportformid, where);
                            break;
                            #endregion
                    }
                    //if (count > 0 && (ds.Tables[0].Rows[i]["PrintTimes"] ==null || ds.Tables[0].Rows[i]["PrintTimes"].ToString()=="" || ds.Tables[0].Rows[i]["PrintTimes"].ToString()=="0"))
                    //{
                    //    //危急值报告计数
                    //    ZhiFang.Common.Log.Log.Debug("ReportFormWebService.getReportFormResultIsNotCrisis.含有危急值报告ReportFormID:" + reportformid);
                    //    criticalValueReportCount++;
                    //}
                    //else
                    //{
                    //    //不含危急值报告返回
                    //    dataTable.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                    //}
                    if (count <= 0)
                    {
                        //不含危急值报告返回
                        dataTable.Rows.Add(ds.Tables[0].Rows[i].ItemArray);
                    }
                    if (count > 0 && (ds.Tables[0].Rows[i]["PrintTimes"] == null || ds.Tables[0].Rows[i]["PrintTimes"].ToString() == "" || ds.Tables[0].Rows[i]["PrintTimes"].ToString() == "0"))
                    {
                        //危急值报告计数
                        ZhiFang.Common.Log.Log.Debug("ReportFormWebService.getReportFormResultIsNotCrisis.含有危急值报告ReportFormID:" + reportformid);
                        criticalValueReportCount++;
                    }
                    #endregion
                }
                newDataSet.Tables.Add(dataTable);
                isHaveReport = true;
            }
            else
            {
                newDataSet.Tables.Add(new DataTable());
            }
            ZhiFang.Common.Log.Log.Debug("ReportFormWebService.getReportFormResultIsNotCrisis.共有" + criticalValueReportCount + "份未打印危急值报告");
            message = "共有" + criticalValueReportCount + "份未打印危急值报告。";
            if (criticalValueReportCount > 0)
            {
                message += "请联系检验科查看！南院区电话:0319-3286105；北院区电话:0319-3956711或3956970";
            }
            return isHaveReport;
        }

        [WebMethod(Description = "返回报告ReportFormId和Base64字符串")]
        public string GetReportFormIdAndBase64(List<string> fields, List<string> values, List<string> order, int RangeDay,  int flag)
        {

            
            
            string ErrorInfo = "";
            try
            {
                if (fields == null || fields.Count <= 0 || fields[0].Length <= 2)
                {
                    
                    return "fields为空!";
                }
                if (values == null || values.Count <= 0 || values[0].Length <= 2)
                {
                    
                    return "values为空!";
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
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportInfoAndBase64.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportInfoAndBase64.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportInfoAndBase64.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportInfoAndBase64.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
                    }
                }
                string urlWhere = " ";
                var itemDirectory = System.AppDomain.CurrentDomain.BaseDirectory;//获取项目物理路径
                for (int i = 0; i < fields.Count; i++)
                {
                    urlWhere += fields[i] + "='" + values[i] + "' and ";
                }
                urlWhere = urlWhere.Substring(0, urlWhere.Length - 4);

                DateTime enddate = DateTime.Today;
                DateTime stdate = enddate.AddDays((-1) * RangeDay);

                
                urlWhere += " and (ReceiveDate >='" + stdate + "' and ReceiveDate <='" + enddate + "' )";
                DataSet ds = barf.GetList_FormFull("ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo", urlWhere + urlOrder);
                
                Dictionary<string, string> reportFormDictionary = new Dictionary<string, string>();
                List<string> reportformidlist = new List<string>();
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    reportformidlist.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString().Trim());
                }
                var listreportformfile = bprf.CreatReportFormFiles(reportformidlist, ReportFormTitle.center, ReportFormFileType.PDF, "1", flag);
                if (listreportformfile.Count > 0)
                {

                    
                    for (int i = 0; i < listreportformfile.Count; i++)
                    {
                        try
                        {
                            
                            byte[] byteArray = ByteHelper.File2Bytes(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/"));//文件转成byte二进制数组
                            string JarContent = Convert.ToBase64String(byteArray);//将二进制转成string类型

                            reportFormDictionary.Add(listreportformfile[i].ReportFormID, JarContent) ;
                        }
                        catch (Exception ex)
                        {
                            ErrorInfo = ex.Message;
                        }

                        if (File.Exists(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/")))
                        {
                            File.Delete(itemDirectory + listreportformfile[i].PDFPath.Replace(@"\", "/"));
                        }
                    }
                    
                    ErrorInfo = "";
                    
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("GetReportFormPDFBitsByDS.未查找到报告单");
                    ErrorInfo = "未查找到报告单";
                    
                    return ErrorInfo;
                }
                return ZhiFang.ReportFormQueryPrint.Common.JsonSerializer.JsonDotNetSerializerNoEnterSpace(reportFormDictionary); ;
                
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportInfoAndBase64.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetReportInfoAndBase64:" + e.ToString());
                ErrorInfo = "出现异常请查看日志";
                return "";
            }

        }

        [WebMethod(Description = "根据传入字段和值生成PDF报告并合成并返回报告URL，不限制打印次数")]
        public bool GetBlendReportFormPDFByFields_NoClientPrint(List<string> fields, List<string> values, List<string> order, int RangeDay, int IsPrintTimes, int flag, out string[] ReportFormPdfPath, out string ErrorInfo)
        {
            ReportFormPdfPath = null;
            ErrorInfo = "";
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
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetBlendReportFormPDFByFields_NoClientPrint.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);

                if (RangeDay <= 0)
                {
                    ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetBlendReportFormPDFByFields_NoClientPrint.未传入RangeDay");
                    if (ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").HasValue)
                    {
                        RangeDay = Math.Abs(ZhiFang.ReportFormQueryPrint.Common.ConfigHelper.GetConfigInt("SearchBeforeDayNum").Value);
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetBlendReportFormPDFByFields_NoClientPrint.未传入RangeDay读取配置文件参数：SearchBeforeDayNum，得到绝对值：" + RangeDay);
                    }
                    else
                    {
                        RangeDay = 31;
                        ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetBlendReportFormPDFByFields_NoClientPrint.未传入RangeDay，配置文件参数：SearchBeforeDayNum未配置，得到默认值：" + RangeDay);
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
                DataSet ds = barf.GetList_FormFull("ReportFormID,Receivedate,SectionNo,TestTypeNo,SampleNo,PageName,CheckDate", urlWhere + urlOrder);
                string tempPath = "";
                List<string> idList = new List<string>();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        tempPath += ds.Tables[0].Rows[i]["ReportFormID"] + "_";
                        idList.Add(ds.Tables[0].Rows[i]["ReportFormID"].ToString());
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("GetBlendReportFormPDFByFields_NoClientPrint.没找到报告,请检查报告日期及查询条件。");
                    ErrorInfo = "没找到报告,请检查报告日期及查询条件。";
                    ReportFormPdfPath = null;
                    return false;
                }
                var listreportformfile = bprf.CreatReportFormFiles(idList, ReportFormTitle.center, ReportFormFileType.PDF, "0", 0);
                //bool isPdf = GetReportFormPDFByFields_NoClientPrint(fields, values, order,RangeDay, IsPrintTimes, flag, out ReportFormPdfPath,out ErrorInfo);
                string path = "";
                if (listreportformfile != null && listreportformfile.Count>0)
                {
                    path = System.AppDomain.CurrentDomain.BaseDirectory + listreportformfile[0].PDFPath;
                    
                    
                    string[] fileArray = new string[] { };
                    string[] pageTypeArray = new string[] { };
                    List<string> fileList = new List<string>();
                    List<string> pageTypeList = new List<string>();
                    for (int i = 0; i < listreportformfile.Count; i++)
                    {
                        fileList.Add(System.AppDomain.CurrentDomain.BaseDirectory + listreportformfile[i].PDFPath);
                        pageTypeList.Add(ds.Tables[0].Rows[i]["PageName"].ToString());
                    }
                    fileArray = fileList.ToArray();
                    pageTypeArray = pageTypeList.ToArray();

                    string directoryName = Path.GetDirectoryName(path);
                    path = directoryName + "\\" + tempPath + ".PDF";
                    //PDFMergeHelp.Blend(fileArray, pageTypeArray, path);
                    if(PDFMergeHelp.mergePdfFiles(fileArray,path,false)){
                        DateTime checkdate = DateTime.Parse(ds.Tables[0].Rows[0]["CheckDate"].ToString().Trim());
                        string ReportFormRelativePath = SysContractPara.ReportFormFilePath + checkdate.ToString("yyyy-MM-dd");
                        string filePath= "http://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath + "/" + ReportFormRelativePath.Replace(@"\", "/")  +"/" + tempPath + ".PDF";
                        
                        ReportFormPdfPath = new string[] { filePath };
                    }else{
                        ErrorInfo = "合成失败";
                        return false;
                    }
                    
                }
                return true;
                //return GetReportFormPDFByDS(ds, flag, out ReportFormPdfPath, out ErrorInfo);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetBlendReportFormPDFByFields_NoClientPrint.fields:" + fields.ToArray().ToString() + " values:" + values.ToArray().ToString() + " order: " + order.ToArray().ToString() + ", RangeDay:" + RangeDay + ", flag:" + flag);
                ZhiFang.Common.Log.Log.Debug(this.GetType().FullName + ".GetBlendReportFormPDFByFields_NoClientPrint:" + e.ToString());
                ErrorInfo = "出现异常请查看日志";
                return false;
            }
        }

        /// <summary>
        /// 中日医院糖耐量报告定制
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="SectionType"></param>
        /// <param name="flag"></param>
        /// <param name="errorInfo"></param>
        /// <param name="listreportformfile"></param>
        /// <returns></returns>
        public  bool ZhongRiBloodGlucoseTesting(string ReportFormID, string SectionType, int flag, ref string errorInfo, ref List<ReportFormFilesVO> listreportformfile)
        {
            bool matchFlag = false;
            #region 中日定制-糖耐量
            string BloodGlucoseTestingConfiguration = ConfigHelper.GetConfigString("BloodGlucoseTestingConfiguration");
            if (!string.IsNullOrWhiteSpace(BloodGlucoseTestingConfiguration) && BloodGlucoseTestingConfiguration == "1")
            {
                BRequestForm bqf = new BLL.BRequestForm();
                DataTable reportform = brf.GetListByDataSource(ReportFormID);

                if (reportform != null && reportform.Rows.Count > 0)
                {
                    if (reportform.Rows[0]["zdy4"]==null || reportform.Rows[0]["zdy4"].ToString().Trim()=="")
                    {
                        errorInfo = "zdy4为空";
                        return false;
                    }
                    DateTime ReceiveDate = DateTime.Parse(reportform.Rows[0]["zdy4"].ToString().Trim());
                    string receiveDate = ReceiveDate.ToString("yyyy/MM/d");
                    string where = "zdy9='" + reportform.Rows[0]["ZDY9"].ToString() + "' and zdy4>='" + receiveDate + "' and zdy4<'" + receiveDate + " 23:59:59'";
                    //查NRequestForm表，对应病人当天数据
                    List<NRequestForm> NRequestFormList = bnrf.GetModelList_FormFull("", where);
                    if (NRequestFormList != null && NRequestFormList.Count > 0)
                    {
                        List<string> serialNos = new List<string>();
                        for (int i = 0; i < NRequestFormList.Count; i++)
                        {
                            serialNos.Add(NRequestFormList[i].SerialNo);
                        }
                        #region 先判断本分报告是不是特殊报告，如果不是直接不走此流程
                        List<Model.NRequestItem> NRequestItemList1 = bnri.GetModelList("SerialNo in ('" + string.Join("','", serialNos) + "') and Paritemno in (101010,101111,101112,101113,101114,101115)");//糖耐量
                        List<Model.NRequestItem> NRequestItemList2 = bnri.GetModelList("SerialNo in ('" + string.Join("','", serialNos) + "') and Paritemno in (701036,701043,701039,701041,701045,701110,701035,701037)");//胰岛素
                        List<Model.NRequestItem> NRequestItemList3 = bnri.GetModelList("SerialNo in ('" + string.Join("','", serialNos) + "') and Paritemno in (406117,406129,406130,406131,701100)");//C肽
                        List<Model.NRequestItem> NRequestItem1 = NRequestItemList1.Where(a => (a.SerialNo.ToString() == reportform.Rows[0]["SerialNo"].ToString())).ToList();
                        List<Model.NRequestItem> NRequestItem2 = NRequestItemList2.Where(a => (a.SerialNo.ToString() == reportform.Rows[0]["SerialNo"].ToString())).ToList();
                        List<Model.NRequestItem> NRequestItem3 = NRequestItemList3.Where(a => (a.SerialNo.ToString() == reportform.Rows[0]["SerialNo"].ToString())).ToList();
                        bool isSpecial = false;
                        //对应NRequestItem表,特殊项目号，并且两项以上
                        if (NRequestItemList1 != null && NRequestItemList1.Count >= 2 && NRequestItem1.Count > 0)
                        {
                            errorInfo = "各时间点血糖报告尚未全部完成，请稍后再试，全部完成后系统将把各时点血糖合并打印。";
                            isSpecial = true;
                        }
                        else if (NRequestItemList2 != null && NRequestItemList2.Count >= 2 && NRequestItem2.Count > 0)
                        {
                            NRequestItemList1 = NRequestItemList2;
                            errorInfo = "各时间点胰岛素报告尚未全部完成，请稍后再试，全部完成后系统将把各时点胰岛素合并打印。";
                            isSpecial = true;
                            ZhiFang.Common.Log.Log.Debug("ReportFormWebService.ZhongRiBloodGlucoseTesting.胰岛素检测");
                        }
                        else if (NRequestItemList3 != null && NRequestItemList3.Count >= 2 && NRequestItem3.Count > 0)
                        {
                            NRequestItemList1 = NRequestItemList3;
                            errorInfo = "各时间点C肽报告尚未全部完成，请稍后再试，全部完成后系统将把各时点C肽合并打印。";
                            isSpecial = true;
                            ZhiFang.Common.Log.Log.Debug("ReportFormWebService.ZhongRiBloodGlucoseTesting.C肽检测");
                        }
                        else
                        {
                            return false;
                        }
                        #endregion

                        if (isSpecial)
                        {
                            ZhiFang.Common.Log.Log.Debug("ReportFormServiceInterFace.ZhongRiBloodGlucoseTesting.特殊项目2项及以上");
                            //项目可能开重复，重复的项目如果有一个对应的检验单签收或核收，那么其他项目则作废
                            var itemGroups = NRequestItemList1.GroupBy(a => a.ParItemNo);//按项目号分组
                            List<string> itemSerialNoList = new List<string>();//核收的项目申请单号集合
                            foreach (var group in itemGroups)
                            {
                                foreach (var item in group)
                                {
                                    if (item.RECEIVEFLAG == 1)
                                    {
                                        itemSerialNoList.Add(item.SerialNo);
                                    }
                                }
                            }
                            //判断所有特殊项目是否全部核收
                            if (itemSerialNoList.Count != itemGroups.Count())
                            {
                                return true;
                            }
                            //特殊项目全部核收再查看报告单是否有在检验中
                            ZhiFang.Common.Log.Log.Debug("ZhongRiBloodGlucoseTesting.特殊项目全部核收");
                            int requestFormCount = bqf.GetCountFormFull("zdy9='" + reportform.Rows[0]["ZDY9"].ToString() + "' and SerialNo in ('" + string.Join("','", itemSerialNoList) + "')");
                            if (requestFormCount > 0)
                            {
                                ZhiFang.Common.Log.Log.Debug("ReportFormWebService.ZhongRiBloodGlucoseTesting.报告单有" + requestFormCount + "份正在检验中");
                                return true;
                            }
                            //均完成后，判断是否全部二审
                            int finalSendCount = barf.GetCountFormFull("zdy9='" + reportform.Rows[0]["ZDY9"].ToString() + "' and SerialNo in ('" + string.Join("','", itemSerialNoList) + "')");
                            if (finalSendCount != itemSerialNoList.Count)
                            {
                                ZhiFang.Common.Log.Log.Debug("ReportFormWebService.ZhongRiBloodGlucoseTesting.报告单没有全部二审");
                                return true;
                            }
                            //所有报告检验完成，开始合成
                            errorInfo = "";
                            string SerialNos = string.Join(",", itemSerialNoList);
                            ZhiFang.Common.Log.Log.Debug("ReportFormWebService.ZhongRiBloodGlucoseTesting.所有报告检验完成，开始合成：" + SerialNos);
                            //如果都检验完成则合并所有item到一张pdf里
                            listreportformfile = bprf.CreatReportFormFilesGlucoseToleranceZhongRi(new List<string>() { ReportFormID }, ReportFormTitle.center, ReportFormFileType.PDF, SectionType, SerialNos, flag);
                            return true;
                        }
                    }
                }
            }
            #endregion
            return matchFlag;
        }

    }
}
 