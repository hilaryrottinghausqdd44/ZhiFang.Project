using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Web;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LIIP;
using ZhiFang.Entity.LIIP.ViewObject.Request;
using ZhiFang.IBLL.LIIP;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabInformationIntegratePlatform.ServerWCF
{  
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WXService : ServerContract.IWXService
    {

        IBWXAccountType IBWXAccountType { get; set; }

        IBWXIcons IBWXIcons { get; set; }

        IBWXMessageSendTask IBWXMessageSendTask { get; set; }

        IBWXMsgSendLog IBWXMsgSendLog { get; set; }

        IBWXWeiXinEmpLink IBWXWeiXinEmpLink { get; set; }

        IBWXWeiXinPushMessageTemplate IBWXWeiXinPushMessageTemplate { get; set; }

        IBWXWeiXinAccount IBWXWeiXinAccount { get; set; }

        IBWXWeiXinUserGroup IBWXWeiXinUserGroup { get; set; }


        #region WXAccountType
        //Add  WXAccountType
        public BaseResultDataValue ST_UDTO_AddWXAccountType(WXAccountType entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBWXAccountType.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBWXAccountType.Add();
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
        //Update  WXAccountType
        public BaseResultBool ST_UDTO_UpdateWXAccountType(WXAccountType entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBWXAccountType.Entity = entity;
                try
                {
                    baseResultBool.success = IBWXAccountType.Edit();
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
        //Update  WXAccountType
        public BaseResultBool ST_UDTO_UpdateWXAccountTypeByField(WXAccountType entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBWXAccountType.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBWXAccountType.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBWXAccountType.Update(tempArray);
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
                        //baseResultBool.success = IBWXAccountType.Edit();
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
        //Delele  WXAccountType
        public BaseResultBool ST_UDTO_DelWXAccountType(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBWXAccountType.Entity = IBWXAccountType.Get(id);
                if (IBWXAccountType.Entity != null)
                {
                    long labid = IBWXAccountType.Entity.LabID;
                    string entityName = IBWXAccountType.Entity.GetType().Name;
                    baseResultBool.success = IBWXAccountType.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchWXAccountType(WXAccountType entity)
        {
            EntityList<WXAccountType> entityList = new EntityList<WXAccountType>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBWXAccountType.Entity = entity;
                try
                {
                    entityList.list = IBWXAccountType.Search();
                    entityList.count = IBWXAccountType.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXAccountType>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXAccountTypeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<WXAccountType> entityList = new EntityList<WXAccountType>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBWXAccountType.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBWXAccountType.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXAccountType>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXAccountTypeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBWXAccountType.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<WXAccountType>(entity);
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

        #region WXIcons
        //Add  WXIcons
        public BaseResultDataValue ST_UDTO_AddWXIcons(WXIcons entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBWXIcons.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBWXIcons.Add();
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
        //Update  WXIcons
        public BaseResultBool ST_UDTO_UpdateWXIcons(WXIcons entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBWXIcons.Entity = entity;
                try
                {
                    baseResultBool.success = IBWXIcons.Edit();
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
        //Update  WXIcons
        public BaseResultBool ST_UDTO_UpdateWXIconsByField(WXIcons entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBWXIcons.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBWXIcons.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBWXIcons.Update(tempArray);
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
                        //baseResultBool.success = IBWXIcons.Edit();
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
        //Delele  WXIcons
        public BaseResultBool ST_UDTO_DelWXIcons(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBWXIcons.Entity = IBWXIcons.Get(id);
                if (IBWXIcons.Entity != null)
                {
                    long labid = IBWXIcons.Entity.LabID;
                    string entityName = IBWXIcons.Entity.GetType().Name;
                    baseResultBool.success = IBWXIcons.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchWXIcons(WXIcons entity)
        {
            EntityList<WXIcons> entityList = new EntityList<WXIcons>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBWXIcons.Entity = entity;
                try
                {
                    entityList.list = IBWXIcons.Search();
                    entityList.count = IBWXIcons.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXIcons>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXIconsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<WXIcons> entityList = new EntityList<WXIcons>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBWXIcons.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBWXIcons.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXIcons>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXIconsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBWXIcons.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<WXIcons>(entity);
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

        #region WXMessageSendTask
        //Add  WXMessageSendTask
        public BaseResultDataValue ST_UDTO_AddWXMessageSendTask(WXMessageSendTask entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBWXMessageSendTask.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBWXMessageSendTask.Add();
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
        //Update  WXMessageSendTask
        public BaseResultBool ST_UDTO_UpdateWXMessageSendTask(WXMessageSendTask entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBWXMessageSendTask.Entity = entity;
                try
                {
                    baseResultBool.success = IBWXMessageSendTask.Edit();
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
        //Update  WXMessageSendTask
        public BaseResultBool ST_UDTO_UpdateWXMessageSendTaskByField(WXMessageSendTask entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBWXMessageSendTask.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBWXMessageSendTask.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBWXMessageSendTask.Update(tempArray);
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
                        //baseResultBool.success = IBWXMessageSendTask.Edit();
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
        //Delele  WXMessageSendTask
        public BaseResultBool ST_UDTO_DelWXMessageSendTask(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBWXMessageSendTask.Entity = IBWXMessageSendTask.Get(id);
                if (IBWXMessageSendTask.Entity != null)
                {
                    long labid = IBWXMessageSendTask.Entity.LabID;
                    string entityName = IBWXMessageSendTask.Entity.GetType().Name;
                    baseResultBool.success = IBWXMessageSendTask.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchWXMessageSendTask(WXMessageSendTask entity)
        {
            EntityList<WXMessageSendTask> entityList = new EntityList<WXMessageSendTask>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBWXMessageSendTask.Entity = entity;
                try
                {
                    entityList.list = IBWXMessageSendTask.Search();
                    entityList.count = IBWXMessageSendTask.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXMessageSendTask>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXMessageSendTaskByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<WXMessageSendTask> entityList = new EntityList<WXMessageSendTask>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBWXMessageSendTask.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBWXMessageSendTask.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXMessageSendTask>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXMessageSendTaskById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBWXMessageSendTask.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<WXMessageSendTask>(entity);
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

        #region WXMsgSendLog
        //Add  WXMsgSendLog
        public BaseResultDataValue ST_UDTO_AddWXMsgSendLog(WXMsgSendLog entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBWXMsgSendLog.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBWXMsgSendLog.Add();
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
        //Update  WXMsgSendLog
        public BaseResultBool ST_UDTO_UpdateWXMsgSendLog(WXMsgSendLog entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBWXMsgSendLog.Entity = entity;
                try
                {
                    baseResultBool.success = IBWXMsgSendLog.Edit();
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
        //Update  WXMsgSendLog
        public BaseResultBool ST_UDTO_UpdateWXMsgSendLogByField(WXMsgSendLog entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBWXMsgSendLog.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBWXMsgSendLog.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBWXMsgSendLog.Update(tempArray);
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
                        //baseResultBool.success = IBWXMsgSendLog.Edit();
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
        //Delele  WXMsgSendLog
        public BaseResultBool ST_UDTO_DelWXMsgSendLog(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBWXMsgSendLog.Entity = IBWXMsgSendLog.Get(id);
                if (IBWXMsgSendLog.Entity != null)
                {
                    long labid = IBWXMsgSendLog.Entity.LabID;
                    string entityName = IBWXMsgSendLog.Entity.GetType().Name;
                    baseResultBool.success = IBWXMsgSendLog.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchWXMsgSendLog(WXMsgSendLog entity)
        {
            EntityList<WXMsgSendLog> entityList = new EntityList<WXMsgSendLog>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBWXMsgSendLog.Entity = entity;
                try
                {
                    entityList.list = IBWXMsgSendLog.Search();
                    entityList.count = IBWXMsgSendLog.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXMsgSendLog>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXMsgSendLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<WXMsgSendLog> entityList = new EntityList<WXMsgSendLog>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBWXMsgSendLog.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBWXMsgSendLog.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXMsgSendLog>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXMsgSendLogById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBWXMsgSendLog.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<WXMsgSendLog>(entity);
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

        #region WXWeiXinEmpLink
        //Add  WXWeiXinEmpLink
        public BaseResultDataValue ST_UDTO_AddWXWeiXinEmpLink(WXWeiXinEmpLink entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBWXWeiXinEmpLink.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBWXWeiXinEmpLink.Add();
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
        //Update  WXWeiXinEmpLink
        public BaseResultBool ST_UDTO_UpdateWXWeiXinEmpLink(WXWeiXinEmpLink entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBWXWeiXinEmpLink.Entity = entity;
                try
                {
                    baseResultBool.success = IBWXWeiXinEmpLink.Edit();
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
        //Update  WXWeiXinEmpLink
        public BaseResultBool ST_UDTO_UpdateWXWeiXinEmpLinkByField(WXWeiXinEmpLink entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBWXWeiXinEmpLink.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBWXWeiXinEmpLink.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBWXWeiXinEmpLink.Update(tempArray);
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
                        //baseResultBool.success = IBWXWeiXinEmpLink.Edit();
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
        //Delele  WXWeiXinEmpLink
        public BaseResultBool ST_UDTO_DelWXWeiXinEmpLink(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBWXWeiXinEmpLink.Entity = IBWXWeiXinEmpLink.Get(id);
                if (IBWXWeiXinEmpLink.Entity != null)
                {
                    long labid = IBWXWeiXinEmpLink.Entity.LabID;
                    string entityName = IBWXWeiXinEmpLink.Entity.GetType().Name;
                    baseResultBool.success = IBWXWeiXinEmpLink.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchWXWeiXinEmpLink(WXWeiXinEmpLink entity)
        {
            EntityList<WXWeiXinEmpLink> entityList = new EntityList<WXWeiXinEmpLink>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBWXWeiXinEmpLink.Entity = entity;
                try
                {
                    entityList.list = IBWXWeiXinEmpLink.Search();
                    entityList.count = IBWXWeiXinEmpLink.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXWeiXinEmpLink>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXWeiXinEmpLinkByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<WXWeiXinEmpLink> entityList = new EntityList<WXWeiXinEmpLink>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBWXWeiXinEmpLink.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBWXWeiXinEmpLink.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXWeiXinEmpLink>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXWeiXinEmpLinkById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBWXWeiXinEmpLink.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<WXWeiXinEmpLink>(entity);
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

        #region WXWeiXinPushMessageTemplate
        //Add  WXWeiXinPushMessageTemplate
        public BaseResultDataValue ST_UDTO_AddWXWeiXinPushMessageTemplate(WXWeiXinPushMessageTemplate entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBWXWeiXinPushMessageTemplate.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBWXWeiXinPushMessageTemplate.Add();
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
        //Update  WXWeiXinPushMessageTemplate
        public BaseResultBool ST_UDTO_UpdateWXWeiXinPushMessageTemplate(WXWeiXinPushMessageTemplate entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBWXWeiXinPushMessageTemplate.Entity = entity;
                try
                {
                    baseResultBool.success = IBWXWeiXinPushMessageTemplate.Edit();
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
        //Update  WXWeiXinPushMessageTemplate
        public BaseResultBool ST_UDTO_UpdateWXWeiXinPushMessageTemplateByField(WXWeiXinPushMessageTemplate entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBWXWeiXinPushMessageTemplate.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBWXWeiXinPushMessageTemplate.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBWXWeiXinPushMessageTemplate.Update(tempArray);
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
                        //baseResultBool.success = IBWXWeiXinPushMessageTemplate.Edit();
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
        //Delele  WXWeiXinPushMessageTemplate
        public BaseResultBool ST_UDTO_DelWXWeiXinPushMessageTemplate(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBWXWeiXinPushMessageTemplate.Entity = IBWXWeiXinPushMessageTemplate.Get(id);
                if (IBWXWeiXinPushMessageTemplate.Entity != null)
                {
                    long labid = IBWXWeiXinPushMessageTemplate.Entity.LabID;
                    string entityName = IBWXWeiXinPushMessageTemplate.Entity.GetType().Name;
                    baseResultBool.success = IBWXWeiXinPushMessageTemplate.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchWXWeiXinPushMessageTemplate(WXWeiXinPushMessageTemplate entity)
        {
            EntityList<WXWeiXinPushMessageTemplate> entityList = new EntityList<WXWeiXinPushMessageTemplate>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBWXWeiXinPushMessageTemplate.Entity = entity;
                try
                {
                    entityList.list = IBWXWeiXinPushMessageTemplate.Search();
                    entityList.count = IBWXWeiXinPushMessageTemplate.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXWeiXinPushMessageTemplate>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXWeiXinPushMessageTemplateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<WXWeiXinPushMessageTemplate> entityList = new EntityList<WXWeiXinPushMessageTemplate>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBWXWeiXinPushMessageTemplate.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBWXWeiXinPushMessageTemplate.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXWeiXinPushMessageTemplate>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXWeiXinPushMessageTemplateById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBWXWeiXinPushMessageTemplate.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<WXWeiXinPushMessageTemplate>(entity);
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

        #region WXWeiXinAccount
        //Add  WXWeiXinAccount
        public BaseResultDataValue ST_UDTO_AddWXWeiXinAccount(WXWeiXinAccount entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBWXWeiXinAccount.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBWXWeiXinAccount.Add();
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
        //Update  WXWeiXinAccount
        public BaseResultBool ST_UDTO_UpdateWXWeiXinAccount(WXWeiXinAccount entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBWXWeiXinAccount.Entity = entity;
                try
                {
                    baseResultBool.success = IBWXWeiXinAccount.Edit();
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
        //Update  WXWeiXinAccount
        public BaseResultBool ST_UDTO_UpdateWXWeiXinAccountByField(WXWeiXinAccount entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBWXWeiXinAccount.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBWXWeiXinAccount.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBWXWeiXinAccount.Update(tempArray);
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
                        //baseResultBool.success = IBWXWeiXinAccount.Edit();
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
        //Delele  WXWeiXinAccount
        public BaseResultBool ST_UDTO_DelWXWeiXinAccount(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBWXWeiXinAccount.Entity = IBWXWeiXinAccount.Get(id);
                if (IBWXWeiXinAccount.Entity != null)
                {
                    long labid = IBWXWeiXinAccount.Entity.LabID;
                    string entityName = IBWXWeiXinAccount.Entity.GetType().Name;
                    baseResultBool.success = IBWXWeiXinAccount.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchWXWeiXinAccount(WXWeiXinAccount entity)
        {
            EntityList<WXWeiXinAccount> entityList = new EntityList<WXWeiXinAccount>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBWXWeiXinAccount.Entity = entity;
                try
                {
                    entityList.list = IBWXWeiXinAccount.Search();
                    entityList.count = IBWXWeiXinAccount.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXWeiXinAccount>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXWeiXinAccountByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<WXWeiXinAccount> entityList = new EntityList<WXWeiXinAccount>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBWXWeiXinAccount.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBWXWeiXinAccount.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXWeiXinAccount>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXWeiXinAccountById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBWXWeiXinAccount.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<WXWeiXinAccount>(entity);
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

        #region WXWeiXinUserGroup
        //Add  WXWeiXinUserGroup
        public BaseResultDataValue ST_UDTO_AddWXWeiXinUserGroup(WXWeiXinUserGroup entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBWXWeiXinUserGroup.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBWXWeiXinUserGroup.Add();
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
        //Update  WXWeiXinUserGroup
        public BaseResultBool ST_UDTO_UpdateWXWeiXinUserGroup(WXWeiXinUserGroup entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBWXWeiXinUserGroup.Entity = entity;
                try
                {
                    baseResultBool.success = IBWXWeiXinUserGroup.Edit();
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
        //Update  WXWeiXinUserGroup
        public BaseResultBool ST_UDTO_UpdateWXWeiXinUserGroupByField(WXWeiXinUserGroup entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBWXWeiXinUserGroup.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBWXWeiXinUserGroup.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBWXWeiXinUserGroup.Update(tempArray);
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
                        //baseResultBool.success = IBWXWeiXinUserGroup.Edit();
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
        //Delele  WXWeiXinUserGroup
        public BaseResultBool ST_UDTO_DelWXWeiXinUserGroup(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBWXWeiXinUserGroup.Entity = IBWXWeiXinUserGroup.Get(id);
                if (IBWXWeiXinUserGroup.Entity != null)
                {
                    long labid = IBWXWeiXinUserGroup.Entity.LabID;
                    string entityName = IBWXWeiXinUserGroup.Entity.GetType().Name;
                    baseResultBool.success = IBWXWeiXinUserGroup.RemoveByHQL(id);
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

        public BaseResultDataValue ST_UDTO_SearchWXWeiXinUserGroup(WXWeiXinUserGroup entity)
        {
            EntityList<WXWeiXinUserGroup> entityList = new EntityList<WXWeiXinUserGroup>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBWXWeiXinUserGroup.Entity = entity;
                try
                {
                    entityList.list = IBWXWeiXinUserGroup.Search();
                    entityList.count = IBWXWeiXinUserGroup.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXWeiXinUserGroup>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXWeiXinUserGroupByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<WXWeiXinUserGroup> entityList = new EntityList<WXWeiXinUserGroup>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBWXWeiXinUserGroup.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBWXWeiXinUserGroup.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<WXWeiXinUserGroup>(entityList);
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

        public BaseResultDataValue ST_UDTO_SearchWXWeiXinUserGroupById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBWXWeiXinUserGroup.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<WXWeiXinUserGroup>(entity);
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

        public BaseResultBool WXMessageSendTaskUpLoadFile()
        {
            BaseResultBool brdv = new BaseResultBool();
            try
            {
                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    brdv.ErrorInfo = "未检测到文件！";
                    brdv.success = false;
                    return brdv;
                }
                else
                {
                    HttpPostedFile file = HttpContext.Current.Request.Files[0];
                    if (file == null)
                    {
                        brdv.ErrorInfo = "未检测到文件!";
                        brdv.success = false;
                        return brdv;
                    }
                    string CurDir = "WXMessageSendTaskUpLoadFile/" + DateTime.Today.ToShortDateString();    //设置当前目录   + 
                    if (!System.IO.Directory.Exists(System.AppDomain.CurrentDomain.BaseDirectory + CurDir)) System.IO.Directory.CreateDirectory(System.AppDomain.CurrentDomain.BaseDirectory + CurDir);   //该路径不存在时，在当前文件目录下创建文件夹"导出.."
                    string guid = GUIDHelp.GetGUIDInt().ToString();
                    string filename = guid + "_" + file.FileName;
                    string filePath = CurDir + "/" + filename;
                    file.SaveAs(System.AppDomain.CurrentDomain.BaseDirectory + filePath);

                    brdv.BoolInfo = file.FileName + " , " + filePath + ", " +file.ContentLength;
                    brdv.success = true;
                }
            }
            catch (Exception e)
            {
                brdv.ErrorInfo = e.Message.ToString();
                brdv.success = false;
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return brdv;
        }
    
        public BaseResultBool WXMessageSendOut(long id,long peopleId)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                string EmpID = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeID);
                string EmpName = Cookie.CookieHelper.Read(ZhiFang.Entity.RBAC.DicCookieSession.EmployeeName);
                baseResultBool.success = IBWXMessageSendTask.Add_WXMessageSendOut(id,peopleId, EmpID,EmpName);
            }
            catch (Exception e)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = e.Message;
                ZhiFang.Common.Log.Log.Debug(e.ToString());
            }
            return baseResultBool;
        }
    }
}
