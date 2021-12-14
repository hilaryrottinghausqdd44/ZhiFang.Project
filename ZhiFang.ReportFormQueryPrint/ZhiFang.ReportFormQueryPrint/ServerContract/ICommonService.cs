using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.ReportFormQueryPrint.Common;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.ReportFormQueryPrint")]
    public interface ICommonService
    {
        [ServiceContractDescription(Name = "获取程序及数据库版本", Desc = "获取程序及数据库版本", Url = "CommonService.svc/GetSystemVersion", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSystemVersion", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetSystemVersion();

        [ServiceContractDescription(Name = "升级程序及数据库版本", Desc = "升级程序及数据库版本", Url = "CommonService.svc/GetUpDateVersion", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetUpDateVersion", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetUpDateVersion();
    }
}
