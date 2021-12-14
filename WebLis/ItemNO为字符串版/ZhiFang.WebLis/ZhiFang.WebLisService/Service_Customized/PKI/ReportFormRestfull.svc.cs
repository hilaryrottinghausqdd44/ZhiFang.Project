using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using Tools;
using ZhiFang.BLLFactory;
using ZhiFang.Common.Log;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.IBLL.Report;
using ZhiFang.Model;
using ZhiFang.Tools;

namespace ZhiFang.WebLisService.Service_Customized.PKI
{
    [ServiceContract(Namespace = "ZhiFang.WebLisService")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReportFormRestfull
    {
        private readonly IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
        private readonly IBReportItemFull ibrif = BLLFactory<IBReportItemFull>.GetBLL("ReportItemFull");
        private readonly IBTestItemControl ibtic = BLLFactory<IBTestItemControl>.GetBLL("BaseDictionary.TestItemControl");
        private readonly IBLL.Report.Other.IBView ibv = BLLFactory<IBLL.Report.Other.IBView>.GetBLL("Other.BView");

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DownLoadReportFormListToOther_PKI?Account={Account}&PWD={PWD}&Barcode={Barcode}&ReportTimeStart={ReportTimeStart}&ReportTimeEnd={ReportTimeEnd}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue DownLoadReportFormListToOther_PKI(string Account, string PWD, string Barcode, string ReportTimeStart, string ReportTimeEnd)
        {
            Log.Info("DownLoadReportFormToOther_PKI.调用。");
            BaseResultDataValue brdv = new BaseResultDataValue();
            string ClientNo = "";
            WSRBAC_Service.WSRbacSoapClient wsrbac = null;
            string tmpBarcode = "";
            try
            {
                #region 验证参数
                if (Account == null || Account.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "账户为空！";
                    ZhiFang.Common.Log.Log.Error("DownLoadReportFormToOther_PKI:账户为空！");
                    return brdv;
                }
                if (PWD == null || PWD.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "密码为空！";
                    ZhiFang.Common.Log.Log.Error("DownLoadReportFormToOther_PKI:密码为空！");
                    return brdv;
                }
                if (ReportTimeStart == null || ReportTimeStart.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "报告开始时间为空！";
                    ZhiFang.Common.Log.Log.Error("DownLoadReportFormToOther_PKI:报告开始时间为空！");
                    return brdv;
                }
                if (ReportTimeEnd == null || ReportTimeEnd.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "报告结束时间为空！";
                    ZhiFang.Common.Log.Log.Error("DownLoadReportFormToOther_PKI:报告结束时间为空！");
                    return brdv;
                }
                if (Barcode == null || Barcode.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Info("DownLoadReportFormToOther_PKI:条码为空！");
                }
                else
                {
                    tmpBarcode = Barcode;
                }
                #endregion
                ZhiFang.Common.Log.Log.Debug("DownLoadReportFormToOther_PKI.参数:Account:" + Account + "@PWD:" + PWD + "@ReportTimeStart:" + ReportTimeStart + "@ReportTimeEnd:" + ReportTimeEnd + "@tmpBarcode:" + tmpBarcode);
                #region 初始化权限服务
                try
                {
                    wsrbac = new WSRBAC_Service.WSRbacSoapClient();
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("DownLoadReportFormToOther_PKI.未能初始化权限服务:" + ex.ToString());
                    brdv.success = false;
                    brdv.ErrorInfo = "权限异常！";
                    return brdv;
                }
                #endregion

                #region 登录验证
                string rbacerror;
                bool loginbool = wsrbac.Login(Account, PWD, out rbacerror);
                if (!loginbool)
                {
                    ZhiFang.Common.Log.Log.Debug("DownLoadReportFormToOther_PKI.登录验证错误，可能是用户名密码错误！" + rbacerror);
                    brdv.success = false;
                    brdv.ErrorInfo = "登录验证错误，可能是用户名密码错误！";
                    return brdv;
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
                        DataSet dsuser = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(userinfostr);

                        string deptxml = wsrbac.getUserOrgInfo(dsuser.Tables[0].Rows[0]["Account"].ToString());
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
                        ZhiFang.Common.Log.Log.Debug("DownLoadReportFormToOther_PKI.根据账户密码未找到相关送检单位，可能是未配置。");
                        brdv.success = false;
                        brdv.ErrorInfo = "根据账户密码未找到相关送检单位，可能是未配置！";
                        return brdv;
                    }
                }
                #endregion
                DataSet dsReportFormFull = new DataSet();
                Model.ReportFormFull tempModel = new Model.ReportFormFull();

                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIKESEARCH") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIKESEARCH").Trim() != "")
                {
                    tempModel.LIKESEARCH = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LIKESEARCH").Trim();
                }
                tempModel.CLIENTNO = ClientNo;
                if (Barcode != null && Barcode.Trim() != "")
                {
                    tempModel.SERIALNO = Barcode;
                }
                if (ReportTimeStart != null && ReportTimeStart.Trim() != "")
                {
                    tempModel.CheckStartDate = Convert.ToDateTime(ReportTimeStart);
                }
                if (ReportTimeEnd != null && ReportTimeEnd.Trim() != "")
                {
                    tempModel.CheckEndDate = Convert.ToDateTime(ReportTimeEnd); ;
                }
                dsReportFormFull = ibrff.GetList(10000, tempModel, "  RECEIVEDATE desc ");
                if (((dsReportFormFull != null) && (dsReportFormFull.Tables.Count > 0)) && (dsReportFormFull.Tables[0].Rows.Count > 0))
                {
                    DataSet tmpds = new DataSet();
                    DataTable dtReportFormFull = new DataTable("ReportFormList");
                    dtReportFormFull.Columns.Add("ReportFormId");
                    IBBClientPara ibbcp = BLLFactory<IBBClientPara>.GetBLL("BaseDictionary.BBClientPara");
                    B_ClientPara bcp = ibbcp.GetModel(long.Parse(ClientNo), "ReportFormRowRule");
                    DataRow[] drlist = null;
                    if (bcp != null && bcp.ParaValue != null && bcp.ParaValue != "")
                    {
                        drlist = dsReportFormFull.Tables[0].Select(bcp.ParaValue);
                    }
                    else
                    {
                        drlist = dsReportFormFull.Tables[0].Select();
                    }
                    for (int i = 0; i < drlist.Length; i++)
                    {
                        DataRow dr = dtReportFormFull.NewRow();
                        dr["ReportFormId"] = drlist[i]["ReportFormId"].ToString();
                        dtReportFormFull.Rows.Add(dr);
                    }
                    if (((dtReportFormFull != null) && dtReportFormFull.Rows.Count > 0))
                    {
                        List<Model.Other.ReportFormFull_VO> listReportFormFull = ZhiFang.Tools.ListTool.GetListColumnsStatic<Model.Other.ReportFormFull_VO>(dtReportFormFull);
                        brdv.success = true;
                        brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(listReportFormFull);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormToOther_PKI.dtReportFormFull查不到数据!");
                        brdv.success = false;
                        brdv.ErrorInfo = "查不到数据。";
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("DownLoadReportFormToOther_PKI.ReportFormFull查不到数据!");
                    brdv.success = false;
                    brdv.ErrorInfo = "查不到数据。";
                }

                return brdv;
            }

            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("DownLoadReportFormToOther_PKI:下载报告异常" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "下载报告异常!请查看服务端日志。";
                return brdv;
            }
            return brdv;
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DownLoadReportFormInfoToOther_PKI?Account={Account}&PWD={PWD}&ReportFormId={ReportFormId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue DownLoadReportFormInfoToOther_PKI(string Account, string PWD, string ReportFormId)
        {
            Log.Info("DownLoadReportFormInfoToOther_PKI.调用。");
            BaseResultDataValue brdv = new BaseResultDataValue();
            string ClientNo = "";
            WSRBAC_Service.WSRbacSoapClient wsrbac = null;

            try
            {
                #region 验证参数
                if (Account == null || Account.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "账户为空！";
                    ZhiFang.Common.Log.Log.Error("DownLoadReportFormInfoToOther_PKI:账户为空！");
                    return brdv;
                }
                if (PWD == null || PWD.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "密码为空！";
                    ZhiFang.Common.Log.Log.Error("DownLoadReportFormInfoToOther_PKI:密码为空！");
                    return brdv;
                }

                if (ReportFormId == null || ReportFormId.Trim() == "")
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "报告单ID为空！";
                    ZhiFang.Common.Log.Log.Error("DownLoadReportFormInfoToOther_PKI:报告单ID为空！");
                    return brdv;
                }
                #endregion
                ZhiFang.Common.Log.Log.Debug("DownLoadReportFormInfoToOther_PKI.参数:Account:" + Account + "@PWD:" + PWD + "@ReportFormId:" + ReportFormId);
                #region 初始化权限服务
                try
                {
                    wsrbac = new WSRBAC_Service.WSRbacSoapClient();
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("DownLoadReportFormInfoToOther_PKI.未能初始化权限服务:" + ex.ToString());
                    brdv.success = false;
                    brdv.ErrorInfo = "权限异常！";
                    return brdv;
                }
                #endregion

                #region 登录验证
                string rbacerror;
                bool loginbool = wsrbac.Login(Account, PWD, out rbacerror);
                if (!loginbool)
                {
                    ZhiFang.Common.Log.Log.Debug("DownLoadReportFormInfoToOther_PKI.登录验证错误，可能是用户名密码错误！" + rbacerror);
                    brdv.success = false;
                    brdv.ErrorInfo = "登录验证错误，可能是用户名密码错误！";
                    return brdv;
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
                        DataSet dsuser = ZhiFang.Common.Public.XmlToData.CXmlToDataSet(userinfostr);

                        string deptxml = wsrbac.getUserOrgInfo(dsuser.Tables[0].Rows[0]["Account"].ToString());
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
                        ZhiFang.Common.Log.Log.Debug("DownLoadReportFormInfoToOther_PKI.根据账户密码未找到相关送检单位，可能是未配置。");
                        brdv.success = false;
                        brdv.ErrorInfo = "根据账户密码未找到相关送检单位，可能是未配置！";
                        return brdv;
                    }
                }
                #endregion

                Model.Other.ReportFormFull_VO tmpReportFormFull_VO = new Model.Other.ReportFormFull_VO();
                IBBClientPara ibbcp = BLLFactory<IBBClientPara>.GetBLL("BaseDictionary.BBClientPara");
                ZhiFang.Common.Log.Log.Info("DownLoadReportFormInfoToOther_PKI.开始查询ReportFormFull");
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
                        tmpReportFormFull_VO = ZhiFang.Tools.ListTool.GetListColumnsStatic<Model.Other.ReportFormFull_VO>(dstmp.Tables[0])[0];
                    }
                    else
                    {
                        tmpReportFormFull_VO = ZhiFang.Tools.ListTool.GetListColumnsStatic<Model.Other.ReportFormFull_VO>(dsreportform.Tables[0])[0];
                    }

                    #region 开始查询ReportItemFull
                    ZhiFang.Common.Log.Log.Info("DownLoadReportFormInfoToOther_PKI.开始查询ReportItemFull");
                    DataSet DsReportItem = ibv.GetViewData(-1, "ReportItemFull", "  ReportFormID='" + ReportFormId + "' ", "");
                    tmpReportFormFull_VO.ReportItemList = new List<ReportItemFull>();
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
                        tmpReportFormFull_VO.ReportItemList = ZhiFang.Tools.ListTool.GetListColumnsStatic<ReportItemFull>(DsReportItem.Tables[0]);
                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormInfoToOther_PKI.查询到ReportItemList记录总数:" + DsReportItem.Tables[0].Rows.Count);                       
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormInfoToOther_PKI.ReportItemList记录为0");
                    }
                    #endregion


