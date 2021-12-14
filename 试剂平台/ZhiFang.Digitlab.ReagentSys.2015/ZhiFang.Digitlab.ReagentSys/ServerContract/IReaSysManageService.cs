using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Digitlab.ServiceCommon;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using System.IO;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaConfirm;
using ZhiFang.Digitlab.Entity.ReagentSys.ViewObject.ReaStoreIn;

namespace ZhiFang.Digitlab.ReagentSys.ServerContract
{
    [ServiceContract]
    public interface IReaSysManageService
    {

        #region ReaCenOrg

        [ServiceContractDescription(Name = "新增机构表", Desc = "新增机构表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaCenOrg", Get = "", Post = "ReaCenOrg", Return = "BaseResultDataValue", ReturnType = "ReaCenOrg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaCenOrg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaCenOrg(ReaCenOrg entity);

        [ServiceContractDescription(Name = "修改机构表", Desc = "修改机构表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaCenOrg", Get = "", Post = "ReaCenOrg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaCenOrg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaCenOrg(ReaCenOrg entity);

        [ServiceContractDescription(Name = "修改机构表指定的属性", Desc = "修改机构表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaCenOrgByField", Get = "", Post = "ReaCenOrg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaCenOrgByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaCenOrgByField(ReaCenOrg entity, string fields);

        [ServiceContractDescription(Name = "删除机构表", Desc = "删除机构表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaCenOrg?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaCenOrg?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaCenOrg(long id);

        [ServiceContractDescription(Name = "查询机构表", Desc = "查询机构表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenOrg", Get = "", Post = "ReaCenOrg", Return = "BaseResultList<ReaCenOrg>", ReturnType = "ListReaCenOrg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCenOrg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenOrg(ReaCenOrg entity);

        [ServiceContractDescription(Name = "查询机构表(HQL)", Desc = "查询机构表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaCenOrg>", ReturnType = "ListReaCenOrg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCenOrgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenOrgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询机构表", Desc = "通过主键ID查询机构表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenOrgById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaCenOrg>", ReturnType = "ReaCenOrg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCenOrgById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenOrgById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaGoods

        [ServiceContractDescription(Name = "新增平台货品表", Desc = "新增平台货品表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaGoods", Get = "", Post = "ReaGoods", Return = "BaseResultDataValue", ReturnType = "ReaGoods")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaGoods(ReaGoods entity);

        [ServiceContractDescription(Name = "修改平台货品表", Desc = "修改平台货品表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoods", Get = "", Post = "ReaGoods", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoods(ReaGoods entity);

        [ServiceContractDescription(Name = "修改平台货品表指定的属性", Desc = "修改平台货品表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsByField", Get = "", Post = "ReaGoods", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsByField(ReaGoods entity, string fields);

        [ServiceContractDescription(Name = "删除平台货品表", Desc = "删除平台货品表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaGoods?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaGoods?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaGoods(long id);

        [ServiceContractDescription(Name = "查询平台货品表", Desc = "查询平台货品表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoods", Get = "", Post = "ReaGoods", Return = "BaseResultList<ReaGoods>", ReturnType = "ListReaGoods")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoods(ReaGoods entity);

        [ServiceContractDescription(Name = "查询平台货品表(HQL)", Desc = "查询平台货品表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoods>", ReturnType = "ListReaGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台货品表", Desc = "通过主键ID查询平台货品表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoods>", ReturnType = "ReaGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "根据选中行货品id修改平台货品表的相同码", Desc = "根据选中行货品id修改平台货品表的相同码", Url = "ReaSysManageService.svc/ST_UDTO_UpdateGonvertGroupCode?idList={idList}&Code={Code}", Get = "idList={idList}&Code={Code}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_UpdateGonvertGroupCode?idList={idList}&Code={Code}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGonvertGroupCode(string idList, string Code);

        #endregion

        #region ReaGoodsOrgLink

        [ServiceContractDescription(Name = "新增订货信息表", Desc = "新增订货信息表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaGoodsOrgLink", Get = "", Post = "ReaGoodsOrgLink", Return = "BaseResultDataValue", ReturnType = "ReaGoodsOrgLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaGoodsOrgLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaGoodsOrgLink(ReaGoodsOrgLink entity);

        [ServiceContractDescription(Name = "修改订货信息表", Desc = "修改订货信息表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsOrgLink", Get = "", Post = "ReaGoodsOrgLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsOrgLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsOrgLink(ReaGoodsOrgLink entity);

        [ServiceContractDescription(Name = "修改订货信息表指定的属性", Desc = "修改订货信息表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsOrgLinkByField", Get = "", Post = "ReaGoodsOrgLink", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsOrgLinkByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsOrgLinkByField(ReaGoodsOrgLink entity, string fields);

        [ServiceContractDescription(Name = "删除订货信息表", Desc = "删除订货信息表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaGoodsOrgLink?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaGoodsOrgLink?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaGoodsOrgLink(long id);

        [ServiceContractDescription(Name = "查询订货信息表", Desc = "查询订货信息表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLink", Get = "", Post = "ReaGoodsOrgLink", Return = "BaseResultList<ReaGoodsOrgLink>", ReturnType = "ListReaGoodsOrgLink")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsOrgLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLink(ReaGoodsOrgLink entity);

        [ServiceContractDescription(Name = "查询订货信息表(HQL)", Desc = "查询订货信息表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsOrgLink>", ReturnType = "ListReaGoodsOrgLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsOrgLinkByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询订货信息表", Desc = "通过主键ID查询订货信息表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsOrgLink>", ReturnType = "ReaGoodsOrgLink")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsOrgLinkById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsInDoc

        [ServiceContractDescription(Name = "新增入库总单表", Desc = "新增入库总单表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsInDoc", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsInDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsInDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsInDoc(ReaBmsInDoc entity);

        [ServiceContractDescription(Name = "修改入库总单表", Desc = "修改入库总单表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDoc", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsInDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsInDoc(ReaBmsInDoc entity);

        [ServiceContractDescription(Name = "修改入库总单表指定的属性", Desc = "修改入库总单表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDocByField", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsInDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsInDocByField(ReaBmsInDoc entity, string fields);

        [ServiceContractDescription(Name = "删除入库总单表", Desc = "删除入库总单表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsInDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsInDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsInDoc(long id);

        [ServiceContractDescription(Name = "查询入库总单表", Desc = "查询入库总单表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDoc", Get = "", Post = "ReaBmsInDoc", Return = "BaseResultList<ReaBmsInDoc>", ReturnType = "ListReaBmsInDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDoc(ReaBmsInDoc entity);

        [ServiceContractDescription(Name = "查询入库总单表(HQL)", Desc = "查询入库总单表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsInDoc>", ReturnType = "ListReaBmsInDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询入库总单表", Desc = "通过主键ID查询入库总单表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsInDoc>", ReturnType = "ReaBmsInDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDocById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsInDtl

        [ServiceContractDescription(Name = "新增入库明细表", Desc = "新增入库明细表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsInDtl", Get = "", Post = "ReaBmsInDtl", Return = "BaseResultDataValue", ReturnType = "ReaBmsInDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsInDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsInDtl(ReaBmsInDtl entity);

        [ServiceContractDescription(Name = "修改入库明细表", Desc = "修改入库明细表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDtl", Get = "", Post = "ReaBmsInDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsInDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsInDtl(ReaBmsInDtl entity);

        [ServiceContractDescription(Name = "修改入库明细表指定的属性", Desc = "修改入库明细表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsInDtlByField", Get = "", Post = "ReaBmsInDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsInDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsInDtlByField(ReaBmsInDtl entity, string fields);

        [ServiceContractDescription(Name = "删除入库明细表", Desc = "删除入库明细表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsInDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsInDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsInDtl(long id);

        [ServiceContractDescription(Name = "查询入库明细表", Desc = "查询入库明细表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDtl", Get = "", Post = "ReaBmsInDtl", Return = "BaseResultList<ReaBmsInDtl>", ReturnType = "ListReaBmsInDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDtl(ReaBmsInDtl entity);

        [ServiceContractDescription(Name = "查询入库明细表(HQL)", Desc = "查询入库明细表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsInDtl>", ReturnType = "ListReaBmsInDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询入库明细表", Desc = "通过主键ID查询入库明细表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsInDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsInDtl>", ReturnType = "ReaBmsInDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsInDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsInDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsReqDoc

        [ServiceContractDescription(Name = "新增申请总单表", Desc = "新增申请总单表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsReqDoc", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsReqDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsReqDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsReqDoc(ReaBmsReqDoc entity);

        [ServiceContractDescription(Name = "修改申请总单表", Desc = "修改申请总单表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDoc", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDoc(ReaBmsReqDoc entity);

        [ServiceContractDescription(Name = "修改申请总单表指定的属性", Desc = "修改申请总单表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDocByField", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDocByField(ReaBmsReqDoc entity, string fields);

        [ServiceContractDescription(Name = "删除申请总单表", Desc = "删除申请总单表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsReqDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsReqDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsReqDoc(long id);

        [ServiceContractDescription(Name = "查询申请总单表", Desc = "查询申请总单表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDoc", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultList<ReaBmsReqDoc>", ReturnType = "ListReaBmsReqDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDoc(ReaBmsReqDoc entity);

        [ServiceContractDescription(Name = "查询申请总单表(HQL)", Desc = "查询申请总单表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsReqDoc>", ReturnType = "ListReaBmsReqDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询申请总单表", Desc = "通过主键ID查询申请总单表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsReqDoc>", ReturnType = "ReaBmsReqDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDocById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsReqDtl

        [ServiceContractDescription(Name = "新增申请明细表", Desc = "新增申请明细表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsReqDtl", Get = "", Post = "ReaBmsReqDtl", Return = "BaseResultDataValue", ReturnType = "ReaBmsReqDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsReqDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsReqDtl(ReaBmsReqDtl entity);

        [ServiceContractDescription(Name = "修改申请明细表", Desc = "修改申请明细表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDtl", Get = "", Post = "ReaBmsReqDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDtl(ReaBmsReqDtl entity);

        [ServiceContractDescription(Name = "修改申请明细表指定的属性", Desc = "修改申请明细表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDtlByField", Get = "", Post = "ReaBmsReqDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDtlByField(ReaBmsReqDtl entity, string fields);

        [ServiceContractDescription(Name = "删除申请明细表", Desc = "删除申请明细表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsReqDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsReqDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsReqDtl(long id);

        [ServiceContractDescription(Name = "查询申请明细表", Desc = "查询申请明细表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDtl", Get = "", Post = "ReaBmsReqDtl", Return = "BaseResultList<ReaBmsReqDtl>", ReturnType = "ListReaBmsReqDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDtl(ReaBmsReqDtl entity);

        [ServiceContractDescription(Name = "查询申请明细表(HQL)", Desc = "查询申请明细表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsReqDtl>", ReturnType = "ListReaBmsReqDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询申请明细表", Desc = "通过主键ID查询申请明细表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsReqDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsReqDtl>", ReturnType = "ReaBmsReqDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsReqDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsReqDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsTransferDoc

        [ServiceContractDescription(Name = "新增Rea_BmsTransferDoc", Desc = "新增Rea_BmsTransferDoc", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsTransferDoc", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsTransferDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsTransferDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsTransferDoc(ReaBmsTransferDoc entity);

        [ServiceContractDescription(Name = "修改Rea_BmsTransferDoc", Desc = "修改Rea_BmsTransferDoc", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDoc", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsTransferDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsTransferDoc(ReaBmsTransferDoc entity);

        [ServiceContractDescription(Name = "修改Rea_BmsTransferDoc指定的属性", Desc = "修改Rea_BmsTransferDoc指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDocByField", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsTransferDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsTransferDocByField(ReaBmsTransferDoc entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_BmsTransferDoc", Desc = "删除Rea_BmsTransferDoc", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsTransferDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsTransferDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsTransferDoc(long id);

        [ServiceContractDescription(Name = "查询Rea_BmsTransferDoc", Desc = "查询Rea_BmsTransferDoc", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDoc", Get = "", Post = "ReaBmsTransferDoc", Return = "BaseResultList<ReaBmsTransferDoc>", ReturnType = "ListReaBmsTransferDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsTransferDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsTransferDoc(ReaBmsTransferDoc entity);

        [ServiceContractDescription(Name = "查询Rea_BmsTransferDoc(HQL)", Desc = "查询Rea_BmsTransferDoc(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsTransferDoc>", ReturnType = "ListReaBmsTransferDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsTransferDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsTransferDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_BmsTransferDoc", Desc = "通过主键ID查询Rea_BmsTransferDoc", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsTransferDoc>", ReturnType = "ReaBmsTransferDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsTransferDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsTransferDocById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsTransferDtl

        [ServiceContractDescription(Name = "新增Rea_BmsTransferDtl", Desc = "新增Rea_BmsTransferDtl", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsTransferDtl", Get = "", Post = "ReaBmsTransferDtl", Return = "BaseResultDataValue", ReturnType = "ReaBmsTransferDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsTransferDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsTransferDtl(ReaBmsTransferDtl entity);

        [ServiceContractDescription(Name = "修改Rea_BmsTransferDtl", Desc = "修改Rea_BmsTransferDtl", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDtl", Get = "", Post = "ReaBmsTransferDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsTransferDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsTransferDtl(ReaBmsTransferDtl entity);

        [ServiceContractDescription(Name = "修改Rea_BmsTransferDtl指定的属性", Desc = "修改Rea_BmsTransferDtl指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsTransferDtlByField", Get = "", Post = "ReaBmsTransferDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsTransferDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsTransferDtlByField(ReaBmsTransferDtl entity, string fields);

        [ServiceContractDescription(Name = "删除Rea_BmsTransferDtl", Desc = "删除Rea_BmsTransferDtl", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsTransferDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsTransferDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsTransferDtl(long id);

        [ServiceContractDescription(Name = "查询Rea_BmsTransferDtl", Desc = "查询Rea_BmsTransferDtl", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDtl", Get = "", Post = "ReaBmsTransferDtl", Return = "BaseResultList<ReaBmsTransferDtl>", ReturnType = "ListReaBmsTransferDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsTransferDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsTransferDtl(ReaBmsTransferDtl entity);

        [ServiceContractDescription(Name = "查询Rea_BmsTransferDtl(HQL)", Desc = "查询Rea_BmsTransferDtl(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsTransferDtl>", ReturnType = "ListReaBmsTransferDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsTransferDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsTransferDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Rea_BmsTransferDtl", Desc = "通过主键ID查询Rea_BmsTransferDtl", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsTransferDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsTransferDtl>", ReturnType = "ReaBmsTransferDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsTransferDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsTransferDtlById(long id, string fields, bool isPlanish);
        #endregion        

        #region ReaDeptGoods

        [ServiceContractDescription(Name = "新增部门货品表", Desc = "新增部门货品表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaDeptGoods", Get = "", Post = "ReaDeptGoods", Return = "BaseResultDataValue", ReturnType = "ReaDeptGoods")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaDeptGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaDeptGoods(ReaDeptGoods entity);

        [ServiceContractDescription(Name = "修改部门货品表", Desc = "修改部门货品表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaDeptGoods", Get = "", Post = "ReaDeptGoods", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaDeptGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaDeptGoods(ReaDeptGoods entity);

        [ServiceContractDescription(Name = "修改部门货品表指定的属性", Desc = "修改部门货品表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaDeptGoodsByField", Get = "", Post = "ReaDeptGoods", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaDeptGoodsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaDeptGoodsByField(ReaDeptGoods entity, string fields);

        [ServiceContractDescription(Name = "删除部门货品表", Desc = "删除部门货品表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaDeptGoods?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaDeptGoods?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaDeptGoods(long id);

        [ServiceContractDescription(Name = "查询部门货品表", Desc = "查询部门货品表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaDeptGoods", Get = "", Post = "ReaDeptGoods", Return = "BaseResultList<ReaDeptGoods>", ReturnType = "ListReaDeptGoods")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaDeptGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaDeptGoods(ReaDeptGoods entity);

        [ServiceContractDescription(Name = "查询部门货品表(HQL)", Desc = "查询部门货品表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaDeptGoodsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaDeptGoods>", ReturnType = "ListReaDeptGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaDeptGoodsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaDeptGoodsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询部门货品表", Desc = "通过主键ID查询部门货品表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaDeptGoodsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaDeptGoods>", ReturnType = "ReaDeptGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaDeptGoodsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaDeptGoodsById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaGoodsLot

        [ServiceContractDescription(Name = "新增货品批号表", Desc = "新增货品批号表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaGoodsLot", Get = "", Post = "ReaGoodsLot", Return = "BaseResultDataValue", ReturnType = "ReaGoodsLot")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaGoodsLot", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaGoodsLot(ReaGoodsLot entity);

        [ServiceContractDescription(Name = "修改货品批号表", Desc = "修改货品批号表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsLot", Get = "", Post = "ReaGoodsLot", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsLot", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsLot(ReaGoodsLot entity);

        [ServiceContractDescription(Name = "修改货品批号表指定的属性", Desc = "修改货品批号表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsLotByField", Get = "", Post = "ReaGoodsLot", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsLotByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsLotByField(ReaGoodsLot entity, string fields);

        [ServiceContractDescription(Name = "删除货品批号表", Desc = "删除货品批号表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaGoodsLot?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaGoodsLot?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaGoodsLot(long id);

        [ServiceContractDescription(Name = "查询货品批号表", Desc = "查询货品批号表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsLot", Get = "", Post = "ReaGoodsLot", Return = "BaseResultList<ReaGoodsLot>", ReturnType = "ListReaGoodsLot")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsLot", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsLot(ReaGoodsLot entity);

        [ServiceContractDescription(Name = "查询货品批号表(HQL)", Desc = "查询货品批号表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsLotByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsLot>", ReturnType = "ListReaGoodsLot")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsLotByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsLotByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询货品批号表", Desc = "通过主键ID查询货品批号表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsLotById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsLot>", ReturnType = "ReaGoodsLot")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsLotById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsLotById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaGoodsRegister
        [ServiceContractDescription(Name = "通过FormData方式新增产品注册证信息", Desc = "通过FormData方式新增产品注册证信息", Url = "ReaSysManageService.svc/ST_UDTO_AddReaGoodsRegisterAndUploadRegisterFile", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaGoodsRegisterAndUploadRegisterFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_AddReaGoodsRegisterAndUploadRegisterFile();

        [ServiceContractDescription(Name = "通过FormData方式更新产品注册证信息", Desc = "通过FormData方式更新产品注册证信息", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsRegisterAndUploadRegisterFileByField", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsRegisterAndUploadRegisterFileByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_UpdateReaGoodsRegisterAndUploadRegisterFileByField();

        [ServiceContractDescription(Name = "预览产品注册证文件", Desc = "预览产品注册证文件", Url = "ReaSysManageService.svc/ST_UDTO_ReaGoodsRegisterPreviewPdf?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_ReaGoodsRegisterPreviewPdf?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_ReaGoodsRegisterPreviewPdf(long id, long operateType);

        [ServiceContractDescription(Name = "查询过滤掉重复的注册证编号的产品注册证表(HQL)", Desc = "查询过滤掉重复的注册证编号的产品注册证表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsRegister>", ReturnType = "ListReaGoodsRegister")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsRegisterOfFilterRepeatRegisterNoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);


        [ServiceContractDescription(Name = "新增货品注册证表", Desc = "新增货品注册证表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaGoodsRegister", Get = "", Post = "ReaGoodsRegister", Return = "BaseResultDataValue", ReturnType = "ReaGoodsRegister")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaGoodsRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaGoodsRegister(ReaGoodsRegister entity);

        [ServiceContractDescription(Name = "修改货品注册证表", Desc = "修改货品注册证表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsRegister", Get = "", Post = "ReaGoodsRegister", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsRegister(ReaGoodsRegister entity);

        [ServiceContractDescription(Name = "修改货品注册证表指定的属性", Desc = "修改货品注册证表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsRegisterByField", Get = "", Post = "ReaGoodsRegister", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsRegisterByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsRegisterByField(ReaGoodsRegister entity, string fields);

        [ServiceContractDescription(Name = "删除货品注册证表", Desc = "删除货品注册证表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaGoodsRegister?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaGoodsRegister?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaGoodsRegister(long id);

        [ServiceContractDescription(Name = "查询货品注册证表", Desc = "查询货品注册证表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsRegister", Get = "", Post = "ReaGoodsRegister", Return = "BaseResultList<ReaGoodsRegister>", ReturnType = "ListReaGoodsRegister")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsRegister(ReaGoodsRegister entity);

        [ServiceContractDescription(Name = "查询货品注册证表(HQL)", Desc = "查询货品注册证表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsRegisterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsRegister>", ReturnType = "ListReaGoodsRegister")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsRegisterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsRegisterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询货品注册证表", Desc = "通过主键ID查询货品注册证表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsRegisterById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsRegister>", ReturnType = "ReaGoodsRegister")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsRegisterById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsRegisterById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaGoodsUnit

        [ServiceContractDescription(Name = "新增货品包装单位表", Desc = "新增货品包装单位表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaGoodsUnit", Get = "", Post = "ReaGoodsUnit", Return = "BaseResultDataValue", ReturnType = "ReaGoodsUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaGoodsUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaGoodsUnit(ReaGoodsUnit entity);

        [ServiceContractDescription(Name = "修改货品包装单位表", Desc = "修改货品包装单位表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsUnit", Get = "", Post = "ReaGoodsUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsUnit(ReaGoodsUnit entity);

        [ServiceContractDescription(Name = "修改货品包装单位表指定的属性", Desc = "修改货品包装单位表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaGoodsUnitByField", Get = "", Post = "ReaGoodsUnit", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaGoodsUnitByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaGoodsUnitByField(ReaGoodsUnit entity, string fields);

        [ServiceContractDescription(Name = "删除货品包装单位表", Desc = "删除货品包装单位表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaGoodsUnit?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaGoodsUnit?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaGoodsUnit(long id);

        [ServiceContractDescription(Name = "查询货品包装单位表", Desc = "查询货品包装单位表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsUnit", Get = "", Post = "ReaGoodsUnit", Return = "BaseResultList<ReaGoodsUnit>", ReturnType = "ListReaGoodsUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsUnit(ReaGoodsUnit entity);

        [ServiceContractDescription(Name = "查询货品包装单位表(HQL)", Desc = "查询货品包装单位表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsUnit>", ReturnType = "ListReaGoodsUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsUnitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询货品包装单位表", Desc = "通过主键ID查询货品包装单位表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaGoodsUnit>", ReturnType = "ReaGoodsUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsUnitById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaPlace

        [ServiceContractDescription(Name = "新增货位表", Desc = "新增货位表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaPlace", Get = "", Post = "ReaPlace", Return = "BaseResultDataValue", ReturnType = "ReaPlace")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaPlace", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaPlace(ReaPlace entity);

        [ServiceContractDescription(Name = "修改货位表", Desc = "修改货位表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaPlace", Get = "", Post = "ReaPlace", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaPlace", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaPlace(ReaPlace entity);

        [ServiceContractDescription(Name = "修改货位表指定的属性", Desc = "修改货位表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaPlaceByField", Get = "", Post = "ReaPlace", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaPlaceByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaPlaceByField(ReaPlace entity, string fields);

        [ServiceContractDescription(Name = "删除货位表", Desc = "删除货位表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaPlace?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaPlace?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaPlace(long id);

        [ServiceContractDescription(Name = "查询货位表", Desc = "查询货位表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaPlace", Get = "", Post = "ReaPlace", Return = "BaseResultList<ReaPlace>", ReturnType = "ListReaPlace")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaPlace", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaPlace(ReaPlace entity);

        [ServiceContractDescription(Name = "查询货位表(HQL)", Desc = "查询货位表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaPlaceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaPlace>", ReturnType = "ListReaPlace")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaPlaceByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaPlaceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询货位表", Desc = "通过主键ID查询货位表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaPlaceById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaPlace>", ReturnType = "ReaPlace")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaPlaceById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaPlaceById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaStorage

        [ServiceContractDescription(Name = "新增存储库房表", Desc = "新增存储库房表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaStorage", Get = "", Post = "ReaStorage", Return = "BaseResultDataValue", ReturnType = "ReaStorage")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaStorage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaStorage(ReaStorage entity);

        [ServiceContractDescription(Name = "修改存储库房表", Desc = "修改存储库房表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaStorage", Get = "", Post = "ReaStorage", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaStorage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaStorage(ReaStorage entity);

        [ServiceContractDescription(Name = "修改存储库房表指定的属性", Desc = "修改存储库房表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaStorageByField", Get = "", Post = "ReaStorage", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaStorageByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaStorageByField(ReaStorage entity, string fields);

        [ServiceContractDescription(Name = "删除存储库房表", Desc = "删除存储库房表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaStorage?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaStorage?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaStorage(long id);

        [ServiceContractDescription(Name = "查询存储库房表", Desc = "查询存储库房表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaStorage", Get = "", Post = "ReaStorage", Return = "BaseResultList<ReaStorage>", ReturnType = "ListReaStorage")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaStorage", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaStorage(ReaStorage entity);

        [ServiceContractDescription(Name = "查询存储库房表(HQL)", Desc = "查询存储库房表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaStorageByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaStorage>", ReturnType = "ListReaStorage")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaStorageByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaStorageByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询存储库房表", Desc = "通过主键ID查询存储库房表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaStorageById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaStorage>", ReturnType = "ReaStorage")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaStorageById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaStorageById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaReqOperation

        [ServiceContractDescription(Name = "新增采购申请单、订单操作记录表", Desc = "新增采购申请单、订单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaReqOperation", Get = "", Post = "ReaReqOperation", Return = "BaseResultDataValue", ReturnType = "ReaReqOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaReqOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaReqOperation(ReaReqOperation entity);

        [ServiceContractDescription(Name = "修改采购申请单、订单操作记录表", Desc = "修改采购申请单、订单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaReqOperation", Get = "", Post = "ReaReqOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaReqOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaReqOperation(ReaReqOperation entity);

        [ServiceContractDescription(Name = "修改采购申请单、订单操作记录表指定的属性", Desc = "修改采购申请单、订单操作记录表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaReqOperationByField", Get = "", Post = "ReaReqOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaReqOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaReqOperationByField(ReaReqOperation entity, string fields);

        [ServiceContractDescription(Name = "删除采购申请单、订单操作记录表", Desc = "删除采购申请单、订单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaReqOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaReqOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaReqOperation(long id);

        [ServiceContractDescription(Name = "查询采购申请单、订单操作记录表", Desc = "查询采购申请单、订单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaReqOperation", Get = "", Post = "ReaReqOperation", Return = "BaseResultList<ReaReqOperation>", ReturnType = "ListReaReqOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaReqOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaReqOperation(ReaReqOperation entity);

        [ServiceContractDescription(Name = "查询采购申请单、订单操作记录表(HQL)", Desc = "查询采购申请单、订单操作记录表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaReqOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaReqOperation>", ReturnType = "ListReaReqOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaReqOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaReqOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询采购申请单、订单操作记录表", Desc = "通过主键ID查询采购申请单、订单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaReqOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaReqOperation>", ReturnType = "ReaReqOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaReqOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaReqOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaBmsQtyDtl

        [ServiceContractDescription(Name = "新增货品库存表", Desc = "新增货品库存表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsQtyDtl", Get = "", Post = "ReaBmsQtyDtl", Return = "BaseResultDataValue", ReturnType = "ReaBmsQtyDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsQtyDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsQtyDtl(ReaBmsQtyDtl entity);

        [ServiceContractDescription(Name = "修改货品库存表", Desc = "修改货品库存表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsQtyDtl", Get = "", Post = "ReaBmsQtyDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsQtyDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsQtyDtl(ReaBmsQtyDtl entity);

        [ServiceContractDescription(Name = "修改货品库存表指定的属性", Desc = "修改货品库存表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsQtyDtlByField", Get = "", Post = "ReaBmsQtyDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsQtyDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsQtyDtlByField(ReaBmsQtyDtl entity, string fields);

        [ServiceContractDescription(Name = "删除货品库存表", Desc = "删除货品库存表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaBmsQtyDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsQtyDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsQtyDtl(long id);

        [ServiceContractDescription(Name = "查询货品库存表", Desc = "查询货品库存表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyDtl", Get = "", Post = "ReaBmsQtyDtl", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ListReaBmsQtyDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtl(ReaBmsQtyDtl entity);

        [ServiceContractDescription(Name = "查询货品库存表(HQL)", Desc = "查询货品库存表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ListReaBmsQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询货品库存表", Desc = "通过主键ID查询货品库存表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaBmsQtyDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaBmsQtyDtl>", ReturnType = "ReaBmsQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaBmsQtyDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaCheckInOperation

        [ServiceContractDescription(Name = "新增验收单、入库单操作记录表", Desc = "新增验收单、入库单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaCheckInOperation", Get = "", Post = "ReaCheckInOperation", Return = "BaseResultDataValue", ReturnType = "ReaCheckInOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaCheckInOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaCheckInOperation(ReaCheckInOperation entity);

        [ServiceContractDescription(Name = "修改验收单、入库单操作记录表", Desc = "修改验收单、入库单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaCheckInOperation", Get = "", Post = "ReaCheckInOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaCheckInOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaCheckInOperation(ReaCheckInOperation entity);

        [ServiceContractDescription(Name = "修改验收单、入库单操作记录表指定的属性", Desc = "修改验收单、入库单操作记录表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaCheckInOperationByField", Get = "", Post = "ReaCheckInOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaCheckInOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaCheckInOperationByField(ReaCheckInOperation entity, string fields);

        [ServiceContractDescription(Name = "删除验收单、入库单操作记录表", Desc = "删除验收单、入库单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaCheckInOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaCheckInOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaCheckInOperation(long id);

        [ServiceContractDescription(Name = "查询验收单、入库单操作记录表", Desc = "查询验收单、入库单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCheckInOperation", Get = "", Post = "ReaCheckInOperation", Return = "BaseResultList<ReaCheckInOperation>", ReturnType = "ListReaCheckInOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCheckInOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCheckInOperation(ReaCheckInOperation entity);

        [ServiceContractDescription(Name = "查询验收单、入库单操作记录表(HQL)", Desc = "查询验收单、入库单操作记录表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCheckInOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaCheckInOperation>", ReturnType = "ListReaCheckInOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCheckInOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCheckInOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询验收单、入库单操作记录表", Desc = "通过主键ID查询验收单、入库单操作记录表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCheckInOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaCheckInOperation>", ReturnType = "ReaCheckInOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCheckInOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCheckInOperationById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaCenBarCodeFormat

        [ServiceContractDescription(Name = "新增供应商条码格式表", Desc = "新增供应商条码格式表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaCenBarCodeFormat", Get = "", Post = "ReaCenBarCodeFormat", Return = "BaseResultDataValue", ReturnType = "ReaCenBarCodeFormat")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaCenBarCodeFormat", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaCenBarCodeFormat(ReaCenBarCodeFormat entity);

        [ServiceContractDescription(Name = "修改供应商条码格式表", Desc = "修改供应商条码格式表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaCenBarCodeFormat", Get = "", Post = "ReaCenBarCodeFormat", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaCenBarCodeFormat", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaCenBarCodeFormat(ReaCenBarCodeFormat entity);

        [ServiceContractDescription(Name = "修改供应商条码格式表指定的属性", Desc = "修改供应商条码格式表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaCenBarCodeFormatByField", Get = "", Post = "ReaCenBarCodeFormat", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaCenBarCodeFormatByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaCenBarCodeFormatByField(ReaCenBarCodeFormat entity, string fields);

        [ServiceContractDescription(Name = "删除供应商条码格式表", Desc = "删除供应商条码格式表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaCenBarCodeFormat?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaCenBarCodeFormat?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaCenBarCodeFormat(long id);

        [ServiceContractDescription(Name = "查询供应商条码格式表", Desc = "查询供应商条码格式表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenBarCodeFormat", Get = "", Post = "ReaCenBarCodeFormat", Return = "BaseResultList<ReaCenBarCodeFormat>", ReturnType = "ListReaCenBarCodeFormat")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCenBarCodeFormat", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenBarCodeFormat(ReaCenBarCodeFormat entity);

        [ServiceContractDescription(Name = "查询供应商条码格式表(HQL)", Desc = "查询供应商条码格式表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenBarCodeFormatByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaCenBarCodeFormat>", ReturnType = "ListReaCenBarCodeFormat")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCenBarCodeFormatByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenBarCodeFormatByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询供应商条码格式表", Desc = "通过主键ID查询供应商条码格式表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaCenBarCodeFormatById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaCenBarCodeFormat>", ReturnType = "ReaCenBarCodeFormat")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaCenBarCodeFormatById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaCenBarCodeFormatById(long id, string fields, bool isPlanish);
        #endregion

        #region ReaChooseGoodsTemplate

        [ServiceContractDescription(Name = "新增申请明细及订单明细模板表", Desc = "新增申请明细及订单明细模板表", Url = "ReaSysManageService.svc/ST_UDTO_AddReaChooseGoodsTemplate", Get = "", Post = "ReaChooseGoodsTemplate", Return = "BaseResultDataValue", ReturnType = "ReaChooseGoodsTemplate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaChooseGoodsTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaChooseGoodsTemplate(ReaChooseGoodsTemplate entity);

        [ServiceContractDescription(Name = "修改申请明细及订单明细模板表", Desc = "修改申请明细及订单明细模板表", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaChooseGoodsTemplate", Get = "", Post = "ReaChooseGoodsTemplate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaChooseGoodsTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaChooseGoodsTemplate(ReaChooseGoodsTemplate entity);

        [ServiceContractDescription(Name = "修改申请明细及订单明细模板表指定的属性", Desc = "修改申请明细及订单明细模板表指定的属性", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaChooseGoodsTemplateByField", Get = "", Post = "ReaChooseGoodsTemplate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaChooseGoodsTemplateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaChooseGoodsTemplateByField(ReaChooseGoodsTemplate entity, string fields);

        [ServiceContractDescription(Name = "删除申请明细及订单明细模板表", Desc = "删除申请明细及订单明细模板表", Url = "ReaSysManageService.svc/ST_UDTO_DelReaChooseGoodsTemplate?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaChooseGoodsTemplate?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaChooseGoodsTemplate(long id);

        [ServiceContractDescription(Name = "查询申请明细及订单明细模板表", Desc = "查询申请明细及订单明细模板表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaChooseGoodsTemplate", Get = "", Post = "ReaChooseGoodsTemplate", Return = "BaseResultList<ReaChooseGoodsTemplate>", ReturnType = "ListReaChooseGoodsTemplate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaChooseGoodsTemplate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaChooseGoodsTemplate(ReaChooseGoodsTemplate entity);

        [ServiceContractDescription(Name = "查询申请明细及订单明细模板表(HQL)", Desc = "查询申请明细及订单明细模板表(HQL)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaChooseGoodsTemplateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaChooseGoodsTemplate>", ReturnType = "ListReaChooseGoodsTemplate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaChooseGoodsTemplateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaChooseGoodsTemplateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询申请明细及订单明细模板表", Desc = "通过主键ID查询申请明细及订单明细模板表", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaChooseGoodsTemplateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ReaChooseGoodsTemplate>", ReturnType = "ReaChooseGoodsTemplate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaChooseGoodsTemplateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaChooseGoodsTemplateById(long id, string fields, bool isPlanish);
        #endregion

        #region 客户端部门采购定制服务

        [ServiceContractDescription(Name = "依员工Id获取员工申请录入的申请部门", Desc = "依员工Id获取员工申请录入的申请部门", Url = "ReaSysManageService.svc/ST_UDTO_SearchApplyHRDeptByByHRDeptId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&deptId={deptId}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&deptId={deptId}", Post = "", Return = "BaseResultList<ReaDeptGoods>", ReturnType = "ListReaDeptGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchApplyHRDeptByByHRDeptId?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&deptId={deptId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchApplyHRDeptByByHRDeptId(int page, int limit, string fields, string where, string sort, bool isPlanish, long deptId);

        [ServiceContractDescription(Name = "获取部门货品的全部货品与货品所属供应商信息(按货品进行分组,找出每组货品下的对应的供应商信息)", Desc = "获取部门货品的全部货品与货品所属供应商信息(按货品进行分组,找出每组货品下的对应的供应商信息)", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsOrgLinkByHRDeptId?deptId={deptId}", Get = "deptId={deptId}", Post = "", Return = "BaseResultList<ReaCenOrgGoodsVO>", ReturnType = "ListReaCenOrgGoodsVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsOrgLinkByHRDeptId?deptId={deptId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkByHRDeptId(long deptId);

        [ServiceContractDescription(Name = "获取采购申请货品库存数量", Desc = "获取采购申请货品库存数量", Url = "ReaSysManageService.svc/ST_UDTO_SearchReaGoodsCurrentQtyByGoodIdStr?idStr={idStr}&goodIdStr={goodIdStr}", Get = "idStr={idStr}&goodIdStr={goodIdStr}", Post = "", Return = "BaseResultList<ReaGoodsCurrentQtyVO>", ReturnType = "ListReaGoodsCurrentQtyVO")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchReaGoodsCurrentQtyByGoodIdStr?idStr={idStr}&goodIdStr={goodIdStr}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchReaGoodsCurrentQtyByGoodIdStr(string idStr, string goodIdStr);

        [ServiceContractDescription(Name = "部门采购申请新增服务", Desc = "部门采购申请新增服务", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsReqDocAndDt", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsReqDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsReqDocAndDt", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsReqDocAndDt(ReaBmsReqDoc entity, IList<ReaBmsReqDtl> dtAddList);

        [ServiceContractDescription(Name = "部门采购申请更新服务", Desc = "部门采购申请更新服务", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDocAndDt", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDocAndDt", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDocAndDt(ReaBmsReqDoc entity, string fields, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList);

        [ServiceContractDescription(Name = "部门采购申请明细更新(验证主单后只操作明细)", Desc = "部门采购申请明细更新(验证主单后只操作明细)", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDtlOfCheck", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDtlOfCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDtlOfCheck(ReaBmsReqDoc entity, string fields, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList);

        [ServiceContractDescription(Name = "部门采购申请审核服务", Desc = "部门采购申请审核服务", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck", Get = "", Post = "ReaBmsReqDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck(ReaBmsReqDoc entity, string fields, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList);

        [ServiceContractDescription(Name = "依据客户端的申请主单(已审核)生成客户端订单信息", Desc = "依据客户端的申请主单(已审核)生成客户端订单信息", Url = "ReaSysManageService.svc/ST_UDTO_AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr", Get = "", Post = "String", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr(string idStr);
        #endregion

        #region 客户端订单处理
        [ServiceContractDescription(Name = "客户端订单新增服务", Desc = "客户端订单新增服务", Url = "ReaSysManageService.svc/ST_UDTO_AddReaBmsCenOrderDocAndDt", Get = "", Post = "BmsCenOrderDoc", Return = "BaseResultDataValue", ReturnType = "ReaBmsReqDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddReaBmsCenOrderDocAndDt", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddReaBmsCenOrderDocAndDt(BmsCenOrderDoc entity, IList<BmsCenOrderDtl> dtAddList);

        [ServiceContractDescription(Name = "客户端订单更新服务", Desc = "客户端订单更新服务", Url = "ReaSysManageService.svc/ST_UDTO_UpdateReaBmsCenOrderDocAndDt", Get = "", Post = "BmsCenOrderDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateReaBmsCenOrderDocAndDt", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateReaBmsCenOrderDocAndDt(BmsCenOrderDoc entity, string fields, IList<BmsCenOrderDtl> dtAddList, IList<BmsCenOrderDtl> dtEditList);

        [ServiceContractDescription(Name = "删除平台订货明细(并重新更新订单总价)", Desc = "删除平台订货明细(并重新更新订单总价)", Url = "ReagentSysService.svc/ST_UDTO_DelReaBmsCenOrderDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelReaBmsCenOrderDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelReaBmsCenOrderDtl(long id);
        #endregion

    }
}
