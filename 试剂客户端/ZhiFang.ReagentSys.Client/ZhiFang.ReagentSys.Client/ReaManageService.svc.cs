using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using System.IO;
using System.Data;
using Newtonsoft.Json.Linq;
using ZhiFang.ReagentSys.Client.ServerContract;
using ZhiFang.IBLL.ReagentSys.Client;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ReagentSys.Client;
using ZhiFang.Entity.RBAC;
using ZhiFang.ServiceCommon.RBAC;
using System.ServiceModel.Channels;
using System.Reflection;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.Response;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaStoreIn;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaGoodsScanCode;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaConfirm;
using ZhiFang.IBLL.RBAC;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ZFReaRestful.BmsSaleExtract;
using ZhiFang.Entity.ReagentSys.Client.ViewObject;
using ZhiFang.ReagentSys.Client.Common;
using ZhiFang.Entity.ReagentSys.Client.ViewObject.ReaSale;
using ZhiFang.Common.Public;

namespace ZhiFang.ReagentSys.Client
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ReaManageService : IReaManageService
    {
        #region IBLL
        IBReaGoods IBReaGoods { get; set; }
        IBReaGoodsOrgLink IBReaGoodsOrgLink { get; set; }
        IBLL.RBAC.IBHRDept IBHRDept { get; set; }
        IBReaBmsReqDoc IBReaBmsReqDoc { get; set; }
        IBReaDeptGoods IBReaDeptGoods { get; set; }
        IBReaBmsInDtl IBReaBmsInDtl { get; set; }
        IBReaBmsQtyDtl IBReaBmsQtyDtl { get; set; }
        IBReaBmsOutDoc IBReaBmsOutDoc { get; set; }
        IBReaBmsOutDtl IBReaBmsOutDtl { get; set; }
        IBReaBmsCheckDoc IBReaBmsCheckDoc { get; set; }
        IBReaBmsCheckDtl IBReaBmsCheckDtl { get; set; }
        IBReaCenBarCodeFormat IBReaCenBarCodeFormat { get; set; }
        IBReaEquipReagentLink IBReaEquipReagentLink { get; set; }
        IBReaBmsInDoc IBReaBmsInDoc { get; set; }
        IBReaBmsTransferDoc IBReaBmsTransferDoc { get; set; }
        IBReaGoodsBarcodeOperation IBReaGoodsBarcodeOperation { get; set; }
        IBReaBmsCenSaleDocConfirm IBReaBmsCenSaleDocConfirm { get; set; }
        IBReaBmsCenSaleDtlConfirm IBReaBmsCenSaleDtlConfirm { get; set; }
        IBLL.RBAC.IBRBACUser IBRBACUser { get; set; }
        IBReaBmsCenOrderDoc IBReaBmsCenOrderDoc { get; set; }
        IBReaBmsCenOrderDtl IBReaBmsCenOrderDtl { get; set; }
        IBReaBmsCenSaleDoc IBReaBmsCenSaleDoc { get; set; }
        IBReaBmsCenSaleDtl IBReaBmsCenSaleDtl { get; set; }
        IBReaBmsQtyMonthBalanceDoc IBReaBmsQtyMonthBalanceDoc { get; set; }
        IBReaBmsQtyBalanceDoc IBReaBmsQtyBalanceDoc { get; set; }
        IBReaBmsCenOrderDocOfService IBReaBmsCenOrderDocOfService { get; set; }
        IBCenOrg IBCenOrg { get; set; }
        IBBTemplate IBBTemplate { get; set; }
        IBReaMonthUsageStatisticsDoc IBReaMonthUsageStatisticsDoc { get; set; }
        IBReaLisTestStatisticalResults IBReaLisTestStatisticalResults { get; set; }
        IBReaEquipTestItemReaGoodLink IBReaEquipTestItemReaGoodLink { get; set; }
        IBReaTestEquipItem IBReaTestEquipItem { get; set; }
        IBReaTestEquipLab IBReaTestEquipLab { get; set; }
        IBReaTestItem IBReaTestItem { get; set; }
        IBCSUpdateToBS IBCSUpdateToBS { get; set; }
        IBReaStorage IBReaStorage { get; set; }
        IBReaPlace IBReaPlace { get; set; }
        IBLL.RBAC.IBHREmployee IBHREmployee { get; set; }
        IBBParameter IBBParameter { get; set; }
        IBReaCenOrg IBReaCenOrg { get; set; }
        IBReaGoodsLot IBReaGoodsLot { get; set; }
        IBReaBmsQtyDtlOperation IBReaBmsQtyDtlOperation { get; set; }
        IBReaMonthUsageStatisticsDtl IBReaMonthUsageStatisticsDtl { get; set; }
        IBReaStorageGoodsLink IBReaStorageGoodsLink { get; set; }
        IBReaChooseGoodsTemplate IBReaChooseGoodsTemplate { get; set; }
        IBReaBmsReqDtl IBReaBmsReqDtl { get; set; }
        IBReaOpenBottleOperDoc IBReaOpenBottleOperDoc { get; set; }
        #endregion

        #region 客户端机构维护
        public BaseResultDataValue ST_UDTO_AddCenOrgOfInitialize(long labId, CenOrg cenOrg, RBACUser user, string roleIdStrOfZf, string moduleIdStr)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (cenOrg == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入参数(cenOrg)信息为空!";
                return tempBaseResultDataValue;
            }
            if (user == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入参数(user)信息为空!";
                return tempBaseResultDataValue;
            }
            if (string.IsNullOrEmpty(moduleIdStr.Trim()))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入参数(moduleIdStr)信息为空!";
                return tempBaseResultDataValue;
            }
            string[] tempArr = moduleIdStr.TrimEnd(',').Split(',');
            if (tempArr.Length <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入参数(moduleIdStr)信息为空!";
                return tempBaseResultDataValue;
            }
            try
            {
                if (string.IsNullOrEmpty(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                {
                    tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息异常,请重新登录后再操作!";
                    tempBaseResultDataValue.success = false;
                    return tempBaseResultDataValue;
                }
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                SysPublicSet.IsSetLicense = true;
                tempBaseResultDataValue = IBCenOrg.AddCenOrgOfInitializeOfPlatform(labId, cenOrg, user, roleIdStrOfZf, moduleIdStr, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBCenOrg.Get(IBCenOrg.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBCenOrg.Entity);
                }
            }
            catch (Exception ex)
            {
                SysPublicSet.IsSetLicense = false;
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("机构初始化失败:错误信息为:" + ex.StackTrace);
            }
            SysPublicSet.IsSetLicense = false;
            return tempBaseResultDataValue;
        }
        public BaseResultBool ST_UDTO_UpdateCenOrgAuthorizationModifyOfPlatform(long labId, long cenOrgId, IList<RBACModule> moduleList)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (moduleList == null || moduleList.Count <= 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "传入参数(labId)信息为空!";
                return baseResultBool;
            }
            if (labId <= 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "获取传入参数(labId)信息的值为空!";
                return baseResultBool;
            }
            if (cenOrgId <= 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "获取传入参数(cenOrgId)信息的值为空!";
                return baseResultBool;
            }
            try
            {
                if (string.IsNullOrEmpty(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                {
                    baseResultBool.ErrorInfo = "获取登录帐号信息异常,请重新登录后再操作!";
                    baseResultBool.success = false;
                    return baseResultBool;
                }
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                SysPublicSet.IsSetLicense = true;
                baseResultBool = IBCenOrg.EditCenOrgAuthorizationModifyOfPlatform(labId, cenOrgId, moduleList, empID, empName);
            }
            catch (Exception ex)
            {
                SysPublicSet.IsSetLicense = false;
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("授权变更错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            SysPublicSet.IsSetLicense = false;
            return baseResultBool;
        }
        public BaseResultDataValue ST_UDTO_SearchRBACRoleModuleByLabIDAndSysRoleId(long labId, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (labId <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "获取传入参数(labId)的值为空!";
                return tempBaseResultDataValue;
            }
            EntityList<RBACRoleModule> tempEntityList = new EntityList<RBACRoleModule>();
            try
            {
                tempEntityList = IBCenOrg.SearchRBACRoleModuleByLabIDAndSysRoleId(labId);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<RBACRoleModule>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public Stream ST_UDTO_SearchExportAuthorizationFileOfPlatform(long labId, long cenOrgId, long fileType)
        {
            Stream fileStream = null;
            bool result = true;
            try
            {
                if (string.IsNullOrEmpty(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                {
                    string errorInfo = "获取登录帐号信息异常,请重新登录后再操作!";
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                    return memoryStream;
                }

                string filename = "";
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                fileStream = IBCenOrg.SearchExportAuthorizationFileOfPlatform(labId, cenOrgId, fileType, ref result, ref filename, empID, empName);
                if (result == false) return fileStream;

                Encoding code = Encoding.GetEncoding("UTF-8");
                System.Web.HttpContext.Current.Response.ContentEncoding = code;
                System.Web.HttpContext.Current.Response.HeaderEncoding = code;

                filename = EncodeFileName.ToEncodeFileName(filename);
                System.Web.HttpContext.Current.Response.ContentType = "text/plain";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                return fileStream;
            }
            catch (Exception ex)
            {
                string errorInfo = "导出的授权文件失败!";
                ZhiFang.Common.Log.Log.Error(errorInfo + ex.StackTrace);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                return memoryStream;
            }
        }
        /// <summary>
        /// 客户端授权文件导入
        /// </summary>
        /// <returns></returns>
        public Message ST_UDTO_UploadAuthorizationFileOfClient()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string resultDataValue = IBCenOrg.GetCenOrgDataValue("");

            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                brdv.success = true;
                HttpPostedFile file = null;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到上传的授权文件！";
                    brdv.ResultDataValue = resultDataValue;
                    brdv.success = false;
                    return WebOperationContext.Current.CreateTextResponse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
                }
                if (iTotal > 1)
                {
                    brdv.ErrorInfo = "检测到上传的授权文件存在多个！";
                    brdv.ResultDataValue = resultDataValue;
                    brdv.success = false;
                    return WebOperationContext.Current.CreateTextResponse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
                }

                file = HttpContext.Current.Request.Files[0];
                if (file.ContentLength <= 0)
                {
                    brdv.ErrorInfo = "上传的授权文件内容为空！";
                    brdv.ResultDataValue = resultDataValue;
                    brdv.success = false;
                    return WebOperationContext.Current.CreateTextResponse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
                }
                //if (string.IsNullOrEmpty(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                //{
                //    brdv.ErrorInfo = "获取登录帐号信息异常,请重新登录后再操作!";
                //    brdv.success = false;
                //    return WebOperationContext.Current.CreateTextResponse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
                //}
                long empID = -1;
                string empIdStr = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (!string.IsNullOrEmpty(empIdStr))
                    empID = long.Parse(empIdStr);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                SysPublicSet.IsSetLicense = true;
                brdv = IBCenOrg.AddUploadAuthorizationFileOfClient(file, empID, empName);
                SysPublicSet.IsSetLicense = false;
            }
            catch (Exception ex)
            {
                SysPublicSet.IsSetLicense = false;
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = resultDataValue;
                ZhiFang.Common.Log.Log.Error("授权导入失败，错误信息为：" + ex.StackTrace);
                brdv.success = false;
            }

            return WebOperationContext.Current.CreateTextResponse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
        }
        public BaseResultDataValue ST_UDTO_GetCenOrgInitializeInfo()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            try
            {
                tempBaseResultDataValue = IBCenOrg.GetCenOrgInitializeInfo();
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_SearchLicenseGuideVOByCenOrg(CenOrg cenOrg, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (cenOrg == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "获取传入参数(cenOrg)的值为空!";
                return tempBaseResultDataValue;
            }
            EntityList<LicenseGuideVO> tempEntityList = new EntityList<LicenseGuideVO>();
            try
            {
                tempEntityList = IBCenOrg.SearchLicenseGuideVOByCenOrg(cenOrg, ref tempBaseResultDataValue);

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<LicenseGuideVO>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_AddCenOrgInitializeByStep(CenOrg cenOrg, string entity)
        {
            SysPublicSet.IsSetLicense = true;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (cenOrg == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入参数(cenOrg)信息为空!";
                return tempBaseResultDataValue;
            }
            if (string.IsNullOrEmpty(entity))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入参数(entity)信息为空!";
                return tempBaseResultDataValue;
            }
            try
            {
                tempBaseResultDataValue = IBCenOrg.AddCenOrgInitializeByStep(cenOrg, entity);
            }
            catch (Exception ex)
            {
                SysPublicSet.IsSetLicense = false;
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("机构初始化失败:错误信息为:" + ex.StackTrace);
            }
            SysPublicSet.IsSetLicense = false;
            return tempBaseResultDataValue;
        }
        #endregion

        #region 机构货品维护
        public BaseResultDataValue RS_UDTO_SearchGoodsClassListByClassTypeAndHQL(string classType, string where, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<ReaGoodsClassVO> goodsClassList = IBReaGoods.SearchGoodsClassListByClassTypeAndHQL(classType, false, where, sort, page, limit);

                ParseObjectProperty pop = new ParseObjectProperty();
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(goodsClassList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchGoodsClassEntityListByClassTypeAndHQL(string classType, bool hasNull, bool isPlanish, string fields, string where, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsClassVO> entityList = new EntityList<ReaGoodsClassVO>();
            try
            {
                entityList = IBReaGoods.SearchGoodsClassEntityListByClassTypeAndHQL(classType, false, where, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsClassVO>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 获取最大的时间戳，接口同步货品使用
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue RS_UDTO_GetMaxTS()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.ResultDataValue = IBReaGoods.GetMaxTS();
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region 部门采购
        public BaseResultDataValue RS_UDTO_SearchListByDeptIdAndHQL(long deptId, int page, int limit, string fields, string where, string goodsQty, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaDeptGoods> entityList = new EntityList<ReaDeptGoods>();
            try
            {
                if (fields.Contains("GoodsOtherQty")) fields = fields.Replace(",GoodsOtherQty", "");
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBReaDeptGoods.SearchListByDeptIdAndHQL(deptId, where, goodsQty, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaDeptGoods>(entityList);
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
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchListByDeptIdAndHQL.Error:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_SearchApplyHRDeptByByHRDeptId(int page, int limit, string fields, string where, string sort, bool isPlanish, long deptId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<HRDept> entityList = new EntityList<HRDept>();
            try
            {
                //IList<HRDept> deptList = IBHRDept.SearchHRDeptByHREmpID(empId);
                //deptId = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID));
                IList<HRDept> deptList = IBHRDept.SearchHRDeptListByHRDeptId(deptId);

                entityList.count = deptList.Count;
                //查询处理
                if (!String.IsNullOrEmpty(where))
                {
                    where = where.Replace("(", "");
                    where = where.Replace(")", "");
                    if (where.Contains("CName"))
                    {
                        where = where.Replace(" like ", ";");
                        string[] strArr = where.Split(';');
                        if (strArr.Length == 2 && !string.IsNullOrEmpty(strArr[1]))
                        {
                            //"部门%"
                            var tempList = deptList.Where(p => p.CName.StartsWith(strArr[1]) || p.EName.ToLower().Contains(strArr[1].ToLower()));
                            //tempList = tempList.Where((x, i) => tempList.ToList().FindIndex(z => z.EName == x.EName) == i).ToList();
                            deptList = tempList.ToList();
                        }
                    }
                }
                #region 分页处理
                if (limit < deptList.Count)
                {
                    int startIndex = limit * (page - 1);
                    int endIndex = limit;
                    var list = deptList.Skip(startIndex).Take(endIndex);
                    if (list != null)
                    {
                        deptList = list.ToList();
                    }
                }
                entityList.list = deptList;
                #endregion
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<HRDept>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        //
        public BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkByHRDeptId(long deptId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<ReaGoodsCenOrgVO> entityList = new List<ReaGoodsCenOrgVO>();
                string goodIdStr = IBReaDeptGoods.SearchReaDeptGoodsListByHRDeptId(deptId);
                entityList = IBReaGoodsOrgLink.SearchReaCenOrgGoodsListByGoodIdStr(goodIdStr);

                ParseObjectProperty pop = new ParseObjectProperty();
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList);
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
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchReaGoodsOrgLinkByHRDeptId.Error:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_SearchReaGoodsCurrentQtyByGoodIdStr(string idStr, string goodIdStr)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<ReaGoodsCurrentQtyVO> entityList = new List<ReaGoodsCurrentQtyVO>();
                entityList = IBReaBmsQtyDtl.SearchReaGoodsCurrentQtyByGoodIdStr(idStr, goodIdStr);

                ParseObjectProperty pop = new ParseObjectProperty();
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList);
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
                ZhiFang.Common.Log.Log.Error("获取采购申请试剂的当前库存数量错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_AddReaBmsReqDocAndDt(ReaBmsReqDoc entity, IList<ReaBmsReqDtl> dtAddList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (!entity.ApplyID.HasValue)
                    entity.ApplyID = empID;
                if (string.IsNullOrEmpty(entity.ApplyName))
                    entity.ApplyName = empName;
                if (!entity.OperDate.HasValue)
                    entity.OperDate = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsReqDoc.Entity = entity;
                tempBaseResultDataValue = IBReaBmsReqDoc.AddReaBmsReqDocAndDt(entity, dtAddList, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsReqDoc.Get(IBReaBmsReqDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsReqDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDocAndDt(ReaBmsReqDoc entity, string fields, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList)
        {
            IBReaBmsReqDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsReqDoc.Entity, fields);
                tempBaseResultBool = IBReaBmsReqDoc.UpdateReaBmsReqDocAndDt(entity, tempArray, dtAddList, dtEditList, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_UpdateReaBmsReqDocAndDt:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDtlOfCheck(ReaBmsReqDoc entity, string fields, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList)
        {
            IBReaBmsReqDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsReqDoc.Entity, fields);
                tempBaseResultBool = IBReaBmsReqDoc.UpdateReaBmsReqDtlOfCheck(entity, tempArray, dtAddList, dtEditList, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_UpdateReaBmsReqDtlOfCheck:" + tempBaseResultBool.ErrorInfo);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 部门采购申请审核及撤消审核服务
        /// </summary>
        /// <param name="entity">待审核的主单信息</param>
        /// <param name="dtAddList">审核前的新增的明细集合</param>
        /// <param name="dtEditList">审核前的待更新的明细集合</param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck(ReaBmsReqDoc entity, string fields, IList<ReaBmsReqDtl> dtAddList, IList<ReaBmsReqDtl> dtEditList)
        {
            long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

            IBReaBmsReqDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsReqDoc.Entity, fields);
                tempBaseResultBool = IBReaBmsReqDoc.UpdateReaBmsReqDocAndDtOfCheck(entity, tempArray, dtAddList, dtEditList, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_UpdateReaBmsReqDocAndDtOfCheck:" + tempBaseResultBool.ErrorInfo);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 依据客户端的申请主单(已审核)生成客户端订单信息
        /// </summary>
        /// <param name="idStr">申请主单IDStr</param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr(string idStr, bool commonIsMerge, bool ugentIsMerge, string reaServerLabcCode, string labcName)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(idStr))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "传入的申请主单参数(idStr)为空";
                return tempBaseResultBool;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                long deptID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID));
                string deptName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptName);
                tempBaseResultBool = IBReaBmsReqDoc.AddReaCenOrgReaBmsCenOrderDocOfReaBmsReqDocIDStr(idStr, commonIsMerge, ugentIsMerge, empID, empName, deptID, deptName, reaServerLabcCode, labcName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddReaCenOrgBmsCenOrderDocOfReaBmsReqDocIDStr:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue ST_UDTO_AddReaBmsReqDocAndDtOfCopyApply(long id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                long deptID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID));
                string deptName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptName);
                tempBaseResultDataValue = IBReaBmsReqDoc.AddReaBmsReqDocAndDtOfCopyApply(id, deptID, deptName, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsReqDoc.Get(IBReaBmsReqDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsReqDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddReaBmsReqDocAndDtOfCopyApply:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }

        #endregion

        #region 采购申请(通用)

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDtlByHQLCommon(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsReqDtl> entityList = new EntityList<ReaBmsReqDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsReqDtl.GetReaBmsReqDtlListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsReqDtl.GetReaBmsReqDtlListByHQL(where, "", page, limit);
                }

                //IList<ReaGoods> tempGoodsList = IBReaGoods.SearchListByHQL("Visible=1");
                //foreach (ReaBmsReqDtl dtl in entityList.list)
                //{
                //    var l = tempGoodsList.Where(p => p.Id == dtl.GoodsID).ToList();
                //    if (l.Count > 0)
                //    {
                //        dtl.GoodsSName = l[0].SName;
                //        dtl.GoodsEName = l[0].EName;
                //    }
                //}

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsReqDtl>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        #endregion

        #region 采购申请(智能采购)

        public BaseResultDataValue ST_UDTO_SearchReaBmsReqDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isCalcQty)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsReqDtl> entityList = new EntityList<ReaBmsReqDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsReqDtl.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsReqDtl.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isCalcQty)
                    {
                        #region 计算货品的平均使用量和建议采购量
                        BParameter para1 = IBBParameter.GetParameterByParaNo(SYSParaNo.平均使用量计算月数.Key);
                        BParameter para2 = IBBParameter.GetParameterByParaNo(SYSParaNo.采购系数.Key);

                        if (para1.ParaValue.Trim() == "")
                            para1.ParaValue = "3";

                        if (para2.ParaValue == "")
                            para2.ParaValue = "2.0";

                        foreach (ReaBmsReqDtl dtl in entityList.list)
                        {
                            ReaGoods entityGoods = IBReaBmsReqDtl.CalcAvgUsedAndSuggestPurchaseQty(dtl.GoodsID.Value, para1.ParaValue, para2.ParaValue);
                            if (entityGoods != null)
                            {
                                dtl.AvgUsedQty = entityGoods.AvgUsedQty;
                                dtl.SuggestPurchaseQty = entityGoods.SuggestPurchaseQty;
                            }
                        }
                        #endregion
                    }
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsReqDtl>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue RS_UDTO_SearchGoodsByDeptIdAndTemplateIdByHQL(long deptId, long templateId, string fields, bool isPlanish, bool isCalcQty)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaDeptGoods> entityList = new EntityList<ReaDeptGoods>();
            if (deptId == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "传入的参数部门deptId为0！";
                return baseResultDataValue;
            }
            if (templateId == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "传入的参数模板templateId为0！";
                return baseResultDataValue;
            }
            try
            {
                string where = "(( readeptgoods.ReaGoods.Visible=1 and readeptgoods.DeptID=" + deptId + ") and  readeptgoods.ReaGoods.Visible=1 and readeptgoods.Visible=1)";
                #region 如果模板templateId>0，获取模板里的货品ID，只查询这些货品
                string goodsIdStr = "";
                if (templateId > 0)
                {
                    //获取模板表里的货品ID
                    ReaChooseGoodsTemplate entityTemplate = IBReaChooseGoodsTemplate.Get(templateId);
                    string contextJson = entityTemplate.ContextJson;
                    if (!string.IsNullOrWhiteSpace(contextJson))
                    {
                        contextJson = contextJson.Replace("'", "\"");
                        try
                        {
                            JArray arr = JArray.Parse(contextJson);
                            var goodsIdArr = arr.Select(p => p["ReaBmsReqDtl_GoodsID"].ToString().Trim()).Distinct().ToArray<string>();
                            goodsIdStr = string.Join(",", goodsIdArr);
                        }
                        catch (Exception ex)
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "解析模板JSON格式异常：" + ex.Message;
                            return baseResultDataValue;
                        }
                    }
                }
                if (goodsIdStr.Trim() != "")
                {
                    where += string.Format(" and ReaGoods.Id in ({0})", goodsIdStr);
                }
                #endregion

                entityList = IBReaDeptGoods.SearchListByDeptIdAndHQL(deptId, where, "", "", 0, 0);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isCalcQty)
                    {
                        #region 计算货品的平均使用量和建议采购量
                        BParameter para1 = IBBParameter.GetParameterByParaNo(SYSParaNo.平均使用量计算月数.Key);
                        BParameter para2 = IBBParameter.GetParameterByParaNo(SYSParaNo.采购系数.Key);

                        if (para1.ParaValue.Trim() == "")
                            para1.ParaValue = "3";

                        if (para2.ParaValue == "")
                            para2.ParaValue = "2.0";

                        foreach (ReaDeptGoods deptGoods in entityList.list)
                        {
                            ReaGoods entityGoods = IBReaBmsReqDtl.CalcAvgUsedAndSuggestPurchaseQty(deptGoods.ReaGoods.Id, para1.ParaValue, para2.ParaValue);
                            if (entityGoods != null)
                            {
                                deptGoods.ReaGoods.AvgUsedQty = entityGoods.AvgUsedQty;
                                deptGoods.ReaGoods.SuggestPurchaseQty = entityGoods.SuggestPurchaseQty;
                            }
                        }
                        #endregion
                    }
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaDeptGoods>(entityList);
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
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchGoodsByDeptIdAndTemplateIdByHQL.Error:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 计算平均使用量和建议采购量
        /// </summary>
        public BaseResultDataValue ST_UDTO_CalcAvgUsedAndSuggestPurchaseQty(long goodsId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (goodsId <= 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "参数错误，goodsId不能为0";
                    return baseResultDataValue;
                }
                BParameter para1 = IBBParameter.GetParameterByParaNo(SYSParaNo.平均使用量计算月数.Key);
                BParameter para2 = IBBParameter.GetParameterByParaNo(SYSParaNo.采购系数.Key);

                if (para1.ParaValue.Trim() == "")
                    para1.ParaValue = "3";

                if (para2.ParaValue == "")
                    para2.ParaValue = "2.0";

                ReaGoods reaGoods = IBReaBmsReqDtl.CalcAvgUsedAndSuggestPurchaseQty(goodsId, para1.ParaValue, para2.ParaValue);
                baseResultDataValue.ResultDataValue = "{\"AvgUsedQty\":\"" + reaGoods.AvgUsedQty + "\",\"SuggestPurchaseQty\":\"" + reaGoods.SuggestPurchaseQty + "\"}";
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        #endregion

        #region 客户端订单

        public BaseResultDataValue ST_UDTO_SearchReaBmsCenOrderDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, long orderDocId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCenOrderDtl> entityList = new EntityList<ReaBmsCenOrderDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsCenOrderDtl.GetReaBmsCenOrderDtlListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit, orderDocId);
                }
                else
                {
                    entityList = IBReaBmsCenOrderDtl.GetReaBmsCenOrderDtlListByHQL(where, "", page, limit, orderDocId);
                }
                
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsCenOrderDtl>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_AddReaBmsCenOrderDocAndDt(ReaBmsCenOrderDoc entity, IList<ReaBmsCenOrderDtl> dtAddList, int otype)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (entity.LabID <= 0)
                {
                    long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                    entity.LabID = labID;
                }
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                entity.DeptName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptName);
                if (!entity.DeptID.HasValue)
                    entity.DeptID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID));

                entity.UserID = empID;
                if (string.IsNullOrEmpty(entity.UserName))
                    entity.UserName = empName;
                if (!entity.OperDate.HasValue)
                    entity.OperDate = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;

                IBReaBmsCenOrderDoc.Entity = entity;
                tempBaseResultDataValue = IBReaBmsCenOrderDoc.AddReaBmsCenOrderDocAndDt(IBReaBmsCenOrderDoc.Entity, dtAddList, otype, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsCenOrderDoc.Get(IBReaBmsCenOrderDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsCenOrderDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_AddReaBmsCenOrderDocAndDt:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool ST_UDTO_UpdateReaBmsCenOrderDocAndDt(ReaBmsCenOrderDoc entity, string fields, IList<ReaBmsCenOrderDtl> dtAddList, IList<ReaBmsCenOrderDtl> dtEditList, string checkIsUploaded)
        {
            if (string.IsNullOrEmpty(entity.StatusName) && entity.Status > 0)
            {
                entity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
                if (!fields.Contains("StatusName")) fields = fields + ",StatusName";
                if (!fields.Contains("DeptName")) fields = fields + ",DeptName";
            }
            if (string.IsNullOrEmpty(entity.DeptName))
                entity.DeptName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptName);
            IBReaBmsCenOrderDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                entity.LabID = labID;

                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCenOrderDoc.Entity, fields);
                tempBaseResultBool = IBReaBmsCenOrderDoc.EditReaBmsCenOrderDocAndDt(entity, tempArray, dtAddList, dtEditList, checkIsUploaded, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_UpdateReaBmsCenOrderDocAndDt:" + ex.Message + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool RS_UDTO_UpdateReaBmsCenOrderDocByPay(ReaBmsCenOrderDoc entity, string fields)
        {
            if (string.IsNullOrEmpty(entity.StatusName) && entity.Status > 0)
            {
                entity.StatusName = ReaBmsOrderDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
                //if (!fields.Contains("StatusName")) fields = fields + ",StatusName";
            }

            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                //entity.LabID = labID;

                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                entity.PayUserId = empID;
                entity.PayUserCName = empName;
                entity.PayTime = DateTime.Now;
                IBReaBmsCenOrderDoc.Entity = entity;
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCenOrderDoc.Entity, fields);
                tempBaseResultBool = IBReaBmsCenOrderDoc.EditReaBmsCenOrderDocByPay(entity, tempArray, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_UpdateReaBmsCenOrderDocByPay:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool ST_UDTO_DelReaBmsCenOrderDtl(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsCenOrderDtl.Entity = IBReaBmsCenOrderDtl.Get(id);
                //是否是申请明细转换生成的订单明细(需要依订单明细的ID到申请明细里查询)

                if (IBReaBmsCenOrderDtl.Entity != null)
                {
                    long labid = IBReaBmsCenOrderDtl.Entity.LabID;
                    string entityName = IBReaBmsCenOrderDtl.Entity.GetType().Name;
                    long docID = IBReaBmsCenOrderDtl.Entity.OrderDocID;
                    baseResultBool.success = IBReaBmsCenOrderDtl.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        IBReaBmsCenOrderDoc.EditReaBmsCenOrderDocTotalPrice(docID);
                    }
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }

        #endregion

        #region 客户端手工验收
        public BaseResultDataValue ST_UDTO_AddReaSaleDocConfirmOfManualInput(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (entity == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：参数entity值为空!";
                return tempBaseResultDataValue;
            }
            if (dtAddList == null && dtAddList.Count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：参数dtAddList值为空!";
                return tempBaseResultDataValue;
            }

            //是否需要验收双确认(当验收单的状态为确认验收并且secAccepterType=3时需要)
            if (entity.Status == int.Parse(ReaBmsCenSaleDocConfirmStatus.已验收.Key) && secAccepterType == int.Parse(ConfirmSecAccepterType.供应商或实验室.Key))
            {
                if (string.IsNullOrEmpty(secAccepterAccount) || string.IsNullOrEmpty(secAccepterPwd))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：次验收人账号或密码不能为空！";
                    return tempBaseResultDataValue;
                }
                IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(secAccepterAccount);
                if (tempRBACUser.Count >= 1)
                {
                    BaseResultBool tempBaseResultBool = IBReaBmsCenSaleDocConfirm.EditOfJudgeIsSameOrg(secAccepterType, entity.ReaCompID.Value.ToString(), secAccepterAccount, secAccepterPwd, tempRBACUser[0]);
                    if (!tempBaseResultBool.success)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                        return tempBaseResultDataValue;
                    }

                    entity.SecAccepterID = tempRBACUser[0].HREmployee.Id;
                    entity.SecAccepterName = tempRBACUser[0].HREmployee.CName;
                    entity.SecAcceptTime = DateTime.Now;
                }
                else if (tempRBACUser.Count <= 0)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：次验收人账号不存在！";
                    return tempBaseResultDataValue;
                }
            }

            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                IBReaBmsCenSaleDocConfirm.Entity = entity;
                tempBaseResultDataValue = IBReaBmsCenSaleDocConfirm.AddReaSaleDocConfirmOfManualInput(entity, dtAddList, secAccepterType, codeScanningMode, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsCenSaleDocConfirm.Get(IBReaBmsCenSaleDocConfirm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsCenSaleDocConfirm.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool ST_UDTO_UpdateReaSaleDocConfirmOfManualInput(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, IList<ReaSaleDtlConfirmVO> dtEditList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode, string fields, string fieldsDtl)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数entity值为空!";
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(fields))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数fields值为空!";
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(fieldsDtl))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数fieldsDtl值为空!";
                return tempBaseResultBool;
            }
            //是否需要验收双确认(当验收单的状态为确认验收并且secAccepterType=3时需要)
            if (entity.Status == int.Parse(ReaBmsCenSaleDocConfirmStatus.已验收.Key) && secAccepterType == int.Parse(ConfirmSecAccepterType.供应商或实验室.Key))
            {
                if (string.IsNullOrEmpty(secAccepterAccount) || string.IsNullOrEmpty(secAccepterPwd))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：次验收人账号或密码不能为空！";
                    return tempBaseResultBool;
                }
                IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(secAccepterAccount);
                if (tempRBACUser.Count >= 1)
                {
                    tempBaseResultBool = IBReaBmsCenSaleDocConfirm.EditOfJudgeIsSameOrg(secAccepterType, entity.ReaCompID.Value.ToString(), secAccepterAccount, secAccepterPwd, tempRBACUser[0]);
                    if (!tempBaseResultBool.success)
                        return tempBaseResultBool;

                    entity.SecAccepterID = tempRBACUser[0].HREmployee.Id;
                    entity.SecAccepterName = tempRBACUser[0].HREmployee.CName;
                    entity.SecAcceptTime = DateTime.Now;
                    if (!fields.Contains("SecAccepterID")) fields = fields + ",SecAccepterID";
                    if (!fields.Contains("SecAccepterName")) fields = fields + ",SecAccepterName";
                    if (!fields.Contains("SecAcceptTime")) fields = fields + ",SecAcceptTime";
                }
                else if (tempRBACUser.Count <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：次验收人账号不存在！";
                    return tempBaseResultBool;
                }
            }

            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (string.IsNullOrEmpty(entity.StatusName) && entity.Status > 0)
                {
                    entity.StatusName = ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[entity.Status.ToString()].Name;
                    if (!fields.Contains("StatusName")) fields = fields + ",StatusName";
                }
                if (!fields.Contains("TotalPrice")) fields = fields + ",TotalPrice";
                entity.TotalPrice = 0;
                if (dtAddList != null && dtAddList.Count > 0)
                {
                    entity.TotalPrice = dtAddList.Sum(p => p.ReaBmsCenSaleDtlConfirm.SumTotal);
                }
                if (dtEditList != null && dtEditList.Count > 0)
                {
                    entity.TotalPrice = entity.TotalPrice + dtEditList.Sum(p => p.ReaBmsCenSaleDtlConfirm.SumTotal);
                }
                IBReaBmsCenSaleDocConfirm.Entity = entity;
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCenSaleDocConfirm.Entity, fields);
                tempBaseResultBool = IBReaBmsCenSaleDocConfirm.EditReaSaleDocConfirmOfManualInput(tempArray, dtAddList, dtEditList, secAccepterType, codeScanningMode, empID, empName, fieldsDtl);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultBool;
        }
        #endregion

        #region 订单验收
        public Message RS_UDTO_UploadSupplyReaOrderDataByExcel()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string fields = "ReaOrderDtlOfConfirmVO_BarCodeType,ReaOrderDtlOfConfirmVO_ReaGoodsName,ReaOrderDtlOfConfirmVO_DtlGoodsQty,ReaOrderDtlOfConfirmVO_GoodsQty,ReaOrderDtlOfConfirmVO_ReceivedCount,ReaOrderDtlOfConfirmVO_RejectedCount,ReaOrderDtlOfConfirmVO_Price,ReaOrderDtlOfConfirmVO_SumTotal,ReaOrderDtlOfConfirmVO_GoodsUnit,ReaOrderDtlOfConfirmVO_UnitMemo,ReaOrderDtlOfConfirmVO_BiddingNo,ReaOrderDtlOfConfirmVO_Id,ReaOrderDtlOfConfirmVO_ReaGoodsID,ReaOrderDtlOfConfirmVO_LabcGoodsLinkID,ReaOrderDtlOfConfirmVO_CompGoodsLinkID,ReaOrderDtlOfConfirmVO_LotSerial,ReaOrderDtlOfConfirmVO_LotNo,ReaOrderDtlOfConfirmVO_ReaGoods_ApproveDocNo,ReaOrderDtlOfConfirmVO_ReaGoods_RegistNo,ReaOrderDtlOfConfirmVO_ReaGoods_RegistDate,ReaOrderDtlOfConfirmVO_ReaGoods_RegistNoInvalidDate,ReaOrderDtlOfConfirmVO_ConfirmCount,ReaOrderDtlOfConfirmVO_AcceptFlag,ReaOrderDtlOfConfirmVO_OrderDocID,ReaOrderDtlOfConfirmVO_OrderDocNo,ReaOrderDtlOfConfirmVO_ReaBmsCenSaleDtlConfirmLinkVOListStr,ReaOrderDtlOfConfirmVO_ReaGoods_EName,ReaOrderDtlOfConfirmVO_ReaGoods_SName,ReaOrderDtlOfConfirmVO_ReaGoodsNo,ReaOrderDtlOfConfirmVO_ProdGoodsNo,ReaOrderDtlOfConfirmVO_CenOrgGoodsNo,ReaOrderDtlOfConfirmVO_GoodsNo,ReaOrderDtlOfConfirmVO_ReaGoods_GoodsSort,ReaOrderDtlOfConfirmVO_ProdDate,ReaOrderDtlOfConfirmVO_InvalidDate,ReaOrderDtlOfConfirmVO_RegisterNo,ReaOrderDtlOfConfirmVO_StorageType,ReaOrderDtlOfConfirmVO_SaleGoodsQty,ReaOrderDtlOfConfirmVO_ReaCompID,ReaOrderDtlOfConfirmVO_CompanyName,ReaOrderDtlOfConfirmVO_ReaServerCompCode,ReaOrderDtlOfConfirmVO_ReaCompCode";
            bool isPlanish = true;
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到文件！";
                    brdv.ResultDataValue = "";
                    brdv.success = false;
                    return WebOperationContext.Current.CreateTextResponse(Common.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
                }
                string[] allkey = HttpContext.Current.Request.Form.AllKeys;
                for (int i = 0; i < allkey.Length; i++)
                {
                    switch (allkey[i])
                    {
                        case "dtlfields":
                            if (!string.IsNullOrEmpty(HttpContext.Current.Request.Form["dtlfields"]))
                                fields = HttpContext.Current.Request.Form["dtlfields"];
                            break;
                        default:
                            break;
                    }
                }
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                int len = file.ContentLength;
                if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string[] temp = file.FileName.Split('.');
                    if (temp[temp.Length - 1].ToLower() != "xlsx" && temp[temp.Length - 1].ToLower() != "xls")
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "错误信息：只能上传Excel格式的原件!";
                        ZhiFang.Common.Log.Log.Error(brdv.ErrorInfo);
                        return WebOperationContext.Current.CreateTextResponse(Common.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
                    }
                }
                else
                {
                    brdv.ErrorInfo = "文件大小为0或为空！";
                    brdv.success = false;
                    return WebOperationContext.Current.CreateTextResponse(Common.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
                };
                //解析上传的Excel文件
                long labID = long.Parse(ZhiFang.ReagentSys.Client.Common.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labName = ZhiFang.ReagentSys.Client.Common.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                if (string.IsNullOrEmpty(labName))
                    labName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(Entity.Base.SysPublicSet.SysDicCookieSession.LabName);
                ZhiFang.Common.Log.Log.Debug("订单验收.供货导入.LabID:" + labID + ",labName:" + labName);
                EntityList<ReaOrderDtlOfConfirmVO> entityList = new EntityList<ReaOrderDtlOfConfirmVO>();
                entityList.list = new List<ReaOrderDtlOfConfirmVO>();
                brdv = IBReaBmsCenOrderDoc.UploadSupplyReaOrderDataByExcel(file, labID, labName, ref entityList);
                if (brdv.success == true && entityList.count > 0)
                {
                    ZhiFang.Common.Log.Log.Debug("订单验收.供货导入.fields:" + fields);
                    ParseObjectProperty pop = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            brdv.ResultDataValue = pop.GetObjectListPlanish<ReaOrderDtlOfConfirmVO>(entityList);
                        }
                        else
                        {
                            brdv.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                        }
                    }
                    catch (Exception ex)
                    {
                        brdv.success = false;
                        brdv.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                        ZhiFang.Common.Log.Log.Error("订单验收.供货导入.Error.fields:" + fields);
                        ZhiFang.Common.Log.Log.Error("订单验收.供货导入.Error.StackTrace:" + ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = "";
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("订单验收.供货导入.Error.StackTrace2:" + ex.StackTrace);
            }
            return WebOperationContext.Current.CreateTextResponse(Common.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
        } //
        public BaseResultDataValue ST_UDTO_SearchReaOrderDtlOfConfirmVOByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaOrderDtlOfConfirmVO> entityList = new EntityList<ReaOrderDtlOfConfirmVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsCenOrderDtl.SearchReaOrderDtlOfConfirmVOListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsCenOrderDtl.SearchReaOrderDtlOfConfirmVOListByHQL(where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaOrderDtlOfConfirmVO>(entityList);
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
                ZhiFang.Common.Log.Log.Error("订单验收提取错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_AddReaSaleDocConfirmOfOrder(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //是否需要验收双确认(当验收单的状态为确认验收并且secAccepterType=3时需要)
            if (entity.Status == int.Parse(ReaBmsCenSaleDocConfirmStatus.已验收.Key) && secAccepterType == int.Parse(ConfirmSecAccepterType.供应商或实验室.Key))
            {
                if (string.IsNullOrEmpty(secAccepterAccount) || string.IsNullOrEmpty(secAccepterPwd))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：次验收人账号或密码不能为空！";
                    return tempBaseResultDataValue;
                }
                IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(secAccepterAccount);
                if (tempRBACUser.Count >= 1)
                {
                    BaseResultBool tempBaseResultBool = IBReaBmsCenSaleDocConfirm.EditOfJudgeIsSameOrg(secAccepterType, entity.ReaCompID.Value.ToString(), secAccepterAccount, secAccepterPwd, tempRBACUser[0]);
                    if (!tempBaseResultBool.success)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                        return tempBaseResultDataValue;
                    }

                    entity.SecAccepterID = tempRBACUser[0].HREmployee.Id;
                    entity.SecAccepterName = tempRBACUser[0].HREmployee.CName;
                    entity.SecAcceptTime = DateTime.Now;
                }
                else if (tempRBACUser.Count <= 0)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：次验收人账号不存在！";
                    return tempBaseResultDataValue;
                }
            }

            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                IBReaBmsCenSaleDocConfirm.Entity = entity;
                tempBaseResultDataValue = IBReaBmsCenSaleDocConfirm.AddReaSaleDocConfirmOfOrder(dtAddList, secAccepterType, codeScanningMode, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsCenSaleDocConfirm.Get(IBReaBmsCenSaleDocConfirm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsCenSaleDocConfirm.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool ST_UDTO_UpdateReaSaleDocConfirmOfOrder(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, IList<ReaSaleDtlConfirmVO> dtEditList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode, string fields, string fieldsDtl)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数entity值为空!";
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(fields))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数fields值为空!";
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(fieldsDtl))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数fieldsDtl值为空!";
                return tempBaseResultBool;
            }
            //是否需要验收双确认(当验收单的状态为确认验收并且secAccepterType=3时需要)
            if (entity.Status == int.Parse(ReaBmsCenSaleDocConfirmStatus.已验收.Key) && secAccepterType == int.Parse(ConfirmSecAccepterType.供应商或实验室.Key))
            {
                if (string.IsNullOrEmpty(secAccepterAccount) || string.IsNullOrEmpty(secAccepterPwd))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：次验收人账号或密码不能为空！";
                    return tempBaseResultBool;
                }
                IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(secAccepterAccount);
                if (tempRBACUser.Count >= 1)
                {
                    tempBaseResultBool = IBReaBmsCenSaleDocConfirm.EditOfJudgeIsSameOrg(secAccepterType, entity.ReaCompID.Value.ToString(), secAccepterAccount, secAccepterPwd, tempRBACUser[0]);
                    if (!tempBaseResultBool.success)
                        return tempBaseResultBool;

                    entity.SecAccepterID = tempRBACUser[0].HREmployee.Id;
                    entity.SecAccepterName = tempRBACUser[0].HREmployee.CName;
                    entity.SecAcceptTime = DateTime.Now;
                    if (!fields.Contains("SecAccepterID")) fields = fields + ",SecAccepterID";
                    if (!fields.Contains("SecAccepterName")) fields = fields + ",SecAccepterName";
                    if (!fields.Contains("SecAcceptTime")) fields = fields + ",SecAcceptTime";
                }
                else if (tempRBACUser.Count <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：次验收人账号不存在！";
                    return tempBaseResultBool;
                }
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (string.IsNullOrEmpty(entity.StatusName) && entity.Status > 0)
                {
                    entity.StatusName = ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[entity.Status.ToString()].Name;
                    if (!fields.Contains("StatusName")) fields = fields + ",StatusName";
                }
                if (!fields.Contains("TotalPrice")) fields = fields + ",TotalPrice";
                entity.TotalPrice = 0;
                if (dtAddList != null && dtAddList.Count > 0)
                {
                    entity.TotalPrice = dtEditList.Sum(p => p.ReaBmsCenSaleDtlConfirm.SumTotal);
                }
                if (dtEditList != null && dtEditList.Count > 0)
                {
                    entity.TotalPrice = entity.TotalPrice + dtEditList.Sum(p => p.ReaBmsCenSaleDtlConfirm.SumTotal);
                }
                IBReaBmsCenSaleDocConfirm.Entity = entity;
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCenSaleDocConfirm.Entity, fields);
                tempBaseResultBool = IBReaBmsCenSaleDocConfirm.EditReaSaleDocConfirmOfOrder(tempArray, dtAddList, dtEditList, secAccepterType, codeScanningMode, empID, empName, fieldsDtl);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool ST_UDTO_SearchOrderIsConfirmOfByOrderId(long orderId, long confirmId)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.SearchOrderIsConfirmOfByOrderId(orderId, confirmId);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultBool;
        }

        #endregion

        #region 订单管理-订单申请-模板选择
        public BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, bool isGetGoodsQty, long templateId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsOrgLink> entityList = new EntityList<ReaGoodsOrgLink>();
            try
            {
                string goodsIdStr = "";
                if (templateId > 0)
                {
                    //获取模板表里的货品ID
                    ReaChooseGoodsTemplate entityTemplate = IBReaChooseGoodsTemplate.Get(templateId);
                    string contextJson = entityTemplate.ContextJson;
                    if (!string.IsNullOrWhiteSpace(contextJson))
                    {
                        contextJson = contextJson.Replace("'", "\"");
                        try
                        {
                            JArray arr = JArray.Parse(contextJson);
                            var goodsIdArr = arr.Select(p => p["ReaBmsCenOrderDtl_ReaGoodsID"].ToString().Trim()).Distinct().ToArray<string>();
                            goodsIdStr = string.Join(",", goodsIdArr);
                        }
                        catch (Exception ex)
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "解析模板JSON格式异常：" + ex.Message;
                            return baseResultDataValue;
                        }
                    }
                }
                if (goodsIdStr.Trim() != "")
                {
                    where += string.Format(" and ReaGoods.Id in ({0})", goodsIdStr);
                }

                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaGoodsOrgLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaGoodsOrgLink.SearchListByHQL(where, page, limit);
                }

                if (isGetGoodsQty)
                {
                    //获取库存数
                    string goodIdStr = string.Join(",", entityList.list.Where(p => p.ReaGoods != null).Select(p => p.ReaGoods.Id).ToArray());
                    IList<ReaGoodsCurrentQtyVO> goodsQtyList = IBReaBmsQtyDtl.SearchReaGoodsCurrentQtyByGoodIdStr("", goodIdStr);
                    foreach (ReaGoodsOrgLink link in entityList.list)
                    {
                        if (link.ReaGoods != null)
                        {
                            var temp = goodsQtyList.Where(p => p.CurGoodsId == link.ReaGoods.Id).ToList();
                            if (temp.Count > 0)
                            {
                                link.ReaGoods.CurrentQty = temp[0].CurrentQty;
                                link.ReaGoods.GoodsQty = temp[0].GoodsQty;
                            }
                        }
                    }
                }

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsOrgLink>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        #endregion

        #region 供货验收
        public BaseResultBool ST_UDTO_SearchIsExistsReaBmsCenSaleDocByReaCompIDAndSaleDocNo(long reaCompID, string saleDocNo)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (string.IsNullOrEmpty(saleDocNo))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "供货单号为空!";
                return tempBaseResultBool;
            }
            try
            {
                IList<ReaBmsCenSaleDoc> tempList = IBReaBmsCenSaleDoc.SearchListByHQL("reabmscensaledoc.DeleteFlag!=1 and reabmscensaledoc.CompID=" + reaCompID + " and reabmscensaledoc.SaleDocNo='" + saleDocNo + "'");
                if (tempList.Count > 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "供货单号为:" + saleDocNo + ",已存在,请不要重复提取!";
                }
                else
                {
                    tempBaseResultBool.success = true;
                    tempBaseResultBool.ErrorInfo = "";
                }

            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue ST_UDTO_SearchReaGoodsOrgLinkByReaCompIDAndGoodsNoStr(long reaCompID, string goodsNoStr)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(goodsNoStr))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "货品平台编码信息为空!";
                return baseResultDataValue;
            }
            try
            {
                IList<ReaGoodsVO> resultList = IBReaGoodsOrgLink.SearchReaGoodsOrgLinkByReaCompIDAndGoodsNoStr(reaCompID, goodsNoStr);
                ParseObjectProperty pop = new ParseObjectProperty();
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(resultList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultBool ST_UDTO_SearchReaSaleIsConfirmOfBySaleDocID(long saleDocID, long confirmId)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool = IBReaBmsCenSaleDtlConfirm.SearchReaSaleIsConfirmOfBySaleDocID(saleDocID, confirmId);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultBool;
        }
        public BaseResultBool ST_UDTO_GetLocalReaSaleDocOfConfirmBySaleDocNo(string reaServerCompCode, string saleDocNo, string reaServerLabcCode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                tempBaseResultBool = IBReaBmsCenSaleDoc.SearchLocalReaSaleDocOfConfirmBySaleDocNo(reaServerCompCode, saleDocNo, reaServerLabcCode, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误：" + ex.Message;
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlOfConfirmVOListBySaleDocID(int page, int limit, string fields, string where, string sort, bool isPlanish, long saleDocID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaSaleDtlOfConfirmVO> entityList = new EntityList<ReaSaleDtlOfConfirmVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsCenSaleDtl.SearchReaBmsCenSaleDtlOfConfirmVOListBySaleDocID(where, CommonServiceMethod.GetSortHQL(sort), page, limit, saleDocID);
                }
                else
                {
                    entityList = IBReaBmsCenSaleDtl.SearchReaBmsCenSaleDtlOfConfirmVOListBySaleDocID(where, "", page, limit, saleDocID);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaSaleDtlOfConfirmVO>(entityList);
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
                ZhiFang.Common.Log.Log.Error("供货验收,获取某一供货单的供货明细集合信息(可验收数大于0)错误信息:" + ex.Message + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_AddReaSaleDocConfirmOfSale(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //是否需要验收双确认(当验收单的状态为确认验收并且secAccepterType=3时需要)
            if (entity.Status == int.Parse(ReaBmsCenSaleDocConfirmStatus.已验收.Key) && secAccepterType == int.Parse(ConfirmSecAccepterType.供应商或实验室.Key))
            {
                if (string.IsNullOrEmpty(secAccepterAccount) || string.IsNullOrEmpty(secAccepterPwd))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：次验收人账号或密码不能为空！";
                    return tempBaseResultDataValue;
                }
                IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(secAccepterAccount);
                if (tempRBACUser.Count >= 1)
                {
                    BaseResultBool tempBaseResultBool = IBReaBmsCenSaleDocConfirm.EditOfJudgeIsSameOrg(secAccepterType, entity.ReaCompID.Value.ToString(), secAccepterAccount, secAccepterPwd, tempRBACUser[0]);
                    if (!tempBaseResultBool.success)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                        return tempBaseResultDataValue;
                    }

                    entity.SecAccepterID = tempRBACUser[0].HREmployee.Id;
                    entity.SecAccepterName = tempRBACUser[0].HREmployee.CName;
                    entity.SecAcceptTime = DateTime.Now;
                }
                else if (tempRBACUser.Count <= 0)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：次验收人账号不存在！";
                    return tempBaseResultDataValue;
                }
            }

            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                IBReaBmsCenSaleDocConfirm.Entity = entity;
                tempBaseResultDataValue = IBReaBmsCenSaleDocConfirm.AddReaSaleDocConfirmOfSale(dtAddList, secAccepterType, codeScanningMode, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    #region 更新订单的单据状态
                    if (entity.Status == int.Parse(ReaBmsCenSaleDtlConfirmStatus.已验收.Key))
                    {
                        ReaBmsCenSaleDoc saleDoc = IBReaBmsCenSaleDoc.Get(entity.SaleDocID.Value);
                        long orderDocID = (saleDoc.OrderDocID == null ? 0 : saleDoc.OrderDocID.Value);
                        if (orderDocID > 0)
                        {
                            ZhiFang.Common.Log.Log.Info("供货验收-确认验收后，同步更新其所属订单[orderDocID=" + orderDocID + "]的单据状态");
                            string hqlWhere = string.Format("SaleDocConfirmID in (select Id from ReaBmsCenSaleDocConfirm where SaleDocID in (select Id from ReaBmsCenSaleDoc where OrderDocID={0}))", orderDocID);
                            IList<ReaBmsCenSaleDtlConfirm> tempDtlConfirmList = IBReaBmsCenSaleDtlConfirm.SearchListByHQL(hqlWhere);
                            IBReaBmsCenOrderDoc.EditReaBmsCenOrderDocStatus(orderDocID, tempDtlConfirmList, empID, empName);
                        }                        
                    }
                    #endregion
                    IBReaBmsCenSaleDocConfirm.Get(IBReaBmsCenSaleDocConfirm.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsCenSaleDocConfirm.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool ST_UDTO_UpdateReaSaleDocConfirmOfSale(ReaBmsCenSaleDocConfirm entity, IList<ReaSaleDtlConfirmVO> dtAddList, IList<ReaSaleDtlConfirmVO> dtEditList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode, string fields, string fieldsDtl)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数entity值为空!";
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(fields))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数fields值为空!";
                return tempBaseResultBool;
            }
            if (string.IsNullOrEmpty(fieldsDtl))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数fieldsDtl值为空!";
                return tempBaseResultBool;
            }
            //是否需要验收双确认(当验收单的状态为确认验收并且secAccepterType=3时需要)
            if (entity.Status == int.Parse(ReaBmsCenSaleDocConfirmStatus.已验收.Key) && secAccepterType == int.Parse(ConfirmSecAccepterType.供应商或实验室.Key))
            {
                if (string.IsNullOrEmpty(secAccepterAccount) || string.IsNullOrEmpty(secAccepterPwd))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：次验收人账号或密码不能为空！";
                    return tempBaseResultBool;
                }
                IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(secAccepterAccount);
                if (tempRBACUser.Count >= 1)
                {
                    tempBaseResultBool = IBReaBmsCenSaleDocConfirm.EditOfJudgeIsSameOrg(secAccepterType, entity.ReaCompID.Value.ToString(), secAccepterAccount, secAccepterPwd, tempRBACUser[0]);
                    if (!tempBaseResultBool.success)
                        return tempBaseResultBool;

                    entity.SecAccepterID = tempRBACUser[0].HREmployee.Id;
                    entity.SecAccepterName = tempRBACUser[0].HREmployee.CName;
                    entity.SecAcceptTime = DateTime.Now;
                    if (!fields.Contains("SecAccepterID")) fields = fields + ",SecAccepterID";
                    if (!fields.Contains("SecAccepterName")) fields = fields + ",SecAccepterName";
                    if (!fields.Contains("SecAcceptTime")) fields = fields + ",SecAcceptTime";
                }
                else if (tempRBACUser.Count <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：次验收人账号不存在！";
                    return tempBaseResultBool;
                }
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (string.IsNullOrEmpty(entity.StatusName) && entity.Status > 0)
                {
                    entity.StatusName = ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[entity.Status.ToString()].Name;
                    if (!fields.Contains("StatusName")) fields = fields + ",StatusName";
                }
                if (!fields.Contains("TotalPrice")) fields = fields + ",TotalPrice";
                entity.TotalPrice = 0;
                if (dtAddList != null && dtAddList.Count > 0)
                {
                    entity.TotalPrice = dtEditList.Sum(p => p.ReaBmsCenSaleDtlConfirm.SumTotal);
                }
                if (dtEditList != null && dtEditList.Count > 0)
                {
                    entity.TotalPrice = entity.TotalPrice + dtEditList.Sum(p => p.ReaBmsCenSaleDtlConfirm.SumTotal);
                }
                IBReaBmsCenSaleDocConfirm.Entity = entity;
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCenSaleDocConfirm.Entity, fields);
                tempBaseResultBool = IBReaBmsCenSaleDocConfirm.EditReaSaleDocConfirmOfSale(tempArray, dtAddList, dtEditList, secAccepterType, codeScanningMode, empID, empName, fieldsDtl);
                if (tempBaseResultBool.success)
                {
                    #region 更新订单的单据状态
                    if (entity.Status == int.Parse(ReaBmsCenSaleDtlConfirmStatus.已验收.Key))
                    {                        
                        ReaBmsCenSaleDoc saleDoc = IBReaBmsCenSaleDoc.Get(entity.SaleDocID.Value);
                        long orderDocID = (saleDoc.OrderDocID == null ? 0 : saleDoc.OrderDocID.Value);
                        if (orderDocID > 0)
                        {
                            ZhiFang.Common.Log.Log.Info("供货验收-确认验收后，同步更新其所属订单[orderDocID="+ orderDocID + "]的单据状态");
                            string hqlWhere = string.Format("SaleDocConfirmID in (select Id from ReaBmsCenSaleDocConfirm where SaleDocID in (select Id from ReaBmsCenSaleDoc where OrderDocID={0}))", orderDocID);
                            IList<ReaBmsCenSaleDtlConfirm> tempDtlConfirmList = IBReaBmsCenSaleDtlConfirm.SearchListByHQL(hqlWhere);
                            IBReaBmsCenOrderDoc.EditReaBmsCenOrderDocStatus(orderDocID, tempDtlConfirmList, empID, empName);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlConfirmByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCenSaleDtlConfirm> entityList = new EntityList<ReaBmsCenSaleDtlConfirm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsCenSaleDtlConfirm.GetReaBmsCenSaleDtlConfirmListByHql(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsCenSaleDtlConfirm.GetReaBmsCenSaleDtlConfirmListByHql(where, "", page, limit);
                }
                
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsCenSaleDtlConfirm>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        #endregion

        #region 验收处理
        public BaseResultDataValue ST_UDTO_SearchReaGoodsScanCodeVOOfConfirm(long reaCompID, string serialNo, string scanCodeType, long bobjectID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(serialNo))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "serialNo参数值为空!";
                return baseResultDataValue;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                ReaGoodsScanCodeVO entity = IBReaGoodsBarcodeOperation.DecodingReaGoodsScanCodeVOOfConfirm(reaCompID, serialNo, scanCodeType, bobjectID);
                ParseObjectProperty pop = new ParseObjectProperty();
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity);
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
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_SearchReaBmsCenSaleDtlConfirmVOOfConfirmTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, string confirmType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaSaleDtlConfirmVO> entityList = new EntityList<ReaSaleDtlConfirmVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsCenSaleDtlConfirm.SearchBmsCenSaleDtlConfirmVOOfConfirmTypeByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit, confirmType);
                }
                else
                {
                    entityList = IBReaBmsCenSaleDtlConfirm.SearchBmsCenSaleDtlConfirmVOOfConfirmTypeByHQL(where, "", page, limit, confirmType);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaSaleDtlConfirmVO>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultBool ST_UDTO_UpdateReaSaleDocConfirmAndDtl(ReaBmsCenSaleDocConfirm entity, IList<ReaBmsCenSaleDtlConfirm> dtEditList, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode, string fields, string fieldsDtl)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //是否需要验收双确认(当验收单的状态为确认验收并且secAccepterType=3时需要)
            if (entity.Status == int.Parse(ReaBmsCenSaleDocConfirmStatus.已验收.Key) && secAccepterType == int.Parse(ConfirmSecAccepterType.供应商或实验室.Key))
            {
                if (string.IsNullOrEmpty(secAccepterAccount) || string.IsNullOrEmpty(secAccepterPwd))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：次验收人账号或密码不能为空！";
                    return tempBaseResultBool;
                }
                IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(secAccepterAccount);
                if (tempRBACUser.Count >= 1)
                {
                    tempBaseResultBool = IBReaBmsCenSaleDocConfirm.EditOfJudgeIsSameOrg(secAccepterType, entity.ReaCompID.Value.ToString(), secAccepterAccount, secAccepterPwd, tempRBACUser[0]);
                    if (!tempBaseResultBool.success)
                        return tempBaseResultBool;

                    entity.SecAccepterID = tempRBACUser[0].HREmployee.Id;
                    entity.SecAccepterName = tempRBACUser[0].HREmployee.CName;
                    entity.SecAcceptTime = DateTime.Now;
                    if (!fields.Contains("SecAccepterID")) fields = fields + ",SecAccepterID";
                    if (!fields.Contains("SecAccepterName")) fields = fields + ",SecAccepterName";
                    if (!fields.Contains("SecAcceptTime")) fields = fields + ",SecAcceptTime";
                }
                else if (tempRBACUser.Count <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：次验收人账号不存在！";
                    return tempBaseResultBool;
                }
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (string.IsNullOrEmpty(entity.StatusName) && entity.Status > 0)
                {
                    entity.StatusName = ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[entity.Status.ToString()].Name;
                    if (!fields.Contains("StatusName")) fields = fields + ",StatusName";
                }
                if (!fields.Contains("TotalPrice")) fields = fields + ",TotalPrice";
                entity.TotalPrice = 0;
                if (dtEditList != null && dtEditList.Count > 0)
                {
                    entity.TotalPrice = entity.TotalPrice + dtEditList.Sum(p => p.SumTotal);
                }
                IBReaBmsCenSaleDocConfirm.Entity = entity;
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCenSaleDocConfirm.Entity, fields);
                tempBaseResultBool = IBReaBmsCenSaleDocConfirm.EditReaSaleDocConfirmAndDtl(tempArray, dtEditList, secAccepterType, codeScanningMode, fieldsDtl, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool ST_UDTO_DelReaBmsCenSaleDtlConfirm(long id, string confirmSourceType)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBReaBmsCenSaleDtlConfirm.Entity = IBReaBmsCenSaleDtlConfirm.Get(id);
                if (IBReaBmsCenSaleDtlConfirm.Entity != null)
                {
                    long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                    string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    baseResultBool = IBReaBmsCenSaleDtlConfirm.DelReaBmsCenSaleDtlConfirm(id, confirmSourceType, empID, empName);
                }
                else
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：删除的信息已不存在！";
                }
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }
        public BaseResultBool ST_UDTO_UpdateReaBmsCenSaleDocConfirmOfConfirmType(ReaBmsCenSaleDocConfirm entity, int secAccepterType, string secAccepterAccount, string secAccepterPwd, string codeScanningMode, string fields, string confirmType)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //是否需要验收双确认(当验收单的状态为确认验收并且secAccepterType=3时需要)
            if (entity.Status == int.Parse(ReaBmsCenSaleDocConfirmStatus.已验收.Key) && secAccepterType == int.Parse(ConfirmSecAccepterType.供应商或实验室.Key))
            {
                if (string.IsNullOrEmpty(secAccepterAccount) || string.IsNullOrEmpty(secAccepterPwd))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：次验收人账号或密码不能为空！";
                    return tempBaseResultBool;
                }
                IList<RBACUser> tempRBACUser = IBRBACUser.SearchRBACUserByUserAccount(secAccepterAccount);
                if (tempRBACUser.Count >= 1)
                {
                    tempBaseResultBool = IBReaBmsCenSaleDocConfirm.EditOfJudgeIsSameOrg(secAccepterType, entity.ReaCompID.Value.ToString(), secAccepterAccount, secAccepterPwd, tempRBACUser[0]);
                    if (!tempBaseResultBool.success)
                        return tempBaseResultBool;

                    entity.SecAccepterID = tempRBACUser[0].HREmployee.Id;
                    entity.SecAccepterName = tempRBACUser[0].HREmployee.CName;
                    entity.SecAcceptTime = DateTime.Now;
                    if (!fields.Contains("SecAccepterID")) fields = fields + ",SecAccepterID";
                    if (!fields.Contains("SecAccepterName")) fields = fields + ",SecAccepterName";
                    if (!fields.Contains("SecAcceptTime")) fields = fields + ",SecAcceptTime";
                }
                else if (tempRBACUser.Count <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：次验收人账号不存在！";
                    return tempBaseResultBool;
                }
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (string.IsNullOrEmpty(entity.StatusName) && entity.Status > 0)
                {
                    entity.StatusName = ReaBmsCenSaleDtlConfirmStatus.GetStatusDic()[entity.Status.ToString()].Name;
                    if (!fields.Contains("StatusName")) fields = fields + ",StatusName";
                }
                IBReaBmsCenSaleDocConfirm.Entity = entity;
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCenSaleDocConfirm.Entity, fields);
                tempBaseResultBool = IBReaBmsCenSaleDocConfirm.EditReaBmsCenSaleDocConfirmOfConfirmType(tempArray, secAccepterType, codeScanningMode, empID, empName, confirmType);
                if (tempBaseResultBool.success)
                {
                    #region 更新订单的单据状态
                    if (entity.Status == int.Parse(ReaBmsCenSaleDtlConfirmStatus.已验收.Key))
                    {
                        ReaBmsCenSaleDoc saleDoc = IBReaBmsCenSaleDoc.Get(entity.SaleDocID.Value);
                        long orderDocID = (saleDoc.OrderDocID == null ? 0 : saleDoc.OrderDocID.Value);
                        if (orderDocID > 0)
                        {
                            ZhiFang.Common.Log.Log.Info("供货验收-确认验收后，同步更新其所属订单[orderDocID=" + orderDocID + "]的单据状态");
                            string hqlWhere = string.Format("SaleDocConfirmID in (select Id from ReaBmsCenSaleDocConfirm where SaleDocID in (select Id from ReaBmsCenSaleDoc where OrderDocID={0}))", orderDocID);
                            IList<ReaBmsCenSaleDtlConfirm> tempDtlConfirmList = IBReaBmsCenSaleDtlConfirm.SearchListByHQL(hqlWhere);
                            IBReaBmsCenOrderDoc.EditReaBmsCenOrderDocStatus(orderDocID, tempDtlConfirmList, empID, empName);
                        }
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        #endregion

        #region 入库管理
        public BaseResultDataValue RS_UDTO_AddReaBmsInDocAndDtlByInterface(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode, string iSNeedCreateBarCode)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (entity == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：参数entity值为空!";
                return tempBaseResultDataValue;
            }
            if (dtAddList == null || dtAddList.Count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：参数dtAddList值为空!";
                return tempBaseResultDataValue;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsInDoc.Entity = entity;
                tempBaseResultDataValue = IBReaBmsInDoc.AddReaBmsInDocAndDtlByInterface(dtAddList, codeScanningMode, iSNeedCreateBarCode, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsInDoc.Get(IBReaBmsInDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsInDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("手工入库错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool RS_UDTO_UpdateReaBmsInDocAndInDtlListByInterface(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> inDtlList, string iSNeedCreateBarCode, string fieldsDtl, string fields, string codeScanningMode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数entity值为空!";
                return tempBaseResultBool;
            }
            if ((inDtlList == null || inDtlList.Count <= 0))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数inDtlList为空!";
                return tempBaseResultBool;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                entity.DataUpdateTime = DateTime.Now;
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                tempBaseResultBool = IBReaBmsInDoc.EditReaBmsInDocAndDtlOfVOByInterface(entity, tempArray, inDtlList, iSNeedCreateBarCode, fieldsDtl, codeScanningMode, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("入库修改保存错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool RS_UDTO_UpdateConfirmInDocByInterface(long id, string codeScanningMode, string iSNeedCreateBarCode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                tempBaseResultBool = IBReaBmsInDoc.EditConfirmInDocByInterface(id, codeScanningMode, iSNeedCreateBarCode, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("确认入库错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool RS_UDTO_UpdateReaBmsInDocAndInDtlList(ReaBmsInDoc entity, IList<ReaBmsInDtlVO> inDtlList, string fieldsDtl, string fields, string codeScanningMode)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数entity值为空!";
                return tempBaseResultBool;
            }
            if ((inDtlList == null || inDtlList.Count <= 0))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数inDtlList为空!";
                return tempBaseResultBool;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                entity.DataUpdateTime = DateTime.Now;
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                tempBaseResultBool = IBReaBmsInDoc.EditReaBmsInDocAndDtlOfVO(entity, tempArray, null, inDtlList, fieldsDtl, codeScanningMode, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("入库修改保存错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue RS_UDTO_SearchReaBmsInDocByHQL(int page, int limit, string fields, string where, string dtlHql, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsInDoc> tempEntityList = new EntityList<ReaBmsInDoc>();
            try
            {

                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBReaBmsInDoc.SearchListByDocAndDtlHQL(where, dtlHql, sort, page, limit);

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsInDoc>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        #endregion

        #region 出库/移库/退库入库
        public BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtl(string deptGoodsHql, string reaGoodsHql, string qtyHql, int page, int limit, string fields, string sort, bool isPlanish, bool isMergeInDocNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            try
            {
                //库存货品合并方式
                string groupTypeStr = "";// jsonObject["GroupType"] != null ? jsonObject["GroupType"].ToString() : "";
                int groupType = 0;
                if (!string.IsNullOrEmpty(groupTypeStr))
                {
                    int.TryParse(groupTypeStr, out groupType);
                }

                IList<ReaBmsQtyDtl> list = IBReaBmsQtyDtl.SearchReaBmsQtyDtl(deptGoodsHql, reaGoodsHql, qtyHql, groupType, isMergeInDocNo);
                if (list == null || list.Count == 0)
                    return baseResultDataValue;
                //list = list.OrderByDescending(p => p.GoodsQty).ToList();
                entityList.count = list.Count;
                entityList.list = list;
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsQtyDtl>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaGoodsByBarCode(string barcode, string fields, bool isPlanish)
        {

            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoods> entityList = new EntityList<ReaGoods>();
            try
            {
                if (string.IsNullOrEmpty(barcode))
                    return baseResultDataValue;
                entityList = IBReaGoodsBarcodeOperation.SearchReaGoodsBySanBarCode(barcode);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoods>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaGoodsOrgLinkByBarCode(string barcode, string fields, bool isPlanish)
        {

            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsOrgLink> entityList = new EntityList<ReaGoodsOrgLink>();
            try
            {
                if (string.IsNullOrEmpty(barcode))
                    return baseResultDataValue;
                entityList = IBReaGoodsBarcodeOperation.SearchReaGoodsOrgLinkBySanBarCode(barcode);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsOrgLink>(entityList);
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
                ZhiFang.Common.Log.Log.Error(ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtlByBarCode(string storageId, string placeId, string barcode, string fields, bool isPlanish, string barcodeOperType, bool isMergeInDocNo, bool isAllowOfALLStorage)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            try
            {
                if (string.IsNullOrEmpty(barcode))
                    return baseResultDataValue;
                entityList = IBReaBmsQtyDtl.SearchReaBmsQtyDtlByBarCode(storageId, placeId, barcode, barcodeOperType, isMergeInDocNo, isAllowOfALLStorage, ref baseResultDataValue);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsQtyDtl>(entityList);
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
                ZhiFang.Common.Log.Log.Error(ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        //退库入库服务
        public BaseResultDataValue RS_UDTO_AddInputReaGoodsByReturn(int inputType, ReaBmsInDoc inDoc, IList<ReaBmsInDtl> listReaBmsInDtl)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (inDoc == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "参数入库总单不能为空！";
                    return baseResultDataValue;
                }
                if (listReaBmsInDtl == null || listReaBmsInDtl.Count == 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "参数入库明细单不能为空！";
                    return baseResultDataValue;
                }
                //找回退库入库选择的出库单信息
                long outDocId = inDoc.Id;
                ReaBmsOutDoc reaBmsOutDoc = IBReaBmsOutDoc.Get(outDocId);
                //出库确认后调用退库接口的出库明细信息
                IList<ReaBmsOutDtl> listOutDtlOfIn = new List<ReaBmsOutDtl>();
                //实际的入库明细集合信息
                IList<ReaBmsInDtl> addInDtlList = new List<ReaBmsInDtl>();
                //实际的入库库存集合信息
                IList<ReaBmsQtyDtl> addQtyDtlList = new List<ReaBmsQtyDtl>();

                baseResultDataValue = IBReaBmsInDoc.AddReaBmsInDocByReturnReaGoods(inputType, ref inDoc, listReaBmsInDtl, ref listOutDtlOfIn, ref addInDtlList, ref addQtyDtlList);
                if (baseResultDataValue.success == true)
                {
                    //出库确认后是否调用退库接口
                    BParameter parameter = IBBParameter.GetParameterByParaNo(SYSParaNo.出库确认后是否调用退库接口.Key);
                    ZhiFang.Common.Log.Log.Info("退库入库.出库确认后是否调用退库接口:" + (parameter.ParaValue == "1" ? "是" : "否"));
                    if (parameter.ParaValue == "1")
                    {
                        ReaBmsOutDoc mapperOutDoc = ClassMapperHelp.GetMapper<ReaBmsOutDoc, ReaBmsOutDoc>(reaBmsOutDoc);
                        mapperOutDoc.TotalPrice = listOutDtlOfIn.Sum(p => p.SumTotal);
                        mapperOutDoc.OutType = int.Parse(ReaBmsOutDocOutType.退库入库.Key);
                        mapperOutDoc.OutTypeName = ReaBmsOutDocOutType.GetStatusDic()[mapperOutDoc.OutType.ToString()].Name;
                        BaseResultData baseResultData = ReaGoodsStorageSyncInterface(mapperOutDoc, listOutDtlOfIn);
                        ZhiFang.Common.Log.Log.Info("退库入库.库接口返回结果:" + baseResultData.success + ",返回信息" + baseResultData.message);
                        //baseResultDataValue.success = baseResultData.success;
                        //baseResultDataValue.ErrorInfo = "退库入库.调用退库接口失败,库接口返回结果:" + baseResultData.message;

                        inDoc.IOFlag = int.Parse(ReaBmsInDocIOFlag.退库成功.Key);
                        if (baseResultData.success == false)
                        {
                            inDoc.IOFlag = int.Parse(ReaBmsInDocIOFlag.退库失败.Key);
                            inDoc.IOMemo = baseResultData.message;
                            //物理删除已产生的退库入库信息
                            //BaseResultBool tempBaseResultBool = IBReaBmsInDoc.DelReaBmsInDocByReturn(inDoc);
                        }
                        IBReaBmsInDoc.Entity = inDoc;
                        IBReaBmsInDoc.Edit();
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("退库入库保存错误:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultBool RS_UDTO_DelDelReaBmsInDocByReturnOfInDocId(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                ReaBmsInDoc inDoc = IBReaBmsInDoc.Get(id);
                tempBaseResultBool = IBReaBmsInDoc.DelReaBmsInDocByReturn(inDoc);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("退库入库调用退库接口失败后,物理删除退库入库的相关信息错误:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsOutDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsOutDtl> entityList = new EntityList<ReaBmsOutDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsOutDtl.GetListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit, true);
                }
                else
                {
                    entityList = IBReaBmsOutDtl.GetListByHQL(where, "", page, limit, true);
                }
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaBmsInDtlByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsInDtl> entityList = new EntityList<ReaBmsInDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsInDtl.GetReaBmsInDtlListByHql(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBReaBmsInDtl.GetReaBmsInDtlListByHql(where, "", page, limit);
                }

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsInDtl>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        
        #endregion

        #region 盘库管理
        public BaseResultDataValue RS_UDTO_SearchReaBmsCheckDtlByHQL(int page, int limit, string fields, string checkHql, string where, string reaGoodHql, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCheckDtl> tempEntityList = new EntityList<ReaBmsCheckDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    //ZhiFang.Common.Log.Log.Debug("sort:" + sort+ ";GetSortHQL:"+ CommonServiceMethod.GetSortHQL(sort));
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBReaBmsCheckDtl.SearchReaBmsCheckDtlEntityListByJoinHQL(checkHql, where, reaGoodHql, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsCheckDtl>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue RS_UDTO_SearchAddReaBmsCheckDtlByHQL(int page, int limit, string fields, string docEntity, string reaGoodHql, int days, string sort, bool isPlanish, bool preIsVO, int zeroQtyDays)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsCheckDtl> tempEntityList = new EntityList<ReaBmsCheckDtl>();
            if (string.IsNullOrEmpty(docEntity))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：docEntity参数为空!";
                return tempBaseResultDataValue;
            }
            try
            {
                if (preIsVO == true)
                {
                    fields = fields.Replace("VO_", "ReaBmsCheckDtl_");
                    //ZhiFang.Common.Log.Log.Debug("fields:"+ fields);
                }
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                ReaBmsCheckDoc checkDoc = null;
                checkDoc = JObject.Parse(docEntity).ToObject<ReaBmsCheckDoc>();
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                tempEntityList = IBReaBmsCheckDoc.SearchAddReaBmsCheckDtlByHQL(checkDoc, reaGoodHql, days, zeroQtyDays, sort, page, limit, empID, empName, ref tempBaseResultDataValue);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsCheckDtl>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("获取新增盘库明细货品信息错误:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        //Add  ReaBmsCheckDoc
        public BaseResultDataValue RS_UDTO_AddReaBmsCheckDoc(ReaBmsCheckDoc entity, int mergeType)
        {
            IBReaBmsCheckDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                tempBaseResultDataValue = IBReaBmsCheckDoc.AddReaBmsCheckDoc(mergeType, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsCheckDoc.Get(IBReaBmsCheckDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsCheckDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("新增盘库信息错误:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_AddReaBmsCheckDocAndDtlList(ReaBmsCheckDoc entity, IList<ReaBmsCheckDtl> dtAddList, bool isTakenFromQty)
        {
            ZhiFang.Common.Log.Log.Error("新增盘库时,盘库数是否等于库存数:" + isTakenFromQty);
            IBReaBmsCheckDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                tempBaseResultDataValue = IBReaBmsCheckDoc.AddReaBmsCheckDocAndDtlList(entity, dtAddList, empID, empName, isTakenFromQty);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsCheckDoc.Get(IBReaBmsCheckDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsCheckDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("新增盘库信息错误:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool RS_UDTO_UpdateReaBmsCheckDocAndDtl(ReaBmsCheckDoc entity, string fields, IList<ReaBmsCheckDtl> dtEditList, string fieldsDtl)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数entity值为空!";
                return tempBaseResultBool;
            }
            if (fields == null || fields.Length <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：参数fields值为空!";
                return tempBaseResultBool;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (string.IsNullOrEmpty(entity.StatusName) && entity.Status > 0)
                {
                    entity.StatusName = ReaBmsCheckDocStatus.GetStatusDic()[entity.Status.ToString()].Name;
                    if (!fields.Contains("StatusName")) fields = fields + ",StatusName";
                }
                IBReaBmsCheckDoc.Entity = entity;
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaBmsCheckDoc.Entity, fields);
                tempBaseResultBool = IBReaBmsCheckDoc.EditReaBmsCheckDocAndDtl(tempArray, dtEditList, fieldsDtl, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("保存盘库信息错误:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue ST_UDTO_SearchReaBmsInDocOfCheckDocID(long id, bool isPlanish, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                baseResultDataValue = IBReaBmsCheckDoc.SearchReaBmsInDocOfCheckDocID(id, isPlanish, fields, empID, empName);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_SearchReaBmsInDtlListOfCheckDocID(long id, bool isPlanish, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                baseResultDataValue = IBReaBmsCheckDoc.SearchReaBmsInDtlListOfCheckDocID(id, isPlanish, fields, empID, empName);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_SearchReaBmsOutDocOfCheckDocID(long id, bool isPlanish, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                long deptID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID));
                string deptName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptName);
                baseResultDataValue = IBReaBmsCheckDoc.SearchReaBmsOutDocOfCheckDocID(id, isPlanish, fields, deptID, deptName, empID, empName);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_SearchReaBmsOutDtlListOfCheckDocID(long id, bool isPlanish, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                baseResultDataValue = IBReaBmsCheckDoc.SearchReaBmsOutDtlListOfCheckDocID(id, isPlanish, fields, empID, empName);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_AddReaBmsInDocAndDtlOfCheckDocID(long checkDocID, ReaBmsInDoc inDoc, IList<ReaBmsInDtlVO> dtAddList, string codeScanningMode)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (inDoc == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：参数inDoc值为空!";
                return tempBaseResultDataValue;
            }
            if (dtAddList == null || dtAddList.Count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：参数dtAddList值为空!";
                return tempBaseResultDataValue;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                inDoc.DataUpdateTime = DateTime.Now;
                tempBaseResultDataValue = IBReaBmsCheckDoc.AddReaBmsInDocAndDtlOfCheckDocID(checkDocID, inDoc, dtAddList, codeScanningMode, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsInDoc.Get(inDoc.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsInDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("库差异调整保存盘盈入库错误:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_AddReaBmsOutDocAndDtlOfCheckDocID(long checkDocID, ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> dtAddList, string codeScanningMode)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (outDoc == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：参数outDoc值为空!";
                return tempBaseResultDataValue;
            }
            if (dtAddList == null || dtAddList.Count <= 0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：参数dtAddList值为空!";
                return tempBaseResultDataValue;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                outDoc.DataUpdateTime = DateTime.Now;
                tempBaseResultDataValue = IBReaBmsCheckDoc.AddReaBmsOutDocAndDtlOfCheckDocID(checkDocID, outDoc, dtAddList, codeScanningMode, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsOutDoc.Get(outDoc.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsOutDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("库差异调整保存盘亏出库错误:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public Stream RS_UDTO_GetReaBmsCheckDocAndDtlOfPdf(long id, string sort, long operateType, string frx)
        {
            Stream fileStream = null;
            ZhiFang.Common.Log.Log.Debug("RS_UDTO_GetReaBmsCheckDocAndDtlOfPdf开始");
            try
            {
                string filename = id + ".pdf";
                if (!string.IsNullOrEmpty(sort))
                    sort = CommonServiceMethod.GetSortHQL(sort);
                fileStream = IBReaBmsCheckDoc.GetReaBmsCheckDocAndDtlOfPdf(id, sort, frx);
                //获取错误提示信息
                if (fileStream == null)
                {
                    string errorInfo = "附件不存在!请重新生成或联系管理员。";
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                    return memoryStream;
                }

                Encoding code = Encoding.GetEncoding("gb2312");
                System.Web.HttpContext.Current.Response.ContentEncoding = code;
                System.Web.HttpContext.Current.Response.HeaderEncoding = code;

                filename = EncodeFileName.ToEncodeFileName(filename);
                if (operateType == 0) //下载文件
                {
                    System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                }
                else if (operateType == 1)//直接打开文件
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                    WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                }

                return fileStream;
            }
            catch (Exception ex)
            {
                string errorInfo = "获取盘点单PDF文件失败!";
                ZhiFang.Common.Log.Log.Error(errorInfo + ex.Message);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                return memoryStream;
            }
        }
        public BaseResultDataValue ST_UDTO_SearchReaEquipReagentLinkVOList()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<ReaEquipReagentLinkVO> entityList = new List<ReaEquipReagentLinkVO>();
                long deptID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID));
                string deptName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptName);
                string deptIDStr = IBHRDept.SearchHRDeptIdListByHRDeptId(deptID);
                if (string.IsNullOrEmpty(deptIDStr))
                {
                    deptIDStr = deptID.ToString();
                }
                //ZhiFang.Common.Log.Log.Debug("deptIDStr:"+ deptIDStr);
                //获取当前登录帐号所属部门(包括子部门)的仪器信息
                IList<ReaTestEquipLab> testEquipLabList = IBReaTestEquipLab.SearchListByHQL("reatestequiplab.Visible=1 and reatestequiplab.DeptID in (" + deptIDStr + ")");
                entityList = IBReaEquipReagentLink.SearchReaEquipReagentLinkVOList(testEquipLabList, deptName);
                ParseObjectProperty pop = new ParseObjectProperty();
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaTestEquipLabByEmpDeptHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaTestEquipLab> tempEntityList = new EntityList<ReaTestEquipLab>();
            try
            {
                long deptID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID));
                string deptName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptName);
                string deptIDStr = IBHRDept.SearchHRDeptIdListByHRDeptId(deptID);
                if (string.IsNullOrEmpty(deptIDStr))
                {
                    deptIDStr = deptID.ToString();
                }

                string hql2 = " 1=1 ";
                if (!string.IsNullOrEmpty(deptIDStr)) hql2 = "reatestequiplab.Visible=1 and reatestequiplab.DeptID in (" + deptIDStr + ")";
                if (string.IsNullOrEmpty(where))
                {
                    where = hql2;
                }
                else
                {
                    where = where + " and " + hql2;
                }

                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaTestEquipLab.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaTestEquipLab.SearchListByHQL(where, page, limit);
                }

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaTestEquipLab>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region 库存结转
        public BaseResultBool RS_UDTO_GetJudgeISAddReaBmsQtyBalanceDoc(string beginDate, string endDate)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool = IBReaBmsQtyBalanceDoc.GetJudgeISAddReaBmsQtyBalanceDoc(beginDate, endDate);
            return tempBaseResultBool;
        }
        public BaseResultDataValue RS_UDTO_AddReaBmsQtyBalanceDocOfQtyBalance(ReaBmsQtyBalanceDoc entity, bool isCover, string beginDate, string endDate)
        {
            //IBReaBmsQtyBalanceDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                tempBaseResultDataValue = IBReaBmsQtyBalanceDoc.AddReaBmsQtyBalanceDocOfQtyBalance(entity, empID, empName, isCover, beginDate, endDate);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsQtyBalanceDoc.Get(IBReaBmsQtyBalanceDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsQtyBalanceDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool RS_UDTO_UpdateVisibleReaBmsQtyBalanceDocById(long id, bool visible)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            tempBaseResultBool = IBReaBmsQtyBalanceDoc.UpdateVisibleReaBmsQtyBalanceDocById(id, visible, empID, empName);
            return tempBaseResultBool;
        }
        #endregion

        #region 结转报表
        public BaseResultDataValue RS_UDTO_AddQtyBalanceReportOfQtyBalanceDtlList(ReaBmsQtyMonthBalanceDoc entity, string labCName)
        {
            //IBReaBmsQtyMonthBalanceDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                tempBaseResultDataValue = IBReaBmsQtyMonthBalanceDoc.SaveOfQtyBalanceDtlList(entity, labCName, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsQtyMonthBalanceDoc.Get(IBReaBmsQtyMonthBalanceDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsQtyMonthBalanceDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("生成结转报表错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_AddQtyBalanceReportOfQtyDtlOperList(ReaBmsQtyMonthBalanceDoc entity, string labCName)
        {
            //IBReaBmsQtyMonthBalanceDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                tempBaseResultDataValue = IBReaBmsQtyMonthBalanceDoc.SaveQtyBalanceReportOfQtyDtlOperList(entity, labCName, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsQtyMonthBalanceDoc.Get(IBReaBmsQtyMonthBalanceDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsQtyMonthBalanceDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("生成结转报表错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool ST_UDTO_UpdateCancelReaBmsQtyMonthBalanceDocById(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
            string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            tempBaseResultBool = IBReaBmsQtyMonthBalanceDoc.UpdateCancelReaBmsQtyMonthBalanceDocById(id, empID, empName);
            return tempBaseResultBool;
        }
        public BaseResultDataValue RS_UDTO_SearchQtyMonthBalanceDtlListById(long id, int page, int limit, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsQtyMonthBalanceDtl> entityList = new EntityList<ReaBmsQtyMonthBalanceDtl>();
            try
            {
                entityList = IBReaBmsQtyMonthBalanceDoc.SearchQtyMonthBalanceDtlListById(id, page, limit, ref baseResultDataValue);
                if (baseResultDataValue.success == false) return baseResultDataValue;

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsQtyMonthBalanceDtl>(entityList);
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
                ZhiFang.Common.Log.Log.Error(ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public Stream RS_UDTO_GetQtyMonthBalanceAndDtlOfPdf(long id, long operateType, string frx, string labCName)
        {
            Stream fileStream = null;
            try
            {
                //ZhiFang.Common.Log.Log.Debug("RS_UDTO_GetQtyMonthBalanceAndDtlOfPdf");
                string filename = id + ".pdf";
                fileStream = IBReaBmsQtyMonthBalanceDoc.GetQtyMonthBalanceAndDtlOfPdf(id, frx, labCName);
                //获取错误提示信息
                if (fileStream == null)
                {
                    string errorInfo = "附件不存在!请重新生成或联系管理员。";
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                    return memoryStream;
                }

                Encoding code = Encoding.GetEncoding("gb2312");
                System.Web.HttpContext.Current.Response.ContentEncoding = code;
                System.Web.HttpContext.Current.Response.HeaderEncoding = code;

                filename = EncodeFileName.ToEncodeFileName(filename);
                if (operateType == 0) //下载文件
                {
                    System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                    System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                }
                else if (operateType == 1)//直接打开文件
                {
                    WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                    WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                }
                return fileStream;
            }
            catch (Exception ex)
            {
                string errorInfo = "获取结转报表PDF文件失败!";
                ZhiFang.Common.Log.Log.Error(errorInfo + ex.Message);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                return memoryStream;
            }
        }
        #endregion

        #region 条码打印
        public BaseResultDataValue RS_UDTO_SearchReaGoodsPrintBarCodeVOListByInDocId(long inDocId, string dtlIdStr, string boxHql, string fields, bool isPlanish, int page, int limit)
        {

            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (inDocId <= 0) return baseResultDataValue;

            EntityList<ReaGoodsPrintBarCodeVO> entityList = new EntityList<ReaGoodsPrintBarCodeVO>();
            try
            {
                entityList = IBReaGoodsBarcodeOperation.SearchReaGoodsPrintBarCodeVOListByInDocId(inDocId, dtlIdStr, boxHql, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsPrintBarCodeVO>(entityList);
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
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaGoodsPrintBarCodeVOListByInDtlId(long inDtlId, string boxHql, string fields, bool isPlanish, int page, int limit)
        {

            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (inDtlId <= 0) return baseResultDataValue;

            EntityList<ReaGoodsPrintBarCodeVO> entityList = new EntityList<ReaGoodsPrintBarCodeVO>();
            try
            {
                entityList = IBReaGoodsBarcodeOperation.SearchReaGoodsPrintBarCodeVOListByInDtlId(inDtlId, boxHql, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsPrintBarCodeVO>(entityList);
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
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaGoodsPrintBarCodeVOListBySaledocId(long saledocId, string dtlIdStr, string fields, bool isPlanish, int page, int limit)
        {

            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (saledocId <= 0) return baseResultDataValue;

            EntityList<ReaGoodsPrintBarCodeVO> entityList = new EntityList<ReaGoodsPrintBarCodeVO>();
            try
            {
                entityList = IBReaGoodsBarcodeOperation.SearchReaGoodsPrintBarCodeVOListBySaledocId(saledocId, dtlIdStr, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsPrintBarCodeVO>(entityList);
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
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaGoodsPrintBarCodeVOListBySaleDtlId(long saleDtlId, string fields, bool isPlanish, int page, int limit)
        {

            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (saleDtlId <= 0) return baseResultDataValue;

            EntityList<ReaGoodsPrintBarCodeVO> entityList = new EntityList<ReaGoodsPrintBarCodeVO>();
            try
            {
                entityList = IBReaGoodsBarcodeOperation.SearchReaGoodsPrintBarCodeVOListBySaleDtlId(saleDtlId, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsPrintBarCodeVO>(entityList);
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
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("获取供货条码信息错误:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }
        public BaseResultBool RS_UDTO_UpdatePrintCount(IList<long> lotList, string lotType, IList<long> boxList)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            tempBaseResultBool = IBReaGoodsBarcodeOperation.UpdatePrintCount(lotList, lotType, boxList);
            return tempBaseResultBool;
        }
        #endregion

        #region 货品导入导出

        //货品导入
        public Message RS_UDTO_UploadReaGoodsDataByExcel()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            int iTotal = 0;
            try
            {
                ZhiFang.Common.Log.Log.Debug("RS_UDTO_UploadReaGoodsDataByExcel.上传开始:");
                iTotal = HttpContext.Current.Request.Files.Count;
                ZhiFang.Common.Log.Log.Debug("RS_UDTO_UploadReaGoodsDataByExcel.上传文件个数:" + iTotal);
                if (iTotal == 0)
                {
                    baseResultDataValue.ErrorInfo = "未检测到文件！";
                    baseResultDataValue.ResultDataValue = "";
                    baseResultDataValue.success = false;
                    return WebOperationContext.Current.CreateTextResponse(ZhiFang.ReagentSys.Client.Common.JsonSerializer.JsonDotNetSerializer(baseResultDataValue), "text/plain", Encoding.UTF8);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.ErrorInfo = ex.Message;
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.success = false;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_UploadReaGoodsDataByExcel.错误:" + ex.Message);
                return WebOperationContext.Current.CreateTextResponse(ZhiFang.ReagentSys.Client.Common.JsonSerializer.JsonDotNetSerializer(baseResultDataValue), "text/plain", Encoding.UTF8);
            }

            try
            {

                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                int len = file.ContentLength;
                if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string parentPath = HttpContext.Current.Server.MapPath("~/UploadBaseTableInfo/");
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }
                    string ProdID = HttpContext.Current.Request.Form["ProdID"];
                    string filepath = Path.Combine(parentPath, ZhiFang.Common.Public.GUIDHelp.GetGUIDString() + '_' + Path.GetFileName(file.FileName));
                    file.SaveAs(filepath);
                    baseResultDataValue = IBReaGoods.CheckGoodsExcelFormat(filepath, HttpContext.Current.Server.MapPath("~/"));
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue = IBReaGoods.AddGoodsDataFormExcel(ProdID, filepath, HttpContext.Current.Server.MapPath("~/"));
                    }
                }
                else
                {
                    baseResultDataValue.ErrorInfo = "文件大小为0或为空！";
                    baseResultDataValue.success = false;
                };
            }
            catch (Exception ex)
            {
                baseResultDataValue.ErrorInfo = ex.Message;
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.success = false;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_UploadReaGoodsDataByExcel.错误:" + ex.Message);
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        } //

        //货品导出
        public Message RS_UDTO_GetReaGoodsReportExcelPath()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string reportType = "";
            string idList = "";
            string where = "";
            string isHeader = "";
            string sort = "";
            string tempFileName = "";
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            DataSet ds = null;

            string[] allkey = HttpContext.Current.Request.Form.AllKeys;
            for (int i = 0; i < allkey.Length; i++)
            {
                switch (allkey[i])
                {
                    case "reportType":
                        reportType = HttpContext.Current.Request.Form["reportType"];
                        break;
                    case "idList":
                        idList = HttpContext.Current.Request.Form["idList"];
                        break;
                    case "where":
                        where = HttpContext.Current.Request.Form["where"];
                        break;
                    case "isHeader":
                        isHeader = HttpContext.Current.Request.Form["isHeader"];
                        break;
                    case "sort":
                        sort = HttpContext.Current.Request.Form["sort"];
                        break;
                }
            }
            try
            {
                if (reportType == "1")
                {
                    if (string.IsNullOrEmpty(sort))
                        sort = "[{\"property\":\"ReaGoods_GoodsNo\",\"direction\":\"ASC\"}]";
                    sort = CommonServiceMethod.GetSortHQL(sort);
                    tempFileName = "货品信息列表";
                    ds = IBReaGoods.GetReaGoodsInfoByID(idList, where, sort, basePath + "\\BaseTableXML\\Report_ReaGoods.xml");
                }
                else if (reportType == "2")
                {
                    //if (string.IsNullOrEmpty(sort))
                    //    sort = "[{\"property\":\"BmsCenSaleDtl_Goods_GoodsNo\",\"direction\":\"ASC\"}]";
                    sort = CommonServiceMethod.GetSortHQL(sort);
                    tempFileName = "供货单货品列表";
                    //ds = IBReaBmsCenSaleDoc.GetBmsCenSaleDtlInfoByID(idList, where, sort, basePath + "\\BaseTableXML\\Report_BmsCenSaleDtl.xml");
                }
                else if (reportType == "3")
                {
                    //if (string.IsNullOrEmpty(sort))
                    //    sort = "[{\"property\":\"BmsCenOrderDtl_Goods_GoodsNo\",\"direction\":\"ASC\"}]";
                    sort = CommonServiceMethod.GetSortHQL(sort);
                    tempFileName = "订货单货品列表";
                    //ds = IBReaBmsCenOrderDoc.GetBmsCenOrderDtlInfoByID(idList, where, sort, basePath + "\\BaseTableXML\\Report_BmsCenOrderDtl.xml");
                }
                string excelName = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "." + CommonRS.GetExcelExtName();
                string tempFilePath = basePath + "\\TempExcelFile\\" + excelName;
                if (!Directory.Exists(basePath + "\\TempExcelFile"))
                {
                    Directory.CreateDirectory(basePath + "\\TempExcelFile");
                }
                if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    string headerText = "";
                    if (isHeader == "1")
                        headerText = tempFileName;
                    if (!ExcelHelper.CreateExcelByNPOI(ds.Tables[0], headerText, tempFilePath))
                    {
                        tempFilePath = "";
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "生成Excel文件失败！";
                    }
                }
                else
                {
                    tempFilePath = "";
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无任何要导出的记录信息！";
                }
                if (!string.IsNullOrEmpty(tempFilePath) && File.Exists(tempFilePath))
                {
                    baseResultDataValue.ResultDataValue = "/TempExcelFile/" + excelName;
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("生成Excel文件失败:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        //导出实体对象列表
        public Message RS_UDTO_GetEntityListExcelPath()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string entityName = "";
            string listTitle = "";
            string idList = "";
            string where = "";
            string sort = "";
            string fieldJson = "";
            string version = "";
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            DataSet ds = null;
            string[] allkey = HttpContext.Current.Request.Form.AllKeys;
            for (int i = 0; i < allkey.Length; i++)
            {
                switch (allkey[i])
                {
                    case "entityName"://实体名称
                        entityName = HttpContext.Current.Request.Form["entityName"];
                        break;
                    case "listTitle"://报表标题
                        listTitle = HttpContext.Current.Request.Form["listTitle"];
                        break;
                    case "idList"://实体ID列表，格式：ID1,ID2,ID3......
                        idList = HttpContext.Current.Request.Form["idList"];
                        break;
                    case "where"://查询条件，注意idList和where只取其中一个，优先判断idList
                        where = HttpContext.Current.Request.Form["where"];
                        break;
                    case "sort":
                        sort = HttpContext.Current.Request.Form["sort"];
                        break;
                    case "fieldJson":
                        fieldJson = HttpContext.Current.Request.Form["fieldJson"];
                        break;
                    case "version":
                        version = HttpContext.Current.Request.Form["version"];
                        break;
                }
            }
            try
            {
                if ((!string.IsNullOrEmpty(entityName)) && (!string.IsNullOrEmpty(fieldJson)))
                {
                    if ((!string.IsNullOrEmpty(idList)) || (!string.IsNullOrEmpty(where)))
                    {
                        if (!string.IsNullOrEmpty(idList))
                            where = entityName.ToLower() + ".Id in (" + idList + ")";
                        if (!string.IsNullOrEmpty(sort))
                            sort = CommonServiceMethod.GetSortHQL(sort);
                        if (string.IsNullOrEmpty(listTitle))
                            listTitle = "信息导出Excel列表";
                        baseResultDataValue = GetEntityListDataSet(entityName, where, sort, fieldJson, version, ref ds);
                        if (baseResultDataValue.success)
                        {
                            string excelName = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "." + CommonRS.GetExcelExtName();
                            string filePath = basePath + "\\TempExcelFile\\" + excelName;
                            if (!Directory.Exists(basePath + "\\TempExcelFile"))
                            {
                                Directory.CreateDirectory(basePath + "\\TempExcelFile");
                            }
                            if (ds != null && ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                            {
                                if (!ExcelHelper.CreateExcelByNPOI(ds.Tables[0], listTitle, filePath))
                                {
                                    filePath = "";
                                    baseResultDataValue.success = false;
                                    baseResultDataValue.ErrorInfo = "生成Excel文件失败！";
                                }
                            }
                            else
                            {
                                filePath = "";
                                baseResultDataValue.success = false;
                                baseResultDataValue.ErrorInfo = "无任何要导出的记录信息！";
                            }
                            if (!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
                            {
                                baseResultDataValue.ResultDataValue = "/TempExcelFile/" + excelName;
                            }
                        }
                    }
                    else
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "导出对象的查询条件不能为空！";
                    }
                }
                else

                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "导出对象的名称或字段参数值不能为空！";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        private BaseResultDataValue GetEntityListDataSet(string entityName, string where, string sort, string fieldJson, string version, ref DataSet dsEntityList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BindingFlags bf = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            PropertyInfo pi = this.GetType().GetProperty("IB" + entityName, bf);
            if (pi == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取不到导出列表对象！";
                return baseResultDataValue;
            }
            var obj = pi.GetValue(this, null);
            if (obj == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取不到导出列表对象！";
                return baseResultDataValue;
            }
            Type t = obj.GetType();
            MethodInfo mi = null;
            mi = t.GetMethod("SearchListByHQL", new Type[] { typeof(string), typeof(string), typeof(int), typeof(int) });
            if (mi == null)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取不到导出列表的查询方法！";
                return baseResultDataValue;
            }
            var entityList = mi.Invoke(obj, new object[] { where, CommonServiceMethod.GetSortHQL(sort), 0, 0 });
            if (entityList != null)
            {
                PropertyInfo pi_list = entityList.GetType().GetProperty("list");
                if (pi_list == null)
                {
                    return baseResultDataValue;
                }
                List<BaseEntity> baseEntity = new List<BaseEntity>();
                dynamic listEntity = pi_list.GetValue(entityList, null);
                if (listEntity == null)
                {
                    return baseResultDataValue;
                }
                foreach (var entity in listEntity)
                    baseEntity.Add(entity);
                dsEntityList = CommonRS.GetListObjectToDataSet<BaseEntity>(baseEntity, fieldJson, "0");
            }
            return baseResultDataValue;
        }

        //下载Excel文件
        public Stream RS_UDTO_DownLoadExcel(string fileName, string downFileName, int isUpLoadFile, int operateType)
        {
            FileStream tempFileStream = null;
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                string extName = Path.GetExtension(fileName);
                if (string.IsNullOrEmpty(downFileName))
                    downFileName = "试剂信息错误文件" + extName;
                else
                    downFileName = downFileName + extName;
                string tempFilePath = basePath + "\\UploadBaseTableInfo\\" + fileName;
                if (isUpLoadFile == 1)
                    tempFilePath = basePath + "\\TempExcelFile\\" + fileName;

                if (!string.IsNullOrEmpty(tempFilePath) && File.Exists(tempFilePath))
                {
                    tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);
                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + downFileName);
                        //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                        //WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment;filename=" + tempFileName);
                    }
                    else if (operateType == 1)//直接打开PDF文件
                    {
                        //WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/vnd.ms-excel";
                        WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + downFileName);
                        //System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                        //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + tempFileName);
                    }
                }
            }
            catch (Exception ex)
            {
                tempFileStream = null;
                throw new Exception(ex.Message);
            }
            return tempFileStream;
        }
        #endregion

        #region 供货管理
        public BaseResultDataValue RS_UDTO_AddReaBmsCenSaleDocAndDtl(ReaBmsCenSaleDoc entity, IList<ReaBmsCenSaleDtl> dtAddList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (entity == null)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "参数供货总单不能为空！";
                    return baseResultDataValue;
                }
                if (dtAddList == null || dtAddList.Count == 0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "参数供货明细单不能为空！";
                    return baseResultDataValue;
                }
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                baseResultDataValue = IBReaBmsCenSaleDoc.AddReaBmsCenSaleDocAndDtl(entity, dtAddList, empID, empName, true);
                if (entity.LabID <= 0)
                {
                    long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                    entity.LabID = labID;
                }
                if (baseResultDataValue.success)
                {
                    IBReaBmsCenSaleDoc.Get(IBReaBmsCenSaleDoc.Entity.Id);
                    baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsCenSaleDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("RS_UDTO_AddReaBmsCenSaleDocAndDtl:" + ex.Source);
            }
            return baseResultDataValue;
        }

        public BaseResultBool RS_UDTO_UpdateReaBmsCenSaleDocAndDtl(ReaBmsCenSaleDoc entity, string fields, IList<ReaBmsCenSaleDtl> dtAddList, IList<ReaBmsCenSaleDtl> dtEditList, string dtlFields)
        {

            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "参数供货总单不能为空！";
                return tempBaseResultBool;
            }
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                tempBaseResultBool = IBReaBmsCenSaleDoc.UpdateReaBmsCenSaleDocAndDt(entity, tempArray, dtAddList, dtEditList, dtlFields, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_UpdateReaBmsCenSaleDocAndDtl:" + ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool RS_UDTO_AddCreateBarcodeInfoOfSaleDocId(long saleDocId)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                tempBaseResultBool = IBReaBmsCenSaleDtl.AddCreateBarcodeInfoOfSaleDocId(saleDocId, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_AddCreateBarcodeInfoOfSaleDocId:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue RS_UDTO_AddReaBmsCenSaleDocAndDtlVO(ReaBmsCenSaleDoc entity, IList<ReaBmsCenSaleDtlVO> dtAddList)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (entity.LabID <= 0)
                {
                    long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                    entity.LabID = labID;
                }
                IBReaBmsCenSaleDoc.Entity = entity;
                tempBaseResultDataValue = IBReaBmsCenSaleDoc.AddReaBmsCenSaleDocAndDtl(dtAddList, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsCenSaleDoc.Get(IBReaBmsCenSaleDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsCenSaleDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        #endregion

        #region 客户端字典与平台供应商字典同步
        public Stream ST_UDTO_GetLabDictionaryExportToComp(string reaServerCompCode, string reaServerLabcCode)
        {
            Stream fileStream = null;
            long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
            string fileName = "";
            try
            {
                fileStream = IBReaGoodsOrgLink.GetLabDictionaryExportToComp(reaServerCompCode, reaServerLabcCode, ref fileName);
                Encoding code = Encoding.GetEncoding("gb2312");
                System.Web.HttpContext.Current.Response.ContentEncoding = code;
                System.Web.HttpContext.Current.Response.HeaderEncoding = code;

                fileName = EncodeFileName.ToEncodeFileName(fileName);
                System.Web.HttpContext.Current.Response.ContentType = "text/plain";
                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
            }
            catch (Exception ex)
            {
                string errorInfo = "导出供应商货品关系信息失败!" + ex.Message;
                ZhiFang.Common.Log.Log.Error(errorInfo + ex.StackTrace);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                return memoryStream;
            }

            return fileStream;
        }
        public Message ST_UDTO_AddUploadLabDictionaryOfCompSync()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string resultDataValue = "";

            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                brdv.success = true;
                HttpPostedFile file = null;

                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到上传的导入文件！";
                    brdv.ResultDataValue = resultDataValue;
                    brdv.success = false;
                    return WebOperationContext.Current.CreateTextResponse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
                }
                if (iTotal > 1)
                {
                    brdv.ErrorInfo = "检测到上传的导入文件存在多个！";
                    brdv.ResultDataValue = resultDataValue;
                    brdv.success = false;
                    return WebOperationContext.Current.CreateTextResponse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
                }

                file = HttpContext.Current.Request.Files[0];
                if (file.ContentLength <= 0)
                {
                    brdv.ErrorInfo = "上传的导入文件内容为空！";
                    brdv.ResultDataValue = resultDataValue;
                    brdv.success = false;
                    return WebOperationContext.Current.CreateTextResponse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
                }
                long empID = -1;
                string empIdStr = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (!string.IsNullOrEmpty(empIdStr))
                    empID = long.Parse(empIdStr);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));

                //SysPublicSet.IsSetLicense = true;
                brdv = IBReaGoodsOrgLink.AddUploadLabDictionaryOfCompSync(file, labID);
                //SysPublicSet.IsSetLicense = false;
            }
            catch (Exception ex)
            {
                //SysPublicSet.IsSetLicense = false;
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = resultDataValue;
                ZhiFang.Common.Log.Log.Error("导入实验室字典信息失败，错误信息为：" + ex.StackTrace);
                brdv.success = false;
            }

            return WebOperationContext.Current.CreateTextResponse(ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv), "text/plain", Encoding.UTF8);
        }
        #endregion

        #region 撤消功能
        public BaseResultBool ST_UDTO_UpdateCancelReaBmsInDocById(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (id <= 0)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "获取传入参数(id)信息的值为空!";
                return baseResultBool;
            }
            try
            {
                if (string.IsNullOrEmpty(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID)))
                {
                    baseResultBool.ErrorInfo = "获取登录帐号信息异常,请重新登录后再操作!";
                    baseResultBool.success = false;
                    return baseResultBool;
                }
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                baseResultBool = IBReaBmsInDoc.EditCancelReaBmsInDocById(id, empID, empName);
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("入库撤消错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return baseResultBool;
        }
        #endregion

        #region 库存合并查询及库存预警
        public BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtlListByGroupType(int groupType, int page, int limit, string fields, string where, string deptGoodsHql, string reaGoodsHql, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<ReaBmsQtyDtl> entityList = new List<ReaBmsQtyDtl>();
                entityList = IBReaBmsQtyDtl.SearchReaBmsQtyDtlListByGroupType(groupType, where, deptGoodsHql, reaGoodsHql, sort, page, limit);

                ParseObjectProperty pop = new ParseObjectProperty();
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList);
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
        public BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtlEntityListByGroupType(int groupType, int page, int limit, string fields, string where, string deptGoodsHql, string reaGoodsHql, string sort, bool isPlanish, bool isEmpPermission)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBReaBmsQtyDtl.SearchReaBmsQtyDtlEntityListByGroupType(groupType, where, deptGoodsHql, reaGoodsHql, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsQtyDtl>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchReaBmsQtyDtlListByStockWarning(int page, int limit, string fields, string where, string reaGoodsHql, bool isPlanish, int warningType, int groupType, float storePercent, string comparisonType, int monthValue, string sort)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBReaBmsQtyDtl.SearchReaBmsQtyDtlListByStockWarning(warningType, groupType, storePercent, comparisonType, monthValue, where, reaGoodsHql, page, limit, sort);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsQtyDtl>(entityList);
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
        public BaseResultDataValue RS_UDTO_GetReaGoodsWarningAlertInfo()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                baseResultDataValue = IBReaBmsQtyDtl.SearchReaGoodsWarningAlertInfo(empID, empName);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取预警信息错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error(ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region PDF清单报表
        public Stream RS_UDTO_SearchReaBmsReqDocMergeReportOfPdfByIdStr(string reaReportClass, string breportType, string idStr, long operateType, string frx)
        {
            Stream fileStream = null;
            string fileName = "", info = "";
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }
                if (string.IsNullOrEmpty(idStr))
                {
                    return ResponseResultStream.GetErrMemoryStreamInfo("传入的采购申请信息(idStr)为空!");
                }
                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                if (breportType == BTemplateType.采购申请.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.采购申请.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsReqDoc.SearchReaBmsReqDocMergeReportOfPdfByIdStr(reaReportClass, idStr, labId, labCName, breportType, frx, ref fileName);
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
        public Stream RS_UDTO_SearchReaBmsCenOrderDocOfPdfByIdStr(string reaReportClass, string breportType, string idStr, long operateType, string frx)
        {
            Stream fileStream = null;
            string fileName = "", info = "";
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }
                if (string.IsNullOrEmpty(idStr))
                {
                    return ResponseResultStream.GetErrMemoryStreamInfo("传入的订单信息(idStr)为空!");
                }

                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);

                if (breportType == BTemplateType.订货清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.订货清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsCenOrderDoc.SearchReaBmsCenOrderDocOfPdfByIdStr(reaReportClass, idStr, labId, labCName, breportType, frx, ref fileName);

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

        public BaseResultDataValue RS_UDTO_SearchPublicTemplateFileInfoByType(string publicTemplateDir)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<JObject> entityList = new EntityList<JObject>();
            try
            {
                entityList = IBBTemplate.GetPublicTemplateDirFile(publicTemplateDir);

                ParseObjectProperty pop = new ParseObjectProperty("");
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultBool RS_UDTO_AddBTemplateOfPublicTemplate(string entityList, long labId, string labCName)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (!string.IsNullOrEmpty(entityList))
            {
                try
                {
                    JArray jarray = JArray.Parse(entityList);
                    long empID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                    string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    if (labId <= 0)
                        labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                    if (string.IsNullOrEmpty(labCName))
                        labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                    baseResultBool = IBBTemplate.AddBTemplateOfPublicTemplate(jarray, labId, labCName, empID, empName);
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                    ZhiFang.Common.Log.Log.Error("新增报表模板保存失败:" + ex.StackTrace);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        public BaseResultDataValue RS_UDTO_SearchBTemplateByLabIdAndType(long labId, long breportType, string publicTemplateDir)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<JObject> entityList = new EntityList<JObject>();
            try
            {
                entityList = IBBTemplate.SearchBTemplateByLabIdAndType(labId, breportType, publicTemplateDir);

                ParseObjectProperty pop = new ParseObjectProperty("");
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public Stream RS_UDTO_SearchBusinessReportOfPdfById(string reaReportClass, string breportType, long id, long operateType, string frx)
        {
            Stream fileStream = null;
            string fileName = "", info = "";
            try
            {
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
                if (breportType == BTemplateType.采购申请.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.采购申请.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsReqDoc.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.订货清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.订货清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsCenOrderDoc.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.供货清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.供货清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsCenSaleDoc.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.入库清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.入库清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsInDoc.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.移库清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.移库清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsTransferDoc.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.出库清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.出库清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsOutDoc.SearchPdfReportOfTypeById(reaReportClass, id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.出库使用量.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.出库使用量.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaMonthUsageStatisticsDoc.GetPdfReportOfTypeById(reaReportClass, id, labId, labCName, breportType, frx, ref fileName);
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
        public Stream RS_UDTO_SearchReaBmsCenOrderDocListReportOfPdf(string where, long operateType, string frx)
        {
            Stream fileStream = null;
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }
                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);
                string fileName = "";
                fileStream = IBReaBmsCenOrderDoc.SearchReaBmsCenOrderDocListReportOfPdf(where, frx, labID, labCName, ref fileName);

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
                string errorInfo = "获取试剂耗材月订货清单PDF文件失败!";
                ZhiFang.Common.Log.Log.Error(errorInfo + ex.Message);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(errorInfo);
                return memoryStream;
            }
        }
        #endregion

        #region Excel导出
        public Stream RS_UDTO_SearchReaBmsCenOrderDocOfExcelByIdStr(string reaReportClass, long operateType, string breportType, string idStr, string frx)
        {
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
                long empID = long.Parse(employeeID);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);

                BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.订货清单.Key];
                info = dicEntity.Name;
                breportType = dicEntity.Name;
                //fileStream = IBReaBmsCenOrderDoc.SearchReaBmsCenOrderDocOfExcelByIdStr(idStr, labId, labCName, breportType, frx, ref fileName);

                fileStream = IBReaBmsCenOrderDoc.SearchReaBmsCenOrderDocOfExcelByIdStr(reaReportClass, idStr, labId, labCName, breportType, frx, ref fileName);

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
        public Stream RS_UDTO_SearchBusinessReportOfExcelById(long operateType, string breportType, long id, string frx)
        {
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
                long empID = long.Parse(employeeID);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);

                if (breportType == BTemplateType.采购申请.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.采购申请.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsReqDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.订货清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.订货清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsCenOrderDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.供货清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.供货清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsCenSaleDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.入库清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.入库清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsInDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.移库清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.移库清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsTransferDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.出库清单.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.出库清单.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaBmsOutDoc.SearchExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
                }
                else if (breportType == BTemplateType.出库使用量.Key)
                {
                    BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.出库使用量.Key];
                    info = dicEntity.Name;
                    breportType = dicEntity.Name;
                    fileStream = IBReaMonthUsageStatisticsDoc.GetExcelReportOfExcelById(id, labId, labCName, breportType, frx, ref fileName);
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
        public Stream RS_UDTO_DownLoadGetExportExcelReaBmsQtyDtlByGroupType(int qtyType, int groupType, long operateType, string where, string deptGoodsHql, string reaGoodsHql, string sort, int page, int limit)
        {
            ZhiFang.Common.Log.Log.Error("RS_UDTO_DownLoadGetExportExcelReaBmsQtyDtlByGroupType.where:" + where);
            ZhiFang.Common.Log.Log.Error("RS_UDTO_DownLoadGetExportExcelReaBmsQtyDtlByGroupType.deptGoodsHql:" + deptGoodsHql);
            ZhiFang.Common.Log.Log.Error("RS_UDTO_DownLoadGetExportExcelReaBmsQtyDtlByGroupType.reaGoodsHql:" + reaGoodsHql);
            Stream fileStream = null;
            string fileName = "库存清单.xlsx";
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }
                long empID = long.Parse(employeeID);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }

                fileStream = IBReaBmsQtyDtl.SearchExportExcelReaBmsQtyDtlByGroupType(labId, qtyType, groupType, where, deptGoodsHql, reaGoodsHql, sort, page, limit, ref fileName);
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
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出库存清单数据为空!");
                    return memoryStream;
                }
            }
            catch (Exception ex)
            {
                //fileStream = null;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出库存清单错误!");
                return memoryStream;
            }
            return fileStream;
        }
        public Stream RS_UDTO_DownLoadGetExportExcelByStockWarning(long operateType, int warningType, int groupType, float storePercent, string comparisonType, int monthValue, string where, string reaGoodsHql, string sort)
        {
            FileStream fileStream = null;
            string fileName = "库存预警清单.xlsx";
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("获取登录帐号信息失败,请重新登录后再操作!");
                    return memoryStream;
                }
                long empID = long.Parse(employeeID);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                fileStream = IBReaBmsQtyDtl.SearchExportExcelByStockWarning(labId, warningType, groupType, storePercent, comparisonType, monthValue, where, reaGoodsHql, sort, ref fileName);
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
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出库存预警清单为空!");
                    return memoryStream;
                }
            }
            catch (Exception ex)
            {
                //fileStream = null;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo("导出库存预警清单错误!");
                return memoryStream;
            }
            return fileStream;
        }
        public Stream RS_UDTO_SearchReaBmsQtyDtlOfExcelByQtyHql(long operateType, string breportType, int groupType, string qtyHql, string deptGoodsHql, string reaGoodsHql, string sort, string frx, bool isEmpPermission)
        {
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
                long empID = long.Parse(employeeID);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);

                BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.库存清单.Key];
                info = dicEntity.Name;
                breportType = dicEntity.Name;
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                fileStream = IBReaBmsQtyDtl.SearchReaBmsQtyDtlOfExcelByQtyHql(labId, labCName, breportType, groupType, qtyHql, deptGoodsHql, reaGoodsHql, sort, frx, ref fileName);

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
        public Stream RS_UDTO_SearchReaGoodsLotOfExcelByHql(long operateType, string breportType, string where, string reaGoodsHql, string sort, string frx)
        {
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
                long empID = long.Parse(employeeID);
                long labId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                string labCName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabName);

                BaseClassDicEntity dicEntity = BTemplateType.GetStatusDic()[BTemplateType.批号性能验证清单.Key];
                info = dicEntity.Name;
                breportType = dicEntity.Name;
                if (!string.IsNullOrEmpty(sort))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                fileStream = IBReaGoodsLot.SearchReaGoodsLotOfExcelByHql(labId, labCName, breportType, where, reaGoodsHql, sort, frx, ref fileName);

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
        #endregion

        #region ReaMonthUsageStatisticsDoc
        public BaseResultDataValue RS_UDTO_AddReaMonthUsageStatisticsDoc(ReaMonthUsageStatisticsDoc entity)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultDataValue;
                }
                long empID = long.Parse(employeeID);
                string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                tempBaseResultDataValue = IBReaMonthUsageStatisticsDoc.AddReaMonthUsageStatisticsDoc(entity, empID, employeeName);

            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("错误信息：" + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool RS_UDTO_DelReaMonthUsageStatisticsDocAndDtlByDocId(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool = IBReaMonthUsageStatisticsDoc.RemoveDocAndDtlByDocId(id);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        #endregion

        #region 定制查询
        //ReaStorageGoodsLink
        public BaseResultDataValue RS_UDTO_SearchReaStorageGoodsLinkEntityListByAllJoinHQL(int page, int limit, string fields, string where, string storageHql, string placeHql, string reaGoodsHql, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaStorageGoodsLink> tempEntityList = new EntityList<ReaStorageGoodsLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBReaStorageGoodsLink.SearchReaStorageGoodsLinkEntityListByAllJoinHql(where, storageHql, placeHql, reaGoodsHql, page, limit, sort);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaStorageGoodsLink>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchReaStorageGoodsLinkEntityListByAllJoinHQL.错误:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        //ReaEquipReagentLink
        public BaseResultDataValue RS_UDTO_SearchReaEquipReagentLinkNewEntityListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaEquipReagentLink> tempEntityList = new EntityList<ReaEquipReagentLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaEquipReagentLink.SearchNewEntityListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaEquipReagentLink.SearchNewEntityListByHQL(where, "", page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaEquipReagentLink>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchReaEquipReagentLinkNewEntityListByHQL.Error:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //ReaTestEquipItem
        public BaseResultDataValue RS_UDTO_SearchReaTestEquipItemEntityListByJoinHql(string where, string reatestitemHql, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaTestEquipItem> tempEntityList = new EntityList<ReaTestEquipItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaTestEquipItem.SearchReaTestEquipItemEntityListByJoinHql(where, reatestitemHql, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaTestEquipItem.SearchReaTestEquipItemEntityListByJoinHql(where, reatestitemHql, "", page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaTestEquipItem>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //ReaEquipTestItemReaGoodLink
        public BaseResultDataValue RS_UDTO_SearchReaEquipTestItemReaGoodLinkNewEntityListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaEquipTestItemReaGoodLink> tempEntityList = new EntityList<ReaEquipTestItemReaGoodLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaEquipTestItemReaGoodLink.SearchNewEntityListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaEquipTestItemReaGoodLink.SearchNewEntityListByHQL(where, "", page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaEquipTestItemReaGoodLink>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region 试剂客户端同步LIS系统基础信息
        public BaseResultBool RS_UDTO_AddSyncLISBasicInfo(string syncType)
        {
            BaseResultBool tempBaseResultDataValue = new BaseResultBool();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultDataValue;
                }
                long empID = long.Parse(employeeID);
                string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                long labid = long.Parse(Common.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
                switch (syncType)
                {
                    case "ReaTestEquipLab":
                        tempBaseResultDataValue = IBReaTestEquipLab.SaveSyncLisTestEquipLabInfo(labid);
                        break;
                    case "ReaTestItem":
                        tempBaseResultDataValue = IBReaTestItem.SaveSyncReaTestItemInfo();
                        break;
                    case "EquipLabAndTestItem":
                        tempBaseResultDataValue = IBReaTestEquipLab.SaveSyncLisTestEquipLabInfo(labid);
                        tempBaseResultDataValue = IBReaTestItem.SaveSyncReaTestItemInfo();
                        break;
                    case "ReaTestEquipItem":
                        tempBaseResultDataValue = IBReaTestEquipItem.SaveSyncLisReaTestEquipItemInfo("");
                        break;
                    default:
                        tempBaseResultDataValue = IBReaTestEquipLab.SaveSyncLisTestEquipLabInfo(labid);
                        tempBaseResultDataValue = IBReaTestItem.SaveSyncReaTestItemInfo();
                        tempBaseResultDataValue = IBReaTestEquipItem.SaveSyncLisReaTestEquipItemInfo("");
                        break;
                }
                tempBaseResultDataValue = IBReaTestEquipLab.SaveSyncLisTestEquipLabInfo(labid);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息：" + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool RS_UDTO_EditSyncLisTestEquipLabInfo()
        {
            BaseResultBool tempBaseResultDataValue = new BaseResultBool();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultDataValue;
                }
                long empID = long.Parse(employeeID);
                string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                long labid = long.Parse(Common.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
                tempBaseResultDataValue = IBReaTestEquipLab.SaveSyncLisTestEquipLabInfo(labid);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息：" + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool RS_UDTO_EditSyncReaTestItemInfo()
        {
            BaseResultBool tempBaseResultDataValue = new BaseResultBool();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultDataValue;
                }
                long empID = long.Parse(employeeID);
                string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                tempBaseResultDataValue = IBReaTestItem.SaveSyncReaTestItemInfo();
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息：" + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool RS_UDTO_EditSyncLisReaTestEquipItemInfo(string equipId)
        {
            BaseResultBool tempBaseResultDataValue = new BaseResultBool();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultDataValue;
                }
                long empID = long.Parse(employeeID);
                string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                tempBaseResultDataValue = IBReaTestEquipItem.SaveSyncLisReaTestEquipItemInfo(equipId);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息：" + ex.StackTrace);
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool RS_UDTO_AddReaLisTestStatisticalResults(string testType, string beginDate, string endDate, string equipIDStr, string lisEquipCodeStr, string where, string order, bool isCover)
        {
            BaseResultBool tempBaseResultDataValue = new BaseResultBool();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultDataValue;
                }
                long empID = long.Parse(employeeID);
                string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                //先
                tempBaseResultDataValue = IBReaLisTestStatisticalResults.SaveReaLisTestStatisticalResults(testType, beginDate, endDate, equipIDStr, lisEquipCodeStr, where, order, isCover);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("错误信息.Message：" + ex.Message);
                ZhiFang.Common.Log.Log.Error("错误信息.StackTrace：" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region BS导入CS试剂客户端信息
        public BaseResultDataValue RS_UDTO_DeleteCSUpdateToBSQtyDtlInfo(string entity)
        {
            SysPublicSet.IsSetLicense = true;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                return tempBaseResultDataValue;
            }
            long empID = long.Parse(employeeID);
            string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            if (string.IsNullOrEmpty(entity))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入参数(entity)信息为空!";
                return tempBaseResultDataValue;
            }
            try
            {
                long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                tempBaseResultDataValue = IBCSUpdateToBS.DeleteCSUpdateToBSQtyDtlInfo(labID, entity, empID, employeeName);
            }
            catch (Exception ex)
            {
                SysPublicSet.IsSetLicense = false;
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("CS试剂客户端升级BS失败:错误信息为:" + ex.StackTrace);
            }
            SysPublicSet.IsSetLicense = false;
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_AddCSUpdateToBSByStep(string entity)
        {
            SysPublicSet.IsSetLicense = true;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                return tempBaseResultDataValue;
            }
            long empID = long.Parse(employeeID);
            string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            if (string.IsNullOrEmpty(entity))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "传入参数(entity)信息为空!";
                return tempBaseResultDataValue;
            }
            try
            {
                long labID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(SysPublicSet.SysDicCookieSession.LabID));
                tempBaseResultDataValue = IBCSUpdateToBS.AddCSUpdateToBSByStep(labID, entity, empID, employeeName);
            }
            catch (Exception ex)
            {
                SysPublicSet.IsSetLicense = false;
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("CS试剂客户端升级BS失败:Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("CS试剂客户端升级BS失败:StackTrace:" + ex.StackTrace);
            }
            SysPublicSet.IsSetLicense = false;
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_GetCSUpdateToBSInfo()
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();

            try
            {
                tempBaseResultDataValue = IBCSUpdateToBS.GetCSUpdateToBSInfo();
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return tempBaseResultDataValue;
        }

        #endregion

        #region 移库管理
        public BaseResultDataValue RS_UDTO_SearchReaBmsTransferDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, string isUseEmpOut, string type, long empId)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsTransferDoc> tempEntityList = new EntityList<ReaBmsTransferDoc>();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultDataValue;
                }
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBReaBmsTransferDoc.SearchListByHQL(where, sort, page, limit, isUseEmpOut, type, empId);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsTransferDoc>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchSumReqGoodsQtyAndCurrentQtyByHQL(string dtlType, string qtyHql, string dtlHql, string goodsId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (dtlType == "ReaBmsTransferDtl")
                {
                    baseResultDataValue = IBReaBmsTransferDoc.SearchSumReqGoodsQtyAndCurrentQtyByHQL(qtyHql, dtlHql, goodsId);
                }
                else if (dtlType == "ReaBmsOutDtl")
                {
                    baseResultDataValue = IBReaBmsOutDoc.SearchSumReqGoodsQtyAndCurrentQtyByHQL(qtyHql, dtlHql, goodsId);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchSumReqGoodsQtyAndCurrentQtyByHQL.dtlType:" + dtlType + ",goodsId:" + goodsId);
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchSumReqGoodsQtyAndCurrentQtyByHQL.qtyHql:" + qtyHql);
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchSumReqGoodsQtyAndCurrentQtyByHQL.dtlHql:" + dtlHql);
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchSumReqGoodsQtyAndCurrentQtyByHQL.Message:" + ex.Message);
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchSumReqGoodsQtyAndCurrentQtyByHQL.StackTrace:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaBmsTransferDocByReqDeptHQL(string reqDeptId, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsTransferDoc> tempEntityList = new EntityList<ReaBmsTransferDoc>();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultDataValue;
                }
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                else
                {
                    sort = "";
                }
                long deptId = -1;
                if (!string.IsNullOrEmpty(reqDeptId))
                {
                    deptId = long.Parse(reqDeptId);
                }
                else
                {
                    deptId = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID));
                }
                tempEntityList = IBReaBmsTransferDoc.SearchReaBmsTransferDocByReqDeptHQL(deptId, where, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsTransferDoc>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_AddReaBmsTransferDocAndDtl(ReaBmsTransferDoc entity, IList<ReaBmsTransferDtl> dtAddList, bool isEmpTransfer)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultDataValue;
                }
                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (!entity.CreaterID.HasValue)
                    entity.CreaterID = empID;
                if (string.IsNullOrEmpty(entity.CreaterName))
                    entity.CreaterName = empName;
                if (!entity.OperDate.HasValue)
                    entity.OperDate = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsTransferDoc.Entity = entity;
                isEmpTransfer = isEmpTransfer == true ? true : false;
                tempBaseResultDataValue = IBReaBmsTransferDoc.AddTransferDocAndDtlOfApply(entity, dtAddList, isEmpTransfer, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsTransferDoc.Get(IBReaBmsTransferDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsTransferDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_AddReaBmsTransferDocAndDtl:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool RS_UDTO_UpdateReaBmsTransferDocAndDtl(ReaBmsTransferDoc entity, IList<ReaBmsTransferDtl> dtAddList, IList<ReaBmsTransferDtl> dtEditList, bool isEmpTransfer)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultBool;
                }
                long empID = long.Parse(employeeID);

                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                IBReaBmsTransferDoc.Entity = entity;
                //string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                isEmpTransfer = isEmpTransfer == true ? true : false;
                tempBaseResultBool = IBReaBmsTransferDoc.UpdateTransferDocAndDtl(entity, dtAddList, dtEditList, isEmpTransfer, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_UpdateReaBmsTransferDocAndDtl:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool RS_UDTO_UpdateReaBmsTransferDocAndDtlOfComp(ReaBmsTransferDoc entity, IList<ReaBmsTransferDtl> dtAddList, IList<ReaBmsTransferDtl> dtEditList, bool isEmpTransfer)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultBool;
                }
                long empID = long.Parse(employeeID);

                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                IBReaBmsTransferDoc.Entity = entity;
                //string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                isEmpTransfer = isEmpTransfer == true ? true : false;
                tempBaseResultBool = IBReaBmsTransferDoc.UpdateTransferDocAndDtl(entity, dtAddList, dtEditList, isEmpTransfer, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_UpdateReaBmsTransferDocAndDtlOfComp:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue RS_UDTO_AddGoodsReaBmsTransferDoc(ReaBmsTransferDoc reaBmsTransferDoc, IList<ReaBmsTransferDtl> listReaBmsTransferDtl, bool isEmpTransfer)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return baseResultDataValue;
                }
                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                isEmpTransfer = isEmpTransfer == true ? true : false;
                ZhiFang.Common.Log.Log.Debug("新增移库保存开始，操作人为:" + empName);
                baseResultDataValue = IBReaBmsTransferDoc.AddTransferDocAndDtlListOfComp(reaBmsTransferDoc, listReaBmsTransferDtl, isEmpTransfer, empID, empName);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增移库保存失败:" + ex.Message;
                ZhiFang.Common.Log.Log.Error("新增移库保存失败:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaBmsInDocOfQtyGEZeroByJoinHql(int page, int limit, string fields, string where, string dtlHql, string reaGoodsHql, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsInDoc> entityList = new EntityList<ReaBmsInDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                else
                {
                    sort = "";
                }
                entityList = IBReaBmsInDoc.SearchReaBmsInDocOfQtyGEZeroByJoinHql(where, dtlHql, reaGoodsHql, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsInDoc>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtlOfQtyGEZeroByJoinHql(int page, int limit, string fields, string where, string inDtlHql, string qtyHql, string reaGoodsHql, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                else
                {
                    sort = "";
                }
                entityList = IBReaBmsQtyDtl.SearchReaBmsQtyDtlEntityListOfQtyGEZeroByJoinHql(where, inDtlHql, qtyHql, reaGoodsHql, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsQtyDtl>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_AddTransferDocOfInDoc(ReaBmsInDoc inDoc, ReaBmsTransferDoc transferDoc, IList<ReaBmsTransferDtl> transferDtlList, bool isEmpTransfer)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (inDoc == null)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数inDoc为空!";
                return brdv;
            }
            if (transferDtlList == null || transferDtlList.Count <= 0)
            {
                brdv.success = false;
                brdv.ErrorInfo = "传入参数transferDtlList为空!";
                return brdv;
            }
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return brdv;
                }
                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                isEmpTransfer = isEmpTransfer == true ? true : false;
                brdv = IBReaBmsTransferDoc.AddTransferDocOfInDoc(inDoc, transferDoc, transferDtlList, isEmpTransfer, empID, empName);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = ex.Message;
                ZhiFang.Common.Log.Log.Error("新增移库保存错误:" + ex.StackTrace);
                //throw new Exception(ex.Message);
            }
            return brdv;
        }

        #endregion

        #region 出库管理
        public BaseResultDataValue RS_UDTO_AddGoodsReaBmsOutDoc(ReaBmsOutDoc reaBmsOutDoc, IList<ReaBmsOutDtl> listReaBmsOutDtl, bool isEmpOut, bool isNeedPerformanceTest)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (employeeID == "-1" || String.IsNullOrEmpty(employeeID) || reaBmsOutDoc.LabID == 0)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                return baseResultDataValue;
            }
            long empID = long.Parse(employeeID);
            string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            long deptID = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID));

            try
            {
                reaBmsOutDoc.DataAddTime = DateTime.Now;
                reaBmsOutDoc.DataUpdateTime = DateTime.Now;
                baseResultDataValue = IBReaBmsOutDoc.AddOutDocAndOutDtlListOfComp(ref reaBmsOutDoc, listReaBmsOutDtl, isEmpOut, isNeedPerformanceTest, empID, empName);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = ex.Message;
                //throw new Exception(ex.Message);
            }
            if (baseResultDataValue.success == true && reaBmsOutDoc.Status.ToString() == ReaBmsOutDocStatus.出库完成.Key)
            {
                //出库确认后是否调用退库接口
                BParameter parameter = IBBParameter.GetParameterByParaNo(SYSParaNo.出库确认后是否调用退库接口.Key);
                reaBmsOutDoc.OutTypeName = ReaBmsOutDocOutType.GetStatusDic()[reaBmsOutDoc.OutType.ToString()].Name;
                ZhiFang.Common.Log.Log.Info(reaBmsOutDoc.OutTypeName + ".出库确认后是否调用退库接口:" + (parameter.ParaValue == "1" ? "是" : "否"));
                if (parameter.ParaValue == "1")
                {
                    BaseResultData baseResultData = new BaseResultData();

                    if (reaBmsOutDoc.OutType.ToString() == ReaBmsOutDocOutType.退供应商.Key)
                        baseResultData = RS_UDTO_ReaGoodsBackStorageByOutDocInterface(reaBmsOutDoc, listReaBmsOutDtl);
                    else
                        baseResultData = ReaGoodsStorageSyncInterface(reaBmsOutDoc, listReaBmsOutDtl);
                    ZhiFang.Common.Log.Log.Info("出库成功!调用退库接口结果:" + baseResultData.success + ",返回信息" + baseResultData.message);

                    //ReaBmsOutDoc outDoc = IBReaBmsOutDoc.Get(reaBmsOutDoc.Id);
                    reaBmsOutDoc.IOFlag = int.Parse(ReaBmsOutDocIOFlag.退库成功.Key);
                    if (baseResultData.success == false)
                    {
                        reaBmsOutDoc.IOFlag = int.Parse(ReaBmsOutDocIOFlag.退库失败.Key);
                        reaBmsOutDoc.IOMemo = baseResultData.message;
                    }
                    //IBReaBmsOutDoc.Entity = outDoc;
                    //IBReaBmsOutDoc.Edit();

                    //baseResultDataValue.success = baseResultData.success;
                    //baseResultDataValue.ErrorInfo = "出库成功!调用退库接口结果:" + baseResultData.message;
                }

                #region 出库确认后是否上传试剂平台
                BParameter para = IBBParameter.GetParameterByParaNo(SYSParaNo.出库确认后是否上传试剂平台.Key);
                if (para.ParaValue.Trim() == "1")
                {
                    //出库单上传至智方试剂平台，调用内部接口                   
                    para = IBBParameter.GetParameterByParaNo(SYSParaNo.访问BS平台的URL.Key);
                    if (para != null && para.ParaValue.Trim() != "")
                    {
                        ZhiFang.Common.Log.Log.Info("出库成功!运行参数[出库确认后是否上传试剂平台]=是，继续上传到试剂平台");
                        string platformUrl = para.ParaValue;
                        if (platformUrl.Substring(platformUrl.Length - 1) != "/")
                        {
                            platformUrl = platformUrl + "/";
                        }

                        string clientUrl = HttpContext.Current.Request.Url.ToString().Replace("ReaManageService.svc/RS_UDTO_AddGoodsReaBmsOutDoc", "ZFReaRestfulService.svc/RS_Client_UploadOutDocAndDtlToPlatform");
                        string postDataStr = Newtonsoft.Json.JsonConvert.SerializeObject(new { outDocId = reaBmsOutDoc.Id, appkey = "", timestamp = "", token = "", version = "", platformUrl = platformUrl, empID = empID, empName = empName, deptID = deptID });
                        ZhiFang.Common.Log.Log.Info("调用接口地址：" + clientUrl + "，参数：" + postDataStr);
                        string resultStr = WebRequestHelp.Post(postDataStr, "JSON", clientUrl, ZFPlatformHelp.TIME_OUT_MILLISECOND);
                        if (!string.IsNullOrEmpty(resultStr))
                        {
                            JObject jresult = JObject.Parse(resultStr);
                            if (jresult["success"] != null)
                            {
                                baseResultDataValue.success = bool.Parse(jresult["success"].ToString());
                                baseResultDataValue.ErrorInfo = jresult["ErrorInfo"].ToString();
                            }
                            else
                            {
                                baseResultDataValue.success = false;
                                baseResultDataValue.ErrorInfo = "出库单号为:" + reaBmsOutDoc.OutDocNo + ",上传试剂平台的返回结果信息异常!";
                            }
                        }
                    }
                }
                #endregion

            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaBmsOutDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish, string isUseEmpOut, string type, long empId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsOutDoc> entityList = new EntityList<ReaBmsOutDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBReaBmsOutDoc.SearchListByHQL(where, sort, page, limit, isUseEmpOut, type, empId);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsOutDoc>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaBmsOutDocByReqDeptHQL(string reqDeptId, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsOutDoc> tempEntityList = new EntityList<ReaBmsOutDoc>();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultDataValue;
                }
                long empID = long.Parse(employeeID);
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                else
                {
                    sort = "";
                }
                long deptId = -1;
                if (!string.IsNullOrEmpty(reqDeptId))
                {
                    deptId = long.Parse(reqDeptId);
                }
                else
                {
                    deptId = long.Parse(ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID));
                }
                tempEntityList = IBReaBmsOutDoc.SearchReaBmsOutDocByReqDeptHQL(deptId, where, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsOutDoc>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.StackTrace;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_AddReaBmsOutDocAndDtl(ReaBmsOutDoc entity, IList<ReaBmsOutDtl> dtAddList, bool isEmpOut, bool isNeedPerformanceTest)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultDataValue;
                }
                long empID = long.Parse(employeeID);
                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                if (!entity.CreaterID.HasValue)
                    entity.CreaterID = empID;
                if (string.IsNullOrEmpty(entity.CreaterName))
                    entity.CreaterName = empName;
                if (!entity.OperDate.HasValue)
                    entity.OperDate = DateTime.Now;
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBReaBmsOutDoc.Entity = entity;
                isEmpOut = isEmpOut == true ? true : false;
                tempBaseResultDataValue = IBReaBmsOutDoc.AddReaBmsOutDocAndDtlOfApply(entity, dtAddList, isEmpOut, isNeedPerformanceTest, empID, empName);
                if (tempBaseResultDataValue.success)
                {
                    IBReaBmsOutDoc.Get(IBReaBmsOutDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaBmsOutDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_AddReaBmsOutDocAndDtl:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool RS_UDTO_UpdateReaBmsOutDocAndDtl(ReaBmsOutDoc entity, IList<ReaBmsOutDtl> dtAddList, IList<ReaBmsOutDtl> dtEditList, bool isEmpOut, bool isNeedPerformanceTest)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultBool;
                }
                long empID = long.Parse(employeeID);

                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                //IBReaBmsOutDoc.Entity = entity;
                //string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                isEmpOut = isEmpOut == true ? true : false;
                tempBaseResultBool = IBReaBmsOutDoc.UpdateReaBmsOutDocAndDtl(entity, dtAddList, dtEditList, isEmpOut, isNeedPerformanceTest, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_UpdateReaBmsOutDocAndDtl:" + ex.StackTrace);
            }
            //判断出库确认后是否需要调用出库接口
            if (tempBaseResultBool.success == true && entity.Status.ToString() == ReaBmsOutDocStatus.出库完成.Key)
            {
                BParameter parameter = IBBParameter.GetParameterByParaNo(SYSParaNo.出库确认后是否调用退库接口.Key);
                ZhiFang.Common.Log.Log.Info("出库确认.出库确认后是否调用退库接口:" + (parameter.ParaValue == "1" ? "是" : "否"));
                if (parameter.ParaValue == "1")
                {
                    IList<ReaBmsOutDtl> listReaBmsOutDtl = new List<ReaBmsOutDtl>();
                    if (dtAddList != null)
                    {
                        foreach (var outDtl in dtAddList)
                        {
                            listReaBmsOutDtl.Add(outDtl);
                        }
                    }
                    if (dtEditList != null)
                    {
                        foreach (var outDtl in dtEditList)
                        {
                            listReaBmsOutDtl.Add(outDtl);
                        }
                    }
                    BaseResultData baseResultData = ReaGoodsStorageSyncInterface(entity, listReaBmsOutDtl);
                    ZhiFang.Common.Log.Log.Info("出库成功!调用退库接口结果2:" + baseResultData.success + ",返回信息" + baseResultData.message);

                    ReaBmsOutDoc entity2 = IBReaBmsOutDoc.Get(entity.Id);
                    entity2.IOFlag = int.Parse(ReaBmsOutDocIOFlag.退库成功.Key);
                    if (baseResultData.success == false)
                    {
                        entity2.IOFlag = int.Parse(ReaBmsOutDocIOFlag.退库失败.Key);
                        entity2.IOMemo = baseResultData.message;
                    }
                    IBReaBmsOutDoc.Entity = entity2;
                    IBReaBmsOutDoc.Edit();

                    //tempBaseResultBool.success = baseResultData.success;
                    //tempBaseResultBool.ErrorInfo = "出库成功!调用退库接口结果:" + baseResultData.message;

                }
            }
            return tempBaseResultBool;
        }
        public BaseResultBool RS_UDTO_UpdateReaBmsOutDocByCheck(ReaBmsOutDoc entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                //IBReaBmsOutDoc.Entity = entity;
                try
                {
                    string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                    if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                        return baseResultBool;
                    }
                    long empID = long.Parse(employeeID);
                    string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool = IBReaBmsOutDoc.UpdateReaBmsOutDocByCheck(entity, tempArray, empID, empName);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBReaBmsOutDoc.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("RS_UDTO_UpdateReaBmsOutDocByCheck:" + ex.StackTrace);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        public BaseResultBool RS_UDTO_UpdateReaBmsOutDocAndDtlOfComp(ReaBmsOutDoc entity, IList<ReaBmsOutDtl> dtAddList, IList<ReaBmsOutDtl> dtEditList, bool isEmpOut, bool isNeedPerformanceTest)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultBool;
                }
                long empID = long.Parse(employeeID);

                string empName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                IBReaBmsOutDoc.Entity = entity;
                //string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                isEmpOut = isEmpOut == true ? true : false;
                tempBaseResultBool = IBReaBmsOutDoc.UpdateReaBmsOutDocAndDtlOfComp(entity, dtAddList, dtEditList, isEmpOut, isNeedPerformanceTest, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_UpdateReaBmsOutDocAndDtlOfComp:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }

        /// <summary>
        /// 获取某一货品上一次出库的批号和货运单号
        /// </summary>
        public BaseResultDataValue RS_UDTO_GetLastLotNoAndTransportNo(long goodsId)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                //根据货品ID，查询出库明细表，找到该货品的出库记录，按时间倒序
                IList<ReaBmsOutDtl> tempOutDtlList = IBReaBmsOutDtl.SearchListByHQL(string.Format("GoodsID={0}", goodsId), "DataAddTime desc", 0, 0).list;
                if (tempOutDtlList.Count > 0)
                {
                    //取到最近的一次的出库单OutDocID
                    long OutDocID = tempOutDtlList[0].OutDocID.Value;

                    //再根据OutDocID和GoodsID，找到最近一次该货品的出库记录
                    IList<ReaBmsOutDtl> tempList = tempOutDtlList.Where(p => p.OutDocID == OutDocID && p.GoodsID == goodsId).ToList();

                    //获取批号，用^拼接，返回给界面，界面将本次批号和上次批号进行比较
                    string lastLotNos = string.Join("^", tempList.Select(p => p.LotNo).Distinct().ToArray());

                    //获取货运单号，用^拼接，返回给界面，界面将本次货运单号和上次货运单号进行比较
                    string lastTransportNos = string.Join("^", tempList.Select(p => p.TransportNo).Distinct().ToArray());

                    baseResultDataValue.ResultDataValue = "{\"LastLotNo\":\"" + lastLotNos + "\",\"LastTransportNo\":\"" + lastTransportNos + "\"}";
                }
                else
                {
                    baseResultDataValue.ResultDataValue = "{\"LastLotNo\":\"\",\"LastTransportNo\":\"\"}";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = ex.Message;
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 在双击添加到待出库列表时，进行批号性能验证
        /// 验证通过才可以加入到待出库列表；验证没有通过不可以加入到待出库列表
        /// 判断逻辑：
        /// 1）货品里是否性能验证=是且系统参数开启，且批号性能验证状态=验证通过的，可以加入到待出库列表；
        /// 2）货品里是否性能验证=是且系统参数开启，且批号性能验证状态<> 验证通过的，不可以加入到待出库列表，提示“  性能验证没有通过，不能出库  ；
        /// 3）货品里是否性能验证=是且系统参数未开启，不判断，直接加入到待出库列表；
        /// 4）货品里是否性能验证=否，不判断，直接加入到待出库列表；
        /// </summary>
        /// <returns>true验证通过/false验证不通过</returns>
        public BaseResultBool RS_UDTO_LotNoPerformanceVerification(long goodsId, string goodsLotNo)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultBool;
                }
                BParameter entityPara = IBBParameter.GetParameterByParaNo("C-RBOD-ISPV-0032");//系统参数：库存货品是否需要性能验证后才能使用出库
                ReaGoods entityGoods = IBReaGoods.Get(goodsId); //货品表：是否需要性能验证
                IList<ReaGoodsLot> goodsLotList = IBReaGoodsLot.SearchListByHQL(string.Format("GoodsID={0} and LotNo='{1}'", goodsId, goodsLotNo));
                if (entityGoods.IsNeedPerformanceTest)
                {
                    if (entityPara.ParaValue == "1")
                    {
                        //货品里是否性能验证=是且系统参数开启,需要判断是否验证通过
                        if (goodsLotList.Count() == 0)
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = "未获取到当前批号的批号性能验证信息，无法进行验证！";
                            return tempBaseResultBool;
                        }
                        if (goodsLotList[0].VerificationStatus != long.Parse(ReaGoodsLotVerificationStatus.验证通过.Key))
                        {
                            tempBaseResultBool.success = false;
                            tempBaseResultBool.ErrorInfo = "当前批号性能验证没有通过，不能出库！";
                            return tempBaseResultBool;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_LotNoPerformanceVerification:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }

        /// <summary>
        /// 智方试剂平台使用
        /// 查询 状态=出库单上传平台 且 订货方类型=调拨 的出库单
        /// </summary>
        public BaseResultDataValue GetPlatformOutDocListByDBClient(string where, string sort, int page, int limit, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                EntityList<ReaBmsOutDoc> tempEntityList = new EntityList<ReaBmsOutDoc>();
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                else
                {
                    sort = "";
                }
                tempEntityList = IBReaBmsOutDoc.GetPlatformOutDocListByDBClient(where, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsOutDoc>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.StackTrace;
            }
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 智方试剂平台查询使用，根据出库主单查询明细。
        /// hasLabId=false，不增加LabID的默认条件
        /// </summary>
        public BaseResultDataValue GetPlatformOutDtlListByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsOutDtl> entityList = new EntityList<ReaBmsOutDtl>();
            try
            {
                //筛选出库明细，只有属于当前登录供应商的货品，才显示
                string hRDeptID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.HRDeptID);
                if (string.IsNullOrEmpty(hRDeptID))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取当前用户的部门信息,请重新登录！";
                    return baseResultDataValue;
                }
                long deptID = long.Parse(hRDeptID);
                List<long> allParent = IBHRDept.GetParentDeptIdListByDeptId(deptID);
                deptID = allParent[allParent.Count - 1];
                HRDept hRDept = IBHRDept.Get(deptID);

                where += " and reabmsoutdtl.ReaServerCompCode=" + hRDept.UseCode;
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBReaBmsOutDtl.GetListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit, false);
                }
                else
                {
                    entityList = IBReaBmsOutDtl.GetListByHQL(where, "", page, limit, false);
                }
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        #endregion

        #region 库房权限查询
        public BaseResultDataValue RS_UDTO_SearchListByStorageAndLinHQL(int page, int limit, string fields, string storageHql, string linkHql, string operType, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(storageHql) && string.IsNullOrEmpty(linkHql))
            {
                return baseResultDataValue;
            }
            EntityList<ReaStorage> entityList = new EntityList<ReaStorage>();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return baseResultDataValue;
                }
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                else
                {
                    sort = "";
                }
                entityList = IBReaStorage.SearchListByStorageAndLinHQL(storageHql, linkHql, operType, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaStorage>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaPlaceByPlaceAndLinHQL(int page, int limit, string fields, string placeHql, string linkHql, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(placeHql) && string.IsNullOrEmpty(linkHql))
            {
                return baseResultDataValue;
            }
            EntityList<ReaPlace> entityList = new EntityList<ReaPlace>();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return baseResultDataValue;
                }
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                else
                {
                    sort = "";
                }
                entityList = IBReaPlace.SearchEntityListByPlaceeAndLinHQL(placeHql, linkHql, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaPlace>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue RS_UDTO_GetStoragePlaceListTree(bool isEmpPermission, string operType)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree tempBaseResultTree = new BaseResultTree();
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "无法获取当前用户的ID信息";
                    return tempBaseResultDataValue;
                }
                tempBaseResultTree = IBReaStorage.GetReaStorageTree(long.Parse(employeeID), isEmpPermission, operType);
                if (tempBaseResultTree.Tree.Count > 0)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree);
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region 联合查询
        public BaseResultDataValue RS_UDTO_SearchReaGoodsLotByAllJoinHql(int page, int limit, string fields, string where, string reaGoodsHql, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsLot> entityList = new EntityList<ReaGoodsLot>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                else
                {
                    sort = "";
                }
                entityList = IBReaGoodsLot.SearchReaGoodsLotEntityListByAllJoinHql(where, reaGoodsHql, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsLot>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtlOperationByAllJoinHql(int page, int limit, string fields, string where, string reaGoodsHql, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaBmsQtyDtlOperation> tempEntityList = new EntityList<ReaBmsQtyDtlOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                else
                {
                    sort = "";
                }
                tempEntityList = IBReaBmsQtyDtlOperation.SearchReaBmsQtyDtlOperationEntityListByAllJoinHql(where, reaGoodsHql, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaBmsQtyDtlOperation>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region 接口服务
        public BaseResultData RS_UDTO_InputSaleDocInterface(string saleDocNo, long compOrgId, long labOrgId, string mainFields, string childFields, string entityType)
        {
            BaseResultData baseResultData = new BaseResultData();
            try
            {
                HREmployee emp = null;
                CenOrg cenOrg = null;
                baseResultData = GetCurUserCenOrg(ref emp, ref cenOrg);
                if (!baseResultData.success)
                    return baseResultData;

                bool isExist = false;
                if (entityType == InterfaceDataConvertType.转供货单.Key)
                {
                    IList<ReaBmsCenSaleDoc> list = IBReaBmsCenSaleDoc.SearchListByHQL(" reabmscensaledoc.OtherDocNo=\'" + saleDocNo + "\'");
                    isExist = (list != null && list.Count > 0);
                }
                else if (entityType == InterfaceDataConvertType.转验收单.Key)
                {
                    IList<ReaBmsCenSaleDocConfirm> list = IBReaBmsCenSaleDocConfirm.SearchListByHQL(" reabmscensaledocconfirm.OtherDocNo=\'" + saleDocNo + "\'");
                    isExist = (list != null && list.Count > 0);
                }
                else if (entityType == InterfaceDataConvertType.转入库单.Key)
                {
                    IList<ReaBmsInDoc> list = IBReaBmsInDoc.SearchListByHQL(" reabmsindoc.OtherDocNo=\'" + saleDocNo + "\'");
                    isExist = (list != null && list.Count > 0);
                }
                if (isExist)
                {
                    baseResultData.success = false;
                    baseResultData.message = "第三方单号为【" + saleDocNo + "】的信息已经存在！";
                    return baseResultData;
                }

                ReaBmsCenSaleDoc saleDoc = new ReaBmsCenSaleDoc();
                IList<ReaBmsCenSaleDtl> saleDtlList = new List<ReaBmsCenSaleDtl>();
                long storageID = 0;
                baseResultData = InterfaceCommon.GetThirdSaleDocType();
                if (baseResultData.success && (!string.IsNullOrEmpty(baseResultData.data)))
                {
                    string userType = baseResultData.data.ToLower();
                    ZhiFang.Common.Log.Log.Info("接口获取供货单-开始-" + userType);
                    if (userType == "bjsjtyy_wanghai") //北京世纪坛医院望海
                    {
                        InterfaceWangHai interfaceWangHai = new InterfaceWangHai();
                        baseResultData = interfaceWangHai.CheckInStorageByCode(saleDocNo, ref saleDoc, ref saleDtlList, ref storageID);
                        if (!baseResultData.success)
                            return baseResultData;
                    }
                    else if (userType == "jxgnfyyy_his" || userType == "sshqyy_his" || userType == "nnsdyrmyy_his") //江西赣南附一医院
                    {
                        InterfaceView interfaceView = new InterfaceView();
                        baseResultData = interfaceView.GetReaSaleDocInfo(saleDocNo, IBReaBmsCenSaleDoc, ref saleDoc, ref saleDtlList, ref storageID);
                        if (!baseResultData.success)
                            return baseResultData;
                    }
                    else if (userType == "bjellyy_his") //北京二龙路医院
                    {
                        InterfaceErLongLu interfaceErLongLu = new InterfaceErLongLu();
                        baseResultData = interfaceErLongLu.GetReaSaleDocInfo(saleDocNo, IBReaBmsCenSaleDoc, emp, cenOrg, ref saleDoc, ref saleDtlList, ref storageID);
                        if (!baseResultData.success)
                            return baseResultData;
                    }
                    InterfaceDataConvert ifdc = new InterfaceDataConvert();
                    if (entityType == InterfaceDataConvertType.转供货单.Key)
                    {
                        ZhiFang.Common.Log.Log.Info("接口获取供货单-开始-供货单");
                        BaseResultDataValue brdv = ifdc.GetReaBmsSaleDocJson<ReaBmsCenSaleDoc, ReaBmsCenSaleDtl>(saleDoc, saleDtlList, mainFields, childFields);
                        baseResultData.success = brdv.success;
                        baseResultData.data = brdv.ResultDataValue;
                        baseResultData.message = brdv.ErrorInfo;
                        ZhiFang.Common.Log.Log.Info("接口获取供货单-结束-供货单");
                    }
                    else if (entityType == InterfaceDataConvertType.转验收单.Key)
                    {
                        ZhiFang.Common.Log.Log.Info("接口获取供货单-开始-供货单转验收单");
                        BaseResultDataValue brdv = ifdc.GetReaBmsSaleDocCovertJson<ReaBmsCenSaleDocConfirm, ReaBmsCenSaleDtlConfirm>(saleDoc, saleDtlList, mainFields, childFields, storageID);
                        baseResultData.success = brdv.success;
                        baseResultData.data = brdv.ResultDataValue;
                        baseResultData.message = brdv.ErrorInfo;
                        ZhiFang.Common.Log.Log.Info("接口获取供货单-结束-供货单转验收单");
                    }
                    else if (entityType == InterfaceDataConvertType.转入库单.Key)
                    {
                        ZhiFang.Common.Log.Log.Info("接口获取供货单-开始-供货单转入库单");
                        BaseResultDataValue brdv = ifdc.GetReaBmsSaleDocCovertJson<ReaBmsInDoc, ReaBmsInDtl>(saleDoc, saleDtlList, mainFields, childFields, storageID);
                        baseResultData.success = brdv.success;
                        baseResultData.data = brdv.ResultDataValue;
                        baseResultData.message = brdv.ErrorInfo;
                        ZhiFang.Common.Log.Log.Info("接口获取供货单-结束-供货单转入库单");
                    }
                    ZhiFang.Common.Log.Log.Info("接口获取供货单-结束");
                }
            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "获取供货单错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultData;
        }

        private BaseResultData GetCurUserCenOrg(ref HREmployee emp, ref CenOrg cenOrg)
        {
            BaseResultData brd = new BaseResultData();
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (string.IsNullOrEmpty(employeeID))
            {
                brd.success = false;
                brd.message = "无法获取当前用户的ID信息";
                ZhiFang.Common.Log.Log.Info(brd.message);
                return brd;
            }
            HREmployee hrEMP = IBHREmployee.Get(long.Parse(employeeID));
            if (hrEMP == null)
            {
                brd.success = false;
                brd.message = "无法获取当前用户信息";
                ZhiFang.Common.Log.Log.Info(brd.message);
                return brd;

            }
            emp = hrEMP;
            string orgNo = hrEMP.HRDept.UseCode;
            if (string.IsNullOrEmpty(orgNo))
            {
                brd.success = false;
                brd.message = "当前用户所属机构的编码为空！请联系管理员维护！";
                ZhiFang.Common.Log.Log.Info(brd.message);
                return brd;
            }
            else
            {
                IList<CenOrg> listCenOrg = IBCenOrg.SearchListByHQL(" cenorg.OrgNo=\'" + orgNo + "\'");
                if (listCenOrg != null && listCenOrg.Count > 0)
                    cenOrg = listCenOrg[0];
            }
            return brd;
        }

        public BaseResultDataValue Test(string entityType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            ReaBmsCenSaleDoc saleDoc = IBReaBmsCenSaleDoc.Get(4705007767408469091);
            IList<ReaBmsCenSaleDtl> saleDtlList = IBReaBmsCenSaleDtl.SearchListByHQL(" reabmscensaledtl.SaleDocID=4705007767408469091");
            string mainFields = "ReaBmsCenSaleDoc_Id,ReaBmsCenSaleDoc_LabcName,ReaBmsCenSaleDoc_CompanyName";
            string childFields = "ReaBmsCenSaleDtl_SaleDocID,ReaBmsCenSaleDtl_SaleDocNo,ReaBmsCenSaleDtl_GoodsQty";
            InterfaceDataConvert ifdc = new InterfaceDataConvert();
            if (entityType == InterfaceDataConvertType.转供货单.Key)
                baseResultDataValue = ifdc.GetReaBmsSaleDocJson<ReaBmsCenSaleDoc, ReaBmsCenSaleDtl>(saleDoc, saleDtlList, mainFields, childFields);
            else if (entityType == InterfaceDataConvertType.转验收单.Key)
            {
                mainFields = "ReaBmsCenSaleDocConfirm_Id,ReaBmsCenSaleDocConfirm_LabcName,ReaBmsCenSaleDocConfirm_CompanyName";
                childFields = "ReaBmsCenSaleDtlConfirm_SaleDocID,ReaBmsCenSaleDtlConfirm_SaleDocNo,ReaBmsCenSaleDtlConfirm_GoodsQty";
                baseResultDataValue = ifdc.GetReaBmsSaleDocCovertJson<ReaBmsCenSaleDocConfirm, ReaBmsCenSaleDtlConfirm>(saleDoc, saleDtlList, mainFields, childFields, 0);
            }
            else if (entityType == InterfaceDataConvertType.转入库单.Key)
            {
                mainFields = "ReaBmsInDoc_Id,ReaBmsInDoc_InDocNo,ReaBmsInDoc_CompanyName";
                childFields = "ReaBmsInDtl_GoodsCName,ReaBmsInDtl_InDocNo,ReaBmsInDtl_GoodsQty";
                baseResultDataValue = ifdc.GetReaBmsSaleDocCovertJson<ReaBmsInDoc, ReaBmsInDtl>(saleDoc, saleDtlList, mainFields, childFields, 0);
            }
            return baseResultDataValue;
        }


        /// <summary>
        /// 货品出库与退库入库接口
        /// </summary>
        /// <param name="outDoc"></param>
        /// <param name="outDtlList"></param>
        /// <returns></returns>
        public BaseResultData RS_UDTO_ReaGoodsStorageSyncInterface(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> outDtlList)
        {
            BaseResultData baseResultData = new BaseResultData();
            baseResultData = ReaGoodsStorageSyncInterface(outDoc, outDtlList);
            return baseResultData;
        }

        /// <summary>
        /// 货品出库与退库入库接口
        /// </summary>
        /// <param name="outDoc"></param>
        /// <param name="outDtlList"></param>
        /// <returns></returns>
        public BaseResultData RS_UDTO_ReaGoodsStorageSyncInterfaceByID(long outDocID, string outDocNo)
        {
            BaseResultData baseResultData = new BaseResultData();
            ReaBmsOutDoc outDoc = null;
            IList<ReaBmsOutDtl> outDtlList = null;
            if (outDocID > 0)
            {
                outDoc = IBReaBmsOutDoc.Get(outDocID);
                if (outDoc != null)
                    outDtlList = IBReaBmsOutDtl.SearchListByHQL(" reabmsoutdtl.OutDocID=" + outDocID.ToString());
            }
            else if (!string.IsNullOrEmpty(outDocNo))
            {
                IList<ReaBmsOutDoc> outDocList = IBReaBmsOutDoc.SearchListByHQL(" reabmsoutdoc.OutDocNo=\'" + outDocNo + "\'");
                if (outDocList != null && outDocList.Count > 0)
                {
                    outDoc = outDocList[0];
                    outDtlList = IBReaBmsOutDtl.SearchListByHQL(" reabmsoutdtl.OutDocID=" + outDoc.Id.ToString());
                }
            }
            if (outDoc != null && outDtlList != null && outDtlList.Count > 0)
            {
                baseResultData = ReaGoodsStorageSyncInterface(outDoc, outDtlList);

                outDoc.IOFlag = int.Parse(ReaBmsOutDocIOFlag.退库成功.Key);
                if (baseResultData.success == false)
                {
                    outDoc.IOFlag = int.Parse(ReaBmsOutDocIOFlag.退库失败.Key);
                    outDoc.IOMemo = baseResultData.message;
                }
                IBReaBmsOutDoc.Entity = outDoc;
                IBReaBmsOutDoc.Edit();
            }
            else
            {
                baseResultData.success = false;
                baseResultData.message = "";
            }
            return baseResultData;
        }

        /// <summary>
        /// 货品出库与退库入库
        /// </summary>
        /// <param name="outDoc"></param>
        /// <param name="outDtlList"></param>
        /// <returns></returns>
        private BaseResultData ReaGoodsStorageSyncInterface(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> outDtlList)
        {
            BaseResultData baseResultData = new BaseResultData();
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID))
                {
                    baseResultData.success = false;
                    baseResultData.message = "无法获取当前用户的ID信息";
                    ZhiFang.Common.Log.Log.Info(baseResultData.message);
                    return baseResultData;
                }
                baseResultData = InterfaceCommon.GetThirdSaleDocType();
                if (!baseResultData.success)
                {
                    return baseResultData;
                }
                string userType = baseResultData.data.ToLower();
                if (string.IsNullOrEmpty(userType))
                {
                    baseResultData.success = false;
                    baseResultData.message = "获取不到第三方接口配置信息！";
                    return baseResultData;
                }

                if (userType == "bjsjtyy_wanghai") //北京世纪坛医院--望海物资
                {
                    ZhiFang.Common.Log.Log.Info("望海物资出库确认后开始调用退库接口");
                    InterfaceWangHai interfaceWangHai = new InterfaceWangHai();
                    //将出库信息转换为退库的入库明细信息
                    IList<ReaBmsInDtl> inDtlList = new List<ReaBmsInDtl>();
                    InterfaceDataConvert ifdc = new InterfaceDataConvert();
                    baseResultData = ifdc.DisposeReaBmsOutDtlList(outDoc, ref outDtlList);
                    if (!baseResultData.success)
                    {
                        return baseResultData;
                    }
                    baseResultData = ifdc.ConvertReaBmsInDtlList(outDoc, outDtlList, ref inDtlList);
                    //baseResultData = ifdc.ConvertReaBmsInDtlList(outDoc, outDtlList, ref inDtlList);
                    if (!baseResultData.success)
                    {
                        return baseResultData;
                    }
                    if (inDtlList == null || inDtlList.Count <= 0)
                    {
                        ZhiFang.Common.Log.Log.Info("出库确认后调用退库接口.获取退库封装的入库明细信息为空,不调用退库接口!");
                        return baseResultData;
                    }

                    baseResultData = interfaceWangHai.ReaGoodsStocksSync(outDoc, outDtlList);
                    ZhiFang.Common.Log.Log.Info("望海物资出库确认后结束调用退库接口");
                }
                else if (userType == "gnyxfsdyyy_hrp")//赣南医学附属第一医院--上海京颐HRP
                {
                    ZhiFang.Common.Log.Log.Info("赣南医学附属第一医院，出库确认执行成功后，调用HRP接口发送消息开始");

                    //获取当前部门ID，其所有父级部门，然后固定取第二级
                    HRDept secondDept = IBHRDept.GetSecondDept(outDoc.DeptID.Value);
                    if (secondDept == null)
                    {
                        baseResultData.success = false;
                        baseResultData.message = "订单上传失败，当前人员所属部门错误！";
                        return baseResultData;
                    }

                    InterfaceGanNanFSDYYY ganNanFSDYYY = new InterfaceGanNanFSDYYY();
                    string sendXml = ganNanFSDYYY.GetDoUpdateStatus(outDoc, outDtlList, secondDept);
                    baseResultData = ganNanFSDYYY.CallService("DoUpdateStatus", sendXml);
                    ZhiFang.Common.Log.Log.Info("赣南医学附属第一医院，出库确认执行成功后，调用HRP接口发送消息结束");
                }
                else
                {
                    ZhiFang.Common.Log.Log.Info(userType + "---出库确认后无需调用退库同步接口!");
                }
            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "货品出库同步接口错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            return baseResultData;
        }

        /// <summary>
        /// 货品退供应商接口
        /// </summary>
        /// <param name="inDoc"></param>
        /// <param name="inDtlList"></param>
        /// <returns></returns>
        public BaseResultData RS_UDTO_ReaGoodsBackStorageByInDocInterface(ReaBmsInDoc inDoc, IList<ReaBmsInDtl> inDtlList)
        {
            BaseResultData baseResultData = new BaseResultData();
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID))
                {
                    baseResultData.success = false;
                    baseResultData.message = "无法获取当前用户的ID信息";
                    ZhiFang.Common.Log.Log.Info(baseResultData.message);
                    return baseResultData;
                }
                HREmployee emp = IBHREmployee.Get(long.Parse(employeeID));
                baseResultData = InterfaceCommon.GetThirdSaleDocType();
                if (!baseResultData.success)
                {
                    return baseResultData;
                }
                string userType = baseResultData.data.ToLower();
                if (string.IsNullOrEmpty(userType))
                {
                    baseResultData.success = false;
                    baseResultData.message = "获取不到第三方接口配置信息！";
                    return baseResultData;
                }
                ZhiFang.Common.Log.Log.Info("退供应商接口：参数为入库单和入库单明细！");
                inDoc = IBReaBmsInDoc.Get(inDoc.Id);
                inDtlList = IBReaBmsInDtl.SearchListByHQL(" reabmsindtl.InDocID=" + inDoc.Id.ToString());
                if (inDtlList == null || inDtlList.Count <= 0)
                {
                    baseResultData.success = false;
                    baseResultData.message = "出库确认后调用退供应商接口,获取退库封装的入库明细信息为空,不调用退库接口!";
                    ZhiFang.Common.Log.Log.Info(baseResultData.message);
                    return baseResultData;
                }
                ZhiFang.Common.Log.Log.Info("货品退供应商接口开始-" + userType);

                if (userType == "bjsjtyy_wanghai") //北京世纪坛医院--望海物资
                {
                    InterfaceWangHai interfaceWangHai = new InterfaceWangHai();
                    baseResultData = interfaceWangHai.ReaGoodsCancellingStocks(inDoc, inDtlList);
                }
                else if (userType == "yzrmyy_donghua") //鄞州人民医院--东华物资
                {
                    InterfaceYZRMYYDongHua interfaceYZRMYYDongHua = new InterfaceYZRMYYDongHua();
                    baseResultData = interfaceYZRMYYDongHua.ReaGoodsCancellingStocks(inDoc, inDtlList, emp);
                }
                ZhiFang.Common.Log.Log.Info("货品退供应商接口结束-" + userType);
            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "货品退供应商接口错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            return baseResultData;
        }

        /// <summary>
        /// 货品退供应商接口
        /// </summary>
        /// <param name="outDoc"></param>
        /// <param name="outDtlList"></param>
        /// <returns></returns>
        public BaseResultData RS_UDTO_ReaGoodsBackStorageByOutDocInterface(ReaBmsOutDoc outDoc, IList<ReaBmsOutDtl> outDtlList)
        {
            BaseResultData baseResultData = new BaseResultData();
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID))
                {
                    baseResultData.success = false;
                    baseResultData.message = "无法获取当前用户的ID信息";
                    ZhiFang.Common.Log.Log.Info(baseResultData.message);
                    return baseResultData;
                }
                HREmployee emp = IBHREmployee.Get(long.Parse(employeeID));
                baseResultData = InterfaceCommon.GetThirdSaleDocType();
                if (!baseResultData.success)
                {
                    return baseResultData;
                }
                string userType = baseResultData.data.ToLower();
                if (string.IsNullOrEmpty(userType))
                {
                    baseResultData.success = false;
                    baseResultData.message = "获取不到第三方接口配置信息！";
                    return baseResultData;
                }

                ZhiFang.Common.Log.Log.Info("退供应商接口：参数为出库单和出库单明细！");

                //将出库信息转换为退库的入库明细信息
                IList<ReaBmsInDtl> inDtlList = new List<ReaBmsInDtl>();
                InterfaceDataConvert ifdc = new InterfaceDataConvert();
                baseResultData = ifdc.ConvertReaBmsInDtlList(outDoc, outDtlList, ref inDtlList);
                if (!baseResultData.success)
                {
                    return baseResultData;
                }

                if (inDtlList == null || inDtlList.Count <= 0)
                {
                    baseResultData.success = false;
                    baseResultData.message = "出库确认后调用退供应商接口,获取退库封装的入库明细信息为空,不调用退库接口!";
                    ZhiFang.Common.Log.Log.Info(baseResultData.message);
                    return baseResultData;
                }
                Dictionary<long, KeyValuePair<ReaBmsInDoc, IList<ReaBmsInDtl>>> dic = null;
                ifdc.ConvertInDtlListToInDocList(inDtlList, ref dic);
                if (dic == null || dic.Count == 0)
                {
                    baseResultData.success = false;
                    baseResultData.message = "出库确认后调用退供应商接口,获取退库封装的入库主单与明细信息为空,不调用退库接口!";
                    ZhiFang.Common.Log.Log.Info(baseResultData.message);
                    return baseResultData;
                }
                ZhiFang.Common.Log.Log.Info("货品退供应商接口开始-" + userType);

                if (userType == "bjsjtyy_wanghai") //北京世纪坛医院--望海物资
                {
                    InterfaceWangHai interfaceWangHai = new InterfaceWangHai();
                    baseResultData = interfaceWangHai.ReaGoodsCancellingStocks(dic);
                }
                else if (userType == "yzrmyy_donghua") //鄞州人民医院--东华物资
                {
                    InterfaceYZRMYYDongHua interfaceYZRMYYDongHua = new InterfaceYZRMYYDongHua();
                    baseResultData = interfaceYZRMYYDongHua.ReaGoodsCancellingStocks(dic, emp);
                }
                ZhiFang.Common.Log.Log.Info("货品退供应商接口结束-" + userType);
            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "货品退供应商接口错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            return baseResultData;
        }


        /// <summary>
        /// 货品退供应商接口
        /// </summary>
        /// <param name="outDocID"></param>
        /// <param name="outDocNo"></param>
        /// <returns></returns>
        public BaseResultData RS_UDTO_ReaGoodsBackStorageByOutDocInterfaceByID(long outDocID, string outDocNo)
        {
            BaseResultData baseResultData = new BaseResultData();
            ReaBmsOutDoc outDoc = null;
            IList<ReaBmsOutDtl> outDtlList = null;
            if (outDocID > 0)
            {
                outDoc = IBReaBmsOutDoc.Get(outDocID);
                if (outDoc != null)
                    outDtlList = IBReaBmsOutDtl.SearchListByHQL(" reabmsoutdtl.OutDocID=" + outDocID.ToString());
            }
            else if (!string.IsNullOrEmpty(outDocNo))
            {
                IList<ReaBmsOutDoc> outDocList = IBReaBmsOutDoc.SearchListByHQL(" reabmsoutdoc.OutDocNo=\'" + outDocNo + "\'");
                if (outDocList != null && outDocList.Count > 0)
                {
                    outDoc = outDocList[0];
                    outDtlList = IBReaBmsOutDtl.SearchListByHQL(" reabmsoutdtl.OutDocID=" + outDoc.Id.ToString());
                }
            }
            if (outDoc != null && outDtlList != null && outDtlList.Count > 0)
                baseResultData = RS_UDTO_ReaGoodsBackStorageByOutDocInterface(outDoc, outDtlList);
            else
            {
                baseResultData.success = false;
                baseResultData.message = "出库单信息为空！";
            }
            return baseResultData;
        }

        /// <summary>
        /// 订单提交接口
        /// 前台通过判断参数，确定是否调用该服务
        ///     参数[订单上传类型]：1:不上传;2:上传平台;3:上传第三方系统;4:上传平台及上传第三方系统(预留,未实现);
        ///     当前处理：订单上传类型=3，调用该服务
        /// </summary>
        /// <param name="orderDocID">订单ID</param>
        /// <returns></returns>
        public BaseResultDataValue RS_UDTO_OrderDocSaveToOtherSystem(long id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BaseResultData baseResultData = new BaseResultData();

            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(employeeID))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "无法获取当前用户的ID信息";
                    ZhiFang.Common.Log.Log.Info(baseResultData.message);
                    return baseResultDataValue;
                }
                HREmployee emp = IBHREmployee.Get(long.Parse(employeeID));

                ReaBmsCenOrderDoc orderDoc = IBReaBmsCenOrderDoc.Get(id);
                if (orderDoc != null)
                {
                    if (orderDoc.IsThirdFlag.ToString() == ReaBmsOrderDocThirdFlag.同步成功.Key)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "订货单已同步其他系统平台";
                        return baseResultDataValue;
                    }
                    baseResultData = InterfaceCommon.GetThirdSaleDocType();
                    if (!baseResultData.success)
                    {
                        baseResultDataValue.success = baseResultData.success;
                        baseResultDataValue.ErrorInfo = baseResultData.message;
                        return baseResultDataValue;
                    }

                    if (string.IsNullOrEmpty(baseResultData.data))
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "获取不到第三方接口配置信息！";
                        return baseResultDataValue;
                    }

                    #region 接口实现
                    string userType = baseResultData.data.ToLower();
                    ZhiFang.Common.Log.Log.Info("接口订货单同步开始-" + userType);
                    IList<ReaBmsCenOrderDtl> orderDtlList = IBReaBmsCenOrderDtl.SearchListByHQL(" reabmscenorderdtl.OrderDocID=" + id);
                    if (orderDtlList == null || orderDtlList.Count == 0)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "订货单同步其他系统平台失败：无法根据订货单ID" + id.ToString() + "获取订货单明细信息";
                        return baseResultDataValue;
                    }
                    if (string.IsNullOrEmpty(userType))
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "未实现当前用户的订单上传接口功能!";
                        return baseResultDataValue;
                    }

                    if (userType == "bjwjyy_his") //北京望京医院--东华物资
                    {
                        InterfaceWangJingHIS wangjingHIS = new InterfaceWangJingHIS();
                        Service_HIS_WangJing.WebLisServiceSoapClient soapClient = new Service_HIS_WangJing.WebLisServiceSoapClient();
                        string strXML = wangjingHIS.ReaBmsCenOrderDocToXMLWangJingHIS(orderDoc, orderDtlList);
                        if (string.IsNullOrEmpty(strXML))
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "获取订货单XML格式信息失败！";
                            return baseResultDataValue;
                        }
                        ZhiFang.Common.Log.Log.Info("订货单上传XML字符串：" + strXML);
                        string strResult = soapClient.PHTranOut(strXML);
                        ZhiFang.Common.Log.Log.Info("订货单上传接口返回值：" + strResult);
                        baseResultData = wangjingHIS.GetInterfaceResult(strResult);
                        string isThirdFlag = baseResultData.success ? ReaBmsOrderDocThirdFlag.同步成功.Key : ReaBmsOrderDocThirdFlag.同步失败.Key;
                        IBReaBmsCenOrderDoc.EditReaBmsCenOrderDocThirdFlag(id, int.Parse(isThirdFlag));
                    }
                    else if (userType == "yzrmyy_donghua") //鄞州人民医院--东华物资
                    {
                        InterfaceYZRMYYDongHua yzrmyyDongHua = new InterfaceYZRMYYDongHua();
                        string strXML = yzrmyyDongHua.ReaBmsCenOrderDocToXMLYZRMYYDongHua(orderDoc, orderDtlList, emp);
                        if (string.IsNullOrEmpty(strXML))
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "获取订货单XML格式信息失败！";
                            return baseResultDataValue;
                        }
                        ZhiFang.Common.Log.Log.Info("订货单上传XML字符串：" + strXML);
                        string otherOrderDocID = "";
                        string otherOrderDocNo = "";
                        baseResultData = yzrmyyDongHua.ReaBmsCenOrderDocPost(strXML, ref otherOrderDocID, ref otherOrderDocNo);
                        string isThirdFlag = baseResultData.success ? ReaBmsOrderDocThirdFlag.同步成功.Key : ReaBmsOrderDocThirdFlag.同步失败.Key;
                        IBReaBmsCenOrderDoc.EditReaBmsCenOrderDocThirdFlag(id, int.Parse(isThirdFlag), otherOrderDocID, otherOrderDocNo);
                    }
                    else if (userType == "gnyxfsdyyy_hrp")//赣南医学附属第一医院--上海京颐HRP
                    {
                        //获取当前部门ID，其所有父级部门，然后固定取第二级
                        HRDept secondDept = IBHRDept.GetSecondDept(emp.HRDept.Id);
                        if (secondDept == null)
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "订单上传失败，当前人员所属部门错误！";
                            return baseResultDataValue;
                        }

                        if (orderDoc.ReaCompID == null)
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "订单上传失败，供应商ID为空！";
                            return baseResultDataValue;
                        }

                        InterfaceGanNanFSDYYY ganNanFSDYYY = new InterfaceGanNanFSDYYY();
                        string sendXml = ganNanFSDYYY.GetDoSynMtrlPurchasePlanInfo(orderDoc, orderDtlList, secondDept);
                        baseResultData = ganNanFSDYYY.CallService("DoSynMtrlPurchasePlanInfo", sendXml);

                        string isThirdFlag = baseResultData.success ? ReaBmsOrderDocThirdFlag.同步成功.Key : ReaBmsOrderDocThirdFlag.同步失败.Key;
                        IBReaBmsCenOrderDoc.EditReaBmsCenOrderDocThirdFlagAndStatus(id, int.Parse(isThirdFlag), int.Parse(ReaBmsOrderDocStatus.订单上传.Key));
                    }
                    ZhiFang.Common.Log.Log.Info("接口订货单同步结束-" + userType);
                    #endregion
                }
                else
                {
                    baseResultData.success = false;
                    baseResultData.message = "订货单同步其他系统平台失败：无法根据订货单ID" + id.ToString() + "获取订货单信息";
                    ZhiFang.Common.Log.Log.Info(baseResultData.message);
                }
            }
            catch (Exception ex)
            {
                baseResultData.success = false;
                baseResultData.message = "接口订货单同步失败：" + ex.Message;
                ZhiFang.Common.Log.Log.Info(baseResultData.message);
            }
            baseResultDataValue.success = baseResultData.success;
            baseResultDataValue.ErrorInfo = baseResultData.message;
            return baseResultDataValue;
        }

        #endregion

        #region ReaMonthUsageStatisticsDtl
        public BaseResultDataValue RS_UDTO_SearchReaMonthUsageStatisticsDtlByAllJoinHql(int page, int limit, string fields, string where, string reaGoodsHql, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaMonthUsageStatisticsDtl> tempEntityList = new EntityList<ReaMonthUsageStatisticsDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                tempEntityList = IBReaMonthUsageStatisticsDtl.SearchReaMonthUsageStatisticsEntityListDtlByAllJoinHql(where, reaGoodsHql, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaMonthUsageStatisticsDtl>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region ReaGoodsBarcodeOperation
        public BaseResultDataValue RS_UDTO_SearchOverReaGoodsBarcodeOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsBarcodeOperation> tempEntityList = new EntityList<ReaGoodsBarcodeOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                else
                {
                    sort = "";
                }
                tempEntityList = IBReaGoodsBarcodeOperation.SearchOverReaGoodsBarcodeOperationByHQL(where, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaGoodsBarcodeOperation>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region 库存标志处理
        public BaseResultDataValue RS_UDTO_SearchReaBmsQtyDtlEntityListOfQtyMarkByGroupType(string storageId, int groupType, int page, int limit, string fields, string where, string deptGoodsHql, string reaGoodsHql, string sort, bool isPlanish, bool isEmpPermission)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(storageId))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "传入的库房信息为空!";
            }
            EntityList<ReaBmsQtyDtl> entityList = new EntityList<ReaBmsQtyDtl>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                entityList = IBReaBmsQtyDtl.SearchReaBmsQtyDtlEntityListOfQtyMarkByGroupType(storageId, groupType, where, deptGoodsHql, reaGoodsHql, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaBmsQtyDtl>(entityList);
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

        public BaseResultBool RS_UDTO_UpdateReaBmsQtyDtlByQtyDtlMark(QtyMarkVO entity)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "未指定更新信息!";
                return tempBaseResultBool;
            }
            if (!entity.StorageID.HasValue)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "未指定库房信息!";
                return tempBaseResultBool;
            }
            if (entity.ReaGoodNoList == null || entity.ReaGoodNoList.Count <= 0)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "未指定库存货品信息!";
                return tempBaseResultBool;
            }
            try
            {
                tempBaseResultBool = IBReaBmsQtyDtl.UpdateReaBmsQtyDtlByQtyDtlMark(entity);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        #endregion

        #region 双表查询定制服务
        public BaseResultDataValue RS_UDTO_SearchReaGoodsByHQL(int page, int limit, string fields, string where, string linkWhere, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoods> entityList = new EntityList<ReaGoods>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }

                entityList = IBReaDeptGoods.SearchReaGoodsListByHQL(page, limit, where, linkWhere, sort);

                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoods>(entityList);
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
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchReaGoodsByHQL.Error:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }

        #endregion

<<<<<<< .mine
        #region ReaOpenBottleOperDoc
        //Add  ReaOpenBottleOperDoc
        public BaseResultDataValue ST_UDTO_AddReaOpenBottleOperDoc(ReaOpenBottleOperDoc entity)
        {
            IBReaOpenBottleOperDoc.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBReaOpenBottleOperDoc.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBReaOpenBottleOperDoc.Get(IBReaOpenBottleOperDoc.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBReaOpenBottleOperDoc.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  ReaOpenBottleOperDoc
        public BaseResultBool ST_UDTO_UpdateReaOpenBottleOperDoc(ReaOpenBottleOperDoc entity)
        {
            IBReaOpenBottleOperDoc.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaOpenBottleOperDoc.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ReaOpenBottleOperDoc
        public BaseResultBool ST_UDTO_UpdateReaOpenBottleOperDocByField(ReaOpenBottleOperDoc entity, string fields)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                return tempBaseResultBool;
            }
            string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

            if (entity.IsObsolete == true)
            {
                entity.ObsoleteID = long.Parse(employeeID);
                entity.ObsoleteName = employeeName;
                if (!entity.ObsoleteTime.HasValue) entity.ObsoleteTime = DateTime.Now;
                if (!fields.Contains("ObsoleteID")) fields = fields + ",ObsoleteID";
                if (!fields.Contains("ObsoleteName")) fields = fields + ",ObsoleteName";
                if (!fields.Contains("ObsoleteTime")) fields = fields + ",ObsoleteTime";
            }
            if (entity.IsUseCompleteFlag == true)
            {
                if (!entity.UseCompleteDate.HasValue) entity.UseCompleteDate = DateTime.Now;
                if (!fields.Contains("UseCompleteDate")) fields = fields + ",UseCompleteDate";
            }
            IBReaOpenBottleOperDoc.Entity = entity;

            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBReaOpenBottleOperDoc.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBReaOpenBottleOperDoc.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBReaOpenBottleOperDoc.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  ReaOpenBottleOperDoc
        public BaseResultBool ST_UDTO_DelReaOpenBottleOperDoc(long longReaOpenBottleOperDocID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBReaOpenBottleOperDoc.Remove(longReaOpenBottleOperDocID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchReaOpenBottleOperDoc(ReaOpenBottleOperDoc entity)
        {
            IBReaOpenBottleOperDoc.Entity = entity;
            EntityList<ReaOpenBottleOperDoc> tempEntityList = new EntityList<ReaOpenBottleOperDoc>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBReaOpenBottleOperDoc.Search();
                tempEntityList.count = IBReaOpenBottleOperDoc.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaOpenBottleOperDoc>(tempEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaOpenBottleOperDocByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ReaOpenBottleOperDoc> tempEntityList = new EntityList<ReaOpenBottleOperDoc>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBReaOpenBottleOperDoc.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBReaOpenBottleOperDoc.SearchListByHQL(where, page, limit);
                   
                }
                if (fields.Contains("ReaBmsQtyDtl") || fields.Contains("ReaGoods"))
                {
                    for (int i = 0; i < tempEntityList.list.Count; i++)
                    {
                        if (fields.Contains("ReaBmsQtyDtl") && tempEntityList.list[i].QtyDtlID.HasValue)
                            tempEntityList.list[i].ReaBmsQtyDtl = IBReaBmsQtyDtl.Get(tempEntityList.list[i].QtyDtlID.Value);
                        if (fields.Contains("ReaGoods") && tempEntityList.list[i].GoodsID.HasValue)
                            tempEntityList.list[i].ReaGoods = IBReaGoods.Get(tempEntityList.list[i].GoodsID.Value);
                    }
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ReaOpenBottleOperDoc>(tempEntityList);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchReaOpenBottleOperDocById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBReaOpenBottleOperDoc.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaOpenBottleOperDoc>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_GetOBottleOperDocByOutDtlId(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (employeeID == "-1" || String.IsNullOrEmpty(employeeID))
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "获取登录帐号信息失败,请重新登录后再操作!";
                    return tempBaseResultDataValue;
                }
                string employeeName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);

                var tempEntity = IBReaOpenBottleOperDoc.GetOBottleOperDocByOutDtlId(id, long.Parse(employeeID), employeeName);

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ReaOpenBottleOperDoc>(tempEntity);
                    }
                    else
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntity, fields);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("ST_UDTO_GetOBottleOperDocByOutDtlId.Error:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }

        #endregion

||||||| .r2673
=======
        #region 四川大家试剂投屏需求

        /// <summary>
        /// 四川大家投屏查询服务
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue RS_UDTO_SearchReaGoodsQtyWarningInfo(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    sort = CommonServiceMethod.GetSortHQL(sort);
                }
                string warningPromptInfo = "";
                EntityList<ReaGoodsStockWarning> entityList = IBReaBmsQtyDtl.SearchReaGoodsStockWarningList(page, limit, where, sort, ref warningPromptInfo);
                
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsStockWarning>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }

                    baseResultDataValue.DataCode = warningPromptInfo;
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("RS_UDTO_SearchReaGoodsQtyWarningInfo.Error:" + ex.StackTrace);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 货品表结合库存表，查询二级分类并返回
        /// </summary>
        public BaseResultDataValue RS_UDTO_SearchGoodsClassTypeJoinQtyDtl(bool isPlanish, string fields, string where, string sort, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ReaGoodsClassVO> entityList = new EntityList<ReaGoodsClassVO>();
            try
            {
                entityList = IBReaGoods.SearchGoodsClassTypeJoinQtyDtl(where, sort, page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ReaGoodsClassVO>(entityList);
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
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        #endregion

>>>>>>> .r2783
    }
}
