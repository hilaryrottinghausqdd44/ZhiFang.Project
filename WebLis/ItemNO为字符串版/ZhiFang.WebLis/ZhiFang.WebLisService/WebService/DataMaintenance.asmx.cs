using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Xml;

using ZhiFang.WebLisService.WL.Common;

namespace ZhiFang.WebLisService.WebService
{
    /// <summary>
    ///  主要负责基础数据的维护
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class DataMaintenance : System.Web.Services.WebService
    {


        /// <summary>
        /// 将上传的数据字典保存到数据库中
        /// </summary>
        /// <param name="xmlData">数据字典XML数组</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        [WebMethod]
        public int UpLoadDictDataFromBytes(string token, byte[] xmlData, out string errorMsg)
        {
            errorMsg = "";
            return WL.Common.DictConfig.UpLoadDictDataFromBytes(xmlData, out errorMsg);
        }


        /// <summary>
        /// 保存发布的字典内容ＸＭＬ文件
        /// </summary>
        /// <param name="xml"></param>
        [WebMethod]
        public void saveXmlFileContent(string token, string xml)
        {
            string versionXmlFileName = System.AppDomain.CurrentDomain.BaseDirectory + "report\\DictData\\Lastest.xml";
            if (System.IO.File.Exists(versionXmlFileName))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(versionXmlFileName);
                string xPath = "/root";
                XmlNodeList nodelist = doc.SelectNodes(xPath);
                int count = nodelist.Count;
                if (count > 0)
                {
                    //一般应该等于1,只取最后一个
                    XmlNode xmlNode = nodelist[count - 1];
                    Hashtable hashAttr = clsCommon.GetXMLData.getXmlNodeNameAndValue(xmlNode);
                    string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
                    string relativePath = "";
                    if (hashAttr["RelativePath"] != null)
                    {
                        relativePath = hashAttr["RelativePath"].ToString();
                    }
                    string fileName = "";
                    if (hashAttr["FileName"] != null)
                    {
                        fileName = hashAttr["FileName"].ToString();
                    }
                    if (fileName != "")
                    {
                        string fullFileName = basePath + relativePath + fileName;
                        clsCommon.Tools.writeStringToLocalFile(fullFileName, xml);
                    }
                }
            }
        }



        /// <summary>
        /// 保存版本信息ＸＭＬ文件
        /// </summary>
        /// <param name="xml"></param>
        [WebMethod]
        public void saveXmlFileVersion(string token, string xml)
        {
            string versionXmlFileName = System.AppDomain.CurrentDomain.BaseDirectory + "report\\DictData\\Lastest.xml";
            clsCommon.Tools.writeStringToLocalFile(versionXmlFileName, xml);

        }


        /// <summary>
        /// 获取最后一次发布的字典数据的XML内容
        /// </summary>
        /// <param name="errorMsg">错误信息</param>
        /// <returns></returns>
        [WebMethod]
        public string getDictDataLatestContentXML(string token, out string errorMsg)
        {
            errorMsg = "";
            return DictConfig.getDictDataLatestContentXML(out errorMsg);
        }



        [WebMethod]
        public string downLoadXmlFileContent(string token, string fileName)
        {
            return "A";
        }

        [WebMethod]
        public string downLoadXmlFileVersion(string token)
        {
            return "B";
        }



    }
}
