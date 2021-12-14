using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Data;
using System.Xml;

namespace ZhiFang.WebLisService
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IReportFormService”。
    [ServiceContract]
    public interface IReportFormService
    {
        [OperationContract]
        string ReportFormDownLoad(DateTime start, DateTime end, string labcode, string BarCode, out string magStr);
        [OperationContract]
        string GetReportFormColumn();
        [OperationContract]
        bool DownloadReport(
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string BarCodeNo,               //条码码
            out string nodeReportFormItem, //一个条码可能有多个报告单，多个项目，多个文件
            out byte[][] FileReport,        //报告单号 + 报告文件流
            out string FileType,            //PDF,FRP,RTF
            out string xmlWebLisOthers,     //返回更多信息
            out string ReturnDescription);

        [OperationContract]
        bool SelectDownloadReport(
            DateTime start,
            DateTime end,
            string SourceOrgID,             //送检(源)单位
            string DestiOrgID,              //外送(至)单位
            string BarCodeNo,               //条码码
            out string nodeReportFormItem, //一个条码可能有多个报告单，多个项目，多个文件
            out byte[][] FileReport,        //报告单号 + 报告文件流
            out string FileType,            //PDF,FRP,RTF
            out string xmlWebLisOthers,     //返回更多信息
            out string ReturnDescription);

        [OperationContract]
        bool UploadReport_WhoNet(byte[] filedata, out string errorMsg);
        [OperationContract]
        bool UploadReportData_WhoNet(string filedata, out string errorMsg);
        [OperationContract]
        int UploadReportInfo_Synchronous(byte[] filedata, string filesName, string strJiaM, out string errorMsg);
        [OperationContract]
        bool QueryReport(int count, string WebLisFlag, string ClientNo, string Startdate, string Enddate, out string nodeReportForm, out string Error);
        [OperationContract]
        bool QueryReport_PKI(string WebLisFlag, string ClientNo, string Startdate, string Enddate, out DataSet ReportFormFull, out string Error);
        
        [OperationContract]
        bool DownLoadReportFormID(string ReportFormID, string ClientNo, out string WebReportXML, out string Error);
        [OperationContract]
        bool DownLoadReportFormID_yinzhou(string ReportFormID, string ClientNo, out string WebReportXML, out string Error);
        [OperationContract]
        bool DownLoadReportForm_PKI(out DataSet ReportItemFull, out DataSet ReportMicroFull, out DataSet ReportMarrowFull, out byte[] pdfData, out string Error, string ReportFormID);
        [OperationContract]
        bool Changestatus(string BarCode, string ClientNo, string Error);
        [OperationContract]
        bool DownLoadReportFormID_Hehe(string ReportFormID, string ClientNo, out string WebReportXML, out string FileDate, out string Error);
    }
}
