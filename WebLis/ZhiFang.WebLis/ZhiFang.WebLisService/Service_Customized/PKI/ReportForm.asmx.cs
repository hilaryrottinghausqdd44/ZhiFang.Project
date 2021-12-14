using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using Tools;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.IBLL.Report;
using ZhiFang.Model;

namespace ZhiFang.WebLisService.Service_Customized.PKI
{
    /// <summary>
    /// ReportForm 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class ReportForm : System.Web.Services.WebService
    {

        [WebMethod]
        public bool QueryReport_PKI_PWD(string Account, string PWD, string Startdate, string Enddate, string DateType,string Serialno,  out string ReportFormFull, out string Error)
        {
            Error = "";
            ReportFormFull = "";
            string ClientNo = "";
            WSRBAC_Service.WSRbacSoapClient wsrbac = null;
            #region 验证参数
            if (Account == null || Account.Trim() == "")
            {
                ZhiFang.Common.Log.Log.Debug("QueryReport_PKI_PWD.Account为空。");
                Error = "Account为空.";
                return false;
            }
            if (Startdate == null || Startdate.Trim() == "")
            {
                ZhiFang.Common.Log.Log.Debug("QueryReport_PKI_PWD.Startdate为空。");
                Error = "Startdate为空.";
                return false;
            }
            if (Enddate == null || Enddate.Trim() == "")
            {
                ZhiFang.Common.Log.Log.Debug("QueryReport_PKI_PWD.Enddate为空。");
                Error = "Enddate为空.";
                return false;
            }
            if (DateType == null || DateType.Trim() == "")
            {
                DateType = "RECEIVE";
                ZhiFang.Common.Log.Log.Debug("QueryReport_PKI_PWD.DateType为空。默认值：" + DateType);
            }
            
            #endregion

            ZhiFang.Common.Log.Log.Debug("QueryReport_PKI_PWD.参数:Account:" + Account + "@Startdate:" + Startdate + "@Enddate:" + Enddate + "@DateType:" + DateType );

            #region 初始化权限服务
            try
            {
                wsrbac = new WSRBAC_Service.WSRbacSoapClient();
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("QueryReport_PKI_PWD.未能初始化权限服务:" + ex.ToString());
                return false;
            }
            #endregion

            #region 登录验证
            string rbacerror;
            bool loginbool = wsrbac.Login(Account, PWD, out rbacerror);
            if (!loginbool)
            {
                ZhiFang.Common.Log.Log.Debug("QueryReport_PKI_PWD.登录验证错误:" + rbacerror);
                Error = "登录验证错误，可能是用户名密码错误！";
                return false;
            }
            else
            {
                IBLL.Common.BaseDictionary.IBBusinessLogicClientControl blcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
                EntityList<Model.CLIENTELE> cl = blcc.GetBusinessLogicClientList(new Model.BusinessLogicClientControl { Account = Account.Trim(), SelectedFlag = true, Flag = 0 }, 1, 10, "CLIENTELE.ClIENTNO", "", "");
                if (cl != null && cl.count > 0)
                {
                    ClientNo = cl.list[0].ClIENTNO;
                }
                else
                {
                    #region 得到部门信息
                    string deptCode = "";
                    string userinfostr = wsrbac.getUserInfo(Account.Trim());
                    DataSet ds = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(userinfostr);

                    string deptxml = wsrbac.getUserOrgInfo(ds.Tables[0].Rows[0]["Account"].ToString());
                    DataSet deptds = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(deptxml);
                    string strsn = "0";
                    if (deptds != null && deptds.Tables.Count > 0 && deptds.Tables[0].Rows.Count > 0)
                    {  //的到层级关系
                        for (int i = 0; i < deptds.Tables[0].Rows.Count; i++)
                        {
                            if (deptds.Tables[0].Rows[i]["SN"].ToString() != "01Root" && (deptds.Tables[0].Rows[i]["SN"].ToString().Length) >= (Convert.ToInt32(strsn) + 2))
                            {
                                strsn = deptds.Tables[0].Rows[i]["SN"].ToString();
                            }
                        }
                        for (int i = 0; i < deptds.Tables[0].Rows.Count; i++)
                        {
                            if (deptds.Tables[0].Rows[i]["SN"].ToString() != "01Root" && strsn.Length == deptds.Tables[0].Rows[i]["sn"].ToString().Length)
                            {
                                //单位
                                deptCode = deptds.Tables[0].Rows[i]["orgcode"].ToString();
                                break;
                            }
                        }
                    }
                    #endregion
                    IBLL.Common.BaseDictionary.IBCLIENTELE ibctl = BLLFactory<ZhiFang.IBLL.Common.BaseDictionary.IBCLIENTELE>.GetBLL();
                    if (deptCode != "" && deptCode != null)
                    {
                        Model.CLIENTELE model = ibctl.GetModel(long.Parse(deptCode));

                        if (model != null && cl.list != null)
                        {
                            ClientNo = model.ClIENTNO;
                        }
                    }
                }
                if (ClientNo == null || ClientNo.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Debug("QueryReport_PKI_PWD.根据账户密码未找到相关送检单位，可能是未配置。");
                    Error = "根据账户密码未找到相关送检单位，可能是未配置！";
                    return false;
                }
            }
            #endregion

            ZhiFang.Common.Log.Log.Info("QueryReport_PKI_PWD.查询报告开始:");
            Error = "";
            DataSet dsReportFormFull = new DataSet();
            try
            {
                Model.ReportFormFull tempModel = new Model.ReportFormFull();

                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIKESEARCH") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIKESEARCH").Trim() != "")
                {
                    tempModel.LIKESEARCH = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIKESEARCH").Trim();
                }
                switch (DateType.ToUpper().Trim())
                {
                    case "RECEIVE": tempModel.Startdate = Convert.ToDateTime(Startdate); tempModel.Enddate = Convert.ToDateTime(Enddate); break;
                    case "CHECK": tempModel.CheckStartDate = Convert.ToDateTime(Startdate); tempModel.CheckEndDate = Convert.ToDateTime(Enddate); break;
                    case "COLLECT": tempModel.collectStartdate = Convert.ToDateTime(Startdate); tempModel.collectEnddate = Convert.ToDateTime(Enddate); break;
                    case "NOPER": tempModel.noperdateStart = Convert.ToDateTime(Startdate); tempModel.noperdateEnd = Convert.ToDateTime(Enddate); break;
                    default: tempModel.Startdate = Convert.ToDateTime(Startdate); tempModel.Enddate = Convert.ToDateTime(Enddate); break;
                }
                tempModel.CLIENTNO = ClientNo;
                if (Serialno != null && Serialno.Trim() != "")
                {
                    tempModel.serialno = Serialno;
                }
                //if (SECTIONNO != null)
                //    tempModel.SECTIONNO = SECTIONNO;
                //if (CNAME != null)
                //{
                //    tempModel.CNAME = CNAME;
                //}
                //if (GENDERNAME != null)
                //    tempModel.GENDERNAME = GENDERNAME;
                //if (SAMPLENO != null)
                //    tempModel.SAMPLENO = SAMPLENO;
                //if (PATNO != null)
                //    tempModel.PATNO = PATNO;
                //if (PRINTTIMES != null)
                //    tempModel.PRINTTIMES = int.Parse(PRINTTIMES);
                //if (ZDY10 != null)
                //    tempModel.ZDY10 = ZDY10;
                //if (PERSONID != null)
                //    tempModel.PERSONID = PERSONID;
                //if (LIKESEARCH != null)
                //    tempModel.LIKESEARCH = LIKESEARCH;
                //if (serialno != null)
                //    tempModel.serialno = serialno;
                //if (clientcode != null)
                //    tempModel.clientcode = clientcode;
                //if (SICKTYPENO != null && SICKTYPENO.Trim() != "")
                //    tempModel.SICKTYPENO = SICKTYPENO;
                IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
                DataSet ds = ibrff.GetList(10000, tempModel, "  RECEIVEDATE desc ");
                if (((ds != null) && (ds.Tables.Count > 0)) && (ds.Tables[0].Rows.Count > 0))
                {
                    DataSet tmpds = new DataSet();
                    DataTable dt = new DataTable("ReportFormList");
                    dt.Columns.Add("ReportFormId");
                    IBBClientPara ibbcp = BLLFactory<IBBClientPara>.GetBLL("BaseDictionary.BBClientPara");
                    B_ClientPara bcp = ibbcp.GetModel(long.Parse(ClientNo), "ReportFormRowRule");
                    DataRow[] drlist = null;
                    if (bcp != null && bcp.ParaValue != null && bcp.ParaValue != "")
                    {
                        drlist = ds.Tables[0].Select(bcp.ParaValue);
                    }
                    else
                    {
                        drlist = ds.Tables[0].Select();
                    }
                    for (int i = 0; i < drlist.Length; i++)
                    {
                        DataRow dr = dt.NewRow();
                        dr["ReportFormId"] = drlist[i]["ReportFormId"].ToString();
                        dt.Rows.Add(dr);
                    }
                    tmpds.Tables.Add(dt);
                    ReportFormFull = tmpds.GetXml();
                    return true;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("QueryReport_PKI_PWD.ReportFormFull查不到数据!");
                    Error = "QueryReport_PKI_PWD.ReportFormFull查不到数据";
                    return true;
                }

            }
            catch (Exception e)
            {
                Error = DateTime.Now + ":" + e.ToString() + e.StackTrace;
                ZhiFang.Common.Log.Log.Error("QueryReport_PKI_PWD.查询报告异常：" + Error);
                return false;
            }
            return false;
        }

        [WebMethod]
        public bool QueryReportInfo_PKI_PWD(string Account, string PWD, string ReportFormId, string NameOutputType, out string ReportFormInfo, out string ReportItemInfo, out string ReportMicroInfo, out string ReportMarrowInfo, out string pdfdata,out string Error)
        {
            Error = "";
            ReportFormInfo = "";
            ZhiFang.Common.Log.Log.Info("QueryReportInfo_PKI_PWD.开始下载报告");
            ReportItemInfo = "";
            ReportMicroInfo = "";
            ReportMarrowInfo = "";
            pdfdata = "";
            Error = "";
            IBLL.Report.Other.IBView ibv = BLLFactory<IBLL.Report.Other.IBView>.GetBLL("Other.BView");
            IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
            IBTestItemControl ibtic = BLLFactory<IBTestItemControl>.GetBLL("BaseDictionary.TestItemControl");
            IBBClientPara ibbcp = BLLFactory<IBBClientPara>.GetBLL("BaseDictionary.BBClientPara");

            if (NameOutputType == null || NameOutputType.Trim() == "")
            {
                NameOutputType = "CHS";
                ZhiFang.Common.Log.Log.Debug("QueryReportInfo_PKI_PWD.NameOutputType为空。默认值：" + NameOutputType);
            }
            else
            {
                ZhiFang.Common.Log.Log.Debug("QueryReportInfo_PKI_PWD.NameOutputType：" + NameOutputType);
            }
            try
            {
                ZhiFang.Common.Log.Log.Info("QueryReportInfo_PKI_PWD.开始查询ReportItemFull");
                #region 开始查询ReportItemFull
                DataSet DsReportItem = ibv.GetViewData(-1, "ReportItemFull", "  ReportFormID='" + ReportFormId + "' ", "");
                DataSet dsreportform = ibrff.GetList("  ReportFormID='" + ReportFormId + "' ");
                if (dsreportform != null && dsreportform.Tables.Count > 0 && dsreportform.Tables[0].Rows.Count > 0)
                {
                    
                    B_ClientPara bcp = ibbcp.GetModel(long.Parse(dsreportform.Tables[0].Rows[0]["CLIENTNO"].ToString().Trim()), "ReportFormColumnRule");
                    //dsreportform.DataSetName = "ReportFormInfo";
                    dsreportform.Tables[0].TableName = "ReportFormInfo";
                    if (bcp != null && bcp.ParaValue != null && bcp.ParaValue.Trim() != null)
                    {
                        DataSet dstmp = dsreportform.Copy();
                        DataTableHelper.SetTableColumn(dstmp.Tables[0], bcp.ParaValue.Trim());
                        ReportFormInfo = dstmp.GetXml();
                    }
                    else
                    {
                        ReportFormInfo = dsreportform.GetXml();
                    }
                    string WeblisSourceOrgId = dsreportform.Tables[0].Rows[0]["WeblisSourceOrgId"].ToString();
                    if (DsReportItem != null && DsReportItem.Tables[0].Rows.Count > 0)
                    {
                        if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("TransCodField") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("TransCodField").ToUpper().Contains("ITEMNO"))
                        {
                            if (DsReportItem.Tables[0].Columns.Contains("ItemNo") || DsReportItem.Tables[0].Columns.Contains("ITEMNO"))
                            {
                                for (int i = 0; i < DsReportItem.Tables[0].Rows.Count; i++)
                                {
                                    DsReportItem.Tables[0].Rows[i]["ITEMNO"] = ibtic.GetLabCodeNo(WeblisSourceOrgId, DsReportItem.Tables[0].Rows[i]["ITEMNO"].ToString());
                                }
                            }
                        }
                        if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("TransCodField") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("TransCodField").ToUpper().Contains("PARITEMNO"))
                        {
                            if (DsReportItem.Tables[0].Columns.Contains("PARITEMNO"))
                            {
                                for (int i = 0; i < DsReportItem.Tables[0].Rows.Count; i++)
                                {
                                    DsReportItem.Tables[0].Rows[i]["PARITEMNO"] = ibtic.GetLabCodeNo(WeblisSourceOrgId, DsReportItem.Tables[0].Rows[i]["PARITEMNO"].ToString());
                                }
                            }
                        }
                        DsReportItem.Tables[0].TableName = "ReportItemInfo";
                        bcp = ibbcp.GetModel(long.Parse(dsreportform.Tables[0].Rows[0]["CLIENTNO"].ToString().Trim()), "ReportItemColumnRule");
                        if (bcp != null && bcp.ParaValue != null && bcp.ParaValue.Trim() != null)
                        {
                            DataTableHelper.SetTableColumn(DsReportItem.Tables[0], bcp.ParaValue.Trim());
                        }
                        ReportItemInfo = DsReportItem.GetXml();
                        ZhiFang.Common.Log.Log.Info("QueryReportInfo_PKI_PWD.查询到DsReportItem记录总数:" + DsReportItem.Tables[0].Rows.Count);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("QueryReportInfo_PKI_PWD.DsReportItem记录为0");
                    }
                }
                #endregion
                
                ZhiFang.Common.Log.Log.Info("QueryReportInfo_PKI_PWD.开始查询ReportMicroFull");
                #region 开始查询ReportMicroFull
                DataSet dsReportMicroFull = ibv.GetViewData(-1, "ReportMicroFull", "  ReportFormID='" + ReportFormId + "' ", "");
                if (dsReportMicroFull != null && dsReportMicroFull.Tables[0].Rows.Count > 0)
                {
                    dsReportMicroFull.Tables[0].TableName = "ReportMicroInfo";
                    B_ClientPara bcp = ibbcp.GetModel(long.Parse(dsreportform.Tables[0].Rows[0]["CLIENTNO"].ToString().Trim()), "ReportMicroColumnRule");
                    if (bcp != null && bcp.ParaValue != null && bcp.ParaValue.Trim() != null)
                    {
                        DataTableHelper.SetTableColumn(dsReportMicroFull.Tables[0], bcp.ParaValue.Trim());
                    }
                    ReportMicroInfo = dsReportMicroFull.GetXml();
                    ZhiFang.Common.Log.Log.Info("QueryReportInfo_PKI_PWD.查询到ReportMicroFull记录总数:" + dsReportMicroFull.Tables[0].Rows.Count);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("QueryReportInfo_PKI_PWD.ReportMicroFull记录为0");
                }
                #endregion

                ZhiFang.Common.Log.Log.Info("QueryReportInfo_PKI_PWD.开始查询ReportMarrowFull");
                #region 开始查询ReportMarrowFull
                DataSet dsReportMarrowFull = ibv.GetViewData(-1, "ReportMarrowFull", "  ReportFormID='" + ReportFormId + "' ", "");
                if (dsReportMarrowFull != null && dsReportMarrowFull.Tables[0].Rows.Count > 0)
                {
                    dsReportMarrowFull.Tables[0].TableName = "ReportMarrowInfo";
                    B_ClientPara bcp = ibbcp.GetModel(long.Parse(dsreportform.Tables[0].Rows[0]["CLIENTNO"].ToString().Trim()), "ReportMarrowColumnRule");
                    if (bcp != null && bcp.ParaValue != null && bcp.ParaValue.Trim() != null)
                    {
                        DataTableHelper.SetTableColumn(dsReportMarrowFull.Tables[0], bcp.ParaValue.Trim());
                    }
                    ReportMarrowInfo = dsReportMarrowFull.GetXml();
                    ZhiFang.Common.Log.Log.Info("QueryReportInfo_PKI_PWD.查询到ReportMarrowFull记录总数:" + dsReportMarrowFull.Tables[0].Rows.Count);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("QueryReportInfo_PKI_PWD.ReportMarrowFull记录为0");
                }
                #endregion
            }
            catch (Exception e)
            {
                Error = DateTime.Now + ":QueryReportInfo_PKI_PWD.下载报告异常" + e.ToString() + e.StackTrace;
                return false;
            }
            return true;
        }

        [WebMethod]
        public bool GetPDF_PKI_PWD(string Account, string PWD, out byte[] PDFData, out string Error, string ReportFormID)
        {
            PDFData = null;
            Error = null;
            string UploadDate = null;
            string TestDate = null;
            IBLL.Report.Other.IBView ibv = BLLFactory<IBLL.Report.Other.IBView>.GetBLL("Other.BView");
            try
            {
                string pdfPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportConfigPath");
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

                ZhiFang.Common.Log.Log.Info("GetPDF_PKI_PWD.开始读取PDF文件");
                DataSet ds = ibv.GetViewData(-1, "ReportFormFull", "  ReportFormID='" + ReportFormID + "' ", "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    UploadDate = ds.Tables[0].Rows[0]["UploadDate"] == null ? null : (DateTime.Parse(ds.Tables[0].Rows[0]["UploadDate"].ToString())).ToString("yyyy/MM/dd");
                    if (UploadDate != null)
                    {
                        ZhiFang.Common.Log.Log.Info("GetPDF_PKI_PWD.UploadDate:" + UploadDate + ",根据UploadDate年月日查找PDF文件所在目录");
                        TestDate = UploadDate.Split(' ')[0];

                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("GetPDF_PKI_PWD.UploadDate为空,尝试取今天日期查找PDF文件所在目录");
                        TestDate = DateTime.Now.ToString("yyyy/MM/dd");
                    }
                    string Year = TestDate.Split('/')[0];
                    int Month = int.Parse(TestDate.Split('/')[1]);
                    int Day = int.Parse(TestDate.Split('/')[2]);
                    pdfPath = pdfPath + "\\" + Year + "\\" + Month + "\\" + Day + "\\";
                    ZhiFang.Common.Log.Log.Info("GetPDF_PKI_PWD.报告:" + ReportFormID + " 所在目录:" + pdfPath);
                    foreach (string pdfFile in Directory.GetFiles(pdfPath, "*.pdf"))
                    {
                        string FileName = Path.GetFileName(pdfFile);
                        ReportFormID = ReportFormID.Replace(':', '：');//替换成中文的冒号,因为英文格式的冒号在文件名里面是非法的
                        if (FileName.IndexOf(ReportFormID) > -1)
                        {
                            pdfPath += FileName;
                            ZhiFang.Common.Log.Log.Info("GetPDF_PKI_PWD.成功找到PDF文件路径:" + pdfPath);
                            //c#文件流读文件
                            using (FileStream fsRead = new FileStream(pdfPath, FileMode.Open))
                            {
                                int fsLen = (int)fsRead.Length;
                                PDFData = new byte[fsLen];
                                int r = fsRead.Read(PDFData, 0, PDFData.Length);
                            }
                            break;
                        }
                    }
                    if (PDFData == null)
                    {
                        ZhiFang.Common.Log.Log.Info("GetPDF_PKI_PWD.找不到PDF报告");
                        Error = "找不到PDF报告";
                        return false;
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("GetPDF_PKI_PWD.不存在ReportFormFull记录:" + ReportFormID);
                    Error = "不存在ReportFormFull记录:" + ReportFormID;
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("GetPDF_PKI_PWD.获取PDF文件出错:" + ex.Message.ToString());
                Error = "获取PDF文件出错:" + ex.Message.ToString();
                return false;
            }
        }

       
    }
}
