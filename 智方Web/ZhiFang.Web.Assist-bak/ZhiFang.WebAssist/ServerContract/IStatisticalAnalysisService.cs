using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.WebAssist;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.WebAssist.ServerContract
{
    [ServiceContract]
    public interface IStatisticalAnalysisService
    {
        [ServiceContractDescription(Name = "", Desc = "", Url = "ServerWCF/StatisticalAnalysisService.svc/Test", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Test", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Test();

    }
}
