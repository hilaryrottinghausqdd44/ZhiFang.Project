using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.ServerWCF
{
    [ServiceContract]
    public interface ILIIPCommonService
    {

        #region BHospital

        [ServiceContractDescription(Name = "新增医院字典", Desc = "新增医院字典", Url = "LIIPCommonService.svc/ST_UDTO_AddBHospital", Get = "", Post = "BHospital", Return = "BaseResultDataValue", ReturnType = "BHospital")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBHospital", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBHospital(BHospital entity);

        [ServiceContractDescription(Name = "修改医院字典", Desc = "修改医院字典", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospital", Get = "", Post = "BHospital", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospital", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospital(BHospital entity);

        [ServiceContractDescription(Name = "修改医院字典指定的属性", Desc = "修改医院字典指定的属性", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalByField", Get = "", Post = "BHospital", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalByField(BHospital entity, string fields);

        [ServiceContractDescription(Name = "删除医院字典", Desc = "删除医院字典", Url = "LIIPCommonService.svc/ST_UDTO_DelBHospital?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBHospital?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBHospital(long id);

        [ServiceContractDescription(Name = "查询医院字典", Desc = "查询医院字典", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospital", Get = "", Post = "BHospital", Return = "BaseResultList<BHospital>", ReturnType = "ListBHospital")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospital", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospital(BHospital entity);

        [ServiceContractDescription(Name = "查询医院字典(HQL)", Desc = "查询医院字典(HQL)", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospital>", ReturnType = "ListBHospital")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医院字典", Desc = "通过主键ID查询医院字典", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospital>", ReturnType = "BHospital")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalById(long id, string fields, bool isPlanish);
        #endregion

        #region BHospitalArea

        [ServiceContractDescription(Name = "新增医院区域", Desc = "新增医院区域", Url = "LIIPCommonService.svc/ST_UDTO_AddBHospitalArea", Get = "", Post = "BHospitalArea", Return = "BaseResultDataValue", ReturnType = "BHospitalArea")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBHospitalArea", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBHospitalArea(BHospitalArea entity);

        [ServiceContractDescription(Name = "修改医院区域", Desc = "修改医院区域", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalArea", Get = "", Post = "BHospitalArea", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalArea", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalArea(BHospitalArea entity);

        [ServiceContractDescription(Name = "修改医院区域指定的属性", Desc = "修改医院区域指定的属性", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalAreaByField", Get = "", Post = "BHospitalArea", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalAreaByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalAreaByField(BHospitalArea entity, string fields);

        [ServiceContractDescription(Name = "删除医院区域", Desc = "删除医院区域", Url = "LIIPCommonService.svc/ST_UDTO_DelBHospitalArea?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBHospitalArea?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBHospitalArea(long id);

        [ServiceContractDescription(Name = "查询医院区域", Desc = "查询医院区域", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalArea", Get = "", Post = "BHospitalArea", Return = "BaseResultList<BHospitalArea>", ReturnType = "ListBHospitalArea")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalArea", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalArea(BHospitalArea entity);

        [ServiceContractDescription(Name = "查询医院区域(HQL)", Desc = "查询医院区域(HQL)", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalAreaByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalArea>", ReturnType = "ListBHospitalArea")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalAreaByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalAreaByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医院区域", Desc = "通过主键ID查询医院区域", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalAreaById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalArea>", ReturnType = "BHospitalArea")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalAreaById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalAreaById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID过滤区域子节点", Desc = "通过主键ID过滤子节点", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalAreaFiltrationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalArea>", ReturnType = "BHospitalArea")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalAreaFiltrationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalAreaFiltrationById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询拼接区域等级", Desc = "通过主键ID查询拼接区域等级", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalAreaLevelNameTreeByID?id={id}", Get = "id={id}", Post = "", Return = "BaseResultList<BHospitalArea>", ReturnType = "BHospitalArea")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalAreaLevelNameTreeByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_UpdateBHospitalAreaLevelNameTreeByID(long id);

        [ServiceContractDescription(Name = "通过主键ID查询子区域", Desc = "通过主键ID查询子区域", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalAreaSonByID?id={id}", Get = "id={id}", Post = "", Return = "BaseResultList<BHospitalArea>", ReturnType = "BHospitalArea")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalAreaSonByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalAreaSonByID(long id);
        #endregion

        #region BHospitalDept

        [ServiceContractDescription(Name = "新增医院科室", Desc = "新增医院科室", Url = "LIIPCommonService.svc/ST_UDTO_AddBHospitalDept", Get = "", Post = "BHospitalDept", Return = "BaseResultDataValue", ReturnType = "BHospitalDept")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBHospitalDept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBHospitalDept(BHospitalDept entity);

        [ServiceContractDescription(Name = "修改医院科室", Desc = "修改医院科室", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalDept", Get = "", Post = "BHospitalDept", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalDept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalDept(BHospitalDept entity);

        [ServiceContractDescription(Name = "修改医院科室指定的属性", Desc = "修改医院科室指定的属性", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalDeptByField", Get = "", Post = "BHospitalDept", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalDeptByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalDeptByField(BHospitalDept entity, string fields);

        [ServiceContractDescription(Name = "删除医院科室", Desc = "删除医院科室", Url = "LIIPCommonService.svc/ST_UDTO_DelBHospitalDept?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBHospitalDept?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBHospitalDept(long id);

        [ServiceContractDescription(Name = "查询医院科室", Desc = "查询医院科室", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalDept", Get = "", Post = "BHospitalDept", Return = "BaseResultList<BHospitalDept>", ReturnType = "ListBHospitalDept")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalDept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalDept(BHospitalDept entity);

        [ServiceContractDescription(Name = "查询医院科室(HQL)", Desc = "查询医院科室(HQL)", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalDeptByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalDept>", ReturnType = "ListBHospitalDept")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalDeptByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalDeptByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医院科室", Desc = "通过主键ID查询医院科室", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalDeptById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalDept>", ReturnType = "BHospitalDept")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalDeptById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalDeptById(long id, string fields, bool isPlanish);
        #endregion

        #region BHospitalLevel

        [ServiceContractDescription(Name = "新增医院级别", Desc = "新增医院级别", Url = "LIIPCommonService.svc/ST_UDTO_AddBHospitalLevel", Get = "", Post = "BHospitalLevel", Return = "BaseResultDataValue", ReturnType = "BHospitalLevel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBHospitalLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBHospitalLevel(BHospitalLevel entity);

        [ServiceContractDescription(Name = "修改医院级别", Desc = "修改医院级别", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalLevel", Get = "", Post = "BHospitalLevel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalLevel(BHospitalLevel entity);

        [ServiceContractDescription(Name = "修改医院级别指定的属性", Desc = "修改医院级别指定的属性", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalLevelByField", Get = "", Post = "BHospitalLevel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalLevelByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalLevelByField(BHospitalLevel entity, string fields);

        [ServiceContractDescription(Name = "删除医院级别", Desc = "删除医院级别", Url = "LIIPCommonService.svc/ST_UDTO_DelBHospitalLevel?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBHospitalLevel?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBHospitalLevel(long id);

        [ServiceContractDescription(Name = "查询医院级别", Desc = "查询医院级别", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalLevel", Get = "", Post = "BHospitalLevel", Return = "BaseResultList<BHospitalLevel>", ReturnType = "ListBHospitalLevel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalLevel(BHospitalLevel entity);

        [ServiceContractDescription(Name = "查询医院级别(HQL)", Desc = "查询医院级别(HQL)", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalLevelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalLevel>", ReturnType = "ListBHospitalLevel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalLevelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalLevelByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医院级别", Desc = "通过主键ID查询医院级别", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalLevelById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalLevel>", ReturnType = "BHospitalLevel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalLevelById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalLevelById(long id, string fields, bool isPlanish);
        #endregion

        #region BHospitalType

        [ServiceContractDescription(Name = "新增医院分类", Desc = "新增医院分类", Url = "LIIPCommonService.svc/ST_UDTO_AddBHospitalType", Get = "", Post = "BHospitalType", Return = "BaseResultDataValue", ReturnType = "BHospitalType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBHospitalType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBHospitalType(BHospitalType entity);

        [ServiceContractDescription(Name = "修改医院分类", Desc = "修改医院分类", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalType", Get = "", Post = "BHospitalType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalType(BHospitalType entity);

        [ServiceContractDescription(Name = "修改医院分类指定的属性", Desc = "修改医院分类指定的属性", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalTypeByField", Get = "", Post = "BHospitalType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalTypeByField(BHospitalType entity, string fields);

        [ServiceContractDescription(Name = "删除医院分类", Desc = "删除医院分类", Url = "LIIPCommonService.svc/ST_UDTO_DelBHospitalType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBHospitalType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBHospitalType(long id);

        [ServiceContractDescription(Name = "查询医院分类", Desc = "查询医院分类", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalType", Get = "", Post = "BHospitalType", Return = "BaseResultList<BHospitalType>", ReturnType = "ListBHospitalType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalType(BHospitalType entity);

        [ServiceContractDescription(Name = "查询医院分类(HQL)", Desc = "查询医院分类(HQL)", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalType>", ReturnType = "ListBHospitalType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医院分类", Desc = "通过主键ID查询医院分类", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalType>", ReturnType = "BHospitalType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BHospitalTypeLink

        [ServiceContractDescription(Name = "新增B_HospitalTypeLink", Desc = "新增B_HospitalTypeLink", Url = "LIIPCommonService.svc/ST_UDTO_AddBHospitalTypeLink", Get = "", Post = "BHospitalTypeLink", Return = "BaseResultDataValue", ReturnType = "BHospitalTypeLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBHospitalTypeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBHospitalTypeLink(BHospitalTypeLink entity);

        [ServiceContractDescription(Name = "修改B_HospitalTypeLink", Desc = "修改B_HospitalTypeLink", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalTypeLink", Get = "", Post = "BHospitalTypeLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalTypeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalTypeLink(BHospitalTypeLink entity);

        [ServiceContractDescription(Name = "修改B_HospitalTypeLink指定的属性", Desc = "修改B_HospitalTypeLink指定的属性", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalTypeLinkByField", Get = "", Post = "BHospitalTypeLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalTypeLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalTypeLinkByField(BHospitalTypeLink entity, string fields);

        [ServiceContractDescription(Name = "删除B_HospitalTypeLink", Desc = "删除B_HospitalTypeLink", Url = "LIIPCommonService.svc/ST_UDTO_DelBHospitalTypeLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBHospitalTypeLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBHospitalTypeLink(long id);

        [ServiceContractDescription(Name = "查询B_HospitalTypeLink", Desc = "查询B_HospitalTypeLink", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalTypeLink", Get = "", Post = "BHospitalTypeLink", Return = "BaseResultList<BHospitalTypeLink>", ReturnType = "ListBHospitalTypeLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalTypeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalTypeLink(BHospitalTypeLink entity);

        [ServiceContractDescription(Name = "查询B_HospitalTypeLink(HQL)", Desc = "查询B_HospitalTypeLink(HQL)", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalTypeLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalTypeLink>", ReturnType = "ListBHospitalTypeLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalTypeLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalTypeLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_HospitalTypeLink", Desc = "通过主键ID查询B_HospitalTypeLink", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalTypeLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalTypeLink>", ReturnType = "BHospitalTypeLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalTypeLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalTypeLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region BHospitalEmpLink

        [ServiceContractDescription(Name = "新增B_HospitalEmpLink", Desc = "新增B_HospitalEmpLink", Url = "LIIPCommonService.svc/ST_UDTO_AddBHospitalEmpLink", Get = "", Post = "BHospitalEmpLink", Return = "BaseResultDataValue", ReturnType = "BHospitalEmpLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBHospitalEmpLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBHospitalEmpLink(BHospitalEmpLink entity);

        [ServiceContractDescription(Name = "新增B_HospitalEmpLink", Desc = "新增B_HospitalEmpLink", Url = "LIIPCommonService.svc/ST_UDTO_BatchAddBHospitalEmpLink", Get = "", Post = "BHospitalEmpLink", Return = "BaseResultDataValue", ReturnType = "BHospitalEmpLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_BatchAddBHospitalEmpLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_BatchAddBHospitalEmpLink(List<BHospitalEmpLink> entity);

        //[ServiceContractDescription(Name = "修改B_HospitalEmpLink", Desc = "修改B_HospitalEmpLink", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalEmpLink", Get = "", Post = "BHospitalEmpLink", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalEmpLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateBHospitalEmpLink(BHospitalEmpLink entity);

        //[ServiceContractDescription(Name = "修改B_HospitalEmpLink指定的属性", Desc = "修改B_HospitalEmpLink指定的属性", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalEmpLinkByField", Get = "", Post = "BHospitalEmpLink", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalEmpLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateBHospitalEmpLinkByField(BHospitalEmpLink entity, string fields);

        [ServiceContractDescription(Name = "删除B_HospitalEmpLink", Desc = "删除B_HospitalEmpLink", Url = "LIIPCommonService.svc/ST_UDTO_DelBHospitalEmpLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBHospitalEmpLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBHospitalEmpLink(long id);


        [ServiceContractDescription(Name = "设置关系类型", Desc = "删除B_HospitalEmpLink", Url = "LIIPCommonService.svc/BHospitalEmpLinkSetLinkType?id={id}&typeid={typeid}", Get = "id={id}&typeid={typeid}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/BHospitalEmpLinkSetLinkType?id={id}&typeid={typeid}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool BHospitalEmpLinkSetLinkType(long id, long typeid);

        //[ServiceContractDescription(Name = "查询B_HospitalEmpLink", Desc = "查询B_HospitalEmpLink", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalEmpLink", Get = "", Post = "BHospitalEmpLink", Return = "BaseResultList<BHospitalEmpLink>", ReturnType = "ListBHospitalEmpLink")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalEmpLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_SearchBHospitalEmpLink(BHospitalEmpLink entity);

        [ServiceContractDescription(Name = "查询B_HospitalEmpLink(HQL)", Desc = "查询B_HospitalEmpLink(HQL)", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalEmpLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalEmpLink>", ReturnType = "ListBHospitalEmpLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalEmpLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalEmpLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询员工所关联的医院列表", Desc = "查询员工所关联的医院列表", Url = "LIIPCommonService.svc/ST_UDTO_SearchSelectHospitalListByEmpId?page={page}&limit={limit}&fields={fields}&EmpId={EmpId}&sort={sort}&isPlanish={isPlanish}&flag={flag}", Get = "page={page}&limit={limit}&fields={fields}&EmpId={EmpId}&sort={sort}&isPlanish={isPlanish}&flag={flag}", Post = "", Return = "BaseResultList<BHospitalEmpLink>", ReturnType = "ListBHospitalEmpLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSelectHospitalListByEmpId?page={page}&limit={limit}&fields={fields}&EmpId={EmpId}&sort={sort}&isPlanish={isPlanish}&flag={flag}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSelectHospitalListByEmpId(int page, int limit, string fields, string EmpId, string sort, bool isPlanish, bool flag);

        [ServiceContractDescription(Name = "通过主键ID查询B_HospitalEmpLink", Desc = "通过主键ID查询B_HospitalEmpLink", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalEmpLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalEmpLink>", ReturnType = "BHospitalEmpLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalEmpLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalEmpLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region SAccountRegister

        [ServiceContractDescription(Name = "新增S_AccountRegister", Desc = "新增S_AccountRegister", Url = "SingleTableService.svc/ST_UDTO_AddSAccountRegister", Get = "", Post = "SAccountRegister", Return = "BaseResultDataValue", ReturnType = "SAccountRegister")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSAccountRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSAccountRegister(SAccountRegister entity);

        //[ServiceContractDescription(Name = "修改S_AccountRegister指定的属性", Desc = "修改S_AccountRegister指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateSAccountRegisterByField", Get = "", Post = "SAccountRegister", Return = "BaseResultBool", ReturnType = "Bool")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSAccountRegisterByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultBool ST_UDTO_UpdateSAccountRegisterByField(SAccountRegister entity, string fields);
       

        [ServiceContractDescription(Name = "查询S_AccountRegister(HQL)", Desc = "查询S_AccountRegister(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchSAccountRegisterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SAccountRegister>", ReturnType = "ListSAccountRegister")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSAccountRegisterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSAccountRegisterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询S_AccountRegister", Desc = "通过主键ID查询S_AccountRegister", Url = "SingleTableService.svc/ST_UDTO_SearchSAccountRegisterById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SAccountRegister>", ReturnType = "SAccountRegister")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSAccountRegisterById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSAccountRegisterById(long id, string fields, bool isPlanish);
        #endregion

        [ServiceContractDescription(Name = "查询医院区域树列表(HQL)", Desc = "查询医院区域树列表(HQL)", Url = "LIIPCommonService.svc/ST_UDTO_SearchTreeBHospitalArea", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchTreeBHospitalArea", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchTreeBHospitalArea();
        [ServiceContractDescription(Name = "查询医院区域(HQL)", Desc = "查询医院区域(HQL)", Url = "LIIPCommonService.svc/ST_UDTO_SearchTreeGridBHospitalAreaByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalArea>", ReturnType = "ListBHospitalArea")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchTreeGridBHospitalAreaByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchTreeGridBHospitalAreaByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询医院所属区域中心医院Code", Desc = "查询医院所属区域中心医院Code", Url = "LIIPCommonService.svc/ST_UDTO_GetBHospitalAreaCenterHospitalLabCodeByLabCode?LabCode={LabCode}", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetBHospitalAreaCenterHospitalLabCodeByLabCode?LabCode={LabCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetBHospitalAreaCenterHospitalLabCodeByLabCode(string LabCode);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_GetMSGParameterByParaNo?paraNo={paraNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_GetMSGParameterByParaNo(string paraNo);

        [ServiceContractDescription(Name = "根据条件修改医院区域指定的属性", Desc = "修改医院区域指定的属性", Url = "LIIPCommonService.svc/ST_UDTO_UpdateBHospitalAreaByWhere", Get = "", Post = "BHospitalArea", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalAreaByWhere", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalAreaByWhere(BHospitalArea entity, string fields,string where);


        [ServiceContractDescription(Name = "审批申请", Desc = "审批申请", Url = "LIIPCommonService.svc/ST_UDTO_ApprovalSAccountRegister", Get = "", Post = "BHospitalArea", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ApprovalSAccountRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_ApprovalSAccountRegister(SAccountRegister entity,bool IsPass);

        [ServiceContractDescription(Name = "查询B_HospitalEmpLink和账户信息(HQL)", Desc = "查询B_HospitalEmpLink(HQL)", Url = "LIIPCommonService.svc/ST_UDTO_SearchBHospitalEmpLinkAndAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalEmpLink>", ReturnType = "ListBHospitalEmpLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalEmpLinkAndAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalEmpLinkAndAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

    }
}
