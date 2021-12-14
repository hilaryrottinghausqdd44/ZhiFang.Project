using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.RBAC;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Statistics;
using ZhiFang.ReagentSys.Client.Common;
using ZhiFang.ReagentSys.Client.ServerContract;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.ReagentSys.Client
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReaStatisticalAnalysisService : IReaStatisticalAnalysisService
    {
        IBLL.ReagentSys.Client.IBReaBmsCenOrderDtl IBReaBmsCenOrderDtl { get; set; }
        IBLL.ReagentSys.Client.IBReaBmsInDtl IBReaBmsInDtl { get; set; }
        IBLL.ReagentSys.Client.IBReaBmsQtyDtl IBReaBmsQtyDtl { get; set; }
        IBLL.ReagentSys.Client.IBReaBmsTransferDtl IBReaBmsTransferDtl { get; set; }
        IBLL.ReagentSys.Client.IBReaBmsOutDtl IBReaBmsOutDtl { get; set; }
        IBLL.ReagentSys.Client.IBComprehensiveStatistics IBComprehensiveStatistics { get; set; }
        IBLL.ReagentSys.Client.IBReaLisTestStatisticalResults IBReaLisTestStatisticalResults { get; set; }
        IBLL.ReagentSys.Client.IBReaBmsCenSaleDtl IBReaBmsCenSaleDtl { get; set; }
        IBLL.ReagentSys.Client.IBReaEquipTestItemReaGoodLink IBReaEquipTestItemReaGoodLink { get; set; }

        #region 统计报表查询
        public BaseResultDataValue StatisticalAnalysisBmsInDocByGoodsNo(string StartDate, string EndDate, string CenOrgIdList, string GoodsNoList, string ComOrgIdList, string BmsInDocTypeList, string DateType, string CenOrgLevel)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            try
            {
                if (StartDate == null || StartDate.Trim() == "")
                {
                    baseResultDataValue.ErrorInfo = "开始时间不能为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (EndDate == null || EndDate.Trim() == "")
                {
                    baseResultDataValue.ErrorInfo = "开始时间不能为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }

                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID) == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID).Trim() == "")
                {
                    baseResultDataValue.ErrorInfo = "未能获取身份信息，请重新登录！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }

                //baseResultDataValue = IBReaBmsInDoc.StatisticalAnalysisBmsInDocByGoodsNo(StartDate, EndDate, CenOrgIdList, GoodsNoList, ComOrgIdList, BmsInDocTypeList, DateType, CenOrgLevel);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("StatisticalAnalysisBmsInDocByGoodsNo.异常：" + ex.ToString());
            }

            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaBmsCenOrderDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string fields, bool isPlanish, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCenOrderDtl> tempEntityList = new EntityList<ReaBmsCenOrderDtl>();
            try
            {
                if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                {
                    baseResultDataValue.ErrorInfo = "统计条件不能为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }

                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID) == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID).Trim() == "")
                {
                    baseResultDataValue.ErrorInfo = "未能获取身份信息，请重新登录！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBReaBmsCenOrderDtl.SearchReaBmsCenOrderDtlSummaryByHQL(groupType, docHql, dtlHql, reaGoodsHql, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsCenOrderDtl>(tempEntityList);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchReaBmsCenOrderDtlSummaryByHQL.异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaBmsCenSaleDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string fields, bool isPlanish, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCenSaleDtl> tempEntityList = new EntityList<ReaBmsCenSaleDtl>();
            try
            {
                if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                {
                    baseResultDataValue.ErrorInfo = "统计条件不能为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }

                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID) == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID).Trim() == "")
                {
                    baseResultDataValue.ErrorInfo = "未能获取身份信息，请重新登录！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBReaBmsCenSaleDtl.SearchReaBmsCenSaleDtlSummaryByHQL(groupType, docHql, dtlHql, reaGoodsHql, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsCenSaleDtl>(tempEntityList);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchReaBmsCenSaleDtlSummaryByHQL.异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaBmsInDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string fields, bool isPlanish, string sort, int page, int limit, bool isOfColdInfoMerge)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsInDtl> tempEntityList = new EntityList<ReaBmsInDtl>();
            try
            {
                if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                {
                    baseResultDataValue.ErrorInfo = "统计条件不能为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }

                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID) == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID).Trim() == "")
                {
                    baseResultDataValue.ErrorInfo = "未能获取身份信息，请重新登录！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBReaBmsInDtl.SearchReaBmsInDtlSummaryByHQL(groupType, docHql, dtlHql, reaGoodsHql, sort, page, limit, isOfColdInfoMerge, true);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsInDtl>(tempEntityList);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchReaBmsInDtlSummaryByHQL.异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchTransferDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string fields, bool isPlanish, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsTransferDtl> tempEntityList = new EntityList<ReaBmsTransferDtl>();
            try
            {
                if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                {
                    baseResultDataValue.ErrorInfo = "统计条件不能为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }

                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID) == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID).Trim() == "")
                {
                    baseResultDataValue.ErrorInfo = "未能获取身份信息，请重新登录！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBReaBmsTransferDtl.SearchReaBmsTransferDtlSummaryByHQL(groupType, docHql, dtlHql, reaGoodsHql, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsTransferDtl>(tempEntityList);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchTransferDtlSummaryByHQL.异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaBmsOutDtlSummaryByHQL(int groupType, string docHql, string dtlHql, string reaGoodsHql, string fields, bool isPlanish, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsOutDtl> tempEntityList = new EntityList<ReaBmsOutDtl>();
            try
            {
                if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                {
                    baseResultDataValue.ErrorInfo = "统计条件不能为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }

                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID) == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID).Trim() == "")
                {
                    baseResultDataValue.ErrorInfo = "未能获取身份信息，请重新登录！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBReaBmsOutDtl.SearchReaBmsOutDtlSummaryByHQL(groupType, docHql, dtlHql, reaGoodsHql, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsOutDtl>(tempEntityList);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchReaBmsOutDtlSummaryByHQL.异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaGoodsStatisticsOfMaxGonvertQtyEntityListHQL(int groupType, string companyId, string deptId, string testEquipId, string reaGoodsNo, string startDate, string endDate, string fields, bool isPlanish, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(companyId) && string.IsNullOrEmpty(deptId) && string.IsNullOrEmpty(testEquipId) && string.IsNullOrEmpty(reaGoodsNo) && string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
                {
                    baseResultDataValue.ErrorInfo = "统计条件不能为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID) == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID).Trim() == "")
                {
                    baseResultDataValue.ErrorInfo = "未能获取身份信息，请重新登录！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                EntityList<ReaGoodsOfMaxGonvertQtyVO> tempEntityList = new EntityList<ReaGoodsOfMaxGonvertQtyVO>();
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBComprehensiveStatistics.SearchReaGoodsStatisticsOfMaxGonvertQtyEntityListHQL(groupType, companyId, deptId, testEquipId, reaGoodsNo, startDate, endDate, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaGoodsOfMaxGonvertQtyVO>(tempEntityList);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchComprehensiveStatistics1HQL.异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaTransferAndOutDtlVOListByHQL(int groupType, string hqlStr, string fields, bool isPlanish, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaTransferAndOutDtlVO> tempEntityList = new EntityList<ReaTransferAndOutDtlVO>();
            try
            {
                if (string.IsNullOrWhiteSpace(hqlStr))
                {
                    baseResultDataValue.ErrorInfo = "统计条件不能为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }

                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID) == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID).Trim() == "")
                {
                    baseResultDataValue.ErrorInfo = "未能获取身份信息，请重新登录！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }

                tempEntityList = IBReaBmsTransferDtl.SearchReaTransferAndOutDtlVOListByHQL(groupType, hqlStr, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaTransferAndOutDtlVO>(tempEntityList);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchReaTransferAndOutDtlVOListByHQL.异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaBmsOutDtlLotNoAndTransportNoChangeByHQL(string docHql, string dtlHql, string reaGoodsHql, string fields, bool isPlanish, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsOutDtl> tempEntityList = new EntityList<ReaBmsOutDtl>();
            try
            {
                if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
                {
                    baseResultDataValue.ErrorInfo = "统计条件不能为空！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }

                if (ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID) == null || ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID).Trim() == "")
                {
                    baseResultDataValue.ErrorInfo = "未能获取身份信息，请重新登录！";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }

                IList<ReaBmsOutDtl> dtlList = IBReaBmsOutDtl.SearchReaBmsOutDtlSummaryListByHQL(docHql, dtlHql, reaGoodsHql, sort, -1, -1);
                tempEntityList.count = dtlList.Count;
                //分页处理
                if (limit > 0 && limit < dtlList.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = dtlList.Skip(startIndex).Take(endIndex);
                    if (list != null)
                        dtlList = list.ToList();
                }
                tempEntityList.list = dtlList;
                
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsOutDtl>(tempEntityList);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchReaBmsOutDtlLotNoAndTransportNoChangeByHQL.异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region Excel导出/PDF预览       
        public Stream RS_UDTO_SearchBusinessSummaryReportOfExcelByHql(long operateType, string breportType, string frx, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string startDate, string endDate, bool isOfColdInfoMerge)
        {
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            Stream fileStream = null;
            string fileName = "";
            string info = "";
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                long empID = long.Parse(employeeID);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                if (breportType == BTemplateType.订单汇总.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.订单汇总.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsCenOrderDtl.SearchBusinessSummaryReportOfExcelByHql(labId, labCName, groupType, docHql, dtlHql, reaGoodsHql, sort, breportType, frx, ref fileName, startDate, endDate);
                }
                else if (breportType == BTemplateType.入库汇总.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.入库汇总.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsInDtl.SearchBusinessSummaryReportOfExcelByHql(labId, labCName, groupType, docHql, dtlHql, reaGoodsHql, sort, breportType, frx, ref fileName, startDate, endDate, isOfColdInfoMerge);
                }
                else if (breportType == BTemplateType.移库汇总.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.移库汇总.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsTransferDtl.SearchBusinessSummaryReportOfExcelByHql(labId, labCName, groupType, docHql, dtlHql, reaGoodsHql, sort, breportType, frx, ref fileName, startDate, endDate);
                }
                else if (breportType == BTemplateType.出库汇总.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.出库汇总.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsOutDtl.SearchBusinessSummaryReportOfExcelByHql(labId, labCName, groupType, docHql, dtlHql, reaGoodsHql, sort, breportType, frx, ref fileName, startDate, endDate);
                }
                else if (breportType == BTemplateType.出库变更台账.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.出库变更台账.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsOutDtl.SearchReaBmsOutDtlLotNoAndTransportNoChangeOfExcelPdfByHQL("Excel", labId, labCName, docHql, dtlHql, reaGoodsHql, sort, breportType, frx, ref fileName, startDate, endDate);
                }

                if (fileStream != null)
                {
                    Encoding code = Encoding.GetEncoding("GB2312");
                    System.Web.HttpContext.Current.Response.Charset = "GB2312";
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    fileName = EncodeFileName.ToEncodeFileName(fileName);
                    if (operateType == 0) //下载文件application/octet-stream
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/ms-excel";// "" + file.FileType;
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                    }
                }
                else
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出" + info + "数据为空!");
                    return memoryStream;
                }
            }
            catch (Exception ex)
            {
                //fileStream = null;
                if (fileStream != null)
                    fileStream.Close();
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出" + info + ex.Message + "错误!");
                return memoryStream;
            }
            return fileStream;
        }
        public Stream RS_UDTO_SearchReaGoodsStatisticsOfMaxGonvertQtyReportOfExcelByHql(long operateType, string breportType, string frx, int groupType, string companyId, string deptId, string testEquipId, string reaGoodsNo, string startDate, string endDate, string sort)
        {
            if (string.IsNullOrEmpty(companyId) && string.IsNullOrEmpty(deptId) && string.IsNullOrEmpty(testEquipId) && string.IsNullOrEmpty(reaGoodsNo) && string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("统计条件信息为空!");
            }
            Stream fileStream = null;
            string fileName = "";
            string info = "";
            try
            {
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }

                long empID = long.Parse(employeeID);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.综合统计.Key];
                info = dicEntity.Name;
                breportType = dicEntity.Name;
                fileStream = IBComprehensiveStatistics.GetReaGoodsStatisticsOfMaxGonvertQtyReportOfExcelByHql(labId, labCName, groupType, companyId, deptId, testEquipId, reaGoodsNo, startDate, endDate, sort, breportType, frx, ref fileName);
                if (fileStream != null)
                {
                    Encoding code = Encoding.GetEncoding("GB2312");
                    System.Web.HttpContext.Current.Response.Charset = "GB2312";
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    fileName = EncodeFileName.ToEncodeFileName(fileName);
                    if (operateType == 0) //下载文件application/octet-stream
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/ms-excel";// "" + file.FileType;
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                    }
                }
                else
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出" + info + "数据为空!");
                    return memoryStream;
                }
            }
            catch (Exception ex)
            {
                //fileStream = null;
                if (fileStream != null)
                    fileStream.Close();
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出" + info + ex.Message + "错误!");
                return memoryStream;
            }
            return fileStream;
        }
        public Stream RS_UDTO_SearchBusinessSummaryReportOfPdfByHql(string reaReportClass, long operateType, string breportType, string frx, int groupType, string docHql, string dtlHql, string reaGoodsHql, string sort, string startDate, string endDate, bool isOfColdInfoMerge)
        {
            if (string.IsNullOrWhiteSpace(docHql) && string.IsNullOrWhiteSpace(dtlHql))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            Stream fileStream = null;
            string fileName = "", info = "";
            try
            {
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }
                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                if (breportType == BTemplateType.订单汇总.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.订单汇总.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsCenOrderDtl.SearchBusinessSummaryReportOfPdfByHql(reaReportClass, labId, labCName, groupType, docHql, dtlHql, reaGoodsHql, sort, breportType, frx, ref fileName, startDate, endDate);
                }
                else if (breportType == BTemplateType.入库汇总.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.入库汇总.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsInDtl.SearchBusinessSummaryReportOfPdfByHql(reaReportClass, labId, labCName, groupType, docHql, dtlHql, reaGoodsHql, sort, breportType, frx, ref fileName, startDate, endDate, isOfColdInfoMerge);
                }
                else if (breportType == BTemplateType.移库汇总.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.移库汇总.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsTransferDtl.SearchBusinessSummaryReportOfPdfByHql(reaReportClass, labId, labCName, groupType, docHql, dtlHql, reaGoodsHql, sort, breportType, frx, ref fileName, startDate, endDate);
                }
                else if (breportType == BTemplateType.出库汇总.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.出库汇总.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsOutDtl.SearchBusinessSummaryReportOfPdfByHql(reaReportClass, labId, labCName, groupType, docHql, dtlHql, reaGoodsHql, sort, breportType, frx, ref fileName, startDate, endDate);
                }
                else if (breportType == BTemplateType.出库变更台账.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.出库变更台账.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsOutDtl.SearchReaBmsOutDtlLotNoAndTransportNoChangeOfExcelPdfByHQL(reaReportClass, labId, labCName, docHql, dtlHql, reaGoodsHql, sort, breportType, frx, ref fileName, startDate, endDate);
                }

                Encoding code = Encoding.GetEncoding("gb2312");
                System.Web.HttpContext.Current.Response.ContentEncoding = code;
                System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                fileName = EncodeFileName.ToEncodeFileName(fileName);
                if (operateType == 0) //下载文件
                {
                    System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                }
                else if (operateType == 1)//直接打开文件
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                    WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                }
                return fileStream;
            }
            catch (Exception ex)
            {
                string errorInfo = "获取" + info + "PDF文件失败!";
                ZhiFang.Common.Log.Log.Error(errorInfo + ex.Message);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                return memoryStream;
            }
        }
        public Stream RS_UDTO_SearchReaGoodsStatisticsOfMaxGonvertQtyReportOfPdfByHql(string reaReportClass, long operateType, string breportType, string frx, int groupType, string companyId, string deptId, string testEquipId, string reaGoodsNo, string startDate, string endDate, string sort)
        {
            if (string.IsNullOrEmpty(companyId) && string.IsNullOrEmpty(deptId) && string.IsNullOrEmpty(testEquipId) && string.IsNullOrEmpty(reaGoodsNo) && string.IsNullOrEmpty(startDate) && string.IsNullOrEmpty(endDate))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("统计条件信息为空!");
            }
            Stream fileStream = null;
            string fileName = "";
            string info = "";
            try
            {
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }

                long empID = long.Parse(employeeID);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.综合统计.Key];
                info = dicEntity.Name;
                breportType = dicEntity.Name;
                fileStream = IBComprehensiveStatistics.GetReaGoodsStatisticsOfMaxGonvertQtyReportOfPdfByHql(reaReportClass, labId, labCName, groupType, companyId, deptId, testEquipId, reaGoodsNo, startDate, endDate, sort, breportType, frx, ref fileName);
                if (fileStream != null)
                {
                    Encoding code = Encoding.GetEncoding("GB2312");
                    System.Web.HttpContext.Current.Response.Charset = "GB2312";
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    fileName = EncodeFileName.ToEncodeFileName(fileName);
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                    }
                }
                else
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出" + info + "数据为空!");
                    return memoryStream;
                }
            }
            catch (Exception ex)
            {
                //fileStream = null;
                if (fileStream != null)
                    fileStream.Close();
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出" + info + "失败," + ex.Message + "错误!");
                return memoryStream;
            }
            return fileStream;
        }
        public Stream RS_UDTO_SearchReaTransferAndOutDtlVOOfExcelByHql(long operateType, string breportType, string frx, int groupType, string hqlStr, string sort)
        {
            if (string.IsNullOrWhiteSpace(hqlStr))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            Stream fileStream = null;
            string fileName = "";
            string info = "";
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                long empID = long.Parse(employeeID);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                if (breportType == BTemplateType.移库及使用.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.移库及使用.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsTransferDtl.SearchReaTransferAndOutDtlVOOfExcelByHql(labId, labCName, groupType, hqlStr, sort, breportType, frx, ref fileName);
                }

                if (fileStream != null)
                {
                    Encoding code = Encoding.GetEncoding("GB2312");
                    System.Web.HttpContext.Current.Response.Charset = "GB2312";
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    fileName = EncodeFileName.ToEncodeFileName(fileName);
                    if (operateType == 0) //下载文件application/octet-stream
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/ms-excel";// "" + file.FileType;
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                    }
                }
                else
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出" + info + "数据为空!");
                    return memoryStream;
                }
            }
            catch (Exception ex)
            {
                //fileStream = null;
                if (fileStream != null)
                    fileStream.Close();
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出" + info + ex.Message + "错误!");
                return memoryStream;
            }
            return fileStream;
        }
        public Stream RS_UDTO_SearchReaTransferAndOutDtlVOOfPdfByHql(string reaReportClass, long operateType, string breportType, string frx, int groupType, string hqlStr, string sort)
        {
            if (string.IsNullOrWhiteSpace(hqlStr))
            {
                return ResponseResultStream.GetErrMemoryStreamInfo("获取统计条件为空!");
            }
            Stream fileStream = null;
            string fileName = "", info = "";
            try
            {
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }
                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                if (breportType == BTemplateType.移库及使用.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.移库及使用.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsTransferDtl.SearchReaTransferAndOutDtlVOOfPdfByHql(reaReportClass, labId, labCName, groupType, hqlStr, sort, breportType, frx, ref fileName);
                }
                Encoding code = Encoding.GetEncoding("gb2312");
                System.Web.HttpContext.Current.Response.ContentEncoding = code;
                System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                fileName = EncodeFileName.ToEncodeFileName(fileName);
                if (operateType == 0) //下载文件
                {
                    System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                }
                else if (operateType == 1)//直接打开文件
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                    WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                }
                return fileStream;
            }
            catch (Exception ex)
            {
                string errorInfo = "获取" + info + "PDF文件失败!";
                ZhiFang.Common.Log.Log.Error(errorInfo + ex.StackTrace);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                return memoryStream;
            }
        }
        #endregion

        #region 消耗比对分析
        public BaseResultDataValue RS_UDTO_SearchReaBmsOutDtEntityListByJoinHql(int groupType, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, string fields, int page, int limit, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsOutDtl> entityList = new EntityList<ReaBmsOutDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBReaBmsOutDtl.SearchReaBmsOutDtEntityListByJoinHql(groupType, docHql, dtlHql, deptGoodsHql, reaGoodsHql, page, limit, sort);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsOutDtl>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchTestStatisticalResultsListByJoinHql(int groupType, string startDate, string endDate, string dtlHql, string fields, int page, int limit, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaLisTestStatisticalResults> entityList = new EntityList<ReaLisTestStatisticalResults>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBReaLisTestStatisticalResults.SearchTestStatisticalResultsEntityListByJoinHql(groupType, startDate, endDate, dtlHql, page, limit, sort);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaLisTestStatisticalResults>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchConsumptionComparisonAnalysisVOListByHql(int groupType, int statisticType, string startDate, string endDate, string equipIdStr, string goodsIdStr, string fields, int page, int limit, string sortType, bool isPlanish, bool isMergeOfItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ConsumptionComparisonAnalysisVO> entityList = new EntityList<ConsumptionComparisonAnalysisVO>();
            try
            {
                entityList = IBReaEquipTestItemReaGoodLink.SearchConsumptionComparisonAnalysisVOListByHql(statisticType, startDate, endDate, equipIdStr, goodsIdStr, page, limit, sortType, isMergeOfItem);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ConsumptionComparisonAnalysisVO>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        #endregion

        #region ECharts图表统计
        public BaseResultDataValue RS_UDTO_SearchStackEChartsVOListByHql(int statisticType, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (statisticType >= 1 && statisticType <= 10)
                {
                    //入库按货品一级分类(堆叠为二级分类)
                    baseResultDataValue = IBReaBmsInDtl.SearchStackInEChartsVOListByHql(statisticType, startDate, endDate, docHql, dtlHql, deptGoodsHql, reaGoodsHql);
                }
                else if (statisticType >= 11 && statisticType <= 20)
                {
                    //库存按货品一级分类(堆叠为二级分类)
                    baseResultDataValue = IBReaBmsQtyDtl.SearchStackQtyEChartsVOListByHql(statisticType, startDate, endDate, dtlHql, deptGoodsHql, reaGoodsHql);
                }
                else if (statisticType >= 21 && statisticType <= 30)
                {
                    //出库按货品一级分类(堆叠为二级分类)
                    baseResultDataValue = IBReaBmsOutDtl.SearchStackOutEChartsVOListByHql(statisticType, startDate, endDate, docHql, dtlHql, deptGoodsHql, reaGoodsHql);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "HQL查询错误：传入的参数(statisticType)值不正确!";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchInEChartsVOListByHql(int statisticType, bool showZero, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<EChartsVO> entityList = new EntityList<EChartsVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBReaBmsInDtl.SearchInEChartsVOListByHql(statisticType, showZero, startDate, endDate, docHql, dtlHql, deptGoodsHql, reaGoodsHql, sort);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<EChartsVO>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchStockEChartsVOListByHql(int statisticType, bool showZero, string startDate, string endDate, string dtlHql, string deptGoodsHql, string reaGoodsHql, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<EChartsVO> entityList = new EntityList<EChartsVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBReaBmsQtyDtl.SearchQtyEChartsVOListByHql(statisticType, showZero, startDate, endDate, dtlHql, deptGoodsHql, reaGoodsHql, sort);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<EChartsVO>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchOutEChartsVOListByHql(int statisticType, bool showZero, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<EChartsVO> entityList = new EntityList<EChartsVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBReaBmsOutDtl.SearchOutEChartsVOListByHql(statisticType, showZero, startDate, endDate, docHql, dtlHql, deptGoodsHql, reaGoodsHql, sort);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<EChartsVO>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchEquipReagUsageEChartsVOByHql(int statisticType, bool showZero, string startDate, string endDate, string docHql, string dtlHql, string deptGoodsHql, string reaGoodsHql, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<EChartsVO> entityList = new EntityList<EChartsVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBReaBmsOutDtl.SearchEquipReagUsageEChartsVOListByHql(statisticType, showZero, startDate, endDate, docHql, dtlHql, deptGoodsHql, reaGoodsHql, sort);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<EChartsVO>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchLisResultsEChartsVOByHql(int statisticType, bool showZero, string startDate, string endDate, string dtlHql, string deptGoodsHql, string reaGoodsHql, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            //EntityList<EChartsVO> entityList = new EntityList<EChartsVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                baseResultDataValue = IBReaLisTestStatisticalResults.SearchLisResultsEChartsVOByHql(statisticType, showZero, startDate, endDate, dtlHql, deptGoodsHql, reaGoodsHql, sort);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearChconsumeTheoryEChartsVOByHql(int statisticType, bool showZero, string startDate, string endDate, string dtlHql, string equipIdStr, string goodsIdStr, string fields, bool isMergeOfItem, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<EChartsVO> entityList = new EntityList<EChartsVO>();
            try
            {
                baseResultDataValue = IBReaEquipTestItemReaGoodLink.SearChconsumeTheoryEChartsVOByHql(statisticType, showZero, startDate, endDate, dtlHql, equipIdStr, goodsIdStr, isMergeOfItem);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearChconsumeComparisonEChartsVOByHql(int statisticType, bool showZero, string startDate, string endDate, string dtlHql, string equipIdStr, string goodsIdStr, string fields, bool isMergeOfItem, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBReaEquipTestItemReaGoodLink.SearChconsumeComparisonEChartsVOByHql(statisticType, showZero, startDate, endDate, dtlHql, equipIdStr, goodsIdStr, isMergeOfItem);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        #endregion
    }
}
