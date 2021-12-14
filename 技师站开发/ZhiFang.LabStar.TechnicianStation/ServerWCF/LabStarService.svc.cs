using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.Text;
using System.Web;
using ZhiFang.Common.Public;
using ZhiFang.Entity.Base;
using ZhiFang.Entity.LabStar;
using ZhiFang.Entity.LabStar.ViewObject.Response;
using ZhiFang.Entity.RBAC;
using ZhiFang.IBLL.LabStar;
using ZhiFang.ServiceCommon.RBAC;

namespace ZhiFang.LabStar.TechnicianStation.ServerWCF
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]

    //检验中业务系统服务：业务表基础服务及其相关的定制服务
    public class LabStarService : ILabStarService
    {
        #region
        ZhiFang.IBLL.LabStar.IBLisCommon IBLisCommon { get; set; }

        ZhiFang.IBLL.LabStar.IBLisBarCodeForm IBLisBarCodeForm { get; set; }

        ZhiFang.IBLL.LabStar.IBLisBarCodeItem IBLisBarCodeItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLisEquipForm IBLisEquipForm { get; set; }

        ZhiFang.IBLL.LabStar.IBLisEquipItem IBLisEquipItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLisOrderForm IBLisOrderForm { get; set; }

        ZhiFang.IBLL.LabStar.IBLisOrderItem IBLisOrderItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLisPatient IBLisPatient { get; set; }

        ZhiFang.IBLL.LabStar.IBLisTestForm IBLisTestForm { get; set; }

        ZhiFang.IBLL.LabStar.IBLisTestItem IBLisTestItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLisTestGraph IBLisTestGraph { get; set; }

        ZhiFang.IBLL.LabStar.IBLisOperateAuthorize IBLisOperateAuthorize { get; set; }

        ZhiFang.IBLL.LabStar.IBLisOperateASection IBLisOperateASection { get; set; }

        ZhiFang.IBLL.LabStar.IBLisOperate IBLisOperate { get; set; }

        ZhiFang.IBLL.LabStar.IBLisEquipGraph IBLisEquipGraph { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItemRange IBLBItemRange { get; set; }

        ZhiFang.IBLL.LabStar.IBLBItem IBLBItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLisEquipComLog IBLisEquipComLog { get; set; }

        ZhiFang.IBLL.LabStar.IBLisEquipComFile IBLisEquipComFile { get; set; }

        ZhiFang.IBLL.LabStar.IBLisTestFormMsg IBLisTestFormMsg { get; set; }

        ZhiFang.IBLL.LabStar.IBLisTestFormMsgItem IBLisTestFormMsgItem { get; set; }

        ZhiFang.IBLL.LabStar.IBLisTestItemReceive IBLisTestItemReceive { get; set; }
        #endregion

        #region LisBarCodeForm
        //Add  LisBarCodeForm
        public BaseResultDataValue LS_UDTO_AddLisBarCodeForm(LisBarCodeForm entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisBarCodeForm.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisBarCodeForm.Add();
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
        //Update  LisBarCodeForm
        public BaseResultBool LS_UDTO_UpdateLisBarCodeForm(LisBarCodeForm entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisBarCodeForm.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisBarCodeForm.Edit();
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
        //Update  LisBarCodeForm
        public BaseResultBool LS_UDTO_UpdateLisBarCodeFormByField(LisBarCodeForm entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisBarCodeForm.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisBarCodeForm.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisBarCodeForm.Update(tempArray);
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
                        //baseResultBool.success = IBLisBarCodeForm.Edit();
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
        //Delele  LisBarCodeForm
        public BaseResultBool LS_UDTO_DelLisBarCodeForm(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisBarCodeForm.Entity = IBLisBarCodeForm.Get(id);
                if (IBLisBarCodeForm.Entity != null)
                {
                    long labid = IBLisBarCodeForm.Entity.LabID;
                    string entityName = IBLisBarCodeForm.Entity.GetType().Name;
                    baseResultBool.success = IBLisBarCodeForm.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisBarCodeForm(LisBarCodeForm entity)
        {
            EntityList<LisBarCodeForm> entityList = new EntityList<LisBarCodeForm>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisBarCodeForm.Entity = entity;
                try
                {
                    entityList.list = IBLisBarCodeForm.Search();
                    entityList.count = IBLisBarCodeForm.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisBarCodeFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisBarCodeForm> entityList = new EntityList<LisBarCodeForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisBarCodeForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisBarCodeForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisBarCodeFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisBarCodeForm.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisBarCodeForm>(entity);
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

        #region LisBarCodeItem
        //Add  LisBarCodeItem
        public BaseResultDataValue LS_UDTO_AddLisBarCodeItem(LisBarCodeItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisBarCodeItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisBarCodeItem.Add();
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
        //Update  LisBarCodeItem
        public BaseResultBool LS_UDTO_UpdateLisBarCodeItem(LisBarCodeItem entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisBarCodeItem.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisBarCodeItem.Edit();
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
        //Update  LisBarCodeItem
        public BaseResultBool LS_UDTO_UpdateLisBarCodeItemByField(LisBarCodeItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisBarCodeItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisBarCodeItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisBarCodeItem.Update(tempArray);
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
                        //baseResultBool.success = IBLisBarCodeItem.Edit();
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
        //Delele  LisBarCodeItem
        public BaseResultBool LS_UDTO_DelLisBarCodeItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisBarCodeItem.Entity = IBLisBarCodeItem.Get(id);
                if (IBLisBarCodeItem.Entity != null)
                {
                    long labid = IBLisBarCodeItem.Entity.LabID;
                    string entityName = IBLisBarCodeItem.Entity.GetType().Name;
                    baseResultBool.success = IBLisBarCodeItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisBarCodeItem(LisBarCodeItem entity)
        {
            EntityList<LisBarCodeItem> entityList = new EntityList<LisBarCodeItem>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisBarCodeItem.Entity = entity;
                try
                {
                    entityList.list = IBLisBarCodeItem.Search();
                    entityList.count = IBLisBarCodeItem.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisBarCodeItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisBarCodeItem> entityList = new EntityList<LisBarCodeItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisBarCodeItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisBarCodeItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisBarCodeItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisBarCodeItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisBarCodeItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisBarCodeItem>(entity);
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

        #region LisEquipForm
        //Add  LisEquipForm
        public BaseResultDataValue LS_UDTO_AddLisEquipForm(LisEquipForm entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipForm.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisEquipForm.Add();
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
        //Update  LisEquipForm
        public BaseResultBool LS_UDTO_UpdateLisEquipForm(LisEquipForm entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipForm.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisEquipForm.Edit();
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
        //Update  LisEquipForm
        public BaseResultBool LS_UDTO_UpdateLisEquipFormByField(LisEquipForm entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipForm.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisEquipForm.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisEquipForm.Update(tempArray);
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
                        //baseResultBool.success = IBLisEquipForm.Edit();
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
        //Delele  LisEquipForm
        public BaseResultBool LS_UDTO_DelLisEquipForm(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisEquipForm.Entity = IBLisEquipForm.Get(id);
                if (IBLisEquipForm.Entity != null)
                {
                    long labid = IBLisEquipForm.Entity.LabID;
                    string entityName = IBLisEquipForm.Entity.GetType().Name;
                    baseResultBool.success = IBLisEquipForm.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipForm(LisEquipForm entity)
        {
            EntityList<LisEquipForm> entityList = new EntityList<LisEquipForm>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisEquipForm.Entity = entity;
                try
                {
                    entityList.list = IBLisEquipForm.Search();
                    entityList.count = IBLisEquipForm.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisEquipForm> entityList = new EntityList<LisEquipForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisEquipForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisEquipForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisEquipForm.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisEquipForm>(entity);
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

        #region LisEquipItem
        //Add  LisEquipItem
        public BaseResultDataValue LS_UDTO_AddLisEquipItem(LisEquipItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisEquipItem.Add();
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
        //Update  LisEquipItem
        public BaseResultBool LS_UDTO_UpdateLisEquipItem(LisEquipItem entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipItem.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisEquipItem.Edit();
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
        //Update  LisEquipItem
        public BaseResultBool LS_UDTO_UpdateLisEquipItemByField(LisEquipItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisEquipItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisEquipItem.Update(tempArray);
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
                        //baseResultBool.success = IBLisEquipItem.Edit();
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
        //Delele  LisEquipItem
        public BaseResultBool LS_UDTO_DelLisEquipItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisEquipItem.Entity = IBLisEquipItem.Get(id);
                if (IBLisEquipItem.Entity != null)
                {
                    long labid = IBLisEquipItem.Entity.LabID;
                    string entityName = IBLisEquipItem.Entity.GetType().Name;
                    baseResultBool.success = IBLisEquipItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipItem(LisEquipItem entity)
        {
            EntityList<LisEquipItem> entityList = new EntityList<LisEquipItem>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisEquipItem.Entity = entity;
                try
                {
                    entityList.list = IBLisEquipItem.Search();
                    entityList.count = IBLisEquipItem.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisEquipItem> entityList = new EntityList<LisEquipItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisEquipItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisEquipItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisEquipItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisEquipItem>(entity);
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

        #region LisOrderForm
        //Add  LisOrderForm
        public BaseResultDataValue LS_UDTO_AddLisOrderForm(LisOrderForm entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisOrderForm.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisOrderForm.Add();
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
        //Update  LisOrderForm
        public BaseResultBool LS_UDTO_UpdateLisOrderForm(LisOrderForm entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisOrderForm.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisOrderForm.Edit();
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
        //Update  LisOrderForm
        public BaseResultBool LS_UDTO_UpdateLisOrderFormByField(LisOrderForm entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisOrderForm.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisOrderForm.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisOrderForm.Update(tempArray);
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
                        //baseResultBool.success = IBLisOrderForm.Edit();
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
        //Delele  LisOrderForm
        public BaseResultBool LS_UDTO_DelLisOrderForm(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisOrderForm.Entity = IBLisOrderForm.Get(id);
                if (IBLisOrderForm.Entity != null)
                {
                    long labid = IBLisOrderForm.Entity.LabID;
                    string entityName = IBLisOrderForm.Entity.GetType().Name;
                    baseResultBool.success = IBLisOrderForm.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisOrderForm(LisOrderForm entity)
        {
            EntityList<LisOrderForm> entityList = new EntityList<LisOrderForm>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisOrderForm.Entity = entity;
                try
                {
                    entityList.list = IBLisOrderForm.Search();
                    entityList.count = IBLisOrderForm.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisOrderForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisOrderFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisOrderForm> entityList = new EntityList<LisOrderForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisOrderForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisOrderForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisOrderForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisOrderFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisOrderForm.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisOrderForm>(entity);
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

        #region LisOrderItem
        //Add  LisOrderItem
        public BaseResultDataValue LS_UDTO_AddLisOrderItem(LisOrderItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisOrderItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisOrderItem.Add();
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
        //Update  LisOrderItem
        public BaseResultBool LS_UDTO_UpdateLisOrderItem(LisOrderItem entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisOrderItem.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisOrderItem.Edit();
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
        //Update  LisOrderItem
        public BaseResultBool LS_UDTO_UpdateLisOrderItemByField(LisOrderItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisOrderItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisOrderItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisOrderItem.Update(tempArray);
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
                        //baseResultBool.success = IBLisOrderItem.Edit();
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
        //Delele  LisOrderItem
        public BaseResultBool LS_UDTO_DelLisOrderItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisOrderItem.Entity = IBLisOrderItem.Get(id);
                if (IBLisOrderItem.Entity != null)
                {
                    long labid = IBLisOrderItem.Entity.LabID;
                    string entityName = IBLisOrderItem.Entity.GetType().Name;
                    baseResultBool.success = IBLisOrderItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisOrderItem(LisOrderItem entity)
        {
            EntityList<LisOrderItem> entityList = new EntityList<LisOrderItem>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisOrderItem.Entity = entity;
                try
                {
                    entityList.list = IBLisOrderItem.Search();
                    entityList.count = IBLisOrderItem.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisOrderItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisOrderItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisOrderItem> entityList = new EntityList<LisOrderItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisOrderItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisOrderItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisOrderItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisOrderItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisOrderItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisOrderItem>(entity);
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

        #region LisPatient
        //Add  LisPatient
        public BaseResultDataValue LS_UDTO_AddLisPatient(LisPatient entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisPatient.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisPatient.Add();
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
        //Update  LisPatient
        public BaseResultBool LS_UDTO_UpdateLisPatient(LisPatient entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisPatient.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisPatient.Edit();
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
        //Update  LisPatient
        public BaseResultBool LS_UDTO_UpdateLisPatientByField(LisPatient entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisPatient.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisPatient.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisPatient.Update(tempArray);
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
                        //baseResultBool.success = IBLisPatient.Edit();
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
        //Delele  LisPatient
        public BaseResultBool LS_UDTO_DelLisPatient(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisPatient.Entity = IBLisPatient.Get(id);
                if (IBLisPatient.Entity != null)
                {
                    long labid = IBLisPatient.Entity.LabID;
                    string entityName = IBLisPatient.Entity.GetType().Name;
                    baseResultBool.success = IBLisPatient.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisPatient(LisPatient entity)
        {
            EntityList<LisPatient> entityList = new EntityList<LisPatient>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisPatient.Entity = entity;
                try
                {
                    entityList.list = IBLisPatient.Search();
                    entityList.count = IBLisPatient.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisPatient>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisPatientByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisPatient> entityList = new EntityList<LisPatient>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisPatient.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisPatient.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisPatient>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisPatientById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisPatient.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisPatient>(entity);
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

        #region LisTestForm
        //Add  LisTestForm
        public BaseResultDataValue LS_UDTO_AddLisTestForm(LisTestForm entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestForm.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisTestForm.Add();
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
        //Update  LisTestForm
        public BaseResultBool LS_UDTO_UpdateLisTestForm(LisTestForm entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestForm.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisTestForm.Edit();
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
        //Update  LisTestForm
        public BaseResultBool LS_UDTO_UpdateLisTestFormByField(LisTestForm entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestForm.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisTestForm.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisTestForm.Update(tempArray);
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
                        //baseResultBool.success = IBLisTestForm.Edit();
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
        //Delele  LisTestForm
        public BaseResultBool LS_UDTO_DelLisTestForm(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisTestForm.Entity = IBLisTestForm.Get(id);
                if (IBLisTestForm.Entity != null)
                {
                    long labid = IBLisTestForm.Entity.LabID;
                    string entityName = IBLisTestForm.Entity.GetType().Name;
                    baseResultBool.success = IBLisTestForm.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestForm(LisTestForm entity)
        {
            EntityList<LisTestForm> entityList = new EntityList<LisTestForm>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisTestForm.Entity = entity;
                try
                {
                    entityList.list = IBLisTestForm.Search();
                    entityList.count = IBLisTestForm.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestForm> entityList = new EntityList<LisTestForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisTestForm.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisTestForm.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestFormById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisTestForm.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisTestForm>(entity);
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

        #region LisTestItem
        //Add  LisTestItem
        public BaseResultDataValue LS_UDTO_AddLisTestItem(LisTestItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisTestItem.Add();
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
        //Update  LisTestItem
        public BaseResultBool LS_UDTO_UpdateLisTestItem(LisTestItem entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestItem.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisTestItem.Edit();
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
        //Update  LisTestItem
        public BaseResultBool LS_UDTO_UpdateLisTestItemByField(LisTestItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisTestItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisTestItem.Update(tempArray);
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
                        //baseResultBool.success = IBLisTestItem.Edit();
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
        //Delele  LisTestItem
        public BaseResultBool LS_UDTO_DelLisTestItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisTestItem.Entity = IBLisTestItem.Get(id);
                if (IBLisTestItem.Entity != null)
                {
                    long labid = IBLisTestItem.Entity.LabID;
                    string entityName = IBLisTestItem.Entity.GetType().Name;
                    baseResultBool.success = IBLisTestItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestItem(LisTestItem entity)
        {
            EntityList<LisTestItem> entityList = new EntityList<LisTestItem>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisTestItem.Entity = entity;
                try
                {
                    entityList.list = IBLisTestItem.Search();
                    entityList.count = IBLisTestItem.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestItem> entityList = new EntityList<LisTestItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisTestItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisTestItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisTestItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisTestItem>(entity);
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

        #region LisTestGraph
        //Add  LisTestGraph
        public BaseResultDataValue LS_UDTO_AddLisTestGraph(LisTestGraph entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestGraph.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisTestGraph.Add();
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
        //Update  LisTestGraph
        public BaseResultBool LS_UDTO_UpdateLisTestGraph(LisTestGraph entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestGraph.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisTestGraph.Edit();
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
        //Update  LisTestGraph
        public BaseResultBool LS_UDTO_UpdateLisTestGraphByField(LisTestGraph entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestGraph.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisTestGraph.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisTestGraph.Update(tempArray);
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
                        //baseResultBool.success = IBLisTestGraph.Edit();
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
        //Delele  LisTestGraph
        public BaseResultBool LS_UDTO_DelLisTestGraph(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisTestGraph.Entity = IBLisTestGraph.Get(id);
                if (IBLisTestGraph.Entity != null)
                {
                    long labid = IBLisTestGraph.Entity.LabID;
                    string entityName = IBLisTestGraph.Entity.GetType().Name;
                    baseResultBool.success = IBLisTestGraph.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestGraph(LisTestGraph entity)
        {
            EntityList<LisTestGraph> entityList = new EntityList<LisTestGraph>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisTestGraph.Entity = entity;
                try
                {
                    entityList.list = IBLisTestGraph.Search();
                    entityList.count = IBLisTestGraph.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestGraph>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestGraphByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestGraph> entityList = new EntityList<LisTestGraph>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisTestGraph.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisTestGraph.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestGraph>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestGraphById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisTestGraph.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisTestGraph>(entity);
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

        #region LisOperateAuthorize
        //Add  LisOperateAuthorize
        public BaseResultDataValue LS_UDTO_AddLisOperateAuthorize(LisOperateAuthorize entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisOperateAuthorize.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisOperateAuthorize.Add();
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
        //Update  LisOperateAuthorize
        public BaseResultBool LS_UDTO_UpdateLisOperateAuthorize(LisOperateAuthorize entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisOperateAuthorize.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisOperateAuthorize.Edit();
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
        //Update  LisOperateAuthorize
        public BaseResultBool LS_UDTO_UpdateLisOperateAuthorizeByField(LisOperateAuthorize entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisOperateAuthorize.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisOperateAuthorize.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisOperateAuthorize.Update(tempArray);
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
                        //baseResultBool.success = IBLisOperateAuthorize.Edit();
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
        //Delele  LisOperateAuthorize
        public BaseResultBool LS_UDTO_DelLisOperateAuthorize(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisOperateAuthorize.Entity = IBLisOperateAuthorize.Get(id);
                if (IBLisOperateAuthorize.Entity != null)
                {
                    long labid = IBLisOperateAuthorize.Entity.LabID;
                    string entityName = IBLisOperateAuthorize.Entity.GetType().Name;
                    baseResultBool.success = IBLisOperateAuthorize.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisOperateAuthorize(LisOperateAuthorize entity)
        {
            EntityList<LisOperateAuthorize> entityList = new EntityList<LisOperateAuthorize>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisOperateAuthorize.Entity = entity;
                try
                {
                    entityList.list = IBLisOperateAuthorize.Search();
                    entityList.count = IBLisOperateAuthorize.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisOperateAuthorize>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisOperateAuthorizeByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisOperateAuthorize> entityList = new EntityList<LisOperateAuthorize>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisOperateAuthorize.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisOperateAuthorize.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisOperateAuthorize>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisOperateAuthorizeById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisOperateAuthorize.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisOperateAuthorize>(entity);
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

        #region LisOperateASection
        //Add  LisOperateASection
        public BaseResultDataValue LS_UDTO_AddLisOperateASection(LisOperateASection entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                //entity.DataUpdateTime = DateTime.Now;
                IBLisOperateASection.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisOperateASection.Add();
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
        //Update  LisOperateASection
        public BaseResultBool LS_UDTO_UpdateLisOperateASection(LisOperateASection entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                //entity.DataUpdateTime = DateTime.Now;
                IBLisOperateASection.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisOperateASection.Edit();
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
        //Update  LisOperateASection
        public BaseResultBool LS_UDTO_UpdateLisOperateASectionByField(LisOperateASection entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                //entity.DataUpdateTime = DateTime.Now;
                IBLisOperateASection.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisOperateASection.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisOperateASection.Update(tempArray);
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
                        //baseResultBool.success = IBLisOperateASection.Edit();
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
        //Delele  LisOperateASection
        public BaseResultBool LS_UDTO_DelLisOperateASection(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisOperateASection.Entity = IBLisOperateASection.Get(id);
                if (IBLisOperateASection.Entity != null)
                {
                    long labid = IBLisOperateASection.Entity.LabID;
                    string entityName = IBLisOperateASection.Entity.GetType().Name;
                    baseResultBool.success = IBLisOperateASection.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisOperateASection(LisOperateASection entity)
        {
            EntityList<LisOperateASection> entityList = new EntityList<LisOperateASection>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisOperateASection.Entity = entity;
                try
                {
                    entityList.list = IBLisOperateASection.Search();
                    entityList.count = IBLisOperateASection.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisOperateASection>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisOperateASectionByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisOperateASection> entityList = new EntityList<LisOperateASection>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisOperateASection.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisOperateASection.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisOperateASection>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisOperateASectionById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisOperateASection.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisOperateASection>(entity);
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

        #region LisOperate
        //Add  LisOperate
        public BaseResultDataValue LS_UDTO_AddLisOperate(LisOperate entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisOperate.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisOperate.Add();
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
        //Update  LisOperate
        public BaseResultBool LS_UDTO_UpdateLisOperate(LisOperate entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisOperate.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisOperate.Edit();
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
        //Update  LisOperate
        public BaseResultBool LS_UDTO_UpdateLisOperateByField(LisOperate entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisOperate.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisOperate.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisOperate.Update(tempArray);
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
                        //baseResultBool.success = IBLisOperate.Edit();
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
        //Delele  LisOperate
        public BaseResultBool LS_UDTO_DelLisOperate(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisOperate.Entity = IBLisOperate.Get(id);
                if (IBLisOperate.Entity != null)
                {
                    long labid = IBLisOperate.Entity.LabID;
                    string entityName = IBLisOperate.Entity.GetType().Name;
                    baseResultBool.success = IBLisOperate.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisOperate(LisOperate entity)
        {
            EntityList<LisOperate> entityList = new EntityList<LisOperate>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisOperate.Entity = entity;
                try
                {
                    entityList.list = IBLisOperate.Search();
                    entityList.count = IBLisOperate.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisOperate>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisOperateByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisOperate> entityList = new EntityList<LisOperate>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisOperate.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisOperate.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisOperate>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisOperateById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisOperate.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisOperate>(entity);
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

        #region LisEquipGraph
        //Add  LisEquipGraph
        public BaseResultDataValue LS_UDTO_AddLisEquipGraph(LisEquipGraph entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipGraph.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisEquipGraph.Add();
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
        //Update  LisEquipGraph
        public BaseResultBool LS_UDTO_UpdateLisEquipGraph(LisEquipGraph entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipGraph.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisEquipGraph.Edit();
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
        //Update  LisEquipGraph
        public BaseResultBool LS_UDTO_UpdateLisEquipGraphByField(LisEquipGraph entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipGraph.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisEquipGraph.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisEquipGraph.Update(tempArray);
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
                        //baseResultBool.success = IBLisEquipGraph.Edit();
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
        //Delele  LisEquipGraph
        public BaseResultBool LS_UDTO_DelLisEquipGraph(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisEquipGraph.Entity = IBLisEquipGraph.Get(id);
                if (IBLisEquipGraph.Entity != null)
                {
                    long labid = IBLisEquipGraph.Entity.LabID;
                    string entityName = IBLisEquipGraph.Entity.GetType().Name;
                    baseResultBool.success = IBLisEquipGraph.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipGraph(LisEquipGraph entity)
        {
            EntityList<LisEquipGraph> entityList = new EntityList<LisEquipGraph>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisEquipGraph.Entity = entity;
                try
                {
                    entityList.list = IBLisEquipGraph.Search();
                    entityList.count = IBLisEquipGraph.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipGraph>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipGraphByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisEquipGraph> entityList = new EntityList<LisEquipGraph>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisEquipGraph.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisEquipGraph.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipGraph>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipGraphById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisEquipGraph.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisEquipGraph>(entity);
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

        #region LisEquipComLog
        //Add  LisEquipComLog
        public BaseResultDataValue LS_UDTO_AddLisEquipComLog(LisEquipComLog entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipComLog.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisEquipComLog.Add();
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
        //Update  LisEquipComLog
        public BaseResultBool LS_UDTO_UpdateLisEquipComLog(LisEquipComLog entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipComLog.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisEquipComLog.Edit();
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
        //Update  LisEquipComLog
        public BaseResultBool LS_UDTO_UpdateLisEquipComLogByField(LisEquipComLog entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipComLog.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisEquipComLog.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisEquipComLog.Update(tempArray);
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
                        //baseResultBool.success = IBLisEquipComLog.Edit();
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
        //Delele  LisEquipComLog
        public BaseResultBool LS_UDTO_DelLisEquipComLog(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisEquipComLog.Entity = IBLisEquipComLog.Get(id);
                if (IBLisEquipComLog.Entity != null)
                {
                    long labid = IBLisEquipComLog.Entity.LabID;
                    string entityName = IBLisEquipComLog.Entity.GetType().Name;
                    baseResultBool.success = IBLisEquipComLog.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipComLog(LisEquipComLog entity)
        {
            EntityList<LisEquipComLog> entityList = new EntityList<LisEquipComLog>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisEquipComLog.Entity = entity;
                try
                {
                    entityList.list = IBLisEquipComLog.Search();
                    entityList.count = IBLisEquipComLog.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipComLog>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipComLogByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisEquipComLog> entityList = new EntityList<LisEquipComLog>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisEquipComLog.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisEquipComLog.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipComLog>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipComLogById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisEquipComLog.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisEquipComLog>(entity);
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

        #region LisEquipComFile
        //Add  LisEquipComFile
        public BaseResultDataValue LS_UDTO_AddLisEquipComFile(LisEquipComFile entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipComFile.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisEquipComFile.Add();
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
        //Update  LisEquipComFile
        public BaseResultBool LS_UDTO_UpdateLisEquipComFile(LisEquipComFile entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipComFile.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisEquipComFile.Edit();
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
        //Update  LisEquipComFile
        public BaseResultBool LS_UDTO_UpdateLisEquipComFileByField(LisEquipComFile entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisEquipComFile.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisEquipComFile.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisEquipComFile.Update(tempArray);
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
                        //baseResultBool.success = IBLisEquipComFile.Edit();
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
        //Delele  LisEquipComFile
        public BaseResultBool LS_UDTO_DelLisEquipComFile(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisEquipComFile.Entity = IBLisEquipComFile.Get(id);
                if (IBLisEquipComFile.Entity != null)
                {
                    long labid = IBLisEquipComFile.Entity.LabID;
                    string entityName = IBLisEquipComFile.Entity.GetType().Name;
                    baseResultBool.success = IBLisEquipComFile.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipComFile(LisEquipComFile entity)
        {
            EntityList<LisEquipComFile> entityList = new EntityList<LisEquipComFile>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisEquipComFile.Entity = entity;
                try
                {
                    entityList.list = IBLisEquipComFile.Search();
                    entityList.count = IBLisEquipComFile.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipComFile>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipComFileByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisEquipComFile> entityList = new EntityList<LisEquipComFile>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisEquipComFile.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisEquipComFile.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipComFile>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisEquipComFileById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisEquipComFile.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisEquipComFile>(entity);
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

        #region LisTestFormMsg
        //Add  LisTestFormMsg
        public BaseResultDataValue LS_UDTO_AddLisTestFormMsg(LisTestFormMsg entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestFormMsg.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisTestFormMsg.Add();
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
        //Update  LisTestFormMsg
        public BaseResultBool LS_UDTO_UpdateLisTestFormMsg(LisTestFormMsg entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestFormMsg.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisTestFormMsg.Edit();
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
        //Update  LisTestFormMsg
        public BaseResultBool LS_UDTO_UpdateLisTestFormMsgByField(LisTestFormMsg entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestFormMsg.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisTestFormMsg.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisTestFormMsg.Update(tempArray);
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
                        //baseResultBool.success = IBLisTestFormMsg.Edit();
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
        //Delele  LisTestFormMsg
        public BaseResultBool LS_UDTO_DelLisTestFormMsg(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisTestFormMsg.Entity = IBLisTestFormMsg.Get(id);
                if (IBLisTestFormMsg.Entity != null)
                {
                    long labid = IBLisTestFormMsg.Entity.LabID;
                    string entityName = IBLisTestFormMsg.Entity.GetType().Name;
                    baseResultBool.success = IBLisTestFormMsg.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestFormMsg(LisTestFormMsg entity)
        {
            EntityList<LisTestFormMsg> entityList = new EntityList<LisTestFormMsg>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisTestFormMsg.Entity = entity;
                try
                {
                    entityList.list = IBLisTestFormMsg.Search();
                    entityList.count = IBLisTestFormMsg.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestFormMsg>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestFormMsgByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestFormMsg> entityList = new EntityList<LisTestFormMsg>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisTestFormMsg.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisTestFormMsg.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestFormMsg>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestFormMsgById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisTestFormMsg.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisTestFormMsg>(entity);
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

        #region LisTestFormMsgItem
        //Add  LisTestFormMsgItem
        public BaseResultDataValue LS_UDTO_AddLisTestFormMsgItem(LisTestFormMsgItem entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestFormMsgItem.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisTestFormMsgItem.Add();
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
        //Update  LisTestFormMsgItem
        public BaseResultBool LS_UDTO_UpdateLisTestFormMsgItem(LisTestFormMsgItem entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestFormMsgItem.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisTestFormMsgItem.Edit();
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
        //Update  LisTestFormMsgItem
        public BaseResultBool LS_UDTO_UpdateLisTestFormMsgItemByField(LisTestFormMsgItem entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestFormMsgItem.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisTestFormMsgItem.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisTestFormMsgItem.Update(tempArray);
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
                        //baseResultBool.success = IBLisTestFormMsgItem.Edit();
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
        //Delele  LisTestFormMsgItem
        public BaseResultBool LS_UDTO_DelLisTestFormMsgItem(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisTestFormMsgItem.Entity = IBLisTestFormMsgItem.Get(id);
                if (IBLisTestFormMsgItem.Entity != null)
                {
                    long labid = IBLisTestFormMsgItem.Entity.LabID;
                    string entityName = IBLisTestFormMsgItem.Entity.GetType().Name;
                    baseResultBool.success = IBLisTestFormMsgItem.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestFormMsgItem(LisTestFormMsgItem entity)
        {
            EntityList<LisTestFormMsgItem> entityList = new EntityList<LisTestFormMsgItem>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisTestFormMsgItem.Entity = entity;
                try
                {
                    entityList.list = IBLisTestFormMsgItem.Search();
                    entityList.count = IBLisTestFormMsgItem.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestFormMsgItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestFormMsgItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestFormMsgItem> entityList = new EntityList<LisTestFormMsgItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisTestFormMsgItem.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisTestFormMsgItem.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestFormMsgItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestFormMsgItemById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisTestFormMsgItem.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisTestFormMsgItem>(entity);
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

        #region LisTestItemReceive
        //Add  LisTestItemReceive
        public BaseResultDataValue LS_UDTO_AddLisTestItemReceive(LisTestItemReceive entity)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                entity.DataAddTime = DateTime.Now;
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestItemReceive.Entity = entity;
                try
                {
                    baseResultDataValue.success = IBLisTestItemReceive.Add();
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
        //Update  LisTestItemReceive
        public BaseResultBool LS_UDTO_UpdateLisTestItemReceive(LisTestItemReceive entity)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestItemReceive.Entity = entity;
                try
                {
                    baseResultBool.success = IBLisTestItemReceive.Edit();
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
        //Update  LisTestItemReceive
        public BaseResultBool LS_UDTO_UpdateLisTestItemReceiveByField(LisTestItemReceive entity, string fields)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            if (entity != null)
            {
                if (!string.IsNullOrEmpty(fields))
                    fields = fields + ",DataUpdateTime";
                entity.DataUpdateTime = DateTime.Now;
                IBLisTestItemReceive.Entity = entity;
                try
                {
                    if ((fields != null) && (fields.Length > 0))
                    {
                        string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(IBLisTestItemReceive.Entity, fields);
                        if (tempArray != null)
                        {
                            baseResultBool.success = IBLisTestItemReceive.Update(tempArray);
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
                        //baseResultBool.success = IBLisTestItemReceive.Edit();
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
        //Delele  LisTestItemReceive
        public BaseResultBool LS_UDTO_DelLisTestItemReceive(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisTestItemReceive.Entity = IBLisTestItemReceive.Get(id);
                if (IBLisTestItemReceive.Entity != null)
                {
                    long labid = IBLisTestItemReceive.Entity.LabID;
                    string entityName = IBLisTestItemReceive.Entity.GetType().Name;
                    baseResultBool.success = IBLisTestItemReceive.RemoveByHQL(id);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestItemReceive(LisTestItemReceive entity)
        {
            EntityList<LisTestItemReceive> entityList = new EntityList<LisTestItemReceive>();
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entity != null)
            {
                IBLisTestItemReceive.Entity = entity;
                try
                {
                    entityList.list = IBLisTestItemReceive.Search();
                    entityList.count = IBLisTestItemReceive.GetTotalCount();
                    ParseObjectProperty pop = new ParseObjectProperty("");
                    try
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestItemReceive>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestItemReceiveByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestItemReceive> entityList = new EntityList<LisTestItemReceive>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisTestItemReceive.SearchListByHQL(where, CommonServiceMethod.GetSortHQL(sort), page, limit);
                }
                else
                {
                    entityList = IBLisTestItemReceive.SearchListByHQL(where, page, limit);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestItemReceive>(entityList);
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

        public BaseResultDataValue LS_UDTO_SearchLisTestItemReceiveById(long id, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                var entity = IBLisTestItemReceive.Get(id);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetSingleObjectPlanish<LisTestItemReceive>(entity);
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

        #region 检验单定制服务

        public BaseResultDataValue LS_UDTO_GetNewSampleNoByOldSampleNo(long sectionID, string testDate, string curSampleNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisTestForm.CreateNewSampleNoByOldSampleNo(sectionID, testDate, curSampleNo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_QueryNextSampleNoByCurSampleNo(string curSampleNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisTestForm.QueryNextSampleNoByCurSampleNo(curSampleNo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;

        }

        public BaseResultDataValue LS_UDTO_BatchCreateSampleNoByCurSampleNo(string curSampleNo, int SampleNoCount)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IList<string> listSampleNo = IBLisTestForm.BatchCreateNewSampleNoByOldSampleNo(curSampleNo, SampleNoCount);
                if (listSampleNo != null && listSampleNo.Count > 0)
                    baseResultDataValue.ResultDataValue = string.Join(",", listSampleNo.ToArray());
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 新增检验单
        /// 检验单和检验单项目都不存在
        /// </summary>
        /// <param name="testForm">检验单实体</param>
        /// <param name="listTestItem">检验单项目列表</param>
        /// <param name="isCreateSampleNo">是否创建样本号</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_AddSingleLisTestForm(LisTestForm testForm, IList<LisTestItem> listTestItem, bool isCreateSampleNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ZhiFang.LabStar.Common.SysDelegateVar.SendSysMsgDelegateVar = SendSysMessage.SendSysMessageDelegate;
            if (testForm != null)
            {
                try
                {
                    baseResultDataValue = IBLisTestForm.AddSingleLisTestForm(testForm, listTestItem, isCreateSampleNo);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue.ResultDataValue = CommonServiceMethod.GetAddMethodResultStr(testForm.Id);
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
                baseResultDataValue.ErrorInfo = "错误信息：检验单实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 编辑检验单信息
        /// </summary>
        /// <param name="testForm">检验单实体</param>
        /// <param name="testFormFields">检验单编辑字段列表</param>
        /// <param name="patientFields">患者信息编辑字段列表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_EditLisTestFormByField(LisTestForm testForm, string testFormFields, string patientFields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (testForm != null)
            {
                if ((testFormFields != null) && (testFormFields.Trim().Length > 0))
                {
                    testFormFields = testFormFields + ",DataUpdateTime";
                    testForm.DataUpdateTime = DateTime.Now;
                    baseResultDataValue = IBLisTestForm.LisTestFormEditByField(testForm, testFormFields, patientFields);
                    if (baseResultDataValue.success)
                    {
                        IBLisTestForm.EditLisTestItemAfterTreatment(testForm.Id);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：fields参数不能为空！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：检验单实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 编辑检验单和项目结果信息
        /// </summary>
        /// <param name="testForm">检验单实体</param>
        /// <param name="testFormFields">检验单编辑字段列表</param>
        /// <param name="patientFields">患者信息编辑字段列表</param>
        /// <param name="listTestItemResult">检验单项目结果</param>
        /// <param name="testItemFileds">检验单项目结果字段列表（注意：不包括项目结果字段ReportValue）</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_EditLisTestFormAndItemByField(LisTestForm testForm, string testFormFields, string patientFields, IList<LisTestItem> listTestItemResult, string testItemFileds)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ZhiFang.LabStar.Common.SysDelegateVar.SendSysMsgDelegateVar = SendSysMessage.SendSysMessageDelegate;
            if (testForm != null)
            {
                if ((testFormFields != null) && (testFormFields.Trim().Length > 0))
                {
                    testFormFields += ",DataUpdateTime";
                    testForm.DataUpdateTime = DateTime.Now;
                    baseResultDataValue = IBLisTestForm.LisTestFormEditByField(testForm, testFormFields, patientFields);
                    if (baseResultDataValue.success)
                    {
                        baseResultDataValue = IBLisTestForm.EditBatchLisTestItemResult(testForm.Id, listTestItemResult, testItemFileds);
                        if (baseResultDataValue.success)
                        {
                            IBLisTestForm.EditLisTestFormTestTime(testForm.Id);                         
                        }
                        IBLisTestForm.EditLisTestItemAfterTreatment(testForm.Id);
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：fields参数不能为空！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：检验单实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 批量新增检验单
        /// </summary>
        /// <param name="sampleInfo">样本信息json串</param>
        /// <param name="testDate">检验日期</param>
        /// <param name="sectionID">小组ID</param>
        /// <param name="startSampleNo">开始样本号</param>
        /// <param name="sampleCount">样本数量</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_AddBatchLisTestForm(string sampleInfo, string testDate, long sectionID, string startSampleNo, int sampleCount)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if ((!string.IsNullOrEmpty(sampleInfo)) && (!string.IsNullOrEmpty(testDate)))
            {
                ZhiFang.LabStar.Common.SysDelegateVar.SendSysMsgDelegateVar = SendSysMessage.SendSysMessageDelegate;
                try
                {
                    IBLisTestForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                    baseResultDataValue = IBLisTestForm.AddBatchLisTestForm(sampleInfo, testDate, sectionID, startSampleNo, sampleCount);
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
                baseResultDataValue.ErrorInfo = "错误信息：样本信息或检验日期参数不能为空！";
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 批量更新检验单
        /// </summary>
        /// <param name="entityList">要更新的检验单实体列表</param>
        /// <param name="fields">更新的字段列表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_UpdateBatchLisTestForm(IList<LisTestForm> entityList, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (entityList != null && entityList.Count > 0)
            {
                IList<string[]> listArray = new List<string[]>();
                if ((fields != null) && (fields.Length > 0))
                {
                    try
                    {
                        fields += ",DataUpdateTime";
                        foreach (LisTestForm entity in entityList)
                        {
                            entity.DataUpdateTime = DateTime.Now;
                            string[] tempArray = CommonServiceMethod.GetUpdateFieldValueStr(entity, fields);
                            listArray.Add(tempArray);
                        }
                        baseResultDataValue = IBLisTestForm.UpdateBatchLisTestForm(listArray);
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
                    baseResultDataValue.ErrorInfo = "错误信息：fields参数不能为空！";
                }
            }
            else
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_EditBatchLisTestItemResult(long testFormID, IList<LisTestItem> listTestItemResult)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (listTestItemResult != null && listTestItemResult.Count > 0)
            {
                ZhiFang.LabStar.Common.SysDelegateVar.SendSysMsgDelegateVar = SendSysMessage.SendSysMessageDelegate;
                try
                {
                    baseResultDataValue = IBLisTestForm.EditBatchLisTestItemResult(testFormID, listTestItemResult, "");
                    if (baseResultDataValue.success)
                    {
                        IBLisTestForm.EditLisTestFormTestTime(testFormID);
                        IBLisTestForm.EditLisTestItemAfterTreatment(testFormID);
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

        /// <summary>
        /// 批量删除检验单
        /// </summary>
        /// <param name="delIDList">检验单ID列表字符串</param>
        /// <param name="isReceiveDelete">核收的检验单是否删除</param>
        /// <param name="isResultDelete">存在项目结果的检验单是否删除</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_DeleteBatchLisTestForm(string delIDList, int receiveDeleteFlag, int resultDeleteFlag)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (delIDList != null && delIDList.Trim().Length > 0)
            {
                try
                {
                    bool isReceiveDelete = (receiveDeleteFlag == 1);
                    bool isResultDelete = (resultDeleteFlag == 1);
                    baseResultDataValue = IBLisTestForm.DeleteBatchLisTestForm(delIDList, isReceiveDelete, isResultDelete);
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


        /// <summary>
        /// 查询样本单是否可以删除
        /// </summary>
        /// <param name="delIDList">要删除的检验单实体ID列表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_QueryLisTestFormIsDelete(string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (delIDList != null && delIDList.Trim().Length > 0)
            {
                try
                {
                    baseResultDataValue = IBLisTestForm.QueryLisTestFormIsDelete(delIDList);
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

        /// <summary>
        /// 批量新增检验单项目
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="listAddTestItem">检验单项目实体列表</param>
        /// <param name="isRepPItem">是否替换组合项目</param>
        /// <param name="testItemFileds">对项目赋值的字段列表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_AddBatchLisTestItem(string testFormID, IList<LisTestItem> listAddTestItem, string testItemFileds, bool isRepPItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (testFormID != null && testFormID.Trim().Length > 0)
                {
                    string[] arrayFormID = testFormID.Split(',');
                    foreach (string formID in arrayFormID)
                    {
                        if (string.IsNullOrWhiteSpace(formID))
                            continue;
                        baseResultDataValue = IBLisTestForm.AddBatchLisTestItem(long.Parse(formID), listAddTestItem, testItemFileds, isRepPItem);
                        if (baseResultDataValue.success)
                            IBLisTestForm.GetLisTestFormCalcItem(long.Parse(formID));
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

        /// <summary>
        /// 批量新增检验单项目结果
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="listAddTestItem">检验单项目实体列表</param>
        /// <param name="isAddItem">是否新增项目</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_AddBatchLisTestItemResult(string testFormID, IList<LisTestItem> listAddTestItem, bool isAddItem, bool isSingleItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.LabStar.Common.SysDelegateVar.SendSysMsgDelegateVar = SendSysMessage.SendSysMessageDelegate;
                if (isSingleItem)
                {
                    baseResultDataValue = IBLisTestForm.AddBatchLisTestItemResult(listAddTestItem, isAddItem);
                }
                else
                {
                    if (testFormID != null && testFormID.Trim().Length > 0)
                    {
                        string[] arrayFormID = testFormID.Split(',');
                        foreach (string formID in arrayFormID)
                            baseResultDataValue = IBLisTestForm.AddBatchLisTestItemResult(long.Parse(formID), listAddTestItem, isAddItem);
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


        /// <summary>
        /// 批量检验单项目结果偏移
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="listAddTestItem">检验单项目实体列表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestItemResultOffset(string testItemInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ZhiFang.LabStar.Common.SysDelegateVar.SendSysMsgDelegateVar = SendSysMessage.SendSysMessageDelegate;
            try
            {
                if (testItemInfo != null && testItemInfo.Trim().Length > 0)
                {
                    baseResultDataValue = IBLisTestItem.EditLisTestItemResultByOffset(testItemInfo);
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

        /// <summary>
        /// 样本结果稀释处理
        /// </summary>
        /// <param name="testItemIDList">样本项目单ID列表</param>
        /// <param name="dilutionTimes">稀释倍数</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestItemResultDilution(string testItemIDList, double? dilutionTimes)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            ZhiFang.LabStar.Common.SysDelegateVar.SendSysMsgDelegateVar = SendSysMessage.SendSysMessageDelegate;
            try
            {
                baseResultDataValue = IBLisTestItem.EditLisTestItemResultByDilution(testItemIDList, dilutionTimes);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 批量删除检验单项目
        /// </summary>
        /// <param name="testFormID">要删除的检验单ID</param>
        /// <param name="delIDList">要删除的样本项目实体ID列表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_DeleteBatchLisTestItem(long testFormID, string delIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            if (delIDList != null && delIDList.Trim().Length > 0)
            {
                try
                {
                    baseResultDataValue = IBLisTestForm.DeleteBatchLisTestItem(testFormID, delIDList);
                    if (baseResultDataValue.success)
                    {
                        IBLisTestForm.DeleteLisTestFormCalcItem(testFormID);
                        IBLisTestForm.EditLisTestFormTestTimeByDelTestItem(testFormID);
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

        /// <summary>
        /// 批量删除检验单项目
        /// </summary>
        /// <param name="testFormIDList">检验单ID字符串列表</param>
        /// <param name="itemIDList">检验项目ID字符串列表</param>
        /// <param name="isDelNoResultItem">仅删除结果为空的项目</param>
        /// <param name="isDelNoOrderItem">仅删除非医嘱项目</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_DeleteBatchLisTestItemByTestFormIDList(string testFormIDList, string itemIDList, bool isDelNoResultItem, bool isDelNoOrderItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                if (testFormIDList != null && testFormIDList.Trim().Length > 0 && itemIDList != null && itemIDList.Trim().Length > 0)
                {
                    string[] listTestFormID = testFormIDList.Split(',');
                    foreach (string formID in listTestFormID)
                    {
                        long testFormID = long.Parse(formID);
                        baseResultDataValue = IBLisTestForm.DeleteBatchLisTestItem(testFormID, itemIDList, isDelNoResultItem, isDelNoOrderItem);
                        if (baseResultDataValue.success)
                        {
                            IBLisTestForm.EditLisTestFormTestTimeByDelTestItem(testFormID);
                            IBLisTestForm.EditLisTestItemAfterTreatment(testFormID);
                        }
                    }
                }
                else
                {
                    baseResultDataValue.success = false;
                    baseResultDataValue.ErrorInfo = "错误信息：实体参数不能为空！";
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

        /// <summary>
        /// 检验单计算项目处理
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestFormCalcItemDisposeByID(long testFormID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisTestForm.GetLisTestFormCalcItem(testFormID);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_QueryLisTestFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestForm> entityList = new EntityList<LisTestForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisTestForm.QueryLisTestForm(where, CommonServiceMethod.GetSortHQL(sort), page, limit, fields);
                }
                else
                {
                    entityList = IBLisTestForm.QueryLisTestForm(where, "", page, limit, fields);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryLisTestFormBySampleNo(string beginSampleNo, string endSampleNo, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestForm> entityList = new EntityList<LisTestForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisTestForm.QueryLisTestFormBySampleNo(beginSampleNo, endSampleNo, where, CommonServiceMethod.GetSortHQL(sort), page, limit, fields);
                }
                else
                {
                    entityList = IBLisTestForm.QueryLisTestFormBySampleNo(beginSampleNo, endSampleNo, where, "", page, limit, fields);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryLisTestFormAndItemNameList(string beginSampleNo, string endSampleNo, int page, int limit, string fields, string where, string sort, bool isPlanish, int isOrderItem, int itemNameType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestForm> entityList = new EntityList<LisTestForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisTestForm.QueryLisTestFormAndItemNameList(beginSampleNo, endSampleNo, where, CommonServiceMethod.GetSortHQL(sort), page, limit, fields, isOrderItem, itemNameType);
                }
                else
                {
                    entityList = IBLisTestForm.QueryLisTestFormAndItemNameList(beginSampleNo, endSampleNo, where, "", page, limit, fields, isOrderItem, itemNameType);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryWillConfirmLisTestForm(string beginSampleNo, string endSampleNo, string itemResultFlag, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestForm> entityList = new EntityList<LisTestForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisTestForm.QueryWillConfirmLisTestForm(beginSampleNo, endSampleNo, itemResultFlag, where, CommonServiceMethod.GetSortHQL(sort), page, limit, fields);
                }
                else
                {
                    entityList = IBLisTestForm.QueryWillConfirmLisTestForm(beginSampleNo, endSampleNo, itemResultFlag, where, "", page, limit, fields);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryLisTestItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestItem> entityList = new EntityList<LisTestItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisTestItem.QueryLisTestItem(where, CommonServiceMethod.GetSortHQL(sort), page, limit, fields);
                }
                else
                {
                    entityList = IBLisTestItem.QueryLisTestItem(where, "", page, limit, fields);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryLisTestItemBySampleNo(string beginSampleNo, string endSampleNo, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestItem> entityList = new EntityList<LisTestItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisTestItem.QueryLisTestItem(beginSampleNo, endSampleNo, where, CommonServiceMethod.GetSortHQL(sort), page, limit, fields);
                }
                else
                {
                    entityList = IBLisTestItem.QueryLisTestItem(beginSampleNo, endSampleNo, where, "", page, limit, fields);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryCommonItemByTestFormID(string testFormIDList, string fields)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LBItem> entityList = new EntityList<LBItem>();
            try
            {
                if (testFormIDList != null && testFormIDList.Trim().Length > 0)
                {
                    string itemIDList = IBLisTestItem.QueryCommonItemByTestFormID(testFormIDList);
                    if (itemIDList != null && itemIDList.Trim().Length > 0)
                        entityList = IBLBItem.SearchListByHQL(" lbitem.Id in (" + itemIDList + ")", 0, 0);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBItem>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryLisTestFormCurPageByHQL(long id, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityPageList<LisTestForm> entityList = new EntityPageList<LisTestForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisTestForm.QueryLisTestFormCurPage(id, where, CommonServiceMethod.GetSortHQL(sort), page, limit, fields);
                }
                else
                {
                    entityList = IBLisTestForm.QueryLisTestFormCurPage(id, where, "", page, limit, fields);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryLisTestFormCurPageBySampleNo(int page, int limit, string fields, string where, string oldWhere, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityPageList<LisTestForm> entityList = new EntityPageList<LisTestForm>();
            try
            {
                if (!string.IsNullOrWhiteSpace(where))
                {
                    IList<LisTestForm> tempList = IBLisTestForm.SearchListByHQL(where);
                    if (tempList != null && tempList.Count > 0)
                    {
                        long id = tempList[0].Id;
                        if ((sort != null) && (sort.Length > 0))
                        {
                            entityList = IBLisTestForm.QueryLisTestFormCurPage(id, oldWhere, CommonServiceMethod.GetSortHQL(sort), page, limit, fields);
                        }
                        else
                        {
                            entityList = IBLisTestForm.QueryLisTestFormCurPage(id, oldWhere, "", page, limit, fields);
                        }
                        ParseObjectProperty pop = new ParseObjectProperty(fields);
                        try
                        {
                            if (isPlanish)
                            {
                                baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestForm>(entityList);
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
        /// 检验样本合并
        /// </summary>
        /// <param name="fromTestFormID">检验源样本ID</param>
        /// <param name="toTestForm">检验目标样本</param>
        /// <param name="strFromTestItemID">检验项目信息ID（注意不是项目ID，是LisTestItem的ID）</param>
        /// <param name="mergeType">合并类型（1 只合并样本信息，2 只合并样本项目信息，3 合并样本和样本项目信息）</param>
        /// <param name="isSampleNoMerge">是否合并样本号相关信息</param>
        /// <param name="isSerialNoMerge">是否合并条码相关信息</param>
        /// <param name="isDelFormTestItem">合并后是否删除源样本项目</param>
        /// <param name="isDelFormTestForm">合并后源样本下项目为空时，是否删除源样本</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestFormInfoMerge(long fromTestFormID, LisTestForm toTestForm, string strFromTestItemID, int mergeType, bool isSampleNoMerge, bool isSerialNoMerge, bool isDelFormTestItem, bool isDelFormTestForm)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisTestForm.EditLisTestFormInfoMerge(fromTestFormID, toTestForm, strFromTestItemID, mergeType, isSampleNoMerge, isSerialNoMerge, isDelFormTestItem, isDelFormTestForm);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 查询项目合并的样本信息列表
        /// </summary>
        /// <param name="itemID">项目ID</param>
        /// <param name="beginDate">检测开始日期</param>
        /// <param name="endDate">检测结束日期</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_QueryItemMergeFormInfo(long itemID, string beginDate, string endDate, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    EntityList<LBMergeItemFormVO> entityList = IBLisTestForm.QueryItemMergeFormInfo(itemID, beginDate, endDate);
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBMergeItemFormVO>(entityList);
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
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 查询项目合并的样本的项目信息列表
        /// </summary>
        /// <param name="itemID">项目ID</param>
        /// <param name="patNo">病历号</param>
        /// <param name="cName">病人姓名</param>
        /// <param name="beginDate">检测开始日期</param>
        /// <param name="endDate">检测结束日期</param>
        /// <param name="isMerge">样本是否已经合并（已合并为1，否则为空字符串）</param>
        /// <param name="fields">要获取对象的属性列表</param>
        /// <param name="isPlanish">对象是否压平</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_QueryItemMergeInfo(long itemID, string patNo, string cName, string beginDate, string endDate, string isMerge, string fields, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    EntityList<LBMergeItemVO> entityList = IBLisTestForm.QueryItemMergeItemInfo(itemID, patNo, cName, beginDate, endDate, isMerge);
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LBMergeItemVO>(entityList);
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
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 样本项目合并(预留服务)
        /// </summary>
        /// <param name="toFormID">要合并的目标样本ID</param>
        /// <param name="strLisTestItemID">要合并的样本项目（格式：ItemID1,ItemID2...）</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestItemMerge(long toFormID, string strLisTestItemID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisTestForm.EditMergeItemInfo(toFormID, strLisTestItemID);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 样本项目合并
        /// </summary>
        /// <param name="listLBMergeItemVO">要合并的样本项目列表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestItemMergeByVOEntity(IList<LBMergeItemVO> listLBMergeItemVO)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisTestForm.EditMergeItemInfo(listLBMergeItemVO);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_QueryLisTestItemMergeImageData(long toFormID, IList<LisTestItem> listLisTestItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                LisTestForm lisTestForm = IBLisTestForm.Get(toFormID);
                if (lisTestForm == null || listLisTestItem == null)
                    return baseResultDataValue;
                IList<Line> listLine = IBLBItemRange.QueryItemRangeChartLinePoint(lisTestForm, listLisTestItem, null);
                baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(listLine);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public Message LS_UDTO_AddLisTestItemMergeGraph()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string testFormID = "";
                string graphInfo = "";
                string graphBase64 = "";
                string graphThumb = "";
                string graphName = "";
                string graphType = "";
                string graphHeight = "";
                string graphWidth = "";
                string graphComment = "";
                string graphNo = "";
                string iExamine = "";
                string dispOrder = "";
                string isReport = "";

                string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
                for (int i = 0; i < allkeys.Length; i++)
                {
                    switch (allkeys[i])
                    {
                        case "testFormID"://检验单ID（不能为空）
                            if (HttpContext.Current.Request.Form["testFormID"].Trim() != "")
                                testFormID = HttpContext.Current.Request.Form["testFormID"].Trim();
                            break;
                        case "graphInfo"://图片信息
                            if (HttpContext.Current.Request.Form["graphInfo"].Trim() != "")
                                graphInfo = HttpContext.Current.Request.Form["graphInfo"].Trim();
                            break;
                        case "graphBase64"://图片Base64编码字符串
                            if (HttpContext.Current.Request.Form["graphBase64"].Trim() != "")
                                graphBase64 = HttpContext.Current.Request.Form["graphBase64"].Trim();
                            break;
                        case "graphThumb"://图片缩略图Base64编码
                            if (HttpContext.Current.Request.Form["graphThumb"].Trim() != "")
                                graphThumb = HttpContext.Current.Request.Form["graphThumb"].Trim();
                            break;
                        case "graphName"://图片名称（不能为空）
                            if (HttpContext.Current.Request.Form["graphName"].Trim() != "")
                                graphName = HttpContext.Current.Request.Form["graphName"].Trim();
                            break;
                        case "graphType"://图片扩展名（jpg，gif，png等）
                            if (HttpContext.Current.Request.Form["graphType"].Trim() != "")
                                graphType = HttpContext.Current.Request.Form["graphType"].Trim();
                            break;
                        case "graphHeight"://图片高度
                            if (HttpContext.Current.Request.Form["graphHeight"].Trim() != "")
                                graphHeight = HttpContext.Current.Request.Form["graphHeight"].Trim();
                            break;
                        case "graphWidth"://图片宽度
                            if (HttpContext.Current.Request.Form["graphWidth"].Trim() != "")
                                graphWidth = HttpContext.Current.Request.Form["graphWidth"].Trim();
                            break;
                        case "graphNo"://图片编号
                            if (HttpContext.Current.Request.Form["graphNo"].Trim() != "")
                                graphNo = HttpContext.Current.Request.Form["graphNo"].Trim();
                            break;
                        case "graphComment"://图片备注
                            if (HttpContext.Current.Request.Form["graphComment"].Trim() != "")
                                graphComment = HttpContext.Current.Request.Form["graphComment"].Trim();
                            break;
                        case "isReport":///是否报告
                            if (HttpContext.Current.Request.Form["isReport"].Trim() != "")
                                isReport = HttpContext.Current.Request.Form["isReport"].Trim();
                            break;
                        case "iExamine"://检查次数
                            if (HttpContext.Current.Request.Form["iExamine"].Trim() != "")
                                iExamine = HttpContext.Current.Request.Form["iExamine"].Trim();
                            break;
                        case "dispOrder"://排序
                            if (HttpContext.Current.Request.Form["dispOrder"].Trim() != "")
                                dispOrder = HttpContext.Current.Request.Form["dispOrder"].Trim();
                            break;
                    }
                }
                if (testFormID != null && testFormID.Trim().Length > 0)
                {
                    LisTestForm lisTestForm = IBLisTestForm.Get(long.Parse(testFormID));
                    if (lisTestForm != null)
                    {
                        BaseResultDataValue isAddAndEdit = null;
                        if (graphInfo == null || graphInfo.Trim().Length == 0)
                        {
                            IList<LisTestItem> listLisTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=" + testFormID);
                            IList<Line> listLine = IBLBItemRange.QueryItemRangeChartLinePoint(lisTestForm, listLisTestItem, null);
                            graphInfo = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(listLine);
                        }
                        LisTestGraph entityImage = null;
                        IList<LisTestGraph> listTestGragh = IBLisTestGraph.SearchListByHQL(" listestgraph.LisTestForm.Id=" + testFormID +
                            " and listestgraph.GTestDate=\'" + ((DateTime)lisTestForm.GTestDate).ToString("yyyy-MM-dd") + "\'" +
                            " and listestgraph.GraphName=\'" + graphName + "\'");
                        bool isEdit = (listTestGragh != null && listTestGragh.Count > 0);
                        if (isEdit)
                        {
                            entityImage = listTestGragh[0];
                            isAddAndEdit = IBLisTestGraph.AppendLisTestGraphToDataBase(entityImage.GraphDataID, entityImage.LabID, graphBase64, graphThumb);
                        }
                        else
                        {
                            entityImage = new LisTestGraph();
                            entityImage.LisTestForm = lisTestForm;
                            isAddAndEdit = IBLisTestGraph.AppendLisTestGraphToDataBase(null, lisTestForm.LabID, graphBase64, graphThumb);
                            if (isAddAndEdit.success)
                                entityImage.GraphDataID = long.Parse(isAddAndEdit.ResultDataValue);
                        }
                        entityImage.GTestDate = lisTestForm.GTestDate;
                        entityImage.MainStatusID = lisTestForm.MainStatusID;
                        entityImage.StatusID = lisTestForm.StatusID;
                        //entityImage.GraphData = bytes;
                        entityImage.GraphInfo = graphInfo;
                        entityImage.GraphName = graphName;
                        entityImage.GraphType = graphType;
                        if (graphHeight != null && graphHeight.Trim().Length > 0)
                            entityImage.GraphHeight = int.Parse(graphHeight);
                        if (graphWidth != null && graphWidth.Trim().Length > 0)
                            entityImage.GraphWidth = int.Parse(graphWidth);
                        entityImage.GraphComment = graphComment;
                        if (graphHeight != null && graphHeight.Trim().Length > 0)
                            entityImage.GraphHeight = int.Parse(graphHeight);
                        if (graphWidth != null && graphWidth.Trim().Length > 0)
                            entityImage.GraphWidth = int.Parse(graphWidth);
                        if (graphNo != null && graphNo.Trim().Length > 0)
                            entityImage.GraphNo = int.Parse(graphNo);
                        if (iExamine != null && iExamine.Trim().Length > 0)
                            entityImage.IExamine = int.Parse(iExamine);
                        if (dispOrder != null && dispOrder.Trim().Length > 0)
                            entityImage.DispOrder = int.Parse(dispOrder);
                        else
                            entityImage.DispOrder = int.Parse(IBLisCommon.GetMaxNoByFieldName("LisTestGraph", "DispOrder"));
                        if (isReport != null && isReport.Trim().Length > 0)
                            entityImage.IsReport = (isReport == "1");
                        else
                            entityImage.IsReport = true;
                        IBLisTestGraph.Entity = entityImage;
                        if (isAddAndEdit.success)
                        {
                            if (isEdit)
                                IBLisTestGraph.Edit();
                            else
                                IBLisTestGraph.Add();
                        }
                        else
                        {
                            brdv = isAddAndEdit;
                            ZhiFang.LabStar.Common.LogHelp.Error("LS_UDTO_AddLisTestItemMergeImge图形保存更新失败！：" + brdv.ErrorInfo);
                        }
                    }
                }
                #region
                //int iTotal = HttpContext.Current.Request.Files.Count;
                //if (iTotal > 0)
                //{
                //    HttpPostedFile file = HttpContext.Current.Request.Files[0];
                //    if (file.ContentLength > 0)
                //    {
                //        Stream fs = file.InputStream;
                //        byte[] bytes = new byte[fs.Length];
                //        try
                //        {
                //            fs.Read(bytes, 0, (int)fs.Length);
                //        }
                //        finally
                //        {
                //            fs.Close();
                //            fs = null;
                //        }
                //    }
                //    else
                //    {
                //        brdv.ErrorInfo = "图形文件无效！";
                //        brdv.success = false;
                //    }
                //}
                //else
                //{
                //    brdv.ErrorInfo = "无法获取图形文件！";
                //    brdv.success = false;
                //}
                #endregion
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "保存图形时发生错误，原因为：<br>" + ex.Message;
                brdv.success = false;
                ZhiFang.LabStar.Common.LogHelp.Error("LS_UDTO_AddLisTestItemMergeImge异常：" + ex.ToString());
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        /// <summary>
        /// 检验结果图形表数据保存
        /// </summary>
        /// <returns></returns>
        public Message LS_UDTO_AddLisTestGraphData()
        {
            BaseResultDataValue brdv = new BaseResultDataValue();
            try
            {
                string testGraphID = "";
                string testFormID = "";
                string graphInfo = "";
                string graphBase64 = "";
                string graphThumb = "";
                string graphName = "";
                string graphType = "";
                string graphHeight = "";
                string graphWidth = "";
                string graphComment = "";
                string graphNo = "";
                string iExamine = "";
                string dispOrder = "";
                string isReport = "";

                string[] allkeys = HttpContext.Current.Request.Form.AllKeys;
                for (int i = 0; i < allkeys.Length; i++)
                {
                    switch (allkeys[i])
                    {
                        case "testGraphID"://检验单图形ID
                            if (HttpContext.Current.Request.Form["testGraphID"].Trim() != "")
                                testGraphID = HttpContext.Current.Request.Form["testGraphID"].Trim();
                            break;
                        case "testFormID"://检验单ID（不能为空）
                            if (HttpContext.Current.Request.Form["testFormID"].Trim() != "")
                                testFormID = HttpContext.Current.Request.Form["testFormID"].Trim();
                            break;
                        case "graphInfo"://图片信息
                            if (HttpContext.Current.Request.Form["graphInfo"].Trim() != "")
                                graphInfo = HttpContext.Current.Request.Form["graphInfo"].Trim();
                            break;
                        case "graphBase64"://图片Base64编码字符串
                            if (HttpContext.Current.Request.Form["graphBase64"].Trim() != "")
                                graphBase64 = HttpContext.Current.Request.Form["graphBase64"].Trim();
                            break;
                        case "graphThumb"://图片缩略图Base64编码
                            if (HttpContext.Current.Request.Form["graphThumb"].Trim() != "")
                                graphThumb = HttpContext.Current.Request.Form["graphThumb"].Trim();
                            break;
                        case "graphName"://图片名称（不能为空）
                            if (HttpContext.Current.Request.Form["graphName"].Trim() != "")
                                graphName = HttpContext.Current.Request.Form["graphName"].Trim();
                            break;
                        case "graphType"://图片扩展名（jpg，gif，png等）
                            if (HttpContext.Current.Request.Form["graphType"].Trim() != "")
                                graphType = HttpContext.Current.Request.Form["graphType"].Trim();
                            break;
                        case "graphHeight"://图片高度
                            if (HttpContext.Current.Request.Form["graphHeight"].Trim() != "")
                                graphHeight = HttpContext.Current.Request.Form["graphHeight"].Trim();
                            break;
                        case "graphWidth"://图片宽度
                            if (HttpContext.Current.Request.Form["graphWidth"].Trim() != "")
                                graphWidth = HttpContext.Current.Request.Form["graphWidth"].Trim();
                            break;
                        case "graphNo"://图片编号
                            if (HttpContext.Current.Request.Form["graphNo"].Trim() != "")
                                graphNo = HttpContext.Current.Request.Form["graphNo"].Trim();
                            break;
                        case "graphComment"://图片备注
                            if (HttpContext.Current.Request.Form["graphComment"].Trim() != "")
                                graphComment = HttpContext.Current.Request.Form["graphComment"].Trim();
                            break;
                        case "isReport":///是否报告
                            if (HttpContext.Current.Request.Form["isReport"].Trim() != "")
                                isReport = HttpContext.Current.Request.Form["isReport"].Trim();
                            break;
                        case "iExamine"://检查次数
                            if (HttpContext.Current.Request.Form["iExamine"].Trim() != "")
                                iExamine = HttpContext.Current.Request.Form["iExamine"].Trim();
                            break;
                        case "dispOrder"://排序
                            if (HttpContext.Current.Request.Form["dispOrder"].Trim() != "")
                                dispOrder = HttpContext.Current.Request.Form["dispOrder"].Trim();
                            break;
                    }
                }
                if (testFormID != null && testFormID.Trim().Length > 0)
                {
                    LisTestForm lisTestForm = IBLisTestForm.Get(long.Parse(testFormID));
                    if (lisTestForm != null)
                    {
                        BaseResultDataValue isAddAndEdit = null;

                        LisTestGraph entityImage = null;
                        IList<LisTestGraph> listTestGragh = null;
                        if (testGraphID != null && testGraphID.Trim().Length > 0)
                            listTestGragh = IBLisTestGraph.SearchListByHQL(" listestgraph.Id=" + testGraphID);
                        else
                            listTestGragh = IBLisTestGraph.SearchListByHQL(" listestgraph.LisTestForm.Id=" + testFormID +
                            " and listestgraph.GTestDate=\'" + ((DateTime)lisTestForm.GTestDate).ToString("yyyy-MM-dd") + "\'" +
                            " and listestgraph.GraphName=\'" + graphName + "\'");
                        bool isEdit = (listTestGragh != null && listTestGragh.Count > 0);
                        if (isEdit)
                        {
                            entityImage = listTestGragh[0];
                            isAddAndEdit = IBLisTestGraph.AppendLisTestGraphToDataBase(entityImage.GraphDataID, entityImage.LabID, graphBase64, graphThumb);
                        }
                        else
                        {
                            entityImage = new LisTestGraph();
                            entityImage.LisTestForm = lisTestForm;
                            isAddAndEdit = IBLisTestGraph.AppendLisTestGraphToDataBase(null, lisTestForm.LabID, graphBase64, graphThumb);
                            if (isAddAndEdit.success)
                                entityImage.GraphDataID = long.Parse(isAddAndEdit.ResultDataValue);
                        }
                        entityImage.GTestDate = lisTestForm.GTestDate;
                        entityImage.MainStatusID = lisTestForm.MainStatusID;
                        entityImage.StatusID = lisTestForm.StatusID;
                        //entityImage.GraphData = bytes;
                        entityImage.GraphInfo = graphInfo;
                        entityImage.GraphName = graphName;
                        entityImage.GraphType = graphType;
                        entityImage.GraphComment = graphComment;
                        if (graphHeight != null && graphHeight.Trim().Length > 0)
                            entityImage.GraphHeight = int.Parse(graphHeight);
                        if (graphWidth != null && graphWidth.Trim().Length > 0)
                            entityImage.GraphWidth = int.Parse(graphWidth);
                        if (graphNo != null && graphNo.Trim().Length > 0)
                            entityImage.GraphNo = int.Parse(graphNo);
                        if (iExamine != null && iExamine.Trim().Length > 0)
                            entityImage.IExamine = int.Parse(iExamine);
                        if (dispOrder != null && dispOrder.Trim().Length > 0)
                            entityImage.DispOrder = int.Parse(dispOrder);
                        else
                            entityImage.DispOrder = int.Parse(IBLisCommon.GetMaxNoByFieldName("LisTestGraph", "DispOrder"));
                        if (isReport != null && isReport.Trim().Length > 0)
                            entityImage.IsReport = (isReport == "1");
                        else
                            entityImage.IsReport = true;
                        IBLisTestGraph.Entity = entityImage;
                        if (isAddAndEdit.success)
                        {
                            if (isEdit)
                                IBLisTestGraph.Edit();
                            else
                                IBLisTestGraph.Add();
                        }
                        else
                        {
                            brdv = isAddAndEdit;
                            ZhiFang.LabStar.Common.LogHelp.Error("LS_UDTO_AddLisTestGraphData图形保存更新失败！：" + brdv.ErrorInfo);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                brdv.ErrorInfo = "保存图形时发生错误，原因为：<br>" + ex.Message;
                brdv.success = false;
                ZhiFang.LabStar.Common.LogHelp.Error("LS_UDTO_AddLisTestGraphData异常：" + ex.ToString());
            }
            string strResult = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(brdv);
            return WebOperationContext.Current.CreateTextResponse(strResult, "text/plain", Encoding.UTF8);
        }

        /// <summary>
        /// 获取LIS图形库图形结果表数据
        /// </summary>
        /// <param name="graphDataID">图形数据ID</param>
        /// <param name="graphSizeType">图形大小类型：1为大图，其他数值为小图</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_QueryLisGraphData(long graphDataID, int graphSizeType)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string strSQL = "select * from Lis_GraphData where GraphDataID=" + graphDataID;
                DataSet ds = ZhiFang.LabStar.DAO.ADO.SqlServerHelper.QuerySql(strSQL, ZhiFang.LabStar.DAO.ADO.SqlServerHelper.LabStarGraphConnectStr);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows != null && ds.Tables[0].Rows.Count > 0)
                {
                    string graphFieldName = "ThumbData";
                    if (graphSizeType == 1)
                        graphFieldName = "GraphData";
                    baseResultDataValue.ResultDataValue = ds.Tables[0].Rows[0][graphFieldName] != null ? ds.Tables[0].Rows[0][graphFieldName].ToString() : "";
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "查询错误：" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        public BaseResultBool LS_UDTO_DelLisTestGraphData(long id)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                IBLisTestGraph.Entity = IBLisTestGraph.Get(id);
                if (IBLisTestGraph.Entity != null)
                {
                    long labid = IBLisTestGraph.Entity.LabID;
                    string entityName = IBLisTestGraph.Entity.GetType().Name;
                    baseResultBool.success = IBLisTestGraph.RemoveByHQL(id);
                    if (baseResultBool.success && IBLisTestGraph.Entity.GraphDataID != null)
                    {
                        string strSQL = "delete from Lis_GraphData where GraphDataID=" + IBLisTestGraph.Entity.GraphDataID;
                        ZhiFang.LabStar.DAO.ADO.BaseResult baseResult = ZhiFang.LabStar.DAO.ADO.SqlServerHelper.ExecuteSql(strSQL, ZhiFang.LabStar.DAO.ADO.SqlServerHelper.LabStarGraphConnectStr);
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

        /// <summary>
        /// 提取仪器结果
        /// </summary>
        /// <param name="testFormID">检验检验单ID</param>
        /// <param name="equipFormID">仪器检验单ID</param>
        /// <param name="equipItemID">要提取仪器项目ID串，为空提取全部仪器项目结果</param>
        /// <param name="changeSampleNo">是否改变检验检验单样本号</param>
        /// <param name="changeTestFormID">是否改变仪器检验单对应的检验检验单</param>
        /// <param name="isDelAuotAddItem">是否删除检验单中仪器自增项目</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_AddLisItemResultByEquipResult(long testFormID, long equipFormID, string equipItemID, bool isChangeSampleNo, bool isChangeTestFormID, bool isDelAuotAddItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.LabStar.Common.SysDelegateVar.SendSysMsgDelegateVar = SendSysMessage.SendSysMessageDelegate;
                string empID = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
                string empName = Cookie.CookieHelper.Read(DicCookieSession.EmployeeName);
                long? longEmpID = null;
                if (empID != null && empID.Trim().Length > 0)
                    longEmpID = long.Parse(empID);
                baseResultDataValue = IBLisEquipForm.AddLisItemResultByEquipResult(testFormID, equipFormID, equipItemID, isChangeSampleNo, isChangeTestFormID, isDelAuotAddItem, longEmpID, empName, "", null);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 采用仪器结果
        /// </summary>
        /// <param name="testFormID">检验检验单ID</param>
        /// <param name="equipFormID">仪器检验单ID</param>
        /// <param name="equipItemID">要提取仪器项目ID串，为空提取全部仪器项目结果</param>
        /// <param name="reCheckMemoInfo">复检备注信息</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_AddItemReCheckResultByEquipResult(long testFormID, long equipFormID, string equipItemID, string reCheckMemoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.LabStar.Common.SysDelegateVar.SendSysMsgDelegateVar = SendSysMessage.SendSysMessageDelegate;
                string empID = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
                string empName = Cookie.CookieHelper.Read(DicCookieSession.EmployeeName);
                long? longEmpID = null;
                if (empID != null && empID.Trim().Length > 0)
                    longEmpID = long.Parse(empID);
                baseResultDataValue = IBLisEquipForm.AddLisItemResultByEquipResult(testFormID, equipFormID, equipItemID, false, false, false, longEmpID, empName, reCheckMemoInfo, null);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }


        /// <summary>
        /// 批量提取仪器结果
        /// </summary>
        /// <param name="testFormIDList">检验检验单ID字符串列表</param>
        /// <param name="equipFormIDList">仪器检验单ID字符串列表</param>
        /// <param name="isChangeSampleNo">是否改变检验检验单样本号</param>
        /// <param name="isDelAuotAddItem">是否删除检验单中仪器自增项目</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_BatchExtractEquipResult(string testFormIDList, string equipFormIDList, bool isChangeSampleNo, bool isDelAuotAddItem)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                ZhiFang.LabStar.Common.SysDelegateVar.SendSysMsgDelegateVar = SendSysMessage.SendSysMessageDelegate;
                string empID = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
                string empName = Cookie.CookieHelper.Read(DicCookieSession.EmployeeName);
                long? longEmpID = null;
                if (empID != null && empID.Trim().Length > 0)
                    longEmpID = long.Parse(empID);
                baseResultDataValue = IBLisEquipForm.AddBatchExtractEquipResult(testFormIDList, equipFormIDList, isChangeSampleNo, isDelAuotAddItem, longEmpID, empName);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        public BaseResultDataValue LS_UDTO_QueryLisTestItemMergeImgeTest()
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                //LisTestForm lisTestForm = IBLisTestForm.Get(100101);
                //IList<LisTestItem> listLisTestItem = IBLisTestItem.SearchListByHQL(" listestitem.LisTestForm.Id=100101");
                //IList<Line> listLine = IBLBItemRange.QueryItemRangeChartLinePoint(lisTestForm, listLisTestItem, null);
                //baseResultDataValue.ResultDataValue = ZhiFang.Common.Public.JsonSerializer.JsonDotNetSerializer(listLine);
                LisTestGraph entityImage = new LisTestGraph();
                string pathName = @"d:\A1123.jpg";

                System.IO.FileStream fs = new System.IO.FileStream(pathName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                byte[] bytes = new byte[fs.Length];
                try
                {
                    fs.Read(bytes, 0, (int)fs.Length);
                }
                finally
                {
                    fs.Close();
                    fs = null;
                }
                //entityImage.GraphData = bytes;
                IBLisTestGraph.Entity = entityImage;
                IBLisTestGraph.Add();
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 检验单智能审核判定服务
        /// 此服务只更新智能审核标志和原因，不做任何状态判断
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="checkFlag">智能审核标志：1为智能审核成功，0未进行智能审核，-1审核失败</param>
        /// <param name="checkInfo">智能审核原因说明</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestFormIntellectCheck(long testFormID, int checkFlag, string checkInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IBLisTestForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisTestForm.EditLisTestFormZFSysCheckStatus(testFormID, checkFlag, checkInfo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 检验单检验确认服务
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="empID">确认人ID“</param>
        /// <param name="empName">确认人</param>
        /// <param name="memoInfo">备注</param>
        /// <param name="isCheckTestFormInfo">确认前是否检查检验单相关信息完整：true为检查</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestFormConfirm(long testFormID, long empID, string empName, string memoInfo, bool isCheckTestFormInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IBLisTestForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                string mainTesterId = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
                string mainTester = Cookie.CookieHelper.Read(DicCookieSession.EmployeeName);
                long? longMainTesterId = null;
                if (mainTesterId != null && mainTesterId.Trim().Length > 0)
                    longMainTesterId = long.Parse(mainTesterId);
                baseResultDataValue = IBLisTestForm.EditLisTestFormConfirmByTestFormID(testFormID, empID, empName, longMainTesterId, mainTester, memoInfo, isCheckTestFormInfo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 检验单批量检验确认服务
        /// </summary>
        /// <param name="testFormIDList">检验单ID列表</param>
        /// <param name="empID">确认人ID“</param>
        /// <param name="empName">确认人</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestFormBatchConfirm(string testFormIDList, long empID, string empName, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                string mainTesterId = Cookie.CookieHelper.Read(DicCookieSession.EmployeeID);
                string mainTester = Cookie.CookieHelper.Read(DicCookieSession.EmployeeName);
                long? longMainTesterId = null;
                if (mainTesterId != null && mainTesterId.Trim().Length > 0)
                    longMainTesterId = long.Parse(mainTesterId);
                baseResultDataValue = IBLisTestForm.EditLisTestFormBatchConfirmByTestFormID(testFormIDList, empID, empName, longMainTesterId, mainTester, memoInfo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 检验单取消检验确认服务
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestFormConfirmCancel(long testFormID, long empID, string empName, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IBLisTestForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisTestForm.EditLisTestFormConfirmCancelByTestFormID(testFormID, memoInfo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 检验单审定服务
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="empID">审定人ID“</param>
        /// <param name="empName">审定人</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestFormCheck(long testFormID, long empID, string empName, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IBLisTestForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisTestForm.EditLisTestFormCheckByTestFormID(testFormID, empID, empName, memoInfo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 检验单复检服务
        /// </summary>
        /// <param name="testFormIDList">检验单ID</param>
        /// <param name="listReCheckTestItem">检验单复检项目</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestFormReCheck(long testFormID, IList<LisTestItem> listReCheckTestItem, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IBLisTestForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisTestForm.EditLisTestItemReCheck(testFormID, listReCheckTestItem, memoInfo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 检验单整单取消复检服务
        /// </summary>
        /// <param name="testFormIDList">检验单ID列表字符串</param>
        /// <param name="isClearRedoDesc">是否清除复检原因</param>
        /// <param name="isClearRedoValues">是否清除复检备份结果</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestFormReCheckCancel(string testFormIDList, bool isClearRedoDesc, bool isClearRedoValues, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IBLisTestForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisTestForm.EditLisTestFormReCheckCancel(testFormIDList, isClearRedoDesc, isClearRedoValues, memoInfo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 检验单项目取消复检服务
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="testItemIDList">检验单项目ID列表字符串</param>
        /// <param name="isClearRedoDesc">是否清除复检原因</param>
        /// <param name="isClearRedoValues">是否清除复检备份结果</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestItemReCheckCancel(long testFormID, string testItemIDList, bool isClearRedoDesc, bool isClearRedoValues, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IBLisTestForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisTestForm.EditLisTestItemReCheckCancel(testFormID, testItemIDList, isClearRedoDesc, isClearRedoValues, memoInfo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 检验单批量审定服务
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="empID">审定人ID“</param>
        /// <param name="empName">审定人</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestFormBatchCheck(string testFormIDList, long empID, string empName, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IBLisTestForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisTestForm.EditLisTestFormBatchCheckByTestFormID(testFormIDList, empID, empName, memoInfo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 检验单取消审定服务
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="empID">操作者ID“</param>
        /// <param name="empName">操作者</param>
        /// <param name="memoInfo">备注(取消审定原因)</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestFormCheckCancel(long testFormID, long empID, string empName, string memoInfo)       
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                IBLisTestForm.SysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                if (empID > 0 && (!string.IsNullOrWhiteSpace(empName)))
                {
                    IBLisTestForm.SysCookieValue.EmpID = empID;
                    IBLisTestForm.SysCookieValue.EmpName = empName;
                }
                baseResultDataValue = IBLisTestForm.EditLisTestFormCheckCancelByTestFormID(testFormID, empID, empName, memoInfo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        #region 项目历史对比服务

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testFormID"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_ItemResultHistoryCompareByTestFormID(long testFormID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisTestForm.EditLisTestItemResultHistoryCompareByTestFormID(testFormID);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 获取项目的历史对比值
        /// </summary>
        /// <param name="testForm">检验单实体</param>
        /// <param name="listTestItem">检验单项目实体</param>
        /// <param name="fields">LisTestItem实体属性列表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_ItemResultHistoryCompareByTestItem(LisTestForm testForm, IList<LisTestItem> listTestItem, string fields)
        {

            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestItem> entityList = new EntityList<LisTestItem>();
            try
            {

                entityList = IBLisTestForm.EditLisTestItemResultHistoryCompareByTestItem(testForm, ref listTestItem);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestItem>(entityList);
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

        #region 项目参考值范围判断服务

        /// <summary>
        /// 计算项目值参考范围和状态
        /// </summary>
        /// <param name="testFormID"></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_ItemResultRangeByTestFormID(long testFormID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisTestForm.EditLisTestItemResultHistoryCompareByTestFormID(testFormID);
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

        /// <summary>
        /// 检验单删除恢复服务
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <param name="memoInfo">备注</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestFormDeleteCancel(long testFormID, string memoInfo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisTestForm.EditBatchLisTestFormDeleteCancel(testFormID);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 样本号批量修改
        /// </summary>
        /// <param name="sectionID">检验小组ID</param>
        /// <param name="curTestDate">当前检验日期</param>
        /// <param name="curMinSampleNo">当前起始样本号</param>
        /// <param name="sampleCount">样本数量</param>
        /// <param name="targetTestDate">目标检验日期</param>
        /// <param name="targetMinSampleNo">目标起始样本号<</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_LisTestFormSampleNoModify(long sectionID, string curTestDate, string curMinSampleNo, int sampleCount, string targetTestDate, string targetMinSampleNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisTestForm.EditLisTestFormSampleNoByTargetSampleNo(sectionID, curTestDate, curMinSampleNo, sampleCount, targetTestDate, targetMinSampleNo);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "错误信息：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 检查样本是否符合转换质控物
        /// </summary>
        /// <param name="TestFormID"></param>
        /// <param name="QCMatID"></param>
        /// <returns></returns>
        public BaseResultBool LS_UDTO_CheckSampleConvertStatus(long TestFormID, long QCMatID)
        {
            BaseResultBool baseResultBool = new BaseResultBool();
            try
            {
                baseResultBool = IBLisTestForm.CheckSampleConvertStatus(TestFormID, QCMatID);
            }
            catch (Exception e)
            {
                ZhiFang.LabStar.Common.LogHelp.Debug(e.ToString());
                baseResultBool.success = true;
                baseResultBool.ErrorInfo = e.Message;
            }
            return baseResultBool;
        }

        /// <summary>
        /// 检验追加项目 -- 定制查询检验项目 根据小组项目排序、组合项目排序、项目排序
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_DZQueryLisTestItemByHQL(long TestFormId, int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisTestItemVO> entityList = new EntityList<LisTestItemVO>();
            try
            {
                entityList = IBLisTestForm.DZQueryLisTestItem(TestFormId);
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisTestItemVO>(entityList);
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
                ZhiFang.LabStar.Common.LogHelp.Debug(ex.ToString());
                baseResultDataValue.ErrorInfo = "HQL查询错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 更新检验单打印次数
        /// </summary>
        /// <param name="testFormID">检验单ID</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_UpdateLisTestFormPrintCount(string testFormID)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisTestForm.EditLisTestFormPrintCount(testFormID);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                ZhiFang.LabStar.Common.LogHelp.Debug(ex.ToString());
                baseResultDataValue.ErrorInfo = "更新打印次数错误：" + ex.Message;
                //throw new Exception(ex.Message);
            }
            return baseResultDataValue;
        }

        #endregion

        #region 危急值发送服务

        /// <summary>
        /// 危急值发送服务
        /// </summary>
        /// <param name="testFormMsgID">危急值消息ID字符串列表</param>
        /// <param name="msgSendFlag">发送标志</param>
        /// <param name="msgSendInfo">发送说明</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_SendPanicValueMsgToPlatform(string testFormMsgIDList, int msgSendFlag, string msgSendMemo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                SysCookieValue sysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisTestForm.EditDisposeLisTestItemPanicValue(testFormMsgIDList, msgSendFlag, msgSendMemo, sysCookieValue);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "危急值发送错误：" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 危急值电话通知服务
        /// </summary>
        /// <param name="testFormMsgIDList">危急值消息ID字符串列表</param>
        /// <param name="phoneCallFlag">通知标志</param>
        /// <param name="phoneNumber">电话号码</param>
        /// <param name="phoneReceiver">接听人</param>
        /// <param name="phoneCallInfo">通知备注</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PanicValuePhoneCall(string testFormMsgIDList, int phoneCallFlag, string phoneNumber, string phoneReceiver, string phoneCallMemo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                SysCookieValue sysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisTestForm.EditPanicValuePhoneCallInfo(testFormMsgIDList, phoneCallFlag, phoneNumber, phoneReceiver, phoneCallMemo, sysCookieValue);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "危急值发送错误：" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }

        /// <summary>
        /// 危急值阅读服务
        /// </summary>
        /// <param name="testFormMsgIDList">危急值消息ID字符串列表</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_PanicValueRead(string testFormMsgIDList)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                SysCookieValue sysCookieValue = ZhiFang.LabStar.Common.SysCookieValueHelper.GetSysCookieValue();
                baseResultDataValue = IBLisTestForm.EditPanicValueReadInfo(testFormMsgIDList, sysCookieValue);
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "危急值发送错误：" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }
        #endregion

        #region 仪器检验单定制服务
        public BaseResultDataValue LS_UDTO_QueryLisEquipFormByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisEquipForm> entityList = new EntityList<LisEquipForm>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisEquipForm.QueryLisEquipForm(where, CommonServiceMethod.GetSortHQL(sort), page, limit, fields);
                }
                else
                {
                    entityList = IBLisEquipForm.QueryLisEquipForm(where, "", page, limit, fields);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipForm>(entityList);
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

        public BaseResultDataValue LS_UDTO_QueryLisEquipItemByHQL(int page, int limit, string fields, string where, string sort, bool isPlanish)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            EntityList<LisEquipItem> entityList = new EntityList<LisEquipItem>();
            try
            {
                if ((sort != null) && (sort.Length > 0))
                {
                    entityList = IBLisEquipItem.QueryLisEquipItem(where, CommonServiceMethod.GetSortHQL(sort), page, limit, fields);
                }
                else
                {
                    entityList = IBLisEquipItem.QueryLisEquipItem(where, "", page, limit, fields);
                }
                ParseObjectProperty pop = new ParseObjectProperty(fields);
                try
                {
                    if (isPlanish)
                    {
                        baseResultDataValue.ResultDataValue = pop.GetObjectListPlanish<LisEquipItem>(entityList);
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

        #region 核收服务

        /// <summary>
        /// 快捷核收服务
        /// </summary>
        /// <param name="fieldName">核收字段名，必填，为空默认按条码号BarCode核收</param>
        /// <param name="filedVlaue">核收字段值，必填</param>
        /// <param name="sectionID">小组ID，必填，核收到那个小组</param>
        /// <param name="receiveDate">核收日期，核收到那个日期，为空则核收到当前日期</param>
        /// <param name="sampleNo">指定的样本号</param>
        /// <returns></returns>
        public BaseResultDataValue LS_UDTO_QuickReceiveBarCodeForm(string fieldName, string filedVlaue, long sectionID, string receiveDate, string sampleNo)
        {
            BaseResultDataValue baseResultDataValue = new BaseResultDataValue();
            try
            {
                baseResultDataValue = IBLisTestForm.AddTestFormByQuickReceive(filedVlaue, sectionID, receiveDate, sampleNo);
                if (baseResultDataValue.success)
                {
                    long tesrFormID = 0;
                    if (long.TryParse(baseResultDataValue.ResultDataValue, out tesrFormID))
                        IBLisTestForm.EditLisTestItemAfterTreatment(tesrFormID);
                }
            }
            catch (Exception ex)
            {
                baseResultDataValue.success = false;
                baseResultDataValue.ErrorInfo = "快捷核收【" + filedVlaue + "】错误：" + ex.Message;
                ZhiFang.LabStar.Common.LogHelp.Error(baseResultDataValue.ErrorInfo);
            }
            return baseResultDataValue;
        }
        #endregion
    }
}
