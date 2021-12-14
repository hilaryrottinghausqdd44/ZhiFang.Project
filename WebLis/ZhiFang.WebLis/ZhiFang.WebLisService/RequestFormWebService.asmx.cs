using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using ZhiFang.Common.Log;
using System.Xml;
using System.Data;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.IBLL.Common;
using System.IO;
using ZhiFang.Common.Public;
using System.Text;
using ZhiFang.BLL.Common;
using System.Dynamic;

namespace ZhiFang.WebLisService
{
    /// <summary>
    /// RequestFormService1 的摘要说明
    /// </summary>
    [WebService(Namespace =(true)? "http://tempuri.org/" : "http://zhifang.com.cn/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class RequestFormWebService : System.Web.Services.WebService
    {
        private readonly IBNRequestForm ibnrf = BLLFactory<IBNRequestForm>.GetBLL("NRequestForm");
        private readonly IBNRequestItem ibnri = BLLFactory<IBNRequestItem>.GetBLL("NRequestItem");
        private readonly IBTestItemControl ibtic = BLLFactory<IBTestItemControl>.GetBLL("BaseDictionary.TestItemControl");
        private readonly IBSampleTypeControl ibstc = BLLFactory<IBSampleTypeControl>.GetBLL("BaseDictionary.SampleTypeControl");
        private readonly IBGenderTypeControl ibgtc = BLLFactory<IBGenderTypeControl>.GetBLL("BaseDictionary.GenderTypeControl");
        private readonly IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
        private readonly IBBusinessLogicClientControl iblcc = BLLFactory<IBBusinessLogicClientControl>.GetBLL();
        IBRequestData ibrd = BLLFactory<IBRequestData>.GetBLL("RequestData");
        IBReportData ibr = BLLFactory<IBReportData>.GetBLL("ReportData");
        RequestFormService requestformservice_svc = new RequestFormService();

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        [WebMethod(Description = "申请单下载")]
        public bool DownloadBarCode(
          string DestiOrgID,              //外送(至)单位(独立实验室编号)
          string BarCodeNo,               //条码码
          XmlNode WebLiser,               //下载人的其他信息，下载人姓名，地点，时间等等扩展信息(本次先不开发)
          out XmlNode nodeBarCode,        //一个条码XML
          out XmlNode nodeNRequestItem,   //多少个项目
          out XmlNode nodeNRequestForm,   //多少个申请单
          out string xmlWebLisOthers,     //返回更多信息
          out string ReturnDescription)   //其他描述
        {
            nodeBarCode = null;
            nodeNRequestForm = null;
            nodeNRequestItem = null;
            xmlWebLisOthers = null;
            ReturnDescription = "";
            ZhiFang.Common.Log.Log.Info(String.Format("下载申请开始DestiOrgID={0},BarCodeNo={1}", DestiOrgID, BarCodeNo));
            ////---------------------------------------------------------------------------------------------------------
            //-----
            if (DestiOrgID == null
                || DestiOrgID == ""
                || BarCodeNo == null
                || BarCodeNo == "")
            {
                ReturnDescription = "外送单位,与标本条码号不能为空";
                ZhiFang.Common.Log.Log.Error("外送单位,与标本条码号不能为空");
                return false;
            }
            string str = "";
            DataSet dsBarCodeForm = ibbcf.GetList(new Model.BarCodeForm() { BarCode = BarCodeNo });
            if (!ibbcf.CheckBarCodeCenter(dsBarCodeForm, DestiOrgID, out str))
            {
                ReturnDescription += str;
                return false;
            }
            //+ "' and WebLisOrgID='" + DestiOrgID + "'");
            //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID
            //WebLisSourceOrgID=SourceOrgID
            //ZhiFang.Common.Log.Log.Info("执行查询语句:" + strsql);

            if (!(((dsBarCodeForm != null) && (dsBarCodeForm.Tables.Count > 0)) && (dsBarCodeForm.Tables[0].Rows.Count > 0)))
            {
                ReturnDescription = "未找到条码号为[" + BarCodeNo + "]的数据";
                ZhiFang.Common.Log.Log.Error("未找到条码号为[" + BarCodeNo + "]的数据");
                return false;
            }

            //if (dsBarCodeForm.Tables[0].Rows.Count == 0)
            //{
            //    ReturnDescription = "未找到条码号为[" + BarCodeNo + "]的数据";
            //    return false;
            //}
            try
            {

                string PreviouseWebLisFlag = "0";
                Log.Error("1");
                if (!Convert.IsDBNull(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"]))
                {
                    PreviouseWebLisFlag = dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"].ToString().Trim();
                }
                Log.Error("2");
                if (PreviouseWebLisFlag == "5" || PreviouseWebLisFlag == "3"
                    || Convert.ToInt32(PreviouseWebLisFlag) > 6)
                //|| PreviouseWebLisFlag == "7"
                //|| PreviouseWebLisFlag == "8"
                //|| PreviouseWebLisFlag == "10")
                {
                    ReturnDescription = "数据编号[" + BarCodeNo + "]不能核收，目前状态为[" + PreviouseWebLisFlag + "]";
                    Log.Error("数据编号[" + BarCodeNo + "]不能核收，目前状态为[" + PreviouseWebLisFlag + "]");
                    return false;
                }
                Log.Error("3");
                //这里要讨论决定, barcodeFormNo,NRequestFormNo等重新生成唯一号，用于NRequestItem关联
                string strBarCodeFormNo = dsBarCodeForm.Tables[0].Rows[0]["BarCodeFormNo"].ToString();
                if (strBarCodeFormNo == "")
                {
                    ReturnDescription = "BarCodeFormNo为空";
                    Log.Error("BarCodeFormNo为空,程序退出");
                    return false;
                }
                Log.Error("4");
                //strsql = "select * from NRequestItem where BarCodeFormNo=" + strBarCodeFormNo;
                DataSet dsItem = ibnri.GetList(new Model.NRequestItem() { BarCodeFormNo = long.Parse(strBarCodeFormNo) });
                if (!ibnri.CheckNReportItemCenter(dsItem, DestiOrgID, out str))
                {
                    ReturnDescription += str;
                    return false;
                }
                Log.Error("5");
                if (!((dsBarCodeForm != null) && (dsBarCodeForm.Tables.Count > 0) && (dsBarCodeForm.Tables[0].Rows.Count > 0)))
                {
                    ReturnDescription = String.Format("未找到该条码所对应的项目，BarCodeFormNo:{0}", strBarCodeFormNo);
                    Log.Error(String.Format("未找到该条码所对应的项目，BarCodeFormNo:{0}", strBarCodeFormNo));
                    return false;
                }
                //-----
                //strsql = "select * from NRequestForm where NRequestFormNo='" + dsItem.Tables[0].Rows[0]["NRequestFormNo"].ToString() + "'";
                DataSet dsForm = ibnrf.GetList(new Model.NRequestForm() { NRequestFormNo = long.Parse(dsItem.Tables[0].Rows[0]["NRequestFormNo"].ToString()) });
                Log.Error("6");
                #region 条码表中的样本类型号，赋到申请单表。样本号都以条码为准
                try
                {
                    if (!dsForm.Tables[0].Columns.Contains("SampleTypeNo"))
                    {
                        dsForm.Tables[0].Columns.Add("SampleTypeNo");
                        foreach (DataRow dr in dsForm.Tables[0].Rows)
                        {
                            dr["SampleTypeNo"] = dsBarCodeForm.Tables[0].Rows[0]["SampleTypeNo"];
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in dsForm.Tables[0].Rows)
                        {
                            dr["SampleTypeNo"] = dsBarCodeForm.Tables[0].Rows[0]["SampleTypeNo"];
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                #endregion
                Log.Error("7");
                if (!ibnrf.CheckNReportFormCenter(dsForm, DestiOrgID, out str))
                {
                    ReturnDescription += str;
                    return false;
                }
                Log.Error("8");
                dsBarCodeForm = MatchClientNo(dsBarCodeForm, DestiOrgID);
                dsItem = MatchClientNo(dsItem, DestiOrgID);
                dsForm = MatchClientNo(dsForm, DestiOrgID);
                int row = ibrd.UpdateWebLisOrgID(dsBarCodeForm, DestiOrgID);
                row = ibrd.UpdateItemWebLisOrgID(dsItem, DestiOrgID);
                Log.Error("9");
                if (row > 0)
                {
                    Log.Error("10");
                    //-----
                    XmlDocument docBarCode = new XmlDocument();     //条码信息
                    docBarCode.LoadXml(dsBarCodeForm.GetXml());
                    nodeBarCode = docBarCode.DocumentElement;
                    //-----
                    XmlDocument docNRequestItem = new XmlDocument();   //项目信息
                    docNRequestItem.LoadXml(dsItem.GetXml());
                    nodeNRequestItem = docNRequestItem.DocumentElement;
                    //-----
                    XmlDocument docNRequestForm = new XmlDocument();
                    docNRequestForm.LoadXml(dsForm.GetXml());
                    nodeNRequestForm = docNRequestForm.DocumentElement;
                    string DownloadBarCodeFlag_ReturnDescription;
                    DownloadBarCodeFlag(DestiOrgID, BarCodeNo, null, out DownloadBarCodeFlag_ReturnDescription);
                }

            }
            catch (Exception ex)
            {
                ReturnDescription += "下载申请失败" + ex.Message;
                Log.Error("下载申请失败" + ex.ToString());
                return false;
            }
            ////---------------------------------------------------------------------------------------------------------
            return true;
        }
        [WebMethod(Description = "样本签收标志")]
        public bool DownloadBarCodeFlag(
           string DestiOrgID,              //外送(至)单位(独立实验室编号)
           string BarCodeNo,               //条码码
           XmlNode WebLiser,               //操作人的更多信息
           out string ReturnDescription)   //其他描述
        {
            try
            {
                if (ibbcf.UpdateWebLisFlagByBarCode("5", BarCodeNo, DestiOrgID) > 0)
                {
                    ReturnDescription = "打签收标志成功！";
                    Log.Info("打签收标志成功！DestiOrgID=" + DestiOrgID + ",BarCodeNo=" + BarCodeNo);
                    return true;
                }
                else
                {
                    ReturnDescription = "打签收标志失败！";
                    Log.Error("打签收标志失败！DestiOrgID=" + DestiOrgID + ",BarCodeNo=" + BarCodeNo);
                    return false;
                }
            }
            catch (Exception e)
            {
                ReturnDescription = "打签收标志失败" + e.Message;
                Log.Error("打签收标志败" + e.Message);
                return false;
            }
        }
        [WebMethod(Description = "取消下载")]
        public bool DownloadBarCodeCancel(
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            XmlNode WebLiser,               //操作人的更多信息
            out string ReturnDescription)   //其他描述
        {
            Log.Info("打取消下载标志Start。");
            if (ibbcf.UpdateByBarCode(new Model.BarCodeForm() { WebLisFlag = 0, BarCode = BarCodeNo, WebLisOrgID = DestiOrgID }) > 0)
            {
                ReturnDescription = "打取消下载标志成功！";
                Log.Info("打取消下载标志成功！DestiOrgID=" + DestiOrgID + ",BarCodeNo=" + BarCodeNo);
                return true;
            }
            else
            {
                ReturnDescription = "打取消签收标志失败！";
                Log.Error("取消签收标志失败！DestiOrgID=" + DestiOrgID + ",BarCodeNo=" + BarCodeNo);
                return false;
            }

        }
        [WebMethod(Description = "样本退回")]
        public bool RefuseDownloadBarCode(
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            XmlNode WebLiser,               //操作人的更多信息
            out string ReturnDescription)   //其他描述
        {
            try
            {
                if (ibbcf.UpdateByBarCode(new Model.BarCodeForm() { WebLisFlag = 6, BarCode = BarCodeNo, WebLisOrgID = DestiOrgID }) > 0)
                {
                    ReturnDescription = "退回成功！";
                    Log.Info("退回成功！DestiOrgID=" + DestiOrgID + ",BarCodeNo=" + BarCodeNo);
                    return true;
                }
                else
                {
                    ReturnDescription = "退回失败！";
                    Log.Error("退回失败！DestiOrgID=" + DestiOrgID + ",BarCodeNo=" + BarCodeNo);
                    return false;
                }
            }
            catch (Exception e)
            {
                ReturnDescription = "退回失败" + e.Message;
                Log.Error("退回失败" + e.Message);
                return false;
            }
        }
        [WebMethod(Description = "申请单上传")]
        public bool UpgradeRequestForm(
         string SourceOrgID,             //送检(源)单位
         string DestiOrgID,              //外送(至)单位
         string BarCodeNo,               //条码码
         string nodeBarCodeForm,
         string nodeNRequestForm,
         string nodeNRequestItem,
         string nodeOthers,
         out string WebLisFlag,
         out string ReturnDescription)
        {
            Log.Info("RequestFormWebService.asmx.UpgradeRequestForm.开始.");
            WebLisFlag = "";
            ReturnDescription = "";
            //return false;
            Log.Info(String.Format("上传申请开始SourceOrgID={0},DestiOrgID={1},BarCodeNo={2}", SourceOrgID, DestiOrgID, BarCodeNo));
            WebLisFlag = "0";
            ReturnDescription = "";
            #region 判断部分
            if (SourceOrgID == null
                || SourceOrgID == ""
                || DestiOrgID == null
                || DestiOrgID == ""
                || BarCodeNo == null
                || BarCodeNo == "")
            {
                ReturnDescription = "送检单位,外送单位,与标本条码号不能为空";
                return false;
            }
            Model.BarCodeForm barCodeForm = new Model.BarCodeForm();
            barCodeForm.BarCode = BarCodeNo;
            barCodeForm.WebLisOrgID = SourceOrgID;
            DataSet dsBarCodeForm = ibbcf.GetList(barCodeForm);
            //DataSet dsBarCodeForm = sqlDB.ExecDS("select top 1 * from barcodeForm where BarCode='"
            //    + BarCodeNo + "' and WebLisSourceOrgID='" + SourceOrgID + "'");
            //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID
            if (dsBarCodeForm == null || dsBarCodeForm.Tables.Count == 0)
            {
                ReturnDescription = "barcodeForm出错，请检查";
                return false;
            }
            //判断是新增加条码，还是再次上传更新数据
            bool bAddNew = false;
            if (dsBarCodeForm.Tables[0].Rows.Count == 0)
                bAddNew = true;
            else
            {
                string PreviouseWebLisFlag = "0";
                if (!Convert.IsDBNull(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"]))
                    PreviouseWebLisFlag = dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"].ToString().Trim();
                if (PreviouseWebLisFlag == "5"
                    || Convert.ToInt32(PreviouseWebLisFlag) > 6)
                {
                    ReturnDescription = "数据编号[" + BarCodeNo + "]不能再次上传，目前状态为[" + PreviouseWebLisFlag + "]";
                    WebLisFlag = PreviouseWebLisFlag;
                    return false;
                }
            }
            #endregion
            #region 读取数据集部分
            string barcodeFormNo = ibbcf.GetNewBarCodeFormNo(Convert.ToInt32(SourceOrgID));
            StringReader strTemp = new StringReader(nodeBarCodeForm);
            DataSet wsBarCode = new DataSet();
            wsBarCode.ReadXml(strTemp);
            strTemp = new StringReader(nodeNRequestItem);
            DataSet wsNRequestItem = new DataSet();
            wsNRequestItem.ReadXml(strTemp);
            if (wsNRequestItem == null && wsNRequestItem.Tables.Count == 0)
            {
                ReturnDescription += String.Format("未获取到任何BarCode={0}的项目数据", BarCodeNo);
                Log.Error(String.Format("未获取到任何BarCode={0}的项目数据", BarCodeNo));
                return false;
            }
            else
            {
                Log.Info(String.Format("获取到项目数据{0}条", wsNRequestItem.Tables[0].Rows.Count));
            }
            strTemp = new StringReader(nodeNRequestForm);
            DataSet wsNRequestForm = new DataSet();
            wsNRequestForm.ReadXml(strTemp);
            if (wsNRequestForm == null && wsNRequestForm.Tables.Count == 0)
            {
                ReturnDescription += String.Format("未获取到任何BarCode={0}的申请单数据", BarCodeNo);
                Log.Error(String.Format("未获取到任何BarCode={0}的申请单数据", BarCodeNo));
                return false;
            }
            else
            {
                Log.Info(String.Format("获取到申请单数据{0}条", wsNRequestForm.Tables[0].Rows.Count));
            }
            string str = "";
            if (!ibbcf.CheckBarCodeLab(wsBarCode, SourceOrgID, out str))
            {
                ReturnDescription += str;
                return false;
            }
            if (!ibnrf.CheckNReportFormLab(wsNRequestForm, SourceOrgID, out str))
            {
                ReturnDescription += str;
                return false;
            }
            if (!ibnri.CheckNReportItemLab(wsNRequestItem, SourceOrgID, out str))
            {
                ReturnDescription += str;
                return false;
            }
            wsBarCode = MatchCenterNo(wsBarCode, SourceOrgID);
            wsNRequestForm = MatchCenterNo(wsNRequestForm, SourceOrgID);
            wsNRequestItem = MatchCenterNo(wsNRequestItem, SourceOrgID);
            #endregion
            try
            {
                ibrd.UpdateBarCode(barcodeFormNo, wsBarCode, SourceOrgID, DestiOrgID);
                ibrd.UpdateNRequestForm(wsNRequestForm, SourceOrgID, DestiOrgID);
                ibrd.UpdateNRequestItem(barcodeFormNo, wsNRequestItem, wsNRequestForm, SourceOrgID, DestiOrgID);
                int result = ibrd.SaveWebLisData(BarCodeNo, wsBarCode, wsNRequestItem, wsNRequestForm);
            }
            catch (Exception ex)
            {
                ReturnDescription += ex.Message;
                Log.Error(ex.Message);
                return false;
            }
            return true;
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
                string B_Lab_tableName = "";
                string B_Lab_controlTableName = "";
                switch (str)
                {
                    case "SAMPLETYPENO":
                        B_Lab_tableName = "B_Lab_SampleType";
                        B_Lab_controlTableName = "B_SampleTypeControl";
                        break;
                    case "GENDERNO":
                        B_Lab_tableName = "b_lab_GenderType";
                        B_Lab_controlTableName = "B_GenderTypeControl";
                        break;
                    case "FOLKNO":
                        B_Lab_tableName = "B_Lab_FolkType";
                        B_Lab_controlTableName = "B_FolkTypeControl";
                        break;
                    case "ITEMNO":
                        B_Lab_tableName = "B_Lab_TestItem";
                        B_Lab_controlTableName = "B_TestItemControl";
                        break;
                    case "SUPERGROUPNO":
                        B_Lab_tableName = "B_Lab_SuperGroup";
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
                        else
                        {
                            string str1 = "";
                            if (str.IndexOf('N') > -1)
                            {
                                str1 = str.Substring(0, str.Length - 2);
                            }
                            if (ds.Tables[0].Columns.Contains(str1 + "Name"))
                            {
                                if (ds.Tables[0].Rows[count][str1 + "Name"].ToString() != "")
                                {
                                    ListStrName.Add(ds.Tables[0].Rows[count][str1 + "Name"].ToString());
                                    if (ListStrName.Count > 0)
                                    {
                                        DataSet dsLabNo = ibr.GetLabNo(B_Lab_tableName, ListStrName, SourceOrgID, str);
                                        for (int j = 0; j < dsLabNo.Tables[0].Rows.Count; j++)
                                        {
                                            if (B_Lab_tableName != "ITEMNO")
                                            {
                                                ListStr.Add(dsLabNo.Tables[0].Rows[j]["lab" + str].ToString());
                                            }
                                            else
                                                ListStr.Add(dsLabNo.Tables[0].Rows[j][str].ToString());
                                        }
                                    }
                                }
                            }

                        }
                    }
                    if (ListStr.Count != 0)
                    {
                        DataSet CenteNo = ibr.GetCentNo(B_Lab_controlTableName, ListStr, SourceOrgID, str);
                        foreach (DataRow dr in ds.Tables[0].Rows)
                        {
                            foreach (DataRow dritem in CenteNo.Tables[0].Rows)
                            {
                                if (dr[str].ToString() == dritem["Control" + str].ToString() || dr[str].ToString() == "")
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
        /// <summary>
        /// 根据中心端的编码得到实验室的编码
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="SourceOrgID"></param>
        /// <returns></returns>
        public DataSet MatchClientNo(DataSet ds, string SourceOrgID)
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
                    case "COMBIITEMNO":
                        B_Lab_Columns = "ITEMNO";
                        B_Lab_controlTableName = "B_TestItemControl";
                        break;
                    case "SUPERGROUPNO":
                        B_Lab_Columns = "SUPERGROUPNO";
                        B_Lab_controlTableName = "B_SuperGroupControl";
                        break;
                }
                try
                {
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
                            DataSet labNo = ibr.GetLabControlNo(B_Lab_controlTableName, ListStr, SourceOrgID, B_Lab_Columns);
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                foreach (DataRow dritem in labNo.Tables[0].Rows)
                                {
                                    if (dr[str].ToString() == dritem[B_Lab_Columns].ToString() || dr[str].ToString() == "")
                                    {
                                        dr[str] = dritem["Control" + B_Lab_Columns].ToString();
                                        // dr[str] = dritem[str].ToString();
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log.Info(ex.ToString() + ex.StackTrace);
                }
            }
            return ds;
        }

        RequestFormService rfs = new RequestFormService();
        [WebMethod(Description = "申请单上传（芜湖）")]

        public bool UpLoadRequestFormClient(string drs, string orgID, string jzType, out string strMsg)
        {
            Log.Info("RequestFormWebService.asmx.UpLoadRequestFormClient.开始.");
            strMsg = "";
            //return false;
            return rfs.UpLoadRequestFormClient(drs, orgID, jzType, out strMsg);
        }

        [WebMethod(Description = "上传申请")]
        public bool AppliyUpLoad(string xmlData, string orgID, string jzType, out string sMsg)
        {
            //sMsg = "";
            //string msg;
            //ZhiFang.Common.Log.Log.Info("RequestFormWebService->AppliyUpload：传入的字符串:" + xmlData);
            //byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(xmlData);
            //bool resutl = rfs.AppliyUpLoad(byteArray, orgID, jzType, out msg);
            //sMsg = msg;
            //ZhiFang.Common.Log.Log.Info("RequestFormWebService->AppliyUpload：resutl:" + resutl.ToString() + "@sMsg:" + sMsg);
            //return resutl;

            sMsg = "";
            string msg;
            ZhiFang.Common.Log.Log.Info("RequestFormWebService->AppliyUpload：传入的字符串:" + xmlData);
            bool resutl = rfs.AppliyUpLoadStr(xmlData, orgID, jzType, out msg);
            sMsg = msg;
            ZhiFang.Common.Log.Log.Info("RequestFormWebService->AppliyUpload：resutl:" + resutl.ToString() + "@sMsg:" + sMsg);
            return resutl;
        }

        [WebMethod(Description = "上传申请_生成新条码")]
        public bool AppliyUpLoad_CreatNewBarCode(string xmlData, string orgID, string jzType, out string sMsg)
        {
            ZhiFang.Common.Log.Log.Info("RequestFormWebService.asmx.AppliyUpLoad_CreatNewBarCode.开始.");
            sMsg = "";
            //return false;
            string msg;
            ZhiFang.Common.Log.Log.Info("RequestFormWebService->AppliyUpLoad_CreatNewBarCode：传入的字符串:" + xmlData);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(xmlData);
            bool resutl = rfs.AppliyUpLoad_CreatNewBarCode(byteArray, orgID, jzType, out msg);
            sMsg = msg;
            ZhiFang.Common.Log.Log.Info("RequestFormWebService->AppliyUpLoad_CreatNewBarCode：resutl:" + resutl.ToString() + "@sMsg:" + sMsg);
            return resutl;
        }


        [WebMethod(Description = "上传申请(博尔诚医学检验所)")]
        public bool AppliyUpLoad_BoErCheng(string xmlData, string orgID, out string sMsg)
        {
            ZhiFang.Common.Log.Log.Info("RequestFormWebService.asmx.AppliyUpLoad_BoErCheng.开始.");
            sMsg = "";
            //return false;
            ZhiFang.Common.Log.Log.Info("RequestFormWebService->AppliyUpload：传入的字符串:" + xmlData);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(xmlData);
            return rfs.AppliyUpLoad_BoErCheng(byteArray, orgID, out sMsg);
        }

        #region 申请上传 PKI
        /// <summary>
        /// PKI定制 上传不需要转码
        /// </summary>
        /// <param name="drs"></param>
        /// <param name="orgID"></param>
        /// <param name="jzType"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public bool UpLoadRequestFormClient_PKI(string drs, string orgID, string jzType, out string strMsg)
        {
            ZhiFang.Common.Log.Log.Info("RequestFormWebService.asmx.UpLoadRequestFormClient_PKI.开始.");
            strMsg = "";
            //return false;
            return requestformservice_svc.UpLoadRequestFormClient_PKI(drs, orgID, jzType, out strMsg);
        }
        #endregion

        /// <summary>
        /// 申请单下载RBAC
        /// </summary>
        /// <param name="Account"></param>
        /// <param name="Password"></param>
        /// <param name="BarCodeNo"></param>
        /// <param name="nodeBarCode"></param>
        /// <param name="odeNRequestItem"></param>
        /// <param name="nodeNRequestForm"></param>
        /// <param name="xmlWebLisOthers"></param>
        /// <param name="ReturnDescription"></param>
        /// <returns></returns>
        [WebMethod(Description = "申请单下载RBAC")]
        public bool DownloadBarCodeFormByRBAC(string Account, string Password, string BarCodeNo, out string NRequestFormJSON, out string ReturnDescription)
        {
            NRequestFormJSON = "";
            ReturnDescription = "";
            WSRBAC_Service.WSRbacSoapClient wsrbac = null;
            string PWD = "";
            string DestiOrgID = "";
            if (string.IsNullOrEmpty(Account))
            {
                ReturnDescription = "接检单位账户不能为空！";
                ZhiFang.Common.Log.Log.Error("DownloadBarCodeFormByRBAC.接检单位账户不能为空！");
                return false;
            }
            if (string.IsNullOrEmpty(Password))
            {
                ReturnDescription = "接检单位账户不能为空！";
                ZhiFang.Common.Log.Log.Error("DownloadBarCodeFormByRBAC.外送单位,与标本条码号不能为空");
                return false;
            }
            ZhiFang.Common.Log.Log.Info("DownloadBarCodeFormByRBAC.下载申请开始:Account=" + Account + ",Password=" + Password + ",BarCodeNo=" + BarCodeNo + ",IP=" + IPHelper.GetClientIP());
            if (!string.IsNullOrEmpty(ConfigHelper.GetConfigString("PwdBase64Flag")) && ConfigHelper.GetConfigString("PwdBase64Flag").Trim() == "1")
            {
                PWD = ZhiFang.Common.Public.Base64Help.EncodingString(Password);
                ZhiFang.Common.Log.Log.Info("DownloadBarCodeFormByRBAC.下载申请开始:Password=" + Password + ",Base64DecodePassword=" + PWD + ",IP=" + IPHelper.GetClientIP());
            }
            #region 初始化权限服务
            try
            {
                wsrbac = new WSRBAC_Service.WSRbacSoapClient();
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("DownloadBarCodeFormByRBAC.未能初始化权限服务:" + ex.ToString());
                ReturnDescription = "";
                return false;
            }
            #endregion

            #region 登录验证
            string rbacerror;
            bool loginbool = wsrbac.Login(Account, PWD, out rbacerror);
            if (!loginbool)
            {
                ZhiFang.Common.Log.Log.Debug("DownloadNRequestFormByRBAC.登录验证错误，可能是用户名密码错误！" + rbacerror + ",IP=" + IPHelper.GetClientIP());
                ReturnDescription = "登录验证错误，可能是用户名密码错误！";
                return false;
            }
            #endregion

            DataSet clientds = iblcc.GetList(new ZhiFang.Model.BusinessLogicClientControl() { Account = Account.Trim(), SelectedFlag = true });
            if (clientds == null || clientds.Tables.Count <= 0 || clientds.Tables[0].Rows.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("DownloadBarCodeFormByRBAC.该账户没有设置管理的送检单位！Account：" + Account + ",IP=" + IPHelper.GetClientIP());
                ReturnDescription = "该账户没有设置管理的送检单位！";
                return false;
            }
            List<string> ClientNoList = new List<string>();
            for (int i = 0; i < clientds.Tables[0].Rows.Count; i++)
            {
                ClientNoList.Add(clientds.Tables[0].Rows[i]["ClientNo"].ToString().Trim());
                if (clientds.Tables[0].Rows[i]["Flag"] != DBNull.Value && clientds.Tables[0].Rows[i]["Flag"].ToString().Trim() == "1")
                {
                    DestiOrgID = clientds.Tables[0].Rows[i]["ClientNo"].ToString().Trim();
                }
            }
            if (string.IsNullOrEmpty(DestiOrgID))
            {
                ZhiFang.Common.Log.Log.Debug("DownloadBarCodeFormByRBAC.该账户没有设置所属的接检单位！Account：" + Account + ",IP=" + IPHelper.GetClientIP());
                ReturnDescription = "该账户没有设置所属的接检单位！";
                return false;

            }
            List<string> WebLisFlagList = new List<string>();
            DataSet dsBarCodeForm = ibbcf.GetList(new Model.BarCodeForm() { ClientNoList = ClientNoList, WebLisFlag = 2 });
            if (dsBarCodeForm == null || dsBarCodeForm.Tables.Count <= 0 || dsBarCodeForm.Tables[0].Rows.Count <= 0)
            {
                ZhiFang.Common.Log.Log.Debug("DownloadBarCodeFormByRBAC.暂无申请单！IP=" + IPHelper.GetClientIP());
                ReturnDescription = "暂无申请单！";
                return false;
            }
            try
            {
                List<ZhiFang.Model.BarCodeForm> bcflist = new List<Model.BarCodeForm>();
                bcflist = ibbcf.DataTableToList(dsBarCodeForm.Tables[0]);
                List<string> barcodeformnolist = new List<string>();
                List<Model.UiModel.NrequestCombiItemBarCodeEntity> ncblist = new List<Model.UiModel.NrequestCombiItemBarCodeEntity>();
                int DownloadBarCodeFormLimit = 200;
                if (!string.IsNullOrEmpty(ConfigHelper.GetConfigString("DownloadBarCodeFormLimit")))
                {
                    DownloadBarCodeFormLimit = ConfigHelper.GetConfigInt("DownloadBarCodeFormLimit");
                }
                #region 已下代码待优化，先暂时做演示流程。
                for (int i = 0; i < bcflist.Count()&&i< DownloadBarCodeFormLimit; i++)
                {
                    Model.UiModel.NrequestCombiItemBarCodeEntity ncb = new Model.UiModel.NrequestCombiItemBarCodeEntity();
                    var nrequstformlist = ibnrf.GetListByBarCodeNo(bcflist[i].BarCode);
                    if (nrequstformlist == null && nrequstformlist.Tables.Count <= 0 && nrequstformlist.Tables[0].Rows.Count <=0)
                    {
                        ZhiFang.Common.Log.Log.Debug("DownloadBarCodeFormByRBAC.暂无申请单病人信息！BarCode：" + bcflist[i].BarCode + ",IP=" + IPHelper.GetClientIP());
                        continue;
                    }
                    List<Model.NRequestForm> nrequestformlist = ibnrf.DataTableToList(nrequstformlist.Tables[0]);
                    ncb.NrequestForm = nrequestformlist[0];
                    ncb.BarCodeList = new List<Model.UiModel.UiBarCode>() { new Model.UiModel.UiBarCode() {
                        BarCode=bcflist[i].BarCode,
                        ColorName=bcflist[i].Color,
                        SampleType=bcflist[i].SampleTypeNo.HasValue?bcflist[i].SampleTypeNo.ToString():"",
                        SampleTypeName=bcflist[i].SampleTypeName,
                        ItemList=(!string.IsNullOrEmpty(bcflist[i].ItemNo))?bcflist[i].ItemNo.Split(',').ToList():new List<string>()
                    } };
                    if (ncb.BarCodeList[0].ItemList.Count > 0)
                    {
                        ncb.BarCodeList[0].ItemNameList = new List<string>();
                        DataSet labitemds = ibtic.GetLabCodeNo(DestiOrgID, ncb.BarCodeList[0].ItemList);
                        ZhiFang.Common.Log.Log.Debug("DownloadBarCodeFormByRBAC.ibtic.GetLabCodeNo:DestiOrgID：" + DestiOrgID + ",ncb.BarCodeList[0].ItemList：" + string.Join(",", ncb.BarCodeList[0].ItemList) + ",IP=" + IPHelper.GetClientIP());
                        if (labitemds != null && labitemds.Tables.Count > 0 && labitemds.Tables[0].Rows.Count > 0)
                        {
                            List<string> tmplabitemnolist = new List<string>();
                            ncb.BarCodeList[0].ItemList.ForEach(a =>
                            {
                                DataRow[] dra = labitemds.Tables[0].Select(" ItemNo='" + a + "' ");
                                if (dra != null && dra.Length > 0)
                                {
                                    ncb.BarCodeList[0].ItemNameList.Add(dra[0]["CName"].ToString());
                                    tmplabitemnolist.Add(dra[0]["ControlItemNo"].ToString());
                                }
                                else
                                {
                                    ZhiFang.Common.Log.Log.Debug("DownloadBarCodeFormByRBAC.ibtic.GetLabCodeNo:未找到对应关系：DestiOrgID：" + DestiOrgID + ",a：" +a + ",labitemds：" + labitemds.GetXml()+ ",IP=" + IPHelper.GetClientIP());
                                }
                            });
                            ncb.BarCodeList[0].ItemList = tmplabitemnolist;
                        }
                    }
                    ncblist.Add(ncb);
                    #region 申请项目明细--暂时不开发
                    #endregion


                }
                NRequestFormJSON = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(ncblist);
                #endregion
            }
            catch (Exception ex)
            {
                ReturnDescription += "DownloadBarCodeFormByRBAC.下载申请异常。";
                Log.Error("DownloadBarCodeFormByRBAC.下载申请异常：" + ex.ToString());
                return false;
            }
            return true;
        }
    }
}
