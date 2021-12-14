using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.ReportFormQueryPrint.Model;

namespace ZhiFang.ReportFormQueryPrint.ServerContract
{
    [ServiceContract(Namespace = "ZhiFang.ReportFormQueryPrint")]
    public interface IDictionaryService
    {
        /// <summary>
        /// 查询报告单
        /// </summary>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetDeptList?Where={Where}&fields={fields}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询报告单 /GetDeptList?Where={Where}&fields={fields}&page={page}&limit={limit}")]
        [OperationContract]
        BaseResultDataValue GetDeptList(string Where, string fields, int page, int limit);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPGroup?Where={Where}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询报告单 /GetPGroup?Where={Where}&fields={fields}")]
        [OperationContract]
        BaseResultDataValue GetPGroup(string Where, string fields);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetDeptListPaging?Where={Where}&fields={fields}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询报告单 /GetDeptListPaging?Where={Where}&fields={fields}&page={page}&limit={limit}")]
        [OperationContract]
        BaseResultDataValue GetDeptListPaging(string Where, string fields, int page, int limit);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPGroupPaging?Where={Where}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询报告单 /GetPGroupPaging?Where={Where}&fields={fields}")]
        [OperationContract]
        BaseResultDataValue GetPGroupPaging(string Where, string fields);
        //配置显示列
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAllColumnsSetting", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetAllColumnsSetting();

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetColumnsTemplateByAppType?AppType={AppType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetColumnsTemplateByAppType(string AppType);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddColumnsTempale", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddColumnsTempale(List<BColumnsSetting> columnsTemplate);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAllColumnsTemplate?appType={appType}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetAllColumnsTemplate(string appType,int page, int limit);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/deleteColumnsTempale", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue deleteColumnsTempale(List<long> CTIDList);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/setColumnsDefaultSetting?appType={appType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SetColumnsDefaultSetting(string appType);

        //配置查询项
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSelectTemplateByAppType?AppType={AppType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetSelectTemplateByAppType(string AppType);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAllSelectSetting", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetAllSelectSetting();

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddSelectTempale", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddSelectTempale(List<BSearchSetting> selectTempale);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAllSelectTemplate?appType={appType}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetAllSelectTemplate(string appType,int page, int limit);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/deleteSelectTempale", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue deleteSelectTempale(List<int> STIDList);
        
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SetSearchDefaultSetting?appType={appType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SetSearchDefaultSetting(string appType);


        //设置全局选项
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdatePublicSetting", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdatePublicSetting(List<BParameter> models);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAllPublicSetting?pageType={pageType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetAllPublicSetting(string pageType);
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/SetPublicDefaultSetting?appType={appType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue SetPublicDefaultSetting(string appType);
        

        //用户
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UserLogin?Account={Account}&pwd={pwd}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UserLogin(string Account, string pwd);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateUserPwd", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateUserPwd(int userNo, string oldPwd, string newPwd);

        //查询小组打印表
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSectionPrintList?SectionNo={SectionNo}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询小组打印表 /GetSectionPrintList")]
        [OperationContract]
        BaseResultDataValue GetSectionPrintList(string SectionNo,int page, int limit);

        //查询所有小组名
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPGroupCNameList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询所有小组名 /GetPGroupCNameList")]
        [OperationContract]
        BaseResultDataValue GetPGroupCNameList();

        //修改列信息
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateColumnsSetting", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateColumnsSetting(List<Model.BColumnsSetting> bColumnsSetting);

        //修改查询项信息
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateSearchSetting", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateSearchSetting(List<Model.BSearchSetting >bSearchSetting);

        //修改小组打印信息
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateSectionPrint", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateSectionPrint(Model.SectionPrint entity);

        //增加小组打印信息
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddSectionPrint", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddSectionPrint(SectionPrint entity);

        //删除小组打印信息
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteSectionPrint", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DeleteSectionPrint(List<int> SPID);
        //查询收费类型
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetChargeType?Where={Where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询收费类型 /GetChargeType?Where={Where}")]
        [OperationContract]
        BaseResultDataValue GetChargeType(string Where);
        //查询病区
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetDistrict?Where={Where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询病区 /GetDistrict?Where={Where}")]
        [OperationContract]
        BaseResultDataValue GetDistrict(string Where);
        //查询录入者和审核者
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetOperatorChecker?Where={Where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询录入者和审核者 /GetOperatorChecker?Where={Where}")]
        [OperationContract]
        BaseResultDataValue GetOperatorChecker(string Where);
        //查询高级查询项
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetAllSeniorSearch?urlwhere={urlwhere}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetAllSeniorSearch(string urlwhere);
        //获取已经添加的高级查询项
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSeniorSetting?appType={appType}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetSeniorSetting(string appType, int page, int limit);
        /// <summary>
        /// 添加高级查询到配置表
        /// </summary>
        /// <param name="selectTempale"></param>
        /// <returns></returns>
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddSeniorSetting", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddSeniorSetting(List<BSearchSetting> selectTempale);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="appType"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSeniorPublicSetting?SName={SName}&ParaNo={ParaNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetSeniorPublicSetting(string SName, string ParaNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSickType?Where={Where}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询就诊类型字典 /GetSickType?Where={Where}&fields={fields}")]
        [OperationContract]
        BaseResultDataValue GetSickType(string Where, string fields);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSampleType?Where={Where}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询样本类型字段 /GetSampleType?Where={Where}&fields={fields}")]
        [OperationContract]
        BaseResultDataValue GetSampleType(string Where, string fields);

        //查询小组打印表
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetSectionPrintListBySectionNo?SectionNo={SectionNo}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询小组打印表 /GetSectionPrintListBySectionNo")]
        [OperationContract]
        BaseResultDataValue GetSectionPrintListBySectionNo(string SectionNo, int page, int limit);

        //查询testitem表中itemdesc临床意义
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetTestItemItemDescByItemNo?ItemNo={ItemNo}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询testitem表中itemdesc临床意义 /GetTestItemItemDescByItemNo")]
        [OperationContract]
        BaseResultDataValue GetTestItemItemDescByItemNo(string ItemNo);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetWardType?Where={Where}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询病房字典表 /GetWardType?Where={Where}&page={page}&limit={limit}")]
        [OperationContract]
        BaseResultDataValue GetWardType(string Where, int page, int limit);

        //查询用户表
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetPUser?Where={Where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询用户表 /GetPUser?Where={Where}")]
        [OperationContract]
        BaseResultDataValue GetPUser(string Where);

        //增加用户
        //[WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddAndUpdatePUser", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        //[OperationContract]
        //BaseResultDataValue AddAndUpdatePUser(Model.PUser entity);

        //查询用户科室关联表
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetEmpDeptLinks?Where={Where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询用户表 /GetEmpDeptLinks?Where={Where}")]
        [OperationContract]
        BaseResultDataValue GetEmpDeptLinks(string Where);

        //增加用户科室关联表数据
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddEmpDeptLinks", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddEmpDeptLinks(List<EmpDeptLinks> entity);

        //增加用户科室关联表数据
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/DeleteEmpDeptLinks", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue DeleteEmpDeptLinks(List<EmpDeptLinks> entity);

        //用户
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/EmpDeptLinksUserLogin?Account={Account}&pwd={pwd}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue EmpDeptLinksUserLogin(string Account, string pwd);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetTestItem?Where={Where}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询检验项目 /GetTestItem?Where={Where}&fields={fields}")]
        [OperationContract]
        BaseResultDataValue GetTestItem(string Where, string fields);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetClentele?Where={Where}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询送检单位 /GetClentele?Where={Where}&fields={fields}")]
        [OperationContract]
        BaseResultDataValue GetClentele(string Where, string fields);


        
        //layui版模块表单显示列关系ModuleFormGridLink
        //获取模块关系
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBModuleModuleFormGridLinkByModuleID?ModuleID={ModuleID}&linkType={linkType}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBModuleModuleFormGridLinkByModuleID(string ModuleID, string linkType);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBModuleModuleFormGridLink?fields={fields}&page={page}&limit={limit}&where={where}&sort={sort}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBModuleModuleFormGridLink(string fields, int page, int limit, string where, string sort);
        //添加
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddBModuleModuleFormGridLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddBModuleModuleFormGridLink(List<BModuleModuleFormGridLink> ModuleFormGridLink);
        //更新
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateBModuleModuleFormGridLink", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateBModuleModuleFormGridLink(List<Model.BModuleModuleFormGridLink> ModuleFormGridLink);

        //删除模块表单显示列关系
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/deleteBModuleModuleFormGridLinkByModuleID", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue deleteBModuleModuleFormGridLinkByModuleID(List<long> ModuleFormGridLinkID);


        //FormList
        //获取
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBModuleFormList?where={where}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBModuleFormList(int page, int limit, string where);
        //添加
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddBModuleFormList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddBModuleFormList(List<BModuleFormList> ModuleFormList);
        //更新
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateBModuleFormList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateBModuleFormList(List<Model.BModuleFormList> ModuleFormList);

        //删除模块表单显示列关系
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/deleteBModuleFormList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue deleteBModuleFormList(List<long> FormID);


        //获取某个基础列表拥有的所有列FormControlList
        //获取
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBModuleFormControlListByFormCode?FormCode={FormCode}&page={page}&limit={limit}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBModuleFormControlListByFormCode(string FormCode, int page, int limit, string where);
        //添加
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddFormControlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddFormControlList(List<BModuleFormControlList> ModuleFormControlList);
        //更新
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateFormControlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateFormControlList(List<Model.BModuleFormControlList> ModuleFormControlList);

        //删除模块表单显示列关系
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/deleteFormControlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue deleteFormControlList(List<long> FormControlID);


        //查询条件相关方法FormControlSet
        //获取客户表单配置列表
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBModuleFormControlSetByFormCode?FormCode={FormCode}&page={page}&limit={limit}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBModuleFormControlSetByFormCode(string FormCode, int page, int limit, string where);
        //添加
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddBModuleFormControlSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddBModuleFormControlSet(List<BModuleFormControlSet> columnsTemplate);
        //更新
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateBModuleFormControlSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateBModuleFormControlSet(List<Model.BModuleFormControlSet> bModuleFormControlSetList);
        //删除
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/deleteBModuleFormControlSet?FormControSetlID={FormControSetlID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue deleteBModuleFormControlSet(long FormControSetlID);


        //GridList
        //获取相关显示列表GridList
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBModuleGridList?where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBModuleGridList(string where);
        //通过模块关系表获取gridcode再获取gridlist
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBModuleGridListByModuleID?ModuleID={ModuleID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBModuleGridListByModuleID(string ModuleID);
        //添加
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddBModuleGridList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddBModuleGridList(List<BModuleGridList> ModuleGridList);
        //更新
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateBModuleGridList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateBModuleGridList(List<Model.BModuleGridList> ModuleGridList);
        //删除
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/deleteBModuleGridList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue deleteBModuleGridList(List<long> GridListID);


        //layui版配置显示列GridControlList
        //获取某个基础列表拥有的所有列
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBModuleGridControlListByGridCode?GridCode={GridCode}&page={page}&limit={limit}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBModuleGridControlListByGridCode(string GridCode ,int page, int limit, string where);
        //添加
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddBModuleGridControlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddBModuleGridControlList(List<BModuleGridControlList> ModuleGridControlList);
        //更新
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateBModuleGridControlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateBModuleGridControlList(List<Model.BModuleGridControlList> ModuleGridControlList);
        //删除
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/deleteBModuleGridControlList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue deleteBModuleGridControlList(List<long> GridControlListID);



        //GridControlSet
        //添加
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/AddBModuleGridControlSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue AddBModuleGridControlSet(List<BModuleGridControlSet> columnsTemplate);
        //修改列信息(GridControlSet)
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/UpdateBModuleGridControlSet", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue UpdateBModuleGridControlSet(List<Model.BModuleGridControlSet> bModuleGridControlSetList);
        //获取
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBModuleGridControlSetByGridCode?GridCode={GridCode}&page={page}&limit={limit}&where={where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBModuleGridControlSetByGridCode(string GridCode, int page, int limit,string where);
        //删除
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/deleteBModuleGridControlSet?GridControSetlID={GridControSetlID}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue deleteBModuleGridControlSet(long GridControSetlID);


        //ControlSet和ControlList合并，返回ControlList
        //获取
        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBModuleGridControlSetList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBModuleGridControlSetList(string where, string sort);

        [WebInvoke(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetBModuleFormControlSetList", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        BaseResultDataValue GetBModuleFormControlSetList(string where, string sort);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LabStarGetPGroup?Where={Where}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询小组 /LabStarGetPGroup?Where={Where}&fields={fields}")]
        [OperationContract]
        BaseResultDataValue LabStarGetPGroup(string Where, string fields);

        //LabStar查询小组打印表
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LabStarGetSectionPrintList?SectionNo={SectionNo}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询小组打印表 /LabStarGetSectionPrintList")]
        [OperationContract]
        BaseResultDataValue LabStarGetSectionPrintList(string SectionNo, int page, int limit);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LabStarGetSickType?Where={Where}&fields={fields}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询就诊类型字典 /LabStarGetSickType?Where={Where}&fields={fields}")]
        [OperationContract]
        BaseResultDataValue LabStarGetSickType(string Where, string fields);

        //查询病区
        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LabStarGetDistrict?Where={Where}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询病区 /LabStarGetDistrict?Where={Where}")]
        [OperationContract]
        BaseResultDataValue LabStarGetDistrict(string Where);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/LabStarGetDeptListPaging?Where={Where}&fields={fields}&page={page}&limit={limit}", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("查询科室 /LabStarGetDeptListPaging?Where={Where}&fields={fields}&page={page}&limit={limit}")]
        [OperationContract]
        BaseResultDataValue LabStarGetDeptListPaging(string Where, string fields, int page, int limit);

        [WebGet(BodyStyle = WebMessageBodyStyle.WrappedRequest, UriTemplate = "/GetConfigLabStarUrl", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [Description("获取labstar工程地址 /GetConfigLabStarUrl")]
        [OperationContract]
        BaseResultDataValue GetConfigLabStarUrl();
    }
}
