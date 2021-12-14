using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;
using ZhiFang.Entity.Base;

namespace ZhiFang.LabInformationIntegratePlatform.ServerContract.Customization
{
    [ServiceContract(Namespace = "ZhiFang.LabInformationIntegratePlatform")]
    public interface IDicReceiveService
    {
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ReceiveHospitalAndArea", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ReceiveHospitalAndArea(string areaJosn, string hospitalJson);
    }
}
