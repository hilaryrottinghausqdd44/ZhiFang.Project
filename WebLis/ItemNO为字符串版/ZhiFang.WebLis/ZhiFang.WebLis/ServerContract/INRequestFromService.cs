using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Model;
using ZhiFang.Model.UiModel;
using System.EnterpriseServices;

namespace ZhiFang.WebLis.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.WebLis", Name = "ZhiFang.WebLis.NRequestFromService")]
    public interface INRequestFromService
    {

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetNRequestFromListByRBAC?page={page}&rows={rows}&fields={fields}&jsonentity={jsonentity}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        EntityListEasyUI<Model.NRequestFormResult> GetNRequestFromListByRBAC(int page, int rows, string fields, string jsonentity, string sort);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetNRequestFromStatisticsList?page={page}&rows={rows}&fields={fields}&jsonentity={jsonentity}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        EntityListEasyUI<Model.NRequestFormResult> GetNRequestFromStatisticsList(int page, int rows, string fields, string jsonentity, string sort);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CheckNRequestFromStatusByNRequestFromNo?nrequestfromno={nrequestfromno}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CheckNRequestFromStatusByNRequestFromNo(string nrequestfromno);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteNRequestFromByNRequestFromNo?nrequestfromno={nrequestfromno}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool DeleteNRequestFromByNRequestFromNo(string nrequestfromno);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/NrequestFormAddOrUpdate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("申请录入增加和修改服务")]
        [OperationContract]
        BaseResult NrequestFormAddOrUpdate(NrequestCombiItemBarCodeEntity jsonentity);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetNrequestFormByFormNo?nrequestformno={nrequestformno}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetNrequestFormByFormNo(string nrequestformno);


        #region 十堰大和契约
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/requestFormAdd_BarCodePrint", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("十堰大和weblis申请录入增加服务")]
        [OperationContract]
        BaseResultDataValue requestFormAdd_BarCodePrint(NrequestCombiItemBarCodeEntity jsonentity);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/requestFormAdd_BarCodePrintTaiHe", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("十堰大和weblis申请录入增加服务")]
        [OperationContract]
        BaseResultDataValue requestFormAdd_BarCodePrintTaiHe(NrequestCombiItemBarCodeEntity jsonentity);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/requestFormUpdate_BarCodePrint", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("十堰大和weblis申请录入修改服务")]
        [OperationContract]
        BaseResultDataValue requestFormUpdate_BarCodePrint(NrequestCombiItemBarCodeEntity jsonentity);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/requestFormUpdate_BarCodePrintTaiHe", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("十堰大和weblis申请录入修改服务")]
        [OperationContract]
        BaseResultDataValue requestFormUpdate_BarCodePrintTaiHe(NrequestCombiItemBarCodeEntity jsonentity);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetNrequestFormByFormNo_BarCodePrint?nrequestformno={nrequestformno}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetNrequestFormByFormNo_BarCodePrint(string nrequestformno);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetNrequestFormByFormNo_BarCodePrintTaiHe?nrequestformno={nrequestformno}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetNrequestFormByFormNo_BarCodePrintTaiHe(string nrequestformno);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBatchCodeListByFormNo_BarCodePrint?nrequestformnos={nrequestformnos}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBatchCodeListByFormNo_BarCodePrint(string nrequestformnos);


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateNrequestPrintTimesByFormNo?nrequestformnos={nrequestformnos}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateNrequestPrintTimesByFormNo(string nrequestformnos);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetTestItem_BarCodePrint?supergroupno={supergroupno}&itemkey={itemkey}&rows={rows}&page={page}&labcode={labcode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetTestItem_BarCodePrint(string supergroupno, string itemkey, int rows, int page, string labcode);

        #region
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetStaticRecOrgSamplePrice?StartDate={StartDate}&EndDate={EndDate}&rows={rows}&page={page}&labName={labName}&TestItem={TestItem}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetStaticRecOrgSamplePrice(string StartDate, string EndDate, int rows, int page, string labName, string TestItem);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBarcodePrice?StartDate={StartDate}&EndDate={EndDate}&rows={rows}&page={page}&labName={labName}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBarcodePrice(string StartDate, string EndDate, int rows, int page, string labName,string DateType);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetOpertorWorkCount?OperDateSart={OperDateSart}&OperDateEnd={OperDateEnd}&rows={rows}&page={page}&ClientNo={ClientNo}&Operator={Operator}&DateType={DateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetOpertorWorkCount(string OperDateSart, string OperDateEnd, int rows, int page, string ClientNo, string Operator, string DateType);

        #endregion

        #region 太和个人检验情况统计
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetStaticPersonTestItemPriceList?page={page}&rows={rows}&jsonentity={jsonentity}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetStaticPersonTestItemPriceList(int page, int rows, string jsonentity);
        #endregion
        #endregion


        #region 订单管理
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetOrderList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetOrderList(Model.SendOrder jsonentity);


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateOrderNote?OrderNo={OrderNo}&Note={Note}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateOrderNote(string OrderNo, string Note);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBarCodeByOrderNo?OrderNo={OrderNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBarCodeByOrderNo(string OrderNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetNrequestItemByBarCodeFormNo?BarCodeFormNo={BarCodeFormNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetNrequestItemByBarCodeFormNo(string BarCodeFormNo);


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelOrder?OrderNo={OrderNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DelOrder(string OrderNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetNRequestFromListByOrderNo?page={page}&rows={rows}&OrderNo={OrderNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetNRequestFromListByOrderNo(int page, int rows, string OrderNo);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBarCodeListByWhere", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBarCodeListByWhere(NRequestForm jsonentity);


        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddOrder", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResult AddOrder(Model.SendOrder jsonentity);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ConFirmOrder?OrderNo={OrderNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ConFirmOrder(string OrderNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ConFirmPrint?OrderNo={OrderNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ConFirmPrint(string OrderNo);


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SaveOrderText?strTxt={strTxt}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SaveOrderText(string strTxt);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetOrderText", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetOrderText();

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetNRequestFromListByByDetailsAndRBAC?page={page}&rows={rows}&fields={fields}&jsonentity={jsonentity}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        EntityListEasyUI<Model.NRequestFormResultOfConsume> GetNRequestFromListByByDetailsAndRBAC(int page, int rows, string fields, string jsonentity, string sort);
        #endregion

        #region 四川大家微信消费
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/OSConsumerUserOrderForm?PayCode={PayCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue OSConsumerUserOrderForm(string PayCode);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SaveOSConsumerUserOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("消费")]
        [OperationContract]
        BaseResultDataValue SaveOSConsumerUserOrderForm(NrequestCombiItemBarCodeEntity jsonentity);
        #endregion
    }
}
