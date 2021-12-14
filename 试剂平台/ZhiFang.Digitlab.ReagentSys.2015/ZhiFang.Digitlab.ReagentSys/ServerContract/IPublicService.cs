using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using ZhiFang.Digitlab.ServiceCommon;
using System.ServiceModel.Web;

namespace ZhiFang.Digitlab.ReagentSys.ServerContract
{
    [ServiceContract]
    public interface IPublicService
    {
        [ServiceContractDescription(Name = "文件上传服务", Desc = "文件上传服务（表单方式）", Url = "PublicService.svc/Public_File_UpLoadByFrom", Get = "", Post = "relativePath: string, isGUIDFileName: string", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Public_File_UpLoadByFrom", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Public_File_UpLoadByFrom();
    }
}