                    #region 开始查询ReportMicroFull
                    ZhiFang.Common.Log.Log.Info("QueryReportInfo_PKI_PWD.开始查询ReportMicroFull");
                    DataSet dsReportMicroFull = ibv.GetViewData(-1, "ReportMicroFull", "  ReportFormID='" + ReportFormId + "' ", "");
                    tmpReportFormFull_VO.ReportMicroList = new List<ReportMicroFull>();
                    if (dsReportMicroFull != null && dsReportMicroFull.Tables[0].Rows.Count > 0)
                    {
                        dsReportMicroFull.Tables[0].TableName = "ReportMicroInfo";
                        bcp = ibbcp.GetModel(long.Parse(dsreportform.Tables[0].Rows[0]["CLIENTNO"].ToString().Trim()), "ReportMicroColumnRule");
                        if (bcp != null && bcp.ParaValue != null && bcp.ParaValue.Trim() != null)
                        {
                            DataTableHelper.SetTableColumn(dsReportMicroFull.Tables[0], bcp.ParaValue.Trim());
                        }
                        tmpReportFormFull_VO.ReportMicroList = ZhiFang.Tools.ListTool.GetListColumnsStatic<ReportMicroFull>(dsReportMicroFull.Tables[0]);
                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormInfoToOther_PKI.查询到ReportMicroList记录总数:" + dsReportMicroFull.Tables[0].Rows.Count);                       
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormInfoToOther_PKI.ReportMicroList记录为0");
                    }
                    #endregion


