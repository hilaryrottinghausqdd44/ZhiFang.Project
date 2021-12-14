using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.Base;

namespace ZhiFang.LabInformationIntegratePlatform.ServerWCF.Customization
{
    [ServiceContract(Namespace = "ZhiFang.LabInformationIntegratePlatform")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class SystemCacheService
    {
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetEmpListByCache", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        public BaseResultDataValue GetEmpListByCache()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            return brdv;
        }
    }
}
