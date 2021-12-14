using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ZhiFang.Entity.RBAC;
using System.ServiceModel.Web;
using System.ComponentModel;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA;
using System.IO;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;

namespace ZhiFang.ProjectProgressMonitorManage.ServerContract
{
    [ServiceContract]
    public interface IWeiXinAppService
    {
        #region ATApproveStatus

        [ServiceContractDescription(Name = "新增审批状态", Desc = "新增审批状态", Url = "WeiXinAppService.svc/ST_UDTO_AddATApproveStatus", Get = "", Post = "ATApproveStatus", Return = "BaseResultDataValue", ReturnType = "ATApproveStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATApproveStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATApproveStatus(ATApproveStatus entity);

        [ServiceContractDescription(Name = "修改审批状态", Desc = "修改审批状态", Url = "WeiXinAppService.svc/ST_UDTO_UpdateATApproveStatus", Get = "", Post = "ATApproveStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateATApproveStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateATApproveStatus(ATApproveStatus entity);

        [ServiceContractDescription(Name = "修改审批状态指定的属性", Desc = "修改审批状态指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateATApproveStatusByField", Get = "", Post = "ATApproveStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateATApproveStatusByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateATApproveStatusByField(ATApproveStatus entity, string fields);

        [ServiceContractDescription(Name = "删除审批状态", Desc = "删除审批状态", Url = "WeiXinAppService.svc/ST_UDTO_DelATApproveStatus?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelATApproveStatus?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelATApproveStatus(long id);

        [ServiceContractDescription(Name = "查询审批状态", Desc = "查询审批状态", Url = "WeiXinAppService.svc/ST_UDTO_SearchATApproveStatus", Get = "", Post = "ATApproveStatus", Return = "BaseResultList<ATApproveStatus>", ReturnType = "ListATApproveStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATApproveStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATApproveStatus(ATApproveStatus entity);

        [ServiceContractDescription(Name = "查询审批状态(HQL)", Desc = "查询审批状态(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchATApproveStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ATApproveStatus>", ReturnType = "ListATApproveStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATApproveStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATApproveStatusByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询审批状态", Desc = "通过主键ID查询审批状态", Url = "WeiXinAppService.svc/ST_UDTO_SearchATApproveStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ATApproveStatus>", ReturnType = "ATApproveStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATApproveStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATApproveStatusById(long id, string fields, bool isPlanish);
        #endregion

        #region ATAttendanceEventType

        [ServiceContractDescription(Name = "新增考勤事件类型表", Desc = "新增考勤事件类型表", Url = "WeiXinAppService.svc/ST_UDTO_AddATAttendanceEventType", Get = "", Post = "ATAttendanceEventType", Return = "BaseResultDataValue", ReturnType = "ATAttendanceEventType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATAttendanceEventType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATAttendanceEventType(ATAttendanceEventType entity);

        [ServiceContractDescription(Name = "修改考勤事件类型表", Desc = "修改考勤事件类型表", Url = "WeiXinAppService.svc/ST_UDTO_UpdateATAttendanceEventType", Get = "", Post = "ATAttendanceEventType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateATAttendanceEventType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateATAttendanceEventType(ATAttendanceEventType entity);

        [ServiceContractDescription(Name = "修改考勤事件类型表指定的属性", Desc = "修改考勤事件类型表指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateATAttendanceEventTypeByField", Get = "", Post = "ATAttendanceEventType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateATAttendanceEventTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateATAttendanceEventTypeByField(ATAttendanceEventType entity, string fields);

        [ServiceContractDescription(Name = "删除考勤事件类型表", Desc = "删除考勤事件类型表", Url = "WeiXinAppService.svc/ST_UDTO_DelATAttendanceEventType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelATAttendanceEventType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelATAttendanceEventType(long id);

        [ServiceContractDescription(Name = "查询考勤事件类型表", Desc = "查询考勤事件类型表", Url = "WeiXinAppService.svc/ST_UDTO_SearchATAttendanceEventType", Get = "", Post = "ATAttendanceEventType", Return = "BaseResultList<ATAttendanceEventType>", ReturnType = "ListATAttendanceEventType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATAttendanceEventType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATAttendanceEventType(ATAttendanceEventType entity);

        [ServiceContractDescription(Name = "查询考勤事件类型表(HQL)", Desc = "查询考勤事件类型表(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchATAttendanceEventTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ATAttendanceEventType>", ReturnType = "ListATAttendanceEventType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATAttendanceEventTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATAttendanceEventTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询考勤事件类型表", Desc = "通过主键ID查询考勤事件类型表", Url = "WeiXinAppService.svc/ST_UDTO_SearchATAttendanceEventTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ATAttendanceEventType>", ReturnType = "ATAttendanceEventType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATAttendanceEventTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATAttendanceEventTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region ATEmpAttendanceEventLog

        [ServiceContractDescription(Name = "新增员工考勤事件日志表", Desc = "新增员工考勤事件日志表", Url = "WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventLog", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATEmpAttendanceEventLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventLog(ATEmpAttendanceEventLog entity);

        [ServiceContractDescription(Name = "新增员工考勤请假事件", Desc = "新增员工考勤请假事件", Url = "WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventleaveevent", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATEmpAttendanceEventleaveevent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventleaveevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime);

        [ServiceContractDescription(Name = "新增员工考勤外出事件", Desc = "新增员工考勤外出事件", Url = "WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventEgressevent", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATEmpAttendanceEventEgressevent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventEgressevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime);

        [ServiceContractDescription(Name = "新增员工考勤出差事件", Desc = "新增员工考勤出差事件", Url = "WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventTripevent", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATEmpAttendanceEventTripevent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventTripevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime);

        [ServiceContractDescription(Name = "新增员工考勤加班事件", Desc = "新增员工考勤加班事件", Url = "WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventOvertimeevent", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATEmpAttendanceEventOvertimeevent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventOvertimeevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime);

        [ServiceContractDescription(Name = "新增员工考勤请假事件并验证是否允许申请", Desc = "新增员工考勤请假事件并验证是否允许申请", Url = "WeiXinAppService.svc/ST_UDTO_AddAndCheckATEmpAttendanceEventleaveevent", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddAndCheckATEmpAttendanceEventleaveevent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddAndCheckATEmpAttendanceEventleaveevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime);

        [ServiceContractDescription(Name = "新增员工考勤外出事件并验证是否允许申请", Desc = "新增员工考勤外出事件并验证是否允许申请", Url = "WeiXinAppService.svc/ST_UDTO_AddAndCheckATEmpAttendanceEventEgressevent", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddAndCheckATEmpAttendanceEventEgressevent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddAndCheckATEmpAttendanceEventEgressevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime);

        [ServiceContractDescription(Name = "新增员工考勤出差事件并验证是否允许申请", Desc = "新增员工考勤出差事件并验证是否允许申请", Url = "WeiXinAppService.svc/ST_UDTO_AddAndCheckATEmpAttendanceEventTripevent", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddAndCheckATEmpAttendanceEventTripevent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddAndCheckATEmpAttendanceEventTripevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime);

        [ServiceContractDescription(Name = "新增员工考勤加班事件并验证是否允许申请", Desc = "新增员工考勤加班事件并验证是否允许申请", Url = "WeiXinAppService.svc/ST_UDTO_AddAndCheckATEmpAttendanceEventOvertimeevent", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddAndCheckATEmpAttendanceEventOvertimeevent", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddAndCheckATEmpAttendanceEventOvertimeevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime);

        [ServiceContractDescription(Name = "员工考勤签到事件", Desc = "员工考勤签到事件", Url = "WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventLogSignIn", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATEmpAttendanceEventLogSignIn", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventLogSignIn(ATEmpAttendanceEventLog entity);

        [ServiceContractDescription(Name = "员工考勤签退事件", Desc = "员工考勤签退事件", Url = "WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventLogSignOut", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATEmpAttendanceEventLogSignOut", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventLogSignOut(ATEmpAttendanceEventLog entity);

        [ServiceContractDescription(Name = "员工考勤上报位置事件", Desc = "员工考勤上报位置事件", Url = "WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventLogUploadPostion", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATEmpAttendanceEventLogUploadPostion", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventLogUploadPostion(ATEmpAttendanceEventLog entity);

        [ServiceContractDescription(Name = "员工考勤上报位置并与考勤设置进行验证,判断是否可以上报事件", Desc = "员工考勤上报位置并与考勤设置进行验证,判断是否可以上报事件", Url = "WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventLogUploadPostionAndCheck", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATEmpAttendanceEventLogUploadPostionAndCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventLogUploadPostionAndCheck(ATEmpAttendanceEventLog entity);

        [ServiceContractDescription(Name = "考勤签到签退时的地点及时间判断处理", Desc = "考勤签到签退时的地点及时间判断处理", Url = "WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventLogCheck", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATEmpAttendanceEventLogCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_AddATEmpAttendanceEventLogCheck(ATEmpAttendanceEventLog entity);

        [ServiceContractDescription(Name = "考勤签到签退时的地点及时间判断处理(测试用)", Desc = "考勤签到签退时的地点及时间判断处理(测试用)", Url = "WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventLogCheckTest?subType={subType}", Get = "subType={subType}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATEmpAttendanceEventLogCheckTest?subType={subType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_AddATEmpAttendanceEventLogCheckTest(long subType);

        [ServiceContractDescription(Name = "员工签到签退的考勤地点是否正确或脱岗处理", Desc = "员工签到签退的考勤地点是否正确或脱岗处理", Url = "WeiXinAppService.svc/ST_UDTO_ATEmpAttendanceEventLogCheckPostion", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ATEmpAttendanceEventLogCheckPostion", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_ATEmpAttendanceEventLogCheckPostion(ATEmpAttendanceEventLog entity, int postionType);

        [ServiceContractDescription(Name = "处理签到时间与考勤设置上班时间是正常还是迟到", Desc = "处理签到时间与考勤设置上班时间是正常还是迟到", Url = "WeiXinAppService.svc/ST_UDTO_ATEmpAttendanceEventLogCheckSignInTime", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ATEmpAttendanceEventLogCheckSignInTime", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_ATEmpAttendanceEventLogCheckSignInTime(ATEmpAttendanceEventLog entity);

        [ServiceContractDescription(Name = "处理签退时间与考勤设置下班时间是正常还是早退", Desc = "处理签退时间与考勤设置下班时间是正常还是早退", Url = "WeiXinAppService.svc/ST_UDTO_ATEmpAttendanceEventLogCheckSignOutTime", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ATEmpAttendanceEventLogCheckSignOutTime", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_ATEmpAttendanceEventLogCheckSignOutTime(ATEmpAttendanceEventLog entity);

        [ServiceContractDescription(Name = "修改员工考勤事件日志表", Desc = "修改员工考勤事件日志表", Url = "WeiXinAppService.svc/ST_UDTO_UpdateATEmpAttendanceEventLog", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateATEmpAttendanceEventLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateATEmpAttendanceEventLog(ATEmpAttendanceEventLog entity);

        [ServiceContractDescription(Name = "修改员工考勤事件日志表指定的属性", Desc = "修改员工考勤事件日志表指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateATEmpAttendanceEventLogByField", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateATEmpAttendanceEventLogByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateATEmpAttendanceEventLogByField(ATEmpAttendanceEventLog entity, string fields);

        [ServiceContractDescription(Name = "删除员工考勤事件日志表", Desc = "删除员工考勤事件日志表", Url = "WeiXinAppService.svc/ST_UDTO_DelATEmpAttendanceEventLog?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelATEmpAttendanceEventLog?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelATEmpAttendanceEventLog(long id);

        [ServiceContractDescription(Name = "查询员工考勤事件日志表", Desc = "查询员工考勤事件日志表", Url = "WeiXinAppService.svc/ST_UDTO_SearchATEmpAttendanceEventLog", Get = "", Post = "ATEmpAttendanceEventLog", Return = "BaseResultList<ATEmpAttendanceEventLog>", ReturnType = "ListATEmpAttendanceEventLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATEmpAttendanceEventLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventLog(ATEmpAttendanceEventLog entity);

        [ServiceContractDescription(Name = "查询员工考勤事件日志表(HQL)", Desc = "查询员工考勤事件日志表(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchATEmpAttendanceEventLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ATEmpAttendanceEventLog>", ReturnType = "ListATEmpAttendanceEventLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATEmpAttendanceEventLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询员工考勤事件日志表", Desc = "通过主键ID查询员工考勤事件日志表", Url = "WeiXinAppService.svc/ST_UDTO_SearchATEmpAttendanceEventLogById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ATEmpAttendanceEventLog>", ReturnType = "ATEmpAttendanceEventLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATEmpAttendanceEventLogById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventLogById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过日期代码获取登录者考勤记录", Desc = "通过日期代码获取登录者考勤记录", Url = "WeiXinAppService.svc/GetATEmpAttendanceEventLogByDTCode?dtcode={dtcode}", Get = "dtcode={dtcode}", Post = "", Return = "BaseResultList<SignInfo>", ReturnType = "SignInfo")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetATEmpAttendanceEventLogByDTCode?dtcode={dtcode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetATEmpAttendanceEventLogByDTCode(string dtcode);

        [ServiceContractDescription(Name = "通过部门代码获取审批人", Desc = "通过部门代码获取审批人", Url = "WeiXinAppService.svc/GetATEmpAttendanceEventApproveByDeptId?DeptId={DeptId}", Get = "DeptId={DeptId}", Post = "", Return = "BaseResult<HREmployee>", ReturnType = "HREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetATEmpAttendanceEventApproveByDeptId?DeptId={DeptId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetATEmpAttendanceEventApproveByDeptId(string DeptId);

        [ServiceContractDescription(Name = "获取审批人", Desc = "获取审批人", Url = "WeiXinAppService.svc/GetATEmpAttendanceEventApprove?EmpId={EmpId}", Get = "DeptId={DeptId}", Post = "", Return = "BaseResult<HREmployee>", ReturnType = "HREmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetATEmpAttendanceEventApprove?EmpId={EmpId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetATEmpAttendanceEventApprove(string EmpId);

        [ServiceContractDescription(Name = "计算相隔天数刨去节假日", Desc = "计算相隔天数刨去节假日", Url = "WeiXinAppService.svc/GetATEmpAttendanceEventDayCount?sd={sd}&ed={ed}", Get = "sd={sd}&ed={ed}", Post = "", Return = "float", ReturnType = "float")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetATEmpAttendanceEventDayCount?sd={sd}&ed={ed}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetATEmpAttendanceEventDayCount(string sd, string ed);

        [ServiceContractDescription(Name = "计算相隔小时数", Desc = "计算相隔小时数", Url = "WeiXinAppService.svc/GetATEmpAttendanceEventHourCount?sd={sd}&ed={ed}", Get = "sd={sd}&ed={ed}", Post = "", Return = "float", ReturnType = "float")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetATEmpAttendanceEventHourCount?sd={sd}&ed={ed}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetATEmpAttendanceEventHourCount(string sd, string ed);

        [ServiceContractDescription(Name = "通过日期范围和考勤类型获取登录者考勤记录", Desc = "通过日期范围和考勤类型获取登录者考勤记录", Url = "WeiXinAppService.svc/SearchATEmpSignLog?sd={sd}&ed={ed}&type={type}", Get = "sd={sd}&ed={ed}&type={type}", Post = "", Return = "BaseResultList<ATEmpAttendanceEventLog>", ReturnType = "ATEmpAttendanceEventLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchATEmpSignLog?sd={sd}&ed={ed}&type={type}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SearchATEmpSignLog(string sd, string ed, int type);

        [ServiceContractDescription(Name = "通过开始日期和考勤类型获取登录者考勤记录", Desc = "通过开始日期和考勤类型获取登录者考勤记录", Url = "WeiXinAppService.svc/SearchATEmpSignLogByLimit?ed={ed}&limit={limit}&type={type}", Get = "ed={ed}&limit={limit}&type={type}", Post = "", Return = "BaseResultList<SignLog>", ReturnType = "SignLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchATEmpSignLogByLimit?ed={ed}&limit={limit}&type={type}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SearchATEmpSignLogByLimit(string ed, int limit, int type);

        [ServiceContractDescription(Name = "查询部门内的考勤记录", Desc = "查询部门内的考勤记录", Url = "WeiXinAppService.svc/SearchATOtherSignLogByLimit?ed={ed}&limit={limit}&empid={empid}&type={type}", Get = "ed={ed}&limit={limit}&empid={empid}&type={type}", Post = "", Return = "BaseResultList<SignLog>", ReturnType = "SignLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchATOtherSignLogByLimit?ed={ed}&limit={limit}&empid={empid}&type={type}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SearchATOtherSignLogByLimit(string ed, int limit, string empid, int type);

        [ServiceContractDescription(Name = "查询部门内的考勤记录", Desc = "查询部门内的考勤记录", Url = "WeiXinAppService.svc/SearchATMyApplyAllLogByLimit?sd={sd}&ed={ed}&pageindex={pageindex}&limit={limit}&apsid={apsid}&type={type}", Get = "sd={sd}&ed={ed}&pageindex={pageindex}&limit={limit}&apsid={apsid}&type={type}", Post = "", Return = "BaseResultList<SignLog>", ReturnType = "SignLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchATMyApplyAllLogByLimit?sd={sd}&ed={ed}&pageindex={pageindex}&limit={limit}&apsid={apsid}&type={type}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SearchATMyApplyAllLogByLimit(string sd, string ed, int pageindex, int limit, string apsid, int type);

        [ServiceContractDescription(Name = "查询部门内的考勤记录", Desc = "查询部门内的考勤记录", Url = "WeiXinAppService.svc/SearchATMyApprovalAllLogByEmpId?sd={sd}&ed={ed}&pageindex={pageindex}&limit={limit}&apsid={apsid}&typeidlist={typeidlist}&empid={empid}", Get = "sd={sd}&ed={ed}&pageindex={pageindex}&limit={limit}&apsid={apsid}&typeidlist={typeidlist}&empid={empid}", Post = "", Return = "BaseResultList<SignLog>", ReturnType = "SignLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchATMyApprovalAllLogByEmpId?sd={sd}&ed={ed}&pageindex={pageindex}&limit={limit}&apsid={apsid}&typeidlist={typeidlist}&empid={empid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SearchATMyApprovalAllLogByEmpId(string sd, string ed, int pageindex, int limit, string apsid, string typeidlist, string empid);

        [ServiceContractDescription(Name = "批量审批", Desc = "批量审批", Url = "WeiXinAppService.svc/ApprovalATApplyEventLog", Get = "", Post = "ApprovalATApplyEventLog", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ApprovalATApplyEventLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ApprovalATApplyEventLog(string memo, string[] eventlogids, int type);

        [ServiceContractDescription(Name = "查询月考勤", Desc = "查询月考勤", Url = "WeiXinAppService.svc/GetEmpMonthLog?EmpId={EmpId}&MonthCode={MonthCode}", Get = "EmpId={EmpId}&MonthCode={MonthCode}", Post = "", Return = "BaseResultList<ATEmpMonthLog>", ReturnType = "ATEmpMonthLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetEmpMonthLog?EmpId={EmpId}&MonthCode={MonthCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetEmpMonthLog(long EmpId, string MonthCode);

        [ServiceContractDescription(Name = "查询周考勤0个人，1部门，2公司", Desc = "查询周考勤0个人，1部门，2公司", Url = "WeiXinAppService.svc/GetATEmpListWeekLog?Type={Type}&StartDate={StartDate}&EndDate={EndDate}", Get = "Type={Type}&StartDate={StartDate}&EndDate={EndDate}", Post = "", Return = "BaseResultList<ATEmpListWeekLog>", ReturnType = "ATEmpListWeekLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetATEmpListWeekLog?Type={Type}&StartDate={StartDate}&EndDate={EndDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetATEmpListWeekLog(long Type, string StartDate, string EndDate);

        #endregion

        #region ATEmpAttendanceEventParaSettings

        [ServiceContractDescription(Name = "新增员工考勤事件参数设置表", Desc = "新增员工考勤事件参数设置表", Url = "WeiXinAppService.svc/ST_UDTO_AddATEmpAttendanceEventParaSettings", Get = "", Post = "ATEmpAttendanceEventParaSettings", Return = "BaseResultDataValue", ReturnType = "ATEmpAttendanceEventParaSettings")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATEmpAttendanceEventParaSettings", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventParaSettings(ATEmpAttendanceEventParaSettings entity);

        [ServiceContractDescription(Name = "修改员工考勤事件参数设置表", Desc = "修改员工考勤事件参数设置表", Url = "WeiXinAppService.svc/ST_UDTO_UpdateATEmpAttendanceEventParaSettings", Get = "", Post = "ATEmpAttendanceEventParaSettings", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateATEmpAttendanceEventParaSettings", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateATEmpAttendanceEventParaSettings(ATEmpAttendanceEventParaSettings entity);

        [ServiceContractDescription(Name = "修改员工考勤事件参数设置表指定的属性", Desc = "修改员工考勤事件参数设置表指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateATEmpAttendanceEventParaSettingsByField", Get = "", Post = "ATEmpAttendanceEventParaSettings", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateATEmpAttendanceEventParaSettingsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateATEmpAttendanceEventParaSettingsByField(ATEmpAttendanceEventParaSettings entity, string fields);

        [ServiceContractDescription(Name = "删除员工考勤事件参数设置表", Desc = "删除员工考勤事件参数设置表", Url = "WeiXinAppService.svc/ST_UDTO_DelATEmpAttendanceEventParaSettings?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelATEmpAttendanceEventParaSettings?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelATEmpAttendanceEventParaSettings(long id);

        [ServiceContractDescription(Name = "查询员工考勤事件参数设置表", Desc = "查询员工考勤事件参数设置表", Url = "WeiXinAppService.svc/ST_UDTO_SearchATEmpAttendanceEventParaSettings", Get = "", Post = "ATEmpAttendanceEventParaSettings", Return = "BaseResultList<ATEmpAttendanceEventParaSettings>", ReturnType = "ListATEmpAttendanceEventParaSettings")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATEmpAttendanceEventParaSettings", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventParaSettings(ATEmpAttendanceEventParaSettings entity);

        [ServiceContractDescription(Name = "查询员工考勤事件参数设置表(HQL)", Desc = "查询员工考勤事件参数设置表(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchATEmpAttendanceEventParaSettingsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ATEmpAttendanceEventParaSettings>", ReturnType = "ListATEmpAttendanceEventParaSettings")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATEmpAttendanceEventParaSettingsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventParaSettingsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询员工考勤事件参数设置表", Desc = "通过主键ID查询员工考勤事件参数设置表", Url = "WeiXinAppService.svc/ST_UDTO_SearchATEmpAttendanceEventParaSettingsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ATEmpAttendanceEventParaSettings>", ReturnType = "ATEmpAttendanceEventParaSettings")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATEmpAttendanceEventParaSettingsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventParaSettingsById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询员工考勤事件参数设置表", Desc = "通过主键ID查询员工考勤事件参数设置表", Url = "WeiXinAppService.svc/ST_UDTO_SearchATEmpAttendanceEventParaSettingsByDeptId?deptid={deptid}&isincludesubdept={isincludesubdept}", Get = "deptid={deptid}&isincludesubdept={isincludesubdept}", Post = "", Return = "BaseResultList<ATEmpAttendanceEventParaSettings>", ReturnType = "ATEmpAttendanceEventParaSettings")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATEmpAttendanceEventParaSettingsByDeptId?deptid={deptid}&isincludesubdept={isincludesubdept}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventParaSettingsByDeptId(string deptid, bool isincludesubdept);
        #endregion

        #region ATTransportation

        [ServiceContractDescription(Name = "新增交通工具", Desc = "新增交通工具", Url = "WeiXinAppService.svc/ST_UDTO_AddATTransportation", Get = "", Post = "ATTransportation", Return = "BaseResultDataValue", ReturnType = "ATTransportation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATTransportation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATTransportation(ATTransportation entity);

        [ServiceContractDescription(Name = "修改交通工具", Desc = "修改交通工具", Url = "WeiXinAppService.svc/ST_UDTO_UpdateATTransportation", Get = "", Post = "ATTransportation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateATTransportation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateATTransportation(ATTransportation entity);

        [ServiceContractDescription(Name = "修改交通工具指定的属性", Desc = "修改交通工具指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateATTransportationByField", Get = "", Post = "ATTransportation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateATTransportationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateATTransportationByField(ATTransportation entity, string fields);

        [ServiceContractDescription(Name = "删除交通工具", Desc = "删除交通工具", Url = "WeiXinAppService.svc/ST_UDTO_DelATTransportation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelATTransportation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelATTransportation(long id);

        [ServiceContractDescription(Name = "查询交通工具", Desc = "查询交通工具", Url = "WeiXinAppService.svc/ST_UDTO_SearchATTransportation", Get = "", Post = "ATTransportation", Return = "BaseResultList<ATTransportation>", ReturnType = "ListATTransportation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATTransportation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATTransportation(ATTransportation entity);

        [ServiceContractDescription(Name = "查询交通工具(HQL)", Desc = "查询交通工具(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchATTransportationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ATTransportation>", ReturnType = "ListATTransportation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATTransportationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATTransportationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询交通工具", Desc = "通过主键ID查询交通工具", Url = "WeiXinAppService.svc/ST_UDTO_SearchATTransportationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ATTransportation>", ReturnType = "ATTransportation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATTransportationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATTransportationById(long id, string fields, bool isPlanish);
        #endregion

        #region BAccountType

        [ServiceContractDescription(Name = "新增应用系统账户类型", Desc = "新增应用系统账户类型", Url = "WeiXinAppService.svc/ST_UDTO_AddBAccountType", Get = "", Post = "BAccountType", Return = "BaseResultDataValue", ReturnType = "BAccountType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBAccountType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBAccountType(BAccountType entity);

        [ServiceContractDescription(Name = "修改应用系统账户类型", Desc = "修改应用系统账户类型", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBAccountType", Get = "", Post = "BAccountType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBAccountType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBAccountType(BAccountType entity);

        [ServiceContractDescription(Name = "修改应用系统账户类型指定的属性", Desc = "修改应用系统账户类型指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBAccountTypeByField", Get = "", Post = "BAccountType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBAccountTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBAccountTypeByField(BAccountType entity, string fields);

        [ServiceContractDescription(Name = "删除应用系统账户类型", Desc = "删除应用系统账户类型", Url = "WeiXinAppService.svc/ST_UDTO_DelBAccountType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBAccountType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBAccountType(long id);

        [ServiceContractDescription(Name = "查询应用系统账户类型", Desc = "查询应用系统账户类型", Url = "WeiXinAppService.svc/ST_UDTO_SearchBAccountType", Get = "", Post = "BAccountType", Return = "BaseResultList<BAccountType>", ReturnType = "ListBAccountType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAccountType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAccountType(BAccountType entity);

        [ServiceContractDescription(Name = "查询应用系统账户类型(HQL)", Desc = "查询应用系统账户类型(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchBAccountTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BAccountType>", ReturnType = "ListBAccountType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAccountTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAccountTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询应用系统账户类型", Desc = "通过主键ID查询应用系统账户类型", Url = "WeiXinAppService.svc/ST_UDTO_SearchBAccountTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BAccountType>", ReturnType = "BAccountType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAccountTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAccountTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BIcons

        [ServiceContractDescription(Name = "新增图标头像", Desc = "新增图标头像", Url = "WeiXinAppService.svc/ST_UDTO_AddBIcons", Get = "", Post = "BIcons", Return = "BaseResultDataValue", ReturnType = "BIcons")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBIcons", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBIcons(BIcons entity);

        [ServiceContractDescription(Name = "修改图标头像", Desc = "修改图标头像", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBIcons", Get = "", Post = "BIcons", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBIcons", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBIcons(BIcons entity);

        [ServiceContractDescription(Name = "修改图标头像指定的属性", Desc = "修改图标头像指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBIconsByField", Get = "", Post = "BIcons", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBIconsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBIconsByField(BIcons entity, string fields);

        [ServiceContractDescription(Name = "删除图标头像", Desc = "删除图标头像", Url = "WeiXinAppService.svc/ST_UDTO_DelBIcons?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBIcons?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBIcons(long id);

        [ServiceContractDescription(Name = "查询图标头像", Desc = "查询图标头像", Url = "WeiXinAppService.svc/ST_UDTO_SearchBIcons", Get = "", Post = "BIcons", Return = "BaseResultList<BIcons>", ReturnType = "ListBIcons")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBIcons", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBIcons(BIcons entity);

        [ServiceContractDescription(Name = "查询图标头像(HQL)", Desc = "查询图标头像(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchBIconsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BIcons>", ReturnType = "ListBIcons")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBIconsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBIconsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询图标头像", Desc = "通过主键ID查询图标头像", Url = "WeiXinAppService.svc/ST_UDTO_SearchBIconsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BIcons>", ReturnType = "BIcons")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBIconsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBIconsById(long id, string fields, bool isPlanish);
        #endregion

        #region BWeiXinEmpLink

        [ServiceContractDescription(Name = "新增微信账户绑定员工表", Desc = "新增微信账户绑定员工表", Url = "WeiXinAppService.svc/ST_UDTO_AddBWeiXinEmpLink", Get = "", Post = "BWeiXinEmpLink", Return = "BaseResultDataValue", ReturnType = "BWeiXinEmpLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBWeiXinEmpLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBWeiXinEmpLink(BWeiXinEmpLink entity);

        [ServiceContractDescription(Name = "修改微信账户绑定员工表", Desc = "修改微信账户绑定员工表", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBWeiXinEmpLink", Get = "", Post = "BWeiXinEmpLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBWeiXinEmpLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBWeiXinEmpLink(BWeiXinEmpLink entity);

        [ServiceContractDescription(Name = "修改微信账户绑定员工表指定的属性", Desc = "修改微信账户绑定员工表指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBWeiXinEmpLinkByField", Get = "", Post = "BWeiXinEmpLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBWeiXinEmpLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBWeiXinEmpLinkByField(BWeiXinEmpLink entity, string fields);

        [ServiceContractDescription(Name = "删除微信账户绑定员工表", Desc = "删除微信账户绑定员工表", Url = "WeiXinAppService.svc/ST_UDTO_DelBWeiXinEmpLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBWeiXinEmpLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBWeiXinEmpLink(long id);

        [ServiceContractDescription(Name = "查询微信账户绑定员工表", Desc = "查询微信账户绑定员工表", Url = "WeiXinAppService.svc/ST_UDTO_SearchBWeiXinEmpLink", Get = "", Post = "BWeiXinEmpLink", Return = "BaseResultList<BWeiXinEmpLink>", ReturnType = "ListBWeiXinEmpLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinEmpLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinEmpLink(BWeiXinEmpLink entity);

        [ServiceContractDescription(Name = "查询微信账户绑定员工表(HQL)", Desc = "查询微信账户绑定员工表(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchBWeiXinEmpLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BWeiXinEmpLink>", ReturnType = "ListBWeiXinEmpLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinEmpLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinEmpLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询微信账户绑定员工表", Desc = "通过主键ID查询微信账户绑定员工表", Url = "WeiXinAppService.svc/ST_UDTO_SearchBWeiXinEmpLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BWeiXinEmpLink>", ReturnType = "BWeiXinEmpLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinEmpLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinEmpLinkById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过微信和员工帐号绑定", Desc = "通过微信和员工帐号绑定", Url = "WeiXinAppService.svc/ST_UDTO_AddBWeiXinEmpLinkByUserAccount?strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}", Get = "strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}", Post = "", Return = "BaseResultList<BWeiXinEmpLink>", ReturnType = "BWeiXinEmpLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBWeiXinEmpLinkByUserAccount?strUserAccount={strUserAccount}&strPassWord={strPassWord}&isValidate={isValidate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBWeiXinEmpLinkByUserAccount(string strUserAccount, string strPassWord, bool isValidate);
        #endregion

        #region BWeiXinAccount

        [ServiceContractDescription(Name = "新增微信账户", Desc = "新增微信账户", Url = "WeiXinAppService.svc/ST_UDTO_AddBWeiXinAccount", Get = "", Post = "BWeiXinAccount", Return = "BaseResultDataValue", ReturnType = "BWeiXinAccount")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBWeiXinAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBWeiXinAccount(BWeiXinAccount entity);

        [ServiceContractDescription(Name = "修改微信账户", Desc = "修改微信账户", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBWeiXinAccount", Get = "", Post = "BWeiXinAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBWeiXinAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBWeiXinAccount(BWeiXinAccount entity);

        [ServiceContractDescription(Name = "修改微信账户指定的属性", Desc = "修改微信账户指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBWeiXinAccountByField", Get = "", Post = "BWeiXinAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBWeiXinAccountByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBWeiXinAccountByField(BWeiXinAccount entity, string fields);

        [ServiceContractDescription(Name = "删除微信账户", Desc = "删除微信账户", Url = "WeiXinAppService.svc/ST_UDTO_DelBWeiXinAccount?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBWeiXinAccount?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBWeiXinAccount(long id);

        [ServiceContractDescription(Name = "查询微信账户", Desc = "查询微信账户", Url = "WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccount", Get = "", Post = "BWeiXinAccount", Return = "BaseResultList<BWeiXinAccount>", ReturnType = "ListBWeiXinAccount")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinAccount(BWeiXinAccount entity);

        [ServiceContractDescription(Name = "查询微信账户(HQL)", Desc = "查询微信账户(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BWeiXinAccount>", ReturnType = "ListBWeiXinAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询微信账户(HQL)", Desc = "查询微信账户(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_GetBWeiXinAccountByWeiXinAccount?WeiXinAccount={WeiXinAccount}&fields={fields}", Get = "WeiXinAccount={WeiXinAccount}&fields={fields}", Post = "", Return = "BaseResultList<BWeiXinAccount>", ReturnType = "ListBWeiXinAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetBWeiXinAccountByWeiXinAccount?WeiXinAccount={WeiXinAccount}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetBWeiXinAccountByWeiXinAccount(long WeiXinAccount, string fields);

        [ServiceContractDescription(Name = "通过主键ID查询微信账户", Desc = "通过主键ID查询微信账户", Url = "WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BWeiXinAccount>", ReturnType = "BWeiXinAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinAccountById(long id, string fields, bool isPlanish);
        #endregion

        #region BWeiXinUserGroup

        [ServiceContractDescription(Name = "新增微信用户组", Desc = "新增微信用户组", Url = "WeiXinAppService.svc/ST_UDTO_AddBWeiXinUserGroup", Get = "", Post = "BWeiXinUserGroup", Return = "BaseResultDataValue", ReturnType = "BWeiXinUserGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBWeiXinUserGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBWeiXinUserGroup(BWeiXinUserGroup entity);

        [ServiceContractDescription(Name = "修改微信用户组", Desc = "修改微信用户组", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBWeiXinUserGroup", Get = "", Post = "BWeiXinUserGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBWeiXinUserGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBWeiXinUserGroup(BWeiXinUserGroup entity);

        [ServiceContractDescription(Name = "修改微信用户组指定的属性", Desc = "修改微信用户组指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBWeiXinUserGroupByField", Get = "", Post = "BWeiXinUserGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBWeiXinUserGroupByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBWeiXinUserGroupByField(BWeiXinUserGroup entity, string fields);

        [ServiceContractDescription(Name = "删除微信用户组", Desc = "删除微信用户组", Url = "WeiXinAppService.svc/ST_UDTO_DelBWeiXinUserGroup?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBWeiXinUserGroup?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBWeiXinUserGroup(long id);

        [ServiceContractDescription(Name = "查询微信用户组", Desc = "查询微信用户组", Url = "WeiXinAppService.svc/ST_UDTO_SearchBWeiXinUserGroup", Get = "", Post = "BWeiXinUserGroup", Return = "BaseResultList<BWeiXinUserGroup>", ReturnType = "ListBWeiXinUserGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinUserGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinUserGroup(BWeiXinUserGroup entity);

        [ServiceContractDescription(Name = "查询微信用户组(HQL)", Desc = "查询微信用户组(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchBWeiXinUserGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BWeiXinUserGroup>", ReturnType = "ListBWeiXinUserGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinUserGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinUserGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询微信用户组", Desc = "通过主键ID查询微信用户组", Url = "WeiXinAppService.svc/ST_UDTO_SearchBWeiXinUserGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BWeiXinUserGroup>", ReturnType = "BWeiXinUserGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinUserGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinUserGroupById(long id, string fields, bool isPlanish);
        #endregion

        #region SServiceClient

        [ServiceContractDescription(Name = "新增平台客户", Desc = "新增平台客户", Url = "WeiXinAppService.svc/ST_UDTO_AddSServiceClient", Get = "", Post = "SServiceClient", Return = "BaseResultDataValue", ReturnType = "SServiceClient")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSServiceClient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSServiceClient(SServiceClient entity);

        [ServiceContractDescription(Name = "修改平台客户", Desc = "修改平台客户", Url = "WeiXinAppService.svc/ST_UDTO_UpdateSServiceClient", Get = "", Post = "SServiceClient", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSServiceClient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSServiceClient(SServiceClient entity);

        [ServiceContractDescription(Name = "修改平台客户指定的属性", Desc = "修改平台客户指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateSServiceClientByField", Get = "", Post = "SServiceClient", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSServiceClientByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSServiceClientByField(SServiceClient entity, string fields);

        [ServiceContractDescription(Name = "删除平台客户", Desc = "删除平台客户", Url = "WeiXinAppService.svc/ST_UDTO_DelSServiceClient?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSServiceClient?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSServiceClient(long id);

        [ServiceContractDescription(Name = "查询平台客户", Desc = "查询平台客户", Url = "WeiXinAppService.svc/ST_UDTO_SearchSServiceClient", Get = "", Post = "SServiceClient", Return = "BaseResultList<SServiceClient>", ReturnType = "ListSServiceClient")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSServiceClient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSServiceClient(SServiceClient entity);

        [ServiceContractDescription(Name = "查询平台客户(HQL)", Desc = "查询平台客户(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchSServiceClientByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SServiceClient>", ReturnType = "ListSServiceClient")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSServiceClientByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSServiceClientByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台客户", Desc = "通过主键ID查询平台客户", Url = "WeiXinAppService.svc/ST_UDTO_SearchSServiceClientById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SServiceClient>", ReturnType = "SServiceClient")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSServiceClientById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSServiceClientById(long id, string fields, bool isPlanish);
        #endregion

        #region SServiceClientlevel

        [ServiceContractDescription(Name = "新增平台客户级别：普通客户、试用客户、付费客户、VIP客户。", Desc = "新增平台客户级别：普通客户、试用客户、付费客户、VIP客户。", Url = "WeiXinAppService.svc/ST_UDTO_AddSServiceClientlevel", Get = "", Post = "SServiceClientlevel", Return = "BaseResultDataValue", ReturnType = "SServiceClientlevel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSServiceClientlevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSServiceClientlevel(SServiceClientlevel entity);

        [ServiceContractDescription(Name = "修改平台客户级别：普通客户、试用客户、付费客户、VIP客户。", Desc = "修改平台客户级别：普通客户、试用客户、付费客户、VIP客户。", Url = "WeiXinAppService.svc/ST_UDTO_UpdateSServiceClientlevel", Get = "", Post = "SServiceClientlevel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSServiceClientlevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSServiceClientlevel(SServiceClientlevel entity);

        [ServiceContractDescription(Name = "修改平台客户级别：普通客户、试用客户、付费客户、VIP客户。指定的属性", Desc = "修改平台客户级别：普通客户、试用客户、付费客户、VIP客户。指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateSServiceClientlevelByField", Get = "", Post = "SServiceClientlevel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSServiceClientlevelByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSServiceClientlevelByField(SServiceClientlevel entity, string fields);

        [ServiceContractDescription(Name = "删除平台客户级别：普通客户、试用客户、付费客户、VIP客户。", Desc = "删除平台客户级别：普通客户、试用客户、付费客户、VIP客户。", Url = "WeiXinAppService.svc/ST_UDTO_DelSServiceClientlevel?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSServiceClientlevel?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSServiceClientlevel(long id);

        [ServiceContractDescription(Name = "查询平台客户级别：普通客户、试用客户、付费客户、VIP客户。", Desc = "查询平台客户级别：普通客户、试用客户、付费客户、VIP客户。", Url = "WeiXinAppService.svc/ST_UDTO_SearchSServiceClientlevel", Get = "", Post = "SServiceClientlevel", Return = "BaseResultList<SServiceClientlevel>", ReturnType = "ListSServiceClientlevel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSServiceClientlevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSServiceClientlevel(SServiceClientlevel entity);

        [ServiceContractDescription(Name = "查询平台客户级别：普通客户、试用客户、付费客户、VIP客户。(HQL)", Desc = "查询平台客户级别：普通客户、试用客户、付费客户、VIP客户。(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchSServiceClientlevelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SServiceClientlevel>", ReturnType = "ListSServiceClientlevel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSServiceClientlevelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSServiceClientlevelByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台客户级别：普通客户、试用客户、付费客户、VIP客户。", Desc = "通过主键ID查询平台客户级别：普通客户、试用客户、付费客户、VIP客户。", Url = "WeiXinAppService.svc/ST_UDTO_SearchSServiceClientlevelById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SServiceClientlevel>", ReturnType = "SServiceClientlevel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSServiceClientlevelById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSServiceClientlevelById(long id, string fields, bool isPlanish);
        #endregion

        #region jsapi
        [ServiceContractDescription(Name = "获取jsapi签名", Desc = "获取jsapi签名", Url = "WeiXinAppService.svc/GetJSAPISignature?noncestr={noncestr}&timestamp={timestamp}&url={url}", Post = "", Return = "BaseResultDataValue<string>", ReturnType = "string")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetJSAPISignature?noncestr={noncestr}&timestamp={timestamp}&url={url}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetJSAPISignature(string noncestr, string timestamp, string url);
        #endregion

        #region 导出考勤统计
        [ServiceContractDescription(Name = "获取公司所有员工的考勤统计信息", Desc = "获取公司所有员工的考勤统计信息", Url = "WeiXinAppService.svc/GetAllMonthLogCountList?monthCode={monthCode}&wagesDays={wagesDays}&punch={punch}", Get = "monthCode={monthCode}&wagesDays={wagesDays}&punch={punch}", Post = "", Return = "BaseResultList<ATEmpMonthLogCount>", ReturnType = "ATEmpMonthLogCount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAllMonthLogCountList?monthCode={monthCode}&wagesDays={wagesDays}&punch={punch}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetAllMonthLogCountList(string monthCode, int wagesDays, int punch);

        [ServiceContractDescription(Name = "导出公司所有的员工的某一个月的考勤统计信息", Desc = "导出公司所有的员工的某一个月的考勤统计信息", Url = "WeiXinAppService.svc/SC_UDTO_DownLoadExportExcelOfAllMonthLogCount?monthCode={monthCode}&operateType={operateType}&wagesDays={wagesDays}&punch={punch}", Get = "monthCode={monthCode}&operateType={operateType}&wagesDays={wagesDays}&punch={punch}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_DownLoadExportExcelOfAllMonthLogCount?monthCode={monthCode}&operateType={operateType}&wagesDays={wagesDays}&punch={punch}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]

        Stream SC_UDTO_DownLoadExportExcelOfAllMonthLogCount(long operateType, string monthCode, int wagesDays, int punch);

        [ServiceContractDescription(Name = "获取员工考勤统计清单信息", Desc = "获取员工考勤统计清单信息", Url = "WeiXinAppService.svc/SC_UDTO_GetATEmpAttendanceEventLogDetailList?searchType={searchType}&attypeId={attypeId}&deptId={deptId}&isGetSubDept={isGetSubDept}&empId={empId}&startDate={startDate}&endDate={endDate}&approveStatusID={approveStatusID}&page={page}&limit={limit}&sort={sort}", Get = "searchType={searchType}&attypeId={attypeId}&deptId={deptId}&isGetSubDept={isGetSubDept}&empId={empId}&startDate={startDate}&endDate={endDate}&approveStatusID={approveStatusID}&page={page}&limit={limit}&sort={sort}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_GetATEmpAttendanceEventLogDetailList?searchType={searchType}&attypeId={attypeId}&deptId={deptId}&isGetSubDept={isGetSubDept}&empId={empId}&startDate={startDate}&endDate={endDate}&approveStatusID={approveStatusID}&page={page}&limit={limit}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_GetATEmpAttendanceEventLogDetailList(long searchType, string attypeId, string deptId, bool isGetSubDept, string empId, string startDate, string endDate, long approveStatusID, int page, int limit, string sort);

        [ServiceContractDescription(Name = "导出员工考勤统计清单信息", Desc = "导出员工考勤统计清单信息", Url = "WeiXinAppService.svc/SC_UDTO_DownLoadExportExcelOfATEmpAttendanceEventLogDetail?searchType={searchType}&operateType={operateType}&attypeId={attypeId}&deptId={deptId}&isGetSubDept={isGetSubDept}&empId={empId}&startDate={startDate}&endDate={endDate}&approveStatusID={approveStatusID}", Get = "searchType={searchType}&operateType={operateType}&attypeId={attypeId}&deptId={deptId}&isGetSubDept={isGetSubDept}&empId={empId}&startDate={startDate}&endDate={endDate}&approveStatusID={approveStatusID}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_DownLoadExportExcelOfATEmpAttendanceEventLogDetail?searchType={searchType}&operateType={operateType}&attypeId={attypeId}&deptId={deptId}&isGetSubDept={isGetSubDept}&empId={empId}&startDate={startDate}&endDate={endDate}&approveStatusID={approveStatusID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream SC_UDTO_DownLoadExportExcelOfATEmpAttendanceEventLogDetail(long operateType, long searchType, string attypeId, string deptId, bool isGetSubDept, string empId, string startDate, string endDate, long approveStatusID);

        [ServiceContractDescription(Name = "导出员工考勤统计打卡清单信息", Desc = "导出员工考勤统计打卡清单信息", Url = "WeiXinAppService.svc/SC_UDTO_GetATEmpSignInfoDetailList?filterType={filterType}&deptId={deptId}&isGetSubDept={isGetSubDept}&empId={empId}&startDate={startDate}&endDate={endDate}&page={page}&limit={limit}&sort={sort}", Get = "filterType={filterType}&deptId={deptId}&isGetSubDept={isGetSubDept}&empId={empId}&startDate={startDate}&endDate={endDate}&page={page}&limit={limit}&sort={sort}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_GetATEmpSignInfoDetailList?filterType={filterType}&deptId={deptId}&isGetSubDept={isGetSubDept}&empId={empId}&startDate={startDate}&endDate={endDate}&page={page}&limit={limit}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_GetATEmpSignInfoDetailList(string filterType, string deptId, bool isGetSubDept, string empId, string startDate, string endDate, int page, int limit, string sort);

        [ServiceContractDescription(Name = "导出员工考勤统计打卡清单信息", Desc = "导出员工考勤统计打卡清单信息", Url = "WeiXinAppService.svc/SC_UDTO_DownLoadGetExportExcelOfATEmpSignInfoDetail?operateType={operateType}&filterType={filterType}&deptId={deptId}&isGetSubDept={isGetSubDept}&empId={empId}&startDate={startDate}&endDate={endDate}", Get = "operateType={operateType}&filterType={filterType}&deptId={deptId}&isGetSubDept={isGetSubDept}&empId={empId}&startDate={startDate}&endDate={endDate}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_DownLoadGetExportExcelOfATEmpSignInfoDetail?operateType={operateType}&filterType={filterType}&deptId={deptId}&isGetSubDept={isGetSubDept}&empId={empId}&startDate={startDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream SC_UDTO_DownLoadGetExportExcelOfATEmpSignInfoDetail(long operateType, string filterType, string deptId, bool isGetSubDept, string empId, string startDate, string endDate);
        #endregion

        #region ATHolidaySetting

        [ServiceContractDescription(Name = "新增节假日设置", Desc = "新增节假日设置", Url = "WeiXinAppService.svc/ST_UDTO_AddATHolidaySetting", Get = "", Post = "ATHolidaySetting", Return = "BaseResultDataValue", ReturnType = "ATHolidaySetting")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddATHolidaySetting", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddATHolidaySetting(ATHolidaySetting entity);

        [ServiceContractDescription(Name = "修改节假日设置", Desc = "修改节假日设置", Url = "WeiXinAppService.svc/ST_UDTO_UpdateATHolidaySetting", Get = "", Post = "ATHolidaySetting", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateATHolidaySetting", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateATHolidaySetting(ATHolidaySetting entity);

        [ServiceContractDescription(Name = "修改节假日设置指定的属性", Desc = "修改节假日设置指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateATHolidaySettingByField", Get = "", Post = "ATHolidaySetting", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateATHolidaySettingByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateATHolidaySettingByField(ATHolidaySetting entity, string fields);

        [ServiceContractDescription(Name = "删除节假日设置", Desc = "删除节假日设置", Url = "WeiXinAppService.svc/ST_UDTO_DelATHolidaySetting?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelATHolidaySetting?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelATHolidaySetting(long id);

        [ServiceContractDescription(Name = "依idStr删除节假日设置", Desc = "依idStr删除节假日设置", Url = "WeiXinAppService.svc/ST_UDTO_DelATHolidaySettingByIdStr?idStr={idStr}", Get = "idStr={idStr}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelATHolidaySettingByIdStr?idStr={idStr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelATHolidaySettingByIdStr(string idStr);

        [ServiceContractDescription(Name = "获取某年的某一月共有多少个工作日天数(工资日)", Desc = "获取某年的某一月共有多少个工作日天数(工资日)", Url = "WeiXinAppService.svc/ST_UDTO_GetWorkDaysOfOneMonth?year={year}&month={month}", Get = "year={year}&month={month}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetWorkDaysOfOneMonth?year={year}&month={month}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetWorkDaysOfOneMonth(int year, int month);

        [ServiceContractDescription(Name = "查询节假日设置", Desc = "查询节假日设置", Url = "WeiXinAppService.svc/ST_UDTO_SearchATHolidaySetting", Get = "", Post = "ATHolidaySetting", Return = "BaseResultList<ATHolidaySetting>", ReturnType = "ListATHolidaySetting")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATHolidaySetting", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATHolidaySetting(ATHolidaySetting entity);

        [ServiceContractDescription(Name = "查询节假日设置(HQL)", Desc = "查询节假日设置(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchATHolidaySettingByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ATHolidaySetting>", ReturnType = "ListATHolidaySetting")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATHolidaySettingByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATHolidaySettingByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "(通过年和月)查询节假日设置", Desc = "(通过年和月)查询节假日设置", Url = "WeiXinAppService.svc/ST_UDTO_SearchATHolidaySettingByYearAndMonth?year={year}&month={month}", Get = "year={year}&month={month}", Post = "", Return = "BaseResultList<ATHolidaySetting>", ReturnType = "ListATHolidaySetting")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATHolidaySettingByYearAndMonth?year={year}&month={month}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATHolidaySettingByYearAndMonth(int year, int month);

        [ServiceContractDescription(Name = "通过主键ID查询节假日设置", Desc = "通过主键ID查询节假日设置", Url = "WeiXinAppService.svc/ST_UDTO_SearchATHolidaySettingById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ATHolidaySetting>", ReturnType = "ATHolidaySetting")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchATHolidaySettingById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchATHolidaySettingById(long id, string fields, bool isPlanish);
        #endregion

        [ServiceContractDescription(Name = "获取微信新闻永久素材列表", Desc = "获取微信新闻永久素材列表", Url = "WeiXinAppService.svc/GetPermanentMediaNewsList?page={page}&limit={limit}", Get = "page={page}&limit={limit}", Post = "", Return = "BaseResultList<ATApproveStatus>", ReturnType = "ATApproveStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPermanentMediaNewsList?page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetPermanentMediaNewsList(int page, int limit);
    }
}
