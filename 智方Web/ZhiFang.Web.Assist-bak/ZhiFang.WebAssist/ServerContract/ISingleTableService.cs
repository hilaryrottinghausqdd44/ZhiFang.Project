using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.WebAssist;
using ZhiFang.ServiceCommon.RBAC;
using System.Collections.Generic;

namespace ZhiFang.WebAssist.ServerContract
{
    [ServiceContract]
    public interface ISingleTableService
    { 

        #region BCountry

        [ServiceContractDescription(Name = "新增国家", Desc = "新增国家", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBCountry", Get = "", Post = "BCountry", Return = "BaseResultDataValue", ReturnType = "BCountry")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBCountry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBCountry(BCountry entity);

        [ServiceContractDescription(Name = "修改国家", Desc = "修改国家", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBCountry", Get = "", Post = "BCountry", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCountry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCountry(BCountry entity);

        [ServiceContractDescription(Name = "修改国家指定的属性", Desc = "修改国家指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBCountryByField", Get = "", Post = "BCountry", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCountryByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCountryByField(BCountry entity, string fields);

        [ServiceContractDescription(Name = "删除国家", Desc = "删除国家", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBCountry?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBCountry?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBCountry(long id);

        [ServiceContractDescription(Name = "查询国家", Desc = "查询国家", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBCountry", Get = "", Post = "BCountry", Return = "BaseResultList<BCountry>", ReturnType = "ListBCountry")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCountry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCountry(BCountry entity);

        [ServiceContractDescription(Name = "查询国家(HQL)", Desc = "查询国家(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBCountryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCountry>", ReturnType = "ListBCountry")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCountryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCountryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询国家", Desc = "通过主键ID查询国家", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBCountryById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCountry>", ReturnType = "BCountry")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCountryById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCountryById(long id, string fields, bool isPlanish);
        #endregion

        #region BCity

        [ServiceContractDescription(Name = "新增城市", Desc = "新增城市", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBCity", Get = "", Post = "BCity", Return = "BaseResultDataValue", ReturnType = "BCity")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBCity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBCity(BCity entity);

        [ServiceContractDescription(Name = "修改城市", Desc = "修改城市", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBCity", Get = "", Post = "BCity", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCity(BCity entity);

        [ServiceContractDescription(Name = "修改城市指定的属性", Desc = "修改城市指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBCityByField", Get = "", Post = "BCity", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCityByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCityByField(BCity entity, string fields);

        [ServiceContractDescription(Name = "删除城市", Desc = "删除城市", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBCity?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBCity?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBCity(long id);

        [ServiceContractDescription(Name = "查询城市", Desc = "查询城市", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBCity", Get = "", Post = "BCity", Return = "BaseResultList<BCity>", ReturnType = "ListBCity")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCity(BCity entity);

        [ServiceContractDescription(Name = "查询城市(HQL)", Desc = "查询城市(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBCityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCity>", ReturnType = "ListBCity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询城市", Desc = "通过主键ID查询城市", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBCityById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCity>", ReturnType = "BCity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCityById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCityById(long id, string fields, bool isPlanish);
        #endregion

        #region BDegree

        [ServiceContractDescription(Name = "新增学位", Desc = "新增学位", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBDegree", Get = "", Post = "BDegree", Return = "BaseResultDataValue", ReturnType = "BDegree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBDegree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBDegree(BDegree entity);

        [ServiceContractDescription(Name = "修改学位", Desc = "修改学位", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBDegree", Get = "", Post = "BDegree", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDegree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDegree(BDegree entity);

        [ServiceContractDescription(Name = "修改学位指定的属性", Desc = "修改学位指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBDegreeByField", Get = "", Post = "BDegree", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDegreeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDegreeByField(BDegree entity, string fields);

        [ServiceContractDescription(Name = "删除学位", Desc = "删除学位", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBDegree?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBDegree?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBDegree(long id);

        [ServiceContractDescription(Name = "查询学位", Desc = "查询学位", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDegree", Get = "", Post = "BDegree", Return = "BaseResultList<BDegree>", ReturnType = "ListBDegree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDegree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDegree(BDegree entity);

        [ServiceContractDescription(Name = "查询学位(HQL)", Desc = "查询学位(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDegreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDegree>", ReturnType = "ListBDegree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDegreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDegreeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询学位", Desc = "通过主键ID查询学位", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDegreeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDegree>", ReturnType = "BDegree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDegreeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDegreeById(long id, string fields, bool isPlanish);
        #endregion        

        #region BEducationLevel

        [ServiceContractDescription(Name = "新增学历", Desc = "新增学历", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBEducationLevel", Get = "", Post = "BEducationLevel", Return = "BaseResultDataValue", ReturnType = "BEducationLevel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBEducationLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBEducationLevel(BEducationLevel entity);

        [ServiceContractDescription(Name = "修改学历", Desc = "修改学历", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBEducationLevel", Get = "", Post = "BEducationLevel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBEducationLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBEducationLevel(BEducationLevel entity);

        [ServiceContractDescription(Name = "修改学历指定的属性", Desc = "修改学历指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBEducationLevelByField", Get = "", Post = "BEducationLevel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBEducationLevelByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBEducationLevelByField(BEducationLevel entity, string fields);

        [ServiceContractDescription(Name = "删除学历", Desc = "删除学历", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBEducationLevel?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBEducationLevel?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBEducationLevel(long id);

        [ServiceContractDescription(Name = "查询学历", Desc = "查询学历", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBEducationLevel", Get = "", Post = "BEducationLevel", Return = "BaseResultList<BEducationLevel>", ReturnType = "ListBEducationLevel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBEducationLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBEducationLevel(BEducationLevel entity);

        [ServiceContractDescription(Name = "查询学历(HQL)", Desc = "查询学历(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBEducationLevelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BEducationLevel>", ReturnType = "ListBEducationLevel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBEducationLevelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBEducationLevelByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询学历", Desc = "通过主键ID查询学历", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBEducationLevelById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BEducationLevel>", ReturnType = "BEducationLevel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBEducationLevelById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBEducationLevelById(long id, string fields, bool isPlanish);
        #endregion

        #region BMaritalStatus

        [ServiceContractDescription(Name = "新增婚姻状况", Desc = "新增婚姻状况", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBMaritalStatus", Get = "", Post = "BMaritalStatus", Return = "BaseResultDataValue", ReturnType = "BMaritalStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBMaritalStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBMaritalStatus(BMaritalStatus entity);

        [ServiceContractDescription(Name = "修改婚姻状况", Desc = "修改婚姻状况", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBMaritalStatus", Get = "", Post = "BMaritalStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBMaritalStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBMaritalStatus(BMaritalStatus entity);

        [ServiceContractDescription(Name = "修改婚姻状况指定的属性", Desc = "修改婚姻状况指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBMaritalStatusByField", Get = "", Post = "BMaritalStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBMaritalStatusByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBMaritalStatusByField(BMaritalStatus entity, string fields);

        [ServiceContractDescription(Name = "删除婚姻状况", Desc = "删除婚姻状况", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBMaritalStatus?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBMaritalStatus?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBMaritalStatus(long id);

        [ServiceContractDescription(Name = "查询婚姻状况", Desc = "查询婚姻状况", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBMaritalStatus", Get = "", Post = "BMaritalStatus", Return = "BaseResultList<BMaritalStatus>", ReturnType = "ListBMaritalStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBMaritalStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBMaritalStatus(BMaritalStatus entity);

        [ServiceContractDescription(Name = "查询婚姻状况(HQL)", Desc = "查询婚姻状况(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBMaritalStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BMaritalStatus>", ReturnType = "ListBMaritalStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBMaritalStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBMaritalStatusByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询婚姻状况", Desc = "通过主键ID查询婚姻状况", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBMaritalStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BMaritalStatus>", ReturnType = "BMaritalStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBMaritalStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBMaritalStatusById(long id, string fields, bool isPlanish);
        #endregion

        #region BNationality

        [ServiceContractDescription(Name = "新增民族", Desc = "新增民族", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBNationality", Get = "", Post = "BNationality", Return = "BaseResultDataValue", ReturnType = "BNationality")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBNationality", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBNationality(BNationality entity);

        [ServiceContractDescription(Name = "修改民族", Desc = "修改民族", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBNationality", Get = "", Post = "BNationality", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBNationality", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBNationality(BNationality entity);

        [ServiceContractDescription(Name = "修改民族指定的属性", Desc = "修改民族指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBNationalityByField", Get = "", Post = "BNationality", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBNationalityByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBNationalityByField(BNationality entity, string fields);

        [ServiceContractDescription(Name = "删除民族", Desc = "删除民族", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBNationality?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBNationality?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBNationality(long id);

        [ServiceContractDescription(Name = "查询民族", Desc = "查询民族", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBNationality", Get = "", Post = "BNationality", Return = "BaseResultList<BNationality>", ReturnType = "ListBNationality")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNationality", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNationality(BNationality entity);

        [ServiceContractDescription(Name = "查询民族(HQL)", Desc = "查询民族(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBNationalityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BNationality>", ReturnType = "ListBNationality")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNationalityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNationalityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询民族", Desc = "通过主键ID查询民族", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBNationalityById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BNationality>", ReturnType = "BNationality")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBNationalityById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBNationalityById(long id, string fields, bool isPlanish);
        #endregion        

        #region BParameter
        [ServiceContractDescription(Name = "新增参数表(依ParaNo判断是否已存在)", Desc = "新增参数表(依ParaNo判断是否已存在)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBParameterByParaNo", Get = "", Post = "BParameter", Return = "BaseResultDataValue", ReturnType = "BParameter")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBParameterByParaNo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBParameterByParaNo(BParameter entity);

        [ServiceContractDescription(Name = "修改参数表指定的属性(依ParaNo判断是否已存在)", Desc = "修改参数表指定的属性(依ParaNo判断是否已存在)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBParameterByParaNoAndField", Get = "", Post = "BParameter", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBParameterByParaNoAndField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBParameterByParaNoAndField(BParameter entity, string fields);

        [ServiceContractDescription(Name = "新增参数表", Desc = "新增参数表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBParameter", Get = "", Post = "BParameter", Return = "BaseResultDataValue", ReturnType = "BParameter")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBParameter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBParameter(BParameter entity);

        [ServiceContractDescription(Name = "修改参数表", Desc = "修改参数表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBParameter", Get = "", Post = "BParameter", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBParameter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBParameter(BParameter entity);

        [ServiceContractDescription(Name = "修改参数表指定的属性", Desc = "修改参数表指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBParameterByField", Get = "", Post = "BParameter", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBParameterByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBParameterByField(BParameter entity, string fields);

        [ServiceContractDescription(Name = "删除参数表", Desc = "删除参数表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBParameter?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBParameter?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBParameter(long id);

        [ServiceContractDescription(Name = "查询参数表", Desc = "查询参数表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBParameter", Get = "", Post = "BParameter", Return = "BaseResultList<BParameter>", ReturnType = "ListBParameter")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBParameter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBParameter(BParameter entity);

        [ServiceContractDescription(Name = "查询参数表(HQL)", Desc = "查询参数表(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBParameterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BParameter>", ReturnType = "ListBParameter")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBParameterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBParameterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询参数表", Desc = "通过主键ID查询参数表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBParameterById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BParameter>", ReturnType = "BParameter")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBParameterById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBParameterById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过参数编码查询参数表", Desc = "通过参数编码查询参数表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBParameterByByParaNo?paraNo={paraNo}", Get = "paraNo={paraNo}", Post = "", Return = "BParameter", ReturnType = "BParameter")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBParameterByByParaNo?paraNo={paraNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBParameterByByParaNo(string paraNo);

        [ServiceContractDescription(Name = "按分组获取用户设置的系统运行参数集合信息(HQL)", Desc = "按分组获取用户设置的系统运行参数集合信息(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBParameterOfUserSetByHQL?where={where}&sort={sort}", Get = "where={where}&sort={sort}", Post = "", Return = "BaseResultList<BParameter>", ReturnType = "ListBParameter")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBParameterOfUserSetByHQL?where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBParameterOfUserSetByHQL(string where, string sort);

        [ServiceContractDescription(Name = "批量修改用户设置的系统运行参数值", Desc = "批量修改用户设置的系统运行参数值", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBParameterListByBatch", Get = "", Post = "entityList", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBParameterListByBatch", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBParameterListByBatch(IList<BParameter> entityList);

        #endregion               

        #region BProvince

        [ServiceContractDescription(Name = "新增省份", Desc = "新增省份", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBProvince", Get = "", Post = "BProvince", Return = "BaseResultDataValue", ReturnType = "BProvince")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBProvince", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBProvince(BProvince entity);

        [ServiceContractDescription(Name = "修改省份", Desc = "修改省份", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBProvince", Get = "", Post = "BProvince", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProvince", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProvince(BProvince entity);

        [ServiceContractDescription(Name = "修改省份指定的属性", Desc = "修改省份指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBProvinceByField", Get = "", Post = "BProvince", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProvinceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProvinceByField(BProvince entity, string fields);

        [ServiceContractDescription(Name = "删除省份", Desc = "删除省份", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBProvince?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBProvince?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBProvince(long id);

        [ServiceContractDescription(Name = "查询省份", Desc = "查询省份", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBProvince", Get = "", Post = "BProvince", Return = "BaseResultList<BProvince>", ReturnType = "ListBProvince")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProvince", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProvince(BProvince entity);

        [ServiceContractDescription(Name = "查询省份(HQL)", Desc = "查询省份(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBProvinceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProvince>", ReturnType = "ListBProvince")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProvinceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProvinceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询省份", Desc = "通过主键ID查询省份", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBProvinceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProvince>", ReturnType = "BProvince")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProvinceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProvinceById(long id, string fields, bool isPlanish);
        #endregion

        #region BPoliticsStatus

        [ServiceContractDescription(Name = "新增政治面貌", Desc = "新增政治面貌", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBPoliticsStatus", Get = "", Post = "BPoliticsStatus", Return = "BaseResultDataValue", ReturnType = "BPoliticsStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBPoliticsStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBPoliticsStatus(BPoliticsStatus entity);

        [ServiceContractDescription(Name = "修改政治面貌", Desc = "修改政治面貌", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBPoliticsStatus", Get = "", Post = "BPoliticsStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBPoliticsStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBPoliticsStatus(BPoliticsStatus entity);

        [ServiceContractDescription(Name = "修改政治面貌指定的属性", Desc = "修改政治面貌指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBPoliticsStatusByField", Get = "", Post = "BPoliticsStatus", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBPoliticsStatusByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBPoliticsStatusByField(BPoliticsStatus entity, string fields);

        [ServiceContractDescription(Name = "删除政治面貌", Desc = "删除政治面貌", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBPoliticsStatus?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBPoliticsStatus?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBPoliticsStatus(long id);

        [ServiceContractDescription(Name = "查询政治面貌", Desc = "查询政治面貌", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBPoliticsStatus", Get = "", Post = "BPoliticsStatus", Return = "BaseResultList<BPoliticsStatus>", ReturnType = "ListBPoliticsStatus")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPoliticsStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPoliticsStatus(BPoliticsStatus entity);

        [ServiceContractDescription(Name = "查询政治面貌(HQL)", Desc = "查询政治面貌(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBPoliticsStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPoliticsStatus>", ReturnType = "ListBPoliticsStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPoliticsStatusByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPoliticsStatusByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询政治面貌", Desc = "通过主键ID查询政治面貌", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBPoliticsStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPoliticsStatus>", ReturnType = "BPoliticsStatus")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBPoliticsStatusById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBPoliticsStatusById(long id, string fields, bool isPlanish);
        #endregion

        #region BProfessionalAbility

        [ServiceContractDescription(Name = "新增专业级别", Desc = "新增专业级别", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBProfessionalAbility", Get = "", Post = "BProfessionalAbility", Return = "BaseResultDataValue", ReturnType = "BProfessionalAbility")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBProfessionalAbility", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBProfessionalAbility(BProfessionalAbility entity);

        [ServiceContractDescription(Name = "修改专业级别", Desc = "修改专业级别", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBProfessionalAbility", Get = "", Post = "BProfessionalAbility", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProfessionalAbility", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProfessionalAbility(BProfessionalAbility entity);

        [ServiceContractDescription(Name = "修改专业级别指定的属性", Desc = "修改专业级别指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBProfessionalAbilityByField", Get = "", Post = "BProfessionalAbility", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProfessionalAbilityByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProfessionalAbilityByField(BProfessionalAbility entity, string fields);

        [ServiceContractDescription(Name = "删除专业级别", Desc = "删除专业级别", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBProfessionalAbility?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBProfessionalAbility?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBProfessionalAbility(long id);

        [ServiceContractDescription(Name = "查询专业级别", Desc = "查询专业级别", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBProfessionalAbility", Get = "", Post = "BProfessionalAbility", Return = "BaseResultList<BProfessionalAbility>", ReturnType = "ListBProfessionalAbility")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProfessionalAbility", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProfessionalAbility(BProfessionalAbility entity);

        [ServiceContractDescription(Name = "查询专业级别(HQL)", Desc = "查询专业级别(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBProfessionalAbilityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProfessionalAbility>", ReturnType = "ListBProfessionalAbility")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProfessionalAbilityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProfessionalAbilityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询专业级别", Desc = "通过主键ID查询专业级别", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBProfessionalAbilityById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProfessionalAbility>", ReturnType = "BProfessionalAbility")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProfessionalAbilityById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProfessionalAbilityById(long id, string fields, bool isPlanish);
        #endregion

        #region BSex

        [ServiceContractDescription(Name = "新增性别", Desc = "新增性别", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBSex", Get = "", Post = "BSex", Return = "BaseResultDataValue", ReturnType = "BSex")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSex(BSex entity);

        [ServiceContractDescription(Name = "修改性别", Desc = "修改性别", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBSex", Get = "", Post = "BSex", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSex(BSex entity);

        [ServiceContractDescription(Name = "修改性别指定的属性", Desc = "修改性别指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBSexByField", Get = "", Post = "BSex", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSexByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSexByField(BSex entity, string fields);

        [ServiceContractDescription(Name = "删除性别", Desc = "删除性别", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBSex?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSex?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSex(long id);

        [ServiceContractDescription(Name = "查询性别", Desc = "查询性别", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBSex", Get = "", Post = "BSex", Return = "BaseResultList<BSex>", ReturnType = "ListBSex")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSex(BSex entity);

        [ServiceContractDescription(Name = "查询性别(HQL)", Desc = "查询性别(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBSexByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSex>", ReturnType = "ListBSex")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSexByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSexByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询性别", Desc = "通过主键ID查询性别", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBSexById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSex>", ReturnType = "BSex")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSexById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSexById(long id, string fields, bool isPlanish);
        #endregion                   

        #region BObjectOperate

        [ServiceContractDescription(Name = "新增对象操作", Desc = "新增对象操作", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBObjectOperate", Get = "", Post = "BObjectOperate", Return = "BaseResultDataValue", ReturnType = "BObjectOperate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBObjectOperate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBObjectOperate(BObjectOperate entity);

        [ServiceContractDescription(Name = "修改样本操作", Desc = "修改样本操作", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBObjectOperate", Get = "", Post = "BObjectOperate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBObjectOperate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBObjectOperate(BObjectOperate entity);

        [ServiceContractDescription(Name = "修改样本操作指定的属性", Desc = "修改样本操作指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBObjectOperateByField", Get = "", Post = "BObjectOperate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBObjectOperateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBObjectOperateByField(BObjectOperate entity, string fields);

        [ServiceContractDescription(Name = "删除样本操作", Desc = "删除样本操作", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBObjectOperate?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBObjectOperate?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBObjectOperate(long id);

        [ServiceContractDescription(Name = "查询样本操作", Desc = "查询样本操作", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBObjectOperate", Get = "", Post = "BObjectOperate", Return = "BaseResultList<BObjectOperate>", ReturnType = "ListBObjectOperate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBObjectOperate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBObjectOperate(BObjectOperate entity);

        [ServiceContractDescription(Name = "查询样本操作(HQL)", Desc = "查询样本操作(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBObjectOperateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BObjectOperate>", ReturnType = "ListBObjectOperate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBObjectOperateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBObjectOperateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询样本操作", Desc = "通过主键ID查询样本操作", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBObjectOperateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BObjectOperate>", ReturnType = "BObjectOperate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBObjectOperateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBObjectOperateById(long id, string fields, bool isPlanish);
        #endregion

        #region BObjectOperateType

        [ServiceContractDescription(Name = "新增样本操作类型", Desc = "新增样本操作类型", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBObjectOperateType", Get = "", Post = "BObjectOperateType", Return = "BaseResultDataValue", ReturnType = "BObjectOperateType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBObjectOperateType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBObjectOperateType(BObjectOperateType entity);

        [ServiceContractDescription(Name = "修改样本操作类型", Desc = "修改样本操作类型", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBObjectOperateType", Get = "", Post = "BObjectOperateType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBObjectOperateType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBObjectOperateType(BObjectOperateType entity);

        [ServiceContractDescription(Name = "修改样本操作类型指定的属性", Desc = "修改样本操作类型指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBObjectOperateTypeByField", Get = "", Post = "BObjectOperateType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBObjectOperateTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBObjectOperateTypeByField(BObjectOperateType entity, string fields);

        [ServiceContractDescription(Name = "删除样本操作类型", Desc = "删除样本操作类型", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBObjectOperateType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBObjectOperateType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBObjectOperateType(long id);

        [ServiceContractDescription(Name = "查询样本操作类型", Desc = "查询样本操作类型", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBObjectOperateType", Get = "", Post = "BObjectOperateType", Return = "BaseResultList<BObjectOperateType>", ReturnType = "ListBObjectOperateType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBObjectOperateType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBObjectOperateType(BObjectOperateType entity);

        [ServiceContractDescription(Name = "查询样本操作类型(HQL)", Desc = "查询样本操作类型(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBObjectOperateTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BObjectOperateType>", ReturnType = "ListBObjectOperateType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBObjectOperateTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBObjectOperateTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询样本操作类型", Desc = "通过主键ID查询样本操作类型", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBObjectOperateTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BObjectOperateType>", ReturnType = "BObjectOperateType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBObjectOperateTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBObjectOperateTypeById(long id, string fields, bool isPlanish);
        #endregion            

        #region STypeDetail

        [ServiceContractDescription(Name = "新增系统类型明细表", Desc = "新增系统类型明细表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddSTypeDetail", Get = "", Post = "STypeDetail", Return = "BaseResultDataValue", ReturnType = "STypeDetail")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSTypeDetail", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSTypeDetail(STypeDetail entity);

        [ServiceContractDescription(Name = "修改系统类型明细表", Desc = "修改系统类型明细表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateSTypeDetail", Get = "", Post = "STypeDetail", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSTypeDetail", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSTypeDetail(STypeDetail entity);

        [ServiceContractDescription(Name = "修改系统类型明细表指定的属性", Desc = "修改系统类型明细表指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateSTypeDetailByField", Get = "", Post = "STypeDetail", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSTypeDetailByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSTypeDetailByField(STypeDetail entity, string fields);

        [ServiceContractDescription(Name = "删除系统类型明细表", Desc = "删除系统类型明细表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelSTypeDetail?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSTypeDetail?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSTypeDetail(long id);

        [ServiceContractDescription(Name = "查询系统类型明细表", Desc = "查询系统类型明细表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchSTypeDetail", Get = "", Post = "STypeDetail", Return = "BaseResultList<STypeDetail>", ReturnType = "ListSTypeDetail")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSTypeDetail", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSTypeDetail(STypeDetail entity);

        [ServiceContractDescription(Name = "查询系统类型明细表(HQL)", Desc = "查询系统类型明细表(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchSTypeDetailByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<STypeDetail>", ReturnType = "ListSTypeDetail")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSTypeDetailByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSTypeDetailByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询系统类型明细表", Desc = "通过主键ID查询系统类型明细表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchSTypeDetailById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<STypeDetail>", ReturnType = "STypeDetail")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSTypeDetailById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSTypeDetailById(long id, string fields, bool isPlanish);
        #endregion

        #region SType

        [ServiceContractDescription(Name = "新增系统类型表", Desc = "新增系统类型表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddSType", Get = "", Post = "SType", Return = "BaseResultDataValue", ReturnType = "SType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSType(SType entity);

        [ServiceContractDescription(Name = "修改系统类型表", Desc = "修改系统类型表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateSType", Get = "", Post = "SType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSType(SType entity);

        [ServiceContractDescription(Name = "修改系统类型表指定的属性", Desc = "修改系统类型表指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateSTypeByField", Get = "", Post = "SType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSTypeByField(SType entity, string fields);

        [ServiceContractDescription(Name = "删除系统类型表", Desc = "删除系统类型表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelSType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSType(long id);

        [ServiceContractDescription(Name = "查询系统类型表", Desc = "查询系统类型表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchSType", Get = "", Post = "SType", Return = "BaseResultList<SType>", ReturnType = "ListSType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSType(SType entity);

        [ServiceContractDescription(Name = "查询系统类型表(HQL)", Desc = "查询系统类型表(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchSTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SType>", ReturnType = "ListSType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询系统类型表", Desc = "通过主键ID查询系统类型表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchSTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SType>", ReturnType = "SType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSTypeById(long id, string fields, bool isPlanish);
        #endregion             

        #region BDictType

        [ServiceContractDescription(Name = "新增字典类型表", Desc = "新增字典类型表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBDictType", Get = "", Post = "BDictType", Return = "BaseResultDataValue", ReturnType = "BDictType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBDictType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBDictType(BDictType entity);

        [ServiceContractDescription(Name = "修改字典类型表", Desc = "修改字典类型表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBDictType", Get = "", Post = "BDictType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDictType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDictType(BDictType entity);

        [ServiceContractDescription(Name = "修改字典类型表指定的属性", Desc = "修改字典类型表指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBDictTypeByField", Get = "", Post = "BDictType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDictTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDictTypeByField(BDictType entity, string fields);

        [ServiceContractDescription(Name = "删除字典类型表", Desc = "删除字典类型表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBDictType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBDictType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBDictType(long id);

        [ServiceContractDescription(Name = "查询字典类型表", Desc = "查询字典类型表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDictType", Get = "", Post = "BDictType", Return = "BaseResultList<BDictType>", ReturnType = "ListBDictType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDictType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDictType(BDictType entity);

        [ServiceContractDescription(Name = "查询字典类型表(HQL)", Desc = "查询字典类型表(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDictTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDictType>", ReturnType = "ListBDictType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDictTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDictTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询字典类型表", Desc = "通过主键ID查询字典类型表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDictTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDictType>", ReturnType = "BDictType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDictTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDictTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BDict

        [ServiceContractDescription(Name = "新增字典表", Desc = "新增字典表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_AddBDict", Get = "", Post = "BDict", Return = "BaseResultDataValue", ReturnType = "BDict")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBDict(BDict entity);

        [ServiceContractDescription(Name = "修改字典表", Desc = "修改字典表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBDict", Get = "", Post = "BDict", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDict(BDict entity);

        [ServiceContractDescription(Name = "修改字典表指定的属性", Desc = "修改字典表指定的属性", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_UpdateBDictByField", Get = "", Post = "BDict", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDictByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDictByField(BDict entity, string fields);

        [ServiceContractDescription(Name = "删除字典表", Desc = "删除字典表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_DelBDict?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBDict?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBDict(long id);

        [ServiceContractDescription(Name = "查询字典表", Desc = "查询字典表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDict", Get = "", Post = "BDict", Return = "BaseResultList<BDict>", ReturnType = "ListBDict")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDict(BDict entity);

        [ServiceContractDescription(Name = "查询字典表(HQL)", Desc = "查询字典表(HQL)", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDict>", ReturnType = "ListBDict")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDictByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询字典表", Desc = "通过主键ID查询字典表", Url = "ServerWCF/SingleTableService.svc/ST_UDTO_SearchBDictById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDict>", ReturnType = "BDict")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDictById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDictById(long id, string fields, bool isPlanish);
        #endregion

        #region BDictTree

        [ServiceContractDescription(Name = "新增类型树", Desc = "新增类型树", Url = "ServerWCF/SingleTableService.svc/UDTO_AddBDictTree", Get = "", Post = "BDictTree", Return = "BaseResultDataValue", ReturnType = "BDictTree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UDTO_AddBDictTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UDTO_AddBDictTree(BDictTree entity);

        [ServiceContractDescription(Name = "修改类型树", Desc = "修改类型树", Url = "ServerWCF/SingleTableService.svc/UDTO_UpdateBDictTree", Get = "", Post = "BDictTree", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UDTO_UpdateBDictTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool UDTO_UpdateBDictTree(BDictTree entity);

        [ServiceContractDescription(Name = "修改类型树指定的属性", Desc = "修改类型树指定的属性", Url = "ServerWCF/SingleTableService.svc/UDTO_UpdateBDictTreeByField", Get = "", Post = "BDictTree", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UDTO_UpdateBDictTreeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool UDTO_UpdateBDictTreeByField(BDictTree entity, string fields);

        [ServiceContractDescription(Name = "删除类型树", Desc = "删除类型树", Url = "ServerWCF/SingleTableService.svc/UDTO_DelBDictTree?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/UDTO_DelBDictTree?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool UDTO_DelBDictTree(long id);

        [ServiceContractDescription(Name = "查询类型树", Desc = "查询类型树", Url = "ServerWCF/SingleTableService.svc/UDTO_SearchBDictTree", Get = "", Post = "BDictTree", Return = "BaseResultList<BDictTree>", ReturnType = "ListBDictTree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchBDictTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UDTO_SearchBDictTree(BDictTree entity);

        [ServiceContractDescription(Name = "查询类型树(HQL)", Desc = "查询类型树(HQL)", Url = "ServerWCF/SingleTableService.svc/UDTO_SearchBDictTreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDictTree>", ReturnType = "ListBDictTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UDTO_SearchBDictTreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UDTO_SearchBDictTreeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询类型树", Desc = "通过主键ID查询类型树", Url = "ServerWCF/SingleTableService.svc/UDTO_SearchBDictTreeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDictTree>", ReturnType = "BDictTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UDTO_SearchBDictTreeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UDTO_SearchBDictTreeById(long id, string fields, bool isPlanish);

        #region 文件类型树
        [ServiceContractDescription(Name = "根据类型树id(支持传入多个值)及快捷码获取类型树", Desc = "根据类型树id(支持传入多个值)及快捷码获取类型树", Url = "ServerWCF/SingleTableService.svc/UDTO_SearchBDictTreeListTreeByIdListStr?id={id}&idListStr={idListStr}&fields={fields}&maxLevelStr={maxLevelStr}", Get = "id={id}&idListStr={idListStr}&fields={fields}&maxLevelStr={maxLevelStr}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeBBDictTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/UDTO_SearchBDictTreeListTreeByIdListStr?id={id}&idListStr={idListStr}&fields={fields}&maxLevelStr={maxLevelStr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UDTO_SearchBDictTreeListTreeByIdListStr(string id, string idListStr, string fields, string maxLevelStr);
        #endregion
        #endregion

    }
}
