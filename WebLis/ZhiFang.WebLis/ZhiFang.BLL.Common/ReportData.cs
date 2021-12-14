using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using ZhiFang.Common.Log;
using System.Collections;
using ZhiFang.Tools;
using System.Configuration;
using System.Data;
using ZhiFang.IDAL;
using ZhiFang.DALFactory;
using System.IO;
using ZhiFang.IBLL.Common;
using ZhiFang.IBLL.Common.BaseDictionary;
using ZhiFang.Model;
using ZhiFang.Common.Public;
using ZhiFang.IDAL.Other;

namespace ZhiFang.BLL.Common
{
    public class ReportData : IBReportData
    {
        private readonly IDB_Lab_Base idblb = DalFactory<IDB_Lab_Base>.GetDalByClassName("B_Lab_Base");
        private readonly IDapplybase iapply = DalFactory<IDapplybase>.GetDalByClassName("applybase");
        /// <summary>
        /// 上传检验报告
        /// </summary>
        /// <param name="xmlData">xml数据</param>
        /// <param name="pdfdata">pdf检验报告</param>
        /// <param name="pdfdata_td">套打pdf检验报告</param>
        /// <param name="fileData">其他文件，例如jpg,frp，word,rtf等</param>
        /// <param name="fileType">其他文件的类型</param>
        /// <param name="errormsg">错误信息</param>
        /// <returns>
        /// 0 成功
        /// 负数:失败
        ///     -1:系统出现异常
        ///     -2:数据格式不合法等
        /// </returns>
        public int UpLoadReportDataFromBytes(byte[] xmlData, byte[] pdfdata, byte[] pdfdata_td, byte[] fileData, string fileType, out string errorMsg, out long OutReportformIndexID)
        {
            OutReportformIndexID = 0;
            string msg = "调用方法“UpLoadReportDataFromBytes”上传检验报告!\r\n";
            Log.Info(msg);
            int result = -2;
            errorMsg = "";
            //XML文件
            if (xmlData == null)
            {
                errorMsg = "上传失败:没有数据,即xmldata数据为null";
                Log.Error(errorMsg);
                return result;
            }
            try
            {
                //报告数据存放的目录
                string reportSavePath = ReportData.getReportDataSaveDiskPath();
                //分析xml数据
                System.Text.UTF8Encoding converter = new UTF8Encoding();
                string xml = converter.GetString(xmlData);
                if (xml == "")
                {
                    errorMsg = "\r\n上传失败:没有数据,即xmldata数据为空串!";
                    Log.Error(errorMsg);
                    return result;
                }

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string xPath = "//ReportForm";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                if (nodelist.Count != 1)
                {
                    errorMsg = "\r\n上传失败:上传的XML数据格式不符合规定!\r\nReportForm节点必须存在,且只能有一个!";
                    Log.Error(errorMsg + xml);
                    return result;
                }
                XmlNode xmlNode = nodelist[0];
                Hashtable hashReportForm = GetXMLData.getXmlNodeNameAndValue(xmlNode);
                //将字段的名称转化成大写
                hashReportForm = ConvertData.convertHashKeyToUpper(hashReportForm);
                
                //生成统一的文件名称
                string fileNameUnique = "";
                if (hashReportForm["REPORTFORMID"] == null)
                {
                    errorMsg = "\r\n上传失败:上传的XML数据格式不符合规定!没有REPORTFORMID字段!\r\n";
                    Log.Error(errorMsg + xml);
                    //应该退出程序!!!!
                    return result;
                }
                else
                {
                    fileNameUnique = hashReportForm["REPORTFORMID"].ToString();
                    if (fileNameUnique == "")
                    {
                        errorMsg = "\r\n上传失败:上传的XML数据格式不符合规定!REPORTFORMID字段的内容为空!\r\n";
                        Log.Error(errorMsg + xml);
                        //应该退出程序!!!!
                        return result;
                    }
                }


                if (fileNameUnique == "")
                    fileNameUnique = System.Guid.NewGuid().ToString();
                fileNameUnique = fileNameUnique.Replace(":", "：");
                //xml数据要保存的文件名称
                string xmlFileName = reportSavePath + fileNameUnique + ".xml";
                msg = "保存xml数据到本地文件：\r\n" + xmlFileName;
                Log.Info(msg);
                //保存xml数据到本地文件
                ZhiFang.Tools.Tools.writeStringToLocalFile(xmlFileName, xml);
                //保存到数据库
                //保存pdf文件
                string pdfFileName = "";
                if (pdfdata != null)
                {
                    if (pdfdata.Length > 0)
                    {
                        //    fileNameUnique = Convert.ToDateTime(hashReportForm["RECEIVEDATE"]).ToString("yyyy-MM-dd")
                        //+ ";" + hashReportForm["SectionNo"].ToString()
                        //+ ";" + hashReportForm["TestTypeNo"].ToString()
                        //+ ";" + hashReportForm["SampleNo"].ToString();

                        pdfFileName = reportSavePath + fileNameUnique + ".pdf";
                        msg = "保存PDF数据到本地文件：\r\n" + pdfFileName;
                        Log.Info(msg);
                        //保存到本地文件
                        ZhiFang.Tools.Tools.writeBytesToLocalFile(pdfFileName, pdfdata);
                    }
                    else
                    {
                        msg = "没有PDF数据！\r\n";
                        Log.Error(msg);
                    }
                }
                //保存pdf文件(套打)
                string pdfFileNameTD = "";
                if (pdfdata_td != null)
                {
                    if (pdfdata_td.Length > 0)
                    {
                        pdfFileNameTD = reportSavePath + "T" + fileNameUnique + ".pdf";
                        msg = "保存套打的PDF数据到本地文件：\r\n" + pdfFileNameTD;
                        Log.Info(msg);
                        //保存
                        ZhiFang.Tools.Tools.writeBytesToLocalFile(pdfFileNameTD, pdfdata_td);
                    }
                }
                //保存其他文件
                string pdfFileNameOther = "";
                reportSavePath = ReportData.getReportDataSaveDiskPath(hashReportForm["RECEIVEDATE"].ToString());
                if (fileData != null)
                {
                    if (fileData.Length > 0)
                    {
                        pdfFileNameOther = reportSavePath + fileNameUnique + "_QT." + fileType;
                        msg = "保存其他的图片数据到本地文件：\r\n" + pdfFileNameOther;
                        Log.Info(msg);
                        //保存
                        ZhiFang.Tools.Tools.writeBytesToLocalFile(pdfFileNameOther, fileData);
                    }
                }
                //保存到数据库
                result = ReportData.saveReportDataToDB(xml, reportSavePath, pdfFileName, pdfFileNameTD, pdfFileNameOther, out errorMsg, out OutReportformIndexID);
                if (result == 0)
                {
                    errorMsg = "上传文件成功!";
                }
                else
                    errorMsg = "上传文件失败!";
            }
            catch (System.Exception ex)
            {
                result = -1;
                errorMsg = "上传数据出错:" + ex.ToString();
                Log.Error(ex.ToString());
            }
            return result;
        }
        /// <summary>
        /// 保存报告到数据库
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="reportSavePath"></param>
        /// <param name="pdfFileName"></param>
        /// <param name="pdfFileNameTD"></param>
        /// <param name="pdfFileNameOther"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static int saveReportDataToDB(string xml, string reportSavePath, string pdfFileName, string pdfFileNameTD, string pdfFileNameOther, out string errorMsg, out long OutReportformIndexID)
        {
            //Log.Info("IDBaseDALLisDB ibdallisdb = DalFactory<IDBaseDALLisDB>.GetDal()");
            IDBaseDALLisDB ibdallisdb = DalFactory<IDBaseDALLisDB>.GetBaseDal();
            OutReportformIndexID = 0;
            string msg = "调用方法“saveReportDataToDB”保存报告到数据库!\r\n";
            Log.Info(msg);
            int result = -1;
            errorMsg = "";
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string xPath = "//ReportForm";

                XmlNodeList nodelist = doc.SelectNodes(xPath);
                //ReportForm节点必须存在,且只能有一个
                #region 暂时关闭 两处
                //if (nodelist.Count != 1)
                //{
                //    errorMsg = "\r\n上传的XML数据格式不符合规定!ReportForm节点必须存在,且只能有一个\r\n";
                //    Log.Error(errorMsg + xml);
                //    return result;
                //}
                #endregion
                XmlNode reportFormNodeSave = nodelist[0];
                //第一级是保存到ReportFormFull
                string tableNameForm = "ReportFormFull";
                //第二级是保存到ReportItemFull(生化，免疫),ReportMarrowFull(细胞学),ReportMicroFull（微生物）
                string tableNameItem = "ReportItemFull";
                string tableNameItemXMLData = "ReportItem";
                //字段列表
                Hashtable hashFormColumn = ReportData.getTableColumnFormList(tableNameForm);

                //从传入的XML数据取子表的名称，根据子表名称来确定报告项目的表名称，我们一般在原来的表名称基础上加Full,
                //字段就比原来的多加两个(REPORTFORMID,REPORTITEMID)
                // xPath = "/WebReportFile";
                xPath = "NewDataSet";
                nodelist = doc.SelectNodes(xPath);
                //if (nodelist.Count != 1)
                //{
                //    errorMsg = "\r\n上传的XML数据格式不符合规定!必须以WebReportFile节点作为根节点,且只能有一个\r\n";
                //    Log.Error(errorMsg + xml);
                //    return result;
                //}
                foreach (XmlNode xmlNode in nodelist[0].ChildNodes)
                {
                    string nodeName = xmlNode.Name.ToLower();
                    if (nodeName == "reportform")
                        continue;
                    //由于子表数据可能是多个，所以只取第一个就行
                    bool isGet = false;
                    switch (nodeName)
                    {
                        case "reportitem"://生化，免疫
                            tableNameItem = "ReportItemFull";
                            tableNameItemXMLData = "ReportItem";
                            isGet = true;
                            break;
                        case "reportmarrow"://细胞学
                            tableNameItem = "ReportMarrowFull";
                            tableNameItemXMLData = "ReportMarrow";
                            isGet = true;
                            break;
                        case "reportmicro"://微生物
                            tableNameItem = "ReportMicroFull";
                            tableNameItemXMLData = "ReportMicro";
                            isGet = true;
                            break;
                    }
                    if (isGet == true)
                        break;
                }
                //字段列表
                Hashtable hashItemColumn = ReportData.getTableColumnNameList(tableNameItem);

                //取主表的字段名称和内容
                Hashtable hashForm = GetXMLData.getXmlNodeNameAndValue(reportFormNodeSave);
                //转换字段名称为大写
                hashForm = ConvertData.convertHashKeyToUpper(hashForm);

                //取XML数据表对应的主键(唯一索引)
                string pkWhere = ReportData.getReportDataPkWhere(hashForm);
                if (pkWhere == "")
                {
                    errorMsg = "\r\n上传的XML数据格式不符合规定!没有主键字段ReportFormID或者该字段没有内容！\r\n";
                    Log.Error(errorMsg + xml);
                    return result;
                }
                IDReportFormFull dalrff = DalFactory<IDReportFormFull>.GetDal("ReportFormFull", ZhiFang.Common.Dictionary.DBSource.LisDB());
                IDReportItemFull dalrif = DalFactory<IDReportItemFull>.GetDal("ReportItemFull", ZhiFang.Common.Dictionary.DBSource.LisDB());
                IDReportMicroFull dalrmicrof = DalFactory<IDReportMicroFull>.GetDal("ReportMicroFull", ZhiFang.Common.Dictionary.DBSource.LisDB());
                IDReportMarrowFull dalrmarrowf = DalFactory<IDReportMarrowFull>.GetDal("ReportMarrowFull", ZhiFang.Common.Dictionary.DBSource.LisDB());

                #region weblis苏州pki项目,保留之前的报告记录,将记录插入_backup表

                if (ZhiFang.Common.Public.ConfigHelper.GetConfigString("isBackUpReport") == "1")
                {
                    try
                    {
                        dalrif.BackUpReportItemFullByWhere(pkWhere);
                        dalrmicrof.BackUpReportMicroFullByWhere(pkWhere);
                        dalrmarrowf.BackUpReportMarrowFullByWhere(pkWhere);
                        dalrff.BackUpReportFormFullByWhere(pkWhere);
                    }
                    catch (Exception e) 
                    {
                        
                        throw;
                    }
                }
                #endregion

                //先删除以前的数据
                dalrif.DeleteByWhere(pkWhere);
                dalrmicrof.DeleteByWhere(pkWhere);
                dalrmarrowf.DeleteByWhere(pkWhere);

                //获取或设置REPORTFORMINDEXID
                DataSet ds = dalrff.GetList(pkWhere);
                long REPORTFORMINDEXID = GUIDHelp.GetGUIDLong();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Rows[0]["ReportFormIndexID"] != null && ds.Tables[0].Rows[0]["ReportFormIndexID"].ToString().Trim() != "")
                {
                    REPORTFORMINDEXID = long.Parse(ds.Tables[0].Rows[0]["ReportFormIndexID"].ToString());
                }                
                OutReportformIndexID = REPORTFORMINDEXID;
                if (hashForm["REPORTFORMINDEXID"] == null)
                {
                    hashForm.Add("REPORTFORMINDEXID", REPORTFORMINDEXID);
                }
                else
                {
                    hashForm["REPORTFORMINDEXID"] = REPORTFORMINDEXID;
                }

