using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;
using ZhiFang.ReportFormQueryPrint.Model.VO;

namespace ZhiFang.ReportFormQueryPrint.ServerContract
{
     [ServiceContract(Namespace = "ZhiFang.ReportFormQueryPrint")]
    public interface IReportFormService
    {
        /// <summary>
        /// 查询报告单
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectReport?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询报告单 /SelectReport?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}")]
        [OperationContract]
         BaseResultDataValue SelectReport(string Where, string fields, int page, int limit,string SerialNo);

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
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PreviewReport?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}&sortFields={sortFields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("预览报告单 /PreviewReport?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}&sortFields={sortFields}")]
        [OperationContract]
        BaseResultDataValue PreviewReport(string ReportFormID, string SectionNo, string SectionType, string ModelType, string sortFields);
        /// <summary>
        /// 预览报告单(页面选择)
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PreviewReportExtPageName?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}&PageName={PageName}&sortFields={sortFields}&SerialNos={SerialNos}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("预览报告单 /PreviewReportExtPageName?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}&PageName={PageName}&sortFields={sortFields}&SerialNos={SerialNos}")]
        [OperationContract]
        BaseResultDataValue PreviewReportExtPageName(string ReportFormID, string SectionNo, string SectionType, string ModelType, string PageName, string sortFields,string SerialNos);
        /// <summary>
        /// 打印报告单
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PrintReport?ReportFormID={ReportFormID}&ReportFormTitle={ReportFormTitle}&PrintType={PrintType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("打印报告单 /PrintReport?ReportFormID={ReportFormID}&ReportFormTitle={ReportFormTitle}&PrintType={PrintType}")]
        [OperationContract]
        BaseResultDataValue PrintReport(string ReportFormID, string ReportFormTitle, string PrintType);
        /// <summary>
        /// 历史结果
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ResultHistory?PatNo={PatNo}&ItemNo={ItemNo}&Table={Table}&Where={Where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询报告单 /ResultHistory?PatNo={PatNo}&ItemNo={ItemNo}&Table={Table}&Where={Where}")]
        [OperationContract]
        BaseResultDataValue ResultHistory(string PatNo, string ItemNo, string Table, string Where);
         /// <summary>
         /// 报告打印计数
         /// </summary>
         /// <param name="reportformidstr"></param>
         /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ReportFormAddPrintTimes?reportformidstr={reportformidstr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Model.BaseResult ReportFormAddPrintTimes(string reportformidstr);

        /// <summary>
        /// 混合合并
        /// </summary>
        /// <param name="fileList">pdf路径列表,多个文件名以，分隔</param>
        /// <param name="outMergeFile">合并后A4pdf路径</param>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BlendPDF", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue BlendPDF(string fileList ,string pageType);

        /// <summary>
        /// 双A5合并成A4
        /// </summary>
        /// <param name="fileList">A5pdf路径列表,多个文件名以，分隔</param>
        /// <param name="outMergeFile">合并后A4pdf路径</param>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DobuleA5MergeA4PDFFiles?fileList={fileList}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DobuleA5MergeA4PDFFiles(string fileList);

        /// <summary>
        /// 双A5合并成A4
        /// </summary>
        /// <param name="fileList">A5pdf路径列表,多个文件名以，分隔</param>
        /// <param name="outMergeFile">合并后A4pdf路径</param>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DobuleA5MergeA4PDFFilesPost", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DobuleA5MergeA4PDFFilesPost(string fileList);

        /// <summary>
        /// 双32K合并成16K
        /// </summary>
        /// <param name="fileList">16Kpdf路径列表,多个文件名以，分隔</param>
        /// <param name="outMergeFile">合并后16Kpdf路径</param>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Dobule32KMerge16KPDFFiles?fileList={fileList}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Dobule32KMerge16KPDFFiles(string fileList);

        /// <summary>
        /// 双32K合并成16K
        /// </summary>
        /// <param name="fileList">16Kpdf路径列表,多个文件名以，分隔</param>
        /// <param name="outMergeFile">合并后16Kpdf路径</param>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Dobule32KMerge16KPDFFilesPost", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Dobule32KMerge16KPDFFilesPost(string fileList);

        /// <summary>
        /// A4合并
        /// </summary>
        /// <param name="fileList">A4pdf路径列表,多个文件名以，分隔</param>
        /// <param name="outMergeFile">合并后A4pdf路径</param>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/MergeA4PDFFiles", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue MergeA4PDFFiles(string fileList);

        /// <summary>
        /// 16K合并
        /// </summary>
        /// <param name="fileList">A4pdf路径列表,多个文件名以，分隔</param>
        /// <param name="outMergeFile">合并后A4pdf路径</param>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Merge16KPDFFiles", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Merge16KPDFFiles(string fileList);

        /// <summary>
        /// 读取配置文件
        /// </summary>
        /// <param name="fileName">配置文件名称，带扩展名</param>
        /// <param name="outMergeFile"></param>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LoadConfig?fileName={fileName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LoadConfig(string fileName);

        /// <summary>
        /// 保存配置文件
        /// </summary>
        /// <param name="fileName">配置文件名称，带扩展名</param>
        /// <param name="configStr">配置字符串</param>
        /// <param name="outMergeFile"></param>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SaveConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
         [OperationContract]
        BaseResultDataValue SaveConfig(string fileName,string configStr);


        /// <summary>
        /// 双A5合并成A4
        /// </summary>
        /// <param name="fileList">A5pdf路径列表,多个文件名以，分隔</param>
        /// <param name="outMergeFile">合并后A4pdf路径</param>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/MergerReportContent?formNoList={formNoList}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue MergerReportContent(string formNoList);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportFormPDFByReportFormID?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&flag={flag}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetReportFormPDFByReportFormID(string ReportFormID, string SectionNo, string SectionType, int flag);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportFormPDFByReportFormIDTest?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&flag={flag}&nextIndex={nextIndex}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetReportFormPDFByReportFormIDTest(string ReportFormID, string SectionNo, string SectionType, int flag, int nextIndex);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteReportPDFFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DeleteReportPDFFile(string startDate, string endDate);

        /// <summary>
        /// 多项目历史对比
        /// </summary>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ResultMhistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("多项目历史对比 /ResultMhistory")]
        [OperationContract]
        BaseResultDataValue ResultMhistory(List<string> ReportFormID,string PatNo,List<string> SectionType,string Where);
        /// <summary>
        /// 根据病历号和时间查询报告单
        /// </summary>
        /// <param name="PatNo"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ResultDataTimeMhistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("根据病历号和时间查询报告单 /ResultDataTimeMhistory")]
        [OperationContract]
        BaseResultDataValue ResultDataTimeMhistory(string PatNO, string Where);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetMergReportFromByReportFormIdList?ReportFormIdList={ReportFormIdList}&SectionType={SectionType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetMergReportFromByReportFormIdList(string ReportFormIdList, string SectionType);

        /// <summary>
        /// 技师站查询报告单
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectRequest?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询报告单 /SelectRequest?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}")]
        [OperationContract]
        BaseResultDataValue SelectRequest(string Where, string fields, int page, int limit, string SerialNo);

        /// <summary>
        /// 点击字段排序查询报告单
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectRequestSort?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("点击字段排序查询报告单 /SelectRequestSort?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}&sort={sort}")]
        [OperationContract]
        BaseResultDataValue SelectRequestSort(string Where, string fields, int page, int limit, string SerialNo, string sort);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetRequestFormPDFByReportFormID?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&flag={flag}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetRequestFormPDFByReportFormID(string ReportFormID, string SectionNo, string SectionType, int flag);

        /// <summary>
        /// 预览报告单Request
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PreviewRequest?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("预览报告单 /PreviewRequest?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}")]
        [OperationContract]
        BaseResultDataValue PreviewRequest(string ReportFormID, string SectionNo, string SectionType, string ModelType);
        /// <summary>
        /// 预览报告单(页面选择)Request
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PreviewRequestExtPageName?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}&PageName={PageName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("预览报告单 /PreviewRequestExtPageName?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}&PageName={PageName}")]
        [OperationContract]
        BaseResultDataValue PreviewRequestExtPageName(string ReportFormID, string SectionNo, string SectionType, string ModelType, string PageName);

        /// <summary>
        /// 判断报告结果是否为空
        /// </summary>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RequestIsPrintNullValues", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("判断报告结果是否为空 /RequestIsPrintNullValues")]
        [OperationContract]
        BaseResultDataValue RequestIsPrintNullValues(string ReportFormID,string SectionType, string QueryType);

        /// <summary>
        /// 报告打印计数
        /// </summary>
        /// <param name="reportformidstr"></param>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RequestFormAddPrintTimes?reportformidstr={reportformidstr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Model.BaseResult RequestFormAddPrintTimes(string reportformidstr);

        /// <summary>
        /// request多项目历史对比
        /// </summary>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RequestResultMhistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("request多项目历史对比 /RequestResultMhistory")]
        [OperationContract]
        BaseResultDataValue RequestResultMhistory(List<string> ReportFormID, string PatNo, List<string> SectionType, string Where);

        /// <summary>
        /// 根据病历号和时间查询报告单
        /// </summary>
        /// <param name="PatNo"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RequestResultDataTimeMhistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("根据病历号和时间查询报告单 /RequestResultDataTimeMhistory")]
        [OperationContract]
        BaseResultDataValue RequestResultDataTimeMhistory(string PatNO, string Where);
        /// <summary>
        /// 根据选择的数据和选择的模板生成报告单
        /// </summary>
        /// <param name="ReportFormID"></param>
        /// <param name="SectionNo"></param>
        /// <param name="SectionType"></param>
        /// <param name="flag"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportFormPDFByReportFormIDANDTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetReportFormPDFByReportFormIDANDTemplate(string ReportFormID, string SectionNo, string SectionType, int flag, string Template, string QueryType);

        /// <summary>
        /// 根据四个联合主键查询报告单审核后
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportFromByReportFormID?idList={idList}&fields={fields}&strWhere={strWhere}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("根据四个联合主键查询报告单检验后 /GetReportFromByReportFormID?idList={idList}&fields={fields}&strWhere={strWhere}")]
        [OperationContract]
        BaseResultDataValue GetReportFromByReportFormID(string idList, string fields, string strWhere);

        /// <summary>
        /// 根据四个联合主键查询报告单审核中
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetRequestFromByReportFormID?idList={idList}&fields={fields}&strWhere={strWhere}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("根据四个联合主键查询报告单检验中 /GetRequestFromByReportFormID?idList={idList}&fields={fields}&strWhere={strWhere}")]
        [OperationContract]
        BaseResultDataValue GetRequestFromByReportFormID(string idList, string fields, string strWhere);


        /// <summary>
        /// 报告打印计数只更新PrintTimes
        /// </summary>
        /// <param name="reportformidstr"></param>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ReportFormAddUpdatePrintTimes?reportformidstr={reportformidstr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Model.BaseResult ReportFormAddUpdatePrintTimes(string reportformidstr);

        /// <summary>
        /// 报告打印计数只更新PrintTimes
        /// </summary>
        /// <param name="reportformidstr"></param>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RequestFormAddUpdatePrintTimes?reportformidstr={reportformidstr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Model.BaseResult RequestFormAddUpdatePrintTimes(string reportformidstr);

        /// <summary>
        /// 报告打印计数只更新ClientPrint
        /// </summary>
        /// <param name="reportformidstr"></param>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ReportFormAddUpdateClientPrintTimes?reportformidstr={reportformidstr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Model.BaseResult ReportFormAddUpdateClientPrintTimes(string reportformidstr);

        /// <summary>
        /// 报告打印计数只更新ClientPrint
        /// </summary>
        /// <param name="reportformidstr"></param>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RequestFormAddUpdateClientPrintTimes?reportformidstr={reportformidstr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Model.BaseResult RequestFormAddUpdateClientPrintTimes(string reportformidstr);

        /// <summary>
        /// 删除自助打印次数
        /// </summary>
        /// <param formno=""></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/deleteClientPrint", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue deleteClientPrint(List<string> formno);

        /// <summary>
        /// 删除临床打印次数
        /// </summary>
        /// <param formno=""></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/deletePrintTimes", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue deletePrintTimes(List<string> formno);


        /// <summary>
        /// 预览报告单Request  临时报告
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PreviewRequestByReportTemp?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("预览报告单 /PreviewRequestByReportTemp?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}")]
        [OperationContract]
        BaseResultDataValue PreviewRequestByReportTemp(string ReportFormID, string SectionNo, string SectionType, string ModelType);
        /// <summary>
        /// 预览报告单(页面选择)Request  临时报告
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PreviewRequestExtPageNameByReportTemp?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}&PageName={PageName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("预览报告单 /PreviewRequestExtPageNameByReportTemp?ReportFormID={ReportFormID}&SectionNo={SectionNo}&SectionType={SectionType}&ModelType={ModelType}&PageName={PageName}")]
        [OperationContract]
        BaseResultDataValue PreviewRequestExtPageNameByReportTemp(string ReportFormID, string SectionNo, string SectionType, string ModelType, string PageName);   

        /// <summary>
        /// 下载打包pdf
        /// </summary>
        /// <param ReportFormID=""></ReportFormID>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DownloadthePDFByReportFormID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DownloadthePDFByReportFormID(string ReportFormID, string SectionType);
        

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSectionPrintStrPageNameBySectionNo?SectionNo={SectionNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetSectionPrintStrPageNameBySectionNo(string SectionNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetCreatePUserESignature?SqlWhere={SqlWhere}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetCreatePUserESignature(string SqlWhere);

        /// <summary>
        /// 查询报告单（检验中和检验后）
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectReportRequest?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询报告单 /SelectReportRequest?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}")]
        [OperationContract]
        BaseResultDataValue SelectReportRequest(string Where, string fields, int page, int limit, string SerialNo);

        /// <summary>
        /// 生成样本清单
        /// </summary>
        /// <param name="ReportFormIdList"></param>
        /// <param name="SectionType"></param>
        /// <returns></returns>
        /// 
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSampleReportFromByReportFormIdList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetSampleReportFromByReportFormIdList(string ReportFormIdList, string SectionType);


        /// <summary>
        ///根据ReportFormID查询ReportFormFull列表(只查询报告库)
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportFormFullByReportFormID?ReportFormID={ReportFormID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询ReportFormFull列表 /GetReportFormFullByReportFormID?ReportFormID={ReportFormID}")]
        [OperationContract]
        BaseResultDataValue GetReportFormFullByReportFormID(string ReportFormID );

        /// <summary>
        ///根据ReportFormID查询ReportItemFull列表(只查询报告库)
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportItemFullByReportFormID?ReportFormID={ReportFormID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询ReportItemFull列表 /GetReportItemFullByReportFormID?ReportFormID={ReportFormID}")]
        [OperationContract]
        BaseResultDataValue GetReportItemFullByReportFormID(string ReportFormID);

        /// <summary>
        ///根据ReportFormID查询ReportMicroFull列表(只查询报告库)
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportMicroFullByReportFormID?ReportFormID={ReportFormID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询ReportMicroFull列表 /GetReportMicroFullByReportFormID?ReportFormID={ReportFormID}")]
        [OperationContract]
        BaseResultDataValue GetReportMicroFullByReportFormID(string ReportFormID);

        /// <summary>
        ///根据ReportFormID查询ReportMarrowFull列表(只查询报告库)
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportMarrowFullByReportFormID?ReportFormID={ReportFormID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询ReportMarrowFull列表 /GetReportMarrowFullByReportFormID?ReportFormID={ReportFormID}")]
        [OperationContract]
        BaseResultDataValue GetReportMarrowFullByReportFormID(string ReportFormID);


        /// <summary>
        /// 修改表报告库ReportFormFull表
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateReportFormFull", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateReportFormFull(List<ReportFormFull> models);

        /// <summary>
        /// 修改表报告库ReportItemFull表
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateReportItemFull", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateReportItemFull(List<ReportItemFull> models);

        /// <summary>
        /// 修改表报告库ReportMicroFull表
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateReportMicroFull", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateReportMicroFull(List<ReportMicroFull> models);

        /// <summary>
        /// 修改表报告库ReportMarrowFull表
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateReportMarrowFull", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateReportMarrowFull(List<ReportMarrowFull> models);

        /// <summary>
        /// 增加修改表的操作日志
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddSC_Operation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddSC_Operation(List<SC_Operation> models);

        /// <summary>
        /// 根据文字生成语音文件
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CreatVoice?txt={txt}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("根据文字生成语音文件 /CreatVoice?txt={txt}")]
        [OperationContract]
        BaseResultDataValue CreatVoice(string txt);

        /// <summary>
        /// 修改webconfig配置文件
        /// </summary>
        /// <param name="configStr">配置字符串</param>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateWebConfig", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateWebConfig( List<webconfigVo> model);

        /// <summary>
        /// 读取webconfig配置文件参数
        /// </summary>
        /// <param name="key">节点名称</param>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LoadWebConfig?key={key}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("读取webconfig配置文件 /LoadWebConfig?key={key}")]
        [OperationContract]
        BaseResultDataValue LoadWebConfig(string key);

        /// <summary>
        /// 数据库升级
        /// </summary>
        /// <param name="Version">版本号</param>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DBupdate?Version={Version}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("数据库升级 /DBupdate?Version={Version}")]
        [OperationContract]
        BaseResultDataValue DBupdate(string Version);

        /// <summary>
        /// 获取程序集以及数据库版本
        /// </summary>
        /// <param name="Version">版本号</param>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetDBVersion?Version={Version}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("获取程序集以及数据库版本 /GetDBVersion?Version={Version}")]
        [OperationContract]
        BaseResultDataValue GetDBVersion(string Version);

        //静态账户登录
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/StaticUserLogin?Account={Account}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue StaticUserLogin(string Account);

        /// <summary>
        /// 技师站多项目历史对比
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LabStarResultMhistory?Where={Where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("技师站多项目历史对比 /LabStarResultMhistory?Where={Where}")]
        [OperationContract]
        BaseResultDataValue LabStarResultMhistory(string Where);

        /// <summary>
        /// 技师站多项目历史对比(新)
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/NewLabStarResultMhistory?Where={Where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("技师站多项目历史对比(新) /NewLabStarResultMhistory?Where={Where}")]
        [OperationContract]
        BaseResultDataValue NewLabStarResultMhistory(string Where);

        /// <summary>
        /// 技师站历史结果
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LabStarResultHistory?PatNo={PatNo}&ItemNo={ItemNo}&Table={Table}&Where={Where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("技师站历史结果 /LabStarResultHistory?PatNo={PatNo}&ItemNo={ItemNo}&Table={Table}&Where={Where}")]
        [OperationContract]
        BaseResultDataValue LabStarResultHistory(string PatNo, string ItemNo, string Table, string Where);

        /// <summary>
        /// 样本状态跟踪
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SampleStateTailAfter?ReportFormId={ReportFormId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("样本状态跟踪 /SampleStateTailAfter?ReportFormId={ReportFormId}")]
        [OperationContract]
        BaseResultDataValue SampleStateTailAfter(string ReportFormId);

        /// <summary>
        /// 山东自助打单定制调用第三方服务获取查询条件
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelfhelpCustomizationServiceGetWhere?barCode={barCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("自助打单定制调用第三方服务获取查询条件 /SelfhelpCustomizationServiceGetWhere?barCode={barCode}")]
        [OperationContract]
        BaseResultDataValue SelfhelpCustomizationServiceGetWhere(string barCode);

        /// <summary>
        /// 济南中心医院自助打单定制调用第三方服务获取查询条件
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelfhelpCustomizationServiceGetWhereJiNan?barCode={barCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("济南中心医院自助打单定制调用第三方服务获取查询条件 /SelfhelpCustomizationServiceGetWhereJiNan?barCode={barCode}")]
        [OperationContract]
        BaseResultDataValue SelfhelpCustomizationServiceGetWhereJiNan(string barCode);

        /// <summary>
        /// 查询NRequestForm病人信息
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectNRequestForm?Where={Where}&fields={fields}&page={page}&limit={limit}&sort={sort}&SerialNo={SerialNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询病人信息 /SelectNRequestForm?Where={Where}&fields={fields}&page={page}&limit={limit}&sort={sort}&SerialNo={SerialNo}")]
        [OperationContract]
        BaseResultDataValue SelectNRequestForm(string Where, string fields, int page, int limit, string sort,string SerialNo);
        /// <summary>
        /// 查询检验前后报告单
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectRequestFormAndReportForm?Where={Where}&fields={fields}&page={page}&limit={limit}&sort={sort}&SerialNo={SerialNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询检验前后报告单 /SelectRequestFormAndReportForm?Where={Where}&fields={fields}&page={page}&limit={limit}&sort={sort}&SerialNo={SerialNo}")]
        [OperationContract]
        BaseResultDataValue SelectRequestFormAndReportForm(string Where, string fields, int page, int limit, string sort, string SerialNo);
        /// <summary>
        /// 查询检验前后报告单数量
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SelectRequestFormAndReportFormCount?Where={Where}&fields={fields}&page={page}&limit={limit}&sort={sort}&SerialNo={SerialNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询检验前后报告单数量 /SelectRequestFormAndReportFormCount?Where={Where}&fields={fields}&page={page}&limit={limit}&sort={sort}&SerialNo={SerialNo}")]
        [OperationContract]
        BaseResultDataValue SelectRequestFormAndReportFormCount(string Where, string fields, int page, int limit, string sort, string SerialNo);


        /// <summary>
        /// 移动报告pdf
        /// reportfields报告单路径，reportformid报告id，folderfield要打包的文件夹名称
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/MoveThePDFByReportFormID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue MoveThePDFByReportFormID(List<string> Reportfields, string ReportFormID, string Folderfield);


        /// <summary>
        /// 下载打包pdf
        /// folderPath要打包的文件夹名称
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ZipAndDowanloadThePDFByReportFormID?FolderPath={FolderPath}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ZipAndDowanloadThePDF(string FolderPath);

        /// <summary>
        /// LabStar点击字段排序查询报告单
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LabStarSelectReportSort?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("LabStar点击字段排序查询报告单 /LabStarSelectReportSort?Where={Where}&fields={fields}&page={page}&limit={limit}&SerialNo={SerialNo}&sort={sort}")]
        [OperationContract]
        BaseResultDataValue LabStarSelectReportSort(string Where, string fields, int page, int limit, string SerialNo, string sort);
    }
}