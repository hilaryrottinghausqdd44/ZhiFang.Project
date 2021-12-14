using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

using System.Xml;

namespace ZhiFang.WebLisService.WebService
{
    /// <summary>
    /// ServiceReportDownload 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class ServiceReportDownload : System.Web.Services.WebService
    {

        //社区医院BarcodeForm [WebLisFlag]: 0未上传，1上传, 2,修改中, 3删除,4(预留),5签收,6退回, 7核收，8正在检验,9 报告重审中, 10报告已发,11报告修订,12 部分报告

        //交换数据中心BarcodeForm [WebLisFlag]: 0未处理, 1(预留), 2修改中, 3删除,4(预留),5签收, 6退回, 7核收，8正在检验, 9 报告重审中,10报告已发, 11报告修订, 12 部分报告

        [WebMethod(Description = "下载报告Delphi")]
        public bool DownloadReportForDelphi(
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string BarCodeNo,               //条码码
            out string nodeReportFormItem, //一个条码可能有多个报告单，多个项目，多个文件
            out byte[][] FileReport,        //报告单号 + 报告文件流
            out string FileType,            //PDF,FRP,RTF
            out string xmlWebLisOthers,     //返回更多信息
            out string ReturnDescription)   //其他描述
        {
            nodeReportFormItem = "";
            FileReport = null;
            FileType = "PDF";
            ReturnDescription = "";
            xmlWebLisOthers = null;

            XmlNode nodeReportFI = null;

            bool ret = DownloadReport(SourceOrgID, DestiOrgID, BarCodeNo, out nodeReportFI, out FileReport, out FileType, out xmlWebLisOthers, out ReturnDescription);
            if (nodeReportFI != null)
            {
                nodeReportFormItem = nodeReportFI.OuterXml;
            }
            return ret;
        }



        [WebMethod(Description = "下载报告BarCode")]
        public bool DownloadReport(
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string BarCodeNo,               //条码码
            out XmlNode nodeReportFormItem, //一个条码可能有多个报告单，多个项目，多个文件
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
            //return false;

            try
            {
                string sqlReport = "";
                if (BarCodeNo != null && BarCodeNo.Trim() != "")
                {
                    sqlReport = "select top 1 BarCodeFormNo,BarCode,WebLisFlag,WebLisOpTime,WebLiser,WebLisDescript from barcodeForm where BarCode='"
                        + BarCodeNo + "' and WebLisSourceOrgID='" + SourceOrgID + "'";
                }
                else
                {
                    ReturnDescription = "要求传入条码号才能下载报告";
                    return false;
                }
                SqlServerDB sqlDB = new SqlServerDB();
                DataSet dsBarCodeForm = sqlDB.ExecDS(sqlReport);
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

                DataSet dsReportForms = sqlDB.ExecDS("select * from ReportFormFull where BarCode='" + BarCodeNo + "'");

                XmlDocument xdReportForms = new XmlDocument();
                xdReportForms.LoadXml(dsReportForms.GetXml());


                XmlDocument xdReportItems = new XmlDocument();

                DataRowCollection drReportForms = dsReportForms.Tables[0].Rows;
                foreach (DataRow drReportForm in drReportForms)
                {
                    string ReportFormNo = drReportForm["ReportFormID"].ToString();
                    XmlNode nodeEachForm = xdReportForms.SelectSingleNode("//ReportFormID[text()='" + ReportFormNo + "']");
                    XmlNode nodeEachFormParent = nodeEachForm.ParentNode;
                    DataSet dsReportItems = sqlDB.ExecDS("select * from ReportItemFull where ReportFormID='" + ReportFormNo + "'");
                    xdReportItems.LoadXml(dsReportItems.GetXml());
                    XmlNode nodeTempItem = xdReportForms.CreateElement("ReportItems");
                    nodeTempItem.InnerXml = xdReportItems.DocumentElement.InnerXml;
                    nodeEachFormParent.AppendChild(nodeTempItem);
                    
                }

                nodeReportFormItem = xdReportForms.DocumentElement;
            }
            catch (Exception ex)
            {
                ReturnDescription = "出错:" + ex.Message;
                return false;
            }
            return true;
        }

        //四川大家
        [WebMethod(Description = "下载报告DelphiSerialNo")]
        public bool DownloadReportbarcode(
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string barcode,               //条码码
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
                DataSet dsReportForms = new DataSet();
                XmlDocument xdReportForms = new XmlDocument();
                //bool BarcodeState = GetBarcodeForm(SourceOrgID, SerialNo, out xmlWebLisOthers, out ReturnDescription);
                //if (!BarcodeState)1
                //{
                //    return false;
                //}
                bool ReportFormState = GetReportFormFullbarcode(barcode, out dsReportForms, out xdReportForms, out ReturnDescription);
                if (!ReportFormState)
                {
                    return false;
                }
                bool ReportItemState = GetReportItemFull(barcode, dsReportForms.Tables[0].Rows, xdReportForms, out nodeReportFormItem, out ReturnDescription);
                if (!ReportItemState)
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                ReturnDescription = "出错:" + ex.Message;
                return false;
            }
            return true;
        }
        //四川大家
        [WebMethod(Description = "获取报告单信息")]
        public bool ReportCount(
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string SerialNo,               //条码码
            out string barcode,         //下载条码
            out string Count,        //报告数量
            out string FileType,            //PDF,FRP,RTF
            out string xmlWebLisOthers,     //返回更多信息
            out string ReturnDescription)   //其他描述
        {
            barcode = "";
            FileType = "pdf";
            ReturnDescription = "";
            Count = "0";
            xmlWebLisOthers = null;
            try
            {
                DataSet dsReportForms = new DataSet();
                XmlDocument xdReportForms = new XmlDocument();
                bool BarcodeState = GetBarcodeForm(SourceOrgID, SerialNo, out xmlWebLisOthers, out ReturnDescription);
                if (!BarcodeState)
                {
                    return false;
                }
                bool ReportFormState = GetReportFormFull(SerialNo, out dsReportForms, out xdReportForms, out ReturnDescription);
                if (!ReportFormState)
                {
                    return false;
                }
                else
                {
                    if (dsReportForms != null && dsReportForms.Tables.Count > 0 && dsReportForms.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < dsReportForms.Tables[0].Rows.Count; i++)
                        {
                            barcode += dsReportForms.Tables[0].Rows[i]["BARCODE"].ToString() + ",";
                        }
                        barcode = barcode.TrimEnd(',');
                        Count = dsReportForms.Tables[0].Rows.Count.ToString();
                    }
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }


        //查询条码
        private bool GetBarcodeForm(
            string SourceOrgID,
            string SerialNo,
            out string xmlWebLisOthers,
            out string ReturnDescription)
        {
            xmlWebLisOthers = null;
            ReturnDescription = "";
            string sqlReport = "";
            if (SerialNo != null && SerialNo.Trim() != "")
            {
                sqlReport = "select top 1 BarCodeFormNo,BarCode,WebLisFlag,WebLisOpTime,WebLiser,WebLisDescript from barcodeForm where BarCode='"
                    + SerialNo + "' and WebLisSourceOrgID='" + SourceOrgID + "'";
            }
            else
            {
                ReturnDescription = "要求传入条码号才能下载报告";
                return false;
            }
            SqlServerDB sqlDB = new SqlServerDB();
            DataSet dsBarCodeForm = sqlDB.ExecDS(sqlReport);
            //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID

            if (dsBarCodeForm == null || dsBarCodeForm.Tables.Count == 0)
            {
                ReturnDescription = "数据库连接或表结构出错";
                return false;
            }
            if (dsBarCodeForm.Tables[0].Rows.Count == 0)
            {
                ReturnDescription = "没有找到条码号[" + SerialNo + "]的报告数据";
                return false;
            }

            xmlWebLisOthers = dsBarCodeForm.GetXml();

            if (Convert.IsDBNull(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"]))
            {
                ReturnDescription = "条码号[" + SerialNo + "]样本未开始检验,无报告";
                return false;
            }

            int webLisFlag = Int32.Parse(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"].ToString());
            if (webLisFlag <= 9)
            {
                ReturnDescription = "样本处于状态[" + webLisFlag.ToString() + "]\n状态小于8(<)代表已经签收\n状态等于8代表正在检验\n状态等于9代表正在重新审定中";
                return false;
            }
            return true;
        }
        //查询报告
        private bool GetReportFormFull(
            string SerialNo,
            out DataSet dsReportForms,
            out XmlDocument xdReportForms,
            out string ReturnDescription)
        {
            dsReportForms = null;
            xdReportForms = null;
            ReturnDescription = "";
            try
            {
                SqlServerDB sqlDB = new SqlServerDB();
                dsReportForms = sqlDB.ExecDS("select * from ReportFormFull where SerialNo='" + SerialNo + "'");
                xdReportForms = new XmlDocument();
                xdReportForms.LoadXml(dsReportForms.GetXml());
                if (dsReportForms != null && dsReportForms.Tables.Count > 0 && dsReportForms.Tables[0].Rows.Count > 0)
                    return true;
                else
                    ReturnDescription = "没找到报告条码号为" + SerialNo + "的报告!";
                return false;
            }
            catch (Exception ex)
            {
                ReturnDescription = ex.Message;
                return false;
            }


        }

        //查询报告barcoe
        private bool GetReportFormFullbarcode(
            string SerialNo,
            out DataSet dsReportForms,
            out XmlDocument xdReportForms,
            out string ReturnDescription)
        {
            dsReportForms = null;
            xdReportForms = null;
            ReturnDescription = "";
            try
            {
                SqlServerDB sqlDB = new SqlServerDB();
                dsReportForms = sqlDB.ExecDS("select * from ReportFormFull where barcode='" + SerialNo + "'");
                xdReportForms = new XmlDocument();
                xdReportForms.LoadXml(dsReportForms.GetXml());
                if (dsReportForms != null && dsReportForms.Tables.Count > 0 && dsReportForms.Tables[0].Rows.Count > 0)
                    return true;
                else
                    ReturnDescription = "没找到报告条码号为" + SerialNo + "的报告!";
                return false;
            }
            catch (Exception ex)
            {
                ReturnDescription = ex.Message;
                return false;
            }


        }
        //查询项目
        private bool GetReportItemFull(string SerialNo,
            DataRowCollection drReportForms,
            XmlDocument xdReportForms,
            out string nodeReportFormItem,
            out string ReturnDescription)
        {
            nodeReportFormItem = null;
            ReturnDescription = "";
            SqlServerDB sqlDB = new SqlServerDB();
            XmlDocument xdReportItems = new XmlDocument();
            try
            {
                foreach (DataRow drReportForm in drReportForms)
                {
                    string ReportFormNo = drReportForm["ReportFormID"].ToString();
                    XmlNode nodeEachForm = xdReportForms.SelectSingleNode("//ReportFormID[text()='" + ReportFormNo + "']");
                    XmlNode nodeEachFormParent = nodeEachForm.ParentNode;
                    DataSet dsReportItems = sqlDB.ExecDS("select * from ReportItemFull where ReportFormID='" + ReportFormNo + "'");
                    xdReportItems.LoadXml(dsReportItems.GetXml());
                    XmlNode nodeTempItem = xdReportForms.CreateElement("ReportItems");
                    nodeTempItem.InnerXml = xdReportItems.DocumentElement.InnerXml;
                    nodeEachFormParent.AppendChild(nodeTempItem);
                }
                nodeReportFormItem = xdReportForms.InnerXml ;
                return true;
            }
            catch (Exception ex)
            {
                ReturnDescription = ex.Message;
                return false;
            }
        }

        //string s = GetDataSetXml("Table", ds.Tables[0]);

        // xdReportForms.LoadXml(s);

        // XmlDocument xdReportItems = new XmlDocument();

        // DataRowCollection drReportForms = ds.Tables[0].Rows;
        // foreach (DataRow drReportForm in drReportForms)
        // {
        //     string ReportFormNo = convertESCToXml(drReportForm["ReportFormID"].ToString());
        //     sqlDB.ExecuteNonQuery(" update ReportFormFull set isdone='true' where ReportFormID='" + convertESCToHtml(ReportFormNo) + "'");
        //     XmlNode nodeEachForm = xdReportForms.SelectSingleNode("//ReportFormID[text()='" + ReportFormNo + "']");
        //     XmlNode nodeEachFormParent = nodeEachForm.ParentNode;
        //     DataSet dsReportItems = sqlDB.ExecDS("select * from ReportItemFull where ReportFormID='" + convertESCToHtml(ReportFormNo) + "'");
        //     string ss = GetDataSetXml("Table", dsReportItems.Tables[0]);
        //     xdReportItems.LoadXml(ss);
        //     XmlNode nodeTempItem = xdReportForms.CreateElement("ReportItems");
        //     nodeTempItem.InnerXml = xdReportItems.DocumentElement.InnerXml;
        //     nodeEachFormParent.AppendChild(nodeTempItem);
        // }

        // nodeReportFormItem = xdReportForms.DocumentElement;
        // if ("<NewDataSet></NewDataSet>" == nodeReportFormItem.OuterXml)
        // {
        //     nodeReportFormItem = null;
        //     ReturnDescription += "无报告";
        //     return false;
        // }
        // return true;


        [WebMethod(Description = "下载报告patno")]
        public bool DownloadReportPatno(
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string ClientNo,                 //客户编码
            string Patno,                    //病历号
            out XmlNode nodeReportFormItem, //一个条码可能有多个报告单，多个项目，多个文件
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
            //return false;
            if (string.IsNullOrEmpty(ClientNo))
            {
                ReturnDescription = "客户编码不能为空！";
                return false;
            }
            if (string.IsNullOrEmpty(Patno))
            {
                ReturnDescription = "病历号不能为空！";
                return false;
            }
            if (string.IsNullOrEmpty(SourceOrgID))
            {
                ReturnDescription = "送检(源)单位不能为空！";
                return false;
            }
            SqlServerDB sqlBCode = new SqlServerDB();
            DataSet dsReportForms = sqlBCode.ExecDS("select top 1 * from ReportFormFull where ClientNo='" + ClientNo + "' and Patno='" + Patno + "'");
            #region 垃圾代码
            //if (dsBCode == null && dsBCode.Tables.Count <= 0)
            //{
            //    ReturnDescription = "无此报告！";
            //    return false;
            //}
            //else
            //{
            //    BarCodeNo = dsBCode.Tables[0].Rows[0]["barCode"].ToString();
            //}
            try
            {
                //    string sqlReport = "";
                //    if (BarCodeNo != null && BarCodeNo.Trim() != "")
                //    {
                //        sqlReport = "select top 1 BarCodeFormNo,BarCode,WebLisFlag,WebLisOpTime,WebLiser,WebLisDescript from barcodeForm where BarCode='"
                //            + BarCodeNo + "' and WebLisSourceOrgID='" + SourceOrgID + "'";
                //    }
                //    else
                //    {
                //        ReturnDescription = "无此报告！";
                //        return false;
                //    }
                SqlServerDB sqlDB = new SqlServerDB();
                //    DataSet dsBarCodeForm = sqlDB.ExecDS(sqlReport);
                //    //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID

                //    if (dsBarCodeForm == null || dsBarCodeForm.Tables.Count == 0)
                //    {
                //        ReturnDescription = "数据库连接或表结构出错";
                //        return false;
                //    }
                //    if (dsBarCodeForm.Tables[0].Rows.Count == 0)
                //    {
                //        ReturnDescription = "没有找到条码号[" + BarCodeNo + "]的报告数据";
                //        return false;
                //    }

                //    xmlWebLisOthers = dsBarCodeForm.GetXml();

                //    if (Convert.IsDBNull(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"]))
                //    {
                //        ReturnDescription = "条码号[" + BarCodeNo + "]样本未开始检验,无报告";
                //        return false;
                //    }

                //    int webLisFlag = Int32.Parse(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"].ToString());
                //    if (webLisFlag <= 9)
                //    {
                //        ReturnDescription = "样本处于状态[" + webLisFlag.ToString() + "]\n状态小于8(<)代表已经签收\n状态等于8代表正在检验\n状态等于9代表正在重新审定中";
                //        return false;
                //    }

                //    DataSet dsReportForms = sqlDB.ExecDS("select * from ReportFormFull where BarCode='" + BarCodeNo + "'");
            #endregion
                XmlDocument xdReportForms = new XmlDocument();
                string s = GetDataSetXml("Table", dsReportForms.Tables[0]);
                xdReportForms.LoadXml(s);
                XmlDocument xdReportItems = new XmlDocument();
                DataRowCollection drReportForms = dsReportForms.Tables[0].Rows;
                foreach (DataRow drReportForm in drReportForms)
                {
                    string ReportFormNo = convertESCToXml(drReportForm["ReportFormID"].ToString());
                    sqlDB.ExecuteNonQuery(" update ReportFormFull set isdone='true' where ReportFormID='" + convertESCToHtml(ReportFormNo) + "'");
                    XmlNode nodeEachForm = xdReportForms.SelectSingleNode("//ReportFormID[text()='" + ReportFormNo + "']");
                    XmlNode nodeEachFormParent = nodeEachForm.ParentNode;
                    DataSet dsReportItems = sqlDB.ExecDS("select * from ReportItemFull where ReportFormID='" + convertESCToHtml(ReportFormNo) + "'");

                    string ss = GetDataSetXml("Table", dsReportItems.Tables[0]);
                    xdReportItems.LoadXml(ss);
                    XmlNode nodeTempItem = xdReportForms.CreateElement("ReportItems");
                    nodeTempItem.InnerXml = xdReportItems.DocumentElement.InnerXml;
                    nodeEachFormParent.AppendChild(nodeTempItem);
                }

                nodeReportFormItem = xdReportForms.DocumentElement;
                if ("<NewDataSet></NewDataSet>" == nodeReportFormItem.OuterXml)
                {
                    nodeReportFormItem = null;
                    ReturnDescription += "无报告";
                    return false;
                }
            }
            catch (Exception ex)
            {
                ReturnDescription = "出错:" + ex.Message;
                return false;
            }
            return true;
        }

        [WebMethod(Description = "下载报告boolAll")]
        public bool DownloadReportboolAll(
            string SourceOrgID,           //送检(源)单位
            string DestiOrgID,            //外送(至)单位
            string ClientNo,                //客户编码
            DateTime starDate,              //开始时间
            DateTime endDate,               //结束时间
            bool boolAll,                   //下载全部(true),下载为下载过的(false)。默认下载未下载过的
            out XmlNode nodeReportFormItem, //一个条码可能有多个报告单，多个项目，多个文件
            out byte[][] FileReport,        //报告单号 + 报告文件流
            out string FileType,            //PDF,FRP,RTF
            out string[] xmlWebLisOthers,     //返回更多信息
            out string ReturnDescription)   //其他描述
        {
            nodeReportFormItem = null;
            FileReport = null;
            FileType = "PDF";
            ReturnDescription = "";
            xmlWebLisOthers = null;
            if (ClientNo != null && ClientNo.Trim() != "")
            {
            }
            else
            {
                ReturnDescription = "要求传入客户编码才能下载报告";
                return false;
            }
            try
            {
                if (starDate > endDate)
                {
                    ReturnDescription = "开始时间不能大于结束时间!";
                    return false;
                }
            }
            catch (Exception)
            {
                ReturnDescription = "日期格式错误!";
                return false;
            }
            if (string.IsNullOrEmpty(SourceOrgID))
            {
                ReturnDescription = "送检(源)单位不能为空！";
                return false;
            }
            SqlServerDB sqlBCode = new SqlServerDB();
            string strWhere = "";
            if (boolAll == false)
            {
            }
            else
            {
                strWhere = " and isdone='0' ";
            }

            DataSet dsReportForms = sqlBCode.ExecDS("select * from ReportFormFull where ClientNo='" +
                ClientNo + "' and OPERDATE between '" + starDate + "' and '" + endDate + "' " + strWhere + " ");

            try
            {
                SqlServerDB sqlDB = new SqlServerDB();
                DataSet dsBarCodeForm = null;
                if (dsReportForms != null && dsReportForms.Tables.Count > 0)
                {
                    #region 垃圾代码
                    //sqlReport = "select top 1 BarCodeFormNo,BarCode,WebLisFlag,WebLisOpTime,WebLiser,WebLisDescript from barcodeForm where BarCode='"
                    //  + dsBCode.Tables[0].Rows[i]["barCode"].ToString() + "' and WebLisSourceOrgID='" + SourceOrgID + "'";
                    //dsBarCodeForm = sqlDB.ExecDS(sqlReport);
                    ////以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID

                    //if (dsBarCodeForm == null || dsBarCodeForm.Tables.Count == 0)
                    //{
                    //    ReturnDescription += "数据库连接或表结构出错";
                    //    return false;
                    //}
                    //if (dsBarCodeForm.Tables[0].Rows.Count == 0)
                    //{
                    //    ReturnDescription += "没有找到条码号[" + dsBCode.Tables[0].Rows[i]["barCode"].ToString() + "]的报告数据";
                    //    return false;
                    //}

                    //xmlWebLisOthers[i] = dsBarCodeForm.GetXml();

                    //if (Convert.IsDBNull(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"]))
                    //{
                    //    ReturnDescription += "条码号[" + BarCodeNo + "]样本未开始检验,无报告";
                    //    continue;
                    //    //return false;
                    //}
                    //int webLisFlag = Int32.Parse(dsBarCodeForm.Tables[0].Rows[0]["WebLisFlag"].ToString());
                    //if (webLisFlag <= 9)
                    //{
                    //    ReturnDescription += "条码号[" + dsBCode.Tables[0].Rows[i]["barCode"].ToString() + "]的报告样本处于状态[" + webLisFlag.ToString() + "]\n状态小于8(<)代表已经签收\n状态等于8代表正在检验\n状态等于9代表正在重新审定中！";
                    //    continue;
                    //    //return false;
                    //}
                    //else
                    //{
                    //    ReturnDescription += "条码号[" + dsBCode.Tables[0].Rows[i]["barCode"].ToString() + "]的报告样本处于状态[" + webLisFlag.ToString() + "]\n状态小于8(<)代表已经签收\n状态等于8代表正在检验\n状态等于9代表正在重新审定中！";
                    //    //状态正确 打下载过的标记

                    //}
                    //DataSet dsReportForms = sqlDB.ExecDS("select * from ReportFormFull where BarCode='" + dsBCode.Tables[0].Rows[i]["barCode"].ToString() + "'");
                    #endregion
                    XmlDocument xdReportForms = new XmlDocument();

                    string s = GetDataSetXml("Table", dsReportForms.Tables[0]);

                    xdReportForms.LoadXml(s);

                    XmlDocument xdReportItems = new XmlDocument();

                    DataRowCollection drReportForms = dsReportForms.Tables[0].Rows;
                    foreach (DataRow drReportForm in drReportForms)
                    {
                        string ReportFormNo = convertESCToXml(drReportForm["ReportFormID"].ToString());
                        sqlDB.ExecuteNonQuery(" update ReportFormFull set isdone='true' where ReportFormID='" + convertESCToHtml(ReportFormNo) + "'");
                        XmlNode nodeEachForm = xdReportForms.SelectSingleNode("//ReportFormID[text()='" + ReportFormNo + "']");
                        XmlNode nodeEachFormParent = nodeEachForm.ParentNode;
                        DataSet dsReportItems = sqlDB.ExecDS("select * from ReportItemFull where ReportFormID='" + convertESCToHtml(ReportFormNo) + "'");
                        string ss = GetDataSetXml("Table", dsReportItems.Tables[0]);
                        xdReportItems.LoadXml(ss);
                        XmlNode nodeTempItem = xdReportForms.CreateElement("ReportItems");
                        nodeTempItem.InnerXml = xdReportItems.DocumentElement.InnerXml;
                        nodeEachFormParent.AppendChild(nodeTempItem);
                    }

                    nodeReportFormItem = xdReportForms.DocumentElement;
                    if ("<NewDataSet></NewDataSet>" == nodeReportFormItem.OuterXml)
                    {
                        nodeReportFormItem = null;
                        ReturnDescription += "无报告";
                        return false;
                    }
                }
                else
                {
                    ReturnDescription += "无报告";
                    return false;
                }

            }
            catch (Exception ex)
            {
                ReturnDescription += "出错:" + ex.Message;
                return false;
            }
            return true;
        }


        /// <summary>
        /// 获取DataSet的Xml格式
        /// </summary>
        /// <param name="tableName">名称 Table1</param>
        /// <param name="table">DataTable</param>
        private string GetDataSetXml(string tableName, DataTable table)
        {
            string str = string.Empty;
            str += "<NewDataSet>\r\n  ";
            for (int i = 0; i < table.Rows.Count; i++)
            {
                str += "<" + tableName + ">\r\n  ";
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    string clName = table.Columns[j].ColumnName;
                    str += "<" + clName + ">" + convertESCToXml(table.Rows[i][clName].ToString()) + "</" + clName + ">\r\n  ";
                }
                str += "</" + tableName + ">\r\n  ";
            }
            str += "</NewDataSet>\r\n  ";
            return str;
        }
        #region //转换
        public static string convertESCToHtml(string esc)
        {
            string ret = esc;
            //转义
            ret = ret.Replace("&quot;", "\"");
            ret = ret.Replace("&apos;", "'");
            ret = ret.Replace("&gt;", ">");
            ret = ret.Replace("&lt;", "<");
            ret = ret.Replace("%20", " ");//空格
            ret = ret.Replace("&nbsp;", " ");//空格
            ret = ret.Replace("&amp;", "&");//要最后转，这样可以保证二次转义的正确性，比如输入的是&lt;会先转义为&amp;lt;
            return ret;
        }

        public static string convertESCToXml(string esc)
        {
            string ret = esc;
            //转义
            ret = ret.Replace("\"", "&quot;");
            ret = ret.Replace("'", "&apos;");
            ret = ret.Replace(">", "&gt;");
            ret = ret.Replace("<", "&lt;");
            ret = ret.Replace(" ", "%20");//空格
            ret = ret.Replace(" ", "&nbsp;");//空格
            ret = ret.Replace("&", "&amp;");//要最后转，这样可以保证二次转义的正确性，比如输入的是&lt;会先转义为&amp;lt;
            return ret;
        }
        #endregion

        [WebMethod(Description = "报告状态查询")]
        public bool QueryReportStatus(
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string BarCodeNo,               //条码码　(单个条码查询时)
            string xmlOthersWhereClause,    //其他条件 如(XX日期>=2009-10-26 and xx日期<=2009-10-27) xml字符串
            out string xmlWebLisOthers,     //返回更多信息
            out string ReturnDescription)   //其他描述
        {
            ReturnDescription = "";
            xmlWebLisOthers = null;
            try
            {
                //BarCodeFormNo,BarCode,WebLisFlag,WebLisOpTime,WebLiser,WebLisDescript
                SqlServerDB sqlDB = new SqlServerDB();
                string sqlReport = "";
                if (BarCodeNo != null && BarCodeNo.Trim() != "")
                {
                    sqlReport = "select top 1 BarCodeFormNo,BarCode,WebLisFlag,WebLisOpTime,WebLiser,WebLisDescript from barcodeForm where BarCode='"
                        + BarCodeNo + "' and WebLisSourceOrgID='" + SourceOrgID + "'";

                    //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID

                }
                else
                {
                    if (xmlOthersWhereClause == null || xmlOthersWhereClause.Trim() == "")
                    {
                        ReturnDescription = "没有接收到批量条件, 拒绝提供数据状态";
                        return false;
                    }

                    XmlDocument docWhereClause = new XmlDocument();

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

                    sqlReport = "select top 1000 BarCodeFormNo,BarCode,WebLisFlag,WebLisSourceOrgID,WebLisOrgID from barcodeForm where (weblisflag>8) and (WebLisOrgID IS NOT NULL) and WebLisSourceOrgID='" + SourceOrgID + "' and " + allWheres + NRequestFormWheres;
                }

                DataSet dsBarCodeForm = sqlDB.ExecDS(sqlReport);
                //以后要是有多家独立实验室共享交换数据库，还应该增加WebLisOrgID=DestiOrgID
                if (BarCodeNo == null || BarCodeNo.Trim() == "")
                    BarCodeNo = xmlOthersWhereClause;
                else
                    BarCodeNo = "条码编号=" + BarCodeNo;
                if (!ECDS.Common.Security.FormatTools.CheckDataSet(dsBarCodeForm))
                {
                    ReturnDescription = "没有找到条件[" + BarCodeNo + "]的数据";
                    return false;
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(dsBarCodeForm.GetXml());
                xmlWebLisOthers = dsBarCodeForm.GetXml();// doc.DocumentElement;
            }
            catch (Exception ex)
            {
                ReturnDescription = ex.Message;
                return false;
            }
            return true;
        }
    }
}
