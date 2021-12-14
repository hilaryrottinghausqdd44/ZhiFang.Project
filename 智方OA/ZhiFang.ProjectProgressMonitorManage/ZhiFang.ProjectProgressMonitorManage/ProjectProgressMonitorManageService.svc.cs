using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using ZhiFang.IBLL.ProjectProgressMonitorManage;
using ZhiFang.ProjectProgressMonitorManage.BusinessObject;
using ZhiFang.Entity.OA;
using Newtonsoft.Json.Linq;
using ZhiFang.Entity.OA.ViewObject.Response;

namespace ZhiFang.ProjectProgressMonitorManage
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ProjectProgressMonitorManageService : IProjectProgressMonitorManageService
    {
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPDict IBPDict { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPDictType IBPDictType { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPInteraction IBPInteraction { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPProjectAttachment IBPProjectAttachment { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPTask IBPTask { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPTaskCopyFor IBPTaskCopyFor { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPTaskOperLog IBPTaskOperLog { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPWorkDayLog IBPWorkDayLog { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPWorkLogCopyFor IBPWorkLogCopyFor { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPWorkLogSendFor IBPWorkLogSendFor { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPWorkMonthLog IBPWorkMonthLog { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPWorkWeekLog IBPWorkWeekLog { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPWorkLogInteraction IBPWorkLogInteraction { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPClient IBPClient { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPClientLinker IBPClientLinker { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPContract IBPContract { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPContractFollow IBPContractFollow { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPContractFollowInteraction IBPContractFollowInteraction { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPTaskTypeEmpLink IBPTaskTypeEmpLink { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBFFileAttachment IBFFileAttachment { get; set; }
        ZhiFang.IBLL.ProjectProgressMonitorManage.IBBDictTree IBBDictTree { get; set; }
        ZhiFang.IBLL.OA.IBBWeiXinPushMessageTemplate IBBWeiXinPushMessageTemplate { get; set; }
        //ZhiFang.ProjectProgressMonitorManage.upload.UEditorWCFUpload UEditorWCFUpload { get; set; }
        IBLL.ProjectProgressMonitorManage.IBPInvoice IBPInvoice { get; set; }
        IBLL.ProjectProgressMonitorManage.IBPFinanceReceive IBPFinanceReceive { get; set; }
        IBLL.ProjectProgressMonitorManage.IBPReceive IBPReceive { get; set; }
        IBLL.ProjectProgressMonitorManage.IBPReceivePlan IBPReceivePlan { get; set; }
        IBLL.ProjectProgressMonitorManage.IBBParameter IBBParameter { get; set; }
        IBLL.ProjectProgressMonitorManage.IBPRepayment IBPRepayment { get; set; }
        IBLL.ProjectProgressMonitorManage.IBPLoanBill IBPLoanBill { get; set; }
        IBLL.ProjectProgressMonitorManage.IBPEmpFinanceAccount IBPEmpFinanceAccount { get; set; }
        IBLL.ProjectProgressMonitorManage.IBPExpenseAccount IBPExpenseAccount { get; set; }
        IBLL.ProjectProgressMonitorManage.IBPCustomerServiceOperation IBPCustomerServiceOperation { get; set; }
        IBLL.ProjectProgressMonitorManage.IBPCustomerServiceAttachment IBPCustomerServiceAttachment { get; set; }
        IBLL.ProjectProgressMonitorManage.IBPCustomerService IBPCustomerService { get; set; }
        IBLL.ProjectProgressMonitorManage.IBPSalesManClientLink IBPSalesManClientLink { get; set; }
        IBLL.ProjectProgressMonitorManage.IBPCustomerServiceOperationLog IBPCustomerServiceOperationLog { get; set; }
        ZhiFang.IBLL.OA.IBAHOperation IBAHOperation { get; set; }
        ZhiFang.IBLL.OA.IBAHServerEquipLicence IBAHServerEquipLicence { get; set; }
        ZhiFang.IBLL.OA.IBAHServerLicence IBAHServerLicence { get; set; }
        ZhiFang.IBLL.OA.IBAHServerProgramLicence IBAHServerProgramLicence { get; set; }
        ZhiFang.IBLL.OA.IBAHSingleLicence IBAHSingleLicence { get; set; }

        ZhiFang.IBLL.OA.IBATHolidaySetting IBATHolidaySetting { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPProject IBPProject { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPProjectTask IBPProjectTask { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPProjectTaskProgress IBPProjectTaskProgress { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBPProjectDocument IBPProjectDocument { get; set; }

        ZhiFang.IBLL.ProjectProgressMonitorManage.IBCUser IBCUser { get; set; }

        #region PDict
        //Add  PDict
        public BaseResultDataValue ST_UDTO_AddPDict(PDict entity)
        {
            IBPDict.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPDict.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPDict.Get(IBPDict.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPDict.Entity);
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
        //Update  PDict
        public BaseResultBool ST_UDTO_UpdatePDict(PDict entity)
        {
            IBPDict.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPDict.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PDict
        public BaseResultBool ST_UDTO_UpdatePDictByField(PDict entity, string fields)
        {
            IBPDict.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPDict.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPDict.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPDict.Edit();
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
        //Delele  PDict
        public BaseResultBool ST_UDTO_DelPDict(long longPDictID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPDict.Remove(longPDictID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPDict(PDict entity)
        {
            IBPDict.Entity = entity;
            EntityList<PDict> tempEntityList = new EntityList<PDict>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPDict.Search();
                tempEntityList.count = IBPDict.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PDict>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPDictByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PDict> tempEntityList = new EntityList<PDict>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPDict.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPDict.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PDict>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPDictById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPDict.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PDict>(tempEntity);
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

        #region PDictType
        //Add  PDictType
        public BaseResultDataValue ST_UDTO_AddPDictType(PDictType entity)
        {
            IBPDictType.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPDictType.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPDictType.Get(IBPDictType.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPDictType.Entity);
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
        //Update  PDictType
        public BaseResultBool ST_UDTO_UpdatePDictType(PDictType entity)
        {
            IBPDictType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPDictType.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PDictType
        public BaseResultBool ST_UDTO_UpdatePDictTypeByField(PDictType entity, string fields)
        {
            IBPDictType.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPDictType.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPDictType.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPDictType.Edit();
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
        //Delele  PDictType
        public BaseResultBool ST_UDTO_DelPDictType(long longPDictTypeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPDictType.Remove(longPDictTypeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPDictType(PDictType entity)
        {
            IBPDictType.Entity = entity;
            EntityList<PDictType> tempEntityList = new EntityList<PDictType>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPDictType.Search();
                tempEntityList.count = IBPDictType.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PDictType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPDictTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PDictType> tempEntityList = new EntityList<PDictType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPDictType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPDictType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PDictType>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPDictTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPDictType.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PDictType>(tempEntity);
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

        #region PInteraction
        //Add  PInteraction
        public BaseResultDataValue ST_UDTO_AddPInteraction(PInteraction entity)
        {
            IBPInteraction.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPInteraction.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPInteraction.Get(IBPInteraction.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPInteraction.Entity);
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
        //Update  PInteraction
        public BaseResultBool ST_UDTO_UpdatePInteraction(PInteraction entity)
        {
            IBPInteraction.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPInteraction.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PInteraction
        public BaseResultBool ST_UDTO_UpdatePInteractionByField(PInteraction entity, string fields)
        {
            IBPInteraction.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPInteraction.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPInteraction.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPInteraction.Edit();
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
        //Delele  PInteraction
        public BaseResultBool ST_UDTO_DelPInteraction(long longPInteractionID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPInteraction.Remove(longPInteractionID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPInteraction(PInteraction entity)
        {
            IBPInteraction.Entity = entity;
            EntityList<PInteraction> tempEntityList = new EntityList<PInteraction>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPInteraction.Search();
                tempEntityList.count = IBPInteraction.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PInteraction>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PInteraction> tempEntityList = new EntityList<PInteraction>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPInteraction.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPInteraction.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PInteraction>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPInteractionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPInteraction.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PInteraction>(tempEntity);
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

        #region PProjectAttachment
        //Add  PProjectAttachment
        public BaseResultDataValue ST_UDTO_AddPProjectAttachment(PProjectAttachment entity)
        {
            IBPProjectAttachment.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPProjectAttachment.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPProjectAttachment.Get(IBPProjectAttachment.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPProjectAttachment.Entity);
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
        //Update  PProjectAttachment
        public BaseResultBool ST_UDTO_UpdatePProjectAttachment(PProjectAttachment entity)
        {
            IBPProjectAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPProjectAttachment.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PProjectAttachment
        public BaseResultBool ST_UDTO_UpdatePProjectAttachmentByField(PProjectAttachment entity, string fields)
        {
            IBPProjectAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPProjectAttachment.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPProjectAttachment.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPProjectAttachment.Edit();
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
        //Delele  PProjectAttachment
        public BaseResultBool ST_UDTO_DelPProjectAttachment(long longPProjectAttachmentID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPProjectAttachment.Remove(longPProjectAttachmentID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPProjectAttachment(PProjectAttachment entity)
        {
            IBPProjectAttachment.Entity = entity;
            EntityList<PProjectAttachment> tempEntityList = new EntityList<PProjectAttachment>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPProjectAttachment.Search();
                tempEntityList.count = IBPProjectAttachment.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PProjectAttachment>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPProjectAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PProjectAttachment> tempEntityList = new EntityList<PProjectAttachment>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPProjectAttachment.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPProjectAttachment.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PProjectAttachment>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPProjectAttachmentById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPProjectAttachment.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PProjectAttachment>(tempEntity);
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

        #region PTask
        //Add  PTask
        public BaseResultDataValue ST_UDTO_AddPTask(PTask entity)
        {
            IBPTask.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (entity.Status == null)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：任务状态为空！";
                    return tempBaseResultDataValue;
                }
                tempBaseResultDataValue.success = IBPTask.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPTask.Get(IBPTask.Entity.Id);
                    //BasePage.PushMessageTemplateContext
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPTask.Entity);
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
        //Add  PTask
        public BaseResultDataValue ST_UDTO_PTaskAdd(PTask entity)
        {
            IBPTask.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (entity.Status == null)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "错误信息：任务状态为空！";
                    return tempBaseResultDataValue;
                }
                tempBaseResultDataValue.success = IBPTask.PTaskAdd((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction);
                if (tempBaseResultDataValue.success)
                {
                    IBPTask.Get(IBPTask.Entity.Id);
                    //BasePage.PushMessageTemplateContext
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPTask.Entity);
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

        //Update  PTask
        public BaseResultBool ST_UDTO_UpdatePTask(PTask entity)
        {
            IBPTask.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPTask.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PTask
        public BaseResultBool ST_UDTO_UpdatePTaskByField(PTask entity, string fields)
        {
            IBPTask.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPTask.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPTask.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPTask.Edit();
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

        //Update  PTask
        public BaseResultBool ST_UDTO_UpdatePTaskStatusByField(PTask entity, string fields)
        {
            IBPTask.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if (entity.Id <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：任务ID为空！";
                    return tempBaseResultBool;
                }
                if (entity.Status == null)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：任务状态为空！";
                    return tempBaseResultBool;
                }
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPTask.Entity, fields);
                    if (tempArray != null)
                    {
                        string ErrorInfo;
                        long EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                        string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                        tempBaseResultBool.success = IBPTask.PTaskStatusUpdate((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, IBPTask.Entity, tempArray, EmpID, EmpName, out ErrorInfo);
                        if (!tempBaseResultBool.success)
                        {
                            tempBaseResultBool.ErrorInfo = ErrorInfo;
                        }
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPTask.Edit();
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
        //Delele  PTask
        public BaseResultBool ST_UDTO_DelPTask(long longPTaskID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPTask.Remove(longPTaskID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPTask(PTask entity)
        {
            IBPTask.Entity = entity;
            EntityList<PTask> tempEntityList = new EntityList<PTask>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPTask.Search();
                tempEntityList.count = IBPTask.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PTask>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPTaskByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PTask> tempEntityList = new EntityList<PTask>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPTask.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPTask.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PTask>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPTaskById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPTask.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PTask>(tempEntity);
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

        public BaseResultDataValue ST_UDTO_AdvSearchPTask(Task_Search entity)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PTask> tempEntityList = new EntityList<PTask>();
            EntityList<Task_Search_ResultVO> Task_Search_ResultVOEntityList = new EntityList<Task_Search_ResultVO>();
            try
            {
                if ((entity.Sort != null) && (entity.Sort.Length > 0))
                {
                    tempEntityList = IBPTask.SearchListByEntity(entity, long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID)), CommonServiceMethod.GetSortHQL(entity.Sort), entity.Page, entity.Limit);
                }
                else
                {
                    tempEntityList = IBPTask.SearchListByEntity(entity, long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID)), entity.Page, entity.Limit);
                }

                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(entity.Fields);
                try
                {
                    if (tempEntityList != null && tempEntityList.count > 0)
                    {
                        Task_Search_ResultVOEntityList.count = tempEntityList.count;
                        Task_Search_ResultVOEntityList.list = new List<Task_Search_ResultVO>();
                        for (int i = 0; i < tempEntityList.list.Count; i++)
                        {
                            Task_Search_ResultVO tsrvo = new Task_Search_ResultVO();
                            if (tempEntityList.list[i] != null)
                            {
                                PTask pt = tempEntityList.list[i];
                                #region 赋值一级
                                tsrvo.IdString = pt.Id.ToString();
                                ZhiFang.Common.Log.Log.Debug("IdString:" + tsrvo.IdString + "@@@pt.Id.ToString():" + pt.Id.ToString());
                                tsrvo.CheckerID = pt.CheckerID;
                                tsrvo.CheckerName = pt.CheckerName;
                                tsrvo.CName = pt.CName;
                                tsrvo.Contents = pt.Contents;
                                tsrvo.DataAddTime = pt.DataAddTime;
                                tsrvo.DataTimeStamp = pt.DataTimeStamp;

                                if (pt.EfficiencyEval != null)
                                {
                                    tsrvo.EfficiencyEval = pt.EfficiencyEval.Id;
                                }
                                else
                                {
                                    tsrvo.EfficiencyEval = null;
                                }
                                tsrvo.EfficiencyEvalName = pt.EfficiencyEvalName;

                                tsrvo.ReqEndTime = pt.ReqEndTime;
                                tsrvo.EndTime = pt.EndTime;
                                tsrvo.EstiEndTime = pt.EstiEndTime;
                                tsrvo.EstiStartTime = pt.EstiStartTime;
                                tsrvo.EstiWorkload = pt.EstiWorkload;
                                tsrvo.ExecutAddr = pt.ExecutAddr;

                                if (pt.ExecutorID != null)
                                {
                                    tsrvo.ExecutorID = pt.ExecutorID;
                                }
                                else
                                {
                                    tsrvo.ExecutorID = null;
                                }
                                tsrvo.ExecutorName = pt.ExecutorName;

                                if (pt.ExecutType != null)
                                {
                                    tsrvo.ExecutType = pt.ExecutType.Id;
                                }
                                else
                                {
                                    tsrvo.ExecutType = null;
                                }
                                tsrvo.ExecutTypeName = pt.ExecutTypeName;

                                tsrvo.ExtraMsg = pt.ExtraMsg;
                                tsrvo.Id = pt.Id;
                                tsrvo.IsUse = pt.IsUse;
                                tsrvo.LabID = pt.LabID;
                                tsrvo.Memo = pt.Memo;
                                tsrvo.OtherMsg = pt.OtherMsg;

                                if (pt.Pace != null)
                                {
                                    tsrvo.Pace = pt.Pace.Id;
                                }
                                else
                                {
                                    tsrvo.Pace = null;
                                }
                                tsrvo.PaceName = pt.PaceName;

                                if (pt.PaceEval != null)
                                {
                                    tsrvo.PaceEval = pt.PaceEval.Id;
                                }
                                else
                                {
                                    tsrvo.PaceEval = null;
                                }
                                tsrvo.PaceEvalName = pt.PaceEvalName;

                                if (pt.PClient != null)
                                {
                                    tsrvo.PClient = pt.PClient.Id;
                                }
                                else
                                {
                                    tsrvo.PClient = null;
                                }

                                if (pt.PTaskID != null)
                                {
                                    tsrvo.PProject = pt.PTaskID;
                                }
                                else
                                {
                                    tsrvo.PProject = null;
                                }

                                tsrvo.ProjectName = pt.PTaskCName;
                                tsrvo.PublisherID = pt.PublisherID;
                                tsrvo.PublisherName = pt.PublisherName;

                                tsrvo.ApplyID = pt.ApplyID;
                                tsrvo.ApplyName = pt.ApplyName;

                                tsrvo.OneAuditID = pt.OneAuditID;
                                tsrvo.OneAuditName = pt.OneAuditName;

                                tsrvo.TwoAuditID = pt.TwoAuditID;
                                tsrvo.TwoAuditName = pt.TwoAuditName;

                                if (pt.QualityEval != null)
                                {
                                    tsrvo.QualityEval = pt.QualityEval.Id;
                                }
                                else
                                {
                                    tsrvo.QualityEval = null;
                                }
                                tsrvo.QualityEvalName = pt.QualityEvalName;

                                tsrvo.StartTime = pt.StartTime;

                                if (pt.Status != null)
                                {
                                    tsrvo.Status = pt.Status.Id;
                                }
                                else
                                {
                                    tsrvo.Status = null;
                                }
                                tsrvo.StatusName = pt.StatusName;

                                if (pt.TeamworkEval != null)
                                {
                                    tsrvo.TeamworkEval = pt.TeamworkEval.Id;
                                }
                                else
                                {
                                    tsrvo.TeamworkEval = null;
                                }
                                tsrvo.TeamworkEvalName = pt.TeamworkEvalName;

                                if (pt.TotalityEval != null)
                                {
                                    tsrvo.TotalityEval = pt.TotalityEval.Id;
                                }
                                else
                                {
                                    tsrvo.TotalityEval = null;
                                }
                                tsrvo.TotalityEvalName = pt.TotalityEvalName;

                                if (pt.TypeID != null)
                                {
                                    tsrvo.TypeID = pt.TypeID;
                                }
                                else
                                {
                                    tsrvo.TypeID = null;
                                }
                                tsrvo.TypeName = pt.TypeName;

                                if (pt.PTypeID != null)
                                {
                                    tsrvo.PTypeID = pt.PTypeID;
                                }
                                else
                                {
                                    tsrvo.PTypeID = null;
                                }
                                tsrvo.PTypeName = pt.PTypeName;

                                if (pt.TypeID != null)
                                {
                                    tsrvo.MTypeID = pt.MTypeID;
                                }
                                else
                                {
                                    tsrvo.MTypeID = null;
                                }
                                tsrvo.MTypeName = pt.MTypeName;

                                if (pt.Urgency != null)
                                {
                                    tsrvo.Urgency = pt.Urgency.Id;
                                }
                                else
                                {
                                    tsrvo.Urgency = null;
                                }
                                tsrvo.UrgencyName = pt.UrgencyName;

                                tsrvo.Workload = pt.Workload;
                                #endregion
                                #region 赋值二级
                                if (pt.PProjectAttachmentList != null && pt.PProjectAttachmentList.Count > 0)
                                {
                                    for (int ppalisti = 0; ppalisti < pt.PProjectAttachmentList.Count; ppalisti++)
                                    {
                                        PProjectAttachmentVO ppavo = new PProjectAttachmentVO();
                                        if (pt.PProjectAttachmentList[ppalisti] != null && pt.PProjectAttachmentList[ppalisti].IsUse)
                                        {
                                            ppavo.CreatorID = pt.PProjectAttachmentList[ppalisti].CreatorID;
                                            ppavo.CreatorName = pt.PProjectAttachmentList[ppalisti].CreatorName;
                                            ppavo.FileExt = pt.PProjectAttachmentList[ppalisti].FileExt;
                                            ppavo.FileName = pt.PProjectAttachmentList[ppalisti].FileName;
                                            ppavo.FileSize = pt.PProjectAttachmentList[ppalisti].FileSize;
                                            ppavo.Id = pt.PProjectAttachmentList[ppalisti].Id;
                                            ppavo.LabID = pt.PProjectAttachmentList[ppalisti].LabID;
                                            tsrvo.PProjectAttachmentList.Add(ppavo);
                                        }
                                    }
                                }
                                if (pt.PTaskCopyForList != null && pt.PTaskCopyForList.Count > 0)
                                {
                                    for (int ptcdlisti = 0; ptcdlisti < pt.PTaskCopyForList.Count; ptcdlisti++)
                                    {
                                        PTaskCopyForVO ptcfvo = new PTaskCopyForVO();
                                        if (pt.PTaskCopyForList[ptcdlisti] != null)
                                        {
                                            ptcfvo.CopyForEmpID = pt.PTaskCopyForList[ptcdlisti].CopyForEmpID;
                                            ptcfvo.CopyForEmpName = pt.PTaskCopyForList[ptcdlisti].CopyForEmpName;
                                            ptcfvo.DataAddTime = pt.PTaskCopyForList[ptcdlisti].DataAddTime;
                                            ptcfvo.DataUpdateTime = pt.PTaskCopyForList[ptcdlisti].DataUpdateTime;
                                            ptcfvo.Id = pt.PTaskCopyForList[ptcdlisti].Id;
                                            ptcfvo.LabID = pt.PTaskCopyForList[ptcdlisti].LabID;
                                            tsrvo.PTaskCopyForList.Add(ptcfvo);
                                        }
                                    }
                                }
                                #endregion
                                Task_Search_ResultVOEntityList.list.Add(tsrvo);
                            }
                        }
                    }
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(Task_Search_ResultVOEntityList);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_AdvSearchPTask序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_AdvSearchPTask查询错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        #endregion

        #region PTaskCopyFor
        //Add  PTaskCopyFor
        public BaseResultDataValue ST_UDTO_AddPTaskCopyFor(PTaskCopyFor entity)
        {
            IBPTaskCopyFor.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPTaskCopyFor.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPTaskCopyFor.Get(IBPTaskCopyFor.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPTaskCopyFor.Entity);
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
        //Update  PTaskCopyFor
        public BaseResultBool ST_UDTO_UpdatePTaskCopyFor(PTaskCopyFor entity)
        {
            IBPTaskCopyFor.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPTaskCopyFor.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PTaskCopyFor
        public BaseResultBool ST_UDTO_UpdatePTaskCopyForByField(PTaskCopyFor entity, string fields)
        {
            IBPTaskCopyFor.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPTaskCopyFor.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPTaskCopyFor.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPTaskCopyFor.Edit();
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
        //Delele  PTaskCopyFor
        public BaseResultBool ST_UDTO_DelPTaskCopyFor(long longPTaskCopyForID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPTaskCopyFor.Remove(longPTaskCopyForID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPTaskCopyFor(PTaskCopyFor entity)
        {
            IBPTaskCopyFor.Entity = entity;
            EntityList<PTaskCopyFor> tempEntityList = new EntityList<PTaskCopyFor>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPTaskCopyFor.Search();
                tempEntityList.count = IBPTaskCopyFor.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PTaskCopyFor>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPTaskCopyForByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PTaskCopyFor> tempEntityList = new EntityList<PTaskCopyFor>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPTaskCopyFor.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPTaskCopyFor.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PTaskCopyFor>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPTaskCopyForById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPTaskCopyFor.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PTaskCopyFor>(tempEntity);
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

        #region PTaskOperLog
        //Add  PTaskOperLog
        public BaseResultDataValue ST_UDTO_AddPTaskOperLog(PTaskOperLog entity)
        {
            IBPTaskOperLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPTaskOperLog.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPTaskOperLog.Get(IBPTaskOperLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPTaskOperLog.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.ToString();
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  PTaskOperLog
        public BaseResultBool ST_UDTO_UpdatePTaskOperLog(PTaskOperLog entity)
        {
            IBPTaskOperLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPTaskOperLog.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PTaskOperLog
        public BaseResultBool ST_UDTO_UpdatePTaskOperLogByField(PTaskOperLog entity, string fields)
        {
            IBPTaskOperLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPTaskOperLog.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPTaskOperLog.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPTaskOperLog.Edit();
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
        //Delele  PTaskOperLog
        public BaseResultBool ST_UDTO_DelPTaskOperLog(long longPTaskOperLogID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPTaskOperLog.Remove(longPTaskOperLogID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPTaskOperLog(PTaskOperLog entity)
        {
            IBPTaskOperLog.Entity = entity;
            EntityList<PTaskOperLog> tempEntityList = new EntityList<PTaskOperLog>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPTaskOperLog.Search();
                tempEntityList.count = IBPTaskOperLog.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PTaskOperLog>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPTaskOperLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PTaskOperLog> tempEntityList = new EntityList<PTaskOperLog>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPTaskOperLog.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPTaskOperLog.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PTaskOperLog>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPTaskOperLogById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPTaskOperLog.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PTaskOperLog>(tempEntity);
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

        #region PWorkDayLog
        //Add  PWorkDayLog
        public BaseResultDataValue ST_UDTO_AddPWorkDayLog(PWorkDayLog entity)
        {
            IBPWorkDayLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPWorkDayLog.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPWorkDayLog.Get(IBPWorkDayLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPWorkDayLog.Entity);
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
        public BaseResultDataValue ST_UDTO_AddPWorkDayLogByWeiXin(PWorkDayLog entity, List<string> AttachmentUrlList)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddPWorkDayLogByWeiXin.LabID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddPWorkDayLogByWeiXin.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag));

            IBPWorkDayLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                IBPWorkDayLog.Entity.DateCode = DateTime.Now.ToString("yyyy-MM-dd");
                IBPWorkDayLog.Entity.EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                IBPWorkDayLog.Entity.EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                #region 图片
                if (AttachmentUrlList.Count >= 5)
                {
                    if (AttachmentUrlList[0] != null && AttachmentUrlList[0].Trim() != "")
                    {
                        IBPWorkDayLog.Entity.Image1 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[0]);
                    }

                    if (AttachmentUrlList[1] != null && AttachmentUrlList[1].Trim() != "")
                    {
                        IBPWorkDayLog.Entity.Image2 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[1]);
                    }

                    if (AttachmentUrlList[2] != null && AttachmentUrlList[2].Trim() != "")
                    {
                        IBPWorkDayLog.Entity.Image3 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[2]);
                    }

                    if (AttachmentUrlList[3] != null && AttachmentUrlList[3].Trim() != "")
                    {
                        IBPWorkDayLog.Entity.Image4 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[3]);
                    }

                    if (AttachmentUrlList[4] != null && AttachmentUrlList[4].Trim() != "")
                    {
                        IBPWorkDayLog.Entity.Image5 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[4]);
                    }
                }
                #endregion
                tempBaseResultDataValue.success = IBPWorkDayLog.AddPWorkDayLogByWeiXin(AttachmentUrlList);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_AddPWorkDayLogByWeiXin.错误信息：" + ex.ToString());
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  PWorkDayLog
        public BaseResultBool ST_UDTO_UpdatePWorkDayLog(PWorkDayLog entity)
        {
            IBPWorkDayLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPWorkDayLog.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PWorkDayLog
        public BaseResultBool ST_UDTO_UpdatePWorkDayLogByField(PWorkDayLog entity, string fields)
        {
            IBPWorkDayLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPWorkDayLog.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPWorkDayLog.UpdatePWorkDayLogByField(entity, tempArray);
                        //tempBaseResultBool.success = IBPWorkDayLog.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPWorkDayLog.Edit();
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
        //Delele  PWorkDayLog
        public BaseResultBool ST_UDTO_DelPWorkDayLog(long longPWorkDayLogID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPWorkDayLog.Remove(longPWorkDayLogID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPWorkDayLog(PWorkDayLog entity)
        {
            IBPWorkDayLog.Entity = entity;
            EntityList<PWorkDayLog> tempEntityList = new EntityList<PWorkDayLog>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPWorkDayLog.Search();
                tempEntityList.count = IBPWorkDayLog.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PWorkDayLog>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPWorkDayLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PWorkDayLog> tempEntityList = new EntityList<PWorkDayLog>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPWorkDayLog.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPWorkDayLog.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PWorkDayLog>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPWorkDayLogById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPWorkDayLog.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PWorkDayLog>(tempEntity);
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

        public BaseResultDataValue ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType(string sd, string ed, int page, int limit, string sendtype, string worklogtype, string sort, string empid)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType,sd=" + sd + "@ed=" + ed + "page=" + page + "@limit=" + limit + "@sendtype=" + sendtype + "@worklogtype=" + worklogtype + "@sort=" + sort + "@empid=" + empid);


                if (ed == null || ed.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType,ed为空!");
                }
                if (sd == null || sd.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType,sd为空!");
                }
                if (empid == null || empid.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType ,empid!默认为当前登录者。");
                    empid = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
                }
                IList<WorkLogVO> worklogvolist = IBPWorkDayLog.SearchPWorkDayLogBySendTypeAndWorkLogType(sd, ed, page, limit, sendtype, worklogtype, sort, long.Parse(empid), long.Parse(Cookie.CookieHelper.Read(DicCookieSession.EmployeeID)));
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
                    tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogBySendTypeAndWorkLogType错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchTaskWorkDayLogTaskId(string sd, string ed, int page, int limit, string taskid, string sort, string empid)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchTaskWorkDayLogTaskId,sd=" + sd + "@ed=" + ed + "page=" + page + "@limit=" + limit + "@sort=" + sort + "@empid=" + empid + "@taskid=" + taskid);


                if (ed == null || ed.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchTaskWorkDayLogTaskId,ed为空!");
                }
                if (sd == null || sd.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchTaskWorkDayLogTaskId,sd为空!");
                }
                if (taskid == null || taskid.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchTaskWorkDayLogTaskId,taskid为空!");
                }
                if (empid == null || empid.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchTaskWorkDayLogTaskId ,empid!默认为当前登录者。");
                    empid = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
                }
                IList<WorkLogVO> worklogvolist = IBPWorkDayLog.SearchTaskWorkDayLogTaskId(sd, ed, page, limit, sort, long.Parse(empid), long.Parse(Cookie.CookieHelper.Read(DicCookieSession.EmployeeID)), long.Parse(taskid));
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
                    tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchTaskWorkDayLogTaskId序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchTaskWorkDayLogTaskId序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchTaskWorkDayLogTaskId错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchTaskWorkDayLogTaskId错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchWorkDayLogByIdAndWorkLogType(long Id, string worklogtype)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchWorkDayLogByIdAndWorkLogType,Id=" + Id + "@worklogtype=" + worklogtype);

                if (Id <= 0)
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchWorkDayLogByIdAndWorkLogType,Id无效！" + Id);
                }
                if (worklogtype == null || worklogtype.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchWorkDayLogByIdAndWorkLogType,worklogtype为空!");
                }

                WorkLogVO worklogvolist = IBPWorkDayLog.ST_UDTO_SearchWorkDayLogByIdAndWorkLogType(Id, worklogtype);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(worklogvolist);
                    tempBaseResultDataValue.success = true;
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchWorkDayLogByIdAndWorkLogType序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchWorkDayLogByIdAndWorkLogType序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchWorkDayLogByIdAndWorkLogType错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchWorkDayLogByIdAndWorkLogType错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_WorkDayAddLikeCountLogByIdAndWorkLogType(long Id, string worklogtype)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_WorkDayAddLikeCountLogByIdAndWorkLogType,Id=" + Id + "@worklogtype=" + worklogtype);

                if (Id <= 0)
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_WorkDayAddLikeCountLogByIdAndWorkLogType,Id无效！" + Id);
                }
                if (worklogtype == null || worklogtype.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_WorkDayAddLikeCountLogByIdAndWorkLogType,worklogtype为空!");
                }

                int result = IBPWorkDayLog.WorkDayAddLikeCountLogByIdAndWorkLogType(Id, worklogtype);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(result > 0);
                    tempBaseResultDataValue.success = true;
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "ST_UDTO_WorkDayAddLikeCountLogByIdAndWorkLogType：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_WorkDayAddLikeCountLogByIdAndWorkLogType：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "ST_UDTO_WorkDayAddLikeCountLogByIdAndWorkLogType：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_WorkDayAddLikeCountLogByIdAndWorkLogType：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType(string sd, string ed, int page, int limit, string deptid, string worklogtype, string sort, string empid, bool isincludesubdept)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType,sd=" + sd + "@ed=" + ed + "page=" + page + "@limit=" + limit + "@DeptId=" + deptid + "@worklogtype=" + worklogtype + "@sort=" + sort + "@empid=" + empid + "@isincludesubdept=" + isincludesubdept);


                if (ed == null || ed.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType,ed为空!");
                }
                if (sd == null || sd.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType,sd为空!");
                }
                if (empid == null || empid.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType ,empid!默认为当前登录者。");
                    empid = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
                }
                if (deptid == null || deptid.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType ,deptid为空。");
                    deptid = "0";
                }
                IList<WorkLogVO> worklogvolist = IBPWorkDayLog.SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType(sd, ed, page, limit, long.Parse(deptid), worklogtype, sort, long.Parse(empid), long.Parse(Cookie.CookieHelper.Read(DicCookieSession.EmployeeID)), isincludesubdept);
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
                    tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogByDeptIdOrEmpIdAndWorkLogType错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchPWorkDayLogByEmpId(string monthday, string empid, bool isincludesubdept)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogByEmpId,monthday=" + monthday + "@empid=" + empid + "@isincludesubdept=" + isincludesubdept);


                if (monthday == null || monthday.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogByEmpId,monthday为空!");
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchPWorkDayLogByEmpId,monthday为空!";
                    return tempBaseResultDataValue;
                }
                if (empid == null || empid.Trim() == "")
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogByEmpId ,empid为空!");
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchPWorkDayLogByEmpId ,empid为空!";
                    return tempBaseResultDataValue;
                }
                IList<WorkLogVO> worklogvolist = IBPWorkDayLog.SearchPWorkDayLogByEmpId(monthday, long.Parse(empid), isincludesubdept);
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
                    tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchPWorkDayLogByEmpId序列化错误：" + ex.Message;
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogByEmpId序列化错误：" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "ST_UDTO_SearchPWorkDayLogByEmpId错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchPWorkDayLogByEmpId错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        #endregion

        #region PWorkLogCopyFor
        //Add  PWorkLogCopyFor
        public BaseResultDataValue ST_UDTO_AddPWorkLogCopyFor(PWorkLogCopyFor entity)
        {
            IBPWorkLogCopyFor.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPWorkLogCopyFor.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPWorkLogCopyFor.Get(IBPWorkLogCopyFor.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPWorkLogCopyFor.Entity);
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
        //Update  PWorkLogCopyFor
        public BaseResultBool ST_UDTO_UpdatePWorkLogCopyFor(PWorkLogCopyFor entity)
        {
            IBPWorkLogCopyFor.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPWorkLogCopyFor.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PWorkLogCopyFor
        public BaseResultBool ST_UDTO_UpdatePWorkLogCopyForByField(PWorkLogCopyFor entity, string fields)
        {
            IBPWorkLogCopyFor.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPWorkLogCopyFor.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPWorkLogCopyFor.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPWorkLogCopyFor.Edit();
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
        //Delele  PWorkLogCopyFor
        public BaseResultBool ST_UDTO_DelPWorkLogCopyFor(long longPWorkLogCopyForID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPWorkLogCopyFor.Remove(longPWorkLogCopyForID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPWorkLogCopyFor(PWorkLogCopyFor entity)
        {
            IBPWorkLogCopyFor.Entity = entity;
            EntityList<PWorkLogCopyFor> tempEntityList = new EntityList<PWorkLogCopyFor>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPWorkLogCopyFor.Search();
                tempEntityList.count = IBPWorkLogCopyFor.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PWorkLogCopyFor>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPWorkLogCopyForByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PWorkLogCopyFor> tempEntityList = new EntityList<PWorkLogCopyFor>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPWorkLogCopyFor.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPWorkLogCopyFor.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PWorkLogCopyFor>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPWorkLogCopyForById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPWorkLogCopyFor.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PWorkLogCopyFor>(tempEntity);
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

        #region PWorkLogSendFor
        //Add  PWorkLogSendFor
        public BaseResultDataValue ST_UDTO_AddPWorkLogSendFor(PWorkLogSendFor entity)
        {
            IBPWorkLogSendFor.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPWorkLogSendFor.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPWorkLogSendFor.Get(IBPWorkLogSendFor.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPWorkLogSendFor.Entity);
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
        //Update  PWorkLogSendFor
        public BaseResultBool ST_UDTO_UpdatePWorkLogSendFor(PWorkLogSendFor entity)
        {
            IBPWorkLogSendFor.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPWorkLogSendFor.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PWorkLogSendFor
        public BaseResultBool ST_UDTO_UpdatePWorkLogSendForByField(PWorkLogSendFor entity, string fields)
        {
            IBPWorkLogSendFor.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPWorkLogSendFor.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPWorkLogSendFor.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPWorkLogSendFor.Edit();
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
        //Delele  PWorkLogSendFor
        public BaseResultBool ST_UDTO_DelPWorkLogSendFor(long longPWorkLogSendForID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPWorkLogSendFor.Remove(longPWorkLogSendForID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPWorkLogSendFor(PWorkLogSendFor entity)
        {
            IBPWorkLogSendFor.Entity = entity;
            EntityList<PWorkLogSendFor> tempEntityList = new EntityList<PWorkLogSendFor>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPWorkLogSendFor.Search();
                tempEntityList.count = IBPWorkLogSendFor.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PWorkLogSendFor>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPWorkLogSendForByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PWorkLogSendFor> tempEntityList = new EntityList<PWorkLogSendFor>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPWorkLogSendFor.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPWorkLogSendFor.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PWorkLogSendFor>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPWorkLogSendForById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPWorkLogSendFor.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PWorkLogSendFor>(tempEntity);
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

        #region PWorkMonthLog
        //Add  PWorkMonthLog
        public BaseResultDataValue ST_UDTO_AddPWorkMonthLog(PWorkMonthLog entity)
        {
            IBPWorkMonthLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPWorkMonthLog.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPWorkMonthLog.Get(IBPWorkMonthLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPWorkMonthLog.Entity);
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

        public BaseResultDataValue ST_UDTO_AddPWorkMonthLogByWeiXin(PWorkMonthLog entity, List<string> AttachmentUrlList)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddPWorkMonthLogByWeiXin.LabID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddPWorkMonthLogByWeiXin.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag));

            IBPWorkMonthLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                IBPWorkMonthLog.Entity.DateCode = DateTime.Now.ToString("yyyy-MM");
                IBPWorkMonthLog.Entity.EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                IBPWorkMonthLog.Entity.EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);

                #region 图片
                if (AttachmentUrlList.Count >= 5)
                {
                    if (AttachmentUrlList[0] != null && AttachmentUrlList[0].Trim() != "")
                    {
                        IBPWorkMonthLog.Entity.Image1 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[0]);
                    }

                    if (AttachmentUrlList[1] != null && AttachmentUrlList[1].Trim() != "")
                    {
                        IBPWorkMonthLog.Entity.Image2 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[1]);
                    }

                    if (AttachmentUrlList[2] != null && AttachmentUrlList[2].Trim() != "")
                    {
                        IBPWorkMonthLog.Entity.Image3 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[2]);
                    }

                    if (AttachmentUrlList[3] != null && AttachmentUrlList[3].Trim() != "")
                    {
                        IBPWorkMonthLog.Entity.Image4 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[3]);
                    }

                    if (AttachmentUrlList[4] != null && AttachmentUrlList[4].Trim() != "")
                    {
                        IBPWorkMonthLog.Entity.Image5 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[4]);
                    }
                }
                #endregion

                tempBaseResultDataValue.success = IBPWorkMonthLog.AddPWorkMonthLogByWeiXin(AttachmentUrlList);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_AddPWorkMonthLogByWeiXin.错误信息：" + ex.ToString());
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        //Update  PWorkMonthLog
        public BaseResultBool ST_UDTO_UpdatePWorkMonthLog(PWorkMonthLog entity)
        {
            IBPWorkMonthLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPWorkMonthLog.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PWorkMonthLog
        public BaseResultBool ST_UDTO_UpdatePWorkMonthLogByField(PWorkMonthLog entity, string fields)
        {
            IBPWorkMonthLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPWorkMonthLog.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPWorkMonthLog.UpdatePWorkMonthLogByField(entity, tempArray);
                        //tempBaseResultBool.success = IBPWorkMonthLog.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPWorkMonthLog.Edit();
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
        //Delele  PWorkMonthLog
        public BaseResultBool ST_UDTO_DelPWorkMonthLog(long longPWorkMonthLogID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPWorkMonthLog.Remove(longPWorkMonthLogID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPWorkMonthLog(PWorkMonthLog entity)
        {
            IBPWorkMonthLog.Entity = entity;
            EntityList<PWorkMonthLog> tempEntityList = new EntityList<PWorkMonthLog>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPWorkMonthLog.Search();
                tempEntityList.count = IBPWorkMonthLog.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PWorkMonthLog>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPWorkMonthLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PWorkMonthLog> tempEntityList = new EntityList<PWorkMonthLog>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPWorkMonthLog.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPWorkMonthLog.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PWorkMonthLog>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPWorkMonthLogById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPWorkMonthLog.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PWorkMonthLog>(tempEntity);
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

        #region PWorkWeekLog
        //Add  PWorkWeekLog
        public BaseResultDataValue ST_UDTO_AddPWorkWeekLog(PWorkWeekLog entity)
        {
            IBPWorkWeekLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPWorkWeekLog.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPWorkWeekLog.Get(IBPWorkWeekLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPWorkWeekLog.Entity);
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

        public BaseResultDataValue ST_UDTO_AddPWorkWeekLogByWeiXin(PWorkWeekLog entity, List<string> AttachmentUrlList)
        {
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddPWorkWeekLogByWeiXin.LabID:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.LabID));
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_AddPWorkWeekLogByWeiXin.IsLabFlag:" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.Base.SysPublicSet.SysDicCookieSession.IsLabFlag));

            IBPWorkWeekLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                IBPWorkWeekLog.Entity.DateCode = DateTime.Now.ToString("yyyy-") + DateTimeHelp.GetWeekOfYear(DateTime.Now);
                IBPWorkWeekLog.Entity.EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                IBPWorkWeekLog.Entity.EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);

                #region 图片
                if (AttachmentUrlList.Count >= 5)
                {
                    if (AttachmentUrlList[0] != null && AttachmentUrlList[0].Trim() != "")
                    {
                        IBPWorkWeekLog.Entity.Image1 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[0]);
                    }

                    if (AttachmentUrlList[1] != null && AttachmentUrlList[1].Trim() != "")
                    {
                        IBPWorkWeekLog.Entity.Image2 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[1]);
                    }

                    if (AttachmentUrlList[2] != null && AttachmentUrlList[2].Trim() != "")
                    {
                        IBPWorkWeekLog.Entity.Image3 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[2]);
                    }

                    if (AttachmentUrlList[3] != null && AttachmentUrlList[3].Trim() != "")
                    {
                        IBPWorkWeekLog.Entity.Image4 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[3]);
                    }

                    if (AttachmentUrlList[4] != null && AttachmentUrlList[4].Trim() != "")
                    {
                        IBPWorkWeekLog.Entity.Image5 = BasePage.GetMultimedia(HttpContext.Current.Application, AttachmentUrlList[4]);
                    }
                }
                #endregion

                tempBaseResultDataValue.success = IBPWorkWeekLog.AddPWorkWeekLogByWeiXin(AttachmentUrlList);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_AddPWorkWeekLogByWeiXin.错误信息：" + ex.ToString());
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  PWorkWeekLog
        public BaseResultBool ST_UDTO_UpdatePWorkWeekLog(PWorkWeekLog entity)
        {
            IBPWorkWeekLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPWorkWeekLog.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PWorkWeekLog
        public BaseResultBool ST_UDTO_UpdatePWorkWeekLogByField(PWorkWeekLog entity, string fields)
        {
            IBPWorkWeekLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPWorkWeekLog.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPWorkWeekLog.UpdatePWorkWeekLogByField(entity, tempArray);
                        //tempBaseResultBool.success = IBPWorkWeekLog.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPWorkWeekLog.Edit();
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
        //Delele  PWorkWeekLog
        public BaseResultBool ST_UDTO_DelPWorkWeekLog(long longPWorkWeekLogID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPWorkWeekLog.Remove(longPWorkWeekLogID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPWorkWeekLog(PWorkWeekLog entity)
        {
            IBPWorkWeekLog.Entity = entity;
            EntityList<PWorkWeekLog> tempEntityList = new EntityList<PWorkWeekLog>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPWorkWeekLog.Search();
                tempEntityList.count = IBPWorkWeekLog.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PWorkWeekLog>(tempEntityList);
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


        public BaseResultDataValue ST_UDTO_SearchPWorkWeekLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PWorkWeekLog> tempEntityList = new EntityList<PWorkWeekLog>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPWorkWeekLog.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPWorkWeekLog.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PWorkWeekLog>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPWorkWeekLogById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPWorkWeekLog.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PWorkWeekLog>(tempEntity);
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

        #region PWorkLogInteraction
        //Add  PWorkLogInteraction
        public BaseResultDataValue ST_UDTO_AddPWorkLogInteraction(PWorkLogInteraction entity)
        {
            IBPWorkLogInteraction.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPWorkLogInteraction.Add((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction);
                if (tempBaseResultDataValue.success)
                {
                    IBPWorkLogInteraction.Get(IBPWorkLogInteraction.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPWorkLogInteraction.Entity);
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
        //Update  PWorkLogInteraction
        public BaseResultBool ST_UDTO_UpdatePWorkLogInteraction(PWorkLogInteraction entity)
        {
            IBPWorkLogInteraction.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPWorkLogInteraction.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PWorkLogInteraction
        public BaseResultBool ST_UDTO_UpdatePWorkLogInteractionByField(PWorkLogInteraction entity, string fields)
        {
            IBPWorkLogInteraction.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPWorkLogInteraction.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPWorkLogInteraction.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPWorkLogInteraction.Edit();
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
        //Delele  PWorkLogInteraction
        public BaseResultBool ST_UDTO_DelPWorkLogInteraction(long longPWorkLogInteractionID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPWorkLogInteraction.Remove(longPWorkLogInteractionID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPWorkLogInteraction(PWorkLogInteraction entity)
        {
            IBPWorkLogInteraction.Entity = entity;
            EntityList<PWorkLogInteraction> tempEntityList = new EntityList<PWorkLogInteraction>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPWorkLogInteraction.Search();
                tempEntityList.count = IBPWorkLogInteraction.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PWorkLogInteraction>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPWorkLogInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PWorkLogInteraction> tempEntityList = new EntityList<PWorkLogInteraction>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPWorkLogInteraction.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPWorkLogInteraction.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PWorkLogInteraction>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPWorkLogInteractionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPWorkLogInteraction.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PWorkLogInteraction>(tempEntity);
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

        #region PClient
        //Add  PClient
        public BaseResultDataValue ST_UDTO_AddPClient(PClient entity)
        {
            IBPClient.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPClient.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPClient.Get(IBPClient.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPClient.Entity);
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
        //Update  PClient
        public BaseResultBool ST_UDTO_UpdatePClient(PClient entity)
        {
            IBPClient.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPClient.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PClient
        public BaseResultBool ST_UDTO_UpdatePClientByField(PClient entity, string fields)
        {
            IBPClient.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPClient.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPClient.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPClient.Edit();
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
        //Delele  PClient
        public BaseResultBool ST_UDTO_DelPClient(long longPClientID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPClient.Remove(longPClientID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPClient(PClient entity)
        {
            IBPClient.Entity = entity;
            EntityList<PClient> tempEntityList = new EntityList<PClient>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPClient.Search();
                tempEntityList.count = IBPClient.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PClient>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPClientByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PClient> tempEntityList = new EntityList<PClient>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPClient.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPClient.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PClient>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchPClientByHQLAndSalesManId(int page, int limit, string fields, string where, string sort, bool isPlanish, long SalesManId, bool IsOwn)
        {

            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PClient> tempEntityList = new EntityList<PClient>();

            ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchPClientByHQLAndSalesManId:SalesManId=" + SalesManId);
            ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchPClientByHQLAndSalesManId:IsOwn=" + IsOwn);
            if (SalesManId == 0)
            {
                SalesManId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchPClientByHQLAndSalesManId:SalesManId为0取当前登录者ID=" + ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID) + "为SalesManId");
            }
            long OwnId = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPClient.SearchPClientByHQLAndSalesManId(where, CommonServiceMethod.GetSortHQL(sort), page, limit, SalesManId, IsOwn, OwnId);
                }
                else
                {
                    tempEntityList = IBPClient.SearchPClientByHQLAndSalesManId(where, page, limit, SalesManId, IsOwn, OwnId);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PClient>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPClientById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPClient.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PClient>(tempEntity);
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


        /// <summary>
        /// 客户信息Excel导出
        /// </summary>
        /// <param name="operateType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public Stream ST_UDTO_ExportExcelPClient(long operateType, string where, int page, int limit, string fields, string sort, bool isPlanish, long SalesManId, bool IsOwn, string filename, string type)
        {
            FileStream fileStream = null;
            if (String.IsNullOrEmpty(filename))
            {
                filename = "用户统计表";

            }
            if (String.IsNullOrEmpty(type))
            {
                type = "PClient";
            }
            bool isExec = true;
            if (isExec)
            {
                if (String.IsNullOrEmpty(where))
                {
                    isExec = false;
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "查询条件不能为空!");
                    return memoryStream;
                }

            }
            if (isExec)
            {
                try
                {
                    fileStream = IBPClient.GetPClientExportExcel(where, ref filename, type, page, limit, fields, sort, isPlanish, SalesManId, IsOwn);
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
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出客户数据为空!");
                        return memoryStream;
                    }
                }
                catch (Exception ex)
                {
                    //fileStream = null;
                    ZhiFang.Common.Log.Log.Error("错误信息:" + ex.StackTrace);
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(-1, "导出客户数据错误!");
                    return memoryStream;
                }
            }
            return fileStream;
        }
        #endregion

        #region PClientLinker
        //Add  PClientLinker
        public BaseResultDataValue ST_UDTO_AddPClientLinker(PClientLinker entity)
        {
            IBPClientLinker.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPClientLinker.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPClientLinker.Get(IBPClientLinker.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPClientLinker.Entity);
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
        //Update  PClientLinker
        public BaseResultBool ST_UDTO_UpdatePClientLinker(PClientLinker entity)
        {
            IBPClientLinker.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPClientLinker.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PClientLinker
        public BaseResultBool ST_UDTO_UpdatePClientLinkerByField(PClientLinker entity, string fields)
        {
            IBPClientLinker.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPClientLinker.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPClientLinker.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPClientLinker.Edit();
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
        //Delele  PClientLinker
        public BaseResultBool ST_UDTO_DelPClientLinker(long longPClientLinkerID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPClientLinker.Remove(longPClientLinkerID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPClientLinker(PClientLinker entity)
        {
            IBPClientLinker.Entity = entity;
            EntityList<PClientLinker> tempEntityList = new EntityList<PClientLinker>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPClientLinker.Search();
                tempEntityList.count = IBPClientLinker.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PClientLinker>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPClientLinkerByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PClientLinker> tempEntityList = new EntityList<PClientLinker>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPClientLinker.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPClientLinker.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PClientLinker>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPClientLinkerById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPClientLinker.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PClientLinker>(tempEntity);
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

        #region PContract
        //Add  PContract
        public BaseResultDataValue ST_UDTO_AddPContract(PContract entity)
        {
            IBPContract.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                //tempBaseResultDataValue.success = IBPContract.Add();
                IBPContract.GetEntityProvinceInfo();
                tempBaseResultDataValue = IBPContract.BPContractAdd((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction);
                if (tempBaseResultDataValue.success)
                {
                    IBPContract.Get(IBPContract.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPContract.Entity);
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
        //Update  PContract
        public BaseResultBool ST_UDTO_UpdatePContract(PContract entity)
        {
            IBPContract.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                IBPContract.GetEntityProvinceInfo();
                tempBaseResultBool.success = IBPContract.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PContract
        public BaseResultBool ST_UDTO_UpdatePContractByField(PContract entity, string fields)
        {
            IBPContract.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    IBPContract.GetEntityProvinceInfo();
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPContract.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPContract.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPContract.Edit();
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
        //Delele  PContract
        public BaseResultBool ST_UDTO_DelPContract(long longPContractID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPContract.Remove(longPContractID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPContract(PContract entity)
        {
            IBPContract.Entity = entity;
            EntityList<PContract> tempEntityList = new EntityList<PContract>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPContract.Search();
                tempEntityList.count = IBPContract.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PContract>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPContractByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PContract> tempEntityList = new EntityList<PContract>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPContract.SearchListByHQL(where, CommonServiceMethod.GetExpandSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPContract.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PContract>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchPContractTotalByHQL(string fields, string where)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPContract.SearchListTotalByHQL(where, fields);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchPContractById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPContract.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PContract>(tempEntity);
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

        public BaseResultBool ST_UDTO_UpdatePContractStatus(PContract entity, string fields)
        {

            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                IBPContract.Entity = entity;
                if (entity.Id <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：任务ID为空！";
                    return tempBaseResultBool;
                }
                if (entity.ContractStatus == null)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：任务状态为空！";
                    return tempBaseResultBool;
                }

                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                    if (tempArray != null)
                    {
                        long EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                        string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                        tempBaseResultBool = IBPContract.UpdatePContractStatus((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, tempArray, EmpID, EmpName);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPTask.Edit();
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

        #region PContractFollow
        //Add  PContract
        public BaseResultDataValue ST_UDTO_AddPContractFollow(PContractFollow entity)
        {
            IBPContractFollow.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (IBPContractFollow.Add())
                {
                    IBPContractFollow.Get(IBPContractFollow.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPContractFollow.Entity);
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
        //Update  PContract
        public BaseResultBool ST_UDTO_UpdatePContractFollow(PContractFollow entity)
        {
            IBPContractFollow.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPContractFollow.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PContract
        public BaseResultBool ST_UDTO_UpdatePContractFollowByField(PContractFollow entity, string fields)
        {
            IBPContractFollow.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPContractFollow.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPContractFollow.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPContractFollow.Edit();
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
        //Delele  PContract
        public BaseResultBool ST_UDTO_DelPContractFollow(long longPContractFollowID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPContractFollow.Remove(longPContractFollowID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPContractFollow(PContractFollow entity)
        {
            IBPContractFollow.Entity = entity;
            EntityList<PContractFollow> tempEntityList = new EntityList<PContractFollow>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPContractFollow.Search();
                tempEntityList.count = IBPContractFollow.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PContractFollow>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPContractFollowByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PContractFollow> tempEntityList = new EntityList<PContractFollow>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPContractFollow.SearchListByHQL(where, CommonServiceMethod.GetExpandSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPContractFollow.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PContractFollow>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchPContractFollowTotalByHQL(string fields, string where)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPContractFollow.SearchListTotalByHQL(where, fields);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchPContractFollowById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPContractFollow.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PContractFollow>(tempEntity);
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

        #region PContractFollowInteraction
        //Add  PContract
        public BaseResultDataValue ST_UDTO_AddPContractFollowInteraction(PContractFollowInteraction entity)
        {
            IBPContractFollowInteraction.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                if (IBPContractFollowInteraction.Add())
                {
                    IBPContractFollowInteraction.Get(IBPContractFollowInteraction.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPContractFollowInteraction.Entity);
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
        //Update  PContract
        public BaseResultBool ST_UDTO_UpdatePContractFollowInteraction(PContractFollowInteraction entity)
        {
            IBPContractFollowInteraction.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPContractFollowInteraction.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PContract
        public BaseResultBool ST_UDTO_UpdatePContractFollowInteractionByField(PContractFollowInteraction entity, string fields)
        {
            IBPContractFollowInteraction.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPContractFollowInteraction.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPContractFollowInteraction.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPContractFollowInteraction.Edit();
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
        //Delele  PContract
        public BaseResultBool ST_UDTO_DelPContractFollowInteraction(long longPContractFollowInteractionID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPContractFollowInteraction.Remove(longPContractFollowInteractionID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPContractFollowInteraction(PContractFollowInteraction entity)
        {
            IBPContractFollowInteraction.Entity = entity;
            EntityList<PContractFollowInteraction> tempEntityList = new EntityList<PContractFollowInteraction>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPContractFollowInteraction.Search();
                tempEntityList.count = IBPContractFollowInteraction.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PContractFollowInteraction>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPContractFollowInteractionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PContractFollowInteraction> tempEntityList = new EntityList<PContractFollowInteraction>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPContractFollowInteraction.SearchListByHQL(where, CommonServiceMethod.GetExpandSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPContractFollowInteraction.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PContractFollowInteraction>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchPContractFollowInteractionTotalByHQL(string fields, string where)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPContractFollowInteraction.SearchListTotalByHQL(where, fields);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchPContractFollowInteractionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPContractFollowInteraction.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PContractFollowInteraction>(tempEntity);
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

        #region PTaskTypeEmpLink
        //Add  PTaskTypeEmpLink
        public BaseResultDataValue ST_UDTO_AddPTaskTypeEmpLink(PTaskTypeEmpLink entity)
        {
            IBPTaskTypeEmpLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPTaskTypeEmpLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPTaskTypeEmpLink.Get(IBPTaskTypeEmpLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPTaskTypeEmpLink.Entity);
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
        //Update  PTaskTypeEmpLink
        public BaseResultBool ST_UDTO_UpdatePTaskTypeEmpLink(PTaskTypeEmpLink entity)
        {
            IBPTaskTypeEmpLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPTaskTypeEmpLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PTaskTypeEmpLink
        public BaseResultBool ST_UDTO_UpdatePTaskTypeEmpLinkByField(PTaskTypeEmpLink entity, string fields)
        {
            IBPTaskTypeEmpLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPTaskTypeEmpLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPTaskTypeEmpLink.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPTaskTypeEmpLink.Edit();
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
        //Delele  PTaskTypeEmpLink
        public BaseResultBool ST_UDTO_DelPTaskTypeEmpLink(long longPTaskTypeEmpLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPTaskTypeEmpLink.Remove(longPTaskTypeEmpLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPTaskTypeEmpLink(PTaskTypeEmpLink entity)
        {
            IBPTaskTypeEmpLink.Entity = entity;
            EntityList<PTaskTypeEmpLink> tempEntityList = new EntityList<PTaskTypeEmpLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPTaskTypeEmpLink.Search();
                tempEntityList.count = IBPTaskTypeEmpLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PTaskTypeEmpLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPTaskTypeEmpLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PTaskTypeEmpLink> tempEntityList = new EntityList<PTaskTypeEmpLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPTaskTypeEmpLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPTaskTypeEmpLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PTaskTypeEmpLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPTaskTypeEmpLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPTaskTypeEmpLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PTaskTypeEmpLink>(tempEntity);
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
        /// <summary>
        /// 根据Session中员工ID查询该人员所具有权限的字典树
        /// </summary>
        /// <param name="idStr"></param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_SearchPTaskTypeEmpLinkToTreeBySessionHREmpID(string id, string where)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (String.IsNullOrEmpty(id) || String.IsNullOrEmpty(where))
                return tempBaseResultDataValue;
            object tempBaseResultTree = null;
            try
            {
                string tempEmployeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID); //EmployeeID 员工ID

                if ((tempEmployeeID != null) && (tempEmployeeID.Length > 0) && !String.IsNullOrEmpty(id))
                    tempBaseResultTree = IBPTaskTypeEmpLink.SearchPTaskTypeEmpLinkToTree(Int64.Parse(tempEmployeeID), id, where);

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
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region PInvoice
        //Add  PInvoice
        public BaseResultDataValue ST_UDTO_AddPInvoice(PInvoice entity)
        {
            IBPInvoice.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPInvoice.PInvoiceAdd((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction);
                if (tempBaseResultDataValue.success)
                {
                    IBPInvoice.Get(IBPInvoice.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPInvoice.Entity);
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
        //Update  PInvoice
        public BaseResultBool ST_UDTO_UpdatePInvoice(PInvoice entity)
        {
            IBPInvoice.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPInvoice.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PInvoice
        public BaseResultBool ST_UDTO_UpdatePInvoiceByField(PInvoice entity, string fields)
        {
            IBPInvoice.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if (entity.Id <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "ST_UDTO_UpdatePInvoiceByField错误信息：发票ID为空！";
                    return tempBaseResultBool;
                }
                if (entity.Status == 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "ST_UDTO_UpdatePInvoiceByField错误信息：发票状态为空！";
                    return tempBaseResultBool;
                }
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPInvoice.Entity, fields);
                    if (tempArray != null)
                    {
                        long EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                        string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                        tempBaseResultBool = IBPInvoice.PInvoiceStatusUpdate((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, tempArray, EmpID, EmpName);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPInvoice.Edit();
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
        public BaseResultBool ST_UDTO_UpdatePInvoiceByFieldManager(PInvoice entity, string fields)
        {
            IBPInvoice.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPInvoice.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPInvoice.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPDictType.Edit();
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
        //Delele  PInvoice
        public BaseResultBool ST_UDTO_DelPInvoice(long longPInvoiceID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPInvoice.Remove(longPInvoiceID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPInvoice(PInvoice entity)
        {
            IBPInvoice.Entity = entity;
            EntityList<PInvoice> tempEntityList = new EntityList<PInvoice>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPInvoice.Search();
                tempEntityList.count = IBPInvoice.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PInvoice>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPInvoiceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PInvoice> tempEntityList = new EntityList<PInvoice>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPInvoice.SearchListByHQL(where, CommonServiceMethod.GetExpandSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPInvoice.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PInvoice>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPInvoiceById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPInvoice.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PInvoice>(tempEntity);
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

        public BaseResultDataValue ST_UDTO_SearchPInvoiceByExportType(long ExportType, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PInvoice> tempEntityList = new EntityList<PInvoice>();
            try
            {
                long empid = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID)); ;
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPInvoice.SearchPInvoiceByExportType(ExportType, empid, where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPInvoice.SearchPInvoiceByExportType(ExportType, empid, where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PInvoice>(tempEntityList);
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

        #region PFinanceReceive
        //Add  PFinanceReceive
        public BaseResultDataValue ST_UDTO_AddPFinanceReceive(PFinanceReceive entity)
        {
            IBPFinanceReceive.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPFinanceReceive.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPFinanceReceive.Get(IBPFinanceReceive.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPFinanceReceive.Entity);
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
        //Update  PFinanceReceive
        public BaseResultBool ST_UDTO_UpdatePFinanceReceive(PFinanceReceive entity)
        {
            IBPFinanceReceive.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPFinanceReceive.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PFinanceReceive
        public BaseResultBool ST_UDTO_UpdatePFinanceReceiveByField(PFinanceReceive entity, string fields)
        {
            IBPFinanceReceive.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPFinanceReceive.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPFinanceReceive.UpdateByFinance(tempArray, entity, fields);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPFinanceReceive.Edit();
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
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_UpdatePFinanceReceiveOfAssociateByField(PFinanceReceive entity, string fields)
        {
            IBPFinanceReceive.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPFinanceReceive.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPFinanceReceive.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPFinanceReceive.Edit();
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
        //Delele  PFinanceReceive
        public BaseResultBool ST_UDTO_DelPFinanceReceive(long longPFinanceReceiveID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPFinanceReceive.RemoveByFinance(longPFinanceReceiveID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPFinanceReceive(PFinanceReceive entity)
        {
            IBPFinanceReceive.Entity = entity;
            EntityList<PFinanceReceive> tempEntityList = new EntityList<PFinanceReceive>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPFinanceReceive.Search();
                tempEntityList.count = IBPFinanceReceive.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PFinanceReceive>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPFinanceReceiveByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PFinanceReceive> tempEntityList = new EntityList<PFinanceReceive>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPFinanceReceive.SearchListByHQL(where, CommonServiceMethod.GetExpandSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPFinanceReceive.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PFinanceReceive>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPFinanceReceiveTotalByHQL(string fields, string where)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPFinanceReceive.SearchListTotalByHQL(where, fields);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchPFinanceReceiveById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPFinanceReceive.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PFinanceReceive>(tempEntity);
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

        #region PReceive
        //Add  PReceive
        public BaseResultDataValue ST_UDTO_AddPReceive(PReceive entity)
        {
            IBPReceive.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPReceive.AddPReceive();
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  PReceive
        public BaseResultBool ST_UDTO_UpdatePReceive(PReceive entity)
        {
            IBPReceive.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPReceive.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PReceive
        public BaseResultBool ST_UDTO_UpdatePReceiveByField(PReceive entity, string fields)
        {
            IBPReceive.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPReceive.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPReceive.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPReceive.Edit();
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

        public BaseResultDataValue ST_UDTO_AddBackPReceive(PReceive entity)
        {
            IBPReceive.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPReceive.AddBackPReceive();
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Delele  PReceive
        public BaseResultBool ST_UDTO_DelPReceive(long longPReceiveID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPReceive.Remove(longPReceiveID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPReceive(PReceive entity)
        {
            IBPReceive.Entity = entity;
            EntityList<PReceive> tempEntityList = new EntityList<PReceive>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPReceive.Search();
                tempEntityList.count = IBPReceive.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PReceive>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPReceiveByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PReceive> tempEntityList = new EntityList<PReceive>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPReceive.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPReceive.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PReceive>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_AdvSearchPReceiveByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PReceive> tempEntityList = new EntityList<PReceive>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPReceive.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPReceive.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PReceive>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPReceiveTotalByHQL(string fields, string where)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPReceive.SearchListTotalByHQL(where, fields);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchPReceiveById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPReceive.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PReceive>(tempEntity);
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

        #region PReceivePlan
        //Add  PReceivePlan
        public BaseResultDataValue ST_UDTO_AddPReceivePlan(PReceivePlan entity)
        {
            IBPReceivePlan.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPReceivePlan.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPReceivePlan.Get(IBPReceivePlan.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPReceivePlan.Entity);
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

        public BaseResultDataValue ST_UDTO_AddPReceivePlanList(List<PReceivePlan> entity)
        {

            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPReceivePlan.AddPReceivePlanList((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, entity);
                return tempBaseResultDataValue;
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  PReceivePlan
        public BaseResultBool ST_UDTO_UpdatePReceivePlan(PReceivePlan entity)
        {
            IBPReceivePlan.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPReceivePlan.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        public BaseResultDataValue ST_UDTO_ChangeApplyPReceivePlan(List<PReceivePlan> entity, long PPReceivePlanID)
        {

            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPReceivePlan.ChangeApplyPReceivePlan((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, entity, PPReceivePlanID);
                return tempBaseResultDataValue;
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_ChangeSubmitPReceivePlan(long PPReceivePlanID)
        {

            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPReceivePlan.ChangeSubmitPReceivePlan((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, PPReceivePlanID);
                return tempBaseResultDataValue;
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_UnChangeSubmitPReceivePlan(long PPReceivePlanID)
        {

            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPReceivePlan.UnChangeSubmitPReceivePlan((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, PPReceivePlanID);
                return tempBaseResultDataValue;
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  PReceivePlan
        public BaseResultBool ST_UDTO_UpdatePReceivePlanByField(PReceivePlan entity, string fields)
        {
            IBPReceivePlan.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPReceivePlan.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPReceivePlan.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPReceivePlan.Edit();
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
        //Delele  PReceivePlan
        public BaseResultBool ST_UDTO_DelPReceivePlan(long longPReceivePlanID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPReceivePlan.DelPReceivePlan(longPReceivePlanID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPReceivePlan(PReceivePlan entity)
        {
            IBPReceivePlan.Entity = entity;
            EntityList<PReceivePlan> tempEntityList = new EntityList<PReceivePlan>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPReceivePlan.Search();
                tempEntityList.count = IBPReceivePlan.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PReceivePlan>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPReceivePlanByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PReceivePlan> tempEntityList = new EntityList<PReceivePlan>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPReceivePlan.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPReceivePlan.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PReceivePlan>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchPReceivePlanTreeByHQL(string fields, string where)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree<PReceivePlan> baseResultTree = new BaseResultTree<PReceivePlan>();
            try
            {
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchPReceivePlanTreeByHQL:where" + where);
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_SearchPReceivePlanTreeByHQL:fields" + fields);
                baseResultTree = IBPReceivePlan.SearchListTreeByHQL(where);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish<BaseResultTree<PReceivePlan>>(baseResultTree, fields);
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

        public BaseResultDataValue ST_UDTO_AdvSearchPReceivePlanTreeByHQL(string fields, string where)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            BaseResultTree<PReceivePlan> baseResultTree = new BaseResultTree<PReceivePlan>();
            try
            {
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AdvSearchPReceivePlanTreeByHQL:where" + where);
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AdvSearchPReceivePlanTreeByHQL:fields" + fields);
                baseResultTree = IBPReceivePlan.AdvSearchListTreeByHQL(where);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish<BaseResultTree<PReceivePlan>>(baseResultTree, fields);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "ST_UDTO_AdvSearchPReceivePlanTreeByHQL序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "ST_UDTO_AdvSearchPReceivePlanTreeByHQLHQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_AdvSearchTotalPReceivePlanByHQL(string fields, string where)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AdvSearchTotalPReceivePlanTreeByHQL:where" + where);
                ZhiFang.Common.Log.Log.Debug("ST_UDTO_AdvSearchTotalPReceivePlanTreeByHQL:fields" + fields);
                tempBaseResultDataValue = IBPReceivePlan.AdvSearchTotalListTreeByHQL(where, fields); ;
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchPReceivePlanById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPReceivePlan.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PReceivePlan>(tempEntity);
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

        #region PRepayment
        //Add  PRepayment
        public BaseResultDataValue ST_UDTO_AddPRepayment(PRepayment entity)
        {
            IBPRepayment.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPRepayment.PRepaymentAdd((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction);
                if (tempBaseResultDataValue.success)
                {
                    IBPRepayment.Get(IBPRepayment.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPRepayment.Entity);
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
        //Update  PRepayment
        public BaseResultBool ST_UDTO_UpdatePRepayment(PRepayment entity)
        {
            IBPRepayment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPRepayment.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PRepayment
        public BaseResultBool ST_UDTO_UpdatePRepaymentByField(PRepayment entity, string fields)
        {
            IBPRepayment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if (entity.Id <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "ST_UDTO_UpdatePRepaymentByField：还款单ID为空！";
                    return tempBaseResultBool;
                }
                if (entity.Status == 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "ST_UDTO_UpdatePRepaymentByField：还款单状态为空！";
                    return tempBaseResultBool;
                }
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPRepayment.Entity, fields);
                    if (tempArray != null)
                    {
                        long EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                        string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                        tempBaseResultBool = IBPRepayment.PRepaymentStatusUpdate((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, tempArray, EmpID, EmpName);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPRepayment.Edit();
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
        //Delele  PRepayment
        public BaseResultBool ST_UDTO_DelPRepayment(long longPRepaymentID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPRepayment.Remove(longPRepaymentID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPRepayment(PRepayment entity)
        {
            IBPRepayment.Entity = entity;
            EntityList<PRepayment> tempEntityList = new EntityList<PRepayment>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPRepayment.Search();
                tempEntityList.count = IBPRepayment.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PRepayment>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPRepaymentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PRepayment> tempEntityList = new EntityList<PRepayment>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPRepayment.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPRepayment.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PRepayment>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPRepaymentById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPRepayment.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PRepayment>(tempEntity);
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

        #region PLoanBill
        //Add  PLoanBill
        public BaseResultDataValue ST_UDTO_AddPLoanBill(PLoanBill entity)
        {
            IBPLoanBill.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPLoanBill.PLoanBillAdd((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction);
                if (tempBaseResultDataValue.success)
                {
                    IBPLoanBill.Get(IBPLoanBill.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPLoanBill.Entity);
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
        //Update  PLoanBill
        public BaseResultBool ST_UDTO_UpdatePLoanBill(PLoanBill entity)
        {
            IBPLoanBill.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPLoanBill.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PLoanBill
        public BaseResultBool ST_UDTO_UpdatePLoanBillByField(PLoanBill entity, string fields)
        {
            IBPLoanBill.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if (entity.Id <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "ST_UDTO_UpdatePLoanBillByField：借款单ID为空！";
                    return tempBaseResultBool;
                }
                if (entity.Status == 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "ST_UDTO_UpdatePLoanBillByField：借款单状态为空！";
                    return tempBaseResultBool;
                }
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPLoanBill.Entity, fields);
                    if (tempArray != null)
                    {
                        long EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                        string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                        tempBaseResultBool = IBPLoanBill.PLoanBillStatusUpdate((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, tempArray, EmpID, EmpName);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPLoanBill.Edit();
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

        //Delele  PLoanBill
        public BaseResultBool ST_UDTO_DelPLoanBill(long longPLoanBillID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPLoanBill.Remove(longPLoanBillID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPLoanBill(PLoanBill entity)
        {
            IBPLoanBill.Entity = entity;
            EntityList<PLoanBill> tempEntityList = new EntityList<PLoanBill>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPLoanBill.Search();
                tempEntityList.count = IBPLoanBill.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PLoanBill>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchPLoanBillNoPlanishByHQL(int page, int limit, string fields, string where, string sort)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PLoanBill> tempEntityList = new EntityList<PLoanBill>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    Newtonsoft.Json.Linq.JArray tempJArray = Newtonsoft.Json.Linq.JArray.Parse(sort);
                    List<string> SortList = new List<string>();
                    string sortStr = "";
                    string tempStr = "";
                    foreach (var tempObject in tempJArray)
                    {
                        tempStr = tempObject["property"].ToString() + " " + tempObject["direction"].ToString().ToUpper();
                        SortList.Add(tempStr);
                    }
                    if (SortList.Count > 0)
                    {
                        sortStr = string.Join(",", SortList.ToArray());
                    }
                    //ZhiFang.Common.Log.Log.Debug("sortStr:" + sortStr);
                    tempEntityList = IBPLoanBill.SearchListByHQL(where, sortStr, page, limit);
                }
                else
                {
                    tempEntityList = IBPLoanBill.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
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

        public BaseResultDataValue ST_UDTO_SearchPLoanBillByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PLoanBill> tempEntityList = new EntityList<PLoanBill>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPLoanBill.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPLoanBill.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PLoanBill>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPLoanBillById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPLoanBill.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PLoanBill>(tempEntity);
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

        #region PEmpFinanceAccount
        //Add  PEmpFinanceAccount
        public BaseResultDataValue ST_UDTO_AddPEmpFinanceAccount(PEmpFinanceAccount entity)
        {
            IBPEmpFinanceAccount.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPEmpFinanceAccount.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPEmpFinanceAccount.Get(IBPEmpFinanceAccount.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPEmpFinanceAccount.Entity);
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
        //Update  PEmpFinanceAccount
        public BaseResultBool ST_UDTO_UpdatePEmpFinanceAccount(PEmpFinanceAccount entity)
        {
            IBPEmpFinanceAccount.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPEmpFinanceAccount.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PEmpFinanceAccount
        public BaseResultBool ST_UDTO_UpdatePEmpFinanceAccountByField(PEmpFinanceAccount entity, string fields)
        {
            IBPEmpFinanceAccount.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPEmpFinanceAccount.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPEmpFinanceAccount.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPEmpFinanceAccount.Edit();
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
        //Delele  PEmpFinanceAccount
        public BaseResultBool ST_UDTO_DelPEmpFinanceAccount(long longPEmpFinanceAccountID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPEmpFinanceAccount.Remove(longPEmpFinanceAccountID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPEmpFinanceAccount(PEmpFinanceAccount entity)
        {
            IBPEmpFinanceAccount.Entity = entity;
            EntityList<PEmpFinanceAccount> tempEntityList = new EntityList<PEmpFinanceAccount>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPEmpFinanceAccount.Search();
                tempEntityList.count = IBPEmpFinanceAccount.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PEmpFinanceAccount>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPEmpFinanceAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PEmpFinanceAccount> tempEntityList = new EntityList<PEmpFinanceAccount>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPEmpFinanceAccount.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPEmpFinanceAccount.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PEmpFinanceAccount>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPEmpFinanceAccountByDeptId(string deptid, bool isincludesubdept)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.Common.Log.Log.Error("SearchPEmpFinanceAccountByDeptId,deptid=" + deptid + "@isincludesubdept=" + isincludesubdept);

                if (String.IsNullOrEmpty(deptid))
                {
                    ZhiFang.Common.Log.Log.Error("SearchPEmpFinanceAccountByDeptId,deptid为空!");
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ErrorInfo = "SearchPEmpFinanceAccountByDeptId,deptid为空!";
                    //return tempBaseResultDataValue;
                }
                else
                {
                    IList<PEmpFinanceAccount> worklogvolist = IBPEmpFinanceAccount.SearchPEmpFinanceAccountByDeptId(long.Parse(deptid), isincludesubdept);
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                    try
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(worklogvolist);
                        tempBaseResultDataValue.success = true;
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultDataValue.success = false;
                        tempBaseResultDataValue.ErrorInfo = "SearchPEmpFinanceAccountByDeptId：" + ex.Message;
                        ZhiFang.Common.Log.Log.Error("SearchPEmpFinanceAccountByDeptId：" + ex.ToString());
                        //throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "SearchPEmpFinanceAccountByDeptId错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("SearchPEmpFinanceAccountByDeptId错误：" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_SearchPEmpFinanceAccountById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPEmpFinanceAccount.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PEmpFinanceAccount>(tempEntity);
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

        #region PExpenseAccount
        //Add  PExpenseAccount
        public BaseResultDataValue ST_UDTO_AddPExpenseAccount(PExpenseAccount entity)
        {
            IBPExpenseAccount.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPExpenseAccount.PExpenseAccountAdd((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction);
                if (tempBaseResultDataValue.success)
                {
                    IBPExpenseAccount.Get(IBPExpenseAccount.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPExpenseAccount.Entity);
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
        //Update  PExpenseAccount
        public BaseResultBool ST_UDTO_UpdatePExpenseAccount(PExpenseAccount entity)
        {
            IBPExpenseAccount.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPExpenseAccount.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PExpenseAccount
        public BaseResultBool ST_UDTO_UpdatePExpenseAccountByField(PExpenseAccount entity, string fields)
        {
            IBPExpenseAccount.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPExpenseAccount.Entity, fields);
                    if (tempArray != null)
                    {
                        long EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                        string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                        tempBaseResultBool = IBPExpenseAccount.PExpenseAccountStatusUpdate((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, tempArray, EmpID, EmpName);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPExpenseAccount.Edit();
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
        //Delele  PExpenseAccount
        public BaseResultBool ST_UDTO_DelPExpenseAccount(long longPExpenseAccountID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPExpenseAccount.Remove(longPExpenseAccountID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPExpenseAccount(PExpenseAccount entity)
        {
            IBPExpenseAccount.Entity = entity;
            EntityList<PExpenseAccount> tempEntityList = new EntityList<PExpenseAccount>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPExpenseAccount.Search();
                tempEntityList.count = IBPExpenseAccount.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PExpenseAccount>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPExpenseAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PExpenseAccount> tempEntityList = new EntityList<PExpenseAccount>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPExpenseAccount.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPExpenseAccount.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PExpenseAccount>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPExpenseAccountById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPExpenseAccount.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PExpenseAccount>(tempEntity);
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

        #region PCustomerServiceOperation
        //Add  PCustomerServiceOperation
        public BaseResultDataValue ST_UDTO_AddPCustomerServiceOperation(PCustomerServiceOperation entity)
        {
            IBPCustomerServiceOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPCustomerServiceOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPCustomerServiceOperation.Get(IBPCustomerServiceOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPCustomerServiceOperation.Entity);
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
        //Update  PCustomerServiceOperation
        public BaseResultBool ST_UDTO_UpdatePCustomerServiceOperation(PCustomerServiceOperation entity)
        {
            IBPCustomerServiceOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPCustomerServiceOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PCustomerServiceOperation
        public BaseResultBool ST_UDTO_UpdatePCustomerServiceOperationByField(PCustomerServiceOperation entity, string fields)
        {
            IBPCustomerServiceOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPCustomerServiceOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPCustomerServiceOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPCustomerServiceOperation.Edit();
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
        //Delele  PCustomerServiceOperation
        public BaseResultBool ST_UDTO_DelPCustomerServiceOperation(long longPCustomerServiceOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPCustomerServiceOperation.Remove(longPCustomerServiceOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPCustomerServiceOperation(PCustomerServiceOperation entity)
        {
            IBPCustomerServiceOperation.Entity = entity;
            EntityList<PCustomerServiceOperation> tempEntityList = new EntityList<PCustomerServiceOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPCustomerServiceOperation.Search();
                tempEntityList.count = IBPCustomerServiceOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PCustomerServiceOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPCustomerServiceOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PCustomerServiceOperation> tempEntityList = new EntityList<PCustomerServiceOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPCustomerServiceOperation.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPCustomerServiceOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PCustomerServiceOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPCustomerServiceOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPCustomerServiceOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PCustomerServiceOperation>(tempEntity);
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

        #region PCustomerServiceAttachment

        /// <summary>
        /// 上传服务附件
        /// </summary>
        /// <returns></returns>
        public Message SC_UploadAddPCustomerServiceAttachment()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string nullValue = "{id:'',fileSize:''}";
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                brdv.success = true;
                HttpPostedFile file = null;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到文件！";
                    brdv.ResultDataValue = nullValue;
                    //brdv.success = false;
                }
                else
                {
                    file = HttpContext.Current.Request.Files[0];
                }

                //需要保存的数据对象名称
                string objectName = "", fkObjectId = "", fkObjectName = "", fileName = "", newFileName = "", contentType = "";
                objectName = HttpContext.Current.Request.Form["ObjectEName"];
                //需要保存的数据对象的子对象Id值
                fkObjectId = HttpContext.Current.Request.Form["FKObjectId"];
                //需要保存的数据对象的子对象名称
                fkObjectName = HttpContext.Current.Request.Form["FKObjectName"];
                string fileSize = HttpContext.Current.Request.Form["FileSize"];
                fileSize = HttpContext.Current.Request.Form["FileSize"];
                //文件名称
                fileName = HttpContext.Current.Request.Form["FileName"];
                newFileName = HttpContext.Current.Request.Form["NewFileName"];
                //公共附件分类保存的文件夹名称
                string saveCategory = HttpContext.Current.Request.Form["SaveCategory"];
                //业务模块代码
                string businessModuleCode = HttpContext.Current.Request.Form["BusinessModuleCode"];
                int len = 0;
                if (!string.IsNullOrEmpty(fileSize))
                {
                    len = Int32.Parse(fileSize);
                }
                if (file != null)
                {
                    //file.FileName处理
                    //如果是IE传回来的是"H:\常用.txt"格式,需要处理为常用.txt;火狐传回的是常用.txt
                    len = file.ContentLength;
                    contentType = file.ContentType;
                    int startIndex = file.FileName.LastIndexOf(@"\");
                    startIndex = startIndex > -1 ? startIndex + 1 : startIndex;
                    if (string.IsNullOrEmpty(fileName))
                        fileName = startIndex > -1 ? file.FileName.Substring(startIndex) : file.FileName;
                    //ZhiFang.Common.Log.Log.Debug("FileName:" + fileName);
                }


                if (String.IsNullOrEmpty(objectName))
                {
                    brdv.ErrorInfo = "上传附件的数据对象名称为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && String.IsNullOrEmpty(fkObjectName))
                {
                    brdv.ErrorInfo = "上传附件的数据对象子对象名称为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && String.IsNullOrEmpty(fkObjectId))
                {
                    brdv.ErrorInfo = "上传附件的数据对象子对象值为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && !string.IsNullOrEmpty(fkObjectId) && len > 0 && !string.IsNullOrEmpty(fileName))
                {
                    //上传附件路径
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNo.UploadFilesPath.ToString());
                    string tempPath = "\\" + objectName + "\\";
                    if (!String.IsNullOrEmpty(saveCategory))
                    {
                        tempPath = tempPath + saveCategory + "\\";
                    }
                    tempPath = tempPath + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\";
                    parentPath = parentPath + tempPath;
                    if (!Directory.Exists(parentPath))
                        Directory.CreateDirectory(parentPath);
                    string fileExt = fileName.Substring(fileName.LastIndexOf("."));


                    PCustomerServiceAttachment entity = new PCustomerServiceAttachment();
                    entity.BusinessModuleCode = businessModuleCode;
                    entity.FileName = fileName;
                    entity.FileSize = len;// / 1024;
                    entity.FilePath = tempPath;
                    entity.IsUse = true;
                    entity.NewFileName = newFileName;
                    entity.FileExt = fileExt;
                    entity.FileType = contentType;
                    brdv = IBPCustomerServiceAttachment.AddPCustomerServiceAttachment(fkObjectId, fkObjectName, file, parentPath, tempPath, fileExt, entity);
                    //brdv.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPCustomerServiceAttachment.Entity);
                    if (brdv.success)
                        brdv.ResultDataValue = "{id:" + "\"" + entity.Id.ToString() + "\"" + ",fileSize:" + "\"" + len + "\"" + "}";
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("公共附件上传错误:" + ex.Message);
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = nullValue;
                brdv.success = false;
            }

            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }
        /// <summary>
        /// 下载公共附件的文件
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>
        public Stream SC_UDTO_DownLoadPCustomerServiceAttachment(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filePath = "";
                PCustomerServiceAttachment file = IBPCustomerServiceAttachment.GetAttachmentFilePathAndFileName(id, ref filePath);
                if (!string.IsNullOrEmpty(filePath) && file != null)
                {
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    //获取错误提示信息
                    if (fileStream == null)
                    {
                        string errorInfo = "附件不存在!请联系管理员。";
                        //fileStream = ErrFileStreamInfo.GetErrFileStreamInfo(id, errorInfo);
                        MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                        return memoryStream;
                    }
                    else
                    {
                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;
                        System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                        string filename = file.NewFileName + file.FileExt;
                        if (string.IsNullOrEmpty(filename))
                        {
                            filename = file.FileName;
                        }
                        filename = EncodeFileName.ToEncodeFileName(filename);
                        if (operateType == 0) //下载文件
                        {
                            System.Web.HttpContext.Current.Response.ContentType = "" + file.FileType;
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                        }
                        else if (operateType == 1)//直接打开文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "" + file.FileType;// "" + file.FileType;
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
                return fileStream;
            }
            catch (Exception ex)
            {
                string errorInfo = "附件不存在!请联系管理员。";
                ZhiFang.Common.Log.Log.Error("公共附件下载错误信息:" + ex.Message);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                return memoryStream;
            }
        }
        //Add  PCustomerServiceAttachment
        public BaseResultDataValue ST_UDTO_AddPCustomerServiceAttachment(PCustomerServiceAttachment entity)
        {
            IBPCustomerServiceAttachment.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPCustomerServiceAttachment.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPCustomerServiceAttachment.Get(IBPCustomerServiceAttachment.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPCustomerServiceAttachment.Entity);
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
        //Update  PCustomerServiceAttachment
        public BaseResultBool ST_UDTO_UpdatePCustomerServiceAttachment(PCustomerServiceAttachment entity)
        {
            IBPCustomerServiceAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPCustomerServiceAttachment.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PCustomerServiceAttachment
        public BaseResultBool ST_UDTO_UpdatePCustomerServiceAttachmentByField(PCustomerServiceAttachment entity, string fields)
        {
            IBPCustomerServiceAttachment.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPCustomerServiceAttachment.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPCustomerServiceAttachment.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPCustomerServiceAttachment.Edit();
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
        //Delele  PCustomerServiceAttachment
        public BaseResultBool ST_UDTO_DelPCustomerServiceAttachment(long longPCustomerServiceAttachmentID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPCustomerServiceAttachment.Remove(longPCustomerServiceAttachmentID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPCustomerServiceAttachment(PCustomerServiceAttachment entity)
        {
            IBPCustomerServiceAttachment.Entity = entity;
            EntityList<PCustomerServiceAttachment> tempEntityList = new EntityList<PCustomerServiceAttachment>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPCustomerServiceAttachment.Search();
                tempEntityList.count = IBPCustomerServiceAttachment.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PCustomerServiceAttachment>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPCustomerServiceAttachmentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PCustomerServiceAttachment> tempEntityList = new EntityList<PCustomerServiceAttachment>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPCustomerServiceAttachment.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPCustomerServiceAttachment.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PCustomerServiceAttachment>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPCustomerServiceAttachmentById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPCustomerServiceAttachment.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PCustomerServiceAttachment>(tempEntity);
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

        #region PCustomerService
        //Add  PCustomerService
        public BaseResultDataValue ST_UDTO_AddPCustomerService(PCustomerService entity)
        {
            IBPCustomerService.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPCustomerService.PCustomerServiceAdd((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction);
                if (tempBaseResultDataValue.success)
                {
                    IBPCustomerService.Get(IBPCustomerService.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPCustomerService.Entity);
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
        //Update  PCustomerService
        public BaseResultBool ST_UDTO_UpdatePCustomerService(PCustomerService entity)
        {
            IBPCustomerService.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPCustomerService.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PCustomerService
        public BaseResultBool ST_UDTO_UpdatePCustomerServiceByField(PCustomerService entity, string fields)
        {
            IBPCustomerService.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if (entity.Id <= 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "ST_UDTO_UpdatePCustomerServiceByField：服务单ID为空！";
                    return tempBaseResultBool;
                }
                if (entity.Status == 0)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "ST_UDTO_UpdatePCustomerServiceByField：服务单状态为空！";
                    return tempBaseResultBool;
                }

                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPCustomerService.Entity, fields);
                    if (tempArray != null)
                    {

                        long EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                        string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                        tempBaseResultBool.success = IBPCustomerService.PCustomerServiceStatusUpdate((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, tempArray, EmpID, EmpName);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPCustomerService.Edit();
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
        //Delele  PCustomerService
        public BaseResultBool ST_UDTO_DelPCustomerService(long longPCustomerServiceID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPCustomerService.Remove(longPCustomerServiceID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPCustomerService(PCustomerService entity)
        {
            IBPCustomerService.Entity = entity;
            EntityList<PCustomerService> tempEntityList = new EntityList<PCustomerService>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPCustomerService.Search();
                tempEntityList.count = IBPCustomerService.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PCustomerService>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPCustomerServiceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PCustomerService> tempEntityList = new EntityList<PCustomerService>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPCustomerService.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPCustomerService.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PCustomerService>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPCustomerServiceById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPCustomerService.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PCustomerService>(tempEntity);
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

        #region PSalesManClientLink
        //Add  PSalesManClientLink
        public BaseResultDataValue ST_UDTO_AddPSalesManClientLink(PSalesManClientLink entity)
        {
            IBPSalesManClientLink.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPSalesManClientLink.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPSalesManClientLink.Get(IBPSalesManClientLink.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPSalesManClientLink.Entity);
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
        //Update  PSalesManClientLink
        public BaseResultBool ST_UDTO_UpdatePSalesManClientLink(PSalesManClientLink entity)
        {
            IBPSalesManClientLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPSalesManClientLink.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PSalesManClientLink
        public BaseResultBool ST_UDTO_UpdatePSalesManClientLinkByField(PSalesManClientLink entity, string fields)
        {
            IBPSalesManClientLink.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPSalesManClientLink.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPSalesManClientLink.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPSalesManClientLink.Edit();
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
        //Delele  PSalesManClientLink
        public BaseResultBool ST_UDTO_DelPSalesManClientLink(long longPSalesManClientLinkID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPSalesManClientLink.Remove(longPSalesManClientLinkID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPSalesManClientLink(PSalesManClientLink entity)
        {
            IBPSalesManClientLink.Entity = entity;
            EntityList<PSalesManClientLink> tempEntityList = new EntityList<PSalesManClientLink>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPSalesManClientLink.Search();
                tempEntityList.count = IBPSalesManClientLink.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PSalesManClientLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPSalesManClientLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PSalesManClientLink> tempEntityList = new EntityList<PSalesManClientLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPSalesManClientLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPSalesManClientLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PSalesManClientLink>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPSalesManClientLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPSalesManClientLink.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PSalesManClientLink>(tempEntity);
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

        #region PCustomerServiceOperationLog
        //Add  PCustomerServiceOperationLog
        public BaseResultDataValue ST_UDTO_AddPCustomerServiceOperationLog(PCustomerServiceOperationLog entity)
        {
            IBPCustomerServiceOperationLog.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPCustomerServiceOperationLog.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPCustomerServiceOperationLog.Get(IBPCustomerServiceOperationLog.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPCustomerServiceOperationLog.Entity);
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
        //Update  PCustomerServiceOperationLog
        public BaseResultBool ST_UDTO_UpdatePCustomerServiceOperationLog(PCustomerServiceOperationLog entity)
        {
            IBPCustomerServiceOperationLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPCustomerServiceOperationLog.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PCustomerServiceOperationLog
        public BaseResultBool ST_UDTO_UpdatePCustomerServiceOperationLogByField(PCustomerServiceOperationLog entity, string fields)
        {
            IBPCustomerServiceOperationLog.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPCustomerServiceOperationLog.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPCustomerServiceOperationLog.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPCustomerServiceOperationLog.Edit();
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
        //Delele  PCustomerServiceOperationLog
        public BaseResultBool ST_UDTO_DelPCustomerServiceOperationLog(long longPCustomerServiceOperationLogID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPCustomerServiceOperationLog.Remove(longPCustomerServiceOperationLogID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPCustomerServiceOperationLog(PCustomerServiceOperationLog entity)
        {
            IBPCustomerServiceOperationLog.Entity = entity;
            EntityList<PCustomerServiceOperationLog> tempEntityList = new EntityList<PCustomerServiceOperationLog>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPCustomerServiceOperationLog.Search();
                tempEntityList.count = IBPCustomerServiceOperationLog.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PCustomerServiceOperationLog>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPCustomerServiceOperationLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PCustomerServiceOperationLog> tempEntityList = new EntityList<PCustomerServiceOperationLog>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPCustomerServiceOperationLog.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPCustomerServiceOperationLog.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PCustomerServiceOperationLog>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPCustomerServiceOperationLogById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPCustomerServiceOperationLog.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PCustomerServiceOperationLog>(tempEntity);
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

        #region 附件上传及下载
        public Stream WM_UDTO_AttachmentDownLoad(long AttachmentID)
        {
            FileStream fileStream = null;
            try
            {
                string filePath = IBPProjectAttachment.GetAttachmentFilePath(AttachmentID);
                if (!string.IsNullOrEmpty(filePath))
                {
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    Encoding code = Encoding.GetEncoding("gb2312");
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                    //if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filePath);
                        //WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
                        //WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "attachment;filename=" + tempFileName);
                    }

                }
            }
            catch (Exception ex)
            {
                fileStream = null;
                throw new Exception(ex.Message);
            }
            return fileStream;
        }

        /// <summary>
        /// 上传文件DEMO
        /// </summary>
        /// <returns></returns>
        public Message WM_UploadDemo()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到文件！";
                    brdv.ResultDataValue = "";
                    brdv.success = false;
                }
                string fkObjectId = HttpContext.Current.Request.QueryString["FKObjectId"];
                string fkObjectName = HttpContext.Current.Request.QueryString["FKObjectName"];
                HttpPostedFile file = HttpContext.Current.Request.Files[0];
                int len = file.ContentLength;
                if (len > 0 && !string.IsNullOrEmpty(file.FileName))
                {
                    string parentPath = HttpContext.Current.Server.MapPath("~/UploadFiles/");
                    if (!Directory.Exists(parentPath))
                    {
                        Directory.CreateDirectory(parentPath);
                    }

                    string Test = HttpContext.Current.Request.Form["Test"];
                    string filepath = Path.Combine(parentPath, Path.GetFileName(file.FileName));
                    file.SaveAs(filepath);
                }
                else
                {
                    brdv.ErrorInfo = "文件大小为0或为空！";
                    brdv.success = false;
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = "";
                brdv.success = false;
            }
            ZhiFang.Common.Log.Log.Debug("上传测试");
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        public Message WM_UploadNewFiles()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string nullValue = "{id:'',fileSize:''}";
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                brdv.success = true;
                

                //需要保存的数据对象名称
                string objectName = "", fkObjectId = "", fkObjectName = "", fileName = "", newFileName = "", contentType = "", oldObjectId = "";
                objectName = HttpContext.Current.Request.Form["ObjectEName"];
                //需要保存的数据对象的子对象Id值
                fkObjectId = HttpContext.Current.Request.Form["FKObjectId"];
                //需要保存的数据对象的子对象名称
                fkObjectName = HttpContext.Current.Request.Form["FKObjectName"];

                //文件名称
                fileName = HttpContext.Current.Request.Form["FileName"];
                newFileName = HttpContext.Current.Request.Form["NewFileName"];
                //原附件的Id
                oldObjectId = HttpContext.Current.Request.Form["OldObjectId"];
                string businessModuleCode = HttpContext.Current.Request.Form["BusinessModuleCode"];
                int len = 0;
                int dispOrder = 0;
                string dispOrderStr = HttpContext.Current.Request.Form["DispOrder"];

                HttpPostedFile file = null;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到文件！";
                    brdv.ResultDataValue = nullValue;
                    //brdv.success = false;
                }
                else
                {
                    file = HttpContext.Current.Request.Files[0];
                }
                if (!string.IsNullOrEmpty(dispOrderStr)) {
                    dispOrder = int.Parse(dispOrderStr);
                }
                if (file != null)
                {
                    //file.FileName处理
                    //如果是IE传回来的是"H:\常用.txt"格式,需要处理为常用.txt;火狐传回的是常用.txt
                    len = file.ContentLength;
                    contentType = file.ContentType;
                    int startIndex = file.FileName.LastIndexOf(@"\");
                    startIndex = startIndex > -1 ? startIndex + 1 : startIndex;
                    if (string.IsNullOrEmpty(fileName))
                        fileName = startIndex > -1 ? file.FileName.Substring(startIndex) : file.FileName;
                    //ZhiFang.Common.Log.Log.Debug("FileName:" + fileName);
                }

                if (String.IsNullOrEmpty(objectName))
                {
                    brdv.ErrorInfo = "上传附件的数据对象名称为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && String.IsNullOrEmpty(fkObjectName))
                {
                    brdv.ErrorInfo = "上传附件的数据对象子对象名称为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && String.IsNullOrEmpty(fkObjectId))
                {
                    brdv.ErrorInfo = "上传附件的数据对象子对象值为空！";
                    brdv.ResultDataValue = nullValue;
                    brdv.success = false;
                }
                if (brdv.success && !string.IsNullOrEmpty(fkObjectId) &&(!string.IsNullOrEmpty(oldObjectId)||len > 0 )&& !string.IsNullOrEmpty(fileName))
                {
                    //上传附件路径
                    string parentPath = (string)IBBParameter.GetCache(BParameterParaNo.UploadFilesPath.ToString());
                    string tempPath = "\\" + objectName + "\\" + DateTime.Now.Year + "\\" + DateTime.Now.Month + "\\" + DateTime.Now.Day + "\\";
                    parentPath = parentPath + tempPath;
                    if (!Directory.Exists(parentPath))
                        Directory.CreateDirectory(parentPath);
                    string fileExt = fileName.Substring(fileName.LastIndexOf("."));

                    switch (objectName)
                    {
                        case "PProjectAttachment"://项目监控的附件表
                            PProjectAttachment model = new PProjectAttachment();
                            model.FileName = fileName;
                            model.FileSize = len;// / 1024;
                            model.FilePath = tempPath;
                            model.IsUse = true;
                            model.NewFileName = newFileName;
                            model.FileExt = fileExt;
                            model.FileType = contentType;
                            
                            // model.BusinessModuleCode = businessModuleCode;
                            brdv = AddPProjectAttachment(fkObjectId, fkObjectName, file, parentPath, tempPath, fileExt, model);
                            if (brdv.success)
                                brdv.ResultDataValue = "{id:" + "\"" + model.Id.ToString() + "\"" + ",fileSize:" + "\"" + len + "\"" + "}";
                            break;
                        case "FFileAttachment"://QMS文档附件表
                            FFileAttachment entity = new FFileAttachment();
                            entity.FileName = fileName;
                            entity.FileSize = len;// / 1024;
                            entity.FilePath = tempPath;
                            entity.IsUse = true;
                            entity.NewFileName = newFileName;
                            entity.FileExt = fileExt;
                            entity.FileType = contentType;
                            entity.DispOrder = dispOrder;
                            //entity.BusinessModuleCode = businessModuleCode;
                            brdv = IBFFileAttachment.AddFFileAttachment(fkObjectId, fkObjectName, file, parentPath, tempPath, fileExt, entity, oldObjectId);
                            //brdv.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBFFileAttachment.Entity);
                            if (brdv.success)
                                brdv.ResultDataValue = "{id:" + "\"" + entity.Id.ToString() + "\"" + ",fileSize:" + "\"" + len + "\"" + "}";
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                ZhiFang.Common.Log.Log.Error("QMS上传附件错误:" + ex.Message);
                brdv.ErrorInfo = ex.Message;
                brdv.ResultDataValue = nullValue;
                brdv.success = false;
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }
        /// <summary>
        /// 项目进度的附件信息保存
        /// </summary>
        /// <param name="fkObjectId"></param>
        /// <param name="fkObjectName"></param>
        /// <param name="file"></param>
        /// <param name="parentPath"></param>
        /// <param name="tempPath"></param>
        /// <param name="fileExt"></param>
        /// <param name="tempFileName"></param>
        /// <returns></returns>
        private BaseResultDataValue AddPProjectAttachment(string fkObjectId, string fkObjectName, HttpPostedFile file, string parentPath, string tempPath, string fileExt, PProjectAttachment entity)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            string employeeID = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
            if (employeeID != "-1" && !String.IsNullOrEmpty(employeeID))
            {
                entity.CreatorID = long.Parse(employeeID);
            }
            entity.CreatorName = ZhiFang.Common.Public.SessionHelper.GetSessionValue(DicCookieSession.EmployeeName);
            string fileName = entity.FileName.Substring(0, entity.FileName.LastIndexOf(".")) + "_" + entity.Id + fileExt + "." + FileExt.zf.ToString();
            switch (fkObjectName)
            {
                case "PTask":
                    PTask ptask = new PTask();
                    ptask.Id = long.Parse(fkObjectId);
                    ptask = IBPTask.Get(long.Parse(fkObjectId));
                    entity.PTask = ptask;
                    entity.TaskName = ptask.CName;
                    entity.LabID = ptask.LabID;
                    break;
                case "PWorkDayLog":
                    PWorkDayLog pworkDayLog = new PWorkDayLog();
                    pworkDayLog.Id = long.Parse(fkObjectId);
                    pworkDayLog = IBPWorkDayLog.Get(long.Parse(fkObjectId));
                    entity.PWorkDayLog = pworkDayLog;
                    entity.WorkTaskLogName = pworkDayLog.PTask.CName;
                    entity.LabID = pworkDayLog.LabID;
                    break;
                case "PTaskCopyFor":
                    PTaskCopyFor ptaskCopyFor = new PTaskCopyFor();
                    ptaskCopyFor.Id = long.Parse(fkObjectId);
                    break;
                default:
                    break;
            }
            entity.FilePath = entity.FilePath + fileName;
            try
            {
                //文件物理存储时，做一个处理：在文件名后+（.zf）,用来防止病毒文件挂在服务器直接执行
                string filepath = Path.Combine(parentPath, fileName);
                if (file != null)
                {
                    file.SaveAs(filepath);
                }
            }
            catch (Exception ex)
            {
                brdv.success = false;
                brdv.ResultDataValue = "{id:'',fileSize:''}";
                brdv.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("任务管理附件上传错误：" + ex.Message);
            }
            finally
            {
                if (brdv.success)
                {
                    IBPProjectAttachment.Entity = entity;
                    brdv.success = IBPProjectAttachment.Add();
                    //ZhiFang.Common.Log.Log.Debug("任务管理附件新增保存：" + brdv.success);
                    if (brdv.success)
                    {
                        IBPProjectAttachment.Get(IBPProjectAttachment.Entity.Id);
                        brdv.ResultDataValue = "{id:" + "\"" + entity.Id.ToString() + "\"" + ",fileSize:" + "\"" + file.ContentLength + "\"" + "}";
                    }
                }
            }
            return brdv;
        }

        /// <summary>
        /// 下载项目监控的附件表的文件
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>
        public Stream WM_UDTO_PProjectAttachmentDownLoadFiles(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filePath = "";
                PProjectAttachment file = IBPProjectAttachment.GetAttachmentFilePathAndFileName(id, ref filePath);
                if (!string.IsNullOrEmpty(filePath) && file != null)
                {
                    fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

                    Encoding code = Encoding.GetEncoding("gb2312");
                    //System.Web.HttpContext.Current.Response.Charset = "gb2312";
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;
                    //System.Web.HttpContext.Current.Response.HeaderEncoding = code;
                    string filename = file.NewFileName + file.FileExt;
                    if (string.IsNullOrEmpty(filename))
                    {
                        filename = file.FileName;
                    }
                    filename = EncodeFileName.ToEncodeFileName(filename);
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "" + file.FileType;
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "" + file.FileType;// "" + file.FileType;
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
                ZhiFang.Common.Log.Log.Error("任务管理附件下载错误:" + ex.Message);
                //throw new Exception(ex.Message);
            }
            return fileStream;
        }
        #endregion

        #region BDictTree
        //Add  BDictTree
        public BaseResultDataValue UDTO_AddBDictTree(BDictTree entity)
        {
            IBBDictTree.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBBDictTree.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBBDictTree.Get(IBBDictTree.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBBDictTree.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  BDictTree
        public BaseResultBool UDTO_UpdateBDictTree(BDictTree entity)
        {
            IBBDictTree.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBDictTree.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  BDictTree
        public BaseResultBool UDTO_UpdateBDictTreeByField(BDictTree entity, string fields)
        {
            IBBDictTree.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBBDictTree.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBBDictTree.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBBDictTree.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  BDictTree
        public BaseResultBool UDTO_DelBDictTree(long longBDictTreeID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBBDictTree.Remove(longBDictTreeID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue UDTO_SearchBDictTree(BDictTree entity)
        {
            IBBDictTree.Entity = entity;
            EntityList<BDictTree> tempEntityList = new EntityList<BDictTree>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBBDictTree.Search();
                tempEntityList.count = IBBDictTree.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BDictTree>(tempEntityList);
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
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //查询类型树ByHQL
        public BaseResultDataValue UDTO_SearchBDictTreeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<BDictTree> tempEntityList = new EntityList<BDictTree>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBBDictTree.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBBDictTree.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<BDictTree>(tempEntityList);
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

        public BaseResultDataValue UDTO_SearchBDictTreeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBBDictTree.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<BDictTree>(tempEntity);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region 文件类型树
        /// <summary>
        /// 根据某一节点id获取该节点及节点的子孙节点信息
        /// </summary>
        /// <param name="id">支持传入多个节点id值,如(1,2,4,5)</param>
        /// <param name="fields"></param>
        /// <param name="maxLevelStr">最大层数参数</param>
        /// <returns></returns>
        public BaseResultDataValue UDTO_SearchBDictTreeListTreeByIdListStr(string id, string idListStr, string fields, string maxLevelStr)
        {
            //BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            //BaseResultTree<BDictTree> tempBaseResultTree = new BaseResultTree<BDictTree>();
            ////string idListStr = "0";
            //try
            //{

            //    tempBaseResultTree = IBBDictTree.SearchBDictTreeListTree(id, idListStr, maxLevelStr);
            //    if (tempBaseResultTree.Tree != null && tempBaseResultTree.Tree.Count > 0)
            //    {
            //        ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
            //        try
            //        {
            //            tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectPropertyNoPlanish(tempBaseResultTree, fields);
            //        }
            //        catch (Exception ex)
            //        {
            //            tempBaseResultDataValue.success = false;
            //            tempBaseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    tempBaseResultDataValue.success = false;
            //    tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
            //    //throw new Exception(ex.Message);
            //}

            ////ZhiFang.Common.Log.Log.Debug("ResultDataValue:" + tempBaseResultDataValue.ResultDataValue);
            //return tempBaseResultDataValue;
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            BaseResultTree<BDictTree> baseResultTree = new BaseResultTree<BDictTree>();
            try
            {
                baseResultTree = this.IBBDictTree.SearchBDictTreeListTree(id, idListStr, maxLevelStr);
                bool flag = baseResultTree.Tree != null && baseResultTree.Tree.Count > 0;
                if (flag)
                {
                    ParseObjectProperty parseObjectProperty = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = parseObjectProperty.GetObjectPropertyNoPlanish<BaseResultTree<BDictTree>>(baseResultTree, fields);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    }
                }
            }
            catch (Exception ex2)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex2.Message;
            }
            return baseResultDataValue;
        }

        #endregion

        #region 微信消息推送
        public BaseResultDataValue PushWeiXinMessageTest(string openid, string first, string remark)
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string tmpopenid = "o8QEDt7e4VnTeLdY1AmyvVgyTIqs";
                if (openid != null && openid.Trim() != "")
                {
                    tmpopenid = openid;
                }
                IBPTask.Test((SysWeiXinTemplate.PushWeiXinMessage)PushWeiXinMessageTestAction, openid);
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

        public void PushWeiXinMessageTestAction(string openid, string templateid, string color, string url, Dictionary<string, TemplateDataObject> data)
        {
            string tid = templateid != null && templateid.Trim() != "" ? templateid : "C7B6d49l8_3aUdSfJkZWae47pEatmk3jW8eNM8Rulv8";
            string c = color != null && color.Trim() != "" ? color : "#336699";
            string u = url != null && url.Trim() != "" ? url : "";
            BasePage.PushMessageTemplate5Context(HttpContext.Current.Application, openid, tid, u, c, new TemplateIdObject5()
            {
                first = new TemplateDataObject() { value = "first" },
                keyword1 = new TemplateDataObject() { value = "keyword1" },
                keyword2 = new TemplateDataObject() { value = "keyword2" },
                keyword3 = new TemplateDataObject() { value = "keyword3" },
                keyword4 = new TemplateDataObject() { value = "keyword4" },
                keyword5 = new TemplateDataObject() { value = "keyword5" },
                remark = new TemplateDataObject() { value = "remark" }
            });
        }

        //public void PushWeiXinMessageAction(string openid, string templateid, string color, string url, Dictionary<string, TemplateDataObject> data)
        //{
        //    if (ConfigHelper.GetConfigString("PushMessageFlag") =="1")
        //    { string tid = (templateid != null && templateid.Trim() != "") ? templateid : "C7B6d49l8_3aUdSfJkZWae47pEatmk3jW8eNM8Rulv8";
        //        string c = (color != null && color.Trim() != "") ? color : "#336699";
        //        string u = (url != null && url.Trim() != "") ? url : "";
        //        BasePage.PushMessageTemplateContext(HttpContext.Current.Application, openid, tid, u, c, data);
        //    }
        //}
        #endregion

        #region 借款单打印
        public Stream PLoanBill_UDTO_PreviewPdf(long id, int operateType, bool isPreview, string templetName)
        {
            FileStream tempFileStream = null;
            string fileName = "借款单.pdf";
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (isPreview == true && string.IsNullOrEmpty(employeeID))
                {
                    ZhiFang.Common.Log.Log.Error("PLoanBill_UDTO_PreviewPdf：登录超时，请重新登录！");
                    throw new Exception("登录超时，请重新登录！");
                }
                else
                {
                    BaseResultDataValue baseResultDataValue = IBPLoanBill.ExcelToPdfFile(id, isPreview, templetName, ref fileName);
                    if (baseResultDataValue.success && (!string.IsNullOrEmpty(baseResultDataValue.ResultDataValue)))
                    {
                        string tempFilePath = baseResultDataValue.ResultDataValue;
                        tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.Charset = "gb2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;

                        if (operateType == 0) //下载文件
                        {
                            fileName = EncodeFileName.ToEncodeFileName(fileName);
                            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                        }
                        else if (operateType == 1)//直接打开PDF文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                fileName = "PDF预览.pdf";
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + fileName);
                            }
                            else
                            {
                                fileName = EncodeFileName.ToEncodeFileName(fileName);
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                            }

                        }
                    }
                }
                return tempFileStream;
            }
            catch (Exception ex)
            {
                string strError = "PLoanBill_UDTO_PreviewPdf异常：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("PLoanBill_UDTO_PreviewPdf异常：" + ex.ToString());
                byte[] bError = Encoding.UTF8.GetBytes(strError);
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
        }

        #endregion

        #region 发票打印
        public Stream PInvoice_UDTO_PreviewPdf(long id, int operateType, bool isPreview, string templetName)
        {
            FileStream tempFileStream = null;
            string fileName = "发票申请单.pdf";
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (isPreview == true && string.IsNullOrEmpty(employeeID))
                {
                    ZhiFang.Common.Log.Log.Error("PInvoice_UDTO_PreviewPdf：登录超时，请重新登录！");
                    throw new Exception("登录超时，请重新登录！");
                }
                else
                {
                    BaseResultDataValue baseResultDataValue = IBPInvoice.ExcelToPdfFile(id, isPreview, templetName, ref fileName);
                    if (baseResultDataValue.success && (!string.IsNullOrEmpty(baseResultDataValue.ResultDataValue)))
                    {
                        string tempFilePath = baseResultDataValue.ResultDataValue;
                        tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.Charset = "gb2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;

                        if (operateType == 0) //下载文件
                        {
                            fileName = EncodeFileName.ToEncodeFileName(fileName);
                            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                        }
                        else if (operateType == 1)//直接打开PDF文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                fileName = "PDF预览.pdf";
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + fileName);
                            }
                            else
                            {
                                fileName = EncodeFileName.ToEncodeFileName(fileName);
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                            }

                        }
                    }
                }
                return tempFileStream;
            }
            catch (Exception ex)
            {
                string strError = "PInvoice_UDTO_PreviewPdf：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("PInvoice_UDTO_PreviewPdf：" + ex.ToString());
                byte[] bError = Encoding.UTF8.GetBytes(strError);
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
        }
        #endregion

        #region 合同评审打印
        public Stream PContract_UDTO_PreviewPdf(long id, int operateType, bool isPreview, string templetName)
        {
            FileStream tempFileStream = null;
            string fileName = "合同评审.pdf";
            try
            {
                string employeeID = SessionHelper.GetSessionValue(DicCookieSession.EmployeeID);
                if (isPreview == true && string.IsNullOrEmpty(employeeID))
                {
                    ZhiFang.Common.Log.Log.Error("PInvoice_UDTO_PreviewPdf：登录超时，请重新登录！");
                    throw new Exception("登录超时，请重新登录！");
                }
                else
                {
                    BaseResultDataValue baseResultDataValue = IBPContract.ExcelToPdfFile(id, isPreview, templetName, ref fileName);
                    if (baseResultDataValue.success && (!string.IsNullOrEmpty(baseResultDataValue.ResultDataValue)))
                    {
                        string tempFilePath = baseResultDataValue.ResultDataValue;
                        tempFileStream = new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);

                        Encoding code = Encoding.GetEncoding("gb2312");
                        System.Web.HttpContext.Current.Response.Charset = "gb2312";
                        System.Web.HttpContext.Current.Response.ContentEncoding = code;

                        if (operateType == 0) //下载文件
                        {
                            fileName = EncodeFileName.ToEncodeFileName(fileName);
                            System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                            System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + fileName + "\"");
                        }
                        else if (operateType == 1)//直接打开PDF文件
                        {
                            WebOperationContext.Current.OutgoingResponse.ContentType = "application/pdf";
                            if (HttpContext.Current.Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") > 0)
                            {
                                fileName = "PDF预览.pdf";
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=" + fileName);
                            }
                            else
                            {
                                fileName = EncodeFileName.ToEncodeFileName(fileName);
                                WebOperationContext.Current.OutgoingResponse.Headers.Add("Content-Disposition", "inline;filename=\"" + fileName + "\"");
                            }

                        }
                    }
                }
                return tempFileStream;
            }
            catch (Exception ex)
            {
                string strError = "PContract_UDTO_PreviewPdf：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("PContract_UDTO_PreviewPdf：" + ex.ToString());
                byte[] bError = Encoding.UTF8.GetBytes(strError);
                MemoryStream memoryStream = new MemoryStream(bError);
                WebOperationContext.Current.OutgoingResponse.ContentType = "application/json";
                return memoryStream;
            }
        }
        #endregion

        #region 授权

        #region AHOperation
        //Add  AHOperation
        public BaseResultDataValue ST_UDTO_AddAHOperation(AHOperation entity)
        {
            IBAHOperation.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBAHOperation.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBAHOperation.Get(IBAHOperation.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBAHOperation.Entity);
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
        //Update  AHOperation
        public BaseResultBool ST_UDTO_UpdateAHOperation(AHOperation entity)
        {
            IBAHOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBAHOperation.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  AHOperation
        public BaseResultBool ST_UDTO_UpdateAHOperationByField(AHOperation entity, string fields)
        {
            IBAHOperation.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBAHOperation.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBAHOperation.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBAHOperation.Edit();
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
        //Delele  AHOperation
        public BaseResultBool ST_UDTO_DelAHOperation(long longAHOperationID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBAHOperation.Remove(longAHOperationID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchAHOperation(AHOperation entity)
        {
            IBAHOperation.Entity = entity;
            EntityList<AHOperation> tempEntityList = new EntityList<AHOperation>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBAHOperation.Search();
                tempEntityList.count = IBAHOperation.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AHOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchAHOperationByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<AHOperation> tempEntityList = new EntityList<AHOperation>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    string returnSortStr = "";
                    if (!sort.Contains("_"))
                    {
                        JArray tempJArray = JArray.Parse(sort);
                        List<string> sortList = new List<string>();
                        foreach (var tempObject in tempJArray)
                        {
                            sortList.Add(tempObject["property"].ToString() + " " + tempObject["direction"].ToString().ToUpper());
                        }
                        if (sortList.Count > 0)
                        {
                            returnSortStr = string.Join(",", sortList.ToArray());
                        }
                    }
                    else
                    {
                        returnSortStr = CommonServiceMethod.GetSortHQL(sort);
                    }
                    tempEntityList = IBAHOperation.SearchListByHQL(where, returnSortStr, page, limit);
                }
                else
                {
                    tempEntityList = IBAHOperation.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AHOperation>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchAHOperationById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBAHOperation.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<AHOperation>(tempEntity);
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

        #region AHServerEquipLicence
        //Add  AHServerEquipLicence
        public BaseResultDataValue ST_UDTO_AddAHServerEquipLicence(AHServerEquipLicence entity)
        {
            IBAHServerEquipLicence.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBAHServerEquipLicence.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBAHServerEquipLicence.Get(IBAHServerEquipLicence.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBAHServerEquipLicence.Entity);
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
        //Update  AHServerEquipLicence
        public BaseResultBool ST_UDTO_UpdateAHServerEquipLicence(AHServerEquipLicence entity)
        {
            IBAHServerEquipLicence.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBAHServerEquipLicence.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  AHServerEquipLicence
        public BaseResultBool ST_UDTO_UpdateAHServerEquipLicenceByField(AHServerEquipLicence entity, string fields)
        {
            IBAHServerEquipLicence.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBAHServerEquipLicence.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBAHServerEquipLicence.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBAHServerEquipLicence.Edit();
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
        //Delele  AHServerEquipLicence
        public BaseResultBool ST_UDTO_DelAHServerEquipLicence(long longAHServerEquipLicenceID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBAHServerEquipLicence.Remove(longAHServerEquipLicenceID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchAHServerEquipLicence(AHServerEquipLicence entity)
        {
            IBAHServerEquipLicence.Entity = entity;
            EntityList<AHServerEquipLicence> tempEntityList = new EntityList<AHServerEquipLicence>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBAHServerEquipLicence.Search();
                tempEntityList.count = IBAHServerEquipLicence.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AHServerEquipLicence>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchAHServerEquipLicenceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<AHServerEquipLicence> tempEntityList = new EntityList<AHServerEquipLicence>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    string returnSortStr = "";
                    if (!sort.Contains("_"))
                    {
                        JArray tempJArray = JArray.Parse(sort);
                        List<string> sortList = new List<string>();
                        foreach (var tempObject in tempJArray)
                        {
                            sortList.Add(tempObject["property"].ToString() + " " + tempObject["direction"].ToString().ToUpper());
                        }
                        if (sortList.Count > 0)
                        {
                            returnSortStr = string.Join(",", sortList.ToArray());
                        }
                    }
                    else
                    {
                        returnSortStr = CommonServiceMethod.GetSortHQL(sort);
                    }
                    tempEntityList = IBAHServerEquipLicence.SearchListByHQL(where, returnSortStr, page, limit);
                }
                else
                {
                    tempEntityList = IBAHServerEquipLicence.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AHServerEquipLicence>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchAHServerEquipLicenceById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBAHServerEquipLicence.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<AHServerEquipLicence>(tempEntity);
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

        #region AHServerLicence
        /// <summary>
        /// 服务器申请授权文件上传并返回处理好的申请信息
        /// </summary>
        /// <returns></returns>
        public Stream ST_UploadAHServerLicenceFile()
        {
            BaseResultDataValue tempBaseResultBool = new BaseResultDataValue();
            string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
            string pclientID = "";
            string licenceCode = "";
            int iTotal = HttpContext.Current.Request.Files.Count;
            HttpPostedFile file = null;
            if (HttpContext.Current.Request.Form.AllKeys.Contains("PClientID"))
            {
                pclientID = HttpContext.Current.Request.Form["PClientID"];
            }
            if (HttpContext.Current.Request.Form.AllKeys.Contains("LicenceCode"))
            {
                licenceCode = HttpContext.Current.Request.Form["LicenceCode"];
            }
            if (String.IsNullOrEmpty(pclientID))// || String.IsNullOrEmpty(licenceCode)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "没有获取授权申请的客户信息";
                ZhiFang.Common.Log.Log.Error(tempBaseResultBool.ErrorInfo);
            }
            if (tempBaseResultBool.success)
            {
                if (iTotal > 0)
                {
                    file = HttpContext.Current.Request.Files[0];
                    try
                    {
                        long EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                        string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                        ZhiFang.Common.Log.Log.Debug("ST_UploadAHServerLicenceFile.Emp:"+ EmpID + ",EmpName:"+ EmpName+ ",pclientID:"+ pclientID);
                        tempBaseResultBool = IBAHServerLicence.UploadAHServerLicenceFile(file, long.Parse(pclientID), licenceCode);
                        if (tempBaseResultBool.success)
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        tempBaseResultBool.success = false;
                        tempBaseResultBool.ErrorInfo = "上传服务器授权文件出错:" + ex.Message;
                        ZhiFang.Common.Log.Log.Error(ex.ToString());
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：没有获取上传服务器授权文件";
                    ZhiFang.Common.Log.Log.Error(tempBaseResultBool.ErrorInfo);
                }
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(tempBaseResultBool);
            return ResponseResultStream.GetResultInfoOfStream(strResult);
        }
        public BaseResultDataValue ST_UDTO_AddAHAHServerLicenceAndDetails(ApplyAHServerLicence entity)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            if (entity.AHServerLicence == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "授权申请信息(AHServerLicence)为空!";
                return tempBaseResultDataValue;
            }
            else if (entity.ApplyProgramInfoList == null && entity.AHServerEquipLicenceList == null)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "授权申请的程序明细信息和通讯明细信息为空!";
                return tempBaseResultDataValue;
            }

            try
            {
                long EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                if (!entity.AHServerLicence.ApplyID.HasValue || String.IsNullOrEmpty(entity.AHServerLicence.ApplyName))
                {
                    entity.AHServerLicence.ApplyID = EmpID;
                    entity.AHServerLicence.ApplyName = EmpName;
                }

                tempBaseResultDataValue = IBAHServerLicence.AddAHAHServerLicenceAndDetails((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, entity);
                if (tempBaseResultDataValue.success)
                {
                    IBAHServerLicence.Get(IBAHServerLicence.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBAHServerLicence.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_AddAHAHServerLicenceAndDetails.Error:服务器授权ID：" + entity.AHServerLicence.Id  + ",出错:" + ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        /// <summary>
        /// 修改服务器授权信息及明细信息(包括手工追加的程序明细)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_UpdateAHServerLicenceAndDetailsByField(ApplyAHServerLicence entity, string fields)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            if (entity == null || entity.AHServerLicence == null)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：entity参数不能为空！";
            }
            else
            {
                IBAHServerLicence.Entity = entity.AHServerLicence;
            }
            if (tempBaseResultBool.success && String.IsNullOrEmpty(fields))
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
            }
            if (tempBaseResultBool.success)
            {
                try
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBAHServerLicence.Entity, fields);
                    if (tempArray != null)
                    {
                        long EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                        string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                        tempBaseResultBool = IBAHServerLicence.UpdateAHServerLicenceAndDetails((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, entity, tempArray, EmpID, EmpName);
                    }
                }
                catch (Exception ex)
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                    //throw new Exception(ex.Message);
                    ZhiFang.Common.Log.Log.Error("服务器授权ID：" + entity.AHServerLicence.Id + "的状态为：" + LicenceStatus.GetStatusDic()[entity.AHServerLicence.Status.ToString()].Name + "出错:" + ex.StackTrace);
                }
            }
            return tempBaseResultBool;
        }
        //Add  AHServerLicence
        public BaseResultDataValue ST_UDTO_AddAHServerLicence(AHServerLicence entity)
        {
            IBAHServerLicence.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBAHServerLicence.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBAHServerLicence.Get(IBAHServerLicence.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBAHServerLicence.Entity);
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
        //Update  AHServerLicence
        public BaseResultBool ST_UDTO_UpdateAHServerLicence(AHServerLicence entity)
        {
            IBAHServerLicence.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBAHServerLicence.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  AHServerLicence
        public BaseResultBool ST_UDTO_UpdateAHServerLicenceByField(AHServerLicence entity, string fields)
        {
            IBAHServerLicence.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBAHServerLicence.Entity, fields);
                    if (tempArray != null)
                    {
                        long EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                        string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                        tempBaseResultBool = IBAHServerLicence.AHServerLicenceStatusUpdate((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, tempArray, EmpID, EmpName);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBAHServerLicence.Edit();
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
        //Delele  AHServerLicence
        public BaseResultBool ST_UDTO_DelAHServerLicence(long longAHServerLicenceID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBAHServerLicence.Remove(longAHServerLicenceID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchAHServerLicence(AHServerLicence entity)
        {
            IBAHServerLicence.Entity = entity;
            EntityList<AHServerLicence> tempEntityList = new EntityList<AHServerLicence>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBAHServerLicence.Search();
                tempEntityList.count = IBAHServerLicence.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AHServerLicence>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchAHServerLicenceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<AHServerLicence> tempEntityList = new EntityList<AHServerLicence>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    string returnSortStr = "";
                    if (!sort.Contains("_"))
                    {
                        JArray tempJArray = JArray.Parse(sort);
                        List<string> sortList = new List<string>();
                        foreach (var tempObject in tempJArray)
                        {
                            sortList.Add(tempObject["property"].ToString() + " " + tempObject["direction"].ToString().ToUpper());
                        }
                        if (sortList.Count > 0)
                        {
                            returnSortStr = string.Join(",", sortList.ToArray());
                        }
                    }
                    else
                    {
                        returnSortStr = CommonServiceMethod.GetSortHQL(sort);
                    }
                    //tempEntityList = IBAHServerLicence.SearchListByHQL(where, returnSortStr, page, limit);
                    tempEntityList = IBAHServerLicence.SearchListByHQL_LicenceInfo(where, returnSortStr, page, limit);
                }
                else
                {
                    //tempEntityList = IBAHServerLicence.SearchListByHQL(where, page, limit);
                    tempEntityList = IBAHServerLicence.SearchListByHQL_LicenceInfo(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AHServerLicence>(tempEntityList);
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
         public BaseResultDataValue ST_UDTO_SearchAHServerLicenceByDocAndDtlHQL(int page, int limit, string fields, string where, string dtlWhere, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<AHServerLicence> tempEntityList = new EntityList<AHServerLicence>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    string returnSortStr = "";
                    if (!sort.Contains("_"))
                    {
                        JArray tempJArray = JArray.Parse(sort);
                        List<string> sortList = new List<string>();
                        foreach (var tempObject in tempJArray)
                        {
                            sortList.Add(tempObject["property"].ToString() + " " + tempObject["direction"].ToString().ToUpper());
                        }
                        if (sortList.Count > 0)
                        {
                            returnSortStr = string.Join(",", sortList.ToArray());
                        }
                    }
                    else
                    {
                        returnSortStr = CommonServiceMethod.GetSortHQL(sort);
                    }
                    tempEntityList = IBAHServerLicence.SearchListByDocAndDtlHQL_LicenceInfo(where, dtlWhere, returnSortStr, page, limit);
                }
                else
                {
                    tempEntityList = IBAHServerLicence.SearchListByDocAndDtlHQL_LicenceInfo(where, dtlWhere, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AHServerLicence>(tempEntityList);
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
                ZhiFang.Common.Log.Log.Error("Error:"+ ex.StackTrace);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultBool ST_UDTO_RegenerateAHServerLicenceById(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if (id > 0)
                {
                    tempBaseResultBool = IBAHServerLicence.RegenerateAHServerLicenceById(id);
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：id参数不能为空！";
                    //tempBaseResultBool.success = IBAHServerLicence.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("Error:" + ex.StackTrace);
            }
            return tempBaseResultBool;
        }

        /// <summary>
        /// 获取服务器授权需要特批的数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_SearchSpecialApprovalAHServerLicenceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<AHServerLicence> tempEntityList = new EntityList<AHServerLicence>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    string returnSortStr = "";
                    if (!sort.Contains("_"))
                    {
                        JArray tempJArray = JArray.Parse(sort);
                        List<string> sortList = new List<string>();
                        foreach (var tempObject in tempJArray)
                        {
                            sortList.Add(tempObject["property"].ToString() + " " + tempObject["direction"].ToString().ToUpper());
                        }
                        if (sortList.Count > 0)
                        {
                            returnSortStr = string.Join(",", sortList.ToArray());
                        }
                    }
                    else
                    {
                        returnSortStr = CommonServiceMethod.GetSortHQL(sort);
                    }
                    tempEntityList = IBAHServerLicence.SearchSpecialApprovalAHServerLicenceByHQL(where, page, limit, returnSortStr);
                }
                else
                {
                    tempEntityList = IBAHServerLicence.SearchSpecialApprovalAHServerLicenceByHQL(where, page, limit, null);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AHServerLicence>(tempEntityList);
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
        public BaseResultDataValue ST_UDTO_SearchAHServerLicenceAndAndDetailsById(long id)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                try
                {
                    tempBaseResultDataValue = IBAHServerLicence.SearchAHServerLicenceAndAndDetailsById(id);
                }
                catch (Exception ex)
                {
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchAHServerLicenceAndAndDetailsById.异常:" + ex.ToString());
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
        public BaseResultDataValue ST_UDTO_SearchAHServerLicenceById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBAHServerLicence.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<AHServerLicence>(tempEntity);
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
        /// <summary>
        /// 下载服务器授权文件
        /// </summary>
        /// <param name="id">附件ID</param>
        /// <param name="operateType">0:</param>
        /// <returns></returns>
        public Stream ST_UDTO_DownLoadAHServerLicenceFile(long id, long operateType)
        {
            FileStream fileStream = null;
            try
            {
                string filename = id + ".txt";
                fileStream = IBAHServerLicence.DownLoadAHServerLicenceFile(id, ref filename);
                //获取错误提示信息
                if (fileStream == null)
                {
                    string errorInfo = "服务器授权返回文件不存在!请联系管理员。";
                    MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                    return memoryStream;
                }
                else
                {
                    Encoding code = Encoding.GetEncoding("gb2312");//
                    System.Web.HttpContext.Current.Response.ContentEncoding = code;// Encoding.Default;
                    System.Web.HttpContext.Current.Response.HeaderEncoding = code;// Encoding.Default;

                    filename = EncodeFileName.ToEncodeFileName(filename);
                    if (operateType == 0) //下载文件
                    {
                        System.Web.HttpContext.Current.Response.ContentType = "application/octet-stream";
                        System.Web.HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filename + "\"");
                    }
                    else if (operateType == 1)//直接打开文件
                    {
                        WebOperationContext.Current.OutgoingResponse.ContentType = "application/octet-stream";
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
                    //fileStream.Close();
                }
                return fileStream;
            }
            catch (Exception ex)
            {
                string errorInfo = "服务器授权返回文件不存在!请联系管理员。";
                ZhiFang.Common.Log.Log.Error("下载服务器授权返回文件错误:" + ex.Message);
                MemoryStream memoryStream = ResponseResultStream.GetErrMemoryStreamInfo(id, errorInfo);
                return memoryStream;
            }

        }

        #endregion

        #region AHServerProgramLicence
        //Add  AHServerProgramLicence
        public BaseResultDataValue ST_UDTO_AddAHServerProgramLicence(AHServerProgramLicence entity)
        {
            IBAHServerProgramLicence.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBAHServerProgramLicence.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBAHServerProgramLicence.Get(IBAHServerProgramLicence.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBAHServerProgramLicence.Entity);
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
        //Update  AHServerProgramLicence
        public BaseResultBool ST_UDTO_UpdateAHServerProgramLicence(AHServerProgramLicence entity)
        {
            IBAHServerProgramLicence.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBAHServerProgramLicence.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  AHServerProgramLicence
        public BaseResultBool ST_UDTO_UpdateAHServerProgramLicenceByField(AHServerProgramLicence entity, string fields)
        {
            IBAHServerProgramLicence.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBAHServerProgramLicence.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBAHServerProgramLicence.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBAHServerProgramLicence.Edit();
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
        //Delele  AHServerProgramLicence
        public BaseResultBool ST_UDTO_DelAHServerProgramLicence(long longAHServerProgramLicenceID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBAHServerProgramLicence.Remove(longAHServerProgramLicenceID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchAHServerProgramLicence(AHServerProgramLicence entity)
        {
            IBAHServerProgramLicence.Entity = entity;
            EntityList<AHServerProgramLicence> tempEntityList = new EntityList<AHServerProgramLicence>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBAHServerProgramLicence.Search();
                tempEntityList.count = IBAHServerProgramLicence.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AHServerProgramLicence>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchAHServerProgramLicenceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<AHServerProgramLicence> tempEntityList = new EntityList<AHServerProgramLicence>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    string returnSortStr = "";
                    if (!sort.Contains("_"))
                    {
                        JArray tempJArray = JArray.Parse(sort);
                        List<string> sortList = new List<string>();
                        foreach (var tempObject in tempJArray)
                        {
                            sortList.Add(tempObject["property"].ToString() + " " + tempObject["direction"].ToString().ToUpper());
                        }
                        if (sortList.Count > 0)
                        {
                            returnSortStr = string.Join(",", sortList.ToArray());
                        }
                    }
                    else
                    {
                        returnSortStr = CommonServiceMethod.GetSortHQL(sort);
                    }
                    tempEntityList = IBAHServerProgramLicence.SearchListByHQL(where, returnSortStr, page, limit);
                }
                else
                {
                    tempEntityList = IBAHServerProgramLicence.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AHServerProgramLicence>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchAHServerProgramLicenceById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBAHServerProgramLicence.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<AHServerProgramLicence>(tempEntity);
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

        #region AHSingleLicence
        //Add  AHSingleLicence
        public BaseResultDataValue ST_UDTO_AddAHSingleLicence(AHSingleLicence entity)
        {
            entity.LabID = 1;
            IBAHSingleLicence.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBAHSingleLicence.AHSingleLicenceAdd((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction);
                if (tempBaseResultDataValue.success)
                {
                    IBAHSingleLicence.Get(IBAHSingleLicence.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBAHSingleLicence.Entity);
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
        //Update  AHSingleLicence
        //public BaseResultBool ST_UDTO_UpdateAHSingleLicence(AHSingleLicence entity)
        //{
        //    IBAHSingleLicence.Entity = entity;
        //    BaseResultBool tempBaseResultBool = new BaseResultBool();
        //    try
        //    {
        //        tempBaseResultBool.success = IBAHSingleLicence.Edit();
        //    }
        //    catch (Exception ex)
        //    {
        //        tempBaseResultBool.success = false;
        //        tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
        //        //throw new Exception(ex.Message);
        //    }
        //    return tempBaseResultBool;
        //}
        //Update  AHSingleLicence
        public BaseResultBool ST_UDTO_UpdateAHSingleLicenceByField(AHSingleLicence entity, string fields)
        {
            IBAHSingleLicence.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBAHSingleLicence.Entity, fields);

                    if (tempArray != null)
                    {
                        long EmpID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                        string EmpName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                        tempBaseResultBool = IBAHSingleLicence.AHSingleLicenceStatusUpdate((SysWeiXinTemplate.PushWeiXinMessage)BasePage.PushWeiXinMessageAction, tempArray, EmpID, EmpName);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBAHSingleLicence.Edit();
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
        //Delele  AHSingleLicence
        public BaseResultBool ST_UDTO_DelAHSingleLicence(long longAHSingleLicenceID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            //try
            //{
            //    tempBaseResultBool.success = IBAHSingleLicence.Remove(longAHSingleLicenceID);
            //}
            //catch (Exception ex)
            //{
            //    tempBaseResultBool.success = false;
            //    tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
            //    //throw new Exception(ex.Message);
            //}
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchAHSingleLicence(AHSingleLicence entity)
        {
            IBAHSingleLicence.Entity = entity;
            EntityList<AHSingleLicence> tempEntityList = new EntityList<AHSingleLicence>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBAHSingleLicence.Search();
                tempEntityList.count = IBAHSingleLicence.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AHSingleLicence>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchAHSingleLicenceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<AHSingleLicence> tempEntityList = new EntityList<AHSingleLicence>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    string returnSortStr = "";
                    if (!sort.Contains("_"))
                    {
                        JArray tempJArray = JArray.Parse(sort);
                        List<string> sortList = new List<string>();
                        foreach (var tempObject in tempJArray)
                        {
                            sortList.Add(tempObject["property"].ToString() + " " + tempObject["direction"].ToString().ToUpper());
                        }
                        if (sortList.Count > 0)
                        {
                            returnSortStr = string.Join(",", sortList.ToArray());
                        }
                    }
                    else
                    {
                        returnSortStr = CommonServiceMethod.GetSortHQL(sort);
                    }
                    //tempEntityList = IBAHSingleLicence.SearchListByHQL(where, returnSortStr, page, limit);
                    tempEntityList = IBAHSingleLicence.SearchListByHQL_LicenceInfo(where, returnSortStr, page, limit);
                }
                else
                {
                    //tempEntityList = IBAHSingleLicence.SearchListByHQL(where, page, limit);
                    tempEntityList = IBAHSingleLicence.SearchListByHQL_LicenceInfo(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AHSingleLicence>(tempEntityList);
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
                    ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchAHSingleLicenceByHQL.序列化错误.异常:" + ex.ToString());
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                ZhiFang.Common.Log.Log.Error("ST_UDTO_SearchAHSingleLicenceByHQL.异常:" + ex.ToString());
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        public BaseResultDataValue ST_UDTO_GetStartDateValueOfApply(string mac, string sqh)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBAHSingleLicence.GetStartDateValueOfApply(mac, sqh);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                tempBaseResultDataValue.ResultDataValue = "{StartDate:" + "\"" + DateTime.Now.ToString("yyyy-MM-dd") + "\"" + "}";
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        /// <summary>
        /// 获取单站点需要特批的数据
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_SearchSpecialApprovalAHSingleLicenceByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<AHSingleLicence> tempEntityList = new EntityList<AHSingleLicence>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    string returnSortStr = "";
                    if (!sort.Contains("_"))
                    {
                        JArray tempJArray = JArray.Parse(sort);
                        List<string> sortList = new List<string>();
                        foreach (var tempObject in tempJArray)
                        {
                            sortList.Add(tempObject["property"].ToString() + " " + tempObject["direction"].ToString().ToUpper());
                        }
                        if (sortList.Count > 0)
                        {
                            returnSortStr = string.Join(",", sortList.ToArray());
                        }
                    }
                    else
                    {
                        returnSortStr = CommonServiceMethod.GetSortHQL(sort);
                    }
                    tempEntityList = IBAHSingleLicence.SearchSpecialApprovalAHSingleLicenceByHQL(where, page, limit, returnSortStr);
                }
                else
                {
                    tempEntityList = IBAHSingleLicence.SearchSpecialApprovalAHSingleLicenceByHQL(where, page, limit, null);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<AHSingleLicence>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchAHSingleLicenceById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBAHSingleLicence.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<AHSingleLicence>(tempEntity);
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
        /// <summary>
        /// 授权截止日期节假日顺延处理
        /// </summary>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue ST_UDTO_GetLicenceEndDate(string endDate)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            tempBaseResultDataValue.success = true;
            if (String.IsNullOrEmpty(endDate))
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ResultDataValue = "{ \"EndDate\":\"" + "\"" + "}";
                tempBaseResultDataValue.ErrorInfo = "授权截止日期为空";
            }
            if (tempBaseResultDataValue.success)
            {
                try
                {
                    tempBaseResultDataValue = IBATHolidaySetting.GetLicenceEndDate(endDate);
                }
                catch (Exception ex)
                {
                    tempBaseResultDataValue.success = false;
                    tempBaseResultDataValue.ResultDataValue = "{ \"EndDate\":\"" + "\"" + "}";
                    tempBaseResultDataValue.ErrorInfo = "处理授权截止日期节假日出错：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }

            return tempBaseResultDataValue;
        }
        #endregion

        #endregion

        #region PProject
        //Add  PProject
        public BaseResultDataValue ST_UDTO_AddPProject(PProject entity)
        {
            IBPProject.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                IBPProject.CalcProcessing();
                tempBaseResultDataValue.success = IBPProject.AddProject(entity);
                if (tempBaseResultDataValue.success)
                {
                    IBPProject.Get(IBPProject.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPProject.Entity);
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
        //Update  PProject
        public BaseResultBool ST_UDTO_UpdatePProject(PProject entity)
        {
            IBPProject.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                IBPProject.CalcProcessing();
                tempBaseResultBool.success = IBPProject.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PProject
        public BaseResultBool ST_UDTO_UpdatePProjectByField(PProject entity, string fields)
        {
            IBPProject.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    IBPProject.CalcProcessing();
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPProject.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPProject.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPProject.Edit();
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
        //Delele  PProject
        public BaseResultBool ST_UDTO_DelPProject(long longPProjectID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPProject.Remove(longPProjectID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPProject(PProject entity)
        {
            IBPProject.Entity = entity;
            EntityList<PProject> tempEntityList = new EntityList<PProject>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPProject.Search();
                tempEntityList.count = IBPProject.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PProject>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPProjectByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PProject> tempEntityList = new EntityList<PProject>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPProject.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPProject.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PProject>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPProjectById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPProject.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PProject>(tempEntity);
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

        #region PProjectTask
        //Add  PProjectTask
        public BaseResultDataValue ST_UDTO_AddPProjectTask(PProjectTask entity)
        {
            IBPProjectTask.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPProjectTask.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPProjectTask.Get(IBPProjectTask.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPProjectTask.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  PProjectTask
        public BaseResultBool ST_UDTO_UpdatePProjectTask(PProjectTask entity)
        {
            IBPProjectTask.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPProjectTask.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PProjectTask
        public BaseResultBool ST_UDTO_UpdatePProjectTaskByField(PProjectTask entity, string fields)
        {
            IBPProjectTask.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPProjectTask.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPProjectTask.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPProjectTask.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  PProjectTask
        public BaseResultBool ST_UDTO_DelPProjectTask(long longPProjectTaskID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPProjectTask.Remove(longPProjectTaskID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPProjectTask(PProjectTask entity)
        {
            IBPProjectTask.Entity = entity;
            EntityList<PProjectTask> tempEntityList = new EntityList<PProjectTask>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPProjectTask.Search();
                tempEntityList.count = IBPProjectTask.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PProjectTask>(tempEntityList);
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
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchPProjectTaskByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PProjectTask> tempEntityList = new EntityList<PProjectTask>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPProjectTask.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPProjectTask.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PProjectTask>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPProjectTaskById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPProjectTask.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PProjectTask>(tempEntity);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region PProjectTaskProgress
        //Add  PProjectTaskProgress
        public BaseResultDataValue ST_UDTO_AddPProjectTaskProgress(PProjectTaskProgress entity)
        {
            IBPProjectTaskProgress.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPProjectTaskProgress.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPProjectTaskProgress.Get(IBPProjectTaskProgress.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPProjectTaskProgress.Entity);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        //Update  PProjectTaskProgress
        public BaseResultBool ST_UDTO_UpdatePProjectTaskProgress(PProjectTaskProgress entity)
        {
            IBPProjectTaskProgress.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPProjectTaskProgress.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PProjectTaskProgress
        public BaseResultBool ST_UDTO_UpdatePProjectTaskProgressByField(PProjectTaskProgress entity, string fields)
        {
            IBPProjectTaskProgress.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPProjectTaskProgress.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPProjectTaskProgress.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPProjectTaskProgress.Edit();
                }
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Delele  PProjectTaskProgress
        public BaseResultBool ST_UDTO_DelPProjectTaskProgress(long longPProjectTaskProgressID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPProjectTaskProgress.Remove(longPProjectTaskProgressID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPProjectTaskProgress(PProjectTaskProgress entity)
        {
            IBPProjectTaskProgress.Entity = entity;
            EntityList<PProjectTaskProgress> tempEntityList = new EntityList<PProjectTaskProgress>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPProjectTaskProgress.Search();
                tempEntityList.count = IBPProjectTaskProgress.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PProjectTaskProgress>(tempEntityList);
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
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue ST_UDTO_SearchPProjectTaskProgressByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PProjectTaskProgress> tempEntityList = new EntityList<PProjectTaskProgress>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPProjectTaskProgress.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPProjectTaskProgress.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PProjectTaskProgress>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPProjectTaskProgressById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPProjectTaskProgress.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PProjectTaskProgress>(tempEntity);
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
                    throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }
        #endregion

        #region PProjectDocument
        //Add  PProjectDocument
        public BaseResultDataValue ST_UDTO_AddPProjectDocument(PProjectDocument entity)
        {
            IBPProjectDocument.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBPProjectDocument.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBPProjectDocument.Get(IBPProjectDocument.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBPProjectDocument.Entity);
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
        //Update  PProjectDocument
        public BaseResultBool ST_UDTO_UpdatePProjectDocument(PProjectDocument entity)
        {
            IBPProjectDocument.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPProjectDocument.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  PProjectDocument
        public BaseResultBool ST_UDTO_UpdatePProjectDocumentByField(PProjectDocument entity, string fields)
        {
            IBPProjectDocument.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBPProjectDocument.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBPProjectDocument.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBPProjectDocument.Edit();
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
        //Delele  PProjectDocument
        public BaseResultBool ST_UDTO_DelPProjectDocument(long longPProjectDocumentID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBPProjectDocument.Remove(longPProjectDocumentID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchPProjectDocument(PProjectDocument entity)
        {
            IBPProjectDocument.Entity = entity;
            EntityList<PProjectDocument> tempEntityList = new EntityList<PProjectDocument>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBPProjectDocument.Search();
                tempEntityList.count = IBPProjectDocument.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PProjectDocument>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPProjectDocumentByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<PProjectDocument> tempEntityList = new EntityList<PProjectDocument>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBPProjectDocument.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBPProjectDocument.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<PProjectDocument>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchPProjectDocumentById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBPProjectDocument.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<PProjectDocument>(tempEntity);
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

        #region CUser
        //Add  CUser
        public BaseResultDataValue ST_UDTO_AddCUser(CUser entity)
        {
            IBCUser.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBCUser.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBCUser.Get(IBCUser.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBCUser.Entity);
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
        //Update  CUser
        public BaseResultBool ST_UDTO_UpdateCUser(CUser entity)
        {
            IBCUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCUser.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  CUser
        public BaseResultBool ST_UDTO_UpdateCUserByField(CUser entity, string fields)
        {
            IBCUser.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBCUser.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBCUser.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBCUser.Edit();
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
        //Delele  CUser
        public BaseResultBool ST_UDTO_DelCUser(long id)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBCUser.Remove(id);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchCUser(CUser entity)
        {
            IBCUser.Entity = entity;
            EntityList<CUser> tempEntityList = new EntityList<CUser>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBCUser.Search();
                tempEntityList.count = IBCUser.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CUser>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCUserByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<CUser> tempEntityList = new EntityList<CUser>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBCUser.SearchListByHQL(where, CommonServiceMethod.GetExpandSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBCUser.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<CUser>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchCUserById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBCUser.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<CUser>(tempEntity);
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
        /// <summary>
        /// 将CUser某一记录行复制到PClient中
        /// </summary>
        /// <param name="cuserId"></param>
        /// <returns></returns>
        public BaseResultBool ST_UDTO_CopyCUserToPClientByCUserId(long id, int type)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                long empID = long.Parse(ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID));
                string empName = ZhiFang.Common.Public.Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                tempBaseResultBool = IBCUser.CopyCUserToPClientByCUserId(id, type, empID, empName);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
                ZhiFang.Common.Log.Log.Error("CUser复制到PClient出错:" + ex.Message);
            }
            return tempBaseResultBool;
        }
        #endregion

        #region 项目管理定制服务

        public BaseResultDataValue PM_UDTO_CopyProject(long projectID, long typeID, bool isStandard)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPProject.AddCopyProjectByID(projectID, typeID, isStandard);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue PM_UDTO_CopyStandardTask(long projectID)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPProject.AddStandardTask(projectID);
            }
            catch (Exception ex)
            {
                tempBaseResultDataValue.success = false;
                tempBaseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultDataValue;
        }

        public BaseResultDataValue PM_UDTO_CopyProjectTask(long fromProjectID, long toProjectID, bool isStandard)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue = IBPProject.AddProjectTaskByProjectID(fromProjectID, toProjectID, isStandard);
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

    }
}
