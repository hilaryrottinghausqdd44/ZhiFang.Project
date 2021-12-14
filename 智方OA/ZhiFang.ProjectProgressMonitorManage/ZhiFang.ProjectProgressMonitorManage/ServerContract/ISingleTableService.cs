using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using ZhiFang.Entity.RBAC;
using System.ServiceModel.Web;
using System.ComponentModel;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;

namespace ZhiFang.ProjectProgressMonitorManage.ServerContract
{
    [ServiceContract]
    public interface ISingleTableService
    {

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

        #region BEquip

        [ServiceContractDescription(Name = "新增仪器表", Desc = "新增仪器表", Url = "SingleTableService.svc/ST_UDTO_AddBEquip", Get = "", Post = "BEquip", Return = "BaseResultDataValue", ReturnType = "BEquip")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBEquip(BEquip entity);

        [ServiceContractDescription(Name = "修改仪器表", Desc = "修改仪器表", Url = "SingleTableService.svc/ST_UDTO_UpdateBEquip", Get = "", Post = "BEquip", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBEquip(BEquip entity);

        [ServiceContractDescription(Name = "修改仪器表指定的属性", Desc = "修改仪器表指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBEquipByField", Get = "", Post = "BEquip", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBEquipByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBEquipByField(BEquip entity, string fields);

        [ServiceContractDescription(Name = "删除仪器表", Desc = "删除仪器表", Url = "SingleTableService.svc/ST_UDTO_DelBEquip?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBEquip?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBEquip(long id);

        [ServiceContractDescription(Name = "查询仪器表", Desc = "查询仪器表", Url = "SingleTableService.svc/ST_UDTO_SearchBEquip", Get = "", Post = "BEquip", Return = "BaseResultList<BEquip>", ReturnType = "ListBEquip")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBEquip(BEquip entity);

        [ServiceContractDescription(Name = "查询仪器表(HQL)", Desc = "查询仪器表(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBEquipByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BEquip>", ReturnType = "ListBEquip")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBEquipByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBEquipByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询仪器表", Desc = "通过主键ID查询仪器表", Url = "SingleTableService.svc/ST_UDTO_SearchBEquipById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BEquip>", ReturnType = "BEquip")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBEquipById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBEquipById(long id, string fields, bool isPlanish);
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
    }
}
