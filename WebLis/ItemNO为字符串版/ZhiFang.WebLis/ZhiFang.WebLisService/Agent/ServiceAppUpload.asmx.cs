using System;
using System.Collections;
using System.ComponentModel;
using System.Data;

using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using WSInvoke;
using System.IO;
using ECDS.Common;
using System.Xml;
using System.Data.SqlClient;

namespace ZhiFang.WebLisService.Agent
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
        public string wsfAddr = LIS.CacheConfig.Util.Readcfg.ReadINIConfig("UpLoadRequestAddr").ToString();


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

            object[] args = new object[8];
            args[0] = SourceOrgID;
            args[1] = DestiOrgID;
            args[2] = BarCodeNo;
            args[3] = WebLisFlagSource;
            args[4] = xmlOthersWhereClause;
            args[5] = WebLisFlag;
            args[6] = xmlWebLisOthers;
            args[7] = ReturnDescription;

            Log.Info(String.Format("连接远程服务{0}", wsfAddr));
            bool rs = (bool)WebServiceHelper.InvokeWebService(wsfAddr, "WebLisQueryStatus", args);

            return rs;
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

            WebLisFlag = "0";
            ReturnDescription = "";

            object[] args = new object[9];
            args[0] = SourceOrgID;
            args[1] = DestiOrgID;
            args[2] = BarCodeNo;
            args[3] = nodeBarCodeForm;
            args[4] = nodeNRequestForm;
            args[5] = nodeNRequestItem;
            args[6] = nodeOthers;
            args[7] = WebLisFlag;
            args[8] = ReturnDescription;

            Log.Info(String.Format("连接远程服务{0}", wsfAddr));
            bool rs = (bool)WebServiceHelper.InvokeWebService(wsfAddr, "WebLisUpgradeRequisitionsForDelphi", args);

            return rs;
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

            object[] args = new object[8];
            args[0] = SourceOrgID;
            args[1] = DestiOrgID;
            args[2] = BarCodeNo;
            args[3] = WebLisFlagSource;
            args[4] = WebLisFlagDestination;
            args[5] = WebLisOthers;
            args[6] = WebLisFlag;
            args[7] = ReturnDescription;

            Log.Info(String.Format("连接远程服务{0}", wsfAddr));
            bool rs = (bool)WebServiceHelper.InvokeWebService(wsfAddr, "WebLisUpgradeStatus", args);

            return rs;
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

            object[] args = new object[5];
            args[0] = SourceOrgID;
            args[1] = DestiOrgID;
            args[2] = BarCodeNo;
            args[3] = xmlWebLisOthers;
            args[4] = ReturnDescription;

            Log.Info(String.Format("连接远程服务{0}", wsfAddr));
            bool rs = (bool)WebServiceHelper.InvokeWebService(wsfAddr, "WebLisRequestCancel", args);

            return rs;
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

            object[] args = new object[8];
            args[0] = SourceOrgID;
            args[1] = DestiOrgID;
            args[2] = BarCodeNo;
            args[3] = nodeBarCodeForm;
            args[4] = nodeNRequestForm;
            args[5] = nodeNRequestItem;
            args[6] = nodeOthers;
            args[7] = WebLisFlag;
            args[8] = ReturnDescription;

            Log.Info(String.Format("连接远程服务{0}", wsfAddr));
            bool rs = (bool)WebServiceHelper.InvokeWebService(wsfAddr, "WebLisUpgradeRequisitions", args);
            return rs;

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
                    bRet = UpdateXmlInfoToDB1(nodeSource, TableEName, bAddOrUpgrade, ref descXmlToDB, nodeOthers, sql, "BarCode");
                    break;
                case "NRequestForm":
                    sql = "select * from " + TableEName;
                    sql += " where NRequestFormNo in (select NRequestFormNo from NRequestItem where BarCodeFormNo in (select BarCodeFormNo from BarCodeForm where BarCode='"
                    + BarCodeNo + "')" + " and WebLisSourceOrgID='" + SourceOrgID + "') and WebLisSourceOrgID='" + SourceOrgID + "'";
                    bRet = UpdateXmlInfoToDB1(nodeSource, TableEName, bAddOrUpgrade, ref descXmlToDB, nodeOthers, sql, "NRequestFormNo");
                    break;
                case "NRequestItem":
                    sql = "select * from " + TableEName;
                    sql += " where BarCodeFormNo in (select BarCodeFormNo from BarCodeForm where BarCode='"
                    + BarCodeNo + "')" + " and WebLisSourceOrgID='" + SourceOrgID + "'";
                    bRet = UpdateXmlInfoToDB1(nodeSource, TableEName, bAddOrUpgrade, ref descXmlToDB, nodeOthers, sql, "NRequestItemNo");
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
