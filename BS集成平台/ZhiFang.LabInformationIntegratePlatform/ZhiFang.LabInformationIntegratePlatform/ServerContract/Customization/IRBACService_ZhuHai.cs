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
    public interface IRBACService_ZhuHai
    {
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetUserInfoAndLogin?verifyCode={verifyCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetUserInfoAndLogin(string verifyCode);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAndAddHospital?StartTime={StartTime}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetAndAddHospital(string StartTime);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAndAddEmp_UserByHospitalCode?HospitalCode={HospitalCode}&StartTime={StartTime}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetAndAddEmp_UserByHospitalCode(string HospitalCode, string StartTime);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ExceptionInfoTest", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ExceptionInfoTest();
    }
}
