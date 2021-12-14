using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZhiFang.ServiceCommon.RBAC;
using System.ServiceModel.Web;
using System.ServiceModel;
using ZhiFang.Entity.RBAC;
using System.ServiceModel.Activation;
using ZhiFang.Entity.Base;

namespace ZhiFang.ProjectProgressMonitorManage.ServerContract
{    
    [ServiceContract]
    public interface IPublicServiceWebHttpBinding
    {
        [ServiceContractDescription(Name = "获取服务端版本号", Desc = "获取服务端版本号", Url = "PublicService.svc/GetServiceSystemVersion", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "string")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetServiceSystemVersion", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetServiceSystemVersion();

        [ServiceContractDescription(Name = "获取服务端文件版本号", Desc = "获取服务端文件版本号", Url = "PublicService.svc/GetServiceSystemFileVersion", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "string")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetServiceSystemFileVersion", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetServiceSystemFileVersion();

        [ServiceContractDescription(Name = "通过脚本升级数据库", Desc = "通过脚本升级数据库", Url = "PublicService.svc/Public_UDTO_DBVersionUpate?curDBVersion={curDBVersion}", Get = "curDBVersion={curDBVersion}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Public_UDTO_DBVersionUpate?curDBVersion={curDBVersion}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Public_UDTO_DBVersionUpate(string curDBVersion);
    }
}