                //再删除主表
                dalrff.DeleteByWhere(pkWhere);

                //插入新数据到数据库中
                //本次上传记录的主键
                string guid = System.Guid.NewGuid().ToString();
                if (hashForm["REPORTFORMID"] == null)
                    hashForm.Add("REPORTFORMID", "");
                else
                {
                    string pkFieldValue = hashForm["REPORTFORMID"].ToString().Trim();
                    if (pkFieldValue == "")
                    {
                        //主键自动生成
                        hashForm["REPORTFORMID"] = guid;
                    }
                    else
                    {
                        //主键取传进来的
                        guid = pkFieldValue;
                    }
                }
                //加新生成的PDF文件名称等
                if (hashForm["PDFFILE"] == null)
                    hashForm.Add("PDFFILE", "");
                string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
                //D:\WebLis\WebLis\WebLis\report\2009\12\23\_569_6_1_92_2009-09-15 00：00：00.pdf
                //hashForm["PDFFILE"] = pdfFileName.Replace(basePath, "");//相对路径
                if (pdfFileName != null && pdfFileName != "")
                {
                    hashForm["PDFFILE"] = pdfFileName.Substring(pdfFileName.IndexOf("report"));//相对路径
                }
                //hashForm["PDFFILE"] = pdfFileName;//绝对路径
                if (hashForm["JPGFILE"] == null)
                    hashForm.Add("JPGFILE", "");
                hashForm["JPGFILE"] = pdfFileNameOther.Replace(basePath, "");//相对路径
                //hashForm["JPGFILE"] = pdfFileNameOther;//绝对路径
                //将子表的名称保存到主表
                if (hashForm["CHILDTABLENAME"] == null)
                {
                    hashForm.Add("CHILDTABLENAME", tableNameItem);
                }
                else
                {
                    hashForm["CHILDTABLENAME"] = tableNameItem;
                }

               
                #region 小组类型验证
                if (hashForm["SECTIONTYPE"] == null)
                {
                    hashForm.Add("SECTIONTYPE", "");
                    if (tableNameItem == "ReportItemFull")
                        hashForm["SECTIONTYPE"] = 1;
                    else if (tableNameItem == "ReportMicroFull")
                        hashForm["SECTIONTYPE"] = 2;
                    else if (tableNameItem == "ReportMarrowFull")
                        hashForm["SECTIONTYPE"] = 5;
                }
                #endregion

