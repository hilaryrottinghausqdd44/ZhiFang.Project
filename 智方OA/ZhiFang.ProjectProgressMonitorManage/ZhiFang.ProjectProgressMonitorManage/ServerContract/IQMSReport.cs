using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.Text;
using System.IO;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;

namespace ZhiFang.ProjectProgressMonitorManage.ServerContract
{

    [ServiceContract]
    public interface IQMSReport
    {
        #region EParameter

        [ServiceContractDescription(Name = "新增质量记录参数表表", Desc = "新增质量记录参数表表", Url = "QMSReport.svc/ST_UDTO_AddEParameter", Get = "", Post = "EParameter", Return = "BaseResultDataValue", ReturnType = "EParameter")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddEParameter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddEParameter(EParameter entity);

        [ServiceContractDescription(Name = "修改质量记录参数表表", Desc = "修改质量记录参数表表", Url = "QMSReport.svc/ST_UDTO_UpdateEParameter", Get = "", Post = "EParameter", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEParameter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEParameter(EParameter entity);

        [ServiceContractDescription(Name = "修改质量记录参数表表指定的属性", Desc = "修改质量记录参数表表指定的属性", Url = "QMSReport.svc/ST_UDTO_UpdateEParameterByField", Get = "", Post = "EParameter", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEParameterByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEParameterByField(EParameter entity, string fields);

        [ServiceContractDescription(Name = "删除质量记录参数表表", Desc = "删除质量记录参数表表", Url = "QMSReport.svc/ST_UDTO_DelEParameter?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelEParameter?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelEParameter(long id);

        [ServiceContractDescription(Name = "查询质量记录参数表表", Desc = "查询质量记录参数表表", Url = "QMSReport.svc/ST_UDTO_SearchEParameter", Get = "", Post = "EParameter", Return = "BaseResultList<EParameter>", ReturnType = "ListEParameter")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEParameter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEParameter(EParameter entity);

        [ServiceContractDescription(Name = "查询质量记录参数表表(HQL)", Desc = "查询质量记录参数表表(HQL)", Url = "QMSReport.svc/ST_UDTO_SearchEParameterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EParameter>", ReturnType = "ListEParameter")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEParameterByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEParameterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询质量记录参数表表", Desc = "通过主键ID查询质量记录参数表表", Url = "QMSReport.svc/ST_UDTO_SearchEParameterById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EParameter>", ReturnType = "EParameter")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEParameterById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEParameterById(long id, string fields, bool isPlanish);
        #endregion

        #region EEquip

        [ServiceContractDescription(Name = "新增仪器表", Desc = "新增仪器表", Url = "QMSReport.svc/ST_UDTO_AddEEquip", Get = "", Post = "EEquip", Return = "BaseResultDataValue", ReturnType = "EEquip")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddEEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddEEquip(EEquip entity);

        [ServiceContractDescription(Name = "修改仪器表", Desc = "修改仪器表", Url = "QMSReport.svc/ST_UDTO_UpdateEEquip", Get = "", Post = "EEquip", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEEquip(EEquip entity);

        [ServiceContractDescription(Name = "修改仪器表指定的属性", Desc = "修改仪器表指定的属性", Url = "QMSReport.svc/ST_UDTO_UpdateEEquipByField", Get = "", Post = "EEquip", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEEquipByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEEquipByField(EEquip entity, string fields);

        [ServiceContractDescription(Name = "删除仪器表", Desc = "删除仪器表", Url = "QMSReport.svc/ST_UDTO_DelEEquip?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelEEquip?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelEEquip(long id);

        [ServiceContractDescription(Name = "查询仪器表", Desc = "查询仪器表", Url = "QMSReport.svc/ST_UDTO_SearchEEquip", Get = "", Post = "EEquip", Return = "BaseResultList<EEquip>", ReturnType = "ListEEquip")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEEquip(EEquip entity);

        [ServiceContractDescription(Name = "查询仪器表(HQL)", Desc = "查询仪器表(HQL)", Url = "QMSReport.svc/ST_UDTO_SearchEEquipByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EEquip>", ReturnType = "ListEEquip")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEEquipByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEEquipByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询仪器表", Desc = "通过主键ID查询仪器表", Url = "QMSReport.svc/ST_UDTO_SearchEEquipById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EEquip>", ReturnType = "EEquip")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEEquipById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEEquipById(long id, string fields, bool isPlanish);
        #endregion

        #region EMaintenanceData

        [ServiceContractDescription(Name = "新增仪器维护数据表", Desc = "新增仪器维护数据表", Url = "QMSReport.svc/ST_UDTO_AddEMaintenanceData", Get = "", Post = "EMaintenanceData", Return = "BaseResultDataValue", ReturnType = "EMaintenanceData")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddEMaintenanceData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddEMaintenanceData(EMaintenanceData entity);

        [ServiceContractDescription(Name = "修改仪器维护数据表", Desc = "修改仪器维护数据表", Url = "QMSReport.svc/ST_UDTO_UpdateEMaintenanceData", Get = "", Post = "EMaintenanceData", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEMaintenanceData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEMaintenanceData(EMaintenanceData entity);

        [ServiceContractDescription(Name = "修改仪器维护数据表指定的属性", Desc = "修改仪器维护数据表指定的属性", Url = "QMSReport.svc/ST_UDTO_UpdateEMaintenanceDataByField", Get = "", Post = "EMaintenanceData", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEMaintenanceDataByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEMaintenanceDataByField(EMaintenanceData entity, string fields);

        [ServiceContractDescription(Name = "删除仪器维护数据表", Desc = "删除仪器维护数据表", Url = "QMSReport.svc/ST_UDTO_DelEMaintenanceData?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelEMaintenanceData?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelEMaintenanceData(long id);

        [ServiceContractDescription(Name = "查询仪器维护数据表", Desc = "查询仪器维护数据表", Url = "QMSReport.svc/ST_UDTO_SearchEMaintenanceData", Get = "", Post = "EMaintenanceData", Return = "BaseResultList<EMaintenanceData>", ReturnType = "ListEMaintenanceData")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEMaintenanceData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEMaintenanceData(EMaintenanceData entity);

        [ServiceContractDescription(Name = "查询仪器维护数据表(HQL)", Desc = "查询仪器维护数据表(HQL)", Url = "QMSReport.svc/ST_UDTO_SearchEMaintenanceDataByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EMaintenanceData>", ReturnType = "ListEMaintenanceData")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEMaintenanceDataByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEMaintenanceDataByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询仪器维护数据表", Desc = "通过主键ID查询仪器维护数据表", Url = "QMSReport.svc/ST_UDTO_SearchEMaintenanceDataById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EMaintenanceData>", ReturnType = "EMaintenanceData")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEMaintenanceDataById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEMaintenanceDataById(long id, string fields, bool isPlanish);
        #endregion

        #region EReportData

        [ServiceContractDescription(Name = "新增仪器报表数据表", Desc = "新增仪器报表数据表", Url = "QMSReport.svc/ST_UDTO_AddEReportData", Get = "", Post = "EReportData", Return = "BaseResultDataValue", ReturnType = "EReportData")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddEReportData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddEReportData(EReportData entity);

        [ServiceContractDescription(Name = "修改仪器报表数据表", Desc = "修改仪器报表数据表", Url = "QMSReport.svc/ST_UDTO_UpdateEReportData", Get = "", Post = "EReportData", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEReportData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEReportData(EReportData entity);

        [ServiceContractDescription(Name = "修改仪器报表数据表指定的属性", Desc = "修改仪器报表数据表指定的属性", Url = "QMSReport.svc/ST_UDTO_UpdateEReportDataByField", Get = "", Post = "EReportData", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEReportDataByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEReportDataByField(EReportData entity, string fields);

        [ServiceContractDescription(Name = "删除仪器报表数据表", Desc = "删除仪器报表数据表", Url = "QMSReport.svc/ST_UDTO_DelEReportData?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelEReportData?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelEReportData(long id);

        [ServiceContractDescription(Name = "查询仪器报表数据表", Desc = "查询仪器报表数据表", Url = "QMSReport.svc/ST_UDTO_SearchEReportData", Get = "", Post = "EReportData", Return = "BaseResultList<EReportData>", ReturnType = "ListEReportData")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEReportData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEReportData(EReportData entity);

        [ServiceContractDescription(Name = "查询仪器报表数据表(HQL)", Desc = "查询仪器报表数据表(HQL)", Url = "QMSReport.svc/ST_UDTO_SearchEReportDataByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EReportData>", ReturnType = "ListEReportData")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEReportDataByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEReportDataByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询仪器报表数据表", Desc = "通过主键ID查询仪器报表数据表", Url = "QMSReport.svc/ST_UDTO_SearchEReportDataById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EReportData>", ReturnType = "EReportData")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEReportDataById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEReportDataById(long id, string fields, bool isPlanish);
        #endregion

        #region ETemplet

        [ServiceContractDescription(Name = "新增仪器模板表", Desc = "新增仪器模板表", Url = "QMSReport.svc/ST_UDTO_AddETemplet", Get = "", Post = "ETemplet", Return = "BaseResultDataValue", ReturnType = "ETemplet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddETemplet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddETemplet(ETemplet entity);

        [ServiceContractDescription(Name = "修改仪器模板表", Desc = "修改仪器模板表", Url = "QMSReport.svc/ST_UDTO_UpdateETemplet", Get = "", Post = "ETemplet", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateETemplet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateETemplet(ETemplet entity);

        [ServiceContractDescription(Name = "修改仪器模板表指定的属性", Desc = "修改仪器模板表指定的属性", Url = "QMSReport.svc/ST_UDTO_UpdateETempletByField", Get = "", Post = "ETemplet", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateETempletByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateETempletByField(ETemplet entity, string fields);

        [ServiceContractDescription(Name = "删除仪器模板表", Desc = "删除仪器模板表", Url = "QMSReport.svc/ST_UDTO_DelETemplet?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelETemplet?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelETemplet(long id);

        [ServiceContractDescription(Name = "查询仪器模板表", Desc = "查询仪器模板表", Url = "QMSReport.svc/ST_UDTO_SearchETemplet", Get = "", Post = "ETemplet", Return = "BaseResultList<ETemplet>", ReturnType = "ListETemplet")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchETemplet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchETemplet(ETemplet entity);

        [ServiceContractDescription(Name = "查询仪器模板表(HQL)", Desc = "查询仪器模板表(HQL)", Url = "QMSReport.svc/ST_UDTO_SearchETempletByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ETemplet>", ReturnType = "ListETemplet")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchETempletByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchETempletByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询仪器模板表", Desc = "通过主键ID查询仪器模板表", Url = "QMSReport.svc/ST_UDTO_SearchETempletById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ETemplet>", ReturnType = "ETemplet")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchETempletById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchETempletById(long id, string fields, bool isPlanish);
        #endregion

        #region ETempletEmp

        [ServiceContractDescription(Name = "新增模板与员工关系表", Desc = "新增模板与员工关系表", Url = "QMSReport.svc/ST_UDTO_AddETempletEmp", Get = "", Post = "ETempletEmp", Return = "BaseResultDataValue", ReturnType = "ETempletEmp")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddETempletEmp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddETempletEmp(ETempletEmp entity);

        [ServiceContractDescription(Name = "修改模板与员工关系表", Desc = "修改模板与员工关系表", Url = "QMSReport.svc/ST_UDTO_UpdateETempletEmp", Get = "", Post = "ETempletEmp", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateETempletEmp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateETempletEmp(ETempletEmp entity);

        [ServiceContractDescription(Name = "修改模板与员工关系表指定的属性", Desc = "修改模板与员工关系表指定的属性", Url = "QMSReport.svc/ST_UDTO_UpdateETempletEmpByField", Get = "", Post = "ETempletEmp", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateETempletEmpByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateETempletEmpByField(ETempletEmp entity, string fields);

        [ServiceContractDescription(Name = "删除模板与员工关系表", Desc = "删除模板与员工关系表", Url = "QMSReport.svc/ST_UDTO_DelETempletEmp?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelETempletEmp?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelETempletEmp(long id);

        [ServiceContractDescription(Name = "查询模板与员工关系表", Desc = "查询模板与员工关系表", Url = "QMSReport.svc/ST_UDTO_SearchETempletEmp", Get = "", Post = "ETempletEmp", Return = "BaseResultList<ETempletEmp>", ReturnType = "ListETempletEmp")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchETempletEmp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchETempletEmp(ETempletEmp entity);

        [ServiceContractDescription(Name = "查询模板与员工关系表(HQL)", Desc = "查询模板与员工关系表(HQL)", Url = "QMSReport.svc/ST_UDTO_SearchETempletEmpByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ETempletEmp>", ReturnType = "ListETempletEmp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchETempletEmpByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchETempletEmpByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询模板与员工关系表", Desc = "通过主键ID查询模板与员工关系表", Url = "QMSReport.svc/ST_UDTO_SearchETempletEmpById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ETempletEmp>", ReturnType = "ETempletEmp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchETempletEmpById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchETempletEmpById(long id, string fields, bool isPlanish);
        #endregion

        #region EAttachment

        [ServiceContractDescription(Name = "新增文档附件表", Desc = "新增文档附件表", Url = "QMSReport.svc/ST_UDTO_AddEAttachment", Get = "", Post = "EAttachment", Return = "BaseResultDataValue", ReturnType = "EAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddEAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddEAttachment(EAttachment entity);

        [ServiceContractDescription(Name = "修改文档附件表", Desc = "修改文档附件表", Url = "QMSReport.svc/ST_UDTO_UpdateEAttachment", Get = "", Post = "EAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEAttachment(EAttachment entity);

        [ServiceContractDescription(Name = "修改文档附件表指定的属性", Desc = "修改文档附件表指定的属性", Url = "QMSReport.svc/ST_UDTO_UpdateEAttachmentByField", Get = "", Post = "EAttachment", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEAttachmentByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEAttachmentByField(EAttachment entity, string fields);

        [ServiceContractDescription(Name = "删除文档附件表", Desc = "删除文档附件表", Url = "QMSReport.svc/ST_UDTO_DelEAttachment?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelEAttachment?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelEAttachment(long id);

        [ServiceContractDescription(Name = "查询文档附件表", Desc = "查询文档附件表", Url = "QMSReport.svc/ST_UDTO_SearchEAttachment", Get = "", Post = "EAttachment", Return = "BaseResultList<EAttachment>", ReturnType = "ListEAttachment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEAttachment(EAttachment entity);

        [ServiceContractDescription(Name = "查询文档附件表(HQL)", Desc = "查询文档附件表(HQL)", Url = "QMSReport.svc/ST_UDTO_SearchEAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EAttachment>", ReturnType = "ListEAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEAttachmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询文档附件表", Desc = "通过主键ID查询文档附件表", Url = "QMSReport.svc/ST_UDTO_SearchEAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EAttachment>", ReturnType = "EAttachment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEAttachmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEAttachmentById(long id, string fields, bool isPlanish);
        #endregion

        #region EResEmp

        [ServiceContractDescription(Name = "新增职责与员工关系表", Desc = "新增职责与员工关系表", Url = "QMSReport.svc/ST_UDTO_AddEResEmp", Get = "", Post = "EResEmp", Return = "BaseResultDataValue", ReturnType = "EResEmp")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddEResEmp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddEResEmp(EResEmp entity);

        [ServiceContractDescription(Name = "修改职责与员工关系表", Desc = "修改职责与员工关系表", Url = "QMSReport.svc/ST_UDTO_UpdateEResEmp", Get = "", Post = "EResEmp", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEResEmp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEResEmp(EResEmp entity);

        [ServiceContractDescription(Name = "修改职责与员工关系表指定的属性", Desc = "修改职责与员工关系表指定的属性", Url = "QMSReport.svc/ST_UDTO_UpdateEResEmpByField", Get = "", Post = "EResEmp", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEResEmpByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEResEmpByField(EResEmp entity, string fields);

        [ServiceContractDescription(Name = "删除职责与员工关系表", Desc = "删除职责与员工关系表", Url = "QMSReport.svc/ST_UDTO_DelEResEmp?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelEResEmp?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelEResEmp(long id);

        [ServiceContractDescription(Name = "查询职责与员工关系表", Desc = "查询职责与员工关系表", Url = "QMSReport.svc/ST_UDTO_SearchEResEmp", Get = "", Post = "EResEmp", Return = "BaseResultList<EResEmp>", ReturnType = "ListEResEmp")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEResEmp", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEResEmp(EResEmp entity);

        [ServiceContractDescription(Name = "查询职责与员工关系表(HQL)", Desc = "查询职责与员工关系表(HQL)", Url = "QMSReport.svc/ST_UDTO_SearchEResEmpByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EResEmp>", ReturnType = "ListEResEmp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEResEmpByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEResEmpByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询职责与员工关系表", Desc = "通过主键ID查询职责与员工关系表", Url = "QMSReport.svc/ST_UDTO_SearchEResEmpById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EResEmp>", ReturnType = "EResEmp")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEResEmpById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEResEmpById(long id, string fields, bool isPlanish);
        #endregion

        #region EResponsibility

        [ServiceContractDescription(Name = "新增质量记录人员职责表", Desc = "新增质量记录人员职责表", Url = "QMSReport.svc/ST_UDTO_AddEResponsibility", Get = "", Post = "EResponsibility", Return = "BaseResultDataValue", ReturnType = "EResponsibility")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddEResponsibility", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddEResponsibility(EResponsibility entity);

        [ServiceContractDescription(Name = "修改质量记录人员职责表", Desc = "修改质量记录人员职责表", Url = "QMSReport.svc/ST_UDTO_UpdateEResponsibility", Get = "", Post = "EResponsibility", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEResponsibility", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEResponsibility(EResponsibility entity);

        [ServiceContractDescription(Name = "修改质量记录人员职责表指定的属性", Desc = "修改质量记录人员职责表指定的属性", Url = "QMSReport.svc/ST_UDTO_UpdateEResponsibilityByField", Get = "", Post = "EResponsibility", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateEResponsibilityByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateEResponsibilityByField(EResponsibility entity, string fields);

        [ServiceContractDescription(Name = "删除质量记录人员职责表", Desc = "删除质量记录人员职责表", Url = "QMSReport.svc/ST_UDTO_DelEResponsibility?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelEResponsibility?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelEResponsibility(long id);

        [ServiceContractDescription(Name = "查询质量记录人员职责表", Desc = "查询质量记录人员职责表", Url = "QMSReport.svc/ST_UDTO_SearchEResponsibility", Get = "", Post = "EResponsibility", Return = "BaseResultList<EResponsibility>", ReturnType = "ListEResponsibility")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEResponsibility", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEResponsibility(EResponsibility entity);

        [ServiceContractDescription(Name = "查询质量记录人员职责表(HQL)", Desc = "查询质量记录人员职责表(HQL)", Url = "QMSReport.svc/ST_UDTO_SearchEResponsibilityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EResponsibility>", ReturnType = "ListEResponsibility")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEResponsibilityByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEResponsibilityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询质量记录人员职责表", Desc = "通过主键ID查询质量记录人员职责表", Url = "QMSReport.svc/ST_UDTO_SearchEResponsibilityById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EResponsibility>", ReturnType = "EResponsibility")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchEResponsibilityById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchEResponsibilityById(long id, string fields, bool isPlanish);
        #endregion

        #region ETempletRes

        [ServiceContractDescription(Name = "新增模板与职责关系表", Desc = "新增模板与职责关系表", Url = "QMSReport.svc/ST_UDTO_AddETempletRes", Get = "", Post = "ETempletRes", Return = "BaseResultDataValue", ReturnType = "ETempletRes")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_AddETempletRes", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_AddETempletRes(ETempletRes entity);

        [ServiceContractDescription(Name = "修改模板与职责关系表", Desc = "修改模板与职责关系表", Url = "QMSReport.svc/ST_UDTO_UpdateETempletRes", Get = "", Post = "ETempletRes", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateETempletRes", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateETempletRes(ETempletRes entity);

        [ServiceContractDescription(Name = "修改模板与职责关系表指定的属性", Desc = "修改模板与职责关系表指定的属性", Url = "QMSReport.svc/ST_UDTO_UpdateETempletResByField", Get = "", Post = "ETempletRes", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_UpdateETempletResByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_UpdateETempletResByField(ETempletRes entity, string fields);

        [ServiceContractDescription(Name = "删除模板与职责关系表", Desc = "删除模板与职责关系表", Url = "QMSReport.svc/ST_UDTO_DelETempletRes?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/ST_UDTO_DelETempletRes?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool ST_UDTO_DelETempletRes(long id);

        [ServiceContractDescription(Name = "查询模板与职责关系表", Desc = "查询模板与职责关系表", Url = "QMSReport.svc/ST_UDTO_SearchETempletRes", Get = "", Post = "ETempletRes", Return = "BaseResultList<ETempletRes>", ReturnType = "ListETempletRes")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchETempletRes", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchETempletRes(ETempletRes entity);

        [ServiceContractDescription(Name = "查询模板与职责关系表(HQL)", Desc = "查询模板与职责关系表(HQL)", Url = "QMSReport.svc/ST_UDTO_SearchETempletResByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ETempletRes>", ReturnType = "ListETempletRes")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchETempletResByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchETempletResByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询模板与职责关系表", Desc = "通过主键ID查询模板与职责关系表", Url = "QMSReport.svc/ST_UDTO_SearchETempletResById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ETempletRes>", ReturnType = "ETempletRes")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ST_UDTO_SearchETempletResById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ST_UDTO_SearchETempletResById(long id, string fields, bool isPlanish);
        #endregion

        [ServiceContractDescription(Name = "新增仪器维护数据表", Desc = "新增仪器维护数据表", Url = "QMSReport.svc/QMS_UDTO_AddEMaintenanceData", Get = "", Post = "templetID,itemDate,typeCode,templetBatNo,entityList", Return = "BaseResultDataValue", ReturnType = "EMaintenanceData")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_AddEMaintenanceData", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_AddEMaintenanceData(long templetID, string itemDate, string typeCode, string templetBatNo, IList<EMaintenanceData> entityList);

        [ServiceContractDescription(Name = "质量记录数据保存", Desc = "质量记录数据保存", Url = "QMSReport.svc/QMS_UDTO_AddEMaintenanceDataAndResult", Get = "", Post = "templetID,itemDate,templetBatNo,entityList,fields,isPlanish", Return = "BaseResultDataValue", ReturnType = "EMaintenanceData")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_AddEMaintenanceDataAndResult", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_AddEMaintenanceDataAndResult(long templetID, string itemDate, string templetBatNo, IList<EMaintenanceData> entityList, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "查询模板列表数据", Desc = "查询模板列表数据", Url = "QMSReport.svc/QMS_UDTO_SearchMaintenanceDataTB?templetID={templetID}&typeCode={typeCode}&beginDate={beginDate}&endDate={endDate}&isLoadBeforeData={isLoadBeforeData}", Get = "templetID={templetID}&typeCode={typeCode}&beginDate={beginDate}&endDate={endDate}&isLoadBeforeData={isLoadBeforeData}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QMS_UDTO_SearchMaintenanceDataTB?templetID={templetID}&typeCode={typeCode}&beginDate={beginDate}&endDate={endDate}&isLoadBeforeData={isLoadBeforeData}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchMaintenanceDataTB(long templetID, string typeCode, string beginDate, string endDate, int isLoadBeforeData);

        [ServiceContractDescription(Name = "删除模板列表数据", Desc = "删除模板列表数据", Url = "QMSReport.svc/QMS_UDTO_DelMaintenanceDataTB?templetID={templetID}&typeCode={typeCode}&itemDate={itemDate}&batchNumber={batchNumber}", Get = "templetID={templetID}&typeCode={typeCode}&itemDate={itemDate}&batchNumber={batchNumber}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QMS_UDTO_DelMaintenanceDataTB?templetID={templetID}&typeCode={typeCode}&itemDate={itemDate}&batchNumber={batchNumber}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_DelMaintenanceDataTB(long templetID, string typeCode, string itemDate, string batchNumber);

        [ServiceContractDescription(Name = "新增仪器Excel模板", Desc = "新增仪器Excel模板", Url = "QMSReport.svc/QMS_UDTO_AddExcelTemplet", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_AddExcelTemplet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message QMS_UDTO_AddExcelTemplet();

        [ServiceContractDescription(Name = "修改仪器Excel模板", Desc = "修改仪器Excel模板", Url = "QMSReport.svc/QMS_UDTO_UpdateExcelTemplet", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UpdateExcelTemplet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message QMS_UDTO_UpdateExcelTemplet();


        [ServiceContractDescription(Name = "删除仪器Excel模板及相关数据", Desc = "删除仪器Excel模板及相关数据", Url = "QMSReport.svc/QMS_UDTO_DelETemplet?id={id}&isDelTempletData={isDelTempletData}", Get = "id={id}&isDelTempletData={isDelTempletData}", Post = "", Return = "BaseResultBool", ReturnType = "BaseResultBool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/QMS_UDTO_DelETemplet?id={id}&isDelTempletData={isDelTempletData}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool QMS_UDTO_DelETemplet(long id, bool isDelTempletData);

        [ServiceContractDescription(Name = "删除模板相关数据", Desc = "删除模板相关数据", Url = "QMSReport.svc/QMS_UDTO_DelETempletData?templetID={templetID}&templetBatNo={templetBatNo}&beginDate={beginDate}&endDate={endDate}", Get = "templetID={templetID}&templetBatNo={templetBatNo}&beginDate={beginDate}&endDate={endDate}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_DelETempletData?templetID={templetID}&templetBatNo={templetBatNo}&beginDate={beginDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_DelETempletData(long templetID, string templetBatNo, string beginDate, string endDate);

        [ServiceContractDescription(Name = "下载仪器质量记录模板", Desc = "下载仪器质量记录模板", Url = "QMSReport.svc/QMS_UDTO_GetExcelTemplet?templetID={templetID}&operateType={operateType}", Get = "templetID={templetID}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/QMS_UDTO_GetExcelTemplet?templetID={templetID}&operateType={operateType}")]
        [OperationContract]
        Stream QMS_UDTO_GetExcelTemplet(long templetID, int operateType);

        [ServiceContractDescription(Name = "获取模板多次记录数据", Desc = "获取模板多次记录数据", Url = "QMSReport.svc/QMS_UDTO_SearchTempletGroupData?templetID={templetID}&beginDate={beginDate}&endDate={endDate}", Get = "templetID={templetID}&beginDate={beginDate}&endDate={endDate}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchTempletGroupData?templetID={templetID}&beginDate={beginDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchTempletGroupData(long templetID, string beginDate, string endDate);

        [ServiceContractDescription(Name = "获取模板报表数据", Desc = "获取模板报表数据", Url = "QMSReport.svc/QMS_UDTO_SearchReportGroupData?reportDataID={reportDataID}&templetID={templetID}&beginDate={beginDate}&endDate={endDate}", Get = "reportDataID={reportDataID}&templetID={templetID}&beginDate={beginDate}&endDate={endDate}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchReportGroupData?reportDataID={reportDataID}&templetID={templetID}&beginDate={beginDate}&endDate={endDate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchReportGroupData(long reportDataID, long templetID, string beginDate, string endDate);

        [ServiceContractDescription(Name = "预览仪器质量记录模板", Desc = "预览仪器质量记录模板", Url = "QMSReport.svc/QMS_UDTO_PreviewExcelTemplet?templetID={templetID}&operateType={operateType}", Get = "templetID={templetID}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/QMS_UDTO_PreviewExcelTemplet?templetID={templetID}&operateType={operateType}")]
        [OperationContract]
        Stream QMS_UDTO_PreviewExcelTemplet(long templetID, int operateType);

        [ServiceContractDescription(Name = "预览仪器质量记录PDF文件", Desc = "预览仪器质量记录PDF文件", Url = "QMSReport.svc/QMS_UDTO_PreviewPdf?templetID={templetID}&reportName={reportName}&beginDate={beginDate}&endDate={endDate}&templetBatNo={templetBatNo}&operateType={operateType}&isCheckPreview={isCheckPreview}", Get = "templetID={templetID}&reportName={reportName}&beginDate={beginDate}&endDate={endDate}&templetBatNo={templetBatNo}&operateType={operateType}&isCheckPreview={isCheckPreview}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/QMS_UDTO_PreviewPdf?templetID={templetID}&reportName={reportName}&beginDate={beginDate}&endDate={endDate}&templetBatNo={templetBatNo}&operateType={operateType}&isCheckPreview={isCheckPreview}")]
        [OperationContract]
        Stream QMS_UDTO_PreviewPdf(long templetID, string reportName, string beginDate, string endDate, string templetBatNo, int operateType, int isCheckPreview);

        [ServiceContractDescription(Name = "浏览已审核仪器质量记录PDF文件", Desc = "浏览已审核仪器质量记录PDF文件", Url = "QMSReport.svc/QMS_UDTO_PreviewCheckPdf?reportDataID={reportDataID}&reportName={reportName}&operateType={operateType}", Get = "reportDataID={reportDataID}&reportName={reportName}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/QMS_UDTO_PreviewCheckPdf?reportDataID={reportDataID}&reportName={reportName}&operateType={operateType}")]
        [OperationContract]
        Stream QMS_UDTO_PreviewCheckPdf(long reportDataID, string reportName, int operateType);

        //[ServiceContractDescription(Name = "查询审核的质量记录", Desc = "查询审核的质量记录", Url = "QMSReport.svc/QMS_UDTO_SearchWillCheckRecord?beginDate={beginDate}&endDate={endDate}&fields={fields}&isPlanish={isPlanish}&page={page}&limit={limit}", Get = "beginDate={beginDate}&endDate={endDate}&fields={fields}&isPlanish={isPlanish}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchWillCheckRecord?beginDate={beginDate}&endDate={endDate}&fields={fields}&isPlanish={isPlanish}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue QMS_UDTO_SearchWillCheckRecord(string beginDate, string endDate, string fields, bool isPlanish, int page, int limit);

        [ServiceContractDescription(Name = "查询审核的质量记录", Desc = "查询审核的质量记录", Url = "QMSReport.svc/QMS_UDTO_SearchWillCheckRecord?templetType={templetType}&templetID={templetID}&equipID={equipID}&beginDate={beginDate}&endDate={endDate}&CheckType={CheckType}&otherPara={otherPara}&fields={fields}&isPlanish={isPlanish}&page={page}&limit={limit}", Get = "templetType={templetType}&templetID={templetID}&equipID={equipID}&beginDate={beginDate}&endDate={endDate}&CheckType={CheckType}&otherPara={otherPara}&fields={fields}&isPlanish={isPlanish}&page={page}&limit={limit}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchWillCheckRecord?templetType={templetType}&templetID={templetID}&equipID={equipID}&beginDate={beginDate}&endDate={endDate}&CheckType={CheckType}&otherPara={otherPara}&fields={fields}&isPlanish={isPlanish}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchWillCheckRecord(int templetType, string templetID, string equipID, string beginDate, string endDate, int checkType, string otherPara, string fields, bool isPlanish, int page, int limit);

        [ServiceContractDescription(Name = "审核质量记录", Desc = "审核质量记录", Url = "QMSReport.svc/QMS_UDTO_CheckReport?templetID={templetID}&beginDate={beginDate}&endDate={endDate}&templetBatNo={templetBatNo}&checkView={checkView}", Get = "templetID={templetID}&beginDate={beginDate}&endDate={endDate}&templetBatNo={templetBatNo}&checkView={checkView}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_CheckReport?templetID={templetID}&beginDate={beginDate}&endDate={endDate}&templetBatNo={templetBatNo}&checkView={checkView}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_CheckReport(long templetID, string beginDate, string endDate, string templetBatNo, string checkView);

        [ServiceContractDescription(Name = "反审质量记录", Desc = "反审质量记录", Url = "QMSReport.svc/QMS_UDTO_CheckReportCancel?reportID={reportID}&cancelCheckView={cancelCheckView}", Get = "reportID={reportID}&cancelCheckView={cancelCheckView}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_CheckReportCancel?reportID={reportID}&cancelCheckView={cancelCheckView}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_CheckReportCancel(long reportID, string cancelCheckView);

        [ServiceContractDescription(Name = "查询小组的质量记录模板列表(包含子小组)", Desc = "查询小组的质量记录模板列表(包含子小组)", Url = "QMSReport.svc/QMS_UDTO_GetETempletByHRDeptID?where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Get = "where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_GetETempletByHRDeptID?where={where}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_GetETempletByHRDeptID(string where, int limit, int page, bool isPlanish, string fields, string sort);

        [ServiceContractDescription(Name = "预览仪器质量记录附件", Desc = "预览仪器质量记录附件", Url = "QMSReport.svc/QMS_UDTO_PreviewTempletAttachment?eattachmentID={eattachmentID}&operateType={operateType}", Get = "eattachmentID={eattachmentID}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/QMS_UDTO_PreviewTempletAttachment?eattachmentID={eattachmentID}&operateType={operateType}")]
        [OperationContract]
        Stream QMS_UDTO_PreviewTempletAttachment(long eattachmentID, int operateType);

        [ServiceContractDescription(Name = "上传仪器质量记录附件", Desc = "上传仪器质量记录附件", Url = "QMSReport.svc/QMS_UDTO_UploadTempletAttachment", Get = "", Post = "", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_UploadTempletAttachment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message QMS_UDTO_UploadTempletAttachment();

        [ServiceContractDescription(Name = "查询仪器报表数据表(HQL)", Desc = "查询仪器报表数据表(HQL)", Url = "QMSReport.svc/QMS_UDTO_SearchEReportDataByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EReportData>", ReturnType = "ListEReportData")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchEReportDataByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchEReportDataByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询质量记录登记模板信息", Desc = "查询质量记录登记模板信息", Url = "QMSReport.svc/QMS_UDTO_SearchTempletByEmp?relationType={relationType}&empID={empID}&where={where}&resWhere={resWhere}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Get = "relationType={relationType}&empID={empID}&where={where}&resWhere={resWhere}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchTempletByEmp?relationType={relationType}&empID={empID}&where={where}&resWhere={resWhere}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchTempletByEmp(int relationType, long empID, string where, string resWhere, int page, int limit, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询质量记录登记模板信息", Desc = "查询质量记录登记模板信息", Url = "QMSReport.svc/QMS_UDTO_SearchTempletByEmpAndTempletDate?relationType={relationType}&empID={empID}&templetDate={templetDate}&where={where}&resWhere={resWhere}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Get = "relationType={relationType}&empID={empID}&templetDate={templetDate}&where={where}&resWhere={resWhere}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchTempletByEmpAndTempletDate?relationType={relationType}&empID={empID}&templetDate={templetDate}&where={where}&resWhere={resWhere}&page={page}&limit={limit}&fields={fields}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchTempletByEmpAndTempletDate(int relationType, long empID, string templetDate, string where, string resWhere, int page, int limit, string fields, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "新增和修改质量记录参数", Desc = "新增和修改质量记录参数", Url = "QMSReport.svc/QMS_UDTO_AddEParameter", Get = "", Post = "EParameter", Return = "BaseResultDataValue", ReturnType = "EParameter")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_AddEParameter", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_AddEParameter(EParameter entity);

        [ServiceContractDescription(Name = "查询仪器维护数据表", Desc = "查询仪器维护数据表", Url = "QMSReport.svc/QMS_UDTO_SearchEMaintenanceData?templetID={templetID}&itemDate={itemDate}&typeCode={typeCode}&templetBatNo={templetBatNo}&isLoadBeforeData={isLoadBeforeData}&fields={fields}&isPlanish={isPlanish}", Get = "templetID={templetID}&itemDate={itemDate}&typeCode={typeCode}&templetBatNo={templetBatNo}&isLoadBeforeData={isLoadBeforeData}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_SearchEMaintenanceData?templetID={templetID}&itemDate={itemDate}&typeCode={typeCode}&templetBatNo={templetBatNo}&isLoadBeforeData={isLoadBeforeData}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_SearchEMaintenanceData(long templetID, string itemDate, string typeCode, string templetBatNo, int isLoadBeforeData, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "获取模板审核和数据状态", Desc = "获取模板审核和数据状态", Url = "QMSReport.svc/QMS_UDTO_GetTempletState?templetID={templetID}&itemDate={itemDate}&fields={fields}&isPlanish={isPlanish}", Get = "templetID={templetID}&itemDate={itemDate}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_GetTempletState?templetID={templetID}&itemDate={itemDate}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue QMS_UDTO_GetTempletState(long templetID, string itemDate, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "导出Excel文件路径", Desc = "导出Excel文件路径", Url = "QMSReport.svc/QMS_UDTO_GetReportDetailExcelPath", Get = "", Post = "reportType,idList,where,isHeader", Return = "Message", ReturnType = "Message")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/QMS_UDTO_GetReportDetailExcelPath", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        Message QMS_UDTO_GetReportDetailExcelPath();

        [ServiceContractDescription(Name = "下载Excel文件", Desc = "下载Excel文件", Url = "QMSReport.svc/QMS_UDTO_DownLoadExcel?fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}", Get = "fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}", Post = "", Return = "Stream", ReturnType = "Stream")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedResponse, UriTemplate = "/QMS_UDTO_DownLoadExcel?fileName={fileName}&downFileName={downFileName}&isUpLoadFile={isUpLoadFile}&operateType={operateType}")]
        [OperationContract]
        Stream QMS_UDTO_DownLoadExcel(string fileName, string downFileName, int isUpLoadFile, int operateType);
    }
}
