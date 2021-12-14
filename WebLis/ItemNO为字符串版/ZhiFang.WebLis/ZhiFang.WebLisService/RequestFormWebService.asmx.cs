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
using System.Web.Services.Protocols;
using System.ComponentModel;

namespace ZhiFang.WebLisService
{
    /// <summary>
    /// ReportFormWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
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
        IBRequestData ibrd = BLLFactory<IBRequestData>.GetBLL("RequestData");
        IBReportData ibr = BLLFactory<IBReportData>.GetBLL("ReportData");
        RequestFormService requestformservice_svc = new RequestFormService();

        [WebMethod]
        public string HelloWorld(string name)
        {
            Log.Info("RequestFormWebService--HelloWorld:" + name);
            return "Hello World " + name;
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
                #region 条码表中的样本号，赋到申请单表。样本号都以条码为准
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
                if (ibbcf.UpdateWebLisFlagByBarCode("5",BarCodeNo,DestiOrgID) > 0)
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
            return rfs.UpLoadRequestFormClient(drs, orgID, jzType, out  strMsg);
        }

        [WebMethod(Description = "上传申请")]
        public bool AppliyUpLoad(string xmlData, string orgID, string jzType, out string sMsg)
         {
            ZhiFang.Common.Log.Log.Info("RequestFormWebService->AppliyUpload：传入的字符串:" + xmlData);
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(xmlData);
            return rfs.AppliyUpLoad(byteArray, orgID, jzType, out sMsg);
        }

        [WebMethod(Description = "上传申请(博尔诚医学检验所)")]
        public bool AppliyUpLoad_BoErCheng(string xmlData, string orgID,  out string sMsg)
        {
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
            return requestformservice_svc.UpLoadRequestFormClient_PKI(drs, orgID, jzType, out strMsg);
        }
        #endregion
    }
}
