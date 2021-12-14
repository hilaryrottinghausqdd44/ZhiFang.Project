using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using System.Data;
using ZhiFang.Common.Log;
using System.IO;
using System.Xml;
using ZhiFang.IBLL.Common.BaseDictionary;
using System.Drawing;
using ZhiFang.Common.Public;
using ZhiFang.Common.Dictionary;
using Tools;
using ZhiFang.IBLL.Common;
using ZhiFang.Model;

namespace ZhiFang.WebLisService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“ReportFormService”。
    public class ReportFormService : IReportFormService
    {

        #region IReportFormService 成员
        private readonly IBBarCodeForm ibbcf = BLLFactory<IBBarCodeForm>.GetBLL("BarCodeForm");
        private readonly IBReportFormFull ibrff = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
        private readonly IBReportItemFull ibrif = BLLFactory<IBReportItemFull>.GetBLL("ReportItemFull");
        private readonly IBTestItemControl ibtic = BLLFactory<IBTestItemControl>.GetBLL("BaseDictionary.TestItemControl");
        private readonly IBSampleTypeControl ibstc = BLLFactory<IBSampleTypeControl>.GetBLL("BaseDictionary.SampleTypeControl");
        private readonly IBGenderTypeControl ibgtc = BLLFactory<IBGenderTypeControl>.GetBLL("BaseDictionary.GenderTypeControl");
        private readonly IBCLIENTELE client = ZhiFang.BLLFactory.BLLFactory<IBCLIENTELE>.GetBLL();
        private readonly IBReportData ibrd = BLLFactory<IBReportData>.GetBLL("ReportData");
        ZhiFang.Model.CLIENTELE Model_client = new Model.CLIENTELE();
        private readonly IBLL.Report.Other.IBView ibv = BLLFactory<IBLL.Report.Other.IBView>.GetBLL("Other.BView");
        Model.BarCodeForm Model_BarCode = new Model.BarCodeForm();
        /// <summary>
        /// AntiData 抗生素信息表
        /// </summary>
        ZhiFang.Model.WhoNet_AntiData Model_AntaD = new Model.WhoNet_AntiData();
        private readonly IBWhoNet_AntiData IbAntiD = BLLFactory<IBWhoNet_AntiData>.GetBLL("WhoNet_AntiData");
        /// <summary>
        /// FormData 标本信息表
        /// </summary>
        ZhiFang.Model.WhoNet_FormData Model_FormD = new Model.WhoNet_FormData();
        private readonly IBWhoNet_FormData IbFormD = BLLFactory<IBWhoNet_FormData>.GetBLL("WhoNet_FormData");
        /// <summary>
        /// MicroData 微生物信息表
        /// </summary>
        ZhiFang.Model.WhoNet_MicroData Model_MicroD = new Model.WhoNet_MicroData();
        private readonly IBWhoNet_MicroData IbMicroD = BLLFactory<IBWhoNet_MicroData>.GetBLL("WhoNet_MicroData");
        /// <summary>
        /// 报告同步
        /// </summary>
        ZhiFang.Model.ReportFileInfo Model_File = new Model.ReportFileInfo();
        private readonly IReportFileInfo IbFile = BLLFactory<IReportFileInfo>.GetBLL("ReportFileInfo");

        public string ReportFormDownLoad(DateTime start, DateTime end, string labcode, string BarCode, out string magStr)
        {
            try
            {
                magStr = "";
                Model.ReportFormFull rff_m = new Model.ReportFormFull { CLIENTNO = labcode, BarCode = BarCode };
                IBReportFormFull rffb = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
                DataSet ds = rffb.GetMatchList(rff_m);
                ds.Tables[0].Columns.Remove("BarCode1");
                ds.Tables[0].Columns.Remove("ReportFormID1");
                ds.Tables[0].Columns.Remove("RECEIVEDATE1");
                ds.Tables[0].Columns.Remove("SECTIONNO1");
                ds.Tables[0].Columns.Remove("TESTTYPENO1");
                ds.Tables[0].Columns.Remove("SAMPLENO1");
                ds.Tables[0].Columns.Remove("SERIALNO1");
                ds.Tables[0].Columns.Remove("FORMNO1");
                ds.Tables[0].Columns.Remove("TECHNICIAN1");
                ds.Tables[0].Columns.Remove("OLDSERIALNO1");
                ds.Tables[0].Columns.Remove("ZDY51");
                ds.Tables[0].Columns.Remove("TESTTYPE");
                string str = "";
                //检测ReportFormFull是否对照
                if (!ibrff.CheckReportFormCenter(ds, labcode, out str))
                {
                    magStr += str;
                    return null;
                }
                //转码过程
                ds = MatchClientNo(ds, labcode);
                magStr = str;
                return "<?xml version=\"1.0\" standalone=\"yes\"?>" + ds.GetXml();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                magStr = e.ToString();
                return "-1";
            }
        }
        public string GetReportFormColumn()
        {
            try
            {
                IBReportFormFull rffb = BLLFactory<IBReportFormFull>.GetBLL("ReportFormFull");
                DataSet ds = rffb.GetColumn("view");
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int i = ds.Tables[0].Columns.Count - 1; i > 0; i--)
                    {
                        ds.Tables[0].Columns.RemoveAt(i);
                    }
                }
                return ds.GetXml();
            }
            catch (Exception e)
            {
                Log.Error(e.ToString());
                return "-1";
            }
        }
        public int MatchItemNo(string clientNo, string ItemCode)
        {
            Model.TestItemControl testModel = new Model.TestItemControl();
            DAL.MsSql.Weblis.B_TestItemControl testItem = new DAL.MsSql.Weblis.B_TestItemControl();
            DataSet ds = testItem.GetList("ControlLabNo='" + clientNo + "' and ItemNo='" + ItemCode + "'");
            if (ds.Tables[0].Rows.Count != 0)
            {
                testModel.ItemNo = ds.Tables[0].Rows[0]["ControlItemNo"].ToString();
                return Convert.ToInt32(testModel.ItemNo);
            }
            else
            {
                return 0;
            }
        }
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
            nodeReportFormItem = "";
            FileReport = null;
            FileType = "PDF";
            ReturnDescription = "";
            xmlWebLisOthers = null;
            XmlNode nodeReportFI = null;
            bool ret = DownloadReportForm(SourceOrgID, DestiOrgID, BarCodeNo, out nodeReportFI, out FileReport, out FileType, out xmlWebLisOthers, out ReturnDescription);
            if (nodeReportFI != null)
            {
                nodeReportFormItem = nodeReportFI.OuterXml;
            }
            return ret;
        }
        public bool DownloadReportForm(
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
                //检测ReportFormFull是否对照
                if (!ibrff.CheckReportFormCenter(dsReportForms, DestiOrgID, out str))
                {
                    ReturnDescription += str;
                    return false;
                }
                //转码过程
                dsReportForms = MatchClientNo(dsReportForms, DestiOrgID);
                XmlDocument xdReportForms = new XmlDocument();
                xdReportForms.LoadXml(dsReportForms.GetXml());
                XmlDocument xdReportItems = new XmlDocument();
                DataRowCollection drReportForms = dsReportForms.Tables[0].Rows;
                foreach (DataRow drReportForm in drReportForms)
                {
                    Model.ReportItemFull ReportItemFull = new Model.ReportItemFull();
                    ReportItemFull.ReportFormID = drReportForm["ReportFormID"].ToString();
                    XmlNode nodeEachForm = xdReportForms.SelectSingleNode("//ReportFormID[text()='" + ReportItemFull.ReportFormID + "']");
                    XmlNode nodeEachFormParent = nodeEachForm.ParentNode;
                    DataSet dsReportItems = ibrif.GetList(ReportItemFull);
                    //检测ReportItemFull是否对照
                    if (!ibrif.CheckReportItemCenter(dsReportItems, DestiOrgID, out str))
                    {
                        ReturnDescription += str;
                        return false;
                    }
                    dsReportItems = MatchClientNo(dsReportItems, DestiOrgID);
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

        //自动
        public bool SelectDownloadReport(
            DateTime start,
            DateTime end,
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
            //return false;

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
                //检测ReportFormFull是否对照
                if (!ibrff.CheckReportFormCenter(dsReportForms, DestiOrgID, out str))
                {
                    ReturnDescription += str;
                    return false;
                }
                //转码过程
                dsReportForms = MatchClientNo(dsReportForms, DestiOrgID);
                XmlDocument xdReportForms = new XmlDocument();
                xdReportForms.LoadXml(dsReportForms.GetXml());
                XmlDocument xdReportItems = new XmlDocument();
                DataRowCollection drReportForms = dsReportForms.Tables[0].Rows;
                foreach (DataRow drReportForm in drReportForms)
                {
                    Model.ReportItemFull ReportItemFull = new Model.ReportItemFull();
                    ReportItemFull.ReportFormID = drReportForm["ReportFormID"].ToString();
                    XmlNode nodeEachForm = xdReportForms.SelectSingleNode("//ReportFormID[text()='" + ReportItemFull.ReportFormID + "']");
                    XmlNode nodeEachFormParent = nodeEachForm.ParentNode;
                    DataSet dsReportItems = ibrif.GetList(ReportItemFull);
                    //检测ReportItemFull是否对照
                    if (!ibrif.CheckReportItemCenter(dsReportItems, DestiOrgID, out str))
                    {
                        ReturnDescription += str;
                        return false;
                    }
                    dsReportItems = MatchClientNo(dsReportItems, DestiOrgID);
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
        #endregion
        /// <summary>
        /// 根据实验室的编码得到中心的码
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="SourceOrgID"></param>
        /// <returns></returns>
        public DataSet MatchCenterNo(DataSet ds, string SourceOrgID)
        {
            List<string> stringList = new List<string>();
            List<string> l = new List<string>();
            List<string> ListStr = new List<string>();
            Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
            Model.TestItemControl TestItemControl = new Model.TestItemControl();
            Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
            if (ds.Tables[0].Columns.Contains("ParItemNo"))
            {
                for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                {
                    ListStr.Add(ds.Tables[0].Rows[count]["ParItemNo"].ToString());
                }
                DataSet dsItem = ibtic.GetCenterNo(SourceOrgID, ListStr);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    foreach (DataRow dritem in dsItem.Tables[0].Rows)
                    {
                        if (dr["ParItemNo"].ToString() == dritem["ControlItemNo"].ToString())
                        {
                            dr["ParItemNo"] = dritem["ItemNo"].ToString();
                        }
                    }
                }
            }
            if (ds.Tables[0].Columns.Contains("SampleTypeNo"))
            {
                for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                {
                    l.Add(ds.Tables[0].Rows[count]["SampleTypeNo"].ToString());
                }
                DataSet dsSample = ibstc.GetCenterNo(SourceOrgID, l);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    foreach (DataRow drSample in dsSample.Tables[0].Rows)
                    {
                        if (dr["SampleTypeNo"].ToString() == drSample["ControlSampleTypeNo"].ToString())
                        {
                            dr["SampleTypeNo"] = drSample["SampleTypeNo"].ToString();
                        }
                    }
                }
            }
            if (ds.Tables[0].Columns.Contains("GenderNo"))
            {
                for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
                {
                    stringList.Add(ds.Tables[0].Rows[count]["GenderNo"].ToString());

                }
                DataSet dsGender = ibgtc.GetCenterNo(SourceOrgID, stringList);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    foreach (DataRow drGender in dsGender.Tables[0].Rows)
                    {
                        if (dr["GenderNo"].ToString() == drGender["ControlGenderNo"].ToString())
                        {
                            dr["GenderNo"] = drGender["GenderNo"].ToString();
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
        //public DataSet MatchClientNo(DataSet ds, string SourceOrgID)
        //{
        //    List<string> stringList = new List<string>();
        //    List<string> l = new List<string>();
        //    List<string> ListStr = new List<string>();
        //    Model.SampleTypeControl SamplleTypeControl = new Model.SampleTypeControl();
        //    Model.TestItemControl TestItemControl = new Model.TestItemControl();
        //    Model.GenderTypeControl GenderType = new Model.GenderTypeControl();
        //    if (ds.Tables[0].Columns.Contains("ParItemNo"))
        //    {
        //        Log.Info("包涵ParItemNo");
        //        for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
        //        {
        //            Log.Info("包涵ParItemNo:" + ds.Tables[0].Rows[count]["ParItemNo"].ToString());
        //            ListStr.Add(ds.Tables[0].Rows[count]["ParItemNo"].ToString());
        //        }
        //        DataSet dsItem = ibtic.GetLabCodeNo(SourceOrgID, ListStr);
        //        Log.Info("dsItem项目个数:" + dsItem.Tables[0].Rows.Count);
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            foreach (DataRow dritem in dsItem.Tables[0].Rows)
        //            {
        //                Log.Info("ParItemNo:" + dr["ParItemNo"].ToString() + ";ItemNo:" + dritem["ItemNo"].ToString());
        //                if (dr["ParItemNo"].ToString() == dritem["ItemNo"].ToString())
        //                {
        //                    Log.Info("ParItemNo:" + dr["ParItemNo"].ToString() + ";ItemNo:" + dritem["ItemNo"].ToString() + ";ControlItemNo:" + dritem["ControlItemNo"].ToString());
        //                    dr["ParItemNo"] = dritem["ControlItemNo"].ToString();
        //                }
        //            }
        //        }
        //    }
        //    if (ds.Tables[0].Columns.Contains("SampleTypeNo"))
        //    {
        //        for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
        //        {
        //            l.Add(ds.Tables[0].Rows[count]["SampleTypeNo"].ToString());

        //        }
        //        DataSet dsSample = ibstc.GetLabCodeNo(SourceOrgID, l);
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            foreach (DataRow drSample in dsSample.Tables[0].Rows)
        //            {
        //                if (dr["SampleTypeNo"].ToString() == drSample["SampleTypeNo"].ToString())
        //                {
        //                    dr["SampleTypeNo"] = drSample["ControlSampleTypeNo"].ToString();
        //                }
        //            }
        //        }
        //    }
        //    if (ds.Tables[0].Columns.Contains("GenderNo"))
        //    {
        //        for (int count = 0; count < ds.Tables[0].Rows.Count; count++)
        //        {
        //            stringList.Add(ds.Tables[0].Rows[count]["GenderNo"].ToString());

        //        }
        //        DataSet dsGender = ibgtc.GetLabCodeNo(SourceOrgID, stringList);
        //        foreach (DataRow dr in ds.Tables[0].Rows)
        //        {
        //            foreach (DataRow drGender in dsGender.Tables[0].Rows)
        //            {
        //                if (dr["GenderNo"].ToString() == drGender["GenderNo"].ToString())
        //                {
        //                    dr["GenderNo"] = drGender["ControlGenderNo"].ToString();
        //                }
        //            }
        //        }
        //    }
        //    return ds;
        //}

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
                    case "SECTIONNO":
                        B_Lab_Columns = "SECTIONNO";
                        B_Lab_controlTableName = "B_PGroupControl";
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
        /// 根据中心端的编码得到实验室的编码 取B_ResultTestItemControl表
        /// </summary>
        /// <param name="dsreportitem"></param>
        /// <param name="SourceOrgID"></param>
        /// <returns></returns>
        public DataSet MatchClientNo_yinzhou(DataSet dsreportitem, string SourceOrgID)
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
                        B_Lab_controlTableName = "B_ResultTestItemControl";
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
                            int count = dsreportitem.Tables[0].Rows.Count;
                            ZhiFang.Common.Log.Log.Info("dsreportitem.Tables[0].Rows.Count:" + count);
                            ZhiFang.Common.Log.Log.Info("B_ResultTestItemControl.Tables[0].Rows.Count:" + labNo.Tables[0].Rows.Count);
                            for (int i = 0; i < count; i++)
                            {
                                DataRow[] drlist = dt.Select(B_Lab_Columns + "='" + dsreportitem.Tables[0].Rows[i][str].ToString() + "'");
                                ZhiFang.Common.Log.Log.Info("dtwhere:" + B_Lab_Columns + "='" + dsreportitem.Tables[0].Rows[i][str].ToString() + "'");
                                if (drlist != null && drlist.Count() > 0)
                                {
                                    ZhiFang.Common.Log.Log.Info("drlist.Length:" + drlist.Length);
                                    for (int j = 0; j < drlist.Length; j++)
                                    {
                                        if (j > 0)
                                        {
                                            //鄞州区 中心-客户端 存在一对多的情况，取出所有的项目返回给客户端

                                            dsreportitem.Tables[0].Rows.Add(dsreportitem.Tables[0].Rows[i].ItemArray);//复制一行数据 追加到dataset中
                                            int index = dsreportitem.Tables[0].Rows.Count;
                                            ZhiFang.Common.Log.Log.Info("追加新行后dsreportitem.Tables[0].Rows.Count:" + index + " 客户端编码:" + drlist[j]["Control" + B_Lab_Columns].ToString());
                                            dsreportitem.Tables[0].Rows[index - 1][str] = drlist[j]["Control" + B_Lab_Columns].ToString();
                                        }
                                        else
                                        {
                                            dsreportitem.Tables[0].Rows[i][str] = drlist[j]["Control" + B_Lab_Columns].ToString();
                                        }

                                    }
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
        /// 传递Byte类型文件
        /// </summary>
        /// <param name="filedata"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public bool UploadReport_WhoNet(byte[] filedata, out string errorMsg)
        {
            string url = "";
            //随机文件ID
            string Gid = Math.Abs(Guid.NewGuid().GetHashCode()) + "-";
            string time = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond.ToString();
            string FileName = Gid + time;
            errorMsg = ""; //string sss = "";
            try
            {
                //写入dbf文件 路径+ 名称
                url = String.Format("{0}{1}\\{2}.dbf", System.AppDomain.CurrentDomain.BaseDirectory, ZhiFang.Common.Public.ConfigHelper.GetConfigString("UploadFilesUrl"), FileName);

                FileStream Wrfs = new FileStream(url, FileMode.Create, FileAccess.Write);
                Wrfs.Write(filedata, 0, filedata.Length);
                Wrfs.Close();

                //读取dbf文件返回值给DataSet
                ZhiFang.BLL.Common.DBFFile BC = new BLL.Common.DBFFile();
                BC.Open(url);
                DataSet ds = BC.GetDataSet();
                BC.Close();
                return DataSetGotSql(ds);
            }
            catch (Exception ex)
            {
                errorMsg = ex.ToString();
                Log.Error("错误信息:" + ex);
                return false;
            }
        }
        /// <summary>
        /// WhoNet 公共
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private bool DataSetGotSql(DataSet ds)
        {
            try
            {
                //记录列数
                int dsC = ds.Tables[0].Columns.Count;
                //列数组
                var arrayColumns = new string[dsC];
                //共有多少条数据
                int dsR = ds.Tables[0].Rows.Count;
                //行数组
                var arrayRows = new string[dsR];

                //不执行insert update 默认执行
                bool perform = true;
                //选择执行 默认 ADD
                bool choose = true;

                for (int i = 0; i < dsR; i++)
                {
                    Model_MicroD.MicroID = Math.Abs(Guid.NewGuid().GetHashCode()) + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond; //每条不一样
                    Model_MicroD.FomID = Math.Abs(Guid.NewGuid().GetHashCode()) + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond; //每条不一样
                    Model_FormD.FormID = Model_MicroD.FomID; //与MicroD.F id相同  外键

                    for (int j = 0; j < dsC; j++)
                    {
                        arrayColumns[j] = ds.Tables[0].Columns[j].ToString();
                        arrayRows[i] = ds.Tables[0].Rows[i][arrayColumns[j]].ToString();
                        //sss += arrayRows[i].ToLower() + "、";

                        // FormData 标本信息表
                        switch (arrayColumns[j].ToLower())
                        {
                            case "country_a":
                                Model_FormD.country_a = arrayRows[i];
                                break;
                            case "laboratory":
                                Model_FormD.laboratory = arrayRows[i];
                                Model_FormD.LabID = Convert.ToInt64(Model_FormD.laboratory);
                                Model_AntaD.LabID = Convert.ToInt64(Model_FormD.laboratory);
                                Model_MicroD.LabID = Convert.ToInt64(Model_FormD.laboratory);
                                break;
                            case "patient_id":
                                Model_FormD.patient_id = arrayRows[i];
                                break;
                            case "last_name":
                                Model_FormD.last_name = arrayRows[i];
                                if (Model_FormD.last_name != "" && Model_FormD.last_name != null)
                                { }
                                else
                                {
                                    j = dsC; continue;
                                }
                                break;
                            case "first_name":
                                Model_FormD.first_name = arrayRows[i];
                                break;
                            case "sex":
                                Model_FormD.sex = arrayRows[i];
                                break;
                            case "age":
                                Model_FormD.age = arrayRows[i];
                                break;
                            case "pat_type":
                                Model_FormD.pat_type = arrayRows[i];
                                break;
                            case "ward":
                                Model_FormD.ward = arrayRows[i];
                                break;
                            case "department":
                                Model_FormD.department = arrayRows[i];
                                break;
                            case "ward_type":
                                Model_FormD.ward_type = arrayRows[i];
                                break;
                            case "date_brith":
                                if (Convert.ToDateTime(arrayRows[i]) > DateTime.MinValue && Convert.ToDateTime(arrayRows[i]) < DateTime.MaxValue)
                                { Model_FormD.date_brith = Convert.ToDateTime(arrayRows[i]); }
                                break;
                            case "institut":
                                Model_FormD.institut = arrayRows[i];
                                break;
                            case "spec_num":
                                Model_FormD.SPEC_NUM = arrayRows[i];
                                break;
                            case "spec_date":
                                if (Convert.ToDateTime(arrayRows[i]) > DateTime.MinValue && Convert.ToDateTime(arrayRows[i]) < DateTime.MaxValue)
                                {
                                    Model_FormD.SPEC_DATE = Convert.ToDateTime(arrayRows[i]);
                                }
                                else
                                {
                                    Model_MicroD.date_data = DateTime.Now;
                                }
                                break;
                            case "spec_type":
                                Model_FormD.SPEC_TYPE = arrayRows[i];
                                break;
                            case "spec_code":
                                Model_FormD.SPEC_CODE = arrayRows[i];
                                break;
                            case "spec_reas":
                                Model_FormD.SPEC_REAS = arrayRows[i];
                                break;
                            //Model_FormD.DATE_ADMIS.IndexOf(arrayColumns[i])
                            //Model_FormD.DATE_DISCH.IndexOf(arrayColumns[i])
                            //Model_FormD.DATE_OPER.IndexOf(arrayColumns[i])
                            //Model_FormD.DATE_WARD.IndexOf(arrayColumns[i])
                            case "diagnosis":
                                Model_FormD.DIAGNOSIS = arrayRows[i];
                                break;
                            //Model_FormD.DATE_INFEC.IndexOf(arrayColumns[i])
                            case "siteinfect":
                                Model_FormD.SITEINFECT = arrayRows[i];
                                break;
                            case "operation":
                                Model_FormD.OPERATION = arrayRows[i];
                                break;
                            case "order_md":
                                Model_FormD.ORDER_MD = arrayRows[i];
                                break;
                            case "clnoutcome":
                                Model_FormD.CLNOUTCOME = arrayRows[i];
                                break;
                            case "physician":
                                Model_FormD.PHYSICIAN = arrayRows[i];
                                break;
                            case "prior_abx":
                                Model_FormD.PRIOR_ABX = arrayRows[i];
                                break;
                            case "resp_to_tx":
                                Model_FormD.RESP_TO_TX = arrayRows[i];
                                break;
                            case "surgeon":
                                Model_FormD.SURGEON = arrayRows[i];
                                break;
                            case "storageloc":
                                Model_FormD.STORAGELOC = arrayRows[i];
                                break;
                            case "storagenum":
                                Model_FormD.STORAGENUM = arrayRows[i];
                                break;
                            case "resid_type":
                                Model_FormD.RESID_TYPE = arrayRows[i];
                                break;
                            case "occupation":
                                Model_FormD.OCCUPATION = arrayRows[i];
                                break;
                            case "ethnic":
                                Model_FormD.ETHNIC = arrayRows[i];
                                break;
                            //Model_FormD.DataAddTime.IndexOf(arrayColumns[i])
                            //Model_FormD.DataUpdateTime.IndexOf(arrayColumns[i])

                            // MicroData 微生物信息表
                            case "date_data":
                                if (Convert.ToDateTime(arrayRows[i]) > DateTime.MinValue && Convert.ToDateTime(arrayRows[i]) < DateTime.MaxValue)
                                {
                                    Model_MicroD.date_data = Convert.ToDateTime(arrayRows[i]);
                                }
                                else
                                {
                                    Model_MicroD.date_data = DateTime.Now;
                                }
                                break;
                            case "organism":
                                Model_MicroD.organism = arrayRows[i];
                                if (Model_MicroD.organism != "" && Model_MicroD.organism != null)
                                { }
                                else
                                {
                                    j = dsC; continue;
                                }
                                break;
                            case "org_type":
                                Model_MicroD.org_type = arrayRows[i];
                                break;
                            case "origin":
                                Model_MicroD.origin = arrayRows[i];
                                break;
                            case "esbl":
                                Model_MicroD.ESBL = arrayRows[i];
                                break;
                            case "comment":
                                Model_MicroD.comment = arrayRows[i];
                                break;
                            case "dataaddTime":
                                break;
                            case "dataupdatetime":
                                break;
                            case "datatimestamp":
                                break;
                            case "beta_lact":
                                Model_MicroD.beta_lact = arrayRows[i];
                                break;

                            // AntiData 抗生素信息表
                            default:
                                bool default_perform = true;
                                bool AntaDInsert = true; //是否执行insert update 
                                string strSubingNm =
                                arrayColumns[j].ToLower().Substring(arrayColumns[j].ToLower().Trim().Length - 3, 3);
                                if (strSubingNm == "_nm" || strSubingNm == "_ne" || strSubingNm == "_nd")
                                {
                                    string[] arrAyNm = arrayColumns[j].ToUpper().Trim().Split('_');
                                    // case "antiname":
                                    Model_AntaD.AntiName = arrAyNm[0];
                                    // case "testmethod":
                                    Model_AntaD.TestMethod = arrAyNm[1];

                                    if (arrayRows[i].Trim() != "" && arrayRows[i].Trim() != null)
                                    {
                                        string strSubVal =
                                            arrayRows[i].Trim().Substring(arrayRows[i].ToUpper().Trim().Length - 1, 1);

                                        if (strSubVal == "R" || strSubVal == "I" || strSubVal == "S" || strSubVal == "?")
                                        {
                                            //  case "suscept":
                                            Model_AntaD.Suscept = arrayRows[i].Trim();
                                        }
                                        else
                                        {
                                            //  case "refrange":
                                            Model_AntaD.RefRange = arrayRows[i].Trim();
                                        }
                                        if (arrayRows[i].Trim() != null)
                                        {
                                        }
                                        else
                                        {
                                            AntaDInsert = false; //不执行录入
                                        }
                                    }
                                    else
                                    {
                                        Model_AntaD.Suscept = "";
                                        Model_AntaD.RefRange = "";
                                        AntaDInsert = false; //不执行录入
                                    }
                                }
                                else
                                {
                                    AntaDInsert = false;
                                }
                                //查询是否存在数据 存在执行update 否则insert
                                DataSet boolInsert = IbFormD.JoinCount(Model_FormD.laboratory, Model_FormD.SPEC_DATE, Model_FormD.SPEC_TYPE, Model_MicroD.organism);
                                if (AntaDInsert)
                                {
                                    Model_AntaD.LabID = Convert.ToInt64(Model_FormD.laboratory);
                                    Model_AntaD.MicroID = Model_MicroD.MicroID;//与MicroD.M id相同 外键
                                    Model_AntaD.AntiID = Math.Abs(Guid.NewGuid().GetHashCode()) + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond; ;  //每条不一样
                                    if (default_perform)
                                    {
                                        if (boolInsert != null && boolInsert.Tables[0].Rows.Count > 0)
                                        {
                                            IbAntiD.Update(Model_AntaD);
                                        }
                                        else
                                        {
                                            IbAntiD.Add(Model_AntaD);
                                        }
                                    }
                                    Model_AntaD = new Model.WhoNet_AntiData();
                                }
                                break;
                            //Model_AntaD.DataAddTime = 1;
                            //Model_AntaD.DataUpdateTime = 1;
                            //Model_AntaD.DataTimeStamp = 1;
                        }
                    }
                    if (perform)
                    {
                        //判断
                        DataSet boolInsert = IbFormD.JoinCount(Model_FormD.laboratory, Model_FormD.SPEC_DATE, Model_FormD.SPEC_TYPE, Model_MicroD.organism);
                        if (boolInsert != null && boolInsert.Tables[0].Rows.Count > 0)
                        {
                            choose = false;
                        }
                        else
                        {
                            choose = true;
                        }
                        if (choose)
                        {
                            IbFormD.Add(Model_FormD);
                            Model_FormD = new Model.WhoNet_FormData();
                            IbMicroD.Add(Model_MicroD);
                            Model_MicroD = new Model.WhoNet_MicroData();
                        }
                        else
                        {
                            IbFormD.Update(Model_FormD);
                            Model_FormD = new Model.WhoNet_FormData();
                            IbMicroD.Update(Model_MicroD);
                            Model_MicroD = new Model.WhoNet_MicroData();
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 传递Xml字符串
        /// </summary>
        /// <param name="filedata"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public bool UploadReportData_WhoNet(string filedata, out string errorMsg)
        {
            const string url = "";
            //随机文件ID
            string Gid = Math.Abs(Guid.NewGuid().GetHashCode()) + "-";
            string time = DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond.ToString();
            string FileName = Gid + time;
            errorMsg = ""; const string sss = "";
            DataSet ds = new DataSet(); //接收字符串转换
            try
            {
                if (!string.IsNullOrEmpty(filedata))
                {
                    //读取字符串中的信息
                    //获取StrStream中的数据
                    //ds获取Xmlrdr中的数据    
                    StringReader StrStream = new StringReader(filedata);
                    XmlTextReader Xmlrdr = new XmlTextReader(StrStream);
                    ds.ReadXml(Xmlrdr);
                    Xmlrdr.Close();
                    StrStream.Close();
                    StrStream.Dispose();
                }
                return DataSetGotSql(ds);
            }
            catch (Exception ex)
            {
                errorMsg = String.Format("{0}自定义：{1}", ex, sss);
                Log.Error("错误信息:" + ex);
                return false;
            }
        }

        /// <summary>
        /// 0失败1成功2验证不成
        /// </summary>
        /// <param name="filedata">文件</param>
        /// <param name="peport">文件名称</param>
        /// <param name="strJiaM">加密客户编码</param>
        /// <param name="errorMsg">返回值</param>
        /// <returns></returns>
        public int UploadReportInfo_Synchronous(byte[] filedata, string filesName, string strJiaM, out string errorMsg)
        {
            if (string.IsNullOrEmpty(strJiaM))
            {
                errorMsg = "没有获取到客户编码！";
                return 3;
            }
            long strJieMi = Convert.ToInt64(BLL.Common.MD5.Encrypt.DeCode(strJiaM));
            Model_client.ClIENTNO = strJieMi.ToString();
            DataSet JiaMids = client.GetList(Model_client);

            if (JiaMids == null && JiaMids.Tables[0].Rows.Count <= 0)
            {
                errorMsg = "非本院的用户禁止调用！";
                return 3;
            }
            int state = 0;
            //System.AppDomain.CurrentDomain.BaseDirectory + 程序相对
            string create_successful = ZhiFang.Common.Public.ConfigHelper.GetConfigString("create_successful");
            if (!Common.Public.FilesHelper.CheckAndCreatDir(create_successful))
                errorMsg = "“成功”文件夹未创建成功";
            // string filesName = peport[4];
            MemoryStream ms = new MemoryStream(filedata, 0, filedata.Length);
            Image returnImage = Image.FromStream(ms);

            int wim = returnImage.Width;
            int him = returnImage.Height;
            double imgSize = filedata.Length / 1024;//大小
            string fileType = filesName.Substring(filesName.LastIndexOf(".") + 1);//格式
            errorMsg = "";
            if (fileType == "jpg" || fileType == "jpeg")
            {
                if (him <= 800 && wim <= 1024)
                {
                    try //存储
                    {

                        errorMsg = filesName + "-成功!";
                        state = 1;
                        var arrayRow = new string[9];
                        switch (fileType)
                        {
                            case "jpg": arrayRow = filesName.ToLower().Replace(".jpg", "").Split(';'); break;
                            case "jpeg": arrayRow = filesName.ToLower().Replace(".jpeg", "").Split(';'); break;
                        }

                        //Model_File
                        //IbFile
                        try
                        {
                            //“被检验者万达的PAT_ID;身份证号;门诊（就诊卡号）/住院（住院号）/体检（体检编号）;病人姓名;
                            //年龄;性别;手机号;报告时间;就诊医疗机构编码”。后缀名为”.jpg”。  ---数据库
                            //盘符：\医疗机构编码\报告年\报告月\报告日\报告时\报告唯一ID.jpg。
                            Model_File.UniqueID = Math.Abs(Guid.NewGuid().GetHashCode()) + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond; //每条不一样
                            Model_File.G_ID = Math.Abs(Guid.NewGuid().GetHashCode()) + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond; //报告相同GUID相同
                            Model_File.PAT_ID = arrayRow[0];
                            Model_File.Card_ID = arrayRow[1];
                            Model_File.ClinicType = arrayRow[2];
                            Model_File.Name = arrayRow[3];
                            Model_File.Age = arrayRow[4];
                            Model_File.Sex = arrayRow[5];
                            Model_File.MobilePhone = arrayRow[6];
                            string[] a = (arrayRow[7]).Split(' ');
                            string time = a[1].Replace("-", ":");
                            Model_File.Report_Time = a[0] + " " + time;
                            Model_File.Medical_Institution_ID = arrayRow[8];
                            try
                            {
                                Model_File.Medical_Institution_Code = arrayRow[9].Split('_')[0];
                                Model_File.ProjectCode = arrayRow[9].Split('_')[1];
                                Model_File.PageNo = arrayRow[9].Split('_')[2];
                            }
                            catch (Exception)
                            {
                                Model_File.Medical_Institution_Code = null;
                                Model_File.ProjectCode = null;
                                Model_File.PageNo = null;
                                Model_File.Medical_Institution_Code = arrayRow[9].Split('_')[0];
                                Model_File.PageNo = arrayRow[9].Split('_')[1];
                            }


                            //Model_client.ClIENTNO = Medical_Institution_Code_md5;
                            //client.GetList(Model_client);

                            Model_File.OperaTion = "1";
                            Model_File.File_Name = filesName;
                            Model_File.AddDataTime = DateTime.Now;
                            if (!string.IsNullOrEmpty(Model_File.Name))
                            {
                                if (!string.IsNullOrEmpty(Model_File.Report_Time))
                                {
                                    string saveUrl = string.Format("{0}/{1}/{2}/{3}/{4}", Model_File.Medical_Institution_Code, Convert.ToDateTime(a[0]).Year, Convert.ToDateTime(a[0]).Month, Convert.ToDateTime(a[0]).Day, a[1]);
                                    //IbFile.Add(Model_File);
                                    DataSet bds = IbFile.GetList(String.Format("File_Name like '{0}%' order by ChangeStatus asc", filesName.Substring(0, filesName.LastIndexOf("."))));
                                    if (bds != null && bds.Tables[0].Rows.Count > 0)
                                    {
                                        //组合分页
                                        string NProjectCode = !string.IsNullOrEmpty(Model_File.ProjectCode) ? "_" + Model_File.ProjectCode : "";
                                        string NPageNo = !string.IsNullOrEmpty(Model_File.PageNo) ? "_" + Model_File.PageNo : "";
                                        //查处最新报告Model_File.ChangeStatus 0代表最新  条数
                                        //改变原主键值Model_File.File_Url 已被修改 加编号  Model_File.ChangeStatus 累加编号
                                        //从新添加源数据 
                                        //新旧数据都用同一ID
                                        Model_File.G_ID = (long?)bds.Tables[0].Rows[0]["G_ID"];//相同的
                                        Model_File.UniqueID = (long?)bds.Tables[0].Rows[0]["UniqueID"];//条件
                                        Model_File.ChangeStatus = bds.Tables[0].Rows.Count;//修改次数
                                        Model_File.File_Name = String.Format("{0}已被修改{1}.{2}", filesName.Substring(0, filesName.LastIndexOf(".")), bds.Tables[0].Rows.Count, fileType);

                                        //更改原版本文件名称
                                        string OrignFile = String.Format("{0}/{2}\\{1}.jpg", @create_successful, Model_File.Medical_Institution_ID + NProjectCode + NPageNo, saveUrl);
                                        string OrignFile2 = String.Format("{1}\\{0}.jpg", Model_File.Medical_Institution_ID + NProjectCode + NPageNo, saveUrl);
                                        string NewFile = String.Format("{0}/{4}\\{1}已被修改{2}.{3}", @create_successful, Model_File.Medical_Institution_ID + NProjectCode + NPageNo, bds.Tables[0].Rows.Count, fileType, saveUrl);
                                        string NewFile2 = String.Format("{3}\\{0}已被修改{1}.{2}", Model_File.Medical_Institution_ID + NProjectCode + NPageNo, bds.Tables[0].Rows.Count, fileType, saveUrl);
                                        Model_File.File_Url = NewFile2;
                                        IbFile.Update(Model_File);
                                        File.Move(OrignFile, NewFile);

                                        Bitmap bitmap1;
                                        Log.Info("1取图片!");
                                        bitmap1 = (Bitmap)Bitmap.FromFile(NewFile);
                                        // 如果高大于600小于1100

                                        if (bitmap1.Height > 600)
                                        {
                                            Log.Info("2高度大于600!");
                                            if (bitmap1.Width > bitmap1.Height)
                                            {
                                                Log.Info("3旋转图片！");
                                                bitmap1.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                                Log.Info("4保存图片!");
                                                bitmap1.Save(NewFile, System.Drawing.Imaging.ImageFormat.Jpeg);
                                            }
                                        }
                                        Log.Info("5清除!");
                                        bitmap1.Dispose();
                                        //新文件
                                        FileStream Wrfs = new FileStream(String.Format("{0}/{2}\\{1}.jpg", @create_successful, Model_File.Medical_Institution_ID + NProjectCode + NPageNo, saveUrl), FileMode.Create, FileAccess.Write);
                                        Wrfs.Write(filedata, 0, filedata.Length);
                                        Wrfs.Close();

                                        Bitmap bitmap2;
                                        Log.Info("6取图片!");
                                        bitmap2 = (Bitmap)Bitmap.FromFile(String.Format("{0}/{2}\\{1}.jpg", @create_successful, Model_File.Medical_Institution_ID + NProjectCode + NPageNo, saveUrl));
                                        if (bitmap2.Height > 600)
                                        {
                                            Log.Info("7高度大于600!");
                                            if (bitmap2.Width > bitmap2.Height)
                                            {
                                                Log.Info("8旋转图片！");
                                                bitmap2.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                                Log.Info("9保存图片!");
                                                bitmap2.Save(String.Format("{0}/{2}\\{1}.jpg", @create_successful, Model_File.Medical_Institution_ID + NProjectCode + NPageNo, saveUrl));
                                            }
                                        }
                                        Log.Info("10清除!");
                                        bitmap2.Dispose();

                                        Model_File.File_Url = OrignFile2;
                                        Model_File.ChangeStatus = 0;
                                        Model_File.UniqueID = Math.Abs(Guid.NewGuid().GetHashCode()) + DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Millisecond; //每条不一样
                                        Model_File.File_Name = filesName;
                                        IbFile.Add(Model_File);
                                        Log.Error(errorMsg);
                                    }
                                    else
                                    {
                                        string NProjectCode = !string.IsNullOrEmpty(Model_File.ProjectCode) ? "_" + Model_File.ProjectCode : "";
                                        string NPageNo = !string.IsNullOrEmpty(Model_File.PageNo) ? "_" + Model_File.PageNo : "";

                                        //新文件  
                                        if (!Directory.Exists(string.Format("{0}/{1}", @create_successful, saveUrl)))
                                        {
                                            Directory.CreateDirectory(string.Format("{0}/{1}", @create_successful, saveUrl));
                                            FileStream Wrfs = new FileStream(String.Format("{0}/{2}\\{1}.jpg", @create_successful, Model_File.Medical_Institution_ID + NProjectCode + NPageNo, saveUrl), FileMode.OpenOrCreate, FileAccess.Write);
                                            Wrfs.Write(filedata, 0, filedata.Length);
                                            Wrfs.Close();
                                        }
                                        else
                                        {
                                            FileStream Wrfs = new FileStream(String.Format("{0}/{2}\\{1}.jpg", @create_successful, Model_File.Medical_Institution_ID + NProjectCode + NPageNo, saveUrl), FileMode.OpenOrCreate, FileAccess.Write);
                                            Wrfs.Write(filedata, 0, filedata.Length);
                                            Wrfs.Close();
                                        }
                                        Bitmap bitmap3;
                                        Log.Info("11取图片!");
                                        bitmap3 = (Bitmap)Bitmap.FromFile(String.Format("{0}/{2}\\{1}.jpg", @create_successful, Model_File.Medical_Institution_ID + NProjectCode + NPageNo, saveUrl));
                                        if (bitmap3.Height > 600)
                                        {
                                            Log.Info("12高度大于600!");
                                            if (bitmap3.Width > bitmap3.Height)
                                            {
                                                Log.Info("13旋转图片！");
                                                bitmap3.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                                Log.Info("14保存图片!");
                                                bitmap3.Save(String.Format("{0}/{2}\\{1}.jpg", @create_successful, Model_File.Medical_Institution_ID + NProjectCode + NPageNo, saveUrl));
                                            }
                                        }
                                        Log.Info("15清除!");
                                        bitmap3.Dispose();
                                        Model_File.ChangeStatus = 0;
                                        //Model_File.File_Url = String.Format("{0}/{2}\\{1}.jpg", @create_successful, Model_File.Medical_Institution_ID, saveUrl);
                                        Model_File.File_Url = String.Format("/{1}\\{0}.jpg", Model_File.Medical_Institution_ID + NProjectCode + NPageNo, saveUrl);
                                        IbFile.Add(Model_File);
                                        Log.Error(errorMsg);
                                    }
                                }
                                else
                                {
                                    errorMsg = filesName + "-未检测到检验时间!";
                                    return 2;
                                }
                            }
                            else
                            {
                                errorMsg = filesName + "-未检测到被检验者!";
                                return 2;
                            }
                        }
                        catch (Exception ex)
                        {
                            errorMsg = ex.Message;
                            Log.Error(String.Format("上传时出错:①{0}--②{1}--③{2}--④{3}--⑤{4}--'{5} {6}'", filesName, ex.Message, ex, ex.StackTrace, ex.TargetSite, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
                            errorMsg += @create_successful;
                            return 0;
                        }

                        return state;
                    }
                    catch (Exception ex)
                    {
                        Log.Error(String.Format("上传时出错:①{0}--②{1}--③{2}--④{3}--⑤{4}--'{5} {6}'", filesName, ex.Message, ex, ex.StackTrace, ex.TargetSite, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
                        return 0;
                    }
                }
                else
                {
                    try //验证不成功方法
                    {
                        errorMsg = filesName + "-尺寸不符验证规则!";
                        return 2;
                    }
                    catch (Exception ex)
                    {
                        errorMsg = filesName + "-失败";
                        Log.Error(String.Format("上传时出错:{0}--{1}--{2}--{3}--{4}--'{5} {6}'", filesName, ex.Message, ex, ex.StackTrace, ex.TargetSite, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
                        return 0;
                    }
                }
            }
            else
            {
                try   //验证不成功方法
                {
                    errorMsg = filesName + "：格式不符验证规则!";
                    return 2;
                }
                catch (Exception ex)
                {
                    errorMsg = filesName + "-失败";
                    Log.Error(String.Format("上传时出错:{0}--{1}--{2}--{3}--{4}--'{5} {6}'", filesName, ex.Message, ex, ex.StackTrace, ex.TargetSite, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
                    return 0;
                }
            }
        }

        #region 佛山、芜湖用下载报告（不带报告图片下载功能）
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="count">条数-1为所有</param>
        /// <param name="WebLisFlag">状态10为已发布、11为以下载</param>
        /// <param name="ClientNo">送检单位</param>
        /// <param name="Startdate">起止时间</param>
        /// <param name="Enddate">截止时间</param>
        /// <param name="nodeReportForm">Form XML</param>
        /// <param name="Error">错误</param>
        /// <returns></returns>
        public bool QueryReport(int count, string WebLisFlag, string ClientNo, string Startdate, string Enddate, out string nodeReportForm, out string Error)
        {
            int defaultCount = -1;
            if (count > 0)
            {
                defaultCount = count;
            }
            Log.Info("QueryReport");
            Error = "";
            nodeReportForm = "";
            try
            {
                DataSet reportFormFull = new DataSet();
                XmlDocument xdReportForms = new XmlDocument();
                XmlDocument xx = new XmlDocument();
                Model.ReportFormFull Model_ReportFormFull = new Model.ReportFormFull();
                string strDLUnit = ZhiFang.Common.Public.ConfigHelper.GetConfigString("DownLoadUnit");
                //CLIENTNO,WEBLISSOURCEORGID,WEBLISORGID
                if (strDLUnit.ToUpper() == "CLIENTNO")
                    Model_ReportFormFull.CLIENTNO = ClientNo;
                else if (strDLUnit.ToUpper() == "WEBLISSOURCEORGID")
                    Model_ReportFormFull.WeblisSourceOrgId = ClientNo;


                Model_ReportFormFull.Startdate = Convert.ToDateTime(Startdate);
                Model_ReportFormFull.Enddate = Convert.ToDateTime(Enddate);
                //reportFormFull = ibrff.GetList(count, Model_ReportFormFull, "");
                string DBSourceType = ZhiFang.Common.Public.ConfigHelper.GetConfigString("DBSourceType");
                string strWhere = "";
                if (DBSourceType == "ZhiFang.DAL.Oracle.weblis")
                {
                    strWhere = " and r.ReceiveDate between to_date('" + Startdate + "','YYYY-MM-DD HH24:MI:SS') and to_date('" + Enddate + "','YYYY-MM-DD HH24:MI:SS')";
                }
                else
                {
                    strWhere = " and r.ReceiveDate between '" + Startdate + "' and '" + Enddate + "'";
                }
                string NoUpLoadSectionNo = ZhiFang.Common.Public.ConfigHelper.GetConfigString("NoUpLoadSectionNo");
                if (NoUpLoadSectionNo.Length > 3)
                {
                    strWhere += " and " + NoUpLoadSectionNo;
                }
                //此注视为走weblis平台，协同报告下载
                //string strWebFlag = "";
                //if (WebLisFlag == "10")
                //{
                //    strWebFlag = " and r.PRINTTIMES <=0 ";
                //}
                //else if (WebLisFlag == "11")
                //{
                //    strWebFlag = " and r.PRINTTIMES >0 ";
                //}
                //reportFormFull = ibv.GetViewData(defaultCount, "", " r.clientno ='" + ClientNo + "' and r.WebLisFlag in (" + WebLisFlag + ") " + strWhere, "");
                //不走weblis平台，不签收申请。手动录入申请
                string strWebFlag = "";
                if (WebLisFlag == "10")
                {
                    strWebFlag = " and r.isdown <=0 ";
                }
                else if (WebLisFlag == "11")
                {
                    strWebFlag = " and r.isdown >0 ";
                }
                if (ClientNo != null && ClientNo.Trim() != "")
                {
                    ClientNo = string.Join("','", ClientNo.Split(','));
                }
                reportFormFull = ibv.GetViewData(defaultCount, "_SynergyReportFormFull", " r.clientno in ('" + ClientNo + "')" + strWebFlag + strWhere, "");
                if (((reportFormFull != null) && (reportFormFull.Tables.Count > 0)) && (reportFormFull.Tables[0].Rows.Count > 0))
                {
                    xx.LoadXml(reportFormFull.GetXml());
                    nodeReportForm = xx.InnerXml;
                    Log.Info(xx.InnerXml);
                    return true;
                }
                else
                {
                    nodeReportForm = "";
                    Error = "没有报告！";
                    return false;
                }

            }
            catch (Exception e)
            {
                Error = DateTime.Now + ":" + e.ToString() + e.StackTrace;
                Log.Error("异常：" + Error);
                return false;
            }

        }

        /// <summary>
        /// PKI定制报告查询 已ds形式返回报告 
        /// </summary>
        /// <param name="WebLisFlag">下载标志 0未下载 1已下载</param>
        /// <param name="ClientNo">送检单位编号</param>
        /// <param name="Startdate">开始日期</param>
        /// <param name="Enddate">结束日期</param>
        /// <param name="ReportFormFull">返回ds类型的报告</param>
        /// <param name="Error">错误信息</param>
        /// <returns></returns>
        public bool QueryReport_PKI(string WebLisFlag, string ClientNo, string Startdate, string Enddate, out DataSet ReportFormFull, out string Error)
        {

            Log.Info("查询报告开始:");
            Error = "";
            ReportFormFull = new DataSet();
            try
            {
                string strWhere = "";
                strWhere = " and CHECKDATE between '" + Startdate + "' and '" + Enddate + "'";
                string strWebFlag = "";
                if (WebLisFlag == "10")
                {
                    strWebFlag = " and isdown =0 ";//未下载
                }
                else if (WebLisFlag == "11")
                {
                    strWebFlag = " and isdown =1 ";//已下载
                }
                ReportFormFull = ibv.GetViewData(-1, "ReportFormFull", " clientno in (" + ClientNo + ")" + strWebFlag + strWhere, "");
                if (((ReportFormFull != null) && (ReportFormFull.Tables.Count > 0)) && (ReportFormFull.Tables[0].Rows.Count > 0))
                {
                    return true;
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("ReportFormFull查不到数据!");
                    Error = "ReportFormFull查不到数据";
                    return true;
                }

            }
            catch (Exception e)
            {
                Error = DateTime.Now + ":" + e.ToString() + e.StackTrace;
                Log.Error("查询报告异常：" + Error);
                return false;
            }

        }

        //下载成功打标记
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BarCode"></param>
        /// <param name="ClientNo"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public bool Changestatus(string BarCode, string ClientNo, string Error)
        {
            Log.Info("Changestatus");
            Log.Info(DateTime.Now + ":" + "开始打标记");
            Error = "";
            try
            {
                Model.ReportFormFull Model_ReportFormFull = new Model.ReportFormFull();
                Model_ReportFormFull.SERIALNO = BarCode;
                Model_ReportFormFull.CLIENTNO = ClientNo;
                DataSet dsrf = ibrff.GetList(1, Model_ReportFormFull, "");
                if (dsrf != null && dsrf.Tables.Count > 0 && dsrf.Tables[0].Rows.Count > 0)
                {
                    Log.Info(DateTime.Now + ":" + "当前报告状态为isdown：" + Model_ReportFormFull.Isdown);
                    Model_ReportFormFull.Isdown = 1;
                    int success = ibrff.Update(Model_ReportFormFull);

                    if (success > 0)
                    {
                        Log.Info(DateTime.Now + ":" + "打标记结束！当前报告状态为isdown：1");
                        return true;
                    }

                    else
                    {
                        Log.Info(DateTime.Now + ":" + "打标记结束！打标志出现异常");
                        return false;
                    }
                }
                return false;


            }
            catch (Exception e)
            {
                Error = DateTime.Now + ":" + e.StackTrace + e.ToString();
                Log.Error(DateTime.Now + ":" + e.StackTrace + e.ToString());
            }
            return false;
        }

        #region 转换XML
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

        #region 下载报告
        /// <summary>
        /// 下载报告
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public bool DownLoadReportFormID(string ReportFormID, string ClientNo, out string WebReportXML, out string Error)
        {
            Log.Info("DownLoadReportFormID");
            WebReportXML = "";
            Error = "";
            string str = "";
            try
            {
                Model.ReportFormFull Model_ReportFormFull = new Model.ReportFormFull();
                Model_ReportFormFull.ReportFormID = ReportFormID;
                DataSet reportFormFull = new DataSet();
                DataSet reportAllItemView = new DataSet();
                reportFormFull = ibv.GetViewData(1, "_ReportFormFullDataSource", " ReportFormID='" + ReportFormID + "'", "");

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
                        reportAllItemView = ibv.GetViewData(-1, "_ReportItemFullDataSource", "  ReportFormID='" + ReportFormID + "' ", "");
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
                        reportAllItemView.Clear();
                        reportAllItemView = ibv.GetViewData(-1, "_ReportMicroFullDataSource", "  ReportFormID='" + ReportFormID + "' ", "");
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
                            return true;
                        }
                        Log.Info("ReportMicro:" + reportFormFull.Tables[0].Rows.Count);
                        reportAllItemView.Clear();
                        reportAllItemView = ibv.GetViewData(-1, "_ReportMarrowFullDataSource", "  ReportFormID='" + ReportFormID + "' ", "");
                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                            {
                                Error += str;
                                return false;
                            }
                            reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMarrow"), "WebReportFile");
                            WebReportXML = document.InnerXml;
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
        #endregion

        #region 下载报告
        /// <summary>
        /// 下载报告 鄞州人民医院 报告现在对照关系取的是B_ResultTestItemControl 
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public bool DownLoadReportFormID_yinzhou(string ReportFormID, string ClientNo, out string WebReportXML, out string Error)
        {
            Log.Info("DownLoadReportFormID");
            WebReportXML = "";
            Error = "";
            string str = "";
            try
            {
                Model.ReportFormFull Model_ReportFormFull = new Model.ReportFormFull();
                Model_ReportFormFull.ReportFormID = ReportFormID;
                DataSet reportFormFull = new DataSet();
                DataSet reportAllItemView = new DataSet();
                reportFormFull = ibv.GetViewData(1, "_ReportFormFullDataSource", " ReportFormID='" + ReportFormID + "'", "");

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
                    reportFormFull = MatchClientNo_yinzhou(reportFormFull, ClientNo);

                    int[] numArray = new int[1];
                    XmlDocument document = TransformDTRowIntoXML(DataTableHelper.ColumnNameToUpper(reportFormFull.Tables[0]), "WebReportFile", "ReportForm", numArray);
                    for (int index = 0; index < reportFormFull.Tables[0].Rows.Count; index++)
                    {
                        reportAllItemView.Clear();
                        reportAllItemView = ibv.GetViewData(-1, "_ReportItemFullDataSource", "  ReportFormID='" + ReportFormID + "' ", "");
                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            if (!ibrif.CheckReportItemCenter_yinzhou(reportAllItemView, ClientNo, out str))
                            {
                                Error += str;
                                return false;
                            }
                            reportAllItemView = MatchClientNo_yinzhou(reportAllItemView, ClientNo);
                            //ZhiFang.Common.Log.Log.Debug("reportAllItemView->xml:" + reportAllItemView.GetXml());
                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportItem"), "WebReportFile");
                            WebReportXML = document.InnerXml;
                            return true;
                        }
                        Log.Info("ReportItem:" + reportFormFull.Tables[0].Rows.Count);
                        reportAllItemView.Clear();
                        reportAllItemView = ibv.GetViewData(-1, "_ReportMicroFullDataSource", "  ReportFormID='" + ReportFormID + "' ", "");
                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            if (!ibrif.CheckReportItemCenter_yinzhou(reportAllItemView, ClientNo, out str))
                            {
                                Error += str;
                                return false;
                            }
                            reportAllItemView = MatchClientNo_yinzhou(reportAllItemView, ClientNo);
                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMicro"), "WebReportFile");
                            WebReportXML = document.InnerXml;
                            return true;
                        }
                        Log.Info("ReportMicro:" + reportFormFull.Tables[0].Rows.Count);
                        reportAllItemView.Clear();
                        reportAllItemView = ibv.GetViewData(-1, "_ReportMarrowFullDataSource", "  ReportFormID='" + ReportFormID + "' ", "");
                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            if (!ibrif.CheckReportItemCenter_yinzhou(reportAllItemView, ClientNo, out str))
                            {
                                Error += str;
                                return false;
                            }
                            reportAllItemView = MatchClientNo_yinzhou(reportAllItemView, ClientNo);
                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMarrow"), "WebReportFile");
                            WebReportXML = document.InnerXml;
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
                return false;
            }

        }
        #endregion

        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="ClientNo"></param>
        /// <param name="imageByte"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public bool DownLoadImageReport(string ReportFormID, string ClientNo, out byte imageByte, out string Msg)
        {
            imageByte = 0;
            Msg = "";



            return false;
        }

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
        #endregion

        #region 下载报告 PKI
        /// <summary>
        /// PKI定制报告下载服务 把ReportFormFull,ReportItemFull,ReportMicroFull,ReportMarrowFull拷贝到本地数据库 
        /// </summary>
        /// <param name="ReportItemFull">返回常规项目</param>
        /// <param name="ReportMicroFull">返回微生物项目</param>
        /// <param name="ReportMarrowFull">返回病理项目</param>
        /// <param name="Error">返回错误信息</param>
        /// <param name="ReportFormID">报告单号</param>
        /// <param name="strClientNo">送检单位编号</param>
        /// <returns></returns>
        public bool DownLoadReportForm_PKI(out DataSet ReportItemFull, out DataSet ReportMicroFull, out DataSet ReportMarrowFull, out byte[] pdfData, out string Error, string ReportFormID)
        {
            Log.Info("开始下载报告");
            ReportItemFull = new DataSet();
            ReportMicroFull = new DataSet();
            ReportMarrowFull = new DataSet();
            pdfData = null;
            Error = "";
            try
            {
                ZhiFang.Common.Log.Log.Info("开始查询ReportItemFull");
                ReportItemFull = ibv.GetViewData(-1, "ReportItemFull", "  ReportFormID='" + ReportFormID + "' ", "");
                DataSet dsreportform = ibrff.GetList("  ReportFormID='" + ReportFormID + "' ");
                if (dsreportform != null && dsreportform.Tables.Count > 0 && dsreportform.Tables[0].Rows.Count > 0)
                {
                    string WeblisSourceOrgId = dsreportform.Tables[0].Rows[0]["WeblisSourceOrgId"].ToString();
                    if (ReportItemFull != null && ReportItemFull.Tables[0].Rows.Count > 0)
                    {
                        if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("TransCodField") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("TransCodField").ToUpper().Contains("ITEMNO"))
                        {
                            if (ReportItemFull.Tables[0].Columns.Contains("ItemNo") || ReportItemFull.Tables[0].Columns.Contains("ITEMNO"))
                            {
                                for (int i = 0; i < ReportItemFull.Tables[0].Rows.Count; i++)
                                {
                                    ReportItemFull.Tables[0].Rows[i]["ITEMNO"] = ibtic.GetLabCodeNo(WeblisSourceOrgId, ReportItemFull.Tables[0].Rows[i]["ITEMNO"].ToString());
                                }
                            }
                        }
                        if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("TransCodField") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("TransCodField").ToUpper().Contains("PARITEMNO"))
                        {
                            if (ReportItemFull.Tables[0].Columns.Contains("PARITEMNO"))
                            {
                                for (int i = 0; i < ReportItemFull.Tables[0].Rows.Count; i++)
                                {
                                    ReportItemFull.Tables[0].Rows[i]["PARITEMNO"] = ibtic.GetLabCodeNo(WeblisSourceOrgId, ReportItemFull.Tables[0].Rows[i]["PARITEMNO"].ToString());
                                }
                            }
                        }


                        ZhiFang.Common.Log.Log.Info("查询到ReportItemFull记录总数:" + ReportItemFull.Tables[0].Rows.Count);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("ReportItemFull记录为0");
                    }
                }
                ZhiFang.Common.Log.Log.Info("开始查询ReportMicroFull");
                ReportMicroFull = ibv.GetViewData(-1, "ReportMicroFull", "  ReportFormID='" + ReportFormID + "' ", "");
                if (ReportMicroFull != null && ReportMicroFull.Tables[0].Rows.Count > 0)
                {
                    ZhiFang.Common.Log.Log.Info("查询到ReportMicroFull记录总数:" + ReportMicroFull.Tables[0].Rows.Count);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("ReportMicroFull记录为0");
                }
                ZhiFang.Common.Log.Log.Info("开始查询ReportMarrowFull");
                ReportMarrowFull = ibv.GetViewData(-1, "ReportMarrowFull", "  ReportFormID='" + ReportFormID + "' ", "");
                if (ReportMarrowFull != null && ReportMarrowFull.Tables[0].Rows.Count > 0)
                {
                    ZhiFang.Common.Log.Log.Info("查询到ReportMarrowFull记录总数:" + ReportMarrowFull.Tables[0].Rows.Count);
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("ReportMarrowFull记录为0");
                }

                this.GetPDF(out pdfData, out Error, ReportFormID);
            }

            catch (Exception e)
            {
                Error = DateTime.Now + ":下载报告异常" + e.ToString() + e.StackTrace;
                return false;
            }
            return true;
        }

        /// <summary>
        /// 读取PDF文件
        /// </summary>
        /// <param name="PDFData">PDF文件流</param>
        /// <param name="Error">错误信息</param>
        /// <param name="ReportFormID">报告单号</param>
        private void GetPDF(out byte[] PDFData, out string Error, string ReportFormID)
        {
            PDFData = null;
            Error = null;
            string UploadDate = null;
            string TestDate = null;
            try
            {
                string pdfPath = System.Configuration.ConfigurationManager.AppSettings["ReportConfigPath"];
                if (string.IsNullOrEmpty(pdfPath))
                {
                    return;
                }
                if (!string.IsNullOrEmpty(ZhiFang.Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir")))
                {
                    pdfPath = pdfPath + "\\" + Common.Public.ConfigHelper.GetConfigString("ReportFormFilesDir");
                }
                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("isUpLoadReport") != null && ZhiFang.Common.Public.ConfigHelper.GetConfigString("isUpLoadReport").ToString() == "1")
                {
                    pdfPath = pdfPath + "\\Report";
                }
                ZhiFang.Common.Log.Log.Info("开始读取PDF文件");
                DataSet ds = ibv.GetViewData(-1, "ReportFormFull", "  ReportFormID='" + ReportFormID + "' ", "");
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    UploadDate = ds.Tables[0].Rows[0]["UploadDate"] == null ? null : (DateTime.Parse(ds.Tables[0].Rows[0]["UploadDate"].ToString())).ToString("yyyy/MM/dd");
                    if (UploadDate != null)
                    {
                        ZhiFang.Common.Log.Log.Info("UploadDate:" + UploadDate + ",根据UploadDate年月日查找PDF文件所在目录");
                        TestDate = UploadDate.Split(' ')[0];

                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Info("UploadDate为空,尝试取今天日期查找PDF文件所在目录");
                        TestDate = DateTime.Now.ToString("yyyy/MM/dd");
                    }
                    string Year = TestDate.Split('/')[0];
                    int Month = int.Parse(TestDate.Split('/')[1]);
                    int Day = int.Parse(TestDate.Split('/')[2]);
                    pdfPath = pdfPath + "\\" + Year + "\\" + Month + "\\" + Day + "\\";
                    ZhiFang.Common.Log.Log.Info("报告:" + ReportFormID + " 所在目录:" + pdfPath);
                    foreach (string pdfFile in Directory.GetFiles(pdfPath, "*.pdf"))
                    {
                        string FileName = Path.GetFileName(pdfFile);
                        ReportFormID = ReportFormID.Replace(':', '：');//替换成中文的冒号,因为英文格式的冒号在文件名里面是非法的
                        if (FileName.IndexOf(ReportFormID) > -1)
                        {
                            pdfPath += FileName;
                            ZhiFang.Common.Log.Log.Info("成功找到PDF文件路径:" + pdfPath);
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
                        ZhiFang.Common.Log.Log.Info("找不到PDF报告");
                        Error = "找不到PDF报告";
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info("不存在ReportFormFull记录:" + ReportFormID);
                    Error = "不存在ReportFormFull记录:" + ReportFormID;
                }

            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("获取PDF文件出错:" + ex.Message.ToString());
                Error = "获取PDF文件出错:" + ex.Message.ToString();
            }
        }

       
        #endregion

        #region 和合与四川大家版本用下载报告（四川大家申请可能不走weblis、和合走weblis、带图片报告下载功能）
        /// <summary>
        /// 下载报告（此是个定制版本，因为上传申请单时,没有将oleSerialno保存到到ReportForm表 保存到ReportItem表。所以查询关联ReportItem表的OleSerialno）
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="Msg"></param>
        /// <returns></returns>
        public bool DownLoadReportFormID_Hehe(string ReportFormID, string ClientNo, out string WebReportXML, out string fileData, out string Msg)
        {
            Log.Info("DownLoadReportFormID_Hehe");
            WebReportXML = "";
            Msg = "";
            fileData = "";
            string str = "";
            try
            {
                Model.ReportFormFull Model_ReportFormFull = new Model.ReportFormFull();
                Model_ReportFormFull.ReportFormID = ReportFormID;
                DataSet reportFormFull = new DataSet();
                DataSet reportAllItemView = new DataSet();
                //reportFormFull = ibv.GetViewData(1, "_ReportFormFullDataSource_Hehe", " r.ReportFormID='" + ReportFormID + "'", "");//中日

                reportFormFull = ibv.GetViewData_Revise(1, "DownLoadReportFormFull", " where rf.ReportFormID='" + ReportFormID + "'", ""); //四川
                Log.Info("ReportForm:" + reportFormFull.Tables[0].Rows.Count);
                if (((reportFormFull != null) && (reportFormFull.Tables.Count > 0)) && (reportFormFull.Tables[0].Rows.Count > 0))
                {
                    //检测ReportFormFull是否对照
                    if (!ibrff.CheckReportFormCenter(reportFormFull, ClientNo, out str))
                    {
                        Msg += str;
                        return false;
                    }
                    //转码过程
                    Log.Info("开始转码");
                    reportFormFull = MatchClientNo(reportFormFull, ClientNo);
                    Log.Info("转码结束");
                    int[] numArray = new int[1];
                    XmlDocument document = TransformDTRowIntoXML(DataTableHelper.ColumnNameToUpper(reportFormFull.Tables[0]), "WebReportFile", "ReportForm", numArray);
                    //document.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    for (int index = 0; index < reportFormFull.Tables[0].Rows.Count; index++)
                    {
                        reportAllItemView.Clear();
                        //reportAllItemView = ibv.GetViewData(-1, "_ReportItemFullDataSource", "  ri.ReportFormID='" + ReportFormID + "' ", ""); 中日、四川
                        reportAllItemView = ibv.GetViewData_Revise(-1, "DownLoadReportItemFull", " where ri.ReportFormID='" + ReportFormID + "' ", "");

                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                            {
                                Msg += str;
                                return false;
                            }
                            Log.Info("开始转码item");
                            Log.Info("reportAllItemView开始" + reportAllItemView.GetXml());
                            reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                            Log.Info("reportAllItemView结束" + reportAllItemView.GetXml());
                            Log.Info("转码结束item");
                            //document.AppendChild(document.CreateXmlDeclaration("1.0", "utf-8", ""));

                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportItem"), "WebReportFile");
                            //string a="<?xml version="1.0" encoding="utf-8"?>"
                            //document.InnerXml.Insert(0,"");

                            WebReportXML = document.InnerXml;
                            //图片
                            GetReportFile(ref fileData, ref Msg, reportFormFull);
                            return true;
                        }
                        //Log.Info("ReportItem:" + reportFormFull.Tables[0].Rows.Count);
                        reportAllItemView.Clear();
                        //reportAllItemView = ibv.GetViewData(-1, "_ReportMicroFullDataSource", "  rm.ReportFormID='" + ReportFormID + "' ", "");  //四川
                        reportAllItemView = ibv.GetViewData_Revise(-1, "DownLoadReportMicroFull", " where rm.ReportFormID='" + ReportFormID + "' ", "");
                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                            {
                                Msg += str;
                                return false;
                            }
                            reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMicro"), "WebReportFile");
                            WebReportXML = document.InnerXml;
                            //图片
                            GetReportFile(ref fileData, ref Msg, reportFormFull);
                            return true;
                        }
                        //Log.Info("ReportMicro:" + reportFormFull.Tables[0].Rows.Count);
                        reportAllItemView.Clear();
                        //reportAllItemView = ibv.GetViewData(-1, "_ReportMarrowFullDataSource", "  rmm.ReportFormID='" + ReportFormID + "' ", "");//四川
                        reportAllItemView = ibv.GetViewData_Revise(-1, "DownLoadReportMarrowFull", " where rmm.ReportFormID='" + ReportFormID + "' ", "");
                        if (((reportAllItemView != null) && (reportAllItemView.Tables.Count > 0)) && (reportAllItemView.Tables[0].Rows.Count > 0))
                        {
                            //检测ReportItemFull是否对照
                            if (!ibrif.CheckReportItemCenter(reportAllItemView, ClientNo, out str))
                            {
                                Msg += str;
                                return false;
                            }
                            reportAllItemView = MatchClientNo(reportAllItemView, ClientNo);
                            document = TransDataToXML.MergeXML(document, TransDataToXML.TransformDTIntoXML(DataTableHelper.ColumnNameToUpper(reportAllItemView.Tables[0]), "WebReportFile", "ReportMarrow"), "WebReportFile");
                            WebReportXML = document.InnerXml;
                            //报告形式的图片(jpg)
                            GetReportFile(ref fileData, ref Msg, reportFormFull);
                            return true;
                        }
                        Log.Info("ReportMarrow:" + reportFormFull.Tables[0].Rows.Count);
                    }

                }
                return true;
            }
            catch (Exception e)
            {
                Msg = DateTime.Now + ":" + e.ToString() + e.StackTrace;
                return false;
            }
        }

        //图片
        private static void GetReportFile(ref string fileData, ref string Msg, DataSet reportFormFull)
        {
            Log.Info("GetReportFile");
            try
            {
                string url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("jpgorpdf");
                //图片
                string date = url + Convert.ToDateTime(reportFormFull.Tables[0].Rows[0]["RECEIVEDATE"]).Year.ToString() + "\\" + Convert.ToDateTime(reportFormFull.Tables[0].Rows[0]["RECEIVEDATE"]).Month.ToString() + "\\" + Convert.ToDateTime(reportFormFull.Tables[0].Rows[0]["RECEIVEDATE"]).Day.ToString();
                string fileNameUnique = date + "\\" + Convert.ToDateTime(reportFormFull.Tables[0].Rows[0]["REPORTFORMID"].ToString().Split('_')[5]).ToString("yyyy-MM-dd") + "\\" + reportFormFull.Tables[0].Rows[0]["REPORTFORMID"].ToString().Split('_')[1] + "\\";
                //string fileNameUnique = date + "\\";
                if (!Directory.Exists(fileNameUnique))
                {
                    Log.Info("服务器不存在此路径:" + fileNameUnique);
                    Msg = "服务器不存在此路径" + fileNameUnique;
                }
                else
                {
                    Log.Info("服务器报告图片路径:" + fileNameUnique);
                    DirectoryInfo TheFolder = new DirectoryInfo(fileNameUnique);
                    //定义文件数组
                    string files = "";
                    var arrayFile = new string[TheFolder.GetFiles().Length];
                    //遍历文件
                    foreach (FileInfo NextFile in TheFolder.GetFiles())
                    {
                        files += NextFile.Name + "@";
                    }
                    Log.Info(files);
                    arrayFile = files.Trim().TrimEnd('@').Split('@');
                    for (int i = 0; i < arrayFile.Length; i++)
                    {
                        int j = i + 1;
                        Msg = "Error+";
                        try
                        {
                            url = ZhiFang.Common.Public.ConfigHelper.GetConfigString("jpgorpdf");
                            Log.Info("路径+文件名:" + fileNameUnique + arrayFile[i]);
                            FileStream fs = new FileStream(fileNameUnique + arrayFile[i], FileMode.Open);//可以是其他重载方法 
                            byte[] byData = new byte[fs.Length];
                            fs.Read(byData, 0, byData.Length);
                            fs.Close();
                            fileData += Convert.ToBase64String(byData) + ",";
                        }
                        catch (Exception ex)
                        {
                            fileData = null;
                            Msg += ex.ToString();
                            Log.Error("错误信息:" + ex);
                        }
                    }
                    fileData = fileData.TrimEnd(',');
                }
            }
            catch (Exception ex)
            {
                Log.Error("错误信息:" + ex.ToString() + ex.TargetSite + ex.StackTrace);
            }
        }
        #endregion
    }
}
