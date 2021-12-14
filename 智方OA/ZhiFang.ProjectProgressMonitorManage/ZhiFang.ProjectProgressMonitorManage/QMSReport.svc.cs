using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.ProjectProgressMonitorManage;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Request;
using ZhiFang.Entity.ProjectProgressMonitorManage.ViewObject.Response;
using ZhiFang.ProjectProgressMonitorManage.ServerContract;
using ZhiFang.ServiceCommon.RBAC;
using ZhiFang.Common.Public;
using ZhiFang.Entity.RBAC;
using ZhiFang.ProjectProgressMonitorManage.Common;

namespace ZhiFang.ProjectProgressMonitorManage
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class QMSReport : IQMSReport
    {
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBEEquip IBEEquip { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBEParameter IBEParameter { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBEMaintenanceData IBEMaintenanceData { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBEReportData IBEReportData { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBETemplet IBETemplet { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBETempletEmp IBETempletEmp { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBEAttachment IBEAttachment { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBEResEmp IBEResEmp { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBEResponsibility IBEResponsibility { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBETempletRes IBETempletRes { get; set; }

        ZhiFang.IBLL.RBAC.IBHREmployee IBHREmployee { get; set; }

        #region 基础服务

        #region EEquip
        //Add  EEquip
        public BaseResultDataValue ST_UDTO_AddEEquip(EEquip entity)
        {
            IBEEquip.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBEEquip.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBEEquip.Get(IBEEquip.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBEEquip.Entity);
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
        //Update  EEquip
        public BaseResultBool ST_UDTO_UpdateEEquip(EEquip entity)
        {
            IBEEquip.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBEEquip.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  EEquip
        public BaseResultBool ST_UDTO_UpdateEEquipByField(EEquip entity, string fields)
        {
            IBEEquip.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBEEquip.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBEEquip.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBEEquip.Edit();
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
        //Delele  EEquip
        public BaseResultBool ST_UDTO_DelEEquip(long longEEquipID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBEEquip.Remove(longEEquipID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchEEquip(EEquip entity)
        {
            IBEEquip.Entity = entity;
            EntityList<EEquip> tempEntityList = new EntityList<EEquip>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBEEquip.Search();
                tempEntityList.count = IBEEquip.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<EEquip>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchEEquipByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<EEquip> tempEntityList = new EntityList<EEquip>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBEEquip.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBEEquip.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<EEquip>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchEEquipById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBEEquip.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<EEquip>(tempEntity);
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


        #region EParameter
        //Add  EParameter
        public BaseResultDataValue ST_UDTO_AddEParameter(EParameter entity)
        {
            IBEParameter.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBEParameter.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBEParameter.Get(IBEParameter.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBEParameter.Entity);
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
        //Update  EParameter
        public BaseResultBool ST_UDTO_UpdateEParameter(EParameter entity)
        {
            IBEParameter.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBEParameter.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  EParameter
        public BaseResultBool ST_UDTO_UpdateEParameterByField(EParameter entity, string fields)
        {
            IBEParameter.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBEParameter.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBEParameter.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBEParameter.Edit();
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
        //Delele  EParameter
        public BaseResultBool ST_UDTO_DelEParameter(long longEParameterID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBEParameter.Remove(longEParameterID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchEParameter(EParameter entity)
        {
            IBEParameter.Entity = entity;
            EntityList<EParameter> tempEntityList = new EntityList<EParameter>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBEParameter.Search();
                tempEntityList.count = IBEParameter.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<EParameter>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchEParameterByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<EParameter> tempEntityList = new EntityList<EParameter>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBEParameter.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBEParameter.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<EParameter>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchEParameterById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBEParameter.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<EParameter>(tempEntity);
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


        #region EMaintenanceData
        //Add  EMaintenanceData
        public BaseResultDataValue ST_UDTO_AddEMaintenanceData(EMaintenanceData entity)
        {
            IBEMaintenanceData.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (entity != null)
                {
                    entity.Operater = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    entity.OperateTime = DateTime.Now;
                    entity.DataAddTime = DateTime.Now;
                    if (entity.ItemDate == null)
                        entity.ItemDate = DateTime.Today;
                }
                tempBaseResultDataValue.success = IBEMaintenanceData.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBEMaintenanceData.Get(IBEMaintenanceData.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBEMaintenanceData.Entity);
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
        //Update  EMaintenanceData
        public BaseResultBool ST_UDTO_UpdateEMaintenanceData(EMaintenanceData entity)
        {
            IBEMaintenanceData.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBEMaintenanceData.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  EMaintenanceData
        public BaseResultBool ST_UDTO_UpdateEMaintenanceDataByField(EMaintenanceData entity, string fields)
        {
            IBEMaintenanceData.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((entity != null) && (fields != null) && (fields.Length > 0))
                {

                    entity.Operater = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
                    entity.OperateTime = DateTime.Now;
                    entity.DataUpdateTime = DateTime.Now;
                    fields += ",Operater,OperateTime,DataUpdateTime";
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBEMaintenanceData.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBEMaintenanceData.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBEMaintenanceData.Edit();
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
        //Delele  EMaintenanceData
        public BaseResultBool ST_UDTO_DelEMaintenanceData(long longEMaintenanceDataID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBEMaintenanceData.Remove(longEMaintenanceDataID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchEMaintenanceData(EMaintenanceData entity)
        {
            IBEMaintenanceData.Entity = entity;
            EntityList<EMaintenanceData> tempEntityList = new EntityList<EMaintenanceData>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBEMaintenanceData.Search();
                tempEntityList.count = IBEMaintenanceData.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<EMaintenanceData>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchEMaintenanceDataByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<EMaintenanceData> tempEntityList = new EntityList<EMaintenanceData>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBEMaintenanceData.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBEMaintenanceData.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<EMaintenanceData>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchEMaintenanceDataById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBEMaintenanceData.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<EMaintenanceData>(tempEntity);
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


        #region EReportData
        //Add  EReportData
        public BaseResultDataValue ST_UDTO_AddEReportData(EReportData entity)
        {
            IBEReportData.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBEReportData.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBEReportData.Get(IBEReportData.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBEReportData.Entity);
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
        //Update  EReportData
        public BaseResultBool ST_UDTO_UpdateEReportData(EReportData entity)
        {
            IBEReportData.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBEReportData.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  EReportData
        public BaseResultBool ST_UDTO_UpdateEReportDataByField(EReportData entity, string fields)
        {
            IBEReportData.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBEReportData.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBEReportData.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBEReportData.Edit();
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
        //Delele  EReportData
        public BaseResultBool ST_UDTO_DelEReportData(long longEReportDataID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBEReportData.Remove(longEReportDataID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchEReportData(EReportData entity)
        {
            IBEReportData.Entity = entity;
            EntityList<EReportData> tempEntityList = new EntityList<EReportData>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBEReportData.Search();
                tempEntityList.count = IBEReportData.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<EReportData>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchEReportDataByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<EReportData> tempEntityList = new EntityList<EReportData>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBEReportData.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBEReportData.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<EReportData>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchEReportDataById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBEReportData.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<EReportData>(tempEntity);
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


        #region ETemplet
        //Add  ETemplet
        public BaseResultDataValue ST_UDTO_AddETemplet(ETemplet entity)
        {
            IBETemplet.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBETemplet.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBETemplet.Get(IBETemplet.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBETemplet.Entity);
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
        //Update  ETemplet
        public BaseResultBool ST_UDTO_UpdateETemplet(ETemplet entity)
        {
            IBETemplet.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBETemplet.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ETemplet
        public BaseResultBool ST_UDTO_UpdateETempletByField(ETemplet entity, string fields)
        {
            IBETemplet.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    tempBaseResultBool = IBETemplet.EditETemplet(entity, fields);
                    //string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBETemplet.Entity, fields);
                    //if (tempArray != null)
                    //{
                    //    tempBaseResultBool.success = IBETemplet.Update(tempArray);
                    //}
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBETemplet.Edit();
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
        //Delele  ETemplet
        public BaseResultBool ST_UDTO_DelETemplet(long longETempletID)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                string templetPath = "";
                ETemplet templet = IBETemplet.Get(longETempletID);
                if (templet != null)
                {
                    templetPath = templet.TempletPath;
                    if (!string.IsNullOrEmpty(templetPath))
                        templetPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath") + templetPath;
                }
                baseResultBool.success = IBETemplet.Remove(longETempletID);
                if (baseResultBool.success)
                    if (File.Exists(templetPath))
                    {
                        string destFileName = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath") + "\\" + templet.LabID.ToString() + "\\QMS\\ExcelTemplet\\DeleteTemplet\\";
                        if (!Directory.Exists(destFileName))
                        {
                            Directory.CreateDirectory(destFileName);
                        }
                        string fileName = Path.GetFileName(templetPath);
                        destFileName = destFileName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileName;
                        File.Copy(templetPath, destFileName, true);
                        File.Delete(templetPath);
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

        public BaseResultDataValue ST_UDTO_SearchETemplet(ETemplet entity)
        {
            IBETemplet.Entity = entity;
            EntityList<ETemplet> tempEntityList = new EntityList<ETemplet>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBETemplet.Search();
                tempEntityList.count = IBETemplet.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ETemplet>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchETempletByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ETemplet> tempEntityList = new EntityList<ETemplet>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBETemplet.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBETemplet.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ETemplet>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchETempletById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBETemplet.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ETemplet>(tempEntity);
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


        #region ETempletEmp
        //Add  ETempletEmp
        public BaseResultDataValue ST_UDTO_AddETempletEmp(ETempletEmp entity)
        {
            IBETempletEmp.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBETempletEmp.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBETempletEmp.Get(IBETempletEmp.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBETempletEmp.Entity);
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
        //Update  ETempletEmp
        public BaseResultBool ST_UDTO_UpdateETempletEmp(ETempletEmp entity)
        {
            IBETempletEmp.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBETempletEmp.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  ETempletEmp
        public BaseResultBool ST_UDTO_UpdateETempletEmpByField(ETempletEmp entity, string fields)
        {
            IBETempletEmp.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBETempletEmp.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBETempletEmp.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBETempletEmp.Edit();
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
        //Delele  ETempletEmp
        public BaseResultBool ST_UDTO_DelETempletEmp(long longETempletEmpID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBETempletEmp.Remove(longETempletEmpID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchETempletEmp(ETempletEmp entity)
        {
            IBETempletEmp.Entity = entity;
            EntityList<ETempletEmp> tempEntityList = new EntityList<ETempletEmp>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBETempletEmp.Search();
                tempEntityList.count = IBETempletEmp.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ETempletEmp>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchETempletEmpByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ETempletEmp> tempEntityList = new EntityList<ETempletEmp>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBETempletEmp.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBETempletEmp.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ETempletEmp>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchETempletEmpById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBETempletEmp.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ETempletEmp>(tempEntity);
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


        #region EAttachment
        //Add  EAttachment
        public BaseResultDataValue ST_UDTO_AddEAttachment(EAttachment entity)
        {
            IBEAttachment.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBEAttachment.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBEAttachment.Get(IBEAttachment.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBEAttachment.Entity);
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
        //Update  EAttachment
        public BaseResultBool ST_UDTO_UpdateEAttachment(EAttachment entity)
        {
            IBEAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBEAttachment.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  EAttachment
        public BaseResultBool ST_UDTO_UpdateEAttachmentByField(EAttachment entity, string fields)
        {
            IBEAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBEAttachment.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBEAttachment.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBEAttachment.Edit();
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
        //Delele  EAttachment
        public BaseResultBool ST_UDTO_DelEAttachment(long longEAttachmentID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                EAttachment attachment = IBEAttachment.Get(longEAttachmentID);
                if (IBEAttachment != null)
                {
                    if (!string.IsNullOrEmpty(attachment.FilePath))
                    {
                        string filePath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath") + attachment.FilePath;
                        if (File.Exists(filePath))
                            File.Delete(filePath);
                    }
                    IBEAttachment.Entity = attachment;
                    //tempBaseResultBool.success = IBETemplet.Remove();
                }
                tempBaseResultBool.success = IBEAttachment.Remove(longEAttachmentID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchEAttachment(EAttachment entity)
        {
            IBEAttachment.Entity = entity;
            EntityList<EAttachment> tempEntityList = new EntityList<EAttachment>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBEAttachment.Search();
                tempEntityList.count = IBEAttachment.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<EAttachment>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchEAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<EAttachment> tempEntityList = new EntityList<EAttachment>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBEAttachment.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBEAttachment.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<EAttachment>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchEAttachmentById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBEAttachment.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<EAttachment>(tempEntity);
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


        #region EResEmp
        //Add  EResEmp
        public BaseResultDataValue ST_UDTO_AddEResEmp(EResEmp entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBEResEmp.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBEResEmp.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  EResEmp
        public BaseResultBool ST_UDTO_UpdateEResEmp(EResEmp entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBEResEmp.Entity = entity;
                try
                {
                    baseResultBool.success = IBEResEmp.Edit();
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  EResEmp
        public BaseResultBool ST_UDTO_UpdateEResEmpByField(EResEmp entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBEResEmp.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBEResEmp.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBEResEmp.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBEResEmp.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  EResEmp
        public BaseResultBool ST_UDTO_DelEResEmp(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBEResEmp.Entity = IBEResEmp.Get(id);
                if (IBEResEmp.Entity != null)
                {
                    long labid = IBEResEmp.Entity.LabID;
                    string entityName = IBEResEmp.Entity.GetType().Name;
                    baseResultBool.success = IBEResEmp.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
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

        public BaseResultDataValue ST_UDTO_SearchEResEmp(EResEmp entity)
        {
            EntityList<EResEmp> entityList = new EntityList<EResEmp>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBEResEmp.Entity = entity;
                try
                {
                    entityList.list = IBEResEmp.Search();
                    entityList.count = IBEResEmp.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<EResEmp>(entityList);
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
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchEResEmpByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<EResEmp> entityList = new EntityList<EResEmp>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBEResEmp.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBEResEmp.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<EResEmp>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchEResEmpById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBEResEmp.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<EResEmp>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
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
        #endregion


        #region EResponsibility
        //Add  EResponsibility
        public BaseResultDataValue ST_UDTO_AddEResponsibility(EResponsibility entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBEResponsibility.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBEResponsibility.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  EResponsibility
        public BaseResultBool ST_UDTO_UpdateEResponsibility(EResponsibility entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBEResponsibility.Entity = entity;
                try
                {
                    baseResultBool.success = IBEResponsibility.Edit();
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  EResponsibility
        public BaseResultBool ST_UDTO_UpdateEResponsibilityByField(EResponsibility entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBEResponsibility.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBEResponsibility.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBEResponsibility.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBEResponsibility.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  EResponsibility
        public BaseResultBool ST_UDTO_DelEResponsibility(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBEResponsibility.Entity = IBEResponsibility.Get(id);
                if (IBEResponsibility.Entity != null)
                {
                    long labid = IBEResponsibility.Entity.LabID;
                    string entityName = IBEResponsibility.Entity.GetType().Name;
                    baseResultBool.success = IBEResponsibility.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
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

        public BaseResultDataValue ST_UDTO_SearchEResponsibility(EResponsibility entity)
        {
            EntityList<EResponsibility> entityList = new EntityList<EResponsibility>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBEResponsibility.Entity = entity;
                try
                {
                    entityList.list = IBEResponsibility.Search();
                    entityList.count = IBEResponsibility.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<EResponsibility>(entityList);
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
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchEResponsibilityByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<EResponsibility> entityList = new EntityList<EResponsibility>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBEResponsibility.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBEResponsibility.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<EResponsibility>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchEResponsibilityById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBEResponsibility.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<EResponsibility>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
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
        #endregion


        #region ETempletRes
        //Add  ETempletRes
        public BaseResultDataValue ST_UDTO_AddETempletRes(ETempletRes entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBETempletRes.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBETempletRes.Add();
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(entity.Id);
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "AddEntity", "新增实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }
        //Update  ETempletRes
        public BaseResultBool ST_UDTO_UpdateETempletRes(ETempletRes entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBETempletRes.Entity = entity;
                try
                {
                    baseResultBool.success = IBETempletRes.Edit();
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Update  ETempletRes
        public BaseResultBool ST_UDTO_UpdateETempletResByField(ETempletRes entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBETempletRes.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBETempletRes.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBETempletRes.Update(tempArray);
                            if (baseResultBool.success)
                            {
                                //IBBSampleOperate.AddObjectOperate(entity, entity.GetType().Name, "EditEntity", "修改实体操作");
                            }
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                        //baseResultBool.success = IBETempletRes.Edit();
                    }
                }
                catch (Exception ex)
                {
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        //Delele  ETempletRes
        public BaseResultBool ST_UDTO_DelETempletRes(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBETempletRes.Entity = IBETempletRes.Get(id);
                if (IBETempletRes.Entity != null)
                {
                    long labid = IBETempletRes.Entity.LabID;
                    string entityName = IBETempletRes.Entity.GetType().Name;
                    baseResultBool.success = IBETempletRes.RemoveByHQL(id);
                    if (baseResultBool.success)
                    {
                        //IBBSampleOperate.AddObjectOperate(id, labid, entityName, "DeleteEntity", "删除实体操作");
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

        public BaseResultDataValue ST_UDTO_SearchETempletRes(ETempletRes entity)
        {
            EntityList<ETempletRes> entityList = new EntityList<ETempletRes>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBETempletRes.Entity = entity;
                try
                {
                    entityList.list = IBETempletRes.Search();
                    entityList.count = IBETempletRes.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ETempletRes>(entityList);
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
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchETempletResByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<ETempletRes> entityList = new EntityList<ETempletRes>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBETempletRes.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBETempletRes.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<ETempletRes>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchETempletResById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBETempletRes.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<ETempletRes>(entity);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entity, fields);
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
        #endregion


        #endregion 基础服务

        #region 定制服务

        public BaseResultDataValue QMS_UDTO_AddEParameter(EParameter entity)
        {
            IBEParameter.Entity = entity;
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBEParameter.AddEParameter(entity);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QMS_UDTO_AddEMaintenanceData(long templetID, string itemDate, string typeCode, string templetBatNo, IList<EMaintenanceData> entityList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<EMaintenanceData> resultList = new List<EMaintenanceData>();
                baseResultDataValue = IBEMaintenanceData.JudgeTempletIsFillData(templetID, typeCode, itemDate);
                if (!baseResultDataValue.success)
                {
                    return baseResultDataValue;
                }
                baseResultDataValue = IBEMaintenanceData.AddEMaintenanceData(templetID, itemDate, typeCode, templetBatNo, entityList, ref resultList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            }
            return baseResultDataValue;
        }


        public BaseResultDataValue QMS_UDTO_AddEMaintenanceDataAndResult(long templetID, string itemDate, string templetBatNo, IList<EMaintenanceData> entityList, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<EMaintenanceData> resultList = new List<EMaintenanceData>();
                baseResultDataValue = IBEMaintenanceData.AddEMaintenanceData(templetID, itemDate, templetBatNo, entityList, ref resultList);
                if (baseResultDataValue.success)
                {
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<EMaintenanceData>(resultList);
                        }
                        else
                        {
                            baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(resultList, fields);
                        }
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增质量登记数据错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QMS_UDTO_SearchEMaintenanceData(long templetID, string itemDate, string typeCode, string templetBatNo, int isLoadBeforeData, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<EMaintenanceData> tempEntityList = new EntityList<EMaintenanceData>();
            try
            {
                DateTime tempDate = DateTime.Now;
                if (!string.IsNullOrEmpty(itemDate))
                    tempDate = DateTime.Parse(itemDate);
                tempEntityList = IBEMaintenanceData.SearchEMaintenanceDataByTypeCode(templetID, tempDate, typeCode, templetBatNo, isLoadBeforeData);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<EMaintenanceData>(tempEntityList);
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

        public BaseResultDataValue QMS_UDTO_SearchMaintenanceDataTB(long templetID, string typeCode, string beginDate, string endDate, int isLoadBeforeData)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(beginDate) || string.IsNullOrEmpty(endDate))
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：开始日期或结束日期不能为空！";
                }
                else
                {
                    if (string.IsNullOrEmpty(typeCode))
                        typeCode = "TB";
                    beginDate = DateTime.Parse(beginDate).ToString("yyyy-MM-dd");
                    endDate = DateTime.Parse(endDate).AddDays(1).ToString("yyyy-MM-dd");
                    baseResultDataValue = IBEMaintenanceData.GroupMaintenanceDataTB(templetID, typeCode, beginDate, endDate, isLoadBeforeData);
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

        public BaseResultDataValue QMS_UDTO_DelMaintenanceDataTB(long templetID, string typeCode, string itemDate, string batchNumber)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (string.IsNullOrEmpty(typeCode))
                    typeCode = "TB";
                IBEMaintenanceData.DeleteMaintenanceDataTB(templetID, typeCode, itemDate, batchNumber);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 新增仪器Excel模板文件
        /// </summary>
        /// <returns></returns>
        public Message QMS_UDTO_AddExcelTemplet()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string entityJson = "";
                string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
                for (int i = 0; i < allkeys.Length; i++)
                {
                    switch (allkeys[i])
                    {
                        case "entityJson":
                            if (HttpContext.Current.Request.Form["entityJson"].Trim() != "")
                                entityJson = HttpContext.Current.Request.Form["entityJson"].Trim();
                            break;
                    }
                }
                ETemplet templet = null;
                if (!string.IsNullOrEmpty(entityJson))//FFile实体参数的json串,序列化实体
                    templet = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<ETemplet>(entityJson);

                if (templet != null)
                {
                    int iTotal = HttpContext.Current.Request.Files.Count;
                    if (iTotal > 0)
                    {
                        HttpPostedFile file = HttpContext.Current.Request.Files[0];
                        int len = file.ContentLength;
                        if (len > 0)
                        {
                            string equipName = "";
                            if (templet.EEquip != null && templet.EEquip.Id > 0)
                                equipName = IBEEquip.QueryEquipNameByID(templet.EEquip.Id, true);
                            //对于不指定仪器的模板，存储在OtherTemplet文件夹下
                            if (string.IsNullOrEmpty(equipName))
                                equipName = "OtherTemplet";
                            string fileName = file.FileName;
                            if (string.IsNullOrEmpty(fileName))
                                fileName = "Templet" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + ".xlsx";
                            else if (!string.IsNullOrEmpty(templet.CName))
                                fileName = templet.CName + Path.GetExtension(file.FileName);
                            string parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath");
                            string relativePath = "\\" + templet.LabID.ToString() + "\\QMS\\ExcelTemplet\\" + equipName + "\\";
                            string fileDirectory = parentPath + relativePath;
                            if (!Directory.Exists(fileDirectory))
                            {
                                Directory.CreateDirectory(fileDirectory);
                            }
                            string filePathName = Path.Combine(fileDirectory, Path.GetFileName(fileName));
                            file.SaveAs(filePathName);
                            templet.TempletPath = relativePath + Path.GetFileName(fileName);
                            templet.DataAddTime = DateTime.Now;
                            IBETemplet.AddETemplet(templet);
                        }
                        else
                        {
                            brdv.ErrorInfo = "上传的模板文件无效！";
                            brdv.success = false;
                        }
                    }
                    else
                    {
                        brdv.ErrorInfo = "无法获取上传的模板文件！";
                        brdv.success = false;
                    }
                }
                else
                {
                    brdv.ErrorInfo = "模板信息为空或序列化失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "新增模板时发生错误，原因为：<br>" + ex.Message;
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("QMS_UDTO_AddExcelTemplet异常：" + ex.ToString());
            }
            string strResult = ZhiFang.ProjectProgressMonitorManage.Common.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        /// <summary>
        /// 修改仪器Excel模板文件
        /// </summary>
        /// <returns></returns>
        public Message QMS_UDTO_UpdateExcelTemplet()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string entityJson = "";
                string fields = "";
                string filePathName = "";
                string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
                for (int i = 0; i < allkeys.Length; i++)
                {
                    switch (allkeys[i])
                    {
                        case "entityJson":
                            if (HttpContext.Current.Request.Form["entityJson"].Trim() != "")
                                entityJson = HttpContext.Current.Request.Form["entityJson"].Trim();
                            break;
                        case "fields":
                            if (HttpContext.Current.Request.Form["fields"].Trim() != "")
                                fields = HttpContext.Current.Request.Form["fields"].Trim();
                            break;
                    }
                }
                ETemplet templet = null;
                if (!string.IsNullOrEmpty(entityJson))
                    templet = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<ETemplet>(entityJson);
                int iTotal = HttpContext.Current.Request.Files.Count;
                string parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath");
                if (templet != null && iTotal > 0)
                {

                    HttpPostedFile file = HttpContext.Current.Request.Files[0];
                    int len = file.ContentLength;
                    if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                    {
                        string equipName = IBETemplet.QueryEquipNameByID(templet.Id, true);
                        string fileName = file.FileName;
                        if (!string.IsNullOrEmpty(templet.CName))
                            fileName = templet.CName + Path.GetExtension(file.FileName);
                        string relativePath = "\\" + templet.LabID.ToString() + "\\QMS\\ExcelTemplet\\" + equipName + "\\";
                        string fileDirectory = parentPath + relativePath;
                        if (!Directory.Exists(fileDirectory))
                        {
                            Directory.CreateDirectory(fileDirectory);
                        }
                        filePathName = Path.Combine(fileDirectory, Path.GetFileName(fileName));
                        file.SaveAs(filePathName);
                        templet.TempletPath = relativePath + Path.GetFileName(fileName);
                    }
                    //else
                    //{
                    //    baseResultDataValue.ErrorInfo = "文件大小为0或为空！";
                    //    baseResultDataValue.success = false;
                    //}
                }
                if (templet != null && fields != null && fields.Length > 0)
                {
                    if (!string.IsNullOrEmpty(filePathName))
                    {
                        fields += "," + "TempletPath,TempletStruct,TempletFillStruct,FillStruct,ShowFillItem";
                        IBETemplet.GetTempletStruct(templet, filePathName);
                    }
                    fields += "," + "DataUpdateTime";
                    templet.DataUpdateTime = DateTime.Now;
                    baseResultDataValue.success = IBETemplet.EditETemplet(templet, fields).success;
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：修改时实体或fields参数不能为空！";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.ErrorInfo = "修改模板时发生错误，原因为：<br>" + ex.Message;
                baseResultDataValue.success = false;
                ZhiFang.Common.Log.Log.Error("QMS_UDTO_UpdateExcelTemplet异常：" + ex.ToString());
            }
            string strResult = ZhiFang.ProjectProgressMonitorManage.Common.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        /// <summary>
        /// 删除仪器Excel模板文件
        /// </summary>
        /// <param name="longETempletID"></param>
        /// <returns></returns>
        public BaseResultBool QMS_UDTO_DelETemplet(long id, bool isDelTempletData)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                ZhiFang.Common.Log.Log.Info("开始删除模板数据！模板ID：" + id.ToString() + ",操作者：" + SessionHelper.GetSessionValue(DicCookieSession.EmployeeName) + ",操作者ID：" + SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                BaseResultDataValue baseResultDataValue = IBEMaintenanceData.TempletDataDelete(id, isDelTempletData);
                baseResultBool.success = baseResultDataValue.success;
                baseResultBool.ErrorInfo = baseResultDataValue.ErrorInfo;
                baseResultBool.BoolInfo = baseResultDataValue.ResultDataValue;
                return baseResultBool;
            }
            catch (Exception ex)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误：" + ex.Message;
                return baseResultBool;
            }
        }

        public BaseResultDataValue QMS_UDTO_DelETempletData(long templetID, string templetBatNo, string beginDate, string endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if ((!string.IsNullOrEmpty(beginDate)) && (!string.IsNullOrEmpty(endDate)))
                {
                    baseResultDataValue = IBEMaintenanceData.DeleteMaintenanceData(templetID, templetBatNo, beginDate, endDate);
                    ZhiFang.Common.Log.Log.Info("开始删除模板数据！模板ID：" + templetID.ToString() +
                            ",操作者：" + SessionHelper.GetSessionValue(DicCookieSession.EmployeeName) +
                            ",操作者ID：" + SessionHelper.GetSessionValue(DicCookieSession.EmployeeID));
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误：日期参数不能为空！";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public Stream QMS_UDTO_GetExcelTemplet(long templetID, int operateType)
        {
            FileStream tempFileStream = null;
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (!string.IsNullOrEmpty(employeeID))
                {
                    ETemplet templet = IBETemplet.Get(templetID);
                    if (templet != null && (!string.IsNullOrEmpty(templet.TempletPath)))
                    {
                        string parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath");
                        string tempFilePath = parentPath + templet.TempletPath;
                        string tempFileName = "Excel模板.xlsx";
                        if (!string.IsNullOrEmpty(templet.CName))
                            tempFileName = templet.CName + Path.GetExtension(tempFilePath);

                        tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                        if (operateType == 0) //下载文件
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + tempFileName);
                            //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                            //WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment;filename=" + tempFileName);
                        }
                        else if (operateType == 1)//直接打开PDF文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                            WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + tempFileName);
                            //System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                            //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + tempFileName);
                        }
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("QMS_UDTO_GetExcelTemplet：登录超时，请重新登录！");
                    throw new Exception("登录超时，请重新登录！");
                }
                return tempFileStream;
            }
            catch (Exception ex)
            {
                string strError = "QMS_UDTO_GetExcelTemplet异常：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("QMS_UDTO_GetExcelTemplet异常：" + ex.ToString());
                byte[] bError = Encoding.UTF8.GetBytes(strError);
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
        }

        public Stream QMS_UDTO_PreviewExcelTemplet(long templetID, int operateType)
        {
            FileStream tempFileStream = null;
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (!string.IsNullOrEmpty(employeeID))
                {
                    BaseResultDataValue baseResultDataValue = IBEMaintenanceData.PreviewExcelTemplet(templetID);
                    if (baseResultDataValue.success && (!string.IsNullOrEmpty(baseResultDataValue.ResultDataValue)))
                    {
                        string tempFilePath = baseResultDataValue.ResultDataValue;
                        string tempFileName = "报表预览.pdf";
                        //if (!string.IsNullOrEmpty(reportName))
                        //    tempFileName = reportName + ".pdf";
                        //else
                        //{
                        //    ETemplet templet = IBETemplet.Get(templetID);
                        //    if (templet != null)
                        //        tempFileName = templet.CName + ".pdf";
                        //}
                        tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                        if (operateType == 0) //下载文件
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + tempFileName);
                            //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                            //WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment;filename=" + tempFileName);
                        }
                        else if (operateType == 1)//直接打开PDF文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                            WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + tempFileName);
                            //System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                            //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + tempFileName);
                        }
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("QMS_UDTO_PreviewExcelTemplet异常：登录超时，请重新登录！");
                    throw new Exception("登录超时，请重新登录！");
                }
                return tempFileStream;
            }
            catch (Exception ex)
            {
                string strError = "QMS_UDTO_PreviewExcelTemplet异常：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("QMS_UDTO_PreviewExcelTemplet异常：" + ex.ToString());
                byte[] bError = Encoding.UTF8.GetBytes(strError);
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
        }

        public Stream QMS_UDTO_PreviewPdf(long templetID, string reportName, string beginDate, string endDate, string templetBatNo, int operateType, int isCheckPreview)
        {
            FileStream tempFileStream = null;
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (isCheckPreview == 1 && string.IsNullOrEmpty(employeeID))
                {
                    ZhiFang.Common.Log.Log.Error("QMS_UDTO_PreviewPdf异常：登录超时，请重新登录！");
                    throw new Exception("登录超时，请重新登录！");
                }
                else
                {
                    if (isCheckPreview == 0)
                        employeeID = "-1";

                    BaseResultDataValue baseResultDataValue = IBEMaintenanceData.ExcelToPdfFile(templetID, long.Parse(employeeID), beginDate, endDate, templetBatNo, true, "");
                    if (baseResultDataValue.success && (!string.IsNullOrEmpty(baseResultDataValue.ResultDataValue)))
                    {
                        string tempFilePath = baseResultDataValue.ResultDataValue;
                        string tempFileName = "报表预览.pdf";
                        //if (!string.IsNullOrEmpty(reportName))
                        //    tempFileName = reportName + ".pdf";
                        //else
                        //{
                        //    ETemplet templet = IBETemplet.Get(templetID);
                        //    if (templet != null)
                        //        tempFileName = templet.CName + ".pdf";
                        //}
                        tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                        if (operateType == 0) //下载文件
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + tempFileName);
                            //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                            //WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment;filename=" + tempFileName);
                        }
                        else if (operateType == 1)//直接打开PDF文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                            WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + tempFileName);
                            //System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                            //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + tempFileName);
                        }
                    }
                }
                return tempFileStream;
            }
            catch (Exception ex)
            {
                string strError = "QMS_UDTO_PreviewPdf异常：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("QMS_UDTO_PreviewPdf异常：" + ex.ToString());
                byte[] bError = Encoding.UTF8.GetBytes(strError);
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
        }

        public Stream QMS_UDTO_PreviewCheckPdf(long reportDataID, string reportName, int operateType)
        {
            FileStream tempFileStream = null;
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (!string.IsNullOrEmpty(employeeID))
                {
                    BaseResultDataValue baseResultDataValue = IBEReportData.QueryCheckPdfPath(reportDataID);
                    if (baseResultDataValue.success && (!string.IsNullOrEmpty(baseResultDataValue.ResultDataValue)))
                    {
                        string tempFilePath = baseResultDataValue.ResultDataValue;
                        string tempFileName = "报表预览.pdf";
                        //if (!string.IsNullOrEmpty(reportName))
                        //    tempFileName = reportName + ".pdf";
                        if (!File.Exists(tempFilePath))
                        {
                            ZhiFang.Common.Log.Log.Error("要浏览的文件不存在，文件路径：" + tempFilePath);
                            throw new Exception("要浏览的文件不存在，详细信息请查看日志！");
                        }
                        tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);
                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                        if (operateType == 0) //下载文件
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + tempFileName);
                            //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                            //WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment;filename=" + tempFileName);
                        }
                        else if (operateType == 1)//直接打开PDF文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                            WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + tempFileName);
                            //System.Web.HttpContext.Current.Response.ContentType = "application/pdf";
                            //System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "inline;filename=" + tempFileName);
                        }
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("QMS_UDTO_PreviewCheckPdf异常：登录超时，请重新登录！");
                    throw new Exception("登录超时，请重新登录！");
                }
                return tempFileStream;
            }
            catch (Exception ex)
            {
                string strError = "QMS_UDTO_PreviewCheckPdf异常：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("QMS_UDTO_PreviewCheckPdf异常：" + ex.ToString());
                byte[] bError = Encoding.UTF8.GetBytes(strError);
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
        }

        /// <summary>
        /// 查询近两月审核的质量记录列表
        /// </summary>
        /// <returns></returns>
        //public BaseResultDataValue QMS_UDTO_SearchWillCheckRecord(string beginDate, string endDate, string fields, bool isPlanish, int page, int limit)
        public BaseResultDataValue QMS_UDTO_SearchWillCheckRecord(int templetType, string templetID, string equipID, string beginDate, string endDate, int checkType, string otherPara, string fields, bool isPlanish, int page, int limit)

        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<PReportData> tempEntityList = new EntityList<PReportData>();
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (!string.IsNullOrEmpty(employeeID))
            {
                tempEntityList = IBEReportData.QueryCheckReportData(templetType, templetID, equipID, employeeID, beginDate, endDate, checkType, otherPara);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PReportData>(tempEntityList);
                    else
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempEntityList, fields);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "登录超时，请重新登录！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QMS_UDTO_CheckReport(long templetID, string beginDate, string endDate, string templetBatNo, string checkView)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string employeeName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            if (!string.IsNullOrEmpty(employeeID))
            {
                DateTime reportDate = DateTime.Parse(beginDate);
                //DateTime firstDay = new DateTime(reportDate.Year, reportDate.Month, 1);
                baseResultDataValue = IBEReportData.QueryReportIsChecked(templetID, reportDate);
                if (baseResultDataValue.success)
                {
                    //int dayMonth = DateTime.DaysInMonth(reportDate.Year, reportDate.Month);
                    //DateTime lastDay = new DateTime(reportDate.Year, reportDate.Month, dayMonth);
                    baseResultDataValue = IBEMaintenanceData.ExcelToPdfFile(templetID, long.Parse(employeeID), beginDate, endDate, templetBatNo, false, checkView);
                    if (baseResultDataValue.success && (!string.IsNullOrEmpty(baseResultDataValue.ResultDataValue)))
                    {
                        string parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath");
                        string filePath = baseResultDataValue.ResultDataValue.Replace(parentPath, "");
                        baseResultDataValue = IBEReportData.AddEReportData(templetID, reportDate, filePath, 1, employeeName, checkView);
                    }
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "登录超时，请重新登录！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QMS_UDTO_CheckReportCancel(long reportID, string cancelCheckView)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            string employeeName = SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            if (!string.IsNullOrEmpty(employeeID))
            {
                baseResultDataValue = IBEReportData.EditEReportDataCheckState(reportID, employeeID, employeeName, cancelCheckView);
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "登录超时，请重新登录！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QMS_UDTO_GetETempletByHRDeptID(string where, int limit, int page, bool isPlanish, string fields, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ETemplet> tempEntityList = new EntityList<ETemplet>();
            try
            {
                tempEntityList = IBETemplet.SearchETempletByHRDeptID(where, page, limit, CommonServiceMethod.GetSortHQL(sort));

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                if (isPlanish)
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ETemplet>(tempEntityList.list);
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
            return tempBaseResultDataValue;
        }

        public Stream QMS_UDTO_PreviewTempletAttachment(long eattachmentID, int operateType)
        {
            FileStream tempFileStream = null;
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (!string.IsNullOrEmpty(employeeID))
                {
                    EAttachment attachment = IBEAttachment.Get(eattachmentID);
                    if (attachment == null)
                    {
                        ZhiFang.Common.Log.Log.Info("无法根据附件ID获取附件信息,附件ID：" + eattachmentID.ToString());
                        return tempFileStream;
                    }
                    string attachmentName = attachment.FileName;
                    BaseResultDataValue baseResultDataValue = IBEAttachment.PreviewTempletAttachment(attachment);
                    if (baseResultDataValue.success && (!string.IsNullOrEmpty(baseResultDataValue.ResultDataValue)))
                    {
                        string tempFilePath = baseResultDataValue.ResultDataValue;
                        tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                        if (operateType == 0) //下载文件
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + attachmentName);
                        }
                        else if (operateType == 1)//直接打开PDF文件
                        {
                            string contentType = "application/pdf";
                            if (!string.IsNullOrEmpty(attachment.FileType))
                                contentType = attachment.FileType;
                            WebOperationContext.Current.OutgoingResponse.ContentType = contentType;
                            WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + attachmentName);
                        }
                    }
                }
                else
                {
                    ZhiFang.Common.Log.Log.Error("QMS_UDTO_PreviewTempletAttachment异常：登录超时，请重新登录！");
                    throw new Exception("登录超时，请重新登录！");
                }
                return tempFileStream;
            }
            catch (Exception ex)
            {
                string strError = "QMS_UDTO_PreviewTempletAttachment异常：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("QMS_UDTO_PreviewTempletAttachment异常：" + ex.ToString());
                byte[] bError = Encoding.UTF8.GetBytes(strError);
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Message QMS_UDTO_UploadTempletAttachment()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string entityJson = "";
                string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
                for (int i = 0; i < allkeys.Length; i++)
                {
                    switch (allkeys[i])
                    {
                        case "entityJson":
                            if (HttpContext.Current.Request.Form["entityJson"].Trim() != "")
                                entityJson = HttpContext.Current.Request.Form["entityJson"].Trim();
                            break;
                    }
                }
                EAttachment attachment = null;
                if (!string.IsNullOrEmpty(entityJson))//FFile实体参数的json串,序列化实体
                    attachment = ZhiFang.Common.Public.JsonSerializer.JsonDotNetDeserializeObject<EAttachment>(entityJson);
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (attachment != null && iTotal > 0)
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files[0];
                    int len = file.ContentLength;
                    if (len > 0)
                    {
                        string equipName = "OtherEquip";
                        if (attachment.ETemplet != null && attachment.ETemplet.Id > 0)
                        {
                            ETemplet templet = IBETemplet.Get(attachment.ETemplet.Id);
                            if (templet.EEquip != null && (!string.IsNullOrEmpty(templet.EEquip.CName)))
                                equipName = templet.EEquip.CName;
                        }

                        string fileName = "Attachment" + ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + Path.GetExtension(file.FileName);
                        attachment.FileName = fileName;
                        string parentPath = ZhiFang.Common.Public.ConfigHelper.GetConfigString("LabFilesPath");
                        string relativePath = "\\" + attachment.LabID.ToString() + "\\QMS\\Attachment\\" + equipName + DateTime.Now.ToString("yyyyMM") + "\\";
                        string fileDirectory = parentPath + relativePath;
                        if (!Directory.Exists(fileDirectory))
                        {
                            Directory.CreateDirectory(fileDirectory);
                        }
                        string filePathName = Path.Combine(fileDirectory, Path.GetFileName(fileName));
                        file.SaveAs(filePathName);
                        attachment.FilePath = relativePath + Path.GetFileName(fileName);
                    }
                    else
                    {
                        brdv.ErrorInfo = "文件大小为0或为空！";
                        brdv.success = false;
                    }
                }
                if (attachment != null)
                {
                    if (attachment.FileUploadDate != null)
                        attachment.FileUploadDate = DateTime.Parse(((DateTime)attachment.FileUploadDate).ToString("yyyy-MM-dd") + " " + DateTime.Now.ToString("HH:mm:ss"));
                    else
                        attachment.FileUploadDate = DateTime.Now;
                    attachment.DataAddTime = DateTime.Now;
                    IBEAttachment.AddEAttachment(attachment);
                }
                else
                {
                    brdv.ErrorInfo = "新增时实体为空或序列化失败！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = ex.Message;
                brdv.success = false;
                ZhiFang.Common.Log.Log.Error("QMS_UDTO_UploadTempletAttachment：" + ex.ToString());
            }
            string strResult = ZhiFang.ProjectProgressMonitorManage.Common.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        public BaseResultDataValue QMS_UDTO_SearchEReportDataByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<EReportData> tempEntityList = new EntityList<EReportData>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                    sort = CommonServiceMethod.GetSortHQL(sort);
                tempEntityList = IBEReportData.QueryEReportDataByHQL(where, sort, page, limit);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<EReportData>(tempEntityList);
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

        public BaseResultDataValue QMS_UDTO_SearchTempletByEmp(int relationType, long empID, string where, string resWhere, int page, int limit, string fields, string sort, bool isPlanish)
        {
            return QMS_UDTO_SearchTempletByEmpAndTempletDate(relationType, empID, DateTime.Now.ToString("yyyy-MM-dd"), where, resWhere, page, limit, fields, sort, isPlanish);
        }

        public BaseResultDataValue QMS_UDTO_SearchTempletByEmpAndTempletDate(int relationType, long empID, string templetDate, string where, string resWhere, int page, int limit, string fields, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<ETemplet> tempEntityList = new EntityList<ETemplet>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBETemplet.SearchTempletByEmp(relationType, empID, where, resWhere, page, limit, CommonServiceMethod.GetSortHQL(sort));
                }
                else
                {
                    tempEntityList = IBETemplet.SearchTempletByEmp(relationType, empID, where, resWhere, page, limit, "");
                }
                if (tempEntityList != null && tempEntityList.count > 0)
                {

                    DateTime tempDate = DateTime.Now;
                    if (!string.IsNullOrEmpty(templetDate))
                        tempDate = DateTime.Parse(templetDate);
                    DateTime beginDate = new DateTime(tempDate.Year, tempDate.Month, 1);
                    DateTime endDate = beginDate.AddMonths(1).AddDays(-1);
                    foreach (ETemplet entity in tempEntityList.list)
                    {
                        entity.IsCheck = IBEReportData.QueryReportIsChecked(entity.Id, beginDate).success ? 0 : 1;
                        entity.IsFillData = int.Parse(IBEMaintenanceData.JudgeTempletIsFillData(entity.Id, templetDate, beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")).ResultDataValue);
                    }
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<ETemplet>(tempEntityList);
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

        public BaseResultDataValue QMS_UDTO_GetTempletState(long templetID, string itemDate, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                DateTime tempDate = DateTime.Now;
                if (!string.IsNullOrEmpty(itemDate))
                    tempDate = DateTime.Parse(itemDate);
                DateTime beginDate = new DateTime(tempDate.Year, tempDate.Month, 1);
                DateTime endDate = beginDate.AddMonths(1).AddDays(-1);
                ETemplet templet = IBETemplet.Get(templetID);
                if (templet != null)
                {
                    templet.IsCheck = IBEReportData.QueryReportIsChecked(templetID, beginDate).success ? 0 : 1;
                    templet.IsFillData = int.Parse(IBEMaintenanceData.JudgeTempletIsFillData(templetID, itemDate, beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd")).ResultDataValue);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    try
                    {
                        if (isPlanish)
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<ETemplet>(templet);
                        }
                        else
                        {
                            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(templet, fields);
                        }
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        ZhiFang.Common.Log.Log.Error(tempBaseResultDataValue.ErrorInfo);
                        throw new Exception(ex.Message);
                    }
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

        public BaseResultDataValue QMS_UDTO_SearchTempletGroupData(long templetID, string beginDate, string endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if ((!string.IsNullOrEmpty(beginDate)) && (!string.IsNullOrEmpty(endDate)))
                {
                    baseResultDataValue = IBEMaintenanceData.TempletBatNoGroupData(templetID, beginDate, endDate);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：开始日期或结束日期不能为空！";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QMS_UDTO_SearchReportGroupData(long reportDataID, long templetID, string beginDate, string endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if ((!string.IsNullOrEmpty(beginDate)) && (!string.IsNullOrEmpty(endDate)))
                {
                    baseResultDataValue = IBEMaintenanceData.QueryReportGroupData(reportDataID, templetID, beginDate, endDate);
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "查询错误：开始日期或结束日期不能为空！";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        #endregion 定制服务

        #region 导出服务

        public Message QMS_UDTO_GetReportDetailExcelPath()
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
                        sort = "[{\"property\":\"HREmployee_CName\",\"direction\":\"ASC\"}]";
                    sort = CommonServiceMethod.GetSortHQL(sort);
                    tempFileName = "员工信息列表";
                    ds = IBHREmployee.GetHREmployeeInfoByID(idList, where, sort, basePath + "\\BaseTableXML\\Report_HREmployee.xml");
                }
                else if (reportType == "2")
                {
                    if (string.IsNullOrEmpty(sort))
                        sort = "[{\"property\":\"EEquip_EquipNo\",\"direction\":\"ASC\"}]";
                    sort = CommonServiceMethod.GetSortHQL(sort);
                    tempFileName = "仪器信息列表";
                    ds = IBEEquip.GetEquipInfoByID(idList, where, sort, basePath + "\\BaseTableXML\\Report_Equip.xml");
                }

                string excelName = ZhiFang.Common.Public.GUIDHelp.GetGUIDLong().ToString() + "." + ExcelDataCommon.GetExcelExtName();
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
                    if (!ExcelHelp.CreateExcelByNPOI(ds.Tables[0], headerText, tempFilePath))
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
                //throw new Exception(ex.Message);
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(baseResultDataValue);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        public Stream QMS_UDTO_DownLoadExcel(string fileName, string downFileName, int isUpLoadFile, int operateType)
        {
            FileStream tempFileStream = null;
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            try
            {
                string extName = Path.GetExtension(fileName);
                if (string.IsNullOrEmpty(downFileName))
                    downFileName = "错误信息文件" + extName;
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

    }
}
