using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Web;
using System.ServiceModel;
using ZhiFang.WeiXin.Entity;
using ZhiFang.WeiXin.BusinessObject;
using ZhiFang.WeiXin.BusinessObject.LabObject;
using ZhiFang.Entity.Base;

namespace ZhiFang.WeiXin.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.WeiXin")]
    public interface IWeiXinAppService
    {

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

        #region BDoctorAccount

        [ServiceContractDescription(Name = "新增医生账户信息", Desc = "新增医生账户信息", Url = "WeiXinAppService.svc/ST_UDTO_AddBDoctorAccount", Get = "", Post = "BDoctorAccount", Return = "BaseResultDataValue", ReturnType = "BDoctorAccount")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBDoctorAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBDoctorAccount(BDoctorAccount entity);

        [ServiceContractDescription(Name = "修改医生账户信息", Desc = "修改医生账户信息", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBDoctorAccount", Get = "", Post = "BDoctorAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDoctorAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDoctorAccount(BDoctorAccount entity);

        [ServiceContractDescription(Name = "修改医生账户信息指定的属性", Desc = "修改医生账户信息指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBDoctorAccountByField", Get = "", Post = "BDoctorAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBDoctorAccountByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBDoctorAccountByField(BDoctorAccount entity, string fields);

        [ServiceContractDescription(Name = "删除医生账户信息", Desc = "删除医生账户信息", Url = "WeiXinAppService.svc/ST_UDTO_DelBDoctorAccount?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBDoctorAccount?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBDoctorAccount(long id);

        [ServiceContractDescription(Name = "查询医生账户信息", Desc = "查询医生账户信息", Url = "WeiXinAppService.svc/ST_UDTO_SearchBDoctorAccount", Get = "", Post = "BDoctorAccount", Return = "BaseResultList<BDoctorAccount>", ReturnType = "ListBDoctorAccount")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDoctorAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDoctorAccount(BDoctorAccount entity);

        [ServiceContractDescription(Name = "查询医生账户信息(HQL)", Desc = "查询医生账户信息(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchBDoctorAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDoctorAccount>", ReturnType = "ListBDoctorAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDoctorAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDoctorAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询医生账户信息", Desc = "通过主键ID查询医生账户信息", Url = "WeiXinAppService.svc/ST_UDTO_SearchBDoctorAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BDoctorAccount>", ReturnType = "BDoctorAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBDoctorAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBDoctorAccountById(long id, string fields, bool isPlanish);
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

        #region BSearchAccount

        //[ServiceContractDescription(Name = "新增查询子账户", Desc = "新增查询子账户", Url = "WeiXinAppService.svc/ST_UDTO_AddBSearchAccount", Get = "", Post = "BSearchAccount", Return = "BaseResultDataValue", ReturnType = "BSearchAccount")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSearchAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_AddBSearchAccount(BSearchAccount entity);

        [ServiceContractDescription(Name = "修改查询子账户", Desc = "修改查询子账户", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBSearchAccount", Get = "", Post = "BSearchAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSearchAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSearchAccount(BSearchAccount entity);

        [ServiceContractDescription(Name = "修改查询子账户指定的属性", Desc = "修改查询子账户指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBSearchAccountByField", Get = "", Post = "BSearchAccount", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSearchAccountByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBSearchAccountByField(BSearchAccount entity, string fields);

        [ServiceContractDescription(Name = "删除查询子账户", Desc = "删除查询子账户", Url = "WeiXinAppService.svc/ST_UDTO_DelBSearchAccount?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBSearchAccount?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBSearchAccount(long id);

        [ServiceContractDescription(Name = "查询查询子账户", Desc = "查询查询子账户", Url = "WeiXinAppService.svc/ST_UDTO_SearchBSearchAccount", Get = "", Post = "BSearchAccount", Return = "BaseResultList<BSearchAccount>", ReturnType = "ListBSearchAccount")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSearchAccount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSearchAccount(BSearchAccount entity);

        [ServiceContractDescription(Name = "查询查询子账户(HQL)", Desc = "查询查询子账户(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchBSearchAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSearchAccount>", ReturnType = "ListBSearchAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSearchAccountByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSearchAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询查询子账户", Desc = "通过主键ID查询查询子账户", Url = "WeiXinAppService.svc/ST_UDTO_SearchBSearchAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BSearchAccount>", ReturnType = "BSearchAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBSearchAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBSearchAccountById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "新增查询子账户", Desc = "新增查询子账户", Url = "WeiXinAppService.svc/ST_UDTO_AddBSearchAccountVO", Get = "", Post = "SearchAccountVO", Return = "BaseResultDataValue", ReturnType = "SearchAccountVO")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBSearchAccountVO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBSearchAccountVO(SearchAccountVO entity);

        [ServiceContractDescription(Name = "修改查询子账户", Desc = "修改查询子账户", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBSearchAccount", Get = "", Post = "SearchAccountVO", Return = "BaseResultDataValue", ReturnType = "SearchAccountVO")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBSearchAccountVO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_UpdateBSearchAccountVO(SearchAccountVO entity);

        [ServiceContractDescription(Name = "根据账户ID查询查询子账户", Desc = "根据账户ID查询查询子账户", Url = "WeiXinAppService.svc/ST_UDTO_GetBSearchAccountVOListByWeiXinAccountId", Get = "", Post = "", Return = "BaseResultList<BSearchAccount>", ReturnType = "BSearchAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetBSearchAccountVOListByWeiXinAccountId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetBSearchAccountVOListByWeiXinAccountId();
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

        [ServiceContractDescription(Name = "通过主键ID查询微信账户", Desc = "通过主键ID查询微信账户", Url = "WeiXinAppService.svc/ST_UDTO_SearchBWeiXinAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BWeiXinAccount>", ReturnType = "BWeiXinAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBWeiXinAccountById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBWeiXinAccountById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过OpenID更新手机号", Desc = "通过OpenID更新手机号", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBWeiXinAccountMobileCodeByOpenid?MobileCode={MobileCode}", Get = "MobileCode={MobileCode}", Post = "", Return = "BaseResultList<BaseResultBool>", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBWeiXinAccountMobileCodeByOpenid?MobileCode={MobileCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBWeiXinAccountMobileCodeByOpenid(string MobileCode);


        [ServiceContractDescription(Name = "通过手机号登录", Desc = "通过手机号登录", Url = "WeiXinAppService.svc/Login?MobileCode={MobileCode}&Pwd={Pwd}", Get = "MobileCode={MobileCode}&Pwd={Pwd}", Post = "", Return = "BaseResultList<BaseResultBool>", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Login?MobileCode={MobileCode}&Pwd={Pwd}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool Login(string MobileCode, string Pwd);

        [ServiceContractDescription(Name = "更新登陆时是否需要密码标记", Desc = "更新登陆时是否需要密码标记", Url = "WeiXinAppService.svc/ChangeLoginPasswordFlag?Flag={Flag}", Get = "Flag={Flag}", Post = "", Return = "BaseResultList<BaseResultBool>", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ChangeLoginPasswordFlag?Flag={Flag}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ChangeLoginPasswordFlag(bool Flag);

        [ServiceContractDescription(Name = "医生帐号修改手机号和密码（待微信用户确认未绑定）", Desc = "医生帐号修改手机号和密码（待微信用户确认未绑定）", Url = "WeiXinAppService.svc/WXADS_DoctorAccountBindWeiXinAccountChange?id={id}&AccountCode={AccountCode}&password={password}", Get = "id={id}&AccountCode={AccountCode}&password={password}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/WXADS_DoctorAccountBindWeiXinAccountChange?id={id}&AccountCode={AccountCode}&password={password}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXADS_DoctorAccountBindWeiXinAccountChange(long id,string AccountCode, string password);

        [ServiceContractDescription(Name = "查询微信账户(HQL)", Desc = "查询微信账户(HQL)", Url = "WeiXinAppService.svc/WXADS_SearchWeiXinAccount_User?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BWeiXinAccount>", ReturnType = "ListBWeiXinAccount")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/WXADS_SearchWeiXinAccount_User?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue WXADS_SearchWeiXinAccount_User(int page, int limit, string fields, string where, string sort, bool isPlanish);

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

        #region BScanningBarCodeReportForm

        //[ServiceContractDescription(Name = "新增扫一扫(条码)报告索引表", Desc = "新增扫一扫(条码)报告索引表", Url = "WeiXinAppService.svc/ST_UDTO_AddBScanningBarCodeReportForm", Get = "", Post = "BScanningBarCodeReportForm", Return = "BaseResultDataValue", ReturnType = "BScanningBarCodeReportForm")]
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBScanningBarCodeReportForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_AddBScanningBarCodeReportForm(BScanningBarCodeReportForm entity);

        [ServiceContractDescription(Name = "修改扫一扫(条码)报告索引表", Desc = "修改扫一扫(条码)报告索引表", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBScanningBarCodeReportForm", Get = "", Post = "BScanningBarCodeReportForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBScanningBarCodeReportForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBScanningBarCodeReportForm(BScanningBarCodeReportForm entity);

        [ServiceContractDescription(Name = "修改扫一扫(条码)报告索引表指定的属性", Desc = "修改扫一扫(条码)报告索引表指定的属性", Url = "WeiXinAppService.svc/ST_UDTO_UpdateBScanningBarCodeReportFormByField", Get = "", Post = "BScanningBarCodeReportForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBScanningBarCodeReportFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBScanningBarCodeReportFormByField(BScanningBarCodeReportForm entity, string fields);

        [ServiceContractDescription(Name = "删除扫一扫(条码)报告索引表", Desc = "删除扫一扫(条码)报告索引表", Url = "WeiXinAppService.svc/ST_UDTO_DelBScanningBarCodeReportForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBScanningBarCodeReportForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBScanningBarCodeReportForm(long id);

        [ServiceContractDescription(Name = "查询扫一扫(条码)报告索引表", Desc = "查询扫一扫(条码)报告索引表", Url = "WeiXinAppService.svc/ST_UDTO_SearchBScanningBarCodeReportForm", Get = "", Post = "BScanningBarCodeReportForm", Return = "BaseResultList<BScanningBarCodeReportForm>", ReturnType = "ListBScanningBarCodeReportForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBScanningBarCodeReportForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBScanningBarCodeReportForm(BScanningBarCodeReportForm entity);

        [ServiceContractDescription(Name = "查询扫一扫(条码)报告索引表(HQL)", Desc = "查询扫一扫(条码)报告索引表(HQL)", Url = "WeiXinAppService.svc/ST_UDTO_SearchBScanningBarCodeReportFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BScanningBarCodeReportForm>", ReturnType = "ListBScanningBarCodeReportForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBScanningBarCodeReportFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBScanningBarCodeReportFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询扫一扫(条码)报告索引表", Desc = "通过主键ID查询扫一扫(条码)报告索引表", Url = "WeiXinAppService.svc/ST_UDTO_SearchBScanningBarCodeReportFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BScanningBarCodeReportForm>", ReturnType = "BScanningBarCodeReportForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBScanningBarCodeReportFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBScanningBarCodeReportFormById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "新增扫一扫查询索引", Desc = "新增扫一扫查询索引", Url = "WeiXinAppService.svc/ST_UDTO_AddBScanningBarCodeReportForm?Barcode={Barcode}&SearchUserName={SearchUserName}", Get = "Barcode={Barcode}&SearchUserName={SearchUserName}", Post = "", Return = "BaseResultList<BScanningBarCodeReportForm>", ReturnType = "BScanningBarCodeReportForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "ST_UDTO_AddBScanningBarCodeReportForm?Barcode={Barcode}&SearchUserName={SearchUserName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBScanningBarCodeReportForm(string Barcode, string SearchUserName);

        [ServiceContractDescription(Name = "扫一扫查询索引", Desc = "扫一扫查询索引", Url = "WeiXinAppService.svc/ST_UDTO_GetBScanningBarCodeReportFormList?BScanningBarCodeReportFormID={BScanningBarCodeReportFormID}&Barcode={Barcode}&SearchUserName={SearchUserName}", Get = "BScanningBarCodeReportFormID={BScanningBarCodeReportFormID}&Barcode={Barcode}&SearchUserName={SearchUserName}", Post = "", Return = "BaseResultList<AppRFObject>", ReturnType = "AppRFObject")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "ST_UDTO_GetBScanningBarCodeReportFormList?BScanningBarCodeReportFormID={BScanningBarCodeReportFormID}&Barcode={Barcode}&SearchUserName={SearchUserName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetBScanningBarCodeReportFormList(string BScanningBarCodeReportFormID, string Barcode, string SearchUserName);

        [ServiceContractDescription(Name = "扫一扫查询索引", Desc = "扫一扫查询索引", Url = "WeiXinAppService.svc/ST_UDTO_GetBScanningBarCodeReportFormListByBarcodeSearchUserName?Barcode={Barcode}&SearchUserName={SearchUserName}", Get = "Barcode={Barcode}&SearchUserName={SearchUserName}", Post = "", Return = "BaseResultList<AppRFObject>", ReturnType = "AppRFObject")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "ST_UDTO_GetBScanningBarCodeReportFormListByBarcodeSearchUserName?Barcode={Barcode}&SearchUserName={SearchUserName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetBScanningBarCodeReportFormListByBarcodeSearchUserName(string Barcode, string SearchUserName);

        [ServiceContractDescription(Name = "扫一扫条码查询索引", Desc = "扫一扫条码查询索引", Url = "WeiXinAppService.svc/ST_UDTO_GetBSearchAccountRFListByBarcode?Barcode={Barcode}&Page={Page}&limit={limit}", Get = "Barcode={Barcode}&Page={Page}&limit={limit}", Post = "", Return = "BaseResultList<AppRFObject>", ReturnType = "AppRFObject")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "ST_UDTO_GetBSearchAccountRFListByBarcode?Barcode={Barcode}&Page={Page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetBSearchAccountRFListByBarcode(string Barcode, int Page, int limit);

        #endregion

        #region 手机注册

        [ServiceContractDescription(Name = "验证手机号", Desc = "验证手机号", Url = "WeiXinAppService.svc/SJBhttp_SmsOperator_Vaild?MobileCode={MobileCode}", Post = "", Return = "BaseResultDataValue<string>", ReturnType = "string")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SJBhttp_SmsOperator_Vaild?MobileCode={MobileCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SJBhttp_SmsOperator_Vaild(string MobileCode);

        [ServiceContractDescription(Name = "验证手机号", Desc = "验证手机号", Url = "WeiXinAppService.svc/VaildMobileCode?MobileCode={MobileCode}", Post = "", Return = "BaseResultDataValue<string>", ReturnType = "string")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/VaildMobileCode?MobileCode={MobileCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue VaildMobileCode(string MobileCode);
        #endregion

        #region jsapi
        [ServiceContractDescription(Name = "获取jsapi签名", Desc = "获取jsapi签名", Url = "WeiXinAppService.svc/GetJSAPISignature?noncestr={noncestr}&timestamp={timestamp}&url={url}", Post = "", Return = "BaseResultDataValue<string>", ReturnType = "string")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetJSAPISignature?noncestr={noncestr}&timestamp={timestamp}&url={url}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetJSAPISignature(string noncestr, string timestamp, string url);
        #endregion

        #region BHospitalSearchKey
        [ServiceContractDescription(Name = "获取医院查询条件列表", Desc = "获取医院查询条件列表", Url = "WeiXinAppService.svc/ST_UDTO_GetBHospitalSearchKeyList?HospitalCode={HospitalCode}", Get = "HospitalCode={HospitalCode}", Post = "", Return = "BaseResultList<BHospitalSearch>", ReturnType = "ListBHospitalSearch")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetBHospitalSearchKeyList?HospitalCode={HospitalCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetBHospitalSearchKeyList(string HospitalCode);

        #endregion

        #region BAccountHospitalSearchContext
        //[ServiceContractDescription(Name = "新增查询账户查询条件", Desc = "新增查询账户查询条件", Url = "WeiXinAppService.svc/ST_UDTO_AddBAccountHospitalSearchContext?HospitalCode={HospitalCode}&HospitalSearchID={HospitalSearchID}&key={key}&value={value}&FieldsName={FieldsName}&Comment={Comment}&AccountID={AccountID}", Get = "HospitalCode={HospitalCode}&HospitalSearchID={HospitalSearchID}&key={key}&value={value}&FieldsName={FieldsName}&Comment={Comment}&AccountID={AccountID}", Post = "", Return = "BaseResultDataValue", ReturnType = "BAccountHospitalSearchContext")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBAccountHospitalSearchContext?HospitalCode={HospitalCode}&HospitalSearchID={HospitalSearchID}&key={key}&value={value}&FieldsName={FieldsName}&Comment={Comment}&AccountID={AccountID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_AddBAccountHospitalSearchContext(string HospitalCode, string HospitalSearchID, string key, string value, string FieldsName, string Comment, long AccountID);

        //[ServiceContractDescription(Name = "获取查询账户查询条件列表", Desc = "获取查询账户查询条件列表", Url = "WeiXinAppService.svc/ST_UDTO_GetBAccountHospitalSearchContextList?SearchAccountId={SearchAccountId}&fields={fields}", Get = "SearchAccountId={SearchAccountId}&fields={fields}", Post = "", Return = "BaseResultList<BAccountHospitalSearchContext>", ReturnType = "ListBAccountHospitalSearchContext")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetBAccountHospitalSearchContextList?SearchAccountId={SearchAccountId}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue ST_UDTO_GetBAccountHospitalSearchContextList(long SearchAccountId, string fields);

        [ServiceContractDescription(Name = "查询子账户报告列表", Desc = "查询子账户报告列表", Url = "WeiXinAppService.svc/ST_UDTO_GetBSearchAccountRFList?SearchAccountId={SearchAccountId}&Name={Name}&Page={Page}&limit={limit}", Get = "SearchAccountId={SearchAccountId}&Name={Name}&Page={Page}&limit={limit}", Post = "", Return = "BaseResultList<BSearchAccountReportForm>", ReturnType = "BSearchAccountReportForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetBSearchAccountRFList?SearchAccountId={SearchAccountId}&Name={Name}&Page={Page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetBSearchAccountRFList(string SearchAccountId, string Name, int Page, int limit);

        [ServiceContractDescription(Name = "查询子账户报告列表通过RFIdList", Desc = "查询子账户报告列表RFIdList", Url = "WeiXinAppService.svc/ST_UDTO_GetSearchAccountReportFormListById", Get = "", Post = "ReportFormIndexIdList", Return = "BaseResultList<BSearchAccountReportForm>", ReturnType = "BSearchAccountReportForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetSearchAccountReportFormListById", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetSearchAccountReportFormListById(string ReportFormIndexIdList);

        [ServiceContractDescription(Name = "查询子账户报告通过RFId", Desc = "查询子账户报告通过RFId", Url = "WeiXinAppService.svc/ST_UDTO_GetSearchAccountReportFormById?ReportFormIndexId={ReportFormIndexId}&SearchAccountId={SearchAccountId}", Get = "ReportFormIndexId={ReportFormIndexId}&SearchAccountId={SearchAccountId}", Post = "", Return = "BaseResultList<BSearchAccountReportForm>", ReturnType = "BSearchAccountReportForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetSearchAccountReportFormById?ReportFormIndexId={ReportFormIndexId}&SearchAccountId={SearchAccountId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetSearchAccountReportFormById(string ReportFormIndexId, string SearchAccountId);

        [ServiceContractDescription(Name = "查询子账户报告通过RFId", Desc = "查询子账户报告通过RFId", Url = "WeiXinAppService.svc/Capchcwoaduntnge?OldPwd={OldPwd}&NewPwd={NewPwd}", Get = "OldPwd={OldPwd}&NewPwd={NewPwd}", Post = "", Return = "BaseResultList<BSearchAccountReportForm>", ReturnType = "BSearchAccountReportForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Capchcwoaduntnge?OldPwd={OldPwd}&NewPwd={NewPwd}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool Capchcwoaduntnge(string OldPwd, string NewPwd);

        [ServiceContractDescription(Name = "查询报告列表通过病历号和姓名", Desc = "查询报告列表通过病历号和姓名", Url = "WeiXinAppService.svc/ST_UDTO_GetBSearchAccountRFListByPatNoAndName?PatNo={PatNo}&Name={Name}&Page={Page}&limit={limit}", Get = "PatNo={PatNo}&Name={Name}&Page={Page}&limit={limit}", Post = "", Return = "BaseResultList<BSearchAccountReportForm>", ReturnType = "BSearchAccountReportForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetBSearchAccountRFListByPatNoAndName?PatNo={PatNo}&Name={Name}&Page={Page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetBSearchAccountRFListByPatNoAndName(string PatNo, string Name, int Page, int limit);
        #endregion
    }
}
