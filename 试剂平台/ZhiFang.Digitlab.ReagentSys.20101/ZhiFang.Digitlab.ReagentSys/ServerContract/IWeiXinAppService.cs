using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel.Activation;
using ZhiFang.Digitlab.IBLL.Business;
using ZhiFang.Digitlab.ServiceCommon;
using System.ServiceModel.Web;
using System.ServiceModel;
using ZhiFang.Digitlab.Entity;

namespace ZhiFang.Digitlab.ReagentSys.ServerContract
{
     [ServiceContract(Namespace = "ZhiFang.Digitlab.ReagentSys.WeiXinAppService")]
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

        [ServiceContractDescription(Name = "订单微信消息推送", Desc = "订单微信消息推送", Url = "WeiXinAppService.svc/PushAddBmsCenOrderDoc?Id={Id}", Get = "Id={Id}", Post = "", Return = "BaseResultList<BAccountType>", ReturnType = "ListBAccountType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PushAddBmsCenOrderDoc?Id={Id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue PushAddBmsCenOrderDoc(long Id);

        [ServiceContractDescription(Name = "订单确认微信消息推送", Desc = "订单确认微信消息推送", Url = "WeiXinAppService.svc/PushConfirmBmsCenOrderDoc?Id={Id}", Get = "Id={Id}", Post = "", Return = "BaseResultList<BAccountType>", ReturnType = "ListBAccountType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/PushConfirmBmsCenOrderDoc?Id={Id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue PushConfirmBmsCenOrderDoc(long Id);
    }
}
