using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.OA;
using ZhiFang.Entity.OA.ViewObject.Response;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.OA;
using ZhiFang.ProjectProgressMonitorManage.BusinessObject;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.ProjectProgressMonitorManage
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WeiXinAppService : ServerContract.IWeiXinAppService
    {
        IBLL.OA.IBATApproveStatus IBATApproveStatus { get; set; }
        IBLL.OA.IBATAttendanceEventType IBATAttendanceEventType { get; set; }
        IBLL.OA.IBATEmpAttendanceEventLog IBATEmpAttendanceEventLog { get; set; }
        IBLL.OA.IBATEmpAttendanceEventParaSettings IBATEmpAttendanceEventParaSettings { get; set; }
        IBLL.OA.IBATTransportation IBATTransportation { get; set; }
        IBLL.OA.IBBAccountType IBBAccountType { get; set; }
        IBLL.OA.IBBIcons IBBIcons { get; set; }
        IBLL.OA.IBBWeiXinEmpLink IBBWeiXinEmpLink { get; set; }
        IBLL.OA.IBBWeiXinAccount IBBWeiXinAccount { get; set; }
        IBLL.OA.IBBWeiXinUserGroup IBBWeiXinUserGroup { get; set; }
        IBLL.OA.IBSServiceClient IBSServiceClient { get; set; }
        IBLL.OA.IBSServiceClientlevel IBSServiceClientlevel { get; set; }
        ZhiFang.IBLL.OA.IBBWeiXinPushMessageTemplate IBBWeiXinPushMessageTemplate { get; set; }
        ZhiFang.IBLL.OA.IBATHolidaySetting IBATHolidaySetting { get; set; }

        #region ATApproveStatus
        //Add  ATApproveStatus
        public BaseResultDataValue ST_UDTO_AddATApproveStatus(ATApproveStatus entity)
        {
            IBATApproveStatus.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBATApproveStatus.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBATApproveStatus.Get(IBATApproveStatus.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATApproveStatus.Entity);
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
        //Update  ATApproveStatus
        public BaseResultBool ST_UDTO_UpdateATApproveStatus(ATApproveStatus entity)
        {
            IBATApproveStatus.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBATApproveStatus.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ATApproveStatus
        public BaseResultBool ST_UDTO_UpdateATApproveStatusByField(ATApproveStatus entity, string fields)
        {
            IBATApproveStatus.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBATApproveStatus.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBATApproveStatus.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBATApproveStatus.Edit();
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
        //Delele  ATApproveStatus
        public BaseResultBool ST_UDTO_DelATApproveStatus(long longATApproveStatusID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBATApproveStatus.Remove(longATApproveStatusID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchATApproveStatus(ATApproveStatus entity)
        {
            IBATApproveStatus.Entity = entity;
            EntityList<ATApproveStatus> tempEntityList = new EntityList<ATApproveStatus>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBATApproveStatus.Search();
                tempEntityList.count = IBATApproveStatus.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ATApproveStatus>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchATApproveStatusByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ATApproveStatus> tempEntityList = new EntityList<ATApproveStatus>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBATApproveStatus.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBATApproveStatus.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ATApproveStatus>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchATApproveStatusById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBATApproveStatus.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ATApproveStatus>(tempEntity);
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
        #endregion

        #region ATAttendanceEventType
        //Add  ATAttendanceEventType
        public BaseResultDataValue ST_UDTO_AddATAttendanceEventType(ATAttendanceEventType entity)
        {
            IBATAttendanceEventType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBATAttendanceEventType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBATAttendanceEventType.Get(IBATAttendanceEventType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATAttendanceEventType.Entity);
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
        //Update  ATAttendanceEventType
        public BaseResultBool ST_UDTO_UpdateATAttendanceEventType(ATAttendanceEventType entity)
        {
            IBATAttendanceEventType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBATAttendanceEventType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ATAttendanceEventType
        public BaseResultBool ST_UDTO_UpdateATAttendanceEventTypeByField(ATAttendanceEventType entity, string fields)
        {
            IBATAttendanceEventType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBATAttendanceEventType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBATAttendanceEventType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBATAttendanceEventType.Edit();
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
        //Delele  ATAttendanceEventType
        public BaseResultBool ST_UDTO_DelATAttendanceEventType(long longATAttendanceEventTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBATAttendanceEventType.Remove(longATAttendanceEventTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchATAttendanceEventType(ATAttendanceEventType entity)
        {
            IBATAttendanceEventType.Entity = entity;
            EntityList<ATAttendanceEventType> tempEntityList = new EntityList<ATAttendanceEventType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBATAttendanceEventType.Search();
                tempEntityList.count = IBATAttendanceEventType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ATAttendanceEventType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchATAttendanceEventTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ATAttendanceEventType> tempEntityList = new EntityList<ATAttendanceEventType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBATAttendanceEventType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBATAttendanceEventType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ATAttendanceEventType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchATAttendanceEventTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBATAttendanceEventType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ATAttendanceEventType>(tempEntity);
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
        #endregion

        #region ATEmpAttendanceEventLog
        //Add  ATEmpAttendanceEventLog
        public BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventLog(ATEmpAttendanceEventLog entity)
        {
            //ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLog.LabID:"+ ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysDicCookieSession.LabID));
            //ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLog.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysDicCookieSession.IsLabFlag));
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBATEmpAttendanceEventLog.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventLog.Get(IBATEmpAttendanceEventLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLog.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventleaveevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventleaveevent.LabID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventleaveevent.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag));

            entity.StartDateTime = Convert.ToDateTime(StartDateTime);
            entity.EndDateTime = Convert.ToDateTime(EndDateTime);
            entity.ApplyID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
            entity.ApplyName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBATEmpAttendanceEventLog.AddATEmpAttendanceEventleaveevent((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, entity);
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventLog.Get(IBATEmpAttendanceEventLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLog.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventEgressevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventEgressevent.LabID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventEgressevent.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag));

            entity.StartDateTime = Convert.ToDateTime(StartDateTime);
            entity.EndDateTime = Convert.ToDateTime(EndDateTime);
            entity.ApplyID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
            entity.ApplyName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBATEmpAttendanceEventLog.AddATEmpAttendanceEventEgressevent((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, entity);
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventLog.Get(IBATEmpAttendanceEventLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLog.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventTripevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventTripevent.LabID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventTripevent.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag));

            entity.StartDateTime = Convert.ToDateTime(StartDateTime);
            entity.EndDateTime = Convert.ToDateTime(EndDateTime);
            entity.ApplyID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
            entity.ApplyName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBATEmpAttendanceEventLog.AddATEmpAttendanceEventTripevent((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, entity);
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventLog.Get(IBATEmpAttendanceEventLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddAndCheckATEmpAttendanceEventTripevent.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventOvertimeevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventOvertimeevent.LabID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventOvertimeevent.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag));

            entity.StartDateTime = Convert.ToDateTime(StartDateTime);
            entity.EndDateTime = Convert.ToDateTime(EndDateTime);
            entity.ApplyID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
            entity.ApplyName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBATEmpAttendanceEventLog.AddATEmpAttendanceEventOvertimeevent((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, entity);
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventLog.Get(IBATEmpAttendanceEventLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLog.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 新增员工考勤请假事件并验证是否允许申请
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="StartDateTime"></param>
        /// <param name="EndDateTime"></param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_AddAndCheckATEmpAttendanceEventleaveevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddAndCheckATEmpAttendanceEventleaveevent.LabID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddAndCheckATEmpAttendanceEventleaveevent.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag));

            entity.StartDateTime = Convert.ToDateTime(StartDateTime);
            entity.EndDateTime = Convert.ToDateTime(EndDateTime);
            entity.ApplyID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
            entity.ApplyName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultBool = IBATEmpAttendanceEventLog.AddAndCheckATEmpAttendanceEventleaveevent((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, entity);
                tempBaseResultDataValue.success = tempBaseResultBool.success;
                tempBaseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventLog.Get(IBATEmpAttendanceEventLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLog.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 新增员工考勤外出事件并验证是否允许申请
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="StartDateTime"></param>
        /// <param name="EndDateTime"></param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_AddAndCheckATEmpAttendanceEventEgressevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddAndCheckATEmpAttendanceEventEgressevent.LabID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddAndCheckATEmpAttendanceEventEgressevent.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag));

            entity.StartDateTime = Convert.ToDateTime(StartDateTime);
            entity.EndDateTime = Convert.ToDateTime(EndDateTime);
            entity.ApplyID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
            entity.ApplyName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultBool = IBATEmpAttendanceEventLog.AddAndCheckATEmpAttendanceEventEgressevent((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, entity);
                tempBaseResultDataValue.success = tempBaseResultBool.success;
                tempBaseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventLog.Get(IBATEmpAttendanceEventLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddAndCheckATEmpAttendanceEventEgressevent.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 新增员工考勤出差事件并验证是否允许申请
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="StartDateTime"></param>
        /// <param name="EndDateTime"></param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_AddAndCheckATEmpAttendanceEventTripevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddAndCheckATEmpAttendanceEventTripevent.LabID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddAndCheckATEmpAttendanceEventTripevent.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag));

            entity.StartDateTime = Convert.ToDateTime(StartDateTime);
            entity.EndDateTime = Convert.ToDateTime(EndDateTime);
            entity.ApplyID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
            entity.ApplyName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultBool = IBATEmpAttendanceEventLog.AddAndCheckATEmpAttendanceEventTripevent((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, entity);
                tempBaseResultDataValue.success = tempBaseResultBool.success;
                tempBaseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventLog.Get(IBATEmpAttendanceEventLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddAndCheckATEmpAttendanceEventTripevent.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 新增员工考勤加班事件并验证是否允许申请
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="StartDateTime"></param>
        /// <param name="EndDateTime"></param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_AddAndCheckATEmpAttendanceEventOvertimeevent(ATEmpAttendanceEventLog entity, string StartDateTime, string EndDateTime)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddAndCheckATEmpAttendanceEventOvertimeevent.LabID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddAndCheckATEmpAttendanceEventOvertimeevent.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag));

            entity.StartDateTime = Convert.ToDateTime(StartDateTime);
            entity.EndDateTime = Convert.ToDateTime(EndDateTime);
            entity.ApplyID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
            entity.ApplyName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultBool = IBATEmpAttendanceEventLog.AddAndCheckATEmpAttendanceEventOvertimeevent((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, entity);
                tempBaseResultDataValue.success = tempBaseResultBool.success;
                tempBaseResultDataValue.ErrorInfo = tempBaseResultBool.ErrorInfo;
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventLog.Get(IBATEmpAttendanceEventLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddAndCheckATEmpAttendanceEventOvertimeevent.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Add  ATEmpAttendanceEventLog
        public BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventLogSignIn(ATEmpAttendanceEventLog entity)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLogSignIn.LabID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLogSignIn.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag));
            entity.ApplyID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
            entity.ApplyName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBATEmpAttendanceEventLog.AddSignIn();
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventLog.Get(IBATEmpAttendanceEventLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLog.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        //Add  ATEmpAttendanceEventLog
        public BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventLogSignOut(ATEmpAttendanceEventLog entity)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLogSignOut.LabID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLogSignOut.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag));
            entity.ApplyID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
            entity.ApplyName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBATEmpAttendanceEventLog.AddSignOut();
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventLog.Get(IBATEmpAttendanceEventLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLog.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        //Add  ATEmpAttendanceEventLog
        public BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventLogUploadPostion(ATEmpAttendanceEventLog entity)
        {
            entity.ApplyID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
            entity.ApplyName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBATEmpAttendanceEventLog.AddUploadPostion();
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventLog.Get(IBATEmpAttendanceEventLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLog.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 员工考勤上报位置并与考勤设置进行验证,判断是否可以上报事件
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventLogUploadPostionAndCheck(ATEmpAttendanceEventLog entity)
        {
            entity.ApplyID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
            entity.ApplyName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBATEmpAttendanceEventLog.AddUploadPostionAndCheck();
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventLog.Get(IBATEmpAttendanceEventLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddATEmpAttendanceEventLog.ex:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 员工的考勤签到或签退时的地点及时间判断处理
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_AddATEmpAttendanceEventLogCheck(ATEmpAttendanceEventLog entity)
        {
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            try
            {
                tempBaseResultBool = IBATEmpAttendanceEventLog.AddATEmpAttendanceEventLogCheck();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 员工签到签退的考勤地点是否正确或脱岗处理
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="postionType"></param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_ATEmpAttendanceEventLogCheckPostion(ATEmpAttendanceEventLog entity, int postionType)
        {
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            try
            {
                tempBaseResultBool = IBATEmpAttendanceEventLog.ATEmpAttendanceEventLogCheckPostion(null, (ATEventPostionType)postionType);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 处理签到时间与考勤设置上班时间是正常还是迟到
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_ATEmpAttendanceEventLogCheckSignInTime(ATEmpAttendanceEventLog entity)
        {
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            try
            {
                tempBaseResultBool = IBATEmpAttendanceEventLog.ATEmpAttendanceEventLogCheckSignInTime(null);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 处理签退时间与考勤设置下班时间是正常还是早退
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_ATEmpAttendanceEventLogCheckSignOutTime(ATEmpAttendanceEventLog entity)
        {
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();

            try
            {
                tempBaseResultBool = IBATEmpAttendanceEventLog.ATEmpAttendanceEventLogCheckSignOutTime(null);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        /// <summary>
        /// 测试用
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_AddATEmpAttendanceEventLogCheckTest(long subType)
        {
            ATEmpAttendanceEventLog entity = new ATEmpAttendanceEventLog();

            entity.ApplyID = 5140692457370078869;
            entity.ApplyName = "龙福初";
            switch (subType)
            {
                case 10101://签到地点异常,超出考勤设置的地点范围27米
                    entity.Id = 5338265127092684671;
                    entity.ATEventTypeID = 10101;
                    entity.ATEventTypeName = "签到";
                    entity.ATEventSubTypeID = 10101;
                    entity.DataAddTime = DateTime.Parse("2016-11-25 08:41:15.000");
                    entity.ATEventLogPostion = "22.805170,108.314880";
                    entity.ATEventLogPostionName = "广西壮族自治区南宁市江南区星光大道3-6号金秋大厦1楼";
                    break;
                case 10201://签退地点异常,超出考勤设置的地点范围3,405.15米
                    entity.Id = 5338265127092684671;
                    entity.ATEventTypeID = 10201;
                    entity.ATEventTypeName = "签退";
                    entity.ATEventSubTypeID = 10201;
                    entity.DataAddTime = DateTime.Parse("2016-11-25 08:41:15.000");
                    entity.ATEventLogPostion = "22.774979,108.32241";
                    entity.ATEventLogPostionName = "中国广西壮族自治区南宁市江南区盘岭路";
                    break;
                case 101011://签到时间异常,迟到
                    entity.Id = 5338265127092684671;
                    entity.ATEventTypeID = 10101;
                    entity.ATEventTypeName = "签到";
                    entity.ATEventSubTypeID = 10101;
                    entity.DataAddTime = DateTime.Parse("2016-11-25 09:41:15.000");
                    entity.ATEventLogPostion = "22.80588, 108.31585";
                    entity.ATEventLogPostionName = "广西壮族自治区南宁市江南区水岸都市B座";
                    break;
                case 102011://签退时间异常,固定时间早退
                    entity.Id = 5338265127092684671;
                    entity.ATEventTypeID = 10201;
                    entity.ATEventTypeName = "签退";
                    entity.ATEventSubTypeID = 10201;
                    entity.DataAddTime = DateTime.Parse("2016-11-25 08:41:15.000");
                    entity.ATEventLogPostion = "22.80588, 108.31585";
                    entity.ATEventLogPostionName = "广西壮族自治区南宁市江南区水岸都市B座";
                    break;
                case 102012://签退时间异常,弹性时间早退
                    entity.Id = 5338265127092684671;
                    entity.ApplyID = 5167923262829645849;
                    entity.ApplyName = "梁海为";
                    entity.ATEventTypeID = 10201;
                    entity.ATEventTypeName = "签退";
                    entity.ATEventSubTypeID = 10201;
                    entity.DataAddTime = DateTime.Parse("2016-11-25 16:41:15.000");
                    entity.ATEventLogPostion = "22.80588, 108.31585";
                    entity.ATEventLogPostionName = "广西壮族自治区南宁市江南区水岸都市B座";
                    break;
                default:
                    break;
            }
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                switch (subType)
                {
                    case 10701://上报地点的测试
                        entity.Id = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong();
                        entity.ATEventTypeID = ATTypeId.P上报位置;
                        entity.ATEventTypeName = "上报位置";
                        entity.ATEventSubTypeID = ATTypeId.P上报位置;
                        entity.ATEventSubTypeName = "上报位置";
                        entity.DataAddTime = DateTime.Now;
                        entity.ATEventLogPostion = "22.805170,108.314880";
                        entity.ATEventLogPostionName = "广西壮族自治区南宁市江南区星光大道3-6号金秋大厦1楼";
                        BaseResultDataValue tempResult = IBATEmpAttendanceEventLog.AddUploadPostionAndCheck();
                        tempBaseResultBool.success = tempResult.success;
                        tempBaseResultBool.ErrorInfo = tempResult.ErrorInfo;
                        break;
                    default:
                        tempBaseResultBool = IBATEmpAttendanceEventLog.AddATEmpAttendanceEventLogCheck();
                        break;
                }

            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }

            string fristDay = DateTime.Now.ToString("yyyy-MM");
            DateTime tempStartDate = DateTime.Parse("2016-12");
            DateTime tempPerMonthDate = DateTime.Parse("2016-12");

            tempBaseResultBool.BoolInfo = tempBaseResultBool.BoolInfo + "Compare:" + DateTime.Compare(tempStartDate, tempPerMonthDate);
            return tempBaseResultBool;
        }
        //Update  ATEmpAttendanceEventLog
        public BaseResultBool ST_UDTO_UpdateATEmpAttendanceEventLog(ATEmpAttendanceEventLog entity)
        {
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBATEmpAttendanceEventLog.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ATEmpAttendanceEventLog
        public BaseResultBool ST_UDTO_UpdateATEmpAttendanceEventLogByField(ATEmpAttendanceEventLog entity, string fields)
        {
            IBATEmpAttendanceEventLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_UpdateATEmpAttendanceEventLogByField,fields:" + fields);
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBATEmpAttendanceEventLog.Entity, fields);
                    if (tempArray != null)
                    {
                        ZhiFang.Common.Log.Log.Debug("ST_UDTO_UpdateATEmpAttendanceEventLogByField,tempArray:" + string.Join("@@@@@@@@@@@@@", tempArray));
                        tempBaseResultBool.success = IBATEmpAttendanceEventLog.Update(tempArray);
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("ST_UDTO_UpdateATEmpAttendanceEventLogByField,tempArray:null");
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBATEmpAttendanceEventLog.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_UpdateATEmpAttendanceEventLogByField,错误信息:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  ATEmpAttendanceEventLog
        public BaseResultBool ST_UDTO_DelATEmpAttendanceEventLog(long longATEmpAttendanceEventLogID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBATEmpAttendanceEventLog.Remove(longATEmpAttendanceEventLogID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventLog(ATEmpAttendanceEventLog entity)
        {
            IBATEmpAttendanceEventLog.Entity = entity;
            EntityList<ATEmpAttendanceEventLog> tempEntityList = new EntityList<ATEmpAttendanceEventLog>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBATEmpAttendanceEventLog.Search();
                tempEntityList.count = IBATEmpAttendanceEventLog.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ATEmpAttendanceEventLog>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ATEmpAttendanceEventLog> tempEntityList = new EntityList<ATEmpAttendanceEventLog>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBATEmpAttendanceEventLog.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBATEmpAttendanceEventLog.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ATEmpAttendanceEventLog>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventLogById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBATEmpAttendanceEventLog.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ATEmpAttendanceEventLog>(tempEntity);
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
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchATEmpAttendanceEventLogById序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchATEmpAttendanceEventLogById查询错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue GetATEmpAttendanceEventLogByDTCode(string dtcode)
        {

            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (dtcode == null || dtcode.Trim() == "")
                    dtcode = DateTime.Now.ToString("yyyy-MM-dd");
                ZhiFang.Entity.OA.ViewObject.Response.SignInfo signinfo = IBATEmpAttendanceEventLog.GetSignInfoBydtcode(dtcode, long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID)));
                if (signinfo != null)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(signinfo);
                        tempBaseResultDataValue.success = true;
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "GetATEmpAttendanceEventLogByDTCode序列化错误：" + ex.Message;
                        ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventLogByDTCode序列化错误：" + ex.ToString());
                        //throw new Exception(ex.Message);
                    }
                }
                //tempBaseResultDataValue.ResultDataValue = "{\"id\":\"123123123\"}";
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetATEmpAttendanceEventLogByDTCode错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventLogByDTCode错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue GetATEmpAttendanceEventApproveByDeptId(string DeptId)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (DeptId == null || DeptId.Trim() == "")
                    DeptId = ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.HRDeptID);

                Entity.RBAC.HREmployee hremp = IBATEmpAttendanceEventLog.GetATEmpAttendanceEventApproveByDeptId(long.Parse(DeptId));
                if (hremp != null)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("HREmployee_Id,HREmployee_CName");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(hremp);
                        tempBaseResultDataValue.success = true;
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "GetATEmpAttendanceEventApproveByDeptId序列化错误：" + ex.Message;
                        ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventApproveByDeptId序列化错误：" + ex.ToString());
                        //throw new Exception(ex.Message);
                    }
                }
                //tempBaseResultDataValue.ResultDataValue = "{\"id\":\"123123123\"}";
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetATEmpAttendanceEventApproveByDeptId错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventApproveByDeptId错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue GetATEmpAttendanceEventApprove(string EmpId)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (EmpId == null || EmpId.Trim() == "")
                    EmpId = ZhiFang.Common.Public.Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);

                Entity.RBAC.HREmployee hremp = IBATEmpAttendanceEventLog.GetATEmpAttendanceEventApproveByEmpId(long.Parse(EmpId));
                if (hremp != null)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("HREmployee_Id,HREmployee_CName");
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(hremp);
                        tempBaseResultDataValue.success = true;
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "GetATEmpAttendanceEventApprove序列化错误：" + ex.Message;
                        ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventApprove序列化错误：" + ex.ToString());
                        //throw new Exception(ex.Message);
                    }
                }
                //tempBaseResultDataValue.ResultDataValue = "{\"id\":\"123123123\"}";
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetATEmpAttendanceEventApprove错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventApprove错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue GetATEmpAttendanceEventDayCount(string sd, string ed)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventDayCount,sd=" + sd + "@ed=" + ed);
                if (sd == null || sd.Trim() == "")
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "开始时间为空！";
                    ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventDayCount,开始时间为空!");
                }

                if (ed == null || ed.Trim() == "")
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "结束时间为空！";
                    ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventDayCount,结束时间为空!");
                }
                try
                {
                    tempBaseResultDataValue.ResultDataValue = IBATEmpAttendanceEventLog.GetATEmpAttendanceEventDayCount(sd, ed).ToString();
                    tempBaseResultDataValue.success = true;
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "GetATEmpAttendanceEventDayCount序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventDayCount序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetATEmpAttendanceEventDayCount错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventDayCount错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue GetATEmpAttendanceEventHourCount(string sd, string ed)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventDayCount,sd=" + sd + "@ed=" + ed);
                if (sd == null || sd.Trim() == "")
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "开始时间为空！";
                    ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventDayCount,开始时间为空!");
                }

                if (ed == null || ed.Trim() == "")
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "结束时间为空！";
                    ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventDayCount,结束时间为空!");
                }
                try
                {
                    tempBaseResultDataValue.ResultDataValue = IBATEmpAttendanceEventLog.GetATEmpAttendanceEventHourCount(sd, ed).ToString();
                    tempBaseResultDataValue.success = true;
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "GetATEmpAttendanceEventDayCount序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventDayCount序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetATEmpAttendanceEventDayCount错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("GetATEmpAttendanceEventDayCount错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue SearchATEmpSignLog(string sd, string ed, int type)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("SearchATEmpSignLog,sd=" + sd + "@ed=" + ed);
                if (sd == null || sd.Trim() == "")
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "开始时间为空！";
                    ZhiFang.Common.Log.Log.Error("SearchATEmpSignLog,开始时间为空!");
                }

                if (ed == null || ed.Trim() == "")
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "结束时间为空！";
                    ZhiFang.Common.Log.Log.Error("SearchATEmpSignLog,结束时间为空!");
                }

                IList<SignLog> sllist = IBATEmpAttendanceEventLog.SearchATEmpSignLog(sd, ed, type, long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID)));
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(sllist);
                    tempBaseResultDataValue.success = true;
                }


                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "SearchATEmpSignLog序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("SearchATEmpSignLog序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "SearchATEmpSignLog错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("SearchATEmpSignLog错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue SearchATEmpSignLogByLimit(string ed, int limit, int type)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("SearchATEmpSignLogByLimit,ed=" + ed + "@limit=" + limit);
                if (ed == null || ed.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("SearchATEmpSignLogByLimit,ed为空!");
                    ed = DateTime.Now.ToString("yyyy-MM-dd");
                }
                IList<SignLog> sllist = IBATEmpAttendanceEventLog.SearchATEmpSignLogByLimit(ed, limit, type, long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID)));
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(sllist);
                    tempBaseResultDataValue.success = true;
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "SearchATEmpSignLog序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("SearchATEmpSignLog序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "SearchATEmpSignLog错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("SearchATEmpSignLog错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue SearchATOtherSignLogByLimit(string ed, int limit, string otherempid, int type)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("SearchATOtherSignLogByLimit,ed=" + ed + "@limit=" + limit + "@otherempid=" + otherempid + "@type=" + type);
                if (ed == null || ed.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("SearchATOtherSignLogByLimit,ed为空!");
                    ed = DateTime.Now.ToString("yyyy-MM-dd");
                }
                long otherempidl = (otherempid != null && otherempid.Trim() != "") ? long.Parse(otherempid) : 0;
                ZhiFang.Common.Log.Log.Error("SearchATOtherSignLogByLimit,otherempidl:" + otherempidl);
                IList<SignLogEmpList> sginloglistlist = IBATEmpAttendanceEventLog.SearchATOtherSignLogByLimit(ed, limit, type, long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.HRDeptID)), long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID)), otherempidl);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(sginloglistlist);
                    ZhiFang.Common.Log.Log.Debug("SearchATOtherSignLogByLimit.tempBaseResultDataValue.ResultDataValue:" + tempBaseResultDataValue.ResultDataValue);
                    tempBaseResultDataValue.success = true;
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "SearchATOtherSignLogByLimit序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("SearchATOtherSignLogByLimit序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "SearchATOtherSignLogByLimit错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("SearchATOtherSignLogByLimit错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue SearchATMyApplyAllLogByLimit(string sd, string ed, int pageindex, int limit, string apsid, int type)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("SearchATMyApplyAllLogByLimit,ed=" + ed + "@pageindex=" + pageindex + "@limit=" + limit + "@apsid=" + apsid + "@type=" + type);


                if (ed == null || ed.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("SearchATMyApplyAllLogByLimit,ed为空!");
                    ed = DateTime.Now.ToString("yyyy-MM-dd");
                }

                IList<ATEmpApplyAllLog> sginloglistlist = IBATEmpAttendanceEventLog.SearchATEmpApplyAllLogByLimit(sd, ed, pageindex, limit, apsid, type, long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID)));
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(sginloglistlist);
                    ZhiFang.Common.Log.Log.Debug("SearchATMyApplyAllLogByLimit.tempBaseResultDataValue.ResultDataValue:" + tempBaseResultDataValue.ResultDataValue);
                    tempBaseResultDataValue.success = true;
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "SearchATMyApplyAllLogByLimit序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("SearchATMyApplyAllLogByLimit序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "SearchATMyApplyAllLogByLimit错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("SearchATMyApplyAllLogByLimit错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue SearchATMyApprovalAllLogByEmpId(string sd, string ed, int pageindex, int limit, string apsid, string typeidlist, string empid)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("SearchATMyApprovalAllLogByEmpId,sd=" + sd + "@ed=" + ed + "@pageindex=" + pageindex + "@limit=" + limit + "@apsid=" + apsid + "@typeidlist=" + typeidlist + "@empid=" + empid);


                if (ed == null || ed.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("SearchATMyApprovalAllLogByEmpId,ed为空!");
                }
                if (sd == null || sd.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("SearchATMyApprovalAllLogByEmpId,sd为空!");
                }
                if (empid == null || empid.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("SearchATMyApprovalAllLogByEmpId,empid!默认为当前登录者。");
                    empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                }
                IList<ATEmpApplyAllLog> sginloglistlist = IBATEmpAttendanceEventLog.SearchATMyApprovalAllLogByEmpId(sd, ed, pageindex, limit, apsid, typeidlist, long.Parse(empid));
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(sginloglistlist);
                    //ZhiFang.Common.Log.Log.Debug("SearchATMyApprovalAllLogByEmpId.tempBaseResultDataValue.ResultDataValue:" + tempBaseResultDataValue.ResultDataValue);
                    tempBaseResultDataValue.success = true;
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "SearchATMyApprovalAllLogByEmpId序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("SearchATMyApprovalAllLogByEmpId序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "SearchATMyApprovalAllLogByEmpId错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("SearchATMyApprovalAllLogByEmpId错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ApprovalATApplyEventLog(string memo, string[] eventlogids, int type)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("ApprovalATApplyEventLog,memo=" + memo + "@eventlogids=" + string.Join(",", eventlogids) + "@type=" + type);


                if (memo == null || memo.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ApprovalATApplyEventLog,memo为空!");
                }
                if (eventlogids == null || eventlogids.Length <= 0)
                {
                    ZhiFang.Common.Log.Log.Error("ApprovalATApplyEventLog,eventlogids为空!");
                }
                //if (empid == null || empid.Trim() == "")
                //{
                //    ZhiFang.Common.Log.Log.Error("SearchATMyApprovalAllLogByEmpId,empid!默认为当前登录者。");
                //    empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                //}
                tempBaseResultDataValue.success = IBATEmpAttendanceEventLog.ApprovalATApplyEventLog((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, memo, eventlogids, type, long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID)));
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    if (tempBaseResultDataValue.success)
                    {
                        //tempBaseResultDataValue.ResultDataValue = tempBaseResultDataValue.success.ToString();
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "ApprovalATApplyEventLog序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("ApprovalATApplyEventLog序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "ApprovalATApplyEventLog错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ApprovalATApplyEventLog错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue GetEmpMonthLog(long EmpId, string MonthCode)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Debug("GetEmpMonthLog,EmpId=" + EmpId + "@MonthCode=" + MonthCode);
                if (EmpId <= 0)
                {
                    ZhiFang.Common.Log.Log.Error("GetEmpMonthLog,EmpId为空!");
                }
                if (MonthCode == null || MonthCode.Length <= 0)
                {
                    ZhiFang.Common.Log.Log.Error("GetEmpMonthLog,MonthCode为空!");
                }
                //if (empid == null || empid.Trim() == "")
                //{
                //    ZhiFang.Common.Log.Log.Error("SearchATMyApprovalAllLogByEmpId,empid!默认为当前登录者。");
                //    empid = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                //}
                ATEmpMonthLog atempmonthlog = IBATEmpAttendanceEventLog.GetEmpMonthLog(EmpId, MonthCode, long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID)));
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(atempmonthlog);
                    //ZhiFang.Common.Log.Log.Debug("SearchATMyApprovalAllLogByEmpId.tempBaseResultDataValue.ResultDataValue:" + tempBaseResultDataValue.ResultDataValue);
                    tempBaseResultDataValue.success = true;
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "GetEmpMonthLog序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("GetEmpMonthLog序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetEmpMonthLog错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("GetEmpMonthLog错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue GetATEmpListWeekLog(long Type, string StartDate, string EndDate)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("GetATEmpListWeekLog,Type=" + Type + "@StartDate=" + StartDate + "@EndDate=" + EndDate);
                if (Type <= 0)
                {
                    ZhiFang.Common.Log.Log.Error("GetATEmpListWeekLog,Type为空!");
                }
                if (StartDate == null || StartDate.Length <= 0)
                {
                    ZhiFang.Common.Log.Log.Error("GetATEmpListWeekLog,StartDate为空!");
                }
                if (EndDate == null || EndDate.Length <= 0)
                {
                    ZhiFang.Common.Log.Log.Error("GetATEmpListWeekLog,EndDate为空!");
                }

                List<ATEmpListWeekLog> atelwllist = IBATEmpAttendanceEventLog.GetATEmpListWeekLog(Type, StartDate, EndDate, long.Parse(Cookie.CookieHelper.Read(DicCookieSession.EmployeeID)), Cookie.CookieHelper.Read(DicCookieSession.EmployeeName), long.Parse(Cookie.CookieHelper.Read(DicCookieSession.HRDeptID)), Cookie.CookieHelper.Read(DicCookieSession.HRDeptName));
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(atelwllist);
                    //ZhiFang.Common.Log.Log.Debug("SearchATMyApprovalAllLogByEmpId.tempBaseResultDataValue.ResultDataValue:" + tempBaseResultDataValue.ResultDataValue);
                    tempBaseResultDataValue.success = true;
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "GetATEmpListWeekLog序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("GetATEmpListWeekLog序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetATEmpListWeekLog错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("GetATEmpListWeekLog错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        #endregion

        #region ATEmpAttendanceEventParaSettings
        //Add  ATEmpAttendanceEventParaSettings
        public BaseResultDataValue ST_UDTO_AddATEmpAttendanceEventParaSettings(ATEmpAttendanceEventParaSettings entity)
        {
            IBATEmpAttendanceEventParaSettings.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBATEmpAttendanceEventParaSettings.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBATEmpAttendanceEventParaSettings.Get(IBATEmpAttendanceEventParaSettings.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATEmpAttendanceEventParaSettings.Entity);
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
        //Update  ATEmpAttendanceEventParaSettings
        public BaseResultBool ST_UDTO_UpdateATEmpAttendanceEventParaSettings(ATEmpAttendanceEventParaSettings entity)
        {
            IBATEmpAttendanceEventParaSettings.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBATEmpAttendanceEventParaSettings.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ATEmpAttendanceEventParaSettings
        public BaseResultBool ST_UDTO_UpdateATEmpAttendanceEventParaSettingsByField(ATEmpAttendanceEventParaSettings entity, string fields)
        {
            IBATEmpAttendanceEventParaSettings.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBATEmpAttendanceEventParaSettings.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBATEmpAttendanceEventParaSettings.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBATEmpAttendanceEventParaSettings.Edit();
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
        //Delele  ATEmpAttendanceEventParaSettings
        public BaseResultBool ST_UDTO_DelATEmpAttendanceEventParaSettings(long longATEmpAttendanceEventParaSettingsID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBATEmpAttendanceEventParaSettings.Remove(longATEmpAttendanceEventParaSettingsID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventParaSettings(ATEmpAttendanceEventParaSettings entity)
        {
            IBATEmpAttendanceEventParaSettings.Entity = entity;
            EntityList<ATEmpAttendanceEventParaSettings> tempEntityList = new EntityList<ATEmpAttendanceEventParaSettings>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBATEmpAttendanceEventParaSettings.Search();
                tempEntityList.count = IBATEmpAttendanceEventParaSettings.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ATEmpAttendanceEventParaSettings>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventParaSettingsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ATEmpAttendanceEventParaSettings> tempEntityList = new EntityList<ATEmpAttendanceEventParaSettings>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBATEmpAttendanceEventParaSettings.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBATEmpAttendanceEventParaSettings.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ATEmpAttendanceEventParaSettings>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventParaSettingsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBATEmpAttendanceEventParaSettings.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ATEmpAttendanceEventParaSettings>(tempEntity);
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


        public BaseResultDataValue ST_UDTO_SearchATEmpAttendanceEventParaSettingsByDeptId(string deptid, bool isincludesubdept)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchATEmpAttendanceEventParaSettingsByDeptId,deptid=" + deptid + "@isincludesubdept=" + isincludesubdept);


                if (deptid == null || deptid.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchATEmpAttendanceEventParaSettingsByDeptId,deptid为空!");
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchATEmpAttendanceEventParaSettingsByDeptId,monthday为空!";
                    return tempBaseResultDataValue;
                }
                IList<ATEmpAttendanceEventParaSettings> worklogvolist = IBATEmpAttendanceEventParaSettings.SearchATEmpAttendanceEventParaSettingsByDeptId(long.Parse(deptid), isincludesubdept);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(worklogvolist);
                    //ZhiFang.Common.Log.Log.Debug("SearchATMyApprovalAllLogByEmpId.tempBaseResultDataValue.ResultDataValue:" + tempBaseResultDataValue.ResultDataValue);
                    tempBaseResultDataValue.success = true;
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchATEmpAttendanceEventParaSettingsByDeptId序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchATEmpAttendanceEventParaSettingsByDeptId序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchATEmpAttendanceEventParaSettingsByDeptId错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchATEmpAttendanceEventParaSettingsByDeptId错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region ATTransportation
        //Add  ATTransportation
        public BaseResultDataValue ST_UDTO_AddATTransportation(ATTransportation entity)
        {
            IBATTransportation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBATTransportation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBATTransportation.Get(IBATTransportation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATTransportation.Entity);
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
        //Update  ATTransportation
        public BaseResultBool ST_UDTO_UpdateATTransportation(ATTransportation entity)
        {
            IBATTransportation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBATTransportation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ATTransportation
        public BaseResultBool ST_UDTO_UpdateATTransportationByField(ATTransportation entity, string fields)
        {
            IBATTransportation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBATTransportation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBATTransportation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBATTransportation.Edit();
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
        //Delele  ATTransportation
        public BaseResultBool ST_UDTO_DelATTransportation(long longATTransportationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBATTransportation.Remove(longATTransportationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchATTransportation(ATTransportation entity)
        {
            IBATTransportation.Entity = entity;
            EntityList<ATTransportation> tempEntityList = new EntityList<ATTransportation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBATTransportation.Search();
                tempEntityList.count = IBATTransportation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ATTransportation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchATTransportationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ATTransportation> tempEntityList = new EntityList<ATTransportation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBATTransportation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBATTransportation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ATTransportation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchATTransportationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBATTransportation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ATTransportation>(tempEntity);
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
        #endregion

        #region BAccountType
        //Add  BAccountType
        public BaseResultDataValue ST_UDTO_AddBAccountType(BAccountType entity)
        {
            IBBAccountType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBAccountType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBAccountType.Get(IBBAccountType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBAccountType.Entity);
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
        //Update  BAccountType
        public BaseResultBool ST_UDTO_UpdateBAccountType(BAccountType entity)
        {
            IBBAccountType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBAccountType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BAccountType
        public BaseResultBool ST_UDTO_UpdateBAccountTypeByField(BAccountType entity, string fields)
        {
            IBBAccountType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBAccountType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBAccountType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBAccountType.Edit();
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
        //Delele  BAccountType
        public BaseResultBool ST_UDTO_DelBAccountType(long longBAccountTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBAccountType.Remove(longBAccountTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBAccountType(BAccountType entity)
        {
            IBBAccountType.Entity = entity;
            EntityList<BAccountType> tempEntityList = new EntityList<BAccountType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBAccountType.Search();
                tempEntityList.count = IBBAccountType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BAccountType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBAccountTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BAccountType> tempEntityList = new EntityList<BAccountType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBAccountType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBAccountType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BAccountType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBAccountTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBAccountType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BAccountType>(tempEntity);
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
        #endregion

        #region BIcons
        //Add  BIcons
        public BaseResultDataValue ST_UDTO_AddBIcons(BIcons entity)
        {
            IBBIcons.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBIcons.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBIcons.Get(IBBIcons.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBIcons.Entity);
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
        //Update  BIcons
        public BaseResultBool ST_UDTO_UpdateBIcons(BIcons entity)
        {
            IBBIcons.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBIcons.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BIcons
        public BaseResultBool ST_UDTO_UpdateBIconsByField(BIcons entity, string fields)
        {
            IBBIcons.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBIcons.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBIcons.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBIcons.Edit();
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
        //Delele  BIcons
        public BaseResultBool ST_UDTO_DelBIcons(long longBIconsID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBIcons.Remove(longBIconsID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBIcons(BIcons entity)
        {
            IBBIcons.Entity = entity;
            EntityList<BIcons> tempEntityList = new EntityList<BIcons>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBIcons.Search();
                tempEntityList.count = IBBIcons.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BIcons>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBIconsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BIcons> tempEntityList = new EntityList<BIcons>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBIcons.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBIcons.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BIcons>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBIconsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBIcons.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BIcons>(tempEntity);
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
        #endregion

        #region BWeiXinEmpLink
        //Add  BWeiXinEmpLink
        public BaseResultDataValue ST_UDTO_AddBWeiXinEmpLink(BWeiXinEmpLink entity)
        {
            IBBWeiXinEmpLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBWeiXinEmpLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBWeiXinEmpLink.Get(IBBWeiXinEmpLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBWeiXinEmpLink.Entity);
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
        //Update  BWeiXinEmpLink
        public BaseResultBool ST_UDTO_UpdateBWeiXinEmpLink(BWeiXinEmpLink entity)
        {
            IBBWeiXinEmpLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBWeiXinEmpLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BWeiXinEmpLink
        public BaseResultBool ST_UDTO_UpdateBWeiXinEmpLinkByField(BWeiXinEmpLink entity, string fields)
        {
            IBBWeiXinEmpLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBWeiXinEmpLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBWeiXinEmpLink.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBWeiXinEmpLink.Edit();
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
        //Delele  BWeiXinEmpLink
        public BaseResultBool ST_UDTO_DelBWeiXinEmpLink(long longBWeiXinEmpLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBWeiXinEmpLink.Remove(longBWeiXinEmpLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBWeiXinEmpLink(BWeiXinEmpLink entity)
        {
            IBBWeiXinEmpLink.Entity = entity;
            EntityList<BWeiXinEmpLink> tempEntityList = new EntityList<BWeiXinEmpLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBWeiXinEmpLink.Search();
                tempEntityList.count = IBBWeiXinEmpLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BWeiXinEmpLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBWeiXinEmpLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BWeiXinEmpLink> tempEntityList = new EntityList<BWeiXinEmpLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBWeiXinEmpLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBWeiXinEmpLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BWeiXinEmpLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBWeiXinEmpLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBWeiXinEmpLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BWeiXinEmpLink>(tempEntity);
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

        public BaseResultDataValue ST_UDTO_AddBWeiXinEmpLinkByUserAccount(string strUserAccount, string strPassWord, bool isValidate)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddBWeiXinEmpLinkByUserAccount:strUserAccount：" + strUserAccount.ToString()+ "@@@strPassWord:"+ strPassWord);
            // IBBWeiXinEmpLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                //ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddBWeiXinEmpLinkByUserAccount:strUserAccount：1");
                string ErrorInfo;
                //ZhiFang.Common.Log.Log.Debug("WeiXinOpenID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.WeiXinOpenID));
                string WeiXinOpenID = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.WeiXinOpenID);
                if (WeiXinOpenID != null && WeiXinOpenID.Trim() != "")
                {
                    HREmployee emp;
                    //ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddBWeiXinEmpLinkByUserAccount:strUserAccount：2");
                    tempBaseResultDataValue.success = IBBWeiXinEmpLink.AddByUserAccountOpenID(strUserAccount, strPassWord, WeiXinOpenID, out ErrorInfo, out emp);
                    if (emp != null)
                    {
                        Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.LabID, "");
                        Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.IsLabFlag, "");

                        SessionHelper.SetSessionValue(SysPublicSet.SysDicCookieSession.LabID, emp.LabID.ToString());//实验室ID
                        SessionHelper.SetSessionValue(DicCookieSession.UserAccount, emp.RBACUserList[0].Account);//员工账户名
                        SessionHelper.SetSessionValue(DicCookieSession.UseCode, emp.RBACUserList[0].UseCode);//员工代码

                        Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.LabID, emp.LabID.ToString());//实验室ID
                        if (emp.LabID > 0)
                            Cookie.CookieHelper.Write(SysPublicSet.SysDicCookieSession.IsLabFlag, "1");

                        Cookie.CookieHelper.Write(DicCookieSession.UserID, emp.RBACUserList[0].Id.ToString());
                        Cookie.CookieHelper.Write(DicCookieSession.UserAccount, emp.RBACUserList[0].Account);
                        Cookie.CookieHelper.Write(DicCookieSession.UseCode, emp.RBACUserList[0].UseCode);

                        //Cookie.CookieHelper.Write("000500", "4794031815009582380"); // 模块ID
                        SessionHelper.SetSessionValue(DicCookieSession.EmployeeID, emp.Id); //员工ID
                        SessionHelper.SetSessionValue(DicCookieSession.EmployeeName, emp.CName);//员工姓名 

                        SessionHelper.SetSessionValue(DicCookieSession.EmployeeUseCode, emp.UseCode);//员工代码 

                        SessionHelper.SetSessionValue(DicCookieSession.HRDeptID, emp.HRDept.Id);//部门ID
                        SessionHelper.SetSessionValue(DicCookieSession.HRDeptName, emp.HRDept.CName);//部门名称

                        //员工时间戳
                        //SessionHelper.SetSessionValue(rbacUser.HREmployee.Id.ToString(), rbacUser.HREmployee.DataTimeStamp);

                        Cookie.CookieHelper.Write(DicCookieSession.EmployeeID, emp.Id.ToString());// 员工ID
                        Cookie.CookieHelper.Write(DicCookieSession.EmployeeName, emp.CName);// 员工姓名
                        Cookie.CookieHelper.Write(DicCookieSession.EmployeeUseCode, emp.UseCode);// 员工代码

                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptID, emp.HRDept.Id.ToString());//部门ID
                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptName, emp.HRDept.CName);//部门名称

                        Cookie.CookieHelper.Write(DicCookieSession.HRDeptCode, emp.HRDept.UseCode);//部门名称
                    }
                    else
                    {
                        ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddBWeiXinEmpLinkByUserAccount.Emp为空!");
                    }
                    //ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddBWeiXinEmpLinkByUserAccount:strUserAccount：3");
                }
                else
                {
                    //ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddBWeiXinEmpLinkByUserAccount:strUserAccount：4");
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：未能读取Cookie中的OpenId";
                    ZhiFang.Common.Log.Log.Debug("错误信息：未能读取Cookie中的OpenId");
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Debug("账户和微信绑定异常，错误信息：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region BWeiXinAccount
        //Add  BWeiXinAccount
        public BaseResultDataValue ST_UDTO_AddBWeiXinAccount(BWeiXinAccount entity)
        {
            IBBWeiXinAccount.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBWeiXinAccount.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBWeiXinAccount.Get(IBBWeiXinAccount.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBWeiXinAccount.Entity);
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
        //Update  BWeiXinAccount
        public BaseResultBool ST_UDTO_UpdateBWeiXinAccount(BWeiXinAccount entity)
        {
            IBBWeiXinAccount.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBWeiXinAccount.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BWeiXinAccount
        public BaseResultBool ST_UDTO_UpdateBWeiXinAccountByField(BWeiXinAccount entity, string fields)
        {
            IBBWeiXinAccount.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBWeiXinAccount.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBWeiXinAccount.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBWeiXinAccount.Edit();
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
        //Delele  BWeiXinAccount
        public BaseResultBool ST_UDTO_DelBWeiXinAccount(long longBWeiXinAccountID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBWeiXinAccount.Remove(longBWeiXinAccountID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBWeiXinAccount(BWeiXinAccount entity)
        {
            IBBWeiXinAccount.Entity = entity;
            EntityList<BWeiXinAccount> tempEntityList = new EntityList<BWeiXinAccount>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBWeiXinAccount.Search();
                tempEntityList.count = IBBWeiXinAccount.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BWeiXinAccount>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBWeiXinAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BWeiXinAccount> tempEntityList = new EntityList<BWeiXinAccount>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBWeiXinAccount.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBWeiXinAccount.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BWeiXinAccount>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_GetBWeiXinAccountByWeiXinAccount(long WeiXinAccount,string fields)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BWeiXinAccount> tempEntityList = new EntityList<BWeiXinAccount>();
            try
            {
                
                    tempEntityList = IBBWeiXinAccount.SearchListByHQL("WeiXinAccount='" + WeiXinAccount + "'", -1, -1);
               
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                   
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                   
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

        public BaseResultDataValue ST_UDTO_SearchBWeiXinAccountById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBWeiXinAccount.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BWeiXinAccount>(tempEntity);
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
        #endregion

        #region BWeiXinUserGroup
        //Add  BWeiXinUserGroup
        public BaseResultDataValue ST_UDTO_AddBWeiXinUserGroup(BWeiXinUserGroup entity)
        {
            IBBWeiXinUserGroup.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBWeiXinUserGroup.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBWeiXinUserGroup.Get(IBBWeiXinUserGroup.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBWeiXinUserGroup.Entity);
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
        //Update  BWeiXinUserGroup
        public BaseResultBool ST_UDTO_UpdateBWeiXinUserGroup(BWeiXinUserGroup entity)
        {
            IBBWeiXinUserGroup.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBWeiXinUserGroup.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BWeiXinUserGroup
        public BaseResultBool ST_UDTO_UpdateBWeiXinUserGroupByField(BWeiXinUserGroup entity, string fields)
        {
            IBBWeiXinUserGroup.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBWeiXinUserGroup.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBWeiXinUserGroup.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBWeiXinUserGroup.Edit();
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
        //Delele  BWeiXinUserGroup
        public BaseResultBool ST_UDTO_DelBWeiXinUserGroup(long longBWeiXinUserGroupID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBWeiXinUserGroup.Remove(longBWeiXinUserGroupID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchBWeiXinUserGroup(BWeiXinUserGroup entity)
        {
            IBBWeiXinUserGroup.Entity = entity;
            EntityList<BWeiXinUserGroup> tempEntityList = new EntityList<BWeiXinUserGroup>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBWeiXinUserGroup.Search();
                tempEntityList.count = IBBWeiXinUserGroup.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BWeiXinUserGroup>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBWeiXinUserGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BWeiXinUserGroup> tempEntityList = new EntityList<BWeiXinUserGroup>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBWeiXinUserGroup.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBWeiXinUserGroup.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BWeiXinUserGroup>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchBWeiXinUserGroupById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBWeiXinUserGroup.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BWeiXinUserGroup>(tempEntity);
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
        #endregion

        #region SServiceClient
        //Add  SServiceClient
        public BaseResultDataValue ST_UDTO_AddSServiceClient(SServiceClient entity)
        {
            IBSServiceClient.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSServiceClient.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSServiceClient.Get(IBSServiceClient.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSServiceClient.Entity);
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
        //Update  SServiceClient
        public BaseResultBool ST_UDTO_UpdateSServiceClient(SServiceClient entity)
        {
            IBSServiceClient.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSServiceClient.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SServiceClient
        public BaseResultBool ST_UDTO_UpdateSServiceClientByField(SServiceClient entity, string fields)
        {
            IBSServiceClient.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSServiceClient.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSServiceClient.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSServiceClient.Edit();
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
        //Delele  SServiceClient
        public BaseResultBool ST_UDTO_DelSServiceClient(long longSServiceClientID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSServiceClient.Remove(longSServiceClientID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchSServiceClient(SServiceClient entity)
        {
            IBSServiceClient.Entity = entity;
            EntityList<SServiceClient> tempEntityList = new EntityList<SServiceClient>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSServiceClient.Search();
                tempEntityList.count = IBSServiceClient.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SServiceClient>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchSServiceClientByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SServiceClient> tempEntityList = new EntityList<SServiceClient>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSServiceClient.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSServiceClient.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SServiceClient>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchSServiceClientById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSServiceClient.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SServiceClient>(tempEntity);
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
        #endregion

        #region SServiceClientlevel
        //Add  SServiceClientlevel
        public BaseResultDataValue ST_UDTO_AddSServiceClientlevel(SServiceClientlevel entity)
        {
            IBSServiceClientlevel.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBSServiceClientlevel.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBSServiceClientlevel.Get(IBSServiceClientlevel.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBSServiceClientlevel.Entity);
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
        //Update  SServiceClientlevel
        public BaseResultBool ST_UDTO_UpdateSServiceClientlevel(SServiceClientlevel entity)
        {
            IBSServiceClientlevel.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSServiceClientlevel.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  SServiceClientlevel
        public BaseResultBool ST_UDTO_UpdateSServiceClientlevelByField(SServiceClientlevel entity, string fields)
        {
            IBSServiceClientlevel.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBSServiceClientlevel.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBSServiceClientlevel.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBSServiceClientlevel.Edit();
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
        //Delele  SServiceClientlevel
        public BaseResultBool ST_UDTO_DelSServiceClientlevel(long longSServiceClientlevelID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBSServiceClientlevel.Remove(longSServiceClientlevelID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchSServiceClientlevel(SServiceClientlevel entity)
        {
            IBSServiceClientlevel.Entity = entity;
            EntityList<SServiceClientlevel> tempEntityList = new EntityList<SServiceClientlevel>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBSServiceClientlevel.Search();
                tempEntityList.count = IBSServiceClientlevel.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SServiceClientlevel>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchSServiceClientlevelByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SServiceClientlevel> tempEntityList = new EntityList<SServiceClientlevel>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBSServiceClientlevel.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBSServiceClientlevel.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SServiceClientlevel>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchSServiceClientlevelById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBSServiceClientlevel.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<SServiceClientlevel>(tempEntity);
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
        #endregion 

        #region jsapi
        /// <summary>
        /// 获取jsapi签名
        /// </summary>
        /// <param name="noncestr">随机字符串</param>
        /// <param name="timestamp">时间戳</param>
        /// <param name="url">URL地址</param>
        /// <returns></returns>
        public BaseResultDataValue GetJSAPISignature(string noncestr, string timestamp, string url)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Debug("noncestr=" + noncestr + ";timestamp=" + timestamp + ";url=" + url);
                if (!(noncestr != null && noncestr.Trim().Length > 0))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：noncestr格式不正确！";
                    return brdv;
                }
                if (!(timestamp != null && timestamp.Trim().Length > 0))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：timestamp格式不正确！";
                    return brdv;
                }
                if (!(url != null && url.Trim().Length > 0))
                {
                    brdv.success = false;
                    brdv.ErrorInfo = "错误信息：url格式不正确！";
                    return brdv;
                }
                int expires_in;
                ZhiFang.Common.Log.Log.Debug("HttpContext.Current.Application:" + HttpContext.Current.Application.AllKeys.Length);
                string signature = BasePage.GetSignature(HttpContext.Current.Application, noncestr, timestamp, url, out expires_in);
                brdv.ResultDataValue = "{\"signature\":\"" + signature + "\",\"TimeOut\":" + expires_in + "}";
                brdv.success = true;
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return brdv;

        }

        #endregion

        //#region 微信消息推送
        //public static void PushWeiXinMessageAction(string openid, string templateid, string color, string url, Dictionary<string, TemplateDataObject> data)
        //{
        //    if (ConfigHelper.GetConfigString("PushMessageFlag") == "1")
        //    {
        //        string tid = (templateid != null && templateid.Trim() != "") ? templateid : "r0zTjCUo_93wlQPydX2mwpXEak8UVcwO-PsTzdxLqjI";
        //        string c = (color != null && color.Trim() != "") ? color : "#336699";
        //        string u = (url != null && url.Trim() != "") ? url : "";
        //        BasePage.PushMessageTemplateContext(HttpContext.Current.Application, openid, tid, u, c, data);
        //    }
        //}
        //#endregion
        #region 考勤统计
        /// <summary>
        /// 获取公司所有员工的考勤统计信息
        /// </summary>
        /// <param name="monthCode">年月</param>
        /// <param name="wagesDays">工资日总天数</param>
        /// <param name="punch">每天的打卡次数</param>
        /// <returns></returns>
        public BaseResultDataValue GetAllMonthLogCountList(string monthCode, int wagesDays, int punch)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (String.IsNullOrEmpty(monthCode))
                {
                    ZhiFang.Common.Log.Log.Error("GetAllMonthLogCountList,monthCode为空!");
                }
                IList<ATEmpMonthLogCount> atempmonthlogCount = IBATEmpAttendanceEventLog.GetAllMonthLogCountList(monthCode, wagesDays, punch);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(atempmonthlogCount);

                    tempBaseResultDataValue.success = true;
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "GetAllMonthLogCountList序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("GetAllMonthLogCountList序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "GetAllMonthLogCountList错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("GetAllMonthLogCountList错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 导出公司所有的员工的某一个月的考勤统计信息
        /// </summary>
        /// <param name="operateType"></param>
        /// <param name="monthCode"></param>
        /// <param name="wagesDays"></param>
        /// <param name="punch"></param>
        /// <returns></returns>
        public Stream SC_UDTO_DownLoadExportExcelOfAllMonthLogCount(long operateType, string monthCode, int wagesDays, int punch)
        {
            FileStream fileStream = null;
            monthCode = monthCode.Replace("/", "-");
            monthCode = monthCode.Trim();
            string filename = monthCode.Replace("-", "") + "考勤统计.xlsx";
            try
            {
                fileStream = IBATEmpAttendanceEventLog.GetExportExcelOfAllMonthLogCount(monthCode, wagesDays, punch, filename);
                if (fileStream != null)
                {
                    Encoding code = Encoding.GetEncoding("GB2312");
                    System.Web.HttpContext.Current.Response.Charset = "GB2312";
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    filename = EncodeFileName.ToEncodeFileName(filename);
                    if (operateType == 0) //下载文件application/octet-stream
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/ms-excel";// "" + file.FileType;
                        if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                        {
                            //如果是火狐,修改为下载
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        }
                        else
                        {
                            WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //fileStream = null;
                ZhiFang.Common.Log.Log.Error("错误信息:" + ex.Message);
                //throw new Exception(ex.Message);
            }
            return fileStream;
        }

        /// <summary>
        /// 获取员工考勤统计清单信息(不包含打卡签到签退)
        /// </summary>
        /// /// <param name="searchType">查询类型</param>
        /// <param name="attypeId">考勤事件类型Id</param>
        /// <param name="deptId">部门id</param>
        /// <param name="isGetSubDept">是否获取子部门的员工信息</param>
        /// <param name="empId">员工id字符串,如123,232,1233</param>
        /// <param name="startDate">开始日期</param>
        /// <param name="endDate">结束日期</param>
        /// <returns></returns>
        public BaseResultDataValue SC_UDTO_GetATEmpAttendanceEventLogDetailList(long searchType, string attypeId, string deptId, bool isGetSubDept, string empId, string startDate, string endDate, long approveStatusID, int page, int limit, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ATEmpApplyAllLog> entityList = new EntityList<ATEmpApplyAllLog>();
            if (tempBaseResultDataValue.success && searchType < -0)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "searchType为空!";
            }
            if (tempBaseResultDataValue.success)
            {
                verificationDate(startDate, endDate, ref tempBaseResultDataValue);
            }

            if (tempBaseResultDataValue.success)
            {
                try
                {
                    entityList = IBATEmpAttendanceEventLog.GetATEmpAttendanceEventLogDetailList(searchType, attypeId, deptId, isGetSubDept, empId, startDate, endDate, approveStatusID, page, limit, sort);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                    try
                    {
                        //不带实体名称的
                        //tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
                        //带实体名称的
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ATEmpApplyAllLog>(entityList);
                        tempBaseResultDataValue.success = true;
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "SC_UDTO_GetATEmpAttendanceEventLogDetailList序列化错误：" + ex.Message;
                        ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "SC_UDTO_GetATEmpAttendanceEventLogDetailList错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo + ex.StackTrace);
                    //throw new Exception(ex.Message);
                }
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 导出员工考勤统计清单信息(不包含打卡签到签退)
        /// </summary>
        /// <param name="operateType"></param>
        /// <param name="searchType"></param>
        /// <param name="attypeId"></param>
        /// <param name="deptId"></param>
        /// <param name="isGetSubDept"></param>
        /// <param name="empId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="approveStatusID"></param>
        /// <returns></returns>
        public Stream SC_UDTO_DownLoadExportExcelOfATEmpAttendanceEventLogDetail(long operateType, long searchType, string attypeId, string deptId, bool isGetSubDept, string empId, string startDate, string endDate, long approveStatusID)
        {
            FileStream fileStream = null;
            string filename = "考勤统计清单.xlsx";
            bool isExec = true;
            if (searchType <= 0)
            {
                isExec = false;
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "searchType为空!");
                return memoryStream;
            }
            if (isExec)
            {
                if ((String.IsNullOrEmpty(startDate) || String.IsNullOrEmpty(endDate)))
                {
                    isExec = false;
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "查询日期范围为空!");
                    return memoryStream;
                }
                else if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                {
                    DateTime dtStart = DateTime.Parse(startDate);
                    DateTime dtEnd = DateTime.Parse(endDate);
                    if (dtStart.CompareTo(dtEnd) >0)
                    {
                        isExec = false;
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "开始日期大于结束日期!");
                        return memoryStream;
                    }
                }
            }
            if (isExec)
            {
                try
                {

                    fileStream = IBATEmpAttendanceEventLog.GetExportExcelOfATEmpAttendanceEventLogDetail(searchType, attypeId, deptId, isGetSubDept, empId, startDate, endDate, approveStatusID, ref filename);
                    if (fileStream != null)
                    {
                        Encoding code = Encoding.GetEncoding("GB2312");
                        System.Web.HttpContext.Current.Response.Charset = "GB2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        filename = EncodeFileName.ToEncodeFileName(filename);
                        if (operateType == 0) //下载文件application/octet-stream
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        }
                        else if (operateType == 1)//直接打开文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/ms-excel";// "" + file.FileType;
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                //如果是火狐,修改为下载
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                            }
                            else
                            {
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                            }
                        }
                    }
                    else
                    {
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出考勤统计清单数据为空!");
                        return memoryStream;
                    }
                }
                catch (Exception ex)
                {
                    //fileStream = null;
                    ZhiFang.Common.Log.Log.Error("导出考勤统计清单错误:" + ex.Message);
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出考勤统计清单错误!");
                    return memoryStream;
                }
            }
            return fileStream;
        }
        /// <summary>
        /// 获取员工考勤统计打卡清单信息
        /// </summary>
        /// <param name="filterType"></param>
        /// <param name="deptId"></param>
        /// <param name="isGetSubDept"></param>
        /// <param name="empId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        public BaseResultDataValue SC_UDTO_GetATEmpSignInfoDetailList(string filterType, string deptId, bool isGetSubDept, string empId, string startDate, string endDate, int page, int limit, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<SignInfoExport> entityList = new EntityList<SignInfoExport>();
            verificationDate(startDate, endDate, ref tempBaseResultDataValue);
            if (tempBaseResultDataValue.success)
            {
                try
                {
                    entityList = IBATEmpAttendanceEventLog.GetATEmpSignInfoDetailList(filterType, deptId, isGetSubDept, empId, startDate, endDate, page, limit, sort);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                    try
                    {
                        //不带实体名称的
                        //tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(entityList);
                        //带实体名称的
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<SignInfoExport>(entityList);
                        tempBaseResultDataValue.success = true;
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "SC_UDTO_GetATEmpSignInfoDetailList序列化错误：" + ex.Message;
                        ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                        //throw new Exception(ex.Message);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "SC_UDTO_GetATEmpSignInfoDetailList错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo + ex.StackTrace);
                    //throw new Exception(ex.Message);
                }
            }
            return tempBaseResultDataValue;
        }

        private void verificationDate(string startDate, string endDate, ref BaseResultDataValue tempBaseResultDataValue)
        {
            if ((String.IsNullOrEmpty(startDate) || String.IsNullOrEmpty(endDate)))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询日期范围为空!";
            }
            else if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
            {
                DateTime dtStart = DateTime.Parse(startDate);
                DateTime dtEnd = DateTime.Parse(endDate);
                if (dtStart.CompareTo(dtEnd) > 0)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "开始日期大于结束日期!";
                }
            }
        }
        public Stream SC_UDTO_DownLoadGetExportExcelOfATEmpSignInfoDetail(long operateType, string filterType, string deptId, bool isGetSubDept, string empId, string startDate, string endDate)
        {
            FileStream fileStream = null;
            string filename = "考勤统计打卡清单.xlsx";
            bool isExec = true;
            if (isExec)
            {
                if ((String.IsNullOrEmpty(startDate) || String.IsNullOrEmpty(endDate)))
                {
                    isExec = false;
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "查询日期范围为空!");
                    return memoryStream;
                }
                else if (!String.IsNullOrEmpty(startDate) && !String.IsNullOrEmpty(endDate))
                {
                    DateTime dtStart = DateTime.Parse(startDate);
                    DateTime dtEnd = DateTime.Parse(endDate);
                    if (dtStart.CompareTo(dtEnd) > 0)
                    {
                        isExec = false;
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "开始日期大于结束日期!");
                        return memoryStream;
                    }
                }
            }
            if (isExec)
            {
                try
                {
                    fileStream = IBATEmpAttendanceEventLog.GetExportExcelOfATEmpSignInfoDetail(filterType, deptId, isGetSubDept, empId, startDate, endDate, ref filename);
                    if (fileStream != null)
                    {
                        Encoding code = Encoding.GetEncoding("GB2312");
                        System.Web.HttpContext.Current.Response.Charset = "GB2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        filename = EncodeFileName.ToEncodeFileName(filename);
                        if (operateType == 0) //下载文件application/octet-stream
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "application/ms-excel";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        }
                        else if (operateType == 1)//直接打开文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/ms-excel";// "" + file.FileType;
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                //如果是火狐,修改为下载
                                System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                            }
                            else
                            {
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + filename + "\"");
                            }
                        }
                    }
                    else
                    {
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出考勤统计打卡清单数据为空!");
                        return memoryStream;
                    }
                }
                catch (Exception ex)
                {
                    //fileStream = null;
                    ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出考勤统计打卡清单错误!");
                    return memoryStream;
                }
            }
            return fileStream;
        }
        #endregion

        #region ATHolidaySetting
        //Add  ATHolidaySetting
        public BaseResultDataValue ST_UDTO_AddATHolidaySetting(ATHolidaySetting entity)
        {
            IBATHolidaySetting.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBATHolidaySetting.AddAndValidation(entity);
                if (tempBaseResultDataValue.success)
                {
                    IBATHolidaySetting.Get(IBATHolidaySetting.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBATHolidaySetting.Entity);
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
        //Update  ATHolidaySetting
        public BaseResultBool ST_UDTO_UpdateATHolidaySetting(ATHolidaySetting entity)
        {
            IBATHolidaySetting.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBATHolidaySetting.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ATHolidaySetting
        public BaseResultBool ST_UDTO_UpdateATHolidaySettingByField(ATHolidaySetting entity, string fields)
        {
            IBATHolidaySetting.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBATHolidaySetting.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBATHolidaySetting.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBATHolidaySetting.Edit();
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
        //Delele  ATHolidaySetting
        public BaseResultBool ST_UDTO_DelATHolidaySetting(long longATHolidaySettingID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBATHolidaySetting.Remove(longATHolidaySettingID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultBool ST_UDTO_DelATHolidaySettingByIdStr(string idStr)
        {
            if (string.IsNullOrEmpty(idStr))
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_DelATHolidaySettingByIdStr,idStr为空!");
            }
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if (!string.IsNullOrEmpty(idStr))
                {
                    tempBaseResultBool.success = IBATHolidaySetting.DelATHolidaySettingByIdStr(idStr);
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
        /// <summary>
        /// 获取某年的某一月共有多少个工作日天数(工资日)
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_GetWorkDaysOfOneMonth(int year, int month)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            int workDays = 0;
            try
            {
                workDays = IBATHolidaySetting.GetWorkDaysOfOneMonth(year, month, null, null);
                tempBaseResultDataValue.ResultDataValue = "{ \"workDays\":\"" + workDays + "\"" + "}";
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ResultDataValue = "{ \"workDays\":\"" + workDays + "\"" + "}";
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_SearchATHolidaySetting(ATHolidaySetting entity)
        {
            IBATHolidaySetting.Entity = entity;
            EntityList<ATHolidaySetting> tempEntityList = new EntityList<ATHolidaySetting>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBATHolidaySetting.Search();
                tempEntityList.count = IBATHolidaySetting.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ATHolidaySetting>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchATHolidaySettingByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ATHolidaySetting> tempEntityList = new EntityList<ATHolidaySetting>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBATHolidaySetting.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBATHolidaySetting.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ATHolidaySetting>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchATHolidaySettingByYearAndMonth(int year, int month)
        {
            ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchATHolidaySettingByYearAndMonth,year=" + year + "@month=" + month);
            if (year <= 0)
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchATHolidaySettingByYearAndMonth,year为空!");
            }
            if (month <= 0)
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchATHolidaySettingByYearAndMonth,month!");
            }
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            IList<ATHolidaySetting> tempEntityList = new List<ATHolidaySetting>();
            try
            {
                if (year >= 0 && month >= 0)
                {
                    tempEntityList = IBATHolidaySetting.SearchATHolidaySettingByYearAndMonth(year, month, null);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchATHolidaySettingById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBATHolidaySetting.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ATHolidaySetting>(tempEntity);
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
        #endregion

        public BaseResultDataValue GetPermanentMediaNewsList(int page, int limit)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                int offset = 0;
                int count = 0;
                if (page <= 0)
                {
                    page = 1;
                }
                if (limit <= 0)
                {
                    limit = 10;
                }
                offset = (page - 1) * limit;
                count = page + limit - 1;
                PermanentMediaList pmlist = BasePage.GetPermanentMediaList(HttpContext.Current.Application, PermanentMediaType.news, offset, count);
                if (pmlist != null)
                {
                    if (pmlist.item != null && pmlist.item.Length > 0)
                    {
                        for (int i = 0; i < pmlist.item.Length; i++)
                        {
                            if (pmlist.item[i] != null && pmlist.item[i].content != null && pmlist.item[i].content.news_item != null && pmlist.item[i].content.news_item.Length > 0)
                            {
                                for (int j = 0; j < pmlist.item[i].content.news_item.Length; j++)
                                {
                                    pmlist.item[i].content.news_item[j].thumb_media_Url = BasePage.GetPermanentMediaFile(HttpContext.Current.Application, pmlist.item[i].content.news_item[j].thumb_media_id).Replace(System.AppDomain.CurrentDomain.BaseDirectory, "");
                                }
                            }
                        }
                    }
                    tempBaseResultDataValue.ResultDataValue = JsonSerializer.JsonDotNetSerializer(pmlist);
                }
                else
                {
                    tempBaseResultDataValue.ResultDataValue = "";
                }
                tempBaseResultDataValue.success = true;
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
    }
}
