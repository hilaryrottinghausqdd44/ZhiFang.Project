using Common;
using FastReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.BLL.Common;
using ZhiFang.BLL.Report;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.IBLL.Report;
using ZhiFang.Model;
using ZhiFang.Model.VO;

namespace ZhiFang.WebLis.ServiceWCF
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class StatisticsService
    {
        #region 报告统计
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ReportFormGroupByClientNoAndReportDate?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue ReportFormGroupByClientNoAndReportDate(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.ReportFormGroupByClientNoAndReportDate(StartDateTime, EndDateTime, ClientNoList, DateType);
                brdv.ResultDataValue = DataSetToJson.ToJson(resultdt);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.ReportFormGroupByClientNoAndReportDate.异常：" + e.ToString());
                return brdv;
            }
        }


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ReportFormGroupByClientNoAndReportDate_Pdf?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue ReportFormGroupByClientNoAndReportDate_Pdf(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DateTime StartDateTimeT;
                DateTime EndDateTimeT;
                if (!DateTime.TryParse(StartDateTime, out StartDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：开始时间参数为空！";
                    return brdv;
                }
                if (!DateTime.TryParse(EndDateTime, out EndDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：结束时间参数为空！";
                    return brdv;
                }
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.ReportFormGroupByClientNoAndReportDate(StartDateTime, EndDateTime, ClientNoList, DateType);
                FastReport.Report report = new FastReport.Report();

                string url = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd") + ".pdf";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\ReportFormGroupByClientNoAndReportDate\\" + url;
                ZhiFang.Common.Log.Log.Debug("ReportFormGroupByClientNoAndReportDate_Pdf.Url:" + strPath);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\ReportFormGroupByClientNoAndReportDate\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\ReportFormGroupByClientNoAndReportDate\\");
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\ReportFormGroupByClientNoAndReportDate.frx");
                TextObject Text_DateRange = (TextObject)report.FindObject("Text_DateRange");
                if (Text_DateRange != null)
                {
                    Text_DateRange.Text = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd");
                }
                resultdt.TableName = "Table";
                report.RegisterData(resultdt.DataSet);
                report.Prepare();
                FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
                report.Export(export, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/ReportFormGroupByClientNoAndReportDate/" + url;
                brdv.ResultDataValue = strTable;
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.ReportFormGroupByClientNoAndReportDate_pdf.异常：" + e.ToString());
                return brdv;
            }
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ReportFormGroupByClientNoAndReportDate_Excel?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue ReportFormGroupByClientNoAndReportDate_Excel(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DateTime StartDateTimeT;
                DateTime EndDateTimeT;
                if (!DateTime.TryParse(StartDateTime, out StartDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：开始时间参数为空！";
                    return brdv;
                }
                if (!DateTime.TryParse(EndDateTime, out EndDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：结束时间参数为空！";
                    return brdv;
                }
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.ReportFormGroupByClientNoAndReportDate(StartDateTime, EndDateTime, ClientNoList, DateType);
                FastReport.Report report = new FastReport.Report();

                string url = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd") + ".xlsx";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\ReportFormGroupByClientNoAndReportDate\\" + url;
                ZhiFang.Common.Log.Log.Debug("ReportFormGroupByClientNoAndReportDate_Excel.Url:" + strPath);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\ReportFormGroupByClientNoAndReportDate\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\ReportFormGroupByClientNoAndReportDate\\");
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\ReportFormGroupByClientNoAndReportDate.frx");
                TextObject Text_DateRange = (TextObject)report.FindObject("Text_DateRange");
                if (Text_DateRange != null)
                {
                    Text_DateRange.Text = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd");
                }
                resultdt.TableName = "Table";
                report.RegisterData(resultdt.DataSet);
                report.Prepare();
                FastReport.Export.OoXML.Excel2007Export export = new FastReport.Export.OoXML.Excel2007Export();
                report.Export(export, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/ReportFormGroupByClientNoAndReportDate/" + url;
                brdv.ResultDataValue = strTable;
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.ReportFormGroupByClientNoAndReportDate_Excel.异常：" + e.ToString());
                return brdv;
            }
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ReportFormGroupByClientNo?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue ReportFormGroupByClientNo(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.ReportFormGroupByClientNo(StartDateTime, EndDateTime, ClientNoList, DateType);
                brdv.ResultDataValue = DataSetToJson.ToJson(resultdt);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.ReportFormGroupByClientNoAndReportDate.异常：" + e.ToString());
                return brdv;
            }
        }
        #endregion
        #region 申请单统计
        #region 开单项目汇总报表中心
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsRequestItemCenter?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsRequestItemCenter(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsRequestItemCenter(StartDateTime, EndDateTime, ClientNoList);
                brdv.ResultDataValue = DataSetToJson.ToJson(resultdt);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsRequestItemCenter.异常：" + e.ToString());
                return brdv;
            }
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsRequestItemCenter_Pdf?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsRequestItemCenter_Pdf(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DateTime StartDateTimeT;
                DateTime EndDateTimeT;
                if (!DateTime.TryParse(StartDateTime, out StartDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：开始时间参数为空！";
                    return brdv;
                }
                if (!DateTime.TryParse(EndDateTime, out EndDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：结束时间参数为空！";
                    return brdv;
                }
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsRequestItemCenter(StartDateTime, EndDateTime, ClientNoList);
                FastReport.Report report = new FastReport.Report();

                string url = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd") + ".pdf";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestItemCenter\\" + url;
                ZhiFang.Common.Log.Log.Debug("StatisticsRequestItemCenter_Pdf.Url:" + strPath);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestItemCenter\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestItemCenter\\");
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\StatisticsRequestItemCenter.frx");
                TextObject Text_DateRange = (TextObject)report.FindObject("Text_DateRange");
                if (Text_DateRange != null)
                {
                    Text_DateRange.Text = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd");
                }
                resultdt.TableName = "Table";
                report.RegisterData(resultdt.DataSet);
                report.Prepare();
                FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
                report.Export(export, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/StatisticsRequestItemCenter/" + url;
                brdv.ResultDataValue = strTable;
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsRequestItemCenter_pdf.异常：" + e.ToString());
                return brdv;
            }
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsRequestItemCenter_Excel?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsRequestItemCenter_Excel(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DateTime StartDateTimeT;
                DateTime EndDateTimeT;
                if (!DateTime.TryParse(StartDateTime, out StartDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：开始时间参数为空！";
                    return brdv;
                }
                if (!DateTime.TryParse(EndDateTime, out EndDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：结束时间参数为空！";
                    return brdv;
                }
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsRequestItemCenter(StartDateTime, EndDateTime, ClientNoList);
                FastReport.Report report = new FastReport.Report();

                string url = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd") + ".xlsx";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestItemCenter\\" + url;
                ZhiFang.Common.Log.Log.Debug("StatisticsRequestItemCenter_Excel.Url:" + strPath);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestItemCenter\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestItemCenter\\");
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\StatisticsRequestItemCenter.frx");
                TextObject Text_DateRange = (TextObject)report.FindObject("Text_DateRange");
                if (Text_DateRange != null)
                {
                    Text_DateRange.Text = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd");
                }
                resultdt.TableName = "Table";
                report.RegisterData(resultdt.DataSet);
                report.Prepare();
                FastReport.Export.OoXML.Excel2007Export export = new FastReport.Export.OoXML.Excel2007Export();
                report.Export(export, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/StatisticsRequestItemCenter/" + url;
                brdv.ResultDataValue = strTable;
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsRequestItemCenter_Excel.异常：" + e.ToString());
                return brdv;
            }
        }
        #endregion
        #region 开单项目汇总报表客户
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsRequestItemClient?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsRequestItemClient(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    brdv.ErrorInfo = "未登录，请登陆后继续！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestItemClient." + brdv.ErrorInfo);
                    return null;
                }
                else
                {
                    string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                    user = new ZhiFang.WebLis.Class.User(UserId);
                }
                IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
                EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = user.Account.Trim(), SelectedFlag = true, Flag = 0 }, 1, 10000, "ClIENTNO", " 1=1 ", " ClIENTNO ");
                if (cl == null || cl.list == null || cl.list.Count <= 0)
                {
                    brdv.ErrorInfo = "当前用户未配置所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestItemClient." + brdv.ErrorInfo);
                }
                List<string> cnolist = new List<string>();
                if (ClientNoList != null && ClientNoList.Trim() != "")
                {
                    var tmp = ClientNoList.Split(',');
                    foreach (var cno in tmp)
                    {
                        if (cl.list.Count(a => a.ClIENTNO == cno) > 0)
                        {
                            cnolist.Add(cno);
                        }
                    }
                }
                else
                {
                    foreach (var cno in cl.list)
                    {
                        cnolist.Add(cno.ClIENTNO);
                    }
                }
                if (cnolist.Count <= 0)
                {
                    brdv.ErrorInfo = "当前所选单位未配置成当前用户所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestItemClient." + brdv.ErrorInfo);
                }

                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsRequestItemClient(StartDateTime, EndDateTime, string.Join(",", cnolist));

                if (resultdt != null && resultdt.Rows.Count > 0)
                {
                    string tmpclientno = "";
                    int tmpcount = 0;
                    //decimal tmpprice = 0;
                    decimal tmpcountprice = 0;
                    Dictionary<int, DataRow> tmpdrlist = new Dictionary<int, DataRow>();
                    for (int i = 0; i < resultdt.Rows.Count; i++)
                    {
                        if (tmpclientno == resultdt.Rows[i]["WeblisSourceOrgId"].ToString().Trim())
                        {
                            tmpcount += int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                            //tmpprice = decimal.Parse(resultdt.Rows[i]["ParItemPrice"].ToString().Trim());
                            tmpcountprice += decimal.Parse(resultdt.Rows[i]["ParItemPriceCount"].ToString().Trim());
                        }
                        else
                        {
                            DataRow dr = resultdt.NewRow();
                            dr["WebLisSourceOrgName"] = "合计";
                            dr["StatisticsCount"] = tmpcount;
                            //dr["ParItemPrice"] = tmpprice;
                            dr["ParItemPriceCount"] = tmpcountprice;
                            if (i > 0)
                            {
                                tmpdrlist.Add(i, dr);
                            }
                            tmpclientno = resultdt.Rows[i]["WeblisSourceOrgId"].ToString().Trim();
                            tmpcount = int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                            //tmpprice = decimal.Parse(resultdt.Rows[i]["ParItemPrice"].ToString().Trim());
                            tmpcountprice = decimal.Parse(resultdt.Rows[i]["ParItemPriceCount"].ToString().Trim());
                        }
                        if (i == resultdt.Rows.Count - 1)
                        {
                            DataRow dr = resultdt.NewRow();
                            dr["WebLisSourceOrgName"] = "合计";
                            dr["StatisticsCount"] = tmpcount;
                            // dr["ParItemPrice"] = tmpprice;
                            dr["ParItemPriceCount"] = tmpcountprice;
                            tmpdrlist.Add(i + 1, dr);
                        }
                    }
                    if (tmpdrlist.Count > 0)
                    {
                        for (int i = tmpdrlist.Count - 1; i >= 0; i--)
                        {
                            resultdt.Rows.InsertAt(tmpdrlist.ElementAt(i).Value, tmpdrlist.ElementAt(i).Key);
                        }
                    }
                }
                brdv.ResultDataValue = DataSetToJson.ToJson(resultdt);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsRequestItemClient.异常：" + e.ToString());
                return brdv;
            }
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsRequestItemClient_Pdf?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsRequestItemClient_Pdf(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DateTime StartDateTimeT;
                DateTime EndDateTimeT;
                if (!DateTime.TryParse(StartDateTime, out StartDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：开始时间参数为空！";
                    return brdv;
                }
                if (!DateTime.TryParse(EndDateTime, out EndDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：结束时间参数为空！";
                    return brdv;
                }
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    brdv.ErrorInfo = "未登录，请登陆后继续！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestItemClient." + brdv.ErrorInfo);
                    return null;
                }
                else
                {
                    string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                    user = new ZhiFang.WebLis.Class.User(UserId);
                }
                IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
                EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = user.Account.Trim(), SelectedFlag = true, Flag = 0 }, 1, 10000, "ClIENTNO", " 1=1 ", " ClIENTNO ");
                if (cl == null || cl.list == null || cl.list.Count <= 0)
                {
                    brdv.ErrorInfo = "当前用户未配置所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestItemClient." + brdv.ErrorInfo);
                }
                List<string> cnolist = new List<string>();
                if (ClientNoList != null && ClientNoList.Trim() != "")
                {
                    var tmp = ClientNoList.Split(',');
                    foreach (var cno in tmp)
                    {
                        if (cl.list.Count(a => a.ClIENTNO == cno) > 0)
                        {
                            cnolist.Add(cno);
                        }
                    }
                }
                else
                {
                    foreach (var cno in cl.list)
                    {
                        cnolist.Add(cno.ClIENTNO);
                    }
                }
                if (cnolist.Count <= 0)
                {
                    brdv.ErrorInfo = "当前所选单位未配置成当前用户所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestItemClient." + brdv.ErrorInfo);
                }
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsRequestItemClient(StartDateTime, EndDateTime, string.Join(",", cnolist));
                FastReport.Report report = new FastReport.Report();
                if (resultdt != null && resultdt.Rows.Count > 0)
                {
                    string tmpclientno = "";
                    int tmpcount = 0;
                    //decimal tmpprice = 0;
                    decimal tmpcountprice = 0;
                    Dictionary<int, DataRow> tmpdrlist = new Dictionary<int, DataRow>();
                    for (int i = 0; i < resultdt.Rows.Count; i++)
                    {
                        if (tmpclientno == resultdt.Rows[i]["WeblisSourceOrgId"].ToString().Trim())
                        {
                            tmpcount += int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                            //tmpprice = decimal.Parse(resultdt.Rows[i]["ParItemPrice"].ToString().Trim());
                            tmpcountprice += decimal.Parse(resultdt.Rows[i]["ParItemPriceCount"].ToString().Trim());
                        }
                        else
                        {
                            DataRow dr = resultdt.NewRow();
                            dr["WebLisSourceOrgName"] = "合计";
                            dr["StatisticsCount"] = tmpcount;
                            //dr["ParItemPrice"] = tmpprice;
                            dr["ParItemPriceCount"] = tmpcountprice;
                            if (i > 0)
                            {
                                tmpdrlist.Add(i, dr);
                            }
                            tmpclientno = resultdt.Rows[i]["WeblisSourceOrgId"].ToString().Trim();
                            tmpcount = int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                            //tmpprice = decimal.Parse(resultdt.Rows[i]["ParItemPrice"].ToString().Trim());
                            tmpcountprice = decimal.Parse(resultdt.Rows[i]["ParItemPriceCount"].ToString().Trim());
                        }
                        if (i == resultdt.Rows.Count - 1)
                        {
                            DataRow dr = resultdt.NewRow();
                            dr["WebLisSourceOrgName"] = "合计";
                            dr["StatisticsCount"] = tmpcount;
                            //dr["ParItemPrice"] = tmpprice;
                            dr["ParItemPriceCount"] = tmpcountprice;
                            tmpdrlist.Add(i + 1, dr);
                        }
                    }
                    if (tmpdrlist.Count > 0)
                    {
                        for (int i = tmpdrlist.Count - 1; i >= 0; i--)
                        {
                            resultdt.Rows.InsertAt(tmpdrlist.ElementAt(i).Value, tmpdrlist.ElementAt(i).Key);
                        }
                    }
                }
                string url = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd") + "_" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong() + ".pdf";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestItemClient\\" + url;
                ZhiFang.Common.Log.Log.Debug("StatisticsRequestItemClient_Pdf.Url:" + strPath);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestItemClient\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestItemClient\\");
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\StatisticsRequestItemClient.frx");
                TextObject Text_DateRange = (TextObject)report.FindObject("Text_DateRange");
                if (Text_DateRange != null)
                {
                    Text_DateRange.Text = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd");
                }
                resultdt.TableName = "Table";
                report.RegisterData(resultdt.DataSet);
                report.Prepare();
                FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
                report.Export(export, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/StatisticsRequestItemClient/" + url;
                brdv.ResultDataValue = strTable;
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsRequestItemClient_pdf.异常：" + e.ToString());
                return brdv;
            }
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsRequestItemClient_Excel?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsRequestItemClient_Excel(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DateTime StartDateTimeT;
                DateTime EndDateTimeT;
                if (!DateTime.TryParse(StartDateTime, out StartDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：开始时间参数为空！";
                    return brdv;
                }
                if (!DateTime.TryParse(EndDateTime, out EndDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：结束时间参数为空！";
                    return brdv;
                }
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    brdv.ErrorInfo = "未登录，请登陆后继续！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestItemClient." + brdv.ErrorInfo);
                    return null;
                }
                else
                {
                    string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                    user = new ZhiFang.WebLis.Class.User(UserId);
                }
                IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
                EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = user.Account.Trim(), SelectedFlag = true, Flag = 0 }, 1, 10000, "ClIENTNO", " 1=1 ", " ClIENTNO ");
                if (cl == null || cl.list == null || cl.list.Count <= 0)
                {
                    brdv.ErrorInfo = "当前用户未配置所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestItemClient." + brdv.ErrorInfo);
                }
                List<string> cnolist = new List<string>();
                if (ClientNoList != null && ClientNoList.Trim() != "")
                {
                    var tmp = ClientNoList.Split(',');
                    foreach (var cno in tmp)
                    {
                        if (cl.list.Count(a => a.ClIENTNO == cno) > 0)
                        {
                            cnolist.Add(cno);
                        }
                    }
                }
                else
                {
                    foreach (var cno in cl.list)
                    {
                        cnolist.Add(cno.ClIENTNO);
                    }
                }
                if (cnolist.Count <= 0)
                {
                    brdv.ErrorInfo = "当前所选单位未配置成当前用户所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestItemClient." + brdv.ErrorInfo);
                }
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsRequestItemClient(StartDateTime, EndDateTime, string.Join(",", cnolist));

                if (resultdt != null && resultdt.Rows.Count > 0)
                {
                    string tmpclientno = "";
                    int tmpcount = 0;
                    //decimal tmpprice = 0;
                    decimal tmpcountprice = 0;
                    Dictionary<int, DataRow> tmpdrlist = new Dictionary<int, DataRow>();
                    for (int i = 0; i < resultdt.Rows.Count; i++)
                    {
                        if (tmpclientno == resultdt.Rows[i]["WeblisSourceOrgId"].ToString().Trim())
                        {
                            tmpcount += int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                            //tmpprice = decimal.Parse(resultdt.Rows[i]["ParItemPrice"].ToString().Trim());
                            tmpcountprice += decimal.Parse(resultdt.Rows[i]["ParItemPriceCount"].ToString().Trim());
                        }
                        else
                        {
                            DataRow dr = resultdt.NewRow();
                            dr["WebLisSourceOrgName"] = "合计";
                            dr["StatisticsCount"] = tmpcount;
                            //dr["ParItemPrice"] = tmpprice;
                            dr["ParItemPriceCount"] = tmpcountprice;
                            if (i > 0)
                            {
                                tmpdrlist.Add(i, dr);
                            }
                            tmpclientno = resultdt.Rows[i]["WeblisSourceOrgId"].ToString().Trim();
                            tmpcount = int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                            //tmpprice = decimal.Parse(resultdt.Rows[i]["ParItemPrice"].ToString().Trim());
                            tmpcountprice = decimal.Parse(resultdt.Rows[i]["ParItemPriceCount"].ToString().Trim());
                        }
                        if (i == resultdt.Rows.Count - 1)
                        {
                            DataRow dr = resultdt.NewRow();
                            dr["WebLisSourceOrgName"] = "合计";
                            dr["StatisticsCount"] = tmpcount;
                            //dr["ParItemPrice"] = tmpprice;
                            dr["ParItemPriceCount"] = tmpcountprice;
                            tmpdrlist.Add(i + 1, dr);
                        }
                    }
                    if (tmpdrlist.Count > 0)
                    {
                        for (int i = tmpdrlist.Count - 1; i >= 0; i--)
                        {
                            resultdt.Rows.InsertAt(tmpdrlist.ElementAt(i).Value, tmpdrlist.ElementAt(i).Key);
                        }
                    }
                }

                FastReport.Report report = new FastReport.Report();

                string url = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd") + "_" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong() + ".xlsx";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestItemClient\\" + url;
                ZhiFang.Common.Log.Log.Debug("StatisticsRequestItemClient_Excel.Url:" + strPath);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestItemClient\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestItemClient\\");
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\StatisticsRequestItemClient.frx");
                TextObject Text_DateRange = (TextObject)report.FindObject("Text_DateRange");
                if (Text_DateRange != null)
                {
                    Text_DateRange.Text = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd");
                }
                resultdt.TableName = "Table";
                report.RegisterData(resultdt.DataSet);
                report.Prepare();
                FastReport.Export.OoXML.Excel2007Export export = new FastReport.Export.OoXML.Excel2007Export();
                report.Export(export, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/StatisticsRequestItemClient/" + url;
                brdv.ResultDataValue = strTable;
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsRequestItemClient_Excel.异常：" + e.ToString());
                return brdv;
            }
        }
        #endregion
        #region 开单明细项目汇总报表客户
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsRequestDetailItemLab?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsRequestDetailItemLab(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    brdv.ErrorInfo = "未登录，请登陆后继续！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestDetailItemLab." + brdv.ErrorInfo);
                    return null;
                }
                else
                {
                    string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                    user = new ZhiFang.WebLis.Class.User(UserId);
                }
                IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
                EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = user.Account.Trim(), SelectedFlag = true, Flag = 0 }, 1, 10000, "ClIENTNO", " 1=1 ", " ClIENTNO ");
                if (cl == null || cl.list == null || cl.list.Count <= 0)
                {
                    brdv.ErrorInfo = "当前用户未配置所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestDetailItemLab." + brdv.ErrorInfo);
                }
                List<string> cnolist = new List<string>();
                if (ClientNoList != null && ClientNoList.Trim() != "")
                {
                    var tmp = ClientNoList.Split(',');
                    foreach (var cno in tmp)
                    {
                        if (cl.list.Count(a => a.ClIENTNO == cno) > 0)
                        {
                            cnolist.Add(cno);
                        }
                    }
                }
                else
                {
                    foreach (var cno in cl.list)
                    {
                        cnolist.Add(cno.ClIENTNO);
                    }
                }
                if (cnolist.Count <= 0)
                {
                    brdv.ErrorInfo = "当前所选单位未配置成当前用户所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestDetailItemLab." + brdv.ErrorInfo);
                }

                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsRequestDetailItemLab(StartDateTime, EndDateTime, string.Join(",", cnolist));

                if (resultdt != null && resultdt.Rows.Count > 0)
                {
                    int tmpcount = 0;
                    decimal tmpcountprice = 0;
                    Dictionary<int, DataRow> tmpdrlist = new Dictionary<int, DataRow>();
                    for (int i = 0; i < resultdt.Rows.Count; i++)
                    {
                        tmpcount += int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                        tmpcountprice += decimal.Parse(resultdt.Rows[i]["ParItemPriceCount"].ToString().Trim());
                    }

                    DataRow dr = resultdt.NewRow();
                    dr["WebLisSourceOrgName"] = "合计";
                    dr["StatisticsCount"] = tmpcount;
                    dr["ParItemPriceCount"] = tmpcountprice;
                    resultdt.Rows.Add(dr);
                }
                brdv.ResultDataValue = DataSetToJson.ToJson(resultdt);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsRequestDetailItemLab.异常：" + e.ToString());
                return brdv;
            }
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsRequestDetailItemLab_Pdf?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsRequestDetailItemLab_Pdf(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DateTime StartDateTimeT;
                DateTime EndDateTimeT;
                if (!DateTime.TryParse(StartDateTime, out StartDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：开始时间参数为空！";
                    return brdv;
                }
                if (!DateTime.TryParse(EndDateTime, out EndDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：结束时间参数为空！";
                    return brdv;
                }
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    brdv.ErrorInfo = "未登录，请登陆后继续！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestDetailItemLab_Pdf." + brdv.ErrorInfo);
                    return null;
                }
                else
                {
                    string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                    user = new ZhiFang.WebLis.Class.User(UserId);
                }
                IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
                EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = user.Account.Trim(), SelectedFlag = true, Flag = 0 }, 1, 10000, "ClIENTNO", " 1=1 ", " ClIENTNO ");
                if (cl == null || cl.list == null || cl.list.Count <= 0)
                {
                    brdv.ErrorInfo = "当前用户未配置所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestDetailItemLab_Pdf." + brdv.ErrorInfo);
                }
                List<string> cnolist = new List<string>();
                if (ClientNoList != null && ClientNoList.Trim() != "")
                {
                    var tmp = ClientNoList.Split(',');
                    foreach (var cno in tmp)
                    {
                        if (cl.list.Count(a => a.ClIENTNO == cno) > 0)
                        {
                            cnolist.Add(cno);
                        }
                    }
                }
                else
                {
                    foreach (var cno in cl.list)
                    {
                        cnolist.Add(cno.ClIENTNO);
                    }
                }
                if (cnolist.Count <= 0)
                {
                    brdv.ErrorInfo = "当前所选单位未配置成当前用户所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestDetailItemLab_Pdf." + brdv.ErrorInfo);
                }
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsRequestDetailItemLab(StartDateTime, EndDateTime, string.Join(",", cnolist));
                FastReport.Report report = new FastReport.Report();
                if (resultdt != null && resultdt.Rows.Count > 0)
                {
                    int tmpcount = 0;
                    decimal tmpcountprice = 0;
                    Dictionary<int, DataRow> tmpdrlist = new Dictionary<int, DataRow>();
                    for (int i = 0; i < resultdt.Rows.Count; i++)
                    {
                        tmpcount += int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                        tmpcountprice += decimal.Parse(resultdt.Rows[i]["ParItemPriceCount"].ToString().Trim());
                    }

                    DataRow dr = resultdt.NewRow();
                    dr["WebLisSourceOrgName"] = "合计";
                    dr["StatisticsCount"] = tmpcount;
                    dr["ParItemPriceCount"] = tmpcountprice;
                    resultdt.Rows.Add(dr);
                }
                string url = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd") + "_" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong() + ".pdf";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestDetailItemLab\\" + url;
                ZhiFang.Common.Log.Log.Debug("StatisticsRequestDetailItemLab_Pdf.Url:" + strPath);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestDetailItemLab\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestDetailItemLab\\");
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\StatisticsRequestDetailItemLab.frx");
                TextObject Text_DateRange = (TextObject)report.FindObject("Text_DateRange");
                if (Text_DateRange != null)
                {
                    Text_DateRange.Text = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd");
                }
                resultdt.TableName = "Table";
                report.RegisterData(resultdt.DataSet);
                report.Prepare();
                FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
                report.Export(export, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/StatisticsRequestDetailItemLab/" + url;
                brdv.ResultDataValue = strTable;
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsRequestDetailItemLab_pdf.异常：" + e.ToString());
                return brdv;
            }
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsRequestDetailItemLab_Excel?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsRequestDetailItemLab_Excel(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DateTime StartDateTimeT;
                DateTime EndDateTimeT;
                if (!DateTime.TryParse(StartDateTime, out StartDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：开始时间参数为空！";
                    return brdv;
                }
                if (!DateTime.TryParse(EndDateTime, out EndDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：结束时间参数为空！";
                    return brdv;
                }
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    brdv.ErrorInfo = "未登录，请登陆后继续！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestDetailItemLab_Excel." + brdv.ErrorInfo);
                    return null;
                }
                else
                {
                    string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                    user = new ZhiFang.WebLis.Class.User(UserId);
                }
                IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
                EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = user.Account.Trim(), SelectedFlag = true, Flag = 0 }, 1, 10000, "ClIENTNO", " 1=1 ", " ClIENTNO ");
                if (cl == null || cl.list == null || cl.list.Count <= 0)
                {
                    brdv.ErrorInfo = "当前用户未配置所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestDetailItemLab_Excel." + brdv.ErrorInfo);
                }
                List<string> cnolist = new List<string>();
                if (ClientNoList != null && ClientNoList.Trim() != "")
                {
                    var tmp = ClientNoList.Split(',');
                    foreach (var cno in tmp)
                    {
                        if (cl.list.Count(a => a.ClIENTNO == cno) > 0)
                        {
                            cnolist.Add(cno);
                        }
                    }
                }
                else
                {
                    foreach (var cno in cl.list)
                    {
                        cnolist.Add(cno.ClIENTNO);
                    }
                }
                if (cnolist.Count <= 0)
                {
                    brdv.ErrorInfo = "当前所选单位未配置成当前用户所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestDetailItemLab_Excel." + brdv.ErrorInfo);
                }
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsRequestDetailItemLab(StartDateTime, EndDateTime, string.Join(",", cnolist));

                if (resultdt != null && resultdt.Rows.Count > 0)
                {
                    int tmpcount = 0;
                    decimal tmpcountprice = 0;
                    Dictionary<int, DataRow> tmpdrlist = new Dictionary<int, DataRow>();
                    for (int i = 0; i < resultdt.Rows.Count; i++)
                    {
                        tmpcount += int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                        tmpcountprice += decimal.Parse(resultdt.Rows[i]["ParItemPriceCount"].ToString().Trim());
                    }

                    DataRow dr = resultdt.NewRow();
                    dr["WebLisSourceOrgName"] = "合计";
                    dr["StatisticsCount"] = tmpcount;
                    dr["ParItemPriceCount"] = tmpcountprice;
                    resultdt.Rows.Add(dr);
                }

                FastReport.Report report = new FastReport.Report();

                string url = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd") + "_" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong() + ".xlsx";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestDetailItemLab\\" + url;
                ZhiFang.Common.Log.Log.Debug("StatisticsRequestDetailItemLab_Excel.Url:" + strPath);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestDetailItemLab\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestDetailItemLab\\");
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\StatisticsRequestDetailItemLab.frx");
                TextObject Text_DateRange = (TextObject)report.FindObject("Text_DateRange");
                if (Text_DateRange != null)
                {
                    Text_DateRange.Text = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd");
                }
                resultdt.TableName = "Table";
                report.RegisterData(resultdt.DataSet);
                report.Prepare();
                FastReport.Export.OoXML.Excel2007Export export = new FastReport.Export.OoXML.Excel2007Export();
                report.Export(export, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/StatisticsRequestDetailItemLab/" + url;
                brdv.ResultDataValue = strTable;
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsRequestDetailItemLab_Excel.异常：" + e.ToString());
                return brdv;
            }
        }
        #endregion
        #region 开单组套项目汇总报表客户
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsRequestCombiItemLab?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsRequestCombiItemLab(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    brdv.ErrorInfo = "未登录，请登陆后继续！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestCombiItemLab." + brdv.ErrorInfo);
                    return null;
                }
                else
                {
                    string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                    user = new ZhiFang.WebLis.Class.User(UserId);
                }
                IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
                EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = user.Account.Trim(), SelectedFlag = true, Flag = 0 }, 1, 10000, "ClIENTNO", " 1=1 ", " ClIENTNO ");
                if (cl == null || cl.list == null || cl.list.Count <= 0)
                {
                    brdv.ErrorInfo = "当前用户未配置所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestCombiItemLab." + brdv.ErrorInfo);
                }
                List<string> cnolist = new List<string>();
                if (ClientNoList != null && ClientNoList.Trim() != "")
                {
                    var tmp = ClientNoList.Split(',');
                    foreach (var cno in tmp)
                    {
                        if (cl.list.Count(a => a.ClIENTNO == cno) > 0)
                        {
                            cnolist.Add(cno);
                        }
                    }
                }
                else
                {
                    foreach (var cno in cl.list)
                    {
                        cnolist.Add(cno.ClIENTNO);
                    }
                }
                if (cnolist.Count <= 0)
                {
                    brdv.ErrorInfo = "当前所选单位未配置成当前用户所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestCombiItemLab." + brdv.ErrorInfo);
                }

                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsRequestCombiItemLab(StartDateTime, EndDateTime, string.Join(",", cnolist));

                if (resultdt != null && resultdt.Rows.Count > 0)
                {

                    //Dictionary<int, DataRow> tmpdrlist = new Dictionary<int, DataRow>();
                    //for (int i = 0; i < resultdt.Rows.Count; i++)
                    //{
                    //    tmpcount += int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                    //    tmpcountprice += decimal.Parse(resultdt.Rows[i]["CombiItemPriceCount"].ToString().Trim());
                    //    tmpcountmarketprice += decimal.Parse(resultdt.Rows[i]["CombiItemMarketPriceCount"].ToString().Trim());
                    //}

                    //DataRow dr = resultdt.NewRow();
                    //dr["WebLisSourceOrgName"] = "合计";
                    //dr["StatisticsCount"] = tmpcount;
                    //dr["CombiItemMarketPriceCount"] = tmpcountprice;
                    //dr["CombiItemPriceCount"] = tmpcountmarketprice;
                    //resultdt.Rows.Add(dr);

                    int tmpcount = 0;
                    string tmpclientno = "";
                    decimal tmpcountprice = 0;
                    decimal tmpcountmarketprice = 0;
                    Dictionary<int, DataRow> tmpdrlist = new Dictionary<int, DataRow>();
                    for (int i = 0; i < resultdt.Rows.Count; i++)
                    {
                        if (tmpclientno == resultdt.Rows[i]["WeblisSourceOrgId"].ToString().Trim())
                        {
                            tmpcount += int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                            tmpcountprice += decimal.Parse(resultdt.Rows[i]["CombiItemPriceCount"].ToString().Trim());
                            tmpcountmarketprice += decimal.Parse(resultdt.Rows[i]["CombiItemMarketPriceCount"].ToString().Trim());
                        }
                        else
                        {
                            DataRow dr = resultdt.NewRow();
                            dr["WebLisSourceOrgName"] = "合计";
                            dr["StatisticsCount"] = tmpcount;
                            dr["CombiItemPriceCount"] = tmpcountprice;
                            dr["CombiItemMarketPriceCount"] = tmpcountmarketprice;
                            if (i > 0)
                            {
                                tmpdrlist.Add(i, dr);
                            }
                            tmpclientno = resultdt.Rows[i]["WeblisSourceOrgId"].ToString().Trim();
                            tmpcount = int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                            tmpcountprice = decimal.Parse(resultdt.Rows[i]["CombiItemPriceCount"].ToString().Trim());
                            tmpcountmarketprice += decimal.Parse(resultdt.Rows[i]["CombiItemMarketPriceCount"].ToString().Trim());
                        }
                        if (i == resultdt.Rows.Count - 1)
                        {
                            DataRow dr = resultdt.NewRow();
                            dr["WebLisSourceOrgName"] = "合计";
                            dr["StatisticsCount"] = tmpcount;
                            dr["CombiItemPriceCount"] = tmpcountprice;
                            dr["CombiItemMarketPriceCount"] = tmpcountmarketprice;
                            tmpdrlist.Add(i + 1, dr);
                        }
                    }
                    if (tmpdrlist.Count > 0)
                    {
                        for (int i = tmpdrlist.Count - 1; i >= 0; i--)
                        {
                            resultdt.Rows.InsertAt(tmpdrlist.ElementAt(i).Value, tmpdrlist.ElementAt(i).Key);
                        }
                    }
                }
                brdv.ResultDataValue = DataSetToJson.ToJson(resultdt);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsRequestCombiItemLab.异常：" + e.ToString());
                return brdv;
            }
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsRequestCombiItemLab_Pdf?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsRequestCombiItemLab_Pdf(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DateTime StartDateTimeT;
                DateTime EndDateTimeT;
                if (!DateTime.TryParse(StartDateTime, out StartDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：开始时间参数为空！";
                    return brdv;
                }
                if (!DateTime.TryParse(EndDateTime, out EndDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：结束时间参数为空！";
                    return brdv;
                }
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    brdv.ErrorInfo = "未登录，请登陆后继续！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestCombiItemLab_Pdf." + brdv.ErrorInfo);
                    return null;
                }
                else
                {
                    string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                    user = new ZhiFang.WebLis.Class.User(UserId);
                }
                IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
                EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = user.Account.Trim(), SelectedFlag = true, Flag = 0 }, 1, 10000, "ClIENTNO", " 1=1 ", " ClIENTNO ");
                if (cl == null || cl.list == null || cl.list.Count <= 0)
                {
                    brdv.ErrorInfo = "当前用户未配置所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestCombiItemLab_Pdf." + brdv.ErrorInfo);
                }
                List<string> cnolist = new List<string>();
                if (ClientNoList != null && ClientNoList.Trim() != "")
                {
                    var tmp = ClientNoList.Split(',');
                    foreach (var cno in tmp)
                    {
                        if (cl.list.Count(a => a.ClIENTNO == cno) > 0)
                        {
                            cnolist.Add(cno);
                        }
                    }
                }
                else
                {
                    foreach (var cno in cl.list)
                    {
                        cnolist.Add(cno.ClIENTNO);
                    }
                }
                if (cnolist.Count <= 0)
                {
                    brdv.ErrorInfo = "当前所选单位未配置成当前用户所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestCombiItemLab_Pdf." + brdv.ErrorInfo);
                }
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsRequestCombiItemLab(StartDateTime, EndDateTime, string.Join(",", cnolist));
                FastReport.Report report = new FastReport.Report();
                if (resultdt != null && resultdt.Rows.Count > 0)
                {
                    int tmpcount = 0;
                    decimal tmpcountprice = 0;
                    Dictionary<int, DataRow> tmpdrlist = new Dictionary<int, DataRow>();
                    for (int i = 0; i < resultdt.Rows.Count; i++)
                    {
                        tmpcount += int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                        tmpcountprice += decimal.Parse(resultdt.Rows[i]["CombiItemPriceCount"].ToString().Trim());
                    }

                    DataRow dr = resultdt.NewRow();
                    dr["WebLisSourceOrgName"] = "合计";
                    dr["StatisticsCount"] = tmpcount;
                    dr["CombiItemPriceCount"] = tmpcountprice;
                    resultdt.Rows.Add(dr);
                }
                string url = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd") + "_" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong() + ".pdf";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestCombiItemLab\\" + url;
                ZhiFang.Common.Log.Log.Debug("StatisticsRequestCombiItemLab_Pdf.Url:" + strPath);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestCombiItemLab\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestCombiItemLab\\");
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\StatisticsRequestCombiItemLab.frx");
                TextObject Text_DateRange = (TextObject)report.FindObject("Text_DateRange");
                if (Text_DateRange != null)
                {
                    Text_DateRange.Text = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd");
                }
                resultdt.TableName = "Table";
                report.RegisterData(resultdt.DataSet);
                report.Prepare();
                FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
                report.Export(export, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/StatisticsRequestCombiItemLab/" + url;
                brdv.ResultDataValue = strTable;
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsRequestCombiItemLab_pdf.异常：" + e.ToString());
                return brdv;
            }
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsRequestCombiItemLab_Excel?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsRequestCombiItemLab_Excel(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DateTime StartDateTimeT;
                DateTime EndDateTimeT;
                if (!DateTime.TryParse(StartDateTime, out StartDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：开始时间参数为空！";
                    return brdv;
                }
                if (!DateTime.TryParse(EndDateTime, out EndDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：结束时间参数为空！";
                    return brdv;
                }
                ZhiFang.WebLis.Class.User user = new ZhiFang.WebLis.Class.User();
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser") == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser").Trim() == "")
                {
                    brdv.ErrorInfo = "未登录，请登陆后继续！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestCombiItemLab_Excel." + brdv.ErrorInfo);
                    return null;
                }
                else
                {
                    string UserId = ZhiFang.Common.Public.Cookie.CookieHelper.Read("ZhiFangUser");
                    user = new ZhiFang.WebLis.Class.User(UserId);
                }
                IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
                EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = user.Account.Trim(), SelectedFlag = true, Flag = 0 }, 1, 10000, "ClIENTNO", " 1=1 ", " ClIENTNO ");
                if (cl == null || cl.list == null || cl.list.Count <= 0)
                {
                    brdv.ErrorInfo = "当前用户未配置所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestCombiItemLab_Excel." + brdv.ErrorInfo);
                }
                List<string> cnolist = new List<string>();
                if (ClientNoList != null && ClientNoList.Trim() != "")
                {
                    var tmp = ClientNoList.Split(',');
                    foreach (var cno in tmp)
                    {
                        if (cl.list.Count(a => a.ClIENTNO == cno) > 0)
                        {
                            cnolist.Add(cno);
                        }
                    }
                }
                else
                {
                    foreach (var cno in cl.list)
                    {
                        cnolist.Add(cno.ClIENTNO);
                    }
                }
                if (cnolist.Count <= 0)
                {
                    brdv.ErrorInfo = "当前所选单位未配置成当前用户所属单位和管理单位！";
                    brdv.success = false;
                    ZhiFang.Common.Log.Log.Info("StatisticsRequestCombiItemLab_Excel." + brdv.ErrorInfo);
                }
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsRequestCombiItemLab(StartDateTime, EndDateTime, string.Join(",", cnolist));

                if (resultdt != null && resultdt.Rows.Count > 0)
                {
                    int tmpcount = 0;
                    decimal tmpcountprice = 0;
                    Dictionary<int, DataRow> tmpdrlist = new Dictionary<int, DataRow>();
                    for (int i = 0; i < resultdt.Rows.Count; i++)
                    {
                        tmpcount += int.Parse(resultdt.Rows[i]["StatisticsCount"].ToString().Trim());
                        tmpcountprice += decimal.Parse(resultdt.Rows[i]["CombiItemPriceCount"].ToString().Trim());
                    }

                    DataRow dr = resultdt.NewRow();
                    dr["WebLisSourceOrgName"] = "合计";
                    dr["StatisticsCount"] = tmpcount;
                    dr["CombiItemPriceCount"] = tmpcountprice;
                    resultdt.Rows.Add(dr);
                }

                FastReport.Report report = new FastReport.Report();

                string url = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd") + "_" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong() + ".xlsx";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestCombiItemLab\\" + url;
                ZhiFang.Common.Log.Log.Debug("StatisticsRequestCombiItemLab_Excel.Url:" + strPath);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestCombiItemLab\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsRequestCombiItemLab\\");
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\StatisticsRequestCombiItemLab.frx");
                TextObject Text_DateRange = (TextObject)report.FindObject("Text_DateRange");
                if (Text_DateRange != null)
                {
                    Text_DateRange.Text = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd");
                }
                resultdt.TableName = "Table";
                report.RegisterData(resultdt.DataSet);
                report.Prepare();
                FastReport.Export.OoXML.Excel2007Export export = new FastReport.Export.OoXML.Excel2007Export();
                report.Export(export, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/StatisticsRequestCombiItemLab/" + url;
                brdv.ResultDataValue = strTable;
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsRequestCombiItemLab_Excel.异常：" + e.ToString());
                return brdv;
            }
        }
        #endregion

        #region 样本量汇总报表中心
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsBarCodeCountGroupByClientNoAndReportDate?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsBarCodeCountGroupByClientNoAndReportDate(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsBarCodeCountGroupByClientNoAndReportDate(StartDateTime, EndDateTime, ClientNoList, DateType);
                brdv.ResultDataValue = DataSetToJson.ToJson(resultdt);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsBarCodeCountGroupByClientNoAndReportDate.异常：" + e.ToString());
                return brdv;
            }
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsBarCodeCountGroupByClientNoAndReportDate_Pdf?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsBarCodeCountGroupByClientNoAndReportDate_Pdf(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DateTime StartDateTimeT;
                DateTime EndDateTimeT;
                if (!DateTime.TryParse(StartDateTime, out StartDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：开始时间参数为空！";
                    return brdv;
                }
                if (!DateTime.TryParse(EndDateTime, out EndDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：结束时间参数为空！";
                    return brdv;
                }
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsBarCodeCountGroupByClientNoAndReportDate(StartDateTime, EndDateTime, ClientNoList, DateType);
                FastReport.Report report = new FastReport.Report();

                string url = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd") + ".pdf";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsBarCodeCountGroupByClientNoAndReportDate\\" + url;
                ZhiFang.Common.Log.Log.Debug("StatisticsBarCodeCountGroupByClientNoAndReportDate_Pdf.Url:" + strPath);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsBarCodeCountGroupByClientNoAndReportDate\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsBarCodeCountGroupByClientNoAndReportDate\\");
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\StatisticsBarCodeCountGroupByClientNoAndReportDate.frx");
                TextObject Text_DateRange = (TextObject)report.FindObject("Text_DateRange");
                if (Text_DateRange != null)
                {
                    Text_DateRange.Text = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd");
                }
                resultdt.TableName = "Table";
                report.RegisterData(resultdt.DataSet);
                report.Prepare();
                FastReport.Export.Pdf.PDFExport export = new FastReport.Export.Pdf.PDFExport();
                report.Export(export, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/StatisticsBarCodeCountGroupByClientNoAndReportDate/" + url;
                brdv.ResultDataValue = strTable;
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsBarCodeCountGroupByClientNoAndReportDate_pdf.异常：" + e.ToString());
                return brdv;
            }
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsBarCodeCountGroupByClientNoAndReportDate_Excel?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsBarCodeCountGroupByClientNoAndReportDate_Excel(string StartDateTime, string EndDateTime, string ClientNoList, int DateType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                DateTime StartDateTimeT;
                DateTime EndDateTimeT;
                if (!DateTime.TryParse(StartDateTime, out StartDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：开始时间参数为空！";
                    return brdv;
                }
                if (!DateTime.TryParse(EndDateTime, out EndDateTimeT))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "程序异常：结束时间参数为空！";
                    return brdv;
                }
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsBarCodeCountGroupByClientNoAndReportDate(StartDateTime, EndDateTime, ClientNoList, DateType);
                FastReport.Report report = new FastReport.Report();

                string url = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd") + ".xlsx";
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsBarCodeCountGroupByClientNoAndReportDate\\" + url;
                ZhiFang.Common.Log.Log.Debug("StatisticsBarCodeCountGroupByClientNoAndReportDate_Excel.Url:" + strPath);

                if (!Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsBarCodeCountGroupByClientNoAndReportDate\\"))
                    Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + ConfigHelper.GetConfigString("SaveExcel") + "\\StatisticsBarCodeCountGroupByClientNoAndReportDate\\");
                report.Load(System.AppDomain.CurrentDomain.BaseDirectory + "\\ModelPathFrx\\StatisticsBarCodeCountGroupByClientNoAndReportDate.frx");
                TextObject Text_DateRange = (TextObject)report.FindObject("Text_DateRange");
                if (Text_DateRange != null)
                {
                    Text_DateRange.Text = StartDateTimeT.ToString("yyyy-MM-dd") + "_" + EndDateTimeT.ToString("yyyy-MM-dd");
                }
                resultdt.TableName = "Table";
                report.RegisterData(resultdt.DataSet);
                report.Prepare();
                FastReport.Export.OoXML.Excel2007Export export = new FastReport.Export.OoXML.Excel2007Export();
                report.Export(export, strPath);
                report.Dispose();
                string strTable = "/" + ConfigHelper.GetConfigString("SaveExcel") + "/StatisticsBarCodeCountGroupByClientNoAndReportDate/" + url;
                brdv.ResultDataValue = strTable;
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsBarCodeCountGroupByClientNoAndReportDate_Excel.异常：" + e.ToString());
                return brdv;
            }
        }
        #endregion
        #endregion
        #region 大屏

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsMainScreen?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&Limit={Limit}&DataType={DataType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsMainScreen(string StartDateTime, string EndDateTime, string ClientNoList, int Limit, string DataType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            if (string.IsNullOrWhiteSpace(DataType))
            {
                brdv.success = false;
                brdv.ErrorInfo = "DataType参数错误！";
                return brdv;
            }
            switch (DataType.Trim())
            {
                case "1":
                    brdv = StatisticsGetTestFinish(StartDateTime, EndDateTime, ClientNoList, Limit);
                    break;
                case "2":
                    brdv = StatisticsGetTestFinishCount(StartDateTime, EndDateTime, ClientNoList);
                    break;
                case "3":
                    brdv = StatisticsGetTestFinishCountTop(StartDateTime, EndDateTime, ClientNoList, Limit);
                    break;
                case "4":
                    brdv = StatisticsGetBarCodeDeliveryInfo(StartDateTime, EndDateTime, ClientNoList, Limit);
                    break;
                case "5":
                    brdv = StatisticsGetTestFinishCountByYear(StartDateTime, EndDateTime, ClientNoList);
                    break;
                case "6":
                    brdv = StatisticsGetBarCodeSendCountTop(StartDateTime, EndDateTime, ClientNoList, Limit);
                    break;
                case "7":
                    brdv = StatisticsGetReportCountTop(StartDateTime, EndDateTime, ClientNoList, Limit);
                    break;
                case "8":
                    brdv = StatisticsGetReportCountByYear(StartDateTime, EndDateTime, ClientNoList);
                    break;
                case "9":
                    brdv = StatisticsGetTestItemCountTop(StartDateTime, EndDateTime, ClientNoList, Limit);
                    break;
            }
            return brdv;
        }

        #region 当日检验完成量统计
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsGetTestFinishCount?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsGetTestFinishCount(string StartDateTime, string EndDateTime, string ClientNoList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                Dictionary<string, int> tmp = StatisticsReport.StatisticsGetTestFinishCount(StartDateTime, EndDateTime, ClientNoList);
                dynamic test = new ExpandoObject();
                test.BarCodeCount = tmp["BarCodeCount"];
                test.BarCodeTestFinishCount = tmp["BarCodeTestFinishCount"];
                test.BarCodeTestingCount = tmp["BarCodeTestingCount"];
                test.BarCodeUnTestCount = tmp["BarCodeUnTestCount"];

                brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(test);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsGetTestFinishCount.异常：" + e.ToString());
                return brdv;
            }
        }
        #endregion

        #region 检验完成量统计
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsGetTestFinish?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&Limit={Limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsGetTestFinish(string StartDateTime, string EndDateTime, string ClientNoList, int Limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsGetTestFinish(StartDateTime, EndDateTime, ClientNoList, Limit);
                brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(resultdt);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsGetTestFinish.异常：" + e.ToString());
                return brdv;
            }
        }
        #endregion

        #region 检验量排名(总)
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsGetTestFinishCountTop?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&Limit={Limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsGetTestFinishCountTop(string StartDateTime, string EndDateTime, string ClientNoList, int Limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsGetTestFinishCountTop(StartDateTime, EndDateTime, ClientNoList, Limit);
                brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(resultdt);
                brdv.success = true;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = e.Message;
            }

            return brdv;

        }
        #endregion        

        #region 全年检验量统计(总)
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsGetTestFinishCountByYear?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsGetTestFinishCountByYear(string StartDateTime, string EndDateTime, string ClientNoList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsGetTestFinishCountByYear(StartDateTime, EndDateTime, ClientNoList);
                brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(resultdt);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsGetTestFinishCountByYear.异常：" + e.ToString());
                return brdv;
            }
        }
        #endregion

        #region 标本物流信息明细
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsGetBarCodeDeliveryInfo?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&Limit={Limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsGetBarCodeDeliveryInfo(string StartDateTime, string EndDateTime, string ClientNoList, int Limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                List<dynamic> list = new List<dynamic>();
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable dt = StatisticsReport.StatisticsGetBarCodeDeliveryInfo(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00:00", DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59", ClientNoList, Limit);
                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dynamic test = new ExpandoObject();
                        test.LabCode = dt.Rows[i]["WeblisSourceOrgId"].ToString();
                        test.LabName = dt.Rows[i]["WeblisSourceOrgName"].ToString();
                        test.DeliveryBarCodeCount = dt.Rows[i]["DeliveryBarCodeCount"].ToString();
                        test.DateTime = DateTime.Now.ToString("yyyy-MM-dd");
                        list.Add(test);
                    }
                    brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(list);
                }
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = "程序异常！";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("StatisticsGetBarCodeDeliveryInfo.异常！" + e.ToString());
                return brdv;
            }
        }
        #endregion

        #region 送检量排名(总)
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsGetBarCodeSendCountTop?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&Limit={Limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsGetBarCodeSendCountTop(string StartDateTime, string EndDateTime, string ClientNoList, int Limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsGetBarCodeSendCountTop(StartDateTime, EndDateTime, ClientNoList, Limit);
                brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(resultdt);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return brdv;
            }
        }
        #endregion        

        #region 全年报告量统计(总)
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsGetReportCountByYear?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsGetReportCountByYear(string StartDateTime, string EndDateTime, string ClientNoList)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsGetReportCountByYear(StartDateTime, EndDateTime, ClientNoList);
                brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(resultdt);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsGetReportCountByYear.异常：" + e.ToString());
                return brdv;
            }
        }
        #endregion

        #region 报告量排名(总)
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsGetReportCountTop?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&Limit={Limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsGetReportCountTop(string StartDateTime, string EndDateTime, string ClientNoList, int Limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsGetReportCountTop(StartDateTime, EndDateTime, ClientNoList, Limit);
                brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(resultdt);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(" ZhiFang.WebLis.ServiceWCF.StatisticsService.StatisticsGetReportCountTop.异常：" + e.ToString());
                return brdv;
            }
        }
        #endregion

        #region 检验项目量排名(总)
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StatisticsGetTestItemCountTop?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&ClientNoList={ClientNoList}&Limit={Limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue StatisticsGetTestItemCountTop(string StartDateTime, string EndDateTime, string ClientNoList, int Limit)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable resultdt = StatisticsReport.StatisticsGetTestItemCountTop(StartDateTime, EndDateTime, ClientNoList, Limit);
                brdv.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(resultdt);
                brdv.success = true;
                return brdv;
            }
            catch (Exception e)
            {
                brdv.success = false;
                brdv.ErrorInfo = "程序异常：" + e.Message;
                ZhiFang.Common.Log.Log.Error(e.ToString());
                return brdv;
            }
        }
        #endregion

        #endregion

        #region 芜湖大屏
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Wuhu_StatisticsMainScreen?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}&Limit={Limit}&DataType={DataType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue Wuhu_StatisticsMainScreen(string StartDateTime, string EndDateTime,  int Limit, string DataType)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();

            if (string.IsNullOrWhiteSpace(DataType))
            {
                brdv.success = false;
                brdv.ErrorInfo = "DataType参数错误！";
                return brdv;
            }
            switch (DataType.Trim())
            {
                case "1":
                    brdv = Wuhu_StatisticsGender(StartDateTime, EndDateTime);
                    break;
                case "2":
                    brdv = Wuhu_StatisticsAge(StartDateTime, EndDateTime);
                    break;
                case "3":
                    brdv = Wuhu_StatisticsInspectionData(StartDateTime, EndDateTime);
                    break;
                case "4":
                    brdv = Wuhu_StatisticsDataAnalysis(StartDateTime, EndDateTime);
                    break;
                case "5":
                    brdv = Wuhu_StatisticsHosptalGrade(StartDateTime, EndDateTime);
                    break;
                case "6":
                    brdv = Wuhu_StatisticsUrbanRuralGrade(StartDateTime, EndDateTime);
                    break;
                case "7":
                    brdv = Wuhu_StatisticsAreaDetectionQuantity(StartDateTime, EndDateTime);
                    break;
                //case "8":
                //    brdv = StatisticsGetReportCountByYear(StartDateTime, EndDateTime);
                //    break;
                case "9":
                    brdv = Wuhu_StatisticsPopInspectionFee(StartDateTime, EndDateTime);
                    break;
            }
            return brdv;
        }

        #region 性别分布统计
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Wuhu_StatisticsGender?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        
        public BaseResultDataValue Wuhu_StatisticsGender(string StartDateTime, string EndDateTime)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                Dictionary<string, string> tmp = StatisticsReport.Wuhu_StatisticsGender(StartDateTime, EndDateTime);
                dynamic test = new ExpandoObject(); 
                test.Man = tmp["man"];
                test.WuMan = tmp["wuman"];

                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(test);
                baseResultDataValue.success = true;
                return baseResultDataValue;
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region 年龄分布统计
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Wuhu_StatisticsAge?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]

        public BaseResultDataValue Wuhu_StatisticsAge(string StartDateTime, string EndDateTime)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                Dictionary<string, string> tmp = StatisticsReport.Wuhu_StatisticsAge(StartDateTime, EndDateTime);
                dynamic test = new ExpandoObject();
                test.Age5 = tmp["age5"];
                test.Age5_14 = tmp["age5_14"];
                test.Age15_44 = tmp["age15_44"];
                test.Age45_59 = tmp["age45_59"]; 
                test.Age60 = tmp["age60"];
                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(test);
                baseResultDataValue.success = true;
                return baseResultDataValue;
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return baseResultDataValue;
        }

        #endregion

        #region 检验数据构成
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Wuhu_StatisticsInspectionData?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]

        public BaseResultDataValue Wuhu_StatisticsInspectionData(string StartDateTime, string EndDateTime)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable tmp = StatisticsReport.Wuhu_StatisticsInspectionData(StartDateTime, EndDateTime);
                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tmp);
                baseResultDataValue.success = true;
                return baseResultDataValue;
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region 检验数据分析统计
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Wuhu_StatisticsDataAnalysis?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]

        public BaseResultDataValue Wuhu_StatisticsDataAnalysis(string StartDateTime, string EndDateTime)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                Wuhu_ReportFormFullVo tmp = StatisticsReport.Wuhu_StatisticsDataAnalysis(StartDateTime, EndDateTime);
                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tmp);
                baseResultDataValue.success = true;
                return baseResultDataValue;
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region 各级医院分布
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Wuhu_StatisticsHosptalGrade?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]

        public BaseResultDataValue Wuhu_StatisticsHosptalGrade(string StartDateTime, string EndDateTime)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable tmp = StatisticsReport.Wuhu_StatisticsHosptalGrade(StartDateTime, EndDateTime);
                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tmp);
                baseResultDataValue.success = true;
                return baseResultDataValue;
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region 城乡分布
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Wuhu_StatisticsUrbanRuralGrade?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]

        public BaseResultDataValue Wuhu_StatisticsUrbanRuralGrade(string StartDateTime, string EndDateTime)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable tmp = StatisticsReport.Wuhu_StatisticsUrbanRuralGrade(StartDateTime, EndDateTime);
                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tmp);
                baseResultDataValue.success = true;
                return baseResultDataValue;
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region 根据时间查询检测量
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Wuhu_StatisticsAreaDetectionQuantity?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]

        public BaseResultDataValue Wuhu_StatisticsAreaDetectionQuantity(string StartDateTime, string EndDateTime)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable tmp = StatisticsReport.Wuhu_StatisticsAreaDetectionQuantity(StartDateTime, EndDateTime);
                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tmp);
                baseResultDataValue.success = true;
                return baseResultDataValue;
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region 人均检验费用
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Wuhu_StatisticsPopInspectionFee?StartDateTime={StartDateTime}&EndDateTime={EndDateTime}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]

        public BaseResultDataValue Wuhu_StatisticsPopInspectionFee(string StartDateTime, string EndDateTime)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                StatisticsReport StatisticsReport = new StatisticsReport();
                DataTable tmp = StatisticsReport.Wuhu_StatisticsPopInspectionFee(StartDateTime, EndDateTime);
                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tmp);
                baseResultDataValue.success = true;
                return baseResultDataValue;
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #endregion
    }
}
