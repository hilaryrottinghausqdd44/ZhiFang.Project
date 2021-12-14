using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using ZhiFang.Common.Log;
using System.Configuration;
using WSInvoke;

namespace ZhiFang.WebLisService
{
    /// <summary>
    /// AgentRequestFormWebService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消对下行的注释。
    // [System.Web.Script.Services.ScriptService]
    public class AgentRequestFormWebService : System.Web.Services.WebService
    {
        public string AgentRequestDown = ConfigurationManager.AppSettings["AgentRequestDown"];
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
            object[] args = new object[8];
            args[0] = DestiOrgID;
            args[1] = BarCodeNo;
            args[2] = WebLiser;
            args[3] = nodeBarCode;
            args[4] = nodeNRequestItem;
            args[5] = nodeNRequestForm;
            args[6] = xmlWebLisOthers;
            args[7] = ReturnDescription;
            Log.Info(String.Format("连接远程服务{0}", AgentRequestDown));
            bool rs = (bool)WebServiceHelper.InvokeWebService(AgentRequestDown, "DownloadBarCode", args);
            nodeBarCode = (XmlNode)args[3];
            nodeNRequestItem = (XmlNode)args[4];
            nodeNRequestForm = (XmlNode)args[5];
            xmlWebLisOthers = Convert.ToString(args[6]);
            ReturnDescription = Convert.ToString(args[7]);
            return rs;
        }


        [WebMethod(Description = "样本签收标志")]
        public bool DownloadBarCodeFlag(
           string DestiOrgID,              //外送(至)单位(独立实验室编号)
           string BarCodeNo,               //条码码
           XmlNode WebLiser,               //操作人的更多信息
           out string ReturnDescription)   //其他描述
        {
            ReturnDescription = "";
            object[] args = new object[4];
            args[0] = DestiOrgID;
            args[1] = BarCodeNo;
            args[2] = WebLiser;
            args[3] = ReturnDescription;
            bool rs = (bool)WebServiceHelper.InvokeWebService(AgentRequestDown, "DownloadBarCodeFlag", args);
            ReturnDescription = Convert.ToString(args[3]);
            return rs;
        }


        [WebMethod(Description = "取消下载")]
        public bool DownloadBarCodeCancel(
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            XmlNode WebLiser,               //操作人的更多信息
            out string ReturnDescription)   //其他描述
        {
            ReturnDescription = "";
            object[] args = new object[4];
            args[0] = DestiOrgID;
            args[1] = BarCodeNo;
            args[2] = WebLiser;
            args[3] = ReturnDescription;
            bool rs = (bool)WebServiceHelper.InvokeWebService(AgentRequestDown, "DownloadBarCodeCancel", args);
            ReturnDescription = Convert.ToString(args[3]);
            return rs;
        }


        [WebMethod(Description = "样本退回")]
        public bool RefuseDownloadBarCode(
            string DestiOrgID,              //外送(至)单位(独立实验室编号)
            string BarCodeNo,               //条码码
            XmlNode WebLiser,               //操作人的更多信息
            out string ReturnDescription)   //其他描述
        {
            ReturnDescription = "";
            object[] args = new object[4];
            args[0] = DestiOrgID;
            args[1] = BarCodeNo;
            args[2] = WebLiser;
            args[3] = ReturnDescription;
            bool rs = (bool)WebServiceHelper.InvokeWebService(AgentRequestDown, "RefuseDownloadBarCode", args);
            ReturnDescription = Convert.ToString(args[3]);
            return rs;
        }
    }
}
