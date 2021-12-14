using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using ZhiFang.Common.Public;

namespace ZhiFang.WebLisService
{
    /// <summary>
    /// WebLisProxyService 的摘要说明
    /// </summary>
    [WebService(Namespace = "ZhiFang.WebLisService")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebLisProxyService : System.Web.Services.WebService
    {
        WeblisService.ReportFormWebService.ReportFormWebService WebLisReportProxy = new WeblisService.ReportFormWebService.ReportFormWebService();
        ZhiFang.WebLisService.WeblisService.RequestFormWebService.RequestFormWebService RequestFormWebServiceProxy = new WeblisService.RequestFormWebService.RequestFormWebService();

        [WebMethod(Description = "上传申请_生成新条码")]
        public bool AppliyUpLoad_CreatNewBarCode(string xmlData, string orgID, string jzType, out string sMsg)
        {
            sMsg = "";
            ZhiFang.Common.Log.Log.Info("WebLisProxyService->AppliyUpload：传入的字符串:" + xmlData);
            bool result = RequestFormWebServiceProxy.AppliyUpLoad_CreatNewBarCode(xmlData, orgID, jzType,out  sMsg);
            ZhiFang.Common.Log.Log.Info("WebLisProxyService->AppliyUpload：resutl:" + result.ToString() + "@sMsg:" + sMsg);
            return result;
        }
        [WebMethod(Description = "下载报告单ID列表ByClientBarcodeNo")]
        public bool DownloadReportFormIDListByClientBarcodeNo(string Account, string PassWord, string BarcodeNo, string ClientNo, string DestiOrgID, out string[] ReportFormIDList, out string Error)
        {
            //ReportFormIDList=
            Error = "";
            ZhiFang.Common.Log.Log.Info("WebLisProxyService->DownloadReportFormIDListByClientBarcodeNo：传入的字符串:Account:" + Account+ ",PassWord:"+ PassWord + ",BarcodeNo:" + BarcodeNo + ",ClientNo:" + ClientNo + ",DestiOrgID:" + DestiOrgID);
            //PassWord = Base64Help.EncodingString(PassWord);
            //ZhiFang.Common.Log.Log.Info("WebLisProxyService->DownloadReportFormIDListByClientBarcodeNo：Base64Help.EncodingString(PassWord):" + PassWord );
            bool result = WebLisReportProxy.DownloadReportFormIDListByClientBarcodeNo(Account, PassWord, BarcodeNo, ClientNo,DestiOrgID, out ReportFormIDList,out Error);
            ZhiFang.Common.Log.Log.Info("WebLisProxyService->DownloadReportFormIDListByClientBarcodeNo：resutl:" + result.ToString() + "@Error:" + Error);
            return result;
        }

        [WebMethod(Description = "下载报告单")]
        public bool DownloadReportByReportFormID(string Account, string PassWord, string ReportFormID, string ClientNo, string DestiOrgID, out string WebReportXML, out string Error)
        {
            Error = "";
            ZhiFang.Common.Log.Log.Info("WebLisProxyService->DownloadReportByReportFormID：传入的字符串:Account:" + Account + ",PassWord:" + PassWord + ",ReportFormID:" + ReportFormID + ",ClientNo:" + ClientNo + ",DestiOrgID:" + DestiOrgID);
            //PassWord = Base64Help.EncodingString(PassWord);
            //ZhiFang.Common.Log.Log.Info("WebLisProxyService->DownloadReportFormIDListByClientBarcodeNo：Base64Help.EncodingString(PassWord):" + PassWord);
            bool result = WebLisReportProxy.DownloadReportByReportFormID(Account, PassWord, ReportFormID, ClientNo, DestiOrgID,out WebReportXML, out Error);
            ZhiFang.Common.Log.Log.Info("WebLisProxyService->DownloadReportByReportFormID：resutl:" + result.ToString() + "@sMsg:" + Error);
            return result;
        }

        [WebMethod(Description = "下载PDF报告单")]
        public bool DownLoadReportFormPDFByAccountPassWord(string Account, string PassWord, string ClientNo, out byte[] PDFDataCenter, out byte[] PDFDataLab, out string Error, string ReportFormID)
        {
            Error = "";
            PDFDataCenter = null;
            PDFDataLab = null;
            ZhiFang.Common.Log.Log.Info("WebLisProxyService->DownLoadReportFormPDFByAccountPassWord：传入的字符串:Account:" + Account + ",PassWord:" + PassWord + ",ReportFormID:" + ReportFormID + ",ClientNo:" + ClientNo );
            bool result = WebLisReportProxy.DownLoadReportFormPDFByAccountPassWord(Account, PassWord, ClientNo, ReportFormID,out PDFDataCenter,out  PDFDataLab, out Error );
            ZhiFang.Common.Log.Log.Info("WebLisProxyService->DownLoadReportFormPDFByAccountPassWord：resutl:" + result.ToString() + "@sMsg:" + Error);
            return result;
        }

        [WebMethod(Description = "下载申请")]
        public bool DownloadBarCode(string DestiOrgID, string BarCodeNo, System.Xml.XmlNode WebLiser, out System.Xml.XmlNode nodeBarCode, out System.Xml.XmlNode nodeNRequestItem, out System.Xml.XmlNode nodeNRequestForm, out string xmlWebLisOthers, out string ReturnDescription)
        {
            SampleSwapInterface.A aa = new SampleSwapInterface.A();
            ZhiFang.Common.Log.Log.Info("WebLisProxyService->DownloadBarCode：传入的DestiOrgID:" + DestiOrgID + ";BarCodeNo:" + BarCodeNo);
            bool result = aa.DownloadBarCode(DestiOrgID, BarCodeNo, WebLiser, out nodeBarCode, out nodeNRequestItem, out nodeNRequestForm, out xmlWebLisOthers, out ReturnDescription);
            ZhiFang.Common.Log.Log.Info("WebLisProxyService->DownloadBarCode：传入的DestiOrgID:" + DestiOrgID + ";BarCodeNo:" + BarCodeNo + "@ReturnDescription:" + ReturnDescription);
            return result;
        }

        [WebMethod(Description = "上传报告")]
        public int UpLoadReportFromBytes(string token,byte[] xmlData, byte[] pdfdata, byte[] pdfdata_td, byte[] fileData, string fileType, out string errorMsg)
        {
            System.Text.UTF8Encoding converter = new UTF8Encoding();
            string xml = converter.GetString(xmlData);
            WebLisReport.WebLisReport wlr = new WebLisReport.WebLisReport();
            ZhiFang.Common.Log.Log.Info("WebLisProxyService->UpLoadReportFromBytes：传入的xmlData:" + xml );
           int flag =wlr.UpLoadReportFromBytes(token, xmlData, pdfdata, pdfdata_td, fileData, fileType, out errorMsg);            
            ZhiFang.Common.Log.Log.Info("WebLisProxyService->UpLoadReportFromBytes：传入的xmlData:" + xml + "@errorMsg:" + errorMsg);
            return flag;
        }
    }
}