                //先生成插入主表的SQL脚本
                int count = ibdallisdb.getInsertSQL(tableNameForm, hashFormColumn, hashForm);
                xPath = "//" + tableNameItemXMLData;
                nodelist = doc.SelectNodes(xPath);
                int itemID = 1;
                int itemrannum = 1000; //项目编号
                foreach (XmlNode xmlNode in nodelist)
                {
                    itemrannum = itemrannum + 1;
                    //取主表的字段名称和内容
                    Hashtable hashItem = GetXMLData.getXmlNodeNameAndValue(xmlNode);
                    //加外键
                    if (!hashItem.Contains("REPORTFORMID"))
                    {
                        hashItem.Add("REPORTFORMID", "");
                        hashItem["REPORTFORMID"] = guid;
                    }
                    else
                    {
                        if (hashItem["REPORTFORMID"] == null)
                        {
                            hashItem["REPORTFORMID"] = guid;
                        }
                    }
                    //子表的ID
                    if (hashItem["REPORTITEMID"] == null)
                        hashItem.Add("REPORTITEMID", "");
                    hashItem["REPORTITEMID"] = itemID++;// System.Guid.NewGuid().ToString();

                    if (tableNameItem == "ReportItemFull")
                    {
                        ZhiFang.Common.Log.Log.Debug("tableNameItem:ReportItemFull" );
                        if (!hashItem.Contains("ReportItemIndexID"))
                        {
                            hashItem.Add("ReportItemIndexID", GUIDHelp.GetGUIDLong());
                            ZhiFang.Common.Log.Log.Debug("hashItem[ReportItemIndexID]1:"+ hashItem["ReportItemIndexID"]);

                        }
                        hashItem["ReportItemIndexID"] = GUIDHelp.GetGUIDLong();
                        ZhiFang.Common.Log.Log.Debug("hashItem[ReportItemIndexID]2:" + hashItem["ReportItemIndexID"]);
                    }

                    if (tableNameItem == "ReportMarrowFull")
                    {
                        if (!hashItem.Contains("ReportMarrowIndexID") )
                        {
                            hashItem.Add("ReportMarrowIndexID", GUIDHelp.GetGUIDLong());
                        }
                        if (hashItem["ReportMarrowIndexID"] == null)
                        {
                            hashItem["ReportMarrowIndexID"] = GUIDHelp.GetGUIDLong();
                        }
                    }
                    if (tableNameItem == "ReportMicroFull")
                    {
                        if (!hashItem.Contains("ReportMicroIndexID") )
                        {
                            hashItem.Add("ReportMicroIndexID", GUIDHelp.GetGUIDLong());
                        }
                        if (hashItem["ReportMicroIndexID"] == null)
                        {
                            hashItem["ReportMicroIndexID"] = GUIDHelp.GetGUIDLong();
                        }
                    }
                    if (!hashItem.Contains("REPORTFORMINDEXID"))
                    {
                        hashItem.Add("REPORTFORMINDEXID", REPORTFORMINDEXID);
                    }
                    else
                    {
                        hashItem["REPORTFORMINDEXID"] = REPORTFORMINDEXID;
                    }
                    //先生成插入子表的SQL脚本
                    count = ibdallisdb.getInsertSQL(tableNameItem, hashItemColumn, hashItem);
                    Log.Info(tableNameItem + ":" + count);
                }

