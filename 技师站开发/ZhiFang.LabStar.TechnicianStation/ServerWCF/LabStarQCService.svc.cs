using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.Activation;
using System.Web;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation.ServerWCF
{
    //检验前业务系统质控服务
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class LabStarQCService : ILabStarQCService
    {
        #region
        ZhiFang.IBLL.LabStar.IBLBQCItem IBLBQCItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLBQCItemTime IBLBQCItemTime { get; set; }

        ZhiFang.IBLL.LabStar.IBLBQCPrintTemplate IBLBQCPrintTemplate { get; set; }

        ZhiFang.IBLL.LabStar.IBLBQCMaterial IBLBQCMaterial { get; set; }

        ZhiFang.IBLL.LabStar.IBLBQCMatTime IBLBQCMatTime { get; set; }

        ZhiFang.IBLL.LabStar.IBLisCommon IBLisCommon { get; set; }

        ZhiFang.IBLL.LabStar.IBLBQCItemRule IBLBQCItemRule { get; set; }

        ZhiFang.IBLL.LabStar.IBLBQCRule IBLBQCRule { get; set; }

        ZhiFang.IBLL.LabStar.IBLBQCRuleBase IBLBQCRuleBase { get; set; }

        ZhiFang.IBLL.LabStar.IBLBQCRulesCon IBLBQCRulesCon { get; set; }

        ZhiFang.IBLL.LabStar.IBLisQCData IBLisQCData { get; set; }

        ZhiFang.IBLL.LabStar.IBLisQCComments IBLisQCComments { get; set; }
        ZhiFang.IBLL.LabStar.IBLisTestItem IBLisTestItem { get; set; }
        #endregion

        #region LBQCItem
        //Add  LBQCItem
        public BaseResultDataValue QC_UDTO_AddLBQCItem(LBQCItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBQCItem.Add();
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
        //Update  LBQCItem
        public BaseResultBool QC_UDTO_UpdateLBQCItem(LBQCItem entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCItem.Entity = entity;
                try
                {
                    baseResultBool.success = IBLBQCItem.Edit();
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
        //Update  LBQCItem
        public BaseResultBool QC_UDTO_UpdateLBQCItemByField(LBQCItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBQCItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBQCItem.Update(tempArray);
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
                        //baseResultBool.success = IBLBQCItem.Edit();
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
        //Delele  LBQCItem
        public BaseResultBool QC_UDTO_DelLBQCItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBQCItem.Entity = IBLBQCItem.Get(id);
                if (IBLBQCItem.Entity != null)
                {
                    long labid = IBLBQCItem.Entity.LabID;
                    string entityName = IBLBQCItem.Entity.GetType().Name;
                    baseResultBool.success = IBLBQCItem.RemoveByHQL(id);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCItem(LBQCItem entity)
        {
            EntityList<LBQCItem> entityList = new EntityList<LBQCItem>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLBQCItem.Entity = entity;
                try
                {
                    entityList.list = IBLBQCItem.Search();
                    entityList.count = IBLBQCItem.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCItem>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBQCItem> entityList = new EntityList<LBQCItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBQCItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBQCItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCItem>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBQCItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBQCItem>(entity);
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

        #region LBQCItemTime
        //Add  LBQCItemTime
        public BaseResultDataValue QC_UDTO_AddLBQCItemTime(LBQCItemTime entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCItemTime.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBQCItemTime.Add();
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
        //Update  LBQCItemTime
        public BaseResultBool QC_UDTO_UpdateLBQCItemTime(LBQCItemTime entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCItemTime.Entity = entity;
                try
                {
                    baseResultBool.success = IBLBQCItemTime.Edit();
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
        //Update  LBQCItemTime
        public BaseResultBool QC_UDTO_UpdateLBQCItemTimeByField(LBQCItemTime entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCItemTime.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBQCItemTime.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBQCItemTime.Update(tempArray);
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
                        //baseResultBool.success = IBLBQCItemTime.Edit();
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
        //Delele  LBQCItemTime
        public BaseResultBool QC_UDTO_DelLBQCItemTime(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBQCItemTime.Entity = IBLBQCItemTime.Get(id);
                if (IBLBQCItemTime.Entity != null)
                {
                    long labid = IBLBQCItemTime.Entity.LabID;
                    string entityName = IBLBQCItemTime.Entity.GetType().Name;
                    baseResultBool.success = IBLBQCItemTime.RemoveByHQL(id);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCItemTime(LBQCItemTime entity)
        {
            EntityList<LBQCItemTime> entityList = new EntityList<LBQCItemTime>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLBQCItemTime.Entity = entity;
                try
                {
                    entityList.list = IBLBQCItemTime.Search();
                    entityList.count = IBLBQCItemTime.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCItemTime>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCItemTimeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBQCItemTime> entityList = new EntityList<LBQCItemTime>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBQCItemTime.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBQCItemTime.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCItemTime>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCItemTimeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBQCItemTime.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBQCItemTime>(entity);
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

        #region LBQCMaterial
        //Add  LBQCMaterial
        public BaseResultDataValue QC_UDTO_AddLBQCMaterial(LBQCMaterial entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCMaterial.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBQCMaterial.Add();
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
        //Update  LBQCMaterial
        public BaseResultBool QC_UDTO_UpdateLBQCMaterial(LBQCMaterial entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCMaterial.Entity = entity;
                try
                {
                    baseResultBool.success = IBLBQCMaterial.Edit();
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
        //Update  LBQCMaterial
        public BaseResultBool QC_UDTO_UpdateLBQCMaterialByField(LBQCMaterial entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCMaterial.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBQCMaterial.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBQCMaterial.Update(tempArray);
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
                        //baseResultBool.success = IBLBQCMaterial.Edit();
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
        //Delele  LBQCMaterial
        public BaseResultBool QC_UDTO_DelLBQCMaterial(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBQCMaterial.Entity = IBLBQCMaterial.Get(id);
                if (IBLBQCMaterial.Entity != null)
                {
                    long labid = IBLBQCMaterial.Entity.LabID;
                    string entityName = IBLBQCMaterial.Entity.GetType().Name;
                    baseResultBool.success = IBLBQCMaterial.RemoveByHQL(id);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCMaterial(LBQCMaterial entity)
        {
            EntityList<LBQCMaterial> entityList = new EntityList<LBQCMaterial>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLBQCMaterial.Entity = entity;
                try
                {
                    entityList.list = IBLBQCMaterial.Search();
                    entityList.count = IBLBQCMaterial.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCMaterial>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCMaterialByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBQCMaterial> entityList = new EntityList<LBQCMaterial>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBQCMaterial.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBQCMaterial.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCMaterial>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCMaterialById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBQCMaterial.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBQCMaterial>(entity);
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

        #region LBQCMatTime
        //Add  LBQCMatTime
        public BaseResultDataValue QC_UDTO_AddLBQCMatTime(LBQCMatTime entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCMatTime.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBQCMatTime.Add();
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
        //Update  LBQCMatTime
        public BaseResultBool QC_UDTO_UpdateLBQCMatTime(LBQCMatTime entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCMatTime.Entity = entity;
                try
                {
                    baseResultBool.success = IBLBQCMatTime.Edit();
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
        //Update  LBQCMatTime
        public BaseResultBool QC_UDTO_UpdateLBQCMatTimeByField(LBQCMatTime entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCMatTime.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBQCMatTime.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBQCMatTime.Update(tempArray);
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
                        //baseResultBool.success = IBLBQCMatTime.Edit();
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
        //Delele  LBQCMatTime
        public BaseResultBool QC_UDTO_DelLBQCMatTime(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBQCMatTime.Entity = IBLBQCMatTime.Get(id);
                if (IBLBQCMatTime.Entity != null)
                {
                    long labid = IBLBQCMatTime.Entity.LabID;
                    string entityName = IBLBQCMatTime.Entity.GetType().Name;
                    baseResultBool.success = IBLBQCMatTime.RemoveByHQL(id);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCMatTime(LBQCMatTime entity)
        {
            EntityList<LBQCMatTime> entityList = new EntityList<LBQCMatTime>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLBQCMatTime.Entity = entity;
                try
                {
                    entityList.list = IBLBQCMatTime.Search();
                    entityList.count = IBLBQCMatTime.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCMatTime>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCMatTimeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBQCMatTime> entityList = new EntityList<LBQCMatTime>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBQCMatTime.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBQCMatTime.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCMatTime>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCMatTimeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBQCMatTime.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBQCMatTime>(entity);
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

        #region LBQCItemRule
        //Add  LBQCItemRule
        public BaseResultDataValue QC_UDTO_AddLBQCItemRule(LBQCItemRule entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCItemRule.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBQCItemRule.Add();
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
        //Update  LBQCItemRule
        public BaseResultBool QC_UDTO_UpdateLBQCItemRule(LBQCItemRule entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCItemRule.Entity = entity;
                try
                {
                    baseResultBool.success = IBLBQCItemRule.Edit();
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
        //Update  LBQCItemRule
        public BaseResultBool QC_UDTO_UpdateLBQCItemRuleByField(LBQCItemRule entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCItemRule.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBQCItemRule.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBQCItemRule.Update(tempArray);
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
                        //baseResultBool.success = IBLBQCItemRule.Edit();
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
        //Delele  LBQCItemRule
        public BaseResultBool QC_UDTO_DelLBQCItemRule(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBQCItemRule.Entity = IBLBQCItemRule.Get(id);
                if (IBLBQCItemRule.Entity != null)
                {
                    long labid = IBLBQCItemRule.Entity.LabID;
                    string entityName = IBLBQCItemRule.Entity.GetType().Name;
                    baseResultBool.success = IBLBQCItemRule.RemoveByHQL(id);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCItemRule(LBQCItemRule entity)
        {
            EntityList<LBQCItemRule> entityList = new EntityList<LBQCItemRule>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLBQCItemRule.Entity = entity;
                try
                {
                    entityList.list = IBLBQCItemRule.Search();
                    entityList.count = IBLBQCItemRule.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCItemRule>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCItemRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBQCItemRule> entityList = new EntityList<LBQCItemRule>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBQCItemRule.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBQCItemRule.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCItemRule>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCItemRuleById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBQCItemRule.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBQCItemRule>(entity);
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

        #region LBQCRule
        //Add  LBQCRule
        public BaseResultDataValue QC_UDTO_AddLBQCRule(LBQCRule entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCRule.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBQCRule.Add();
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
        //Update  LBQCRule
        public BaseResultBool QC_UDTO_UpdateLBQCRule(LBQCRule entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCRule.Entity = entity;
                try
                {
                    baseResultBool.success = IBLBQCRule.Edit();
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
        //Update  LBQCRule
        public BaseResultBool QC_UDTO_UpdateLBQCRuleByField(LBQCRule entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCRule.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBQCRule.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBQCRule.Update(tempArray);
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
                        //baseResultBool.success = IBLBQCRule.Edit();
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
        //Delele  LBQCRule
        public BaseResultBool QC_UDTO_DelLBQCRule(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBQCRule.Entity = IBLBQCRule.Get(id);
                if (IBLBQCRule.Entity != null)
                {
                    long labid = IBLBQCRule.Entity.LabID;
                    string entityName = IBLBQCRule.Entity.GetType().Name;
                    baseResultBool.success = IBLBQCRule.RemoveByHQL(id);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCRule(LBQCRule entity)
        {
            EntityList<LBQCRule> entityList = new EntityList<LBQCRule>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLBQCRule.Entity = entity;
                try
                {
                    entityList.list = IBLBQCRule.Search();
                    entityList.count = IBLBQCRule.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCRule>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCRuleByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBQCRule> entityList = new EntityList<LBQCRule>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBQCRule.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBQCRule.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCRule>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCRuleById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBQCRule.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBQCRule>(entity);
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

        #region LBQCRuleBase
        //Add  LBQCRuleBase
        public BaseResultDataValue QC_UDTO_AddLBQCRuleBase(LBQCRuleBase entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCRuleBase.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBQCRuleBase.Add();
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
        //Update  LBQCRuleBase
        public BaseResultBool QC_UDTO_UpdateLBQCRuleBase(LBQCRuleBase entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCRuleBase.Entity = entity;
                try
                {
                    baseResultBool.success = IBLBQCRuleBase.Edit();
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
        //Update  LBQCRuleBase
        public BaseResultBool QC_UDTO_UpdateLBQCRuleBaseByField(LBQCRuleBase entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCRuleBase.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBQCRuleBase.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBQCRuleBase.Update(tempArray);
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
                        //baseResultBool.success = IBLBQCRuleBase.Edit();
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
        //Delele  LBQCRuleBase
        public BaseResultBool QC_UDTO_DelLBQCRuleBase(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBQCRuleBase.Entity = IBLBQCRuleBase.Get(id);
                if (IBLBQCRuleBase.Entity != null)
                {
                    long labid = IBLBQCRuleBase.Entity.LabID;
                    string entityName = IBLBQCRuleBase.Entity.GetType().Name;
                    baseResultBool.success = IBLBQCRuleBase.RemoveByHQL(id);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCRuleBase(LBQCRuleBase entity)
        {
            EntityList<LBQCRuleBase> entityList = new EntityList<LBQCRuleBase>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLBQCRuleBase.Entity = entity;
                try
                {
                    entityList.list = IBLBQCRuleBase.Search();
                    entityList.count = IBLBQCRuleBase.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCRuleBase>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCRuleBaseByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBQCRuleBase> entityList = new EntityList<LBQCRuleBase>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBQCRuleBase.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBQCRuleBase.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCRuleBase>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCRuleBaseById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBQCRuleBase.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBQCRuleBase>(entity);
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

        #region LBQCRulesCon
        //Add  LBQCRulesCon
        public BaseResultDataValue QC_UDTO_AddLBQCRulesCon(LBQCRulesCon entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCRulesCon.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLBQCRulesCon.Add();
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
        //Update  LBQCRulesCon
        public BaseResultBool QC_UDTO_UpdateLBQCRulesCon(LBQCRulesCon entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCRulesCon.Entity = entity;
                try
                {
                    baseResultBool.success = IBLBQCRulesCon.Edit();
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
        //Update  LBQCRulesCon
        public BaseResultBool QC_UDTO_UpdateLBQCRulesConByField(LBQCRulesCon entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLBQCRulesCon.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBQCRulesCon.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLBQCRulesCon.Update(tempArray);
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
                        //baseResultBool.success = IBLBQCRulesCon.Edit();
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
        //Delele  LBQCRulesCon
        public BaseResultBool QC_UDTO_DelLBQCRulesCon(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLBQCRulesCon.Entity = IBLBQCRulesCon.Get(id);
                if (IBLBQCRulesCon.Entity != null)
                {
                    long labid = IBLBQCRulesCon.Entity.LabID;
                    string entityName = IBLBQCRulesCon.Entity.GetType().Name;
                    baseResultBool.success = IBLBQCRulesCon.RemoveByHQL(id);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCRulesCon(LBQCRulesCon entity)
        {
            EntityList<LBQCRulesCon> entityList = new EntityList<LBQCRulesCon>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLBQCRulesCon.Entity = entity;
                try
                {
                    entityList.list = IBLBQCRulesCon.Search();
                    entityList.count = IBLBQCRulesCon.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCRulesCon>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCRulesConByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBQCRulesCon> entityList = new EntityList<LBQCRulesCon>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBQCRulesCon.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBQCRulesCon.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCRulesCon>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLBQCRulesConById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLBQCRulesCon.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LBQCRulesCon>(entity);
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

        #region LisQCData
        //Add  LisQCData
        public BaseResultDataValue QC_UDTO_AddLisQCData(LisQCData entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisQCData.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisQCData.Add();
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
        //Update  LisQCData
        public BaseResultBool QC_UDTO_UpdateLisQCData(LisQCData entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisQCData.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisQCData.Edit();
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
        //Update  LisQCData
        public BaseResultBool QC_UDTO_UpdateLisQCDataByField(LisQCData entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisQCData.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisQCData.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisQCData.Update(tempArray);
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
                        //baseResultBool.success = IBLisQCData.Edit();
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
        //Delele  LisQCData
        public BaseResultBool QC_UDTO_DelLisQCData(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisQCData.Entity = IBLisQCData.Get(id);
                if (IBLisQCData.Entity != null)
                {
                    long labid = IBLisQCData.Entity.LabID;
                    string entityName = IBLisQCData.Entity.GetType().Name;
                    baseResultBool.success = IBLisQCData.RemoveByHQL(id);
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

        public BaseResultDataValue QC_UDTO_SearchLisQCData(LisQCData entity)
        {
            EntityList<LisQCData> entityList = new EntityList<LisQCData>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisQCData.Entity = entity;
                try
                {
                    entityList.list = IBLisQCData.Search();
                    entityList.count = IBLisQCData.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisQCData>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLisQCDataByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisQCData> entityList = new EntityList<LisQCData>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisQCData.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisQCData.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisQCData>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLisQCDataById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisQCData.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisQCData>(entity);
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

        #region LisQCComments
        //Add  LisQCComments
        public BaseResultDataValue QC_UDTO_AddLisQCComments(LisQCComments entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisQCComments.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisQCComments.Add();
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
        //Update  LisQCComments
        public BaseResultBool QC_UDTO_UpdateLisQCComments(LisQCComments entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisQCComments.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisQCComments.Edit();
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
        //Update  LisQCComments
        public BaseResultBool QC_UDTO_UpdateLisQCCommentsByField(LisQCComments entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisQCComments.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisQCComments.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisQCComments.Update(tempArray);
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
                        //baseResultBool.success = IBLisQCComments.Edit();
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
        //Delele  LisQCComments
        public BaseResultBool QC_UDTO_DelLisQCComments(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisQCComments.Entity = IBLisQCComments.Get(id);
                if (IBLisQCComments.Entity != null)
                {
                    long labid = IBLisQCComments.Entity.LabID;
                    string entityName = IBLisQCComments.Entity.GetType().Name;
                    baseResultBool.success = IBLisQCComments.RemoveByHQL(id);
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

        public BaseResultDataValue QC_UDTO_SearchLisQCComments(LisQCComments entity)
        {
            EntityList<LisQCComments> entityList = new EntityList<LisQCComments>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisQCComments.Entity = entity;
                try
                {
                    entityList.list = IBLisQCComments.Search();
                    entityList.count = IBLisQCComments.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisQCComments>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLisQCCommentsByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisQCComments> entityList = new EntityList<LisQCComments>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisQCComments.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisQCComments.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisQCComments>(entityList);
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

        public BaseResultDataValue QC_UDTO_SearchLisQCCommentsById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisQCComments.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisQCComments>(entity);
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

        #region LBQCPrintTemplate
        //Add  LBQCPrintTemplate
        public BaseResultDataValue ST_UDTO_AddLBQCPrintTemplate(LBQCPrintTemplate entity)
        {
            IBLBQCPrintTemplate.Entity = entity;
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempBaseResultDataValue.success = IBLBQCPrintTemplate.Add();
                if (tempBaseResultDataValue.success)
                {
                    IBLBQCPrintTemplate.Get(IBLBQCPrintTemplate.Entity.Id);
                    tempBaseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(IBLBQCPrintTemplate.Entity);
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
        //Update  LBQCPrintTemplate
        public BaseResultBool ST_UDTO_UpdateLBQCPrintTemplate(LBQCPrintTemplate entity)
        {
            IBLBQCPrintTemplate.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBLBQCPrintTemplate.Edit();
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }
        //Update  LBQCPrintTemplate
        public BaseResultBool ST_UDTO_UpdateLBQCPrintTemplateByField(LBQCPrintTemplate entity, string fields)
        {
            IBLBQCPrintTemplate.Entity = entity;
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                if ((fields != null) && (fields.Length > 0))
                {
                    string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLBQCPrintTemplate.Entity, fields);
                    if (tempArray != null)
                    {
                        tempBaseResultBool.success = IBLBQCPrintTemplate.Update(tempArray);
                    }
                }
                else
                {
                    tempBaseResultBool.success = false;
                    tempBaseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    //tempBaseResultBool.success = IBLBQCPrintTemplate.Edit();
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
        //Delele  LBQCPrintTemplate
        public BaseResultBool ST_UDTO_DelLBQCPrintTemplate(long longLBQCPrintTemplateID)
        {
            BaseResultBool tempBaseResultBool = new BaseResultBool();
            try
            {
                tempBaseResultBool.success = IBLBQCPrintTemplate.Remove(longLBQCPrintTemplateID);
            }
            catch (Exception ex)
            {
                tempBaseResultBool.success = false;
                tempBaseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return tempBaseResultBool;
        }

        public BaseResultDataValue ST_UDTO_SearchLBQCPrintTemplate(LBQCPrintTemplate entity)
        {
            IBLBQCPrintTemplate.Entity = entity;
            EntityList<LBQCPrintTemplate> tempEntityList = new EntityList<LBQCPrintTemplate>();
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                tempEntityList.list = IBLBQCPrintTemplate.Search();
                tempEntityList.count = IBLBQCPrintTemplate.GetTotalCount();
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                try
                {
                    tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<LBQCPrintTemplate>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchLBQCPrintTemplateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            EntityList<LBQCPrintTemplate> tempEntityList = new EntityList<LBQCPrintTemplate>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    tempEntityList = IBLBQCPrintTemplate.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    tempEntityList = IBLBQCPrintTemplate.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish<LBQCPrintTemplate>(tempEntityList);
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

        public BaseResultDataValue ST_UDTO_SearchLBQCPrintTemplateById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue tempBaseResultDataValue = new BaseResultDataValue();
            try
            {
                var tempEntity = IBLBQCPrintTemplate.Get(id);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        tempBaseResultDataValue.ResultDataValue = tempParseObjectProperty.GetSingleObjectPlanish<LBQCPrintTemplate>(tempEntity);
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

        #region 质控定制服务

        public BaseResultDataValue QC_UDTO_GetEquipMaterialTree(long equipID, long matID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                BaseResultTree baseResultTree = IBLBQCMaterial.GetEquipMaterialTree(equipID, matID);
                if (baseResultTree.Tree.Count > 0)
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
                        //throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QC_UDTO_AddDelLBQCItem(IList<LBQCItem> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                Dictionary<string, object> propertyList = new Dictionary<string, object> { { "DataUpdateTime", DateTime.Now } };
                baseResultDataValue = IBLisCommon.AddCommonBaseRelationEntity(IBLBQCItem, addEntityList, isCheckEntityExist, false, propertyList, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除质控项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QC_UDTO_AddDelLBQCRulesCon(IList<LBQCRulesCon> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                Dictionary<string, object> propertyList = new Dictionary<string, object> { { "DataUpdateTime", DateTime.Now } };
                baseResultDataValue = IBLisCommon.AddCommonBaseRelationEntity(IBLBQCRulesCon, addEntityList, isCheckEntityExist, false, propertyList, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除质控规则关系错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QC_UDTO_AddDelLBQCItemRule(IList<LBQCItemRule> addEntityList, bool isCheckEntityExist, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                Dictionary<string, object> propertyList = new Dictionary<string, object> { { "DataUpdateTime", DateTime.Now } };
                baseResultDataValue = IBLisCommon.AddCommonBaseRelationEntity(IBLBQCItemRule, addEntityList, isCheckEntityExist, true, propertyList, delIDList);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除质控项目特殊规则错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QC_UDTO_AddLBQCItemRuleByItemList(IList<LBQCItem> listLBQCItem, IList<LBQCRule> listLBQCRule)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBQCRule.AddLBQCItemRuleByItemList(listLBQCItem, listLBQCRule);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除质控项目特殊规则错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QC_UDTO_DeleteInvalidLBQCRule()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBQCRule.DeleteInvalidLBQCRule();
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "新增或删除质控项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QC_UDTO_CopyLBQCItemByMatID(long fromMatID, long toMatID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLBQCMaterial.AddCopyLBQCItemByMatID(fromMatID, toMatID);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "复制质控项目错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QC_UDTO_GetCalcTargetByQCData(long qcItemID, string beginDate, string endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisQCData.GetCalcTargetByQCData(qcItemID, beginDate, endDate);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "靶值计算错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QC_UDTO_GetCalcSDByQCData(long qcItemID, string beginDate, string endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisQCData.GetCalcSDByQCData(qcItemID, beginDate, endDate);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "标准差计算错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QC_UDTO_GetCalcTargetSDByQCData(string listQCItemID, string beginDate, string endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisQCData.GetCalcTargetSDByQCData(listQCItemID, beginDate, endDate);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "标准差计算错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QC_UDTO_GetLJChartData(long qcItemID, string beginDate, string endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisQCData.GetCalcTargetByQCData(qcItemID, beginDate, endDate);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "靶值计算错误：" + ex.Message;
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 日指控查询
        /// 分页功能暂不支持
        /// </summary>
        /// <param name="EquipID">仪器id</param>
        /// <param name="QCMatID">质控物id</param>
        /// <param name="dateTime">质控日期</param>
        /// <param name="page">第几页</param>
        /// <param name="limit">查询数量</param>
        /// <returns></returns>
        public BaseResultDataValue GetQCDays(long EquipID, long QCMatID, DateTime dateTime, int page, int limit)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty();
                List<LBQCItemVO> entityList = IBLBQCItem.GetQCDays(EquipID, QCMatID, dateTime, page, limit);
                try
                {
                    baseResultDataValue.success = true;
                    foreach (var item in entityList)
                    {
                        item.LBItem = null;
                        item.LBQCMaterial = null;
                    }
                    baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(entityList);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    ZhiFang.LabStar.Common.LogHelp.Debug("LabStarQCService.GetQCDays 序列化错误:" + ex.ToString());
                }
            }
            catch (Exception e)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "程序出现错误，请查看日志！";
                ZhiFang.LabStar.Common.LogHelp.Debug("LabStarQCService.GetQCDays 错误信息:" + e.ToString());
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 月质控查询 
        /// </summary>
        /// <param name="QCItemID"></param>
        /// <param name="fields"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue getQCMoths(long QCItemID, bool Buse, string fields, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<LisQCDataMonthVO> lisQCDataMonthVOs = IBLisQCData.getQCMothsData(QCItemID, Buse, startDate, endDate);
                try
                {
                    baseResultDataValue.success = true;
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish(new EntityList<LisQCDataMonthVO>() { count = lisQCDataMonthVOs.Count, list = lisQCDataMonthVOs }); //    ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(lisQCDataMonthVOs);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    ZhiFang.LabStar.Common.LogHelp.Debug("LabStarQCService.getQCMoths 序列化错误:" + ex.ToString());
                }
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 月质控项目查询
        /// </summary>
        /// <param name="QCItemID"></param>
        /// <param name="fields"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue GetQCMothItem(long QCItemID, string fields, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<MultipleConcentrationQCM> lisQCDataMonthVOs = IBLBQCItem.GetQCMothItem(QCItemID, startDate, endDate);
                try
                {
                    baseResultDataValue.success = true;
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish(new EntityList<MultipleConcentrationQCM>() { count = lisQCDataMonthVOs.Count, list = lisQCDataMonthVOs }); //    ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(lisQCDataMonthVOs);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    ZhiFang.LabStar.Common.LogHelp.Debug("LabStarQCService.GetQCMothItem 序列化错误:" + ex.ToString());
                }
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 多浓度质控查询
        /// </summary>
        /// <param name="EquipId"></param>
        /// <param name="ItemId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="QCMModule"></param>
        /// <param name="QCMGroup"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public BaseResultDataValue GetMultipleConcentrationQCM(long EquipId, long ItemId, DateTime startDate, DateTime endDate, string QCMMoudle, string QCMGroup, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<MultipleConcentrationQCM> multipleConcentrationQCMs = IBLBQCItem.GetMultipleConcentrationQCM(EquipId, ItemId, QCMMoudle, QCMGroup, startDate, endDate);
                ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish(new EntityList<MultipleConcentrationQCM>() { count = multipleConcentrationQCMs.Count, list = multipleConcentrationQCMs }); //    ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(lisQCDataMonthVOs);
            }
            catch (Exception e)
            {
                baseResultDataValue.success = true;
                baseResultDataValue.ErrorInfo = e.Message;
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 多浓度质控获得树列表
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue GetMultipleConcentrationQCMTree()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                BaseResultTree<MultipleConcentrationQCMTree> baseResultTree = IBLBQCItem.GetMultipleConcentrationQCMTree();
                if (baseResultTree.Tree.Count > 0)
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
                        //throw new Exception(ex.Message);
                    }
                }

            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 月质控树列表
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue GetQCMothTree()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                BaseResultTree baseResultTree = new BaseResultTree() { Tree = new List<tree>() };
                baseResultTree = IBLBQCItem.GetQCMothTree();
                if (baseResultTree.Tree.Count > 0)
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
                        //throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 多浓度质控树筛选列表
        /// </summary>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="fields"></param>
        /// <param name="where"></param>
        /// <param name="sort"></param>
        /// <param name="isPlanish"></param>
        /// <returns></returns>
        public BaseResultDataValue SearchLBQCItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBQCItem> entityList = new EntityList<LBQCItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBQCItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBQCItem.SearchListByHQL(where, page, limit);
                }
                if (entityList.count > 0)
                {
                    entityList.list = IBLBQCItem.SearchLBQCItemByHQL(entityList.list);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCItem>(entityList);
                    }
                    else
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectPropertyNoPlanish(entityList, fields);
                    }
                }
                catch (Exception ex)
                {
                    ZhiFang.LabStar.Common.LogHelp.Debug(ex.ToString());
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    //throw new Exception(ex.Message);
                }
            }
            catch (Exception ex)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(ex.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue QC_UDTO_QueryLBQCItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBQCItem> entityList = new EntityList<LBQCItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLBQCItem.QueryLBQCItem(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLBQCItem.QueryLBQCItem(where, "", page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBQCItem>(entityList);
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
        /// 多浓度质控对比信息
        /// </summary>
        /// <param name="QCItemIds"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue GetMultipleConcentrationQCMCompareInfo(string QCItemIds, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                DataTable dataTable = IBLBQCItem.GetMultipleConcentrationQCMCompareInfo(QCItemIds, startDate, endDate);
                if (dataTable.Rows.Count > 0)
                {
                    try
                    {
                        baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }

            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 多浓度质控详细信息
        /// </summary>
        /// <param name="QCItemIds"></param>
        /// <param name="fields"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue GetMultipleConcentrationQCMInfoFull(string QCItemIds, string fields, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<MultipleConcentrationQCMInfoFull> multipleConcentrationQCMInfoFulls = IBLBQCItem.GetMultipleConcentrationQCMInfoFull(QCItemIds, startDate, endDate);
                if (multipleConcentrationQCMInfoFulls.Count > 0)
                {
                    try
                    {
                        baseResultDataValue.success = true;
                        ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish(new EntityList<MultipleConcentrationQCMInfoFull>() { count = multipleConcentrationQCMInfoFulls.Count, list = multipleConcentrationQCMInfoFulls }); //    ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(lisQCDataMonthVOs);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        ZhiFang.LabStar.Common.LogHelp.Debug("LabStarQCService.GetMultipleConcentrationQCMInfoFull 序列化错误:" + ex.ToString());
                    }
                }

            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 新增质控项目数据
        /// </summary>
        /// <param name="lisQCDataVO"></param>
        /// <returns></returns>
        public BaseResultBool SaveLisQCData(LisQCData entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                baseResultBool.success = IBLisQCData.AddLisQCData(entity);
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = e.Message;
            }
            return baseResultBool;

        }
        /// <summary>
        /// 新增质控项目数据 --批量
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public BaseResultBool SaveLisQCDataBatch(List<LisQCData> entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                foreach (var item in entity)
                {
                    baseResultBool.success = IBLisQCData.AddLisQCData(item);
                }
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = e.Message;
            }
            return baseResultBool;

        }

        /// <summary>
        /// 更新质控项目数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public BaseResultBool UpdateLisQCData(LisQCData entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                //IBLisQCData.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        IBLisQCData.EditLisQCData(entity);
                        fields += ",QuanValue,Loserule,LoseType";
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                        baseResultBool.success = IBLisQCData.Update(tempArray);
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    }
                }
                catch (Exception ex)
                {
                    ZhiFang.LabStar.Common.LogHelp.Debug(ex.ToString());
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        /// <summary>
        /// 更新质控服务--批量
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public BaseResultBool UpdateLisQCDataBatch(List<LisQCData> entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity.Count > 0)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.ForEach(a => a.DataUpdateTime = DateTime.Now);
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        foreach (var item in entity)
                        {
                            IBLisQCData.EditLisQCData(item);
                            fields += ",QuanValue,Loserule,LoseType";
                            string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                            baseResultBool.success = IBLisQCData.Update(tempArray);
                        }
                    }
                    else
                    {
                        baseResultBool.success = false;
                        baseResultBool.ErrorInfo = "错误信息：fields参数不能为空！";
                    }
                }
                catch (Exception ex)
                {
                    ZhiFang.LabStar.Common.LogHelp.Debug(ex.ToString());
                    baseResultBool.success = false;
                    baseResultBool.ErrorInfo = "错误信息：" + ex.Message;
                }
            }
            else
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultBool;
        }
        /// <summary>
        /// 仪器日指控 仪器列表查询
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue SearchEquipDayQCM()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                DataTable dataTable = IBLBQCItem.SearchEquipDayQCM();
                if (dataTable.Rows.Count > 0)
                {
                    try
                    {
                        baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 仪器日指控对比信息查询
        /// </summary>
        /// <param name="EquipId"></param>
        /// <param name="EquipModel"></param>
        /// <param name="EquipGroup"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue SearchEquipDayQCMList(long EquipId, string EquipModel, string EquipGroup, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                DataTable dataTable = IBLBQCItem.SearchEquipDayQCMList(EquipId, EquipModel, EquipGroup, startDate, endDate);
                if (dataTable.Rows.Count > 0)
                {
                    try
                    {
                        baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(dataTable);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        //throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 仪器日指控单个仪器查询
        /// </summary>
        /// <param name="EquipId"></param>
        /// <param name="EquipModel"></param>
        /// <param name="EquipGroup"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue SearchEquipDayQCData(long QCMId, string fields, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<LisQCDataMonthVO> lisQCDataMonthVOs = IBLBQCItem.SearchEquipDayQCData(QCMId, startDate, endDate);
                try
                {
                    baseResultDataValue.success = true;
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish(new EntityList<LisQCDataMonthVO>() { count = lisQCDataMonthVOs.Count, list = lisQCDataMonthVOs }); //    ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(lisQCDataMonthVOs);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    ZhiFang.LabStar.Common.LogHelp.Debug("LabStarQCService.SearchEquipDayQCData 序列化错误:" + ex.ToString());
                }
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 仪器日指控详细信息查询
        /// </summary>
        /// <param name="EqID"></param>
        /// <param name="fields"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue SearchEquipDayQCMFull(string QCMIDs, string fields, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<MultipleConcentrationQCMInfoFull> multipleConcentrationQCMInfoFulls = IBLBQCItem.SearchEquipDayQCMFull(QCMIDs, startDate, endDate);
                if (multipleConcentrationQCMInfoFulls.Count > 0)
                {
                    try
                    {
                        baseResultDataValue.success = true;
                        ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                        baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish(new EntityList<MultipleConcentrationQCMInfoFull>() { count = multipleConcentrationQCMInfoFulls.Count, list = multipleConcentrationQCMInfoFulls }); //    ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(lisQCDataMonthVOs);
                    }
                    catch (Exception ex)
                    {
                        baseResultDataValue.success = false;
                        baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                        ZhiFang.LabStar.Common.LogHelp.Debug("LabStarQCService.SearchEquipDayQCMFull 序列化错误:" + ex.ToString());
                    }
                }

            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue SearchEquipDayQCMalt(long EquipId, string EquipModel, string EquipGroup, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<LBQCMaterial> lBQCMaterials = IBLBQCItem.SearchEquipDayQCM(EquipId, EquipModel, EquipGroup);
                try
                {
                    baseResultDataValue.success = true;
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish(new EntityList<LBQCMaterial>() { count = lBQCMaterials.Count, list = lBQCMaterials }); //    ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(lisQCDataMonthVOs);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    ZhiFang.LabStar.Common.LogHelp.Debug("LabStarQCService.SearchEquipDayQCMalt 序列化错误:" + ex.ToString());
                }
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 失控处理树
        /// </summary>
        /// <returns></returns>
        public BaseResultDataValue GetOutControlTree(string type)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                BaseResultTree baseResultTree = new BaseResultTree() { Tree = new List<tree>() };
                switch (type)
                {
                    case "1":
                        //仪器-质控物
                        baseResultTree = IBLBQCItem.GetOutControlEQ_QCMTree();
                        break;
                    case "2":
                        //仪器-项目
                        baseResultTree = IBLBQCItem.GetOutControlEQ_ITEMTree();
                        break;
                    case "3":
                        //项目 
                        baseResultTree = IBLBQCItem.GetOutControlITEM_QC_QCMTree();
                        break;
                }

                if (baseResultTree.Tree.Count > 0)
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
                        //throw new Exception(ex.Message);
                    }
                }
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue GetQCTempleModuleList(string Name)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();

            try
            {
                DataTable list = IBLBQCPrintTemplate.GetQCTempleModuleList(Name);
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(list);
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }

        #region 质控图
        /// <summary>
        /// LJ图
        /// </summary>
        /// <param name="QCItemId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue QCMFigureLJ(long QCItemId, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                QCMFigureLJ qCMFigureLJ = IBLisQCData.QCMFigureLJ(QCItemId, startDate, endDate);
                //ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(qCMFigureLJ);// tempParseObjectProperty.GetObjectPropertyNoPlanish(qCMFigureLJ);
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.ToString();
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// Z图
        /// </summary>
        /// <param name="QCItemId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue QCMFigureZ(long EquipId, long QCMId, long ItemId, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                QCMFigureZ qCMFigureZ = IBLisQCData.QCMFigureZ(EquipId, QCMId, ItemId, startDate, endDate);
                //ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(qCMFigureZ);// tempParseObjectProperty.GetObjectPropertyNoPlanish(qCMFigureLJ);
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.ToString();
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 值范围图
        /// </summary>
        /// <param name="QCItemId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns> 
        public BaseResultDataValue QCMFigureValueRange(long QCItemId, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                QCMFigureValueRange qCMFigureValueRange = IBLisQCData.QCMFigureValueRange(QCItemId, startDate, endDate);
                //ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(qCMFigureValueRange);// tempParseObjectProperty.GetObjectPropertyNoPlanish(qCMFigureLJ);
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.ToString();
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 定性图
        /// </summary>
        /// <param name="QCItemId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue QCMFigureQualitative(long QCItemId, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                QCMFigureQualitative QCMFigureQualitative = IBLisQCData.QCMFigureQualitative(QCItemId, startDate, endDate);
                //ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(QCMFigureQualitative);// tempParseObjectProperty.GetObjectPropertyNoPlanish(qCMFigureLJ);
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.ToString();
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// Monica图
        /// </summary>
        /// <param name="QCItemId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue QCMFigureValueMonica(long QCItemId, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                QCMFigureValueMonica QCMFigureValueMonica = IBLisQCData.QCMFigureValueMonica(QCItemId, startDate, endDate);
                //ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(QCMFigureValueMonica);// tempParseObjectProperty.GetObjectPropertyNoPlanish(qCMFigureLJ);
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.ToString();
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// Youden图
        /// </summary>
        /// <param name="QCItemId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue QCMFigureYouden(string QCItemIds, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var qcitems = QCItemIds.Split(',');
                if (qcitems.Length != 2)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "需要2个浓度参数";
                    return baseResultDataValue;
                }
                QCMFigureYouden QCMFigureYouden = IBLisQCData.QCMFigureYouden(new List<long>() { long.Parse(qcitems[0]), long.Parse(qcitems[1]) }, startDate, endDate);
                //ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(QCMFigureYouden);// tempParseObjectProperty.GetObjectPropertyNoPlanish(qCMFigureLJ);
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.ToString();
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 正太分布图
        /// </summary>
        /// <param name="QCItemIds"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue QCMFigureNormalDistribution(string QCItemIds, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var qcitems = QCItemIds.Split(',');
                List<long> ids = new List<long>();
                foreach (var item in qcitems)
                {
                    ids.Add(long.Parse(item));
                }

                QCMFigureNormalDistribution QCMFigureNormalDistribution = IBLisQCData.QCMFigureNormalDistribution(ids, startDate, endDate);
                //ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty("");
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(QCMFigureNormalDistribution);// tempParseObjectProperty.GetObjectPropertyNoPlanish(qCMFigureLJ);
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.ToString();
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 累积和图
        /// </summary>
        /// <param name="QCItemId"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue QCMFigureCumulativeSumGraph(long QCItemId, double Target, double SD, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                QCMFigureCumulativeSumGraph QCMFigureValueMonica = IBLisQCData.QCMFigureCumulativeSumGraph(QCItemId, Target, SD, startDate, endDate);
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(QCMFigureValueMonica);// tempParseObjectProperty.GetObjectPropertyNoPlanish(qCMFigureLJ);
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.ToString();
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 频数分布图
        /// </summary>
        /// <param name="QCItemId"></param>
        /// <param name="Target"></param>
        /// <param name="SD"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public BaseResultDataValue QCMFigureFrequencyDistribution(long QCItemId, double Target, double SD, DateTime startDate, DateTime endDate)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                QCMFigureFrequencyDistribution qCMFigureFrequencyDistribution = IBLisQCData.QCMFigureFrequencyDistribution(QCItemId, Target, SD, startDate, endDate);
                baseResultDataValue.ResultDataValue = Newtonsoft.Json.JsonConvert.SerializeObject(qCMFigureFrequencyDistribution);// tempParseObjectProperty.GetObjectPropertyNoPlanish(qCMFigureLJ);
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.ToString();
            }
            return baseResultDataValue;
        }
        #endregion
        #endregion

        #region 检验业务需要的质控服务
        /// <summary>
        /// 根据小组找仪器质控物
        /// </summary>
        /// <param name="SectionId"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        public BaseResultDataValue SearchQCMaterialbySectionEquip(long SectionId, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                List<LBQCMaterial> lBQCMaterials = IBLBQCMaterial.SearchQCMaterialbySectionEquip(SectionId);
                try
                {
                    baseResultDataValue.success = true;
                    ParseObjectProperty tempParseObjectProperty = new ParseObjectProperty(fields);
                    baseResultDataValue.ResultDataValue = tempParseObjectProperty.GetObjectListPlanish(new EntityList<LBQCMaterial>() { count = lBQCMaterials.Count, list = lBQCMaterials }); //    ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(lisQCDataMonthVOs);
                }
                catch (Exception ex)
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "序列化错误：" + ex.Message;
                    ZhiFang.LabStar.Common.LogHelp.Debug("LabStarQCService.SearchQCMaterialbySectionEquip 序列化错误:" + ex.ToString());
                }
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }
            return baseResultDataValue;
        }
        /// <summary>
        /// 检验结果转为质控
        /// </summary>
        /// <param name="QCMatId"></param>
        /// <param name="TestFormId"></param>
        /// <returns></returns>
        public BaseResultBool TestFormConvatQCItem(long QCMatId, long TestFormId)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            baseResultBool.BoolFlag = false;
            try
            {
                var QCItems = IBLBQCItem.SearchListByHQL("LBQCMaterial.Id=" + QCMatId);
                var testItems = IBLisTestItem.SearchListByHQL("LisTestForm.Id=" + TestFormId);
                int convatcount = 0;
                QCItems.ToList().ForEach(a =>
                {
                    var item = testItems.Where(b => b.LBItem.Id == a.LBItem.Id);
                    if (item.Count() > 0)
                    {
                        var receiveTime = item.First().GTestDate.Value.ToString("yyyy-MM-dd") + " " + item.First().TestTime.Value.ToString("yyyy-MM-dd HH:mm:ss").Split(' ')[1];
                        bool flag = IBLisQCData.AddLisQCData(new LisQCData() { LBQCItem = a, ReportValue = item.First().ReportValue, ReceiveTime = DateTime.Parse(receiveTime) });
                        if (flag)
                        {
                            convatcount++;
                        }
                    }
                });
                if (convatcount == testItems.Count)
                {
                    baseResultBool.BoolFlag = true;
                }
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = e.ToString();
            }

            return baseResultBool;
        }

        #endregion

        #region 质控报告打印
        public BaseResultDataValue MathQCMReport()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            #region 文件验证
            int iTotal = HttpContext.Current.Request.Files.Count;
            SortedList<string, System.IO.Stream> streams = new SortedList<string, System.IO.Stream>();
            ZhiFang.LabStar.Common.LogHelp.Debug("接收到的文件数量：" + iTotal);
            if (!(HttpContext.Current.Request.Form.AllKeys.Contains("StartDate") && HttpContext.Current.Request.Form["StartDate"] != null && HttpContext.Current.Request.Form["StartDate"].Trim() != ""))
            {
                baseResultDataValue.ErrorInfo = "未检测到开始时间！";
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.success = false;
                return baseResultDataValue;
            }
            if (!(HttpContext.Current.Request.Form.AllKeys.Contains("EndDate") && HttpContext.Current.Request.Form["EndDate"] != null && HttpContext.Current.Request.Form["EndDate"].Trim() != ""))
            {
                baseResultDataValue.ErrorInfo = "未检测到结束时间！";
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.success = false;
                return baseResultDataValue;
            }
            if (!(HttpContext.Current.Request.Form.AllKeys.Contains("tempath") && HttpContext.Current.Request.Form["tempath"] != null && HttpContext.Current.Request.Form["tempath"].Trim() != ""))
            {
                baseResultDataValue.ErrorInfo = "未检测到模板名称！";
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.success = false;
                return baseResultDataValue;
            }
            if (!(HttpContext.Current.Request.Form.AllKeys.Contains("PrintType") && HttpContext.Current.Request.Form["PrintType"] != null && HttpContext.Current.Request.Form["PrintType"].Trim() != ""))
            {
                baseResultDataValue.ErrorInfo = "未检测到打印格式类型！";
                baseResultDataValue.ResultDataValue = "";
                baseResultDataValue.success = false;
                return baseResultDataValue;
            }
            #endregion
            //传递的参数
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();

            //根据打印格式区分传入参数
            switch (HttpContext.Current.Request.Form.GetValues("PrintType")[0])
            {
                case "0":
                    //日指控
                    if (!(HttpContext.Current.Request.Form.AllKeys.Contains("QCMatID") && HttpContext.Current.Request.Form["QCMatID"] != null && HttpContext.Current.Request.Form["QCMatID"].Trim() != ""))
                    {
                        baseResultDataValue.ErrorInfo = "未检测到质控物id！";
                        baseResultDataValue.ResultDataValue = "";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    keyValuePairs.Add("QCMatID", HttpContext.Current.Request.Form.GetValues("QCMatID")[0]);//质控物id
                    break;
                case "1":
                    //仪器日指控
                    if (!(HttpContext.Current.Request.Form.AllKeys.Contains("QCMatIDs") && HttpContext.Current.Request.Form["QCMatIDs"] != null && HttpContext.Current.Request.Form["QCMatIDs"].Trim() != ""))
                    {
                        baseResultDataValue.ErrorInfo = "未检测到质控物id！";
                        baseResultDataValue.ResultDataValue = "";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    keyValuePairs.Add("QCMatIDs", HttpContext.Current.Request.Form.GetValues("QCMatIDs")[0]);//质控物id
                    break;
                case "2":
                    if (iTotal == 0)
                    {
                        baseResultDataValue.ErrorInfo = "未检测到文件！";
                        baseResultDataValue.ResultDataValue = "";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    else
                    {
                        //获得需要打印的质控图
                        for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                        {
                            var item = HttpContext.Current.Request.Files[i];
                            streams.Add(item.FileName, item.InputStream);
                        }
                    }
                    //月指控
                    if (!(HttpContext.Current.Request.Form.AllKeys.Contains("QCMatID") && HttpContext.Current.Request.Form["QCMatID"] != null && HttpContext.Current.Request.Form["QCMatID"].Trim() != ""))
                    {
                        baseResultDataValue.ErrorInfo = "未检测到质控物id！";
                        baseResultDataValue.ResultDataValue = "";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    if (!(HttpContext.Current.Request.Form.AllKeys.Contains("QCItemId") && HttpContext.Current.Request.Form["QCItemId"] != null && HttpContext.Current.Request.Form["QCItemId"].Trim() != ""))
                    {
                        baseResultDataValue.ErrorInfo = "未检测到质控项目id！";
                        baseResultDataValue.ResultDataValue = "";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    keyValuePairs.Add("QCMatID", HttpContext.Current.Request.Form.GetValues("QCMatID")[0]);
                    keyValuePairs.Add("QCItemId", HttpContext.Current.Request.Form.GetValues("QCItemId")[0]);
                    break;
                case "3":
                    //多浓度指控
                    if (iTotal == 0)
                    {
                        baseResultDataValue.ErrorInfo = "未检测到文件！";
                        baseResultDataValue.ResultDataValue = "";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    else
                    {
                        //获得需要打印的质控图
                        for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                        {
                            var item = HttpContext.Current.Request.Files[i];
                            streams.Add(item.FileName, item.InputStream);
                        }
                    }
                    if (!(HttpContext.Current.Request.Form.AllKeys.Contains("EquipId") && HttpContext.Current.Request.Form["EquipId"] != null && HttpContext.Current.Request.Form["EquipId"].Trim() != ""))
                    {
                        baseResultDataValue.ErrorInfo = "未检测到仪器id！";
                        baseResultDataValue.ResultDataValue = "";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    if (!(HttpContext.Current.Request.Form.AllKeys.Contains("ItemId") && HttpContext.Current.Request.Form["ItemId"] != null && HttpContext.Current.Request.Form["ItemId"].Trim() != ""))
                    {
                        baseResultDataValue.ErrorInfo = "未检测到项目id！";
                        baseResultDataValue.ResultDataValue = "";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    if (!(HttpContext.Current.Request.Form.AllKeys.Contains("EquipGroup") && HttpContext.Current.Request.Form["EquipGroup"] != null))
                    {
                        baseResultDataValue.ErrorInfo = "未检测到仪器分组！";
                        baseResultDataValue.ResultDataValue = "";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    if (!(HttpContext.Current.Request.Form.AllKeys.Contains("EquipModel") && HttpContext.Current.Request.Form["EquipModel"] != null))
                    {
                        baseResultDataValue.ErrorInfo = "未检测到仪器模块！";
                        baseResultDataValue.ResultDataValue = "";
                        baseResultDataValue.success = false;
                        return baseResultDataValue;
                    }
                    keyValuePairs.Add("EquipId", HttpContext.Current.Request.Form.GetValues("EquipId")[0]);
                    keyValuePairs.Add("ItemId", HttpContext.Current.Request.Form.GetValues("ItemId")[0]);
                    keyValuePairs.Add("EquipGroup", HttpContext.Current.Request.Form.GetValues("EquipGroup")[0]);
                    keyValuePairs.Add("EquipModel", HttpContext.Current.Request.Form.GetValues("EquipModel")[0]);
                    break;
                default:
                    baseResultDataValue.ErrorInfo = "未知的 质控打印格式类型！";
                    baseResultDataValue.ResultDataValue = "";
                    baseResultDataValue.success = false;
                    return baseResultDataValue;
            }

            try
            {
                baseResultDataValue.ResultDataValue = IBLBQCItem.QCMReportFormPrint(keyValuePairs,
                    DateTime.Parse(HttpContext.Current.Request.Form.GetValues("StartDate")[0]),
                    DateTime.Parse(HttpContext.Current.Request.Form.GetValues("EndDate")[0]),
                HttpContext.Current.Request.Form.GetValues("PrintType")[0],
                HttpContext.Current.Request.Form.GetValues("tempath")[0],
                streams
                );
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = e.Message;
            }

            return baseResultDataValue;
        }

        public BaseResultBool ReportTempleUpload()
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                string path = System.AppDomain.CurrentDomain.BaseDirectory + "/" + ZhiFang.Common.Public.ConfigHelper.GetConfigString("QCPrintTempleUrl");

                int iTotal = HttpContext.Current.Request.Files.Count;
                if (iTotal == 0)
                {
                    baseResultBool.ErrorInfo = "未检测到文件！";
                    baseResultBool.success = false;
                    return baseResultBool;
                }
                //获得需要打印的质控图
                for (int i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                {
                    var item = HttpContext.Current.Request.Files[i];
                    item.SaveAs(path + "/" + item.FileName);
                }
            }
            catch (Exception e)
            {
                baseResultBool.success = false;
                baseResultBool.ErrorInfo = e.Message;
            }
            return baseResultBool;
        }

        #endregion



    }
}
