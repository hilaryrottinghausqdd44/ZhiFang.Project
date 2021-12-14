using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation
{
    [ServiceContract]
    public interface ILabStarCommonService
    {
        [ServiceContractDescription(Name = "提取中文字符串拼音字头", Desc = "提取中文字符串拼音字头", Url = "LabStarCommonService.svc/GetPinYinZiTou?chinese={chinese}", Get = "chinese={chinese}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetPinYinZiTou?chinese={chinese}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetPinYinZiTou(string chinese);

        [ServiceContractDescription(Name = "获取指定实体字段的最大号", Desc = "获取指定实体字段的最大号", Url = "LabStarCommonService.svc/GetMaxNoByEntityField?entityName={entityName}&entityField={entityField}", Get = "entityName={entityName}&entityField={entityField}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetMaxNoByEntityField?entityName={entityName}&entityField={entityField}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetMaxNoByEntityField(string entityName, string entityField);

        [ServiceContractDescription(Name = "获取指定枚举类型信息", Desc = "获取指定枚举类型信息", Url = "LabStarCommonService.svc/GetLabStarEnumType?enumTypeName={enumTypeName}", Get = "enumTypeName={enumTypeName}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetLabStarEnumType?enumTypeName={enumTypeName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetLabStarEnumType(string enumTypeName);

        [ServiceContractDescription(Name = "获取年龄相关信息", Desc = "根据出生日期获取年龄相关信息", Url = "LabStarCommonService.svc/GetPatientAge?collectTime={collectTime}&testTime={testTime}&DataAddTime={DataAddTime}&birthday={birthday}", Get = "collectTime={collectTime}&testTime={testTime}&DataAddTime={DataAddTime}&birthday={birthday}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetPatientAge?collectTime={collectTime}&testTime={testTime}&DataAddTime={DataAddTime}&birthday={birthday}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetPatientAge(string collectTime, string testTime, string DataAddTime, string birthday);

        [ServiceContractDescription(Name = "执行SQL语句", Desc = "执行SQL语句", Url = "LabStarCommonService.svc/ExecSQL", Get = "", Post = "strSQL", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ExecSQL", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message ExecSQL();

        [ServiceContractDescription(Name = "执行查询SQL语句", Desc = "执行查询SQL语句", Url = "LabStarCommonService.svc/QuerySQL", Get = "", Post = "strSQL", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QuerySQL", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message QuerySQL();

    }
}
