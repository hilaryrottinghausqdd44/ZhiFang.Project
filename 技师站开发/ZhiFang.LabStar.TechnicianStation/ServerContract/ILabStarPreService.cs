using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Request;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation
{
    [ServiceContract]
    public interface ILabStarPreService
    {
        #region
        [ServiceContractDescription(Name = "新增和删除采样组项目", Desc = "新增和删除采样组项目", Url = "LabStarPreService.svc/LS_UDTO_AddDelLBSamplingItem", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddDelLBSamplingItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddDelLBSamplingItem(IList<LBSamplingItem> addEntityList, bool isCheckEntityExist, string delIDList);

        [ServiceContractDescription(Name = "新增和删除取单分类项目", Desc = "新增和删除取单分类项目", Url = "LabStarPreService.svc/LS_UDTO_AddDelLBReportDateItem", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddDelLBReportDateItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddDelLBReportDateItem(IList<LBReportDateItem> addEntityList, bool isCheckEntityExist, string delIDList);

        [ServiceContractDescription(Name = "查询LB_SamplingGroup(HQL)", Desc = "查询LB_SamplingGroup(HQL)", Url = "LabStarPreService.svc/LS_UDTO_QueryLBSamplingGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLBSamplingGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLBSamplingGroupByHQL(string where, string fields, string sort, int page, int limit, bool isPlanish);

        [ServiceContractDescription(Name = "查询LB_SamplingItem(HQL)", Desc = "查询LB_SamplingItem(HQL)", Url = "LabStarPreService.svc/LS_UDTO_QueryLBSamplingItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLBSamplingItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLBSamplingItemByHQL(string where, string fields, string sort, int page, int limit, bool isPlanish);

        [ServiceContractDescription(Name = "根据采样组和项目关系查询采样组信息", Desc = "根据采样组和项目关系查询采样组信息", Url = "LabStarPreService.svc/LS_UDTO_QuerySamplingGroupIsMultiItem?where={where}&fields={fields}&isMulti={isMulti}&isPlanish={isPlanish}", Get = "where={where}&fields={fields}&isMulti={isMulti}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QuerySamplingGroupIsMultiItem?where={where}&fields={fields}&isMulti={isMulti}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QuerySamplingGroupIsMultiItem(string where, string fields, bool isMulti, bool isPlanish);

        [ServiceContractDescription(Name = "根据采样组和项目关系查询项目信息", Desc = "根据采样组和项目关系查询项目信息", Url = "LabStarPreService.svc/LS_UDTO_QueryItemIsMultiSamplingGroup?where={where}&strSectionID={strSectionID}&fields={fields}&isMulti={isMulti}&isPlanish={isPlanish}", Get = "where={where}&strSectionID={strSectionID}&fields={fields}&isMulti={isMulti}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryItemIsMultiSamplingGroup?where={where}&strSectionID={strSectionID}&fields={fields}&isMulti={isMulti}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryItemIsMultiSamplingGroup(string where, string strSectionID, string fields, bool isMulti, bool isPlanish);

        [ServiceContractDescription(Name = "查询没有设置采样组的项目信息", Desc = "查询没有设置采样组的项目信息", Url = "LabStarPreService.svc/LS_UDTO_QueryItemNoSamplingGroup?where={where}&strSectionID={strSectionID}&fields={fields}&isPlanish={isPlanish}", Get = "where={where}&strSectionID={strSectionID}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryItemNoSamplingGroup?where={where}&strSectionID={strSectionID}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryItemNoSamplingGroup(string where, string strSectionID, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询LB_ReportDateItem(HQL)", Desc = "查询LB_ReportDateItem(HQL)", Url = "LabStarPreService.svc/LS_UDTO_QueryLBReportDateItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLBReportDateItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLBReportDateItemByHQL(string where, string fields, string sort, int page, int limit, bool isPlanish);

        [ServiceContractDescription(Name = "查询LB_ReportDateRule(HQL)", Desc = "查询LB_ReportDateRule(HQL)", Url = "LabStarPreService.svc/LS_UDTO_QueryLBReportDateRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryLBReportDateRuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryLBReportDateRuleByHQL(string where, string fields, string sort, int page, int limit, bool isPlanish);

        [ServiceContractDescription(Name = "根据ID删除LB_ReportDate", Desc = "根据ID删除LB_ReportDate", Url = "LabStarPreService.svc/LS_UDTO_DeleteLBReportDateByID?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_DeleteLBReportDateByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_DeleteLBReportDateByID(long id);
        #endregion

        #region 医嘱开单
        [ServiceContractDescription(Name = "检验医嘱_医嘱开单保存", Desc = "检验医嘱_医嘱开单保存", Url = "LabStarPreService.svc/AddOrder", Get = "", Post = "strSQL", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddOrder", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddOrder(LisPatient LisPatient, LisOrderForm LisOrderForm, IList<LisOrderItem> LisOrderItems);

        [ServiceContractDescription(Name = "检验医嘱_医嘱开单修改", Desc = "检验医嘱_医嘱开单修改", Url = "LabStarPreService.svc/EditOrder", Get = "", Post = "strSQL", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/EditOrder", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResult EditOrder(LisPatient LisPatient, string LisPatientFields, LisOrderForm LisOrderForm, string LisOrderFormFields, IList<LisOrderItem> LisOrderItems);

        [ServiceContractDescription(Name = "检验医嘱_医嘱开单获得项目树", Desc = "检验医嘱_医嘱开单获得项目树", Url = "LabStarPreService.svc/GetItemModelTree", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetItemModelTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetItemModelTree();

        [ServiceContractDescription(Name = "检验医嘱_申请单列表", Desc = "检验医嘱_申请单列表", Url = "LabStarPreService.svc/GetOrderList", Get = "hisDeptNo={hisDeptNo}&patno={patno}&sickTypeNo={sickTypeNo}&strWhere={strWhere}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetOrderList?hisDeptNo={hisDeptNo}&patno={patno}&sickTypeNo={sickTypeNo}&strWhere={strWhere}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetOrderList(string hisDeptNo, string patno, string sickTypeNo, string strWhere);

        [ServiceContractDescription(Name = "检验医嘱_审核医嘱单", Desc = "检验医嘱_审核医嘱单", Url = "LabStarPreService.svc/UpdateOrder", Get = "orderFormNo={orderFormNo}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateOrder?orderFormNo={orderFormNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateOrder(string orderFormNo);
        [ServiceContractDescription(Name = "检验医嘱_取消审核医嘱单", Desc = "检验医嘱_取消审核医嘱单", Url = "LabStarPreService.svc/CancelOrder", Get = "orderFormNo={orderFormNo}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CancelOrder?orderFormNo={orderFormNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CancelOrder(string orderFormNo);

        [ServiceContractDescription(Name = "检验医嘱_删除医嘱单", Desc = "检验医嘱_删除医嘱单", Url = "LabStarPreService.svc/DeleteOrder", Get = "orderFormNo={orderFormNo}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteOrder?orderFormNo={orderFormNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DeleteOrder(string orderFormNo);

        [ServiceContractDescription(Name = "检验医嘱_人员和部门查询", Desc = "检验医嘱_人员和部门查询", Url = "LabStarPreService.svc/GetLIIPHREmployeeAndHRDept", Get = "CName={CName}&DicType={DicType}&TSysCode={TSysCode}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLIIPHREmployeeAndHRDept?CName={CName}&DicType={DicType}&TSysCode={TSysCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetLIIPHREmployeeAndHRDept(string CName, string DicType, string TSysCode);
        #endregion

        #region 采样组项目定制服务
        [ServiceContractDescription(Name = "SearchLBSamplingItemBandItemNameList(HQL)", Desc = "查询SearchLBSamplingItemBandItemNameList(HQL)", Url = "LabStarPreService.svc/SearchLBSamplingItemBandItemNameList?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBSamplingItem>", ReturnType = "ListLBSamplingItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SearchLBSamplingItemBandItemNameList?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SearchLBSamplingItemBandItemNameList(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "根据采样组项目查询LB_Item未选项", Desc = "根据采样组项目查询LB_Item未选项", Url = "LabStarPreService.svc/LS_UDTO_SearchLBItemByLBSamplingItem?SamplingGroupID={SamplingGroupID}&SectionID={SectionID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItem>", ReturnType = "ListLBItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemByLBSamplingItem?SamplingGroupID={SamplingGroupID}&SectionID={SectionID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemByLBSamplingItem(long SamplingGroupID, long SectionID, int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "根据取单时间分类项目查询LB_Item未选项", Desc = "根据取单时间分类项目查询LB_Item未选项", Url = "LabStarPreService.svc/LS_UDTO_SearchLBItemByLBReportDateItem?ReportDateID={ReportDateID}&SectionID={SectionID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBItem>", ReturnType = "ListLBItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBItemByLBReportDateItem?ReportDateID={ReportDateID}&SectionID={SectionID}&page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBItemByReportDateID(long ReportDateID, long SectionID, int page, int limit, string fields, string where, string sort, bool isPlanish);

        /// <summary>
        /// 采样组项目缺省设置（只允许设置一个缺省采样组）
        /// </summary>
        /// <param name="Id">主键id</param>
        /// <param name="ItemId">项目id</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "采样组项目缺省设置", Desc = "采样组项目缺省设置", Url = "LabStarPreService.svc/LS_UDTO_UpdateSamplingItemIsDefault", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateSamplingItemIsDefault", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_UpdateSamplingItemIsDefault(long? Id, long? ItemId, bool IsDefault);

        #endregion
                                                                   
        #region 前处理参数维护定制服务

        /// <summary>
        /// 查询个性设置信息列表并赋值默认参数列与新增默认参数设置
        /// 例如：按站点设置的参数，则查询站点列表
        /// </summary>
        /// <param name="paraTypeCode">系统相关性ID</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "前处理系统参数维护_查询个性设置信息列表", Desc = "前处理系统参数维护_查询个性设置信息列表", Url = "LabStarPreService.svc/LS_UDTO_QueryParaSystemTypeInfoAndAddDefultPara?systemTypeCode={systemTypeCode}&paraTypeCode={paraTypeCode}", Get = "systemTypeCode={systemTypeCode}&paraTypeCode={paraTypeCode}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryParaSystemTypeInfoAndAddDefultPara?systemTypeCode={systemTypeCode}&paraTypeCode={paraTypeCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryParaSystemTypeInfoAndAddDefultPara(string systemTypeCode, string paraTypeCode);

        /// <summary>
        /// 修改前处理系统个性参数设置
        /// </summary>
        /// <param name="objectInfo">个性化信息</param>
        /// <param name="entityList">个性参数列表</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "前处理系统参数维护_修改前处理系统个性参数设置", Desc = "前处理系统参数维护_修改前处理系统个性参数设置", Url = "LabStarPreService.svc/LS_UDTO_UpdateParSystemParaItem", Get = "", Post = "entityList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateParSystemParaItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_UpdateParSystemParaItem(string ObjectID, List<BParaItem> entityList);

        [ServiceContractDescription(Name = "前处理系统参数维护_查询字段选择枚举", Desc = "前处理系统参数维护_查询字段选择枚举", Url = "LabStarPreService.svc/LS_UDTO_QueryOrderBarCodeSelectFields?paraTypeCode={paraTypeCode}", Get = "paraTypeCode={paraTypeCode}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_QueryOrderBarCodeSelectFields?paraTypeCode={paraTypeCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_QueryOrderBarCodeSelectFields(string paraTypeCode);

        [ServiceContractDescription(Name = "前处理系统参数维护_根据字典类型获取字典数据", Desc = "前处理系统参数维护_根据字典类型获取字典数据", Url = "LabStarPreService.svc/LS_UDTO_GetParaDicData?type={type}", Get = "paraTypeCode={paraTypeCode}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_GetParaDicData?type={type}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_GetParaDicData(string type);

        [ServiceContractDescription(Name = "前处理系统参数维护_获取系统参数", Desc = "前处理系统参数维护_获取系统参数", Url = "LabStarPreService.svc/LS_UDTO_GetParParaAndParaItem?nodetype={nodetype}&paranos={paranos}&typecode={typecode}", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_GetParParaAndParaItem?nodetype={nodetype}&paranos={paranos}&typecode={typecode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_GetParParaAndParaItem(long nodetype, string paranos, string typecode);

        /// <summary>
        /// 删除系统个性参数设置
        /// </summary>
        /// <param name="objectInfo">个性化信息</param>
        /// <returns></returns>
        [ServiceContractDescription(Name = "前处理系统参数维护_删除系统个性参数设置", Desc = "前处理系统参数维护_删除系统个性参数设置", Url = "LabStarPreService.svc/LS_UDTO_DeleteSystemParaItem", Get = "", Post = "objectInfo", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_DeleteSystemParaItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_DeleteSystemParaItem(string objectInfo);

        #endregion

        #region 样本条码

        [ServiceContractDescription(Name = "样本条码_HIS获取医嘱并进行分管", Desc = "样本条码_HIS获取医嘱并进行分管", Url = "LabStarPreService.svc/LS_UDTO_PreHISGetSamplingGrouping", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreHISGetSamplingGrouping", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreHISGetSamplingGrouping(long nodetype, string receiveType, string value, int days, string fields, bool isPlanish, int nextindex);

        [ServiceContractDescription(Name = "样本条码_LIS获取医嘱并进行分管", Desc = "样本条码_LIS获取医嘱并进行分管", Url = "LabStarPreService.svc/LS_UDTO_PreLISGETSamplingGrouping", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreLISGETSamplingGrouping", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreLISGETSamplingGrouping(long nodetype, string receiveType, string value, int days, string fields, bool isPlanish, int nextindex);

        [ServiceContractDescription(Name = "样本条码_重新分组", Desc = "样本条码_重新分组", Url = "LabStarPreService.svc/LS_UDTO_PreANewSamplingGrouping", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreANewSamplingGrouping", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreANewSamplingGrouping(string barcode,long nodetype, string receiveType, string value, int days, string fields, bool isPlanish, int nextindex);

        [ServiceContractDescription(Name = "样本条码_样本确认", Desc = "样本条码_样本确认", Url = "LabStarPreService.svc/LS_UDTO_PreBarCodeAffirm", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreBarCodeAffirm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreBarCodeAffirm(long nodetype, string barcodes);

        [ServiceContractDescription(Name = "样本条码_作废条码", Desc = "样本条码_作废条码", Url = "LabStarPreService.svc/LS_UDTO_PreBarCodeInvalid", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreBarCodeInvalid", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreBarCodeInvalid(long nodetype, string barcodes);

        [ServiceContractDescription(Name = "样本条码_验卡", Desc = "样本条码_验卡", Url = "LabStarPreService.svc/LS_UDTO_VerifyCardNo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_VerifyCardNo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_VerifyCardNo(long nodetype, string receiveType, string cardno);

        [ServiceContractDescription(Name = "样本条码_HIS医嘱信息", Desc = "样本条码_HIS医嘱信息", Url = "LabStarPreService.svc/LS_UDTO_HISOrderInfo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_HISOrderInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_HISOrderInfo(long nodetype, string barcode);

        [ServiceContractDescription(Name = "样本条码_取单凭证", Desc = "样本条码_取单凭证", Url = "LabStarPreService.svc/LS_UDTO_PreBarCodeGatherVoucher", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreBarCodeGatherVoucher", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreBarCodeGatherVoucher(long nodetype, string barcode,bool? isloadtable,bool? isupdatebcitems,string modelcode);

        [ServiceContractDescription(Name = "样本条码_已打印样本条码查询", Desc = "样本条码_已打印样本条码查询", Url = "LabStarPreService.svc/LS_UDTO_GetHaveToPrintBarCodeForm", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_GetHaveToPrintBarCodeForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_GetHaveToPrintBarCodeForm(string barcode,string where,bool? printStatus, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "样本条码_条码打印数量获取", Desc = "样本条码_条码打印数量", Url = "LabStarPreService.svc/LS_UDTO_GetPrintBarCodeCount", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_GetPrintBarCodeCount", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_GetPrintBarCodeCount(long nodetype, string barcodes);

        [ServiceContractDescription(Name = "样本条码_打印状态更新", Desc = "样本条码_打印状态更新", Url = "LabStarPreService.svc/LS_UDTO_UpdateBarCodeFromPrintStatus", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateBarCodeFromPrintStatus", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_UpdateBarCodeFromPrintStatus(long nodetype, string barcodes,string IsAffirmBarCode);

        [ServiceContractDescription(Name = "样本条码_叫号系统排队号信息生成", Desc = "样本条码_叫号系统排队号信息生成", Url = "LabStarPreService.svc/LS_UDTO_CreatEqueuingMachineInfo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_CreatEqueuingMachineInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_CreatEqueuingMachineInfo(long nodetype, string barcode,string patientType);

        [ServiceContractDescription(Name = "样本条码_预制条码保存", Desc = "样本条码_预制条码保存", Url = "LabStarPreService.svc/LS_UDTO_UpdateLisBarCodeFormBarCodeByBarCodeFormID", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_UpdateLisBarCodeFormBarCodeByBarCodeFormID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_UpdateLisBarCodeFormBarCodeByBarCodeFormID(long nodetype, string barcode, string barcodeformid);

        [ServiceContractDescription(Name = "样本条码_撤销确认数据查询以及校验", Desc = "样本条码_撤销确认数据查询以及校验", Url = "LabStarPreService.svc/LS_UDTO_PreSampleAffirmDataVerifyByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleAffirmDataVerifyByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleAffirmDataVerifyByBarCode(long nodetype, string barcodes);

        [ServiceContractDescription(Name = "样本条码_根据条码号撤销确认", Desc = "样本条码_根据条码号撤销确认", Url = "LabStarPreService.svc/LS_UDTO_PreSampleRevocationAffirmByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleRevocationAffirmByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleRevocationAffirmByBarCode(long nodetype, string barcodes);
        #endregion

        #region 样本采集
        [ServiceContractDescription(Name = "样本采集_根据条码号获取样本数据_更新样本状态", Desc = "样本采集_根据条码号获取样本数据_更新样本状态", Url = "LabStarPreService.svc/LS_UDTO_PreSampleGatherAndUpdateStateByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleGatherAndUpdateStateByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleGatherAndUpdateStateByBarCode(long nodetype, string barcodes,bool? isupdate, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "样本采集_根据条码号更新采集状态", Desc = "样本采集_根据条码号更新采集状态", Url = "LabStarPreService.svc/LS_UDTO_PreSampleGatherAndUpdateStateByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreUpdateSampleGatherStateByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreUpdateSampleGatherStateByBarCode(long nodetype, string barcodes,bool? isConstraintUpdate);

        [ServiceContractDescription(Name = "样本采集_根据查询条件获取样本数据", Desc = "样本采集_根据查询条件获取样本数据", Url = "LabStarPreService.svc/LS_UDTO_PreGetSampleGatherFormListByWhere", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreGetSampleGatherFormListByWhere", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreGetSampleGatherFormListByWhere(long nodetype, string where, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "样本采集_根据条码号获取样本数据_更新样本状态", Desc = "样本采集_根据条码号获取样本数据_更新样本状态", Url = "LabStarPreService.svc/LS_UDTO_PreSampleGatherGetBarCodeFromByCheckWhere", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleGatherGetBarCodeFromByCheckWhere", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleGatherGetBarCodeFromByCheckWhere(long nodetype, string receiveType, string value, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "样本采集_根据条码号撤销采集", Desc = "样本采集_根据条码号撤销采集", Url = "LabStarPreService.svc/LS_UDTO_PreSampleRevocationGatherByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleRevocationGatherByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleRevocationGatherByBarCode(long nodetype, string barcodes);

        [ServiceContractDescription(Name = "样本采集_撤销采集数据查询以及校验", Desc = "样本采集_撤销数据查询以及校验", Url = "LabStarPreService.svc/LS_UDTO_PreSampleRevocationGatherDataVerifyByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleRevocationGatherDataVerifyByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleRevocationGatherDataVerifyByBarCode(long nodetype, string barcodes);

        [ServiceContractDescription(Name = "样本采集_一批条码生成打包号", Desc = "样本采集_一批条码生成打包号", Url = "LabStarPreService.svc/LS_UDTO_PreSampleGatherCreateCollectPackNoByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleGatherCreateCollectPackNoByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleGatherCreateCollectPackNoByBarCode(long nodetype, string barcodes);

        #endregion

        #region 样本送检
        [ServiceContractDescription(Name = "样本送检_根据条码号获取样本数据_更新样本状态", Desc = "样本送检_根据条码号获取样本数据_更新样本状态", Url = "LabStarPreService.svc/LS_UDTO_PreSampleExchangeInspectAndUpdateStateByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleExchangeInspectAndUpdateStateByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleExchangeInspectAndUpdateStateByBarCode(long nodetype, string barcodes, bool? isupdate, string userid, string username, string fields, bool isPlanish);
        
        [ServiceContractDescription(Name = "样本送检_根据条码号更新送检状态", Desc = "样本送检_根据条码号更新送检状态", Url = "LabStarPreService.svc/LS_UDTO_PreUpdateSampleExchangeInspectStateByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreUpdateSampleExchangeInspectStateByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreUpdateSampleExchangeInspectStateByBarCode(long nodetype, string barcodes,string userid,string username, bool? isConstraintUpdate);
        
        [ServiceContractDescription(Name = "样本送检_根据条码号撤销送检", Desc = "样本送检_根据条码号撤销送检", Url = "LabStarPreService.svc/LS_UDTO_PreSampleRevocationExchangeInspectByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleRevocationExchangeInspectByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleRevocationExchangeInspectByBarCode(long nodetype, string barcodes);

        [ServiceContractDescription(Name = "样本送检_撤销送检数据查询以及校验", Desc = "样本送检_撤销送检数据查询以及校验", Url = "LabStarPreService.svc/LS_UDTO_PreSampleRevocationExchangeInspectDataVerifyByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleRevocationExchangeInspectDataVerifyByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleRevocationExchangeInspectDataVerifyByBarCode(long nodetype, string barcodes);

        [ServiceContractDescription(Name = "样本送检_根据查询条件获取样本数据", Desc = "样本送检_根据查询条件获取样本数据", Url = "LabStarPreService.svc/LS_UDTO_PreGetSampleExchangeInspectFormListByWhere", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreGetSampleExchangeInspectFormListByWhere", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreGetSampleExchangeInspectFormListByWhere(long nodetype, string where, string fields, bool isPlanish, string relationForm);
        #endregion

        #region 样本送达
        [ServiceContractDescription(Name = "样本送达_根据条码号获取样本数据", Desc = "样本送达_根据条码号获取样本数据", Url = "LabStarPreService.svc/LS_UDTO_PreSampledeliveryGetBarCodeForm", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampledeliveryGetBarCodeForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampledeliveryGetBarCodeForm(long nodetype, string barcodes,bool isUpdate, string userid, string username, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "样本送达_根据护工号获取身份信息（护工或护士）", Desc = "样本送达_根据护工号获取身份信息（护工或护士）", Url = "LabStarPreService.svc/LS_UDTO_PreSampledeliveryGetEmpInfo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampledeliveryGetEmpInfo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampledeliveryGetEmpInfo(string cardno);

        [ServiceContractDescription(Name = "样本送达_根据条码号跟新送达状态", Desc = "样本送达_根据条码号跟新送达状态", Url = "LabStarPreService.svc/LS_UDTO_PreSampledeliveryUpdateBarCodeFormArrive", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampledeliveryUpdateBarCodeFormArrive", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampledeliveryUpdateBarCodeFormArrive(long nodetype, string barcodes,string userid,string username);

        #endregion

        #region 样本签收
        [ServiceContractDescription(Name = "样本签收_根据查询条件获取样本列表", Desc = "样本签收_根据查询框的条件获取样本列表", Url = "LabStarPreService.svc/LS_UDTO_PreSampleSignForGetBarCodeFromListByWhere", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleSignForGetBarCodeFromListByWhere", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleSignForGetBarCodeFromListByWhere(long nodetypeID, string fields, string where, bool isPlanish,string sortFields,string relationForm);

        [ServiceContractDescription(Name = "样本签收_签收操作", Desc = "样本签收_根据条码号进行具体签收操作", Url = "LabStarPreService.svc/LS_UDTO_PreSignForSampleByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSignForSampleByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSignForSampleByBarCode(long nodetypeID, string barCodes, string fields, string sickType, string deliverier, string deliverierID, bool isPlanish, bool isAutoSignFor, bool isForceSignFor);

        [ServiceContractDescription(Name = "样本签收_根据条码号获取样本列表", Desc = "样本签收_根据条码号获取样本列表", Url = "LabStarPreService.svc/LS_UDTO_PreSampleSignForGetBarCodeFormByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleSignForGetBarCodeFormByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleSignForGetBarCodeFormByBarCode(long nodetypeID, string barCode, string sickType, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "样本签收_根据打包号签收或获取样本列表", Desc = "样本签收_根据打包号签收或获取样本列表", Url = "LabStarPreService.svc/LS_UDTO_PreSampleSignForOrGetBarCodeFormByPackNo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleSignForOrGetBarCodeFormByPackNo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleSignForOrGetBarCodeFormByPackNo(long nodetypeID, string packNo, string sickType, string deliverier, string deliverierID, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "样本签收_取消签收", Desc = "样本签收_通过条码号取消签收", Url = "LabStarPreService.svc/LS_UDTO_PreCancelSampleSignForOrByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreCancelSampleSignForOrByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreCancelSampleSignForOrByBarCode(long nodetypeID, string BarCodeList, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "样本签收_通过条码号获取所在打包号所有样本信息", Desc = "样本签收_通过条码号获取所在打包号所有样本信息", Url = "LabStarPreService.svc/LS_UDTO_PreSampleSignForGetPackNoRelationBarCodeFormListByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleSignForGetPackNoRelationBarCodeFormListByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleSignForGetPackNoRelationBarCodeFormListByBarCode(long nodetypeID, string barCode, string sickType, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "样本签收_通过条码号获取和配置参数条件获取需要打印取单凭证的样本信息", Desc = "样本签收_通过条码号获取和配置参数条件获取需要打印取单凭证的样本信息", Url = "LabStarPreService.svc/LS_UDTO_PreSampleSignForGetNeedPrintVoucherBarCodeFormListByBarCodeAndPara", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleSignForGetNeedPrintVoucherBarCodeFormListByBarCodeAndPara", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleSignForGetNeedPrintVoucherBarCodeFormListByBarCodeAndPara(long nodetypeID, string barCodes,  string modelcode);

        [ServiceContractDescription(Name = "样本签收_通过专业大组ID获取所有小组下的所有项目ID", Desc = "样本签收_通过专业大组ID获取所有小组下的所有项目ID", Url = "LabStarPreService.svc/LS_UDTO_PreSampleSignForGetAllItemIdBySuperSectionID", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleSignForGetAllItemIdBySuperSectionID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleSignForGetAllItemIdBySuperSectionID(string superSectionID);

        [ServiceContractDescription(Name = "样本签收_通过检验小组ID获取小组下的所有项目ID", Desc = "样本签收_通过检验小组ID获取小组下的所有项目ID", Url = "LabStarPreService.svc/LS_UDTO_PreSampleSignForGetAllItemIdBySectionID", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleSignForGetAllItemIdBySectionID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleSignForGetAllItemIdBySectionID(string sectionID);
        #endregion

        #region 样本拒收

        [ServiceContractDescription(Name = "样本拒收_获取样本拒收列表", Desc = "样本拒收_根据查询条件获取样本拒收列表", Url = "LabStarPreService.svc/LS_UDTO_PreRefuseAcceptGetSampleListByWhere", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreRefuseAcceptGetSampleListByWhere", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreRefuseAcceptGetSampleListByWhere(long nodetypeID, string where, string fields, bool isPlanish,string sickTypeId,string sickTypeName);


        [ServiceContractDescription(Name = "样本拒收_样本拒收操作", Desc = "样本拒收_根据条码号进行样本拒收操作", Url = "LabStarPreService.svc/LS_UDTO_PreRefuseAcceptEditSample", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreRefuseAcceptEditSample", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreRefuseAcceptEditSample(long nodetypeID, string barcodes, string refuseReason, string handleAdvice, string answerPeople, string phoneNum, string refuseRemark, string fields, bool isPlanish, bool isForceReject, string sickTypeId,string sickTyepName,long refuseID);
        #endregion

        #region 样本分发
        [ServiceContractDescription(Name = "样本分发_根据查询条件获取样本列表", Desc = "样本分发_根据查询框的条件获取样本列表", Url = "LabStarPreService.svc/LS_UDTO_PreSampleDispenseGetBarCodeFromListByWhere", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleDispenseGetBarCodeFromListByWhere", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleDispenseGetBarCodeFromListByWhere(long nodetypeID, string fields, string where, bool isPlanish, string sortFields, string relationForm);

        [ServiceContractDescription(Name = "样本分发_根据样本单id获取所包含项目的分发信息列表", Desc = "样本分发_根据样本单id获取所包含项目的分发信息列表", Url = "LabStarPreService.svc/LS_UDTO_PreSampleDispenseGetBarCodeItemDispenseInfoListByFormId", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleDispenseGetBarCodeItemDispenseInfoListByFormId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleDispenseGetBarCodeItemDispenseInfoListByFormId(long nodetypeID, string fields, long barcodeFormId, bool isPlanish);

        [ServiceContractDescription(Name = "样本分发_根据条码号获取样本列表", Desc = "样本分发_根据条码号获取样本列表", Url = "LabStarPreService.svc/LS_UDTO_PreSampleDispenseGetBarCodeFormByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleDispenseGetBarCodeFormByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleDispenseGetBarCodeFormByBarCode(long nodetypeID, string barCode, string sickType, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "样本分发_根据条码号进行签收", Desc = "样本分发_根据条码号进行签收", Url = "LabStarPreService.svc/LS_UDTO_PreSampleDispenseSingnForBarCodeFormByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleDispenseSingnForBarCodeFormByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleDispenseSingnForBarCodeFormByBarCode(long nodetypeID, string barCodes, string fields, string sickType, string deliverier, string deliverierID, bool isPlanish, bool isAutoSignFor, bool isForceSignFor);

        [ServiceContractDescription(Name = "样本分发_根据条码号进行分发", Desc = "样本分发_根据条码号进行分发", Url = "LabStarPreService.svc/LS_UDTO_PreSampleDispenseByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleDispenseByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleDispenseByBarCode(long nodetypeID, string barCodes, string fields, string sickType, bool isPlanish, bool isForceDispense,string TestDate,string ruleType);

        [ServiceContractDescription(Name = "样本分发_根据条码号进行分发取消操作", Desc = "样本分发_根据条码号进行分发取消操作", Url = "LabStarPreService.svc/LS_UDTO_PreSampleDispenseCancelByBarCodeFormId", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleDispenseCancelByBarCodeFormId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleDispenseCancelByBarCodeFormId(long nodetypeID, string barCodeFormIds, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "样本分发_打印分发标签", Desc = "样本分发_打印分发标签", Url = "LabStarPreService.svc/LS_UDTO_PreSampleDispensePrintDispenseTagByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleDispensePrintDispenseTagByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleDispensePrintDispenseTagByBarCode(long nodetypeID, string barCodes);

        [ServiceContractDescription(Name = "样本分发_打印流转单", Desc = "样本分发_打印流转单", Url = "LabStarPreService.svc/LS_UDTO_PreSampleDispensePrintFlowSheetByBarCode", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_PreSampleDispensePrintFlowSheetByBarCode", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_PreSampleDispensePrintFlowSheetByBarCode(long nodetypeID, string barCodes);
        #endregion

        #region 其他定制服务
        [ServiceContractDescription(Name = "查询没有配置过的站点类型", Desc = "查询没有配置过的站点类型", Url = "LabStarPreService.svc/LS_UDTO_SearchBHostTypeNotPara", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBHostTypeNotPara", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBHostTypeNotPara(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "人员站点类型复制", Desc = "人员站点类型复制", Url = "LabStarPreService.svc/LS_UDTO_BHostTypeUserCopy", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_BHostTypeUserCopy", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_BHostTypeUserCopy(long pasteuser, string Copyusers);

        [ServiceContractDescription(Name = "医嘱模板 获得项目树", Desc = "医嘱模板 获得项目树", Url = "LabStarPreService.svc/GetOrderModelTree", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetOrderModelTree", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetOrderModelTree(string OrderModelTypeID,string ItemWhere);

        [ServiceContractDescription(Name = "获取人员以及人员权限", Desc = "获取人员以及人员权限", Url = "LabStarPreService.svc/LS_UDTO_GetLIIPHREmpIdentity", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_GetLIIPHREmpIdentity", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_GetLIIPHREmpIdentity(string where);

        [ServiceContractDescription(Name = "查询站点类型表(HQL)", Desc = "查询站点类型表(HQL)", Url = "LabStarPreService.svc/LS_UDTO_SearchBHostTypeUserAndHostTypeNameByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<BHostTypeUser>", ReturnType = "ListBHostTypeUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchBHostTypeUserAndHostTypeNameByHQL?page={page}&limit={limit}&fields={fields}&where={where}&systemTypeCode={systemTypeCode}&paraTypeCode={paraTypeCode}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchBHostTypeUserAndHostTypeNameByHQL(int page, int limit, string fields, string where, string systemTypeCode, string paraTypeCode, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询分发规则以及表中ID对应的Name(HQL)", Desc = "查询分发规则以及表中ID对应的Name(HQL)", Url = "LabStarPreService.svc/LS_UDTO_SearchLBTranRuleAndDicNameByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LBTranRule>", ReturnType = "ListLBTranRule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_SearchLBTranRuleAndDicNameByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_SearchLBTranRuleAndDicNameByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "新增和删除分发规则项目", Desc = "新增和删除分发规则项目", Url = "LabStarPreService.svc/LS_UDTO_AddDelLBTranRuleItem", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddDelLBTranRuleItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddDelLBTranRuleItem(IList<LBTranRuleItem> addEntityList, bool isCheckEntityExist, string delIDList);

        [ServiceContractDescription(Name = "新增和删除站点类型对应小组", Desc = "新增和删除站点类型对应小组", Url = "LabStarPreService.svc/LS_UDTO_AddDelLBTranRuleHostSection", Get = "", Post = "addEntityList,isCheckEntityExist,delIDList", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_AddDelLBTranRuleHostSection", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_AddDelLBTranRuleHostSection(IList<LBTranRuleHostSection> addEntityList, bool isCheckEntityExist, string delIDList);
       
        [ServiceContractDescription(Name = "分发规则生成下一样本号", Desc = "分发规则生成下一样本号", Url = "LabStarPreService.svc/LS_UDTO_GetLBTranRuleNextSampleNo", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_GetLBTranRuleNextSampleNo", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_GetLBTranRuleNextSampleNo(int? SampleNoSection,string SampleNoPrefix);

        [ServiceContractDescription(Name = "根据人员获取模块功能权限", Desc = "根据人员获取模块功能权限", Url = "LabStarPreService.svc/LS_UDTO_GetModuleFunRoleByEmpId", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LS_UDTO_GetModuleFunRoleByEmpId", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue LS_UDTO_GetModuleFunRoleByEmpId(string moduleid, string code);
        #endregion

        #region 从web.config获取程序地址
        [ServiceContractDescription(Name = "获取平台地址", Desc = "获取平台地址", Url = "LabStarPreService.svc/GetConfigLIIPUrl", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetConfigLIIPUrl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetConfigLIIPUrl();
     
        [ServiceContractDescription(Name = "获取报告打印程序地址", Desc = "获取报告打印程序地址", Url = "LabStarPreService.svc/GetConfigReportFormQueryPrintUrl", Get = "", Post = "", Return = "", ReturnType = "")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetConfigReportFormQueryPrintUrl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetConfigReportFormQueryPrintUrl();
        #endregion
    }
}
