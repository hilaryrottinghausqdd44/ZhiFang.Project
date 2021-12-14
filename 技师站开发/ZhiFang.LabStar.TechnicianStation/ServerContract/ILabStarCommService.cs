using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation
{
    [ServiceContract]
    public interface ILabStarCommService
    {

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

        #region 定制服务

        [ServiceContractDescription(Name = "上传仪器参数文件", Desc = "上传仪器参数文件", Url = "LabStarCommService.svc/LS_UDTO_UpLoadEquipCommParaFile", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpLoadEquipCommParaFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message LS_UDTO_UpLoadEquipCommParaFile();

        [ServiceContractDescription(Name = "下载仪器参数文件", Desc = "下载仪器参数文件", Url = "LabStarCommService.svc/LS_UDTO_DownLoadEquipCommParaFile?equipID={equipID}&contentType={contentType}&operateType={operateType}", Get = "equipID={equipID}&contentType={contentType}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/LS_UDTO_DownLoadEquipCommParaFile?equipID={equipID}&contentType={contentType}&operateType={operateType}")]
        [OperationContract]
        Stream LS_UDTO_DownLoadEquipCommParaFile(long equipID, long contentType, int operateType);


        [ServiceContractDescription(Name = "上传仪器结果信息", Desc = "上传仪器结果信息", Url = "LabStarCommService.svc/LS_UDTO_UpLoadEquipResultInfo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpLoadEquipResultInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_UpLoadEquipResultInfo();

        #endregion

        #region 通讯管理程序基础数据同步服务

        [ServiceContractDescription(Name = "查询LB_Equip(HQL)--通讯程序同步", Desc = "查询LB_Equip(HQL)--通讯程序同步", Url = "LabStarBaseTableService.svc/LS_Sync_SearchLBEquipByHQL?labID={labID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "labID={labID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBEquip>", ReturnType = "ListLBEquip")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_Sync_SearchLBEquipByHQL?labID={labID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_Sync_SearchLBEquipByHQL(string labID, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询LBEquipItem(HQL)--通讯程序同步", Desc = "查询LBEquipItem(HQL)--通讯程序同步", Url = "LabStarBaseTableService.svc/LS_Sync_QueryLBEquipItemByHQL?labID={labID}&where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", Get = "labID={labID}&where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBEquipItem>", ReturnType = "ListLBEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_Sync_QueryLBEquipItemByHQL?labID={labID}&where={where}&sort={sort}&page={page}&limit={limit}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_Sync_QueryLBEquipItemByHQL(string labID, string where, string sort, int page, int limit, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询LB_SampleType(HQL)--通讯程序同步", Desc = "查询LB_SampleType(HQL)--通讯程序同步", Url = "LabStarBaseTableService.svc/LS_Sync_SearchLBSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "labID={labID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSampleType>", ReturnType = "ListLBSampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_Sync_SearchLBSampleTypeByHQL?labID={labID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_Sync_SearchLBSampleTypeByHQL(string labID, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询LB_QCItem(HQL)--通讯程序同步", Desc = "查询LB_QCItem(HQL)--通讯程序同步", Url = "LabStarQCService.svc/QC_Sync_QueryLBQCItemByHQL?labID={labID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "labID={labID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCItem>", ReturnType = "ListLBQCItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_Sync_QueryLBQCItemByHQL?labID={labID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QC_Sync_QueryLBQCItemByHQL(string labID, int page, int limit, string fields, string where, string sort, bool isPlanish);


        [ServiceContractDescription(Name = "查询LB_QCMaterial(HQL)--通讯程序同步", Desc = "查询LB_QCMaterial(HQL)--通讯程序同步", Url = "LabStarQCService.svc/QC_Sync_SearchLBQCMaterialByHQL?labID={labID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "labID={labID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBQCMaterial>", ReturnType = "ListLBQCMaterial")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QC_Sync_SearchLBQCMaterialByHQL?labID={labID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QC_Sync_SearchLBQCMaterialByHQL(string labID, int page, int limit, string fields, string where, string sort, bool isPlanish);

        #endregion

    }
}
