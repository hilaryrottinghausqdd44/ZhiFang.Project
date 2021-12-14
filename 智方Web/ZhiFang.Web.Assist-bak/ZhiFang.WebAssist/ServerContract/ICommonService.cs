using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.Common;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.WebAssist.ServerContract
{
    [ServiceContract]
    public interface ICommonService

    {
        #region FFile

        [ServiceContractDescription(Name = "新增文档表", Desc = "新增文档表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_AddFFile", Get = "", Post = "FFile", Return = "BaseResultDataValue", ReturnType = "FFile")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_AddFFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_AddFFile(FFile entity);

        [ServiceContractDescription(Name = "通过FormData方式新增文档信息", Desc = "通过FormData方式新增文档信息", Url = "ServerWCF/CommonService.svc/QMS_UDTO_AddFFileByFormData", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_AddFFileByFormData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream QMS_UDTO_AddFFileByFormData();

        [ServiceContractDescription(Name = "通过FormData方式更新文档信息", Desc = "通过FormData方式更新文档信息", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileByFieldAndFormData", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileByFieldAndFormData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream QMS_UDTO_UpdateFFileByFieldAndFormData();

        [ServiceContractDescription(Name = "新增文档信息及文档抄送对象信息表", Desc = "新增文档信息及文档抄送对象信息表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_AddFFileAndFFileCopyUser", Get = "", Post = "FFile", Return = "BaseResultDataValue", ReturnType = "FFile")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_AddFFileAndFFileCopyUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_AddFFileAndFFileCopyUser(FFile entity, IList<FFileCopyUser> fFileCopyUserList, int fFileOperationType, int ffileCopyUserType, string ffileOperationMemo, IList<FFileReadingUser> fFileReadingUserList, int ffileReadingUserType);

        [ServiceContractDescription(Name = "修改文档表", Desc = "修改文档表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFile", Get = "", Post = "FFile", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFile(FFile entity);

        [ServiceContractDescription(Name = "修改文档表指定的属性", Desc = "修改文档表指定的属性", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileByField", Get = "", Post = "FFile", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileByField(FFile entity, string fields);

        [ServiceContractDescription(Name = "更新文档基本信息时,更新文档抄送对象或更新文档阅读对象信息", Desc = "更新文档基本信息时,更新文档抄送对象或更新文档阅读对象信息", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileAndFFileCopyUserOrFFileReadingUserByField", Get = "", Post = "FFile", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileAndFFileCopyUserOrFFileReadingUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileAndFFileCopyUserOrFFileReadingUserByField(FFile entity, IList<FFileCopyUser> fFileCopyUserList, IList<FFileReadingUser> fFileReadingUserList, string fields, int fFileOperationType, int ffileCopyUserType, int ffileReadingUserType, string ffileOperationMemo);

        [ServiceContractDescription(Name = "删除文档表", Desc = "删除文档表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_DelFFile?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QMS_UDTO_DelFFile?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_DelFFile(long id);

        [ServiceContractDescription(Name = "删除文档信息(更新IsUse为false,文档状态为作废)", Desc = "删除文档信息(更新IsUse为false,文档状态为作废)", Url = "ServerWCF/CommonService.svc/QMS_UDTO_DeleleFFileByIds", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_DeleleFFileByIds", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_DeleleFFileByIds(string strIds, bool isUse, int fFileOperationType);

        [ServiceContractDescription(Name = "置顶/撤消置顶文档信息", Desc = "置顶/撤消置顶文档信息", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileIsTopByIds", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileIsTopByIds", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileIsTopByIds(string strIds, bool isTop, int fFileOperationType);

        [ServiceContractDescription(Name = "查询文档表", Desc = "查询文档表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFile", Get = "", Post = "FFile", Return = "BaseResultList<FFile>", ReturnType = "ListFFile")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFile(FFile entity);

        [ServiceContractDescription(Name = "查询文档表(HQL)", Desc = "查询文档表(HQL)", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFile>", ReturnType = "ListFFile")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询某一类型树的直属文档列表(包含某一类型树的所有子类型树)", Desc = "查询某一类型树的直属文档列表(包含某一类型树的所有子类型树)", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileByBDictTreeId?page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFile>", ReturnType = "ListFFile")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileByBDictTreeId?page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileByBDictTreeId(string where, bool isSearchChildNode, int limit, int page, bool isPlanish, string fields, string sort);

        [ServiceContractDescription(Name = "通过主键ID查询文档表", Desc = "通过主键ID查询文档表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileById?id={id}&fields={fields}&isPlanish={isPlanish}&isAddFFileReadingLog={isAddFFileReadingLog}&isAddFFileOperation={isAddFFileOperation}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}&isAddFFileReadingLog={isAddFFileReadingLog}&isAddFFileOperation={isAddFFileOperation}", Post = "", Return = "BaseResultList<FFile>", ReturnType = "FFile")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileById?id={id}&fields={fields}&isPlanish={isPlanish}&isAddFFileReadingLog={isAddFFileReadingLog}&isAddFFileOperation={isAddFFileOperation}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileById(long id, string fields, bool isPlanish, int isAddFFileReadingLog, int isAddFFileOperation);

        [ServiceContractDescription(Name = "查询当前登录者的抄送文档信息(HQL)", Desc = "查询当前登录者的抄送文档信息(HQL)", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileCopyUserListByHQLAndEmployeeID?page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFile>", ReturnType = "ListFFileCopyUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileCopyUserListByHQLAndEmployeeID?page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileCopyUserListByHQLAndEmployeeID(int page, int limit, string fields, string where, bool isSearchChildNode, string sort, bool isPlanish);
        [ServiceContractDescription(Name = "查询当前登录者的阅读文档信息(HQL)", Desc = "查询当前登录者的阅读文档信息(HQL)", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeID?page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFile>", ReturnType = "ListFFileCopyUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeID?page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeID(int page, int limit, string fields, string where, bool isSearchChildNode, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询当前登录者的阅读文档信息(HQL)", Desc = "查询当前登录者的阅读文档信息(HQL)", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeIDAndDictreeids?dictreeids={dictreeids}&page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", Get = "dictreeids={dictreeids}&page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFile>", ReturnType = "ListFFileCopyUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeIDAndDictreeids?dictreeids={dictreeids}&page={page}&limit={limit}&fields={fields}&where={where}&isSearchChildNode={isSearchChildNode}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileReadingUserListByHQLAndEmployeeIDAndDictreeids(string dictreeids, int page, int limit, string fields, string where, bool isSearchChildNode, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "根据新闻ID进行微信消息推送", Desc = "根据新闻ID进行微信消息推送", Url = "ServerWCF/CommonService.svc/QMS_FFileWeiXinMessagePushById?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue<Bool>", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_FFileWeiXinMessagePushById?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_FFileWeiXinMessagePushById(long id);
        #endregion

        #region FFileAttachment

        [ServiceContractDescription(Name = "新增文档附件表", Desc = "新增文档附件表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_AddFFileAttachment", Get = "", Post = "FFileAttachment", Return = "BaseResultDataValue", ReturnType = "FFileAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_AddFFileAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_AddFFileAttachment(FFileAttachment entity);

        [ServiceContractDescription(Name = "修改文档附件表", Desc = "修改文档附件表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileAttachment", Get = "", Post = "FFileAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileAttachment(FFileAttachment entity);

        [ServiceContractDescription(Name = "修改文档附件表指定的属性", Desc = "修改文档附件表指定的属性", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileAttachmentByField", Get = "", Post = "FFileAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileAttachmentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileAttachmentByField(FFileAttachment entity, string fields);

        [ServiceContractDescription(Name = "删除文档附件表", Desc = "删除文档附件表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_DelFFileAttachment?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QMS_UDTO_DelFFileAttachment?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_DelFFileAttachment(long id);

        [ServiceContractDescription(Name = "查询文档附件表", Desc = "查询文档附件表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileAttachment", Get = "", Post = "FFileAttachment", Return = "BaseResultList<FFileAttachment>", ReturnType = "ListFFileAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileAttachment(FFileAttachment entity);

        [ServiceContractDescription(Name = "查询文档附件表(HQL)", Desc = "查询文档附件表(HQL)", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFileAttachment>", ReturnType = "ListFFileAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询文档附件表", Desc = "通过主键ID查询文档附件表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFileAttachment>", ReturnType = "FFileAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileAttachmentById(long id, string fields, bool isPlanish);
        #endregion

        #region FFileCopyUser

        [ServiceContractDescription(Name = "新增文档抄送对象表", Desc = "新增文档抄送对象表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_AddFFileCopyUser", Get = "", Post = "FFileCopyUser", Return = "BaseResultDataValue", ReturnType = "FFileCopyUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_AddFFileCopyUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_AddFFileCopyUser(FFileCopyUser entity);

        [ServiceContractDescription(Name = "修改文档抄送对象表", Desc = "修改文档抄送对象表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileCopyUser", Get = "", Post = "FFileCopyUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileCopyUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileCopyUser(FFileCopyUser entity);

        [ServiceContractDescription(Name = "修改文档抄送对象表指定的属性", Desc = "修改文档抄送对象表指定的属性", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileCopyUserByField", Get = "", Post = "FFileCopyUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileCopyUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileCopyUserByField(FFileCopyUser entity, string fields);

        [ServiceContractDescription(Name = "删除文档抄送对象表", Desc = "删除文档抄送对象表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_DelFFileCopyUser?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QMS_UDTO_DelFFileCopyUser?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_DelFFileCopyUser(long id);

        [ServiceContractDescription(Name = "查询文档抄送对象表", Desc = "查询文档抄送对象表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileCopyUser", Get = "", Post = "FFileCopyUser", Return = "BaseResultList<FFileCopyUser>", ReturnType = "ListFFileCopyUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileCopyUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileCopyUser(FFileCopyUser entity);

        [ServiceContractDescription(Name = "查询文档抄送对象表(HQL)", Desc = "查询文档抄送对象表(HQL)", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileCopyUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFileCopyUser>", ReturnType = "ListFFileCopyUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileCopyUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileCopyUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询文档抄送对象表", Desc = "通过主键ID查询文档抄送对象表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileCopyUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFileCopyUser>", ReturnType = "FFileCopyUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileCopyUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileCopyUserById(long id, string fields, bool isPlanish);
        #endregion

        #region FFileInteraction

        [ServiceContractDescription(Name = "新增文档交流表（不附带附件）", Desc = "新增文档交流表（不附带附件）", Url = "ServerWCF/CommonService.svc/QMS_UDTO_AddFFileInteraction", Get = "", Post = "FFileInteraction", Return = "BaseResultDataValue", ReturnType = "FFileInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_AddFFileInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_AddFFileInteraction(FFileInteraction entity);

        [ServiceContractDescription(Name = "修改文档交流表（不附带附件）", Desc = "修改文档交流表（不附带附件）", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileInteraction", Get = "", Post = "FFileInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileInteraction(FFileInteraction entity);

        [ServiceContractDescription(Name = "修改文档交流表（不附带附件）指定的属性", Desc = "修改文档交流表（不附带附件）指定的属性", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileInteractionByField", Get = "", Post = "FFileInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileInteractionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileInteractionByField(FFileInteraction entity, string fields);

        [ServiceContractDescription(Name = "删除文档交流表（不附带附件）", Desc = "删除文档交流表（不附带附件）", Url = "ServerWCF/CommonService.svc/QMS_UDTO_DelFFileInteraction?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QMS_UDTO_DelFFileInteraction?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_DelFFileInteraction(long id);

        [ServiceContractDescription(Name = "查询文档交流表（不附带附件）", Desc = "查询文档交流表（不附带附件）", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileInteraction", Get = "", Post = "FFileInteraction", Return = "BaseResultList<FFileInteraction>", ReturnType = "ListFFileInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileInteraction(FFileInteraction entity);

        [ServiceContractDescription(Name = "查询文档交流表（不附带附件）(HQL)", Desc = "查询文档交流表（不附带附件）(HQL)", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFileInteraction>", ReturnType = "ListFFileInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询文档交流表（不附带附件）", Desc = "通过主键ID查询文档交流表（不附带附件）", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFileInteraction>", ReturnType = "FFileInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileInteractionById(long id, string fields, bool isPlanish);
        #endregion

        #region FFileOperation

        [ServiceContractDescription(Name = "新增文档操作记录表", Desc = "新增文档操作记录表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_AddFFileOperation", Get = "", Post = "FFileOperation", Return = "BaseResultDataValue", ReturnType = "FFileOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_AddFFileOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_AddFFileOperation(FFileOperation entity);

        [ServiceContractDescription(Name = "修改文档操作记录表", Desc = "修改文档操作记录表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileOperation", Get = "", Post = "FFileOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileOperation(FFileOperation entity);

        [ServiceContractDescription(Name = "修改文档操作记录表指定的属性", Desc = "修改文档操作记录表指定的属性", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileOperationByField", Get = "", Post = "FFileOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileOperationByField(FFileOperation entity, string fields);

        [ServiceContractDescription(Name = "删除文档操作记录表", Desc = "删除文档操作记录表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_DelFFileOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QMS_UDTO_DelFFileOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_DelFFileOperation(long id);

        [ServiceContractDescription(Name = "查询文档操作记录表", Desc = "查询文档操作记录表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileOperation", Get = "", Post = "FFileOperation", Return = "BaseResultList<FFileOperation>", ReturnType = "ListFFileOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileOperation(FFileOperation entity);

        [ServiceContractDescription(Name = "查询文档操作记录表(HQL)", Desc = "查询文档操作记录表(HQL)", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFileOperation>", ReturnType = "ListFFileOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询文档操作记录表", Desc = "通过主键ID查询文档操作记录表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFileOperation>", ReturnType = "FFileOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region FFileReadingLog

        [ServiceContractDescription(Name = "新增文档阅读记录表", Desc = "新增文档阅读记录表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_AddFFileReadingLog", Get = "", Post = "FFileReadingLog", Return = "BaseResultDataValue", ReturnType = "FFileReadingLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_AddFFileReadingLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_AddFFileReadingLog(FFileReadingLog entity);

        [ServiceContractDescription(Name = "修改文档阅读记录表", Desc = "修改文档阅读记录表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileReadingLog", Get = "", Post = "FFileReadingLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileReadingLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileReadingLog(FFileReadingLog entity);

        [ServiceContractDescription(Name = "修改文档阅读记录表指定的属性", Desc = "修改文档阅读记录表指定的属性", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileReadingLogByField", Get = "", Post = "FFileReadingLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileReadingLogByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileReadingLogByField(FFileReadingLog entity, string fields);

        [ServiceContractDescription(Name = "删除文档阅读记录表", Desc = "删除文档阅读记录表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_DelFFileReadingLog?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QMS_UDTO_DelFFileReadingLog?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_DelFFileReadingLog(long id);

        [ServiceContractDescription(Name = "查询文档阅读记录表", Desc = "查询文档阅读记录表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileReadingLog", Get = "", Post = "FFileReadingLog", Return = "BaseResultList<FFileReadingLog>", ReturnType = "ListFFileReadingLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileReadingLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileReadingLog(FFileReadingLog entity);

        [ServiceContractDescription(Name = "查询文档阅读记录表(HQL)", Desc = "查询文档阅读记录表(HQL)", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileReadingLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFileReadingLog>", ReturnType = "ListFFileReadingLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileReadingLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileReadingLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询文档阅读记录表", Desc = "通过主键ID查询文档阅读记录表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileReadingLogById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFileReadingLog>", ReturnType = "FFileReadingLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileReadingLogById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileReadingLogById(long id, string fields, bool isPlanish);
        #endregion

        #region FFileReadingUser

        [ServiceContractDescription(Name = "新增文档阅读对象表", Desc = "新增文档阅读对象表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_AddFFileReadingUser", Get = "", Post = "FFileReadingUser", Return = "BaseResultDataValue", ReturnType = "FFileReadingUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_AddFFileReadingUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_AddFFileReadingUser(FFileReadingUser entity);

        [ServiceContractDescription(Name = "修改文档阅读对象表", Desc = "修改文档阅读对象表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileReadingUser", Get = "", Post = "FFileReadingUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileReadingUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileReadingUser(FFileReadingUser entity);

        [ServiceContractDescription(Name = "修改文档阅读对象表指定的属性", Desc = "修改文档阅读对象表指定的属性", Url = "ServerWCF/CommonService.svc/QMS_UDTO_UpdateFFileReadingUserByField", Get = "", Post = "FFileReadingUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateFFileReadingUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_UpdateFFileReadingUserByField(FFileReadingUser entity, string fields);

        [ServiceContractDescription(Name = "删除文档阅读对象表", Desc = "删除文档阅读对象表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_DelFFileReadingUser?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QMS_UDTO_DelFFileReadingUser?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_DelFFileReadingUser(long id);

        [ServiceContractDescription(Name = "查询文档阅读对象表", Desc = "查询文档阅读对象表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileReadingUser", Get = "", Post = "FFileReadingUser", Return = "BaseResultList<FFileReadingUser>", ReturnType = "ListFFileReadingUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileReadingUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileReadingUser(FFileReadingUser entity);

        [ServiceContractDescription(Name = "查询文档阅读对象表(HQL)", Desc = "查询文档阅读对象表(HQL)", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileReadingUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFileReadingUser>", ReturnType = "ListFFileReadingUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileReadingUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileReadingUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询文档阅读对象表", Desc = "通过主键ID查询文档阅读对象表", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileReadingUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FFileReadingUser>", ReturnType = "FFileReadingUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileReadingUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileReadingUserById(long id, string fields, bool isPlanish);
        #endregion

        [ServiceContractDescription(Name = "下载QMS的文档附件表的文件", Desc = "下载QMS的文档附件表的文件", Url = "ServerWCF/CommonService.svc/QMS_UDTO_FFileAttachmentDownLoadFiles?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_FFileAttachmentDownLoadFiles?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream QMS_UDTO_FFileAttachmentDownLoadFiles(long id, long operateType);

        [ServiceContractDescription(Name = "依文档Id及查询类型获取文档的抄送对象信息或阅读对象信息", Desc = "依文档Id及查询类型获取文档的抄送对象信息或阅读对象信息", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SearchFFileCopyUserOrReadingUserByFFileId?ffileId={ffileId}&searchType={searchType}", Get = "ffileId={ffileId}&searchType={searchType}", Post = "", Return = "BaseResultList", ReturnType = "ListFFile")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchFFileCopyUserOrReadingUserByFFileId?ffileId={ffileId}&searchType={searchType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchFFileCopyUserOrReadingUserByFFileId(long ffileId, string searchType);

        [ServiceContractDescription(Name = "生成帮助文档", Desc = "生成帮助文档", Url = "ServerWCF/CommonService.svc/QMS_UDTO_SaveHelpHtmlAndJson?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SaveHelpHtmlAndJson?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SaveHelpHtmlAndJson(long id);

        [ServiceContractDescription(Name = "获取到生成的帮助文档", Desc = "获取到生成的帮助文档", Url = "ServerWCF/CommonService.svc/QMS_UDTO_GetHelpHtmlAndJson?no={no}&fileName={fileName}", Get = "no={no}&fileName={fileName}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_GetHelpHtmlAndJson?no={no}&fileName={fileName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_GetHelpHtmlAndJson(string no, string fileName);

        [ServiceContractDescription(Name = "上传新闻缩略图服务", Desc = "上传新闻缩略图服务", Url = "ProjectProgressMonitorManageService.svc/QMS_UDTO_UploadNewsThumbnails", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UploadNewsThumbnails", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message QMS_UDTO_UploadNewsThumbnails();

        [ServiceContractDescription(Name = "下载新闻缩略图文件", Desc = "下载新闻缩略图文件", Url = "ServerWCF/CommonService.svc/QMS_UDTO_DownLoadNewsThumbnailsById?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_DownLoadNewsThumbnailsById?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream QMS_UDTO_DownLoadNewsThumbnailsById(long id, long operateType);

        #region 获取程序内部字典
        [ServiceContractDescription(Name = "获取枚举字典", Desc = "获取枚举字典", Url = "ServerWCF/CommonService.svc/GetEnumDic?enumname={enumname}", Get = "enumname={enumname}", Post = "", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetEnumDic?enumname={enumname}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetEnumDic(string enumname);

        [ServiceContractDescription(Name = "获取类字典", Desc = "获取类字典", Url = "ServerWCF/CommonService.svc/GetClassDic?classname={classname}&classnamespace={classnamespace}", Get = "classname={classname}&classnamespace={classnamespace}", Post = "", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetClassDic?classname={classname}&classnamespace={classnamespace}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetClassDic(string classname, string classnamespace);

        [ServiceContractDescription(Name = "获取类字典列表", Desc = "获取类字典列表", Url = "ServerWCF/CommonService.svc/GetClassDicList", Get = "", Post = "ClassDicSearchPara", Return = "BaseResultDataValue", ReturnType = "ClassDicList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetClassDicList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetClassDicList(ClassDicSearchPara[] jsonpara);

        #endregion
    }
}