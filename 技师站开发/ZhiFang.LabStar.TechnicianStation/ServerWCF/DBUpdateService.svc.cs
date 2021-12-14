using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation.ServerWCF
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class DBUpdateService
    {
        [ServiceContractDescription(Name = "升级数据库", Desc = "升级数据库", Url = "DBUpdateService.svc/DataBaseUpdate", Get = "DataBaseUpdate", Post = "", Return = "", ReturnType = "bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DataBaseUpdate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public ZhiFang.Entity.Base.BaseResultBool DataBaseUpdate()
        {
            BaseResultBool brb = new BaseResultBool();
            ZhiFang.DBUpdate.DBUpdate.DataBaseUpdate("");
            return brb;
        }
    }
}
