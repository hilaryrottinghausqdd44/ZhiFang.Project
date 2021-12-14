using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation
{
    [ServiceContract]
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

        #region 获取程序内部字典
        [ServiceContractDescription(Name = "获取枚举字典", Desc = "获取枚举字典", Url = "CommonService.svc/GetEnumDic?enumname={enumname}", Get = "enumname={enumname}", Post = "", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetEnumDic?enumname={enumname}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetEnumDic(string enumname);

        [ServiceContractDescription(Name = "获取类字典", Desc = "获取类字典", Url = "CommonService.svc/GetClassDic?classname={classname}&classnamespace={classnamespace}", Get = "classname={classname}&classnamespace={classnamespace}", Post = "", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetClassDic?classname={classname}&classnamespace={classnamespace}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetClassDic(string classname, string classnamespace);

        [ServiceContractDescription(Name = "获取类字典列表", Desc = "获取类字典列表", Url = "CommonService.svc/GetClassDicList", Get = "", Post = "ClassDicSearchPara", Return = "BaseResultDataValue", ReturnType = "ClassDicList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetClassDicList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetClassDicList(ClassDicSearchPara[] jsonpara);

        #endregion
    }
}