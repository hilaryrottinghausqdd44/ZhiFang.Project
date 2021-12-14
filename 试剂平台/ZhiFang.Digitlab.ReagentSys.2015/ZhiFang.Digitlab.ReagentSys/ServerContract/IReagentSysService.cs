using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ZhiFang.Digitlab.ServiceCommon;
using System.ServiceModel.Web;
using ZhiFang.Digitlab.Entity;
using ZhiFang.Digitlab.Entity.ReagentSys;
using System.IO;
using System.ServiceModel.Channels;

namespace ZhiFang.Digitlab.ReagentSys.ServerContract
{
    [ServiceContract]
    public interface IReagentSysService
    {
        #region BmsCenOrderDoc

        [ServiceContractDescription(Name = "新增平台订货总单", Desc = "新增平台订货总单", Url = "ReagentSysService.svc/ST_UDTO_AddBmsCenOrderDoc", Get = "", Post = "BmsCenOrderDoc", Return = "BaseResultDataValue", ReturnType = "BmsCenOrderDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsCenOrderDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsCenOrderDoc(BmsCenOrderDoc entity);

        [ServiceContractDescription(Name = "修改平台订货总单", Desc = "修改平台订货总单", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenOrderDoc", Get = "", Post = "BmsCenOrderDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenOrderDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenOrderDoc(BmsCenOrderDoc entity);

        [ServiceContractDescription(Name = "修改平台订货总单指定的属性", Desc = "修改平台订货总单指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenOrderDocByField", Get = "", Post = "BmsCenOrderDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenOrderDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenOrderDocByField(BmsCenOrderDoc entity, string fields);

        [ServiceContractDescription(Name = "删除平台订货总单", Desc = "删除平台订货总单", Url = "ReagentSysService.svc/ST_UDTO_DelBmsCenOrderDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBmsCenOrderDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBmsCenOrderDoc(long id);

        [ServiceContractDescription(Name = "查询平台订货总单", Desc = "查询平台订货总单", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDoc", Get = "", Post = "BmsCenOrderDoc", Return = "BaseResultList<BmsCenOrderDoc>", ReturnType = "ListBmsCenOrderDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenOrderDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenOrderDoc(BmsCenOrderDoc entity);

        [ServiceContractDescription(Name = "查询平台订货总单(HQL)", Desc = "查询平台订货总单(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenOrderDoc>", ReturnType = "ListBmsCenOrderDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenOrderDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台订货总单", Desc = "通过主键ID查询平台订货总单", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenOrderDoc>", ReturnType = "BmsCenOrderDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenOrderDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocById(long id, string fields, bool isPlanish);
        #endregion

        #region BmsCenOrderDtl

        [ServiceContractDescription(Name = "新增平台订货明细", Desc = "新增平台订货明细", Url = "ReagentSysService.svc/ST_UDTO_AddBmsCenOrderDtl", Get = "", Post = "BmsCenOrderDtl", Return = "BaseResultDataValue", ReturnType = "BmsCenOrderDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsCenOrderDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsCenOrderDtl(BmsCenOrderDtl entity);

        [ServiceContractDescription(Name = "修改平台订货明细", Desc = "修改平台订货明细", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenOrderDtl", Get = "", Post = "BmsCenOrderDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenOrderDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenOrderDtl(BmsCenOrderDtl entity);

        [ServiceContractDescription(Name = "修改平台订货明细指定的属性", Desc = "修改平台订货明细指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenOrderDtlByField", Get = "", Post = "BmsCenOrderDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenOrderDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenOrderDtlByField(BmsCenOrderDtl entity, string fields);

        [ServiceContractDescription(Name = "删除平台订货明细", Desc = "删除平台订货明细", Url = "ReagentSysService.svc/ST_UDTO_DelBmsCenOrderDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBmsCenOrderDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBmsCenOrderDtl(long id);

        [ServiceContractDescription(Name = "查询平台订货明细", Desc = "查询平台订货明细", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDtl", Get = "", Post = "BmsCenOrderDtl", Return = "BaseResultList<BmsCenOrderDtl>", ReturnType = "ListBmsCenOrderDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenOrderDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtl(BmsCenOrderDtl entity);

        [ServiceContractDescription(Name = "查询平台订货明细(HQL)", Desc = "查询平台订货明细(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenOrderDtl>", ReturnType = "ListBmsCenOrderDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenOrderDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台订货明细", Desc = "通过主键ID查询平台订货明细", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenOrderDtl>", ReturnType = "BmsCenOrderDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenOrderDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region BmsCenSaleDoc

        [ServiceContractDescription(Name = "新增平台供货总单", Desc = "新增平台供货总单", Url = "ReagentSysService.svc/ST_UDTO_AddBmsCenSaleDoc", Get = "", Post = "BmsCenSaleDoc", Return = "BaseResultDataValue", ReturnType = "BmsCenSaleDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsCenSaleDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsCenSaleDoc(BmsCenSaleDoc entity);

        [ServiceContractDescription(Name = "修改平台供货总单", Desc = "修改平台供货总单", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDoc", Get = "", Post = "BmsCenSaleDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDoc(BmsCenSaleDoc entity);

        [ServiceContractDescription(Name = "修改平台供货总单指定的属性", Desc = "修改平台供货总单指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDocByField", Get = "", Post = "BmsCenSaleDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDocByField(BmsCenSaleDoc entity, string fields);

        [ServiceContractDescription(Name = "删除平台供货总单", Desc = "删除平台供货总单", Url = "ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBmsCenSaleDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBmsCenSaleDoc(long id);

        [ServiceContractDescription(Name = "查询平台供货总单", Desc = "查询平台供货总单", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDoc", Get = "", Post = "BmsCenSaleDoc", Return = "BaseResultList<BmsCenSaleDoc>", ReturnType = "ListBmsCenSaleDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDoc(BmsCenSaleDoc entity);

        [ServiceContractDescription(Name = "查询平台供货总单(HQL)", Desc = "查询平台供货总单(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDoc>", ReturnType = "ListBmsCenSaleDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台供货总单", Desc = "通过主键ID查询平台供货总单", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDoc>", ReturnType = "BmsCenSaleDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocById(long id, string fields, bool isPlanish);
        #endregion

        #region BmsCenSaleDtl

        [ServiceContractDescription(Name = "新增平台供货明细", Desc = "新增平台供货明细", Url = "ReagentSysService.svc/ST_UDTO_AddBmsCenSaleDtl", Get = "", Post = "BmsCenSaleDtl", Return = "BaseResultDataValue", ReturnType = "BmsCenSaleDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsCenSaleDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsCenSaleDtl(BmsCenSaleDtl entity);

        [ServiceContractDescription(Name = "修改平台供货明细", Desc = "修改平台供货明细", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDtl", Get = "", Post = "BmsCenSaleDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDtl(BmsCenSaleDtl entity);

        [ServiceContractDescription(Name = "修改平台供货明细指定的属性", Desc = "修改平台供货明细指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDtlByField", Get = "", Post = "BmsCenSaleDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlByField(BmsCenSaleDtl entity, string fields);

        [ServiceContractDescription(Name = "删除平台供货明细", Desc = "删除平台供货明细", Url = "ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBmsCenSaleDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBmsCenSaleDtl(long id);

        [ServiceContractDescription(Name = "查询平台供货明细", Desc = "查询平台供货明细", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtl", Get = "", Post = "BmsCenSaleDtl", Return = "BaseResultList<BmsCenSaleDtl>", ReturnType = "ListBmsCenSaleDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtl(BmsCenSaleDtl entity);

        [ServiceContractDescription(Name = "查询平台供货明细(HQL)", Desc = "查询平台供货明细(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDtl>", ReturnType = "ListBmsCenSaleDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台供货明细", Desc = "通过主键ID查询平台供货明细", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDtl>", ReturnType = "BmsCenSaleDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region BmsCenSaleDtlBarCode

        [ServiceContractDescription(Name = "新增BmsCenSaleDtlBarCode", Desc = "新增BmsCenSaleDtlBarCode", Url = "ReagentSysService.svc/ST_UDTO_AddBmsCenSaleDtlBarCode", Get = "", Post = "BmsCenSaleDtlBarCode", Return = "BaseResultDataValue", ReturnType = "BmsCenSaleDtlBarCode")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsCenSaleDtlBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsCenSaleDtlBarCode(BmsCenSaleDtlBarCode entity);

        [ServiceContractDescription(Name = "修改BmsCenSaleDtlBarCode", Desc = "修改BmsCenSaleDtlBarCode", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDtlBarCode", Get = "", Post = "BmsCenSaleDtlBarCode", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDtlBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlBarCode(BmsCenSaleDtlBarCode entity);

        [ServiceContractDescription(Name = "修改BmsCenSaleDtlBarCode指定的属性", Desc = "修改BmsCenSaleDtlBarCode指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDtlBarCodeByField", Get = "", Post = "BmsCenSaleDtlBarCode", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDtlBarCodeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlBarCodeByField(BmsCenSaleDtlBarCode entity, string fields);

        [ServiceContractDescription(Name = "删除BmsCenSaleDtlBarCode", Desc = "删除BmsCenSaleDtlBarCode", Url = "ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDtlBarCode?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBmsCenSaleDtlBarCode?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBmsCenSaleDtlBarCode(long id);

        [ServiceContractDescription(Name = "查询BmsCenSaleDtlBarCode", Desc = "查询BmsCenSaleDtlBarCode", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlBarCode", Get = "", Post = "BmsCenSaleDtlBarCode", Return = "BaseResultList<BmsCenSaleDtlBarCode>", ReturnType = "ListBmsCenSaleDtlBarCode")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDtlBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlBarCode(BmsCenSaleDtlBarCode entity);

        [ServiceContractDescription(Name = "查询BmsCenSaleDtlBarCode(HQL)", Desc = "查询BmsCenSaleDtlBarCode(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlBarCodeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDtlBarCode>", ReturnType = "ListBmsCenSaleDtlBarCode")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDtlBarCodeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlBarCodeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询BmsCenSaleDtlBarCode", Desc = "通过主键ID查询BmsCenSaleDtlBarCode", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlBarCodeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDtlBarCode>", ReturnType = "BmsCenSaleDtlBarCode")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDtlBarCodeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlBarCodeById(long id, string fields, bool isPlanish);
        #endregion

        #region CenOrg

        [ServiceContractDescription(Name = "新增机构信息", Desc = "新增机构信息", Url = "ReagentSysService.svc/ST_UDTO_AddCenOrg", Get = "", Post = "CenOrg", Return = "BaseResultDataValue", ReturnType = "CenOrg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddCenOrg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddCenOrg(CenOrg entity);

        [ServiceContractDescription(Name = "修改机构信息", Desc = "修改机构信息", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenOrg", Get = "", Post = "CenOrg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenOrg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenOrg(CenOrg entity);

        [ServiceContractDescription(Name = "修改机构信息指定的属性", Desc = "修改机构信息指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenOrgByField", Get = "", Post = "CenOrg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenOrgByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenOrgByField(CenOrg entity, string fields);

        [ServiceContractDescription(Name = "删除机构信息", Desc = "删除机构信息", Url = "ReagentSysService.svc/ST_UDTO_DelCenOrg?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelCenOrg?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelCenOrg(long id);

        [ServiceContractDescription(Name = "查询机构信息", Desc = "查询机构信息", Url = "ReagentSysService.svc/ST_UDTO_SearchCenOrg", Get = "", Post = "CenOrg", Return = "BaseResultList<CenOrg>", ReturnType = "ListCenOrg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrg(CenOrg entity);

        [ServiceContractDescription(Name = "查询机构信息(HQL)", Desc = "查询机构信息(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchCenOrgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenOrg>", ReturnType = "ListCenOrg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询机构信息", Desc = "通过主键ID查询机构信息", Url = "ReagentSysService.svc/ST_UDTO_SearchCenOrgById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenOrg>", ReturnType = "CenOrg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrgById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrgById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "机构信息导入Excel", Desc = "机构信息导入Excel", Url = "ReagentSysService.svc/ST_UDTO_CenOrgUploadExcelData", Get = "", Post = "UploadExcelFile", Return = "BaseResultList<CenOrg>", ReturnType = "CenOrg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_CenOrgUploadExcelData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_CenOrgUploadExcelData();
        #endregion

        #region CenOrgCondition

        [ServiceContractDescription(Name = "新增机构关系", Desc = "新增机构关系", Url = "ReagentSysService.svc/ST_UDTO_AddCenOrgCondition", Get = "", Post = "CenOrgCondition", Return = "BaseResultDataValue", ReturnType = "CenOrgCondition")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddCenOrgCondition", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddCenOrgCondition(CenOrgCondition entity);

        [ServiceContractDescription(Name = "修改机构关系", Desc = "修改机构关系", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenOrgCondition", Get = "", Post = "CenOrgCondition", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenOrgCondition", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenOrgCondition(CenOrgCondition entity);

        [ServiceContractDescription(Name = "修改机构关系指定的属性", Desc = "修改机构关系指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenOrgConditionByField", Get = "", Post = "CenOrgCondition", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenOrgConditionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenOrgConditionByField(CenOrgCondition entity, string fields);

        [ServiceContractDescription(Name = "删除机构关系", Desc = "删除机构关系", Url = "ReagentSysService.svc/ST_UDTO_DelCenOrgCondition?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelCenOrgCondition?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelCenOrgCondition(long id);

        [ServiceContractDescription(Name = "查询机构关系", Desc = "查询机构关系", Url = "ReagentSysService.svc/ST_UDTO_SearchCenOrgCondition", Get = "", Post = "CenOrgCondition", Return = "BaseResultList<CenOrgCondition>", ReturnType = "ListCenOrgCondition")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrgCondition", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrgCondition(CenOrgCondition entity);

        [ServiceContractDescription(Name = "查询机构关系(HQL)", Desc = "查询机构关系(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchCenOrgConditionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenOrgCondition>", ReturnType = "ListCenOrgCondition")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrgConditionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrgConditionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询机构关系", Desc = "通过主键ID查询机构关系", Url = "ReagentSysService.svc/ST_UDTO_SearchCenOrgConditionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenOrgCondition>", ReturnType = "CenOrgCondition")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrgConditionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrgConditionById(long id, string fields, bool isPlanish);
        #endregion

        #region CenOrgType

        [ServiceContractDescription(Name = "新增机构类型", Desc = "新增机构类型", Url = "ReagentSysService.svc/ST_UDTO_AddCenOrgType", Get = "", Post = "CenOrgType", Return = "BaseResultDataValue", ReturnType = "CenOrgType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddCenOrgType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddCenOrgType(CenOrgType entity);

        [ServiceContractDescription(Name = "修改机构类型", Desc = "修改机构类型", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenOrgType", Get = "", Post = "CenOrgType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenOrgType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenOrgType(CenOrgType entity);

        [ServiceContractDescription(Name = "修改机构类型指定的属性", Desc = "修改机构类型指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenOrgTypeByField", Get = "", Post = "CenOrgType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenOrgTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenOrgTypeByField(CenOrgType entity, string fields);

        [ServiceContractDescription(Name = "删除机构类型", Desc = "删除机构类型", Url = "ReagentSysService.svc/ST_UDTO_DelCenOrgType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelCenOrgType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelCenOrgType(long id);

        [ServiceContractDescription(Name = "查询机构类型", Desc = "查询机构类型", Url = "ReagentSysService.svc/ST_UDTO_SearchCenOrgType", Get = "", Post = "CenOrgType", Return = "BaseResultList<CenOrgType>", ReturnType = "ListCenOrgType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrgType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrgType(CenOrgType entity);

        [ServiceContractDescription(Name = "查询机构类型(HQL)", Desc = "查询机构类型(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchCenOrgTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenOrgType>", ReturnType = "ListCenOrgType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrgTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrgTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询机构类型", Desc = "通过主键ID查询机构类型", Url = "ReagentSysService.svc/ST_UDTO_SearchCenOrgTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenOrgType>", ReturnType = "CenOrgType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenOrgTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenOrgTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region CenMsg

        [ServiceContractDescription(Name = "新增平台消息表", Desc = "新增平台消息表", Url = "ReagentSysService.svc/ST_UDTO_AddCenMsg", Get = "", Post = "CenMsg", Return = "BaseResultDataValue", ReturnType = "CenMsg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddCenMsg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddCenMsg(CenMsg entity);

        [ServiceContractDescription(Name = "修改平台消息表", Desc = "修改平台消息表", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenMsg", Get = "", Post = "CenMsg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenMsg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenMsg(CenMsg entity);

        [ServiceContractDescription(Name = "修改平台消息表指定的属性", Desc = "修改平台消息表指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenMsgByField", Get = "", Post = "CenMsg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenMsgByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenMsgByField(CenMsg entity, string fields);

        [ServiceContractDescription(Name = "删除平台消息表", Desc = "删除平台消息表", Url = "ReagentSysService.svc/ST_UDTO_DelCenMsg?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelCenMsg?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelCenMsg(long id);

        [ServiceContractDescription(Name = "查询平台消息表", Desc = "查询平台消息表", Url = "ReagentSysService.svc/ST_UDTO_SearchCenMsg", Get = "", Post = "CenMsg", Return = "BaseResultList<CenMsg>", ReturnType = "ListCenMsg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenMsg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenMsg(CenMsg entity);

        [ServiceContractDescription(Name = "查询平台消息表(HQL)", Desc = "查询平台消息表(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchCenMsgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenMsg>", ReturnType = "ListCenMsg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenMsgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenMsgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台消息表", Desc = "通过主键ID查询平台消息表", Url = "ReagentSysService.svc/ST_UDTO_SearchCenMsgById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenMsg>", ReturnType = "CenMsg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenMsgById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenMsgById(long id, string fields, bool isPlanish);
        #endregion

        #region CenQtyDtl

        [ServiceContractDescription(Name = "新增平台库存表", Desc = "新增平台库存表", Url = "ReagentSysService.svc/ST_UDTO_AddCenQtyDtl", Get = "", Post = "CenQtyDtl", Return = "BaseResultDataValue", ReturnType = "CenQtyDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddCenQtyDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddCenQtyDtl(CenQtyDtl entity);

        [ServiceContractDescription(Name = "修改平台库存表", Desc = "修改平台库存表", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenQtyDtl", Get = "", Post = "CenQtyDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenQtyDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenQtyDtl(CenQtyDtl entity);

        [ServiceContractDescription(Name = "修改平台库存表指定的属性", Desc = "修改平台库存表指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenQtyDtlByField", Get = "", Post = "CenQtyDtl", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenQtyDtlByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenQtyDtlByField(CenQtyDtl entity, string fields);

        [ServiceContractDescription(Name = "删除平台库存表", Desc = "删除平台库存表", Url = "ReagentSysService.svc/ST_UDTO_DelCenQtyDtl?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelCenQtyDtl?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelCenQtyDtl(long id);

        [ServiceContractDescription(Name = "查询平台库存表", Desc = "查询平台库存表", Url = "ReagentSysService.svc/ST_UDTO_SearchCenQtyDtl", Get = "", Post = "CenQtyDtl", Return = "BaseResultList<CenQtyDtl>", ReturnType = "ListCenQtyDtl")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenQtyDtl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenQtyDtl(CenQtyDtl entity);

        [ServiceContractDescription(Name = "查询平台库存表(HQL)", Desc = "查询平台库存表(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchCenQtyDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenQtyDtl>", ReturnType = "ListCenQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenQtyDtlByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenQtyDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台库存表", Desc = "通过主键ID查询平台库存表", Url = "ReagentSysService.svc/ST_UDTO_SearchCenQtyDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenQtyDtl>", ReturnType = "CenQtyDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenQtyDtlById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenQtyDtlById(long id, string fields, bool isPlanish);
        #endregion

        #region Goods

        [ServiceContractDescription(Name = "新增平台产品", Desc = "新增平台产品", Url = "ReagentSysService.svc/ST_UDTO_AddGoods", Get = "", Post = "Goods", Return = "BaseResultDataValue", ReturnType = "Goods")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddGoods(Goods entity);

        [ServiceContractDescription(Name = "修改平台产品", Desc = "修改平台产品", Url = "ReagentSysService.svc/ST_UDTO_UpdateGoods", Get = "", Post = "Goods", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGoods(Goods entity);

        [ServiceContractDescription(Name = "修改平台产品指定的属性", Desc = "修改平台产品指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateGoodsByField", Get = "", Post = "Goods", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGoodsByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGoodsByField(Goods entity, string fields);

        [ServiceContractDescription(Name = "删除平台产品", Desc = "删除平台产品", Url = "ReagentSysService.svc/ST_UDTO_DelGoods?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelGoods?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelGoods(long id);

        [ServiceContractDescription(Name = "查询平台产品", Desc = "查询平台产品", Url = "ReagentSysService.svc/ST_UDTO_SearchGoods", Get = "", Post = "Goods", Return = "BaseResultList<Goods>", ReturnType = "ListGoods")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGoods", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGoods(Goods entity);

        [ServiceContractDescription(Name = "查询平台产品(HQL)", Desc = "查询平台产品(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchGoodsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Goods>", ReturnType = "ListGoods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGoodsByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGoodsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台产品", Desc = "通过主键ID查询平台产品", Url = "ReagentSysService.svc/ST_UDTO_SearchGoodsById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Goods>", ReturnType = "Goods")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGoodsById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGoodsById(long id, string fields, bool isPlanish);

        #endregion

        #region GoodsRegister
        [ServiceContractDescription(Name = "通过FormData方式新增产品注册证信息", Desc = "通过FormData方式新增产品注册证信息", Url = "ReagentSysService.svc/ST_UDTO_AddGoodsRegisterAndUploadRegisterFile", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddGoodsRegisterAndUploadRegisterFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_AddGoodsRegisterAndUploadRegisterFile();

        [ServiceContractDescription(Name = "通过FormData方式更新产品注册证信息", Desc = "通过FormData方式更新产品注册证信息", Url = "ReagentSysService.svc/ST_UDTO_UpdateGoodsRegisterAndUploadRegisterFileByField", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGoodsRegisterAndUploadRegisterFileByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_UpdateGoodsRegisterAndUploadRegisterFileByField();

        [ServiceContractDescription(Name = "预览产品注册证文件", Desc = "预览产品注册证文件", Url = "ReagentSysService.svc/ST_UDTO_GoodsRegisterPreviewPdf?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GoodsRegisterPreviewPdf?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_GoodsRegisterPreviewPdf(long id, long operateType);

        [ServiceContractDescription(Name = "查询过滤掉重复的注册证编号的产品注册证表(HQL)", Desc = "查询过滤掉重复的注册证编号的产品注册证表(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GoodsRegister>", ReturnType = "ListGoodsRegister")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGoodsRegisterOfFilterRepeatRegisterNoByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "新增产品注册证表", Desc = "新增产品注册证表", Url = "ReagentSysService.svc/ST_UDTO_AddGoodsRegister", Get = "", Post = "GoodsRegister", Return = "BaseResultDataValue", ReturnType = "GoodsRegister")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddGoodsRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddGoodsRegister(GoodsRegister entity);

        [ServiceContractDescription(Name = "修改产品注册证表", Desc = "修改产品注册证表", Url = "ReagentSysService.svc/ST_UDTO_UpdateGoodsRegister", Get = "", Post = "GoodsRegister", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGoodsRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGoodsRegister(GoodsRegister entity);

        [ServiceContractDescription(Name = "修改产品注册证表指定的属性", Desc = "修改产品注册证表指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateGoodsRegisterByField", Get = "", Post = "GoodsRegister", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGoodsRegisterByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGoodsRegisterByField(GoodsRegister entity, string fields);

        [ServiceContractDescription(Name = "删除产品注册证表", Desc = "删除产品注册证表", Url = "ReagentSysService.svc/ST_UDTO_DelGoodsRegister?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelGoodsRegister?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelGoodsRegister(long id);

        [ServiceContractDescription(Name = "查询产品注册证表", Desc = "查询产品注册证表", Url = "ReagentSysService.svc/ST_UDTO_SearchGoodsRegister", Get = "", Post = "GoodsRegister", Return = "BaseResultList<GoodsRegister>", ReturnType = "ListGoodsRegister")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGoodsRegister", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGoodsRegister(GoodsRegister entity);

        [ServiceContractDescription(Name = "查询产品注册证表(HQL)", Desc = "查询产品注册证表(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchGoodsRegisterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GoodsRegister>", ReturnType = "ListGoodsRegister")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGoodsRegisterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGoodsRegisterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询产品注册证表", Desc = "通过主键ID查询产品注册证表", Url = "ReagentSysService.svc/ST_UDTO_SearchGoodsRegisterById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GoodsRegister>", ReturnType = "GoodsRegister")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGoodsRegisterById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGoodsRegisterById(long id, string fields, bool isPlanish);
        #endregion

        #region BmsCenOrderDocHistory

        [ServiceContractDescription(Name = "新增平台订货总单历史表", Desc = "新增平台订货总单历史表", Url = "ReagentSysService.svc/ST_UDTO_AddBmsCenOrderDocHistory", Get = "", Post = "BmsCenOrderDocHistory", Return = "BaseResultDataValue", ReturnType = "BmsCenOrderDocHistory")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsCenOrderDocHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsCenOrderDocHistory(BmsCenOrderDocHistory entity);

        [ServiceContractDescription(Name = "修改平台订货总单历史表", Desc = "修改平台订货总单历史表", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenOrderDocHistory", Get = "", Post = "BmsCenOrderDocHistory", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenOrderDocHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenOrderDocHistory(BmsCenOrderDocHistory entity);

        [ServiceContractDescription(Name = "修改平台订货总单历史表指定的属性", Desc = "修改平台订货总单历史表指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenOrderDocHistoryByField", Get = "", Post = "BmsCenOrderDocHistory", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenOrderDocHistoryByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenOrderDocHistoryByField(BmsCenOrderDocHistory entity, string fields);

        [ServiceContractDescription(Name = "删除平台订货总单历史表", Desc = "删除平台订货总单历史表", Url = "ReagentSysService.svc/ST_UDTO_DelBmsCenOrderDocHistory?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBmsCenOrderDocHistory?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBmsCenOrderDocHistory(long id);

        [ServiceContractDescription(Name = "查询平台订货总单历史表", Desc = "查询平台订货总单历史表", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocHistory", Get = "", Post = "BmsCenOrderDocHistory", Return = "BaseResultList<BmsCenOrderDocHistory>", ReturnType = "ListBmsCenOrderDocHistory")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenOrderDocHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocHistory(BmsCenOrderDocHistory entity);

        [ServiceContractDescription(Name = "查询平台订货总单历史表(HQL)", Desc = "查询平台订货总单历史表(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocHistoryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenOrderDocHistory>", ReturnType = "ListBmsCenOrderDocHistory")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenOrderDocHistoryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台订货总单历史表", Desc = "通过主键ID查询平台订货总单历史表", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDocHistoryById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenOrderDocHistory>", ReturnType = "BmsCenOrderDocHistory")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenOrderDocHistoryById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenOrderDocHistoryById(long id, string fields, bool isPlanish);
        #endregion

        #region BmsCenOrderDtlHistory

        [ServiceContractDescription(Name = "新增平台订货明细历史表", Desc = "新增平台订货明细历史表", Url = "ReagentSysService.svc/ST_UDTO_AddBmsCenOrderDtlHistory", Get = "", Post = "BmsCenOrderDtlHistory", Return = "BaseResultDataValue", ReturnType = "BmsCenOrderDtlHistory")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsCenOrderDtlHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsCenOrderDtlHistory(BmsCenOrderDtlHistory entity);

        [ServiceContractDescription(Name = "修改平台订货明细历史表", Desc = "修改平台订货明细历史表", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenOrderDtlHistory", Get = "", Post = "BmsCenOrderDtlHistory", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenOrderDtlHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenOrderDtlHistory(BmsCenOrderDtlHistory entity);

        [ServiceContractDescription(Name = "修改平台订货明细历史表指定的属性", Desc = "修改平台订货明细历史表指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenOrderDtlHistoryByField", Get = "", Post = "BmsCenOrderDtlHistory", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenOrderDtlHistoryByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenOrderDtlHistoryByField(BmsCenOrderDtlHistory entity, string fields);

        [ServiceContractDescription(Name = "删除平台订货明细历史表", Desc = "删除平台订货明细历史表", Url = "ReagentSysService.svc/ST_UDTO_DelBmsCenOrderDtlHistory?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBmsCenOrderDtlHistory?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBmsCenOrderDtlHistory(long id);

        [ServiceContractDescription(Name = "查询平台订货明细历史表", Desc = "查询平台订货明细历史表", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDtlHistory", Get = "", Post = "BmsCenOrderDtlHistory", Return = "BaseResultList<BmsCenOrderDtlHistory>", ReturnType = "ListBmsCenOrderDtlHistory")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenOrderDtlHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlHistory(BmsCenOrderDtlHistory entity);

        [ServiceContractDescription(Name = "查询平台订货明细历史表(HQL)", Desc = "查询平台订货明细历史表(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDtlHistoryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenOrderDtlHistory>", ReturnType = "ListBmsCenOrderDtlHistory")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenOrderDtlHistoryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台订货明细历史表", Desc = "通过主键ID查询平台订货明细历史表", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenOrderDtlHistoryById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenOrderDtlHistory>", ReturnType = "BmsCenOrderDtlHistory")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenOrderDtlHistoryById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenOrderDtlHistoryById(long id, string fields, bool isPlanish);
        #endregion

        #region BmsCenSaleDocHistory

        [ServiceContractDescription(Name = "新增平台供货总单历史表", Desc = "新增平台供货总单历史表", Url = "ReagentSysService.svc/ST_UDTO_AddBmsCenSaleDocHistory", Get = "", Post = "BmsCenSaleDocHistory", Return = "BaseResultDataValue", ReturnType = "BmsCenSaleDocHistory")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsCenSaleDocHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsCenSaleDocHistory(BmsCenSaleDocHistory entity);

        [ServiceContractDescription(Name = "修改平台供货总单历史表", Desc = "修改平台供货总单历史表", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDocHistory", Get = "", Post = "BmsCenSaleDocHistory", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDocHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDocHistory(BmsCenSaleDocHistory entity);

        [ServiceContractDescription(Name = "修改平台供货总单历史表指定的属性", Desc = "修改平台供货总单历史表指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDocHistoryByField", Get = "", Post = "BmsCenSaleDocHistory", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDocHistoryByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDocHistoryByField(BmsCenSaleDocHistory entity, string fields);

        [ServiceContractDescription(Name = "删除平台供货总单历史表", Desc = "删除平台供货总单历史表", Url = "ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDocHistory?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBmsCenSaleDocHistory?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBmsCenSaleDocHistory(long id);

        [ServiceContractDescription(Name = "查询平台供货总单历史表", Desc = "查询平台供货总单历史表", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocHistory", Get = "", Post = "BmsCenSaleDocHistory", Return = "BaseResultList<BmsCenSaleDocHistory>", ReturnType = "ListBmsCenSaleDocHistory")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDocHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocHistory(BmsCenSaleDocHistory entity);

        [ServiceContractDescription(Name = "查询平台供货总单历史表(HQL)", Desc = "查询平台供货总单历史表(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocHistoryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDocHistory>", ReturnType = "ListBmsCenSaleDocHistory")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDocHistoryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台供货总单历史表", Desc = "通过主键ID查询平台供货总单历史表", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocHistoryById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDocHistory>", ReturnType = "BmsCenSaleDocHistory")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDocHistoryById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocHistoryById(long id, string fields, bool isPlanish);
        #endregion

        #region BmsCenSaleDtlHistory

        [ServiceContractDescription(Name = "新增平台供货明细历史表", Desc = "新增平台供货明细历史表", Url = "ReagentSysService.svc/ST_UDTO_AddBmsCenSaleDtlHistory", Get = "", Post = "BmsCenSaleDtlHistory", Return = "BaseResultDataValue", ReturnType = "BmsCenSaleDtlHistory")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsCenSaleDtlHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsCenSaleDtlHistory(BmsCenSaleDtlHistory entity);

        [ServiceContractDescription(Name = "修改平台供货明细历史表", Desc = "修改平台供货明细历史表", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDtlHistory", Get = "", Post = "BmsCenSaleDtlHistory", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDtlHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlHistory(BmsCenSaleDtlHistory entity);

        [ServiceContractDescription(Name = "修改平台供货明细历史表指定的属性", Desc = "修改平台供货明细历史表指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDtlHistoryByField", Get = "", Post = "BmsCenSaleDtlHistory", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDtlHistoryByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlHistoryByField(BmsCenSaleDtlHistory entity, string fields);

        [ServiceContractDescription(Name = "删除平台供货明细历史表", Desc = "删除平台供货明细历史表", Url = "ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDtlHistory?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBmsCenSaleDtlHistory?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBmsCenSaleDtlHistory(long id);

        [ServiceContractDescription(Name = "查询平台供货明细历史表", Desc = "查询平台供货明细历史表", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlHistory", Get = "", Post = "BmsCenSaleDtlHistory", Return = "BaseResultList<BmsCenSaleDtlHistory>", ReturnType = "ListBmsCenSaleDtlHistory")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDtlHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlHistory(BmsCenSaleDtlHistory entity);

        [ServiceContractDescription(Name = "查询平台供货明细历史表(HQL)", Desc = "查询平台供货明细历史表(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlHistoryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDtlHistory>", ReturnType = "ListBmsCenSaleDtlHistory")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDtlHistoryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台供货明细历史表", Desc = "通过主键ID查询平台供货明细历史表", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlHistoryById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDtlHistory>", ReturnType = "BmsCenSaleDtlHistory")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDtlHistoryById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlHistoryById(long id, string fields, bool isPlanish);
        #endregion

        #region CenQtyDtlTemp

        [ServiceContractDescription(Name = "新增平台临时库存表", Desc = "新增平台临时库存表", Url = "ReagentSysService.svc/ST_UDTO_AddCenQtyDtlTemp", Get = "", Post = "CenQtyDtlTemp", Return = "BaseResultDataValue", ReturnType = "CenQtyDtlTemp")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddCenQtyDtlTemp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddCenQtyDtlTemp(CenQtyDtlTemp entity);

        [ServiceContractDescription(Name = "修改平台临时库存表", Desc = "修改平台临时库存表", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenQtyDtlTemp", Get = "", Post = "CenQtyDtlTemp", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenQtyDtlTemp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenQtyDtlTemp(CenQtyDtlTemp entity);

        [ServiceContractDescription(Name = "修改平台临时库存表指定的属性", Desc = "修改平台临时库存表指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenQtyDtlTempByField", Get = "", Post = "CenQtyDtlTemp", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenQtyDtlTempByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenQtyDtlTempByField(CenQtyDtlTemp entity, string fields);

        [ServiceContractDescription(Name = "删除平台临时库存表", Desc = "删除平台临时库存表", Url = "ReagentSysService.svc/ST_UDTO_DelCenQtyDtlTemp?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelCenQtyDtlTemp?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelCenQtyDtlTemp(long id);

        [ServiceContractDescription(Name = "查询平台临时库存表", Desc = "查询平台临时库存表", Url = "ReagentSysService.svc/ST_UDTO_SearchCenQtyDtlTemp", Get = "", Post = "CenQtyDtlTemp", Return = "BaseResultList<CenQtyDtlTemp>", ReturnType = "ListCenQtyDtlTemp")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenQtyDtlTemp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenQtyDtlTemp(CenQtyDtlTemp entity);

        [ServiceContractDescription(Name = "查询平台临时库存表(HQL)", Desc = "查询平台临时库存表(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchCenQtyDtlTempByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenQtyDtlTemp>", ReturnType = "ListCenQtyDtlTemp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenQtyDtlTempByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台临时库存表", Desc = "通过主键ID查询平台临时库存表", Url = "ReagentSysService.svc/ST_UDTO_SearchCenQtyDtlTempById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenQtyDtlTemp>", ReturnType = "CenQtyDtlTemp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenQtyDtlTempById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempById(long id, string fields, bool isPlanish);
        #endregion

        #region CenQtyDtlTempHistory

        [ServiceContractDescription(Name = "新增平台历史库存历史表", Desc = "新增平台历史库存历史表", Url = "ReagentSysService.svc/ST_UDTO_AddCenQtyDtlTempHistory", Get = "", Post = "CenQtyDtlTempHistory", Return = "BaseResultDataValue", ReturnType = "CenQtyDtlTempHistory")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddCenQtyDtlTempHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddCenQtyDtlTempHistory(CenQtyDtlTempHistory entity);

        [ServiceContractDescription(Name = "修改平台历史库存历史表", Desc = "修改平台历史库存历史表", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenQtyDtlTempHistory", Get = "", Post = "CenQtyDtlTempHistory", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenQtyDtlTempHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenQtyDtlTempHistory(CenQtyDtlTempHistory entity);

        [ServiceContractDescription(Name = "修改平台历史库存历史表指定的属性", Desc = "修改平台历史库存历史表指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateCenQtyDtlTempHistoryByField", Get = "", Post = "CenQtyDtlTempHistory", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateCenQtyDtlTempHistoryByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateCenQtyDtlTempHistoryByField(CenQtyDtlTempHistory entity, string fields);

        [ServiceContractDescription(Name = "删除平台历史库存历史表", Desc = "删除平台历史库存历史表", Url = "ReagentSysService.svc/ST_UDTO_DelCenQtyDtlTempHistory?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelCenQtyDtlTempHistory?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelCenQtyDtlTempHistory(long id);

        [ServiceContractDescription(Name = "查询平台历史库存历史表", Desc = "查询平台历史库存历史表", Url = "ReagentSysService.svc/ST_UDTO_SearchCenQtyDtlTempHistory", Get = "", Post = "CenQtyDtlTempHistory", Return = "BaseResultList<CenQtyDtlTempHistory>", ReturnType = "ListCenQtyDtlTempHistory")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenQtyDtlTempHistory", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempHistory(CenQtyDtlTempHistory entity);

        [ServiceContractDescription(Name = "查询平台历史库存历史表(HQL)", Desc = "查询平台历史库存历史表(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchCenQtyDtlTempHistoryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenQtyDtlTempHistory>", ReturnType = "ListCenQtyDtlTempHistory")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenQtyDtlTempHistoryByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempHistoryByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询平台历史库存历史表", Desc = "通过主键ID查询平台历史库存历史表", Url = "ReagentSysService.svc/ST_UDTO_SearchCenQtyDtlTempHistoryById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<CenQtyDtlTempHistory>", ReturnType = "CenQtyDtlTempHistory")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchCenQtyDtlTempHistoryById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchCenQtyDtlTempHistoryById(long id, string fields, bool isPlanish);
        #endregion

        #region TestEquipLab

        [ServiceContractDescription(Name = "新增TestEquipLab", Desc = "新增TestEquipLab", Url = "ReagentSysService.svc/ST_UDTO_AddTestEquipLab", Get = "", Post = "TestEquipLab", Return = "BaseResultDataValue", ReturnType = "TestEquipLab")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddTestEquipLab", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddTestEquipLab(TestEquipLab entity);

        [ServiceContractDescription(Name = "修改TestEquipLab", Desc = "修改TestEquipLab", Url = "ReagentSysService.svc/ST_UDTO_UpdateTestEquipLab", Get = "", Post = "TestEquipLab", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateTestEquipLab", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateTestEquipLab(TestEquipLab entity);

        [ServiceContractDescription(Name = "修改TestEquipLab指定的属性", Desc = "修改TestEquipLab指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateTestEquipLabByField", Get = "", Post = "TestEquipLab", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateTestEquipLabByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateTestEquipLabByField(TestEquipLab entity, string fields);

        [ServiceContractDescription(Name = "删除TestEquipLab", Desc = "删除TestEquipLab", Url = "ReagentSysService.svc/ST_UDTO_DelTestEquipLab?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelTestEquipLab?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelTestEquipLab(long id);

        [ServiceContractDescription(Name = "查询TestEquipLab", Desc = "查询TestEquipLab", Url = "ReagentSysService.svc/ST_UDTO_SearchTestEquipLab", Get = "", Post = "TestEquipLab", Return = "BaseResultList<TestEquipLab>", ReturnType = "ListTestEquipLab")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchTestEquipLab", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchTestEquipLab(TestEquipLab entity);

        [ServiceContractDescription(Name = "查询TestEquipLab(HQL)", Desc = "查询TestEquipLab(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchTestEquipLabByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestEquipLab>", ReturnType = "ListTestEquipLab")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchTestEquipLabByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchTestEquipLabByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询TestEquipLab", Desc = "通过主键ID查询TestEquipLab", Url = "ReagentSysService.svc/ST_UDTO_SearchTestEquipLabById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestEquipLab>", ReturnType = "TestEquipLab")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchTestEquipLabById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchTestEquipLabById(long id, string fields, bool isPlanish);
        #endregion

        #region TestEquipProd

        [ServiceContractDescription(Name = "新增TestEquipProd", Desc = "新增TestEquipProd", Url = "ReagentSysService.svc/ST_UDTO_AddTestEquipProd", Get = "", Post = "TestEquipProd", Return = "BaseResultDataValue", ReturnType = "TestEquipProd")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddTestEquipProd", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddTestEquipProd(TestEquipProd entity);

        [ServiceContractDescription(Name = "修改TestEquipProd", Desc = "修改TestEquipProd", Url = "ReagentSysService.svc/ST_UDTO_UpdateTestEquipProd", Get = "", Post = "TestEquipProd", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateTestEquipProd", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateTestEquipProd(TestEquipProd entity);

        [ServiceContractDescription(Name = "修改TestEquipProd指定的属性", Desc = "修改TestEquipProd指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateTestEquipProdByField", Get = "", Post = "TestEquipProd", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateTestEquipProdByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateTestEquipProdByField(TestEquipProd entity, string fields);

        [ServiceContractDescription(Name = "删除TestEquipProd", Desc = "删除TestEquipProd", Url = "ReagentSysService.svc/ST_UDTO_DelTestEquipProd?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelTestEquipProd?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelTestEquipProd(long id);

        [ServiceContractDescription(Name = "查询TestEquipProd", Desc = "查询TestEquipProd", Url = "ReagentSysService.svc/ST_UDTO_SearchTestEquipProd", Get = "", Post = "TestEquipProd", Return = "BaseResultList<TestEquipProd>", ReturnType = "ListTestEquipProd")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchTestEquipProd", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchTestEquipProd(TestEquipProd entity);

        [ServiceContractDescription(Name = "查询TestEquipProd(HQL)", Desc = "查询TestEquipProd(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchTestEquipProdByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestEquipProd>", ReturnType = "ListTestEquipProd")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchTestEquipProdByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchTestEquipProdByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询TestEquipProd", Desc = "通过主键ID查询TestEquipProd", Url = "ReagentSysService.svc/ST_UDTO_SearchTestEquipProdById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestEquipProd>", ReturnType = "TestEquipProd")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchTestEquipProdById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchTestEquipProdById(long id, string fields, bool isPlanish);
        #endregion

        #region TestEquipType

        [ServiceContractDescription(Name = "新增TestEquipType", Desc = "新增TestEquipType", Url = "ReagentSysService.svc/ST_UDTO_AddTestEquipType", Get = "", Post = "TestEquipType", Return = "BaseResultDataValue", ReturnType = "TestEquipType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddTestEquipType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddTestEquipType(TestEquipType entity);

        [ServiceContractDescription(Name = "修改TestEquipType", Desc = "修改TestEquipType", Url = "ReagentSysService.svc/ST_UDTO_UpdateTestEquipType", Get = "", Post = "TestEquipType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateTestEquipType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateTestEquipType(TestEquipType entity);

        [ServiceContractDescription(Name = "修改TestEquipType指定的属性", Desc = "修改TestEquipType指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateTestEquipTypeByField", Get = "", Post = "TestEquipType", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateTestEquipTypeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateTestEquipTypeByField(TestEquipType entity, string fields);

        [ServiceContractDescription(Name = "删除TestEquipType", Desc = "删除TestEquipType", Url = "ReagentSysService.svc/ST_UDTO_DelTestEquipType?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelTestEquipType?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelTestEquipType(long id);

        [ServiceContractDescription(Name = "查询TestEquipType", Desc = "查询TestEquipType", Url = "ReagentSysService.svc/ST_UDTO_SearchTestEquipType", Get = "", Post = "TestEquipType", Return = "BaseResultList<TestEquipType>", ReturnType = "ListTestEquipType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchTestEquipType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchTestEquipType(TestEquipType entity);

        [ServiceContractDescription(Name = "查询TestEquipType(HQL)", Desc = "查询TestEquipType(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchTestEquipTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestEquipType>", ReturnType = "ListTestEquipType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchTestEquipTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchTestEquipTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询TestEquipType", Desc = "通过主键ID查询TestEquipType", Url = "ReagentSysService.svc/ST_UDTO_SearchTestEquipTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestEquipType>", ReturnType = "TestEquipType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchTestEquipTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchTestEquipTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region SCAttachment
        [ServiceContractDescription(Name = "上传公共附件服务", Desc = "上传公共附件服务", Url = "ReagentSysService.svc/SC_UploadAddSCAttachment", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UploadAddSCAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message SC_UploadAddSCAttachment();

        [ServiceContractDescription(Name = "下载公共附件服务", Desc = "下载公共附件服务", Url = "ReagentSysService.svc/SC_UDTO_DownLoadSCAttachment?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_DownLoadSCAttachment?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream SC_UDTO_DownLoadSCAttachment(long id, long operateType);
        [ServiceContractDescription(Name = "新增公共附件表", Desc = "新增公共附件表", Url = "ReagentSysService.svc/SC_UDTO_AddSCAttachment", Get = "", Post = "SCAttachment", Return = "BaseResultDataValue", ReturnType = "SCAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_AddSCAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_AddSCAttachment(SCAttachment entity);

        [ServiceContractDescription(Name = "修改公共附件表", Desc = "修改公共附件表", Url = "ReagentSysService.svc/SC_UDTO_UpdateSCAttachment", Get = "", Post = "SCAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCAttachment(SCAttachment entity);

        [ServiceContractDescription(Name = "修改公共附件表指定的属性", Desc = "修改公共附件表指定的属性", Url = "ReagentSysService.svc/SC_UDTO_UpdateSCAttachmentByField", Get = "", Post = "SCAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCAttachmentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCAttachmentByField(SCAttachment entity, string fields);

        [ServiceContractDescription(Name = "删除公共附件表", Desc = "删除公共附件表", Url = "ReagentSysService.svc/SC_UDTO_DelSCAttachment?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SC_UDTO_DelSCAttachment?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_DelSCAttachment(long id);

        [ServiceContractDescription(Name = "查询公共附件表", Desc = "查询公共附件表", Url = "ReagentSysService.svc/SC_UDTO_SearchSCAttachment", Get = "", Post = "SCAttachment", Return = "BaseResultList<SCAttachment>", ReturnType = "ListSCAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCAttachment(SCAttachment entity);

        [ServiceContractDescription(Name = "查询公共附件表(HQL)", Desc = "查询公共附件表(HQL)", Url = "ReagentSysService.svc/SC_UDTO_SearchSCAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCAttachment>", ReturnType = "ListSCAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共附件表", Desc = "通过主键ID查询公共附件表", Url = "ReagentSysService.svc/SC_UDTO_SearchSCAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCAttachment>", ReturnType = "SCAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCAttachmentById(long id, string fields, bool isPlanish);
        #endregion

        #region SCInteraction

        [ServiceContractDescription(Name = "新增程序交流表", Desc = "新增程序交流表", Url = "ReagentSysService.svc/SC_UDTO_AddSCInteraction", Get = "", Post = "SCInteraction", Return = "BaseResultDataValue", ReturnType = "SCInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_AddSCInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_AddSCInteraction(SCInteraction entity);

        [ServiceContractDescription(Name = "修改程序交流表", Desc = "修改程序交流表", Url = "ReagentSysService.svc/SC_UDTO_UpdateSCInteraction", Get = "", Post = "SCInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCInteraction(SCInteraction entity);

        [ServiceContractDescription(Name = "修改程序交流表指定的属性", Desc = "修改程序交流表指定的属性", Url = "ReagentSysService.svc/SC_UDTO_UpdateSCInteractionByField", Get = "", Post = "SCInteraction", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCInteractionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCInteractionByField(SCInteraction entity, string fields);

        [ServiceContractDescription(Name = "删除程序交流表", Desc = "删除程序交流表", Url = "ReagentSysService.svc/SC_UDTO_DelSCInteraction?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SC_UDTO_DelSCInteraction?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_DelSCInteraction(long id);

        [ServiceContractDescription(Name = "查询程序交流表", Desc = "查询程序交流表", Url = "ReagentSysService.svc/SC_UDTO_SearchSCInteraction", Get = "", Post = "SCInteraction", Return = "BaseResultList<SCInteraction>", ReturnType = "ListSCInteraction")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCInteraction", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCInteraction(SCInteraction entity);

        [ServiceContractDescription(Name = "查询程序交流表(HQL)", Desc = "查询程序交流表(HQL)", Url = "ReagentSysService.svc/SC_UDTO_SearchSCInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCInteraction>", ReturnType = "ListSCInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCInteractionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询程序交流表", Desc = "通过主键ID查询程序交流表", Url = "ReagentSysService.svc/SC_UDTO_SearchSCInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCInteraction>", ReturnType = "SCInteraction")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCInteractionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCInteractionById(long id, string fields, bool isPlanish);
        #endregion

        #region SCOperation

        [ServiceContractDescription(Name = "新增公共操作记录表", Desc = "新增公共操作记录表", Url = "ReagentSysService.svc/SC_UDTO_AddSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultDataValue", ReturnType = "SCOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_AddSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_AddSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "修改公共操作记录表", Desc = "修改公共操作记录表", Url = "ReagentSysService.svc/SC_UDTO_UpdateSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "修改公共操作记录表指定的属性", Desc = "修改公共操作记录表指定的属性", Url = "ReagentSysService.svc/SC_UDTO_UpdateSCOperationByField", Get = "", Post = "SCOperation", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_UpdateSCOperationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_UpdateSCOperationByField(SCOperation entity, string fields);

        [ServiceContractDescription(Name = "删除公共操作记录表", Desc = "删除公共操作记录表", Url = "ReagentSysService.svc/SC_UDTO_DelSCOperation?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SC_UDTO_DelSCOperation?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool SC_UDTO_DelSCOperation(long id);

        [ServiceContractDescription(Name = "查询公共操作记录表", Desc = "查询公共操作记录表", Url = "ReagentSysService.svc/SC_UDTO_SearchSCOperation", Get = "", Post = "SCOperation", Return = "BaseResultList<SCOperation>", ReturnType = "ListSCOperation")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCOperation", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCOperation(SCOperation entity);

        [ServiceContractDescription(Name = "查询公共操作记录表(HQL)", Desc = "查询公共操作记录表(HQL)", Url = "ReagentSysService.svc/SC_UDTO_SearchSCOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCOperation>", ReturnType = "ListSCOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCOperationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询公共操作记录表", Desc = "通过主键ID查询公共操作记录表", Url = "ReagentSysService.svc/SC_UDTO_SearchSCOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SCOperation>", ReturnType = "SCOperation")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SC_UDTO_SearchSCOperationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SC_UDTO_SearchSCOperationById(long id, string fields, bool isPlanish);
        #endregion        

        #region 获取程序内部字典
        [ServiceContractDescription(Name = "获取枚举字典", Desc = "获取枚举字典", Url = "ReagentSysService.svc/GetEnumDic?enumname={enumname}", Get = "enumname={enumname}", Post = "", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetEnumDic?enumname={enumname}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetEnumDic(string enumname);

        [ServiceContractDescription(Name = "获取类字典", Desc = "获取类字典", Url = "ReagentSysService.svc/GetClassDic?classname={classname}&classnamespace={classnamespace}", Get = "classname={classname}&classnamespace={classnamespace}", Post = "", Return = "BaseResultDataValue", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetClassDic?classname={classname}&classnamespace={classnamespace}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetClassDic(string classname, string classnamespace);

        [ServiceContractDescription(Name = "获取类字典列表", Desc = "获取类字典列表", Url = "ReagentSysService.svc/GetClassDicList", Get = "", Post = "ClassDicSearchPara", Return = "BaseResultDataValue", ReturnType = "ClassDicList")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetClassDicList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetClassDicList(ClassDicSearchPara[] jsonpara);

        #endregion

        #region BmsAccountInput
        [ServiceContractDescription(Name = "新增入帐总单及入帐明细,同时更新供货单的是否入帐标志", Desc = "新增入帐总单及入帐明细,同时更新供货单的是否入帐标志", Url = "ReagentSysService.svc/ST_UDTO_AddBmsAccountInputAndDtList", Get = "", Post = "BmsAccountInput", Return = "BaseResultDataValue", ReturnType = "BmsAccountInput")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsAccountInputAndDtList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsAccountInputAndDtList(BmsAccountInput entity, string saleDocIDStr);

        [ServiceContractDescription(Name = "删除入帐总单及入帐明细,同时更新供货单的是否入帐标志", Desc = "删除入帐总单及入帐明细,同时更新供货单的是否入帐标志", Url = "ReagentSysService.svc/ST_UDTO_DeleteBmsAccountInputAndDtList?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DeleteBmsAccountInputAndDtList?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DeleteBmsAccountInputAndDtList(long id);

        [ServiceContractDescription(Name = "新增入账管理", Desc = "新增入账管理", Url = "ReagentSysService.svc/ST_UDTO_AddBmsAccountInput", Get = "", Post = "BmsAccountInput", Return = "BaseResultDataValue", ReturnType = "BmsAccountInput")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsAccountInput", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsAccountInput(BmsAccountInput entity);

        [ServiceContractDescription(Name = "修改入账管理", Desc = "修改入账管理", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsAccountInput", Get = "", Post = "BmsAccountInput", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsAccountInput", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsAccountInput(BmsAccountInput entity);

        [ServiceContractDescription(Name = "修改入账管理指定的属性", Desc = "修改入账管理指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsAccountInputByField", Get = "", Post = "BmsAccountInput", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsAccountInputByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsAccountInputByField(BmsAccountInput entity, string fields);

        [ServiceContractDescription(Name = "删除入账管理", Desc = "删除入账管理", Url = "ReagentSysService.svc/ST_UDTO_DelBmsAccountInput?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBmsAccountInput?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBmsAccountInput(long id);

        [ServiceContractDescription(Name = "查询入账管理", Desc = "查询入账管理", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsAccountInput", Get = "", Post = "BmsAccountInput", Return = "BaseResultList<BmsAccountInput>", ReturnType = "ListBmsAccountInput")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsAccountInput", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsAccountInput(BmsAccountInput entity);

        [ServiceContractDescription(Name = "查询入账管理(HQL)", Desc = "查询入账管理(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsAccountInputByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsAccountInput>", ReturnType = "ListBmsAccountInput")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsAccountInputByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsAccountInputByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询入账管理", Desc = "通过主键ID查询入账管理", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsAccountInputById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsAccountInput>", ReturnType = "BmsAccountInput")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsAccountInputById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsAccountInputById(long id, string fields, bool isPlanish);
        #endregion

        #region BmsAccountSaleDoc

        [ServiceContractDescription(Name = "新增入账管理关系", Desc = "新增入账管理关系", Url = "ReagentSysService.svc/ST_UDTO_AddBmsAccountSaleDoc", Get = "", Post = "BmsAccountSaleDoc", Return = "BaseResultDataValue", ReturnType = "BmsAccountSaleDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsAccountSaleDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsAccountSaleDoc(BmsAccountSaleDoc entity);

        [ServiceContractDescription(Name = "修改入账管理关系", Desc = "修改入账管理关系", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsAccountSaleDoc", Get = "", Post = "BmsAccountSaleDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsAccountSaleDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsAccountSaleDoc(BmsAccountSaleDoc entity);

        [ServiceContractDescription(Name = "修改入账管理关系指定的属性", Desc = "修改入账管理关系指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsAccountSaleDocByField", Get = "", Post = "BmsAccountSaleDoc", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsAccountSaleDocByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsAccountSaleDocByField(BmsAccountSaleDoc entity, string fields);

        [ServiceContractDescription(Name = "删除入账管理关系", Desc = "删除入账管理关系", Url = "ReagentSysService.svc/ST_UDTO_DelBmsAccountSaleDoc?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBmsAccountSaleDoc?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBmsAccountSaleDoc(long id);

        [ServiceContractDescription(Name = "查询入账管理关系", Desc = "查询入账管理关系", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsAccountSaleDoc", Get = "", Post = "BmsAccountSaleDoc", Return = "BaseResultList<BmsAccountSaleDoc>", ReturnType = "ListBmsAccountSaleDoc")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsAccountSaleDoc", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsAccountSaleDoc(BmsAccountSaleDoc entity);

        [ServiceContractDescription(Name = "查询入账管理关系(HQL)", Desc = "查询入账管理关系(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsAccountSaleDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsAccountSaleDoc>", ReturnType = "ListBmsAccountSaleDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsAccountSaleDocByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsAccountSaleDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询入账管理关系", Desc = "通过主键ID查询入账管理关系", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsAccountSaleDocById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsAccountSaleDoc>", ReturnType = "BmsAccountSaleDoc")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsAccountSaleDocById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsAccountSaleDocById(long id, string fields, bool isPlanish);
        #endregion

        #region GoodsQualification
        [ServiceContractDescription(Name = "通过FormData方式新增资质证件信息", Desc = "通过FormData方式新增资质证件信息", Url = "ReagentSysService.svc/ST_UDTO_AddGoodsQualificationAndUploadRegisterFile", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddGoodsQualificationAndUploadRegisterFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_AddGoodsQualificationAndUploadRegisterFile();

        [ServiceContractDescription(Name = "通过FormData方式更新资质证件信息", Desc = "通过FormData方式更新资质证件信息", Url = "ReagentSysService.svc/ST_UDTO_UpdateGoodsQualificationAndUploadRegisterFileByField", Get = "", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGoodsQualificationAndUploadRegisterFileByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_UpdateGoodsQualificationAndUploadRegisterFileByField();

        [ServiceContractDescription(Name = "预览资质证件文件", Desc = "预览资质证件文件", Url = "ReagentSysService.svc/ST_UDTO_GoodsQualificationPreviewPdf?id={id}&operateType={operateType}", Get = "id={id}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_GoodsQualificationPreviewPdf?id={id}&operateType={operateType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Stream ST_UDTO_GoodsQualificationPreviewPdf(long id, long operateType);

        [ServiceContractDescription(Name = "新增资质证件表", Desc = "新增资质证件表", Url = "ReagentSysService.svc/ST_UDTO_AddGoodsQualification", Get = "", Post = "GoodsQualification", Return = "BaseResultDataValue", ReturnType = "GoodsQualification")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddGoodsQualification", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddGoodsQualification(GoodsQualification entity);

        [ServiceContractDescription(Name = "修改资质证件表", Desc = "修改资质证件表", Url = "ReagentSysService.svc/ST_UDTO_UpdateGoodsQualification", Get = "", Post = "GoodsQualification", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGoodsQualification", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGoodsQualification(GoodsQualification entity);

        [ServiceContractDescription(Name = "修改资质证件表指定的属性", Desc = "修改资质证件表指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateGoodsQualificationByField", Get = "", Post = "GoodsQualification", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateGoodsQualificationByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateGoodsQualificationByField(GoodsQualification entity, string fields);

        [ServiceContractDescription(Name = "删除资质证件表", Desc = "删除资质证件表", Url = "ReagentSysService.svc/ST_UDTO_DelGoodsQualification?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelGoodsQualification?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelGoodsQualification(long id);

        [ServiceContractDescription(Name = "查询资质证件表", Desc = "查询资质证件表", Url = "ReagentSysService.svc/ST_UDTO_SearchGoodsQualification", Get = "", Post = "GoodsQualification", Return = "BaseResultList<GoodsQualification>", ReturnType = "ListGoodsQualification")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGoodsQualification", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGoodsQualification(GoodsQualification entity);

        [ServiceContractDescription(Name = "查询资质证件表(HQL)", Desc = "查询资质证件表(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchGoodsQualificationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GoodsQualification>", ReturnType = "ListGoodsQualification")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGoodsQualificationByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGoodsQualificationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询资质证件表", Desc = "通过主键ID查询资质证件表", Url = "ReagentSysService.svc/ST_UDTO_SearchGoodsQualificationById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GoodsQualification>", ReturnType = "GoodsQualification")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchGoodsQualificationById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchGoodsQualificationById(long id, string fields, bool isPlanish);
        #endregion

        #region BmsCenSaleDocConfirm

        [ServiceContractDescription(Name = "新增供货验收单表", Desc = "新增供货验收单表", Url = "ReagentSysService.svc/ST_UDTO_AddBmsCenSaleDocConfirm", Get = "", Post = "BmsCenSaleDocConfirm", Return = "BaseResultDataValue", ReturnType = "BmsCenSaleDocConfirm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsCenSaleDocConfirm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsCenSaleDocConfirm(BmsCenSaleDocConfirm entity);

        [ServiceContractDescription(Name = "修改供货验收单表", Desc = "修改供货验收单表", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDocConfirm", Get = "", Post = "BmsCenSaleDocConfirm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDocConfirm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDocConfirm(BmsCenSaleDocConfirm entity);

        [ServiceContractDescription(Name = "修改供货验收单表指定的属性", Desc = "修改供货验收单表指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDocConfirmByField", Get = "", Post = "BmsCenSaleDocConfirm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDocConfirmByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDocConfirmByField(BmsCenSaleDocConfirm entity, string fields);

        [ServiceContractDescription(Name = "删除供货验收单表", Desc = "删除供货验收单表", Url = "ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDocConfirm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBmsCenSaleDocConfirm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBmsCenSaleDocConfirm(long id);

        [ServiceContractDescription(Name = "查询供货验收单表", Desc = "查询供货验收单表", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocConfirm", Get = "", Post = "BmsCenSaleDocConfirm", Return = "BaseResultList<BmsCenSaleDocConfirm>", ReturnType = "ListBmsCenSaleDocConfirm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDocConfirm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocConfirm(BmsCenSaleDocConfirm entity);

        [ServiceContractDescription(Name = "查询供货验收单表(HQL)", Desc = "查询供货验收单表(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocConfirmByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDocConfirm>", ReturnType = "ListBmsCenSaleDocConfirm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDocConfirmByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocConfirmByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询供货验收单表", Desc = "通过主键ID查询供货验收单表", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDocConfirmById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDocConfirm>", ReturnType = "BmsCenSaleDocConfirm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDocConfirmById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDocConfirmById(long id, string fields, bool isPlanish);
        #endregion

        #region BmsCenSaleDtlConfirm

        [ServiceContractDescription(Name = "新增供货验收明细单表", Desc = "新增供货验收明细单表", Url = "ReagentSysService.svc/ST_UDTO_AddBmsCenSaleDtlConfirm", Get = "", Post = "BmsCenSaleDtlConfirm", Return = "BaseResultDataValue", ReturnType = "BmsCenSaleDtlConfirm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddBmsCenSaleDtlConfirm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddBmsCenSaleDtlConfirm(BmsCenSaleDtlConfirm entity);

        [ServiceContractDescription(Name = "修改供货验收明细单表", Desc = "修改供货验收明细单表", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDtlConfirm", Get = "", Post = "BmsCenSaleDtlConfirm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDtlConfirm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlConfirm(BmsCenSaleDtlConfirm entity);

        [ServiceContractDescription(Name = "修改供货验收明细单表指定的属性", Desc = "修改供货验收明细单表指定的属性", Url = "ReagentSysService.svc/ST_UDTO_UpdateBmsCenSaleDtlConfirmByField", Get = "", Post = "BmsCenSaleDtlConfirm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateBmsCenSaleDtlConfirmByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateBmsCenSaleDtlConfirmByField(BmsCenSaleDtlConfirm entity, string fields);

        [ServiceContractDescription(Name = "删除供货验收明细单表", Desc = "删除供货验收明细单表", Url = "ReagentSysService.svc/ST_UDTO_DelBmsCenSaleDtlConfirm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelBmsCenSaleDtlConfirm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelBmsCenSaleDtlConfirm(long id);

        [ServiceContractDescription(Name = "查询供货验收明细单表", Desc = "查询供货验收明细单表", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlConfirm", Get = "", Post = "BmsCenSaleDtlConfirm", Return = "BaseResultList<BmsCenSaleDtlConfirm>", ReturnType = "ListBmsCenSaleDtlConfirm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDtlConfirm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlConfirm(BmsCenSaleDtlConfirm entity);

        [ServiceContractDescription(Name = "查询供货验收明细单表(HQL)", Desc = "查询供货验收明细单表(HQL)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlConfirmByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDtlConfirm>", ReturnType = "ListBmsCenSaleDtlConfirm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDtlConfirmByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlConfirmByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询供货验收明细单表", Desc = "通过主键ID查询供货验收明细单表", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlConfirmById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDtlConfirm>", ReturnType = "BmsCenSaleDtlConfirm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDtlConfirmById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlConfirmById(long id, string fields, bool isPlanish);
        #endregion

        #region 供货单多批次验收

        [ServiceContractDescription(Name = "获取某一供货单的明细(包括原始明细及合并后的明细)", Desc = "获取某一供货单的明细(包括原始明细及合并后的明细)", Url = "ReagentSysService.svc/ST_UDTO_SearchBmsCenSaleDtlForCheckByBmsCenSaleDocId?page={page}&limit={limit}&fields={fields}&bmsCenSaleDocId={bmsCenSaleDocId}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&bmsCenSaleDocId={bmsCenSaleDocId}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDtl>", ReturnType = "ListBmsCenSaleDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchBmsCenSaleDtlForCheckByBmsCenSaleDocId?page={page}&limit={limit}&fields={fields}&bmsCenSaleDocId={bmsCenSaleDocId}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchBmsCenSaleDtlForCheckByBmsCenSaleDocId(int page, int limit, string fields, long bmsCenSaleDocId, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "获取某一供货单的(同一种试剂)合并处理后的明细", Desc = "获取某一供货单的(同一种试剂)合并处理后的明细", Url = "ReagentSysService.svc/ST_UDTO_SearchMergerDtListForCheckByBmsCenSaleDocId?page={page}&limit={limit}&fields={fields}&bmsCenSaleDocId={bmsCenSaleDocId}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&bmsCenSaleDocId={bmsCenSaleDocId}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BmsCenSaleDtl>", ReturnType = "ListBmsCenSaleDtl")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchMergerDtListForCheckByBmsCenSaleDocId?page={page}&limit={limit}&fields={fields}&bmsCenSaleDocId={bmsCenSaleDocId}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchMergerDtListForCheckByBmsCenSaleDocId(int page, int limit, string fields, long bmsCenSaleDocId, string sort, bool isPlanish);
        #endregion

        
    }
}
