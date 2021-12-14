using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using ECDS.Common;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using WSInvoke;
using System.Xml;

namespace ZhiFang.WebLisService.Agent
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

        public string wsfAddr = LIS.CacheConfig.Util.Readcfg.ReadINIConfig("DownReportAddr").ToString();

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


            object[] args = new object[8];
            args[0] = SourceOrgID;
            args[1] = DestiOrgID;
            args[2] = BarCodeNo;
            args[3] = nodeReportFormItem;
            args[4] = FileReport;
            args[5] = FileType;
            args[6] = xmlWebLisOthers;
            args[7] = ReturnDescription;

            Log.Info(String.Format("连接远程服务{0}", wsfAddr));
            string rs = (string)WebServiceHelper.InvokeWebService(wsfAddr, "DownloadReportForDelphi", args);

            return false;
        }



        [WebMethod(Description = "下载报告")]
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

            object[] args = new object[8];
            args[0] = SourceOrgID;
            args[1] = DestiOrgID;
            args[2] = BarCodeNo;
            args[3] = nodeReportFormItem;
            args[4] = FileReport;
            args[5] = FileType;
            args[6] = xmlWebLisOthers;
            args[7] = ReturnDescription;

            Log.Info(String.Format("连接远程服务{0}", wsfAddr));
            string rs = (string)WebServiceHelper.InvokeWebService(wsfAddr, "DownloadReport", args);

            return true;
        }


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

            object[] args = new object[6];
            args[0] = SourceOrgID;
            args[1] = DestiOrgID;
            args[2] = BarCodeNo;
            args[3] = xmlOthersWhereClause;
            args[4] = xmlWebLisOthers;
            args[5] = ReturnDescription;
 

            Log.Info(String.Format("连接远程服务{0}", wsfAddr));
            string rs = (string)WebServiceHelper.InvokeWebService(wsfAddr, "QueryReportStatus", args);

            return true;
        }
    }
}
