using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ZhiFang.Digitlab.Entity;
using System.ServiceModel.Web;
using System.ComponentModel;
using ZhiFang.Digitlab.ServiceCommon;

namespace ZhiFang.Digitlab.ReagentSys.ServerContract
{
    [ServiceContract]
    public interface ISingleTableService
    {

        #region BAgeUnit

        [ServiceContractDescription(Name = "新增年龄单位", Desc = "新增年龄单位", Url = "SingleTableService.svc/ST_UDTO_AddBAgeUnit", Get = "", Post = "BAgeUnit", Return = "BaseResultDataValue", ReturnType = "BAgeUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBAgeUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBAgeUnit(BAgeUnit entity);

        [ServiceContractDescription(Name = "修改年龄单位", Desc = "修改年龄单位", Url = "SingleTableService.svc/ST_UDTO_UpdateBAgeUnit", Get = "", Post = "BAgeUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBAgeUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBAgeUnit(BAgeUnit entity);

        [ServiceContractDescription(Name = "修改年龄单位指定的属性", Desc = "修改年龄单位指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBAgeUnitByField", Get = "", Post = "BAgeUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBAgeUnitByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBAgeUnitByField(BAgeUnit entity, string fields);

        [ServiceContractDescription(Name = "删除年龄单位", Desc = "删除年龄单位", Url = "SingleTableService.svc/ST_UDTO_DelBAgeUnit?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBAgeUnit?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBAgeUnit(long id);

        [ServiceContractDescription(Name = "查询年龄单位", Desc = "查询年龄单位", Url = "SingleTableService.svc/ST_UDTO_SearchBAgeUnit", Get = "", Post = "BAgeUnit", Return = "BaseResultList<BAgeUnit>", ReturnType = "ListBAgeUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAgeUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAgeUnit(BAgeUnit entity);

        [ServiceContractDescription(Name = "查询年龄单位(HQL)", Desc = "查询年龄单位(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBAgeUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BAgeUnit>", ReturnType = "ListBAgeUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAgeUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAgeUnitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询年龄单位", Desc = "通过主键ID查询年龄单位", Url = "SingleTableService.svc/ST_UDTO_SearchBAgeUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BAgeUnit>", ReturnType = "BAgeUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAgeUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAgeUnitById(long id, string fields, bool isPlanish);
        #endregion

        #region BAttendanceType

        [ServiceContractDescription(Name = "新增考勤类型", Desc = "新增考勤类型", Url = "SingleTableService.svc/ST_UDTO_AddBAttendanceType", Get = "", Post = "BAttendanceType", Return = "BaseResultDataValue", ReturnType = "BAttendanceType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBAttendanceType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBAttendanceType(BAttendanceType entity);

        [ServiceContractDescription(Name = "修改考勤类型", Desc = "修改考勤类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBAttendanceType", Get = "", Post = "BAttendanceType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBAttendanceType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBAttendanceType(BAttendanceType entity);

        [ServiceContractDescription(Name = "修改考勤类型指定的属性", Desc = "修改考勤类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBAttendanceTypeByField", Get = "", Post = "BAttendanceType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBAttendanceTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBAttendanceTypeByField(BAttendanceType entity, string fields);

        [ServiceContractDescription(Name = "删除考勤类型", Desc = "删除考勤类型", Url = "SingleTableService.svc/ST_UDTO_DelBAttendanceType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBAttendanceType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBAttendanceType(long id);

        [ServiceContractDescription(Name = "查询考勤类型", Desc = "查询考勤类型", Url = "SingleTableService.svc/ST_UDTO_SearchBAttendanceType", Get = "", Post = "BAttendanceType", Return = "BaseResultList<BAttendanceType>", ReturnType = "ListBAttendanceType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAttendanceType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAttendanceType(BAttendanceType entity);

        [ServiceContractDescription(Name = "查询考勤类型(HQL)", Desc = "查询考勤类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBAttendanceTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BAttendanceType>", ReturnType = "ListBAttendanceType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAttendanceTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAttendanceTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询考勤类型", Desc = "通过主键ID查询考勤类型", Url = "SingleTableService.svc/ST_UDTO_SearchBAttendanceTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BAttendanceType>", ReturnType = "BAttendanceType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAttendanceTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAttendanceTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BBackSectionState

        [ServiceContractDescription(Name = "新增回款状况", Desc = "新增回款状况", Url = "SingleTableService.svc/ST_UDTO_AddBBackSectionState", Get = "", Post = "BBackSectionState", Return = "BaseResultDataValue", ReturnType = "BBackSectionState")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBBackSectionState", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBBackSectionState(BBackSectionState entity);

        [ServiceContractDescription(Name = "修改回款状况", Desc = "修改回款状况", Url = "SingleTableService.svc/ST_UDTO_UpdateBBackSectionState", Get = "", Post = "BBackSectionState", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBBackSectionState", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBBackSectionState(BBackSectionState entity);

        [ServiceContractDescription(Name = "修改回款状况指定的属性", Desc = "修改回款状况指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBBackSectionStateByField", Get = "", Post = "BBackSectionState", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBBackSectionStateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBBackSectionStateByField(BBackSectionState entity, string fields);

        [ServiceContractDescription(Name = "删除回款状况", Desc = "删除回款状况", Url = "SingleTableService.svc/ST_UDTO_DelBBackSectionState?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBBackSectionState?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBBackSectionState(long id);

        [ServiceContractDescription(Name = "查询回款状况", Desc = "查询回款状况", Url = "SingleTableService.svc/ST_UDTO_SearchBBackSectionState", Get = "", Post = "BBackSectionState", Return = "BaseResultList<BBackSectionState>", ReturnType = "ListBBackSectionState")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBBackSectionState", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBBackSectionState(BBackSectionState entity);

        [ServiceContractDescription(Name = "查询回款状况(HQL)", Desc = "查询回款状况(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBBackSectionStateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BBackSectionState>", ReturnType = "ListBBackSectionState")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBBackSectionStateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBBackSectionStateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询回款状况", Desc = "通过主键ID查询回款状况", Url = "SingleTableService.svc/ST_UDTO_SearchBBackSectionStateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BBackSectionState>", ReturnType = "BBackSectionState")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBBackSectionStateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBBackSectionStateById(long id, string fields, bool isPlanish);
        #endregion

        #region BBusinessType

        [ServiceContractDescription(Name = "新增业务类型", Desc = "新增业务类型", Url = "SingleTableService.svc/ST_UDTO_AddBBusinessType", Get = "", Post = "BBusinessType", Return = "BaseResultDataValue", ReturnType = "BBusinessType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBBusinessType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBBusinessType(BBusinessType entity);

        [ServiceContractDescription(Name = "修改业务类型", Desc = "修改业务类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBBusinessType", Get = "", Post = "BBusinessType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBBusinessType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBBusinessType(BBusinessType entity);

        [ServiceContractDescription(Name = "修改业务类型指定的属性", Desc = "修改业务类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBBusinessTypeByField", Get = "", Post = "BBusinessType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBBusinessTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBBusinessTypeByField(BBusinessType entity, string fields);

        [ServiceContractDescription(Name = "删除业务类型", Desc = "删除业务类型", Url = "SingleTableService.svc/ST_UDTO_DelBBusinessType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBBusinessType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBBusinessType(long id);

        [ServiceContractDescription(Name = "查询业务类型", Desc = "查询业务类型", Url = "SingleTableService.svc/ST_UDTO_SearchBBusinessType", Get = "", Post = "BBusinessType", Return = "BaseResultList<BBusinessType>", ReturnType = "ListBBusinessType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBBusinessType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBBusinessType(BBusinessType entity);

        [ServiceContractDescription(Name = "查询业务类型(HQL)", Desc = "查询业务类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBBusinessTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BBusinessType>", ReturnType = "ListBBusinessType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBBusinessTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBBusinessTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询业务类型", Desc = "通过主键ID查询业务类型", Url = "SingleTableService.svc/ST_UDTO_SearchBBusinessTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BBusinessType>", ReturnType = "BBusinessType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBBusinessTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBBusinessTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BCheckStatus

        [ServiceContractDescription(Name = "新增审核状态", Desc = "新增审核状态", Url = "SingleTableService.svc/ST_UDTO_AddBCheckStatus", Get = "", Post = "BCheckStatus", Return = "BaseResultDataValue", ReturnType = "BCheckStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBCheckStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBCheckStatus(BCheckStatus entity);

        [ServiceContractDescription(Name = "修改审核状态", Desc = "修改审核状态", Url = "SingleTableService.svc/ST_UDTO_UpdateBCheckStatus", Get = "", Post = "BCheckStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCheckStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCheckStatus(BCheckStatus entity);

        [ServiceContractDescription(Name = "修改审核状态指定的属性", Desc = "修改审核状态指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBCheckStatusByField", Get = "", Post = "BCheckStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCheckStatusByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCheckStatusByField(BCheckStatus entity, string fields);

        [ServiceContractDescription(Name = "删除审核状态", Desc = "删除审核状态", Url = "SingleTableService.svc/ST_UDTO_DelBCheckStatus?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBCheckStatus?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBCheckStatus(long id);

        [ServiceContractDescription(Name = "查询审核状态", Desc = "查询审核状态", Url = "SingleTableService.svc/ST_UDTO_SearchBCheckStatus", Get = "", Post = "BCheckStatus", Return = "BaseResultList<BCheckStatus>", ReturnType = "ListBCheckStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCheckStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCheckStatus(BCheckStatus entity);

        [ServiceContractDescription(Name = "查询审核状态(HQL)", Desc = "查询审核状态(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBCheckStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCheckStatus>", ReturnType = "ListBCheckStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCheckStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCheckStatusByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询审核状态", Desc = "通过主键ID查询审核状态", Url = "SingleTableService.svc/ST_UDTO_SearchBCheckStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCheckStatus>", ReturnType = "BCheckStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCheckStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCheckStatusById(long id, string fields, bool isPlanish);
        #endregion        

        #region BConstellation

        [ServiceContractDescription(Name = "新增星座", Desc = "新增星座", Url = "SingleTableService.svc/ST_UDTO_AddBConstellation", Get = "", Post = "BConstellation", Return = "BaseResultDataValue", ReturnType = "BConstellation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBConstellation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBConstellation(BConstellation entity);

        [ServiceContractDescription(Name = "修改星座", Desc = "修改星座", Url = "SingleTableService.svc/ST_UDTO_UpdateBConstellation", Get = "", Post = "BConstellation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBConstellation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBConstellation(BConstellation entity);

        [ServiceContractDescription(Name = "修改星座指定的属性", Desc = "修改星座指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBConstellationByField", Get = "", Post = "BConstellation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBConstellationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBConstellationByField(BConstellation entity, string fields);

        [ServiceContractDescription(Name = "删除星座", Desc = "删除星座", Url = "SingleTableService.svc/ST_UDTO_DelBConstellation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBConstellation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBConstellation(long id);

        [ServiceContractDescription(Name = "查询星座", Desc = "查询星座", Url = "SingleTableService.svc/ST_UDTO_SearchBConstellation", Get = "", Post = "BConstellation", Return = "BaseResultList<BConstellation>", ReturnType = "ListBConstellation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBConstellation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBConstellation(BConstellation entity);

        [ServiceContractDescription(Name = "查询星座(HQL)", Desc = "查询星座(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBConstellationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BConstellation>", ReturnType = "ListBConstellation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBConstellationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBConstellationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询星座", Desc = "通过主键ID查询星座", Url = "SingleTableService.svc/ST_UDTO_SearchBConstellationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BConstellation>", ReturnType = "BConstellation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBConstellationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBConstellationById(long id, string fields, bool isPlanish);
        #endregion

        #region BCountry

        [ServiceContractDescription(Name = "新增国家", Desc = "新增国家", Url = "SingleTableService.svc/ST_UDTO_AddBCountry", Get = "", Post = "BCountry", Return = "BaseResultDataValue", ReturnType = "BCountry")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBCountry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBCountry(BCountry entity);

        [ServiceContractDescription(Name = "修改国家", Desc = "修改国家", Url = "SingleTableService.svc/ST_UDTO_UpdateBCountry", Get = "", Post = "BCountry", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCountry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCountry(BCountry entity);

        [ServiceContractDescription(Name = "修改国家指定的属性", Desc = "修改国家指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBCountryByField", Get = "", Post = "BCountry", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCountryByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCountryByField(BCountry entity, string fields);

        [ServiceContractDescription(Name = "删除国家", Desc = "删除国家", Url = "SingleTableService.svc/ST_UDTO_DelBCountry?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBCountry?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBCountry(long id);

        [ServiceContractDescription(Name = "查询国家", Desc = "查询国家", Url = "SingleTableService.svc/ST_UDTO_SearchBCountry", Get = "", Post = "BCountry", Return = "BaseResultList<BCountry>", ReturnType = "ListBCountry")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCountry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCountry(BCountry entity);

        [ServiceContractDescription(Name = "查询国家(HQL)", Desc = "查询国家(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBCountryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCountry>", ReturnType = "ListBCountry")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCountryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCountryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询国家", Desc = "通过主键ID查询国家", Url = "SingleTableService.svc/ST_UDTO_SearchBCountryById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCountry>", ReturnType = "BCountry")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCountryById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCountryById(long id, string fields, bool isPlanish);
        #endregion

        #region BCity

        [ServiceContractDescription(Name = "新增城市", Desc = "新增城市", Url = "SingleTableService.svc/ST_UDTO_AddBCity", Get = "", Post = "BCity", Return = "BaseResultDataValue", ReturnType = "BCity")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBCity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBCity(BCity entity);

        [ServiceContractDescription(Name = "修改城市", Desc = "修改城市", Url = "SingleTableService.svc/ST_UDTO_UpdateBCity", Get = "", Post = "BCity", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCity(BCity entity);

        [ServiceContractDescription(Name = "修改城市指定的属性", Desc = "修改城市指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBCityByField", Get = "", Post = "BCity", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCityByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCityByField(BCity entity, string fields);

        [ServiceContractDescription(Name = "删除城市", Desc = "删除城市", Url = "SingleTableService.svc/ST_UDTO_DelBCity?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBCity?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBCity(long id);

        [ServiceContractDescription(Name = "查询城市", Desc = "查询城市", Url = "SingleTableService.svc/ST_UDTO_SearchBCity", Get = "", Post = "BCity", Return = "BaseResultList<BCity>", ReturnType = "ListBCity")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCity(BCity entity);

        [ServiceContractDescription(Name = "查询城市(HQL)", Desc = "查询城市(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBCityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCity>", ReturnType = "ListBCity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询城市", Desc = "通过主键ID查询城市", Url = "SingleTableService.svc/ST_UDTO_SearchBCityById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCity>", ReturnType = "BCity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCityById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCityById(long id, string fields, bool isPlanish);
        #endregion

        #region BDegree

        [ServiceContractDescription(Name = "新增学位", Desc = "新增学位", Url = "SingleTableService.svc/ST_UDTO_AddBDegree", Get = "", Post = "BDegree", Return = "BaseResultDataValue", ReturnType = "BDegree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBDegree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBDegree(BDegree entity);

        [ServiceContractDescription(Name = "修改学位", Desc = "修改学位", Url = "SingleTableService.svc/ST_UDTO_UpdateBDegree", Get = "", Post = "BDegree", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDegree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDegree(BDegree entity);

        [ServiceContractDescription(Name = "修改学位指定的属性", Desc = "修改学位指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBDegreeByField", Get = "", Post = "BDegree", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDegreeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDegreeByField(BDegree entity, string fields);

        [ServiceContractDescription(Name = "删除学位", Desc = "删除学位", Url = "SingleTableService.svc/ST_UDTO_DelBDegree?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBDegree?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBDegree(long id);

        [ServiceContractDescription(Name = "查询学位", Desc = "查询学位", Url = "SingleTableService.svc/ST_UDTO_SearchBDegree", Get = "", Post = "BDegree", Return = "BaseResultList<BDegree>", ReturnType = "ListBDegree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDegree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDegree(BDegree entity);

        [ServiceContractDescription(Name = "查询学位(HQL)", Desc = "查询学位(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBDegreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDegree>", ReturnType = "ListBDegree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDegreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDegreeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询学位", Desc = "通过主键ID查询学位", Url = "SingleTableService.svc/ST_UDTO_SearchBDegreeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDegree>", ReturnType = "BDegree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDegreeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDegreeById(long id, string fields, bool isPlanish);
        #endregion

        #region BDiag

        [ServiceContractDescription(Name = "新增临床诊断", Desc = "新增临床诊断", Url = "SingleTableService.svc/ST_UDTO_AddBDiag", Get = "", Post = "BDiag", Return = "BaseResultDataValue", ReturnType = "BDiag")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBDiag", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBDiag(BDiag entity);

        [ServiceContractDescription(Name = "修改临床诊断", Desc = "修改临床诊断", Url = "SingleTableService.svc/ST_UDTO_UpdateBDiag", Get = "", Post = "BDiag", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDiag", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDiag(BDiag entity);

        [ServiceContractDescription(Name = "修改临床诊断指定的属性", Desc = "修改临床诊断指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBDiagByField", Get = "", Post = "BDiag", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDiagByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDiagByField(BDiag entity, string fields);

        [ServiceContractDescription(Name = "删除临床诊断", Desc = "删除临床诊断", Url = "SingleTableService.svc/ST_UDTO_DelBDiag?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBDiag?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBDiag(long id);

        [ServiceContractDescription(Name = "查询临床诊断", Desc = "查询临床诊断", Url = "SingleTableService.svc/ST_UDTO_SearchBDiag", Get = "", Post = "BDiag", Return = "BaseResultList<BDiag>", ReturnType = "ListBDiag")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDiag", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDiag(BDiag entity);

        [ServiceContractDescription(Name = "查询临床诊断(HQL)", Desc = "查询临床诊断(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBDiagByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDiag>", ReturnType = "ListBDiag")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDiagByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDiagByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询临床诊断", Desc = "通过主键ID查询临床诊断", Url = "SingleTableService.svc/ST_UDTO_SearchBDiagById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDiag>", ReturnType = "BDiag")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDiagById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDiagById(long id, string fields, bool isPlanish);
        #endregion

        #region BEducationLevel

        [ServiceContractDescription(Name = "新增学历", Desc = "新增学历", Url = "SingleTableService.svc/ST_UDTO_AddBEducationLevel", Get = "", Post = "BEducationLevel", Return = "BaseResultDataValue", ReturnType = "BEducationLevel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBEducationLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBEducationLevel(BEducationLevel entity);

        [ServiceContractDescription(Name = "修改学历", Desc = "修改学历", Url = "SingleTableService.svc/ST_UDTO_UpdateBEducationLevel", Get = "", Post = "BEducationLevel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBEducationLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBEducationLevel(BEducationLevel entity);

        [ServiceContractDescription(Name = "修改学历指定的属性", Desc = "修改学历指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBEducationLevelByField", Get = "", Post = "BEducationLevel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBEducationLevelByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBEducationLevelByField(BEducationLevel entity, string fields);

        [ServiceContractDescription(Name = "删除学历", Desc = "删除学历", Url = "SingleTableService.svc/ST_UDTO_DelBEducationLevel?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBEducationLevel?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBEducationLevel(long id);

        [ServiceContractDescription(Name = "查询学历", Desc = "查询学历", Url = "SingleTableService.svc/ST_UDTO_SearchBEducationLevel", Get = "", Post = "BEducationLevel", Return = "BaseResultList<BEducationLevel>", ReturnType = "ListBEducationLevel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBEducationLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBEducationLevel(BEducationLevel entity);

        [ServiceContractDescription(Name = "查询学历(HQL)", Desc = "查询学历(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBEducationLevelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BEducationLevel>", ReturnType = "ListBEducationLevel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBEducationLevelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBEducationLevelByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询学历", Desc = "通过主键ID查询学历", Url = "SingleTableService.svc/ST_UDTO_SearchBEducationLevelById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BEducationLevel>", ReturnType = "BEducationLevel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBEducationLevelById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBEducationLevelById(long id, string fields, bool isPlanish);
        #endregion

        #region BExecutionState

        [ServiceContractDescription(Name = "新增执行状态", Desc = "新增执行状态", Url = "SingleTableService.svc/ST_UDTO_AddBExecutionState", Get = "", Post = "BExecutionState", Return = "BaseResultDataValue", ReturnType = "BExecutionState")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBExecutionState", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBExecutionState(BExecutionState entity);

        [ServiceContractDescription(Name = "修改执行状态", Desc = "修改执行状态", Url = "SingleTableService.svc/ST_UDTO_UpdateBExecutionState", Get = "", Post = "BExecutionState", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBExecutionState", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBExecutionState(BExecutionState entity);

        [ServiceContractDescription(Name = "修改执行状态指定的属性", Desc = "修改执行状态指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBExecutionStateByField", Get = "", Post = "BExecutionState", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBExecutionStateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBExecutionStateByField(BExecutionState entity, string fields);

        [ServiceContractDescription(Name = "删除执行状态", Desc = "删除执行状态", Url = "SingleTableService.svc/ST_UDTO_DelBExecutionState?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBExecutionState?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBExecutionState(long id);

        [ServiceContractDescription(Name = "查询执行状态", Desc = "查询执行状态", Url = "SingleTableService.svc/ST_UDTO_SearchBExecutionState", Get = "", Post = "BExecutionState", Return = "BaseResultList<BExecutionState>", ReturnType = "ListBExecutionState")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBExecutionState", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBExecutionState(BExecutionState entity);

        [ServiceContractDescription(Name = "查询执行状态(HQL)", Desc = "查询执行状态(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBExecutionStateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BExecutionState>", ReturnType = "ListBExecutionState")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBExecutionStateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBExecutionStateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询执行状态", Desc = "通过主键ID查询执行状态", Url = "SingleTableService.svc/ST_UDTO_SearchBExecutionStateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BExecutionState>", ReturnType = "BExecutionState")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBExecutionStateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBExecutionStateById(long id, string fields, bool isPlanish);
        #endregion

        #region BFileGroup

        [ServiceContractDescription(Name = "新增文件分类", Desc = "新增文件分类", Url = "SingleTableService.svc/ST_UDTO_AddBFileGroup", Get = "", Post = "BFileGroup", Return = "BaseResultDataValue", ReturnType = "BFileGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBFileGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBFileGroup(BFileGroup entity);

        [ServiceContractDescription(Name = "修改文件分类", Desc = "修改文件分类", Url = "SingleTableService.svc/ST_UDTO_UpdateBFileGroup", Get = "", Post = "BFileGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBFileGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBFileGroup(BFileGroup entity);

        [ServiceContractDescription(Name = "修改文件分类指定的属性", Desc = "修改文件分类指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBFileGroupByField", Get = "", Post = "BFileGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBFileGroupByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBFileGroupByField(BFileGroup entity, string fields);

        [ServiceContractDescription(Name = "删除文件分类", Desc = "删除文件分类", Url = "SingleTableService.svc/ST_UDTO_DelBFileGroup?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBFileGroup?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBFileGroup(long id);

        [ServiceContractDescription(Name = "查询文件分类", Desc = "查询文件分类", Url = "SingleTableService.svc/ST_UDTO_SearchBFileGroup", Get = "", Post = "BFileGroup", Return = "BaseResultList<BFileGroup>", ReturnType = "ListBFileGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBFileGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBFileGroup(BFileGroup entity);

        [ServiceContractDescription(Name = "查询文件分类(HQL)", Desc = "查询文件分类(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBFileGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BFileGroup>", ReturnType = "ListBFileGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBFileGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBFileGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询文件分类", Desc = "通过主键ID查询文件分类", Url = "SingleTableService.svc/ST_UDTO_SearchBFileGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BFileGroup>", ReturnType = "BFileGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBFileGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBFileGroupById(long id, string fields, bool isPlanish);
        #endregion

        #region BFileType

        [ServiceContractDescription(Name = "新增文件类型", Desc = "新增文件类型", Url = "SingleTableService.svc/ST_UDTO_AddBFileType", Get = "", Post = "BFileType", Return = "BaseResultDataValue", ReturnType = "BFileType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBFileType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBFileType(BFileType entity);

        [ServiceContractDescription(Name = "修改文件类型", Desc = "修改文件类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBFileType", Get = "", Post = "BFileType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBFileType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBFileType(BFileType entity);

        [ServiceContractDescription(Name = "修改文件类型指定的属性", Desc = "修改文件类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBFileTypeByField", Get = "", Post = "BFileType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBFileTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBFileTypeByField(BFileType entity, string fields);

        [ServiceContractDescription(Name = "删除文件类型", Desc = "删除文件类型", Url = "SingleTableService.svc/ST_UDTO_DelBFileType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBFileType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBFileType(long id);

        [ServiceContractDescription(Name = "查询文件类型", Desc = "查询文件类型", Url = "SingleTableService.svc/ST_UDTO_SearchBFileType", Get = "", Post = "BFileType", Return = "BaseResultList<BFileType>", ReturnType = "ListBFileType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBFileType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBFileType(BFileType entity);

        [ServiceContractDescription(Name = "查询文件类型(HQL)", Desc = "查询文件类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBFileTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BFileType>", ReturnType = "ListBFileType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBFileTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBFileTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询文件类型", Desc = "通过主键ID查询文件类型", Url = "SingleTableService.svc/ST_UDTO_SearchBFileTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BFileType>", ReturnType = "BFileType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBFileTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBFileTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BFinancial

        [ServiceContractDescription(Name = "新增财务管理", Desc = "新增财务管理", Url = "SingleTableService.svc/ST_UDTO_AddBFinancial", Get = "", Post = "BFinancial", Return = "BaseResultDataValue", ReturnType = "BFinancial")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBFinancial", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBFinancial(BFinancial entity);

        [ServiceContractDescription(Name = "修改财务管理", Desc = "修改财务管理", Url = "SingleTableService.svc/ST_UDTO_UpdateBFinancial", Get = "", Post = "BFinancial", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBFinancial", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBFinancial(BFinancial entity);

        [ServiceContractDescription(Name = "修改财务管理指定的属性", Desc = "修改财务管理指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBFinancialByField", Get = "", Post = "BFinancial", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBFinancialByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBFinancialByField(BFinancial entity, string fields);

        [ServiceContractDescription(Name = "删除财务管理", Desc = "删除财务管理", Url = "SingleTableService.svc/ST_UDTO_DelBFinancial?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBFinancial?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBFinancial(long id);

        [ServiceContractDescription(Name = "查询财务管理", Desc = "查询财务管理", Url = "SingleTableService.svc/ST_UDTO_SearchBFinancial", Get = "", Post = "BFinancial", Return = "BaseResultList<BFinancial>", ReturnType = "ListBFinancial")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBFinancial", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBFinancial(BFinancial entity);

        [ServiceContractDescription(Name = "查询财务管理(HQL)", Desc = "查询财务管理(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBFinancialByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BFinancial>", ReturnType = "ListBFinancial")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBFinancialByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBFinancialByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询财务管理", Desc = "通过主键ID查询财务管理", Url = "SingleTableService.svc/ST_UDTO_SearchBFinancialById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BFinancial>", ReturnType = "BFinancial")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBFinancialById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBFinancialById(long id, string fields, bool isPlanish);
        #endregion

        #region BHealthStatus

        [ServiceContractDescription(Name = "新增健康状况", Desc = "新增健康状况", Url = "SingleTableService.svc/ST_UDTO_AddBHealthStatus", Get = "", Post = "BHealthStatus", Return = "BaseResultDataValue", ReturnType = "BHealthStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBHealthStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBHealthStatus(BHealthStatus entity);

        [ServiceContractDescription(Name = "修改健康状况", Desc = "修改健康状况", Url = "SingleTableService.svc/ST_UDTO_UpdateBHealthStatus", Get = "", Post = "BHealthStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHealthStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHealthStatus(BHealthStatus entity);

        [ServiceContractDescription(Name = "修改健康状况指定的属性", Desc = "修改健康状况指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBHealthStatusByField", Get = "", Post = "BHealthStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHealthStatusByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHealthStatusByField(BHealthStatus entity, string fields);

        [ServiceContractDescription(Name = "删除健康状况", Desc = "删除健康状况", Url = "SingleTableService.svc/ST_UDTO_DelBHealthStatus?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBHealthStatus?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBHealthStatus(long id);

        [ServiceContractDescription(Name = "查询健康状况", Desc = "查询健康状况", Url = "SingleTableService.svc/ST_UDTO_SearchBHealthStatus", Get = "", Post = "BHealthStatus", Return = "BaseResultList<BHealthStatus>", ReturnType = "ListBHealthStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHealthStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHealthStatus(BHealthStatus entity);

        [ServiceContractDescription(Name = "查询健康状况(HQL)", Desc = "查询健康状况(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBHealthStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHealthStatus>", ReturnType = "ListBHealthStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHealthStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHealthStatusByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询健康状况", Desc = "通过主键ID查询健康状况", Url = "SingleTableService.svc/ST_UDTO_SearchBHealthStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHealthStatus>", ReturnType = "BHealthStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHealthStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHealthStatusById(long id, string fields, bool isPlanish);
        #endregion

        #region BLeaveType

        [ServiceContractDescription(Name = "新增请假类型", Desc = "新增请假类型", Url = "SingleTableService.svc/ST_UDTO_AddBLeaveType", Get = "", Post = "BLeaveType", Return = "BaseResultDataValue", ReturnType = "BLeaveType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBLeaveType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBLeaveType(BLeaveType entity);

        [ServiceContractDescription(Name = "修改请假类型", Desc = "修改请假类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBLeaveType", Get = "", Post = "BLeaveType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLeaveType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLeaveType(BLeaveType entity);

        [ServiceContractDescription(Name = "修改请假类型指定的属性", Desc = "修改请假类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBLeaveTypeByField", Get = "", Post = "BLeaveType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLeaveTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLeaveTypeByField(BLeaveType entity, string fields);

        [ServiceContractDescription(Name = "删除请假类型", Desc = "删除请假类型", Url = "SingleTableService.svc/ST_UDTO_DelBLeaveType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBLeaveType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBLeaveType(long id);

        [ServiceContractDescription(Name = "查询请假类型", Desc = "查询请假类型", Url = "SingleTableService.svc/ST_UDTO_SearchBLeaveType", Get = "", Post = "BLeaveType", Return = "BaseResultList<BLeaveType>", ReturnType = "ListBLeaveType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLeaveType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLeaveType(BLeaveType entity);

        [ServiceContractDescription(Name = "查询请假类型(HQL)", Desc = "查询请假类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBLeaveTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLeaveType>", ReturnType = "ListBLeaveType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLeaveTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLeaveTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询请假类型", Desc = "通过主键ID查询请假类型", Url = "SingleTableService.svc/ST_UDTO_SearchBLeaveTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLeaveType>", ReturnType = "BLeaveType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLeaveTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLeaveTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BLevel

        [ServiceContractDescription(Name = "新增级别", Desc = "新增级别", Url = "SingleTableService.svc/ST_UDTO_AddBLevel", Get = "", Post = "BLevel", Return = "BaseResultDataValue", ReturnType = "BLevel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBLevel(BLevel entity);

        [ServiceContractDescription(Name = "修改级别", Desc = "修改级别", Url = "SingleTableService.svc/ST_UDTO_UpdateBLevel", Get = "", Post = "BLevel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLevel(BLevel entity);

        [ServiceContractDescription(Name = "修改级别指定的属性", Desc = "修改级别指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBLevelByField", Get = "", Post = "BLevel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLevelByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLevelByField(BLevel entity, string fields);

        [ServiceContractDescription(Name = "删除级别", Desc = "删除级别", Url = "SingleTableService.svc/ST_UDTO_DelBLevel?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBLevel?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBLevel(long id);

        [ServiceContractDescription(Name = "查询级别", Desc = "查询级别", Url = "SingleTableService.svc/ST_UDTO_SearchBLevel", Get = "", Post = "BLevel", Return = "BaseResultList<BLevel>", ReturnType = "ListBLevel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLevel(BLevel entity);

        [ServiceContractDescription(Name = "查询级别(HQL)", Desc = "查询级别(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBLevelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLevel>", ReturnType = "ListBLevel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLevelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLevelByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询级别", Desc = "通过主键ID查询级别", Url = "SingleTableService.svc/ST_UDTO_SearchBLevelById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLevel>", ReturnType = "BLevel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLevelById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLevelById(long id, string fields, bool isPlanish);
        #endregion           

        #region BMaritalStatus

        [ServiceContractDescription(Name = "新增婚姻状况", Desc = "新增婚姻状况", Url = "SingleTableService.svc/ST_UDTO_AddBMaritalStatus", Get = "", Post = "BMaritalStatus", Return = "BaseResultDataValue", ReturnType = "BMaritalStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBMaritalStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBMaritalStatus(BMaritalStatus entity);

        [ServiceContractDescription(Name = "修改婚姻状况", Desc = "修改婚姻状况", Url = "SingleTableService.svc/ST_UDTO_UpdateBMaritalStatus", Get = "", Post = "BMaritalStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBMaritalStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBMaritalStatus(BMaritalStatus entity);

        [ServiceContractDescription(Name = "修改婚姻状况指定的属性", Desc = "修改婚姻状况指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBMaritalStatusByField", Get = "", Post = "BMaritalStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBMaritalStatusByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBMaritalStatusByField(BMaritalStatus entity, string fields);

        [ServiceContractDescription(Name = "删除婚姻状况", Desc = "删除婚姻状况", Url = "SingleTableService.svc/ST_UDTO_DelBMaritalStatus?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBMaritalStatus?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBMaritalStatus(long id);

        [ServiceContractDescription(Name = "查询婚姻状况", Desc = "查询婚姻状况", Url = "SingleTableService.svc/ST_UDTO_SearchBMaritalStatus", Get = "", Post = "BMaritalStatus", Return = "BaseResultList<BMaritalStatus>", ReturnType = "ListBMaritalStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBMaritalStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBMaritalStatus(BMaritalStatus entity);

        [ServiceContractDescription(Name = "查询婚姻状况(HQL)", Desc = "查询婚姻状况(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBMaritalStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BMaritalStatus>", ReturnType = "ListBMaritalStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBMaritalStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBMaritalStatusByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询婚姻状况", Desc = "通过主键ID查询婚姻状况", Url = "SingleTableService.svc/ST_UDTO_SearchBMaritalStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BMaritalStatus>", ReturnType = "BMaritalStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBMaritalStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBMaritalStatusById(long id, string fields, bool isPlanish);
        #endregion

        #region BNationality

        [ServiceContractDescription(Name = "新增民族", Desc = "新增民族", Url = "SingleTableService.svc/ST_UDTO_AddBNationality", Get = "", Post = "BNationality", Return = "BaseResultDataValue", ReturnType = "BNationality")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBNationality", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBNationality(BNationality entity);

        [ServiceContractDescription(Name = "修改民族", Desc = "修改民族", Url = "SingleTableService.svc/ST_UDTO_UpdateBNationality", Get = "", Post = "BNationality", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBNationality", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBNationality(BNationality entity);

        [ServiceContractDescription(Name = "修改民族指定的属性", Desc = "修改民族指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBNationalityByField", Get = "", Post = "BNationality", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBNationalityByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBNationalityByField(BNationality entity, string fields);

        [ServiceContractDescription(Name = "删除民族", Desc = "删除民族", Url = "SingleTableService.svc/ST_UDTO_DelBNationality?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBNationality?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBNationality(long id);

        [ServiceContractDescription(Name = "查询民族", Desc = "查询民族", Url = "SingleTableService.svc/ST_UDTO_SearchBNationality", Get = "", Post = "BNationality", Return = "BaseResultList<BNationality>", ReturnType = "ListBNationality")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNationality", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNationality(BNationality entity);

        [ServiceContractDescription(Name = "查询民族(HQL)", Desc = "查询民族(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBNationalityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BNationality>", ReturnType = "ListBNationality")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNationalityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNationalityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询民族", Desc = "通过主键ID查询民族", Url = "SingleTableService.svc/ST_UDTO_SearchBNationalityById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BNationality>", ReturnType = "BNationality")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNationalityById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNationalityById(long id, string fields, bool isPlanish);
        #endregion

        #region BNodeTable

        [ServiceContractDescription(Name = "新增站点表", Desc = "新增站点表", Url = "SingleTableService.svc/ST_UDTO_AddBNodeTable", Get = "", Post = "BNodeTable", Return = "BaseResultDataValue", ReturnType = "BNodeTable")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBNodeTable", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBNodeTable(BNodeTable entity);

        [ServiceContractDescription(Name = "修改站点表", Desc = "修改站点表", Url = "SingleTableService.svc/ST_UDTO_UpdateBNodeTable", Get = "", Post = "BNodeTable", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBNodeTable", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBNodeTable(BNodeTable entity);

        [ServiceContractDescription(Name = "修改站点表指定的属性", Desc = "修改站点表指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBNodeTableByField", Get = "", Post = "BNodeTable", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBNodeTableByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBNodeTableByField(BNodeTable entity, string fields);

        [ServiceContractDescription(Name = "删除站点表", Desc = "删除站点表", Url = "SingleTableService.svc/ST_UDTO_DelBNodeTable?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBNodeTable?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBNodeTable(long id);

        [ServiceContractDescription(Name = "查询站点表", Desc = "查询站点表", Url = "SingleTableService.svc/ST_UDTO_SearchBNodeTable", Get = "", Post = "BNodeTable", Return = "BaseResultList<BNodeTable>", ReturnType = "ListBNodeTable")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNodeTable", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNodeTable(BNodeTable entity);

        [ServiceContractDescription(Name = "查询站点表(HQL)", Desc = "查询站点表(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBNodeTableByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BNodeTable>", ReturnType = "ListBNodeTable")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNodeTableByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNodeTableByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询站点表", Desc = "通过主键ID查询站点表", Url = "SingleTableService.svc/ST_UDTO_SearchBNodeTableById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BNodeTable>", ReturnType = "BNodeTable")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNodeTableById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNodeTableById(long id, string fields, bool isPlanish);
        #endregion

        #region BNumberBuildRule

        [ServiceContractDescription(Name = "新增号码生成规则", Desc = "新增号码生成规则", Url = "SingleTableService.svc/ST_UDTO_AddBNumberBuildRule", Get = "", Post = "BNumberBuildRule", Return = "BaseResultDataValue", ReturnType = "BNumberBuildRule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBNumberBuildRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBNumberBuildRule(BNumberBuildRule entity);

        [ServiceContractDescription(Name = "修改号码生成规则", Desc = "修改号码生成规则", Url = "SingleTableService.svc/ST_UDTO_UpdateBNumberBuildRule", Get = "", Post = "BNumberBuildRule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBNumberBuildRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBNumberBuildRule(BNumberBuildRule entity);

        [ServiceContractDescription(Name = "修改号码生成规则指定的属性", Desc = "修改号码生成规则指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBNumberBuildRuleByField", Get = "", Post = "BNumberBuildRule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBNumberBuildRuleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBNumberBuildRuleByField(BNumberBuildRule entity, string fields);

        [ServiceContractDescription(Name = "删除号码生成规则", Desc = "删除号码生成规则", Url = "SingleTableService.svc/ST_UDTO_DelBNumberBuildRule?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBNumberBuildRule?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBNumberBuildRule(long id);

        [ServiceContractDescription(Name = "查询号码生成规则", Desc = "查询号码生成规则", Url = "SingleTableService.svc/ST_UDTO_SearchBNumberBuildRule", Get = "", Post = "BNumberBuildRule", Return = "BaseResultList<BNumberBuildRule>", ReturnType = "ListBNumberBuildRule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNumberBuildRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNumberBuildRule(BNumberBuildRule entity);

        [ServiceContractDescription(Name = "查询号码生成规则(HQL)", Desc = "查询号码生成规则(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBNumberBuildRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BNumberBuildRule>", ReturnType = "ListBNumberBuildRule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNumberBuildRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNumberBuildRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询号码生成规则", Desc = "通过主键ID查询号码生成规则", Url = "SingleTableService.svc/ST_UDTO_SearchBNumberBuildRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BNumberBuildRule>", ReturnType = "BNumberBuildRule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNumberBuildRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNumberBuildRuleById(long id, string fields, bool isPlanish);
        #endregion

        #region BNumberBuildRuleCondition

        [ServiceContractDescription(Name = "新增号码生成规则条件", Desc = "新增号码生成规则条件", Url = "SingleTableService.svc/ST_UDTO_AddBNumberBuildRuleCondition", Get = "", Post = "BNumberBuildRuleCondition", Return = "BaseResultDataValue", ReturnType = "BNumberBuildRuleCondition")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBNumberBuildRuleCondition", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBNumberBuildRuleCondition(BNumberBuildRuleCondition entity);

        [ServiceContractDescription(Name = "修改号码生成规则条件", Desc = "修改号码生成规则条件", Url = "SingleTableService.svc/ST_UDTO_UpdateBNumberBuildRuleCondition", Get = "", Post = "BNumberBuildRuleCondition", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBNumberBuildRuleCondition", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBNumberBuildRuleCondition(BNumberBuildRuleCondition entity);

        [ServiceContractDescription(Name = "修改号码生成规则条件指定的属性", Desc = "修改号码生成规则条件指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBNumberBuildRuleConditionByField", Get = "", Post = "BNumberBuildRuleCondition", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBNumberBuildRuleConditionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBNumberBuildRuleConditionByField(BNumberBuildRuleCondition entity, string fields);

        [ServiceContractDescription(Name = "删除号码生成规则条件", Desc = "删除号码生成规则条件", Url = "SingleTableService.svc/ST_UDTO_DelBNumberBuildRuleCondition?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBNumberBuildRuleCondition?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBNumberBuildRuleCondition(long id);

        [ServiceContractDescription(Name = "查询号码生成规则条件", Desc = "查询号码生成规则条件", Url = "SingleTableService.svc/ST_UDTO_SearchBNumberBuildRuleCondition", Get = "", Post = "BNumberBuildRuleCondition", Return = "BaseResultList<BNumberBuildRuleCondition>", ReturnType = "ListBNumberBuildRuleCondition")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNumberBuildRuleCondition", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNumberBuildRuleCondition(BNumberBuildRuleCondition entity);

        [ServiceContractDescription(Name = "查询号码生成规则条件(HQL)", Desc = "查询号码生成规则条件(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBNumberBuildRuleConditionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BNumberBuildRuleCondition>", ReturnType = "ListBNumberBuildRuleCondition")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNumberBuildRuleConditionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNumberBuildRuleConditionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询号码生成规则条件", Desc = "通过主键ID查询号码生成规则条件", Url = "SingleTableService.svc/ST_UDTO_SearchBNumberBuildRuleConditionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BNumberBuildRuleCondition>", ReturnType = "BNumberBuildRuleCondition")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNumberBuildRuleConditionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNumberBuildRuleConditionById(long id, string fields, bool isPlanish);
        #endregion

        #region BMaxNumberToRule

        [ServiceContractDescription(Name = "新增规则最大号", Desc = "新增规则最大号", Url = "SingleTableService.svc/ST_UDTO_AddBMaxNumberToRule", Get = "", Post = "BMaxNumberToRule", Return = "BaseResultDataValue", ReturnType = "BMaxNumberToRule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBMaxNumberToRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBMaxNumberToRule(BMaxNumberToRule entity);

        [ServiceContractDescription(Name = "修改规则最大号", Desc = "修改规则最大号", Url = "SingleTableService.svc/ST_UDTO_UpdateBMaxNumberToRule", Get = "", Post = "BMaxNumberToRule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBMaxNumberToRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBMaxNumberToRule(BMaxNumberToRule entity);

        [ServiceContractDescription(Name = "修改规则最大号指定的属性", Desc = "修改规则最大号指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBMaxNumberToRuleByField", Get = "", Post = "BMaxNumberToRule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBMaxNumberToRuleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBMaxNumberToRuleByField(BMaxNumberToRule entity, string fields);

        [ServiceContractDescription(Name = "删除规则最大号", Desc = "删除规则最大号", Url = "SingleTableService.svc/ST_UDTO_DelBMaxNumberToRule?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBMaxNumberToRule?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBMaxNumberToRule(long id);

        [ServiceContractDescription(Name = "查询规则最大号", Desc = "查询规则最大号", Url = "SingleTableService.svc/ST_UDTO_SearchBMaxNumberToRule", Get = "", Post = "BMaxNumberToRule", Return = "BaseResultList<BMaxNumberToRule>", ReturnType = "ListBMaxNumberToRule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBMaxNumberToRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBMaxNumberToRule(BMaxNumberToRule entity);

        [ServiceContractDescription(Name = "查询规则最大号(HQL)", Desc = "查询规则最大号(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBMaxNumberToRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BMaxNumberToRule>", ReturnType = "ListBMaxNumberToRule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBMaxNumberToRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBMaxNumberToRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询规则最大号", Desc = "通过主键ID查询规则最大号", Url = "SingleTableService.svc/ST_UDTO_SearchBMaxNumberToRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BMaxNumberToRule>", ReturnType = "BMaxNumberToRule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBMaxNumberToRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBMaxNumberToRuleById(long id, string fields, bool isPlanish);
        #endregion

        #region BOperateObjectType

        [ServiceContractDescription(Name = "新增操作对象类型", Desc = "新增操作对象类型", Url = "SingleTableService.svc/ST_UDTO_AddBOperateObjectType", Get = "", Post = "BOperateObjectType", Return = "BaseResultDataValue", ReturnType = "BOperateObjectType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBOperateObjectType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBOperateObjectType(BOperateObjectType entity);

        [ServiceContractDescription(Name = "修改操作对象类型", Desc = "修改操作对象类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBOperateObjectType", Get = "", Post = "BOperateObjectType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBOperateObjectType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBOperateObjectType(BOperateObjectType entity);

        [ServiceContractDescription(Name = "修改操作对象类型指定的属性", Desc = "修改操作对象类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBOperateObjectTypeByField", Get = "", Post = "BOperateObjectType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBOperateObjectTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBOperateObjectTypeByField(BOperateObjectType entity, string fields);

        [ServiceContractDescription(Name = "删除操作对象类型", Desc = "删除操作对象类型", Url = "SingleTableService.svc/ST_UDTO_DelBOperateObjectType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBOperateObjectType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBOperateObjectType(long id);

        [ServiceContractDescription(Name = "查询操作对象类型", Desc = "查询操作对象类型", Url = "SingleTableService.svc/ST_UDTO_SearchBOperateObjectType", Get = "", Post = "BOperateObjectType", Return = "BaseResultList<BOperateObjectType>", ReturnType = "ListBOperateObjectType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBOperateObjectType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBOperateObjectType(BOperateObjectType entity);

        [ServiceContractDescription(Name = "查询操作对象类型(HQL)", Desc = "查询操作对象类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBOperateObjectTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BOperateObjectType>", ReturnType = "ListBOperateObjectType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBOperateObjectTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBOperateObjectTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询操作对象类型", Desc = "通过主键ID查询操作对象类型", Url = "SingleTableService.svc/ST_UDTO_SearchBOperateObjectTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BOperateObjectType>", ReturnType = "BOperateObjectType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBOperateObjectTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBOperateObjectTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BProjectType

        [ServiceContractDescription(Name = "新增项目类型", Desc = "新增项目类型", Url = "SingleTableService.svc/ST_UDTO_AddBProjectType", Get = "", Post = "BProjectType", Return = "BaseResultDataValue", ReturnType = "BProjectType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBProjectType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBProjectType(BProjectType entity);

        [ServiceContractDescription(Name = "修改项目类型", Desc = "修改项目类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBProjectType", Get = "", Post = "BProjectType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProjectType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProjectType(BProjectType entity);

        [ServiceContractDescription(Name = "修改项目类型指定的属性", Desc = "修改项目类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBProjectTypeByField", Get = "", Post = "BProjectType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProjectTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProjectTypeByField(BProjectType entity, string fields);

        [ServiceContractDescription(Name = "删除项目类型", Desc = "删除项目类型", Url = "SingleTableService.svc/ST_UDTO_DelBProjectType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBProjectType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBProjectType(long id);

        [ServiceContractDescription(Name = "查询项目类型", Desc = "查询项目类型", Url = "SingleTableService.svc/ST_UDTO_SearchBProjectType", Get = "", Post = "BProjectType", Return = "BaseResultList<BProjectType>", ReturnType = "ListBProjectType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProjectType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProjectType(BProjectType entity);

        [ServiceContractDescription(Name = "查询项目类型(HQL)", Desc = "查询项目类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBProjectTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProjectType>", ReturnType = "ListBProjectType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProjectTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProjectTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询项目类型", Desc = "通过主键ID查询项目类型", Url = "SingleTableService.svc/ST_UDTO_SearchBProjectTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProjectType>", ReturnType = "BProjectType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProjectTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProjectTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BParameter

        [ServiceContractDescription(Name = "新增参数表", Desc = "新增参数表", Url = "SingleTableService.svc/ST_UDTO_AddBParameter", Get = "", Post = "BParameter", Return = "BaseResultDataValue", ReturnType = "BParameter")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBParameter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBParameter(BParameter entity);

        [ServiceContractDescription(Name = "修改参数表", Desc = "修改参数表", Url = "SingleTableService.svc/ST_UDTO_UpdateBParameter", Get = "", Post = "BParameter", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBParameter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBParameter(BParameter entity);

        [ServiceContractDescription(Name = "修改参数表指定的属性", Desc = "修改参数表指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBParameterByField", Get = "", Post = "BParameter", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBParameterByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBParameterByField(BParameter entity, string fields);

        [ServiceContractDescription(Name = "删除参数表", Desc = "删除参数表", Url = "SingleTableService.svc/ST_UDTO_DelBParameter?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBParameter?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBParameter(long id);

        [ServiceContractDescription(Name = "查询参数表", Desc = "查询参数表", Url = "SingleTableService.svc/ST_UDTO_SearchBParameter", Get = "", Post = "BParameter", Return = "BaseResultList<BParameter>", ReturnType = "ListBParameter")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBParameter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBParameter(BParameter entity);

        [ServiceContractDescription(Name = "查询参数表(HQL)", Desc = "查询参数表(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBParameterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BParameter>", ReturnType = "ListBParameter")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBParameterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBParameterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询参数表", Desc = "通过主键ID查询参数表", Url = "SingleTableService.svc/ST_UDTO_SearchBParameterById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BParameter>", ReturnType = "BParameter")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBParameterById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBParameterById(long id, string fields, bool isPlanish);
        #endregion

        #region BPaymentType

        [ServiceContractDescription(Name = "新增收支类型", Desc = "新增收支类型", Url = "SingleTableService.svc/ST_UDTO_AddBPaymentType", Get = "", Post = "BPaymentType", Return = "BaseResultDataValue", ReturnType = "BPaymentType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBPaymentType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBPaymentType(BPaymentType entity);

        [ServiceContractDescription(Name = "修改收支类型", Desc = "修改收支类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBPaymentType", Get = "", Post = "BPaymentType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBPaymentType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBPaymentType(BPaymentType entity);

        [ServiceContractDescription(Name = "修改收支类型指定的属性", Desc = "修改收支类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBPaymentTypeByField", Get = "", Post = "BPaymentType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBPaymentTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBPaymentTypeByField(BPaymentType entity, string fields);

        [ServiceContractDescription(Name = "删除收支类型", Desc = "删除收支类型", Url = "SingleTableService.svc/ST_UDTO_DelBPaymentType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBPaymentType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBPaymentType(long id);

        [ServiceContractDescription(Name = "查询收支类型", Desc = "查询收支类型", Url = "SingleTableService.svc/ST_UDTO_SearchBPaymentType", Get = "", Post = "BPaymentType", Return = "BaseResultList<BPaymentType>", ReturnType = "ListBPaymentType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPaymentType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPaymentType(BPaymentType entity);

        [ServiceContractDescription(Name = "查询收支类型(HQL)", Desc = "查询收支类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBPaymentTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPaymentType>", ReturnType = "ListBPaymentType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPaymentTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPaymentTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询收支类型", Desc = "通过主键ID查询收支类型", Url = "SingleTableService.svc/ST_UDTO_SearchBPaymentTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPaymentType>", ReturnType = "BPaymentType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPaymentTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPaymentTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BProduce

        [ServiceContractDescription(Name = "新增产品", Desc = "新增产品", Url = "SingleTableService.svc/ST_UDTO_AddBProduce", Get = "", Post = "BProduce", Return = "BaseResultDataValue", ReturnType = "BProduce")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBProduce", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBProduce(BProduce entity);

        [ServiceContractDescription(Name = "修改产品", Desc = "修改产品", Url = "SingleTableService.svc/ST_UDTO_UpdateBProduce", Get = "", Post = "BProduce", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProduce", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProduce(BProduce entity);

        [ServiceContractDescription(Name = "修改产品指定的属性", Desc = "修改产品指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBProduceByField", Get = "", Post = "BProduce", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProduceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProduceByField(BProduce entity, string fields);

        [ServiceContractDescription(Name = "删除产品", Desc = "删除产品", Url = "SingleTableService.svc/ST_UDTO_DelBProduce?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBProduce?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBProduce(long id);

        [ServiceContractDescription(Name = "查询产品", Desc = "查询产品", Url = "SingleTableService.svc/ST_UDTO_SearchBProduce", Get = "", Post = "BProduce", Return = "BaseResultList<BProduce>", ReturnType = "ListBProduce")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProduce", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProduce(BProduce entity);

        [ServiceContractDescription(Name = "查询产品(HQL)", Desc = "查询产品(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBProduceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProduce>", ReturnType = "ListBProduce")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProduceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProduceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询产品", Desc = "通过主键ID查询产品", Url = "SingleTableService.svc/ST_UDTO_SearchBProduceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProduce>", ReturnType = "BProduce")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProduceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProduceById(long id, string fields, bool isPlanish);
        #endregion

        #region BProvince

        [ServiceContractDescription(Name = "新增省份", Desc = "新增省份", Url = "SingleTableService.svc/ST_UDTO_AddBProvince", Get = "", Post = "BProvince", Return = "BaseResultDataValue", ReturnType = "BProvince")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBProvince", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBProvince(BProvince entity);

        [ServiceContractDescription(Name = "修改省份", Desc = "修改省份", Url = "SingleTableService.svc/ST_UDTO_UpdateBProvince", Get = "", Post = "BProvince", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProvince", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProvince(BProvince entity);

        [ServiceContractDescription(Name = "修改省份指定的属性", Desc = "修改省份指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBProvinceByField", Get = "", Post = "BProvince", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProvinceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProvinceByField(BProvince entity, string fields);

        [ServiceContractDescription(Name = "删除省份", Desc = "删除省份", Url = "SingleTableService.svc/ST_UDTO_DelBProvince?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBProvince?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBProvince(long id);

        [ServiceContractDescription(Name = "查询省份", Desc = "查询省份", Url = "SingleTableService.svc/ST_UDTO_SearchBProvince", Get = "", Post = "BProvince", Return = "BaseResultList<BProvince>", ReturnType = "ListBProvince")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProvince", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProvince(BProvince entity);

        [ServiceContractDescription(Name = "查询省份(HQL)", Desc = "查询省份(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBProvinceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProvince>", ReturnType = "ListBProvince")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProvinceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProvinceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询省份", Desc = "通过主键ID查询省份", Url = "SingleTableService.svc/ST_UDTO_SearchBProvinceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProvince>", ReturnType = "BProvince")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProvinceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProvinceById(long id, string fields, bool isPlanish);
        #endregion

        #region BPoliticsStatus

        [ServiceContractDescription(Name = "新增政治面貌", Desc = "新增政治面貌", Url = "SingleTableService.svc/ST_UDTO_AddBPoliticsStatus", Get = "", Post = "BPoliticsStatus", Return = "BaseResultDataValue", ReturnType = "BPoliticsStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBPoliticsStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBPoliticsStatus(BPoliticsStatus entity);

        [ServiceContractDescription(Name = "修改政治面貌", Desc = "修改政治面貌", Url = "SingleTableService.svc/ST_UDTO_UpdateBPoliticsStatus", Get = "", Post = "BPoliticsStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBPoliticsStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBPoliticsStatus(BPoliticsStatus entity);

        [ServiceContractDescription(Name = "修改政治面貌指定的属性", Desc = "修改政治面貌指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBPoliticsStatusByField", Get = "", Post = "BPoliticsStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBPoliticsStatusByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBPoliticsStatusByField(BPoliticsStatus entity, string fields);

        [ServiceContractDescription(Name = "删除政治面貌", Desc = "删除政治面貌", Url = "SingleTableService.svc/ST_UDTO_DelBPoliticsStatus?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBPoliticsStatus?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBPoliticsStatus(long id);

        [ServiceContractDescription(Name = "查询政治面貌", Desc = "查询政治面貌", Url = "SingleTableService.svc/ST_UDTO_SearchBPoliticsStatus", Get = "", Post = "BPoliticsStatus", Return = "BaseResultList<BPoliticsStatus>", ReturnType = "ListBPoliticsStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPoliticsStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPoliticsStatus(BPoliticsStatus entity);

        [ServiceContractDescription(Name = "查询政治面貌(HQL)", Desc = "查询政治面貌(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBPoliticsStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPoliticsStatus>", ReturnType = "ListBPoliticsStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPoliticsStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPoliticsStatusByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询政治面貌", Desc = "通过主键ID查询政治面貌", Url = "SingleTableService.svc/ST_UDTO_SearchBPoliticsStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPoliticsStatus>", ReturnType = "BPoliticsStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPoliticsStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPoliticsStatusById(long id, string fields, bool isPlanish);
        #endregion

        #region BProfessionalAbility

        [ServiceContractDescription(Name = "新增专业级别", Desc = "新增专业级别", Url = "SingleTableService.svc/ST_UDTO_AddBProfessionalAbility", Get = "", Post = "BProfessionalAbility", Return = "BaseResultDataValue", ReturnType = "BProfessionalAbility")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBProfessionalAbility", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBProfessionalAbility(BProfessionalAbility entity);

        [ServiceContractDescription(Name = "修改专业级别", Desc = "修改专业级别", Url = "SingleTableService.svc/ST_UDTO_UpdateBProfessionalAbility", Get = "", Post = "BProfessionalAbility", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProfessionalAbility", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProfessionalAbility(BProfessionalAbility entity);

        [ServiceContractDescription(Name = "修改专业级别指定的属性", Desc = "修改专业级别指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBProfessionalAbilityByField", Get = "", Post = "BProfessionalAbility", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProfessionalAbilityByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProfessionalAbilityByField(BProfessionalAbility entity, string fields);

        [ServiceContractDescription(Name = "删除专业级别", Desc = "删除专业级别", Url = "SingleTableService.svc/ST_UDTO_DelBProfessionalAbility?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBProfessionalAbility?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBProfessionalAbility(long id);

        [ServiceContractDescription(Name = "查询专业级别", Desc = "查询专业级别", Url = "SingleTableService.svc/ST_UDTO_SearchBProfessionalAbility", Get = "", Post = "BProfessionalAbility", Return = "BaseResultList<BProfessionalAbility>", ReturnType = "ListBProfessionalAbility")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProfessionalAbility", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProfessionalAbility(BProfessionalAbility entity);

        [ServiceContractDescription(Name = "查询专业级别(HQL)", Desc = "查询专业级别(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBProfessionalAbilityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProfessionalAbility>", ReturnType = "ListBProfessionalAbility")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProfessionalAbilityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProfessionalAbilityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询专业级别", Desc = "通过主键ID查询专业级别", Url = "SingleTableService.svc/ST_UDTO_SearchBProfessionalAbilityById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProfessionalAbility>", ReturnType = "BProfessionalAbility")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProfessionalAbilityById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProfessionalAbilityById(long id, string fields, bool isPlanish);
        #endregion

        #region BPhrase

        [ServiceContractDescription(Name = "新增短语表", Desc = "新增短语表", Url = "SingleTableService.svc/ST_UDTO_AddBPhrase", Get = "", Post = "BPhrase", Return = "BaseResultDataValue", ReturnType = "BPhrase")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBPhrase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBPhrase(BPhrase entity);

        [ServiceContractDescription(Name = "修改短语表", Desc = "修改短语表", Url = "SingleTableService.svc/ST_UDTO_UpdateBPhrase", Get = "", Post = "BPhrase", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBPhrase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBPhrase(BPhrase entity);

        [ServiceContractDescription(Name = "修改短语表指定的属性", Desc = "修改短语表指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBPhraseByField", Get = "", Post = "BPhrase", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBPhraseByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBPhraseByField(BPhrase entity, string fields);

        [ServiceContractDescription(Name = "删除短语表", Desc = "删除短语表", Url = "SingleTableService.svc/ST_UDTO_DelBPhrase?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBPhrase?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBPhrase(long id);

        [ServiceContractDescription(Name = "查询短语表", Desc = "查询短语表", Url = "SingleTableService.svc/ST_UDTO_SearchBPhrase", Get = "", Post = "BPhrase", Return = "BaseResultList<BPhrase>", ReturnType = "ListBPhrase")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPhrase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPhrase(BPhrase entity);

        [ServiceContractDescription(Name = "查询短语表(HQL)", Desc = "查询短语表(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBPhraseByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPhrase>", ReturnType = "ListBPhrase")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPhraseByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPhraseByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询短语表", Desc = "通过主键ID查询短语表", Url = "SingleTableService.svc/ST_UDTO_SearchBPhraseById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPhrase>", ReturnType = "BPhrase")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPhraseById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPhraseById(long id, string fields, bool isPlanish);
        #endregion

        #region BPhraseType

        [ServiceContractDescription(Name = "新增短语类型表", Desc = "新增短语类型表", Url = "SingleTableService.svc/ST_UDTO_AddBPhraseType", Get = "", Post = "BPhraseType", Return = "BaseResultDataValue", ReturnType = "BPhraseType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBPhraseType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBPhraseType(BPhraseType entity);

        [ServiceContractDescription(Name = "修改短语类型表", Desc = "修改短语类型表", Url = "SingleTableService.svc/ST_UDTO_UpdateBPhraseType", Get = "", Post = "BPhraseType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBPhraseType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBPhraseType(BPhraseType entity);

        [ServiceContractDescription(Name = "修改短语类型表指定的属性", Desc = "修改短语类型表指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBPhraseTypeByField", Get = "", Post = "BPhraseType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBPhraseTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBPhraseTypeByField(BPhraseType entity, string fields);

        [ServiceContractDescription(Name = "删除短语类型表", Desc = "删除短语类型表", Url = "SingleTableService.svc/ST_UDTO_DelBPhraseType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBPhraseType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBPhraseType(long id);

        [ServiceContractDescription(Name = "查询短语类型表", Desc = "查询短语类型表", Url = "SingleTableService.svc/ST_UDTO_SearchBPhraseType", Get = "", Post = "BPhraseType", Return = "BaseResultList<BPhraseType>", ReturnType = "ListBPhraseType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPhraseType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPhraseType(BPhraseType entity);

        [ServiceContractDescription(Name = "查询短语类型表(HQL)", Desc = "查询短语类型表(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBPhraseTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPhraseType>", ReturnType = "ListBPhraseType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPhraseTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPhraseTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询短语类型表", Desc = "通过主键ID查询短语类型表", Url = "SingleTableService.svc/ST_UDTO_SearchBPhraseTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPhraseType>", ReturnType = "BPhraseType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPhraseTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPhraseTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BPatientInfo

        [ServiceContractDescription(Name = "新增病人信息", Desc = "新增病人信息", Url = "SingleTableService.svc/ST_UDTO_AddBPatientInfo", Get = "", Post = "BPatientInfo", Return = "BaseResultDataValue", ReturnType = "BPatientInfo")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBPatientInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBPatientInfo(BPatientInfo entity);

        [ServiceContractDescription(Name = "修改病人信息", Desc = "修改病人信息", Url = "SingleTableService.svc/ST_UDTO_UpdateBPatientInfo", Get = "", Post = "BPatientInfo", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBPatientInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBPatientInfo(BPatientInfo entity);

        [ServiceContractDescription(Name = "修改病人信息指定的属性", Desc = "修改病人信息指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBPatientInfoByField", Get = "", Post = "BPatientInfo", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBPatientInfoByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBPatientInfoByField(BPatientInfo entity, string fields);

        [ServiceContractDescription(Name = "删除病人信息", Desc = "删除病人信息", Url = "SingleTableService.svc/ST_UDTO_DelBPatientInfo?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBPatientInfo?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBPatientInfo(long id);

        [ServiceContractDescription(Name = "查询病人信息", Desc = "查询病人信息", Url = "SingleTableService.svc/ST_UDTO_SearchBPatientInfo", Get = "", Post = "BPatientInfo", Return = "BaseResultList<BPatientInfo>", ReturnType = "ListBPatientInfo")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPatientInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPatientInfo(BPatientInfo entity);

        [ServiceContractDescription(Name = "查询病人信息(HQL)", Desc = "查询病人信息(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBPatientInfoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPatientInfo>", ReturnType = "ListBPatientInfo")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPatientInfoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPatientInfoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询病人信息", Desc = "通过主键ID查询病人信息", Url = "SingleTableService.svc/ST_UDTO_SearchBPatientInfoById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPatientInfo>", ReturnType = "BPatientInfo")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPatientInfoById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPatientInfoById(long id, string fields, bool isPlanish);
        #endregion

        #region BReimbursementState

        [ServiceContractDescription(Name = "新增报销状态", Desc = "新增报销状态", Url = "SingleTableService.svc/ST_UDTO_AddBReimbursementState", Get = "", Post = "BReimbursementState", Return = "BaseResultDataValue", ReturnType = "BReimbursementState")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBReimbursementState", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBReimbursementState(BReimbursementState entity);

        [ServiceContractDescription(Name = "修改报销状态", Desc = "修改报销状态", Url = "SingleTableService.svc/ST_UDTO_UpdateBReimbursementState", Get = "", Post = "BReimbursementState", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBReimbursementState", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBReimbursementState(BReimbursementState entity);

        [ServiceContractDescription(Name = "修改报销状态指定的属性", Desc = "修改报销状态指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBReimbursementStateByField", Get = "", Post = "BReimbursementState", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBReimbursementStateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBReimbursementStateByField(BReimbursementState entity, string fields);

        [ServiceContractDescription(Name = "删除报销状态", Desc = "删除报销状态", Url = "SingleTableService.svc/ST_UDTO_DelBReimbursementState?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBReimbursementState?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBReimbursementState(long id);

        [ServiceContractDescription(Name = "查询报销状态", Desc = "查询报销状态", Url = "SingleTableService.svc/ST_UDTO_SearchBReimbursementState", Get = "", Post = "BReimbursementState", Return = "BaseResultList<BReimbursementState>", ReturnType = "ListBReimbursementState")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReimbursementState", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReimbursementState(BReimbursementState entity);

        [ServiceContractDescription(Name = "查询报销状态(HQL)", Desc = "查询报销状态(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBReimbursementStateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReimbursementState>", ReturnType = "ListBReimbursementState")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReimbursementStateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReimbursementStateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询报销状态", Desc = "通过主键ID查询报销状态", Url = "SingleTableService.svc/ST_UDTO_SearchBReimbursementStateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReimbursementState>", ReturnType = "BReimbursementState")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReimbursementStateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReimbursementStateById(long id, string fields, bool isPlanish);
        #endregion

        #region BReimbursementType

        [ServiceContractDescription(Name = "新增报销类型", Desc = "新增报销类型", Url = "SingleTableService.svc/ST_UDTO_AddBReimbursementType", Get = "", Post = "BReimbursementType", Return = "BaseResultDataValue", ReturnType = "BReimbursementType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBReimbursementType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBReimbursementType(BReimbursementType entity);

        [ServiceContractDescription(Name = "修改报销类型", Desc = "修改报销类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBReimbursementType", Get = "", Post = "BReimbursementType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBReimbursementType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBReimbursementType(BReimbursementType entity);

        [ServiceContractDescription(Name = "修改报销类型指定的属性", Desc = "修改报销类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBReimbursementTypeByField", Get = "", Post = "BReimbursementType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBReimbursementTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBReimbursementTypeByField(BReimbursementType entity, string fields);

        [ServiceContractDescription(Name = "删除报销类型", Desc = "删除报销类型", Url = "SingleTableService.svc/ST_UDTO_DelBReimbursementType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBReimbursementType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBReimbursementType(long id);

        [ServiceContractDescription(Name = "查询报销类型", Desc = "查询报销类型", Url = "SingleTableService.svc/ST_UDTO_SearchBReimbursementType", Get = "", Post = "BReimbursementType", Return = "BaseResultList<BReimbursementType>", ReturnType = "ListBReimbursementType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReimbursementType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReimbursementType(BReimbursementType entity);

        [ServiceContractDescription(Name = "查询报销类型(HQL)", Desc = "查询报销类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBReimbursementTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReimbursementType>", ReturnType = "ListBReimbursementType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReimbursementTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReimbursementTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询报销类型", Desc = "通过主键ID查询报销类型", Url = "SingleTableService.svc/ST_UDTO_SearchBReimbursementTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReimbursementType>", ReturnType = "BReimbursementType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReimbursementTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReimbursementTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BReportCycle

        [ServiceContractDescription(Name = "新增报告周期", Desc = "新增报告周期", Url = "SingleTableService.svc/ST_UDTO_AddBReportCycle", Get = "", Post = "BReportCycle", Return = "BaseResultDataValue", ReturnType = "BReportCycle")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBReportCycle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBReportCycle(BReportCycle entity);

        [ServiceContractDescription(Name = "修改报告周期", Desc = "修改报告周期", Url = "SingleTableService.svc/ST_UDTO_UpdateBReportCycle", Get = "", Post = "BReportCycle", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBReportCycle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBReportCycle(BReportCycle entity);

        [ServiceContractDescription(Name = "修改报告周期指定的属性", Desc = "修改报告周期指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBReportCycleByField", Get = "", Post = "BReportCycle", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBReportCycleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBReportCycleByField(BReportCycle entity, string fields);

        [ServiceContractDescription(Name = "删除报告周期", Desc = "删除报告周期", Url = "SingleTableService.svc/ST_UDTO_DelBReportCycle?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBReportCycle?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBReportCycle(long id);

        [ServiceContractDescription(Name = "查询报告周期", Desc = "查询报告周期", Url = "SingleTableService.svc/ST_UDTO_SearchBReportCycle", Get = "", Post = "BReportCycle", Return = "BaseResultList<BReportCycle>", ReturnType = "ListBReportCycle")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReportCycle", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReportCycle(BReportCycle entity);

        [ServiceContractDescription(Name = "查询报告周期(HQL)", Desc = "查询报告周期(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBReportCycleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReportCycle>", ReturnType = "ListBReportCycle")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReportCycleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReportCycleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询报告周期", Desc = "通过主键ID查询报告周期", Url = "SingleTableService.svc/ST_UDTO_SearchBReportCycleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReportCycle>", ReturnType = "BReportCycle")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReportCycleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReportCycleById(long id, string fields, bool isPlanish);
        #endregion

        #region BReportStatus

        [ServiceContractDescription(Name = "新增报告状态", Desc = "新增报告状态", Url = "SingleTableService.svc/ST_UDTO_AddBReportStatus", Get = "", Post = "BReportStatus", Return = "BaseResultDataValue", ReturnType = "BReportStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBReportStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBReportStatus(BReportStatus entity);

        [ServiceContractDescription(Name = "修改报告状态", Desc = "修改报告状态", Url = "SingleTableService.svc/ST_UDTO_UpdateBReportStatus", Get = "", Post = "BReportStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBReportStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBReportStatus(BReportStatus entity);

        [ServiceContractDescription(Name = "修改报告状态指定的属性", Desc = "修改报告状态指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBReportStatusByField", Get = "", Post = "BReportStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBReportStatusByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBReportStatusByField(BReportStatus entity, string fields);

        [ServiceContractDescription(Name = "删除报告状态", Desc = "删除报告状态", Url = "SingleTableService.svc/ST_UDTO_DelBReportStatus?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBReportStatus?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBReportStatus(long id);

        [ServiceContractDescription(Name = "查询报告状态", Desc = "查询报告状态", Url = "SingleTableService.svc/ST_UDTO_SearchBReportStatus", Get = "", Post = "BReportStatus", Return = "BaseResultList<BReportStatus>", ReturnType = "ListBReportStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReportStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReportStatus(BReportStatus entity);

        [ServiceContractDescription(Name = "查询报告状态(HQL)", Desc = "查询报告状态(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBReportStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReportStatus>", ReturnType = "ListBReportStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReportStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReportStatusByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询报告状态", Desc = "通过主键ID查询报告状态", Url = "SingleTableService.svc/ST_UDTO_SearchBReportStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReportStatus>", ReturnType = "BReportStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReportStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReportStatusById(long id, string fields, bool isPlanish);
        #endregion

        #region BReportType

        [ServiceContractDescription(Name = "新增报告类型", Desc = "新增报告类型", Url = "SingleTableService.svc/ST_UDTO_AddBReportType", Get = "", Post = "BReportType", Return = "BaseResultDataValue", ReturnType = "BReportType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBReportType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBReportType(BReportType entity);

        [ServiceContractDescription(Name = "修改报告类型", Desc = "修改报告类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBReportType", Get = "", Post = "BReportType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBReportType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBReportType(BReportType entity);

        [ServiceContractDescription(Name = "修改报告类型指定的属性", Desc = "修改报告类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBReportTypeByField", Get = "", Post = "BReportType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBReportTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBReportTypeByField(BReportType entity, string fields);

        [ServiceContractDescription(Name = "删除报告类型", Desc = "删除报告类型", Url = "SingleTableService.svc/ST_UDTO_DelBReportType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBReportType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBReportType(long id);

        [ServiceContractDescription(Name = "查询报告类型", Desc = "查询报告类型", Url = "SingleTableService.svc/ST_UDTO_SearchBReportType", Get = "", Post = "BReportType", Return = "BaseResultList<BReportType>", ReturnType = "ListBReportType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReportType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReportType(BReportType entity);

        [ServiceContractDescription(Name = "查询报告类型(HQL)", Desc = "查询报告类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBReportTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReportType>", ReturnType = "ListBReportType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReportTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReportTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询报告类型", Desc = "通过主键ID查询报告类型", Url = "SingleTableService.svc/ST_UDTO_SearchBReportTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BReportType>", ReturnType = "BReportType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBReportTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBReportTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BSampleType

        [ServiceContractDescription(Name = "新增样本类型", Desc = "新增样本类型", Url = "SingleTableService.svc/ST_UDTO_AddBSampleType", Get = "", Post = "BSampleType", Return = "BaseResultDataValue", ReturnType = "BSampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSampleType(BSampleType entity);

        [ServiceContractDescription(Name = "修改样本类型", Desc = "修改样本类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBSampleType", Get = "", Post = "BSampleType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleType(BSampleType entity);

        [ServiceContractDescription(Name = "修改样本类型指定的属性", Desc = "修改样本类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBSampleTypeByField", Get = "", Post = "BSampleType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleTypeByField(BSampleType entity, string fields);

        [ServiceContractDescription(Name = "删除样本类型", Desc = "删除样本类型", Url = "SingleTableService.svc/ST_UDTO_DelBSampleType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSampleType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSampleType(long id);

        [ServiceContractDescription(Name = "查询样本类型", Desc = "查询样本类型", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleType", Get = "", Post = "BSampleType", Return = "BaseResultList<BSampleType>", ReturnType = "ListBSampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleType(BSampleType entity);

        [ServiceContractDescription(Name = "查询样本类型(HQL)", Desc = "查询样本类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleType>", ReturnType = "ListBSampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询样本类型", Desc = "通过主键ID查询样本类型", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleType>", ReturnType = "BSampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BSex

        [ServiceContractDescription(Name = "新增性别", Desc = "新增性别", Url = "SingleTableService.svc/ST_UDTO_AddBSex", Get = "", Post = "BSex", Return = "BaseResultDataValue", ReturnType = "BSex")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSex(BSex entity);

        [ServiceContractDescription(Name = "修改性别", Desc = "修改性别", Url = "SingleTableService.svc/ST_UDTO_UpdateBSex", Get = "", Post = "BSex", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSex(BSex entity);

        [ServiceContractDescription(Name = "修改性别指定的属性", Desc = "修改性别指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBSexByField", Get = "", Post = "BSex", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSexByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSexByField(BSex entity, string fields);

        [ServiceContractDescription(Name = "删除性别", Desc = "删除性别", Url = "SingleTableService.svc/ST_UDTO_DelBSex?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSex?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSex(long id);

        [ServiceContractDescription(Name = "查询性别", Desc = "查询性别", Url = "SingleTableService.svc/ST_UDTO_SearchBSex", Get = "", Post = "BSex", Return = "BaseResultList<BSex>", ReturnType = "ListBSex")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSex(BSex entity);

        [ServiceContractDescription(Name = "查询性别(HQL)", Desc = "查询性别(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBSexByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSex>", ReturnType = "ListBSex")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSexByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSexByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询性别", Desc = "通过主键ID查询性别", Url = "SingleTableService.svc/ST_UDTO_SearchBSexById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSex>", ReturnType = "BSex")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSexById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSexById(long id, string fields, bool isPlanish);
        #endregion

        #region BSickType

        [ServiceContractDescription(Name = "新增就诊类型", Desc = "新增就诊类型", Url = "SingleTableService.svc/ST_UDTO_AddBSickType", Get = "", Post = "BSickType", Return = "BaseResultDataValue", ReturnType = "BSickType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSickType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSickType(BSickType entity);

        [ServiceContractDescription(Name = "修改就诊类型", Desc = "修改就诊类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBSickType", Get = "", Post = "BSickType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSickType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSickType(BSickType entity);

        [ServiceContractDescription(Name = "修改就诊类型指定的属性", Desc = "修改就诊类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBSickTypeByField", Get = "", Post = "BSickType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSickTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSickTypeByField(BSickType entity, string fields);

        [ServiceContractDescription(Name = "删除就诊类型", Desc = "删除就诊类型", Url = "SingleTableService.svc/ST_UDTO_DelBSickType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSickType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSickType(long id);

        [ServiceContractDescription(Name = "查询就诊类型", Desc = "查询就诊类型", Url = "SingleTableService.svc/ST_UDTO_SearchBSickType", Get = "", Post = "BSickType", Return = "BaseResultList<BSickType>", ReturnType = "ListBSickType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSickType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSickType(BSickType entity);

        [ServiceContractDescription(Name = "查询就诊类型(HQL)", Desc = "查询就诊类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBSickTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSickType>", ReturnType = "ListBSickType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSickTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSickTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询就诊类型", Desc = "通过主键ID查询就诊类型", Url = "SingleTableService.svc/ST_UDTO_SearchBSickTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSickType>", ReturnType = "BSickType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSickTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSickTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BStatus

        [ServiceContractDescription(Name = "新增状态", Desc = "新增状态", Url = "SingleTableService.svc/ST_UDTO_AddBStatus", Get = "", Post = "BStatus", Return = "BaseResultDataValue", ReturnType = "BStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBStatus(BStatus entity);

        [ServiceContractDescription(Name = "修改状态", Desc = "修改状态", Url = "SingleTableService.svc/ST_UDTO_UpdateBStatus", Get = "", Post = "BStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBStatus(BStatus entity);

        [ServiceContractDescription(Name = "修改状态指定的属性", Desc = "修改状态指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBStatusByField", Get = "", Post = "BStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBStatusByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBStatusByField(BStatus entity, string fields);

        [ServiceContractDescription(Name = "删除状态", Desc = "删除状态", Url = "SingleTableService.svc/ST_UDTO_DelBStatus?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBStatus?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBStatus(long id);

        [ServiceContractDescription(Name = "查询状态", Desc = "查询状态", Url = "SingleTableService.svc/ST_UDTO_SearchBStatus", Get = "", Post = "BStatus", Return = "BaseResultList<BStatus>", ReturnType = "ListBStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBStatus(BStatus entity);

        [ServiceContractDescription(Name = "查询状态(HQL)", Desc = "查询状态(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BStatus>", ReturnType = "ListBStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBStatusByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询状态", Desc = "通过主键ID查询状态", Url = "SingleTableService.svc/ST_UDTO_SearchBStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BStatus>", ReturnType = "BStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBStatusById(long id, string fields, bool isPlanish);
        #endregion

        #region BStorageType

        [ServiceContractDescription(Name = "新增入库类型", Desc = "新增入库类型", Url = "SingleTableService.svc/ST_UDTO_AddBStorageType", Get = "", Post = "BStorageType", Return = "BaseResultDataValue", ReturnType = "BStorageType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBStorageType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBStorageType(BStorageType entity);

        [ServiceContractDescription(Name = "修改入库类型", Desc = "修改入库类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBStorageType", Get = "", Post = "BStorageType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBStorageType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBStorageType(BStorageType entity);

        [ServiceContractDescription(Name = "修改入库类型指定的属性", Desc = "修改入库类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBStorageTypeByField", Get = "", Post = "BStorageType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBStorageTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBStorageTypeByField(BStorageType entity, string fields);

        [ServiceContractDescription(Name = "删除入库类型", Desc = "删除入库类型", Url = "SingleTableService.svc/ST_UDTO_DelBStorageType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBStorageType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBStorageType(long id);

        [ServiceContractDescription(Name = "查询入库类型", Desc = "查询入库类型", Url = "SingleTableService.svc/ST_UDTO_SearchBStorageType", Get = "", Post = "BStorageType", Return = "BaseResultList<BStorageType>", ReturnType = "ListBStorageType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBStorageType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBStorageType(BStorageType entity);

        [ServiceContractDescription(Name = "查询入库类型(HQL)", Desc = "查询入库类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBStorageTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BStorageType>", ReturnType = "ListBStorageType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBStorageTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBStorageTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询入库类型", Desc = "通过主键ID查询入库类型", Url = "SingleTableService.svc/ST_UDTO_SearchBStorageTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BStorageType>", ReturnType = "BStorageType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBStorageTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBStorageTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BStorehouse

        [ServiceContractDescription(Name = "新增仓库", Desc = "新增仓库", Url = "SingleTableService.svc/ST_UDTO_AddBStorehouse", Get = "", Post = "BStorehouse", Return = "BaseResultDataValue", ReturnType = "BStorehouse")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBStorehouse", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBStorehouse(BStorehouse entity);

        [ServiceContractDescription(Name = "修改仓库", Desc = "修改仓库", Url = "SingleTableService.svc/ST_UDTO_UpdateBStorehouse", Get = "", Post = "BStorehouse", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBStorehouse", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBStorehouse(BStorehouse entity);

        [ServiceContractDescription(Name = "修改仓库指定的属性", Desc = "修改仓库指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBStorehouseByField", Get = "", Post = "BStorehouse", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBStorehouseByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBStorehouseByField(BStorehouse entity, string fields);

        [ServiceContractDescription(Name = "删除仓库", Desc = "删除仓库", Url = "SingleTableService.svc/ST_UDTO_DelBStorehouse?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBStorehouse?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBStorehouse(long id);

        [ServiceContractDescription(Name = "查询仓库", Desc = "查询仓库", Url = "SingleTableService.svc/ST_UDTO_SearchBStorehouse", Get = "", Post = "BStorehouse", Return = "BaseResultList<BStorehouse>", ReturnType = "ListBStorehouse")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBStorehouse", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBStorehouse(BStorehouse entity);

        [ServiceContractDescription(Name = "查询仓库(HQL)", Desc = "查询仓库(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBStorehouseByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BStorehouse>", ReturnType = "ListBStorehouse")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBStorehouseByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBStorehouseByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询仓库", Desc = "通过主键ID查询仓库", Url = "SingleTableService.svc/ST_UDTO_SearchBStorehouseById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BStorehouse>", ReturnType = "BStorehouse")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBStorehouseById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBStorehouseById(long id, string fields, bool isPlanish);
        #endregion

        #region BSubject

        [ServiceContractDescription(Name = "新增科目", Desc = "新增科目", Url = "SingleTableService.svc/ST_UDTO_AddBSubject", Get = "", Post = "BSubject", Return = "BaseResultDataValue", ReturnType = "BSubject")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSubject", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSubject(BSubject entity);

        [ServiceContractDescription(Name = "修改科目", Desc = "修改科目", Url = "SingleTableService.svc/ST_UDTO_UpdateBSubject", Get = "", Post = "BSubject", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSubject", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSubject(BSubject entity);

        [ServiceContractDescription(Name = "修改科目指定的属性", Desc = "修改科目指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBSubjectByField", Get = "", Post = "BSubject", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSubjectByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSubjectByField(BSubject entity, string fields);

        [ServiceContractDescription(Name = "删除科目", Desc = "删除科目", Url = "SingleTableService.svc/ST_UDTO_DelBSubject?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSubject?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSubject(long id);

        [ServiceContractDescription(Name = "查询科目", Desc = "查询科目", Url = "SingleTableService.svc/ST_UDTO_SearchBSubject", Get = "", Post = "BSubject", Return = "BaseResultList<BSubject>", ReturnType = "ListBSubject")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSubject", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSubject(BSubject entity);

        [ServiceContractDescription(Name = "查询科目(HQL)", Desc = "查询科目(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBSubjectByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSubject>", ReturnType = "ListBSubject")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSubjectByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSubjectByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询科目", Desc = "通过主键ID查询科目", Url = "SingleTableService.svc/ST_UDTO_SearchBSubjectById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSubject>", ReturnType = "BSubject")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSubjectById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSubjectById(long id, string fields, bool isPlanish);
        #endregion

        #region BSupplier

        [ServiceContractDescription(Name = "新增供应商", Desc = "新增供应商", Url = "SingleTableService.svc/ST_UDTO_AddBSupplier", Get = "", Post = "BSupplier", Return = "BaseResultDataValue", ReturnType = "BSupplier")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSupplier", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSupplier(BSupplier entity);

        [ServiceContractDescription(Name = "修改供应商", Desc = "修改供应商", Url = "SingleTableService.svc/ST_UDTO_UpdateBSupplier", Get = "", Post = "BSupplier", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSupplier", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSupplier(BSupplier entity);

        [ServiceContractDescription(Name = "修改供应商指定的属性", Desc = "修改供应商指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBSupplierByField", Get = "", Post = "BSupplier", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSupplierByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSupplierByField(BSupplier entity, string fields);

        [ServiceContractDescription(Name = "删除供应商", Desc = "删除供应商", Url = "SingleTableService.svc/ST_UDTO_DelBSupplier?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSupplier?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSupplier(long id);

        [ServiceContractDescription(Name = "查询供应商", Desc = "查询供应商", Url = "SingleTableService.svc/ST_UDTO_SearchBSupplier", Get = "", Post = "BSupplier", Return = "BaseResultList<BSupplier>", ReturnType = "ListBSupplier")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSupplier", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSupplier(BSupplier entity);

        [ServiceContractDescription(Name = "查询供应商(HQL)", Desc = "查询供应商(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBSupplierByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSupplier>", ReturnType = "ListBSupplier")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSupplierByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSupplierByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询供应商", Desc = "通过主键ID查询供应商", Url = "SingleTableService.svc/ST_UDTO_SearchBSupplierById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSupplier>", ReturnType = "BSupplier")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSupplierById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSupplierById(long id, string fields, bool isPlanish);
        #endregion

        #region BSpecialty

        [ServiceContractDescription(Name = "新增专业表", Desc = "新增专业表", Url = "SingleTableService.svc/ST_UDTO_AddBSpecialty", Get = "", Post = "BSpecialty", Return = "BaseResultDataValue", ReturnType = "BSpecialty")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSpecialty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSpecialty(BSpecialty entity);

        [ServiceContractDescription(Name = "修改专业表", Desc = "修改专业表", Url = "SingleTableService.svc/ST_UDTO_UpdateBSpecialty", Get = "", Post = "BSpecialty", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSpecialty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSpecialty(BSpecialty entity);

        [ServiceContractDescription(Name = "修改专业表指定的属性", Desc = "修改专业表指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBSpecialtyByField", Get = "", Post = "BSpecialty", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSpecialtyByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSpecialtyByField(BSpecialty entity, string fields);

        [ServiceContractDescription(Name = "删除专业表", Desc = "删除专业表", Url = "SingleTableService.svc/ST_UDTO_DelBSpecialty?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSpecialty?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSpecialty(long id);

        [ServiceContractDescription(Name = "查询专业表", Desc = "查询专业表", Url = "SingleTableService.svc/ST_UDTO_SearchBSpecialty", Get = "", Post = "BSpecialty", Return = "BaseResultList<BSpecialty>", ReturnType = "ListBSpecialty")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSpecialty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSpecialty(BSpecialty entity);

        [ServiceContractDescription(Name = "查询专业表(HQL)", Desc = "查询专业表(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBSpecialtyByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSpecialty>", ReturnType = "ListBSpecialty")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSpecialtyByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSpecialtyByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询专业表", Desc = "通过主键ID查询专业表", Url = "SingleTableService.svc/ST_UDTO_SearchBSpecialtyById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSpecialty>", ReturnType = "BSpecialty")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSpecialtyById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSpecialtyById(long id, string fields, bool isPlanish);
        #endregion

        #region BSpecialtyItem

        //[ServiceContractDescription(Name = "新增专业项目", Desc = "新增专业项目", Url = "SingleTableService.svc/ST_UDTO_AddBSpecialtyItem", Get = "", Post = "BSpecialtyItem", Return = "BaseResultDataValue", ReturnType = "BSpecialtyItem")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSpecialtyItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_AddBSpecialtyItem(BSpecialtyItem entity);

        //[ServiceContractDescription(Name = "修改专业项目", Desc = "修改专业项目", Url = "SingleTableService.svc/ST_UDTO_UpdateBSpecialtyItem", Get = "", Post = "BSpecialtyItem", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSpecialtyItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateBSpecialtyItem(BSpecialtyItem entity);

        //[ServiceContractDescription(Name = "修改专业项目指定的属性", Desc = "修改专业项目指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBSpecialtyItemByField", Get = "", Post = "BSpecialtyItem", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSpecialtyItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateBSpecialtyItemByField(BSpecialtyItem entity, string fields);

        //[ServiceContractDescription(Name = "删除专业项目", Desc = "删除专业项目", Url = "SingleTableService.svc/ST_UDTO_DelBSpecialtyItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSpecialtyItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelBSpecialtyItem(long id);

        //[ServiceContractDescription(Name = "查询专业项目", Desc = "查询专业项目", Url = "SingleTableService.svc/ST_UDTO_SearchBSpecialtyItem", Get = "", Post = "BSpecialtyItem", Return = "BaseResultList<BSpecialtyItem>", ReturnType = "ListBSpecialtyItem")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSpecialtyItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_SearchBSpecialtyItem(BSpecialtyItem entity);

        //[ServiceContractDescription(Name = "查询专业项目(HQL)", Desc = "查询专业项目(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBSpecialtyItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSpecialtyItem>", ReturnType = "ListBSpecialtyItem")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSpecialtyItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_SearchBSpecialtyItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询专业项目", Desc = "通过主键ID查询专业项目", Url = "SingleTableService.svc/ST_UDTO_SearchBSpecialtyItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSpecialtyItem>", ReturnType = "BSpecialtyItem")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSpecialtyItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_SearchBSpecialtyItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BSampleOperate

        [ServiceContractDescription(Name = "新增样本操作", Desc = "新增样本操作", Url = "SingleTableService.svc/ST_UDTO_AddBSampleOperate", Get = "", Post = "BSampleOperate", Return = "BaseResultDataValue", ReturnType = "BSampleOperate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSampleOperate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSampleOperate(BSampleOperate entity);

        [ServiceContractDescription(Name = "修改样本操作", Desc = "修改样本操作", Url = "SingleTableService.svc/ST_UDTO_UpdateBSampleOperate", Get = "", Post = "BSampleOperate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleOperate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleOperate(BSampleOperate entity);

        [ServiceContractDescription(Name = "修改样本操作指定的属性", Desc = "修改样本操作指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBSampleOperateByField", Get = "", Post = "BSampleOperate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleOperateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleOperateByField(BSampleOperate entity, string fields);

        [ServiceContractDescription(Name = "删除样本操作", Desc = "删除样本操作", Url = "SingleTableService.svc/ST_UDTO_DelBSampleOperate?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSampleOperate?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSampleOperate(long id);

        [ServiceContractDescription(Name = "查询样本操作", Desc = "查询样本操作", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleOperate", Get = "", Post = "BSampleOperate", Return = "BaseResultList<BSampleOperate>", ReturnType = "ListBSampleOperate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleOperate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleOperate(BSampleOperate entity);

        [ServiceContractDescription(Name = "查询样本操作(HQL)", Desc = "查询样本操作(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleOperateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleOperate>", ReturnType = "ListBSampleOperate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleOperateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleOperateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询样本操作", Desc = "通过主键ID查询样本操作", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleOperateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleOperate>", ReturnType = "BSampleOperate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleOperateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleOperateById(long id, string fields, bool isPlanish);
        #endregion

        #region BSampleOperateType

        [ServiceContractDescription(Name = "新增样本操作类型", Desc = "新增样本操作类型", Url = "SingleTableService.svc/ST_UDTO_AddBSampleOperateType", Get = "", Post = "BSampleOperateType", Return = "BaseResultDataValue", ReturnType = "BSampleOperateType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSampleOperateType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSampleOperateType(BSampleOperateType entity);

        [ServiceContractDescription(Name = "修改样本操作类型", Desc = "修改样本操作类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBSampleOperateType", Get = "", Post = "BSampleOperateType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleOperateType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleOperateType(BSampleOperateType entity);

        [ServiceContractDescription(Name = "修改样本操作类型指定的属性", Desc = "修改样本操作类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBSampleOperateTypeByField", Get = "", Post = "BSampleOperateType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleOperateTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleOperateTypeByField(BSampleOperateType entity, string fields);

        [ServiceContractDescription(Name = "删除样本操作类型", Desc = "删除样本操作类型", Url = "SingleTableService.svc/ST_UDTO_DelBSampleOperateType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSampleOperateType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSampleOperateType(long id);

        [ServiceContractDescription(Name = "查询样本操作类型", Desc = "查询样本操作类型", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleOperateType", Get = "", Post = "BSampleOperateType", Return = "BaseResultList<BSampleOperateType>", ReturnType = "ListBSampleOperateType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleOperateType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleOperateType(BSampleOperateType entity);

        [ServiceContractDescription(Name = "查询样本操作类型(HQL)", Desc = "查询样本操作类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleOperateTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleOperateType>", ReturnType = "ListBSampleOperateType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleOperateTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleOperateTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询样本操作类型", Desc = "通过主键ID查询样本操作类型", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleOperateTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleOperateType>", ReturnType = "BSampleOperateType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleOperateTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleOperateTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BSampleStatus

        [ServiceContractDescription(Name = "新增样本状态", Desc = "新增样本状态", Url = "SingleTableService.svc/ST_UDTO_AddBSampleStatus", Get = "", Post = "BSampleStatus", Return = "BaseResultDataValue", ReturnType = "BSampleStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSampleStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSampleStatus(BSampleStatus entity);

        [ServiceContractDescription(Name = "修改样本状态", Desc = "修改样本状态", Url = "SingleTableService.svc/ST_UDTO_UpdateBSampleStatus", Get = "", Post = "BSampleStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleStatus(BSampleStatus entity);

        [ServiceContractDescription(Name = "修改样本状态指定的属性", Desc = "修改样本状态指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBSampleStatusByField", Get = "", Post = "BSampleStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleStatusByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleStatusByField(BSampleStatus entity, string fields);

        [ServiceContractDescription(Name = "删除样本状态", Desc = "删除样本状态", Url = "SingleTableService.svc/ST_UDTO_DelBSampleStatus?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSampleStatus?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSampleStatus(long id);

        [ServiceContractDescription(Name = "查询样本状态", Desc = "查询样本状态", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleStatus", Get = "", Post = "BSampleStatus", Return = "BaseResultList<BSampleStatus>", ReturnType = "ListBSampleStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleStatus(BSampleStatus entity);

        [ServiceContractDescription(Name = "查询样本状态(HQL)", Desc = "查询样本状态(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleStatus>", ReturnType = "ListBSampleStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleStatusByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询样本状态", Desc = "通过主键ID查询样本状态", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleStatus>", ReturnType = "BSampleStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleStatusById(long id, string fields, bool isPlanish);
        #endregion

        #region BSampleStatusType

        [ServiceContractDescription(Name = "新增样本状态类型表", Desc = "新增样本状态类型表", Url = "SingleTableService.svc/ST_UDTO_AddBSampleStatusType", Get = "", Post = "BSampleStatusType", Return = "BaseResultDataValue", ReturnType = "BSampleStatusType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSampleStatusType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSampleStatusType(BSampleStatusType entity);

        [ServiceContractDescription(Name = "修改样本状态类型表", Desc = "修改样本状态类型表", Url = "SingleTableService.svc/ST_UDTO_UpdateBSampleStatusType", Get = "", Post = "BSampleStatusType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleStatusType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleStatusType(BSampleStatusType entity);

        [ServiceContractDescription(Name = "修改样本状态类型表指定的属性", Desc = "修改样本状态类型表指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBSampleStatusTypeByField", Get = "", Post = "BSampleStatusType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleStatusTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleStatusTypeByField(BSampleStatusType entity, string fields);

        [ServiceContractDescription(Name = "删除样本状态类型表", Desc = "删除样本状态类型表", Url = "SingleTableService.svc/ST_UDTO_DelBSampleStatusType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSampleStatusType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSampleStatusType(long id);

        [ServiceContractDescription(Name = "查询样本状态类型表", Desc = "查询样本状态类型表", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleStatusType", Get = "", Post = "BSampleStatusType", Return = "BaseResultList<BSampleStatusType>", ReturnType = "ListBSampleStatusType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleStatusType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleStatusType(BSampleStatusType entity);

        [ServiceContractDescription(Name = "查询样本状态类型表(HQL)", Desc = "查询样本状态类型表(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleStatusTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleStatusType>", ReturnType = "ListBSampleStatusType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleStatusTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleStatusTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询样本状态类型表", Desc = "通过主键ID查询样本状态类型表", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleStatusTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleStatusType>", ReturnType = "BSampleStatusType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleStatusTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleStatusTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BTestType

        [ServiceContractDescription(Name = "新增检验类型", Desc = "新增检验类型", Url = "SingleTableService.svc/ST_UDTO_AddBTestType", Get = "", Post = "BTestType", Return = "BaseResultDataValue", ReturnType = "BTestType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBTestType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBTestType(BTestType entity);

        [ServiceContractDescription(Name = "修改检验类型", Desc = "修改检验类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBTestType", Get = "", Post = "BTestType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBTestType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBTestType(BTestType entity);

        [ServiceContractDescription(Name = "修改检验类型指定的属性", Desc = "修改检验类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBTestTypeByField", Get = "", Post = "BTestType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBTestTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBTestTypeByField(BTestType entity, string fields);

        [ServiceContractDescription(Name = "删除检验类型", Desc = "删除检验类型", Url = "SingleTableService.svc/ST_UDTO_DelBTestType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBTestType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBTestType(long id);

        [ServiceContractDescription(Name = "查询检验类型", Desc = "查询检验类型", Url = "SingleTableService.svc/ST_UDTO_SearchBTestType", Get = "", Post = "BTestType", Return = "BaseResultList<BTestType>", ReturnType = "ListBTestType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTestType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTestType(BTestType entity);

        [ServiceContractDescription(Name = "查询检验类型(HQL)", Desc = "查询检验类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBTestTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTestType>", ReturnType = "ListBTestType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTestTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTestTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询检验类型", Desc = "通过主键ID查询检验类型", Url = "SingleTableService.svc/ST_UDTO_SearchBTestTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTestType>", ReturnType = "BTestType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTestTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTestTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BTimeUnit

        [ServiceContractDescription(Name = "新增时间单位", Desc = "新增时间单位", Url = "SingleTableService.svc/ST_UDTO_AddBTimeUnit", Get = "", Post = "BTimeUnit", Return = "BaseResultDataValue", ReturnType = "BTimeUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBTimeUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBTimeUnit(BTimeUnit entity);

        [ServiceContractDescription(Name = "修改时间单位", Desc = "修改时间单位", Url = "SingleTableService.svc/ST_UDTO_UpdateBTimeUnit", Get = "", Post = "BTimeUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBTimeUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBTimeUnit(BTimeUnit entity);

        [ServiceContractDescription(Name = "修改时间单位指定的属性", Desc = "修改时间单位指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBTimeUnitByField", Get = "", Post = "BTimeUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBTimeUnitByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBTimeUnitByField(BTimeUnit entity, string fields);

        [ServiceContractDescription(Name = "删除时间单位", Desc = "删除时间单位", Url = "SingleTableService.svc/ST_UDTO_DelBTimeUnit?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBTimeUnit?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBTimeUnit(long id);

        [ServiceContractDescription(Name = "查询时间单位", Desc = "查询时间单位", Url = "SingleTableService.svc/ST_UDTO_SearchBTimeUnit", Get = "", Post = "BTimeUnit", Return = "BaseResultList<BTimeUnit>", ReturnType = "ListBTimeUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTimeUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTimeUnit(BTimeUnit entity);

        [ServiceContractDescription(Name = "查询时间单位(HQL)", Desc = "查询时间单位(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBTimeUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTimeUnit>", ReturnType = "ListBTimeUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTimeUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTimeUnitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询时间单位", Desc = "通过主键ID查询时间单位", Url = "SingleTableService.svc/ST_UDTO_SearchBTimeUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTimeUnit>", ReturnType = "BTimeUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTimeUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTimeUnitById(long id, string fields, bool isPlanish);
        #endregion

        #region STypeDetail

        [ServiceContractDescription(Name = "新增系统类型明细表", Desc = "新增系统类型明细表", Url = "SingleTableService.svc/ST_UDTO_AddSTypeDetail", Get = "", Post = "STypeDetail", Return = "BaseResultDataValue", ReturnType = "STypeDetail")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSTypeDetail", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSTypeDetail(STypeDetail entity);

        [ServiceContractDescription(Name = "修改系统类型明细表", Desc = "修改系统类型明细表", Url = "SingleTableService.svc/ST_UDTO_UpdateSTypeDetail", Get = "", Post = "STypeDetail", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSTypeDetail", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSTypeDetail(STypeDetail entity);

        [ServiceContractDescription(Name = "修改系统类型明细表指定的属性", Desc = "修改系统类型明细表指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateSTypeDetailByField", Get = "", Post = "STypeDetail", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSTypeDetailByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSTypeDetailByField(STypeDetail entity, string fields);

        [ServiceContractDescription(Name = "删除系统类型明细表", Desc = "删除系统类型明细表", Url = "SingleTableService.svc/ST_UDTO_DelSTypeDetail?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSTypeDetail?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSTypeDetail(long id);

        [ServiceContractDescription(Name = "查询系统类型明细表", Desc = "查询系统类型明细表", Url = "SingleTableService.svc/ST_UDTO_SearchSTypeDetail", Get = "", Post = "STypeDetail", Return = "BaseResultList<STypeDetail>", ReturnType = "ListSTypeDetail")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSTypeDetail", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSTypeDetail(STypeDetail entity);

        [ServiceContractDescription(Name = "查询系统类型明细表(HQL)", Desc = "查询系统类型明细表(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchSTypeDetailByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<STypeDetail>", ReturnType = "ListSTypeDetail")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSTypeDetailByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSTypeDetailByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询系统类型明细表", Desc = "通过主键ID查询系统类型明细表", Url = "SingleTableService.svc/ST_UDTO_SearchSTypeDetailById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<STypeDetail>", ReturnType = "STypeDetail")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSTypeDetailById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSTypeDetailById(long id, string fields, bool isPlanish);
        #endregion

        #region SType

        [ServiceContractDescription(Name = "新增系统类型表", Desc = "新增系统类型表", Url = "SingleTableService.svc/ST_UDTO_AddSType", Get = "", Post = "SType", Return = "BaseResultDataValue", ReturnType = "SType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSType(SType entity);

        [ServiceContractDescription(Name = "修改系统类型表", Desc = "修改系统类型表", Url = "SingleTableService.svc/ST_UDTO_UpdateSType", Get = "", Post = "SType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSType(SType entity);

        [ServiceContractDescription(Name = "修改系统类型表指定的属性", Desc = "修改系统类型表指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateSTypeByField", Get = "", Post = "SType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSTypeByField(SType entity, string fields);

        [ServiceContractDescription(Name = "删除系统类型表", Desc = "删除系统类型表", Url = "SingleTableService.svc/ST_UDTO_DelSType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSType(long id);

        [ServiceContractDescription(Name = "查询系统类型表", Desc = "查询系统类型表", Url = "SingleTableService.svc/ST_UDTO_SearchSType", Get = "", Post = "SType", Return = "BaseResultList<SType>", ReturnType = "ListSType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSType(SType entity);

        [ServiceContractDescription(Name = "查询系统类型表(HQL)", Desc = "查询系统类型表(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchSTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SType>", ReturnType = "ListSType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询系统类型表", Desc = "通过主键ID查询系统类型表", Url = "SingleTableService.svc/ST_UDTO_SearchSTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SType>", ReturnType = "SType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BURLGroup

        [ServiceContractDescription(Name = "新增URL类别", Desc = "新增URL类别", Url = "SingleTableService.svc/ST_UDTO_AddBURLGroup", Get = "", Post = "BURLGroup", Return = "BaseResultDataValue", ReturnType = "BURLGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBURLGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBURLGroup(BURLGroup entity);

        [ServiceContractDescription(Name = "修改URL类别", Desc = "修改URL类别", Url = "SingleTableService.svc/ST_UDTO_UpdateBURLGroup", Get = "", Post = "BURLGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBURLGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBURLGroup(BURLGroup entity);

        [ServiceContractDescription(Name = "修改URL类别指定的属性", Desc = "修改URL类别指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBURLGroupByField", Get = "", Post = "BURLGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBURLGroupByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBURLGroupByField(BURLGroup entity, string fields);

        [ServiceContractDescription(Name = "删除URL类别", Desc = "删除URL类别", Url = "SingleTableService.svc/ST_UDTO_DelBURLGroup?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBURLGroup?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBURLGroup(long id);

        [ServiceContractDescription(Name = "查询URL类别", Desc = "查询URL类别", Url = "SingleTableService.svc/ST_UDTO_SearchBURLGroup", Get = "", Post = "BURLGroup", Return = "BaseResultList<BURLGroup>", ReturnType = "ListBURLGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBURLGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBURLGroup(BURLGroup entity);

        [ServiceContractDescription(Name = "查询URL类别(HQL)", Desc = "查询URL类别(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBURLGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BURLGroup>", ReturnType = "ListBURLGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBURLGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBURLGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询URL类别", Desc = "通过主键ID查询URL类别", Url = "SingleTableService.svc/ST_UDTO_SearchBURLGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BURLGroup>", ReturnType = "BURLGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBURLGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBURLGroupById(long id, string fields, bool isPlanish);
        #endregion

        #region BURLType

        [ServiceContractDescription(Name = "新增URL类型", Desc = "新增URL类型", Url = "SingleTableService.svc/ST_UDTO_AddBURLType", Get = "", Post = "BURLType", Return = "BaseResultDataValue", ReturnType = "BURLType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBURLType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBURLType(BURLType entity);

        [ServiceContractDescription(Name = "修改URL类型", Desc = "修改URL类型", Url = "SingleTableService.svc/ST_UDTO_UpdateBURLType", Get = "", Post = "BURLType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBURLType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBURLType(BURLType entity);

        [ServiceContractDescription(Name = "修改URL类型指定的属性", Desc = "修改URL类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBURLTypeByField", Get = "", Post = "BURLType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBURLTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBURLTypeByField(BURLType entity, string fields);

        [ServiceContractDescription(Name = "删除URL类型", Desc = "删除URL类型", Url = "SingleTableService.svc/ST_UDTO_DelBURLType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBURLType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBURLType(long id);

        [ServiceContractDescription(Name = "查询URL类型", Desc = "查询URL类型", Url = "SingleTableService.svc/ST_UDTO_SearchBURLType", Get = "", Post = "BURLType", Return = "BaseResultList<BURLType>", ReturnType = "ListBURLType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBURLType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBURLType(BURLType entity);

        [ServiceContractDescription(Name = "查询URL类型(HQL)", Desc = "查询URL类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBURLTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BURLType>", ReturnType = "ListBURLType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBURLTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBURLTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询URL类型", Desc = "通过主键ID查询URL类型", Url = "SingleTableService.svc/ST_UDTO_SearchBURLTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BURLType>", ReturnType = "BURLType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBURLTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBURLTypeById(long id, string fields, bool isPlanish);
        #endregion        

        #region ItemAllItem

        [ServiceContractDescription(Name = "新增所有项目", Desc = "新增所有项目", Url = "SingleTableService.svc/ST_UDTO_AddItemAllItem", Get = "", Post = "ItemAllItem", Return = "BaseResultDataValue", ReturnType = "ItemAllItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddItemAllItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddItemAllItem(ItemAllItem entity);

        [ServiceContractDescription(Name = "修改所有项目", Desc = "修改所有项目", Url = "SingleTableService.svc/ST_UDTO_UpdateItemAllItem", Get = "", Post = "ItemAllItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateItemAllItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateItemAllItem(ItemAllItem entity);

        [ServiceContractDescription(Name = "修改所有项目指定的属性", Desc = "修改所有项目指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateItemAllItemByField", Get = "", Post = "ItemAllItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateItemAllItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateItemAllItemByField(ItemAllItem entity, string fields);

        [ServiceContractDescription(Name = "删除所有项目", Desc = "删除所有项目", Url = "SingleTableService.svc/ST_UDTO_DelItemAllItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelItemAllItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelItemAllItem(long id);

        [ServiceContractDescription(Name = "查询所有项目", Desc = "查询所有项目", Url = "SingleTableService.svc/ST_UDTO_SearchItemAllItem", Get = "", Post = "ItemAllItem", Return = "BaseResultList<ItemAllItem>", ReturnType = "ListItemAllItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemAllItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemAllItem(ItemAllItem entity);

        [ServiceContractDescription(Name = "查询所有项目(HQL)", Desc = "查询所有项目(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchItemAllItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemAllItem>", ReturnType = "ListItemAllItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemAllItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemAllItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询所有项目", Desc = "通过主键ID查询所有项目", Url = "SingleTableService.svc/ST_UDTO_SearchItemAllItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemAllItem>", ReturnType = "ItemAllItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemAllItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemAllItemById(long id, string fields, bool isPlanish);
        #endregion

        #region ItemItemCon

        [ServiceContractDescription(Name = "新增项目关系", Desc = "新增项目关系", Url = "SingleTableService.svc/ST_UDTO_AddItemItemCon", Get = "", Post = "ItemItemCon", Return = "BaseResultDataValue", ReturnType = "ItemItemCon")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddItemItemCon", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddItemItemCon(ItemItemCon entity);

        [ServiceContractDescription(Name = "修改项目关系", Desc = "修改项目关系", Url = "SingleTableService.svc/ST_UDTO_UpdateItemItemCon", Get = "", Post = "ItemItemCon", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateItemItemCon", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateItemItemCon(ItemItemCon entity);

        [ServiceContractDescription(Name = "修改项目关系指定的属性", Desc = "修改项目关系指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateItemItemConByField", Get = "", Post = "ItemItemCon", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateItemItemConByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateItemItemConByField(ItemItemCon entity, string fields);

        [ServiceContractDescription(Name = "删除项目关系", Desc = "删除项目关系", Url = "SingleTableService.svc/ST_UDTO_DelItemItemCon?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelItemItemCon?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelItemItemCon(long id);

        [ServiceContractDescription(Name = "查询项目关系", Desc = "查询项目关系", Url = "SingleTableService.svc/ST_UDTO_SearchItemItemCon", Get = "", Post = "ItemItemCon", Return = "BaseResultList<ItemItemCon>", ReturnType = "ListItemItemCon")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemItemCon", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemItemCon(ItemItemCon entity);

        [ServiceContractDescription(Name = "查询项目关系(HQL)", Desc = "查询项目关系(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchItemItemConByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemItemCon>", ReturnType = "ListItemItemCon")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemItemConByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemItemConByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询项目关系", Desc = "通过主键ID查询项目关系", Url = "SingleTableService.svc/ST_UDTO_SearchItemItemConById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemItemCon>", ReturnType = "ItemItemCon")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemItemConById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemItemConById(long id, string fields, bool isPlanish);
        #endregion

        #region EPBEquip

        [ServiceContractDescription(Name = "新增仪器表", Desc = "新增仪器表", Url = "SingleTableService.svc/ST_UDTO_AddEPBEquip", Get = "", Post = "EPBEquip", Return = "BaseResultDataValue", ReturnType = "EPBEquip")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddEPBEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddEPBEquip(EPBEquip entity);

        [ServiceContractDescription(Name = "修改仪器表", Desc = "修改仪器表", Url = "SingleTableService.svc/ST_UDTO_UpdateEPBEquip", Get = "", Post = "EPBEquip", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEPBEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEPBEquip(EPBEquip entity);

        [ServiceContractDescription(Name = "修改仪器表指定的属性", Desc = "修改仪器表指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateEPBEquipByField", Get = "", Post = "EPBEquip", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEPBEquipByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEPBEquipByField(EPBEquip entity, string fields);

        [ServiceContractDescription(Name = "删除仪器表", Desc = "删除仪器表", Url = "SingleTableService.svc/ST_UDTO_DelEPBEquip?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelEPBEquip?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelEPBEquip(long id);

        [ServiceContractDescription(Name = "查询仪器表", Desc = "查询仪器表", Url = "SingleTableService.svc/ST_UDTO_SearchEPBEquip", Get = "", Post = "EPBEquip", Return = "BaseResultList<EPBEquip>", ReturnType = "ListEPBEquip")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEPBEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEPBEquip(EPBEquip entity);

        [ServiceContractDescription(Name = "查询仪器表(HQL)", Desc = "查询仪器表(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchEPBEquipByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EPBEquip>", ReturnType = "ListEPBEquip")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEPBEquipByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEPBEquipByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询仪器表", Desc = "通过主键ID查询仪器表", Url = "SingleTableService.svc/ST_UDTO_SearchEPBEquipById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EPBEquip>", ReturnType = "EPBEquip")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEPBEquipById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEPBEquipById(long id, string fields, bool isPlanish);
        #endregion

        #region EPEquipItem

        [ServiceContractDescription(Name = "新增仪器项目关系表", Desc = "新增仪器项目关系表", Url = "SingleTableService.svc/ST_UDTO_AddEPEquipItem", Get = "", Post = "EPEquipItem", Return = "BaseResultDataValue", ReturnType = "EPEquipItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddEPEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddEPEquipItem(EPEquipItem entity);

        [ServiceContractDescription(Name = "修改仪器项目关系表", Desc = "修改仪器项目关系表", Url = "SingleTableService.svc/ST_UDTO_UpdateEPEquipItem", Get = "", Post = "EPEquipItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEPEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEPEquipItem(EPEquipItem entity);

        [ServiceContractDescription(Name = "修改仪器项目关系表指定的属性", Desc = "修改仪器项目关系表指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateEPEquipItemByField", Get = "", Post = "EPEquipItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEPEquipItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEPEquipItemByField(EPEquipItem entity, string fields);

        [ServiceContractDescription(Name = "删除仪器项目关系表", Desc = "删除仪器项目关系表", Url = "SingleTableService.svc/ST_UDTO_DelEPEquipItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelEPEquipItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelEPEquipItem(long id);

        [ServiceContractDescription(Name = "查询仪器项目关系表", Desc = "查询仪器项目关系表", Url = "SingleTableService.svc/ST_UDTO_SearchEPEquipItem", Get = "", Post = "EPEquipItem", Return = "BaseResultList<EPEquipItem>", ReturnType = "ListEPEquipItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEPEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEPEquipItem(EPEquipItem entity);

        [ServiceContractDescription(Name = "查询仪器项目关系表(HQL)", Desc = "查询仪器项目关系表(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchEPEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EPEquipItem>", ReturnType = "ListEPEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEPEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEPEquipItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询仪器项目关系表", Desc = "通过主键ID查询仪器项目关系表", Url = "SingleTableService.svc/ST_UDTO_SearchEPEquipItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EPEquipItem>", ReturnType = "EPEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEPEquipItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEPEquipItemById(long id, string fields, bool isPlanish);
        #endregion

        #region EPModule

        [ServiceContractDescription(Name = "新增仪器模块", Desc = "新增仪器模块", Url = "SingleTableService.svc/ST_UDTO_AddEPModule", Get = "", Post = "EPModule", Return = "BaseResultDataValue", ReturnType = "EPModule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddEPModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddEPModule(EPModule entity);

        [ServiceContractDescription(Name = "修改仪器模块", Desc = "修改仪器模块", Url = "SingleTableService.svc/ST_UDTO_UpdateEPModule", Get = "", Post = "EPModule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEPModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEPModule(EPModule entity);

        [ServiceContractDescription(Name = "修改仪器模块指定的属性", Desc = "修改仪器模块指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateEPModuleByField", Get = "", Post = "EPModule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEPModuleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEPModuleByField(EPModule entity, string fields);

        [ServiceContractDescription(Name = "删除仪器模块", Desc = "删除仪器模块", Url = "SingleTableService.svc/ST_UDTO_DelEPModule?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelEPModule?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelEPModule(long id);

        [ServiceContractDescription(Name = "查询仪器模块", Desc = "查询仪器模块", Url = "SingleTableService.svc/ST_UDTO_SearchEPModule", Get = "", Post = "EPModule", Return = "BaseResultList<EPModule>", ReturnType = "ListEPModule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEPModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEPModule(EPModule entity);

        [ServiceContractDescription(Name = "查询仪器模块(HQL)", Desc = "查询仪器模块(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchEPModuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EPModule>", ReturnType = "ListEPModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEPModuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEPModuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询仪器模块", Desc = "通过主键ID查询仪器模块", Url = "SingleTableService.svc/ST_UDTO_SearchEPModuleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EPModule>", ReturnType = "EPModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEPModuleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEPModuleById(long id, string fields, bool isPlanish);
        #endregion

        #region GMGroupType

        [ServiceContractDescription(Name = "新增小组类型", Desc = "新增小组类型", Url = "SingleTableService.svc/ST_UDTO_AddGMGroupType", Get = "", Post = "GMGroupType", Return = "BaseResultDataValue", ReturnType = "GMGroupType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddGMGroupType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddGMGroupType(GMGroupType entity);

        [ServiceContractDescription(Name = "修改小组类型", Desc = "修改小组类型", Url = "SingleTableService.svc/ST_UDTO_UpdateGMGroupType", Get = "", Post = "GMGroupType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGMGroupType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGMGroupType(GMGroupType entity);

        [ServiceContractDescription(Name = "修改小组类型指定的属性", Desc = "修改小组类型指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateGMGroupTypeByField", Get = "", Post = "GMGroupType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGMGroupTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGMGroupTypeByField(GMGroupType entity, string fields);

        [ServiceContractDescription(Name = "删除小组类型", Desc = "删除小组类型", Url = "SingleTableService.svc/ST_UDTO_DelGMGroupType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelGMGroupType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelGMGroupType(long id);

        [ServiceContractDescription(Name = "查询小组类型", Desc = "查询小组类型", Url = "SingleTableService.svc/ST_UDTO_SearchGMGroupType", Get = "", Post = "GMGroupType", Return = "BaseResultList<GMGroupType>", ReturnType = "ListGMGroupType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGMGroupType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGMGroupType(GMGroupType entity);

        [ServiceContractDescription(Name = "查询小组类型(HQL)", Desc = "查询小组类型(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchGMGroupTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GMGroupType>", ReturnType = "ListGMGroupType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGMGroupTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGMGroupTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询小组类型", Desc = "通过主键ID查询小组类型", Url = "SingleTableService.svc/ST_UDTO_SearchGMGroupTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GMGroupType>", ReturnType = "GMGroupType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGMGroupTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGMGroupTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region GMGroup

        [ServiceContractDescription(Name = "新增小组表", Desc = "新增小组表", Url = "SingleTableService.svc/ST_UDTO_AddGMGroup", Get = "", Post = "GMGroup", Return = "BaseResultDataValue", ReturnType = "GMGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddGMGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddGMGroup(GMGroup entity);

        [ServiceContractDescription(Name = "修改小组表", Desc = "修改小组表", Url = "SingleTableService.svc/ST_UDTO_UpdateGMGroup", Get = "", Post = "GMGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGMGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGMGroup(GMGroup entity);

        [ServiceContractDescription(Name = "修改小组表指定的属性", Desc = "修改小组表指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateGMGroupByField", Get = "", Post = "GMGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGMGroupByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGMGroupByField(GMGroup entity, string fields);

        [ServiceContractDescription(Name = "删除小组表", Desc = "删除小组表", Url = "SingleTableService.svc/ST_UDTO_DelGMGroup?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelGMGroup?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelGMGroup(long id);

        [ServiceContractDescription(Name = "查询小组表", Desc = "查询小组表", Url = "SingleTableService.svc/ST_UDTO_SearchGMGroup", Get = "", Post = "GMGroup", Return = "BaseResultList<GMGroup>", ReturnType = "ListGMGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGMGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGMGroup(GMGroup entity);

        [ServiceContractDescription(Name = "查询小组表(HQL)", Desc = "查询小组表(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchGMGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GMGroup>", ReturnType = "ListGMGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGMGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGMGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询小组表", Desc = "通过主键ID查询小组表", Url = "SingleTableService.svc/ST_UDTO_SearchGMGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GMGroup>", ReturnType = "GMGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGMGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGMGroupById(long id, string fields, bool isPlanish);
        #endregion

        #region GMGroupItem

        [ServiceContractDescription(Name = "新增小组项目", Desc = "新增小组项目", Url = "SingleTableService.svc/ST_UDTO_AddGMGroupItem", Get = "", Post = "GMGroupItem", Return = "BaseResultDataValue", ReturnType = "GMGroupItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddGMGroupItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddGMGroupItem(GMGroupItem entity);

        [ServiceContractDescription(Name = "修改小组项目", Desc = "修改小组项目", Url = "SingleTableService.svc/ST_UDTO_UpdateGMGroupItem", Get = "", Post = "GMGroupItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGMGroupItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGMGroupItem(GMGroupItem entity);

        [ServiceContractDescription(Name = "修改小组项目指定的属性", Desc = "修改小组项目指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateGMGroupItemByField", Get = "", Post = "GMGroupItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGMGroupItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGMGroupItemByField(GMGroupItem entity, string fields);

        [ServiceContractDescription(Name = "删除小组项目", Desc = "删除小组项目", Url = "SingleTableService.svc/ST_UDTO_DelGMGroupItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelGMGroupItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelGMGroupItem(long id);

        [ServiceContractDescription(Name = "查询小组项目", Desc = "查询小组项目", Url = "SingleTableService.svc/ST_UDTO_SearchGMGroupItem", Get = "", Post = "GMGroupItem", Return = "BaseResultList<GMGroupItem>", ReturnType = "ListGMGroupItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGMGroupItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGMGroupItem(GMGroupItem entity);

        [ServiceContractDescription(Name = "查询小组项目(HQL)", Desc = "查询小组项目(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchGMGroupItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GMGroupItem>", ReturnType = "ListGMGroupItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGMGroupItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGMGroupItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询小组项目", Desc = "通过主键ID查询小组项目", Url = "SingleTableService.svc/ST_UDTO_SearchGMGroupItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GMGroupItem>", ReturnType = "GMGroupItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGMGroupItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGMGroupItemById(long id, string fields, bool isPlanish);
        #endregion

        #region GMGroupEquip

        [ServiceContractDescription(Name = "新增小组仪器", Desc = "新增小组仪器", Url = "SingleTableService.svc/ST_UDTO_AddGMGroupEquip", Get = "", Post = "GMGroupEquip", Return = "BaseResultDataValue", ReturnType = "GMGroupEquip")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddGMGroupEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddGMGroupEquip(GMGroupEquip entity);

        [ServiceContractDescription(Name = "修改小组仪器", Desc = "修改小组仪器", Url = "SingleTableService.svc/ST_UDTO_UpdateGMGroupEquip", Get = "", Post = "GMGroupEquip", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGMGroupEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGMGroupEquip(GMGroupEquip entity);

        [ServiceContractDescription(Name = "修改小组仪器指定的属性", Desc = "修改小组仪器指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateGMGroupEquipByField", Get = "", Post = "GMGroupEquip", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGMGroupEquipByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGMGroupEquipByField(GMGroupEquip entity, string fields);

        [ServiceContractDescription(Name = "删除小组仪器", Desc = "删除小组仪器", Url = "SingleTableService.svc/ST_UDTO_DelGMGroupEquip?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelGMGroupEquip?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelGMGroupEquip(long id);

        [ServiceContractDescription(Name = "查询小组仪器", Desc = "查询小组仪器", Url = "SingleTableService.svc/ST_UDTO_SearchGMGroupEquip", Get = "", Post = "GMGroupEquip", Return = "BaseResultList<GMGroupEquip>", ReturnType = "ListGMGroupEquip")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGMGroupEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGMGroupEquip(GMGroupEquip entity);

        [ServiceContractDescription(Name = "查询小组仪器(HQL)", Desc = "查询小组仪器(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchGMGroupEquipByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GMGroupEquip>", ReturnType = "ListGMGroupEquip")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGMGroupEquipByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGMGroupEquipByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询小组仪器", Desc = "通过主键ID查询小组仪器", Url = "SingleTableService.svc/ST_UDTO_SearchGMGroupEquipById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GMGroupEquip>", ReturnType = "GMGroupEquip")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGMGroupEquipById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGMGroupEquipById(long id, string fields, bool isPlanish);
        #endregion

        #region BDic

        [ServiceContractDescription(Name = "新增字典", Desc = "新增字典", Url = "SingleTableService.svc/ST_UDTO_AddBDic", Get = "", Post = "BDic", Return = "BaseResultDataValue", ReturnType = "BDic")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBDic", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBDic(BDic entity);

        [ServiceContractDescription(Name = "修改字典", Desc = "修改字典", Url = "SingleTableService.svc/ST_UDTO_UpdateBDic", Get = "", Post = "BDic", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDic", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDic(BDic entity);

        [ServiceContractDescription(Name = "修改字典指定的属性", Desc = "修改字典指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBDicByField", Get = "", Post = "BDic", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDicByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDicByField(BDic entity, string fields);

        [ServiceContractDescription(Name = "删除字典", Desc = "删除字典", Url = "SingleTableService.svc/ST_UDTO_DelBDic?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBDic?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBDic(long id);

        [ServiceContractDescription(Name = "查询字典", Desc = "查询字典", Url = "SingleTableService.svc/ST_UDTO_SearchBDic", Get = "", Post = "BDic", Return = "BaseResultList<BDic>", ReturnType = "ListBDic")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDic", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDic(BDic entity);

        [ServiceContractDescription(Name = "查询字典(HQL)", Desc = "查询字典(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBDicByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDic>", ReturnType = "ListBDic")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDicByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDicByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询字典", Desc = "通过主键ID查询字典", Url = "SingleTableService.svc/ST_UDTO_SearchBDicById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDic>", ReturnType = "BDic")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDicById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDicById(long id, string fields, bool isPlanish);
        #endregion

        #region BDicClass

        [ServiceContractDescription(Name = "新增字典类别", Desc = "新增字典类别", Url = "SingleTableService.svc/ST_UDTO_AddBDicClass", Get = "", Post = "BDicClass", Return = "BaseResultDataValue", ReturnType = "BDicClass")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBDicClass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBDicClass(BDicClass entity);

        [ServiceContractDescription(Name = "修改字典类别", Desc = "修改字典类别", Url = "SingleTableService.svc/ST_UDTO_UpdateBDicClass", Get = "", Post = "BDicClass", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDicClass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDicClass(BDicClass entity);

        [ServiceContractDescription(Name = "修改字典类别指定的属性", Desc = "修改字典类别指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBDicClassByField", Get = "", Post = "BDicClass", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDicClassByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDicClassByField(BDicClass entity, string fields);

        [ServiceContractDescription(Name = "删除字典类别", Desc = "删除字典类别", Url = "SingleTableService.svc/ST_UDTO_DelBDicClass?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBDicClass?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBDicClass(long id);

        [ServiceContractDescription(Name = "查询字典类别", Desc = "查询字典类别", Url = "SingleTableService.svc/ST_UDTO_SearchBDicClass", Get = "", Post = "BDicClass", Return = "BaseResultList<BDicClass>", ReturnType = "ListBDicClass")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDicClass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDicClass(BDicClass entity);

        [ServiceContractDescription(Name = "查询字典类别(HQL)", Desc = "查询字典类别(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBDicClassByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDicClass>", ReturnType = "ListBDicClass")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDicClassByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDicClassByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询字典类别", Desc = "通过主键ID查询字典类别", Url = "SingleTableService.svc/ST_UDTO_SearchBDicClassById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDicClass>", ReturnType = "BDicClass")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDicClassById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDicClassById(long id, string fields, bool isPlanish);
        #endregion

        

    }
}
