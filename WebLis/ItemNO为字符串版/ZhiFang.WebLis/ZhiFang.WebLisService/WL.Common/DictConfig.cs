using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using System.Xml;
using System.IO;
using System.Collections;
using System.Text;

using ECDS.Common;

namespace ZhiFang.WebLisService.WL.Common
{
    /// <summary>
    /// 字典表配置类
    /// </summary>
    public class DictConfig
    {

        /// <summary>
        /// 获取系统字典表的配置文件名称:xml\TableFieldConfig.xml
        /// </summary>
        /// <returns></returns>
        public static string getDictTableXmlFileName()
        {
            string path = BaseFunction.getXmlConfigPath();
            path += "TableFieldConfig.xml";
            return path;
        }


        /// <summary>
        /// 获取要发布的系统字典表的存放路径,如:report\DictData\
        /// </summary>
        /// <returns></returns>
        public static string getDictDataSaveDiskPath()
        {
            string path = BaseFunction.getReportConfigPath();
            path += "DictData\\";
            return path;
        }


        /// <summary>
        /// 获取发布字典的版本信息XML文件名称
        /// </summary>
        /// <returns></returns>
        public static string getDictDataVersionXmlFileName()
        {
            //字典存放路径
            string dictPath = DictConfig.getDictDataSaveDiskPath();
            //文件名称
            return dictPath + "Lastest.xml";
        }


        /// <summary>
        /// 从字典表的配置文件中获取表的英文名称和中文名称,列名为(ID,Name)
        /// 如果配置文件不存在.返回null,并将错误信息写入Log
        /// </summary>
        /// <returns></returns>
        public static DataTable getDictTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Clear();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");

            string dictTableXmlFileName = DictConfig.getDictTableXmlFileName();
            if (System.IO.File.Exists(dictTableXmlFileName) == false)
            {
                Log.Error("没有找到字典表的配置文件:" + dictTableXmlFileName);
                return null;
            }
            else
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(dictTableXmlFileName);
                string xPath = "//Contrast";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                foreach (XmlNode xmlNode in nodelist)
                {
                    //表名称
                    string tableName = xmlNode.Attributes.GetNamedItem("tableName").Value;
                    //表描述
                    string tableDesc = xmlNode.Attributes.GetNamedItem("name").Value;
                    DataRow row = dt.NewRow();
                    row[0] = tableName;
                    row[1] = tableDesc;
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }



        /// <summary>
        /// 从字典表的配置文件中获取指定表的主键名称
        /// 如果配置文件不存在.返回"",并将错误信息写入Log
        /// 如果对于的表不存在,也返回""
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static string getDictTablePrimaryKey(string tableName)
        {
            string primaryKey = "";
            Hashtable hashAttr = DictConfig.getDictTableInfo(tableName);
            if (hashAttr["fieldKey"] != null)
            {
                primaryKey = hashAttr["fieldKey"].ToString();
            }
            return primaryKey;
        }



        /// <summary>
        /// 从字典表的配置文件中获取指定表的所有属性名称和内容
        /// 返回哈希表[属性名称,属性值]
        /// 如果配置文件不存在.返回空的哈希表,并将错误信息写入Log
        /// 如果对于的表不存在,也返回空的哈希表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static Hashtable getDictTableInfo(string tableName)
        {
            Hashtable hashAttr = new Hashtable();
            string dictTableXmlFileName = DictConfig.getDictTableXmlFileName();
            if (System.IO.File.Exists(dictTableXmlFileName) == false)
            {
                Log.Error("没有找到字典表的配置文件:" + dictTableXmlFileName);
                return hashAttr;
            }

            XmlDocument doc = new XmlDocument();
            doc.Load(dictTableXmlFileName);
            string xPath = "//Contrast[@tableName='" + tableName + "']";
            XmlNodeList nodelist = doc.SelectNodes(xPath);
            if (nodelist.Count > 0)
            {
                XmlNode xmlNode = nodelist[0];
                hashAttr = clsCommon.GetXMLData.getXmlNodeAttributes(xmlNode);
            }
            return hashAttr;
        }



        /// <summary>
        /// 生成一个表的XML数据
        /// 将内容里的字符<>"&进行转义,如<转义为"&lt;等
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="cname"></param>
        /// <param name="dtDictData"></param>
        /// <returns></returns>
        public static string makeDictDataToXML(string tableName, string cname, DataTable dtDictData)
        {
            string tableModal = "\t<Table tableName=\"{0}\" name =\"{1}\">\r\n";

            StringBuilder sbTableXML = new StringBuilder();
            //加表头
            sbTableXML.Append(string.Format(tableModal, tableName, cname));
            //加正文
            string rowBegin = "\t\t<RowData>\r\n";
            string rowContentModal = "\t\t\t<{0}>{1}</{0}>\r\n";
            string rowEnd = "\t\t</RowData>\r\n";
            for (int row = 0; row < dtDictData.Rows.Count; row++)
            {
                string rowContent = "";
                for (int col = 0; col < dtDictData.Columns.Count; col++)
                {
                    string fieldName = dtDictData.Columns[col].ToString().Trim();
                    if (fieldName == "")
                        continue;
                    string fieldValue = dtDictData.Rows[row][col].ToString().Trim();
                    //将内容转义
                    fieldValue = fieldValue.Replace("&", "&amp;");
                    fieldValue = fieldValue.Replace("\"", "&quot;");
                    fieldValue = fieldValue.Replace(">", "&gt;");
                    fieldValue = fieldValue.Replace("<", "&lt;");
                    rowContent += string.Format(rowContentModal, fieldName, fieldValue);
                }
                if (rowContent != "")
                {
                    sbTableXML.Append(rowBegin);
                    sbTableXML.Append(rowContent);
                    sbTableXML.Append(rowEnd);
                }
            }
            //加表尾
            sbTableXML.Append("\t</Table>\r\n");
            return sbTableXML.ToString();
        }


