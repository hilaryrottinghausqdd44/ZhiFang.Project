using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ZhiFang.Digitlab.Entity;
using System.ServiceModel.Web;
using System.ComponentModel;
using ZhiFang.Digitlab.ServiceCommon;

namespace ZhiFang.Digitlab.ReagentSys.ServerContract
{
    [ServiceContract]
    public interface IConstructionService
    {
        #region 公共服务类

        [ServiceContractDescription(Name = "数据对象列表", Desc = "数据对象列表", Url = "ConstructionService.svc/CS_BA_GetEntityList", Get = "", Post = "", Return = "List<BaseResultEntityClassInfo>", ReturnType = "ListEntity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_BA_GetEntityList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_BA_GetEntityList();

        [ServiceContractDescription(Name = "数据对象结构树", Desc = "数据对象结构树", Url = "ConstructionService.svc/CS_BA_GetEntityFrameTree?EntityName={EntityName}", Get = "EntityName={EntityName}", Post = "", Return = "List<EntityFrameTree>", ReturnType = "Tree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_BA_GetEntityFrameTree?EntityName={EntityName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_BA_GetEntityFrameTree(string EntityName);

        [ServiceContractDescription(Name = "根据模块ID获取预定义属性(数据对象结构树)", Desc = "根据模块ID获取预定义属性(数据对象结构树)", Url = "ConstructionService.svc/CS_BA_GetEntityFrameTreeByModuleOperID?ModuleOperID={ModuleOperID}&EntityName={EntityName}", Get = "ModuleOperID={ModuleOperID}&EntityName={EntityName}", Post = "", Return = "List<EntityFrameTree>", ReturnType = "Tree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_BA_GetEntityFrameTreeByModuleOperID?ModuleOperID={ModuleOperID}&EntityName={EntityName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_BA_GetEntityFrameTreeByModuleOperID(long ModuleOperID, string EntityName);

        [ServiceContractDescription(Name = "查询具体数据对象服务列表", Desc = "查询具体数据对象服务列表", Url = "ConstructionService.svc/CS_BA_SearchReturnEntityServiceListByEntityName?EntityName={EntityName}", Get = "EntityName={EntityName}", Post = "", Return = "List<ServiceContractnfo>", ReturnType = "ListService")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_BA_SearchReturnEntityServiceListByEntityName?EntityName={EntityName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_BA_SearchReturnEntityServiceListByEntityName(string EntityName);

        [ServiceContractDescription(Name = "查询对象属性所属字典服务列表", Desc = "查询对象属性所属字典服务列表", Url = "ConstructionService.svc/CS_BA_SearchReturnEntityDictionaryServiceListByEntityPropertynName?EntityPropertynName={EntityPropertynName}", Get = "EntityPropertynName={EntityPropertynName}", Post = "", Return = "List<ServiceContractnfo>", ReturnType = "ListService")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_BA_SearchReturnEntityDictionaryServiceListByEntityPropertynName?EntityPropertynName={EntityPropertynName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_BA_SearchReturnEntityDictionaryServiceListByEntityPropertynName(string EntityPropertynName);

        [ServiceContractDescription(Name = "查询使用具体数据对象为参数的服务列表", Desc = "查询使用具体数据对象为参数的服务列表", Url = "ConstructionService.svc/CS_BA_SearchParaEntityServiceListByEntityName?EntityName={EntityName}", Get = "EntityName={EntityName}", Post = "", Return = "List<ServiceContractnfo>", ReturnType = "ListService")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_BA_SearchParaEntityServiceListByEntityName?EntityName={EntityName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_BA_SearchParaEntityServiceListByEntityName(string EntityName);


        [ServiceContractDescription(Name = "文件接收服务", Desc = "文件接收服务", Url = "ConstructionService.svc/ReceiveFileService", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "String")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ReceiveFileService", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ReceiveFileService();

        [ServiceContractDescription(Name = "提取中文字符串拼音字头", Desc = "提取中文字符串拼音字头", Url = "ConstructionService.svc/GetPinYin?chinese={chinese}", Get = "chinese={chinese}", Post = "", Return = "BaseResultDataValue", ReturnType = "String")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetPinYin?chinese={chinese}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetPinYin(string chinese);

        [ServiceContractDescription(Name = "模块图表接收服务", Desc = "模块图表接收服务", Url = "ConstructionService.svc/ReceiveModuleIconService", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "String")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ReceiveModuleIconService", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ReceiveModuleIconService();

        [ServiceContractDescription(Name = "JSON字符串接收", Desc = "字符串接收", Url = "ConstructionService.svc/ReceiveJsonStringService", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "String")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ReceiveJsonStringService", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ReceiveJsonStringService();

        [ServiceContractDescription(Name = "返回简单字典信息", Desc = "返回简单字典信息", Url = "ConstructionService.svc/CS_BA_GetCRUDAndFrameByEntityName?EntityName={EntityName}", Get = "EntityName={EntityName}", Post = "", Return = "BaseResultDataValue", ReturnType = "DicEntityInfo")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_BA_GetCRUDAndFrameByEntityName?EntityName={EntityName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Entity.BaseResultDataValue CS_BA_GetCRUDAndFrameByEntityName(string EntityName);

        #endregion

        #region 单表

        #region BTDAppComponents应用组件

        [ServiceContractDescription(Name = "新增应用组件", Desc = "新增应用组件", Url = "ConstructionService.svc/CS_UDTO_AddBTDAppComponents", Get = "", Post = "BTDAppComponents", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_AddBTDAppComponents", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_AddBTDAppComponents();

        [ServiceContractDescription(Name = "修改应用组件", Desc = "修改应用组件", Url = "ConstructionService.svc/CS_UDTO_UpdateBTDAppComponents", Get = "", Post = "BTDAppComponents", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_UpdateBTDAppComponents", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_UpdateBTDAppComponents();

        [ServiceContractDescription(Name = "修改应用组件指定的属性", Desc = "修改应用组件指定的属性", Url = "ConstructionService.svc/CS_UDTO_UpdateBTDAppComponentsByField", Get = "", Post = "BTDAppComponents", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_UpdateBTDAppComponentsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_UpdateBTDAppComponentsByField(BTDAppComponents entity, string fields);

        [ServiceContractDescription(Name = "删除应用组件", Desc = "删除应用组件", Url = "ConstructionService.svc/CS_UDTO_DelBTDAppComponents?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_UDTO_DelBTDAppComponents?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_DelBTDAppComponents(long id);

        [ServiceContractDescription(Name = "查询应用组件", Desc = "查询应用组件", Url = "ConstructionService.svc/CS_UDTO_SearchBTDAppComponents", Get = "", Post = "BTDAppComponents", Return = "BaseResultList<BTDAppComponents>", ReturnType = "ListBTDAppComponents")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDAppComponents", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDAppComponents(BTDAppComponents entity);

        [ServiceContractDescription(Name = "查询应用组件(HQL)", Desc = "查询应用组件(HQL)", Url = "ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDAppComponents>", ReturnType = "ListBTDAppComponents")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDAppComponentsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDAppComponentsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询应用组件", Desc = "通过主键ID查询应用组件", Url = "ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDAppComponents>", ReturnType = "BTDAppComponents")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDAppComponentsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDAppComponentsById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询引用应用组件（HQL）", Desc = "通过传递HQL和ID查询要可以引用的应用组件列表", Url = "ConstructionService.svc/CS_UDTO_SearchRefBTDAppComponentsByHQLAndId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&AppId={AppId}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&AppId={AppId}", Post = "", Return = "EntityList<BTDAppComponents>", ReturnType = "ListBTDAppComponents")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchRefBTDAppComponentsByHQLAndId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&AppId={AppId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchRefBTDAppComponentsByHQLAndId(int page, int limit, string fields, string where, string sort, bool isPlanish, string AppId);

        [ServiceContractDescription(Name = "通过主键ID查询应用组件引用", Desc = "通过主键ID查询应用组件引用", Url = "ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsRefListById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDAppComponentsRef>", ReturnType = "BTDAppComponentsRef")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDAppComponentsRefListById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDAppComponentsRefListById(long id, string fields, bool isPlanish);


        [ServiceContractDescription(Name = "新增应用组件代码文件扩展", Desc = "新增应用组件代码文件扩展", Url = "ConstructionService.svc/CS_UDTO_AddBTDAppComponentsFileEx", Get = "", Post = "BTDAppComponents", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_AddBTDAppComponentsFileEx", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_AddBTDAppComponentsFileEx();

        [ServiceContractDescription(Name = "修改应用组件代码文件扩展", Desc = "修改应用组件代码文件扩展", Url = "ConstructionService.svc/CS_UDTO_UpdateBTDAppComponentsFileEx", Get = "", Post = "BTDAppComponents", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_UpdateBTDAppComponentsFileEx", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_UpdateBTDAppComponentsFileEx();

        [ServiceContractDescription(Name = "根据应用组件ID获取应用组件和操作单列树", Desc = "根据应用组件ID获取应用组件和操作单列树", Url = "ConstructionService.svc/CS_RJ_GetBTDAppComponentsFrameTree?id={id}&treeDataConfig={treeDataConfig}", Get = "id={id}&treeDataConfig={treeDataConfig}", Post = "", Return = "BaseResultDataValue", ReturnType = "Tree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_RJ_GetBTDAppComponentsFrameTree?id={id}&treeDataConfig={treeDataConfig}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_RJ_GetBTDAppComponentsFrameTree(string id, string treeDataConfig);

        [ServiceContractDescription(Name = "根据应用ID获取应用列表树", Desc = "根据应用ID获取应用列表树", Url = "ConstructionService.svc/CS_RJ_GetBTDAppComponentsFrameListTree?id={id}&fields={fields}", Get = "id={id}&fields={fields}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeBTDAppComponents")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_RJ_GetBTDAppComponentsFrameListTree?id={id}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_RJ_GetBTDAppComponentsFrameListTree(string id, string fields);

        [ServiceContractDescription(Name = "根据应用ID获该元应用相关的列表树", Desc = "根据应用ID获该元应用相关的列表树", Url = "ConstructionService.svc/CS_RJ_GetBTDAppComponentsRefTree?id={id}&fields={fields}", Get = "id={id}&fields={fields}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreeBTDAppComponents")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_RJ_GetBTDAppComponentsRefTree?id={id}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_RJ_GetBTDAppComponentsRefTree(string id, string fields);

        [ServiceContractDescription(Name = "上传应用背景图片", Desc = "上传应用背景图片", Url = "ConstructionService.svc/CS_RJ_GetBackgroundPicture", Get = "", Post = "Picture", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_RJ_GetBackgroundPicture", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_RJ_GetBackgroundPicture();
        #endregion

        #region BTDModuleType

        [ServiceContractDescription(Name = "新增应用类型", Desc = "新增应用类型", Url = "ConstructionService.svc/CS_UDTO_AddBTDModuleType", Get = "", Post = "BTDModuleType", Return = "BaseResultDataValue", ReturnType = "BTDModuleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_AddBTDModuleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_AddBTDModuleType(BTDModuleType entity);

        [ServiceContractDescription(Name = "修改应用类型", Desc = "修改应用类型", Url = "ConstructionService.svc/CS_UDTO_UpdateBTDModuleType", Get = "", Post = "BTDModuleType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_UpdateBTDModuleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_UpdateBTDModuleType(BTDModuleType entity);

        [ServiceContractDescription(Name = "修改应用类型指定的属性", Desc = "修改应用类型指定的属性", Url = "ConstructionService.svc/CS_UDTO_UpdateBTDModuleTypeByField", Get = "", Post = "BTDModuleType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_UpdateBTDModuleTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_UpdateBTDModuleTypeByField(BTDModuleType entity, string fields);

        [ServiceContractDescription(Name = "删除应用类型", Desc = "删除应用类型", Url = "ConstructionService.svc/CS_UDTO_DelBTDModuleType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_UDTO_DelBTDModuleType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_DelBTDModuleType(long id);

        [ServiceContractDescription(Name = "查询应用类型", Desc = "查询应用类型", Url = "ConstructionService.svc/CS_UDTO_SearchBTDModuleType", Get = "", Post = "BTDModuleType", Return = "BaseResultList<BTDModuleType>", ReturnType = "ListBTDModuleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDModuleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDModuleType(BTDModuleType entity);

        [ServiceContractDescription(Name = "查询应用类型(HQL)", Desc = "查询应用类型(HQL)", Url = "ConstructionService.svc/CS_UDTO_SearchBTDModuleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDModuleType>", ReturnType = "ListBTDModuleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDModuleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDModuleTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询应用类型", Desc = "通过主键ID查询应用类型", Url = "ConstructionService.svc/CS_UDTO_SearchBTDModuleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDModuleType>", ReturnType = "BTDModuleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDModuleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDModuleTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BTDPictureType
        [ServiceContractDescription(Name = "新增图片类别", Desc = "新增图片类别", Url = "ConstructionService.svc/CS_UDTO_AddBTDPictureType", Get = "", Post = "BTDPictureType", Return = "BaseResultDataValue", ReturnType = "BTDPictureType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_AddBTDPictureType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_AddBTDPictureType(BTDPictureType entity);

        [ServiceContractDescription(Name = "修改图片类别", Desc = "修改图片类别", Url = "ConstructionService.svc/CS_UDTO_UpdateBTDPictureType", Get = "", Post = "BTDPictureType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_UpdateBTDPictureType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_UpdateBTDPictureType(BTDPictureType entity);

        [ServiceContractDescription(Name = "修改图片类别指定的属性", Desc = "修改图片类别指定的属性", Url = "ConstructionService.svc/CS_UDTO_UpdateBTDPictureTypeByField", Get = "", Post = "BTDPictureType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_UpdateBTDPictureTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_UpdateBTDPictureTypeByField(BTDPictureType entity, string fields);

        [ServiceContractDescription(Name = "删除图片类别", Desc = "删除图片类别", Url = "ConstructionService.svc/CS_UDTO_DelBTDPictureType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_UDTO_DelBTDPictureType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_DelBTDPictureType(long id);

        [ServiceContractDescription(Name = "查询图片类别", Desc = "查询图片类别", Url = "ConstructionService.svc/CS_UDTO_SearchBTDPictureType", Get = "", Post = "BTDPictureType", Return = "BaseResultList<BTDPictureType>", ReturnType = "ListBTDPictureType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDPictureType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDPictureType(BTDPictureType entity);

        [ServiceContractDescription(Name = "查询图片类别(HQL)", Desc = "查询图片类别(HQL)", Url = "ConstructionService.svc/CS_UDTO_SearchBTDPictureTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDPictureType>", ReturnType = "ListBTDPictureType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDPictureTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDPictureTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询图片类别", Desc = "通过主键ID查询图片类别", Url = "ConstructionService.svc/CS_UDTO_SearchBTDPictureTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDPictureType>", ReturnType = "BTDPictureType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDPictureTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDPictureTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BTDAppPicture

        [ServiceContractDescription(Name = "新增应用图片", Desc = "新增应用图片", Url = "ConstructionService.svc/CS_UDTO_AddBTDAppPicture", Get = "", Post = "BTDAppPicture", Return = "BaseResultDataValue", ReturnType = "BTDAppPicture")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_AddBTDAppPicture", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_AddBTDAppPicture(BTDAppPicture entity);

        [ServiceContractDescription(Name = "修改应用图片", Desc = "修改应用图片", Url = "ConstructionService.svc/CS_UDTO_UpdateBTDAppPicture", Get = "", Post = "BTDAppPicture", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_UpdateBTDAppPicture", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_UpdateBTDAppPicture(BTDAppPicture entity);

        [ServiceContractDescription(Name = "修改应用图片指定的属性", Desc = "修改应用图片指定的属性", Url = "ConstructionService.svc/CS_UDTO_UpdateBTDAppPictureByField", Get = "", Post = "BTDAppPicture", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_UpdateBTDAppPictureByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_UpdateBTDAppPictureByField(BTDAppPicture entity, string fields);

        [ServiceContractDescription(Name = "删除应用图片", Desc = "删除应用图片", Url = "ConstructionService.svc/CS_UDTO_DelBTDAppPicture?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_UDTO_DelBTDAppPicture?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_DelBTDAppPicture(long id);

        [ServiceContractDescription(Name = "查询应用图片", Desc = "查询应用图片", Url = "ConstructionService.svc/CS_UDTO_SearchBTDAppPicture", Get = "", Post = "BTDAppPicture", Return = "BaseResultList<BTDAppPicture>", ReturnType = "ListBTDAppPicture")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDAppPicture", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDAppPicture(BTDAppPicture entity);

        [ServiceContractDescription(Name = "查询应用图片(HQL)", Desc = "查询应用图片(HQL)", Url = "ConstructionService.svc/CS_UDTO_SearchBTDAppPictureByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDAppPicture>", ReturnType = "ListBTDAppPicture")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDAppPictureByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDAppPictureByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询应用图片", Desc = "通过主键ID查询应用图片", Url = "ConstructionService.svc/CS_UDTO_SearchBTDAppPictureById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDAppPicture>", ReturnType = "BTDAppPicture")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDAppPictureById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDAppPictureById(long id, string fields, bool isPlanish);
        #endregion

        #region BTDPictureTypeCon

        [ServiceContractDescription(Name = "新增图片类型关联", Desc = "新增图片类型关联", Url = "ConstructionService.svc/CS_UDTO_AddBTDPictureTypeCon", Get = "", Post = "BTDPictureTypeCon", Return = "BaseResultDataValue", ReturnType = "BTDPictureTypeCon")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_AddBTDPictureTypeCon", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_AddBTDPictureTypeCon(BTDPictureTypeCon entity);

        [ServiceContractDescription(Name = "修改图片类型关联", Desc = "修改图片类型关联", Url = "ConstructionService.svc/CS_UDTO_UpdateBTDPictureTypeCon", Get = "", Post = "BTDPictureTypeCon", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_UpdateBTDPictureTypeCon", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_UpdateBTDPictureTypeCon(BTDPictureTypeCon entity);

        [ServiceContractDescription(Name = "修改图片类型关联指定的属性", Desc = "修改图片类型关联指定的属性", Url = "ConstructionService.svc/CS_UDTO_UpdateBTDPictureTypeConByField", Get = "", Post = "BTDPictureTypeCon", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_UpdateBTDPictureTypeConByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_UpdateBTDPictureTypeConByField(BTDPictureTypeCon entity, string fields);

        [ServiceContractDescription(Name = "删除图片类型关联", Desc = "删除图片类型关联", Url = "ConstructionService.svc/CS_UDTO_DelBTDPictureTypeCon?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_UDTO_DelBTDPictureTypeCon?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_DelBTDPictureTypeCon(long id);

        [ServiceContractDescription(Name = "查询图片类型关联", Desc = "查询图片类型关联", Url = "ConstructionService.svc/CS_UDTO_SearchBTDPictureTypeCon", Get = "", Post = "BTDPictureTypeCon", Return = "BaseResultList<BTDPictureTypeCon>", ReturnType = "ListBTDPictureTypeCon")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDPictureTypeCon", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDPictureTypeCon(BTDPictureTypeCon entity);

        [ServiceContractDescription(Name = "查询图片类型关联(HQL)", Desc = "查询图片类型关联(HQL)", Url = "ConstructionService.svc/CS_UDTO_SearchBTDPictureTypeConByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDPictureTypeCon>", ReturnType = "ListBTDPictureTypeCon")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDPictureTypeConByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDPictureTypeConByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询图片类型关联", Desc = "通过主键ID查询图片类型关联", Url = "ConstructionService.svc/CS_UDTO_SearchBTDPictureTypeConById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDPictureTypeCon>", ReturnType = "BTDPictureTypeCon")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDPictureTypeConById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDPictureTypeConById(long id, string fields, bool isPlanish);
        #endregion

        #region BTDMacroCommand
        [ServiceContractDescription(Name = "查询宏命令(HQL)", Desc = "查询宏命令(HQL)", Url = "ConstructionService.svc/CS_UDTO_SearchBTDMacroCommandByHQL?page={page}&limit={limit}&fields={fields}&where={where}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDMacroCommand>", ReturnType = "ListBTDMacroCommand")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDMacroCommandByHQL?page={page}&limit={limit}&fields={fields}&where={where}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDMacroCommandByHQL(int page, int limit, string fields, string where, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询宏命令", Desc = "通过主键ID查询宏命令", Url = "ConstructionService.svc/CS_UDTO_SearchBTDMacroCommandByKey?key={key}&fields={fields}&isPlanish={isPlanish}", Get = "key={key}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDMacroCommand>", ReturnType = "BTDMacroCommand")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDMacroCommandByKey?key={key}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDMacroCommandByKey(string key, string fields, bool isPlanish);
        #endregion

        #region BTDAppComponentsOperate

        [ServiceContractDescription(Name = "新增程序操作包括按钮及非按钮操作（如下拉列表等）", Desc = "新增程序操作包括按钮及非按钮操作（如下拉列表等）", Url = "ConstructionService.svc/CS_UDTO_AddBTDAppComponentsOperate", Get = "", Post = "BTDAppComponentsOperate", Return = "BaseResultDataValue", ReturnType = "BTDAppComponentsOperate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_AddBTDAppComponentsOperate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_AddBTDAppComponentsOperate(BTDAppComponentsOperate entity);

        [ServiceContractDescription(Name = "修改程序操作包括按钮及非按钮操作（如下拉列表等）", Desc = "修改程序操作包括按钮及非按钮操作（如下拉列表等）", Url = "ConstructionService.svc/CS_UDTO_UpdateBTDAppComponentsOperate", Get = "", Post = "BTDAppComponentsOperate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_UpdateBTDAppComponentsOperate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_UpdateBTDAppComponentsOperate(BTDAppComponentsOperate entity);

        [ServiceContractDescription(Name = "修改程序操作包括按钮及非按钮操作（如下拉列表等）指定的属性", Desc = "修改程序操作包括按钮及非按钮操作（如下拉列表等）指定的属性", Url = "ConstructionService.svc/CS_UDTO_UpdateBTDAppComponentsOperateByField", Get = "", Post = "BTDAppComponentsOperate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_UpdateBTDAppComponentsOperateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_UpdateBTDAppComponentsOperateByField(BTDAppComponentsOperate entity, string fields);

        [ServiceContractDescription(Name = "删除程序操作包括按钮及非按钮操作（如下拉列表等）", Desc = "删除程序操作包括按钮及非按钮操作（如下拉列表等）", Url = "ConstructionService.svc/CS_UDTO_DelBTDAppComponentsOperate?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_UDTO_DelBTDAppComponentsOperate?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool CS_UDTO_DelBTDAppComponentsOperate(long id);

        [ServiceContractDescription(Name = "查询程序操作包括按钮及非按钮操作（如下拉列表等）", Desc = "查询程序操作包括按钮及非按钮操作（如下拉列表等）", Url = "ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsOperate", Get = "", Post = "BTDAppComponentsOperate", Return = "BaseResultList<BTDAppComponentsOperate>", ReturnType = "ListBTDAppComponentsOperate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDAppComponentsOperate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDAppComponentsOperate(BTDAppComponentsOperate entity);

        [ServiceContractDescription(Name = "查询程序操作包括按钮及非按钮操作（如下拉列表等）(HQL)", Desc = "查询程序操作包括按钮及非按钮操作（如下拉列表等）(HQL)", Url = "ConstructionService.svc/CS_UDTO_SearchBTDAppComponentsOperateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDAppComponentsOperate>", ReturnType = "ListBTDAppComponentsOperate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDAppComponentsOperateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDAppComponentsOperateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询程序操作包括按钮及非按钮操作（如下拉列表等）", Desc = "通过主键ID查询程序操作包括按钮及非按钮操作（如下拉列表等）", Url = "ConstructionService.svc/cCS_UDTO_SearchBTDAppComponentsOperateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BTDAppComponentsOperate>", ReturnType = "BTDAppComponentsOperate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_SearchBTDAppComponentsOperateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_SearchBTDAppComponentsOperateById(long id, string fields, bool isPlanish);
        #endregion

        #endregion

        #region 其他
        [ServiceContractDescription(Name = "获取服务器时间", Desc = "获取服务器时间", Url = "ConstructionService.svc/CS_UDTO_GetServerInformation", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CS_UDTO_GetServerInformation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_UDTO_GetServerInformation();
        #endregion

        #region 图表数据服务构建测试
        [ServiceContractDescription(Name = "图表数据服务", Desc = "图表数据服务", Url = "ConstructionService.svc/CS_ChartData", Get = "", Post = "", Return = "Chart<BaseResultEntityClassInfo>", ReturnType = "ChartEntity")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/CS_ChartData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CS_ChartData();
        #endregion

    }
}

