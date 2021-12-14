using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.BusinessObject;
using ZhiFang.WeiXin.Entity.ViewObject.Request;

namespace ZhiFang.WeiXin.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.WeiXinAppDoctService")]
    public interface IZhiFangWeiXinAppDoctService
    {
        [ServiceContractDescription(Name = "获取套餐", Desc = "获取套餐", Url = "ZhiFangWeiXinAppDoctService.svc/DS_UDTO_SearchOSTestItemByAreaID?page={page}&limit={limit}&where={where}&sort={sort}", Get = "page={page}&limit={limit}&where={where}&sort={sort}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DS_UDTO_SearchOSTestItemByAreaID?page={page}&limit={limit}&where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DS_UDTO_SearchOSTestItemByAreaID(int page, int limit, string where, string sort);

        [ServiceContractDescription(Name = "获取特推套餐", Desc = "获取特推套餐", Url = "ZhiFangWeiXinAppDoctService.svc/DS_UDTO_SearchOSRecommendationItemByAreaID?page={page}&limit={limit}&where={where}&sort={sort}", Get = "page={page}&limit={limit}&where={where}&sort={sort}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DS_UDTO_SearchOSRecommendationItemByAreaID?page={page}&limit={limit}&where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DS_UDTO_SearchOSRecommendationItemByAreaID(int page, int limit, string where, string sort);

        [ServiceContractDescription(Name = "新增医嘱单", Desc = "新增医嘱单", Url = "ZhiFangWeiXinAppDoctService.svc/DS_UDTO_SaveOSDoctorOrderForm", Get = "", Post = "PDict", Return = "BaseResultDataValue", ReturnType = "PDict")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DS_UDTO_SaveOSDoctorOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DS_UDTO_SaveOSDoctorOrderForm(OSDoctorOrderFormVO entity);

        [ServiceContractDescription(Name = "获取医嘱单信息", Desc = "获取医生医嘱单信息", Url = "ZhiFangWeiXinAppDoctService.svc/DS_UDTO_SearchOSDoctorOrderForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DS_UDTO_SearchOSDoctorOrderForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DS_UDTO_SearchOSDoctorOrderForm(long id);

        [ServiceContractDescription(Name = "查找医嘱单信息", Desc = "查找医生医嘱单信息", Url = "ZhiFangWeiXinAppDoctService.svc/DS_UDTO_SearchOSDoctorOrderFormList?beginDate={beginDate}&endDate={endDate}&patName={patName}&sort={sort}&page={page}&limit={limit}", Get = "beginDate={beginDate}&endDate={endDate}&patName={patName}&sort={sort}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DS_UDTO_SearchOSDoctorOrderFormList?beginDate={beginDate}&endDate={endDate}&patName={patName}&sort={sort}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DS_UDTO_SearchOSDoctorOrderFormList(string beginDate, string endDate, string patName, string sort, int page, int limit);

        [ServiceContractDescription(Name = "医生强制登陆服务", Desc = "医生强制登陆服务", Url = "ZhiFangWeiXinAppDoctService.svc/WXADS_BA_Login?password={password}", Get = "password={password}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXADS_BA_Login?password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXADS_BA_Login(string password);

        [ServiceContractDescription(Name = "医生帐号绑定", Desc = "医生帐号绑定", Url = "ZhiFangWeiXinAppDoctService.svc/WXADS_DoctorAccountBind?AccountCode={AccountCode}&password={password}", Get = "AccountCode={AccountCode}&password={password}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXADS_DoctorAccountBind?AccountCode={AccountCode}&password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXADS_DoctorAccountBind(string AccountCode, string password);

        

        [ServiceContractDescription(Name = "获取医生账号信息服务", Desc = "获取医生账号信息服务", Url = "ZhiFangWeiXinAppDoctService.svc/WXADS_BA_GetDoctorAccountInfo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXADS_BA_GetDoctorAccountInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXADS_BA_GetDoctorAccountInfo();

        [ServiceContractDescription(Name = "查询医生医嘱单信息服务", Desc = "查询医生医嘱单信息服务", Url = "ZhiFangWeiXinAppDoctService.svc/WXADS_BA_SearchDoctorOrderForm", Get = "", Post = "page,limit", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WXADS_BA_SearchDoctorOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXADS_BA_SearchDoctorOrderForm(int page, int limit);

        [ServiceContractDescription(Name = "获取医生费用信息服务", Desc = "获取医生费用信息服务", Url = "ZhiFangWeiXinAppDoctService.svc/WXADS_BA_GetDoctorChargeInfo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXADS_BA_GetDoctorChargeInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXADS_BA_GetDoctorChargeInfo();

        [ServiceContractDescription(Name = "获取医生费用信息服务", Desc = "获取医生费用信息服务", Url = "ZhiFangWeiXinAppDoctService.svc/WXADS_BA_GetDoctorChargeInfoDay?page={page}&limit={limit}", Get = "page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXADS_BA_GetDoctorChargeInfoDay?page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXADS_BA_GetDoctorChargeInfoDay(int page, int limit);

        [ServiceContractDescription(Name = "获取消费单信息服务", Desc = "获取消费单信息服务", Url = "ZhiFangWeiXinAppDoctService.svc/WXADS_BA_GetOSUserConsumerForm?StartDay={StartDay}&EndDay={EndDay}&page={page}&limit={limit}", Get = "StartDay={StartDay}&EndDay={EndDay}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXADS_BA_GetOSUserConsumerForm?StartDay={StartDay}&EndDay={EndDay}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXADS_BA_GetOSUserConsumerForm(string StartDay, string EndDay, int page, int limit);

        [ServiceContractDescription(Name = "查询检测项目产品分类树信息", Desc = "查询检测项目产品分类树信息", Url = "ZhiFangWeiXinAppDoctService.svc/DS_UDTO_SearchOSItemProductClassTreeByIdAndHQL?id={id}&where={where}&fields={fields}", Get = "id={id}&maxlevel={maxlevel}&where={where}&fields={fields}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeOSItemProductClassTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DS_UDTO_SearchOSItemProductClassTreeByIdAndHQL?id={id}&maxlevel={maxlevel}&where={where}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DS_UDTO_SearchOSItemProductClassTreeByIdAndHQL(string id, string maxlevel, string where, string fields);

        [ServiceContractDescription(Name = "依产品分类树节点Id获取检测项目产品分类信息", Desc = "依产品分类树节点Id获取检测项目产品分类信息", Url = "ZhiFangWeiXinAppDoctService.svc/DS_UDTO_SearchBLabTestItemVOByTreeId?page={page}&limit={limit}&isPlanish={isPlanish}&isSearchChild={isSearchChild}&treeId={treeId}&fields={fields}&where={where}&sort={sort}", Get = "page={page}&limit={limit}&isPlanish={isPlanish}&isSearchChild={isSearchChild}&treeId={treeId}&fields={fields}&where={where}&sort={sort}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DS_UDTO_SearchBLabTestItemVOByTreeId?page={page}&limit={limit}&isPlanish={isPlanish}&isSearchChild={isSearchChild}&treeId={treeId}&fields={fields}&where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DS_UDTO_SearchBLabTestItemVOByTreeId(int limit, int page, bool isPlanish, bool isSearchChild, string treeId, string where, string fields, string sort);
    }
}