        /// <summary>
        /// 保存发布的字典数据到本地文件
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static bool saveDictDataXML(string xml)
        {
            bool isOK = false;
            try
            {
                //字典存放路径
                string dictPath = DictConfig.getDictDataSaveDiskPath();
                //加日期做分类目录
                string path = dictPath + System.DateTime.Now.ToShortDateString() + "\\";
                //如果目录不存在则创建
                if (System.IO.Directory.Exists(path) == false)
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                //文件名称
                string fileName = "DictData_" + System.DateTime.Now.ToLongTimeString().Replace(":", "-") + ".xml";
                string fullFileName = path + fileName;
                //保存到本地文件
                clsCommon.Tools.writeStringToLocalFile(fullFileName, xml);
                //记录最后发布的版本信息
                DictConfig.saveDictDataVersionInfo(path, fileName);
                //写日志
                string log = "发布字典数据,文件为:" + path;
                Log.Info(log);
                isOK = true;
            }
            catch (System.Exception ex)
            {
                string errMsg = "保存发布字典XML数据到本地文件出错:\r\n" + ex.Message;
                Log.Error(errMsg);
            }
            return isOK;
        }




        /// <summary>
        /// 保存发布字典的版本信息到XML文件
        /// </summary>
        /// <param name="dictDataFileName"></param>
        /// <returns></returns>
        public static string saveDictDataVersionInfo(string path, string fileName)
        {
            string versionXmlFileName = "";
            try
            {
                string rowContentModal = "\t<{0}>{1}</{0}>\r\n";
                StringBuilder sbXML = new StringBuilder();
                //加头
                sbXML.Append("<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n");
                sbXML.Append("<root>\r\n");
                //绝对目录
                sbXML.Append(string.Format(rowContentModal, "Path", path));
                //相对目录
                string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
                string relativePath = path.Replace(basePath, "");
                sbXML.Append(string.Format(rowContentModal, "RelativePath", relativePath));
                //文件名称
                sbXML.Append(string.Format(rowContentModal, "FileName", fileName));
                //完整的文件名称(带路径)
                sbXML.Append(string.Format(rowContentModal, "FullFileName", path + fileName));
                sbXML.Append("</root>\r\n");
                //保存到本地文件
                string xml = sbXML.ToString();
                versionXmlFileName = DictConfig.getDictDataVersionXmlFileName();
                clsCommon.Tools.writeStringToLocalFile(versionXmlFileName, xml);
            }
            catch (System.Exception ex)
            {
                string errMsg = "保存发布字典版本信息到XML文件出错:\r\n" + ex.Message;
                Log.Error(errMsg);
            }
            return versionXmlFileName;
        }



        /// <summary>
        /// 获取最后一次发布的字典XML文件
        /// 如果没有发布过,返回""
        /// </summary>
        /// <returns></returns>
        public static string getDictDataLatestFileName()
        {
            string fileName = "";
            try
            {
                //版本XML文件名称
                string versionXmlFileName = DictConfig.getDictDataVersionXmlFileName();
                if (System.IO.File.Exists(versionXmlFileName))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(versionXmlFileName);
                    string xPath = "//FullFileName";
                    XmlNodeList nodelist = doc.SelectNodes(xPath);
                    int count = nodelist.Count;
                    if (count > 0)
                    {
                        //一般应该等于1,只取最后一个
                        XmlNode xmlNode = nodelist[count - 1];
                        fileName = xmlNode.InnerXml;
                    }
                }
            }
            catch (System.Exception ex)
            {
                string errMsg = "获取发布字典版本信息XML文件出错:\r\n" + ex.Message;
                Log.Error(errMsg);
            }
            return fileName;
        }




        /// <summary>
        /// 从本地文件获取发布的字典数据XML
        /// WEBLISTools\report\DictData下
        /// </summary>
        /// <param name="fileName">本地文件名称</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static string getDictDataFileContentXML(string fileName, out string errorMsg)
        {
            errorMsg = "";
            string xml = "";
            try
            {
               if (System.IO.File.Exists(fileName))
                {
                    xml = clsCommon.Tools.readFromLocalFile(fileName);
                }
            }
            catch (System.Exception ex)
            {
                errorMsg = "从文件“" + fileName + "”获取字典数据的XML内容出错！\r\n" + ex.Message;
                Log.Error(errorMsg);
            }
            return xml;
        }



        /// <summary>
        /// 获取最后一次发布的字典数据的XML内容
        /// 如D:\WebLis\WEBLISTools\WEBLISTools\report\DictData\2009-7-31\DictData_16-29-57.xml
        /// </summary>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static string getDictDataLatestContentXML(out string errorMsg)
        {
            errorMsg = "";
            string fileName = DictConfig.getDictDataLatestFileName();
            return DictConfig.getDictDataFileContentXML(fileName, out errorMsg);
        }



        /// <summary>
        /// 将上传的数据字典保存到数据库中
        /// </summary>
        /// <param name="xmlData">数据字典XML数组</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        public static int UpLoadDictDataFromBytes(byte[] xmlData, out string errorMsg)
        {
            errorMsg = "";
            return 0;
        }




    }
}
