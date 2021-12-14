using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Request;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.Entity.LIIP;
using ZhiFang.IBLL.LabStar;
using ZhiFang.LabStar.Common;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    //检验前业务系统服务：业务表基础服务及其相关的定制服务
    public class LabStarPreService : ILabStarPreService
    {
        #region 引用

        ZhiFang.IBLL.LabStar.IBLBSamplingGroup IBLBSamplingGroup { get; set; }

        ZhiFang.IBLL.LabStar.IBLBSamplingItem IBLBSamplingItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBReportDate IBLBReportDate { get; set; }

        ZhiFang.IBLL.LabStar.IBLBReportDateItem IBLBReportDateItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBReportDateRule IBLBReportDateRule { get; set; }

        ZhiFang.IBLL.LabStar.IBLisCommon IBLisCommon { get; set; }

        ZhiFang.IBLL.LabStar.IBLisOrderForm IBLisOrderForm { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItem IBLBItem { get; set; }
        ZhiFang.IBLL.LabStar.IBLisBarCodeForm IBLisBarCodeForm { get; set; }
        ZhiFang.IBLL.LabStar.IBLisBarCodeItem IBLisBarCodeItem { get; set; }
        IBLisPatient IBLisPatient { get; set; }
        IBBParaItem IBBParaItem { get; set; }
        IBBPara IBBPara { get; set; }
        IBBHostType IBBHostType { get; set; }
        IBLBOrderModel IBLBOrderModel { get; set; }
        IBLBOrderModelItem IBLBOrderModelItem { get; set; }
        IBBHostTypeUser IBBHostTypeUser { get; set; }
        IBLBDicCodeLink IBLBDicCodeLink { get; set; }
        IBLBTranRule IBLBTranRule { get; set; }
        IBLBTranRuleItem IBLBTranRuleItem { get; set; }
        IBLBTranRuleHostSection IBLBTranRuleHostSection { get; set; }
        #endregion

        #region
        public BaseResultDataValue LS_UDTO_AddDelLBSamplingItem(IList<LBSamplingItem> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                Dictionary<string, object> propertyList = new Dictionary<string, object> { { "DataUpdateTime", DateTime.Now } };
                baseResultDataValue = IBLisCommon.AddCommonBaseRelationEntity(IBLBSamplingItem, addEntityList, isCheckEntityExist, false, propertyList, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除采样组项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_AddDelLBReportDateItem(IList<LBReportDateItem> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                Dictionary<string, object> propertyList = new Dictionary<string, object> { { "DataUpdateTime", DateTime.Now } };
                baseResultDataValue = IBLisCommon.AddCommonBaseRelationEntity(IBLBReportDateItem, addEntityList, isCheckEntityExist, false, propertyList, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除取单分类项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_QueryLBSamplingGroupByHQL(string where, string fields, string sort, int page, int limit, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSamplingGroup> entityList = new EntityList<LBSamplingGroup>();
            try
            {
                entityList = IBLBSamplingGroup.QueryLBSamplingGroupByFetch(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSamplingGroup>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryLBSamplingItemByHQL(string where, string fields, string sort, int page, int limit, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSamplingItem> entityList = new EntityList<LBSamplingItem>();
            try
            {
                entityList = IBLBSamplingItem.QueryLBSamplingItemByFetch(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSamplingItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_QuerySamplingGroupIsMultiItem(string where, string fields, bool isMulti, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSamplingGroup> entityList = new EntityList<LBSamplingGroup>();
            try
            {
                entityList = IBLBSamplingItem.QuerySamplingGroupIsMultiItem(where, isMulti);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSamplingGroup>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryItemIsMultiSamplingGroup(string where, string strSectionID, string fields, bool isMulti, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            try
            {
                entityList = IBLBSamplingItem.QueryItemIsMultiSamplingGroup(where, strSectionID, isMulti);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryItemNoSamplingGroup(string where, string strSectionID, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            try
            {
                entityList = IBLBSamplingItem.QueryItemNoSamplingGroup(where, strSectionID);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItem>(entityList);
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
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_QueryLBReportDateItemByHQL(string where, string fields, string sort, int page, int limit, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBReportDateItem> entityList = new EntityList<LBReportDateItem>();
            try
            {
                entityList = IBLBReportDateItem.QueryLBReportDateItemByFetch(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBReportDateItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryLBReportDateRuleByHQL(string where, string fields, string sort, int page, int limit, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBReportDateRule> entityList = new EntityList<LBReportDateRule>();
            try
            {
                entityList = IBLBReportDateRule.QueryLBReportDateRuleByFetch(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBReportDateRule>(entityList);
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

        public BaseResultDataValue LS_UDTO_DeleteLBReportDateByID(long id)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBReportDate.DeleteLBReportDateByID(id);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }
        #endregion

        #region 医嘱开单

        /// <summary>
        /// 保存医嘱单
        /// </summary>
        /// <param name="LisPatient"></param>
        /// <param name="LisOrderForm"></param>
        /// <param name="LisOrderItems"></param>
        /// <returns></returns>
        public BaseResultDataValue AddOrder(LisPatient LisPatient, LisOrderForm LisOrderForm, IList<LisOrderItem> LisOrderItems)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (LisPatient == null || LisOrderForm == null || LisOrderItems == null || LisOrderItems.Count == 0) {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "请检查病人信息和项目信息必填项是否填写完整！";
                    return baseResultDataValue;
                }
                if (LisPatient.SickTypeID == 0 || LisPatient.SickTypeID == null) {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "请选择就诊类型！";
                    return baseResultDataValue;
                }
                IBLisOrderForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                //当前登录人ID与当前登录人姓名
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                baseResultDataValue = IBLisOrderForm.Edit_AddOrder(LisPatient, LisOrderForm, LisOrderItems, System.Uri.UnescapeDataString(username), userid);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.AddOrder异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 医嘱开单修改
        /// </summary>
        /// <param name="LisPatient">病人信息</param>
        /// <param name="lisPatientFields">病人信息需要修改的字段</param>
        /// <param name="LisOrderForm">医嘱单信息</param>
        /// <param name="lisOrderFormFields">医嘱信息需要修改的字段</param>
        /// <param name="LisOrderItems">医嘱项目需要修改信息</param>
        /// <returns></returns>
        public BaseResult EditOrder(LisPatient LisPatient, string lisPatientFields, LisOrderForm LisOrderForm, string lisOrderFormFields, IList<LisOrderItem> LisOrderItems)
        {
            BaseResult br = new BaseResult();
            try
            {
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                LisOrderForm entity = IBLisOrderForm.Get(LisOrderForm.Id);
                if (entity.IsAffirm == 1)
                {
                    br.success = false;
                    br.ErrorInfo = "审核过的医嘱单不允许修改！";
                    return br;
                }
                LisPatient.DataUpdateTime = DateTime.Now;
                LisOrderForm.DataUpdateTime = DateTime.Now;

                string[] lisPatientFieldsarry = null;
                string[] lisOrderFormFieldsarry = null;

                if (!string.IsNullOrEmpty(lisPatientFields))
                {
                    lisPatientFields = lisPatientFields + ",DataUpdateTime,DataTimeStamp";
                    LisPatient.DataTimeStamp = IBLisPatient.Get(LisPatient.Id).DataTimeStamp;
                    lisPatientFieldsarry = CommonServiceMethod.GetUpdateFieldValueStr(LisPatient, lisPatientFields);
                }

                if (!string.IsNullOrEmpty(lisOrderFormFields))
                {
                    lisOrderFormFields = lisOrderFormFields + ",DataUpdateTime,DataTimeStamp";
                    LisOrderForm.DataTimeStamp = IBLisOrderForm.Get(LisOrderForm.Id).DataTimeStamp;
                    lisOrderFormFieldsarry = CommonServiceMethod.GetUpdateFieldValueStr(LisOrderForm, lisOrderFormFields);
                }
                IBLisOrderForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                br.success = IBLisOrderForm.EditOrder(lisPatientFieldsarry, lisOrderFormFieldsarry, LisOrderItems, LisOrderForm, userid, System.Uri.UnescapeDataString(username));
            }
            catch (Exception ex)
            {
                br.success = false;
                br.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.EditOrder异常：" + ex.ToString());
            }


            return br;
        }
        /// <summary>
        /// 医嘱开单 获得项目树
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue GetItemModelTree()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<tree> baseResultTree = IBLBItem.GetItemModelTree();
                if (baseResultTree.Count > 0)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(baseResultTree);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    }
                }

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.GetItemModelTree异常：" +ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 医嘱单列表
        /// </summary>
        /// <param name="hisDeptNo"></param>
        /// <param name="patno"></param>
        /// <param name="sickTypeNo"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public BaseResultDataValue GetOrderList(string hisDeptNo, string patno, string sickTypeNo, string strWhere)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisOrderForm.GetOrderList(hisDeptNo, patno, sickTypeNo, strWhere);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.GetOrderList异常："+ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 医嘱单审核
        /// </summary>
        /// <param name="orderFormNo"></param>
        /// <returns></returns>
        public BaseResultDataValue UpdateOrder(string orderFormNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                IBLisOrderForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisOrderForm.UpdateOrder(orderFormNo,long.Parse(userid), System.Uri.UnescapeDataString(username));
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.UpdateOrder异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue CancelOrder(string orderFormNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                IBLisOrderForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisOrderForm.CancelOrder(orderFormNo,long.Parse(userid),Uri.UnescapeDataString(username));
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.CancelOrder 异常：" +ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue DeleteOrder(string orderFormNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                IBLisOrderForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisOrderForm.DeleteOrder(orderFormNo, long.Parse(userid), Uri.UnescapeDataString(username));
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.DeleteOrder异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue GetLIIPHREmployeeAndHRDept(string CName, string DicType, string TSysCode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string labid = ZhiFang.Common.Public.Cookie.Get("000100");
                List<HREmpIdentity> hREmps = new List<HREmpIdentity>();
                if (DicType == ContrastDicType.人员.Key)
                {
                    if (TSysCode == "1")
                    {
                        string where = "";
                        if (string.IsNullOrEmpty(CName))
                        {
                            where = $"IsUse = 1 and LabID = {labid}";
                        }
                        else
                        {
                            where = $"IsUse = 1 and LabID = {labid} and CName like '%{CName}%'";
                        }
                        IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHREmployeeByHQL(where).toList<HREmpIdentity>();
                        hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;
                    }
                    else
                    {
                        string where = "";
                        if (string.IsNullOrEmpty(CName))
                        {
                            where = $"hrempidentity.TSysCode='{TSysCode}' and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.LabID = {labid}";
                        }
                        else
                        {
                            where = $"hremployee.CName like '%{CName}%' and hrempidentity.TSysCode='{TSysCode}' and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.LabID = {labid}";
                        }
                        IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHREmpIdentityByHQL(where).toList<HREmpIdentity>();
                        hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;
                    }
                }
                else if (DicType == ContrastDicType.部门.Key)
                {
                    if (TSysCode == "1")
                    {
                        string where = "";
                        if (string.IsNullOrEmpty(CName))
                        {
                            where = $"IsUse = 1 and LabID = {labid}";
                        }
                        else
                        {
                            where = $"IsUse = 1 and LabID = {labid} and CName like '%{CName}%'";
                        }
                        IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHRDeptByHQL(where).toList<HREmpIdentity>();
                        hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;
                    }
                    else
                    {
                        string where = "";
                        if (string.IsNullOrEmpty(CName))
                        {
                            where = $"hrempidentity.TSysCode='{TSysCode}' and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.LabID = {labid}";
                        }
                        else
                        {
                            where = $"hrdept.CName like '%{CName}%'  and  hrempidentity.TSysCode='{TSysCode}' and hrempidentity.SystemCode='ZF_LAB_START' and hrempidentity.LabID = {labid}";
                        }
                        IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHRDeptIdentityByHQL(where).toList<HREmpIdentity>();
                        hREmps = hREmpIdentities != null ? hREmpIdentities.ToList() : null;
                    }
                }
                if (hREmps != null && hREmps.Count() > 0)
                {
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(hREmps);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = "";
                }
                baseResultDataValue.success = true;
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.GetLIIPHREmployeeAndHRDept异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        #endregion

        #region 采样组项目定制服务
        public BaseResultDataValue SearchLBSamplingItemBandItemNameList(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBSamplingItemVO> entityList = new EntityList<LBSamplingItemVO>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBSamplingItem.SearchLBSamplingItemBandItemName(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBSamplingItem.SearchLBSamplingItemBandItemName(where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBSamplingItemVO>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                    ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.SearchLBSamplingItemBandItemNameList异常：" + ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.SearchLBSamplingItemBandItemNameList异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_SearchLBItemByLBSamplingItem(long SamplingGroupID, long SectionID, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            try
            {

                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItem.LS_UDTO_SearchLBItemBySamplingGroupID(SamplingGroupID, SectionID, page, limit, where, CommonServiceMethod.GetSortHQL(sort));
                }
                else
                {
                    entityList = IBLBItem.LS_UDTO_SearchLBItemBySamplingGroupID(SamplingGroupID, SectionID, page, limit, where, "");
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItem>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                    ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_SearchLBItemBySamplingGroupID异常：" + ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_SearchLBItemBySamplingGroupID异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_SearchLBItemByReportDateID(long ReportDateID, long SectionID, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBItem.LS_UDTO_SearchLBItemByReportDateID(ReportDateID, SectionID, page, limit, where, CommonServiceMethod.GetSortHQL(sort));
                }
                else
                {
                    entityList = IBLBItem.LS_UDTO_SearchLBItemByReportDateID(ReportDateID, SectionID, page, limit, where, "");
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItem>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                    ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_SearchLBItemByReportDateID异常：" + ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_SearchLBItemByReportDateID异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_UpdateSamplingItemIsDefault(long? Id, long? ItemId, bool IsDefault)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBSamplingItem.LS_UDTO_UpdateSamplingItemIsDefault(Id, ItemId, IsDefault);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_UpdateSamplingItemIsDefault异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        #endregion

        #region 前处理参数设置定制服务
        public BaseResultDataValue LS_UDTO_QueryParaSystemTypeInfoAndAddDefultPara(string systemTypeCode, string paraTypeCode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<BPara> bParas = IBBPara.SearchListByHQL("TypeCode='" + paraTypeCode + "'");
                IList<BPara> paras = IBBPara.QueryFactoryParaListByParaClassName(paraTypeCode);
                if (bParas.Count != paras.Count)
                {
                    for (int i = 0; i < paras.Count; i++)
                    {
                        var siglepara = bParas.Where(a => a.ParaNo == paras[i].ParaNo);
                        if (siglepara.Count() > 0)
                        {
                            paras[i] = siglepara.First();
                        }
                    }
                    string userid = ZhiFang.Common.Public.Cookie.Get(Entity.RBAC.DicCookieSession.EmployeeID);
                    string username = ZhiFang.Common.Public.Cookie.Get(Entity.RBAC.DicCookieSession.EmployeeName);
                    IBBPara.AddAndEditPara(paras, userid, username);
                }
                List<object> list = new List<object>();
                List<string> defult = new List<string>();
                defult.Add(paraTypeCode);
                defult.Add(" * 默认参数");
                list.Add(defult);
                var paraitemlist = IBBParaItem.QueryParaSystemTypeInfo(systemTypeCode, paraTypeCode);
                if (paraitemlist.Count > 0)
                {
                    list.AddRange(paraitemlist.ToList());
                }
                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(list);

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_QueryParaSystemTypeInfoAndAddDefultPara异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_UpdateParSystemParaItem(string ObjectID, List<BParaItem> entityList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string userid = ZhiFang.Common.Public.Cookie.Get(Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(Entity.RBAC.DicCookieSession.EmployeeName);
                baseResultDataValue = IBBParaItem.PreEditParSystemParaItem(ObjectID, entityList, userid, username);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_UpdateParSystemParaItem异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 查询字段选择枚举
        /// </summary>
        /// <param name="paraTypeCode">参数字典类名</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_QueryOrderBarCodeSelectFields(string paraTypeCode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<PreParaEnumTypeEntity> List = IBBPara.QueryOrderBarCodeSelectFieldsByClassName(paraTypeCode);
                baseResultDataValue.success = true;
                if (List.Count > 0)
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(List);
                else
                    baseResultDataValue.ResultDataValue = "";
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_QueryOrderBarCodeSelectFields异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 根据类型查询不同的数据源
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_GetParaDicData(string type)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if(string.IsNullOrEmpty(type)){
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "参数不可为空！";
                return baseResultDataValue;
            }
            baseResultDataValue.success = true;
            try
            {
                EntityList<LBDicCodeLinkVO> entityList = new EntityList<LBDicCodeLinkVO>();
                entityList = IBLBDicCodeLink.GetParaDicData(type);
                if (entityList.count > 0)
                {
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(entityList.list);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = "";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_GetParaDicData异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue LS_UDTO_GetParParaAndParaItem(long nodetype, string paranos, string typecode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(typecode))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "参数模块类型不可为空！";
                return baseResultDataValue;
            }
            baseResultDataValue.success = true;
            try
            {
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                List<BPara> list = new List<BPara>();
                list = IBBParaItem.SelPreBParas(nodetype, paranos, typecode,userid,username);
                if (list.Count > 0)
                {
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(list);
                }
                else
                {
                    baseResultDataValue.ResultDataValue = "";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_GetParParaAndParaItem异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_DeleteSystemParaItem(string objectInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string empID = SessionHelper.GetSessionValue(Entity.RBAC.DicCookieSession.EmployeeID);
                string empName = SessionHelper.GetSessionValue(Entity.RBAC.DicCookieSession.EmployeeID);
                if (objectInfo != null && objectInfo.Trim().Length > 0)
                    baseResultDataValue = IBBParaItem.DeleteParaItemByObjectIDAndHostTypeUser(objectInfo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_DeleteSystemParaItem异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }


        #endregion

        #region 样本条码
        /// <summary>
        /// 从HIS获取医嘱并采样分管
        /// </summary>
        /// <param name="nodetype">站点类型</param>
        /// <param name="receiveType">核收条件</param>
        /// <param name="value">核收条件对应的值</param>
        /// <param name="days">样本过滤天数</param>
        /// <param name="nextindex">测试使用，下一个编号</param>
        /// <returns>分管列表</returns>
        public BaseResultDataValue LS_UDTO_PreHISGetSamplingGrouping(long nodetype, string receiveType, string value, int days, string fields, bool isPlanish, int nextindex)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
                IBLisOrderForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                List<BPara> paralist = new List<BPara>();
                List<List<GroupingOrderItemVo>> goivlist = new List<List<GroupingOrderItemVo>>();
                List<List<LisOrderFormItemVo>> lisoftvolist = new List<List<LisOrderFormItemVo>>();
                List<LBSamplingItem> lbslilist = new List<LBSamplingItem>();
                List<LisBarCodeFormVo> barCodeFormVos = new List<LisBarCodeFormVo>();
                var baseResult = IBLisOrderForm.GetHISCheckData(nodetype, receiveType, value, days, long.Parse(userid), System.Uri.UnescapeDataString(username), labid, nextindex);
                if (!baseResult.success) {
                    return baseResult;
                }
                baseResultDataValue = IBLisOrderForm.HISGetSampleAndGrouping(nodetype, receiveType, value, days,long.Parse(userid),System.Uri.UnescapeDataString(username), labid, nextindex,out paralist, out goivlist,out lisoftvolist, out lbslilist,out barCodeFormVos);
                if (baseResultDataValue.success)
                {
                    if (goivlist.Count > 0) {
                        List<LisBarCodeFormVo> lisBarCodeFormVos = IBLisOrderForm.AddBarCodeFormAndEditOrderForm(paralist, goivlist, lisoftvolist, lbslilist, long.Parse(userid),Uri.UnescapeDataString(username), nextindex);
                        if (barCodeFormVos == null)
                            barCodeFormVos = lisBarCodeFormVos;
                        else 
                            barCodeFormVos.AddRange(lisBarCodeFormVos);
                        IBLisOrderForm.Edit_OrderFormExecFlag(lisoftvolist);//查询主单状态是否更新
                    }
                    #region 数据压平
                    EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                    if (barCodeFormVos != null) {
                        entityList.count = barCodeFormVos.Count;
                        entityList.list = barCodeFormVos;
                    }
                    ParseObjectProperty pop = new ParseObjectProperty(fields);
                    try
                    {
                        baseResultDataValue.success = true;
                        if (isPlanish)
                        {
                            baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                        }
                        else
                        {
                            baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                        }
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                        ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreHISGetSamplingGrouping异常：" + ex.ToString());
                    }
                    
                }                
                #endregion
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreHISGetSamplingGrouping异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 从LIS获取医嘱并采样分管
        /// </summary>
        /// <param name="nodetype">站点类型</param>
        /// <param name="receiveType">核收条件</param>
        /// <param name="value">核收条件对应的值</param>
        /// <param name="days">样本过滤天数</param>
        /// <param name="nextindex">测试使用，下一个编号</param>
        /// <returns>分管列表</returns>
        public BaseResultDataValue LS_UDTO_PreLISGETSamplingGrouping(long nodetype, string receiveType, string value, int days, string fields, bool isPlanish, int nextindex)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                //当前登录人ID与当前登录人姓名
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
                List<BPara> paralist = new List<BPara>();
                List<List<GroupingOrderItemVo>> goivlist = new List<List<GroupingOrderItemVo>>();
                List<List<LisOrderFormItemVo>> lisoftvolist = new List<List<LisOrderFormItemVo>>();
                List<LBSamplingItem> lbslilist = new List<LBSamplingItem>();
                List<LisBarCodeFormVo> barCodeFormVos = new List<LisBarCodeFormVo>();
                baseResultDataValue = IBLisOrderForm.LISGetSampleAndGrouping(nodetype, receiveType, value, days, userid,Uri.UnescapeDataString(username), labid, nextindex, out paralist, out goivlist, out lisoftvolist, out lbslilist, out barCodeFormVos);
                if (baseResultDataValue.success)
                {
                    if (goivlist.Count > 0)
                    {
                        List<LisBarCodeFormVo> lisBarCodeFormVos = IBLisOrderForm.AddBarCodeFormAndEditOrderForm(paralist, goivlist, lisoftvolist, lbslilist,long.Parse(userid), Uri.UnescapeDataString(username), nextindex);
                        if (barCodeFormVos == null)
                            barCodeFormVos = lisBarCodeFormVos;
                        else
                            barCodeFormVos.AddRange(lisBarCodeFormVos);
                        IBLisOrderForm.Edit_OrderFormExecFlag(lisoftvolist);//查询主单状态是否更新
                    }
                    #region 数据压平
                    EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                    if (barCodeFormVos != null)
                    {
                        entityList.count = barCodeFormVos.Count;
                        entityList.list = barCodeFormVos;
                    }
                    ParseObjectProperty pop = new ParseObjectProperty(fields);
                    try
                    {
                        baseResultDataValue.success = true;
                        if (isPlanish)
                        {
                            baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                        }
                        else
                        {
                            baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                        }
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                        ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreLISGETSamplingGrouping异常：" + ex.ToString());
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreLISGETSamplingGrouping异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 重新分组（根据原有条件将未指定采样组的项目重新分组）
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="receiveType"></param>
        /// <param name="value"></param>
        /// <param name="days"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <param name="nextindex"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreANewSamplingGrouping(string barcode, long nodetype, string receiveType, string value, int days, string fields, bool isPlanish, int nextindex)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                //当前登录人ID与当前登录人姓名
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
                baseResultDataValue = IBLisOrderForm.GetSampleAndANewGrouping(barcode,nodetype, receiveType, value, days,userid,Uri.UnescapeDataString(username), fields, labid, isPlanish, nextindex);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreANewSamplingGrouping异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 已打印条码数据查询
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="receiveType"></param>
        /// <param name="value"></param>
        /// <param name="days"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_GetHaveToPrintBarCodeForm(string barcode, string where, bool? printStatus, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisOrderForm.GetHaveToPrintBarCodeForm(barcode,where, printStatus,fields,isPlanish); 
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_GetHaveToPrintBarCodeForm异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 条码确认
        /// </summary>
        /// <param name="nodetype">站点类型</param>
        /// <param name="barcodes">条码号(多个,分割)</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreBarCodeAffirm(long nodetype, string barcodes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisBarCodeForm.Edit_PreBarCodeAffirm(nodetype, barcodes,long.Parse(userid),Uri.UnescapeDataString(username));
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreBarCodeAffirm异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 条码作废
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreBarCodeInvalid(long nodetype, string barcodes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisBarCodeForm.Edit_PreBarCodeInvalid(nodetype, barcodes,false,userid,Uri.UnescapeDataString(username));
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreBarCodeInvalid异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 验卡服务
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_VerifyCardNo(long nodetype, string receiveType, string cardno)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisOrderForm.CheckCard(nodetype, receiveType, cardno);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_VerifyCardNo异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// HIS医嘱信息
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcode"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_HISOrderInfo(long nodetype, string barcode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisOrderForm.GetHISOrderInfo(nodetype, barcode);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_HISOrderInfo异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 取单凭证
        /// </summary>
        /// <param name="nodetype">站点类型</param>
        /// <param name="barcode">条码号，多个使用逗号隔开</param>
        /// <param name="isloadtable">是否加载取单时间表</param>
        /// <param name="isupdatebcitems">是否更新样本单取单时间描述字段</param>
        /// <param name="modelcode">模板编码</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreBarCodeGatherVoucher(long nodetype, string barcode, bool? isloadtable, bool? isupdatebcitems, string modelcode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcode))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不允许为空！";
                    return baseResultDataValue;
                }
                baseResultDataValue = IBLisBarCodeForm.GetBarCodeSamppleGatherVoucherData(nodetype, barcode, isloadtable, isupdatebcitems, modelcode);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreBarCodeGatherVoucher异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 条码打印数量获取
        /// </summary>
        /// <param name="where"></param>
        /// <param name="printStatus"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_GetPrintBarCodeCount(long nodetype, string barcodes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不允许为空！";
                    return baseResultDataValue;
                }
                //当前登录人ID与当前登录人姓名
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                baseResultDataValue = IBLisBarCodeForm.GetPrintBarCodeCount(nodetype, barcodes,userid,Uri.UnescapeDataString(username));
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_GetPrintBarCodeCount异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 打印状态更新
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_UpdateBarCodeFromPrintStatus(long nodetype, string barcodes, string IsAffirmBarCode)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不允许为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisBarCodeForm.Edit_BarCodeFromPrintStatus(nodetype, barcodes, IsAffirmBarCode, userid,Uri.UnescapeDataString(username));
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_UpdateBarCodeFromPrintStatus异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 采集排队信息
        /// </summary>
        /// <param name="nodetype">站点类型</param>
        /// <param name="barcode">条码号</param>
        /// <param name="patientType">叫号系统病人类型</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_CreatEqueuingMachineInfo(long nodetype, string barcode, string patientType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcode))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不允许为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisBarCodeForm.Add_EqueuingMachineInfo(nodetype, barcode, patientType, userid, Uri.UnescapeDataString(username));
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_UpdateBarCodeFromPrintStatus异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 验证预制条码并保存预制条码号
        /// </summary>
        /// <param name="nodetype">站点类型</param>
        /// <param name="barcode">条码号</param>
        /// <param name="barcodeformid">样本单ID</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_UpdateLisBarCodeFormBarCodeByBarCodeFormID(long nodetype, string barcode, string barcodeformid)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {                
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                baseResultDataValue = IBLisBarCodeForm.Edit_BarCodeFormBarCodeByBarCodeFormID(nodetype, barcode, barcodeformid, userid, Uri.UnescapeDataString(username));
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_UpdateBarCodeFromPrintStatus异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 样本确认_撤销确认数据获取与校验
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleAffirmDataVerifyByBarCode(long nodetype, string barcodes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不可为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                baseResultDataValue = IBLisBarCodeForm.GetSampleAffirmDataAndVerifyByBarCode(nodetype, barcodes, userid, username);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampleAffirmDataVerifyByBarCode：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 根据条码撤销确认
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleRevocationAffirmByBarCode(long nodetype, string barcodes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不可为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisBarCodeForm.EditBarCodeFormRevocationAffirm(nodetype, barcodes, userid, username);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampleRevocationAffirmByBarCode：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region 样本采集
        /// <summary>
        /// 根据条码号获取数据并更新状态
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="isupdate"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleGatherAndUpdateStateByBarCode(long nodetype, string barcodes, bool? isupdate, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不可为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
                bool funGetIsUpdate = true;
                lisBarCodeFormVos = IBLisBarCodeForm.GetSampleGatherWantDataByBarCode(nodetype, barcodes, userid, username, out funGetIsUpdate);
                if ((isupdate == null || isupdate == true) && funGetIsUpdate && lisBarCodeFormVos != null && lisBarCodeFormVos.Count >0)
                {
                    lisBarCodeFormVos = IBLisBarCodeForm.EditBarCodeFormCollect(nodetype, barcodes, lisBarCodeFormVos, userid, username);
                }
                #region 数据压平
                EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                if (lisBarCodeFormVos != null && lisBarCodeFormVos.Count > 0)
                {
                    entityList.count = lisBarCodeFormVos.Count;
                    entityList.list = lisBarCodeFormVos;
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    baseResultDataValue.success = true;
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                    ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampleGatherAndUpdateStateByBarCode异常：" + ex.ToString());
                }
                #endregion
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampleGatherAndUpdateStateByBarCode异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_PreUpdateSampleGatherStateByBarCode(long nodetype, string barcodes, bool? isConstraintUpdate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不可为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                lisBarCodeFormVos = IBLisBarCodeForm.EditBarCodeFormCollect(nodetype, barcodes, lisBarCodeFormVos, userid, username,isConstraintUpdate);
                EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                if (lisBarCodeFormVos != null && lisBarCodeFormVos.Count > 0)
                {
                    entityList.count = lisBarCodeFormVos.Count;
                    entityList.list = lisBarCodeFormVos;
                }
                baseResultDataValue.success = true;
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(entityList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreUpdateSampleGatherStateByBarCode异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_PreGetSampleGatherFormListByWhere(long nodetype, string where, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(where))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条件不可为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
                lisBarCodeFormVos = IBLisBarCodeForm.GetSampleGatherWantDataByWhere(nodetype, where,userid, username);
                #region 数据压平
                EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                if (lisBarCodeFormVos != null && lisBarCodeFormVos.Count > 0)
                {
                    entityList.count = lisBarCodeFormVos.Count;
                    entityList.list = lisBarCodeFormVos;
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    baseResultDataValue.success = true;
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                    ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreGetSampleGatherFormListByWhere异常：" + ex.ToString());
                }
                #endregion
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreGetSampleGatherFormListByWhere异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
       
        /// <summary>
        /// 根据核收条件从HIS核收数据并写入LIS库
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="receiveType"></param>
        /// <param name="value"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleGatherGetBarCodeFromByCheckWhere(long nodetype, string receiveType, string value, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(receiveType) || string.IsNullOrEmpty(value))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条件不可为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
                lisBarCodeFormVos = IBLisBarCodeForm.GetBarCodeFromByCheckWhereAndAddBarCodeForm(nodetype, receiveType, value, userid, username);
                #region 数据压平
                EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                if (lisBarCodeFormVos != null && lisBarCodeFormVos.Count > 0)
                {
                    entityList.count = lisBarCodeFormVos.Count;
                    entityList.list = lisBarCodeFormVos;
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    baseResultDataValue.success = true;
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                    ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampleGatherGetBarCodeFromByCheckWhere异常：" + ex.ToString());
                }
                #endregion
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampleGatherGetBarCodeFromByCheckWhere异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 撤销采集
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleRevocationGatherByBarCode(long nodetype, string barcodes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不可为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisBarCodeForm.EditBarCodeFormRevocationCollect(nodetype, barcodes, userid, username);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampleRevocationGatherByBarCode异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 撤销采集数据获取并校验
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleRevocationGatherDataVerifyByBarCode(long nodetype, string barcodes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不可为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                baseResultDataValue = IBLisBarCodeForm.GetSampleGatherDataAndVerifyByBarCode(nodetype, barcodes, userid, username);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampleRevocationGatherDataVerifyByBarCode：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 打包号生成
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleGatherCreateCollectPackNoByBarCode(long nodetype, string barcodes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不可为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                baseResultDataValue = IBLisBarCodeForm.Edit_SampleGatherCreateCollectPackNoByBarCode(nodetype, barcodes, userid, username);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampleRevocationGatherDataVerifyByBarCode：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        #endregion

        #region 样本送检
        /// <summary>
        /// 条码号获取数据并更新送检状态
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="isupdate"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleExchangeInspectAndUpdateStateByBarCode(long nodetype, string barcodes, bool? isupdate, string userid, string username, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不可为空！";
                    return baseResultDataValue;
                }
                string localuserid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string localusername = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
                bool funGetIsUpdate = true;
                lisBarCodeFormVos = IBLisBarCodeForm.GetSampleExchangeInspectWantDataByBarCode(nodetype, barcodes, localuserid, localusername, out funGetIsUpdate);
                if ((isupdate == null || isupdate == true) && funGetIsUpdate)
                {
                    lisBarCodeFormVos = IBLisBarCodeForm.EditExchangeInspectBarCodeForm(nodetype, barcodes, lisBarCodeFormVos, localuserid, Uri.UnescapeDataString(localusername), userid,username, labid, funGetIsUpdate);
                }
                #region 数据压平
                EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                if (lisBarCodeFormVos != null && lisBarCodeFormVos.Count > 0)
                {
                    entityList.count = lisBarCodeFormVos.Count;
                    entityList.list = lisBarCodeFormVos;
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    baseResultDataValue.success = true;
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                    ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampleGatherAndUpdateStateByBarCode异常：" + ex.ToString());
                }
                #endregion
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampleGatherAndUpdateStateByBarCode异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 更新送检标记
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <param name="isConstraintUpdate"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreUpdateSampleExchangeInspectStateByBarCode(long nodetype, string barcodes, string userid, string username, bool? isConstraintUpdate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不可为空！";
                    return baseResultDataValue;
                }
                string localuserid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string localusername = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                string labid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID);
                List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                lisBarCodeFormVos = IBLisBarCodeForm.EditExchangeInspectBarCodeForm(nodetype, barcodes, lisBarCodeFormVos, localuserid, localusername, userid, username, labid,isConstraintUpdate);
                EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                if (lisBarCodeFormVos != null && lisBarCodeFormVos.Count > 0)
                {
                    entityList.count = lisBarCodeFormVos.Count;
                    entityList.list = lisBarCodeFormVos;
                }
                baseResultDataValue.success = true;
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(entityList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreUpdateSampleExchangeInspectStateByBarCode异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 撤销送检
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleRevocationExchangeInspectByBarCode(long nodetype, string barcodes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不可为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisBarCodeForm.EditBarCodeFormRevocationExchangeInspect(nodetype, barcodes, userid, username);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampleRevocationGatherByBarCode异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 获取撤销送检数据并校验
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="barcodes"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleRevocationExchangeInspectDataVerifyByBarCode(long nodetype, string barcodes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不可为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                baseResultDataValue = IBLisBarCodeForm.GetSampleExchangeInspectDataAndVerifyByBarCode(nodetype, barcodes, userid, username);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampleRevocationGatherDataVerifyByBarCode：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 根据where条件查询样本数据
        /// </summary>
        /// <param name="nodetype"></param>
        /// <param name="where"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreGetSampleExchangeInspectFormListByWhere(long nodetype, string where, string fields, bool isPlanish, string relationForm)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(where))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条件不可为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                List<LisBarCodeFormVo> lisBarCodeFormVos = new List<LisBarCodeFormVo>();
                lisBarCodeFormVos = IBLisBarCodeForm.GetSampleExchangeInspectWantDataByWhere(nodetype, where, userid, username, relationForm);
                #region 数据压平
                EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                if (lisBarCodeFormVos != null && lisBarCodeFormVos.Count > 0)
                {
                    entityList.count = lisBarCodeFormVos.Count;
                    entityList.list = lisBarCodeFormVos;
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    baseResultDataValue.success = true;
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                    ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreGetSampleGatherFormListByWhere异常：" + ex.ToString());
                }
                #endregion
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreGetSampleGatherFormListByWhere异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }


        #endregion

        #region 样本送达
        public BaseResultDataValue LS_UDTO_PreSampledeliveryGetBarCodeForm(long nodetype, string barcodes, bool isUpdate, string userid, string username, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barcodes) || string.IsNullOrEmpty(barcodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不可为空！";
                    return baseResultDataValue;
                }
                string loginuserid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string loginusername = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                baseResultDataValue = IBLisBarCodeForm.GetSampledeliveryBarCodeFormListAndEditBarCodeForm(nodetype, barcodes,  userid, username,loginuserid,loginusername, fields,isPlanish, isUpdate);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampledeliveryGetBarCodeForm异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue LS_UDTO_PreSampledeliveryGetEmpInfo(string cardno)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            try
            {
                if (string.IsNullOrEmpty(cardno))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "人员工号不允许为空！";
                    return baseResultDataValue;
                }
                string where = " hrempidentity.SystemCode in('ZF_LAB_START','ZF_PRE') and hrempidentity.TSysCode in('"+ EmpSystemType .护士.Key+"','"+ EmpSystemType .护工.Key+ "')";
                where += " and hrempidentity.HREmployee.StandCode = '"+ cardno+"'";
                IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHREmpIdentityByHQL(where).toList<HREmpIdentity>();
                if (hREmpIdentities != null && hREmpIdentities.Count > 0)
                {
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(hREmpIdentities.ToList());
                }
                else 
                {
                    baseResultDataValue.ResultDataValue = "";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampledeliveryGetEmpInfo异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue LS_UDTO_PreSampledeliveryUpdateBarCodeFormArrive(long nodetype, string barcodes, string userid, string username)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string loginuserid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string loginusername = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisBarCodeForm.EditBarCodeFormArrive(nodetype, barcodes, userid, username,loginuserid,loginusername);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_PreSampledeliveryUpdateBarCodeFormArrive异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region 样本签收
        /// <summary>
        /// 通过条件查询样本列表--查询按钮
        /// </summary>
        /// <param name="nodetypeID">站点id</param>
        /// <param name="fields">查询字段</param>
        /// <param name="where">查询条件</param>
        /// <param name="isPlanish">是否压平</param>
        /// <param name="sortFields">排序字段</param>
        /// <param name="relationForm">查询要关联的表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleSignForGetBarCodeFromListByWhere(long nodetypeID, string fields, string where, bool isPlanish, string sortFields, string relationForm)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_PreSampleSignForGetBarCodeFromListByWhere.入参：" + nodetypeID + ";" + fields + ";" + where + ";" + isPlanish+";"+sortFields+";"+relationForm);
                if (string.IsNullOrEmpty(where))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询条件不能为空！";
                }
                else
                {
                    //当前登录人ID与当前登录人姓名
                    string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                    string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                    username = Uri.UnescapeDataString(username);
                    EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                    var barCodeFormVos = IBLisBarCodeForm.GetSignForBarCodeFormListByWhere(nodetypeID, where, userid, username, CommonServiceMethod.GetSortHQL(sortFields), relationForm);
                    if (barCodeFormVos != null && barCodeFormVos.Count > 0)
                    {
                        entityList.count = barCodeFormVos.Count;
                        entityList.list = barCodeFormVos;
                        ParseObjectProperty pop = new ParseObjectProperty(fields);
                        try
                        {
                            baseResultDataValue.success = true;
                            if (isPlanish)
                            {
                                baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                            }
                            else
                            {
                                baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                            }
                        }
                        catch (Exception ex)
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "序列化错误";
                            ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleSignForGetBarCodeFromListByWhere" + ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleSignForGetBarCodeFromListByWhere" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 通过条码号执行具体的签收操作
        /// </summary>
        /// <param name="nodetypeID">站点id</param>
        /// <param name="barCodes">条码号</param>
        /// <param name="fields">查询字段</param>
        /// <param name="sickType">就诊类型</param>
        /// <param name="deliverier">送达人</param>
        /// <param name="deliverierID">送达人ID</param>
        /// <param name="isPlanish">是否压平</param>
        /// <param name="isAutoSignFor">自动签收</param>
        /// <param name="isForceSignFor">强制签收</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSignForSampleByBarCode(long nodetypeID, string barCodes, string fields, string sickType, string deliverier, string deliverierID, bool isPlanish, bool isAutoSignFor, bool isForceSignFor)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.success = true;
                if (string.IsNullOrEmpty(barCodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不能为空！";
                }
                else
                {
                    //当前登录人ID与当前登录人姓名
                    string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                    string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                    username = Uri.UnescapeDataString(username);
                    IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                    baseResultDataValue = IBLisBarCodeForm.EditSignForSampleByBarCode(nodetypeID, barCodes, sickType, deliverier, deliverierID, fields, isPlanish, isAutoSignFor, isForceSignFor, userid, username, "OrderSignFor");
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_SignForSampleByBarCode" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 通过条码号获取样本信息
        /// </summary>
        /// <param name="nodetypeID">站点ID</param>
        /// <param name="barCode">条码号</param>
        /// <param name="sickType">就诊类型</param>
        /// <param name="fields">查询字段</param>
        /// <param name="isPlanish">是否压平</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleSignForGetBarCodeFormByBarCode(long nodetypeID, string barCode, string sickType, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_PreSampleSignForGetBarCodeFormByBarCode.入参：" + nodetypeID + ";" + fields + ";" + barCode + ";" + isPlanish);
                if (string.IsNullOrEmpty(barCode))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不能为空！";
                }
                else
                {
                    EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                    var barCodeFormVos = IBLisBarCodeForm.GetSignForBarCodeFormListByBarCode(barCode, sickType);
                    if (barCodeFormVos != null && barCodeFormVos.Count > 0)
                    {
                        entityList.count = barCodeFormVos.Count;
                        entityList.list = barCodeFormVos;
                        ParseObjectProperty pop = new ParseObjectProperty(fields);
                        try
                        {
                            baseResultDataValue.success = true;
                            if (isPlanish)
                            {
                                baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                            }
                            else
                            {
                                baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                            }
                        }
                        catch (Exception ex)
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "序列化错误";
                            ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleSignForGetBarCodeFormByBarCode" + ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleSignForGetBarCodeFormByBarCode" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 根据打包号签收或获取数据
        /// </summary>
        /// <param name="nodetypeID">站点id</param>
        /// <param name="packNo">打包号或条码号</param>
        /// <param name="sickType">就诊类型</param>
        /// <param name="deliverier">送达人</param>
        /// <param name="deliverierID">送达人ID</param>
        /// <param name="fields">查询字段</param>
        /// <param name="isPlanish">是否压平</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleSignForOrGetBarCodeFormByPackNo(long nodetypeID, string packNo, string sickType, string deliverier, string deliverierID, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(packNo))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo="请传入条码号或打包号";
                    return baseResultDataValue;
                }
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_PreSampleSignForOrGetBarCodeFormByPackNo.条件：" + nodetypeID + ";" + packNo + ";" + sickType + ";" + deliverier + ";" + fields + ";" + isPlanish + ";");
                //当前登录人ID与当前登录人姓名
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                username = Uri.UnescapeDataString(username);
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                //获取参数配置
                List<BPara> bParas = IBBParaItem.SelPreBParas(nodetypeID, "", Pre_AllModules.样本签收.Value.DefaultValue, userid, username);
                var IsUseCollectPackNo = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0017").First();//获取是否通过打包号签收参数,1(0)|P 0:不使用，1:使用 P:打包号标识符
                var UseCollectPackNoAutoSignFor = bParas.Where(w => w.ParaNo == "Pre_OrderSignFor_DefaultPara_0018").First();//打包号自动还是手动
                //判断是否打包号
                string isUsePackNoFlag = "";//用打包号签收标志
                string PackNoIdentifier = "-1";//打包号标识符
                if (!string.IsNullOrWhiteSpace(IsUseCollectPackNo.ParaValue))
                {
                    string[] isUsePackNo = IsUseCollectPackNo.ParaValue.Split('|');
                    if (isUsePackNo.Length == 2)
                    {
                        isUsePackNoFlag = isUsePackNo[0];
                        PackNoIdentifier = isUsePackNo[1];
                    }
                }
                List<LisBarCodeFormVo> barCodeFormVos = new List<LisBarCodeFormVo>();
                if (isUsePackNoFlag=="0")//不通过打包号方式
                {
                    EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                    barCodeFormVos = IBLisBarCodeForm.GetSignForBarCodeFormListByBarCode(packNo, sickType);
                }
                else
                {
                    if (packNo.IndexOf(PackNoIdentifier) == 0 && UseCollectPackNoAutoSignFor.ParaValue == "1")//是打包号并且自动签收模式
                    {
                        baseResultDataValue = IBLisBarCodeForm.EditSampleSignForByPackNo(nodetypeID, packNo, sickType, deliverier, deliverierID, fields, isPlanish, bParas, userid, username);
                    }
                    else
                    {
                        //通过条码号或打包号获取所在打包号所有样本列表
                        barCodeFormVos = IBLisBarCodeForm.GetSignForBarCodeFormListByBarCodeOrPackNo(nodetypeID, packNo, sickType, userid, username);
                    }
                }
                if (barCodeFormVos != null && barCodeFormVos.Count > 0)
                {
                    EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                    entityList.count = barCodeFormVos.Count;
                    entityList.list = barCodeFormVos;
                    ParseObjectProperty pop = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
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
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleSignForOrGetBarCodeFormByPackNo" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 取消签收
        /// </summary>
        /// <param name="nodetypeID">站点ID</param>
        /// <param name="BarCodeList">条码号</param>
        /// <param name="fields">查询字段</param>
        /// <param name="isPlanish">是否压平</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreCancelSampleSignForOrByBarCode(long nodetypeID, string BarCodeList, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                //当前登录人ID与当前登录人姓名
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                username = Uri.UnescapeDataString(username);
                baseResultDataValue = IBLisBarCodeForm.EditCancelSampleSignForOrByBarCode(nodetypeID, BarCodeList, fields, isPlanish, userid, username);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_CancelSampleSignForOrByBarCode" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 通过条码号获取所在打包号所有样本信息
        /// </summary>
        /// <param name="nodetypeID">站点ID</param>
        /// <param name="barCode">条码号</param>
        /// <param name="sickType">就诊类型</param>
        /// <param name="fields">查询字段</param>
        /// <param name="isPlanish">是否压平</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleSignForGetPackNoRelationBarCodeFormListByBarCode(long nodetypeID, string barCode, string sickType, string fields, bool isPlanish) {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                
                if (string.IsNullOrEmpty(barCode))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "请传入条码号!";
                    return baseResultDataValue;
                }
                //当前登录人ID与当前登录人姓名
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                username = Uri.UnescapeDataString(username);
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_PreSampleSignForGetPackNoRelationBarCodeFormListByBarCode.条件：" + nodetypeID + ";" + barCode + ";" + sickType + ";"  + ";" + fields + ";" + isPlanish + ";");
                //通过条码号或打包号获取所在打包号所有样本列表
                var barCodeFormVos = IBLisBarCodeForm.GetSignForBarCodeFormListByBarCodeOrPackNo(nodetypeID, barCode, sickType, userid, username);
                if (barCodeFormVos != null && barCodeFormVos.Count > 0)
                {
                    EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                    entityList.count = barCodeFormVos.Count;
                    entityList.list = barCodeFormVos;
                    ParseObjectProperty pop = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
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
                    }
                }
            }
            catch (Exception ex)
            {

                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleSignForGetPackNoRelationBarCodeFormListByBarCode" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 通过条码号获取和配置参数条件获取需要打印取单凭证的样本信息
        /// </summary>
        /// <param name="nodetypeID">站点ID</param>
        /// <param name="barCodes">条码号</param>
        /// <param name="modelcode">模板code</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleSignForGetNeedPrintVoucherBarCodeFormListByBarCodeAndPara(long nodetypeID, string barCodes,string modelcode) {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(barCodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "请传入条码号!";
                    return baseResultDataValue;
                }
                //当前登录人ID与当前登录人姓名
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                username = Uri.UnescapeDataString(username);
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_PreSampleSignForGetNeedPrintVoucherBarCodeFormListByBarCodeAndPara.条件：" + nodetypeID + ";" + barCodes + ";"  + modelcode + ";");
                baseResultDataValue = IBLisBarCodeForm.GetNeedPrintVoucherBarCodeFormListByBarCodeAndPara(nodetypeID, barCodes, modelcode, userid, username);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleSignForGetPackNoRelationBarCodeFormListByBarCode" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 通过专业大组ID获取所有小组下的所有项目ID
        /// </summary>
        /// <param name="nodetypeID">站点ID</param>
        /// <param name="superSectionID">专业大组ID</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleSignForGetAllItemIdBySuperSectionID(string superSectionID) {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(superSectionID))
            {
                brdv.success = false;
                brdv.ErrorInfo = "入参大组ID不能为空！";
                return brdv;
            }
            try
            {
                brdv = IBLisBarCodeForm.GetAllItemIdBySuperSectionID(superSectionID);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleSignForGetAllItemIdBySuperSectionID" + ex.ToString());
            }
            return brdv;
        }

        /// <summary>
        /// 通过小组ID获取所有小组下的所有项目ID
        /// </summary>
        /// <param name="nodetypeID">站点ID</param>
        /// <param name="sectionID">小组ID</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleSignForGetAllItemIdBySectionID(string sectionID)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            if (string.IsNullOrEmpty(sectionID))
            {
                brdv.success = false;
                brdv.ErrorInfo = "入参小组ID不能为空！";
                return brdv;
            }
            try
            {
                brdv = IBLisBarCodeForm.GetAllItemIdBySectionID(sectionID);
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleSignForGetAllItemIdBySectionID" + ex.ToString());
            }
            return brdv;
        }
        
        #endregion

        #region 样本拒收
        /// <summary>
        /// 根据查询条件获取样本列表
        /// </summary>
        /// <param name="nodetypeID">站点ID</param>
        /// <param name="where">查询条件</param>
        /// <param name="fields">查询字段</param>
        /// <param name="isPlanish">是否压平</param>
        /// <param name="sickTypeId">就诊类型Id</param>
        /// <param name="sickTypeName">就诊类型名称</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreRefuseAcceptGetSampleListByWhere(long nodetypeID, string where, string fields, bool isPlanish, string sickTypeId, string sickTypeName)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (string.IsNullOrEmpty(where))
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询条件不能为空";
            }
            try
            {
                //当前登录人ID与当前登录人姓名
                baseResultDataValue = IBLisBarCodeForm.GetSampleListByWhereRefuseAccept( where, fields, isPlanish, sickTypeId,sickTypeName);
            }
            catch (Exception ex)
            {

                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_GetSampleListByWhereRefuseAccept" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 通过条码号进行样本拒收
        /// </summary>
        /// <param name="nodetypeID">站点ID</param>
        /// <param name="barcodes">条码号</param>
        /// <param name="refuseReason">拒收原因</param>
        /// <param name="handleAdvice">处理意见</param>
        /// <param name="answerPeople">接听人</param>
        /// <param name="phoneNum">电话</param>
        /// <param name="refuseRemark">拒收备注</param>
        /// <param name="fields">查询字段</param>
        /// <param name="isPlanish">是否压平</param>
        /// <param name="isForceReject">是否强制签收</param>
        /// <param name="sickTypeId">就诊类型ID</param>
        /// <param name="sickTyepName">就诊类型名称</param>
        /// <param name="refuseID">拒收原因ID</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreRefuseAcceptEditSample(long nodetypeID, string barcodes, string refuseReason, string handleAdvice, string answerPeople, string phoneNum, string refuseRemark, string fields, bool isPlanish, bool isForceReject, string sickTypeId, string sickTyepName, long refuseID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                //当前登录人ID与当前登录人姓名
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                username = Uri.UnescapeDataString(username);
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisBarCodeForm.EditRefuseAcceptSample(nodetypeID, barcodes, refuseReason, handleAdvice, answerPeople, phoneNum, refuseRemark, fields, isPlanish, isForceReject, userid,username, refuseID);

            }
            catch (Exception ex)
            {

                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_EditRefuseAcceptSample" + ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region 样本分发
        /// <summary>
        /// 样本分发_通过条件查询样本列表--查询按钮
        /// </summary>
        /// <param name="nodetypeID">站点id</param>
        /// <param name="fields">查询字段</param>
        /// <param name="where">查询条件</param>
        /// <param name="isPlanish">是否压平</param>
        /// <param name="sortFields">排序字段</param>
        /// <param name="relationForm">查询要关联的表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleDispenseGetBarCodeFromListByWhere(long nodetypeID, string fields, string where, bool isPlanish, string sortFields, string relationForm)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_PreSampleTranGetBarCodeFromListByWhere.入参：" + nodetypeID  + ";" + where + ";" + isPlanish + ";" + sortFields + ";" + relationForm);
                if (string.IsNullOrEmpty(where))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询条件不能为空！";
                }
                else
                {
                    //当前登录人ID与当前登录人姓名
                    string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                    string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                    username = Uri.UnescapeDataString(username);
                    EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                    var barCodeFormVos = IBLisBarCodeForm.GetDispenseBarCodeFormListByWhere(nodetypeID, where, userid, username, CommonServiceMethod.GetSortHQL(sortFields), relationForm);
                    if (barCodeFormVos != null && barCodeFormVos.Count > 0)
                    {
                        entityList.count = barCodeFormVos.Count;
                        entityList.list = barCodeFormVos;
                        ParseObjectProperty pop = new ParseObjectProperty(fields);
                        try
                        {
                            baseResultDataValue.success = true;
                            if (isPlanish)
                            {
                                baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                            }
                            else
                            {
                                baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                            }
                        }
                        catch (Exception ex)
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "序列化错误";
                            ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleTranGetBarCodeFromListByWhere" + ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleSignForGetBarCodeFromListByWhere" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 根据样本单id获取所包含项目的分发信息列表
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="fields"></param>
        /// <param name="barcodeFormId"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleDispenseGetBarCodeItemDispenseInfoListByFormId(long nodetypeID, string fields, long barcodeFormId, bool isPlanish) {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_PreSampleDispenseGetBarCodeItemDispenseInfoListByFormId.入参：" + nodetypeID + ";" + fields + ";" + barcodeFormId + ";" + isPlanish + ";");
                if (barcodeFormId<=0)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "样本单Id不能为空！";
                }
                else
                {
                    EntityList<LisBarCodeItemVoResp> entityList = new EntityList<LisBarCodeItemVoResp>();
                    var barCodeItemVos = IBLisBarCodeForm.GetBarCodeItemDispenseInfoListByFormId(barcodeFormId);
                    if (barCodeItemVos != null && barCodeItemVos.Count > 0)
                    {
                        entityList.count = barCodeItemVos.Count;
                        entityList.list = barCodeItemVos;
                        ParseObjectProperty pop = new ParseObjectProperty(fields);
                        try
                        {
                            baseResultDataValue.success = true;
                            if (isPlanish)
                            {
                                baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeItemVoResp>(entityList);
                            }
                            else
                            {
                                baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                            }
                        }
                        catch (Exception ex)
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "序列化错误";
                            ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleDispenseGetBarCodeItemDispenseInfoListByFormId" + ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleDispenseGetBarCodeItemDispenseInfoListByFormId" + ex.ToString());
            }
            return baseResultDataValue;
        }
        
        /// <summary>
        /// 样本分发_根据条码号获取样本列表
        /// </summary>
        /// <param name="nodetypeID">站点ID</param>
        /// <param name="barCode">条码号</param>
        /// <param name="sickType">就诊类型</param>
        /// <param name="fields">要查询的字段</param>
        /// <param name="isPlanish">是否压平</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleDispenseGetBarCodeFormByBarCode(long nodetypeID, string barCode, string sickType, string fields, bool isPlanish) {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Debug("LS_UDTO_PreSampleDistributionGetBarCodeFormByBarCode.入参：" + nodetypeID + ";" + fields + ";" + barCode + ";" + isPlanish+";"+ sickType);
                if (string.IsNullOrEmpty(barCode))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不能为空！";
                }
                else
                {
                    EntityList<LisBarCodeFormVo> entityList = new EntityList<LisBarCodeFormVo>();
                    var barCodeFormVos = IBLisBarCodeForm.GetDispenseBarCodeFormListByBarCode(barCode, sickType);
                    if (barCodeFormVos != null && barCodeFormVos.Count > 0)
                    {
                        entityList.count = barCodeFormVos.Count;
                        entityList.list = barCodeFormVos;
                        ParseObjectProperty pop = new ParseObjectProperty(fields);
                        try
                        {
                            baseResultDataValue.success = true;
                            if (isPlanish)
                            {
                                baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeFormVo>(entityList);
                            }
                            else
                            {
                                baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                            }
                        }
                        catch (Exception ex)
                        {
                            baseResultDataValue.success = false;
                            baseResultDataValue.ErrorInfo = "序列化错误";
                            ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleDistributionGetBarCodeFormByBarCode" + ex.ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleDistributionGetBarCodeFormByBarCode" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 通过条码号签收
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="barCodes"></param>
        /// <param name="fields"></param>
        /// <param name="sickType"></param>
        /// <param name="deliverier"></param>
        /// <param name="deliverierID"></param>
        /// <param name="isPlanish"></param>
        /// <param name="isAutoSignFor"></param>
        /// <param name="isForceSignFor"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleDispenseSingnForBarCodeFormByBarCode(long nodetypeID, string barCodes, string fields, string sickType, string deliverier, string deliverierID, bool isPlanish, bool isAutoSignFor, bool isForceSignFor) {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.success = true;
                if (string.IsNullOrEmpty(barCodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不能为空！";
                }
                else
                {
                    //当前登录人ID与当前登录人姓名
                    string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                    string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                    username = Uri.UnescapeDataString(username);
                    IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                    baseResultDataValue = IBLisBarCodeForm.EditSignForSampleByBarCode(nodetypeID, barCodes, sickType, deliverier, deliverierID, fields, isPlanish, isAutoSignFor, isForceSignFor, userid, username, "OrderDispense");
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleDispenseSingnForBarCodeFormByBarCode" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 通过条码号分发
        /// </summary>
        /// <param name="nodetypeID">站点id</param>
        /// <param name="barCodes">条码号</param>
        /// <param name="fields">字段</param>
        /// <param name="sickType">就诊类型</param>
        /// <param name="isPlanish">是否压平</param>
        /// <param name="isForceDispense">强制分发</param>
        /// <param name="TestDate">检测日期</param>
        /// <param name="ruleType">规则类型</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleDispenseByBarCode(long nodetypeID, string barCodes, string fields, string sickType, bool isPlanish, bool isForceDispense, string TestDate, string ruleType) {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.success = true;
                if (string.IsNullOrEmpty(barCodes))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "条码号不能为空！";
                }
                else
                {
                    //当前登录人ID与当前登录人姓名
                    string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                    string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                    username = Uri.UnescapeDataString(username);
                    IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                    baseResultDataValue = IBLisBarCodeForm.EditDispenseSampleByBarCode(nodetypeID, barCodes, sickType, isForceDispense, userid, username, isPlanish, fields, TestDate, ruleType);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleDispenseByBarCode" + ex.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 分发取消
        /// </summary>
        /// <param name="nodetypeID"></param>
        /// <param name="barCodeFormIds"></param>
        /// <param name="fields"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PreSampleDispenseCancelByBarCodeFormId(long nodetypeID, string barCodeFormIds, string fields, bool isPlanish) {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                //当前登录人ID与当前登录人姓名
                //string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                //string username = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                //username = Uri.UnescapeDataString(username);
                IBLisBarCodeForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisBarCodeForm.EditSampleDispenseCancelByBarCodeFormId(nodetypeID, barCodeFormIds, fields, isPlanish);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleDispenseCancelByBarCodeFormId" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_PreSampleDispensePrintDispenseTagByBarCode(long nodetypeID, string barCodes) {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                
                baseResultDataValue = IBLisBarCodeForm.PrintDispenseTagByBarCode(nodetypeID, barCodes);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleDispensePrintDispenseTagByBarCode" + ex.ToString());
            }
            return baseResultDataValue;
        }
        public BaseResultDataValue LS_UDTO_PreSampleDispensePrintFlowSheetByBarCode(long nodetypeID, string barCodes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {

                baseResultDataValue = IBLisBarCodeForm.PrintDispenseTagByBarCode(nodetypeID, barCodes);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.LS_UDTO_PreSampleDispensePrintFlowSheetByBarCode" + ex.ToString());
            }
            return baseResultDataValue;
        }
        #endregion

        #region 其他定制服务
        /// <summary>
        /// 查询未使用过的站点类型
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_SearchBHostTypeNotPara(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHostType> entityList = new EntityList<BHostType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHostType.SearchBHostTypeNotPara( page, limit,where, CommonServiceMethod.GetSortHQL(sort));
                }
                else
                {
                    entityList = IBBHostType.SearchBHostTypeNotPara(page, limit, where,"");
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHostType>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                    ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_SearchBHostTypeNotPara异常：" + ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_SearchBHostTypeNotPara异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_BHostTypeUserCopy(long pasteuser, string Copyusers)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {              
                baseResultDataValue = IBBHostTypeUser.copyBHostTypeUserByEmpId(pasteuser, Copyusers);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_BHostTypeUserCopy异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue GetOrderModelTree(string OrderModelTypeID, string ItemWhere)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(OrderModelTypeID)) {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "医嘱模板类型不可为空！";
                    return baseResultDataValue;
                }
                List<tree> baseResultTree = IBLBOrderModel.GetOrderModelTree(OrderModelTypeID, ItemWhere);
                
                if (baseResultTree.Count > 0)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(baseResultTree);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                        ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.GetOrderModelTree异常：" + ex.ToString());
                    }
                }

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.GetOrderModelTree异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_GetLIIPHREmpIdentity(string where)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            try
            {
                if (string.IsNullOrEmpty(where))
                {
                    where = " hrempidentity.SystemCode in('ZF_LAB_START','ZF_PRE')";
                }
                else {

                    where += " and  hrempidentity.SystemCode in('ZF_LAB_START','ZF_PRE')";
                }
                IList<HREmpIdentity> hREmpIdentities = LIIPHelp.RBAC_UDTO_SearchHREmpIdentityByHQL(where).toList<HREmpIdentity>();
                if (hREmpIdentities != null && hREmpIdentities.Count > 0)
                {
                    List<HREmpIdentity> hREmps = new List<HREmpIdentity>();
                    var  groupemp = hREmpIdentities.GroupBy(a => a.Id);
                    foreach (var emps in groupemp)
                    {
                        HREmpIdentity entity = new HREmpIdentity();
                        entity.Id = emps.Key;
                        entity.CName = emps.First().CName;
                        entity.DeveCode = emps.First().DeveCode;
                        entity.LabID = emps.First().LabID;
                        entity.StandCode = emps.First().StandCode;
                        List<string> sysname = new List<string>();
                        emps.ToList().ForEach(f => sysname.Add(f.TSysName + "_" + f.SystemName));
                        entity.TSysName = string.Join(",", sysname);
                        hREmps.Add(entity);
                    }
                    baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(hREmps);                   
                }

            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_GetLIIPHREmpIdentity异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_SearchBHostTypeUserAndHostTypeNameByHQL(int page, int limit, string fields, string where, string systemTypeCode, string paraTypeCode, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<BHostTypeUser> entityList = new EntityList<BHostTypeUser>();
            try
            {
                if (string.IsNullOrEmpty(systemTypeCode) || string.IsNullOrEmpty(paraTypeCode))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "模块类型和系统相关性不可为空！";
                    return baseResultDataValue;
                }
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBBHostTypeUser.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBBHostTypeUser.SearchListByHQL(where, page, limit);
                }
                var paraitemlist = IBBParaItem.QueryParaSystemTypeInfo(systemTypeCode, paraTypeCode);
                if (paraitemlist.Count > 0 && entityList.count > 0)
                {
                    List<long> objids = new List<long>();
                    foreach (var formid in paraitemlist)
                    {
                        Array dataarr = (Array)formid;
                        long id = long.Parse(dataarr.GetValue(0).ToString());
                        if (!objids.Contains(id))
                            objids.Add(id);
                    }
                    List<long?> ids = new List<long?>();
                    List<BHostTypeUser> bHostTypeUsers = new List<BHostTypeUser>();
                    foreach (var bHostTypeUser in entityList.list)
                    {
                        //判断站点类型人员关系与系统参数设置中个性化设置是否存在
                        if (objids.Where(a=>a == bHostTypeUser.HostTypeID).Count() > 0) {
                            if (!ids.Contains(bHostTypeUser.HostTypeID))
                            { ids.Add(bHostTypeUser.HostTypeID); }
                            bHostTypeUsers.Add(bHostTypeUser);
                        }
                    }
                    if (ids.Count > 0)
                    {
                        IList<BHostType> bHostTypes = IBBHostType.SearchListByHQL("Id in (" + string.Join(",", ids) + ")");
                        foreach (var bHostTypeUser in bHostTypeUsers)
                        {
                            bHostTypeUser.HostTypeName = bHostTypes.Where(a => a.Id == bHostTypeUser.HostTypeID).First().CName;
                        }
                        entityList.list = bHostTypeUsers;
                        entityList.count = bHostTypeUsers.Count;
                    }
                    else 
                    {
                        entityList = new EntityList<BHostTypeUser>();
                    }
                   
                }
                else 
                {
                    entityList = new EntityList<BHostTypeUser>();
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<BHostTypeUser>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                    ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_SearchBHostTypeUserAndHostTypeNameByHQL异常：" + ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_SearchBHostTypeUserAndHostTypeNameByHQL异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_SearchLBTranRuleAndDicNameByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBTranRule> entityList = new EntityList<LBTranRule>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBTranRule.GetLBTranRuleList(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBTranRule.GetLBTranRuleList(where, "", page, limit);
                } 
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBTranRule>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                    ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_SearchLBTranRuleAndDicNameByHQL异常：" + ex.ToString());
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_SearchLBTranRuleAndDicNameByHQL异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_AddDelLBTranRuleItem(IList<LBTranRuleItem> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                Dictionary<string, object> propertyList = new Dictionary<string, object> { { "DataUpdateTime", DateTime.Now } };
                baseResultDataValue = IBLisCommon.AddCommonBaseRelationEntity(IBLBTranRuleItem, addEntityList, isCheckEntityExist, false, propertyList, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_AddDelLBTranRuleItem异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_AddDelLBTranRuleHostSection(IList<LBTranRuleHostSection> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                Dictionary<string, object> propertyList = new Dictionary<string, object> { { "DataUpdateTime", DateTime.Now } };
                baseResultDataValue = IBLisCommon.AddCommonBaseRelationEntity(IBLBTranRuleHostSection, addEntityList, isCheckEntityExist, false, propertyList, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_AddDelLBTranRuleHostSection异常：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 获取下一个样本号
        /// </summary>
        /// <param name="SampleNoSection"></param>
        /// <param name="SampleNoPrefix"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_GetLBTranRuleNextSampleNo(int? SampleNoSection, string SampleNoPrefix)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBTranRule.GetLBTranRuleNextSampleNo(SampleNoSection, SampleNoPrefix);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_GetLBTranRuleNextSampleNo：" + ex.ToString());
            }

            return baseResultDataValue;
        }
        /// <summary>
        /// 根据人员角色获取模块功能权限
        /// </summary>
        /// <param name="moduleid"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_GetModuleFunRoleByEmpId(string moduleid, string code)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            baseResultDataValue.success = true;
            try
            {
                #region
                if (string.IsNullOrEmpty(moduleid) || string.IsNullOrEmpty(code)) {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "模块ID或者模块功能编码为空！";
                    return baseResultDataValue;
                }
                string userid = ZhiFang.Common.Public.Cookie.Get(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                if (string.IsNullOrEmpty(userid)) {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "未找到登录人信息请重新登录！";
                    return baseResultDataValue;
                }
                var hRs = LIIPHelp.SearchRBACEmpRolesByHQL("HREmployee.Id="+ userid).toList<HREmpIdentity>();
                if (hRs != null && hRs.Count > 0) {
                    List<long> ids = new List<long>();
                    foreach (var hr in hRs)
                    {
                        if (!ids.Contains(hr.Id)) {
                            ids.Add(hr.Id);
                        }
                    }
                    string where = "rbacroleright.RBACModuleOper.RBACModule.Id=" + moduleid +
                        "and rbacroleright.RBACRole.Id in (" + string.Join(",", ids) + ")";
                    var  moduleroles = LIIPHelp.SearchRBACRoleRightByHQL(where).toList<HREmpIdentity>();
                    bool isok = false;
                    if (moduleroles != null && moduleroles.Count > 0) {
                        foreach (var modulerole in moduleroles)
                        {
                            if (code == modulerole.StandCode) {
                                isok = true;
                            }
                        }
                    }
                    if (isok)
                    {
                        baseResultDataValue.ResultDataValue = "1";
                    }
                    else {
                        baseResultDataValue.ResultDataValue = "0";
                    }
                }
                else {
                    baseResultDataValue.ResultDataValue = "0";
                }
                #endregion
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.LS_UDTO_GetModuleFunRoleByEmpId：" + ex.ToString());
            }

            return baseResultDataValue;
        }
        #endregion

        #region 获取web.config中程序地址
        /// <summary>
        /// 获取集成平台地址
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue GetConfigLIIPUrl()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.ResultDataValue = ZhiFang.LabStar.Common.ConfigHelper.GetConfigString("MsgPlatformServiceUrl");
                baseResultDataValue.success = true;
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.GetConfigLIIPUrl：" + ex.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 获取报告查询程序地址
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue GetConfigReportFormQueryPrintUrl()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue.ResultDataValue = ZhiFang.LabStar.Common.ConfigHelper.GetConfigString("ReportFormQueryPrintUrl");
                baseResultDataValue.success = true;
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "出现异常请查看日志！";
                ZhiFang.Common.Log.Log.Error("LabStarPreService.svc.GetConfigReportFormQueryPrintUrl：" + ex.ToString());
            }
            return baseResultDataValue;
        }


        #endregion
    }

}