                    #region 开始查询ReportMarrowFull
                    ZhiFang.Common.Log.Log.Info("DownLoadReportFormInfoToOther_PKI.开始查询ReportMarrowFull");
                    DataSet dsReportMarrowFull = ibv.GetViewData(-1, "ReportMarrowFull", "  ReportFormID='" + ReportFormId + "' ", "");
                    tmpReportFormFull_VO.ReportMarrowList = new List<ReportMarrowFull>();
                    if (dsReportMarrowFull != null && dsReportMarrowFull.Tables[0].Rows.Count > 0)
                    {
                        dsReportMarrowFull.Tables[0].TableName = "ReportMarrowInfo";
                        bcp = ibbcp.GetModel(long.Parse(dsreportform.Tables[0].Rows[0]["CLIENTNO"].ToString().Trim()), "ReportMarrowColumnRule");
                        if (bcp != null && bcp.ParaValue != null && bcp.ParaValue.Trim() != null)
                        {
                            DataTableHelper.SetTableColumn(dsReportMarrowFull.Tables[0], bcp.ParaValue.Trim());
                        }
                        tmpReportFormFull_VO.ReportMarrowList = ZhiFang.Tools.ListTool.GetListColumnsStatic<ReportMarrowFull>(dsReportMicroFull.Tables[0]);
                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormInfoToOther_PKI.查询到ReportMarrowList记录总数:" + dsReportMarrowFull.Tables[0].Rows.Count);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormInfoToOther_PKI.ReportMarrowList记录为0");
                    }
                    #endregion
                    brdv.success = true;
                    brdv.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tmpReportFormFull_VO);
                }
                
                return brdv;
            }

            catch (Exception e)
            {
                ZhiFang.Common.Log.Log.Error("DownLoadReportFormInfoToOther_PKI:下载报告异常" + e.ToString());
                brdv.success = false;
                brdv.ErrorInfo = "下载报告异常!请查看服务端日志。";
                return brdv;
            }
        }

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DownLoadReportFormPDFFiles?Account={Account}&PWD={PWD}&ReportFormId={ReportFormId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public Stream DownLoadReportFormPDFFiles(string Account, string PWD, string ReportFormId)
        {
            FileStream fileStream = null;
            BaseResultDataValue brdv = new BaseResultDataValue();
            string UploadDate = null;
            string TestDate = null;
            try
            {
                IBLL.Report.Other.IBView ibv = BLLFactory<IBLL.Report.Other.IBView>.GetBLL("Other.BView");
                try
                {
                    string pdfPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportConfigPath");
                    if (string.IsNullOrEmpty(pdfPath))
                    {
                        brdv.ErrorInfo = "无报告文件!";
                        ZhiFang.Common.Log.Log.Error("DownLoadReportFormPDFFiles.无报告文件pdfPath:" + pdfPath);
                        brdv.success = false;
                        byte[] bbrdv = Encoding.UTF8.GetBytes(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv));
                        MemoryStream memoryStream = new MemoryStream(bbrdv);
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                        return memoryStream;
                    }
                    if (!string.IsNullOrEmpty(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")))
                    {
                        pdfPath = pdfPath + "\\" + Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir");
                    }
                    if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("isUpLoadReport") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("isUpLoadReport").ToString() == "1")
                    {
                        pdfPath = pdfPath + "\\Report";
                    }

                    ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDFFiles.开始读取PDF文件");
                    DataSet ds = ibv.GetViewData(-1, "ReportFormFull", "  ReportFormID='" + ReportFormId + "' ", "");
                    if (ds != null && ds.Tables[0].Rows.Count > 0)
                    {
                        UploadDate = ds.Tables[0].Rows[0]["UploadDate"] == null ? null : (DateTime.Parse(ds.Tables[0].Rows[0]["UploadDate"].ToString())).ToString("yyyy/MM/dd");
                        if (UploadDate != null)
                        {
                            ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDFFiles.UploadDate:" + UploadDate + ",根据UploadDate年月日查找PDF文件所在目录");
                            TestDate = UploadDate.Split(' ')[0];

                        }
                        else
                        {
                            ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDFFiles.UploadDate为空,尝试取今天日期查找PDF文件所在目录");
                            TestDate = DateTime.Now.ToString("yyyy/MM/dd");
                        }
                        string Year = TestDate.Split('/')[0];
                        int Month = int.Parse(TestDate.Split('/')[1]);
                        int Day = int.Parse(TestDate.Split('/')[2]);
                        pdfPath = pdfPath + "\\" + Year + "\\" + Month + "\\" + Day + "\\";
                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDFFiles.报告:" + ReportFormId + " 所在目录:" + pdfPath);
                        foreach (string pdfFile in Directory.GetFiles(pdfPath, "*.pdf"))
                        {
                            string FileName = Path.GetFileName(pdfFile);
                            //ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDFFiles.找到PDF文件路径:" + pdfFile);
                            ReportFormId = ReportFormId.Replace(':', '：');//替换成中文的冒号,因为英文格式的冒号在文件名里面是非法的
                            if (FileName.IndexOf(ReportFormId) > -1)
                            {
                                pdfPath += FileName;
                                ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDFFiles.成功找到PDF文件路径:" + pdfPath);
                                //c#文件流读文件

                                fileStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read);

                                Encoding code = Encoding.GetEncoding("gb2312");
                                System.Web.HttpContext.Current.Response.Charset = "gb2312";
                                System.Web.HttpContext.Current.Response.ContentEncoding = code;


                                ReportFormId = EncodeFileName.ToEncodeFileName(ReportFormId+ ".pdf");
                                System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + ReportFormId + "\"");
                                break;
                            }
                            
                        }

                        if (fileStream == null)
                        {
                            ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDFFiles.找不到PDF报告!");
                            brdv.ErrorInfo = "找不到PDF报告!" ;
                            brdv.success = false;
                            byte[] bbrdv = Encoding.UTF8.GetBytes(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv));
                            MemoryStream memoryStream = new MemoryStream(bbrdv);
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                            return memoryStream;
                        }
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("DownLoadReportFormPDFFiles.不存在ReportFormFull记录:" + ReportFormId);
                        brdv.ErrorInfo = "不存在ReportFormFull记录:" + ReportFormId;
                        brdv.success = false;
                        byte[] bbrdv = Encoding.UTF8.GetBytes(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv));
                        MemoryStream memoryStream = new MemoryStream(bbrdv);
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                        return memoryStream;
                    }
                    return fileStream;
                }
                catch (Exception ex)
                {
                    
                    brdv.ErrorInfo = "获取PDF文件出错!";
                    ZhiFang.Common.Log.Log.Error("DownLoadReportFormPDFFiles.获取PDF文件出错：" + ex.ToString());
                    brdv.success = false;

                    byte[] bbrdv = Encoding.UTF8.GetBytes(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv));
                    MemoryStream memoryStream = new MemoryStream(bbrdv);
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                    return memoryStream;
                }
            }
            catch (Exception ex)
            {
                //fileStream = null;
                ZhiFang.Common.Log.Log.Error("任务管理附件下载错误:" + ex.Message);
                //throw new Exception(ex.Message);
            }
            return fileStream;
        }
    }
}
