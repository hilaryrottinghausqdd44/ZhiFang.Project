using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ZhiFang.WeiXin.BusinessObject;
using System.ServiceModel.Web;
using ZhiFang.WeiXin.Entity;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.WeiXin.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.WeiXin")]
    public interface IDictionaryService
    {       

        #region BAccountType

        [ServiceContractDescription(Name = "新增应用系统账户类型", Desc = "新增应用系统账户类型", Url = "DictionaryService.svc/ST_UDTO_AddBAccountType", Get = "", Post = "BAccountType", Return = "BaseResultDataValue", ReturnType = "BAccountType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBAccountType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBAccountType(BAccountType entity);

        [ServiceContractDescription(Name = "修改应用系统账户类型", Desc = "修改应用系统账户类型", Url = "DictionaryService.svc/ST_UDTO_UpdateBAccountType", Get = "", Post = "BAccountType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBAccountType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBAccountType(BAccountType entity);

        [ServiceContractDescription(Name = "修改应用系统账户类型指定的属性", Desc = "修改应用系统账户类型指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBAccountTypeByField", Get = "", Post = "BAccountType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBAccountTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBAccountTypeByField(BAccountType entity, string fields);

        [ServiceContractDescription(Name = "删除应用系统账户类型", Desc = "删除应用系统账户类型", Url = "DictionaryService.svc/ST_UDTO_DelBAccountType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBAccountType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBAccountType(long id);

        [ServiceContractDescription(Name = "查询应用系统账户类型", Desc = "查询应用系统账户类型", Url = "DictionaryService.svc/ST_UDTO_SearchBAccountType", Get = "", Post = "BAccountType", Return = "BaseResultList<BAccountType>", ReturnType = "ListBAccountType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAccountType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAccountType(BAccountType entity);

        [ServiceContractDescription(Name = "查询应用系统账户类型(HQL)", Desc = "查询应用系统账户类型(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBAccountTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BAccountType>", ReturnType = "ListBAccountType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAccountTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAccountTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询应用系统账户类型", Desc = "通过主键ID查询应用系统账户类型", Url = "DictionaryService.svc/ST_UDTO_SearchBAccountTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BAccountType>", ReturnType = "BAccountType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAccountTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAccountTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BAntiBiotic

        [ServiceContractDescription(Name = "新增B_AntiBiotic", Desc = "新增B_AntiBiotic", Url = "DictionaryService.svc/ST_UDTO_AddBAntiBiotic", Get = "", Post = "BAntiBiotic", Return = "BaseResultDataValue", ReturnType = "BAntiBiotic")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBAntiBiotic", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBAntiBiotic(BAntiBiotic entity);

        [ServiceContractDescription(Name = "修改B_AntiBiotic", Desc = "修改B_AntiBiotic", Url = "DictionaryService.svc/ST_UDTO_UpdateBAntiBiotic", Get = "", Post = "BAntiBiotic", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBAntiBiotic", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBAntiBiotic(BAntiBiotic entity);

        [ServiceContractDescription(Name = "修改B_AntiBiotic指定的属性", Desc = "修改B_AntiBiotic指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBAntiBioticByField", Get = "", Post = "BAntiBiotic", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBAntiBioticByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBAntiBioticByField(BAntiBiotic entity, string fields);

        [ServiceContractDescription(Name = "删除B_AntiBiotic", Desc = "删除B_AntiBiotic", Url = "DictionaryService.svc/ST_UDTO_DelBAntiBiotic?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBAntiBiotic?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBAntiBiotic(long id);

        [ServiceContractDescription(Name = "查询B_AntiBiotic", Desc = "查询B_AntiBiotic", Url = "DictionaryService.svc/ST_UDTO_SearchBAntiBiotic", Get = "", Post = "BAntiBiotic", Return = "BaseResultList<BAntiBiotic>", ReturnType = "ListBAntiBiotic")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAntiBiotic", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAntiBiotic(BAntiBiotic entity);

        [ServiceContractDescription(Name = "查询B_AntiBiotic(HQL)", Desc = "查询B_AntiBiotic(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBAntiBioticByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BAntiBiotic>", ReturnType = "ListBAntiBiotic")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAntiBioticByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAntiBioticByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_AntiBiotic", Desc = "通过主键ID查询B_AntiBiotic", Url = "DictionaryService.svc/ST_UDTO_SearchBAntiBioticById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BAntiBiotic>", ReturnType = "BAntiBiotic")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAntiBioticById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAntiBioticById(long id, string fields, bool isPlanish);
        #endregion

        #region BAntiType

        [ServiceContractDescription(Name = "新增B_AntiType", Desc = "新增B_AntiType", Url = "DictionaryService.svc/ST_UDTO_AddBAntiType", Get = "", Post = "BAntiType", Return = "BaseResultDataValue", ReturnType = "BAntiType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBAntiType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBAntiType(BAntiType entity);

        [ServiceContractDescription(Name = "修改B_AntiType", Desc = "修改B_AntiType", Url = "DictionaryService.svc/ST_UDTO_UpdateBAntiType", Get = "", Post = "BAntiType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBAntiType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBAntiType(BAntiType entity);

        [ServiceContractDescription(Name = "修改B_AntiType指定的属性", Desc = "修改B_AntiType指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBAntiTypeByField", Get = "", Post = "BAntiType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBAntiTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBAntiTypeByField(BAntiType entity, string fields);

        [ServiceContractDescription(Name = "删除B_AntiType", Desc = "删除B_AntiType", Url = "DictionaryService.svc/ST_UDTO_DelBAntiType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBAntiType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBAntiType(long id);

        [ServiceContractDescription(Name = "查询B_AntiType", Desc = "查询B_AntiType", Url = "DictionaryService.svc/ST_UDTO_SearchBAntiType", Get = "", Post = "BAntiType", Return = "BaseResultList<BAntiType>", ReturnType = "ListBAntiType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAntiType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAntiType(BAntiType entity);

        [ServiceContractDescription(Name = "查询B_AntiType(HQL)", Desc = "查询B_AntiType(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBAntiTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BAntiType>", ReturnType = "ListBAntiType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAntiTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAntiTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_AntiType", Desc = "通过主键ID查询B_AntiType", Url = "DictionaryService.svc/ST_UDTO_SearchBAntiTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BAntiType>", ReturnType = "BAntiType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBAntiTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBAntiTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BCity

        [ServiceContractDescription(Name = "新增城市", Desc = "新增城市", Url = "DictionaryService.svc/ST_UDTO_AddBCity", Get = "", Post = "BCity", Return = "BaseResultDataValue", ReturnType = "BCity")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBCity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBCity(BCity entity);

        [ServiceContractDescription(Name = "修改城市", Desc = "修改城市", Url = "DictionaryService.svc/ST_UDTO_UpdateBCity", Get = "", Post = "BCity", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCity(BCity entity);

        [ServiceContractDescription(Name = "修改城市指定的属性", Desc = "修改城市指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBCityByField", Get = "", Post = "BCity", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCityByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCityByField(BCity entity, string fields);

        [ServiceContractDescription(Name = "删除城市", Desc = "删除城市", Url = "DictionaryService.svc/ST_UDTO_DelBCity?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBCity?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBCity(long id);

        [ServiceContractDescription(Name = "查询城市", Desc = "查询城市", Url = "DictionaryService.svc/ST_UDTO_SearchBCity", Get = "", Post = "BCity", Return = "BaseResultList<BCity>", ReturnType = "ListBCity")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCity(BCity entity);

        [ServiceContractDescription(Name = "查询城市(HQL)", Desc = "查询城市(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBCityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCity>", ReturnType = "ListBCity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询城市", Desc = "通过主键ID查询城市", Url = "DictionaryService.svc/ST_UDTO_SearchBCityById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCity>", ReturnType = "BCity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCityById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCityById(long id, string fields, bool isPlanish);
        #endregion

        #region BCountry

        [ServiceContractDescription(Name = "新增B_Country国家", Desc = "新增B_Country国家", Url = "DictionaryService.svc/ST_UDTO_AddBCountry", Get = "", Post = "BCountry", Return = "BaseResultDataValue", ReturnType = "BCountry")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBCountry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBCountry(BCountry entity);

        [ServiceContractDescription(Name = "修改B_Country国家", Desc = "修改B_Country国家", Url = "DictionaryService.svc/ST_UDTO_UpdateBCountry", Get = "", Post = "BCountry", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCountry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCountry(BCountry entity);

        [ServiceContractDescription(Name = "修改B_Country国家指定的属性", Desc = "修改B_Country国家指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBCountryByField", Get = "", Post = "BCountry", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBCountryByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBCountryByField(BCountry entity, string fields);

        [ServiceContractDescription(Name = "删除B_Country国家", Desc = "删除B_Country国家", Url = "DictionaryService.svc/ST_UDTO_DelBCountry?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBCountry?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBCountry(long id);

        [ServiceContractDescription(Name = "查询B_Country国家", Desc = "查询B_Country国家", Url = "DictionaryService.svc/ST_UDTO_SearchBCountry", Get = "", Post = "BCountry", Return = "BaseResultList<BCountry>", ReturnType = "ListBCountry")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCountry", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCountry(BCountry entity);

        [ServiceContractDescription(Name = "查询B_Country国家(HQL)", Desc = "查询B_Country国家(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBCountryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCountry>", ReturnType = "ListBCountry")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCountryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCountryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_Country国家", Desc = "通过主键ID查询B_Country国家", Url = "DictionaryService.svc/ST_UDTO_SearchBCountryById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BCountry>", ReturnType = "BCountry")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBCountryById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBCountryById(long id, string fields, bool isPlanish);
        #endregion        

        #region BHospital

        [ServiceContractDescription(Name = "新增医院字典", Desc = "新增医院字典", Url = "DictionaryService.svc/ST_UDTO_AddBHospital", Get = "", Post = "BHospital", Return = "BaseResultDataValue", ReturnType = "BHospital")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBHospital", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBHospital(BHospital entity);

        [ServiceContractDescription(Name = "修改医院字典", Desc = "修改医院字典", Url = "DictionaryService.svc/ST_UDTO_UpdateBHospital", Get = "", Post = "BHospital", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospital", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospital(BHospital entity);

        [ServiceContractDescription(Name = "修改医院字典指定的属性", Desc = "修改医院字典指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBHospitalByField", Get = "", Post = "BHospital", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalByField(BHospital entity, string fields);

        [ServiceContractDescription(Name = "删除医院字典", Desc = "删除医院字典", Url = "DictionaryService.svc/ST_UDTO_DelBHospital?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBHospital?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBHospital(long id);

        [ServiceContractDescription(Name = "查询医院字典", Desc = "查询医院字典", Url = "DictionaryService.svc/ST_UDTO_SearchBHospital", Get = "", Post = "BHospital", Return = "BaseResultList<BHospital>", ReturnType = "ListBHospital")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospital", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospital(BHospital entity);

        [ServiceContractDescription(Name = "查询医院字典(HQL)", Desc = "查询医院字典(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospital>", ReturnType = "ListBHospital")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医院字典", Desc = "通过主键ID查询医院字典", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospital>", ReturnType = "BHospital")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalById(long id, string fields, bool isPlanish);
        #endregion

        #region BHospitalDept

        [ServiceContractDescription(Name = "新增医院科室", Desc = "新增医院科室", Url = "DictionaryService.svc/ST_UDTO_AddBHospitalDept", Get = "", Post = "BHospitalDept", Return = "BaseResultDataValue", ReturnType = "BHospitalDept")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBHospitalDept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBHospitalDept(BHospitalDept entity);

        [ServiceContractDescription(Name = "修改医院科室", Desc = "修改医院科室", Url = "DictionaryService.svc/ST_UDTO_UpdateBHospitalDept", Get = "", Post = "BHospitalDept", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalDept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalDept(BHospitalDept entity);

        [ServiceContractDescription(Name = "修改医院科室指定的属性", Desc = "修改医院科室指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBHospitalDeptByField", Get = "", Post = "BHospitalDept", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalDeptByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalDeptByField(BHospitalDept entity, string fields);

        [ServiceContractDescription(Name = "删除医院科室", Desc = "删除医院科室", Url = "DictionaryService.svc/ST_UDTO_DelBHospitalDept?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBHospitalDept?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBHospitalDept(long id);

        [ServiceContractDescription(Name = "查询医院科室", Desc = "查询医院科室", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalDept", Get = "", Post = "BHospitalDept", Return = "BaseResultList<BHospitalDept>", ReturnType = "ListBHospitalDept")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalDept", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalDept(BHospitalDept entity);

        [ServiceContractDescription(Name = "查询医院科室(HQL)", Desc = "查询医院科室(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalDeptByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalDept>", ReturnType = "ListBHospitalDept")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalDeptByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalDeptByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医院科室", Desc = "通过主键ID查询医院科室", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalDeptById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalDept>", ReturnType = "BHospitalDept")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalDeptById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalDeptById(long id, string fields, bool isPlanish);
        #endregion

        #region BHospitalLevel

        [ServiceContractDescription(Name = "新增医院级别", Desc = "新增医院级别", Url = "DictionaryService.svc/ST_UDTO_AddBHospitalLevel", Get = "", Post = "BHospitalLevel", Return = "BaseResultDataValue", ReturnType = "BHospitalLevel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBHospitalLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBHospitalLevel(BHospitalLevel entity);

        [ServiceContractDescription(Name = "修改医院级别", Desc = "修改医院级别", Url = "DictionaryService.svc/ST_UDTO_UpdateBHospitalLevel", Get = "", Post = "BHospitalLevel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalLevel(BHospitalLevel entity);

        [ServiceContractDescription(Name = "修改医院级别指定的属性", Desc = "修改医院级别指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBHospitalLevelByField", Get = "", Post = "BHospitalLevel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalLevelByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalLevelByField(BHospitalLevel entity, string fields);

        [ServiceContractDescription(Name = "删除医院级别", Desc = "删除医院级别", Url = "DictionaryService.svc/ST_UDTO_DelBHospitalLevel?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBHospitalLevel?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBHospitalLevel(long id);

        [ServiceContractDescription(Name = "查询医院级别", Desc = "查询医院级别", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalLevel", Get = "", Post = "BHospitalLevel", Return = "BaseResultList<BHospitalLevel>", ReturnType = "ListBHospitalLevel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalLevel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalLevel(BHospitalLevel entity);

        [ServiceContractDescription(Name = "查询医院级别(HQL)", Desc = "查询医院级别(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalLevelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalLevel>", ReturnType = "ListBHospitalLevel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalLevelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalLevelByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医院级别", Desc = "通过主键ID查询医院级别", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalLevelById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalLevel>", ReturnType = "BHospitalLevel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalLevelById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalLevelById(long id, string fields, bool isPlanish);
        #endregion        

        #region BHospitalType

        [ServiceContractDescription(Name = "新增医院分类", Desc = "新增医院分类", Url = "DictionaryService.svc/ST_UDTO_AddBHospitalType", Get = "", Post = "BHospitalType", Return = "BaseResultDataValue", ReturnType = "BHospitalType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBHospitalType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBHospitalType(BHospitalType entity);

        [ServiceContractDescription(Name = "修改医院分类", Desc = "修改医院分类", Url = "DictionaryService.svc/ST_UDTO_UpdateBHospitalType", Get = "", Post = "BHospitalType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalType(BHospitalType entity);

        [ServiceContractDescription(Name = "修改医院分类指定的属性", Desc = "修改医院分类指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBHospitalTypeByField", Get = "", Post = "BHospitalType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalTypeByField(BHospitalType entity, string fields);

        [ServiceContractDescription(Name = "删除医院分类", Desc = "删除医院分类", Url = "DictionaryService.svc/ST_UDTO_DelBHospitalType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBHospitalType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBHospitalType(long id);

        [ServiceContractDescription(Name = "查询医院分类", Desc = "查询医院分类", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalType", Get = "", Post = "BHospitalType", Return = "BaseResultList<BHospitalType>", ReturnType = "ListBHospitalType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalType(BHospitalType entity);

        [ServiceContractDescription(Name = "查询医院分类(HQL)", Desc = "查询医院分类(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalType>", ReturnType = "ListBHospitalType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医院分类", Desc = "通过主键ID查询医院分类", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalType>", ReturnType = "BHospitalType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BIcons

        [ServiceContractDescription(Name = "新增图标头像", Desc = "新增图标头像", Url = "DictionaryService.svc/ST_UDTO_AddBIcons", Get = "", Post = "BIcons", Return = "BaseResultDataValue", ReturnType = "BIcons")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBIcons", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBIcons(BIcons entity);

        [ServiceContractDescription(Name = "修改图标头像", Desc = "修改图标头像", Url = "DictionaryService.svc/ST_UDTO_UpdateBIcons", Get = "", Post = "BIcons", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBIcons", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBIcons(BIcons entity);

        [ServiceContractDescription(Name = "修改图标头像指定的属性", Desc = "修改图标头像指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBIconsByField", Get = "", Post = "BIcons", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBIconsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBIconsByField(BIcons entity, string fields);

        [ServiceContractDescription(Name = "删除图标头像", Desc = "删除图标头像", Url = "DictionaryService.svc/ST_UDTO_DelBIcons?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBIcons?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBIcons(long id);

        [ServiceContractDescription(Name = "查询图标头像", Desc = "查询图标头像", Url = "DictionaryService.svc/ST_UDTO_SearchBIcons", Get = "", Post = "BIcons", Return = "BaseResultList<BIcons>", ReturnType = "ListBIcons")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBIcons", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBIcons(BIcons entity);

        [ServiceContractDescription(Name = "查询图标头像(HQL)", Desc = "查询图标头像(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBIconsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BIcons>", ReturnType = "ListBIcons")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBIconsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBIconsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询图标头像", Desc = "通过主键ID查询图标头像", Url = "DictionaryService.svc/ST_UDTO_SearchBIconsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BIcons>", ReturnType = "BIcons")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBIconsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBIconsById(long id, string fields, bool isPlanish);
        #endregion

        #region BIconsType

        [ServiceContractDescription(Name = "新增图标头像类型", Desc = "新增图标头像类型", Url = "DictionaryService.svc/ST_UDTO_AddBIconsType", Get = "", Post = "BIconsType", Return = "BaseResultDataValue", ReturnType = "BIconsType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBIconsType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBIconsType(BIconsType entity);

        [ServiceContractDescription(Name = "修改图标头像类型", Desc = "修改图标头像类型", Url = "DictionaryService.svc/ST_UDTO_UpdateBIconsType", Get = "", Post = "BIconsType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBIconsType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBIconsType(BIconsType entity);

        [ServiceContractDescription(Name = "修改图标头像类型指定的属性", Desc = "修改图标头像类型指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBIconsTypeByField", Get = "", Post = "BIconsType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBIconsTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBIconsTypeByField(BIconsType entity, string fields);

        [ServiceContractDescription(Name = "删除图标头像类型", Desc = "删除图标头像类型", Url = "DictionaryService.svc/ST_UDTO_DelBIconsType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBIconsType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBIconsType(long id);

        [ServiceContractDescription(Name = "查询图标头像类型", Desc = "查询图标头像类型", Url = "DictionaryService.svc/ST_UDTO_SearchBIconsType", Get = "", Post = "BIconsType", Return = "BaseResultList<BIconsType>", ReturnType = "ListBIconsType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBIconsType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBIconsType(BIconsType entity);

        [ServiceContractDescription(Name = "查询图标头像类型(HQL)", Desc = "查询图标头像类型(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBIconsTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BIconsType>", ReturnType = "ListBIconsType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBIconsTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBIconsTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询图标头像类型", Desc = "通过主键ID查询图标头像类型", Url = "DictionaryService.svc/ST_UDTO_SearchBIconsTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BIconsType>", ReturnType = "BIconsType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBIconsTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBIconsTypeById(long id, string fields, bool isPlanish);
        #endregion        

        #region BMicroItem

        [ServiceContractDescription(Name = "新增B_MicroItem", Desc = "新增B_MicroItem", Url = "DictionaryService.svc/ST_UDTO_AddBMicroItem", Get = "", Post = "BMicroItem", Return = "BaseResultDataValue", ReturnType = "BMicroItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBMicroItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBMicroItem(BMicroItem entity);

        [ServiceContractDescription(Name = "修改B_MicroItem", Desc = "修改B_MicroItem", Url = "DictionaryService.svc/ST_UDTO_UpdateBMicroItem", Get = "", Post = "BMicroItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBMicroItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBMicroItem(BMicroItem entity);

        [ServiceContractDescription(Name = "修改B_MicroItem指定的属性", Desc = "修改B_MicroItem指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBMicroItemByField", Get = "", Post = "BMicroItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBMicroItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBMicroItemByField(BMicroItem entity, string fields);

        [ServiceContractDescription(Name = "删除B_MicroItem", Desc = "删除B_MicroItem", Url = "DictionaryService.svc/ST_UDTO_DelBMicroItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBMicroItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBMicroItem(long id);

        [ServiceContractDescription(Name = "查询B_MicroItem", Desc = "查询B_MicroItem", Url = "DictionaryService.svc/ST_UDTO_SearchBMicroItem", Get = "", Post = "BMicroItem", Return = "BaseResultList<BMicroItem>", ReturnType = "ListBMicroItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBMicroItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBMicroItem(BMicroItem entity);

        [ServiceContractDescription(Name = "查询B_MicroItem(HQL)", Desc = "查询B_MicroItem(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBMicroItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BMicroItem>", ReturnType = "ListBMicroItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBMicroItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBMicroItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_MicroItem", Desc = "通过主键ID查询B_MicroItem", Url = "DictionaryService.svc/ST_UDTO_SearchBMicroItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BMicroItem>", ReturnType = "BMicroItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBMicroItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBMicroItemById(long id, string fields, bool isPlanish);
        #endregion

        #region BProvince

        [ServiceContractDescription(Name = "新增省份", Desc = "新增省份", Url = "DictionaryService.svc/ST_UDTO_AddBProvince", Get = "", Post = "BProvince", Return = "BaseResultDataValue", ReturnType = "BProvince")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBProvince", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBProvince(BProvince entity);

        [ServiceContractDescription(Name = "修改省份", Desc = "修改省份", Url = "DictionaryService.svc/ST_UDTO_UpdateBProvince", Get = "", Post = "BProvince", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProvince", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProvince(BProvince entity);

        [ServiceContractDescription(Name = "修改省份指定的属性", Desc = "修改省份指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBProvinceByField", Get = "", Post = "BProvince", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProvinceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProvinceByField(BProvince entity, string fields);

        [ServiceContractDescription(Name = "删除省份", Desc = "删除省份", Url = "DictionaryService.svc/ST_UDTO_DelBProvince?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBProvince?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBProvince(long id);

        [ServiceContractDescription(Name = "查询省份", Desc = "查询省份", Url = "DictionaryService.svc/ST_UDTO_SearchBProvince", Get = "", Post = "BProvince", Return = "BaseResultList<BProvince>", ReturnType = "ListBProvince")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProvince", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProvince(BProvince entity);

        [ServiceContractDescription(Name = "查询省份(HQL)", Desc = "查询省份(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBProvinceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProvince>", ReturnType = "ListBProvince")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProvinceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProvinceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询省份", Desc = "通过主键ID查询省份", Url = "DictionaryService.svc/ST_UDTO_SearchBProvinceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProvince>", ReturnType = "BProvince")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProvinceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProvinceById(long id, string fields, bool isPlanish);
        #endregion        

        #region BSex

        [ServiceContractDescription(Name = "新增性别", Desc = "新增性别", Url = "DictionaryService.svc/ST_UDTO_AddBSex", Get = "", Post = "BSex", Return = "BaseResultDataValue", ReturnType = "BSex")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSex(BSex entity);

        [ServiceContractDescription(Name = "修改性别", Desc = "修改性别", Url = "DictionaryService.svc/ST_UDTO_UpdateBSex", Get = "", Post = "BSex", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSex(BSex entity);

        [ServiceContractDescription(Name = "修改性别指定的属性", Desc = "修改性别指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBSexByField", Get = "", Post = "BSex", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSexByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSexByField(BSex entity, string fields);

        [ServiceContractDescription(Name = "删除性别", Desc = "删除性别", Url = "DictionaryService.svc/ST_UDTO_DelBSex?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSex?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSex(long id);

        [ServiceContractDescription(Name = "查询性别", Desc = "查询性别", Url = "DictionaryService.svc/ST_UDTO_SearchBSex", Get = "", Post = "BSex", Return = "BaseResultList<BSex>", ReturnType = "ListBSex")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSex", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSex(BSex entity);

        [ServiceContractDescription(Name = "查询性别(HQL)", Desc = "查询性别(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBSexByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSex>", ReturnType = "ListBSex")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSexByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSexByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询性别", Desc = "通过主键ID查询性别", Url = "DictionaryService.svc/ST_UDTO_SearchBSexById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSex>", ReturnType = "BSex")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSexById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSexById(long id, string fields, bool isPlanish);
        #endregion

        #region BSampleType

        [ServiceContractDescription(Name = "新增样本类型", Desc = "新增样本类型", Url = "DictionaryService.svc/ST_UDTO_AddBSampleType", Get = "", Post = "BSampleType", Return = "BaseResultDataValue", ReturnType = "BSampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSampleType(BSampleType entity);

        [ServiceContractDescription(Name = "修改样本类型", Desc = "修改样本类型", Url = "DictionaryService.svc/ST_UDTO_UpdateBSampleType", Get = "", Post = "BSampleType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleType(BSampleType entity);

        [ServiceContractDescription(Name = "修改样本类型指定的属性", Desc = "修改样本类型指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBSampleTypeByField", Get = "", Post = "BSampleType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleTypeByField(BSampleType entity, string fields);

        [ServiceContractDescription(Name = "删除样本类型", Desc = "删除样本类型", Url = "DictionaryService.svc/ST_UDTO_DelBSampleType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSampleType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSampleType(long id);

        [ServiceContractDescription(Name = "查询样本类型", Desc = "查询样本类型", Url = "DictionaryService.svc/ST_UDTO_SearchBSampleType", Get = "", Post = "BSampleType", Return = "BaseResultList<BSampleType>", ReturnType = "ListBSampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleType(BSampleType entity);

        [ServiceContractDescription(Name = "查询样本类型(HQL)", Desc = "查询样本类型(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleType>", ReturnType = "ListBSampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询样本类型", Desc = "通过主键ID查询样本类型", Url = "DictionaryService.svc/ST_UDTO_SearchBSampleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleType>", ReturnType = "BSampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BTestItemClinicalSignificance

        [ServiceContractDescription(Name = "新增项目临床意义", Desc = "新增项目临床意义", Url = "DictionaryService.svc/ST_UDTO_AddBTestItemClinicalSignificance", Get = "", Post = "BTestItemClinicalSignificance", Return = "BaseResultDataValue", ReturnType = "BTestItemClinicalSignificance")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBTestItemClinicalSignificance", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBTestItemClinicalSignificance(BTestItemClinicalSignificance entity);

        [ServiceContractDescription(Name = "修改项目临床意义", Desc = "修改项目临床意义", Url = "DictionaryService.svc/ST_UDTO_UpdateBTestItemClinicalSignificance", Get = "", Post = "BTestItemClinicalSignificance", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBTestItemClinicalSignificance", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBTestItemClinicalSignificance(BTestItemClinicalSignificance entity);

        [ServiceContractDescription(Name = "修改项目临床意义指定的属性", Desc = "修改项目临床意义指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBTestItemClinicalSignificanceByField", Get = "", Post = "BTestItemClinicalSignificance", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBTestItemClinicalSignificanceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBTestItemClinicalSignificanceByField(BTestItemClinicalSignificance entity, string fields);

        [ServiceContractDescription(Name = "删除项目临床意义", Desc = "删除项目临床意义", Url = "DictionaryService.svc/ST_UDTO_DelBTestItemClinicalSignificance?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBTestItemClinicalSignificance?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBTestItemClinicalSignificance(long id);

        [ServiceContractDescription(Name = "查询项目临床意义", Desc = "查询项目临床意义", Url = "DictionaryService.svc/ST_UDTO_SearchBTestItemClinicalSignificance", Get = "", Post = "BTestItemClinicalSignificance", Return = "BaseResultList<BTestItemClinicalSignificance>", ReturnType = "ListBTestItemClinicalSignificance")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTestItemClinicalSignificance", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTestItemClinicalSignificance(BTestItemClinicalSignificance entity);

        [ServiceContractDescription(Name = "查询项目临床意义(HQL)", Desc = "查询项目临床意义(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBTestItemClinicalSignificanceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTestItemClinicalSignificance>", ReturnType = "ListBTestItemClinicalSignificance")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTestItemClinicalSignificanceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTestItemClinicalSignificanceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询项目临床意义", Desc = "通过主键ID查询项目临床意义", Url = "DictionaryService.svc/ST_UDTO_SearchBTestItemClinicalSignificanceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTestItemClinicalSignificance>", ReturnType = "BTestItemClinicalSignificance")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBTestItemClinicalSignificanceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBTestItemClinicalSignificanceById(long id, string fields, bool isPlanish);
        #endregion

        #region BSpecialty

        [ServiceContractDescription(Name = "新增专业表", Desc = "新增专业表", Url = "DictionaryService.svc/ST_UDTO_AddBSpecialty", Get = "", Post = "BSpecialty", Return = "BaseResultDataValue", ReturnType = "BSpecialty")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSpecialty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSpecialty(BSpecialty entity);

        [ServiceContractDescription(Name = "修改专业表", Desc = "修改专业表", Url = "DictionaryService.svc/ST_UDTO_UpdateBSpecialty", Get = "", Post = "BSpecialty", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSpecialty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSpecialty(BSpecialty entity);

        [ServiceContractDescription(Name = "修改专业表指定的属性", Desc = "修改专业表指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBSpecialtyByField", Get = "", Post = "BSpecialty", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSpecialtyByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSpecialtyByField(BSpecialty entity, string fields);

        [ServiceContractDescription(Name = "删除专业表", Desc = "删除专业表", Url = "DictionaryService.svc/ST_UDTO_DelBSpecialty?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSpecialty?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSpecialty(long id);

        [ServiceContractDescription(Name = "查询专业表", Desc = "查询专业表", Url = "DictionaryService.svc/ST_UDTO_SearchBSpecialty", Get = "", Post = "BSpecialty", Return = "BaseResultList<BSpecialty>", ReturnType = "ListBSpecialty")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSpecialty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSpecialty(BSpecialty entity);

        [ServiceContractDescription(Name = "查询专业表(HQL)", Desc = "查询专业表(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBSpecialtyByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSpecialty>", ReturnType = "ListBSpecialty")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSpecialtyByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSpecialtyByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询专业表", Desc = "通过主键ID查询专业表", Url = "DictionaryService.svc/ST_UDTO_SearchBSpecialtyById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSpecialty>", ReturnType = "BSpecialty")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSpecialtyById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSpecialtyById(long id, string fields, bool isPlanish);
        #endregion

        #region BWeiXinAccount

        [ServiceContractDescription(Name = "新增微信账户", Desc = "新增微信账户", Url = "DictionaryService.svc/ST_UDTO_AddBWeiXinAccount", Get = "", Post = "BWeiXinAccount", Return = "BaseResultDataValue", ReturnType = "BWeiXinAccount")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBWeiXinAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBWeiXinAccount(BWeiXinAccount entity);

        [ServiceContractDescription(Name = "修改微信账户", Desc = "修改微信账户", Url = "DictionaryService.svc/ST_UDTO_UpdateBWeiXinAccount", Get = "", Post = "BWeiXinAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBWeiXinAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBWeiXinAccount(BWeiXinAccount entity);

        [ServiceContractDescription(Name = "修改微信账户指定的属性", Desc = "修改微信账户指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBWeiXinAccountByField", Get = "", Post = "BWeiXinAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBWeiXinAccountByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBWeiXinAccountByField(BWeiXinAccount entity, string fields);

        [ServiceContractDescription(Name = "删除微信账户", Desc = "删除微信账户", Url = "DictionaryService.svc/ST_UDTO_DelBWeiXinAccount?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBWeiXinAccount?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBWeiXinAccount(long id);

        [ServiceContractDescription(Name = "查询微信账户", Desc = "查询微信账户", Url = "DictionaryService.svc/ST_UDTO_SearchBWeiXinAccount", Get = "", Post = "BWeiXinAccount", Return = "BaseResultList<BWeiXinAccount>", ReturnType = "ListBWeiXinAccount")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinAccount(BWeiXinAccount entity);

        [ServiceContractDescription(Name = "查询微信账户(HQL)", Desc = "查询微信账户(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBWeiXinAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BWeiXinAccount>", ReturnType = "ListBWeiXinAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询微信账户", Desc = "通过主键ID查询微信账户", Url = "DictionaryService.svc/ST_UDTO_SearchBWeiXinAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BWeiXinAccount>", ReturnType = "BWeiXinAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinAccountById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "批量获取用户图片", Desc = "通过主键ID批量获取用户图片", Url = "DictionaryService.svc/GetUserIcon", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetUserIcon", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool GetUserIcon();

        #endregion

        #region BWeiXinUserGroup

        [ServiceContractDescription(Name = "新增微信用户组", Desc = "新增微信用户组", Url = "DictionaryService.svc/ST_UDTO_AddBWeiXinUserGroup", Get = "", Post = "BWeiXinUserGroup", Return = "BaseResultDataValue", ReturnType = "BWeiXinUserGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBWeiXinUserGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBWeiXinUserGroup(BWeiXinUserGroup entity);

        [ServiceContractDescription(Name = "修改微信用户组", Desc = "修改微信用户组", Url = "DictionaryService.svc/ST_UDTO_UpdateBWeiXinUserGroup", Get = "", Post = "BWeiXinUserGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBWeiXinUserGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBWeiXinUserGroup(BWeiXinUserGroup entity);

        [ServiceContractDescription(Name = "修改微信用户组指定的属性", Desc = "修改微信用户组指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBWeiXinUserGroupByField", Get = "", Post = "BWeiXinUserGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBWeiXinUserGroupByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBWeiXinUserGroupByField(BWeiXinUserGroup entity, string fields);

        [ServiceContractDescription(Name = "删除微信用户组", Desc = "删除微信用户组", Url = "DictionaryService.svc/ST_UDTO_DelBWeiXinUserGroup?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBWeiXinUserGroup?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBWeiXinUserGroup(long id);

        [ServiceContractDescription(Name = "查询微信用户组", Desc = "查询微信用户组", Url = "DictionaryService.svc/ST_UDTO_SearchBWeiXinUserGroup", Get = "", Post = "BWeiXinUserGroup", Return = "BaseResultList<BWeiXinUserGroup>", ReturnType = "ListBWeiXinUserGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinUserGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinUserGroup(BWeiXinUserGroup entity);

        [ServiceContractDescription(Name = "查询微信用户组(HQL)", Desc = "查询微信用户组(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBWeiXinUserGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BWeiXinUserGroup>", ReturnType = "ListBWeiXinUserGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinUserGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinUserGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询微信用户组", Desc = "通过主键ID查询微信用户组", Url = "DictionaryService.svc/ST_UDTO_SearchBWeiXinUserGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BWeiXinUserGroup>", ReturnType = "BWeiXinUserGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinUserGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinUserGroupById(long id, string fields, bool isPlanish);
        #endregion        

        #region SLog

        [ServiceContractDescription(Name = "新增系统日志", Desc = "新增系统日志", Url = "DictionaryService.svc/ST_UDTO_AddSLog", Get = "", Post = "SLog", Return = "BaseResultDataValue", ReturnType = "SLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSLog(SLog entity);

        [ServiceContractDescription(Name = "修改系统日志", Desc = "修改系统日志", Url = "DictionaryService.svc/ST_UDTO_UpdateSLog", Get = "", Post = "SLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSLog(SLog entity);

        [ServiceContractDescription(Name = "修改系统日志指定的属性", Desc = "修改系统日志指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateSLogByField", Get = "", Post = "SLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSLogByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSLogByField(SLog entity, string fields);

        [ServiceContractDescription(Name = "删除系统日志", Desc = "删除系统日志", Url = "DictionaryService.svc/ST_UDTO_DelSLog?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSLog?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSLog(long id);

        [ServiceContractDescription(Name = "查询系统日志", Desc = "查询系统日志", Url = "DictionaryService.svc/ST_UDTO_SearchSLog", Get = "", Post = "SLog", Return = "BaseResultList<SLog>", ReturnType = "ListSLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSLog(SLog entity);

        [ServiceContractDescription(Name = "查询系统日志(HQL)", Desc = "查询系统日志(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchSLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SLog>", ReturnType = "ListSLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询系统日志", Desc = "通过主键ID查询系统日志", Url = "DictionaryService.svc/ST_UDTO_SearchSLogById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SLog>", ReturnType = "SLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSLogById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSLogById(long id, string fields, bool isPlanish);
        #endregion

        #region SWeiXinAccountLog

        [ServiceContractDescription(Name = "新增S_WeiXinAccountLog", Desc = "新增S_WeiXinAccountLog", Url = "DictionaryService.svc/ST_UDTO_AddSWeiXinAccountLog", Get = "", Post = "SWeiXinAccountLog", Return = "BaseResultDataValue", ReturnType = "SWeiXinAccountLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSWeiXinAccountLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSWeiXinAccountLog(SWeiXinAccountLog entity);

        [ServiceContractDescription(Name = "修改S_WeiXinAccountLog", Desc = "修改S_WeiXinAccountLog", Url = "DictionaryService.svc/ST_UDTO_UpdateSWeiXinAccountLog", Get = "", Post = "SWeiXinAccountLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSWeiXinAccountLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSWeiXinAccountLog(SWeiXinAccountLog entity);

        [ServiceContractDescription(Name = "修改S_WeiXinAccountLog指定的属性", Desc = "修改S_WeiXinAccountLog指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateSWeiXinAccountLogByField", Get = "", Post = "SWeiXinAccountLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSWeiXinAccountLogByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSWeiXinAccountLogByField(SWeiXinAccountLog entity, string fields);

        [ServiceContractDescription(Name = "删除S_WeiXinAccountLog", Desc = "删除S_WeiXinAccountLog", Url = "DictionaryService.svc/ST_UDTO_DelSWeiXinAccountLog?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSWeiXinAccountLog?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSWeiXinAccountLog(long id);

        [ServiceContractDescription(Name = "查询S_WeiXinAccountLog", Desc = "查询S_WeiXinAccountLog", Url = "DictionaryService.svc/ST_UDTO_SearchSWeiXinAccountLog", Get = "", Post = "SWeiXinAccountLog", Return = "BaseResultList<SWeiXinAccountLog>", ReturnType = "ListSWeiXinAccountLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSWeiXinAccountLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSWeiXinAccountLog(SWeiXinAccountLog entity);

        [ServiceContractDescription(Name = "查询S_WeiXinAccountLog(HQL)", Desc = "查询S_WeiXinAccountLog(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchSWeiXinAccountLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SWeiXinAccountLog>", ReturnType = "ListSWeiXinAccountLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSWeiXinAccountLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSWeiXinAccountLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询S_WeiXinAccountLog", Desc = "通过主键ID查询S_WeiXinAccountLog", Url = "DictionaryService.svc/ST_UDTO_SearchSWeiXinAccountLogById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SWeiXinAccountLog>", ReturnType = "SWeiXinAccountLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSWeiXinAccountLogById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSWeiXinAccountLogById(long id, string fields, bool isPlanish);
        #endregion

        #region BHospitalSearch

        [ServiceContractDescription(Name = "新增医院查询条件字典", Desc = "新增医院查询条件字典", Url = "DictionaryService.svc/ST_UDTO_AddBHospitalSearch", Get = "", Post = "BHospitalSearch", Return = "BaseResultDataValue", ReturnType = "BHospitalSearch")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBHospitalSearch", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBHospitalSearch(BHospitalSearch entity);

        [ServiceContractDescription(Name = "修改医院查询条件字典", Desc = "修改医院查询条件字典", Url = "DictionaryService.svc/ST_UDTO_UpdateBHospitalSearch", Get = "", Post = "BHospitalSearch", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalSearch", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalSearch(BHospitalSearch entity);

        [ServiceContractDescription(Name = "修改医院查询条件字典指定的属性", Desc = "修改医院查询条件字典指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBHospitalSearchByField", Get = "", Post = "BHospitalSearch", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBHospitalSearchByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBHospitalSearchByField(BHospitalSearch entity, string fields);

        [ServiceContractDescription(Name = "删除医院查询条件字典", Desc = "删除医院查询条件字典", Url = "DictionaryService.svc/ST_UDTO_DelBHospitalSearch?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBHospitalSearch?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBHospitalSearch(long id);

        [ServiceContractDescription(Name = "查询医院查询条件字典", Desc = "查询医院查询条件字典", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalSearch", Get = "", Post = "BHospitalSearch", Return = "BaseResultList<BHospitalSearch>", ReturnType = "ListBHospitalSearch")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalSearch", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalSearch(BHospitalSearch entity);

        [ServiceContractDescription(Name = "查询医院查询条件字典(HQL)", Desc = "查询医院查询条件字典(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalSearchByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalSearch>", ReturnType = "ListBHospitalSearch")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalSearchByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalSearchByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医院查询条件字典", Desc = "通过主键ID查询医院查询条件字典", Url = "DictionaryService.svc/ST_UDTO_SearchBHospitalSearchById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHospitalSearch>", ReturnType = "BHospitalSearch")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBHospitalSearchById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBHospitalSearchById(long id, string fields, bool isPlanish);
        #endregion


        #region OSItemProductClassTree

        [ServiceContractDescription(Name = "新增检测项目产品分类树", Desc = "新增检测项目产品分类树", Url = "DictionaryService.svc/ST_UDTO_AddOSItemProductClassTree", Get = "", Post = "OSItemProductClassTree", Return = "BaseResultDataValue", ReturnType = "OSItemProductClassTree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSItemProductClassTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSItemProductClassTree(OSItemProductClassTree entity);

        [ServiceContractDescription(Name = "修改检测项目产品分类树", Desc = "修改检测项目产品分类树", Url = "DictionaryService.svc/ST_UDTO_UpdateOSItemProductClassTree", Get = "", Post = "OSItemProductClassTree", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSItemProductClassTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSItemProductClassTree(OSItemProductClassTree entity);

        [ServiceContractDescription(Name = "修改检测项目产品分类树指定的属性", Desc = "修改检测项目产品分类树指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateOSItemProductClassTreeByField", Get = "", Post = "OSItemProductClassTree", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSItemProductClassTreeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSItemProductClassTreeByField(OSItemProductClassTree entity, string fields);

        [ServiceContractDescription(Name = "删除检测项目产品分类树", Desc = "删除检测项目产品分类树", Url = "DictionaryService.svc/ST_UDTO_DelOSItemProductClassTree?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSItemProductClassTree?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelOSItemProductClassTree(long id);

        [ServiceContractDescription(Name = "查询检测项目产品分类树", Desc = "查询检测项目产品分类树", Url = "DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTree", Get = "", Post = "OSItemProductClassTree", Return = "BaseResultList<OSItemProductClassTree>", ReturnType = "ListOSItemProductClassTree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTree(OSItemProductClassTree entity);

        [ServiceContractDescription(Name = "查询检测项目产品分类树(HQL)", Desc = "查询检测项目产品分类树(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSItemProductClassTree>", ReturnType = "ListOSItemProductClassTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询检测项目产品分类树及其所有子孙节点树信息(HQL)", Desc = "查询检测项目产品分类树及其所有子孙节点树信息(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeAndChildTreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isSearchChild={isSearchChild}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isSearchChild={isSearchChild}", Post = "", Return = "BaseResultList<OSItemProductClassTree>", ReturnType = "ListOSItemProductClassTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTreeAndChildTreeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isSearchChild={isSearchChild}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeAndChildTreeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isSearchChild);

        [ServiceContractDescription(Name = "通过主键ID查询检测项目产品分类树", Desc = "通过主键ID查询检测项目产品分类树", Url = "DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSItemProductClassTree>", ReturnType = "OSItemProductClassTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTreeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过区域Id查询检测项目产品分类树", Desc = "通过区域Id查询检测项目产品分类树", Url = "DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeByAreaID?id={id}&areaID={areaID}&fields={fields}", Get = "id={id}&areaID={areaID}&fields={fields}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeOSItemProductClassTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTreeByAreaID?id={id}&areaID={areaID}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeByAreaID(string id, string areaID, string fields);

        #endregion

        #region OSItemProductClassTreeLink

        [ServiceContractDescription(Name = "新增检测项目产品分类树关系", Desc = "新增检测项目产品分类树关系", Url = "DictionaryService.svc/ST_UDTO_AddOSItemProductClassTreeLink", Get = "", Post = "OSItemProductClassTreeLink", Return = "BaseResultDataValue", ReturnType = "OSItemProductClassTreeLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddOSItemProductClassTreeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddOSItemProductClassTreeLink(OSItemProductClassTreeLink entity);

        [ServiceContractDescription(Name = "修改检测项目产品分类树关系", Desc = "修改检测项目产品分类树关系", Url = "DictionaryService.svc/ST_UDTO_UpdateOSItemProductClassTreeLink", Get = "", Post = "OSItemProductClassTreeLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSItemProductClassTreeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSItemProductClassTreeLink(OSItemProductClassTreeLink entity);

        [ServiceContractDescription(Name = "修改检测项目产品分类树关系指定的属性", Desc = "修改检测项目产品分类树关系指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateOSItemProductClassTreeLinkByField", Get = "", Post = "OSItemProductClassTreeLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateOSItemProductClassTreeLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateOSItemProductClassTreeLinkByField(OSItemProductClassTreeLink entity, string fields);

        [ServiceContractDescription(Name = "删除检测项目产品分类树关系", Desc = "删除检测项目产品分类树关系", Url = "DictionaryService.svc/ST_UDTO_DelOSItemProductClassTreeLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelOSItemProductClassTreeLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelOSItemProductClassTreeLink(long id);

        [ServiceContractDescription(Name = "查询检测项目产品分类树关系", Desc = "查询检测项目产品分类树关系", Url = "DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeLink", Get = "", Post = "OSItemProductClassTreeLink", Return = "BaseResultList<OSItemProductClassTreeLink>", ReturnType = "ListOSItemProductClassTreeLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTreeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeLink(OSItemProductClassTreeLink entity);

        [ServiceContractDescription(Name = "查询检测项目产品分类树关系(HQL)", Desc = "查询检测项目产品分类树关系(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSItemProductClassTreeLink>", ReturnType = "ListOSItemProductClassTreeLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTreeLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "依树节点Id查询检测项目产品分类树关系", Desc = "依树节点Id查询检测项目产品分类树关系", Url = "DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeLinkByTreeId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&areaId={areaId}&treeId={treeId}&isSearchChild={isSearchChild}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&areaId={areaId}&treeId={treeId}&isSearchChild={isSearchChild}", Post = "", Return = "BaseResultList<OSItemProductClassTreeLinkVO>", ReturnType = "ListOSItemProductClassTreeLinkVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTreeLinkByTreeId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&areaId={areaId}&treeId={treeId}&isSearchChild={isSearchChild}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeLinkByTreeId(int page, int limit, string fields, string where, string sort, bool isPlanish, string areaId, string treeId, bool isSearchChild);

        [ServiceContractDescription(Name = "通过主键ID查询检测项目产品分类树关系", Desc = "通过主键ID查询检测项目产品分类树关系", Url = "DictionaryService.svc/ST_UDTO_SearchOSItemProductClassTreeLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<OSItemProductClassTreeLink>", ReturnType = "OSItemProductClassTreeLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchOSItemProductClassTreeLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchOSItemProductClassTreeLinkById(long id, string fields, bool isPlanish);
        #endregion


        #region ItemAllItem

        [ServiceContractDescription(Name = "新增所有项目", Desc = "新增所有项目", Url = "DictionaryService.svc/ST_UDTO_AddItemAllItem", Get = "", Post = "ItemAllItem", Return = "BaseResultDataValue", ReturnType = "OSItemProductClassTree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddItemAllItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddItemAllItem(TestItem entity);


        [ServiceContractDescription(Name = "新增中心项目VO", Desc = "新增所有项目VO", Url = "DictionaryService.svc/ST_UDTO_AddItemAllItemVO", Get = "", Post = "ItemAllItem", Return = "BaseResultDataValue", ReturnType = "OSItemProductClassTree")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddItemAllItemVO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddItemAllItemVO(ZhiFang.WeiXin.Entity.ViewObject.Request.TestItemVO entity);

        [ServiceContractDescription(Name = "修改所有项目", Desc = "修改所有项目", Url = "DictionaryService.svc/ST_UDTO_UpdateItemAllItem", Get = "", Post = "ItemAllItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateItemAllItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateItemAllItem(TestItem entity);

        [ServiceContractDescription(Name = "修改所有项目指定的属性", Desc = "修改所有项目指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateItemAllItemByField", Get = "", Post = "ItemAllItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateItemAllItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateItemAllItemByField(TestItem entity, string fields);

        [ServiceContractDescription(Name = "修改中心项目指定的属性VO", Desc = "修改所有项目指定的属性VO", Url = "DictionaryService.svc/ST_UDTO_UpdateItemAllItemByFieldVO", Get = "", Post = "ItemAllItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateItemAllItemByFieldVO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateItemAllItemByFieldVO(ZhiFang.WeiXin.Entity.ViewObject.Request.TestItemVO entity, string fields);

        [ServiceContractDescription(Name = "删除所有项目", Desc = "删除所有项目", Url = "DictionaryService.svc/ST_UDTO_DelItemAllItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelItemAllItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelItemAllItem(long id);

        [ServiceContractDescription(Name = "查询所有项目", Desc = "查询所有项目", Url = "DictionaryService.svc/ST_UDTO_SearchItemAllItem", Get = "", Post = "ItemAllItem", Return = "BaseResultList<ItemAllItem>", ReturnType = "ListItemAllItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemAllItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemAllItem(TestItem entity);

        [ServiceContractDescription(Name = "查询所有项目(HQL)", Desc = "查询所有项目(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchItemAllItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemAllItem>", ReturnType = "ListItemAllItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemAllItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemAllItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询所有项目", Desc = "通过主键ID查询所有项目", Url = "DictionaryService.svc/ST_UDTO_SearchItemAllItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemAllItem>", ReturnType = "ItemAllItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchItemAllItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchItemAllItemById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "中心项目字典复制", Desc = "中心项目字典复制", Url = "DictionaryService.svc/ST_UDTO_TestItemCopy", Get = "", Post = "BLabTestItem", Return = "BaseResultList<BLabTestItem>", ReturnType = "ListBLabTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_TestItemCopy", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_TestItemCopy( List<string> LabCodeList, List<string> ItemNoList, bool isall, int OverRideType);

        [ServiceContractDescription(Name = "获取中心组套项目的项目明细信息最小细项", Desc = "获取中心组套项目的项目明细信息最小细项", Url = "ZhiFangWeiXinService.svc/ST_UDTO_SearchGroupItemSubItemByPItemNo?page={page}&limit={limit}&fields={fields}&pitemNo={pitemNo}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&pitemNo={pitemNo}}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabTestItem>", ReturnType = "ListBLabTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGroupItemSubItemByPItemNo?page={page}&limit={limit}&fields={fields}&pitemNo={pitemNo}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGroupItemSubItemByPItemNo(int page, int limit, string fields, string pitemNo,  string sort, bool isPlanish);

        #endregion

        #region ClientEleArea

        [ServiceContractDescription(Name = "新增区域信息", Desc = "新增区域信息", Url = "WeiXinAppService.svc/ST_UDTO_AddClientEleArea", Get = "", Post = "ClientEleArea", Return = "BaseResultDataValue", ReturnType = "ClientEleArea")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddClientEleArea", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddClientEleArea(ClientEleArea entity);

        [ServiceContractDescription(Name = "修改区域信息", Desc = "修改区域信息", Url = "WeiXinAppService.svc/ST_UDTO_UpdateClientEleArea", Get = "", Post = "ClientEleArea", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateClientEleArea", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateClientEleArea(ClientEleArea entity);

        [ServiceContractDescription(Name = "修改区域信息指定的属性", Desc = "修改区域信息指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateClientEleAreaByField", Get = "", Post = "ClientEleArea", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateClientEleAreaByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateClientEleAreaByField(ClientEleArea entity, string fields);

        [ServiceContractDescription(Name = "删除区域信息", Desc = "删除区域信息", Url = "WeiXinAppService.svc/ST_UDTO_DelClientEleArea?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelClientEleArea?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelClientEleArea(long id);

        [ServiceContractDescription(Name = "查询区域信息", Desc = "查询区域信息", Url = "WeiXinAppService.svc/ST_UDTO_SearchClientEleArea", Get = "", Post = "ClientEleArea", Return = "BaseResultList<ClientEleArea>", ReturnType = "ListClientEleArea")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchClientEleArea", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchClientEleArea(ClientEleArea entity);

        [ServiceContractDescription(Name = "查询区域信息(HQL)", Desc = "查询区域信息(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchClientEleAreaByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ClientEleArea>", ReturnType = "ListClientEleArea")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchClientEleAreaByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchClientEleAreaByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询区域信息", Desc = "通过主键ID查询区域信息", Url = "WeiXinAppService.svc/ST_UDTO_SearchClientEleAreaById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ClientEleArea>", ReturnType = "ClientEleArea")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchClientEleAreaById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchClientEleAreaById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询区域和机构(HQL)", Desc = "查询区域和机构(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchClientEleAreaAndCLIENTELEByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ClientEleArea>", ReturnType = "ListClientEleArea")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchClientEleAreaAndCLIENTELEByHQL?id={id}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchClientEleAreaAndCLIENTELEByHQL(int id,int page, int limit, string fields, string where, string sort, bool isPlanish);
        
        #endregion

        #region BProfessionalAbility

        [ServiceContractDescription(Name = "新增专业级别", Desc = "新增专业级别", Url = "DictionaryService.svc/ST_UDTO_AddBProfessionalAbility", Get = "", Post = "BProfessionalAbility", Return = "BaseResultDataValue", ReturnType = "BProfessionalAbility")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBProfessionalAbility", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBProfessionalAbility(BProfessionalAbility entity);

        [ServiceContractDescription(Name = "修改专业级别", Desc = "修改专业级别", Url = "DictionaryService.svc/ST_UDTO_UpdateBProfessionalAbility", Get = "", Post = "BProfessionalAbility", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProfessionalAbility", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProfessionalAbility(BProfessionalAbility entity);

        [ServiceContractDescription(Name = "修改专业级别指定的属性", Desc = "修改专业级别指定的属性", Url = "DictionaryService.svc/ST_UDTO_UpdateBProfessionalAbilityByField", Get = "", Post = "BProfessionalAbility", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBProfessionalAbilityByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBProfessionalAbilityByField(BProfessionalAbility entity, string fields);

        [ServiceContractDescription(Name = "删除专业级别", Desc = "删除专业级别", Url = "DictionaryService.svc/ST_UDTO_DelBProfessionalAbility?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBProfessionalAbility?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBProfessionalAbility(long id);

        [ServiceContractDescription(Name = "查询专业级别", Desc = "查询专业级别", Url = "DictionaryService.svc/ST_UDTO_SearchBProfessionalAbility", Get = "", Post = "BProfessionalAbility", Return = "BaseResultList<BProfessionalAbility>", ReturnType = "ListBProfessionalAbility")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProfessionalAbility", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProfessionalAbility(BProfessionalAbility entity);

        [ServiceContractDescription(Name = "查询专业级别(HQL)", Desc = "查询专业级别(HQL)", Url = "DictionaryService.svc/ST_UDTO_SearchBProfessionalAbilityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProfessionalAbility>", ReturnType = "ListBProfessionalAbility")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProfessionalAbilityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProfessionalAbilityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询专业级别", Desc = "通过主键ID查询专业级别", Url = "DictionaryService.svc/ST_UDTO_SearchBProfessionalAbilityById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BProfessionalAbility>", ReturnType = "BProfessionalAbility")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBProfessionalAbilityById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBProfessionalAbilityById(long id, string fields, bool isPlanish);
        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/teststring?aaa={aaa}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string teststring(string aaa);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/testentity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string testentity(int entity);


        #region SickType

        [ServiceContractDescription(Name = "新增SickType", Desc = "新增SickType", Url = "SingleTableService.svc/ST_UDTO_AddSickType", Get = "", Post = "SickType", Return = "BaseResultDataValue", ReturnType = "SickType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSickType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSickType(SickType entity);

        [ServiceContractDescription(Name = "修改SickType", Desc = "修改SickType", Url = "SingleTableService.svc/ST_UDTO_UpdateSickType", Get = "", Post = "SickType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSickType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSickType(SickType entity);

        [ServiceContractDescription(Name = "修改SickType指定的属性", Desc = "修改SickType指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateSickTypeByField", Get = "", Post = "SickType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSickTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSickTypeByField(SickType entity, string fields);

        [ServiceContractDescription(Name = "删除SickType", Desc = "删除SickType", Url = "SingleTableService.svc/ST_UDTO_DelSickType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSickType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSickType(long id);

        [ServiceContractDescription(Name = "查询SickType", Desc = "查询SickType", Url = "SingleTableService.svc/ST_UDTO_SearchSickType", Get = "", Post = "SickType", Return = "BaseResultList<SickType>", ReturnType = "ListSickType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSickType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSickType(SickType entity);

        [ServiceContractDescription(Name = "查询SickType(HQL)", Desc = "查询SickType(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchSickTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SickType>", ReturnType = "ListSickType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSickTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSickTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询SickType", Desc = "通过主键ID查询SickType", Url = "SingleTableService.svc/ST_UDTO_SearchSickTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SickType>", ReturnType = "SickType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSickTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSickTypeById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "实验室SickType复制", Desc = "查询SickType", Url = "SingleTableService.svc/ST_UDTO_SickTypeCopy", Get = "", Post = "BLabSickType", Return = "BaseResultList<BLabSickType>", ReturnType = "ListBLabSickType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SickTypeCopy", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SickTypeCopy(List<string> LabCodeList, List<string> ItemNoList, bool Isall, int OverRideType);

        #endregion


        #region BLabSickType

        [ServiceContractDescription(Name = "新增B_Lab_SickType", Desc = "新增B_Lab_SickType", Url = "SingleTableService.svc/ST_UDTO_AddBLabSickType", Get = "", Post = "BLabSickType", Return = "BaseResultDataValue", ReturnType = "BLabSickType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBLabSickType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBLabSickType(BLabSickType entity);

        [ServiceContractDescription(Name = "修改B_Lab_SickType", Desc = "修改B_Lab_SickType", Url = "SingleTableService.svc/ST_UDTO_UpdateBLabSickType", Get = "", Post = "BLabSickType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLabSickType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLabSickType(BLabSickType entity);

        [ServiceContractDescription(Name = "修改B_Lab_SickType指定的属性", Desc = "修改B_Lab_SickType指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBLabSickTypeByField", Get = "", Post = "BLabSickType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLabSickTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLabSickTypeByField(BLabSickType entity, string fields);

        [ServiceContractDescription(Name = "删除B_Lab_SickType", Desc = "删除B_Lab_SickType", Url = "SingleTableService.svc/ST_UDTO_DelBLabSickType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBLabSickType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBLabSickType(long id);

        [ServiceContractDescription(Name = "查询B_Lab_SickType", Desc = "查询B_Lab_SickType", Url = "SingleTableService.svc/ST_UDTO_SearchBLabSickType", Get = "", Post = "BLabSickType", Return = "BaseResultList<BLabSickType>", ReturnType = "ListBLabSickType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabSickType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabSickType(BLabSickType entity);

        [ServiceContractDescription(Name = "查询B_Lab_SickType(HQL)", Desc = "查询B_Lab_SickType(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBLabSickTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabSickType>", ReturnType = "ListBLabSickType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabSickTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabSickTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_Lab_SickType", Desc = "通过主键ID查询B_Lab_SickType", Url = "SingleTableService.svc/ST_UDTO_SearchBLabSickTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabSickType>", ReturnType = "BLabSickType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabSickTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabSickTypeById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "实验室B_Lab_SickType复制", Desc = "查询B_Lab_SickType", Url = "SingleTableService.svc/ST_UDTO_BLabSickTypeCopy", Get = "", Post = "BLabSickType", Return = "BaseResultList<BLabSickType>", ReturnType = "ListBLabSickType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_BLabSickTypeCopy", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_BLabSickTypeCopy(string SourceLabCode, List<string> LabCodeList, List<string> ItemNoList, bool Isall, int OverRideType);
        
        [ServiceContractDescription(Name = "查询B_Lab_SickTypeAndControl", Desc = "查询B_Lab_SickType(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBLabSickTypeAndControl?page={page}&limit={limit}&fields={fields}&where={where}&labCode={labCode}&controlType={controlType}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&labCode={labCode}&controlType={controlType}", Post = "", Return = "BaseResultList<BLabSickType>", ReturnType = "ListBLabSickType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabSickTypeAndControl?page={page}&limit={limit}&fields={fields}&where={where}&labCode={labCode}&controlType={controlType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabSickTypeAndControl(int page, int limit, string fields, string where,string labCode,int controlType);
        #endregion

        #region BSickTypeControl

        [ServiceContractDescription(Name = "新增B_SickTypeControl", Desc = "新增B_SickTypeControl", Url = "SingleTableService.svc/ST_UDTO_AddBSickTypeControl", Get = "", Post = "BSickTypeControl", Return = "BaseResultDataValue", ReturnType = "BSickTypeControl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSickTypeControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSickTypeControl(BSickTypeControl entity);

        [ServiceContractDescription(Name = "修改B_SickTypeControl", Desc = "修改B_SickTypeControl", Url = "SingleTableService.svc/ST_UDTO_UpdateBSickTypeControl", Get = "", Post = "BSickTypeControl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSickTypeControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSickTypeControl(BSickTypeControl entity);

        [ServiceContractDescription(Name = "修改B_SickTypeControl指定的属性", Desc = "修改B_SickTypeControl指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBSickTypeControlByField", Get = "", Post = "BSickTypeControl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSickTypeControlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSickTypeControlByField(BSickTypeControl entity, string fields);

        [ServiceContractDescription(Name = "删除B_SickTypeControl", Desc = "删除B_SickTypeControl", Url = "SingleTableService.svc/ST_UDTO_DelBSickTypeControl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSickTypeControl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSickTypeControl(long id);

        [ServiceContractDescription(Name = "查询B_SickTypeControl", Desc = "查询B_SickTypeControl", Url = "SingleTableService.svc/ST_UDTO_SearchBSickTypeControl", Get = "", Post = "BSickTypeControl", Return = "BaseResultList<BSickTypeControl>", ReturnType = "ListBSickTypeControl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSickTypeControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSickTypeControl(BSickTypeControl entity);

        [ServiceContractDescription(Name = "查询B_SickTypeControl(HQL)", Desc = "查询B_SickTypeControl(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBSickTypeControlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSickTypeControl>", ReturnType = "ListBSickTypeControl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSickTypeControlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSickTypeControlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_SickTypeControl", Desc = "通过主键ID查询B_SickTypeControl", Url = "SingleTableService.svc/ST_UDTO_SearchBSickTypeControlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSickTypeControl>", ReturnType = "BSickTypeControl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSickTypeControlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSickTypeControlById(long id, string fields, bool isPlanish);
        #endregion


        #region SampleType

        [ServiceContractDescription(Name = "新增SampleType", Desc = "新增SampleType", Url = "SingleTableService.svc/ST_UDTO_AddSampleType", Get = "", Post = "SampleType", Return = "BaseResultDataValue", ReturnType = "SampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSampleType(SampleType entity);

        [ServiceContractDescription(Name = "修改SampleType", Desc = "修改SampleType", Url = "SingleTableService.svc/ST_UDTO_UpdateSampleType", Get = "", Post = "SampleType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSampleType(SampleType entity);

        [ServiceContractDescription(Name = "修改SampleType指定的属性", Desc = "修改SampleType指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateSampleTypeByField", Get = "", Post = "SampleType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateSampleTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateSampleTypeByField(SampleType entity, string fields);

        [ServiceContractDescription(Name = "删除SampleType", Desc = "删除SampleType", Url = "SingleTableService.svc/ST_UDTO_DelSampleType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelSampleType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelSampleType(long id);

        [ServiceContractDescription(Name = "查询SampleType", Desc = "查询SampleType", Url = "SingleTableService.svc/ST_UDTO_SearchSampleType", Get = "", Post = "SampleType", Return = "BaseResultList<SampleType>", ReturnType = "ListSampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSampleType(SampleType entity);

        [ServiceContractDescription(Name = "查询SampleType(HQL)", Desc = "查询SampleType(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SampleType>", ReturnType = "ListSampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSampleTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询SampleType", Desc = "通过主键ID查询SampleType", Url = "SingleTableService.svc/ST_UDTO_SearchSampleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SampleType>", ReturnType = "SampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchSampleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchSampleTypeById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "新增SampleType", Desc = "新增SampleType", Url = "SingleTableService.svc/ST_UDTO_AddSampleType", Get = "", Post = "SampleType", Return = "BaseResultDataValue", ReturnType = "SampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddSampleTypeCopy", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddSampleTypeCopy(List<string> ItemNoList, List<string> LabCodeList,bool Isall,int OverRideType);
        #endregion


        #region BLabSampleType

        [ServiceContractDescription(Name = "新增B_Lab_SampleType", Desc = "新增B_Lab_SampleType", Url = "SingleTableService.svc/ST_UDTO_AddBLabSampleType", Get = "", Post = "BLabSampleType", Return = "BaseResultDataValue", ReturnType = "BLabSampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBLabSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBLabSampleType(BLabSampleType entity);

        [ServiceContractDescription(Name = "修改B_Lab_SampleType", Desc = "修改B_Lab_SampleType", Url = "SingleTableService.svc/ST_UDTO_UpdateBLabSampleType", Get = "", Post = "BLabSampleType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLabSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLabSampleType(BLabSampleType entity);

        [ServiceContractDescription(Name = "修改B_Lab_SampleType指定的属性", Desc = "修改B_Lab_SampleType指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBLabSampleTypeByField", Get = "", Post = "BLabSampleType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLabSampleTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLabSampleTypeByField(BLabSampleType entity, string fields);

        [ServiceContractDescription(Name = "删除B_Lab_SampleType", Desc = "删除B_Lab_SampleType", Url = "SingleTableService.svc/ST_UDTO_DelBLabSampleType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBLabSampleType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBLabSampleType(long id);

        [ServiceContractDescription(Name = "查询B_Lab_SampleType", Desc = "查询B_Lab_SampleType", Url = "SingleTableService.svc/ST_UDTO_SearchBLabSampleType", Get = "", Post = "BLabSampleType", Return = "BaseResultList<BLabSampleType>", ReturnType = "ListBLabSampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabSampleType(BLabSampleType entity);

        [ServiceContractDescription(Name = "查询B_Lab_SampleType(HQL)", Desc = "查询B_Lab_SampleType(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBLabSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabSampleType>", ReturnType = "ListBLabSampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabSampleTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_Lab_SampleType", Desc = "通过主键ID查询B_Lab_SampleType", Url = "SingleTableService.svc/ST_UDTO_SearchBLabSampleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabSampleType>", ReturnType = "BLabSampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabSampleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabSampleTypeById(long id, string fields, bool isPlanish);


        [ServiceContractDescription(Name = "查询B_Lab_SampleType(HQL)", Desc = "查询B_Lab_SampleType(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBLabSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}", Post = "", Return = "BaseResultList<BLabSampleType>", ReturnType = "ListBLabSampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabSampleTypeByHQLAndControl?page={page}&limit={limit}&fields={fields}&where={where}&labCode={labCode}&controlType={controlType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabSampleTypeByHQLAndControl(int page, int limit, string fields, string where,string labCode, int controlType);

        [ServiceContractDescription(Name = "实验室复制", Desc = "查询B_Lab_SickType", Url = "SingleTableService.svc/ST_UDTO_AddLabSampleTypeCopy", Get = "", Post = "BLabSampleType", Return = "BaseResultList<BLabSampleType>", ReturnType = "ListBLabSampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddLabSampleTypeCopy", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddLabSampleTypeCopy(string originalLabCode, List<string> ItemNoList, List<string> LabCodeList, bool Isall, int OverRideType);
        #endregion

        #region BSampleTypeControl

        [ServiceContractDescription(Name = "新增B_SampleTypeControl", Desc = "新增B_SampleTypeControl", Url = "SingleTableService.svc/ST_UDTO_AddBSampleTypeControl", Get = "", Post = "BSampleTypeControl", Return = "BaseResultDataValue", ReturnType = "BSampleTypeControl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSampleTypeControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSampleTypeControl(BSampleTypeControl entity);

        [ServiceContractDescription(Name = "修改B_SampleTypeControl", Desc = "修改B_SampleTypeControl", Url = "SingleTableService.svc/ST_UDTO_UpdateBSampleTypeControl", Get = "", Post = "BSampleTypeControl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleTypeControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleTypeControl(BSampleTypeControl entity);

        [ServiceContractDescription(Name = "修改B_SampleTypeControl指定的属性", Desc = "修改B_SampleTypeControl指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBSampleTypeControlByField", Get = "", Post = "BSampleTypeControl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSampleTypeControlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSampleTypeControlByField(BSampleTypeControl entity, string fields);

        [ServiceContractDescription(Name = "删除B_SampleTypeControl", Desc = "删除B_SampleTypeControl", Url = "SingleTableService.svc/ST_UDTO_DelBSampleTypeControl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSampleTypeControl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSampleTypeControl(long id);

        [ServiceContractDescription(Name = "查询B_SampleTypeControl", Desc = "查询B_SampleTypeControl", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleTypeControl", Get = "", Post = "BSampleTypeControl", Return = "BaseResultList<BSampleTypeControl>", ReturnType = "ListBSampleTypeControl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleTypeControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleTypeControl(BSampleTypeControl entity);

        [ServiceContractDescription(Name = "查询B_SampleTypeControl(HQL)", Desc = "查询B_SampleTypeControl(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleTypeControlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleTypeControl>", ReturnType = "ListBSampleTypeControl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleTypeControlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleTypeControlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_SampleTypeControl", Desc = "通过主键ID查询B_SampleTypeControl", Url = "SingleTableService.svc/ST_UDTO_SearchBSampleTypeControlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSampleTypeControl>", ReturnType = "BSampleTypeControl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSampleTypeControlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSampleTypeControlById(long id, string fields, bool isPlanish);
        #endregion


        #region BDoctorControl

        [ServiceContractDescription(Name = "新增B_DoctorControl", Desc = "新增B_DoctorControl", Url = "SingleTableService.svc/ST_UDTO_AddBDoctorControl", Get = "", Post = "BDoctorControl", Return = "BaseResultDataValue", ReturnType = "BDoctorControl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBDoctorControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBDoctorControl(BDoctorControl entity);

        [ServiceContractDescription(Name = "修改B_DoctorControl", Desc = "修改B_DoctorControl", Url = "SingleTableService.svc/ST_UDTO_UpdateBDoctorControl", Get = "", Post = "BDoctorControl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDoctorControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDoctorControl(BDoctorControl entity);

        [ServiceContractDescription(Name = "修改B_DoctorControl指定的属性", Desc = "修改B_DoctorControl指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBDoctorControlByField", Get = "", Post = "BDoctorControl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDoctorControlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDoctorControlByField(BDoctorControl entity, string fields);

        [ServiceContractDescription(Name = "删除B_DoctorControl", Desc = "删除B_DoctorControl", Url = "SingleTableService.svc/ST_UDTO_DelBDoctorControl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBDoctorControl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBDoctorControl(long id);

        [ServiceContractDescription(Name = "查询B_DoctorControl", Desc = "查询B_DoctorControl", Url = "SingleTableService.svc/ST_UDTO_SearchBDoctorControl", Get = "", Post = "BDoctorControl", Return = "BaseResultList<BDoctorControl>", ReturnType = "ListBDoctorControl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDoctorControl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDoctorControl(BDoctorControl entity);

        [ServiceContractDescription(Name = "查询B_DoctorControl(HQL)", Desc = "查询B_DoctorControl(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBDoctorControlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDoctorControl>", ReturnType = "ListBDoctorControl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDoctorControlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDoctorControlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_DoctorControl", Desc = "通过主键ID查询B_DoctorControl", Url = "SingleTableService.svc/ST_UDTO_SearchBDoctorControlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDoctorControl>", ReturnType = "BDoctorControl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDoctorControlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDoctorControlById(long id, string fields, bool isPlanish);
        #endregion


        #region BLabDoctor

        [ServiceContractDescription(Name = "新增B_Lab_Doctor", Desc = "新增B_Lab_Doctor", Url = "SingleTableService.svc/ST_UDTO_AddBLabDoctor", Get = "", Post = "BLabDoctor", Return = "BaseResultDataValue", ReturnType = "BLabDoctor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBLabDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBLabDoctor(BLabDoctor entity);

        [ServiceContractDescription(Name = "修改B_Lab_Doctor", Desc = "修改B_Lab_Doctor", Url = "SingleTableService.svc/ST_UDTO_UpdateBLabDoctor", Get = "", Post = "BLabDoctor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLabDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLabDoctor(BLabDoctor entity);

        [ServiceContractDescription(Name = "修改B_Lab_Doctor指定的属性", Desc = "修改B_Lab_Doctor指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateBLabDoctorByField", Get = "", Post = "BLabDoctor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBLabDoctorByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBLabDoctorByField(BLabDoctor entity, string fields);

        [ServiceContractDescription(Name = "删除B_Lab_Doctor", Desc = "删除B_Lab_Doctor", Url = "SingleTableService.svc/ST_UDTO_DelBLabDoctor?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBLabDoctor?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBLabDoctor(long id);

        [ServiceContractDescription(Name = "查询B_Lab_Doctor", Desc = "查询B_Lab_Doctor", Url = "SingleTableService.svc/ST_UDTO_SearchBLabDoctor", Get = "", Post = "BLabDoctor", Return = "BaseResultList<BLabDoctor>", ReturnType = "ListBLabDoctor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabDoctor(BLabDoctor entity);

        [ServiceContractDescription(Name = "查询B_Lab_Doctor(HQL)", Desc = "查询B_Lab_Doctor(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBLabDoctorByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabDoctor>", ReturnType = "ListBLabDoctor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabDoctorByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabDoctorByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_Lab_Doctor", Desc = "通过主键ID查询B_Lab_Doctor", Url = "SingleTableService.svc/ST_UDTO_SearchBLabDoctorById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BLabDoctor>", ReturnType = "BLabDoctor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabDoctorById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabDoctorById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询(HQL)", Desc = "查询B_Lab_SampleType(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchBLabDoctorByHQLAndControl?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}", Post = "", Return = "BaseResultList<BLabSampleType>", ReturnType = "ListBLabSampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBLabDoctorByHQLAndControl?page={page}&limit={limit}&fields={fields}&where={where}&labCode={labCode}&controlType={controlType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBLabDoctorByHQLAndControl(int page, int limit, string fields, string where, string labCode, int controlType);

        [ServiceContractDescription(Name = "删除", Desc = "删除B_Lab_Doctor", Url = "SingleTableService.svc/ST_UDTO_DelBLabDoctorAndControl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBLabDoctorAndControl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBLabDoctorAndControl(long id);

        [ServiceContractDescription(Name = "实验室复制", Desc = "查询B_Lab_SickType", Url = "SingleTableService.svc/ST_UDTO_AddLabDoctorCopy", Get = "", Post = "BLabSampleType", Return = "BaseResultList<BLabSampleType>", ReturnType = "ListBLabSampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddLabDoctorCopy", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddLabDoctorCopy(string originalLabCode, List<string> ItemNoList, List<string> LabCodeList, bool Isall, int OverRideType);

        #endregion


        #region Doctor

        [ServiceContractDescription(Name = "新增Doctor", Desc = "新增Doctor", Url = "SingleTableService.svc/ST_UDTO_AddDoctor", Get = "", Post = "Doctor", Return = "BaseResultDataValue", ReturnType = "Doctor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddDoctor(Doctor entity);

        [ServiceContractDescription(Name = "修改Doctor", Desc = "修改Doctor", Url = "SingleTableService.svc/ST_UDTO_UpdateDoctor", Get = "", Post = "Doctor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateDoctor(Doctor entity);

        [ServiceContractDescription(Name = "修改Doctor指定的属性", Desc = "修改Doctor指定的属性", Url = "SingleTableService.svc/ST_UDTO_UpdateDoctorByField", Get = "", Post = "Doctor", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateDoctorByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateDoctorByField(Doctor entity, string fields);

        [ServiceContractDescription(Name = "删除Doctor", Desc = "删除Doctor", Url = "SingleTableService.svc/ST_UDTO_DelDoctor?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelDoctor?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelDoctor(long id);

        [ServiceContractDescription(Name = "查询Doctor", Desc = "查询Doctor", Url = "SingleTableService.svc/ST_UDTO_SearchDoctor", Get = "", Post = "Doctor", Return = "BaseResultList<Doctor>", ReturnType = "ListDoctor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchDoctor(Doctor entity);

        [ServiceContractDescription(Name = "查询Doctor(HQL)", Desc = "查询Doctor(HQL)", Url = "SingleTableService.svc/ST_UDTO_SearchDoctorByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Doctor>", ReturnType = "ListDoctor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchDoctorByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchDoctorByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Doctor", Desc = "通过主键ID查询Doctor", Url = "SingleTableService.svc/ST_UDTO_SearchDoctorById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Doctor>", ReturnType = "Doctor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchDoctorById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchDoctorById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "新增SampleType", Desc = "新增SampleType", Url = "SingleTableService.svc/ST_UDTO_AddSampleType", Get = "", Post = "SampleType", Return = "BaseResultDataValue", ReturnType = "SampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddDoctorCopy", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddDoctorCopy(List<string> ItemNoList, List<string> LabCodeList, bool Isall, int OverRideType);

        [ServiceContractDescription(Name = "删除Doctor", Desc = "删除Doctor", Url = "SingleTableService.svc/ST_UDTO_DelDoctor?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelDoctorAndControl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelDoctorAndControl(long id);
        #endregion
    }
}
