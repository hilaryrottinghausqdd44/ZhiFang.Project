using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.IO;
using ECDS.Common;
using System.Xml;
using System.Data.SqlClient;

namespace ZhiFang.WebLisService.WebService
{
    /// <summary>
    /// ServiceAppUpload 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ServiceAppUpload : System.Web.Services.WebService
    {
        [WebMethod(Description = "申请状态查询")]
        public bool WebLisQueryStatus(
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string BarCodeNo,               //条码码
            string WebLisFlagSource,        //询问前状态
            string xmlOthersWhereClause,      //其他条件 如(XX日期>=2009-10-26 and xx日期<=2009-10-27) xml字符串
            out string WebLisFlag,          //返回状态
            out string xmlWebLisOthers,       //返回更多信息
            out string ReturnDescription)   //其他描述
        {
            WebLisFlag = "0";
            ReturnDescription = "";
            xmlWebLisOthers = null;
            try
            {
                //BarCodeFormNo,BarCode,WebLisFlag,WebLisOpTime,WebLiser,WebLisDescript
                SqlServerDB sqlDB = new SqlServerDB();
                if (BarCodeNo != null && BarCodeNo.Trim() != "")
                {

                    DataSet dsBarCodeForm = sqlDB.ExecDS("select top 1 BarCodeFormNo,BarCode,WebLisFlag,WebLisOpTime,WebLiser,WebLisDescript from barcodeForm where BarCode='"
                        + BarCodeNo + "' and WebLisSourceOrgID='" + SourceOrgID + "'");
                    //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID

                    if (dsBarCodeForm == null || dsBarCodeForm.Tables.Count == 0 || dsBarCodeForm.Tables[0].Rows.Count == 0)
                    {
                        ReturnDescription = "没有找到编号[" + BarCodeNo + "]的数据";
                        return false;
                    }

                    string PreviouseWebLisFlag = "0";
                    if (!Convert.IsDBNull(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"]))
                        PreviouseWebLisFlag = dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"].ToString().Trim();
                    WebLisFlag = PreviouseWebLisFlag;

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(dsBarCodeForm.GetXml());
                    xmlWebLisOthers = dsBarCodeForm.GetXml();// doc.DocumentElement;
                }
                else
                {
                    if (xmlOthersWhereClause == null || xmlOthersWhereClause.Trim() == "")
                    {
                        ReturnDescription = "没有接收到批量条件, 拒绝提供数据状态";
                        return false;
                    }
                    XmlDocument docWhereClause = new XmlDocument();

                    //xmlOthersWhereClause = xmlOthersWhereClause.Replace("&gt;=", "");
                    //xmlOthersWhereClause = xmlOthersWhereClause.Replace("&lt;=", "");


                    //xmlOthersWhereClause = xmlOthersWhereClause;
                    docWhereClause.LoadXml(xmlOthersWhereClause);

                    DataSet dsBarCodeFormTemp = sqlDB.ExecDS("select top 1 * from barcodeForm");//View_barcodeForm_NRequestForm

                    string allWheres = "";
                    string NRequestFormWheres = "";

                    foreach (XmlNode xmlTable in docWhereClause.DocumentElement.ChildNodes)
                    {
                        string logicAND = "";
                        string logicOR = "";

                        foreach (XmlNode eachClause in xmlTable.ChildNodes)
                        {
                            string strLogic = "=";
                            if (eachClause.Attributes.GetNamedItem("Type") != null)
                                strLogic = eachClause.Attributes.GetNamedItem("Type").Value;
                            string strClause = eachClause.Name + strLogic + "'" + eachClause.InnerXml + "'";

                            if (dsBarCodeFormTemp.Tables[0].Columns.Contains(eachClause.Name))
                            {
                                if (dsBarCodeFormTemp.Tables[0].Columns[eachClause.Name].DataType.Name.ToLower().IndexOf("int") >= 0)
                                {
                                    strClause = eachClause.Name + strLogic + eachClause.InnerXml;
                                }
                                //else if()
                                //{
                                //    strClause = eachClause.Name + strLogic + eachClause.InnerXml;
                                //    //strClause = "CONVERT(varchar(10), " + eachClause.Name + ",121)>=CONVERT(varchar(10), '2009-08-01',121) 
                                //}
                            }
                            else
                            {
                                NRequestFormWheres += " and " + eachClause.Name + "='" + eachClause.InnerXml + "'";
                                continue;
                            }
                            if (eachClause.Attributes.GetNamedItem("Logic") == null
                                || eachClause.Attributes.GetNamedItem("Logic").Value.ToLower() == "and")
                                logicAND += " and " + strClause;
                            else
                                logicOR += " or " + strClause;
                        }
                        if (logicOR.Length > 4)
                            logicOR = logicOR.Substring(4);
                        if (logicAND.Length > 5)
                            logicAND = logicAND.Substring(5);
                        string WhereClause = logicAND;
                        if (logicOR.Length > 0)
                        {
                            if (logicAND.Length == 0)
                                WhereClause += "(" + logicOR + ")";
                            else
                                WhereClause += " and (" + logicOR + ")";
                        }
                        else
                        {
                            if (logicAND.Length == 0)
                            {
                                ReturnDescription = "必须传入准确的批量条件";
                                return false;
                            }
                        }
                        allWheres += " or (" + WhereClause + ")";
                    }

                    if (allWheres.Length > 4)
                        allWheres = "(" + allWheres.Substring(4) + ")";
                    if (NRequestFormWheres.Length > 4)
                    {
                        NRequestFormWheres = NRequestFormWheres.Substring(4);
                        NRequestFormWheres = " and BarcodeFormNo in (select BarcodeFormNo From NRequestItem where NRequestFormNo in (select NRequestFormNo from NRequestForm where " + NRequestFormWheres + "))";
                    }

                    DataSet dsBarCodeForm = sqlDB.ExecDS("select top 1000 BarCodeFormNo,BarCode,WebLisFlag,WebLisOpTime,WebLiser,WebLisDescript from barcodeForm where WebLisSourceOrgID='" + SourceOrgID + "' and " + allWheres + NRequestFormWheres);
                    //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID

                    if (dsBarCodeForm == null || dsBarCodeForm.Tables.Count == 0)
                    {
                        ReturnDescription = "没有找到编号[" + BarCodeNo + "]的数据";
                        return false;
                    }

                    WebLisFlag = "*";

                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(dsBarCodeForm.GetXml());
                    xmlWebLisOthers = dsBarCodeForm.GetXml();// doc.DocumentElement;
                }
            }
            catch (Exception ex)
            {
                ReturnDescription = ex.Message;
                return false;
            }
            return true;
        }


        [WebMethod(Description = "上传申请")]
        public bool WebLisUpgradeRequisitionsForDelphi(
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
            Log.Info("BarcodeForm"+nodeBarCodeForm);
            Log.Info("NRequestForm" + nodeNRequestForm);
            Log.Info("NRequestItem" + nodeNRequestItem);
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


            DataSet dsBarCodeForm = LIS.DataConn.CreateLisDB().ExecDS("select top 1 * from barcodeForm where BarCode='"
                + BarCodeNo + "' and WebLisSourceOrgID='" + SourceOrgID + "'");

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
                //|| PreviouseWebLisFlag == "7"
                //|| PreviouseWebLisFlag == "8"
                //|| PreviouseWebLisFlag == "10")
                {
                    ReturnDescription = "数据编号[" + BarCodeNo + "]不能再次上传，目前状态为[" + PreviouseWebLisFlag + "]";
                    WebLisFlag = PreviouseWebLisFlag;
                    return false;
                }

            }
            #endregion

            #region 读取数据集部分

            StringReader strTemp = new StringReader(nodeBarCodeForm);
            DataSet wsBarCode = new DataSet();
           
            wsBarCode.ReadXml(strTemp);
            if (!ECDS.Common.Security.FormatTools.CheckDataSet(wsBarCode))
            {
                ReturnDescription += String.Format("未获取到任何BarCode={0}的条码数据", BarCodeNo);
                Log.Error(String.Format("未获取到任何BarCode={0}的条码数据", BarCodeNo));
                return false;
            }
            else
            {
                Log.Info(String.Format("获取到条码数据{0}条", wsBarCode.Tables[0].Rows.Count));
            }

            StringReader strTempNRequestItem = new StringReader(nodeNRequestItem);
            DataSet wsNRequestItem = new DataSet();
            wsNRequestItem.ReadXml(strTempNRequestItem);
            if (!ECDS.Common.Security.FormatTools.CheckDataSet(wsNRequestItem))
            {
                ReturnDescription +=String.Format("未获取到任何BarCode={0}的项目数据", BarCodeNo);
                Log.Error(String.Format("未获取到任何BarCode={0}的项目数据", BarCodeNo));
                return false;
            }
            else
            {
                Log.Info(String.Format("获取到项目数据{0}条", wsNRequestItem.Tables[0].Rows.Count));
            }

            foreach (DataRow dr in wsNRequestItem.Tables[0].Rows)
            {
                if (dr.Table.Columns.Contains("ParItemNo"))
                {
                    if (dr["ParItemNo"] == null || dr["ParItemNo"].ToString() == "")
                    {
                        ReturnDescription += String.Format("BarCode={0}的项目代码未对照", BarCodeNo);
                        Log.Error(String.Format("未获取到任何BarCode={0}的项目数据", BarCodeNo));
                        return false;
                    }
                }
            }
            
                StringReader NRequestFormstrTemp = new StringReader(nodeNRequestForm);
                DataSet wsNRequestForm = new DataSet();
            try
            {
                StringReader StrStream = null;
                XmlTextReader Xmlrdr = null;
                //读取字符串中的信息
                StrStream = new StringReader(nodeNRequestForm);
                Log.Info("########################################################################" + nodeNRequestForm);
                Log.Info("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@"+StrStream.ReadToEnd());
                //获取StrStream中的数据
                Xmlrdr = new XmlTextReader(StrStream);
                Log.Info("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@" + Xmlrdr.ReadInnerXml());
                wsNRequestForm.ReadXml(NRequestFormstrTemp);
                if (!ECDS.Common.Security.FormatTools.CheckDataSet(wsNRequestForm))
                {
                ReturnDescription += String.Format("未获取到任何BarCode={0}的申请单数据", BarCodeNo);
                Log.Error(String.Format("未获取到任何BarCode={0}的申请单数据", BarCodeNo));
                return false;
                }
                else
                {
                    Log.Info(String.Format("获取到申请单数据{0}条", wsNRequestForm.Tables[0].Rows.Count));
                }
            }
            catch (Exception e)
            {
                Log.Info("@@@@@@@@@@@@" + e.ToString());
            }

            #endregion
            try
            {
                WSData ws = new WSData();
                Log.Info(String.Format("", ws.ToString()));
                ws.UpdateBarCode(wsBarCode, SourceOrgID, DestiOrgID);
                ws.UpdateNRequestForm(wsNRequestForm, SourceOrgID, DestiOrgID);
                ws.UpdateNRequestItem(wsNRequestItem, wsNRequestForm, SourceOrgID, DestiOrgID);
                int result = ws.SaveWebLisData(BarCodeNo, wsBarCode, wsNRequestItem, wsNRequestForm);
            }
            catch (Exception ex)
            {
                ReturnDescription += ex.Message;
                Log.Error(ex.Message);
                return false;
            }

            return true;
        }
        [WebMethod(Description = "身份验证上传申请")]
        public bool WebLisUpgradeRequisitionsFor301(
            string token, string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string BarCodeNo,               //条码码
            string nodeBarCodeForm,
            string nodeNRequestForm,
            string nodeNRequestItem,
            string nodeOthers)
        {
            bool result = false;
            string desc = "";
            string WebLisFlag = "0";
            if (token == "E852037E-CC7B-40DC-AC44-29909A02E87C")
            {
                result = WebLisUpgradeRequisitionsForDelphi(SourceOrgID, DestiOrgID, BarCodeNo, nodeBarCodeForm, nodeNRequestForm, nodeNRequestItem, nodeOthers, out WebLisFlag, out desc);
                return result;
            }
            else
            {
                return result;
            }
        }

        [WebMethod(Description = "更新申请状态")]
        public bool WebLisUpgradeStatus(
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string BarCodeNo,               //条码码
            string WebLisFlagSource,        //要更新前状态
            string WebLisFlagDestination,   //要更新后状态
            XmlNode WebLisOthers,           //要更新的其他信息(如更新人等)
            out string WebLisFlag,          //返回更新后的状态
            out string ReturnDescription)   //其他描述
        {
            WebLisFlag = "0";
            ReturnDescription = "";

            try
            {
                SqlServerDB sqlDB = new SqlServerDB();
                DataSet dsBarCodeForm = sqlDB.ExecDS("select top 1 * from barcodeForm where BarCode='"
                    + BarCodeNo + "' and WebLisSourceOrgID='" + SourceOrgID + "'");
                //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID

                if (dsBarCodeForm == null || dsBarCodeForm.Tables.Count == 0 || dsBarCodeForm.Tables[0].Rows.Count == 0)
                {
                    ReturnDescription = "没有找到编号[" + BarCodeNo + "]的数据";
                    return false;
                }

                if (WebLisFlagSource != null && WebLisFlagSource.Trim() != "")
                {
                    if (dsBarCodeForm.Tables[0].Select("WebLisFlag=" + WebLisFlagSource).Length == 0)
                    {
                        ReturnDescription = "编号[" + BarCodeNo + "]的数据，目前状态不为[" + WebLisFlagSource + "]";
                        return false;
                    }
                }
                if (WebLisFlagDestination == null || WebLisFlagDestination.Trim() == "")
                {
                    ReturnDescription = "没有传入正确的WebLisFlagDestination参数";
                    return false;
                }
                else
                {
                    int iUpdated = sqlDB.ExecuteNonQuery("update barcodeForm set WebLisFlag=" + WebLisFlagDestination
                        + " where BarCode='"
                        + BarCodeNo + "' and WebLisSourceOrgID='" + SourceOrgID + "'");
                    if (iUpdated > 0)
                        WebLisFlag = WebLisFlagDestination;
                    else
                    {
                    }
                }
            }
            catch (Exception ex)
            {
                ReturnDescription = ex.Message;
                return false;
            }

            return true;
        }

        /// <summary>
        ///  删除申请,彻底删除
        /// </summary>
        /// <param name="DestiOrgID"></param>
        /// <param name="BarCodeNo"></param>
        /// <param name="ReturnDescription"></param>
        /// <returns></returns>
        [WebMethod(Description = "撤销删除申请")]
        public bool WebLisRequestCancel(
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string BarCodeNo,               //条码码
            string xmlWebLisOthers,           //要更新的其他信息(如更新人等),用户保留到远程日志中
            out string ReturnDescription)   //其他描述
        {
            ReturnDescription = "";
            xmlWebLisOthers = null;
            try
            {
                SqlServerDB sqlDB = new SqlServerDB();
                DataSet dsBarCodeForm = sqlDB.ExecDS("select top 1 * from barcodeForm where BarCode='"
                    + BarCodeNo + "' and WebLisSourceOrgID='" + SourceOrgID + "'");
                //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID

                if (dsBarCodeForm == null || dsBarCodeForm.Tables.Count == 0 || dsBarCodeForm.Tables[0].Rows.Count == 0)
                {
                    ReturnDescription = "没有找到编号[" + BarCodeNo + "]的数据";
                    return false;
                }

                string PreviouseWebLisFlag = "0";
                if (!Convert.IsDBNull(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"]))
                    PreviouseWebLisFlag = dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"].ToString().Trim();
                if (PreviouseWebLisFlag == "5"
                    || Convert.ToInt32(PreviouseWebLisFlag) >= 7)// 5 7 8 9 10 11 12状态时，不能撤销
                //|| PreviouseWebLisFlag == "8"
                //|| PreviouseWebLisFlag == "10")
                {
                    ReturnDescription = "数据编号[" + BarCodeNo + "]不能撤销，目前状态为[" + PreviouseWebLisFlag + "]";
                    return false;
                }
                XmlNode WebLisOthers = null;
                XmlDocument doc = new XmlDocument();
                if (xmlWebLisOthers != null && xmlWebLisOthers.Trim() != "")
                {
                    try
                    {
                        doc.LoadXml(xmlWebLisOthers);
                    }
                    catch (Exception ex)
                    {
                        ReturnDescription = ex.Message;
                        return false;
                    }
                }

                string WebLisFlag = "";
                if (!WebLisUpgradeStatus(
                     SourceOrgID,             //送检(源)单位
                     DestiOrgID,              //外送(至)单位
                     BarCodeNo,               //条码码
                     null,                    //要更新前状态
                     "3",                    //要更新后状态
                     WebLisOthers,           //要更新的其他信息(如更新人等)
                    out WebLisFlag,          //返回更新后的状态
                    out ReturnDescription))  //其他描述
                {
                    ReturnDescription = "更新状态失败:" + ReturnDescription;
                    return false;
                }
            }
            catch (Exception ex)
            {
                ReturnDescription = ex.Message;
                return false;
            }
            return true;
        }


        [WebMethod(Description = "上传申请")]
        public bool WebLisUpgradeRequisitions(
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string BarCodeNo,               //条码码
            XmlNode nodeBarCodeForm,
            XmlNode nodeNRequestForm,
            XmlNode nodeNRequestItem,
            XmlNode nodeOthers,
            out string WebLisFlag,
            out string ReturnDescription)
        {
            WebLisFlag = "0";
            ReturnDescription = "";
            //-----
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
            SqlServerDB sqlDB = new SqlServerDB();
            DataSet dsBarCodeForm = sqlDB.ExecDS("select top 1 * from barcodeForm where BarCode='"
                + BarCodeNo + "' and WebLisSourceOrgID='" + SourceOrgID + "'");
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
                //|| PreviouseWebLisFlag == "7"
                //|| PreviouseWebLisFlag == "8"
                //|| PreviouseWebLisFlag == "10")
                {
                    ReturnDescription = "数据编号[" + BarCodeNo + "]不能再次上传，目前状态为[" + PreviouseWebLisFlag + "]";
                    WebLisFlag = PreviouseWebLisFlag;
                    return false;
                }

            }

            //这里要讨论决定, barcodeFormNo,NRequestFormNo等重新生成唯一号，用于NRequestItem关联

            if (nodeBarCodeForm == null || nodeBarCodeForm.ChildNodes.Count == 0)
            {
                ReturnDescription = "没有传入条码表信息";
                return false;
            }

            XmlNode nodeBarCodeFormNo = nodeBarCodeForm.FirstChild.SelectSingleNode("BARCODEFORMNO");

            if (nodeBarCodeFormNo == null || nodeBarCodeFormNo.InnerXml.Trim() == "")
            {
                ReturnDescription = "没有传入条码BarCodeFormNo信息";
                return false;
            }
            string strBarCodeFormNo = nodeBarCodeFormNo.InnerXml.Trim();

            nodeBarCodeForm = UpdateXmlOrgInfo(nodeBarCodeForm, SourceOrgID, "WebLisSourceOrgID");
            nodeNRequestForm = UpdateXmlOrgInfo(nodeNRequestForm, SourceOrgID, "WebLisSourceOrgID");
            nodeNRequestItem = UpdateXmlOrgInfo(nodeNRequestItem, SourceOrgID, "WebLisSourceOrgID");
            nodeBarCodeForm = UpdateXmlOrgInfo(nodeBarCodeForm, DestiOrgID, "WebLisOrgID");
            nodeNRequestForm = UpdateXmlOrgInfo(nodeNRequestForm, DestiOrgID, "WebLisOrgID");
            nodeNRequestItem = UpdateXmlOrgInfo(nodeNRequestItem, DestiOrgID, "WebLisOrgID");

            string descXmlToDB = "";

            try
            {
                //进行数据增加或更新
                if (UpdateXmlInfoToDB(SourceOrgID, DestiOrgID, BarCodeNo, nodeBarCodeForm, "BarCodeForm", bAddNew, ref descXmlToDB, dsBarCodeForm, nodeOthers)
                    && UpdateXmlInfoToDB(SourceOrgID, DestiOrgID, BarCodeNo, nodeNRequestItem, "NRequestItem", bAddNew, ref descXmlToDB, dsBarCodeForm, nodeOthers)
                    && UpdateXmlInfoToDB(SourceOrgID, DestiOrgID, BarCodeNo, nodeNRequestForm, "NRequestForm", bAddNew, ref descXmlToDB, dsBarCodeForm, nodeOthers)
                    )
                {
                    //打标志
                    //然后返回
                    return true;
                }
                else
                {
                    ReturnDescription = descXmlToDB;
                    return false;
                }
            }
            catch (Exception ex)
            {
                ReturnDescription = ex.Message;
                return false;
            }
        }
        [WebMethod(Description = "条码检测")]
        public bool WeblisBarCodeFormCheckbyBarcode(string barCode)
        {
            bool flag = false;
            SqlServerDB sqlDB = new SqlServerDB();
            //string strsql = "select count(*) from BarCodeForm where barCode='" + barCode + "'";
            int count = sqlDB.ExecCount("BarCodeForm","barCode='"+barCode+"'");
            if (count > 0)
            {
                return true;
            }
            else
                return flag;
        }

        private XmlNode UpdateXmlOrgInfo(XmlNode nodeSource, string SourceOrgID, string SourceOrgIDTableFieldName)
        {
            try
            {
                if (nodeSource == null)
                    return null;
                if (nodeSource.ChildNodes.Count == 0)
                    return nodeSource;
                foreach (XmlNode eachNode in nodeSource.ChildNodes)
                {
                    XmlNode nodeSourceOrgID = eachNode.SelectSingleNode(SourceOrgIDTableFieldName); //要判断不区分大小写
                    if (nodeSourceOrgID == null)
                    {
                        nodeSourceOrgID = eachNode.OwnerDocument.CreateElement(SourceOrgIDTableFieldName);
                        nodeSourceOrgID = eachNode.AppendChild(nodeSourceOrgID);
                    }
                    nodeSourceOrgID.InnerXml = SourceOrgID;
                }
            }
            catch
            { }
            return nodeSource;
        }

        private bool UpdateXmlInfoToDB(
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string BarCodeNo,               //条码码XmlNode nodeBarCodeForm,
            XmlNode nodeSource,
            string TableEName,
            bool bAddOrUpgrade,
            ref string descXmlToDB,
            DataSet dsBarCodeForm,
            XmlNode nodeOthers)
        {
            bool bRet = false;
            string sql = "";
            switch (TableEName)
            {
                case "BarCodeForm":
                    sql = "select * from " + TableEName + " where BarCode='"
                    + BarCodeNo + "' and WebLisSourceOrgID='" + SourceOrgID + "'";
                    bRet = UpdateXmlInfoToDB1(nodeSource, TableEName, bAddOrUpgrade, ref descXmlToDB, nodeOthers, sql, "BARCODE");
                    break;
                case "NRequestForm":
                    sql = "select * from " + TableEName;
                    sql += " where NRequestFormNo in (select NRequestFormNo from NRequestItem where BarCodeFormNo in (select BarCodeFormNo from BarCodeForm where BarCode='"
                    + BarCodeNo + "')" + " and WebLisSourceOrgID='" + SourceOrgID + "') and WebLisSourceOrgID='" + SourceOrgID + "'";
                    bRet = UpdateXmlInfoToDB1(nodeSource, TableEName, bAddOrUpgrade, ref descXmlToDB, nodeOthers, sql, "NONREQUESTFORMNO");
                    break;
                case "NRequestItem":
                    sql = "select * from " + TableEName;
                    sql += " where BarCodeFormNo in (select BarCodeFormNo from BarCodeForm where BarCode='"
                    + BarCodeNo + "')" + " and WebLisSourceOrgID='" + SourceOrgID + "'";
                    bRet = UpdateXmlInfoToDB1(nodeSource, TableEName, bAddOrUpgrade, ref descXmlToDB, nodeOthers, sql, "NREQUESTITEMNO");
                    break;
                default:
                    bRet = false;
                    break;
            }
            return bRet;
        }

        private bool UpdateXmlInfoToDB1(
            //string SourceOrgID,             //送检(源)单位
            //string DestiOrgID,              //外送(至)单位
            //string BarCodeNo,               //条码码
            XmlNode nodeSource,
            string TableEName,
            bool bAddOrUpgrade,
            ref string descXmlToDB,
            XmlNode nodeOthers,
            string sql,
            string keyColumn)
        {
            try
            {
                SqlServerDB sqldb = new SqlServerDB();
                sqldb.OpenDB();
                SqlDataAdapter da = new SqlDataAdapter(sql, sqldb.Conn);

                DataTable dt = new DataTable();
                // 获取架构
                da.Fill(dt);

                SqlCommandBuilder cb = new SqlCommandBuilder(da);

                foreach (DataRow dr in dt.Rows)
                {
                    string destiKey = dr[keyColumn].ToString();
                    if (nodeSource.SelectSingleNode("./" + keyColumn + "[text()='" + destiKey + "']") != null)
                        dt.Rows.Remove(dr);
                }

                foreach (XmlNode eachNode in nodeSource.ChildNodes)
                {
                    if (!eachNode.HasChildNodes
                        || eachNode.SelectSingleNode(keyColumn) == null
                        || eachNode.SelectSingleNode(keyColumn).InnerXml.Trim() == "")
                    {
                        descXmlToDB += "上传数据XML中没有找到" + TableEName + "[" + keyColumn + "]字段\n";
                        return false;
                    }
                    DataRow drUpdate = null;
                    string keyColumnValue = eachNode.SelectSingleNode(keyColumn).InnerXml;
                    string sqlFilter = keyColumn + "=" + keyColumnValue;
                    if (!dt.Columns.Contains(keyColumn))
                    {
                        descXmlToDB += "数据库没有" + TableEName + "[" + keyColumn + "]字段\n";
                        return false;
                    }
                    if (dt.Columns[keyColumn].DataType.Name.ToLower().IndexOf("string") >= 0
                        || dt.Columns[keyColumn].DataType.Name.ToLower().IndexOf("char") >= 0
                        )
                    {
                        sqlFilter = keyColumn + "='" + keyColumnValue + "'";
                    }
                    DataRow[] drs = dt.Select(sqlFilter);
                    if (drs.Length == 0)
                    {
                        drUpdate = dt.NewRow();
                    }
                    else
                        drUpdate = drs[0];

                    foreach (XmlNode eachFieldNode in eachNode.ChildNodes)
                    {
                        if (drUpdate.Table.Columns.Contains(eachFieldNode.Name))
                            drUpdate[eachFieldNode.Name] = eachFieldNode.InnerXml;
                    }
                    if (drs.Length == 0)
                        dt.Rows.Add(drUpdate);

                    da.Update(dt);
                }


                sqldb.CloseDB();
            }
            catch (Exception ex)
            {
                descXmlToDB += "系统出错:" + ex.Message + "\n";
                return false;
            }

            return true;
        }
    }
}
