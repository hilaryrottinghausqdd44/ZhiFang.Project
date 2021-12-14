using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ZhiFang.Model;
using System.ServiceModel.Web;

namespace ZhiFang.WebLis.ServerContract
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的接口名“IPubliserService”。
    [ServiceContract(CallbackContract = typeof(IPublisherEvents), Namespace = "ZhiFang.WebLis")]
    public interface IPubliserService
    {
        [OperationContract]
        bool RegisterClient(string id);//订阅

        [OperationContract]
        bool UnRegisterClinet(string id);//取消订阅

    }
    [ServiceContract(Namespace = "ZhiFang.WebLis")]
    public interface IPublisherEvents
    {
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPatientInfoByOrgIdAndPatientID?OrdId={OrdId}&PatientId={PatientId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetPatientInfoByOrgIdAndPatientID(string OrdId, string PatientId);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPatientIdByOrgId?OrdId={OrdId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetPatientIdByOrgId(string OrdId);

        /// <summary>
        /// 寿光区中心平台嵌入报告查询界面  ganwh add 2015-10-13
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportQueryUI?user={user}&password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResult GetReportQueryUI(string user, string password);
    }
}
