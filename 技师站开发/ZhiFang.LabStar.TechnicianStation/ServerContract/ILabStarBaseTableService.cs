using System.IO;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation
{
    [ServiceContract]
    public interface ILabStarBaseTableService
    {

        #region BHost

        [ServiceContractDescription(Name = "新增站点表", Desc = "新增站点表", Url = "LabStarBaseTableService.svc/LS_UDTO_AddBHost", Get = "", Post = "BHost", Return = "BaseResultDataValue", ReturnType = "BHost")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddBHost", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddBHost(BHost entity);

        [ServiceContractDescription(Name = "修改站点表指定的属性", Desc = "修改站点表指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateBHostByField", Get = "", Post = "BHost", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateBHostByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateBHostByField(BHost entity, string fields);

        [ServiceContractDescription(Name = "删除站点表", Desc = "删除站点表", Url = "LabStarBaseTableService.svc/LS_UDTO_DelBHost?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelBHost?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelBHost(long id);

        [ServiceContractDescription(Name = "查询站点表(HQL)", Desc = "查询站点表(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBHostByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHost>", ReturnType = "ListBHost")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBHostByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBHostByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询站点表", Desc = "通过主键ID查询站点表", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBHostById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHost>", ReturnType = "BHost")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBHostById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBHostById(long id, string fields, bool isPlanish);
        #endregion

        #region BHostType

        [ServiceContractDescription(Name = "新增站点类型表", Desc = "新增站点类型表", Url = "LabStarBaseTableService.svc/LS_UDTO_AddBHostType", Get = "", Post = "BHostType", Return = "BaseResultDataValue", ReturnType = "BHostType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddBHostType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddBHostType(BHostType entity);

        [ServiceContractDescription(Name = "修改站点类型表指定的属性", Desc = "修改站点类型表指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateBHostTypeByField", Get = "", Post = "BHostType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateBHostTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateBHostTypeByField(BHostType entity, string fields);

        [ServiceContractDescription(Name = "删除站点类型表", Desc = "删除站点类型表", Url = "LabStarBaseTableService.svc/LS_UDTO_DelBHostType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelBHostType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelBHostType(long id);

        [ServiceContractDescription(Name = "查询站点类型表(HQL)", Desc = "查询站点类型表(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBHostTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHostType>", ReturnType = "ListBHostType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBHostTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBHostTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询站点类型表", Desc = "通过主键ID查询站点类型表", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBHostTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHostType>", ReturnType = "BHostType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBHostTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBHostTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region BHostTypeUser

        [ServiceContractDescription(Name = "新增站点类型表", Desc = "新增站点类型表", Url = "LabStarBaseTableService.svc/LS_UDTO_AddBHostTypeUser", Get = "", Post = "BHostTypeUser", Return = "BaseResultDataValue", ReturnType = "BHostTypeUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddBHostTypeUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddBHostTypeUser(BHostTypeUser entity);

        [ServiceContractDescription(Name = "修改站点类型表指定的属性", Desc = "修改站点类型表指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateBHostTypeUserByField", Get = "", Post = "BHostTypeUser", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateBHostTypeUserByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateBHostTypeUserByField(BHostTypeUser entity, string fields);

        [ServiceContractDescription(Name = "删除站点类型表", Desc = "删除站点类型表", Url = "LabStarBaseTableService.svc/LS_UDTO_DelBHostTypeUser?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelBHostTypeUser?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelBHostTypeUser(long id);

        [ServiceContractDescription(Name = "通过主键ID查询站点类型表", Desc = "通过主键ID查询站点类型表", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBHostTypeUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHostTypeUser>", ReturnType = "BHostTypeUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBHostTypeUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBHostTypeUserById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询站点类型人员关系表(HQL)", Desc = "查询站点类型人员关系表(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBHostTypeUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHostTypeUser>", ReturnType = "BHostTypeUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBHostTypeUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBHostTypeUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);
        #endregion

        #region BPara

        [ServiceContractDescription(Name = "新增系统参数表", Desc = "新增系统参数表", Url = "LabStarBaseTableService.svc/LS_UDTO_AddBPara", Get = "", Post = "BPara", Return = "BaseResultDataValue", ReturnType = "BPara")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddBPara", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddBPara(BPara entity);

        [ServiceContractDescription(Name = "修改系统参数表指定的属性", Desc = "修改系统参数表指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateBParaByField", Get = "", Post = "BPara", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateBParaByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateBParaByField(BPara entity, string fields);

        [ServiceContractDescription(Name = "删除系统参数表", Desc = "删除系统参数表", Url = "LabStarBaseTableService.svc/LS_UDTO_DelBPara?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelBPara?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelBPara(long id);

        [ServiceContractDescription(Name = "查询系统参数表(HQL)", Desc = "查询系统参数表(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBParaByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPara>", ReturnType = "ListBPara")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBParaByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBParaByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询系统参数表", Desc = "通过主键ID查询系统参数表", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBParaById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPara>", ReturnType = "BPara")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBParaById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBParaById(long id, string fields, bool isPlanish);
        #endregion

        #region BParaItem

        [ServiceContractDescription(Name = "新增系统参数关系表", Desc = "新增系统参数关系表", Url = "LabStarBaseTableService.svc/LS_UDTO_AddBParaItem", Get = "", Post = "BParaItem", Return = "BaseResultDataValue", ReturnType = "BParaItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddBParaItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddBParaItem(BParaItem entity);

        [ServiceContractDescription(Name = "修改系统参数关系表指定的属性", Desc = "修改系统参数关系表指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateBParaItemByField", Get = "", Post = "BParaItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateBParaItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateBParaItemByField(BParaItem entity, string fields);

        [ServiceContractDescription(Name = "删除系统参数关系表", Desc = "删除系统参数关系表", Url = "LabStarBaseTableService.svc/LS_UDTO_DelBParaItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelBParaItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelBParaItem(long id);

        [ServiceContractDescription(Name = "查询系统参数关系表(HQL)", Desc = "查询系统参数关系表(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBParaItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BParaItem>", ReturnType = "ListBParaItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBParaItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBParaItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询系统参数关系表", Desc = "通过主键ID查询系统参数关系表", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBParaItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BParaItem>", ReturnType = "BParaItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBParaItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBParaItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LBChargeType

        [ServiceContractDescription(Name = "新增LB_Destination", Desc = "新增LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBChargeType", Get = "", Post = "LBChargeType", Return = "BaseResultDataValue", ReturnType = "LBChargeType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBChargeType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBChargeType(LBChargeType entity);

        [ServiceContractDescription(Name = "修改LB_Destination指定的属性", Desc = "修改LB_Destination指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBChargeTypeByField", Get = "", Post = "LBChargeType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBChargeTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBChargeTypeByField(LBChargeType entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Destination", Desc = "删除LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBChargeType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBChargeType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBChargeType(long id);

        [ServiceContractDescription(Name = "查询LB_Destination(HQL)", Desc = "查询LB_Destination(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBChargeTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBChargeType>", ReturnType = "ListLBChargeType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBChargeTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBChargeTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Destination", Desc = "通过主键ID查询LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBChargeTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBChargeType>", ReturnType = "LBChargeType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBChargeTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBChargeTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region LBCollectPart

        [ServiceContractDescription(Name = "新增LB_Destination", Desc = "新增LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBCollectPart", Get = "", Post = "LBCollectPart", Return = "BaseResultDataValue", ReturnType = "LBCollectPart")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBCollectPart", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBCollectPart(LBCollectPart entity);

        [ServiceContractDescription(Name = "修改LB_Destination指定的属性", Desc = "修改LB_Destination指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBCollectPartByField", Get = "", Post = "LBCollectPart", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBCollectPartByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBCollectPartByField(LBCollectPart entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Destination", Desc = "删除LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBCollectPart?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBCollectPart?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBCollectPart(long id);

        [ServiceContractDescription(Name = "查询LB_Destination(HQL)", Desc = "查询LB_Destination(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBCollectPartByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBCollectPart>", ReturnType = "ListLBCollectPart")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBCollectPartByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBCollectPartByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Destination", Desc = "通过主键ID查询LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBCollectPartById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBCollectPart>", ReturnType = "LBCollectPart")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBCollectPartById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBCollectPartById(long id, string fields, bool isPlanish);
        #endregion

        #region LBDestination

        [ServiceContractDescription(Name = "新增LB_Destination", Desc = "新增LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBDestination", Get = "", Post = "LBDestination", Return = "BaseResultDataValue", ReturnType = "LBDestination")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBDestination", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBDestination(LBDestination entity);

        [ServiceContractDescription(Name = "修改LB_Destination指定的属性", Desc = "修改LB_Destination指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBDestinationByField", Get = "", Post = "LBDestination", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBDestinationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBDestinationByField(LBDestination entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Destination", Desc = "删除LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBDestination?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBDestination?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBDestination(long id);

        [ServiceContractDescription(Name = "查询LB_Destination(HQL)", Desc = "查询LB_Destination(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBDestinationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBDestination>", ReturnType = "ListLBDestination")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBDestinationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBDestinationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Destination", Desc = "通过主键ID查询LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBDestinationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBDestination>", ReturnType = "LBDestination")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBDestinationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBDestinationById(long id, string fields, bool isPlanish);
        #endregion

        #region LBDiag

        [ServiceContractDescription(Name = "新增LB_Diag", Desc = "新增LB_Diag", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBDiag", Get = "", Post = "LBDiag", Return = "BaseResultDataValue", ReturnType = "LBDiag")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBDiag", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBDiag(LBDiag entity);

        [ServiceContractDescription(Name = "修改LB_Diag指定的属性", Desc = "修改LB_Diag指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBDiagByField", Get = "", Post = "LBDiag", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBDiagByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBDiagByField(LBDiag entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Diag", Desc = "删除LB_Diag", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBDiag?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBDiag?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBDiag(long id);

        [ServiceContractDescription(Name = "查询LB_Diag(HQL)", Desc = "查询LB_Diag(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBDiagByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBDiag>", ReturnType = "ListLBDiag")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBDiagByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBDiagByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Diag", Desc = "通过主键ID查询LB_Diag", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBDiagById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBDiag>", ReturnType = "LBDiag")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBDiagById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBDiagById(long id, string fields, bool isPlanish);
        #endregion

        #region LBDict

        [ServiceContractDescription(Name = "新增LB_Dict", Desc = "新增LB_Dict", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBDict", Get = "", Post = "LBDict", Return = "BaseResultDataValue", ReturnType = "LBDict")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBDict(LBDict entity);

        [ServiceContractDescription(Name = "修改LB_Dict指定的属性", Desc = "修改LB_Dict指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBDictByField", Get = "", Post = "LBDict", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBDictByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBDictByField(LBDict entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Dict", Desc = "删除LB_Dict", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBDict?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBDict?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBDict(long id);

        [ServiceContractDescription(Name = "查询LB_Dict(HQL)", Desc = "查询LB_Dict(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBDictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBDict>", ReturnType = "ListLBDict")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBDictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBDictByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Dict", Desc = "通过主键ID查询LB_Dict", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBDictById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBDict>", ReturnType = "LBDict")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBDictById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBDictById(long id, string fields, bool isPlanish);
        #endregion

        #region LBEquip

        [ServiceContractDescription(Name = "新增LB_Equip", Desc = "新增LB_Equip", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBEquip", Get = "", Post = "LBEquip", Return = "BaseResultDataValue", ReturnType = "LBEquip")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBEquip(LBEquip entity);

        [ServiceContractDescription(Name = "修改LB_Equip指定的属性", Desc = "修改LB_Equip指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBEquipByField", Get = "", Post = "LBEquip", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBEquipByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBEquipByField(LBEquip entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Equip", Desc = "删除LB_Equip", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBEquip?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBEquip?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBEquip(long id);

        [ServiceContractDescription(Name = "查询LB_Equip(HQL)", Desc = "查询LB_Equip(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBEquip>", ReturnType = "ListLBEquip")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBEquipByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBEquipByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Equip", Desc = "通过主键ID查询LB_Equip", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBEquip>", ReturnType = "LBEquip")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBEquipById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBEquipById(long id, string fields, bool isPlanish);
        #endregion

        #region LBEquipItem

        [ServiceContractDescription(Name = "新增LB_EquipItem", Desc = "新增LB_EquipItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBEquipItem", Get = "", Post = "LBEquipItem", Return = "BaseResultDataValue", ReturnType = "LBEquipItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBEquipItem(LBEquipItem entity);

        [ServiceContractDescription(Name = "修改LB_EquipItem指定的属性", Desc = "修改LB_EquipItem指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBEquipItemByField", Get = "", Post = "LBEquipItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBEquipItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBEquipItemByField(LBEquipItem entity, string fields);

        [ServiceContractDescription(Name = "删除LB_EquipItem", Desc = "删除LB_EquipItem", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBEquipItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBEquipItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBEquipItem(long id);

        [ServiceContractDescription(Name = "查询LB_EquipItem(HQL)", Desc = "查询LB_EquipItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBEquipItem>", ReturnType = "ListLBEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBEquipItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_EquipItem", Desc = "通过主键ID查询LB_EquipItem", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBEquipItem>", ReturnType = "LBEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBEquipItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBEquipItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LBEquipResultTH

        [ServiceContractDescription(Name = "新增LB_EquipItem", Desc = "新增LB_EquipItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBEquipResultTH", Get = "", Post = "LBEquipResultTH", Return = "BaseResultDataValue", ReturnType = "LBEquipResultTH")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBEquipResultTH", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBEquipResultTH(LBEquipResultTH entity);

        [ServiceContractDescription(Name = "修改LB_EquipItem指定的属性", Desc = "修改LB_EquipItem指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBEquipResultTHByField", Get = "", Post = "LBEquipResultTH", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBEquipResultTHByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBEquipResultTHByField(LBEquipResultTH entity, string fields);

        [ServiceContractDescription(Name = "删除LB_EquipItem", Desc = "删除LB_EquipItem", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBEquipResultTH?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBEquipResultTH?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBEquipResultTH(long id);

        [ServiceContractDescription(Name = "查询LB_EquipItem(HQL)", Desc = "查询LB_EquipItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipResultTHByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBEquipResultTH>", ReturnType = "ListLBEquipResultTH")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBEquipResultTHByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBEquipResultTHByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_EquipItem", Desc = "通过主键ID查询LB_EquipItem", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipResultTHById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBEquipResultTH>", ReturnType = "LBEquipResultTH")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBEquipResultTHById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBEquipResultTHById(long id, string fields, bool isPlanish);
        #endregion

        #region LBEquipSection

        [ServiceContractDescription(Name = "新增LB_EquipItem", Desc = "新增LB_EquipItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBEquipSection", Get = "", Post = "LBEquipSection", Return = "BaseResultDataValue", ReturnType = "LBEquipSection")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBEquipSection", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBEquipSection(LBEquipSection entity);

        [ServiceContractDescription(Name = "修改LB_EquipItem指定的属性", Desc = "修改LB_EquipItem指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBEquipSectionByField", Get = "", Post = "LBEquipSection", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBEquipSectionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBEquipSectionByField(LBEquipSection entity, string fields);

        [ServiceContractDescription(Name = "删除LB_EquipItem", Desc = "删除LB_EquipItem", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBEquipSection?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBEquipSection?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBEquipSection(long id);

        [ServiceContractDescription(Name = "查询LB_EquipItem(HQL)", Desc = "查询LB_EquipItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipSectionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBEquipSection>", ReturnType = "ListLBEquipSection")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBEquipSectionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBEquipSectionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_EquipItem", Desc = "通过主键ID查询LB_EquipItem", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipSectionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBEquipSection>", ReturnType = "LBEquipSection")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBEquipSectionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBEquipSectionById(long id, string fields, bool isPlanish);
        #endregion

        #region LBExpertRule

        [ServiceContractDescription(Name = "新增LBExpertRule", Desc = "新增LBExpertRule", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBExpertRule", Get = "", Post = "LBExpertRule", Return = "BaseResultDataValue", ReturnType = "LBExpertRule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBExpertRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBExpertRule(LBExpertRule entity);

        [ServiceContractDescription(Name = "修改LBExpertRule指定的属性", Desc = "修改LBExpertRule指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBExpertRuleByField", Get = "", Post = "LBExpertRule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBExpertRuleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBExpertRuleByField(LBExpertRule entity, string fields);

        [ServiceContractDescription(Name = "删除LBExpertRule", Desc = "删除LBExpertRule", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBExpertRule?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBExpertRule?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBExpertRule(long id);

        [ServiceContractDescription(Name = "查询LBExpertRule(HQL)", Desc = "查询LBExpertRule(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBExpertRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBExpertRule>", ReturnType = "ListLBExpertRule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBExpertRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBExpertRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LBExpertRule", Desc = "通过主键ID查询LBExpertRule", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBExpertRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBExpertRule>", ReturnType = "LBExpertRule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBExpertRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBExpertRuleById(long id, string fields, bool isPlanish);
        #endregion

        #region LBExpertRuleList

        [ServiceContractDescription(Name = "新增LBExpertRuleList", Desc = "新增LBExpertRuleList", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBExpertRuleList", Get = "", Post = "LBExpertRuleList", Return = "BaseResultDataValue", ReturnType = "LBExpertRuleList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBExpertRuleList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBExpertRuleList(LBExpertRuleList entity);

        [ServiceContractDescription(Name = "修改LBExpertRuleList指定的属性", Desc = "修改LBExpertRuleList指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBExpertRuleListByField", Get = "", Post = "LBExpertRuleList", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBExpertRuleListByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBExpertRuleListByField(LBExpertRuleList entity, string fields);

        [ServiceContractDescription(Name = "删除LBExpertRuleList", Desc = "删除LBExpertRuleList", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBExpertRuleList?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBExpertRuleList?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBExpertRuleList(long id);

        [ServiceContractDescription(Name = "查询LBExpertRuleList(HQL)", Desc = "查询LBExpertRuleList(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBExpertRuleListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBExpertRuleList>", ReturnType = "ListLBExpertRuleList")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBExpertRuleListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBExpertRuleListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LBExpertRuleList", Desc = "通过主键ID查询LBExpertRuleList", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBExpertRuleListById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBExpertRuleList>", ReturnType = "LBExpertRuleList")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBExpertRuleListById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBExpertRuleListById(long id, string fields, bool isPlanish);
        #endregion

        #region LBFolk

        [ServiceContractDescription(Name = "新增LB_Folk", Desc = "新增LB_Folk", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBFolk", Get = "", Post = "LBFolk", Return = "BaseResultDataValue", ReturnType = "LBFolk")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBFolk", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBFolk(LBFolk entity);

        [ServiceContractDescription(Name = "修改LB_Folk指定的属性", Desc = "修改LB_Folk指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBFolkByField", Get = "", Post = "LBFolk", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBFolkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBFolkByField(LBFolk entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Folk", Desc = "删除LB_Folk", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBFolk?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBFolk?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBFolk(long id);

        [ServiceContractDescription(Name = "查询LB_Folk(HQL)", Desc = "查询LB_Folk(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBFolkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBFolk>", ReturnType = "ListLBFolk")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBFolkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBFolkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Folk", Desc = "通过主键ID查询LB_Folk", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBFolkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBFolk>", ReturnType = "LBFolk")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBFolkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBFolkById(long id, string fields, bool isPlanish);
        #endregion

        #region LBItem

        [ServiceContractDescription(Name = "新增LB_Item", Desc = "新增LB_Item", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBItem", Get = "", Post = "LBItem", Return = "BaseResultDataValue", ReturnType = "LBItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBItem(LBItem entity);

        [ServiceContractDescription(Name = "修改LB_Item指定的属性", Desc = "修改LB_Item指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemByField", Get = "", Post = "LBItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBItemByField(LBItem entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Item", Desc = "删除LB_Item", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBItem(long id);

        [ServiceContractDescription(Name = "查询LB_Item(HQL)", Desc = "查询LB_Item(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItem>", ReturnType = "ListLBItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询LB_Item(HQL)", Desc = "查询LB_Item(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCurPageByHQL?id={id}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "id={id}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItem>", ReturnType = "ListLBItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemCurPageByHQL?id={id}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemCurPageByHQL(long id, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Item", Desc = "通过主键ID查询LB_Item", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItem>", ReturnType = "LBItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "按传入的检验小组Id从小组项目里获取检验项目信息(传入的检验小组Id值为空时从检验项目获取信息)", Desc = "按传入的检验小组Id从小组项目里获取检验项目信息(传入的检验小组Id值为空时从检验项目获取信息)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchByLBSectionItemHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&lbsectionId={lbsectionId}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&lbsectionId={lbsectionId}", Post = "", Return = "BaseResultList<LBItem>", ReturnType = "ListLBItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchByLBSectionItemHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&lbsectionId={lbsectionId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchByLBSectionItemHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, string lbsectionId);

        [ServiceContractDescription(Name = "获取未进行组合项目拆分的组合项目信息", Desc = "获取未进行组合项目拆分的组合项目信息", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchNotLBParItemSplitPLBItemListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItem>", ReturnType = "ListLBItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchNotLBParItemSplitPLBItemListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchNotLBParItemSplitPLBItemListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "从已进行组合项目拆分的关系表获取到组合项目集合信息", Desc = "从已进行组合项目拆分的关系表获取到组合项目集合信息", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchAlreadyLBParItemSplitPLBItemListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItem>", ReturnType = "ListLBItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchAlreadyLBParItemSplitPLBItemListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchAlreadyLBParItemSplitPLBItemListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        #endregion

        #region LBItemCharge

        [ServiceContractDescription(Name = "新增LB_Destination", Desc = "新增LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBItemCharge", Get = "", Post = "LBItemCharge", Return = "BaseResultDataValue", ReturnType = "LBItemCharge")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBItemCharge", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBItemCharge(LBItemCharge entity);

        [ServiceContractDescription(Name = "修改LB_Destination指定的属性", Desc = "修改LB_Destination指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemChargeByField", Get = "", Post = "LBItemCharge", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBItemChargeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBItemChargeByField(LBItemCharge entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Destination", Desc = "删除LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBItemCharge?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBItemCharge?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBItemCharge(long id);

        [ServiceContractDescription(Name = "查询LB_Destination(HQL)", Desc = "查询LB_Destination(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemChargeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemCharge>", ReturnType = "ListLBItemCharge")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemChargeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemChargeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Destination", Desc = "通过主键ID查询LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemChargeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemCharge>", ReturnType = "LBItemCharge")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemChargeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemChargeById(long id, string fields, bool isPlanish);
        #endregion

        #region LBItemCalc

        [ServiceContractDescription(Name = "新增LB_ItemCalc", Desc = "新增LB_ItemCalc", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBItemCalc", Get = "", Post = "LBItemCalc", Return = "BaseResultDataValue", ReturnType = "LBItemCalc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBItemCalc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBItemCalc(LBItemCalc entity);

        [ServiceContractDescription(Name = "修改LB_ItemCalc指定的属性", Desc = "修改LB_ItemCalc指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemCalcByField", Get = "", Post = "LBItemCalc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBItemCalcByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBItemCalcByField(LBItemCalc entity, string fields);

        [ServiceContractDescription(Name = "删除LB_ItemCalc", Desc = "删除LB_ItemCalc", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBItemCalc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBItemCalc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBItemCalc(long id);

        [ServiceContractDescription(Name = "查询LB_ItemCalc(HQL)", Desc = "查询LB_ItemCalc(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCalcByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemCalc>", ReturnType = "ListLBItemCalc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemCalcByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemCalcByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_ItemCalc", Desc = "通过主键ID查询LB_ItemCalc", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCalcById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemCalc>", ReturnType = "LBItemCalc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemCalcById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemCalcById(long id, string fields, bool isPlanish);
        #endregion

        #region LBItemCalcFormula

        [ServiceContractDescription(Name = "新增LB_ItemCalcFormula", Desc = "新增LB_ItemCalcFormula", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBItemCalcFormula", Get = "", Post = "LBItemCalcFormula", Return = "BaseResultDataValue", ReturnType = "LBItemCalcFormula")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBItemCalcFormula", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBItemCalcFormula(LBItemCalcFormula entity);

        [ServiceContractDescription(Name = "修改LB_ItemCalcFormula指定的属性", Desc = "修改LB_ItemCalcFormula指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemCalcFormulaByField", Get = "", Post = "LBItemCalcFormula", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBItemCalcFormulaByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBItemCalcFormulaByField(LBItemCalcFormula entity, string fields);

        [ServiceContractDescription(Name = "删除LB_ItemCalcFormula", Desc = "删除LB_ItemCalcFormula", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBItemCalcFormula?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBItemCalcFormula?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBItemCalcFormula(long id);

        [ServiceContractDescription(Name = "查询LB_ItemCalcFormula(HQL)", Desc = "查询LB_ItemCalcFormula(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCalcFormulaByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemCalcFormula>", ReturnType = "ListLBItemCalcFormula")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemCalcFormulaByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemCalcFormulaByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_ItemCalcFormula", Desc = "通过主键ID查询LB_ItemCalcFormula", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCalcFormulaById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemCalcFormula>", ReturnType = "LBItemCalcFormula")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemCalcFormulaById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemCalcFormulaById(long id, string fields, bool isPlanish);
        #endregion

        #region LBItemComp

        [ServiceContractDescription(Name = "新增LB_ItemComp", Desc = "新增LB_ItemComp", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBItemComp", Get = "", Post = "LBItemComp", Return = "BaseResultDataValue", ReturnType = "LBItemComp")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBItemComp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBItemComp(LBItemComp entity);

        [ServiceContractDescription(Name = "修改LB_ItemComp指定的属性", Desc = "修改LB_ItemComp指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemCompByField", Get = "", Post = "LBItemComp", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBItemCompByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBItemCompByField(LBItemComp entity, string fields);

        [ServiceContractDescription(Name = "删除LB_ItemComp", Desc = "删除LB_ItemComp", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBItemComp?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBItemComp?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBItemComp(long id);

        [ServiceContractDescription(Name = "查询LB_ItemComp(HQL)", Desc = "查询LB_ItemComp(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCompByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemComp>", ReturnType = "ListLBItemComp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemCompByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemCompByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_ItemComp", Desc = "通过主键ID查询LB_ItemComp", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCompById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemComp>", ReturnType = "LBItemComp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemCompById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemCompById(long id, string fields, bool isPlanish);
        #endregion

        #region LBItemGroup

        [ServiceContractDescription(Name = "新增LB_ItemGroup", Desc = "新增LB_ItemGroup", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBItemGroup", Get = "", Post = "LBItemGroup", Return = "BaseResultDataValue", ReturnType = "LBItemGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBItemGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBItemGroup(LBItemGroup entity);

        [ServiceContractDescription(Name = "修改LB_ItemGroup指定的属性", Desc = "修改LB_ItemGroup指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemGroupByField", Get = "", Post = "LBItemGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBItemGroupByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBItemGroupByField(LBItemGroup entity, string fields);

        [ServiceContractDescription(Name = "删除LB_ItemGroup", Desc = "删除LB_ItemGroup", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBItemGroup?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBItemGroup?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBItemGroup(long id);

        [ServiceContractDescription(Name = "查询LB_ItemGroup(HQL)", Desc = "查询LB_ItemGroup(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemGroup>", ReturnType = "ListLBItemGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_ItemGroup", Desc = "通过主键ID查询LB_ItemGroup", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemGroup>", ReturnType = "LBItemGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemGroupById(long id, string fields, bool isPlanish);
        #endregion

        #region LBItemRange

        [ServiceContractDescription(Name = "新增LB_ItemRange", Desc = "新增LB_ItemRange", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBItemRange", Get = "", Post = "LBItemRange", Return = "BaseResultDataValue", ReturnType = "LBItemRange")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBItemRange", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBItemRange(LBItemRange entity);

        [ServiceContractDescription(Name = "修改LB_ItemRange指定的属性", Desc = "修改LB_ItemRange指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemRangeByField", Get = "", Post = "LBItemRange", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBItemRangeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBItemRangeByField(LBItemRange entity, string fields);

        [ServiceContractDescription(Name = "删除LB_ItemRange", Desc = "删除LB_ItemRange", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBItemRange?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBItemRange?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBItemRange(long id);

        [ServiceContractDescription(Name = "查询LB_ItemRange(HQL)", Desc = "查询LB_ItemRange(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemRangeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemRange>", ReturnType = "ListLBItemRange")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemRangeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemRangeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_ItemRange", Desc = "通过主键ID查询LB_ItemRange", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemRangeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemRange>", ReturnType = "LBItemRange")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemRangeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemRangeById(long id, string fields, bool isPlanish);
        #endregion

        #region LBItemRangeExp

        [ServiceContractDescription(Name = "新增LB_ItemRangeExp", Desc = "新增LB_ItemRangeExp", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBItemRangeExp", Get = "", Post = "LBItemRangeExp", Return = "BaseResultDataValue", ReturnType = "LBItemRangeExp")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBItemRangeExp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBItemRangeExp(LBItemRangeExp entity);

        [ServiceContractDescription(Name = "修改LB_ItemRangeExp指定的属性", Desc = "修改LB_ItemRangeExp指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemRangeExpByField", Get = "", Post = "LBItemRangeExp", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBItemRangeExpByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBItemRangeExpByField(LBItemRangeExp entity, string fields);

        [ServiceContractDescription(Name = "删除LB_ItemRangeExp", Desc = "删除LB_ItemRangeExp", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBItemRangeExp?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBItemRangeExp?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBItemRangeExp(long id);

        [ServiceContractDescription(Name = "查询LB_ItemRangeExp(HQL)", Desc = "查询LB_ItemRangeExp(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemRangeExpByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemRangeExp>", ReturnType = "ListLBItemRangeExp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemRangeExpByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemRangeExpByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_ItemRangeExp", Desc = "通过主键ID查询LB_ItemRangeExp", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemRangeExpById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemRangeExp>", ReturnType = "LBItemRangeExp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemRangeExpById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemRangeExpById(long id, string fields, bool isPlanish);
        #endregion

        #region LBItemTimeW

        [ServiceContractDescription(Name = "新增LB_ItemTimeW", Desc = "新增LB_ItemTimeW", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBItemTimeW", Get = "", Post = "LBItemTimeW", Return = "BaseResultDataValue", ReturnType = "LBItemTimeW")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBItemTimeW", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBItemTimeW(LBItemTimeW entity);

        [ServiceContractDescription(Name = "修改LB_ItemTimeW指定的属性", Desc = "修改LB_ItemTimeW指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemTimeWByField", Get = "", Post = "LBItemTimeW", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBItemTimeWByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBItemTimeWByField(LBItemTimeW entity, string fields);

        [ServiceContractDescription(Name = "删除LB_ItemTimeW", Desc = "删除LB_ItemTimeW", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBItemTimeW?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBItemTimeW?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBItemTimeW(long id);

        [ServiceContractDescription(Name = "查询LB_ItemTimeW(HQL)", Desc = "查询LB_ItemTimeW(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemTimeWByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemTimeW>", ReturnType = "ListLBItemTimeW")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemTimeWByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemTimeWByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_ItemTimeW", Desc = "通过主键ID查询LB_ItemTimeW", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemTimeWById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemTimeW>", ReturnType = "LBItemTimeW")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemTimeWById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemTimeWById(long id, string fields, bool isPlanish);
        #endregion

        #region LBItemExp

        [ServiceContractDescription(Name = "新增LB_ItemTimeW", Desc = "新增LB_ItemTimeW", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBItemExp", Get = "", Post = "LBItemExp", Return = "BaseResultDataValue", ReturnType = "LBItemExp")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBItemExp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBItemExp(LBItemExp entity);

        [ServiceContractDescription(Name = "修改LB_ItemTimeW指定的属性", Desc = "修改LB_ItemTimeW指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemExpByField", Get = "", Post = "LBItemExp", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBItemExpByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBItemExpByField(LBItemExp entity, string fields);

        [ServiceContractDescription(Name = "删除LB_ItemTimeW", Desc = "删除LB_ItemTimeW", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBItemExp?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBItemExp?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBItemExp(long id);

        [ServiceContractDescription(Name = "查询LB_ItemTimeW(HQL)", Desc = "查询LB_ItemTimeW(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemExpByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemExp>", ReturnType = "ListLBItemExp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemExpByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemExpByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_ItemTimeW", Desc = "通过主键ID查询LB_ItemTimeW", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemExpById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemExp>", ReturnType = "LBItemExp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemExpById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemExpById(long id, string fields, bool isPlanish);
        #endregion

        #region LBPhrase

        [ServiceContractDescription(Name = "新增LB_Phrase", Desc = "新增LB_Phrase", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBPhrase", Get = "", Post = "LBPhrase", Return = "BaseResultDataValue", ReturnType = "LBPhrase")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBPhrase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBPhrase(LBPhrase entity);

        [ServiceContractDescription(Name = "修改LB_Phrase指定的属性", Desc = "修改LB_Phrase指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBPhraseByField", Get = "", Post = "LBPhrase", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBPhraseByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBPhraseByField(LBPhrase entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Phrase", Desc = "删除LB_Phrase", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBPhrase?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBPhrase?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBPhrase(long id);

        [ServiceContractDescription(Name = "查询LB_Phrase(HQL)", Desc = "查询LB_Phrase(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBPhraseByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBPhrase>", ReturnType = "ListLBPhrase")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBPhraseByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBPhraseByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Phrase", Desc = "通过主键ID查询LB_Phrase", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBPhraseById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBPhrase>", ReturnType = "LBPhrase")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBPhraseById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBPhraseById(long id, string fields, bool isPlanish);
        #endregion

        #region LBPhyPeriod

        [ServiceContractDescription(Name = "新增LB_Destination", Desc = "新增LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBPhyPeriod", Get = "", Post = "LBPhyPeriod", Return = "BaseResultDataValue", ReturnType = "LBPhyPeriod")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBPhyPeriod", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBPhyPeriod(LBPhyPeriod entity);

        [ServiceContractDescription(Name = "修改LB_Destination指定的属性", Desc = "修改LB_Destination指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBPhyPeriodByField", Get = "", Post = "LBPhyPeriod", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBPhyPeriodByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBPhyPeriodByField(LBPhyPeriod entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Destination", Desc = "删除LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBPhyPeriod?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBPhyPeriod?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBPhyPeriod(long id);

        [ServiceContractDescription(Name = "查询LB_Destination(HQL)", Desc = "查询LB_Destination(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBPhyPeriodByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBPhyPeriod>", ReturnType = "ListLBPhyPeriod")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBPhyPeriodByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBPhyPeriodByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Destination", Desc = "通过主键ID查询LB_Destination", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBPhyPeriodById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBPhyPeriod>", ReturnType = "LBPhyPeriod")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBPhyPeriodById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBPhyPeriodById(long id, string fields, bool isPlanish);
        #endregion

        #region LBOrderModel

        [ServiceContractDescription(Name = "新增LB_Phrase", Desc = "新增LB_Phrase", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBOrderModel", Get = "", Post = "LBOrderModel", Return = "BaseResultDataValue", ReturnType = "LBOrderModel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBOrderModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBOrderModel(LBOrderModel entity);

        [ServiceContractDescription(Name = "修改LB_Phrase指定的属性", Desc = "修改LB_Phrase指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBOrderModelByField", Get = "", Post = "LBOrderModel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBOrderModelByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBOrderModelByField(LBOrderModel entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Phrase", Desc = "删除LB_Phrase", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBOrderModel?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBOrderModel?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBOrderModel(long id);

        [ServiceContractDescription(Name = "查询LB_Phrase(HQL)", Desc = "查询LB_Phrase(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBOrderModelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBOrderModel>", ReturnType = "ListLBOrderModel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBOrderModelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBOrderModelByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Phrase", Desc = "通过主键ID查询LB_Phrase", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBOrderModelById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBOrderModel>", ReturnType = "LBOrderModel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBOrderModelById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBOrderModelById(long id, string fields, bool isPlanish);
        #endregion

        #region LBOrderModelItem

        [ServiceContractDescription(Name = "新增LB_Phrase", Desc = "新增LB_Phrase", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBOrderModelItem", Get = "", Post = "LBOrderModelItem", Return = "BaseResultDataValue", ReturnType = "LBOrderModelItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBOrderModelItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBOrderModelItem(LBOrderModelItem entity);

        [ServiceContractDescription(Name = "修改LB_Phrase指定的属性", Desc = "修改LB_Phrase指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBOrderModelItemByField", Get = "", Post = "LBOrderModelItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBOrderModelItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBOrderModelItemByField(LBOrderModelItem entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Phrase", Desc = "删除LB_Phrase", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBOrderModelItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBOrderModelItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBOrderModelItem(long id);

        [ServiceContractDescription(Name = "查询LB_Phrase(HQL)", Desc = "查询LB_Phrase(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBOrderModelItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBOrderModelItem>", ReturnType = "ListLBOrderModelItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBOrderModelItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBOrderModelItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Phrase", Desc = "通过主键ID查询LB_Phrase", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBOrderModelItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBOrderModelItem>", ReturnType = "LBOrderModelItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBOrderModelItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBOrderModelItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LBSampleType

        [ServiceContractDescription(Name = "新增LB_SampleType", Desc = "新增LB_SampleType", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBSampleType", Get = "", Post = "LBSampleType", Return = "BaseResultDataValue", ReturnType = "LBSampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBSampleType(LBSampleType entity);

        [ServiceContractDescription(Name = "修改LB_SampleType指定的属性", Desc = "修改LB_SampleType指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBSampleTypeByField", Get = "", Post = "LBSampleType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBSampleTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBSampleTypeByField(LBSampleType entity, string fields);

        [ServiceContractDescription(Name = "删除LB_SampleType", Desc = "删除LB_SampleType", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBSampleType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBSampleType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBSampleType(long id);

        [ServiceContractDescription(Name = "查询LB_SampleType(HQL)", Desc = "查询LB_SampleType(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSampleType>", ReturnType = "ListLBSampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSampleTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_SampleType", Desc = "通过主键ID查询LB_SampleType", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSampleType>", ReturnType = "LBSampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSampleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSampleTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region LBSection

        [ServiceContractDescription(Name = "新增LB_Section", Desc = "新增LB_Section", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBSection", Get = "", Post = "LBSection", Return = "BaseResultDataValue", ReturnType = "LBSection")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBSection", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBSection(LBSection entity);

        [ServiceContractDescription(Name = "修改LB_Section指定的属性", Desc = "修改LB_Section指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBSectionByField", Get = "", Post = "LBSection", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBSectionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBSectionByField(LBSection entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Section", Desc = "删除LB_Section", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBSection?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBSection?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBSection(long id);

        [ServiceContractDescription(Name = "查询LB_Section(HQL)", Desc = "查询LB_Section(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSection>", ReturnType = "ListLBSection")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSectionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSectionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Section", Desc = "通过主键ID查询LB_Section", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSection>", ReturnType = "LBSection")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSectionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSectionById(long id, string fields, bool isPlanish);
        #endregion

        #region LBSectionItem

        [ServiceContractDescription(Name = "新增LB_SectionItem", Desc = "新增LB_SectionItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBSectionItem", Get = "", Post = "LBSectionItem", Return = "BaseResultDataValue", ReturnType = "LBSectionItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBSectionItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBSectionItem(LBSectionItem entity);

        [ServiceContractDescription(Name = "修改LB_SectionItem指定的属性", Desc = "修改LB_SectionItem指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBSectionItemByField", Get = "", Post = "LBSectionItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBSectionItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBSectionItemByField(LBSectionItem entity, string fields);

        [ServiceContractDescription(Name = "删除LB_SectionItem", Desc = "删除LB_SectionItem", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBSectionItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBSectionItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBSectionItem(long id);

        [ServiceContractDescription(Name = "查询LB_SectionItem(HQL)", Desc = "查询LB_SectionItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSectionItem>", ReturnType = "ListLBSectionItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSectionItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSectionItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_SectionItem", Desc = "通过主键ID查询LB_SectionItem", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSectionItem>", ReturnType = "LBSectionItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSectionItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSectionItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LBSectionPrint

        [ServiceContractDescription(Name = "新增LB_SectionItem", Desc = "新增LB_SectionItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBSectionPrint", Get = "", Post = "LBSectionPrint", Return = "BaseResultDataValue", ReturnType = "LBSectionPrint")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBSectionPrint", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBSectionPrint(LBSectionPrint entity);

        [ServiceContractDescription(Name = "修改LB_SectionItem指定的属性", Desc = "修改LB_SectionItem指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBSectionPrintByField", Get = "", Post = "LBSectionPrint", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBSectionPrintByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBSectionPrintByField(LBSectionPrint entity, string fields);

        [ServiceContractDescription(Name = "删除LB_SectionItem", Desc = "删除LB_SectionItem", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBSectionPrint?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBSectionPrint?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBSectionPrint(long id);

        [ServiceContractDescription(Name = "查询LB_SectionItem(HQL)", Desc = "查询LB_SectionItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionPrintByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSectionPrint>", ReturnType = "ListLBSectionPrint")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSectionPrintByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSectionPrintByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_SectionItem", Desc = "通过主键ID查询LB_SectionItem", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionPrintById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSectionPrint>", ReturnType = "LBSectionPrint")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSectionPrintById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSectionPrintById(long id, string fields, bool isPlanish);
        #endregion

        #region LBSectionHisComp

        [ServiceContractDescription(Name = "新增LB_SectionItem", Desc = "新增LB_SectionItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBSectionHisComp", Get = "", Post = "LBSectionHisComp", Return = "BaseResultDataValue", ReturnType = "LBSectionHisComp")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBSectionHisComp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBSectionHisComp(LBSectionHisComp entity);

        [ServiceContractDescription(Name = "修改LB_SectionItem指定的属性", Desc = "修改LB_SectionItem指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBSectionHisCompByField", Get = "", Post = "LBSectionHisComp", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBSectionHisCompByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBSectionHisCompByField(LBSectionHisComp entity, string fields);

        [ServiceContractDescription(Name = "删除LB_SectionItem", Desc = "删除LB_SectionItem", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBSectionHisComp?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBSectionHisComp?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBSectionHisComp(long id);

        [ServiceContractDescription(Name = "查询LB_SectionItem(HQL)", Desc = "查询LB_SectionItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionHisCompByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSectionHisComp>", ReturnType = "ListLBSectionHisComp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSectionHisCompByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSectionHisCompByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_SectionItem", Desc = "通过主键ID查询LB_SectionItem", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionHisCompById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSectionHisComp>", ReturnType = "LBSectionHisComp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSectionHisCompById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSectionHisCompById(long id, string fields, bool isPlanish);
        #endregion

        #region LBSickType

        [ServiceContractDescription(Name = "新增LB_SickType", Desc = "新增LB_SickType", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBSickType", Get = "", Post = "LBSickType", Return = "BaseResultDataValue", ReturnType = "LBSickType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBSickType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBSickType(LBSickType entity);

        [ServiceContractDescription(Name = "修改LB_SickType指定的属性", Desc = "修改LB_SickType指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBSickTypeByField", Get = "", Post = "LBSickType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBSickTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBSickTypeByField(LBSickType entity, string fields);

        [ServiceContractDescription(Name = "删除LB_SickType", Desc = "删除LB_SickType", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBSickType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBSickType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBSickType(long id);

        [ServiceContractDescription(Name = "查询LB_SickType(HQL)", Desc = "查询LB_SickType(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSickType>", ReturnType = "ListLBSickType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSickTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSickTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_SickType", Desc = "通过主键ID查询LB_SickType", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSickType>", ReturnType = "LBSickType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSickTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSickTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region LBSpecialty

        [ServiceContractDescription(Name = "新增LB_Specialty", Desc = "新增LB_Specialty", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBSpecialty", Get = "", Post = "LBSpecialty", Return = "BaseResultDataValue", ReturnType = "LBSpecialty")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBSpecialty", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBSpecialty(LBSpecialty entity);

        [ServiceContractDescription(Name = "修改LB_Specialty指定的属性", Desc = "修改LB_Specialty指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBSpecialtyByField", Get = "", Post = "LBSpecialty", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBSpecialtyByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBSpecialtyByField(LBSpecialty entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Specialty", Desc = "删除LB_Specialty", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBSpecialty?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBSpecialty?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBSpecialty(long id);

        [ServiceContractDescription(Name = "查询LB_Specialty(HQL)", Desc = "查询LB_Specialty(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSpecialtyByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSpecialty>", ReturnType = "ListLBSpecialty")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSpecialtyByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSpecialtyByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Specialty", Desc = "通过主键ID查询LB_Specialty", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSpecialtyById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSpecialty>", ReturnType = "LBSpecialty")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSpecialtyById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSpecialtyById(long id, string fields, bool isPlanish);
        #endregion

        #region LBRight

        [ServiceContractDescription(Name = "新增LB_GetReportDate", Desc = "新增LB_GetReportDate", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBRight", Get = "", Post = "LBRight", Return = "BaseResultDataValue", ReturnType = "LBRight")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBRight", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBRight(LBRight entity);

        [ServiceContractDescription(Name = "修改LB_GetReportDate指定的属性", Desc = "修改LB_GetReportDate指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBRightByField", Get = "", Post = "LBRight", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBRightByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBRightByField(LBRight entity, string fields);

        [ServiceContractDescription(Name = "删除LB_GetReportDate", Desc = "删除LB_GetReportDate", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBRight?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBRight?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBRight(long id);

        [ServiceContractDescription(Name = "查询LB_GetReportDate(HQL)", Desc = "查询LB_GetReportDate(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBRightByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBRight>", ReturnType = "ListLBRight")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBRightByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBRightByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_GetReportDate", Desc = "通过主键ID查询LB_GetReportDate", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBRightById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBRight>", ReturnType = "LBRight")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBRightById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBRightById(long id, string fields, bool isPlanish);
        #endregion

        #region LBReportDate

        [ServiceContractDescription(Name = "新增LB_GetReportDate", Desc = "新增LB_GetReportDate", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBReportDate", Get = "", Post = "LBReportDate", Return = "BaseResultDataValue", ReturnType = "LBReportDate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBReportDate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBReportDate(LBReportDate entity);

        [ServiceContractDescription(Name = "修改LB_GetReportDate指定的属性", Desc = "修改LB_GetReportDate指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBReportDateByField", Get = "", Post = "LBReportDate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBReportDateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBReportDateByField(LBReportDate entity, string fields);

        [ServiceContractDescription(Name = "删除LB_GetReportDate", Desc = "删除LB_GetReportDate", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBReportDate?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBReportDate?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBReportDate(long id);

        [ServiceContractDescription(Name = "查询LB_GetReportDate(HQL)", Desc = "查询LB_GetReportDate(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBReportDate>", ReturnType = "ListLBReportDate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBReportDateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBReportDateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_GetReportDate", Desc = "通过主键ID查询LB_GetReportDate", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBReportDate>", ReturnType = "LBReportDate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBReportDateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBReportDateById(long id, string fields, bool isPlanish);
        #endregion

        #region LBReportDateItem

        [ServiceContractDescription(Name = "新增LB_ReportDateItem", Desc = "新增LB_ReportDateItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBReportDateItem", Get = "", Post = "LBReportDateItem", Return = "BaseResultDataValue", ReturnType = "LBReportDateItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBReportDateItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBReportDateItem(LBReportDateItem entity);

        [ServiceContractDescription(Name = "修改LB_ReportDateItem指定的属性", Desc = "修改LB_ReportDateItem指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBReportDateItemByField", Get = "", Post = "LBReportDateItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBReportDateItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBReportDateItemByField(LBReportDateItem entity, string fields);

        [ServiceContractDescription(Name = "删除LB_ReportDateItem", Desc = "删除LB_ReportDateItem", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBReportDateItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBReportDateItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBReportDateItem(long id);

        [ServiceContractDescription(Name = "查询LB_ReportDateItem(HQL)", Desc = "查询LB_ReportDateItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBReportDateItem>", ReturnType = "ListLBReportDateItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBReportDateItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBReportDateItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_ReportDateItem", Desc = "通过主键ID查询LB_ReportDateItem", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBReportDateItem>", ReturnType = "LBReportDateItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBReportDateItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBReportDateItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LBReportDateRule

        [ServiceContractDescription(Name = "新增LB_GetReportDateItem", Desc = "新增LB_GetReportDateItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBReportDateRule", Get = "", Post = "LBReportDateRule", Return = "BaseResultDataValue", ReturnType = "LBReportDateRule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBReportDateRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBReportDateRule(LBReportDateRule entity);

        [ServiceContractDescription(Name = "修改LB_GetReportDateItem指定的属性", Desc = "修改LB_GetReportDateItem指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBReportDateRuleByField", Get = "", Post = "LBReportDateRule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBReportDateRuleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBReportDateRuleByField(LBReportDateRule entity, string fields);

        [ServiceContractDescription(Name = "删除LB_GetReportDateItem", Desc = "删除LB_GetReportDateItem", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBReportDateRule?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBReportDateRule?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBReportDateRule(long id);

        [ServiceContractDescription(Name = "查询LB_GetReportDateItem(HQL)", Desc = "查询LB_GetReportDateItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBReportDateRule>", ReturnType = "ListLBReportDateRule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBReportDateRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBReportDateRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_GetReportDateItem", Desc = "通过主键ID查询LB_GetReportDateItem", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBReportDateRule>", ReturnType = "LBReportDateRule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBReportDateRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBReportDateRuleById(long id, string fields, bool isPlanish);
        #endregion

        #region LBParItemSplit

        [ServiceContractDescription(Name = "新增LB_ParItemSplit", Desc = "新增LB_ParItemSplit", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBParItemSplit", Get = "", Post = "LBParItemSplit", Return = "BaseResultDataValue", ReturnType = "LBParItemSplit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBParItemSplit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBParItemSplit(LBParItemSplit entity);

        [ServiceContractDescription(Name = "修改LB_ParItemSplit指定的属性", Desc = "修改LB_ParItemSplit指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBParItemSplitByField", Get = "", Post = "LBParItemSplit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBParItemSplitByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBParItemSplitByField(LBParItemSplit entity, string fields);

        [ServiceContractDescription(Name = "删除LB_ParItemSplit", Desc = "删除LB_ParItemSplit", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBParItemSplit?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBParItemSplit?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBParItemSplit(long id);

        [ServiceContractDescription(Name = "查询LB_ParItemSplit(HQL)", Desc = "查询LB_ParItemSplit(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBParItemSplitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBParItemSplit>", ReturnType = "ListLBParItemSplit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBParItemSplitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBParItemSplitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_ParItemSplit", Desc = "通过主键ID查询LB_ParItemSplit", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBParItemSplitById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBParItemSplit>", ReturnType = "LBParItemSplit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBParItemSplitById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBParItemSplitById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "新增组合项目拆分时,根据组合项目ID获取到该组合项目所有子项目及子项目的所属采样组信息", Desc = "新增组合项目拆分时,根据组合项目ID获取到该组合项目所有子项目及子项目的所属采样组信息", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchAddLBParItemSplitListByParItemId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&parItemId={parItemId}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&parItemId={parItemId}", Post = "", Return = "BaseResultList<LBParItemSplit>", ReturnType = "ListLBParItemSplit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchAddLBParItemSplitListByParItemId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&parItemId={parItemId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchAddLBParItemSplitListByParItemId(int page, int limit, string fields, string where, string sort, bool isPlanish, string parItemId);

        [ServiceContractDescription(Name = "获取某一组合项目编辑的组合项目拆分信息(HQL)", Desc = "获取某一组合项目编辑的组合项目拆分信息(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchEditLBParItemSplitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBParItemSplit>", ReturnType = "ListLBParItemSplit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchEditLBParItemSplitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchEditLBParItemSplitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "定制新增保存组合项目拆分", Desc = "定制新增保存组合项目拆分", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBParItemSplitList", Get = "", Post = "IList<LBParItemSplit>", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBParItemSplitList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBParItemSplitList(IList<LBParItemSplit> entityList);

        [ServiceContractDescription(Name = "定制编辑保存组合项目拆分", Desc = "定制编辑保存组合项目拆分", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBParItemSplitList", Get = "", Post = "IList<LBParItemSplit>", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBParItemSplitList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBParItemSplitList(IList<LBParItemSplit> entityList);

        [ServiceContractDescription(Name = "定制按组合项目ID删除组合项目拆分关系", Desc = "定制按组合项目ID删除组合项目拆分关系", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBParItemSplitByParItemId?parItemId={parItemId}", Get = "parItemId={parItemId}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBParItemSplitByParItemId?parItemId={parItemId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBParItemSplitByParItemId(long parItemId);

        #endregion

        #region LBPhrasesWatch

        [ServiceContractDescription(Name = "新增LB_PhrasesWatch", Desc = "新增LB_PhrasesWatch", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBPhrasesWatch", Get = "", Post = "LBPhrasesWatch", Return = "BaseResultDataValue", ReturnType = "LBPhrasesWatch")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBPhrasesWatch", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBPhrasesWatch(LBPhrasesWatch entity);

        [ServiceContractDescription(Name = "修改LB_PhrasesWatch指定的属性", Desc = "修改LB_PhrasesWatch指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBPhrasesWatchByField", Get = "", Post = "LBPhrasesWatch", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBPhrasesWatchByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBPhrasesWatchByField(LBPhrasesWatch entity, string fields);

        [ServiceContractDescription(Name = "删除LB_PhrasesWatch", Desc = "删除LB_PhrasesWatch", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBPhrasesWatch?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBPhrasesWatch?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBPhrasesWatch(long id);

        [ServiceContractDescription(Name = "查询LB_PhrasesWatch(HQL)", Desc = "查询LB_PhrasesWatch(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBPhrasesWatchByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBPhrasesWatch>", ReturnType = "ListLBPhrasesWatch")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBPhrasesWatchByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBPhrasesWatchByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_PhrasesWatch", Desc = "通过主键ID查询LB_PhrasesWatch", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBPhrasesWatchById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBPhrasesWatch>", ReturnType = "LBPhrasesWatch")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBPhrasesWatchById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBPhrasesWatchById(long id, string fields, bool isPlanish);
        #endregion

        #region LBPhrasesWatchClass

        [ServiceContractDescription(Name = "新增LB_PhrasesWatchClass", Desc = "新增LB_PhrasesWatchClass", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBPhrasesWatchClass", Get = "", Post = "LBPhrasesWatchClass", Return = "BaseResultDataValue", ReturnType = "LBPhrasesWatchClass")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBPhrasesWatchClass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBPhrasesWatchClass(LBPhrasesWatchClass entity);

        [ServiceContractDescription(Name = "修改LB_PhrasesWatchClass指定的属性", Desc = "修改LB_PhrasesWatchClass指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBPhrasesWatchClassByField", Get = "", Post = "LBPhrasesWatchClass", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBPhrasesWatchClassByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBPhrasesWatchClassByField(LBPhrasesWatchClass entity, string fields);

        [ServiceContractDescription(Name = "删除LB_PhrasesWatchClass", Desc = "删除LB_PhrasesWatchClass", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBPhrasesWatchClass?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBPhrasesWatchClass?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBPhrasesWatchClass(long id);

        [ServiceContractDescription(Name = "查询LB_PhrasesWatchClass(HQL)", Desc = "查询LB_PhrasesWatchClass(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBPhrasesWatchClassByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBPhrasesWatchClass>", ReturnType = "ListLBPhrasesWatchClass")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBPhrasesWatchClassByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBPhrasesWatchClassByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_PhrasesWatchClass", Desc = "通过主键ID查询LB_PhrasesWatchClass", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBPhrasesWatchClassById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBPhrasesWatchClass>", ReturnType = "LBPhrasesWatchClass")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBPhrasesWatchClassById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBPhrasesWatchClassById(long id, string fields, bool isPlanish);
        #endregion

        #region LBPhrasesWatchClassItem

        [ServiceContractDescription(Name = "新增LB_PhrasesWatchClassItem", Desc = "新增LB_PhrasesWatchClassItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBPhrasesWatchClassItem", Get = "", Post = "LBPhrasesWatchClassItem", Return = "BaseResultDataValue", ReturnType = "LBPhrasesWatchClassItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBPhrasesWatchClassItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBPhrasesWatchClassItem(LBPhrasesWatchClassItem entity);

        [ServiceContractDescription(Name = "修改LB_PhrasesWatchClassItem指定的属性", Desc = "修改LB_PhrasesWatchClassItem指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBPhrasesWatchClassItemByField", Get = "", Post = "LBPhrasesWatchClassItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBPhrasesWatchClassItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBPhrasesWatchClassItemByField(LBPhrasesWatchClassItem entity, string fields);

        [ServiceContractDescription(Name = "删除LB_PhrasesWatchClassItem", Desc = "删除LB_PhrasesWatchClassItem", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBPhrasesWatchClassItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBPhrasesWatchClassItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBPhrasesWatchClassItem(long id);

        [ServiceContractDescription(Name = "查询LB_PhrasesWatchClassItem(HQL)", Desc = "查询LB_PhrasesWatchClassItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBPhrasesWatchClassItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBPhrasesWatchClassItem>", ReturnType = "ListLBPhrasesWatchClassItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBPhrasesWatchClassItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBPhrasesWatchClassItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_PhrasesWatchClassItem", Desc = "通过主键ID查询LB_PhrasesWatchClassItem", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBPhrasesWatchClassItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBPhrasesWatchClassItem>", ReturnType = "LBPhrasesWatchClassItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBPhrasesWatchClassItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBPhrasesWatchClassItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LBSampleItem
        [ServiceContractDescription(Name = "按选择的样本类型或按选择的检验项目,新增LB_SampleItem", Desc = "按选择的样本类型或按选择的检验项目,新增LB_SampleItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBSampleItemList", Get = "", Post = "IList<LBSampleItem>", Return = "BaseResultDataValue", ReturnType = "LBSampleItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBSampleItemList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBSampleItemList(IList<LBSampleItem> entityList, string ofType, bool isHasDel);

        [ServiceContractDescription(Name = "新增LB_SampleItem", Desc = "新增LB_SampleItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBSampleItem", Get = "", Post = "LBSampleItem", Return = "BaseResultDataValue", ReturnType = "LBSampleItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBSampleItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBSampleItem(LBSampleItem entity);

        [ServiceContractDescription(Name = "修改LB_SampleItem指定的属性", Desc = "修改LB_SampleItem指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBSampleItemByField", Get = "", Post = "LBSampleItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBSampleItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBSampleItemByField(LBSampleItem entity, string fields);

        [ServiceContractDescription(Name = "删除LB_SampleItem", Desc = "删除LB_SampleItem", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBSampleItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBSampleItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBSampleItem(long id);

        [ServiceContractDescription(Name = "查询LB_SampleItem(HQL)", Desc = "查询LB_SampleItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSampleItem>", ReturnType = "ListLBSampleItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSampleItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSampleItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_SampleItem", Desc = "通过主键ID查询LB_SampleItem", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSampleItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSampleItem>", ReturnType = "LBSampleItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSampleItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSampleItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LBSamplingChargeItem

        [ServiceContractDescription(Name = "新增LB_SamplingChargeItem", Desc = "新增LB_SamplingChargeItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBSamplingChargeItem", Get = "", Post = "LBSamplingChargeItem", Return = "BaseResultDataValue", ReturnType = "LBSamplingChargeItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBSamplingChargeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBSamplingChargeItem(LBSamplingChargeItem entity);

        [ServiceContractDescription(Name = "修改LB_SamplingChargeItem指定的属性", Desc = "修改LB_SamplingChargeItem指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBSamplingChargeItemByField", Get = "", Post = "LBSamplingChargeItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBSamplingChargeItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBSamplingChargeItemByField(LBSamplingChargeItem entity, string fields);

        [ServiceContractDescription(Name = "删除LB_SamplingChargeItem", Desc = "删除LB_SamplingChargeItem", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBSamplingChargeItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBSamplingChargeItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBSamplingChargeItem(long id);

        [ServiceContractDescription(Name = "查询LB_SamplingChargeItem(HQL)", Desc = "查询LB_SamplingChargeItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSamplingChargeItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSamplingChargeItem>", ReturnType = "ListLBSamplingChargeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSamplingChargeItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSamplingChargeItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_SamplingChargeItem", Desc = "通过主键ID查询LB_SamplingChargeItem", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSamplingChargeItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSamplingChargeItem>", ReturnType = "LBSamplingChargeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSamplingChargeItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSamplingChargeItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LBSamplingGroup

        [ServiceContractDescription(Name = "新增LB_SamplingGroup", Desc = "新增LB_SamplingGroup", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBSamplingGroup", Get = "", Post = "LBSamplingGroup", Return = "BaseResultDataValue", ReturnType = "LBSamplingGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBSamplingGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBSamplingGroup(LBSamplingGroup entity);

        [ServiceContractDescription(Name = "修改LB_SamplingGroup指定的属性", Desc = "修改LB_SamplingGroup指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBSamplingGroupByField", Get = "", Post = "LBSamplingGroup", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBSamplingGroupByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBSamplingGroupByField(LBSamplingGroup entity, string fields);

        [ServiceContractDescription(Name = "删除LB_SamplingGroup", Desc = "删除LB_SamplingGroup", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBSamplingGroup?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBSamplingGroup?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBSamplingGroup(long id);

        [ServiceContractDescription(Name = "查询LB_SamplingGroup(HQL)", Desc = "查询LB_SamplingGroup(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSamplingGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSamplingGroup>", ReturnType = "ListLBSamplingGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSamplingGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSamplingGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_SamplingGroup", Desc = "通过主键ID查询LB_SamplingGroup", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSamplingGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSamplingGroup>", ReturnType = "LBSamplingGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSamplingGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSamplingGroupById(long id, string fields, bool isPlanish);
        #endregion

        #region LBSamplingItem

        [ServiceContractDescription(Name = "新增LB_SamplingItem", Desc = "新增LB_SamplingItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBSamplingItem", Get = "", Post = "LBSamplingItem", Return = "BaseResultDataValue", ReturnType = "LBSamplingItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBSamplingItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBSamplingItem(LBSamplingItem entity);

        [ServiceContractDescription(Name = "修改LB_SamplingItem指定的属性", Desc = "修改LB_SamplingItem指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBSamplingItemByField", Get = "", Post = "LBSamplingItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBSamplingItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBSamplingItemByField(LBSamplingItem entity, string fields);

        [ServiceContractDescription(Name = "删除LB_SamplingItem", Desc = "删除LB_SamplingItem", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBSamplingItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBSamplingItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBSamplingItem(long id);

        [ServiceContractDescription(Name = "查询LB_SamplingItem(HQL)", Desc = "查询LB_SamplingItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSamplingItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSamplingItem>", ReturnType = "ListLBSamplingItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSamplingItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSamplingItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_SamplingItem", Desc = "通过主键ID查询LB_SamplingItem", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSamplingItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSamplingItem>", ReturnType = "LBSamplingItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSamplingItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSamplingItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LBTcuvete

        [ServiceContractDescription(Name = "新增LB_Tcuvete", Desc = "新增LB_Tcuvete", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBTcuvete", Get = "", Post = "LBTcuvete", Return = "BaseResultDataValue", ReturnType = "LBTcuvete")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBTcuvete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBTcuvete(LBTcuvete entity);

        [ServiceContractDescription(Name = "修改LB_Tcuvete指定的属性", Desc = "修改LB_Tcuvete指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBTcuveteByField", Get = "", Post = "LBTcuvete", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBTcuveteByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBTcuveteByField(LBTcuvete entity, string fields);

        [ServiceContractDescription(Name = "删除LB_Tcuvete", Desc = "删除LB_Tcuvete", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBTcuvete?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBTcuvete?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBTcuvete(long id);

        [ServiceContractDescription(Name = "查询LB_Tcuvete(HQL)", Desc = "查询LB_Tcuvete(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBTcuveteByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBTcuvete>", ReturnType = "ListLBTcuvete")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBTcuveteByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBTcuveteByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_Tcuvete", Desc = "通过主键ID查询LB_Tcuvete", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBTcuveteById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBTcuvete>", ReturnType = "LBTcuvete")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBTcuveteById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBTcuveteById(long id, string fields, bool isPlanish);
        #endregion

        #region LBTranRule

        [ServiceContractDescription(Name = "新增LB_TranRule", Desc = "新增LB_TranRule", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBTranRule", Get = "", Post = "LBTranRule", Return = "BaseResultDataValue", ReturnType = "LBTranRule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBTranRule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBTranRule(LBTranRule entity);

        [ServiceContractDescription(Name = "修改LB_TranRule指定的属性", Desc = "修改LB_TranRule指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBTranRuleByField", Get = "", Post = "LBTranRule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBTranRuleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBTranRuleByField(LBTranRule entity, string fields);

        [ServiceContractDescription(Name = "删除LB_TranRule", Desc = "删除LB_TranRule", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBTranRule?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBTranRule?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBTranRule(long id);

        [ServiceContractDescription(Name = "查询LB_TranRule(HQL)", Desc = "查询LB_TranRule(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBTranRule>", ReturnType = "ListLBTranRule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBTranRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBTranRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_TranRule", Desc = "通过主键ID查询LB_TranRule", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBTranRule>", ReturnType = "LBTranRule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBTranRuleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBTranRuleById(long id, string fields, bool isPlanish);
        #endregion

        #region LBTranRuleItem

        [ServiceContractDescription(Name = "新增LB_TranRuleItem", Desc = "新增LB_TranRuleItem", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBTranRuleItem", Get = "", Post = "LBTranRuleItem", Return = "BaseResultDataValue", ReturnType = "LBTranRuleItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBTranRuleItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBTranRuleItem(LBTranRuleItem entity);

        [ServiceContractDescription(Name = "修改LB_TranRuleItem指定的属性", Desc = "修改LB_TranRuleItem指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBTranRuleItemByField", Get = "", Post = "LBTranRuleItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBTranRuleItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBTranRuleItemByField(LBTranRuleItem entity, string fields);

        [ServiceContractDescription(Name = "删除LB_TranRuleItem", Desc = "删除LB_TranRuleItem", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBTranRuleItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBTranRuleItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBTranRuleItem(long id);

        [ServiceContractDescription(Name = "查询LB_TranRuleItem(HQL)", Desc = "查询LB_TranRuleItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBTranRuleItem>", ReturnType = "ListLBTranRuleItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBTranRuleItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBTranRuleItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_TranRuleItem", Desc = "通过主键ID查询LB_TranRuleItem", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBTranRuleItem>", ReturnType = "LBTranRuleItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBTranRuleItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBTranRuleItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LBTranRuleType

        [ServiceContractDescription(Name = "新增LB_TranRuleType", Desc = "新增LB_TranRuleType", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBTranRuleType", Get = "", Post = "LBTranRuleType", Return = "BaseResultDataValue", ReturnType = "LBTranRuleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBTranRuleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBTranRuleType(LBTranRuleType entity);

        [ServiceContractDescription(Name = "修改LB_TranRuleType指定的属性", Desc = "修改LB_TranRuleType指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBTranRuleTypeByField", Get = "", Post = "LBTranRuleType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBTranRuleTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBTranRuleTypeByField(LBTranRuleType entity, string fields);

        [ServiceContractDescription(Name = "删除LB_TranRuleType", Desc = "删除LB_TranRuleType", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBTranRuleType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBTranRuleType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBTranRuleType(long id);

        [ServiceContractDescription(Name = "查询LB_TranRuleType(HQL)", Desc = "查询LB_TranRuleType(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBTranRuleType>", ReturnType = "ListLBTranRuleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBTranRuleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBTranRuleTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_TranRuleType", Desc = "通过主键ID查询LB_TranRuleType", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBTranRuleType>", ReturnType = "LBTranRuleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBTranRuleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBTranRuleTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region LBDicCodeLink

        [ServiceContractDescription(Name = "新增LB_DicCodeLink", Desc = "新增LB_DicCodeLink", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBDicCodeLink", Get = "", Post = "LBDicCodeLink", Return = "BaseResultDataValue", ReturnType = "LBDicCodeLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBDicCodeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBDicCodeLink(LBDicCodeLink entity);

        [ServiceContractDescription(Name = "修改LB_DicCodeLink指定的属性", Desc = "修改LB_DicCodeLink指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBDicCodeLinkByField", Get = "", Post = "LBDicCodeLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBDicCodeLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBDicCodeLinkByField(LBDicCodeLink entity, string fields);

        [ServiceContractDescription(Name = "删除LB_DicCodeLink", Desc = "删除LB_DicCodeLink", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBDicCodeLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBDicCodeLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBDicCodeLink(long id);

        [ServiceContractDescription(Name = "查询LB_DicCodeLink(HQL)", Desc = "查询LB_DicCodeLink(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBDicCodeLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBDicCodeLink>", ReturnType = "ListLBDicCodeLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBDicCodeLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBDicCodeLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_DicCodeLink", Desc = "通过主键ID查询LB_DicCodeLink", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBDicCodeLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBDicCodeLink>", ReturnType = "LBDicCodeLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBDicCodeLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBDicCodeLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region LBItemCodeLink

        [ServiceContractDescription(Name = "新增LB_ItemCodeLink", Desc = "新增LB_ItemCodeLink", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBItemCodeLink", Get = "", Post = "LBItemCodeLink", Return = "BaseResultDataValue", ReturnType = "LBItemCodeLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBItemCodeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBItemCodeLink(LBItemCodeLink entity);

        [ServiceContractDescription(Name = "修改LB_ItemCodeLink指定的属性", Desc = "修改LB_ItemCodeLink指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBItemCodeLinkByField", Get = "", Post = "LBItemCodeLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBItemCodeLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBItemCodeLinkByField(LBItemCodeLink entity, string fields);

        [ServiceContractDescription(Name = "删除LB_ItemCodeLink", Desc = "删除LB_ItemCodeLink", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBItemCodeLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBItemCodeLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBItemCodeLink(long id);

        [ServiceContractDescription(Name = "查询LB_ItemCodeLink(HQL)", Desc = "查询LB_ItemCodeLink(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCodeLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemCodeLink>", ReturnType = "ListLBItemCodeLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemCodeLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemCodeLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_ItemCodeLink", Desc = "通过主键ID查询LB_ItemCodeLink", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemCodeLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItemCodeLink>", ReturnType = "LBItemCodeLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemCodeLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemCodeLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region BPrintModel

        [ServiceContractDescription(Name = "新增B_PrintModel", Desc = "新增B_PrintModel", Url = "LabStarBaseTableService.svc/LS_UDTO_AddBPrintModel", Get = "", Post = "BPrintModel", Return = "BaseResultDataValue", ReturnType = "BPrintModel")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddBPrintModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddBPrintModel(BPrintModel entity);

        [ServiceContractDescription(Name = "修改B_PrintModel指定的属性", Desc = "修改B_PrintModel指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateBPrintModelByField", Get = "", Post = "BPrintModel", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateBPrintModelByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateBPrintModelByField(BPrintModel entity, string fields);

        [ServiceContractDescription(Name = "删除B_PrintModel", Desc = "删除B_PrintModel", Url = "LabStarBaseTableService.svc/LS_UDTO_DelBPrintModel?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelBPrintModel?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelBPrintModel(long id);

        [ServiceContractDescription(Name = "查询B_PrintModel(HQL)", Desc = "查询B_PrintModel(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBPrintModelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPrintModel>", ReturnType = "ListBPrintModel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBPrintModelByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBPrintModelByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询B_PrintModel", Desc = "通过主键ID查询B_PrintModel", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBPrintModelById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPrintModel>", ReturnType = "BPrintModel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBPrintModelById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBPrintModelById(long id, string fields, bool isPlanish);
        #endregion

        #region LBReportDateDesc

        [ServiceContractDescription(Name = "新增LB_ReportDateDesc", Desc = "新增LB_ReportDateDesc", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBReportDateDesc", Get = "", Post = "LBReportDateDesc", Return = "BaseResultDataValue", ReturnType = "LBReportDateDesc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBReportDateDesc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBReportDateDesc(LBReportDateDesc entity);

        [ServiceContractDescription(Name = "修改LB_ReportDateDesc指定的属性", Desc = "修改LB_ReportDateDesc指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBReportDateDescByField", Get = "", Post = "LBReportDateDesc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBReportDateDescByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBReportDateDescByField(LBReportDateDesc entity, string fields);

        [ServiceContractDescription(Name = "删除LB_ReportDateDesc", Desc = "删除LB_ReportDateDesc", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBReportDateDesc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBReportDateDesc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBReportDateDesc(long id);

        [ServiceContractDescription(Name = "查询LB_ReportDateDesc(HQL)", Desc = "查询LB_ReportDateDesc(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateDescByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBReportDateDesc>", ReturnType = "ListLBReportDateDesc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBReportDateDescByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBReportDateDescByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_ReportDateDesc", Desc = "通过主键ID查询LB_ReportDateDesc", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBReportDateDescById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBReportDateDesc>", ReturnType = "LBReportDateDesc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBReportDateDescById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBReportDateDescById(long id, string fields, bool isPlanish);
        #endregion

        #region LBTGetMaxNo

        [ServiceContractDescription(Name = "新增LB_ReportDateDesc", Desc = "新增LB_ReportDateDesc", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBTGetMaxNo", Get = "", Post = "LBTGetMaxNo", Return = "BaseResultDataValue", ReturnType = "LBTGetMaxNo")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBTGetMaxNo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBTGetMaxNo(LBTGetMaxNo entity);

        [ServiceContractDescription(Name = "修改LB_ReportDateDesc指定的属性", Desc = "修改LB_ReportDateDesc指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBTGetMaxNoByField", Get = "", Post = "LBTGetMaxNo", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBTGetMaxNoByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBTGetMaxNoByField(LBTGetMaxNo entity, string fields);

        [ServiceContractDescription(Name = "删除LB_ReportDateDesc", Desc = "删除LB_ReportDateDesc", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBTGetMaxNo?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBTGetMaxNo?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBTGetMaxNo(long id);

        [ServiceContractDescription(Name = "查询LB_ReportDateDesc(HQL)", Desc = "查询LB_ReportDateDesc(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBTGetMaxNoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBTGetMaxNo>", ReturnType = "ListLBTGetMaxNo")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBTGetMaxNoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBTGetMaxNoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_ReportDateDesc", Desc = "通过主键ID查询LB_ReportDateDesc", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBTGetMaxNoById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBTGetMaxNo>", ReturnType = "LBTGetMaxNo")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBTGetMaxNoById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBTGetMaxNoById(long id, string fields, bool isPlanish);
        #endregion

        #region LBTranRuleHostSection

        [ServiceContractDescription(Name = "新增LB_TranRuleHostSection", Desc = "新增LB_TranRuleHostSection", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLBTranRuleHostSection", Get = "", Post = "LBTranRuleHostSection", Return = "BaseResultDataValue", ReturnType = "LBTranRuleHostSection")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLBTranRuleHostSection", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLBTranRuleHostSection(LBTranRuleHostSection entity);

        [ServiceContractDescription(Name = "修改LB_TranRuleHostSection指定的属性", Desc = "修改LB_TranRuleHostSection指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLBTranRuleHostSectionByField", Get = "", Post = "LBTranRuleHostSection", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLBTranRuleHostSectionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLBTranRuleHostSectionByField(LBTranRuleHostSection entity, string fields);

        [ServiceContractDescription(Name = "删除LB_TranRuleHostSection", Desc = "删除LB_TranRuleHostSection", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLBTranRuleHostSection?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLBTranRuleHostSection?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLBTranRuleHostSection(long id);

        [ServiceContractDescription(Name = "查询LB_TranRuleHostSection(HQL)", Desc = "查询LB_TranRuleHostSection(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleHostSectionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBTranRuleHostSection>", ReturnType = "ListLBTranRuleHostSection")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBTranRuleHostSectionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBTranRuleHostSectionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LB_TranRuleHostSection", Desc = "通过主键ID查询LB_TranRuleHostSection", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBTranRuleHostSectionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBTranRuleHostSection>", ReturnType = "LBTranRuleHostSection")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBTranRuleHostSectionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBTranRuleHostSectionById(long id, string fields, bool isPlanish);
        #endregion

        #region LisBarCodeRefuseRecord

        [ServiceContractDescription(Name = "新增Lis_BarCodeRefuseRecord", Desc = "新增Lis_BarCodeRefuseRecord", Url = "LabStarBaseTableService.svc/LS_UDTO_AddLisBarCodeRefuseRecord", Get = "", Post = "LisBarCodeRefuseRecord", Return = "BaseResultDataValue", ReturnType = "LisBarCodeRefuseRecord")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisBarCodeRefuseRecord", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisBarCodeRefuseRecord(LisBarCodeRefuseRecord entity);

        [ServiceContractDescription(Name = "修改Lis_BarCodeRefuseRecord指定的属性", Desc = "修改Lis_BarCodeRefuseRecord指定的属性", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateLisBarCodeRefuseRecordByField", Get = "", Post = "LisBarCodeRefuseRecord", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisBarCodeRefuseRecordByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisBarCodeRefuseRecordByField(LisBarCodeRefuseRecord entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_BarCodeRefuseRecord", Desc = "删除Lis_BarCodeRefuseRecord", Url = "LabStarBaseTableService.svc/LS_UDTO_DelLisBarCodeRefuseRecord?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisBarCodeRefuseRecord?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisBarCodeRefuseRecord(long id);

        [ServiceContractDescription(Name = "查询Lis_BarCodeRefuseRecord(HQL)", Desc = "查询Lis_BarCodeRefuseRecord(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLisBarCodeRefuseRecordByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisBarCodeRefuseRecord>", ReturnType = "ListLisBarCodeRefuseRecord")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisBarCodeRefuseRecordByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisBarCodeRefuseRecordByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_BarCodeRefuseRecord", Desc = "通过主键ID查询Lis_BarCodeRefuseRecord", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLisBarCodeRefuseRecordById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisBarCodeRefuseRecord>", ReturnType = "LisBarCodeRefuseRecord")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisBarCodeRefuseRecordById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisBarCodeRefuseRecordById(long id, string fields, bool isPlanish);
        #endregion

        #region LisQueue

        [ServiceContractDescription(Name = "新增Lis_Queue", Desc = "新增Lis_Queue", Url = "SingleTableService.svc/LS_UDTO_AddLisQueue", Get = "", Post = "LisQueue", Return = "BaseResultDataValue", ReturnType = "LisQueue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisQueue", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisQueue(LisQueue entity);

        [ServiceContractDescription(Name = "修改Lis_Queue指定的属性", Desc = "修改Lis_Queue指定的属性", Url = "SingleTableService.svc/LS_UDTO_UpdateLisQueueByField", Get = "", Post = "LisQueue", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisQueueByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisQueueByField(LisQueue entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_Queue", Desc = "删除Lis_Queue", Url = "SingleTableService.svc/LS_UDTO_DelLisQueue?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisQueue?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisQueue(long id);

        [ServiceContractDescription(Name = "查询Lis_Queue(HQL)", Desc = "查询Lis_Queue(HQL)", Url = "SingleTableService.svc/LS_UDTO_SearchLisQueueByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisQueue>", ReturnType = "ListLisQueue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisQueueByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisQueueByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_Queue", Desc = "通过主键ID查询Lis_Queue", Url = "SingleTableService.svc/LS_UDTO_SearchLisQueueById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisQueue>", ReturnType = "LisQueue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisQueueById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisQueueById(long id, string fields, bool isPlanish);
        #endregion

        #region 定制服务

        #region 仪器定制服务
        [ServiceContractDescription(Name = "仪器复制服务", Desc = "仪器复制服务", Url = "LabStarBaseTableService.svc/LS_UDTO_LBEquipCopyByID?equipID={equipID}", Get = "equipID={equipID}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LBEquipCopyByID?equipID={equipID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LBEquipCopyByID(long equipID);

        [ServiceContractDescription(Name = "复制仪器项目", Desc = "复制仪器项目", Url = "LabStarBaseTableService.svc/LS_UDTO_LBEquipItemCopyByEquipID?equipID={equipID}&toEquipID={toEquipID}", Get = "equipID={equipID}&toEquipID={toEquipID}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LBEquipItemCopyByEquipID?fromEquipID={fromEquipID}&toEquipID={toEquipID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LBEquipItemCopyByEquipID(long fromEquipID, long toEquipID);
        #endregion

        #region 项目表定制服务
        [ServiceContractDescription(Name = "查询LB_Item(HQL)", Desc = "查询LB_Item(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemListByHQL?itemID={itemID}&groupItemID={groupItemID}&sectionID={sectionID}&equipID={equipID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "itemID={itemID}&groupItemID={groupItemID}&sectionID={sectionID}&equipID={equipID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemListByHQL?itemID={itemID}&groupItemID={groupItemID}&sectionID={sectionID}&equipID={equipID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemListByHQL(long itemID, long groupItemID, long sectionID, long equipID, string where, string fields, int page, int limit, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "新增和删除组合项目", Desc = "新增和删除组合项目", Url = "LabStarBaseTableService.svc/LS_UDTO_AddDelLBItemGroup", Get = "", Post = "addEntityList,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddDelLBItemGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddDelLBItemGroup(IList<LBItemGroup> addEntityList, string delIDList);

        [ServiceContractDescription(Name = "新增和删除计算项目", Desc = "新增和删除计算项目", Url = "LabStarBaseTableService.svc/LS_UDTO_AddDelLBItemCalc", Get = "", Post = "addEntityList,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddDelLBItemCalc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddDelLBItemCalc(IList<LBItemCalc> addEntityList, string delIDList);

        [ServiceContractDescription(Name = "复制组合项目", Desc = "复制组合项目", Url = "LabStarBaseTableService.svc/LS_UDTO_AddCopyLBItemGroup?fromGroupItemID={fromGroupItemID}&toGroupItemID={toGroupItemID}", Get = "fromGroupItemID={fromGroupItemID}&toGroupItemID={toGroupItemID}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddCopyLBItemGroup?fromGroupItemID={fromGroupItemID}&toGroupItemID={toGroupItemID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddCopyLBItemGroup(long fromGroupItemID, long toGroupItemID);

        [ServiceContractDescription(Name = "复制小组项目", Desc = "复制小组项目", Url = "LabStarBaseTableService.svc/LS_UDTO_AddCopyLBSectionItem?fromSectionID={fromSectionID}&toSectionID={toSectionID}", Get = "fromSectionID={fromSectionID}&toSectionID={toSectionID}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddCopyLBSectionItem?fromSectionID={fromSectionID}&toSectionID={toSectionID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddCopyLBSectionItem(long fromSectionID, long toSectionID);

        [ServiceContractDescription(Name = "新增和删除仪器项目", Desc = "新增和删除仪器项目", Url = "LabStarBaseTableService.svc/LS_UDTO_AddDelLBEquipItem", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddDelLBEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddDelLBEquipItem(IList<LBEquipItem> addEntityList, bool isCheckEntityExist, string delIDList);

        [ServiceContractDescription(Name = "根据项目ID字符串获取项目树", Desc = "根据项目ID字符串获取项目树", Url = "LabStarBaseTableService.svc/LS_UDTO_GetItemTreeByItemIDList", Get = "", Post = "listItemID", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_GetItemTreeByItemIDList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_GetItemTreeByItemIDList(string listItemID);

        [ServiceContractDescription(Name = "删除仪器项目", Desc = "删除仪器项目", Url = "LabStarBaseTableService.svc/LS_UDTO_DeleteLBEquipItem", Get = "", Post = "delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_DeleteLBEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_DeleteLBEquipItem(string delIDList);

        [ServiceContractDescription(Name = "新增和删除小组项目", Desc = "新增和删除小组项目", Url = "LabStarBaseTableService.svc/LS_UDTO_AddDelLBSectionItem", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddDelLBSectionItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddDelLBSectionItem(IList<LBSectionItem> addEntityList, bool isCheckEntityExist, string delIDList);

        [ServiceContractDescription(Name = "删除小组项目", Desc = "删除小组项目", Url = "LabStarBaseTableService.svc/LS_UDTO_DeleteLBSectionItem", Get = "", Post = "delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_DeleteLBSectionItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_DeleteLBSectionItem(string delIDList);

        [ServiceContractDescription(Name = "查询LBSectionItemVO(HQL)", Desc = "查询LBSectionItemVO(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionItemVOByHQL?where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", Get = "where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSectionItemVO>", ReturnType = "LBSectionItemVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSectionItemVOByHQL?where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSectionItemVOByHQL(string where, string sort, int page, int limit, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询LBSectionSingleItemVO?(sectionID)", Desc = "查询LBSectionSingleItemVO?(sectionID)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSectionDefultSingleItemVO?sectionID={sectionID}&fields={fields}&isPlanish={isPlanish}", Get = "sectionID={sectionID}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSectionSingleItemVO>", ReturnType = "LBSectionSingleItemVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSectionDefultSingleItemVO?sectionID={sectionID}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSectionDefultSingleItemVO(long sectionID, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询LBItemGroup(HQL)", Desc = "查询LBItemGroup(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_QueryLBItemGroupByHQL?where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", Get = "where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSectionItem>", ReturnType = "ListLBSectionItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLBItemGroupByHQL?where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLBItemGroupByHQL(string where, string sort, int page, int limit, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询LBEquipItemVO(HQL)", Desc = "查询LBEquipItemVO(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBEquipItemVOByHQL?where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", Get = "where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBEquipItem>", ReturnType = "ListLBEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBEquipItemVOByHQL?where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBEquipItemVOByHQL(string where, string sort, int page, int limit, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询LBEquipItem(HQL)", Desc = "查询LBEquipItem(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_QueryLBEquipItemByHQL?where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", Get = "where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBEquipItem>", ReturnType = "ListLBEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLBEquipItemByHQL?where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLBEquipItemByHQL(string where, string sort, int page, int limit, string fields, bool isPlanish);

        #endregion

        #region LBPhrase 定制服务

        [ServiceContractDescription(Name = "新增和删除短语", Desc = "新增和删除短语", Url = "LabStarBaseTableService.svc/LS_UDTO_AddDelLBPhrase", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddDelLBPhrase", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddDelLBPhrase(IList<LBPhrase> addEntityList, bool isCheckEntityExist, string delIDList);

        [ServiceContractDescription(Name = "查询短语信息", Desc = "查询短语信息", Url = "LabStarBaseTableService.svc/LS_UDTO_QueryLBPhraseValue?phraseType={phraseType}&typeName={typeName}&typeCode={typeCode}&objectID={objectID}&otherWhere={otherWhere}", Get = "phraseType={phraseType}&typeName={typeName}&typeCode={typeCode}&objectID={objectID}&otherWhere={otherWhere}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLBPhraseValue?phraseType={phraseType}&typeName={typeName}&typeCode={typeCode}&objectID={objectID}&otherWhere={otherWhere}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLBPhraseValue(string phraseType, string typeName, string typeCode, long objectID, string otherWhere);

        #endregion

        #region 专家规则

        [ServiceContractDescription(Name = "查询LBExpertRule(HQL)", Desc = "查询LBExpertRule(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_QueryLBExpertRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBExpertRule>", ReturnType = "ListLBExpertRule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLBExpertRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLBExpertRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        /// <summary>
        /// 复制专家规则并新增
        /// </summary>
        /// <param name="expertRuleID">专家规则ID</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "复制专家规则并新增", Desc = "复制专家规则并新增", Url = "LabStarBaseTableService.svc/LS_UDTO_LBExpertRuleCopyByRuleID", Get = "", Post = "expertRuleID", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue ")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LBExpertRuleCopyByRuleID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LBExpertRuleCopyByRuleID(long expertRuleID);

        #endregion

        #region 参数表定制服务

        [ServiceContractDescription(Name = "查询出厂设置参数", Desc = "查询出厂设置参数", Url = "LabStarBaseTableService.svc/LS_UDTO_QueryFactorySettingPara?paraTypeCode={paraTypeCode}&fields={fields}&isPlanish={isPlanish}", Get = "paraTypeCode={paraTypeCode}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryFactorySettingPara?paraTypeCode={paraTypeCode}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryFactorySettingPara(string paraTypeCode, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询系统默认设置参数", Desc = "查询系统默认设置参数", Url = "LabStarBaseTableService.svc/LS_UDTO_QuerySystemDefaultPara?paraTypeCode={paraTypeCode}&fields={fields}&isPlanish={isPlanish}", Get = "paraTypeCode={paraTypeCode}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QuerySystemDefaultPara?paraTypeCode={paraTypeCode}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QuerySystemDefaultPara(string paraTypeCode, string fields, bool isPlanish);

        /// <summary>
        /// 根据参数名称查询相关参数信息
        /// </summary>
        /// <param name="paraName">参数名称</param>
        /// <param name="fields">返回属性列表</param>
        /// <param name="isPlanish">是否压平</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "根据参数名称查询相关参数信息", Desc = "根据参数名称查询相关参数信息", Url = "LabStarBaseTableService.svc/LS_UDTO_QueryParaInfoByParaName?paraName={paraName}&fields={fields}&isPlanish={isPlanish}", Get = "paraName={paraName}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryParaInfoByParaName?paraName={paraName}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryParaInfoByParaName(string paraName, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "保存系统默认设置参数", Desc = "保存系统默认设置参数", Url = "LabStarBaseTableService.svc/LS_UDTO_SaveSystemDefaultPara", Get = "", Post = "entityList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SaveSystemDefaultPara", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SaveSystemDefaultPara(IList<BPara> entityList);

        /// <summary>
        /// 查询个性设置信息列表
        /// 例如：按站点设置的参数，则查询站点列表
        /// </summary>
        /// <param name="paraTypeCode">系统相关性ID</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "查询个性设置信息列表", Desc = "查询个性设置信息列表", Url = "LabStarBaseTableService.svc/LS_UDTO_QueryParaSystemTypeInfo?systemTypeCode={systemTypeCode}&paraTypeCode={paraTypeCode}", Get = "systemTypeCode={systemTypeCode}&paraTypeCode={paraTypeCode}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryParaSystemTypeInfo?systemTypeCode={systemTypeCode}&paraTypeCode={paraTypeCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryParaSystemTypeInfo(string systemTypeCode, string paraTypeCode);

        /// <summary>
        /// 查询系统个性参数设置
        /// </summary>
        /// <param name="where">通用查询条件</param>
        /// <param name="systemTypeCode">参数相关性</param>
        /// <param name="paraTypeCode">参数字典类名</param>
        /// <param name="fields">返回属性列表</param>
        /// <param name="isPlanish">是否压平</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "查询系统个性参数设置", Desc = "查询系统个性参数设置", Url = "LabStarBaseTableService.svc/LS_UDTO_QuerySystemParaItem?where={where}&systemTypeCode={systemTypeCode}&paraTypeCode={paraTypeCode}&fields={fields}&isPlanish={isPlanish}", Get = "where={where}&systemTypeCode={systemTypeCode}&paraTypeCode={paraTypeCode}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QuerySystemParaItem?where={where}&systemTypeCode={systemTypeCode}&paraTypeCode={paraTypeCode}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QuerySystemParaItem(string where, string systemTypeCode, string paraTypeCode, string fields, bool isPlanish);

        /// <summary>
        /// 根据参数编码和参数相关性对象ID查找参数值
        /// </summary>
        /// <param name="paraNo">参数编码</param>
        /// <param name="objectID">参数相关性对象ID</param>
        /// <param name="fields">返回属性列表</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "根据参数编码和参数相关性对象ID查找参数值", Desc = "根据参数编码和参数相关性对象ID查找参数值", Url = "LabStarBaseTableService.svc/LS_UDTO_QueryParaValueByParaNo?paraNo={paraNo}&objectID={objectID}&fields={fields}", Get = "paraNo={paraNo}&objectID={objectID}&fields={fields}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryParaValueByParaNo?paraNo={paraNo}&objectID={objectID}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryParaValueByParaNo(string paraNo, string objectID, string fields);

        /// <summary>
        /// 根据参数分类编码和参数相关性对象ID查找参数值
        /// </summary>
        /// <param name="paraTypeCode">参数分类编码</param>
        /// <param name="objectID">参数相关性对象ID</param>
        /// <param name="fields">返回属性列表</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "根据参数分类编码和参数相关性对象ID查找参数值", Desc = "根据参数分类编码和参数相关性对象ID查找参数值", Url = "LabStarBaseTableService.svc/LS_UDTO_QueryParaValueByParaTypeCode?paraTypeCode={paraTypeCode}&objectID={objectID}&fields={fields}", Get = "paraTypeCode={paraTypeCode}&objectID={objectID}&fields={fields}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryParaValueByParaTypeCode?paraTypeCode={paraTypeCode}&objectID={objectID}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryParaValueByParaTypeCode(string paraTypeCode, string objectID, string fields);

        /// <summary>
        /// 保存系统个性参数设置
        /// </summary>
        /// <param name="objectInfo">个性化信息</param>
        /// <param name="entityList">个性参数列表</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "保存系统个性参数设置", Desc = "保存系统个性参数设置", Url = "LabStarBaseTableService.svc/LS_UDTO_SaveSystemParaItem", Get = "", Post = "entityList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SaveSystemParaItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SaveSystemParaItem(string objectInfo, IList<BParaItem> entityList);

        /// <summary>
        /// 复制系统个性参数设置
        /// </summary>
        /// <param name="objectInfo">个性化信息</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "复制系统个性参数设置", Desc = "复制系统个性参数设置", Url = "LabStarBaseTableService.svc/LS_UDTO_AddCopySystemParaItem", Get = "", Post = "fromObjectID,toObjectID,toObjectName", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddCopySystemParaItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddCopySystemParaItem(string fromObjectID, string toObjectID, string toObjectName);

        /// <summary>
        /// 个性参数值设置为系统默认值---按ObjectID设置
        /// </summary>
        /// <param name="objectID"></param>
        /// <param name="paraTypeCode"></param> 
        /// <returns></returns>
        [ServiceContractDescription(Name = "复制系统个性参数设置", Desc = "复制系统个性参数设置", Url = "LS_UDTO_SetParaItemDefaultValue.svc/LS_UDTO_AddCopySystemParaItem", Get = "", Post = "objectID,paraTypeCode", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SetParaItemDefaultValue", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SetParaItemDefaultValue(string objectID, string paraTypeCode);

        /// <summary>
        /// 删除系统个性参数设置
        /// </summary>
        /// <param name="objectInfo">个性化信息</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "删除系统个性参数设置", Desc = "删除系统个性参数设置", Url = "LabStarBaseTableService.svc/LS_UDTO_DeleteSystemParaItem", Get = "", Post = "objectInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_DeleteSystemParaItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_DeleteSystemParaItem(string objectInfo);

        #endregion

        #region 历史对比
        [ServiceContractDescription(Name = "新增和删除小组历史对比", Desc = "新增和删除小组历史对比", Url = "LabStarBaseTableService.svc/LS_UDTO_AddDelLBSectionHisComp", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddDelLBSectionHisComp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddDelLBSectionHisComp(IList<LBSectionHisComp> addEntityList, bool isCheckEntityExist, string delIDList);
        #endregion

        #region 人员小组角色权限
        [ServiceContractDescription(Name = "新增和删除检验小组权限", Desc = "新增和删除检验小组权限", Url = "LabStarBaseTableService.svc/LS_UDTO_AddDelLBRight", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddDelLBRight", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddDelLBRight(IList<LBRight> addEntityList, bool isCheckEntityExist, string delIDList);

        [ServiceContractDescription(Name = "查询员工共同数据权限小组", Desc = "查询员工共同数据权限小组", Url = "LabStarBaseTableService.svc/LS_UDTO_QueryCommonSectionRightByEmpID?empIDList={empIDList}&fields={fields}&isPlanish={isPlanish}", Get = "empIDList={empIDList}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryCommonSectionRightByEmpID?empIDList={empIDList}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryCommonSectionRightByEmpID(string empIDList, string fields, bool isPlanish);


        [ServiceContractDescription(Name = "新增员工小组数据权限", Desc = "新增员工小组数据权限", Url = "LabStarBaseTableService.svc/LS_UDTO_AddEmpSectionDataRight?empIDList={empIDList}&sectionIDList={sectionIDList}&roleIDList={roleIDList}", Get = "empIDList={empIDList}&sectionIDList={sectionIDList}&roleIDList={roleIDList}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddEmpSectionDataRight?empIDList={empIDList}&sectionIDList={sectionIDList}&roleIDList={roleIDList}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddEmpSectionDataRight(string empIDList, string sectionIDList, string roleIDList);


        [ServiceContractDescription(Name = "删除员工小组数据权限", Desc = "删除员工小组数据权限", Url = "LabStarBaseTableService.svc/LS_UDTO_DelelteEmpSectionDataRight?empIDList={empIDList}&sectionIDList={sectionIDList}&roleIDList={roleIDList}", Get = "empIDList={empIDList}&sectionIDList={sectionIDList}&roleIDList={roleIDList}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_DelelteEmpSectionDataRight?empIDList={empIDList}&sectionIDList={sectionIDList}&roleIDList={roleIDList}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_DelelteEmpSectionDataRight(string empIDList, string sectionIDList, string roleIDList);



        #endregion

        #region 基础表导入导出服务
        [ServiceContractDescription(Name = "导入基础表Excel文件", Desc = "导入基础表Excel文件", Url = "LabStarBaseTableService.svc/LS_UDTO_UploadBaseTableDataByExcelFile", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UploadBaseTableDataByExcelFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message LS_UDTO_UploadBaseTableDataByExcelFile();

        [ServiceContractDescription(Name = "下载Excel文件", Desc = "下载Excel文件", Url = "LabStarBaseTableService.svc/LS_UDTO_DownLoadExcel?fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}", Get = "fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/LS_UDTO_DownLoadExcel?fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}")]
        [OperationContract]
        Stream LS_UDTO_DownLoadExcel(string fileName, string downFileName, int isUpLoadFile, int operateType);

        [ServiceContractDescription(Name = "导出Excel文件路径", Desc = "导出Excel文件路径", Url = "LabStarBaseTableService.svc/LS_UDTO_OutputBaseTableDataExcelFilePath", Get = "", Post = "entityName,idList,where,isHeader", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_OutputBaseTableDataExcelFilePath", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message LS_UDTO_OutputBaseTableDataExcelFilePath();
        #endregion

        #region 字典对照定制服务
        [ServiceContractDescription(Name = "查询对接系统以及项目对照数量", Desc = "查询对接系统以及项目对照数量", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeDicContrastNumByHQL?SectionID={SectionID}&GroupType={GroupType}&CName={CName}", Get = "", Post = "", Return = "BaseResultList<LBSickType>", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSickTypeDicContrastNumByHQL?SectionID={SectionID}&GroupType={GroupType}&CName={CName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSickTypeDicContrastNumByHQL(long SectionID, int GroupType, string CName);

        [ServiceContractDescription(Name = "根据对接系统查询检验项目以及对照项目信息", Desc = "根据对接系统查询检验项目以及对照项目信息", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemAndLBItemCodeLink?SectionID={SectionID}&GroupType={GroupType}&SickTypeID={SickTypeID}&ItemCName={ItemCName}&sort={sort}&page={page}&limit={limit}", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemAndLBItemCodeLink?SectionID={SectionID}&GroupType={GroupType}&SickTypeID={SickTypeID}&ItemCName={ItemCName}&sort={sort}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemAndLBItemCodeLink(long SectionID, int GroupType, long SickTypeID, string ItemCName, string sort, int page, int limit);

        [ServiceContractDescription(Name = "保存或者修改项目对照信息", Desc = "保存或者修改项目对照信息", Url = "LabStarBaseTableService.svc/LS_UDTO_AddOrUPDateLBItemCodeLink", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddOrUPDateLBItemCodeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddOrUPDateLBItemCodeLink(Entity.LabStar.ViewObject.Response.LBItemCodeLinkVO entity);

        [ServiceContractDescription(Name = "根据对接系统查询检验项目", Desc = "根据对接系统查询检验项目", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBItemBySickTypeID?SectionID={SectionID}&GroupType={GroupType}&SickTypeID={SickTypeID}&ItemCName={ItemCName}", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemBySickTypeID?SectionID={SectionID}&GroupType={GroupType}&SickTypeID={SickTypeID}&ItemCName={ItemCName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemBySickTypeID(long SectionID, int GroupType, long SickTypeID, string ItemCName);

        [ServiceContractDescription(Name = "查询对接系统以及字典对照数量", Desc = "查询对接系统以及字典对照数量", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchLBSickTypeOtherDicContrastNum?ContrastDicType={ContrastDicType}ContrastDicType", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBSickTypeOtherDicContrastNum?ContrastDicType={ContrastDicType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBSickTypeOtherDicContrastNum(string ContrastDicType);

        [ServiceContractDescription(Name = "查询基础字典数据与对照数据", Desc = "查询基础字典数据与对照数据", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBasicsDicAndLBDicCodeLink?SickTypeId={SickTypeId}&ContrastDicType={ContrastDicType}&DicInfo={DicInfo}&CName={CName}&sort={sort}&page={page}&limit={limit}", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBasicsDicAndLBDicCodeLink?SickTypeId={SickTypeId}&ContrastDicType={ContrastDicType}&DicInfo={DicInfo}&CName={CName}&sort={sort}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBasicsDicAndLBDicCodeLink(long SickTypeId, string ContrastDicType, string DicInfo, string CName, string sort, int page, int limit);

        [ServiceContractDescription(Name = "保存或者修改基础字典对照信息", Desc = "保存或者修改基础字典对照信息", Url = "LabStarBaseTableService.svc/LS_UDTO_AddOrUPDateLBDicCodeLink", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddOrUPDateLBDicCodeLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddOrUPDateLBDicCodeLink(Entity.LabStar.ViewObject.Response.LBDicCodeLinkVO entity);

        [ServiceContractDescription(Name = "查询基础字典数据", Desc = "查询基础字典数据", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBasicsDicDataBySickTypeId?SickTypeId={SickTypeId}&ContrastDicType={ContrastDicType}&DicInfo={DicInfo}&CName={CName}", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBasicsDicDataBySickTypeId?SickTypeId={SickTypeId}&ContrastDicType={ContrastDicType}&DicInfo={DicInfo}&CName={CName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBasicsDicDataBySickTypeId(long SickTypeId, string ContrastDicType, string DicInfo, string CName);

        [ServiceContractDescription(Name = "复制项目对照", Desc = "复制项目对照", Url = "LabStarBaseTableService.svc/LS_UDTO_CopyLBItemCodeLinkContrast?SickTypeIds={SickTypeIds}&thisSickTypeId={thisSickTypeId}", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_CopyLBItemCodeLinkContrast?SickTypeIds={SickTypeIds}&thisSickTypeId={thisSickTypeId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_CopyLBItemCodeLinkContrast(string SickTypeIds,string thisSickTypeId);

        [ServiceContractDescription(Name = "其他字典对照复制", Desc = "其他字典对照复制", Url = "LabStarBaseTableService.svc/LS_UDTO_CopyLBDicCodeLinkContrast?SickTypeIds={SickTypeIds}&thisSickTypeId={thisSickTypeId}&dictype={dictype}", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_CopyLBDicCodeLinkContrast?SickTypeIds={SickTypeIds}&thisSickTypeId={thisSickTypeId}&dictype={dictype}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_CopyLBDicCodeLinkContrast(string SickTypeIds, string thisSickTypeId,string dictype);
        #endregion

        #region 打印模板定制服务
        [ServiceContractDescription(Name = "查询打印模板数据和自动新增打印模板数据", Desc = "查询打印模板数据和自动新增打印模板数据", Url = "LabStarBaseTableService.svc/LS_UDTO_SearchBPrintModelAndAutoUploadModel?BusinessTypeCode={BusinessTypeCode}&ModelTypeCode={ModelTypeCode}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BPrintModel>", ReturnType = "ListBPrintModel")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBPrintModelAndAutoUploadModel?BusinessTypeCode={BusinessTypeCode}&ModelTypeCode={ModelTypeCode}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBPrintModelAndAutoUploadModel(string BusinessTypeCode, string ModelTypeCode, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "打印模板上传", Desc = "打印模板上传", Url = "LabStarBaseTableService.svc/LS_UDTO_UploadReportModel", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UploadReportModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_UploadReportModel();

        [ServiceContractDescription(Name = "打印模板修改", Desc = "打印模板修改", Url = "LabStarBaseTableService.svc/LS_UDTO_UpdateReportModel", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateReportModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_UpdateReportModel();

        [ServiceContractDescription(Name = "打印模板修改", Desc = "打印模板修改", Url = "LabStarBaseTableService.svc/LS_UDTO_DelReportModelById?Id={Id}", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_DelReportModelById?Id={Id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelReportModelById(long Id);

        [ServiceContractDescription(Name = "数据打印", Desc = "数据打印", Url = "LabStarBaseTableService.svc/LS_UDTO_PrintDataByPrintModel", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PrintDataByPrintModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PrintDataByPrintModel(string Data, long PrintModelID);

        [ServiceContractDescription(Name = "数据导出", Desc = "数据导出", Url = "LabStarBaseTableService.svc/LS_UDTO_ExportDataByPrintModel", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_ExportDataByPrintModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_ExportDataByPrintModel(string Data, long PrintModelID);
        #endregion

        #endregion
    }
}
