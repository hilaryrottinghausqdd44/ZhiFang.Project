using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ComponentModel;
using ZhiFang.Model;
using ZhiFang.Model.UiModel;

namespace ZhiFang.WebLis.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.WebLis", Name = "ZhiFang.WebLis.ServiceWCF.ReportFromService")]
    public interface IReportFromService
    {
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectReportList?wherestr={wherestr}&page={page}&rows={rows}&sort={sort}&order={order}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SelectReportList(string wherestr, int page, int rows, string sort, string order);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectReportList2?Startdate={Startdate}&Enddate={Enddate}&CLIENTNO={CLIENTNO}&SECTIONNO={SECTIONNO}&CNAME={CNAME}&GENDERNAME={GENDERNAME}&SAMPLENO={SAMPLENO}&PATNO={PATNO}&SICKTYPENO={SICKTYPENO}&statues={statues}&ZDY10={ZDY10}&PERSONID={PERSONID}&LIKESEARCH={LIKESEARCH}&serialno={serialno}&clientcode={clientcode}&collectStartdate={collectStartdate}&collectEnddate={collectEnddate}&noperdateStart={noperdateStart}&noperdateEnd={noperdateEnd}&checkdateStart={checkdateStart}&checkdateEnd={checkdateEnd}&page={page}&rows={rows}&sort={sort}&order={order}&abnormalstatues={abnormalstatues}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SelectReportList2(string Startdate, string Enddate, string CLIENTNO, string SECTIONNO, string CNAME, string GENDERNAME, string SAMPLENO, string PATNO, string SICKTYPENO, string statues, string ZDY10, string PERSONID, string LIKESEARCH, string serialno, string clientcode, string collectStartdate, string collectEnddate, string noperdateStart, string noperdateEnd, string checkdateStart, string checkdateEnd, int page, int rows, string sort, string order, string abnormalstatues);
        //BaseResultDataValue SelectReportList2(int page, int rows);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectReportListFoshan?Startdate={Startdate}&Enddate={Enddate}&CLIENTNO={CLIENTNO}&SECTIONNO={SECTIONNO}&CNAME={CNAME}&GENDERNAME={GENDERNAME}&SAMPLENO={SAMPLENO}&PATNO={PATNO}&statues={statues}&ZDY10={ZDY10}&PERSONID={PERSONID}&LIKESEARCH={LIKESEARCH}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SelectReportListFoshan(string Startdate, string Enddate, string CLIENTNO, string SECTIONNO, string CNAME, string GENDERNAME, string SAMPLENO, string PATNO, string statues, string ZDY10, string PERSONID, string LIKESEARCH, int page, int rows);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPreviewReportResultById?reportformId={reportformId}&sectionNo={sectionNo}&sectionType={sectionType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetPreviewReportResultById(string reportformId, int sectionNo, int sectionType);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPreviewReportImageById?reportformId={reportformId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetPreviewReportImageById(string reportformId);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPreviewReportById?reportformId={reportformId}&sectionNo={sectionNo}&sectionType={sectionType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetPreviewReportById(string reportformId, int sectionNo, int sectionType);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/testAAA?page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue testAAA(int page, int rows);

        #region 导出Excel报告
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetReportItem(VIEW_ReportItemFull jsonentity);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DownLoadReportExcel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DownLoadReportExcel(VIEW_ReportItemFull jsonentity);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DownloadReportExcelByPara?Startdate={Startdate}&Enddate={Enddate}&CLIENTNO={CLIENTNO}&SECTIONNO={SECTIONNO}&CNAME={CNAME}&GENDERNAME={GENDERNAME}&SAMPLENO={SAMPLENO}&PATNO={PATNO}&SICKTYPENO={SICKTYPENO}&statues={statues}&ZDY10={ZDY10}&PERSONID={PERSONID}&LIKESEARCH={LIKESEARCH}&serialno={serialno}&clientcode={clientcode}&collectStartdate={collectStartdate}&collectEnddate={collectEnddate}&noperdateStart={noperdateStart}&noperdateEnd={noperdateEnd}&checkdateStart={checkdateStart}&checkdateEnd={checkdateEnd}&page={page}&rows={rows}&sort={sort}&order={order}&abnormalstatues={abnormalstatues}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DownloadReportExcelByPara(string Startdate, string Enddate, string CLIENTNO, string SECTIONNO, string CNAME, string GENDERNAME, string SAMPLENO, string PATNO, string SICKTYPENO, string statues, string ZDY10, string PERSONID, string LIKESEARCH, string serialno, string clientcode, string collectStartdate, string collectEnddate, string noperdateStart, string noperdateEnd, string checkdateStart, string checkdateEnd, int page, int rows, string sort, string order, string abnormalstatues);
        #endregion

        #region 工作量统计报表

        #region 获取报表下载地址

        //工作量条码查询生成Excel
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBarcodePrice?StartDate={StartDate}&EndDate={EndDate}&rows={rows}&page={page}&labName={labName}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBarcodePrice(string StartDate, string EndDate, int rows, int page, string labName, string DateType);
        #endregion

        #region  工作量项目查询生成Excel
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetStaticRecOrgSamplePrice?StartDate={StartDate}&EndDate={EndDate}&rows={rows}&page={page}&labName={labName}&TestItem={TestItem}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetStaticRecOrgSamplePrice(string StartDate, string EndDate, int rows, int page, string labName, string TestItem);
        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetOperatorWorkCountExcel?OperDateSart={OperDateSart}&OperDateEnd={OperDateEnd}&ClientNo={ClientNo}&Operator={Operator}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetOperatorWorkCountExcel(string OperDateSart, string OperDateEnd, string ClientNo, string Operator, string DateType);

        #region 工作量报表下载
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DownLoadStaticRecOrgSamplePriceExcel?StartDate={StartDate}&EndDate={EndDate}&rows={rows}&page={page}&labName={labName}&TestItem={TestItem}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DownLoadStaticRecOrgSamplePriceExcel(string StartDate, string EndDate, int rows, int page, string labName, string TestItem);
        #endregion

        #region  工作量报表打印
        #endregion

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteReportForm?ReportFormID={ReportFormID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResult DeleteReportForm(string ReportFormID);

        #region 太和个人检验情况统计
        #region 导出Excel报告
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetStaticPersonTestItemPriceItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetStaticPersonTestItemPriceItem(Model.StaticPersonTestItemPrice jsonentity);
        #endregion

        #region 下载
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DownLoadStaticPersonTestItemPriceExcel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DownLoadStaticPersonTestItemPriceExcel(Model.StaticPersonTestItemPrice jsonentity);
        #endregion

        #region 报告打印
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StaticPersonTestItemPricePrint", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetStaticPersonTestItemPricePrint(Model.StaticPersonTestItemPrice jsonentity);
        #endregion
        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectReportListByPerson_Barcode_Name?Barcode={Barcode}&Name={Name}&page={page}&rows={rows}&sort={sort}&order={order}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SelectReportListByPerson_Barcode_Name(string Barcode, string Name, int page, int rows, string sort, string order);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PersonSearch_ValidateCode_PKI?tmpvalidatecode={tmpvalidatecode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        bool PersonSearch_ValidateCode_PKI(string tmpvalidatecode);
    }
}
