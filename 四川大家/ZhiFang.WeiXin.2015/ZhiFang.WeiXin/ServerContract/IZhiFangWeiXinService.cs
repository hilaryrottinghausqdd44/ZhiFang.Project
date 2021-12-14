
using System.ServiceModel.Web;
using System.ServiceModel;
using ZhiFang.WeiXin.BusinessObject;
using System.ServiceModel.Channels;
using System.IO;
using ZhiFang.Entity.Base;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.Entity.ViewObject.Response;
using ZhiFang.WeiXin.Entity.ViewObject.Request;
using System.Collections.Generic;

namespace ZhiFang.WeiXin.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.WeiXinService")]
    public interface IZhiFangWeiXinService
    {
        #region BDictType

        [ServiceContractDescription(Name = "新增字典类型表", Desc = "新增字典类型表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddBDictType", Get = "", Post = "BDictType", Return = "BaseResultDataValue", ReturnType = "BDictType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBDictType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBDictType(BDictType entity);

        [ServiceContractDescription(Name = "修改字典类型表", Desc = "修改字典类型表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBDictType", Get = "", Post = "BDictType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDictType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDictType(BDictType entity);

        [ServiceContractDescription(Name = "修改字典类型表指定的属性", Desc = "修改字典类型表指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBDictTypeByField", Get = "", Post = "BDictType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDictTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDictTypeByField(BDictType entity, string fields);

        [ServiceContractDescription(Name = "删除字典类型表", Desc = "删除字典类型表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelBDictType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBDictType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBDictType(long id);

        [ServiceContractDescription(Name = "查询字典类型表", Desc = "查询字典类型表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBDictType", Get = "", Post = "BDictType", Return = "BaseResultList<BDictType>", ReturnType = "ListBDictType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDictType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDictType(BDictType entity);

        [ServiceContractDescription(Name = "查询字典类型表(HQL)", Desc = "查询字典类型表(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBDictTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDictType>", ReturnType = "ListBDictType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDictTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDictTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询字典类型表", Desc = "通过主键ID查询字典类型表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBDictTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDictType>", ReturnType = "BDictType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDictTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDictTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BDict

        [ServiceContractDescription(Name = "新增字典表", Desc = "新增字典表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddBDict", Get = "", Post = "BDict", Return = "BaseResultDataValue", ReturnType = "BDict")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBDict(BDict entity);

        [ServiceContractDescription(Name = "修改字典表", Desc = "修改字典表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBDict", Get = "", Post = "BDict", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDict(BDict entity);

        [ServiceContractDescription(Name = "修改字典表指定的属性", Desc = "修改字典表指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBDictByField", Get = "", Post = "BDict", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDictByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDictByField(BDict entity, string fields);

        [ServiceContractDescription(Name = "删除字典表", Desc = "删除字典表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelBDict?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBDict?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBDict(long id);

        [ServiceContractDescription(Name = "查询字典表", Desc = "查询字典表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBDict", Get = "", Post = "BDict", Return = "BaseResultList<BDict>", ReturnType = "ListBDict")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDict(BDict entity);

        [ServiceContractDescription(Name = "查询字典表(HQL)", Desc = "查询字典表(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBDictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDict>", ReturnType = "ListBDict")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDictByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询字典表", Desc = "通过主键ID查询字典表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBDictById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDict>", ReturnType = "BDict")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDictById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDictById(long id, string fields, bool isPlanish);
        #endregion

        #region BParameter

        [ServiceContractDescription(Name = "新增参数表", Desc = "新增参数表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddBParameter", Get = "", Post = "BParameter", Return = "BaseResultDataValue", ReturnType = "BParameter")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBParameter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBParameter(BParameter entity);

        [ServiceContractDescription(Name = "修改参数表", Desc = "修改参数表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBParameter", Get = "", Post = "BParameter", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBParameter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBParameter(BParameter entity);

        [ServiceContractDescription(Name = "修改参数表指定的属性", Desc = "修改参数表指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBParameterByField", Get = "", Post = "BParameter", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBParameterByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBParameterByField(BParameter entity, string fields);

        [ServiceContractDescription(Name = "删除参数表", Desc = "删除参数表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelBParameter?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBParameter?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBParameter(long id);

        [ServiceContractDescription(Name = "查询参数表", Desc = "查询参数表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBParameter", Get = "", Post = "BParameter", Return = "BaseResultList<BParameter>", ReturnType = "ListBParameter")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBParameter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBParameter(BParameter entity);

        [ServiceContractDescription(Name = "查询参数表(HQL)", Desc = "查询参数表(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBParameterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BParameter>", ReturnType = "ListBParameter")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBParameterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBParameterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询参数表", Desc = "通过主键ID查询参数表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBParameterById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BParameter>", ReturnType = "BParameter")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBParameterById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBParameterById(long id, string fields, bool isPlanish);
        #endregion

        #region 医生相片上传及下载处理
        [ServiceContractDescription(Name = "医生相片上传", Desc = "医生相片上传", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UploadBDoctorAccountImageByAccountID", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UploadBDoctorAccountImageByAccountID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message ST_UDTO_UploadBDoctorAccountImageByAccountID();

        [ServiceContractDescription(Name = "下载医生相片", Desc = "下载医生相片,图片类型(职业证书:ProfessionalAbility;个人照片:PersonImage)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DownLoadBDoctorAccountImageByAccountID?accountID={accountID}&operateType={operateType}&imageType={imageType}", Get = "accountID={accountID}&operateType={operateType}&imageType={imageType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_DownLoadBDoctorAccountImageByAccountID?accountID={accountID}&operateType={operateType}&imageType={imageType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_DownLoadBDoctorAccountImageByAccountID(long accountID, long operateType, string imageType);
        #endregion

        #region OSDoctorBonus

        [ServiceContractDescription(Name = "新增医生奖金结算记录", Desc = "新增医生奖金结算记录", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSDoctorBonus", Get = "", Post = "OSDoctorBonus", Return = "BaseResultDataValue", ReturnType = "OSDoctorBonus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSDoctorBonus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSDoctorBonus(OSDoctorBonus entity);

        [ServiceContractDescription(Name = "修改医生奖金结算记录", Desc = "修改医生奖金结算记录", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonus", Get = "", Post = "OSDoctorBonus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorBonus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorBonus(OSDoctorBonus entity);

        [ServiceContractDescription(Name = "修改医生奖金结算记录指定的属性", Desc = "修改医生奖金结算记录指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonusByField", Get = "", Post = "OSDoctorBonus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorBonusByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorBonusByField(OSDoctorBonus entity, string fields);

        //[ServiceContractDescription(Name = "删除医生奖金结算记录", Desc = "删除医生奖金结算记录", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSDoctorBonus?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSDoctorBonus?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSDoctorBonus(long id);

        [ServiceContractDescription(Name = "查询医生奖金结算记录", Desc = "查询医生奖金结算记录", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonus", Get = "", Post = "OSDoctorBonus", Return = "BaseResultList<OSDoctorBonus>", ReturnType = "ListOSDoctorBonus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorBonus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorBonus(OSDoctorBonus entity);

        [ServiceContractDescription(Name = "查询医生奖金结算记录(HQL)", Desc = "查询医生奖金结算记录(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSDoctorBonus>", ReturnType = "ListOSDoctorBonus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorBonusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorBonusByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医生奖金结算记录", Desc = "通过主键ID查询医生奖金结算记录", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSDoctorBonus>", ReturnType = "OSDoctorBonus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorBonusById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorBonusById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "咨询费打款明细报表Excel导出", Desc = "咨询费打款明细报表Excel导出", Url = "ZhiFangWeiXinService.svc/ST_UDTO_ExportExcelOSDoctorBonusDetail?operateType={operateType}&where={where}", Get = "operateType={operateType}&where={where}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ExportExcelOSDoctorBonusDetail?operateType={operateType}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_ExportExcelOSDoctorBonusDetail(long operateType, string where);
        #endregion

        #region OSDoctorBonusAttachment

        [ServiceContractDescription(Name = "新增医生结算单附件表", Desc = "新增医生结算单附件表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSDoctorBonusAttachment", Get = "", Post = "OSDoctorBonusAttachment", Return = "BaseResultDataValue", ReturnType = "OSDoctorBonusAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSDoctorBonusAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSDoctorBonusAttachment(OSDoctorBonusAttachment entity);

        [ServiceContractDescription(Name = "修改医生结算单附件表", Desc = "修改医生结算单附件表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonusAttachment", Get = "", Post = "OSDoctorBonusAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorBonusAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorBonusAttachment(OSDoctorBonusAttachment entity);

        [ServiceContractDescription(Name = "修改医生结算单附件表指定的属性", Desc = "修改医生结算单附件表指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonusAttachmentByField", Get = "", Post = "OSDoctorBonusAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorBonusAttachmentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorBonusAttachmentByField(OSDoctorBonusAttachment entity, string fields);

        //[ServiceContractDescription(Name = "删除医生结算单附件表", Desc = "删除医生结算单附件表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSDoctorBonusAttachment?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSDoctorBonusAttachment?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        // BaseResultBool ST_UDTO_DelOSDoctorBonusAttachment(long id);

        [ServiceContractDescription(Name = "查询医生结算单附件表", Desc = "查询医生结算单附件表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusAttachment", Get = "", Post = "OSDoctorBonusAttachment", Return = "BaseResultList<OSDoctorBonusAttachment>", ReturnType = "ListOSDoctorBonusAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorBonusAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorBonusAttachment(OSDoctorBonusAttachment entity);

        [ServiceContractDescription(Name = "查询医生结算单附件表(HQL)", Desc = "查询医生结算单附件表(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSDoctorBonusAttachment>", ReturnType = "ListOSDoctorBonusAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorBonusAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorBonusAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医生结算单附件表", Desc = "通过主键ID查询医生结算单附件表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSDoctorBonusAttachment>", ReturnType = "OSDoctorBonusAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorBonusAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorBonusAttachmentById(long id, string fields, bool isPlanish);
        #endregion

        #region 医生结算单附件表上传及下载处理

        [ServiceContractDescription(Name = "医生结算单附件表附件上传服务", Desc = "医生结算单附件表附件上传服务", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UploadOSDoctorBonusAttachment", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UploadOSDoctorBonusAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message ST_UDTO_UploadOSDoctorBonusAttachment();

        [ServiceContractDescription(Name = "下载医生结算单附件表附件文件", Desc = "下载医生结算单附件表附件文件", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DownLoadOSDoctorBonusAttachment?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_DownLoadOSDoctorBonusAttachment?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_DownLoadOSDoctorBonusAttachment(long id, long operateType);
        #endregion

        #region OSDoctorBonusOperation

        [ServiceContractDescription(Name = "新增医生结算单操作记录表", Desc = "新增医生结算单操作记录表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSDoctorBonusOperation", Get = "", Post = "OSDoctorBonusOperation", Return = "BaseResultDataValue", ReturnType = "OSDoctorBonusOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSDoctorBonusOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSDoctorBonusOperation(OSDoctorBonusOperation entity);

        [ServiceContractDescription(Name = "修改医生结算单操作记录表", Desc = "修改医生结算单操作记录表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonusOperation", Get = "", Post = "OSDoctorBonusOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorBonusOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorBonusOperation(OSDoctorBonusOperation entity);

        [ServiceContractDescription(Name = "修改医生结算单操作记录表指定的属性", Desc = "修改医生结算单操作记录表指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonusOperationByField", Get = "", Post = "OSDoctorBonusOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorBonusOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorBonusOperationByField(OSDoctorBonusOperation entity, string fields);

        //[ServiceContractDescription(Name = "删除医生结算单操作记录表", Desc = "删除医生结算单操作记录表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSDoctorBonusOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSDoctorBonusOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSDoctorBonusOperation(long id);

        [ServiceContractDescription(Name = "查询医生结算单操作记录表", Desc = "查询医生结算单操作记录表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusOperation", Get = "", Post = "OSDoctorBonusOperation", Return = "BaseResultList<OSDoctorBonusOperation>", ReturnType = "ListOSDoctorBonusOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorBonusOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorBonusOperation(OSDoctorBonusOperation entity);

        [ServiceContractDescription(Name = "查询医生结算单操作记录表(HQL)", Desc = "查询医生结算单操作记录表(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSDoctorBonusOperation>", ReturnType = "ListOSDoctorBonusOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorBonusOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorBonusOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医生结算单操作记录表", Desc = "通过主键ID查询医生结算单操作记录表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSDoctorBonusOperation>", ReturnType = "OSDoctorBonusOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorBonusOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorBonusOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region OSDoctorBonusForm

        [ServiceContractDescription(Name = "新增医生奖金结算单", Desc = "新增医生奖金结算单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSDoctorBonusForm", Get = "", Post = "OSDoctorBonusForm", Return = "BaseResultDataValue", ReturnType = "OSDoctorBonusForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSDoctorBonusForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSDoctorBonusForm(OSDoctorBonusForm entity);

        [ServiceContractDescription(Name = "新增医生奖金结算单及记录明细", Desc = "新增医生奖金结算单及记录明细", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSDoctorBonusFormAndDetails", Get = "", Post = "OSDoctorBonusApply", Return = "BaseResultDataValue", ReturnType = "OSDoctorBonusApply")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSDoctorBonusFormAndDetails", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSDoctorBonusFormAndDetails(OSDoctorBonusApply entity);

        [ServiceContractDescription(Name = "修改医生奖金结算单", Desc = "修改医生奖金结算单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonusForm", Get = "", Post = "OSDoctorBonusForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorBonusForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorBonusForm(OSDoctorBonusForm entity);

        [ServiceContractDescription(Name = "修改医生奖金结算单指定的属性", Desc = "修改医生奖金结算单指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonusFormByField", Get = "", Post = "OSDoctorBonusForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorBonusFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorBonusFormByField(OSDoctorBonusForm entity, string fields);

        [ServiceContractDescription(Name = "修改医生奖金结算单申请及记录明细", Desc = "修改医生奖金结算单申请及记录明细", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonusFormAndDetails", Get = "", Post = "OSDoctorBonusApply", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorBonusFormAndDetails", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorBonusFormAndDetails(OSDoctorBonusApply entity, string fields);

        [ServiceContractDescription(Name = "批量选择医生奖金记录的检查并打款操作处理", Desc = "批量选择医生奖金记录的检查并打款操作处理", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorBonusListPayStatus", Get = "", Post = "OSDoctorBonusApply", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorBonusListPayStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorBonusListPayStatus(OSDoctorBonusApply entity);

        //[ServiceContractDescription(Name = "删除医生奖金结算单", Desc = "删除医生奖金结算单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSDoctorBonusForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSDoctorBonusForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSDoctorBonusForm(long id);

        [ServiceContractDescription(Name = "物理删除医生奖金结算单并同时删除医生奖金结算记录,操作记录及附件信息", Desc = "物理删除医生奖金结算单并同时删除医生奖金结算记录,操作记录及附件信息", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSDoctorBonusFormAndDetails?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSDoctorBonusFormAndDetails?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        BaseResultBool ST_UDTO_DelOSDoctorBonusFormAndDetails(long id);

        [ServiceContractDescription(Name = "检查奖金记录里是否还有未打款", Desc = "检查奖金记录里是否还有未打款", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchCheckIsUpdatePayed?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_SearchCheckIsUpdatePayed?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        BaseResultBool ST_UDTO_SearchCheckIsUpdatePayed(long id);

        [ServiceContractDescription(Name = "查询医生奖金结算单", Desc = "查询医生奖金结算单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusForm", Get = "", Post = "OSDoctorBonusForm", Return = "BaseResultList<OSDoctorBonusForm>", ReturnType = "ListOSDoctorBonusForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorBonusForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorBonusForm(OSDoctorBonusForm entity);

        [ServiceContractDescription(Name = "查询医生奖金结算单(HQL)", Desc = "查询医生奖金结算单(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSDoctorBonusForm>", ReturnType = "ListOSDoctorBonusForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorBonusFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorBonusFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医生奖金结算单", Desc = "通过主键ID查询医生奖金结算单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorBonusFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSDoctorBonusForm>", ReturnType = "OSDoctorBonusForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorBonusFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorBonusFormById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过结算周期获取医生奖金结算单及结算记录明细的结算申请数据", Desc = "通过结算周期获取医生奖金结算单及结算记录明细的结算申请数据", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchSettlementApplyInfoByBonusFormRound?bonusFormRound={bonusFormRound}", Get = "bonusFormRound={bonusFormRound}", Post = "", Return = "BaseResultList<OSDoctorBonusForm>", ReturnType = "OSDoctorBonusForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSettlementApplyInfoByBonusFormRound?bonusFormRound={bonusFormRound}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSettlementApplyInfoByBonusFormRound(string bonusFormRound);
        #endregion

        #region 医生奖金结算单PDF预览及Excel导出
        [ServiceContractDescription(Name = "预览医生奖金结算单PDF", Desc = "预览医生奖金结算单PDF", Url = "ZhiFangWeiXinService.svc/ST_UDTO_OSDoctorBonusFormPreviewPdf?id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}", Get = "id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/ST_UDTO_OSDoctorBonusFormPreviewPdf?id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}")]
        [OperationContract]
        Stream ST_UDTO_OSDoctorBonusFormPreviewPdf(long id, int operateType, bool isPreview, string templetName);

        [ServiceContractDescription(Name = "医生奖金结算单Excel导出", Desc = "医生奖金结算单Excel导出", Url = "ZhiFangWeiXinService.svc/ST_UDTO_ExportExcelOSDoctorBonusFormDetail?operateType={operateType}&where={where}", Get = "operateType={operateType}&where={where}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ExportExcelOSDoctorBonusFormDetail?operateType={operateType}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_ExportExcelOSDoctorBonusFormDetail(long operateType, string where);
        #endregion

        #region OSDoctorOrderForm

        [ServiceContractDescription(Name = "新增医生医嘱单", Desc = "新增医生医嘱单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSDoctorOrderForm", Get = "", Post = "OSDoctorOrderForm", Return = "BaseResultDataValue", ReturnType = "OSDoctorOrderForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSDoctorOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSDoctorOrderForm(OSDoctorOrderForm entity);

        [ServiceContractDescription(Name = "修改医生医嘱单", Desc = "修改医生医嘱单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorOrderForm", Get = "", Post = "OSDoctorOrderForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorOrderForm(OSDoctorOrderForm entity);

        [ServiceContractDescription(Name = "修改医生医嘱单指定的属性", Desc = "修改医生医嘱单指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorOrderFormByField", Get = "", Post = "OSDoctorOrderForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorOrderFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorOrderFormByField(OSDoctorOrderForm entity, string fields);

        //[ServiceContractDescription(Name = "删除医生医嘱单", Desc = "删除医生医嘱单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSDoctorOrderForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSDoctorOrderForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSDoctorOrderForm(long id);

        [ServiceContractDescription(Name = "查询医生医嘱单", Desc = "查询医生医嘱单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorOrderForm", Get = "", Post = "OSDoctorOrderForm", Return = "BaseResultList<OSDoctorOrderForm>", ReturnType = "ListOSDoctorOrderForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorOrderForm(OSDoctorOrderForm entity);

        [ServiceContractDescription(Name = "查询医生医嘱单(HQL)", Desc = "查询医生医嘱单(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorOrderFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSDoctorOrderForm>", ReturnType = "ListOSDoctorOrderForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorOrderFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorOrderFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医生医嘱单", Desc = "通过主键ID查询医生医嘱单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorOrderFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSDoctorOrderForm>", ReturnType = "OSDoctorOrderForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorOrderFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorOrderFormById(long id, string fields, bool isPlanish);
        #endregion

        #region OSDoctorOrderItem

        [ServiceContractDescription(Name = "新增医生医嘱项目", Desc = "新增医生医嘱项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSDoctorOrderItem", Get = "", Post = "OSDoctorOrderItem", Return = "BaseResultDataValue", ReturnType = "OSDoctorOrderItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSDoctorOrderItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSDoctorOrderItem(OSDoctorOrderItem entity);

        [ServiceContractDescription(Name = "修改医生医嘱项目", Desc = "修改医生医嘱项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorOrderItem", Get = "", Post = "OSDoctorOrderItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorOrderItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorOrderItem(OSDoctorOrderItem entity);

        [ServiceContractDescription(Name = "修改医生医嘱项目指定的属性", Desc = "修改医生医嘱项目指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSDoctorOrderItemByField", Get = "", Post = "OSDoctorOrderItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSDoctorOrderItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSDoctorOrderItemByField(OSDoctorOrderItem entity, string fields);

        //[ServiceContractDescription(Name = "删除医生医嘱项目", Desc = "删除医生医嘱项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSDoctorOrderItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSDoctorOrderItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSDoctorOrderItem(long id);

        [ServiceContractDescription(Name = "查询医生医嘱项目", Desc = "查询医生医嘱项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorOrderItem", Get = "", Post = "OSDoctorOrderItem", Return = "BaseResultList<OSDoctorOrderItem>", ReturnType = "ListOSDoctorOrderItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorOrderItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorOrderItem(OSDoctorOrderItem entity);

        [ServiceContractDescription(Name = "查询医生医嘱项目(HQL)", Desc = "查询医生医嘱项目(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorOrderItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSDoctorOrderItem>", ReturnType = "ListOSDoctorOrderItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorOrderItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorOrderItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医生医嘱项目", Desc = "通过主键ID查询医生医嘱项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSDoctorOrderItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSDoctorOrderItem>", ReturnType = "OSDoctorOrderItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSDoctorOrderItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSDoctorOrderItemById(long id, string fields, bool isPlanish);
        #endregion

        #region OSItemProductClassTree

        [ServiceContractDescription(Name = "新增检测项目产品分类树", Desc = "新增检测项目产品分类树", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSItemProductClassTree", Get = "", Post = "OSItemProductClassTree", Return = "BaseResultDataValue", ReturnType = "OSItemProductClassTree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSItemProductClassTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSItemProductClassTree(OSItemProductClassTree entity);

        [ServiceContractDescription(Name = "修改检测项目产品分类树", Desc = "修改检测项目产品分类树", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSItemProductClassTree", Get = "", Post = "OSItemProductClassTree", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSItemProductClassTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSItemProductClassTree(OSItemProductClassTree entity);

        [ServiceContractDescription(Name = "修改检测项目产品分类树指定的属性", Desc = "修改检测项目产品分类树指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSItemProductClassTreeByField", Get = "", Post = "OSItemProductClassTree", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSItemProductClassTreeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSItemProductClassTreeByField(OSItemProductClassTree entity, string fields);

        //[ServiceContractDescription(Name = "删除检测项目产品分类树", Desc = "删除检测项目产品分类树", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSItemProductClassTree?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSItemProductClassTree?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSItemProductClassTree(long id);

        [ServiceContractDescription(Name = "查询检测项目产品分类树", Desc = "查询检测项目产品分类树", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSItemProductClassTree", Get = "", Post = "OSItemProductClassTree", Return = "BaseResultList<OSItemProductClassTree>", ReturnType = "ListOSItemProductClassTree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTree(OSItemProductClassTree entity);

        [ServiceContractDescription(Name = "查询检测项目产品分类树(HQL)", Desc = "查询检测项目产品分类树(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSItemProductClassTreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSItemProductClassTree>", ReturnType = "ListOSItemProductClassTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询检测项目产品分类树", Desc = "通过主键ID查询检测项目产品分类树", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSItemProductClassTreeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSItemProductClassTree>", ReturnType = "OSItemProductClassTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTreeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeById(long id, string fields, bool isPlanish);
        #endregion

        #region OSItemProductClassTreeLink

        [ServiceContractDescription(Name = "新增检测项目产品分类树关系", Desc = "新增检测项目产品分类树关系", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSItemProductClassTreeLink", Get = "", Post = "OSItemProductClassTreeLink", Return = "BaseResultDataValue", ReturnType = "OSItemProductClassTreeLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSItemProductClassTreeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSItemProductClassTreeLink(OSItemProductClassTreeLink entity);

        [ServiceContractDescription(Name = "修改检测项目产品分类树关系", Desc = "修改检测项目产品分类树关系", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSItemProductClassTreeLink", Get = "", Post = "OSItemProductClassTreeLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSItemProductClassTreeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSItemProductClassTreeLink(OSItemProductClassTreeLink entity);

        [ServiceContractDescription(Name = "修改检测项目产品分类树关系指定的属性", Desc = "修改检测项目产品分类树关系指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSItemProductClassTreeLinkByField", Get = "", Post = "OSItemProductClassTreeLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSItemProductClassTreeLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSItemProductClassTreeLinkByField(OSItemProductClassTreeLink entity, string fields);

        //[ServiceContractDescription(Name = "删除检测项目产品分类树关系", Desc = "删除检测项目产品分类树关系", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSItemProductClassTreeLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSItemProductClassTreeLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSItemProductClassTreeLink(long id);

        [ServiceContractDescription(Name = "查询检测项目产品分类树关系", Desc = "查询检测项目产品分类树关系", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSItemProductClassTreeLink", Get = "", Post = "OSItemProductClassTreeLink", Return = "BaseResultList<OSItemProductClassTreeLink>", ReturnType = "ListOSItemProductClassTreeLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTreeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeLink(OSItemProductClassTreeLink entity);

        [ServiceContractDescription(Name = "查询检测项目产品分类树关系(HQL)", Desc = "查询检测项目产品分类树关系(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSItemProductClassTreeLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSItemProductClassTreeLink>", ReturnType = "ListOSItemProductClassTreeLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTreeLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询检测项目产品分类树关系", Desc = "通过主键ID查询检测项目产品分类树关系", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSItemProductClassTreeLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSItemProductClassTreeLink>", ReturnType = "OSItemProductClassTreeLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTreeLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region OSManagerRefundForm
        #region 注释新增、修改、删除，基本退款单服务
        //[ServiceContractDescription(Name = "新增退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。", Desc = "新增退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSManagerRefundForm", Get = "", Post = "OSManagerRefundForm", Return = "BaseResultDataValue", ReturnType = "OSManagerRefundForm")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSManagerRefundForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_AddOSManagerRefundForm(OSManagerRefundForm entity);

        //[ServiceContractDescription(Name = "修改退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。", Desc = "修改退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSManagerRefundForm", Get = "", Post = "OSManagerRefundForm", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSManagerRefundForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateOSManagerRefundForm(OSManagerRefundForm entity);

        //[ServiceContractDescription(Name = "修改退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。指定的属性", Desc = "修改退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSManagerRefundFormByField", Get = "", Post = "OSManagerRefundForm", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSManagerRefundFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateOSManagerRefundFormByField(OSManagerRefundForm entity, string fields);

        //[ServiceContractDescription(Name = "删除退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。", Desc = "删除退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSManagerRefundForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSManagerRefundForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSManagerRefundForm(long id);
        #endregion
        [ServiceContractDescription(Name = "退费申请单一审", Desc = "退费申请单一审。", Url = "ZhiFangWeiXinService.svc/ST_UDTO_OSManagerRefundFormOneReview", Get = "", Post = "RefundFormVO", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_OSManagerRefundFormOneReview", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_OSManagerRefundFormOneReview(RefundFormVO entity);

        [ServiceContractDescription(Name = "退费申请单二审", Desc = "退费申请单二审。", Url = "ZhiFangWeiXinService.svc/ST_UDTO_OSManagerRefundFormTwoReview", Get = "", Post = "RefundFormVO", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_OSManagerRefundFormTwoReview", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_OSManagerRefundFormTwoReview(RefundFormVO entity);

        [ServiceContractDescription(Name = "退费申请单发放", Desc = "退费申请单发放。", Url = "ZhiFangWeiXinService.svc/ST_UDTO_OSManagerRefundFormThreeReview", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_OSManagerRefundFormThreeReview", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_OSManagerRefundFormThreeReview(RefundFormThreeReviewVO entity);

        [ServiceContractDescription(Name = "查询退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。", Desc = "查询退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSManagerRefundForm", Get = "", Post = "OSManagerRefundForm", Return = "BaseResultList<OSManagerRefundForm>", ReturnType = "ListOSManagerRefundForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSManagerRefundForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSManagerRefundForm(OSManagerRefundForm entity);

        [ServiceContractDescription(Name = "查询退费申请单NoPlanish(HQL)", Desc = "查询退费申请单NoPlanish(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSManagerRefundFormNoPlanishByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}", Post = "", Return = "BaseResultList<OSUserOrderForm>", ReturnType = "ListOSUserOrderForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSManagerRefundFormNoPlanishByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormNoPlanishByHQL(int page, int limit, string fields, string where, string sort);

        [ServiceContractDescription(Name = "查询退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。(HQL)", Desc = "查询退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSManagerRefundFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSManagerRefundForm>", ReturnType = "ListOSManagerRefundForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSManagerRefundFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。", Desc = "通过主键ID查询退费申请单：退费申请由商家内部特定角色员工发起，退费价格是同客户协商确定。", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSManagerRefundFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSManagerRefundForm>", ReturnType = "OSManagerRefundForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSManagerRefundFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormById(long id, string fields, bool isPlanish);
        #endregion

        #region 退款单附件上传及下载处理

        [ServiceContractDescription(Name = "退款单附件上传服务", Desc = "退款单附件上传服务", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UploadOSManagerRefundFormAttachment", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UploadOSManagerRefundFormAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message ST_UDTO_UploadOSManagerRefundFormAttachment();

        [ServiceContractDescription(Name = "下载退款单附件服务", Desc = "下载退款单附件服务", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DownLoadOSManagerRefundFormAttachment?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_DownLoadOSManagerRefundFormAttachment?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_DownLoadOSManagerRefundFormAttachment(long id, long operateType);
        #endregion

        #region 退款申请单PDF预览及Excel导出
        [ServiceContractDescription(Name = "预览退款申请单PDF文件", Desc = "预览退款申请单PDF文件", Url = "ZhiFangWeiXinService.svc/ST_UDTO_OSManagerRefundFormPreviewPdf?id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}", Get = "id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/ST_UDTO_OSManagerRefundFormPreviewPdf?id={id}&operateType={operateType}&isPreview={isPreview}&templetName={templetName}")]
        [OperationContract]
        Stream ST_UDTO_OSManagerRefundFormPreviewPdf(long id, int operateType, bool isPreview, string templetName);

        [ServiceContractDescription(Name = "退款申请清单Excel导出", Desc = "退款申请清单Excel导出", Url = "ZhiFangWeiXinService.svc/ST_UDTO_ExportExcelOSManagerRefundFormDetail?operateType={operateType}&where={where}", Get = "operateType={operateType}&where={where}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ExportExcelOSManagerRefundFormDetail?operateType={operateType}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_ExportExcelOSManagerRefundFormDetail(long operateType, string where);
        #endregion

        #region OSManagerRefundFormAttachment

        [ServiceContractDescription(Name = "新增退款单单附件表", Desc = "新增退款单单附件表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSManagerRefundFormAttachment", Get = "", Post = "OSManagerRefundFormAttachment", Return = "BaseResultDataValue", ReturnType = "OSManagerRefundFormAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSManagerRefundFormAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSManagerRefundFormAttachment(OSManagerRefundFormAttachment entity);

        [ServiceContractDescription(Name = "修改退款单单附件表", Desc = "修改退款单单附件表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSManagerRefundFormAttachment", Get = "", Post = "OSManagerRefundFormAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSManagerRefundFormAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSManagerRefundFormAttachment(OSManagerRefundFormAttachment entity);

        [ServiceContractDescription(Name = "修改退款单单附件表指定的属性", Desc = "修改退款单单附件表指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSManagerRefundFormAttachmentByField", Get = "", Post = "OSManagerRefundFormAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSManagerRefundFormAttachmentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSManagerRefundFormAttachmentByField(OSManagerRefundFormAttachment entity, string fields);

        //[ServiceContractDescription(Name = "删除退款单单附件表", Desc = "删除退款单单附件表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSManagerRefundFormAttachment?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSManagerRefundFormAttachment?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSManagerRefundFormAttachment(long id);

        [ServiceContractDescription(Name = "查询退款单单附件表", Desc = "查询退款单单附件表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSManagerRefundFormAttachment", Get = "", Post = "OSManagerRefundFormAttachment", Return = "BaseResultList<OSManagerRefundFormAttachment>", ReturnType = "ListOSManagerRefundFormAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSManagerRefundFormAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormAttachment(OSManagerRefundFormAttachment entity);

        [ServiceContractDescription(Name = "查询退款单单附件表(HQL)", Desc = "查询退款单单附件表(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSManagerRefundFormAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSManagerRefundFormAttachment>", ReturnType = "ListOSManagerRefundFormAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSManagerRefundFormAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询退款单单附件表", Desc = "通过主键ID查询退款单单附件表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSManagerRefundFormAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSManagerRefundFormAttachment>", ReturnType = "OSManagerRefundFormAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSManagerRefundFormAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormAttachmentById(long id, string fields, bool isPlanish);
        #endregion

        #region OSManagerRefundFormOperation

        [ServiceContractDescription(Name = "新增退款单操作记录表", Desc = "新增退款单操作记录表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSManagerRefundFormOperation", Get = "", Post = "OSManagerRefundFormOperation", Return = "BaseResultDataValue", ReturnType = "OSManagerRefundFormOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSManagerRefundFormOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSManagerRefundFormOperation(OSManagerRefundFormOperation entity);

        [ServiceContractDescription(Name = "修改退款单操作记录表", Desc = "修改退款单操作记录表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSManagerRefundFormOperation", Get = "", Post = "OSManagerRefundFormOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSManagerRefundFormOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSManagerRefundFormOperation(OSManagerRefundFormOperation entity);

        [ServiceContractDescription(Name = "修改退款单操作记录表指定的属性", Desc = "修改退款单操作记录表指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSManagerRefundFormOperationByField", Get = "", Post = "OSManagerRefundFormOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSManagerRefundFormOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSManagerRefundFormOperationByField(OSManagerRefundFormOperation entity, string fields);

        //[ServiceContractDescription(Name = "删除退款单操作记录表", Desc = "删除退款单操作记录表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSManagerRefundFormOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSManagerRefundFormOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSManagerRefundFormOperation(long id);

        [ServiceContractDescription(Name = "查询退款单操作记录表", Desc = "查询退款单操作记录表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSManagerRefundFormOperation", Get = "", Post = "OSManagerRefundFormOperation", Return = "BaseResultList<OSManagerRefundFormOperation>", ReturnType = "ListOSManagerRefundFormOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSManagerRefundFormOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormOperation(OSManagerRefundFormOperation entity);

        [ServiceContractDescription(Name = "查询退款单操作记录表(HQL)", Desc = "查询退款单操作记录表(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSManagerRefundFormOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSManagerRefundFormOperation>", ReturnType = "ListOSManagerRefundFormOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSManagerRefundFormOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询退款单操作记录表", Desc = "通过主键ID查询退款单操作记录表", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSManagerRefundFormOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSManagerRefundFormOperation>", ReturnType = "OSManagerRefundFormOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSManagerRefundFormOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSManagerRefundFormOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region OSRecommendationItemProduct

        [ServiceContractDescription(Name = "新增特推项目产品", Desc = "新增特推项目产品", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSRecommendationItemProduct", Get = "", Post = "OSRecommendationItemProduct", Return = "BaseResultDataValue", ReturnType = "OSRecommendationItemProduct")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSRecommendationItemProduct", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSRecommendationItemProduct(OSRecommendationItemProduct entity);

        [ServiceContractDescription(Name = "修改特推项目产品", Desc = "修改特推项目产品", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSRecommendationItemProduct", Get = "", Post = "OSRecommendationItemProduct", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSRecommendationItemProduct", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSRecommendationItemProduct(OSRecommendationItemProduct entity);

        [ServiceContractDescription(Name = "修改特推项目产品指定的属性", Desc = "修改特推项目产品指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSRecommendationItemProductByField", Get = "", Post = "OSRecommendationItemProduct", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSRecommendationItemProductByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSRecommendationItemProductByField(OSRecommendationItemProduct entity, string fields);

        //[ServiceContractDescription(Name = "删除特推项目产品", Desc = "删除特推项目产品", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSRecommendationItemProduct?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSRecommendationItemProduct?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSRecommendationItemProduct(long id);

        [ServiceContractDescription(Name = "查询特推项目产品", Desc = "查询特推项目产品", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSRecommendationItemProduct", Get = "", Post = "OSRecommendationItemProduct", Return = "BaseResultList<OSRecommendationItemProduct>", ReturnType = "ListOSRecommendationItemProduct")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSRecommendationItemProduct", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSRecommendationItemProduct(OSRecommendationItemProduct entity);

        [ServiceContractDescription(Name = "特推项目维护时的查询定制,可只查询有效特推项目", Desc = "特推项目维护时的查询定制,可只查询有效特推项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSRecommendationItemProductOrEffectiveByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&effective={effective}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&effective={effective}", Post = "", Return = "BaseResultList<OSRecommendationItemProduct>", ReturnType = "ListOSRecommendationItemProduct")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSRecommendationItemProductOrEffectiveByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&effective={effective}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSRecommendationItemProductOrEffectiveByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool effective);

        [ServiceContractDescription(Name = "查询特推项目产品(HQL)", Desc = "查询特推项目产品(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSRecommendationItemProductByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSRecommendationItemProduct>", ReturnType = "ListOSRecommendationItemProduct")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSRecommendationItemProductByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSRecommendationItemProductByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询特推项目产品", Desc = "通过主键ID查询特推项目产品", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSRecommendationItemProductById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSRecommendationItemProduct>", ReturnType = "OSRecommendationItemProduct")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSRecommendationItemProductById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSRecommendationItemProductById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "特推项目产品图片上传", Desc = "特推项目产品图片上传", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UploadRecommendationItemProductImageByID", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UploadRecommendationItemProductImageByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message ST_UDTO_UploadRecommendationItemProductImageByID();

        [ServiceContractDescription(Name = "下载特推项目产品图片", Desc = "下载特推项目产品图片,图片类型(职业证书:ProfessionalAbility;个人照片:PersonImage)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DownLoadOSRecommendationItemProductByID?recommendationitemproductID={recommendationitemproductID}&operateType={operateType}&imageType={imageType}", Get = "recommendationitemproductID={recommendationitemproductID}&operateType={operateType}&imageType={imageType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_DownLoadOSRecommendationItemProductByID?recommendationitemproductID={recommendationitemproductID}&operateType={operateType}&imageType={imageType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_DownLoadOSRecommendationItemProductByID(long recommendationitemproductID, long operateType, string imageType);
        #endregion

        #region OSShoppingCart

        [ServiceContractDescription(Name = "新增购物车", Desc = "新增购物车", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSShoppingCart", Get = "", Post = "OSShoppingCart", Return = "BaseResultDataValue", ReturnType = "OSShoppingCart")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSShoppingCart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSShoppingCart(OSShoppingCart entity);

        [ServiceContractDescription(Name = "修改购物车", Desc = "修改购物车", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSShoppingCart", Get = "", Post = "OSShoppingCart", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSShoppingCart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSShoppingCart(OSShoppingCart entity);

        [ServiceContractDescription(Name = "修改购物车指定的属性", Desc = "修改购物车指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSShoppingCartByField", Get = "", Post = "OSShoppingCart", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSShoppingCartByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSShoppingCartByField(OSShoppingCart entity, string fields);

        //[ServiceContractDescription(Name = "删除购物车", Desc = "删除购物车", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSShoppingCart?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSShoppingCart?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSShoppingCart(long id);

        [ServiceContractDescription(Name = "查询购物车", Desc = "查询购物车", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSShoppingCart", Get = "", Post = "OSShoppingCart", Return = "BaseResultList<OSShoppingCart>", ReturnType = "ListOSShoppingCart")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSShoppingCart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSShoppingCart(OSShoppingCart entity);

        [ServiceContractDescription(Name = "查询购物车(HQL)", Desc = "查询购物车(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSShoppingCartByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSShoppingCart>", ReturnType = "ListOSShoppingCart")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSShoppingCartByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSShoppingCartByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询购物车", Desc = "通过主键ID查询购物车", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSShoppingCartById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSShoppingCart>", ReturnType = "OSShoppingCart")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSShoppingCartById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSShoppingCartById(long id, string fields, bool isPlanish);
        #endregion

        #region OSUserConsumerForm

        [ServiceContractDescription(Name = "新增用户消费单", Desc = "新增用户消费单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSUserConsumerForm", Get = "", Post = "OSUserConsumerForm", Return = "BaseResultDataValue", ReturnType = "OSUserConsumerForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSUserConsumerForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSUserConsumerForm(OSUserConsumerForm entity);

        [ServiceContractDescription(Name = "修改用户消费单", Desc = "修改用户消费单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSUserConsumerForm", Get = "", Post = "OSUserConsumerForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSUserConsumerForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSUserConsumerForm(OSUserConsumerForm entity);

        [ServiceContractDescription(Name = "修改用户消费单指定的属性", Desc = "修改用户消费单指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSUserConsumerFormByField", Get = "", Post = "OSUserConsumerForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSUserConsumerFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSUserConsumerFormByField(OSUserConsumerForm entity, string fields);

        //[ServiceContractDescription(Name = "删除用户消费单", Desc = "删除用户消费单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSUserConsumerForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSUserConsumerForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSUserConsumerForm(long id);

        [ServiceContractDescription(Name = "查询用户消费单", Desc = "查询用户消费单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserConsumerForm", Get = "", Post = "OSUserConsumerForm", Return = "BaseResultList<OSUserConsumerForm>", ReturnType = "ListOSUserConsumerForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSUserConsumerForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSUserConsumerForm(OSUserConsumerForm entity);

        [ServiceContractDescription(Name = "查询用户消费单(HQL)", Desc = "查询用户消费单(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserConsumerFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSUserConsumerForm>", ReturnType = "ListOSUserConsumerForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSUserConsumerFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSUserConsumerFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询用户消费单", Desc = "通过主键ID查询用户消费单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserConsumerFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSUserConsumerForm>", ReturnType = "OSUserConsumerForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSUserConsumerFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSUserConsumerFormById(long id, string fields, bool isPlanish);
        #endregion

        #region OSUserConsumerItem

        [ServiceContractDescription(Name = "新增用户消费项目", Desc = "新增用户消费项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSUserConsumerItem", Get = "", Post = "OSUserConsumerItem", Return = "BaseResultDataValue", ReturnType = "OSUserConsumerItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSUserConsumerItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSUserConsumerItem(OSUserConsumerItem entity);

        [ServiceContractDescription(Name = "修改用户消费项目", Desc = "修改用户消费项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSUserConsumerItem", Get = "", Post = "OSUserConsumerItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSUserConsumerItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSUserConsumerItem(OSUserConsumerItem entity);

        [ServiceContractDescription(Name = "修改用户消费项目指定的属性", Desc = "修改用户消费项目指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSUserConsumerItemByField", Get = "", Post = "OSUserConsumerItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSUserConsumerItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSUserConsumerItemByField(OSUserConsumerItem entity, string fields);

        //[ServiceContractDescription(Name = "删除用户消费项目", Desc = "删除用户消费项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSUserConsumerItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSUserConsumerItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSUserConsumerItem(long id);

        [ServiceContractDescription(Name = "查询用户消费项目", Desc = "查询用户消费项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserConsumerItem", Get = "", Post = "OSUserConsumerItem", Return = "BaseResultList<OSUserConsumerItem>", ReturnType = "ListOSUserConsumerItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSUserConsumerItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSUserConsumerItem(OSUserConsumerItem entity);

        [ServiceContractDescription(Name = "查询用户消费项目(HQL)", Desc = "查询用户消费项目(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserConsumerItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSUserConsumerItem>", ReturnType = "ListOSUserConsumerItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSUserConsumerItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSUserConsumerItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询用户消费项目", Desc = "通过主键ID查询用户消费项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserConsumerItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSUserConsumerItem>", ReturnType = "OSUserConsumerItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSUserConsumerItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSUserConsumerItemById(long id, string fields, bool isPlanish);
        #endregion

        #region OSUserOrderForm

        //[ServiceContractDescription(Name = "新增用户订单", Desc = "新增用户订单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSUserOrderForm", Get = "", Post = "OSUserOrderForm", Return = "BaseResultDataValue", ReturnType = "OSUserOrderForm")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSUserOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_AddOSUserOrderForm(OSUserOrderForm entity);

        [ServiceContractDescription(Name = "管理订单退费申请", Desc = "管理订单退费申请", Url = "ZhiFangWeiXinService.svc/ST_UDTO_OSUserOrderFormRefundE?OrderFormID={OrderFormID}&MessageStr={MessageStr}&RefundReason={RefundReason}&RefundPrice={RefundPrice}", Get = "OrderFormID={OrderFormID}&MessageStr={MessageStr}&RefundReason={RefundReason}&RefundPrice={RefundPrice}", Post = "", Return = "BaseResultDataValue", ReturnType = "OSUserOrderForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_OSUserOrderFormRefundE?OrderFormID={OrderFormID}&MessageStr={MessageStr}&RefundReason={RefundReason}&RefundPrice={RefundPrice}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_OSUserOrderFormRefundE(long OrderFormID, string MessageStr, string RefundReason, double RefundPrice);

        [ServiceContractDescription(Name = "更新用户订单的状态为取消订单", Desc = "更新用户订单的状态为取消订单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSUserOrderFormStatusOfCancelOrder?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "OSUserOrderForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSUserOrderFormStatusOfCancelOrder?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSUserOrderFormStatusOfCancelOrder(long id);

        //[ServiceContractDescription(Name = "修改用户订单", Desc = "修改用户订单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSUserOrderForm", Get = "", Post = "OSUserOrderForm", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSUserOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateOSUserOrderForm(OSUserOrderForm entity);

        //[ServiceContractDescription(Name = "修改用户订单指定的属性", Desc = "修改用户订单指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSUserOrderFormByField", Get = "", Post = "OSUserOrderForm", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSUserOrderFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateOSUserOrderFormByField(OSUserOrderForm entity, string fields);

        //[ServiceContractDescription(Name = "删除用户订单", Desc = "删除用户订单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSUserOrderForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSUserOrderForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSUserOrderForm(long id);

        [ServiceContractDescription(Name = "查询用户订单", Desc = "查询用户订单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderForm", Get = "", Post = "OSUserOrderForm", Return = "BaseResultList<OSUserOrderForm>", ReturnType = "ListOSUserOrderForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSUserOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSUserOrderForm(OSUserOrderForm entity);

        [ServiceContractDescription(Name = "查询用户订单NoPlanish(HQL)", Desc = "查询用户订单NoPlanish(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderFormNoPlanishByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}", Post = "", Return = "BaseResultList<OSUserOrderForm>", ReturnType = "ListOSUserOrderForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSUserOrderFormNoPlanishByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSUserOrderFormNoPlanishByHQL(int page, int limit, string fields, string where, string sort);

        [ServiceContractDescription(Name = "查询用户订单(HQL)", Desc = "查询用户订单(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSUserOrderForm>", ReturnType = "ListOSUserOrderForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSUserOrderFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSUserOrderFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询用户订单", Desc = "通过主键ID查询用户订单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSUserOrderForm>", ReturnType = "OSUserOrderForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSUserOrderFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSUserOrderFormById(long id, string fields, bool isPlanish);
        #endregion

        [ServiceContractDescription(Name = "通过主键ID解锁用户订单", Desc = "通过主键ID解锁用户订单", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UnLockOSUserOrderFormById", Get = "", Post = "", Return = "BaseResultList<OSUserOrderForm>", ReturnType = "OSUserOrderForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UnLockOSUserOrderFormById", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_UnLockOSUserOrderFormById(long Id);

        #region OSUserOrderItem

        [ServiceContractDescription(Name = "新增用户订单项目", Desc = "新增用户订单项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddOSUserOrderItem", Get = "", Post = "OSUserOrderItem", Return = "BaseResultDataValue", ReturnType = "OSUserOrderItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSUserOrderItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSUserOrderItem(OSUserOrderItem entity);

        [ServiceContractDescription(Name = "修改用户订单项目", Desc = "修改用户订单项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSUserOrderItem", Get = "", Post = "OSUserOrderItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSUserOrderItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSUserOrderItem(OSUserOrderItem entity);

        [ServiceContractDescription(Name = "修改用户订单项目指定的属性", Desc = "修改用户订单项目指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateOSUserOrderItemByField", Get = "", Post = "OSUserOrderItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSUserOrderItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSUserOrderItemByField(OSUserOrderItem entity, string fields);

        //[ServiceContractDescription(Name = "删除用户订单项目", Desc = "删除用户订单项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelOSUserOrderItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSUserOrderItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelOSUserOrderItem(long id);

        [ServiceContractDescription(Name = "查询用户订单项目", Desc = "查询用户订单项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderItem", Get = "", Post = "OSUserOrderItem", Return = "BaseResultList<OSUserOrderItem>", ReturnType = "ListOSUserOrderItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSUserOrderItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSUserOrderItem(OSUserOrderItem entity);

        [ServiceContractDescription(Name = "查询用户订单项目(HQL)", Desc = "查询用户订单项目(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSUserOrderItem>", ReturnType = "ListOSUserOrderItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSUserOrderItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSUserOrderItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询用户订单项目", Desc = "通过主键ID查询用户订单项目", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSUserOrderItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSUserOrderItem>", ReturnType = "OSUserOrderItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSUserOrderItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSUserOrderItemById(long id, string fields, bool isPlanish);
        #endregion


        #region 收入明细报表/项目统计报表及Excel/PDF导出

        [ServiceContractDescription(Name = "查询财务收入报表数据", Desc = "查询财务收入报表数据", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchFinanceIncomeList?page={page}&limit={limit}&fields={fields}&searchEntity={searchEntity}&sort={sort}", Get = "page={page}&limit={limit}&fields={fields}&searchEntity={searchEntity}&sort={sort}", Post = "", Return = "BaseResultList<FinanceIncome>", ReturnType = "ListFinanceIncome")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchFinanceIncomeList?page={page}&limit={limit}&fields={fields}&searchEntity={searchEntity}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchFinanceIncomeList(int page, int limit, string fields, string searchEntity, string sort);

        [ServiceContractDescription(Name = "财务收入报表PDF文件", Desc = "财务收入报表PDF文件", Url = "ZhiFangWeiXinService.svc/ST_UDTO_GetFinanceIncomeExcelToPdfFile?searchEntity={searchEntity}&operateType={operateType}", Get = "searchEntity={searchEntity}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/ST_UDTO_GetFinanceIncomeExcelToPdfFile?searchEntity={searchEntity}&operateType={operateType}")]
        [OperationContract]
        Stream ST_UDTO_GetFinanceIncomeExcelToPdfFile(string searchEntity, int operateType);

        [ServiceContractDescription(Name = "财务收入报表Excel导出", Desc = "财务收入报表Excel导出", Url = "ZhiFangWeiXinService.svc/ST_UDTO_ExportExcelFinanceIncome?operateType={operateType}&searchEntity={searchEntity}", Get = "operateType={operateType}&searchEntity={searchEntity}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ExportExcelFinanceIncome?operateType={operateType}&searchEntity={searchEntity}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_ExportExcelFinanceIncome(long operateType, string searchEntity);

        [ServiceContractDescription(Name = "查询项目统计报表数据", Desc = "查询项目统计报表数据", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchUserConsumerItemDetails?page={page}&limit={limit}&fields={fields}&searchEntity={searchEntity}&sort={sort}", Get = "page={page}&limit={limit}&fields={fields}&searchEntity={searchEntity}&sort={sort}", Post = "", Return = "BaseResultList<UserConsumerItemDetails>", ReturnType = "ListUserConsumerItemDetails")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchUserConsumerItemDetails?page={page}&limit={limit}&fields={fields}&searchEntity={searchEntity}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchUserConsumerItemDetails(int page, int limit, string fields, string searchEntity, string sort);

        [ServiceContractDescription(Name = "获取项目统计报表Excel转PDF的文件", Desc = "获取项目统计报表Excel转PDF的文件", Url = "ZhiFangWeiXinService.svc/ST_UDTO_GetUserConsumerItemExcelToPdfFile?searchEntity={searchEntity}&operateType={operateType}", Get = "searchEntity={searchEntity}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/ST_UDTO_GetUserConsumerItemExcelToPdfFile?searchEntity={searchEntity}&operateType={operateType}")]
        [OperationContract]
        Stream ST_UDTO_GetUserConsumerItemExcelToPdfFile(string searchEntity, int operateType);

        [ServiceContractDescription(Name = "获取项目统计报表Excel导出文件", Desc = "获取项目统计报表Excel导出文件", Url = "ZhiFangWeiXinService.svc/ST_UDTO_ExportExcelUserConsumerItemDetails?operateType={operateType}&searchEntity={searchEntity}", Get = "operateType={operateType}&searchEntity={searchEntity}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ExportExcelUserConsumerItemDetails?operateType={operateType}&searchEntity={searchEntity}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_ExportExcelUserConsumerItemDetails(long operateType, string searchEntity);
        #endregion

        #region BLabTestItem

        [ServiceContractDescription(Name = "新增区域项目字典", Desc = "新增区域项目字典", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddBLabTestItem", Get = "", Post = "BLabTestItem", Return = "BaseResultDataValue", ReturnType = "BLabTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBLabTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBLabTestItem(BLabTestItem entity);

        [ServiceContractDescription(Name = "新增区域项目字典VO", Desc = "新增区域项目字典", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddBLabTestItemVO", Get = "", Post = "BLabTestItem", Return = "BaseResultDataValue", ReturnType = "BLabTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBLabTestItemVO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBLabTestItemVO(ZhiFang.WeiXin.Entity.ViewObject.Request.BLabTestItemVO entity);

        [ServiceContractDescription(Name = "修改区域项目字典", Desc = "修改区域项目字典", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBLabTestItem", Get = "", Post = "BLabTestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLabTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLabTestItem(BLabTestItem entity);

        [ServiceContractDescription(Name = "修改区域项目字典指定的属性", Desc = "修改区域项目字典指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBLabTestItemByField", Get = "", Post = "BLabTestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLabTestItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLabTestItemByField(BLabTestItem entity, string fields);

        [ServiceContractDescription(Name = "修改区域项目字典指定的属性VO", Desc = "修改区域项目字典指定的属性VO", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBLabTestItemByFieldVO", Get = "", Post = "BLabTestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLabTestItemByFieldVO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLabTestItemByFieldVO(ZhiFang.WeiXin.Entity.ViewObject.Request.BLabTestItemVO entity, string fields);

        [ServiceContractDescription(Name = "删除区域项目字典", Desc = "删除区域项目字典", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelBLabTestItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBLabTestItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBLabTestItem(long id);

        [ServiceContractDescription(Name = "查询区域项目字典", Desc = "查询区域项目字典", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabTestItem", Get = "", Post = "BLabTestItem", Return = "BaseResultList<BLabTestItem>", ReturnType = "ListBLabTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabTestItem(BLabTestItem entity);

        [ServiceContractDescription(Name = "查询区域项目字典(HQL)", Desc = "查询区域项目字典(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabTestItem>", ReturnType = "ListBLabTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabTestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询区域项目字典", Desc = "通过主键ID查询区域项目字典", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabTestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabTestItem>", ReturnType = "BLabTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabTestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabTestItemById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "获取某一组套项目的项目明细信息", Desc = "获取某一组套项目的项目明细信息", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabGroupItemByPItemNoAndAreaID?page={page}&limit={limit}&fields={fields}&pitemNo={pitemNo}&areaID={areaID}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&pitemNo={pitemNo}&areaID={areaID}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabTestItem>", ReturnType = "ListBLabTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabGroupItemByPItemNoAndAreaID?page={page}&limit={limit}&fields={fields}&pitemNo={pitemNo}&areaID={areaID}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabGroupItemByPItemNoAndAreaID(int page, int limit, string fields, string pitemNo, string areaID, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取区域内项目信息", Desc = "获取区域内项目信息", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSBLabTestItemByAreaID?page={page}&limit={limit}&where={where}&sort={sort}&areaID={areaID}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&where={where}&sort={sort}&areaID={areaID}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabTestItem>", ReturnType = "ListBLabTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSBLabTestItemByAreaID?page={page}&limit={limit}&where={where}&sort={sort}&areaID={areaID}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSBLabTestItemByAreaID(int page, int limit, string where, string sort, string areaID, bool isPlanish);

        [ServiceContractDescription(Name = "获取送检单位内项目信息", Desc = "获取送检单位内项目信息", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchOSBLabTestItemByLabCode?page={page}&limit={limit}&where={where}&sort={sort}&LabCode={LabCode}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&where={where}&sort={sort}&LabCode={LabCode}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabTestItem>", ReturnType = "ListBLabTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSBLabTestItemByLabCode?page={page}&limit={limit}&where={where}&sort={sort}&LabCode={LabCode}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSBLabTestItemByLabCode(int page, int limit, string where, string sort, string LabCode, bool isPlanish);

        [ServiceContractDescription(Name = "获取区域内组套项目的项目明细信息最小细项", Desc = "获取区域内组套项目的项目明细信息最小细项", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndAreaID?page={page}&limit={limit}&fields={fields}&pitemNo={pitemNo}&areaID={areaID}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&pitemNo={pitemNo}&areaID={areaID}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabTestItem>", ReturnType = "ListBLabTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndAreaID?page={page}&limit={limit}&fields={fields}&pitemNo={pitemNo}&areaID={areaID}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndAreaID(int page, int limit, string fields, string pitemNo, string areaID, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取送检单位内组套项目的项目明细信息最小细项", Desc = "获取送检单位内组套项目的项目明细信息最小细项", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndLabCode?page={page}&limit={limit}&fields={fields}&pitemNo={pitemNo}&LabCode={LabCode}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&pitemNo={pitemNo}&LabCode={LabCode}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabTestItem>", ReturnType = "ListBLabTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndLabCode?page={page}&limit={limit}&fields={fields}&pitemNo={pitemNo}&LabCode={LabCode}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabGroupItemSubItemByPItemNoAndLabCode(int page, int limit, string fields, string pitemNo, string LabCode, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "实验室项目字典复制", Desc = "实验室项目字典复制", Url = "ZhiFangWeiXinService.svc/ST_UDTO_BLabTestItemCopy", Get = "", Post = "BLabTestItem", Return = "BaseResultList<BLabTestItem>", ReturnType = "ListBLabTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_BLabTestItemCopy", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_BLabTestItemCopy(string SourceLabCode, List<string> LabCodeList, List<string> ItemNoList, bool isall, int OverRideType);

        [ServiceContractDescription(Name = "实验室项目字典对照查询", Desc = "实验室项目字典对照查询", Url = "ZhiFangWeiXinService.svc/SearchBLabTestItemByLabCodeAndType?where={where}&LabCode={LabCode}&Type={Type}&sort={sort}", Get = "", Post = "BLabTestItem", Return = "BaseResultList<BLabTestItem>", ReturnType = "ListBLabTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchBLabTestItemByLabCodeAndType?where={where}&LabCode={LabCode}&Type={Type}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SearchBLabTestItemByLabCodeAndType(string where, string LabCode, string Type, string sort);
        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WXADS_WeiXinAccountPwdRest?WeiXinAccount={WeiXinAccount}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXADS_WeiXinAccountPwdRest(string WeiXinAccount);


        #region ItemColorAndSampleTypeDetail

        [ServiceContractDescription(Name = "新增ItemColorAndSampleTypeDetail", Desc = "新增ItemColorAndSampleTypeDetail", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddItemColorAndSampleTypeDetail", Get = "", Post = "ItemColorAndSampleTypeDetail", Return = "BaseResultDataValue", ReturnType = "ItemColorAndSampleTypeDetail")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddItemColorAndSampleTypeDetail", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddItemColorAndSampleTypeDetail(ItemColorAndSampleTypeDetail entity);

        [ServiceContractDescription(Name = "修改ItemColorAndSampleTypeDetail", Desc = "修改ItemColorAndSampleTypeDetail", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateItemColorAndSampleTypeDetail", Get = "", Post = "ItemColorAndSampleTypeDetail", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateItemColorAndSampleTypeDetail", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateItemColorAndSampleTypeDetail(ItemColorAndSampleTypeDetail entity);

        [ServiceContractDescription(Name = "修改ItemColorAndSampleTypeDetail指定的属性", Desc = "修改ItemColorAndSampleTypeDetail指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateItemColorAndSampleTypeDetailByField", Get = "", Post = "ItemColorAndSampleTypeDetail", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateItemColorAndSampleTypeDetailByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateItemColorAndSampleTypeDetailByField(ItemColorAndSampleTypeDetail entity, string fields);

        [ServiceContractDescription(Name = "删除ItemColorAndSampleTypeDetail", Desc = "删除ItemColorAndSampleTypeDetail", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelItemColorAndSampleTypeDetail?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelItemColorAndSampleTypeDetail?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelItemColorAndSampleTypeDetail(long id);

        [ServiceContractDescription(Name = "查询ItemColorAndSampleTypeDetail", Desc = "查询ItemColorAndSampleTypeDetail", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchItemColorAndSampleTypeDetail", Get = "", Post = "ItemColorAndSampleTypeDetail", Return = "BaseResultList<ItemColorAndSampleTypeDetail>", ReturnType = "ListItemColorAndSampleTypeDetail")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemColorAndSampleTypeDetail", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemColorAndSampleTypeDetail(ItemColorAndSampleTypeDetail entity);

        [ServiceContractDescription(Name = "查询ItemColorAndSampleTypeDetail(HQL)", Desc = "查询ItemColorAndSampleTypeDetail(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchItemColorAndSampleTypeDetailByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemColorAndSampleTypeDetail>", ReturnType = "ListItemColorAndSampleTypeDetail")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemColorAndSampleTypeDetailByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemColorAndSampleTypeDetailByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询ItemColorAndSampleTypeDetail", Desc = "通过主键ID查询ItemColorAndSampleTypeDetail", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchItemColorAndSampleTypeDetailById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemColorAndSampleTypeDetail>", ReturnType = "ItemColorAndSampleTypeDetail")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemColorAndSampleTypeDetailById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemColorAndSampleTypeDetailById(long id, string fields, bool isPlanish);
        #endregion

        #region ItemColorDict

        [ServiceContractDescription(Name = "新增ItemColorDict", Desc = "新增ItemColorDict", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddItemColorDict", Get = "", Post = "ItemColorDict", Return = "BaseResultDataValue", ReturnType = "ItemColorDict")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddItemColorDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddItemColorDict(ItemColorDict entity);

        [ServiceContractDescription(Name = "修改ItemColorDict", Desc = "修改ItemColorDict", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateItemColorDict", Get = "", Post = "ItemColorDict", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateItemColorDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateItemColorDict(ItemColorDict entity);

        [ServiceContractDescription(Name = "修改ItemColorDict指定的属性", Desc = "修改ItemColorDict指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateItemColorDictByField", Get = "", Post = "ItemColorDict", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateItemColorDictByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateItemColorDictByField(ItemColorDict entity, string fields);

        [ServiceContractDescription(Name = "删除ItemColorDict", Desc = "删除ItemColorDict", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelItemColorDict?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelItemColorDict?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelItemColorDict(long id);

        [ServiceContractDescription(Name = "查询ItemColorDict", Desc = "查询ItemColorDict", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchItemColorDict", Get = "", Post = "ItemColorDict", Return = "BaseResultList<ItemColorDict>", ReturnType = "ListItemColorDict")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemColorDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemColorDict(ItemColorDict entity);

        [ServiceContractDescription(Name = "查询ItemColorDict(HQL)", Desc = "查询ItemColorDict(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchItemColorDictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemColorDict>", ReturnType = "ListItemColorDict")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemColorDictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemColorDictByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询ItemColorDict", Desc = "通过主键ID查询ItemColorDict", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchItemColorDictById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemColorDict>", ReturnType = "ItemColorDict")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemColorDictById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemColorDictById(long id, string fields, bool isPlanish);
        #endregion

        #region CLIENTELE

        [ServiceContractDescription(Name = "新增CLIENTELE", Desc = "新增CLIENTELE", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddCLIENTELE", Get = "", Post = "CLIENTELE", Return = "BaseResultDataValue", ReturnType = "CLIENTELE")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddCLIENTELE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddCLIENTELE(CLIENTELE entity);

        [ServiceContractDescription(Name = "修改CLIENTELE", Desc = "修改CLIENTELE", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateCLIENTELE", Get = "", Post = "CLIENTELE", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCLIENTELE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCLIENTELE(CLIENTELE entity);

        [ServiceContractDescription(Name = "修改CLIENTELE指定的属性", Desc = "修改CLIENTELE指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateCLIENTELEByField", Get = "", Post = "CLIENTELE", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCLIENTELEByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCLIENTELEByField(CLIENTELE entity, string fields);

        [ServiceContractDescription(Name = "删除CLIENTELE", Desc = "删除CLIENTELE", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelCLIENTELE?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelCLIENTELE?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelCLIENTELE(long id);

        [ServiceContractDescription(Name = "查询CLIENTELE", Desc = "查询CLIENTELE", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELE", Get = "", Post = "CLIENTELE", Return = "BaseResultList<CLIENTELE>", ReturnType = "ListCLIENTELE")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCLIENTELE", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCLIENTELE(CLIENTELE entity);

        [ServiceContractDescription(Name = "查询CLIENTELE(HQL)", Desc = "查询CLIENTELE(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CLIENTELE>", ReturnType = "ListCLIENTELE")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCLIENTELEByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCLIENTELEByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询CLIENTELE", Desc = "通过主键ID查询CLIENTELE", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchCLIENTELEById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CLIENTELE>", ReturnType = "CLIENTELE")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCLIENTELEById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCLIENTELEById(long id, string fields, bool isPlanish);
        #endregion

        #region BusinessLogicClientControl

        [ServiceContractDescription(Name = "新增BusinessLogicClientControl", Desc = "新增BusinessLogicClientControl", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddBusinessLogicClientControl", Get = "", Post = "BusinessLogicClientControl", Return = "BaseResultDataValue", ReturnType = "BusinessLogicClientControl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBusinessLogicClientControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBusinessLogicClientControl(BusinessLogicClientControl entity);

        [ServiceContractDescription(Name = "修改BusinessLogicClientControl", Desc = "修改BusinessLogicClientControl", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBusinessLogicClientControl", Get = "", Post = "BusinessLogicClientControl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBusinessLogicClientControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBusinessLogicClientControl(BusinessLogicClientControl entity);

        [ServiceContractDescription(Name = "修改BusinessLogicClientControl指定的属性", Desc = "修改BusinessLogicClientControl指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBusinessLogicClientControlByField", Get = "", Post = "BusinessLogicClientControl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBusinessLogicClientControlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBusinessLogicClientControlByField(BusinessLogicClientControl entity, string fields);

        [ServiceContractDescription(Name = "删除BusinessLogicClientControl", Desc = "删除BusinessLogicClientControl", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelBusinessLogicClientControl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBusinessLogicClientControl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBusinessLogicClientControl(long id);

        [ServiceContractDescription(Name = "查询BusinessLogicClientControl", Desc = "查询BusinessLogicClientControl", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBusinessLogicClientControl", Get = "", Post = "BusinessLogicClientControl", Return = "BaseResultList<BusinessLogicClientControl>", ReturnType = "ListBusinessLogicClientControl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBusinessLogicClientControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBusinessLogicClientControl(BusinessLogicClientControl entity);

        [ServiceContractDescription(Name = "查询BusinessLogicClientControl(HQL)", Desc = "查询BusinessLogicClientControl(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBusinessLogicClientControlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BusinessLogicClientControl>", ReturnType = "ListBusinessLogicClientControl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBusinessLogicClientControlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBusinessLogicClientControlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询BusinessLogicClientControl", Desc = "通过主键ID查询BusinessLogicClientControl", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBusinessLogicClientControlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BusinessLogicClientControl>", ReturnType = "BusinessLogicClientControl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBusinessLogicClientControlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBusinessLogicClientControlById(long id, string fields, bool isPlanish);
        #endregion

        #region BLabGroupItem

        //[ServiceContractDescription(Name = "新增B_Lab_GroupItem", Desc = "新增B_Lab_GroupItem", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddBLabGroupItem", Get = "", Post = "BLabGroupItem", Return = "BaseResultDataValue", ReturnType = "BLabGroupItem")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBLabGroupItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_AddBLabGroupItem(BLabGroupItem entity);

        //[ServiceContractDescription(Name = "修改B_Lab_GroupItem", Desc = "修改B_Lab_GroupItem", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBLabGroupItem", Get = "", Post = "BLabGroupItem", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLabGroupItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateBLabGroupItem(BLabGroupItem entity);

        //[ServiceContractDescription(Name = "修改B_Lab_GroupItem指定的属性", Desc = "修改B_Lab_GroupItem指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBLabGroupItemByField", Get = "", Post = "BLabGroupItem", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLabGroupItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateBLabGroupItemByField(BLabGroupItem entity, string fields);

        //[ServiceContractDescription(Name = "删除B_Lab_GroupItem", Desc = "删除B_Lab_GroupItem", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelBLabGroupItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBLabGroupItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_DelBLabGroupItem(long id);

        //[ServiceContractDescription(Name = "查询B_Lab_GroupItem", Desc = "查询B_Lab_GroupItem", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabGroupItem", Get = "", Post = "BLabGroupItem", Return = "BaseResultList<BLabGroupItem>", ReturnType = "ListBLabGroupItem")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabGroupItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_SearchBLabGroupItem(BLabGroupItem entity);

        //[ServiceContractDescription(Name = "查询B_Lab_GroupItem(HQL)", Desc = "查询B_Lab_GroupItem(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabGroupItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabGroupItem>", ReturnType = "ListBLabGroupItem")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabGroupItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_SearchBLabGroupItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        //[ServiceContractDescription(Name = "通过主键ID查询B_Lab_GroupItem", Desc = "通过主键ID查询B_Lab_GroupItem", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBLabGroupItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabGroupItem>", ReturnType = "BLabGroupItem")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabGroupItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_SearchBLabGroupItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BTestItemControl

        [ServiceContractDescription(Name = "新增B_TestItemControl", Desc = "新增B_TestItemControl", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddBTestItemControl", Get = "", Post = "BTestItemControl", Return = "BaseResultDataValue", ReturnType = "BTestItemControl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBTestItemControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBTestItemControl(BTestItemControl entity);

        [ServiceContractDescription(Name = "修改B_TestItemControl", Desc = "修改B_TestItemControl", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBTestItemControl", Get = "", Post = "BTestItemControl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBTestItemControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBTestItemControl(BTestItemControl entity);

        [ServiceContractDescription(Name = "修改B_TestItemControl指定的属性", Desc = "修改B_TestItemControl指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBTestItemControlByField", Get = "", Post = "BTestItemControl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBTestItemControlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBTestItemControlByField(BTestItemControl entity, string fields);

        [ServiceContractDescription(Name = "删除B_TestItemControl", Desc = "删除B_TestItemControl", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelBTestItemControl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBTestItemControl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBTestItemControl(long id);

        [ServiceContractDescription(Name = "查询B_TestItemControl", Desc = "查询B_TestItemControl", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBTestItemControl", Get = "", Post = "BTestItemControl", Return = "BaseResultList<BTestItemControl>", ReturnType = "ListBTestItemControl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTestItemControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTestItemControl(BTestItemControl entity);

        [ServiceContractDescription(Name = "查询B_TestItemControl(HQL)", Desc = "查询B_TestItemControl(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBTestItemControlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTestItemControl>", ReturnType = "ListBTestItemControl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTestItemControlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTestItemControlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_TestItemControl", Desc = "通过主键ID查询B_TestItemControl", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBTestItemControlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTestItemControl>", ReturnType = "BTestItemControl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTestItemControlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTestItemControlById(long id, string fields, bool isPlanish);
        #endregion

        #region GenderType

        [ServiceContractDescription(Name = "新增GenderType", Desc = "新增GenderType", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddGenderType", Get = "", Post = "GenderType", Return = "BaseResultDataValue", ReturnType = "GenderType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddGenderType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddGenderType(GenderType entity);

        [ServiceContractDescription(Name = "修改GenderType", Desc = "修改GenderType", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateGenderType", Get = "", Post = "GenderType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGenderType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGenderType(GenderType entity);

        [ServiceContractDescription(Name = "修改GenderType指定的属性", Desc = "修改GenderType指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateGenderTypeByField", Get = "", Post = "GenderType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGenderTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGenderTypeByField(GenderType entity, string fields);

        [ServiceContractDescription(Name = "删除GenderType", Desc = "删除GenderType", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelGenderType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelGenderType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelGenderType(long id);

        [ServiceContractDescription(Name = "查询GenderType", Desc = "查询GenderType", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchGenderType", Get = "", Post = "GenderType", Return = "BaseResultList<GenderType>", ReturnType = "ListGenderType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGenderType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGenderType(GenderType entity);

        [ServiceContractDescription(Name = "查询GenderType(HQL)", Desc = "查询GenderType(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchGenderTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GenderType>", ReturnType = "ListGenderType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGenderTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGenderTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询GenderType", Desc = "通过主键ID查询GenderType", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchGenderTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GenderType>", ReturnType = "GenderType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGenderTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGenderTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BarCodeForm

        [ServiceContractDescription(Name = "新增BarCodeForm", Desc = "新增BarCodeForm", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddBarCodeForm", Get = "", Post = "BarCodeForm", Return = "BaseResultDataValue", ReturnType = "BarCodeForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBarCodeForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBarCodeForm(BarCodeForm entity);

        [ServiceContractDescription(Name = "修改BarCodeForm", Desc = "修改BarCodeForm", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBarCodeForm", Get = "", Post = "BarCodeForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBarCodeForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBarCodeForm(BarCodeForm entity);

        [ServiceContractDescription(Name = "修改BarCodeForm指定的属性", Desc = "修改BarCodeForm指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateBarCodeFormByField", Get = "", Post = "BarCodeForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBarCodeFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBarCodeFormByField(BarCodeForm entity, string fields);

        [ServiceContractDescription(Name = "删除BarCodeForm", Desc = "删除BarCodeForm", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelBarCodeForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBarCodeForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBarCodeForm(long id);

        [ServiceContractDescription(Name = "查询BarCodeForm", Desc = "查询BarCodeForm", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBarCodeForm", Get = "", Post = "BarCodeForm", Return = "BaseResultList<BarCodeForm>", ReturnType = "ListBarCodeForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBarCodeForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBarCodeForm(BarCodeForm entity);

        [ServiceContractDescription(Name = "查询BarCodeForm(HQL)", Desc = "查询BarCodeForm(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBarCodeFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BarCodeForm>", ReturnType = "ListBarCodeForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBarCodeFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBarCodeFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询BarCodeForm", Desc = "通过主键ID查询BarCodeForm", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchBarCodeFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BarCodeForm>", ReturnType = "BarCodeForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBarCodeFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBarCodeFormById(long id, string fields, bool isPlanish);
        #endregion

        #region PatDiagInfo

        [ServiceContractDescription(Name = "新增PatDiagInfo", Desc = "新增PatDiagInfo", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddPatDiagInfo", Get = "", Post = "PatDiagInfo", Return = "BaseResultDataValue", ReturnType = "PatDiagInfo")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddPatDiagInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddPatDiagInfo(PatDiagInfo entity);

        [ServiceContractDescription(Name = "修改PatDiagInfo", Desc = "修改PatDiagInfo", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdatePatDiagInfo", Get = "", Post = "PatDiagInfo", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePatDiagInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePatDiagInfo(PatDiagInfo entity);

        [ServiceContractDescription(Name = "修改PatDiagInfo指定的属性", Desc = "修改PatDiagInfo指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdatePatDiagInfoByField", Get = "", Post = "PatDiagInfo", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdatePatDiagInfoByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdatePatDiagInfoByField(PatDiagInfo entity, string fields);

        [ServiceContractDescription(Name = "删除PatDiagInfo", Desc = "删除PatDiagInfo", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelPatDiagInfo?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelPatDiagInfo?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelPatDiagInfo(long id);

        [ServiceContractDescription(Name = "查询PatDiagInfo", Desc = "查询PatDiagInfo", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchPatDiagInfo", Get = "", Post = "PatDiagInfo", Return = "BaseResultList<PatDiagInfo>", ReturnType = "ListPatDiagInfo")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPatDiagInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPatDiagInfo(PatDiagInfo entity);

        [ServiceContractDescription(Name = "查询PatDiagInfo(HQL)", Desc = "查询PatDiagInfo(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchPatDiagInfoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PatDiagInfo>", ReturnType = "ListPatDiagInfo")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPatDiagInfoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPatDiagInfoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询PatDiagInfo", Desc = "通过主键ID查询PatDiagInfo", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchPatDiagInfoById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PatDiagInfo>", ReturnType = "PatDiagInfo")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchPatDiagInfoById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchPatDiagInfoById(long id, string fields, bool isPlanish);
        #endregion

        #region NRequestItem

        [ServiceContractDescription(Name = "新增NRequestItem", Desc = "新增NRequestItem", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddNRequestItem", Get = "", Post = "NRequestItem", Return = "BaseResultDataValue", ReturnType = "NRequestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddNRequestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddNRequestItem(NRequestItem entity);

        [ServiceContractDescription(Name = "修改NRequestItem", Desc = "修改NRequestItem", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateNRequestItem", Get = "", Post = "NRequestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateNRequestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateNRequestItem(NRequestItem entity);

        [ServiceContractDescription(Name = "修改NRequestItem指定的属性", Desc = "修改NRequestItem指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateNRequestItemByField", Get = "", Post = "NRequestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateNRequestItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateNRequestItemByField(NRequestItem entity, string fields);

        [ServiceContractDescription(Name = "删除NRequestItem", Desc = "删除NRequestItem", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelNRequestItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelNRequestItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelNRequestItem(long id);

        [ServiceContractDescription(Name = "查询NRequestItem", Desc = "查询NRequestItem", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchNRequestItem", Get = "", Post = "NRequestItem", Return = "BaseResultList<NRequestItem>", ReturnType = "ListNRequestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchNRequestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchNRequestItem(NRequestItem entity);

        [ServiceContractDescription(Name = "查询NRequestItem(HQL)", Desc = "查询NRequestItem(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchNRequestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<NRequestItem>", ReturnType = "ListNRequestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchNRequestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchNRequestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询NRequestItem", Desc = "通过主键ID查询NRequestItem", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchNRequestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<NRequestItem>", ReturnType = "NRequestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchNRequestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchNRequestItemById(long id, string fields, bool isPlanish);
        #endregion

        #region NRequestForm

        [ServiceContractDescription(Name = "新增NRequestForm", Desc = "新增NRequestForm", Url = "ZhiFangWeiXinService.svc/ST_UDTO_AddNRequestForm", Get = "", Post = "NRequestForm", Return = "BaseResultDataValue", ReturnType = "NRequestForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddNRequestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddNRequestForm(NRequestForm entity);

        [ServiceContractDescription(Name = "修改NRequestForm", Desc = "修改NRequestForm", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateNRequestForm", Get = "", Post = "NRequestForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateNRequestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateNRequestForm(NRequestForm entity);

        [ServiceContractDescription(Name = "修改NRequestForm指定的属性", Desc = "修改NRequestForm指定的属性", Url = "ZhiFangWeiXinService.svc/ST_UDTO_UpdateNRequestFormByField", Get = "", Post = "NRequestForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateNRequestFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateNRequestFormByField(NRequestForm entity, string fields);

        [ServiceContractDescription(Name = "删除NRequestForm", Desc = "删除NRequestForm", Url = "ZhiFangWeiXinService.svc/ST_UDTO_DelNRequestForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelNRequestForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelNRequestForm(long id);

        [ServiceContractDescription(Name = "查询NRequestForm", Desc = "查询NRequestForm", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchNRequestForm", Get = "", Post = "NRequestForm", Return = "BaseResultList<NRequestForm>", ReturnType = "ListNRequestForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchNRequestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchNRequestForm(NRequestForm entity);

        [ServiceContractDescription(Name = "查询NRequestForm(HQL)", Desc = "查询NRequestForm(HQL)", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchNRequestFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<NRequestForm>", ReturnType = "ListNRequestForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchNRequestFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchNRequestFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询NRequestForm", Desc = "通过主键ID查询NRequestForm", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchNRequestFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<NRequestForm>", ReturnType = "NRequestForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchNRequestFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchNRequestFormById(long id, string fields, bool isPlanish);
        #endregion
        #region BLabDistrict

        [ServiceContractDescription(Name = "新增B_Lab_District", Desc = "新增B_Lab_District", Url = "ST_UDTO_AddBLabDistrict", Get = "", Post = "BLabDistrict", Return = "BaseResultDataValue", ReturnType = "BLabDistrict")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBLabDistrict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBLabDistrict(BLabDistrict entity);

        [ServiceContractDescription(Name = "修改B_Lab_District", Desc = "修改B_Lab_District", Url = "ST_UDTO_UpdateBLabDistrict", Get = "", Post = "BLabDistrict", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLabDistrict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLabDistrict(BLabDistrict entity);

        [ServiceContractDescription(Name = "修改B_Lab_District指定的属性", Desc = "修改B_Lab_District指定的属性", Url = "ST_UDTO_UpdateBLabDistrictByField", Get = "", Post = "BLabDistrict", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLabDistrictByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLabDistrictByField(BLabDistrict entity, string fields);

        [ServiceContractDescription(Name = "删除B_Lab_District", Desc = "删除B_Lab_District", Url = "ST_UDTO_DelBLabDistrict?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBLabDistrict?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBLabDistrict(long id);

        [ServiceContractDescription(Name = "查询B_Lab_District", Desc = "查询B_Lab_District", Url = "ST_UDTO_SearchBLabDistrict", Get = "", Post = "BLabDistrict", Return = "BaseResultList<BLabDistrict>", ReturnType = "ListBLabDistrict")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabDistrict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabDistrict(BLabDistrict entity);

        [ServiceContractDescription(Name = "查询B_Lab_District(HQL)", Desc = "查询B_Lab_District(HQL)", Url = "ST_UDTO_SearchBLabDistrictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabDistrict>", ReturnType = "ListBLabDistrict")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabDistrictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabDistrictByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_Lab_District", Desc = "通过主键ID查询B_Lab_District", Url = "ST_UDTO_SearchBLabDistrictById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabDistrict>", ReturnType = "BLabDistrict")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabDistrictById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabDistrictById(long id, string fields, bool isPlanish);
        #endregion

        [ServiceContractDescription(Name = "", Desc = "", Url = "", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/MobileSendTest?PhoneNumber={PhoneNumber}&Content={Content}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string MobileSendTest(string PhoneNumber, string Content);
        #region 微信消费采样

        [ServiceContractDescription(Name = "申请列表", Desc = "申请列表", Url = "ZhiFangWeiXinService.svc/GetNRequestFromListByByDetailsAndRBAC?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetNRequestFromListByByDetailsAndRBAC?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetNRequestFromListByByDetailsAndRBAC(int page, int limit, string fields, string where, string sort, bool isPlanish);
        
        [ServiceContractDescription(Name = "订单套餐", Desc = "订单套餐", Url = "ZhiFangWeiXinService.svc/OSConsumerUserOrderForm?PayCode={PayCode}&WeblisSourceOrgID={WeblisSourceOrgID}&WeblisSourceOrgName={WeblisSourceOrgName}&AreaID={AreaID}", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/OSConsumerUserOrderForm?PayCode={PayCode}&WeblisSourceOrgID={WeblisSourceOrgID}&WeblisSourceOrgName={WeblisSourceOrgName}&AreaID={AreaID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue OSConsumerUserOrderForm(string PayCode,string WeblisSourceOrgID,string WeblisSourceOrgName,string AreaID);

        [ServiceContractDescription(Name = "根据检验类型获取检验项目列表", Desc = "根据检验类型获取检验项目列表", Url = "", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetTestItem?supergroupno={supergroupno}&itemkey={itemkey}&rows={rows}&page={page}&labcode={labcode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetTestItem(string supergroupno, string itemkey, int rows, int page, string labcode);

        [ServiceContractDescription(Name = "根据检验项目ID查询检验明细", Desc = "根据检验项目ID查询检验明细", Url = "", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetTestDetailByItemID?itemid={itemid}&labcode={labcode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetTestDetailByItemID(string itemid, string labcode);

        [ServiceContractDescription(Name = "微信消费采样消费码查询", Desc = "微信消费采样消费码查询", Url = "", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetWeChatConsumptionSamplingInfo?PayCode={PayCode}&WeblisSourceOrgID={WeblisSourceOrgID}&WeblisSourceOrgName={WeblisSourceOrgName}&AreaID={AreaID}&AreaNo={AreaNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetWeChatConsumptionSamplingInfo(string PayCode, string WeblisSourceOrgID, string WeblisSourceOrgName, string AreaID,string AreaNo);

        /// <summary>
        /// 消费码解锁
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UnConsumerUserOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UnConsumerUserOrderForm(ConsumerUserOrderFormVO jsonentity);

        /// <summary>
        /// 保存申请单
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SaveOSConsumerUserOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SaveOSConsumerUserOrderForm(NrequestCombiItemBarCodeEntity jsonentity);

        /// <summary>
        /// 查询订单信息
        /// </summary>
        /// <param name="jsonentity"></param>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchUnConsumerUserOrderFormList?PayCode={PayCode}&WeblisSourceOrgID={WeblisSourceOrgID}&WeblisSourceOrgName={WeblisSourceOrgName}&ConsumerAreaID={ConsumerAreaID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SearchUnConsumerUserOrderFormList(string PayCode, string WeblisSourceOrgID, string WeblisSourceOrgName, string ConsumerAreaID);
        
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PrintNRequestForm_PDF?where={where}&LabId={LabId}&StartDateTime={StartDateTime}&EndDateTime={EndDateTime}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue PrintNRequestForm_PDF(string where, long LabId, string StartDateTime, string EndDateTime);

        #endregion
    }
}
