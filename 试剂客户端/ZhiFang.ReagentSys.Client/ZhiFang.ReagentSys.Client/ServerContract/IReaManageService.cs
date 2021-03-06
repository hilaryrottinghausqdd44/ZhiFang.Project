using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaStoreIn;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using System.ServiceModel.Channels;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaSale;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client.ViewObject;

namespace ZhiFang.ReagentSys.Client.ServerContract
{
    [ServiceContract]
    public interface IReaManageService
    {
        #region 客户机构维护
        [ServiceContractDescription(Name = "智方平台初始化客户机构信息", Desc = "智方平台初始化客户机构信息", Url = "ReaManageService.svc/ST_UDTO_AddCenOrgOfInitialize", Get = "", Post = "labId,cenOrg,user,roleIdStrOfZf,moduleIdStr", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddCenOrgOfInitialize", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddCenOrgOfInitialize(long labId, CenOrg cenOrg, RBACUser user, string roleIdStrOfZf, string moduleIdStr);

        [ServiceContractDescription(Name = "授权变更定制,获取机构授权的系统角色的角色模块访问权限", Desc = "授权变更定制,获取机构授权的系统角色的角色模块访问权限", Url = "ReaManageService.svc/ST_UDTO_SearchRBACRoleModuleByLabIDAndSysRoleId?labId={labId}&fields={fields}&isPlanish={isPlanish}", Get = "labId={labId}&fields={fields}&isPlanish={isPlanish", Post = "", Return = "BaseResultList<RBACRoleModule>", ReturnType = "ListRBACRoleModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchRBACRoleModuleByLabIDAndSysRoleId?labId={labId}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchRBACRoleModuleByLabIDAndSysRoleId(long labId, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "智方平台修改客户机构的授权变更信息", Desc = "智方平台修改客户机构的授权变更信息", Url = "ReaManageService.svc/ST_UDTO_UpdateCenOrgAuthorizationModifyOfPlatform", Get = "", Post = "ReaCenOrg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenOrgAuthorizationModifyOfPlatform", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenOrgAuthorizationModifyOfPlatform(long labId, long cenOrgId, IList<RBACModule> moduleList);

        [ServiceContractDescription(Name = "授权文件从智方平台导出,fileType:(授权文件类型:1:机构初始化;2:授权变更)", Desc = "授权文件从智方平台导出,fileType:(授权文件类型:1:机构初始化;2:授权变更)", Url = "ReaManageService.svc/ST_UDTO_SearchExportAuthorizationFileOfPlatform?labId={labId}&cenOrgId={cenOrgId}&fileType={fileType}", Get = "labId={labId}&cenOrgId={cenOrgId}&fileType={fileType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchExportAuthorizationFileOfPlatform?labId={labId}&cenOrgId={cenOrgId}&fileType={fileType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_SearchExportAuthorizationFileOfPlatform(long labId, long cenOrgId, long fileType);

        [ServiceContractDescription(Name = "客户端的机构授权文件导入", Desc = "客户端的机构授权文件导入", Url = "ReaManageService.svc/ST_UDTO_UploadAuthorizationFileOfClient", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UploadAuthorizationFileOfClient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message ST_UDTO_UploadAuthorizationFileOfClient();

        [ServiceContractDescription(Name = "获取当前授权机构的授权进度信息", Desc = "获取当前授权机构的授权进度信息", Url = "ReaManageService.svc/ST_UDTO_SearchLicenseGuideVOByCenOrg", Get = "", Post = "cenOrg", Return = "BaseResultList<LicenseGuideVO>", ReturnType = "ListLicenseGuideVO")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchLicenseGuideVOByCenOrg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchLicenseGuideVOByCenOrg(CenOrg cenOrg, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "授权机构初始化信息分步处理", Desc = "授权机构初始化信息分步处理", Url = "ReaManageService.svc/ST_UDTO_AddCenOrgInitializeByStep", Get = "", Post = "cenOrg,entity", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddCenOrgInitializeByStep", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddCenOrgInitializeByStep(CenOrg cenOrg, string entity);

        [ServiceContractDescription(Name = "从授权初始化文件获取授权机构的基本信息", Desc = "从授权初始化文件获取授权机构的基本信息", Url = "ReaManageService.svc/ST_UDTO_GetCenOrgInitializeInfo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetCenOrgInitializeInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetCenOrgInitializeInfo();

        #endregion

        #region 机构货品维护
        [ServiceContractDescription(Name = "依分类类型获取机构货品的一级分类或二级分类List信息", Desc = "依分类类型获取机构货品的一级分类或二级分类List信息", Url = "ReaManageService.svc/RS_UDTO_SearchGoodsClassListByClassTypeAndHQL?classType={classType}&page={page}&limit={limit}&where={where}&sort={sort}", Get = "classType={classType}&page={page}&limit={limit}&where={where}&sort={sort}", Post = "", Return = "BaseResultList<ReaGoodsClassVO>", ReturnType = "ListReaGoodsClassVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchGoodsClassListByClassTypeAndHQL?classType={classType}&page={page}&limit={limit}&where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchGoodsClassListByClassTypeAndHQL(string classType, string where, string sort, int page, int limit);

        [ServiceContractDescription(Name = "依分类类型获取机构货品的一级分类或二级分类EntityList信息", Desc = "依分类类型获取机构货品的一级分类或二级分类EntityList信息", Url = "ReaManageService.svc/RS_UDTO_SearchGoodsClassEntityListByClassTypeAndHQL?classType={classType}&hasNull={hasNull}&isPlanish={isPlanish}&fields={fields}&page={page}&limit={limit}&where={where}&sort={sort}", Get = "classType={classType}&hasNull={hasNull}&isPlanish={isPlanish}&fields={fields}&page={page}&limit={limit}&where={where}&sort={sort}", Post = "", Return = "BaseResultList<ReaGoodsClassVO>", ReturnType = "ListReaGoodsClassVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchGoodsClassEntityListByClassTypeAndHQL?classType={classType}&hasNull={hasNull}&isPlanish={isPlanish}&fields={fields}&page={page}&limit={limit}&where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchGoodsClassEntityListByClassTypeAndHQL(string classType, bool hasNull, bool isPlanish, string fields, string where, string sort, int page, int limit);

        [ServiceContractDescription(Name = "获取最大的时间戳，接口同步货品使用", Desc = "", Url = "ReaManageService.svc/RS_UDTO_GetMaxTS", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_GetMaxTS", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_GetMaxTS();
        #endregion

        #region 客户端部门采购定制服务

        [ServiceContractDescription(Name = "定制查询部门货品表(HQL)", Desc = "定制查询部门货品表(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchListByDeptIdAndHQL?deptId={deptId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&goodsQty={goodsQty}", Get = "deptId={deptId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&goodsQty={goodsQty}", Post = "", Return = "BaseResultList<ReaDeptGoods>", ReturnType = "ListReaDeptGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchListByDeptIdAndHQL?deptId={deptId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&goodsQty={goodsQty}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchListByDeptIdAndHQL(long deptId, int page, int limit, string fields, string where, string goodsQty, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "依员工Id获取员工申请录入的申请部门", Desc = "依员工Id获取员工申请录入的申请部门", Url = "ReaManageService.svc/ST_UDTO_SearchApplyHRDeptByByHRDeptId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&deptId={deptId}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&deptId={deptId}", Post = "", Return = "BaseResultList<ReaDeptGoods>", ReturnType = "ListReaDeptGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchApplyHRDeptByByHRDeptId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&deptId={deptId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchApplyHRDeptByByHRDeptId(int page, int limit, string fields, string where, string sort, bool isPlanish, long deptId);

        [ServiceContractDescription(Name = "获取部门货品的全部货品与货品所属供应商信息(按货品进行分组,找出每组货品下的对应的供应商信息)", Desc = "获取部门货品的全部货品与货品所属供应商信息(按货品进行分组,找出每组货品下的对应的供应商信息)", Url = "ReaManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHRDeptId?deptId={deptId}", Get = "deptId={deptId}", Post = "", Return = "BaseResultList<ReaCenOrgGoodsVO>", ReturnType = "ListReaCenOrgGoodsVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsOrgLinkByHRDeptId?deptId={deptId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkByHRDeptId(long deptId);

        [ServiceContractDescription(Name = "获取采购申请货品库存数量", Desc = "获取采购申请货品库存数量", Url = "ReaManageService.svc/ST_UDTO_SearchReaGoodsCurrentQtyByGoodIdStr?idStr={idStr}&goodIdStr={goodIdStr}", Get = "idStr={idStr}&goodIdStr={goodIdStr}", Post = "", Return = "BaseResultList<ReaGoodsCurrentQtyVO>", ReturnType = "ListReaGoodsCurrentQtyVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsCurrentQtyByGoodIdStr?idStr={idStr}&goodIdStr={goodIdStr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsCurrentQtyByGoodIdStr(string idStr, string goodIdStr);

        [ServiceContractDescription(Name = "部门采购申请新增服务", Desc = "部门采购申请新增服务", Url = "ReaManageService.svc/ST_UDTO_AddReaBmsReqDocAndDt", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsReqDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsReqDocAndDt", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsReqDocAndDt(ReaBmsReqDoc entity, IList<ReaBmsReqDtl> dtAddList);

        [ServiceContractDescription(Name = "部门采购申请更新服务", Desc = "部门采购申请更新服务", Url = "ReaManageService.svc/ST_UDTO_UpdateReaBmsReqDocAndDt", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDocAndDt", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDocAndDt(ReaBmsReqDoc entity, string fields, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList);

        [ServiceContractDescription(Name = "部门采购申请明细更新(验证主单后只操作明细)", Desc = "部门采购申请明细更新(验证主单后只操作明细)", Url = "ReaManageService.svc/ST_UDTO_UpdateReaBmsReqDtlOfCheck", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDtlOfCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDtlOfCheck(ReaBmsReqDoc entity, string fields, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList);

        [ServiceContractDescription(Name = "部门采购申请审核服务", Desc = "部门采购申请审核服务", Url = "ReaManageService.svc/ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck(ReaBmsReqDoc entity, string fields, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList);

        [ServiceContractDescription(Name = "依据客户端的申请主单(已审核)生成客户端订单信息", Desc = "依据客户端的申请主单(已审核)生成客户端订单信息", Url = "ReaManageService.svc/ST_UDTO_AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr", Get = "", Post = "String", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr(string idStr, bool commonIsMerge, bool ugentIsMerge, string reaServerLabcCode, string labcName);

        [ServiceContractDescription(Name = "部门采购申请复制新增", Desc = "部门采购申请复制新增", Url = "ReaManageService.svc/ST_UDTO_AddReaBmsReqDocAndDtOfCopyApply", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsReqDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsReqDocAndDtOfCopyApply", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsReqDocAndDtOfCopyApply(long id);
        #endregion

        #region 采购申请(通用)

        [ServiceContractDescription(Name = "查询申请明细表(HQL)", Desc = "查询申请明细表(HQL)", Url = "ReaManageService.svc/ST_UDTO_SearchReaBmsReqDtlByHQLCommon?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsReqDtl>", ReturnType = "ListReaBmsReqDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDtlByHQLCommon?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDtlByHQLCommon(int page, int limit, string fields, string where, string sort, bool isPlanish);

        #endregion

        #region 采购申请(智能采购)

        [ServiceContractDescription(Name = "查询采购申请明细(HQL)", Desc = "查询采购申请明细(HQL)", Url = "ReaManageService.svc/ST_UDTO_SearchReaBmsReqDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isCalcQty={isCalcQty}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isCalcQty={isCalcQty}", Post = "", Return = "BaseResultList<ReaBmsReqDtl>", ReturnType = "ReaBmsReqDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isCalcQty={isCalcQty}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isCalcQty);
        
        [ServiceContractDescription(Name = "选择模板返回货品信息", Desc = "选择模板返回货品信息", Url = "ReaManageService.svc/RS_UDTO_SearchGoodsByDeptIdAndTemplateIdByHQL?deptId={deptId}&templateId={templateId}&fields={fields}&isPlanish={isPlanish}&isCalcQty={isCalcQty}", Get = "deptId={deptId}&templateId={templateId}&fields={fields}&isPlanish={isPlanish}&isCalcQty={isCalcQty}", Post = "", Return = "BaseResultList<ReaDeptGoods>", ReturnType = "ReaDeptGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchGoodsByDeptIdAndTemplateIdByHQL?deptId={deptId}&templateId={templateId}&fields={fields}&isPlanish={isPlanish}&isCalcQty={isCalcQty}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchGoodsByDeptIdAndTemplateIdByHQL(long deptId, long templateId, string fields, bool isPlanish, bool isCalcQty);

        [ServiceContractDescription(Name = "智能采购-计算某货品的平均使用量和建议采购量", Desc = "", Url = "ReaManageService.svc/ST_UDTO_CalcAvgUsedAndSuggestPurchaseQty?goodsId={goodsId}", Get = "goodsId={goodsId}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_CalcAvgUsedAndSuggestPurchaseQty?goodsId={goodsId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_CalcAvgUsedAndSuggestPurchaseQty(long goodsId);

        #endregion

        #region 客户端订单处理

        [ServiceContractDescription(Name = "查询订单明细(HQL)", Desc = "查询订单明细单(HQL)", Url = "ReaManageService.svc/ST_UDTO_SearchReaBmsCenOrderDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&orderDocId={orderDocId}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&orderDocId={orderDocId}", Post = "", Return = "BaseResultList<ReaBmsCenOrderDoc>", ReturnType = "ListBmsCenOrderDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenOrderDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&orderDocId={orderDocId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenOrderDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, long orderDocId);

        [ServiceContractDescription(Name = "客户端订单新增服务", Desc = "客户端订单新增服务", Url = "ReaManageService.svc/ST_UDTO_AddReaBmsCenOrderDocAndDt", Get = "", Post = "BmsCenOrderDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsReqDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsCenOrderDocAndDt", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsCenOrderDocAndDt(ReaBmsCenOrderDoc entity, IList<ReaBmsCenOrderDtl> dtAddList, int otype);

        [ServiceContractDescription(Name = "客户端订单更新服务", Desc = "客户端订单更新服务", Url = "ReaManageService.svc/ST_UDTO_UpdateReaBmsCenOrderDocAndDt", Get = "", Post = "BmsCenOrderDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCenOrderDocAndDt", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCenOrderDocAndDt(ReaBmsCenOrderDoc entity, string fields, IList<ReaBmsCenOrderDtl> dtAddList, IList<ReaBmsCenOrderDtl> dtEditList, string checkIsUploaded);

        [ServiceContractDescription(Name = "客户端订单付款管理更新服务", Desc = "客户端订单付款管理更新服务", Url = "ReaManageService.svc/RS_UDTO_UpdateReaBmsCenOrderDocByPay", Get = "", Post = "BmsCenOrderDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsCenOrderDocByPay", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsCenOrderDocByPay(ReaBmsCenOrderDoc entity, string fields);

        [ServiceContractDescription(Name = "删除平台订货明细(并重新更新订单总价)", Desc = "删除平台订货明细(并重新更新订单总价)", Url = "ReaManageService.svc/ST_UDTO_DelReaBmsCenOrderDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsCenOrderDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsCenOrderDtl(long id);

        #endregion

        #region 手工验收
        [ServiceContractDescription(Name = "新增客户端手工验收信息", Desc = "新增客户端手工验收信息", Url = "ReaManageService.svc/ST_UDTO_AddReaSaleDocConfirmOfManualInput", Get = "", Post = "ReaBmsCenSaleDocConfirm", Return = "BaseResultDataValue", ReturnType = "ReaBmsCenSaleDocConfirm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaSaleDocConfirmOfManualInput", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaSaleDocConfirmOfManualInput(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode);

        [ServiceContractDescription(Name = "编辑更新客户端手工验收信息", Desc = "编辑更新客户端手工验收信息", Url = "ReaManageService.svc/ST_UDTO_UpdateReaSaleDocConfirmOfManualInput", Get = "", Post = "ReaBmsCenSaleDocConfirm", Return = "BaseResultBool", ReturnType = "ReaBmsCenSaleDocConfirm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaSaleDocConfirmOfManualInput", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaSaleDocConfirmOfManualInput(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, IList<ReaSaleDtlConfirmVO> dtEditList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode, string fields, string fieldsDtl);

        #endregion

        #region 订单验收
        [ServiceContractDescription(Name = "导入供货订单Excel文件", Desc = "导入供货订单Excel文件", Url = "ReaManageService.svc/RS_UDTO_UploadSupplyReaOrderDataByExcel", Get = "", Post = "", Return = "BaseResultList<ReaOrderDtlOfConfirmVO>", ReturnType = "ListReaOrderDtlOfConfirmVO")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UploadSupplyReaOrderDataByExcel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message RS_UDTO_UploadSupplyReaOrderDataByExcel();

        [ServiceContractDescription(Name = "客户端订单验收,获取某一订单的订单明细集合信息(可验收数大于0)", Desc = "客户端订单验收,获取某一订单的订单明细集合信息(可验收数大于0)", Url = "ReaManageService.svc/ST_UDTO_SearchReaOrderDtlOfConfirmVOByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaOrderDtlOfConfirmVO>", ReturnType = "ListReaOrderDtlOfConfirmVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaOrderDtlOfConfirmVOByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaOrderDtlOfConfirmVOByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "订单验收,新增客户端订单的验收信息", Desc = "订单验收,新增客户端订单的验收信息", Url = "ReaManageService.svc/ST_UDTO_AddReaSaleDocConfirmOfOrder", Get = "", Post = "ReaBmsCenSaleDocConfirm", Return = "BaseResultDataValue", ReturnType = "ReaBmsCenSaleDocConfirm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaSaleDocConfirmOfOrder", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaSaleDocConfirmOfOrder(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode);

        [ServiceContractDescription(Name = "订单验收,编辑更新客户端订单的验收信息", Desc = "订单验收,编辑更新客户端订单的验收信息", Url = "ReaManageService.svc/ST_UDTO_UpdateReaSaleDocConfirmOfOrder", Get = "", Post = "ReaBmsCenSaleDocConfirm", Return = "BaseResultBool", ReturnType = "ReaBmsCenSaleDocConfirm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaSaleDocConfirmOfOrder", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaSaleDocConfirmOfOrder(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, IList<ReaSaleDtlConfirmVO> dtEditList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode, string fields, string fieldsDtl);

        [ServiceContractDescription(Name = "订单验收,判断订单是否可以新增验收或继续验收", Desc = "订单验收,判断订单是否可以新增验收或继续验收", Url = "ReaManageService.svc/ST_UDTO_SearchOrderIsConfirmOfByOrderId?orderId={orderId}&confirmId={confirmId}", Get = "orderId={orderId}&confirmId={confirmId}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_SearchOrderIsConfirmOfByOrderId?orderId={orderId}&confirmId={confirmId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_SearchOrderIsConfirmOfByOrderId(long orderId, long confirmId);

        #endregion

        #region 订单管理-订单申请-模板选择
        [ServiceContractDescription(Name = "查询货品机构关系(HQL)", Desc = "查询货品机构关系(HQL)", Url = "ReaManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isGetGoodsQty={isGetGoodsQty}&templateId={templateId}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isGetGoodsQty={isGetGoodsQty}&templateId={templateId}", Post = "", Return = "BaseResultList<ReaGoodsOrgLink>", ReturnType = "ListReaGoodsOrgLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsOrgLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isGetGoodsQty={isGetGoodsQty}&templateId={templateId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isGetGoodsQty, long templateId);
        #endregion

        #region 供货验收

        [ServiceContractDescription(Name = "导入平台供货单,先判断该供货商的供货单号是否已存在本地供货单", Desc = "导入平台供货单,先判断该供货商的供货单号是否已存在本地供货单", Url = "ReaManageService.svc/ST_UDTO_SearchIsExistsReaBmsCenSaleDocByReaCompIDAndSaleDocNo?reaCompID={reaCompID}&saleDocNo={saleDocNo}", Get = "reaCompID={reaCompID}&saleDocNo={saleDocNo}", Post = "", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchIsExistsReaBmsCenSaleDocByReaCompIDAndSaleDocNo?reaCompID={reaCompID}&saleDocNo={saleDocNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_SearchIsExistsReaBmsCenSaleDocByReaCompIDAndSaleDocNo(long reaCompID, string saleDocNo);

        [ServiceContractDescription(Name = "导入平台供货单,按本地供应商ID及平台货品编码获取对照的供应商与货品信关系的信息", Desc = "导入平台供货单,按本地供应商ID及平台货品编码获取对照的供应商与货品信关系的信息", Url = "ReaManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByReaCompIDAndGoodsNoStr?reaCompID={reaCompID}&goodsNoStr={goodsNoStr}", Get = "reaCompID={reaCompID}&goodsNoStr={goodsNoStr}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDtlOfConfirmVO>", ReturnType = "ListReaBmsCenSaleDtlOfConfirmVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsOrgLinkByReaCompIDAndGoodsNoStr?reaCompID={reaCompID}&goodsNoStr={goodsNoStr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkByReaCompIDAndGoodsNoStr(long reaCompID, string goodsNoStr);

        [ServiceContractDescription(Name = "供货单验收,选择某一供货单判断供货单是否可以新增验收或继续验收", Desc = "供货单验收,选择某一供货单判断供货单是否可以新增验收或继续验收", Url = "ReaManageService.svc/ST_UDTO_SearchReaSaleIsConfirmOfBySaleDocID?saleDocID={saleDocID}&confirmId={confirmId}", Get = "saleDocID={saleDocID}&confirmId={confirmId}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_SearchReaSaleIsConfirmOfBySaleDocID?saleDocID={saleDocID}&confirmId={confirmId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_SearchReaSaleIsConfirmOfBySaleDocID(long saleDocID, long confirmId);

        [ServiceContractDescription(Name = "通过选择供货方+供货单号,获取本地待验收供货单信息", Desc = "通过选择供货方+供货单号,获取本地待验收供货单信息", Url = "ReaManageService.svc/ST_UDTO_GetLocalReaSaleDocOfConfirmBySaleDocNo", Get = "", Post = "reaServerCompCode,saleDocNo,reaServerLabcCode", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetLocalReaSaleDocOfConfirmBySaleDocNo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_GetLocalReaSaleDocOfConfirmBySaleDocNo(string reaServerCompCode, string saleDocNo, string reaServerLabcCode);

        [ServiceContractDescription(Name = "供货验收,获取某一供货单的供货明细集合信息(可验收数大于0)", Desc = "供货验收,获取某一供货单的供货明细集合信息(可验收数大于0)", Url = "ReaManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlOfConfirmVOListBySaleDocID?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&saleDocID={saleDocID}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&saleDocID={saleDocID}", Post = "", Return = "BaseResultList<ReaSaleDtlOfConfirmVO>", ReturnType = "ListReaSaleDtlOfConfirmVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenSaleDtlOfConfirmVOListBySaleDocID?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&saleDocID={saleDocID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlOfConfirmVOListBySaleDocID(int page, int limit, string fields, string where, string sort, bool isPlanish, long saleDocID);

        [ServiceContractDescription(Name = "供货验收,新增客户端供货单的验收信息", Desc = "供货验收,新增客户端供货单的验收信息", Url = "ReaManageService.svc/ST_UDTO_AddReaSaleDocConfirmOfSale", Get = "", Post = "ReaBmsCenSaleDocConfirm", Return = "BaseResultDataValue", ReturnType = "ReaBmsCenSaleDocConfirm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaSaleDocConfirmOfSale", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaSaleDocConfirmOfSale(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode);

        [ServiceContractDescription(Name = "供货验收,编辑更新客户端供货单的验收信息", Desc = "供货验收,编辑更新客户端供货单的验收信息", Url = "ReaManageService.svc/ST_UDTO_UpdateReaSaleDocConfirmOfSale", Get = "", Post = "ReaBmsCenSaleDocConfirm", Return = "BaseResultBool", ReturnType = "ReaBmsCenSaleDocConfirm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaSaleDocConfirmOfSale", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaSaleDocConfirmOfSale(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, IList<ReaSaleDtlConfirmVO> dtEditList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode, string fields, string fieldsDtl);

        [ServiceContractDescription(Name = "查询供货验收明细单表(HQL)", Desc = "查询供货验收明细单表(HQL)", Url = "ReaManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlConfirmByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCenSaleDtlConfirm>", ReturnType = "ListBmsCenSaleDtlConfirm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenSaleDtlConfirmByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlConfirmByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        #endregion

        #region 验收处理
        [ServiceContractDescription(Name = "验收货品扫码服务", Desc = "定制验收货品扫码服务", Url = "ReaManageService.svc/ST_UDTO_SearchReaGoodsScanCodeVOOfConfirm?reaCompID={reaCompID}&serialNo={serialNo}&scanCodeType={scanCodeType}&bobjectID={bobjectID}", Get = "?reaCompID={reaCompID}&serialNo={serialNo}&scanCodeType={scanCodeType}&bobjectID={bobjectID}", Post = "", Return = "BaseResultList<ReaGoodsScanCodeVO>", ReturnType = "ReaGoodsScanCodeVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsScanCodeVOOfConfirm?reaCompID={reaCompID}&serialNo={serialNo}&scanCodeType={scanCodeType}&bobjectID={bobjectID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsScanCodeVOOfConfirm(long reaCompID, string serialNo, string scanCodeType, long bobjectID);

        [ServiceContractDescription(Name = "订单/供货单验收,获取某一订单/供货单的明细集合信息(可验收数大于0)", Desc = "订单/供货单验收,获取某一订单/供货单的明细集合信息(可验收数大于0)", Url = "ReaManageService.svc/ST_UDTO_SearchReaBmsCenSaleDtlConfirmVOOfConfirmTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&confirmType={confirmType}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&confirmType={confirmType}", Post = "", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ListReaBmsQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsCenSaleDtlConfirmVOOfConfirmTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&confirmType={confirmType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlConfirmVOOfConfirmTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, string confirmType);

        [ServiceContractDescription(Name = "更新客户端验收单信息", Desc = "更新客户端验收单信息", Url = "ReaManageService.svc/ST_UDTO_UpdateReaSaleDocConfirmAndDtl", Get = "", Post = "BmsCenSaleDocConfirm", Return = "BaseResultBool", ReturnType = "BmsCenSaleDocConfirm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaSaleDocConfirmAndDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaSaleDocConfirmAndDtl(ReaBmsCenSaleDocConfirm entity, IList<ReaBmsCenSaleDtlConfirm> dtEditList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode, string fields, string fieldsDtl);

        [ServiceContractDescription(Name = "对客户端验收信息进行确认验收操作", Desc = "对客户端验收信息进行确认验收操作", Url = "ReaManageService.svc/ST_UDTO_UpdateReaBmsCenSaleDocConfirmOfConfirmType", Get = "", Post = "BmsCenSaleDocConfirm", Return = "BaseResultBool", ReturnType = "BmsCenSaleDocConfirm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCenSaleDocConfirmOfConfirmType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDocConfirmOfConfirmType(ReaBmsCenSaleDocConfirm entity, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode, string fields, string confirmType);

        [ServiceContractDescription(Name = "客户端验收明细删除操作", Desc = "客户端验收明细删除操作", Url = "ReaManageService.svc/ST_UDTO_DelReaBmsCenSaleDtlConfirm?id={id}&confirmSourceType={confirmSourceType}", Get = "id={id}&confirmSourceType={confirmSourceType}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsCenSaleDtlConfirm?id={id}&confirmSourceType={confirmSourceType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsCenSaleDtlConfirm(long id, string confirmSourceType);

        #endregion

        #region 入库管理

        [ServiceContractDescription(Name = "查询ReaBmsInDoc(HQL),支持按入库明细(供应商)查询", Desc = "查询ReaBmsInDoc(HQL),支持按入库明细(供应商)查询", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsInDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&dtlHql={dtlHql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&dtlHql={dtlHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsInDoc>", ReturnType = "ListReaBmsTransferDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsInDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&dtlHql={dtlHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsInDocByHQL(int page, int limit, string fields, string where, string dtlHql, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "接口提取数据转新增入库", Desc = "接口提取数据转新增入库", Url = "ReaManageService.svc/RS_UDTO_AddReaBmsInDocAndDtlByInterface", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsInDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddReaBmsInDocAndDtlByInterface", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddReaBmsInDocAndDtlByInterface(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode, string iSNeedCreateBarCode);

        [ServiceContractDescription(Name = "接口供货转入库,编辑更新入库主单信息及编辑入库明细集合信息", Desc = "接口供货转入库,编辑更新入库主单信息及编辑入库明细集合信息", Url = "ReaManageService.svc/RS_UDTO_UpdateReaBmsInDocAndInDtlListByInterface", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultBool", ReturnType = "ReaBmsInDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsInDocAndInDtlListByInterface", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsInDocAndInDtlListByInterface(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> inDtlList, string iSNeedCreateBarCode, string fieldsDtl, string fields, string codeScanningMode);

        [ServiceContractDescription(Name = "接口供货转入库,确认入库", Desc = "接口供货转入库,确认入库", Url = "ReaManageService.svc/RS_UDTO_UpdateConfirmInDocByInterface?id={id}&codeScanningMode={codeScanningMode}&iSNeedCreateBarCode={iSNeedCreateBarCode}", Get = "id={id}&codeScanningMode={codeScanningMode}&iSNeedCreateBarCode={iSNeedCreateBarCode}", Post = "", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateConfirmInDocByInterface?id={id}&codeScanningMode={codeScanningMode}&iSNeedCreateBarCode={iSNeedCreateBarCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateConfirmInDocByInterface(long id, string codeScanningMode, string iSNeedCreateBarCode);

        [ServiceContractDescription(Name = "编辑更新入库主单信息及编辑入库明细集合信息", Desc = "编辑更新入库主单信息及编辑入库明细集合信息", Url = "ReaManageService.svc/RS_UDTO_UpdateReaBmsInDocAndInDtlList", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultBool", ReturnType = "ReaBmsInDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsInDocAndInDtlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsInDocAndInDtlList(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> inDtlList, string fieldsDtl, string fields, string codeScanningMode);

        #endregion

        #region 出库/移库/退库入库
        [ServiceContractDescription(Name = "出库/移库/退库入库选择的库存货品信息(HQL)", Desc = "出库/移库/退库入库选择的库存货品信息(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtl?page={page}&limit={limit}&fields={fields}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&qtyHql={qtyHql}&sort={sort}&isPlanish={isPlanish}&isMergeInDocNo={isMergeInDocNo}", Get = "page={page}&limit={limit}&fields={fields}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&qtyHql={qtyHql}&sort={sort}&isPlanish={isPlanish}&isMergeInDocNo={isMergeInDocNo}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsQtyDtl?page={page}&limit={limit}&fields={fields}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&qtyHql={qtyHql}&sort={sort}&isPlanish={isPlanish}&isMergeInDocNo={isMergeInDocNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtl(string deptGoodsHql, string reaGoodsHql, string qtyHql, int page, int limit, string fields, string sort, bool isPlanish, bool isMergeInDocNo);

        [ServiceContractDescription(Name = "出库扫码,根据货品条码获取货品相关信息", Desc = "出库扫码,根据货品条码获取货品相关信息", Url = "ReaManageService.svc/RS_UDTO_SearchReaGoodsByBarCode?barcode={barcode}&fields={fields}&isPlanish={isPlanish}", Get = "barcode={barcode}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaGoodsByBarCode?barcode={barcode}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaGoodsByBarCode(string barcode, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "出库/移库扫码,根据货品条码获取机构货品关系相关信息", Desc = "出库/移库扫码,根据货品条码获取机构货品关系相关信息", Url = "ReaManageService.svc/RS_UDTO_SearchReaGoodsOrgLinkByBarCode?barcode={barcode}&fields={fields}&isPlanish={isPlanish}", Get = "barcode={barcode}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaGoodsOrgLinkByBarCode?barcode={barcode}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaGoodsOrgLinkByBarCode(string barcode, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "出库/移库/退库入库扫码,通过扫码定位出库存货品信息", Desc = "出库/移库/退库入库扫码,通过扫码定位出库存货品信息", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlByBarCode?storageId={storageId}&placeId={placeId}&barcode={barcode}&fields={fields}&isPlanish={isPlanish}&barcodeOperType={barcodeOperType}&isMergeInDocNo={isMergeInDocNo}&isAllowOfALLStorage={isAllowOfALLStorage}", Get = "storageId={storageId}&placeId={placeId}&barcode={barcode}&fields={fields}&isPlanish={isPlanish}&barcodeOperType={barcodeOperType}&isMergeInDocNo={isMergeInDocNo}&isAllowOfALLStorage={isAllowOfALLStorage}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsQtyDtlByBarCode?storageId={storageId}&placeId={placeId}&barcode={barcode}&fields={fields}&isPlanish={isPlanish}&barcodeOperType={barcodeOperType}&isMergeInDocNo={isMergeInDocNo}&isAllowOfALLStorage={isAllowOfALLStorage}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtlByBarCode(string storageId, string placeId, string barcode, string fields, bool isPlanish, string barcodeOperType, bool isMergeInDocNo, bool isAllowOfALLStorage);

        [ServiceContractDescription(Name = "退库入库", Desc = "退库入库", Url = "ReaManageService.svc/RS_UDTO_AddInputReaGoodsByReturn", Get = "", Post = "inputType,inDoc,listReaBmsInDtl", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddInputReaGoodsByReturn", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddInputReaGoodsByReturn(int inputType, ReaBmsInDoc inDoc, IList<ReaBmsInDtl> listReaBmsInDtl);

        [ServiceContractDescription(Name = "退库入库调用退库接口失败后,物理删除退库入库的相关信息", Desc = "退库入库调用退库接口失败后,物理删除退库入库的相关信息", Url = "ReaManageService.svc/RS_UDTO_DelDelReaBmsInDocByReturnOfInDocId?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RS_UDTO_DelDelReaBmsInDocByReturnOfInDocId?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_DelDelReaBmsInDocByReturnOfInDocId(long id);

        [ServiceContractDescription(Name = "查询出库明细表(HQL)", Desc = "查询出库明细表(HQL)", Url = "ReaManageService.svc/ST_UDTO_SearchReaBmsOutDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsOutDtl>", ReturnType = "ListReaBmsOutDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsOutDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsOutDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询入库明细表(HQL)", Desc = "查询入库明细表(HQL)", Url = "ReaManageService.svc/ST_UDTO_SearchReaBmsInDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsInDtl>", ReturnType = "ListReaBmsInDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        #endregion

        #region 盘库管理
        [ServiceContractDescription(Name = "查询Rea_BmsCheckDtl(HQL)", Desc = "查询Rea_BmsCheckDtl(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsCheckDtlByHQL?page={page}&limit={limit}&fields={fields}&checkHql={checkHql}&where={where}&reaGoodHql={reaGoodHql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&checkHql={checkHql}&where={where}&reaGoodHql={reaGoodHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsCheckDtl>", ReturnType = "ListReaBmsCheckDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsCheckDtlByHQL?page={page}&limit={limit}&fields={fields}&checkHql={checkHql}&where={where}&reaGoodHql={reaGoodHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsCheckDtlByHQL(int page, int limit, string fields, string checkHql, string where, string reaGoodHql, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取新增盘库明细货品信息", Desc = "获取新增盘库明细货品信息", Url = "ReaManageService.svc/RS_UDTO_SearchAddReaBmsCheckDtlByHQL?page={page}&limit={limit}&fields={fields}&docEntity={docEntity}&days={days}&reaGoodHql={reaGoodHql}&sort={sort}&isPlanish={isPlanish}&preIsVO={preIsVO}&zeroQtyDays={zeroQtyDays}", Get = "page={page}&limit={limit}&fields={fields}&docEntity={docEntity}&days={days}&reaGoodHql={reaGoodHql}&sort={sort}&isPlanish={isPlanish}&preIsVO={preIsVO}&zeroQtyDays={zeroQtyDays}", Post = "", Return = "BaseResultList<ReaBmsCheckDtl>", ReturnType = "ListReaBmsCheckDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchAddReaBmsCheckDtlByHQL?page={page}&limit={limit}&fields={fields}&docEntity={docEntity}&days={days}&reaGoodHql={reaGoodHql}&sort={sort}&isPlanish={isPlanish}&preIsVO={preIsVO}&zeroQtyDays={zeroQtyDays}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchAddReaBmsCheckDtlByHQL(int page, int limit, string fields, string docEntity, string reaGoodHql, int days, string sort, bool isPlanish, bool preIsVO, int zeroQtyDays);

        [ServiceContractDescription(Name = "新增盘库", Desc = "新增盘库", Url = "ReaManageService.svc/RS_UDTO_AddReaBmsCheckDoc", Get = "", Post = "ReaBmsCheckDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsCheckDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddReaBmsCheckDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddReaBmsCheckDoc(ReaBmsCheckDoc entity, int mergeType);

        [ServiceContractDescription(Name = "新增盘库主单及盘库明细信息", Desc = "新增盘库主单及盘库明细信息", Url = "ReaManageService.svc/RS_UDTO_AddReaBmsCheckDocAndDtlList", Get = "", Post = "ReaBmsCheckDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsCheckDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddReaBmsCheckDocAndDtlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddReaBmsCheckDocAndDtlList(ReaBmsCheckDoc entity, IList<ReaBmsCheckDtl> dtAddList, bool isTakenFromQty);

        [ServiceContractDescription(Name = "修改编辑盘库及明细信息指定的属性", Desc = "修改编辑盘库及明细信息指定的属性", Url = "ReaManageService.svc/RS_UDTO_UpdateReaBmsCheckDocAndDtl", Get = "", Post = "ReaBmsCheckDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsCheckDocAndDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsCheckDocAndDtl(ReaBmsCheckDoc entity, string fields, IList<ReaBmsCheckDtl> dtEditList, string fieldsDtl);

        [ServiceContractDescription(Name = "盘库差异调整,依盘库单Id获取盘盈入库信息", Desc = "盘库差异调整,依盘库单Id获取盘盈入库信息", Url = "ReaManageService.svc/ST_UDTO_SearchReaBmsInDocOfCheckDocID?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "ReaBmsInDoc", ReturnType = "ReaBmsInDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDocOfCheckDocID?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDocOfCheckDocID(long id, bool isPlanish, string fields);

        [ServiceContractDescription(Name = "盘库差异调整,依盘库单Id获取盘盈入库明细信息", Desc = "盘库差异调整,依盘库单Id获取盘盈入库明细信息", Url = "ReaManageService.svc/ST_UDTO_SearchReaBmsInDtlListOfCheckDocID?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsInDtl>", ReturnType = "ReaBmsInDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDtlListOfCheckDocID?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDtlListOfCheckDocID(long id, bool isPlanish, string fields);

        [ServiceContractDescription(Name = "盘库差异调整,依盘库单Id获取盘亏出库信息", Desc = "盘库差异调整,依盘库单Id获取盘亏出库信息", Url = "ReaManageService.svc/ST_UDTO_SearchReaBmsOutDocOfCheckDocID?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "ReaBmsOutDoc", ReturnType = "ReaBmsOutDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsOutDocOfCheckDocID?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsOutDocOfCheckDocID(long id, bool isPlanish, string fields);

        [ServiceContractDescription(Name = "盘库差异调整,依盘库单Id获取盘亏出库明细信息", Desc = "盘库差异调整,依盘库单Id获取盘亏出库明细信息", Url = "ReaManageService.svc/ST_UDTO_SearchReaBmsOutDtlListOfCheckDocID?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsInDtl>", ReturnType = "ReaBmsInDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsOutDtlListOfCheckDocID?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsOutDtlListOfCheckDocID(long id, bool isPlanish, string fields);

        [ServiceContractDescription(Name = "依盘库Id保存盘库差异调整的盘盈入库及入库明细信息", Desc = "依盘库Id保存盘库差异调整的盘盈入库及入库明细信息", Url = "ReaManageService.svc/ST_UDTO_AddReaBmsInDocAndDtlOfCheckDocID", Get = "", Post = "ReaBmsCheckDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsCheckDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsInDocAndDtlOfCheckDocID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsInDocAndDtlOfCheckDocID(long checkDocID, ReaBmsInDoc inDoc, IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode);

        [ServiceContractDescription(Name = "依盘库Id保存盘库差异调整的盘亏出库及出库明细信息", Desc = "依盘库Id保存盘库差异调整的盘亏出库及出库明细信息", Url = "ReaManageService.svc/ST_UDTO_AddReaBmsOutDocAndDtlOfCheckDocID", Get = "", Post = "ReaBmsCheckDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsCheckDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsOutDocAndDtlOfCheckDocID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsOutDocAndDtlOfCheckDocID(long checkDocID, ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> dtAddList, string codeScanningMode);

        [ServiceContractDescription(Name = "获取生成PDF格式的盘库单", Desc = "获取生成PDF格式的盘库单", Url = "ReaManageService.svc/RS_UDTO_GetReaBmsCheckDocAndDtlOfPdf?id={id}&sort={sort}&operateType={operateType}&frx={frx}", Get = "id={id}&sort={sort}&operateType={operateType}&frx={frx}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_GetReaBmsCheckDocAndDtlOfPdf?id={id}&sort={sort}&operateType={operateType}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream RS_UDTO_GetReaBmsCheckDocAndDtlOfPdf(long id, string sort, long operateType, string frx);

        [ServiceContractDescription(Name = "获取仪器试剂VO信息,按试剂分组,按当前登录帐号的所属部门进行仪器过滤", Desc = "获取仪器试剂VO信息,按试剂分组,按当前登录帐号的所属部门进行仪器过滤", Url = "ReaManageService.svc/ST_UDTO_SearchReaEquipReagentLinkVOList", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaEquipReagentLinkVOList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaEquipReagentLinkVOList();

        [ServiceContractDescription(Name = "按登录帐号所属部门获取部门仪器信息(HQL)", Desc = "按登录帐号所属部门获取部门仪器信息(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaTestEquipLabByEmpDeptHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaTestEquipLab>", ReturnType = "ListReaTestEquipLab")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaTestEquipLabByEmpDeptHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaTestEquipLabByEmpDeptHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);
        #endregion

        #region 库存结转

        [ServiceContractDescription(Name = "库存结转时,判断当前月是否已生成过库存结转单", Desc = "库存结转时,判断当前月是否已生成过库存结转单", Url = "ReaManageService.svc/RS_UDTO_GetJudgeISAddReaBmsQtyBalanceDoc?beginDate={beginDate}&endDate={endDate}", Get = "?beginDate={beginDate}&endDate={endDate}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RS_UDTO_GetJudgeISAddReaBmsQtyBalanceDoc?beginDate={beginDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_GetJudgeISAddReaBmsQtyBalanceDoc(string beginDate, string endDate);

        [ServiceContractDescription(Name = "按库存结转单新增库存结转", Desc = "按库存结转单新增库存结转", Url = "ReaManageService.svc/RS_UDTO_AddReaBmsQtyBalanceDocOfQtyBalance", Get = "", Post = "entity,isCover", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddReaBmsQtyBalanceDocOfQtyBalance", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddReaBmsQtyBalanceDocOfQtyBalance(ReaBmsQtyBalanceDoc entity, bool isCover, string beginDate, string endDate);

        [ServiceContractDescription(Name = "启用/禁用库存结转单", Desc = "启用/禁用库存结转单", Url = "ReaManageService.svc/RS_UDTO_UpdateVisibleReaBmsQtyBalanceDocById?id={id}&visible={visible}", Get = "?id={id}&visible={visible}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RS_UDTO_UpdateVisibleReaBmsQtyBalanceDocById?id={id}&visible={visible}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateVisibleReaBmsQtyBalanceDocById(long id, bool visible);
        #endregion

        #region 结转报表
        [ServiceContractDescription(Name = "按库存结转单新增结转报表", Desc = "按库存结转单新增结转报表", Url = "ReaManageService.svc/RS_UDTO_AddQtyBalanceReportOfQtyBalanceDtlList", Get = "", Post = "entity", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddQtyBalanceReportOfQtyBalanceDtlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddQtyBalanceReportOfQtyBalanceDtlList(ReaBmsQtyMonthBalanceDoc entity, string labCName);

        [ServiceContractDescription(Name = "按库存变化操作记录新增结转报表", Desc = "按库存变化操作记录新增结转报表", Url = "ReaManageService.svc/RS_UDTO_AddQtyBalanceReportOfQtyDtlOperList", Get = "", Post = "entity", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddQtyBalanceReportOfQtyDtlOperList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddQtyBalanceReportOfQtyDtlOperList(ReaBmsQtyMonthBalanceDoc entity, string labCName);

        [ServiceContractDescription(Name = "取消结转报表", Desc = "取消结转报表", Url = "ReaManageService.svc/ST_UDTO_UpdateCancelReaBmsQtyMonthBalanceDocById?id={id}", Get = "?id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_UpdateCancelReaBmsQtyMonthBalanceDocById?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCancelReaBmsQtyMonthBalanceDocById(long id);

        [ServiceContractDescription(Name = "依结转报表ID获取月结统计明细数据", Desc = "依结转报表ID获取月结统计明细数据", Url = "ReaManageService.svc/RS_UDTO_SearchQtyMonthBalanceDtlListById?page={page}&limit={limit}&fields={fields}&id={id}&isPlanish={isPlanish}", Get = "?page={page}&limit={limit}&fields={fields}&id={id}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyMonthBalanceDtl>", ReturnType = "ListReaBmsQtyMonthBalanceDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchQtyMonthBalanceDtlListById?page={page}&limit={limit}&fields={fields}&id={id}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchQtyMonthBalanceDtlListById(long id, int page, int limit, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "获取生成PDF格式的结转报表", Desc = "获取生成PDF格式的结转报表", Url = "ReaManageService.svc/RS_UDTO_GetQtyMonthBalanceAndDtlOfPdf?id={id}&operateType={operateType}&frx={frx}&labCName={labCName}", Get = "id={id}&operateType={operateType}&frx={frx}&labCName={labCName}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_GetQtyMonthBalanceAndDtlOfPdf?id={id}&operateType={operateType}&frx={frx}&labCName={labCName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream RS_UDTO_GetQtyMonthBalanceAndDtlOfPdf(long id, long operateType, string frx, string labCName);
        #endregion

        #region 条码打印
        [ServiceContractDescription(Name = "依入库单ID获取(生成)货品条码信息", Desc = "依入库单ID获取(生成)货品条码信息", Url = "ReaManageService.svc/RS_UDTO_SearchReaGoodsPrintBarCodeVOListByInDocId?page={page}&limit={limit}&inDocId={inDocId}&dtlIdStr={dtlIdStr}&boxHql={boxHql}&fields={fields}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&inDocId={inDocId}&dtlIdStr={dtlIdStr}&boxHql={boxHql}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaGoodsPrintBarCodeVOListByInDocId?page={page}&limit={limit}&inDocId={inDocId}&dtlIdStr={dtlIdStr}&boxHql={boxHql}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaGoodsPrintBarCodeVOListByInDocId(long inDocId, string dtlIdStr, string boxHql, string fields, bool isPlanish, int page, int limit);

        [ServiceContractDescription(Name = "依入库明细ID获取(生成)货品条码信息", Desc = "依入库明细ID获取(生成)货品条码信息", Url = "ReaManageService.svc/RS_UDTO_SearchReaGoodsPrintBarCodeVOListByInDtlId?page={page}&limit={limit}&inDtlId={inDtlId}&boxHql={boxHql}&fields={fields}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&inDtlId={inDtlId}&boxHql={boxHql}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaGoodsPrintBarCodeVOListByInDtlId?page={page}&limit={limit}&inDtlId={inDtlId}&boxHql={boxHql}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaGoodsPrintBarCodeVOListByInDtlId(long inDtlId, string boxHql, string fields, bool isPlanish, int page, int limit);

        [ServiceContractDescription(Name = "依供货单ID获取(生成)货品条码信息", Desc = "依供货单ID获取(生成)货品条码信息", Url = "ReaManageService.svc/RS_UDTO_SearchReaGoodsPrintBarCodeVOListBySaledocId?page={page}&limit={limit}&saledocId={saledocId}&dtlIdStr={dtlIdStr}&fields={fields}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&saledocId={saledocId}&dtlIdStr={dtlIdStr}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaGoodsPrintBarCodeVOListBySaledocId?page={page}&limit={limit}&saledocId={saledocId}&dtlIdStr={dtlIdStr}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaGoodsPrintBarCodeVOListBySaledocId(long saledocId, string dtlIdStr, string fields, bool isPlanish, int page, int limit);

        [ServiceContractDescription(Name = "依供货明细ID获取(生成)货品条码生成信息", Desc = "依供货明细ID获取(生成)货品条码生成信息", Url = "ReaManageService.svc/RS_UDTO_SearchReaGoodsPrintBarCodeVOListBySaleDtlId?page={page}&limit={limit}&saleDtlId={saleDtlId}&fields={fields}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&saleDtlId={saleDtlId}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaGoodsPrintBarCodeVOListBySaleDtlId?page={page}&limit={limit}&saleDtlId={saleDtlId}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaGoodsPrintBarCodeVOListBySaleDtlId(long saleDtlId, string fields, bool isPlanish, int page, int limit);

        [ServiceContractDescription(Name = "条码打印后根据选中的条码数据更新条码打印次数", Desc = "条码打印后根据选中的条码数据更新条码打印次数", Url = "ReaManageService.svc/RS_UDTO_UpdatePrintCount", Get = "", Post = "lotList,lotType,boxList", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdatePrintCount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdatePrintCount(IList<long> lotList, string lotType, IList<long> boxList);
        #endregion

        #region 货品导入导出
        [ServiceContractDescription(Name = "导入货品Excel文件", Desc = "导入货品Excel文件", Url = "ReaManageService.svc/RS_UDTO_UploadReaGoodsDataByExcel", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UploadReaGoodsDataByExcel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message RS_UDTO_UploadReaGoodsDataByExcel();

        [ServiceContractDescription(Name = "货品信息列表导出Excel文件路径", Desc = "货品信息列表导出Excel文件路径", Url = "ReaManageService.svc/RS_UDTO_GetReaGoodsReportExcelPath", Get = "", Post = "reportType,idList,where,isHeader", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_GetReaGoodsReportExcelPath", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message RS_UDTO_GetReaGoodsReportExcelPath();

        [ServiceContractDescription(Name = "对象信息列表导出Excel文件路径", Desc = "对象信息列表导出Excel文件路径", Url = "ReaManageService.svc/RS_UDTO_GetEntityListExcelPath", Get = "", Post = "entityName,listTitle,idList,where,sort,fieldJson,version", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_GetEntityListExcelPath", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message RS_UDTO_GetEntityListExcelPath();

        [ServiceContractDescription(Name = "下载Excel文件", Desc = "下载Excel文件", Url = "ReaManageService.svc/RS_UDTO_DownLoadExcel?fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}", Get = "fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/RS_UDTO_DownLoadExcel?fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}")]
        [OperationContract]
        Stream RS_UDTO_DownLoadExcel(string fileName, string downFileName, int isUpLoadFile, int operateType);
        #endregion

        #region 供货管理
        [ServiceContractDescription(Name = "新增供货单及供货明细", Desc = "新增供货单及供货明细", Url = "ReaManageService.svc/RS_UDTO_AddReaBmsCenSaleDocAndDtl", Get = "", Post = "entity,dtAddList", Return = "BaseResultDataValue", ReturnType = "ReaBmsCenSaleDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddReaBmsCenSaleDocAndDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddReaBmsCenSaleDocAndDtl(ReaBmsCenSaleDoc entity, IList<ReaBmsCenSaleDtl> dtAddList);

        [ServiceContractDescription(Name = "修改供货单指定的属性及供货明细", Desc = "修改供货单指定的属性及供货明细", Url = "ReaManageService.svc/RS_UDTO_UpdateReaBmsCenSaleDocAndDtl", Get = "", Post = "ReaBmsCheckDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsCenSaleDocAndDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsCenSaleDocAndDtl(ReaBmsCenSaleDoc entity, string fields, IList<ReaBmsCenSaleDtl> dtAddList, IList<ReaBmsCenSaleDtl> dtEditList, string dtlFields);

        [ServiceContractDescription(Name = "供货审核通过后,处理生成供货条码信息", Desc = "供货审核通过后,处理生成供货条码信息", Url = "ReaManageService.svc/RS_UDTO_AddCreateBarcodeInfoOfSaleDocId?saleDocId={saleDocId}", Get = "saleDocId={saleDocId}", Post = "", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddCreateBarcodeInfoOfSaleDocId?saleDocId={saleDocId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_AddCreateBarcodeInfoOfSaleDocId(long saleDocId);

        [ServiceContractDescription(Name = "客户端导入平台供货单,新增供货单及明细,供货条码关系信息", Desc = "客户端导入平台供货单,新增供货单及明细,供货条码关系信息", Url = "ReaManageService.svc/RS_UDTO_AddReaBmsCenSaleDocAndDtlVO", Get = "", Post = "entity,dtAddList", Return = "BaseResultDataValue", ReturnType = "ReaBmsCenSaleDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddReaBmsCenSaleDocAndDtlVO", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddReaBmsCenSaleDocAndDtlVO(ReaBmsCenSaleDoc entity, IList<ReaBmsCenSaleDtlVO> dtAddList);

        #endregion

        #region 客户端字典与平台供应商字典同步
        [ServiceContractDescription(Name = "试剂耗材月订货清单", Desc = "试剂耗材月订货清单", Url = "ReaManageService.svc/ST_UDTO_GetLabDictionaryExportToComp?reaServerCompCode={reaServerCompCode}&reaServerLabcCode={reaServerLabcCode}", Get = "reaServerCompCode={reaServerCompCode}&reaServerLabcCode={reaServerLabcCode}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetLabDictionaryExportToComp?reaServerCompCode={reaServerCompCode}&reaServerLabcCode={reaServerLabcCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_GetLabDictionaryExportToComp(string reaServerCompCode, string reaServerLabcCode);

        [ServiceContractDescription(Name = "平台供应商导入客户端某一供应商的字典信息", Desc = "平台供应商导入客户端某一供应商的字典信息", Url = "ReaManageService.svc/ST_UDTO_AddUploadLabDictionaryOfCompSync", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddUploadLabDictionaryOfCompSync", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message ST_UDTO_AddUploadLabDictionaryOfCompSync();
        #endregion

        #region 撤消管理
        [ServiceContractDescription(Name = "撤消入库", Desc = "撤消入库", Url = "ReaManageService.svc/ST_UDTO_UpdateCancelReaBmsInDocById?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_UpdateCancelReaBmsInDocById?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCancelReaBmsInDocById(long id);
        #endregion

        #region 库存合并查询及库存预警
        [ServiceContractDescription(Name = "获取库存查询结果,返回按合并项合并后List", Desc = "获取库存查询结果,返回按合并项合并后List", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlListByGroupType?groupType={groupType}&page={page}&limit={limit}&fields={fields}&where={where}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Get = "groupType={groupType}&page={page}&limit={limit}&fields={fields}&where={where}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ListReaBmsQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsQtyDtlListByGroupType?groupType={groupType}&page={page}&limit={limit}&fields={fields}&where={where}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtlListByGroupType(int groupType, int page, int limit, string fields, string where, string deptGoodsHql, string reaGoodsHql, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取库存查询结果,返回按合并项合并后EntityList", Desc = "获取库存查询结果,返回按合并项合并后EntityList", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlEntityListByGroupType?groupType={groupType}&page={page}&limit={limit}&fields={fields}&where={where}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}&isEmpPermission={isEmpPermission}", Get = "groupType={groupType}&page={page}&limit={limit}&fields={fields}&where={where}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}&isEmpPermission={isEmpPermission}", Post = "", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ListReaBmsQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsQtyDtlEntityListByGroupType?groupType={groupType}&page={page}&limit={limit}&fields={fields}&where={where}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}&isEmpPermission={isEmpPermission}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtlEntityListByGroupType(int groupType, int page, int limit, string fields, string where, string deptGoodsHql, string reaGoodsHql, string sort, bool isPlanish, bool isEmpPermission);

        [ServiceContractDescription(Name = "库存预警，warningType：预警类型(1:低库存：2：高库存)", Desc = "库存预警，warningType：预警类型(1:低库存：2：高库存)", Url = "ReaManageService.svc/ST_UDTO_SearchReaBmsQtyDtlListByStockWarning?page={page}&limit={limit}&fields={fields}&where={where}&reaGoodsHql={reaGoodsHql}&isPlanish={isPlanish}&warningType={warningType}&groupType={groupType}&storePercent={storePercent}&comparisonType={comparisonType}&monthValue={monthValue}&sort={sort}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&reaGoodsHql={reaGoodsHql}&isPlanish={isPlanish}&warningType={warningType}&groupType={groupType}&storePercent={storePercent}&comparisonType={comparisonType}&monthValue={monthValue}&sort={sort}", Post = "", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ListReaBmsQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyDtlListByStockWarning?page={page}&limit={limit}&fields={fields}&where={where}&reaGoodsHql={reaGoodsHql}&isPlanish={isPlanish}&warningType={warningType}&groupType={groupType}&storePercent={storePercent}&comparisonType={comparisonType}&monthValue={monthValue}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlListByStockWarning(int page, int limit, string fields, string where, string reaGoodsHql, bool isPlanish, int warningType, int groupType, float storePercent, string comparisonType, int monthValue, string sort);

        [ServiceContractDescription(Name = "登录成功后,获取库存预警,效期预警,注册证预警提示信息", Desc = "登录成功后,获取库存预警,效期预警,注册证预警提示信息", Url = "ReaManageService.svc/RS_UDTO_GetReaGoodsWarningAlertInfo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_GetReaGoodsWarningAlertInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_GetReaGoodsWarningAlertInfo();
        #endregion

        #region PDF清单报表
        [ServiceContractDescription(Name = "将选择的多个采购申请单按'供货商+货品编码+包装单位'合并后,生成PDF报表文件", Desc = "将选择的多个采购申请单按'供货商+货品编码+包装单位'合并后,生成PDF报表文件", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsReqDocMergeReportOfPdfByIdStr?reaReportClass={reaReportClass}&breportType={breportType}&idStr={idStr}&operateType={operateType}&frx={frx}", Get = "reaReportClass={reaReportClass}&breportType={breportType}&idStr={idStr}&operateType={operateType}&frx={frx}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsReqDocMergeReportOfPdfByIdStr?reaReportClass={reaReportClass}&breportType={breportType}&idStr={idStr}&operateType={operateType}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream RS_UDTO_SearchReaBmsReqDocMergeReportOfPdfByIdStr(string reaReportClass, string breportType, string idStr, long operateType, string frx);

        [ServiceContractDescription(Name = "将选择的多个订单按供货商+货品编码+包装单位合并后,生成PDF报表文件", Desc = "将选择的多个订单按供货商+货品编码+包装单位合并后,生成PDF报表文件", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsCenOrderDocOfPdfByIdStr?reaReportClass={reaReportClass}&breportType={breportType}&idStr={idStr}&operateType={operateType}&frx={frx}", Get = "reaReportClass={reaReportClass}&breportType={breportType}&idStr={idStr}&operateType={operateType}&frx={frx}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsCenOrderDocOfPdfByIdStr?reaReportClass={reaReportClass}&breportType={breportType}&idStr={idStr}&operateType={operateType}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream RS_UDTO_SearchReaBmsCenOrderDocOfPdfByIdStr(string reaReportClass, string breportType, string idStr, long operateType, string frx);

        [ServiceContractDescription(Name = "获取公共模板目录的子文件夹中的所有报表模板文件", Desc = "获取公共模板目录的子文件夹中的所有报表模板文件", Url = "ReaManageService.svc/RS_UDTO_SearchPublicTemplateFileInfoByType?publicTemplateDir={publicTemplateDir}", Get = "publicTemplateDir={publicTemplateDir}", Post = "", Return = "BaseResultList<JObject>", ReturnType = "ListJObject")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPublicTemplateFileInfoByType?publicTemplateDir={publicTemplateDir}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPublicTemplateFileInfoByType(string publicTemplateDir);

        [ServiceContractDescription(Name = "将选择的公共报表模板新增保存到当前实验室的报表模板表", Desc = "将选择的公共报表模板新增保存到当前实验室的报表模板表", Url = "ReaManageService.svc/RS_UDTO_AddBTemplateOfPublicTemplate", Get = "", Post = "JObject", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddBTemplateOfPublicTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_AddBTemplateOfPublicTemplate(string entityList, long labId, string labCName);

        [ServiceContractDescription(Name = "获取当前机构的某一模板类型的全部报表模板信息,如果当前机构未维护,取该模板类型的公共报表模板信息", Desc = "获取当前机构的某一模板类型的全部报表模板信息,如果当前机构未维护,取该模板类型的公共报表模板信息", Url = "ReaManageService.svc/RS_UDTO_SearchBTemplateByLabIdAndType?labId={labId}&breportType={breportType}&publicTemplateDir={publicTemplateDir}", Get = "labId={labId}&breportType={breportType}&publicTemplateDir={publicTemplateDir}", Post = "", Return = "BaseResultList<BTemplate>", ReturnType = "ListBTemplate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchBTemplateByLabIdAndType?labId={labId}&breportType={breportType}&publicTemplateDir={publicTemplateDir}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchBTemplateByLabIdAndType(long labId, long breportType, string publicTemplateDir);

        [ServiceContractDescription(Name = "获取各业务报表(如采购申请,订货清单等)PDF文件", Desc = "获取各业务报表(如采购申请,订货清单等)PDF文件", Url = "ReaManageService.svc/RS_UDTO_SearchBusinessReportOfPdfById?reaReportClass={reaReportClass}&breportType={breportType}&id={id}&operateType={operateType}&frx={frx}", Get = "reaReportClass={reaReportClass}&breportType={breportType}&id={id}&operateType={operateType}&frx={frx}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchBusinessReportOfPdfById?reaReportClass={reaReportClass}&breportType={breportType}&id={id}&operateType={operateType}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream RS_UDTO_SearchBusinessReportOfPdfById(string reaReportClass, string breportType, long id, long operateType, string frx);

        [ServiceContractDescription(Name = "获取试剂耗材月订货PDF清单", Desc = "试剂耗材月订货PDF清单", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsCenOrderDocListReportOfPdf?where={where}&operateType={operateType}&frx={frx}", Get = "where={where}&operateType={operateType}&frx={frx}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsCenOrderDocListReportOfPdf?where={where}&operateType={operateType}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream RS_UDTO_SearchReaBmsCenOrderDocListReportOfPdf(string where, long operateType, string frx);
        #endregion

        #region Excel导出

        [ServiceContractDescription(Name = "将选择的多个订单按供货商+货品编码+包装单位合并后,生成Excel报表文件", Desc = "将选择的多个订单按供货商+货品编码+包装单位合并后,生成Excel报表文件", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsCenOrderDocOfExcelByIdStr?reaReportClass={reaReportClass}&operateType={operateType}&breportType={breportType}&idStr={idStr}&frx={frx}", Get = "reaReportClass={reaReportClass}&operateType={operateType}&breportType={breportType}&idStr={idStr}&frx={frx}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsCenOrderDocOfExcelByIdStr?reaReportClass={reaReportClass}&operateType={operateType}&breportType={breportType}&idStr={idStr}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream RS_UDTO_SearchReaBmsCenOrderDocOfExcelByIdStr(string reaReportClass, long operateType, string breportType, string idStr, string frx);

        [ServiceContractDescription(Name = "获取各业务报表(如采购申请,订货清单等)Excel导出文件", Desc = "获取各业务报表(如采购申请,订货清单等)Excel导出文件", Url = "ReaManageService.svc/RS_UDTO_SearchBusinessReportOfExcelById?operateType={operateType}&breportType={breportType}&id={id}&frx={frx}", Get = "operateType={operateType}&breportType={breportType}&id={id}&frx={frx}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchBusinessReportOfExcelById?operateType={operateType}&breportType={breportType}&id={id}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream RS_UDTO_SearchBusinessReportOfExcelById(long operateType, string breportType, long id, string frx);

        [ServiceContractDescription(Name = "导出库存查询或库存效期清单信息", Desc = "导出库存查询或库存效期清单信息", Url = "ReaManageService.svc/RS_UDTO_DownLoadGetExportExcelReaBmsQtyDtlByGroupType?qtyType={qtyType}&operateType={operateType}&groupType={groupType}&where={where}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&page={page}&limit={limit}", Get = "qtyType={qtyType}&operateType={operateType}&groupType={groupType}&where={where}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&page={page}&limit={limit}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_DownLoadGetExportExcelReaBmsQtyDtlByGroupType?qtyType={qtyType}&operateType={operateType}&groupType={groupType}&where={where}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream RS_UDTO_DownLoadGetExportExcelReaBmsQtyDtlByGroupType(int qtyType, int groupType, long operateType, string where, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit);

        [ServiceContractDescription(Name = "导出库存预警，warningType：预警类型(1:低库存：2：高库存)", Desc = "导出库存预警，warningType：预警类型(1:低库存：2：高库存)", Url = "ReaManageService.svc/RS_UDTO_DownLoadGetExportExcelByStockWarning?operateType={operateType}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&warningType={warningType}&groupType={groupType}&storePercent={storePercent}&comparisonType={comparisonType}&monthValue={monthValue}", Get = "operateType={operateType}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&warningType={warningType}&groupType={groupType}&storePercent={storePercent}&comparisonType={comparisonType}&monthValue={monthValue}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_DownLoadGetExportExcelByStockWarning?operateType={operateType}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&warningType={warningType}&groupType={groupType}&storePercent={storePercent}&comparisonType={comparisonType}&monthValue={monthValue}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream RS_UDTO_DownLoadGetExportExcelByStockWarning(long operateType, int warningType, int groupType, float storePercent, string comparisonType, int monthValue, string where, string reaGoodsHql, string sort);

        [ServiceContractDescription(Name = "获取库存货品信息,导出Excel文件", Desc = "获取库存货品信息,导出Excel文件", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlOfExcelByQtyHql?operateType={operateType}&breportType={breportType}&groupType={groupType}&qtyHql={qtyHql}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&frx={frx}&isEmpPermission={isEmpPermission}", Get = "operateType={operateType}&breportType={breportType}&groupType={groupType}&qtyHql={qtyHql}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&frx={frx}&isEmpPermission={isEmpPermission}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsQtyDtlOfExcelByQtyHql?operateType={operateType}&breportType={breportType}&groupType={groupType}&qtyHql={qtyHql}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&frx={frx}&isEmpPermission={isEmpPermission}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream RS_UDTO_SearchReaBmsQtyDtlOfExcelByQtyHql(long operateType, string breportType, int groupType, string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, string frx, bool isEmpPermission);

        [ServiceContractDescription(Name = "获取货品批号性能验证信息,导出Excel文件", Desc = "获取货品批号性能验证信息,导出Excel文件", Url = "ReaManageService.svc/RS_UDTO_SearchReaGoodsLotOfExcelByHql?operateType={operateType}&breportType={breportType}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&frx={frx}", Get = "operateType={operateType}&breportType={breportType}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&frx={frx}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaGoodsLotOfExcelByHql?operateType={operateType}&breportType={breportType}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&frx={frx}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream RS_UDTO_SearchReaGoodsLotOfExcelByHql(long operateType, string breportType, string where, string reaGoodsHql, string sort, string frx);

        #endregion

        #region ReaMonthUsageStatisticsDoc
        [ServiceContractDescription(Name = "新增出库使用量统计报表", Desc = "新增出库使用量统计报表", Url = "ReaManageService.svc/RS_UDTO_AddReaMonthUsageStatisticsDoc", Get = "", Post = "ReaMonthUsageStatisticsDoc", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddReaMonthUsageStatisticsDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddReaMonthUsageStatisticsDoc(ReaMonthUsageStatisticsDoc entity);

        [ServiceContractDescription(Name = "依主单ID,删除使用出库使用量统计主单及明细信息", Desc = "依主单ID,删除使用出库使用量统计主单及明细信息", Url = "ReaManageService.svc/RS_UDTO_DelReaMonthUsageStatisticsDocAndDtlByDocId?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RS_UDTO_DelReaMonthUsageStatisticsDocAndDtlByDocId?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_DelReaMonthUsageStatisticsDocAndDtlByDocId(long id);
        #endregion

        #region 定制查询

        [ServiceContractDescription(Name = "查询库房(货架)的试剂关系信息(HQL)", Desc = "查询库房(货架)的试剂关系信息(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaStorageGoodsLinkEntityListByAllJoinHQL?page={page}&limit={limit}&fields={fields}&where={where}&storageHql={storageHql}&placeHql={placeHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&storageHql={storageHql}&placeHql={placeHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaStorageGoodsLink>", ReturnType = "ListReaStorageGoodsLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaStorageGoodsLinkEntityListByAllJoinHQL?page={page}&limit={limit}&fields={fields}&where={where}&storageHql={storageHql}&placeHql={placeHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaStorageGoodsLinkEntityListByAllJoinHQL(int page, int limit, string fields, string where, string storageHql, string placeHql, string reaGoodsHql, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询仪器试剂关系信息(HQL)", Desc = "查询仪器试剂关系信息(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaEquipReagentLinkNewEntityListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaEquipReagentLink>", ReturnType = "ListReaEquipReagentLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaEquipReagentLinkNewEntityListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaEquipReagentLinkNewEntityListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询仪器项目关系信息(HQL)", Desc = "查询仪器项目关系信息(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaTestEquipItemEntityListByJoinHql?where={where}&reatestitemHql={reatestitemHql}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Get = "where={where}&reatestitemHql={reatestitemHql}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaTestEquipItem>", ReturnType = "ListReaTestEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaTestEquipItemEntityListByJoinHql?where={where}&reatestitemHql={reatestitemHql}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaTestEquipItemEntityListByJoinHql(string where, string reatestitemHql, int page, int limit, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询仪器项目试剂关系信息(HQL)", Desc = "查询仪器项目试剂关系信息(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaEquipTestItemReaGoodLinkNewEntityListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaEquipTestItemReaGoodLink>", ReturnType = "ListReaEquipTestItemReaGoodLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaEquipTestItemReaGoodLinkNewEntityListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaEquipTestItemReaGoodLinkNewEntityListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);
        #endregion

        #region 试剂客户端同步LIS系统基础信息
        [ServiceContractDescription(Name = "试剂客户端同步LIS基础信息(检验仪器/检验项目/仪器项目信息)", Desc = "试剂客户端同步LIS基础信息(检验仪器/检验项目/仪器项目信息)", Url = "ReaManageService.svc/RS_UDTO_AddSyncLISBasicInfo", Get = "", Post = "string", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddSyncLISBasicInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_AddSyncLISBasicInfo(string syncType);

        [ServiceContractDescription(Name = "试剂客户端同步LIS的检验仪器信息", Desc = "试剂客户端同步LIS的检验仪器信息", Url = "ReaManageService.svc/RS_UDTO_EditSyncLisTestEquipLabInfo", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_EditSyncLisTestEquipLabInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_EditSyncLisTestEquipLabInfo();

        [ServiceContractDescription(Name = "试剂客户端同步LIS的检验项目信息", Desc = "试剂客户端同步LIS的检验项目信息", Url = "ReaManageService.svc/RS_UDTO_EditSyncReaTestItemInfo", Get = "", Post = "", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_EditSyncReaTestItemInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_EditSyncReaTestItemInfo();

        [ServiceContractDescription(Name = "客户端同步LIS的仪器项目关系信息", Desc = "客户端同步LIS的仪器项目关系信息", Url = "ReaManageService.svc/RS_UDTO_EditSyncLisReaTestEquipItemInfo?equipId={equipId}", Get = "?equipId={equipId}", Post = "", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_EditSyncLisReaTestEquipItemInfo?equipId={equipId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_EditSyncLisReaTestEquipItemInfo(string equipId);

        [ServiceContractDescription(Name = "试剂客户端,从LIS系统导入检测结果", Desc = "试剂客户端,从LIS系统导入检测结果", Url = "ReaManageService.svc/RS_UDTO_AddReaLisTestStatisticalResults", Get = "", Post = "string", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddReaLisTestStatisticalResults", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_AddReaLisTestStatisticalResults(string testType, string beginDate, string endDate, string equipIDStr, string lisEquipCodeStr, string where, string order, bool isCover);

        #endregion

        #region CSUpdateToBS
        [ServiceContractDescription(Name = "CS试剂客户端升级到BS,清空BS库存信息", Desc = "CS试剂客户端升级到BS,清空BS库存信息", Url = "ReaManageService.svc/RS_UDTO_DeleteCSUpdateToBSQtyDtlInfo", Get = "", Post = "entity", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_DeleteCSUpdateToBSQtyDtlInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_DeleteCSUpdateToBSQtyDtlInfo(string entity);

        [ServiceContractDescription(Name = "CS试剂客户端升级到BS试剂客户端分步处理", Desc = "CS试剂客户端升级到BS试剂客户端分步处理", Url = "ReaManageService.svc/RS_UDTO_AddCSUpdateToBSByStep", Get = "", Post = "entity", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddCSUpdateToBSByStep", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddCSUpdateToBSByStep(string entity);

        [ServiceContractDescription(Name = "获取从CS试剂客户端升级到BS试剂客户端的当前进度信息", Desc = "获取从CS试剂客户端升级到BS试剂客户端的当前进度信息", Url = "ReaManageService.svc/RS_UDTO_GetCSUpdateToBSInfo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_GetCSUpdateToBSInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_GetCSUpdateToBSInfo();
        #endregion

        #region 移库管理
        [ServiceContractDescription(Name = "(依分配给员工的库房权限获取移库信息),查询Rea_BmsTransferDoc(HQL)", Desc = "(依分配给员工的库房权限获取移库信息),查询Rea_BmsTransferDoc(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsTransferDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&empId={empId}&type={type}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&empId={empId}&type={type}&isUseEmpOut={isUseEmpOut}", Post = "", Return = "BaseResultList<ReaBmsTransferDoc>", ReturnType = "ListReaBmsTransferDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsTransferDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&empId={empId}&type={type}&isUseEmpOut={isUseEmpOut}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsTransferDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, string isUseEmpOut, string type, long empId);

        [ServiceContractDescription(Name = "移库申请/出库申请时,获取某一申请货品的已申请总数及当前库存总数", Desc = "移库申请/出库申请时,获取某一申请货品的已申请总数及当前库存总数", Url = "ReaManageService.svc/RS_UDTO_SearchSumReqGoodsQtyAndCurrentQtyByHQL?dtlType={dtlType}&qtyHql={qtyHql}&dtlHql={dtlHql}&goodsId={goodsId}", Get = "dtlType={dtlType}&qtyHql={qtyHql}&dtlHql={dtlHql}&goodsId={goodsId}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSumReqGoodsQtyAndCurrentQtyByHQL?dtlType={dtlType}&qtyHql={qtyHql}&dtlHql={dtlHql}&goodsId={goodsId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSumReqGoodsQtyAndCurrentQtyByHQL(string dtlType, string qtyHql, string dtlHql, string goodsId);

        [ServiceContractDescription(Name = "移库申请单查询定制(数据范围限制申请人所属部门及子部门)(HQL)", Desc = "移库申请单查询定制(数据范围限制申请人所属部门及子部门)(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsTransferDocByReqDeptHQL?reqDeptId={reqDeptId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "reqDeptId={reqDeptId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsTransferDoc>", ReturnType = "ListReaBmsTransferDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsTransferDocByReqDeptHQL?reqDeptId={reqDeptId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsTransferDocByReqDeptHQL(string reqDeptId, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "客户端移库申请新增服务", Desc = "客户端移库申请新增服务", Url = "ReaManageService.svc/RS_UDTO_AddReaBmsTransferDocAndDtl", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddReaBmsTransferDocAndDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddReaBmsTransferDocAndDtl(ReaBmsTransferDoc entity, IList<ReaBmsTransferDtl> dtAddList, bool isEmpTransfer);

        [ServiceContractDescription(Name = "客户端移库申请更新服务", Desc = "客户端移库申请更新服务", Url = "ReaManageService.svc/RS_UDTO_UpdateReaBmsTransferDocAndDtl", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsTransferDocAndDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsTransferDocAndDtl(ReaBmsTransferDoc entity, IList<ReaBmsTransferDtl> dtAddList, IList<ReaBmsTransferDtl> dtEditList, bool isEmpTransfer);

        [ServiceContractDescription(Name = "客户端对移库申请单进行移库完成处理更新服务", Desc = "客户端对移库申请单进行移库完成处理更新服务", Url = "ReaManageService.svc/RS_UDTO_UpdateReaBmsTransferDocAndDtlOfComp", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsTransferDocAndDtlOfComp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsTransferDocAndDtlOfComp(ReaBmsTransferDoc entity, IList<ReaBmsTransferDtl> dtAddList, IList<ReaBmsTransferDtl> dtEditList, bool isEmpTransfer);

        [ServiceContractDescription(Name = "新增直接移库完成并更新库存", Desc = "新增直接移库完成并更新库存", Url = "ReaManageService.svc/RS_UDTO_AddGoodsReaBmsTransferDoc", Get = "", Post = "reaBmsTransferDoc,listReaBmsTransferDtl", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddGoodsReaBmsTransferDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddGoodsReaBmsTransferDoc(ReaBmsTransferDoc reaBmsTransferDoc, IList<ReaBmsTransferDtl> listReaBmsTransferDtl, bool isEmpTransfer);

        [ServiceContractDescription(Name = "入库移库,获取库存货品库存数大于0的入库主单信息(HQL)", Desc = "入库移库,获取库存货品库存数大于0的入库主单信息(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsInDocOfQtyGEZeroByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&dtlHql={dtlHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&dtlHql={dtlHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsInDoc>", ReturnType = "ListReaBmsInDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsInDocOfQtyGEZeroByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&dtlHql={dtlHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsInDocOfQtyGEZeroByJoinHql(int page, int limit, string fields, string where, string dtlHql, string reaGoodsHql, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "入库移库,获取库存货品库存数大于0的入库库存记录信息(HQL)", Desc = "入库移库,获取库存货品库存数大于0的入库主单信息(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlOfQtyGEZeroByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&inDtlHql={inDtlHql}&qtyHql={qtyHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&inDtlHql={inDtlHql}&qtyHql={qtyHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ListReaBmsQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsQtyDtlOfQtyGEZeroByJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&inDtlHql={inDtlHql}&qtyHql={qtyHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtlOfQtyGEZeroByJoinHql(int page, int limit, string fields, string where, string inDtlHql, string qtyHql, string reaGoodsHql, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "入库移库的确认移库新增服务", Desc = "入库移库的确认移库新增服务", Url = "ReaManageService.svc/RS_UDTO_AddTransferDocOfInDoc", Get = "", Post = "reaBmsTransferDoc,listReaBmsTransferDtl", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddTransferDocOfInDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddTransferDocOfInDoc(ReaBmsInDoc inDoc, ReaBmsTransferDoc transferDoc, IList<ReaBmsTransferDtl> transferDtlList, bool isEmpTransfer);

        #endregion

        #region 出库管理

        [ServiceContractDescription(Name = "新增直接出库完成并更新库存", Desc = "新增直接出库完成并更新库存", Url = "ReaManageService.svc/RS_UDTO_AddGoodsReaBmsOutDoc", Get = "", Post = "reaBmsOutDoc,listReaBmsOutDtl,isEmpOut", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddGoodsReaBmsOutDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddGoodsReaBmsOutDoc(ReaBmsOutDoc reaBmsOutDoc, IList<ReaBmsOutDtl> listReaBmsOutDtl, bool isEmpOut, bool isNeedPerformanceTest);

        [ServiceContractDescription(Name = "(依分配给员工的库房权限获取出库信息),查询ReaBmsOutDoc(HQL)", Desc = "(依分配给员工的库房权限获取出库信息),查询ReaBmsOutDoc(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsOutDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&empId={empId}&type={type}&isUseEmpOut={isUseEmpOut}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&empId={empId}&type={type}&isUseEmpOut={isUseEmpOut}", Post = "", Return = "BaseResultList<ReaBmsOutDoc>", ReturnType = "ListReaBmsOutDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsOutDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&empId={empId}&type={type}&isUseEmpOut={isUseEmpOut}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsOutDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, string isUseEmpOut, string type, long empId);

        [ServiceContractDescription(Name = "出库申请单查询定制(数据范围限制申请人所属部门及子部门)(HQL)", Desc = "出库申请单查询定制(数据范围限制申请人所属部门及子部门)(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsOutDocByReqDeptHQL?reqDeptId={reqDeptId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "reqDeptId={reqDeptId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsTransferDoc>", ReturnType = "ListReaBmsTransferDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsOutDocByReqDeptHQL?reqDeptId={reqDeptId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsOutDocByReqDeptHQL(string reqDeptId, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "客户端出库申请新增服务", Desc = "客户端出库申请新增服务", Url = "ReaManageService.svc/RS_UDTO_AddReaBmsOutDocAndDtl", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddReaBmsOutDocAndDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddReaBmsOutDocAndDtl(ReaBmsOutDoc entity, IList<ReaBmsOutDtl> dtAddList, bool isEmpOut, bool isNeedPerformanceTest);

        [ServiceContractDescription(Name = "客户端出库申请更新服务", Desc = "客户端出库申请更新服务", Url = "ReaManageService.svc/RS_UDTO_UpdateReaBmsOutDocAndDtl", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsOutDocAndDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsOutDocAndDtl(ReaBmsOutDoc entity, IList<ReaBmsOutDtl> dtAddList, IList<ReaBmsOutDtl> dtEditList, bool isEmpOut, bool isNeedPerformanceTest);

        [ServiceContractDescription(Name = "客户端对出库申请单进行出库完成处理更新服务", Desc = "客户端对出库申请单进行出库完成处理更新服务", Url = "ReaManageService.svc/RS_UDTO_UpdateReaBmsOutDocAndDtlOfComp", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsOutDocAndDtlOfComp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsOutDocAndDtlOfComp(ReaBmsOutDoc entity, IList<ReaBmsOutDtl> dtAddList, IList<ReaBmsOutDtl> dtEditList, bool isEmpOut, bool isNeedPerformanceTest);

        [ServiceContractDescription(Name = "出库申请单的审核/审批更新服务", Desc = "出库申请单的审核/审批更新服务", Url = "ReaManageService.svc/RS_UDTO_UpdateReaBmsOutDocByCheck", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsOutDocByCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsOutDocByCheck(ReaBmsOutDoc entity, string fields);

        [ServiceContractDescription(Name = "获取某一货品上一次出库的批号和货运单号", Desc = "获取某一货品上一次出库的批号和货运单号", Url = "ReaManageService.svc/RS_UDTO_GetLastLotNoAndTransportNo?goodsId={goodsId}", Get = "goodsId={goodsId}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_GetLastLotNoAndTransportNo?goodsId={goodsId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_GetLastLotNoAndTransportNo(long goodsId);

        [ServiceContractDescription(Name = "出库时批号性能验证，是否允许加入到待出库列表", Desc = "出库时批号性能验证，是否允许加入到待出库列表", Url = "ReaManageService.svc/RS_UDTO_LotNoPerformanceVerification?goodsId={goodsId}&goodsLotNo={goodsLotNo}", Get = "goodsId={goodsId}&goodsLotNo={goodsLotNo}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_LotNoPerformanceVerification?goodsId={goodsId}&goodsLotNo={goodsLotNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_LotNoPerformanceVerification(long goodsId, string goodsLotNo);

        [ServiceContractDescription(Name = "智方试剂平台，查询状态=[出库单上传平台]且订货方类型=[调拨]的出库单", Desc = "", Url = "ReaManageService.svc/GetPlatformOutDocListByDBClient?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsOutDoc>", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPlatformOutDocListByDBClient?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetPlatformOutDocListByDBClient(string where, string sort, int page, int limit, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "智方试剂平台，根据出库单查询其明细，不带LabID条件", Desc = "", Url = "ReaManageService.svc/GetPlatformOutDtlListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsOutDtl>", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPlatformOutDtlListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetPlatformOutDtlListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);
        #endregion

        #region 库房货架权限信息
        [ServiceContractDescription(Name = "获取库房货架权限的库房信息(HQL)", Desc = "获取库房货架权限的库房信息(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchListByStorageAndLinHQL?page={page}&limit={limit}&fields={fields}&storageHql={storageHql}&linkHql={linkHql}&operType={operType}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&storageHql={storageHql}&linkHql={linkHql}&operType={operType}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchListByStorageAndLinHQL?page={page}&limit={limit}&fields={fields}&storageHql={storageHql}&linkHql={linkHql}&operType={operType}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchListByStorageAndLinHQL(int page, int limit, string fields, string storageHql, string linkHql, string operType, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取库房货架权限的货架信息(HQL)", Desc = "获取库房货架权限的货架信息(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaPlaceByPlaceAndLinHQL?page={page}&limit={limit}&fields={fields}&placeHql={placeHql}&linkHql={linkHql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&placeHql={placeHql}&linkHql={linkHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaPlaceByPlaceAndLinHQL?page={page}&limit={limit}&fields={fields}&placeHql={placeHql}&linkHql={linkHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaPlaceByPlaceAndLinHQL(int page, int limit, string fields, string placeHql, string linkHql, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询库房货架树(HQL)", Desc = "查询库房货架树(HQL)", Url = "ReaSManageService.svc/RS_UDTO_GetStoragePlaceListTree?isEmpPermission={isEmpPermission}&operType={operType}", Get = "isEmpPermission={isEmpPermission}&operType={operType}", Post = "", Return = "BaseResultTree<BaseResultTree>", ReturnType = "ListBaseResultTree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_GetStoragePlaceListTree?isEmpPermission={isEmpPermission}&operType={operType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_GetStoragePlaceListTree(bool isEmpPermission, string operType);

        #endregion

        #region 联合查询

        [ServiceContractDescription(Name = "查询货品批号性能验证(HQL)", Desc = "查询货品批号性能验证(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaGoodsLotByAllJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsLot>", ReturnType = "ListReaGoodsLot")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaGoodsLotByAllJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaGoodsLotByAllJoinHql(int page, int limit, string fields, string where, string reaGoodsHql, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询库存货品操作记录(HQL)", Desc = "查询库存货品操作记录(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlOperationByAllJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyDtlOperation>", ReturnType = "ListReaBmsQtyDtlOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsQtyDtlOperationByAllJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtlOperationByAllJoinHql(int page, int limit, string fields, string where, string reaGoodsHql, string sort, bool isPlanish);
        #endregion

        #region 接口服务
        [ServiceContractDescription(Name = "供货单接口服务", Desc = "供货单接口服务", Url = "ReaManageService.svc/RS_UDTO_InputSaleDocInterface?saleDocNo={saleDocNo}&compOrgId={compOrgId}&labOrgId={labOrgId}&mainFields={mainFields}&childFields={childFields}&entityType={entityType}", Get = "saleDocNo={saleDocNo}&compOrgId={compOrgId}&labOrgId={labOrgId}&mainFields={mainFields}&childFields={childFields}&entityType={entityType}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_InputSaleDocInterface?saleDocNo={saleDocNo}&compOrgId={compOrgId}&labOrgId={labOrgId}&mainFields={mainFields}&childFields={childFields}&entityType={entityType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_UDTO_InputSaleDocInterface(string saleDocNo, long compOrgId, long labOrgId, string mainFields, string childFields, string entityType);

        [ServiceContractDescription(Name = "退库接口服务", Desc = "入库单退库接口服务", Url = "ReaManageService.svc/RS_UDTO_ReaGoodsBackStorageByInDocInterface", Get = "", Post = "inDoc,inDtlList", Return = "BaseResultData", ReturnType = "")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_ReaGoodsBackStorageByInDocInterface", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_UDTO_ReaGoodsBackStorageByInDocInterface(ReaBmsInDoc inDoc, IList<ReaBmsInDtl> inDtlList);

        [ServiceContractDescription(Name = "退库接口服务", Desc = "出库单退库接口服务", Url = "ReaManageService.svc/RS_UDTO_ReaGoodsBackStorageByOutDocInterface", Get = "", Post = "outDoc,outDtlList", Return = "BaseResultData", ReturnType = "")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_ReaGoodsBackStorageByOutDocInterface", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_UDTO_ReaGoodsBackStorageByOutDocInterface(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> outDtlList);

        [ServiceContractDescription(Name = "出库接口服务", Desc = "出库接口服务", Url = "ReaManageService.svc/RS_UDTO_ReaGoodsStorageSyncInterface", Get = "", Post = "outDoc,outDtlList", Return = "BaseResultData", ReturnType = "")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_ReaGoodsStorageSyncInterface", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_UDTO_ReaGoodsStorageSyncInterface(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> outDtlList);

        [ServiceContractDescription(Name = "出库接口服务", Desc = "出库接口服务", Url = "ReaManageService.svc/RS_UDTO_ReaGoodsStorageSyncInterfaceByID?outDocID={outDocID}&outDocNo={outDocNo}", Get = "outDocID={outDocID}&outDocNo={outDocNo}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_ReaGoodsStorageSyncInterfaceByID?outDocID={outDocID}&outDocNo={outDocNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_UDTO_ReaGoodsStorageSyncInterfaceByID(long outDocID, string outDocNo);

        [ServiceContractDescription(Name = "退供应商接口服务", Desc = "退供应商接口服务", Url = "ReaManageService.svc/RS_UDTO_ReaGoodsBackStorageByOutDocInterfaceByID?outDocID={outDocID}&outDocNo={outDocNo}", Get = "outDocID={outDocID}&outDocNo={outDocNo}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_ReaGoodsBackStorageByOutDocInterfaceByID?outDocID={outDocID}&outDocNo={outDocNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultData RS_UDTO_ReaGoodsBackStorageByOutDocInterfaceByID(long outDocID, string outDocNo);

        [ServiceContractDescription(Name = "订单审核通过后,订单同步到其他系统平台", Desc = "订单审核通过后,订单同步到其他系统平台", Url = "ReaManageService.svc/RS_UDTO_OrderDocSaveToOtherSystem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_OrderDocSaveToOtherSystem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_OrderDocSaveToOtherSystem(long id);

        [ServiceContractDescription(Name = "", Desc = "", Url = "ReaManageService.svc/Test?entityType={entityType}", Get = "entityType={entityType}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Test?entityType={entityType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Test(string entityType);

        #endregion

        #region ReaMonthUsageStatisticsDtl
        [ServiceContractDescription(Name = "按联合条件查询Rea_MonthUsageStatisticsDtl(HQL)", Desc = "按联合条件查询Rea_MonthUsageStatisticsDtl(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaMonthUsageStatisticsDtlByAllJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaMonthUsageStatisticsDtl>", ReturnType = "ListReaMonthUsageStatisticsDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaMonthUsageStatisticsDtlByAllJoinHql?page={page}&limit={limit}&fields={fields}&where={where}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaMonthUsageStatisticsDtlByAllJoinHql(int page, int limit, string fields, string where, string reaGoodsHql, string sort, bool isPlanish);
        #endregion

        #region ReaGoodsBarcodeOperation
        [ServiceContractDescription(Name = "查询某一库存货品的剩余货品条码操作记录信息(HQL)", Desc = "查询某一库存货品的剩余货品条码操作记录信息(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchOverReaGoodsBarcodeOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsBarcodeOperation>", ReturnType = "ListReaGoodsBarcodeOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchOverReaGoodsBarcodeOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchOverReaGoodsBarcodeOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        #endregion

        #region 库存标志处理

        [ServiceContractDescription(Name = "库存标志维护获取库存查询结果,返回按合并项合并后EntityList", Desc = "库存标志维护获取库存查询结果,返回按合并项合并后EntityList", Url = "ReaManageService.svc/RS_UDTO_SearchReaBmsQtyDtlEntityListOfQtyMarkByGroupType?groupType={groupType}&storageId={storageId}&page={page}&limit={limit}&fields={fields}&where={where}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}&isEmpPermission={isEmpPermission}", Get = "groupType={groupType}&storageId={storageId}&page={page}&limit={limit}&fields={fields}&where={where}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}&isEmpPermission={isEmpPermission}", Post = "", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ListReaBmsQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaBmsQtyDtlEntityListOfQtyMarkByGroupType?groupType={groupType}&storageId={storageId}&page={page}&limit={limit}&fields={fields}&where={where}&deptGoodsHql={deptGoodsHql}&reaGoodsHql={reaGoodsHql}&sort={sort}&isPlanish={isPlanish}&isEmpPermission={isEmpPermission}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtlEntityListOfQtyMarkByGroupType(string storageId, int groupType, int page, int limit, string fields, string where, string deptGoodsHql, string reaGoodsHql, string sort, bool isPlanish, bool isEmpPermission);

        [ServiceContractDescription(Name = "修改指定库房的库存货品的库存标志值", Desc = "修改指定库房的库存货品的库存标志值", Url = "ReaManageService.svc/RS_UDTO_UpdateReaBmsQtyDtlByQtyDtlMark", Get = "", Post = "QtyMarkVO", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateReaBmsQtyDtlByQtyDtlMark", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateReaBmsQtyDtlByQtyDtlMark(QtyMarkVO entity);

        #endregion

        #region 双表查询定制服务

        [ServiceContractDescription(Name = "部门货品选择机构货品时,获取待选择的机构货品信息(HQL)", Desc = "部门货品选择机构货品时,获取待选择的机构货品信息(HQL)", Url = "ReaManageService.svc/RS_UDTO_SearchReaGoodsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoods>", ReturnType = "ListReaGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaGoodsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&linkWhere={linkWhere}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaGoodsByHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish);

        #endregion

<<<<<<< .mine
        #region ReaOpenBottleOperDoc

        [ServiceContractDescription(Name = "新增开瓶管理记录主单", Desc = "新增开瓶管理记录主单", Url = "ReaManageService.svc/ST_UDTO_AddReaOpenBottleOperDoc", Get = "", Post = "ReaOpenBottleOperDoc", Return = "BaseResultDataValue", ReturnType = "ReaOpenBottleOperDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaOpenBottleOperDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaOpenBottleOperDoc(ReaOpenBottleOperDoc entity);

        [ServiceContractDescription(Name = "修改开瓶管理记录主单", Desc = "修改开瓶管理记录主单", Url = "ReaManageService.svc/ST_UDTO_UpdateReaOpenBottleOperDoc", Get = "", Post = "ReaOpenBottleOperDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaOpenBottleOperDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaOpenBottleOperDoc(ReaOpenBottleOperDoc entity);

        [ServiceContractDescription(Name = "修改开瓶管理记录主单指定的属性", Desc = "修改开瓶管理记录主单指定的属性", Url = "ReaManageService.svc/ST_UDTO_UpdateReaOpenBottleOperDocByField", Get = "", Post = "ReaOpenBottleOperDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaOpenBottleOperDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaOpenBottleOperDocByField(ReaOpenBottleOperDoc entity, string fields);

        [ServiceContractDescription(Name = "删除开瓶管理记录主单", Desc = "删除开瓶管理记录主单", Url = "ReaManageService.svc/ST_UDTO_DelReaOpenBottleOperDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaOpenBottleOperDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaOpenBottleOperDoc(long id);

        [ServiceContractDescription(Name = "查询开瓶管理记录主单", Desc = "查询开瓶管理记录主单", Url = "ReaManageService.svc/ST_UDTO_SearchReaOpenBottleOperDoc", Get = "", Post = "ReaOpenBottleOperDoc", Return = "BaseResultList<ReaOpenBottleOperDoc>", ReturnType = "ListReaOpenBottleOperDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaOpenBottleOperDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaOpenBottleOperDoc(ReaOpenBottleOperDoc entity);

        [ServiceContractDescription(Name = "查询开瓶管理记录主单(HQL)", Desc = "查询开瓶管理记录主单(HQL)", Url = "ReaManageService.svc/ST_UDTO_SearchReaOpenBottleOperDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaOpenBottleOperDoc>", ReturnType = "ListReaOpenBottleOperDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaOpenBottleOperDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaOpenBottleOperDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询开瓶管理记录主单", Desc = "通过主键ID查询开瓶管理记录主单", Url = "ReaManageService.svc/ST_UDTO_SearchReaOpenBottleOperDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaOpenBottleOperDoc>", ReturnType = "ReaOpenBottleOperDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaOpenBottleOperDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaOpenBottleOperDocById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "通过出库明细主键ID查询开瓶管理记录主单", Desc = "通过出库明细主键ID查询开瓶管理记录主单", Url = "ReaManageService.svc/ST_UDTO_GetOBottleOperDocByOutDtlId?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaOpenBottleOperDoc>", ReturnType = "ReaOpenBottleOperDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GetOBottleOperDocByOutDtlId?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_GetOBottleOperDocByOutDtlId(long id, string fields, bool isPlanish);
        #endregion

||||||| .r2673
=======
        #region 四川大家试剂投屏需求

        /// <summary>
        /// 四川大家投屏查询服务
        /// </summary>
        /// <returns></returns>
        [ServiceContractDescription(Name = "四川大家投屏查询数据服务", Desc = "", Url = "ReaManageService.svc/RS_UDTO_SearchReaGoodsQtyWarningInfo?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoods>", ReturnType = "ListReaGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchReaGoodsQtyWarningInfo?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchReaGoodsQtyWarningInfo(int page, int limit, string fields, string where, string sort, bool isPlanish);

        /// <summary>
        /// 货品表结合库存表，查询二级分类并返回
        /// </summary>
        /// <returns></returns>
        [ServiceContractDescription(Name = "货品表结合库存表，查询二级分类并返回", Desc = "", Url = "ReaManageService.svc/RS_UDTO_SearchGoodsClassTypeJoinQtyDtl?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsClassVO>", ReturnType = "ListReaGoodsClassVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchGoodsClassTypeJoinQtyDtl?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchGoodsClassTypeJoinQtyDtl(bool isPlanish, string fields, string where, string sort, int page, int limit);

        #endregion
>>>>>>> .r2783
    }
}
