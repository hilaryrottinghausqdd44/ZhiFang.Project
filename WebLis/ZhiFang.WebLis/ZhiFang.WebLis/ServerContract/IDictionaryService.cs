using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Web;
using ZhiFang.Model;
using System.ComponentModel;
using ZhiFang.Model.UiModel;
using System.IO;
using System.Data;

namespace ZhiFang.WebLis.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.WebLis", Name = "ZhiFang.WebLis.ServiceWCF.DictionaryService")]
    public interface IDictionaryService
    {
        #region 小组模版设置

        /// <summary>
        /// 查询所有
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAllReportGroupModelSet?page={page}&rows={rows}&itemkey={itemkey}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询所有小组模板对照关系 /GetAllReportGroupModelSet?page={page}&rows={rows}&itemkey={itemkey}&sort={sort}")]
        [OperationContract]
        BaseResultList<Model.PGroupFormat> GetAllReportGroupModelSet(int page, int rows, string itemkey, string sort);

        /// <summary>
        /// 查询单个
        /// </summary>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportGroupModelByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询谋个小组模版设置 /GetReportGroupModelByID?id={id}")]
        [OperationContract]
        BaseResultDataValue GetReportGroupModelByID(string id);

        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateReportGroupModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改小组模版设置 /UpdateReportGroupModelByID")]
        [OperationContract]
        BaseResult UpdateReportGroupModelByID(Model.PGroupPrint jsonentity);

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddReportGroupModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增小组模版设置 /AddReportGroupModel")]
        [OperationContract]
        BaseResultDataValue AddReportGroupModel(Model.PGroupPrint jsonentity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelReportGroupModel?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("删除小组模版 /DelReportGroupModel?id={id}")]
        [OperationContract]
        BaseResult DelReportGroupModel(string id);
        #endregion

        #region 公共服务
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPubDict?tableName={tablename}&fields={fields}&filervalue={filervalue}&itemno={itemno}&selectedflag={selectedflag}&labcode={labcode}&page={page}&rows={rows}&precisequery={precisequery}&sort={sort}&order={order}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description(" tablename={数据库字典名}&fields={字段}&filervalue={模糊查询}&itemno={项目编码}&selectedflag={0 全部 1组套外 2组套内}&labcode={实验室机构编码}&page={当前页}&rows={条数}&precisequery={精确字段(单个)} 字典公共服务(AgeUnit、B_Lab_CLIENTELE、B_Lab_District、B_Lab_Doctor、B_Lab_FolkType、B_Lab_SampleType、B_Lab_SickType、B_Lab_SuperGroup、B_Lab_TestItem、ChargeType、CLIENTELE、GenderType、SampleType、PGroup、SecretType、SickType、TestItem、TestType、WardType)")]
        [OperationContract]
        BaseResultDataValue GetPubDict(string tablename, string fields, string filervalue, string itemno, string selectedflag, string labcode, int page, int rows, string precisequery, string sort, string order);
        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DownReportExcel?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("下载对账单 /DownReportExcel?id={id}")]
        [OperationContract]
        string DownReportExcel(string id);

        #region 物流人员
        #region 获取物流人员信息列表
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLogisticsDeliveryPerson?page={page}&rows={rows}&presonname={presonname}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("获取物流人员信息列表 /GetLogisticsDeliveryPerson?page={page}&rows={rows}&presonname={presonname}")]
        [OperationContract]
        BaseResultDataValue GetLogisticsDeliveryPerson(int page, int rows, string presonname);
        #endregion

        #region 根据物流人员ID、已选或未选状态，查询客户信息列表
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLogisticsCustomerByDeliveryIDAndType?page={page}&rows={rows}&selectedflag={selectedflag}&account={account}&itemkey={itemkey}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        [Description("根据物流人员ID、已选或未选状态，查询客户信息列表 /GetLogisticsCustomerByDeliveryIDAndType?page={page}&rows={rows}&selectedflag={selectedflag}&account={account}&itemkey={itemkey}")]
        BaseResultDataValue GetLogisticsCustomerByDeliveryIDAndType(int page, int rows, int selectedflag, string account, string itemkey);
        #endregion

        #region 修改物流人员和客户的关系
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLogisticsDeliveryCustomer", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改物流人员和客户的关系 /UpdateLogisticsDeliveryCustomer")]
        [OperationContract]
        BaseResult UpdateLogisticsDeliveryCustomer(LogisticsEntity strentity);
        #endregion
        #endregion

        #region 财务对账单

        #region 获取对账单列表
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBill?page={page}&rows={rows}&monthname={monthname}&clientname={clientname}&status={status}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("获取对账单列表 /GetBill?page={page}&rows={rows}&monthname={monthname}&clientname={clientname}&status={status}")]
        [OperationContract]
        BaseResultDataValue GetBill(int page, int rows, string monthname, string clientname, string status);
        #endregion

        #region 修改对账单
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateBill", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改对账单 /UpdateBill")]
        [OperationContract]
        //1:id(序号)2:status(确认状态) 3:remark(备注)
        BaseResult UpdateBill(TB_CheckClientAccount jsonentity);
        #endregion

        #region 下载对账单
        //1:id(序号)2:type(下载)
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DownLoadExcel?id={id}&type={type}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("下载对账单 /DownLoadExcel?id={id}&type={type}")]
        [OperationContract]
        string DownLoadExcel(int id, int type);



        #endregion

        #endregion

        #region 中心字典服务

        #region CLIENTELE 中心医疗机构字典

        //#region CLIENTELE 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetCLIENTELEModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取CLIENTELE字典信息列表,/GetCLIENTELEModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetCLIENTELEModelManage(string itemkey, int page, int rows);

        //#endregion

        #region CLIENTELE 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddCLIENTELEModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增CLIENTELE字典 /AddCLIENTELEModel")]
        [OperationContract]
        BaseResultDataValue AddCLIENTELEModel(Model.CLIENTELE jsonentity);

        #endregion

        #region CLIENTELE 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateCLIENTELEModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改CLIENTELE字典 /UpdateCLIENTELEModelByID")]
        [OperationContract]
        BaseResult UpdateCLIENTELEModelByID(Model.CLIENTELE jsonentity);

        #endregion
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteCLIENTELEModelByID?clinetNo={clinetNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteCLIENTELEModelByID(string clinetNo);
        #endregion

        #region ClientEleArea 中心区域字典
        //#region ClientEleArea 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetClientEleAreaModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取ClientEleArea字典信息列表,/GetClientEleAreaModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetClientEleAreaModelManage(string itemkey, int page, int rows);

        //#endregion

        #region ClientEleArea 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddClientEleAreaModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增ClientEleArea字典 /AddClientEleAreaModel")]
        [OperationContract]
        BaseResultDataValue AddClientEleAreaModel(Model.ClientEleArea jsonentity);

        #endregion

        #region ClientEleArea 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateClientEleAreaModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改ClientEleArea字典 /UpdateClientEleAreaModelByID")]
        [OperationContract]
        BaseResult UpdateClientEleAreaModelByID(Model.ClientEleArea jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteClientEleAreaModelByID?areaID={areaID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteClientEleAreaModelByID(string areaID);
        #endregion

        #region AgeUnit 中心年龄字典
        //#region AgeUnit 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAgeUnitModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取AgeUnit字典信息列表,/GetAgeUnitModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetAgeUnitModelManage(string itemkey, int page, int rows);

        //#endregion

        #region AgeUnit 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddAgeUnitModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增AgeUnit字典 /AddAgeUnitModel")]
        [OperationContract]
        BaseResultDataValue AddAgeUnitModel(Model.AgeUnit jsonentity);

        #endregion

        #region AgeUnit 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateAgeUnitModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改AgeUnit字典 /UpdateAgeUnitModelByID")]
        [OperationContract]
        BaseResult UpdateAgeUnitModelByID(Model.AgeUnit jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteAgeUnitModelByID?AgeUnitNo={AgeUnitNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteAgeUnitModelByID(string AgeUnitNo);
        #endregion

        #region Doctor 中心医生字典

        //#region Doctor 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetDoctorModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取Doctor字典信息列表,/GetDoctorModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetDoctorModelManage(string itemkey, int page, int rows);

        //#endregion

        #region Doctor 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddDoctorModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增Doctor字典 /AddDoctorModel")]
        [OperationContract]
        BaseResultDataValue AddDoctorModel(Model.Doctor jsonentity);

        #endregion

        #region Doctor 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateDoctorModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改Doctor字典 /UpdateDoctorModelByID")]
        [OperationContract]
        BaseResult UpdateDoctorModelByID(Model.Doctor jsonentity);

        #endregion


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteDoctorModelByID?DoctorNo={DoctorNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteDoctorModelByID(string DoctorNo);
        #endregion

        #region District 中心病区字典
        //#region District 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetDistrictModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取District字典信息列表,/GetDistrictModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetDistrictModelManage(string itemkey, int page, int rows);

        //#endregion

        #region District 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddDistrictModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增District字典 /AddDistrictModel")]
        [OperationContract]
        BaseResultDataValue AddDistrictModel(Model.District jsonentity);

        #endregion

        #region District 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateDistrictModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改District字典 /UpdateDistrictModelByID")]
        [OperationContract]
        BaseResult UpdateDistrictModelByID(Model.District jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteDistrictModelByID?DistrictNo={DistrictNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteDistrictModelByID(string DistrictNo);
        #endregion

        #region PGroup 中心检验小组字典
        //#region PGroup 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPGroupModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取PGroup字典信息列表,/GetPGroupModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetPGroupModelManage(string itemkey, int page, int rows);

        //#endregion

        #region PGroup 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddPGroupModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增PGroup字典 /AddPGroupModel")]
        [OperationContract]
        BaseResultDataValue AddPGroupModel(Model.PGroup jsonentity);

        #endregion

        #region PGroup 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdatePGroupModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改PGroup字典 /UpdatePGroupModelByID")]
        [OperationContract]
        BaseResult UpdatePGroupModelByID(Model.PGroup jsonentity);

        #endregion
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeletePGroupModelByID?SectionNo={SectionNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeletePGroupModelByID(string SectionNo);
        #endregion

        #region SampleType 中心样本类型字典
        //#region SampleType 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSampleTypeModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取SampleType字典信息列表,/GetSampleTypeModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetSampleTypeModelManage(string itemkey, int page, int rows);

        //#endregion

        #region SampleType 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddSampleTypeModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增SampleType字典 /AddSampleTypeModel")]
        [OperationContract]
        BaseResultDataValue AddSampleTypeModel(Model.SampleType jsonentity);

        #endregion

        #region SampleType 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateSampleTypeModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改SampleType字典 /UpdateSampleTypeModelByID")]
        [OperationContract]
        BaseResult UpdateSampleTypeModelByID(Model.SampleType jsonentity);

        #endregion


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteSampleTypeModelByID?SampleTypeNo={SampleTypeNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteSampleTypeModelByID(string SampleTypeNo);
        #endregion

        #region TestItem 中心项目字典
        //#region TestItem 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetTestItemModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取TestItem字典信息列表,/GetTestItemModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetTestItemModelManage(string itemkey, int page, int rows);

        //#endregion

        #region TestItem 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddTestItemModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增TestItem字典 /AddTestItemModel")]
        [OperationContract]
        BaseResultDataValue AddTestItemModel(Model.TestItem jsonentity);

        #endregion

        #region TestItem 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateTestItemModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改TestItem字典 /UpdateTestItemModelByID")]
        [OperationContract]
        BaseResult UpdateTestItemModelByID(Model.TestItem jsonentity);

        #endregion
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteTestItemModelByID?ItemNo={ItemNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteTestItemModelByID(string ItemNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSubTestItemByItemNo?ItemNo={ItemNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResultDataValue GetSubTestItemByItemNo(string ItemNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetParTestItemByItemNo?ItemNo={ItemNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResultDataValue GetParTestItemByItemNo(string ItemNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetItemListByColor?colorvalue={colorvalue}&where={where}&page={page}&rows={rows}&sort={sort}&order={order}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResultDataValue GetItemListByColor(string colorvalue, string where, int page, int rows, string sort, string order);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SetItemColorByItemNoList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SetItemColorByItemNoList(string colorvalue, List<string> itemnolist);
        #endregion

        #region FolkType 中心民族类型字典
        //#region FolkType 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetFolkTypeModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取FolkType字典信息列表,/GetFolkTypeModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetFolkTypeModelManage(string itemkey, int page, int rows);

        //#endregion

        #region FolkType 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddFolkTypeModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增FolkType字典 /AddFolkTypeModel")]
        [OperationContract]
        BaseResultDataValue AddFolkTypeModel(Model.FolkType jsonentity);

        #endregion

        #region FolkType 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateFolkTypeModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改FolkType字典 /UpdateFolkTypeModelByID")]
        [OperationContract]
        BaseResult UpdateFolkTypeModelByID(Model.FolkType jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteFolkTypeModelByID?FolkNo={FolkNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteFolkTypeModelByID(string FolkNo);
        #endregion

        #region SuperGroup 中心检验大组字典
        //#region SuperGroup 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSuperGroupModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取SuperGroup字典信息列表,/GetSuperGroupModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetSuperGroupModelManage(string itemkey, int page, int rows);

        //#endregion

        #region SuperGroup 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddSuperGroupModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增SuperGroup字典 /AddSuperGroupModel")]
        [OperationContract]
        BaseResultDataValue AddSuperGroupModel(Model.SuperGroup jsonentity);

        #endregion

        #region SuperGroup 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateSuperGroupModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改SuperGroup字典 /UpdateSuperGroupModelByID")]
        [OperationContract]
        BaseResult UpdateSuperGroupModelByID(Model.SuperGroup jsonentity);

        #endregion


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteSuperGroupModelByID?SuperGroupNo={SuperGroupNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteSuperGroupModelByID(string SuperGroupNo);
        #endregion

        #region GenderType 中心性别字典字典
        //#region GenderType 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetGenderTypeModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取GenderType字典信息列表,/GetGenderTypeModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetGenderTypeModelManage(string itemkey, int page, int rows);

        //#endregion

        #region GenderType 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddGenderTypeModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增GenderType字典 /AddGenderTypeModel")]
        [OperationContract]
        BaseResultDataValue AddGenderTypeModel(Model.GenderType jsonentity);

        #endregion

        #region GenderType 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateGenderTypeModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改GenderType字典 /UpdateGenderTypeModelByID")]
        [OperationContract]
        BaseResult UpdateGenderTypeModelByID(Model.GenderType jsonentity);

        #endregion


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteGenderTypeModelByID?GenderNo={GenderNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteGenderTypeModelByID(string GenderNo);
        #endregion

        #region SickType 中心就诊类型字典
        //#region SickType 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSickTypeModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取SickType字典信息列表,/GetSickTypeModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetSickTypeModelManage(string itemkey, int page, int rows);

        //#endregion

        #region SickType 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddSickTypeModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增SickType字典 /AddSickTypeModel")]
        [OperationContract]
        BaseResultDataValue AddSickTypeModel(Model.SickType jsonentity);

        #endregion

        #region SickType 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateSickTypeModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改SickType字典 /UpdateSickTypeModelByID")]
        [OperationContract]
        BaseResult UpdateSickTypeModelByID(Model.SickType jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteSickTypeModelByID?SickTypeNo={SickTypeNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteSickTypeModelByID(string SickTypeNo);
        #endregion

        #region WardType 中心病房字典
        //#region WardType 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetWardTypeModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取WardType字典信息列表,/GetWardTypeModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetWardTypeModelManage(string itemkey, int page, int rows);

        //#endregion

        #region WardType 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddWardTypeModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增WardType字典 /AddWardTypeModel")]
        [OperationContract]
        BaseResultDataValue AddWardTypeModel(Model.WardType jsonentity);

        #endregion

        #region WardType 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateWardTypeModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改WardType字典 /UpdateWardTypeModelByID")]
        [OperationContract]
        BaseResult UpdateWardTypeModelByID(Model.WardType jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteWardTypeModelByID?DistrictNo={DistrictNo}&WardNo={WardNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteWardTypeModelByID(string DistrictNo, string WardNo);
        #endregion

        #region Department 中心科室字典
        //#region Department 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetDepartmentModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取Department字典信息列表,/GetDepartmentModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetDepartmentModelManage(string itemkey, int page, int rows);

        //#endregion

        #region Department 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddDepartmentModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增Department字典 /AddDepartmentModel")]
        [OperationContract]
        BaseResultDataValue AddDepartmentModel(Model.Department jsonentity);

        #endregion

        #region Department 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateDepartmentModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改Department字典 /UpdateDepartmentModelByID")]
        [OperationContract]
        BaseResult UpdateDepartmentModelByID(Model.Department jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteDepartmentModelByID?DeptNo={DeptNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteDepartmentModelByID(string DeptNo);
        #endregion

        #region GroupItem 实验室组套字典
        //#region GroupItem 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetGroupItemModelManage?itemkey={itemkey}&itemno={itemno}&selectedflag={selectedflag}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取GroupItem字典信息列表,/GetGroupItemModelManage?itemkey={检索}&itemno={项目}&selectedflag={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetGroupItemModelManage(string itemkey, string itemno, string selectedflag);

        //#endregion

        #region GroupItem 字典表_增加

        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddGroupItemModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("新增GroupItem字典 /AddGroupItemModel")]
        //[OperationContract]
        //BaseResultDataValue AddGroupItemModel(Model.GroupItem jsonentity);

        #endregion

        #region GroupItem 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateGroupItemModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改GroupItem字典 /UpdateGroupItemModelByID")]
        [OperationContract]
        BaseResult UpdateGroupItemModelByID(Model.UiModel.GroupItemEntity jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteGroupItemModelByID?PItemNo={PItemNo}&ItemNo={ItemNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteGroupItemModelByID(string PItemNo, string ItemNo);
        #endregion

        #region ItemColorDict 项目颜色字典
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAllItemColorDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResultDataValue GetAllItemColorDict();


        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddItemColorDict", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddItemColorDict(Model.ItemColorDict jsonentity);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateItemColorDictByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResult UpdateItemColorDictByID(Model.ItemColorDict jsonentity);


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteItemColorDictByID?ColorId={ColorId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteItemColorDictByID(string ColorId);
        #endregion

        #region BPhysicalExamType 体检类型字典
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAllBPhysicalExamType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResultDataValue GetAllBPhysicalExamType();

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddBPhysicalExamType", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddBPhysicalExamType(Model.BPhysicalExamType jsonentity);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateBPhysicalExamTypeByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResult UpdateBPhysicalExamTypeByID(Model.BPhysicalExamType jsonentity);


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteBPhysicalExamTypeByID?Id={Id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteBPhysicalExamTypeByID(string Id);
        #endregion

        #endregion

        #region 实验室字典

        #region B_Lab_TestItem 实验室项目字典
        //#region B_Lab_TestItem 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabTestItemModelManage?itemkey={itemkey}&labcode={labcode}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取B_Lab_TestItem字典信息列表,/GetLabTestItemModelManage?itemkey={检索}&labcode={机构代码}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetLabTestItemModelManage(string itemkey, string labcode, int page, int rows);

        //#endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabTestItemByLabItemNo?LabItemNo={LabItemNo}&labcode={labcode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("/GetLabTestItemByLabItemNo?LabItemNo={LabItemNo}&labcode={labcode}")]
        [OperationContract]
        BaseResultDataValue GetLabTestItemByLabItemNo(string LabItemNo, string labcode);

        #region B_Lab_TestItem 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddLabTestItemModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增B_Lab_TestItem字典 /AddLabTestItemModel")]
        [OperationContract]
        BaseResultDataValue AddLabTestItemModel(Model.Lab_TestItem jsonentity);

        #endregion

        #region B_Lab_TestItem 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLabTestItemModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改B_Lab_TestItem字典 /UpdateLabTestItemModelByID")]
        [OperationContract]
        BaseResult UpdateLabTestItemModelByID(Model.Lab_TestItem jsonentity);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLabTestItemColorByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改B_Lab_TestItem字典 /UpdateLabTestItemColorByID")]
        [OperationContract]
        BaseResult UpdateLabTestItemColorByID(Model.Lab_TestItem jsonentity);
        #endregion


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteLabTestItemModelByID?labCode={labCode}&labItemNo={labItemNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]

        BaseResult DeleteLabTestItemModelByID(string labCode, string labItemNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabSubTestItemByItemNo?ItemNo={ItemNo}&LabCode={LabCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]

        BaseResultDataValue GetLabSubTestItemByItemNo(string ItemNo, string LabCode);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabParTestItemByItemNo?ItemNo={ItemNo}&LabCode={LabCode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]

        BaseResultDataValue GetLabParTestItemByItemNo(string ItemNo, string LabCode);
        #endregion

        #region B_Lab_FolkType 实验室民族字典
        //#region B_Lab_FolkType 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabFolkTypeModelManage?itemkey={itemkey}&labcode={labcode}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取B_Lab_FolkType字典信息列表,/GetLabFolkTypeModelManage?itemkey={检索}&labcode={机构代码}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetLabFolkTypeModelManage(string itemkey, string labcode, int page, int rows);

        //#endregion

        #region B_Lab_FolkType 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddLabFolkTypeModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增B_Lab_FolkType字典 /AddLabFolkTypeModel")]
        [OperationContract]
        BaseResultDataValue AddLabFolkTypeModel(Model.Lab_FolkType jsonentity);

        #endregion

        #region B_Lab_FolkType 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLabFolkTypeModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改B_Lab_FolkType字典 /UpdateLabFolkTypeModelByID")]
        [OperationContract]
        BaseResult UpdateLabFolkTypeModelByID(Model.Lab_FolkType jsonentity);

        #endregion


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteLabFolkTypeModelByID?labCode={labCode}&labClientNo={labClientNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteLabFolkTypeModelByID(string labCode, string labClientNo);

        #endregion

        #region B_Lab_Doctor 实验室医生字典
        //#region B_Lab_Doctor 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabDoctorModelManage?itemkey={itemkey}&labcode={labcode}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取B_Lab_Doctor字典信息列表,/GetLabDoctorModelManage?itemkey={检索}&labcode={机构代码}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetLabDoctorModelManage(string itemkey, string labcode, int page, int rows);

        //#endregion

        #region B_Lab_Doctor 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddLabDoctorModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增B_Lab_Doctor字典 /AddLabDoctorModel")]
        [OperationContract]
        BaseResultDataValue AddLabDoctorModel(Model.Lab_Doctor jsonentity);

        #endregion

        #region B_Lab_Doctor 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLabDoctorModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改B_Lab_Doctor字典 /UpdateLabDoctorModelByID")]
        [OperationContract]
        BaseResult UpdateLabDoctorModelByID(Model.Lab_Doctor jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteLabDoctorModelByID?labCode={labCode}&labDoctorNo={labDoctorNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteLabDoctorModelByID(string labCode, string labDoctorNo);

        #endregion

        #region B_Lab_SampleType 实验室样本类型字典
        //#region B_Lab_SampleType 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabSampleTypeModelManage?itemkey={itemkey}&labcode={labcode}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取B_Lab_SampleType字典信息列表,/GetLabSampleTypeModelManage?itemkey={检索}&labcode={机构代码}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetLabSampleTypeModelManage(string itemkey, string labcode, int page, int rows);

        //#endregion

        #region B_Lab_SampleType 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddLabSampleTypeModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增B_Lab_SampleType字典 /AddLabSampleTypeModel")]
        [OperationContract]
        BaseResultDataValue AddLabSampleTypeModel(Model.Lab_SampleType jsonentity);

        #endregion

        #region B_Lab_SampleType 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLabSampleTypeModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改B_Lab_SampleType字典 /UpdateLabSampleTypeModelByID")]
        [OperationContract]
        BaseResult UpdateLabSampleTypeModelByID(Model.Lab_SampleType jsonentity);

        #endregion


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteLabSampleTypeModelByID?labCode={labCode}&labSampleTypeNo={labSampleTypeNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteLabSampleTypeModelByID(string labCode, string labSampleTypeNo);

        #endregion

        #region B_Lab_SickType 实验室就诊类型字典
        //#region B_Lab_SickType 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabSickTypeModelManage?itemkey={itemkey}&labcode={labcode}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取B_Lab_SickType字典信息列表,/GetLabSickTypeModelManage?itemkey={检索}&labcode={机构代码}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetLabSickTypeModelManage(string itemkey, string labcode, int page, int rows);

        //#endregion

        #region B_Lab_SickType 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddLabSickTypeModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增B_Lab_SickType字典 /AddLabSickTypeModel")]
        [OperationContract]
        BaseResultDataValue AddLabSickTypeModel(Model.Lab_SickType jsonentity);

        #endregion

        #region B_Lab_SickType 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLabSickTypeModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改B_Lab_SickType字典 /UpdateLabSickTypeModelByID")]
        [OperationContract]
        BaseResult UpdateLabSickTypeModelByID(Model.Lab_SickType jsonentity);

        #endregion
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteLabSickTypeModelByID?labCode={labCode}&labSickTypeNo={labSickTypeNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteLabSickTypeModelByID(string labCode, string labSickTypeNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetCenterSickTypeListByLab_Area_Center?itemkey={itemkey}&labcode={labcode}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResultDataValue GetCenterSickTypeListByLab_Area_Center(string itemkey, string labcode, int page, int rows);

        #endregion

        #region B_Lab_CLIENTELE 实验室医疗机构字典
        //#region B_Lab_CLIENTELE 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabCLIENTELEModelManage?itemkey={itemkey}&labcode={labcode}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取B_Lab_CLIENTELE字典信息列表,/GetLabCLIENTELEModelManage?itemkey={检索}&labcode={医疗机构}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetLabCLIENTELEModelManage(string itemkey,string labcode, int page, int rows);

        //#endregion

        #region B_Lab_CLIENTELE 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddLabCLIENTELEModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增B_Lab_CLIENTELE字典 /AddLabCLIENTELEModel")]
        [OperationContract]
        BaseResultDataValue AddLabCLIENTELEModel(Model.Lab_CLIENTELE jsonentity);

        #endregion

        #region B_Lab_CLIENTELE 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLabCLIENTELEModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改B_Lab_CLIENTELE字典 /UpdateLabCLIENTELEModelByID")]
        [OperationContract]
        BaseResult UpdateLabCLIENTELEModelByID(Model.Lab_CLIENTELE jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteLabCLIENTELEModelByID?labCode={labCode}&labClientNo={labClientNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteLabCLIENTELEModelByID(string labCode, string labClientNo);
        #endregion

        #region B_Lab_SuperGroup 实验室检验大组字典字典
        //#region B_Lab_SuperGroup 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabSuperGroupModelManage?itemkey={itemkey}&labcode={labcode}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取B_Lab_SuperGroup字典信息列表,/GetLabSuperGroupModelManage?itemkey={检索}&labcode={医疗机构}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetLabSuperGroupModelManage(string itemkey, string labcode, int page, int rows);

        //#endregion

        #region B_Lab_SuperGroup 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddLabSuperGroupModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增B_Lab_SuperGroup字典 /AddLabSuperGroupModel")]
        [OperationContract]
        BaseResultDataValue AddLabSuperGroupModel(Model.Lab_SuperGroup jsonentity);

        #endregion

        #region B_Lab_SuperGroup 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLabSuperGroupModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改B_Lab_SuperGroup字典 /UpdateB_Lab_SuperGroupModelByID")]
        [OperationContract]
        BaseResult UpdateLabSuperGroupModelByID(Model.Lab_SuperGroup jsonentity);

        #endregion


        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteLabSuperGroupModelByID?labCode={labCode}&labSuperGroupNo={labSuperGroupNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteLabSuperGroupModelByID(string labCode, string labSuperGroupNo);
        #endregion

        #region B_Lab_District 实验室病区字典
        //#region B_Lab_District 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabDistrictModelManage?itemkey={itemkey}&labcode={labcode}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取B_Lab_District字典信息列表,/GetLabDistrictModelManage?itemkey={检索}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetLabDistrictModelManage(string itemkey, string labcode, int page, int rows);

        //#endregion

        #region B_Lab_District 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddLabDistrictModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增B_Lab_District字典 /AddLabDistrictModel")]
        [OperationContract]
        BaseResultDataValue AddLabDistrictModel(Model.Lab_District jsonentity);

        #endregion

        #region B_Lab_District 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLabDistrictModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改B_Lab_District字典 /UpdateLabDistrictModelByID")]
        [OperationContract]
        BaseResult UpdateLabDistrictModelByID(Model.Lab_District jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteLabDistrictModelByID?labCode={labCode}&labDistrictNo={labDistrictNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteLabDistrictModelByID(string labCode, string labDistrictNo);
        #endregion

        #region B_Lab_PGroup 实验室检验小组字典
        //#region B_Lab_PGroup 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabPGroupModelManage?itemkey={itemkey}&labcode={labcode}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取B_Lab_PGroup字典信息列表,/GetB_Lab_PGroupModelManage?itemkey={检索}&labcode={机构编码},&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetLabPGroupModelManage(string itemkey, string labcode, int page, int rows);

        //#endregion

        #region B_Lab_PGroup 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddLabPGroupModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增B_Lab_PGroup字典 /AddLabPGroupModel")]
        [OperationContract]
        BaseResultDataValue AddLabPGroupModel(Model.Lab_PGroup jsonentity);

        #endregion

        #region B_Lab_PGroup 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLabPGroupModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改B_Lab_PGroup字典 /UpdateLabPGroupModelByID")]
        [OperationContract]
        BaseResult UpdateLabPGroupModelByID(Model.Lab_PGroup jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteLabPGroupModelByID?labCode={labCode}&labSectionNo={labSectionNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteLabPGroupModelByID(string labCode, string labSectionNo);
        #endregion

        #region B_Lab_Department 实验室科室字典
        //#region B_Lab_Department 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabDepartmentModelManage?itemkey={itemkey}&labcode={labcode}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取B_Lab_Department字典信息列表,/GetLabDepartmentModelManage?itemkey={检索}&labcode={labcode}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetLabDepartmentModelManage(string itemkey, string labcode, int page, int rows);

        //#endregion

        #region B_Lab_Department 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddLabDepartmentModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增B_Lab_Department字典 /AddLabDepartmentModel")]
        [OperationContract]
        BaseResultDataValue AddLabDepartmentModel(Model.Lab_Department jsonentity);

        #endregion

        #region B_Lab_Department 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLabDepartmentModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改B_Lab_Department字典 /UpdateLabDepartmentModelByID")]
        [OperationContract]
        BaseResult UpdateLabDepartmentModelByID(Model.Lab_Department jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteLabDepartmentModelByID?labCode={labCode}&labDeptNo={labDeptNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteLabDepartmentModelByID(string labCode, string labDeptNo);
        #endregion

        #region b_lab_GenderType 实验室性别字典
        //#region b_lab_GenderType 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabGenderTypeModelManage?itemkey={itemkey}&labcode={labcode}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取b_lab_GenderType字典信息列表,/GetLabGenderTypeModelManage?itemkey={检索}&labcode={机构代码}&page={当前页}&rows={当前页数量}")]
        //[OperationContract]
        //BaseResultDataValue GetLabGenderTypeModelManage(string itemkey, string labcode, int page, int rows);

        //#endregion

        #region b_lab_GenderType 字典表_增加

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddLabGenderTypeModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("新增b_lab_GenderType字典 /Addb_lab_GenderTypeModel")]
        [OperationContract]
        BaseResultDataValue AddLabGenderTypeModel(Model.Lab_GenderType jsonentity);

        #endregion

        #region b_lab_GenderType 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLabGenderTypeModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改b_lab_GenderType字典 /UpdateLabGenderTypeModelByID")]
        [OperationContract]
        BaseResult UpdateLabGenderTypeModelByID(Model.Lab_GenderType jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteLabGenderTypeModelByID?labCode={labCode}&labGenderNo={labGenderNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteLabGenderTypeModelByID(string labCode, string labGenderNo);

        #endregion

        #region B_Lab_GroupItem 实验室组套字典
        //#region B_Lab_GroupItem 字典表_查询

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLabGroupItemModelManage?itemkey={itemkey}&labcode={labcode}&itemno={itemno}&selectedflag={selectedflag}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("获取B_Lab_GroupItem字典信息列表,/GetLabGroupItemModelManage?itemkey={检索}&labcode={机构编码}&itemno={项目}&selectedflag={状态}")]
        //[OperationContract]
        //BaseResultDataValue GetLabGroupItemModelManage(string itemkey, string labcode, string itemno, string selectedflag);

        //#endregion

        #region B_Lab_GroupItem 字典表_增加

        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddLabGroupItemModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("新增B_Lab_GroupItem字典 /AddLabGroupItemModel")]
        //[OperationContract]
        //BaseResultDataValue AddLabGroupItemModel(Model.Lab_GroupItem jsonentity);

        #endregion

        #region B_Lab_GroupItem 字典表_修改

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLabGroupItemModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改B_Lab_GroupItem字典 /UpdateLabGroupItemModelByID")]
        [OperationContract]
        BaseResult UpdateLabGroupItemModelByID(Model.UiModel.GroupItemEntity jsonentity);

        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteLabGroupItemModelByID?labCode={labCode}&pItemNo={pItemNo}&ItemNo={ItemNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResult DeleteLabGroupItemModelByID(string labCode, string pItemNo, string ItemNo);
        #endregion

        #endregion

        #region 对照关系字典

        #region SampleTypeControl 样本类型字典
        #region SampleType 字典 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelSampleTypeControlModelByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("删除SampleType /DelSampleTypeControlModelByID?id={id}")]
        [OperationContract]
        BaseResult DelSampleTypeControlModelByID(string id);
        #endregion

        #region SampleType 字典 修改
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateOrAddSampleTypeControlModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改SampleTypeControl字典 /UpdateOrAddSampleTypeControlModelByID")]
        [OperationContract]
        BaseResult UpdateOrAddSampleTypeControlModelByID(Model.SampleTypeControl jsonentity);
        #endregion
        #endregion

        #region TestItem
        #region TestItem 字典 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelTestItemControlModelByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("删除TestItem /DelTestItemControlModelByID?id={id}")]
        [OperationContract]
        BaseResult DelTestItemControlModelByID(string id);
        #endregion

        #region B_ResultTestItemControl 字典 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelResultTestItemControlModelByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("删除B_ResultTestItemControl /DelResultTestItemControlModelByID?id={id}")]
        [OperationContract]
        BaseResult DelResultTestItemControlModelByID(string id);
        #endregion

        #region TestItem 字典 修改
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateOrAddTestItemControlModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改TestItemControl字典 /UpdateOrAddTestItemControlModelByID")]
        [OperationContract]
        BaseResult UpdateOrAddTestItemControlModelByID(Model.TestItemControl jsonentity);
        #endregion

        #region B_ResultTestItemControl 字典 修改
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateOrAddResultTestItemControlModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改B_ResultTestItemControl字典 /UpdateOrAddResultTestItemControlModelByID")]
        [OperationContract]
        BaseResult UpdateOrAddResultTestItemControlModelByID(Model.TestItemControl jsonentity);
        #endregion
        #endregion

        #region SuperGroupControl 字典
        #region SuperGroup 字典 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelSuperGroupControlModelByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("删除SuperGroup /DelSuperGroupControlModelByID?id={id}")]
        [OperationContract]
        BaseResult DelSuperGroupControlModelByID(string id);
        #endregion

        #region SuperGroup 字典 修改
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateOrAddSuperGroupControlModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改SuperGroupControl字典 /UpdateOrAddSuperGroupControlModelByID")]
        [OperationContract]
        BaseResult UpdateOrAddSuperGroupControlModelByID(Model.SuperGroupControl jsonentity);
        #endregion
        #endregion

        #region SickTypeControl 字典
        #region SickType 字典 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelSickTypeControlModelByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("删除SickType /DelSickTypeControlModelByID?id={id}")]
        [OperationContract]
        BaseResult DelSickTypeControlModelByID(string id);
        #endregion

        #region SickType 字典 修改
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateOrAddSickTypeControlModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改SickTypeControl字典 /UpdateOrAddSickTypeControlModelByID")]
        [OperationContract]
        BaseResult UpdateOrAddSickTypeControlModelByID(Model.SickTypeControl jsonentity);
        #endregion
        #endregion

        #region DistrictControl 字典
        #region District 字典 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelDistrictControlModelByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("删除District /DelDistrictControlModelByID?id={id}")]
        [OperationContract]
        BaseResult DelDistrictControlModelByID(string id);
        #endregion

        #region District 字典 修改
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateOrAddDistrictControlModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改DistrictControl字典 /UpdateOrAddDistrictControlModelByID")]
        [OperationContract]
        BaseResult UpdateOrAddDistrictControlModelByID(Model.DistrictControl jsonentity);
        #endregion
        #endregion

        #region FolkTypeControl 字典
        #region FolkType 字典 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelFolkTypeControlModelByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("删除FolkType /DelFolkTypeControlModelByID?id={id}")]
        [OperationContract]
        BaseResult DelFolkTypeControlModelByID(string id);
        #endregion

        #region FolkType 字典 修改
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateOrAddFolkTypeControlModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改FolkTypeControl字典 /UpdateOrAddFolkTypeControlModelByID")]
        [OperationContract]
        BaseResult UpdateOrAddFolkTypeControlModelByID(Model.FolkTypeControl jsonentity);
        #endregion
        #endregion

        #region DoctorControl 字典
        #region Doctor 字典 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelDoctorControlModelByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("删除Doctor /DelDoctorControlModelByID?id={id}")]
        [OperationContract]
        BaseResult DelDoctorControlModelByID(string id);
        #endregion

        #region Doctor 字典 修改
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateOrAddDoctorControlModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改DoctorControl字典 /UpdateOrAddDoctorControlModelByID")]
        [OperationContract]
        BaseResult UpdateOrAddDoctorControlModelByID(Model.DoctorControl jsonentity);
        #endregion
        #endregion

        #region GenderTypeControl 字典
        #region GenderType 字典 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelGenderTypeControlModelByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("删除GenderType /DelGenderTypeControlModelByID?id={id}")]
        [OperationContract]
        BaseResult DelGenderTypeControlModelByID(string id);
        #endregion

        #region GenderType 字典 修改
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateOrAddGenderTypeControlModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改GenderTypeControl字典 /UpdateOrAddGenderTypeControlModelByID")]
        [OperationContract]
        BaseResult UpdateOrAddGenderTypeControlModelByID(Model.GenderTypeControl jsonentity);
        #endregion
        #endregion

        #region PGroupControl 字典
        #region PGroup 字典 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelPGroupControlModelByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("删除PGroup /DelPGroupControlModelByID?id={id}")]
        [OperationContract]
        BaseResult DelPGroupControlModelByID(string id);
        #endregion

        #region PGroup 字典 修改
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateOrAddPGroupControlModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改PGroupControl字典 /UpdateOrAddPGroupControlModelByID")]
        [OperationContract]
        BaseResult UpdateOrAddPGroupControlModelByID(Model.PGroupControl jsonentity);
        #endregion
        #endregion

        #region DepartmentControl 字典
        #region Department 字典 删除
        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelDepartmentControlModelByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("删除Department /DelDepartmentControlModelByID?id={id}")]
        [OperationContract]
        BaseResult DelDepartmentControlModelByID(string id);
        #endregion

        #region Department 字典 修改
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateOrAddDepartmentControlModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改DepartmentControl字典 /UpdateOrAddDepartmentControlModelByID")]
        [OperationContract]
        BaseResult UpdateOrAddDepartmentControlModelByID(Model.DepartmentControl jsonentity);
        #endregion
        #endregion


        #region ItemColorAndSampleTypeDetail字典
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetItemColorAndSampleDetail?ColorId={ColorId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        BaseResultDataValue GetItemColorAndSampleDetail(string ColorId);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SaveToItemColorAndSampleTypeDetail", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResult SaveToItemColorAndSampleTypeDetail(Model.UiModel.UiItemColorAndSampleTypeDetail jsonentity);
        #endregion
        #endregion

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetClientListByRBAC?page={page}&rows={rows}&fields={fields}&where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Model.CLIENTELE> GetClientListByRBAC(int page, int rows, string fields, string where, string sort);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetCenterDoctorAllList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Model.Doctor> GetCenterDoctorAllList();

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetCenterDoctorList?page={page}&rows={rows}&fields={fields}&jsonentity={jsonentity}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        List<Model.Doctor> GetCenterDoctorList(int page, int rows, string fields, string jsonentity, string sort);

        #region 批量复制
        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CopyAllToLabs?DicTable={DicTable}&LabCodeNo={LabCodeNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue CopyAllToLabs(string DicTable, string LabCodeNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CopyAllToLabs?DicTable={DicTable}&fromLab={fromLab}&toLab={toLab}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue CopyAllToLabs(string DicTable, string fromLab, string toLab);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ExistLabsData?DicTable={DicTable}&LabCodeNo={LabCodeNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResult ExistLabsData(string DicTable, string LabCodeNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteByLabCode?DicTable={DicTable}&LabCodeNo={LabCodeNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResult DeleteByLabCode(string DicTable, string LabCodeNo);

        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BatchCopyItemsToLab?ItemNos={ItemNos}&LabCodeNo={LabCodeNo}&ItemKey={ItemKey}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResult BatchCopyItemsToLab(string ItemNos, string LabCodeNo, string ItemKey);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BatchCopyItemsToLab?ItemNos={ItemNos}&fromLabCodeNo={fromLabCodeNo}&LabCodeNo={LabCodeNo}&ItemKey={ItemKey}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResult BatchCopyItemsToLab(string ItemNos, string fromLabCodeNo, string LabCodeNo, string ItemKey);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/BatchCopySickTypeToLab?ItemNos={ItemNos}&fromLabCodeNo={fromLabCodeNo}&LabCodeNo={LabCodeNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResult BatchCopySickTypeToLab(string ItemNos, string fromLabCodeNo, string LabCodeNo);
        #endregion

        #region 模板管理
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAllReportModelManage?itemkey={itemkey}&page={page}&rows={rows}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("获取所有报告信息列表,/GetAllReportModelManage?itemkey={拼音检索}&page={当前页}&rows={当前页数量}")]
        [OperationContract]
        BaseResultDataValue GetAllReportModelManage(string itemkey, int page, int rows);


        //[WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetReportModelManageByID?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[Description("根据ID查询报告模板,/GetReportModelManageByID?id={id}")]
        //[OperationContract]
        //BaseResultDataValue GetReportModelManageByID(string id);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelReportModel?id={id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("根据ID删除一个报告模板,/DelReportModel?id={id}")]
        [OperationContract]
        BaseResult DelReportModel(string id);


        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddReportModel", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("增加一个报告模板,/AddReportModel")]
        [OperationContract]
        void AddReportModel();

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateReportModelByID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("修改单个报告模板")]
        [OperationContract]
        BaseResult UpdateReportModelByID(Model.PrintFormat jsonentity);
        #endregion

        #region 检验申请录入

        [Description("根据检验类型获取检验项目列表,/GetTestItem?supergroupno={COMBI为组合,int数字为检验大组}&itemkey={itemkey}&rows={rows}&page={page}&labcode={labcode}")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetTestItem?supergroupno={supergroupno}&itemkey={itemkey}&rows={rows}&page={page}&labcode={labcode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetTestItem(string supergroupno, string itemkey, int rows, int page, string labcode);



        [Description("查询检验类型项目列表,/GetSuperGroupList?typestate={1-组合,2-检验大组,3-组合+检验大组都存在}")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSuperGroupList?typestate={typestate}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetSuperGroupList(int typestate);


        [Description("根据检验项目ID查询检验明细,/GetTestDetailByItemID?itemid={项目号}&labcode={机构号}")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetTestDetailByItemID?itemid={itemid}&labcode={labcode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetTestDetailByItemID(string itemid, string labcode);

        [Description("根据检验项目ID查询本身项目明细,/GetParentItemAsChildItemByItemID?itemid={项目号}&labcode={机构号}")]
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetParentItemAsChildItemByItemID?itemid={itemid}&labcode={labcode}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetParentItemAsChildItemByItemID(string itemid, string labcode);
        #endregion

        #region 条码打印配置表增删改查服务
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetLocationBarCodePrintPamater?AccountId={AccountId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetLocationBarCodePrintPamater(string AccountId);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddLocationBarCodePrintPamater", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        [Description("增加一个条码打印配置参数,/AddLocationBarCodePrintPamater/jsonentity{账户ID：AccountId、配置参数:ParaMeter}")]
        BaseResultDataValue AddLocationBarCodePrintPamater(Model.LocationbarCodePrintPamater jsonentity);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateLocationBarCodePrintPamater", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        [Description("修改一个条码打印配置参数,/AddLocationBarCodePrintPamater/jsonentity{账户ID：AccountId、配置参数:ParaMeter}")]
        BaseResultDataValue UpdateLocationBarCodePrintPamater(Model.LocationbarCodePrintPamater jsonentity);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DelLocationBarCodePrintPamater?AccountId={AccountId}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DelLocationBarCodePrintPamater(string AccountId);
        #endregion

        #region 客户参数

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddBBClientPara", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("客户参数新增")]
        [OperationContract]
        BaseResultDataValue AddBBClientPara(B_ClientPara jsonentity);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/EditBBClientPara", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("客户参数修改")]
        [OperationContract]
        BaseResultDataValue EditBBClientPara(B_ClientPara jsonentity);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DeleteBBClientPara?Id={Id}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("客户参数删除")]
        [OperationContract]
        BaseResultDataValue DeleteBBClientPara(long Id);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SearchBBClientParaByParaNo?ParaNo={ParaNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("客户参数查询")]
        [OperationContract]
        BaseResultDataValue SearchBBClientParaByParaNo(string ParaNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SearchBBClientParaGroupByName?Name={Name}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("客户参数查询")]
        [OperationContract]
        BaseResultDataValue SearchBBClientParaGroupByName(string Name);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/SearchBBClientParaByParaNoAndLabIDAndLabName?ParaNo={ParaNo}&LabID={LabID}&LabName={LabName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("客户参数查询")]
        [OperationContract]
        BaseResultDataValue SearchBBClientParaByParaNoAndLabIDAndLabName(string ParaNo, string LabID, string LabName);

        #endregion

        #region 无用&未知
        /*
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SaveReportConfig?xmlName={xmlName}&selFieldName={selFieldName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SaveReportConfig(string xmlName, string selFieldName);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/ReadReportConfig?xmlName={xmlName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue ReadReportConfig(string xmlName);
        */
        #endregion

        #region 客户端密钥

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/CreatAESEncryptFile", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("客户端密钥文件生成并保存(WebLis用)")]
        [OperationContract]
        BaseResultDataValue CreatAESEncryptFile(UIAESEntity jsonentity);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/GetAESDecryptFileByFileName?fileName={fileName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("依fileName获取加密的密钥文件并解密还原为UIAESEntity(WebLis用)")]
        [OperationContract]
        BaseResultDataValue GetAESDecryptFileByFileName(string fileName);

        [WebGet(BodyStyle = WebMessageBodyStyle.Bare, UriTemplate = "/DownLoadAESEncryptFile?fileName={fileName}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("依clientNo下载按AES加密生成的密文文件(WebLis用)")]
        [OperationContract]
        Stream DownLoadAESEncryptFile(string fileName);
        #endregion
    }
}