                msg = "成功！";
                Log.Info(msg);

                Log.Info("打标志开始");
                if (hashForm["BARCODE"] != null)
                {
                    if (UpdateReportStatus(hashForm["BARCODE"].ToString(), "10", out msg))
                    {
                        Log.Info(String.Format("打标志成功,barcode={0},weblisflag=10", hashForm["BARCODE"].ToString()));
                    }
                    else
                    {
                        Log.Error("打标志失败");
                    }
                }
                else
                {
                    Log.Error("未找到条码数据");
                }
                //string err;
                //string classname = "UpLoadReportForm";
                //if (System.Configuration.ConfigurationManager.AppSettings["classname"] != null && System.Configuration.ConfigurationManager.AppSettings["classname"].ToString().Trim() != "")
                //{
                //    classname = System.Configuration.ConfigurationManager.AppSettings["classname"].ToString().Trim();

                //IBUpLoadReportForm ibulrf = ZhiFang.BLLFactory.BLLFactory<IBUpLoadReportForm>.GetBLL(classname);
                //if (ibulrf != null)
                //{
                //    if (!(ibulrf.UpLoadReportForm(xml, out err) > 0))
                //    {
                //        Log.Error(err + xml);
                //        return -1;
                //    }
                //}
                //}
                //成功
                result = 0;
            }
            catch (System.Exception ex)
            {
                errorMsg = "\r\n上传失败:将数据保存到数据库是失败:\r\n" + ex.ToString();
                Log.Error(ex.ToString());
            }
            return result;
        }
        /// <summary>
        /// 取主键的查询表达式,即where条件
        /// </summary>
        /// <param name="hashForm"></param>
        /// <returns></returns>
        public static string getReportDataPkWhere(Hashtable hashForm)
        {
            string pkFieldName = "ReportFormID".ToUpper();
            string fieldWhereModal = "\"{0}\"='{1}'";
            string pkWhere = "";
            if (hashForm[pkFieldName] != null)
            {
                string pkFieldValue = hashForm[pkFieldName].ToString().Trim();
                if (pkFieldValue != "")
                {
                    if (pkWhere != "")
                        pkWhere += " AND ";
                    pkWhere += string.Format(fieldWhereModal, pkFieldName, pkFieldValue);
                }
            }


            return pkWhere;
        }

        /// <summary>
        /// 更新上传报告状态
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="weblisflag"></param>
        /// <param name="errmsg"></param>
        /// <returns></returns>
        public static bool UpdateReportStatus(string barcode, string weblisflag, out string errmsg)
        {
            errmsg = "";
            bool r = false;
            try
            {
                IDBarCodeForm dalbcf = DalFactory<IDBarCodeForm>.GetDal("BarCodeForm", ZhiFang.Common.Dictionary.DBSource.LisDB());
                ZhiFang.Model.BarCodeForm barCodeForm = new Model.BarCodeForm();
                barCodeForm.BarCode = barcode;
                barCodeForm.WebLisFlag = Convert.ToInt32(weblisflag);
                dalbcf.Update(barCodeForm);
                r = true;
            }
            catch (Exception ex)
            {
                r = false;
                errmsg = ex.Message;
            }
            return r;
        }

        /// <summary>
        /// 获取要报告数据的存放路径,如:report\年\月\日
        /// </summary>
        /// <returns></returns>
        public static string getReportDataSaveDiskPath()
        {
            //报告的统一路径
            string path = getReportConfigPath();
            //报告的发送日期
            System.DateTime date = System.DateTime.Now;
            path += date.Year.ToString() + "\\" + date.Month.ToString() + "\\" + date.Day.ToString() + "\\";
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }

        /// <summary>
        /// 获取要报告数据的存放路径,如:report\年\月\日
        /// </summary>
        /// <returns></returns>
        public static string getReportDataSaveDiskPath(string ReceiveDate)
        {
            //报告的统一路径
            string path = getReportConfigPath();
            //报告的发送日期
            if (!string.IsNullOrEmpty(ReceiveDate))
            {
                path += Convert.ToDateTime(ReceiveDate).Year.ToString() + "\\" + Convert.ToDateTime(ReceiveDate).Month.ToString() + "\\" + Convert.ToDateTime(ReceiveDate).Day.ToString() + "\\";
            }
            if (System.IO.Directory.Exists(path) == false)
            {
                System.IO.Directory.CreateDirectory(path);
            }
            return path;
        }
        /// <summary>
        /// 从数据库里获取某个表的字段名称列表(转换成大写)
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getTableColumnNameList(string tableName)
        {
            string msg = "调用方法“getTableColumnNameList”从数据库里获取某个表的字段名称列表(转换成大写)!\r\n";
            Log.Info(msg);
            DataSet ds = new DataSet();
            if (tableName == "ReportItemFull")
            {
                Log.Info("转换大写1");
                IDReportItemFull dalrtf = DalFactory<IDReportItemFull>.GetDal("ReportItemFull", ZhiFang.Common.Dictionary.DBSource.LisDB());
                ZhiFang.Model.ReportItemFull reportItemFull = new Model.ReportItemFull();
                try
                {
                    ds = dalrtf.GetColumns();
                    //ds = dalrtf.GetList(1, reportItemFull, "ITEMNO");
                }
                catch (Exception)
                {

                }

            }
            if (tableName == "ReportMarrowFull")
            {
                Log.Info("转换大写2");
                IDReportMarrowFull dalrtf = DalFactory<IDReportMarrowFull>.GetDal("ReportMarrowFull", ZhiFang.Common.Dictionary.DBSource.LisDB());
                ZhiFang.Model.ReportMarrowFull reportMarrowFull = new Model.ReportMarrowFull();
                try
                {
                    ds = dalrtf.GetColumns();
                    //ds = dalrtf.GetList(1, reportMarrowFull, "ITEMNO");
                }
                catch (Exception)
                {

                }

            }
            if (tableName == "ReportMicroFull")
            {
                Log.Info("转换大写3");
                IDReportMicroFull dalrtf = DalFactory<IDReportMicroFull>.GetDal("ReportMicroFull", ZhiFang.Common.Dictionary.DBSource.LisDB());
                ZhiFang.Model.ReportMicroFull reportMicroFull = new Model.ReportMicroFull();
                try
                {
                    ds = dalrtf.GetColumns();
                    //ds = dalrtf.GetList(1, reportMicroFull, "ITEMNO");
                }
                catch (Exception)
                {

                }
            }
            Log.Info("转换大写4");
            DataTable dtColumn = ds.Tables[0];
            //取当前数据库表的字典到哈希表
            Hashtable hashColumn = new Hashtable();
            for (int i = 0; i < dtColumn.Columns.Count; i++)
            {

                //字段名称:用大写
                string fieldName = dtColumn.Columns[i].ColumnName.ToUpper();
                if (hashColumn[fieldName] == null)
                    hashColumn.Add(fieldName, fieldName);
                msg += fieldName + ",";
            }
            Log.Info("转换大写5");
            msg += "成功！返回哈希表！！";
            Log.Info(msg);
            return hashColumn;
        }
        public static Hashtable getTableColumnFormList(string tableName)
        {
            string msg = "调用方法“getTableColumnFormList”从数据库里获取某个表的字段名称列表(转换成大写)!\r\n";
            Log.Info(msg);
            IDReportFormFull dalrtf = DalFactory<IDReportFormFull>.GetDal("ReportFormFull", ZhiFang.Common.Dictionary.DBSource.LisDB());
            ZhiFang.Model.ReportFormFull reportFromFull = new Model.ReportFormFull();
            //DataSet ds = dalrtf.GetList(reportFromFull);
            DataSet ds = dalrtf.GetColumns();
            DataTable dtColumn = ds.Tables[0];
            //取当前数据库表的字典到哈希表
            Hashtable hashColumn = new Hashtable();
            for (int i = 0; i < dtColumn.Columns.Count; i++)
            {
                //字段名称:用大写
                string fieldName = dtColumn.Columns[i].ColumnName.ToUpper();
                if (hashColumn[fieldName] == null)
                    hashColumn.Add(fieldName, fieldName);
                msg += fieldName + ",";
            }
            msg += "成功！返回哈希表！！";
            Log.Info(msg);
            return hashColumn;
        }
        /// <summary>
        /// 获取发布数据存放的磁盘目录
        /// 从web.config的配置项ReportConfigPath获取,如果没有该配置,则返回系统的启动目录
        /// 获取配置项后,在后面自动加上report\\作为目录返回
        /// </summary>
        /// <returns></returns>
        public static string getReportConfigPath()
        {
            string path = "";
            if (ConfigHelper.GetConfigString("ReportConfigPath") != null)
            {
                path = ConfigHelper.GetConfigString("ReportConfigPath");                
            }
            else
            {
                path = System.AppDomain.CurrentDomain.BaseDirectory;
            }
            if (ConfigHelper.GetConfigString("ReportFormFilesDir") != null)
            {
                path = path + "\\" + ConfigHelper.GetConfigString("ReportFormFilesDir");
            }
            path += "\\report\\";
            path = path.Replace("\\\\", "\\");
            return path;
        }

        public DataSet Getapply(string strWhere)
        {
            DataSet ds = new DataSet();
            return iapply.GetApply(strWhere);

        }
        public DataSet GetCentNo(string tableName, List<string> labNo, string SourceOrgID, string str)
        {
            DataSet ds = new DataSet();
            ds = idblb.GetCentNo(tableName, labNo, SourceOrgID, str);
            return ds;
        }

        public DataSet GetLabNo(string tableName, List<string> labCname, string SourceOrgID, string str)
        {
            DataSet ds = new DataSet();
            ds = idblb.GetLabNo(tableName, labCname, SourceOrgID, str);
            return ds;
        }

        public bool CheckLabNo(DataSet ds, string SourceOrgID, out string ReturnDescription)
        {
            ReturnDescription = "";
            try
            {
                string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
                foreach (string str in strArray)
                {
                    List<string> ListStr = new List<string>();
                    List<string> ListStrName = new List<string>();
                    string B_Lab_Columns = "";
                    string B_Lab_tableName = "";
                    string B_Lab_controlTableName = "";
                    bool result = false;
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
                        case "FOLKNO":
                            B_Lab_Columns = "FOLKNO";
                            B_Lab_tableName = "B_Lab_FolkType";
                            B_Lab_controlTableName = "B_FolkTypeControl";
                            break;
                        case "ITEMNO":
                            B_Lab_Columns = "ITEMNO";
                            B_Lab_tableName = "B_Lab_TestItem";
                            B_Lab_controlTableName = "B_TestItemControl";
                            break;
                        case "SUPERGROUPNO":
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
                        default:
                            Log.Info(string.Format("{0}:当前字段不能转码", str));
                            break;

                    }

                    Log.Info("检验xml中是否存在：" + str);
                    if (str.Trim()!="" &&ds.Tables[0].Columns.Contains(str))
                    {
                        Log.Info("存在：" + str);
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            Log.Info("检验xml中" + str + "是否有值?");
                            if (ds.Tables[0].Rows[i][str].ToString() != null && ds.Tables[0].Rows[i][str].ToString() != "")
                            {
                                Log.Info("xml：" + str + "有值." + ds.Tables[0].Rows[i][str].ToString() + "\\" + ds.Tables[0].Rows.Count);
                                ListStr.Add(ds.Tables[0].Rows[i][str].ToString());
                            }
                            else
                            {
                                Log.Info("xml：" + str + "没值.");
                                string str1 = "";
                                if (str.IndexOf('N') > -1)
                                {
                                    Log.Info("xml：" + str + "有N.");
                                    str1 = str.Substring(0, str.Length - 2);
                                }
                                if (ds.Tables[0].Columns.Contains(str1 + "Name"))
                                {
                                    Log.Info("判断xml中:" + str1 + "Name 是否有值.");
                                    if (ds.Tables[0].Rows[i][str1 + "Name"].ToString() != "")
                                    {
                                        Log.Info(str1 + "Name 有值.");
                                        ListStrName.Add(ds.Tables[0].Rows[i][str1 + "Name"].ToString());

                                        DataSet dsLabNo = GetLabNo(B_Lab_tableName, ListStrName, SourceOrgID, B_Lab_Columns);
                                        for (int j = 0; j < dsLabNo.Tables[0].Rows.Count; j++)
                                        {
                                            Log.Info("字典关系");
                                            if (B_Lab_tableName != "ITEMNO")
                                            {
                                                Log.Info("字典关系为ITEMNO表");
                                                ListStr.Add(dsLabNo.Tables[0].Rows[j]["lab" + B_Lab_Columns].ToString());
                                            }
                                            else
                                            {
                                                Log.Info("字典关系为其他表");
                                                ListStr.Add(dsLabNo.Tables[0].Rows[j][B_Lab_Columns].ToString());
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        Log.Info("已找到需要转换的项目。");
                        if (ListStr.Count > 0)
                        {
                            Log.Info("检测是否有项目.");
                            result = idblb.CheckCenterNo(B_Lab_controlTableName, ListStr, SourceOrgID, B_Lab_Columns);
                            if (!result)
                            {
                                Log.Info("没有找到项目，返回状态，result:" + result);
                                for (int j = 0; j < ListStr.Count; j++)
                                {
                                    Log.Info("遍历xml中的项目.");
                                    string labNo = ListStr[j].ToString();
                                    Log.Info("检测转码的字段：" + str + "编码为：" + labNo + "是否做过对照关系？");
                                    int count = idblb.GetTotalCount(B_Lab_tableName, SourceOrgID, labNo, B_Lab_Columns);
                                    Log.Info("检测完毕:共有数据" + count + "条。");
                                    if (count <= 0)
                                    {
                                        ReturnDescription += String.Format("实验室端的{0}={1}的编号未和中心端的对照\r\n", str, labNo);
                                    }
                                }
                                return result;
                            }
                        }

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("异常信息:" + ex.ToString());
                ReturnDescription += ex.ToString();
                return false;
            }
        }

        public DataSet GetLabControlNo(string tableName, List<string> CenterNO, string SourceOrgID, string str)
        {
            DataSet ds = new DataSet();
            ds = idblb.GetLabControlNo(tableName, CenterNO, SourceOrgID, str);
            return ds;
        }

        public bool CheckCenterNo(DataSet ds, string SourceOrgID, out string ReturnDescription)
        {
            ReturnDescription = "";
            try
            {
                string[] strArray = ConfigHelper.GetConfigString("TransCodField").Split(new char[] { ';' });
                foreach (string str in strArray)
                {
                    List<string> ListStr = new List<string>();
                    List<string> ListStrName = new List<string>();
                    string B_Lab_controlTableName = "";
                    bool result = false;
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
                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            if (ds.Tables[0].Rows[i][str].ToString() != null && ds.Tables[0].Rows[i][str].ToString() != "")
                            {
                                ListStr.Add(ds.Tables[0].Rows[i][str].ToString());
                            }
                        }
                        if (ListStr.Count > 0)
                        {
                            result = idblb.CheckCenterNo(B_Lab_controlTableName, ListStr, SourceOrgID, str);
                            if (!result)
                            {
                                for (int j = 0; j < ListStr.Count; j++)
                                {
                                    string centerNo = ListStr[j].ToString();
                                    int count = idblb.GetTotalCenterCount(B_Lab_controlTableName, SourceOrgID, centerNo, str);
                                    if (count <= 0)
                                    {
                                        ReturnDescription += String.Format("中心端的{0}={1}的编号未和实验室的对照\r\n", str, centerNo);
                                    }
                                }
                                return result;
                            }
                        }

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Debug("异常信息:" + ex.ToString());
                ReturnDescription += ex.ToString();
                return false;
            }
        }


        #region IBReportData 成员

        /// <summary>
        /// 返回小组编号
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="SourceOrgID"></param>
        /// <param name="LabNo"></param>
        /// <returns></returns>
        public string GetControl(string tableName, string SourceOrgID, string LabNo)
        {
            string str = "";
            str = idblb.GetControl(tableName, SourceOrgID, LabNo);
            return str;
        }
        /// <summary>
        /// 传入实验室号返回中心小组
        /// </summary>
        /// <param name="LabCode">实验室编号</param>
        /// <returns></returns>
        public DataSet GetPGroup(string LabCode)
        {
            DataSet ds = new DataSet();
            ds = idblb.GetPGroup(LabCode);
            return ds;
        }
        #endregion
    }
}
