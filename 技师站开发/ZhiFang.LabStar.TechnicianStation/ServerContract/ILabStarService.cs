using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation
{
    [ServiceContract]
    public interface ILabStarService
    {
        #region LisBarCodeForm

        [ServiceContractDescription(Name = "新增Lis_BarCodeForm", Desc = "新增Lis_BarCodeForm", Url = "LabStarService.svc/LS_UDTO_AddLisBarCodeForm", Get = "", Post = "LisBarCodeForm", Return = "BaseResultDataValue", ReturnType = "LisBarCodeForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisBarCodeForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisBarCodeForm(LisBarCodeForm entity);

        [ServiceContractDescription(Name = "修改Lis_BarCodeForm", Desc = "修改Lis_BarCodeForm", Url = "LabStarService.svc/LS_UDTO_UpdateLisBarCodeForm", Get = "", Post = "LisBarCodeForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisBarCodeForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisBarCodeForm(LisBarCodeForm entity);

        [ServiceContractDescription(Name = "修改Lis_BarCodeForm指定的属性", Desc = "修改Lis_BarCodeForm指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisBarCodeFormByField", Get = "", Post = "LisBarCodeForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisBarCodeFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisBarCodeFormByField(LisBarCodeForm entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_BarCodeForm", Desc = "删除Lis_BarCodeForm", Url = "LabStarService.svc/LS_UDTO_DelLisBarCodeForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisBarCodeForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisBarCodeForm(long id);

        [ServiceContractDescription(Name = "查询Lis_BarCodeForm", Desc = "查询Lis_BarCodeForm", Url = "LabStarService.svc/LS_UDTO_SearchLisBarCodeForm", Get = "", Post = "LisBarCodeForm", Return = "BaseResultList<LisBarCodeForm>", ReturnType = "ListLisBarCodeForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisBarCodeForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisBarCodeForm(LisBarCodeForm entity);

        [ServiceContractDescription(Name = "查询Lis_BarCodeForm(HQL)", Desc = "查询Lis_BarCodeForm(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisBarCodeFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisBarCodeForm>", ReturnType = "ListLisBarCodeForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisBarCodeFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisBarCodeFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_BarCodeForm", Desc = "通过主键ID查询Lis_BarCodeForm", Url = "LabStarService.svc/LS_UDTO_SearchLisBarCodeFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisBarCodeForm>", ReturnType = "LisBarCodeForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisBarCodeFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisBarCodeFormById(long id, string fields, bool isPlanish);
        #endregion

        #region LisBarCodeItem

        [ServiceContractDescription(Name = "新增Lis_BarCodeItem", Desc = "新增Lis_BarCodeItem", Url = "LabStarService.svc/LS_UDTO_AddLisBarCodeItem", Get = "", Post = "LisBarCodeItem", Return = "BaseResultDataValue", ReturnType = "LisBarCodeItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisBarCodeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisBarCodeItem(LisBarCodeItem entity);

        [ServiceContractDescription(Name = "修改Lis_BarCodeItem", Desc = "修改Lis_BarCodeItem", Url = "LabStarService.svc/LS_UDTO_UpdateLisBarCodeItem", Get = "", Post = "LisBarCodeItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisBarCodeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisBarCodeItem(LisBarCodeItem entity);

        [ServiceContractDescription(Name = "修改Lis_BarCodeItem指定的属性", Desc = "修改Lis_BarCodeItem指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisBarCodeItemByField", Get = "", Post = "LisBarCodeItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisBarCodeItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisBarCodeItemByField(LisBarCodeItem entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_BarCodeItem", Desc = "删除Lis_BarCodeItem", Url = "LabStarService.svc/LS_UDTO_DelLisBarCodeItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisBarCodeItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisBarCodeItem(long id);

        [ServiceContractDescription(Name = "查询Lis_BarCodeItem", Desc = "查询Lis_BarCodeItem", Url = "LabStarService.svc/LS_UDTO_SearchLisBarCodeItem", Get = "", Post = "LisBarCodeItem", Return = "BaseResultList<LisBarCodeItem>", ReturnType = "ListLisBarCodeItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisBarCodeItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisBarCodeItem(LisBarCodeItem entity);

        [ServiceContractDescription(Name = "查询Lis_BarCodeItem(HQL)", Desc = "查询Lis_BarCodeItem(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisBarCodeItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisBarCodeItem>", ReturnType = "ListLisBarCodeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisBarCodeItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisBarCodeItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_BarCodeItem", Desc = "通过主键ID查询Lis_BarCodeItem", Url = "LabStarService.svc/LS_UDTO_SearchLisBarCodeItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisBarCodeItem>", ReturnType = "LisBarCodeItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisBarCodeItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisBarCodeItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LisEquipForm

        [ServiceContractDescription(Name = "新增Lis_EquipForm", Desc = "新增Lis_EquipForm", Url = "LabStarService.svc/LS_UDTO_AddLisEquipForm", Get = "", Post = "LisEquipForm", Return = "BaseResultDataValue", ReturnType = "LisEquipForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisEquipForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisEquipForm(LisEquipForm entity);

        [ServiceContractDescription(Name = "修改Lis_EquipForm", Desc = "修改Lis_EquipForm", Url = "LabStarService.svc/LS_UDTO_UpdateLisEquipForm", Get = "", Post = "LisEquipForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisEquipForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisEquipForm(LisEquipForm entity);

        [ServiceContractDescription(Name = "修改Lis_EquipForm指定的属性", Desc = "修改Lis_EquipForm指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisEquipFormByField", Get = "", Post = "LisEquipForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisEquipFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisEquipFormByField(LisEquipForm entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_EquipForm", Desc = "删除Lis_EquipForm", Url = "LabStarService.svc/LS_UDTO_DelLisEquipForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisEquipForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisEquipForm(long id);

        [ServiceContractDescription(Name = "查询Lis_EquipForm", Desc = "查询Lis_EquipForm", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipForm", Get = "", Post = "LisEquipForm", Return = "BaseResultList<LisEquipForm>", ReturnType = "ListLisEquipForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipForm(LisEquipForm entity);

        [ServiceContractDescription(Name = "查询Lis_EquipForm(HQL)", Desc = "查询Lis_EquipForm(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisEquipForm>", ReturnType = "ListLisEquipForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_EquipForm", Desc = "通过主键ID查询Lis_EquipForm", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisEquipForm>", ReturnType = "LisEquipForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipFormById(long id, string fields, bool isPlanish);
        #endregion

        #region LisEquipItem

        [ServiceContractDescription(Name = "新增Lis_EquipItem", Desc = "新增Lis_EquipItem", Url = "LabStarService.svc/LS_UDTO_AddLisEquipItem", Get = "", Post = "LisEquipItem", Return = "BaseResultDataValue", ReturnType = "LisEquipItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisEquipItem(LisEquipItem entity);

        [ServiceContractDescription(Name = "修改Lis_EquipItem", Desc = "修改Lis_EquipItem", Url = "LabStarService.svc/LS_UDTO_UpdateLisEquipItem", Get = "", Post = "LisEquipItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisEquipItem(LisEquipItem entity);

        [ServiceContractDescription(Name = "修改Lis_EquipItem指定的属性", Desc = "修改Lis_EquipItem指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisEquipItemByField", Get = "", Post = "LisEquipItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisEquipItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisEquipItemByField(LisEquipItem entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_EquipItem", Desc = "删除Lis_EquipItem", Url = "LabStarService.svc/LS_UDTO_DelLisEquipItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisEquipItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisEquipItem(long id);

        [ServiceContractDescription(Name = "查询Lis_EquipItem", Desc = "查询Lis_EquipItem", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipItem", Get = "", Post = "LisEquipItem", Return = "BaseResultList<LisEquipItem>", ReturnType = "ListLisEquipItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipItem(LisEquipItem entity);

        [ServiceContractDescription(Name = "查询Lis_EquipItem(HQL)", Desc = "查询Lis_EquipItem(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisEquipItem>", ReturnType = "ListLisEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_EquipItem", Desc = "通过主键ID查询Lis_EquipItem", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisEquipItem>", ReturnType = "LisEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LisOrderForm

        [ServiceContractDescription(Name = "新增Lis_OrderForm", Desc = "新增Lis_OrderForm", Url = "LabStarService.svc/LS_UDTO_AddLisOrderForm", Get = "", Post = "LisOrderForm", Return = "BaseResultDataValue", ReturnType = "LisOrderForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisOrderForm(LisOrderForm entity);

        [ServiceContractDescription(Name = "修改Lis_OrderForm", Desc = "修改Lis_OrderForm", Url = "LabStarService.svc/LS_UDTO_UpdateLisOrderForm", Get = "", Post = "LisOrderForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisOrderForm(LisOrderForm entity);

        [ServiceContractDescription(Name = "修改Lis_OrderForm指定的属性", Desc = "修改Lis_OrderForm指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisOrderFormByField", Get = "", Post = "LisOrderForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisOrderFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisOrderFormByField(LisOrderForm entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_OrderForm", Desc = "删除Lis_OrderForm", Url = "LabStarService.svc/LS_UDTO_DelLisOrderForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisOrderForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisOrderForm(long id);

        [ServiceContractDescription(Name = "查询Lis_OrderForm", Desc = "查询Lis_OrderForm", Url = "LabStarService.svc/LS_UDTO_SearchLisOrderForm", Get = "", Post = "LisOrderForm", Return = "BaseResultList<LisOrderForm>", ReturnType = "ListLisOrderForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOrderForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOrderForm(LisOrderForm entity);

        [ServiceContractDescription(Name = "查询Lis_OrderForm(HQL)", Desc = "查询Lis_OrderForm(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisOrderFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisOrderForm>", ReturnType = "ListLisOrderForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOrderFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOrderFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_OrderForm", Desc = "通过主键ID查询Lis_OrderForm", Url = "LabStarService.svc/LS_UDTO_SearchLisOrderFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisOrderForm>", ReturnType = "LisOrderForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOrderFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOrderFormById(long id, string fields, bool isPlanish);
        #endregion

        #region LisOrderItem

        [ServiceContractDescription(Name = "新增Lis_OrderItem", Desc = "新增Lis_OrderItem", Url = "LabStarService.svc/LS_UDTO_AddLisOrderItem", Get = "", Post = "LisOrderItem", Return = "BaseResultDataValue", ReturnType = "LisOrderItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisOrderItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisOrderItem(LisOrderItem entity);

        [ServiceContractDescription(Name = "修改Lis_OrderItem", Desc = "修改Lis_OrderItem", Url = "LabStarService.svc/LS_UDTO_UpdateLisOrderItem", Get = "", Post = "LisOrderItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisOrderItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisOrderItem(LisOrderItem entity);

        [ServiceContractDescription(Name = "修改Lis_OrderItem指定的属性", Desc = "修改Lis_OrderItem指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisOrderItemByField", Get = "", Post = "LisOrderItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisOrderItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisOrderItemByField(LisOrderItem entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_OrderItem", Desc = "删除Lis_OrderItem", Url = "LabStarService.svc/LS_UDTO_DelLisOrderItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisOrderItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisOrderItem(long id);

        [ServiceContractDescription(Name = "查询Lis_OrderItem", Desc = "查询Lis_OrderItem", Url = "LabStarService.svc/LS_UDTO_SearchLisOrderItem", Get = "", Post = "LisOrderItem", Return = "BaseResultList<LisOrderItem>", ReturnType = "ListLisOrderItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOrderItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOrderItem(LisOrderItem entity);

        [ServiceContractDescription(Name = "查询Lis_OrderItem(HQL)", Desc = "查询Lis_OrderItem(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisOrderItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisOrderItem>", ReturnType = "ListLisOrderItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOrderItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOrderItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_OrderItem", Desc = "通过主键ID查询Lis_OrderItem", Url = "LabStarService.svc/LS_UDTO_SearchLisOrderItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisOrderItem>", ReturnType = "LisOrderItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOrderItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOrderItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LisPatient

        [ServiceContractDescription(Name = "新增Lis_Patient", Desc = "新增Lis_Patient", Url = "LabStarService.svc/LS_UDTO_AddLisPatient", Get = "", Post = "LisPatient", Return = "BaseResultDataValue", ReturnType = "LisPatient")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisPatient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisPatient(LisPatient entity);

        [ServiceContractDescription(Name = "修改Lis_Patient", Desc = "修改Lis_Patient", Url = "LabStarService.svc/LS_UDTO_UpdateLisPatient", Get = "", Post = "LisPatient", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisPatient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisPatient(LisPatient entity);

        [ServiceContractDescription(Name = "修改Lis_Patient指定的属性", Desc = "修改Lis_Patient指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisPatientByField", Get = "", Post = "LisPatient", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisPatientByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisPatientByField(LisPatient entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_Patient", Desc = "删除Lis_Patient", Url = "LabStarService.svc/LS_UDTO_DelLisPatient?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisPatient?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisPatient(long id);

        [ServiceContractDescription(Name = "查询Lis_Patient", Desc = "查询Lis_Patient", Url = "LabStarService.svc/LS_UDTO_SearchLisPatient", Get = "", Post = "LisPatient", Return = "BaseResultList<LisPatient>", ReturnType = "ListLisPatient")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisPatient", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisPatient(LisPatient entity);

        [ServiceContractDescription(Name = "查询Lis_Patient(HQL)", Desc = "查询Lis_Patient(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisPatientByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisPatient>", ReturnType = "ListLisPatient")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisPatientByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisPatientByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_Patient", Desc = "通过主键ID查询Lis_Patient", Url = "LabStarService.svc/LS_UDTO_SearchLisPatientById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisPatient>", ReturnType = "LisPatient")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisPatientById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisPatientById(long id, string fields, bool isPlanish);
        #endregion

        #region LisTestForm

        [ServiceContractDescription(Name = "新增Lis_TestForm", Desc = "新增Lis_TestForm", Url = "LabStarService.svc/LS_UDTO_AddLisTestForm", Get = "", Post = "LisTestForm", Return = "BaseResultDataValue", ReturnType = "LisTestForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisTestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisTestForm(LisTestForm entity);

        [ServiceContractDescription(Name = "修改Lis_TestForm", Desc = "修改Lis_TestForm", Url = "LabStarService.svc/LS_UDTO_UpdateLisTestForm", Get = "", Post = "LisTestForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisTestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisTestForm(LisTestForm entity);

        [ServiceContractDescription(Name = "修改Lis_TestForm指定的属性", Desc = "修改Lis_TestForm指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisTestFormByField", Get = "", Post = "LisTestForm", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisTestFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisTestFormByField(LisTestForm entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_TestForm", Desc = "删除Lis_TestForm", Url = "LabStarService.svc/LS_UDTO_DelLisTestForm?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisTestForm?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisTestForm(long id);

        [ServiceContractDescription(Name = "查询Lis_TestForm", Desc = "查询Lis_TestForm", Url = "LabStarService.svc/LS_UDTO_SearchLisTestForm", Get = "", Post = "LisTestForm", Return = "BaseResultList<LisTestForm>", ReturnType = "ListLisTestForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestForm(LisTestForm entity);

        [ServiceContractDescription(Name = "查询Lis_TestForm(HQL)", Desc = "查询Lis_TestForm(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisTestFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestForm>", ReturnType = "ListLisTestForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_TestForm", Desc = "通过主键ID查询Lis_TestForm", Url = "LabStarService.svc/LS_UDTO_SearchLisTestFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestForm>", ReturnType = "LisTestForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestFormById(long id, string fields, bool isPlanish);
        #endregion

        #region LisTestItem

        [ServiceContractDescription(Name = "新增Lis_TestItem", Desc = "新增Lis_TestItem", Url = "LabStarService.svc/LS_UDTO_AddLisTestItem", Get = "", Post = "LisTestItem", Return = "BaseResultDataValue", ReturnType = "LisTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisTestItem(LisTestItem entity);

        [ServiceContractDescription(Name = "修改Lis_TestItem", Desc = "修改Lis_TestItem", Url = "LabStarService.svc/LS_UDTO_UpdateLisTestItem", Get = "", Post = "LisTestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisTestItem(LisTestItem entity);

        [ServiceContractDescription(Name = "修改Lis_TestItem指定的属性", Desc = "修改Lis_TestItem指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisTestItemByField", Get = "", Post = "LisTestItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisTestItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisTestItemByField(LisTestItem entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_TestItem", Desc = "删除Lis_TestItem", Url = "LabStarService.svc/LS_UDTO_DelLisTestItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisTestItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisTestItem(long id);

        [ServiceContractDescription(Name = "查询Lis_TestItem", Desc = "查询Lis_TestItem", Url = "LabStarService.svc/LS_UDTO_SearchLisTestItem", Get = "", Post = "LisTestItem", Return = "BaseResultList<LisTestItem>", ReturnType = "ListLisTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestItem(LisTestItem entity);

        [ServiceContractDescription(Name = "查询Lis_TestItem(HQL)", Desc = "查询Lis_TestItem(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestItem>", ReturnType = "ListLisTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_TestItem", Desc = "通过主键ID查询Lis_TestItem", Url = "LabStarService.svc/LS_UDTO_SearchLisTestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestItem>", ReturnType = "LisTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LisTestGraph

        [ServiceContractDescription(Name = "新增Lis_TestGraph", Desc = "新增Lis_TestGraph", Url = "LabStarService.svc/LS_UDTO_AddLisTestGraph", Get = "", Post = "LisTestGraph", Return = "BaseResultDataValue", ReturnType = "LisTestGraph")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisTestGraph", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisTestGraph(LisTestGraph entity);

        [ServiceContractDescription(Name = "修改Lis_TestGraph", Desc = "修改Lis_TestGraph", Url = "LabStarService.svc/LS_UDTO_UpdateLisTestGraph", Get = "", Post = "LisTestGraph", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisTestGraph", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisTestGraph(LisTestGraph entity);

        [ServiceContractDescription(Name = "修改Lis_TestGraph指定的属性", Desc = "修改Lis_TestGraph指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisTestGraphByField", Get = "", Post = "LisTestGraph", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisTestGraphByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisTestGraphByField(LisTestGraph entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_TestGraph", Desc = "删除Lis_TestGraph", Url = "LabStarService.svc/LS_UDTO_DelLisTestGraph?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisTestGraph?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisTestGraph(long id);

        [ServiceContractDescription(Name = "查询Lis_TestGraph", Desc = "查询Lis_TestGraph", Url = "LabStarService.svc/LS_UDTO_SearchLisTestGraph", Get = "", Post = "LisTestGraph", Return = "BaseResultList<LisTestGraph>", ReturnType = "ListLisTestGraph")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestGraph", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestGraph(LisTestGraph entity);

        [ServiceContractDescription(Name = "查询Lis_TestGraph(HQL)", Desc = "查询Lis_TestGraph(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisTestGraphByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestGraph>", ReturnType = "ListLisTestGraph")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestGraphByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestGraphByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_TestGraph", Desc = "通过主键ID查询Lis_TestGraph", Url = "LabStarService.svc/LS_UDTO_SearchLisTestGraphById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestGraph>", ReturnType = "LisTestGraph")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestGraphById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestGraphById(long id, string fields, bool isPlanish);
        #endregion

        #region LisOperateAuthorize

        [ServiceContractDescription(Name = "新增Lis_OperateAuthorize", Desc = "新增Lis_OperateAuthorize", Url = "LabStarService.svc/LS_UDTO_AddLisOperateAuthorize", Get = "", Post = "LisOperateAuthorize", Return = "BaseResultDataValue", ReturnType = "LisOperateAuthorize")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisOperateAuthorize", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisOperateAuthorize(LisOperateAuthorize entity);

        [ServiceContractDescription(Name = "修改Lis_OperateAuthorize", Desc = "修改Lis_OperateAuthorize", Url = "LabStarService.svc/LS_UDTO_UpdateLisOperateAuthorize", Get = "", Post = "LisOperateAuthorize", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisOperateAuthorize", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisOperateAuthorize(LisOperateAuthorize entity);

        [ServiceContractDescription(Name = "修改Lis_OperateAuthorize指定的属性", Desc = "修改Lis_OperateAuthorize指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisOperateAuthorizeByField", Get = "", Post = "LisOperateAuthorize", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisOperateAuthorizeByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisOperateAuthorizeByField(LisOperateAuthorize entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_OperateAuthorize", Desc = "删除Lis_OperateAuthorize", Url = "LabStarService.svc/LS_UDTO_DelLisOperateAuthorize?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisOperateAuthorize?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisOperateAuthorize(long id);

        [ServiceContractDescription(Name = "查询Lis_OperateAuthorize", Desc = "查询Lis_OperateAuthorize", Url = "LabStarService.svc/LS_UDTO_SearchLisOperateAuthorize", Get = "", Post = "LisOperateAuthorize", Return = "BaseResultList<LisOperateAuthorize>", ReturnType = "ListLisOperateAuthorize")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOperateAuthorize", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOperateAuthorize(LisOperateAuthorize entity);

        [ServiceContractDescription(Name = "查询Lis_OperateAuthorize(HQL)", Desc = "查询Lis_OperateAuthorize(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisOperateAuthorizeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisOperateAuthorize>", ReturnType = "ListLisOperateAuthorize")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOperateAuthorizeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOperateAuthorizeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_OperateAuthorize", Desc = "通过主键ID查询Lis_OperateAuthorize", Url = "LabStarService.svc/LS_UDTO_SearchLisOperateAuthorizeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisOperateAuthorize>", ReturnType = "LisOperateAuthorize")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOperateAuthorizeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOperateAuthorizeById(long id, string fields, bool isPlanish);
        #endregion

        #region LisOperateASection

        [ServiceContractDescription(Name = "新增Lis_OperateASection", Desc = "新增Lis_OperateASection", Url = "LabStarService.svc/LS_UDTO_AddLisOperateASection", Get = "", Post = "LisOperateASection", Return = "BaseResultDataValue", ReturnType = "LisOperateASection")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisOperateASection", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisOperateASection(LisOperateASection entity);

        [ServiceContractDescription(Name = "修改Lis_OperateASection", Desc = "修改Lis_OperateASection", Url = "LabStarService.svc/LS_UDTO_UpdateLisOperateASection", Get = "", Post = "LisOperateASection", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisOperateASection", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisOperateASection(LisOperateASection entity);

        [ServiceContractDescription(Name = "修改Lis_OperateASection指定的属性", Desc = "修改Lis_OperateASection指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisOperateASectionByField", Get = "", Post = "LisOperateASection", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisOperateASectionByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisOperateASectionByField(LisOperateASection entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_OperateASection", Desc = "删除Lis_OperateASection", Url = "LabStarService.svc/LS_UDTO_DelLisOperateASection?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisOperateASection?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisOperateASection(long id);

        [ServiceContractDescription(Name = "查询Lis_OperateASection", Desc = "查询Lis_OperateASection", Url = "LabStarService.svc/LS_UDTO_SearchLisOperateASection", Get = "", Post = "LisOperateASection", Return = "BaseResultList<LisOperateASection>", ReturnType = "ListLisOperateASection")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOperateASection", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOperateASection(LisOperateASection entity);

        [ServiceContractDescription(Name = "查询Lis_OperateASection(HQL)", Desc = "查询Lis_OperateASection(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisOperateASectionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisOperateASection>", ReturnType = "ListLisOperateASection")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOperateASectionByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOperateASectionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_OperateASection", Desc = "通过主键ID查询Lis_OperateASection", Url = "LabStarService.svc/LS_UDTO_SearchLisOperateASectionById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisOperateASection>", ReturnType = "LisOperateASection")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOperateASectionById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOperateASectionById(long id, string fields, bool isPlanish);
        #endregion

        #region LisOperate

        [ServiceContractDescription(Name = "新增Lis_Operate", Desc = "新增Lis_Operate", Url = "LabStarService.svc/LS_UDTO_AddLisOperate", Get = "", Post = "LisOperate", Return = "BaseResultDataValue", ReturnType = "LisOperate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisOperate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisOperate(LisOperate entity);

        [ServiceContractDescription(Name = "修改Lis_Operate", Desc = "修改Lis_Operate", Url = "LabStarService.svc/LS_UDTO_UpdateLisOperate", Get = "", Post = "LisOperate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisOperate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisOperate(LisOperate entity);

        [ServiceContractDescription(Name = "修改Lis_Operate指定的属性", Desc = "修改Lis_Operate指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisOperateByField", Get = "", Post = "LisOperate", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisOperateByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisOperateByField(LisOperate entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_Operate", Desc = "删除Lis_Operate", Url = "LabStarService.svc/LS_UDTO_DelLisOperate?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisOperate?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisOperate(long id);

        [ServiceContractDescription(Name = "查询Lis_Operate", Desc = "查询Lis_Operate", Url = "LabStarService.svc/LS_UDTO_SearchLisOperate", Get = "", Post = "LisOperate", Return = "BaseResultList<LisOperate>", ReturnType = "ListLisOperate")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOperate", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOperate(LisOperate entity);

        [ServiceContractDescription(Name = "查询Lis_Operate(HQL)", Desc = "查询Lis_Operate(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisOperateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisOperate>", ReturnType = "ListLisOperate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOperateByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOperateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_Operate", Desc = "通过主键ID查询Lis_Operate", Url = "LabStarService.svc/LS_UDTO_SearchLisOperateById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisOperate>", ReturnType = "LisOperate")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisOperateById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisOperateById(long id, string fields, bool isPlanish);
        #endregion

        #region LisEquipGraph

        [ServiceContractDescription(Name = "新增Lis_EquipGraph", Desc = "新增Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_AddLisEquipGraph", Get = "", Post = "LisEquipGraph", Return = "BaseResultDataValue", ReturnType = "LisEquipGraph")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisEquipGraph", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisEquipGraph(LisEquipGraph entity);

        [ServiceContractDescription(Name = "修改Lis_EquipGraph", Desc = "修改Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_UpdateLisEquipGraph", Get = "", Post = "LisEquipGraph", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisEquipGraph", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisEquipGraph(LisEquipGraph entity);

        [ServiceContractDescription(Name = "修改Lis_EquipGraph指定的属性", Desc = "修改Lis_EquipGraph指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisEquipGraphByField", Get = "", Post = "LisEquipGraph", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisEquipGraphByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisEquipGraphByField(LisEquipGraph entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_EquipGraph", Desc = "删除Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_DelLisEquipGraph?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisEquipGraph?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisEquipGraph(long id);

        [ServiceContractDescription(Name = "查询Lis_EquipGraph", Desc = "查询Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipGraph", Get = "", Post = "LisEquipGraph", Return = "BaseResultList<LisEquipGraph>", ReturnType = "ListLisEquipGraph")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipGraph", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipGraph(LisEquipGraph entity);

        [ServiceContractDescription(Name = "查询Lis_EquipGraph(HQL)", Desc = "查询Lis_EquipGraph(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipGraphByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisEquipGraph>", ReturnType = "ListLisEquipGraph")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipGraphByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipGraphByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_EquipGraph", Desc = "通过主键ID查询Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipGraphById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisEquipGraph>", ReturnType = "LisEquipGraph")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipGraphById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipGraphById(long id, string fields, bool isPlanish);
        #endregion

        #region LisEquipComFile

        [ServiceContractDescription(Name = "新增Lis_EquipGraph", Desc = "新增Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_AddLisEquipComFile", Get = "", Post = "LisEquipComFile", Return = "BaseResultDataValue", ReturnType = "LisEquipComFile")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisEquipComFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisEquipComFile(LisEquipComFile entity);

        [ServiceContractDescription(Name = "修改Lis_EquipGraph", Desc = "修改Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_UpdateLisEquipComFile", Get = "", Post = "LisEquipComFile", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisEquipComFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisEquipComFile(LisEquipComFile entity);

        [ServiceContractDescription(Name = "修改Lis_EquipGraph指定的属性", Desc = "修改Lis_EquipGraph指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisEquipComFileByField", Get = "", Post = "LisEquipComFile", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisEquipComFileByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisEquipComFileByField(LisEquipComFile entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_EquipGraph", Desc = "删除Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_DelLisEquipComFile?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisEquipComFile?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisEquipComFile(long id);

        [ServiceContractDescription(Name = "查询Lis_EquipGraph", Desc = "查询Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipComFile", Get = "", Post = "LisEquipComFile", Return = "BaseResultList<LisEquipComFile>", ReturnType = "ListLisEquipComFile")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipComFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipComFile(LisEquipComFile entity);

        [ServiceContractDescription(Name = "查询Lis_EquipGraph(HQL)", Desc = "查询Lis_EquipGraph(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipComFileByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisEquipComFile>", ReturnType = "ListLisEquipComFile")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipComFileByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipComFileByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_EquipGraph", Desc = "通过主键ID查询Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipComFileById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisEquipComFile>", ReturnType = "LisEquipComFile")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipComFileById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipComFileById(long id, string fields, bool isPlanish);
        #endregion

        #region LisEquipComLog

        [ServiceContractDescription(Name = "新增Lis_EquipGraph", Desc = "新增Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_AddLisEquipComLog", Get = "", Post = "LisEquipComLog", Return = "BaseResultDataValue", ReturnType = "LisEquipComLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisEquipComLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisEquipComLog(LisEquipComLog entity);

        [ServiceContractDescription(Name = "修改Lis_EquipGraph", Desc = "修改Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_UpdateLisEquipComLog", Get = "", Post = "LisEquipComLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisEquipComLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisEquipComLog(LisEquipComLog entity);

        [ServiceContractDescription(Name = "修改Lis_EquipGraph指定的属性", Desc = "修改Lis_EquipGraph指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisEquipComLogByField", Get = "", Post = "LisEquipComLog", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisEquipComLogByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisEquipComLogByField(LisEquipComLog entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_EquipGraph", Desc = "删除Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_DelLisEquipComLog?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisEquipComLog?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisEquipComLog(long id);

        [ServiceContractDescription(Name = "查询Lis_EquipGraph", Desc = "查询Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipComLog", Get = "", Post = "LisEquipComLog", Return = "BaseResultList<LisEquipComLog>", ReturnType = "ListLisEquipComLog")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipComLog", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipComLog(LisEquipComLog entity);

        [ServiceContractDescription(Name = "查询Lis_EquipGraph(HQL)", Desc = "查询Lis_EquipGraph(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipComLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisEquipComLog>", ReturnType = "ListLisEquipComLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipComLogByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipComLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_EquipGraph", Desc = "通过主键ID查询Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_SearchLisEquipComLogById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisEquipComLog>", ReturnType = "LisEquipComLog")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisEquipComLogById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisEquipComLogById(long id, string fields, bool isPlanish);
        #endregion

        #region LisTestFormMsg

        [ServiceContractDescription(Name = "新增Lis_Operate", Desc = "新增Lis_Operate", Url = "LabStarService.svc/LS_UDTO_AddLisTestFormMsg", Get = "", Post = "LisTestFormMsg", Return = "BaseResultDataValue", ReturnType = "LisTestFormMsg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisTestFormMsg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisTestFormMsg(LisTestFormMsg entity);

        [ServiceContractDescription(Name = "修改Lis_Operate", Desc = "修改Lis_Operate", Url = "LabStarService.svc/LS_UDTO_UpdateLisTestFormMsg", Get = "", Post = "LisTestFormMsg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisTestFormMsg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisTestFormMsg(LisTestFormMsg entity);

        [ServiceContractDescription(Name = "修改Lis_Operate指定的属性", Desc = "修改Lis_Operate指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisTestFormMsgByField", Get = "", Post = "LisTestFormMsg", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisTestFormMsgByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisTestFormMsgByField(LisTestFormMsg entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_Operate", Desc = "删除Lis_Operate", Url = "LabStarService.svc/LS_UDTO_DelLisTestFormMsg?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisTestFormMsg?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisTestFormMsg(long id);

        [ServiceContractDescription(Name = "查询Lis_Operate", Desc = "查询Lis_Operate", Url = "LabStarService.svc/LS_UDTO_SearchLisTestFormMsg", Get = "", Post = "LisTestFormMsg", Return = "BaseResultList<LisTestFormMsg>", ReturnType = "ListLisTestFormMsg")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestFormMsg", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestFormMsg(LisTestFormMsg entity);

        [ServiceContractDescription(Name = "查询Lis_Operate(HQL)", Desc = "查询Lis_Operate(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisTestFormMsgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestFormMsg>", ReturnType = "ListLisTestFormMsg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestFormMsgByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestFormMsgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_Operate", Desc = "通过主键ID查询Lis_Operate", Url = "LabStarService.svc/LS_UDTO_SearchLisTestFormMsgById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestFormMsg>", ReturnType = "LisTestFormMsg")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestFormMsgById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestFormMsgById(long id, string fields, bool isPlanish);
        #endregion

        #region LisTestFormMsgItem

        [ServiceContractDescription(Name = "新增Lis_EquipGraph", Desc = "新增Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_AddLisTestFormMsgItem", Get = "", Post = "LisTestFormMsgItem", Return = "BaseResultDataValue", ReturnType = "LisTestFormMsgItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisTestFormMsgItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisTestFormMsgItem(LisTestFormMsgItem entity);

        [ServiceContractDescription(Name = "修改Lis_EquipGraph", Desc = "修改Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_UpdateLisTestFormMsgItem", Get = "", Post = "LisTestFormMsgItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisTestFormMsgItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisTestFormMsgItem(LisTestFormMsgItem entity);

        [ServiceContractDescription(Name = "修改Lis_EquipGraph指定的属性", Desc = "修改Lis_EquipGraph指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisTestFormMsgItemByField", Get = "", Post = "LisTestFormMsgItem", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisTestFormMsgItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisTestFormMsgItemByField(LisTestFormMsgItem entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_EquipGraph", Desc = "删除Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_DelLisTestFormMsgItem?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisTestFormMsgItem?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisTestFormMsgItem(long id);

        [ServiceContractDescription(Name = "查询Lis_EquipGraph", Desc = "查询Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_SearchLisTestFormMsgItem", Get = "", Post = "LisTestFormMsgItem", Return = "BaseResultList<LisTestFormMsgItem>", ReturnType = "ListLisTestFormMsgItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestFormMsgItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestFormMsgItem(LisTestFormMsgItem entity);

        [ServiceContractDescription(Name = "查询Lis_EquipGraph(HQL)", Desc = "查询Lis_EquipGraph(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisTestFormMsgItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestFormMsgItem>", ReturnType = "ListLisTestFormMsgItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestFormMsgItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestFormMsgItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_EquipGraph", Desc = "通过主键ID查询Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_SearchLisTestFormMsgItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestFormMsgItem>", ReturnType = "LisTestFormMsgItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestFormMsgItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestFormMsgItemById(long id, string fields, bool isPlanish);
        #endregion

        #region LisTestItemReceive

        [ServiceContractDescription(Name = "新增Lis_EquipGraph", Desc = "新增Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_AddLisTestItemReceive", Get = "", Post = "LisTestItemReceive", Return = "BaseResultDataValue", ReturnType = "LisTestItemReceive")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisTestItemReceive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisTestItemReceive(LisTestItemReceive entity);

        [ServiceContractDescription(Name = "修改Lis_EquipGraph", Desc = "修改Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_UpdateLisTestItemReceive", Get = "", Post = "LisTestItemReceive", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisTestItemReceive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisTestItemReceive(LisTestItemReceive entity);

        [ServiceContractDescription(Name = "修改Lis_EquipGraph指定的属性", Desc = "修改Lis_EquipGraph指定的属性", Url = "LabStarService.svc/LS_UDTO_UpdateLisTestItemReceiveByField", Get = "", Post = "LisTestItemReceive", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisTestItemReceiveByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_UpdateLisTestItemReceiveByField(LisTestItemReceive entity, string fields);

        [ServiceContractDescription(Name = "删除Lis_EquipGraph", Desc = "删除Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_DelLisTestItemReceive?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisTestItemReceive?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisTestItemReceive(long id);

        [ServiceContractDescription(Name = "查询Lis_EquipGraph", Desc = "查询Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_SearchLisTestItemReceive", Get = "", Post = "LisTestItemReceive", Return = "BaseResultList<LisTestItemReceive>", ReturnType = "ListLisTestItemReceive")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestItemReceive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestItemReceive(LisTestItemReceive entity);

        [ServiceContractDescription(Name = "查询Lis_EquipGraph(HQL)", Desc = "查询Lis_EquipGraph(HQL)", Url = "LabStarService.svc/LS_UDTO_SearchLisTestItemReceiveByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestItemReceive>", ReturnType = "ListLisTestItemReceive")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestItemReceiveByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestItemReceiveByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Lis_EquipGraph", Desc = "通过主键ID查询Lis_EquipGraph", Url = "LabStarService.svc/LS_UDTO_SearchLisTestItemReceiveById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestItemReceive>", ReturnType = "LisTestItemReceive")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLisTestItemReceiveById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLisTestItemReceiveById(long id, string fields, bool isPlanish);
        #endregion

        #region 定制服务

        //[ServiceContractDescription(Name = "根据指定的样本号生成新样本号", Desc = "根据指定的样本号生成新样本号", Url = "LabStarService.svc/LS_UDTO_GetNewSampleNoByOldSampleNo?", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_GetNewSampleNoByOldSampleNo?sectionID={sectionID}&testDate={testDate}&curSampleNo={curSampleNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue LS_UDTO_GetNewSampleNoByOldSampleNo(long sectionID, string testDate, string curSampleNo);

        [ServiceContractDescription(Name = "根据指定的样本号生成新样本号", Desc = "根据指定的样本号生成新样本号", Url = "LabStarService.svc/LS_UDTO_GetNewSampleNoByOldSampleNo", Get = "", Post = "sectionID,testDate,curSampleNo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_GetNewSampleNoByOldSampleNo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_GetNewSampleNoByOldSampleNo(long sectionID, string testDate, string curSampleNo);

        [ServiceContractDescription(Name = "根据指定的样本号获取下一个顺序样本号", Desc = "根据指定的样本号获取下一个顺序样本号", Url = "LabStarService.svc/LS_UDTO_QueryNextSampleNoByCurSampleNo", Get = "", Post = "curSampleNo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryNextSampleNoByCurSampleNo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryNextSampleNoByCurSampleNo(string curSampleNo);

        [ServiceContractDescription(Name = "根据指定样本号批量生成新样本号", Desc = "根据指定样本号批量生成新样本号", Url = "LabStarService.svc/LS_UDTO_BatchCreateSampleNoByCurSampleNo", Get = "", Post = "curSampleNo,SampleNoCount", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_BatchCreateSampleNoByCurSampleNo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_BatchCreateSampleNoByCurSampleNo(string curSampleNo, int SampleNoCount);

        [ServiceContractDescription(Name = "新增样本单", Desc = "新增样本单,success为False时，Code返回值：1代表样本号已经存在；小于零代表必填项为空，直接显示ErrorInfo", Url = "LabStarService.svc/LS_UDTO_AddSingleLisTestForm", Get = "", Post = "testForm,listTestItem,isCreateSampleNo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddSingleLisTestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddSingleLisTestForm(LisTestForm testForm, IList<LisTestItem> listTestItem, bool isCreateSampleNo);

        [ServiceContractDescription(Name = "批量新增样本单", Desc = "批量新增样本单", Url = "LabStarService.svc/LS_UDTO_AddBatchLisTestForm", Get = "", Post = "sampleInfo,testDate,sectionID,startSampleNo,sampleCount", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddBatchLisTestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddBatchLisTestForm(string sampleInfo, string testDate, long sectionID, string startSampleNo, int sampleCount);

        [ServiceContractDescription(Name = "批量更新样本单", Desc = "批量更新样本单", Url = "LabStarService.svc/LS_UDTO_UpdateBatchLisTestForm", Get = "", Post = "entityList,fields", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateBatchLisTestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_UpdateBatchLisTestForm(IList<LisTestForm> entityList, string fields);

        [ServiceContractDescription(Name = "批量更新样本项目结果", Desc = "批量更新样本项目结果", Url = "LabStarService.svc/LS_UDTO_EditBatchLisTestItemResult", Get = "", Post = "testFormID, listTestItemResult", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_EditBatchLisTestItemResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_EditBatchLisTestItemResult(long testFormID, IList<LisTestItem> listTestItemResult);

        [ServiceContractDescription(Name = "编辑样本单", Desc = "编辑样本单", Url = "LabStarService.svc/LS_UDTO_EditLisTestFormByField", Get = "", Post = "testForm,testFormFields,patientFields", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_EditLisTestFormByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_EditLisTestFormByField(LisTestForm testForm, string testFormFields, string patientFields);

        [ServiceContractDescription(Name = "编辑样本单及其项目", Desc = "编辑样本单及其项目", Url = "LabStarService.svc/LS_UDTO_EditLisTestFormAndItemByField", Get = "", Post = "testForm,testFormFields,patientFields,listTestItemResult,testItemFileds", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_EditLisTestFormAndItemByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_EditLisTestFormAndItemByField(LisTestForm testForm, string testFormFields, string patientFields, IList<LisTestItem> listTestItemResult, string testItemFileds);

        [ServiceContractDescription(Name = "批量删除样本单", Desc = "批量删除样本单", Url = "LabStarService.svc/LS_UDTO_DeleteBatchLisTestForm", Get = "", Post = "delIDList,receiveDeleteFlag,resultDeleteFlag)", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_DeleteBatchLisTestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_DeleteBatchLisTestForm(string delIDList, int receiveDeleteFlag, int resultDeleteFlag);

        [ServiceContractDescription(Name = "查询样本单是否可以删除", Desc = "查询样本单是否可以删除", Url = "LabStarService.svc/LS_UDTO_QueryLisTestFormIsDelete", Get = "", Post = "delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLisTestFormIsDelete", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLisTestFormIsDelete(string delIDList);

        [ServiceContractDescription(Name = "批量新增样本单项目", Desc = "批量新增样本单项目", Url = "LabStarService.svc/LS_UDTO_AddBatchLisTestItem", Get = "", Post = "testFormID,listAddTestItem,testItemFileds,isRepPItem", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddBatchLisTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddBatchLisTestItem(string testFormID, IList<LisTestItem> listAddTestItem, string testItemFileds, bool isRepPItem);

        [ServiceContractDescription(Name = "批量新增样本单项目结果", Desc = "批量新增样本单项目结果", Url = "LabStarService.svc/LS_UDTO_AddBatchLisTestItemResult", Get = "", Post = "testFormID,listAddTestItem,isAddItem,isSingleItem", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddBatchLisTestItemResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddBatchLisTestItemResult(string testFormID, IList<LisTestItem> listAddTestItem, bool isAddItem, bool isSingleItem);

        [ServiceContractDescription(Name = "批量样本单项目结果偏移", Desc = "批量样本单项目结果偏移", Url = "LabStarService.svc/LS_UDTO_LisTestItemResultOffset", Get = "", Post = "testItemInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestItemResultOffset", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestItemResultOffset(string testItemInfo);

        [ServiceContractDescription(Name = "批量删除样本单项目", Desc = "批量删除样本单项目", Url = "LabStarService.svc/LS_UDTO_DeleteBatchLisTestItem", Get = "", Post = "testFormID,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_DeleteBatchLisTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_DeleteBatchLisTestItem(long testFormID, string delIDList);

        [ServiceContractDescription(Name = "批量删除样本单项目", Desc = "批量删除样本单项目", Url = "LabStarService.svc/LS_UDTO_DeleteBatchLisTestItemByTestFormIDList", Get = "", Post = "testFormIDList,itemIDList,isDelNoResultItem,isDelNoOrderItem", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_DeleteBatchLisTestItemByTestFormIDList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_DeleteBatchLisTestItemByTestFormIDList(string testFormIDList, string itemIDList, bool isDelNoResultItem, bool isDelNoOrderItem);

        [ServiceContractDescription(Name = "检验单计算项目判定与计算", Desc = "检验单计算项目判定与计算", Url = "LabStarService.svc/LS_UDTO_LisTestFormCalcItemDisposeByID?testFormID={testFormID}", Get = "testFormID={testFormID}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestFormCalcItemDisposeByID?testFormID={testFormID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestFormCalcItemDisposeByID(long testFormID);

        [ServiceContractDescription(Name = "查询Lis_TestForm(HQL)", Desc = "查询Lis_TestForm(HQL)", Url = "LabStarService.svc/LS_UDTO_QueryLisTestFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLisTestFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLisTestFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询Lis_TestForm定位分页(HQL)", Desc = "查询Lis_TestForm定位分页(HQL)", Url = "LabStarBaseTableService.svc/LS_UDTO_QueryLisTestFormCurPageByHQL?id={id}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "id={id}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLisTestFormCurPageByHQL?id={id}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLisTestFormCurPageByHQL(long id, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过样本号和其他条件查询Lis_TestForm定位分页", Desc = "通过样本号和其他条件查询Lis_TestForm定位分页", Url = "LabStarBaseTableService.svc/LS_UDTO_QueryLisTestFormCurPageBySampleNo?page={page}&limit={limit}&fields={fields}&where={where}}&oldWhere={oldWhere}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}}&oldWhere={oldWhere}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLisTestFormCurPageBySampleNo?page={page}&limit={limit}&fields={fields}&where={where}&oldWhere={oldWhere}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLisTestFormCurPageBySampleNo(int page, int limit, string fields, string where, string oldWhere, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询Lis_TestForm(HQL)", Desc = "查询Lis_TestForm(HQL)", Url = "LabStarService.svc/LS_UDTO_QueryLisTestFormBySampleNo?beginSampleNo={beginSampleNo}&endSampleNo={endSampleNo}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "beginSampleNo={beginSampleNo}&endSampleNo={endSampleNo}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLisTestFormBySampleNo?beginSampleNo={beginSampleNo}&endSampleNo={endSampleNo}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLisTestFormBySampleNo(string beginSampleNo, string endSampleNo, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询Lis_TestForm（检验项目）", Desc = "查询Lis_TestForm（检验项目）", Url = "LabStarService.svc/LS_UDTO_QueryLisTestFormAndItemNameList?beginSampleNo={beginSampleNo}&endSampleNo={endSampleNo}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isOrderItem={isOrderItem}&itemNameType={itemNameType}", Get = "beginSampleNo={beginSampleNo}&endSampleNo={endSampleNo}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isOrderItem={isOrderItem}&itemNameType={itemNameType}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLisTestFormAndItemNameList?beginSampleNo={beginSampleNo}&endSampleNo={endSampleNo}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isOrderItem={isOrderItem}&itemNameType={itemNameType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLisTestFormAndItemNameList(string beginSampleNo, string endSampleNo, int page, int limit, string fields, string where, string sort, bool isPlanish, int isOrderItem, int itemNameType);

        [ServiceContractDescription(Name = "查询要确认的Lis_TestForm", Desc = "查询要确认的Lis_TestForm", Url = "LabStarService.svc/LS_UDTO_QueryWillConfirmLisTestForm?beginSampleNo={beginSampleNo}&endSampleNo={endSampleNo}&itemResultFlag={itemResultFlag}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "beginSampleNo={beginSampleNo}&endSampleNo={endSampleNo}&itemResultFlag={itemResultFlag}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryWillConfirmLisTestForm?beginSampleNo={beginSampleNo}&endSampleNo={endSampleNo}&itemResultFlag={itemResultFlag}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryWillConfirmLisTestForm(string beginSampleNo, string endSampleNo, string itemResultFlag, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询Lis_TestItem(HQL)", Desc = "查询Lis_TestItem(HQL)", Url = "LabStarService.svc/LS_UDTO_QueryLisTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLisTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLisTestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询Lis_TestItem(HQL)", Desc = "查询Lis_TestItem(HQL)", Url = "LabStarService.svc/LS_UDTO_QueryLisTestItemBySampleNo?beginSampleNo={beginSampleNo}&endSampleNo={endSampleNo}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "beginSampleNo={beginSampleNo}&endSampleNo={endSampleNo}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLisTestItemBySampleNo?beginSampleNo={beginSampleNo}&endSampleNo={endSampleNo}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLisTestItemBySampleNo(string beginSampleNo, string endSampleNo, int page, int limit, string fields, string where, string sort, bool isPlanish);


        [ServiceContractDescription(Name = "检验样本合并", Desc = "检验样本合并", Url = "LabStarService.svc/LS_UDTO_LisTestFormInfoMerge", Get = "", Post = "fromTestFormID,toTestFormID,strFromTestItemID,mergeType,isSampleNoMerge,isSerialNoMerge,isDelFormTestItem,isDelFormTestForm", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestFormInfoMerge", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestFormInfoMerge(long fromTestFormID, LisTestForm toTestForm, string strFromTestItemID, int mergeType, bool isSampleNoMerge, bool isSerialNoMerge, bool isDelFormTestItem, bool isDelFormTestForm);

        [ServiceContractDescription(Name = "获取要合并的检验样本信息", Desc = "获取要合并的检验样本信息", Url = "LabStarService.svc/LS_UDTO_QueryItemMergeFormInfo", Get = "", Post = "itemID,beginDate,endDate,fields,isPlanish", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryItemMergeFormInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryItemMergeFormInfo(long itemID, string beginDate, string endDate, string fields, bool isPlanish);


        [ServiceContractDescription(Name = "获取要合并的检验样本项目信息", Desc = "获取要合并的检验样本项目信息", Url = "LabStarService.svc/LS_UDTO_QueryItemMergeInfo", Get = "", Post = "itemID,patNo,cName,beginDate,endDate,isMerge,fields,isPlanish", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryItemMergeInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryItemMergeInfo(long itemID, string patNo, string cName, string beginDate, string endDate, string isMerge, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "检验样本项目合并", Desc = "检验样本项目合并", Url = "LabStarService.svc/LS_UDTO_LisTestItemMerge", Get = "", Post = "toFormID,strLisTestItemID", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestItemMerge", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestItemMerge(long toFormID, string strLisTestItemID);

        [ServiceContractDescription(Name = "检验样本项目合并", Desc = "检验样本项目合并", Url = "LabStarService.svc/LS_UDTO_LisTestItemMergeByVOEntity", Get = "", Post = "listLBMergeItemVO", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestItemMergeByVOEntity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestItemMergeByVOEntity(IList<LBMergeItemVO> listLBMergeItemVO);

        [ServiceContractDescription(Name = "获取样本项目合并图形数据", Desc = "获取样本项目合并图形数据", Url = "LabStarService.svc/LS_UDTO_QueryLisTestItemMergeImageData", Get = "", Post = "toFormID,listLisTestItem", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLisTestItemMergeImageData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLisTestItemMergeImageData(long toFormID, IList<LisTestItem> listLisTestItem);

        [ServiceContractDescription(Name = "保存样本项目结果图形", Desc = "保存样本项目结果图形", Url = "LabStarService.svc/LS_UDTO_AddLisTestItemMergeGraph", Get = "", Post = "testFormID,graphInfo,graphName,graphType,graphHeight,graphWidth", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisTestItemMergeGraph", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message LS_UDTO_AddLisTestItemMergeGraph();

        [ServiceContractDescription(Name = "样本结果稀释处理", Desc = "样本结果稀释处理", Url = "LabStarService.svc/LS_UDTO_LisTestItemResultDilution", Get = "", Post = "testItemIDList,dilutionTimes", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestItemResultDilution", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestItemResultDilution(string testItemIDList, double? dilutionTimes);

        [ServiceContractDescription(Name = "获取多样本单共有项目列表", Desc = "获取多样本单共有项目列表", Url = "LabStarService.svc/LS_UDTO_QueryCommonItemByTestFormID", Get = "", Post = "testItemIDList,fields", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryCommonItemByTestFormID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryCommonItemByTestFormID(string testFormIDList, string fields);

        [ServiceContractDescription(Name = "检验结果图形表数据保存", Desc = "检验结果图形表数据保存", Url = "LabStarService.svc/LS_UDTO_AddLisTestGraphData", Get = "", Post = "testFormID,graphInfo,graphName,graphType,graphHeight,graphWidth", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisTestGraphData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message LS_UDTO_AddLisTestGraphData();

        [ServiceContractDescription(Name = "获取LIS图形库图形结果表数据", Desc = "获取LIS图形库图形结果表数据", Url = "LabStarService.svc/LS_UDTO_QueryLisGraphData?graphDataID={graphDataID}&graphSizeType={graphSizeType}", Get = "graphDataID={graphDataID}&graphSizeType={graphSizeType}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLisGraphData?graphDataID={graphDataID}&graphSizeType={graphSizeType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLisGraphData(long graphDataID, int graphSizeType);

        [ServiceContractDescription(Name = "删除Lis_TestGraph及图形表数据", Desc = "删除Lis_TestGraph及图形表数据", Url = "LabStarService.svc/LS_UDTO_DelLisTestGraphData?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/LS_UDTO_DelLisTestGraphData?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_DelLisTestGraphData(long id);

        [ServiceContractDescription(Name = "提取仪器样本单结果", Desc = "提取仪器样本单结果", Url = "LabStarService.svc/LS_UDTO_AddLisItemResultByEquipResult", Get = "", Post = "testFormID,equipFormID,equipItemID,isChangeSampleNo,isChangeTestFormID,isDelAuotAddItem", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddLisItemResultByEquipResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddLisItemResultByEquipResult(long testFormID, long equipFormID, string equipItemID, bool isChangeSampleNo, bool isChangeTestFormID, bool isDelAuotAddItem);

        [ServiceContractDescription(Name = "采用仪器结果", Desc = "采用仪器结果", Url = "LabStarService.svc/LS_UDTO_AddItemReCheckResultByEquipResult", Get = "", Post = "testFormID,equipFormID,equipItemID,reCheckMemoInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddItemReCheckResultByEquipResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddItemReCheckResultByEquipResult(long testFormID, long equipFormID, string equipItemID, string reCheckMemoInfo);

        [ServiceContractDescription(Name = "批量提取仪器样本单结果", Desc = "批量提取仪器样本单结果", Url = "LabStarService.svc/LS_UDTO_BatchExtractEquipResult", Get = "", Post = "testFormIDList,equipFormIDList,isChangeSampleNo,isDelAuotAddItem", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_BatchExtractEquipResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_BatchExtractEquipResult(string testFormIDList, string equipFormIDList, bool isChangeSampleNo, bool isDelAuotAddItem);

        /// <summary>
        /// 检验单智能审核判定服务
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="checkFlag">智能审核标志：1为智能审核成功</param>
        /// <param name="checkInfo">智能审核原因说明</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "样本单智能审核判定服务", Desc = "样本单智能审核判定服务", Url = "LabStarService.svc/LS_UDTO_LisTestFormIntellectCheck", Get = "", Post = "testFormID, checkFlag, checkInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestFormIntellectCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestFormIntellectCheck(long testFormID, int checkFlag, string checkInfo);

        /// <summary>
        /// 检验单检验确认服务
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="empID">确认人ID“</param>
        /// <param name="empName">确认人</param>
        /// <param name="memoInfo">备注</param>
        /// <param name="isCheckTestFormInfo">确认前是否检查检验单相关信息完整：true为检查</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "样本单检验确认服务", Desc = "样本单检验确认服务", Url = "LabStarService.svc/LS_UDTO_LisTestFormConfirm", Get = "", Post = "testFormID,empID,empName,memoInfo, isCheckTestFormInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestFormConfirm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestFormConfirm(long testFormID, long empID, string empName, string memoInfo, bool isCheckTestFormInfo);

        /// <summary>
        /// 样本单批量检验确认服务
        /// </summary>
        /// <param name="testFormIDList">样本单ID列表</param>
        /// <param name="empID">确认人ID“</param>
        /// <param name="empName">确认人</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "样本单检验确认服务", Desc = "样本单检验确认服务", Url = "LabStarService.svc/LS_UDTO_LisTestFormBatchConfirm", Get = "", Post = "testFormIDList,empID,empName,memoInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestFormBatchConfirm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestFormBatchConfirm(string testFormIDList, long empID, string empName, string memoInfo);

        /// <summary>
        /// 样本单取消检验确认服务
        /// </summary>
        /// <param name="testFormID">样本单ID</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "样本单取消检验确认服务", Desc = "样本单取消检验确认服务", Url = "LabStarService.svc/LS_UDTO_LisTestFormConfirmCancel", Get = "", Post = "testFormID,empID,empNamememoInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestFormConfirmCancel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestFormConfirmCancel(long testFormID, long empID, string empName, string memoInfo);

        /// <summary>
        /// 检验单复检服务
        /// </summary>
        /// <param name="testFormIDList">检验单ID</param>
        /// <param name="listReCheckTestItem">检验单复检项目</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "检验单复检服务", Desc = "检验单复检服务", Url = "LabStarService.svc/LS_UDTO_LisTestFormReCheck", Get = "", Post = "testFormID,listReCheckTestItem,memoInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestFormReCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestFormReCheck(long testFormID, IList<LisTestItem> listReCheckTestItem, string memoInfo);

        /// <summary>
        /// 检验单整单取消复检服务
        /// </summary>
        /// <param name="testFormIDList">检验单ID列表字符串</param>
        /// <param name="isClearRedoDesc">是否清除复检原因</param>
        /// <param name="isClearRedoValues">是否清除复检备份结果</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "检验单整单取消复检服务", Desc = "检验单整单取消复检服务", Url = "LabStarService.svc/LS_UDTO_LisTestFormReCheckCancel", Get = "", Post = "testFormIDList,isClearRedoDesc,isClearRedoValues,memoInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestFormReCheckCancel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestFormReCheckCancel(string testFormIDList, bool isClearRedoDesc, bool isClearRedoValues, string memoInfo);

        /// <summary>
        /// 检验单项目取消复检服务
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="testItemIDList">检验单项目ID列表字符串</param>
        /// <param name="isClearRedoDesc">是否清除复检原因</param>
        /// <param name="isClearRedoValues">是否清除复检备份结果</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "检验单项目取消复检服务", Desc = "检验单项目取消复检服务", Url = "LabStarService.svc/LS_UDTO_LisTestItemReCheckCancel", Get = "", Post = "testFormID,testItemIDList,isClearRedoDesc,isClearRedoValues", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestItemReCheckCancel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestItemReCheckCancel(long testFormID, string testItemIDList, bool isClearRedoDesc, bool isClearRedoValues, string memoInfo);


        /// <summary>
        /// 样本单批量审定服务
        /// </summary>
        /// <param name="testFormID">样本单ID</param>
        /// <param name="empID">审定人ID“</param>
        /// <param name="empName">审定人</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "样本单批量审定服务", Desc = "样本单批量审定服务", Url = "LabStarService.svc/LS_UDTO_LisTestFormBatchCheck", Get = "", Post = "testFormIDList,empID,empName,memoInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestFormBatchCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestFormBatchCheck(string testFormIDList, long empID, string empName, string memoInfo);

        /// <summary>
        /// 样本单审定服务
        /// </summary>
        /// <param name="testFormID">样本单ID</param>
        /// <param name="empID">审定人ID“</param>
        /// <param name="empName">审定人</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "样本单审定服务", Desc = "样本单审定服务", Url = "LabStarService.svc/LS_UDTO_LisTestFormCheck", Get = "", Post = "testFormID,empID,empName,memoInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestFormCheck", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestFormCheck(long testFormID, long empID, string empName, string memoInfo);

        /// <summary>
        /// 样本单取消审定服务
        /// </summary>
        /// <param name="testFormID">样本单ID</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "样本单取消审定服务", Desc = "样本单取消审定服务", Url = "LabStarService.svc/LS_UDTO_LisTestFormCheckCancel", Get = "", Post = "testFormID,empID,empName,memoInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestFormCheckCancel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestFormCheckCancel(long testFormID, long empID, string empName, string memoInfo);

        /// <summary>
        ///样本单删除恢复服务
        /// </summary>
        /// <param name="testFormID">样本单ID</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "样本单删除恢复服务", Desc = "样本单删除恢复服务", Url = "LabStarService.svc/LS_UDTO_LisTestFormDeleteCancel", Get = "", Post = "testFormID,memoInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestFormDeleteCancel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestFormDeleteCancel(long testFormID, string memoInfo);

        [ServiceContractDescription(Name = "样本号批量修改", Desc = "样本号批量修改", Url = "LabStarService.svc/LS_UDTO_LisTestFormSampleNoModify", Get = "", Post = "sectionID,curTestDate,curMinSampleNo,sampleCount,targetTestDate,targetMinSampleNo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_LisTestFormSampleNoModify", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_LisTestFormSampleNoModify(long sectionID, string curTestDate, string curMinSampleNo, int sampleCount, string targetTestDate, string targetMinSampleNo);

        #endregion

        #region 历史对比

        [ServiceContractDescription(Name = "样本单项目结果历史对比", Desc = "样本单项目结果历史对比", Url = "LabStarService.svc/LS_UDTO_ItemResultHistoryCompareByTestFormID", Get = "", Post = "testFormID", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_ItemResultHistoryCompareByTestFormID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_ItemResultHistoryCompareByTestFormID(long testFormID);

        [ServiceContractDescription(Name = "获取样本单项目结果历史对比值", Desc = "获取样本单项目结果历史对比值", Url = "LabStarService.svc/LS_UDTO_ItemResultHistoryCompareByTestItem", Get = "", Post = "testForm,listTestItem,fields", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_ItemResultHistoryCompareByTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_ItemResultHistoryCompareByTestItem(LisTestForm testForm, IList<LisTestItem> listTestItem, string fields);

        #endregion

        #region 仪器检验单定制服务

        [ServiceContractDescription(Name = "查询Lis_EquipForm(HQL)", Desc = "查询Lis_TestForm(HQL)", Url = "LabStarService.svc/LS_UDTO_QueryLisEquipFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestItem>", ReturnType = "ListLisTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLisEquipFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLisEquipFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询Lis_EquipItem(HQL)", Desc = "查询Lis_TestItem(HQL)", Url = "LabStarService.svc/LS_UDTO_QueryLisEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestItem>", ReturnType = "ListLisTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLisEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLisEquipItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        #endregion

        #region 危急值发送服务
        [ServiceContractDescription(Name = "危急值发送服务", Desc = "危急值发送服务", Url = "LabStarService.svc/LS_UDTO_SendPanicValueMsgToPlatform", Get = "", Post = "testFormMsgIDList,msgSendFlag,msgSendMemo", Return = "BaseResultDataValue", ReturnType = "LisTestItemReceive")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SendPanicValueMsgToPlatform", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SendPanicValueMsgToPlatform(string testFormMsgIDList, int msgSendFlag, string msgSendMemo);

        [ServiceContractDescription(Name = "危急值电话通知服务", Desc = "危急值电话通知服务", Url = "LabStarService.svc/LS_UDTO_PanicValuePhoneCall", Get = "", Post = "testFormMsgIDList,phoneCallFlag phoneNumber,phoneReceiver,phoneCallMemo", Return = "BaseResultDataValue", ReturnType = "LisTestItemReceive")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PanicValuePhoneCall", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PanicValuePhoneCall(string testFormMsgIDList, int phoneCallFlag, string phoneNumber, string phoneReceiver, string phoneCallMemo);

        [ServiceContractDescription(Name = "危急值阅读服务", Desc = "危急值阅读服务", Url = "LabStarService.svc/LS_UDTO_PanicValueRead", Get = "", Post = "testFormMsgIDList", Return = "BaseResultDataValue", ReturnType = "LisTestItemReceive")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PanicValueRead", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PanicValueRead(string testFormMsgIDList);
        #endregion

        #region 核收服务

        /// <summary>
        /// 快捷核收服务
        /// </summary>
        /// <param name="fieldName">核收字段名，必填，为空默认按条码号BarCode核收</param>
        /// <param name="filedVlaue">核收字段值，必填</param>
        /// <param name="sectionID">小组ID，必填，核收到那个小组</param>
        /// <param name="receiveDate">核收日期，核收到那个日期，为空则核收到当前日期</param>
        /// <param name="sampleNo">指定的样本号，可以为空</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "快捷核收服务", Desc = "快捷核收服务", Url = "LabStarService.svc/LS_UDTO_QuickReceiveBarCodeForm", Get = "", Post = "fieldName,filedVlaue,sectionID,receiveDate,sampleNo", Return = "BaseResultDataValue", ReturnType = "LisTestItemReceive")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QuickReceiveBarCodeForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QuickReceiveBarCodeForm(string fieldName, string filedVlaue, long sectionID, string receiveDate, string sampleNo);

        #endregion

        /// <summary>
        /// 更新检验单打印次数
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "更新检验单打印次数", Desc = "更新检验单打印次数", Url = "LabStarService.svc/LS_UDTO_UpdateLisTestFormPrintCount", Get = "", Post = "testFormID", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisTestFormPrintCount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_UpdateLisTestFormPrintCount(string testFormID);

        [ServiceContractDescription(Name = "", Desc = "", Url = "LabStarService.svc/LS_UDTO_QueryLisTestItemMergeImgeTest", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLisTestItemMergeImgeTest", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLisTestItemMergeImgeTest();

        [ServiceContractDescription(Name = "检查样本是否符合转换质控物", Desc = "检查样本是否符合转换质控物", Url = "LabStarService.svc/LS_UDTO_CheckSampleConvertStatus?TestFormID={TestFormID}&QCMatID={QCMatID}", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_CheckSampleConvertStatus?TestFormID={TestFormID}&QCMatID={QCMatID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool LS_UDTO_CheckSampleConvertStatus(long TestFormID, long QCMatID);

        [ServiceContractDescription(Name = "定制查询Lis_TestItem(HQL)", Desc = "定制查询Lis_TestItem(HQL)", Url = "LabStarService.svc/LS_UDTO_DZQueryLisTestItemByHQL?TestFormId={TestFormId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LisTestItem>", ReturnType = "ListLisTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_DZQueryLisTestItemByHQL?TestFormId={TestFormId}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_DZQueryLisTestItemByHQL(long TestFormId, int page, int limit, string fields, string where, string sort, bool isPlanish);

    }
}
