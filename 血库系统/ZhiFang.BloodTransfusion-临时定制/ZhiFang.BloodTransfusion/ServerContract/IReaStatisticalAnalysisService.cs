using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.IO;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.BloodTransfusion;
using ZhiFang.Entity.RBAC;

namespace ZhiFang.BloodTransfusion.ServerContract
{
    [ServiceContract]
    public interface IReaStatisticalAnalysisService
    {
        [ServiceContractDescription(Name = "", Desc = "", Url = "ReaStatisticalAnalysisService.svc/Test", Get = "", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/Test", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue Test();

        #region SAConfigModule

        [ServiceContractDescription(Name = "新增SA_ConfigModule", Desc = "新增SA_ConfigModule", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_AddSAConfigModule", Get = "", Post = "SAConfigModule", Return = "BaseResultDataValue", ReturnType = "SAConfigModule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddSAConfigModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddSAConfigModule(SAConfigModule entity);

        [ServiceContractDescription(Name = "修改SA_ConfigModule", Desc = "修改SA_ConfigModule", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_UpdateSAConfigModule", Get = "", Post = "SAConfigModule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateSAConfigModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateSAConfigModule(SAConfigModule entity);

        [ServiceContractDescription(Name = "修改SA_ConfigModule指定的属性", Desc = "修改SA_ConfigModule指定的属性", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_UpdateSAConfigModuleByField", Get = "", Post = "SAConfigModule", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdateSAConfigModuleByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdateSAConfigModuleByField(SAConfigModule entity, string fields);

        [ServiceContractDescription(Name = "删除SA_ConfigModule", Desc = "删除SA_ConfigModule", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_DelSAConfigModule?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RS_UDTO_DelSAConfigModule?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_DelSAConfigModule(long id);

        [ServiceContractDescription(Name = "查询SA_ConfigModule", Desc = "查询SA_ConfigModule", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSAConfigModule", Get = "", Post = "SAConfigModule", Return = "BaseResultList<SAConfigModule>", ReturnType = "ListSAConfigModule")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSAConfigModule", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSAConfigModule(SAConfigModule entity);

        [ServiceContractDescription(Name = "查询SA_ConfigModule(HQL)", Desc = "查询SA_ConfigModule(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSAConfigModuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SAConfigModule>", ReturnType = "ListSAConfigModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSAConfigModuleByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSAConfigModuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询SA_ConfigModule", Desc = "通过主键ID查询SA_ConfigModule", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSAConfigModuleById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SAConfigModule>", ReturnType = "SAConfigModule")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSAConfigModuleById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSAConfigModuleById(long id, string fields, bool isPlanish);
        #endregion

        #region AgeUnit

        [ServiceContractDescription(Name = "查询AgeUnit", Desc = "查询AgeUnit", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchAgeUnit", Get = "", Post = "AgeUnit", Return = "BaseResultList<AgeUnit>", ReturnType = "ListAgeUnit")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchAgeUnit", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchAgeUnit(AgeUnit entity);

        [ServiceContractDescription(Name = "查询AgeUnit(HQL)", Desc = "查询AgeUnit(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchAgeUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AgeUnit>", ReturnType = "ListAgeUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchAgeUnitByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchAgeUnitByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询AgeUnit", Desc = "通过主键ID查询AgeUnit", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchAgeUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<AgeUnit>", ReturnType = "AgeUnit")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchAgeUnitById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchAgeUnitById(long id, string fields, bool isPlanish);
        #endregion

        #region Department

        [ServiceContractDescription(Name = "查询Department", Desc = "查询Department", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchDepartment", Get = "", Post = "Department", Return = "BaseResultList<Department>", ReturnType = "ListDepartment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchDepartment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchDepartment(Department entity);

        [ServiceContractDescription(Name = "查询Department(HQL)", Desc = "查询Department(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchDepartmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Department>", ReturnType = "ListDepartment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchDepartmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchDepartmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Department", Desc = "通过主键ID查询Department", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchDepartmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Department>", ReturnType = "Department")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchDepartmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchDepartmentById(int id, string fields, bool isPlanish);
        #endregion

        #region District

        [ServiceContractDescription(Name = "查询District", Desc = "查询District", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchDistrict", Get = "", Post = "District", Return = "BaseResultList<District>", ReturnType = "ListDistrict")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchDistrict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchDistrict(District entity);

        [ServiceContractDescription(Name = "查询District(HQL)", Desc = "查询District(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchDistrictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<District>", ReturnType = "ListDistrict")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchDistrictByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchDistrictByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询District", Desc = "通过主键ID查询District", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchDistrictById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<District>", ReturnType = "District")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchDistrictById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchDistrictById(long id, string fields, bool isPlanish);
        #endregion

        #region Doctor

        [ServiceContractDescription(Name = "查询Doctor", Desc = "查询Doctor", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchDoctor", Get = "", Post = "Doctor", Return = "BaseResultList<Doctor>", ReturnType = "ListDoctor")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchDoctor", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchDoctor(Doctor entity);

        [ServiceContractDescription(Name = "查询Doctor(HQL)", Desc = "查询Doctor(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchDoctorByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Doctor>", ReturnType = "ListDoctor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchDoctorByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchDoctorByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Doctor", Desc = "通过主键ID查询Doctor", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchDoctorById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Doctor>", ReturnType = "Doctor")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchDoctorById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchDoctorById(int id, string fields, bool isPlanish);
        #endregion

        #region Employee

        [ServiceContractDescription(Name = "查询Employee", Desc = "查询Employee", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchEmployee", Get = "", Post = "Employee", Return = "BaseResultList<Employee>", ReturnType = "ListEmployee")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchEmployee", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchEmployee(Employee entity);

        [ServiceContractDescription(Name = "查询Employee(HQL)", Desc = "查询Employee(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchEmployeeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Employee>", ReturnType = "ListEmployee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchEmployeeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchEmployeeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Employee", Desc = "通过主键ID查询Employee", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchEmployeeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Employee>", ReturnType = "Employee")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchEmployeeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchEmployeeById(long id, string fields, bool isPlanish);
        #endregion

        #region EquipItem

        [ServiceContractDescription(Name = "查询EquipItem", Desc = "查询EquipItem", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchEquipItem", Get = "", Post = "EquipItem", Return = "BaseResultList<EquipItem>", ReturnType = "ListEquipItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchEquipItem(EquipItem entity);

        [ServiceContractDescription(Name = "查询EquipItem(HQL)", Desc = "查询EquipItem(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EquipItem>", ReturnType = "ListEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchEquipItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询EquipItem", Desc = "通过主键ID查询EquipItem", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchEquipItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<EquipItem>", ReturnType = "EquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchEquipItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchEquipItemById(long id, string fields, bool isPlanish);
        #endregion

        #region Equipment

        [ServiceContractDescription(Name = "查询Equipment", Desc = "查询Equipment", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchEquipment", Get = "", Post = "Equipment", Return = "BaseResultList<Equipment>", ReturnType = "ListEquipment")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchEquipment", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchEquipment(Equipment entity);

        [ServiceContractDescription(Name = "查询Equipment(HQL)", Desc = "查询Equipment(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchEquipmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Equipment>", ReturnType = "ListEquipment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchEquipmentByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchEquipmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Equipment", Desc = "通过主键ID查询Equipment", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchEquipmentById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Equipment>", ReturnType = "Equipment")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchEquipmentById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchEquipmentById(long id, string fields, bool isPlanish);
        #endregion

        #region FolkType

        [ServiceContractDescription(Name = "查询FolkType", Desc = "查询FolkType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchFolkType", Get = "", Post = "FolkType", Return = "BaseResultList<FolkType>", ReturnType = "ListFolkType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchFolkType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchFolkType(FolkType entity);

        [ServiceContractDescription(Name = "查询FolkType(HQL)", Desc = "查询FolkType(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchFolkTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FolkType>", ReturnType = "ListFolkType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchFolkTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchFolkTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询FolkType", Desc = "通过主键ID查询FolkType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchFolkTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<FolkType>", ReturnType = "FolkType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchFolkTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchFolkTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region GenderType

        [ServiceContractDescription(Name = "查询GenderType", Desc = "查询GenderType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchGenderType", Get = "", Post = "GenderType", Return = "BaseResultList<GenderType>", ReturnType = "ListGenderType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchGenderType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchGenderType(GenderType entity);

        [ServiceContractDescription(Name = "查询GenderType(HQL)", Desc = "查询GenderType(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchGenderTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GenderType>", ReturnType = "ListGenderType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchGenderTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchGenderTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询GenderType", Desc = "通过主键ID查询GenderType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchGenderTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GenderType>", ReturnType = "GenderType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchGenderTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchGenderTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region GroupItem

        [ServiceContractDescription(Name = "查询GroupItem", Desc = "查询GroupItem", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchGroupItem", Get = "", Post = "GroupItem", Return = "BaseResultList<GroupItem>", ReturnType = "ListGroupItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchGroupItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchGroupItem(GroupItem entity);

        [ServiceContractDescription(Name = "查询GroupItem(HQL)", Desc = "查询GroupItem(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchGroupItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GroupItem>", ReturnType = "ListGroupItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchGroupItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchGroupItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询GroupItem", Desc = "通过主键ID查询GroupItem", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchGroupItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<GroupItem>", ReturnType = "GroupItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchGroupItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchGroupItemById(long id, string fields, bool isPlanish);
        #endregion

        #region ItemRange

        [ServiceContractDescription(Name = "查询ItemRange", Desc = "查询ItemRange", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchItemRange", Get = "", Post = "ItemRange", Return = "BaseResultList<ItemRange>", ReturnType = "ListItemRange")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchItemRange", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchItemRange(ItemRange entity);

        [ServiceContractDescription(Name = "查询ItemRange(HQL)", Desc = "查询ItemRange(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchItemRangeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemRange>", ReturnType = "ListItemRange")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchItemRangeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchItemRangeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询ItemRange", Desc = "通过主键ID查询ItemRange", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchItemRangeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemRange>", ReturnType = "ItemRange")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchItemRangeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchItemRangeById(long id, string fields, bool isPlanish);
        #endregion

        #region ItemType

        [ServiceContractDescription(Name = "查询ItemType", Desc = "查询ItemType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchItemType", Get = "", Post = "ItemType", Return = "BaseResultList<ItemType>", ReturnType = "ListItemType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchItemType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchItemType(ItemType entity);

        [ServiceContractDescription(Name = "查询ItemType(HQL)", Desc = "查询ItemType(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchItemTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemType>", ReturnType = "ListItemType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchItemTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchItemTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询ItemType", Desc = "通过主键ID查询ItemType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchItemTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<ItemType>", ReturnType = "ItemType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchItemTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchItemTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region PGroup

        [ServiceContractDescription(Name = "查询PGroup", Desc = "查询PGroup", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPGroup", Get = "", Post = "PGroup", Return = "BaseResultList<PGroup>", ReturnType = "ListPGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPGroup(PGroup entity);

        [ServiceContractDescription(Name = "查询PGroup(HQL)", Desc = "查询PGroup(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PGroup>", ReturnType = "ListPGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询PGroup", Desc = "通过主键ID查询PGroup", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PGroup>", ReturnType = "PGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPGroupById(long id, string fields, bool isPlanish);
        #endregion

        #region PUser

        [ServiceContractDescription(Name = "查询PUser", Desc = "查询PUser", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPUser", Get = "", Post = "PUser", Return = "BaseResultList<PUser>", ReturnType = "ListPUser")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPUser(PUser entity);

        [ServiceContractDescription(Name = "查询PUser(HQL)", Desc = "查询PUser(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "ListPUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPUserByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询PUser", Desc = "通过主键ID查询PUser", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPUserById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PUser>", ReturnType = "PUser")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPUserById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPUserById(long id, string fields, bool isPlanish);
        #endregion

        #region SampleType

        [ServiceContractDescription(Name = "查询SampleType", Desc = "查询SampleType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSampleType", Get = "", Post = "SampleType", Return = "BaseResultList<SampleType>", ReturnType = "ListSampleType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSampleType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSampleType(SampleType entity);

        [ServiceContractDescription(Name = "查询SampleType(HQL)", Desc = "查询SampleType(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SampleType>", ReturnType = "ListSampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSampleTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSampleTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询SampleType", Desc = "通过主键ID查询SampleType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSampleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SampleType>", ReturnType = "SampleType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSampleTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSampleTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region SickType

        [ServiceContractDescription(Name = "查询SickType", Desc = "查询SickType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSickType", Get = "", Post = "SickType", Return = "BaseResultList<SickType>", ReturnType = "ListSickType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSickType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSickType(SickType entity);

        [ServiceContractDescription(Name = "查询SickType(HQL)", Desc = "查询SickType(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSickTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SickType>", ReturnType = "ListSickType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSickTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSickTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询SickType", Desc = "通过主键ID查询SickType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSickTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SickType>", ReturnType = "SickType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSickTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSickTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region SuperGroup

        [ServiceContractDescription(Name = "查询SuperGroup", Desc = "查询SuperGroup", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSuperGroup", Get = "", Post = "SuperGroup", Return = "BaseResultList<SuperGroup>", ReturnType = "ListSuperGroup")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSuperGroup", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSuperGroup(SuperGroup entity);

        [ServiceContractDescription(Name = "查询SuperGroup(HQL)", Desc = "查询SuperGroup(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSuperGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SuperGroup>", ReturnType = "ListSuperGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSuperGroupByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSuperGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询SuperGroup", Desc = "通过主键ID查询SuperGroup", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSuperGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SuperGroup>", ReturnType = "SuperGroup")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSuperGroupById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSuperGroupById(long id, string fields, bool isPlanish);
        #endregion

        #region TestEquip

        [ServiceContractDescription(Name = "查询TestEquip", Desc = "查询TestEquip", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTestEquip", Get = "", Post = "TestEquip", Return = "BaseResultList<TestEquip>", ReturnType = "ListTestEquip")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchTestEquip", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchTestEquip(TestEquip entity);

        [ServiceContractDescription(Name = "查询TestEquip(HQL)", Desc = "查询TestEquip(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTestEquipByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestEquip>", ReturnType = "ListTestEquip")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchTestEquipByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchTestEquipByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询TestEquip", Desc = "通过主键ID查询TestEquip", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTestEquipById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestEquip>", ReturnType = "TestEquip")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchTestEquipById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchTestEquipById(long id, string fields, bool isPlanish);
        #endregion

        #region TestEquipItem

        [ServiceContractDescription(Name = "查询TestEquipItem", Desc = "查询TestEquipItem", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTestEquipItem", Get = "", Post = "TestEquipItem", Return = "BaseResultList<TestEquipItem>", ReturnType = "ListTestEquipItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchTestEquipItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchTestEquipItem(TestEquipItem entity);

        [ServiceContractDescription(Name = "查询TestEquipItem(HQL)", Desc = "查询TestEquipItem(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTestEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestEquipItem>", ReturnType = "ListTestEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchTestEquipItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchTestEquipItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询TestEquipItem", Desc = "通过主键ID查询TestEquipItem", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTestEquipItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestEquipItem>", ReturnType = "TestEquipItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchTestEquipItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchTestEquipItemById(long id, string fields, bool isPlanish);
        #endregion

        #region TestItem

        [ServiceContractDescription(Name = "查询TestItem", Desc = "查询TestItem", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTestItem", Get = "", Post = "TestItem", Return = "BaseResultList<TestItem>", ReturnType = "ListTestItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchTestItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchTestItem(TestItem entity);

        [ServiceContractDescription(Name = "查询TestItem(HQL)", Desc = "查询TestItem(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestItem>", ReturnType = "ListTestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchTestItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchTestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询TestItem", Desc = "通过主键ID查询TestItem", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestItem>", ReturnType = "TestItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchTestItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchTestItemById(long id, string fields, bool isPlanish);
        #endregion

        #region TestType

        [ServiceContractDescription(Name = "查询TestType", Desc = "查询TestType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTestType", Get = "", Post = "TestType", Return = "BaseResultList<TestType>", ReturnType = "ListTestType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchTestType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchTestType(TestType entity);

        [ServiceContractDescription(Name = "查询TestType(HQL)", Desc = "查询TestType(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTestTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestType>", ReturnType = "ListTestType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchTestTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchTestTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询TestType", Desc = "通过主键ID查询TestType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchTestTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<TestType>", ReturnType = "TestType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchTestTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchTestTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region WardType

        [ServiceContractDescription(Name = "查询WardType", Desc = "查询WardType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchWardType", Get = "", Post = "WardType", Return = "BaseResultList<WardType>", ReturnType = "ListWardType")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchWardType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchWardType(WardType entity);

        [ServiceContractDescription(Name = "查询WardType(HQL)", Desc = "查询WardType(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchWardTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WardType>", ReturnType = "ListWardType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchWardTypeByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchWardTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询WardType", Desc = "通过主键ID查询WardType", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchWardTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<WardType>", ReturnType = "WardType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchWardTypeById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchWardTypeById(long id, string fields, bool isPlanish);
        #endregion

        #region Phraseswatch

        [ServiceContractDescription(Name = "查询Phraseswatch", Desc = "查询Phraseswatch", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhraseswatch", Get = "", Post = "Phraseswatch", Return = "BaseResultList<Phraseswatch>", ReturnType = "ListPhraseswatch")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPhraseswatch", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPhraseswatch(Phraseswatch entity);

        [ServiceContractDescription(Name = "查询Phraseswatch(HQL)", Desc = "查询Phraseswatch(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhraseswatchByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Phraseswatch>", ReturnType = "ListPhraseswatch")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPhraseswatchByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPhraseswatchByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询Phraseswatch", Desc = "通过主键ID查询Phraseswatch", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhraseswatchById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<Phraseswatch>", ReturnType = "Phraseswatch")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPhraseswatchById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPhraseswatchById(long id, string fields, bool isPlanish);
        #endregion

        #region PhrasesWatchClass

        [ServiceContractDescription(Name = "新增PhrasesWatchClass表", Desc = "新增PhrasesWatchClass表", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_AddPhrasesWatchClass", Get = "", Post = "PhrasesWatchClass", Return = "BaseResultDataValue", ReturnType = "PhrasesWatchClass")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_AddPhrasesWatchClass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_AddPhrasesWatchClass(PhrasesWatchClass entity);

        [ServiceContractDescription(Name = "修改PhrasesWatchClass表", Desc = "修改PhrasesWatchClass表", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_UpdatePhrasesWatchClass", Get = "", Post = "PhrasesWatchClass", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdatePhrasesWatchClass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdatePhrasesWatchClass(PhrasesWatchClass entity);

        [ServiceContractDescription(Name = "修改PhrasesWatchClass表指定的属性", Desc = "修改PhrasesWatchClass表指定的属性", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_UpdatePhrasesWatchClassByField", Get = "", Post = "PhrasesWatchClass", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_UpdatePhrasesWatchClassByField", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_UpdatePhrasesWatchClassByField(PhrasesWatchClass entity, string fields);

        [ServiceContractDescription(Name = "删除PhrasesWatchClass表", Desc = "删除PhrasesWatchClass表", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_DelPhrasesWatchClass?id={id}", Get = "id={id}", Post = "", Return = "BaseResultBool", ReturnType = "Bool")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RS_UDTO_DelPhrasesWatchClass?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultBool RS_UDTO_DelPhrasesWatchClass(long id);

        [ServiceContractDescription(Name = "查询PhrasesWatchClass", Desc = "查询PhrasesWatchClass", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClass", Get = "", Post = "PhrasesWatchClass", Return = "BaseResultList<PhrasesWatchClass>", ReturnType = "ListPhrasesWatchClass")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPhrasesWatchClass", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPhrasesWatchClass(PhrasesWatchClass entity);

        [ServiceContractDescription(Name = "查询PhrasesWatchClass(HQL)", Desc = "查询PhrasesWatchClass(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PhrasesWatchClass>", ReturnType = "ListPhrasesWatchClass")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPhrasesWatchClassByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPhrasesWatchClassByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询PhrasesWatchClass", Desc = "通过主键ID查询PhrasesWatchClass", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PhrasesWatchClass>", ReturnType = "PhrasesWatchClass")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPhrasesWatchClassById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPhrasesWatchClassById(long id, string fields, bool isPlanish);

        [ServiceContractDescription(Name = "根据PhrasesWatchClassID查询机构信息单列树", Desc = "根据PhrasesWatchClassID查询机构信息单列树", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassTreeById?id={id}", Get = "id={id}", Post = "", Return = "BaseResultDataValue", ReturnType = "Tree")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RS_UDTO_SearchPhrasesWatchClassTreeById?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPhrasesWatchClassTreeById(string id);

        [ServiceContractDescription(Name = "根据PhrasesWatchClassID查询机构信息列表树", Desc = "根据PhrasesWatchClassID查询机构信息列表树", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassListTreeById?id={id}&fields={fields}", Get = "id={id}&fields={fields}", Post = "", Return = "BaseResultDataValue", ReturnType = "TreePhrasesWatchClass")]
        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/RS_UDTO_SearchPhrasesWatchClassListTreeById?id={id}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPhrasesWatchClassListTreeById(string id, string fields);

        [ServiceContractDescription(Name = "查询PhrasesWatchClass信息列表数据(HQL)(可获取机构子孙节点)", Desc = "查询PhrasesWatchClass信息列表数据(HQL)(可获取机构子孙节点)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassAndChildListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isSearchChild={isSearchChild}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isSearchChild={isSearchChild}", Post = "", Return = "BaseResultList<PhrasesWatchClass>", ReturnType = "ListPhrasesWatchClass")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPhrasesWatchClassAndChildListByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}&isSearchChild={isSearchChild}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPhrasesWatchClassAndChildListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isSearchChild);

        #endregion

        #region PhrasesWatchClassItem

        [ServiceContractDescription(Name = "查询PhrasesWatchClassItem", Desc = "查询PhrasesWatchClassItem", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassItem", Get = "", Post = "PhrasesWatchClassItem", Return = "BaseResultList<PhrasesWatchClassItem>", ReturnType = "ListPhrasesWatchClassItem")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPhrasesWatchClassItem", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPhrasesWatchClassItem(PhrasesWatchClassItem entity);

        [ServiceContractDescription(Name = "查询PhrasesWatchClassItem(HQL)", Desc = "查询PhrasesWatchClassItem(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PhrasesWatchClassItem>", ReturnType = "ListPhrasesWatchClassItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPhrasesWatchClassItemByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPhrasesWatchClassItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询PhrasesWatchClassItem", Desc = "通过主键ID查询PhrasesWatchClassItem", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchPhrasesWatchClassItemById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<PhrasesWatchClassItem>", ReturnType = "PhrasesWatchClassItem")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchPhrasesWatchClassItemById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchPhrasesWatchClassItemById(long id, string fields, bool isPlanish);
        #endregion

        #region NRequestForm

        [ServiceContractDescription(Name = "查询NRequestForm", Desc = "查询NRequestForm", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchNRequestForm", Get = "", Post = "NRequestForm", Return = "BaseResultList<NRequestForm>", ReturnType = "ListNRequestForm")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchNRequestForm", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchNRequestForm(NRequestForm entity);

        [ServiceContractDescription(Name = "查询NRequestForm(HQL)", Desc = "查询NRequestForm(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchNRequestFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<NRequestForm>", ReturnType = "ListNRequestForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchNRequestFormByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchNRequestFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询NRequestForm", Desc = "通过主键ID查询NRequestForm", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchNRequestFormById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<NRequestForm>", ReturnType = "NRequestForm")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchNRequestFormById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchNRequestFormById(long id, string fields, bool isPlanish);
        #endregion

        #region LStatTotal

        [ServiceContractDescription(Name = "查询LStat_Total", Desc = "查询LStat_Total", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchLStatTotal", Get = "", Post = "LStatTotal", Return = "BaseResultList<LStatTotal>", ReturnType = "ListLStatTotal")]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchLStatTotal", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchLStatTotal(LStatTotal entity);

        [ServiceContractDescription(Name = "查询LStat_Total(HQL)", Desc = "查询LStat_Total(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchLStatTotalByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LStatTotal>", ReturnType = "ListLStatTotal")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchLStatTotalByHQL?page={page}&limit={limit}&fields={fields}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchLStatTotalByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "通过主键ID查询LStat_Total", Desc = "通过主键ID查询LStat_Total", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchLStatTotalById?id={id}&fields={fields}&isPlanish={isPlanish}", Get = "id={id}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<LStatTotal>", ReturnType = "LStatTotal")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchLStatTotalById?id={id}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchLStatTotalById(long id, string fields, bool isPlanish);
        #endregion

        #region 统计分析定制 

        [ServiceContractDescription(Name = "获取某一质量指标类型的指的年份及指定的月份的不合格总数及标本总量", Desc = "获取某一质量指标类型的指的年份及指定的月份的不合格总数及标本总量", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchFailedTotalAndSpecimenTotalOfYearAndMonth?classificationId={classificationId}&qitype={qitype}&year={year}&month={month}", Get = "classificationId={classificationId}&qitype={qitype}&year={year}&month={month}", Post = "", Return = "BaseResultDataValue", ReturnType = "BaseResultDataValue")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchFailedTotalAndSpecimenTotalOfYearAndMonth?classificationId={classificationId}&qitype={qitype}&year={year}&month={month}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchFailedTotalAndSpecimenTotalOfYearAndMonth(string classificationId, string qitype, string year, string month);

        [ServiceContractDescription(Name = "查询质量指标类型(HQL)", Desc = "查询质量指标类型(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSPSAQualityIndicatorTypeByHQL?page={page}&limit={limit}&fields={fields}&classificationId={classificationId}&qitype={qitype}&statDateType={statDateType}&sadimension={sadimension}&startDate={startDate}&endDate={endDate}&where={where}&sort={sort}&isPlanish={isPlanish}", Get = "page={page}&limit={limit}&fields={fields}&classificationId={classificationId}&qitype={qitype}&statDateType={statDateType}&sadimension={sadimension}&startDate={startDate}&endDate={endDate}&where={where}&sort={sort}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SPSAQualityIndicatorType>", ReturnType = "ListSPSAQualityIndicatorType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSPSAQualityIndicatorTypeByHQL?page={page}&limit={limit}&fields={fields}&classificationId={classificationId}&qitype={qitype}&statDateType={statDateType}&sadimension={sadimension}&startDate={startDate}&endDate={endDate}&where={where}&sort={sort}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSPSAQualityIndicatorTypeByHQL(int page, int limit, string fields, string classificationId, string qitype, string statDateType, string sadimension, string startDate, string endDate, string where, string sort, bool isPlanish);

        [ServiceContractDescription(Name = "查询质量指标类型EChart图表(HQL)", Desc = "查询质量指标类型EChart图表(HQL)", Url = "ReaStatisticalAnalysisService.svc/RS_UDTO_SearchSPSAQualityIndicatorTypeOfEChart?classificationId={classificationId}&qitype={qitype}&statDateType={statDateType}&sadimension={sadimension}&startDate={startDate}&endDate={endDate}&where={where}&sort={sort}&fields={fields}&isPlanish={isPlanish}", Get = "classificationId={classificationId}&qitype={qitype}&statDateType={statDateType}&sadimension={sadimension}&startDate={startDate}&endDate={endDate}&where={where}&sort={sort}&fields={fields}&isPlanish={isPlanish}", Post = "", Return = "BaseResultList<SPSAQualityIndicatorType>", ReturnType = "ListSPSAQualityIndicatorType")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/RS_UDTO_SearchSPSAQualityIndicatorTypeOfEChart?classificationId={classificationId}&qitype={qitype}&statDateType={statDateType}&sadimension={sadimension}&startDate={startDate}&endDate={endDate}&where={where}&sort={sort}&fields={fields}&isPlanish={isPlanish}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue RS_UDTO_SearchSPSAQualityIndicatorTypeOfEChart(string classificationId, string qitype, string statDateType, string sadimension, string startDate, string endDate, string where, string sort, string fields, bool isPlanish);

        #endregion

    }
}
