using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data;
using System.Collections;
using ZhiFang.IBLL.Report;
using ZhiFang.BLLFactory;
using System.Security.AccessControl;
using ECDS.Common;

namespace ZhiFang.WebLisService.WL.Common
{
    public class RequestData
    {
        /// <summary>
        /// 取主键的查询表达式,即where条件
        /// </summary>
        /// <param name="hashForm"></param>
        /// <returns></returns>
        public static string getRequestDataPkWhere(Hashtable hashForm)
        {
            string fieldWhereModal = "\"{0}\"='{1}'";
            string pkWhere = "";
            if (pkWhere != "")
                pkWhere += " AND ";
            pkWhere += string.Format(fieldWhereModal, "NRequestFormNo", hashForm["NRequestFormNo".ToUpper()].ToString());
            return pkWhere;
        }
        /// <summary>
        /// 申请单上传（将上传的申请单保存到数据库中）
        /// </summary>
        /// <param name="xmlData">xml数据</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>0 成功； -1 失败</returns>
        public static int UpLoadRequestFromBytes(byte[] xmlData, out string errorMsg)
        {
            string msg = "调用方法“UpLoadRequestFromBytes”将上传的申请单保存报告到数据库!\r\n";
            ZhiFang.Common.Log.Log.Info(msg);
            int result = -1;
            errorMsg = "";
            try
            {
                //将字节数组转换成字符串
                string xml = ZhiFang.WebLisService.clsCommon.ConvertData.convertBytesToString(xmlData);
                if ((xml == null) || (xml == ""))
                {
                    errorMsg = "xmlData没有内容，不能上传申请单!\r\n";
                    ZhiFang.Common.Log.Log.Info(errorMsg);
                    return result;
                }
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                string xPath = "//ds";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                XmlNode reportFormNodeSave = nodelist[0];
                string tableNameBarCode = "BarCodeForm";
                //第一级是保存到NRequestForm
                string tableNameForm = "NRequestForm";
                //第二级是保存到NRequestItem
                string tableNameItem = "NRequestItem";
                string tableNameItemXMLData = "NRequestItem";
                //表barcodeForm中的字段列表
                Hashtable hashBarCode = WL.Common.ReportData.getTableColumnNameList(tableNameBarCode);
                //表NrequestForm中的字段列表
                Hashtable hashFormColumn = WL.Common.ReportData.getTableColumnNameList(tableNameForm);
                //表NrequestItem中的字段列表
                Hashtable hashItemColumn = WL.Common.ReportData.getTableColumnNameList(tableNameItem);
                //取主表的字段名称和内容
                Hashtable hashForm = ZhiFang.WebLisService.clsCommon.GetXMLData.getXmlNodeNameAndValue(reportFormNodeSave);
                //转换字段名称为大写
                hashForm = clsCommon.ConvertData.convertHashKeyToUpper(hashForm);
                string pkWhere = "";
                if (hashForm.Contains("NREQUESTFORMNO"))
                {
                    //取XML数据表对应的主键(唯一索引)
                    pkWhere = RequestData.getRequestDataPkWhere(hashForm);
                }
                else
                {
                    pkWhere = DateTime.Now.Ticks.ToString();
                }
                string NRequestFormNo = pkWhere;
                DAL.MsSql.Weblis.NRequestItem NRequestItem = new DAL.MsSql.Weblis.NRequestItem();
                DAL.MsSql.Weblis.NRequestForm NRequestForm = new DAL.MsSql.Weblis.NRequestForm();
                //先删除以前的数据
                bool count = NRequestItem.DeleteList(NRequestFormNo);
                msg = "运行SQL语句删除子表（项目表）：\r\n" ;
                ZhiFang.Common.Log.Log.Info(msg);
                if (count)
                {
                    //再删除主表
                    msg = "运行SQL语句删除主表（报告表）：\r\n";
                    ZhiFang.Common.Log.Log.Info(msg);
                    int i = NRequestForm.Delete(Convert.ToInt32(NRequestFormNo));
                }
                //插入新数据到数据库中
                string sqlForm = Common.Tools.getInsertSQL(tableNameBarCode, hashBarCode, hashForm);
              
                if (sqlForm != "")
                {
                    msg = "运行SQL语句插入主表数据（BarCodeForm表）：\r\n" + sqlForm;
                    ZhiFang.Common.Log.Log.Info(msg);

                }
                if (hashForm.Contains("SampleTypeNo"))
                {

                }
                //先生成插入主表的SQL脚本
                sqlForm = Common.Tools.getInsertSQL(tableNameForm, hashFormColumn, hashForm);
                if (sqlForm != "")
                {
                    msg = "运行SQL语句插入主表数据（报告表）：\r\n" + sqlForm;
                    ZhiFang.Common.Log.Log.Info(msg);

                    //插入主表数据
                  // CreateDB().ExecuteNonQuery(sqlForm);
                }
                xPath = "//" + tableNameItemXMLData;
                nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode xmlNode in nodelist)
                {
                    //取主表的字段名称和内容
                    Hashtable hashItem = ZhiFang.WebLisService.clsCommon.GetXMLData.getXmlNodeNameAndValue(xmlNode);
                    //先生成插入子表的SQL脚本
                    string sqlItem = Common.Tools.getInsertSQL(tableNameItem, hashItemColumn, hashItem);
                    if (sqlItem != "")
                    {
                        msg = "运行SQL语句插入子表数据（项目表）：\r\n" + sqlItem;
                        ZhiFang.Common.Log.Log.Info(msg);
                        //插入子表数据
                        //WL.BLL.DataConn.CreateDB().ExecuteNonQuery(sqlItem);
                        //DBUtility.DBFactory.CreateDB(dbsourceconn);
                    }
                }
                msg = "成功！";
                ZhiFang.Common.Log.Log.Info(msg);

                //成功
                result = 0;
            }
            catch (System.Exception ex)
            {
                errorMsg = "上传申请单失败:将数据保存到数据库是失败:\r\n" + ex.Message;
                ZhiFang.Common.Log.Log.Info(msg);
            }
            return result;
        }
        /// <summary>
        /// 根据医院分类和查询条件得出数据集
        /// </summary>
        /// <param name="OrgName">医院分类</param>
        /// <param name="WhereClause">查询条件</param>
        /// <returns>xml转换成string返回数据集结果</returns>
        public static string DownloadRequestFormList(string OrgName, string WhereClause, out string errorMsg)
        {
            errorMsg = "";
            return "";
        }
        /// <summary>
        /// 根据查询条件获取检验申请单NRequestForm和NRequestItem列表
        /// <param name="whereSQL">查询条件，如果是空串，则返回所有的数据，尽量不要传空串</param>
        /// <returns>XML</returns>
        public static string DownloadRequestFormItemList(string whereSQL, out string errorMsg) 
        {
            string msg = "调用方法“DownloadRequestFormItemList”根据查询条件获取检验申请单NRequestForm和NRequestItem列表!\r\n";
            ZhiFang.Common.Log.Log.Info(msg);
            errorMsg = "";
            if (whereSQL != "")
                whereSQL = " where " + whereSQL;
            DAL.MsSql.Weblis.NRequestForm NRequestForm = new DAL.MsSql.Weblis.NRequestForm();
            DataSet ds = NRequestForm.GetList(whereSQL);
            msg = "生成XML数据！";
            ZhiFang.Common.Log.Log.Info(msg);
            //加入XML的声明段落
            XmlDocument doc = new XmlDocument();
            XmlDeclaration declarationDoc = doc.CreateXmlDeclaration("1.0", "utf-8", "");
            doc.AppendChild(declarationDoc);
            //遍历检验报告单，获取报告单的项目表名称，获取项目表内容,生产XML
            DataTable dt = ds.Tables[0];
            //创建一个根元素
            XmlElement elementRoot = doc.CreateElement("WebRequestFile");
            for (int rowCount = 0; rowCount < dt.Rows.Count; rowCount++)
            {
                //创建表结点
                XmlNode tableNode = doc.CreateNode(XmlNodeType.Element, "NRequestForm", "");
                string reportFormID = "";
                string tableNameItem = "NRequestItem";
                for (int colCount = 0; colCount < dt.Columns.Count; colCount++)
                {
                    string fieldName = dt.Columns[colCount].ToString().Trim();
                    string fieldValue = dt.Rows[rowCount][colCount].ToString().Trim();
                    //创建字段结点
                    XmlNode fieldNode = doc.CreateNode(XmlNodeType.Element, fieldName, "");
                    fieldNode.InnerXml = fieldValue;
                    tableNode.AppendChild(fieldNode);
                    if (fieldName.ToUpper() == "NRequestFormNo".ToUpper())
                    {
                        reportFormID = fieldValue;
                    }
                }
                //取子表内容
                if ((reportFormID != ""))
                {
                    DAL.MsSql.Weblis.NRequestItem NRequestItem = new DAL.MsSql.Weblis.NRequestItem();
                    Model.NRequestItem model = new Model.NRequestItem();
                    msg = "运行SQL语句查询检验申请单的项目：\r\n" ;
                    ZhiFang.Common.Log.Log.Info(msg);
                    model.NRequestFormNo = Convert.ToInt32(reportFormID);
                    DataSet dsChild = NRequestItem.GetList(model);
                    DataTable dtChild = dsChild.Tables[0];
                    for (int rowCountChild = 0; rowCountChild < dtChild.Rows.Count; rowCountChild++)
                    {
                        //创建表结点
                        XmlNode tableNodeChild = doc.CreateNode(XmlNodeType.Element, tableNameItem, "");
                        for (int colCountChild = 0; colCountChild < dtChild.Columns.Count; colCountChild++)
                        {
                            string fieldNameChild = dtChild.Columns[colCountChild].ToString().Trim();
                            string fieldValueChild = dtChild.Rows[rowCountChild][colCountChild].ToString().Trim();
                            //创建字段结点
                            XmlNode fieldNodeChild = doc.CreateNode(XmlNodeType.Element, fieldNameChild, "");
                            fieldNodeChild.InnerXml = fieldValueChild;
                            tableNodeChild.AppendChild(fieldNodeChild);
                        }
                        tableNode.AppendChild(tableNodeChild);
                    }
                }
                //将表节点加到根节点
                elementRoot.AppendChild(tableNode);
            }
            //加根节点到文档
            doc.AppendChild(elementRoot);
            msg = "成功！返回XML！！";
           ZhiFang.Common.Log.Log.Info(msg);

            //doc.Save(@"D:\WebLis\WEBLIS2009\WEBLIS\Report\21.XML");
            return doc.OuterXml;
        }


    }
}