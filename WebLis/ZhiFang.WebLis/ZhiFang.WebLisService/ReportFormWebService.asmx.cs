using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZhiFang.IBLL.Report;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common;
using System.Xml;
using System.Data;
using ZhiFang.Common.Log;
using System.Text;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using ZhiFang.Common.Public;
using ZhiFang.Common.Dictionary;
using Tools;
using ZhiFang.BLL.Report.Print;

namespace ZhiFang.WebLisService
{
    /// <summary>
    /// ReportFormWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = (true) ? "http://tempuri.org/" : "http://ZhiFang.WebLisService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ReportFormWebService : System.Web.Services.WebService
    {
        private readonly IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
        private readonly IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
        private readonly IBReportItemFull ibrif = BLLFactory<IBReportItemFull>.GetBLL("ReportItemFull");
        private readonly IBTestItemControl ibtic = BLLFactory<IBTestItemControl>.GetBLL("BaseDictionary.TestItemControl");
        private readonly IBSuperGroupControl ibsgc = BLLFactory<IBSuperGroupControl>.GetBLL("BaseDictionary.SuperGroupControl");
        private readonly IBFolkTypeControl ibftc = BLLFactory<IBFolkTypeControl>.GetBLL("BaseDictionary.FolkTypeControl");
        private readonly IBSampleTypeControl ibstc = BLLFactory<IBSampleTypeControl>.GetBLL("BaseDictionary.SampleTypeControl");
        private readonly IBGenderTypeControl ibgtc = BLLFactory<IBGenderTypeControl>.GetBLL("BaseDictionary.GenderTypeControl");
        private readonly IBReportData ibrd = BLLFactory<IBReportData>.GetBLL("ReportData");
        private readonly IFilesHelper ifh = BLLFactory<IFilesHelper>.GetBLL("FilesHelper");
        private readonly IBLL.Report.Other.IBView ibv = BLLFactory<IBLL.Report.Other.IBView>.GetBLL("Other.BView");
        private readonly IBShowFrom showform = BLLFactory<IBShowFrom>.GetBLL("ShowFrom");

        Model.ReportFormFull rf = new Model.ReportFormFull();
        ReportFormService rfs_svc = new ReportFormService();

        //ZhiFang.WebLisService.printService.ZhiFangWebLisPrintServiceClient zhifangprintService = new printService.ZhiFangWebLisPrintServiceClient();

        [WebMethod]
        public string HelloWorld(string name)
        {
            return "Hello World " + name;
        }

        [WebMethod(Description = "下载报告")]
        public bool DownloadReport(
           string SourceOrgID,             //送检(源)单位
           string DestiOrgID,              //外送(至)单位
           string BarCodeNo,               //条码码
           out string nodeReportFormItem, //一个条码可能有多个报告单，多个项目，多个文件
           out byte[][] FileReport,        //报告单号 + 报告文件流
           out string FileType,            //PDF,FRP,RTF
           out string xmlWebLisOthers,     //返回更多信息
           out string ReturnDescription)   //其他描述
        {
            nodeReportFormItem = null;
            FileReport = null;
            FileType = "PDF";
            ReturnDescription = "";
            xmlWebLisOthers = null;
            try
            {
                Model.BarCodeForm barCodeForm = new Model.BarCodeForm();
                if (BarCodeNo != null && BarCodeNo.Trim() != "")
                {
                    barCodeForm.BarCode = BarCodeNo;
                    barCodeForm.WebLisSourceOrgId = SourceOrgID;
                }
                else
                {
                    ReturnDescription = "要求传入条码号才能下载报告";
                    return false;
                }
                DataSet dsBarCodeForm = ibbcf.GetList(barCodeForm);
                dsBarCodeForm.Tables[0].TableName = "Table";
                //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID

                if (dsBarCodeForm == null || dsBarCodeForm.Tables.Count == 0)
                {
                    ReturnDescription = "数据库连接或表结构出错";
                    return false;
                }
                if (dsBarCodeForm.Tables[0].Rows.Count == 0)
                {
                    ReturnDescription = "没有找到条码号[" + BarCodeNo + "]的报告数据";
                    return false;
                }
                xmlWebLisOthers = dsBarCodeForm.GetXml();
                if (Convert.IsDBNull(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"]))
                {
                    ReturnDescription = "条码号[" + BarCodeNo + "]样本未开始检验,无报告";
                    return false;
                }
                int webLisFlag = Int32.Parse(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"].ToString());
                if (webLisFlag <= 9)
                {
                    ReturnDescription = "样本处于状态[" + webLisFlag.ToString() + "]\n状态小于8(<)代表已经签收\n状态等于8代表正在检验\n状态等于9代表正在重新审定中";
                    return false;
                }
                string str = "";
                Model.ReportFormFull reportFormFull = new Model.ReportFormFull();
                reportFormFull.BarCode = BarCodeNo;
                DataSet dsReportForms = ibrff.GetList(reportFormFull);
                dsReportForms.Tables[0].TableName = "Table";
                //检测中心端的编码是否对照
                if (!ibrd.CheckCenterNo(dsReportForms, DestiOrgID, out str))
                {
                    ReturnDescription += str;
                    return false;
                }
                XmlDocument xdReportItems = new XmlDocument();
                XmlDocument xdReportForms = new XmlDocument();
                DataRowCollection drReportForms = dsReportForms.Tables[0].Rows;
                foreach (DataRow drReportForm in drReportForms)
                {
                    Model.ReportItemFull ReportItemFull = new Model.ReportItemFull();
                    ReportItemFull.ReportFormID = drReportForm["ReportFormID"].ToString();
                    DataSet dsReportItems = ibrif.GetList(ReportItemFull);
                    dsReportItems.Tables[0].TableName = "Table";
                    //检测ReportItemFull是否对照
                    if (!ibrd.CheckCenterNo(dsReportItems, DestiOrgID, out str))
                    {
                        ReturnDescription += str;
                        return false;
                    }

                    //转码过程
                    dsReportForms = MatchLabNo(dsReportForms, DestiOrgID);
                    dsReportItems = MatchLabNo(dsReportItems, DestiOrgID);
                    xdReportForms.LoadXml(dsReportForms.GetXml());
                    XmlNode nodeEachForm = xdReportForms.SelectSingleNode("//ReportFormID[text()='" + ReportItemFull.ReportFormID + "']");
                    XmlNode nodeEachFormParent = nodeEachForm.ParentNode;
                    xdReportItems.LoadXml(dsReportItems.GetXml());
                    XmlNode nodeTempItem = xdReportForms.CreateElement("ReportItems");
                    nodeTempItem.InnerXml = xdReportItems.DocumentElement.InnerXml;
                    nodeEachFormParent.AppendChild(nodeTempItem);
                }
                nodeReportFormItem = xdReportForms.DocumentElement.OuterXml;
            }
            catch (Exception ex)
            {
                ReturnDescription = "出错:" + ex.Message;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 报告下载服务，需要验证用户名和密码 
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="PassWord"></param>
        /// <param name="BarcodeNo"></param>
        /// <param name="ClientNo"></param>
        /// <param name="WebReportXML"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        [WebMethod(Description = "下载报告")]
        public bool DownloadReportByBarcodeNo(string Account, string PassWord, string BarcodeNo, string ClientNo, string DestiOrgID, out string WebReportXML, out string Error)
        {
            Log.Info("DownloadReportByBarcodeNo");
            WebReportXML = "";
            Error = "";
            string str = "";
            try
            {
                //string user = ConfigHelper.GetConfigString("Account");
                //string pwd = ConfigHelper.GetConfigString("PassWord");
                string user = "";
                string pwd = "";
                if (System.Configuration.ConfigurationManager.AppSettings["Account"] != null && System.Configuration.ConfigurationManager.AppSettings["Account"].Trim() != "")
                {
                    user = System.Configuration.ConfigurationManager.AppSettings["Account"].Trim();
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Account未配置！");
                }
                if (System.Configuration.ConfigurationManager.AppSettings["PassWord"] != null && System.Configuration.ConfigurationManager.AppSettings["PassWord"].Trim() != "")
                {
                    pwd = System.Configuration.ConfigurationManager.AppSettings["PassWord"].Trim();
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("PassWord未配置！");
                }
                if (user != Account && pwd != PassWord)
                {
                    ZhiFang.Common.Log.Log.Info("用户和密码：" + Account + " " + PassWord);
                    Error = "用户名或者密码错误！";
                    return false;
                }

                DataSet reportFormFull = new DataSet();
                DataSet reportAllItemView = new DataSet();
                StringBuilder builder = new StringBuilder();
                builder.Append(" 1=1 ");
                if (BarcodeNo != null && BarcodeNo != "")
                    builder.Append(" and SERIALNO='" + BarcodeNo + "'");
                if (ClientNo != null && ClientNo != "")
                    builder.Append(" and ClientNo='" + ClientNo + "'");
                if (DestiOrgID != null && DestiOrgID != "")
                    builder.Append(" and WebLisOrgID='" + DestiOrgID + "'");

                reportFormFull = ibv.GetViewData(1, "_ReportFormFullDataSource", builder.ToString(), "");
                if (reportFormFull == null || reportFormFull.Tables[0].Rows.Count == 0)
                {
                    ZhiFang.Common.Log.Log.Info("DownloadReportByBarcodeNo.条件查不到数据：" + builder.ToString());
                    Error = "条件查不到数据：" + builder.ToString();
                    return false;
                }
                Log.Info("DownloadReportByBarcodeNo.ReportForm:" + reportFormFull.Tables[0].Rows.Count);
                if (reportFormFull != null && reportFormFull.Tables.Count > 0 && reportFormFull.Tables[0].Rows.Count > 0)
                {
                    //检测ReportFormFull是否对照
                    if (!ibrff.CheckReportFormCenter(reportFormFull, ClientNo, out str))
                    {
                        Error += str;
                        Log.Info("DownloadReportByBarcodeNo：Error1" + Error);
                        return false;
                    }
                    //转码过程
                    reportFormFull = MatchClientNo(reportFormFull, ClientNo);

                    int[] numArray = new int[1];
                    XmlDocument document = TransformDTRowIntoXML(DataTableHelper.ColumnNameToUpper(reportFormFull.Tables[0]), "WebReportFile", "ReportForm", numArray);

                    for (int index = 0; index < reportFormFull.Tables[0].Rows.Count; index++)
                    {
                        reportAllItemView.Clear();
                        reportAllItemView = ibv.GetViewData(-1, "_ReportItemFullDataSource", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                            {
                                Error += str;
                                Log.Info("DownloadReportByBarcodeNo：Error2" + Error);
                                return false;
                            }
                            reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                            //ZhiFang.Common.Log.Log.Debug("reportAllItemView->xml:" + reportAllItemView.GetXml());
                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportItem"), "WebReportFile");
                            WebReportXML = document.InnerXml;
                            Log.Info("DownloadReportByBarcodeNo.ReportItem：WebReportXML" + WebReportXML);
                            return true;
                        }
                        Log.Info("ReportItem:" + reportFormFull.Tables[0].Rows.Count);
                        reportAllItemView.Clear();
                        reportAllItemView = ibv.GetViewData(-1, "_ReportMicroFullDataSource", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                            {
                                Error += str;
                                Log.Info("DownloadReportByBarcodeNo：Error3" + Error);
                                return false;
                            }
                            reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMicro"), "WebReportFile");
                            WebReportXML = document.InnerXml;
                            Log.Info("DownloadReportByBarcodeNo.ReportMicro：WebReportXML" + WebReportXML);
                            return true;
                        }
                        Log.Info("ReportMicro:" + reportFormFull.Tables[0].Rows.Count);
                        reportAllItemView.Clear();
                        reportAllItemView = ibv.GetViewData(-1, "_ReportMarrowFullDataSource", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                            {
                                Error += str;
                                Log.Info("DownloadReportByBarcodeNo：Error4" + Error);
                                return false;
                            }
                            reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMarrow"), "WebReportFile");
                            WebReportXML = document.InnerXml;
                            Log.Info("DownloadReportByBarcodeNo.ReportMarrow：WebReportXML" + WebReportXML);
                            return true;
                        }
                        Log.Info("ReportMarrow:" + reportFormFull.Tables[0].Rows.Count);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Error = DateTime.Now + ":" + e.ToString() + e.StackTrace;
                Log.Info("错误信息：" + Error);
                return false;
            }

        }

        /// <summary>
        /// 报告下载服务，需要验证用户名和密码 
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="PassWord"></param>
        /// <param name="BarcodeNo"></param>
        /// <param name="ClientNo"></param>
        /// <param name="WebReportXML"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        [WebMethod(Description = "下载报告单ID列表")]
        public bool DownloadReportFormIDListByBarcodeNo(string Account, string PassWord, string BarcodeNo, string ClientNo, string DestiOrgID, out string[] ReportFormIDList, out string Error)
        {
            Log.Info("DownloadReportFormIDListByBarcodeNo");
            ReportFormIDList = new string[0];
            Error = "";
            string str = "";
            try
            {
                string user = "";
                string pwd = "";
                if (System.Configuration.ConfigurationManager.AppSettings["Account"] != null && System.Configuration.ConfigurationManager.AppSettings["Account"].Trim() != "")
                {
                    user = System.Configuration.ConfigurationManager.AppSettings["Account"].Trim();
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Account未配置！");
                }
                if (System.Configuration.ConfigurationManager.AppSettings["PassWord"] != null && System.Configuration.ConfigurationManager.AppSettings["PassWord"].Trim() != "")
                {
                    pwd = System.Configuration.ConfigurationManager.AppSettings["PassWord"].Trim();
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("PassWord未配置！");
                }
                if (user != Account && pwd != PassWord)
                {
                    ZhiFang.Common.Log.Log.Info("用户和密码：" + Account + " " + PassWord);
                    Error = "用户名或者密码错误！";
                    return false;
                }

                DataSet reportFormFull = new DataSet();
                DataSet reportAllItemView = new DataSet();
                StringBuilder builder = new StringBuilder();
                builder.Append(" 1=1 ");
                if (BarcodeNo != null && BarcodeNo != "")
                    builder.Append(" and SERIALNO='" + BarcodeNo + "'");
                if (ClientNo != null && ClientNo != "")
                    builder.Append(" and ClientNo='" + ClientNo + "'");
                if (DestiOrgID != null && DestiOrgID != "")
                    builder.Append(" and WebLisOrgID='" + DestiOrgID + "'");

                reportFormFull = ibv.GetViewData(-1, "_ReportFormFullDataSource", builder.ToString(), "");
                if (reportFormFull == null || reportFormFull.Tables[0].Rows.Count == 0)
                {
                    ZhiFang.Common.Log.Log.Info("条件查不到数据：" + builder.ToString());
                    Error = "条件查不到数据：" + builder.ToString();
                    return false;
                }
                Log.Info("DownloadReportFormIDListByBarcodeNo.ReportForm.Count:" + reportFormFull.Tables[0].Rows.Count);
                if (((reportFormFull != null) && (reportFormFull.Tables.Count > 0)) && (reportFormFull.Tables[0].Rows.Count > 0))
                {
                    ReportFormIDList = new string[reportFormFull.Tables[0].Rows.Count];
                    for (int index = 0; index < reportFormFull.Tables[0].Rows.Count; index++)
                    {
                        ReportFormIDList[index] = reportFormFull.Tables[0].Rows[index]["ReportFormID"].ToString();
                    }
                    Log.Info("DownloadReportFormIDListByBarcodeNo.ReportFormIDList:" + string.Join(",", ReportFormIDList));
                }

                return true;
            }
            catch (Exception e)
            {
                Error = DateTime.Now + ":" + e.ToString() + e.StackTrace;
                Log.Info("错误信息：" + Error);
                return false;
            }

        }

        /// <summary>
        /// 报告下载服务，需要验证用户名和密码 
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="PassWord"></param>
        /// <param name="BarcodeNo"></param>
        /// <param name="ClientNo"></param>
        /// <param name="WebReportXML"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        [WebMethod(Description = "下载报告单ID列表")]
        public bool DownloadReportFormIDListByClientBarcodeNo(string Account, string PassWord, string BarcodeNo, string ClientNo, string DestiOrgID, out string[] ReportFormIDList, out string Error)
        {
            Log.Info("DownloadReportFormIDListByBarcodeNo");
            ReportFormIDList = new string[0];
            Error = "";
            string str = "";
            try
            {
                string user = "";
                string pwd = "";
                if (System.Configuration.ConfigurationManager.AppSettings["Account"] != null && System.Configuration.ConfigurationManager.AppSettings["Account"].Trim() != "")
                {
                    user = System.Configuration.ConfigurationManager.AppSettings["Account"].Trim();
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("DownloadReportFormIDListByClientBarcodeNo.Account未配置！");
                }
                if (System.Configuration.ConfigurationManager.AppSettings["PassWord"] != null && System.Configuration.ConfigurationManager.AppSettings["PassWord"].Trim() != "")
                {
                    pwd = System.Configuration.ConfigurationManager.AppSettings["PassWord"].Trim();
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("DownloadReportFormIDListByClientBarcodeNo.PassWord未配置！");
                }
                if (user != Account && pwd != PassWord)
                {
                    ZhiFang.Common.Log.Log.Info("DownloadReportFormIDListByClientBarcodeNo.用户和密码：" + Account + " " + PassWord);
                    Error = "用户名或者密码错误！";
                    return false;
                }

                DataSet reportFormFull = new DataSet();
                DataSet reportAllItemView = new DataSet();
                StringBuilder builder = new StringBuilder();
                builder.Append(" 1=1 ");
                if (BarcodeNo != null && BarcodeNo != "")
                    builder.Append(" and OldSerialno='" + BarcodeNo + "'");
                else
                {
                    ZhiFang.Common.Log.Log.Info("DownloadReportFormIDListByClientBarcodeNo.BarcodeNo为空!");
                    Error = "BarcodeNo不能为空！";
                    return false;
                }
                if (ClientNo != null && ClientNo != "")
                    builder.Append(" and ClientNo='" + ClientNo + "'");
                else
                {
                    ZhiFang.Common.Log.Log.Info("DownloadReportFormIDListByClientBarcodeNo.ClientNo为空!");
                    Error = "ClientNo不能为空！";
                    return false;
                }
                if (DestiOrgID != null && DestiOrgID != "")
                    builder.Append(" and WebLisOrgID='" + DestiOrgID + "'");

                reportFormFull = ibv.GetViewData(-1, "_ReportFormFullDataSource", builder.ToString(), "");
                if (reportFormFull == null || reportFormFull.Tables[0].Rows.Count == 0)
                {
                    ZhiFang.Common.Log.Log.Info("DownloadReportFormIDListByClientBarcodeNo.条件查不到数据：" + builder.ToString());
                    Error = "条件查不到数据：" + builder.ToString();
                    return false;
                }
                Log.Info("DownloadReportFormIDListByClientBarcodeNo.ReportForm.Count:" + reportFormFull.Tables[0].Rows.Count);
                if (((reportFormFull != null) && (reportFormFull.Tables.Count > 0)) && (reportFormFull.Tables[0].Rows.Count > 0))
                {
                    ReportFormIDList = new string[reportFormFull.Tables[0].Rows.Count];
                    for (int index = 0; index < reportFormFull.Tables[0].Rows.Count; index++)
                    {
                        ReportFormIDList[index] = reportFormFull.Tables[0].Rows[index]["ReportFormID"].ToString();
                    }
                    Log.Info("DownloadReportFormIDListByClientBarcodeNo.ReportFormIDList:" + string.Join(",", ReportFormIDList));
                }

                return true;
            }
            catch (Exception e)
            {
                Error = DateTime.Now + ":" + e.ToString() + e.StackTrace;
                Log.Info("DownloadReportFormIDListByClientBarcodeNo.错误信息：" + Error);
                return false;
            }

        }

        /// <summary>
        /// 报告下载服务，需要验证用户名和密码 
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="PassWord"></param>
        /// <param name="ReportFormID"></param>
        /// <param name="ClientNo"></param>
        /// <param name="WebReportXML"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        [WebMethod(Description = "下载报告单")]
        public bool DownloadReportByReportFormID(string Account, string PassWord, string ReportFormID, string ClientNo, string DestiOrgID, out string WebReportXML, out string Error)
        {
            Log.Info("DownloadReportFormIDListByBarcodeNo");
            WebReportXML = "";
            Error = "";
            string str = "";
            try
            {
                string user = "";
                string pwd = "";
                if (System.Configuration.ConfigurationManager.AppSettings["Account"] != null && System.Configuration.ConfigurationManager.AppSettings["Account"].Trim() != "")
                {
                    user = System.Configuration.ConfigurationManager.AppSettings["Account"].Trim();
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("Account未配置！");
                }
                if (System.Configuration.ConfigurationManager.AppSettings["PassWord"] != null && System.Configuration.ConfigurationManager.AppSettings["PassWord"].Trim() != "")
                {
                    pwd = System.Configuration.ConfigurationManager.AppSettings["PassWord"].Trim();
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("PassWord未配置！");
                }
                if (user != Account && pwd != PassWord)
                {
                    ZhiFang.Common.Log.Log.Info("用户和密码：" + Account + " " + PassWord);
                    Error = "用户名或者密码错误！";
                    return false;
                }

                DataSet reportFormFull = new DataSet();
                DataSet reportAllItemView = new DataSet();
                StringBuilder builder = new StringBuilder();
                builder.Append(" 1=1 ");
                if (ReportFormID != null && ReportFormID != "")
                    builder.Append(" and ReportFormID='" + ReportFormID + "'");
                if (ClientNo != null && ClientNo != "")
                    builder.Append(" and ClientNo='" + ClientNo + "'");
                if (DestiOrgID != null && DestiOrgID != "")
                    builder.Append(" and WebLisOrgID='" + DestiOrgID + "'");

                reportFormFull = ibv.GetViewData(1, "_ReportFormFullDataSource", builder.ToString(), "");
                if (reportFormFull == null || reportFormFull.Tables[0].Rows.Count == 0)
                {
                    ZhiFang.Common.Log.Log.Info("条件查不到数据：" + builder.ToString());
                    Error = "条件查不到数据：" + builder.ToString();
                    return false;
                }
                Log.Info("ReportForm:" + reportFormFull.Tables[0].Rows.Count);

                if (((reportFormFull != null) && (reportFormFull.Tables.Count > 0)) && (reportFormFull.Tables[0].Rows.Count > 0))
                {
                    //检测ReportFormFull是否对照
                    if (!ibrff.CheckReportFormCenter(reportFormFull, ClientNo, out str))
                    {
                        Error += str;
                        return false;
                    }
                    //转码过程
                    reportFormFull = MatchClientNo(reportFormFull, ClientNo);

                    int[] numArray = new int[1];
                    XmlDocument document = TransformDTRowIntoXML(DataTableHelper.ColumnNameToUpper(reportFormFull.Tables[0]), "WebReportFile", "ReportForm", numArray);
                    for (int index = 0; index < reportFormFull.Tables[0].Rows.Count; index++)
                    {
                        reportAllItemView.Clear();
                        reportAllItemView = ibv.GetViewData(-1, "_ReportItemFullDataSource", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                            {
                                Error += str;
                                return false;
                            }
                            reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                            //ZhiFang.Common.Log.Log.Debug("reportAllItemView->xml:" + reportAllItemView.GetXml());
                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportItem"), "WebReportFile");
                            WebReportXML = document.InnerXml;
                            Log.Info("ReportItem:" + reportAllItemView.Tables[0].Rows.Count);
                            return true;
                        }
                       
                        reportAllItemView.Clear();
                        reportAllItemView = ibv.GetViewData(-1, "_ReportMicroFullDataSource", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                            {
                                Error += str;
                                return false;
                            }
                            reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMicro"), "WebReportFile");
                            WebReportXML = document.InnerXml;
                            Log.Info("ReportMicro:" + reportAllItemView.Tables[0].Rows.Count);
                            return true;
                        }
                        
                        reportAllItemView.Clear();
                        reportAllItemView = ibv.GetViewData(-1, "_ReportMarrowFullDataSource", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            //if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                            //{
                            //    Error += str;
                            //    return false;
                            //}
                            //reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMarrow"), "WebReportFile");
                            WebReportXML = document.InnerXml;
                            Log.Info("ReportMarrow:" + reportAllItemView.Tables[0].Rows.Count);
                            return true;
                        }
                        
                    }
                }
                ZhiFang.Common.Log.Log.Debug("DownloadReportByReportFormID.WebReportXML:" + WebReportXML);
                return true;
            }
            catch (Exception e)
            {
                Error = DateTime.Now + ":" + e.ToString() + e.StackTrace;
                Log.Info("错误信息：" + Error);
                return false;
            }

        }


        [WebMethod(Description = "下载报告(博尔诚医学检验所)身份证号、病历号")]
        public bool DownloadReportByPersonID(string Account, string PassWord, string[] PersonID, string ClientNo, out string[] WebReportXML, out string[] ReportFormImageList, out string Error)
        {
            Log.Info("DownloadReportByPersonID:PersonID=" + string.Join(",", PersonID));
            WebReportXML = null;
            List<string> listReport = new List<string>();
            string ReportXML;
            Error = "";
            string str = "";
            ReportFormImageList = new string[PersonID.Length];
            try
            {
                string user = ConfigHelper.GetConfigString("Account");
                string pwd = ConfigHelper.GetConfigString("PassWord");
                if (user != Account && pwd != PassWord)
                {
                    ZhiFang.Common.Log.Log.Info("用户和密码：" + Account + " " + PassWord);
                    Error = "用户名或者密码错误！";
                    return false;
                }
                #region 遍历身份证号
                for (int i = 0; i < PersonID.Length; i++)
                {
                    ZhiFang.Common.Log.Log.Info("DownloadReportByPersonID:PersonID[" + i + "]：" + PersonID[i]);
                    ReportXML = "";
                    DataSet reportFormFull = new DataSet();
                    DataSet reportAllItemView = new DataSet();
                    StringBuilder sqlwhere = new StringBuilder();
                    sqlwhere.Append(" 1=1 ");
                    //if (PersonID[i] != null && PersonID[i] != "")
                    //    sqlwhere.Append(" and PersonID='" + PersonID[i] + "'");
                    if (PersonID[i] != null && PersonID[i] != "")
                        sqlwhere.Append(" and PatNo='" + PersonID[i] + "'");
                    if (ClientNo != null && ClientNo != "")
                        sqlwhere.Append(" and ClientNo='" + ClientNo + "'");


                    reportFormFull = ibv.GetViewData(-1, "_ReportFormFullDataSource_BoErCheng", sqlwhere.ToString(), "");
                    ZhiFang.Common.Log.Log.Info("DownloadReportByPersonID:PersonID[" + i + "]：" + PersonID[i] + ",builder=" + sqlwhere);


                    if (reportFormFull != null && reportFormFull.Tables.Count > 0 && reportFormFull.Tables[0].Rows.Count > 0)
                    {
                        //检测ReportFormFull是否对照
                        if (!ibrff.CheckReportFormCenter(reportFormFull, ClientNo, out str))
                        {
                            Error += str;
                            return false;
                        }
                        //转码过程
                        reportFormFull = MatchClientNo(reportFormFull, ClientNo);
                        #region
                        for (int index = 0; index < reportFormFull.Tables[0].Rows.Count; index++)
                        {
                            XmlDocument document = ZhiFang.BLL.Common.TransDataToXML.TransformDRIntoXML(reportFormFull.Tables[0].Rows[index], "WebReportFile", "ReportForm");

                            #region reportitem
                            reportAllItemView.Clear();
                            reportAllItemView = ibv.GetViewData(-1, "_ReportItemFullDataSource_BoErCheng", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                            if (reportAllItemView != null && reportAllItemView.Tables.Count > 0 && reportAllItemView.Tables[0].Rows.Count > 0)
                            {
                                //检测ReportItemFull是否对照
                                if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                                {
                                    Error += str;
                                    continue;
                                }
                                reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                                document = ZhiFang.BLL.Common.TransDataToXML.TransformDTInsertIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile/ReportForm", "ReportItem", document);
                                //document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(Table.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile/ReportForm", "ReportItem"), "WebReportFile");
                                ReportXML = document.InnerXml;

                            }
                            #endregion
                            #region reportmicro
                            reportAllItemView.Clear();
                            reportAllItemView = ibv.GetViewData(-1, "_ReportMicroFullDataSource_BoErCheng", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                            if (reportAllItemView != null && reportAllItemView.Tables.Count > 0 && reportAllItemView.Tables[0].Rows.Count > 0)
                            {
                                //检测ReportItemFull是否对照
                                if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                                {
                                    Error += str;
                                    continue;
                                }
                                reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                                document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMicro"), "WebReportFile");
                                ReportXML = document.InnerXml;

                            }
                            #endregion
                            #region reportmarrow
                            reportAllItemView.Clear();
                            reportAllItemView = ibv.GetViewData(-1, "_ReportMarrowFullDataSource_BoErCheng", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                            if (reportAllItemView != null && reportAllItemView.Tables.Count > 0 && reportAllItemView.Tables[0].Rows.Count > 0)
                            {
                                //检测ReportItemFull是否对照
                                if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                                {
                                    Error += str;
                                    continue;
                                }
                                reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                                document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMarrow"), "WebReportFile");
                                ReportXML = document.InnerXml;

                            }
                            #endregion
                            List<string> a = this.PrintHtml(new string[1] { reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString().Trim() }, "0", "JPG");//一个
                            if (a.Count > 0)
                            {
                                ReportFormImageList[i] = a[0];
                            }
                        }
                        #endregion
                        listReport.Add(ReportXML);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("条件查不到数据：" + sqlwhere.ToString());
                        Error = "条件查不到数据：" + sqlwhere.ToString();
                        continue;
                    }
                }
                #endregion
                if (listReport.Count > 0)
                    WebReportXML = listReport.ToArray();
                ZhiFang.Common.Log.Log.Info("返回的报告:" + string.Join(" ", WebReportXML));
                return true;
            }
            catch (Exception e)
            {
                Error = DateTime.Now + ":" + e.ToString() + e.StackTrace;
                Log.Info("错误信息：" + Error);
                return false;
            }

        }

        [WebMethod(Description = "下载报告(博尔诚医学检验所)条码号")]
        public bool DownloadReportByBarCode(string Account, string PassWord, string[] BarCode, string ClientNo, out string[] WebReportXML, out string[] ReportFormImageList, out string Error)
        {
            Log.Info("DownloadReportByBarCode:BarCode=" + string.Join(",", BarCode));
            WebReportXML = null;
            List<string> listReport = new List<string>();
            string ReportXML;
            Error = "";
            string str = "";
            ReportFormImageList = new string[BarCode.Length];
            try
            {
                string user = ConfigHelper.GetConfigString("Account");
                string pwd = ConfigHelper.GetConfigString("PassWord");
                if (user != Account && pwd != PassWord)
                {
                    ZhiFang.Common.Log.Log.Info("用户和密码：" + Account + " " + PassWord);
                    Error = "用户名或者密码错误！";
                    return false;
                }
                #region 遍历条码号
                for (int i = 0; i < BarCode.Length; i++)
                {
                    ZhiFang.Common.Log.Log.Info("DownloadReportByPersonID:PersonID[" + i + "]：" + BarCode[i]);
                    ReportXML = "";
                    DataSet reportFormFull = new DataSet();
                    DataSet reportAllItemView = new DataSet();
                    StringBuilder sqlwhere = new StringBuilder();
                    sqlwhere.Append(" 1=1 ");
                    if (BarCode[i] != null && BarCode[i] != "")
                        sqlwhere.Append(" and SERIALNO='" + BarCode[i] + "'");
                    if (ClientNo != null && ClientNo != "")
                        sqlwhere.Append(" and ClientNo='" + ClientNo + "'");


                    reportFormFull = ibv.GetViewData(-1, "_ReportFormFullDataSource_BoErCheng", sqlwhere.ToString(), "");
                    ZhiFang.Common.Log.Log.Info("DownloadReportByBarCode:BarCode[" + i + "]：" + BarCode[i] + ",builder=" + sqlwhere);


                    if (reportFormFull != null && reportFormFull.Tables.Count > 0 && reportFormFull.Tables[0].Rows.Count > 0)
                    {
                        //检测ReportFormFull是否对照
                        if (!ibrff.CheckReportFormCenter(reportFormFull, ClientNo, out str))
                        {
                            Error += str;
                            return false;
                        }
                        //转码过程
                        reportFormFull = MatchClientNo(reportFormFull, ClientNo);
                        #region
                        for (int index = 0; index < reportFormFull.Tables[0].Rows.Count; index++)
                        {
                            XmlDocument document = ZhiFang.BLL.Common.TransDataToXML.TransformDRIntoXML(reportFormFull.Tables[0].Rows[index], "WebReportFile", "ReportForm");

                            #region reportitem
                            reportAllItemView.Clear();
                            reportAllItemView = ibv.GetViewData(-1, "_ReportItemFullDataSource_BoErCheng", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                            if (reportAllItemView != null && reportAllItemView.Tables.Count > 0 && reportAllItemView.Tables[0].Rows.Count > 0)
                            {
                                //检测ReportItemFull是否对照
                                if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                                {
                                    Error += str;
                                    continue;
                                }
                                reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                                document = ZhiFang.BLL.Common.TransDataToXML.TransformDTInsertIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile/ReportForm", "ReportItem", document);
                                //document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(Table.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile/ReportForm", "ReportItem"), "WebReportFile");
                                ReportXML = document.InnerXml;

                            }
                            #endregion
                            #region reportmicro
                            reportAllItemView.Clear();
                            reportAllItemView = ibv.GetViewData(-1, "_ReportMicroFullDataSource_BoErCheng", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                            if (reportAllItemView != null && reportAllItemView.Tables.Count > 0 && reportAllItemView.Tables[0].Rows.Count > 0)
                            {
                                //检测ReportItemFull是否对照
                                if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                                {
                                    Error += str;
                                    continue;
                                }
                                reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                                document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMicro"), "WebReportFile");
                                ReportXML = document.InnerXml;

                            }
                            #endregion
                            #region reportmarrow
                            reportAllItemView.Clear();
                            reportAllItemView = ibv.GetViewData(-1, "_ReportMarrowFullDataSource_BoErCheng", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                            if (reportAllItemView != null && reportAllItemView.Tables.Count > 0 && reportAllItemView.Tables[0].Rows.Count > 0)
                            {
                                //检测ReportItemFull是否对照
                                if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                                {
                                    Error += str;
                                    continue;
                                }
                                reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                                document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMarrow"), "WebReportFile");
                                ReportXML = document.InnerXml;

                            }
                            #endregion

                            List<string> a = this.PrintHtml(new string[1] { reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString().Trim() }, "0", "JPG");//一个条码一个报告
                            if (a.Count > 0)
                            {
                                ReportFormImageList[i] = @"http:\\" + System.Web.HttpContext.Current.Request.Url.Host + System.Web.HttpContext.Current.Request.ApplicationPath + "\\" + a[0];//不存在多个报告图片的情况，只返回第一个。
                            }
                            //this.PrintHtml(new string[1] { reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString().Trim() }, "0", "JPG");
                        }
                        #endregion
                        listReport.Add(ReportXML);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("条件查不到数据：" + sqlwhere.ToString());
                        Error = "条件查不到数据：" + sqlwhere.ToString();
                        continue;
                    }
                }
                #endregion
                if (listReport.Count > 0)
                    WebReportXML = listReport.ToArray();
                ZhiFang.Common.Log.Log.Info("返回的报告:" + string.Join(" ", WebReportXML));
                return true;
            }
            catch (Exception e)
            {
                Error = DateTime.Now + ":" + e.ToString() + e.StackTrace;
                Log.Info("错误信息：" + Error);
                return false;
            }

        }

        public List<string> PrintHtml(string[] ReportFormID, string title, string reportformfiletype)
        {
            IBPrintReportForm ibprf = BLLFactory<IBPrintReportForm>.GetBLL("Print.PrintReportForm");
            List<string> tmp;
            List<string> tmplist = new List<string>();
            tmp = ibprf.PrintReportFormHtml(ReportFormID.ToList<string>(), (Common.Dictionary.ReportFormTitle)Convert.ToInt32(title), (Common.Dictionary.ReportFormFileType)Enum.Parse(typeof(Common.Dictionary.ReportFormFileType), reportformfiletype));
            if (tmp != null)
            {
                for (int j = 0; j < tmp.Count; j++)
                {
                    tmplist.Add(tmp[j]);
                }
            }

            List<string> tmphtml = new List<string>();
            string appurl = System.Web.HttpContext.Current.Request.ApplicationPath.ToString();
            string applocalroot = System.AppDomain.CurrentDomain.BaseDirectory;//获取程序根目录
            //string imagesurl2 = imagesurl1.Replace(tmpRootDir, ""); //转换成相对路径
            //imagesurl2 = imagesurl2.Replace(@"\", @"/");
            //return imagesurl2;

            for (int i = 0; i < tmplist.Count; i++)
            {
                tmphtml.Add(tmplist[i].Replace(applocalroot, "").Replace(@"\", @"/"));
            }
            return tmphtml;
        }

        [WebMethod(Description = "下载pdf报告芜湖定制serialno为唯一条码")]
        public string retrieveDocumentViewInfo(
            string documentId
            )
        {
            string Files = "";
            string ErrorMsg = "";
            //根据documentId获取文件名称
            string fileName = "";
            string url = "";
            Log.Info("条码号:" + documentId);
            if (string.IsNullOrEmpty(documentId))
            {
                return "Error+" + "条码号不能位空";
            }
            rf.SERIALNO = documentId;
            DataSet ds = ibrff.GetList(rf);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string date = Convert.ToDateTime(ds.Tables[0].Rows[0]["RECEIVEDATE"]).Year.ToString() + "\\" + Convert.ToDateTime(ds.Tables[0].Rows[0]["RECEIVEDATE"]).Month.ToString() + "\\" + Convert.ToDateTime(ds.Tables[0].Rows[0]["RECEIVEDATE"]).Day.ToString();
                string fileNameUnique = date + "\\" + ds.Tables[0].Rows[0]["REPORTFORMID"].ToString().Replace(":", "：");
                fileName = fileNameUnique + ".pdf";

                ErrorMsg = "Error+";
                try
                {
                    //
                    url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("jpgorpdf");
                    Log.Info("路径+文件名:" + url + fileName);
                    FileStream fs = new FileStream(url + fileName, FileMode.Open);//可以是其他重载方法 
                    byte[] byData = new byte[fs.Length];
                    fs.Read(byData, 0, byData.Length);
                    fs.Close();
                    Files = Convert.ToBase64String(byData);
                    return Files;
                }
                catch (Exception ex)
                {
                    Files = null;
                    ErrorMsg += ex.ToString();
                    Log.Error("错误信息:" + ex);
                    return ErrorMsg;
                }
            }
            else
            {
                Files = null;
                ErrorMsg = "没有找到“" + documentId + "”此条码号的文件";
                Log.Info("没有找到“" + documentId + "”此条码号的文件");
                return "Error+" + ErrorMsg;
            }

        }


        [WebMethod(Description = "查询同一条码多张报告的记录")]
        public bool QueryReportsCount(
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string BarCodeNo,               //条码号(多个)
            out string BarCodeCounts,     //返回有多张报告的条码号
            out string ReturnDescription)   //其他描述
        {
            ReturnDescription = "";
            BarCodeCounts = "";
            string sqlReport = "";
            try
            {
                if (BarCodeNo != null && BarCodeNo.Trim() != "")
                {
                    DataSet dsreportForm = ibrff.GetBarCode(DestiOrgID, BarCodeNo);
                    if (dsreportForm != null && dsreportForm.Tables.Count > 0 && dsreportForm.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsreportForm.Tables[0].Rows)
                        {
                            BarCodeCounts = BarCodeCounts + "'" + dr["barcode"].ToString() + "',";
                        }
                        if (BarCodeCounts.Length > 0 && BarCodeCounts.EndsWith(","))
                        {
                            BarCodeCounts = BarCodeCounts.TrimEnd(',');
                        }
                        return true;
                    }
                    else
                    {
                        ReturnDescription = "没有找到同一条码有两张报告的记录:" + BarCodeNo;
                        Log.Info("QueryReportsCount查询同一条码多张报告的记录" + sqlReport + "," + ReturnDescription);
                        return false;
                    }

                }
                else
                {
                    ReturnDescription = "条码号不能为空:" + BarCodeNo;
                    return false;
                }
            }
            catch (Exception ex)
            {
                ReturnDescription = ex.Message;
                Log.Info("QueryReportsCount查询同一条码多张报告的记录:" + sqlReport + "," + ex.Message.ToString());
                return false;
            }
        }

        /// <summary>
        /// 上传检验报告
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="xmlData">xml数据</param>
        /// <param name="pdfdata">pdf检验报告</param>
        /// <param name="pdfdata_td">套打pdf检验报告</param>
        /// <param name="fileData">其他文件，例如jpg,frp，word,rtf等</param>
        /// <param name="fileType">其他文件的类型</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod(Description = "上传检验报告（通用，此版本必须升级数据库到最新版本，3月27日添加了字段验证）")]
        public int UpLoadReportFromBytes(string token, byte[] xmlData, byte[] pdfdata, byte[] pdfdata_td, byte[] fileData, string fileType, out string errorMsg)
        {
            try
            {
                errorMsg = "";
                System.Text.UTF8Encoding converter = new UTF8Encoding();
                string xml = converter.GetString(xmlData);
                XmlDocument doc = new XmlDocument();
                Log.Debug(xml);
                doc.LoadXml(xml);

                //验证报告字段是否规范
                if (!PerifyField(doc))
                    return -1;

                StringReader strTemp = new StringReader(xml);
                DataSet ds = new DataSet();
                ds.ReadXml(strTemp);
                string str = "";
                string transcoding = ZhiFang.Common.Public.ConfigHelper.GetConfigString("transcoding");
                string orgID = ds.Tables[0].Rows[0][transcoding].ToString();
                Log.Info("按照“" + transcoding + "”进行转码,单位编码为" + orgID);

                DataTable dtReportForm = ds.Tables[0];
                DataTable dtForm = dtReportForm.Copy();
                DataSet wsReportForm = new DataSet();
                //Log.Info("bytes0");
                wsReportForm.Tables.Add(dtForm);
                //Log.Info("bytes1");
                if (!ibrd.CheckLabNo(wsReportForm, orgID, out str))
                {
                    // Log.Info("bytes2");
                    errorMsg += str;
                    return -1;
                }
                //Log.Info("bytes3");
                DataTable dtReportItem = ds.Tables[1];
                DataTable dtItem = dtReportItem.Copy();
                DataSet wsReportItem = new DataSet();
                //Log.Info("bytes4");
                wsReportItem.Tables.Add(dtItem);
                //Log.Info("bytes5");
                if (!ibrd.CheckLabNo(wsReportItem, orgID, out str))
                {
                    // Log.Info("bytes6");
                    errorMsg += str;
                    return -1;
                }
                //Log.Info("bytes7");
                wsReportForm = MatchCenterNo(wsReportForm, orgID);
                wsReportItem = MatchCenterNo(wsReportItem, orgID);
                DataSet dsNew = new DataSet();
                DataTable dtNewForm = wsReportForm.Tables[0].Copy();
                DataTable dtNewItem = wsReportItem.Tables[0].Copy();
                dsNew.Tables.Add(dtNewForm);
                dsNew.Tables.Add(dtNewItem);

                string xmlNew = dsNew.GetXml();
                //Log.Info(xmlNew);
                xmlNew.Replace("NewDataSet", "WebReportFile");
                byte[] xmlDataNew = converter.GetBytes(xmlNew);
                long OutReportformIndexID;
                int flag = ibrd.UpLoadReportDataFromBytes(xmlDataNew, pdfdata, pdfdata_td, fileData, fileType, out errorMsg, out OutReportformIndexID);
                //for (int i = 0; i < dtNewForm.Columns.Count; i++)
                //{
                //    ZhiFang.Common.Log.Log.Debug(dtNewForm.Columns[i].ColumnName);
                //}
                #region WeiXin
                if (flag == 0)
                {
                    if (ConfigHelper.GetConfigString("IsUploadWeiXinService").Trim() == "1")
                    {
                        WeblisService.ReportFormWebService.ReportFormWebService rfws = new WeblisService.ReportFormWebService.ReportFormWebService();
                        rfws.UpLoadReportFromBytes(token, xmlData, pdfdata, pdfdata_td, fileData, fileType, out errorMsg);
                    }
                    else
                    {
                        if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("SendWeiXinService").Trim() == "1")
                        {
                            WeiXinReportFormService.IReportFormWeiXinService wxrfs = new WeiXinReportFormService.ReportFormWeiXinServiceClient();
                            WeiXinReportFormService.BSearchAccountReportForm entity = new WeiXinReportFormService.BSearchAccountReportForm();
                            if (dtNewForm.Columns.Contains("CNAME") && dtNewForm.Rows[0]["CNAME"].ToString().Trim() != "")
                            {
                                entity.Name = dtNewForm.Rows[0]["CNAME"].ToString();
                            }
                            if (dtNewForm.Columns.Contains("PATNO"))
                            {
                                entity.PatNo = dtNewForm.Rows[0]["PATNO"].ToString();
                            }
                            if (dtNewForm.Columns.Contains("AGE") && dtNewForm.Rows[0]["AGE"].ToString().Trim() != "")
                            {
                                entity.Age = int.Parse(dtNewForm.Rows[0]["AGE"].ToString());
                                if (dtNewForm.Columns.Contains("AGEUNITNAME"))
                                {
                                    entity.AgeUnit = dtNewForm.Rows[0]["AGEUNITNAME"].ToString();
                                }
                            }
                            if (dtNewForm.Columns.Contains("GENDERNAME") && dtNewForm.Rows[0]["GENDERNAME"].ToString().Trim() != "")
                            {
                                entity.Sex = dtNewForm.Rows[0]["GENDERNAME"].ToString();
                            }

                            if (dtNewForm.Columns.Contains("COLLECTDATE") && dtNewForm.Rows[0]["COLLECTDATE"].ToString().Trim() != "")
                            {
                                entity.COLLECTDATE = Convert.ToDateTime(dtNewForm.Rows[0]["COLLECTDATE"].ToString());
                            }

                            if (dtNewForm.Columns.Contains("CHECKDATE") && dtNewForm.Rows[0]["CHECKDATE"].ToString().Trim() != "")
                            {
                                entity.ReportFormTime = Convert.ToDateTime(dtNewForm.Rows[0]["CHECKDATE"].ToString());
                            }
                            if (dtNewForm.Columns.Contains("BARCODE") && dtNewForm.Rows[0]["BARCODE"].ToString().Trim() != "")
                            {
                                entity.Barcode = dtNewForm.Rows[0]["BARCODE"].ToString();
                            }
                            else
                            {
                                if (dtNewForm.Columns.Contains("SERIALNO") && dtNewForm.Rows[0]["SERIALNO"].ToString().Trim() != "")
                                {
                                    entity.Barcode = dtNewForm.Rows[0]["SERIALNO"].ToString();
                                }
                            }

                            if (dtNewForm.Columns.Contains("MOBILECODE") && dtNewForm.Rows[0]["MOBILECODE"].ToString().Trim() != "")
                            {
                                entity.MobileCode = dtNewForm.Rows[0]["MOBILECODE"].ToString();
                            }
                            if (dtNewForm.Columns.Contains("IDNUMBER") && dtNewForm.Rows[0]["IDNUMBER"].ToString().Trim() != "")
                            {
                                entity.IDNumber = dtNewForm.Rows[0]["IDNUMBER"].ToString();
                            }
                            if (dtNewForm.Columns.Contains("MEDICARE") && dtNewForm.Rows[0]["MEDICARE"].ToString().Trim() != "")
                            {
                                entity.MediCare = dtNewForm.Rows[0]["MEDICARE"].ToString();
                            }
                            if (dtNewForm.Columns.Contains("VISNO") && dtNewForm.Rows[0]["VISNO"].ToString().Trim() != "")
                            {
                                entity.VisNo = dtNewForm.Rows[0]["VISNO"].ToString();
                            }
                            if (dtNewForm.Columns.Contains("TAKENO") && dtNewForm.Rows[0]["TAKENO"].ToString().Trim() != "")
                            {
                                entity.TakeNo = dtNewForm.Rows[0]["TAKENO"].ToString();
                            }
                            if (dtNewForm.Columns.Contains("CLIENTNO") && dtNewForm.Rows[0]["CLIENTNO"].ToString().Trim() != "")
                            {
                                entity.HospitalCode = dtNewForm.Rows[0]["CLIENTNO"].ToString();
                            }
                            else
                            {
                                if (dtNewForm.Columns.Contains("WEBLISSOURCEORGID") && dtNewForm.Rows[0]["WEBLISSOURCEORGID"].ToString().Trim() != "")
                                {
                                    entity.HospitalCode = dtNewForm.Rows[0]["WEBLISSOURCEORGID"].ToString();
                                }
                            }
                            if (dtNewForm.Columns.Contains("CLIENTNAME") && dtNewForm.Rows[0]["CLIENTNAME"].ToString().Trim() != "")
                            {
                                entity.HospitalName = dtNewForm.Rows[0]["CLIENTNAME"].ToString();
                            }
                            else
                            {
                                if (dtNewForm.Columns.Contains("WEBLISSOURCEORGNAME") && dtNewForm.Rows[0]["WEBLISSOURCEORGNAME"].ToString().Trim() != "")
                                {
                                    entity.HospitalName = dtNewForm.Rows[0]["WEBLISSOURCEORGNAME"].ToString();
                                }
                            }
                            int isectiontype = 0;
                            if (dtNewForm.Columns.Contains("SECTIONTYPE") && dtNewForm.Rows[0]["SECTIONTYPE"].ToString().Trim() != "")
                            {
                                isectiontype = Convert.ToInt32(dtNewForm.Rows[0]["SECTIONTYPE"].ToString().Trim());
                            }
                            switch ((SectionType)isectiontype)
                            {
                                case SectionType.Normal:
                                    entity.ReportFormType = "Normal";
                                    break;
                                case SectionType.Micro:
                                    entity.ReportFormType = "Micro";
                                    break;
                                case SectionType.NormalIncImage:
                                    entity.ReportFormType = "NormalIncImage";
                                    break;
                                case SectionType.MicroIncImage:
                                    entity.ReportFormType = "MicroIncImage";
                                    break;
                                case SectionType.CellMorphology:
                                    entity.ReportFormType = "CellMorphology";
                                    break;
                                case SectionType.FishCheck:
                                    entity.ReportFormType = "FishCheck";
                                    break;
                                case SectionType.SensorCheck:
                                    entity.ReportFormType = "SensorCheck";
                                    break;
                                case SectionType.ChromosomeCheck:
                                    entity.ReportFormType = "ChromosomeCheck";
                                    break;
                                case SectionType.PathologyCheck:
                                    entity.ReportFormType = "PathologyCheck";
                                    break;
                                default:
                                    entity.ReportFormType = "all";
                                    break;
                            }
                            string pitem = "";
                            if (dtNewItem.Columns.Contains("MICRONO"))
                            {
                                for (int i = 0; i < dtNewItem.Rows.Count; i++)
                                {
                                    ZhiFang.Common.Log.Log.Debug("ITEMNAME:" + dtNewItem.Rows[i]["ITEMNAME"].ToString().Trim());
                                    if (pitem.IndexOf(dtNewItem.Rows[i]["ITEMNAME"].ToString().Trim()) < 0)
                                    {
                                        pitem += dtNewItem.Rows[i]["ITEMNAME"].ToString().Trim() + ",";
                                    }
                                }
                                entity.PItemName = pitem.Remove(pitem.Length - 1);
                            }
                            else
                            {
                                if (dtNewItem.Columns.Contains("MARROWNUM"))
                                {
                                }
                                else
                                {
                                    for (int i = 0; i < dtNewItem.Rows.Count; i++)
                                    {
                                        ZhiFang.Common.Log.Log.Debug("PARITEMNAME:" + dtNewItem.Rows[i]["PARITEMNAME"].ToString().Trim());
                                        if (pitem.IndexOf(dtNewItem.Rows[i]["PARITEMNAME"].ToString().Trim()) < 0)
                                        {
                                            pitem += dtNewItem.Rows[i]["PARITEMNAME"].ToString().Trim() + ",";
                                        }
                                        ZhiFang.Common.Log.Log.Debug("pitem:" + pitem.Trim());
                                    }
                                    ZhiFang.Common.Log.Log.Debug("pitem@@@@@@@@@@@@@@@@@@@@@@:" + pitem.Trim());
                                    entity.PItemName = pitem.Remove(pitem.Length - 1);
                                }
                            }
                            entity.ReportFormIndexID = OutReportformIndexID;
                            long weixinrfid = wxrfs.UpLoadRF(entity);
                            //ZhiFang.Common.Log.Log.Debug("微信报告单ID："+weixinrfid);
                        }
                    }
                }
                #endregion

                #region 生成报告单

                //if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("isGeneralReport").Trim() == "1")
                //{
                //    PrintService.ReportFormWebService printservice = new PrintService.ReportFormWebService();
                //    bool b = printservice.GenerationReportPrint(dtNewForm.Rows[0]["FORMNO"].ToString(), "1", "JPG", "1");
                //    if (b == true)
                //    {
                //        Log.Info("报告单生成成功!");
                //    }
                //    else
                //        Log.Info("报告单生成失败!");
                //}
                #endregion
                return flag;
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString() + "---------" + e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                errorMsg = "保存数据错误！" + e.ToString();
                return -1;
            }
        }

        /// <summary>
        /// 上传检验报告
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="xmlData">xml数据</param>
        /// <param name="pdfdata">pdf检验报告</param>
        /// <param name="pdfdata_td">套打pdf检验报告</param>
        /// <param name="fileData">其他文件，例如jpg,frp，word,rtf等</param>
        /// <param name="fileType">其他文件的类型</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod(Description = "上传检验报告（通用，此版本必须升级数据库到最新版本，3月27日添加了字段验证）")]
        public int UpLoadReportFromStr(string token, string xmlData, byte[] pdfdata, byte[] pdfdata_td, byte[] fileData, string fileType, out string errorMsg)
        {
            try
            {
                errorMsg = "";
                System.Text.UTF8Encoding converter = new UTF8Encoding();
                string xml = xmlData;
                if (xml != null && xml.Trim() != "")
                {
                    return UpLoadReportFromBytes(token, converter.GetBytes(xml), pdfdata, pdfdata_td, fileData, fileType, out errorMsg);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Debug("UpLoadReportFromStr.xmlData为空！" + DateTime.Now.ToString("yyMMDD hhmmss"));
                    errorMsg = "保存数据错误！参数xmlData为空！";
                    return -1;
                }
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString() + "---------" + e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                errorMsg = "保存数据错误！" + e.ToString();
                return -1;
            }
        }

        /// <summary>
        /// 上传检验报告
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="xmlData">xml数据</param>
        /// <param name="pdfdata">pdf检验报告</param>
        /// <param name="pdfdata_td">套打pdf检验报告</param>
        /// <param name="fileData">其他文件，例如jpg,frp，word,rtf等</param>
        /// <param name="fileType">其他文件的类型</param>
        /// <param name="errorMsg">错误信息</param>
        /// <param name="checkclient">转码</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod(Description = "上传检验报告（传clientno）")]
        //北京中日友好医院XML报告上传(手动传CLientNo)定制版本 接口
        public int UpLoadReportFromBytesNew(string token, byte[] xmlData, byte[] pdfdata, byte[] pdfdata_td, byte[] fileData, string fileType, out string errorMsg, string checkclient)
        {
            errorMsg = "";
            System.Text.UTF8Encoding converter = new UTF8Encoding();
            string xml = converter.GetString(xmlData);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            StringReader strTemp = new StringReader(xml);
            DataSet ds = new DataSet();
            ds.ReadXml(strTemp);
            string orgID = "";
            if (checkclient == null || checkclient == "")
                orgID = ds.Tables[0].Rows[0]["ClientNO"].ToString();
            else
                orgID = checkclient;

            return classUpLoadReportFromBytesfengzhuang(token, ds, pdfdata, pdfdata_td, fileData, fileType, out errorMsg, checkclient);
        }

        //北京中日友好医院XML报告上传(手动传CLientNo)定制版本
        private int classUpLoadReportFromBytesfengzhuang(string token, DataSet ds, byte[] pdfdata, byte[] pdfdata_td, byte[] fileData, string fileType, out string errorMsg, string checkclient)
        {
            try
            {
                errorMsg = "";
                string str = "";

                //if (checkclient == null || checkclient == "")
                //    orgID = ds.Tables[0].Rows[0]["ClientNO"].ToString();
                //else
                //    orgID = checkclient;

                DataTable dtReportForm = ds.Tables[0];
                DataTable dtForm = dtReportForm.Copy();
                DataSet wsReportForm = new DataSet();
                Log.Info("bytes0");
                wsReportForm.Tables.Add(dtForm);
                Log.Info("bytes1");
                if (!ibrd.CheckLabNo(wsReportForm, checkclient, out str))
                {
                    Log.Info("bytes2");
                    errorMsg += str;
                    return -1;
                }
                Log.Info("bytes3");
                DataTable dtReportItem = ds.Tables[1];
                DataTable dtItem = dtReportItem.Copy();
                DataSet wsReportItem = new DataSet();
                Log.Info("bytes4");
                wsReportItem.Tables.Add(dtItem);
                Log.Info("bytes5");
                if (!ibrd.CheckLabNo(wsReportItem, checkclient, out str))
                {
                    Log.Info("bytes6");
                    errorMsg += str;
                    return -1;
                }
                Log.Info("bytes7");
                wsReportForm = MatchCenterNo(wsReportForm, checkclient);
                wsReportItem = MatchCenterNo(wsReportItem, checkclient);
                DataSet dsNew = new DataSet();
                DataTable dtNewForm = wsReportForm.Tables[0].Copy();
                DataTable dtNewItem = wsReportItem.Tables[0].Copy();
                dsNew.Tables.Add(dtNewForm);
                dsNew.Tables.Add(dtNewItem);

                string xmlNew = dsNew.GetXml();
                //Log.Info(xmlNew);
                xmlNew.Replace("NewDataSet", "WebReportFile");
                System.Text.UTF8Encoding converter = new UTF8Encoding();
                byte[] xmlDataNew = converter.GetBytes(xmlNew);
                long OutReportformIndexID;
                return ibrd.UpLoadReportDataFromBytes(xmlDataNew, pdfdata, pdfdata_td, fileData, fileType, out errorMsg, out OutReportformIndexID);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString() + "---------" + e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                errorMsg = "保存数据错误！" + e.ToString();
                return -1;
            }
        }

        #region 佛山区域医院用 验证字段（已加到通用里）
        /// <param name="token"></pa ram>
        /// <param name="xmlData"></param>
        /// <param name="pdfdata"></param>
        /// <param name="pdfdata_td"></param>
        /// <param name="fileData"></param>
        /// <param name="fileType"></param>
        /// <param name="errorMsg"></param>
        /// <param name="checkclient"></param>
        /// <returns></returns>
        [WebMethod(Description = "上传检验报告(验证标准字段)")]
        public int CheckUpLoadReportFromBytes(string token, byte[] xmlData, byte[] pdfdata, byte[] pdfdata_td, byte[] fileData, string fileType, out string errorMsg, string checkclient)
        {
            try
            {
                errorMsg = "";
                System.Text.UTF8Encoding converter = new UTF8Encoding();
                string xml = converter.GetString(xmlData);
                ZhiFang.Common.Log.Log.Info("xml:" + xml);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                //验证报告字段是否规范
                if (!PerifyField(doc))
                    return -1;

                StringReader strTemp = new StringReader(xml);
                DataSet ds = new DataSet();
                ds.ReadXml(strTemp);


                string str = "";
                string orgID = "";
                if (checkclient == null || checkclient == "")
                {
                    orgID = ds.Tables[0].Rows[0]["ClientNO"].ToString();
                }
                else
                {
                    orgID = checkclient;
                }
                DataTable dtReportForm = ds.Tables[0];
                DataTable dtForm = dtReportForm.Copy();
                DataSet wsReportForm = new DataSet();
                Log.Info("bytes0");
                wsReportForm.Tables.Add(dtForm);
                Log.Info("bytes1");
                if (!ibrd.CheckLabNo(wsReportForm, orgID, out str))
                {
                    Log.Info("bytes2");
                    errorMsg += str;
                    return -1;
                }
                Log.Info("bytes3");
                DataTable dtReportItem = ds.Tables[1];
                DataTable dtItem = dtReportItem.Copy();
                DataSet wsReportItem = new DataSet();
                Log.Info("bytes4");
                wsReportItem.Tables.Add(dtItem);
                Log.Info("bytes5");
                if (!ibrd.CheckLabNo(wsReportItem, orgID, out str))
                {
                    Log.Info("bytes6");
                    errorMsg += str;
                    return -1;
                }
                Log.Info("bytes7");
                wsReportForm = MatchCenterNo(wsReportForm, orgID);
                wsReportItem = MatchCenterNo(wsReportItem, orgID);
                DataSet dsNew = new DataSet();
                DataTable dtNewForm = wsReportForm.Tables[0].Copy();
                DataTable dtNewItem = wsReportItem.Tables[0].Copy();
                dsNew.Tables.Add(dtNewForm);
                dsNew.Tables.Add(dtNewItem);

                string xmlNew = dsNew.GetXml();
                //Log.Info(xmlNew);
                xmlNew.Replace("NewDataSet", "WebReportFile");
                byte[] xmlDataNew = converter.GetBytes(xmlNew);
                long OutReportformIndexID;
                return ibrd.UpLoadReportDataFromBytes(xmlDataNew, pdfdata, pdfdata_td, fileData, fileType, out errorMsg, out OutReportformIndexID);
            }
            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Debug(e.ToString() + "---------" + e.StackTrace.ToString() + "---------" + DateTime.Now.ToString("yyMMDD hhmmss"));
                errorMsg = "保存数据错误！" + e.ToString();
                return -1;
            }
        }
        #endregion

        /// <summary>
        /// 验证报告字段是否规范（可以通用符合佛山区域报告规则即可）
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        private bool PerifyField(XmlDocument doc)
        {
            try
            {
                string messages = "";
                string xPath = "//ReportForm";
                string tableNameForm = "ReportForm";
                //WebReportXML = doc.InnerXml;
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                //ReportForm节点必须存在,且只能有一个

                XmlNode reportFormNodeSave = nodelist[0];
                //读取XML转换DataSet
                DataSet dsForm = Tools.GetXMLData.ConvertXMLToDataSet("<" + tableNameForm + ">" + reportFormNodeSave.InnerXml + "</" + tableNameForm + ">");
                //检测ReportForm表
                bool flag = checkField(dsForm.Tables[0], "ReportForm", out messages);
                if (!flag)
                {
                    ZhiFang.Common.Log.Log.Info("检测ReportForm表:" + messages);
                    return false;
                }
                //根节点
                xPath = "/WebReportFile";
                nodelist = doc.SelectNodes(xPath);
                //第二级是保存到ReportItemFull(生化，免疫),ReportMarrowFull(细胞学),ReportMicroFull（微生物）
                //string tableNameItem = "ReportItem";
                string tableNameItemXMLData = "ReportItem";
                foreach (XmlNode xmlNode in nodelist[0].ChildNodes)
                {
                    string nodeName = xmlNode.Name.ToLower();
                    if (nodeName == "reportform")
                        continue;
                    switch (nodeName)
                    {
                        case "reportitem"://生化，免疫
                            //tableNameItem = "RequestItem";
                            tableNameItemXMLData = "ReportItem";
                            break;
                        case "reportmarrow"://细胞学
                            //tableNameItem = "RequestMarrow";
                            tableNameItemXMLData = "ReportMarrow";
                            break;
                        case "reportmicro"://微生物
                            //tableNameItem = "RequestMicro";
                            tableNameItemXMLData = "ReportMicro";
                            break;
                    }
                    //读取XML转换DataSet
                    DataSet dsItem = Tools.GetXMLData.ConvertXMLToDataSet("<" + tableNameItemXMLData + ">" + xmlNode.InnerXml + "</" + tableNameItemXMLData + ">");
                    //检测ReportItem表
                    flag = checkField(dsItem.Tables[0], tableNameItemXMLData, out messages);
                    if (!flag)
                    {
                        Log.Info("检测" + tableNameItemXMLData + "表:" + messages);
                        return false;
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("报告单：" + messages); return true;
                    }
                }
                return false;
            }
            catch (Exception msg)
            {
                ZhiFang.Common.Log.Log.Info("错误信息：" + msg.ToString());
                return false;
            }
        }

        /// <summary>
        /// 验证四个报告表字段
        /// </summary>
        /// <returns></returns>
        private bool checkField(DataTable dt, string tableName, out string messages)
        {
            messages = "";
            string Field = "";
            switch (tableName)
            {
                case "ReportForm":
                    Field = "ReportFormID,BARCODE,PersonID,RECEIVEDATE,SECTIONNO,SECTIONNAME,TESTTYPENO,SAMPLENO,STATUSNO, SAMPLETYPENO,CNAME,GENDERNO,AGE,AGEUNITNO, FOLKNO,TelNo,DOCTOR, COLLECTER,COLLECTDATE, COLLECTTIME," +
"TECHNICIAN, TESTDATE, TESTTIME, CHECKER, CHECKDATE, CHECKTIME, SERIALNO, SICKTYPENO, WebLisSourceOrgID,WebLisSourceOrgName,WebLisOrgID,WebLisOrgName,resultstatus,AGEUNITNAME,GENDERNAME, DEPTNAME, DOCTORNAME, DISTRICTNAME,WARDNAME, FOLKNAME, SICKTYPENAME, SAMPLETYPENAME, TESTTYPENAME";
                    break;
                case "ReportItem":
                    Field = "ReportFormID";
                    break;
                case "ReportMicro":
                    Field = "ReportFormID";
                    break;
                case "ReportMarrow":
                    Field = " ReportFormID";
                    break;
            }
            return BLL.Common.CheckField.IsColumnField(dt, Field, out messages);
        }

        #region 通用传图片方式
        /// <summary>
        /// 上传检验报告,新增报告图片上传功能
        /// </summary>
        /// <param name="token">Token是用户的令牌，是一个字符串，由用户的身份信息和调用时间等通过加密、解密后组成</param>
        /// <param name="xmlData">xml数据</param>
        /// <param name="pdfdata">pdf检验报告</param>
        /// <param name="pdfdata_td">套打pdf检验报告</param>
        /// <param name="fileData">其他文件，例如jpg,frp，word,rtf等</param>
        /// <param name="ImageNameList">报告所需图片的名称集合</param>
        /// <param name="ImageByteList">报告所需图片的数据流集合</param>
        /// <param name="fileType">其他文件的类型</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -100:非法调用(如用户身份不合法等)
        /// </returns>
        [WebMethod]
        public int UpLoadReportFromBytes_ImageList(string token, byte[] xmlData, byte[] pdfdata, byte[] pdfdata_td, byte[] fileData, List<string> ImageNameList, List<byte[]> ImageByteList, string fileType, out string errorMsg)
        {

            try
            {
                Log.Error("开始保存报告图片！");
                if (ImageNameList != null && ImageByteList != null)
                {
                    Log.Error("报告图片列表不为空！个数：" + ImageNameList.Count);
                    if (ImageNameList.Count > 0 && ImageByteList.Count > 0 && ImageNameList.Count == ImageByteList.Count)
                    {
                        Log.Error("报告图片数组名称个数：" + ImageNameList.Count + "@@@图片流个数：" + ImageByteList.Count);
                        if (ConfigurationManager.AppSettings["ReportIncludeImage"] != null && ConfigurationManager.AppSettings["ReportIncludeImage"].ToString().Trim().Length > 0)
                        {
                            Log.Error("报告图片存放路径：" + ConfigurationManager.AppSettings["ReportIncludeImage"].ToString().Trim());
                            string path = ConfigurationManager.AppSettings["ReportIncludeImage"].ToString().Trim();
                            for (int i = 0; i < ImageNameList.Count; i++)
                            {
                                string[] p = ImageNameList[i].Split('@');
                                if (p[0].IndexOf(';') > -1)
                                {
                                    #region 6.6四个关键字
                                    Log.Info("0\r\n");
                                    Log.Info(p[0]);
                                    //2013-08-27;4;1;53
                                    string[] strFormNo = p[0].Split(';');
                                    Log.Info("0-1:" + strFormNo[0] + ";" + strFormNo[1] + ";" + strFormNo[2] + ";" + strFormNo[3]);
                                    string newFNo = "";
                                    Log.Info("这个FanhuiFormNo()方法执行:ReportFormWebService.asmx");
                                    string orgID = "";
                                    List<string> ListStr = new List<string>();
                                    List<string> ListStrName = new List<string>();
                                    string B_Lab_tableName = "";
                                    string B_Lab_controlTableName = "";
                                    B_Lab_tableName = "B_Lab_PGroup";
                                    B_Lab_controlTableName = "B_PGroupControl";
                                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo").Trim() != "" && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo").Trim() == "1")
                                    {
                                        try
                                        {
                                            System.Text.UTF8Encoding converter = new UTF8Encoding();
                                            string xml = converter.GetString(xmlData);
                                            XmlDocument doc = new XmlDocument();
                                            doc.LoadXml(xml);
                                            StringReader strTemp = new StringReader(xml);
                                            DataSet ds = new DataSet();
                                            ds.ReadXml(strTemp);
                                            orgID = ds.Tables[0].Rows[0]["ClientNO"].ToString();
                                            Log.Info("ClientNO医院：" + orgID);
                                            newFNo = ibrd.GetControl(B_Lab_controlTableName, orgID, strFormNo[1]);
                                            Log.Info("newFNo中心：" + newFNo);
                                            if (newFNo == "")
                                            {
                                                newFNo = strFormNo[1];
                                            }
                                            p[0] = strFormNo[0] + ";" + newFNo + ";" + strFormNo[2] + ";" + strFormNo[3];
                                        }
                                        catch (Exception e)
                                        {
                                            p[0] = strFormNo[0] + ";" + strFormNo[1] + ";" + strFormNo[2] + ";" + strFormNo[3];
                                        }
                                    }
                                    Log.Info("1\r\n");
                                    Log.Info(p[1]);

                                    //string path1 = @path + "/" + Convert.ToDateTime(p[1]).Year + "/" + Convert.ToDateTime(p[1]).Month + "/" + Convert.ToDateTime(p[1]).Day + "/" + p[0];
                                    string path1 = @path + "\\" + Convert.ToDateTime(p[1]).Year + "\\" + Convert.ToDateTime(p[1]).Month + "\\" + Convert.ToDateTime(p[1]).Day + "\\" + p[0];
                                    Log.Info("2\r\n");
                                    Log.Info(path1);
                                    string tmpfilename = ImageNameList[i].Replace(':', '&').ToString();
                                    //string tmpfilename = ImageNameList[i].Replace(p[1], "").Replace(p[0], "");
                                    Log.Info("3\r\n");
                                    Log.Info(tmpfilename);
                                    Log.Info("4\r\n");
                                    Log.Info(ImageByteList[i].Length.ToString());
                                    if (!Directory.Exists(path1))
                                    {
                                        Log.Info("保存图片2");
                                        Directory.CreateDirectory(path1);
                                    }
                                    FileStream fs = new FileStream(path1 + "\\" + tmpfilename, FileMode.OpenOrCreate);

                                    fs.Write(ImageByteList[i], 0, ImageByteList[i].Count());
                                    fs.Close();
                                    if (File.Exists(path1 + "\\" + tmpfilename))
                                    {
                                        Log.Error("保存报告图片成功！" + path1 + "@@@" + tmpfilename);
                                    }
                                    else { Log.Error("保存报告图片失败！" + path1 + "@@@" + tmpfilename); }
                                    //if (CreatDirFile(path1, tmpfilename, ImageByteList[i]))
                                    //{
                                    //   Log.Error("保存报告图片成功！" + path1 + "@@@" + tmpfilename);
                                    //}
                                    //else
                                    //{
                                    //    Log.Error("保存报告图片失败！" + path1 + "@@@" + tmpfilename);
                                    //}
                                    #endregion
                                }
                                else
                                {
                                    #region 2009formno
                                    Log.Info("20090");
                                    Log.Info(p[0]);
                                    //2013-08-27;4;1;53
                                    string strFormNo = p[0];
                                    Log.Info("0-1:" + strFormNo);
                                    string newFNo = "";
                                    Log.Info("这个FanhuiFormNo()方法执行:ReportFormWebService.asmx");
                                    string orgID = "";
                                    List<string> ListStr = new List<string>();
                                    List<string> ListStrName = new List<string>();
                                    string B_Lab_tableName = "";
                                    string B_Lab_controlTableName = "";
                                    B_Lab_tableName = "B_Lab_PGroup";
                                    B_Lab_controlTableName = "B_PGroupControl";

                                    //string path1 = @path + "/" + Convert.ToDateTime(p[1]).Year + "/" + Convert.ToDateTime(p[1]).Month + "/" + Convert.ToDateTime(p[1]).Day + "/" + p[0];
                                    string path1 = @path + "\\" + Convert.ToDateTime(p[1]).Year + "\\" + Convert.ToDateTime(p[1]).Month + "\\" + Convert.ToDateTime(p[1]).Day + "\\" + p[0];
                                    Log.Info("2009" + path1);
                                    string tmpfilename = ImageNameList[i].Replace(':', '&').ToString();
                                    tmpfilename = tmpfilename.Replace(p[1], "").Replace(p[0], "");
                                    tmpfilename = tmpfilename.Replace('/', '-');
                                    //string tmpfilename = ImageNameList[i].Replace(p[1], "").Replace(p[0], "");
                                    Log.Info("2009" + tmpfilename);
                                    Log.Info("2009" + ImageByteList[i].Length.ToString());
                                    if (!Directory.Exists(path1))
                                    {
                                        Log.Info("保存图片2");
                                        Directory.CreateDirectory(path1);
                                    }
                                    FileStream fs = new FileStream(path1 + "\\" + tmpfilename, FileMode.OpenOrCreate);

                                    fs.Write(ImageByteList[i], 0, ImageByteList[i].Count());
                                    fs.Close();
                                    if (File.Exists(path1 + "\\" + tmpfilename))
                                    {
                                        Log.Error("2009保存报告图片成功！" + path1 + "@@@" + tmpfilename);
                                    }
                                    else { Log.Error("2009保存报告图片失败！" + path1 + "@@@" + tmpfilename); }
                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            Log.Error("报告图片存放路径为空！");
                        }
                    }
                    else
                    {
                        errorMsg = "保存报告图片错误！报告图片数量同报告图片名称数量不统一！";
                    }
                }
                else
                {
                    Log.Error("没有报告图片！");
                }


            }
            catch (Exception e)
            {
                Log.Error("保存报告图片错误！" + e.ToString());
                errorMsg = "保存报告图片错误！" + e.ToString();
            }
            ReportFormWebService reportFormWebService = new ReportFormWebService();
            return reportFormWebService.UpLoadReportFromBytes(token, xmlData, pdfdata, pdfdata_td, fileData, fileType, out errorMsg);
        }
        #endregion
        public static bool CreatDirFile(string FilePath, string FileName, Byte[] Filesteam)
        {
            Log.Info("保存图片1");
            try
            {
                if (!Directory.Exists(FilePath))
                {
                    Log.Info("保存图片2");
                    Directory.CreateDirectory(FilePath);
                    return CreatDirFile(FilePath, FileName, Filesteam);
                }
                else
                {
                    try
                    {
                        Log.Info("保存图片3");
                        FileStream fs = new FileStream(FilePath + "\\" + FileName, FileMode.OpenOrCreate);
                        fs.Write(Filesteam, 0, Filesteam.Count());
                        fs.Close();
                        return true;
                    }
                    catch
                    {
                        try
                        {
                            Log.Info("保存图片4");
                            FileStream Wrfs = new FileStream(FilePath + "\\" + FileName, FileMode.Create, FileAccess.Write);
                            Wrfs.Write(Filesteam, 0, Filesteam.Length);
                            Wrfs.Close();
                            return true;
                        }
                        catch (Exception)
                        {
                            return false;
                        }

                    }
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 转换图片编码
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        public string FanhuiFormNo(byte[] xmlData, string fno)
        {
            Log.Info("这个FanhuiFormNo()方法执行:ReportFormWebService.asmx");
            string orgID = "", newFNo = "";
            List<string> ListStr = new List<string>();
            List<string> ListStrName = new List<string>();
            string B_Lab_tableName = "";
            string B_Lab_controlTableName = "";
            B_Lab_tableName = "B_Lab_PGroup";
            B_Lab_controlTableName = "B_PGroupControl";
            if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo").Trim() != "" && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LinSFormNo").Trim() == "1")
            {
                System.Text.UTF8Encoding converter = new UTF8Encoding();
                string xml = converter.GetString(xmlData);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                StringReader strTemp = new StringReader(xml);
                DataSet ds = new DataSet();
                ds.ReadXml(strTemp);
                orgID = ds.Tables[0].Rows[0]["ClientNO"].ToString();
                Log.Info("ClientNO医院：" + orgID);
                newFNo = ibrd.GetControl(B_Lab_controlTableName, orgID, fno);
                Log.Info("newFNo中心：" + newFNo);
            }
            else
            {
                newFNo = "false";
            }
            return newFNo;
        }

        /// <summary>
        /// 根据实验室的编码得到中心的码
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="SourceOrgID"></param>
        /// <returns></returns>
        public DataSet MatchCenterNo(DataSet ds, string SourceOrgID)
        {
            string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
            foreach (string str in strArray)
            {
                List<string> ListStr = new List<string>();
                List<string> ListStrName = new List<string>();
                string B_Lab_Columns = "";
                string B_Lab_tableName = "";
                string B_Lab_controlTableName = "";
                switch (str)
                {
                    case "SAMPLETYPENO":
                        B_Lab_Columns = "SAMPLETYPENO";
                        B_Lab_tableName = "B_Lab_SampleType";
                        B_Lab_controlTableName = "B_SampleTypeControl";
                        break;
                    case "GENDERNO":
                        B_Lab_Columns = "GENDERNO";
                        B_Lab_tableName = "b_lab_GenderType";
                        B_Lab_controlTableName = "B_GenderTypeControl";
                        break;
                    case "FOLKNO":  //民族编号
                        B_Lab_Columns = "FOLKNO";
                        B_Lab_tableName = "B_Lab_FolkType";
                        B_Lab_controlTableName = "B_FolkTypeControl";
                        break;
                    case "ITEMNO"://项目编号
                        B_Lab_Columns = "ITEMNO";
                        B_Lab_tableName = "B_Lab_TestItem";
                        B_Lab_controlTableName = "B_TestItemControl";
                        break;
                    case "SUPERGROUPNO":  //大组编号
                        B_Lab_Columns = "SUPERGROUPNO";
                        B_Lab_tableName = "B_Lab_SuperGroup";
                        B_Lab_controlTableName = "B_SuperGroupControl";
                        break;
                    case "SECTIONNO":
                        B_Lab_Columns = "SECTIONNO";
                        B_Lab_tableName = "B_Lab_PGroup";
                        B_Lab_controlTableName = "B_PGroupControl";
                        break;
                    case "PARITEMNO":
                        B_Lab_Columns = "ITEMNO";
                        B_Lab_tableName = "B_Lab_TestItem";
                        B_Lab_controlTableName = "B_TestItemControl";
                        break;

                }
                if (ds.Tables[0].Columns.Contains(str))
                {
                    Log.Info("数据1");
                    for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                    {
                        Log.Info("数据2");
                        if (ds.Tables[0].Rows[count][str].ToString() != "")
                        {
                            Log.Info("数据3");
                            ListStr.Add(ds.Tables[0].Rows[count][str].ToString());
                        }
                        else
                        {
                            Log.Info("数据4");
                            string str1 = "";
                            if (str.IndexOf('N') > -1)
                            {
                                Log.Info("数据5");
                                str1 = str.Substring(0, str.Length - 2);
                            }
                            if (ds.Tables[0].Columns.Contains(str1 + "Name"))
                            {
                                Log.Info("数据6");
                                if (ds.Tables[0].Rows[count][str1 + "Name"].ToString() != "")
                                {
                                    Log.Info("数据7");
                                    ListStrName.Add(ds.Tables[0].Rows[count][str1 + "Name"].ToString());
                                    if (ListStrName.Count > 0)
                                    {
                                        Log.Info("数据8");
                                        DataSet dsLabNo = ibrd.GetLabNo(B_Lab_tableName, ListStrName, SourceOrgID, B_Lab_Columns);
                                        for (int j = 0; j < dsLabNo.Tables[0].Rows.Count; j++)
                                        {
                                            Log.Info("数据9");
                                            if (B_Lab_tableName != "ITEMNO")
                                            {
                                                Log.Info("数据10");
                                                ListStr.Add(dsLabNo.Tables[0].Rows[j]["lab" + B_Lab_Columns].ToString());
                                            }
                                            else
                                                ListStr.Add(dsLabNo.Tables[0].Rows[j][B_Lab_Columns].ToString());
                                        }
                                    }
                                }
                            }

                        }
                    }
                    if (ListStr.Count != 0)
                    {
                        DataSet CenteNo = ibrd.GetCentNo(B_Lab_controlTableName, ListStr, SourceOrgID, B_Lab_Columns);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            foreach (DataRow dritem in CenteNo.Tables[0].Rows)
                            {
                                Log.Info("原值" + dr[str].ToString() + "实验室：" + dritem["Control" + B_Lab_Columns].ToString() + "中心：" + dritem[B_Lab_Columns].ToString());
                                if (dr[str].ToString() == dritem["Control" + B_Lab_Columns].ToString() || dr[str].ToString() == "")
                                {
                                    Log.Info("中心转换" + dritem[B_Lab_Columns].ToString());
                                    dr[str] = dritem[B_Lab_Columns].ToString();

                                    //string[] arrays = dr["REPORTFORMID"].ToString().Split('_');
                                    //arrays[2] = dritem["REPORTFORMID"].ToString();
                                    //try
                                    //{
                                    //    Log.Info(arrays[0] + arrays[1] + arrays[2] + arrays[3] + arrays[4] + arrays[5] + arrays[6] + arrays[7]);
                                    //    dr["REPORTFORMID"] = arrays[0] + arrays[1] + arrays[2] + arrays[3] + arrays[4] + arrays[5] + arrays[6] + arrays[7];
                                    //}
                                    //catch (Exception)
                                    //{

                                    //}

                                }
                            }
                        }
                    }
                }
            }
            Log.Info("输出小组编码" + ds.Tables[0].Rows[0]["SECTIONNO"].ToString());
            return ds;

        }

        /// <summary>
        /// 根据中心端的编码得到实验室的编码
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="SourceOrgID"></param>
        /// <returns></returns>
        public DataSet MatchLabNo(DataSet ds, string SourceOrgID)
        {
            string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
            foreach (string str in strArray)
            {
                List<string> ListStr = new List<string>();
                List<string> ListStrName = new List<string>();
                string B_Lab_controlTableName = "";
                switch (str)
                {
                    case "SAMPLETYPENO":
                        B_Lab_controlTableName = "B_SampleTypeControl";
                        break;
                    case "GENDERNO":
                        B_Lab_controlTableName = "B_GenderTypeControl";
                        break;
                    case "FOLKNO":
                        B_Lab_controlTableName = "B_FolkTypeControl";
                        break;
                    case "ITEMNO":
                        B_Lab_controlTableName = "B_TestItemControl";
                        break;
                    case "SUPERGROUPNO":
                        B_Lab_controlTableName = "B_SuperGroupControl";
                        break;
                }
                if (ds.Tables[0].Columns.Contains(str))
                {
                    for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                    {
                        if (ds.Tables[0].Rows[count][str].ToString() != "")
                        {
                            ListStr.Add(ds.Tables[0].Rows[count][str].ToString());
                        }
                    }
                    if (ListStr.Count != 0)
                    {
                        DataSet labNo = ibrd.GetLabControlNo(B_Lab_controlTableName, ListStr, SourceOrgID, str);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            foreach (DataRow dritem in labNo.Tables[0].Rows)
                            {
                                if (dr[str].ToString() == dritem[str].ToString() || dr[str].ToString() == "")
                                {
                                    dr[str] = dritem[str].ToString();
                                }
                            }
                        }
                    }
                }
            }
            return ds;
        }

        #region 下载报告寿光区病人下载报告 
        public static XmlDocument TransformDTRowIntoXML(DataTable dt, string rootstr, string recordsstr, int[] index)
        {
            try
            {
                XmlDocument document = new XmlDocument();
                XmlElement newChild = document.CreateElement(rootstr);
                for (int i = 0; i < index.Length; i++)
                {
                    XmlElement element2 = document.CreateElement(recordsstr);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        XmlElement element3 = document.CreateElement(dt.Columns[j].ColumnName);
                        element3.InnerText = XMLCodeTransform(dt.Rows[index[i]][j].ToString());
                        element2.AppendChild(element3);
                    }
                    newChild.AppendChild(element2);
                }
                document.AppendChild(newChild);
                return document;
            }
            catch (Exception)
            {
                return new XmlDocument();
            }
        }
        public static string XMLCodeTransform(string a)
        {
            return a;
            //return a.Replace(">", "&gt;").Replace("<", "&lt;").Replace("\"", "&quot;").Replace("\'", "&apos;").Replace("&", "&amp;");
        }

        /// <summary>
        /// 根据中心端的编码得到实验室的编码
        /// </summary>
        /// <param name="dsreportitem"></param>
        /// <param name="SourceOrgID"></param>
        /// <returns></returns>
        public DataSet MatchClientNo(DataSet dsreportitem, string SourceOrgID)
        {
            string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
            foreach (string str in strArray)
            {
                List<string> ListStr = new List<string>();
                List<string> ListStrName = new List<string>();
                string B_Lab_controlTableName = "";
                string B_Lab_Columns = "";
                switch (str.ToUpper())
                {
                    case "SAMPLETYPENO":
                        B_Lab_Columns = "SAMPLETYPENO";
                        B_Lab_controlTableName = "B_SampleTypeControl";
                        break;
                    case "GENDERNO":
                        B_Lab_Columns = "GENDERNO";
                        B_Lab_controlTableName = "B_GenderTypeControl";
                        break;
                    case "FOLKNO":
                        B_Lab_Columns = "FOLKNO";
                        B_Lab_controlTableName = "B_FolkTypeControl";
                        break;
                    case "ITEMNO":
                        B_Lab_Columns = "ITEMNO";
                        B_Lab_controlTableName = "B_TestItemControl";
                        break;
                    case "PARITEMNO":
                        B_Lab_Columns = "ITEMNO";
                        B_Lab_controlTableName = "B_TestItemControl";
                        break;
                    case "SUPERGROUPNO":
                        B_Lab_Columns = "SUPERGROUPNO";
                        B_Lab_controlTableName = "B_SuperGroupControl";
                        break;
                }
                if (dsreportitem.Tables[0].Columns.Contains(str))
                {
                    for (int count = 0; count < dsreportitem.Tables[0].Rows.Count; count++)
                    {
                        if (dsreportitem.Tables[0].Rows[count][str].ToString() != "")
                        {
                            ListStr.Add(dsreportitem.Tables[0].Rows[count][str].ToString());
                        }
                    }
                    if (ListStr.Count != 0)
                    {
                        DataSet labNo = ibrd.GetLabControlNo(B_Lab_controlTableName, ListStr, SourceOrgID, B_Lab_Columns);
                        ZhiFang.Common.Log.Log.Info("B_Lab_controlTableName:" + B_Lab_controlTableName + ",ListStr:" + ListStr + ",SourceOrgID:" + SourceOrgID + ", B_Lab_Columns:" + B_Lab_Columns);
                        //ZhiFang.Common.Log.Log.Debug("dsreportitem:" + dsreportitem.GetXml());
                        //ZhiFang.Common.Log.Log.Debug("labNo:" + labNo.GetXml());
                        if (labNo != null && labNo.Tables.Count > 0 && labNo.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt = labNo.Tables[0];
                            for (int i = 0; i < dsreportitem.Tables[0].Rows.Count; i++)
                            {
                                DataRow[] drlist = dt.Select(B_Lab_Columns + "='" + dsreportitem.Tables[0].Rows[i][str].ToString() + "'");
                                ZhiFang.Common.Log.Log.Info("dtwhere:" + B_Lab_Columns + "='" + dsreportitem.Tables[0].Rows[i][str].ToString() + "'");
                                if (drlist != null && drlist.Count() > 0)
                                {
                                    dsreportitem.Tables[0].Rows[i][str] = drlist[0]["Control" + B_Lab_Columns].ToString();
                                }
                            }
                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Debug("实验室：" + SourceOrgID + "获取对照关系失败！");
                        }
                    }
                }
            }
            return dsreportitem;
        }

        /// <summary>
        /// 下载报告 寿光区病人下载报告
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        [WebMethod]
        public bool DownLoadReportFormID(string StartDate, string EndDate, string CNAME, string PhoneNumber, string HealthCardNumber, string IDCard, string ClientNo, out string WebReportXML, out string Error)
        {
            Log.Info("DownLoadReportFormID");
            WebReportXML = "";
            Error = "";
            string str = "";
            try
            {
                Model.ReportFormFull Model_ReportFormFull = new Model.ReportFormFull();
                Model_ReportFormFull.ZDY4 = StartDate;
                Model_ReportFormFull.ZDY5 = EndDate;
                Model_ReportFormFull.CNAME = CNAME;
                Model_ReportFormFull.ZDY1 = PhoneNumber;
                Model_ReportFormFull.ZDY2 = HealthCardNumber;
                // Model_ReportFormFull.ZDY3 = IDCard;
                Model_ReportFormFull.ZDY9 = IDCard;
                DataSet reportFormFull = new DataSet();
                DataSet reportAllItemView = new DataSet();

                string strWhere = "1=1";
                if (StartDate != "" && StartDate != null)
                {
                    strWhere += " and OperDate>'" + StartDate + "'";
                }
                if (EndDate != "" && EndDate != null)
                {
                    strWhere += " and OperDate<'" + EndDate + "'";
                }
                if (CNAME != "" && CNAME != null)
                {
                    strWhere += " and CNAME='" + CNAME + "'";
                }
                if (PhoneNumber != "" && PhoneNumber != null)
                {
                    strWhere += " and ZDY1='" + PhoneNumber + "'";
                }
                if (HealthCardNumber != "" && HealthCardNumber != null)
                {
                    strWhere += " and ZDY2='" + HealthCardNumber + "'";
                }
                if (IDCard != "" && IDCard != null)
                {
                    strWhere += " and ZDY9='" + IDCard + "'";
                }
                reportFormFull = ibv.GetViewData(1, "_ReportFormFullDataSource", strWhere, "");
                Log.Info("ReportForm:" + reportFormFull.Tables[0].Rows.Count);

                if (((reportFormFull != null) && (reportFormFull.Tables.Count > 0)) && (reportFormFull.Tables[0].Rows.Count > 0))
                {
                    //检测ReportFormFull是否对照
                    if (!ibrff.CheckReportFormCenter(reportFormFull, ClientNo, out str))
                    {
                        Error += str;
                        return false;
                    }
                    //转码过程
                    reportFormFull = MatchClientNo(reportFormFull, ClientNo);

                    int[] numArray = new int[1];
                    XmlDocument document = TransformDTRowIntoXML(DataTableHelper.ColumnNameToUpper(reportFormFull.Tables[0]), "WebReportFile", "ReportForm", numArray);
                    WebReportXML = document.InnerXml;
                    for (int index = 0; index < reportFormFull.Tables[0].Rows.Count; index++)
                    {
                        string ReportFormID = reportFormFull.Tables[0].Rows[index]["ReportFormID"].ToString().Trim();
                        reportAllItemView.Clear();
                        reportAllItemView = ibv.GetViewData(-1, "_ReportItemFullDataSource", " ReportFormID='" + ReportFormID + "'", "");
                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                            {
                                Error += str;
                                return false;
                            }
                            reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                            //ZhiFang.Common.Log.Log.Debug("reportAllItemView->xml:" + reportAllItemView.GetXml());
                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportItem"), "WebReportFile");
                            WebReportXML = document.InnerXml;
                            return true;
                        }
                        Log.Info("ReportItem:" + reportFormFull.Tables[0].Rows.Count);
                        //    reportAllItemView.Clear();
                        //    reportAllItemView = ibv.GetViewData(-1, "_ReportMicroFullDataSource", strWhere, "");
                        //    if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        //    {
                        //        //检测ReportItemFull是否对照
                        //        if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                        //        {
                        //            Error += str;
                        //            return false;
                        //        }
                        //        reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                        //        document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(Table.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMicro"), "WebReportFile");
                        //        WebReportXML = document.InnerXml;
                        //        return true;
                        //    }
                        //    Log.Info("ReportMicro:" + reportFormFull.Tables[0].Rows.Count);
                        //    reportAllItemView.Clear();
                        //    reportAllItemView = ibv.GetViewData(-1, "_ReportMarrowFullDataSource", strWhere, "");
                        //    if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        //    {
                        //        //检测ReportItemFull是否对照
                        //        if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                        //        {
                        //            Error += str;
                        //            return false;
                        //        }
                        //        reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                        //        document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(Table.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMarrow"), "WebReportFile");
                        //        WebReportXML = document.InnerXml;
                        //        return true;
                        //    }
                        //Log.Info("ReportMarrow:" + reportFormFull.Tables[0].Rows.Count);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Error = DateTime.Now + ":" + e.ToString() + e.StackTrace;
                Log.Info("错误信息：" + Error);
                return false;
            }

        }

        /// <summary>
        /// 寿光区给HIS提供获取PDF报告地址的服务
        /// </summary>
        /// <param name="ReportFormID">报告单号</param>
        /// <param name="ReportFormTitle">报告抬头 医院(CLIENT) 中心(CENTER)</param>
        /// <param name="ReportUri">报告单地址</param>
        /// <param name="ErrInfo">错误信息</param>
        /// <returns>返回值：true成功 false失败</returns>
        [WebMethod(Description = "获取PDF报告地址")]
        public bool GetReportUri(string ReportFormID, string ReportFormTitle, out string ReportUri, out string ErrInfo)
        {
            bool result = true;
            ReportUri = null;
            ErrInfo = null;
            string ReportFormPDFServiceUrl = null;
            try
            {
                string title = "0";
                switch (ReportFormTitle.ToUpper())
                {
                    case "CENTER": title = "0"; break;
                    case "CLIENT": title = "1"; break;
                    case "BATCH": title = "2"; break;
                    case "MENZHEN": title = "3"; break;
                    case "ZHUYUAN": title = "4"; break;
                    case "TIJIAN": title = "5"; break;
                    default: title = "0"; break;
                }
                ZhiFang.BLL.Report.ReportFormFull rffb = new ZhiFang.BLL.Report.ReportFormFull();
                Model.ReportFormFull rff_m = new Model.ReportFormFull();
                List<string> ReportFormContextList = null;
                string ReportConfigPath = System.Configuration.ConfigurationManager.AppSettings["ReportConfigPath"];
                rff_m.ReportFormID = ReportFormID;
                DataSet dsrf = rffb.GetList(rff_m);
                if (dsrf != null && dsrf.Tables[0].Rows.Count > 0)
                {
                    ReportFormContextList = FindReportFormFiles(dsrf.Tables[0].Rows[0], ReportConfigPath, (Common.Dictionary.ReportFormTitle)Convert.ToInt32(title), title);
                    if (ReportFormContextList.Count > 0)
                    {
                        ReportFormPDFServiceUrl = System.Configuration.ConfigurationManager.AppSettings["ReportFormPDFServiceUrl"];

                        ReportFormPDFServiceUrl += ReportFormContextList[0];
                        ReportUri = ReportFormPDFServiceUrl;
                        Log.Info("获取报告地址:" + ReportFormPDFServiceUrl);
                    }
                    else
                    {
                        Log.Info("没有找到报告地址");
                    }

                }

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("获取PDF报告地址出错:" + ex.ToString());
                result = false;
                ErrInfo = ex.Message.ToString();
            }
            Log.Info("返回报告的地址:" + ReportFormPDFServiceUrl);
            return result;
        }

        /// <summary>
        /// 获取PDF路径
        /// </summary>
        /// <param name="reportform">报告单数据</param>
        /// <param name="rft">报告抬头</param>
        /// <param name="title">报告抬头</param>
        /// <returns></returns>
        public static List<string> FindReportFormFiles(DataRow reportform, string path, ReportFormTitle rft, string title)
        {
            try
            {
                string SaveType = "PDF";
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").Trim() != "")
                {
                    SaveType = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFileType").Trim().ToUpper();
                }

                ZhiFang.Common.Log.Log.Info("FindReportFormFiles");
                string reportSavePath = path + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\" +
                 DateTime.Parse(reportform["UploadDate"].ToString()).Year + "\\" + DateTime.Parse(reportform["UploadDate"].ToString()).Month + "\\" + DateTime.Parse(reportform["UploadDate"].ToString()).Day + "\\";

                //是否在ReportFormFilesDir底下添加report文件夹
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("isUpLoadReport") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("isUpLoadReport").ToString() == "1")
                {
                    reportSavePath = path + ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir") + "\\report\\" +
                   DateTime.Parse(reportform["UploadDate"].ToString()).Year + "\\" + DateTime.Parse(reportform["UploadDate"].ToString()).Month + "\\" + DateTime.Parse(reportform["UploadDate"].ToString()).Day + "\\";
                }
                //生成统一的文件名称
                string fileNameUnique = "";
                if (reportform["REPORTFORMID"] != null)
                {
                    fileNameUnique = reportform["REPORTFORMID"].ToString();
                }
                if (fileNameUnique == "")
                    fileNameUnique = System.Guid.NewGuid().ToString();
                fileNameUnique = fileNameUnique.Replace(":", "：");

                ZhiFang.Common.Log.Log.Info("报告单名称:" + fileNameUnique + " 抬头：" + title);
                //fileNameUnique = fileNameUnique.Replace(":", "");
                List<string> filepathlist = new List<string>();
                if (SaveType == "JPG")
                {
                    string[] reportfilearray = Directory.GetFiles(reportSavePath + fileNameUnique + "\\", "*.jpg");
                    if (reportfilearray.Length > 0)
                    {
                        for (int i = 0; i < reportfilearray.Length; i++)
                        {
                            filepathlist.Add(reportfilearray[i]);
                        }
                    }
                }


                if (SaveType == "PDF")
                {
                    ZhiFang.Common.Log.Log.Info("预生成的PDF文件路径：" + reportSavePath);
                    //string filePath = "";
                    string[] reportfilearray;
                    //新的weblis上传服务
                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("IsNewWeblisService").Trim() == "1")
                    {
                        ZhiFang.Common.Log.Log.Info("新服务上传预生成的PDF文件路径：" + reportSavePath + rft.ToString());
                        reportfilearray = Directory.GetFiles(reportSavePath + rft.ToString() + "\\", "*.pdf");

                        string t1 = reportfilearray == null ? "null" : string.Join(";", reportfilearray);
                        ZhiFang.Common.Log.Log.Info("报告路径(新服务):" + t1);
                        if (reportfilearray.Length <= 0)
                            return filepathlist;

                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("旧服务上传预生成的PDF文件路径：" + reportSavePath);
                        reportfilearray = Directory.GetFiles(reportSavePath, "*.pdf");

                        string t1 = reportfilearray == null ? "null" : string.Join(";", reportfilearray);
                        ZhiFang.Common.Log.Log.Info("报告路径(旧服务):" + t1);
                        if (reportfilearray.Length <= 0)
                            return filepathlist;

                    }


                    for (int i = 0; i < reportfilearray.Length; i++)
                    {
                        filepathlist = new List<string>();
                        //ZhiFang.Common.Log.Log.Info("预先生成的报告文件:" + reportfilearray[i]);
                        //ZhiFang.Common.Log.Log.Info("抬头:" + title);
                        ////0：中心  1：医院（医院报告名称带T或者T_）
                        if (title == "2" && reportfilearray[i].Contains(fileNameUnique) && reportfilearray[i].Contains("T_"))
                        {
                            filepathlist.Add(reportfilearray[i]);
                            break;
                        }

                        else if (title == "0" && reportfilearray[i].Contains(fileNameUnique) && (reportfilearray[i].Contains("T_" + fileNameUnique) == false && reportfilearray[i].Contains("T" + fileNameUnique) == false))//中心
                        {
                            filepathlist.Add(reportfilearray[i]);
                            ZhiFang.Common.Log.Log.Info("报告抬头为中心的报告路径：" + reportfilearray[i]);
                            break;
                        }
                        else if (title == "1" && reportfilearray[i].Contains(fileNameUnique) && (reportfilearray[i].Contains("T_" + fileNameUnique) || reportfilearray[i].Contains("T" + fileNameUnique)))//医院
                        {
                            filepathlist.Add(reportfilearray[i]);
                            ZhiFang.Common.Log.Log.Info("报告抬头为医院的报告路径：" + reportfilearray[i]);
                            break;
                        }
                        else
                        {
                            if (title != "1" && title != "0" && reportfilearray[i].Contains(fileNameUnique) && reportfilearray[i].Contains("_QT"))//其他类型
                            {
                                filepathlist.Add(reportfilearray[i]);
                                break;
                            }
                        }
                    }

                }

                string applocalroot = path;//获取程序根目录           
                List<string> tmphtml = new List<string>();
                for (int i = 0; i < filepathlist.Count; i++)
                {
                    tmphtml.Add(filepathlist[i].Replace(applocalroot, "").Replace(@"\", @"/"));
                }
                ZhiFang.Common.Log.Log.Info("返回报告路径:" + string.Join(",", tmphtml.ToArray()));
                return tmphtml;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Info("获取报告出错:" + ex.ToString());
            }
            return null;
        }
        #endregion

        #region PKI定制
        [WebMethod(Description = "PKI获取报告列")]
        public string GetReportFormColumn()
        {
            return rfs_svc.GetReportFormColumn();
        }

        /// <summary>
        /// PKI定制报告下载服务 把ReportFormFull,ReportItemFull,ReportMicroFull,ReportMarrowFull拷贝到本地数据库 ganwh add 2016-1-22
        /// </summary>
        /// <param name="ReportItemFull">返回常规项目</param>
        /// <param name="ReportMicroFull">返回微生物项目</param>
        /// <param name="ReportMarrowFull">返回病理项目</param>
        /// <param name="Error">返回错误信息</param>
        /// <param name="ReportFormID">报告单号</param>
        /// <param name="strClientNo">送检单位编号</param>
        /// <returns></returns>
        [WebMethod(Description = "PKI定制报告下载服务")]
        public bool DownLoadReportForm_PKI(out DataSet ReportItemFull, out DataSet ReportMicroFull, out DataSet ReportMarrowFull, out byte[] pdfData, out string Error, string ReportFormID)
        {
            return rfs_svc.DownLoadReportForm_PKI(out ReportItemFull, out ReportMicroFull, out ReportMarrowFull, out pdfData, out Error, ReportFormID);
        }

        /// <summary>
        /// 下载成功打标记
        /// </summary>
        /// <param name="BarCode"></param>
        /// <param name="ClientNo"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        [WebMethod(Description = "PKI下载成功打标记")]
        public bool Changestatus(string BarCode, string ClientNo, string Error)
        {
            return rfs_svc.Changestatus(BarCode, ClientNo, Error);
        }
        /// <summary>
        /// PKI定制报告查询 已ds形式返回报告 ganwh add 2016-1-22
        /// </summary>
        /// <param name="WebLisFlag">下载标志 0未下载 1已下载</param>
        /// <param name="ClientNo">送检单位编号</param>
        /// <param name="Startdate">开始日期</param>
        /// <param name="Enddate">结束日期</param>
        /// <param name="ReportFormFull">返回ds类型的报告</param>
        /// <param name="Error">错误信息</param>
        /// <returns></returns>
        [WebMethod(Description = "PKI定制报告查询")]
        public bool QueryReport_PKI(string WebLisFlag, string ClientNo, string Startdate, string Enddate, out DataSet ReportFormFull, out string Error)
        {
            return rfs_svc.QueryReport_PKI(WebLisFlag, ClientNo, Startdate, Enddate, out ReportFormFull, out Error);
        }


        #endregion        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ClientNo"></param>
        /// <param name="PDFData"></param>
        /// <param name="PDFDataT"></param>
        /// <param name="PDFDataQT"></param>
        /// <param name="Error"></param>
        /// <param name="ReportFormID"></param>
        /// <param name="pdftype">1中心，2医院，3套打</param>
        /// <returns></returns>
        public bool DownLoadReportFormPDF(string ClientNo, out byte[] PDFData, out string Error, string ReportFormID, int pdftype)
        {
            PDFData = null;
            Error = null;
            string UploadDate = null;
            DateTime TestDate = DateTime.Now;
            IBLL.Report.Other.IBView ibv = BLLFactory<IBLL.Report.Other.IBView>.GetBLL("Other.BView");
            try
            {
                string pdfPath = System.Configuration.ConfigurationManager.AppSettings["ReportConfigPath"];
                if (string.IsNullOrEmpty(pdfPath))
                {
                    Error = "无报告文件！";
                    return false;
                }
                if (!string.IsNullOrEmpty(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")))
                {
                    pdfPath = pdfPath + "\\" + Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir");
                }
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("isUpLoadReport") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("isUpLoadReport").ToString() == "1")
                {
                    pdfPath = pdfPath + "\\Report";
                }
                ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.开始读取PDF文件");
                DataSet ds = ibv.GetViewData(-1, "ReportFormFull", "  ReportFormID='" + ReportFormID + "' ", "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    UploadDate = ds.Tables[0].Rows[0]["UploadDate"] == null ? null : (DateTime.Parse(ds.Tables[0].Rows[0]["UploadDate"].ToString())).ToString("yyyy/MM/dd");
                    if (UploadDate != null)
                    {
                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.UploadDate:" + UploadDate + ",根据UploadDate年月日查找PDF文件所在目录");
                        TestDate = Convert.ToDateTime(UploadDate);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.UploadDate为空,尝试取今天日期查找PDF文件所在目录");
                    }
                    string Year = TestDate.Year.ToString();
                    int Month = TestDate.Month;
                    int Day = TestDate.Day;
                    pdfPath = pdfPath + "\\" + Year + "\\" + Month + "\\" + Day + "\\";
                    ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.报告:" + ReportFormID + " 所在目录:" + pdfPath);
                    List<byte[]> a = new List<byte[]>();
                    foreach (string pdfFile in Directory.GetFiles(pdfPath, "*.pdf"))
                    {
                        string FileName = Path.GetFileName(pdfFile);
                        ReportFormID = ReportFormID.Replace(':', '：');//替换成中文的冒号,因为英文格式的冒号在文件名里面是非法的

                        switch (pdftype)
                        {
                            case 1:
                                if (FileName.IndexOf(ReportFormID) == 0)
                                {
                                    if (FileName.IndexOf(ReportFormID + "QT") < 0)
                                    {

                                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.成功找到PDF文件路径:" + pdfFile);
                                        //c#文件流读文件
                                        using (FileStream fsRead = new FileStream(pdfFile, FileMode.Open))
                                        {
                                            int fsLen = (int)fsRead.Length;
                                            PDFData = new byte[fsLen];
                                            int r = fsRead.Read(PDFData, 0, PDFData.Length);
                                        }
                                    }
                                }
                                break;
                            case 2:
                                if (FileName.IndexOf(ReportFormID + "QT") >= 0)
                                {
                                    ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.成功找到PDF文件路径:" + pdfFile);
                                    //c#文件流读文件
                                    using (FileStream fsRead = new FileStream(pdfFile, FileMode.Open))
                                    {
                                        int fsLen = (int)fsRead.Length;
                                        PDFData = new byte[fsLen];
                                        int r = fsRead.Read(PDFData, 0, PDFData.Length);
                                    }
                                }
                                break;
                            case 3:
                                if (FileName.IndexOf("T" + ReportFormID) == 0)
                                {
                                    ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.成功找到PDF文件路径:" + pdfFile);
                                    //c#文件流读文件
                                    using (FileStream fsRead = new FileStream(pdfFile, FileMode.Open))
                                    {
                                        int fsLen = (int)fsRead.Length;
                                        PDFData = new byte[fsLen];
                                        int r = fsRead.Read(PDFData, 0, PDFData.Length);
                                    }
                                }
                                break;
                            default:
                                if (FileName.IndexOf(ReportFormID) == 0)
                                {
                                    if (FileName.IndexOf(ReportFormID + "QT") < 0)
                                    {
                                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.成功找到PDF文件路径:" + pdfFile);
                                        //c#文件流读文件
                                        using (FileStream fsRead = new FileStream(pdfFile, FileMode.Open))
                                        {
                                            int fsLen = (int)fsRead.Length;
                                            PDFData = new byte[fsLen];
                                            int r = fsRead.Read(PDFData, 0, PDFData.Length);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    if (PDFData == null)
                    {
                        switch (pdftype)
                        {
                            case 1:
                                ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.找不到PDF报告");
                                Error = "找不到PDF报告.";
                                break;
                            case 2:
                                ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.找不到PDF医院报告");
                                Error = "找不到PDF医院报告."; break;
                            case 3:
                                ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.找不到PDF套打报告");
                                Error = "找不到PDF套打报告."; break;
                            default:
                                ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.找不到PDF报告");
                                Error = "找不到PDF报告."; break;
                        }
                        return false;
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDF.不存在ReportFormFull记录:" + ReportFormID);
                    Error = "不存在ReportFormFull记录:" + ReportFormID;
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("DownLoadReportFormPDF.获取PDF文件出错:" + ex.ToString());
                Error = "获取PDF文件出错:" + ex.Message.ToString();
                return false;
            }
        }

        [WebMethod(Description = "通过ReportFormID下载PDF(中心+套打)文件(账户密码)")]
        public bool DownLoadReportFormPDFByAccountPassWord(string Account, string PassWord, string ClientNo, out byte[] PDFDataCenter, out byte[] PDFDataLab, out string Error, string ReportFormID)
        {
            PDFDataCenter = null;
            PDFDataLab = null;
            Error = null;
            try
            {
                string user = ConfigHelper.GetConfigString("Account");
                string pwd = ConfigHelper.GetConfigString("PassWord");
                if (user != Account && pwd != PassWord)
                {
                    ZhiFang.Common.Log.Log.Info("用户和密码：" + Account + " " + PassWord);
                    Error = "用户名或者密码错误！";
                    return false;
                }
                if (!DownLoadReportFormPDF(ClientNo, out PDFDataCenter, out Error, ReportFormID, 1))
                {
                    Error += Error;
                    return false;
                }
                if (!DownLoadReportFormPDF(ClientNo, out PDFDataLab, out Error, ReportFormID, 3))
                {
                    Error += Error;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("DownLoadReportFormPDFByAccountPassWord.获取PDF文件出错:" + ex.ToString());
                Error = "获取PDF文件出错:" + ex.Message.ToString();
                return false;
            }
        }

        [WebMethod(Description = "下载报告身份证号、单位编号、就诊类型")]
        public bool DownloadReportByPersonIDAndClientNoAndSickTypeNo(string Account, string PassWord, string PersonID, string ClientNo, string SickTypeNo, out string[] ReportFormIDList, out string Error)
        {
            ReportFormIDList = null;
            var tmpReportFormIDList = new List<string>();
            string ip = ZhiFang.BLL.Common.IPHelper.GetClientIP();
            int plimit = 50;
            int dlimit = 300;
            string sicktypelist = "5";
            if (ConfigHelper.GetConfigInt("DownloadReportByPersonIDAndClientNoAndSickTypeNo_PLimit") > 0)
            {
                plimit = ConfigHelper.GetConfigInt("DownloadReportByPersonIDAndClientNoAndSickTypeNo_PLimit");
            }

            if (ConfigHelper.GetConfigInt("DownloadReportByPersonIDAndClientNoAndSickTypeNo_DLimit") > 0)
            {
                dlimit = ConfigHelper.GetConfigInt("DownloadReportByPersonIDAndClientNoAndSickTypeNo_DLimit");
            }

            if (ConfigHelper.GetConfigString("DownloadReportByPersonIDAndClientNoAndSickTypeNo_SickTypeNo")!=null && ConfigHelper.GetConfigString("DownloadReportByPersonIDAndClientNoAndSickTypeNo_SickTypeNo").Trim() != "")
            {
                sicktypelist = ConfigHelper.GetConfigString("DownloadReportByPersonIDAndClientNoAndSickTypeNo_SickTypeNo");
            }

            if (dlimit <= 0)//绕过间隔判断
            {
                if (!Application.AllKeys.Contains(ip))
                {
                    Application.Add(ip, DateTime.Now.AddSeconds(dlimit).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    DateTime tmpdatetime = DateTime.Parse(Application[ip].ToString());
                    if (DateTime.Compare(DateTime.Now, tmpdatetime) < 0)
                    {
                        Error = "调用间隔为" + dlimit + "秒！还未到达时间间隔，请稍后！";
                        return false;
                    }
                    else
                    {
                        Application[ip] = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss");
                    }

                }
            }

            if (PersonID == null || PersonID.Trim() == "")
            {
                Error = "参数PersonID为空！";
                return false;
            }
            if (ClientNo == null || ClientNo.Trim() == "")
            {
                Error = "参数ClientNo为空！";
                return false;
            }
            if (SickTypeNo == null || SickTypeNo.Trim() == "")
            {
                Error = "参数SickTypeNo为空！";
                return false;
            }
            if (!sicktypelist.Split(',').Contains(SickTypeNo))
            {
                Error = "参数SickTypeNo值错误！";
                return false;
            }
            Log.Info("DownloadReportByPersonIDAndClientNoAndSickTypeNo:PersonID=" + PersonID + ",ClientNo=" + ClientNo + ",SickTypeNo=" + SickTypeNo);

            List<string> listReport = new List<string>();
            string ReportXML;
            Error = "";
            string str = "";

            List<string> personidlist = PersonID.Split(',').ToList();
            if (personidlist.Count > plimit)
            {
                Error = "身份证数量超过最大限制：" + plimit + "！";
                return false;
            }
            try
            {
                string user = ConfigHelper.GetConfigString("Account");
                string pwd = ConfigHelper.GetConfigString("PassWord");
                if (user != Account && pwd != PassWord)
                {
                    ZhiFang.Common.Log.Log.Info("用户和密码：" + Account + " " + PassWord);
                    Error = "用户名或者密码错误！";
                    return false;
                }
                #region 遍历身份证号
                for (int i = 0; i < personidlist.Count; i++)
                {
                    ZhiFang.Common.Log.Log.Info("DownloadReportByPersonIDAndClientNoAndSickTypeNo:PersonID:" + personidlist[i]);
                    ReportXML = "";
                    DataSet reportFormFull = new DataSet();
                    DataSet reportAllItemView = new DataSet();
                    StringBuilder sqlwhere = new StringBuilder();
                    sqlwhere.Append(" 1=1 ");
                    if (personidlist[i] != null && personidlist[i] != "")
                        sqlwhere.Append(" and PersonID='" + personidlist[i] + "'");
                    if (ClientNo != null && ClientNo != "")
                        sqlwhere.Append(" and ClientNo='" + ClientNo + "'");
                    if (SickTypeNo != null && SickTypeNo != "")
                        sqlwhere.Append(" and SickTypeNo='" + SickTypeNo + "' ");

                    reportFormFull = ibv.GetViewData(-1, "_ReportFormFullDataSource", sqlwhere.ToString(), "");
                    if (reportFormFull == null || reportFormFull.Tables[0].Rows.Count == 0)
                    {
                        ZhiFang.Common.Log.Log.Info("条件查不到数据：" + sqlwhere.ToString());
                        Error = "条件查不到数据：" + sqlwhere.ToString();
                        return false;
                    }
                    Log.Info("DownloadReportByPersonIDAndClientNoAndSickTypeNo.ReportForm.Count:" + reportFormFull.Tables[0].Rows.Count);
                    if (((reportFormFull != null) && (reportFormFull.Tables.Count > 0)) && (reportFormFull.Tables[0].Rows.Count > 0))
                    {
                        //ReportFormIDList = new string[reportFormFull.Tables[0].Rows.Count];
                        for (int index = 0; index < reportFormFull.Tables[0].Rows.Count; index++)
                        {
                            //ReportFormIDList[index] = reportFormFull.Tables[0].Rows[index]["ReportFormID"].ToString();
                            tmpReportFormIDList.Add(reportFormFull.Tables[0].Rows[index]["ReportFormID"].ToString());
                        }
                        Log.Info("DownloadReportByPersonIDAndClientNoAndSickTypeNo.tmpReportFormIDList:" + string.Join(",", tmpReportFormIDList.ToArray()));
                    }

                    //return true;

                    //reportFormFull = ibv.GetViewData(-1, "_ReportFormFullDataSource", sqlwhere.ToString(), "");
                    //ZhiFang.Common.Log.Log.Info("DownloadReportByPersonIDAndClientNoAndSickTypeNo:PersonID[" + i + "]：" + PersonID[i] + ",builder=" + sqlwhere);


                    //if (reportFormFull != null && reportFormFull.Tables.Count > 0 && reportFormFull.Tables[0].Rows.Count > 0)
                    //{
                    //    //检测ReportFormFull是否对照
                    //    if (!ibrff.CheckReportFormCenter(reportFormFull, ClientNo, out str))
                    //    {
                    //        Error += str;
                    //        Log.Info("DownloadReportByPersonIDAndClientNoAndSickTypeNo：Error1" + Error);
                    //        return false;
                    //    }
                    //    //转码过程
                    //    reportFormFull = MatchClientNo(reportFormFull, ClientNo);

                    //    int[] numArray = new int[1];
                    //    XmlDocument document = TransformDTRowIntoXML(DataTableHelper.ColumnNameToUpper(reportFormFull.Tables[0]), "WebReportFile", "ReportForm", numArray);

                    //    #region
                    //    for (int index = 0; index < reportFormFull.Tables[0].Rows.Count; index++)
                    //    {
                    //        #region reportitem
                    //        reportAllItemView.Clear();
                    //        reportAllItemView = ibv.GetViewData(-1, "_ReportItemFullDataSource", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                    //        if (reportAllItemView != null && reportAllItemView.Tables.Count > 0 && reportAllItemView.Tables[0].Rows.Count > 0)
                    //        {
                    //            //检测ReportItemFull是否对照
                    //            //if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                    //            //{
                    //            //    Error += str;
                    //            //    continue;
                    //            //}
                    //            //reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                    //            document = ZhiFang.BLL.Common.TransDataToXML.TransformDTInsertIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile/ReportForm", "ReportItem", document);
                    //            //document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(Table.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile/ReportForm", "ReportItem"), "WebReportFile");
                    //            ReportXML = document.InnerXml;

                    //        }
                    //        #endregion
                    //        #region reportmicro
                    //        reportAllItemView.Clear();
                    //        reportAllItemView = ibv.GetViewData(-1, "_ReportMicroFullDataSource", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                    //        if (reportAllItemView != null && reportAllItemView.Tables.Count > 0 && reportAllItemView.Tables[0].Rows.Count > 0)
                    //        {
                    //            //检测ReportItemFull是否对照
                    //            //if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                    //            //{
                    //            //    Error += str;
                    //            //    continue;
                    //            //}
                    //            //reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                    //            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMicro"), "WebReportFile");
                    //            ReportXML = document.InnerXml;

                    //        }
                    //        #endregion
                    //        #region reportmarrow
                    //        reportAllItemView.Clear();
                    //        reportAllItemView = ibv.GetViewData(-1, "_ReportMarrowFullDataSource", "  ReportFormID='" + reportFormFull.Tables[0].Rows[0]["ReportFormID"].ToString() + "' ", "");
                    //        if (reportAllItemView != null && reportAllItemView.Tables.Count > 0 && reportAllItemView.Tables[0].Rows.Count > 0)
                    //        {
                    //            //检测ReportItemFull是否对照
                    //            //if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                    //            //{
                    //            //    Error += str;
                    //            //    continue;
                    //            //}
                    //            //reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                    //            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMarrow"), "WebReportFile");
                    //            ReportXML = document.InnerXml;

                    //        }
                    //        #endregion
                    //    }
                    //    #endregion
                    //    listReport.Add(ReportXML);
                    //}
                    //else
                    //{
                    //    ZhiFang.Common.Log.Log.Info("条件查不到数据：" + sqlwhere.ToString());
                    //    Error = "条件查不到数据：" + sqlwhere.ToString();
                    //    continue;
                    //}
                }
                #endregion
                if (tmpReportFormIDList.Count > 0)
                {
                    ReportFormIDList = tmpReportFormIDList.ToArray();
                }
                return true;
            }
            catch (Exception e)
            {
                Error = DateTime.Now + ":" + e.Message;
                Log.Info("DownloadReportByPersonIDAndClientNoAndSickTypeNo.错误信息：" + DateTime.Now + ":" + e.ToString() + e.StackTrace);
                return false;
            }

        }

        [WebMethod(Description = "下载单位编号、就诊类型、时间范围")]
        public bool DownloadReportByClientNoAndSickTypeNoAndDateTime(string Account, string PassWord, string ClientNo, string SickTypeNo, string Startdate, string Enddate, out string[] ReportFormIDList, out string Error)
        {
            ReportFormIDList = null;
            var tmpReportFormIDList = new List<string>();
            string ip = ZhiFang.BLL.Common.IPHelper.GetClientIP();
            int dlimit = 300;
            int daylimit = 31;
            string sicktypelist = "5";

            if (ConfigHelper.GetConfigInt("DownloadReportByPersonIDAndClientNoAndSickTypeNo_DLimit") > 0)
            {
                dlimit = ConfigHelper.GetConfigInt("DownloadReportByPersonIDAndClientNoAndSickTypeNo_DLimit");
            }

            if (ConfigHelper.GetConfigInt("DownloadReportByClientNoAndSickTypeNoAndDateTime_DayLimit") > 0)
            {
                daylimit = ConfigHelper.GetConfigInt("DownloadReportByClientNoAndSickTypeNoAndDateTime_DayLimit");
            }

            if (ConfigHelper.GetConfigString("DownloadReportByPersonIDAndClientNoAndSickTypeNo_SickTypeNo") != null && ConfigHelper.GetConfigString("DownloadReportByPersonIDAndClientNoAndSickTypeNo_SickTypeNo").Trim() != "")
            {
                sicktypelist = ConfigHelper.GetConfigString("DownloadReportByPersonIDAndClientNoAndSickTypeNo_SickTypeNo");
            }

            if (dlimit <= 0)//绕过间隔判断
            {
                if (!Application.AllKeys.Contains(ip))
                {
                    Application.Add(ip, DateTime.Now.AddSeconds(dlimit).ToString("yyyy-MM-dd HH:mm:ss"));
                }
                else
                {
                    DateTime tmpdatetime = DateTime.Parse(Application[ip].ToString());
                    if (DateTime.Compare(DateTime.Now, tmpdatetime) < 0)
                    {
                        Error = "调用间隔为" + dlimit + "秒！还未到达时间间隔，请稍后！";
                        return false;
                    }
                    else
                    {
                        Application[ip] = DateTime.Now.AddMinutes(5).ToString("yyyy-MM-dd HH:mm:ss");
                    }

                }
            }

            if (!DateTime.TryParse(Startdate, out DateTime s))
            {
                Error = "参数Startdate错误的值！";
                return false;
            }
            if (!DateTime.TryParse(Enddate, out DateTime e))
            {
                Error = "参数Enddate错误的值！";
                return false;
            }
            if (s.AddDays(daylimit).CompareTo(e) < 0)
            {
                Error = "参数Startdate、Enddate错误的值,时间跨度超过" + daylimit + "天！";
                return false;
            }

            if (ClientNo == null || ClientNo.Trim() == "")
            {
                Error = "参数ClientNo为空！";
                return false;
            }
            if (SickTypeNo == null || SickTypeNo.Trim() == "")
            {
                Error = "参数SickTypeNo为空！";
                return false;
            }
            if (!sicktypelist.Split(',').Contains(SickTypeNo))
            {
                Error = "参数SickTypeNo值错误！";
                return false;
            }
            Log.Info("DownloadReportByClientNoAndSickTypeNoAndDateTime:Startdate=" + Startdate + ",Enddate=" + Enddate + ",ClientNo=" + ClientNo + ",SickTypeNo=" + SickTypeNo);

            List<string> listReport = new List<string>();
            string ReportXML;
            Error = "";
            string str = "";

            try
            {
                string user = ConfigHelper.GetConfigString("Account");
                string pwd = ConfigHelper.GetConfigString("PassWord");
                if (user != Account && pwd != PassWord)
                {
                    ZhiFang.Common.Log.Log.Info("用户和密码：" + Account + " " + PassWord);
                    Error = "用户名或者密码错误！";
                    return false;
                }

                ReportXML = "";
                DataSet reportFormFull = new DataSet();
                DataSet reportAllItemView = new DataSet();
                StringBuilder sqlwhere = new StringBuilder();
                sqlwhere.Append(" 1=1 ");
                sqlwhere.Append(" and ClientNo='" + ClientNo + "'");
                sqlwhere.Append(" and SickTypeNo='" + SickTypeNo + "'");
                sqlwhere.Append(" and CHECKDATE>='" + Startdate + "' and CHECKDATE<='" + Enddate + "' ");

                reportFormFull = ibv.GetViewData(-1, "_ReportFormFullDataSource", sqlwhere.ToString(), "");
                if (reportFormFull == null || reportFormFull.Tables[0].Rows.Count == 0)
                {
                    ZhiFang.Common.Log.Log.Info("条件查不到数据：" + sqlwhere.ToString());
                    Error = "条件查不到数据：" + sqlwhere.ToString();
                    return false;
                }
                Log.Info("DownloadReportByClientNoAndSickTypeNoAndDateTime.ReportForm.Count:" + reportFormFull.Tables[0].Rows.Count);
                if (((reportFormFull != null) && (reportFormFull.Tables.Count > 0)) && (reportFormFull.Tables[0].Rows.Count > 0))
                {
                    //ReportFormIDList = new string[reportFormFull.Tables[0].Rows.Count];
                    for (int index = 0; index < reportFormFull.Tables[0].Rows.Count; index++)
                    {
                        //ReportFormIDList[index] = reportFormFull.Tables[0].Rows[index]["ReportFormID"].ToString();
                        tmpReportFormIDList.Add(reportFormFull.Tables[0].Rows[index]["ReportFormID"].ToString());
                    }
                    Log.Info("DownloadReportByClientNoAndSickTypeNoAndDateTime.tmpReportFormIDList:" + string.Join(",", tmpReportFormIDList.ToArray()));
                }

                if (tmpReportFormIDList.Count > 0)
                {
                    ReportFormIDList = tmpReportFormIDList.ToArray();
                }
                return true;
            }
            catch (Exception ex)
            {
                Error = DateTime.Now + ":" + ex.Message;
                Log.Info("DownloadReportByClientNoAndSickTypeNoAndDateTime.错误信息：" + DateTime.Now + ":" + ex.ToString() + ex.StackTrace);
                return false;
            }

        }
    }
}
