using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.ReportFormQueryPrint")]
    public interface IReportFormHistoryService
    {
        /// <summary>
        /// 查询报告单
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectReport?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询报告单 /SelectReport?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}")]
        [OperationContract]
        BaseResultDataValue SelectReport(string Where, string fields, int page, int limit, string SerialNo);

        /// <summary>
        /// 点击字段排序查询报告单
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectReportSort?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("点击字段排序查询报告单 /SelectReportSort?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}&sort={sort}")]
        [OperationContract]
        BaseResultDataValue SelectReportSort(string Where, string fields, int page, int limit, string SerialNo, string sort);

        /// <summary>
        /// 预览报告单
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PreviewReport?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("预览报告单 /PreviewReport?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}")]
        [OperationContract]
        BaseResultDataValue PreviewReport(string ReportFormID, string SectionNo, string SectionType, string ModelType);
        /// <summary>
        /// 预览报告单(页面选择)
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PreviewReportExtPageName?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}&PageName={PageName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("预览报告单 /PreviewReportExtPageName?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}&PageName={PageName}")]
        [OperationContract]
        BaseResultDataValue PreviewReportExtPageName(string ReportFormID, string SectionNo, string SectionType, string ModelType, string PageName);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportFormPDFByReportFormID?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&flag={flag}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetReportFormPDFByReportFormID(string ReportFormID, string SectionNo, string SectionType, int flag);

        /// <summary>
        /// 报告打印计数只更新PrintTimes
        /// </summary>
        /// <param name="reportformidstr"></param>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ReportFormAddUpdatePrintTimes?reportformidstr={reportformidstr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Model.BaseResult ReportFormAddUpdatePrintTimes(string reportformidstr);
    }
}
