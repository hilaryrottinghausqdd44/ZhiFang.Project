using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.LIIP")]
    public interface ILIIPStaticService
    {
        [ServiceContractDescription(Name = "消息确认处理及时率统计", Desc = "消息确认处理及时率统计", Url = "LIIPStaticService.svc/Static_SCMsg_Confirm_Handle_TimelyPerc", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "IntergrateSystemSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Static_SCMsg_Confirm_Handle_TimelyPerc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Static_SCMsg_Confirm_Handle_TimelyPerc(string LabCode,string StartDate,string EndDdate, string SickTypeId, string MsgTypeCodes, int DeptType, string DId);

        [ServiceContractDescription(Name = "统计消息的确认和处理的时长的平均值和中位数", Desc = "统计消息的确认和处理的时长的平均值和中位数", Url = "LIIPStaticService.svc/Static_SCMsg_Confirm_Handle_TimeRange", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "IntergrateSystemSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Static_SCMsg_Confirm_Handle_TimeRange", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Static_SCMsg_Confirm_Handle_TimeRange(string LabCode, string StartDate, string EndDdate, string SickTypeId, string MsgTypeCodes, int DeptType, string DId);

        [ServiceContractDescription(Name = "全部处理完成时间同比统计", Desc = "全部处理完成时间同比统计", Url = "LIIPStaticService.svc/Static_SCMsg_AllHandleFinish_YOYTimeRange", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "IntergrateSystemSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Static_SCMsg_AllHandleFinish_YOYTimeRange", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Static_SCMsg_AllHandleFinish_YOYTimeRange(string LabCode, int RangType, int Year, int Quarter, int Month, string SickTypeId, string MsgTypeCodes, int DeptType, string DId, int DataType);


        [ServiceContractDescription(Name = "全部处理完成时间环比统计", Desc = "全部处理完成时间环比统计", Url = "LIIPStaticService.svc/Static_SCMsg_Confirm_Handle_MOMTimeRange", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "IntergrateSystemSet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Static_SCMsg_Confirm_Handle_MOMTimeRange", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Static_SCMsg_Confirm_Handle_MOMTimeRange(string LabCode, int RangType, int Year, int Quarter, int Month, string SickTypeId, string MsgTypeCodes, int DeptType, string DId, int DataType);
    }
}
