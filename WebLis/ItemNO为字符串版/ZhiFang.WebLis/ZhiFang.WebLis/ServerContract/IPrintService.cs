using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Model;

namespace ZhiFang.WebLis.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.WebLis", Name = "ZhiFang.WebLis.PrintService")]
    public interface IPrintService
    {
        #region 模块树
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Print_NRequestForm?PatientName={PatientName}&CollectStartDate={CollectStartDate}&CollectEndDate={CollectEndDate}&AddStartDate={AddStartDate}&AddEndDate={AddEndDate}&Doctor={Doctor}&WeblisOrgId={WeblisOrgId}&PatNo={PatNo}&SampleNo={SampleNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //List<string> Print_NRequestForm(string PatientName, string CollectStartDate, string CollectEndDate, string AddStartDate, string AddEndDate, string Doctor, string WeblisOrgId, string PatNo, string SampleTypeNo);
        #endregion


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ReportPrint?reportformId={reportformId}&reportformtitle={reportformtitle}&reportformfiletype={reportformfiletype}&printtype={printtype}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ReportPrint(string reportformId, string reportformtitle, string reportformfiletype, string printtype);

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportFullMerge11", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue GetReportFullMerge11();
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ReportFormIdGroupByCnameOrPatno?reportformIDs={reportformIDs}&GroupByCnameOrPatNo={GroupByCnameOrPatNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ReportFormIdGroupByCnameOrPatno(string reportformIDs, string GroupByCnameOrPatNo);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddReportModelFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddReportModelFile();


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdatePrintTimeByReportFormID?ReportFormID={ReportFormID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResult UpdatePrintTimeByReportFormID(string ReportFormID);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportFullMerge?reportformids={reportformids}&Reportformtitle={Reportformtitle}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetReportFullMerge(string ReportFormIDs, string Reportformtitle);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportFullMergeEn?reportformids={reportformids}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetReportFullMergeEn(string ReportFormIDs);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DownLoadReport", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DownLoadReport(Model.DownloadReportParam jsonentity);
    }
}
